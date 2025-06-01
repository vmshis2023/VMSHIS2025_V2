using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.UI.Dock;
using Janus.Windows.UI.StatusBar;
using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using VNS.Libs;
using VNS.Core.Classes;
using VNS.HIS.DAL;
using System.Collections.Generic;
using VNS.Properties;

namespace CIS.CoreApp
{
       
    public partial class frm_MainForm : Form
    {
        Thread t = null;
        Thread t1 = null;
        private readonly string sPath = Application.StartupPath;
        private Logger log;
        bool fored2Close = false;
        private string sFileName = "File_Config/autoHie.txt";
        private string sFileNameScreen = Application.StartupPath + "/File_Config/defaultscreen.txt";
        /// <summary>
        /// hàm thực hiện việc khởi tạo
        /// </summary>
        public frm_MainForm()
        {
            try
            {
                if (!new ConnectionSQL().ReadConfig())
                {
                    Try2ExitApp();
                    return;
                }
                InitializeComponent();
                PanelManager.MdiTabCreated += new MdiTabEventHandler(PanelManager_MdiTabCreated);
                this.KeyDown += new KeyEventHandler(frm_MainForm_KeyDown);
                PanelManager.MdiTabClosed += new MdiTabClosedEventHandler(PanelManager_MdiTabClosed);
                cmdLoadSysparams.Click += new EventHandler(cmdLoadSysparams_Click);
                cmdClose.Click += new EventHandler(cmdClose_Click);
                cmdHelp.Click += new EventHandler(cmdHelp_Click);
                Utility.loadIconToForm(this);
               
                InitLogs();
                Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
                treeView.CollapseAll();
                if (!globalVariables.IsAdmin)
                {
                    treeView.ExpandAll();
                }
            }
            catch
            {
            }
            finally
            {
                PanelManager.ContainerControl = null;
                this.IsMdiContainer = false;
                pnlHeader.Visible = false;
                Application.DoEvents();
            }
          
        }

