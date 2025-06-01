using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VNS.Libs;

using VMS.QMS.Class;
using VNS.Properties;

namespace VMS.QMS
{
    public partial class FrmShowScreen_Type1 : Form
    {
        QMSChucNang _qms = new QMSChucNang();
        Logger _log = null;
        public delegate void OnRefreshData(string infor, string current, string next, DataRow currentQMSRow, DataRow NextQMSRow, int totalQMS);
        public event OnRefreshData _OnRefreshData;

        public delegate void OnsaveLayout(string layout,int type);
        public event OnsaveLayout _OnsaveLayout;

        public int ThoiGianTuDongLay = 5000;
        public bool _closeme = false;
        int QMSType = 0;//0= QMS phòng khám;1= QMS phòng chức năng
        string layoutfile = Application.StartupPath + @"\QMSGrid1.layout";
        int numberofDisplay = 5;
        List<string> lstColname = new List<string>() { "STT", "Họ và tên", "Giới tính", "Năm sinh" };
        List<int> lstWidth = new List<int>() { 194, 701, 260, 309 };
        string layout = "STT,Họ và tên,Giới tính,Năm sinh@194, 701, 260, 309";
        public FrmShowScreen_Type1(bool UsingWS, int QMSType, int numberofDisplay, string layout)
        {

            InitializeComponent();
            this.layout = layout;
            this.numberofDisplay = numberofDisplay;
            this.QMSType = QMSType;
            InitLogs();
            _log = LogManager.GetLogger("QMSLog");
            if (UsingWS)
            {
                _qms.UsingWS = true;
            }
            else
            {
               // globalVariables.m_strPropertiesFolder = Directory.GetParent(Application.StartupPath) + @"\CauHinh_QMS\";
               
            }
            this.Resize += FrmShowScreen_Type1_Resize;
            this.Shown += FrmShowScreen_Type1_Shown;
            this.FormClosing += FrmShowScreen_Type1_FormClosing;
            CauHinh();
        }

