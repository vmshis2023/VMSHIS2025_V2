using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Newtonsoft.Json;
using VietBaIT.CommonLibrary;
using SubSonic;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.LoadDataCommon;
using VietbaIT.Vacom.Invoice.Model;
using NLog;
using VietBaIT.HISLink.UI.ControlUtility.Log;

namespace VMS.Invoice
{
    public partial class frm_Misa_Patient : Form
    {
        private DataTable _mDtTimKiem;
        private Logger log;
        private MisaInvoicesConnection _MisaInvoices = new MisaInvoicesConnection();

        public frm_Misa_Patient()
        {
            InitializeComponent();
            log = LogManager.GetCurrentClassLogger();
            Utility.SetVisualStyle(this);
            m_oWorker = new BackgroundWorker();
            m_oWorker.DoWork += m_oWorker_DoWork;
            m_oWorker.ProgressChanged += m_oWorker_ProgressChanged;
            m_oWorker.RunWorkerCompleted += m_oWorker_RunWorkerCompleted;
            m_oWorker.WorkerReportsProgress = true;
            m_oWorker.WorkerSupportsCancellation = true;
            dtDenNgay.Value = dtTuNgay.Value = BusinessHelper.GetSysDateTime();

        }

        private string Invoices_GetWebPath(string macoso)
        {
            string webpath = string.Empty;
            DataTable dt = SPs.LSysWebServicePathGet("MISA_INVOICE", macoso).GetDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                webpath = Utility.sDbnull(dt.Rows[0]["WebPath_Name"], "");
            }
            return webpath;
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int status = -1;
                if (radDaHoaDon.Checked) status = 1;
                if (radChuaHoaDon.Checked) status = 0;
                if (radDaHuy.Checked) status = 2;
                if(radChuaHoaDon.Checked)
                {
                    _mDtTimKiem = SPs.SpSearchPatientInvoice(dtTuNgay.Value, dtDenNgay.Value, txtPatient_Code.Text, txtTenBN.Text,
                                          Utility.sDbnull(cboCoSo.SelectedValue),
                                          globalVariables.MA_BENHVIEN, status, "").GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdListChuaTaoHoaDon, _mDtTimKiem, true, true, "", "");
                }
                if (radDaHoaDon.Checked)
                {
                    var sp = SPs.SpSearchPatientInvoice(dtTuNgay.Value, dtDenNgay.Value, txtPatient_Code.Text, txtTenBN.Text,
                                          Utility.sDbnull(cboCoSo.SelectedValue),
                                          globalVariables.MA_BENHVIEN, status, "");
                    sp.CommandTimeout = globalVariables.CommandTimeout;
                    _mDtTimKiem = sp.GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdListDaTaoHoaDon, _mDtTimKiem, true, true, "TRANG_THAI=0", "");
                }
                if (radDaHuy.Checked)
                {
                    var sp = SPs.SpSearchPatientInvoice(dtTuNgay.Value, dtDenNgay.Value, txtPatient_Code.Text, txtTenBN.Text,
                                          Utility.sDbnull(cboCoSo.SelectedValue),
                                          globalVariables.MA_BENHVIEN, status, "");
                    sp.CommandTimeout = globalVariables.CommandTimeout;
                    _mDtTimKiem = sp.GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdListHoaDonDaHuy, _mDtTimKiem, true, true, "TRANG_THAI=1", "");
                }
            }
            catch (Exception ex)
            { 
                Utility.ShowMsg(ex.Message);
            }
         
        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdSave.Enabled = true;
        }

        private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateHoaDon(e);
        }

        private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Misa_Patient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
        }


        private void frm_Misa_Patient_Load(object sender, EventArgs e)
        {
            PhanQuyenChucNang();
            LoadDataToComboboxCoSo();
        }

        private void LoadDataToComboboxCoSo()
        {
            DataTable dataTable =
                CommonBusiness.LoadCoSo_NhanVien_BaoCao(Utility.Int16Dbnull(globalVariables.gv_StaffID));
            DataBinding.BindData(cboCoSo, dataTable, LCoSo.Columns.MaCoSo, LCoSo.Columns.TenCoSo);

        }
        private void TimKiem()
        {
           
        }
        private DataTable dtCapPhat;
        private int HOADON_CAPPHAT_ID = -1;
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string patientCodeFilter = BusinessHelper.SinhMaHoSoKhiTimKiem(txtPatient_Code.Text);
                txtPatient_Code.Text = patientCodeFilter;
                cmdTimKiem.PerformClick();
            }
                
        }

        private int HoaDon_Mau_ID = -1;

        private BackgroundWorker m_oWorker;

        /// <summary>
        /// hàm thưc hiện lưu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sDataRequest = "";
                string result = string.Empty;
                int _tongso = grdListChuaTaoHoaDon.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tongso, true);
                foreach (GridEXRow gridExRow in grdListChuaTaoHoaDon.GetCheckedRows())
                {
                    string eMessage = "";
                    string sThamSo = Utility.sDbnull(gridExRow.Cells[TPayment.Columns.PatientCode].Value);
                    bool kt = false;
                    string _paymentId = "";
                    DataTable dt =  SPs.SpInvoiceGetPayment(Utility.sDbnull(gridExRow.Cells[TPayment.Columns.PatientCode].Value), -1,
                            0).GetDataSet().Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.AsEnumerable())
                        {
                            _paymentId = !string.IsNullOrEmpty(_paymentId)
                                ? _paymentId + "," + Utility.sDbnull(row["Payment_Id"], 0)
                                : Utility.sDbnull(row["Payment_Id"], 0);
                        }
                    }
                    else
                    {
                        break;
                    }
                    kt = _MisaInvoices.CreateInvoicesSaveSignServer(_paymentId, 0, ref eMessage);
                    if (kt)
                    {
                          string eMessge = "";
                          kt = _MisaInvoices.SaveFileInvoice(_paymentId, false, ref eMessge);
                          if (kt)
                          {
                              //StoredProcedure sp =
                              //    SPs.SpCapnhapHoadonLog(objPdfhoadon.fKey,
                              //        globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
                              //sp.Execute();
                              //HISLinkLog.Log(this.Name, globalVariables.UserName,
                              //    string.Format(
                              //        "Tải thành công hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}",
                              //        objPdfhoadon.fKey, "", objPdfhoadon.pattern, objPdfhoadon.serial), action.Update, "UI");
                             
                          }
                          gridExRow.IsChecked = false;
                          LogText(eMessage, Color.DarkBlue);
                    }
                    else
                    {
                        LogText(eMessage, Color.Red);
                    }
                    SetValue4Prg(ProgressBar, 1);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi xuất hóa đơn: {0} ",
                                     ex.Message), action.Insert, "UI");
            }
            finally
            {
                cmdSave.Enabled = true;
            }
           
        }

        private void UpdateHoaDon(DoWorkEventArgs e)
        {
          
        }
        private delegate void SetPrgValue(Janus.Windows.EditControls.UIProgressBar Prg, int _Value);
        private void SetValue4Prg(Janus.Windows.EditControls.UIProgressBar Prg, int _Value)
        {
            try
            {
                if (Prg.InvokeRequired)
                {
                    Prg.Invoke(new SetPrgValue(SetValue4Prg), new object[] { Prg, _Value });
                }
                else
                {
                    if (Prg.Value + _Value <= Prg.Maximum) Prg.Value += _Value;
                    Prg.Refresh();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private const string _sNewline = "\r\n";
        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }
        public delegate void AddLog(string logText, Color sActionColor);
        public void LogText(string sLogText, Color sActionColor)
        {
            if (InvokeRequired)
            {
                Invoke(new AddLog(LogText), new object[] { sLogText, sActionColor });
            }
            else
            {
                AddAction(sLogText, sActionColor);
                rtxtLogs.AppendText(_sNewline);
            }
        }

        private void cmdCancelinvoices_Click(object sender, EventArgs e)
        {
            try
            {
                string returnMessage = "";
                decimal tongBntt = 0; //grdPayment.GetDataRows().Sum();   
                int _tong_sohuy = grdListDaTaoHoaDon.GetCheckedRows().Count();
                Utility.ResetProgressBarJanus(ProgressBar, _tong_sohuy, true);
                foreach (GridEXRow gridExRow in grdListDaTaoHoaDon.GetCheckedRows())
                {
                    string _paymentId = "";
                    int hdon_log_id = Utility.Int32Dbnull(gridExRow.Cells[ LHoadonLog.Columns.HdonLogId].Value);
                    string serie = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.Serie].Value);
                    Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[LHoadonLog.Columns.TrangThai].Value);
                    string mauhdon = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.MauHdon].Value);
                    string kihieu = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.KiHieu].Value);                    
                    bool thanhcong = false;
                    DataSet dshoadonlog = SPs.SpGetinvoiceSerie(serie, mauhdon, kihieu,0).GetDataSet();
                    foreach (DataRow row in dshoadonlog.Tables[0].AsEnumerable())
                    {
                        if (Utility.sDbnull(row[LHoadonLog.Columns.DaGui], 0) == "1" &&
                            Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "") != "")
                        {
                            log.Trace("Bat dau Huy hoa don dien tu cho hóa đơn: " + serie);
                            string invInvoiceAuthId = Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "");
                            thanhcong = _MisaInvoices.CancelInvoice(Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], ""), serie, globalVariables.SysDate, "Hủy hóa đơn", ref returnMessage);
                            if (!thanhcong)
                            {
                                log.Trace("Huy hoa don: Error " + returnMessage);
                                LogText(returnMessage, Color.Red);
                                Utility.ShowMsg(string.Format("Hủy hóa đơn không thành công, lỗi: {0}, SESIE :{1}, hdon_log_id : {2} ", returnMessage, serie, hdon_log_id));
                                HISLinkLog.Log(this.Name, globalVariables.UserName,
                                    string.Format(
                                        "Hủy hóa đơn không thành công, lỗi: {0}, SESIE :{1}, hdon_log_id : {2} ",
                                        returnMessage, serie, hdon_log_id), action.Update, "UI");
                            }
                            else
                            {
                                log.Trace("Huy hoa don: Susscess " + returnMessage);
                                // update bảng thanh toán và bảng hóa đơn đỏ                        
                                SPs.SpInvoiceUpdateTpaymentInvInvoiceAuthId(invInvoiceAuthId, globalVariables.UserName, BusinessHelper.GetSysDateTime(), BusinessHelper.GetMACAddress()).Execute();
                                gridExRow.BeginEdit();
                                gridExRow.Cells["TRANG_THAI"].Value = 1;
                                gridExRow.Cells["CHON"].Value = 0;
                                gridExRow.EndEdit();
                                Utility.ShowMsg(string.Format("Hủy hóa đơn thành công: {0}, SESIE :{1}, hdon_log_id : {2} ", returnMessage, serie, hdon_log_id));
                                HISLinkLog.Log(this.Name, globalVariables.UserName,
                                    string.Format("Hủy hóa đơn đỏ, InvInvoiceAuthId hóa đơn: {0}, SESIE :{1}",
                                        Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], ""), serie),
                                    action.Update, "UI");
                            }
                        }
                    }
                    SetValue4Prg(ProgressBar, 1);
                    HISLinkLog.Log(this.Name, globalVariables.UserName,
                        string.Format("Hủy hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie,
                            trangthai, mauhdon, kihieu), action.Update, "UI");
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Lỗi khi hủy hóa đơn: {0}", ex.Message), action.Update, "UI");

            }
        }
        private void cmddownload_Click(object sender, EventArgs e)
        {
            try
            {
                if(radDaHoaDon.Checked)
                {
                    string eMessge = "";
                    Utility.ResetProgressBarJanus(ProgressBar, grdListDaTaoHoaDon.GetCheckedRows().Count(), true);
                    log.Trace("Tải File invInvoiceAuthId: " + grdListDaTaoHoaDon.GetCheckedRows().Count().ToString());
                    foreach (GridEXRow gridExRow in grdListDaTaoHoaDon.GetCheckedRows())
                    {
                        int hdon_log_id = Utility.Int32Dbnull(gridExRow.Cells[LHoadonLog.Columns.HdonLogId].Value);
                        string serie = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.Serie].Value);
                        Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[LHoadonLog.Columns.TrangThai].Value);
                        string mauhdon = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.MauHdon].Value);
                        string kihieu = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.KiHieu].Value);
                        DataSet dshoadonlog = SPs.SpGetinvoiceSerie(serie, mauhdon, kihieu, 0).GetDataSet();

                        // lay thong tin hóa đơn hủy bỏ 
                        foreach (DataRow row in dshoadonlog.Tables[0].AsEnumerable())
                        {
                            if (Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "") != "")
                            {
                                string invInvoiceAuthId = Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "");
                                log.Trace("Tải File invInvoiceAuthId: " + invInvoiceAuthId);
                                if (string.IsNullOrEmpty(invInvoiceAuthId)) return;
                                bool kt = _MisaInvoices.SaveFileInvoice(invInvoiceAuthId, false, ref eMessge);
                                if (kt)
                                {
                                    StoredProcedure sp =
                                        SPs.SpCapnhapHoadonLog(
                                            Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], ""),
                                            globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
                                    sp.Execute();
                                    gridExRow.IsChecked = false;
                                }
                            }
                        }
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, mauhdon, kihieu), action.Update, "UI");
                        SetValue4Prg(ProgressBar, 1);
                        Application.DoEvents();
                    }

                }
                if (radDaHuy.Checked)
                {
                    string eMessge = "";
                    Utility.ResetProgressBarJanus(ProgressBar, grdListHoaDonDaHuy.GetCheckedRows().Count(), true);
                    foreach (GridEXRow gridExRow in grdListHoaDonDaHuy.GetCheckedRows())
                    {
                        int hdon_log_id = Utility.Int32Dbnull(gridExRow.Cells[LHoadonLog.Columns.HdonLogId].Value);
                        string serie = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.Serie].Value);
                        Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[LHoadonLog.Columns.TrangThai].Value);
                        string mauhdon = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.MauHdon].Value);
                        string kihieu = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.KiHieu].Value);
                        DataSet dshoadonlog = SPs.SpGetinvoiceSerie(serie, mauhdon, kihieu, 1).GetDataSet();
                        foreach (DataRow row in dshoadonlog.Tables[0].AsEnumerable())
                        {
                            if (Utility.sDbnull(row[LHoadonLog.Columns.DaGui], 0) == "1" &&  Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "") != "")
                            {
                                string invInvoiceAuthId = Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "");
                                if (string.IsNullOrEmpty(invInvoiceAuthId)) return;
                                bool kt = _MisaInvoices.SaveFileInvoice(invInvoiceAuthId, false, ref eMessge);
                                if (kt)
                                {
                                    StoredProcedure sp =
                                        SPs.SpCapnhapHoadonLog(
                                            Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], ""),
                                            globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
                                    sp.Execute();
                                    gridExRow.IsChecked = false;
                                }
                            }
                        }
                        HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn đã hủy: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, mauhdon, kihieu), action.Update, "UI");
                        SetValue4Prg(ProgressBar, 1);
                        Application.DoEvents();
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Tải hóa đơn bị lỗi: {0},", ex.Message), action.Update, "UI");

            }
        }
        int huybo = 0;
        private void uiTab2_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            if (uiTab2.SelectedTab == uitabChuaTaoHoaDon)
            {
                radChuaHoaDon.Checked = true;
                cmdSave.Enabled = true;
                cmddownload.Enabled = false;
                cmdCancelinvoices.Enabled = false;
                huybo = 0;
            }
            if (uiTab2.SelectedTab == uiTabDanhSachBenhNhanDaCoHoaDon)
            {
                radDaHoaDon.Checked = true;
                cmdSave.Enabled = false;
                cmddownload.Enabled = true;
                cmdCancelinvoices.Enabled = true;
                huybo = 0;
            }
            if (uiTab2.SelectedTab == uiTabDanhSachHoaDonDaHuy)
            {
                radDaHuy.Checked = true;
                cmdSave.Enabled = false;
                cmddownload.Enabled = true;
                cmdCancelinvoices.Enabled = false;
                huybo = 1;
            }
            PhanQuyenChucNang();
        }

        private void radChuaHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (radChuaHoaDon.Checked) uiTab2.SelectedTab = uitabChuaTaoHoaDon;
        }

        private void radDaHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (radDaHoaDon.Checked) uiTab2.SelectedTab = uiTabDanhSachBenhNhanDaCoHoaDon;
        }

        private void radDaHuy_CheckedChanged(object sender, EventArgs e)
        {
            if (radDaHuy.Checked) uiTab2.SelectedTab = uiTabDanhSachHoaDonDaHuy;
        }

        private void grdListDaTaoHoaDon_SelectionChanged(object sender, EventArgs e)
        {
             try
            {
            int hdon_log_id = Utility.Int32Dbnull(grdListDaTaoHoaDon.GetValue(LHoadonLog.Columns.HdonLogId));
            string serie  = Utility.sDbnull(grdListDaTaoHoaDon.GetValue(LHoadonLog.Columns.Serie));
            Int16 trangthai  = Utility.Int16Dbnull(grdListDaTaoHoaDon.GetValue(LHoadonLog.Columns.TrangThai));
            string mauhdon = Utility.sDbnull(grdListDaTaoHoaDon.GetValue(LHoadonLog.Columns.MauHdon));
            string kihieu = Utility.sDbnull(grdListDaTaoHoaDon.GetValue(LHoadonLog.Columns.KiHieu));
            DataTable dtDetailInvoice = SPs.SpSearchDetailInvoice(serie, mauhdon, kihieu, hdon_log_id, trangthai).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdListDaTaoHoaDon_ChiTiet, dtDetailInvoice, false, true, "1=1", "PATIENT_CODE desc");
            }
             catch (Exception ex)
             {
                 Utility.ShowMsg(ex.Message);
             }
            
        }

  

        private void grdListHoaDonDaHuy_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int hdon_log_id = Utility.Int32Dbnull(grdListHoaDonDaHuy.GetValue(LHoadonLog.Columns.HdonLogId));
                string serie = Utility.sDbnull(grdListHoaDonDaHuy.GetValue(LHoadonLog.Columns.Serie));
                Int16 trangthai = Utility.Int16Dbnull(grdListHoaDonDaHuy.GetValue(LHoadonLog.Columns.TrangThai));
                string mauhdon = Utility.sDbnull(grdListHoaDonDaHuy.GetValue(LHoadonLog.Columns.MauHdon));
                string kihieu = Utility.sDbnull(grdListHoaDonDaHuy.GetValue(LHoadonLog.Columns.KiHieu));
                DataTable dtDetailInvoice = SPs.SpSearchDetailInvoice(serie, mauhdon, kihieu, hdon_log_id, trangthai).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdListHoaDonDaHuy_ChiTiet, dtDetailInvoice, false, true, "1=1", "PATIENT_CODE desc");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            
        }

        private void cmdLayHoaDonTong_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Misa_ListPatient frm = new frm_Misa_ListPatient();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
          
        }

        private void cmdlaythongbaophathanh_Click(object sender, EventArgs e)
        {
            TemplateData listhoadonphathanh = _MisaInvoices.Laydanhsachphathanh(); 
            List<TemplateData> lstTemplateDatas = new List<TemplateData>();
            lstTemplateDatas.Add(listhoadonphathanh);
            gridEX1.DataSource = lstTemplateDatas;
        }

        private void cmdChangeDinhDanh_Click(object sender, EventArgs e)
        {
            try
            {
                string invInvoiceAuthId =
                    Utility.sDbnull(grdListDaTaoHoaDon.CurrentRow.Cells["inv_InvoiceAuth_id"].Value, "");
                if (!string.IsNullOrEmpty(invInvoiceAuthId))
                {
                    FrmThongtinDieuchinh frm = new FrmThongtinDieuchinh();
                    frm.InvInvoiceAuthId = invInvoiceAuthId;
                    //frm._invoicesConnectionDieuchinh = _invoicesConnection;
                    frm.ShowDialog();
                    if (frm.isCancel)
                    {
                        grdListDaTaoHoaDon.SelectionChanged += grdListDaTaoHoaDon_SelectionChanged;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không tồn tại dữ liệu với inv_InvoiceAuth_id = " + " ");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdCapnhatthongtinthue_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.isValidGrid(grdListChuaTaoHoaDon))
                {
                    string patientCode = Utility.sDbnull(grdListChuaTaoHoaDon.CurrentRow.Cells[TRegExam.Columns.PatientCode].Value, -1);
                    int patienId = Utility.Int32Dbnull(grdListChuaTaoHoaDon.CurrentRow.Cells[TRegExam.Columns.PatientId].Value, -1);
                    if (patientCode != "")
                    { 
                        var frm = new ControlUtility.FORM_CHUNG.Frm_CapNhat_Thongtin_Thue(patientCode, patienId);
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin) Utility.ShowMsg(ex.Message);
                log.Trace(ex.Message);
            }
        }
        private void PhanQuyenChucNang()
        {
            cmdSave.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_GOC", false);
            cmddownload.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_GOC", false) || BusinessHelper.IsLoadMaPhanQuyen("HOADON_DIEUCHINH", false) 
                || BusinessHelper.IsLoadMaPhanQuyen("HOADON_XUATTONG", false); //quyền hóa đơn gốc và quyền điều chỉnh đều có thể tải hóa đơn
            cmdCancelinvoices.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_HUYBO", false);
            cmdLayHoaDonTong.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_XUATTONG", false);
            cmdChangeDinhDanh.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_DIEUCHINH", false);
            if(huybo ==0)
            {
                cmddownload.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_GOC", false) 
                    || BusinessHelper.IsLoadMaPhanQuyen("HOADON_DIEUCHINH", false)
                    || BusinessHelper.IsLoadMaPhanQuyen("HOADON_XUATTONG", false); //quyền hóa đơn gốc và quyền điều chỉnh đều có thể tải hóa đơn
            }
            else //(huybo == 0)
            {
                cmddownload.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_HUYBO", false)
                    || BusinessHelper.IsLoadMaPhanQuyen("HOADON_DIEUCHINH", false); 
            }
        }

        private void cmdInHoaDonChuyenDoi_Click(object sender, EventArgs e)
        {
            //string eMessge = "";
            //Utility.ResetProgressBarJanus(ProgressBar, grdListDaTaoHoaDon.GetCheckedRows().Count(), true);
            //foreach (GridEXRow gridExRow in grdListDaTaoHoaDon.GetCheckedRows())
            //{
            //    int hdon_log_id = Utility.Int32Dbnull(gridExRow.Cells[LHoadonLog.Columns.HdonLogId].Value);
            //    string serie = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.Serie].Value);
            //    Int16 trangthai = Utility.Int16Dbnull(gridExRow.Cells[LHoadonLog.Columns.TrangThai].Value);
            //    string mauhdon = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.MauHdon].Value);
            //    string kihieu = Utility.sDbnull(gridExRow.Cells[LHoadonLog.Columns.KiHieu].Value);
            //    DataSet dshoadonlog = SPs.SpGetinvoiceSerie(serie, mauhdon, kihieu, 0).GetDataSet();

            //    // lay thong tin hóa đơn hủy bỏ 



            //    foreach (DataRow row in dshoadonlog.Tables[0].AsEnumerable())
            //    {
            //        if (Utility.sDbnull(row[LHoadonLog.Columns.DaGui], 0) == "1" &&
            //            Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "") != "")
            //        {
            //            string invInvoiceAuthId = Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], "");

            //            if (string.IsNullOrEmpty(invInvoiceAuthId)) return;
            //            bool kt = _HiloInvoices.Inchuyendoi(invInvoiceAuthId, false, ref eMessge);
            //            if (kt)
            //            {
            //                StoredProcedure sp =
            //                    SPs.SpCapnhapHoadonLog(
            //                        Utility.sDbnull(row[LHoadonLog.Columns.InvInvoiceAuthId], ""),
            //                        globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
            //                sp.Execute();
            //                gridExRow.IsChecked = false;
            //            }
            //        }
            //    }
            //    HISLinkLog.Log(this.Name, globalVariables.UserName, string.Format("Tải thành công hóa đơn chuyển đổi: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}", serie, trangthai, mauhdon, kihieu), action.Update, "UI");
            //    SetValue4Prg(ProgressBar, 1);
            //    Application.DoEvents();
            //}
        }

        private void cmdXemLog_Click(object sender, EventArgs e)
        {
            try
            {
                frm_log frm = new frm_log();
                frm.iscall = true;
                frm.noidung = "hóa đơn";
                frm.t_tungay = dtTuNgay.Value;
                frm.t_denngay = dtDenNgay.Value;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
    }
