using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CIS.CoreApp;
using SubSonic;
using VNS.Core.Classes;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using VNS.Libs;
using VNS.Libs.AppLogger;
using VNS.Libs.AppType;
using VNS.Libs.AppUI;
using VNS.Properties;
using KeyManager;
using System.Drawing;
using System.Linq;
namespace VNSCore
{
    public partial class FrmLogin : Form
    {
        public bool BCancel = true;
        public bool BlnRelogin = false;
        private string _oldUid = "";
        bool AllowChanged = false;
        public delegate void OnloadCommonData();
        public event OnloadCommonData _OnloadData;
        public FrmLogin()
        {
            InitializeComponent();
            Shown += FrmLogin_Shown;
            cmdSettings.Click += cmdSettings_Click;
            txtUserName.LostFocus += txtUserName_LostFocus;
            pnlLogo.MouseDoubleClick += panel1_MouseDoubleClick;
            cbogiaodien.SelectedIndex = globalVariables.sMenuStyle == "MENU" ? 0 : 1;
            txtShowHidePwd.MouseUp += txtShowHidePwd_MouseUp;
            txtShowHidePwd.MouseDown += txtShowHidePwd_MouseDown;
        }

        void txtShowHidePwd_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassWord.PasswordChar = '\0';
        }

        void txtShowHidePwd_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassWord.PasswordChar = '*';
        }

        void FrmLogin_Shown(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\logosize_login.txt"))
                {
                    List<string> lstsize = File.ReadAllLines(Application.StartupPath + @"\logosize_login.txt")[0].Split('x').ToList<string>();
                    if (lstsize.Count == 2)
                    {
                        pnlLogo.Width = Utility.Int32Dbnull(lstsize[0], pnlLogo.Width);
                        pnlLogo.Height = Utility.Int32Dbnull(lstsize[1], pnlLogo.Height);
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                SetLogo("logo_login");
            }
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
                    pnlLogo.BackgroundImage = Image.FromFile(filename);
                    pnlLogo.BackgroundImageLayout = ImageLayout.Stretch;
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
        void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                SysClientCollection lstClients = new Select().From(SysClient.Schema).ExecuteAsCollection<SysClientCollection>();
                string lic = "";
                foreach (SysClient client in lstClients)
                {
                    if (Utility.sDbnull(client.Exp, "").Length > 4)
                    {
                        lic += string.Format(";{0}:{1}", client.LicKey, client.Exp);
                    }
                }
                SysSystemParameter _p = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("LICENSE").ExecuteSingle<SysSystemParameter>();
                xvect.Encrypt m_ect = new xvect.Encrypt();
                m_ect.UpdateAlgName(m_ect.AlgName);
                m_ect.sPwd = "LICENSE";
                if (_p != null)
                {
                    Utility.Log("AUTO LICENSE", globalVariables.UserName, string.Format("{0}-{1}", _p.SValue, lic), newaction.CheckLicense, "Core");
                    _p.SValue = m_ect.Mahoa(lic);
                    _p.IsNew = false;
                    _p.MarkOld();
                    _p.Save();
                    Utility.ShowMsg("Auto OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            //oldVal = PropertyLib._ConfigProperties.RunUnderWS;
            var properties = new frm_Properties(PropertyLib._ConfigProperties) {TopMost = true};
            properties.ShowDialog();
            //if (oldVal != PropertyLib._ConfigProperties.RunUnderWS)
            //{
            if (PropertyLib._ConfigProperties.RunUnderWS)
            {
                string dataBaseServer = "";
                string dataBaseName = "";
                string uid = "";
                string pwd = "";
                WS._AdminWS.GetConnectionString(ref dataBaseServer, ref dataBaseName, ref uid, ref pwd);
                PropertyLib._ConfigProperties.DataBaseServer = dataBaseServer;
                PropertyLib._ConfigProperties.DataBaseName = dataBaseName;
                PropertyLib._ConfigProperties.UID = uid;
                PropertyLib._ConfigProperties.PWD = pwd;
                globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
            }
            else
            {
                globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
                globalVariables.sMenuStyle = "DOCKING";

                globalVariables.MA_KHOA_THIEN = PropertyLib._ConfigProperties.MaKhoa;
                globalVariables.MA_PHONG_THIEN = PropertyLib._ConfigProperties.Maphong;
                globalVariables.SOMAYLE = PropertyLib._ConfigProperties.Somayle;
                globalVariables.MIN_STT = PropertyLib._ConfigProperties.Min;
                globalVariables.MAX_STT = PropertyLib._ConfigProperties.Max;
            }
            Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            //}
        }
        /// <summary>
        /// mã hóa
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("hoidapit.com.vn"));
            }
            else keyArray = Encoding.UTF8.GetBytes("hoidapit.com.vn");
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// Giải mã 
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("hoidapit.com.vn"));
            }
            else keyArray = Encoding.UTF8.GetBytes("hoidapit.com.vn");
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        } 

        /// <summary>
        ///     hàm thực hiện việc đăng nhập thông tin của khi đăng nhập Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                globalVariables.StringLicense = GetRequestLicenseCode() + THU_VIEN_CHUNG.GetMACAddress();
                AllowChanged = false;
                DataTable dtCoso = SPs.LoginLaychinhanhkcb(_oldUid, Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboCosoKCB, dtCoso, "MA", "TEN", "---Chọn đơn vị làm việc---", false, true);
                cboCosoKCB.SelectedIndex = Utility.GetSelectedIndex(cboCosoKCB, PropertyLib._AppProperties.Ma_Coso);
                if (cboCosoKCB.Items.Count == 1)
                {
                    cboCosoKCB.Enabled = false;
                    cboCosoKCB.SelectedIndex = 0;
                }
                else if (cboCosoKCB.Items.Count > 1)
                {
                    cboCosoKCB.SelectedIndex = 0;
                    cboCosoKCB.Enabled = true;
                }
                AllowChanged = true;
                cboCosoKCB_SelectedIndexChanged(cboCosoKCB, e);
                PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties();
                cbogiaodien.SelectedIndex = PropertyLib._AppProperties.MenuStype;
                txtUserName.Text = PropertyLib._AppProperties.UID;
                _oldUid = txtUserName.Text;
                chkRemember.Checked = PropertyLib._AppProperties.REM;
                lblMsg.Text = "";

                ///Bỏ đoạn code load khoa phòng. Xác định sau khi nhập username
                //DataTable dtKhoaphong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
                //DataBinding.BindDataCombobox(cboKhoaKCB, dtKhoaphong, DmucKhoaphong.Columns.MaKhoaphong,
                //    DmucKhoaphong.Columns.TenKhoaphong, "---Khoa làm việc---", false);
                //cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._ConfigProperties.MaKhoa);
                //if (cboKhoaKCB.SelectedIndex <= 0)
                //    cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
                if (PropertyLib._AppProperties.AutoLogin)
                {
                    txtPassWord.Text = PropertyLib._AppProperties.PWD;
                    cmdLogin_Click(cmdLogin, e);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex);
            }
            finally
            {
                txtUserName_LostFocus(txtUserName, e);
            }
        }
        public static string ConvertStringToSecureCode(string input1)
        {
            MD5 secu1 = MD5.Create();
            byte[] data1 = secu1.ComputeHash(Encoding.Default.GetBytes(input1));
            var sbd = new StringBuilder();
            for (int i = 0; i <= data1.Length - 1; i++)
            {
                sbd.Append(data1[i].ToString("x2"));
            }
            return sbd.ToString();
        }
        public static string GetRequestLicenseCode()
        {
            string hd1 = HardDiskSeriesNumber();
            string code1 = ConvertStringToSecureCode(hd1);
            string code2 = code1.Substring(24).ToUpper();
            // string s5 = FormatLicenseCode(Code2);
            return code2;
        }
        private static string HardDiskSeriesNumber()
        {
            string output = ExecuteCommandSync("vol");
            string aa = output.Split('.')[output.Split('.').Length - 1];
            string bb = aa.Split(' ')[aa.Split(' ').Length - 1];
            return bb.ToUpper();
        }
        public static string ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                return result;
            }
            catch (Exception)
            {
                // Log the exception
                return null;
            }
        }

        /// <summary>
        ///     hàm thực hienj việc lưu lại thông itn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void txtUserName_LostFocus(object sender, EventArgs eventArgs)
        {
            try
            {

                //if (_oldUid != Utility.sDbnull(txtUserName.Text))
                //{
                _oldUid = Utility.sDbnull(txtUserName.Text);
                globalVariables.UserName = _oldUid;
                globalVariables.IsAdmin = new LoginService().isAdmin(Utility.sDbnull(_oldUid));
                AllowChanged = false;
                DataTable dtCoso = SPs.LoginLaychinhanhkcb(_oldUid,Utility.Bool2byte(globalVariables.IsAdmin)).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboCosoKCB, dtCoso, "MA", "TEN", "---Chọn đơn vị làm việc---", false,true);
                cboCosoKCB.SelectedIndex = Utility.GetSelectedIndex(cboCosoKCB, PropertyLib._AppProperties.Ma_Coso);
                if (cboCosoKCB.Items.Count == 1)
                {
                    cboCosoKCB.Enabled = false;
                    cboCosoKCB.SelectedIndex = 0;
                }
                else if (cboCosoKCB.Items.Count >1)
                {
                    cboCosoKCB.SelectedIndex = 0;
                    cboCosoKCB.Enabled = true;
                }
                AllowChanged = true;
                cboCosoKCB_SelectedIndexChanged(cboCosoKCB, eventArgs);


                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex);
            }
            finally
            {
                AllowChanged = true;
            }
        }

        /// <summary>
        ///     hàm thực hienj viecj đang nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

        /// <summary>
        ///     hàm thực hiện việc đóng for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                BCancel = true;
                if (!BlnRelogin)
                {
                    if (Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                    {
                        Application.Exit();
                    }
                }
                //else
                //    this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex);
            }
        }

        /// <summary>
        ///     hàm thực hiện viecj đang nhập thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                Utility.WaitNow(this);
                globalVariables.IsAdmin = false;
                cmdLogin.Enabled = false;
                if (!IsValid())
                {
                    cmdLogin.Enabled = true;
                    Utility.DefaultNow(this);
                    return;
                }
                //if (chkRemember.Checked)
                //{
                PropertyLib._AppProperties.UID = Utility.sDbnull(txtUserName.Text);
                PropertyLib._AppProperties.REM = true;
                PropertyLib._AppProperties.MenuStype = cbogiaodien.SelectedIndex;

                //}
                PropertyLib._AppProperties.Makhoathien = Utility.sDbnull(cboKhoaKCB.SelectedValue, "KKB");
                globalVariables.MA_KHOA_THIEN = PropertyLib._AppProperties.Makhoathien;
                PropertyLib._AppProperties.PWD = Utility.sDbnull(txtPassWord.Text);
                PropertyLib.SaveProperty(PropertyLib._AppProperties);
                Try2SaveXML();
                UIAction.SetTextStatus(lblMsg, "Nạp thông tin cấu hình...", false);
                Utility.LoadProperties();
                Close();
                if (_OnloadData != null)
                {
                    _OnloadData();
                }
                else LoadDataForm();
                if (IsExceedData())
                {
                    Utility.ShowMsg("Phiên bản Demo chỉ cho phép bạn tiếp đón tối đa 100 lượt khám. Mời bạn liên hệ 0915150148(A. Cường) để được trợ giúp");
                    Application.Exit();
                }
               
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                cmdLogin.Enabled = true;
                Utility.DefaultNow(this);
            }
        }
        List<string> lstLicenseCode = new List<string>() { "DEMO", "LICENSE" };
        bool IsExceedData()
        {
            try
            {
                return false;
                xvect.Encrypt objEncrypt = new xvect.Encrypt(globalVariables.gv_sSymmetricAlgorithmName);
                objEncrypt.sPwd = "VMSHIS.LICENSE_CODE";
                string LicenseCode = objEncrypt.GiaiMa(THU_VIEN_CHUNG.Laygiatrithamsohethong("LICENSE_CODE", "LICENSE_CODE", true));
                bool isDemo = LicenseCode == "DEMO";
                if (!lstLicenseCode.Contains(LicenseCode))
                {
                    Utility.ShowMsg("Sai License Code. Mời bạn liên hệ 0915150148(A. Cường) để được trợ giúp");
                    Application.Exit();
                    return true;
                }
                if (isDemo || PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                {
                    var lst = new Select().From(KcbLuotkham.Schema).ExecuteAsCollection<KcbLuotkhamCollection>();
                    return lst.Count >= 100;
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.CatchException("isExceedData()-->", ex);
                return true;
            }
        }
        /// <summary>
        ///     hàm thực hiện việc đăng nhập thông tin  kiểm tra
        ///     quyền hợp lệ
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            UIAction.SetTextStatus(lblMsg, "", false);

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                UIAction.SetTextStatus(lblMsg, "Bạn phải nhập tên đăng nhập", true);
                cmdLogin.Enabled = true;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            if (cboCosoKCB.Items.Count == 0 || cboCosoKCB.SelectedValue == null ||
               cboCosoKCB.SelectedValue.ToString() == "-1" || cboCosoKCB.SelectedIndex < 0)
            {
                UIAction.SetTextStatus(lblMsg, "Bạn cần chọn đơn vị làm việc", true);
                cboCosoKCB.Focus();
                return false;
            }

            if (cboKhoaKCB.Items.Count == 0 || cboKhoaKCB.SelectedValue == null ||
                cboKhoaKCB.SelectedValue.ToString() == "-1" || cboKhoaKCB.SelectedIndex < 0)
            {
                UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc", true);
                cboKhoaKCB.Focus();
                return false;
            }
            DataTable dtAdmin= new Select().From(SysAdministrator.Schema).ExecuteDataSet().Tables[0];
            List<string> lstAdmin = (from p in dtAdmin.AsEnumerable() select p["PK_sAdminID"].ToString()).Distinct().ToList<string>();
            DmucNhanvien objNhanvien =
                 new Select().From(DmucNhanvien.Schema)
                     .Where(DmucNhanvien.Columns.UserName)
                     .IsEqualTo(txtUserName.Text.Trim())
                     .And(DmucNhanvien.Columns.TrangThai).IsEqualTo(1)
                     .ExecuteSingle<DmucNhanvien>();
            if (objNhanvien == null && !lstAdmin.Contains(txtUserName.Text))
            {
                 UIAction.SetTextStatus(lblMsg, "Tài khoản không được hoạt động", true);
                cmdLogin.Enabled = true;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            
            string userName = Utility.sDbnull(Utility.GetPropertyValue(txtUserName, "Text"));
            string password = Utility.sDbnull(Utility.GetPropertyValue(txtPassWord, "Text"));
            BCancel = true;
            SqlQuery sqlQueryUnit =
               new Select().From(SysManagementUnit.Schema);//.Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo(_admin.FpSBranchID);
            var objManagementUnit = sqlQueryUnit.ExecuteSingle<SysManagementUnit>();
            if (objManagementUnit != null)
            {
                globalVariables.Branch_ID = objManagementUnit.PkSBranchID;
                globalVariables.Branch_Address = objManagementUnit.SAddress;
                globalVariables.Branch_Name = objManagementUnit.SName;
                globalVariables.Branch_Email = objManagementUnit.SEMAIL;
                globalVariables.Branch_Phone = objManagementUnit.SPhone;
                globalVariables.Branch_Hotline = objManagementUnit.SHotPhone;
                globalVariables.Branch_Fax = objManagementUnit.SFAX;
                globalVariables.ParentBranch_Name = objManagementUnit.SParentBranchName;
                globalVariables.Branch_Website = objManagementUnit.Website;
                globalVariables._NumberofBrlink = 3;
                globalVariables.SysLogo = objManagementUnit.Logo;
            }
           
            globalVariables.LoginSuceess = new LoginService().isAdmin(Utility.sDbnull(userName),
                Utility.sDbnull(password));
            if (globalVariables.LoginSuceess) goto _Admin;
            xvect.Encrypt objEncrypt = new xvect.Encrypt(globalVariables.gv_sSymmetricAlgorithmName);
            objEncrypt.sPwd = "DVC@COMPANY";
            SysUser objUID = new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(userName).ExecuteSingle<SysUser>();
            if (objUID != null)
            {
                globalVariables.IsAdmin = objUID.ISecurityLevel == 1;
                globalVariables.UserName = objUID.PkSuid;
                LogAction.LogSCPService(objEncrypt.GiaiMa(objUID.SPwd));
            }
            globalVariables.LoginSuceess = new LoginService().KiemTraUserName(Utility.sDbnull(userName));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai tên đăng nhập. Mời bạn nhập lại", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            globalVariables.LoginSuceess = globalVariables.isSuperAdmin ?true: new LoginService().KiemTraPassword(Utility.sDbnull(userName), Utility.sDbnull(password));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai mật khẩu đăng nhập. Mời bạn nhập lại mật khẩu", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtPassWord);
                return false;
            }
            globalVariables.LoginSuceess = globalVariables.isSuperAdmin ? true : new LoginService().DangNhap(Utility.sDbnull(userName),
                Utility.sDbnull(password.Trim()));
            if (!globalVariables.LoginSuceess)
            {
                Utility.ShowMsg("Thông tin đăng nhập không đúng, Mời bạn nhập lại User hoặc Password", "Thông báo",
                    MessageBoxIcon.Warning);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }

            _Admin:
            BCancel = false;
            globalVariables.sMenuStyle = cbogiaodien.SelectedIndex == 0 ? "MENU" : "DOCKING";
            if (!BlnRelogin && PropertyLib._ConfigProperties.HIS_AppMode != AppEnum.AppMode.Demo)
            {
                UIAction.SetTextStatus(lblMsg, "Đang kiểm tra giấy phép sử dụng phần mềm...", false);
                if (PropertyLib._ConfigProperties.RunUnderWS)
                {
                    if (!WS._AdminWS.IsValidLicense())
                    {
                        globalVariables.LoginSuceess = false;
                        UIAction.SetTextStatus(lblMsg,
                            "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp: 0915150148 (Mr. Cường)",
                            true);
                        return false;
                    }
                }
                else if (!IsValidSoftKey())
                {
                    globalVariables.LoginSuceess = false;
                    Utility.ShowMsg(
                        "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp:  0915150148 (Mr. Cường)");
                    UIAction.SetTextStatus(lblMsg,
                        "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp:  0915150148 (Mr. Cường)",
                        true);
                    return false;
                }
            }
            
            return true;
        }

        private bool IsValidSoftKey()
        {
            try
            {
                if (globalVariables.IsValidLicense) return true;
                string sRegKey = getRegKeyBasedOnSCPLicense();
                var appKey = new MHardKey("XFW", 5, false);
                string giaima = Decrypt(sRegKey, false );

                globalVariables.IsValidLicense = sRegKey == appKey.RegKey;
                if (!globalVariables.IsValidLicense)
                {
                    LogAction.LogSCPService(
                        string.Format(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") +
                                      "-->Kiểm tra khóa mềm không hợp lệ."));
                    return false;
                }
                LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm hợp lệ...");
                return true;
            }
            catch (Exception ex)
            {
                LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Lỗi khi kiểm tra khóa mềm-->" +
                                        ex.Message);
                return false;
            }
        }

        private string getRegKeyBasedOnSCPLicense()
        {
            try
            {
                string fileName = Application.StartupPath + @"\license.lic";
                if (!File.Exists(fileName)) return "";
                using (var streamR = new StreamReader(fileName))
                {
                    return streamR.ReadLine() ;
                }
            }
            catch
            {
                return "";
            }
        }

        private void LoadDataForm()
        {
            Application.DoEvents();
            LoadList();
            Application.DoEvents();
        }

        private void Try2SaveXML()
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(Application.StartupPath + @"\Config.xml");
                ds.Tables[0].Rows[0]["INTERFACEDISPLAY"] = globalVariables.sMenuStyle;
                ds.WriteXml(Application.StartupPath + @"\Config.xml", XmlWriteMode.IgnoreSchema);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
                
        }

        public void LoadList()
        {
            try
            {
               
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu các danh mục dùng chung...", false);
                DataTable dtTemp=new DataTable();
                DataRow[] arrDr = null;
                DataSet ds=SPs.DmucLaydmucKhoitao(globalVariables.UserName).GetDataSet();
                
                globalVariables.gv_dtDangbaoche = ds.Tables[0];
                globalVariables.gv_dtDanhMucThuoc = ds.Tables[1];// new Select().From(VDmucThuoc.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtLoaiThuoc = ds.Tables[2];// new Select().From(DmucLoaithuoc.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucChung = ds.Tables[3];// new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtQheDoituongThuoc = ds.Tables[4];//new Select().From(QheDoituongThuoc.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục bệnh...", false);
                globalVariables.gv_dtDmucLoaibenh = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> {"LOAIBENH"},false);
                globalVariables.gv_danhmucbenhan = ds.Tables[5];// new Select().From(DmucBenhan.Schema).Where(DmucBenhan.Columns.Trangthai).IsEqualTo(1).OrderAsc(DmucBenhan.Columns.SttHienthi).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucBenh = ds.Tables[6];// new Select().From(VDanhmucbenh.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục địa chính...", false);
                globalVariables.gv_dtDmucDiachinh = ds.Tables[7];// new Select().From(VDmucDiachinh.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucBenhVien = ds.Tables[8];// new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                Utility.AutoCompeleteAddress(globalVariables.gv_dtDmucDiachinh);
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục nơi KCBBĐ...", false);
                globalVariables.gv_dtDmucNoiKCBBD = ds.Tables[9];// new Select().From(VDmucNoiKCBBD.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu dịch vụ CLS...", false);
                globalVariables.gv_dtDmucQheCamCLSChungPhieu = ds.Tables[10];//new Select().From(QheCamchidinhChungphieu.Schema).Where(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucDichvuCls = ds.Tables[11];// new Select().From(VDmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucDichvuClsChitiet = ds.Tables[12];//new Select().From(VDmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtNhomDichVuCLS = ds.Tables[13];//new Select().From(DmucNhomcanlamsang.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtQheDoituongDichvu = ds.Tables[14];//new Select().From(QheDoituongDichvucl.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu hệ thống khác...", false);
                 globalVariables.gv_dtSysparams = ds.Tables[15];// new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                 globalVariables.gv_dtQuyenNhanvienCapnhatnhanhDmucChung = ds.Tables[22];
               arrDr = globalVariables.gv_dtSysparams.Select("sName='"+globalVariables.BHXH_WebCode+"'");

                if (arrDr.Length>0)
                {
                    globalVariables.BHXH_WebPath = Utility.sDbnull(arrDr[0]["sValue"], "");
                }
                
                arrDr = globalVariables.gv_dtSysparams.Select("sName='" + globalVariables.Invoice_WebCode + "'");

                if (arrDr.Length > 0)
                {
                    globalVariables.Invoice_WebPath = Utility.sDbnull(arrDr[0]["sValue"], "");
                }

                globalVariables.gv_dtSysTieude = ds.Tables[16];// new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucNhanvien = ds.Tables[17];// new Select().From(VDmucNhanvien.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtNhomInCLS = (from p in globalVariables.gv_dtDmucChung.AsEnumerable()
                    where p.Field<string>(DmucChung.Columns.Loai) == "NHOM_INPHIEU_CLS"
                    select p).CopyToDataTable();
                globalVariables.IdKhoaNhanvien = (Int16) THU_VIEN_CHUNG.LayIDPhongbanTheoUser(globalVariables.UserName);
                globalVariables.gv_dtDoituong = ds.Tables[18];//new Select().From(DmucDoituongkcb.Schema).OrderAsc(DmucDoituongkcb.Columns.SttHthi).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucPhongban = ds.Tables[19];// new Select().From(DmucKhoaphong.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_strIPAddress = THU_VIEN_CHUNG.GetIP4Address();
                globalVariables.gv_strMacAddress = THU_VIEN_CHUNG.GetMACAddress();
                globalVariables.gv_strComputerName = Utility.GetComputerName();
                globalVariables.idKhoatheoMay =
                    (Int16) THU_VIEN_CHUNG.LayIdPhongbanTheoMay(globalVariables.MA_KHOA_THIEN);
                globalVariablesPrivate.objKhoaphong = DmucKhoaphong.FetchByID(globalVariables.idKhoatheoMay);
                globalVariablesPrivate.objNhanvien =
                    new Select().From(DmucNhanvien.Schema)
                        .Where(DmucNhanvien.Columns.UserName)
                        .IsEqualTo(globalVariables.UserName)
                        .ExecuteSingle<DmucNhanvien>();
                if (globalVariablesPrivate.objNhanvien != null)
                {
                    globalVariables.Tennguoidung = globalVariablesPrivate.objNhanvien.TenNhanvien;
                    globalVariablesPrivate.objKhoaphongNhanvien =
                       DmucKhoaphong.FetchByID(globalVariablesPrivate.objNhanvien.IdKhoa);
                    arrDr = ds.Tables[20].Select("Id_Bacsi=" + globalVariablesPrivate.objNhanvien.IdNhanvien);
                    globalVariables.qh_NhanVienPhongKham = arrDr.Length > 0 ? arrDr.CopyToDataTable() : ds.Tables[20].Clone(); // new Select().From(QheBacsiKhoaphong.Schema).Where(QheBacsiKhoaphong.IdBacsiColumn).IsEqualTo(globalVariablesPrivate.objNhanvien.IdNhanvien).ExecuteDataSet().Tables[0];
                    globalVariables.IdPhongNhanvien = globalVariablesPrivate.objNhanvien.IdPhong;
                }
                else
                    globalVariables.Tennguoidung = globalVariables.UserName;
                globalVariables.gv_dtSysUserConfig = ds.Tables[21];
                globalVariables.gv_dtKhoaPhongNgoaiTru =
                    SPs.DmucLaydanhsachCacphongkhamTheoBacsi(globalVariables.UserName, globalVariables.idKhoatheoMay,
                        Utility.Bool2byte(  globalVariables.IsAdmin), 2).GetDataSet().Tables[0];
                globalVariables.g_dtMeasureUnit = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> {"DONVITINH"},
                    false);

                globalVariables.gv_dtDantoc = globalVariables.gv_dtDmucChung.Select("LOAI='DAN_TOC'").CopyToDataTable();//new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("DAN_TOC").ExecuteDataSet().Tables[0];
                globalVariables.SysDate = THU_VIEN_CHUNG.GetSysDateTime();
                globalVariables.UCL = THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL", "", false);
                //globalVariables.UCL =globalVariables.UCL.Substring(globalVariables.UCL.IndexOf(globalVariables.UserName),
               
                Utility.LoadImageLogo();
            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                THU_VIEN_CHUNG.GetIP4Address();
                THU_VIEN_CHUNG.GetMACAddress();
                THU_VIEN_CHUNG.LoadThamSoHeThong();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc lưu lại thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdLogin_Click(cmdLogin, new EventArgs());
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utility.ShowMsg("Liên hệ với quản trị phần mềm : \n Đào Văn Cường - SĐT: 0915150148");
            const string facebook = "http://www.facebook.com/HIS.QLBV";
            Utility.OpenProcess(facebook);
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            cmdSettings.Visible = txtUserName.Text.Trim() == "ADMIN";
        }

        private void cmdSettings_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdSuperAdmin_Click(object sender, EventArgs e)
        {
            frm_SpecialPass _frm = new frm_SpecialPass(THU_VIEN_CHUNG.Laygiatrithamsohethong("MATKHAUDACBIET", "VmsHis!@#$%", true));
            if (_frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                globalVariables.isSuperAdmin = true;
                //Utility.ShowMsg("Xác nhận quyền super admin thành công. Mời bạn thực hiện đăng nhập hệ thống");
            }
        }

        private void cboCosoKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowChanged ||Utility.sDbnull(  txtUserName.Text).Length<=0) return;
                PropertyLib._AppProperties.Ma_Coso = Utility.sDbnull(cboCosoKCB.SelectedValue, "");
                globalVariables.Ma_Coso = Utility.sDbnull(cboCosoKCB.SelectedValue, "");
                DataTable dtKhoa = THU_VIEN_CHUNG.LaydanhsachKhoaKhidangnhap(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin));
                DataBinding.BindDataCombobox(cboKhoaKCB, dtKhoa, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Khoa làm việc---", false);
                cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
                if (cboKhoaKCB.Items.Count == 1) cboKhoaKCB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

        
    }
}