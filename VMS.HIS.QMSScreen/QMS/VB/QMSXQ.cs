using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VNS.Libs;

using VMS.QMS.Class;
using VNS.Properties;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace VMS.QMS
{
    public partial class QMSXQ : Form
    {
        QMSChucNang _qms = new QMSChucNang();
        string errMsg = "";
        Logger _log = null;
        public delegate void OnRefreshData(string infor, string current, string next, DataRow currentQMSRow, DataRow NextQMSRow, int totalQMS);
        public event OnRefreshData _OnRefreshData;
        int QMSType = 0;//0= QMS phòng khám;1= QMS phòng chức năng
        public bool _closeme = false;
        public QMSXQ(bool UsingWS, byte[] SysLogo, int QMSType)
        {
            InitializeComponent();
            this.QMSType = QMSType;
            InitLogs();
            _log = LogManager.GetLogger("QMSLog");
            if (UsingWS)
            {
                _qms.UsingWS=true;
            }
            else
            {
                globalVariables.m_strPropertiesFolder = Directory.GetParent(Application.StartupPath) + @"\CauHinh_QMS\";
            }
            this.Resize += QMSXQ_Resize;
            this.Shown += QMSXQ_Shown;
            this.FormClosing += QMSXQ_FormClosing;
            CauHinh();
            try
            {
                if (SysLogo != null)
                {
                    //globalVariables.SysLogo = SysLogo;
                    //ImageConverter converter = new ImageConverter();
                    //picHosIcon.Image = (System.Drawing.Image)converter.ConvertFrom(globalVariables.SysLogo);
                }
            }
            catch (Exception)
            {
                
            }
        }
        void InitLogs()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName =
                    "${basedir}/Mylogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
                config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch
            {
            }
        }
        void QMSXQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_closeme;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="State">0 = Advertisement;1 = QMS Screen</param>
        public void SetState(int State)
        {
            if (State == 0)
            {
                pnlQMS.SendToBack();
                Application.DoEvents();
            }
            else
            {
                pnlQMS.BringToFront();
                Application.DoEvents();
            }
        }
        void QMSXQ_Resize(object sender, EventArgs e)
        {
           // ShowScreenOnMonitor2();
        }

        void QMSXQ_Shown(object sender, EventArgs e)
        {
           // ShowScreenOnMonitor2();
        }
        public void OnTimerEvent(object source, EventArgs e)
        {
           
        }
        public QMSPrintProperties _qmsPrintProperties;
        public int ThoiGianTuDongLay = 5000;
        public void CauHinh()
        {
            _qmsPrintProperties = PropertyLib.GetQMSPrintProperties(Application.StartupPath + @"\CauHinh_QMS");
            if (PropertyLib._QMSColorProperties == null) PropertyLib._QMSColorProperties = PropertyLib.GetQMSColorProperties();
            //lblHospitalName.Text = globalVariables.Branch_Name;
            //lblHospitalName.Width = _qmsPrintProperties.HospitalWidth;
            lblProductName.Text = _qmsPrintProperties.ProductName;
            lblAct.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActForeColor);
            lblAct.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActBackColor);
            lblAct.Height = _qmsPrintProperties.XQActHeight;
            lblAct.Font = _qmsPrintProperties.XQActFontChu;
            lblAct.Text = _qmsPrintProperties.XQActTitle;

            lblSTT.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQSTTForeColor);
            lblSTT.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQSTTBackColor);
            lblSTT.Font = _qmsPrintProperties.XQSTTFontChu;

            lblPatientInfor.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientForeColor);
            lblPatientInfor.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientBackColor);
            lblPatientInfor.Font = _qmsPrintProperties.XQPatientFontChu;
            lblPatientInfor.Height = _qmsPrintProperties.XQPatientHeight;

            lblNext.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientNextForeColor);
            lblNext.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientNextBackColor);
            lblNext.Font = _qmsPrintProperties.XQPatientNextFontChu;
            lblNext.Height = _qmsPrintProperties.XQPatientNextHeight;

            //lblHospitalName.Text = string.Format("{0} {1}", globalVariables.Branch_Name, _qmsPrintProperties.TenHienThi);
            
            pnlTop.Height = _qmsPrintProperties.XQTitleHeight;
            //picHosIcon.Size = new Size(_qmsPrintProperties.XQTitleHeight, _qmsPrintProperties.XQTitleHeight);
            //lblHospitalName.Font = lblProductName.Font = _qmsPrintProperties.XQTitleFontChu;
            timer1.Interval = ThoiGianTuDongLay;// _qmsPrintProperties.ThoiGianTuDongLay;
            //splitContainer3.Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties._backColor);
        }
       
        [DllImport("user32")]
        public static extern long SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int y, int cx, int cy, int wFlagslong);
        const short SWP_NOSIZE = 0x0001;
        const short SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_SHOWWINDOW = 0x0040;
        public void ShowScreenOnMonitor2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                if (query.Count() >= 2)
                {
                    IntPtr hWnd = this.Handle;
                    Thread.Sleep(1000);
                    Rectangle rectMonitor = Screen.AllScreens[1].WorkingArea;
                    SetWindowPos(hWnd, 0,
                   rectMonitor.Left, rectMonitor.Top, rectMonitor.Width,
                   rectMonitor.Height, SWP_SHOWWINDOW);
                    //this.FormBorderStyle = FormBorderStyle.None;
                    //this.Left = sc[1].Bounds.Width;
                    //this.Top = sc[1].Bounds.Height;
                    //this.StartPosition = FormStartPosition.CenterScreen;
                    //this.Location = sc[1].Bounds.Location;
                    //var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                    //this.Location = p;
                    //this.WindowState = FormWindowState.Maximized;
                    //this.Show();

                }
            }
            catch (Exception)
            {
            }
        }
        private void QMSXQ_Load(object sender, EventArgs e)
        {
           
            //LoadDanhsach(_qmsPrintProperties.MaPhongQMS, DateTime.Now, 100, _qmsPrintProperties.MaKhoaQMS);
            if (ServiceExists(_qms.QMSSer.Url, false, ref errMsg))
            {

                timer1.Enabled = true;// _qmsPrintProperties.TuDongLayThongTin;
                timer1.Start();
                timer1.Tick += timer1_Tick;
            }
            //ShowScreenOnMonitor2();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            LoadDanhsach(_qmsPrintProperties.MaPhongQMS, DateTime.Now, 100, _qmsPrintProperties.MaKhoaQMS);
            Application.DoEvents();
        }
        public static bool ServiceExists(string url, bool throwExceptions, ref string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException("errorMessage");
            try
            {
                errorMessage = string.Empty;
                // try accessing the web service directly via it's URL
                var request =
                    WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 3000;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        errorMessage = "Error locating web service";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage =
                    string.Format("Error testing connection to web service at " +
                                  "\"{0}\":\r\n{1}", url, ex);
                if (throwExceptions)
                    throw new Exception(errorMessage, ex);
                return false;
            }
        }
        public void RefreshQMS()
        {
            try
            {
                timer1.Stop();
                _log.Trace("Timer stopped...");
                _log.Trace("QMS is refreshing");
                LoadDanhsach(_qmsPrintProperties.MaPhongQMS, DateTime.Now, 100, _qmsPrintProperties.MaKhoaQMS);
                timer1.Start();
                _log.Trace("Timer started....");
            }
            catch (Exception)
            {


            }
        }
        DataTable _dtDanhsach = new DataTable();
        private void LoadDanhsach(string maphong, DateTime _ngaytao, int trangthai, string maKhoa )
        {
            string infor = "";
            string current = "";
            string inforNext = "";
            bool found = true;
            DataRow currentQMSRow = null;
            DataRow NextQMSRow = null;
            try
            {
                if (QMSType == 0)
                    _dtDanhsach = _qms.QmsPK_GetData(maphong, _ngaytao, trangthai, maKhoa);
                else
                _dtDanhsach = _qms.GetListQmSbyMaPhong(maphong, _qmsPrintProperties.DisplayGroup, _ngaytao, trangthai, maKhoa);
                if (_dtDanhsach == null) return;
                DataRow[] arrOK = _dtDanhsach.Select("trang_thai=3");//Đã xong
                DataRow[] arrDangThien = _dtDanhsach.Select("trang_thai=2", "So_Kham");//Đang thực hiện
                DataRow[] arrDANGCHO = _dtDanhsach.Select("trang_thai=1", "So_Kham");//Mới tạo
                DataRow[] arrNHO = _dtDanhsach.Select("trang_thai=0");//Nhỡ
                Utility.AddColumToDataTable(ref _dtDanhsach, "IsNo1", typeof(int));
                Utility.AddColumToDataTable(ref _dtDanhsach, "Uu_tien", typeof(int));
                for (int j = 0; j < _dtDanhsach.Rows.Count; j++)
                {
                    _dtDanhsach.Rows[j]["Uu_tien"] = 0;
                    if (j == 0)
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 1;
                    }
                    else
                    {
                        _dtDanhsach.Rows[j]["IsNo1"] = 0;
                    }
                }

                var mDtDanhSachChoKhamNext = new DataTable();
                if (arrDANGCHO.Length != 0)
                {
                    mDtDanhSachChoKhamNext = arrDANGCHO.CopyToDataTable();
                    mDtDanhSachChoKhamNext = mDtDanhSachChoKhamNext.AsEnumerable().Take(5).CopyToDataTable();
                }

                var mDtDanhSachChoKhamPass = new DataTable();
                if (arrNHO.Length != 0)
                {
                    mDtDanhSachChoKhamPass = arrNHO.CopyToDataTable();
                    mDtDanhSachChoKhamPass = mDtDanhSachChoKhamPass.AsEnumerable().Take(5).CopyToDataTable();
                }
                string nhacnho = "";
                if (mDtDanhSachChoKhamPass.Rows.Count > 0)
                {
                    nhacnho = "Danh sách bệnh nhân nhỡ: ";
                    foreach (var row in mDtDanhSachChoKhamPass.AsEnumerable())
                    {
                        nhacnho = nhacnho +
                                  string.Format("{0} - {1};  ", Utility.FormatNumberToString(Utility.Int32Dbnull(row["So_Kham"]), "00"), row["TEN_BENHNHAN"].ToString());
                    }
                }

                if (mDtDanhSachChoKhamNext.Rows.Count > 0 || arrDangThien.Length > 0)
                {
                    DataTable rowDangkham = mDtDanhSachChoKhamNext.Clone();
                    DataTable rowNext = mDtDanhSachChoKhamNext.Clone();
                    if (arrDangThien.Length <= 0)
                    {
                        found = false;
                        rowDangkham = mDtDanhSachChoKhamNext.AsEnumerable().Take(1).CopyToDataTable();
                    }
                    else
                    {
                        found = true;
                        rowDangkham = arrDangThien.CopyToDataTable();
                    }
                    if (rowDangkham.Rows.Count > 0)
                    {
                        currentQMSRow = rowDangkham.Rows[0];
                        foreach (DataRow row in rowDangkham.AsEnumerable())
                        {
                            string sSoKham = "00";
                            if (Utility.Int32Dbnull(Utility.Int32Dbnull(row["So_Kham"])) < 10)
                            {
                                sSoKham = Utility.FormatNumberToString(Utility.Int32Dbnull(row["So_Kham"]), "00");
                            }
                            else
                            {
                                sSoKham = Utility.sDbnull(row["So_Kham"]);
                            }
                            if (Utility.Int32Dbnull(row["Uu_tien"], 0) == 0)
                            {
                                lblAct.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActForeColor);
                                lblAct.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActBackColor);
                                lblAct.Text = string.Format("{0} {1}", _qmsPrintProperties.XQActSothuong,sSoKham);

                                lblSTT.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQSTTForeColor);
                                lblSTT.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQSTTBackColor);

                                lblPatientInfor.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientForeColor);
                                lblPatientInfor.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQPatientBackColor);

                            }
                            else
                            {
                                lblAct.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActForeColorUutien);
                                lblAct.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActBackColorUutien);
                                lblAct.Text = string.Format("{0} {1}", _qmsPrintProperties.XQActSoUutien, sSoKham);

                                lblSTT.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActForeColorUutien);
                                lblSTT.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActBackColorUutien);

                                lblPatientInfor.ForeColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActForeColorUutien);
                                lblPatientInfor.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties.XQActBackColorUutien);

                            }
                            lblSTT.Text = sSoKham;
                            lblPatientInfor.Text = string.Format("{0} {1} tuổi", row["TEN_BENHNHAN"].ToString(), row["tuoi"].ToString());
                            if (Utility.sDbnull(row["trang_thai"]) == "1")
                                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 2, QMSType);
                            infor = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQCurrentPatient, row["TEN_BENHNHAN"].ToString(), sSoKham);
                            current = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQCurrentPatient, row["TEN_BENHNHAN"].ToString(), sSoKham);
                            inforNext = "";
                        }
                        DataRow[] drNext= mDtDanhSachChoKhamNext.AsEnumerable().Take(2).ToArray<DataRow>();
                        if (drNext.Length > 0)
                            rowNext = drNext.CopyToDataTable();
                        if (rowNext.Rows.Count > 1)
                        {
                            NextQMSRow = found ? rowNext.Rows[0] : rowNext.Rows[1];//found= có bản tin đang ở trạng thái chờ thực hiện
                            infor += string.Format(" - {0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, found ? rowNext.Rows[0]["TEN_BENHNHAN"].ToString() : rowNext.Rows[1]["TEN_BENHNHAN"].ToString(), found ? rowNext.Rows[0]["So_Kham"].ToString() : rowNext.Rows[1]["So_Kham"].ToString());
                            inforNext = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, found ? rowNext.Rows[0]["TEN_BENHNHAN"].ToString() : rowNext.Rows[1]["TEN_BENHNHAN"].ToString(), found ? rowNext.Rows[0]["So_Kham"].ToString() : rowNext.Rows[1]["So_Kham"].ToString());
                        }
                        else if (rowNext.Rows.Count == 1)
                        {
                            if (found)
                                if (rowNext.Rows[0]["ma_luotkham"].ToString() != rowDangkham.Rows[0]["ma_luotkham"].ToString())
                                {
                                    NextQMSRow = rowNext.Rows[0];
                                    infor += string.Format(" - {0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, rowNext.Rows[0]["TEN_BENHNHAN"].ToString(), rowNext.Rows[0]["So_Kham"].ToString());
                                    inforNext = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, rowNext.Rows[0]["TEN_BENHNHAN"].ToString(), rowNext.Rows[0]["So_Kham"].ToString());
                                }
                        }
                        if (arrDangThien.Length <= 0)
                            mDtDanhSachChoKhamNext.Rows.RemoveAt(0);
                        //else
                        //    foreach (DataRow dr in arrDangThien.CopyToDataTable().Rows)
                        //        mDtDanhSachChoKhamNext.ImportRow(dr);

                    }
                    else
                    {
                        lblSTT.Text = "";
                        lblPatientInfor.Text = string.Format("{0}", "");
                    }
                }
                else
                {
                    lblSTT.Text = "";
                    lblPatientInfor.Text = string.Format("{0}", "");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                lblNext.Text = inforNext;
                if (_OnRefreshData != null) _OnRefreshData(infor, current, inforNext, currentQMSRow, NextQMSRow, _dtDanhsach.Rows.Count);
            }
        }
    }
    
}
