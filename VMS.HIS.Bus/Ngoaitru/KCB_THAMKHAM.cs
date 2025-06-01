using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using NLog;

namespace VNS.HIS.BusRule.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class KCB_THAMKHAM
    {
        private NLog.Logger log;
        public KCB_THAMKHAM()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public DataTable KcbTiemchungInbangketruocTiemchung(long? IdBenhnhan, string maluotkham, long? Idkham, long? Iddonthuoc, int? Idthuoc)
        {
            return SPs.KcbTiemchungInbangketruocTiemchung(IdBenhnhan, maluotkham, Idkham, Iddonthuoc, Idthuoc).GetDataSet().Tables[0];
        }
        public ActionResult DanhdautrangthaiTiem(KcbDonthuocChitiet objChitiet, long _IdKham, bool Da_tiem)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        if (objChitiet != null)
                        {
                            objChitiet.IsNew = false;
                            objChitiet.DaDung = Utility.Bool2byte(Da_tiem);
                            objChitiet.MarkOld();
                            objChitiet.Save();
                        }
                        else
                        {
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.DaDung).EqualTo(Utility.Bool2byte(Da_tiem))
                                .Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(_IdKham)
                                .Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh chuyen vien khoi noi tru {0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult LuuHoibenhvaChandoan(KcbChandoanKetluan objkcbcdkl,KcbLuotkham objLuotkham, KcbDonthuocChitiet objChitiet,KcbDangkyKcb objCongkham,  bool luudulieutiemchung,bool isFinish)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objCongkham != null)
                            new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.ThoigianBatdauColumn).EqualTo(objCongkham.ThoigianBatdau).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
                        if (objChitiet != null)
                        {
                            objChitiet.IsNew = false;
                            if (objkcbcdkl != null)
                            {
                                objChitiet.PhanungSautiem = objkcbcdkl.PhanungSautiemchung;
                                objChitiet.Xutri = objkcbcdkl.HuongDieutri;
                                objChitiet.KetQua = objkcbcdkl.Ketluan;
                                objChitiet.KetluanNguyennhan = objkcbcdkl.KetluanNguyennhan;
                            }
                            objChitiet.MarkOld();
                            objChitiet.Save();
                        }
                        
                        if (objkcbcdkl != null)
                        {
                            log.Trace("1. Bắt đầu lưu chẩn đoán của bệnh nhân: " + objkcbcdkl.MaLuotkham);
                            if (objLuotkham != null && !objLuotkham.IsNew) objLuotkham.Save();//Lưu thông tin mã bệnh chính, mã bệnh phụ
                            if (objkcbcdkl.IsNew)
                            {

                              //  objkcbcdkl.Save();
                                SPs.SpKcbThemmoiChandoanKetluan(objkcbcdkl.IdChandoan, objkcbcdkl.IdKham,
                                    objkcbcdkl.IdBenhnhan,
                                    objkcbcdkl.MaLuotkham, objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                    objkcbcdkl.NguoiTao,
                                    objkcbcdkl.NgayTao, objkcbcdkl.IdKhoanoitru, objkcbcdkl.IdBuonggiuong,
                                    objkcbcdkl.IdBuong, objkcbcdkl.IdGiuong
                                    , objkcbcdkl.IdPhieudieutri, objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                    objkcbcdkl.TenPhongkham, objkcbcdkl.Mach, objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap
                                    , objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho, objkcbcdkl.Cannang,
                                    objkcbcdkl.Chieucao, objkcbcdkl.Nhommau, objkcbcdkl.Ketluan,objkcbcdkl.ChedoDinhduong,
                                    objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri, objkcbcdkl.TrieuchungBandau
                                    , objkcbcdkl.Chandoan, objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                    objkcbcdkl.MabenhPhu,objkcbcdkl.MotaBenhchinh, objkcbcdkl.IpMaytao, objkcbcdkl.TenMaytao,
                                    objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                    , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4, objkcbcdkl.KPL5,
                                    objkcbcdkl.KPL6, objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                    objkcbcdkl.KL2, objkcbcdkl.KL3, objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet
                                    , objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen,objkcbcdkl.ChisoIbm,
                                    objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp, objkcbcdkl.NhanapMt, objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri
                                    , objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls
                                    ).Execute();
                                log.Trace("1.1 Thêm mới lưu chẩn đoán của bệnh nhân: " + objkcbcdkl.MaLuotkham);
                            }
                            else
                            {
                                SPs.SpKcbCapnhatChandoanKetluan(objkcbcdkl.IdChandoan, objkcbcdkl.IdBacsikham,
                                    objkcbcdkl.NgayChandoan, objkcbcdkl.NguoiSua, objkcbcdkl.NgaySua,
                                    objkcbcdkl.IdPhieudieutri
                                    , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham, objkcbcdkl.TenPhongkham,
                                    objkcbcdkl.Mach, objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap, objkcbcdkl.Nhiptim,
                                    objkcbcdkl.Nhiptho, objkcbcdkl.Cannang, objkcbcdkl.Chieucao
                                    , objkcbcdkl.Nhommau, objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong, objkcbcdkl.HuongDieutri,
                                    objkcbcdkl.SongayDieutri, objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                    objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh
                                    , objkcbcdkl.MabenhPhu,objkcbcdkl.MotaBenhchinh, objkcbcdkl.IpMaysua, objkcbcdkl.TenMaysua,
                                    objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                    , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4, objkcbcdkl.KPL5,
                                    objkcbcdkl.KPL6, objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                    objkcbcdkl.KL2, objkcbcdkl.KL3, objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                    objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen, objkcbcdkl.ChisoIbm,
                                    objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp, objkcbcdkl.NhanapMt
                                    , objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham, objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri
                                    , objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls).Execute();
                                log.Trace("1.2 Cập nhật chẩn đoán của bệnh nhân: " + objkcbcdkl.MaLuotkham);
                                // objkcbcdkl.MarkOld();

                                //  objkcbcdkl.Save();
                            }
                            if (luudulieutiemchung && objChitiet == null && objkcbcdkl != null)
                            {
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.PhanungSautiem).EqualTo(objkcbcdkl.PhanungSautiemchung)
                                    .Set(KcbDonthuocChitiet.Columns.Xutri).EqualTo(objkcbcdkl.HuongDieutri)
                                    .Set(KcbDonthuocChitiet.Columns.KetQua).EqualTo(objkcbcdkl.Ketluan)
                                    .Set(KcbDonthuocChitiet.Columns.KetluanNguyennhan).EqualTo(objkcbcdkl.KetluanNguyennhan)
                                    .Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(objkcbcdkl.IdKham)
                                    .Execute();
                            }
                            if (Utility.sDbnull(objkcbcdkl.Nhommau).Length > 0)
                                new Update(KcbDanhsachBenhnhan.Schema).Set(KcbDanhsachBenhnhan.Columns.NhomMau).EqualTo(objkcbcdkl.Nhommau).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(objkcbcdkl.IdBenhnhan).Execute();
                            log.Trace("2.Kết thúc lưu chẩn đoán của bệnh nhân: " + objkcbcdkl.MaLuotkham);
                        }
                        log.Trace("");
                        //sh.Dispose();
                    }

                    scope.Complete();
                    //  Reg_ID = Utility.Int32Dbnull(objCongkham.IdKham, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh luu chan doan ket luan {0}", exception);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc update thông tin xác nhận gói
        /// </summary>
        /// <param name="objThongtinGoiDvuBnhan"></param>
        /// <returns></returns>
     

        public ActionResult UpdateExamInfo(KcbChandoanKetluan objkcbcdkl, KcbDangkyKcb objCongkham,
                                           KcbLuotkham objPatientExam)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                       
                        if (objkcbcdkl != null)
                        {
                            log.Trace("1.1 Bat dau ket thuc benh nhan: " + objkcbcdkl.MaLuotkham);
                            if (objkcbcdkl.IsNew)
                            {
                                //objkcbcdkl.Save();
                                SPs.SpKcbThemmoiChandoanKetluan(objkcbcdkl.IdChandoan, objkcbcdkl.IdKham,
                                    objkcbcdkl.IdBenhnhan,
                                    objkcbcdkl.MaLuotkham, objkcbcdkl.IdBacsikham, objkcbcdkl.NgayChandoan,
                                    objkcbcdkl.NguoiTao,
                                    objkcbcdkl.NgayTao, objkcbcdkl.IdKhoanoitru, objkcbcdkl.IdBuonggiuong,
                                    objkcbcdkl.IdBuong, objkcbcdkl.IdGiuong
                                    , objkcbcdkl.IdPhieudieutri, objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham,
                                    objkcbcdkl.TenPhongkham, objkcbcdkl.Mach, objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap
                                    , objkcbcdkl.Nhiptim, objkcbcdkl.Nhiptho, objkcbcdkl.Cannang,
                                    objkcbcdkl.Chieucao, objkcbcdkl.Nhommau, objkcbcdkl.Ketluan,
                                    objkcbcdkl.ChedoDinhduong,
                                    objkcbcdkl.HuongDieutri, objkcbcdkl.SongayDieutri, objkcbcdkl.TrieuchungBandau
                                    , objkcbcdkl.Chandoan, objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh,
                                    objkcbcdkl.MabenhPhu, objkcbcdkl.MotaBenhchinh, objkcbcdkl.IpMaytao,
                                    objkcbcdkl.TenMaytao,
                                    objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                    , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4, objkcbcdkl.KPL5,
                                    objkcbcdkl.KPL6, objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                    objkcbcdkl.KL2, objkcbcdkl.KL3, objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet
                                    , objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen, objkcbcdkl.ChisoIbm,
                                    objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp,
                                    objkcbcdkl.NhanapMt, objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri
                                    , objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai, 
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls
                                    ).Execute();

                            }
                            else
                            {
                                SPs.SpKcbCapnhatChandoanKetluan(objkcbcdkl.IdChandoan, objkcbcdkl.IdBacsikham,
                                    objkcbcdkl.NgayChandoan, objkcbcdkl.NguoiSua, objkcbcdkl.NgaySua,
                                    objkcbcdkl.IdPhieudieutri
                                    , objkcbcdkl.Noitru, objkcbcdkl.IdPhongkham, objkcbcdkl.TenPhongkham,
                                    objkcbcdkl.Mach, objkcbcdkl.Nhietdo, objkcbcdkl.Huyetap, objkcbcdkl.Nhiptim,
                                    objkcbcdkl.Nhiptho, objkcbcdkl.Cannang, objkcbcdkl.Chieucao
                                    , objkcbcdkl.Nhommau, objkcbcdkl.Ketluan, objkcbcdkl.ChedoDinhduong,
                                    objkcbcdkl.HuongDieutri,
                                    objkcbcdkl.SongayDieutri, objkcbcdkl.TrieuchungBandau, objkcbcdkl.Chandoan,
                                    objkcbcdkl.ChandoanKemtheo, objkcbcdkl.MabenhChinh
                                    , objkcbcdkl.MabenhPhu, objkcbcdkl.MotaBenhchinh, objkcbcdkl.IpMaysua,
                                    objkcbcdkl.TenMaysua,
                                    objkcbcdkl.PhanungSautiemchung, objkcbcdkl.KPL1
                                    , objkcbcdkl.KPL2, objkcbcdkl.KPL3, objkcbcdkl.KPL4, objkcbcdkl.KPL5,
                                    objkcbcdkl.KPL6, objkcbcdkl.KPL7, objkcbcdkl.KPL8, objkcbcdkl.KL1,
                                    objkcbcdkl.KL2, objkcbcdkl.KL3, objkcbcdkl.KetluanNguyennhan, objkcbcdkl.NhanXet,
                                    objkcbcdkl.ChongchidinhKhac, objkcbcdkl.SoNgayhen, objkcbcdkl.ChisoIbm,
                                    objkcbcdkl.ThilucMp, objkcbcdkl.ThilucMt, objkcbcdkl.NhanapMp,
                                    objkcbcdkl.NhanapMt, objkcbcdkl.IdKham, objkcbcdkl.IdBenhnhan, objkcbcdkl.MaLuotkham, objkcbcdkl.QuatrinhBenhly, objkcbcdkl.TiensuBenh, objkcbcdkl.TomtatCls, objkcbcdkl.LoiDan, objkcbcdkl.XuTri
                                    , objkcbcdkl.Para, objkcbcdkl.QuaiBi, objkcbcdkl.SPO2, objkcbcdkl.PhantruocMatphai, objkcbcdkl.PhantruocMattrai, objkcbcdkl.DaymatMatphai,
                                    objkcbcdkl.DaymatMattrai, objkcbcdkl.VannhanMatphai, objkcbcdkl.VannhanMattrai, objkcbcdkl.ChandoanMatphai, objkcbcdkl.ChandoanMattrai, objkcbcdkl.Khammat
                                    , objkcbcdkl.IcdMatphai, objkcbcdkl.TenIcdMatphai, objkcbcdkl.IcdMattrai, objkcbcdkl.TenIcdMattrai
                                    , objkcbcdkl.VitriIcdChinh, objkcbcdkl.Sotxuathuyet, objkcbcdkl.Taychanmieng, objkcbcdkl.MantinhCapthuoc, objkcbcdkl.MantinhCls).Execute();
                            }

                        }
                        if (objCongkham != null && objPatientExam!=null)
                        {
                            log.Trace("1.2 Luu xong chan doan cua benh nhan: " + objPatientExam.MaLuotkham);
                            SqlQuery sqlQuery = new Select().From(
                                KcbChandoanKetluan.Schema)
                                .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                                .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan)
                                .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(0)//Chỉ lấy các chẩn đoán ngoại trú
                                .OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);

                            var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();

                            var query = (from chandoan in objInfoCollection.AsEnumerable()
                                let y = Utility.sDbnull(chandoan.Chandoan)
                                where (y != "")
                                select y).ToArray();
                            //string maCDchinh = string.Join(";", query);
                            //DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(maCDchinh).GetDataSet().Tables[0];

                            string cdchinh = string.Join(";", query);
                            //if (dtDataChandoan.Rows.Count > 0) cdchinh =Utility.sDbnull( dtDataChandoan.Rows[0][0],"");
                            var querychandoanphu = (from chandoan in objInfoCollection.AsEnumerable()
                                let y = Utility.sDbnull(chandoan.ChandoanKemtheo)
                                where (y != "")
                                select y).ToArray();
                            string cdphu = string.Join(";", querychandoanphu);
                            var querybenhchinh = (from benhchinh in objInfoCollection.AsEnumerable()
                                let y = Utility.sDbnull(benhchinh.MabenhChinh)
                                where (y != "")
                                select y).ToArray();
                            string mabenhchinh = string.Join(",", querybenhchinh);

                            var querybenhphu = (from benhphu in objInfoCollection.AsEnumerable()
                                let y = Utility.sDbnull(benhphu.MabenhPhu)
                                where (y != "")
                                select y).ToArray();
                            string mabenhphu = string.Join(",", querybenhphu);

                            SPs.KcbLuotkhamTrangthaiketthuckham(objPatientExam.IdBenhnhan,
                                objPatientExam.MaLuotkham, objPatientExam.TrangthaiNgoaitru, objPatientExam.Noitru,
                                mabenhchinh, mabenhphu, objPatientExam.KetLuan, objPatientExam.HuongDieutri, cdchinh
                                , cdphu, objPatientExam.Locked, objPatientExam.SongayDieutri, objPatientExam.TrieuChung,
                                globalVariables.UserName, DateTime.Now, globalVariables.gv_strIPAddress,
                                globalVariables.gv_strComputerName, objCongkham.IdBacsikham, objCongkham.TrangThai,
                                objCongkham.IdKham, objCongkham.ThoigianBatdau, objCongkham.ThoigianKetthuc).Execute();

                            log.Trace("1.3 Update thanh cong trang thai cua benh nhan: " + objPatientExam.MaLuotkham);
                        }

                        //new Update(KcbLuotkham.Schema)
                        //    .Set(KcbLuotkham.Columns.MabenhChinh).EqualTo(mabenhchinh)
                        //    .Set(KcbLuotkham.Columns.MabenhPhu).EqualTo(mabenhphu)
                        //    .Set(KcbLuotkham.Columns.KetLuan).EqualTo(objPatientExam.KetLuan)
                        //    .Set(KcbLuotkham.Columns.HuongDieutri).EqualTo(objPatientExam.HuongDieutri)
                        //    .Set(KcbLuotkham.Columns.ChanDoan).EqualTo(cdchinh)
                        //    .Set(KcbLuotkham.Columns.SongayDieutri).EqualTo(objPatientExam.SongayDieutri)
                        //    .Set(KcbLuotkham.Columns.ChandoanKemtheo).EqualTo(cdphu)
                        //    .Set(KcbLuotkham.Columns.TrieuChung).EqualTo(objPatientExam.TrieuChung)
                        //    .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        //    .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                        //    .Set(KcbLuotkham.Columns.Locked).EqualTo(objPatientExam.Locked)
                        //    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(objPatientExam.NguoiKetthuc)
                        //    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(objPatientExam.NgayKetthuc)
                        //    .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(objPatientExam.TrangthaiNgoaitru)
                        //    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                        //    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();
                        ////Tạm bỏ tránh việc bị cập nhật sai bác sĩ chỉ định nếu bác sĩ đó chỉ lưu thông tin kết luận
                        ////SPs.KcbThamkhamCappnhatBsyKham(Utility.Int32Dbnull(objCongkham.IdKham, -1), objPatientExam.MaLuotkham,
                        ////                            Utility.Int32Dbnull(objPatientExam.IdBenhnhan, -1),
                        ////                            Utility.Int32Dbnull(objkcbcdkl.DoctorId, -1)).Execute();

                        //if (objCongkham != null)
                        //{
                        //    new Update(KcbDangkyKcb.Schema)
                        //        .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(DateTime.Now)
                        //        .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        //        .Set(KcbDangkyKcb.Columns.IpMaysua).EqualTo(globalVariables.gv_strIPAddress)
                        //        .Set(KcbDangkyKcb.Columns.TenMaysua).EqualTo(globalVariables.gv_strComputerName)
                        //        .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(objkcbcdkl.IdBacsikham)
                        //        .Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(objCongkham.TrangThai)
                        //        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(Utility.Int32Dbnull(objCongkham.IdKham, -1)).
                        //        Execute();
                        //}
                        //sh.Dispose();
                    }

                    scope.Complete();
                    //  Reg_ID = Utility.Int32Dbnull(objCongkham.IdKham, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh ket thuc benh nhan", exception);
                return ActionResult.Error;
            }
        }
       
        public ActionResult LockExamInfo(KcbLuotkham objPatientExam)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.Locked).EqualTo(objPatientExam.Locked)
                            .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(objPatientExam.NguoiKetthuc)
                            .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(objPatientExam.NgayKetthuc)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan).Execute();

                    }

                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh lưu phiếu điều trị {0}", exception);
                return ActionResult.Error;
            }
        }
        public DataTable NoitruTimkiembenhnhan(string regFrom, string regTo, string patientName, Int16 capcuu, string maluotkham, int DepartmentID, int tthaiBuonggiuong, int chuyenkhoa, int id_bacsi,byte trangthai,string huongdieutri)
        {
            return SPs.NoitruTimkiembenhnhan(DepartmentID, maluotkham, 1, regFrom, regTo, patientName, capcuu, tthaiBuonggiuong, chuyenkhoa, id_bacsi, trangthai, huongdieutri).
                    GetDataSet().Tables[0];
        }

        public DataTable LayDSachBnhanThamkham(DateTime regFrom, DateTime regTo,long id_benhnhan,string ma_luotkham, string patientName, int Status, int SoPhieu,string lstid_phongkham, string DepartmentID, string LoaiBN, string MaKhoaThucHien, Int16 id_dichvu, int id_chitietdichvu,string KQCD,string username)
        {
            return SPs.KcbThamkhamLaydanhsachBnhanChokham(regFrom, regTo,id_benhnhan,ma_luotkham, patientName, Status,
                                                      SoPhieu, lstid_phongkham,
                                                     DepartmentID,LoaiBN,
                                                      MaKhoaThucHien, id_dichvu, id_chitietdichvu, KQCD, username).
                    GetDataSet().Tables[0];
        }
        public DataTable KcbTracuuLskcb2019(DateTime regFrom, DateTime regTo, string patientName,long id_benhnhan, int Status, int SoPhieu, string DepartmentID, string LoaiBN, string MaKhoaThucHien)
        {
            return SPs.KcbTracuuLskcb2019(regFrom, regTo, patientName,id_benhnhan, Status,
                                                      SoPhieu,
                                                     DepartmentID, LoaiBN,
                                                      MaKhoaThucHien).
                    GetDataSet().Tables[0];
        }
        public DataTable LayDSachBnhanThamkhamTiemchung(DateTime regFrom, DateTime regTo, string patientName, int Status, int SoPhieu, int DepartmentID, string LoaiBN, string MaKhoaThucHien)
        {
            return SPs.KcbThamkhamLaydanhsachBnhanTiemchungChokham(regFrom, regTo, patientName, Status,
                                                      SoPhieu,
                                                     DepartmentID, LoaiBN,
                                                      MaKhoaThucHien).
                    GetDataSet().Tables[0];
        }
        public DataTable LayThongtinBenhnhanKCB(string PatientCode, int PatientID, int RegID)
        {
            return SPs.KcbThamkhamLaythongtinBenhnhankcb(PatientCode, PatientID, RegID).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruLaythongtinBenhnhan(string PatientCode, int PatientID)
        {
            return SPs.NoitruLaythongtinbenhnhan(PatientCode, PatientID).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemphieudieutriTheoluotkham( byte IsAdmin,string ngaylapphieu, string PatientCode, int PatientID, string idkhoanoitru, int songayhienthi)
        {
            return SPs.NoitruTimkiemphieudieutriTheoluotkham(globalVariables.UserName,IsAdmin,  ngaylapphieu, PatientCode, PatientID, idkhoanoitru, songayhienthi).
                                GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemlichsuBuonggiuong(string PatientCode, long PatientID, string idKhoanoitru, long idBG)
        {
            return SPs.NoitruTimkiemlichsuBuonggiuong( PatientCode, PatientID,idKhoanoitru,idBG).GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemlichsuNoptientamung(string PatientCode, int PatientID, short kieutamung, int idkhoanoitru, byte noitru)
        {
            return SPs.NoitruTimkiemlichsuNoptientamung(PatientCode, PatientID,kieutamung,idkhoanoitru,noitru).GetDataSet().Tables[0];
        }
        public DataTable KcbTimkiemthongtinMiengiam(string PatientCode, int PatientID, byte noitru)
        {
            return SPs.KcbTimkiemthongtinMiengiam(PatientCode, PatientID, noitru).GetDataSet().Tables[0];
        }
        public DataTable KcbTimkiemphieuThuchi(string PatientCode, int PatientID, short loai_phieu, int idkhoanoitru, byte noitru)
        {
            return SPs.KcbTimkiemphieuThuchi(PatientCode, PatientID, loai_phieu, idkhoanoitru, noitru).GetDataSet().Tables[0];
        }
        public DataTable TimkiemBenhnhan(string PatientCode, int DepartmentId,byte noitru, int Locked)
        {
            return SPs.KcbThamkhamTimkiembenhnhan(PatientCode, DepartmentId,noitru, Locked).
                            GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbTimkiemBenhnhan(DateTime tungay, DateTime denngay, string maluotkham, long idbenhnhan, string tenBenhnhan, string matheBhyt, int idbacsikham, string loaiBn)
        {
            return SPs.KcbLichsukcbTimkiembenhnhan(tungay, denngay, maluotkham, idbenhnhan, tenBenhnhan, matheBhyt, idbacsikham,loaiBn).GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbLuotkham(int? idbenhnhan)
        {
            return SPs.KcbLichsukcbLuotkham(idbenhnhan).GetDataSet().Tables[0];
        }
        public DataTable KcbLichsuKcbTimkiemphongkham(long id_benhnhan, string Maluotkham,byte khonglaycuamakhamhientai)
        {
            return SPs.KcbLichsukcbTimkiemphongkham(id_benhnhan, Maluotkham, khonglaycuamakhamhientai).GetDataSet().Tables[0];
        }

        public DataTable TimkiemThongtinBenhnhansaukhigoMaBN(string PatientCODE, int DepartmentId,string makhoathien)
        {
            return SPs.KcbThamkhamTimkiemBnhanSaukhinhapmabn(PatientCODE, DepartmentId, makhoathien)
                            .GetDataSet().Tables[0];
        }
        public DataTable NoitruTimkiemThongtinBenhnhansaukhigoMaBN(string PatientCODE, int DepartmentId, string makhoathien)
        {
            return SPs.NoitruTimkiemBnhanSaukhinhapmabn(PatientCODE, DepartmentId, makhoathien)
                            .GetDataSet().Tables[0];
        }
        public DataSet LaythongtinInphieuTtatDtriNgoaitru(long IdKham, string ma_luotkham)
        {
            return SPs.KcbThamkhamLaydulieuInphieuTtatDtriNgoaitru(IdKham,ma_luotkham).GetDataSet();
        }
        public DataSet LaythongtinInphieuTtatDtriNgoaitru_2023(long IdKham,long id_benhnhan, string ma_luotkham,byte id_gioitinh)
        {
            return SPs.KcbThamkhamLaydulieuInphieuTtatDtriNgoaitru2023(IdKham, id_benhnhan, ma_luotkham, id_gioitinh).GetDataSet();
        }
        public DataSet KcbThamkhamLayDanhsachPhieuChidinhCLSTheolankham(long PatientID, string PatientCode, int ExamID)
        {
            return SPs.KcbThamkhamLayDanhsachPhieuChidinhCLSTheolankham(PatientID, PatientCode, ExamID).GetDataSet();
        }
        public DataSet KcbThamkhamLayDanhsachDonThuocTheolankham(long PatientID, string PatientCode, long ExamID, long id_phieudieutri,byte kieu_donthuoc, string kieuthuoc_vt,long id_chitietchidinh,byte noitru)
        {
            return SPs.KcbThamkhamLayDanhsachDonThuocTheolankham(PatientID, PatientCode, ExamID, id_phieudieutri, kieu_donthuoc, kieuthuoc_vt,id_chitietchidinh, noitru).GetDataSet();
        }
        public DataSet LaythongtinThuocTheoCongkham(long PatientID, string PatientCode, long ExamID)
        {
            return SPs.KcbThamkhamLaythongtinthuocVtthLichsukcb(PatientID, PatientCode, ExamID).GetDataSet();
        }
        public DataSet LaythongtinCLSTheoCongkham(long PatientID, string PatientCode, long ExamID)
        {
            return SPs.KcbThamkhamLaythongtinclsLichsukcb(PatientID, PatientCode, ExamID).GetDataSet();
        }

        public DataSet LaythongtinCLSVaThuoc(long PatientID, string PatientCode, long ExamID)
        {
            return SPs.KcbThamkhamLaythongtinclsThuocTheolankham(PatientID, PatientCode, ExamID).GetDataSet();
        }
        public DataSet LayDanhsachdonthuoc(int PatientID, string PatientCode, int ExamID)
        {
            return SPs.KcbThamkhamLaythongtinclsThuocTheolankham(PatientID, PatientCode, ExamID).GetDataSet();
        }
        public DataSet NoitruLaythongtinclsThuocTheophieudieutri(int PatientID, string PatientCode, int idPhieudieutri, byte layca_dulieu_ngoaitru_chuathanhtoan, string idKhoanoitru)
        {
            return SPs.NoitruLaythongtinclsThuocTheophieudieutri(PatientID, PatientCode, idPhieudieutri, layca_dulieu_ngoaitru_chuathanhtoan,idKhoanoitru).GetDataSet();
        }
        public DataSet NoitruLayDanhsachVtthGoidichvu(int PatientID, string PatientCode)
        {
            return SPs.NoitruLaydanhsachVtthGoidichvu(PatientID, PatientCode).GetDataSet();
        }

        public DataSet InMauPhieuChuyenKhoa(string maluotkham, long idbenhnhan)
        {
            return SPs.ThamkhamInmauphieuChuyenkhoa(maluotkham, idbenhnhan).GetDataSet();
        }
        public DataSet NoitruLaythongtinVTTHTrongoi(int PatientID, string PatientCode, int idPhieudieutri, int Idgoi)
        {
            return SPs.NoitruLaythongtinvtthTrongoi(PatientID, PatientCode, idPhieudieutri,Idgoi).GetDataSet();
        }
        public DataSet KcbThamkhamLaydulieuInphieuCls(long PatientID, string PatientCode, string Machidinh, string nhomincls,string lstSelectedPrint)
        {
            return SPs.KcbThamkhamLaydulieuInphieuCls(Machidinh, nhomincls, PatientCode, PatientID, lstSelectedPrint).GetDataSet();
        }
        public DataTable ClsLaokhoaInphieuChidinhCls(string AssignCode, string PatientCode, int PatientID)
        {
            return SPs.KcbThamkhamLaythongtinclsInphieuTach(AssignCode, PatientCode,
                                                            PatientID).GetDataSet().
                    Tables[0];
        }
        public DataTable LaydanhsachBenh()
        {
            return new Select().From(DmucBenh.Schema).ExecuteDataSet().Tables[0];
        }
        public DataTable LayThongtinInphieuCLS(int AssingID, int ServicePrintType)
        {
            return SPs.KcbThamkhamLaythongtinclsInphieuChung(AssingID, ServicePrintType).GetDataSet().Tables[0];
        }
        
    }
}
