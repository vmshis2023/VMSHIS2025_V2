using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using SubSonic;
using WMPLib;
using VNS.Libs;
using VMS.QMS.DAL;
using System.ServiceModel;
using System.Configuration;
using System.Xml;
namespace VMS.QMS.GoiLoa
{
    public partial class FrmMain_New : Form
    {
        public FrmMain_New()
        {
            InitializeComponent();
            timer2.Interval = Utility.Int32Dbnull(ConfigurationManager.AppSettings["thoigian_refresh"], 3000);
            timer1.Interval = Utility.Int32Dbnull(ConfigurationManager.AppSettings["thoigian_goi_googleTTS"], 5000);
            
        }

        private void FrmMain_New_Load(object sender, EventArgs e)
        {
            try
            {
BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
			{
				Name = "BasicHttpBinding_ISpeaker",
				Security = 
				{
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.None
					},
					Message = 
					{
						ClientCredentialType = BasicHttpMessageCredentialType.UserName
					}
				},
                MaxBufferPoolSize = Int32.MaxValue,
                MaxBufferSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                ReaderQuotas = new XmlDictionaryReaderQuotas()
                {
                    MaxArrayLength = 200000000,
                    MaxDepth = 32,
                    MaxStringContentLength = 200000000
                }
			};
           EndpointAddress remoteAddress = new EndpointAddress( ConfigurationManager.AppSettings["TTSURL"]);
            _ttsClient = new TTS.TTS.TTSClient(binding, remoteAddress);
            dotre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["do_tre"], 1);
            thoigiantre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["thoigian_cho"], 1);
            txtmaloa.Text = ConfigurationManager.AppSettings["ma_loa"];
            txtDotre.Text = dotre.ToString();
            txtThoigiancho.Text = thoigiantre.ToString();
            InitMaLoa();
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = @"Chương trình gọi loa";
            notifyIcon1.BalloonTipTitle = @"Welcome Message";
            notifyIcon1.ShowBalloonTip(2000);
            cmdStop.PerformClick();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        void player_PlayStateChange(int NewState)
        {
            if (NewState == 1)
            {
                this.player.controls.play();
            }

        }
        private string _path = Application.StartupPath+@"\media";
        TTS.TTS.TTSClient _ttsClient;
        private void cmdGoiSo_Click(object sender, EventArgs e)
        {
            try
			{
                dotre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["do_tre"], 1);
                thoigiantre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["thoigian_cho"], 1);
				string value = txtnoidung.Text;
				if (!string.IsNullOrEmpty(value))
				{
					string fileName = "";
					string messge = "";
					txtnoidung.Text = value;
                    byte[] array = _ttsClient.SaveAudio(txtnoidung.Text, ref fileName, ref messge);
                    using (FileStream fileStream = File.Open(string.Format("{0}\\{1}.mp3", _path, fileName), FileMode.Create))
					{
						fileStream.Position = 100L;
						fileStream.Write(array, 0, array.Length);
					}
					player.URL = string.Format("{0}\\{1}.mp3",_path,fileName);
                    double rate = Utility.DoubletoDbnull(dotre, 1);
					player.settings.rate = rate;
					int millisecondsTimeout = 0;
					player.controls.play();
					while (player.playState == WMPPlayState.wmppsTransitioning)
					{
						Application.DoEvents();
						Thread.Yield();
                        millisecondsTimeout = Convert.ToInt32(Math.Ceiling(player.currentMedia.duration));
                    }
                    Thread.Sleep(millisecondsTimeout * 1000 + thoigiantre);
				}
			}
			catch (Exception ex)
			{
				Utility.ShowMsg(ex.Message);
			}
        }
      
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        WMPPlayState Trangthai = WMPPlayState.wmppsPlaying;
        int dotre = 1;
        int thoigiantre = 100;
        private string _strList = "-1";
        string ma_loa = "-1";
        private void timer1_Tick(object sender, EventArgs e)
        {

           try
			{
				string text = "";
				if (grdListGoiLaiSoKham.RowCount <= 0)
				{
					return;
				}
                dotre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["do_tre"], 1);
                thoigiantre = Utility.Int32Dbnull(ConfigurationManager.AppSettings["thoigian_cho"], 1);
				int num = Utility.Int32Dbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsGoiLoa.Columns.Id].Value);
				text = Utility.sDbnull(grdListGoiLaiSoKham.CurrentRow.Cells[QmsGoiLoa.Columns.NoiDung].Value);
				if (!string.IsNullOrEmpty(text))
				{
					txtnoidung.Text = text;
					string fileName = "";
					string messge = "";
                    byte[] array = _ttsClient.SaveAudio(txtnoidung.Text, ref fileName, ref messge);
                    using (FileStream fileStream = File.Open(string.Format("{0}\\{1}.mp3", _path, fileName), FileMode.Create))
                    {
                        fileStream.Position = 100L;
                        fileStream.Write(array, 0, array.Length);
                    }
                    player.URL = string.Format("{0}\\{1}.mp3", _path, fileName);
					Thread.Sleep(100);
					int millisecondsTimeout = 0;
                    double rate = Utility.DoubletoDbnull(dotre, 1);
					player.settings.rate = rate;
					player.controls.play();
                    player.OpenStateChange += player_OpenStateChange;
					while (player.playState == WMPPlayState.wmppsTransitioning)
					{
						Application.DoEvents();
						Thread.Yield();
                        millisecondsTimeout = Convert.ToInt32(Math.Ceiling( player.currentMedia.duration ));
					}
					Thread.Sleep(millisecondsTimeout*1000+thoigiantre);
				}
				_strList = _strList + "," + num;
				if (grdListGoiLaiSoKham.CurrentRow != null)
				{
					grdListGoiLaiSoKham.CurrentRow.Delete();
					player.controls.stop();
				}
			}
			catch (Exception ex)
			{
				Utility.ShowMsg(ex.Message);
			}
        }
        void releaseMediaPlayer()
        {
            try
            {
                if (player != null)
                {
                    player = null;
                    player = new WindowsMediaPlayer();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        double duration = 1;
        void player_OpenStateChange(int NewState)
        {
            try
            {
                if (player != null && player.currentMedia != null)
                    duration = player.currentMedia.duration;
            }
            catch (Exception ex)
            {
                
               
            }
           
        }

 
        string s = "";
        void player_StatusChange()
        {
          
        }
        DataTable _dtGoiLoa = new DataTable();
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ma_loa = Utility.sDbnull(ConfigurationManager.AppSettings["ma_loa"], "-1");
                byte loai_QMS = Utility.ByteDbnull(ConfigurationManager.AppSettings["loai_qms"], 100);
                _dtGoiLoa = SPs.QmsGoiLoaGetAllByMaLoa(ma_loa, (byte)0,loai_QMS, _strList).GetDataSet().Tables[0];
                if (_dtGoiLoa.Rows.Count > 0)
                {
                    grdListGoiLaiSoKham.DataSource = _dtGoiLoa;
                }
                else
                {
                    grdListGoiLaiSoKham.DataSource = null;
                }
                _strList = "-1";
            }
            catch (Exception)
            {
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

        private void FrmMain_New_FormClosed(object sender, FormClosedEventArgs e)
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

        private void FrmMain_New_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized )
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000);
            }
        }

        private void FrmMain_New_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cmdStop_Click(object sender, EventArgs e)
        {
            try
            {
                cmdStop.Image = cmdStop.Tag.ToString() == "1" ? global::VMS.QMS.TTS.Properties.Resources.QMS_Play_06_48 : global::VMS.QMS.TTS.Properties.Resources.QMS_pause_05_48;
                if (cmdStop.Tag.ToString() == "0")//Chuyển sang chế độ Stop
                {
                    cmdStop.Tag = "1";
                    cmdStop.Text = "Stop";
                    timer2.Start();
                    Thread.Sleep(100);
                    timer1.Start();
                }
                else
                {
                    cmdStop.Tag = "0";
                    cmdStop.Text = "Start";
                    timer2.Stop();
                    Thread.Sleep(100);
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
    }
}