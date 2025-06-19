using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using SubSonic;
using Aspose.Words;
using VNS.Libs;
using VMS.HIS.DAL;
using Aspose.Words.Saving;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public class Pdf2HisItem
    {
        public bool Result = false;
        public bool isSending = false;
        string nguoi_tao = "";
        NLog.Logger log = null;
        private readonly string _baseDirectoryPdf = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "AttatchmentFiles\\");
     FTPclient FtpClientPDF;
     private string FtpClientCurrentDirectoryScan;
     string ngay_tao = DateTime.Now.ToString("yyyy_MM_dd");
        string ma_luotkham = "";
        List<EmrDocument> lstDoc = new List<EmrDocument>();
     public Pdf2HisItem(FTPclient FtpClientPDF, string _baseDirectoryPdf, List<EmrDocument> lstDoc, string FtpClientCurrentDirectoryScan)
        {
            ma_luotkham = lstDoc.FirstOrDefault().MaLuotkham;
            this.FtpClientPDF = FtpClientPDF;
            this._baseDirectoryPdf = _baseDirectoryPdf;
            this.lstDoc = lstDoc;
            this.FtpClientCurrentDirectoryScan = FtpClientCurrentDirectoryScan;
        }
       
        public void Reset()
        {
            try
            {
                isSending = false;
                Result = false;
            }
            catch
            {
            }
        }
        public Pdf2HisItem()
        {
        }
        
        private string CreateFtpPdf(string sourcePath)
        {
            try
            {
                log.Trace("Begin Ftp pdf...");
                //if (!_myProperties.EnabledFTP)
                //{
                //    return sourcePath;
                //}
                string fileName = Path.GetFileName(sourcePath);
                string FtpClientCurrentDirectoryPdf = FtpClientCurrentDirectoryScan + "//" + ma_luotkham;//Thư mục+ngày+mã phiếu+ file
                if (!FtpClientPDF.FtpDirectoryExists(FtpClientCurrentDirectoryPdf))
                    FtpClientPDF.FtpCreateDirectory(FtpClientCurrentDirectoryPdf);

                string uploadDirectory = string.Format("{0}/{1}", FtpClientCurrentDirectoryPdf, fileName);
                FtpClientPDF.CurrentDirectory = FtpClientCurrentDirectoryPdf;
                log.Trace(string.Format("sourcePath={0}uploadDirectory={1}", sourcePath, uploadDirectory));
                FtpClientPDF.Upload(sourcePath, uploadDirectory);
                return fileName;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.ToString());
                return "";
            }
        }


        public bool _closing = false;
        bool expand = false;


        bool _cancel = false;
        bool isS2Sing = true;
        public void DoPdf(NLog.Logger log)
        {
            this.log = log;
            
            isSending = true;
            try
            {

                Send2Pdf();
            }
            catch (Exception ex)
            {

                log.Error(ex.ToString());
            }
            finally
            {
                isSending = false;
            }
        }
        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // UIAction._EnableControl(cmdResend, true, "Resend");

                if (e.Cancelled)
                {
                    log.Trace("you have just canceled storing pdf2his successfully");
                }
                else if (e.Error != null)
                {
                    log.Trace("backgroundworker is failed. Please run again!");
                }
                else
                    log.Trace("backgroundworker finished. Congratulation!");
            }
            catch
            {
            }
        }

        void _BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                // lblImportPercentage.Text = e.ProgressPercentage.ToString() + " %";

            }
            catch
            {
            }
        }

        void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            foreach (EmrDocument emrdoc in lstDoc)
            {
                emrdoc.Save();
                Send2Pdf(emrdoc.FileIn);
            }
           

        }

        void Send2Pdf(string pdf2hisfile)
        {
            try
            {
                CreateFtpPdf(pdf2hisfile);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
            }
        }
        

    }
    
}
