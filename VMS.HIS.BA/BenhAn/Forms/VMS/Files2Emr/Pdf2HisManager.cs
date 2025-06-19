using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
namespace VNS.HIS.UI.Forms.HinhAnh
{
    public class Pdf2HisManager
    {
         public bool AutoStopWhenError = false;
        bool ClearItemsAfterFinished = false;
        public delegate void OnQueueChanged(int QueueCount);
        public event OnQueueChanged _OnQueueChanged;
        public NLog.Logger log = null;
        public Pdf2HisManager()
        {
            
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
        public void AddItems(Pdf2HisItem _newItem)
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
        public void AddItems2Queu(Pdf2HisItem _newItem)
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
                using (BackgroundWorker _BackgroundWorker = new BackgroundWorker())
                {
                    _BackgroundWorker.WorkerReportsProgress = true;
                    _BackgroundWorker.WorkerSupportsCancellation = true;
                    _BackgroundWorker.DoWork += new DoWorkEventHandler(_BackgroundWorker_DoWork);
                    _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_BackgroundWorker_ProgressChanged);
                    _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BackgroundWorker_RunWorkerCompleted);
                    if (!_BackgroundWorker.IsBusy)
                        _BackgroundWorker.RunWorkerAsync();
                }
            }
            catch
            {
            }
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
                    if (_OnQueueChanged != null) _OnQueueChanged(queue.Count);
                    if (queue.Count > 0)
                    {
                        Pdf2HisItem item = queue.Dequeue() as Pdf2HisItem;
                        item.DoPdf(log);
                        while (item.isSending)
                        {
                            Thread.Sleep(10);
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Trace("_BackgroundWorker_DoWork.Exception--> " + ex.Message);
            }
        }
        void Try2ClearItems(Pdf2HisItem item)
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
