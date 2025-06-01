namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_dmuc_doituongBHYT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dmuc_doituongBHYT));
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.GridEX.GridEXLayout grd_Insurance_Objects_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdQhe_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.sysColor = new System.Windows.Forms.ToolStrip();
            this.cmdNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiCheckBox1 = new Janus.Windows.EditControls.UICheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboGroupInsurance = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDieaseName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInsObject_Code = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboObjectType_ID = new System.Windows.Forms.ComboBox();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grd_Insurance_Objects = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdQhe = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdXoaquyenloi = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdInsert = new Janus.Windows.EditControls.UIButton();
            this.ucError1 = new VNS.HIS.UCs.UCError();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.sysColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Insurance_Objects)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdQhe)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sysColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.toolStripSeparator1,
            this.cmdEdit,
            this.toolStripSeparator2,
            this.cmdDelete,
            this.toolStripSeparator4,
            this.cmdSaveAll,
            this.toolStripButton1});
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(902, 39);
            this.sysColor.TabIndex = 4;
            this.sysColor.Text = "toolStrip1";
            // 
            // cmdNew
            // 
            this.cmdNew.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(144, 36);
            this.cmdNew.Text = "Thêm mới (Ctrl+N)";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(106, 36);
            this.cmdEdit.Text = "Sửa(Ctrl+E)";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(102, 36);
            this.cmdDelete.Text = "Xoá(Ctrl+D)";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdSaveAll
            // 
            this.cmdSaveAll.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveAll.Image")));
            this.cmdSaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSaveAll.Name = "cmdSaveAll";
            this.cmdSaveAll.Size = new System.Drawing.Size(106, 36);
            this.cmdSaveAll.Text = "Lưu toàn bộ";
            this.cmdSaveAll.Click += new System.EventHandler(this.cmdSaveAll_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(107, 36);
            this.toolStripButton1.Text = "Thoát(Esc)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.uiGroupBox1.Controls.Add(this.uiCheckBox1);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.cboGroupInsurance);
            this.uiGroupBox1.Controls.Add(this.cmdSearch);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtDieaseName);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtInsObject_Code);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboObjectType_ID);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(902, 85);
            this.uiGroupBox1.TabIndex = 5;
            this.uiGroupBox1.Text = "Thông tin tìm kiếm";
            // 
            // uiCheckBox1
            // 
            this.uiCheckBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiCheckBox1.Location = new System.Drawing.Point(581, 48);
            this.uiCheckBox1.Name = "uiCheckBox1";
            this.uiCheckBox1.Size = new System.Drawing.Size(178, 23);
            this.uiCheckBox1.TabIndex = 13;
            this.uiCheckBox1.Text = "Nhóm theo đối tượng";
            this.uiCheckBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(332, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Nhóm";
            // 
            // cboGroupInsurance
            // 
            this.cboGroupInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroupInsurance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cboGroupInsurance.FormattingEnabled = true;
            this.cboGroupInsurance.Location = new System.Drawing.Point(406, 13);
            this.cboGroupInsurance.Name = "cboGroupInsurance";
            this.cboGroupInsurance.Size = new System.Drawing.Size(216, 24);
            this.cboGroupInsurance.TabIndex = 11;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSearch.Location = new System.Drawing.Point(765, 13);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(128, 54);
            this.cmdSearch.TabIndex = 10;
            this.cmdSearch.Text = "&Tìm kiếm(F3)";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(322, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tên ĐTBH";
            // 
            // txtDieaseName
            // 
            this.txtDieaseName.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtDieaseName.Location = new System.Drawing.Point(406, 49);
            this.txtDieaseName.Name = "txtDieaseName";
            this.txtDieaseName.Size = new System.Drawing.Size(169, 22);
            this.txtDieaseName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(7, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã ĐTBH";
            // 
            // txtInsObject_Code
            // 
            this.txtInsObject_Code.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtInsObject_Code.Location = new System.Drawing.Point(100, 49);
            this.txtInsObject_Code.Name = "txtInsObject_Code";
            this.txtInsObject_Code.Size = new System.Drawing.Size(216, 22);
            this.txtInsObject_Code.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Đối tượng BH";
            // 
            // cboObjectType_ID
            // 
            this.cboObjectType_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectType_ID.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cboObjectType_ID.FormattingEnabled = true;
            this.cboObjectType_ID.Location = new System.Drawing.Point(100, 16);
            this.cboObjectType_ID.Name = "cboObjectType_ID";
            this.cboObjectType_ID.Size = new System.Drawing.Size(216, 24);
            this.cboObjectType_ID.TabIndex = 4;
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 579);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Ctrl+N: thêm mới";
            uiStatusBarPanel1.Width = 117;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Ctrl+E: sửa thông tin ";
            uiStatusBarPanel2.Width = 142;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Ctrl+D: xóa thông tin ";
            uiStatusBarPanel3.Width = 140;
            uiStatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel4.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel4.Key = "";
            uiStatusBarPanel4.ProgressBarValue = 0;
            uiStatusBarPanel4.Text = "Esc:Thoát Form hiện tại";
            uiStatusBarPanel4.Width = 155;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3,
            uiStatusBarPanel4});
            this.uiStatusBar1.Size = new System.Drawing.Size(902, 23);
            this.uiStatusBar1.TabIndex = 6;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grd_Insurance_Objects);
            this.uiGroupBox2.Controls.Add(this.panel1);
            this.uiGroupBox2.Controls.Add(this.ucError1);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 124);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(902, 455);
            this.uiGroupBox2.TabIndex = 7;
            this.uiGroupBox2.Text = "&Thông tin bảo hiểm";
            // 
            // grd_Insurance_Objects
            // 
            this.grd_Insurance_Objects.BuiltInTextsData = resources.GetString("grd_Insurance_Objects.BuiltInTextsData");
            grd_Insurance_Objects_DesignTimeLayout.LayoutString = resources.GetString("grd_Insurance_Objects_DesignTimeLayout.LayoutString");
            this.grd_Insurance_Objects.DesignTimeLayout = grd_Insurance_Objects_DesignTimeLayout;
            this.grd_Insurance_Objects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Insurance_Objects.DynamicFiltering = true;
            this.grd_Insurance_Objects.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grd_Insurance_Objects.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grd_Insurance_Objects.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grd_Insurance_Objects.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grd_Insurance_Objects.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grd_Insurance_Objects.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_Insurance_Objects.GroupByBoxVisible = false;
            this.grd_Insurance_Objects.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grd_Insurance_Objects.Location = new System.Drawing.Point(3, 17);
            this.grd_Insurance_Objects.Name = "grd_Insurance_Objects";
            this.grd_Insurance_Objects.RecordNavigator = true;
            this.grd_Insurance_Objects.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_Insurance_Objects.Size = new System.Drawing.Size(560, 435);
            this.grd_Insurance_Objects.TabIndex = 11;
            this.grd_Insurance_Objects.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grd_Insurance_Objects.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdQhe);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(563, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 435);
            this.panel1.TabIndex = 10;
            // 
            // grdQhe
            // 
            this.grdQhe.BuiltInTextsData = resources.GetString("grdQhe.BuiltInTextsData");
            grdQhe_DesignTimeLayout.LayoutString = resources.GetString("grdQhe_DesignTimeLayout.LayoutString");
            this.grdQhe.DesignTimeLayout = grdQhe_DesignTimeLayout;
            this.grdQhe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQhe.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdQhe.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdQhe.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdQhe.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdQhe.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdQhe.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdQhe.GroupByBoxVisible = false;
            this.grdQhe.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdQhe.Location = new System.Drawing.Point(0, 0);
            this.grdQhe.Name = "grdQhe";
            this.grdQhe.RecordNavigator = true;
            this.grdQhe.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdQhe.Size = new System.Drawing.Size(336, 391);
            this.grdQhe.TabIndex = 12;
            this.grdQhe.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdQhe.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdXoaquyenloi);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdInsert);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(336, 44);
            this.panel2.TabIndex = 11;
            // 
            // cmdXoaquyenloi
            // 
            this.cmdXoaquyenloi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdXoaquyenloi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoaquyenloi.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoaquyenloi.Image")));
            this.cmdXoaquyenloi.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdXoaquyenloi.Location = new System.Drawing.Point(121, 13);
            this.cmdXoaquyenloi.Name = "cmdXoaquyenloi";
            this.cmdXoaquyenloi.Size = new System.Drawing.Size(99, 28);
            this.cmdXoaquyenloi.TabIndex = 447;
            this.cmdXoaquyenloi.Text = "Xóa";
            this.toolTip1.SetToolTip(this.cmdXoaquyenloi, "Xóa quan hệ quyền lợi đang chọn");
            this.cmdXoaquyenloi.ToolTipText = "Xóa";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(226, 13);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(99, 28);
            this.cmdSave.TabIndex = 448;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.Image = ((System.Drawing.Image)(resources.GetObject("cmdInsert.Image")));
            this.cmdInsert.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInsert.Location = new System.Drawing.Point(11, 13);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(99, 28);
            this.cmdInsert.TabIndex = 445;
            this.cmdInsert.Text = "Thêm";
            this.toolTip1.SetToolTip(this.cmdInsert, "Thêm quan hệ đầu thẻ-mã quyền lợi cho đầu thẻ đang chọn");
            this.cmdInsert.ToolTipText = "Lưu lại thông tin ";
            // 
            // ucError1
            // 
            this.ucError1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucError1.BackgroundImage")));
            this.ucError1.Color1 = System.Drawing.Color.Yellow;
            this.ucError1.Color2 = System.Drawing.Color.Red;
            this.ucError1.EllapsedTime = 1000;
            this.ucError1.ErrText = "Error";
            this.ucError1.Location = new System.Drawing.Point(11, 26);
            this.ucError1.Name = "ucError1";
            this.ucError1.NumberofBrlink = 5;
            this.ucError1.Size = new System.Drawing.Size(82, 67);
            this.ucError1.TabIndex = 9;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_dmuc_doituongBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 602);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiStatusBar1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.sysColor);
            this.KeyPreview = true;
            this.Name = "frm_dmuc_doituongBHYT";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Các đối tượng tham gia bảo hiểm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_dmuc_doituongBHYT_Load);
            this.sysColor.ResumeLayout(false);
            this.sysColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Insurance_Objects)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdQhe)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip sysColor;
        private System.Windows.Forms.ToolStripButton cmdNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtDieaseName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtInsObject_Code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboObjectType_ID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboGroupInsurance;
        private System.Windows.Forms.ToolStripButton cmdSaveAll;
        private Janus.Windows.EditControls.UICheckBox uiCheckBox1;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grd_Insurance_Objects;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.GridEX.GridEX grdQhe;
        private System.Windows.Forms.Panel panel2;
        private UCs.UCError ucError1;
        private Janus.Windows.EditControls.UIButton cmdXoaquyenloi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdInsert;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}