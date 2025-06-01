namespace VNS.HIS.UI.Baocao
{
    partial class frm_thongkenhapvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thongkenhapvien));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdTonghop_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboKhoaNoiTru = new Janus.Windows.EditControls.UIComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radSoluong = new System.Windows.Forms.RadioButton();
            this.radChitiet = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.grdTonghop = new Janus.Windows.GridEX.GridEX();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbokhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTonghop)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(389, 13);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(119, 30);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayInPhieu.DropDownCalendar.Name = "";
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(88, 19);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 12;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 88;
            this.label3.Text = "Ngày in";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(514, 13);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(119, 30);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "In báo cáo";
            this.cmdPrint.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(639, 13);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 30);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "&Thoát(Esc)";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cboKhoaNoiTru);
            this.uiGroupBox2.Controls.Add(this.groupBox1);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.panel1);
            this.uiGroupBox2.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.cbokhoa);
            this.uiGroupBox2.Controls.Add(this.label11);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Location = new System.Drawing.Point(3, 56);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(824, 488);
            this.uiGroupBox2.TabIndex = 115;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // cboKhoaNoiTru
            // 
            this.cboKhoaNoiTru.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = -1;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Ngoại trú";
            uiComboBoxItem2.Value = 0;
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Nội trú";
            uiComboBoxItem3.Value = 1;
            this.cboKhoaNoiTru.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboKhoaNoiTru.Location = new System.Drawing.Point(138, 75);
            this.cboKhoaNoiTru.Name = "cboKhoaNoiTru";
            this.cboKhoaNoiTru.Size = new System.Drawing.Size(279, 21);
            this.cboKhoaNoiTru.TabIndex = 273;
            this.cboKhoaNoiTru.Text = "Chọn khoa nội trú";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radSoluong);
            this.groupBox1.Controls.Add(this.radChitiet);
            this.groupBox1.Location = new System.Drawing.Point(451, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 54);
            this.groupBox1.TabIndex = 272;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn điều kiện";
            // 
            // radSoluong
            // 
            this.radSoluong.AutoSize = true;
            this.radSoluong.Location = new System.Drawing.Point(188, 22);
            this.radSoluong.Name = "radSoluong";
            this.radSoluong.Size = new System.Drawing.Size(76, 19);
            this.radSoluong.TabIndex = 1;
            this.radSoluong.Text = "Số lượng";
            this.radSoluong.UseVisualStyleBackColor = true;
            this.radSoluong.CheckedChanged += new System.EventHandler(this.radChuyenDi_CheckedChanged);
            // 
            // radChitiet
            // 
            this.radChitiet.AutoSize = true;
            this.radChitiet.Checked = true;
            this.radChitiet.Location = new System.Drawing.Point(29, 22);
            this.radChitiet.Name = "radChitiet";
            this.radChitiet.Size = new System.Drawing.Size(67, 19);
            this.radChitiet.TabIndex = 0;
            this.radChitiet.TabStop = true;
            this.radChitiet.Text = "Chi Tiết";
            this.radChitiet.UseVisualStyleBackColor = true;
            this.radChitiet.CheckedChanged += new System.EventHandler(this.radChuyenDen_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 270;
            this.label4.Text = "Khoa nhập viện";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "đến";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Controls.Add(this.grdTonghop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 335);
            this.panel1.TabIndex = 56;
            // 
            // grdChitiet
            // 
            this.grdChitiet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdChitiet.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.ScrollBars = Janus.Windows.GridEX.ScrollBars.Horizontal;
            this.grdChitiet.Size = new System.Drawing.Size(818, 335);
            this.grdChitiet.TabIndex = 23;
            this.grdChitiet.TabStop = false;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdTonghop
            // 
            this.grdTonghop.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdTonghop_DesignTimeLayout.LayoutString = resources.GetString("grdTonghop_DesignTimeLayout.LayoutString");
            this.grdTonghop.DesignTimeLayout = grdTonghop_DesignTimeLayout;
            this.grdTonghop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTonghop.Font = new System.Drawing.Font("Arial", 9F);
            this.grdTonghop.GroupByBoxVisible = false;
            this.grdTonghop.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdTonghop.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdTonghop.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdTonghop.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdTonghop.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdTonghop.Location = new System.Drawing.Point(0, 0);
            this.grdTonghop.Name = "grdTonghop";
            this.grdTonghop.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTonghop.Size = new System.Drawing.Size(818, 335);
            this.grdTonghop.TabIndex = 22;
            this.grdTonghop.TabStop = false;
            this.grdTonghop.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTonghop.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdTonghop.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdTonghop.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(138, 49);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(279, 21);
            this.cboDoituongKCB.TabIndex = 2;
            this.cboDoituongKCB.Text = "Chọn loại đối tượng KCB";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 15);
            this.label10.TabIndex = 54;
            this.label10.Text = "Đối tượng KCB:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbokhoa
            // 
            this.cbokhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbokhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cbokhoa.Location = new System.Drawing.Point(138, 22);
            this.cbokhoa.Name = "cbokhoa";
            this.cbokhoa.SelectInDataSource = true;
            this.cbokhoa.Size = new System.Drawing.Size(626, 21);
            this.cbokhoa.TabIndex = 0;
            this.cbokhoa.Text = "Chọn khoa KCB";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 15);
            this.label11.TabIndex = 52;
            this.label11.Text = "Khoa KCB:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(304, 106);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(113, 21);
            this.dtToDate.TabIndex = 7;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(138, 106);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(126, 21);
            this.dtFromDate.TabIndex = 6;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.ForeColor = System.Drawing.Color.Navy;
            this.chkByDate.Location = new System.Drawing.Point(67, 106);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(65, 23);
            this.chkByDate.TabIndex = 5;
            this.chkByDate.Text = "Từ ngày";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(838, 573);
            this.tabControl1.TabIndex = 116;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.uiGroupBox2);
            this.tabPage1.Controls.Add(this.baocaO_TIEUDE1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(830, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông kê nhập viện";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdExportToExcel);
            this.panel2.Controls.Add(this.dtNgayInPhieu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 492);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 52);
            this.panel2.TabIndex = 115;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(3, 3);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BCDSACH_BNHANLINHTHUOC";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(824, 53);
            this.baocaO_TIEUDE1.TabIndex = 114;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO  CÁO THỐNG KÊ NHẬP VIỆN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frm_thongkenhapvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 573);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thongkenhapvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhập viện";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTonghop)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.EditControls.UIComboBox cbokhoa;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.GridEX grdTonghop;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radSoluong;
        private System.Windows.Forms.RadioButton radChitiet;
        private Janus.Windows.EditControls.UIComboBox cboKhoaNoiTru;
    }
}