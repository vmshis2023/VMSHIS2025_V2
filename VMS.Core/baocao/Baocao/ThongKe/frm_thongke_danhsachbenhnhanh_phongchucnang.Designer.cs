namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    partial class frm_thongke_danhsachbenhnhanh_phongchucnang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thongke_danhsachbenhnhanh_phongchucnang));
            Janus.Windows.GridEX.GridEXLayout grdResult_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdInPhieu = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.radDathuchien = new System.Windows.Forms.RadioButton();
            this.radChuathuchien = new System.Windows.Forms.RadioButton();
            this.radTatca = new System.Windows.Forms.RadioButton();
            this.grdResult = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox5 = new Janus.Windows.EditControls.UIGroupBox();
            this.raddmucsau = new System.Windows.Forms.RadioButton();
            this.raddmuctruoc = new System.Windows.Forms.RadioButton();
            this.raddmucall = new System.Windows.Forms.RadioButton();
            this.txtdichvu = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).BeginInit();
            this.uiGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(339, 23);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 123;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
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
            this.dtNgayInPhieu.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(77, 27);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(141, 21);
            this.dtNgayInPhieu.TabIndex = 125;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(22, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 126;
            this.label3.Text = "Ngày in:";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(617, 23);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 124;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cmdExportToExcel);
            this.uiGroupBox2.Controls.Add(this.dtNgayInPhieu);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.cmdInPhieu);
            this.uiGroupBox2.Controls.Add(this.cmdExit);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 286);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1002, 65);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "Chức năng";
            // 
            // cmdInPhieu
            // 
            this.cmdInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieu.Image")));
            this.cmdInPhieu.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieu.Location = new System.Drawing.Point(478, 23);
            this.cmdInPhieu.Name = "cmdInPhieu";
            this.cmdInPhieu.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieu.TabIndex = 122;
            this.cmdInPhieu.Text = "In báo cáo";
            this.cmdInPhieu.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieu.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdInPhieu.Click += new System.EventHandler(this.cmdInPhieu_Click);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.radDathuchien);
            this.uiGroupBox4.Controls.Add(this.radChuathuchien);
            this.uiGroupBox4.Controls.Add(this.radTatca);
            this.uiGroupBox4.Location = new System.Drawing.Point(585, 44);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(355, 34);
            this.uiGroupBox4.TabIndex = 52;
            this.uiGroupBox4.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // radDathuchien
            // 
            this.radDathuchien.AutoSize = true;
            this.radDathuchien.Location = new System.Drawing.Point(248, 12);
            this.radDathuchien.Name = "radDathuchien";
            this.radDathuchien.Size = new System.Drawing.Size(86, 17);
            this.radDathuchien.TabIndex = 2;
            this.radDathuchien.Text = "Đã thực hiện";
            this.radDathuchien.UseVisualStyleBackColor = true;
            // 
            // radChuathuchien
            // 
            this.radChuathuchien.AutoSize = true;
            this.radChuathuchien.Location = new System.Drawing.Point(124, 12);
            this.radChuathuchien.Name = "radChuathuchien";
            this.radChuathuchien.Size = new System.Drawing.Size(97, 17);
            this.radChuathuchien.TabIndex = 1;
            this.radChuathuchien.Text = "Chưa thực hiện";
            this.radChuathuchien.UseVisualStyleBackColor = true;
            // 
            // radTatca
            // 
            this.radTatca.AutoSize = true;
            this.radTatca.Checked = true;
            this.radTatca.Location = new System.Drawing.Point(31, 12);
            this.radTatca.Name = "radTatca";
            this.radTatca.Size = new System.Drawing.Size(56, 17);
            this.radTatca.TabIndex = 0;
            this.radTatca.TabStop = true;
            this.radTatca.Text = "Tất cả";
            this.radTatca.UseVisualStyleBackColor = true;
            this.radTatca.CheckedChanged += new System.EventHandler(this.radTatca_CheckedChanged);
            // 
            // grdResult
            // 
            this.grdResult.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdResult.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdResult_DesignTimeLayout.LayoutString = resources.GetString("grdResult_DesignTimeLayout.LayoutString");
            this.grdResult.DesignTimeLayout = grdResult_DesignTimeLayout;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdResult.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdResult.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdResult.Font = new System.Drawing.Font("Arial", 9F);
            this.grdResult.GroupByBoxVisible = false;
            this.grdResult.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdResult.Location = new System.Drawing.Point(3, 16);
            this.grdResult.Name = "grdResult";
            this.grdResult.RecordNavigator = true;
            this.grdResult.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdResult.Size = new System.Drawing.Size(996, 267);
            this.grdResult.TabIndex = 22;
            this.grdResult.TabStop = false;
            this.grdResult.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdResult.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.grdResult);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(1002, 286);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Kết quả tìm kiếm";
            this.uiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "baocao_thongketheomabenh_icd10_chitiet";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1002, 47);
            this.baocaO_TIEUDE1.TabIndex = 123;
            this.baocaO_TIEUDE1.TIEUDE = "DANH SÁCH BỆNH NHÂN THỰC HIỆN SIÊU ÂM";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.DropDownCalendar.Visible = false;
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(364, 49);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 23);
            this.dtToDate.TabIndex = 51;
            this.dtToDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.DropDownCalendar.Visible = false;
            this.dtFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(156, 49);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(200, 23);
            this.dtFromDate.TabIndex = 50;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(81, 49);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 49;
            this.chkByDate.Text = "Từ ngày";
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(668, 20);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(273, 23);
            this.cboDoituongKCB.TabIndex = 46;
            this.cboDoituongKCB.Text = "Đối tượng";
            this.cboDoituongKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(156, 20);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(408, 23);
            this.cboKhoa.TabIndex = 45;
            this.cboKhoa.Text = "Khoa thực hiện";
            this.cboKhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(46, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Khoa KCB";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(558, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Đối tượng KCB:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiGroupBox5);
            this.uiGroupBox1.Controls.Add(this.txtdichvu);
            this.uiGroupBox1.Controls.Add(this.uiGroupBox4);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox1.Controls.Add(this.cboKhoa);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 47);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1002, 136);
            this.uiGroupBox1.TabIndex = 124;
            this.uiGroupBox1.Text = "Điều kiện tìm kiếm";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // uiGroupBox5
            // 
            this.uiGroupBox5.Controls.Add(this.raddmucsau);
            this.uiGroupBox5.Controls.Add(this.raddmuctruoc);
            this.uiGroupBox5.Controls.Add(this.raddmucall);
            this.uiGroupBox5.Location = new System.Drawing.Point(584, 78);
            this.uiGroupBox5.Name = "uiGroupBox5";
            this.uiGroupBox5.Size = new System.Drawing.Size(355, 34);
            this.uiGroupBox5.TabIndex = 53;
            this.uiGroupBox5.Visible = false;
            this.uiGroupBox5.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // raddmucsau
            // 
            this.raddmucsau.AutoSize = true;
            this.raddmucsau.Location = new System.Drawing.Point(248, 12);
            this.raddmucsau.Name = "raddmucsau";
            this.raddmucsau.Size = new System.Drawing.Size(105, 17);
            this.raddmucsau.TabIndex = 2;
            this.raddmucsau.Text = "Sau 01/03/2016";
            this.raddmucsau.UseVisualStyleBackColor = true;
            this.raddmucsau.CheckedChanged += new System.EventHandler(this.raddmucsau_CheckedChanged);
            // 
            // raddmuctruoc
            // 
            this.raddmuctruoc.AutoSize = true;
            this.raddmuctruoc.Location = new System.Drawing.Point(124, 12);
            this.raddmuctruoc.Name = "raddmuctruoc";
            this.raddmuctruoc.Size = new System.Drawing.Size(114, 17);
            this.raddmuctruoc.TabIndex = 1;
            this.raddmuctruoc.Text = "Trước 01/03/2016";
            this.raddmuctruoc.UseVisualStyleBackColor = true;
            this.raddmuctruoc.CheckedChanged += new System.EventHandler(this.raddmuctruoc_CheckedChanged);
            // 
            // raddmucall
            // 
            this.raddmucall.AutoSize = true;
            this.raddmucall.Checked = true;
            this.raddmucall.Location = new System.Drawing.Point(31, 12);
            this.raddmucall.Name = "raddmucall";
            this.raddmucall.Size = new System.Drawing.Size(56, 17);
            this.raddmucall.TabIndex = 0;
            this.raddmucall.TabStop = true;
            this.raddmucall.Text = "Tất cả";
            this.raddmucall.UseVisualStyleBackColor = true;
            this.raddmucall.CheckedChanged += new System.EventHandler(this.raddmucall_CheckedChanged);
            // 
            // txtdichvu
            // 
            this.txtdichvu._backcolor = System.Drawing.SystemColors.Control;
            this.txtdichvu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdichvu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtdichvu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtdichvu.AutoCompleteList")));
            this.txtdichvu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdichvu.CaseSensitive = false;
            this.txtdichvu.CompareNoID = true;
            this.txtdichvu.DefaultCode = "-1";
            this.txtdichvu.DefaultID = "-1";
            this.txtdichvu.Drug_ID = null;
            this.txtdichvu.ExtraWidth = 0;
            this.txtdichvu.FillValueAfterSelect = false;
            this.txtdichvu.Location = new System.Drawing.Point(156, 86);
            this.txtdichvu.MaxHeight = 150;
            this.txtdichvu.MinTypedCharacters = 2;
            this.txtdichvu.MyCode = "-1";
            this.txtdichvu.MyID = "-1";
            this.txtdichvu.MyText = "";
            this.txtdichvu.Name = "txtdichvu";
            this.txtdichvu.RaiseEvent = false;
            this.txtdichvu.RaiseEventEnter = false;
            this.txtdichvu.RaiseEventEnterWhenEmpty = false;
            this.txtdichvu.SelectedIndex = -1;
            this.txtdichvu.Size = new System.Drawing.Size(408, 20);
            this.txtdichvu.splitChar = '@';
            this.txtdichvu.splitCharIDAndCode = '#';
            this.txtdichvu.TabIndex = 0;
            this.txtdichvu.TakeCode = false;
            this.txtdichvu.txtMyCode = null;
            this.txtdichvu.txtMyCode_Edit = null;
            this.txtdichvu.txtMyID = null;
            this.txtdichvu.txtMyID_Edit = null;
            this.txtdichvu.txtMyName = null;
            this.txtdichvu.txtMyName_Edit = null;
            this.txtdichvu.txtNext = null;
            this.txtdichvu.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.baocaO_TIEUDE1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 538);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 538);
            this.panel1.TabIndex = 3;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // frm_thongke_danhsachbenhnhanh_phongchucnang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 538);
            this.Controls.Add(this.panel1);
            this.Name = "frm_thongke_danhsachbenhnhanh_phongchucnang";
            this.Text = "DANH SÁCH BỆNH NHÂN THỰC HIỆN SIÊU ÂM";
            this.Load += new System.EventHandler(this.frm_thongke_danhsachbenhnhanh_sieuam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).EndInit();
            this.uiGroupBox5.ResumeLayout(false);
            this.uiGroupBox5.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIButton cmdInPhieu;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private System.Windows.Forms.RadioButton radDathuchien;
        private System.Windows.Forms.RadioButton radChuathuchien;
        private System.Windows.Forms.RadioButton radTatca;
        private Janus.Windows.GridEX.GridEX grdResult;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox txtdichvu;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox5;
        private System.Windows.Forms.RadioButton raddmucsau;
        private System.Windows.Forms.RadioButton raddmuctruoc;
        private System.Windows.Forms.RadioButton raddmucall;
    }
}