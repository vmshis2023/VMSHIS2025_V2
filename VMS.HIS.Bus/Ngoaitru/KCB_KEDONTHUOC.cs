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
using System.Collections.Generic;
namespace VNS.HIS.BusRule.Classes
{
    public class KCB_KEDONTHUOC
    {
         private NLog.Logger log;
         public KCB_KEDONTHUOC()
        {
            log = LogManager.GetLogger("KCB_KEDONTHUOC");
        }
         public void XoaChitietDonthuoc(int IdChitietdonthuoc)
         {
             SPs.DonthuocXoaChitiet(IdChitietdonthuoc).Execute();
         }
         public void Capnhatchidanchitiet(long IdChitietdonthuoc,string columnName,object _value)
         {
             new Update(KcbDonthuocChitiet.Schema).Set(columnName).EqualTo(_value).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc).Execute();
         }
         public void Capnhatchidanchitiet(long id_donthuoc,int id_thuoc, string columnName, object _value)
         {
             new Update(KcbDonthuocChitiet.Schema).Set(columnName).EqualTo(_value).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(id_donthuoc).And(KcbDonthuocChitiet.Columns.IdThuoc).IsEqualTo(id_thuoc).Execute();
         }
         public void NoitruXoachandoan(string lstIdChandoan)
         {

             SPs.NoitruXoachandoan(lstIdChandoan).Execute();
         }
         public void NoitruXoaDinhduong(string lstIdChandoan)
         {

             SPs.NoitruXoaDinhduong(lstIdChandoan).Execute();
         }
         public void XoaChitietDonthuoc(string lstIdChitietDonthuoc)
         {

             SPs.DonthuocXoaChitietNew(lstIdChitietDonthuoc).Execute();
         }
         public ActionResult XoaDonthuoctaiquay(long idbenhnhan, long iddonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         new Delete().From(KcbDanhsachBenhnhan.Schema).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).Execute();
                         new Delete().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(iddonthuoc).And(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).Execute();
                         new Delete().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(iddonthuoc).Execute();
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch
             {
                 return ActionResult.Exception;
             }
         }
         public void XoaChitietDonthuoc(string lstIdChitietDonthuoc,int iddetail,decimal soluong)
         {
             using (TransactionScope scope = new TransactionScope())
             {
                 using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                 {
                     if (soluong > 0)
                     {
                         new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.SoLuong).EqualTo(soluong)
                             .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(iddetail).Execute();
                         new Update(TblKedonthuocTempt.Schema).Set(TblKedonthuocTempt.Columns.SoLuong).EqualTo(soluong)
                          .Where(TblKedonthuocTempt.Columns.IdChitietdonthuoc).IsEqualTo(iddetail).Execute();
                     }
                     else
                         lstIdChitietDonthuoc = lstIdChitietDonthuoc + "," + iddetail.ToString();
                     SPs.DonthuocXoaChitietNew(lstIdChitietDonthuoc).Execute();
                 }
                 scope.Complete();
             }
         }
         public static DataTable KcbThamkhamDulieuTiemchungTheoBenhnhan(long id_benhnhan)
         {
             return SPs.KcbThamkhamDulieuTiemchungTheoBenhnhan(id_benhnhan).GetDataSet().Tables[0];
         }
         public DataTable DmucLaychitietDonThuocMau(int ID)
         {
             return SPs.DmucLaychitietDonthuocmau(ID).GetDataSet().Tables[0];
         }
         public DataTable DmucLaychitietDinhmucVtth(int id_chitietdichvu)
         {
             return SPs.DmucLaychitietDinhmucVtth(id_chitietdichvu).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinDonthuoc_In(int idDonthuoc)
         {
             return SPs.DonthuocLaythongtinDein(idDonthuoc).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinDonthuocTaiQuay_In(int idDonthuoc)
         {
             return SPs.DonthuocTaiQuayLaythongtinDein(idDonthuoc).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinDonthuoc_InGop(string maLuotkham)
         {
             return SPs.DonthuocLaythongtinDeingop(maLuotkham).GetDataSet().Tables[0];
         }
         public DataTable Laythongtinchitietdonthuoc(long id_donthuoc)
         {
             return SPs.DonthuocLaythongtinDexem(id_donthuoc).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinchitietdonthuocDeSaoChep(long id_donthuoc)
         {
             return SPs.DonthuocLaythongtinDeSaoChep(id_donthuoc).GetDataSet().Tables[0];
         }
         public DataTable LayThuoctrongkhokedon(int id_kho, string KieuThuocVT, string ma_doituong_kcb,int Dungtuyen,int Noitru, string MaKHOATHIEN)
         {
             return SPs.DonthuocLaythongtinThuoctrongkhoKedon(id_kho, KieuThuocVT, ma_doituong_kcb,Dungtuyen,Noitru, MaKHOATHIEN).GetDataSet().Tables[0];
         }
         public DataTable LayThuoctrongkho(int id_kho, int id_maloaithuoc, string KieuKho)
         {
             return SPs.ThuocTimkiemThuoctrongkho(id_kho, id_maloaithuoc, KieuKho).GetDataSet().Tables[0];
         }
         public ActionResult CapnhatChandoan_NoTrans(KcbChandoanKetluan objkcbcdkl)
         {
             try
             {
                 if (objkcbcdkl == null || objkcbcdkl.IdBenhnhan <= 0 || objkcbcdkl.IdKham <= 0) return ActionResult.Cancel;
                 //using (TransactionScope scope = new TransactionScope())
                 //{
                 //    using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                 //    {
                         SqlQuery sqlkt = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbcdkl.IdKham);
                         if (objkcbcdkl.IsNew || sqlkt.GetRecordCount() <= 0)
                         {
                             var sp = SPs.SpKcbThemmoiChandoanKetluan(objkcbcdkl.IdChandoan,
                                 objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan,
                                 objkcbcdkl.MaLuotkham
                                 , objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                 objkcbcdkl.NguoiTao, objkcbcdkl.NgayTao,
                                 objkcbcdkl.IdKhoanoitru
                                 , objkcbcdkl.IdBuonggiuong, objkcbcdkl.IdBuong,
                                 objkcbcdkl.IdGiuong, objkcbcdkl.IdPhieudieutri
                                 , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                 objkcbcdkl.TenPhongkham, objkcbcdkl.Mach
                                 , objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap,
                                 objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho,
                                 objkcbcdkl.Cannang
                                 , objkcbcdkl.Chieucao, objkcbcdkl.Nhommau,
                                 objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong,
                                 objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri
                                 , objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                 objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                 objkcbcdkl.MabenhPhu, objkcbcdkl.MotaBenhchinh
                                 , objkcbcdkl.IpMaytao, objkcbcdkl.TenMaytao,
                                 objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                 , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4,
                                 objkcbcdkl.KPL5, objkcbcdkl.KPL6
                                 , objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                 objkcbcdkl.KL2, objkcbcdkl.KL3
                                 , objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                 objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen, objkcbcdkl.ChisoIbm, objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp, objkcbcdkl.NhanapMt, objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri,
                                 objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls);

                             sp.Execute();
                             objkcbcdkl.IdChandoan = Utility.Int64Dbnull(sp.OutputValues[0]);
                         }
                         else
                         {
                             SPs.SpKcbCapnhatChandoanKetluan(objkcbcdkl.IdChandoan
                                 , objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                 objkcbcdkl.NguoiSua, objkcbcdkl.NgaySua,
                                 objkcbcdkl.IdPhieudieutri
                                 , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                 objkcbcdkl.TenPhongkham, objkcbcdkl.Mach
                                 , objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap,
                                 objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho,
                                 objkcbcdkl.Cannang
                                 , objkcbcdkl.Chieucao, objkcbcdkl.Nhommau,
                                 objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong,
                                 objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri
                                 , objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                 objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                 objkcbcdkl.MabenhPhu, objkcbcdkl.MotaBenhchinh
                                 , objkcbcdkl.IpMaytao, objkcbcdkl.TenMaytao,
                                 objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                 , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4,
                                 objkcbcdkl.KPL5, objkcbcdkl.KPL6
                                 , objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                 objkcbcdkl.KL2, objkcbcdkl.KL3
                                 , objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                 objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen,
                                 objkcbcdkl.ChisoIbm, objkcbcdkl.ThilucMp,
                                 objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp,
                                 objkcbcdkl.NhanapMt, objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham, 
                                 objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan,
                                 objkcbcdkl.XuTri, objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls).Execute();
                         }
                         DataTable dtData = SPs.SpKcbLaydulieuChandoanKetluanTheoluotkham(objkcbcdkl.IdBenhnhan,0, objkcbcdkl.MaLuotkham).GetDataSet().Tables[0];
                         var query = (from chandoan in dtData.AsEnumerable()
                                      let y = Utility.sDbnull(chandoan["Chandoan"])
                                      where (y != "")
                                      select y).ToArray();
                         string cdchinh = string.Join(";", query);
                         var querychandoanphu = (from chandoan in dtData.AsEnumerable()
                                                 let y = Utility.sDbnull(chandoan["chandoan_kemtheo"])
                                                 where (y != "")
                                                 select y).ToArray();
                         string cdphu = string.Join(";", querychandoanphu);
                         var querybenhchinh = (from benhchinh in dtData.AsEnumerable()
                                               let y = Utility.sDbnull(benhchinh["mabenh_chinh"])
                                               where (y != "")
                                               select y).ToArray();
                         string mabenhchinh = string.Join(";", querybenhchinh);

                         var querybenhphu = (from benhphu in dtData.AsEnumerable()
                                             let y = Utility.sDbnull(benhphu["mabenh_phu"])
                                             where (y != "")
                                             select y).ToArray();
                         string mabenhphu = string.Join(";", querybenhphu);
                         SPs.SpKcbCapnhatMabenhChoLuotkham(objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham,
                             mabenhchinh, mabenhphu, cdchinh, DateTime.Now, globalVariables.UserName).Execute();
                     //}

                     //scope.Complete();
                     return ActionResult.Success;
                 //}
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh chuẩn đoán cho bệnh nhân {0}", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult CapnhatChandoan(KcbChandoanKetluan objkcbcdkl)
         {
             try
             {
                 if (objkcbcdkl == null || objkcbcdkl.IdBenhnhan <= 0 || objkcbcdkl.IdKham<=0) return ActionResult.Cancel;
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         SqlQuery sqlkt = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbcdkl.IdKham);
                         if (objkcbcdkl.IsNew || sqlkt.GetRecordCount()<=0)
                         {
                             var sp = SPs.SpKcbThemmoiChandoanKetluan(objkcbcdkl.IdChandoan,
                                 objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan,
                                 objkcbcdkl.MaLuotkham
                                 , objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                 objkcbcdkl.NguoiTao, objkcbcdkl.NgayTao,
                                 objkcbcdkl.IdKhoanoitru
                                 , objkcbcdkl.IdBuonggiuong, objkcbcdkl.IdBuong,
                                 objkcbcdkl.IdGiuong, objkcbcdkl.IdPhieudieutri
                                 , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                 objkcbcdkl.TenPhongkham, objkcbcdkl.Mach
                                 , objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap,
                                 objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho,
                                 objkcbcdkl.Cannang
                                 , objkcbcdkl.Chieucao, objkcbcdkl.Nhommau,
                                 objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong,
                                 objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri
                                 , objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                 objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                 objkcbcdkl.MabenhPhu,objkcbcdkl.MotaBenhchinh
                                 , objkcbcdkl.IpMaytao, objkcbcdkl.TenMaytao,
                                 objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                 , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4,
                                 objkcbcdkl.KPL5, objkcbcdkl.KPL6
                                 , objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                 objkcbcdkl.KL2, objkcbcdkl.KL3
                                 , objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                 objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen, objkcbcdkl.ChisoIbm, 
                                 objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp, objkcbcdkl.NhanapMt,
                                 objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri,
                                 objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls);

                            sp.Execute();
                            objkcbcdkl.IdChandoan = Utility.Int64Dbnull(sp.OutputValues[0]);
                         }
                         else
                         {
                             SPs.SpKcbCapnhatChandoanKetluan(objkcbcdkl.IdChandoan
                                 , objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                 objkcbcdkl.NguoiSua, objkcbcdkl.NgaySua,
                                 objkcbcdkl.IdPhieudieutri
                                 , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                 objkcbcdkl.TenPhongkham, objkcbcdkl.Mach
                                 , objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap,
                                 objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho,
                                 objkcbcdkl.Cannang
                                 , objkcbcdkl.Chieucao, objkcbcdkl.Nhommau,
                                 objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong,
                                 objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri
                                 , objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                 objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                 objkcbcdkl.MabenhPhu, objkcbcdkl.MotaBenhchinh
                                 , objkcbcdkl.IpMaytao, objkcbcdkl.TenMaytao,
                                 objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                 , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4,
                                 objkcbcdkl.KPL5, objkcbcdkl.KPL6
                                 , objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                 objkcbcdkl.KL2, objkcbcdkl.KL3
                                 , objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                 objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen,
                                 objkcbcdkl.ChisoIbm, objkcbcdkl.ThilucMp,
                                 objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp,
                                 objkcbcdkl.NhanapMt, objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham, 
                                 objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan,
                                 objkcbcdkl.XuTri, objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls).Execute();
                         }
                         DataTable dtData = SPs.SpKcbLaydulieuChandoanKetluanTheoluotkham(objkcbcdkl.IdBenhnhan,0, objkcbcdkl.MaLuotkham).GetDataSet().Tables[0];
                         var query = (from chandoan in dtData.AsEnumerable()
                                      let y = Utility.sDbnull(chandoan["Chandoan"])
                                      where (y != "")
                                      select y).ToArray();
                         string cdchinh = string.Join(";", query);
                         var querychandoanphu = (from chandoan in dtData.AsEnumerable()
                                                 let y = Utility.sDbnull(chandoan["chandoan_kemtheo"])
                                                 where (y != "")
                                                 select y).ToArray();
                         string cdphu = string.Join(";", querychandoanphu);
                         var querybenhchinh = (from benhchinh in dtData.AsEnumerable()
                                               let y = Utility.sDbnull(benhchinh["mabenh_chinh"])
                                               where (y != "")
                                               select y).ToArray();
                         string mabenhchinh = string.Join(";", querybenhchinh);

                         var querybenhphu = (from benhphu in dtData.AsEnumerable()
                                             let y = Utility.sDbnull(benhphu["mabenh_phu"])
                                             where (y != "")
                                             select y).ToArray();
                         string mabenhphu = string.Join(";", querybenhphu);
                         SPs.SpKcbCapnhatMabenhChoLuotkham(objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham,
                             mabenhchinh, mabenhphu, cdchinh, DateTime.Now, globalVariables.UserName).Execute();
                     }

                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh chuẩn đoán cho bệnh nhân {0}", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult ThemDonThuoc(KcbDanhsachBenhnhan objBenhnhan,  KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 string GUID = THU_VIEN_CHUNG.GetGUID();
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         objBenhnhan.Save();
                         if (objBenhnhan != null)
                         {
                             if (objDonthuoc.NgayKedon <= Convert.ToDateTime("01/01/1900"))
                                 objDonthuoc.NgayKedon = DateTime.Now;
                             objDonthuoc.IdBenhnhan = objBenhnhan.IdBenhnhan;
                             objDonthuoc.MaLuotkham = "";
                             objDonthuoc.IdKham = -1;
                             objDonthuoc.IsNew = true;
                             objDonthuoc.TenDonthuoc = "";

                             objDonthuoc.Save();
                             p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                             decimal PtramBH = 0;

                             foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.Save();
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                                 TblKedonthuocTempt objKedonthuocTempt = new TblKedonthuocTempt();
                                 objKedonthuocTempt.IdChitietdonthuoc = objDonthuocChitiet.IdChitietdonthuoc;
                                 objKedonthuocTempt.IdDonthuoc = objDonthuocChitiet.IdDonthuoc;
                                 objKedonthuocTempt.IdKho = Utility.Int32Dbnull(objDonthuocChitiet.IdKho);
                                 objKedonthuocTempt.IdThuockho = objDonthuocChitiet.IdChitietdonthuoc;
                                 objKedonthuocTempt.TrangThai = Utility.ByteDbnull(objDonthuocChitiet.TrangThai);
                                 objKedonthuocTempt.IdThuoc = objDonthuocChitiet.IdThuoc;
                                 objKedonthuocTempt.NgayKedon = objDonthuoc.NgayKedon;
                                 objKedonthuocTempt.SoLuong = objDonthuocChitiet.SoLuong;
                                 objKedonthuocTempt.IsNew = true;
                                 objKedonthuocTempt.Save();
                                 THU_VIEN_CHUNG.UpdateKeTam(Utility.Int32Dbnull(objDonthuocChitiet.IdChitietdonthuoc), Utility.Int32Dbnull(objDonthuocChitiet.IdDonthuoc), GUID,"",
                                                      Utility.Int32Dbnull(objDonthuocChitiet.IdThuockho), Utility.Int32Dbnull(objDonthuocChitiet.IdThuoc), Utility.Int16Dbnull(objDonthuocChitiet.IdKho),
                                                      Utility.DecimaltoDbnull(objDonthuocChitiet.SoLuong), (byte)LoaiTamKe.KEDONTHUOC, objDonthuoc.MaLuotkham, Utility.Int32Dbnull(objDonthuoc.IdBenhnhan),
                                                      Utility.Int32Dbnull(objDonthuoc.Noitru), Convert.ToDateTime(objDonthuoc.NgayKedon),"Thêm mới chi tiết đơn thuốc");
                             }

                         }


                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 Utility.CatchException(exception);
                 log.Error(string.Format("Loi khi them moi don thuoc {0}", exception.Message));
                 return ActionResult.Error;
             }

         }
         public ActionResult ThemDonThuoc(KcbDanhsachBenhnhan objBenhnhan, KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, KcbChandoanKetluan _KcbChandoanKetluan, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             string GUID = THU_VIEN_CHUNG.GetGUID();
             // Query _Query = KcbDonthuoc.CreateQuery();
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         if (objBenhnhan != null && objBenhnhan.KieuBenhnhan == 1 && Utility.Byte2Bool(objDonthuoc.Donthuoctaiquay))
                         {
                             objBenhnhan.Save();
                         }
                         if (objLuotkham != null)
                         {
                             log.Trace("4. Bat dau luu thuoc vao CSDL");
                             if (objDonthuoc.NgayKedon <= Convert.ToDateTime("01/01/1900"))
                                 objDonthuoc.NgayKedon = DateTime.Now;

                             objDonthuoc.IsNew = true;
                             objDonthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objLuotkham.MaLuotkham,
                                                                                        Utility.Int32Dbnull(
                                                                                            objLuotkham.IdBenhnhan,
                                                                                            -1));
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
                             log.Trace("4.1 Da luu don thuoc CSDL");
                                 SPs.SpKcbCapnhatBacsiKham(objDonthuoc.IdKham, objDonthuoc.IdBacsiChidinh, 0).Execute();
                                 log.Trace("4.2 Cap nhat bac si kham = BS ke don");

                                 if (!Utility.Byte2Bool(objDonthuoc.Noitru) && _KcbChandoanKetluan != null)//Chỉ lưu khi đang kê đơn ngoại trú
                                 {
                                     ////REM lại vì chỗ đơn thuốc chỉ cập nhật lại thông tin ngày tái khám trong bảng chẩn đoán kết luận
                                     CapnhatChandoan_NoTrans(_KcbChandoanKetluan);
                                     //if (_KcbChandoanKetluan.IdChandoan > 0)
                                     //{
                                     //    new Update(KcbChandoanKetluan.Schema)
                                     //    .Set(KcbChandoanKetluan.Columns.SoNgayhen).EqualTo(_KcbChandoanKetluan.SoNgayhen)
                                     //    .Set(KcbChandoanKetluan.Columns.SongayDieutri).EqualTo(_KcbChandoanKetluan.SongayDieutri)
                                     //    .Where(KcbChandoanKetluan.Columns.IdChandoan).IsEqualTo(_KcbChandoanKetluan.IdChandoan)
                                     //    .Execute();
                                     //}
                                     //else
                                     //    _KcbChandoanKetluan.Save();
                                 }
                             log.Trace("4.3 Da luu chan doan ket luan");
                             p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                             decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                             bool TUDONGDANHDAU_TRANGTHAISUDUNG = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEMCHUNG_TUDONGDANHDAU_TRANGTHAISUDUNG", "0", false) == "1";
                             foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.NgaySudung = objDonthuoc.NgayKedon;
                                 objDonthuocChitiet.DaDung=Utility.Bool2byte(TUDONGDANHDAU_TRANGTHAISUDUNG);
                                 
                                 byte TrangthaiBhyt = 1;
                                 if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//(objLuotkham.MaDoituongKcb == "DV")//Tự túc
                                 {
                                     PtramBH = 0m;
                                     TrangthaiBhyt = (byte)0;
                                     //ĐỐi tượng dịch vụ thì ko cần đánh dấu tự túc
                                     objDonthuocChitiet.TuTuc = 0;
                                 }
                                 else
                                     TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
                                 //Tính giá BHYT chi trả và BN chi trả theo Đối tượng và % bảo hiểm-->Hơi thừa có thể bỏ qua do đã tính ở Client
                                 //Nếu có dùng thì cần lấy lại KcbLuotkham do lo sợ người khác thay đổi đối tượng
                                 //TinhGiaThuoc.GB_TinhPhtramBHYT(objDonthuocChitiet, PtramBHYT);
                                 objDonthuocChitiet.TrangthaiBhyt = TrangthaiBhyt;// Utility.isTrue(objDonthuocChitiet.TuTuc.Value, 0, 1);
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 var spchitiet = SPs.SpKcbThemmoiChitietDonthuoc(objDonthuocChitiet.IdChitietdonthuoc, objDonthuocChitiet.IdDonthuoc, objDonthuocChitiet.IdDonthuocChuyengoi
                                     , objDonthuocChitiet.IdBenhnhan, objDonthuocChitiet.MaLuotkham, objDonthuocChitiet.IdKham, objDonthuocChitiet.IdKho, objDonthuocChitiet.IdThuoc, objDonthuocChitiet.NgayHethan
                                     , objDonthuocChitiet.SoLuong, objDonthuocChitiet.SluongSua, objDonthuocChitiet.SluongLinh, objDonthuocChitiet.Sang, objDonthuocChitiet.Trua, objDonthuocChitiet.Chieu, objDonthuocChitiet.Toi, objDonthuocChitiet.DonGia, objDonthuocChitiet.IdThuockho
                                     , objDonthuocChitiet.NgayNhap, objDonthuocChitiet.GiaNhap, objDonthuocChitiet.GiaBan, objDonthuocChitiet.GiaBhyt, objDonthuocChitiet.SoLo, objDonthuocChitiet.Vat
                                     , objDonthuocChitiet.MaNhacungcap, objDonthuocChitiet.PhuThu, objDonthuocChitiet.PhuthuDungtuyen, objDonthuocChitiet.PhuthuTraituyen, objDonthuocChitiet.MotaThem
                                     , objDonthuocChitiet.SoluongHuy, objDonthuocChitiet.TrangthaiHuy, objDonthuocChitiet.NguoiHuy, objDonthuocChitiet.NgayHuy, objDonthuocChitiet.TuTuc, objDonthuocChitiet.TrangThai
                                     , objDonthuocChitiet.TrangthaiTonghop, objDonthuocChitiet.NgayXacnhan, objDonthuocChitiet.TrangthaiBhyt, objDonthuocChitiet.SttIn, objDonthuocChitiet.MadoituongGia
                                     , objDonthuocChitiet.PtramBhytGoc, objDonthuocChitiet.PtramBhyt, objDonthuocChitiet.BhytChitra, objDonthuocChitiet.BnhanChitra, objDonthuocChitiet.MaDoituongKcb
                                     , objDonthuocChitiet.IdThanhtoan, objDonthuocChitiet.TrangthaiThanhtoan, objDonthuocChitiet.NgayThanhtoan, objDonthuocChitiet.CachDung
                                     , objDonthuocChitiet.ChidanThem, objDonthuocChitiet.DonviTinh, objDonthuocChitiet.SolanDung, objDonthuocChitiet.SoluongDung, objDonthuocChitiet.TrangthaiChuyen
                                     , objDonthuocChitiet.NgayTao, objDonthuocChitiet.NguoiTao, objDonthuocChitiet.TileChietkhau, objDonthuocChitiet.TienChietkhau, objDonthuocChitiet.KieuChietkhau
                                     , objDonthuocChitiet.IdGoi, objDonthuocChitiet.TrongGoi, objDonthuocChitiet.KieuBiendong, objDonthuocChitiet.NguonThanhtoan, objDonthuocChitiet.IpMaytao
                                     , objDonthuocChitiet.TenMaytao, objDonthuocChitiet.DaDung, objDonthuocChitiet.LydoTiemchung, objDonthuocChitiet.NguoiTiem, objDonthuocChitiet.VitriTiem
                                     , objDonthuocChitiet.MuiThu, objDonthuocChitiet.NgayhenMuiketiep, objDonthuocChitiet.PhanungSautiem, objDonthuocChitiet.Xutri, objDonthuocChitiet.KetluanNguyennhan
                                     , objDonthuocChitiet.KetQua, objDonthuocChitiet.NgaySudung, objDonthuocChitiet.SoDky, objDonthuocChitiet.SoQdinhthau, objDonthuoc.NgayKedon
                                     , objDonthuocChitiet.IdThe, objDonthuocChitiet.TyleTt,objDonthuocChitiet.IdDangky, objDonthuocChitiet.BhytNguonKhac, objDonthuocChitiet.BhytGiaTyle, objDonthuocChitiet.BnTtt, objDonthuocChitiet.BnCct
                                     );
                                 spchitiet.Execute();
                                 objDonthuocChitiet.IdChitietdonthuoc = Utility.Int64Dbnull(spchitiet.OutputValues[0]);
                                 THU_VIEN_CHUNG.UpdateKeTam(Utility.Int32Dbnull(objDonthuocChitiet.IdChitietdonthuoc), Utility.Int32Dbnull(objDonthuocChitiet.IdDonthuoc), GUID,"",
                                                     Utility.Int32Dbnull(objDonthuocChitiet.IdThuockho), Utility.Int32Dbnull(objDonthuocChitiet.IdThuoc), Utility.Int16Dbnull(objDonthuocChitiet.IdKho),
                                                     Utility.DecimaltoDbnull(objDonthuocChitiet.SoLuong), (byte)LoaiTamKe.KEDONTHUOC, objDonthuoc.MaLuotkham, Utility.Int32Dbnull(objDonthuoc.IdBenhnhan),
                                                     Utility.Int32Dbnull(objDonthuoc.Noitru), Convert.ToDateTime(objDonthuoc.NgayKedon), "Thêm mới chi tiết đơn thuốc");

                                 //ThemChitiet(objDonthuoc, objDonthuocChitiet, PtramBH, objLuotkham);
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             log.Trace("4.4 Da luu xong chi tiet don thuoc");

                         }


                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {

                 log.Error("Loi trong qua trinh luu don thuoc {0}", exception);
                 return ActionResult.Error;
             }

         }
         public void ThemChitiet(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDonthuocChitiet, decimal ptramBhyt, KcbLuotkham objLuotkham)
         {
             try
             {
                

                 //    scope.Complete();
                 //}
             }
             catch (Exception exception)
             {

                 log.Error("Loi trong qua trinh chi tiet luu don thuoc {0}", exception);
             }
         }
         public ActionResult CapnhatDonthuoc(KcbDanhsachBenhnhan objBenhnhan, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 string GUID = THU_VIEN_CHUNG.GetGUID();
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         objBenhnhan.Save();
                         p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                         new Update(KcbDonthuoc.Schema)
                             .Set(KcbDonthuoc.Columns.NgayKedon).EqualTo(objDonthuoc.NgayKedon)
                             .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                             .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                        
                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                             if (objDonthuocChitiet.IdChitietdonthuoc == -1)//Moi bo sung
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.SluongLinh = 0;
                                 objDonthuocChitiet.SluongSua = 0;
                                 objDonthuocChitiet.TrangThai = 0;

                                 objDonthuocChitiet.IdThanhtoan = -1;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.CachDung = objDonthuocChitiet.MotaThem;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.Save();
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             else
                             {
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                                 new Update(KcbDonthuocChitiet.Schema)
                                     .Set(KcbDonthuocChitiet.SoLuongColumn).EqualTo(objDonthuocChitiet.SoLuong)
                                     .Set(KcbDonthuocChitiet.NgaySuaColumn).EqualTo(objDonthuocChitiet.NgaySua)
                                     .Set(KcbDonthuocChitiet.NguoiSuaColumn).EqualTo(objDonthuocChitiet.NguoiSua)
                                     .Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(objDonthuocChitiet.IdChitietdonthuoc).Execute();
                             }
                             THU_VIEN_CHUNG.UpdateKeTam(Utility.Int32Dbnull(objDonthuocChitiet.IdChitietdonthuoc), Utility.Int32Dbnull(objDonthuocChitiet.IdDonthuoc), GUID,"",
                                                     Utility.Int32Dbnull(objDonthuocChitiet.IdThuockho), Utility.Int32Dbnull(objDonthuocChitiet.IdThuoc), Utility.Int16Dbnull(objDonthuocChitiet.IdKho),
                                                     Utility.DecimaltoDbnull(objDonthuocChitiet.SoLuong), (byte)LoaiTamKe.KEDONTHUOC, objDonthuoc.MaLuotkham, Utility.Int32Dbnull(objDonthuoc.IdBenhnhan),
                                                     Utility.Int32Dbnull(objDonthuoc.Noitru), Convert.ToDateTime(objDonthuoc.NgayKedon), "Thêm mới chi tiết đơn thuốc");
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh luu don thuoc", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult CapnhatDonthuoc(KcbDanhsachBenhnhan objBenhnhan, KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, KcbChandoanKetluan _KcbChandoanKetluan, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 string GUID = THU_VIEN_CHUNG.GetGUID();
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         if (objBenhnhan != null && objBenhnhan.KieuBenhnhan==1 && Utility.Byte2Bool(objDonthuoc.Donthuoctaiquay))
                         {
                             objBenhnhan.Save();
                         }
                         log.Trace("4. Bat dau cap nhat don thuoc");
                         p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                         SPs.SpKcbCapnhatDonthuoc(objDonthuoc.IdDonthuoc, globalVariables.UserName, DateTime.Now, objDonthuoc.LoidanBacsi,
                             objDonthuoc.NgayTaikham, objDonthuoc.TaiKham,objDonthuoc.ChanDoan, objDonthuoc.IpMaysua, objDonthuoc.TenMaysua).Execute();
                         log.Trace("4.1 Da cap nhat don thuoc");
                         if (Utility.Int32Dbnull(objDonthuoc.IdKham) > 0)
                         {
                             SPs.SpKcbCapnhatBacsiKham(objDonthuoc.IdKham, objDonthuoc.IdBacsiChidinh,0).Execute();
                             log.Trace("4.2 Da cap nhat BS kham=BS ke don");
                         }
                         decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);

                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                         }
                         if (objLuotkham.TrangthaiNoitru <= 0 && _KcbChandoanKetluan!=null) CapnhatChandoan(_KcbChandoanKetluan);
                         log.Trace("4.3 Da cap nhat thong tin chan doan ket luan");
                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             
                             if (objDonthuocChitiet.IdChitietdonthuoc == -1)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.SluongLinh = 0;
                                 objDonthuocChitiet.SluongSua = 0;
                                 objDonthuocChitiet.TrangThai = 0;
                                 
                                 objDonthuocChitiet.IdThanhtoan = -1;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.CachDung = objDonthuocChitiet.MotaThem;
                                 byte TrangthaiBhyt = 1;
                                 if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//(objLuotkham.MaDoituongKcb == "DV")//Tự túc
                                 {
                                     PtramBH = 0m;
                                     TrangthaiBhyt = (byte)0;
                                     //ĐỐi tượng dịch vụ thì ko cần đánh dấu tự túc
                                     objDonthuocChitiet.TuTuc = 0;
                                 }
                                 else
                                     TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
                                 //Tính giá BHYT chi trả và BN chi trả theo Đối tượng và % bảo hiểm-->Hơi thừa có thể bỏ qua do đã tính ở Client
                                 //Nếu có dùng thì cần lấy lại KcbLuotkham do lo sợ người khác thay đổi đối tượng
                                 //TinhGiaThuoc.GB_TinhPhtramBHYT(objDonthuocChitiet, PtramBHYT);
                                 objDonthuocChitiet.TrangthaiBhyt = TrangthaiBhyt;// Utility.isTrue(objDonthuocChitiet.TuTuc.Value, 0, 1);
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 var spchitiet = SPs.SpKcbThemmoiChitietDonthuoc(objDonthuocChitiet.IdChitietdonthuoc, objDonthuocChitiet.IdDonthuoc, objDonthuocChitiet.IdDonthuocChuyengoi
                                     , objDonthuocChitiet.IdBenhnhan, objDonthuocChitiet.MaLuotkham, objDonthuocChitiet.IdKham, objDonthuocChitiet.IdKho, objDonthuocChitiet.IdThuoc, objDonthuocChitiet.NgayHethan
                                     , objDonthuocChitiet.SoLuong, objDonthuocChitiet.SluongSua, objDonthuocChitiet.SluongLinh, objDonthuocChitiet.Sang, objDonthuocChitiet.Trua, objDonthuocChitiet.Chieu, objDonthuocChitiet.Toi, objDonthuocChitiet.DonGia, objDonthuocChitiet.IdThuockho
                                     , objDonthuocChitiet.NgayNhap, objDonthuocChitiet.GiaNhap, objDonthuocChitiet.GiaBan, objDonthuocChitiet.GiaBhyt, objDonthuocChitiet.SoLo, objDonthuocChitiet.Vat
                                     , objDonthuocChitiet.MaNhacungcap, objDonthuocChitiet.PhuThu, objDonthuocChitiet.PhuthuDungtuyen, objDonthuocChitiet.PhuthuTraituyen, objDonthuocChitiet.MotaThem
                                     , objDonthuocChitiet.SoluongHuy, objDonthuocChitiet.TrangthaiHuy, objDonthuocChitiet.NguoiHuy, objDonthuocChitiet.NgayHuy, objDonthuocChitiet.TuTuc, objDonthuocChitiet.TrangThai
                                     , objDonthuocChitiet.TrangthaiTonghop, objDonthuocChitiet.NgayXacnhan, objDonthuocChitiet.TrangthaiBhyt, objDonthuocChitiet.SttIn, objDonthuocChitiet.MadoituongGia
                                     , objDonthuocChitiet.PtramBhytGoc, objDonthuocChitiet.PtramBhyt, objDonthuocChitiet.BhytChitra, objDonthuocChitiet.BnhanChitra, objDonthuocChitiet.MaDoituongKcb
                                     , objDonthuocChitiet.IdThanhtoan, objDonthuocChitiet.TrangthaiThanhtoan, objDonthuocChitiet.NgayThanhtoan, objDonthuocChitiet.CachDung
                                     , objDonthuocChitiet.ChidanThem, objDonthuocChitiet.DonviTinh, objDonthuocChitiet.SolanDung, objDonthuocChitiet.SoluongDung, objDonthuocChitiet.TrangthaiChuyen
                                     , objDonthuocChitiet.NgayTao, objDonthuocChitiet.NguoiTao, objDonthuocChitiet.TileChietkhau, objDonthuocChitiet.TienChietkhau, objDonthuocChitiet.KieuChietkhau
                                     , objDonthuocChitiet.IdGoi, objDonthuocChitiet.TrongGoi, objDonthuocChitiet.KieuBiendong, objDonthuocChitiet.NguonThanhtoan, objDonthuocChitiet.IpMaytao
                                     , objDonthuocChitiet.TenMaytao, objDonthuocChitiet.DaDung, objDonthuocChitiet.LydoTiemchung, objDonthuocChitiet.NguoiTiem, objDonthuocChitiet.VitriTiem
                                     , objDonthuocChitiet.MuiThu, objDonthuocChitiet.NgayhenMuiketiep, objDonthuocChitiet.PhanungSautiem, objDonthuocChitiet.Xutri, objDonthuocChitiet.KetluanNguyennhan
                                     , objDonthuocChitiet.KetQua, objDonthuocChitiet.NgaySudung, objDonthuocChitiet.SoDky, objDonthuocChitiet.SoQdinhthau, objDonthuoc.NgayKedon
                                     , objDonthuocChitiet.IdThe, objDonthuocChitiet.TyleTt, objDonthuocChitiet.IdDangky, objDonthuocChitiet.BhytNguonKhac, objDonthuocChitiet.BhytGiaTyle, objDonthuocChitiet.BnTtt, objDonthuocChitiet.BnCct);
                                 spchitiet.Execute();
                                 objDonthuocChitiet.IdChitietdonthuoc = Utility.Int64Dbnull(spchitiet.OutputValues[0]);
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             else
                             {
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                                 SPs.SpKcbCapnhatChitietDonthuoc(objDonthuocChitiet.IdChitietdonthuoc, objDonthuocChitiet.SoLuong, objDonthuocChitiet.Sang,objDonthuocChitiet.Trua,objDonthuocChitiet.Chieu,objDonthuocChitiet.Toi,objDonthuocChitiet.MotaThem, objDonthuocChitiet.SttIn, objDonthuocChitiet.NgaySua
                                     , objDonthuocChitiet.NguoiSua, objDonthuocChitiet.IpMaysua, objDonthuocChitiet.TenMaysua).Execute();
                             }
                             THU_VIEN_CHUNG.UpdateKeTam(Utility.Int32Dbnull(objDonthuocChitiet.IdChitietdonthuoc), Utility.Int32Dbnull(objDonthuocChitiet.IdDonthuoc), GUID,"",
                                                     Utility.Int32Dbnull(objDonthuocChitiet.IdThuockho), Utility.Int32Dbnull(objDonthuocChitiet.IdThuoc), Utility.Int16Dbnull(objDonthuocChitiet.IdKho),
                                                     Utility.DecimaltoDbnull(objDonthuocChitiet.SoLuong), (byte)LoaiTamKe.KEDONTHUOC, objDonthuoc.MaLuotkham, Utility.Int32Dbnull(objDonthuoc.IdBenhnhan),
                                                     Utility.Int32Dbnull(objDonthuoc.Noitru), Convert.ToDateTime(objDonthuoc.NgayKedon), "Thêm mới chi tiết đơn thuốc");
                         }
                         log.Trace("4.4 Da cap nhat xong chi tiet don thuoc");
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error(string.Format("Loi khi cap nhat don thuoc {0}", exception.Message));
                 return ActionResult.Error;
             }
         }
         public ActionResult CapnhatChitiet( long id_chitiet,decimal PtramBHYT, byte tu_tuc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         KcbDonthuocChitiet objChitietDonthuoc =KcbDonthuocChitiet.FetchByID(id_chitiet);
                         if (objChitietDonthuoc != null)
                         {
                             if (tu_tuc == 1)
                             {
                                 objChitietDonthuoc.BhytChitra = 0;
                                 objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0);
                                 objChitietDonthuoc.PtramBhyt = 0;
                             }
                             else
                             {
                                 objChitietDonthuoc.BhytChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia) *
                                                             Utility.DecimaltoDbnull(PtramBHYT) / 100;

                                 objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) -
                                                           Utility.DecimaltoDbnull(objChitietDonthuoc.BhytChitra, 0);
                                 objChitietDonthuoc.PtramBhyt = Utility.DecimaltoDbnull(PtramBHYT);
                             }
                             objChitietDonthuoc.IsNew = false;
                             objChitietDonthuoc.MarkOld();
                             objChitietDonthuoc.Save();
                         }          }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh luu don thuoc", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult CapnhatnhomDonThuocMau(DmucDonthuocmau objNhom, List<DmucDonthuocmauChitiet> lstChitiet)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {

                         objNhom.Save();
                         foreach (DmucDonthuocmauChitiet objChitiet in lstChitiet)
                         {
                             objChitiet.IdNhom = objNhom.Id;
                             if (Utility.Int32Dbnull(objChitiet.SoLuong) <= 0) objChitiet.SoLuong = 1;
                             if (objChitiet.IdChitiet <= 0)
                             {
                                 objChitiet.IsNew = true;
                                 objChitiet.Save();
                             }
                             else
                             {
                                 objChitiet.MarkOld();
                                 objChitiet.IsNew = false;
                                 objChitiet.Save();
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.InfoException("Loi thong tin ", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult ThemnhomDonThuocMau(DmucDonthuocmau objNhom, List<DmucDonthuocmauChitiet> lstChitiet)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         if (objNhom != null)
                         {
                             objNhom.IsNew = true;
                             objNhom.Save();
                             foreach (DmucDonthuocmauChitiet objChitiet in lstChitiet)
                             {
                                 objChitiet.IdNhom = objNhom.Id;
                                 if (Utility.Int32Dbnull(objChitiet.SoLuong) <= 0) objChitiet.SoLuong = 1;
                                 if (objChitiet.IdChitiet <= 0)
                                 {
                                     objChitiet.IsNew = true;
                                     objChitiet.Save();
                                 }
                                 else
                                 {
                                     objChitiet.MarkOld();
                                     objChitiet.IsNew = false;
                                     objChitiet.Save();
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
         public ActionResult ThemDinhmucVTTH(List<TDinhmucVtth> lstChitiet)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         new Delete().From(TDinhmucVtth.Schema).Where(TDinhmucVtth.Columns.IdChitietdichvu).IsEqualTo(lstChitiet[0].IdChitietdichvu).Execute();
                         foreach (TDinhmucVtth objChitiet in lstChitiet)
                         {
                             if (Utility.Int32Dbnull(objChitiet.SoLuong) <= 0) objChitiet.SoLuong = 1;
                             objChitiet.IsNew = true;
                             objChitiet.Save();
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
    }
}
