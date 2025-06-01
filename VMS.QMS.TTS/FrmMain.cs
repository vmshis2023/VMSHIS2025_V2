using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.QMS.DataAccessLayer;
using WMPLib;
using VietBaIT.CommonLibraryQMS;

namespace VietbaIT.QMS.GoiLoa
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
            if (new ConnectionSql().ReadConfig())
                Utility.InitSubSonic(new ConnectionSql().KhoiTaoKetNoi(), "ORM");
            timer2.Start();
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = @"Chương trình gọi loa";
            notifyIcon1.BalloonTipTitle = @"Welcome Message";
            notifyIcon1.ShowBalloonTip(2000);
            timer1.Start();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitMaLoa();
        }
        void player_PlayStateChange(int NewState)
        {
            if (NewState == 1)
            {
                this.player.controls.play();
            }

        }
        private void cmdGoiSo_Click(object sender, EventArgs e)
        {
            string noidung = txtnoidung.Text.Trim();
            player.URL =
           string.Format(
               "https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx",
                HttpUtility.UrlEncode(noidung), "vi", noidung.Length);
            double time = player.controls.currentPosition; //return always 0 for you, because you pause first and after get the value
            player.controls.play();
            Thread.Yield();
            while (player.playState == WMPLib.WMPPlayState.wmppsTransitioning)
            {
                Application.DoEvents();
                Thread.Yield();
            }
            int duration = Convert.ToInt32(player.currentMedia.duration * 1000);
            Thread.Sleep(duration);
        }
      
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        WMPPlayState Trangthai = WMPPlayState.wmppsPlaying;
        private void timer1_Tick(object sender, EventArgs e)
        {

            string noidung = "";
            if (grdListGoiLaiSoKham.RowCount <=0)
            {
                return;
            }
            else
            {
                int id = Utility.Int32Dbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsGoiLoa.Columns.Id].Value);
                noidung = Utility.sDbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsGoiLoa.Columns.NoiDung].Value);
                txtnoidung.Text = noidung;
                player.URL =
                    string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx",HttpUtility.UrlEncode(noidung), "vi", noidung.Length);
                double time = player.controls.currentPosition; //return always 0 for you, because you pause first and after get the value
                player.controls.play();
                Thread.Yield();
                while (player.playState == WMPPlayState.wmppsTransitioning)
                {
                    Application.DoEvents();
                    Thread.Yield();
                }
                int duration = Convert.ToInt32(player.currentMedia.duration * 1000);
                Thread.Sleep(duration);
                SPs.QmsGoiLoaUpdate(
                    Utility.Int32Dbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsGoiLoa.Columns.Id].Value), 1).Execute();
                grdListGoiLaiSoKham.CurrentRow.Delete();
            }
        }

 
        string s = "";
        void player_StatusChange()
        {
          
        }
        DataTable _dtGoiLoa = new DataTable();
        private void timer2_Tick(object sender, EventArgs e)
        {
            _dtGoiLoa = SPs.QmsGoiLoaGetAllByMaLoa(txtmaloa.Text.Trim(), 0).GetDataSet().Tables[0];
            if (_dtGoiLoa.Rows.Count > 0)
            {
                grdListGoiLaiSoKham.DataSource = _dtGoiLoa;
            }
            else
            {
                grdListGoiLaiSoKham.DataSource = null;
            }
        }
        void GoiSo()
        {

            //if (_queue.Count > 0)
            //{
            //    string noidung = _queue.Dequeue().ToString();
            //    player.URL =
            //   string.Format(
            //       "https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx",
            //        HttpUtility.UrlEncode(noidung), "vi", noidung.Length);
            //    player.settings.rate = 0.8;
            //    player.PlayStateChange += player_PlayStateChange;
            //    player.controls.play();
            //    while (player.playState != WMPPlayState.wmppsMediaEnded)
            //    {
            //        Application.DoEvents();
            //        Thread.Sleep(10);
            //    }
            //}
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
           
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000);
        }
        
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void hiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized )
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(2000);
        }

        void InitMaLoa()
        {
            string userPrintNumberFile = Application.StartupPath + "\\CAUHINH_LOA\\cauhinhloa.txt";
                if (File.Exists(userPrintNumberFile))
                {
                    txtmaloa.Text = File.ReadAllText(userPrintNumberFile);
                }
        }
        private void cmdsavemaloa_Click(object sender, EventArgs e)
        {
            if (txtmaloa.Text.Trim() != "")
            {
                    string userPrintNumberFile = Application.StartupPath + "\\CAUHINH_LOA\\cauhinhloa.txt";
                    string parentFolder = Path.GetDirectoryName(userPrintNumberFile);
                    File.Delete(userPrintNumberFile);
                    if (parentFolder != null && !Directory.Exists(parentFolder)) Directory.CreateDirectory(parentFolder);
                    bool isAppend = File.Exists(userPrintNumberFile);
                    using (var writer = new StreamWriter(userPrintNumberFile, isAppend))
                    {
                        writer.WriteLine(txtmaloa.Text.Trim());
                        writer.Flush();
                        writer.Close();
                    }
            }
        }

    }
}