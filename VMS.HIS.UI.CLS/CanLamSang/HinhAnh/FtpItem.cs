using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public class FtpItem
    {
        public bool Result = false;
        public bool isSending = false;
        public List<string> listNew = new List<string>();
        public List<string> listNewRadio = new List<string>();
        public List<long> listIdHinhAnh = new List<long>();
        public delegate void OnSaving(string msg);
        public event OnSaving _OnSaving;
        NLog.Logger log = null;
        public FTPclient FtpClient;
        public string FtpClientCurrentDirectory = "";
        string _baseDirectory = "";
        public long ID_Detail = -1;
        public bool AutoLoad = false;
        public FtpItem(bool AutoLoad, List<string> listAnh, List<string> listsFTPName, List<string> listNew, long ID_Detail, FTPclient FtpClient, string FtpClientCurrentDirectory, string _baseDirectory)
        {
            this.AutoLoad = AutoLoad;
            this.FtpClient = FtpClient;
            this._baseDirectory = _baseDirectory;
            this.listNew = listNew;
            this.ID_Detail = ID_Detail;
            this.listsFTPName = listsFTPName;
            this.listAnh = listAnh;
            this.FtpClientCurrentDirectory = FtpClientCurrentDirectory;
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
        public FtpItem()
        {
        }
        public bool _closing = false;
        bool expand = false;


        bool _cancel = false;
        bool isS2Sing = true;
        public void DoSendFTP(NLog.Logger log)
        {
            this.log = log;
            if (ID_Detail == -1)
            {
                log.Trace("ID_Detail==-1");
                return;
            }
            isSending = true;
            try
            {
                Send2FTP();
            }
            catch (Exception)
            {

            }
            finally
            {
                isSending = false;
            }

            //using (BackgroundWorker _BackgroundWorker = new BackgroundWorker())
            //{
            //    _BackgroundWorker.WorkerReportsProgress = true;
            //    _BackgroundWorker.WorkerSupportsCancellation = true;
            //    _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
            //    _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_BackgroundWorker_ProgressChanged);
            //    _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
            //    if (!_BackgroundWorker.IsBusy)
            //        _BackgroundWorker.RunWorkerAsync();
            //}
        }
        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // UIAction._EnableControl(cmdResend, true, "Resend");

                if (e.Cancelled)
                {
                    log.Trace("you have just canceled FTP sending");
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
            finally
            {
                isSending = false;
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
            try
            {
                Send2FTP();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #region FTP

        private string CreateFtp(string sourcePath)
        {
            try
            {

                string fileName = "";
                string newDirName = ID_Detail.ToString();
                string ftpCurrentDirectory = FtpClientCurrentDirectory + "//" + newDirName;
                if (!FtpClient.FtpDirectoryExists(ftpCurrentDirectory))
                    FtpClient.FtpCreateDirectory(ftpCurrentDirectory);
                fileName = ID_Detail + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(sourcePath);
                string uploadDirectory = string.Format("{0}/{1}", ftpCurrentDirectory, fileName);
                FtpClient.CurrentDirectory = FtpClientCurrentDirectory;
                FtpClient.Upload(sourcePath, uploadDirectory);
                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }


        }
        void copyImage2Local(int idx, string localimage, string ftpimage)
        {
            try
            {
                if (!Directory.Exists(_baseDirectory + ID_Detail.ToString()))
                    Directory.CreateDirectory(_baseDirectory + ID_Detail.ToString());
                if (File.Exists(localimage))
                {
                    string radiofile = _baseDirectory + ID_Detail.ToString() + "\\" + ftpimage;
                    File.Copy(localimage, radiofile);
                    listNewRadio.Add(radiofile);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public List<string> listAnh = new List<string>();
        public List<string> listsFTPName = new List<string>();
        void Send2FTP()
        {
            try
            {
                int idx = 0;
                log.Trace("Begin sending...");
                foreach (string filename in listNew)
                {
                    log.Trace(filename);
                    if (_OnSaving != null)
                        _OnSaving(string.Format("Đang lưu {0}/{1} :{2} lên máy chủ FTP", (idx + 1).ToString(), listNew.Count.ToString(), filename));
                    Application.DoEvents();
                    if (!listAnh.Contains(filename))
                    {
                        log.Trace("Ftp begin...");
                        string sFtpName = CreateFtp(filename);
                        string radiofile = _baseDirectory + ID_Detail.ToString() + "\\" + sFtpName;
                        listAnh.Add(radiofile);
                        listsFTPName.Add(sFtpName);
                        KcbKetquaHa objKcbKetquaHa = new KcbKetquaHa();
                        objKcbKetquaHa.IdChiTietChiDinh = ID_Detail;
                        objKcbKetquaHa.Chonin = 0;
                        objKcbKetquaHa.Vitri = Utility.GetVitriHinhAnhByName(filename);
                        objKcbKetquaHa.Mota = "";
                        objKcbKetquaHa.TenAnh = sFtpName;
                        objKcbKetquaHa.DuongdanLocal = filename;
                        objKcbKetquaHa.Save();
                        listIdHinhAnh.Add(objKcbKetquaHa.Id);
                        copyImage2Local(idx, filename, sFtpName);
                        Application.DoEvents();
                        log.Trace("Ftp end...");
                    }
                    else
                        log.Trace(string.Format("Ignored file {0}", filename));
                    idx += 1;
                }
                log.Trace("Finish sending...");
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
            }

        }
        #endregion

    }
}
