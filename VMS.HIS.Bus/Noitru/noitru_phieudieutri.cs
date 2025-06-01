using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using VNS.Properties;
using System.Collections.Generic;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_phieudieutri
    {
        private NLog.Logger log;
        public noitru_phieudieutri()
        {
            log = LogManager.GetCurrentClassLogger();
        }
       

        public ActionResult SaoChepDonThuocTheoPhieuDieuTriFullTransaction(KcbDonthuoc objDonthuoc, NoitruPhieudieutri objTreatment,KcbDonthuocChitiet[] arrChitietdonthuoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        objDonthuoc.IdPhieudieutri = objTreatment.IdPhieudieutri;
                        objDonthuoc.IdDonthuocthaythe = -1;
                        objDonthuoc.IdKham = objTreatment.IdPhieudieutri;
                        objDonthuoc.IdBacsiChidinh = objTreatment.IdBacsi;
                        objDonthuoc.NgaySua = null;
                        objDonthuoc.NguoiSua = null;
                        objDonthuoc.NgayKedon = Convert.ToDateTime(objTreatment.NgayDieutri);
                        objDonthuoc.Noitru = 1;
                        NoitruPhanbuonggiuong objPatientDept = NoitruPhanbuonggiuong.FetchByID(objTreatment.IdBuongGiuong);
                        if (objPatientDept != null)
                        {
                            objDonthuoc.IdKhoadieutri = Utility.Int16Dbnull(objPatientDept.IdKhoanoitru);
                            objDonthuoc.IdBuongNoitru = Utility.Int16Dbnull(objPatientDept.IdBuong);
                            objDonthuoc.IdGiuongNoitru = Utility.Int16Dbnull(objPatientDept.IdGiuong);
                        }
                        objDonthuoc.NgayXacnhan = null;
                        objDonthuoc.NgayCapphat = null;
                        objDonthuoc.TrangThai = 0;
                        objDonthuoc.TrangthaiThanhtoan = 0;
                        objDonthuoc.KieuDonthuoc = 0;
                        objDonthuoc.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                        //objDonthuoc.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                        objDonthuoc.MotaThem = "Sao chép";
                        objDonthuoc.NguoiTao = globalVariables.UserName;
                        objDonthuoc.NgayTao = DateTime.Now;
                        objDonthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                        objDonthuoc.TenMaytao = globalVariables.gv_strComputerName;
                        objDonthuoc.IsNew = true;
                        objDonthuoc.Save();
                        foreach (var objChitietdonthuoc in arrChitietdonthuoc)
                        {
                            KcbDonthuocChitiet newItem = KcbDonthuocChitiet.FetchByID(objChitietdonthuoc.IdChitietdonthuoc);
                            newItem.IdKham = objTreatment.IdPhieudieutri;

                            newItem.SoluongHuy = 0;
                            newItem.NgayHuy = null;
                            newItem.TrangthaiHuy = 0;
                            newItem.NguoiHuy = null;
                            newItem.TrangThai = 0;
                            newItem.SluongLinh = 0;
                            newItem.SluongSua = 0;
                            newItem.NgayXacnhan = null;
                            newItem.IdThanhtoan = -1;
                            newItem.TrangthaiThanhtoan = 0;
                            newItem.TrangthaiTonghop = 0;
                            newItem.NgayThanhtoan = null;
                            newItem.TrangthaiChuyen = 0;

                            newItem.NgaySua = null;
                            newItem.NguoiSua = null;
                            newItem.TileChietkhau = 0;
                            newItem.TienChietkhau = 0;
                            newItem.IdGoi = -1;
                            newItem.TrongGoi = 0;




                            newItem.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);

                            newItem.NguoiTao = globalVariables.UserName;
                            newItem.NgayTao = DateTime.Now;
                            newItem.IpMaytao = globalVariables.gv_strIPAddress;
                            newItem.TenMaytao = globalVariables.gv_strComputerName;

                            newItem.IsNew = true;
                            newItem.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult SaoChepDonThuocTheoPhieuDieuTri(string GUID, KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, NoitruPhieudieutri objTreatment, KcbDonthuocChitiet[] arrChitietdonthuoc, List<string> lstError)
        {
            try
            {
                lstError.Clear();
                using (var scope = new TransactionScope())
                {

                    objDonthuoc.IdPhieudieutri = objTreatment.IdPhieudieutri;
                    objDonthuoc.IdDonthuocthaythe = -1;
                    objDonthuoc.IdKham = -1;
                    objDonthuoc.IdBacsiChidinh = objTreatment.IdBacsi;
                    objDonthuoc.NgaySua = null;
                    objDonthuoc.NguoiSua = null;
                    objDonthuoc.NgayKedon = Convert.ToDateTime(objTreatment.NgayDieutri);
                    objDonthuoc.Noitru = 1;
                    NoitruPhanbuonggiuong objPatientDept = NoitruPhanbuonggiuong.FetchByID(objTreatment.IdBuongGiuong);
                    if (objPatientDept != null)
                    {
                        objDonthuoc.IdKhoadieutri = Utility.Int16Dbnull(objPatientDept.IdKhoanoitru);
                        objDonthuoc.IdBuongNoitru = Utility.Int16Dbnull(objPatientDept.IdBuong);
                        objDonthuoc.IdGiuongNoitru = Utility.Int16Dbnull(objPatientDept.IdGiuong);
                    }
                    objDonthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objTreatment.MaLuotkham,
                                                                                        Utility.Int32Dbnull(
                                                                                            objTreatment.IdBenhnhan,
                                                                                            -1));
                    objDonthuoc.NgayXacnhan = null;
                    objDonthuoc.NgayCapphat = null;
                    objDonthuoc.TrangThai = 0;
                    objDonthuoc.TrangthaiThanhtoan = 0;
                    objDonthuoc.KieuDonthuoc = 0;
                    objDonthuoc.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                    //objDonthuoc.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                    objDonthuoc.MotaThem = "Sao chép";
                    objDonthuoc.NguoiTao = globalVariables.UserName;
                    objDonthuoc.NgayTao = DateTime.Now;
                    objDonthuoc.IpMaytao = globalVariables.gv_strIPAddress;
                    objDonthuoc.TenMaytao = globalVariables.gv_strComputerName;
                    objDonthuoc.IsNew = true;
                   
                    var sp = SPs.SpKcbThemmoiDonthuoc(objDonthuoc.IdDonthuoc, objDonthuoc.IdPhieudieutri, objDonthuoc.IdKhoadieutri, objDonthuoc.IdDonthuocthaythe, objDonthuoc.IdKham
                        , objDonthuoc.IdBenhnhan, objDonthuoc.MaLuotkham, objDonthuoc.NgayKedon, objDonthuoc.IdBacsiChidinh, objDonthuoc.TrangThai, objDonthuoc.TthaiTonghop
                        , objDonthuoc.TrangthaiThanhtoan, objDonthuoc.NgayThanhtoan, objDonthuoc.IdGoi, objDonthuoc.TrongGoi, objDonthuoc.NguoiTao, objDonthuoc.NgayTao
                        , objDonthuoc.MotaThem, objDonthuoc.TenDonthuoc, objDonthuoc.MaDoituongKcb, objDonthuoc.Noitru, objDonthuoc.KieuDonthuoc, objDonthuoc.IdPhongkham
                        , objDonthuoc.IdBuongGiuong, objDonthuoc.IdBuongNoitru, objDonthuoc.IdGiuongNoitru, objDonthuoc.LoidanBacsi, objDonthuoc.NgayTaikham
                        , objDonthuoc.TaiKham, objDonthuoc.MaKhoaThuchien, objDonthuoc.NgayCapphat, objDonthuoc.KieuThuocvattu, objDonthuoc.NgayChot
                        , objDonthuoc.IdChot, objDonthuoc.NgayHuychot, objDonthuoc.NguoiHuychot, objDonthuoc.LydoHuychot, objDonthuoc.NgayHuyxacnhan, objDonthuoc.NguoiHuyxacnhan
                        , objDonthuoc.LydoHuyxacnhan, objDonthuoc.NgayXacnhan, objDonthuoc.NguoiXacnhan, objDonthuoc.IdLichsuDoituongKcb, objDonthuoc.MatheBhyt
                        , objDonthuoc.IpMaytao, objDonthuoc.TenMaytao, objDonthuoc.LastActionName, objDonthuoc.Donthuoctaiquay, objDonthuoc.SongayNhaton, objDonthuoc.ChanDoan, objDonthuoc.IdChitietchidinh, objDonthuoc.MaCoso);
                    sp.Execute();
                    objDonthuoc.IdDonthuoc = Utility.Int64Dbnull(sp.OutputValues[0]);

                    //objDonthuoc.Save();
                    foreach (var objChitietdonthuoc in arrChitietdonthuoc)
                    {
                        KcbDonthuocChitiet newItem = KcbDonthuocChitiet.FetchByID(objChitietdonthuoc.IdChitietdonthuoc);
                        newItem.IdKham = objTreatment.IdPhieudieutri;

                        newItem.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                        newItem.IdKham = -1;
                        newItem.TrangThai = 0;
                        newItem.TrangthaiTonghop = 0;
                        newItem.TrangthaiThanhtoan = 0;
                        newItem.TrangthaiChuyen = 0;
                        newItem.IdGoi = 0;
                        newItem.TrongGoi = 0;
                        newItem.SoluongHuy = 0;
                        newItem.TrangthaiHuy = 0;
                        newItem.NgayThanhtoan = null;
                        newItem.NguonThanhtoan = 1;
                        newItem.IdThanhtoan = -1;
                        newItem.SoluongHuy = 0;
                        newItem.NgayHuy = null;
                        newItem.TrangthaiHuy = 0;
                        newItem.NguoiHuy = null;
                        newItem.NgayXacnhan = null;
                        newItem.SoLuong = Utility.DecimaltoDbnull(newItem.SluongSua, -1) <= 0
                            ? newItem.SoLuong
                            : newItem.SluongSua.Value;
                        newItem.SluongSua = 0;
                        newItem.SluongLinh = 0;
                        newItem.NgaySua = null;
                        newItem.NguoiSua = null;
                        
                        newItem.NguoiTao = globalVariables.UserName;
                        newItem.NgayTao = DateTime.Now;
                        newItem.IpMaytao = globalVariables.gv_strIPAddress;
                        newItem.TenMaytao = globalVariables.gv_strComputerName;

                        newItem.IsNew = true;
                        //Kiểm tra số lượng tồn kho
                        decimal num = CommonLoadDuoc.SoLuongTonTrongKho(-1L,Utility.Int32Dbnull( newItem.IdKho,-1), newItem.IdThuoc, -1l,1, (byte)1);
                        log.Trace("1. Lay xong so luong ton kho ke don");

                        decimal sl=newItem.SoLuong;
                        if (sl > num)
                            {
                                string error = string.Format("Thuốc {0} chỉ còn số lượng {1} nên không đủ sao chép theo số lượng {2}. Vui lòng báo kho dược cấp thêm và dùng chức năng sửa đơn thuốc để kê lại thuốc này", newItem.IdThuoc,num, sl);
                                lstError.Add(error);
                                break;
                            }
                       
                       
                        DataTable listdata =
                            new XuatThuoc().GetObjThuocKhoCollection(
                                Utility.Int32Dbnull(newItem.IdKho, 0),
                                Utility.Int32Dbnull(newItem.IdThuoc, -1),
                                -1,
                                sl,
                                Utility.ByteDbnull(objLuotkham.IdLoaidoituongKcb.Value, 0),
                                Utility.ByteDbnull(objLuotkham.DungTuyen.Value, 0), (byte)1);

                        decimal soluongke = Utility.DecimaltoDbnull(sl, 0);
                        foreach (DataRow thuockho in listdata.Rows)
                        {
                            decimal soluong = Utility.DecimaltoDbnull(thuockho[TThuockho.Columns.SoLuong], 0);
                            long IdThuockho = Utility.Int64Dbnull(thuockho[TThuockho.Columns.IdThuockho], 0);
                            newItem.IsNew = true;
                            if (soluong > 0)
                            {

                                newItem.SoLuong = soluong;
                                newItem.IdThuockho = IdThuockho;
                                newItem.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                var spchitiet = SPs.SpKcbThemmoiChitietDonthuoc(newItem.IdChitietdonthuoc, newItem.IdDonthuoc, newItem.IdDonthuocChuyengoi
                                    , newItem.IdBenhnhan, newItem.MaLuotkham, newItem.IdKham, newItem.IdKho, newItem.IdThuoc, newItem.NgayHethan
                                    , newItem.SoLuong, newItem.SluongSua, newItem.SluongLinh, newItem.Sang, newItem.Trua, newItem.Chieu, newItem.Toi, newItem.DonGia, newItem.IdThuockho
                                    , newItem.NgayNhap, newItem.GiaNhap, newItem.GiaBan, newItem.GiaBhyt, newItem.SoLo, newItem.Vat
                                    , newItem.MaNhacungcap, newItem.PhuThu, newItem.PhuthuDungtuyen, newItem.PhuthuTraituyen, newItem.MotaThem
                                    , newItem.SoluongHuy, newItem.TrangthaiHuy, newItem.NguoiHuy, newItem.NgayHuy, newItem.TuTuc, newItem.TrangThai
                                    , newItem.TrangthaiTonghop, newItem.NgayXacnhan, newItem.TrangthaiBhyt, newItem.SttIn, newItem.MadoituongGia
                                    , newItem.PtramBhytGoc, newItem.PtramBhyt, newItem.BhytChitra, newItem.BnhanChitra, newItem.MaDoituongKcb
                                    , newItem.IdThanhtoan, newItem.TrangthaiThanhtoan, newItem.NgayThanhtoan, newItem.CachDung
                                    , newItem.ChidanThem, newItem.DonviTinh, newItem.SolanDung, newItem.SoluongDung, newItem.TrangthaiChuyen
                                    , newItem.NgayTao, newItem.NguoiTao, newItem.TileChietkhau, newItem.TienChietkhau, newItem.KieuChietkhau
                                    , newItem.IdGoi, newItem.TrongGoi, newItem.KieuBiendong, newItem.NguonThanhtoan, newItem.IpMaytao
                                    , newItem.TenMaytao, newItem.DaDung, newItem.LydoTiemchung, newItem.NguoiTiem, newItem.VitriTiem
                                    , newItem.MuiThu, newItem.NgayhenMuiketiep, newItem.PhanungSautiem, newItem.Xutri, newItem.KetluanNguyennhan
                                    , newItem.KetQua, newItem.NgaySudung, newItem.SoDky, newItem.SoQdinhthau, objDonthuoc.NgayKedon
                                    , newItem.IdThe, newItem.TyleTt, newItem.IdDangky, newItem.BhytNguonKhac, newItem.BhytGiaTyle, newItem.BnTtt, newItem.BnCct);
                                spchitiet.Execute();
                                newItem.IdChitietdonthuoc = Utility.Int64Dbnull(spchitiet.OutputValues[0]);
                                //Dùng bảng tạm kê để lưu trữ
                                THU_VIEN_CHUNG.UpdateKeTam(newItem.IdChitietdonthuoc, newItem.IdDonthuoc, GUID,"", IdThuockho, newItem.IdThuoc, Utility.Int16Dbnull(newItem.IdKho), Utility.DecimaltoDbnull(soluong), (byte)LoaiTamKe.KEDONTHUOC,
                                       objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1, THU_VIEN_CHUNG.GetSysDateTime(), "Sao chép thuốc");
                            }
                            if (soluong > Utility.DecimaltoDbnull(soluongke))
                            {
                                break;
                            }
                            else
                            {
                                soluongke = soluongke - soluong;

                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult ThemPhieudieutri(NoitruPhieudieutri objTreatment)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (objTreatment.IdPhieudieutri<=0)
                        {
                            objTreatment.NgaySua = null;
                            objTreatment.NguoiSua = string.Empty;
                            objTreatment.IsNew = true;
                            objTreatment.Save();
                        }
                        else
                        {
                            
                            objTreatment.MarkOld();
                            objTreatment.IsNew = false;
                            objTreatment.IsLoaded = true;
                            objTreatment.Save();
                            new Update(KcbChidinhcl.Schema)
                                .Set(KcbChidinhcl.Columns.NgayChidinh).EqualTo(objTreatment.NgayDieutri)
                                .Where(KcbChidinhcl.Columns.IdDieutri).IsEqualTo(objTreatment.IdPhieudieutri).Execute();
                            new Update(KcbDonthuoc.Schema)
                                .Set(KcbDonthuoc.Columns.NgayKedon).EqualTo(objTreatment.NgayDieutri)
                                .Where(KcbDonthuoc.Columns.IdPhieudieutri).IsEqualTo(objTreatment.IdPhieudieutri).Execute();
                        }
                        new Update(KcbLuotkham.Schema)
                               .Set(KcbLuotkham.Columns.TrangthaiNoitru).EqualTo(2)
                               .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objTreatment.IdBenhnhan)
                               .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objTreatment.MaLuotkham).Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh tao phieu dieu tri: {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public DataSet NoitruLaythongtinphieudieutriIn(string lstIdPhieudieutri)
        {
            return SPs.NoitruLaythongtinphieudieutriIn(lstIdPhieudieutri).GetDataSet();
        }
        public DataTable NoitruLaydulieuClsThuocVtthSaochep(int IdPhieudieutri, int idbenhnhan, string maluotkham)
        {
            return SPs.NoitruLaydulieuclsThuocVtthSaochep(IdPhieudieutri, maluotkham,idbenhnhan ).GetDataSet().Tables[0];
        }
        public ActionResult ChuyentoanboVTTHvaogoi(long IdDonthuoc,int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbDonthuoc.Schema)
                            .Set(KcbDonthuoc.Columns.IdGoi).EqualTo(id_goi)
                            .Set(KcbDonthuoc.Columns.TrongGoi).EqualTo(1)
                            .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();
                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTHvaogoi(long IdDonthuoc,List<long>lstchitiet, int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        //Tạo phiếu mới
                        KcbDonthuoc objnew = KcbDonthuoc.FetchByID(IdDonthuoc);
                        objnew.IsNew = true;
                        objnew.IdGoi = id_goi;
                        objnew.Noitru = 1;
                        objnew.TrongGoi = 1;
                        objnew.Save();
                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objnew.IdDonthuoc)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(IdDonthuoc)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTHvaogoi(long IdDonthuoc_source,long IdDonthuoc_des, List<long> lstchitiet, int id_goi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(id_goi)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(IdDonthuoc_des)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(IdDonthuoc_source)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(1)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenVTTH(List<long> lstchitiet, byte tronggoi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(tronggoi)
                           .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstchitiet).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyentoanboVTTHrakhoigoi(long IdDonthuoc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbDonthuoc.Schema)
                           .Set(KcbDonthuoc.Columns.IdGoi).EqualTo(-1)
                           .Set(KcbDonthuoc.Columns.TrongGoi).EqualTo(0)
                           .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();

                        new Update(KcbDonthuocChitiet.Schema)
                           .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                           .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                           .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                           .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(IdDonthuoc).Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult ChuyenVTTHrakhoigoi(long IdDonthuoc_source,List<long> lstId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        
                        foreach (long _id in lstId)
                        {
                            KcbDonthuoc objKcbDonthuoc = KcbDonthuoc.FetchByID(_id);
                            if (objKcbDonthuoc == null)
                            {
                                objKcbDonthuoc = KcbDonthuoc.FetchByID(IdDonthuoc_source);
                                objKcbDonthuoc.IdGoi = -1;
                                objKcbDonthuoc.TrongGoi = 0;
                                objKcbDonthuoc.IsNew = true;
                                objKcbDonthuoc.Save();
                                new Update(KcbDonthuocChitiet.Schema)
                                   .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                                   .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objKcbDonthuoc.IdDonthuoc)
                                   .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                                   .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                   .Where(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).IsEqualTo(_id).Execute();
                            }
                            else
                            {
                                new Update(KcbDonthuocChitiet.Schema)
                                  .Set(KcbDonthuocChitiet.Columns.IdGoi).EqualTo(-1)
                                  .Set(KcbDonthuocChitiet.Columns.IdDonthuoc).EqualTo(objKcbDonthuoc.IdDonthuoc)
                                  .Set(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).EqualTo(-1)
                                  .Set(KcbDonthuocChitiet.Columns.TrongGoi).EqualTo(0)
                                  .Where(KcbDonthuocChitiet.Columns.IdDonthuocChuyengoi).IsEqualTo(_id).Execute();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }

            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult Xoaphieudieutri(System.Collections.Generic.List<int> lstIdPhieudieutri)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        foreach (int IdPhieudieutri in lstIdPhieudieutri)
                        {
                            SPs.NoitruXoaphieudieutri(IdPhieudieutri).Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh xoa phieu dieu tri {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public  ActionResult SaochepDonthuoc(int CurrentTreatID, KcbLuotkham objLuotkham,
         KcbDonthuocCollection lstPres, DateTime pres_date)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        foreach (KcbDonthuoc pre in lstPres)
                        {
                            long oldPreID = pre.IdDonthuoc;
                            pre.IdPhieudieutri = CurrentTreatID;
                            pre.NgayKedon = pres_date;
                            pre.NguoiTao = globalVariables.UserName;
                            pre.KieuDonthuoc = 0;
                            pre.Noitru = 1;
                            pre.IdKham = -1;
                            pre.IdGoi = -1;
                            pre.TrongGoi = 0;
                            pre.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objLuotkham.MaLuotkham,
                                                                                        Utility.Int32Dbnull(
                                                                                            objLuotkham.IdBenhnhan,
                                                                                            -1));
                            pre.TrangThai = 0;
                            //pre.PresStatus = null;
                            pre.IdDonthuocthaythe = -1;
                            pre.TrangthaiThanhtoan = 0;
                            pre.NgayThanhtoan = null;
                            pre.NgayCapphat = null;
                            pre.NgayXacnhan = null;
                            pre.NguoiSua = null;
                            pre.NgaySua = null;
                            pre.IsNew = true;
                            pre.Save();
                            KcbDonthuocChitietCollection lstobjChitietdonthuoc =
                                new Select().From(KcbDonthuocChitiet.Schema)
                                    .Where(KcbDonthuocChitiet.IdDonthuocColumn)
                                    .IsEqualTo(oldPreID)
                                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                            foreach (KcbDonthuocChitiet _detail in lstobjChitietdonthuoc)
                            {

                                _detail.IdDonthuoc = pre.IdDonthuoc;
                                _detail.IdKham = -1;
                                _detail.TrangThai = 0;
                                _detail.TrangthaiTonghop = 0;
                                _detail.TrangthaiThanhtoan = 0;
                                _detail.IdGoi = 0;
                                _detail.TrongGoi = 0;
                                _detail.SoluongHuy = 0;
                                _detail.TrangthaiHuy = 0;
                                _detail.NgayThanhtoan = null;
                                _detail.NguonThanhtoan = 1;
                                _detail.IdThanhtoan = -1;
                                _detail.NguoiHuy = null;
                                _detail.NgayHuy = null;
                                _detail.SoluongHuy = 0;
                                _detail.NgayXacnhan = null;
                                _detail.SoLuong = Utility.DecimaltoDbnull(_detail.SluongSua, -1) <= 0
                                    ? _detail.SoLuong
                                    : _detail.SluongSua.Value;
                                _detail.SluongSua = null;
                                ;
                                _detail.SluongLinh = null;
                                _detail.IsNew = true;
                                _detail.Save();
                            }
                        }

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        public ActionResult SaoChepPhieuDieuTri(NoitruPhieudieutri[] lstPhieudieutri, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrChidinhCLSChitiet, KcbDonthuocChitiet[] arrDonthuocChitiet , List<KcbDonthuocChitiet> lstVTTH)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        foreach (NoitruPhieudieutri objTreatment in lstPhieudieutri)
                        {
                            objTreatment.NguoiTao = globalVariables.UserName;
                            objTreatment.NgayTao = DateTime.Now;
                            objTreatment.TthaiBosung = 0;
                            objTreatment.IdBacsi = globalVariables.gv_intIDNhanvien;
                            objTreatment.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                            objTreatment.MaLuotkham = objLuotkham.MaLuotkham;
                            objTreatment.IdBenhnhan = objTreatment.IdBenhnhan;
                            objTreatment.IdBuongGiuong = objLuotkham.IdRavien;
                            objTreatment.TrangThai = 0;
                            objTreatment.TthaiIn = 0;
                            objTreatment.IpMaytao = globalVariables.gv_strIPAddress;
                            objTreatment.TenMaytao = globalVariables.gv_strComputerName;
                            objTreatment.IsNew = true;
                            objTreatment.Save();
                            if (arrChidinhCLSChitiet.Length > 0)
                            {
                                KcbChidinhcl objAssignInfo = new KcbChidinhcl();
                                objAssignInfo.IdDieutri = objTreatment.IdPhieudieutri;
                                objAssignInfo.IdBuongGiuong = objTreatment.IdBuongGiuong;
                                objAssignInfo.MaLuotkham = objTreatment.MaLuotkham;
                                objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(objTreatment.IdBenhnhan);
                                objAssignInfo.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                                objAssignInfo.Noitru = 1;
                                objAssignInfo.IdKhoadieutri = objTreatment.IdKhoanoitru;
                                objAssignInfo.IdKhoaChidinh = objTreatment.IdKhoanoitru;
                                objAssignInfo.IdKham=-1;
                                objAssignInfo.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objAssignInfo.IdPhongChidinh = objTreatment.IdKhoanoitru;
                                objAssignInfo.Barcode = string.Empty;
                                
                                objAssignInfo.NgayChidinh = objTreatment.NgayDieutri.Value;
                                objAssignInfo.NguoiTao = globalVariables.UserName;
                                objAssignInfo.NgayTao = DateTime.Now;
                                objAssignInfo.IpMaytao = globalVariables.gv_strIPAddress;
                                objAssignInfo.TenMaytao = globalVariables.gv_strComputerName;

                                objAssignInfo.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                                objAssignInfo.IsNew = true;
                                objAssignInfo.Save();
                               List<KcbChidinhclsChitiet> lstChidinhCLSChitiet=new List<KcbChidinhclsChitiet>();
                                foreach (KcbChidinhclsChitiet objAssignDetail in arrChidinhCLSChitiet)
                                {
                                    KcbChidinhclsChitiet objDetail = KcbChidinhclsChitiet.FetchByID(objAssignDetail.IdChitietchidinh);
                                    if (objDetail != null)
                                    {
                                        objDetail.IdChitietchidinh = -1;
                                        objDetail.IdChidinh = objAssignInfo.IdChidinh;
                                        objDetail.IdKham = -1;
                                        objDetail.TrangthaiThanhtoan = 0;
                                        objDetail.NgayThanhtoan = null;
                                        objDetail.TrangthaiHuy = 0;
                                        objDetail.ImgPath1 = string.Empty;
                                        objDetail.ImgPath2 = string.Empty;
                                        objDetail.ImgPath3 = string.Empty;
                                        objDetail.ImgPath4 = string.Empty;
                                        objDetail.TrangThai =0;
                                        //objDetail.MotaThem = null;
                                        objDetail.TrangthaiBhyt = 0;
                                        objDetail.IdThanhtoan = -1;
                                        objDetail.IdKhoaThuchien =(short) objAssignInfo.IdKhoadieutri;
                                        objDetail.IdPhongThuchien = objDetail.IdKhoaThuchien;
                                        objDetail.IdGoi = -1;
                                        objDetail.IdBacsiThuchien = -1;
                                        objDetail.NgayThuchien = null;
                                        objDetail.NguoiThuchien = null;
                                        //objDetail.KetLuan = null;
                                        objDetail.KetQua = null;
                                        //objDetail.DeNghi = null;
                                        //objDetail.MaVungkhaosat = null;
                                        objDetail.NguoiTao = globalVariables.UserName;
                                        objDetail.NgayTao = DateTime.Now;
                                        objDetail.IpMaytao = globalVariables.gv_strIPAddress;
                                        objDetail.TenMaytao = globalVariables.gv_strComputerName;
                                        objDetail.IsNew=true;
                                        lstChidinhCLSChitiet.Add(objDetail);
                                    }
                                    new KCB_CHIDINH_CANLAMSANG().InsertAssignDetail(objAssignInfo, objLuotkham, lstChidinhCLSChitiet.ToArray<KcbChidinhclsChitiet>());

                                }
                            }
                            if (arrDonthuocChitiet.Length > 0)
                            {
                                List<string> lstErr=new List<string>();
                               string  GUID = THU_VIEN_CHUNG.GetGUID();
                                var query = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                             let y = donthuoc.IdDonthuoc
                                             select y).Distinct();
                                foreach (var pres_id in query.ToList())
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {
                                        objPresInfo.Noitru = 1;
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                                                                       where donthuoc.IdDonthuoc == pres_id
                                                                                       select donthuoc).ToList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, objTreatment, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                    }

                                }
                            }
                            if (lstVTTH != null && lstVTTH.Count > 0)
                            {
                                List<string> lstErr = new List<string>();
                                string GUID = THU_VIEN_CHUNG.GetGUID();
                                var query = (from donthuoc in lstVTTH.AsEnumerable()
                                             let y = donthuoc.IdDonthuoc
                                             select y).Distinct();
                                foreach (var pres_id in query.ToList())
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {
                                        objPresInfo.Noitru = 1;
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = (from chitiet in lstVTTH.AsEnumerable()
                                                                                       where chitiet.IdDonthuoc == pres_id
                                                                                       select chitiet).ToList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, objTreatment, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                    }

                                }
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult SaoChepPhieuDieuTri_Single(NoitruPhieudieutri phieudulieu, NoitruPhieudieutri phieusaochep, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrChidinhCLSChitiet, KcbDonthuocChitiet[] arrDonthuocChitiet, List<KcbDonthuocChitiet> lstVTTH,bool saochepylenh)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (saochepylenh)
                        {
                            phieusaochep.ThongtinDieutri = phieudulieu.ThongtinDieutri;
                            phieusaochep.ThongtinTheodoi = phieudulieu.ThongtinTheodoi;
                            phieusaochep.IpMaysua = globalVariables.gv_strIPAddress;
                            phieusaochep.TenMaysua = globalVariables.gv_strComputerName;
                            phieusaochep.NguoiTao = globalVariables.UserName;
                            phieusaochep.NgayTao = DateTime.Now;
                            phieusaochep.IsNew = false;
                            phieusaochep.Save();
                        }
                            if (arrChidinhCLSChitiet.Length > 0)
                            {
                                KcbChidinhcl objAssignInfo = new KcbChidinhcl();
                                objAssignInfo.IdDieutri = phieusaochep.IdPhieudieutri;
                                objAssignInfo.IdBuongGiuong = phieusaochep.IdBuongGiuong;
                                objAssignInfo.MaLuotkham = phieusaochep.MaLuotkham;
                                objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(phieusaochep.IdBenhnhan);
                                objAssignInfo.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                                objAssignInfo.Noitru = 1;
                                objAssignInfo.IdKhoadieutri = phieusaochep.IdKhoanoitru;
                                objAssignInfo.IdKhoaChidinh = phieusaochep.IdKhoanoitru;
                                objAssignInfo.IdKham = -1;
                                objAssignInfo.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objAssignInfo.IdPhongChidinh = phieusaochep.IdKhoanoitru;
                                objAssignInfo.Barcode = string.Empty;

                                objAssignInfo.NgayChidinh = phieusaochep.NgayDieutri.Value;
                                objAssignInfo.NguoiTao = globalVariables.UserName;
                                objAssignInfo.NgayTao = DateTime.Now;
                                objAssignInfo.IpMaytao = globalVariables.gv_strIPAddress;
                                objAssignInfo.TenMaytao = globalVariables.gv_strComputerName;

                                objAssignInfo.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                                objAssignInfo.IsNew = true;
                                objAssignInfo.Save();
                                List<KcbChidinhclsChitiet> lstChidinhCLSChitiet = new List<KcbChidinhclsChitiet>();
                                foreach (KcbChidinhclsChitiet objAssignDetail in arrChidinhCLSChitiet)
                                {
                                    KcbChidinhclsChitiet objDetail = KcbChidinhclsChitiet.FetchByID(objAssignDetail.IdChitietchidinh);
                                    if (objDetail != null)
                                    {
                                        objDetail.IdChitietchidinh = -1;
                                        objDetail.IdChidinh = objAssignInfo.IdChidinh;
                                        objDetail.IdKham = -1;
                                        objDetail.TrangthaiThanhtoan = 0;
                                        objDetail.NgayThanhtoan = null;
                                        objDetail.TrangthaiHuy = 0;
                                        objDetail.ImgPath1 = string.Empty;
                                        objDetail.ImgPath2 = string.Empty;
                                        objDetail.ImgPath3 = string.Empty;
                                        objDetail.ImgPath4 = string.Empty;
                                        objDetail.TrangThai = 0;
                                        //objDetail.MotaThem = null;
                                        objDetail.TrangthaiBhyt = 0;
                                        objDetail.IdThanhtoan = -1;
                                        objDetail.IdKhoaThuchien = (short)objAssignInfo.IdKhoadieutri;
                                        objDetail.IdPhongThuchien = objDetail.IdKhoaThuchien;
                                        objDetail.IdGoi = -1;
                                        objDetail.IdBacsiThuchien = -1;
                                        objDetail.NgayThuchien = null;
                                        objDetail.NguoiThuchien = null;
                                        //objDetail.KetLuan = null;
                                        objDetail.KetQua = null;
                                        //objDetail.DeNghi = null;
                                        //objDetail.MaVungkhaosat = null;
                                        objDetail.NguoiTao = globalVariables.UserName;
                                        objDetail.NgayTao = DateTime.Now;
                                        objDetail.IpMaytao = globalVariables.gv_strIPAddress;
                                        objDetail.TenMaytao = globalVariables.gv_strComputerName;
                                        objDetail.IsNew = true;
                                        lstChidinhCLSChitiet.Add(objDetail);
                                    }
                                    new KCB_CHIDINH_CANLAMSANG().InsertAssignDetail(objAssignInfo, objLuotkham, lstChidinhCLSChitiet.ToArray<KcbChidinhclsChitiet>());

                                }
                            }
                            if (arrDonthuocChitiet.Length > 0)
                            {
                                List<string> lstErr = new List<string>();
                                string GUID = THU_VIEN_CHUNG.GetGUID();
                                var query = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                             let y = donthuoc.IdDonthuoc
                                             select y).Distinct();
                                foreach (var pres_id in query.ToList())
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {
                                        objPresInfo.Noitru = 1;
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = (from donthuoc in arrDonthuocChitiet.AsEnumerable()
                                                                                       where donthuoc.IdDonthuoc == pres_id
                                                                                       select donthuoc).ToList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, phieusaochep, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                    }

                                }
                            }
                            if (lstVTTH != null && lstVTTH.Count > 0)
                            {
                                List<string> lstErr = new List<string>();
                                string GUID = THU_VIEN_CHUNG.GetGUID();
                                var query = (from donthuoc in lstVTTH.AsEnumerable()
                                             let y = donthuoc.IdDonthuoc
                                             select y).Distinct();
                                foreach (var pres_id in query.ToList())
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {
                                        objPresInfo.Noitru = 1;
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = (from chitiet in lstVTTH.AsEnumerable()
                                                                                       where chitiet.IdDonthuoc == pres_id
                                                                                       select chitiet).ToList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, phieusaochep, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                    }

                                }
                            }
                       
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public ActionResult SaoChepPhieuDieuTri(NoitruPhieudieutri phieudulieu, NoitruPhieudieutri phieusaochep,KcbLuotkham objLuotkham, int loaisaochep)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (loaisaochep == 100 || loaisaochep == 50)//Sao chép y lệnh
                        {
                            phieusaochep.ThongtinDieutri = phieudulieu.ThongtinDieutri;
                            phieusaochep.ThongtinTheodoi = phieudulieu.ThongtinTheodoi;
                            phieusaochep.IpMaysua = globalVariables.gv_strIPAddress;
                            phieusaochep.TenMaysua = globalVariables.gv_strComputerName;
                            phieusaochep.NguoiTao = globalVariables.UserName;
                            phieusaochep.NgayTao = DateTime.Now;
                            phieusaochep.IsNew = false;
                            phieusaochep.Save();
                        }
                        DataTable m_dtDataPhieuDT = new noitru_phieudieutri().NoitruLaydulieuClsThuocVtthSaochep(Utility.Int32Dbnull(phieudulieu.IdPhieudieutri, -1),                    Utility.Int32Dbnull(phieudulieu.IdBenhnhan, -1), Utility.sDbnull(phieudulieu.MaLuotkham, ""));
                        if (loaisaochep == 100 || loaisaochep == 1)//Sao chép cận lâm sàng
                        {
                            KcbChidinhcl objAssignInfo = new KcbChidinhcl();
                            objAssignInfo.IdDieutri = phieusaochep.IdPhieudieutri;
                            objAssignInfo.IdBuongGiuong = phieusaochep.IdBuongGiuong;
                            objAssignInfo.MaLuotkham = phieusaochep.MaLuotkham;
                            objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(phieusaochep.IdBenhnhan);
                            objAssignInfo.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                            objAssignInfo.Noitru = 1;
                            objAssignInfo.IdKhoadieutri = phieusaochep.IdKhoanoitru;
                            objAssignInfo.IdKhoaChidinh = phieusaochep.IdKhoanoitru;
                            objAssignInfo.IdKham = -1;
                            objAssignInfo.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                            objAssignInfo.IdPhongChidinh = phieusaochep.IdKhoanoitru;
                            objAssignInfo.Barcode = string.Empty;

                            objAssignInfo.NgayChidinh = phieusaochep.NgayDieutri.Value;
                            objAssignInfo.NguoiTao = globalVariables.UserName;
                            objAssignInfo.NgayTao = DateTime.Now;
                            objAssignInfo.IpMaytao = globalVariables.gv_strIPAddress;
                            objAssignInfo.TenMaytao = globalVariables.gv_strComputerName;

                            objAssignInfo.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                            objAssignInfo.IsNew = true;
                            objAssignInfo.Save();
                            List<KcbChidinhclsChitiet> lstChidinhCLSChitiet = new List<KcbChidinhclsChitiet>();
                            List<long> lstIdChitietchidinh = getChidinhclsChitietID(m_dtDataPhieuDT);
                            foreach (long IdChitietchidinh in lstIdChitietchidinh)
                            {
                                KcbChidinhclsChitiet objDetail = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                                if (objDetail != null)
                                {
                                    objDetail.IdChitietchidinh = -1;
                                    objDetail.IdChidinh = objAssignInfo.IdChidinh;
                                    objDetail.IdKham = -1;
                                    objDetail.TrangthaiThanhtoan = 0;
                                    objDetail.NgayThanhtoan = null;
                                    objDetail.TrangthaiHuy = 0;
                                    objDetail.ImgPath1 = string.Empty;
                                    objDetail.ImgPath2 = string.Empty;
                                    objDetail.ImgPath3 = string.Empty;
                                    objDetail.ImgPath4 = string.Empty;
                                    objDetail.TrangThai = 0;
                                    //objDetail.MotaThem = null;
                                    objDetail.TrangthaiBhyt = 0;
                                    objDetail.IdThanhtoan = -1;
                                    objDetail.IdKhoaThuchien = (short)objAssignInfo.IdKhoadieutri;
                                    objDetail.IdPhongThuchien = objDetail.IdKhoaThuchien;
                                    objDetail.IdGoi = -1;
                                    objDetail.IdBacsiThuchien = -1;
                                    objDetail.NgayThuchien = null;
                                    objDetail.NguoiThuchien = null;
                                    //objDetail.KetLuan = null;
                                    objDetail.KetQua = null;
                                    //objDetail.DeNghi = null;
                                    //objDetail.MaVungkhaosat = null;
                                    objDetail.NguoiTao = globalVariables.UserName;
                                    objDetail.NgayTao = DateTime.Now;
                                    objDetail.IpMaytao = globalVariables.gv_strIPAddress;
                                    objDetail.TenMaytao = globalVariables.gv_strComputerName;
                                    objDetail.IsNew = true;
                                    lstChidinhCLSChitiet.Add(objDetail);
                                }
                                new KCB_CHIDINH_CANLAMSANG().InsertAssignDetail(objAssignInfo, objLuotkham, lstChidinhCLSChitiet.ToArray<KcbChidinhclsChitiet>());

                            }
                        }
                        if (loaisaochep == 100 || loaisaochep == 3)//Sao chép đơn thuốc
                        {
                            List<string> lstErr = new List<string>();
                            string GUID = THU_VIEN_CHUNG.GetGUID();
                            List<long> lstPresID = GetPresID(m_dtDataPhieuDT);
                          
                            foreach (long pres_id in lstPresID)
                            {
                                KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                if (objPresInfo != null)
                                {
                                    objPresInfo.Noitru = 1;
                                    List<KcbDonthuocChitiet> lstDonthuocchitiet = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(pres_id).ExecuteTypedList<KcbDonthuocChitiet>();
                                    SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, phieusaochep, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                }

                            }
                        }
                        if (loaisaochep == 100 || loaisaochep == 5)//Sao chép VTTH
                        {
                            List<string> lstErr = new List<string>();
                            string GUID = THU_VIEN_CHUNG.GetGUID();
                            List<long> lstPresID = GetVTTH(m_dtDataPhieuDT);

                            foreach (long pres_id in lstPresID)
                                {
                                    KcbDonthuoc objPresInfo = KcbDonthuoc.FetchByID(Utility.Int32Dbnull(pres_id));
                                    if (objPresInfo != null)
                                    {
                                        objPresInfo.Noitru = 1;
                                        List<KcbDonthuocChitiet> lstDonthuocchitiet = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(pres_id).ExecuteTypedList<KcbDonthuocChitiet>();
                                        SaoChepDonThuocTheoPhieuDieuTri(GUID, objLuotkham, objPresInfo, phieusaochep, lstDonthuocchitiet.ToArray<KcbDonthuocChitiet>(), lstErr);
                                    }

                                }
                            }
                        
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        private List<long>  GetPresID(DataTable dtData)
        {
             List<long> lstPresID = new List<long>();
             lstPresID = (from thuoc in dtData.AsEnumerable()
                       where Utility.Int32Dbnull(thuoc["id_loaithanhtoan"]) == 1
                          select Utility.Int64Dbnull(thuoc["id_phieu"])).ToList<long>();
             return lstPresID;
        }
        private List<long> GetVTTH(DataTable dtData)
        {
            List<long> lstPresID = new List<long>();
            lstPresID = (from thuoc in dtData.AsEnumerable()
                         where Utility.Int32Dbnull(thuoc["id_loaithanhtoan"]) == 1
                         select Utility.Int64Dbnull(thuoc["id_phieu"])).ToList<long>();
            return lstPresID;
        }
        private List<long> getChidinhclsChitietID(DataTable dtData)
        {
            List<long> lstCLS = new List<long>();
            lstCLS = (from thuoc in dtData.AsEnumerable()
                      where Utility.Int32Dbnull(thuoc["id_loaithanhtoan"]) == 2
                      select Utility.Int64Dbnull(thuoc["id_phieu_chitiet"])).ToList<long>();

            return lstCLS;
        }

    }
}
