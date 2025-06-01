using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using System.Linq;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using System.Collections.Generic;
using WPF.UCs;
using VNS.Properties;
using VNS.UCs;
using Aspose.Words;
using System.Diagnostics;
using System.Drawing.Printing;
using Aspose.Words.Tables;
using Aspose.Words.Drawing;
using VNS.HIS.UI.HinhAnh;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net.Sockets;
using System.Transactions;
using VNS.HIS.UI.NGOAITRU;
using VMS.HIS.Danhmuc;
using Word = Microsoft.Office.Interop.Word;

namespace VNS.HIS.UI.Forms.HinhAnh
{
    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
    public partial class frm_NhaptraKQ : Form
    {
        #region
        public Pdf2HisManager _Pdf2HisManager = new Pdf2HisManager();
        private int id_Study_Detail;
        public FTPclient FtpClientPDF;
        public FTPclient FtpClient;
        private DataTable _dtThongTinBenhNhan;
        public DataRow drWorklist = null;
        public DataRow drWorklistDetail = null;
        //private DataTable _dtHinhAnh;
        private DataTable _dtKetQuaChuanDoan;
        KcbChidinhclsChitiet objKcbChidinhclsChitiet = null;
        KcbChidinhcl objChidinh = null;
        public int id_VungKS = -1;
        private string FtpClientCurrentDirectoryPdf = "";
        private readonly string _baseDirectoryPdf = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Pdf2His\\");

        private string FtpClientCurrentDirectory = "";
        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Anh_SIEUAM_NOISOI\\");
        private readonly string _path = Application.StartupPath;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        private string sFilter = "All|*.jpg;*.PNG;*.BMP;*.Gif|JPG|*.jpg|PNG|*.PNG|BMP|*.BMP|GIF|*.Gif";
        private string fileNoImage = AppDomain.CurrentDomain.BaseDirectory + @"\Path\noimage.jpg";
        public bool mv_blnCancel = true;
        protected bool b_HasKetLuan = false;
        protected bool b_HasDenghi = false;
        public string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "Doc\\";
        public bool Isxacnhan = false;
        public string StrServiceCode = "ALL";// Cấu trúc mã loại dịch vụ A,B+@+Mã máy C,D.
        Logger log = null;
        bool CKEditorInput = true;//true=CKeditor;false=Dynamic
        #endregion
        public List<string> lstFiles = new List<string>();
        #region Constructor
        //VNS.RISLink.Bussiness.UI.vbcap.Service1 cl = new VNS.RISLink.Bussiness.UI.vbcap.Service1();
        bool AllowCapture = false;
        public bool _IsReadonly = false;
        bool saveDocTemplates = false;
        string SplitterPath = "";
        public frm_NhaptraKQ()
        {
            InitializeComponent();
            

             SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            InitEvents();
            log = LogManager.GetLogger("NhaptraKQ_CDHA");
            _FtpManager = new FtpManager(this);
            _FtpManager._Onfinish += _FtpManager__Onfinish;
            _Pdf2HisManager.log = this.log;
            _Pdf2HisManager._OnQueueChanged += _Pdf2HisManager__OnQueueChanged;
            InitFtp();
            _FtpManager._OnSaving += _FtpManager__OnSaving;
            timer1.Enabled = true;
            timer1.Interval = 900;
            LoadLaserPrinters();
            //cl.Url = PropertyLib._HinhAnhProperties.AdminWS;
            //cl.Timeout = PropertyLib._HinhAnhProperties.Timeout;
            try
            {
                AllowCapture = true;// cl.IsValidLicense(GetIP4Address(), ref videotype, ref lstframeInfor, ref RML, ref numofimg) || File.Exists(Application.StartupPath + @"\nolic.txt"); ;
            }
            catch (Exception ex)
            {
                AllowCapture = false;
                videotype = "0";
                lstframeInfor = new List<string>() { "160 x 120;176 x 144;320 x 240;352 x 288;640 x 480;640 x 400;720 x 480;720 x 576;768 x 576;1280 x 720;480 x 600" }.ToArray();
                RML = "1";
                numofimg = "4";
            }

            //!File.Exists(Application.StartupPath + @"\nolic.txt") && !cl.IsValidLicense(GetIP4Address(), ref videotype, ref lstframeInfor, ref RML, ref numofimg);
            
            cmdChonfile.Visible = globalVariables.IsAdmin;
            chkPreview.Checked = PropertyLib._HinhAnhProperties.PrintPreview;
            cmdSave.Text = PropertyLib._HinhAnhProperties.PrintAfterSave ? "Lưu và in" : "Lưu kết quả";
            chkSignType.Checked = PropertyLib._HinhAnhProperties.SignType;
            this.Text = string.Format("{0}-{1}", this.Text,
                string.Format("Nhân viên thực hiện:{0}", globalVariables.gv_strTenNhanvien));
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor.html");
            webBrowserHistory.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor.html");

            timer2.Start();
            watch();
        }
       
        void _Pdf2HisManager__OnQueueChanged(int QueueCount)
        {

        }
        bool isShowHistory = false;
        void InitEvents()
        {
            this.DragEnter += frm_NhaptraKQ_DragEnter;
            this.DragDrop += frm_NhaptraKQ_DragDrop;
            this.Load += frm_NhaptraKQ_Load;
            this.KeyDown += frm_NhaptraKQ_KeyDown;
            this.SizeChanged += Frm_NhaptraKQ_SizeChanged;
            cmdChonvungKS.Click += cmdChonvungKS_Click;
            cmdChonfile.Click += cmdChonfile_Click;
            cmdChonlai.Click += cmdChonlai_Click;
            cmdChupAnh.Click += cmdChupAnh_Click;
            chkPreview.CheckedChanged += chkPreview_CheckedChanged;
            cmdConfig.Click += cmdConfig_Click;
            this.FormClosing += frm_NhaptraKQ_FormClosing;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
            cmdSave.Click += cmdSave_Click;
            cmdPrint.Click += cmdPrint_Click;
            cmdSaveAndAccept.Click += cmdSaveAndAccept_Click;
            cmdSend2PACS.Click += cmdSend2PACS_Click;
            Shown += frm_NhaptraKQ_Shown;
        }

