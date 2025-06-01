namespace VNS.HIS.UI.NOITRU
{
    partial class frm_phanbuonggiuong
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_phanbuonggiuong));
            Janus.Windows.GridEX.GridEXLayout cboGia_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdBuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpThongTinBN = new Janus.Windows.EditControls.UIGroupBox();
            this.ucThongtinnguoibenh1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh();
            this.lblMsg = new System.Windows.Forms.Label();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.dtpPhut = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpGio = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpNgaynhapvien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDepartment_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtDepartmentName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtsoluongghep = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkGhepgiuong = new System.Windows.Forms.CheckBox();
            this.txtRoom_code = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBedCode = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtNgayChuyen = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.cboGia = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.lblGiaBG = new System.Windows.Forms.Label();
            this.grdBuong = new Janus.Windows.GridEX.GridEX();
            this.grdGiuong = new Janus.Windows.GridEX.GridEX();
            this.lblsoluongghep = new System.Windows.Forms.Label();
            this.txtPatientDept_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkKhongtinh = new System.Windows.Forms.CheckBox();
            this.txtBacsi = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.txtGia = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).BeginInit();
            this.grpThongTinBN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpThongTinBN
            // 
            this.grpThongTinBN.Controls.Add(this.ucThongtinnguoibenh1);
            this.grpThongTinBN.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinBN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinBN.Location = new System.Drawing.Point(0, 0);
            this.grpThongTinBN.Name = "grpThongTinBN";
            this.grpThongTinBN.Size = new System.Drawing.Size(1284, 122);
            this.grpThongTinBN.TabIndex = 0;
            this.grpThongTinBN.Text = "Thông tin Bệnh nhân";
            this.grpThongTinBN.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // ucThongtinnguoibenh1
            // 
            this.ucThongtinnguoibenh1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucThongtinnguoibenh1.Location = new System.Drawing.Point(17, 12);
            this.ucThongtinnguoibenh1.Name = "ucThongtinnguoibenh1";
            this.ucThongtinnguoibenh1.Size = new System.Drawing.Size(1261, 106);
            this.ucThongtinnguoibenh1.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(6, 96);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(739, 22);
            this.lblMsg.TabIndex = 555;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 738);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "F5: Làm mới thông tin buồng-giường";
            uiStatusBarPanel1.Width = 184;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1});
            this.uiStatusBar1.Size = new System.Drawing.Size(1284, 23);
            this.uiStatusBar1.TabIndex = 2;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dtpPhut);
            this.uiGroupBox1.Controls.Add(this.dtpGio);
            this.uiGroupBox1.Controls.Add(this.dtpNgaynhapvien);
            this.uiGroupBox1.Controls.Add(this.label19);
            this.uiGroupBox1.Controls.Add(this.txtDepartment_ID);
            this.uiGroupBox1.Controls.Add(this.txtDepartmentName);
            this.uiGroupBox1.Controls.Add(this.txtsoluongghep);
            this.uiGroupBox1.Controls.Add(this.label12);
            this.uiGroupBox1.Controls.Add(this.chkGhepgiuong);
            this.uiGroupBox1.Controls.Add(this.txtRoom_code);
            this.uiGroupBox1.Controls.Add(this.txtBedCode);
            this.uiGroupBox1.Controls.Add(this.label11);
            this.uiGroupBox1.Controls.Add(this.dtNgayChuyen);
            this.uiGroupBox1.Controls.Add(this.label8);
            this.uiGroupBox1.Controls.Add(this.label10);
            this.uiGroupBox1.Controls.Add(this.label9);
            this.uiGroupBox1.Controls.Add(this.label13);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 122);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1284, 98);
            this.uiGroupBox1.TabIndex = 554;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            // 
            // dtpPhut
            // 
            this.dtpPhut.CustomFormat = "mm";
            this.dtpPhut.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpPhut.DropDownCalendar.Name = "";
            this.dtpPhut.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpPhut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPhut.Location = new System.Drawing.Point(433, 38);
            this.dtpPhut.MinuteIncrement = 1;
            this.dtpPhut.Name = "dtpPhut";
            this.dtpPhut.SecondIncrement = 1;
            this.dtpPhut.ShowDropDown = false;
            this.dtpPhut.ShowTodayButton = false;
            this.dtpPhut.ShowUpDown = true;
            this.dtpPhut.Size = new System.Drawing.Size(59, 22);
            this.dtpPhut.TabIndex = 521;
            this.dtpPhut.Value = new System.DateTime(2023, 9, 9, 0, 0, 0, 0);
            this.dtpPhut.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // dtpGio
            // 
            this.dtpGio.CustomFormat = "HH";
            this.dtpGio.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpGio.DropDownCalendar.Name = "";
            this.dtpGio.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpGio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpGio.Location = new System.Drawing.Point(336, 38);
            this.dtpGio.MinuteIncrement = 1;
            this.dtpGio.Name = "dtpGio";
            this.dtpGio.SecondIncrement = 1;
            this.dtpGio.ShowDropDown = false;
            this.dtpGio.ShowTodayButton = false;
            this.dtpGio.ShowUpDown = true;
            this.dtpGio.Size = new System.Drawing.Size(50, 22);
            this.dtpGio.TabIndex = 520;
            this.dtpGio.Value = new System.DateTime(2023, 9, 9, 0, 0, 0, 0);
            this.dtpGio.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // dtpNgaynhapvien
            // 
            this.dtpNgaynhapvien.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpNgaynhapvien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaynhapvien.DropDownCalendar.Name = "";
            this.dtpNgaynhapvien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgaynhapvien.Enabled = false;
            this.dtpNgaynhapvien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynhapvien.Location = new System.Drawing.Point(617, 10);
            this.dtpNgaynhapvien.MinuteIncrement = 1;
            this.dtpNgaynhapvien.Name = "dtpNgaynhapvien";
            this.dtpNgaynhapvien.ReadOnly = true;
            this.dtpNgaynhapvien.SecondIncrement = 1;
            this.dtpNgaynhapvien.ShowUpDown = true;
            this.dtpNgaynhapvien.Size = new System.Drawing.Size(226, 22);
            this.dtpNgaynhapvien.TabIndex = 518;
            this.dtpNgaynhapvien.Value = new System.DateTime(2023, 9, 9, 0, 0, 0, 0);
            this.dtpNgaynhapvien.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label19.Location = new System.Drawing.Point(509, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(102, 16);
            this.label19.TabIndex = 519;
            this.label19.Text = "Ngày nhập viện:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDepartment_ID
            // 
            this.txtDepartment_ID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment_ID.Location = new System.Drawing.Point(129, 11);
            this.txtDepartment_ID.Name = "txtDepartment_ID";
            this.txtDepartment_ID.ReadOnly = true;
            this.txtDepartment_ID.Size = new System.Drawing.Size(103, 22);
            this.txtDepartment_ID.TabIndex = 517;
            this.txtDepartment_ID.TabStop = false;
            this.txtDepartment_ID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartmentName.Location = new System.Drawing.Point(233, 11);
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.ReadOnly = true;
            this.txtDepartmentName.Size = new System.Drawing.Size(260, 22);
            this.txtDepartmentName.TabIndex = 516;
            this.txtDepartmentName.TabStop = false;
            this.txtDepartmentName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtsoluongghep
            // 
            this.txtsoluongghep.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsoluongghep.FormatMask = Janus.Windows.GridEX.NumericEditFormatMask.General;
            this.txtsoluongghep.Location = new System.Drawing.Point(617, 38);
            this.txtsoluongghep.MaxLength = 2;
            this.txtsoluongghep.Name = "txtsoluongghep";
            this.txtsoluongghep.NullBehavior = Janus.Windows.GridEX.NumericEditNullBehavior.AllowNull;
            this.txtsoluongghep.Size = new System.Drawing.Size(226, 22);
            this.txtsoluongghep.TabIndex = 514;
            this.txtsoluongghep.Text = "0";
            this.txtsoluongghep.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtsoluongghep.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtsoluongghep.Visible = false;
            this.txtsoluongghep.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(19, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 18);
            this.label12.TabIndex = 505;
            this.label12.Text = "Chọn buồng:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkGhepgiuong
            // 
            this.chkGhepgiuong.AutoSize = true;
            this.chkGhepgiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhepgiuong.Location = new System.Drawing.Point(498, 37);
            this.chkGhepgiuong.Name = "chkGhepgiuong";
            this.chkGhepgiuong.Size = new System.Drawing.Size(104, 20);
            this.chkGhepgiuong.TabIndex = 513;
            this.chkGhepgiuong.Text = "Ghép giường";
            this.chkGhepgiuong.UseVisualStyleBackColor = true;
            this.chkGhepgiuong.CheckedChanged += new System.EventHandler(this.chkGhepgiuong_CheckedChanged);
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
            this.txtRoom_code.Location = new System.Drawing.Point(129, 66);
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
            this.txtRoom_code.Size = new System.Drawing.Size(364, 22);
            this.txtRoom_code.splitChar = '@';
            this.txtRoom_code.splitCharIDAndCode = '#';
            this.txtRoom_code.TabIndex = 501;
            this.txtRoom_code.TakeCode = false;
            this.txtRoom_code.txtMyCode = null;
            this.txtRoom_code.txtMyCode_Edit = null;
            this.txtRoom_code.txtMyID = null;
            this.txtRoom_code.txtMyID_Edit = null;
            this.txtRoom_code.txtMyName = null;
            this.txtRoom_code.txtMyName_Edit = null;
            this.txtRoom_code.txtNext = null;
            // 
            // txtBedCode
            // 
            this.txtBedCode._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBedCode._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBedCode._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBedCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtBedCode.Location = new System.Drawing.Point(617, 66);
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
            this.txtBedCode.Size = new System.Drawing.Size(655, 22);
            this.txtBedCode.splitChar = '@';
            this.txtBedCode.splitCharIDAndCode = '#';
            this.txtBedCode.TabIndex = 502;
            this.txtBedCode.TakeCode = false;
            this.txtBedCode.txtMyCode = null;
            this.txtBedCode.txtMyCode_Edit = null;
            this.txtBedCode.txtMyID = null;
            this.txtBedCode.txtMyID_Edit = null;
            this.txtBedCode.txtMyName = null;
            this.txtBedCode.txtMyName_Edit = null;
            this.txtBedCode.txtNext = null;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label11.Location = new System.Drawing.Point(4, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 18);
            this.label11.TabIndex = 508;
            this.label11.Text = "Khoa nội trú:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtNgayChuyen
            // 
            this.dtNgayChuyen.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtNgayChuyen.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayChuyen.DropDownCalendar.Name = "";
            this.dtNgayChuyen.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNgayChuyen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayChuyen.Location = new System.Drawing.Point(129, 38);
            this.dtNgayChuyen.MinuteIncrement = 1;
            this.dtNgayChuyen.Name = "dtNgayChuyen";
            this.dtNgayChuyen.SecondIncrement = 1;
            this.dtNgayChuyen.ShowUpDown = true;
            this.dtNgayChuyen.Size = new System.Drawing.Size(166, 22);
            this.dtNgayChuyen.TabIndex = 497;
            this.dtNgayChuyen.Value = new System.DateTime(2023, 9, 9, 0, 0, 0, 0);
            this.dtNgayChuyen.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label8.Location = new System.Drawing.Point(7, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 16);
            this.label8.TabIndex = 509;
            this.label8.Text = "Ngày vào:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label10.Location = new System.Drawing.Point(392, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 511;
            this.label10.Text = "Phút";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label9.Location = new System.Drawing.Point(301, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 16);
            this.label9.TabIndex = 510;
            this.label9.Text = "Giờ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(514, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 16);
            this.label13.TabIndex = 506;
            this.label13.Text = "Chọn giường:";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(-1, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 16);
            this.label14.TabIndex = 519;
            this.label14.Text = "Bác sĩ điều trị";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.Location = new System.Drawing.Point(1231, 31);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(31, 25);
            this.cmdSearch.TabIndex = 518;
            this.cmdSearch.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cboGia
            // 
            this.cboGia.AllowDrop = true;
            cboGia_DesignTimeLayout.LayoutString = resources.GetString("cboGia_DesignTimeLayout.LayoutString");
            this.cboGia.DesignTimeLayout = cboGia_DesignTimeLayout;
            this.cboGia.DisplayMember = "_name";
            this.cboGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGia.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.cboGia.Location = new System.Drawing.Point(282, 6);
            this.cboGia.Name = "cboGia";
            this.cboGia.SelectedIndex = -1;
            this.cboGia.SelectedItem = null;
            this.cboGia.Size = new System.Drawing.Size(704, 21);
            this.cboGia.TabIndex = 504;
            this.cboGia.Text = "CHỌN GIÁ BUỒNG GIƯỜNG";
            this.cboGia.ValueMember = "ID";
            this.cboGia.Visible = false;
            this.cboGia.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // lblGiaBG
            // 
            this.lblGiaBG.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaBG.ForeColor = System.Drawing.Color.Red;
            this.lblGiaBG.Location = new System.Drawing.Point(3, 9);
            this.lblGiaBG.Name = "lblGiaBG";
            this.lblGiaBG.Size = new System.Drawing.Size(120, 18);
            this.lblGiaBG.TabIndex = 512;
            this.lblGiaBG.Text = "Chọn giá:";
            this.lblGiaBG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.grdBuong.Font = new System.Drawing.Font("Arial", 9F);
            this.grdBuong.GroupByBoxVisible = false;
            this.grdBuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdBuong.Location = new System.Drawing.Point(0, 0);
            this.grdBuong.Name = "grdBuong";
            this.grdBuong.RecordNavigator = true;
            this.grdBuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdBuong.Size = new System.Drawing.Size(598, 387);
            this.grdBuong.TabIndex = 552;
            this.grdBuong.TabStop = false;
            this.grdBuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
            this.grdGiuong.Location = new System.Drawing.Point(598, 0);
            this.grdGiuong.Name = "grdGiuong";
            this.grdGiuong.RecordNavigator = true;
            this.grdGiuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdGiuong.Size = new System.Drawing.Size(686, 387);
            this.grdGiuong.TabIndex = 553;
            this.grdGiuong.TabStop = false;
            this.grdGiuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // lblsoluongghep
            // 
            this.lblsoluongghep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblsoluongghep.AutoSize = true;
            this.lblsoluongghep.ForeColor = System.Drawing.Color.Black;
            this.lblsoluongghep.Location = new System.Drawing.Point(6, 115);
            this.lblsoluongghep.Name = "lblsoluongghep";
            this.lblsoluongghep.Size = new System.Drawing.Size(52, 13);
            this.lblsoluongghep.TabIndex = 515;
            this.lblsoluongghep.Text = "Số lượng:";
            this.lblsoluongghep.Visible = false;
            // 
            // txtPatientDept_ID
            // 
            this.txtPatientDept_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatientDept_ID.Enabled = false;
            this.txtPatientDept_ID.Location = new System.Drawing.Point(83, 96);
            this.txtPatientDept_ID.Name = "txtPatientDept_ID";
            this.txtPatientDept_ID.Size = new System.Drawing.Size(36, 20);
            this.txtPatientDept_ID.TabIndex = 500;
            this.txtPatientDept_ID.TabStop = false;
            this.txtPatientDept_ID.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 507;
            this.label1.Text = "ID";
            this.label1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkKhongtinh);
            this.panel1.Controls.Add(this.txtBacsi);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdSearch);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.txtPatientDept_ID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboGia);
            this.panel1.Controls.Add(this.lblsoluongghep);
            this.panel1.Controls.Add(this.txtGia);
            this.panel1.Controls.Add(this.lblGiaBG);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 607);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 131);
            this.panel1.TabIndex = 496;
            // 
            // chkKhongtinh
            // 
            this.chkKhongtinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkKhongtinh.AutoSize = true;
            this.chkKhongtinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKhongtinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.chkKhongtinh.Location = new System.Drawing.Point(126, 60);
            this.chkKhongtinh.Name = "chkKhongtinh";
            this.chkKhongtinh.Size = new System.Drawing.Size(132, 20);
            this.chkKhongtinh.TabIndex = 558;
            this.chkKhongtinh.TabStop = false;
            this.chkKhongtinh.Text = "Không tính tiền?";
            this.chkKhongtinh.UseVisualStyleBackColor = true;
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
            this.txtBacsi.DisplayType = 0;
            this.txtBacsi.Drug_ID = null;
            this.txtBacsi.ExtraWidth = 0;
            this.txtBacsi.FillValueAfterSelect = false;
            this.txtBacsi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsi.Location = new System.Drawing.Point(126, 33);
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
            this.txtBacsi.Size = new System.Drawing.Size(1099, 21);
            this.txtBacsi.splitChar = '@';
            this.txtBacsi.splitCharIDAndCode = '#';
            this.txtBacsi.TabIndex = 520;
            this.txtBacsi.TabStop = false;
            this.txtBacsi.TakeCode = false;
            this.txtBacsi.txtMyCode = null;
            this.txtBacsi.txtMyCode_Edit = null;
            this.txtBacsi.txtMyID = null;
            this.txtBacsi.txtMyID_Edit = null;
            this.txtBacsi.txtMyName = null;
            this.txtBacsi.txtMyName_Edit = null;
            this.txtBacsi.txtNext = null;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1142, 76);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(130, 35);
            this.cmdExit.TabIndex = 557;
            this.cmdExit.Text = "Thoát(Esc)";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(1006, 76);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(130, 35);
            this.cmdSave.TabIndex = 556;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_2);
            // 
            // txtGia
            // 
            this.txtGia._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtGia._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGia._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGia.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtGia.AutoCompleteList")));
            this.txtGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGia.buildShortcut = false;
            this.txtGia.CaseSensitive = false;
            this.txtGia.CompareNoID = true;
            this.txtGia.DefaultCode = "-1";
            this.txtGia.DefaultID = "-1";
            this.txtGia.DisplayType = 0;
            this.txtGia.Drug_ID = null;
            this.txtGia.ExtraWidth = 500;
            this.txtGia.FillValueAfterSelect = false;
            this.txtGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGia.Location = new System.Drawing.Point(126, 6);
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
            this.txtGia.Size = new System.Drawing.Size(149, 21);
            this.txtGia.splitChar = '@';
            this.txtGia.splitCharIDAndCode = '#';
            this.txtGia.TabIndex = 503;
            this.txtGia.TakeCode = true;
            this.txtGia.txtMyCode = null;
            this.txtGia.txtMyCode_Edit = null;
            this.txtGia.txtMyID = null;
            this.txtGia.txtMyID_Edit = null;
            this.txtGia.txtMyName = null;
            this.txtGia.txtMyName_Edit = null;
            this.txtGia.txtNext = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdBuong);
            this.panel2.Controls.Add(this.grdGiuong);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1284, 387);
            this.panel2.TabIndex = 521;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_phanbuonggiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.grpThongTinBN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiStatusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_phanbuonggiuong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển bệnh nhân vào giường";
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).EndInit();
            this.grpThongTinBN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpThongTinBN;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtDepartment_ID;
        internal Janus.Windows.GridEX.EditControls.EditBox txtDepartmentName;
        private Janus.Windows.GridEX.EditControls.NumericEditBox txtsoluongghep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkGhepgiuong;
        private UCs.AutoCompleteTextbox txtGia;
        public System.Windows.Forms.TextBox txtPatientDept_ID;
        private UCs.AutoCompleteTextbox txtRoom_code;
        private System.Windows.Forms.Label label1;
        private UCs.AutoCompleteTextbox txtBedCode;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo cboGia;
        private System.Windows.Forms.Label lblGiaBG;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayChuyen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblsoluongghep;
        private Janus.Windows.GridEX.GridEX grdBuong;
        private Janus.Windows.GridEX.GridEX grdGiuong;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private UCs.AutoCompleteTextbox txtBacsi;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaynhapvien;
        private System.Windows.Forms.Label label19;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpPhut;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpGio;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh ucThongtinnguoibenh1;
        private System.Windows.Forms.CheckBox chkKhongtinh;
    }
}