        void cmdHelp_Click(object sender, EventArgs e)
        {
            
        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            fored2Close = true;
            if (Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                Application.Exit();
            else 
                fored2Close = false;
        }

        void cmdLoadSysparams_Click(object sender, EventArgs e)
        {
            try
            {
                globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
            }
            catch
            {
            }
        }
       
        void frm_MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                if (pnlHeader.Height == 0) pnlHeader.Height = 38;
                else pnlHeader.Height = 0;
            if (e.KeyCode == Keys.F11)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chuyển giao diện hiển thị về dạng " + (globalVariables.sMenuStyle == "MENU"?" giao diện hòm thư Outlook hay không?":" menu truyền thống hay không?"), "Xác nhận chuyển giao diện", true))
                {
                if (globalVariables.sMenuStyle == "MENU")
                    globalVariables.sMenuStyle = "DOCKING";
                else
                    globalVariables.sMenuStyle = "MENU";
                CreatePanelNavigation();
                }
            }
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
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
                log = LogManager.GetCurrentClassLogger();
            }
            catch
            {
            }
        }
        void Try2ExitApp()
        {
            try
            {
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phép load thông tin của forrm heienjtaij
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBackgroud();
                SetsyncDateTime();
                ClearTab();
                LoadFormMain();
            }
            catch
            {
            }
        }

        private void SetsyncDateTime()
        {
            SystemTime updatedTime = new SystemTime();
            DateTime dateTime = new LoginService().GetSysDateTime();
            updatedTime.DayOfWeek = (short) dateTime.DayOfWeek;
            updatedTime.Year = (short)dateTime.Year;
            updatedTime.Month = (short)dateTime.Month;
            updatedTime.Day = (short)dateTime.Day;
            updatedTime.Hour = (short)dateTime.Hour;
            updatedTime.Minute = (short)dateTime.Minute;
            updatedTime.Second = (short)dateTime.Second;
            SetLocalTime(ref updatedTime);
        }

        public struct SystemTime
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Millisecond;
        };

      
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetLocalTime(ref SystemTime theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetLocalTime(ref SYSTEMTIME theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetSystemTime(ref SYSTEMTIME theDateTime);
        /// <summary>
        /// hàm thực hiện việc clear tab
        /// </summary>
        public void ClearTab()
        {
            try
            {
                Form[] charr = this.MdiChildren;


                foreach (Form chform in charr)
                {
                    
                    chform.Close();
                    chform.Dispose();
                    
                }
            }
            catch (Exception)
            {
                
              
            }
          
           
        }
        /// <summary>
        /// hàm thực hiện load backgroud 
        /// </summary>
        private void LoadBackgroud()
        {
            try
            {
                string filename = "";
                string s = string.Format("{0}{1}Images{1}default.jpg", sPath, Path.DirectorySeparatorChar);
                if (File.Exists(s)) filename = s;

                s = string.Format("{0}{1}Images{1}default.png", sPath, Path.PathSeparator);
                if (File.Exists(s)) filename = s;

                s = string.Format("{0}{1}Images{1}default.bmp", sPath, Path.PathSeparator);
                if (File.Exists(s)) filename = s;
                //if (!string.IsNullOrEmpty(filename)) this.BackgroundImage = Utility.LoadBitmap(filename);

                if (!string.IsNullOrEmpty(filename))
                {
                    Image img;
                    img = Image.FromFile(filename);
                    BackgroundImage = img;
                }
                
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void LoadFormMain()
        {
            try
            {
                isLoginFirstTime = false;
                if (!globalVariables.LoginSuceess) CallLogin();
                if (globalVariables.LoginSuceess)
                {
                    PanelManager.ContainerControl = this;
                    pnlHeader.Visible = true;
                    this.IsMdiContainer = true;
                    Text = globalVariables.FORMTITLE;
                    Application.DoEvents();
                    SetInfor();
                   
                    CreatePanelNavigation();
                   
                }
            }
            catch (Exception exception)
            {
            }
        }

        // private int Status = 1;
        // [DllImport("VietBaIT.HISLink.LoadEnvironments.dll")]
        private void LoadDataForm()
        {
            //Application.DoEvents();
            //var hisData = new HISData();
            //var objThread = new Thread(hisData.LoadList);
            //objThread.Start();
            //Application.DoEvents();
        }

        /// <summary>
        /// hàm thực hiện việc gọi form đăng nhập
        /// </summary>
        private void CallLogin()
        {
            Utility.WaitNow(this);
            Application.DoEvents();
            var frm = new frm_Login();
            frm.ShowDialog();
            Utility.DefaultNow(this);
        }

        

        private bool b_Hasloaded = false;
        /// <summary>
        /// hàm thực hiện việc load thông tin của phần staatus trạng thái của core chạy chương trình
        /// </summary>
        private void SetInfor()
        {
            try
            {
               lblHospital.Text = globalVariables.Branch_Name;
                lblUser.Text = globalVariables.UserName;
                lblIP.Text = THU_VIEN_CHUNG.GetIP4Address();
                DmucKhoaphong objDepartment = new Select().From(DmucKhoaphong.Schema)
                   .Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(globalVariables.MA_KHOA_THIEN).ExecuteSingle
                   <DmucKhoaphong>();
                    if (objDepartment != null)
                    {
                       lblDepartment.Text = string.Format("{0}-{1}", globalVariables.MA_KHOA_THIEN, objDepartment.TenKhoaphong);
                    }
                    else
                    {
                       lblDepartment.Text = globalVariables.MA_KHOA_THIEN;
                    }
                Text = globalVariables.FORMTITLE;
            }
            catch(Exception ex)
            {
                MessageBox.Show("SetInfor.-->Exception()" + ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc làm tạo các phân hệ thông tin 
        /// </summary>
        private void CreatePanelNavigation()
        {
            
            switch (globalVariables.sMenuStyle)
            {
                case "DOCKING":
                    LoadMenuStyleDocking();
                    break;
                case "MENU":
                    LoadMenuStyleNormal();
                    break;
                default:
                    LoadMenuStyleNormal();
                    break;
            }
        }

        private void LoadMenuStyleNormal()
        {
            try
            {
                pMain.Visible = false;
                menuStrip.Visible = true;
                menuStrip.Items.Clear();
                InitalSystemControl();
                FindMenuChild(-2);
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo menu hệ thống\n"+ex.Message);
            }
            finally
            {
                
            }
        }

        private void FindMenuChild(int Parent_ID)
        {
            SqlQuery sqlQuery =
                    new Select().From(SysRole.Schema);
            if (!globalVariables.IsAdmin)
            {
                sqlQuery.Where(SysRole.Columns.IRole).In(
                    new Select(SysRolesForUser.Columns.IRoleID).From(SysRolesForUser.Schema).Where(
                        SysRolesForUser.Columns.SUID).IsEqualTo(
                            globalVariables.UserName));
            }
            sqlQuery.OrderAsc(SysRole.Columns.IParentRole, SysRole.Columns.IOrder);
            SysRoleCollection sysRoleCollection = sqlQuery.ExecuteAsCollection<SysRoleCollection>();
            List<SysRole> lstRoles = (from p in sysRoleCollection
                                      where p.IParentRole == -2
                                      orderby p.IOrder ascending
                                      select p
                    ).ToList<SysRole>();
            //Kiểm tra nếu chỉ có 1 chức năng thì tự động kích hoạt luôn
            List<SysRole> lstActiveRole = (from p in sysRoleCollection
                                           where p.FkIFunctionID != -1 && p.FkIFunctionID != null
                                           orderby p.IOrder ascending
                                           select p
                 ).ToList<SysRole>();

            foreach (SysRole objSysRole in lstRoles)
            {
                var objMenuItem = new ToolStripMenuItem();
                objMenuItem.Text = Utility.sDbnull(objSysRole.SRoleName, "");
                objMenuItem.Name = Utility.sDbnull(objSysRole.IRole, "-1");
                objMenuItem.ToolTipText = Utility.sDbnull(objSysRole.SRoleName, "");
                menuStrip.Items.Add(objMenuItem);
                menuStrip.Font = new Font("Tahoma", 9F);

                List<SysRole> sysRoleChild = GetSysRoleChild(objSysRole, sysRoleCollection);
                int idx = 0;
                bool bLastRecord = false;
                AddChildMenu(objMenuItem, sysRoleChild, idx, bLastRecord);
            }
            if (lstActiveRole.Count > 1)
            {

            }
            else if (lstActiveRole.Count == 1)
            {
                isSingleFunction = true;
                DisplaySingleFunction(lstActiveRole[0]);
            }
        }

        private void FindMenuChildLevel(int Parent_ID, ToolStripMenuItem menuItem)
        {
            SqlQuery sqlQuery =
                new Select().From(SysRole.Schema).Where(SysRole.Columns.IParentRole).IsEqualTo(Parent_ID);
            if (!globalVariables.IsAdmin)
            {
                sqlQuery.And(SysRole.Columns.IRole).In(
                    new Select(SysRolesForUser.Columns.IRoleID).From(SysRolesForUser.Schema).Where(
                        SysRolesForUser.Columns.SUID).IsEqualTo(
                            globalVariables.UserName));
            }
            sqlQuery.OrderAsc(SysRole.Columns.IOrder);
            var sysRoleCollection = sqlQuery.ExecuteAsCollection<SysRoleCollection>();
            foreach (SysRole objChildSysRole in sysRoleCollection)
            {
                int idx = 0;
                string sDllName = "";
                string sFunctionName = "";
                string IsTabView = "0";
                string sThamSo = "";
                SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
                if (objSysFunction != null)
                {
                    sDllName = objSysFunction.SDLLname;
                    sFunctionName = objSysFunction.SFormName;
                    sThamSo = objChildSysRole.SParameterList;
                }
                var objMenuChild = new ToolStripMenuItem(objChildSysRole.SRoleName);

                objMenuChild.Text = objChildSysRole.SRoleName.Trim().Replace("&", "");
                objMenuChild.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo + "#" + objChildSysRole.IRole.ToString();
                //objMenuChild.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo;
                objMenuChild.Name = sDllName + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;
                objMenuChild.Font = new Font("Tahoma", 10, FontStyle.Regular);
                objMenuChild.ForeColor = Color.Navy;
                objMenuChild.ToolTipText = objChildSysRole.SRoleName;
                //objMenuChild.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
                //objTreeNode.SelectedImageIndex = 3;

                objMenuChild.ImageIndex = 2;
                menuItem.DropDownItems.Add(objMenuChild);

                objMenuChild.Click += objmenuStrip_Click;
            }
        }

        private void AddChildMenu(ToolStripMenuItem objMenuItem, List<SysRole> sysRoleChild, int idx,
                                  bool bLastRecord)
        {
            foreach (SysRole objChildSysRole in sysRoleChild)
            {
                idx++;
                if (idx >= sysRoleChild.Count()) bLastRecord = true;

                CreateMenuChild(objMenuItem, objChildSysRole, bLastRecord);
            }
        }

        private void CreateMenuChild(ToolStripMenuItem MenuItem, SysRole objChildSysRole, bool b_LastRecord)
        {
            int idx = 0;
            string sDllName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
            if (objSysFunction != null)
            {
                sDllName = objSysFunction.SDLLname;
                sFunctionName = objSysFunction.SFormName;
                sThamSo = objChildSysRole.SParameterList;
            }
            var objMenuChild = new ToolStripMenuItem(objChildSysRole.SRoleName);

            objMenuChild.Text = objChildSysRole.SRoleName.Trim().Replace("&", "");
            objMenuChild.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo + "#" + objChildSysRole.IRole.ToString();
            //objMenuChild.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo;
            objMenuChild.Name = sDllName + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;
            objMenuChild.Font = new Font("Tahoma", 10, FontStyle.Regular);
            objMenuChild.ForeColor = Color.Navy;
            objMenuChild.ToolTipText = objChildSysRole.SRoleName;
            //objMenuChild.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            //objTreeNode.SelectedImageIndex = 3;

            objMenuChild.ImageIndex = 2;
            MenuItem.DropDownItems.Add(objMenuChild);

            objMenuChild.Click += objmenuStrip_Click;
            FindMenuChildLevel(Utility.Int32Dbnull(objChildSysRole.IRole, -1), objMenuChild);
        }

        private void InitalSystemControl()
        {
            //menuStrip.ImageList = SystemLogin;
            SystemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                                                               {
                                                                   cmdMenuLogin,
                                                                   ToolStripSeparator4,
                                                                   cmdMenuLogout,
                                                                   ToolStripSeparator1,
                                                                   cmdChangePassword,
                                                                   ToolStripSeparator3,
                                                                   cmdMenuDonVi_LamViec,
                                                                   ToolStripSeparator2,
                                                                   cmdMenuExit
                                                               });

            SystemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                                                               {
                                                               });
            SystemToolStripMenuItem.Text = "Hệ thống";
            SystemToolStripMenuItem.ToolTipText = "Cấu hình hệ thống";
            //menuStrip.Items.Add(SystemToolStripMenuItem);
            //menuStrip.Font = new Font("Tahoma", 9F);

            //menuStrip.Location = new Point(0, 0);
            //menuStrip.Size = new Size(957, 24);

            cmdMenuLogin.ImageIndex = 0;
            ;
            cmdMenuLogin.Name = "LoginToolStripMenuItem";
            cmdMenuLogin.Size = new Size(231, 22);
            cmdMenuLogin.Text = "Đăng &nhập";
            cmdMenuLogin.Click += cmdLoginMenuItem_Click;
            // 
            // LogoutToolStripMenuItem
            // 
            cmdMenuLogout.ImageIndex = 1;
            cmdMenuLogout.Name = "LogoutToolStripMenuItem";
            cmdMenuLogout.Size = new Size(231, 22);
            cmdMenuLogout.Text = "Đăng &xuất";
            cmdMenuLogout.Click += cmdMenuLogout_Click;
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(228, 6);
            // 
            // thayĐổiMậtKhẩuToolStripMenuItem
            // 
            cmdChangePassword.ImageIndex = 2;
            cmdChangePassword.Name = "thayĐổiMậtKhẩuToolStripMenuItem";
            cmdChangePassword.Size = new Size(231, 22);
            cmdChangePassword.Text = "Thay đổi mật khẩu";
            cmdChangePassword.Click += cmdChangePasswordMenuItem_Click;

            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(228, 6);
            // 
            // cmdDonVi_LamViec
            // 
            cmdMenuDonVi_LamViec.ImageIndex = 3;
            //this.cmdMenuDonVi_LamViec.Image = ((System.Drawing.Image)(resources.GetObject("cmdDonVi_LamViec.Image")));
            cmdMenuDonVi_LamViec.Name = "cmdDonVi_LamViec";
            cmdMenuDonVi_LamViec.Size = new Size(231, 22);
            cmdMenuDonVi_LamViec.Text = "&Đơn vị làm việc";
            cmdMenuDonVi_LamViec.Click += cmdDonVi_LamViec_Click;
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(228, 6);
            // 
            // ExitToolStripMenuItem
            // 
            cmdMenuExit.ImageIndex = 4;
            //this.cmdMenuExit.Image = ((System.Drawing.Image)(resources.GetObject("ExitToolStripMenuItem.Image")));
            cmdMenuExit.Name = "ExitToolStripMenuItem";
            cmdMenuExit.ShortcutKeys = (((Keys.Control | Keys.D)));
            cmdMenuExit.Size = new Size(231, 22);
            cmdMenuExit.Text = "Thoát";
            cmdMenuExit.Click += cmdExitMenuItem_Click;
        }
        void AutoloadControlPanel()
        {
            try
            {
                ControlPanel form = new ControlPanel();
                form.Tag = -1;
                form.MdiParent = this;
                form.ShowInTaskbar = false;
                form.Show();
            }
            catch
            {
            }
        }
               
        private void LoadMenuStyleDocking()
        {
            try
            {
                pMain.Visible = true;
                menuStrip.Items.Clear();
                menuStrip.Visible = false;
                pMain.AutoHide = true;
                pMain.SuspendLayout();
                //Load bảng điều khiển
                //AutoloadControlPanel();
                PanelManager.ImageList = imageList1;
                SqlQuery sqlQuery =
                    new Select().From(SysRole.Schema).Where(SysRole.Columns.BEnabled).IsEqualTo(1);
                if (!globalVariables.IsAdmin)
                {
                    sqlQuery.And(SysRole.Columns.IRole).In(
                        new Select(SysRolesForUser.Columns.IRoleID).From(SysRolesForUser.Schema).Where(
                            SysRolesForUser.Columns.SUID).IsEqualTo(
                                globalVariables.UserName));
                }
                sqlQuery.OrderAsc(SysRole.Columns.IParentRole, SysRole.Columns.IOrder);
                SysRoleCollection sysRoleCollection = sqlQuery.ExecuteAsCollection<SysRoleCollection>();
                pMain.Panels.Clear();
                List<SysRole> lstRoles = (from p in sysRoleCollection
                                          where p.IParentRole == -2
                                          orderby p.IOrder ascending
                                          select p
                      ).ToList<SysRole>();
                List<UIPanel> lstPanel = new List<UIPanel>();
                //Kiểm tra nếu chỉ có 1 chức năng thì tự động kích hoạt luôn
                List<SysRole> lstActiveRole = (from p in sysRoleCollection
                                               where p.FkIFunctionID != -1 && p.FkIFunctionID != null
                                               orderby p.IOrder ascending
                                               select p
                     ).ToList<SysRole>();

                foreach (SysRole objSysRole in lstRoles)//Phân hệ
                {
                    TreeView objTreeView = null;
                    objTreeView = new TreeView();
                    objTreeView.FullRowSelect = true;
                    objTreeView.ImageList = imageList;
                    //objTreeView.Name = "treeview" + Utility.sDbnull(objSysRole.IRole);
                    objTreeView.Dock = DockStyle.Fill;
                    objTreeView.ImageIndex = 2;
                    objTreeView.AutoSize = true;

                    objTreeView.HideSelection = false;
                    objTreeView.MouseDown += new MouseEventHandler(objTreeView_MouseDown);
                    objTreeView.DoubleClick += objTreeView_DoubleClick;
                    objTreeView.Click += new EventHandler(objTreeView_Click);
                    objTreeView.AfterSelect += new TreeViewEventHandler(objTreeView_AfterSelect);
                    var objPanel = new UIPanel();
                    var innerContainer = new UIPanelInnerContainer();
                    innerContainer.Controls.Add(objTreeView);
                    objPanel.InnerContainer = innerContainer;
                    objPanel.Location = new Point(0, 0);
                    objPanel.Name = Utility.sDbnull(objSysRole.IRole);
                    objPanel.Size = new Size(196, 429);
                    objPanel.TabIndex = Utility.Int32Dbnull(objSysRole.IOrder);
                    objPanel.Text = Utility.sDbnull(objSysRole.SRoleName);
                    objPanel.ImageIndex = 17;
                    objPanel.LargeImageIndex = 17;
                    objPanel.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    lstPanel.Add(objPanel);

                    List<SysRole> sysRoleChild = GetSysRoleChild(objSysRole, sysRoleCollection);// GetSysRoleChild(objSysRole);
                    foreach (SysRole objChildSysRole in sysRoleChild)
                    {
                        CreateNodeTree(objTreeView, objChildSysRole, sysRoleCollection);
                    }

                }
                if (lstActiveRole.Count > 1)
                {

                    foreach (UIPanel _panel in lstPanel)
                        pMain.Panels.Add(_panel);
                    pMain.AutoHide = false;
                }
                else if (lstActiveRole.Count == 1)
                {
                    PanelManager.DefaultPanelSettings.CloseButtonVisible = false;
                    pMain.AutoHide = true;
                    isSingleFunction = true;
                    DisplaySingleFunction(lstActiveRole[0]);
                }
            }
            catch
            {
            }
            finally
            {
               
                pMain.ResumeLayout();
            }
        }
     
        bool isSingleFunction = false;
        void DisplaySingleFunction(SysRole objChildSysRole)
        {
            SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
            if (objSysFunction != null)
            {
                string _Tag = objSysFunction.SFormName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + objChildSysRole.SParameterList + "#" + objChildSysRole.IRole.ToString();
                string _Name = objSysFunction.SDLLname + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;

                string[] arrFunctionName = _Tag.ToString().Split('#');
                string sFormName = arrFunctionName[0];
                string IsTabView = arrFunctionName[1];
                string sThamSo = arrFunctionName[2];
                string[] arrDllName = _Name.Split('#');
                string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                string _ID = arrFunctionName[3];

                if (File.Exists(DllName))
                {
                    if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                    {
                        new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                            globalVariables.UserName)
                            .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                        SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                    }

                    Assembly assembly = Assembly.LoadFile(DllName);
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == sFormName.ToUpper())
                        {
                            if (ExistsObject(arrDllName[1])) continue;
                            if (string.IsNullOrEmpty(sThamSo))
                            {
                                object obj = new DynamicInitializer().NewInstance(objType);

                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    {

                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        this.ActivateMdiChild(dic[_ID].Form);
                                    }

                                }
                            }
                            else
                            {
                                object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    else
                                    {
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                    }
                                }
                            }


                            break;
                        }
                    }
                }
            }
            
        }
        void objTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        void objTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            var objTreeView = (TreeView)sender;
            if (objTreeView.SelectedNode==null || !objTreeView.SelectedNode.Equals(objTreeView.GetNodeAt(e.X, e.Y))) 
                objTreeView.SelectedNode = objTreeView.GetNodeAt(e.X, e.Y);
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            try
            {
                Utility.WaitNow(this);
                
                    //var objTreeView = (TreeView)sender;
                    TreeNode objTreeNode = objTreeView.SelectedNode;

                    string[] arrFunctionName = objTreeNode.Tag.ToString().Split('#');
                    string sFormName = arrFunctionName[0];
                    string IsTabView = arrFunctionName[1];
                    string sThamSo = arrFunctionName[2];
                    string[] arrDllName = objTreeNode.Name.Split('#');
                    string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                    string _ID = arrFunctionName[3];

                    if (File.Exists(DllName))
                    {
                        if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                        {
                            new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                                globalVariables.UserName)
                                .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                            SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                        }
                        Assembly assembly = Assembly.LoadFile(DllName);
                        Type[] type = assembly.GetTypes();
                        foreach (Type objType in type)
                        {
                            if (objType.Name.ToUpper() == sFormName.ToUpper())
                            {
                                if (ExistsObject(arrDllName[1])) continue;
                                if (string.IsNullOrEmpty(sThamSo))
                                {
                                    object obj = new DynamicInitializer().NewInstance(objType);

                                    var form = obj as Form;
                                    if (form != null)
                                    {
                                        if (!dic.ContainsKey(_ID))
                                        {
                                            form.Tag = _ID;
                                            if (IsTabView.Equals("1"))
                                                form.MdiParent = this;
                                            form.ShowInTaskbar = false;
                                            if (IsTabView.Equals("1")) form.Show();
                                            else form.ShowDialog();
                                        }
                                        {

                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                            Form frm = dic[_ID].Form;
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            //frm.MdiParent = null;
                                            //frm.Hide();
                                            //frm.ShowDialog();
                                        }

                                    }
                                }
                                else
                                {
                                    object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                    var form = obj as Form;
                                    if (form != null)
                                    {
                                        if (!dic.ContainsKey(_ID))
                                        {
                                            form.Tag = _ID;
                                            if (IsTabView.Equals("1"))
                                                form.MdiParent = this;
                                            form.ShowInTaskbar = false;
                                            if (IsTabView.Equals("1")) form.Show();
                                            else form.ShowDialog();
                                        }
                                        else
                                        {
                                            Form frm = dic[_ID].Form;
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            //frm.MdiParent = null;
                                            //frm.Hide();
                                            //frm.ShowDialog();
                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        }
                                    }
                                }


                                break;
                            }
                        }
                    }
                
            }
            catch
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        void objTreeView_Click(object sender, EventArgs e)
        {
            
        }
        private List<SysRole> GetSysRoleChild(SysRole objSysRole,SysRoleCollection lstRoles)
        {
            return (from p in lstRoles
                  where p.IParentRole==objSysRole.IRole
                  orderby p.IOrder ascending
                  select p
                  ).ToList<SysRole>();
            
        }
        private SysRoleCollection GetSysRoleChild(SysRole objSysRole)
        {
            SqlQuery sqlQuery;
            sqlQuery = new Select().From(SysRole.Schema)
                .Where(SysRole.Columns.IParentRole).IsEqualTo(objSysRole.IRole);
            if (!globalVariables.IsAdmin)
            {
                sqlQuery.And(SysRole.Columns.IRole).In(
                    new Select(SysRolesForUser.Columns.IRoleID).From(SysRolesForUser.Schema).Where(
                        SysRolesForUser.Columns.SUID).IsEqualTo(
                            globalVariables.UserName));
            }
            sqlQuery.OrderAsc(SysRole.Columns.IOrder);
            return sqlQuery.ExecuteAsCollection<SysRoleCollection>();
        }

        /// <summary>
        /// hàm thực hiện việc xây dựn thông tin của treeview
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="objChildSysRole"></param>
        private void CreateNodeTree(TreeView treeView, SysRole objChildSysRole)
        {
            string sDllName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
            if (objSysFunction != null)
            {
                sDllName = objSysFunction.SDLLname;
                sFunctionName = objSysFunction.SFormName;
                sThamSo = objChildSysRole.SParameterList;
            }
            var objTreeNode = new TreeNode(objChildSysRole.SRoleName);

            objTreeNode.Text = objChildSysRole.SRoleName.Trim().Replace("&", "");

            objTreeNode.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo + "#" + objChildSysRole.IRole.ToString();
            objTreeNode.Name = sDllName + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;
            objTreeNode.NodeFont = new Font("Tahoma", 9, FontStyle.Regular);
            objTreeNode.ForeColor = Color.Navy;
            objTreeNode.ToolTipText = objChildSysRole.SRoleName;

            //objTreeNode.SelectedImageIndex = 3;
            objTreeNode.ImageIndex = 2;
            treeView.Nodes.Add(objTreeNode);


            SysRoleCollection objSysRoleCollection = GetObjSysRoleCollection(objChildSysRole);
            foreach (SysRole objSysRole in objSysRoleCollection)
            {
                AddNodeTree(objTreeNode, objSysRole);
            }
        }

        private void CreateNodeTree(TreeView treeView, SysRole objChildSysRole,SysRoleCollection lstRoles)
        {
            string sDllName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
            if (objSysFunction != null)
            {
                sDllName = objSysFunction.SDLLname;
                sFunctionName = objSysFunction.SFormName;
                sThamSo = objChildSysRole.SParameterList;
            }
            var objTreeNode = new TreeNode(objChildSysRole.SRoleName);

            objTreeNode.Text = objChildSysRole.SRoleName.Trim().Replace("&", "");

            objTreeNode.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo + "#" + objChildSysRole.IRole.ToString();
            objTreeNode.Name = sDllName + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;
            objTreeNode.NodeFont = new Font("Tahoma", 9, FontStyle.Regular);
            objTreeNode.ForeColor = Color.Navy;
            objTreeNode.ToolTipText = objChildSysRole.SRoleName;

            objTreeNode.SelectedImageIndex = 0;
            objTreeNode.ImageIndex = 2;
            treeView.Nodes.Add(objTreeNode);


            List<SysRole> objSysRoleCollection = GetObjSysRoleCollection(objChildSysRole, lstRoles); ;// GetObjSysRoleCollection(objChildSysRole);
            foreach (SysRole objSysRole in objSysRoleCollection)
            {
                AddNodeTree(objTreeNode, objSysRole);
            }
        }

        private SysRoleCollection GetObjSysRoleCollection(SysRole objChildSysRole)
        {
            SqlQuery sqlQuery =
                new Select().From(SysRole.Schema).Where(SysRole.Columns.IParentRole).IsEqualTo(objChildSysRole.IRole);
            if (!globalVariables.IsAdmin)
            {
                sqlQuery.And(SysRole.Columns.IRole).In(
                    new Select(SysRolesForUser.Columns.IRoleID)
                        .From(SysRolesForUser.Schema).Where(SysRolesForUser.Columns.SUID).IsEqualTo(
                            globalVariables.UserName));
            }
            sqlQuery.OrderAsc(SysRole.Columns.IOrder);
            return sqlQuery.ExecuteAsCollection<SysRoleCollection>();
        }
        private List<SysRole> GetObjSysRoleCollection(SysRole objChildSysRole, SysRoleCollection lstRoles)
        {
            return (from p in lstRoles
                    where p.IParentRole == objChildSysRole.IRole
                    orderby p.IOrder ascending
                    select p
                 ).ToList<SysRole>();
        }

        private void AddNodeTree(TreeNode objTreeNode, SysRole objChildSysRole)
        {
            string sDllName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            SysFunction objSysFunction = SysFunction.FetchByID(objChildSysRole.FkIFunctionID);
            if (objSysFunction != null)
            {
                sDllName = objSysFunction.SDLLname;
                sFunctionName = objSysFunction.SFormName;
                sThamSo = objChildSysRole.SParameterList;
            }
            var objChildTreeNode = new TreeNode(objChildSysRole.SRoleName);

            objChildTreeNode.Text = objChildSysRole.SRoleName.Trim().Replace("&", "");

            objChildTreeNode.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo + "#" + objChildSysRole.IRole.ToString();
            objChildTreeNode.Name = sDllName + "#" + objChildSysRole.SRoleName + "#" + objChildSysRole.IRole;
            objChildTreeNode.NodeFont = new Font("Tahoma", 9, FontStyle.Regular);
            objChildTreeNode.ForeColor = Color.Navy;
            objChildTreeNode.ToolTipText = objChildSysRole.SRoleName;
            objTreeNode.SelectedImageIndex = 0;
            objChildTreeNode.ImageIndex = 2;
            objTreeNode.Nodes.Add(objChildTreeNode);
        }

        private void CallForm(string pv_sDLLName, string pv_sAssemblyName, string gv_sAnnouncement)
        {
            Assembly sv_oAss;
            bool bExistForm = false;
            Type sv_oAllForm; //Contains All Forms from Assembly
            if (pv_sAssemblyName == "-1") return;
            string sTenDll = Application.StartupPath + string.Format("\\{0}.dll", pv_sAssemblyName);
            if (File.Exists(sTenDll))
            {
                MessageBox.Show("Không tồn tại phân hệ " + pv_sDLLName + ".DLL", gv_sAnnouncement, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                return;
            }

            Assembly LibAss = Assembly.LoadFrom(Application.StartupPath + "\\CommonLibrary.DLL");
            Utility.LoadParamsValues(LibAss);
            //Load Assembly Information from AssemblyName
            sv_oAss = Assembly.LoadFrom(Application.StartupPath + string.Format("\\{0}.DLL", pv_sDLLName));
            Assembly assembly = Assembly.LoadFrom(sTenDll);
            Type type = assembly.GetType(Application.StartupPath + string.Format("\\{0}.DLL", pv_sDLLName));
            var form = (Form) Activator.CreateInstance(type);
            form.ShowDialog();
        }
        bool isLoginFirstTime = true;
        /// <summary>
        /// hàm thực hiện việc đăng nhập lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            
            var frm = new frm_Login();
            frm.blnRelogin = !isLoginFirstTime;
            frm.ShowDialog();
            if (!frm.b_Cancel)
            {
                if (globalVariables.LoginSuceess)
                {
                    ClearTab();
                    LoadFormMain();
                }
            }
            else//Nhấn Cancel thì quay lại giao diện cũ không thực hiện gì cả
              if(!isLoginFirstTime)  globalVariables.LoginSuceess = true;

        }

        /// <summary>
        /// hàm thực hiện viecj đăng nhập lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMenuLogout_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn muốn thoát chương trình đang chạy không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }
       
        /// <summary>
        /// hàm thực hiện việc gọi sự kiện của phần treeview double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void objTreeView_DoubleClick(object sender, EventArgs eventArgs)
        {
          
        }

        private void objmenuStrip_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                Utility.WaitNow(this);

                var objmenuStrip = (ToolStripMenuItem)sender;
                string[] arrFunctionName = objmenuStrip.Tag.ToString().Split('#');
                string sFormName = arrFunctionName[0];
                string IsTabView = arrFunctionName[1];
                string sThamSo = arrFunctionName[2];
                string[] arrDllName = objmenuStrip.Name.Split('#');
                string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                string _ID = arrFunctionName[3];
                if (File.Exists(DllName))
                {
                    if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                    {
                        new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                            globalVariables.UserName)
                            .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                        SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                    }

                    Assembly assembly = Assembly.LoadFile(DllName);
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == sFormName.ToUpper())
                        {
                            if (ExistsObject(arrDllName[1])) continue;
                            if (string.IsNullOrEmpty(sThamSo))
                            {
                                object obj = new DynamicInitializer().NewInstance(objType);

                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    {

                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        Form frm = dic[_ID].Form;
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        //frm.MdiParent = null;
                                        //frm.Visible = false;
                                        //frm.ShowDialog();
                                    }

                                }
                            }
                            else
                            {
                                object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    else
                                    {
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        Form frm = dic[_ID].Form;
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        //frm.MdiParent = null;
                                        //frm.Visible = false;
                                        //frm.ShowDialog();
                                    }
                                }
                            }


                            break;
                        }
                    }
                }

            }
            catch
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }

           
        }

        //  private void objTreeView_DoubleClick
        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của đối tượng đã tồn tại
        /// </summary>
        /// <param name="sFormName"></param>
        /// <returns></returns>
        public bool ExistsObject(string sFormName)
        {
            bool bFind = false;
            //this.Cursor = Cursors.WaitCursor;

            for (int x = 0; x <= (MdiChildren.Length) - 1; x++)
            {
                Form tempChild = MdiChildren[x];

                if (tempChild.Name == sFormName)
                {
                    tempChild.Activate();
                    bFind = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return bFind;
        }


        private void frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!fored2Close)
                if (!Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                {
                    e.Cancel = true;
                }
        }

        private void ntfSystemInfo_DoubleClick(object sender, EventArgs e)
        {
            Show();
            ntfSystemInfo.Visible = false;
        }

        /// <summary>
        /// hàm thực hienje viecj thay đổi thông tin mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangePass_Click(object sender, EventArgs e)
        {
            var frm = new frm_ChangePass();
            frm.ShowDialog();
        }

        private void PanelManager_PanelAutoHideChanged(object sender, PanelActionEventArgs e)
        {
            WriteHideForm();
        }

        private void WriteHideForm()
        {
            if (File.Exists(sFileName))
            {
                File.WriteAllText(sFileName, pMain.AutoHide ? "1" : "0");
            }
            else
            {
                File.WriteAllText(sFileName, pMain.AutoHide ? "1" : "0");
            }
        }

        private void StatusBar_PanelClick(object sender, StatusBarEventArgs e)
        {
        }

        private void cmdManHinhGhiNho_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện khởi tạo thông tin khi datetime
        /// </summary>
        private void IntialDateTime()
        {

           // new HISData().LoadList();
        }

       
        private void cmdNoiDKBD_Click(object sender, EventArgs e)
        {
            var frm = new frm_DonVi_LamViec();
            frm.ShowDialog();
        }

       

        private void cmdLogout_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn muốn thoát chương trình đang chạy không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }

        #region "Khởi tạo thông tin menu system"
        /// <summary>
        /// hàm thực hiện việc cập nhập đơn vị làm việc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDonVi_LamViec_Click(object sender, EventArgs e)
        {
            var frm = new frm_DonVi_LamViec();
            frm.ShowDialog();
        }
        /// <summary>
        /// hàm thực iheenj viec thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExitMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn thoát khỏi chương trình không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// hàm thực hiện đăng nhập lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLoginMenuItem_Click(object sender, EventArgs e)
        {

            var frmDangNhap = new frm_Login();
            frmDangNhap.ShowDialog();
        }

        /// <summary>
        /// hàm thực hiện việc thay đổi mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frm_ChangePass();
            frm.ShowDialog();
        }

        #endregion

        #region "Khởi tạo đối tượng"

        private readonly ToolStripMenuItem SystemToolStripMenuItem = new ToolStripMenuItem();
        private readonly ToolStripSeparator ToolStripSeparator1 = new ToolStripSeparator();
        private readonly ToolStripSeparator ToolStripSeparator2 = new ToolStripSeparator();

        private readonly ToolStripSeparator ToolStripSeparator3 = new ToolStripSeparator();
        private readonly ToolStripSeparator ToolStripSeparator4 = new ToolStripSeparator();
        private readonly ToolStripMenuItem cmdChangePassword = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuDonVi_LamViec = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuExit = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuLogin = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuLogout = new ToolStripMenuItem();

        #endregion

        #region Nested type: SYSTEMTIME

        public partial struct SYSTEMTIME
        {
            public ushort wDay;
            public ushort wDayOfWeek;
            public ushort wHour;
            public ushort wMilliseconds;
            public ushort wMinute;
            public ushort wMonth;
            public ushort wSecond;
            public ushort wYear;
        }

        #endregion
        List<string> lstTab = new List<string>();
        Dictionary<string, UIMdiChildTab> dic = new Dictionary<string, UIMdiChildTab>();
        private void PanelManager_MdiTabCreated(object sender, MdiTabEventArgs e)
        {
           
            if (!lstTab.Contains(e.Tab.Form.Tag.ToString())) lstTab.Add(e.Tab.Form.Tag.ToString());
            if (!dic.ContainsKey(e.Tab.Form.Tag.ToString())) dic.Add(e.Tab.Form.Tag.ToString(), e.Tab);
        }

        private void PanelManager_MdiTabClosed(object sender, MdiTabClosedEventArgs e)
        {
            
            if (lstTab.Contains(e.Tab.Form.Tag.ToString())) lstTab.Remove(e.Tab.Form.Tag.ToString());
            if (dic.ContainsKey(e.Tab.Form.Tag.ToString())) dic.Remove(e.Tab.Form.Tag.ToString());
        }

        private void PanelManager_MdiTabClick(object sender, MdiTabEventArgs e)
        {
           
        }

        private void cmdrelogin_Click(object sender, EventArgs e)
        {
            var frm = new frm_Login();
            frm.blnRelogin = !isLoginFirstTime;
            frm.ShowDialog();
            if (!frm.b_Cancel)
            {
                if (globalVariables.LoginSuceess)
                {
                    ClearTab();
                    LoadFormMain();
                }
            }
            else//Nhấn Cancel thì quay lại giao diện cũ không thực hiện gì cả
                if (!isLoginFirstTime) globalVariables.LoginSuceess = true;
        }

        private void cmdChangePWD_Click(object sender, EventArgs e)
        {
            frm_ChangePass _ChangePass = new frm_ChangePass();
            _ChangePass.ShowDialog();
        }
    }

    
}