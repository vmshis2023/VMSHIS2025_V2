using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Threading;
using System.Xml.Serialization;
using VNS.Properties;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace UpdateVersion
{
    public partial class UpdateVersion : Form
    {
        string sDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return DoTrim(obj.ToString());
            }
            // Int32Dbnull()
        }
       string sDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DefaultVal.ToString();
            }
            else
            {
                return DoTrim(obj.ToString());
            }
        }
       string DoTrim(string value)
        {
            return value.TrimStart().TrimEnd();
        }
       string AppName = Application.StartupPath + @"\Core.exe";
       Logger log = null;
        public UpdateVersion()
        {
           
            InitializeComponent();
            InitLogs();
            log = LogManager.GetLogger("UpdateLogs");
            //Tự động tắt phần mềm HIS
            this.Shown += new EventHandler(UpdateVersion_Shown);
            this.KeyDown += new KeyEventHandler(UpdateVersion_KeyDown);
            log.Info("UpdateVersion Constructor begin");
            Try2ReadApp();
            KillProcess(AppName);
            log.Info("UpdateVersion Constructor ReadConfig");
            ReadConfig();
            log.Info("UpdateVersion Constructor ReadConfig end");
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
                fileTarget.ArchiveAboveSize = 5242880;
                fileTarget.Encoding = Encoding.UTF8;
                config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void UpdateVersion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                OpenProcess(AppName);
                Try2ExitApp();
            }
        }

        void UpdateVersion_Shown(object sender, EventArgs e)
        {
           
            Thread.Sleep(1);
            Application.DoEvents();
            log.Info("StartUpdateVersion...");
            StartUpdateVersion();

        }
        private void SetLogo(string imgname)
        {
            try
            {

                string filename = "";
                string s = string.Format(@"{0}\Images\{1}.jpg", Application.StartupPath, imgname);
                if (File.Exists(s)) filename = s;
                else
                {
                    s = s = string.Format(@"{0}\Images\{1}.png", Application.StartupPath, imgname);
                    if (File.Exists(s))
                        filename = s;
                    else
                    {
                        s = s = string.Format(@"{0}\Images\{1}.bmp", Application.StartupPath, imgname);
                        if (File.Exists(s)) filename = s;
                    }
                }
                //if (!string.IsNullOrEmpty(filename)) this.BackgroundImage = Utility.LoadBitmap(filename);

                if (!string.IsNullOrEmpty(filename))
                {
                    pictureBox1.BackgroundImage = Image.FromFile(filename);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                Application.DoEvents();
            }
        }
        void Try2ReadApp()
        {
            try
            {
                string StartupFile = Application.StartupPath + @"\Startup.txt";
                if (File.Exists(StartupFile))
                    AppName = Application.StartupPath + @"\" + GetFirstValueFromFile(StartupFile);
            }
            catch
            {
                AppName = Application.StartupPath + @"\Core.exe";
            }
        }
        string GetFirstValueFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return "";
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    object obj = _Reader.ReadLine();
                    if (obj == null) return "";
                    return obj.ToString().Trim();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        void Try2ExitApp()
        {
            try
            {
                //return;
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
        private void UpdateVersion_Load(object sender, EventArgs e)
        {
            log.Info("SetLogo");
            SetLogo("default_update");
            Application.DoEvents();
        }
        DataTable getVersion()
        {
            try
            {
                DataTable dt = new DataTable("SysVersions");
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = conn;
                    sqlcmd.CommandText = "sys_getVersions";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlcmd);
                    sqlDa.Fill(dt);
                    conn.Close();
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }
        DataTable getVersion(string lstID)
        {
            try
            {
                DataTable dt = new DataTable("SysVersions");
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = conn;
                    sqlcmd.CommandText = "Sys_Getversions_Data";
                    sqlcmd.Parameters.Add("@lstID", SqlDbType.Text);
                    sqlcmd.Parameters["@lstID"].Value = lstID;

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlcmd);
                    sqlDa.Fill(dt);
                    conn.Close();
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }
        bool Syserr = false;
        void GetReports()
        {
            try
            {
                Syserr = false;
                Cursor = Cursors.WaitCursor;

                //Lấy dữ liệu phiên bản
                DataTable dtData = getVersion();
                if (dtData == null || dtData.Rows.Count <= 0)
                {

                    return;
                }
                log.Trace(string.Format("Total {0} will be checked for updating", dtData.Rows.Count.ToString()));
                DataTable dtLastVersion = dtData.Clone();
                Tientrinh.Maximum = dtData.Rows.Count;
                Tientrinh.Minimum = 0;
                Tientrinh.Step = 1;
                Tientrinh.Value = 1;
                foreach (DataRow dr in dtData.Rows)
                {
                    if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                        Tientrinh.Value = Tientrinh.Maximum;
                    else
                        Tientrinh.Value += 1;
                    Application.DoEvents();
                    string fullfilePath = "";
                    if (sDbnull(dr["sFolder"], "") != "")
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                    else
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFileName"], "");
                    if (!File.Exists(fullfilePath))
                    {
                        //log.Trace(string.Format("file {0} updated", fullfilePath));
                        InsertNewRow(dr, dtData, ref dtLastVersion);
                        //Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
                    }
                    else
                    {
                        FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(fullfilePath);
                        System.IO.FileInfo fI = new FileInfo(fullfilePath);
                        long ticks = fI.LastWriteTime.Ticks;
                        string sVersion = sDbnull(_FileVersionInfo.ProductVersion, "");
                        if (Int64Dbnull(dr["tick"], -1) > ticks || sVersion != sDbnull(dr["sVersion"], sVersion))
                        {

                            InsertNewRow(dr, dtData, ref dtLastVersion);
                        }
                        //else
                        //    log.Trace(string.Format("file {0} --------------------------------------------------------------------ignored", fullfilePath));

                    }
                }
                dtLastVersion.AcceptChanges();
                Application.DoEvents();

                if (dtLastVersion.Rows.Count > 0)
                {
                    Tientrinh.Maximum = dtLastVersion.Rows.Count;
                    Tientrinh.Minimum = 0;
                    Tientrinh.Step = 1;
                    Tientrinh.Value = 1;
                    foreach (DataRow dr in dtLastVersion.Rows)
                    {
                        string fullfilePath = "";
                        string fullfilePath_rar = "";
                        string trueFile = "";
                        if (sDbnull(dr["sFolder"], "") != "" && sDbnull(dr["sFolder"], "") != "YES")
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sRarFileName"], "");
                        }
                        else
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sRarFileName"], "");
                        }
                        trueFile = Int32Dbnull(dr["intRar"], 0) == 0 ? fullfilePath : fullfilePath_rar;
                        if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                            Tientrinh.Value = Tientrinh.Maximum;
                        else
                            Tientrinh.Value += 1;
                        Application.DoEvents();
                        lblStatus.Visible = true;
                        lblStatus.Text = "Đang cập nhật: " + fullfilePath + "...";
                        if (Path.GetFileName(trueFile).ToUpper().Equals("RIS.EXE") || Path.GetFileName(trueFile).ToUpper().Equals("RIS.RAR"))
                            continue;

                        Application.DoEvents();
                        if (!Directory.Exists(Path.GetDirectoryName(trueFile)))
                            Directory.CreateDirectory(Path.GetDirectoryName(trueFile));
                        SaveOldDLL(fullfilePath);
                        byte[] objData = dr["objData"] as byte[];
                        MemoryStream ms = new MemoryStream(objData);
                        log.Trace(string.Format("file copied {0}", trueFile));
                        FileStream fs = new FileStream(trueFile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(fs);
                        ms.Flush();
                        fs.Close();
                        try
                        {
                            if (Int32Dbnull(dr["intRar"], 0) == 1)
                            {
                                continue;
                                if (!File.Exists(Application.StartupPath + "\\WinRAR\\WinRAR.exe"))
                                {
                                    MessageBox.Show("Bạn cần copy chương trình giải nén file vào tại đường dẫn sau " + Application.StartupPath + "\\WinRAR\\WinRAR.exe");
                                    Syserr = true;
                                    return;
                                }


                                string filetounzip = trueFile;
                                log.Trace(string.Format("file {0} unzip and updated", filetounzip));
                                if (sDbnull(dr["sFolder"], "") == "YES")
                                {
                                    //Kiểm tra nếu là filerar thì thực hiện giải nén không mật khẩu
                                    ProcessStartInfo info = new ProcessStartInfo();
                                    lblStatus.Text = "Giải nén:" + sDbnull(dr["sFileName"]).ToString() + "...";
                                    info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";

                                    info.Arguments = "x -o+ " + Strings.Chr(34) + filetounzip + Strings.Chr(34) + " " + Strings.Chr(34) + Path.GetDirectoryName(filetounzip) + Strings.Chr(34);
                                    // MessageBox.Show(info.Arguments);
                                    info.WindowStyle = ProcessWindowStyle.Hidden;
                                    Process pro = System.Diagnostics.Process.Start(info);
                                    pro.WaitForExit();


                                }
                                else
                                {
                                    ProcessStartInfo info = new ProcessStartInfo();
                                    lblStatus.Text = "Giải nén:" + sDbnull(dr["sFileName"]).ToString() + "...";
                                    info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";

                                    info.Arguments = "e -pSYSMAN -o+ " + Strings.Chr(34) + filetounzip + Strings.Chr(34) + " " + Strings.Chr(34) + Path.GetDirectoryName(filetounzip) + Strings.Chr(34);
                                    // MessageBox.Show(info.Arguments);
                                    info.WindowStyle = ProcessWindowStyle.Hidden;
                                    Process pro = System.Diagnostics.Process.Start(info);
                                    pro.WaitForExit();
                                }
                            }
                            else
                            {
                                //Kiểm tra nếu là filerar thì thực hiện giải nén không mật khẩu

                                log.Trace(string.Format("file not zar {0}", trueFile));
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi thực hiện giải nén file-->" + ex.Message);
                            Syserr = true;
                        }

                    }
                    lblStatus.Text = "Đã cập nhật xong. Xin vui lòng chờ trong giây lát...";
                    if (Debugger.IsAttached)
                    {
                        cmdUpdate.Visible = true;
                        cmdExit.Visible = true;
                    }
                    else
                    {
                        //OpenProcess(AppName);
                        //Try2ExitApp();
                    }
                }
                else
                {
                    lblStatus.Visible = true;
                    cmdUpdate.Visible = true;
                    lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                    //OpenProcess(AppName);
                    //Try2ExitApp();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Cursor = Cursors.Default;
                //OpenProcess(AppName);
                //Try2ExitApp();
            }
            finally
            {
                lblStatus.Visible = true;
                cmdUpdate.Visible = true;
                lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                if (!Syserr) OpenProcess(AppName);
                else
                {
                }
                Try2ExitApp();
            }
        }
        void StartUpdateVersion()
        {
            try
            {
                Syserr = false;
                Cursor = Cursors.WaitCursor;

                //Lấy dữ liệu phiên bản
                log.Info("getVersion...");
                DataTable dtData = getVersion();
                if (dtData == null || dtData.Rows.Count <= 0)
                {

                    return;
                }
                log.Trace(string.Format("Total {0} will be checked for updating", dtData.Rows.Count.ToString()));
                DataTable dtLastVersion = dtData.Clone();
                Tientrinh.Maximum = dtData.Rows.Count;
                Tientrinh.Minimum = 0;
                Tientrinh.Step = 1;
                Tientrinh.Value = 1;
                foreach (DataRow dr in dtData.Rows)
                {
                    if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                        Tientrinh.Value = Tientrinh.Maximum;
                    else
                        Tientrinh.Value += 1;
                   
                    string fullfilePath = "";
                    
                    if (sDbnull(dr["sFolder"], "") != "")
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                    else
                        fullfilePath = Application.StartupPath + @"\" + sDbnull(dr["sFileName"], "");
                    lblStatus.Visible = true;
                    lblStatus.Text = "Đang kiểm tra: " + fullfilePath + "...";
                    Application.DoEvents();
                    if (!File.Exists(fullfilePath))
                    {
                        log.Trace(string.Format("file {0} does not exist-->will be copied 100%", fullfilePath));
                        InsertNewRow(dr, dtData, ref dtLastVersion);
                        //Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
                    }
                    else
                    {
                        FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(fullfilePath);
                        System.IO.FileInfo fI = new FileInfo(fullfilePath);
                        long ticks = fI.LastWriteTime.Ticks;
                        string sVersion = sDbnull(_FileVersionInfo.ProductVersion, "");
                        if (Int64Dbnull(dr["tick"], -1) > ticks )
                        {
                            log.Trace(string.Format("file {0} has tick in db ={1} > LastWriteTime.Ticks ={2}", fullfilePath, Int64Dbnull(dr["tick"], -1), ticks));
                            InsertNewRow(dr, dtData, ref dtLastVersion);
                        }
                        else if (sVersion != sDbnull(dr["sVersion"], sVersion))
                        {
                            log.Trace(string.Format("file {0} has version in db ={1} <> version of file ={2}", fullfilePath, sDbnull(dr["sVersion"], "xxx"), sVersion));
                            InsertNewRow(dr, dtData, ref dtLastVersion);
                        }
                        //else
                        //    log.Trace(string.Format("file {0} --------------------------------------------------------------------ignored", fullfilePath));

                    }
                }
                dtLastVersion.AcceptChanges();
                Application.DoEvents();
                List<string> lstID = (from p in dtLastVersion.AsEnumerable()
                                      select p["PK_intID"].ToString()).ToList<string>();
                log.Info("getVersionData...");
                DataTable dtRealData = getVersion(string.Join(",", lstID.ToArray<string>()));
                log.Info("Check and download the last Version...");
                if (dtRealData.Rows.Count > 0)
                {
                    Tientrinh.Maximum = dtRealData.Rows.Count;
                    Tientrinh.Minimum = 0;
                    Tientrinh.Step = 1;
                    Tientrinh.Value = 1;
                    foreach (DataRow dr in dtRealData.Rows)
                    {
                        string fullfilePath = "";
                        string fullfilePath_rar = "";
                        string trueFile = "";
                        if (sDbnull(dr["sFolder"], "") != "" && sDbnull(dr["sFolder"], "") != "YES")
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sFolder"], "") + @"\" + sDbnull(dr["sRarFileName"], "");
                        }
                        else
                        {
                            fullfilePath = Application.StartupPath + "\\" + sDbnull(dr["sFileName"], "");
                            fullfilePath_rar = Application.StartupPath + "\\" + sDbnull(dr["sRarFileName"], "");
                        }
                        trueFile = Int32Dbnull(dr["intRar"], 0) == 0 ? fullfilePath : fullfilePath_rar;
                        if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                            Tientrinh.Value = Tientrinh.Maximum;
                        else
                            Tientrinh.Value += 1;
                        Application.DoEvents();
                        lblStatus.Visible = true;
                        lblStatus.Text = "Đang cập nhật: " + fullfilePath + "...";
                        if (Path.GetFileName(trueFile).ToUpper().Equals("UpdateVer.EXE".ToUpper()) || Path.GetFileName(trueFile).ToUpper().Equals("UpdateVer.RAR".ToUpper()))
                            continue;

                        Application.DoEvents();
                        if (!Directory.Exists(Path.GetDirectoryName(trueFile)))
                            Directory.CreateDirectory(Path.GetDirectoryName(trueFile));
                        SaveOldDLL(fullfilePath);
                        byte[] objData = dr["objData"] as byte[];
                        MemoryStream ms = new MemoryStream(objData);
                        log.Trace(string.Format("file copied {0}", trueFile));
                        FileStream fs = new FileStream(trueFile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(fs);
                        ms.Flush();
                        fs.Close();
                        try
                        {
                            //if (Int32Dbnull(dr["intRar"], 0) == 1)
                            //{
                            //    continue;
                            //    if (!File.Exists(Application.StartupPath + "\\WinRAR\\WinRAR.exe"))
                            //    {
                            //        MessageBox.Show("Bạn cần copy chương trình giải nén file vào tại đường dẫn sau " + Application.StartupPath + "\\WinRAR\\WinRAR.exe");
                            //        Syserr = true;
                            //        return;
                            //    }


                            //    string filetounzip = trueFile;
                            //    log.Trace(string.Format("file {0} unzip and updated", filetounzip));
                            //    if (sDbnull(dr["sFolder"], "") == "YES")
                            //    {
                            //        //Kiểm tra nếu là filerar thì thực hiện giải nén không mật khẩu
                            //        ProcessStartInfo info = new ProcessStartInfo();
                            //        lblStatus.Text = "Giải nén:" + sDbnull(dr["sFileName"]).ToString() + "...";
                            //        info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";

                            //        info.Arguments = "x -o+ " + Strings.Chr(34) + filetounzip + Strings.Chr(34) + " " + Strings.Chr(34) + Path.GetDirectoryName(filetounzip) + Strings.Chr(34);
                            //        // MessageBox.Show(info.Arguments);
                            //        info.WindowStyle = ProcessWindowStyle.Hidden;
                            //        Process pro = System.Diagnostics.Process.Start(info);
                            //        pro.WaitForExit();


                            //    }
                            //    else
                            //    {
                            //        ProcessStartInfo info = new ProcessStartInfo();
                            //        lblStatus.Text = "Giải nén:" + sDbnull(dr["sFileName"]).ToString() + "...";
                            //        info.FileName = Application.StartupPath + "\\WinRAR\\WinRAR.exe";

                            //        info.Arguments = "e -pSYSMAN -o+ " + Strings.Chr(34) + filetounzip + Strings.Chr(34) + " " + Strings.Chr(34) + Path.GetDirectoryName(filetounzip) + Strings.Chr(34);
                            //        // MessageBox.Show(info.Arguments);
                            //        info.WindowStyle = ProcessWindowStyle.Hidden;
                            //        Process pro = System.Diagnostics.Process.Start(info);
                            //        pro.WaitForExit();
                            //    }
                            //}
                            //else
                            //{
                            //    //Kiểm tra nếu là filerar thì thực hiện giải nén không mật khẩu

                            //    log.Trace(string.Format("file not zar {0}", trueFile));
                            //}

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi thực hiện giải nén file-->" + ex.Message);
                            Syserr = true;
                        }

                    }
                    lblStatus.Text = "Đã cập nhật xong. Xin vui lòng chờ trong giây lát...";
                    if (Debugger.IsAttached)
                    {
                        cmdUpdate.Visible = true;
                        cmdExit.Visible = true;
                    }
                    else
                    {
                        //OpenProcess(AppName);
                        //Try2ExitApp();
                    }
                }
                else
                {
                    lblStatus.Visible = true;
                    cmdUpdate.Visible = true;
                    lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                    //OpenProcess(AppName);
                    //Try2ExitApp();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Cursor = Cursors.Default;
                //OpenProcess(AppName);
                //Try2ExitApp();
            }
            finally
            {
                lblStatus.Visible = true;
                cmdUpdate.Visible = true;
                lblStatus.Text = "Các phiên bản bạn đang dùng là mới nhất";
                if (!Syserr) OpenProcess(AppName);
                else
                {
                }
                Try2ExitApp();
            }
        }
        string sqlConnectionString = "";
        public void ReadConfig()
        {
            try
            {
                HIS.WS.LoginWS _LoginWS = new HIS.WS.LoginWS();

                string _path = Application.StartupPath + @"\Properties";

                ConfigProperties _ConfigProperties = PropertyLib.GetConfigProperties(_path);
                string ServerName = _ConfigProperties.DataBaseServer;
                string sUName = _ConfigProperties.UID;
                string sPwd = _ConfigProperties.PWD;
                string sDbName = _ConfigProperties.DataBaseName;
                _LoginWS.Url = _ConfigProperties.WSURL;
                if (_ConfigProperties.RunUnderWS)
                {
                    string DataBaseServer = "";
                    string DataBaseName = "";
                    string UID = "";
                    string PWD = "";
                    _LoginWS.GetConnectionString(ref ServerName, ref sDbName, ref sUName, ref sPwd);
                }
                sqlConnectionString = "workstation id=" + ServerName + ";packet size=4096;data source=" + ServerName + ";persist security info=False;initial catalog=" + sDbName + ";uid=" + sUName + ";pwd=" + sPwd;
                log.Trace(sqlConnectionString);
            }
            catch (Exception ex)
            {
                sqlConnectionString = "";
                log.Trace(ex.ToString());

            }
        }
        bool IsNumeric(object Value)
        {
            return Microsoft.VisualBasic.Information.IsNumeric(Value);
        }
        Int32 Int32Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32(DefaultVal);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        long Int64Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt64(DefaultVal);
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        public void SaveOldDLL(string pv_sFileName)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\OldVersions"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\OldVersions");
                }
                if (!Directory.Exists(Path.GetDirectoryName( Application.StartupPath +@"\OldVersions\" + pv_sFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Application.StartupPath + @"\OldVersions\" + pv_sFileName));
                }
                if (File.Exists(Application.StartupPath + "\\" + pv_sFileName))
                {
                    File.Copy(Application.StartupPath + "\\" + pv_sFileName, Application.StartupPath + "\\OldVersions\\" + pv_sFileName, true);
                }

            }
            catch (Exception ex)
            {
            }
        }
        public void InsertNewRow(DataRow dr, DataTable pv_SourceTable, ref DataTable pv_DS)
        {
            try
            {
                DataRow DRLastestV = pv_DS.NewRow();
                foreach (DataColumn col in pv_SourceTable.Columns)
                {
                    DRLastestV[col.ColumnName] = dr[col.ColumnName];
                }
                pv_DS.Rows.Add(DRLastestV);

            }
            catch (Exception ex)
            {
            }
        }
         void OpenProcess(string appName)
        {
            try
            {
                System.Diagnostics.Process.Start(appName);
            }
            catch
            {
            }
        }

         void KillProcess(string appName)
        {
            try
            {
                appName = Path.GetFileNameWithoutExtension(appName);
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(appName);
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }

        bool ExistsProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(processName);
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            OpenProcess(AppName);
            Try2ExitApp();
        }

        private void cmdUpdate_Click_1(object sender, EventArgs e)
        {
            StartUpdateVersion();
        }
    }
}
namespace VNS.Properties
{
    public class PropertyLib
    {
        public static ConfigProperties _ConfigProperties = new ConfigProperties();
        public static ConfigProperties GetConfigProperties(string _path)
        {
            try
            {
                if (!System.IO.Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var myProperty = new ConfigProperties();
                string filePath = string.Format(@"{0}\{1}.xml", _path, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ConfigProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ConfigProperties();
            }
        }
    }
    public class ConfigProperties
    {

        public ConfigProperties()
        {
            DataBaseServer = "192.168.1.254";
            DataBaseName = "PACS";
            UID = "sa";
            PWD = "123456";
            ORM = "ORM";
            MaKhoa = "KKB";
            Maphong = "101";
            Somayle = "12345678";
            MaDvi = "HIS";
            Min = 0;
            Max = 1000;
            RunUnderWS = true;
            WSURL = "http://localhost:9003/AdminWS.asmx";
        }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
Description("Địa chỉ Webservice"),
DisplayName("Địa chỉ Webservice")]
        public string WSURL { get; set; }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
 Description("true=Kết nối qua Webservice để nhận chuỗi kết nối chung và kiểm tra giấy phép sử dụng trên máy chủ CSDL. False = Từng máy đăng ký và tự cấu hình vào CSDL"),
 DisplayName("Kết nối qua Webservice")]
        public bool RunUnderWS { get; set; }

       
        public int Min { get; set; }
        [Browsable(true), ReadOnly(false), Category("Department Settings"),
   Description("Số mã bệnh phẩm lớn nhất khi bác sĩ chỉ định CLS"),
   DisplayName("Số mã bệnh phẩm lớn nhất")]
        public int Max { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
    Description("Mã đơn vị(Bệnh viện) sử dụng phần mềm"),
    DisplayName("Mã đơn vị thực hiện")]
        public string MaDvi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã khoa đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã khoa thực hiện")]
        public string MaKhoa { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã phòng đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã phòng thực hiện")]
        public string Maphong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Số máy lẻ của khoa sử dụng"),
     DisplayName("Số máy lẻ khoa sử dụng")]
        public string Somayle { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Server"),
     DisplayName("DataBase Server")]
        public string DataBaseServer { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
       Description("DataBase Name"),
       DisplayName("DataBase Name")]
        public string DataBaseName { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase User"),
     DisplayName("DataBase User")]
        public string UID { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Password"),
     DisplayName("DataBase Password")]
        public string PWD { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
    Description("ProviderName"),
    DisplayName("ProviderName")]
        public string ORM { get; set; }

    }
}
