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
using VMS.Emr;
using NLog;

namespace VMS.HIS.Bus
{
    public class Pdf2emrItem
    {
        public bool Result = false;
        public bool isSending = false;
        NLog.Logger log = null;
        private readonly string _baseDirectoryPdf = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "emr\\");
     FTPclient FtpClientPDF;
     Document doc;
        string EMR_LoaiPhieu = "";
     string fileKetqua;
        EmrDocuments emrdoc;
     private string _CurrentDirectory;
        public Pdf2emrItem(EmrDocuments emrdoc,  Document doc, string fileKetqua)
        {
            this.FtpClientPDF = FtpClientPDF;
            this._baseDirectoryPdf = _baseDirectoryPdf;
            this.doc = doc;
            this.emrdoc = emrdoc;
            this.fileKetqua = fileKetqua;
            this.EMR_LoaiPhieu = EMR_LoaiPhieu;
        }
        private void InitFtp()
        {
            try
            {
                string FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("FTP_EMR_SERVER", string.Format("{0}-{1}-{2}", "127.0.0.1", "emr", "emr"), true);
                string FTPServer = "";
                string UID = "";
                string PWD = "";
                if (FTPInfor.Length > 0 && FTPInfor.Split('-').Count() == 3)
                {
                    FTPServer = FTPInfor.Split('-')[0];
                    UID = FTPInfor.Split('-')[1];
                    PWD = FTPInfor.Split('-')[2];
                }
                FtpClientPDF = new FTPclient(FTPServer, "pdf2emr", "pdf2emr");
                FtpClientPDF.UsePassive = true;
                _CurrentDirectory = FtpClientPDF.CurrentDirectory;
            }
            catch
            {
            }
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
        public Pdf2emrItem()
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
                //string FtpClientCurrentDirectoryPdf = FtpClientCurrentDirectoryScan + "//" +ma_luotkham+"//"+ ngay_chidinh +"//"+folder;//Thư mục+ngày+mã phiếu+ file
                string FtpClientCurrentDirectoryPdf = _CurrentDirectory + "//" + emrdoc.objDoc.MaLuotkham;//Thư mục+ngày+mã phiếu+ file
                if (!FtpClientPDF.FtpDirectoryExists(FtpClientCurrentDirectoryPdf))
                    FtpClientPDF.FtpCreateDirectory(FtpClientCurrentDirectoryPdf);

                string uploadDirectory = string.Format("{0}/{1}", FtpClientCurrentDirectoryPdf, fileName);
                FtpClientPDF.CurrentDirectory = FtpClientCurrentDirectoryPdf;
                log.Trace(string.Format("sourcePath={0}uploadDirectory={1}", sourcePath, uploadDirectory));
                FtpClientPDF.Upload(sourcePath, uploadDirectory);
                emrdoc.SetFilePath(fileName);
                emrdoc.Save();
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
            if(log==null) this.log= LogManager.GetLogger("EmrDocuments");
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
                    log.Trace("you have just canceled storing pdf2emr successfully");
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
           
                
                Send2Pdf();
           

        }

        void Send2Pdf()
        {
            
            string pdf2emrfile = string.Format(@"{0}\{1}_{2}.pdf", Path.GetDirectoryName(fileKetqua), EMR_LoaiPhieu, Guid.NewGuid().ToString("N"));
            try
            {
                
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(fileKetqua);
                doc.Activate();
                doc.SaveAs2(pdf2emrfile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                doc.Close();
                CreateFtpPdf(pdf2emrfile);
            }
            catch (Exception ex)
            {
                PdfSaveOptions saveOptions = new PdfSaveOptions { Compliance = PdfCompliance.Pdf15 };
                doc.Save(pdf2emrfile,SaveFormat.Pdf);
                log.Error(ex.ToString());
            }
            finally
            {
                File.Delete(fileKetqua);
            }
        }
        

    }
    
}
