using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VietbaIT.Vacom.Invoice.Model;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.LoadDataCommon;
using VietBaIT.HISLink.UI.RedInvoice.ClassBus;

namespace VMS.Invoice
{
    public partial class frm_Misa_ListPatient : Form
    {
        private DataTable _mDtTimKiem;
        private Logger log;

        private MisaInvoices _misaClass = new MisaInvoices();
        private CultureInfo cultures = new CultureInfo("en-US");
        private MisaInvoicesConnection _MisaInvoices = new MisaInvoicesConnection();
        public frm_Misa_ListPatient()
        {
            InitializeComponent();
            log = LogManager.GetCurrentClassLogger();
            Utility.SetVisualStyle(this);
           dtpngayhoadon.Value =  dtDenNgay.Value = dtTuNgay.Value = BusinessHelper.GetSysDateTime(); 
        }
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                sotienthu = 0;
                txttennguoimua.Clear();
                txtSoTienHoaDon.Clear();
                txtmadonvi.Clear();
                txttendonvi.Clear();
                txtmasothue.Clear();
                txtemail.Clear();
                txtdiachi.Clear();
                txtstknguoimua.Clear();
                int status = -1;
                int loaibnxuat = 0;
                if (radDaHoaDon.Checked) status = 1;
                if (radChuaHoaDon.Checked) status = 0;
                if (chkXuatBNGoi.Checked) loaibnxuat = 1; 
                var sp = SPs.SpTimKiemHoaDon(dtTuNgay.Value, dtDenNgay.Value, txtPatient_Code.Text, txtTenBN.Text,
                        Utility.sDbnull(cboCoSo.SelectedValue), globalVariables.MA_BENHVIEN, status,"", loaibnxuat);
                sp.CommandTimeout = globalVariables.CommandTimeout;
                _mDtTimKiem = sp.GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiem, true, true, "", "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Misa_ListPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdSendInvoices.PerformClick();
            if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
        }


