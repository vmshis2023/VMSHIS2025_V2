using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.Libs.AppUI;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_InGopBenhNhan : Form
    {
        private DataTable m_dtTimKiem;
        private string _sThamso = "ALL";
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        public delegate void AddLog(string logText, Color sActionColor);
        private const string _sNewline = "\r\n";
        public frm_InGopBenhNhan(string sThamSo)
        {
            _sThamso = sThamSo;
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtDenNgay.Value = dtTuNgay.Value = THU_VIEN_CHUNG.GetSysDateTime();
            txtLydo._OnShowData += txtLydo__OnShowData;
            txtLydo._OnSaveAs += txtLydo__OnSaveAs;
        }
        void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }
        void txtLydo__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }
        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdSave.Enabled = true;
            //btnCancel.Enabled = false;
        }
        /// <summary>
        /// Time consuming operations go here </br>
        /// i.e. Database operations,Reporting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        /// <summary>
        /// Notification is performed here to the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Here you play with the main UI thread
            prgBar.Value = e.ProgressPercentage;
          
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_InGopBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.S&&e.Control)cmdSave.PerformClick();
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
          //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        private void frm_InGopBenhNhan_Load(object sender, EventArgs e)
        {
            LoadInvoiceInfo();
            //LoadHoaDonDoNgoaiTruTheoCoSo();
           
        }
        private void TimKiem()
        {
            int NTNT = -1;
            if (radTatCa.Checked) NTNT = -1;
            if (radNgoaiTru.Checked) NTNT = 0;
            if (radNoiTru.Checked) NTNT = 1;
            var sp =
                SPs.HoaDonTimKiemHoaDonIngop(dtTuNgay.Value, dtDenNgay.Value, txtPatient_Code.Text, txtTenBN.Text, radDaHoaDon.Checked ? 1 : 0, NTNT,_sThamso);
            m_dtTimKiem=sp.GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtTimKiem, true, true, "sotien>0", "");
        }
        private DataTable dtCapPhat;
        private int HOADON_CAPPHAT_ID = -1;
        private void LoadHoaDonDoNgoaiTruTheoCoSo(int status)
        {
            try
            {
                string mahoadon = string.Empty;
                string kihieu = string.Empty;
                string ma_quyen = string.Empty;
                string serie = string.Empty;
                int HoaDon_Mau_ID = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonMau.Columns.IdHoadonMau));
                string error = string.Empty;

                HoadonMau objHoadonMau = HoadonMau.FetchByID(HoaDon_Mau_ID);
                if (objHoadonMau != null)
                {


                    var sp1 = SPs.SinhSysHoadonMau(HoaDon_Mau_ID, objHoadonMau.MauHoadon, objHoadonMau.KiHieu, objHoadonMau.MaQuyen, "BV01", 1);
                    DataTable histemp = sp1.GetDataSet().Tables[0];
                    if (histemp.Rows.Count > 0)
                    {
                        mahoadon = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MauHoadon]);
                        serie = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.SerieHientai]);
                        kihieu = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.KiHieu]);
                        ma_quyen = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MaQuyen]);

                    }

                }
                txtMauHD.Text = mahoadon;
                txtSerie.Text = serie;
                txtKiHieu.Text = kihieu;
                txtSerieDau.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, "");
                txtSerieCuoi.Text = Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieCuoi].Value, "");
                int sSerie = Utility.Int32Dbnull(serie);
                txtSerie.Text = Utility.sDbnull(sSerie <= 0 ? Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value, 0) : sSerie);
                txtSerie.MaxLength = Utility.DoTrim(txtSerieCuoi.Text).Length;
                txtSerie.Text = txtSerie.Text.PadLeft(Utility.sDbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.SerieDau].Value).Length, '0');
                txtMaQuyen.Text = ma_quyen;
                HOADON_CAPPHAT_ID =
                    Utility.Int32Dbnull(grdHoaDonCapPhat.CurrentRow.Cells[HoadonCapphat.Columns.IdCapphat].Value,
                        0);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.Message);
            }

        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)cmdTimKiem.PerformClick();
        }

        private int HoaDon_Mau_ID = -1;

        private HoadonLog objHoadonLog(decimal sotien)
        {
            try
            {
                HoadonLog obj = new HoadonLog();
                obj.MauHoadon = txtMauHD.Text;
                obj.KiHieu = txtKiHieu.Text;
                obj.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue("id_hoadon_mau"), -1);
                obj.MaQuyen = txtMaQuyen.Text;
                obj.Serie = txtSerie.Text;
                obj.TongTien = Utility.DecimaltoDbnull(sotien);
                obj.MaNhanvien = globalVariables.UserName;
                obj.MaLydo = "0";
                obj.NgayIn = globalVariables.SysDate;
                obj.TrangThai = 0;
                return obj;

            }
            catch (Exception)
            {
                return null;
            }
        }
        private HoadonLog objHoadonLog()
        {
            try
            {
                HoadonLog obj = new HoadonLog();
                obj.MauHoadon = txtMauHD.Text;
                obj.KiHieu = txtKiHieu.Text;
                obj.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue("id_hoadon_mau"), -1);
                obj.MaQuyen = txtMaQuyen.Text;
                obj.Serie = txtSerie.Text;

                obj.MaNhanvien = globalVariables.UserName;
                obj.MaLydo = "0";
                obj.NgayIn = globalVariables.SysDate;
                obj.TrangThai = 0;
                return obj;

            }
            catch (Exception)
            {
                return null;
            }
        }
        BackgroundWorker m_oWorker;
        /// <summary>
        /// hàm thưc hiện lưu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            UpdateHoaDon();
            //cmdSave.Enabled = false;
            //m_oWorker.RunWorkerAsync();
        }

        private void UpdateHoaDon()
        {
            rtxtLogs.Clear();
            string error = string.Empty;
            int idx = 1;
            int i = 0;
            Utility.ResetProgressBar(prgBar, grdList.GetCheckedRows().Count(), true);
            HoadonLog objLHoadonLog = objHoadonLog();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
            {
                i = i + 1;
                LoadHoaDonDoNgoaiTruTheoCoSo(1);
                decimal sotien = Utility.DecimaltoDbnull(gridExRow.Cells["SOTIEN"].Value);
                objLHoadonLog.IdThanhtoan = Utility.sDbnull(gridExRow.Cells["id_thanhtoan"].Value);
                objLHoadonLog.MaLuotkham = Utility.sDbnull(gridExRow.Cells["ma_luotkham"].Value);
                objLHoadonLog.IdBenhnhan = Utility.Int32Dbnull(gridExRow.Cells["id_benhnhan"].Value);
                objLHoadonLog.TongTien = Utility.DecimaltoDbnull(gridExRow.Cells["SOTIEN"].Value);
                objLHoadonLog.MaNhanvien = globalVariables.UserName;
                objLHoadonLog.TongTien = sotien;
                objLHoadonLog.MaLydo = "INGOP";
                // phải thêm trạng thái in gộp
                objLHoadonLog.InGop = 1;
                //objLHoadonLog = true;
                int status = Utility.Int32Dbnull(gridExRow.Cells["Kieu_ThanhToan"].Value);
                SqlQuery sqlQuery = new Select().From<HoadonLog>()
                    .Where(HoadonLog.Columns.IdThanhtoan)
                    .IsEqualTo(objLHoadonLog.IdThanhtoan)
                    .And(HoadonLog.Columns.TrangThai).IsEqualTo(0);
                if (sqlQuery.GetRecordCount() > 0) continue;
                int record = VNS.HIS.BusRule.Classes.KCB_HOADONDO.RedInsertHoaDonLog(objLHoadonLog, "BV01","", status);
                if (record > 0)
                {
                    int hoadonCapphatID = Utility.Int32Dbnull(objLHoadonLog.IdCapphat);
                    new Update(HoadonCapphat.Schema).Set(HoadonCapphat.Columns.SerieHientai)
                        .EqualTo(objLHoadonLog.Serie)
                        .Where(HoadonCapphat.Columns.IdHoadonMau).IsEqualTo(hoadonCapphatID).
                        Execute();
                    gridExRow.BeginEdit();
                   
                    gridExRow.Cells[HoadonLog.Columns.MauHoadon].Value = objLHoadonLog.MauHoadon;
                    gridExRow.Cells["MA_QUYEN"].Value = objLHoadonLog.MaQuyen;
                    gridExRow.Cells[HoadonLog.Columns.Serie].Value = objLHoadonLog.Serie;
                    gridExRow.Cells[HoadonLog.Columns.IdHdonLog].Value = objLHoadonLog.IdHdonLog;
                    gridExRow.Cells[HoadonLog.Columns.KiHieu].Value = objLHoadonLog.KiHieu;
                    gridExRow.Cells[HoadonLog.Columns.MaNhanvien].Value = globalVariables.UserName;
                    gridExRow.EndEdit();
                    grdList.UpdateData();
                    LogText(
                        string.Format("Ghép thành công bệnh nhân {0} vào số hóa đơn {1}", objLHoadonLog.MaLuotkham,
                            objLHoadonLog.Serie), Color.DarkBlue);
                    UIAction.SetValue4Prg(prgBar, 1);
                    Application.DoEvents();
                    gridExRow.IsChecked = false;
                    Thread.Sleep(10);
                }
                else
                {
                    LogText(
                       string.Format("Ghép không thành công bệnh nhân {0} vào số hóa đơn {1}", objLHoadonLog.MaLuotkham,
                           objLHoadonLog.Serie), Color.Red);
                }
            }
            LogText(string.Format("------------------------------------------------"), Color.DarkBlue);
            LogText(string.Format("Ghép thành công tổng cộng {0} bệnh nhân vào số hóa đơn {1}", i, objLHoadonLog.Serie), Color.DarkBlue);
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
        
        bool hasLoadedRedInvoice = false;
        private void LoadInvoiceInfo()
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) != "1") return;
                Utility.ResetMessageError(errorProvider1);
                if (globalVariables.UserName == null)
                    return;
                dtCapPhat = _THANHTOAN.LayHoaDonCapPhat(globalVariables.IsAdmin ? "ADMIN" : globalVariables.UserName);
                hasLoadedRedInvoice = true;
                if (dtCapPhat.Rows.Count <= 0)
                {
                    pHoaDonDo.Enabled = false;
                    Utility.SetMsgError(errorProvider1, pHoaDonDo, "Đã xử dụng hết hóa đơn được cấp ");
                }
                grdHoaDonCapPhat.DataSource = dtCapPhat;
                grdHoaDonCapPhat.AutoSizeColumns();
                var _HoadonLog =
                    new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.MaNhanvien).IsEqualTo(
                        globalVariables.UserName).OrderDesc(HoadonLog.Columns.IdHdonLog)
                        .And(HoadonLog.Columns.TrangThai).IsEqualTo(0)
                        .ExecuteSingle<HoadonLog>();
                if (_HoadonLog != null)
                {
                    Utility.GotoNewRowJanus(grdHoaDonCapPhat, HoadonCapphat.Columns.IdCapphat,
                        Utility.sDbnull(_HoadonLog.IdCapphat));
                }
                else
                {
                    grdHoaDonCapPhat.MoveFirst();
                }
            }
            catch
            {
                hasLoadedRedInvoice = false;
            }
        }
        private void cmdLoadHoaDon_Click(object sender, EventArgs e)
        {
            LoadInvoiceInfo();
        }

        private void grdHoaDonCapPhat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) != "1") return;
                if (grdHoaDonCapPhat.CurrentRow != null)
                {
                    if (grdHoaDonCapPhat.CurrentRow.RowType == RowType.Record)
                    {
                        LoadHoaDonDoNgoaiTruTheoCoSo(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
    }
}
