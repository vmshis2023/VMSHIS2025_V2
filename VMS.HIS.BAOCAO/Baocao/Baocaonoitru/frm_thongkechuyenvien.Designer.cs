namespace VNS.HIS.UI.Baocao
{
    partial class frm_thongkechuyenvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thongkechuyenvien));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdChuyendi_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChuyenDen_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBacSy = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBenhVien = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.radChuyenDi = new System.Windows.Forms.RadioButton();
            this.txtTenBenh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.radChuyenDen = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cboNTNT = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChuyendi = new Janus.Windows.GridEX.GridEX();
            this.grdChuyenDen = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
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
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChuyendi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChuyenDen)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.txtBacSy);
            this.uiGroupBox2.Controls.Add(this.txtBenhVien);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.radChuyenDi);
            this.uiGroupBox2.Controls.Add(this.txtTenBenh);
            this.uiGroupBox2.Controls.Add(this.radChuyenDen);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.cboNTNT);
            this.uiGroupBox2.Controls.Add(this.label1);
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
            this.uiGroupBox2.Location = new System.Drawing.Point(3, 39);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1006, 505);
            this.uiGroupBox2.TabIndex = 115;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(491, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 611;
            this.label6.Text = "Bác sỹ chuyển:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBacSy
            // 
            this.txtBacSy._backcolor = System.Drawing.SystemColors.Control;
            this.txtBacSy._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacSy._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBacSy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBacSy.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBacSy.AutoCompleteList")));
            this.txtBacSy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacSy.buildShortcut = false;
            this.txtBacSy.CaseSensitive = false;
            this.txtBacSy.CompareNoID = true;
            this.txtBacSy.DefaultCode = "-1";
            this.txtBacSy.DefaultID = "-1";
            this.txtBacSy.DisplayType = 0;
            this.txtBacSy.Drug_ID = null;
            this.txtBacSy.ExtraWidth = 0;
            this.txtBacSy.FillValueAfterSelect = false;
            this.txtBacSy.Location = new System.Drawing.Point(594, 25);
            this.txtBacSy.MaxHeight = 289;
            this.txtBacSy.MinTypedCharacters = 2;
            this.txtBacSy.MyCode = "-1";
            this.txtBacSy.MyID = "-1";
            this.txtBacSy.MyText = "";
            this.txtBacSy.MyTextOnly = "";
            this.txtBacSy.Name = "txtBacSy";
            this.txtBacSy.RaiseEvent = false;
            this.txtBacSy.RaiseEventEnter = false;
            this.txtBacSy.RaiseEventEnterWhenEmpty = false;
            this.txtBacSy.SelectedIndex = -1;
            this.txtBacSy.Size = new System.Drawing.Size(352, 21);
            this.txtBacSy.splitChar = '@';
            this.txtBacSy.splitCharIDAndCode = '#';
            this.txtBacSy.TabIndex = 610;
            this.txtBacSy.TakeCode = false;
            this.txtBacSy.txtMyCode = null;
            this.txtBacSy.txtMyCode_Edit = null;
            this.txtBacSy.txtMyID = null;
            this.txtBacSy.txtMyID_Edit = null;
            this.txtBacSy.txtMyName = null;
            this.txtBacSy.txtMyName_Edit = null;
            this.txtBacSy.txtNext = null;
            // 
            // txtBenhVien
            // 
            this.txtBenhVien._backcolor = System.Drawing.SystemColors.Control;
            this.txtBenhVien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhVien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhVien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhVien.AutoCompleteList")));
            this.txtBenhVien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhVien.buildShortcut = false;
            this.txtBenhVien.CaseSensitive = false;
            this.txtBenhVien.CompareNoID = true;
            this.txtBenhVien.DefaultCode = "-1";
            this.txtBenhVien.DefaultID = "-1";
            this.txtBenhVien.DisplayType = 0;
            this.txtBenhVien.Drug_ID = null;
            this.txtBenhVien.ExtraWidth = 0;
            this.txtBenhVien.FillValueAfterSelect = false;
            this.txtBenhVien.Location = new System.Drawing.Point(138, 78);
            this.txtBenhVien.MaxHeight = -1;
            this.txtBenhVien.MinTypedCharacters = 2;
            this.txtBenhVien.MyCode = "-1";
            this.txtBenhVien.MyID = "-1";
            this.txtBenhVien.MyText = "";
            this.txtBenhVien.MyTextOnly = "";
            this.txtBenhVien.Name = "txtBenhVien";
            this.txtBenhVien.RaiseEvent = false;
            this.txtBenhVien.RaiseEventEnter = false;
            this.txtBenhVien.RaiseEventEnterWhenEmpty = false;
            this.txtBenhVien.SelectedIndex = -1;
            this.txtBenhVien.Size = new System.Drawing.Size(352, 21);
            this.txtBenhVien.splitChar = '@';
            this.txtBenhVien.splitCharIDAndCode = '#';
            this.txtBenhVien.TabIndex = 609;
            this.txtBenhVien.TakeCode = false;
            this.txtBenhVien.txtMyCode = null;
            this.txtBenhVien.txtMyCode_Edit = null;
            this.txtBenhVien.txtMyID = null;
            this.txtBenhVien.txtMyID_Edit = null;
            this.txtBenhVien.txtMyName = null;
            this.txtBenhVien.txtMyName_Edit = null;
            this.txtBenhVien.txtNext = null;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(496, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 15);
            this.label5.TabIndex = 608;
            this.label5.Text = "Tên bệnh:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radChuyenDi
            // 
            this.radChuyenDi.AutoSize = true;
            this.radChuyenDi.Checked = true;
            this.radChuyenDi.Location = new System.Drawing.Point(598, 108);
            this.radChuyenDi.Name = "radChuyenDi";
            this.radChuyenDi.Size = new System.Drawing.Size(80, 19);
            this.radChuyenDi.TabIndex = 1;
            this.radChuyenDi.TabStop = true;
            this.radChuyenDi.Text = "Chuyển đi";
            this.radChuyenDi.UseVisualStyleBackColor = true;
            this.radChuyenDi.CheckedChanged += new System.EventHandler(this.radChuyenDi_CheckedChanged);
            // 
            // txtTenBenh
            // 
            this.txtTenBenh._backcolor = System.Drawing.SystemColors.Control;
            this.txtTenBenh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenBenh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTenBenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenBenh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTenBenh.AutoCompleteList")));
            this.txtTenBenh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenBenh.buildShortcut = false;
            this.txtTenBenh.CaseSensitive = false;
            this.txtTenBenh.CompareNoID = true;
            this.txtTenBenh.DefaultCode = "-1";
            this.txtTenBenh.DefaultID = "-1";
            this.txtTenBenh.DisplayType = 0;
            this.txtTenBenh.Drug_ID = null;
            this.txtTenBenh.ExtraWidth = 0;
            this.txtTenBenh.FillValueAfterSelect = false;
            this.txtTenBenh.Location = new System.Drawing.Point(594, 78);
            this.txtTenBenh.MaxHeight = 289;
            this.txtTenBenh.MinTypedCharacters = 2;
            this.txtTenBenh.MyCode = "-1";
            this.txtTenBenh.MyID = "-1";
            this.txtTenBenh.MyText = "";
            this.txtTenBenh.MyTextOnly = "";
            this.txtTenBenh.Name = "txtTenBenh";
            this.txtTenBenh.RaiseEvent = false;
            this.txtTenBenh.RaiseEventEnter = false;
            this.txtTenBenh.RaiseEventEnterWhenEmpty = false;
            this.txtTenBenh.SelectedIndex = -1;
            this.txtTenBenh.Size = new System.Drawing.Size(352, 21);
            this.txtTenBenh.splitChar = '@';
            this.txtTenBenh.splitCharIDAndCode = '#';
            this.txtTenBenh.TabIndex = 607;
            this.txtTenBenh.TakeCode = false;
            this.txtTenBenh.txtMyCode = null;
            this.txtTenBenh.txtMyCode_Edit = null;
            this.txtTenBenh.txtMyID = null;
            this.txtTenBenh.txtMyID_Edit = null;
            this.txtTenBenh.txtMyName = null;
            this.txtTenBenh.txtMyName_Edit = null;
            this.txtTenBenh.txtNext = null;
            // 
            // radChuyenDen
            // 
            this.radChuyenDen.AutoSize = true;
            this.radChuyenDen.Location = new System.Drawing.Point(733, 108);
            this.radChuyenDen.Name = "radChuyenDen";
            this.radChuyenDen.Size = new System.Drawing.Size(91, 19);
            this.radChuyenDen.TabIndex = 0;
            this.radChuyenDen.Text = "Chuyển đến";
            this.radChuyenDen.UseVisualStyleBackColor = true;
            this.radChuyenDen.CheckedChanged += new System.EventHandler(this.radChuyenDen_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 270;
            this.label4.Text = "Bệnh viện:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNTNT
            // 
            this.cboNTNT.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
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
            this.cboNTNT.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboNTNT.Location = new System.Drawing.Point(138, 48);
            this.cboNTNT.Name = "cboNTNT";
            this.cboNTNT.Size = new System.Drawing.Size(352, 21);
            this.cboNTNT.TabIndex = 1;
            this.cboNTNT.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 269;
            this.label1.Text = "Loại điều trị:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "đến ngày";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdChuyendi);
            this.panel1.Controls.Add(this.grdChuyenDen);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(3, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 350);
            this.panel1.TabIndex = 56;
            // 
            // grdChuyendi
            // 
            this.grdChuyendi.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChuyendi_DesignTimeLayout.LayoutString = resources.GetString("grdChuyendi_DesignTimeLayout.LayoutString");
            this.grdChuyendi.DesignTimeLayout = grdChuyendi_DesignTimeLayout;
            this.grdChuyendi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChuyendi.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChuyendi.GroupByBoxVisible = false;
            this.grdChuyendi.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuyendi.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdChuyendi.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdChuyendi.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdChuyendi.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChuyendi.Location = new System.Drawing.Point(0, 0);
            this.grdChuyendi.Name = "grdChuyendi";
            this.grdChuyendi.RecordNavigator = true;
            this.grdChuyendi.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChuyendi.Size = new System.Drawing.Size(1000, 298);
            this.grdChuyendi.TabIndex = 120;
            this.grdChuyendi.TabStop = false;
            this.grdChuyendi.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuyendi.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChuyendi.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // grdChuyenDen
            // 
            this.grdChuyenDen.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChuyenDen_DesignTimeLayout.LayoutString = resources.GetString("grdChuyenDen_DesignTimeLayout.LayoutString");
            this.grdChuyenDen.DesignTimeLayout = grdChuyenDen_DesignTimeLayout;
            this.grdChuyenDen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChuyenDen.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChuyenDen.GroupByBoxVisible = false;
            this.grdChuyenDen.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuyenDen.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdChuyenDen.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdChuyenDen.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdChuyenDen.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChuyenDen.Location = new System.Drawing.Point(0, 0);
            this.grdChuyenDen.Name = "grdChuyenDen";
            this.grdChuyenDen.RecordNavigator = true;
            this.grdChuyenDen.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChuyenDen.Size = new System.Drawing.Size(1000, 298);
            this.grdChuyenDen.TabIndex = 118;
            this.grdChuyenDen.TabStop = false;
            this.grdChuyenDen.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuyenDen.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChuyenDen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdExportToExcel);
            this.panel2.Controls.Add(this.dtNgayInPhieu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 52);
            this.panel2.TabIndex = 119;
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(389, 10);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(119, 34);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(65, 16);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(106, 21);
            this.dtNgayInPhieu.TabIndex = 12;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(11, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 88;
            this.label3.Text = "Ngày in";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(639, 10);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 34);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(514, 10);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(119, 34);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "In báo cáo";
            this.cmdPrint.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDoituongKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(594, 50);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(352, 21);
            this.cboDoituongKCB.TabIndex = 2;
            this.cboDoituongKCB.Text = "Chọn loại đối tượng KCB";
            this.cboDoituongKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(493, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 15);
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
            this.cbokhoa.Size = new System.Drawing.Size(352, 21);
            this.cbokhoa.TabIndex = 0;
            this.cbokhoa.Text = "Chọn khoa KCB";
            this.cbokhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
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
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtToDate.Location = new System.Drawing.Point(355, 108);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(135, 21);
            this.dtToDate.TabIndex = 7;
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
            this.dtFromDate.Location = new System.Drawing.Point(138, 106);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(150, 21);
            this.dtFromDate.TabIndex = 6;
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
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
            this.tabControl1.Size = new System.Drawing.Size(1020, 573);
            this.tabControl1.TabIndex = 116;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.uiGroupBox2);
            this.tabPage1.Controls.Add(this.baocaO_TIEUDE1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1012, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông kê chuyển viện";
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1006, 36);
            this.baocaO_TIEUDE1.TabIndex = 114;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO  CÁO THỐNG KÊ CHUYỂN VIỆN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frm_thongkechuyenvien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1020, 573);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thongkechuyenvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo thu viện phí";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChuyendi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChuyenDen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
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
        private Janus.Windows.EditControls.UIComboBox cboNTNT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radChuyenDi;
        private System.Windows.Forms.RadioButton radChuyenDen;
        private UCs.AutoCompleteTextbox txtTenBenh;
        private UCs.AutoCompleteTextbox txtBenhVien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private UCs.AutoCompleteTextbox txtBacSy;
        private Janus.Windows.GridEX.GridEX grdChuyenDen;
        private Janus.Windows.GridEX.GridEX grdChuyendi;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdPrint;
    }
}