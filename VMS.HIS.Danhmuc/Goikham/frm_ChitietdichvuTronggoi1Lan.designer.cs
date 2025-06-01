namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_ChitietdichvuTronggoi1Lan
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
            Janus.Windows.GridEX.GridEXLayout grdChiTietGoiKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChitietdichvuTronggoi1Lan));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.uiGroupBox5 = new Janus.Windows.EditControls.UIGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTongChiPhi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChiTietGoiKham = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBochonDvuDachidinh = new System.Windows.Forms.ToolStripMenuItem();
            this.chkKieuKhuyenmai = new System.Windows.Forms.CheckBox();
            this.cboKieuCK = new Janus.Windows.EditControls.UIComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTienGoi = new MaskedTextBox.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpHieuLucDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpHieuLucTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblTileCK = new System.Windows.Forms.Label();
            this.chkHieuLuc = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTenGoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaGoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).BeginInit();
            this.uiGroupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTietGoiKham)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox5
            // 
            this.uiGroupBox5.Controls.Add(this.label6);
            this.uiGroupBox5.Controls.Add(this.label5);
            this.uiGroupBox5.Controls.Add(this.txtTongChiPhi);
            this.uiGroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox5.Location = new System.Drawing.Point(0, 587);
            this.uiGroupBox5.Name = "uiGroupBox5";
            this.uiGroupBox5.Size = new System.Drawing.Size(485, 0);
            this.uiGroupBox5.TabIndex = 3;
            this.uiGroupBox5.Text = "&Thông tin chi phí";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(258, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 15);
            this.label6.TabIndex = 242;
            this.label6.Text = "Vnđ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 241;
            this.label5.Text = "Chi phí";
            // 
            // txtTongChiPhi
            // 
            this.txtTongChiPhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongChiPhi.Location = new System.Drawing.Point(68, 37);
            this.txtTongChiPhi.Name = "txtTongChiPhi";
            this.txtTongChiPhi.ReadOnly = true;
            this.txtTongChiPhi.Size = new System.Drawing.Size(183, 23);
            this.txtTongChiPhi.TabIndex = 0;
            this.txtTongChiPhi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdChiTietGoiKham);
            this.panel1.Controls.Add(this.chkKieuKhuyenmai);
            this.panel1.Controls.Add(this.cboKieuCK);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtTienGoi);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.dtpHieuLucDenNgay);
            this.panel1.Controls.Add(this.dtpHieuLucTuNgay);
            this.panel1.Controls.Add(this.lblTileCK);
            this.panel1.Controls.Add(this.chkHieuLuc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.txtTenGoi);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMaGoi);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 741);
            this.panel1.TabIndex = 547;
            // 
            // grdChiTietGoiKham
            // 
            this.grdChiTietGoiKham.AlternatingColors = true;
            this.grdChiTietGoiKham.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdChiTietGoiKham.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin dùng chung</FilterRowInfoText></LocalizableData>";
            this.grdChiTietGoiKham.ColumnAutoResize = true;
            this.grdChiTietGoiKham.ContextMenuStrip = this.contextMenuStrip1;
            this.grdChiTietGoiKham.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdChiTietGoiKham_DesignTimeLayout.LayoutString = resources.GetString("grdChiTietGoiKham_DesignTimeLayout.LayoutString");
            this.grdChiTietGoiKham.DesignTimeLayout = grdChiTietGoiKham_DesignTimeLayout;
            this.grdChiTietGoiKham.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChiTietGoiKham.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdChiTietGoiKham.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChiTietGoiKham.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChiTietGoiKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdChiTietGoiKham.GroupByBoxVisible = false;
            this.grdChiTietGoiKham.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdChiTietGoiKham.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdChiTietGoiKham.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdChiTietGoiKham.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChiTietGoiKham.Location = new System.Drawing.Point(8, 61);
            this.grdChiTietGoiKham.Name = "grdChiTietGoiKham";
            this.grdChiTietGoiKham.RecordNavigator = true;
            this.grdChiTietGoiKham.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiTietGoiKham.Size = new System.Drawing.Size(997, 637);
            this.grdChiTietGoiKham.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckAll,
            this.mnuUncheckAll,
            this.toolStripMenuItem2,
            this.mnuBochonDvuDachidinh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(252, 76);
            // 
            // mnuCheckAll
            // 
            this.mnuCheckAll.Name = "mnuCheckAll";
            this.mnuCheckAll.Size = new System.Drawing.Size(251, 22);
            this.mnuCheckAll.Text = "Chọn tất cả";
            this.mnuCheckAll.Click += new System.EventHandler(this.mnuCheckAll_Click);
            // 
            // mnuUncheckAll
            // 
            this.mnuUncheckAll.Name = "mnuUncheckAll";
            this.mnuUncheckAll.Size = new System.Drawing.Size(251, 22);
            this.mnuUncheckAll.Text = "Hủy chọn tất cả";
            this.mnuUncheckAll.Click += new System.EventHandler(this.mnuUncheckAll_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(248, 6);
            // 
            // mnuBochonDvuDachidinh
            // 
            this.mnuBochonDvuDachidinh.Name = "mnuBochonDvuDachidinh";
            this.mnuBochonDvuDachidinh.Size = new System.Drawing.Size(251, 22);
            this.mnuBochonDvuDachidinh.Text = "Hủy chọn các dịch vụ đã chỉ định";
            this.mnuBochonDvuDachidinh.Click += new System.EventHandler(this.mnuBochonDvuDachidinh_Click);
            // 
            // chkKieuKhuyenmai
            // 
            this.chkKieuKhuyenmai.AutoSize = true;
            this.chkKieuKhuyenmai.Checked = true;
            this.chkKieuKhuyenmai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKieuKhuyenmai.Enabled = false;
            this.chkKieuKhuyenmai.Location = new System.Drawing.Point(674, 122);
            this.chkKieuKhuyenmai.Name = "chkKieuKhuyenmai";
            this.chkKieuKhuyenmai.Size = new System.Drawing.Size(194, 19);
            this.chkKieuKhuyenmai.TabIndex = 649;
            this.chkKieuKhuyenmai.TabStop = false;
            this.chkKieuKhuyenmai.Text = "Khuyến mãi cho tất cả dịch vụ?";
            this.chkKieuKhuyenmai.UseVisualStyleBackColor = true;
            this.chkKieuKhuyenmai.Visible = false;
            // 
            // cboKieuCK
            // 
            this.cboKieuCK.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKieuCK.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tiền";
            uiComboBoxItem1.Value = "T";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Phần trăm (%)";
            uiComboBoxItem2.Value = "%";
            this.cboKieuCK.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboKieuCK.Location = new System.Drawing.Point(938, 120);
            this.cboKieuCK.Name = "cboKieuCK";
            this.cboKieuCK.ReadOnly = true;
            this.cboKieuCK.Size = new System.Drawing.Size(10, 21);
            this.cboKieuCK.TabIndex = 648;
            this.cboKieuCK.TabStop = false;
            this.cboKieuCK.Visible = false;
            this.cboKieuCK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(874, 121);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 17);
            this.label15.TabIndex = 647;
            this.label15.Text = "Kiểu CK:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label15.Visible = false;
            // 
            // txtTienGoi
            // 
            this.txtTienGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTienGoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTienGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienGoi.Location = new System.Drawing.Point(584, 33);
            this.txtTienGoi.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTienGoi.Name = "txtTienGoi";
            this.txtTienGoi.ReadOnly = true;
            this.txtTienGoi.Size = new System.Drawing.Size(141, 22);
            this.txtTienGoi.TabIndex = 641;
            this.txtTienGoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(237, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 17);
            this.label11.TabIndex = 646;
            this.label11.Text = "đến ngày";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpHieuLucDenNgay
            // 
            // 
            // 
            // 
            this.dtpHieuLucDenNgay.DropDownCalendar.Name = "";
            this.dtpHieuLucDenNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHieuLucDenNgay.Location = new System.Drawing.Point(308, 30);
            this.dtpHieuLucDenNgay.Name = "dtpHieuLucDenNgay";
            this.dtpHieuLucDenNgay.ReadOnly = true;
            this.dtpHieuLucDenNgay.Size = new System.Drawing.Size(152, 22);
            this.dtpHieuLucDenNgay.TabIndex = 639;
            this.dtpHieuLucDenNgay.Value = new System.DateTime(2018, 3, 8, 0, 0, 0, 0);
            // 
            // dtpHieuLucTuNgay
            // 
            // 
            // 
            // 
            this.dtpHieuLucTuNgay.DropDownCalendar.Name = "";
            this.dtpHieuLucTuNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHieuLucTuNgay.Location = new System.Drawing.Point(98, 30);
            this.dtpHieuLucTuNgay.Name = "dtpHieuLucTuNgay";
            this.dtpHieuLucTuNgay.ReadOnly = true;
            this.dtpHieuLucTuNgay.Size = new System.Drawing.Size(133, 22);
            this.dtpHieuLucTuNgay.TabIndex = 638;
            this.dtpHieuLucTuNgay.Value = new System.DateTime(2018, 3, 8, 0, 0, 0, 0);
            // 
            // lblTileCK
            // 
            this.lblTileCK.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileCK.Location = new System.Drawing.Point(500, 33);
            this.lblTileCK.Name = "lblTileCK";
            this.lblTileCK.Size = new System.Drawing.Size(78, 21);
            this.lblTileCK.TabIndex = 645;
            this.lblTileCK.Text = "Tiền gói";
            this.lblTileCK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkHieuLuc
            // 
            this.chkHieuLuc.AutoSize = true;
            this.chkHieuLuc.Checked = true;
            this.chkHieuLuc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHieuLuc.Enabled = false;
            this.chkHieuLuc.Location = new System.Drawing.Point(3, 32);
            this.chkHieuLuc.Name = "chkHieuLuc";
            this.chkHieuLuc.Size = new System.Drawing.Size(89, 19);
            this.chkHieuLuc.TabIndex = 640;
            this.chkHieuLuc.TabStop = false;
            this.chkHieuLuc.Text = "Hiệu lực từ:";
            this.chkHieuLuc.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 644;
            this.label2.Text = "ID gói";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(98, 7);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(133, 22);
            this.txtID.TabIndex = 635;
            this.txtID.TabStop = false;
            // 
            // txtTenGoi
            // 
            this.txtTenGoi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTenGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenGoi.Location = new System.Drawing.Point(584, 4);
            this.txtTenGoi.Name = "txtTenGoi";
            this.txtTenGoi.ReadOnly = true;
            this.txtTenGoi.Size = new System.Drawing.Size(412, 22);
            this.txtTenGoi.TabIndex = 637;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(466, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 643;
            this.label3.Text = "Tên  gói:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(240, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 642;
            this.label1.Text = "Mã gói:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaGoi
            // 
            this.txtMaGoi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaGoi.Location = new System.Drawing.Point(308, 6);
            this.txtMaGoi.Name = "txtMaGoi";
            this.txtMaGoi.ReadOnly = true;
            this.txtMaGoi.Size = new System.Drawing.Size(152, 22);
            this.txtMaGoi.TabIndex = 636;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkCheckAll);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 698);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 43);
            this.panel2.TabIndex = 9;
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(15, 12);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(139, 19);
            this.chkCheckAll.TabIndex = 643;
            this.chkCheckAll.TabStop = false;
            this.chkCheckAll.Text = "Chọn tất/ Bỏ chọn tất";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            this.chkCheckAll.CheckedChanged += new System.EventHandler(this.chkCheckAll_CheckedChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(779, 5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 30);
            this.cmdSave.TabIndex = 19;
            this.cmdSave.Text = "Chấp nhận";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(895, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(110, 30);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Thoát";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // frm_ChitietdichvuTronggoi1Lan
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1008, 741);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChitietdichvuTronggoi1Lan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết các dịch vụ trong gói.";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).EndInit();
            this.uiGroupBox5.ResumeLayout(false);
            this.uiGroupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTietGoiKham)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox5;

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongChiPhi;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.GridEX.GridEX grdChiTietGoiKham;
        private Janus.Windows.EditControls.UIComboBox cboKieuCK;
        private System.Windows.Forms.Label label15;
        private MaskedTextBox.MaskedTextBox txtTienGoi;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpHieuLucDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpHieuLucTuNgay;
        private System.Windows.Forms.Label lblTileCK;
        private System.Windows.Forms.CheckBox chkHieuLuc;
        private System.Windows.Forms.Label label2;
        internal Janus.Windows.GridEX.EditControls.EditBox txtID;
        internal Janus.Windows.GridEX.EditControls.EditBox txtTenGoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMaGoi;
        private System.Windows.Forms.CheckBox chkKieuKhuyenmai;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckAll;
        private System.Windows.Forms.ToolStripMenuItem mnuUncheckAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuBochonDvuDachidinh;
    }
}