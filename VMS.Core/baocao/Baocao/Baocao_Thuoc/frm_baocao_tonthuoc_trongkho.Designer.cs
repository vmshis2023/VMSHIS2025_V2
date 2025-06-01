namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_baocao_tonthuoc_trongkho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocao_tonthuoc_trongkho));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout cboKhoThuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkInbienbankiemke = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLoaithuoc = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.nmrSongay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDrugID = new System.Windows.Forms.TextBox();
            this.txtthuoc = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc();
            this.label11 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.radDaHetHan = new System.Windows.Forms.RadioButton();
            this.radChuaHetHan = new System.Windows.Forms.RadioButton();
            this.radTatCa = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.cboKhoThuoc = new Janus.Windows.GridEX.EditControls.CheckedComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSongay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkInbienbankiemke);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtLoaithuoc);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nmrSongay);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDrugID);
            this.groupBox2.Controls.Add(this.txtthuoc);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.grdList);
            this.groupBox2.Controls.Add(this.radDaHetHan);
            this.groupBox2.Controls.Add(this.radChuaHetHan);
            this.groupBox2.Controls.Add(this.radTatCa);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cboKhoThuoc);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox2.Location = new System.Drawing.Point(0, 73);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(781, 430);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin tìm kiếm";
            // 
            // chkInbienbankiemke
            // 
            this.chkInbienbankiemke.AutoSize = true;
            this.chkInbienbankiemke.Location = new System.Drawing.Point(128, 123);
            this.chkInbienbankiemke.Name = "chkInbienbankiemke";
            this.chkInbienbankiemke.Size = new System.Drawing.Size(264, 19);
            this.chkInbienbankiemke.TabIndex = 7;
            this.chkInbienbankiemke.Text = "In biên bản kiểm kê thuốc (MS: 11D/BV-01)?";
            this.chkInbienbankiemke.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 18);
            this.label4.TabIndex = 119;
            this.label4.Text = "Nhóm thuốc";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLoaithuoc
            // 
            this.txtLoaithuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLoaithuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaithuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoaithuoc.AutoCompleteList")));
            this.txtLoaithuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaithuoc.CaseSensitive = false;
            this.txtLoaithuoc.CompareNoID = true;
            this.txtLoaithuoc.DefaultCode = "-1";
            this.txtLoaithuoc.DefaultID = "-1";
            this.txtLoaithuoc.Drug_ID = null;
            this.txtLoaithuoc.ExtraWidth = 100;
            this.txtLoaithuoc.FillValueAfterSelect = false;
            this.txtLoaithuoc.Location = new System.Drawing.Point(128, 41);
            this.txtLoaithuoc.MaxHeight = 300;
            this.txtLoaithuoc.MinTypedCharacters = 2;
            this.txtLoaithuoc.MyCode = "-1";
            this.txtLoaithuoc.MyID = "-1";
            this.txtLoaithuoc.Name = "txtLoaithuoc";
            this.txtLoaithuoc.RaiseEvent = true;
            this.txtLoaithuoc.RaiseEventEnter = true;
            this.txtLoaithuoc.RaiseEventEnterWhenEmpty = true;
            this.txtLoaithuoc.SelectedIndex = -1;
            this.txtLoaithuoc.Size = new System.Drawing.Size(589, 21);
            this.txtLoaithuoc.splitChar = '@';
            this.txtLoaithuoc.splitCharIDAndCode = '#';
            this.txtLoaithuoc.TabIndex = 1;
            this.txtLoaithuoc.TakeCode = false;
            this.txtLoaithuoc.txtMyCode = null;
            this.txtLoaithuoc.txtMyCode_Edit = null;
            this.txtLoaithuoc.txtMyID = null;
            this.txtLoaithuoc.txtMyID_Edit = null;
            this.txtLoaithuoc.txtMyName = null;
            this.txtLoaithuoc.txtMyName_Edit = null;
            this.txtLoaithuoc.txtNext = null;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(722, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 18);
            this.label2.TabIndex = 117;
            this.label2.Text = "(ngày)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmrSongay
            // 
            this.nmrSongay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nmrSongay.Location = new System.Drawing.Point(649, 95);
            this.nmrSongay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrSongay.Name = "nmrSongay";
            this.nmrSongay.Size = new System.Drawing.Size(67, 21);
            this.nmrSongay.TabIndex = 6;
            this.nmrSongay.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(491, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 18);
            this.label1.TabIndex = 115;
            this.label1.Text = "Cảnh báo hết hạn trước:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDrugID
            // 
            this.txtDrugID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrugID.Location = new System.Drawing.Point(723, 45);
            this.txtDrugID.Name = "txtDrugID";
            this.txtDrugID.Size = new System.Drawing.Size(21, 21);
            this.txtDrugID.TabIndex = 114;
            this.txtDrugID.Text = "-1";
            this.txtDrugID.Visible = false;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtthuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthuoc.AutoCompleteList")));
            this.txtthuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthuoc.CaseSensitive = false;
            this.txtthuoc.CompareNoID = true;
            this.txtthuoc.DefaultCode = "-1";
            this.txtthuoc.DefaultID = "-1";
            this.txtthuoc.Drug_ID = null;
            this.txtthuoc.ExtraWidth = 0;
            this.txtthuoc.FillValueAfterSelect = true;
            this.txtthuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.txtthuoc.GridView = false;
            this.txtthuoc.Location = new System.Drawing.Point(128, 68);
            this.txtthuoc.MaxHeight = 300;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = false;
            this.txtthuoc.RaiseEventEnter = false;
            this.txtthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(589, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 2;
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = this.txtDrugID;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(4, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 18);
            this.label11.TabIndex = 48;
            this.label11.Text = "Tên thuốc:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.GroupTotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.grdList.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(7, 157);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(771, 268);
            this.grdList.TabIndex = 11;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // radDaHetHan
            // 
            this.radDaHetHan.AutoSize = true;
            this.radDaHetHan.Font = new System.Drawing.Font("Arial", 9F);
            this.radDaHetHan.Location = new System.Drawing.Point(298, 98);
            this.radDaHetHan.Name = "radDaHetHan";
            this.radDaHetHan.Size = new System.Drawing.Size(85, 19);
            this.radDaHetHan.TabIndex = 5;
            this.radDaHetHan.TabStop = true;
            this.radDaHetHan.Text = "Đã hết hạn";
            this.radDaHetHan.UseVisualStyleBackColor = true;
            // 
            // radChuaHetHan
            // 
            this.radChuaHetHan.AutoSize = true;
            this.radChuaHetHan.Font = new System.Drawing.Font("Arial", 9F);
            this.radChuaHetHan.Location = new System.Drawing.Point(192, 98);
            this.radChuaHetHan.Name = "radChuaHetHan";
            this.radChuaHetHan.Size = new System.Drawing.Size(100, 19);
            this.radChuaHetHan.TabIndex = 4;
            this.radChuaHetHan.TabStop = true;
            this.radChuaHetHan.Text = "Chưa hết hạn";
            this.radChuaHetHan.UseVisualStyleBackColor = true;
            // 
            // radTatCa
            // 
            this.radTatCa.AutoSize = true;
            this.radTatCa.Checked = true;
            this.radTatCa.Font = new System.Drawing.Font("Arial", 9F);
            this.radTatCa.Location = new System.Drawing.Point(128, 98);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(58, 19);
            this.radTatCa.TabIndex = 3;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            this.radTatCa.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(5, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Kho thuốc";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoThuoc
            // 
            this.cboKhoThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            cboKhoThuoc_DesignTimeLayout.LayoutString = resources.GetString("cboKhoThuoc_DesignTimeLayout.LayoutString");
            this.cboKhoThuoc.DesignTimeLayout = cboKhoThuoc_DesignTimeLayout;
            this.cboKhoThuoc.DropDownDisplayMember = "TEN_KHO";
            this.cboKhoThuoc.DropDownValueMember = "ID_KHO";
            this.cboKhoThuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.cboKhoThuoc.Location = new System.Drawing.Point(128, 16);
            this.cboKhoThuoc.Name = "cboKhoThuoc";
            this.cboKhoThuoc.SaveSettings = false;
            this.cboKhoThuoc.Size = new System.Drawing.Size(588, 21);
            this.cboKhoThuoc.TabIndex = 0;
            this.cboKhoThuoc.ValueItemDataMember = "(None)";
            this.cboKhoThuoc.ValuesDataMember = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 70);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.baocaO_TIEUDE1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 50);
            this.panel1.TabIndex = 88;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BC_SOLUONGTONTRONGKHO";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(778, 50);
            this.baocaO_TIEUDE1.TabIndex = 0;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO SỐ LƯỢNG TỒN TRONG KHO";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(361, 520);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 9;
            this.cmdExportToExcel.Text = "Xuất Excel (F5)";
            this.toolTip1.SetToolTip(this.cmdExportToExcel, "Nhấn vào đây để export dữ liệu ra file exel(Ctrl+E)");
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
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(76, 526);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(164, 21);
            this.dtNgayInPhieu.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(12, 531);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 122;
            this.label3.Text = "Ngày in:";
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.Location = new System.Drawing.Point(500, 520);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieuXN.TabIndex = 8;
            this.cmdInPhieuXN.Text = "In báo cáo (F4)";
            this.toolTip1.SetToolTip(this.cmdInPhieuXN, "Nhấn vào đây để thực hiện lấy dữ liệu và in báo cáo(Ctrl+P)");
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(639, 520);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Thoát (Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để thoát khỏi chức năng(Esc)");
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdList;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp:";
            // 
            // frm_baocao_tonthuoc_trongkho
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocao_tonthuoc_trongkho";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO SỐ LƯỢNG TỒN TRONG KHO";
            this.Load += new System.EventHandler(this.frm_baocao_tonthuoc_trongkho_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocao_tonthuoc_trongkho_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSongay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radDaHetHan;
        private System.Windows.Forms.RadioButton radChuaHetHan;
        private System.Windows.Forms.RadioButton radTatCa;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.CheckedComboBox cboKhoThuoc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.TextBox txtDrugID;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmrSongay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox txtLoaithuoc;
        private System.Windows.Forms.CheckBox chkInbienbankiemke;
    }
}