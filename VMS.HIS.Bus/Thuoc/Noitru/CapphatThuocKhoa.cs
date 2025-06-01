using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
using VNS.Properties;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class CapphatThuocKhoa
    {
        private NLog.Logger log;
        public CapphatThuocKhoa()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        public ActionResult Kiemtrathuocxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt, ref string errMsg)
        {
            //TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
            //  TThuockho.CreateQuery()
            //  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhoxuat)
            //  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
            //  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
            //  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
            //  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
            //  .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
            //  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
            //   .AND(TThuockho.NgayNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayNhap)
            //    .AND(TThuockho.GiaBhytColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBhyt)
            //  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
            //  );

            //if (vCollection.Count <= 0)
            //{
            //    errMsg = string.Format("ID thuốc={0}, không tồn tại trong kho {1}", objPhieuNhapCt.IdThuoc.ToString(), objPhieuNhap.IdKhonhap.ToString());
            //    return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            //}
            //Thay id_thuockho=id_chuyen. id_chuyen chính là id_thuockho của kho xuất, còn bản chất id_thuockho là id_thuockho của kho nhập sau khi xác nhận
            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdChuyen).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;
            //if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi mất dòng dữ liệu trong bảng kho-thuốc


            decimal SluongChia = Utility.DecimaltoDbnull(objPhieuNhapCt.SluongChia, 1);
            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0][0], 0);// vCollection[0].SoLuong;
            decimal slconlai = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);
            //if (SluongChia > 1)
            //    SoLuong = SoLuong * SluongChia;
            if (slconlai < 0)
            {
                errMsg = string.Format("ID thuốc={0}, Số lượng còn trong kho {1}, Số lượng bị trừ {2}", objPhieuNhapCt.IdThuoc.ToString(), SoLuong.ToString(), objPhieuNhapCt.SoLuong.ToString());
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            }
            return ActionResult.Success;
        }
        public ActionResult XacNhanPhieuCapphatThuoc(TPhieuNhapxuatthuoc objPhieuNhap, DateTime ngayxacnhan, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        TDmucKho objkhonhap=TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat=TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa=DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region//Tách id phiếu,Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho

                            //ActionResult _Kiemtrathuocxacnhan = Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            #endregion
                           
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdChuyen.Value, true, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            long idthuockho = -1;
                            
                            int SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
                            //Chú ý khi lập phiếu xuất thuốc tủ trực thì
                            //objPhieuNhapCt.SoLuong= số lượng đã chia-->Cần trừ số lượng trong kho xuất theo số lượng nguyên gốc. Tức là phải lấy số lượng này / số lượng chia
                            decimal _SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0) *SluongChia;//Số lượng thực sự bị mất khỏi kho xuất(khi xuất thuốc sang tủ trực)
                            StoredProcedure sp;
                            if (objPhieuNhap.IdKhonhap > 0)// dùng chung với cấp phát hao phí(hao phí chỉ có id khoa lĩnh)
                            {
                                sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                          _SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                          objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, idthuockho, objPhieuNhapCt.IdThuockho, ngayxacnhan,
                                                                          objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                                sp.Execute();
                                idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            }
                           
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                           0, errorMessage);

                            sp.Execute();
                            if (idthuockho > 0)
                                new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                                   .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            else
                                idthuockho =Utility.Int64Dbnull( objPhieuNhapCt.IdThuockho,-1);
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            ////Insert dòng kho nhập//Có thể xóa phần này 
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);

                            objXuatNhap.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen);
                            objXuatNhap.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen);
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);

                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdThuockho);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;
                            
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapKho);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc( objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            //Insert dòng của kho xuất
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            
                            objXuatNhap.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen);
                            objXuatNhap.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen);
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);

                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.IdChuyen = -1;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.MaLoaiphieu = objPhieuNhap.LoaiPhieu;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu((LoaiPhieu) objPhieuNhap.LoaiPhieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();

                        }
                        string LACTN = string.Format("Xác nhận(duyệt phiếu) bởi {0} vào lúc {1} tại địa chỉ {2}", globalVariables.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), globalVariables.gv_strIPAddress);
                        Utility.Log("frm_PhieuCapphatThuocKhoa", globalVariables.UserName, string.Format("Xác nhận phiếu cấp phát thuốc tủ trực khoa nội trú ID={0} thành công", objPhieuNhap.IdPhieu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                        int _numAffected = new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Set(TPhieuNhapxuatthuoc.Columns.LastActionName).EqualTo(LACTN)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        if (_numAffected > 0)
                        {
                            objPhieuNhap.TrangThai = 1;
                            new Delete().From(TTamke.Schema).Where(TTamke.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).And(TTamke.Columns.Loai).IsEqualTo(Utility.ByteDbnull(objPhieuNhap.LoaiPhieu)).Execute();
                        }
                        else
                            return ActionResult.UNKNOW;
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xác nhận phiếu cấp phát thuốc-khoa", ex);
                return ActionResult.Error;
            }
        }
        public ActionResult XacNhanPhieuTrathuocTutrucKhoaveKho(TPhieuNhapxuatthuoc objPhieuNhap, DateTime ngayxacnhan, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        TDmucKho objkhonhap = TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat = TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa = DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region //Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho
                            //ActionResult _Kiemtrathuocxacnhan = Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc còn đủ trong kho xuất hay không
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdChuyen.Value, true, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            long idthuockho = -1;
                           //Nhập vào tủ trực
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                      objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau,
                                                                      -1, idthuockho, objPhieuNhapCt.IdThuockho, ngayxacnhan, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, 
                                                                      objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            //Lấy về Id_thuockho tương ứng trong tủ trực
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            
                            //Trừ tủ trực theo đơn vị chia
                            int SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
                            //Chú ý khi lập phiếu xuất thuốc tủ trực thì
                            //objPhieuNhapCt.SoLuong= số lượng đã chia-->Cần trừ số lượng trong kho xuất theo số lượng nguyên gốc. Tức là phải lấy số lượng này / số lượng chia
                            decimal _SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0) *SluongChia;//Số lượng thực sự bị mất khỏi kho xuất(khi xuất thuốc sang tủ trực)
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          _SoLuong, objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                           0, errorMessage);

                            sp.Execute();
                            if (idthuockho > 0)
                                new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                                   .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            else
                                idthuockho = Utility.Int64Dbnull(objPhieuNhapCt.IdThuockho, -1);
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            //Insert dòng kho nhập
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);

                            objXuatNhap.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen);
                            objXuatNhap.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen);
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);
                            objXuatNhap.GiaBhytCu = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhytCu);

                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdThuockho);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;//Chính là khoa trả
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapTraKhoLe;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraKhoLe);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            //Insert dòng của kho xuất
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);

                            objXuatNhap.GiaPhuthuDungtuyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen);
                            objXuatNhap.GiaPhuthuTraituyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen);
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);
                            objXuatNhap.GiaBhytCu = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhytCu);

                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapTraKhoLe;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatTraKhoLe);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();

                        }
                        Utility.Log("frm_PhieuTrathuocKhoaveKho", globalVariables.UserName, string.Format("Xác nhận phiếu trả thuốc từ tủ trực về kho nội trú ID={0} thành công", objPhieuNhap.IdPhieu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                       int _numAffected= new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        if (_numAffected > 0)
                        {
                            objPhieuNhap.TrangThai = 1;
                            new Delete().From(TTamke.Schema).Where(TTamke.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).And(TTamke.Columns.Loai).IsEqualTo(Utility.ByteDbnull(objPhieuNhap.LoaiPhieu)).Execute();
                        }
                        else
                            return ActionResult.UNKNOW;
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xác nhận phiếu trả thuốc từ tủ trực khoa nội trú về kho lẻ",ex);
                return ActionResult.Error;
            }
        }
       
        
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt, ref string errMsg)
        {
            //TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
            //  TThuockho.CreateQuery()
            //  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
            //  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
            //  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
            //  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
            //  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
            //  .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
            //  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
            //  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
            //  );

            //if (vCollection.Count <= 0)
            //{
            //    errMsg = string.Format("ID thuốc={0}, không tồn tại trong kho {1}", objPhieuNhapCt.IdThuoc.ToString(), objPhieuNhap.IdKhonhap.ToString());
            //    return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            //}
            //decimal SluongChia = Utility.Int32Dbnull(objPhieuNhapCt.SluongChia, 1);
            //if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            //decimal SoLuong = vCollection[0].SoLuong;
            //if (SluongChia > 1)
            //    SoLuong = vCollection[0].SoLuong * SluongChia;
            //SoLuong = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);

            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdChuyen).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;
            //if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi mất dòng dữ liệu trong bảng kho-thuốc


            decimal SluongChia = Utility.DecimaltoDbnull(objPhieuNhapCt.SluongChia, 1);
            if (SluongChia <= 0) SluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0][0], 0);// vCollection[0].SoLuong;
            decimal slconlai = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);

            if (SoLuong < 0)
            {
                errMsg = string.Format("ID thuốc={0}, Số lượng còn trong kho {1}, Số lượng bị trừ {2}", objPhieuNhapCt.IdThuoc.ToString(), SoLuong, objPhieuNhapCt.SoLuong.ToString());
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            }
            return ActionResult.Success;
        }
        public ActionResult HuyXacNhanPhieuCapphatThuoc(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
        {
            string GUID = THU_VIEN_CHUNG.GetGUID();
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            //Kiểm tra ở kho nhập xem thuốc đã sử dụng chưa
                            if (Utility.Int16Dbnull(objPhieuNhap.IdKhonhap, 0) > 0)
                            {
                                //Kiểm tra xem thuốc trong kho thanh lý còn đủ để trừ hay không(khả dụng có tính cả số lượng này)
                                ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdThuockho.Value, false, ref errMsg);
                                if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            }
                            //Xóa biến động kho nhập
                            if ((byte)objPhieuNhap.LoaiPhieu ==(byte) LoaiPhieu.PhieuXuatKhoa)//Xóa theo kho nhập
                            {
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                    .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                    .And(TBiendongThuoc.Columns.IdKho).IsEqualTo(objPhieuNhap.IdKhonhap)
                                    .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuNhapKho).Execute();
                            }
                            else//Xóa theo khoa lĩnh với phiếu xuất hao phí phòng chức năng
                            {
                                new Delete().From(TBiendongThuoc.Schema)
                                   .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                   .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                   .And(TBiendongThuoc.Columns.IdKhoaLinh).IsEqualTo(objPhieuNhap.IdKhoalinh)
                                   .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuNhapKho).Execute();
                            }
                            //Xóa biến động kho xuất
                            new Delete().From(TBiendongThuoc.Schema)
                               .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                               .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                               .And(TBiendongThuoc.Columns.IdKho).IsEqualTo(objPhieuNhap.IdKhoxuat)
                               .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)objPhieuNhap.LoaiPhieu).Execute();

                            long id_thuockho = -1;
                            //Cộng trả lại kho xuất
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap,
                                                                      objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, id_thuockho, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt
                                                                      , objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            #region tách id phiếu
                            //int numofRec = -1;
                            //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(objPhieuNhapCt.SoLuong, objPhieuNhapCt.IdChuyen, numofRec);
                            //sp.Execute();
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //if (numofRec <= 0)
                            //{
                            //    errMsg = Utility.sDbnull(objPhieuNhapCt.IdChuyen, "0");
                            //    return ActionResult.notExists;
                            //}
                            #endregion
                            //Trừ thuốc từ tủ trực
                            decimal sluongChia = Utility.DecimaltoDbnull(objPhieuNhapCt.SluongChia, 1);
                            if (sluongChia <= 0) sluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
                            //Chú ý khi lập phiếu xuất thuốc tủ trực thì
                            //objPhieuNhapCt.SoLuong= số lượng đã chia-->Cần trừ số lượng trong kho xuất theo số lượng nguyên gốc. Tức là phải lấy số lượng này / số lượng chia
                            decimal SluongChuachia = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0) * sluongChia;//Số lượng thực sự bị mất khỏi kho xuất(khi xuất thuốc sang tủ trực)
                            if (objPhieuNhap.IdKhonhap > 0)//Áp dụng đối với xuất tủ trực. Xuất hao phí không dùng(99% ko sử dụng)
                            {
                                sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuoc,
                                                              objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                              Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                              SluongChuachia, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,  0, errorMessage);
                                sp.Execute();
                            }
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", objPhieuNhap.IdKhonhap > 0 ? Utility.Int64Dbnull(objPhieuNhapCt.IdChuyen) : Utility.Int64Dbnull(objPhieuNhapCt.IdThuockho), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), Utility.ByteDbnull(objPhieuNhap.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                  "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhap.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh) : (objPhieuNhap.LoaiPhieu == 6 ? string.Format("Xuất thuốc từ kho nội trú sang tủ trực khoa nội trú. Id kho chuyển: {0} sang  id kho nhận: {1} thuộc khoa {2} sang", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhonhap, objPhieuNhap.IdKhoalinh) : string.Format("Trả thuốc từ tủ trực nội trú về kho nội trú. Id kho chuyển: {0} thuộc khoa {1} sang id kho nhận: {2}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh, objPhieuNhap.IdKhonhap)));
                        }
                        Utility.Log("frm_PhieuCapphatThuocKhoa", globalVariables.UserName, string.Format("Hủy xác nhận phiếu cấp phát thuốc tủ trực khoa nội trú ID={0} thành công", objPhieuNhap.IdPhieu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu cấp phát thuốc-khoa", ex);
                return ActionResult.Error;
            }
        }

        public ActionResult HuyXacNhanPhieuTrathuocTutrucKhoaVeKho(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            string GUID = THU_VIEN_CHUNG.GetGUID();
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region//Kiểm tra ở kho nhập xem thuốc đã sử dụng chưa
                            //ActionResult _Kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc còn đủ trong tủ trực hay không để hủy trả về kho lẻ nội trú
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdThuockho.Value, true, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            long idthuockho = -1;

                            //Xóa biến động kho nhập
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)objPhieuNhap.LoaiPhieu).Execute();
                            //Xóa biến động kho xuất--Xem sau vì thừa so với phía trên(01/02/2024)
                            new Delete().From(TBiendongThuoc.Schema)
                               .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                               .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                               .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)objPhieuNhap.LoaiPhieu).Execute();

                            //Cộng trả lại kho xuất
                            //Nhập theo tủ trực theo số lượng chia
                            decimal sluongChia = Utility.DecimaltoDbnull(objPhieuNhapCt.SluongChia, 1);
                            if (sluongChia <= 0) sluongChia = 1;//Nếu lỗi do người dùng sửa tay thì tự động đặt=1
                            //Chú ý khi lập phiếu xuất thuốc tủ trực thì
                            //objPhieuNhapCt.SoLuong= số lượng đã chia-->Cần trừ số lượng trong kho xuất theo số lượng nguyên gốc. Tức là phải lấy số lượng này / số lượng chia
                            decimal sluong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0)* sluongChia;//Số lượng thực sự bị mất khỏi kho xuất(khi xuất thuốc sang tủ trực)
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      sluong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap,
                                                                      objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, idthuockho, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt,
                                                                      objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            #region Tách theo id phiếu
                            //int numofRec = -1;
                            //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(sluong, objPhieuNhapCt.IdChuyen, numofRec);
                            //sp.Execute();
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //if (numofRec <= 0)
                            //{
                            //    errMsg = Utility.sDbnull(objPhieuNhapCt.IdChuyen, "0");
                            //    return ActionResult.notExists;
                            //}
                            #endregion

                            //Xuất thứ nguyên từ kho nhận
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          objPhieuNhapCt.SoLuong, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,  0, errorMessage);
                            sp.Execute();
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapCt.IdChuyen), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), Utility.ByteDbnull(objPhieuNhap.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhap.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh) : string.Format("Trả thuốc từ tủ trực nội trú về kho nội trú. Id kho chuyển: {0} thuộc khoa {1} sang id kho nhận: {2}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh, objPhieuNhap.IdKhonhap));
                            //Cạp nhật lại id_thuockho =-1(giá trị này được update khi xác nhận phiếu). Giá trị id_chuyen cho biết chuyển từ id_thuockho của kho nội trú.
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(-1)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                        }
                        Utility.Log("frm_PhieuTrathuocKhoaveKho", globalVariables.UserName, string.Format("Hủy xác nhận phiếu trả thuốc từ tủ trực về kho nội trú ID={0} thành công", objPhieuNhap.IdPhieu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu trả thuốc từ tủ trực về kho lẻ nội trú",ex);
                return ActionResult.Error;
            }
        }
       
        public ActionResult CappnhatPhieucapphatNoitru(TPhieuCapphatNoitru _phieucapphat, List<TPhieuCapphatChitiet> lstPhieuCapphatCt, List<TThuocCapphatChitiet> lstThuocCapphatCt)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {

                        new Update(TPhieuCapphatNoitru.Schema).Set(TPhieuCapphatNoitru.NgayNhapColumn).EqualTo(_phieucapphat.NgayNhap.Date)
                            .Set(TPhieuCapphatNoitru.TuNgayColumn).EqualTo(_phieucapphat.TuNgay.Date)
                            .Set(TPhieuCapphatNoitru.DenNgayColumn).EqualTo(_phieucapphat.DenNgay.Date)
                            .Set(TPhieuCapphatNoitru.IdKhoXuatColumn).EqualTo(_phieucapphat.IdKhoXuat)
                             .Set(TPhieuCapphatNoitru.NgaySuaColumn).EqualTo(_phieucapphat.NgaySua.Value.Date)
                            .Set(TPhieuCapphatNoitru.NguoiSuaColumn).EqualTo(_phieucapphat.NguoiSua)
                            .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(_phieucapphat.IdCapphat).Execute();
                        List<string> lstIddonthuoc = (from p in lstPhieuCapphatCt.AsEnumerable()
                                                    select Utility.sDbnull(p.IdDonthuoc, "-1")).ToList<string>();
                        new Delete().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(
                            _phieucapphat.IdCapphat).Execute();
                        foreach (var _PhieuCapphatCt in lstPhieuCapphatCt)
                        {
                            _PhieuCapphatCt.IdCapphat = _phieucapphat.IdCapphat;
                            _PhieuCapphatCt.IsNew = true;
                            _PhieuCapphatCt.Save();
                        }
                        new Delete().From(TThuocCapphatChitiet.Schema).Where(TThuocCapphatChitiet.Columns.IdCapphat).IsEqualTo(
                           _phieucapphat.IdCapphat).Execute();
                        foreach (var _ThuocCapphatCt in lstThuocCapphatCt)
                        {
                            _ThuocCapphatCt.IdCapphat = _phieucapphat.IdCapphat;
                            _ThuocCapphatCt.IsNew = true;
                            _ThuocCapphatCt.Save();
                        }
                        SPs.ThuocCapnhatthongtincapphatDonthuocChitiet(string.Join(",", lstIddonthuoc.ToArray<string>()), _phieucapphat.IdCapphat, 1).Execute();
                        Scope.Complete();
                    }
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult XoaPhieuCapPhatNoiTru(int ID_CAPPHAT)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SPs.ThuocXoaphieucapphatnoitru(ID_CAPPHAT).Execute();
                        //new Delete().From(TPhieuCapphatNoitru.Schema).Where(TPhieuCapphatNoitru.Columns.IdCapphat).IsEqualTo(ID_CAPPHAT).Execute();
                        //new Delete().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(ID_CAPPHAT).Execute();
                        //new
                        Scope.Complete();
                    }
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult BenhNhanLinhThuoc13123(int ID_CAPPHAT, int ID_DONTHUOC,long IdChitietdonthuoc, int Trang_thai, int soluongtralai, int thuclinh)
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Update(TPhieuCapphatChitiet.Schema)
                            .Set(TPhieuCapphatChitiet.DaLinhColumn.ColumnName).EqualTo(Trang_thai)
                            .Set(TPhieuCapphatChitiet.ThucLinhColumn.ColumnName).EqualTo(thuclinh)
                            .Set(TPhieuCapphatChitiet.SoLuongtralaiColumn.ColumnName).EqualTo(soluongtralai)
                            .Where(TPhieuCapphatChitiet.IdCapphatColumn).IsEqualTo(ID_CAPPHAT)
                            .And(TPhieuCapphatChitiet.IdDonthuocColumn).IsEqualTo(ID_DONTHUOC)
                            .And(TPhieuCapphatChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc)
                            .Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public static ActionResult BenhNhanLinhThuoc(int idPhieucapphat,List<int> lstIdDonthuoc, int Trang_thai,ref List<int> lstNoValidData)
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        foreach (int IdDonthuoc in lstIdDonthuoc)
                        {
                            if (Trathuocthua.ThuocNoitruKiemtraThuoctralai(idPhieucapphat, IdDonthuoc))
                            {
                                lstNoValidData.Add(IdDonthuoc);
                                continue;
                            }

                            DataTable dtData = SPs.ThuocNoitruLaychitietdonthuocTheophieulinhthuocnoitru(idPhieucapphat, IdDonthuoc).GetDataSet().Tables[0];
                            foreach (DataRow dr in dtData.Rows)
                            {
                                int ID_CAPPHAT = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdCapphat], -1);
                                int ID_DONTHUOC = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdDonthuoc], -1);
                                int IdChitietdonthuoc = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdChitietdonthuoc], -1);
                                int soluongtralai = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.SoLuongtralai], 0);
                                int SoLuong = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.SoLuong], 0);
                                int thuclinh = SoLuong - soluongtralai;// Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.ThucLinh], -1);
                                if(Trang_thai==0)
                                {
                                    soluongtralai = 0;
                                    thuclinh = 0;
                                }
                                new Update(TPhieuCapphatChitiet.Schema)
                                    .Set(TPhieuCapphatChitiet.DaLinhColumn.ColumnName).EqualTo(Trang_thai)
                                    .Set(TPhieuCapphatChitiet.ThucLinhColumn.ColumnName).EqualTo(thuclinh)
                                    .Set(TPhieuCapphatChitiet.SoLuongtralaiColumn.ColumnName).EqualTo(soluongtralai)
                                    .Set(TPhieuCapphatChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                    .Set(TPhieuCapphatChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
                                    .Where(TPhieuCapphatChitiet.IdCapphatColumn).IsEqualTo(ID_CAPPHAT)
                                    .And(TPhieuCapphatChitiet.IdDonthuocColumn).IsEqualTo(ID_DONTHUOC)
                                    .And(TPhieuCapphatChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc)
                                    .Execute();
                            }
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public static ActionResult CapnhatThuclinh(long id_chitiet, decimal thuc_linh, decimal so_luongtralai)
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Update(TPhieuCapphatChitiet.Schema)
                            .Set(TPhieuCapphatChitiet.Columns.ThucLinh).EqualTo(thuc_linh)
                            .Set(TPhieuCapphatChitiet.Columns.SoLuongtralai).EqualTo(so_luongtralai)
                            .Where(TPhieuCapphatChitiet.Columns.IdChitiet).IsEqualTo(id_chitiet)
                            .Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public static ActionResult BenhNhanLinhThuoc(DataTable dtData,int Trang_thai )
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        foreach (DataRow dr in dtData.Rows)
                        {
                            int ID_CAPPHAT=Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdCapphat],-1);
                            int ID_DONTHUOC=Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdDonthuoc],-1);
                            int SoLuong = Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.SoLuong], 0);
                            int IdChitietdonthuoc=Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.IdChitietdonthuoc],-1);
                            int soluongtralai=Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.SoLuongtralai],0);
                            int thuclinh = SoLuong - soluongtralai;// Utility.Int32Dbnull(dr[TPhieuCapphatChitiet.Columns.ThucLinh], -1);
                            if (Trang_thai == 0)
                            {
                                soluongtralai = 0;
                                thuclinh = 0;
                            }
                            new Update(TPhieuCapphatChitiet.Schema)
                                .Set(TPhieuCapphatChitiet.DaLinhColumn.ColumnName).EqualTo(Trang_thai)
                                .Set(TPhieuCapphatChitiet.ThucLinhColumn.ColumnName).EqualTo(thuclinh)
                                .Set(TPhieuCapphatChitiet.SoLuongtralaiColumn.ColumnName).EqualTo(soluongtralai)
                                .Set(TPhieuCapphatChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(TPhieuCapphatChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
                                .Where(TPhieuCapphatChitiet.IdCapphatColumn).IsEqualTo(ID_CAPPHAT)
                                .And(TPhieuCapphatChitiet.IdDonthuocColumn).IsEqualTo(ID_DONTHUOC)
                                .And(TPhieuCapphatChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc)
                                .Execute();
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult XacnhanphieuCapphatNoitru(long ID_CAPPHAT,short ID_KHO,DateTime ngaythuchien)
        {
            ActionResult _result=ActionResult.Success;
           string GUID = THU_VIEN_CHUNG.GetGUID();
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {

                        DataTable _lstPres = SPs.ThuocLaydonthuocTrongphieucapphatNoitru(Utility.Int32Dbnull(ID_CAPPHAT)).GetDataSet().Tables[0];
                        bool codulieu = false;
                        //Xác nhận từng đơn thuốc nội trú
                        foreach (DataRow presRow in _lstPres.Rows)
                        {
                            codulieu = true;
                            //if (Utility.Int32Dbnull(presRow["Status"]) == 0)//Chưa được cấp phát
                            //{
                                KcbDonthuoc pres = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(presRow[KcbDonthuoc.Columns.IdDonthuoc]));
                                //_result = Kiemtrasoluongthuoctrongkho(pres, ID_KHO);
                                //if (_result != ActionResult.Success) return _result;
                                ////Thực hiện cấp phát thuốc
                                TPhieuXuatthuocBenhnhan objXuatBnhan = CreatePhieuXuatBenhNhanNoitru(pres, ID_KHO, (int)ID_CAPPHAT);
                                objXuatBnhan.NgayXacnhan = ngaythuchien;
                                 _result =
                                    new XuatThuoc().Linhthuocnoitru(pres,objXuatBnhan, ID_KHO, ngaythuchien);
                                 if (_result != ActionResult.Success) return _result;
                            //}
                        }
                        if (!codulieu) return ActionResult.DataChanged;
                        //Cập nhật trạng thái cấp phát của phiếu đề nghị=1
                        new Update(TPhieuCapphatNoitru.Schema)
                            .Set(TPhieuCapphatNoitru.Columns.TrangThai).EqualTo(1)
                            .Set(TPhieuCapphatNoitru.Columns.IdKhoXuat).EqualTo(ID_KHO)
                            .Set(TPhieuCapphatNoitru.Columns.IdNhanviencapphat).EqualTo(globalVariablesPrivate.objNhanvien != null ? globalVariablesPrivate.objNhanvien.IdNhanvien : globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuCapphatNoitru.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuCapphatNoitru.Columns.NgayXacnhan).EqualTo( DateTime.Now)
                            .Set(TPhieuCapphatNoitru.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuCapphatNoitru.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Where(TPhieuCapphatNoitru.Columns.IdCapphat).IsEqualTo(ID_CAPPHAT)
                            .And(TPhieuCapphatNoitru.Columns.TrangThai).IsEqualTo(0).Execute();
                    }
                    Scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult KiemtratonthuocNgoaitru(DataTable dtThuocchuaCapphat, short ID_KHO, bool isCheckAll, ref Dictionary<long, string> lstID_Err)
        {
            ActionResult _result = ActionResult.Success;
            string errMsg = "";
            try
            {
                foreach (DataRow dr in dtThuocchuaCapphat.Rows)
                {
                    KcbDonthuocChitiet objChitiet = KcbDonthuocChitiet.FetchByID(Utility.Int64Dbnull( dr[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]));
                    if (objChitiet != null && objChitiet.TrangThai<=0)//Chưa được lĩnh mới kiểm tra
                    {
                        decimal SOLUONG_LINH = objChitiet.SoLuong;
                        if (Utility.DecimaltoDbnull(objChitiet.SluongSua, 0) > 0)
                            SOLUONG_LINH = Utility.DecimaltoDbnull(objChitiet.SluongSua, 0);
                        _result = KiemTra.KiemtraTonthuoctheoIdthuockho_ngoaitru(objChitiet.IdThuoc, (Int16)ID_KHO, SOLUONG_LINH, 1m, objChitiet.IdThuockho.Value, false, ref errMsg);
                        if (_result != ActionResult.Success)
                        {
                            if (!lstID_Err.ContainsKey(objChitiet.IdThuockho.Value))
                                lstID_Err.Add(objChitiet.IdThuoc, errMsg);
                            else
                            {
                                string pre_errMsg = lstID_Err[objChitiet.IdThuockho.Value];
                                pre_errMsg = pre_errMsg + ";\n" + errMsg;
                                lstID_Err[objChitiet.IdThuoc] += pre_errMsg;
                            }
                            //lstID_Err.Add(objChitiet.IdThuockho.Value, errMsg);
                            if (isCheckAll)
                            {
                            }
                            else
                                return _result;
                        }

                        //_result = Kiemtrasoluongthuoctrongkho(pres, ID_KHO);
                        //if (_result != ActionResult.Success) return _result;
                    }
                }
                if (lstID_Err.Count > 0) return ActionResult.NotEnoughDrugInStock;
                return ActionResult.Success;
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult Kiemtratonthuoc(long ID_CAPPHAT, short ID_KHO,bool isCheckAll,ref Dictionary<long,string> lstID_Err)
        {
            ActionResult _result = ActionResult.Success;
            string errMsg = "";
            try
            {
                TPhieuCapphatChitietCollection lstCapphatchitiet=new Select().From(TPhieuCapphatChitiet.Schema)
                     .Where(TPhieuCapphatChitiet.IdCapphatColumn).IsEqualTo(ID_CAPPHAT).ExecuteAsCollection<TPhieuCapphatChitietCollection>();
                //Xác nhận từng đơn thuốc nội trú
                foreach (TPhieuCapphatChitiet pres in lstCapphatchitiet)
                {
                    if (pres.DaLinh == 0)//Chưa được lĩnh mới kiểm tra
                    {
                        decimal SOLUONG_LINH = pres.SoLuong;
                        _result = KiemTra.KiemtraTonthuoctheoIdthuockho(pres.IdThuoc, (Int16)ID_KHO, SOLUONG_LINH, 1m, pres.IdThuockho.Value, true, ref errMsg);
                        if (_result != ActionResult.Success)
                        {
                            if (!lstID_Err.ContainsKey(pres.IdThuoc))
                                lstID_Err.Add(pres.IdThuoc, errMsg);
                            else
                            {
                                string pre_errMsg = lstID_Err[pres.IdThuoc];
                                pre_errMsg = pre_errMsg + ";\n" + errMsg;
                                lstID_Err[pres.IdThuoc] += pre_errMsg;
                            }
                            if (isCheckAll)
                            {
                            }
                            else
                                return _result;
                        }

                        //_result = Kiemtrasoluongthuoctrongkho(pres, ID_KHO);
                        //if (_result != ActionResult.Success) return _result;
                    }
                }
                if (lstID_Err.Count > 0) return ActionResult.NotEnoughDrugInStock;
                return ActionResult.Success;
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacnhanphieuCapphatNoitru(long ID_CAPPHAT, short ID_KHO, ref string err)
        {
            ActionResult _result = ActionResult.Success;
            string GUID = THU_VIEN_CHUNG.GetGUID();
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        bool codulieu = false;
                        //Lấy về danh sách các phiếu cấp phát BN ứng với đợt cấp phát này(Khi xác nhận phiếu cấp phát sẽ có dữ liệu)
                        TPhieuXuatthuocBenhnhanCollection lstPhieu = new Select().From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdCapphatColumn).IsEqualTo(ID_CAPPHAT).ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>();
                        //Mỗi phiếu này quan hệ 1-1 với 1 đơn thuốc. 
                        //Đơn thuốc cấp phát bao nhiêu lần thì có bấy nhiêu phiếu. Mỗi lần bao nhiêu chi tiết thì có bấy nhiêu phiếu chi tiết đi kèm
                        foreach (TPhieuXuatthuocBenhnhan phieuxuat in lstPhieu)
                        {
                            DataTable dtluotkham = SPs.KcbLaythongtinluotkham(phieuxuat.IdBenhnhan, phieuxuat.MaLuotkham).GetDataSet().Tables[0];
                            if (dtluotkham.Rows.Count > 0)
                            {
                                if (Utility.ByteDbnull(dtluotkham.Rows[0]["trangthai_noitru"], 0) >= 3)//Đã lập phiếu ra viện
                                {
                                    err = string.Format("Phiếu cấp phát có chứa đơn thuốc của người bệnh đã làm thủ tục ra viện {0} nên không cho phép hủy xác nhận", Utility.sDbnull(dtluotkham.Rows[0]["ten_benhnhan"], "KXĐ"));
                                    return ActionResult.DataUsed;
                                }
                            }
                            codulieu = true;
                            //Lấy về phiếu cấp phát chi tiết để cộng lại kho xuất
                            TPhieuXuatthuocBenhnhanChitietCollection lstChitiet = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.IdPhieuColumn).IsEqualTo(phieuxuat.IdPhieu).ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            //Phần mới này thì mỗi detail chỉ có duy nhất 1 phieuxuatchitiet
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in lstChitiet)
                            {
                                //Cộng trả lại kho xuất
                                long id_Thuockho_new = -1;
                                long iTThuockho_old = PhieuXuatBnhanCt.IdThuockho.Value;
                                TThuockho objTK = TThuockho.FetchByID(iTThuockho_old);
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                                                     PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                     PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                                                     PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau,
                                                                     PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.IdThuockho, PhieuXuatBnhanCt.NgayNhap,
                                                                     PhieuXuatBnhanCt.GiaBhyt, PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen, phieuxuat.KieuThuocvattu, objTK.IdQdinh);
                                sp.Execute();
                                //Lấy đầu ra iTThuockho nếu thêm mới để update lại presdetail
                                id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);
                                #region Tách Id phiếu
                                //int numofRec = -1;
                                //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(PhieuXuatBnhanCt.SoLuong, PhieuXuatBnhanCt.IdThuockho, numofRec);
                                //sp.Execute();
                                //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                                //if (numofRec <= 0)
                                //{
                                //     sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                //                                      PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                //                                      PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                //                                      PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau,
                                //                                      PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.IdThuockho, PhieuXuatBnhanCt.NgayNhap,
                                //                                      PhieuXuatBnhanCt.GiaBhyt, PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen, phieuxuat.KieuThuocvattu, -1);
                                //    sp.Execute();
                                //    //Lấy đầu ra iTThuockho nếu thêm mới để update lại presdetail
                                //    id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);

                                //}
                                #endregion

                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();
                                //Xóa trong bảng biến động
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(LoaiPhieu.PhieuXuatKhoBenhNhan).Execute();
                                //Cập nhật laijiTThuockho mới cho chi tiết đơn thuốc
                                if (id_Thuockho_new != -1 ) //Gặp trường hợp khi xuất hết thuốc thì xóa kho-->Khi hủy thì tạo ra dòng thuốc kho mới-->02/03/2024 dùng thủ tục ThuocPhieunhapxuatHuyxacnhan phía trên
                                {
                                    if (id_Thuockho_new != iTThuockho_old)
                                    {
                                        //Cập nhật tất cả các bảng liên quan
                                        new Update(KcbDonthuocChitiet.Schema)
                                        .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();

                                        new Update(TBiendongThuoc.Schema)
                                        .Set(TBiendongThuoc.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(TBiendongThuoc.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();

                                        new Update(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                        .Set(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();

                                        new Update(TPhieuCapphatChitiet.Schema)
                                       .Set(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                       .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                       Execute();
                                    }

                                }
                                else
                                {
                                    err = Utility.sDbnull(PhieuXuatBnhanCt.IdThuockho, "0");
                                    return ActionResult.notExists;
                                }

                                KcbDonthuocChitiet objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                               .Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(PhieuXuatBnhanCt.IdChitietdonthuoc)
                               .ExecuteSingle<KcbDonthuocChitiet>();
                                if (objKcbDonthuocChitiet != null)
                                {
                                    ////TẠM REM CÁC DÒNG DƯỚI ĐỂ DÙNG THỦ TỤC XỬ LÝ
                                    //DateTime? _NgayXacnhan = objKcbDonthuocChitiet.NgayXacnhan;
                                    //decimal SOLUONG_LINH = objKcbDonthuocChitiet.SluongLinh.Value - PhieuXuatBnhanCt.SoLuong;
                                    //byte hasConfirm = (byte)(SOLUONG_LINH > 0 ? 1 : 0);
                                    //_NgayXacnhan = hasConfirm == 0 ? null : _NgayXacnhan;
                                    //new Update(KcbDonthuocChitiet.Schema)
                                    //.Set(KcbDonthuocChitiet.SluongLinhColumn).EqualTo(objKcbDonthuocChitiet.SluongLinh - PhieuXuatBnhanCt.SoLuong)
                                    //.Set(KcbDonthuocChitiet.TrangThaiColumn).EqualTo(hasConfirm)
                                    //.Set(KcbDonthuocChitiet.NgayXacnhanColumn).EqualTo(_NgayXacnhan)
                                    //.Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(PhieuXuatBnhanCt.IdChitietdonthuoc).Execute();
                                    SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objKcbDonthuocChitiet.IdChitietdonthuoc, objKcbDonthuocChitiet.SoLuong, 0, -1, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 1).Execute();

                                }
                            }
                            //Xóa trong bảng chi tiết đơn thuốc
                            new Delete().From(TXuatthuocTheodon.Schema).Where(TXuatthuocTheodon.IdPhieuXuatColumn).IsEqualTo(phieuxuat.IdPhieu)
                                   .Execute();
                            ////Xóa phiếu xuất bệnh nhân--Ko hiểu sao có đoạn code kiểm tra này
                            //SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhan.Schema)
                            //        .Where(TPhieuXuatthuocBenhnhan.Columns.IdPhieu).IsEqualTo(
                            //            phieuxuat.IdPhieu);
                            //if (sqlQuery.GetRecordCount() <= 0)//100% phải xảy ra nếu ko là lỗi
                            //{
                                TPhieuXuatthuocBenhnhan.Delete(phieuxuat.IdPhieu);
                            //}
                            //else
                            //    return ActionResult.Exceed;
                            //Kiểm tra các chi tiết chưa được xác nhận
                            SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                             .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(phieuxuat.IdDonthuoc)
                             .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                            int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                            new Update(KcbDonthuoc.Schema)
                                      .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                      .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                      .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)//(status)
                                      .Set(KcbDonthuoc.Columns.TthaiCapphat).EqualTo(0)
                                      .Set(KcbDonthuoc.Columns.NgayCapphat).EqualTo(null)
                                      .Set(KcbDonthuoc.Columns.NgayXacnhan).EqualTo(null)
                                      .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(phieuxuat.IdDonthuoc).Execute();

                            new Update(TPhieuCapphatChitiet.Schema)
                        .Set(TPhieuCapphatChitiet.Columns.DaLinh).EqualTo(0)
                        .Set(TPhieuCapphatChitiet.Columns.ThucLinh).EqualTo(0)
                        .Set(TPhieuCapphatChitiet.Columns.IdPhieuxuatthuocBenhnhan).EqualTo(-1)
                        .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(ID_CAPPHAT)
                        .And(TPhieuCapphatChitiet.Columns.IdPhieuxuatthuocBenhnhan).IsEqualTo(phieuxuat.IdPhieu)
                        .Execute();
                        }
                        if (!codulieu) return ActionResult.DataChanged;
                        //Cập nhật trạng thái cấp phát của phiếu đề nghị=0
                        new Update(TPhieuCapphatNoitru.Schema)
                            .Set(TPhieuCapphatNoitru.TrangThaiColumn.ColumnName).EqualTo(0)
                            .Set(TPhieuCapphatNoitru.IdNhanviencapphatColumn.ColumnName).EqualTo(null)
                            .Set(TPhieuCapphatNoitru.NguoiXacnhanColumn.ColumnName).EqualTo(null)
                            .Set(TPhieuCapphatNoitru.NgayXacnhanColumn.ColumnName).EqualTo(null)
                            .Where(TPhieuCapphatNoitru.IdCapphatColumn).IsEqualTo(ID_CAPPHAT).Execute();

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return ActionResult.Exception;
            }
        }

        private TPhieuXuatthuocBenhnhan CreatePhieuXuatBenhNhanNoitru(KcbDonthuoc objPrescription, short ID_KHO,int ID_CAPPHAT)
        {
            KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objPrescription.IdBenhnhan);
            KcbLuotkham objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPrescription.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPrescription.IdBenhnhan).ExecuteSingle<KcbLuotkham>();

            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = new TPhieuXuatthuocBenhnhan();
            objPhieuXuatBnhan.NgayXacnhan = DateTime.Now;
            objPhieuXuatBnhan.IdPhongChidinh = Utility.Int16Dbnull(objPrescription.IdPhongkham);
            objPhieuXuatBnhan.IdKhoaChidinh = Utility.Int16Dbnull(objPrescription.IdKhoadieutri);
            objPhieuXuatBnhan.IdBacsiKedon = Utility.Int16Dbnull(objPrescription.IdBacsiChidinh);
            objPhieuXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objPrescription.IdDonthuoc);
            objPhieuXuatBnhan.IdNhanvien = globalVariables.gv_intIDNhanvien;
            //objPhieuXuatBnhan.HienThi = 1;
            objPhieuXuatBnhan.IdCapphat = ID_CAPPHAT;
            objPhieuXuatBnhan.ChanDoan = Utility.sDbnull(objLuotkham.ChanDoan);
            objPhieuXuatBnhan.MabenhChinh = Utility.sDbnull(objLuotkham.MabenhChinh);
            objPhieuXuatBnhan.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
            objPhieuXuatBnhan.MaDoituongKcb = objLuotkham.MaDoituongKcb;
            objPhieuXuatBnhan.GioiTinh = objBenhnhan.GioiTinh;
            objPhieuXuatBnhan.TenBenhnhan = Utility.sDbnull(objBenhnhan.TenBenhnhan);
            objPhieuXuatBnhan.TenKhongdau = Utility.sDbnull(Utility.UnSignedCharacter(objBenhnhan.TenBenhnhan));
            objPhieuXuatBnhan.DiaChi = Utility.sDbnull(objBenhnhan.DiaChi);
            objPhieuXuatBnhan.NamSinh = Utility.Int32Dbnull(objBenhnhan.NamSinh);
            objPhieuXuatBnhan.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt);
            objPhieuXuatBnhan.NgayKedon = objPrescription.NgayKedon;
            objPhieuXuatBnhan.NgayTao = DateTime.Now;
            objPhieuXuatBnhan.NguoiTao = globalVariables.UserName;
            objPhieuXuatBnhan.Noitru = objPrescription.Noitru;
            objPhieuXuatBnhan.KieuThuocvattu = objPrescription.KieuThuocvattu;
            objPhieuXuatBnhan.IdKho = ID_KHO;
            objPhieuXuatBnhan.LoaiPhieu = (byte?)LoaiPhieu.PhieuXuatKhoBenhNhan;
            return objPhieuXuatBnhan;
        }
        ActionResult Kiemtrasoluongthuoctrongkho(KcbDonthuoc pres, int ID_KHO)
        {
            KcbDonthuocChitietCollection lstPresdetail = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(pres.IdDonthuoc).ExecuteAsCollection<KcbDonthuocChitietCollection>();
            foreach (KcbDonthuocChitiet _presDetail in lstPresdetail)
            {
                int id_thuoc = _presDetail.IdThuoc;
                DmucThuoc _drug = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.IdThuocColumn).IsEqualTo(id_thuoc).ExecuteSingle<DmucThuoc>();
                if (_drug == null) return ActionResult.UNKNOW;
                string Drug_name = _drug.TenThuoc;
                decimal so_luong = _presDetail.SoLuong;
                decimal SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho(_presDetail.IdDonthuoc, ID_KHO, id_thuoc, (int)_presDetail.IdThuockho.Value, 1, Utility.ByteDbnull(pres.Noitru, 0));
                if (SoLuongTon < so_luong)
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc,Vì thuốc :{0} số lượng tồn hiện tại trong kho({1}) không đủ cấp cho số lượng yêu cầu({2})\n Mời bạn xem lại số lượng", Drug_name, SoLuongTon.ToString(), so_luong.ToString()));
                    return ActionResult.NotEnoughDrugInStock;
                }
            }
            return ActionResult.Success;
        }
        ActionResult Kiemtrasoluongthuoctrongkho(TPhieuCapphatChitiet pres, int ID_KHO)
        {

            int id_thuoc = pres.IdThuoc;
            DmucThuoc _drug = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.IdThuocColumn).IsEqualTo(id_thuoc).ExecuteSingle<DmucThuoc>();
            if (_drug == null) return ActionResult.UNKNOW;
            string Drug_name = _drug.TenThuoc;
            decimal so_luong = pres.SoLuong;
            decimal SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho(pres.IdDonthuoc, ID_KHO, id_thuoc, (int)pres.IdThuockho.Value, 1, (byte)1);
            if (SoLuongTon < so_luong)
            {
                Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc,Vì thuốc :{0} số lượng tồn hiện tại trong kho({1}) không đủ cấp cho số lượng yêu cầu({2})\n Mời bạn xem lại số lượng", Drug_name, SoLuongTon.ToString(), so_luong.ToString()));
                return ActionResult.NotEnoughDrugInStock;
            }
            return ActionResult.Success;
        }
        public ActionResult ThemPhieuCapPhatNoiTru(TPhieuCapphatNoitru _phieucapphat,List< TPhieuCapphatChitiet> lstPhieuCapphatCt, List<TThuocCapphatChitiet> lstThuocCapphatCt)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {

                        _phieucapphat.IsNew = true;
                        _phieucapphat.Save();

                        if (_phieucapphat.IdCapphat > 0)
                        {
                            List<string> lstIddonthuoc = (from p in lstPhieuCapphatCt.AsEnumerable()
                                                          select Utility.sDbnull(p.IdDonthuoc, "-1")).ToList<string>();
                            foreach (var _PhieuCapphatCt in lstPhieuCapphatCt)
                            {
                                _PhieuCapphatCt.IdCapphat = _phieucapphat.IdCapphat;
                                _PhieuCapphatCt.IsNew = true;
                                _PhieuCapphatCt.Save();
                            }
                            foreach (var _ThuocCapphatCt in lstThuocCapphatCt)
                            {
                                _ThuocCapphatCt.IdCapphat = _phieucapphat.IdCapphat;
                                _ThuocCapphatCt.IsNew = true;
                                _ThuocCapphatCt.Save();
                                //Xóa trong bảng kê tạm và cập nhật id theo id phiếu cấp phát để lúc duyệt và hủy tăng tốc độ xử lý. Bỏ phần này thì cần mở các phần ở duyệt phiếu với phần xóa t_tamke theo đơn chi tiết và khóa xóa t_tamke theo id cấp phát

                            }
                            SPs.ThuocCapnhatthongtincapphatDonthuocChitiet(string.Join(",", lstIddonthuoc.ToArray<string>()), _phieucapphat.IdCapphat, 0).Execute();

                        }
                       
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
        }

    }
}