        private void Frm_NhaptraKQ_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (isShowHistory)
                    pnlHistoryCKEditor.Width = Convert.ToInt32(Math.Ceiling((decimal)((pnlCkeditor.Width - 70) / 2)));
                else
                    pnlHistoryCKEditor.Width = 0;
            }
            catch (Exception)
            {

            }
        }

        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count > 0)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void frm_NhaptraKQ_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        void cmdSend2PACS_Click(object sender, EventArgs e)
        {

        }

        void cmdSaveAndAccept_Click(object sender, EventArgs e)
        {
            SaveKQ(false, true);
        }
        void cmdChonfile_Click(object sender, EventArgs e)
        {
            ChonFileMauTraKQ();
        }

        void cmdChonvungKS_Click(object sender, EventArgs e)
        {
            ChonVungKS();
        }

        void InitUI()
        {
            //grbSelectedImgs.Width = PropertyLib._HinhAnhProperties.ThumbnailWidth;
        }
        void cmdConfig_Click(object sender, EventArgs e)
        {
            log.Trace(globalVariables.m_strPropertiesFolder);
            frm_Properties _Properties = new frm_Properties(PropertyLib._HinhAnhProperties);
            _Properties.ShowDialog();
            foreach (ucAutoCompleteParam _ucp in flowDynamics.Controls)
            {
                _ucp.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                _ucp.txtValue.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
            }
            Application.DoEvents();
            InitUI();
            InitFtp();
        }
        private void InitFtp()
        {
            try
            {
                string FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_SERVER", string.Format("{0}-{1}-{2}", "127.0.0.1", "ris", "ris"), true);
                if (FTPInfor.Length > 0 && FTPInfor.Split('-').Count() == 3)
                {
                    PropertyLib._HinhAnhProperties.FTPServer = FTPInfor.Split('-')[0];
                    PropertyLib._HinhAnhProperties.UID = FTPInfor.Split('-')[1];
                    PropertyLib._HinhAnhProperties.PWD = FTPInfor.Split('-')[2];
                }
                PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
                FtpClient = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, "ris", "ris");
                FtpClient.UsePassive = true;
                FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                //_baseDirectory = Utility.DoTrim(PropertyLib._HinhAnhProperties.ImageFolder);
                //if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }

                FtpClientPDF = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, "pdf2his", "pdf2his");
                FtpClientPDF.UsePassive = true;
                FtpClientCurrentDirectoryPdf = FtpClientPDF.CurrentDirectory;


            }
            catch
            {
            }
        }
        void frm_NhaptraKQ_DragDrop(object sender, DragEventArgs e)
        {
            List<string> lstfiles = Tools.GetDropFiles(e.Data).ToList<string>();
            string AutoCheck = Utility.Laygiatrithamsohethong("AutoCheckImage", "0", true);
            if (lstfiles != null)
                foreach (string sFile in lstfiles)
                {
                    if (!listAnh.Contains(sFile))
                    {
                        listAnh.Add(sFile);
                        string sFtpName = CreateFtp(sFile);
                        cboHinhAnh.Items.Add(Path.GetFileName(sFtpName));

                        KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                        objKcbKetquaHa.IdChiTietChiDinh = id_Study_Detail;
                        objKcbKetquaHa.Chonin = Convert.ToByte(AutoCheck == "1" ? 1 : 0);
                        objKcbKetquaHa.Vitri = Utility.GetVitriHinhAnhByName(sFile);
                        objKcbKetquaHa.Mota = string.Empty;
                        //objKcbKetquaHa.HinhAnh = imageToByteArray(GetImage(sFile));
                        objKcbKetquaHa.TenAnh = Path.GetFileName(sFtpName);
                        objKcbKetquaHa.DuongdanLocal = sFile;
                        objKcbKetquaHa.IsNew = true;
                        objKcbKetquaHa.Save();
                        string rfile = sFile;
                        if (PropertyLib._HinhAnhProperties.EnabledFTP) rfile = copyImage2Local(-1, sFile, Path.GetFileName(sFtpName));
                        UC_Image objUcImage = new UC_Image(objKcbKetquaHa.Vitri == 0 ? flowLayoutPanel_Phai : flowLayoutPanel_Trai);
                        //objUcImage.LayoutPanel = flowLayoutPanel1;
                        objUcImage.Chonin = AutoCheck == "1";
                        objUcImage.Vitri = Utility.ByteDbnull(objKcbKetquaHa.Vitri);
                        objUcImage.IdHinhAnh = objKcbKetquaHa.Id;
                        objUcImage.ImgData = GetImage(sFile);// objKcbKetquaHa.HinhAnh;
                        objUcImage.Mota = string.Empty;
                        objUcImage.Tag = rfile;
                        objUcImage.DuongdanLocal = rfile;
                        objUcImage.ftpPath = sFtpName;
                        objUcImage.VisibleControl(true);
                        objUcImage._OnChonIn += objUcImage__OnChonIn;
                        objUcImage._OnDelete += objUcImage__OnDelete;
                        objUcImage._OnClick += objUcImage__OnClick;

                        if (File.Exists(Application.StartupPath + @"\auto.kut"))
                        {
                            AutoC(objUcImage);
                        }
                        if (objUcImage.Vitri == 0)
                            flowLayoutPanel_Phai.Controls.Add(objUcImage);
                        else
                            flowLayoutPanel_Trai.Controls.Add(objUcImage);
                        if (AutoCheck == "1") objUcImage__OnChonIn(objUcImage);
                    }
                }
        }
        void AutoC(UC_Image obj)
        {
            frmXemHA frmXemanh = new frmXemHA(obj.DuongdanLocal);
            try
            {

                frmXemanh.SetImg((System.Drawing.Image)obj.PIC_Image.Image.Clone());
                frmXemanh.Opacity = 0;
                frmXemanh._log = this._log;
                frmXemanh.Show();
                frmXemanh.AutoC(false);
                if (frmXemanh.hasChanged)
                {
                    //Update to ftp
                    string ftpName = obj.ftpPath;
                    FtpClient.Upload(obj.DuongdanLocal, ftpName);
                    obj.RefreshMe();
                }
            }
            catch (Exception ex)
            {
                frmXemanh.Close();
                frmXemanh.Dispose();
                frmXemanh = null;
                log.Trace(ex.ToString());
            }
            finally
            {

            }

        }
        void frm_NhaptraKQ_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        public void ReloadAfterFinish(bool AutoLoad, long ID, List<string> listNew, List<string> listAnh, List<string> listsFTPName, List<long> listIdHinhAnh, List<string> listNewRadio)
        {
            if (ID != ID_Study_Detail) return;
            this.Text = "Đang nạp các ảnh mới chụp...";
            this.Cursor = Cursors.WaitCursor;
            string oldText = this.Text;
            try
            {
                int idx = 0;
                foreach (string filename in listNewRadio)
                {

                    string sFtpName = listsFTPName[idx];
                    string s = string.Format("Đang nạp ảnh mới chụp {0}/{1} :{2}", (idx + 1).ToString(), listNewRadio.Count.ToString(), filename);
                    log.Trace(s);
                    this.Text = s;
                    Application.DoEvents();

                    UC_Image objUcImage = new UC_Image(flowLayoutPanel_Phai);
                    objUcImage.Chonin = false;
                    objUcImage.IdHinhAnh = listIdHinhAnh[idx];
                    objUcImage.ImgData = GetImage(filename);// objKcbKetquaHa.HinhAnh;
                    objUcImage.Mota = string.Empty;
                    objUcImage.Tag = filename;
                    objUcImage.DuongdanLocal = filename;
                    objUcImage.VisibleControl(true);
                    objUcImage._OnChonIn += objUcImage__OnChonIn;
                    objUcImage._OnDelete += objUcImage__OnDelete;
                    objUcImage._OnClick += objUcImage__OnClick;
                    if (AutoLoad)
                    {
                        if (!cboHinhAnh.Items.Contains(sFtpName))
                            cboHinhAnh.Items.Add(sFtpName);
                        AddControl(objUcImage);
                    }
                    // flowLayoutPanel1.Controls.Add(objUcImage);
                    Application.DoEvents();
                    idx += 1;
                }

            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Text = oldText;
            }
        }
        delegate void _AddControl(Control _child);
        public void AddControl(Control _child)
        {
            try
            {
                if (flowLayoutPanel_Phai.InvokeRequired)
                {
                    flowLayoutPanel_Phai.BeginInvoke(new _AddControl(AddControl), new object[] { _child });
                }
                else
                {
                    flowLayoutPanel_Phai.Controls.Add(_child);
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.ToString());
            }
        }
        void _FtpManager__Onfinish(int IdChiTietChiDinh, List<string> listNew, List<string> listAnh, List<string> listsFTPName, List<int> listIdHinhAnh, List<string> listNewRadio)
        {
            this.Text = "";
            //if (IdChiTietChiDinh == this.IdChiTietChiDinh)
            //    ReloadAfterFinish(IdChiTietChiDinh, listNew, listAnh, listsFTPName, listIdHinhAnh, listNewRadio);
        }
        public string RML = "3";
        void frm_NhaptraKQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString() });
                SaveUserConfigs();
                this.Text = "try freeing memory...";
                Application.DoEvents();
                Util.ReleaseMemory(pnlImgs, RML);
                Util.ReleaseMemory(flowLayoutPanel_Phai, RML);
                cboHinhAnh.Items.Clear();
                listAnh.Clear();
                flowLayoutPanel_Phai.Controls.Clear();
                pnlImgs.Controls.Clear();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
        }

        void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._HinhAnhProperties.PrintPreview = chkPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }
        void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._HinhAnhProperties.TenmayInPhieutraKQ = cboLaserPrinters.Text;
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }
        void LoadLaserPrinters()
        {
            try
            {
                //khoi tao may in
                String pkInstalledPrinters;
                cboLaserPrinters.Items.Clear();
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cboLaserPrinters.Items.Add(pkInstalledPrinters);
                }
                if (PropertyLib._HinhAnhProperties.TenmayInPhieutraKQ.TrimEnd().TrimStart() != "")
                    cboLaserPrinters.Text = PropertyLib._HinhAnhProperties.TenmayInPhieutraKQ;
            }
            catch
            {
            }
            finally
            {
            }
        }
        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        DataTable m_dthinhanh = new DataTable();
        private int idx = 1;
        void loadHinhAnh(bool All)
        {
            try
            {
                SqlQuery sqlQuery = null;
                if (All)
                    sqlQuery = new Select().From<KcbKetquaHa>()
                       .Where(KcbKetquaHa.Columns.IdChiTietChiDinh)
                       .IsEqualTo(Utility.Int32Dbnull(ID_Study_Detail))
                       .OrderAsc("Sttin", "Id");
                else
                    sqlQuery = new Select().From<KcbKetquaHa>()
                   .Where(KcbKetquaHa.Columns.IdChiTietChiDinh).IsEqualTo(Utility.Int32Dbnull(ID_Study_Detail))
                   .And(KcbKetquaHa.Columns.Chonin).IsEqualTo(1)
                   .OrderAsc("Sttin", "Id");
                m_dthinhanh = sqlQuery.ExecuteDataSet().Tables[0];
                Utility.AddColumToDataTable(ref m_dthinhanh, "STT", typeof(Int32));
                DataTable dtPhai = m_dthinhanh.Clone();
                DataTable dtTrai = m_dthinhanh.Clone();
                DataRow[] arrDr = m_dthinhanh.Select("vitri=0");
                if (arrDr.Length > 0) dtPhai = arrDr.CopyToDataTable();
                arrDr = m_dthinhanh.Select("vitri=1");
                if (arrDr.Length > 0) dtTrai = arrDr.CopyToDataTable();
                Try2ReleaseImgs();
                loadHinhAnh(flowLayoutPanel_Phai, dtPhai, 0);
                loadHinhAnh(flowLayoutPanel_Trai, dtTrai, 1);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        void Try2ReleaseImgs()
        {
            try
            {
                Util.ReleaseMemory(pnlImgs, RML);
                Util.ReleaseMemory(flowLayoutPanel_Phai, RML);
                Util.ReleaseMemory(flowLayoutPanel_Trai, RML);
                listAnh.Clear();
                flowLayoutPanel_Phai.Controls.Clear();
                flowLayoutPanel_Trai.Controls.Clear();
                pnlImgs.Controls.Clear();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void loadHinhAnh(FlowLayoutPanel FL_Hinhanh, DataTable dt_hinhanh, byte vitri)
        {
            this.Cursor = Cursors.WaitCursor;
            cmdChonlai.Enabled = false;
            GC.Collect();
            string oldTitle = this.Text;
            try
            {
                //Util.ReleaseMemory(pnlImgs, RML);
                //Util.ReleaseMemory(FL_Hinhanh, RML);

                //listAnh.Clear();
                //FL_Hinhanh.Controls.Clear();
                //pnlImgs.Controls.Clear();

                foreach (DataRow dr in dt_hinhanh.Rows)
                {
                    string sNameImage = dr["TenAnh"].ToString();
                    if (!Directory.Exists(_baseDirectory + ID_Study_Detail))
                        Directory.CreateDirectory(_baseDirectory + ID_Study_Detail);
                    if (PropertyLib._HinhAnhProperties.EnabledFTP)
                    {
                        string ftpPath = FtpClientCurrentDirectory + ID_Study_Detail + "/" + sNameImage;
                        if (FtpClient.FtpFileExists(ftpPath))
                        {
                            if (!File.Exists(_baseDirectory + ID_Study_Detail + "\\" + sNameImage))
                            {
                                string s = string.Format("Đang nạp ảnh từ máy chủ FTP: {0}", _baseDirectory + ID_Study_Detail + "\\" + sNameImage);
                                this.Text = s;
                                FtpClient.Download(ftpPath, _baseDirectory + ID_Study_Detail + "\\" + sNameImage, true);
                            }
                            listAnh.Add(_baseDirectory + ID_Study_Detail + "\\" + sNameImage);
                            cboHinhAnh.Items.Add(sNameImage);

                            UC_Image objUcImage = new UC_Image(FL_Hinhanh);
                            // objUcImage.LayoutPanel = flowLayoutPanel1;
                            objUcImage.Chonin = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Chonin]) == 1;
                            objUcImage.Vitri = vitri;
                            objUcImage.STT = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Sttin]);
                            objUcImage.IdHinhAnh = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Id]);
                            objUcImage.ImgData = GetImage(_baseDirectory + ID_Study_Detail + "\\" + sNameImage);// fromimagepath2byte(dr[KcbKetquaHa.Columns.DuongdanLocal].ToString());// (byte[])dr[KcbKetquaHa.Columns.HinhAnh];
                            objUcImage.Tag = _baseDirectory + ID_Study_Detail + "\\" + sNameImage;
                            objUcImage.DuongdanLocal = objUcImage.Tag.ToString();
                            objUcImage.ftpPath = ftpPath;
                            objUcImage.Mota = Utility.sDbnull(dr[KcbKetquaHa.Columns.Mota]);
                            objUcImage.VisibleControl(true);
                            objUcImage._OnChonIn += objUcImage__OnChonIn;
                            objUcImage._OnDelete += objUcImage__OnDelete;
                            objUcImage._OnClick += objUcImage__OnClick;
                            objUcImage._OnChangeMota += objUcImage__OnChangeMota;
                            FL_Hinhanh.Controls.Add(objUcImage);
                            if (objUcImage.Chonin)
                            {
                                UC_Image objPrintedImg = objUcImage.Copy(pnlImgs, true);
                                objPrintedImg.Vitri = vitri;
                                objPrintedImg._OnDelete += objPrintedImg__OnDelete;
                                objPrintedImg._OnChonIn += objPrintedImg__OnChonIn;
                                objPrintedImg._OnClick += objUcImage__OnClick;
                                objPrintedImg.Tag = _baseDirectory + ID_Study_Detail + "\\" + sNameImage;
                                objPrintedImg.DuongdanLocal = objPrintedImg.Tag.ToString();
                                objPrintedImg.ftpPath = ftpPath;
                                objPrintedImg._OnChangeMota += objPrintedImg__OnChangeMota;
                                pnlImgs.Controls.Add(objPrintedImg);
                            }
                            dr["STT"] = idx++;
                        }

                    }
                }
                dt_hinhanh.AcceptChanges();
            }
            catch (Exception ex1)
            {
                // Utility.ShowMsg(ex1.Message);

            }
            finally
            {
                cmdChonlai.Enabled = true;
                cmdSend2PACS.Enabled = pnlImgs.Controls.Count > 0;
                this.Cursor = Cursors.Default;
                this.Text = oldTitle;
            }
        }
        void loadHinhAnh_bak(bool All)
        {
            this.Cursor = Cursors.WaitCursor;
            cmdChonlai.Enabled = false;
            GC.Collect();
            string oldTitle = this.Text;
            try
            {
                Util.ReleaseMemory(pnlImgs, RML);
                Util.ReleaseMemory(flowLayoutPanel_Phai, RML);

                listAnh.Clear();
                flowLayoutPanel_Phai.Controls.Clear();
                pnlImgs.Controls.Clear();
                SqlQuery sqlQuery = null;
                if (All)
                    sqlQuery = new Select().From<KcbKetquaHa>()
                       .Where(KcbKetquaHa.Columns.IdChiTietChiDinh)
                       .IsEqualTo(Utility.Int32Dbnull(ID_Study_Detail))
                       .OrderAsc("Sttin", "Id");
                else
                    sqlQuery = new Select().From<KcbKetquaHa>()
                   .Where(KcbKetquaHa.Columns.IdChiTietChiDinh).IsEqualTo(Utility.Int32Dbnull(ID_Study_Detail))
                   .And(KcbKetquaHa.Columns.Chonin).IsEqualTo(1)
                   .OrderAsc("Sttin", "Id");
                m_dthinhanh = sqlQuery.ExecuteDataSet().Tables[0];
                Utility.AddColumToDataTable(ref m_dthinhanh, "STT", typeof(Int32));
                foreach (DataRow dr in m_dthinhanh.Rows)
                {
                    string sNameImage = dr["TenAnh"].ToString();
                    if (!Directory.Exists(_baseDirectory + ID_Study_Detail))
                        Directory.CreateDirectory(_baseDirectory + ID_Study_Detail);
                    if (PropertyLib._HinhAnhProperties.EnabledFTP)
                    {
                        string ftpPath = FtpClientCurrentDirectory + ID_Study_Detail + "/" + sNameImage;
                        if (FtpClient.FtpFileExists(ftpPath))
                        {
                            if (!File.Exists(_baseDirectory + ID_Study_Detail + "\\" + sNameImage))
                            {
                                string s = string.Format("Đang nạp ảnh từ máy chủ FTP: {0}", _baseDirectory + ID_Study_Detail + "\\" + sNameImage);
                                this.Text = s;
                                FtpClient.Download(ftpPath, _baseDirectory + ID_Study_Detail + "\\" + sNameImage, true);
                            }
                            listAnh.Add(_baseDirectory + ID_Study_Detail + "\\" + sNameImage);
                            cboHinhAnh.Items.Add(sNameImage);

                            UC_Image objUcImage = new UC_Image(flowLayoutPanel_Phai);
                            // objUcImage.LayoutPanel = flowLayoutPanel1;
                            objUcImage.Chonin = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Chonin]) == 1;
                            objUcImage.STT = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Sttin]);
                            objUcImage.IdHinhAnh = Utility.Int32Dbnull(dr[KcbKetquaHa.Columns.Id]);
                            objUcImage.ImgData = GetImage(_baseDirectory + ID_Study_Detail + "\\" + sNameImage);// fromimagepath2byte(dr[KcbKetquaHa.Columns.DuongdanLocal].ToString());// (byte[])dr[KcbKetquaHa.Columns.HinhAnh];
                            objUcImage.Tag = _baseDirectory + ID_Study_Detail + "\\" + sNameImage;
                            objUcImage.DuongdanLocal = objUcImage.Tag.ToString();
                            objUcImage.ftpPath = ftpPath;
                            objUcImage.Mota = Utility.sDbnull(dr[KcbKetquaHa.Columns.Mota]);
                            objUcImage.VisibleControl(true);
                            objUcImage._OnChonIn += objUcImage__OnChonIn;
                            objUcImage._OnDelete += objUcImage__OnDelete;
                            objUcImage._OnClick += objUcImage__OnClick;
                            objUcImage._OnChangeMota += objUcImage__OnChangeMota;
                            flowLayoutPanel_Phai.Controls.Add(objUcImage);
                            if (objUcImage.Chonin)
                            {
                                UC_Image objPrintedImg = objUcImage.Copy(pnlImgs, true);
                                objPrintedImg._OnDelete += objPrintedImg__OnDelete;
                                objPrintedImg._OnChonIn += objPrintedImg__OnChonIn;
                                objPrintedImg._OnClick += objUcImage__OnClick;
                                objPrintedImg.Tag = _baseDirectory + ID_Study_Detail + "\\" + sNameImage;
                                objPrintedImg.DuongdanLocal = objPrintedImg.Tag.ToString();
                                objPrintedImg.ftpPath = ftpPath;
                                objPrintedImg._OnChangeMota += objPrintedImg__OnChangeMota;
                                pnlImgs.Controls.Add(objPrintedImg);
                            }
                            dr["STT"] = idx++;
                        }

                    }
                }
                m_dthinhanh.AcceptChanges();
            }
            catch (Exception ex1)
            {
                // Utility.ShowMsg(ex1.Message);

            }
            finally
            {
                cmdChonlai.Enabled = true;
                cmdSend2PACS.Enabled = pnlImgs.Controls.Count > 0;
                this.Cursor = Cursors.Default;
                this.Text = oldTitle;
            }
        }

        void objPrintedImg__OnChangeMota(UC_Image obj)
        {
            UC_Image findCtrl = null;
            foreach (UC_Image ctr in flowLayoutPanel_Phai.Controls)
            {
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            if (findCtrl != null) findCtrl.txtMota.Text = obj.txtMota.Text;
        }

        void objUcImage__OnChangeMota(UC_Image obj)
        {
            UC_Image findCtrl = null;
            foreach (UC_Image ctr in pnlImgs.Controls)
            {
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            if (findCtrl != null) findCtrl.txtMota.Text = obj.txtMota.Text;
        }
        void objPrintedImg__OnChonIn(UC_Image obj)
        {
            UC_Image findCtrl = null;
            foreach (UC_Image ctr in flowLayoutPanel_Phai.Controls)
            {
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            pnlImgs.Controls.Remove(obj);
            if (findCtrl != null)
            {
                findCtrl.AllowRaiseEvt = false;
                findCtrl.chkChonAnhIn.Checked = false;
                findCtrl.Chonin = findCtrl.chkChonAnhIn.Checked;
                findCtrl.AllowRaiseEvt = true;
                findCtrl.UpdateSTT(1, false);
            }
            cmdSend2PACS.Enabled = pnlImgs.Controls.Count > 0;
        }

        void objPrintedImg__OnDelete(UC_Image obj)
        {
            UC_Image findCtrl = null;
            foreach (UC_Image ctr in flowLayoutPanel_Phai.Controls)
            {
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            if (findCtrl != null) flowLayoutPanel_Phai.Controls.Remove(findCtrl);
            pnlImgs.Controls.Remove(obj);
            DeleteSelectedImg(obj);
        }

        void objUcImage__OnDelete(UC_Image obj)
        {
            UC_Image findCtrl = null;
            foreach (UC_Image ctr in pnlImgs.Controls)
            {
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            if (findCtrl != null) pnlImgs.Controls.Remove(findCtrl);
            flowLayoutPanel_Phai.Controls.Remove(obj);
            DeleteSelectedImg(obj);//Path.GetFileName( obj.Tag.ToString()));

        }
        void DeleteSelectedImg(UC_Image obj)
        {
            string oltTitle = this.Text;
            this.Cursor = Cursors.WaitCursor;
            string s = string.Format("Đang xóa ảnh: {0}", obj.Tag.ToString());
            this.Text = s;
            new Delete().From(KcbKetquaHa.Schema)
                           .Where(KcbKetquaHa.Columns.Id).IsEqualTo(obj.IdHinhAnh).Execute();

            if (PropertyLib._HinhAnhProperties.EnabledFTP)
            {
                if (FtpClient.FtpFileExists(FtpClientCurrentDirectory + ID_Study_Detail + "/" + Path.GetFileName(obj.Tag.ToString())))
                {

                    FtpClient.FtpDelete(FtpClientCurrentDirectory + ID_Study_Detail + "/" + Path.GetFileName(obj.Tag.ToString()));
                }
            }
            if (File.Exists(_baseDirectory + ID_Study_Detail + "\\" + Path.GetFileName(obj.Tag.ToString())))
                File.Delete(_baseDirectory + ID_Study_Detail + "\\" + Path.GetFileName(obj.Tag.ToString()));

            int idx = -1;
            for (int i = 0; i < cboHinhAnh.Items.Count - 1; i++)
            {
                if (cboHinhAnh.Items[i].ToString().ToUpper().TrimStart().TrimEnd() == Path.GetFileName(obj.Tag.ToString()))
                {
                    idx = i;
                    break;
                }
            }
            if (idx >= 0)
            {
                cboHinhAnh.Items.RemoveAt(idx);
                listAnh.RemoveAt(idx);
            }
            this.Cursor = Cursors.Default;
            this.Text = oltTitle;
        }
        void objUcImage__OnClick(UC_Image obj)
        {
            frmXemHA frmXemanh = new frmXemHA(obj.DuongdanLocal);
            frmXemanh._OnCut += frmXemanh__OnCut;
            frmXemanh._log = this._log;
            frmXemanh.SetImg((System.Drawing.Image)obj.PIC_Image.Image.Clone());
            frmXemanh.ShowDialog();
            if (frmXemanh.hasChanged)
            {
                //Update to ftp
                string ftpName = obj.ftpPath;// string.Format(@"//{0}/{1}", Path.GetFileName(Path.GetDirectoryName(obj.DuongdanLocal)), Path.GetFileName(obj.DuongdanLocal));
                FtpClient.Upload(obj.DuongdanLocal, ftpName);
                obj.RefreshMe();
            }
        }

        void frmXemanh__OnCut(Rectangle r)
        {
            //PropertyLib._HinhAnhProperties.CropRec = r;
            //PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }

        void objUcImage__OnChonIn(UC_Image obj)
        {
            bool exist = false;
            UC_Image findCtrl = null;
            bool _rearrange = false;
            foreach (UC_Image ctr in pnlImgs.Controls)
            {
                if (!_rearrange) { ctr.RearrangeControls(); _rearrange = true; }
                if (obj.IdHinhAnh == ctr.IdHinhAnh)
                {
                    findCtrl = ctr;
                    break;
                }
            }
            if (!obj.Chonin && findCtrl != null)
            {
                pnlImgs.Controls.Remove(findCtrl);
                findCtrl.UpdateSTT(1, false);
            }
            if (findCtrl == null)
            {
                UC_Image objPrintedImg = obj.Copy(pnlImgs, true);
                objPrintedImg._OnDelete += objPrintedImg__OnDelete;
                objPrintedImg._OnChonIn += objPrintedImg__OnChonIn;
                objPrintedImg.STT = pnlImgs.Controls.Count + 1;
                pnlImgs.Controls.Add(objPrintedImg);
                objPrintedImg.UpdateSTT(pnlImgs.Controls.Count, true);
                obj.STT = objPrintedImg.STT;
                pnlImgs.ScrollControlIntoView(objPrintedImg);
            }
            cmdSend2PACS.Enabled = pnlImgs.Controls.Count > 0;
        }
        public List<string> listAnh = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        void Autosave2Ftp(List<string> listNew)
        {
            this.Cursor = Cursors.WaitCursor;
            string oldText = this.Text;
            try
            {
                string AutoCheck = Utility.Laygiatrithamsohethong("AutoCheckImage", "0", true);
                int idx = 0;
                foreach (string filename in listNew)
                {
                    idx += 1;
                    string s = string.Format("Đang lưu {0}/{1} :{2} lên máy chủ FTP", idx.ToString(), listNew.Count.ToString(), filename);
                    this.Text = s;
                    Application.DoEvents();
                    if (!listAnh.Contains(filename))
                    {
                        listAnh.Add(filename);
                        string sFtpName = CreateFtp(filename);
                        cboHinhAnh.Items.Add(Path.GetFileName(sFtpName));
                        KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                        objKcbKetquaHa.IdChiTietChiDinh = ID_Study_Detail;
                        objKcbKetquaHa.Chonin = Convert.ToByte(AutoCheck == "1" ? 1 : 0);
                        objKcbKetquaHa.Mota = "";
                        objKcbKetquaHa.Vitri = Utility.GetVitriHinhAnhByName(filename);
                        objKcbKetquaHa.TenAnh = Path.GetFileName(sFtpName);
                        objKcbKetquaHa.DuongdanLocal = filename;
                        objKcbKetquaHa.Save();
                        string rfile = filename;
                        if (PropertyLib._HinhAnhProperties.EnabledFTP) rfile = copyImage2Local(-1, filename, Path.GetFileName(sFtpName));
                        UC_Image objUcImage = new UC_Image(objKcbKetquaHa.Vitri == 0 ? flowLayoutPanel_Phai : flowLayoutPanel_Trai);
                        objUcImage.Chonin = AutoCheck == "1";
                        objUcImage.IdHinhAnh = objKcbKetquaHa.Id;
                        objUcImage.ImgData = GetImage(filename);
                        objUcImage.Mota = string.Empty;
                        objUcImage.Vitri = Utility.ByteDbnull(objKcbKetquaHa.Vitri);
                        objUcImage.Tag = rfile;
                        objUcImage.DuongdanLocal = rfile;
                        objUcImage.ftpPath = sFtpName;
                        objUcImage.VisibleControl(true);
                        objUcImage._OnChonIn += objUcImage__OnChonIn;
                        objUcImage._OnDelete += objUcImage__OnDelete;
                        objUcImage._OnClick += objUcImage__OnClick;
                        if (objUcImage.Vitri == 0)
                            flowLayoutPanel_Phai.Controls.Add(objUcImage);
                        else
                            flowLayoutPanel_Trai.Controls.Add(objUcImage);
                        if (AutoCheck == "1") objUcImage__OnChonIn(objUcImage);
                        Application.DoEvents();

                    }
                }
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Text = oldText;
            }

        }
        void SaveImg2Ftp()
        {
            try
            {
                string AutoCheck = Utility.Laygiatrithamsohethong("AutoCheckImage", "0", true);
                for (int i = 0; i < cboHinhAnh.Items.Count; i++)
                {
                    if (m_dthinhanh.Select("TenAnh = '" + cboHinhAnh.Items[i] + "'").Length == 0)
                    {
                        string pathImage = CreateFtp(listAnh[i]);
                        if (pathImage != null)
                        {
                            cboHinhAnh.Items[i] = pathImage;
                            KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                            objKcbKetquaHa.IdChiTietChiDinh = ID_Study_Detail;
                            objKcbKetquaHa.Chonin = Convert.ToByte(AutoCheck == "1" ? 1 : 0);
                            objKcbKetquaHa.Mota = "";
                            objKcbKetquaHa.TenAnh = Utility.sDbnull(pathImage);
                            objKcbKetquaHa.DuongdanLocal = listAnh[i];
                            objKcbKetquaHa.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }
        string copyImage2Local(int idx, string localimage, string ftpimage)
        {
            string radiofile = "";
            try
            {
                if (!Directory.Exists(_baseDirectory + id_Study_Detail.ToString()))
                    Directory.CreateDirectory(_baseDirectory + id_Study_Detail.ToString());
                if (File.Exists(localimage))
                {
                    radiofile = _baseDirectory + id_Study_Detail.ToString() + "\\" + ftpimage;
                    File.Copy(localimage, radiofile);

                }
                return radiofile;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return "";
            }
        }
        string videotype = "0";
        string[] lstframeInfor = new List<string>().ToArray();
        string numofimg = "4";
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        void cmdChupAnh_Click(object sender, EventArgs e)
        {

            if (!AllowCapture)
            {
                // Utility.ShowMsg("Capture is not licensed on this computer");
                return;
            }
            if (id_Study_Detail > 0)
            {
                frm_ChupAnh frm = new frm_ChupAnh();
                frm.AssginDetail_ID = id_Study_Detail;
                frm.m_dthinhanh = m_dthinhanh;
                frm.filePrefix = string.Format("{0}_{1}_{2}_{3}", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, id_Study_Detail.ToString());
                frm.ShowDialog();
                if (!frm.iscancel)
                {
                    frm.ExitCaptureTest();
                    Autosave2Ftp(frm.lstImgs);
                    if (frm.lstImgs.Count > 0)
                    {
                        Application.DoEvents();
                    }
                }
            }
            else
            {
                Utility.ShowMsg("Bạn cần chọn Bệnh nhân trước khi thực hiện chụp");
            }
        }



        #endregion

        #region Method
        public int ID_Study_Detail
        {
            get { return id_Study_Detail; }
            set { id_Study_Detail = value; }
        }



        private void GetDataBN()
        {
            System.Data.DataSet dsBN = SPs.HinhanhLaydulieuinKQChandoan(ID_Study_Detail).GetDataSet();
            _dtThongTinBenhNhan = dsBN.Tables[0];
            m_dthinhanh = dsBN.Tables[1];
            try
            {
                _dtThongTinBenhNhan.Columns.Remove("PhongThucHien");
            }
            catch (Exception)
            {

            }
            _dtThongTinBenhNhan.Columns.Add(new DataColumn()
            {
                ColumnName = "PhongThucHien",
                DataType = typeof(String),
                DefaultValue = PropertyLib._HinhAnhProperties.Khoaphongthuchien
            });
        }
        private string CreateFtp(string sourcePath, string newDirName)
        {
            try
            {
                if (!saveDocTemplates) return "";
                log.Trace("Begin Ftp...");
                if (!PropertyLib._HinhAnhProperties.EnabledFTP)
                {
                    return sourcePath;
                }
                string fileName = Path.GetFileName(sourcePath);
                string ftpCurrentDirectory = FtpClientCurrentDirectory + "//" + newDirName;
                if (!FtpClient.FtpDirectoryExists(ftpCurrentDirectory))
                    FtpClient.FtpCreateDirectory(ftpCurrentDirectory);
                fileName = Path.GetFileName(sourcePath);
                string uploadDirectory = string.Format("{0}/{1}", ftpCurrentDirectory, fileName);
                FtpClient.CurrentDirectory = FtpClientCurrentDirectory;
                log.Trace(string.Format("sourcePath={0}uploadDirectory={1}", sourcePath, uploadDirectory));
                FtpClient.Upload(sourcePath, uploadDirectory);
                return fileName;

            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.ToString());
                return "";
            }
        }
        private string CreateFtp(string sourcePath)
        {
            try
            {

                log.Trace("Begin Ftp...");
                if (!PropertyLib._HinhAnhProperties.EnabledFTP)
                {
                    return sourcePath;
                }
                string fileName = "";
                string newDirName = ID_Study_Detail.ToString();
                string ftpCurrentDirectory = FtpClientCurrentDirectory + "//" + newDirName;
                if (!FtpClient.FtpDirectoryExists(ftpCurrentDirectory))
                    FtpClient.FtpCreateDirectory(ftpCurrentDirectory);
                fileName = ID_Study_Detail + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(sourcePath);
                string uploadDirectory = string.Format("{0}/{1}", ftpCurrentDirectory, fileName);
                FtpClient.CurrentDirectory = FtpClientCurrentDirectory;
                log.Trace(string.Format("sourcePath={0}uploadDirectory={1}", sourcePath, uploadDirectory));
                FtpClient.Upload(sourcePath, uploadDirectory);
                return uploadDirectory;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.ToString());
                return "";
            }


        }

        #region HIS

        /// <summary>
        /// Hàm convert Image vào mảng chỉ cần truyền vào là Image
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
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
        Document doc;

        private void InKetQua(string dstFileName)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                if (cboLaserPrinters.SelectedIndex < 0 || cboLaserPrinters.Text.TrimEnd().TrimStart() == "")
                {
                    Utility.ShowMsg("Bạn cần chọn máy in trước khi thực hiện in kết quả");
                    cboLaserPrinters.Focus();
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                SysSystemParameter sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                DataTable dtData = SPs.HinhanhLaydulieuinKQChandoan(id_Study_Detail).GetDataSet().Tables[0];
                try
                {
                    if (dtData.Columns.Contains("PhongThucHien"))
                        dtData.Columns.Remove("PhongThucHien");
                }
                catch (Exception)
                {

                }
                dtData.Columns.Add(new DataColumn()
                {
                    ColumnName = "PhongThucHien",
                    DataType = typeof(String),
                    DefaultValue = PropertyLib._HinhAnhProperties.Khoaphongthuchien
                });
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                if (!dtData.Columns.Contains("Chan_Doan")) dtData.Columns.Add("Chan_Doan", typeof(string));
                byte[] NoImage = imageToByteArray(GetImage(AppDomain.CurrentDomain.BaseDirectory + "Noimage\\Noimage.png"));
                dtData.TableName = "KQHA";
                KcbKetquaHaCollection KcbKetquaHaCollection = new Select().From<KcbKetquaHa>()
                    .Where(KcbKetquaHa.Columns.IdChiTietChiDinh)
                    .IsEqualTo(id_Study_Detail).And(KcbKetquaHa.Columns.Chonin).IsEqualTo(1)
                    .OrderAsc(KcbKetquaHa.Columns.Sttin)
                    .ExecuteAsCollection<KcbKetquaHaCollection>();
                DataRow drData = dtData.Rows[0];

                string ngayin = string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year);
                List<string> Values = new List<string>()
                {
                Utility.sDbnull(drData["id_benhnhan"], ""),
                Utility.sDbnull(drData["ma_luotkham"], ""),
                Utility.sDbnull(drData["ma_yte"], ""),
                Utility.sDbnull(drData["ten_benhnhan"], ""),
                Utility.sDbnull(drData["gioi_tinh"], ""),
                Utility.sDbnull(drData["nam_sinh"], ""),
                Utility.sDbnull(drData["mathe_bhyt"], ""),
                Utility.sDbnull(drData["Tuoi"],""),
                Utility.sDbnull("01/01/1900", ""),//Insurance_FromDate
                Utility.sDbnull("01/01/1900", ""),//Insurance_ToDate
                Utility.sDbnull(drData["ten_doituong_kcb"], ""),
                Utility.sDbnull(drData["ngay_sinh"], ""),
                Utility.sDbnull(drData["bacsi_chidinh"], ""),
                Utility.sDbnull(drData["chan_doan"], ""),
               vks.TenVungkhaosat,
                Utility.sDbnull(drData["phong_chidinh"], ""),
               Utility.FormatDateTimeWithLocation( Utility.sDbnull(drData["ngay_thuchien"], DateTime.Now.ToString("dd/MM/yyyy")),
                            globalVariables.gv_strDiadiem),
                cboBacsi.Text,//Nguoi thuc hien
                PropertyLib._HinhAnhProperties.Khoaphongthuchien,
                Utility.sDbnull(drData["ten_dichvu"], ""),
                Utility.sDbnull(drData["dien_thoai"], ""),
                Utility.sDbnull(drData["dia_chi"], ""),
                Utility.sDbnull(drData["ma_chidinh"], ""),
                 Utility.sDbnull(drData["Khoa"], ""),
                  Utility.sDbnull(drData["Buong"], ""),
                   Utility.sDbnull(drData["Giuong"], ""),
                
                 ngayin,
                globalVariables.gv_strTenNhanvien,
                globalVariables.ParentBranch_Name,
                globalVariables.Branch_Name,
                globalVariables.Branch_Address,
                globalVariables.Branch_Phone,globalVariables.Branch_Hotline,globalVariables.Branch_Fax,
                globalVariables.Branch_Website,
                globalVariables.Branch_Email,
                Utility.sDbnull(drData["ten_phieu"], "")
            };
                //Utility.ShowMsg(globalVariables.Branch_Phone + globalVariables.Branch_Hotline + globalVariables.Branch_Fax);
                List<string> fieldNames = new List<string>()
            {
               "id_benhnhan",
                "ma_luotkham",
                "ma_yte",
                "ten_benhnhan",
                "gioi_tinh",
                "nam_sinh",
                "mathe_bhyt",
                "Tuoi",
                "ngay_batdauBHYT",
                "ngay_ketthucBHYT",
                "ten_doituong_kcb",
                "ngay_sinh",
                "bacsi_chidinh",
                "chan_doan",
               "ten_vungks",
                "phong_kham",
                "ngay_thuchien",
               "nguoi_thuchien",
               "noi_thuchien",
                "ten_dichvu",
                "dien_thoai",
                "dia_chi",
                "ma_chidinh",
                 "Khoa",
                  "Buong",
                   "Giuong",
                
                 "ngay_in",
                "nguoi_in","tendvicaptren","tenbvien","diachibvien","SDT","SDTNong","Fax","Website","Email","ten_phieu"
            };
                //Fill thêm các giá trị cho phần Dynamic Values
                dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(vks.Id, objKcbChidinhclsChitiet.IdChitietchidinh).GetDataSet().Tables[0];
                if (dtDynamicData != null && !CKEditorInput)
                {
                    foreach (DataRow dr in dtDynamicData.Rows)
                    {
                        fieldNames.Add(Utility.sDbnull(dr[DynamicField.Columns.Ma]));
                        Values.Add(Utility.sDbnull(dr[DynamicValue.Columns.Giatri]));
                    }
                }
                else
                {
                    Values.AddRange(new List<string>(){Utility.sDbnull(drData["Ket_luan"], ""),
                Utility.sDbnull(drData["mo_ta"], ""),
                Utility.sDbnull(drData["de_nghi"], "")});
                    fieldNames.AddRange(new List<string>() { "Ket_luan",
                "Mo_ta",
                "de_nghi",});
                }
                int idx = 1;
                List<byte[]> lstPic = new List<byte[]>();
                //foreach (KcbKetquaHa objHinhAnh in KcbKetquaHaCollection)
                foreach (UC_Image objHinhAnh in pnlImgs.Controls)
                {
                    lstPic.Add(Utility.fromimagepath2byte(objHinhAnh.Tag.ToString()));
                    Utility.AddColumToDataTable(ref dtData, "anh" + idx.ToString(), typeof(byte[]));
                    fieldNames.Add("MoTa" + idx.ToString());
                    Values.Add(Utility.sDbnull(objHinhAnh.Mota));
                    idx++;
                }
                string docPath = THU_VIEN_CHUNG.Laygiatrithamsohethong("DOC", true);
                string maubaocaogoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\maukq.doc";
                string maubaocao = string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, docPath, txtTenFileKQ.Text);
                _log.Trace(maubaocao);
                string tempmau = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(tempmau)) Directory.CreateDirectory(tempmau);
                if (!File.Exists(maubaocao))
                {
                    maubaocao = maubaocaogoc;
                }
                //  string maubaocao = Application.StartupPath + @"\Reports\" + docChuan;
                string tenfile = Guid.NewGuid().ToString();
                string fileExt = Path.GetExtension(maubaocao);
                fileExt = fileExt == "" ? ".doc" : fileExt;
                string fileKetqua = string.Format("{0}{1}{2}{3}{4}{5}_{6}", Path.GetDirectoryName(tempmau), Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(maubaocao), "_ketqua_", objBenhnhan.TenBenhnhan, tenfile, fileExt);
                CreateFtp(maubaocao, "DOC_TEMPLATES");
                if ((drData != null) && File.Exists(maubaocao))
                {
                    doc = new Document(maubaocao);

                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file mẫu trả KQ. Liên hệ phòng IT để được trợ giúp", "Thông báo");
                        return;
                    }
                    string noidung = string.Empty;
                    var query = from chidinh in dtData.AsEnumerable()
                                where
                                    Utility.Int32Dbnull(chidinh[KcbChidinhclsChitiet.Columns.IdChitietchidinh]) ==
                                   id_Study_Detail
                                select chidinh;
                    if (query.Any())
                    {
                        var firstrow = query.FirstOrDefault();
                        if (firstrow != null)
                        {
                            noidung = Utility.sDbnull(firstrow["mo_ta"]);

                        }

                    }
                    DocumentBuilder builder = new DocumentBuilder(doc);

                    int width = 150;
                    string contentStr = Utility.sDbnull(drData["mota_html"], "");// webBrowser1.Document.InvokeScript("getValue").ToString();
                    //  objKcbKetquaHa.NoiDung = contentStr;
                    if (builder.MoveToMergeField("Mo_ta"))
                        builder.InsertHtml(contentStr, true);
                    doc.MailMerge.RemoveEmptyParagraphs = true;
                    // builder.MoveToField("Noi_dung");
                    List<string> lstImgSize = new List<string>() { "150x120;150x120;150x120;150x120;150x120;150x120;150x120;150x120" };
                    DmucChung mauKQ = new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("MAUTRAKQ")
                        .And(DmucChung.Columns.Ma).IsEqualTo(cboDoc.Text)
                        .ExecuteSingle<DmucChung>();
                    if (mauKQ != null)
                        lstImgSize = mauKQ.MotaThem.Split(';').ToList<string>();
                    else
                    {
                        if (vks != null && vks.Kichthuocanh != null && vks.Kichthuocanh.TrimStart().TrimEnd().Length > 0)
                            lstImgSize = vks.Kichthuocanh.Split(';').ToList<string>();
                        else
                            if (File.Exists(Application.StartupPath + @"\imgsize.txt"))
                                lstImgSize = File.ReadAllText(Application.StartupPath + @"\imgsize.txt").ToLower().Split(';').ToList<string>();
                    }
                    string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", objNhanvien != null ? objNhanvien.UserName : globalVariables.UserName);
                    if (PropertyLib._HinhAnhProperties.SignType || !File.Exists(_signFile))
                        _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", "sign");
                    byte[] _sign = Utility.fromimagepath2byte(_signFile);
                    if (builder.MoveToMergeField("sign"))
                        if (_sign != null)
                        {
                            if (sysSignsize != null)
                            {
                                int w = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[0], 0);
                                int h = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[1], 0);
                                if (w > 0 && h > 0)
                                    builder.InsertImage(_sign, w, h);
                                else
                                    builder.InsertImage(_sign);
                            }
                            else
                                if (_sign != null)
                                    builder.InsertImage(_sign);
                        }
                        else
                            builder.InsertImage(NoImage, 10, 10);

                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);

                    if (builder.MoveToBookmark("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);

                    //if (!File.Exists(Application.StartupPath + @"\bookmark.txt"))
                    //{
                    for (int i = 0; i <= lstImgSize.Count - 1; i++)
                    {
                        string[] size = (width.ToString() + "x" + Height.ToString()).Split('x');
                        if (lstImgSize.Count - 1 >= i)
                            size = lstImgSize[i].Split('x');
                        int w = (int)width;
                        if (size.Count() > 1) w = Utility.Int32Dbnull(size[0], width);
                        int h = (int)Height;
                        if (size.Count() > 1) h = Utility.Int32Dbnull(size[1], Height);

                        if (builder.MoveToMergeField("anh" + (i + 1).ToString()))
                        {
                            if (lstPic.Count - 1 >= i)
                                builder.InsertImage(lstPic[i], w, h);
                            else
                                builder.InsertImage(new List<byte>().ToArray(), 10, 10);
                        }
                        else
                        {
                            if (builder.MoveToMergeField("anh" + (i + 1).ToString()))
                                builder.InsertImage(NoImage, 10, 10);
                        }
                    }
                    //}
                    //else
                    //{
                    //    for (int i = 0; i <= lstImgSize.Count - 1; i++)
                    //    {
                    //        string[] size = (width.ToString() + "x" + Height.ToString()).Split('x');
                    //        if (lstImgSize.Count - 1 >= i)
                    //            size = lstImgSize[i].Split('x');
                    //        int w = (int)width;
                    //        if (size.Count() > 1) w = Utility.Int32Dbnull(size[0], width);
                    //        int h = (int)Height;
                    //        if (size.Count() > 1) h = Utility.Int32Dbnull(size[1], Height);

                    //        if (builder.MoveToBookmark("anh" + (i + 1).ToString()))
                    //            if (lstPic.Count - 1 >= i)
                    //                builder.InsertImage(lstPic[i], w, h);
                    //            else
                    //                builder.InsertImage(new List<byte>().ToArray(), 10, 10);

                    //        else
                    //        {
                    //            if (builder.MoveToBookmark("anh" + (i + 1).ToString()))
                    //                builder.InsertImage(NoImage, 10, 10);
                    //        }
                    //    }
                    //}
                    doc.MailMerge.Execute(fieldNames.ToArray(), Values.ToArray());
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    //Save to Pdf
                    //Save to Pdf
                    string newDocfile = string.Format("{0}{1}{2}.doc", Path.GetDirectoryName(fileKetqua), Path.DirectorySeparatorChar, THU_VIEN_CHUNG.GetGUID());
                    File.Copy(fileKetqua, newDocfile);
                    Pdf2HisItem newItem = new Pdf2HisItem(objChidinh.NgayChidinh.ToString("yyyy_MM_dd"), objKcbChidinhclsChitiet.IdChitietchidinh.ToString(), FtpClientPDF, _baseDirectoryPdf, doc, objChidinh.MaChidinh, newDocfile, FtpClientCurrentDirectoryPdf);
                    _Pdf2HisManager.AddItems(newItem);
                    string path = fileKetqua;
                    if (chkPreview.Checked)
                    {
                        if (File.Exists(path))
                        {
                            Process process = new Process();
                            try
                            {
                                process.StartInfo.FileName = path;
                                process.Start();
                                process.WaitForInputIdle();
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        PrinterSettings printerSettings = new PrinterSettings();
                        printerSettings.DefaultPageSettings.Margins.Top = 0;
                        printerSettings.Copies = 1;
                        printerSettings.PrinterName = PropertyLib._HinhAnhProperties.TenmayInPhieutraKQ;
                        doc.Print(printerSettings, fileKetqua);
                        //doc.Print()
                    }
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("In kết quả CĐHA cho bệnh nhân ID={0}, PID={1}, Tên={2} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Print, this.GetType().Assembly.ManifestModule.Name);
                }
                else
                {
                    MessageBox.Show(string.Format("Không tìm thấy biểu mẫu {0}", maubaocao), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
        #endregion

        #region Events
        static void KillProcess(string appName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(appName);
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }
        void setReadOnly()
        {
            if (_IsReadonly)
            {
                cmdChonfile.Enabled = cmdChonvungKS.Enabled = cmdChupAnh.Enabled = cmdChange.Enabled = cmdConfig.Enabled = cmdSave.Enabled = cmdSend2PACS.Enabled = cmdBrowse.Enabled = lnkMore.Enabled = false;
            }
        }
        void LoadUserConfigs()
        {
            try
            {
                chkInsauluu.Checked = Utility.getUserConfigValue(chkInsauluu.Tag.ToString(), Utility.Bool2byte(chkInsauluu.Checked)) == 1;
                chkPreview.Checked = Utility.getUserConfigValue(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked)) == 1;
                chkSignType.Checked = Utility.getUserConfigValue(chkSignType.Tag.ToString(), Utility.Bool2byte(chkSignType.Checked)) == 1;
                chkXemdonthuocTatca.Checked = Utility.getUserConfigValue(chkXemdonthuocTatca.Tag.ToString(), Utility.Bool2byte(chkXemdonthuocTatca.Checked)) == 1;
                chkXemKQXNTatca.Checked = Utility.getUserConfigValue(chkXemKQXNTatca.Tag.ToString(), Utility.Bool2byte(chkXemKQXNTatca.Checked)) == 1;
                chkXemlichsuKQXN.Checked = Utility.getUserConfigValue(chkXemlichsuKQXN.Tag.ToString(), Utility.Bool2byte(chkXemlichsuKQXN.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkInsauluu.Tag.ToString(), Utility.Bool2byte(chkInsauluu.Checked));
                Utility.SaveUserConfig(chkPreview.Tag.ToString(), Utility.Bool2byte(chkPreview.Checked));
                Utility.SaveUserConfig(chkSignType.Tag.ToString(), Utility.Bool2byte(chkSignType.Checked));
                Utility.SaveUserConfig(chkXemdonthuocTatca.Tag.ToString(), Utility.Bool2byte(chkXemdonthuocTatca.Checked));
                Utility.SaveUserConfig(chkXemKQXNTatca.Tag.ToString(), Utility.Bool2byte(chkXemKQXNTatca.Checked));
                Utility.SaveUserConfig(chkXemlichsuKQXN.Tag.ToString(), Utility.Bool2byte(chkXemlichsuKQXN.Checked));
            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public List<string> lstID = new List<string>();
        DmucVungkhaosat vks = null;
        string docfilepath = Application.StartupPath + @"\CurrentDoc.txt";
        bool AllowSelectionChanged = false;
        string Mota_Org = "";
        string Mota_Current = "";
        bool PhaiTrai = false;

        private void frm_NhaptraKQ_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserConfigs();
                PhaiTrai = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_TACH_PHAITRAI", "1", true) == "1";
                flowLayoutPanel_Trai.Width = PhaiTrai ? 508 : 0;
                ModifyRegionHinhAnh();
                if (!File.Exists(docfilepath)) File.Create(docfilepath);
                List<string> lstDeviceCode = new List<string>();
                if (StrServiceCode.Split('@').Count() >= 2)
                    lstDeviceCode = StrServiceCode.Split('@')[1].Split(',').ToList<string>();
                if (lstDeviceCode.Count <= 0) lstDeviceCode.Add("-1");
                DataTable dtDevice = new Select().From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).IsEqualTo("THIETBI_CDHA")
                    .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                    .And(DmucChung.Columns.Ma).In(lstDeviceCode)
                    .ExecuteDataSet().Tables[0];
                cboDevice.DataSource = dtDevice;
                cboDevice.ValueMember = DmucChung.Columns.Ma;
                cboDevice.DisplayMember = DmucChung.Columns.Ten;
                List<string> lstKieuFilm = new List<string>();
                SysSystemParameter objSysparam = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("KIEU_FILM").ExecuteSingle<SysSystemParameter>();
                if (objSysparam != null && objSysparam.SValue != null && objSysparam.SValue.TrimStart().TrimEnd().Length > 0)
                    lstKieuFilm = objSysparam.SValue.Split(',').ToList<string>();
                cboLoaiFilm.Items.AddRange(lstKieuFilm.ToArray<string>());

                CKEditorInput = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_CKEDITOR", "1", true) == "1";
                InitUI();
                SysSystemParameter objSysParams = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("AUTOCOLLECT_DOCTEMPLATES").ExecuteSingle<SysSystemParameter>();
                saveDocTemplates = objSysParams != null && Utility.sDbnull(objSysParams.SValue, "0") == "1";
                objKcbChidinhclsChitiet = KcbChidinhclsChitiet.FetchByID(ID_Study_Detail);

                if (objKcbChidinhclsChitiet == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin chỉ định. Liên hệ IT bệnh viện để được hỗ trợ");
                    this.Close();
                }
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objKcbChidinhclsChitiet.IdBenhnhan);
                objLuotkham = KcbLuotkham.FetchByID(objKcbChidinhclsChitiet.MaLuotkham);
                objChidinh = KcbChidinhcl.FetchByID(objKcbChidinhclsChitiet.IdChidinh);
                if (objKcbChidinhclsChitiet.TrangThai <= 2)
                {
                    objKcbChidinhclsChitiet.TrangThai = 2;
                    objKcbChidinhclsChitiet.IsNew = false;
                    objKcbChidinhclsChitiet.MarkOld();
                    objKcbChidinhclsChitiet.Save();
                }
                if (objKcbChidinhclsChitiet.TrangThai >= 3)
                    CKEditorInput = Utility.Byte2Bool(objKcbChidinhclsChitiet.ResultType);
                vks = DmucVungkhaosat.FetchByID(id_VungKS);
                txtNoiDung.Text = vks != null ? vks.MotaHtml : "";
                GetDataBN();

                DataTable _dtDoctor = GetAllDoctors();
                cboBacsi.DataSource = _dtDoctor;
                cboBacsi.DisplayMember = DmucNhanvien.Columns.TenNhanvien;
                cboBacsi.ValueMember = DmucNhanvien.Columns.UserName;
                cboBacsi.Enabled = globalVariables.IsAdmin || Utility.Coquyen("cdha_quyen_chonbacsithuchien");
                DataTable dtHistory = SPs.HinhanhTimkiemLichsuCdha(objLuotkham.IdBenhnhan, StrServiceCode).GetDataSet().Tables[0];
                DataRow[] arrDr = dtHistory.Select("id <>" + ID_Study_Detail);
                DataTable dtHistory_1 = dtHistory.Clone();
                if (arrDr.Length > 0)
                    dtHistory_1 = arrDr.CopyToDataTable();
                DataBinding.BindDataCombobox(cboDichvu, dtHistory_1, "id", "ten", "------Chọn dịch vụ cần so sánh kết quả------", false);
                cboDichvu.Enabled = cmdShowHistory.Enabled =dtHistory_1.Rows.Count > 0;
                txtTenBN.Text = Utility.sDbnull(drWorklist["ten_benhnhan"]) + " - " +
                    (Utility.sDbnull(drWorklist["gioi_tinh"])) + " - " +
                                      Utility.sDbnull(drWorklist["NAM_SINH"]);
                this.Text += " - " + txtTenBN.Text;
                txtDiaChi.Text = Utility.sDbnull(drWorklist["DIA_CHI"]);
                txtMaluotkham.Text = Utility.sDbnull(drWorklist["ma_luotkham"]);
                txtCD.Text = Utility.sDbnull(drWorklist["chan_doan"]);
                txtBSCD.Text = Utility.sDbnull(drWorklistDetail["ten_bacsichidinh"]);
                txtIdBenhnhan.Text = Utility.sDbnull(drWorklist["id_benhnhan"]);
                txtObjectType_Name.Text = Utility.sDbnull(drWorklist["ten_doituong_kcb"]);
                txtTendichvu.Text = Utility.sDbnull(drWorklist["ten_chitietdichvu"], "");
                txtKet_Luan.Text = Utility.sDbnull(drWorklist["ket_luan"]);
                txtDenghi.Text = Utility.sDbnull(drWorklist["de_nghi"]);
                string html = Utility.sDbnull(drWorklist["mota_html"], "");
                Mota_Org = Utility.sDbnull(drWorklist["mo_ta"], "");
                if (html != "")
                    txtNoiDung.Text = html;
                objNhanvien = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(objKcbChidinhclsChitiet.NguoiThuchien).ExecuteSingle<DmucNhanvien>();
                if (objNhanvien == null)
                    objNhanvien = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(globalVariables.UserName).ExecuteSingle<DmucNhanvien>();
                if (cboBacsi.Text == "" || cboBacsi.SelectedIndex < 0)
                    if (objNhanvien != null)
                        cboBacsi.SelectedIndex = Utility.GetSelectedIndex(cboBacsi, objNhanvien.UserName);
                if (vks != null)
                {
                    if (txtKet_Luan.Text.TrimStart().TrimEnd() == "")
                        txtKet_Luan.Text = vks.KetLuan;
                    if (txtDenghi.Text.TrimStart().TrimEnd() == "")
                        txtDenghi.Text = vks.DeNghi;
                    cboDoc.Items.AddRange(vks.TenfileKq.Split(',').ToArray<string>());
                    //txtTenFileKQ.Text = vks.TenfileKq;
                    txtVungKS.Text = vks.TenVungkhaosat;
                }
                else
                {
                    txtTenFileKQ.Clear();
                    txtVungKS.Text = "";
                }
                cboDoc.SelectedIndex = Utility.GetSelectedIndexbyStringVal(cboDoc, File.ReadAllText(docfilepath));
                //if (tenFiletraKQ == "") tenFiletraKQ = "default.doc";
                dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(vks.Id, objKcbChidinhclsChitiet.IdChitietchidinh).GetDataSet().Tables[0];
                if (dtDynamicData.Rows.Count == 0)
                    CKEditorInput = true;
                if (CKEditorInput)
                    pnlCkeditor.BringToFront();
                else
                {
                    pnlCkeditor.SendToBack();
                    FillDynamicValues();
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);

            }
            finally
            {
                ModifyCommandButtons();
                if (cboDoc.Items.Count > 0 && cboDoc.SelectedIndex < 0) cboDoc.SelectedIndex = 0;
                AllowSelectionChanged = true;
                txtTenFileKQ.Text = cboDoc.Text;
                setReadOnly();
                timer1.Start();
                LoadHTML();
                loadHinhAnh(false);

            }
        }
        void ModifyCommandButtons()
        {
            if (objKcbChidinhclsChitiet != null)
            {
                cmdPrint.Enabled = objKcbChidinhclsChitiet.TrangThai >= 3;
                cmdSave.Enabled = cmdSaveAndAccept.Enabled = cmdSaveAndPrint.Enabled = objKcbChidinhclsChitiet.TrangThai >= 1 && objKcbChidinhclsChitiet.TrangThai <=3;//Chưa duyệt
                cmdDuyet.Enabled =mnuDuyet.Enabled= objKcbChidinhclsChitiet.TrangThai == 3;
                cmdHuyduyet.Enabled = cmdHuyduyet.Visible =mnuHuyduyet.Enabled= objKcbChidinhclsChitiet.TrangThai == 4;
                cmdDuyet.Visible = !cmdHuyduyet.Visible;
                cmdChupAnh.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("CDHA_NHAPTRAKQ_CHOPHEPCHUPANH", "0", true) == "1";
            }
        }
        void ModifyRegionHinhAnh()
        {
            uiTabPageVTTH.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("CDHA_NHAPTRAKQ_HIENTHITAB_KEVTTH", "0", true) == "1";
            List<string> lstDvucohinhanh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CDHA_NHAPTRAKQ_DICHVUHIENTHICHONANH", "", true).Split(',').ToList<string>();
            List<string> lstDichvu=StrServiceCode.Split('@')[0].Split(',').ToList<string>();
            bool hasHinhanh = false;
            foreach(string s in lstDichvu)
                if (lstDvucohinhanh.Contains(s))
                {
                    hasHinhanh = true;
                    break;
                }
            if (!hasHinhanh)
            {
                splitContainer1.Panel2Collapsed=true;
                uiTabPageHinhanh.TabVisible = false;
            }
        }
        public DataTable GetAllDoctors()
        {
            DataTable result = null;
            List<string> lstKhoaphong = new List<string>();
            lstKhoaphong = Util.GetKhoaPhong();
            string _khoaphong = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_KHOAPHONG_BSTHIEN", "", true);
            if (_khoaphong != "") lstKhoaphong = _khoaphong.Split(',').ToList<string>();
            try
            {
                if (_khoaphong=="ALL"|| lstKhoaphong.Count <= 0)
                    result =
                        new Select().From(DmucNhanvien.Schema)
                        .ExecuteDataSet().Tables[0];
                else
                    result =
                        new Select().From(DmucNhanvien.Schema)
                        .Where(DmucNhanvien.Columns.IdPhong).In(lstKhoaphong)
                        .ExecuteDataSet().Tables[0];
                var dr = result.NewRow();
                dr[DmucNhanvien.Columns.MaNhanvien] = -1;
                dr[DmucNhanvien.Columns.TenNhanvien] = "";
                result.Rows.InsertAt(dr, 0);
            }
            catch (Exception)
            {

            }
            return result;
        }
        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        void LoadHTMLHistory()
        {
            string noidung = txtNoidungHistory.Text;
            webBrowserHistory.Document.InvokeScript("setValue", new[] { noidung });
        }
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
        void CopyImagefromClipboard()
        {
            try
            {
                string Dir = Util.getDir();
                if (!Directory.Exists(Dir)) return;

                System.Drawing.Image obj = Clipboard.GetImage();

                if (obj != null)
                {
                    string ext = Util.getImageFormat();
                    string fileName = Dir + @"\" + Guid.NewGuid().ToString() + ext;
                    obj.Save(fileName, getImgFormat(ext));
                }
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
        }
        private void frm_NhaptraKQ_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Escape)
                //{
                //    this.Close();
                //}
                if (e.Control && e.KeyCode == Keys.V)
                {
                    CopyImagefromClipboard();
                    return;
                }
                if (e.KeyCode == Keys.F9)
                {
                    cmdChupAnh.PerformClick();
                    return;
                }

                if (e.Control && e.KeyCode == Keys.F3)
                    cmdChonvungKS.PerformClick();
                if (e.Control && (e.KeyCode == Keys.F4 || e.KeyCode == Keys.P))
                    cmdPrint.PerformClick();
                if (e.Control)
                    switch (e.KeyCode)
                    {
                        case Keys.S:
                            cmdSave.PerformClick();
                            break;
                        case Keys.P:
                            cmdPrint.PerformClick();
                            break;
                        default:
                            break;
                    }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        void UpdateWorklist_Duyet()
        {
            try
            {
                if (drWorklistDetail != null)
                {
                    drWorklistDetail["trang_thai"] = _dtThongTinBenhNhan.Rows[0]["trang_thai"];
                    drWorklistDetail["ten_trangthai"] = Utility.LaythongtinTrangthaiCLS(Utility.ByteDbnull(drWorklistDetail["trang_thai"], 0));
                    drWorklistDetail["trangthai_chitiet"] = _dtThongTinBenhNhan.Rows[0]["trangthai_chitiet"];
                }
                drWorklist["trang_thai"] = _dtThongTinBenhNhan.Rows[0]["trang_thai"];
                drWorklist["trangthai_chitiet"] = _dtThongTinBenhNhan.Rows[0]["trangthai_chitiet"];
            }
            catch (Exception ex)
            {
            }
            finally
            {
                drWorklist.Table.AcceptChanges();
            }
        }
        void UpdateWorklist()
        {
            try
            {
                if (drWorklistDetail != null)
                {
                    drWorklistDetail["id_vungks"] = id_VungKS;
                    drWorklistDetail["result_type"] = Utility.Bool2byte(CKEditorInput);//Bỏ 3 mục dưới do đã dùng CSDL riêng để lưu
                    //drWorklistDetail["mota_html"] = _dtThongTinBenhNhan.Rows[0]["mota_html"];
                    //drWorklistDetail["Ket_luan"] = _dtThongTinBenhNhan.Rows[0]["Ket_luan"];
                    //drWorklistDetail["de_nghi"] = _dtThongTinBenhNhan.Rows[0]["de_nghi"];
                    drWorklistDetail["ten_nguoi_thuchien"] = _dtThongTinBenhNhan.Rows[0]["ten_nguoi_thuchien"];
                    drWorklistDetail["nguoi_thuchien"] = _dtThongTinBenhNhan.Rows[0]["nguoi_thuchien"];
                    drWorklistDetail["ngay_thuchien"] = _dtThongTinBenhNhan.Rows[0]["ngay_thuchien"];
                    drWorklistDetail["trang_thai"] = _dtThongTinBenhNhan.Rows[0]["trang_thai"];
                    drWorklistDetail["ten_trangthai"] = Utility.LaythongtinTrangthaiCLS(Utility.ByteDbnull(drWorklistDetail["trang_thai"], 0));

                    drWorklistDetail["trangthai_chitiet"] = _dtThongTinBenhNhan.Rows[0]["trangthai_chitiet"];
                }
                drWorklist["id_vungks"] = id_VungKS;
                drWorklist["result_type"] = Utility.Bool2byte(CKEditorInput);
                drWorklist["mota_html"] = _dtThongTinBenhNhan.Rows[0]["mota_html"];
                drWorklist["Ket_luan"] = _dtThongTinBenhNhan.Rows[0]["Ket_luan"];
                drWorklist["de_nghi"] = _dtThongTinBenhNhan.Rows[0]["de_nghi"];
                drWorklist["ten_nguoi_thuchien"] = _dtThongTinBenhNhan.Rows[0]["ten_nguoi_thuchien"];
                drWorklist["nguoi_thuchien"] = _dtThongTinBenhNhan.Rows[0]["nguoi_thuchien"];
                drWorklist["ngay_thuchien"] = _dtThongTinBenhNhan.Rows[0]["ngay_thuchien"];
                drWorklist["trang_thai"] = _dtThongTinBenhNhan.Rows[0]["trang_thai"];
                drWorklist["trangthai_chitiet"] = _dtThongTinBenhNhan.Rows[0]["trangthai_chitiet"];
            }
            catch (Exception ex)
            {
            }
            finally
            {
                drWorklist.Table.AcceptChanges();
            }
        }
        void SaveKQ(bool Msg, bool Confirm)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (objKcbChidinhclsChitiet != null)//Đã có KQ
                {
                    if (globalVariables.IsAdmin)
                    {
                    }
                    else
                    {
                        if (objKcbChidinhclsChitiet.NguoiThuchien != null && Utility.DoTrim(objKcbChidinhclsChitiet.NguoiThuchien).Length > 0 && objKcbChidinhclsChitiet.NguoiThuchien != globalVariables.UserName)
                        {
                            Utility.ShowMsg(
                                string.Format("Kết quả chẩn đoán đã được nhập bởi {0}. Bạn không được phép chỉnh sửa. Vui lòng liên hệ với Bác sĩ thực hiện nếu muốn chỉnh sửa (Hoặc bạn phải là Admin) ", Utility.sDbnull(drWorklist["ten_nguoi_thuchien"], "")),
                                "Thông báo");
                            return;
                        }
                    }
                }
                foreach (DataRow dr in _dtThongTinBenhNhan.Rows)
                {
                    dr["mota_html"] = webBrowser1.Document.InvokeScript("getValue").ToString();// Utility.sDbnull(webBrowser1.Text);
                    dr["Ket_luan"] = Utility.sDbnull(txtKet_Luan.Text);
                    dr["de_nghi"] = Utility.sDbnull(txtDenghi.Text);
                    dr["ten_nguoi_thuchien"] = Utility.sDbnull(cboBacsi.Text);
                    dr["nguoi_thuchien"] = Utility.sDbnull(cboBacsi.SelectedValue);
                    //dr["ngay_thuchien"] = DateTime.Now.ToString("dd/MM/yyyy");//Bỏ đi để in chính xác ngày thực hiện chỗ trình kí
                    if (Confirm)
                    {
                        // if (Utility.Int32Dbnull(dr["trang_thai"], 0) <= 4)
                        dr["trang_thai"] = 4;
                        dr["trangthai_chitiet"] = 4;
                    }
                    else
                        if (Utility.Int32Dbnull(dr["trang_thai"], 0) <= 3)
                        {
                            dr["trang_thai"] = 3;
                            dr["trangthai_chitiet"] = 3;
                        }
                }
                UpdateWorklist();
                string mo_ta_dyn = "";
                string ket_qua = "";
                string mo_ta = "";
                string mota_html = "";
                string ket_luan = "";
                string de_nghi = "";
                if (!CKEditorInput)
                {
                    if (!HasValue(flowDynamics))
                        return;
                    mo_ta_dyn = SaveNow(flowDynamics, false);
                    ket_qua = getKetluan(flowDynamics);
                }
                QheBacsiDichvucl objqhe = null;
                if (objNhanvien != null)
                    objqhe = new Select().From(QheBacsiDichvucl.Schema).Where(QheBacsiDichvucl.Columns.IdNhanvien).IsEqualTo(objNhanvien.IdNhanvien).And(QheBacsiDichvucl.Columns.IdChitietdichvu).IsEqualTo(objKcbChidinhclsChitiet.IdChitietdichvu).ExecuteSingle<QheBacsiDichvucl>();
                // bang thong thong lan kham
                mo_ta = CKEditorInput ? webBrowser1.Document.InvokeScript("getData").ToString() : mo_ta_dyn;
                objKcbChidinhclsChitiet.KetLuanCdha = ket_qua;
                mota_html = webBrowser1.Document.InvokeScript("getValue").ToString();// webBrowser1.Document.InvokeScript("getValue").ToString();// Utility.sDbnull(webBrowser1.Text);
                string s = HtmlToPlainText(mota_html);

                string ketluanfromMota = "";
                try
                {
                    int idx = mo_ta.ToUpper().LastIndexOf("KẾT LUẬN:");
                    ketluanfromMota = mo_ta.Substring(idx + "KẾT LUẬN:".Length).TrimStart().TrimEnd();
                    if (ketluanfromMota.Length == 0) ketluanfromMota = ket_qua;
                }
                catch (Exception ex)
                {


                }

                ket_luan = txtKet_Luan.Visible ? Utility.sDbnull(txtKet_Luan.Text) : ketluanfromMota;
                if (Utility.Int32Dbnull(objKcbChidinhclsChitiet.TrangThai, 0) <= 3)
                    objKcbChidinhclsChitiet.TrangThai = 3;
                de_nghi = Utility.sDbnull(txtDenghi.Text);
                objKcbChidinhclsChitiet.ResultType = Utility.Bool2byte(CKEditorInput);
                if (objKcbChidinhclsChitiet.NgayThuchien == null)
                    objKcbChidinhclsChitiet.NgayThuchien = globalVariables.SysDate;
                else
                {
                    objKcbChidinhclsChitiet.NgaySua = globalVariables.SysDate;
                    objKcbChidinhclsChitiet.NguoiSua = globalVariables.UserName;
                }
                objKcbChidinhclsChitiet.IdVungks = id_VungKS.ToString();
                objKcbChidinhclsChitiet.NguoiThuchien = Utility.sDbnull(cboBacsi.SelectedValue, "");
                if (Confirm)
                {
                    objKcbChidinhclsChitiet.TrangThai = 4;

                }
                if (objqhe != null)
                    objKcbChidinhclsChitiet.PtramCkhau = objqhe.PtramCkhau;
                else
                    objKcbChidinhclsChitiet.PtramCkhau = 0;
                objKcbChidinhclsChitiet.KieuFilm = cboLoaiFilm.Text;
                objKcbChidinhclsChitiet.SolanChup = Utility.ByteDbnull(nmrSLchup.Value, 1);
                objKcbChidinhclsChitiet.SoFilm = Utility.ByteDbnull(nmrSoFilm.Value, 1);
                StoredProcedure sp = SPs.ClsKetquaHa(objKcbChidinhclsChitiet.IdChitietchidinh, mo_ta, ket_luan,
                    de_nghi, mota_html, Utility.sDbnull(cboDevice.SelectedValue, "-1"), objKcbChidinhclsChitiet.NgayThuchien, Utility.Int16Dbnull(objNhanvien != null ? objNhanvien.IdNhanvien : -1, -1),
                  objKcbChidinhclsChitiet.NguoiThuchien, globalVariables.UserName, objKcbChidinhclsChitiet.IdChidinh, objChidinh.MaChidinh, objChidinh.IdBenhnhan, objChidinh.MaLuotkham, txtTendichvu.Text, globalVariables.SysDate, globalVariables.UserName, objKcbChidinhclsChitiet.NgaySua, objKcbChidinhclsChitiet.NguoiSua);
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (!CKEditorInput)
                        {
                            SaveNow(flowDynamics, true);

                        }
                        else
                            sp.Execute();

                        objKcbChidinhclsChitiet.Save();
                    }
                    scope.Complete();
                }
                Mota_Org = Mota_Current = mo_ta;
                //Save DynamicFields
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật kết quả CĐHA cho bệnh nhân ID={0}, PID={1}, Tên={2}, dịch vụ={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, txtTendichvu.Text), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                mv_blnCancel = false;
                Cursor.Current = Cursors.Default;
                if (Msg)
                    Utility.ShowMsg("Cập nhật kết quả thành công!");
                Utility.SetMsg(lblMsg, "Cập nhật kết quả thành công!", false);

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {

            }
        }
        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = false;
                SaveKQ(false, PropertyLib._HinhAnhProperties.SaveAndConfirm);
                if (chkInsauluu.Checked) InKetQua(null);
                ModifyCommandButtons();
            }
            catch (Exception)
            {

            }
            finally
            {
                cmdSave.Enabled = true;
            }
           
        }
        DmucNhanvien objNhanvien = null;
        int oldID_vungks = -1;
        private void ChonVungKS()
        {
            bool OK = false;
            oldID_vungks = id_VungKS;
            try
            {
                //if (lstID.Count <= 0 || (lstID.Count ==1 && lstID[0].TrimEnd().TrimStart()=="") || lstID.Count >= 2)
                //{
                frm_chonvungksat _chonvungksat = new frm_chonvungksat(new List<string>());
                // new RISLink.UI.DanhMuc.frm_chonvungksat(File.Exists(Application.StartupPath+@"\showall.txt")?new List<string>(): lstID);
                if (_chonvungksat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    id_VungKS = _chonvungksat.Id;
                    txtVungKS.Text = _chonvungksat.ten;
                    OK = true;
                }
                else
                    return;
                //}
                //else
                //    if (lstID.Count > 0)
                //        id_VungKS = Utility.Int32Dbnull(lstID[0], -1);
                if (!OK) return;
                vks = DmucVungkhaosat.FetchByID(id_VungKS);
                if (vks != null)
                {

                    cboDoc.Items.Clear();
                    cboDoc.Items.AddRange(vks.TenfileKq.Split(',').ToArray<string>());
                    txtNoiDung.Text = vks.MotaHtml;
                    txtKet_Luan.Text = vks.KetLuan;
                    txtDenghi.Text = vks.DeNghi;
                    //if (tenFiletraKQ == "") tenFiletraKQ = "default.doc";
                    cboDoc.SelectedIndex = Utility.GetSelectedIndexbyStringVal(cboDoc, File.ReadAllText(docfilepath));
                    if (oldID_vungks != id_VungKS)
                    {
                        oldID_vungks = id_VungKS;
                        dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(vks.Id, objKcbChidinhclsChitiet.IdChitietchidinh).GetDataSet().Tables[0];
                        FillDynamicValues();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (cboDoc.Items.Count > 0 && cboDoc.SelectedIndex < 0) cboDoc.SelectedIndex = 0;
                timer1.Start();
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                cmdPrint.Enabled = false;
                //if (!_IsReadonly)
                //    SaveKQ(false, PropertyLib._HinhAnhProperties.SaveAndConfirm);
                //foreach (UC_Image uc in pnlImgs.Controls)
                //{
                //    uc.RearrangeControls();
                //    break;
                //}
                InKetQua(null);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdPrint.Enabled = true;
            }
        }




        #endregion
        private void ChonFileMauTraKQ()
        {
            List<string> lstFiles = new List<string>();
            try
            {
                if (vks == null)
                {
                    Utility.ShowMsg("Bạn cần chọn vùng khảo sát trước khi chọn file in kết quả");
                    return;
                }
                string sFilter = "All|*.Doc;*.Docx;*.doc";
                using (OpenFileDialog _filedlg = new OpenFileDialog())
                {
                    _filedlg.Multiselect = true;
                    _filedlg.Filter = sFilter;
                    if (_filedlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lstFiles = _filedlg.FileNames.ToList<string>();

                    }
                }
                if (lstFiles.Count > 0)
                {
                    foreach (string newFile in lstFiles)
                    {
                        if (newFile.ToUpper() != (pathDirectory + Path.GetFileName(newFile)).ToUpper())
                            File.Copy(newFile, pathDirectory + Path.GetFileName(newFile), true);
                    }
                    vks.TenfileKq = string.Join(",", lstFiles.ToArray<string>());
                    vks.Save();
                    //txtTenFileKQ.Text = vks.TenfileKq;
                    //tenFiletraKQ = txtTenFileKQ.Text;
                    //Utility.ShowMsg("Cập nhật file in kết quả thành công");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);

            }

        }


        private void cmdBrowse_Click_bak(object sender, EventArgs e)
        {
            try
            {
                cmdBrowse.Enabled = false;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = sFilter;
                if (File.Exists(Application.StartupPath + @"\imgfolder.txt"))
                    ofd.InitialDirectory = Path.GetDirectoryName(File.ReadAllText(Application.StartupPath + @"\imgfolder.txt"));
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string AutoCheck = Utility.Laygiatrithamsohethong("AutoCheckImage", "0", true);
                    File.WriteAllText(Application.StartupPath + @"\imgfolder.txt", ofd.FileNames[0]);
                    foreach (string sFile in ofd.FileNames)
                    {
                        if (!listAnh.Contains(sFile))
                        {
                            listAnh.Add(sFile);
                            string sFtpName = CreateFtp(sFile);

                            cboHinhAnh.Items.Add(Path.GetFileName(sFtpName));

                            KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                            objKcbKetquaHa.IdChiTietChiDinh = id_Study_Detail;
                            objKcbKetquaHa.Chonin = Convert.ToByte(AutoCheck == "1" ? 1 : 0);

                            objKcbKetquaHa.Mota = string.Empty;
                            //objKcbKetquaHa.HinhAnh = imageToByteArray(GetImage(sFile));
                            objKcbKetquaHa.TenAnh = Path.GetFileName(sFtpName);
                            objKcbKetquaHa.DuongdanLocal = sFile;
                            objKcbKetquaHa.IsNew = true;
                            objKcbKetquaHa.Save();
                            string rfile = sFile;
                            if (PropertyLib._HinhAnhProperties.EnabledFTP) rfile = copyImage2Local(-1, sFile, Path.GetFileName(sFtpName));
                            UC_Image objUcImage = new UC_Image(flowLayoutPanel_Phai);
                            //objUcImage.LayoutPanel = flowLayoutPanel1;
                            objUcImage.Chonin = AutoCheck == "1";
                            objUcImage.IdHinhAnh = objKcbKetquaHa.Id;
                            objUcImage.ImgData = GetImage(sFile);// objKcbKetquaHa.HinhAnh;
                            objUcImage.Mota = string.Empty;
                            objUcImage.Tag = rfile;
                            objUcImage.ftpPath = sFtpName;
                            objUcImage.DuongdanLocal = rfile;
                            objUcImage.VisibleControl(true);
                            objUcImage._OnChonIn += objUcImage__OnChonIn;
                            objUcImage._OnDelete += objUcImage__OnDelete;
                            objUcImage._OnClick += objUcImage__OnClick;
                            if (File.Exists(Application.StartupPath + @"\auto.kut"))
                            {
                                AutoC(objUcImage);
                            }
                            flowLayoutPanel_Phai.Controls.Add(objUcImage);
                            if (AutoCheck == "1") objUcImage__OnChonIn(objUcImage);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdBrowse.Enabled = true;
                cmdSend2PACS.Enabled = !_IsReadonly && pnlImgs.Controls.Count > 0;
            }
        }
        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                cmdBrowse.Enabled = false;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = sFilter;
                if (File.Exists(Application.StartupPath + @"\imgfolder.txt"))
                    ofd.InitialDirectory = Path.GetDirectoryName(File.ReadAllText(Application.StartupPath + @"\imgfolder.txt"));
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string AutoCheck = Utility.Laygiatrithamsohethong("AutoCheckImage", "0", true);
                    File.WriteAllText(Application.StartupPath + @"\imgfolder.txt", ofd.FileNames[0]);
                    foreach (string sFile in ofd.FileNames)
                    {
                        if (!listAnh.Contains(sFile))
                        {
                            listAnh.Add(sFile);
                            string sFtpName = CreateFtp(sFile);

                            cboHinhAnh.Items.Add(Path.GetFileName(sFtpName));

                            KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                            objKcbKetquaHa.IdChiTietChiDinh = id_Study_Detail;
                            objKcbKetquaHa.Chonin = Convert.ToByte(AutoCheck == "1" ? 1 : 0);

                            objKcbKetquaHa.Mota = string.Empty;
                            objKcbKetquaHa.Vitri = Utility.GetVitriHinhAnhByName(sFile);
                            objKcbKetquaHa.TenAnh = Path.GetFileName(sFtpName);
                            objKcbKetquaHa.DuongdanLocal = sFile;
                            objKcbKetquaHa.IsNew = true;
                            objKcbKetquaHa.Save();
                            string rfile = sFile;
                            if (PropertyLib._HinhAnhProperties.EnabledFTP) rfile = copyImage2Local(-1, sFile, Path.GetFileName(sFtpName));
                            UC_Image objUcImage = new UC_Image(objKcbKetquaHa.Vitri == 0 ? flowLayoutPanel_Phai : flowLayoutPanel_Trai);
                            //objUcImage.LayoutPanel = flowLayoutPanel1;
                            objUcImage.Chonin = AutoCheck == "1";
                            objUcImage.IdHinhAnh = objKcbKetquaHa.Id;
                            objUcImage.ImgData = GetImage(sFile);// objKcbKetquaHa.HinhAnh;
                            objUcImage.Mota = string.Empty;
                            objUcImage.Vitri = Utility.ByteDbnull(objKcbKetquaHa.Vitri);
                            objUcImage.Tag = rfile;
                            objUcImage.ftpPath = sFtpName;
                            objUcImage.DuongdanLocal = rfile;
                            objUcImage.VisibleControl(true);
                            objUcImage._OnChonIn += objUcImage__OnChonIn;
                            objUcImage._OnDelete += objUcImage__OnDelete;
                            objUcImage._OnClick += objUcImage__OnClick;
                            if (File.Exists(Application.StartupPath + @"\auto.kut"))
                            {
                                AutoC(objUcImage);
                            }
                            if (objUcImage.Vitri == 0)
                                flowLayoutPanel_Phai.Controls.Add(objUcImage);
                            else
                                flowLayoutPanel_Trai.Controls.Add(objUcImage);
                            if (AutoCheck == "1") objUcImage__OnChonIn(objUcImage);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdBrowse.Enabled = true;
                cmdSend2PACS.Enabled = !_IsReadonly && pnlImgs.Controls.Count > 0;
            }
        }
        private void watch()
        {
            try
            {
                string Dir = Util.getDir();
                if (!Directory.Exists(Dir)) return;
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = Dir;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Filter = "*.*";
                watcher.Changed += watcher_Changed;
                watcher.Renamed += watcher_Renamed;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
            }
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                if (Utility.Int64Dbnull(DateTime.Now.ToString("yyMMdd")) <= 250912 && PropertyLib._HinhAnhProperties.UseRenamedEvt)
                {
                    if (!lstFiles.Contains(e.FullPath))
                    {
                        _log.Trace(string.Format("renamed file to {1}", e.FullPath));
                        string sFile = e.FullPath;
                        lstFiles.Add(sFile);
                        myQ.Enqueue(sFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }


        Queue myQ = new Queue();
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {

            try
            {
                if (!PropertyLib._HinhAnhProperties.UseRenamedEvt)
                {
                    if (!lstFiles.Contains(e.FullPath))
                    {
                        _log.Trace(string.Format("file created {0}", e.FullPath));
                        string sFile = e.FullPath;
                        lstFiles.Add(sFile);
                        myQ.Enqueue(sFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }
        void _FtpManager__OnSaving(string msg)
        {
            this.Text = msg;
            Application.DoEvents();
        }
        FtpManager _FtpManager = null;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoiDung.Text)) ClickData();
          
        }
        private void ClickData()
        {
            webBrowser1.Document.InvokeScript("setValue", new[] { txtNoiDung.Text });
            timer1.Stop();
        }
        private void ClickDataHistory()
        {
            webBrowserHistory.Document.InvokeScript("setValue", new[] { txtNoidungHistory.Text });
            timer3.Stop();
        }
        private void cboBacsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objNhanvien = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(cboBacsi.SelectedValue.ToString()).ExecuteSingle<DmucNhanvien>();
                //objNhanvien = DmucNhanvien.FetchByID(cboBacsi.SelectedValue.ToString());
            }
            catch (Exception)
            {

            }
        }

        private void cmdWord_Click(object sender, EventArgs e)
        {
            InKetQua(null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void cmdChonlai_Click(object sender, EventArgs e)
        {
            loadHinhAnh(true);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (myQ.Count > 0)
            {
                string sFile = myQ.Dequeue() as string;
                List<string> lstFTPName = new List<string>();
                foreach (string item in cboHinhAnh.Items)
                    lstFTPName.Add(item);
                _log.Trace(string.Format(" file add 2 ftp {0}", sFile));
                FtpItem f = new FtpItem(true, listAnh, lstFTPName, new List<string>() { sFile }, ID_Study_Detail, FtpClient, FtpClientCurrentDirectory, _baseDirectory);
                _FtpManager.AddItems(f);
            }
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdConfig_Click_1(object sender, EventArgs e)
        {

        }
        #region new Input Result
        DataTable dtDynamicData = null;
        void FillDynamicValues()
        {
            try
            {
                if (objKcbChidinhclsChitiet == null) return;
                flowDynamics.SuspendLayout();
                flowDynamics.Controls.Clear();
                //Tạm khóa dòng dưới để tùy biến nếu chưa khai báo Dynamic thì dùng cách nhập kết quả cũ
                dtDynamicData = SPs.HinhanhGetDynamicFieldsValues(vks.Id, objKcbChidinhclsChitiet.IdChitietchidinh).GetDataSet().Tables[0];

                foreach (DataRow dr in dtDynamicData.Select("1=1", "Stt_hthi"))
                {
                    VNS.UCs.ucAutoCompleteParam _ucp = new VNS.UCs.ucAutoCompleteParam(dr, true);
                    _ucp.txtValue.VisibleDefaultItem = true;
                    _ucp.txtValue.MaxHeight = PropertyLib._HinhAnhProperties.AutoCompleteMaxHeight;

                    _ucp.IdChidinhchitiet = objKcbChidinhclsChitiet.IdChitietchidinh;
                    _ucp.txtValue.RaiseEventEnter = true;
                    _ucp.lblName.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    _ucp.txtValue.Font = PropertyLib._HinhAnhProperties.DynamicFontChu;
                    //_ucp.txtValue._OnShowData += txtValue__OnShowData;
                    _ucp._OnEnterKey += _ucp__OnEnterKey;
                    _ucp.TabStop = true;
                    _ucp.txtValue.AllowEmpty = Utility.Int32Dbnull(dr[DynamicField.Columns.AllowEmpty], 0) == 1;
                    _ucp.txtValue.Multiline = Utility.Int32Dbnull(dr[DynamicField.Columns.Multiline], 0) == 1;
                    _ucp.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.W], 0);
                    _ucp.Height = Utility.Int32Dbnull(dr[DynamicField.Columns.H], 0);
                    _ucp.lblName.Width = Utility.Int32Dbnull(dr[DynamicField.Columns.LblW], 0);
                    _ucp.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
                    _ucp.Init();
                    //_ucp.txtValue.ReadOnly = IsEnabledBySex(_ucp.txtValue.LOAI_DANHMUC);
                    SetEnabledBySex(_ucp.txtValue);
                    if (!_ucp.txtValue.Enabled)
                        _ucp.lblName.ForeColor = Color.Black;
                    flowDynamics.Controls.Add(_ucp);
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                flowDynamics.ResumeLayout(true);
            }
        }
        void SetEnabledBySex(HIS.UCs.AutoCompleteTextbox_DynamicField _txt)
        {
            if (_txt.LOAI_DANHMUC == "GTNAM" || _txt.LOAI_DANHMUC == "GTNU")
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_TUYBIEN_GIOITINH", "0", false) == "1")
                {
                    if (objBenhnhan.IdGioitinh == 0)//Nam
                    {
                        if (_txt.LOAI_DANHMUC == "GTNAM")
                            _txt.Enabled = true;
                        else
                        {
                            _txt.Enabled = false;
                            _txt.ClearText();
                        }
                    }
                    else
                    {
                        if (_txt.LOAI_DANHMUC == "GTNU")
                            _txt.Enabled = true;
                        else
                        {
                            _txt.Enabled = false;
                            _txt.ClearText();
                        }
                    }
                }
            }

        }

        void _ucp__OnEnterKey(ucAutoCompleteParam obj)
        {
            try
            {
                //if (!obj._AcceptTab)
                //{
                int idx = -1;
                IEnumerable<int> q = (from p in flowDynamics.Controls.Cast<ucAutoCompleteParam>().AsEnumerable()
                                      where p.TabIndex > obj.TabIndex && p.txtValue.Enabled
                                      select p.TabIndex);
                IEnumerable<int> enumerable = q as int[] ?? q.ToArray();
                if (enumerable.Any())
                    idx = enumerable.Min();
                if (idx > 0)
                {
                    foreach (ucAutoCompleteParam ucs in flowDynamics.Controls)
                    {
                        if (ucs.TabIndex == idx)
                        {
                            ucs.FocusMe();
                            return;
                        }
                    }
                }
                else //Last Controls
                    cmdSave.Focus();
                //}
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        //private void FillDynamicValues()
        //{
        //    try
        //    {
        //        flowDynamics.Controls.Clear();

        //        DataTable dtData = clsHinhanh.GetDynamicFieldsValues(Utility.Int32Dbnull(txtIdDichvuChitiet.Text),
        //            txtMauKQ.MyCode, "", "", -1, Utility.Int32Dbnull(txtidchidinhchitiet.Text));

        //        foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
        //        {
        //            dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(txtidchidinhchitiet.Text);
        //            var ucTextSysparam = new ucDynamicParam(dr, true);

        //            ucTextSysparam.TabStop = true;
        //            ucTextSysparam._OnEnterKey += _ucTextSysparam__OnEnterKey;
        //            ucTextSysparam.TabIndex = 10 +
        //                                      Utility.Int32Dbnull(dr[DynamicField.Columns.Stt],
        //                                          flowDynamics.Controls.Count);

        //            ucTextSysparam.Init();
        //            if (Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]))
        //            {
        //                ucTextSysparam.Size = PropertyLib._DynamicInputProperties.RtfDynamicSize;
        //                ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.RtfTextSize;
        //                ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.RtfLabelSize;
        //            }
        //            else
        //            {
        //                ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
        //                ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
        //                ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
        //            }
        //            flowDynamics.Controls.Add(ucTextSysparam);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (globalVariables.IsAdmin)
        //        {
        //            Utility.ShowMsg(ex.ToString());
        //        }
        //    }
        //}


        //private void lnkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    int idChitietdichvu = Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1);

        //    DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(idChitietdichvu);
        //    try
        //    {
        //        if (objDichvuchitiet == null)
        //        {
        //            Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
        //            return;
        //        }
        //        if (txtMauKQ.MyCode == "-1")
        //        {
        //            Utility.ShowMsg("Bạn cần chọn mẫu kết quả của dịch vụ trước khi tạo thông tin nhập kết quả");
        //            return;
        //        }
        //        var dynamicSetup = new frm_DynamicSetup
        //        {
        //            objDichvuchitiet = objDichvuchitiet,
        //            MafileDoc = txtMauKQ.MyCode,
        //            ImageID = -1,
        //            Id_chidinhchitiet = -1
        //        };
        //        if (dynamicSetup.ShowDialog() == DialogResult.OK)
        //        {
        //            FillDynamicValues();
        //            FocusMe(flowDynamics);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ShowMsg("Lỗi: " + ex.Message);
        //    }
        //}
        private void FocusMe(FlowLayoutPanel pnlParent)
        {
            try
            {
                if (pnlParent.Controls.Count > 0)
                {
                    ((ucAutoCompleteParam)pnlParent.Controls[0]).FocusMe();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void UpdateSaveMode(bool allowSave)
        {
            foreach (ucAutoCompleteParam ctrl in flowDynamics.Controls)
            {
                ctrl.AllowSave = allowSave;
            }
        }

        private bool HasValue(FlowLayoutPanel pnlParent)
        {
            if (pnlParent.Controls.Count <= 0) return true;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_BATNHAPKETQUA", "0", true) == "1")
            {
                foreach (ucAutoCompleteParam ctrl in flowDynamics.Controls)
                {
                    if (ctrl.txtValue.Enabled && !ctrl.txtValue.AllowEmpty && Utility.DoTrim(ctrl.txtValue.Text) == "")
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn phải nhập thông tin {0}", ctrl.lblName.Text), true);
                        ctrl.FocusMe();
                        return false;
                    }
                }

            }
            return true;
        }

        private string SaveNow(FlowLayoutPanel pnlParent, bool saveDB)
        {
            string mo_ta = "";
            foreach (ucAutoCompleteParam ctrl in pnlParent.Controls)
            {
                mo_ta += Utility.DoTrim(ctrl.txtValue.Text) + "\n";
                if (!ctrl.isSaved)
                    if (saveDB) ctrl.Save();
            }
            return mo_ta;
        }
        private string getKetluan(FlowLayoutPanel pnlParent)
        {
            string ket_luan = "";
            foreach (ucAutoCompleteParam ctrl in pnlParent.Controls)
            {
                if (ctrl.Code.Contains("KETLUAN"))
                    return Utility.DoTrim(ctrl.txtValue.Text);
            }
            return ket_luan;
        }
        #endregion

        private void lnkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (vks == null)
                {
                    Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                    return;
                }
                frm_DynamicSetup _DynamicSetup = new frm_DynamicSetup();
                _DynamicSetup.objvks = vks;
                _DynamicSetup.ImageID = -1;
                _DynamicSetup.Id_chidinhchitiet = objKcbChidinhclsChitiet.IdChitietchidinh;
                if (_DynamicSetup.ShowDialog() == DialogResult.OK)
                {
                    if (CKEditorInput)
                        pnlCkeditor.BringToFront();
                    else
                    {
                        pnlCkeditor.SendToBack();
                        FillDynamicValues();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            if (CKEditorInput)
                pnlCkeditor.BringToFront();
            else
            {
                pnlCkeditor.SendToBack();
                FillDynamicValues();
            }
        }

        private void cmdChange_Click(object sender, EventArgs e)
        {
            CKEditorInput = !CKEditorInput;
            if (CKEditorInput)
                pnlCkeditor.BringToFront();
            else
            {
                pnlCkeditor.SendToBack();
                FillDynamicValues();
            }
        }

        private void cmdCungthuchien_Click(object sender, EventArgs e)
        {
            frm_bacsicungthuchien _bacsicungthuchien = new frm_bacsicungthuchien(new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.Cungthuchien).IsEqualTo(true).And(DmucNhanvien.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0], id_Study_Detail);
            _bacsicungthuchien.ShowDialog();
        }

        private void chkSignType_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._HinhAnhProperties.SignType = chkSignType.Checked;
            PropertyLib.SaveProperty(PropertyLib._HinhAnhProperties);
        }
        DmucChung mauKQ = null;
        private void cboDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                txtTenFileKQ.Text = cboDoc.Text;
                mauKQ = new Select().From(DmucChung.Schema)
                           .Where(DmucChung.Columns.Loai).IsEqualTo("MAUTRAKQ")
                           .And(DmucChung.Columns.Ma).IsEqualTo(cboDoc.Text)
                           .ExecuteSingle<DmucChung>();
                if (mauKQ != null)
                    lblImgSize.Text = mauKQ.MotaThem;
                else
                    lblImgSize.Text = "";
                Utility.SaveValue2File(docfilepath, txtTenFileKQ.Text);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }

        private void cmdChonvungKS_Click_1(object sender, EventArgs e)
        {

        }
        bool KQXN_Loaded = false;
        bool Donthuoc_Loaded = false;
        private void uiTab1_SelectedTabChanged(object sender, TabEventArgs e)
        {

        }
        void LoadKQXN()
        {
            try
            {
                DataTable dt = SPs.ClsLayKetquaXn(objLuotkham.IdBenhnhan, chkXemlichsuKQXN.Checked ? "ALL" : objLuotkham.MaLuotkham, "ALL", -1, chkXemKQXNTatca.Checked ? 100 : objChidinh.Noitru, objBenhnhan.IdGioitinh).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,stt_in");
                KQXN_Loaded = true;
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {


            }
        }
        void LoadDonthuoc()
        {
            try
            {
                DataTable dtDonthuoc =
                    new KCB_THAMKHAM().KcbThamkhamLayDanhsachDonThuocTheolankham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1l, -1l, 0, "THUOC",-1, Convert.ToByte(chkXemdonthuocTatca.Checked ? 100 : Utility.ByteDbnull(objChidinh.Noitru, 0))).Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPresDetail, dtDonthuoc, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
                Donthuoc_Loaded = true;
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }

        private void mnuRefreshKQXN_Click(object sender, EventArgs e)
        {
            LoadKQXN();
        }

        private void mnuRefreshDonthuoc_Click(object sender, EventArgs e)
        {
            LoadDonthuoc();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Mota_Current = webBrowser1.Document.InvokeScript("getData").ToString();
            DataTable dtKQCDHA = SPs.HinhanhLaydulieuKQCDHA(ID_Study_Detail).GetDataSet().Tables[0];
            if (dtKQCDHA.Rows.Count>0 && Utility.DoTrim(Mota_Org) != (Utility.DoTrim(Mota_Current)))//Sửa kết quả thì mới nhắc
            {
                if (Utility.AcceptQuestion("Hệ thống phát hiện kết quả đã bị sửa đổi. Bạn có chắc chắn muốn thoát khỏi chức năng?\nNhấn No để kiểm tra lại.\nNhấn Yes để thoát và bỏ qua sửa đổi", "", true))
                    this.Close();
            }
            else
                this.Close();
        }

        private void TabInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
            try
            {

                if (TabInfo.SelectedTab == uiTabPageKQXN && !KQXN_Loaded)
                {
                    LoadKQXN();
                }
                else if (TabInfo.SelectedTab == uiTabPageDonthuoc && !Donthuoc_Loaded)
                {
                    LoadDonthuoc();
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdchucnang_Click(object sender, EventArgs e)
        {
            ctxFunctions.Show(cmdchucnang, new Point(0, cmdchucnang.Height));

        }

        private void mnuDuyet_Click(object sender, EventArgs e)
        {
            DuyetKQ();

        }
        void DuyetKQ()
        {
            try
            {
                if (!Utility.Coquyen("cdha_duyet_ketqua"))
                {
                    Utility.ShowMsg("Bạn không có quyền duyệt kết quả CĐHA(cdha_duyet_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai == 3)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn duyệt kết quả này?\nSau khi duyệt, kết quả sẽ được gửi về các khoa phòng khác và các bác sĩ có thể xem kết quả.", "", true))
                    {
                        int RA = new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(4).Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objKcbChidinhclsChitiet.IdChitietchidinh).Execute();
                        if (RA > 0)
                        {
                            objKcbChidinhclsChitiet.TrangThai = 4;
                            objKcbChidinhclsChitiet.NgayDuyet = DateTime.Now;
                            UpdateWorklist_Duyet();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Duyệt kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ duyệt ={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, txtTendichvu.Text), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg("Đã duyệt kết quả thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommandButtons();
            }

        }
        private void mnuHuyduyet_Click(object sender, EventArgs e)
        {

            HuyduyetKQ();
        }
        void HuyduyetKQ()
        {
            try
            {
                if (!Utility.Coquyen("cdha_huyduyet_ketqua"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy duyệt kết quả CĐHA(cdha_huyduyet_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (objKcbChidinhclsChitiet != null && objKcbChidinhclsChitiet.TrangThai == 4)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn HỦY duyệt kết quả này?\nSau khi HỦY duyệt, các bác sĩ ở các khoa phòng khác KHÔNG thể xem kết quả.\n", "", true))
                    {
                        int RA = new Update(KcbChidinhclsChitiet.Schema).Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(3).Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objKcbChidinhclsChitiet.IdChitietchidinh).Execute();
                        if (RA > 0)
                        {
                            objKcbChidinhclsChitiet.TrangThai = 3;
                            objKcbChidinhclsChitiet.NgayDuyet = null;
                            UpdateWorklist_Duyet();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy duyệt kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ hủy duyệt ={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan,txtTendichvu.Text), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg("Đã HỦY duyệt kết quả thành công. Nhấn OK để kết thúc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommandButtons();
            }
        }
        private void mnuLuu_In_Click(object sender, EventArgs e)
        {
            SaveKQ(false, PropertyLib._HinhAnhProperties.SaveAndConfirm);
            InKetQua(null);
            ModifyCommandButtons();
        }

        private void cmdSaveAndPrint_Click(object sender, EventArgs e)
        {
            SaveKQ(false,false);// PropertyLib._HinhAnhProperties.SaveAndConfirm);
            InKetQua(null);
            ModifyCommandButtons();
        }

        private void cmdDuyet_Click(object sender, EventArgs e)
        {
            DuyetKQ();
        }

        private void cmdHuyduyet_Click(object sender, EventArgs e)
        {
            HuyduyetKQ();
        }

        private void mnuHuyKQ_Click(object sender, EventArgs e)
        {
            HuyKQ();
        }
        private void HuyKQ()
        {
            try
            {
                if (objKcbChidinhclsChitiet.TrangThai <= 2)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} chưa có kết quả nên không thể hủy. Vui lòng kiểm tra lại", txtTendichvu.Text));
                    return;
                }
                if (objKcbChidinhclsChitiet.TrangThai>3)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} đã duyệt kết quả nên muốn hủy kết quả thì bạn phải thực hiện hủy duyệt kết quả trước. Vui lòng kiểm tra lại", txtTendichvu.Text));
                    return;
                }
                if (!Utility.Coquyen("cdha_xoa_ketqua"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa kết quả CĐHA(cdha_xoa_ketqua). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (objKcbChidinhclsChitiet != null)
                {
                    if (!globalVariables.IsAdmin)
                    {
                        if (globalVariables.UserName != objKcbChidinhclsChitiet.NguoiThuchien)
                        {
                            Utility.ShowMsg(string.Format("Kết quả CĐHA của dịch vụ đang chọn được thực hiện bởi {0}. Bạn không có quyền hủy kết quả của người khác. Vui lòng liên hệ {1} hoặc Admin của hệ thống để thực hiện việc hủy", objKcbChidinhclsChitiet.NguoiThuchien, objKcbChidinhclsChitiet.NguoiThuchien));
                            return;
                        }
                        int KCB_CDHA_SONGAY_HUYKETQUA =
                                     Utility.Int32Dbnull(
                                         THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CDHA_SONGAY_HUYKETQUA", "0", true), 0);
                        var chenhlech =
                            (int)Math.Ceiling((globalVariables.SysDate.Date - objKcbChidinhclsChitiet.NgayThuchien.Value.Date).TotalDays);
                        if (chenhlech > KCB_CDHA_SONGAY_HUYKETQUA)
                        {
                            Utility.ShowMsg(string.Format("Ngày thực hiện: {0}. Hệ thống không cho phép bạn hủy kết quả CĐHA khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objKcbChidinhclsChitiet.NgayThuchien.Value.Date.ToString("dd/MM/yyyy"), KCB_CDHA_SONGAY_HUYKETQUA.ToString()));
                            return;
                        }
                    }

                    if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy kết quả của dịch vụ {0} hay không?", txtTendichvu.Text), "Xác nhận hủy", true))
                    {

                        objKcbChidinhclsChitiet.MarkOld();
                        objKcbChidinhclsChitiet.TrangThai = 0;
                        objKcbChidinhclsChitiet.KetLuanCdha = "";
                        objKcbChidinhclsChitiet.KetQua = "";
                        objKcbChidinhclsChitiet.NgayThuchien = null;
                        objKcbChidinhclsChitiet.NguoiThuchien = "";
                        objKcbChidinhclsChitiet.IdVungks = "-1";
                        using (var scope = new TransactionScope())
                        {
                            using (var sh = new SharedDbConnectionScope())
                            {
                                objKcbChidinhclsChitiet.Save();
                                SPs.ClsCdhaDelete(objKcbChidinhclsChitiet.IdChitietchidinh).Execute();
                            }
                            scope.Complete();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy kết quả cho bệnh nhân ID={0}, PID={1}, Tên={2}, Dịch vụ hủy KQ ={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, txtTendichvu.Text), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        }
                        Utility.ShowMsg("Đã hủy kết quả CĐHA thành công. Nhấn OK để kết thúc");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        #region Kê VTTH
        private void mnuKeVTTH_Click(object sender, EventArgs e)
        {

        }
        int Pres_ID = -1;
        GridEXRow RowThuoc = null;
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        
        private void ThemMoiDonThuoc()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.KieuDonthuoc = 4;
                frm.objLuotkham = objLuotkham;
                frm._KcbCDKL = null;
                frm._MabenhChinh = "";
                frm.id_chitietchidinh = objKcbChidinhclsChitiet.IdChitietchidinh;
                frm._Chandoan = "";
                frm.DtIcd = null;
                frm.dt_ICD_PHU = null;
                frm.id_kham = -1;
                frm.objCongkham = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan, "-1");
                frm.txtSoDT.Text = objBenhnhan.DienThoai;
                frm.txtPatientName.Text = objBenhnhan.TenBenhnhan;
                frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                frm.txtPres_ID.Text = "-1";
                frm.dtNgayKhamLai.MinDate = DateTime.Now;
                frm._ngayhenkhamlai = "";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();

                if (!frm.m_blnCancel)
                {
                   
                    LayDanhsachdonthuoc();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
                frm.Dispose();
                frm = null;
                GC.Collect();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();

            }
        }
        DataTable m_dtPresDetail = new DataTable();
        private void LayDanhsachdonthuoc()
        {
            try
            {
                m_dtPresDetail =
                     new KCB_THAMKHAM().KcbThamkhamLayDanhsachDonThuocTheolankham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, -1l, -1l, 4, "THUOC",objKcbChidinhclsChitiet.IdChitietchidinh, 0).Tables[0];
                Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtPresDetail, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        void ModifyCommmands()
        {
            cmdCreateNewPres.Enabled = objLuotkham != null && objKcbChidinhclsChitiet != null;
            cmdUpdatePres.Enabled = cmdDeletePres.Enabled = cmdPrintPres.Enabled = cmdWords.Enabled = Utility.isValidGrid(grdPresDetail) && objLuotkham != null && objKcbChidinhclsChitiet != null ;
        }
        /// <summary>
        /// Kiểm tra xem đã được tổng hợp cấp phát hoặc đã duyệt cấp phát hay chưa
        /// </summary>
        /// <param name="pres_id"></param>
        /// <returns></returns>
        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.RowCount > 0)//grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {


                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát thuốc tại phòng Dược");
                            return;
                        }
                        var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (v_collect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            var frm = new frm_KCB_KE_DONTHUOC("VT");
                            frm.em_Action = action.Update;
                            frm._KcbCDKL = null;
                            frm._MabenhChinh = "";
                            frm._Chandoan = "";
                            frm.DtIcd = null;
                            frm.dt_ICD_PHU = null;
                            frm.noitru = 0;
                            frm.objLuotkham = objLuotkham;
                            frm.id_kham = -1;
                            frm.objCongkham = null;
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objBenhnhan.IdBenhnhan, "-1");
                            frm.txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai, "");
                            frm.txtPatientName.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan, "");
                            frm.txtYearBirth.Text = Utility.sDbnull(objBenhnhan.NamSinh, "");
                            frm.txtSex.Text = Utility.sDbnull(objBenhnhan.GioiTinh, "");
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.dtNgayKhamLai.MinDate = globalVariables.SysDate;
                            frm._ngayhenkhamlai = globalVariables.SysDate.ToString("yyMMdd");

                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                LayDanhsachdonthuoc();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc, Utility.sDbnull(frm.txtPres_ID.Text));
                            }
                            frm.Dispose();
                            frm = null;
                            GC.Collect();
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        
        private void PerformActionDeletePres()
        {
            string s = "";
            var lstIdchitiet = new List<int>();
            if (grdPresDetail.GetCheckedRows().Count() <= 0 && RowThuoc != null)
            {
                try
                {
                    RowThuoc.BeginEdit();
                    RowThuoc.IsChecked = true;
                    RowThuoc.EndEdit();
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất 1 chi tiết thuốc để xóa");
                    return;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
            }
            if (lstIdchitiet.Count <= 0) return;
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            DataRow[] rows =
                         m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdChitietdonthuoc + " IN (" + String.Join(",", lstIdchitiet.ToArray()) + ")");
            string _deleteitems = string.Join(",", (from p in rows.AsEnumerable()
                                                    select Utility.sDbnull(p["ten_thuoc"])).ToList<string>());
            // UserName is Column Name
            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa đơn thuốc của bệnh nhân ID={0}, PID={1}, Tên={2}, DS thuốc xóa={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan, _deleteitems), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
            DeletefromDatatable(lstIdchitiet);
            m_dtPresDetail.AcceptChanges();
        }
        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                      KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                      + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }
        private void DeletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtPresDetail.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtPresDetail.Rows.Remove(p[i]);
                m_dtPresDetail.AcceptChanges();
            }
            catch
            {
            }
        }
        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các thuốc đang chọn hay không?", "Xác nhận xóa", true)) return false;
            if (RowThuoc == null)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") || globalVariables.IsAdmin ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các thuốc bạn chọn xóa, có một số thuốc được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int vIdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    KcbDonthuocChitiet kcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(vIdChitietdonthuoc);
                    if (kcbDonthuocChitiet != null && (Utility.Byte2Bool(kcbDonthuocChitiet.TrangthaiThanhtoan) ||
                         Utility.Byte2Bool(kcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }
        #endregion
        private void cmdHide_Click(object sender, EventArgs e)
        {
            if (grbInfor.Height == 120)
            {
                grbInfor.Height = 66;
                cmdHide.Image = global::VMS.HIS.Cls.Properties.Resources.down_01;
            }
            else
            {
                grbInfor.Height = 120;
                cmdHide.Image = global::VMS.HIS.Cls.Properties.Resources.Up01;
            }
        }

        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            try
            {
                cmdCreateNewPres.Enabled = false;
                if (objLuotkham == null) return;
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Không tồn tại bệnh nhân! Bạn cần nạp lại thông tin dữ liệu", "Thông Báo");
                    return;
                }

                ThemMoiDonThuoc();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                //cmdCreateNewPres.Enabled = true;
            }
        }

        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {

            if (!cmdUpdatePres.Enabled) return;

            if (RowThuoc != null)
            {
                Pres_ID = Utility.Int32Dbnull(Utility.getCellValuefromGridEXRow(RowThuoc, KcbDonthuocChitiet.Columns.IdDonthuoc), -1);// grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                if (Utility.Coquyen("quyen_suadonthuoc") || Utility.sDbnull(Utility.getCellValuefromGridEXRow(RowThuoc, KcbDonthuocChitiet.Columns.NguoiTao)) == globalVariables.UserName)
                {
                    UpdateDonThuoc();
                }
                else
                {
                    Utility.ShowMsg("Đơn thuốc đang chọn sửa được tạo bởi bác sĩ khác hoặc bạn không được gán quyền sửa(quyen_suadonthuoc). Vui lòng kiểm tra lại");
                    return;
                }
            }

        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!KiemtraThuocTruockhixoa()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }

        private void cmdViewPdf2_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(100);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = "";// Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();

          
        }

        private void mnuViewPDF_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null) return;
            frm_PdfViewer _PdfViewer = new frm_PdfViewer(100);
            _PdfViewer.ma_luotkham = objLuotkham.MaLuotkham;
            _PdfViewer.ma_chidinh = "";// Utility.sDbnull(RowCLS.Cells[KcbChidinhcl.Columns.MaChidinh].Value);
            _PdfViewer.ShowDialog();
        }

        private void cmdShowHistory_Click(object sender, EventArgs e)
        {
            isShowHistory = !isShowHistory;
            try
            {
                if (isShowHistory)
                    pnlHistoryCKEditor.Width = Convert.ToInt32(Math.Ceiling((decimal)((pnlCkeditor.Width-70) / 2)));
                else
                    pnlHistoryCKEditor.Width = 0;
            }
            catch (Exception)
            {

            }
        }

        private void cboDichvu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isShowHistory) return;
                long id_chitietchidinh = Utility.Int64Dbnull(cboDichvu.SelectedValue);
                DataTable dtData = Utility.ExecuteSql(string.Format("select * from KQCLS.dbo.kcb_ketqua_cdha where id_chitietchidinh={0}", id_chitietchidinh), CommandType.Text).Tables[0];
                if (dtData.Rows.Count > 0)
                {
                    txtNoidungHistory.Text = Utility.sDbnull(dtData.Rows[0]["mota_html"], "");
                    timer3.Start();
                    LoadHTMLHistory();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoidungHistory.Text)) ClickDataHistory();
        }
    }
}
