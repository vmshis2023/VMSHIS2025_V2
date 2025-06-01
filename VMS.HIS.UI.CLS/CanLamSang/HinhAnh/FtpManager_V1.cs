using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using NLog.Config;
using NLog.Targets;
using NLog;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public class FtpManager_V1
    {
        public bool AutoStopWhenError = false;
        bool ClearItemsAfterFinished = false;
        public delegate void OnQueueChanged(int QueueCount);
        public event OnQueueChanged _OnQueueChanged;
        public delegate void OnSaving(string msg);
        public event OnSaving _OnSaving;
        frm_NhaptraKQ_V1 _parent = null;
        public delegate void Onfinish(int AssignDetailId, List<string> listNew, List<string> listAnh, List<string> listsFTPName, List<int> listIdHinhAnh, List<string> listNewRadio);
        public event Onfinish _Onfinish;

        public NLog.Logger log = null;
        public FtpManager_V1(frm_NhaptraKQ_V1 _parent)
        {
            this._parent = _parent;
            InitLogs();
            log = LogManager.GetLogger("FTPLogs");
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
        bool isSending = false;
        bool IsSuspending = false;
        Queue queue = new Queue();
        public void StartUp()
        {
            StartSending();
        }
        public void UpdateAction(bool ClearItemsAfterFinished)
        {
            this.ClearItemsAfterFinished = ClearItemsAfterFinished;
        }
        public void AddItems(FtpItem _newItem)
        {
            try
            {
                queue.Enqueue(_newItem);
                if (!isSending) StartUp();
            }
            catch
            {
            }
        }
        public void AddItems2Queu(FtpItem _newItem)
        {
            try
            {
                queue.Enqueue(_newItem);
                if (!isSending) StartUp();
            }
            catch
            {
            }
        }
        private void cmdResendErrors_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
            }

        }
        public void StartSending()
        {
            try
            {
                //while (true)
                //{
                Thread.Sleep(10);
                if (queue.Count > 0)
                {
                    FtpItem item = queue.Dequeue() as FtpItem;
                    item._OnSaving += item__OnSaving;
                    item.DoSendFTP(log);

                    while (item.isSending)
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    }
                    //if (_Onfinish != null) _Onfinish
                    _parent.ReloadAfterFinish(item.AutoLoad, item.ID_Detail, item.listNew, item.listAnh, item.listsFTPName, item.listIdHinhAnh, item.listNewRadio);
                }
                //}
            }
            catch (Exception ex)
            {
                log.Trace("_BackgroundWorker_DoWork.Exception--> " + ex.Message);
            }

            //try
            //{
            //    using (BackgroundWorker _BackgroundWorker = new BackgroundWorker())
            //    {
            //        _BackgroundWorker.WorkerReportsProgress = true;
            //        _BackgroundWorker.WorkerSupportsCancellation = true;
            //        _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
            //        _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_BackgroundWorker_ProgressChanged);
            //        _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
            //        if (!_BackgroundWorker.IsBusy)
            //            _BackgroundWorker.RunWorkerAsync();
            //    }
            //}
            //catch
            //{
            //}
        }

        void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        void _BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

            }
            catch
            {
            }
        }

        void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (queue.Count > 0)
                    {
                        FtpItem item = queue.Dequeue() as FtpItem;
                        item._OnSaving += item__OnSaving;
                        item.DoSendFTP(log);

                        while (item.isSending)
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                        }
                        //if (_Onfinish != null) _Onfinish
                        _parent.ReloadAfterFinish(item.AutoLoad, item.ID_Detail, item.listNew, item.listAnh, item.listsFTPName, item.listIdHinhAnh, item.listNewRadio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Trace("_BackgroundWorker_DoWork.Exception--> " + ex.Message);
            }
        }

        void item__OnSaving(string msg)
        {
            if (_OnSaving != null) _OnSaving(msg);
        }
        void Try2ClearItems(FtpItem item)
        {
            try
            {


            }
            catch
            {
            }
        }
        void item__OnAction(string sAction, string sLogText, Color sActionColor)
        {
        }
        void modifyButton(bool _enable)
        {
            try
            {

            }
            catch
            {
            }
        }
        private void cmdResendAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (queue.Count > 0)
                {
                    queue.Clear();
                }
                IsSuspending = true;

                IsSuspending = false;
            }
            catch
            {
            }
        }
        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
            }
        }
        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
            }
        }

        private void cmdStop_Click_1(object sender, EventArgs e)
        {
            IsSuspending = true;
            if (queue.Count > 0)
            {
                queue.Clear();
            }
            IsSuspending = false;
        }
    }
}
