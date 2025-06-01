using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using NLog;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.BusRule.Classes
{
    /// <summary>
    /// </summary>
    public class KCB_THANHTOAN
    {
        private readonly Logger log;

        public KCB_THANHTOAN()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public DataTable LayDsachBenhnhanThanhtoan(int PatientID, string patient_code, string patientName,
            DateTime fromDate, DateTime toDate, string MaDoituongKcb, int BHYT, byte? noi_tru, string KieuTimKiem,
            string MAKHOATHIEN, string loaiBN)
        {
            return SPs.KcbThanhtoanLaydanhsachBenhnhanThanhtoan(PatientID,
                patient_code,
                patientName,
                fromDate,
                toDate,
                MaDoituongKcb, BHYT, noi_tru,
                KieuTimKiem, MAKHOATHIEN, loaiBN).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiDichvu(int? PaymentID, string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinInbienlaiDv(PaymentID, MaLuotkham, PatientID).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiBHYT(int? PaymentID, string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinInbienlaiBhyt(PaymentID, MaLuotkham, PatientID).GetDataSet().Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="id_donthuoc">phục vụ mục đích in tách phiếu xuất thuốc của biên lai thanh toán cho >=2 đơn thuốc nhưng yêu cầu in biên lai tại quầy thuốc phải tách theo từng đơn, từng kho</param>
        /// <param name="noitru"></param>
        /// <returns></returns>
        public DataTable LaythongtininbienlaiDichvu(KcbThanhtoan objThanhtoan,long id_donthuoc, byte noitru)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiDv2023(objThanhtoan.IdThanhtoan, id_donthuoc,objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan, noitru).GetDataSet().Tables[0];
        }
        public DataTable LaythongtininbienlaiDichvu_PhieuChi_Quaythuoc(KcbThanhtoan objThanhtoan, byte noitru)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiPhieuChiDv2023(objThanhtoan.IdThanhtoan,-1, objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan, noitru).GetDataSet().Tables[0];
        }
        public DataTable ThuocLaydulieuinphieutrathuoctaiquay(ThuocLichsuTralaithuoctaiquayPhieu objPhieu)
        {
            return
                SPs.ThuocLaydulieuinphieutrathuoctaiquay(objPhieu.IdTralaithuoc, objPhieu.MaLuotkham,
                    objPhieu.IdBenhnhan).GetDataSet().Tables[0];
        }
        public DataTable LaythongtininbienlaiDichvu_phieuchi(KcbThanhtoan objThanhtoan, byte noitru)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiDvPhieuchi(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan, noitru).GetDataSet().Tables[0];
        }

        public DataTable LaythongtininbienlaiBHYT(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInbienlaiBhyt(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham,
                    objThanhtoan.IdBenhnhan).GetDataSet().Tables[0];
        }

        public ActionResult HuyThanhtoan(KcbThanhtoan objPayment, KcbLuotkham objPatientExam, string lydohuy,
            int idHdonLog, bool huyBienlai)
        {
            try
            {
                //Kiểm tra trạng thái chốt thanh toán
                var thanhtoan = new Select().From(KcbThanhtoan.Schema).Where
                    (KcbThanhtoan.IdThanhtoanColumn.ColumnName).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoan.TrangthaiChotColumn.ColumnName).IsEqualTo(1).ExecuteSingle<KcbThanhtoan>();
                if (thanhtoan != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                var objChitiet = new Select().From(KcbThanhtoanChitiet.Schema)
                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(1).ExecuteSingle<KcbThanhtoanChitiet>();
                if (objChitiet != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã có chi tiết bị trả lại tiền. Mời bạn thực hiện hủy phiếu chi(phiếu trả lại tiền) trước khi thực hiện hủy thanh toán!");
                    return ActionResult.DataUsed; //Để ko hiển thị lại thông báo phía client
                }
                int reval = 0;
                StoredProcedure spcheck = SPs.KcbThanhtoanKiemtraHuyThanhtoan(objPayment.IdThanhtoan, reval);
                spcheck.Execute();
                reval = Utility.Int32Dbnull(spcheck.OutputValues[0], 0);
                if (reval == 1)
                {
                    Utility.ShowMsg("Lần thanh toán đang chọn có chứa dịch vụ khám đã được bác sĩ kết thúc khám nên bạn không được phép hủy thanh toán(Cần hủy kết thúc khám trước khi hủy thanh toán của công khám)");
                    return ActionResult.ExistedRecord;//Để ko hiển thị lại thông báo phía client
                }
                else if (reval == 2)
                {
                    Utility.ShowMsg("Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                    return ActionResult.ExistedRecord;
                }
                else if (reval == 3)
                {
                    Utility.ShowMsg("Lần thanh toán đang chọn có chứa chỉ định dịch vụ CLS đã có kết quả thực hiện nên bạn không được phép hủy thanh toán");
                    return ActionResult.ExistedRecord;
                }
                //Tạm mở đoạn kiểm tra từ chỗ khai báo reval và rem đoạn dưới. Chưa hiểu tạo sao trước kia chỉ sử dụng đoạn dưới kiểm tra mỗi đơn thuốc
                //DataTable dtKTra = KiemtraTrangthaidonthuocTruockhihuythanhtoan(objPayment.IdThanhtoan);
                //if (!Utility.Byte2Bool(objPayment.NoiTru) && dtKTra.Rows.Count > 0)
                //{
                //    Utility.ShowMsg(
                //        "Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                return HuyThongTinLanThanhToan(objPayment, objPatientExam, lydohuy, idHdonLog, huyBienlai);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Exception;
            }
        }
        public ActionResult HuyTamthu(KcbThanhtoan objPayment, KcbLuotkham objPatientExam, string lydohuy,
           int idHdonLog, bool huyBienlai)
        {
            try
            {
                //Kiểm tra trạng thái kết chuyển
                var thanhtoan = new Select().From(KcbThanhtoan.Schema).Where
                    (KcbThanhtoan.IdThanhtoanColumn.ColumnName).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoan.Columns.TrangThai).IsEqualTo(1).ExecuteSingle<KcbThanhtoan>();
                if (thanhtoan != null)
                {
                    Utility.ShowMsg(
                        "Phiếu tạm thu đã được kết chuyển thanh toán nên không thể hủy. Vui lòng kiểm tra lại!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                var objChitiet = new Select().From(KcbThanhtoanChitiet.Schema)
                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(1).ExecuteSingle<KcbThanhtoanChitiet>();
                if (objChitiet != null)
                {
                    Utility.ShowMsg(
                        "Phiếu tạm thu đang chọn đã có chi tiết bị trả lại tiền. Mời bạn thực hiện hủy phiếu chi(phiếu trả lại tiền) trước khi thực hiện hủy tạm thu!");
                    return ActionResult.DataUsed; //Để ko hiển thị lại thông báo phía client
                }
                int reval = 0;
                StoredProcedure spcheck = SPs.KcbThanhtoanKiemtraHuyThanhtoan(objPayment.IdThanhtoan, reval);
                spcheck.Execute();
                reval = Utility.Int32Dbnull(spcheck.OutputValues[0], 0);
                if (reval == 1)
                {
                    Utility.ShowMsg("Phiếu tạm thu đang chọn có chứa dịch vụ khám đã được bác sĩ kết thúc khám nên bạn không được phép hủy tạm thu(Cần hủy kết thúc khám trước khi hủy tạm thu của công khám)");
                    return ActionResult.ExistedRecord;//Để ko hiển thị lại thông báo phía client
                }
                else if (reval == 2)
                {
                    Utility.ShowMsg("Phiếu tạm thu đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy tạm thu");
                    return ActionResult.ExistedRecord;
                }
                else if (reval == 3)
                {
                    Utility.ShowMsg("Phiếu tạm thu đang chọn có chứa chỉ định dịch vụ CLS đã có kết quả thực hiện nên bạn không được phép hủy tạm thu");
                    return ActionResult.ExistedRecord;
                }
                //Tạm mở đoạn kiểm tra từ chỗ khai báo reval và rem đoạn dưới. Chưa hiểu tạo sao trước kia chỉ sử dụng đoạn dưới kiểm tra mỗi đơn thuốc
                //DataTable dtKTra = KiemtraTrangthaidonthuocTruockhihuythanhtoan(objPayment.IdThanhtoan);
                //if (!Utility.Byte2Bool(objPayment.NoiTru) && dtKTra.Rows.Count > 0)
                //{
                //    Utility.ShowMsg(
                //        "Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                return HuyThongTinTamthu(objPayment, objPatientExam, lydohuy, idHdonLog, huyBienlai);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Exception;
            }
        }
        public ActionResult HuyThanhtoanNoitru(KcbThanhtoan objPayment, KcbLuotkham objPatientExam, string lydohuy,
           int idHdonLog, bool huyBienlai)
        {
            try
            {
                //Kiểm tra trạng thái chốt thanh toán
                var thanhtoan = new Select().From(KcbThanhtoan.Schema).Where
                    (KcbThanhtoan.IdThanhtoanColumn.ColumnName).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoan.TrangthaiChotColumn.ColumnName).IsEqualTo(1).ExecuteSingle<KcbThanhtoan>();
                if (thanhtoan != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                var objChitiet = new Select().From(KcbThanhtoanChitiet.Schema)
                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPayment.IdThanhtoan)
                    .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(1).ExecuteSingle<KcbThanhtoanChitiet>();
                if (objChitiet != null)
                {
                    Utility.ShowMsg(
                        "Thanh toán đang chọn đã có chi tiết bị trả lại tiền. Mời bạn thực hiện hủy phiếu chi(phiếu trả lại tiền) trước khi thực hiện hủy thanh toán!");
                    return ActionResult.ExistedRecord; //Để ko hiển thị lại thông báo phía client
                }
                //int reval = 0;
                //StoredProcedure spcheck = SPs.KcbThanhtoanKiemtraHuyThanhtoan(objPayment.IdThanhtoan, reval);
                //spcheck.Execute();
                //reval = Utility.Int32Dbnull(spcheck.OutputValues[0], 0);
                //if (reval == 1)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa dịch vụ khám đã được bác sĩ thăm khám nên bạn không được phép hủy thanh toán");
                //    return ActionResult.ExistedRecord;//Để ko hiển thị lại thông báo phía client
                //}
                //else if (reval == 2)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                //else if (reval == 3)
                //{
                //    Utility.ShowMsg("Lần thanh toán đang chọn có chứa chỉ định dịch vụ CLS đã có kết quả thực hiện nên bạn không được phép hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                //Tạm mở đoạn kiểm tra từ chỗ khai báo reval và rem đoạn dưới. Chưa hiểu tạo sao trước kia chỉ sử dụng đoạn dưới kiểm tra mỗi đơn thuốc
                //DataTable dtKTra = KiemtraTrangthaidonthuocTruockhihuythanhtoan(objPayment.IdThanhtoan);
                //if (!Utility.Byte2Bool(objPayment.NoiTru) && dtKTra.Rows.Count > 0)
                //{
                //    Utility.ShowMsg(
                //        "Lần thanh toán đang chọn có chứa đơn thuốc đã được duyệt cấp phát. Bạn cần liên hệ bộ phận Dược hủy duyệt đơn thuốc trước khi hủy thanh toán");
                //    return ActionResult.ExistedRecord;
                //}
                return HuyThongTinLanThanhToan(objPayment, objPatientExam, lydohuy, idHdonLog, huyBienlai);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Exception;
            }
        }

        public ActionResult HuyThanhtoanDonthuoctaiquay(int v_id_thanhtoan, KcbLuotkham ObjPatientExam, string lydohuy,
            int IdHdonLog, bool HuyBienlai)
        {
            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                if (
                    Utility.AcceptQuestion(
                        string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_id_thanhtoan),
                        "Thông báo", true))
                {
                    return HuyThongTinLanThanhToan_Donthuoctaiquay(v_id_thanhtoan, ObjPatientExam, lydohuy, IdHdonLog,
                        HuyBienlai);
                }
                else
                {
                    return ActionResult.Cancel;
                }
            return HuyThongTinLanThanhToan_Donthuoctaiquay(v_id_thanhtoan, ObjPatientExam, lydohuy, IdHdonLog,
                HuyBienlai);
        }

        public DataTable LaythongtinBenhnhan(string MaLuotkham, int? PatientID)
        {
            return SPs.KcbThanhtoanLaythongtinBenhnhanThanhtoanTheomalankham(MaLuotkham,
                PatientID).GetDataSet().Tables[0];
        }

        public DataTable LayThongtinChuaThanhtoan(string MaLuotkham, int PatientID, int HosStatus,
            string MAKHOATHIEN, string MADOITUONG,string lstIdloaiThanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtindvuChuathanhtoan(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG, lstIdloaiThanhtoan).
                GetDataSet().Tables[0];
        }
        public DataTable LayThongtinChuaThanhtoaGoikhamn(string MaLuotkham, int PatientID, int HosStatus,
           string MAKHOATHIEN, string MADOITUONG, string lstIdloaiThanhtoan,long id_dangky,int id_goi)
        {
            return SPs.KcbThanhtoanLaythongtindvuChuathanhtoanGoikham(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG, lstIdloaiThanhtoan, id_dangky, id_goi).
                GetDataSet().Tables[0];
        }
        public DataTable LayThongtinChuaThanhtoan_CheckError(string MaLuotkham, int PatientID, int HosStatus,
          string MAKHOATHIEN, string MADOITUONG, string lstIdloaiThanhtoan,long id_thanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtindvuChuathanhtoanAdmin(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG, lstIdloaiThanhtoan, id_thanhtoan).
                GetDataSet().Tables[0];
        }

        public DataTable NoitruKcbThanhtoanLaythongtindvuChuathanhtoan(string MaLuotkham, int PatientID, int HosStatus,
            string MAKHOATHIEN, string MADOITUONG)
        {
            return SPs.NoitruKcbThanhtoanLaythongtindvuChuathanhtoan(MaLuotkham, PatientID, HosStatus,
                MAKHOATHIEN, MADOITUONG).
                GetDataSet().Tables[0];
        }

        public DataTable LayThongtinDaThanhtoan(string MaLuotkham, long PatientID, int HosStatus,string lstIdloaiThanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtindvuDathanhtoan(MaLuotkham, PatientID, HosStatus, lstIdloaiThanhtoan).
                GetDataSet().Tables[0];
        }

        public DataTable LayHoaDonCapPhat(string UserName)
        {
            return SPs.HoadondoLaydanhsachHoadonDacapphatTheouser(UserName).GetDataSet().Tables[0];
        }

        public DataTable LaythongtinCacLanthanhtoan(string maLuotkham, Int64 IdBenhnhan, int? kieuThanhToan,
            byte? noi_tru, byte Loaithanhtoan, string MA_KHOA_THIEN, string lstIdloaiThanhtoan,Int16 id_goi)
        {
            return SPs.KcbThanhtoanLaydanhsachCaclanthanhtoanTheobenhnhan(maLuotkham,
                IdBenhnhan, kieuThanhToan, noi_tru, Loaithanhtoan,
                MA_KHOA_THIEN, lstIdloaiThanhtoan, id_goi).GetDataSet().Tables[0];
        }

        public DataTable Laythongtinhoadondo(long PaymentID)
        {
            return SPs.HoadondoLaythongtinhoadonTheothanhtoan(PaymentID).GetDataSet().Tables[0];
        }
        public DataTable KcbThanhtoanLaydulieuthuchikhac(long idphieuthu)
        {
            return SPs.KcbThanhtoanLaydulieuthuchikhac(idphieuthu).GetDataSet().Tables[0];
        }
        public DataTable KcbThanhtoanLaythongtinphieuchi(long PaymentID)
        {
            return SPs.KcbThanhtoanLaythongtinphieuchi(PaymentID).GetDataSet().Tables[0];
        }

        public DataTable KtraXnhanthuoc(int IdThanhtoan)
        {
            return null; // SPs.DonthuocKiemtraxacnhanthuocTrongdon(IdThanhtoan).GetDataSet().Tables[0];
        }

        public DataTable LaythongtinInphoiBHYT(int PaymentID, string MaLuotkham, int? PatientID, int TuTuc)
        {
            return SPs.BhytLaythongtinInphoi(PaymentID, MaLuotkham, PatientID, TuTuc).GetDataSet().Tables[0];
        }
        public DataTable NoiTruLaythongtinInphoiBHYT(int PaymentID, string MaLuotkham, int? PatientID, int TuTuc)
        {
            return SPs.NoiTruBhytLaythongtinInphoi(PaymentID, MaLuotkham, PatientID, TuTuc).GetDataSet().Tables[0];
        }
        private string LayChiKhauChiTiet()
        {
            string PTramChiTiet = "KHONG";
            SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("PTRAM_CHITIET");
            var objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) PTramChiTiet = objSystemParameter.SValue;
            return PTramChiTiet;
        }

        public void XuLyChiKhauDacBietBHYT(KcbLuotkham objLuotkham, decimal ptramBHYT)
        {
            KcbThanhtoanCollection paymentCollection =
                new KcbThanhtoanController().FetchByQuery(
                    KcbThanhtoan.CreateQuery().AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                        objLuotkham.MaLuotkham).AND(KcbThanhtoan.Columns.IdBenhnhan,
                            Comparison.Equals,
                            objLuotkham.IdBenhnhan));
            foreach (KcbThanhtoan payment in paymentCollection)
            {
                KcbThanhtoanChitietCollection paymentDetailCollection =
                    new KcbThanhtoanChitietController().FetchByQuery(
                        KcbThanhtoanChitiet.CreateQuery()
                            .AddWhere(KcbThanhtoanChitiet.Columns.IdThanhtoan, Comparison.Equals, payment.IdThanhtoan)
                            .AND(KcbThanhtoanChitiet.Columns.TuTuc, Comparison.Equals, 0));
                string IsDungTuyen = "DT";
                switch (objLuotkham.MaDoituongKcb)
                {
                    case "BHYT":
                        IsDungTuyen = Utility.Int32Dbnull(objLuotkham.DungTuyen, "0") == 1 ? "DT" : "TT";
                        break;
                    default:
                        IsDungTuyen = "KHAC";
                        break;
                }
                foreach (KcbThanhtoanChitiet PaymentDetail in paymentDetailCollection)
                {
                    SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
                        .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(PaymentDetail.IdChitietdichvu)
                        .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentDetail.IdLoaithanhtoan)
                        .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
                        .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
                    var objDetailDiscountRate = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
                    if (objDetailDiscountRate != null)
                    {
                        log.Info("Neu trong ton tai trong bang cau hinh chi tiet chiet khau void Id_Chitiet=" +
                                 PaymentDetail.IdChitiet);
                        PaymentDetail.PtramBhyt = objDetailDiscountRate.TileGiam;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(objDetailDiscountRate.TileGiam,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(objDetailDiscountRate.TileGiam,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                    }
                    else
                    {
                        PaymentDetail.PtramBhyt = ptramBHYT;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBHYT,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(ptramBHYT,
                            Utility.DecimaltoDbnull(
                                PaymentDetail.DonGia, 0));
                    }
                    log.Info("Thuc hien viec cap nhap thong tin lai gia can phai xem lại gia truoc khi thanh toan");
                }
            }
        }

        private decimal TongtienKhongTutuc(List<KcbThanhtoanChitiet> lstPaymentDetail)
        {
            decimal sumOfPaymentDetail = 0;
            foreach (KcbThanhtoanChitiet paymentDetail in lstPaymentDetail)
            {
                if (paymentDetail.TuTuc == 0 && Utility.Byte2Bool(paymentDetail.TinhChiphi))
                    sumOfPaymentDetail += (Utility.Int32Dbnull(paymentDetail.SoLuong)* Utility.DecimaltoDbnull(paymentDetail.DonGia) * Utility.DecimaltoDbnull(paymentDetail.TyleTt,0)/100);
            }
            return sumOfPaymentDetail;
        }

        public decimal LayThongtinPtramBhyt(decimal vDecTotalMoney, KcbLuotkham objLuotkham, ref decimal PtramBHYT)
        {
            decimal tienBn = 0;
            decimal bhytPtramLuongcoban =
                Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_LUONGCOBAN", "0", false), 0);
            decimal bhytPtramTraituyennoitru =
                Utility.DecimaltoDbnull(
                    THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
            SqlQuery q;
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
            {
                ///thực hiện xem có đúng tuyến không

                if (objLuotkham.DungTuyen == 1)
                {
                    //Các đối tượng đặc biệt hưởng 100% BHYT
                    if (Utility.Byte2Bool(objLuotkham.GiayBhyt) || globalVariables.gv_strMaQuyenLoiHuongBHYT100Phantram.Contains(objLuotkham.MaQuyenloi.ToString()))
                        // objLuotkham.MaQuyenloi.ToString() == "1" || objLuotkham.MaQuyenloi.ToString() == "2")
                    {
                        tienBn = 0;
                        PtramBHYT = 100;
                        log.Info("Benh nhan tuong ung voi muc =" + objLuotkham.MaQuyenloi);
                    }
                    else
                    {
                        if (bhytPtramLuongcoban > 0)
                        {
                            if (vDecTotalMoney >= objLuotkham.LuongCoban*bhytPtramLuongcoban/100)
                            {
                                PtramBHYT = objLuotkham.TrangthaiNoitru <= 0
                                    ? Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)
                                    : Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                                tienBn = vDecTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100;
                                log.Info("Tổng tiền lớn hơn lương cơ bản*% quy định -->Bệnh nhân =" +
                                         objLuotkham.MaLuotkham + " phải trả " + tienBn);
                            }
                            else //Tổng tiền < lương cơ bản*% quy định-->BHYT chi trả 100%
                            {
                                PtramBHYT = 100;
                                tienBn = vDecTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100; //=0
                                log.Info(
                                    "BHYT chi trả 100% tiền dịch vụ do tổng tiền nhỏ hơn lương cơ bản*% quy định. Mã khám=" +
                                    objLuotkham.MaLuotkham);
                            }
                        }
                        else //Chưa khai báo % lương cơ bản-->Tính bình thường
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                            else
                                PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                            tienBn = vDecTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT, 0))/100;
                        }
                    }
                }
                else //Trái tuyến
                {
                    if (objLuotkham.TrangthaiNoitru <= 0)
                        PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    else
                        PtramBHYT = (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0)*bhytPtramTraituyennoitru)/
                                    100;
                    tienBn = vDecTotalMoney*(100 - Utility.DecimaltoDbnull(PtramBHYT))/100;
                }
            }
            else //Đối tượng dịch vụ--> PtramBhyt=0
            {
                //thường PtramBHYT=0%
                PtramBHYT = objLuotkham.TrangthaiNoitru <= 0
                    ? Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0)
                    : Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                tienBn = vDecTotalMoney*(100 - Utility.Int32Dbnull(PtramBHYT, 0))/100;
                ;
            }
            return tienBn;
        }

        public ActionResult ThanhtoanDonthuoctaiquay(KcbThanhtoan objThanhtoan, KcbDanhsachBenhnhan objBenhnhan,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref int id_thanhtoan, long IdHdonLog, bool Layhoadondo)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        ///lấy tổng số Payment của mang truyền vào của pay ment hiện tại
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                        KcbThanhtoanCollection paymentCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere
                                        //(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals, objLuotkham.MaLuotkham).AND
                                        (KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objBenhnhan.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.NoiTru, Comparison.Equals, 0)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                );
                        //Lấy tổng tiền của các lần thanh toán trước
                        long id_donthuoc = -1;
                        foreach (KcbThanhtoan Payment in paymentCollection)
                        {
                            var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                            {
                                if (id_donthuoc == -1) id_donthuoc = paymentDetail.IdPhieu;
                                if (paymentDetail.TuTuc == 0)
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                            Utility.DecimaltoDbnull(paymentDetail.DonGia);
                            }
                        }

                        //LayThongtinPtramBHYT(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        
                        if (id_donthuoc == -1) id_donthuoc = objArrPaymentDetail[0].IdPhieu;
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                        var lstChitiet =
                            new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc)
                                .IsEqualTo(id_donthuoc)
                                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        var actionResult = ActionResult.Success;
                        if (objDonthuoc != null && lstChitiet.Count > 0)
                        {
                            if (!XuatThuoc.InValiKiemTraDonThuoc(lstChitiet, 0))
                                return ActionResult.NotEnoughDrugInStock;
                            actionResult = new XuatThuoc().LinhThuocBenhNhanTaiQuay(id_donthuoc,null,
                                Utility.Int16Dbnull(lstChitiet[0].IdKho, 0), DateTime.Now);
                            switch (actionResult)
                            {
                                case ActionResult.Success:

                                    break;
                                case ActionResult.Error:
                                    return actionResult;
                            }
                        }
                        //Tính lại Bnhan chi trả và BHYT chi trả
                        //objArrPaymentDetail = THU_VIEN_CHUNG.TinhPhamTramBHYT(objArrPaymentDetail, PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                TT_BNCT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_BN += TT_PT + TT_BNCT + TT_TT;

                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objChitietThanhtoan.IsNew = true;
                            objChitietThanhtoan.Save();
                            UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                        }
                        objThanhtoan.TongTien = TT_BHYT + TT_BN;

                        #region Hoadondo

                        if (Layhoadondo)
                        {
                            int record = -1;
                            if (IdHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(IdHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan.ToString();
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = DateTime.Now;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            IdHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                        }

                        #endregion

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0);
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        objPhieuthu.NgayThuchien = DateTime.Now;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    id_thanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public void TinhlaitienBhyTtruocThanhtoan(DataTable mDtData, KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref decimal PtramBHYT)
        {
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
            {
                PtramBHYT = 0;
                //tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal vDblTongtienDCT = 0;
                //tổng tiền đã thanh toán
                decimal vTotalPaymentDetail = 0;
                vDblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                KcbThanhtoanCollection paymentCollection =
                    new KcbThanhtoanController()
                        .FetchByQuery(
                            KcbThanhtoan.CreateQuery()
                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals, objLuotkham.MaLuotkham)
                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, objThanhtoan.KieuThanhtoan)
                        //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                        );
                //Lấy tổng tiền của các lần thanh toán trước
                var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();

                foreach (KcbThanhtoan Payment in paymentCollection)
                {
                    var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                        .And(KcbThanhtoanChitiet.Columns.NoiTru).IsEqualTo(objThanhtoan.NoiTru)
                        .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                        <KcbThanhtoanChitietCollection>();

                    foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                    {
                        if (paymentDetail.TuTuc == 0)
                        {
                            lstKcbThanhtoanChitiet.Add(paymentDetail);
                            paymentDetail.IsNew = false;
                            paymentDetail.MarkOld();
                            vTotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                   Utility.DecimaltoDbnull(paymentDetail.DonGia) * Utility.DecimaltoDbnull(paymentDetail.TyleTt)/100;
                        }
                    }
                }
                List<long> lstIdThanhtoan = (from q in lstKcbThanhtoanChitiet
                    select q.IdThanhtoan).ToList<long>();
                //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                //Phần trăm này có thể bị biến đổi và khác với % trong các bảng dịch vụ
                LayThongtinPtramBhyt(vDblTongtienDCT + vTotalPaymentDetail, objLuotkham, ref PtramBHYT);
                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, mDtData, PtramBHYT);
            }
        }
        public ActionResult ThanhtoanChiphiDvuKcb_FixError(long id_thanhtoan, List<KcbThanhtoanChitiet> objArrPaymentDetail, ref string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            objChitietThanhtoan.IdThanhtoan = id_thanhtoan;
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);

                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// Tạm thu các dịch vụ với đơn giá = đơn giá gốc kể cả với đối tượng BHYT
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <param name="idThanhtoan"></param>
        /// <param name="idHdonLog"></param>
        /// <param name="layhoadondo"></param>
        /// <param name="tongtienBNchitra"></param>
        /// <returns></returns>
        public ActionResult TamthuChiphiDvuKcb(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, List<KcbChietkhau> lstChietkhau, ref long idThanhtoan, long idHdonLog, bool layhoadondo, bool bo_ckchitiet, string ma_uudai,
            ref decimal tongtienBNchitra, ref string ErrMsg)
        {
            ErrMsg = "";
            decimal ptramBhyt = 0;
           
            //tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.PtramBhyt = 0;//Phiếu tạm thu nên không quan tâm
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                        objThanhtoan.MaCoso = objLuotkham.MaCoso;
                        StoredProcedure sp = SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                              objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                              objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                              , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                              objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                              objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                              , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                              objThanhtoan.PtramBhyt
                              , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                              objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                              objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                              , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                              objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                              objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                              , objThanhtoan.MaPttt, objThanhtoan.MaNganhang, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                              objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                              objThanhtoan.MaLydoHuy, objThanhtoan.TtoanThuoc, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.MaxNgayTao, objThanhtoan.Ghichu, objThanhtoan.LydoChietkhau, objThanhtoan.MaCoso
                              ,objThanhtoan.IdCtrinhKhuyenmai,objThanhtoan.MaVoucher,objThanhtoan.TienChietkhauVoucher);
                        sp.Execute();
                        objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                       
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                           
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                 , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);

                            StoredProcedure spupdate = SPs.SpUpdateTrangthaitamthu(objChitietThanhtoan.IdLoaithanhtoan,
                   objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.NoiTru,
                   objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                   objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                   objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao,0);
                            reval = spupdate.Execute();
                            if (reval <= 0)
                            {
                                ErrMsg = string.Format("Cập nhật thông tin tạm thu không thành công dịch vụ loại {0} với id_phieu={1},id_phieuchitiet={2}", objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet);
                                Utility.Log("Tạm thu", globalVariables.UserName, ErrMsg, newaction.Error, "frm_THANHTOAN_NGOAITRU");
                                return ActionResult.Cancel;
                            }
                        }
                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , DateTime.Now, globalVariables.UserName, "Thu tiền bệnh nhân", objThanhtoan.TongTien, objThanhtoan.TongTien, objThanhtoan.MaLydoChietkhau, 0
                            , objThanhtoan.TongtienChietkhau, 0, 1,
                            "", "", Convert.ToByte(5), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
                            , objThanhtoan.MaPttt, "NB", objThanhtoan.MaNganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.TienChietkhauVoucher).Execute();
                       
                       
                        decimal HU = 0m;
                        
                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objThanhtoan.MaPttt, objThanhtoan.MaNganhang,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, objThanhtoan.TongTien,objThanhtoan.TongTien,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao, -1l, 0, (byte)1).Execute();
                       ////Bỏ các dòng dưới do đây là thanh toán tạm thu
                        //int _reval =
                        //    SPs.SpKcbLuotkhamTrangthaithanhtoan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                        //        Utility.Int16Dbnull(objLuotkham.TrangthaiNoitru, 0), Utility.Int16Dbnull(objThanhtoan.NoiTru, 0), objThanhtoan.NgayThanhtoan,
                        //        objLuotkham.MaDoituongKcb).Execute();
                        //if (_reval <= 0)
                        //{
                        //    ErrMsg =
                        //            string.Format(
                        //                "Chưa update thành công trạng thái thanh toán của bệnh nhân {0}",
                        //                objLuotkham.MaLuotkham);
                        //}


                    }
                    scope.Complete();
                    idThanhtoan = Utility.Int64Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <param name="idThanhtoan"></param>
        /// <param name="idHdonLog"></param>
        /// <param name="layhoadondo"></param>
        /// <param name="tongtienBNchitra"></param>
        /// <returns></returns>
        public ActionResult ThanhtoanChiphiDvuKcb_bak_20240904(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, List<long> lstIdTamthu,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, List<KcbChietkhau> lstChietkhau,ref long idThanhtoan, long idHdonLog, bool layhoadondo,bool bo_ckchitiet,string ma_uudai,
            ref decimal tongtienBNchitra, ref string ErrMsg)
        {
            ErrMsg = "";
            decimal ptramBhyt = 0;
            //tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            //tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                       
                        //Tính tổng tiền đồng chi trả
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                );
                        List<long> lstIdThanhtoan = (from q in lstKcbThanhtoanCollection
                            select q.IdThanhtoan).Distinct().ToList();
                        //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();
                        if (lstIdThanhtoan.Count > 0)
                            lstKcbThanhtoanChitiet =
                                new Select().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoan)
                                    .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();

                        v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet
                            where p.TuTuc == 0
                            select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia*Utility.DecimaltoDbnull(c.TyleTt,0)/100);

                        //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                        //Phần trăm này có thể bị biến đổi và khác với % trong các bảng dịch vụ
                        LayThongtinPtramBhyt(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref ptramBhyt);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.PtramBhyt = Utility.Int16Dbnull(ptramBhyt, 0);
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                        objThanhtoan.MaCoso = objLuotkham.MaCoso;
                      StoredProcedure sp =   SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan,objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                            objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                            objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                            , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                            objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                            objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                            , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                            objThanhtoan.PtramBhyt
                            , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                            objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                            objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                            , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                            objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                            objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                            , objThanhtoan.MaPttt, objThanhtoan.MaNganhang, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                            objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                            objThanhtoan.MaLydoHuy, objThanhtoan.TtoanThuoc, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.MaxNgayTao, objThanhtoan.Ghichu, objThanhtoan.LydoChietkhau, objThanhtoan.MaCoso
                            , objThanhtoan.IdCtrinhKhuyenmai, objThanhtoan.MaVoucher, objThanhtoan.TienChietkhauVoucher);
                        sp.Execute();
                        objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);

                        #region Hoàn ứng
                        if (Utility.Int32Dbnull(objThanhtoan.IdGoi, 0) <= 0)//Thanh toán cho gói ko gọi các hàm liên quan đến hoàn ứng
                        {
                            if (Utility.Byte2Bool(objThanhtoan.NoiTru))
                            {
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0",
                                        false) == "1")
                                {
                                    string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                                    globalVariables.MaphieuHoanung = maphieu;
                                    int arc = SPs.NoitruHoanung(objThanhtoan.IdThanhtoan, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                        objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                        globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, 0), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                                             Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 1, objThanhtoan.MaPttt, objThanhtoan.MaNganhang).Execute();
                                    if (arc <= 0) globalVariables.MaphieuHoanung = "";
                                }
                            }
                            else
                            {
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0",
                                        false) == "1")
                                    if (
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                            "NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "1")
                                    {
                                        string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                                        globalVariables.MaphieuHoanung = maphieu;
                                        int arc = SPs.NoitruHoanung(objThanhtoan.IdThanhtoan, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                              objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                              globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, 0), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                                               Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 0, objThanhtoan.MaPttt, objThanhtoan.MaNganhang).Execute();
                                        if (arc <= 0) globalVariables.MaphieuHoanung = "";
                                    }
                            }
                        }
                        #endregion
                        //objThanhtoan.IsNew = true;
                        //objThanhtoan.Save();
                        //Tính lại Bnhan chi trả và BHYT chi trả theo % BHYT mới
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref objArrPaymentDetail, ref lstKcbThanhtoanChitiet,
                            ptramBhyt);
                        decimal ttBn = 0m;
                        decimal ttBnct = 0m;
                        decimal ttPt = 0m;
                        decimal ttTt = 0m;
                        decimal ttBhyt = 0m;
                        decimal ttChietkhauChitiet = 0m;

                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TINHLAI_TOANBODICHVU", "1", false) == "1")
                        {
                            foreach (int IdThanhtoan in lstIdThanhtoan)
                            {
                                ttBn = 0m;
                                ttBhyt = 0m;
                                ttPt = 0m;
                                ttTt = 0m;
                                ttBnct = 0m;
                                ttChietkhauChitiet = 0m;
                                List<KcbThanhtoanChitiet> _LstChitiet = (from q in lstKcbThanhtoanChitiet
                                    where q.IdThanhtoan == IdThanhtoan
                                    select q).ToList<KcbThanhtoanChitiet>();

                                if (_LstChitiet.Count > 0)
                                {
                                    foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                    {
                                        if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            ttBhyt += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        ttChietkhauChitiet +=
                                            Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                        ttPt += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            ttTt += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        else
                                            ttBnct += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);

                                        objChitietThanhtoan.IsNew = false;
                                        objChitietThanhtoan.MarkOld();
                                        objChitietThanhtoan.Save();
                                    }
                                    ttBn += ttPt + ttBnct + ttTt;
                                    //Update lại tiền thanh toán
                                    new Update(KcbThanhtoan.Schema)
                                        .Set(KcbThanhtoan.Columns.TongTien).EqualTo(ttBhyt + ttBn)
                                        .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(ttBnct)
                                        .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(ttBhyt)
                                        .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(ttPt)
                                        .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(ttTt)
                                        .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                                        .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                                        .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(idHdonLog)
                                        .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    //Update phiếu thu
                                    new Update(KcbPhieuthu.Schema)
                                        .Set(KcbPhieuthu.Columns.SoTien).EqualTo(ttBn - ttChietkhauChitiet)
                                        .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(ttBn)
                                        .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                }
                            }
                        }
                        //Reset để không bị cộng dồn với các thanh toán cũ
                        ttBn = 0m;
                        ttBhyt = 0m;
                        ttPt = 0m;
                        ttTt = 0m;
                        ttBnct = 0m;
                        ttChietkhauChitiet = 0m;
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttBhyt += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            decimal tienck_chitiet=Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            ttChietkhauChitiet += tienck_chitiet;
                            ttPt += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttTt += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                ttBnct += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong,objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                 , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);
                            //Bổ sung các bản tin miễn giảm chi tiết
                            if (tienck_chitiet > 0)
                            {
                                KcbChietkhau newck = new KcbChietkhau();
                                newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                newck.MaLuotkham = objThanhtoan.MaLuotkham;
                                newck.IdThanhtoan = objThanhtoan.IdThanhtoan ;
                                newck.SoTien = tienck_chitiet;
                                newck.NoiTru = objThanhtoan.NoiTru;
                                newck.TrangThai = true;
                                newck.NgayMiengiam = objThanhtoan.NgayThanhtoan;
                                newck.IdChitietThanhtoan = objChitietThanhtoan.IdChitiet;
                                newck.TienChuack = objChitietThanhtoan.BnhanChitra;
                                newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                                newck.LydoChietkhau = objThanhtoan.LydoChietkhau;
                                newck.KieuChietkhau = objChitietThanhtoan.KieuChietkhau;
                                newck.TileChietkhau =Utility.DecimaltoDbnull( objChitietThanhtoan.TileChietkhau,0);
                                newck.MaUudai = ma_uudai;
                                newck.BoChitiet = bo_ckchitiet;
                                newck.NguoiTao = objThanhtoan.NguoiTao;
                                newck.NgayTao = objThanhtoan.NgayTao;
                                newck.IsNew = true;
                                newck.Save();
                            }
                            //objChitietThanhtoan.IsNew = true;
                            //objChitietThanhtoan.Save();

                            //reval = UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                            StoredProcedure spupdate = SPs.SpUpdateTrangthaithanhtoan(objChitietThanhtoan.IdLoaithanhtoan,
                   objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.NoiTru,
                   objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                   objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                   objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao);
                            reval = spupdate.Execute();
                            if (reval <= 0)
                            {
                                ErrMsg = string.Format("Cập nhật không thành công dịch vụ loại {0} với id_phieu={1},id_phieuchitiet={2}", objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet);
                                Utility.Log("Thanh toán", globalVariables.UserName, ErrMsg, newaction.Error, "frm_THANHTOAN_NGOAITRU");
                                return ActionResult.Cancel;
                            }
                            //Tạm bỏ mục này vì khi thanh toán gói đẻ gây lỗi hủy do ko tìm thấy bản ghi cập nhật trong các bảng
                            //if (reval <= 0)
                            //{
                            //    ErrMsg =
                            //        string.Format("Dịch vụ {0} đã bị người dùng khác hủy bỏ nên bạn không thể thanh toán. Hãy nhấn nút OK và chọn lại Bệnh nhân để lấy lại các chi phí thanh toán mới nhất",objChitietThanhtoan.TenChitietdichvu);
                            //    return ActionResult.Cancel;
                            //}
                        }
                        //Tạo dữ liệu miễn giảm tổng trên toàn hóa đơn
                        
                        if (Utility.DecimaltoDbnull( objThanhtoan.TongtienChietkhauHoadon,0) > 0)
                        {
                            KcbChietkhau newck = new KcbChietkhau();
                            newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            newck.MaLuotkham = objThanhtoan.MaLuotkham;
                            newck.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            newck.SoTien = Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhauHoadon, 0);
                            newck.NoiTru = objThanhtoan.NoiTru;
                            newck.TrangThai = true;
                            newck.IdChitietThanhtoan = -1;
                            newck.TienChuack = objThanhtoan.TongTien;
                            newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                            newck.LydoChietkhau = objThanhtoan.LydoChietkhau;
                            newck.KieuChietkhau = objThanhtoan.KieuChietkhau;
                            newck.TileChietkhau = objThanhtoan.TileChietkhau;
                            newck.MaUudai = ma_uudai;
                            newck.BoChitiet = bo_ckchitiet;
                            newck.NgayMiengiam = objThanhtoan.NgayThanhtoan;
                            newck.NguoiTao = objThanhtoan.NguoiTao;
                            newck.NgayTao = objThanhtoan.NgayTao;
                            newck.IsNew = true;
                            newck.Save();
                        }
                        if (lstChietkhau != null)
                        {
                            foreach (KcbChietkhau _item in lstChietkhau)
                            {
                                _item.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                _item.MaLuotkham = objThanhtoan.MaLuotkham;
                                _item.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                _item.MaUudai = ma_uudai;
                                _item.IsNew = true;
                                _item.Save();
                            }
                        }
                        ttBn += ttPt + ttBnct + ttTt;
                        tongtienBNchitra = ttBn;
                        objThanhtoan.TongTien = ttBn + ttBhyt;

                        #region Hoadondo

                        if (layhoadondo)
                        {
                            int record = -1;
                            if (idHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(idHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan.ToString();
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = DateTime.Now;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            idHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            //update bảng HoadonCapphat
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                            // hàm thực hiện việc khóa hóa đơn đỏ đã dùng
                            new Update(SysHoadonMau.Schema)
                 .Set(SysHoadonMau.Columns.KhoaLai).EqualTo(1)
                 .Where(SysHoadonMau.Columns.MauHoadon).IsEqualTo(objThanhtoan.MauHoadon)
                 .And(SysHoadonMau.Columns.MaQuyen).IsEqualTo(objThanhtoan.MaQuyen)
                 .And(SysHoadonMau.Columns.KiHieu).IsEqualTo(objThanhtoan.KiHieu)
                 .And(SysHoadonMau.Columns.SerieHientai).IsEqualTo(Utility.Int32Dbnull(objThanhtoan.Serie)).Execute();
                            SqlQuery sqlQuery = new Select().From<HoadonMau>().Where(HoadonMau.Columns.MauHoadon).IsEqualTo(objThanhtoan.MauHoadon);
                            HoadonMau objHoadonMau = sqlQuery.ExecuteSingle<HoadonMau>();
                            if (objHoadonMau != null)
                            {
                                if (Utility.Int32Dbnull(objHoadonMau.SerieCuoi) <= Utility.Int32Dbnull(objThanhtoan.Serie))
                                {
                                    objHoadonMau.MarkOld();
                                    objHoadonMau.TrangThai = 2; //nếu seris  vượt quá thi khoa lại
                                    objHoadonMau.Save();

                                }

                            }
                        }

                        #endregion

                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , DateTime.Now, globalVariables.UserName, "Thu tiền bệnh nhân",
                            ttBn - ttChietkhauChitiet, ttBn, objThanhtoan.MaLydoChietkhau, ttChietkhauChitiet
                            , objThanhtoan.TongtienChietkhau, objThanhtoan.TongtienChietkhau - ttChietkhauChitiet, 1,
                            "", "", Convert.ToByte(0), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
                            , objThanhtoan.MaPttt, "NB", objThanhtoan.MaNganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong, objThanhtoan.IdGoi, objThanhtoan.IdDangky,objThanhtoan.TienChietkhauVoucher).Execute();
                        #region PhieuThuOld
                        //KcbPhieuthu objPhieuthu = new KcbPhieuthu();
                        //objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0);
                        //objPhieuthu.SoluongChungtugoc = 1;
                        //objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        //objPhieuthu.NgayThuchien = DateTime.Now;
                        //objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhieuthu.SotienGoc = TT_BN;
                        //objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        //objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        //objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        //objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        //objPhieuthu.NguoiNop = globalVariables.UserName;
                        //objPhieuthu.TaikhoanCo = "";
                        //objPhieuthu.TaikhoanNo = "";
                        //objPhieuthu.NoiTru = (byte)objThanhtoan.NoiTru;
                        //objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        //objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        //objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        //objPhieuthu.IsNew = true;
                        //objPhieuthu.Save();
                        #endregion
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(ttBhyt + ttBn)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(ttBnct)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(ttBhyt)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(ttPt)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(ttTt)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(idHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        decimal HU = 0m;
                        if (Utility.Int32Dbnull(objThanhtoan.IdGoi, 0) <= 0)//Thanh toán cho gói ko gọi các hàm liên quan đến hoàn ứng
                        {
                            //Lấy tiền hoàn ứng để trừ đi trong phần phân bổ
                            DataTable dtData = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                                .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1).ExecuteDataSet().Tables[0];

                            if (dtData.Rows.Count > 0)
                                HU = Utility.DecimaltoDbnull(dtData.Rows[0]["so_tien"], 0);
                        }
                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan,-1l,-1l, objThanhtoan.MaPttt,objThanhtoan.MaNganhang,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, ttBn - objThanhtoan.TongtienChietkhau-HU, ttBn - objThanhtoan.TongtienChietkhau-HU,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao,-1l,0,(byte)1).Execute();
                        //new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                        //    .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //var objPhanbotienTT = new KcbThanhtoanPhanbotheoPTTT();
                        //objPhanbotienTT.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhanbotienTT.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhanbotienTT.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhanbotienTT.MaPttt = objThanhtoan.MaPttt;
                        //objPhanbotienTT.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhanbotienTT.NoiTru = objThanhtoan.NoiTru;
                        //objPhanbotienTT.TongTien = objPhanbotienTT.SoTien;
                        //objPhanbotienTT.NguoiTao = objThanhtoan.NguoiTao;
                        //objPhanbotienTT.NgayTao = objThanhtoan.NgayTao;
                        //objPhanbotienTT.IsNew = true;
                        //objPhanbotienTT.Save();
                        int _reval =
                            SPs.SpKcbLuotkhamTrangthaithanhtoan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                Utility.Int16Dbnull(objLuotkham.TrangthaiNoitru,0), Utility.Int16Dbnull(objThanhtoan.NoiTru,0), objThanhtoan.NgayThanhtoan, objLuotkham.MucHuongBhyt,
                                objLuotkham.MaDoituongKcb).Execute();
                        if (_reval <= 0)
                        {
                            ErrMsg =
                                    string.Format(
                                        "Chưa update thành công trạng thái thanh toán của bệnh nhân {0}",
                                        objLuotkham.MaLuotkham);
                        }
                        //Update trạng thái các bản ghi tạm thu được kết chuyển trong lần thanh toán này
                        if (lstIdTamthu != null)
                        {
                            foreach (long id_tt in lstIdTamthu)
                            {
                                new Update(KcbThanhtoan.Schema).Set(KcbThanhtoan.Columns.TrangThai).EqualTo(1).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                                new Update(KcbPhieuthu.Schema).Set(KcbPhieuthu.Columns.TrangThai).EqualTo(1).Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                                //Thêm bản ghi thanh toán ứng với nhiều kết chuyển tạm thu
                                QheThanhtoanTamthu tttt = new QheThanhtoanTamthu();
                                tttt.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                tttt.IdTamthu = id_tt;
                                tttt.IsNew = true;
                                tttt.Save();
                            }
                        }
                        //if (Utility.Byte2Bool(objThanhtoan.NoiTru) &&
                        //    Utility.ByteDbnull(objLuotkham.TrangthaiNoitru, 0) >= 2)
                        //    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.TthaiThanhtoannoitru).EqualTo(1)
                        //        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        //        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //        .Execute();

                        
                    }
                    scope.Complete();
                    idThanhtoan = Utility.Int64Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public ActionResult ThanhtoanChiphiDvuKcb(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, List<long> lstIdTamthu,
          List<KcbThanhtoanChitiet> objArrPaymentDetail, List<KcbChietkhau> lstChietkhau, ref long idThanhtoan, long idHdonLog, bool layhoadondo, bool bo_ckchitiet, string ma_uudai,
          ref decimal tongtienBNchitra, ref string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                        objThanhtoan.MaCoso = objLuotkham.MaCoso;
                        StoredProcedure sp = SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                              objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                              objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                              , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                              objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                              objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                              , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                              objThanhtoan.PtramBhyt
                              , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                              objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                              objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                              , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                              objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                              objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                              , objThanhtoan.MaPttt, objThanhtoan.MaNganhang, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                              objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                              objThanhtoan.MaLydoHuy, objThanhtoan.TtoanThuoc, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.MaxNgayTao, objThanhtoan.Ghichu, objThanhtoan.LydoChietkhau, objThanhtoan.MaCoso
                              , objThanhtoan.IdCtrinhKhuyenmai, objThanhtoan.MaVoucher, objThanhtoan.TienChietkhauVoucher);
                        sp.Execute();
                        objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);

                        #region Hoàn ứng
                        if (Utility.Int32Dbnull(objThanhtoan.IdGoi, 0) <= 0)//Thanh toán cho gói ko gọi các hàm liên quan đến hoàn ứng
                        {
                            if (Utility.Byte2Bool(objThanhtoan.NoiTru))
                            {
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0",
                                        false) == "1")
                                {
                                    string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                                    globalVariables.MaphieuHoanung = maphieu;
                                    int arc = SPs.NoitruHoanung(objThanhtoan.IdThanhtoan, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                        objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                        globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, 0), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                                             Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 1, objThanhtoan.MaPttt, objThanhtoan.MaNganhang).Execute();
                                    if (arc <= 0) globalVariables.MaphieuHoanung = "";
                                }
                            }
                            else
                            {
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0",
                                        false) == "1")
                                    if (
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                            "NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "1")
                                    {
                                        string maphieu = THU_VIEN_CHUNG.SinhmaVienphi("HKQ");
                                        globalVariables.MaphieuHoanung = maphieu;
                                        int arc = SPs.NoitruHoanung(objThanhtoan.IdThanhtoan, maphieu, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                              objThanhtoan.NgayThanhtoan, globalVariables.gv_intIDNhanvien,
                                              globalVariables.UserName, Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, 0), Utility.Int64Dbnull(objLuotkham.IdRavien, -1),
                                               Utility.Int16Dbnull(objLuotkham.IdBuong, 0), Utility.Int16Dbnull(objLuotkham.IdGiuong, 0), 0, objThanhtoan.MaPttt, objThanhtoan.MaNganhang).Execute();
                                        if (arc <= 0) globalVariables.MaphieuHoanung = "";
                                    }
                            }
                        }
                        #endregion
                        //objThanhtoan.IsNew = true;
                        //objThanhtoan.Save();
                        decimal ttBn = 0m;
                        decimal ttBnct = 0m;
                        decimal ttPt = 0m;
                        decimal ttTt = 0m;
                        decimal ttBhyt = 0m;
                        decimal ttChietkhauChitiet = 0m;
                        
                        //Reset để không bị cộng dồn với các thanh toán cũ
                        ttBn = 0m;
                        ttBhyt = 0m;
                        ttPt = 0m;
                        ttTt = 0m;
                        ttBnct = 0m;
                        ttChietkhauChitiet = 0m;
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttBhyt += objChitietThanhtoan.BhytChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            decimal tienck_chitiet = Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            ttChietkhauChitiet += tienck_chitiet;
                            ttPt += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttTt += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                ttBnct += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                 , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);
                            //Bổ sung các bản tin miễn giảm chi tiết
                            if (tienck_chitiet > 0)
                            {
                                KcbChietkhau newck = new KcbChietkhau();
                                newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                newck.MaLuotkham = objThanhtoan.MaLuotkham;
                                newck.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                newck.SoTien = tienck_chitiet;
                                newck.NoiTru = objThanhtoan.NoiTru;
                                newck.TrangThai = true;
                                newck.NgayMiengiam = objThanhtoan.NgayThanhtoan;
                                newck.IdChitietThanhtoan = objChitietThanhtoan.IdChitiet;
                                newck.TienChuack = objChitietThanhtoan.BnhanChitra;
                                newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                                newck.LydoChietkhau = objThanhtoan.LydoChietkhau;
                                newck.KieuChietkhau = objChitietThanhtoan.KieuChietkhau;
                                newck.TileChietkhau = Utility.DecimaltoDbnull(objChitietThanhtoan.TileChietkhau, 0);
                                newck.MaUudai = ma_uudai;
                                newck.BoChitiet = bo_ckchitiet;
                                newck.NguoiTao = objThanhtoan.NguoiTao;
                                newck.NgayTao = objThanhtoan.NgayTao;
                                newck.IsNew = true;
                                newck.Save();
                            }
                            //objChitietThanhtoan.IsNew = true;
                            //objChitietThanhtoan.Save();

                            //reval = UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                            StoredProcedure spupdate = SPs.SpUpdateTrangthaithanhtoan(objChitietThanhtoan.IdLoaithanhtoan,
                   objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.NoiTru,
                   objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                   objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                   objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao);
                            reval = spupdate.Execute();
                            if (reval <= 0)
                            {
                                ErrMsg = string.Format("Cập nhật không thành công dịch vụ loại {0} với id_phieu={1},id_phieuchitiet={2}", objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet);
                                Utility.Log("Thanh toán", globalVariables.UserName, ErrMsg, newaction.Error, "frm_THANHTOAN_NGOAITRU");
                                return ActionResult.Cancel;
                            }
                            
                        }
                        //Tạo dữ liệu miễn giảm tổng trên toàn hóa đơn

                        if (Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhauHoadon, 0) > 0)
                        {
                            KcbChietkhau newck = new KcbChietkhau();
                            newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            newck.MaLuotkham = objThanhtoan.MaLuotkham;
                            newck.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            newck.SoTien = Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhauHoadon, 0);
                            newck.NoiTru = objThanhtoan.NoiTru;
                            newck.TrangThai = true;
                            newck.IdChitietThanhtoan = -1;
                            newck.TienChuack = objThanhtoan.TongTien;
                            newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                            newck.LydoChietkhau = objThanhtoan.LydoChietkhau;
                            newck.KieuChietkhau = objThanhtoan.KieuChietkhau;
                            newck.TileChietkhau = objThanhtoan.TileChietkhau;
                            newck.MaUudai = ma_uudai;
                            newck.BoChitiet = bo_ckchitiet;
                            newck.NgayMiengiam = objThanhtoan.NgayThanhtoan;
                            newck.NguoiTao = objThanhtoan.NguoiTao;
                            newck.NgayTao = objThanhtoan.NgayTao;
                            newck.IsNew = true;
                            newck.Save();
                        }
                        if (lstChietkhau != null)
                        {
                            foreach (KcbChietkhau _item in lstChietkhau)
                            {
                                _item.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                _item.MaLuotkham = objThanhtoan.MaLuotkham;
                                _item.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                _item.MaUudai = ma_uudai;
                                _item.IsNew = true;
                                _item.Save();
                            }
                        }
                        ttBn += ttPt + ttBnct + ttTt;
                        tongtienBNchitra = ttBn;
                        objThanhtoan.TongTien = ttBn + ttBhyt;

                        #region Hoadondo

                        if (layhoadondo)
                        {
                            int record = -1;
                            if (idHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(idHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan.ToString();
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = DateTime.Now;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            idHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            //update bảng HoadonCapphat
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                            // hàm thực hiện việc khóa hóa đơn đỏ đã dùng
                            new Update(SysHoadonMau.Schema)
                 .Set(SysHoadonMau.Columns.KhoaLai).EqualTo(1)
                 .Where(SysHoadonMau.Columns.MauHoadon).IsEqualTo(objThanhtoan.MauHoadon)
                 .And(SysHoadonMau.Columns.MaQuyen).IsEqualTo(objThanhtoan.MaQuyen)
                 .And(SysHoadonMau.Columns.KiHieu).IsEqualTo(objThanhtoan.KiHieu)
                 .And(SysHoadonMau.Columns.SerieHientai).IsEqualTo(Utility.Int32Dbnull(objThanhtoan.Serie)).Execute();
                            SqlQuery sqlQuery = new Select().From<HoadonMau>().Where(HoadonMau.Columns.MauHoadon).IsEqualTo(objThanhtoan.MauHoadon);
                            HoadonMau objHoadonMau = sqlQuery.ExecuteSingle<HoadonMau>();
                            if (objHoadonMau != null)
                            {
                                if (Utility.Int32Dbnull(objHoadonMau.SerieCuoi) <= Utility.Int32Dbnull(objThanhtoan.Serie))
                                {
                                    objHoadonMau.MarkOld();
                                    objHoadonMau.TrangThai = 2; //nếu seris  vượt quá thi khoa lại
                                    objHoadonMau.Save();

                                }

                            }
                        }

                        #endregion

                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , DateTime.Now, globalVariables.UserName, "Thu tiền bệnh nhân",
                            ttBn - ttChietkhauChitiet, ttBn, objThanhtoan.MaLydoChietkhau, ttChietkhauChitiet
                            , objThanhtoan.TongtienChietkhau, objThanhtoan.TongtienChietkhau - ttChietkhauChitiet, 1,
                            "", "", Convert.ToByte(0), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
                            , objThanhtoan.MaPttt, "NB", objThanhtoan.MaNganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.TienChietkhauVoucher).Execute();
                        
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(ttBhyt + ttBn)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(ttBnct)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(ttBhyt)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(ttPt)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(ttTt)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.MucHuongBhyt).EqualTo(objLuotkham.MucHuongBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(idHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        decimal HU = 0m;
                        if (Utility.Int32Dbnull(objThanhtoan.IdGoi, 0) <= 0)//Thanh toán cho gói ko gọi các hàm liên quan đến hoàn ứng
                        {
                            //Lấy tiền hoàn ứng để trừ đi trong phần phân bổ
                            DataTable dtData = new Select().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                                .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1).ExecuteDataSet().Tables[0];

                            if (dtData.Rows.Count > 0)
                                HU = Utility.DecimaltoDbnull(dtData.Rows[0]["so_tien"], 0);
                        }
                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objThanhtoan.MaPttt, objThanhtoan.MaNganhang,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, ttBn - objThanhtoan.TongtienChietkhau - HU, ttBn - objThanhtoan.TongtienChietkhau - HU,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao, -1l, 0, (byte)1).Execute();
                        int _reval =
                            SPs.SpKcbLuotkhamTrangthaithanhtoan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                Utility.Int16Dbnull(objLuotkham.TrangthaiNoitru, 0), Utility.Int16Dbnull(objThanhtoan.NoiTru, 0), objThanhtoan.NgayThanhtoan, objLuotkham.MucHuongBhyt,
                                objLuotkham.MaDoituongKcb).Execute();
                        if (_reval <= 0)
                        {
                            ErrMsg = string.Format("Chưa update thành công trạng thái thanh toán của bệnh nhân {0}", objLuotkham.MaLuotkham);
                        }
                        //Update trạng thái các bản ghi tạm thu được kết chuyển trong lần thanh toán này
                        if (lstIdTamthu != null)
                        {
                            foreach (long id_tt in lstIdTamthu)
                            {
                                new Update(KcbThanhtoan.Schema).Set(KcbThanhtoan.Columns.TrangThai).EqualTo(1).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                                new Update(KcbPhieuthu.Schema).Set(KcbPhieuthu.Columns.TrangThai).EqualTo(1).Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                                //Thêm bản ghi thanh toán ứng với nhiều kết chuyển tạm thu
                                QheThanhtoanTamthu tttt = new QheThanhtoanTamthu();
                                tttt.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                tttt.IdTamthu = id_tt;
                                tttt.IsNew = true;
                                tttt.Save();
                            }
                        }
                       
                    }
                    scope.Complete();
                    idThanhtoan = Utility.Int64Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// Thanh toán chi tiết thuộc gói khám
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <param name="lstChietkhau"></param>
        /// <param name="idThanhtoan"></param>
        /// <param name="idHdonLog"></param>
        /// <param name="layhoadondo"></param>
        /// <param name="bo_ckchitiet"></param>
        /// <param name="ma_uudai"></param>
        /// <param name="tongtienBNchitra"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public ActionResult ThanhtoanChiphiDvuKcb_Goikham(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, List<KcbChietkhau> lstChietkhau, ref long idThanhtoan, long idHdonLog, bool layhoadondo, bool bo_ckchitiet, string ma_uudai,
            ref decimal tongtienBNchitra, ref string ErrMsg)
        {
            ErrMsg = "";
            decimal ptramBhyt = 0;
            //tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.PtramBhyt = Utility.Int16Dbnull(ptramBhyt, 0);
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                        objThanhtoan.MaCoso = objLuotkham.MaCoso;
                        StoredProcedure sp = SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                              objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                              objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                              , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                              objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                              objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                              , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                              objThanhtoan.PtramBhyt
                              , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                              objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                              objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                              , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                              objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                              objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                              , objThanhtoan.MaPttt, objThanhtoan.MaNganhang, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                              objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                              objThanhtoan.MaLydoHuy, objThanhtoan.TtoanThuoc, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.MaxNgayTao, objThanhtoan.Ghichu, objThanhtoan.LydoChietkhau, objThanhtoan.MaCoso
                              , objThanhtoan.IdCtrinhKhuyenmai, objThanhtoan.MaVoucher, objThanhtoan.TienChietkhauVoucher);
                        if (objThanhtoan.IdThanhtoan <= 0)
                        {
                            //sp.Execute();
                            //objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            objThanhtoan.IsNew = true;
                            objThanhtoan.Save();
                        }
                        else
                        {
                            //Không cần tạo bản ghi thanh toán-->Tập trung tạo chi tiết bên dưới
                        }

                      
                        decimal ttBn = 0m;
                        decimal ttBnct = 0m;
                        decimal ttPt = 0m;
                        decimal ttTt = 0m;
                        decimal ttBhyt = 0m;
                        decimal ttChietkhauChitiet = 0m;

                        //Reset để không bị cộng dồn với các thanh toán cũ
                        ttBn = 0m;
                        ttBhyt = 0m;
                        ttPt = 0m;
                        ttTt = 0m;
                        ttBnct = 0m;
                        ttChietkhauChitiet = 0m;
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttBhyt += objChitietThanhtoan.BhytChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            decimal tienck_chitiet = Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            ttChietkhauChitiet += tienck_chitiet;
                            ttPt += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttTt += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                ttBnct += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objChitietThanhtoan.IdGoi = objThanhtoan.IdGoi;
                            objChitietThanhtoan.IdDangky = objThanhtoan.IdDangky;
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                 , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);
                            //objChitietThanhtoan.IsNew = true;
                            //objChitietThanhtoan.Save();
                            //Bỏ Bổ sung các bản tin miễn giảm chi tiết---->
                            //Cập nhật trạng thái thanh toán cho các chi tiết thanh toán
                            StoredProcedure spupdate = SPs.SpUpdateTrangthaithanhtoan(objChitietThanhtoan.IdLoaithanhtoan,
                   objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.NoiTru,
                   objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                   objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                   objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao);
                            reval = spupdate.Execute();
                            if (reval <= 0)
                            {
                                ErrMsg = string.Format("Cập nhật không thành công dịch vụ loại {0} với id_phieu={1},id_phieuchitiet={2}", objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet);
                                Utility.Log("Thanh toán", globalVariables.UserName, ErrMsg, newaction.Error, "frm_THANHTOAN_NGOAITRU");
                                return ActionResult.Cancel;
                            }

                        }
                        //Bỏ Tạo dữ liệu miễn giảm tổng trên toàn hóa đơn
                        //Xóa các thứ liên quan đến hóa đơn đỏ
                        //............................
                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , DateTime.Now, globalVariables.UserName, "Bản ghi thanh toán tự động khi đăng ký dịch vụ trong gói",
                            ttBn - ttChietkhauChitiet, ttBn, objThanhtoan.MaLydoChietkhau, ttChietkhauChitiet
                            , objThanhtoan.TongtienChietkhau, objThanhtoan.TongtienChietkhau - ttChietkhauChitiet, 1,
                            "", "", Convert.ToByte(0), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
                            , objThanhtoan.MaPttt, "NB", objThanhtoan.MaNganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong, objThanhtoan.IdGoi, objThanhtoan.IdDangky,objThanhtoan.TienChietkhauVoucher).Execute();
                       
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(ttBhyt + ttBn)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(ttBnct)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(ttBhyt)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(ttPt)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(ttTt)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(idHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        decimal HU = 0m;
                       
                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objThanhtoan.MaPttt, objThanhtoan.MaNganhang,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, ttBn - objThanhtoan.TongtienChietkhau - HU, ttBn - objThanhtoan.TongtienChietkhau - HU,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao, -1l, 0, (byte)1).Execute();
                       //Bỏ update trạng thái thanh toán của bảng lượt khám
                    }
                    scope.Complete();
                    idThanhtoan = Utility.Int64Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }
        public ActionResult ThanhtoanThuoctaiquay_V2(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
           List<KcbThanhtoanChitiet> objArrPaymentDetail, List<KcbChietkhau> lstChietkhau, ref long idThanhtoan, long idHdonLog, bool layhoadondo, bool bo_ckchitiet, string ma_uudai,
           ref decimal tongtienBNchitra,int id_kho, ref string ErrMsg)
        {
            ErrMsg = "";
            decimal ptramBhyt = 0;
            //tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            //tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                );
                        List<long> lstIdThanhtoan = (from q in lstKcbThanhtoanCollection
                                                     select q.IdThanhtoan).Distinct().ToList();
                        //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();
                        if (lstIdThanhtoan.Count > 0)
                            lstKcbThanhtoanChitiet =
                                new Select().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoan)
                                    .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();

                        v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet
                                                where p.TuTuc == 0
                                                select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong) * c.DonGia * Utility.DecimaltoDbnull(c.TyleTt, 0) / 100);
                        List<long> lstIDChitiet = (from p in objArrPaymentDetail.AsEnumerable()
                                                   select  p.IdPhieuChitiet).ToList<long>();
                        //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                        //Phần trăm này có thể bị biến đổi và khác với % trong các bảng dịch vụ
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.PtramBhyt = Utility.Int16Dbnull(ptramBhyt, 0);
                        objThanhtoan.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                        objThanhtoan.MaCoso = objLuotkham.MaCoso;
                        StoredProcedure sp = SPs.SpKcbThanhtoanInsert(objThanhtoan.IdThanhtoan, objThanhtoan.MaLuotkham, objThanhtoan.IdBenhnhan,
                              objThanhtoan.MaDoituongKcb, objThanhtoan.IdDoituongKcb, objThanhtoan.IdLoaidoituongKcb,
                              objThanhtoan.NgayThanhtoan, objThanhtoan.IdNhanvienThanhtoan, objThanhtoan.MaThanhtoan
                              , objThanhtoan.KieuThanhtoan, objThanhtoan.TrangthaiIn, objThanhtoan.NgayIn,
                              objThanhtoan.NguoiIn, objThanhtoan.NgayTonghop, objThanhtoan.NguoiTonghop,
                              objThanhtoan.MaKhoaThuchien, objThanhtoan.NgayChot, objThanhtoan.TrangthaiChot
                              , objThanhtoan.TongTien, objThanhtoan.BhytChitra, objThanhtoan.BnhanChitra,
                              objThanhtoan.PtramBhyt
                              , objThanhtoan.TileChietkhau, objThanhtoan.KieuChietkhau, objThanhtoan.TongtienChietkhau,
                              objThanhtoan.TongtienChietkhauHoadon, objThanhtoan.TongtienChietkhauChitiet,
                              objThanhtoan.MaLydoChietkhau, objThanhtoan.NguoiTao, objThanhtoan.NgayTao
                              , objThanhtoan.NguoiSua, objThanhtoan.NgaySua, objThanhtoan.IdCapphat,
                              objThanhtoan.MauHoadon, objThanhtoan.KiHieu, objThanhtoan.MaQuyen, objThanhtoan.Serie,
                              objThanhtoan.TrangthaiSeri, objThanhtoan.IdHdonLog, objThanhtoan.NoiTru
                              , objThanhtoan.MaPttt,objThanhtoan.MaNganhang, objThanhtoan.IpMaytao, objThanhtoan.IpMaysua, objThanhtoan.TenMaytao,
                              objThanhtoan.TenMaysua, objThanhtoan.NgayRavien, objThanhtoan.PhuThu, objThanhtoan.TuTuc,
                              objThanhtoan.MaLydoHuy, objThanhtoan.TtoanThuoc, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.MaxNgayTao, objThanhtoan.Ghichu, objThanhtoan.LydoChietkhau, objThanhtoan.MaCoso
                              , objThanhtoan.IdCtrinhKhuyenmai, objThanhtoan.MaVoucher, objThanhtoan.TienChietkhauVoucher);
                        sp.Execute();
                        objThanhtoan.IdThanhtoan = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                        //Thực hiện cấp phát thuốc
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_TUDONG_CAPPHATTHUOC_KHITHANHTOANTAIQUAY", "0", true) == "1")
                        {
                            long id_donthuoc = -1;
                            if (id_donthuoc == -1) id_donthuoc = objArrPaymentDetail[0].IdPhieu;
                            KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                            KcbDonthuocChitietCollection lstChitiet =
                                new Select().From(KcbDonthuocChitiet.Schema)
                                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(id_donthuoc)
                                    .And(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstIDChitiet)
                                     .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                                    .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0)
                                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                            var actionResult = ActionResult.Success;
                            if (objDonthuoc != null && lstChitiet.Count > 0)
                            {
                                if (!XuatThuoc.InValiKiemTraDonThuoc(lstChitiet, 0))
                                    return ActionResult.NotEnoughDrugInStock;
                                actionResult = new XuatThuoc().LinhThuocBenhNhanTaiQuay(id_donthuoc,lstChitiet,
                                    Utility.Int16Dbnull(lstChitiet[0].IdKho, 0), DateTime.Now);
                                switch (actionResult)
                                {
                                    case ActionResult.Success:

                                        break;
                                    case ActionResult.Error:
                                        return actionResult;
                                }
                            }
                        }
                        //Kết thúc cấp phát thuốc
                        decimal ttBn = 0m;
                        decimal ttBnct = 0m;
                        decimal ttPt = 0m;
                        decimal ttTt = 0m;
                        decimal ttBhyt = 0m;
                        decimal ttChietkhauChitiet = 0m;
                        int reval = -1;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttBhyt += objChitietThanhtoan.BhytChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            decimal tienck_chitiet = Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            ttChietkhauChitiet += tienck_chitiet;
                            ttPt += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                ttTt += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            else
                                ttBnct += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            StoredProcedure spchitiet = SPs.SpKcbThanhtoanChitietInsert(objChitietThanhtoan.IdChitiet,
                                objChitietThanhtoan.IdThanhtoan, objChitietThanhtoan.MadoituongGia,
                                objChitietThanhtoan.PtramBhytGoc, objChitietThanhtoan.PtramBhyt,
                                objChitietThanhtoan.SoLuong, objChitietThanhtoan.GiaGoc, objChitietThanhtoan.DonGia, objChitietThanhtoan.TyleTt, objChitietThanhtoan.BnhanChitra,
                                objChitietThanhtoan.BhytChitra, objChitietThanhtoan.PhuThu
                                , objChitietThanhtoan.TuTuc, objChitietThanhtoan.IdPhieu,
                                objChitietThanhtoan.IdPhieuChitiet, objChitietThanhtoan.IdDichvu,
                                objChitietThanhtoan.IdChitietdichvu, objChitietThanhtoan.TenChitietdichvu,
                                objChitietThanhtoan.TenBhyt, objChitietThanhtoan.DonviTinh, objChitietThanhtoan.SttIn,
                                objChitietThanhtoan.IdKhoakcb, objChitietThanhtoan.IdPhongkham,
                                objChitietThanhtoan.IdBacsiChidinh
                                , objChitietThanhtoan.IdLoaithanhtoan, objChitietThanhtoan.TenLoaithanhtoan,
                                objChitietThanhtoan.MaDoituongKcb, objChitietThanhtoan.KieuChietkhau,
                                objChitietThanhtoan.TileChietkhau, objChitietThanhtoan.TienChietkhau,
                                objChitietThanhtoan.TrangthaiHuy, objChitietThanhtoan.TrangthaiBhyt,
                                objChitietThanhtoan.TrangthaiChuyen, objChitietThanhtoan.TinhChiphi,
                                objChitietThanhtoan.NoiTru, objChitietThanhtoan.IdGoi
                                , objChitietThanhtoan.TrongGoi, objChitietThanhtoan.IdKham, objChitietThanhtoan.NguonGoc,
                                objChitietThanhtoan.IdThanhtoanhuy, objChitietThanhtoan.IdLichsuDoituongKcb,
                                objChitietThanhtoan.MatheBhyt, objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao, objChitietThanhtoan.TinhChkhau, objChitietThanhtoan.CkNguongt, objChitietThanhtoan.UserTao
                                , objChitietThanhtoan.MultiCancel, objChitietThanhtoan.IdThe, objChitietThanhtoan.IdDangky, objChitietThanhtoan.BhytNguonKhac, objChitietThanhtoan.BhytGiaTyle, objChitietThanhtoan.BnTtt, objChitietThanhtoan.BnCct
                                 , objChitietThanhtoan.TienKhuyenmai, objChitietThanhtoan.TthaiKhuyenmai);
                            spchitiet.Execute();
                            objChitietThanhtoan.IdChitiet = Utility.Int64Dbnull(spchitiet.OutputValues[0], -1);
                            //Bổ sung các bản tin miễn giảm chi tiết
                            if (tienck_chitiet > 0)
                            {
                                KcbChietkhau newck = new KcbChietkhau();
                                newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                newck.MaLuotkham = objThanhtoan.MaLuotkham;
                                newck.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                newck.SoTien = tienck_chitiet;
                                newck.NoiTru = objThanhtoan.NoiTru;
                                newck.TrangThai = true;
                                newck.IdChitietThanhtoan = objChitietThanhtoan.IdChitiet;
                                newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                                newck.KieuChietkhau = objChitietThanhtoan.KieuChietkhau;
                                newck.TileChietkhau = objThanhtoan.TileChietkhau;
                                newck.MaUudai = ma_uudai;
                                newck.BoChitiet = bo_ckchitiet;
                                newck.NguoiTao = objThanhtoan.NguoiTao;
                                newck.NgayTao = objThanhtoan.NgayTao;
                                newck.IsNew = true;
                                newck.Save();
                            }
                            reval = UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                            if (reval <= 0)
                            {
                                ErrMsg =
                                    string.Format("Dịch vụ {0} đã bị người dùng khác hủy bỏ nên bạn không thể thanh toán. Hãy nhấn nút OK và chọn lại Bệnh nhân để lấy lại các chi phí thanh toán mới nhất", objChitietThanhtoan.TenChitietdichvu);
                                return ActionResult.Cancel;
                            }
                        }
                        //Tạo dữ liệu miễn giảm tổng trên toàn hóa đơn

                        if (Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhauHoadon, 0) > 0)
                        {
                            KcbChietkhau newck = new KcbChietkhau();
                            newck.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            newck.MaLuotkham = objThanhtoan.MaLuotkham;
                            newck.IdThanhtoan = objThanhtoan.IdThanhtoan;
                            newck.SoTien = Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhauHoadon, 0);
                            newck.NoiTru = objThanhtoan.NoiTru;
                            newck.TrangThai = true;
                            newck.IdChitietThanhtoan = -1;
                            newck.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                            newck.KieuChietkhau = objThanhtoan.KieuChietkhau;
                            newck.TileChietkhau = objThanhtoan.TileChietkhau;
                            newck.MaUudai = ma_uudai;
                            newck.BoChitiet = bo_ckchitiet;
                            newck.NguoiTao = objThanhtoan.NguoiTao;
                            newck.NgayTao = objThanhtoan.NgayTao;
                            newck.IsNew = true;
                            newck.Save();
                        }
                        if (lstChietkhau != null)
                        {
                            foreach (KcbChietkhau _item in lstChietkhau)
                            {
                                _item.IdBenhnhan = objThanhtoan.IdBenhnhan;
                                _item.MaLuotkham = objThanhtoan.MaLuotkham;
                                _item.IdThanhtoan = objThanhtoan.IdThanhtoan;
                                _item.MaUudai = ma_uudai;
                                _item.IsNew = true;
                                _item.Save();
                            }
                        }
                        ttBn += ttPt + ttBnct + ttTt;
                        tongtienBNchitra = ttBn;
                        objThanhtoan.TongTien = ttBn + ttBhyt;


                        SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0),
                            objThanhtoan.IdThanhtoan, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham
                            , DateTime.Now, globalVariables.UserName, "Thu tiền bệnh nhân",
                            ttBn - ttChietkhauChitiet, ttBn, objThanhtoan.MaLydoChietkhau, ttChietkhauChitiet
                            , objThanhtoan.TongtienChietkhau, objThanhtoan.TongtienChietkhau - ttChietkhauChitiet, 1,
                            "", "", Convert.ToByte(0), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
                            , objThanhtoan.NoiTru, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
                            , objThanhtoan.MaPttt, "NB", objThanhtoan.MaNganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong, objThanhtoan.IdGoi, objThanhtoan.IdDangky, objThanhtoan.TienChietkhauVoucher).Execute();
                        #region PhieuThuOld
                        //KcbPhieuthu objPhieuthu = new KcbPhieuthu();
                        //objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0);
                        //objPhieuthu.SoluongChungtugoc = 1;
                        //objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        //objPhieuthu.NgayThuchien = DateTime.Now;
                        //objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhieuthu.SotienGoc = TT_BN;
                        //objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        //objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        //objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        //objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        //objPhieuthu.NguoiNop = globalVariables.UserName;
                        //objPhieuthu.TaikhoanCo = "";
                        //objPhieuthu.TaikhoanNo = "";
                        //objPhieuthu.NoiTru = (byte)objThanhtoan.NoiTru;
                        //objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        //objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        //objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        //objPhieuthu.IsNew = true;
                        //objPhieuthu.Save();
                        #endregion
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(ttBhyt + ttBn)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(ttBnct)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(ttBhyt)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(ttPt)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(ttTt)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(idHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan,-1l,-1l, objThanhtoan.MaPttt,objThanhtoan.MaNganhang,
                            objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
                            objThanhtoan.NoiTru, ttBn - ttChietkhauChitiet, ttBn - ttChietkhauChitiet,
                            objThanhtoan.NguoiTao, objThanhtoan.NgayTao, "", objThanhtoan.NgayTao,-1l,0,(byte)1).Execute();
                        //new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                        //    .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //var objPhanbotienTT = new KcbThanhtoanPhanbotheoPTTT();
                        //objPhanbotienTT.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        //objPhanbotienTT.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        //objPhanbotienTT.MaLuotkham = objThanhtoan.MaLuotkham;
                        //objPhanbotienTT.MaPttt = objThanhtoan.MaPttt;
                        //objPhanbotienTT.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        //objPhanbotienTT.NoiTru = objThanhtoan.NoiTru;
                        //objPhanbotienTT.TongTien = objPhanbotienTT.SoTien;
                        //objPhanbotienTT.NguoiTao = objThanhtoan.NguoiTao;
                        //objPhanbotienTT.NgayTao = objThanhtoan.NgayTao;
                        //objPhanbotienTT.IsNew = true;
                        //objPhanbotienTT.Save();
                        int _reval =
                            SPs.SpKcbLuotkhamTrangthaithanhtoan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                Utility.Int16Dbnull(objLuotkham.TrangthaiNoitru, 0), Utility.Int16Dbnull(objThanhtoan.NoiTru, 0), objThanhtoan.NgayThanhtoan, objLuotkham.MucHuongBhyt,
                                objLuotkham.MaDoituongKcb).Execute();
                        if (_reval <= 0)
                        {
                            ErrMsg =
                                    string.Format(
                                        "Chưa update thành công trạng thái thanh toán của bệnh nhân {0}",
                                        objLuotkham.MaLuotkham);
                        }

                        //if (Utility.Byte2Bool(objThanhtoan.NoiTru) &&
                        //    Utility.ByteDbnull(objLuotkham.TrangthaiNoitru, 0) >= 2)
                        //    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.TthaiThanhtoannoitru).EqualTo(1)
                        //        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        //        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //        .Execute();


                    }
                    scope.Complete();
                    idThanhtoan = Utility.Int64Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                idThanhtoan = -1;
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }
        public ActionResult ThanhtoanChiphiDVuKCB_Ao(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham,
            List<KcbThanhtoanChitiet> objArrPaymentDetail, ref int id_thanhtoan, long IdHdonLog, bool Layhoadondo)
        {
            decimal PtramBHYT = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal v_dblTongtienDCT = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        ///lấy tổng số Payment của mang truyền vào của pay ment hiện tại
                        v_dblTongtienDCT = TongtienKhongTutuc(objArrPaymentDetail);
                        KcbThanhtoanCollection paymentCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                            objThanhtoan.KieuThanhtoan)
                                );
                        //Lấy tổng tiền của các lần thanh toán trước
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();
                        foreach (KcbThanhtoan Payment in paymentCollection)
                        {
                            var paymentDetailCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0).ExecuteAsCollection
                                <KcbThanhtoanChitietCollection>();

                            foreach (KcbThanhtoanChitiet paymentDetail in paymentDetailCollection)
                            {
                                if (paymentDetail.TuTuc == 0)
                                {
                                    lstKcbThanhtoanChitiet.Add(paymentDetail);

                                    v_TotalPaymentDetail += Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                                            Utility.DecimaltoDbnull(paymentDetail.DonGia);
                                }
                            }
                        }


                        //Tính toán lại phần trăm BHYT chủ yếu liên quan đến phần lương cơ bản. 
                        //Phần trăm này có thể bị biến đổi và khác với % trong bảng lượt khám
                        LayThongtinPtramBhyt(v_dblTongtienDCT + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                        objThanhtoan.MaThanhtoan =
                            THU_VIEN_CHUNG.TaoMathanhtoan(Convert.ToDateTime(objThanhtoan.NgayThanhtoan));
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        //Tính lại Bnhan chi trả và BHYT chi trả
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref objArrPaymentDetail, ref lstKcbThanhtoanChitiet,
                            PtramBHYT);
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in lstKcbThanhtoanChitiet)
                        {
                            objChitietThanhtoan.IsNew = false;
                            objChitietThanhtoan.MarkOld();
                            objChitietThanhtoan.Save();
                        }
                        decimal TT_BN = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in objArrPaymentDetail)
                        {
                            TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                     Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                            objChitietThanhtoan.IdThanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                            objChitietThanhtoan.IsNew = true;
                            objChitietThanhtoan.Save();
                            UpdatePaymentStatus(objThanhtoan, objChitietThanhtoan);
                        }

                        #region Hoadondo

                        if (Layhoadondo)
                        {
                            int record = -1;
                            if (IdHdonLog > 0)
                            {
                                record =
                                    new Delete().From(HoadonLog.Schema)
                                        .Where(HoadonLog.Columns.IdHdonLog)
                                        .IsEqualTo(IdHdonLog)
                                        .Execute();
                                if (record <= 0)
                                {
                                    Utility.ShowMsg(
                                        "Có lỗi trong quá trình xóa thông tin serie hóa đơn đã hủy để cấp lại cho lần thanh toán này.");
                                    return ActionResult.Error;
                                }
                            }
                            var obj = new HoadonLog();
                            obj.IdThanhtoan = objThanhtoan.IdThanhtoan.ToString();
                            obj.TongTien = objThanhtoan.TongTien -
                                           Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                            obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            obj.MaLuotkham = objThanhtoan.MaLuotkham;
                            obj.MauHoadon = objThanhtoan.MauHoadon;
                            obj.KiHieu = objThanhtoan.KiHieu;
                            obj.IdCapphat = objThanhtoan.IdCapphat.Value;
                            obj.MaQuyen = objThanhtoan.MaQuyen;
                            obj.Serie = objThanhtoan.Serie;
                            obj.MaNhanvien = globalVariables.UserName;
                            obj.MaLydo = "0";
                            obj.NgayIn = DateTime.Now;
                            obj.TrangThai = 0;
                            obj.IsNew = true;
                            obj.Save();
                            IdHdonLog = obj.IdHdonLog; //Để update lại vào bảng thanh toán
                            new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                                .EqualTo(objThanhtoan.Serie)
                                .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                                .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(obj.IdCapphat)
                                .Execute();
                        }

                        #endregion

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 0);
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(0);
                        objPhieuthu.NgayThuchien = DateTime.Now;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objThanhtoan.KieuThanhtoan;

                        objPhieuthu.LydoNop = "Thu tiền bệnh nhân";
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();

                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BN)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                            .Set(KcbThanhtoan.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                            .Set(KcbThanhtoan.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(IdHdonLog)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    id_thanhtoan = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi thuc hien thanh toan:" + ex);
                return ActionResult.Error;
            }
        }

        public ActionResult LayHoadondo(long id_thanhtoan, string MauHoadon, string KiHieu, string MaQuyen, string Serie,
            int IdCapphat, long IdHdonLog_huy, ref long IdHdonLog)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objThanhtoan == null) return ActionResult.Error;

                        if (IdHdonLog_huy > 0)
                        {
                            int record =
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog)
                                    .IsEqualTo(IdHdonLog_huy)
                                    .Execute();
                        }

                        var obj = new HoadonLog();
                        obj.IdThanhtoan = objThanhtoan.IdThanhtoan.ToString();
                        obj.TongTien = objThanhtoan.TongTien -
                                       Utility.DecimaltoDbnull(objThanhtoan.TongtienChietkhau, 0);
                        obj.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        obj.MaLuotkham = objThanhtoan.MaLuotkham;
                        obj.MauHoadon = MauHoadon;
                        obj.KiHieu = KiHieu;
                        obj.IdCapphat = IdCapphat;
                        obj.MaQuyen = MaQuyen;
                        obj.Serie = Serie;
                        obj.MaNhanvien = globalVariables.UserName;
                        obj.MaLydo = "0";
                        obj.NgayIn = DateTime.Now;
                        obj.TrangThai = 0;
                        obj.IsNew = true;
                        obj.Save();
                        IdHdonLog = obj.IdHdonLog;
                        //update bảng HoadonCapphat
                        new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                            .EqualTo(Serie)
                            .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                            .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(IdCapphat)
                            .Execute();
                        // hàm thực hiện việc khóa hóa đơn đỏ đã dùng
                        new Update(SysHoadonMau.Schema)
                        .Set(SysHoadonMau.Columns.KhoaLai).EqualTo(1)
                        .Where(SysHoadonMau.Columns.MauHoadon).IsEqualTo(MauHoadon)
                        .And(SysHoadonMau.Columns.MaQuyen).IsEqualTo(MaQuyen)
                        .And(SysHoadonMau.Columns.KiHieu).IsEqualTo(KiHieu)
                         .And(SysHoadonMau.Columns.SerieHientai).IsEqualTo(Utility.Int32Dbnull(Serie)).Execute();
                        SqlQuery sqlQuery = new Select().From<HoadonMau>().Where(HoadonMau.Columns.MauHoadon).IsEqualTo(MauHoadon);
                        HoadonMau objHoadonMau = sqlQuery.ExecuteSingle<HoadonMau>();
                        if (objHoadonMau != null)
                        {
                            if (Utility.Int32Dbnull(objHoadonMau.SerieCuoi) <= Utility.Int32Dbnull(objThanhtoan.Serie))
                            {
                                objHoadonMau.MarkOld();
                                objHoadonMau.TrangThai = 2; //nếu seris  vượt quá thi khoa lại
                                objHoadonMau.Save();

                            }

                        }

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.Serie).EqualTo(Serie)
                            .Set(KcbThanhtoan.Columns.MauHoadon).EqualTo(MauHoadon)
                            .Set(KcbThanhtoan.Columns.MaQuyen).EqualTo(MaQuyen)
                            .Set(KcbThanhtoan.Columns.KiHieu).EqualTo(KiHieu)
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(obj.IdHdonLog)
                            .Set(KcbThanhtoan.Columns.IdCapphat).EqualTo(obj.IdCapphat)
                            .Set(KcbThanhtoan.Columns.TrangthaiSeri).EqualTo(0)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan).Execute();
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

        public ActionResult BoHoadondo(long IdHdonLog)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        new Delete().From(HoadonLog.Schema)
                            .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.Serie).EqualTo("")
                            .Set(KcbThanhtoan.Columns.MauHoadon).EqualTo("")
                            .Set(KcbThanhtoan.Columns.MaQuyen).EqualTo("")
                            .Set(KcbThanhtoan.Columns.KiHieu).EqualTo("")
                            .Set(KcbThanhtoan.Columns.IdHdonLog).EqualTo(-1)
                            .Set(KcbThanhtoan.Columns.IdCapphat).EqualTo(-1)
                            .Set(KcbThanhtoan.Columns.TrangthaiSeri).EqualTo(0)
                            .Where(KcbThanhtoan.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
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

        public ActionResult UpdatePtramBHYT(KcbLuotkham objLuotKham, int option)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        decimal ptramBhyt = Utility.DecimaltoDbnull(objLuotKham.PtramBhyt, 0m);
                        decimal bnhanchitra = 0m;
                        decimal bhytchitra = 0m;
                        decimal dongia = 0m;
                        if (option == 1 || option == -1)
                        {
                            var lstKcbDangkyKcb = new Select().From(KcbDangkyKcb.Schema)
                                .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotKham.MaLuotkham)
                                .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo)
                                .IsNotEqualTo(1)
                                .ExecuteAsCollection<KcbDangkyKcbCollection>();
                            foreach (KcbDangkyKcb _item in lstKcbDangkyKcb)
                            {
                                dongia = _item.DonGia;
                                if (_item.TuTuc == 0)
                                {
                                    bhytchitra = THU_VIEN_CHUNG.TinhBhytChitra(ptramBhyt, dongia, 0);
                                    bnhanchitra = dongia - bhytchitra;
                                }
                                else
                                {
                                    bhytchitra = 0;
                                    bnhanchitra = dongia;
                                }
                            }
                        }
                        else if (option == 2 || option == -1)
                        {
                        }
                        else if (option == 3 || option == -1)
                        {
                        }
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

        public void HUYTHONGTIN_THANHTOAN(KcbThanhtoanChitietCollection objArrPaymentDetail, KcbThanhtoan objThanhtoan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                     var locked = (byte) (objThanhtoan.MaDoituongKcb == "DV" ? 1: 0);
                     if (objThanhtoan.NoiTru == 0)
                     {
                         StoredProcedure sp = SPs.SpKcbHuythanhtoan(objThanhtoan.IdThanhtoan, locked, DateTime.Now,
                             globalVariables.UserName, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham);
                         sp.Execute();
                     }
                     else
                         SPs.SpKcbHuythanhtoanNoitru(objThanhtoan.IdThanhtoan, locked, DateTime.Now,
                             globalVariables.UserName, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham).Execute();

                    #region Hủy thanh toán cũ
                    //new Update(KcbDangkySokham.Schema)
                    //    .Set(KcbDangkySokham.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDangkySokham.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkySokham.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDangkySokham.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkySokham.Columns.NgaySua).EqualTo(DateTime.Now)
                    //    .Set(KcbDangkySokham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDangkySokham.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    //new Update(KcbDangkyKcb.Schema)
                    //    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(DateTime.Now)
                    //    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDangkyKcb.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    //new Update(NoitruPhanbuonggiuong.Schema)
                    //    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(DateTime.Now)
                    //    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(NoitruPhanbuonggiuong.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(KcbChidinhclsChitiet.Schema)
                    //    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
                    //    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbChidinhclsChitiet.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Where(KcbChidinhclsChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(KcbDonthuocChitiet.Schema)
                    //    .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(null)
                    //    .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(-1)
                    //    .Set(KcbDonthuocChitiet.Columns.TileChietkhau).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NguonThanhtoan).EqualTo(null)
                    //    .Set(KcbDonthuocChitiet.Columns.TienChietkhau).EqualTo(0)
                    //    .Set(KcbDonthuocChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
                    //    .Set(KcbDonthuocChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    //    .Where(KcbDonthuocChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Update(TTongChiphi.Schema)
                    //    .Set(TTongChiphi.Columns.PaymentId).EqualTo(null)
                    //    .Set(TTongChiphi.Columns.PaymentStatus).EqualTo(0)
                    //    .Set(TTongChiphi.Columns.PaymentDate).EqualTo(null)
                    //    .Where(TTongChiphi.Columns.PaymentId).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();


                    //new Delete().From(KcbPhieuthu.Schema)
                    //    .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    //new Delete().From(KcbThanhtoanChitiet.Schema)
                    //    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                    #endregion 
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString);
                // return ActionResult.Error;
            }
        }

        public ActionResult HuyThongTinLanThanhToan_Donthuoctaiquay(int id_thanhtoan, KcbLuotkham objLuotkham,
            string lydohuy, int IdHdonLog, bool HuyBienlai)
        {
            try
            {
                decimal v_TotalPaymentDetail = 0;
                decimal v_DiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (IdHdonLog > 0)
                            if (HuyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    id_thanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        long id_donthuoc = -1;
                        if (arrPaymentDetails.Count > 0) id_donthuoc = arrPaymentDetails[0].IdPhieu;
                        if (objThanhtoan != null)
                            HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                        var lstChitiet =
                            new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc)
                                .IsEqualTo(id_donthuoc)
                                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        var actionResult = ActionResult.Success;
                        if (objDonthuoc != null && lstChitiet.Count > 0)
                        {
                            actionResult = new XuatThuoc().HuyXacNhanDonThuocBNTaiQuay(id_donthuoc,
                                Utility.Int16Dbnull(lstChitiet[0].IdKho, 0), DateTime.Now, lydohuy);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    break;
                                case ActionResult.Error:
                                    return actionResult;
                            }
                        }
                        KcbThanhtoan.Delete(id_thanhtoan);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult HuyThongTinLanThanhToan_Ao(int id_thanhtoan, KcbLuotkham objLuotkham, string lydohuy,
            int IdHdonLog, bool HuyBienlai)
        {
            try
            {
                decimal v_TotalPaymentDetail = 0;
                decimal v_DiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (IdHdonLog > 0)
                            if (HuyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    id_thanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                        if (objThanhtoan != null)
                            HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        new Delete().From(KcbPhieuDct.Schema)
                            .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(objThanhtoan.KieuThanhtoan).Execute();
                        if (objLuotkham != null)
                        {
                            var locked = (byte) (objLuotkham.MaDoituongKcb == "DV" ? objLuotkham.Locked : 0);
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                                .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                                .Set(KcbLuotkham.Columns.Locked).EqualTo(locked)
                                .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(locked)
                                .Set(KcbLuotkham.Columns.BoVien).EqualTo(0)
                                .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("")
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                        }
                        KcbThanhtoan.Delete(id_thanhtoan);
                        if (objLuotkham != null)
                            log.Info(
                                string.Format(
                                    "Phiếu thanh toán ID: {0} của bệnh nhân: {1} - ID Bệnh nhân: {2} đã được hủy bởi :{3} với lý do hủy :{4}",
                                    id_thanhtoan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                    globalVariables.UserName, lydohuy));
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HuyThongTinTamthu(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, string lydohuy,
           int idHdonLog, bool huyBienlai)
        {
            try
            {
                decimal vTotalPaymentDetail = 0;
                decimal vDiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        
                        
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    objThanhtoan.IdThanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        var objKcbLoghuy = new KcbLoghuy();
                        objKcbLoghuy.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objKcbLoghuy.MaLuotkham = objThanhtoan.MaLuotkham;
                        objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objKcbLoghuy.SotienHuy = objThanhtoan.TongTien;
                        objKcbLoghuy.LydoHuy = lydohuy;
                        objKcbLoghuy.NgayHuy = DateTime.Now;
                        objKcbLoghuy.NgayTao = DateTime.Now;
                        objKcbLoghuy.NguoiTao = globalVariables.UserName;
                        objKcbLoghuy.IsNew = true;
                        objKcbLoghuy.LoaiphieuHuy = 5;
                        objKcbLoghuy.Save();
                        StoredProcedure sp = SPs.SpKcbHuytamthu(objThanhtoan.IdThanhtoan, 0, DateTime.Now,
                             globalVariables.UserName, objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham);
                        sp.Execute();

                        Utility.Log("HuyThongTinTamthu()", globalVariables.UserName,
                            string.Format(
                                "Phiếu tạm thu ID: {0} của bệnh nhân: {1} - ID Bệnh nhân: {2} đã được hủy bởi :{3} với lý do hủy :{4} và số tiền hủy {5}",
                                objThanhtoan.IdThanhtoan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                globalVariables.UserName, lydohuy, objKcbLoghuy.SotienHuy), newaction.Delete, this.GetType().Assembly.ManifestModule.Name); ;
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HuyThongTinLanThanhToan(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, string lydohuy,
            int idHdonLog, bool huyBienlai)
        {
            try
            {
                decimal vTotalPaymentDetail = 0;
                decimal vDiscountRate = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru,0)>1 && Utility.Byte2Bool(objThanhtoan.NoiTru))
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_TUDONGHOANUNG_KHITHANHTOANNOITRU", "0",
                                    false) == "1")
                                SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,objThanhtoan.IdThanhtoan, 1).Execute();
                        }
                        else
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU", "0",
                                    false) == "1")
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "NGOAITRU_TUDONGHOANUNG_KHITHANHTOANNGOAITRU", "0", false) == "1")
                                    SPs.NoitruHuyhoanung(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objThanhtoan.IdThanhtoan, 0).Execute();
                        }
                        if (idHdonLog > 0)
                            if (!huyBienlai)
                                new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(idHdonLog).Execute();
                            else
                                new Delete().From(HoadonLog.Schema)
                                    .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(idHdonLog).Execute();
                        SqlQuery sqlQuery =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(
                                    objThanhtoan.IdThanhtoan);
                        var arrPaymentDetails = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                        var objKcbLoghuy = new KcbLoghuy();
                        objKcbLoghuy.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objKcbLoghuy.MaLuotkham = objThanhtoan.MaLuotkham;
                        objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objKcbLoghuy.SotienHuy = objThanhtoan.TongTien;
                        objKcbLoghuy.LydoHuy = lydohuy;
                        objKcbLoghuy.NgayHuy = DateTime.Now;
                        objKcbLoghuy.NgayTao = DateTime.Now;
                        objKcbLoghuy.NguoiTao = globalVariables.UserName;
                        objKcbLoghuy.IsNew = true;
                        objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objThanhtoan.KieuThanhtoan, 0);
                        objKcbLoghuy.Save();
                        HUYTHONGTIN_THANHTOAN(arrPaymentDetails, objThanhtoan);
                        DataTable dtTttt = new Select().From(QheThanhtoanTamthu.Schema).Where(QheThanhtoanTamthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).ExecuteDataSet().Tables[0];
                        if (dtTttt.Rows.Count > 0)
                        {
                            new Delete().From(QheThanhtoanTamthu.Schema).Where(QheThanhtoanTamthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                            //Chuyển trạng thái từ đã kết chuyển về chưa kết chuyển
                            foreach (DataRow dr in dtTttt.Rows)
                            {
                                long id_tt = Utility.Int64Dbnull(dr[QheThanhtoanTamthu.Columns.IdTamthu]);
                                new Update(KcbThanhtoan.Schema).Set(KcbThanhtoan.Columns.TrangThai).EqualTo(0).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                                new Update(KcbPhieuthu.Schema).Set(KcbPhieuthu.Columns.TrangThai).EqualTo(0).Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(id_tt).Execute();
                            }
                        }
                        //Hủy số QMS phòng khám thị lực nếu chưa khám
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_COKHAM_DOTHILUC", "0", false) == "1" && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
                        {
                            var lstck = from p in arrPaymentDetails where p.IdLoaithanhtoan == 1 select p;
                            if (lstck.Any())//Nếu có hủy thanh toán công khám-->Tìm công khám đo thị lực để hủy số QMS
                            {
                                KcbDangkyKcb objCongkham = new Select().From(KcbDangkyKcb.Schema)
                                    .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                    .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                    .And(KcbDangkyKcb.Columns.KhamThiluc).IsEqualTo(1)
                                    .ExecuteSingle<KcbDangkyKcb>();
                                //Chưa khám thị lực mới hủy số QMS 
                                if (objCongkham != null && objCongkham.TrangThai != 1 && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
                                {
                                    KCB_QMS _KCB_QMS = new KCB_QMS();
                                    new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.SttKham).EqualTo(0).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
                                    _KCB_QMS.QmsPhongkhamDelete((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt,  objCongkham.MaLuotkham, objCongkham.IdBenhnhan,  objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham);
                                }
                            }
                        }
                        //new Delete().From(KcbPhieuDct.Schema)
                        //    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                        //    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                        //    .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(objThanhtoan.KieuThanhtoan).Execute();
                        //if (objLuotkham != null)
                        //{
                        //    var locked = (byte) (objLuotkham.MaDoituongKcb == "DV" ? objLuotkham.Locked : 0);
                        //    new Update(KcbLuotkham.Schema)
                        //        .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                        //        .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                        //        .Set(KcbLuotkham.Columns.Locked).EqualTo(locked)
                        //        .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(locked)
                        //        .Set(KcbLuotkham.Columns.TthaiThanhtoannoitru).EqualTo(0)
                        //        .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("")
                        //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                        //}
                        //KcbThanhtoan.Delete(objThanhtoan.IdThanhtoan);
                        //new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                        //    .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan)
                        //    .Execute();
                        Utility.Log("HuyThongTinLanThanhToan()", globalVariables.UserName,
                            string.Format(
                                "Phiếu thanh toán ID: {0} của bệnh nhân: {1} - ID Bệnh nhân: {2} đã được hủy bởi :{3} với lý do hủy :{4} và số tiền hủy {5}",
                                objThanhtoan.IdThanhtoan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan,
                                globalVariables.UserName, lydohuy, objKcbLoghuy.SotienHuy), newaction.Delete, this.GetType().Assembly.ManifestModule.Name); ;
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh huy thong tin {0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        public DataTable Laychitietphieuchiquaythuoc(long IdThanhtoan, byte kieuthanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtinchitietPhieutrathuoctaiquayTheoid(IdThanhtoan, kieuthanhtoan).GetDataSet().Tables[0];
        }
        public DataTable Laychitietthanhtoan(long IdThanhtoan, byte kieuthanhtoan)
        {
            return SPs.KcbThanhtoanLaythongtinchitietTheoid(IdThanhtoan, kieuthanhtoan).GetDataSet().Tables[0];
        }
        public DataTable Laychitietthanhtoan(string str_IdThanhtoan, byte kieuthanhtoan)
        {
            return SPs.EInvoiceLaythongtinchitietTheoDanhsachId(str_IdThanhtoan, kieuthanhtoan).GetDataSet().Tables[0];
        }
        public DataTable LaychitietthanhtoanTheoTransactionId(string transaction_id, byte kieuthanhtoan)
        {
            return SPs.EInvoiceLaythongtinchitietTheoTransactionId(transaction_id, kieuthanhtoan).GetDataSet().Tables[0];
        }
        public DataSet KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(long IdThanhtoan, long id_phieuthu, long id_tamung)
        {
            return SPs.KcbThanhtoanLaydulieuphanbothanhtoanTheoPTTT(IdThanhtoan, id_phieuthu, id_tamung).GetDataSet();
        }

        public DataTable KiemtraTrangthaidonthuocTruockhihuythanhtoan(long IdThanhtoan)
        {
            return SPs.DonthuocKiemtraxacnhanthuocTrongdon(IdThanhtoan).GetDataSet().Tables[0];
        }

        public ActionResult UpdateTienphanbotheoPttt( DataRow[] arrDr , KcbLuotkham objluotkham, long id_thanhtoan, long id_phieuthu, long id_tamung, string ma_pttt, string ma_nganhang,decimal tongTien,byte loai_phanbo, ref string msg)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        //decimal tongTien = Utility.Int64Dbnull(arrDr[0]["tong_tien"], 0);
                        if (id_thanhtoan > 0 && id_tamung<=0)
                        {
                            KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan)
                                .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsLessThanOrEqualTo(0)
                                .Execute();
                            objThanhtoan.MaPttt = ma_pttt;
                            objThanhtoan.MaNganhang = ma_nganhang;
                            objThanhtoan.IsNew = false;
                            objThanhtoan.MarkOld();
                            objThanhtoan.Save();
                            new Update(KcbPhieuthu.Schema)
                                .Set(KcbPhieuthu.Columns.MaPttt).EqualTo(objThanhtoan.MaPttt)
                                .Set(KcbPhieuthu.Columns.MaNganhang).EqualTo(objThanhtoan.MaNganhang)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan)
                                .And(KcbPhieuthu.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                                .And(KcbPhieuthu.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                                .Execute();
                            if (objThanhtoan.NoiTru == 1)//Cập nhật cho bản ghi hoàn ứng
                            {
                                new Update(NoitruTamung.Schema)
                                .Set(NoitruTamung.Columns.MaPttt).EqualTo(objThanhtoan.MaPttt)
                                .Set(NoitruTamung.Columns.MaNganhang).EqualTo(objThanhtoan.MaNganhang)
                                    .Where(NoitruTamung.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                                    .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1).Execute();
                            }
                        }
                        if (id_phieuthu > 0)
                        {
                            KcbPhieuthu objPhieuthu = KcbPhieuthu.FetchByID(id_phieuthu);
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdPhieuthu)
                                .IsEqualTo(id_phieuthu)
                                .Execute();
                            objPhieuthu.MaPttt = ma_pttt;
                            objPhieuthu.MaNganhang = ma_nganhang;
                            objPhieuthu.IsNew = false;
                            objPhieuthu.MarkOld();
                            objPhieuthu.Save();
                        }
                        if (id_tamung > 0)
                        {
                            NoitruTamung objTU = NoitruTamung.FetchByID(id_tamung);
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung)
                                .IsEqualTo(id_tamung)
                                .Execute();
                            objTU.MaPttt = ma_pttt;
                            objTU.MaNganhang = ma_nganhang;
                            objTU.IsNew = false;
                            objTU.MarkOld();
                            objTU.Save();
                        }
                        
                        if (arrDr.Length <=1)//Cập nhật về hình thức thanh toán chung
                        {
                            var newItem = new KcbThanhtoanPhanbotheoPTTT();
                            newItem.IdThanhtoan = id_thanhtoan;
                            newItem.IdPhieuthu = id_phieuthu;
                            newItem.IdTamung = id_tamung;
                            newItem.MaPttt = ma_pttt;
                            newItem.IdBenhnhan = objluotkham.IdBenhnhan;
                            newItem.MaLuotkham = objluotkham.MaLuotkham;
                            newItem.TongTien = tongTien;
                            newItem.NoiTru = (byte)objluotkham.Noitru;
                            newItem.SoTien = tongTien;
                            newItem.NguoiTao = globalVariables.UserName;
                            newItem.NgayTao = DateTime.Now;
                            newItem.MaNganhang = ma_nganhang;
                            newItem.LoaiPhanbo = loai_phanbo;
                            //StoredProcedure sp = SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(newItem.IdThanhtoan, newItem.MaPttt,
                            //    newItem.IdBenhnhan, newItem.MaLuotkham, newItem.NoiTru, newItem.TongTien,
                            //    newItem.SoTien
                            //    , newItem.NguoiTao, newItem.NgayTao, newItem.NguoiSua, newItem.NgaySua);
                            //sp.Execute();
                            newItem.IsNew = true;
                            newItem.Save();
                        }
                        else//Là hình thức phân bổ ra nhiều phương thức thanh toán
                        {
                            foreach (DataRow dr in arrDr)
                            {
                                if (Utility.DecimaltoDbnull(dr["so_tien"], 0) > 0)
                                {
                                    var newItem = new KcbThanhtoanPhanbotheoPTTT();
                                    newItem.IdThanhtoan = id_thanhtoan;
                                    newItem.IdPhieuthu = id_phieuthu;
                                    newItem.IdTamung = id_tamung;
                                    newItem.MaPttt = Utility.sDbnull(dr["ma_pttt"], "");
                                    newItem.IdBenhnhan = objluotkham.IdBenhnhan;
                                    newItem.MaLuotkham = objluotkham.MaLuotkham;
                                    newItem.TongTien = tongTien;
                                    newItem.NoiTru = (byte)objluotkham.Noitru;
                                    newItem.SoTien = Utility.DecimaltoDbnull(dr["so_tien"], 0);
                                    newItem.NguoiTao = globalVariables.UserName;
                                    newItem.NgayTao = DateTime.Now;
                                    newItem.MaNganhang = Utility.sDbnull(dr["ma_nganhang"], "");
                                    newItem.LoaiPhanbo = loai_phanbo;
                                    //StoredProcedure sp = SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(newItem.IdThanhtoan, newItem.MaPttt,
                                    //    newItem.IdBenhnhan, newItem.MaLuotkham, newItem.NoiTru, newItem.TongTien,
                                    //    newItem.SoTien
                                    //    , newItem.NguoiTao, newItem.NgayTao, newItem.NguoiSua, newItem.NgaySua);
                                    //sp.Execute();
                                    newItem.IsNew = true;
                                    newItem.Save();
                                }
                            }
                        }
                        
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                log.Error("Loi trong qua trinh tra tien lai:{0}", ex.ToString());
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateHuyInPhoiBHYT(KcbLuotkham objLuotkham, KieuThanhToan kieuThanhToan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            //.Set(KcbLuotkham.Columns.TinhTrangRaVienStatus).EqualTo(0)
                            // .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                            //.Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(null)
                            //.Set(KcbLuotkham.Columns.IpMacSua).EqualTo(globalVariables.IpMacAddress)
                            //.Set(KcbLuotkham.Columns.IpMaySua).EqualTo(globalVariables.IpAddress)
                            //.Set(KcbLuotkham.Columns.ReasonBy).EqualTo("Hủy phôi bảo hiểm")
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .Execute();
                        new Delete().From(KcbPhieuDct.Schema)
                            .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(kieuThanhToan).Execute();
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

        private decimal SumOfPaymentDetail(KcbThanhtoanChitiet[] objArrPaymentDetail)
        {
            decimal SumOfPaymentDetail = 0;
            foreach (KcbThanhtoanChitiet paymentDetail in objArrPaymentDetail)
            {
                if (paymentDetail.TuTuc == 0)
                    SumOfPaymentDetail += (Utility.Int32Dbnull(paymentDetail.SoLuong)*
                                           Utility.DecimaltoDbnull(paymentDetail.DonGia))
                                          +
                                          (Utility.DecimaltoDbnull(paymentDetail.PhuThu, 0)*
                                           Utility.Int32Dbnull(paymentDetail.SoLuong, 0));
            }
            return SumOfPaymentDetail;
        }
        public void TaobanghiBiendongTrathuoctaiquay(KcbThanhtoan objPhieuchi)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                       
                        //Tạo dòng biến động trả lại trong bảng biến động cho thuốc trả lại
                        DataTable dtLichsutralaithuoc = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema).Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).ExecuteDataSet().Tables[0];
                        if (dtLichsutralaithuoc.Rows.Count > 0)
                        {
                            //Xóa dòng biến động thuốc trả lại
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            foreach (DataRow dr in dtLichsutralaithuoc.Rows)
                            {
                                long id_chitietdonthuoc = Utility.Int64Dbnull(dr["id_chitietdonthuoc"], -1);
                                decimal so_luong = Utility.DecimaltoDbnull(dr["so_luong"], -1);
                                //B1: Lấy chi tiết phiếu xuất thuốc người bệnh
                                TPhieuXuatthuocBenhnhanChitiet objpxct = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema).Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(id_chitietdonthuoc).ExecuteSingle<TPhieuXuatthuocBenhnhanChitiet>();
                                if (objpxct != null)//100% trừ khi bị xóa tay
                                {
                                    //B2: Lấy dòng biến động tương ứng để duplicate+ thay số lượng và loại phiếu
                                    TBiendongThuoc objnewBiendong = new Select().From(TBiendongThuoc.Schema)
                                        .Where(TBiendongThuoc.Columns.IdPhieu).IsEqualTo(objpxct.IdPhieu)
                                        .And(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(objpxct.IdPhieuChitiet)
                                        .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3)
                                         .And(TBiendongThuoc.Columns.MaLuotkham).IsEqualTo(objPhieuchi.MaLuotkham)
                                          .And(TBiendongThuoc.Columns.IdBenhnhan).IsEqualTo(objPhieuchi.IdBenhnhan)
                                        .ExecuteSingle<TBiendongThuoc>();
                                    //B3 duplicate phiếu này
                                    if (objnewBiendong != null)//100% trừ khi bị xóa tay
                                    {
                                        objnewBiendong.IsNew = true;
                                        objnewBiendong.SoLuong = so_luong;//Số lượng trả lại
                                        objnewBiendong.IdPhieuchi = objPhieuchi.IdThanhtoan;
                                        objnewBiendong.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                        objnewBiendong.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                        objnewBiendong.NgayBiendong = objPhieuchi.NgayThanhtoan;
                                        objnewBiendong.NgayHoadon = objnewBiendong.NgayBiendong;
                                        objnewBiendong.NgayTao = objnewBiendong.NgayBiendong;
                                        objnewBiendong.NgayNhap = objnewBiendong.NgayBiendong;
                                        objnewBiendong.IdNhanvien = globalVariables.gv_intIDNhanvien;
                                        objnewBiendong.NguoiTao = globalVariables.UserName;
                                        objnewBiendong.MotaThem = "Trả lại thuốc tại quầy";
                                        objnewBiendong.Save();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        /// <summary>
        ///     Trả thuốc tại quầy+ tạo phiếu chi. Dành cho đơn vị bộ phận dược thu tiền, trả tiền tại quầy. 1 người làm
        /// </summary>
        /// <param name="objThanhtoan"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="objArrPaymentDetail"></param>
        /// <returns></returns>
        public ActionResult Trathuoctaiquay(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, List<Tralaithuoctaiquay> lstTralai,
            string malydohuy, string noidunghuy, string lydotratien)
        {
            decimal ptramBhyt = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal vDblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                       
                        //Thêm mới dòng thanh toán hủy
                        objPhieuchi.KieuThanhtoan = 1;
                        objPhieuchi.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objPhieuchi.NgayThanhtoan = DateTime.Now;
                        objPhieuchi.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(DateTime.Now);
                        objPhieuchi.MaLydoHuy = malydohuy;
                        objPhieuchi.IsNew = true;
                        objPhieuchi.Save();
                        //Thêm mới phiếu yêu cầu trả lại
                        ThuocLichsuTralaithuoctaiquayPhieu newPhieu = new ThuocLichsuTralaithuoctaiquayPhieu();
                        newPhieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                        newPhieu.MaLuotkham = objLuotkham.MaLuotkham;
                        newPhieu.TrangThai = 1;
                        newPhieu.IdPhieuchi = objPhieuchi.IdThanhtoan;
                        newPhieu.NguoiTao = globalVariables.UserName;
                        newPhieu.NgayTao = DateTime.Now;
                        newPhieu.Save();
                        //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                        TT_BN = 0m;
                        TT_BNCT = 0m;
                        TT_PT = 0m;
                        TT_TT = 0m;
                        TT_BHYT = 0m;
                        TT_Chietkhau_Chitiet = 0;
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (Tralaithuoctaiquay item in lstTralai)
                        {
                            KcbThanhtoanChitiet objKcbThanhtoanChitiet = KcbThanhtoanChitiet.FetchByID(item.id_chitiet_thanhtoan);
                            if (objKcbThanhtoanChitiet == null)
                            {
                                return ActionResult.Error;
                            }
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra * Utility.DecimaltoDbnull(item.sl_tralai);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);
                            TT_PT += objKcbThanhtoanChitiet.PhuThu * Utility.DecimaltoDbnull(item.sl_tralai);
                            if (Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_TT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(item.sl_tralai);
                            else
                                TT_BNCT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(item.sl_tralai);

                            //Tạo dữ liệu hủy tiền
                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                                //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 1;
                            //objKcbThanhtoanChitiet.TongTralai = item.sl_tralai;
                            objKcbThanhtoanChitiet.IsNew = false;
                            objKcbThanhtoanChitiet.MarkOld();
                            objKcbThanhtoanChitiet.Save();
                            //Thêm 1 dòng chi tiết trả(Áp dụng cho việc trả thuốc nhiều lần tránh id_thanhtoanhuy bị cập nhật đè)
                            objKcbThanhtoanChitiet.IsNew = true;
                            objKcbThanhtoanChitiet.MultiCancel = 1;
                            objKcbThanhtoanChitiet.IdThanhtoan = objPhieuchi.IdThanhtoan;
                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                            objKcbThanhtoanChitiet.SoLuong = item.sl_tralai;
                            objKcbThanhtoanChitiet.TongTralai = item.sl_tralai;
                            objKcbThanhtoanChitiet.Save();
                            //Thêm lịch sử trả lại thuốc
                            ThuocLichsuTralaithuoctaiquayChitiet tralai = new ThuocLichsuTralaithuoctaiquayChitiet();
                            tralai.IdChitietThanhtoan = objKcbThanhtoanChitiet.IdChitiet;
                            tralai.IdChitietThanhtoanGoc = item.id_chitiet_thanhtoan;
                            tralai.IdChitietdonthuoc = item.id_chitiet_donthuoc;
                            tralai.IdDonthuoc = item.id_donthuoc;
                            tralai.IdThuoc = item.id_thuoc;
                            tralai.SoLuong = item.sl_tralai;
                            tralai.IdPhieuchi = objPhieuchi.IdThanhtoan;
                            tralai.DonGia = item.don_gia;
                            tralai.IdLoaithanhtoan = item.id_loaithanhtoan;
                            tralai.IsNew = true;
                            tralai.Save();
                            //Cộng thuốc vào kho
                            DataTable dtchitietdonthuoc = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(tralai.IdChitietdonthuoc).ExecuteDataSet().Tables[0];
                            if (dtchitietdonthuoc.Rows.Count > 0)
                            {
                                int num = SPs.ThuocTrathuoctaiquayvaokho(Utility.Int64Dbnull(dtchitietdonthuoc.Rows[0]["id_thuockho"]), Utility.Int32Dbnull(dtchitietdonthuoc.Rows[0]["id_thuoc"]), tralai.SoLuong).Execute();
                            }
                            else//Chi tiết trong bảng đơn thuốc bị xóa do lỗi gì đó
                                return ActionResult.Cancel;
                            int numoftralai = SPs.KcbThanhtoanChitietCapnhatsluongTralai(tralai.IdChitietThanhtoanGoc, tralai.SoLuong).Execute();
                            //Tạo dòng biến động trả lại trong bảng biến động cho thuốc trả lại
                            //B1: Lấy chi tiết phiếu xuất thuốc người bệnh
                            TPhieuXuatthuocBenhnhanChitiet objpxct = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema).Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(item.id_chitiet_donthuoc).ExecuteSingle<TPhieuXuatthuocBenhnhanChitiet>();
                            if (objpxct != null)//100% trừ khi bị xóa tay
                            {
                                //B2: Lấy dòng biến động tương ứng để duplicate+ thay số lượng và loại phiếu
                                TBiendongThuoc objnewBiendong=new Select().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieu).IsEqualTo(objpxct.IdPhieu)
                                    .And(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(objpxct.IdPhieuChitiet)
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3)
                                     .And(TBiendongThuoc.Columns.MaLuotkham).IsEqualTo(objPhieuchi.MaLuotkham)
                                      .And(TBiendongThuoc.Columns.IdBenhnhan).IsEqualTo(objPhieuchi.IdBenhnhan)
                                    .ExecuteSingle<TBiendongThuoc>();
                                //B3 duplicate phiếu này
                                if (objnewBiendong != null)//100% trừ khi bị xóa tay
                                {
                                    objnewBiendong.IsNew = true;
                                    objnewBiendong.SoLuong = item.sl_tralai;//Số lượng trả lại
                                    objnewBiendong.IdPhieuchi = objPhieuchi.IdThanhtoan;
                                    objnewBiendong.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                    objnewBiendong.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                    objnewBiendong.NgayBiendong = objPhieuchi.NgayThanhtoan;
                                    objnewBiendong.NgayHoadon = objnewBiendong.NgayBiendong;
                                    objnewBiendong.NgayTao = objnewBiendong.NgayBiendong;
                                    objnewBiendong.NgayNhap = objnewBiendong.NgayBiendong;
                                    objnewBiendong.IdNhanvien = globalVariables.gv_intIDNhanvien;
                                    objnewBiendong.NguoiTao = globalVariables.UserName;
                                    objnewBiendong.MotaThem = "Trả lại thuốc tại quầy";
                                    objnewBiendong.Save();
                                }
                            }
                        }
                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                        //Update lại tiền thanh toán
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Set(KcbThanhtoan.Columns.TongtienChietkhauChitiet).EqualTo(TT_Chietkhau_Chitiet)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objPhieuchi.IdThanhtoan;
                        objPhieuthu.IdBenhnhan = objPhieuchi.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objPhieuchi.MaLuotkham;
                        objPhieuthu.NoiDung = noidunghuy;
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 1);
                        objPhieuthu.NgayThuchien = DateTime.Now;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objPhieuchi.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objPhieuchi.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.MaPttt = objPhieuchi.MaPttt;
                        objPhieuthu.MaNganhang = objPhieuchi.MaNganhang;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objPhieuchi.NoiTru;
                        objPhieuthu.LydoNop = lydotratien;
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();
                        //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objPhieuchi.IdThanhtoan, -1l, -1l, objPhieuchi.MaPttt, objPhieuchi.MaNganhang,
                            objPhieuchi.IdBenhnhan, objPhieuchi.MaLuotkham,
                            objPhieuchi.NoiTru, objPhieuthu.SoTien, objPhieuthu.SoTien,
                            objPhieuchi.NguoiTao, objPhieuchi.NgayTao, "", objPhieuchi.NgayTao,-1l,0,(byte)1).Execute();
                        List<KcbThanhtoanChitiet> arrKcbThanhtoanChitietHuy = new List<KcbThanhtoanChitiet>();
                        #region "BHYT"
                        //Kết thúc tạo thông tin phiếu trả tiền(Phiếu chi)-->Kế tiếp cần tính toán lại % BHYT và tiền chênh lệch cho đối tượng BHYT
                        //Riêng đối tượng dịch vụ Giữ nguyên các giá trị thanh toán(Thực thu= Tổng thanh toán-Tổng trả lại)
                        if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        {
                            vDblTongtienHuy = TongtienKhongTutuc(arrKcbThanhtoanChitietHuy);
                            //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                            KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                new KcbThanhtoanController()
                                    .FetchByQuery(
                                        KcbThanhtoan.CreateQuery()
                                            .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                objLuotkham.MaLuotkham)
                                            .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                objLuotkham.IdBenhnhan)
                                            .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                    );
                            List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                select q.IdThanhtoan).Distinct().ToList();
                            //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                            var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                            if (lstIdThanhtoanAll.Count > 0)
                                lstKcbThanhtoanChitiet_Tatca =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                        .ToList<KcbThanhtoanChitiet>();

                            v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                where p.TuTuc == 0
                                select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia);

                            //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                            LayThongtinPtramBhyt(v_TotalPaymentDetail - vDblTongtienHuy, objLuotkham, ref ptramBhyt);


                            //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                            var lsttemp = new List<KcbThanhtoanChitiet>();
                            THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet_Tatca,
                                ptramBhyt);
                            List<long> lstIdThanhtoanTinhlai = (from q in lstKcbThanhtoanChitiet_Tatca
                                select q.IdThanhtoan).Distinct().ToList();
                            //99% đặt thông số này=1
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                    "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                            {
                                foreach (int IdThanhtoan in lstIdThanhtoanTinhlai)
                                    //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                {
                                    TT_BN = 0m;
                                    TT_BNCT = 0m;
                                    TT_PT = 0m;
                                    TT_TT = 0m;
                                    TT_BHYT = 0m;
                                    TT_Chietkhau_Chitiet = 0;
                                    //Lấy lại từ CSDL
                                    List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                        where p.IdThanhtoan == IdThanhtoan
                                        select p).ToList<KcbThanhtoanChitiet>();

                                    if (_LstChitiet.Count > 0)
                                    {
                                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                        {
                                            objChitietThanhtoan.MarkOld();
                                            objChitietThanhtoan.IsNew = false;
                                            objChitietThanhtoan.Save();
                                            if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                //Bỏ qua các bản ghi đã bị hủy
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_BHYT += objChitietThanhtoan.BhytChitra*
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                TT_Chietkhau_Chitiet +=
                                                    Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_TT += objChitietThanhtoan.BnhanChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                else
                                                    TT_BNCT += objChitietThanhtoan.BnhanChitra*
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            }
                                        }
                                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                                        //Update lại tiền thanh toán
                                        new Update(KcbThanhtoan.Schema)
                                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        //Update phiếu thu
                                        new Update(KcbPhieuthu.Schema)
                                            .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                            .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                            .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Trả lại tiền từ một phiếu trả lại thuốc do bộ phận Dược tạo ra
        /// </summary>
        /// <param name="objPhieuchi"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="lstTralai"></param>
        /// <param name="malydohuy"></param>
        /// <param name="noidunghuy"></param>
        /// <param name="lydotratien"></param>
        /// <returns></returns>
        public ActionResult Trathuoctaiquay_Rieng(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, long idphieutralaithuoc,
           string malydohuy, string noidunghuy, string lydotratien)
        {
            decimal ptramBhyt = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal vDblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                ThuocLichsuTralaithuoctaiquayPhieu newPhieu =  ThuocLichsuTralaithuoctaiquayPhieu.FetchByID(idphieutralaithuoc);
                if (newPhieu.TrangThai == 0)//Chưa được trả lại
                {
                    ThuocLichsuTralaithuoctaiquayChitietCollection lstChitiet =
                        new Select().
                        From(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                        .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdTralaithuoc).IsEqualTo(idphieutralaithuoc)
                        .And(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(-1)
                        .ExecuteAsCollection<ThuocLichsuTralaithuoctaiquayChitietCollection>();
                    if (lstChitiet.Count > 0)
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var sh = new SharedDbConnectionScope())
                            {
                                decimal TT_BN = 0m;
                                decimal TT_BNCT = 0m;
                                decimal TT_PT = 0m;
                                decimal TT_TT = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;
                                //Thêm mới dòng thanh toán hủy
                                objPhieuchi.KieuThanhtoan = 1;
                                objPhieuchi.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                                objPhieuchi.NgayThanhtoan = DateTime.Now;
                                objPhieuchi.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(DateTime.Now);
                                objPhieuchi.MaLydoHuy = malydohuy;
                                objPhieuchi.IsNew = true;
                                objPhieuchi.Save();
                                //Cập nhật trạng thái phiếu đã được trả lại=1
                                newPhieu.IdPhieuchi=objPhieuchi.IdThanhtoan;
                                newPhieu.TrangThai=1;
                                newPhieu.Save();
                                //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                                TT_BN = 0m;
                                TT_BNCT = 0m;
                                TT_PT = 0m;
                                TT_TT = 0m;
                                TT_BHYT = 0m;
                                TT_Chietkhau_Chitiet = 0;
                                //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                                foreach (ThuocLichsuTralaithuoctaiquayChitiet item in lstChitiet)
                                {
                                    KcbThanhtoanChitiet objKcbThanhtoanChitiet = KcbThanhtoanChitiet.FetchByID(item.IdChitietThanhtoanGoc);
                                    if (objKcbThanhtoanChitiet == null)
                                    {
                                        return ActionResult.Error;
                                    }
                                    if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                        TT_BHYT += objKcbThanhtoanChitiet.BhytChitra * Utility.DecimaltoDbnull(item.SoLuong);
                                    TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);
                                    TT_PT += objKcbThanhtoanChitiet.PhuThu * Utility.DecimaltoDbnull(item.SoLuong);
                                    if (Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                        TT_TT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(item.SoLuong);
                                    else
                                        TT_BNCT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(item.SoLuong);

                                    //Tạo dữ liệu hủy tiền
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                                    //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                                    objKcbThanhtoanChitiet.TrangthaiHuy = 1;
                                    //objKcbThanhtoanChitiet.TongTralai = item.sl_tralai;
                                    objKcbThanhtoanChitiet.IsNew = false;
                                    objKcbThanhtoanChitiet.MarkOld();
                                    objKcbThanhtoanChitiet.Save();
                                    //Thêm 1 dòng chi tiết trả(Áp dụng cho việc trả thuốc nhiều lần tránh id_thanhtoanhuy bị cập nhật đè)
                                    objKcbThanhtoanChitiet.IsNew = true;
                                    objKcbThanhtoanChitiet.MultiCancel = 1;
                                    objKcbThanhtoanChitiet.IdThanhtoan = objPhieuchi.IdThanhtoan;
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                                    objKcbThanhtoanChitiet.SoLuong = item.SoLuong;
                                    objKcbThanhtoanChitiet.TongTralai = item.SoLuong;
                                    objKcbThanhtoanChitiet.Save();
                                    //Cập nhật dòng lịch sử trả lại thuốc
                                    new Update(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                                    .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).EqualTo(objPhieuchi.IdThanhtoan)
                                    .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoan).EqualTo(objKcbThanhtoanChitiet.IdChitiet)
                                    .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.Id).IsEqualTo(item.Id)
                                    .Execute();
                                    //Cộng thuốc vào kho
                                    DataTable dtchitietdonthuoc = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(item.IdChitietdonthuoc).ExecuteDataSet().Tables[0];
                                    if (dtchitietdonthuoc.Rows.Count > 0)
                                    {
                                        int num = SPs.ThuocTrathuoctaiquayvaokho(Utility.Int64Dbnull(dtchitietdonthuoc.Rows[0]["id_thuockho"]), Utility.Int32Dbnull(dtchitietdonthuoc.Rows[0]["id_thuoc"]), item.SoLuong).Execute();
                                    }
                                    else//Chi tiết trong bảng đơn thuốc bị xóa do lỗi gì đó
                                        return ActionResult.Cancel;
                                    int numoftralai = SPs.KcbThanhtoanChitietCapnhatsluongTralai(item.IdChitietThanhtoanGoc, item.SoLuong).Execute();
                                    //Tạo dòng biến động trả lại trong bảng biến động cho thuốc trả lại
                                    //B1: Lấy chi tiết phiếu xuất thuốc người bệnh
                                    TPhieuXuatthuocBenhnhanChitiet objpxct = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema).Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(item.IdChitietdonthuoc).ExecuteSingle<TPhieuXuatthuocBenhnhanChitiet>();
                                    if (objpxct != null)//100% trừ khi bị xóa tay
                                    {
                                        //B2: Lấy dòng biến động tương ứng để duplicate+ thay số lượng và loại phiếu
                                        TBiendongThuoc objnewBiendong = new Select().From(TBiendongThuoc.Schema)
                                            .Where(TBiendongThuoc.Columns.IdPhieu).IsEqualTo(objpxct.IdPhieu)
                                            .And(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(objpxct.IdPhieuChitiet)
                                            .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3)
                                             .And(TBiendongThuoc.Columns.MaLuotkham).IsEqualTo(objPhieuchi.MaLuotkham)
                                              .And(TBiendongThuoc.Columns.IdBenhnhan).IsEqualTo(objPhieuchi.IdBenhnhan)
                                            .ExecuteSingle<TBiendongThuoc>();
                                        //B3 duplicate phiếu này
                                        if (objnewBiendong != null)//100% trừ khi bị xóa tay
                                        {
                                            objnewBiendong.IsNew = true;
                                            objnewBiendong.SoLuong = item.SoLuong;//Số lượng trả lại
                                            objnewBiendong.IdPhieuchi = objPhieuchi.IdThanhtoan;
                                            objnewBiendong.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                            objnewBiendong.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuTralaithuocTaiQuay);
                                            objnewBiendong.NgayBiendong = objPhieuchi.NgayThanhtoan;
                                            objnewBiendong.NgayHoadon = objnewBiendong.NgayBiendong;
                                            objnewBiendong.NgayTao = objnewBiendong.NgayBiendong;
                                            objnewBiendong.NgayNhap = objnewBiendong.NgayBiendong;
                                            objnewBiendong.IdNhanvien = globalVariables.gv_intIDNhanvien;
                                            objnewBiendong.NguoiTao = globalVariables.UserName;
                                            objnewBiendong.MotaThem = "Trả lại thuốc tại quầy";
                                            objnewBiendong.Save();
                                        }
                                    }
                                }
                                TT_BN += TT_PT + TT_BNCT + TT_TT;
                                //Update lại tiền thanh toán
                                new Update(KcbThanhtoan.Schema)
                                    .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                    .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                    .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                    .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                    .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                    .Set(KcbThanhtoan.Columns.TongtienChietkhauChitiet).EqualTo(TT_Chietkhau_Chitiet)
                                    .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                                var objPhieuthu = new KcbPhieuthu();
                                objPhieuthu.IdThanhtoan = objPhieuchi.IdThanhtoan;
                                objPhieuthu.IdBenhnhan = objPhieuchi.IdBenhnhan;
                                objPhieuthu.MaLuotkham = objPhieuchi.MaLuotkham;
                                objPhieuthu.NoiDung = noidunghuy;
                                objPhieuthu.SoluongChungtugoc = 1;
                                objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                                objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 1);
                                objPhieuthu.NgayThuchien = DateTime.Now;
                                objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                                objPhieuthu.SotienGoc = TT_BN;
                                objPhieuthu.MaLydoChietkhau = objPhieuchi.MaLydoChietkhau;
                                objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                                objPhieuthu.TienChietkhau = objPhieuchi.TongtienChietkhau;
                                objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                                objPhieuthu.NguoiNop = globalVariables.UserName;
                                objPhieuthu.MaPttt = objPhieuchi.MaPttt;
                                objPhieuthu.MaNganhang = objPhieuchi.MaNganhang;
                                objPhieuthu.TaikhoanCo = "";
                                objPhieuthu.TaikhoanNo = "";
                                objPhieuthu.NoiTru = objPhieuchi.NoiTru;
                                objPhieuthu.LydoNop = lydotratien;
                                objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                                objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                                objPhieuthu.IsNew = true;
                                objPhieuthu.Save();
                                //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                                SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objPhieuchi.IdThanhtoan, -1l, -1l, objPhieuchi.MaPttt, objPhieuchi.MaNganhang,
                                    objPhieuchi.IdBenhnhan, objPhieuchi.MaLuotkham,
                                    objPhieuchi.NoiTru, objPhieuthu.SoTien, objPhieuthu.SoTien,
                                    objPhieuchi.NguoiTao, objPhieuchi.NgayTao, "", objPhieuchi.NgayTao, -1l, 0, (byte)1).Execute();
                                List<KcbThanhtoanChitiet> arrKcbThanhtoanChitietHuy = new List<KcbThanhtoanChitiet>();
                                #region "BHYT"
                                //Kết thúc tạo thông tin phiếu trả tiền(Phiếu chi)-->Kế tiếp cần tính toán lại % BHYT và tiền chênh lệch cho đối tượng BHYT
                                //Riêng đối tượng dịch vụ Giữ nguyên các giá trị thanh toán(Thực thu= Tổng thanh toán-Tổng trả lại)
                                if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                                {
                                    vDblTongtienHuy = TongtienKhongTutuc(arrKcbThanhtoanChitietHuy);
                                    //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                                    KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                        new KcbThanhtoanController()
                                            .FetchByQuery(
                                                KcbThanhtoan.CreateQuery()
                                                    .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                        objLuotkham.MaLuotkham)
                                                    .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                        objLuotkham.IdBenhnhan)
                                                    .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                            );
                                    List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                                                    select q.IdThanhtoan).Distinct().ToList();
                                    //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                                    var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                                    if (lstIdThanhtoanAll.Count > 0)
                                        lstKcbThanhtoanChitiet_Tatca =
                                            new Select().From(KcbThanhtoanChitiet.Schema)
                                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                                .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                                .ToList<KcbThanhtoanChitiet>();

                                    v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                                            where p.TuTuc == 0
                                                            select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong) * c.DonGia);

                                    //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                                    LayThongtinPtramBhyt(v_TotalPaymentDetail - vDblTongtienHuy, objLuotkham, ref ptramBhyt);


                                    //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                    var lsttemp = new List<KcbThanhtoanChitiet>();
                                    THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet_Tatca,
                                        ptramBhyt);
                                    List<long> lstIdThanhtoanTinhlai = (from q in lstKcbThanhtoanChitiet_Tatca
                                                                        select q.IdThanhtoan).Distinct().ToList();
                                    //99% đặt thông số này=1
                                    if (
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                            "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                                    {
                                        foreach (int IdThanhtoan in lstIdThanhtoanTinhlai)
                                        //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                        {
                                            TT_BN = 0m;
                                            TT_BNCT = 0m;
                                            TT_PT = 0m;
                                            TT_TT = 0m;
                                            TT_BHYT = 0m;
                                            TT_Chietkhau_Chitiet = 0;
                                            //Lấy lại từ CSDL
                                            List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                                                                     where p.IdThanhtoan == IdThanhtoan
                                                                                     select p).ToList<KcbThanhtoanChitiet>();

                                            if (_LstChitiet.Count > 0)
                                            {
                                                foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                                {
                                                    objChitietThanhtoan.MarkOld();
                                                    objChitietThanhtoan.IsNew = false;
                                                    objChitietThanhtoan.Save();
                                                    if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                    //Bỏ qua các bản ghi đã bị hủy
                                                    {
                                                        if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                            TT_BHYT += objChitietThanhtoan.BhytChitra *
                                                                       Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                        TT_Chietkhau_Chitiet +=
                                                            Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                        TT_PT += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                        if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                            TT_TT += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                        else
                                                            TT_BNCT += objChitietThanhtoan.BnhanChitra *
                                                                       Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    }
                                                }
                                                TT_BN += TT_PT + TT_BNCT + TT_TT;
                                                //Update lại tiền thanh toán
                                                new Update(KcbThanhtoan.Schema)
                                                    .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                                    .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                                    .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                                    .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                                    .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                                    .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                                //Update phiếu thu
                                                new Update(KcbPhieuthu.Schema)
                                                    .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                                    .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                                    .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            scope.Complete();
                            return ActionResult.Success;
                        }
                    }
                }
                return ActionResult.Success;
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Exception;
            }
        }
        /// <summary>
        ///Dược Lập phiếu trả thuốc tại quầy. Mô hình Dược cấp phát và Nhận lại thuốc. TNV thanh toán và trả lại tiền
        /// </summary>
        /// <param name="objPhieuchi"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="lstTralai"></param>
        /// <param name="malydohuy"></param>
        /// <param name="noidunghuy"></param>
        /// <param name="lydotratien"></param>
        /// <returns></returns>
        public ActionResult TaoPhieuTrathuoctaiquay_BophanDuoc( ThuocLichsuTralaithuoctaiquayPhieu newPhieu, KcbLuotkham objLuotkham, List<Tralaithuoctaiquay> lstTralai)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        newPhieu.Save();
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (Tralaithuoctaiquay item in lstTralai)
                        {
                            KcbThanhtoanChitiet objKcbThanhtoanChitiet = KcbThanhtoanChitiet.FetchByID(item.id_chitiet_thanhtoan);
                            if (objKcbThanhtoanChitiet == null)
                            {
                                return ActionResult.Error;
                            }
                            //Thêm lịch sử trả lại thuốc
                            ThuocLichsuTralaithuoctaiquayChitiet tralai = new ThuocLichsuTralaithuoctaiquayChitiet();
                            tralai.IdChitietThanhtoan = -1;
                            tralai.IdChitietThanhtoanGoc = item.id_chitiet_thanhtoan;
                            tralai.IdChitietdonthuoc = item.id_chitiet_donthuoc;
                            tralai.IdDonthuoc = item.id_donthuoc;
                            tralai.IdThuoc = item.id_thuoc;
                            tralai.SoLuong = item.sl_tralai;
                            tralai.IdPhieuchi = -1;//Bộ phận TNV sẽ trả vào đây
                            tralai.DonGia = item.don_gia;
                            tralai.IdLoaithanhtoan = item.id_loaithanhtoan;
                            tralai.IdTralaithuoc = newPhieu.IdTralaithuoc;
                            tralai.NgayTao = newPhieu.NgayTao;
                            tralai.NguoiTao = newPhieu.NguoiTao;
                            tralai.IsNew = true;
                            tralai.Save();
                            
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh lap phieu tra thuoc tai quay:{0}", exception.ToString());
                return ActionResult.Exception;
            }
        }
        public ActionResult Tratien(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, List<Int64> lstIdChitiet,
          string malydohuy, string noidunghuy, string lydotratien)
        {
            decimal ptramBhyt = 0;
            ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
            decimal vDblTongtienHuy = 0;
            ///tổng tiền đã thanh toán
            decimal v_TotalPaymentDetail = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        decimal TT_BN = 0m;
                        decimal TT_BNCT = 0m;
                        decimal TT_PT = 0m;
                        decimal TT_TT = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        List<KcbThanhtoanChitiet> arrKcbThanhtoanChitietHuy =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIdChitiet)
                                .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();
                        //Thêm mới dòng thanh toán hủy
                        objPhieuchi.KieuThanhtoan = 1;
                        objPhieuchi.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objPhieuchi.NgayThanhtoan = DateTime.Now;
                        objPhieuchi.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(DateTime.Now);
                        objPhieuchi.MaLydoHuy = malydohuy;
                        objPhieuchi.TrangThai = 0;
                        objPhieuchi.IsNew = true;
                        objPhieuchi.Save();

                        //Reset và tính toán các số tiền liên quan đến các bản ghi hủy
                        TT_BN = 0m;
                        TT_BNCT = 0m;
                        TT_PT = 0m;
                        TT_TT = 0m;
                        TT_BHYT = 0m;
                        TT_Chietkhau_Chitiet = 0;
                        //Cập nhật các dòng chi tiết được chọn hủy về trạng thái hủy và các dịch vụ trong các bảng tương ứng theo id_loaithanhtoan
                        foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in arrKcbThanhtoanChitietHuy)
                        {
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra * Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);
                            TT_PT += objKcbThanhtoanChitiet.PhuThu * Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            if (Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_TT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            else
                                TT_BNCT += objKcbThanhtoanChitiet.BnhanChitra * Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            ///Phí Khám chữa bệnh))
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                            {
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDangkyKcb.Columns.IdKham)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                            }
                            ///Dịch vụ cận lâm sàng
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                            {
                                KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                    KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                    //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            ///Thuốc
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3 || objKcbThanhtoanChitiet.IdLoaithanhtoan == 5)
                            {
                                KcbDonthuocChitiet objKcbDonthuocChitiet =
                                    KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbDonthuocChitiet != null &&
                                        Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }
                                }
                                new Update(KcbDonthuoc.Schema)
                                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbDonthuoc.Columns.IdDonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            //Tạo dữ liệu hủy tiền

                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objPhieuchi.IdThanhtoan;
                            //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 1;
                            objKcbThanhtoanChitiet.TongTralai =Utility.Int32Dbnull( objKcbThanhtoanChitiet.SoLuong,0);//Ngoài thuốc tại quầy thì mặc định trả lại toàn bộ số lượng các dịch vụ khác
                            objKcbThanhtoanChitiet.IsNew = false;
                            objKcbThanhtoanChitiet.MarkOld();
                            objKcbThanhtoanChitiet.Save();
                        }
                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                        //Update lại tiền thanh toán
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                            .Set(KcbThanhtoan.Columns.TongtienChietkhauChitiet).EqualTo(TT_Chietkhau_Chitiet)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objPhieuchi.IdThanhtoan;
                        objPhieuthu.IdBenhnhan = objPhieuchi.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objPhieuchi.MaLuotkham;
                        objPhieuthu.MaPttt = objPhieuchi.MaPttt;
                        objPhieuthu.NoiDung = noidunghuy;
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 1);
                        objPhieuthu.NgayThuchien = DateTime.Now;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objPhieuchi.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objPhieuchi.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet > 0 ? objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet : 0;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objPhieuchi.NoiTru;
                        objPhieuthu.LydoNop = lydotratien;
                        objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
                        objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objPhieuthu.IsNew = true;
                        objPhieuthu.Save();
                         //Tạo bản ghi trong bảng phân bổ tiền theo phương thức thanh toán
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objPhieuchi.IdThanhtoan, -1l, -1l, objPhieuchi.MaPttt, objPhieuchi.MaNganhang,
                            objPhieuchi.IdBenhnhan, objPhieuchi.MaLuotkham,
                            objPhieuchi.NoiTru, objPhieuthu.SoTien, objPhieuthu.SoTien,
                            objPhieuchi.NguoiTao, objPhieuchi.NgayTao, "", objPhieuchi.NgayTao,-1l,0,(byte)1).Execute();
                        //Kết thúc tạo thông tin phiếu trả tiền(Phiếu chi)-->Kế tiếp cần tính toán lại % BHYT và tiền chênh lệch cho đối tượng BHYT
                        //Riêng đối tượng dịch vụ Giữ nguyên các giá trị thanh toán(Thực thu= Tổng thanh toán-Tổng trả lại)
                        if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        {
                            vDblTongtienHuy = TongtienKhongTutuc(arrKcbThanhtoanChitietHuy);
                            //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                            KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                new KcbThanhtoanController()
                                    .FetchByQuery(
                                        KcbThanhtoan.CreateQuery()
                                            .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                objLuotkham.MaLuotkham)
                                            .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                objLuotkham.IdBenhnhan)
                                            .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                    );
                            List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                                            select q.IdThanhtoan).Distinct().ToList();
                            //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                            var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                            if (lstIdThanhtoanAll.Count > 0)
                                lstKcbThanhtoanChitiet_Tatca =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                        .ToList<KcbThanhtoanChitiet>();

                            v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                                    where p.TuTuc == 0
                                                    select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong) * c.DonGia);

                            //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                            LayThongtinPtramBhyt(v_TotalPaymentDetail - vDblTongtienHuy, objLuotkham, ref ptramBhyt);


                            //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                            var lsttemp = new List<KcbThanhtoanChitiet>();
                            THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet_Tatca,
                                ptramBhyt);
                            List<long> lstIdThanhtoanTinhlai = (from q in lstKcbThanhtoanChitiet_Tatca
                                                                select q.IdThanhtoan).Distinct().ToList();
                            //99% đặt thông số này=1
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                    "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                            {
                                foreach (int IdThanhtoan in lstIdThanhtoanTinhlai)
                                //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                {
                                    TT_BN = 0m;
                                    TT_BNCT = 0m;
                                    TT_PT = 0m;
                                    TT_TT = 0m;
                                    TT_BHYT = 0m;
                                    TT_Chietkhau_Chitiet = 0;
                                    //Lấy lại từ CSDL
                                    List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                                                             where p.IdThanhtoan == IdThanhtoan
                                                                             select p).ToList<KcbThanhtoanChitiet>();

                                    if (_LstChitiet.Count > 0)
                                    {
                                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                        {
                                            objChitietThanhtoan.MarkOld();
                                            objChitietThanhtoan.IsNew = false;
                                            objChitietThanhtoan.Save();
                                            if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                            //Bỏ qua các bản ghi đã bị hủy
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_BHYT += objChitietThanhtoan.BhytChitra *
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                TT_Chietkhau_Chitiet +=
                                                    Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                TT_PT += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                    TT_TT += objChitietThanhtoan.BnhanChitra * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                else
                                                    TT_BNCT += objChitietThanhtoan.BnhanChitra *
                                                               Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            }
                                        }
                                        TT_BN += TT_PT + TT_BNCT + TT_TT;
                                        //Update lại tiền thanh toán
                                        new Update(KcbThanhtoan.Schema)
                                            .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                            .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                            .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                            .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                            .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        //Update phiếu thu
                                        new Update(KcbPhieuthu.Schema)
                                            .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                            .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                            .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
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
                log.Error("Loi trong qua trinh tra tien lai:{0}", exception.ToString());
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// Áp dụng đơn vị Dược làm chức năng thu ngân tại quầy
        /// </summary>
        /// <param name="objPhieuchi"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="lydohuy"></param>
        /// <returns></returns>
        public ActionResult HuyPhieuchiTrathuoctaiquay(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objPhieuchi != null)
                        {
                            DataTable dtDataChitiettralai = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                                     .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan)
                                     .ExecuteDataSet().Tables[0];
                            foreach (DataRow dr in dtDataChitiettralai.Rows)
                            {
                                long id_chitiet_thanhtoan = Utility.Int64Dbnull(dr[ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoanGoc]);
                                KcbThanhtoanChitiet objKcbThanhtoanChitiet = KcbThanhtoanChitiet.FetchByID(id_chitiet_thanhtoan);
                                if (objKcbThanhtoanChitiet == null)//Mất dòng chi tiết thanh toán do lỗi gì đó????
                                    return ActionResult.Error;
                                //Cập nhật số lượng trả lại cho dòng thanh toán

                                int tong_tralai = Utility.Int32Dbnull(objKcbThanhtoanChitiet.TongTralai, 0);
                                int sl_tralai = Utility.Int32Dbnull(dr["so_luong"], 0);
                                tong_tralai = tong_tralai - sl_tralai;
                                objKcbThanhtoanChitiet.IsNew = false;
                                objKcbThanhtoanChitiet.MarkOld();
                                objKcbThanhtoanChitiet.TongTralai = tong_tralai;
                                if (tong_tralai <= 0)
                                {
                                    objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                }
                                objKcbThanhtoanChitiet.Save();
                                //Cộng thuốc vào kho
                                DataTable dtchitietdonthuoc = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int64Dbnull(dr["id_chitietdonthuoc"])).ExecuteDataSet().Tables[0];
                                if (dtchitietdonthuoc.Rows.Count > 0)//Trừ thuốc khỏi kho để đưa lại cho khách hàng do hủy phiếu chi
                                {
                                    int num = SPs.ThuocTrathuoctaiquayvaokho(Utility.Int64Dbnull(dtchitietdonthuoc.Rows[0]["id_thuockho"]), Utility.Int32Dbnull(dtchitietdonthuoc.Rows[0]["id_thuoc"]), -1 * Utility.Int32Dbnull(dr["so_luong"])).Execute();
                                }
                                else//Chi tiết trong bảng đơn thuốc bị xóa do lỗi gì đó????
                                    return ActionResult.Cancel;

                            }
                            new Delete().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema).Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            new Delete().From(ThuocLichsuTralaithuoctaiquayPhieu.Schema).Where(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objPhieuchi.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objPhieuchi.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objPhieuchi.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objPhieuchi.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            new Delete().From(KcbThanhtoanChitiet.Schema)
                               .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                               .And(KcbThanhtoanChitiet.Columns.MultiCancel).IsEqualTo(1)
                               .Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsLessThanOrEqualTo(0)
                                .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdPhieuthu).IsLessThanOrEqualTo(0)
                                .Execute();
                            //Xóa dòng biến động thuốc trả lại
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            //Đối tượng Dịch vụ không cần khôi phục dữ liệu
                            //Đối tượng BHYT cần tính toán lại % BHYT để xác định có cần trả lại số tiền >Số tiền hủy cho BN hay không?
                            #region "BHYT"
                            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                            {
                                decimal TT_BN = 0m;
                                decimal TT_BNCT = 0m;
                                decimal TT_PT = 0m;
                                decimal TT_TT = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;

                                //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objLuotkham.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objLuotkham.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                        );
                                List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                                                select q.IdThanhtoan).Distinct().ToList();
                                //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                                var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                                if (lstIdThanhtoanAll.Count > 0)
                                    lstKcbThanhtoanChitiet_Tatca =
                                        new Select().From(KcbThanhtoanChitiet.Schema)
                                            .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                            .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                            .ToList<KcbThanhtoanChitiet>();

                                v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                                        where p.TuTuc == 0
                                                        select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong) * c.DonGia);

                                //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                                LayThongtinPtramBhyt(v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);


                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                var lsttemp = new List<KcbThanhtoanChitiet>();
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp,
                                    ref lstKcbThanhtoanChitiet_Tatca, PtramBHYT);
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                                {
                                    foreach (int IdThanhtoan in lstIdThanhtoanAll)
                                    //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                    {
                                        TT_BN = 0m;
                                        TT_BNCT = 0m;
                                        TT_PT = 0m;
                                        TT_TT = 0m;
                                        TT_BHYT = 0m;
                                        TT_Chietkhau_Chitiet = 0;
                                        //Lấy lại từ CSDL
                                        List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                                                                 where p.IdThanhtoan == IdThanhtoan
                                                                                 select p).ToList<KcbThanhtoanChitiet>();

                                        if (_LstChitiet.Count > 0)
                                        {
                                            foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                //Bỏ qua các bản ghi đã bị hủy
                                                {
                                                    if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_BHYT += objChitietThanhtoan.BhytChitra *
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    TT_Chietkhau_Chitiet +=
                                                        Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                    TT_PT += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_TT += objChitietThanhtoan.BnhanChitra *
                                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    else
                                                        TT_BNCT += objChitietThanhtoan.BnhanChitra *
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                }
                                            }
                                            TT_BN += TT_PT + TT_BNCT + TT_TT;
                                            //Update lại tiền thanh toán
                                            new Update(KcbThanhtoan.Schema)
                                                .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                                .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                                .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                                .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                                .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                                .Where(KcbThanhtoan.Columns.IdThanhtoan)
                                                .IsEqualTo(IdThanhtoan)
                                                .Execute();
                                            //Update phiếu thu
                                            new Update(KcbPhieuthu.Schema)
                                                .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                                .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// Áp dụng cho đơn vị TNV và Dược tại quầy là 2 bộ phận làm cv riêng biệt
        /// </summary>
        /// <param name="objPhieuchi"></param>
        /// <param name="objLuotkham"></param>
        /// <param name="lydohuy"></param>
        /// <returns></returns>
        public ActionResult HuyPhieuchiTrathuoctaiquay_Rieng(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objPhieuchi != null)
                        {
                           DataTable dtDataChitiettralai = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                                    .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan)
                                    .ExecuteDataSet().Tables[0];
                            //Cập nhật trạng thái phiếu trả thuốc tại quầy về lúc Dược mới đề xuất
                           new Update(ThuocLichsuTralaithuoctaiquayPhieu.Schema)
                           .Set(ThuocLichsuTralaithuoctaiquayPhieu.Columns.TrangThai).EqualTo(0)
                           .Set(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdPhieuchi).EqualTo(-1)
                           .Where(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan)
                           .Execute();
                            foreach (DataRow dr in dtDataChitiettralai.Rows)
                            {
                                long id = Utility.Int64Dbnull(dr[ThuocLichsuTralaithuoctaiquayChitiet.Columns.Id]);
                                long id_chitiet_thanhtoan = Utility.Int64Dbnull(dr[ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoanGoc]);
                                KcbThanhtoanChitiet objKcbThanhtoanChitiet = KcbThanhtoanChitiet.FetchByID(id_chitiet_thanhtoan);
                                if (objKcbThanhtoanChitiet == null)//Mất dòng chi tiết thanh toán do lỗi gì đó????
                                    return ActionResult.Error;
                                //Cập nhật số lượng trả lại cho dòng thanh toán
                               
                                int tong_tralai = Utility.Int32Dbnull(objKcbThanhtoanChitiet.TongTralai, 0);
                                int sl_tralai = Utility.Int32Dbnull(dr["so_luong"], 0);
                                tong_tralai = tong_tralai - sl_tralai;
                                objKcbThanhtoanChitiet.IsNew = false;
                                objKcbThanhtoanChitiet.MarkOld();
                                objKcbThanhtoanChitiet.TongTralai = tong_tralai;
                                if (tong_tralai <= 0)
                                {
                                    objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                }
                                objKcbThanhtoanChitiet.Save();
                                //Cập nhật chi tiết trả lại
                                new Update(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                                .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoan).EqualTo(-1)
                                .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).EqualTo(-1)
                                .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.Id).IsEqualTo(id).Execute();
                                //Cộng thuốc vào kho
                                DataTable dtchitietdonthuoc = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(Utility.Int64Dbnull(dr["id_chitietdonthuoc"])).ExecuteDataSet().Tables[0];
                                if (dtchitietdonthuoc.Rows.Count > 0)//Trừ thuốc khỏi kho để đưa lại cho khách hàng do hủy phiếu chi
                                {
                                    int num = SPs.ThuocTrathuoctaiquayvaokho(Utility.Int64Dbnull(dtchitietdonthuoc.Rows[0]["id_thuockho"]), Utility.Int32Dbnull(dtchitietdonthuoc.Rows[0]["id_thuoc"]),-1* Utility.Int32Dbnull(dr["so_luong"])).Execute();
                                }
                                else//Chi tiết trong bảng đơn thuốc bị xóa do lỗi gì đó????
                                    return ActionResult.Cancel;
                                
                            }
                           
                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objPhieuchi.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objPhieuchi.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objPhieuchi.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objPhieuchi.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            new Delete().From(KcbThanhtoanChitiet.Schema)
                               .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                               .And(KcbThanhtoanChitiet.Columns.MultiCancel).IsEqualTo(1)
                               .Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsLessThanOrEqualTo(0)
                                .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdPhieuthu).IsLessThanOrEqualTo(0)
                                .Execute();
                            //Xóa dòng biến động thuốc trả lại
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            //Đối tượng Dịch vụ không cần khôi phục dữ liệu
                            //Đối tượng BHYT cần tính toán lại % BHYT để xác định có cần trả lại số tiền >Số tiền hủy cho BN hay không?
                            #region "BHYT"
                            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                            {
                                decimal TT_BN = 0m;
                                decimal TT_BNCT = 0m;
                                decimal TT_PT = 0m;
                                decimal TT_TT = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;

                                //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objLuotkham.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objLuotkham.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                        );
                                List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                                                select q.IdThanhtoan).Distinct().ToList();
                                //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                                var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                                if (lstIdThanhtoanAll.Count > 0)
                                    lstKcbThanhtoanChitiet_Tatca =
                                        new Select().From(KcbThanhtoanChitiet.Schema)
                                            .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                            .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                            .ToList<KcbThanhtoanChitiet>();

                                v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                                        where p.TuTuc == 0
                                                        select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong) * c.DonGia);

                                //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                                LayThongtinPtramBhyt(v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);


                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                var lsttemp = new List<KcbThanhtoanChitiet>();
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp,
                                    ref lstKcbThanhtoanChitiet_Tatca, PtramBHYT);
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                                {
                                    foreach (int IdThanhtoan in lstIdThanhtoanAll)
                                    //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                    {
                                        TT_BN = 0m;
                                        TT_BNCT = 0m;
                                        TT_PT = 0m;
                                        TT_TT = 0m;
                                        TT_BHYT = 0m;
                                        TT_Chietkhau_Chitiet = 0;
                                        //Lấy lại từ CSDL
                                        List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                                                                 where p.IdThanhtoan == IdThanhtoan
                                                                                 select p).ToList<KcbThanhtoanChitiet>();

                                        if (_LstChitiet.Count > 0)
                                        {
                                            foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                //Bỏ qua các bản ghi đã bị hủy
                                                {
                                                    if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_BHYT += objChitietThanhtoan.BhytChitra *
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    TT_Chietkhau_Chitiet +=
                                                        Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                    TT_PT += objChitietThanhtoan.PhuThu * Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_TT += objChitietThanhtoan.BnhanChitra *
                                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    else
                                                        TT_BNCT += objChitietThanhtoan.BnhanChitra *
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                }
                                            }
                                            TT_BN += TT_PT + TT_BNCT + TT_TT;
                                            //Update lại tiền thanh toán
                                            new Update(KcbThanhtoan.Schema)
                                                .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                                .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                                .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                                .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                                .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                                .Where(KcbThanhtoan.Columns.IdThanhtoan)
                                                .IsEqualTo(IdThanhtoan)
                                                .Execute();
                                            //Update phiếu thu
                                            new Update(KcbPhieuthu.Schema)
                                                .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                                .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }
          public ActionResult HuyPhieuchi(KcbThanhtoan objPhieuchi, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objPhieuchi != null)
                        {
                            var lstKcbThanhtoanChitiet_Phieuchi =
                                new Select().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoanhuy)
                                    .IsEqualTo(objPhieuchi.IdThanhtoan)
                                    .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                            List<long> lstIDThanhtoanKhoiphuc = (from p in lstKcbThanhtoanChitiet_Phieuchi
                                select Utility.Int64Dbnull(p.IdThanhtoan, -1)).ToList<long>();

                            //Khôi phục lại trạng thái hủy
                            foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in lstKcbThanhtoanChitiet_Phieuchi)
                            {
                                objKcbThanhtoanChitiet.IsNew = false;
                                objKcbThanhtoanChitiet.MarkOld();
                                objKcbThanhtoanChitiet.TongTralai = 0;
                                objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                objKcbThanhtoanChitiet.Save();
                                
                            
                                ///thanh toán khám chữa bệnh))
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                                {
                                    new Update(KcbDangkyKcb.Schema)
                                        .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbDangkyKcb.Columns.IdKham)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                        .Execute();
                                }
                                ///thah toán phần dịch vụ cận lâm sàng
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                                {
                                    KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                        KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                        //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }

                                    new Update(KcbChidinhclsChitiet.Schema)
                                        .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                        .Execute();
                                }
                                ///thanh toán phần thuốc
                                if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                                {
                                    KcbDonthuocChitiet objKcbDonthuocChitiet =
                                        KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                    //Bỏ 20240412
                                    //if (objKcbDonthuocChitiet != null &&
                                    //    Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    //{
                                    //    return ActionResult.PresIsConfirmed;
                                    //}

                                    new Update(KcbDonthuocChitiet.Schema)
                                        .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                        .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                        .Execute();
                                }
                            }
                            //Cập nhật trạng thái liên quan đến trả thuốc tại quầy
                            new Update(ThuocLichsuTralaithuoctaiquayPhieu.Schema)
                          .Set(ThuocLichsuTralaithuoctaiquayPhieu.Columns.TrangThai).EqualTo(0)
                          .Set(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdPhieuchi).EqualTo(-1)
                          .Where(ThuocLichsuTralaithuoctaiquayPhieu.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan)
                          .Execute();
                            new Update(ThuocLichsuTralaithuoctaiquayChitiet.Schema)
                               .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietThanhtoan).EqualTo(-1)
                               .Set(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).EqualTo(-1)
                               .Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdPhieuchi).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();

                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objPhieuchi.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objPhieuchi.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objPhieuchi.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objPhieuchi.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan).Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                            new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema)
                                 .Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objPhieuchi.IdThanhtoan)
                                 .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdTamung).IsLessThanOrEqualTo(0)
                                 .And(KcbThanhtoanPhanbotheoPTTT.Columns.IdPhieuthu).IsLessThanOrEqualTo(0).Execute();
                            //Đối tượng Dịch vụ không cần khôi phục dữ liệu
                            //Đối tượng BHYT cần tính toán lại % BHYT để xác định có cần trả lại số tiền >Số tiền hủy cho BN hay không?

                            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                            {
                                decimal TT_BN = 0m;
                                decimal TT_BNCT = 0m;
                                decimal TT_PT = 0m;
                                decimal TT_TT = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;

                                //Thường chỉ trả về 1 bản ghi thanh toán duy nhất vì là đối tượng BHYT
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objLuotkham.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objLuotkham.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals, 0)
                                        );
                                List<long> lstIdThanhtoanAll = (from q in lstKcbThanhtoanCollection
                                    select q.IdThanhtoan).Distinct().ToList();
                                //Biến chứa danh sách tất cả các chi tiết dùng để tính lại tổng tiền thanh toán cho thanh toán có bản ghi bị hủy
                                var lstKcbThanhtoanChitiet_Tatca = new List<KcbThanhtoanChitiet>();
                                if (lstIdThanhtoanAll.Count > 0)
                                    lstKcbThanhtoanChitiet_Tatca =
                                        new Select().From(KcbThanhtoanChitiet.Schema)
                                            .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).In(lstIdThanhtoanAll)
                                            .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                            .ToList<KcbThanhtoanChitiet>();

                                v_TotalPaymentDetail = (from p in lstKcbThanhtoanChitiet_Tatca
                                    where p.TuTuc == 0
                                    select p).Sum(c => Utility.DecimaltoDbnull(c.SoLuong)*c.DonGia);

                                //Tính lại % BHYT mới sau khi đã trả lại tiền một số dịch vụ
                                LayThongtinPtramBhyt(v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);


                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                var lsttemp = new List<KcbThanhtoanChitiet>();
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp,
                                    ref lstKcbThanhtoanChitiet_Tatca, PtramBHYT);
                                if (
                                    THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                        "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                                {
                                    foreach (int IdThanhtoan in lstIdThanhtoanAll)
                                        //Chỉ thực hiện tính lại thanh toán có chứa các chi tiết bị thay đổi
                                    {
                                        TT_BN = 0m;
                                        TT_BNCT = 0m;
                                        TT_PT = 0m;
                                        TT_TT = 0m;
                                        TT_BHYT = 0m;
                                        TT_Chietkhau_Chitiet = 0;
                                        //Lấy lại từ CSDL
                                        List<KcbThanhtoanChitiet> _LstChitiet = (from p in lstKcbThanhtoanChitiet_Tatca
                                            where p.IdThanhtoan == IdThanhtoan
                                            select p).ToList<KcbThanhtoanChitiet>();

                                        if (_LstChitiet.Count > 0)
                                        {
                                            foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                            {
                                                if (!Utility.Byte2Bool(objChitietThanhtoan.TrangthaiHuy))
                                                    //Bỏ qua các bản ghi đã bị hủy
                                                {
                                                    if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_BHYT += objChitietThanhtoan.BhytChitra*
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    TT_Chietkhau_Chitiet +=
                                                        Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                                    TT_PT += objChitietThanhtoan.PhuThu*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    if (Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                        TT_TT += objChitietThanhtoan.BnhanChitra*
                                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                    else
                                                        TT_BNCT += objChitietThanhtoan.BnhanChitra*
                                                                   Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                                }
                                            }
                                            TT_BN += TT_PT + TT_BNCT + TT_TT;
                                            //Update lại tiền thanh toán
                                            new Update(KcbThanhtoan.Schema)
                                                .Set(KcbThanhtoan.Columns.TongTien).EqualTo(TT_BHYT + TT_BN)
                                                .Set(KcbThanhtoan.Columns.BnhanChitra).EqualTo(TT_BNCT)
                                                .Set(KcbThanhtoan.Columns.BhytChitra).EqualTo(TT_BHYT)
                                                .Set(KcbThanhtoan.Columns.PhuThu).EqualTo(TT_PT)
                                                .Set(KcbThanhtoan.Columns.TuTuc).EqualTo(TT_TT)
                                                .Where(KcbThanhtoan.Columns.IdThanhtoan)
                                                .IsEqualTo(IdThanhtoan)
                                                .Execute();
                                            //Update phiếu thu
                                            new Update(KcbPhieuthu.Schema)
                                                .Set(KcbPhieuthu.Columns.SoTien).EqualTo(TT_BN - TT_Chietkhau_Chitiet)
                                                .Set(KcbPhieuthu.Columns.SotienGoc).EqualTo(TT_BN)
                                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(IdThanhtoan).Execute();
                                        }
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
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult Tratien_Old(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, List<Int64> lstIdChitiet,
            string malydohuy, string noidunghuy, string lydotratien)
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
                        List<KcbThanhtoanChitiet> ArrKcbThanhtoanChitiet_Huy =
                            new Select().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).In(lstIdChitiet)
                                .ExecuteAsCollection<KcbThanhtoanChitietCollection>().ToList<KcbThanhtoanChitiet>();

                        List<long> lstIdThanhtoanTinhtoanlai = (from q in ArrKcbThanhtoanChitiet_Huy
                            select q.IdThanhtoan).ToList<long>();

                        v_dblTongtienHuy = TongtienKhongTutuc(ArrKcbThanhtoanChitiet_Huy);
                        KcbThanhtoanCollection lstKcbThanhtoanCollection =
                            new KcbThanhtoanController()
                                .FetchByQuery(
                                    KcbThanhtoan.CreateQuery()
                                        .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                            objLuotkham.MaLuotkham)
                                        .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals, objLuotkham.IdBenhnhan)
                                        .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                            objThanhtoan.KieuThanhtoan)
                                ); //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                        //Lấy tổng tiền của các lần thanh toán trước
                        var lstKcbThanhtoanChitiet = new List<KcbThanhtoanChitiet>();

                        foreach (KcbThanhtoan Payment in lstKcbThanhtoanCollection)
                        {
                            var lstKcbThanhtoanChitietCollection = new Select().From(KcbThanhtoanChitiet.Schema)
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
                                    v_TotalPaymentDetail += Utility.Int32Dbnull(_KcbThanhtoanChitiet.SoLuong)*
                                                            Utility.DecimaltoDbnull(_KcbThanhtoanChitiet.DonGia);
                                }
                            }
                        }
                        List<long> lstIdThanhtoanCu = (from q in lstKcbThanhtoanChitiet
                            select q.IdThanhtoan).Distinct().ToList();


                        LayThongtinPtramBhyt(v_TotalPaymentDetail - v_dblTongtienHuy, objLuotkham, ref PtramBHYT);
                        //Thêm mới dòng thanh toán hủy
                        objThanhtoan.KieuThanhtoan = 1;
                        objThanhtoan.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                        objThanhtoan.NgayThanhtoan = DateTime.Now;
                        objThanhtoan.MaThanhtoan = THU_VIEN_CHUNG.TaoMathanhtoan(DateTime.Now);
                        objThanhtoan.MaLydoHuy = malydohuy;
                        objThanhtoan.IsNew = true;
                        objThanhtoan.Save();
                        //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                        var lsttemp = new List<KcbThanhtoanChitiet>();
                        THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lsttemp, ref lstKcbThanhtoanChitiet, PtramBHYT);
                        decimal TT_BN = 0m;
                        decimal TT_BHYT = 0m;
                        decimal TT_Chietkhau_Chitiet = 0m;
                        //99% đặt thông số này=1
                        if (
                            THU_VIEN_CHUNG.Laygiatrithamsohethong(
                                "KCB_THANHTOAN_TINHLAITONGTIEN_CACTHANHTOAN_BITRALAITIEN", "1", false) == "1")
                        {
                            foreach (int IdThanhtoan in lstIdThanhtoanCu)
                            {
                                TT_BN = 0m;
                                TT_BHYT = 0m;
                                TT_Chietkhau_Chitiet = 0m;
                                List<KcbThanhtoanChitiet> _LstChitiet = (from q in lstKcbThanhtoanChitiet
                                    where q.IdThanhtoan == IdThanhtoan
                                    select q).ToList<KcbThanhtoanChitiet>();

                                if (_LstChitiet.Count > 0)
                                {
                                    foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                    {
                                        TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                                 Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                            TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                        TT_Chietkhau_Chitiet +=
                                            Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                        //Lưu lại các thông tin tiền đã được tính toán lại ở thủ tục THU_VIEN_CHUNG.TinhPhamTramBHYT(...)
                                        objChitietThanhtoan.IsNew = false;
                                        objChitietThanhtoan.MarkOld();
                                        objChitietThanhtoan.Save();
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
                            TT_BN += (objKcbThanhtoanChitiet.BnhanChitra + objKcbThanhtoanChitiet.PhuThu)*
                                     Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            if (!Utility.Byte2Bool(objKcbThanhtoanChitiet.TuTuc))
                                TT_BHYT += objKcbThanhtoanChitiet.BhytChitra*Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.SoLuong);
                            TT_Chietkhau_Chitiet += Utility.DecimaltoDbnull(objKcbThanhtoanChitiet.TienChietkhau, 0);

                            new Update(KcbThanhtoanChitiet.Schema)
                                .Set(KcbThanhtoanChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet)
                                .IsEqualTo(objKcbThanhtoanChitiet.IdChitiet)
                                .
                                Execute();
                            ///thanh toán khám chữa bệnh))
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                            {
                                new Update(KcbDangkyKcb.Schema)
                                    .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDangkyKcb.Columns.IdKham)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                            }
                            ///thah toán phần dịch vụ cận lâm sàng
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                            {
                                KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                    KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai >= 3)
                                        //Đã có kết quả
                                    {
                                        return ActionResult.AssignIsConfirmed;
                                    }
                                }
                                new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            ///thanh toán phần thuốc
                            if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                            {
                                KcbDonthuocChitiet objKcbDonthuocChitiet =
                                    KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                if (globalVariables.UserName != "ADMIN")
                                {
                                    if (objKcbDonthuocChitiet != null &&
                                        Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                    {
                                        return ActionResult.PresIsConfirmed;
                                    }
                                }
                                new Update(KcbDonthuoc.Schema)
                                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbDonthuoc.Columns.IdDonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                    .Execute();
                                new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(1)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                    .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                    .Execute();
                            }
                            //Tạo dữ liệu hủy tiền

                            objKcbThanhtoanChitiet.IdThanhtoanhuy = objThanhtoan.IdThanhtoan;
                                //Để biết dòng hủy này hủy cho chi tiết thanh toán nào
                            objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                            objKcbThanhtoanChitiet.IsNew = true;
                            objKcbThanhtoanChitiet.Save();
                        }

                        var objPhieuthu = new KcbPhieuthu();
                        objPhieuthu.IdThanhtoan = objThanhtoan.IdThanhtoan;
                        objPhieuthu.IdBenhnhan = objThanhtoan.IdBenhnhan;
                        objPhieuthu.MaLuotkham = objThanhtoan.MaLuotkham;
                        objPhieuthu.NoiDung = noidunghuy;
                        objPhieuthu.SoluongChungtugoc = 1;
                        objPhieuthu.LoaiPhieuthu = Convert.ToByte(1); //0= phiếu thu tiền;1= phiếu chi
                        objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 1);
                        objPhieuthu.NgayThuchien = DateTime.Now;
                        objPhieuthu.SoTien = TT_BN - TT_Chietkhau_Chitiet;
                        objPhieuthu.SotienGoc = TT_BN;
                        objPhieuthu.MaLydoChietkhau = objThanhtoan.MaLydoChietkhau;
                        objPhieuthu.TienChietkhauchitiet = TT_Chietkhau_Chitiet;
                        objPhieuthu.TienChietkhau = objThanhtoan.TongtienChietkhau;
                        objPhieuthu.TienChietkhauhoadon = objPhieuthu.TienChietkhau - objPhieuthu.TienChietkhauchitiet;
                        objPhieuthu.NguoiNop = globalVariables.UserName;
                        objPhieuthu.TaikhoanCo = "";
                        objPhieuthu.TaikhoanNo = "";
                        objPhieuthu.NoiTru = objThanhtoan.NoiTru;
                        objPhieuthu.LydoNop = lydotratien;
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

        public ActionResult UpdatePhieuDCT(KcbPhieuDct objPhieuDct, KcbLuotkham objLuotkham)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From<KcbPhieuDct>()
                            .Where(KcbPhieuDct.Columns.MaLuotkham)
                            .IsEqualTo(objPhieuDct.MaLuotkham)
                            .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan)
                            .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(objPhieuDct.KieuThanhtoan);
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            //objPhieuDct.IsNew = true;
                            //objPhieuDct.Save();
                          StoredProcedure sp =   SPs.SpKcbPhieudctInsert(objPhieuDct.MaPhieuDct, objPhieuDct.MaLuotkham,
                                objPhieuDct.IdBenhnhan, objPhieuDct.TongTien, objPhieuDct.BhytChitra,
                                objPhieuDct.BnhanChitra, objPhieuDct.NguonkhacChitra, objPhieuDct.NguoiTao, objPhieuDct.NgayTao, objPhieuDct.NgaySua,
                                objPhieuDct.NguoiSua, objPhieuDct.KieuThanhtoan, objPhieuDct.MaKhoaThuchien
                                , objPhieuDct.TenKieuthanhtoan, objPhieuDct.IdThe, objPhieuDct.MatheBhyt,
                                objPhieuDct.IpMaytao, objPhieuDct.IpMaysua, objPhieuDct.TenMaytao, objPhieuDct.TenMaysua, objPhieuDct.MaCoso,
                                objPhieuDct.TrangthaiXml);
                            sp.Execute();
                            objLuotkham.TrangthaiNgoaitru = 1;
                            objLuotkham.Locked = 1;
                            objLuotkham.TthaiInphoi = 1;
                            objLuotkham.NgayInphoi = objPhieuDct.NgayTao;
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                                .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(KcbLuotkham.Columns.Locked).EqualTo(objLuotkham.Locked)
                                .Set(KcbLuotkham.Columns.TthaiInphoi).EqualTo(objLuotkham.TthaiInphoi)
                                .Set(KcbLuotkham.Columns.NgayInphoi).EqualTo(objPhieuDct.NgayTao)
                                .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(Utility.Int32Dbnull(objLuotkham.TrangthaiNgoaitru))
                                .Set(KcbLuotkham.Columns.LydoKetthuc).EqualTo("In phôi bảo hiểm")
                                .Set(KcbLuotkham.Columns.IpMaysua).EqualTo(objPhieuDct.IpMaysua)
                                .Set(KcbLuotkham.Columns.TenMaysua).EqualTo(objPhieuDct.TenMaysua)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhieuDct.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan).Execute();
                        }
                        else
                        {
                            StoredProcedure sp = SPs.SpKcbPhieudctUpdate(objPhieuDct.MaLuotkham, objPhieuDct.IdBenhnhan,
                                objPhieuDct.TongTien, objPhieuDct.BhytChitra, objPhieuDct.BnhanChitra,
                                DateTime.Now, globalVariables.UserName
                                , objPhieuDct.KieuThanhtoan, globalVariables.gv_strIPAddress,
                                globalVariables.gv_strComputerName);
                            sp.Execute();
                            //new Update(KcbPhieuDct.Schema)
                            //    .Set(KcbPhieuDct.Columns.TongTien).EqualTo(objPhieuDct.TongTien)
                            //    .Set(KcbPhieuDct.Columns.BnhanChitra).EqualTo(objPhieuDct.BnhanChitra)
                            //    .Set(KcbPhieuDct.Columns.BhytChitra).EqualTo(objPhieuDct.BhytChitra)
                            //    .Set(KcbPhieuDct.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            //    .Set(KcbPhieuDct.Columns.IpMaysua).EqualTo(objPhieuDct.IpMaysua)
                            //    .Set(KcbPhieuDct.Columns.TenMaysua).EqualTo(objPhieuDct.TenMaysua)
                            //    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objPhieuDct.MaLuotkham)
                            //    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objPhieuDct.IdBenhnhan)
                            //    .And(KcbPhieuDct.Columns.LoaiThanhtoan)
                            //    .IsEqualTo(Utility.Int32Dbnull(objPhieuDct.LoaiThanhtoan))
                            //    .Execute();
                        }
                        //}
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

        public ActionResult HuyPhieuchi_old(KcbThanhtoan objThanhtoan, KcbLuotkham objLuotkham, string lydohuy)
        {
            try
            {
                decimal PtramBHYT = 0;
                ///tổng tiền hiện tại truyền vào của lần payment đang thực hiện
                decimal v_dblTongtienHuy = 0;
                ///tổng tiền đã thanh toán
                decimal v_TotalPaymentDetail = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objThanhtoan != null)
                        {
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KHOIPHUCLAIDULIEU_KHIHUYPHIEUCHI",
                                    "0", false) == "1")
                            {
                                KcbThanhtoanCollection lstKcbThanhtoanCollection =
                                    new KcbThanhtoanController()
                                        .FetchByQuery(
                                            KcbThanhtoan.CreateQuery()
                                                .AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objThanhtoan.MaLuotkham)
                                                .AND(KcbThanhtoan.Columns.IdBenhnhan, Comparison.Equals,
                                                    objThanhtoan.IdBenhnhan)
                                                .AND(KcbThanhtoan.Columns.KieuThanhtoan, Comparison.Equals,
                                                    objThanhtoan.KieuThanhtoan)
                                        );
                                    //Chỉ lấy về các bản ghi thanh toán thường(0= thường;1= thanh toán hủy(trả lại tiền))
                                //Lấy tổng tiền của các lần thanh toán trước
                                var lstKcbThanhtoanChitiet_KhoiphucChitra = new List<KcbThanhtoanChitiet>();

                                foreach (KcbThanhtoan Payment in lstKcbThanhtoanCollection)
                                {
                                    var lstKcbThanhtoanChitietCollection = new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(Payment.IdThanhtoan)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                                    foreach (
                                        KcbThanhtoanChitiet _KcbThanhtoanChitiet in lstKcbThanhtoanChitietCollection)
                                    {
                                        //Tính các khoản chi tiết đồng chi trả<->Tự túc=0
                                        if (_KcbThanhtoanChitiet.TuTuc == 0)
                                        {
                                            lstKcbThanhtoanChitiet_KhoiphucChitra.Add(_KcbThanhtoanChitiet);
                                            _KcbThanhtoanChitiet.IsNew = false;
                                            _KcbThanhtoanChitiet.MarkOld();
                                            //Tính tiền các khoản có BHYT chi trả
                                            if (!Utility.Byte2Bool(_KcbThanhtoanChitiet.TrangthaiHuy))
                                                v_TotalPaymentDetail +=
                                                    Utility.Int32Dbnull(_KcbThanhtoanChitiet.SoLuong)*
                                                    Utility.DecimaltoDbnull(_KcbThanhtoanChitiet.DonGia);
                                        }
                                    }
                                }

                                var lstKcbThanhtoanChitiet_Phieuchi =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                        .IsEqualTo(objThanhtoan.IdThanhtoan)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>();

                                List<long> lstIDChitiethuy = (from p in lstKcbThanhtoanChitiet_Phieuchi
                                    select Utility.Int64Dbnull(p.IdThanhtoanhuy, -1)).ToList<long>();
                                List<KcbThanhtoanChitiet> lstKcbThanhtoanChitiet_Huy =
                                    new Select().From(KcbThanhtoanChitiet.Schema)
                                        .Where(KcbThanhtoanChitiet.Columns.IdChitiet)
                                        .In(lstIDChitiethuy)
                                        .ExecuteAsCollection<KcbThanhtoanChitietCollection>()
                                        .ToList<KcbThanhtoanChitiet>();

                                v_dblTongtienHuy = TongtienKhongTutuc(lstKcbThanhtoanChitiet_Huy);
                                LayThongtinPtramBhyt(v_dblTongtienHuy + v_TotalPaymentDetail, objLuotkham, ref PtramBHYT);
                                //Tính lại thông tin BHYT,BN chi trả cho toàn bộ các chi tiết của BN đã thanh toán mà không bị hủy
                                THU_VIEN_CHUNG.TinhPhamTramBHYT(objLuotkham, ref lstKcbThanhtoanChitiet_Huy,
                                    ref lstKcbThanhtoanChitiet_KhoiphucChitra, PtramBHYT);

                                //Tính lại tổng tiền cho tất cả các lần thanh toán cũ
                                List<long> lstIdThanhtoanCu = (from q in lstKcbThanhtoanChitiet_KhoiphucChitra
                                    select Utility.Int64Dbnull(q.IdThanhtoan, -1)).Distinct().ToList();
                                decimal TT_BN = 0m;
                                decimal TT_BHYT = 0m;
                                decimal TT_Chietkhau_Chitiet = 0m;
                                foreach (int IdThanhtoan in lstIdThanhtoanCu)
                                {
                                    TT_BN = 0m;
                                    TT_BHYT = 0m;
                                    TT_Chietkhau_Chitiet = 0m;
                                    List<KcbThanhtoanChitiet> _LstChitiet =
                                        (from q in lstKcbThanhtoanChitiet_KhoiphucChitra
                                            where q.IdThanhtoan == IdThanhtoan
                                            select q).ToList<KcbThanhtoanChitiet>();

                                    if (_LstChitiet.Count > 0)
                                    {
                                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in _LstChitiet)
                                        {
                                            TT_BN += (objChitietThanhtoan.BnhanChitra + objChitietThanhtoan.PhuThu)*
                                                     Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            if (!Utility.Byte2Bool(objChitietThanhtoan.TuTuc))
                                                TT_BHYT += objChitietThanhtoan.BhytChitra*Utility.DecimaltoDbnull(objChitietThanhtoan.SoLuong);
                                            TT_Chietkhau_Chitiet +=
                                                Utility.DecimaltoDbnull(objChitietThanhtoan.TienChietkhau, 0);
                                            //Lưu lại các thông tin tiền đã được tính toán lại ở thủ tục THU_VIEN_CHUNG.TinhPhamTramBHYT(...)
                                            objChitietThanhtoan.IsNew = false;
                                            objChitietThanhtoan.MarkOld();
                                            objChitietThanhtoan.Save();
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
                                //Khôi phục lại trạng thái hủy
                                foreach (KcbThanhtoanChitiet objKcbThanhtoanChitiet in lstKcbThanhtoanChitiet_Huy)
                                {
                                    objKcbThanhtoanChitiet.IsNew = false;
                                    objKcbThanhtoanChitiet.MarkOld();
                                    objKcbThanhtoanChitiet.TrangthaiHuy = 0;
                                    objKcbThanhtoanChitiet.IdThanhtoanhuy = -1;
                                    objKcbThanhtoanChitiet.Save();

                                    ///thanh toán khám chữa bệnh))
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 1)
                                    {
                                        new Update(KcbDangkyKcb.Schema)
                                            .Set(KcbDangkyKcb.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbDangkyKcb.Columns.IdKham)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieu)
                                            .Execute();
                                    }
                                    ///thah toán phần dịch vụ cận lâm sàng
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 2)
                                    {
                                        KcbChidinhclsChitiet objKcbChidinhclsChitiet =
                                            KcbChidinhclsChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);
                                        if (globalVariables.UserName != "ADMIN")
                                        {
                                            if (objKcbChidinhclsChitiet != null &&
                                                objKcbChidinhclsChitiet.TrangThai >= 3) //Đã có kết quả
                                            {
                                                return ActionResult.AssignIsConfirmed;
                                            }
                                        }
                                        new Update(KcbChidinhclsChitiet.Schema)
                                            .Set(KcbChidinhclsChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                            .Execute();
                                    }
                                    ///thanh toán phần thuốc
                                    if (objKcbThanhtoanChitiet.IdLoaithanhtoan == 3)
                                    {
                                        KcbDonthuocChitiet objKcbDonthuocChitiet =
                                            KcbDonthuocChitiet.FetchByID(objKcbThanhtoanChitiet.IdPhieuChitiet);

                                        if (globalVariables.UserName != "ADMIN")
                                        {
                                            if (objKcbDonthuocChitiet != null &&
                                                Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                                            {
                                                return ActionResult.PresIsConfirmed;
                                            }
                                        }
                                        new Update(KcbDonthuocChitiet.Schema)
                                            .Set(KcbDonthuocChitiet.Columns.TrangthaiHuy).EqualTo(0)
                                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
                                            .IsEqualTo(objKcbThanhtoanChitiet.IdPhieuChitiet)
                                            .Execute();
                                    }
                                }
                            }
                            //Ghi lại log hủy
                            var objKcbLoghuy = new KcbLoghuy();
                            objKcbLoghuy.IdBenhnhan = objThanhtoan.IdBenhnhan;
                            objKcbLoghuy.MaLuotkham = objThanhtoan.MaLuotkham;
                            objKcbLoghuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                            objKcbLoghuy.SotienHuy = objThanhtoan.TongTien;
                            objKcbLoghuy.LydoHuy = lydohuy;
                            objKcbLoghuy.NgayHuy = DateTime.Now;
                            objKcbLoghuy.NgayTao = DateTime.Now;
                            objKcbLoghuy.NguoiTao = globalVariables.UserName;
                            objKcbLoghuy.IsNew = true;
                            objKcbLoghuy.LoaiphieuHuy = Utility.ByteDbnull(objThanhtoan.KieuThanhtoan, 0);
                            objKcbLoghuy.Save();
                            //Xóa các thông tin phiếu chi
                            new Delete().From(KcbThanhtoan.Schema)
                                .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                            new Delete().From(KcbThanhtoanChitiet.Schema)
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(objThanhtoan.IdThanhtoan)
                                .Execute();
                            new Delete().From(KcbPhieuthu.Schema)
                                .Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                                .And(KcbPhieuthu.Columns.LoaiPhieuthu).IsEqualTo(1).Execute();
                        }
                        else
                        {
                            return ActionResult.Error;
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateNgayThanhtoan(KcbThanhtoan objThanhtoan,string content="")
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        
                    
                        StoredProcedure sp = SPs.SpUpdateNgaythanhtoan(objThanhtoan.IdThanhtoan,
                            objThanhtoan.NgayThanhtoan);
                        sp.Execute();
                        Utility.Log("Thanh toán", globalVariables.UserName, content, newaction.Update, "frm_THANHTOAN_NGOAITRU");
                        #region update ngày thanh toán old
                        //new Update(KcbThanhtoan.Schema)
                        //    .Set(KcbThanhtoan.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //new Update(KcbDangkyKcb.Schema)
                        //    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(KcbDangkyKcb.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //new Update(KcbChidinhclsChitiet.Schema)
                        //    .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(KcbChidinhclsChitiet.Columns.IdThanhtoan)
                        //    .IsEqualTo(objThanhtoan.IdThanhtoan)
                        //    .Execute();
                        //new Update(KcbDonthuocChitiet.Schema)
                        //    .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(KcbDonthuocChitiet.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        ////new Update(TPatientDept.Schema)
                        ////    .Set(TPatientDept.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        ////    .Where(TPatientDept.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        //new Update(TTongChiphi.Schema)
                        //    .Set(TTongChiphi.Columns.PaymentDate).EqualTo(objThanhtoan.NgayThanhtoan)
                        //    .Where(TTongChiphi.Columns.PaymentId).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
                        ////new Update(TDeposit.Schema)
                        ////  .Set(TDeposit.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
                        ////  .Where(TDeposit.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();
#endregion 
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                // log.Error("Loi trong qua trinh huy thong tin {0}",exception.ToString());
                return ActionResult.Error;
            }
        }

        private int UpdatePaymentStatus(KcbThanhtoan objThanhtoan, KcbThanhtoanChitiet objChitietThanhtoan)
        {
            int reval = -1;
            using (var scope = new TransactionScope())
            {
                StoredProcedure sp = SPs.SpUpdateTrangthaithanhtoan(objChitietThanhtoan.IdLoaithanhtoan,
                    objThanhtoan.IdThanhtoan, objThanhtoan.NgayThanhtoan, objThanhtoan.NoiTru,
                    objChitietThanhtoan.KieuChietkhau, objChitietThanhtoan.TileChietkhau,
                    objChitietThanhtoan.TienChietkhau, objChitietThanhtoan.IdPhieu, objChitietThanhtoan.IdPhieuChitiet,
                    objChitietThanhtoan.NgayTao, objChitietThanhtoan.NguoiTao);
                reval = sp.Execute();
               
                #region updatetrangthaithanhtoan_old
        //        switch (objChitietThanhtoan.IdLoaithanhtoan)
        //        {
        //            case 1: //Phí KCB
        //                reval = new Update(KcbDangkyKcb.Schema)
        //                    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();

        //                //new Update(NoitruPhanbuonggiuong.Schema)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //     .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(DateTime.Now)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                //    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //     .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                //    .Where(NoitruPhanbuonggiuong.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu)
        //                //    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(0).Execute();
        //                break;
        //            case 10: //Sổ khám
        //                reval = new Update(KcbDangkySokham.Schema)
        //                    .Set(KcbDangkySokham.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkySokham.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkySokham.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(KcbDangkySokham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbDangkySokham.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkySokham.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Where(KcbDangkySokham.Columns.IdSokcb).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 8: //Gói dịch vụ
        //            case 11: //Công tiêm chủng
        //            case 9: //Chi phí thêm
        //            case 2: //Phí CLS
        //            case 12: //Dịch vụ kiểm nghiệm
        //                reval = new Update(KcbChidinhclsChitiet.Schema)
        //                    .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbChidinhclsChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbChidinhclsChitiet.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh)
        //                    .IsEqualTo(objChitietThanhtoan.IdPhieuChitiet)
        //                    .Execute();
        //                //09/09/2015-->Tạm thời bỏ do ko có ý nghĩa
        //                //new Update(KcbChidinhcl.Schema)
        //                //.Set(KcbChidinhcl.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //.Set(KcbChidinhcl.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //.Where(KcbChidinhcl.Columns.IdChidinh).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 3: //Đơn thuốc ngoại trú,nội trú
        //            case 5: //Vật tư tiêu hao
        //                reval = new Update(KcbDonthuocChitiet.Schema)
        //                    .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbDonthuocChitiet.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbDonthuocChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(KcbDonthuocChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc)
        //                    .IsEqualTo(objChitietThanhtoan.IdPhieuChitiet)
        //                    .Execute();
        //                //09/09/2015-->Tạm thời bỏ do ko có ý nghĩa
        //                //new Update(KcbDonthuoc.Schema)
        //                //   .Set(KcbDonthuoc.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                //   .Set(KcbDonthuoc.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                //   .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;

        //            case 4: //Giường bệnh
        //                reval = new Update(NoitruPhanbuonggiuong.Schema)
        //                    .Set(NoitruPhanbuonggiuong.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Set(NoitruPhanbuonggiuong.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(objChitietThanhtoan.IdPhieu).Execute();
        //                break;
        //            case 0: //Phí dịch vụ kèm theo
        //                reval = new Update(KcbDangkyKcb.Schema)
        //                    .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objThanhtoan.IdThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
        //                    .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objThanhtoan.NgayThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.TienChietkhau).EqualTo(objChitietThanhtoan.TienChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.TileChietkhau).EqualTo(objChitietThanhtoan.TileChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.KieuChietkhau).EqualTo(objChitietThanhtoan.KieuChietkhau)
        //                    .Set(KcbDangkyKcb.Columns.NguonThanhtoan).EqualTo(objThanhtoan.KieuThanhtoan)
        //                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(DateTime.Now)
        //                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
        //                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objChitietThanhtoan.IdPhieu)
        //                    .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(1)
        //                    .Execute();
        //                break;
                // }
#endregion 
                scope.Complete();
            }
            return reval;
        }

        public ActionResult UpdateIcd10(KcbLuotkham objLuotkham, string ICDCode, string ICDTen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.MabenhChinh).EqualTo(ICDCode)
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                        var objChuandoanKetluan =
                            new Select().From(KcbChandoanKetluan.Schema)
                            .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                 .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                                .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbChandoanKetluan>();
                        if (objChuandoanKetluan != null)
                        {
                            new Update(KcbChandoanKetluan.Schema)
                                .Set(KcbChandoanKetluan.Columns.MabenhChinh).EqualTo(ICDCode)
                                .Set(KcbChandoanKetluan.Columns.MotaBenhchinh).EqualTo(ICDTen)
                                .Set(KcbChandoanKetluan.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                 .Set(KcbChandoanKetluan.Columns.NgaySua).EqualTo(DateTime.Now)
                                .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                                .Execute();
                        }
                        else
                        {
                            objChuandoanKetluan = new KcbChandoanKetluan();
                            objChuandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan);
                            objChuandoanKetluan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                            objChuandoanKetluan.SongayDieutri = 1;
                            objChuandoanKetluan.MabenhChinh = ICDCode;
                            objChuandoanKetluan.MotaBenhchinh = ICDTen;
                            objChuandoanKetluan.NgayChandoan = DateTime.Now;
                            objChuandoanKetluan.NgayTao = DateTime.Now;
                            objChuandoanKetluan.NguoiTao = globalVariables.UserName;
                            objChuandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                            objChuandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                            objChuandoanKetluan.Noitru = 0;
                            objChuandoanKetluan.IsNew = true;
                            objChuandoanKetluan.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult Capnhattrangthaithanhtoan(long idThanhtoan)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(DateTime.Now)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(idThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateDataPhieuThu(KcbPhieuthu objPhieuthu)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        StoredProcedure sp = SPs.KcbThanhtoanThemmoiPhieuthu(objPhieuthu.MaPhieuthu,
                            objPhieuthu.IdThanhtoan,
                            objPhieuthu.NgayThuchien,
                            objPhieuthu.NguoiNop, objPhieuthu.LydoNop,
                            objPhieuthu.SoTien, objPhieuthu.SotienGoc, objPhieuthu.TienChietkhau,
                            objPhieuthu.TienChietkhauchitiet, objPhieuthu.TienChietkhauhoadon,
                            objPhieuthu.SoluongChungtugoc, objPhieuthu.TaikhoanNo,
                            objPhieuthu.TaikhoanCo,
                            objPhieuthu.LoaiPhieuthu, globalVariables.UserName,
                            DateTime.Now,
                            globalVariables.gv_intIDNhanvien,
                            globalVariables.idKhoatheoMay,
                            globalVariables.UserName, DateTime.Now);
                        sp.Execute();

                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(DateTime.Now)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuthu.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.InfoException("Ban ra loi exception=", exception);
                return ActionResult.Error;
            }
        }

        public ActionResult UpdateDataPhieuThu(KcbPhieuthu objPhieuthu, KcbThanhtoanChitiet[] arrPaymentDetail)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        StoredProcedure sp = SPs.KcbThanhtoanThemmoiPhieuthu(objPhieuthu.MaPhieuthu,
                            objPhieuthu.IdThanhtoan,
                            objPhieuthu.NgayThuchien,
                            objPhieuthu.NguoiNop, objPhieuthu.LydoNop,
                            objPhieuthu.SoTien, objPhieuthu.SotienGoc, objPhieuthu.TienChietkhau,
                            objPhieuthu.TienChietkhauchitiet, objPhieuthu.TienChietkhauhoadon,
                            objPhieuthu.SoluongChungtugoc, objPhieuthu.TaikhoanNo,
                            objPhieuthu.TaikhoanCo,
                            objPhieuthu.LoaiPhieuthu, globalVariables.UserName,
                            DateTime.Now,
                            globalVariables.gv_intIDNhanvien,
                            globalVariables.idKhoatheoMay,
                            globalVariables.UserName, DateTime.Now);
                        sp.Execute();
                        foreach (KcbThanhtoanChitiet objChitietThanhtoan in arrPaymentDetail)
                        {
                            new Update(KcbThanhtoanChitiet.Schema)
                                .Set(KcbThanhtoanChitiet.Columns.SttIn).EqualTo(objChitietThanhtoan.SttIn)
                                // .Set(KcbThanhtoanChitiet.Columns.PhuThu).EqualTo(objChitietThanhtoan.PhuThu)
                                .Where(KcbThanhtoanChitiet.Columns.IdChitiet).IsEqualTo(
                                    objChitietThanhtoan.IdChitiet).Execute();
                            log.Info("Cạp nhạp lại thong tin cua voi ma ID=" + objChitietThanhtoan.IdChitiet);
                        }
                        new Update(KcbThanhtoan.Schema)
                            .Set(KcbThanhtoan.Columns.NguoiIn).EqualTo(globalVariables.UserName)
                            .Set(KcbThanhtoan.Columns.NgayIn).EqualTo(DateTime.Now)
                            .Set(KcbThanhtoan.Columns.TrangthaiIn).EqualTo(1)
                            .Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objPhieuthu.IdThanhtoan).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Ban ra loi Exception={0}", exception);
                return ActionResult.Error;
            }
        }

        public DataTable GetDataInphieuDichvu(KcbThanhtoan objThanhtoan)
        {
            return
                SPs.KcbThanhtoanLaythongtinInphieuDichvu(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public DataTable GetDataInphieuBh(KcbThanhtoan objThanhtoan, bool IsBH)
        {
            DataTable dataTable =
                SPs.BhytLaythongtinInphieubhyt(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
                    objThanhtoan.MaLuotkham,
                    Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            if (IsBH)
            {
                foreach (DataRow drv in dataTable.Rows)
                {
                    if (drv["TuTuc"].ToString() == "1") drv.Delete();
                }
                dataTable.AcceptChanges();
            }
            return dataTable;
        }

        public DataTable INPHIEUBH_CHOBENHNHAN(KcbThanhtoan objThanhtoan)
        {
            //DataTable dataTable =
            //    SPs.BhytLaythongtinInphieubhytChobenhnhan(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];

            //return dataTable;
            return null;
        }

        public DataTable KYDONG_GetDataInphieuBH(KcbThanhtoan objThanhtoan, bool TuTuc)
        {
            return null;
            //DataTable dataTable =
            //    SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
            //if (!TuTuc)
            //{
            //    foreach (DataRow drv in dataTable.Rows)
            //    {
            //        if (drv["TuTuc"].ToString() == "1") drv.Delete();
            //    }
            //    dataTable.AcceptChanges();
            //}
            //return dataTable;
        }

        public DataTable KYDONG_GetDataInphieuBH_TraiTuyen(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return
            //    SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), objThanhtoan.MaLuotkham,
            //                          Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public void XuLyThongTinPhieu_DichVu(ref DataTable m_dtReportPhieuThu)
        {
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "TONG_BN", typeof (decimal));
            Utility.AddColumToDataTable(ref m_dtReportPhieuThu, "PHU_THU", typeof (decimal));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                //drv["ThanhTien"] = Utility.Int32Dbnull(drv["SoLuong"], 0) *
                //                   Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["TotalSurcharge_Price"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                              Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["TONG_BN"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                 Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["PHU_THU"] = Utility.Int32Dbnull(drv["SoLuong"], 0)*
                                 Utility.DecimaltoDbnull(drv[KcbThanhtoanChitiet.Columns.PhuThu], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
        }

        public DataTable KydongInphieuBaohiemChoBenhnhan(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphieubhKd(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                        Utility.sDbnull(objThanhtoan.MaLuotkham),
            //                                        Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet().Tables[0];
        }

        public DataTable KydongInPhieubaohiemTraituyen(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphieuTraituyen(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), Utility.sDbnull(objThanhtoan.MaLuotkham, ""),
            //                                      Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet().Tables[0];
        }

        public DataTable DetmayPrintAllExtendExamPaymentDetail(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLAYTHONGTInInphoibhytDm(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                              objThanhtoan.MaLuotkham,
            //                                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet()
            //        .Tables[0];
        }

        public DataTable DetmayInphieuBhPhuthu(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLAYTHONGTInInphoibhytPhuhuDm(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1),
            //                                              objThanhtoan.MaLuotkham,
            //                                              Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1)).GetDataSet()
            //        .Tables[0];
        }

        public DataTable LaokhoaInbienlaiBhyt(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytInbienlai(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan), Utility.sDbnull(objThanhtoan.MaLuotkham), Utility.Int32Dbnull(objThanhtoan.IdBenhnhan)).GetDataSet().Tables[0];
        }

        public DataTable LaokhoaInphieuBaohiemNgoaitru(KcbThanhtoan objThanhtoan)
        {
            return null;
            //return SPs.BhytLaythongtinInphoi(Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1), Utility.sDbnull(objThanhtoan.MaLuotkham, ""),
            //                             Utility.Int32Dbnull(objThanhtoan.IdBenhnhan, -1), 0).GetDataSet().Tables[0];
        }

        public ActionResult UPDATE_SOBIENLAI(HoadonLog lHoadonLog)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        int record = -1;
                        record = new Update(HoadonLog.Schema).Set(HoadonLog.Columns.MauHoadon)
                            .EqualTo(lHoadonLog.MauHoadon).Set(HoadonLog.Columns.KiHieu).EqualTo(lHoadonLog.KiHieu)
                            .Set(HoadonLog.Columns.MaQuyen).EqualTo(lHoadonLog.MaQuyen)
                            .Set(HoadonLog.Columns.Serie).EqualTo(lHoadonLog.Serie)
                            .Where(HoadonLog.Columns.IdHdonLog).IsEqualTo(lHoadonLog.IdHdonLog)
                            .Execute();
                        if (record <= 0)
                        {
                            return ActionResult.Error;
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

        public ActionResult CHUYEN_DOITUONG(KcbLuotkham objLuotkham, string DOITUONG)
        {
            //try
            //{
            //    using (var Scope = new TransactionScope())
            //    {
            //        using (var dbScope = new SharedDbConnectionScope())
            //        {
            //            KcbDangkyKcbCollection TexamCollection =
            //                new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(
            //                    objLuotkham.MaLuotkham).And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //                    .ExecuteAsCollection<KcbDangkyKcbCollection>();
            //            if (TexamCollection.Count > 0)
            //            {
            //                //CHUYỂN GIÁ KHÁM BỆNH VÀO PHÒNG
            //                foreach (KcbDangkyKcb regExam in TexamCollection)
            //                {
            //                    if (Utility.Int32Dbnull(regExam.TrangthaiThanhtoan) == 1)
            //                    {
            //                        return ActionResult.ExistedRecord;
            //                    }
            //                    DmucDichvukcb KieuKhamCu = DmucDichvukcb.FetchByID(regExam.IdDichvuKcb);
            //                    DmucDichvukcb KieuKhamMoi =
            //                        new Select().From(DmucDichvukcb.Schema)
            //                        .Where(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(KieuKhamCu.IdKieukham)
            //                        .And(DmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(KieuKhamCu.IdKhoaphong)
            //                        .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(KieuKhamCu.IdPhongkham)
            //                        .And(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo(DOITUONG)
            //                        .ExecuteSingle<DmucDichvukcb>();
            //                    regExam.IdDichvuKcb = Utility.Int16Dbnull(KieuKhamMoi.IdDichvukcb, -1);
            //                    if (objLuotkham.MaKhoaThuchien == "KYC")
            //                    {
            //                        regExam.DonGia = KieuKhamMoi.DonGia;
            //                        regExam.PhuThu = KieuKhamMoi.PhuthuDungtuyen;
            //                        regExam.Save();
            //                    }
            //                    else if (objLuotkham.MaKhoaThuchien == "KKB")
            //                    {
            //                        regExam.DonGia = KieuKhamMoi.DonGia;
            //                        if (Utility.sDbnull(objLuotkham.MaDoituongKcb, "DV") == "BHYT" && Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0)
            //                        {
            //                            regExam.PhuThu = KieuKhamMoi.PhuthuDungtuyen;
            //                        }
            //                        else
            //                        {
            //                            regExam.PhuThu = 0;
            //                        }
            //                        regExam.Save();

            //                        //THÊM CHI PHÍ DỊCH VỤ KÈM THEO KHÁM BỆNH
            //                        SqlQuery sql = new Select().From(KcbChidinhcl.Schema).Where(
            //                            KcbChidinhcl.Columns.MaLuotkham).
            //                            IsEqualTo(objLuotkham.MaLuotkham)
            //                            .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan);
            //                            //.And(KcbChidinhcl.Columns.IsPHIDvuKtheo).IsEqualTo(1);
            //                        int IdDV = -1;
            //                        string[] Ma_UuTien = globalVariables.gv_strMaUutien.Split(',');
            //                        if (!Ma_UuTien.Contains(Utility.sDbnull(objLuotkham.MaQuyenloi)))
            //                        {
            //                            if (Utility.Int32Dbnull(regExam.KhamNgoaigio, 0) == 1)
            //                            {
            //                                IdDV = Utility.Int32Dbnull(KieuKhamMoi.IdPhikemtheongoaigio, -1);
            //                            }
            //                            else
            //                            {
            //                                IdDV = Utility.Int32Dbnull(KieuKhamMoi.IdPhikemtheo, -1);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            IdDV = -1;
            //                            KcbChidinhclCollection taCollection =
            //                                sql.ExecuteAsCollection<KcbChidinhclCollection>();
            //                            foreach (KcbChidinhcl assignInfo in taCollection)
            //                            {
            //                                KcbChidinhclsChitiet.Delete(KcbChidinhclsChitiet.Columns.IdChidinh, assignInfo.IdChidinh);
            //                                KcbChidinhcl.Delete(assignInfo.IdChidinh);
            //                            }
            //                        }
            //                        if (sql.GetRecordCount() <= 0)
            //                        {
            //                            //LServiceDetail lServiceDetail = LServiceDetail.FetchByID(IdDV);
            //                            //if (lServiceDetail != null)
            //                            //{
            //                            //    var objAssignInfo = new KcbChidinhcl();
            //                            //    objAssignInfo.ExamId = -1;
            //                            //    objAssignInfo.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
            //                            //    objAssignInfo.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, "");

            //                            //    objAssignInfo.RegDate = DateTime.Now;
            //                            //    objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //                            //    objAssignInfo.TrangthaiThanhtoan = 0;
            //                            //    objAssignInfo.CreatedBy = globalVariables.UserName;
            //                            //    objAssignInfo.CreateDate = DateTime.Now;
            //                            //    objAssignInfo.Actived = 0;
            //                            //    objAssignInfo.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
            //                            //    objAssignInfo.NoiTru = 0;
            //                            //    objAssignInfo.IdDoituongKcb = Utility.Int16Dbnull(
            //                            //        objLuotkham.IdDoituongKcb, -1);
            //                            //    objAssignInfo.DiagPerson = globalVariables.gv_intIDNhanvien;
            //                            //    objAssignInfo.DepartmentId = globalVariables.DepartmentID;
            //                            //    objAssignInfo.IsPHIDvuKtheo = 1;
            //                            //    objAssignInfo.IsNew = true;
            //                            //    objAssignInfo.Save();
            //                            //    objAssignInfo.IdChidinh =
            //                            //        Utility.Int32Dbnull(
            //                            //            KcbChidinhcl.CreateQuery().GetMax(KcbChidinhcl.Columns.IdChidinh), -1);
            //                            //    var objAssignDetail = new KcbChidinhclsChitiet();
            //                            //    objAssignDetail.ExamId = -1;
            //                            //    objAssignDetail.IdChidinh = objAssignInfo.IdChidinh;
            //                            //    objAssignDetail.ServiceId = lServiceDetail.ServiceId;
            //                            //    objAssignDetail.IdDichvuChitiet = lServiceDetail.IdDichvuChitiet;
            //                            //    objAssignDetail.DiscountPrice = 0;
            //                            //    objAssignDetail.DiscountRate = 0;
            //                            //    objAssignDetail.DiscountType = 0;
            //                            //    objAssignDetail.DonGia = Utility.DecimaltoDbnull(lServiceDetail.Price,
            //                            //                                                          0);
            //                            //    objAssignDetail.DiscountPrice = Utility.DecimaltoDbnull(
            //                            //        lServiceDetail.Price, 0);
            //                            //    objAssignDetail.PhuThu = 0;
            //                            //    objAssignDetail.UserId = globalVariables.UserName;
            //                            //    objAssignDetail.AssignTypeId = 0;
            //                            //    objAssignDetail.InputDate = DateTime.Now;
            //                            //    objAssignDetail.TrangthaiThanhtoan = 0;
            //                            //    objAssignDetail.TuTuc = (byte?)(Utility.sDbnull(objLuotkham.MaDoituongKcb) == "DV" ? 0 : 1);
            //                            //    objAssignDetail.SoLuong = 1;
            //                            //    objAssignDetail.AssignDetailStatus = 0;
            //                            //    objAssignDetail.SDesc =
            //                            //        "Chi phí đi kèm thêm phòng khám khi đăng ký khám bệnh theo yêu cầu";
            //                            //    objAssignDetail.BhytStatus = 0;
            //                            //    objAssignDetail.DisplayOnReport = 1;
            //                            //    objAssignDetail.GiaBhytCt = 0;
            //                            //    objAssignDetail.GiaBnct = Utility.DecimaltoDbnull(lServiceDetail.Price, 0);
            //                            //    objAssignDetail.IpMayTao = globalVariables.IpAddress;
            //                            //    objAssignDetail.IpMacTao = globalVariables.IpMacAddress;
            //                            //    objAssignDetail.ChoPhepIn = 0;
            //                            //    objAssignDetail.AssignDetailStatus = 0;
            //                            //    objAssignDetail.DiagPerson = globalVariables.gv_intIDNhanvien;
            //                            //    objAssignDetail.IdDoituongKcb =
            //                            //        Utility.Int16Dbnull(objLuotkham.IdDoituongKcb,
            //                            //                            -1);
            //                            //    objAssignDetail.Stt = 1;
            //                            //    objAssignDetail.IsNew = true;
            //                            //    objAssignDetail.Save();
            //                            //}
            //                        }
            //                    }


            //                }
            //                //CHUYỂN GIÁ DỊCH VỤ CẬN LÂM SÀNG
            //                KcbChidinhclCollection taAssignInfoCollection = new Select().From(KcbChidinhcl.Schema).
            //                    Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //                    .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //                    .And(KcbChidinhcl.Columns.IsPHIDvuKtheo).IsEqualTo(0)
            //                    .ExecuteAsCollection<KcbChidinhclCollection>();
            //                foreach (KcbChidinhcl assignInfo in taAssignInfoCollection)
            //                {
            //                    KcbChidinhclsChitietCollection tAssignDetailCollection =
            //                        new Select().From(KcbChidinhclsChitiet.Schema)
            //                            .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(assignInfo.IdChidinh).
            //                            ExecuteAsCollection<KcbChidinhclsChitietCollection>();
            //                    foreach (KcbChidinhclsChitiet assignDetail in tAssignDetailCollection)
            //                    {
            //                        if (Utility.Int32Dbnull(assignDetail.TrangthaiThanhtoan) == 1)
            //                        {
            //                            return ActionResult.ExistedRecord;
            //                        }
            //                        DmucDoituongkcbService lObjectTypeService = new Select().From(DmucDoituongkcbService.Schema)
            //                            .Where(DmucDoituongkcbService.Columns.IdDichvuChitiet).IsEqualTo(assignDetail.IdDichvuChitiet)
            //                            .And(DmucDoituongkcbService.Columns.MaDtuong).IsEqualTo(objLuotkham.MaDoituongKcb)
            //                            .And(DmucDoituongkcbService.Columns.MaKhoaThien).IsEqualTo(objLuotkham.MaKhoaThien).ExecuteSingle<DmucDoituongkcbService>();
            //                        if (lObjectTypeService != null)
            //                        {
            //                            assignDetail.DiscountPrice = Utility.DecimaltoDbnull(lObjectTypeService.LastPrice, 0);
            //                            if (Utility.sDbnull(objLuotkham.MaDoituongKcb, "DV") == "BHYT" && Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0)
            //                            {
            //                                assignDetail.PhuThu = Utility.DecimaltoDbnull(lObjectTypeService.PhuThuTraiTuyen, 0);
            //                            }
            //                            else
            //                            {
            //                                assignDetail.PhuThu = Utility.DecimaltoDbnull(lObjectTypeService.Surcharge, 0);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            if (Utility.sDbnull(objLuotkham.MaDoituongKcb) == "BHYT")
            //                            {

            //                                DmucDoituongkcbService lObjectTypeServiceDv = new Select().From(DmucDoituongkcbService.Schema)
            //                                    .Where(DmucDoituongkcbService.Columns.IdDichvuChitiet).IsEqualTo(assignDetail.IdDichvuChitiet)
            //                                    .And(DmucDoituongkcbService.Columns.MaDtuong).IsEqualTo("DV").And(DmucDoituongkcbService.Columns.MaKhoaThien).IsEqualTo(objLuotkham.MaKhoaThien).ExecuteSingle<DmucDoituongkcbService>();
            //                                if (lObjectTypeServiceDv != null)
            //                                {
            //                                    assignDetail.DiscountPrice = Utility.DecimaltoDbnull(lObjectTypeServiceDv.LastPrice, 0);
            //                                    assignDetail.PhuThu = 0;
            //                                    assignDetail.TuTuc = 1;
            //                                }
            //                                else
            //                                {
            //                                    Utility.ShowMsg("Không có giá Dịch Vụ");
            //                                    return ActionResult.Exceed;
            //                                }
            //                            }
            //                        }
            //                        assignDetail.Save();
            //                    }
            //                }
            //                objLuotkham.Save();
            //            }
            //        }
            //        Scope.Complete();
            //    }
            //    return ActionResult.Success;
            //}
            //catch (Exception)
            //{
            return ActionResult.Error;
            //}
        }

        public ActionResult Chotbaocao(DateTime NgayChot, DateTime ngayThanhToan)
        {
            try
            {
                string username = globalVariables.UserName;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOTSOLIEU_THEOTHUNGANVIEN", "0", false) == "0")
                    username = "ALL";
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SPs.KcbThanhtoanChot(NgayChot.ToString("dd/MM/yyyy"), ngayThanhToan.ToString("dd/MM/yyyy"),
                            username).Execute();
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

        public ActionResult ChotVetbaocao(DateTime NgayChot, DateTime NgayThanhToan)
        {
            try
            {
                string username = globalVariables.UserName;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOTSOLIEU_THEOTHUNGANVIEN", "0", false) == "0")
                    username = "ALL";
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SPs.KcbThanhtoanChotvet(NgayChot.ToString("dd/MM/yyyy"), NgayThanhToan.ToString("dd/MM/yyyy"),
                            username).Execute();
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