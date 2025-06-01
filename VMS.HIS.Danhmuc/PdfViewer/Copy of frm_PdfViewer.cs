using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using SubSonic;
namespace VMS.HIS.Danhmuc
{
    public partial class frm_PdfViewer : Form
    {
        public string ma_luotkham = "";
        public string ma_chidinh = "";
        private Logger _log;
        public FTPclient FtpClientRIS;
        public FTPclient FtpClientLIS;
        private string FtpClientCurrentDirectoryRIS = "";
        private readonly string _baseDirectoryRIS = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "pdfRIS\\");

        private string FtpClientCurrentDirectoryLIS = "";
        private string _baseDirectoryLIS = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "pdfLIS\\");
        byte noitru = 0;
        public frm_PdfViewer(byte noitru)
        {
            InitializeComponent();
            this.noitru = noitru;
            Utility.SetVisualStyle(this);
            //globalVariables.File_WebPath = BusinessHelper.WeServicebPath("FILE_API", "KKB", "BV01");
            txtMaluotkham.Visible = btnSearch.Visible = globalVariables.IsAdmin;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtmachidinh.KeyDown += txtmachidinh_KeyDown;
            optMachidinh.CheckedChanged += _CheckedChanged;
            optMaluotkham.CheckedChanged += _CheckedChanged;
            _log = LogManager.GetCurrentClassLogger();
            InitFtp();
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtData = new DataTable();
            if (optMaluotkham.Checked && Utility.sDbnull( ma_luotkham).Length>0)
            {
                 dtData = SPs.ClsChidinhLaythongtinpdf(ma_luotkham, "", noitru).GetDataSet().Tables[0];
            }
            else if (optMachidinh.Checked && Utility.sDbnull(ma_chidinh).Length > 0)
            {
                 dtData = SPs.ClsChidinhLaythongtinpdf("", ma_chidinh, noitru).GetDataSet().Tables[0];
            }
            Utility.SetDataSourceForDataGridEx_Basic(grdKQ, dtData, true, true, "1=1", "STT");
        }

       

        void txtmachidinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (Utility.sDbnull(txtmachidinh.Text).Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập mã lượt khám cụ thể");
                        return;
                    }
                    webBrowser1.Navigate("");
                    SysSystemParameter _p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("DEFAULTYYMMDD").ExecuteSingle<SysSystemParameter>();
                    if (!txtmachidinh.Text.Trim().Contains("."))
                    {
                        if (_p != null && _p.SValue != null && _p.SValue.TrimStart().TrimEnd().Length > 0 && _p.SValue.TrimStart().TrimEnd() != "TODAY")
                        {
                            txtmachidinh.Text = _p.SValue.TrimStart().TrimEnd() + "." + Strings.Right("000000" + Utility.sDbnull(txtmachidinh.Text, "0"), 4);
                        }
                        else
                            txtmachidinh.Text = DateTime.Now.ToString("yyMMdd") + "." + Strings.Right("000000" + Utility.sDbnull(txtmachidinh.Text, "0"), 4);
                    }
                    DataTable dtData = SPs.ClsChidinhLaythongtinpdf("", Utility.sDbnull(txtmachidinh.Text), 100).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx_Basic(grdKQ, dtData, true, true, "1=1", "STT");
                }
            }
            catch (Exception ex)
            {


            }

        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                if (e.KeyCode == Keys.Enter)
                {
                    if (Utility.sDbnull(txtMaluotkham.Text).Length <= 0)
                    {
                        Utility.ShowMsg("Bạn cần nhập mã lượt khám cụ thể");
                        return;
                    }
                    webBrowser1.Navigate("");
                    txtMaluotkham.Text = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                    DataTable dtData = SPs.ClsChidinhLaythongtinpdf(Utility.sDbnull(txtMaluotkham.Text), "", 100).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx_Basic(grdKQ, dtData, true, true, "1=1", "STT");
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void InitFtp()
        {
            try
            {
                List<string> FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_PDFRIS", string.Format("{0}-{1}-{2}", "127.0.0.1", "pdf2his", "pdf2his"), true).Split('-').ToList<string>();

                FtpClientRIS = new FTPclient(FTPInfor[0], FTPInfor[1], FTPInfor[2]);
                FtpClientRIS.UsePassive = true;
                FtpClientCurrentDirectoryRIS = FtpClientRIS.CurrentDirectory;
                if (!Directory.Exists(_baseDirectoryRIS))
                {
                    Directory.CreateDirectory(_baseDirectoryRIS);
                }
                FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_PDFLIS", string.Format("{0}-{1}-{2}", "127.0.0.1", "pdf2his", "pdf2his"), true).Split('-').ToList<string>();
                FtpClientLIS = new FTPclient(FTPInfor[0], FTPInfor[1], FTPInfor[2]);
                FtpClientLIS.UsePassive = true;
                FtpClientCurrentDirectoryLIS = FtpClientLIS.CurrentDirectory;
                if (!Directory.Exists(_baseDirectoryLIS))
                {
                    Directory.CreateDirectory(_baseDirectoryLIS);
                }
            }
            catch
            {
            }
        }
        void SearchData()
        {
            try
            {
                DataTable dtData = SPs.ClsChidinhLaythongtinpdf(ma_luotkham, ma_chidinh,noitru).GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx_Basic(grdKQ, dtData, true, true, "1=1", "STT");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        //public static IEnumerable<string> GetFilesInFtpDirectory(string url, string username, string password)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        // Get the object used to communicate with the server.
        //        var request = (FtpWebRequest)WebRequest.Create(url);
        //        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        //        request.Credentials = new NetworkCredential(username, password);

        //        using (var response = (FtpWebResponse)request.GetResponse())
        //        {
        //            using (var responseStream = response.GetResponseStream())
        //            {
        //                var reader = new StreamReader(responseStream);
        //                while (!reader.EndOfStream)
        //                {
        //                    var line = reader.ReadLine();
        //                    if (string.IsNullOrWhiteSpace(line) == false)
        //                    {
        //                        yield return line.Split(new[] { ' ', '\t' }).Last();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Utility.CatchException(ex);
        //    }
           
        //}
        public static string[] GetFiles(string ftpServer, NetworkCredential Credentials, SearchOption searchOption)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpServer);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = Credentials;
            List<string> files = new List<string>();
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new System.IO.StreamReader(responseStream);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) == false)
                        {
                            if (!line.Contains("<DIR>"))
                            {
                                string[] details = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                string file = line.Replace(details[0], "")
                                    .Replace(details[1], "")
                                    .Replace(details[2], "")
                                    .Trim();
                                files.Add(file);
                            }
                            else
                            {
                                if (searchOption == SearchOption.AllDirectories)
                                {
                                    string dirName = line.Split(
                                            new string[] { "<DIR>" },
                                            StringSplitOptions.RemoveEmptyEntries
                                            ).Last().Trim();
                                    string dirFullName = String.Format("{0}/{1}", ftpServer.Trim('/'), dirName);
                                    files.AddRange(GetFiles(dirFullName, Credentials, searchOption));
                                }
                            }
                        }
                    }
                }
            }
            return files.ToArray();
        }
        void LoadPdf(string ngay_chidinh, string ma_chidnh, FTPclient ftp, string baseDirectory, string FtpDir2Scan, string fileName)
        {
            this.Cursor = Cursors.WaitCursor;
            GC.Collect();
            string oldTitle = this.Text;
            try
            {
                if (!Directory.Exists(baseDirectory))
                    Directory.CreateDirectory(baseDirectory);

                string ftpPath = FtpDir2Scan + ngay_chidinh + "/" + ma_chidnh;
                if (ftp.FtpFileExists(ftpPath))
                {
                    List<string> lstFiles = GetFiles(ftp.Hostname, new NetworkCredential(ftp.Username, ftp.Password), SearchOption.AllDirectories).ToList<string>();
                }


            }
            catch (Exception ex1)
            {
                // Utility.ShowMsg(ex1.Message);

            }
            finally
            {

                this.Cursor = Cursors.Default;
                this.Text = oldTitle;
            }
        }
        private string pathPdf = "";
        
       
        private void frm_PdfViewer_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserConfigs();
                SearchData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = @"PDF Files(*.pdf) |*.pdf;";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
            {
                webBrowser1.Navigate(openFileDialog.FileName); 
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoFirstPage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoPreviousPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoNextPage();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //axAcroPDF.gotoLastPage();
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            //axAcroPDF.setCurrentPage(Convert.ToInt32(nudPage.Value));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
        }

        private void grdThongTin_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdKQ)) return;
                string fileName = grdKQ.GetValue("duongdan_file").ToString();
                string ma_nhom= grdKQ.GetValue("ma_nhom").ToString();
                string localFile="";
                string ftpFile = "";
                if (ma_nhom == "XN")
                {
                    localFile = string.Format(@"{0}{1}", _baseDirectoryLIS, fileName.Replace(@"/", @"\"));
                    ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryLIS, fileName);
                }
                else
                {
                    localFile = string.Format(@"{0}{1}", _baseDirectoryRIS, fileName.Replace(@"/",@"\"));
                    ftpFile = string.Format(@"{0}{1}", FtpClientCurrentDirectoryRIS, fileName);
                }
                string parentFolder = Path.GetDirectoryName(localFile);
                Utility.Try2CreateFolder(Directory.GetParent(parentFolder).FullName);
                Utility.Try2CreateFolder(parentFolder);
                if (File.Exists(localFile))
                {
                    if (chkForced2Download.Checked)
                    {
                        if (ma_nhom == "XN")
                            FtpClientLIS.Download(ftpFile, localFile, true);
                        else
                            FtpClientRIS.Download(ftpFile, localFile, true);
                    }
                }
                else//Download and open
                {
                    if (ma_nhom == "XN")
                        FtpClientLIS.Download(ftpFile, localFile, true);
                    else
                        FtpClientRIS.Download(ftpFile, localFile, true);
                }
                string Url = string.Format("{0}?zoom=100%#navpanes=1&toolbar=1", localFile);
                this.Text = string.Format("Xem kết quả PDF từ file: {0}", Url);
                if (File.Exists(localFile))
                    webBrowser1.Navigate(Url);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate(string.Format("{0}#zoom={1}%&navpanes=1&toolbar=1",pathPdf, numericUpDown1.Text)); 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataTable dt = SPs.KetquaLaydanhsachFile(txtMaluotkham.Text).GetDataSet().Tables[0];
            //Utility.SetDataSourceForDataGridEx(grdKQ, dt, true, true, "", "");
        }
        void LoadUserConfigs()
        {
            try
            {
                chkForced2Download.Checked = Utility.getUserConfigValue(chkForced2Download.Tag.ToString(), Utility.Bool2byte(chkForced2Download.Checked)) == 1;
              
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
                Utility.SaveUserConfig(chkForced2Download.Tag.ToString(), Utility.Bool2byte(chkForced2Download.Checked));
              
            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void frm_PdfViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        private void frm_PdfViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.P) || e.KeyCode == Keys.F4) cmdPrint.PerformClick();
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.KeyCode == Keys.O && e.Control) cmdOpen.PerformClick();
            else if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "PDF Files|*.pdf";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    webBrowser1.Navigate(ofd.FileName);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Print();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

      
    }
    public class FileInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileGroup { get; set; }
    }

    public class ResponseFile
    {
        public bool IsSuccess { get; set; }
        public string Messge { get; set; }
        public FileAudio data { get; set; }
    }

    public class FileAudio
    {

        // Properties
        public byte[] fileByte { get; set; }
        public string fileName { get; set; }
    }
}
