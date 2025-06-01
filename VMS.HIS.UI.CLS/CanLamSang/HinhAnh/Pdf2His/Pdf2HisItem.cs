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
        string IdChitietchidinh = "";
        NLog.Logger log = null;
        private readonly string _baseDirectoryPdf = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Pdf2His\\");
     FTPclient FtpClientPDF;
     Document doc;
     string so_phieu = "";
     string fileKetqua;
     private string FtpClientCurrentDirectoryScan;
     string ngay_chidinh = DateTime.Now.ToString("yyyy_MM_dd");
     public Pdf2HisItem(string ngay_chidinh,string IdChitietchidinh, FTPclient FtpClientPDF, string _baseDirectoryPdf, Document doc, string so_phieu, string fileKetqua, string FtpClientCurrentDirectoryScan)
        {
            this.ngay_chidinh = ngay_chidinh;
            this.IdChitietchidinh = IdChitietchidinh;
            this.FtpClientPDF = FtpClientPDF;
            this._baseDirectoryPdf = _baseDirectoryPdf;
            this.doc = doc;
            this.so_phieu = so_phieu;
            this.fileKetqua = fileKetqua;
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
        
        private string CreateFtpPdf(string sourcePath, string folder)
        {
            try
            {
                log.Trace("Begin Ftp pdf...");
                //if (!_myProperties.EnabledFTP)
                //{
                //    return sourcePath;
                //}
                string fileName = Path.GetFileName(sourcePath);
                string FtpClientCurrentDirectoryPdf = FtpClientCurrentDirectoryScan + "//" +ngay_chidinh +"//"+folder;//Thư mục+ngày+mã phiếu+ file
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
           
                
                Send2Pdf();
           

        }

        void Send2Pdf()
        {
            string pdf2hisfile = string.Format(@"{0}\{1}.pdf", Path.GetDirectoryName(fileKetqua), IdChitietchidinh);
            try
            {
                
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(fileKetqua);
                doc.Activate();
                doc.SaveAs2(pdf2hisfile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                doc.Close();
                CreateFtpPdf(pdf2hisfile, so_phieu);
            }
            catch (Exception ex)
            {
                PdfSaveOptions saveOptions = new PdfSaveOptions { Compliance = PdfCompliance.Pdf15 };
                doc.Save(pdf2hisfile,SaveFormat.Pdf);
                log.Error(ex.ToString());
            }
            finally
            {
                File.Delete(fileKetqua);
            }
        }
        

    }
    
}
