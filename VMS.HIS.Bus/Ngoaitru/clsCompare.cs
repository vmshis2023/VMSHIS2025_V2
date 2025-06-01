using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using System.Text;
using SubSonic;
using NLog;
using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
using System.Collections.Generic;

namespace newBus.Ngoaitru
{
    /// <summary>
    /// Class dùng để copy các thủ tục tương tự nhau để lập trình viên dễ dàng so sánh và copy code. Ví dụ phần thanh toán thường và phần trả tiền
    /// </summary>
    class clsCompare
    {
        public ActionResult Tratien(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, List<Int64> lstIdChitiet)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbThanhtoanChitiet[] ArrKcbThanhtoanChitiet_Huy = new Select().From(KcbThanhtoanChitiet.Schema)
                            .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIdChitiet)
                            .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToArray<KcbThanhtoanChitiet>();

                        List<int> lstIdThanhtoanTinhtoanlai = (from q in ArrKcbThanhtoanChitiet_Huy
                                                               select q.IdThanhtoan).ToList<int>();

                        v_dblTongtienHuy = TongtienKhongTutuc(ArrKcbThanhtoanChitiet_Huy);
                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                           new KcbThanhtoanController()
                           .FetchByQuery(
                               KcbThanhtoan.CreateQuery()
                               .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals, objLuotkham.MaLuotkham)
                               .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                               .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, objThanhtoan.KieuThanhtoan)
                               .AND(KcbThanhtoan.Columns.TrangThai, Comparison.Equals, 0));//Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                        //Lấy tổng tiền của các lần thanh toán trước
                        List<KcbThanhtoanChitiet> lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();

                        foreach (KcbThanhtoan Payment in lstKcbThanhtoanCollection)
                        {
                            KcbThanhtoanChitietCollection lstKcbThanhtoanChitietCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet _KcbThanhtoanChitiet in lstKcbThanhtoanChitietCollection)
                            {
                                if (_KcbThanhtoanChitiet.TuTuc == 0)
                                {
                                    //Lấy các chi tiết sẽ update lại toàn bộ thông tin bhyt,bn chi trả theo % BHYT mới sau khi đã hủy một số dịch vụ
                                    //Các bản ghi hủy sẽ giữ nguyên thông tin không cần cập nhật
                                    if (!lstIdChitiet.Contains(_KcbThanhtoanChitiet.IdChitiet))
                                    {
                                        lstKcbThanhtoanChitiet.Add(_KcbThanhtoanChitiet);
                                        _KcbThanhtoanChitiet.IsNew = false;
                                        _KcbThanhtoanChitiet.MarkOld();
                                    }
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(_KcbThanhtoanChitiet.SoLuong) *
                                                            Utility.DecimaltoDbnull(_KcbThanhtoanChitiet.DonGia);
                                }

                            }
                        }
                        List<int> lstIdThanhtoanCu = (from q in lstKcbThanhtoanChitiet
                                                      select q.IdThanhtoan).Distinct().ToList<int>();



                        LayThongtinPtramBHYT(v_TotalPaymentDetail - v_dblTongtienHuy, objLuotkham, ref PtramBHYT);
                        //Thêm mới dòng thanh toán hủy
                        objThanhtoan.TrangThai = 1;
                        objThanhtoan.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objThanhtoan.NgayThanhtoan = globalVariables.SysDate;
                        objThanhtoan.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(globalVariables.SysDate);
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                        KcbThanhtoanChitiet[] lsttemp = new List<KcbThanhtoanChitiet>().ToArray<KcbThanhtoanChitiet>();
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet, PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        //99% đặt thông số này=1
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TINHLAI_TOANBODICHVU", "1", false) == "1")
                        {
                            foreach (int IdThanhtoan in lstIdThanhtoanCu)
                            {
                                TT_BN = 0m;
                                TT_BHYT = 0m;
                                TT_Chietkhau_Chitiet = 0m;
                                List<KcbThanhtoanChitiet> _LstChitiet = (from q in lstKcbThanhtoanChitiet
                                                                         select q).ToList<KcbThanhtoanChitiet>();

                                if (_LstChitiet.Count > 0)
                                {
                                    foreach (KcbThanhtoanChitiet objThanhtoanDetail in _LstChitiet)
                                    {
                                        TT_BN += (objThanhtoanDetail.BnhanChitra + objThanhtoanDetail.PhuThu) * objThanhtoanDetail.SoLuong;
                                        if (!Utility.Byte2Bool(objThanhtoanDetail.TuTuc))
                                            TT_BHYT += objThanhtoanDetail.BhytChitra * objThanhtoanDetail.SoLuong;
                                        TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objThanhtoanDetail.TienChietkhau, 0);
                                        //Lưu lại các thông tin tiền đã được tính toán lại ở thủ tục THU_VIEN_CHUNG.TinhPhamTramBHYT(...)
                                        objThanhtoanDetail.IsNew = false;
                                        objThanhtoanDetail.MarkOld();
                                        objThanhtoanDetail.Save();
                                    }
                                    //Update lại tiền thanh toán
                                    new Update(KcbThanhtoan.Schema)
                      .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                      .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                      .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                      .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                      .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                      .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                      .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    //Update phiếu thu
                                    new Update(KcbPhieuthu.Schema)
                     .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                     .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                     .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                }

                            }
                        }
                        //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                        TT_BN = 0m;
                        TT_BHYT = 0m;
                        TT_Chietkhau_Chitiet = 0m;
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in ArrKcbThanhtoanChitiet_Huy)
                        {
                            TT_BN += (objKcbThanhtoanChitiet.BnhanChitra + objKcbThanhtoanChitiet.PhuThu) * objKcbThanhtoanChitiet.SoLuong;
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra * objKcbThanhtoanChitiet.SoLuong;
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);

                            new Update(KcbThanhtoanChitiet.Schema)
                                .Set(KcbThanhtoanChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                .Set(KcbThanhtoanChitiet.Columns.NgayHuy).EqualTo(globalVariables.SysDate)
                                .Set(KcbThanhtoanChitiet.Columns.NguoiHuy).EqualTo(globalVariables.UserName)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).IsEqualTo(objKcbThanhtoanChitiet.IdChitiet).
                                Execute();
                            ///thanh toán khám chữa bệnh))
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                            {

                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objKcbThanhtoanChitiet.IdPhieu).Execute();
                            }
                            ///thah toán phần dịch vụ cận lâm sàng
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                            {
                                KcbChidinhclsChitiet objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdChitietdichvu);
                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)//Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objKcbThanhtoanChitiet.IdChitietdichvu)
                                    .Execute();
                            }
                            ///thanh toán phần thuốc
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                            {
                                KcbDonthuocChitiet objKcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbDonthuocChitiet != null && Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }
                                }
                                new Update(KcbDonthuoc.Schema)
                                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objKcbThanhtoanChitiet.IdPhieu).Execute();
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            //Tạo dữ liệu hủy tiền

                            objKcbThanhtoanChitiet.IdChitiethuy = objKcbThanhtoanChitiet.IdChitiet;//Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                            objKcbThanhtoanChitiet.NguoiHuy = null;
                            objKcbThanhtoanChitiet.NgayHuy = null;
                            objKcbThanhtoanChitiet.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objKcbThanhtoanChitiet.IsNew = true;
                            objKcbThanhtoanChitiet.Save();
                        }

                        KcbPhieuthu objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;

                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1);//0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 1);
                        objPhieuthu.NgayThuchien = globalVariables.SysDate;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = (byte)objThanhtoan.NoiTru;
                        objPhieuthu.LydoNop = "Trả tiền bệnh nhân";
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        new Update(KcbThanhtoan.Schema)
                       .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                       .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                       .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                       .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                       .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                       .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                       .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }

        }
    }
}