        void FrmShowScreen_Type1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = !_closeme;
                _log.Trace("Free timer...");
                timer1.Enabled = false;
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
                
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());

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
        void FrmShowScreen_Type1_Resize(object sender, EventArgs e)
        {
            //ShowScreenOnMonitor2();
        }
        void LoadLayout()
        {
            try
            {
                lstColname = (from q in layout.Split('@')[0].Split(',')
                              select q).ToList<string>();
                lstWidth = (from q in layout.Split('@')[1].Split(',')
                            select Utility.Int32Dbnull(q, 100)).ToList<int>();
                for (int i = 0; i <= lstWidth.Count - 1; i++)
                {
                    grdList.RootTable.Columns[i].Width = lstWidth[i];
                    grdList.RootTable.Columns[i].Caption = lstColname[i];
                }

                //if (File.Exists(layoutfile))
                //    grdList.LoadLayoutFile(new FileStream(layoutfile, FileMode.Open, FileAccess.Read));
            }
            catch (Exception)
            {

            }
        }
        void FrmShowScreen_Type1_Shown(object sender, EventArgs e)
        {
            LoadLayout();
        }
        public void OnTimerEvent(object source, EventArgs e)
        {
            _log.Trace("OnTimerEvent...");
            LoadDanhsach(_qmsPrintProperties.MaPhongQMS, DateTime.Now, 100, _qmsPrintProperties.MaKhoaQMS);
           // ReShowScreenOnMonitor2();
        }
        public QMSPrintProperties _qmsPrintProperties;
        public void CauHinh()
        {
            _log.Trace("BEGIN CauHinh..");
            _qmsPrintProperties = PropertyLib.GetQMSPrintProperties(Application.StartupPath + @"\CauHinh_QMS");
            if (PropertyLib._QMSColorProperties == null) PropertyLib._QMSColorProperties = PropertyLib.GetQMSColorProperties();
            #region "Color"
            lbldanhsachnho.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorDanhsachnho);
            lblSoDangKham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorSodangkham);
            lbltenbacsy.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorTenbacsi);//Color.DarkBlue;
            lblphongkham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorphongkham);
            lbldanhsachchokham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColordanhsachchokham);
            lblTenDangKham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorTendangkham);
            grdList.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorGrid);
            grdList.RowFormatStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorGrid);
            grdList.SelectedFormatStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorGrid);

            lbltenbacsy.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorTenbacsi);
            lblphongkham.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorphongkham);
            lblSoDangKham.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorSodangkham);
            lbldanhsachchokham.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColordanhsachchokham);
            lblTenDangKham.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorTendangkham);
            grdList.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorGrid);
            grdList.RowFormatStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorGrid);
            grdList.SelectedFormatStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorGrid);
            lbldanhsachnho.BackColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._backColorDanhsachnho);

           // this.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties._backColor);
            #endregion

            pnlTop.Height = PropertyLib._QMSColorProperties.TopHeight;
            pnlLeft.Width = PropertyLib._QMSColorProperties.LeftWidth;
            pnlBottom.Height = PropertyLib._QMSColorProperties.BottomHeight;

            lblSoDangKham.Height = PropertyLib._QMSColorProperties.TopHeight1;
            lblTenDangKham.Height = PropertyLib._QMSColorProperties.TopHeight2;
            lbldanhsachchokham.Height = PropertyLib._QMSColorProperties.TopHeight3;


            pnlLogo.Height = PropertyLib._QMSColorProperties.LogoHeight;
            grdList.CardHeaders = PropertyLib._QMSColorProperties.VisibleColumnHeader;



            grdList.Font = new Font(grdList.Font.FontFamily, PropertyLib._QMSColorProperties.GridFontSize);
            lbltenbacsy.Font = new Font(lbltenbacsy.Font.FontFamily, PropertyLib._QMSColorProperties.DoctorFontSize);
            lblTenDangKham.Font = new Font(lblTenDangKham.Font.FontFamily, PropertyLib._QMSColorProperties.PatientFontSize);

            lbldanhsachchokham.Font = new Font(lbldanhsachchokham.Font.FontFamily, PropertyLib._QMSColorProperties.WaitFontSize);
            lblSoDangKham.Font = new Font(lblSoDangKham.Font.FontFamily, PropertyLib._QMSColorProperties.WaitFontSize);
            lbldanhsachnho.Font = new Font(lbldanhsachnho.Font.FontFamily, PropertyLib._QMSColorProperties.MissFontSize);

            lbldanhsachnho.Speed = PropertyLib._QMSColorProperties.speedNho;
            lblphongkham.Speed = PropertyLib._QMSColorProperties.speedPK;
            lbltenbacsy.Speed = PropertyLib._QMSColorProperties.speedBS;
            //Dùng tham số hệ thống
            timer1.Interval = ThoiGianTuDongLay;// PropertyLib._QMSColorProperties.ThoiGianTuDongLay;
            LoadThongTinKhoaPhong();
            _log.Trace("END CauHinh..");
            Application.DoEvents();
            //splitContainer3.Panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(_qmsPrintProperties._backColor);
        }
        private void LoadThongTinKhoaPhong()
        {
            lblphongkham.Text = "";
            lbltenbacsy.Text = "";
            lbldanhsachchokham.Text = PropertyLib._QMSColorProperties.TenDScho;
            lbltenbacsy.Text = string.Format("{0}{1}", _qmsPrintProperties.TenHienThi,_qmsPrintProperties.speedBS<=0?"": "                               ");
            lblphongkham.Text = string.Format("{0}{1}", _qmsPrintProperties.PhongHienThi,_qmsPrintProperties.speedPK<=0?"": "                               ");
           
        }
        [DllImport("user32")]
        public static extern long SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int y, int cx, int cy, int wFlagslong);
        const short SWP_NOSIZE = 0x0001;
        const short SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_SHOWWINDOW = 0x0040;
        const int SWP_HIDEWINDOW = 0x80;
        public void ReShowScreenOnMonitor2_()
        {
            try
            {
                _log.Trace("ReShowScreenOnMonitor2...");
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;

                if (query.Count() >= 2)
                {
                    IntPtr hWnd = this.Handle;
                    Thread.Sleep(100);
                    Rectangle rectMonitor = Screen.AllScreens[1].WorkingArea;
                    SetWindowPos(hWnd, 0,
                   rectMonitor.Left, rectMonitor.Top, rectMonitor.Width,
                   rectMonitor.Height, SWP_SHOWWINDOW);


                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        public void ShowScreenOnMonitor2_()
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
                    //Thread.Sleep(1000);
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
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
        private void FrmShowScreen_Type1_Load(object sender, EventArgs e)
        {
            _log.Trace("Load...");
            LoadThongTinKhoaPhong();
            _log.Trace("LoadThongTinKhoaPhong...");
            LoadDanhsach(_qmsPrintProperties.MaPhongQMS, DateTime.Now, 100, _qmsPrintProperties.MaKhoaQMS);
            _log.Trace("LoadDanhsach...");
            timer1.Enabled = true;// _qmsPrintProperties.TuDongLayThongTin;
            timer1.Start();
            timer1.Tick += new System.EventHandler(OnTimerEvent);
            //ShowScreenOnMonitor2();
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
        private void LoadDanhsach(string maphong, DateTime _ngaytao, int trangthai, string maKhoa)
        {
            string infor = "";
            string current = "";
            string next = "";
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
                    mDtDanhSachChoKhamNext = mDtDanhSachChoKhamNext.AsEnumerable().Take(arrDANGCHO.Length > numberofDisplay ? numberofDisplay : arrDANGCHO.Length).CopyToDataTable();
                }

                var mDtDanhSachChoKhamPass = new DataTable();
                if (arrNHO.Length != 0)
                {
                    mDtDanhSachChoKhamPass = arrNHO.CopyToDataTable();
                    mDtDanhSachChoKhamPass = mDtDanhSachChoKhamPass.AsEnumerable().Take(arrNHO.Length > numberofDisplay ? numberofDisplay : arrNHO.Length).CopyToDataTable();
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
                            string _text = string.Format(Utility.Int32Dbnull(row["Uu_tien"], 0) == 0 ? _qmsPrintProperties.XQActSothuong : _qmsPrintProperties.XQActSoUutien + " {0}", sSoKham);
                            if (_text != lblSoDangKham.Text)
                                lblSoDangKham.Text = _text;
                            lblTenDangKham.Text = string.Format("{0} - {1}",sSoKham, row["TEN_BENHNHAN"]);
                            lblTenDangKham.ForeColor = Utility.Int32Dbnull(row["Uu_tien"], 0) == 0 ? System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorTendangkham) : System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties.ForeColorUuTien);
                            lblSoDangKham.ForeColor = Utility.Int32Dbnull(row["Uu_tien"], 0) == 0 ? System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorSodangkham) : System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties.ForeColorUuTien);
                            if (Utility.sDbnull(row["trang_thai"]) == "1")
                                _qms.QmsPK_CapnhatTrangthai(Utility.Int64Dbnull(currentQMSRow["id_kham"], -1), Utility.Int64Dbnull(currentQMSRow["id"], -1), 2, QMSType);
                            infor = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQCurrentPatient, row["TEN_BENHNHAN"].ToString(), sSoKham);
                            current = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQCurrentPatient, row["TEN_BENHNHAN"].ToString(), sSoKham);
                            next = "";
                        }
                        DataRow[] _temp=mDtDanhSachChoKhamNext.AsEnumerable().Take(2).ToArray<DataRow>();
                        if (_temp.Count() > 0)
                            rowNext = _temp.CopyToDataTable();
                        if (rowNext.Rows.Count > 1)
                        {
                            NextQMSRow = found ? rowNext.Rows[0] : rowNext.Rows[1];//found= có bản tin đang ở trạng thái chờ thực hiện
                            infor += string.Format(" - {0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, found ? rowNext.Rows[0]["TEN_BENHNHAN"].ToString() : rowNext.Rows[1]["TEN_BENHNHAN"].ToString(), found ? rowNext.Rows[0]["So_Kham"].ToString() : rowNext.Rows[1]["So_Kham"].ToString());
                            next = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, found ? rowNext.Rows[0]["TEN_BENHNHAN"].ToString() : rowNext.Rows[1]["TEN_BENHNHAN"].ToString(), found ? rowNext.Rows[0]["So_Kham"].ToString() : rowNext.Rows[1]["So_Kham"].ToString());
                        }
                        else if (rowNext.Rows.Count == 1)
                        {
                            if (found)
                            {
                                
                                if (rowNext.Rows[0]["ma_luotkham"].ToString() != rowDangkham.Rows[0]["ma_luotkham"].ToString())
                                {
                                    NextQMSRow = rowNext.Rows[0];
                                    infor += string.Format(" - {0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, rowNext.Rows[0]["TEN_BENHNHAN"].ToString(), rowNext.Rows[0]["So_Kham"].ToString());
                                    next = string.Format("{0}{1} STT: {2}", _qmsPrintProperties.XQNextPatient, rowNext.Rows[0]["TEN_BENHNHAN"].ToString(), rowNext.Rows[0]["So_Kham"].ToString());
                                }
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
                        lblSoDangKham.Text = _qmsPrintProperties.XQActSothuong;
                        lblTenDangKham.Text = string.Format("{0}", _qmsPrintProperties.Hetso);
                        lblTenDangKham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorTendangkham);
                        lblSoDangKham.ForeColor = System.Drawing.ColorTranslator.FromHtml(PropertyLib._QMSColorProperties._ForeColorSodangkham);
                    }
                    grdList.DataSource = mDtDanhSachChoKhamNext;
                   // lbldanhsachchokham.ForeColor = Color.DarkBlue;
                }
                else
                {
                    lblSoDangKham.Text = _qmsPrintProperties.XQActSothuong;
                    lblTenDangKham.Text = string.Format("{0}", _qmsPrintProperties.Hetso);
                    grdList.DataSource = null;

                }
                if (nhacnho.Length <= 0)
                {
                    lbldanhsachnho.Visible =label1.Visible= false;
                }
                else
                {
                    lbldanhsachnho.Visible = label1.Visible = true;
                    label1.AutoSize = true;
                    label1.Font = lbldanhsachnho.Font;
                    label1.Text = nhacnho;
                    if (label1.Width + 10 >= pnlBottom.Width)
                        lbldanhsachnho.AutoSize = true;
                    else
                        lbldanhsachnho.AutoSize = false;
                     Utility.SetMsg(lbldanhsachnho,  nhacnho, true);
                }
            }
            catch (Exception ex)
            {
                //Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                if (_OnRefreshData != null) _OnRefreshData(infor, current, next, currentQMSRow, NextQMSRow, _dtDanhsach.Rows.Count);
            }
        }

        private void mnuSaveLayout_Click(object sender, EventArgs e)
        {
            try
            {
                //grdList.SaveLayoutFile(new FileStream(layoutfile, FileMode.Create, FileAccess.Write));
                //Utility.ShowMsg(string.Format("Đã lưu Layout tại đường dẫn {0}. Có thể cập nhật cho các màn hình khác bằng phần mềm quản trị hệ thống", layoutfile));
                List<string> _lstCaption = new List<string>();
                List<string> _lstWidth = new List<string>();
                for (int i = 0; i <= lstWidth.Count - 1; i++)
                {
                    _lstCaption.Add(grdList.RootTable.Columns[i].Caption);
                    _lstWidth.Add(grdList.RootTable.Columns[i].Width.ToString());
                }
                string layout = String.Format("{0}@{1}", string.Join(",", _lstCaption.ToArray<string>(), string.Join(",", _lstWidth.ToArray<string>())));
                if (_OnsaveLayout != null) _OnsaveLayout(layout,1);
            }
            catch (Exception ex)
            {

            }
        }

        private void mnudeleteLayout_Click(object sender, EventArgs e)
        {
            Utility.Try2DelFile(layoutfile);
        }


    }

}
