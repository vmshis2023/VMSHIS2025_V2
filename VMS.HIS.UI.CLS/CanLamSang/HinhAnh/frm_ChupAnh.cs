using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DirectX.Capture;
using DShowNET;

using NLog.Config;
using NLog;
using NLog.Targets;

using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using VNS.HIS.UI.HinhAnh;
using VNS.Properties;
using VNS.Libs;
using Touchless.Vision.Camera;
using System.Threading;

namespace VNS.HIS.UI.Forms.HinhAnh
{
    public partial class frm_ChupAnh : Form
    {
        public string RML = "-1";
        public int AssginDetail_ID = -1;
        public DataTable m_dthinhanh;
        public bool iscancel = true;
         Logger captureLog=null;
        public string filePrefix="";
        bool AutoRefresh = Util.getAutoRefresh() == "1";
        int Time2Refresh =Convert.ToInt32( Util.getTime2Refresh());
        int extrawidth = Convert.ToInt32(Util.getExtraWidth());
        public string VideoType = Util.getVideoType();//0=DirectX;1=Avicap32.dll
        public List<string> lstImgs = new List<string>();
        public frm_ChupAnh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitLogs();
            captureLog = LogManager.GetLogger("CaptureLogs");
            cboStandards.SelectedIndexChanged += cboStandards_SelectedIndexChanged;
            cboframerate.SelectedIndexChanged += cboframerate_SelectedIndexChanged;
            cboFrameSize.SelectedIndexChanged += cboFrameSize_SelectedIndexChanged;
            cboDevices.SelectedIndexChanged += cboDevices_SelectedIndexChanged;
            cboVideoSource.SelectedIndexChanged += cboVideoSource_SelectedIndexChanged;
            cmdCapture.Click += cmdCapture_Click;
            picHienThi.Click += picHienThi_Click;
          //  btnXoaHA.Click += btnXoaHA_Click;
            this.FormClosing += Form1_FormClosing;
            this.FormClosed += Form1_FormClosed;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            chkBatChupLenStartVideo.Checked =true;
            this.SizeChanged += frm_ChupAnh_SizeChanged;
            txtWidth.LostFocus += txtWidth_LostFocus;
            txtHeight.LostFocus += txtHeight_LostFocus;
            trbBrightness.Value = PropertyLib._HinhAnhProperties.Brightness;
            trbContrast.Value = PropertyLib._HinhAnhProperties.Contrast;
            chkSharpen.Checked = PropertyLib._HinhAnhProperties.Sharpen;


            //Webcam Events
            btnStart.Click+=btnStart_Click;
            btnConfig.Click+=btnConfig_Click;
            btnStop.Click+=btnStop_Click;
            btnSave.Click+=btnSave_Click;
            
            captureLog.Trace("InitializeComponent OK");
            timer1.Enabled = AutoRefresh;
            timer1.Interval = Time2Refresh;
            if (PropertyLib._HinhAnhProperties.AllowCaptureFormSize)
                this.Size = new Size(Convert.ToInt32(PropertyLib._HinhAnhProperties.CaptureFormSize.Split(',')[0]), Convert.ToInt32(PropertyLib._HinhAnhProperties.CaptureFormSize.Split(',')[1]));
            if (PropertyLib._HinhAnhProperties.AllowCaptureFormSize) this.WindowState = FormWindowState.Normal;
            pnlCapture.Refresh();
            Application.DoEvents();

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
        #region Aforge.net
        AForge.Video.DirectShow.FilterInfoCollection videoDevices = null;
        AForge.Video.DirectShow.VideoCaptureDevice videoSource = null;
        void InitAforge()
        {
            this.Text += " - Afge";
            cboDevices.Items.Clear();
            videoDevices = videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            for (int i = 0; i <= videoDevices.Count-1; i++)
            {
                cboDevices.Items.Add(videoDevices[i].Name);
            }
            cboDevices.Text = PropertyLib._HinhAnhProperties.devicename;
            cboFrameSize.Text = PropertyLib._HinhAnhProperties.FrameSize;
            AllowSelectedIndexChanged = true;
            AutoReSize();
            StartAforge();
        }
        #endregion
        #region "Avicap32.dll"
        #region Variables
        public string F_ID;
        private int hHwnd;
        private const short HWND_BOTTOM = 1;
        private int iDevice = 0;
        private BindingManagerBase ManagerPosition;
        public string sFilter;
        private const short SWP_NOMOVE = 2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 4;
        private const short WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        #endregion
        #region API
        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool capGetDriverDescriptionA(short wDriver, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);
        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool DestroyWindow(int hndw);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        #endregion
        private void LoadDeviceList()
        {
            bool flag = true;
            string lpszName = Strings.Space(100);
            string lpszVer = Strings.Space(100);
            int num = 0;
            do
            {
                if (capGetDriverDescriptionA((short)num, ref lpszName, 100, ref lpszVer, 100))
                {
                    cboDevices.Items.Add(lpszName.Trim());
                }
                num++;
            }
            while (flag && num <= 10);
        }
        private void OpenPreviewWindow()
        {
            string lpszWindowName = this.iDevice.ToString();
            Int16 height = Convert.ToInt16(this.pnlVideo.Height);
            int width = this.pnlVideo.Width;
            this.hHwnd = capCreateCaptureWindowA(ref lpszWindowName, 0x50000000, 0, 0, this.pnlVideo.Width,(short) this.pnlVideo.Height, this.pnlVideo.Handle.ToInt32(), 0);
            if (SendMessage(this.hHwnd, 0x40a, this.iDevice, 0) > 0)
            {
                SendMessage(this.hHwnd, 0x435, -1, 0);
                SendMessage(this.hHwnd, 0x434, 0x42, 0);
                SendMessage(this.hHwnd, 0x432, -1, 0);
                SetWindowPos(this.hHwnd, 1, 0, 0, this.pnlVideo.Width, this.pnlVideo.Height, 6);
            }
            else
            {
                DestroyWindow(this.hHwnd);
            }
        }


        private void ClosePreviewWindow()
        {
            try
            {
                SendMessage(this.hHwnd, 0x40b, this.iDevice, 0);
                DestroyWindow(this.hHwnd);
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
            }
        }
        #endregion
        void cmdRestart_Click(object sender, EventArgs e)
        {
            cmdBeginCapture.BringToFront();
        }

        void txtHeight_LostFocus(object sender, EventArgs e)
        {
            SaveSize(); 
        }

        void txtWidth_LostFocus(object sender, EventArgs e)
        {
            SaveSize();
        }