        private void frm_Misa_ListPatient_Load(object sender, EventArgs e)
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
        private DataTable dtCapPhat;
        private int HOADON_CAPPHAT_ID = -1;
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdTimKiem.PerformClick();
        }

        private int HoaDon_Mau_ID = -1;

        private BackgroundWorker m_oWorker;
        private bool InVali()
        {
            if (string.IsNullOrEmpty(txttennguoimua.Text))
            {
                Utility.ShowMsg("Tên người mua không được để trống", "Thông báo", MessageBoxIcon.Warning);
                txttennguoimua.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtSoTienHoaDon.Text) <=0)
            {
                Utility.ShowMsg("Số tiền hóa đơn phải lớn hơn 0, Yêu cầu kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                txtSoTienHoaDon.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thưc hiện lưu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InVali()) return;
                cmdSendInvoices.Enabled = false;
               
                var objNguoiMua = new ThongTinNguoiMua();
                objNguoiMua.ten_donvi = txttendonvi.Text;
                objNguoiMua.ten_nguoimua = txttennguoimua.Text;
                objNguoiMua.dia_chi = txtdiachi.Text;
                objNguoiMua.email = txtemail.Text;
                objNguoiMua.ma_donvi = txtmadonvi.Text; 
                objNguoiMua.ma_sothue = txtmasothue.Text;
                objNguoiMua.so_tienhoadon = txtSoTienHoaDon.Text;
                objNguoiMua.so_tienthu = txtSoTienThu.Text;
                objNguoiMua.ngay_hoadon = Convert.ToDateTime(dtpngayhoadon.Value, cultures).ToString("yyyy-MM-dd");
                string sThamso = "";
                string paymentId = "";
                Utility.ResetProgressBarJanus(ProgressBar, 1, true);
                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    sThamso = !string.IsNullOrEmpty(sThamso)
                        ? sThamso + "," + Utility.sDbnull(gridExRow.Cells["Payment_Id"].Value, 0)
                        : Utility.sDbnull(gridExRow.Cells["Payment_Id"].Value, 0);
                }
                paymentId = sThamso;
                string eMessga = "";
                if(!string.IsNullOrEmpty(paymentId))
                {
                    int benhnhanGoi = chkXuatBNGoi.Checked ? 1 : 0;
                    string transactionId = "";
                    bool kt = _misaClass.MisaGuiHoaDon(paymentId, benhnhanGoi, objNguoiMua, ref eMessga, ref transactionId);
                    if (kt)
                    {
                        foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                        {
                            gridExRow.Delete();
                           
                        }
                        sotienthu = 0;
                        txttennguoimua.Clear();
                        txtSoTienHoaDon.Clear();
                        txtmadonvi.Clear();
                        txttendonvi.Clear();
                        txtmasothue.Clear();
                        txtemail.Clear();
                        txtdiachi.Clear();
                        txtstknguoimua.Clear();
                        //if (!string.IsNullOrEmpty(transactionID))
                        //{
                        //    string eMessge = "";
                        //    kt = _MisaInvoices.SaveFileInvoice(transactionID, true, ref eMessge);
                        //    if (kt)
                        //    {
                        //        StoredProcedure sp =
                        //            SPs.SpCapnhapHoadonLog(objPdfhoadon.fKey,
                        //                globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
                        //        sp.Execute();
                        //        HISLinkLog.Log(this.Name, globalVariables.UserName,
                        //            string.Format(
                        //                "Tải thành công hóa đơn: {0}, trạng thái: {1}, mẫu hóa đơn: {2}, kí hiệu: {3}",
                        //                objPdfhoadon.fKey, "", objPdfhoadon.pattern, objPdfhoadon.serial), action.Update, "UI");
                        //    }
                        //}
                    }
                    else
                    {
                        Utility.ShowMsg(eMessga);
                    }
                    LogText(eMessga, Color.DarkBlue);
                }
    
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
            }
            finally
            {
                cmdSendInvoices.Enabled = true;
            }
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
       
        public delegate void AddLog(string logText, Color sActionColor);

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
        
        public void LogText(string sLogText, Color sActionColor)
        {
            if (InvokeRequired)
            {
                Invoke(new AddLog(LogText), new object[] { sLogText, sActionColor });
            }
            else
            {
                AddAction(sLogText, sActionColor);
                //rtxtLogs.AppendText(sLogText);
                rtxtLogs.AppendText(_sNewline);
                //TextBoxTraceListener.SendMessage(_richTextBoxLog.Handle, TextBoxTraceListener.WM_VSCROLL, TextBoxTraceListener.SB_BOTTOM, 0);
            }
        }

        private void cmdCancelInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                cmdCancelInvoices.Enabled = false;
                string paymentId = "";
                string returnMessage = "";
               
                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    paymentId = !string.IsNullOrEmpty(paymentId)
                        ? paymentId + "," + Utility.sDbnull(gridExRow.Cells["Payment_Id"].Value, 0)
                        : Utility.sDbnull(gridExRow.Cells["Payment_Id"].Value, 0);
                }
                bool thanhcong = false;
                thanhcong = _misaClass.CancelInvoices(paymentId);
                if (thanhcong)
                {
                    Utility.ResetProgressBarJanus(ProgressBar, grdList.GetCheckedRows().Count(), true);
                    foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                    {
                        gridExRow.BeginEdit();
                        gridExRow.Cells["TRANG_THAI"].Value = 1;
                        gridExRow.EndEdit();
                        gridExRow.IsChecked = false;
                    }
                }
               

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdCancelInvoices.Enabled = true;
            }
        }

        private void cmdDownloadInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                cmdDownloadInvoices.Enabled = false;
                string eMessge = "";
                Utility.ResetProgressBarJanus(ProgressBar, grdList.GetCheckedRows().Count(), true);
                foreach (GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    string paymentId = "";
                    DataTable dt =
                        SPs.SpInvoiceGetPayment(Utility.sDbnull(gridExRow.Cells[TPayment.Columns.PaymentId].Value), -1,
                            1)
                            .GetDataSet()
                            .Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.AsEnumerable())
                        {
                            paymentId = !string.IsNullOrEmpty(paymentId)
                                ? paymentId + "," + Utility.sDbnull(row["Payment_Id"], 0)
                                : Utility.sDbnull(row["Payment_Id"], 0);
                        }
                    }
                    else
                    {
                        break;
                    }
                    bool kt = _misaClass.PdfSaveFile(paymentId);
                    gridExRow.IsChecked = false;
                    SetValue4Prg(ProgressBar, 1);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdDownloadInvoices.Enabled = true;
            }
        }
        decimal sotienthu = 0;
        private void grdList_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            //sotienthu = 0;
            try
            {
                if (e.Column.ActAsSelector)
                {
                    if (grdList.CurrentRow.IsChecked)
                    {
                        GridEXRow gridExRow = grdList.CurrentRow;

                        //var querysotienthu = from thanhtoan in grdList.GetCheckedRows()
                        //                     let y = Utility.DecimaltoDbnull(thanhtoan.Cells["SOTIEN"].Value)

                        //                     select y;
                        //sotienthu = Utility.DecimaltoDbnull(querysotienthu.Sum());


                        decimal sotiencheck = Utility.DecimaltoDbnull(gridExRow.Cells["SOTIEN"].Value, 0);
                        sotienthu = sotienthu + sotiencheck;
                    }
                    else
                    {
                        GridEXRow gridExRow = grdList.CurrentRow;
                        //var querysotienthu = from thanhtoan in grdList.GetCheckedRows()
                        //                     let y = Utility.DecimaltoDbnull(thanhtoan.Cells["SOTIEN"].Value)

                        //                     select y;
                        //sotienthu = Utility.DecimaltoDbnull(querysotienthu.Sum());
                        decimal sotiencheck = Utility.DecimaltoDbnull(gridExRow.Cells["SOTIEN"].Value, 0);
                        sotienthu = sotienthu - sotiencheck;
                    }
                  
                    //// tính toán số tiền xuất hóa đơn
                    //var querysotienthu = from thanhtoan in grdList.GetCheckedRows()
                    //                       let y = Utility.DecimaltoDbnull(thanhtoan.Cells["SOTIEN"].Value)
                    //                       select y;
                    //sotienthu = Utility.DecimaltoDbnull(querysotienthu.Sum()); 
                    if (sotienthu > 0)
                    txtSoTienThu.Text = txtSoTienHoaDon.Text = Utility.sDbnull(sotienthu);
                    else
                    {
                        txtSoTienThu.Text = txtSoTienHoaDon.Text = Utility.sDbnull(0);
                    }
                }
              

            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void txtSoTienHoaDon_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtSoTienHoaDon);
        }

        private void txtSoTienThu_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtSoTienThu);
        }

        private void grdList_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            // tính toán số tiền xuất hóa đơn
            var querysotienthu = from thanhtoan in grdList.GetCheckedRows()
                                 let y = Utility.DecimaltoDbnull(thanhtoan.Cells["SOTIEN"].Value)

                                 select y;
             sotienthu = Utility.DecimaltoDbnull(querysotienthu.Sum());
            txtSoTienThu.Text = txtSoTienHoaDon.Text = Utility.sDbnull(sotienthu);
        }
        private void PhanQuyenChucNang()
        {
            cmdSendInvoices.Visible = BusinessHelper.IsLoadMaPhanQuyen("HOADON_XUATTONG", false);
           
        }

        private void grdList_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
    }
}
