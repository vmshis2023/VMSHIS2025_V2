namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Quanlychuyenvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Quanlychuyenvien));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdInsert = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdate = new System.Windows.Forms.ToolStripButton();
            this.cmdDelete = new System.Windows.Forms.ToolStripButton();
            this.cmdPrint = new System.Windows.Forms.ToolStripButton();
            this.cmdExit = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.lnkDelete = new System.Windows.Forms.LinkLabel();
            this.cboTrangthai = new Janus.Windows.EditControls.UIComboBox();
            this.txtBacsi = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label25 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtNoichuyenden = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTennguoibenh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMaluotkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpNgayin = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.mnuSuaBuongGiuong = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdsuagiuong = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdxoagiuong = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMauPhieu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdInPhieuVaoVien = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdInCamKet = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdBienBanHoiChan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.mnuSuaBuongGiuong.SuspendLayout();
            this.ctxMauPhieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdInsert,
            this.cmdUpdate,
            this.cmdDelete,
            this.cmdPrint,
            this.cmdExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.Transparent;
            this.cmdInsert.Font = new System.Drawing.Font("Arial", 10F);
            this.cmdInsert.Image = ((System.Drawing.Image)(resources.GetObject("cmdInsert.Image")));
            this.cmdInsert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(99, 36);
            this.cmdInsert.Text = "Thêm mới";
            this.cmdInsert.ToolTipText = "Thêm mới phiếu chuyển tuyến";
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Enabled = false;
            this.cmdUpdate.Font = new System.Drawing.Font("Arial", 10F);
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(94, 36);
            this.cmdUpdate.Text = "Cập nhật";
            this.cmdUpdate.ToolTipText = "Cập nhật thông tin phiếu chuyển tuyến";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(99, 36);
            this.cmdDelete.Text = "Hủy phiếu";
            this.cmdDelete.ToolTipText = "Xóa phiếu chuyển tuyến";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(86, 36);
            this.cmdPrint.Text = "In phiếu";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 10F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(81, 36);
            this.cmdExit.Text = "Thoát";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lnkDelete);
            this.uiGroupBox1.Controls.Add(this.cboTrangthai);
            this.uiGroupBox1.Controls.Add(this.txtBacsi);
            this.uiGroupBox1.Controls.Add(this.label25);
            this.uiGroupBox1.Controls.Add(this.label22);
            this.uiGroupBox1.Controls.Add(this.txtNoichuyenden);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtTennguoibenh);
            this.uiGroupBox1.Controls.Add(this.txtMaluotkham);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 105);
            this.uiGroupBox1.TabIndex = 5;
            this.uiGroupBox1.Text = "Thông tin tìm kiếm";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // lnkDelete
            // 
            this.lnkDelete.AutoSize = true;
            this.lnkDelete.Location = new System.Drawing.Point(652, 82);
            this.lnkDelete.Name = "lnkDelete";
            this.lnkDelete.Size = new System.Drawing.Size(276, 15);
            this.lnkDelete.TabIndex = 40;
            this.lnkDelete.TabStop = true;
            this.lnkDelete.Text = "Xóa các điều kiện tìm kiếm về trạng thái mặc định";
            this.lnkDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDelete_LinkClicked);
            // 
            // cboTrangthai
            // 
            this.cboTrangthai.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboTrangthai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = 10;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Đủ điều kiện chuyển tuyến (đúng tuyến)";
            uiComboBoxItem2.Value = 0;
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Không đủ điều kiện chuyển tuyến/chuyển tuyến theo yêu cầu nhười bệnh (Vượt tuyến)" +
    "";
            uiComboBoxItem3.Value = 1;
            this.cboTrangthai.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboTrangthai.Location = new System.Drawing.Point(88, 76);
            this.cboTrangthai.Name = "cboTrangthai";
            this.cboTrangthai.SelectedIndex = 0;
            this.cboTrangthai.Size = new System.Drawing.Size(460, 21);
            this.cboTrangthai.TabIndex = 9;
            this.cboTrangthai.TabStop = false;
            this.cboTrangthai.Text = "Tất cả";
            this.cboTrangthai.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // txtBacsi
            // 
            this.txtBacsi._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBacsi._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsi._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBacsi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBacsi.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBacsi.AutoCompleteList")));
            this.txtBacsi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacsi.buildShortcut = false;
            this.txtBacsi.CaseSensitive = false;
            this.txtBacsi.CompareNoID = true;
            this.txtBacsi.DefaultCode = "-1";
            this.txtBacsi.DefaultID = "-1";
            this.txtBacsi.DisplayType = 1;
            this.txtBacsi.Drug_ID = null;
            this.txtBacsi.ExtraWidth = 100;
            this.txtBacsi.FillValueAfterSelect = false;
            this.txtBacsi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsi.Location = new System.Drawing.Point(655, 52);
            this.txtBacsi.MaxHeight = 289;
            this.txtBacsi.MinTypedCharacters = 2;
            this.txtBacsi.MyCode = "-1";
            this.txtBacsi.MyID = "-1";
            this.txtBacsi.MyText = "";
            this.txtBacsi.MyTextOnly = "";
            this.txtBacsi.Name = "txtBacsi";
            this.txtBacsi.RaiseEvent = true;
            this.txtBacsi.RaiseEventEnter = true;
            this.txtBacsi.RaiseEventEnterWhenEmpty = true;
            this.txtBacsi.SelectedIndex = -1;
            this.txtBacsi.Size = new System.Drawing.Size(188, 21);
            this.txtBacsi.splitChar = '@';
            this.txtBacsi.splitCharIDAndCode = '#';
            this.txtBacsi.TabIndex = 8;
            this.txtBacsi.TakeCode = false;
            this.txtBacsi.txtMyCode = null;
            this.txtBacsi.txtMyCode_Edit = null;
            this.txtBacsi.txtMyID = null;
            this.txtBacsi.txtMyID_Edit = null;
            this.txtBacsi.txtMyName = null;
            this.txtBacsi.txtMyName_Edit = null;
            this.txtBacsi.txtNext = null;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(15, 75);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(67, 23);
            this.label25.TabIndex = 37;
            this.label25.Text = "Lý do cv:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(559, 52);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(90, 23);
            this.label22.TabIndex = 35;
            this.label22.Text = "Người chuyển:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNoichuyenden
            // 
            this.txtNoichuyenden._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNoichuyenden._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoichuyenden._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNoichuyenden.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNoichuyenden.AutoCompleteList")));
            this.txtNoichuyenden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoichuyenden.buildShortcut = false;
            this.txtNoichuyenden.CaseSensitive = false;
            this.txtNoichuyenden.CompareNoID = true;
            this.txtNoichuyenden.DefaultCode = "-1";
            this.txtNoichuyenden.DefaultID = "-1";
            this.txtNoichuyenden.DisplayType = 1;
            this.txtNoichuyenden.Drug_ID = null;
            this.txtNoichuyenden.ExtraWidth = 100;
            this.txtNoichuyenden.FillValueAfterSelect = false;
            this.txtNoichuyenden.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoichuyenden.Location = new System.Drawing.Point(88, 52);
            this.txtNoichuyenden.MaxHeight = 289;
            this.txtNoichuyenden.MinTypedCharacters = 2;
            this.txtNoichuyenden.MyCode = "-1";
            this.txtNoichuyenden.MyID = "-1";
            this.txtNoichuyenden.MyText = "";
            this.txtNoichuyenden.MyTextOnly = "";
            this.txtNoichuyenden.Name = "txtNoichuyenden";
            this.txtNoichuyenden.RaiseEvent = true;
            this.txtNoichuyenden.RaiseEventEnter = true;
            this.txtNoichuyenden.RaiseEventEnterWhenEmpty = true;
            this.txtNoichuyenden.SelectedIndex = -1;
            this.txtNoichuyenden.Size = new System.Drawing.Size(460, 21);
            this.txtNoichuyenden.splitChar = '@';
            this.txtNoichuyenden.splitCharIDAndCode = '#';
            this.txtNoichuyenden.TabIndex = 7;
            this.txtNoichuyenden.TakeCode = false;
            this.txtNoichuyenden.txtMyCode = null;
            this.txtNoichuyenden.txtMyCode_Edit = null;
            this.txtNoichuyenden.txtMyID = null;
            this.txtNoichuyenden.txtMyID_Edit = null;
            this.txtNoichuyenden.txtMyName = null;
            this.txtNoichuyenden.txtMyName_Edit = null;
            this.txtNoichuyenden.txtNext = null;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(22, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 31;
            this.label3.Text = "Nơi đến:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTimKiem.Location = new System.Drawing.Point(849, 23);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(153, 50);
            this.cmdTimKiem.TabIndex = 18;
            this.cmdTimKiem.Text = "Tìm kiếm(F3)";
            this.cmdTimKiem.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên bệnh nhân:";
            // 
            // txtTennguoibenh
            // 
            this.txtTennguoibenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTennguoibenh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTennguoibenh.Location = new System.Drawing.Point(655, 26);
            this.txtTennguoibenh.Name = "txtTennguoibenh";
            this.txtTennguoibenh.Size = new System.Drawing.Size(188, 21);
            this.txtTennguoibenh.TabIndex = 5;
            this.txtTennguoibenh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtMaluotkham
            // 
            this.txtMaluotkham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMaluotkham.ButtonFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtMaluotkham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaluotkham.Location = new System.Drawing.Point(461, 26);
            this.txtMaluotkham.Name = "txtMaluotkham";
            this.txtMaluotkham.Size = new System.Drawing.Size(87, 21);
            this.txtMaluotkham.TabIndex = 4;
            this.txtMaluotkham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaluotkham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtToDate.Enabled = false;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(228, 27);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(133, 21);
            this.dtToDate.TabIndex = 1;
            this.dtToDate.Value = new System.DateTime(2015, 6, 12, 0, 0, 0, 0);
            this.dtToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // chkByDate
            // 
            this.chkByDate.Location = new System.Drawing.Point(19, 24);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(67, 27);
            this.chkByDate.TabIndex = 1;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtFromDate.Enabled = false;
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(88, 27);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.Value = new System.DateTime(2015, 6, 12, 0, 0, 0, 0);
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(369, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lượt khám:";
            // 
            // dtpNgayin
            // 
            this.dtpNgayin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayin.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dtpNgayin.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayin.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayin.DropDownCalendar.Name = "";
            this.dtpNgayin.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayin.Enabled = false;
            this.dtpNgayin.Location = new System.Drawing.Point(-156, 707);
            this.dtpNgayin.Name = "dtpNgayin";
            this.dtpNgayin.ShowUpDown = true;
            this.dtpNgayin.Size = new System.Drawing.Size(115, 21);
            this.dtpNgayin.TabIndex = 20;
            this.dtpNgayin.Value = new System.DateTime(2015, 6, 12, 0, 0, 0, 0);
            this.dtpNgayin.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // mnuSuaBuongGiuong
            // 
            this.mnuSuaBuongGiuong.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdsuagiuong,
            this.cmdxoagiuong});
            this.mnuSuaBuongGiuong.Name = "mnuSuaBuongGiuong";
            this.mnuSuaBuongGiuong.Size = new System.Drawing.Size(136, 48);
            this.mnuSuaBuongGiuong.Text = "Sửa buồng giường";
            // 
            // cmdsuagiuong
            // 
            this.cmdsuagiuong.Name = "cmdsuagiuong";
            this.cmdsuagiuong.Size = new System.Drawing.Size(135, 22);
            this.cmdsuagiuong.Text = "Sửa giường";
            // 
            // cmdxoagiuong
            // 
            this.cmdxoagiuong.Name = "cmdxoagiuong";
            this.cmdxoagiuong.Size = new System.Drawing.Size(135, 22);
            this.cmdxoagiuong.Text = "Xóa giường";
            // 
            // ctxMauPhieu
            // 
            this.ctxMauPhieu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdInPhieuVaoVien,
            this.cmdInCamKet,
            this.cmdBienBanHoiChan});
            this.ctxMauPhieu.Name = "ctxMauPhieu";
            this.ctxMauPhieu.Size = new System.Drawing.Size(182, 70);
            // 
            // cmdInPhieuVaoVien
            // 
            this.cmdInPhieuVaoVien.Name = "cmdInPhieuVaoVien";
            this.cmdInPhieuVaoVien.Size = new System.Drawing.Size(181, 22);
            this.cmdInPhieuVaoVien.Text = "1. Phiếu vào viện";
            // 
            // cmdInCamKet
            // 
            this.cmdInCamKet.Name = "cmdInCamKet";
            this.cmdInCamKet.Size = new System.Drawing.Size(181, 22);
            this.cmdInCamKet.Text = "2. Giấy cam kết";
            // 
            // cmdBienBanHoiChan
            // 
            this.cmdBienBanHoiChan.Name = "cmdBienBanHoiChan";
            this.cmdBienBanHoiChan.Size = new System.Drawing.Size(181, 22);
            this.cmdBienBanHoiChan.Text = "3. Biên bản hội chẩn";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 706);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "F3: Tìm kiếm ";
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Esc: Thoát";
            uiStatusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Với mã khám nhập các số cuối khác số 0 và nhấn Enter. Ví dụ tìm mã khám 2300012 t" +
    "hì chỉ cần gõ 12 và Enter.                                       ";
            uiStatusBarPanel3.Width = 798;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3});
            this.uiStatusBar1.Size = new System.Drawing.Size(1008, 23);
            this.uiStatusBar1.TabIndex = 359;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 144);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 562);
            this.uiGroupBox2.TabIndex = 360;
            this.uiGroupBox2.Text = "Danh sách phiếu chuyển viện";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            this.grdList.ContextMenuStrip = this.ctxMauPhieu;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 17);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdList.Size = new System.Drawing.Size(1002, 542);
            this.grdList.TabIndex = 553;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // frm_Quanlychuyenvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.dtpNgayin);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Quanlychuyenvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý phiếu chuyển tuyến";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.mnuSuaBuongGiuong.ResumeLayout(false);
            this.ctxMauPhieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdInsert;
        private System.Windows.Forms.ToolStripButton cmdUpdate;
        private System.Windows.Forms.ToolStripButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTennguoibenh;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaluotkham;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.ToolStripButton cmdDelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip mnuSuaBuongGiuong;
        private System.Windows.Forms.ToolStripMenuItem cmdsuagiuong;
        private System.Windows.Forms.ToolStripMenuItem cmdxoagiuong;
        private System.Windows.Forms.ContextMenuStrip ctxMauPhieu;
        private System.Windows.Forms.ToolStripMenuItem cmdInPhieuVaoVien;
        private System.Windows.Forms.ToolStripMenuItem cmdInCamKet;
        private System.Windows.Forms.ToolStripMenuItem cmdBienBanHoiChan;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayin;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label22;
        private UCs.AutoCompleteTextbox txtNoichuyenden;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton cmdPrint;
        private UCs.AutoCompleteTextbox txtBacsi;
        private Janus.Windows.EditControls.UIComboBox cboTrangthai;
        private System.Windows.Forms.LinkLabel lnkDelete;
        
    }
}