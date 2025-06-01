namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_InGopBenhNhan
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InGopBenhNhan));
            Janus.Windows.GridEX.GridEXLayout grdHoaDonCapPhat_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radTatCa = new System.Windows.Forms.RadioButton();
            this.radNgoaiTru = new System.Windows.Forms.RadioButton();
            this.radNoiTru = new System.Windows.Forms.RadioButton();
            this.radDaHoaDon = new System.Windows.Forms.RadioButton();
            this.radChuaHoaDon = new System.Windows.Forms.RadioButton();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenBN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatient_Code = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiTabPage2 = new Janus.Windows.UI.Tab.UITabPage();
            this.rtxtLogs = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.grpThongTinHoaDon = new System.Windows.Forms.GroupBox();
            this.grdHoaDonCapPhat = new Janus.Windows.GridEX.GridEX();
            this.pHoaDonDo = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cmdLoadHoaDon = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSerieCuoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSerieDau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSerie = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtMaQuyen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtKiHieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtMauHD = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label41 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.uiTabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpThongTinHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHoaDonCapPhat)).BeginInit();
            this.pHoaDonDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.radDaHoaDon);
            this.groupBox1.Controls.Add(this.radChuaHoaDon);
            this.groupBox1.Controls.Add(this.cmdTimKiem);
            this.groupBox1.Controls.Add(this.dtDenNgay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtTuNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTenBN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPatient_Code);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(977, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm thông tin ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "đến";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radTatCa);
            this.panel3.Controls.Add(this.radNgoaiTru);
            this.panel3.Controls.Add(this.radNoiTru);
            this.panel3.Location = new System.Drawing.Point(498, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 26);
            this.panel3.TabIndex = 13;
            // 
            // radTatCa
            // 
            this.radTatCa.AutoSize = true;
            this.radTatCa.Checked = true;
            this.radTatCa.Location = new System.Drawing.Point(3, 4);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(58, 19);
            this.radTatCa.TabIndex = 12;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            this.radTatCa.UseVisualStyleBackColor = true;
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.AutoSize = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(81, 4);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(75, 19);
            this.radNgoaiTru.TabIndex = 11;
            this.radNgoaiTru.Text = "Ngoại trú";
            this.radNgoaiTru.UseVisualStyleBackColor = true;
            // 
            // radNoiTru
            // 
            this.radNoiTru.AutoSize = true;
            this.radNoiTru.Location = new System.Drawing.Point(174, 4);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(61, 19);
            this.radNoiTru.TabIndex = 10;
            this.radNoiTru.Text = "Nội trú";
            this.radNoiTru.UseVisualStyleBackColor = true;
            // 
            // radDaHoaDon
            // 
            this.radDaHoaDon.AutoSize = true;
            this.radDaHoaDon.Location = new System.Drawing.Point(620, 21);
            this.radDaHoaDon.Name = "radDaHoaDon";
            this.radDaHoaDon.Size = new System.Drawing.Size(113, 19);
            this.radDaHoaDon.TabIndex = 9;
            this.radDaHoaDon.Text = "Đã hóa đơn gộp";
            this.radDaHoaDon.UseVisualStyleBackColor = true;
            // 
            // radChuaHoaDon
            // 
            this.radChuaHoaDon.AutoSize = true;
            this.radChuaHoaDon.Checked = true;
            this.radChuaHoaDon.Location = new System.Drawing.Point(501, 21);
            this.radChuaHoaDon.Name = "radChuaHoaDon";
            this.radChuaHoaDon.Size = new System.Drawing.Size(102, 19);
            this.radChuaHoaDon.TabIndex = 8;
            this.radChuaHoaDon.TabStop = true;
            this.radChuaHoaDon.Text = "Chưa hóa đơn";
            this.radChuaHoaDon.UseVisualStyleBackColor = true;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Location = new System.Drawing.Point(824, 24);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(110, 42);
            this.cmdTimKiem.TabIndex = 7;
            this.cmdTimKiem.Text = "Tìm kiếm(F3)";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.VS2005;
            this.dtDenNgay.Location = new System.Drawing.Point(314, 46);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(162, 21);
            this.dtDenNgay.TabIndex = 6;
            this.dtDenNgay.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.VS2005;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Từ ngày";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.VS2005;
            this.dtTuNgay.Location = new System.Drawing.Point(97, 46);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(145, 21);
            this.dtTuNgay.TabIndex = 4;
            this.dtTuNgay.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.VS2005;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên BN";
            // 
            // txtTenBN
            // 
            this.txtTenBN.Location = new System.Drawing.Point(314, 18);
            this.txtTenBN.Name = "txtTenBN";
            this.txtTenBN.Size = new System.Drawing.Size(162, 21);
            this.txtTenBN.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã lần khám";
            // 
            // txtPatient_Code
            // 
            this.txtPatient_Code.Location = new System.Drawing.Point(97, 19);
            this.txtPatient_Code.Name = "txtPatient_Code";
            this.txtPatient_Code.Size = new System.Drawing.Size(145, 21);
            this.txtPatient_Code.TabIndex = 0;
            this.txtPatient_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_Code_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.prgBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 20);
            this.panel1.TabIndex = 1;
            // 
            // prgBar
            // 
            this.prgBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgBar.Location = new System.Drawing.Point(0, 0);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(977, 20);
            this.prgBar.TabIndex = 20;
            this.prgBar.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.uiTab1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(977, 403);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin hóa đơn";
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Location = new System.Drawing.Point(3, 17);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(971, 383);
            this.uiTab1.TabIndex = 0;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1,
            this.uiTabPage2});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Office2007;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.grdList);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(969, 359);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Thông tin thanh toán";
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin thanh toán</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(969, 359);
            this.grdList.TabIndex = 3;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiTabPage2
            // 
            this.uiTabPage2.Controls.Add(this.rtxtLogs);
            this.uiTabPage2.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage2.Name = "uiTabPage2";
            this.uiTabPage2.Size = new System.Drawing.Size(969, 359);
            this.uiTabPage2.TabStop = true;
            this.uiTabPage2.Text = "Log";
            // 
            // rtxtLogs
            // 
            this.rtxtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLogs.Location = new System.Drawing.Point(0, 0);
            this.rtxtLogs.Name = "rtxtLogs";
            this.rtxtLogs.Size = new System.Drawing.Size(969, 359);
            this.rtxtLogs.TabIndex = 0;
            this.rtxtLogs.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 626);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(977, 43);
            this.panel2.TabIndex = 3;
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Location = new System.Drawing.Point(315, 3);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(161, 36);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Text = "Cập nhập hóa đơn";
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(482, 4);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(161, 36);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // grpThongTinHoaDon
            // 
            this.grpThongTinHoaDon.Controls.Add(this.grdHoaDonCapPhat);
            this.grpThongTinHoaDon.Controls.Add(this.pHoaDonDo);
            this.grpThongTinHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinHoaDon.Font = new System.Drawing.Font("Arial", 9F);
            this.grpThongTinHoaDon.Location = new System.Drawing.Point(0, 93);
            this.grpThongTinHoaDon.Name = "grpThongTinHoaDon";
            this.grpThongTinHoaDon.Size = new System.Drawing.Size(977, 130);
            this.grpThongTinHoaDon.TabIndex = 5;
            this.grpThongTinHoaDon.TabStop = false;
            this.grpThongTinHoaDon.Text = "Thông tin hóa đơn";
            // 
            // grdHoaDonCapPhat
            // 
            this.grdHoaDonCapPhat.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdHoaDonCapPhat.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            grdHoaDonCapPhat_DesignTimeLayout.LayoutString = resources.GetString("grdHoaDonCapPhat_DesignTimeLayout.LayoutString");
            this.grdHoaDonCapPhat.DesignTimeLayout = grdHoaDonCapPhat_DesignTimeLayout;
            this.grdHoaDonCapPhat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHoaDonCapPhat.Font = new System.Drawing.Font("Arial", 9F);
            this.grdHoaDonCapPhat.GridLines = Janus.Windows.GridEX.GridLines.Default;
            this.grdHoaDonCapPhat.GroupByBoxVisible = false;
            this.grdHoaDonCapPhat.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdHoaDonCapPhat.Location = new System.Drawing.Point(597, 17);
            this.grdHoaDonCapPhat.Name = "grdHoaDonCapPhat";
            this.grdHoaDonCapPhat.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdHoaDonCapPhat.Size = new System.Drawing.Size(377, 110);
            this.grdHoaDonCapPhat.TabIndex = 514;
            this.grdHoaDonCapPhat.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdHoaDonCapPhat.SelectionChanged += new System.EventHandler(this.grdHoaDonCapPhat_SelectionChanged);
            // 
            // pHoaDonDo
            // 
            this.pHoaDonDo.Controls.Add(this.label4);
            this.pHoaDonDo.Controls.Add(this.txtLydo);
            this.pHoaDonDo.Controls.Add(this.cmdLoadHoaDon);
            this.pHoaDonDo.Controls.Add(this.label45);
            this.pHoaDonDo.Controls.Add(this.label22);
            this.pHoaDonDo.Controls.Add(this.txtSerieCuoi);
            this.pHoaDonDo.Controls.Add(this.txtSerieDau);
            this.pHoaDonDo.Controls.Add(this.txtSerie);
            this.pHoaDonDo.Controls.Add(this.label44);
            this.pHoaDonDo.Controls.Add(this.txtMaQuyen);
            this.pHoaDonDo.Controls.Add(this.label43);
            this.pHoaDonDo.Controls.Add(this.txtKiHieu);
            this.pHoaDonDo.Controls.Add(this.label42);
            this.pHoaDonDo.Controls.Add(this.txtMauHD);
            this.pHoaDonDo.Controls.Add(this.label41);
            this.pHoaDonDo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pHoaDonDo.Location = new System.Drawing.Point(3, 17);
            this.pHoaDonDo.Name = "pHoaDonDo";
            this.pHoaDonDo.Size = new System.Drawing.Size(594, 110);
            this.pHoaDonDo.TabIndex = 513;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 516;
            this.label4.Text = "Lý do";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.SystemColors.Control;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydo.AddValues = false;
            this.txtLydo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "\"\"";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.LOAI_DANHMUC = "LYDO_GOPHOADON";
            this.txtLydo.Location = new System.Drawing.Point(86, 82);
            this.txtLydo.MaxHeight = 300;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.Size = new System.Drawing.Size(403, 21);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 515;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = null;
            // 
            // cmdLoadHoaDon
            // 
            this.cmdLoadHoaDon.BackColor = System.Drawing.SystemColors.Control;
            this.cmdLoadHoaDon.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdLoadHoaDon.Location = new System.Drawing.Point(495, 27);
            this.cmdLoadHoaDon.Name = "cmdLoadHoaDon";
            this.cmdLoadHoaDon.Size = new System.Drawing.Size(95, 29);
            this.cmdLoadHoaDon.TabIndex = 514;
            this.cmdLoadHoaDon.Text = "Lấy hóa đơn";
            this.cmdLoadHoaDon.UseVisualStyleBackColor = false;
            this.cmdLoadHoaDon.Click += new System.EventHandler(this.cmdLoadHoaDon_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(267, 33);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(64, 15);
            this.label45.TabIndex = 63;
            this.label45.Text = "Serie Cuối";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(269, 10);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 15);
            this.label22.TabIndex = 62;
            this.label22.Text = "Serie Đầu";
            // 
            // txtSerieCuoi
            // 
            this.txtSerieCuoi.Location = new System.Drawing.Point(334, 30);
            this.txtSerieCuoi.Name = "txtSerieCuoi";
            this.txtSerieCuoi.ReadOnly = true;
            this.txtSerieCuoi.Size = new System.Drawing.Size(155, 21);
            this.txtSerieCuoi.TabIndex = 61;
            this.txtSerieCuoi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // txtSerieDau
            // 
            this.txtSerieDau.Location = new System.Drawing.Point(334, 6);
            this.txtSerieDau.Name = "txtSerieDau";
            this.txtSerieDau.ReadOnly = true;
            this.txtSerieDau.Size = new System.Drawing.Size(155, 21);
            this.txtSerieDau.TabIndex = 60;
            this.txtSerieDau.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // txtSerie
            // 
            this.txtSerie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.Location = new System.Drawing.Point(334, 54);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.ReadOnly = true;
            this.txtSerie.Size = new System.Drawing.Size(155, 26);
            this.txtSerie.TabIndex = 59;
            this.txtSerie.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSerie.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(249, 60);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(82, 13);
            this.label44.TabIndex = 58;
            this.label44.Text = "Serie hiện tại";
            // 
            // txtMaQuyen
            // 
            this.txtMaQuyen.Location = new System.Drawing.Point(86, 54);
            this.txtMaQuyen.Name = "txtMaQuyen";
            this.txtMaQuyen.ReadOnly = true;
            this.txtMaQuyen.Size = new System.Drawing.Size(155, 21);
            this.txtMaQuyen.TabIndex = 57;
            this.txtMaQuyen.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(22, 57);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(61, 15);
            this.label43.TabIndex = 56;
            this.label43.Text = "Mã quyển";
            // 
            // txtKiHieu
            // 
            this.txtKiHieu.Location = new System.Drawing.Point(86, 30);
            this.txtKiHieu.Name = "txtKiHieu";
            this.txtKiHieu.ReadOnly = true;
            this.txtKiHieu.Size = new System.Drawing.Size(155, 21);
            this.txtKiHieu.TabIndex = 55;
            this.txtKiHieu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(36, 33);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(47, 15);
            this.label42.TabIndex = 54;
            this.label42.Text = "Ký hiệu";
            // 
            // txtMauHD
            // 
            this.txtMauHD.Location = new System.Drawing.Point(86, 6);
            this.txtMauHD.Name = "txtMauHD";
            this.txtMauHD.ReadOnly = true;
            this.txtMauHD.Size = new System.Drawing.Size(155, 21);
            this.txtMauHD.TabIndex = 53;
            this.txtMauHD.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(3, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(80, 15);
            this.label41.TabIndex = 52;
            this.label41.Text = "Mẫu hóa đơn";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_InGopBenhNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 669);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpThongTinHoaDon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_InGopBenhNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chức năng in gộp hóa đơn nhiều bệnh nhân";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_InGopBenhNhan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_InGopBenhNhan_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.uiTabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpThongTinHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHoaDonCapPhat)).EndInit();
            this.pHoaDonDo.ResumeLayout(false);
            this.pHoaDonDo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenBN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPatient_Code;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.RadioButton radDaHoaDon;
        private System.Windows.Forms.RadioButton radChuaHoaDon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage2;
        private System.Windows.Forms.RichTextBox rtxtLogs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radTatCa;
        private System.Windows.Forms.RadioButton radNgoaiTru;
        private System.Windows.Forms.RadioButton radNoiTru;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.GroupBox grpThongTinHoaDon;
        private System.Windows.Forms.Panel pHoaDonDo;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label22;
        private Janus.Windows.GridEX.EditControls.EditBox txtSerieCuoi;
        private Janus.Windows.GridEX.EditControls.EditBox txtSerieDau;
        private Janus.Windows.GridEX.EditControls.EditBox txtSerie;
        private System.Windows.Forms.Label label44;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaQuyen;
        private System.Windows.Forms.Label label43;
        private Janus.Windows.GridEX.EditControls.EditBox txtKiHieu;
        private System.Windows.Forms.Label label42;
        private Janus.Windows.GridEX.EditControls.EditBox txtMauHD;
        private System.Windows.Forms.Label label41;
        private Janus.Windows.GridEX.GridEX grdHoaDonCapPhat;
        private System.Windows.Forms.Button cmdLoadHoaDon;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar prgBar;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.Label label4;
    }
}