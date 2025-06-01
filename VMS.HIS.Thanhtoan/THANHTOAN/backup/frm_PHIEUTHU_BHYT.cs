using System;
using System.Linq;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using Microsoft.VisualBasic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.NGHIEPVU.THANHTOAN;
using VNS.Libs;

using VNS.HIS.NGHIEPVU.BAOCAO;
using VNS.HIS.DAL;





using NLog;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using VNS.HIS.UI.HOADONDO;
using VNS.HIS.UI.BAOCAO;

using VietBaIT.HISLink.UI.YHHQ_Reports.Class;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.Classes;
using VNS.HIS.Reports.VNS.Thanhtoan;
namespace  VNS.HIS.UI.THANHTOAN
{
    public partial class frm_PHIEUTHU_BHYT : Form
    {
        /// <summary>
        /// 05-11-2013
        /// </summary>
        #region "KHAI BÁO BIẾN"
        private readonly string sAccountName = "DETMAY";
        private readonly MoneyByLetter sMoneyByLetter = new MoneyByLetter();
        private ActionResult actionResult = ActionResult.Error;
        private action em_Action = action.Insert;
        private DataTable m_dtPaymentDetail = new DataTable();
        public DataTable _dtPayment= new DataTable();
        public  DataTable _dtPaymentDetail = new DataTable();
        public int ObjectTypeId = -1;
        private NLog.Logger log;
        public int v_ObjectType_Id = -1;
        public CallObject_BN Call_DoiTuong_BenhNhan = CallObject_BN.BN_NGOAITRU;
        /// <summary>
        /// hàm thực hiên việc thu phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable m_dtReportPhieuThu = new DataTable();

        public int status;
        public decimal v_DiscountRate;
        #endregion
        #region "KHAI BÁO CỦA FORM"

        public frm_PHIEUTHU_BHYT()
        {
            InitializeComponent();
            BusinessHelper.GetTieuDeBaoCao(this.Name, txtTieuDe.Text);
            dtNgayIn.Value = globalVariables.SysDate;
            log = LogManager.GetCurrentClassLogger();
            Utility.loadIconToForm(this);
            sAccountName = THU_VIEN_CHUNG.LayMaDviLamViec();
            switch (THU_VIEN_CHUNG.LayMaDviLamViec())
            {
                case "KYDONG":
                    chkInBN.Visible = true;
                    cmdPrintAll.Visible = true;
                    cmdBienLai.Visible = true;
                   // cmdRefundPayment.Visible = true;
                    cmdBienLai.Text = "In cho bệnh nhân";
                   // cmdInPhieuXN.Visible = true;
                    break;
                case "YHOCHAIQUAN":
                    chkInBN.Visible = true;
                    cmdPrintAll.Visible = true;
                    //cmdINPHIEU_CHOBENHNHAN.Visible = false;
                    cmdBienLai.Text = "In cho bệnh nhân";
                    break;
                case "DETMAY":
                    chkInBN.Visible = false;
                    cmdPrintAll.Visible = false;
                    cmdBienLai.Visible = true;
                    //cmdRefundPayment.Visible = false;
                    break;
                default:
                    chkInBN.Visible = false;
                    break;
            }
            radInPayment_ID.Checked = true;
            txtTieuDe.LostFocus+=new EventHandler(txtTieuDe_LostFocus);
            setProperties();

        }
        private void setProperties()
        {
            foreach (Control control in grpTongTien.Controls)
            {
                if(control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    Janus.Windows.GridEX.EditControls.EditBox txtTien=new EditBox();
                    txtTien = (Janus.Windows.GridEX.EditControls.EditBox) control;
                    txtTien.Clear();
                    txtTien.ReadOnly = true;
                    txtTien.TextChanged += new EventHandler(txtEventTongTien_TextChanged);
                    txtTien.KeyPress += new KeyPressEventHandler(txtEventTongTien_KeyPress);
                   // txtTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    txtTien.TextAlignment = TextAlignment.Far;
                }
            }
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            var txtTongTien =
                ((EditBox)(sender));
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien =
                ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
        private void txtTieuDe_LostFocus(object sender, EventArgs eventArgs)
        {
            BusinessHelper.UpdateTieuDe(this.Name, txtTieuDe.Text);
        }
        #endregion
        #region "KHAI BÁI SỰ KIỆN CỦA FORM HIỆN TẠI"
        /// <summary>
        /// hàm thực hiện đóng form hiện tại 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// phím tắt trong form thực hiện khi su dụng phím tắt cho thao tác đơn giản hơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_YHHQ_PhieuThu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            //if (e.KeyCode == Keys.F5) cmdPrint.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieu.PerformClick();
            if (e.KeyCode == Keys.F6) cmdBienLai.PerformClick();
        }
       
        /// <summary>
        /// hàm thực hiện việc load Form hiện tại trên form dùng cho phiếu thu tiền bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PHIEUTHU_DCT_Load(object sender, EventArgs e)
        {
            if (status == 0)
            {
                grdPaymentDetail.RootTable.Columns["CHON"].Visible = true;
               // cmdRefundPayment.Visible = true;
                cmdInPhieu.Enabled = true;
              //  cmdRefundPayment.Visible = BusinessHelper.IsDichVu(ObjectTypeId);
            }
            else
            {
                grdPaymentDetail.RootTable.Columns["CHON"].Visible = false;
               // cmdRefundPayment.Visible = false;
                Text = "Phiếu trả tiền";
                label9.Text = "Người Nhận";
                label4.Text = "Lý do chi";
                cmdInPhieu.Visible = false;
               
            }
            GetData();
        }

        private TPhieuthu CreatePhieuThu()
        {
            var objPhieuThu = new TPhieuthu();
            objPhieuThu.PaymentId = Utility.Int32Dbnull(txtPayment_ID.Text, -1);
            objPhieuThu.MaPthu = Utility.sDbnull(txtMA_PTHU.Text, "");
            objPhieuThu.SluongCtuGoc = Utility.Int16Dbnull(txtSLUONG_CTU_GOC.Text);
            objPhieuThu.LoaiPhieu = Convert.ToByte(status);
            objPhieuThu.NgayThien = dtCreateDate.Value;
            objPhieuThu.SoTien = Utility.DecimaltoDbnull(txtSO_TIEN.Text, 0);
            objPhieuThu.NguoiNop = Utility.sDbnull(txtNGUOI_NOP.Text, "");
            objPhieuThu.TkhoanCo = Utility.sDbnull(txtTKHOAN_CO.Text, "");
            objPhieuThu.TkhoanNo = Utility.sDbnull(txtTKHOAN_NO.Text, "");
            objPhieuThu.LdoNop = Utility.sDbnull(txtLDO_NOP.Text, "");
            return objPhieuThu;
        }
        /// <summary>
        /// hàm thực hiện in phiếu cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {

            try
            {
                frm_Print_RedInvoice frm = new frm_Print_RedInvoice();
                frm.payment_ID = Utility.Int32Dbnull(txtPayment_ID.Text, -1);
                frm.ShowDialog();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý thông tin hóa đơn");

            }
            //actionResult = new PreExtendExamPayment().UpdateDataPhieuThu(CreatePhieuThu());
            //switch (actionResult)
            //{
            //    case ActionResult.Success:
            //        SqlQuery q = new Select().From(TPhieuthu.Schema)
            //            .Where(TPhieuthu.Columns.MaPthu).IsEqualTo(Utility.sDbnull(txtMA_PTHU.Text, "")).And(
            //                TPhieuthu.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            //        m_dtReportPhieuThu = q.ExecuteDataSet().Tables[0];
            //        if (m_dtReportPhieuThu.Rows.Count <= 0)
            //        {
            //            Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
            //            return;
            //        }
            //        InPhieuDichVu();
            //        break;
            //    case ActionResult.Error:
            //        Utility.ShowMsg("Lỗi trong quá trình lập phiếu thu", "Thông báo lỗi", MessageBoxIcon.Warning);
            //        break;
            //}
        }
        private void InPhieuDichVu()
        {
            switch (sAccountName)
            {
                case "YHOCHAIQUAN":
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
                case "DETMAY":
                    DM_PrintPhieuThu("PHIẾU THU");
                    break;
                case "KYDONG":
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
                default :
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu thu của y học hải quân
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void YHHQ_PrintPhieuThu(string sTitleReport)
        {
            var crpt = new ReportDocument();
            Utility.WaitNow(this);
            if(status ==0)
             crpt = new crpt_PhieuThu();
            else
            {
                crpt = new crpt_PhieuChi();
                sTitleReport = "PHIẾU CHI";
            }
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            crpt.SetParameterValue("CharacterMoney", sMoneyByLetter.sMoneyToLetter(txtSO_TIEN.Text));
            DataRow dataRow = m_dtReportPhieuThu.Rows[0];
            //if (status != 0)
            //crpt.SetParameterValue("NguoiChi", BusinessHelper.GetStaffByUserName(dataRow["NGUOI_NOP"].ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// hàm thực hiện việc in phieus thu của dệt may
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void DM_PrintPhieuThu(string sTitleReport)
        {
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuThu_DM();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            crpt.SetParameterValue("CharacterMoney", sMoneyByLetter.sMoneyToLetter(txtSO_TIEN.Text));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }

        private decimal SumOfTotal(DataTable dataTable)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(TONG_BN)+sum(PHU_THU)", "1=1"), 0);
        }
        private decimal SumOfTotal_DV(DataTable dataTable)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(ThanhTien_BN)+sum(TotalSurcharge_Price)", "1=1"), 0);
        }

        private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
        }

        private void YHHQ_PrintDichVu(string sTitleReport)
        {
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuDV();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dtReportPhieuThu);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                //  crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
                crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
                crpt.SetParameterValue("sTitleReport", sTitleReport);
                crpt.SetParameterValue("sMoneyCharacter",
                                       sMoneyByLetter.sMoneyToLetter(SumOfTotal_DV(m_dtReportPhieuThu).ToString()));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTitleReport"></param>
       
