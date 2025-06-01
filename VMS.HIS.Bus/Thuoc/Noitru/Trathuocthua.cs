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

    public class Trathuocthua
    {
        private NLog.Logger log;
        public Trathuocthua()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }

        public static bool ThuocNoitruKiemtraThuoctralai(long id_capphat, long id_donthuoc)
        {
            return SPs.ThuocNoitruKiemtraThuoctralai(id_capphat, id_donthuoc).GetDataSet().Tables[0].Rows.Count > 0;
        }
        public ActionResult CappnhatPhieuTrathuocthua(TPhieutrathuocthua _phieutra, List<long> lstIdCt)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {

                        new Update(TPhieutrathuocthua.Schema).Set(TPhieutrathuocthua.NgayLapphieuColumn).EqualTo(_phieutra.NgayLapphieu.Date)
                            .Set(TPhieutrathuocthua.NguoiLapphieuColumn).EqualTo(_phieutra.NguoiLapphieu)
                            .Set(TPhieutrathuocthua.IdKhoatraColumn).EqualTo(_phieutra.IdKhoatra)
                            .Set(TPhieutrathuocthua.IdKhonhanColumn).EqualTo(_phieutra.IdKhonhan)
                             .Set(TPhieutrathuocthua.NgaySuaColumn).EqualTo(_phieutra.NgaySua.Value)
                            .Set(TPhieutrathuocthua.NguoiSuaColumn).EqualTo(_phieutra.NguoiSua)
                            .Where(TPhieutrathuocthua.IdColumn).IsEqualTo(_phieutra.Id).Execute();

                        new Update(TPhieuCapphatChitiet.Schema)
                           .Set(TPhieuCapphatChitiet.Columns.IdPhieutralai).EqualTo(-1)
                            .Set(TPhieuCapphatChitiet.Columns.TrangthaiTralai).EqualTo(0)
                           .Where(TPhieuCapphatChitiet.Columns.IdPhieutralai).IsEqualTo(_phieutra.Id)
                           .Execute();

                        new Update(TPhieuCapphatChitiet.Schema)
                            .Set(TPhieuCapphatChitiet.Columns.IdPhieutralai).EqualTo(_phieutra.Id)
                             .Set(TPhieuCapphatChitiet.Columns.TrangthaiTralai).EqualTo(1)
                            .Where(TPhieuCapphatChitiet.Columns.IdChitiet).In(lstIdCt)
                            .Execute();

                        Scope.Complete();
                    }
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi cập nhật phiếu trả thuốc thừa", ex);
                return ActionResult.Error;
            }
        }

        public ActionResult ThemPhieutrathuocthua(TPhieutrathuocthua _phieutra, List<long> lstIdCt)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        _phieutra.IsNew = true;
                        _phieutra.Save();
                        if (_phieutra.Id > 0)
                        {
                            new Update(TPhieuCapphatChitiet.Schema)
                            .Set(TPhieuCapphatChitiet.Columns.IdPhieutralai).EqualTo(_phieutra.Id)
                            .Set(TPhieuCapphatChitiet.Columns.TrangthaiTralai).EqualTo(1)
                            .Where(TPhieuCapphatChitiet.Columns.IdChitiet).In(lstIdCt)
                            .Execute();
                        }

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi thêm phiếu trả thuốc thừa", ex);
                return ActionResult.Error;
            }
        }
        public ActionResult XoaPhieuTrathuocthua(int idphieu)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(TPhieutrathuocthua.Schema).Where(TPhieutrathuocthua.Columns.Id).IsEqualTo(idphieu).Execute();
                        new Update(TPhieuCapphatChitiet.Schema)
                          .Set(TPhieuCapphatChitiet.Columns.IdPhieutralai).EqualTo(-1)
                          .Set(TPhieuCapphatChitiet.Columns.TrangthaiTralai).EqualTo(0)
                          .Where(TPhieuCapphatChitiet.Columns.IdPhieutralai).IsEqualTo(idphieu)
                          .Execute();
                        Scope.Complete();
                    }
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xóa phiếu trả thuốc thừa", ex);
                return ActionResult.Error;
            }
        }
        public ActionResult Xacnhanphieutrathuocthua(   TPhieutrathuocthua _phieutrathuocthua)
        {
            ActionResult _result=ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {

                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        long idphieu = _phieutrathuocthua.Id;
                        short ID_KHO =_phieutrathuocthua.IdKhonhan;
                        DateTime ngaytra=_phieutrathuocthua.NgayTra.Value;
                        int idnguoitra = _phieutrathuocthua.NguoiTra.Value;
                        
                        TPhieuCapphatChitietCollection lstChitiet = new Select().From(TPhieuCapphatChitiet.Schema)
                            .Where(TPhieuCapphatChitiet.Columns.IdPhieutralai).IsEqualTo(idphieu).ExecuteAsCollection<TPhieuCapphatChitietCollection>();
                        
                        bool codulieu = false;
                        //Xác nhận từng đơn thuốc nội trú
                        foreach (TPhieuCapphatChitiet _item in lstChitiet)
                        {
                            codulieu = true;
                            KcbDonthuocChitiet objDetail = new Select().From(KcbDonthuocChitiet.Schema)
                       .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(_item.IdChitietdonthuoc)
                       .ExecuteSingle<KcbDonthuocChitiet>();
                            TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc)
                                .ExecuteSingle<TPhieuXuatthuocBenhnhanChitiet>();

                            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = TPhieuXuatthuocBenhnhan.FetchByID(PhieuXuatBnhanCt.IdPhieu);

                            if (objDetail == null) return ActionResult.Exceed;

                            //Cộng vào kho nhận
                            long id_Thuockho_new = -1;
                            long iTThuockho_old = PhieuXuatBnhanCt.IdThuockho.Value;
                            TThuockho objTK = TThuockho.FetchByID(iTThuockho_old);
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                                              _item.SoLuongtralai, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                              PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                                              PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau,
                                                              PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.IdThuockho, PhieuXuatBnhanCt.NgayNhap,
                                                              PhieuXuatBnhanCt.GiaBhyt, PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen, _phieutrathuocthua.KieuThuocVt, objTK.IdQdinh);
                            sp.Execute();
                            
                            id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);
                            //Cập nhật lại ID Thuốc kho(Có thể xóa mất dòng số lượng =0 nên khi nhập kho tạo ra dòng mới)
                            if (id_Thuockho_new != -1) //Gặp trường hợp khi xuất hết thuốc thì xóa kho-->Khi hủy thì tạo ra dòng thuốc kho mới
                            {
                                if (id_Thuockho_new != iTThuockho_old)
                                {
                                    objDetail.IdThuockho = id_Thuockho_new;
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
                            //Cập nhật số lượng thực lĩnh theo Id chi tiết
                            //objDetail.SluongLinh = _item.ThucLinh;
                            //objDetail.MarkOld();
                            //objDetail.IsNew = false;
                            //objDetail.Save();
                            //Cập nhật trạng thái trả lại
                            _item.TrangthaiTralai = 2;
                            _item.IsNew = false;
                            _item.MarkOld();
                            _item.Save();
                            
                            //Insert bảng biến động liên quan đến kho nhập
                            TBiendongThuoc objNhapXuat = new TBiendongThuoc();
                            objNhapXuat.NgayHethan = objDetail.NgayHethan;
                            objNhapXuat.IdThuockho = objDetail.IdThuockho;
                            objNhapXuat.SoLo = objDetail.SoLo;
                            objNhapXuat.SoDky = objDetail.SoDky;
                            objNhapXuat.SoQdinhthau = objDetail.SoQdinhthau;
                            objNhapXuat.IdQdinh = objTK.IdQdinh;
                            objNhapXuat.MaNhacungcap = objDetail.MaNhacungcap;
                            objNhapXuat.Vat = (int)objDetail.Vat;
                            objNhapXuat.QuayThuoc = objPhieuXuatBnhan.QuayThuoc;
                            objNhapXuat.MaPhieu = Utility.sDbnull(_phieutrathuocthua.MaPhieu);
                            objNhapXuat.Noitru = objPhieuXuatBnhan.Noitru;
                            objNhapXuat.NgayHoadon = ngaytra;
                            objNhapXuat.NgayBiendong = ngaytra;
                            objNhapXuat.NgayTao = DateTime.Now;
                            objNhapXuat.NguoiTao = globalVariables.UserName;
                            objNhapXuat.SoLuong = Utility.DecimaltoDbnull(_item.SoLuongtralai);
                            objNhapXuat.DonGia = Utility.DecimaltoDbnull(PhieuXuatBnhanCt.DonGia);
                            objNhapXuat.GiaBan = Utility.DecimaltoDbnull(PhieuXuatBnhanCt.GiaBan);
                            objNhapXuat.GiaNhap = Utility.DecimaltoDbnull(PhieuXuatBnhanCt.GiaNhap);
                            objNhapXuat.PhuThu = objDetail.PhuThu;
                            objNhapXuat.SoHoadon = "-1";
                            objNhapXuat.IdThuoc = Utility.Int32Dbnull(PhieuXuatBnhanCt.IdThuoc);
                            objNhapXuat.IdPhieu =(int) _phieutrathuocthua.Id;
                            objNhapXuat.IdPhieuChitiet = (int)_item.IdChitiet;
                            objNhapXuat.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objNhapXuat.NgayNhap =ngaytra;
                            objNhapXuat.GiaPhuthuTraituyen = objDetail.PhuthuTraituyen;
                            objNhapXuat.GiaPhuthuDungtuyen = objDetail.PhuthuDungtuyen;
                            objNhapXuat.MaNhacungcap = PhieuXuatBnhanCt.MaNhacungcap;
                            objNhapXuat.IdKho = _phieutrathuocthua.IdKhonhan;
                            objNhapXuat.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.Phieutrathuocthua);
                            objNhapXuat.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.Phieutrathuocthua);
                            objNhapXuat.IdKhoaLinh = _phieutrathuocthua.IdKhoatra;
                            objNhapXuat.KieuThuocvattu = _phieutrathuocthua.KieuThuocVt;
                            objNhapXuat.ThanhTien = Utility.DecimaltoDbnull(PhieuXuatBnhanCt.DonGia) *
                                                    Utility.Int32Dbnull(_item.SoLuongtralai);
                            objNhapXuat.MotaThem = "Phiếu trả thuốc thừa";
                            objNhapXuat.IsNew = true;
                            objNhapXuat.Save();
                            
                        }
                        if (!codulieu) return ActionResult.DataChanged;
                        //Cập nhật trạng thái cấp phát của phiếu đề nghị=1
                        new Update(TPhieutrathuocthua.Schema)
                            .Set(TPhieutrathuocthua.TrangThaiColumn.ColumnName).EqualTo(1)
                            .Set(TPhieutrathuocthua.NgayTraColumn.ColumnName).EqualTo(ngaytra)
                            .Set(TPhieutrathuocthua.NguoiNhanColumn.ColumnName).EqualTo(globalVariablesPrivate.objNhanvien != null ? globalVariablesPrivate.objNhanvien.IdNhanvien : globalVariables.gv_intIDNhanvien)
                            .Set(TPhieutrathuocthua.NguoiTraColumn.ColumnName).EqualTo(idnguoitra)
                            .Set(TPhieutrathuocthua.NgaySuaColumn.ColumnName).EqualTo(DateTime.Now)
                            .Set(TPhieutrathuocthua.NguoiSuaColumn.ColumnName).EqualTo(globalVariables.UserName)
                            .Where(TPhieutrathuocthua.IdColumn).IsEqualTo(idphieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xác nhận phiếu trả thuốc thừa", ex);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacnhanphieuphieutrathuocthua(TPhieutrathuocthua _phieutrathuocthua, ref string errMsg)
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        bool codulieu = false;
                       
                        long idphieu=_phieutrathuocthua.Id;
                        short ID_KHO = _phieutrathuocthua.IdKhonhan;
                        TPhieuCapphatChitietCollection lstChitiet = new Select().From(TPhieuCapphatChitiet.Schema)
                            .Where(TPhieuCapphatChitiet.Columns.IdPhieutralai).IsEqualTo(idphieu).ExecuteAsCollection<TPhieuCapphatChitietCollection>();
                        ActionResult _Kiemtrathuochuyxacnhan = Kiemtratonthuoc(lstChitiet, ID_KHO);
                        if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;
                        foreach (TPhieuCapphatChitiet _item in lstChitiet)
                        {
                            codulieu = true;
                            KcbDonthuocChitiet objDetail = new Select().From(KcbDonthuocChitiet.Schema)
                       .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(_item.IdChitietdonthuoc)
                       .ExecuteSingle<KcbDonthuocChitiet>();
                            TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc)
                                .ExecuteSingle<TPhieuXuatthuocBenhnhanChitiet>();

                            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = TPhieuXuatthuocBenhnhan.FetchByID(PhieuXuatBnhanCt.IdPhieu);

                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(_phieutrathuocthua.IdKhonhan),
                                                                         Utility.Int32Dbnull(_item.IdThuoc, -1),
                                                                         objDetail.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                         Utility.DecimaltoDbnull(objDetail.Vat), _item.SoLuongtralai, objDetail.IdThuockho, objDetail.MaNhacungcap, objDetail.SoLo, PropertyLib._HisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errMsg);

                            sp.Execute();
                            //Cập nhật trạng thái trả lại
                            _item.TrangthaiTralai = 1;
                            _item.IsNew = false;
                            _item.MarkOld();
                            _item.Save();

                        }
                        //Xóa trong bảng biến động
                        new Delete().From(TBiendongThuoc.Schema).Where(TBiendongThuoc.Columns.IdPhieu).IsEqualTo(_phieutrathuocthua.Id)
                            .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(LoaiPhieu.Phieutrathuocthua)
                            .Execute();
                        if (!codulieu) return ActionResult.DataChanged;
                        //Cập nhật trạng thái phiếu trả thuốc về 0//Chưa trả
                        new Update(TPhieutrathuocthua.Schema)
                           .Set(TPhieutrathuocthua.TrangThaiColumn.ColumnName).EqualTo(0)
                           .Set(TPhieutrathuocthua.NgayTraColumn.ColumnName).EqualTo(null)
                           .Set(TPhieutrathuocthua.NguoiNhanColumn.ColumnName).EqualTo(-1)
                           .Set(TPhieutrathuocthua.NguoiTraColumn.ColumnName).EqualTo(-1)
                           .Set(TPhieutrathuocthua.NgaySuaColumn.ColumnName).EqualTo(null)
                           .Set(TPhieutrathuocthua.NguoiSuaColumn.ColumnName).EqualTo("")
                           .Where(TPhieutrathuocthua.IdColumn).IsEqualTo(idphieu).Execute();

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu trả thuốc thừa", ex);
                errMsg = ex.Message;
                return ActionResult.Exception;
            }
        }
        public ActionResult Kiemtratonthuoc(TPhieuCapphatChitietCollection lstChitiet, short ID_KHO)
        {
            ActionResult _result = ActionResult.Success;
            try
            {
                foreach (TPhieuCapphatChitiet pres in lstChitiet)
                {
                    if (pres.DaLinh == 1)//Chưa được lĩnh mới kiểm tra
                    {
                        _result = Kiemtrasoluongthuoctrongkho(pres, ID_KHO);
                        if (_result != ActionResult.Success) return _result;
                    }
                }
                return ActionResult.Success;
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
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

       

    }
}
