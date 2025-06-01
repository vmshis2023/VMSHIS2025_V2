namespace VNS.HIS.UI.Forms.Baocao.BaocaoNoitru
{
    partial class Frm_Sokhambenh_NoiTru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sokhambenh_NoiTru));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem22 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem23 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem24 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTinhTrang = new Janus.Windows.EditControls.UIComboBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.txtNhanvientiepdon = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1160, 364);
            this.grdList.TabIndex = 21;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(521, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 15);
            this.label5.TabIndex = 63;
            this.label5.Text = "Tình trạng:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem22.FormatStyle.Alpha = 0;
            uiComboBoxItem22.IsSeparator = false;
            uiComboBoxItem22.Text = "Tất cả";
            uiComboBoxItem22.Value = ((short)(-1));
            uiComboBoxItem23.FormatStyle.Alpha = 0;
            uiComboBoxItem23.IsSeparator = false;
            uiComboBoxItem23.Text = "Ngoại trú";
            uiComboBoxItem23.Value = ((short)(0));
            uiComboBoxItem24.FormatStyle.Alpha = 0;
            uiComboBoxItem24.IsSeparator = false;
            uiComboBoxItem24.Text = "Nội trú";
            uiComboBoxItem24.Value = ((short)(1));
            this.cboTinhTrang.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem22,
            uiComboBoxItem23,
            uiComboBoxItem24});
            this.cboTinhTrang.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboTinhTrang.Location = new System.Drawing.Point(632, 54);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.SelectInDataSource = true;
            this.cboTinhTrang.Size = new System.Drawing.Size(315, 21);
            this.cboTinhTrang.TabIndex = 62;
            this.cboTinhTrang.Text = "Đối tượng";
            this.cboTinhTrang.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.cboTinhTrang);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox2.Controls.Add(this.txtNhanvientiepdon);
            this.uiGroupBox2.Controls.Add(this.panel1);
            this.uiGroupBox2.Controls.Add(this.cboKhoa);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 59);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1172, 493);
            this.uiGroupBox2.TabIndex = 123;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(521, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 61;
            this.label2.Text = "đến ngày:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(234, 54);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(270, 21);
            this.cboDoituongKCB.TabIndex = 1;
            this.cboDoituongKCB.Text = "Đối tượng";
            this.cboDoituongKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // txtNhanvientiepdon
            // 
            this.txtNhanvientiepdon._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNhanvientiepdon._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvientiepdon._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhanvientiepdon.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhanvientiepdon.AutoCompleteList")));
            this.txtNhanvientiepdon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhanvientiepdon.CaseSensitive = false;
            this.txtNhanvientiepdon.CompareNoID = true;
            this.txtNhanvientiepdon.DefaultCode = "-1";
            this.txtNhanvientiepdon.DefaultID = "-1";
            this.txtNhanvientiepdon.Drug_ID = null;
            this.txtNhanvientiepdon.ExtraWidth = 0;
            this.txtNhanvientiepdon.FillValueAfterSelect = false;
            this.txtNhanvientiepdon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvientiepdon.Location = new System.Drawing.Point(632, 27);
            this.txtNhanvientiepdon.MaxHeight = 289;
            this.txtNhanvientiepdon.MaxLength = 100;
            this.txtNhanvientiepdon.MinTypedCharacters = 2;
            this.txtNhanvientiepdon.MyCode = "-1";
            this.txtNhanvientiepdon.MyID = "-1";
            this.txtNhanvientiepdon.MyText = "";
            this.txtNhanvientiepdon.Name = "txtNhanvientiepdon";
            this.txtNhanvientiepdon.RaiseEvent = true;
            this.txtNhanvientiepdon.RaiseEventEnter = false;
            this.txtNhanvientiepdon.RaiseEventEnterWhenEmpty = true;
            this.txtNhanvientiepdon.SelectedIndex = -1;
            this.txtNhanvientiepdon.Size = new System.Drawing.Size(315, 21);
            this.txtNhanvientiepdon.splitChar = '@';
            this.txtNhanvientiepdon.splitCharIDAndCode = '#';
            this.txtNhanvientiepdon.TabIndex = 60;
            this.txtNhanvientiepdon.TakeCode = false;
            this.txtNhanvientiepdon.txtMyCode = null;
            this.txtNhanvientiepdon.txtMyCode_Edit = null;
            this.txtNhanvientiepdon.txtMyID = null;
            this.txtNhanvientiepdon.txtMyID_Edit = null;
            this.txtNhanvientiepdon.txtMyName = null;
            this.txtNhanvientiepdon.txtMyName_Edit = null;
            this.txtNhanvientiepdon.txtNext = null;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(6, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 364);
            this.panel1.TabIndex = 46;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(234, 27);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(270, 21);
            this.cboKhoa.TabIndex = 0;
            this.cboKhoa.Text = "Khoa thực hiện";
            this.cboKhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(124, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Khoa KCB:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(521, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 30;
            this.label8.Text = "Bác sỹ kết thúc:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(124, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Đối tượng KCB:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(632, 81);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(315, 21);
            this.dtToDate.TabIndex = 6;
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
            this.dtFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(234, 81);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(270, 21);
            this.dtFromDate.TabIndex = 5;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(159, 82);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "SO_KHAMBENH";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1172, 53);
            this.baocaO_TIEUDE1.TabIndex = 122;
            this.baocaO_TIEUDE1.TIEUDE = "SỔ KHÁM BỆNH";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(364, 555);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 34);
            this.cmdExportToExcel.TabIndex = 118;
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(64, 560);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(118, 21);
            this.dtNgayInPhieu.TabIndex = 120;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(12, 563);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 121;
            this.label3.Text = "Ngày in:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuXN.Location = new System.Drawing.Point(503, 555);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 34);
            this.cmdInPhieuXN.TabIndex = 117;
            this.cmdInPhieuXN.Text = "In báo cáo";
            this.cmdInPhieuXN.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieuXN.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(642, 555);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 34);
            this.cmdExit.TabIndex = 119;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // Frm_Sokhambenh_NoiTru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 616);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.cmdExit);
            this.KeyPreview = true;
            this.Name = "Frm_Sokhambenh_NoiTru";
            this.Text = "Sổ khám bệnh Nội trú";
            this.Load += new System.EventHandler(this.Frm_Sokhambenh_NoiTru_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Sokhambenh_NoiTru_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIComboBox cboTinhTrang;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        public VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox txtNhanvientiepdon;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}