        private void cmdRefundPayment_Click(object sender, EventArgs e)
        {
            //grdPaymentDetail.UpdateData();
            //m_dtPaymentDetail.AcceptChanges();
            //_dtPaymentDetail.AcceptChanges();
            //if (!ValitedData())
            //{
            //    Utility.ShowMsg("Bạn phải chọn bản ghi đã thanh toán trước khi trả lại tiền");
            //    return;
            //}
            //if(!InValiXacNhan())return;
            //var frm = new frm_ThongTinTraTien();
            //frm.m_dtPaymentDetail = m_dtPaymentDetail;
            //frm._dtPaymentDetail = _dtPaymentDetail;
            //frm._dtPayment = _dtPayment;
            //frm.paymentId = Utility.Int32Dbnull(txtPayment_ID.Text, -1);
            //frm.ShowDialog();
        }
        private bool InValiXacNhan()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPaymentDetail.GetDataRows())
            {
                if(Utility.Int32Dbnull(gridExRow.Cells["CHON"].Value,0)==1)
                {
                    int PaymentDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[TPaymentDetail.Columns.PaymentDetailId],
                                                               -1);
                    SqlQuery sqlQuery = new Select().From(TPaymentDetail.Schema)
                        .Where(TPaymentDetail.Columns.PaymentDetailId).IsEqualTo(PaymentDetail_ID)
                        .And(TPaymentDetail.Columns.DaThien).IsEqualTo(1);
                    if(sqlQuery.GetRecordCount()>0)
                    {
                        Utility.ShowMsg(
                            "Bản ghi đã được thực hiện tại các phòng chức năng, Bạn không thể xóa,Yêu cầu liên hệ với quản trị mạng để thực hiện",
                            "Thông báo", MessageBoxIcon.Warning);
                        return false;
                        break;
                    }
                }
            }
            return true;

        }
        bool ValitedData()
        {

            
            try
            {
                if (m_dtPaymentDetail.Select("CHON = 1 AND IsCancel = 1").GetLength(0) <= 0 && m_dtPaymentDetail.Select("CHON = 1 AND IsCancel = 0").GetLength(0) > 0)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        #endregion
        #region "IN PHIẾU CỦA ĐƠN VỊ Y HỌC HẢI QUÂN"



        /// <summary>
        /// thưccj hiện in phiếu chi tiết đã thực hiện làm j
        /// </summary>
      

        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
           

            actionResult = new PreExtendExamPayment().UpdateDataPhieuThu(CreatePhieuThu(), CreatePaymentDetail());
            switch (actionResult)
            {
                case ActionResult.Success:
                    ///thực hiện in phiếu nếu cần thiết
                    switch (sAccountName)
                    {
                        case "YHOCHAIQUAN":
                            YHHQ_InPhieuChitietDichVuHoacBaohiem();
                            break;
                        case "DETMAY":
                            switch(Call_DoiTuong_BenhNhan)
                            {
                                case  CallObject_BN.BN_NGOAITRU:
                                DETMAY_InPhieuChitietDichVuHoacBaohiem();
                                break;
                                case CallObject_BN.BN_NOITRU:
                                    DETMAY_InPhieuDichVuHoacBaohiem_NoiTru();
                                break;  

                            }
                            break;
                        case "KYDONG":
                            if (BusinessHelper.GetParamValue("PHIEU_NGOAITRU_DV")=="HONGPHUC")
                            {
                                HONGPHUC_InPhieuChitietDichVuHoacBaohiem();
                            }
                            else
                            {
                                KYDONG_InPhieuChitietDichVuHoacBaohiem();                                 
                            }                                                                        
                            break;
                        case "LAOKHOA":
                           
                            if(radInTatCa.Checked) tatca = true;
                            LAOKHO_INPHIEU_BAOHIEM(tatca);    
                            break;
                        default:
                          
                            if (radInTatCa.Checked) tatca = true;
                            LAOKHO_INPHIEU_BAOHIEM(tatca);
                            break;
                    }
                    break;


                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình lập phiếu thu", "Thông báo lỗi", MessageBoxIcon.Warning);
                    break;
            }


            // YHHQ_InPhieuBaoHiem("BẢN KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ", v_PhieuPaymentDetail);
        }
        bool tatca = false;
        private TPaymentDetail []CreatePaymentDetail()
        {
            int idx = 0;
            int i = 0;
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPaymentDetail.GetDataRows())
            {
                if(gridExRow.RowType==Janus.Windows.GridEX.RowType.Record) idx++;
                
            }
            var arrPaymentDetail = new TPaymentDetail[idx];
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPaymentDetail.GetDataRows())
            {
                if (gridExRow.RowType == Janus.Windows.GridEX.RowType.Record)
                {
                    arrPaymentDetail[i]=new TPaymentDetail();
                    arrPaymentDetail[i].ThuTuIn = Utility.Int32Dbnull(gridExRow.Cells["Thu_Tu_In"].Value, 0);
                    arrPaymentDetail[i].PaymentDetailId = Utility.Int64Dbnull(
                        gridExRow.Cells["PaymentDetail_ID"].Value, -1);
                    arrPaymentDetail[i].SurchargePrice = Utility.DecimaltoDbnull(
                       gridExRow.Cells["Surcharge_Price"].Value, -1);
                    i++;
                }

            }
            return arrPaymentDetail;
        }
        //biến xem thuộc đối tượng nào

        private void YHHQ_InPhieuChitietDichVuHoacBaohiem()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                    ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 1:
                    YHHQ_InPhieuDV(false);
                    break;
                case 0:
                if(chkInBN.Checked)YHHQ_InPhieuBH_IN_BN(false);
                else

                    YHHQ_InPhieuBH(false);
                break;
            }
        }
        /// <summary>
        /// hàm thực hiện in phiếu tổng hợp 
        /// </summary>
        private void YHHQ_InPhieuChitietDichVuHoacBaohiem_TongHop()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 1:
                    YHHQ_InPhieuDV(true);
                    break;
                case 0:
                    if (chkInBN.Checked) YHHQ_InPhieuBH_IN_BN(false);
                    else
                        YHHQ_InPhieuBH(true);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện load thông tin của 33 kỳ đồng
        /// </summary>
        private void KYDONG_InPhieuChitietDichVuHoacBaohiem()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeCode)
            {
                    ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case "BHYT":
                    switch (objPatientExam.CorrectLine)
                    {
                        case 1:///hàm thực hiện in phiếu đúng tuyến
                            KyDong_InPhieuBH_DungTuyen(false);
                            break;
                        case 0:///hàm thực hiện in phiếu trái tuyến
                            KyDong_InPhieuBH_ChiDinhTraiTuyen();
                            break;
                        default:
                            KyDong_InPhieuBH_DungTuyen(false);
                            break;

                    }
                    break;
                case "DV":
                    KyDong_InPhieuDV(false);
                    break;
            }
        }

     
        private void HONGPHUC_InPhieuChitietDichVuHoacBaohiem()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 0:
                    if (chkInBN.Checked) YHHQ_InPhieuBH_IN_BN(false);
                    else

                        YHHQ_InPhieuBH(false);
                    break;
                case 1:
                    KyDong_InPhieuDV(false);
                    break;
            }
        }
        private void KYDONG_InPhieuChitietDichVuHoacBaohiem_TONGHOP()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeCode)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case "BHYT":

                    KyDong_InPhieuBH_DungTuyen(true);
                                    
                    break;
                case "DV":
                    KyDong_InPhieuDV(true);
                    break;
            }
        }
        private void HONGPHUC_InPhieuChitietDichVuHoacBaohiem_TONGHOP()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 0:
                    if (chkInBN.Checked) YHHQ_InPhieuBH_IN_BN(false);
                    else
                        HONGPHUC_InPhieuBH(true);
                    break;
                case 1:
                    KyDong_InPhieuDV(true);
                    break;
            }
        }
        private void KYDONG_INPHIEU_BENHNHAN_TONGHOP()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeCode)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case "BHYT":
                    KyDong_InPhieuBH_BENHNHAN(true);
                    break;
                case "DV":
                    KyDong_InPhieuDV(true);
                    break;
            }
        }
        /// <summary>
        /// thực hiện in phiêu dịch vụ
        /// </summary>
        private void YHHQ_InPhieuDV(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            ///lấy thông tin vào phiếu thu
            m_dtReportPhieuThu = new PhieuThuService().GetDataInphieuDichvu(objPayment);

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["ThanhTien"] = Utility.Int32Dbnull(drv["Quantity"], 0)*
                                   Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["TotalSurcharge_Price"] = Utility.Int32Dbnull(drv["Quantity"], 0)*
                                              Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.SurchargePrice], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            YHHQ_PrintDichVu("BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ");
        }

        /// <summary>
        /// thực hiện in phieus bảo hiểm chi tiết theo mẫu của Y học hải quân
        /// </summary>
        private void YHHQ_InPhieuBH(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().GetDataInphieuBH(objPayment,true);
                ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán
          
           
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            log.Info("Xu ly thong tin in phieu theo dung dinh dang cua Y hoc hai quan");

            YHHQ_ProcessData_INPHIEU_BH(ref m_dtReportPhieuThu);

            m_dtReportPhieuThu.DefaultView.Sort = "Service_ID asc";m_dtReportPhieuThu.AcceptChanges();
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuBHYT_V2();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
            crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
             crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);

            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", "BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ");
            crpt.SetParameterValue("sMoneyCharacter_Thanhtien",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien").ToString()));
            crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BH",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BH").ToString()));
            crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BN",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BN").ToString()));
            crpt.SetParameterValue("sMoneyCharacter_Thanhtien_Khac",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_Khac").ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh in bao cao {0}",ex);
                Utility.DefaultNow(this);
            }
        }
        private void HONGPHUC_InPhieuBH(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().GetDataInphieuBH(objPayment, true);
            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán


            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            log.Info("Xu ly thong tin in phieu theo dung dinh dang cua Y hoc hai quan");

            HONGPHUC_ProcessData_INPHIEU_BH(ref m_dtReportPhieuThu);

            m_dtReportPhieuThu.DefaultView.Sort = "Service_ID asc"; m_dtReportPhieuThu.AcceptChanges();
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuBHYT_V2_HP();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);

                crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
                crpt.SetParameterValue("sTitleReport", "BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ");
                crpt.SetParameterValue("sMoneyCharacter_Thanhtien",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien").ToString()));
                crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BH",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BH").ToString()));
                crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BN",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BN").ToString()));
                crpt.SetParameterValue("sMoneyCharacter_Thanhtien_Khac",
                                       sMoneyByLetter.sMoneyToLetter(
                                           SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_Khac").ToString()));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh in bao cao {0}", ex);
                Utility.DefaultNow(this);
            }
        }

        DataTable m_dtTemp = new DataTable();
        /// <summary>
        /// xử lý thông tin in phiếu bảo hiểm y té cho viện y học hải quân
        /// </summary>
        /// <param name="dataTable"></param>
        private void YHHQ_ProcessData_INPHIEU_BH(ref  DataTable dataTable)
        {
            try
            {
                
                m_dtTemp = dataTable.Copy();
                bool b_Tongso = false;
                DataTable m_dtPaymentTEMP = new DataTable();
                m_dtPaymentTEMP=dataTable.Copy();
                dataTable.Clear();
                foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                {
                    drv["Service_ID"] = -1;
                }
                m_dtPaymentTEMP.AcceptChanges();
                foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                {
                    if (drv["PaymentType_ID"].ToString() == "1")
                    {

                        drv["Service_Name"] = "1. Khám bệnh:";
                        drv["Service_ID"] = 1;
                        //dataTable.ImportRow(drv);
                    }
                    //else
                    //{
                    //    if (!ExistsObject(m_dtPaymentTEMP, 1)) AddNewrow("1. Khám bệnh", 1, 1, ref dataTable);

                    //}
                    ///thực hiện việc thêm cận lâm sàng
                    if (drv["PaymentType_ID"].ToString() == "2")
                    {
                        if (drv["Servicetype_Type"].ToString() == "0") ///hàm thực hiện việc thêm thông tin của xét nghiệm
                        {
                            drv["Service_Name"] = "3. Xét nghiệm :";
                            drv["Service_ID"] = 3;

                        }
                        if (drv["Servicetype_Type"].ToString() == "1") //hàm thực hiện việc hình ảnh
                        {
                            drv["Service_Name"] = "4.Chẩn đoán hình ảnh :";
                            drv["Service_ID"] = 4;
                           
                        }
                        if(drv["Service_Code"].ToString()=="TDCN")
                        {
                            drv["Service_Name"] = "5. Thăm dò chức năng :";
                            drv["Service_ID"] = 5;
                        }
                        if (drv["Servicetype_Type"].ToString() == "2") //hàm thực hiện việc hình ảnh
                        {
                            drv["Service_Name"] = "6. Thủ thuật,phẫu thuật :";
                            drv["Service_ID"] = 6;
                        }
                        
                        ///dịch vụ cao kỹ thuật cao chi phí lớn
                        if (drv["Htech_Service"].ToString() == "1")
                        {
                            drv["Service_Name"] = "7. Dịch vụ kỹ thuật cao chi phí lớn :";
                            drv["Service_ID"] = 7;
                        }
                       
                    }
                    if (drv["PaymentType_ID"].ToString() == "3")
                    {
                        drv["Service_Name"] = "8. Thuốc,Dịch truyền :";
                        drv["Service_ID"] = 8;
                    }
                   
                    ///hàm thực hiện viecj đưa thông tin của vật tư y tế
                    if (drv["PaymentType_ID"].ToString() == "5")
                    {
                        drv["Service_Name"] = "9. Vật tư y tế :";
                        drv["Service_ID"] = 9;
                    }
                    


                }
                DataTable m_dtTempK = new DataTable();
                m_dtTempK = m_dtPaymentTEMP.Copy();
               
                    // dataTable.ImportRow(drv);
                    if (!ExistsObject(m_dtPaymentTEMP, 1)) AddNewrow("1. Khám bệnh:", 1, 1, ref m_dtPaymentTEMP);
                    AddNewrow("2. Tổng ngày điều trị:", 2, 6, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 3)) AddNewrow("3. Xét nghiệm :", 3, 2, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 4)) AddNewrow("4.Chẩn đoán hình ảnh :", 4, 2, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 5)) AddNewrow("5. Thăm dò chức năng :", 5, 2, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 6)) AddNewrow("6. Thủ thuật,phẫu thuật :", 6, 2, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 7)) AddNewrow("7. Dịch vụ kỹ thuật cao chi phí lớn :", 7, 2, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 8)) AddNewrow("8. Thuốc,Dịch truyền :", 8, 3, ref m_dtPaymentTEMP);
                    if (!ExistsObject(m_dtPaymentTEMP, 9)) AddNewrow("9. Vật tư y tế  :", 9, 5, ref m_dtPaymentTEMP);
               
                foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                {
                    if(drv["DrugType_Code"].ToString()=="VT"&&drv["PaymentType_ID"].ToString()=="3")
                    {
                        drv["Service_Name"] = "9. Vật tư y tế  :";
                        drv["Service_ID"] = 9;
                    }
                }
                m_dtPaymentTEMP.AcceptChanges();
                dataTable = m_dtPaymentTEMP;
                //dataTable.AcceptChanges();
                log.Info("Cap nhap thong tin xu lyu thong tin thanh toan thanh cong cua lan thanh toan Payment_ID=" +
                         Utility.sDbnull(txtPayment_ID.Text, -1));

            }
            catch (Exception exception)
            {
                
                log.Error("Loi trong qua trinh xu ly thong tin benh nhan theo dung dinh dang cua YHHQ =",exception);
            }
           
        }
        private void HONGPHUC_ProcessData_INPHIEU_BH(ref  DataTable dataTable)
        {
            try
            {

                m_dtTemp = dataTable.Copy();
                bool b_Tongso = false;
                DataTable m_dtPaymentTEMP = new DataTable();
                m_dtPaymentTEMP = dataTable.Copy();
                dataTable.Clear();
                foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                {
                    drv["Service_ID"] = -1;
                }
                m_dtPaymentTEMP.AcceptChanges();
                foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                {
                    if (drv["PaymentType_ID"].ToString() == "1")
                    {

                        drv["Service_Name"] = "1. Khám bệnh:";
                        drv["Service_ID"] = 1;
                        //dataTable.ImportRow(drv);
                    }
                    //else
                    //{
                    //    if (!ExistsObject(m_dtPaymentTEMP, 1)) AddNewrow("1. Khám bệnh", 1, 1, ref dataTable);

                    //}
                    ///thực hiện việc thêm cận lâm sàng
                    if (drv["PaymentType_ID"].ToString() == "2")
                    {
                        if (drv["Servicetype_Type"].ToString() == "0") ///hàm thực hiện việc thêm thông tin của xét nghiệm
                        {
                            drv["Service_Name"] = "3. Xét nghiệm :";
                            drv["Service_ID"] = 3;

                        }
                        if (drv["Servicetype_Type"].ToString() == "1") //hàm thực hiện việc hình ảnh
                        {
                            drv["Service_Name"] = "4.Chẩn đoán hình ảnh :";
                            drv["Service_ID"] = 4;

                        }
                        if (drv["Service_Code"].ToString() == "TDCN")
                        {
                            drv["Service_Name"] = "5. Thăm dò chức năng :";
                            drv["Service_ID"] = 5;
                        }
                        if (drv["Servicetype_Type"].ToString() == "2") //hàm thực hiện việc hình ảnh
                        {
                            drv["Service_Name"] = "6. Thủ thuật,phẫu thuật :";
                            drv["Service_ID"] = 6;
                        }

                        ///dịch vụ cao kỹ thuật cao chi phí lớn
                        if (drv["Htech_Service"].ToString() == "1")
                        {
                            drv["Service_Name"] = "7. Dịch vụ kỹ thuật cao chi phí lớn :";
                            drv["Service_ID"] = 7;
                        }

                    }
                    if (drv["PaymentType_ID"].ToString() == "3")
                    {
                        drv["Service_Name"] = "8. Thuốc,Dịch truyền :";
                        drv["Service_ID"] = 8;
                    }

                    ///hàm thực hiện viecj đưa thông tin của vật tư y tế
                    if (drv["PaymentType_ID"].ToString() == "5")
                    {
                        drv["Service_Name"] = "9. Vật tư y tế :";
                        drv["Service_ID"] = 9;
                    }



                }
                DataTable m_dtTempK = new DataTable();
                m_dtTempK = m_dtPaymentTEMP.Copy();

                
                //if (!ExistsObject(m_dtPaymentTEMP, 1)) AddNewrow("1. Khám bệnh:", 1, 1, ref m_dtPaymentTEMP);
                //AddNewrow("2. Tổng ngày điều trị:", 2, 6, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 3)) AddNewrow("3. Xét nghiệm :", 3, 2, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 4)) AddNewrow("4.Chẩn đoán hình ảnh :", 4, 2, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 5)) AddNewrow("5. Thăm dò chức năng :", 5, 2, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 6)) AddNewrow("6. Thủ thuật,phẫu thuật :", 6, 2, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 7)) AddNewrow("7. Dịch vụ kỹ thuật cao chi phí lớn :", 7, 2, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 8)) AddNewrow("8. Thuốc,Dịch truyền :", 8, 3, ref m_dtPaymentTEMP);
                //if (!ExistsObject(m_dtPaymentTEMP, 9)) AddNewrow("9. Vật tư y tế  :", 9, 5, ref m_dtPaymentTEMP);

                //foreach (DataRow drv in m_dtPaymentTEMP.Rows)
                //{
                //    if (drv["DrugType_Code"].ToString() == "VT" && drv["PaymentType_ID"].ToString() == "3")
                //    {
                //        drv["Service_Name"] = "9. Vật tư y tế  :";
                //        drv["Service_ID"] = 9;
                //    }
                //}
                m_dtPaymentTEMP.AcceptChanges();
                dataTable = m_dtPaymentTEMP;
                //dataTable.AcceptChanges();
                log.Info("Cap nhap thong tin xu lyu thong tin thanh toan thanh cong cua lan thanh toan Payment_ID=" +
                         Utility.sDbnull(txtPayment_ID.Text, -1));

            }
            catch (Exception exception)
            {

                log.Error("Loi trong qua trinh xu ly thong tin benh nhan theo dung dinh dang cua YHHQ =", exception);
            }

        }

        private  bool ExistsObject(DataTable dataTable,int Service_ID)
        {
            if (dataTable.Select(string.Format("Service_ID={0}", Service_ID)).GetLength(0) <= 0) return false;
            else
                return true;
        }
        /// <summary>
        /// hàm thực hiện việc thêm trường thông tin 
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <param name="Service_ID"></param>
        /// <param name="PaymentType_ID"></param>
        /// <param name="dataTable"></param>
        private void AddNewrow(string ServiceName,int Service_ID,int PaymentType_ID,ref DataTable dataTable)
        {
            DataRow newDrv = dataTable.NewRow();
            newDrv["Service_Name"] = ServiceName;
            newDrv["Service_ID"] = Service_ID;
            newDrv["ServiceDetail_Name"] = string.Empty;
            newDrv["DONGIA"] = 0;
            newDrv["IsPayment"] = 0;
            newDrv["Quantity"] = 0;
            newDrv["ThanhTien_BH"] = 0;
            newDrv["TotalSurcharge_Price"] = 0;
            newDrv["ThanhTien"] = 0;
            newDrv["ThanhTien_BH"] = 0;
            newDrv["ThanhTien_Khac"] = 0;
            newDrv["PaymentType_ID"] = PaymentType_ID;
            DataRow[] arrDr = m_dtTemp.Select("Payment_ID=" + Utility.Int32Dbnull(txtPayment_ID.Text,-1));
                    log.Info("thuc hien viec them thong tin vao cac truong trong");
                    if (arrDr.GetLength(0) > 0)
                    {
                        newDrv["sPatientSex"] = Utility.sDbnull(arrDr[0]["sPatientSex"], "");
                        //drv["Patient_Birth"] = Utility.sDbnull(arrDr[0]["Patient_Birth"], "");
                        newDrv["Year_Of_Birth"] = Utility.Int32Dbnull(arrDr[0]["Year_Of_Birth"], "");
                        newDrv["Age"] = Utility.Int32Dbnull(arrDr[0]["Age"], "");
                        newDrv["Patient_Name"] = Utility.sDbnull(arrDr[0]["Patient_Name"], "");
                        newDrv["Insurance_Num"] = Utility.sDbnull(arrDr[0]["Insurance_Num"], "");
                        newDrv["Insurance_ToDate"] = arrDr[0]["Insurance_ToDate"];
                        newDrv["Insurance_FromDate"] = arrDr[0]["Insurance_FromDate"];
                        newDrv["Emergency_Hos"] = arrDr[0]["Emergency_Hos"];
                        newDrv["InsObject_CodeTP"] = arrDr[0]["InsObject_CodeTP"];
                        newDrv["Hos_Trans"] = arrDr[0]["Hos_Trans"];
                        newDrv["MAQL"] = arrDr[0]["MAQL"];
                        newDrv["Input_Second"] = arrDr[0]["Input_Second"];
                        newDrv["CorrectLine"] = arrDr[0]["CorrectLine"];
                        newDrv["FromClinic_Status"] = arrDr[0]["FromClinic_Status"];
                        newDrv["InsClinic_Code"] = arrDr[0]["InsClinic_Code"];
                        newDrv["Clinic_Name"] = arrDr[0]["Clinic_Name"];
                        newDrv["ObjectType_Name"] = arrDr[0]["ObjectType_Name"];
                        newDrv["ChuanDoan"] = arrDr[0]["ChuanDoan"];
                        newDrv["MaBenh"] = arrDr[0]["MaBenh"];
                        newDrv["ChuanDoan"] = arrDr[0]["ChuanDoan"];
                        
                    }
            dataTable.Rows.Add(newDrv);
        }
        
        /// <summary>
        /// thực hiện in phieus bảo hiểm chi tiết theo mẫu của Y học hải quân
        /// </summary>
        private void YHHQ_InPhieuBH_IN_BN(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().INPHIEUBH_CHOBENHNHAN(objPayment);
           
            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }

          //  YHHQ_PrintBH_IN_BN("");

            Utility.WaitNow(this);
            var crpt = new crpt_PhieuBHYT_TRABNHAN();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dtReportPhieuThu);
                 crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                //  crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
                crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
                crpt.SetParameterValue("sTitleReport", "BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ");
                crpt.SetParameterValue("sMoneyCharacter",
                                       sMoneyByLetter.sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu).ToString()));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }
        #endregion

        #region "IN PHIẾU THANH TOÁN CỦA KỲ ĐỒNG"
        /// <summary>
        /// hàm thực hiện in phiếu bảo hiểm đúng tuyến
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void KyDong_PrintBaoHiem_DungTuyen(string sTitleReport,DataTable dataTable)
        {
            Utility.WaitNow(this);

            var crpt = new crpt_PhieuBHYT_KD();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);

            crpt.SetDataSource(dataTable);
            crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                              ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("Telephone", globalVariables.Branch_Phone);
            crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            // crpt.SetParameterValue("sMoneyCharacter_Thanhtien", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien").ToString()));
            //crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BH", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BH").ToString()));
            // crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BN", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BN").ToString()));
            //crpt.SetParameterValue("sMoneyCharacter_Thanhtien_Khac", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_Khac").ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// hàm thực hiện việc tính cho in phiếu hco bệnh nhân 
        /// của đơn vị tâm phúc
        /// </summary>
        /// <param name="sTitleReport"></param>
        /// <param name="dataTable"></param>
        private void TAMPHUC_INPHIEU_BHYT_CHO_BENHNHAN(string sTitleReport, DataTable dataTable)
        {
            Utility.WaitNow(this);
            crpt_PhieuBHYT_TRABNHAN_KD crpt = new crpt_PhieuBHYT_TRABNHAN_KD();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);

            crpt.SetDataSource(dataTable);
            crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                              ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("Telephone", globalVariables.Branch_Phone);
            crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            // crpt.SetParameterValue("sMoneyCharacter_Thanhtien", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien").ToString()));
            //crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BH", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BH").ToString()));
            // crpt.SetParameterValue("sMoneyCharacter_Thanhtien_BN", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_BN").ToString()));
            //crpt.SetParameterValue("sMoneyCharacter_Thanhtien_Khac", sMoneyByLetter.sMoneyToLetter(SumOfTotal_BH(m_dtReportPhieuThu, "ThanhTien_Khac").ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu trái tuyến
        /// </summary>
        /// <param name="sTitleReport"></param>
      
        private void KyDong_InPhieuDV(bool IsTongHop)
        {
           
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            ///lấy thông tin vào phiếu thu
            m_dtReportPhieuThu = new PhieuThuService().GetDataInphieuDichvu(objPayment);
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            ///hàm thực hiện xử lý thôgn tin của phiếu dịch vụ
            new INPHIEU_KYDONG().XuLyThongTinPhieu_DichVu(ref m_dtReportPhieuThu);
           
        }
        private void LAOKHOA_BIENLAI_THUTIEN(bool IsTongHop)
        {

            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            ///lấy thông tin vào phiếu thu
            m_dtReportPhieuThu =
                SPs.LaokhoaInbienlaiBhyt(Utility.Int32Dbnull(objPayment.PaymentId),Utility.sDbnull(objPatientExam.PatientCode),Utility.Int32Dbnull(objPayment.PatientId)).GetDataSet().Tables[0];
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            ///hàm thực hiện xử lý thôgn tin của phiếu dịch vụ
           // new INPHIEU_KYDONG().XuLyThongTinPhieu_DichVu(ref m_dtReportPhieuThu);
            new INPHIEU_THANHTOAN_NGOAITRU().LAOKHOA_INPHIEU_BIENLAI_BHYT(m_dtReportPhieuThu, "PHIẾU TỔNG HỢP CHI PHÍ KHÁM CHỮA BỆNH",
                                                                          dtNgayIn.Value);




            //);

        }
        /// <summary>
        /// hàm thực hiện in báo cáo dịch vụ  của phần kỳ đồng
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void KYDONG_PrintDichVu(string sTitleReport)
        {
            Utility.WaitNow(this);
            try
            {
                string PHIEU_NGOAITRU_DV = Utility.sDbnull(BusinessHelper.LayThongTinThamSo("PHIEU_NGOAITRU_DV"));

                switch (PHIEU_NGOAITRU_DV)
                {
                    case "KYDONG":
                        new INPHIEU_THANHTOAN_NGOAITRU().KYDONG_INPHIEU_DICHVU(m_dtReportPhieuThu,dtNgayIn.Value, sTitleReport,"A4");
                        break;
                    case "LAOKHOA":
                        new INPHIEU_THANHTOAN_NGOAITRU().LAOKHOA_INPHIEU_BIENLAI_BHYT_CHO_BN(m_dtReportPhieuThu, dtNgayIn.Value, sTitleReport);
                        break;
                    case "HONGPHUC":
                       // new VietBaIT.HISLink.UI.YHHQ_Reports.Class.INPHIEU_NGOAITRU().HONGPHUC_INPHIEU_DICHVU(m_dtReportPhieuThu, sTitleReport);
                        break;
                    default:
                        new INPHIEU_THANHTOAN_NGOAITRU().KYDONG_INPHIEU_DICHVU(m_dtReportPhieuThu, dtNgayIn.Value, sTitleReport,"A4");
                        break;
                       
                }
               
           
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}",ex.ToString()));
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu bảo hiểm
        /// </summary>
        private void KyDong_InPhieuBH_DungTuyen(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (radInTatCa.Checked) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().KYDONG_GetDataInphieuBH(objPayment,false);
                ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle")) m_dtReportPhieuThu.Columns.Add("Tyle", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("BNTT")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("TyLe_BH")) m_dtReportPhieuThu.Columns.Add("TyLe_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["Tyle"] = (100 - Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh],0)) + " %";
                drv["BHYT"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0) + " %";
                drv["BNTT"] = (100 - v_DiscountRate) + " %";
                drv["TyLe_BH"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0)  + " %";
                drv["Tyle_BN"] = (100 - v_DiscountRate) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            KyDong_PrintBaoHiem_DungTuyen("PHIẾU THANH TOÁN RA VIỆN", m_dtReportPhieuThu);
        }

        private void KyDong_InPhieuBH_BENHNHAN(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (radInTatCa.Checked) objPayment.PaymentId = -1;
            m_dtReportPhieuThu =
                SPs.KydongInphieuBaohiemChoBenhnhan(Utility.Int32Dbnull(objPayment.PaymentId, -1),
                                                    Utility.sDbnull(objPayment.PatientCode),
                                                    Utility.Int32Dbnull(objPayment.PatientId, -1)).GetDataSet().Tables[0
                    ];

            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }

            if (!m_dtReportPhieuThu.Columns.Contains("Tyle")) m_dtReportPhieuThu.Columns.Add("Tyle", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof(string));
           // if (!m_dtReportPhieuThu.Columns.Contains("BNTT")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("TyLe_BH")) m_dtReportPhieuThu.Columns.Add("TyLe_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["Tyle"] = (100 - Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0)) + " %";
                drv["BHYT"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0) + " %";
              //  drv["BNTT"] = (100 - v_DiscountRate) + " %";
                drv["TyLe_BH"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0) + " %";
                drv["Tyle_BN"] = (100 - v_DiscountRate) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            TAMPHUC_INPHIEU_BHYT_CHO_BENHNHAN("PHIẾU THANH TOÁN RA VIỆN", m_dtReportPhieuThu);
        }
        /// <summary>
        /// HÀM THỰC HIỆN IN PHIẾU TRÁI TUYẾN CHO BỆNH NHÂN BẢO HIỂM
        /// </summary>
        /// <param name="IsTongHop"></param>
        private void KyDong_InPhieuBH_TraiTuyen(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().KYDONG_GetDataInphieuBH_TraiTuyen(objPayment);
            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Ty_Le")) m_dtReportPhieuThu.Columns.Add("Ty_Le", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof(string));
           // if (!m_dtReportPhieuThu.Columns.Contains("Ma_ICD")) m_dtReportPhieuThu.Columns.Add("Ma_ICD", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
               // drv["Ma_ICD"] = Utility.sDbnull(drv["MaBenh"], "");
                drv["Ty_Le"] = (100 - v_DiscountRate) + " %";
                drv["BHYT"] = v_DiscountRate + " %";
                drv["BNTT"] = (100 - v_DiscountRate) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            KyDong_PrintBaoHiem_TraiTuyen("BẢNG KÊ CHI PHÍ KHÁM BỆNH,CHỮA BỆNH NGOẠI TRÚ TRÁI TUYẾN");
        }

        /// <summary>
        /// hàm thực hiện in phieus bệnh nhân chỉ định trái tuyến thanh 
        /// </summary>
        private void KyDong_InPhieuBH_ChiDinhTraiTuyen()
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            //if (IsTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu =
                SPs.KydongInPhieubaohiemTraituyen(Utility.Int32Dbnull(objPayment.PaymentId,-1), Utility.sDbnull(objPayment.PatientCode, ""),
                                                  Utility.Int32Dbnull(objPayment.PatientId, -1)).GetDataSet().Tables[0];

            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán)

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Ty_Le")) m_dtReportPhieuThu.Columns.Add("Ty_Le", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("STT")) m_dtReportPhieuThu.Columns.Add("STT", typeof(int));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BH")) m_dtReportPhieuThu.Columns.Add("Tyle_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("DonGia_BH")) m_dtReportPhieuThu.Columns.Add("DonGia_BH", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("DonGia_DV")) m_dtReportPhieuThu.Columns.Add("DonGia_DV", typeof(decimal));
            // if (!m_dtReportPhieuThu.Columns.Contains("Ma_ICD")) m_dtReportPhieuThu.Columns.Add("Ma_ICD", typeof(string));
            int idx = 1;
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                // drv["Ma_ICD"] = Utility.sDbnull(drv["MaBenh"], "");
                drv["Ty_Le"] = (100 - Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh],0)) + " %";
                drv["Tyle_BH"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh],0) + " %";
                drv["Tyle_BN"] = (100 - v_DiscountRate) + " %";
               
                //if(drv["PaymentType_ID"].ToString())
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                switch (drv["PaymentType_ID"].ToString())
                {
                    case "1":
                        drv["PaymentType_Name"] = "Khám bệnh";
                        //drv["Service_Name"] ="Khám bệnh";
                        break;
                    case "2":
                       // drv["Service_ID"] = 0;
                        drv["PaymentType_Name"] = "Dịch vụ";
                        break;
                    case "3":
                       // drv["Service_ID"] = 0;
                        drv["PaymentType_Name"] = "Thuốc";
                        break;
                }
            }
            m_dtReportPhieuThu.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuBHYT_Traituyen();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
           
            crpt.SetDataSource(m_dtReportPhieuThu);
            crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                      Bác sỹ chỉ định ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("Telephone", globalVariables.Branch_Phone);
            crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            //crpt.SetParameterValue("Staff_Name", globalVariables.Branch_Name);
            // crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(BusinessHelper.GetSysDateTime()));
            crpt.SetParameterValue("CurrentDate","Ngày in phiếu: "+ Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", "PHIẾU CHỈ ĐỊNH DÀNH CHO BỆNH NHÂN BHYT TRÁI TUYẾN NỘI TỈNH-TỰ ĐẾN");
            crpt.SetParameterValue("sMoneyCharacter", sMoneyByLetter.sMoneyToLetter(KYDONG_SumOfTotal(m_dtReportPhieuThu).ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        private decimal  KYDONG_SumOfTotal(DataTable dataTable)
        {

            var tong = (from loz in dataTable.AsEnumerable()
                        select loz.Field<decimal>("TONG_BNCT")).Sum();
            return Utility.DecimaltoDbnull(tong);
        }
        /// <summary>
        /// ham thuc hien in phiêu trái tuyến
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void KyDong_PrintBaoHiem_TraiTuyen(string sTitleReport)
        {
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuBHYT_Traituyen_V2();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("Telephone", globalVariables.Branch_Phone);
            crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            // crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(BusinessHelper.GetSysDateTime()));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            crpt.SetParameterValue("sMoneyCharacter", "0");
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        #endregion

        #region "IN PHIẾU DỊCH VỤ HOẶC BẢO HIỂM CỦA ĐƠN VỊ DỆT MAY"
        /// <summary>
        /// hàm thực hiện in phiếu phụ thu của đơn vị dệt may
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBienLai_Click(object sender, EventArgs e)
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
          
            switch (THU_VIEN_CHUNG.LayMaDviLamViec())
            {
                    
                case "DETMAY":
                    DETMAY_InphieuBH_PHUTHU("BẢNG KÊ CHI PHÍ KHÁM BỆNH,CHỮA BỆNH NGOẠI TRÚ");
                    break;
                case "YHOCHAIQUAN":
                    switch (objectType.ObjectTypeType)
                    {
                        ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                        case 1:
                            YHHQ_InPhieuDV(true);
                            break;
                        case 0:
                            YHHQ_InPhieuBH_IN_BN(radInTatCa.Checked);
                           
                            break;
                    }
                    break;
                case "KYDONG":
                    KYDONG_INPHIEU_BENHNHAN_TONGHOP();
                    break;
                case "LAOKHOA":
                     tatca = radInTatCa.Checked ? true : false;
                     LAOKHOA_BIENLAI_THUTIEN(tatca);
                    break;
                default:
                    tatca = radInTatCa.Checked ? true : false;
                    LAOKHOA_BIENLAI_THUTIEN(tatca);
                    break;

            }
            
        }
        public TPatientExam objPatientExam;
       

        /// <summary>
        /// hàm thực hiện việc in phiếu dịch vụ
        /// </summary>
        private void DETMAY_InPhieuDV(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            ///lấy thông tin vào phiếu thu
            m_dtReportPhieuThu = new PhieuThuService().GetDataInphieuDichvu(objPayment);

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["ThanhTien"] = Utility.Int32Dbnull(drv["Quantity"], 0)*
                                   Utility.DecimaltoDbnull(drv["Discount_Price"], 0);
                drv["TotalSurcharge_Price"] = Utility.Int32Dbnull(drv["Quantity"], 0)*
                                              Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.SurchargePrice], 0);
            }
            m_dtReportPhieuThu.AcceptChanges();
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            YHHQ_PrintDichVu("BẢN KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ");
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu bảo hiểm
        /// </summary>
        /// <summary>
        /// việc thực hiện in chi tiết của bảo hiểm hay dịch vụ
        /// </summary>
        private void DETMAY_InPhieuChitietDichVuHoacBaohiem()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                    ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 1:
                    DETMAY_InPhieuDV(false);
                    break;
                case 0:
                    DETMAY_InphieuBH("BẢN KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ",false);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện in phiếu tổng hợp cho đơn vị dệt may
        /// </summary>
        private void DETMAY_InPhieuChitietDichVuHoacBaohiem_TongHop()
        {
            LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
            switch (objectType.ObjectTypeType)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 1:
                    DETMAY_InPhieuDV(true);
                    break;
                case 0:
                    DETMAY_InphieuBH("BẢN KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ",true);
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu bảo hiểm
        /// </summary>
        /// <param name="sTitleReport"></param>
        /// <param name="isTongHop"></param>
        private void DETMAY_InphieuBH(string sTitleReport,bool isTongHop)
        {
            string v_DiagInfo = "";
            string Disease_Code = "";
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (isTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu =
                SPs.DetmayPrintAllExtendExamPaymentDetail(Utility.Int32Dbnull(objPayment.PaymentId,-1),
                                                          objPayment.PatientCode,
                                                          Utility.Int32Dbnull(objPayment.PatientId, -1)).GetDataSet()
                    .Tables[0];
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu tìm thấy,Bạn kiểm tra lại xem đã thanh toán chưa ", "Thông báo");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Origin_Price"))
                m_dtReportPhieuThu.Columns.Add("Origin_Price", typeof (decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Discount_Price"))
                m_dtReportPhieuThu.Columns.Add("Discount_Price", typeof (decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Discount_Rate"))
                m_dtReportPhieuThu.Columns.Add("Discount_Rate", typeof (decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("ObjectType_Name"))
                m_dtReportPhieuThu.Columns.Add("ObjectType_Name", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("Diag_Info"))
                m_dtReportPhieuThu.Columns.Add("Diag_Info", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("Diag_Info_Name"))
                m_dtReportPhieuThu.Columns.Add("Diag_Info_Name", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("Disease_Code"))
                m_dtReportPhieuThu.Columns.Add("Disease_Code", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("TT")) m_dtReportPhieuThu.Columns.Add("TT", typeof (decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("DG")) m_dtReportPhieuThu.Columns.Add("DG", typeof (decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Clinic_Name"))
                m_dtReportPhieuThu.Columns.Add("Clinic_Name", typeof (string));
            if (!m_dtReportPhieuThu.Columns.Contains("Logo")) m_dtReportPhieuThu.Columns.Add("Logo", typeof (byte[]));
            //LSurvey objSurvey = LSurvey.FetchByID(Utility.Int32Dbnull(objPatientInfo.ProvinceId));
            foreach (DataRow dr in m_dtReportPhieuThu.Rows)
            {
                if (!string.IsNullOrEmpty(dr["Survey_Name"].ToString()))
                {
                    dr["Patient_Addr"] += " , " + dr["Survey_Name"];
                }
                dr["Diag_Info_Name"] = v_DiagInfo;

                if (!string.IsNullOrEmpty(dr["Disease_Name"].ToString()))
                {
                    dr["Diag_Info"] = dr["Disease_Name"] + "; " + dr["Chuandoan"];
                }
                else
                {
                    dr["Diag_Info"] = dr["Chuandoan"].ToString();
                }
                //dr["Disease_Code"] = Disease_Code;

                //  }

                dr["Logo"] =
                    Utility.ConvertImageToByteArray(Image.FromFile(Application.StartupPath + @"\logo\logo.jpg"),
                                                    ImageFormat.Jpeg);


                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "1")
                {
                    DataRow[] arrDr1 =
                        globalVariables.g_dtDepartment.Select("Department_ID=" +
                                                              Utility.Int32Dbnull(dr["Service_ID"], -1));
                    if (arrDr1.GetLength(0) > 0)
                    {
                        dr["Service_Name"] = "-Công khám ";
                        dr["ServiceDetail_Name"] = "-Công khám ";
                    }
                    dr["PaymentType_Name"] = "- Công khám";
                }
                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "2")
                {
                    DataRow[] arrDrAss =
                        globalVariables.g_dtServiceList.Select("Service_ID=" + Utility.Int32Dbnull(dr["Service_ID"], -1));

                    if (arrDrAss.GetLength(0) > 0)
                    {
                        dr["Service_Name"] = arrDrAss[0]["Service_Name"].ToString();
                        DataRow[] arrAssDetail =
                            globalVariables.g_dtServiceDetailList.Select("ServiceDetail_ID=" +
                                                                         Utility.Int32Dbnull(dr["ServiceDetail_ID"], -1));
                        if (arrAssDetail.GetLength(0) > 0)
                        {
                            dr["ServiceDetail_Name"] = arrAssDetail[0]["ServiceDetail_Name"].ToString();
                        }
                    }
                }
                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "3")
                {
                    LDrug objDrug = LDrug.FetchByID(Utility.Int32Dbnull(dr["Service_ID"], -1));
                    dr["PaymentType_Name"] = "- Thuốc, dịch truyền trong BHYT";
                    if (objDrug != null)
                    {
                        dr["Service_Name"] = objDrug.DrugName;
                        dr["ServiceDetail_Name"] = dr["Service_Name"].ToString();
                    }
                }
            }
            DETMAY_ProcessDataPaymentDetail(ref m_dtReportPhieuThu);
            SysUserPrinter objSysUserPrinter = SysUserPrinter.FetchByID(globalVariables.UserName);
            Utility.WaitNow(this);
            //var crpt = new rpt_detailRegExam();
            var crpt = new crpt_Thongkechiphikhambenh();
            //var objForm = new frmPrintPreview("IN PHIẾU TỔNG HỢP THANH TOÁN", crpt, true, txtPatientCode.Text == "" ? false : true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + globalVariables.gv_sStaffName + "                                                                  ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("CharacLastPrice",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfDataTable(m_dtReportPhieuThu, "LastPrice", "1=1").ToString()));
            crpt.SetParameterValue("CharacDiscountRate",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "Discount_Rate", " 1=1"), "0")));
            crpt.SetParameterValue("CharacPatientLastPrice",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "BNCT", "1=1"), "0")));
            crpt.SetParameterValue("CharacSurcharge",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "Surcharge_Price", "1=1"), "0")));
            crpt.SetParameterValue("CharacterCT",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfDataTable(m_dtReportPhieuThu, "LastPrice", "1=1").ToString()));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(dtNgayIn.Value));
            crpt.SetParameterValue("DiscountRate", string.Format("Quỹ BHYT thanh toán ({0}%): ", v_DiscountRate));
            crpt.SetParameterValue("AccountName", Utility.sDbnull(objSysUserPrinter.AccountName, ""));
            crpt.SetParameterValue("InsuranceBy", Utility.sDbnull(objSysUserPrinter.InsuranceBy, ""));
            crpt.SetParameterValue("HospitalBy", Utility.sDbnull(objSysUserPrinter.HospitalBy, ""));
            crpt.SetParameterValue("CreatedBy", Utility.sDbnull(objSysUserPrinter.CreatedBy, ""));
            if (objSysUserPrinter != null)
            {
                crpt.PrintToPrinter(Utility.Int32Dbnull(objSysUserPrinter.PagerCopy, 1), false, 0, 0);
                crpt.PrintOptions.PrinterName = objSysUserPrinter.PrinterName;
            }
            else
            {
                crpt.PrintToPrinter(1, false, 1, 1);
            }


            //crpt.SetParameterValue("sMoneyCharacter", sMoneyByLetter.sMoneyToLetter(GetPaymentedOfPatientExam()));
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.ShowDialog();

            Utility.DefaultNow(this);
            //}
            //catch (Exception exception)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// HÀM THỰC HIỆN KÊ IN PHIEUS PHỤ THU CHO BỆNH NHÂN
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void DETMAY_InphieuBH_PHUTHU(string sTitleReport)
        {
            string v_DiagInfo = "";
            string Disease_Code = "";
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
           // if (isTongHop) objPayment.PaymentId = -1;
            m_dtReportPhieuThu =
                SPs.DetmayInphieuBhPhuthu(Utility.Int32Dbnull(objPayment.PaymentId, -1),
                                                          objPayment.PatientCode,
                                                          Utility.Int32Dbnull(objPayment.PatientId, -1)).GetDataSet()
                    .Tables[0];
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu tìm thấy,Bạn kiểm tra lại xem đã thanh toán chưa ", "Thông báo");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Origin_Price"))
                m_dtReportPhieuThu.Columns.Add("Origin_Price", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("PhuThu"))
                m_dtReportPhieuThu.Columns.Add("PhuThu", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("SumofTotal"))
                m_dtReportPhieuThu.Columns.Add("SumofTotal", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("DonGia_Moi"))
                m_dtReportPhieuThu.Columns.Add("DonGia_Moi", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Discount_Price"))
                m_dtReportPhieuThu.Columns.Add("Discount_Price", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Discount_Rate"))
                m_dtReportPhieuThu.Columns.Add("Discount_Rate", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("ObjectType_Name"))
                m_dtReportPhieuThu.Columns.Add("ObjectType_Name", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Diag_Info"))
                m_dtReportPhieuThu.Columns.Add("Diag_Info", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Diag_Info_Name"))
                m_dtReportPhieuThu.Columns.Add("Diag_Info_Name", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Disease_Code"))
                m_dtReportPhieuThu.Columns.Add("Disease_Code", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("TT")) m_dtReportPhieuThu.Columns.Add("TT", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("DG")) m_dtReportPhieuThu.Columns.Add("DG", typeof(decimal));
            if (!m_dtReportPhieuThu.Columns.Contains("Clinic_Name"))
                m_dtReportPhieuThu.Columns.Add("Clinic_Name", typeof(string));
          
            //LSurvey objSurvey = LSurvey.FetchByID(Utility.Int32Dbnull(objPatientInfo.ProvinceId));
            foreach (DataRow dr in m_dtReportPhieuThu.Rows)
            {
                if (!string.IsNullOrEmpty(dr["Survey_Name"].ToString()))
                {
                    dr["Patient_Addr"] += " , " + dr["Survey_Name"];
                }
                dr["Diag_Info_Name"] = v_DiagInfo;

                if (!string.IsNullOrEmpty(dr["Disease_Name"].ToString()))
                {
                    dr["Diag_Info"] = dr["Disease_Name"] + "; " + dr["Chuandoan"];
                }
                else
                {
                    dr["Diag_Info"] = dr["Chuandoan"].ToString();
                }
                //dr["Disease_Code"] = Disease_Code;

              


                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "1")
                {
                    DataRow[] arrDr1 =
                        globalVariables.g_dtDepartment.Select("Department_ID=" +
                                                              Utility.Int32Dbnull(dr["Service_ID"], -1));
                    if (arrDr1.GetLength(0) > 0)
                    {
                        dr["Service_Name"] = "-Công khám ";
                        dr["ServiceDetail_Name"] = "-Công khám ";
                    }
                    dr["PaymentType_Name"] = "- Công khám";
                }
                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "2")
                {
                    DataRow[] arrDrAss =
                        globalVariables.g_dtServiceList.Select("Service_ID=" + Utility.Int32Dbnull(dr["Service_ID"], -1));

                    if (arrDrAss.GetLength(0) > 0)
                    {
                        dr["Service_Name"] = arrDrAss[0]["Service_Name"].ToString();
                        DataRow[] arrAssDetail =
                            globalVariables.g_dtServiceDetailList.Select("ServiceDetail_ID=" +
                                                                         Utility.Int32Dbnull(dr["ServiceDetail_ID"], -1));
                        if (arrAssDetail.GetLength(0) > 0)
                        {
                            dr["ServiceDetail_Name"] = arrAssDetail[0]["ServiceDetail_Name"].ToString();
                        }
                    }
                }
                if (Utility.Int32Dbnull(dr["PaymentType_ID"], 0).ToString() == "3")
                {
                    LDrug objDrug = LDrug.FetchByID(Utility.Int32Dbnull(dr["Service_ID"], -1));
                    dr["PaymentType_Name"] = "- Thuốc, dịch truyền trong BHYT";
                    if (objDrug != null)
                    {
                        dr["Service_Name"] = objDrug.DrugName;
                        dr["ServiceDetail_Name"] = dr["Service_Name"].ToString();
                    }
                }
                dr["DonGia_Moi"] = Utility.DecimaltoDbnull(dr["Origin_Price"], 0) +
                                   Utility.DecimaltoDbnull(dr["Surcharge_Price"], 0);
                dr["SumOfTotal"] = Utility.DecimaltoDbnull(dr["ThanhTien"], 0) +
                                   Utility.DecimaltoDbnull(dr["PhuThu"], 0);

            }
            DETMAY_ProcessDataPaymentDetail(ref m_dtReportPhieuThu);
            Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            SysUserPrinter objSysUserPrinter = SysUserPrinter.FetchByID(globalVariables.UserName);
            Utility.WaitNow(this);
            //var crpt = new rpt_detailRegExam();
            var crpt = new crpt_InphieuBHYT_phuthu();
            //var objForm = new frmPrintPreview("IN PHIẾU TỔNG HỢP THANH TOÁN", crpt, true, txtPatientCode.Text == "" ? false : true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + globalVariables.gv_sStaffName + "                                                                  ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("CharacLastPrice",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfDataTable(m_dtReportPhieuThu, "LastPrice", "1=1").ToString()));
            crpt.SetParameterValue("CharacDiscountRate",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "Discount_Rate", " 1=1"), "0")));
            crpt.SetParameterValue("CharacPatientLastPrice",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "BNCT", "1=1"), "0")));
            crpt.SetParameterValue("CharacSurcharge",
                                   sMoneyByLetter.sMoneyToLetter(
                                       Utility.sDbnull(SumOfDataTable(m_dtReportPhieuThu, "Surcharge_Price", "1=1"), "0")));
            crpt.SetParameterValue("CharacterCT",
                                   sMoneyByLetter.sMoneyToLetter(
                                       SumOfDataTable(m_dtReportPhieuThu, "LastPrice", "1=1").ToString()));
           // crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(BusinessHelper.GetSysDateTime()));
            crpt.SetParameterValue("DiscountRate", string.Format("Quỹ BHYT thanh toán ({0}%): ", v_DiscountRate));
            crpt.SetParameterValue("AccountName", Utility.sDbnull(objSysUserPrinter.AccountName, ""));
            crpt.SetParameterValue("InsuranceBy", Utility.sDbnull(objSysUserPrinter.InsuranceBy, ""));
            crpt.SetParameterValue("HospitalBy", Utility.sDbnull(objSysUserPrinter.HospitalBy, ""));
            crpt.SetParameterValue("CreatedBy", Utility.sDbnull(objSysUserPrinter.CreatedBy, ""));
            if (objSysUserPrinter != null)
            {
                crpt.PrintToPrinter(Utility.Int32Dbnull(objSysUserPrinter.PagerCopy, 1), false, 0, 0);
                crpt.PrintOptions.PrinterName = objSysUserPrinter.PrinterName;
            }
            else
            {
                crpt.PrintToPrinter(1, false, 1, 1);
            }


            //crpt.SetParameterValue("sMoneyCharacter", sMoneyByLetter.sMoneyToLetter(GetPaymentedOfPatientExam()));
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.ShowDialog();

            Utility.DefaultNow(this);
            //}
            //catch (Exception exception)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        /// <summary>
        /// hàm thực hiện xử lý thông tin 
        /// </summary>
        /// <param name="dataTable"></param>
        private void DETMAY_ProcessDataPaymentDetail(ref DataTable dataTable)
        {
            if (!dataTable.Columns.Contains("LastPrice")) dataTable.Columns.Add("LastPrice", typeof (decimal));
            //if (!dataTable.Columns.Contains("Unit_Name")) dataTable.Columns.Add("Unit_Name", typeof(string));
            if (!dataTable.Columns.Contains("BNCT")) dataTable.Columns.Add("BNCT", typeof (decimal));
            if (!dataTable.Columns.Contains("IntOrder")) dataTable.Columns.Add("IntOrder", typeof (int));
            if (!dataTable.Columns.Contains("ServiceType_Type"))
                dataTable.Columns.Add("ServiceType_Type", typeof (int));
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["ServiceDetail_ID"] = Utility.Int32Dbnull(dr["ServiceDetail_ID"], -1);
                if (dr["PaymentType_ID"].ToString() == "1")
                {
                    DataRow[] arrDT =
                        m_dtReportPhieuThu.Select("PaymentType_ID=1 and  Service_ID=" +
                                                  Utility.Int32Dbnull(dr["Service_ID"], -1));
                    if (arrDT.GetLength(0) > 0)
                    {
                        dr["Discount_Price"] = Utility.DecimaltoDbnull(arrDT[0]["Discount_Price"], 0);
                        dr["Discount_Rate"] = Utility.DecimaltoDbnull(arrDT[0]["Discount_Rate"], 0)*
                                              Utility.Int32Dbnull(dr["Quantity"], 0);
                        //dr["Discount_Rate"] = Utility.DecimaltoDbnull(arrDT[0]["Discount_Rate"], 0);
                        dr["LastPrice"] = Utility.DecimaltoDbnull(arrDT[0]["Origin_Price"], 0)*
                                          Utility.Int32Dbnull(dr["Quantity"], 0);
                    }
                }
                if (dr["PaymentType_ID"].ToString() == "2")
                {
                    DataRow[] arrDT =
                        m_dtReportPhieuThu.Select("PaymentType_ID=2 and  ServiceDetail_ID=" +
                                                  Utility.Int32Dbnull(dr["ServiceDetail_ID"], -1));
                    if (arrDT.GetLength(0) > 0)
                    {
                        dr["Discount_Rate"] = Utility.DecimaltoDbnull(arrDT[0]["Discount_Rate"], 0)*
                                              Utility.Int32Dbnull(dr["Quantity"], 0);
                        dr["LastPrice"] = Utility.DecimaltoDbnull(arrDT[0]["Origin_Price"], 0)*
                                          Utility.Int32Dbnull(dr["Quantity"], 0);
                        //dr["PaymentType_Name"] = Utility.DecimaltoDbnull(arrDT[0]["Origin_Price"], 0) * Utility.Int32Dbnull(dr["Quantity"], 0);
                    }
                }
                if (dr["PaymentType_ID"].ToString() == "3")
                {
                    DataRow[] arrDT =
                        m_dtReportPhieuThu.Select("PaymentType_ID=3 and  Service_ID=" +
                                                  Utility.Int32Dbnull(dr["Service_ID"], -1));
                    if (arrDT.GetLength(0) > 0)
                    {
                        dr["Discount_Rate"] = Utility.DecimaltoDbnull(arrDT[0]["Discount_Rate"], 0)*
                                              Utility.Int32Dbnull(dr["Quantity"], 0);
                        dr["LastPrice"] = Utility.DecimaltoDbnull(arrDT[0]["Origin_Price"], 0)*
                                          Utility.Int32Dbnull(dr["Quantity"], 0);
                    }
                }
            }

            dataTable.AcceptChanges();

            foreach (DataRow dr in dataTable.Rows)
            {
                dr["BNCT"] = Utility.DecimaltoDbnull(dr["LastPrice"], 0) -
                             Utility.DecimaltoDbnull(dr["Discount_Rate"], 0);
                if (dr["PaymentType_ID"].ToString() == "3") dr["PaymentType_ID"] = 5;
            }
            dataTable.AcceptChanges();

            foreach (DataRow dr in dataTable.Select("PaymentType_ID=2"))
            {
                dr["ServiceType_Type"] = GetServiceTypeType(Utility.Int32Dbnull(dr["Service_ID"], -1));
            }
            foreach (DataRow dr in dataTable.Select("PaymentType_ID=2"))
            {
                if (dr["ServiceType_Type"].ToString() == "0")
                {
                    dr["PaymentType_ID"] = 2;
                    dr["PaymentType_Name"] = "- Xét nghiệm";
                }
                if (dr["ServiceType_Type"].ToString() == "1")
                {
                    dr["PaymentType_ID"] = 3;
                    dr["PaymentType_Name"] = "- Chẩn đoán hình ảnh";
                }
                if (Utility.Int32Dbnull(dr["ServiceType_Type"], -1) == 2)
                {
                    dr["PaymentType_ID"] = 4;
                    dr["PaymentType_Name"] = "- Phẫu thuật,thủ thuật";
                }
            }
            dataTable.AcceptChanges();
        }

        /// hàm thực hiện lấy thông tin của kiểu dịch vụ
        /// </summary>
        /// <param name="Service_ID"></param>
        /// <returns></returns>
        private int GetServiceTypeType(int Service_ID)
        {
            int ServiceType_ID = -1;
            LService objService = LService.FetchByID(Service_ID);
            if (objService != null)
            {
                LServiceType objServiceType = LServiceType.FetchByID(objService.ServiceTypeId);
                if (objServiceType != null)
                {
                    ServiceType_ID = Utility.Int32Dbnull(objServiceType.ServiceTypeType, -1);
                }
            }
            return ServiceType_ID;
        }

        /// <summary>
        /// hàm thực hiện lấy tổng 
        /// </summary>
        /// <param name="dataTable">nguồn là datatable </param>
        /// <param name="FiledName">trương cần lấy tổng</param>
        /// <param name="sFilter">điều kiện lọc</param>
        /// <returns></returns>
        private decimal SumOfDataTable(DataTable dataTable, string FiledName, string sFilter)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FiledName + ")", sFilter), 0);
        }

        /// <summary>
        /// HÀM THỰC HIỆN CẤU HÌNHM MÁY IN KHI IN PHIẾU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConfigPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new frm_DM_ConfigPaymentPrint();
            frm.ShowDialog();
        }

        #region "PHẦN IN PHIẾU THANH TOÁN NỘI TRÚ "

        #region "in phiếu thanh toán cho đơn vị dệt may"
        /// <summary>
        /// hàm thực hiện in phiếu nội trú
        /// </summary>
        private void DETMAY_InPhieuDichVuHoacBaohiem_NoiTru()
        {
            LObjectType objectType = LObjectType.FetchByID(objPatientExam.ObjectTypeId);
            switch (objectType.ObjectTypeType)
            {
                ///nếu là bảo hiểm thực hiện in phiếu chi tiết của bảo hiểm
                case 1:
                    DETMAY_InPhieuDV(false);
                    break;
                case 0:
                    new DETMAY_InPhieuNoiTru().DETMAY_InphieuBH_NOITRU(
                        "BẢNG KÊ CHI PHÍ KHÁM,CHỮA BỆNH NỘI TRÚ", CreatePayment(), objPatientExam, false);

                    break;
            }
        }
        private TPayment CreatePayment()
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            return objPayment;
        }

        #endregion
        #endregion

        #endregion

        #region "IN PHIẾU DỊCH VỤ HOẶC BẢO HIỂM CỦA ĐƠN VỊ LÃO KHOA"
       
        /// <summary>
        /// hàm thực hiện việc in phiếu bảo hiểm
        /// </summary>
        private void LAOKHOA_InPhieuBH_DungTuyen(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (radInTatCa.Checked) objPayment.PaymentId = -1;
            m_dtReportPhieuThu = new PhieuThuService().KYDONG_GetDataInphieuBH(objPayment, false);
            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle")) m_dtReportPhieuThu.Columns.Add("Tyle", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BNTT")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("TyLe_BH")) m_dtReportPhieuThu.Columns.Add("TyLe_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["Tyle"] = (100 - Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0)) + " %";
                drv["BHYT"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0) + " %";
                drv["BNTT"] = (100 - v_DiscountRate) + " %";
                drv["TyLe_BH"] = Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.PTramBh], 0) + " %";
                drv["Tyle_BN"] = (100 - v_DiscountRate) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            KyDong_PrintBaoHiem_DungTuyen(txtTieuDe.Text, m_dtReportPhieuThu);
        }

        private TPhieuDct CreatePhieuDongChiTra()
        {
            TPhieuDct objPhieuDct = new TPhieuDct();
            objPhieuDct.PatientCode = Utility.sDbnull(objPatientExam.PatientCode);
            objPhieuDct.PatientId = Utility.Int32Dbnull(objPatientExam.PatientId);
            objPhieuDct.KieuThanhtoan = 0;
            objPhieuDct.NguoiTao = globalVariables.UserName;
            objPhieuDct.NgayTao = globalVariables.SysDate;
            objPhieuDct.TienGoc = 0;
            objPhieuDct.TienBnct = 0;
            objPhieuDct.TienBhct = 0;
            return objPhieuDct;
        }
        /// hàm thực hiện việc in phiếu bảo hiểm
        /// </summary>
        private void LAOKHO_INPHIEU_BAOHIEM(bool IsTongHop)
        {
            TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            if (IsTongHop) objPayment.PaymentId = -1;
            TPhieuDct objPhieuDct = CreatePhieuDongChiTra();
            ActionResult actionResult = new PreExtendExamPayment().UpdatePhieuDCT(objPhieuDct, objPatientExam);
            m_dtReportPhieuThu =
                SPs.LaokhoaInphieuBaohiemNgoaitru(Utility.Int32Dbnull(objPayment.PaymentId, -1), Utility.sDbnull(objPayment.PatientCode, ""),
                                         Utility.Int32Dbnull(objPayment.PatientId, -1),0).GetDataSet().Tables[0];

            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán)

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle")) m_dtReportPhieuThu.Columns.Add("Tyle", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BNTT")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("TyLe_BH")) m_dtReportPhieuThu.Columns.Add("TyLe_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["Tyle"] = (100 - v_DiscountRate) + " %";
                drv["BHYT"] = v_DiscountRate + " %";
                drv["BNTT"] = (100 - v_DiscountRate) + " %";
                drv["TyLe_BH"] = v_DiscountRate + " %";
                drv["Tyle_BN"] = (100 - v_DiscountRate) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                if (drv["PaymentType_ID"].ToString() == "1")
                {
                    int Id_Loai_dvu = -1;
                   
                    drv["TEN_LOAI_DVU"] = string.Empty;
                    drv["STT"] = 1;
                    drv["ID_LOAI_DVU"] = -1;
                }
                if (drv["PaymentType_ID"].ToString() == "2")
                {
                    int Id_Loai_dvu = Utility.Int32Dbnull(drv["ID_LOAI_DVU"], -1);
                    LService objService = LService.FetchByID(Id_Loai_dvu);
                    if (objService != null)
                    {
                        drv["TEN_LOAI_DVU"] = Utility.sDbnull(objService.ServiceName);
                        drv["STT"] = Utility.sDbnull(objService.IntOrder);
                    }
                }
                if (drv["PaymentType_ID"].ToString() == "4")
                {
                    int Id_Loai_dvu = Utility.Int32Dbnull(drv["ID_LOAI_DVU"], -1);
                    LRoom objRoom = LRoom.FetchByID(Id_Loai_dvu);
                    if (objRoom != null)
                    {
                        drv["TEN_LOAI_DVU"] = Utility.sDbnull(objRoom.RoomName);
                    }
                }
                if (drv["PaymentType_ID"].ToString() == "3")
                {
                    int Drug_ID = Utility.Int32Dbnull(drv["ID_DVU"], -1);
                    LDrug objDrug = LDrug.FetchByID(Drug_ID);
                    if (objDrug != null)
                    {
                        LDrugType objLDrugType = LDrugType.FetchByID(objDrug.DrugTypeId);
                        if (objLDrugType != null)
                        {
                            if (objLDrugType != null)
                            {
                                if (objLDrugType.DrugTypeCode == "THUOC")
                                {
                                    drv["ID_LOAI_DVU"] = 1;
                                    drv["STT"] = 1;
                                    drv["TEN_LOAI_DVU"] = "Thuốc ";
                                }
                                if (objLDrugType.DrugTypeCode == "VT")
                                {
                                    drv["ID_LOAI_DVU"] = 2;
                                    drv["STT"] = 2;
                                    drv["TEN_LOAI_DVU"] = "Vật tư y tế ";
                                }
                            }
                        }
                    }

                }
                if (drv["PaymentType_ID"].ToString() == "5")
                {
                    drv["ID_LOAI_DVU"] = 1;
                    drv["STT"] = 1;
                    drv["TEN_LOAI_DVU"] = "Chi phí thêm  ";

                }
            }
            m_dtReportPhieuThu.AcceptChanges();
            // KyDong_PrintBaoHiem_DungTuyen(txtTieuDe.Text);
            new INPHIEU_THANHTOAN_NGOAITRU().LAOKHOA_INPHIEU_BAOHIEM_NGOAITRU(m_dtReportPhieuThu, txtTieuDe.Text, dtNgayIn.Value);
        }
        #endregion
        #region "Method of Common"

        private void GetData()
        {
            string ReasonBy = "";

           string sAccountName = THU_VIEN_CHUNG.LayMaDviLamViec();
           LayThongTinPaymentDetail(sAccountName);
            //grdPaymentDetail.DataSource = m_dtPaymentDetail;
          

            SqlQuery sqlQuery = new Select().From(TPhieuthu.Schema)
                .Where(TPhieuthu.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text)).And(
                    TPhieuthu.Columns.LoaiPhieu).IsEqualTo(status);

            if (sqlQuery.GetRecordCount() <= 0)
            {
                TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
                if (objPayment != null)
                {
                    dtCreateDate.Value = Convert.ToDateTime(objPayment.PaymentDate);
                    txtPayment_ID.Text = Utility.sDbnull(objPayment.PaymentId, "-1");
                    txtMA_PTHU.Text = BusinessHelper.GetMaPhieuThu(dtCreateDate.Value,0);
                    Janus.Windows.GridEX.GridEXColumn gridExColumn = grdPaymentDetail.RootTable.Columns["TONG"];
                    txtSO_TIEN.Text = Utility.sDbnull(grdPaymentDetail.GetTotal(gridExColumn, Janus.Windows.GridEX.AggregateFunction.Sum));
                    txtSLUONG_CTU_GOC.Text = "1";
                    TPatientInfo objPatientInfo = TPatientInfo.FetchByID(objPayment.PatientId);
                    if (objPatientInfo != null)
                    {
                        txtNGUOI_NOP.Text = objPatientInfo.PatientName;
                        label9.Text = "Người nhận";
                    }
                    var query = (from loz in grdPaymentDetail.GetDataRows().AsEnumerable()
                                 let x=Utility.sDbnull(loz.Cells[TPaymentDetail.Columns.ServiceDetailName].Value) 
                                 let y=loz.RowType
                                 where(x!="")  &&(y==Janus.Windows.GridEX.RowType.Record)
                                 select x).ToArray();
                    txtLDO_NOP.Text = string.Join(";", query);

                   
                }
            }
            else
            {
                var objPhieuthu = sqlQuery.ExecuteSingle<TPhieuthu>();
                if (objPhieuthu != null)
                {
                    txtSLUONG_CTU_GOC.Text = Utility.sDbnull(objPhieuthu.SluongCtuGoc, 1);
                    txtMA_PTHU.Text = Utility.sDbnull(objPhieuthu.MaPthu, "");
                    txtNGUOI_NOP.Text = Utility.sDbnull(objPhieuthu.NguoiNop);
                    txtSO_TIEN.Text = Utility.sDbnull(objPhieuthu.SoTien);
                    txtTKHOAN_CO.Text = Utility.sDbnull(objPhieuthu.TkhoanCo, "");
                    txtTKHOAN_NO.Text = Utility.sDbnull(objPhieuthu.TkhoanNo, "");
                    txtLDO_NOP.Text = Utility.sDbnull(objPhieuthu.LdoNop);
                    dtCreateDate.Value = Convert.ToDateTime(objPhieuthu.NgayThien);
                }
            }
            sqlQuery = new Select().From(TPaymentDetail.Schema)
                .Where(TPaymentDetail.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text))
                .And(TPaymentDetail.Columns.IsCancel).IsEqualTo(0);
            TPaymentDetailCollection objPaymentDetailCollection =sqlQuery.ExecuteAsCollection<TPaymentDetailCollection>();
            txtSoTienGoc.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.Quantity*c.OriginPrice));
            txtTienBHCT.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.Quantity*c.DiscountRate));
            txtTienBNCT.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.Quantity*c.DiscountPrice));
            txtTienPhuThu.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.Quantity * c.SurchargePrice));
            txtTienBNPhaiTra.Text =
                Utility.sDbnull(Utility.DecimaltoDbnull(txtTienPhuThu.Text) + Utility.DecimaltoDbnull(txtTienBNCT.Text));
            Utility.SetMessage(lblMoneyLetter, new MoneyByLetter().sMoneyToLetter(txtTienBNPhaiTra.Text), true);
        }

        private void LayThongTinPaymentDetail(string sAccountName)
        {
            m_dtPaymentDetail =
                SPs.YhhqLoadDataPaymentDetail(Utility.Int32Dbnull(txtPayment_ID.Text, -1)).GetDataSet().Tables[0];
            if (!m_dtPaymentDetail.Columns.Contains("Thu_Tu_In"))
                m_dtPaymentDetail.Columns.Add("Thu_Tu_In", typeof (int));
            int idx = 1;
            foreach (DataRow drv in m_dtPaymentDetail.Rows)
            {
              
                drv["PT"] = Utility.Int32Dbnull(drv["Quantity"], 0)*
                            Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.SurchargePrice], 0);
                if (sAccountName!="DETMAY")
                {
                    drv["Thu_Tu_In"] = idx;
                    idx++;
                }
            }
            m_dtPaymentDetail.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdPaymentDetail, m_dtPaymentDetail,false,true,"1=1","");
        }

        /// <summary>
        /// hàm thực hiện lấy tổng của phần thanh toán
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private decimal SumOfPaymentDetail(DataTable dataTable)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(TT)+SUM(PT)", "1=1"));
        }

        #endregion

       
        /// <summary>
        /// hàm thực hiện in phiếu tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintAll_Click(object sender, EventArgs e)
        {
            switch (sAccountName)
            {
                case "YHOCHAIQUAN":
                    YHHQ_InPhieuChitietDichVuHoacBaohiem_TongHop();
                    break;
                case "DETMAY":
                    DETMAY_InPhieuChitietDichVuHoacBaohiem_TongHop();
                    break;
                case "KYDONG":
                    if (BusinessHelper.GetParamValue("PHIEU_NGOAITRU_DV")== "HONGPHUC")
                        HONGPHUC_InPhieuChitietDichVuHoacBaohiem_TONGHOP();
                    else
                    {
                        KYDONG_InPhieuChitietDichVuHoacBaohiem_TONGHOP();                        
                    }                   
                    break;
                case "LAOKHOA":
                  
                    LAOKHO_INPHIEU_BAOHIEM(true);
                    break;
                default:
                    LAOKHO_INPHIEU_BAOHIEM(true);
                    break;
            }
        }
        private bool InVali()
        {
            SqlQuery sqlQuery1 = new Select(LService.Columns.ServiceId).From(LService.Schema)
                .InnerJoin(LServiceType.ServiceTypeIdColumn, LService.ServiceTypeIdColumn)
                .Where(LServiceType.Columns.ServiceTypeType).IsEqualTo(0);
                 
            SqlQuery sqlQuery = new Select(TPaymentDetail.Columns.PaymentDetailId).From(TPaymentDetail.Schema)
                .Where(TPaymentDetail.Columns.PaymentTypeId).IsEqualTo(2)
                .And(TPaymentDetail.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text, -1))
                .And(TPaymentDetail.Columns.ServiceId).In(sqlQuery1);
            if(sqlQuery.GetRecordCount()<=0)
            {
                Utility.ShowMsg(
                    "Hiện tại không có phiếu xét nghiệm nào tồn tại trong lần thanh toán này,Yêu cầu người dùng kiểm tra lại ",
                    "Thông báo");
                cmdInPhieuXN.Focus();
                return false;
            }
            return true;

        }
        /// <summary>
        /// hàm thực hiện việc cho phép in phiếu xét nghiệm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            //if(grdPaymentDetail.CurrentRow!=null)
            //{
            //    if(!InVali())return;
            //    TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
            //    if (objPayment != null)
            //    {
            //        VNS.HIS.Reports.Form_TAMPHUC.frm_KYDONG_INPHIEU_XETNGHIEM frm = new frm_KYDONG_INPHIEU_XETNGHIEM();
            //        frm.objPayment = objPayment;
            //        frm.ShowDialog();
            //    }
            //}
            
        
        }
        /// <summary>
        /// hàm thực hiện việc số tienf định dạng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSO_TIEN_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtSO_TIEN);
        }

        private void grdPaymentDetail_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hienenj việc cho in phiếu đồng chi trả thông tin 
        /// của benh nhân
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieu_DongChiTra_Click(object sender, EventArgs e)
        {
            try
            {
                if(objPatientExam!=null)
                {
                    frm_KiemTra_BHYT frm = new frm_KiemTra_BHYT();
                    frm.tieude = Utility.sDbnull(txtTieuDe.Text);
                    frm.ObjPatientExam = objPatientExam;
                    frm.ShowDialog();
                }               

            }catch(Exception)
            {
                
            }
         
        }

      

      
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void grpTongTien_Click(object sender, EventArgs e)
        {

        }
       


     
    }
}