        void picHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Utility.sDbnull(picHienThi.Tag, "")))
                    System.Diagnostics.Process.Start(picHienThi.Tag.ToString());
            }
            catch
            {
            }
        }

        void frm_ChupAnh_SizeChanged(object sender, EventArgs e)
        {

            AutoReSize();
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.F9)
                {
                    cmdBeginCapture.PerformClick();
                    return;
                }
               
                if (e.KeyCode == Keys.F10)
                {
                    cmdCapture.PerformClick();
                    return;
                }
                if (e.Control && e.KeyCode == Keys.S)
                {
                    cmdLuuLai.PerformClick();
                }
                if (e.KeyCode == Keys.F5)
                    AutoLoadFrameInfor();
                if (e.KeyCode == Keys.F8)
                {
                    VideoType = Util.getVideoType();
                    AllowSelectedIndexChanged = false;
                    cboDevices.Items.Clear();
                    if (VideoType == "0")
                        Init();
                    else if (VideoType == "1")
                        InitAvicap32();
                    else
                    {
                        cboDevices.Items.Clear();
                        for (int i = 0; i <= 1; i++)
                        {
                            cboDevices.Items.Add(videoDevices[i].Name);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
        }
        void SetCaptureProperties()
        {
            if (capture != null)
            {
                
                if (cboStandards.Text.TrimEnd().TrimStart() != "" && cboStandards.SelectedIndex >= 0)
                {
                    AnalogVideoStandard a = (AnalogVideoStandard)Enum.Parse(typeof(AnalogVideoStandard), cboStandards.Text);
                    capture.dxUtils.VideoStandard = a;
                }
                string[] s = cboframerate.Text.Split(' ');
                capture.FrameRate = double.Parse(s[0]);
                s = cboFrameSize.Text.Split('x');
                Size size = new Size(int.Parse(s[0]), int.Parse(s[1]));
                capture.FrameSize = size;
                // Disable preview to avoid additional flashes (optional)
                bool preview = (capture.PreviewWindow != null);
                capture.PreviewWindow = null;
                // Restore previous preview setting
                capture.PreviewWindow = (preview ? pnlVideo : null);
                AutoReSize();
            }
        }
        void Init()
        {
            this.Text += " - DirectX";
            if (timer1.Enabled)
                timer1.Start();
            try
            {
                InitCaptureItems();
                captureLog.Trace("Auto select device");
                cboDevices_SelectedIndexChanged(cboDevices, new EventArgs());
                captureLog.Trace("Autoset Preview mode");
                SetCaptureProperties();
                AutoPreview();
            }
            catch (Exception)
            {

                //throw;
            }
            finally
            {
                AutoReSize();
                modifycommand();
            }
            
        }
        
        void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                pnlWC.SendToBack();
                AutoLoadFrameInfor();
                if (VideoType == "0")
                    Init();
                else if (VideoType == "1")
                    InitWC();
                else if (VideoType == "15011981")
                    InitAforge();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Size = new Size(this.Size.Width+1, this.Size.Height-1);
            }
           
        }
        void InitAvicap32()
        {
            AllowSelectedIndexChanged = false;
            LoadDeviceList();
            cboFrameSize.Text = PropertyLib._HinhAnhProperties.FrameSize;
            this.Text += " - Avicap32";
            if (PropertyLib._HinhAnhProperties.deviceIdx >= 0)
                cboDevices.SelectedIndex = PropertyLib._HinhAnhProperties.deviceIdx;
            if (this.cboDevices.Items.Count > 0)
            {
                this.cboDevices.SelectedIndex = 0;
            }
            else
            {
                Utility.ShowMsg("No Capture Device"); 
            }
            if (cboDevices.Items.Count > 0)
            {
                AllowSelectedIndexChanged = true;
                this.iDevice = cboDevices.SelectedIndex;
                this.OpenPreviewWindow();
            }
            AutoReSize();
        }
        public List<string> lstFrameInfor = new List<string>();
        void AutoLoadFrameInfor()
        {
            cboFrameSize.Items.Clear();
            cboframerate.Items.Clear();
            if (lstFrameInfor.Count <= 0)
                lstFrameInfor = Util.GetFrameInfor();
           // List<string> lstFrameSize = lstFrameInfor[0].Split(';').ToList<string>();
           // List<string> lstFrameRate = lstFrameInfor[1].Split(';').ToList<string>();
            
                cboFrameSize.Items.AddRange(lstFrameInfor[0].Split(';'));
            cboframerate.Items.AddRange(lstFrameInfor[1].Split(';'));
        }
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitCaptureTest();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            PropertyLib._HinhAnhProperties.Brightness = trbBrightness.Value;
            PropertyLib._HinhAnhProperties.Contrast = trbContrast.Value;
            PropertyLib._HinhAnhProperties.Sharpen = chkSharpen.Checked;
            PropertyLib._HinhAnhProperties.CaptureFormSize = string.Format("{0},{1}", this.Size.Width.ToString(), this.Size.Height.ToString());
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
            if (VideoType == "0")
                ExitCaptureTest();
            else if (VideoType == "1")
                this.ClosePreviewWindow();
            else
            {
                //if (videoSource != null)
                //    videoSource.SignalToStop();
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    videoSource.NewFrame -= video_NewFrame;
                }
            }
            SaveImages();
            Util.ReleaseMemory(pnlImgs, RML);
            Realse(pnlVideo);
            Realse(picHienThi);
        }
        public void Realse(PictureBox p)
        {
            try
            {
                if (p.Image != null)
                {
                    p.Image.Dispose();
                    p.Image = null;
                }
                GC.Collect();

            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }
        #region CaptureVideo
        const int WM_GRAPHNOTIFY = 0x8000 + 1;
        private IMediaEventEx mediaEvent = null;
        private int CaptureResult;
        private DirectX.Capture.Capture capture = null;
        private Filters filters  = new Filters();
        bool AllowSelectedIndexChanged = false;
        void cboVideoSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (VideoType == "1" || !AllowSelectedIndexChanged || this.capture == null) return;
                capture.PreviewWindow = null;
                // Restore previous preview setting
                capture.PreviewWindow = pnlVideo;
                this.capture.VideoSources = null;
                this.capture.VideoSource = capture.VideoSources[cboVideoSource.SelectedIndex];
                PropertyLib._HinhAnhProperties.VideoSource = cboVideoSource.SelectedIndex;
                PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
            }
            catch (Exception)
            {
            }
        }

       
        public void ExitCaptureTest()
        {
            RealeaseWC();
            if (capture != null && VideoType=="0")
            {
                capture.Stop();
                capture.Dispose();
                capture = null;
            }
        }

        Filter getvideoDevice()
        {
            Filter f = null;
            for (int c = 0; c < filters.VideoInputDevices.Count; c++)
            {
                f = filters.VideoInputDevices[c];
                if (f.Name.ToUpper().TrimStart().TrimEnd() == PropertyLib._HinhAnhProperties.devicename.ToUpper().TrimStart().TrimEnd())
                    return f;
            }
            return null;
        }
        void InitCaptureItems()
        {
                this.Cursor = Cursors.WaitCursor;
                captureLog.Trace("GetFilters...");
                DirectX.Capture.Source current;
                Filter f;
                Control oldPreviewWindow = null;
                AllowSelectedIndexChanged = false;
            try
            {
                
                // Disable preview to avoid additional flashes (optional)
                if (capture != null)
                {
                    oldPreviewWindow = capture.PreviewWindow;
                    capture.PreviewWindow = null;
                }
                // Load video devices
                Filter videoDevice = null;
                if (capture != null)
                    videoDevice = capture.VideoDevice;
                cboDevices.Items.Clear();
                captureLog.Trace("Load VideoDevices-->Start");
                for (int c = 0; c < filters.VideoInputDevices.Count; c++)
                {
                    f = filters.VideoInputDevices[c];
                    cboDevices.Items.Add(f.Name);
                }
                captureLog.Trace("Load VideoDevices-->End");
                cboDevices.Text = PropertyLib._HinhAnhProperties.devicename;

                cboFrameSize.Text = PropertyLib._HinhAnhProperties.FrameSize;
                cboframerate.SelectedIndex = PropertyLib._HinhAnhProperties.FrameRate;
                
                captureLog.Trace("Load VideoSource-->Start");
                try
                {
                    cboVideoSource.Items.Clear();
                    capture.VideoSources = null;
                    current = capture.VideoSource;
                    for (int c = 0; c < capture.VideoSources.Count; c++)
                    {
                        cboVideoSource.Items.Add(capture.VideoSources[c].Name);
                    }
                    cboVideoSource.SelectedIndex = PropertyLib._HinhAnhProperties.VideoSource;
                    cboVideoSource.Enabled = cboVideoSource.Items.Count > 0;
                }
                catch { cboVideoSource.Enabled = false; }
                captureLog.Trace("Load VideoSource-->End");
                captureLog.Trace("Load VideoStandard-->Start");
                if ((this.capture != null) &&
                (this.capture.dxUtils != null) && (this.capture.dxUtils.VideoDecoderAvail))
                {
                    try
                    {
                        cboStandards.Items.Clear();
                        AnalogVideoStandard currentStandard = capture.dxUtils.VideoStandard;
                        AnalogVideoStandard availableStandards = capture.dxUtils.AvailableVideoStandards;
                        int mask = 1;
                        while (mask <= (int)AnalogVideoStandard.PAL_N_COMBO)
                        {
                            int avs = mask & (int)availableStandards;
                            if (avs != 0)
                            {
                                cboStandards.Items.Add(((AnalogVideoStandard)avs).ToString());
                            }
                            mask *= 2;
                        }
                        cboStandards.Text = PropertyLib._HinhAnhProperties.VideoStandard.ToString();
                        cboStandards.Enabled = cboStandards.Items.Count > 0;
                    }
                    catch { cboStandards.Enabled = false; }
                }
                captureLog.Trace("Load VideoStandard-->End");
                // Reenable preview if it was enabled before
                if (capture != null)
                    capture.PreviewWindow = oldPreviewWindow;
                AllowSelectedIndexChanged = true;
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
        }
        void AutoPreview()
        {
            try
            {
                if (capture == null) return;
                Control oldPreviewWindow = null;
                if (capture != null)
                {
                    oldPreviewWindow = capture.PreviewWindow;
                    capture.PreviewWindow = null;
                }
                if (capture != null && oldPreviewWindow == null)
                    capture.PreviewWindow = pnlVideo;
                else
                    capture.PreviewWindow = oldPreviewWindow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to enable/disable preview. Please submit a bug report.\n\n" + ex.Message + "\n\n" + ex.ToString());
            }
            finally
            {
                AutoReSize();
            }
        }
        void StartUpVideo()
        {
           
            try
            {
                captureLog.Trace("StartUpVideo...");
                
                this.Cursor = Cursors.WaitCursor;
                Filter f;
                DirectX.Capture.Source current;
                AllowSelectedIndexChanged = false;
                cboDevices.Items.Clear();
                captureLog.Trace("cboDevices.Items.Add");
                for (int c = 0; c < filters.VideoInputDevices.Count; c++)
                {
                    f = filters.VideoInputDevices[c];
                    cboDevices.Items.Add(f.Name);
                }
                captureLog.Trace("");
                cboDevices.Text = PropertyLib._HinhAnhProperties.devicename;
                cboFrameSize.Text = PropertyLib._HinhAnhProperties.FrameSize;
                cboframerate.SelectedIndex = PropertyLib._HinhAnhProperties.FrameRate;
                Filter videoDevice = getvideoDevice();
                captureLog.Trace("Create capture...");
                if (videoDevice != null)
                {
                    //Create new instance
                    capture = new DirectX.Capture.Capture(videoDevice, null, true);
                    capture.captureLog = this.captureLog;
                    capture.CaptureComplete += capture_CaptureComplete;
                    capture.Filename = Application.StartupPath + @"\temp.avi";
                }
                if (capture!=null && capture.PreviewWindow == null)
                    capture.PreviewWindow = pnlVideo;
                captureLog.Trace("Create capture Done...");
                try
                {
                    cboVideoSource.Items.Clear();
                    capture.VideoSources = null;
                    current = capture.VideoSource;
                    for (int c = 0; c < capture.VideoSources.Count; c++)
                    {
                        cboVideoSource.Items.Add(capture.VideoSources[c].Name);
                    }
                    cboVideoSource.SelectedIndex = PropertyLib._HinhAnhProperties.VideoSource;
                    cboVideoSource.Enabled = true;
                }
                catch { cboVideoSource.Enabled = false; }

                if ((this.capture != null) &&
                (this.capture.dxUtils != null) && (this.capture.dxUtils.VideoDecoderAvail))
                {
                    try
                    {
                        cboStandards.Items.Clear();
                        AnalogVideoStandard currentStandard = capture.dxUtils.VideoStandard;
                        AnalogVideoStandard availableStandards = capture.dxUtils.AvailableVideoStandards;
                        int mask = 1;
                        while (mask <= (int)AnalogVideoStandard.PAL_N_COMBO)
                        {
                            int avs = mask & (int)availableStandards;
                            if (avs != 0)
                            {
                                cboStandards.Items.Add(((AnalogVideoStandard)avs).ToString());
                            }
                            mask *= 2;
                        }
                        cboStandards.Text = PropertyLib._HinhAnhProperties.VideoStandard.ToString();
                        cboStandards.Enabled = true;
                    }
                    catch { cboStandards.Enabled = false; }
                }
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                
                this.Cursor = Cursors.Default;
                if (capture != null)
                {
                    cmdCapture.Visible = true;
                    AllowSelectedIndexChanged = true;
                    bhCapture = true;
                    cboDevices_SelectedIndexChanged(cboDevices, new EventArgs());
                    //cboframerate_SelectedIndexChanged(cboframerate, new EventArgs());
                    //cboStandards_SelectedIndexChanged(cboStandards, new EventArgs());
                }
            }
        }
        void cboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectedIndexChanged) return;
                // Get current devices and dispose of capture object
                // because the video and audio device can only be changed
                // by creating a new Capture object.
                 if (VideoType == "1")
                {
                    this.iDevice = this.cboDevices.SelectedIndex;
                    PropertyLib._HinhAnhProperties.deviceIdx = cboDevices.SelectedIndex;
                    PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                    ClosePreviewWindow();
                    this.OpenPreviewWindow();
                    return;
                }
                 else if (VideoType == "15011981")
                 {
                     StartAforge();
                     return;
                 }
                Filter videoDevice = null;
                Filter audioDevice = null;
                if (capture != null)
                {
                    videoDevice = capture.VideoDevice;
                    audioDevice = capture.AudioDevice;
                    capture.Dispose();
                    capture = null;
                }
                PropertyLib._HinhAnhProperties.devicename = cboDevices.Text;
                PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                // Get new video device
                videoDevice = getvideoDevice();
                captureLog.Trace(videoDevice == null ? "getvideoDevice=null" : "getvideoDevice=OK");
                // Create capture object
                if ((videoDevice != null) || (audioDevice != null))
                {
                    capture = new DirectX.Capture.Capture(videoDevice, null, false);
                    capture.captureLog = this.captureLog;
                    capture.CaptureComplete += capture_CaptureComplete;
                    capture.Filename = Application.StartupPath + @"\temp.avi";
                    // Set flag only if capture device is initialized
                    this.capture.VideoSource = this.capture.VideoSource;
                    //this.capture.UseVMR9 = this.menuUseVMR9.Checked;
                }
                InitCaptureItems();
                
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
            }
            finally
            {
                
            }
        }
        void StartAforge()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.NewFrame -= new AForge.Video.NewFrameEventHandler(video_NewFrame);
                videoSource.SignalToStop();
            }
                
            videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[cboDevices.SelectedIndex].MonikerString);
            label6_Click(label6, new EventArgs());
            PropertyLib._HinhAnhProperties.devicename = cboDevices.Text;
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
            videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(video_NewFrame);
            
            // start the video source
            videoSource.Start();
            if (PropertyLib._HinhAnhProperties._VideoCapabilities >= 0)
                cboFrameSize.SelectedIndex = PropertyLib._HinhAnhProperties._VideoCapabilities;
        }
        #region WebcamCamera
         
       void InitWC()
        {
            if (!DesignMode)
            {
                AutoReSize();
                pnlWC.BringToFront();
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);

                if( comboBoxCameras.Items.Count > 0 )
                    comboBoxCameras.SelectedIndex = 0;
                // Early return if we've selected the current camera
                if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                    return;

                StartWC();
            }
        }

        void RealeaseWC()
        {
            thrashOldCamera();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            thrashOldCamera();
        }
        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private Camera CurrentCamera
        {
           get
           {
              return comboBoxCameras.SelectedItem as Camera;
           }
        }
        void StartWC()
        {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartWC();
        }
        

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth =Convert.ToInt32( PropertyLib._HinhAnhProperties.FrameSize.Split('x')[0]);//640
                _frameSource.Camera.CaptureHeight = Convert.ToInt32(PropertyLib._HinhAnhProperties.FrameSize.Split('x')[1]);// 480;
                _frameSource.Camera.Fps = PropertyLib._HinhAnhProperties.FrameRate;
               // pictureBoxDisplay.Size = new Size(_frameSource.Camera.CaptureWidth, _frameSource.Camera.CaptureHeight);
                _frameSource.NewFrame += OnImageCaptured;
                pictureBoxDisplay.Size = new Size(_frameSource.Camera.CaptureWidth, _frameSource.Camera.CaptureHeight);
                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                cameraPropertyValue.Enabled = _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        //

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        //

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_frameSource == null)
                return;

            Bitmap current = (Bitmap)_latestFrame.Clone();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "*.png|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    current.Save(sfd.FileName);
                }
            }

            current.Dispose();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            // snap camera
            if (_frameSource != null)
                _frameSource.Camera.ShowPropertiesDialog();
        }

        #region Camera Property Controls
        private IDictionary<String, CameraProperty> displayPropertyValues;

        private IDictionary<String, CameraProperty> DisplayPropertyValues
        {
           get
           {
              if( displayPropertyValues == null )
                 displayPropertyValues = new Dictionary<String, CameraProperty>()
                 {
                    { "Pan (Degrees)", CameraProperty.Pan_degrees }, 
                    { "Tilt (Degrees)", CameraProperty.Tilt_degrees }, 
                    { "Roll (Degrees)", CameraProperty.Roll_degrees }, 
                    { "Zoom (mm)", CameraProperty.Zoom_mm }, 
                    { "Exposure (log2(seconds))", CameraProperty.Exposure_lgSec }, 
                    { "Iris (10f)", CameraProperty.Iris_10f }, 
                    { "Focal Length (mm)", CameraProperty.FocalLength_mm }, 
                    { "Flash", CameraProperty.Flash }, 
                    { "Brightness", CameraProperty.Brightness }, 
                    { "Contrast", CameraProperty.Contrast }, 
                    { "Hue", CameraProperty.Hue }, 
                    { "Saturation", CameraProperty.Saturation }, 
                    { "Sharpness", CameraProperty.Sharpness }, 
                    { "Gamma", CameraProperty.Gamma }, 
                    { "Color Enable", CameraProperty.ColorEnable }, 
                    { "White Balance", CameraProperty.WhiteBalance }, 
                    { "Backlight Compensation", CameraProperty.BacklightCompensation }, 
                    { "Gain", CameraProperty.Gain }, 
                 };

              return displayPropertyValues;
           }
        }

        private IDictionary<CameraProperty, CameraPropertyCapabilities> CurrentCameraPropertyCapabilities
        {
           get;
           set;
        }

        private IDictionary<CameraProperty,CameraPropertyRange> CurrentCameraPropertyRanges
        {
           get;
           set;
        }

        private CameraProperty SelectedCameraProperty
        {
           get
           {
              Int32 selectedIndex = cameraPropertyValue.SelectedIndex;
              String selectedItem = cameraPropertyValue.Items[ selectedIndex ] as String;

              CameraProperty result = DisplayPropertyValues[ selectedItem ];
              return result;
           }
        }

        private Boolean IsSelectedCameraPropertySupported
        {
           get;
           set;
        }

        private Boolean IsCameraPropertyValueTypeValue
        {
           get
           {
              return ( ( String ) cameraPropertyValueTypeSelection.SelectedItem ) == "Value";
           }
        }

        private Boolean IsCameraPropertyValueTypePercentage
        {
           get
           {
              return ( ( String ) cameraPropertyValueTypeSelection.SelectedItem ) == "Percentage";
           }
        }

        private Int32 CameraPropertyValue
        {
           get
           {
              Decimal value = cameraPropertyValueValue.Value;

              Int32 result;
              if( IsCameraPropertyValueTypeValue || IsCameraPropertyValueTypePercentage )
              {
                 value = Math.Round( value );

                 result = Convert.ToInt32( value );
              }
              else
                 throw new NotSupportedException( String.Format( "Camera property value type '{0}' is not supported.", ( String ) cameraPropertyValueTypeSelection.SelectedItem ) );

              return result;
           }
        }

        private Boolean IsCameraPropertyAuto
        {
           get
           {
              return cameraPropertyValueAuto.Checked;
           }
        }

        private Boolean SuppressCameraPropertyValueValueChangedEvent
        {
           get;
           set;
        }

        private Boolean CameraPropertyControlInitializationComplete
        {
           get;
           set;
        }

        private void InitializeCameraPropertyControls()
        {
           CameraPropertyControlInitializationComplete = false;

           CurrentCameraPropertyCapabilities = CurrentCamera.CameraPropertyCapabilities;
           CurrentCameraPropertyRanges = new Dictionary<CameraProperty, CameraPropertyRange>();

           cameraPropertyValueTypeSelection.SelectedIndex = 0;

           cameraPropertyValue.Items.Clear();
           cameraPropertyValue.Items.AddRange( DisplayPropertyValues.Keys.ToArray() );

           CameraPropertyControlInitializationComplete = true;

           cameraPropertyValue.SelectedIndex = 0;
        }

        private void UpdateCameraPropertyRange( CameraPropertyCapabilities propertyCapabilities )
        {
           String text;
           if( IsSelectedCameraPropertySupported && propertyCapabilities.IsGetRangeSupported && propertyCapabilities.IsGetSupported )
           {
              CameraPropertyRange range = CurrentCamera.GetCameraPropertyRange( SelectedCameraProperty );
              text = String.Format( "[ {0}, {1} ], step: {2}", range.Minimum, range.Maximum, range.Step );

              Int32 decimalPlaces;
              Decimal minimum, maximum, increment;
              if( IsCameraPropertyValueTypeValue )
              {
                 minimum = range.Minimum;
                 maximum = range.Maximum;
                 increment = range.Step;
                 decimalPlaces = 0;
              }
              else if( IsCameraPropertyValueTypePercentage )
              {
                 minimum = 0;
                 maximum = 100;
                 increment = 0.01M;
                 decimalPlaces = 2;
              }
              else
                 throw new NotSupportedException( String.Format( "Camera property value type '{0}' is not supported.", ( String ) cameraPropertyValueTypeSelection.SelectedItem ) );

              cameraPropertyValueValue.Minimum = minimum;
              cameraPropertyValueValue.Maximum = maximum;
              cameraPropertyValueValue.Increment = increment;
              cameraPropertyValueValue.DecimalPlaces = decimalPlaces;

              if( CurrentCameraPropertyRanges.ContainsKey( SelectedCameraProperty ) )
                 CurrentCameraPropertyRanges[ SelectedCameraProperty ] = range;
              else
                 CurrentCameraPropertyRanges.Add( SelectedCameraProperty, range );

              CameraPropertyValue value = CurrentCamera.GetCameraProperty( SelectedCameraProperty, IsCameraPropertyValueTypeValue );

              SuppressCameraPropertyValueValueChangedEvent = true;
              cameraPropertyValueValue.Value = value.Value;
              cameraPropertyValueAuto.Checked = value.IsAuto;
              SuppressCameraPropertyValueValueChangedEvent = false;
           }
           else
              text = "N/A";

           cameraPropertyRangeValue.Text = text;
        }

        private void cameraPropertyValueTypeSelection_SelectedIndexChanged( Object sender, EventArgs e )
        {

        }

        private void cameraPropertyValueValue_ValueChanged( Object sender, EventArgs e )
        {

        }

        private void cameraPropertyValueAuto_CheckedChanged( Object sender, EventArgs e )
        {

        }

        private void cameraPropertyValue_SelectedIndexChanged( Object sender, EventArgs e )
        {

        }

        private void cameraPropertyValueValue_EnabledChanged( Object sender, EventArgs e )
        {

        }

        private void cameraPropertyValue_EnabledChanged( Object sender, EventArgs e )
        {

        }
        #endregion
        #endregion
        private void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                using (Bitmap frame = (Bitmap)eventArgs.Frame.Clone())
                {

                    // Hiển thị hình ảnh trong PictureBox
                    if (pnlVideo.Image != null)
                    {
                        pnlVideo.Image.Dispose();
                    }
                    pnlVideo.Image = (Bitmap)frame.Clone();
                }

                //// get new frame
                //Bitmap img = (Bitmap)eventArgs.Frame.Clone();
                ////if (trbContrast.Value != 0)
                ////{
                ////    AForge.Imaging.Filters.ContrastCorrection c = new AForge.Imaging.Filters.ContrastCorrection(trbContrast.Value);
                ////    c.ApplyInPlace(img);
                ////}
                ////if (trbBrightness.Value != 0)
                ////{
                ////    AForge.Imaging.Filters.BrightnessCorrection c1 = new AForge.Imaging.Filters.BrightnessCorrection(trbBrightness.Value);
                ////    c1.ApplyInPlace(img);
                ////}

                ////if (chkSharpen.Checked)
                ////{
                ////    AForge.Imaging.Filters.Sharpen c5 = new AForge.Imaging.Filters.Sharpen();
                ////    c5.ApplyInPlace(img);
                ////}

                //if (pnlVideo.Image != null)
                //{
                //    pnlVideo.Image.Dispose();
                //}
                //pnlVideo.Image = img;
                //Thread.Sleep(10);
            }
            catch (Exception)
            {


            }
        }
        void cboFrameSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (VideoType != "0")
                {
                    if (VideoType == "15011981")
                    {
                        videoSource.Stop();
                        videoSource.VideoResolution =lstResol[cboFrameSize.SelectedIndex];
                        PropertyLib._HinhAnhProperties._VideoCapabilities = cboFrameSize.SelectedIndex;
                        videoSource.Start();
                        
                        PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                    }
                    AutoReSize();
                    return;
                }
                if (!AllowSelectedIndexChanged || this.capture == null) return;
                capture.PreviewWindow = null;
                // Update the frame size
                string[] s = cboFrameSize.Text.Split('x');
                Size size = new Size(int.Parse(s[0]), int.Parse(s[1]));
                capture.FrameSize = size;
                PropertyLib._HinhAnhProperties.FrameSize = cboFrameSize.Text;
                AutoReSize();
                // Restore previous preview setting
                capture.PreviewWindow = pnlVideo;

               PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                InitCaptureItems();
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
            }
        }
        void cboframerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (VideoType=="1"|| !AllowSelectedIndexChanged || this.capture == null) return;
                string[] s = cboframerate.Text.Split(' ');
                capture.FrameRate = double.Parse(s[0]);
                PropertyLib._HinhAnhProperties.FrameRate = cboframerate.SelectedIndex;
               PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                InitCaptureItems();
            }
            catch (Exception ex)
            {

                captureLog.Error(ex.ToString());
            }
        }
        void cboStandards_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (VideoType != "0" || !AllowSelectedIndexChanged || this.capture == null) return;
                if ((this.capture == null) || (this.capture.dxUtils == null))
                {
                    return;
                }
                try
                {
                    AnalogVideoStandard a = (AnalogVideoStandard)Enum.Parse(typeof(AnalogVideoStandard), cboStandards.Text);
                    capture.dxUtils.VideoStandard = a;
                    PropertyLib._HinhAnhProperties.VideoStandard = a;
                   PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                    InitCaptureItems();
                }
                catch (Exception ex)
                {
                    captureLog.Error(ex.ToString());
                }
            }
            catch (Exception ex1)
            {
                captureLog.Error(ex1.ToString());
            }
        }
        void capture_CaptureComplete(object sender, EventArgs e)
        {

        }
        // Media events are sent to use as windows messages
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // If this is a windows media message
                case WM_GRAPHNOTIFY:
