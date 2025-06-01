namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Chuyengiuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Chuyengiuong));
            Janus.Windows.GridEX.GridEXLayout cboGia_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdBuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.txtPatientDept_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.grpThongTinChuyenKhoa = new Janus.Windows.EditControls.UIGroupBox();
            this.nmrSoluongghep = new System.Windows.Forms.NumericUpDown();
            this.nmrSoluong = new System.Windows.Forms.NumericUpDown();
            this.chkGhepgiuong = new System.Windows.Forms.CheckBox();
            this.lblsoluongghep = new System.Windows.Forms.Label();
            this.txtGia = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.txtBedCode = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtRoom_code = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cboGia = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.lblGiaBG = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtHour2Cal = new MaskedTextBox.MaskedTextBox();
            this.txtTotalHour = new MaskedTextBox.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkAutoCal = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtPhutvao = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtGiovao = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.label20 = new System.Windows.Forms.Label();
            this.dtNgayvao = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdBuong = new Janus.Windows.GridEX.GridEX();
            this.grdGiuong = new Janus.Windows.GridEX.GridEX();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPhut = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGio = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtNgayChuyen = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkKhongtinh = new System.Windows.Forms.CheckBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdHelp = new Janus.Windows.EditControls.UIButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpThongTinBN = new Janus.Windows.EditControls.UIGroupBox();
            this.ucThongtinnguoibenh1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinChuyenKhoa)).BeginInit();
            this.grpThongTinChuyenKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoluongghep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoluong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGia)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).BeginInit();
            this.grpThongTinBN.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPatientDept_ID
            // 
            this.txtPatientDept_ID.Enabled = false;
            this.txtPatientDept_ID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientDept_ID.Location = new System.Drawing.Point(755, 17);
            this.txtPatientDept_ID.Name = "txtPatientDept_ID";
            this.txtPatientDept_ID.Size = new System.Drawing.Size(48, 22);
            this.txtPatientDept_ID.TabIndex = 12;
            // 
            // grpThongTinChuyenKhoa
            // 
            this.grpThongTinChuyenKhoa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTinChuyenKhoa.Controls.Add(this.nmrSoluongghep);
            this.grpThongTinChuyenKhoa.Controls.Add(this.nmrSoluong);
            this.grpThongTinChuyenKhoa.Controls.Add(this.chkGhepgiuong);
            this.grpThongTinChuyenKhoa.Controls.Add(this.lblsoluongghep);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtGia);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtBedCode);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtRoom_code);
            this.grpThongTinChuyenKhoa.Controls.Add(this.cboGia);
            this.grpThongTinChuyenKhoa.Controls.Add(this.lblGiaBG);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label13);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label12);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label26);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label25);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label24);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtHour2Cal);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtTotalHour);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label14);
            this.grpThongTinChuyenKhoa.Controls.Add(this.chkAutoCal);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label22);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label23);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtPhutvao);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label19);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtGiovao);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label20);
            this.grpThongTinChuyenKhoa.Controls.Add(this.dtNgayvao);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label21);
            this.grpThongTinChuyenKhoa.Controls.Add(this.panel1);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label11);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtPhut);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label10);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtGio);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label9);
            this.grpThongTinChuyenKhoa.Controls.Add(this.label8);
            this.grpThongTinChuyenKhoa.Controls.Add(this.dtNgayChuyen);
            this.grpThongTinChuyenKhoa.Controls.Add(this.txtPatientDept_ID);
            this.grpThongTinChuyenKhoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinChuyenKhoa.Location = new System.Drawing.Point(6, 128);
            this.grpThongTinChuyenKhoa.Name = "grpThongTinChuyenKhoa";
            this.grpThongTinChuyenKhoa.Size = new System.Drawing.Size(1272, 568);
            this.grpThongTinChuyenKhoa.TabIndex = 1;
            this.grpThongTinChuyenKhoa.Text = "Thông tin chuyển giường";
            // 
            // nmrSoluongghep
            // 
            this.nmrSoluongghep.DecimalPlaces = 1;
            this.nmrSoluongghep.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrSoluongghep.Location = new System.Drawing.Point(792, 67);
            this.nmrSoluongghep.Name = "nmrSoluongghep";
            this.nmrSoluongghep.Size = new System.Drawing.Size(76, 21);
            this.nmrSoluongghep.TabIndex = 7;
            this.nmrSoluongghep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmrSoluong
            // 
            this.nmrSoluong.DecimalPlaces = 1;
            this.nmrSoluong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrSoluong.Location = new System.Drawing.Point(126, 73);
            this.nmrSoluong.Name = "nmrSoluong";
            this.nmrSoluong.Size = new System.Drawing.Size(185, 21);
            this.nmrSoluong.TabIndex = 6;
            this.nmrSoluong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkGhepgiuong
            // 
            this.chkGhepgiuong.AutoSize = true;
            this.chkGhepgiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhepgiuong.Location = new System.Drawing.Point(632, 71);
            this.chkGhepgiuong.Name = "chkGhepgiuong";
            this.chkGhepgiuong.Size = new System.Drawing.Size(104, 20);
            this.chkGhepgiuong.TabIndex = 516;
            this.chkGhepgiuong.Text = "Ghép giường";
            this.chkGhepgiuong.UseVisualStyleBackColor = true;
            this.chkGhepgiuong.CheckedChanged += new System.EventHandler(this.chkGhepgiuong_CheckedChanged);
            // 
            // lblsoluongghep
            // 
            this.lblsoluongghep.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsoluongghep.ForeColor = System.Drawing.Color.Black;
            this.lblsoluongghep.Location = new System.Drawing.Point(754, 70);
            this.lblsoluongghep.Name = "lblsoluongghep";
            this.lblsoluongghep.Size = new System.Drawing.Size(32, 21);
            this.lblsoluongghep.TabIndex = 518;
            this.lblsoluongghep.Text = "S.L";
            this.lblsoluongghep.Visible = false;
            // 
            // txtGia
            // 
            this.txtGia._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtGia._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGia._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGia.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtGia.AutoCompleteList")));
            this.txtGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGia.buildShortcut = false;
            this.txtGia.CaseSensitive = false;
            this.txtGia.CompareNoID = true;
            this.txtGia.DefaultCode = "-1";
            this.txtGia.DefaultID = "-1";
            this.txtGia.DisplayType = 1;
            this.txtGia.Drug_ID = null;
            this.txtGia.ExtraWidth = 400;
            this.txtGia.FillValueAfterSelect = false;
            this.txtGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGia.Location = new System.Drawing.Point(126, 541);
            this.txtGia.MaxHeight = 289;
            this.txtGia.MinTypedCharacters = 2;
            this.txtGia.MyCode = "-1";
            this.txtGia.MyID = "-1";
            this.txtGia.MyText = "";
            this.txtGia.MyTextOnly = "";
            this.txtGia.Name = "txtGia";
            this.txtGia.RaiseEvent = true;
            this.txtGia.RaiseEventEnter = true;
            this.txtGia.RaiseEventEnterWhenEmpty = false;
            this.txtGia.SelectedIndex = -1;
            this.txtGia.Size = new System.Drawing.Size(109, 21);
            this.txtGia.splitChar = '@';
            this.txtGia.splitCharIDAndCode = '#';
            this.txtGia.TabIndex = 9;
            this.txtGia.TakeCode = true;
            this.txtGia.txtMyCode = null;
            this.txtGia.txtMyCode_Edit = null;
            this.txtGia.txtMyID = null;
            this.txtGia.txtMyID_Edit = null;
            this.txtGia.txtMyName = null;
            this.txtGia.txtMyName_Edit = null;
            this.txtGia.txtNext = this.cmdSave;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(1019, 714);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // txtBedCode
            // 
            this.txtBedCode._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBedCode._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBedCode._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBedCode.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBedCode.AutoCompleteList")));
            this.txtBedCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBedCode.buildShortcut = false;
            this.txtBedCode.CaseSensitive = false;
            this.txtBedCode.CompareNoID = true;
            this.txtBedCode.DefaultCode = "-1";
            this.txtBedCode.DefaultID = "-1";
            this.txtBedCode.DisplayType = 0;
            this.txtBedCode.Drug_ID = null;
            this.txtBedCode.ExtraWidth = 0;
            this.txtBedCode.FillValueAfterSelect = false;
            this.txtBedCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBedCode.Location = new System.Drawing.Point(632, 99);
            this.txtBedCode.MaxHeight = 289;
            this.txtBedCode.MinTypedCharacters = 2;
            this.txtBedCode.MyCode = "-1";
            this.txtBedCode.MyID = "-1";
            this.txtBedCode.MyText = "";
            this.txtBedCode.MyTextOnly = "";
            this.txtBedCode.Name = "txtBedCode";
            this.txtBedCode.RaiseEvent = true;
            this.txtBedCode.RaiseEventEnter = true;
            this.txtBedCode.RaiseEventEnterWhenEmpty = true;
            this.txtBedCode.SelectedIndex = -1;
            this.txtBedCode.Size = new System.Drawing.Size(634, 22);
            this.txtBedCode.splitChar = '@';
            this.txtBedCode.splitCharIDAndCode = '#';
            this.txtBedCode.TabIndex = 8;
            this.txtBedCode.TakeCode = false;
            this.txtBedCode.txtMyCode = null;
            this.txtBedCode.txtMyCode_Edit = null;
            this.txtBedCode.txtMyID = null;
            this.txtBedCode.txtMyID_Edit = null;
            this.txtBedCode.txtMyName = null;
            this.txtBedCode.txtMyName_Edit = null;
            this.txtBedCode.txtNext = null;
            // 
            // txtRoom_code
            // 
            this.txtRoom_code._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtRoom_code._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoom_code._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRoom_code.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtRoom_code.AutoCompleteList")));
            this.txtRoom_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoom_code.buildShortcut = false;
            this.txtRoom_code.CaseSensitive = false;
            this.txtRoom_code.CompareNoID = true;
            this.txtRoom_code.DefaultCode = "-1";
            this.txtRoom_code.DefaultID = "-1";
            this.txtRoom_code.DisplayType = 0;
            this.txtRoom_code.Drug_ID = null;
            this.txtRoom_code.ExtraWidth = 0;
            this.txtRoom_code.FillValueAfterSelect = false;
            this.txtRoom_code.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoom_code.Location = new System.Drawing.Point(126, 100);
            this.txtRoom_code.MaxHeight = 289;
            this.txtRoom_code.MinTypedCharacters = 2;
            this.txtRoom_code.MyCode = "-1";
            this.txtRoom_code.MyID = "-1";
            this.txtRoom_code.MyText = "";
            this.txtRoom_code.MyTextOnly = "";
            this.txtRoom_code.Name = "txtRoom_code";
            this.txtRoom_code.RaiseEvent = true;
            this.txtRoom_code.RaiseEventEnter = true;
            this.txtRoom_code.RaiseEventEnterWhenEmpty = true;
            this.txtRoom_code.SelectedIndex = -1;
            this.txtRoom_code.Size = new System.Drawing.Size(387, 22);
            this.txtRoom_code.splitChar = '@';
            this.txtRoom_code.splitCharIDAndCode = '#';
            this.txtRoom_code.TabIndex = 7;
            this.txtRoom_code.TakeCode = false;
            this.txtRoom_code.txtMyCode = null;
            this.txtRoom_code.txtMyCode_Edit = null;
            this.txtRoom_code.txtMyID = null;
            this.txtRoom_code.txtMyID_Edit = null;
            this.txtRoom_code.txtMyName = null;
            this.txtRoom_code.txtMyName_Edit = null;
            this.txtRoom_code.txtNext = null;
            // 
            // cboGia
            // 
            this.cboGia.AllowDrop = true;
            this.cboGia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cboGia_DesignTimeLayout.LayoutString = resources.GetString("cboGia_DesignTimeLayout.LayoutString");
            this.cboGia.DesignTimeLayout = cboGia_DesignTimeLayout;
            this.cboGia.DisplayMember = "_name";
            this.cboGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGia.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.cboGia.Location = new System.Drawing.Point(236, 541);
            this.cboGia.Name = "cboGia";
            this.cboGia.SelectedIndex = -1;
            this.cboGia.SelectedItem = null;
            this.cboGia.Size = new System.Drawing.Size(1030, 21);
            this.cboGia.TabIndex = 9;
            this.cboGia.TabStop = false;
            this.cboGia.Text = "CHỌN GIÁ BUỒNG GIƯỜNG";
            this.cboGia.ValueMember = "ID";
            this.cboGia.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // lblGiaBG
            // 
            this.lblGiaBG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGiaBG.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaBG.ForeColor = System.Drawing.Color.Red;
            this.lblGiaBG.Location = new System.Drawing.Point(0, 541);
            this.lblGiaBG.Name = "lblGiaBG";
            this.lblGiaBG.Size = new System.Drawing.Size(120, 21);
            this.lblGiaBG.TabIndex = 501;
            this.lblGiaBG.Text = "Chọn giá:";
            this.lblGiaBG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(542, 101);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 16);
            this.label13.TabIndex = 500;
            this.label13.Text = "Chọn giường";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(9, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 21);
            this.label12.TabIndex = 499;
            this.label12.Text = "Chọn buồng";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(714, 47);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(35, 16);
            this.label26.TabIndex = 494;
            this.label26.Text = "(giờ)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(714, 19);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 16);
            this.label25.TabIndex = 493;
            this.label25.Text = "(giờ)";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(519, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 21);
            this.label24.TabIndex = 492;
            this.label24.Text = "Tính tròn ngày >=";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHour2Cal
            // 
            this.txtHour2Cal.BackColor = System.Drawing.Color.White;
            this.txtHour2Cal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHour2Cal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHour2Cal.ForeColor = System.Drawing.Color.Navy;
            this.txtHour2Cal.Location = new System.Drawing.Point(632, 17);
            this.txtHour2Cal.Masked = MaskedTextBox.Mask.Digit;
            this.txtHour2Cal.Name = "txtHour2Cal";
            this.txtHour2Cal.ReadOnly = true;
            this.txtHour2Cal.Size = new System.Drawing.Size(76, 22);
            this.txtHour2Cal.TabIndex = 1;
            this.txtHour2Cal.TabStop = false;
            this.txtHour2Cal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalHour
            // 
            this.txtTotalHour.BackColor = System.Drawing.Color.White;
            this.txtTotalHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalHour.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalHour.ForeColor = System.Drawing.Color.Navy;
            this.txtTotalHour.Location = new System.Drawing.Point(632, 44);
            this.txtTotalHour.Masked = MaskedTextBox.Mask.Digit;
            this.txtTotalHour.Name = "txtTotalHour";
            this.txtTotalHour.ReadOnly = true;
            this.txtTotalHour.Size = new System.Drawing.Size(76, 22);
            this.txtTotalHour.TabIndex = 5;
            this.txtTotalHour.TabStop = false;
            this.txtTotalHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(554, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 21);
            this.label14.TabIndex = 489;
            this.label14.Text = "Tổng giờ:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkAutoCal
            // 
            this.chkAutoCal.AutoSize = true;
            this.chkAutoCal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.chkAutoCal.Location = new System.Drawing.Point(375, 73);
            this.chkAutoCal.Name = "chkAutoCal";
            this.chkAutoCal.Size = new System.Drawing.Size(232, 20);
            this.chkAutoCal.TabIndex = 7;
            this.chkAutoCal.TabStop = false;
            this.chkAutoCal.Text = "Tự động tính số ngày nằm viện?";
            this.chkAutoCal.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(332, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 16);
            this.label22.TabIndex = 487;
            this.label22.Text = "(ngày)";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(9, 73);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(111, 21);
            this.label23.TabIndex = 485;
            this.label23.Text = "Tổng số ngày:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhutvao
            // 
            this.txtPhutvao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhutvao.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General;
            this.txtPhutvao.Location = new System.Drawing.Point(472, 17);
            this.txtPhutvao.MaxLength = 2;
            this.txtPhutvao.Name = "txtPhutvao";
            this.txtPhutvao.NullBehavior = Janus.Windows.GridEX.NumericEditNullBehavior.AllowNull;
            this.txtPhutvao.ReadOnly = true;
            this.txtPhutvao.Size = new System.Drawing.Size(41, 22);
            this.txtPhutvao.TabIndex = 1;
            this.txtPhutvao.TabStop = false;
            this.txtPhutvao.Text = "0";
            this.txtPhutvao.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPhutvao.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(429, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 16);
            this.label19.TabIndex = 484;
            this.label19.Text = "Phút";
            // 
            // txtGiovao
            // 
            this.txtGiovao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiovao.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General;
            this.txtGiovao.Location = new System.Drawing.Point(375, 17);
            this.txtGiovao.MaxLength = 2;
            this.txtGiovao.Name = "txtGiovao";
            this.txtGiovao.NullBehavior = Janus.Windows.GridEX.NumericEditNullBehavior.AllowNull;
            this.txtGiovao.ReadOnly = true;
            this.txtGiovao.Size = new System.Drawing.Size(41, 22);
            this.txtGiovao.TabIndex = 1;
            this.txtGiovao.TabStop = false;
            this.txtGiovao.Text = "0";
            this.txtGiovao.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtGiovao.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(332, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(30, 16);
            this.label20.TabIndex = 483;
            this.label20.Text = "Giờ";
            // 
            // dtNgayvao
            // 
            this.dtNgayvao.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtNgayvao.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayvao.DropDownCalendar.Name = "";
            this.dtNgayvao.Enabled = false;
            this.dtNgayvao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayvao.Location = new System.Drawing.Point(126, 19);
            this.dtNgayvao.Name = "dtNgayvao";
            this.dtNgayvao.ReadOnly = true;
            this.dtNgayvao.ShowUpDown = true;
            this.dtNgayvao.Size = new System.Drawing.Size(185, 22);
            this.dtNgayvao.TabIndex = 1;
            this.dtNgayvao.TabStop = false;
            this.dtNgayvao.Value = new System.DateTime(2014, 3, 7, 0, 0, 0, 0);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(111, 21);
            this.label21.TabIndex = 479;
            this.label21.Text = "Ngày vào:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdBuong);
            this.panel1.Controls.Add(this.grdGiuong);
            this.panel1.Location = new System.Drawing.Point(6, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1260, 408);
            this.panel1.TabIndex = 478;
            // 
            // grdBuong
            // 
            this.grdBuong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdBuong.AlternatingColors = true;
            this.grdBuong.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdBuong_DesignTimeLayout.LayoutString = resources.GetString("grdBuong_DesignTimeLayout.LayoutString");
            this.grdBuong.DesignTimeLayout = grdBuong_DesignTimeLayout;
            this.grdBuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdBuong.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdBuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdBuong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdBuong.GroupByBoxVisible = false;
            this.grdBuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdBuong.Location = new System.Drawing.Point(0, 0);
            this.grdBuong.Name = "grdBuong";
            this.grdBuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdBuong.Size = new System.Drawing.Size(632, 408);
            this.grdBuong.TabIndex = 9;
            this.grdBuong.TabStop = false;
            this.grdBuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdGiuong
            // 
            this.grdGiuong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdGiuong.AlternatingColors = true;
            this.grdGiuong.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdGiuong.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grdGiuong.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdGiuong_DesignTimeLayout.LayoutString = resources.GetString("grdGiuong_DesignTimeLayout.LayoutString");
            this.grdGiuong.DesignTimeLayout = grdGiuong_DesignTimeLayout;
            this.grdGiuong.Dock = System.Windows.Forms.DockStyle.Right;
            this.grdGiuong.DynamicFiltering = true;
            this.grdGiuong.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdGiuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdGiuong.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdGiuong.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdGiuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdGiuong.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGiuong.GroupByBoxVisible = false;
            this.grdGiuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdGiuong.Location = new System.Drawing.Point(632, 0);
            this.grdGiuong.Name = "grdGiuong";
            this.grdGiuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdGiuong.Size = new System.Drawing.Size(628, 408);
            this.grdGiuong.TabIndex = 10;
            this.grdGiuong.TabStop = false;
            this.grdGiuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(720, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 16);
            this.label11.TabIndex = 475;
            this.label11.Text = "&ID";
            // 
            // txtPhut
            // 
            this.txtPhut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhut.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General;
            this.txtPhut.Location = new System.Drawing.Point(472, 44);
            this.txtPhut.MaxLength = 2;
            this.txtPhut.Name = "txtPhut";
            this.txtPhut.NullBehavior = Janus.Windows.GridEX.NumericEditNullBehavior.AllowNull;
            this.txtPhut.ReadOnly = true;
            this.txtPhut.Size = new System.Drawing.Size(41, 22);
            this.txtPhut.TabIndex = 4;
            this.txtPhut.Text = "0";
            this.txtPhut.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPhut.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(429, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Phút";
            // 
            // txtGio
            // 
            this.txtGio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGio.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General;
            this.txtGio.Location = new System.Drawing.Point(375, 46);
            this.txtGio.MaxLength = 2;
            this.txtGio.Name = "txtGio";
            this.txtGio.NullBehavior = Janus.Windows.GridEX.NumericEditNullBehavior.AllowNull;
            this.txtGio.ReadOnly = true;
            this.txtGio.Size = new System.Drawing.Size(41, 22);
            this.txtGio.TabIndex = 3;
            this.txtGio.Text = "0";
            this.txtGio.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtGio.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(332, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Giờ";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(9, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "Ngày chuyển";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtNgayChuyen
            // 
            this.dtNgayChuyen.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtNgayChuyen.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayChuyen.DropDownCalendar.Name = "";
            this.dtNgayChuyen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayChuyen.Location = new System.Drawing.Point(126, 46);
            this.dtNgayChuyen.Name = "dtNgayChuyen";
            this.dtNgayChuyen.ShowUpDown = true;
            this.dtNgayChuyen.Size = new System.Drawing.Size(185, 22);
            this.dtNgayChuyen.TabIndex = 2;
            this.dtNgayChuyen.Value = new System.DateTime(2014, 4, 6, 0, 0, 0, 0);
            // 
            // chkKhongtinh
            // 
            this.chkKhongtinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkKhongtinh.AutoSize = true;
            this.chkKhongtinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKhongtinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.chkKhongtinh.Location = new System.Drawing.Point(132, 702);
            this.chkKhongtinh.Name = "chkKhongtinh";
            this.chkKhongtinh.Size = new System.Drawing.Size(132, 20);
            this.chkKhongtinh.TabIndex = 519;
            this.chkKhongtinh.TabStop = false;
            this.chkKhongtinh.Text = "Không tính tiền?";
            this.chkKhongtinh.UseVisualStyleBackColor = true;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1146, 714);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "Thoát";
            // 
            // cmdHelp
            // 
            this.cmdHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHelp.Image = ((System.Drawing.Image)(resources.GetObject("cmdHelp.Image")));
            this.cmdHelp.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHelp.Location = new System.Drawing.Point(6, 720);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(102, 29);
            this.cmdHelp.TabIndex = 13;
            this.cmdHelp.TabStop = false;
            this.cmdHelp.Text = "Trợ giúp";
            this.cmdHelp.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.Location = new System.Drawing.Point(12, 727);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(693, 32);
            this.lblMsg.TabIndex = 19;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // grpThongTinBN
            // 
            this.grpThongTinBN.Controls.Add(this.ucThongtinnguoibenh1);
            this.grpThongTinBN.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinBN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinBN.Location = new System.Drawing.Point(0, 0);
            this.grpThongTinBN.Name = "grpThongTinBN";
            this.grpThongTinBN.Size = new System.Drawing.Size(1284, 122);
            this.grpThongTinBN.TabIndex = 520;
            this.grpThongTinBN.Text = "Thông tin Bệnh nhân";
            this.grpThongTinBN.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // ucThongtinnguoibenh1
            // 
            this.ucThongtinnguoibenh1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucThongtinnguoibenh1.Location = new System.Drawing.Point(18, 12);
            this.ucThongtinnguoibenh1.Name = "ucThongtinnguoibenh1";
            this.ucThongtinnguoibenh1.Size = new System.Drawing.Size(1260, 106);
            this.ucThongtinnguoibenh1.TabIndex = 0;
            // 
            // frm_Chuyengiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.grpThongTinBN);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.chkKhongtinh);
            this.Controls.Add(this.grpThongTinChuyenKhoa);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Chuyengiuong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển giường khoa điều trị";
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinChuyenKhoa)).EndInit();
            this.grpThongTinChuyenKhoa.ResumeLayout(false);
            this.grpThongTinChuyenKhoa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoluongghep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoluong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGia)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).EndInit();
            this.grpThongTinBN.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpThongTinChuyenKhoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayChuyen;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.NumericEditBox txtPhut;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.NumericEditBox txtGio;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientDept_ID;
        private Janus.Windows.EditControls.UIButton cmdHelp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.GridEX.GridEX grdBuong;
        private Janus.Windows.GridEX.GridEX grdGiuong;
        private Janus.Windows.GridEX.EditControls.NumericEditBox txtPhutvao;
        private System.Windows.Forms.Label label19;
        private Janus.Windows.GridEX.EditControls.NumericEditBox txtGiovao;
        private System.Windows.Forms.Label label20;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayvao;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkAutoCal;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private MaskedTextBox.MaskedTextBox txtHour2Cal;
        private MaskedTextBox.MaskedTextBox txtTotalHour;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblMsg;
        private UCs.AutoCompleteTextbox txtBedCode;
        private UCs.AutoCompleteTextbox txtRoom_code;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo cboGia;
        private System.Windows.Forms.Label lblGiaBG;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private UCs.AutoCompleteTextbox txtGia;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.CheckBox chkGhepgiuong;
        private System.Windows.Forms.Label lblsoluongghep;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkKhongtinh;
        private System.Windows.Forms.NumericUpDown nmrSoluong;
        private System.Windows.Forms.NumericUpDown nmrSoluongghep;
        private Janus.Windows.EditControls.UIGroupBox grpThongTinBN;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh ucThongtinnguoibenh1;
        
    }
}