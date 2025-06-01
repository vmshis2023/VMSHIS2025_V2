namespace CIS.CoreApp
{
    partial class frm_MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Node14");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Node9", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MainForm));
            this.treeView = new System.Windows.Forms.TreeView();
            this.pMainConent = new Janus.Windows.UI.Dock.UIPanelGroup();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ntfSystemInfo = new System.Windows.Forms.NotifyIcon(this.components);
            this.SystemLogin = new System.Windows.Forms.ImageList(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdMainPanel = new System.Windows.Forms.Button();
            this.cmdrelogin = new System.Windows.Forms.Button();
            this.cmdChangePWD = new System.Windows.Forms.Button();
            this.cmdLoadSysparams = new Janus.Windows.EditControls.UIButton();
            this.cmdHelp = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblHospital = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDepartment = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelManager = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.pMain = new Janus.Windows.UI.Dock.UIPanelGroup();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxCustom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.pMainConent)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMain)).BeginInit();
            this.ctxCustom.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Node0";
            treeNode17.Name = "Node1";
            treeNode17.Text = "Node1";
            treeNode18.Name = "Node2";
            treeNode18.Text = "Node2";
            treeNode19.Name = "Node3";
            treeNode19.Text = "Node3";
            treeNode20.Name = "Node4";
            treeNode20.Text = "Node4";
            treeNode21.Name = "Node5";
            treeNode21.Text = "Node5";
            treeNode22.Name = "Node6";
            treeNode22.Text = "Node6";
            treeNode23.Name = "Node7";
            treeNode23.Text = "Node7";
            treeNode24.Name = "Node8";
            treeNode24.Text = "Node8";
            treeNode25.Name = "Node10";
            treeNode25.Text = "Node10";
            treeNode26.Name = "Node11";
            treeNode26.Text = "Node11";
            treeNode27.Name = "Node12";
            treeNode27.Text = "Node12";
            treeNode28.Name = "Node13";
            treeNode28.Text = "Node13";
            treeNode29.Name = "Node14";
            treeNode29.Text = "Node14";
            treeNode30.Name = "Node9";
            treeNode30.Text = "Node9";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode30});
            this.treeView.Size = new System.Drawing.Size(196, 461);
            this.treeView.TabIndex = 0;
            // 
            // pMainConent
            // 
            this.pMainConent.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.Tab;
            this.pMainConent.Location = new System.Drawing.Point(203, 3);
            this.pMainConent.Name = "pMainConent";
            this.pMainConent.Size = new System.Drawing.Size(723, 531);
            this.pMainConent.TabIndex = 4;
            this.pMainConent.Text = "Panel 0";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(261, 461);
            this.treeView1.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "OPEN2.PNG");
            this.imageList.Images.SetKeyName(1, "Folder.png");
            this.imageList.Images.SetKeyName(2, "Folder.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            this.imageList1.Images.SetKeyName(1, "system_config_boot_gray.png");
            this.imageList1.Images.SetKeyName(2, "F1.jpg");
            this.imageList1.Images.SetKeyName(3, "1348021754_Folder.png");
            this.imageList1.Images.SetKeyName(4, "1373036916_dossier-blue-pictures.png");
            this.imageList1.Images.SetKeyName(5, "1348021754_Folder_gray3.png");
            this.imageList1.Images.SetKeyName(6, "NEXT.PNG");
            this.imageList1.Images.SetKeyName(7, "suport-F-offline.png");
            this.imageList1.Images.SetKeyName(8, "USER-ADD.PNG");
            this.imageList1.Images.SetKeyName(9, "Close_Gray.png");
            this.imageList1.Images.SetKeyName(10, "excel1.ico");
            this.imageList1.Images.SetKeyName(11, "FOLDER.PNG");
            this.imageList1.Images.SetKeyName(12, "next_gray1.png");
            this.imageList1.Images.SetKeyName(13, "set_smart_folder_gray.png");
            this.imageList1.Images.SetKeyName(14, "STOP_gray.PNG");
            this.imageList1.Images.SetKeyName(15, "PASTE.PNG");
            this.imageList1.Images.SetKeyName(16, "FOLDER.PNG");
            this.imageList1.Images.SetKeyName(17, "Folder.png");
            // 
            // ntfSystemInfo
            // 
            this.ntfSystemInfo.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ntfSystemInfo.BalloonTipText = "Hệ thống quản lý phòng khám";
            this.ntfSystemInfo.BalloonTipTitle = "Hệ thống quản lý phòng khám";
            this.ntfSystemInfo.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfSystemInfo.Icon")));
            this.ntfSystemInfo.Text = "Hệ thống quản lý bệnh viện";
            this.ntfSystemInfo.DoubleClick += new System.EventHandler(this.ntfSystemInfo_DoubleClick);
            // 
            // SystemLogin
            // 
            this.SystemLogin.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SystemLogin.ImageStream")));
            this.SystemLogin.TransparentColor = System.Drawing.Color.Transparent;
            this.SystemLogin.Images.SetKeyName(0, "USER-ADD.PNG");
            this.SystemLogin.Images.SetKeyName(1, "user-remove.png");
            this.SystemLogin.Images.SetKeyName(2, "USERS.PNG");
            this.SystemLogin.Images.SetKeyName(3, "OPEN2.PNG");
            this.SystemLogin.Images.SetKeyName(4, "REDO.PNG");
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.flowLayoutPanel1);
            this.pnlHeader.Controls.Add(this.panel1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1360, 40);
            this.pnlHeader.TabIndex = 8;
            this.toolTip1.SetToolTip(this.pnlHeader, "Nhấn F12 để ẩn(hiện) tiêu đề");
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(63, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(718, 40);
            this.label1.TabIndex = 10;
            this.label1.Text = "QUẢN LÝ THÔNG TIN BỆNH VIỆN - HIS.NET";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.label1, "Nhấn F12 để ẩn(hiện) tiêu đề");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdClose);
            this.flowLayoutPanel1.Controls.Add(this.cmdHelp);
            this.flowLayoutPanel1.Controls.Add(this.cmdLoadSysparams);
            this.flowLayoutPanel1.Controls.Add(this.cmdChangePWD);
            this.flowLayoutPanel1.Controls.Add(this.cmdrelogin);
            this.flowLayoutPanel1.Controls.Add(this.cmdMainPanel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(781, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(579, 40);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // cmdMainPanel
            // 
            this.cmdMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMainPanel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdMainPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdMainPanel.ForeColor = System.Drawing.Color.White;
            this.cmdMainPanel.Location = new System.Drawing.Point(35, 3);
            this.cmdMainPanel.Name = "cmdMainPanel";
            this.cmdMainPanel.Size = new System.Drawing.Size(153, 35);
            this.cmdMainPanel.TabIndex = 7;
            this.cmdMainPanel.Text = "Bảng điều khiển";
            this.cmdMainPanel.UseVisualStyleBackColor = true;
            this.cmdMainPanel.Visible = false;
            // 
            // cmdrelogin
            // 
            this.cmdrelogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdrelogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdrelogin.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdrelogin.ForeColor = System.Drawing.Color.White;
            this.cmdrelogin.Location = new System.Drawing.Point(194, 3);
            this.cmdrelogin.Name = "cmdrelogin";
            this.cmdrelogin.Size = new System.Drawing.Size(113, 35);
            this.cmdrelogin.TabIndex = 8;
            this.cmdrelogin.Text = "Đăng nhập lại";
            this.cmdrelogin.UseVisualStyleBackColor = true;
            this.cmdrelogin.Click += new System.EventHandler(this.cmdrelogin_Click);
            // 
            // cmdChangePWD
            // 
            this.cmdChangePWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChangePWD.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdChangePWD.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdChangePWD.ForeColor = System.Drawing.Color.White;
            this.cmdChangePWD.Location = new System.Drawing.Point(313, 3);
            this.cmdChangePWD.Name = "cmdChangePWD";
            this.cmdChangePWD.Size = new System.Drawing.Size(113, 35);
            this.cmdChangePWD.TabIndex = 1;
            this.cmdChangePWD.Text = "Đổi mật khẩu";
            this.cmdChangePWD.UseVisualStyleBackColor = true;
            this.cmdChangePWD.Click += new System.EventHandler(this.cmdChangePWD_Click);
            // 
            // cmdLoadSysparams
            // 
            this.cmdLoadSysparams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadSysparams.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadSysparams.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadSysparams.Image")));
            this.cmdLoadSysparams.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdLoadSysparams.Location = new System.Drawing.Point(432, 3);
            this.cmdLoadSysparams.Name = "cmdLoadSysparams";
            this.cmdLoadSysparams.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdLoadSysparams.Office2007CustomColor = System.Drawing.Color.White;
            this.cmdLoadSysparams.Size = new System.Drawing.Size(44, 35);
            this.cmdLoadSysparams.TabIndex = 372;
            this.toolTip1.SetToolTip(this.cmdLoadSysparams, "Nạp lại biến hệ thống");
            this.cmdLoadSysparams.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cmdHelp
            // 
            this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHelp.Image = ((System.Drawing.Image)(resources.GetObject("cmdHelp.Image")));
            this.cmdHelp.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdHelp.Location = new System.Drawing.Point(482, 3);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdHelp.Office2007CustomColor = System.Drawing.Color.White;
            this.cmdHelp.Size = new System.Drawing.Size(44, 35);
            this.cmdHelp.TabIndex = 373;
            this.toolTip1.SetToolTip(this.cmdHelp, "Trợ giúp");
            this.cmdHelp.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdClose.Location = new System.Drawing.Point(532, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdClose.Office2007CustomColor = System.Drawing.Color.White;
            this.cmdClose.Size = new System.Drawing.Size(44, 35);
            this.cmdClose.TabIndex = 374;
            this.toolTip1.SetToolTip(this.cmdClose, "Thoát khỏi chương trình");
            this.cmdClose.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(63, 40);
            this.panel1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.panel1, "Nhấn F12 để ẩn(hiện) tiêu đề");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblHospital,
            this.lblDepartment,
            this.lblUser,
            this.lblIP,
            this.lblCopyright});
            this.statusStrip1.Location = new System.Drawing.Point(0, 718);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1360, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblHospital
            // 
            this.lblHospital.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospital.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHospital.Name = "lblHospital";
            this.lblHospital.Size = new System.Drawing.Size(65, 17);
            this.lblHospital.Text = "Bệnh viện:";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(78, 17);
            this.lblDepartment.Text = "Khoa-phòng:";
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(80, 17);
            this.lblUser.Text = "Người dùng: ";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIP
            // 
            this.lblIP.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(21, 17);
            this.lblIP.Text = "IP:";
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(1099, 17);
            this.lblCopyright.Spring = true;
            this.lblCopyright.Text = "COPYRIGHT © Công ty cổ phần CNTT VINASOFT";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PanelManager
            // 
            this.PanelManager.AllowAutoHideAnimation = false;
            this.PanelManager.BackColorSplitter = System.Drawing.Color.Navy;
            this.PanelManager.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><AutoHidePanelToolTip>" +
                "Tự động ẩn</AutoHidePanelToolTip><CloseMdiToolTip>Đóng Form hiện tại</CloseMdiTo" +
                "olTip></LocalizableData>";
            this.PanelManager.ContainerControl = this;
            this.PanelManager.DefaultPanelSettings.ActiveCaptionFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.PanelManager.DefaultPanelSettings.ActiveCaptionFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.PanelManager.DefaultPanelSettings.ActiveCaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.PanelManager.DefaultPanelSettings.ActiveCaptionMode = Janus.Windows.UI.Dock.ActiveCaptionMode.ContainerCaptionFocus;
            this.PanelManager.DefaultPanelSettings.CaptionFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.PanelManager.DefaultPanelSettings.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.PanelManager.DefaultPanelSettings.CloseButtonImage = ((System.Drawing.Image)(resources.GetObject("PanelManager.DefaultPanelSettings.CloseButtonImage")));
            this.PanelManager.DefaultPanelSettings.TabDisplay = Janus.Windows.UI.Dock.TabDisplayMode.ImageAndTextOnSelected;
            this.PanelManager.DefaultPanelSettings.TabStateStyles.PressedFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.PanelManager.DefaultPanelSettings.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.PanelManager.ImageList = this.imageList1;
            this.PanelManager.LargeImageList = this.imageList1;
            this.PanelManager.SplitterSize = 2;
            this.PanelManager.TabbedMdi = true;
            this.PanelManager.TabbedMdiSettings.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.VS2005;
            this.PanelManager.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.VS2005;
            this.pMain.Id = new System.Guid("9b8ad153-3e99-4f09-a06e-6d569af67e03");
            this.pMain.StaticGroup = true;
            this.PanelManager.Panels.Add(this.pMain);
            // 
            // Design Time Panel Info:
            // 
            this.PanelManager.BeginPanelInfo();
            this.PanelManager.AddDockPanelInfo(new System.Guid("9b8ad153-3e99-4f09-a06e-6d569af67e03"), Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator, Janus.Windows.UI.Dock.PanelDockStyle.Left, true, new System.Drawing.Size(268, 494), true);
            this.PanelManager.AddFloatingPanelInfo(new System.Guid("9b8ad153-3e99-4f09-a06e-6d569af67e03"), Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator, true, new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.PanelManager.EndPanelInfo();
            // 
            // pMain
            // 
            this.pMain.AutoHide = true;
            this.pMain.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.pMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pMain.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator;
            this.pMain.Location = new System.Drawing.Point(3, 41);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(268, 494);
            this.pMain.TabIndex = 4;
            this.pMain.TabStop = false;
            this.pMain.Text = "Phân hệ";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // ctxCustom
            // 
            this.ctxCustom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.ctxCustom.Name = "ctxCustom";
            this.ctxCustom.Size = new System.Drawing.Size(265, 26);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(264, 22);
            this.mnuSettings.Text = "Cấu hình cho chức năng được chọn";
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 40);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1360, 24);
            this.menuStrip.TabIndex = 11;
            this.menuStrip.Text = "menuStrip1";
            // 
            // frm_MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1360, 740);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frm_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thông tin Bệnh viện - HIS.NET";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pMainConent)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMain)).EndInit();
            this.ctxCustom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.UI.Dock.UIPanelGroup pMainConent;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.NotifyIcon ntfSystemInfo;
        private System.Windows.Forms.ImageList SystemLogin;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Janus.Windows.UI.Dock.UIPanelManager PanelManager;
        private Janus.Windows.UI.Dock.UIPanelGroup pMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdChangePWD;
        private System.Windows.Forms.Button cmdMainPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblCopyright;
        private System.Windows.Forms.Button cmdrelogin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel lblDepartment;
        private System.Windows.Forms.ContextMenuStrip ctxCustom;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private Janus.Windows.EditControls.UIButton cmdLoadSysparams;
        private Janus.Windows.EditControls.UIButton cmdHelp;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private System.Windows.Forms.ToolStripStatusLabel lblHospital;
        private System.Windows.Forms.ToolStripStatusLabel lblIP;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Label label1;
    }
}