#if DSHOWNET
                    DsEvCode eventCode;
#else

                    EventCode eventCode;
#endif
#if VS2012
                    int p1, p2, hr;

                    hr = mediaEvent.GetEvent(out eventCode, out p1, out p2, 0);
#else
                    IntPtr p1, p2;
                    int hr;

                    hr = mediaEvent.GetEvent(out eventCode, out p1, out p2, 0);
#endif
                    while (hr == 0)
                    {
                        // Handle the event.
                        switch (eventCode)
                        {
#if DSHOWNET
                            case DsEvCode.ErrorAbort:
#else
                            case EventCode.ErrorAbort:
#endif
                                // The capture has been aborted
#if VS2012
                                CaptureResult = p1;
#else
                                int[] tmp = new int[1];
                                Marshal.Copy(p1, tmp, 0, 1);
                                CaptureResult = tmp[0];
#endif
                                break;
#if DSHOWNET
                            case DsEvCode.FullScreenLost:
#else
                            case EventCode.FullScreenLost:
#endif
                                break;
                            default:
                                break;
                        }

#if DEBUG
                        Debug.WriteLine("Media Event " + eventCode.ToString() + " received ...");
#endif
                        // Release parms
                        mediaEvent.FreeEventParams(eventCode, p1, p2);

                        // check for additional events
                        hr = mediaEvent.GetEvent(out eventCode, out p1, out p2, 0);
                    }
                    break;

                // All other messages
                default:
                    try
                    {
                        // unhandled window message
                        base.WndProc(ref m);
                    }
                    catch (Exception ex)
                    {
                        captureLog.Error(ex.ToString());
                        Debug.WriteLine("Fatal exception catching a message with WndProc()");
                    }
                    break;
            }
        }
        bool isViewing = false;
        private void cmdStart_Click(object sender, EventArgs e)
        {
            picHienThi.SendToBack();
            cmdCapture.Visible = true;
            grbPic.Text = "Video";
            Application.DoEvents();
        }
        public List<string> listAnh = new List<string>();
        static System.Drawing.Imaging.ImageFormat getImgFormat(string ext)
        {
            if (ext.ToUpper().Contains("PNG"))
                return System.Drawing.Imaging.ImageFormat.Png;
            if (ext.ToUpper().Contains("BMP"))
                return System.Drawing.Imaging.ImageFormat.Bmp;
            if (ext.ToUpper().Contains("JPEG"))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (ext.ToUpper().Contains("GIF"))
                return System.Drawing.Imaging.ImageFormat.Gif;

            return System.Drawing.Imaging.ImageFormat.Bmp;
        }
        private void SaveControlImage_bak(Control theControl)
        {
            try
            {
                string folderName = Application.StartupPath + @"\TempIMG";
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);
                string ex=Util.getImageFormat();
                string filename = folderName + @"\" + filePrefix + "_" + Guid.NewGuid().ToString() + ex;
                if (theControl == null && _frameSource != null)
                {
                    Bitmap current = (Bitmap)_latestFrame.Clone();
                    current.Save(filename, getImgFormat(ex));

                    current.Dispose();
                    goto _next;
                }
                //using (Bitmap controlBitMap = new Bitmap(theControl.ClientSize.Width, theControl.ClientSize.Height))
                //{
                //if (Utility.Int64Dbnull( DateTime.Now.ToString("yyMMdd")) >= 181231 || PropertyLib._HinhAnhProperties.CropRec.Width <= 0 || PropertyLib._HinhAnhProperties.CropRec.Height <= 0)
                //{
                //    Bitmap controlBitMap = new Bitmap(theControl.ClientSize.Width, theControl.ClientSize.Height);
                //    Graphics g = Graphics.FromImage(controlBitMap);
                //    g.CopyFromScreen(theControl.PointToScreen(System.Drawing.Point.Empty), new System.Drawing.Point(0, 0), theControl.ClientRectangle.Size);
                //    controlBitMap.Save(filename, getImgFormat(ex));
                //    controlBitMap.Dispose();
                //    controlBitMap = null;
                //    GC.Collect();
                //    g.Dispose();
                //    g = null;
                //    GC.Collect();
                //}
                //else
                //{
                    Bitmap controlBitMap = new Bitmap(PropertyLib._HinhAnhProperties.CropRec.Width, PropertyLib._HinhAnhProperties.CropRec.Height);
                    Graphics g = Graphics.FromImage(controlBitMap);
                    int x = (theControl.ClientRectangle.Size.Width - PropertyLib._HinhAnhProperties.CropRec.Width) / 2;
                    int y = (theControl.ClientRectangle.Size.Height - PropertyLib._HinhAnhProperties.CropRec.Height) / 2;
                    g.CopyFromScreen(theControl.PointToScreen(new System.Drawing.Point(x, y)), new System.Drawing.Point(0, 0), PropertyLib._HinhAnhProperties.CropRec.Size);
                    controlBitMap.Save(filename, getImgFormat(ex));
                    controlBitMap.Dispose();
                    controlBitMap = null;
                    GC.Collect();
                    g.Dispose();
                    g = null;
                    GC.Collect();
                //}
                    // example of saving to the desktop

                    // example of saving to the desktop
                  _next:  
                //}

                if (!listAnh.Contains(filename))
                {
                    listAnh.Add(filename);
                    cboHinhAnh.Items.Add(Path.GetFileName(filename));
                }
                UC_Image objUcImage = new UC_Image(pnlImgs);
               // objUcImage.LayoutPanel = pnlImgs;
                objUcImage.DuongdanLocal = filename;
                objUcImage.ImageBytes = imageToByteArray(GetImage(filename));
                objUcImage.Mota = string.Empty;
                objUcImage._OnClick += objUcImage__OnClick;
                //PictureBox p = new PictureBox();
                //p.Image = 
                //p.Tag = filename;
                //p.Click += p_Click;
                //p.Width = 150;
                //p.Height = 150;
                //p.SizeMode = picHienThi.SizeMode;
                pnlImgs.Controls.Add(objUcImage);
                pnlImgs.ScrollControlIntoView(objUcImage);
               // pnlImgs.ScrollControlIntoView(objUcImage);
                
                if (cboHinhAnh.SelectedIndex < 0)
                    cboHinhAnh.SelectedIndex = 0;
                //lblHinhAnh.Text = "Hiện tại đã có " + cboHinhAnh.Items.Count + "hình ảnh.";
                CurrIdx = pnlImgs.Controls.Count - 1;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString());
            }

            //pnlImg.BackgroundImage = Image.FromFile(filename);
        }
        private void SaveControlImage(PictureBox theControl)
        {
            try
            {
                string folderName = Application.StartupPath + @"\TempIMG";
                if (!Directory.Exists(folderName))
                    Directory.CreateDirectory(folderName);
                string ex = Util.getImageFormat();
                string filename = folderName + @"\"  + Guid.NewGuid().ToString() + ex;
               
                theControl.Image.Save(filename, getImgFormat(ex));
                GC.Collect();
               
         
          
                if (!listAnh.Contains(filename))
                {
                    listAnh.Add(filename);
                    cboHinhAnh.Items.Add(Path.GetFileName(filename));
                }
                UC_Image objUcImage = new UC_Image(pnlImgs);
                // objUcImage.LayoutPanel = pnlImgs;
                objUcImage.DuongdanLocal = filename;
                objUcImage.ImgData = theControl.Image;
                objUcImage.Mota = string.Empty;
                objUcImage._OnClick += objUcImage__OnClick;
                //PictureBox p = new PictureBox();
                //p.Image = 
                //p.Tag = filename;
                //p.Click += p_Click;
                //p.Width = 150;
                //p.Height = 150;
                //p.SizeMode = picHienThi.SizeMode;
                pnlImgs.Controls.Add(objUcImage);
                pnlImgs.ScrollControlIntoView(objUcImage);
                // pnlImgs.ScrollControlIntoView(objUcImage);

                if (cboHinhAnh.SelectedIndex < 0)
                    cboHinhAnh.SelectedIndex = 0;
                //lblHinhAnh.Text = "Hiện tại đã có " + cboHinhAnh.Items.Count + "hình ảnh.";
                CurrIdx = pnlImgs.Controls.Count - 1;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //pnlImg.BackgroundImage = Image.FromFile(filename);
        }
        void objUcImage__OnClick(UC_Image obj)
        {
            try
            {
                pnlWC.SendToBack();
                pnlVideo.Visible = false;
                Application.DoEvents();
                picHienThi.Visible = true;
                cmdCapture.Visible = false;
                grbPic.Text = "Hình ảnh";
                picHienThi.Image = (Bitmap)obj.PIC_Image.Image.Clone();
                cmdBeginCapture.Text = "Chụp tiếp(F9)";
                cmdBeginCapture.Tag = "1";
            }
            catch (Exception)
            {
                
               
            }
           
        }

        public bool bhCapture = false;
        void modifycommand()
        {
            cmdCapture.Enabled = true;
            cmdLuuLai.Enabled = cmdXoaToanBo.Enabled = pnlImgs.Controls.Count >= 1;
        }
        /// <summary>
        /// Hàm convert Image vào mảng chỉ cần truyền vào là Image
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
             string ex=Util.getImageFormat();
             imageIn.Save(ms, getImgFormat(ex));
            return ms.ToArray();
        }
        private System.Drawing.Image GetImage(string path)
        {
            using (System.Drawing.Image im = System.Drawing.Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        int CurrIdx = -1;

        private void cmdCapture_Click(object sender, EventArgs e)
        {
            SaveControlImage(VideoType == "1" ? null : pnlVideo);
            uiGroupBox4.Text = pnlImgs.Controls.Count.ToString()+" ảnh";
            Application.DoEvents();
            modifycommand();
        }
        #endregion
        public delegate void SetParameterValueDelegate();
        private void cmdCapture_Click_1(object sender, EventArgs e)
        {

        }


        private void cmdXoaToanBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả các hình ảnh vừa chụp để chụp lại hay không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                pnlImgs.Controls.Clear();
            }
        }
       public bool hasImages = false;
        void SaveImages()
        {
            try
            {
                lstImgs.Clear();
                this.Text = "Đang lưu dữ liệu ảnh";
                iscancel = false;
                hasImages = pnlImgs.Controls.Count > 0;
                int i = 1;
                foreach (UC_Image control in pnlImgs.Controls)
                {
                    lstImgs.Add(control.DuongdanLocal);
                    this.Text = string.Format("{0}/{1}", i.ToString(), pnlImgs.Controls.Count.ToString());
                    i += 1;

                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        string fromimagepath2base64(string filename)
        {
            try
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(filename))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        // Convert byte[] to Base64 String
                        return Convert.ToBase64String(imageBytes);

                    }
                }
            }
            catch (Exception)
            {
                return "";
            }

        }
        static System.Drawing.Image frombase642Img(string base64String)
        {
            try
            {
                byte[] fileBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    System.Drawing.Image streamImage = System.Drawing.Image.FromStream(ms);
                    return streamImage;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        private void cmdLuuLai_Click(object sender, EventArgs e)
        {
            if (iscancel)
                SaveImages();
        }

        private void cmdBeginCapture_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmdBeginCapture.Tag.ToString() == "0")
                {
                    picHienThi.SendToBack();
                    grbPic.Text = "Video";

                    ExitCaptureTest();
                    if (VideoType == "0")
                        Init();
                    else if (VideoType == "1")
                        pnlWC.BringToFront();//InitWC();
                    else
                    {
                        cboDevices.Items.Clear();
                        for (int i = 0; i <= 1; i++)
                        {
                            cboDevices.Items.Add(videoDevices[i].Name);
                        }
                    }
                    modifycommand();
                }
                else
                {
                    picHienThi.Visible = false;
                    Application.DoEvents();
                    pnlVideo.Visible = true;
                    cmdBeginCapture.Tag = "0";
                    cmdBeginCapture.Text = "Restart(F9)";
                    cmdCapture.Visible = true;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                captureLog.Error(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

           
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void SaveSize()
        {
        }
        private void cmdSaveImage_Click(object sender, EventArgs e)
        {

            SaveSize();
        }

        

        private void chkBatChupLenStartVideo_CheckedChanged(object sender, EventArgs e)
        {
           // PropertyLib._HinhAnhProperties.IsStartVideo = chkBatChupLenStartVideo.Checked;
           PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }

        private void cboFrameSize_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        #region AutoResize
        void AutoReSize()
        {
            try
            {
                    string[] s = cboFrameSize.Text.Split('x');
                    Size size = new Size(int.Parse(s[0]), int.Parse(s[1]));

                pnlVideo.Dock = DockStyle.None;
                pnlVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                if (!PropertyLib._HinhAnhProperties.AllowAutoSize )//|| VideoType=="1")
                {
                    pnlVideo.Size = new Size(640, 480);
                    CenterVideoWindow();
                    return;
                }
                captureLog.Trace("AutoReSize...");
                if (PropertyLib._HinhAnhProperties.AllowAutoCalSelectedSize || size.Height > pnlCapture.Height || size.Width > pnlCapture.Width)
                {
                    int _h = pnlCapture.Height - 10;
                    string ssize = cboFrameSize.Text;
                    if (cboFrameSize.SelectedIndex < 0)
                        ssize = "640x480";
                    s = ssize.Split('x');
                    int w1 = int.Parse(s[0]);
                    int h1 = int.Parse(s[1]);
                    int _w = (int)(w1 * _h / h1);
                    if (_w > pnlCapture.Width - 10)
                    {
                        _w = pnlCapture.Width - 10;
                        _h = (int)(h1 * _w / w1);
                    }
                    pnlVideo.Size = new Size(_w, _h);
                }
                else
                {
                    
                    pnlVideo.Size = size;
                }
                OrgWidth = pnlVideo.Size.Width;
               
            }
            catch(Exception ex)
            {
                pnlVideo.Size = new Size(640, 480);
                captureLog.Error(ex.ToString());
            }
            finally
            {
                CenterVideoWindow();
                Application.DoEvents();
            }
        }
        decimal getRatio()
        {
            try
            {
               

                return (decimal)capture.FrameSize.Height / capture.FrameSize.Width;
            }
            catch
            {
                return 480 / 640;
            }
        }
        void CenterVideoWindow()
        {
            try
            {
                CenterHorizontally();
                CenterVertically();
            }
            catch
            {
            }
        }
        void CenterHorizontally()
        {
            try
            {
                Rectangle parentRect = pnlCapture.ClientRectangle;
                pnlVideo.Left = (pnlCapture.Width - pnlVideo.Width) / 2;

                 parentRect = pnlWC.ClientRectangle;
                 pictureBoxDisplay.Left = (pnlWC.Width - pictureBoxDisplay.Width) / 2;
            }
            catch
            {
            }
        }
        void CenterVertically()
        {
            try
            {
                Rectangle parentRect = pnlCapture.ClientRectangle;
                pnlVideo.Top = (pnlCapture.Height - pnlVideo.Height) / 2;

                parentRect = pnlWC.ClientRectangle;
                pictureBoxDisplay.Top = (pnlWC.Height -pnlWCAct.Height- pictureBoxDisplay.Height) / 2;
            }
            catch
            {
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Auto resize+-5 pixcel to refresh
            if (pnlVideo.Width == OrgWidth)
                pnlVideo.Width -= extrawidth;
            else
                pnlVideo.Width += extrawidth;
            pnlVideo.Refresh();
            pnlVideo.Invalidate();
            Application.DoEvents();
        }
        int OrgWidth = -1;
       
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
           
        }
        AForge.Video.DirectShow.VideoCapabilities[] lstResol;
        private void label6_Click(object sender, EventArgs e)
        {
            lstResol = videoSource.VideoCapabilities;
            cboFrameSize.Items.Clear();
            for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
            {
                string[] s = videoSource.VideoCapabilities[i].FrameSize.ToString().Replace("{", "").Replace("}", "").Split(',');
                string temp = s[0].Split('=')[1] + "x" + s[1].Split('=')[1];
                
                cboFrameSize.Items.Add(temp);
            }

        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._HinhAnhProperties);
            _Properties.ShowDialog();
            if (VideoType == "1" && _frameSource != null)
            {
                _frameSource.Camera.CaptureWidth = Convert.ToInt32(PropertyLib._HinhAnhProperties.FrameSize.Split('x')[0]);//640
                _frameSource.Camera.CaptureHeight = Convert.ToInt32(PropertyLib._HinhAnhProperties.FrameSize.Split('x')[1]);// 480;
                _frameSource.Camera.Fps = PropertyLib._HinhAnhProperties.FrameRate;
                pictureBoxDisplay.Size = new Size(_frameSource.Camera.CaptureWidth, _frameSource.Camera.CaptureHeight);
                CenterVideoWindow();
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {

        }
    }
}
