namespace VNS.HIS.UI.Baocao
{
    partial class frm_tonghopchiphiKCB_bvsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_tonghopchiphiKCB_bvsg));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtBacsi = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cboNhanvien = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGT = new Janus.Windows.EditControls.UIComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkLoaitimkiem = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbokhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdTonghop = new Janus.Windows.EditControls.UIButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(612, 10);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(119, 30);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Xuất Excel";
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
            this.dtNgayInPhieu.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(88, 19);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 12;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
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
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(737, 10);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(119, 30);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "In báo cáo";
            this.cmdPrint.ToolTipText = "In lại dữ liệu đã tổng hợp";
            this.cmdPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click_1);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = global::VMS.HIS.BAOCAO.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(987, 10);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 30);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txtBacsi);
            this.uiGroupBox2.Controls.Add(this.cboNhanvien);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.cboGT);
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.chkLoaitimkiem);
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
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 53);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1118, 670);
            this.uiGroupBox2.TabIndex = 115;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
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
            this.txtBacsi.Location = new System.Drawing.Point(559, 50);
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
            this.txtBacsi.Size = new System.Drawing.Size(315, 21);
            this.txtBacsi.splitChar = '@';
            this.txtBacsi.splitCharIDAndCode = '#';
            this.txtBacsi.TabIndex = 133;
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
            // cboNhanvien
            // 
            this.cboNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhanvien.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboNhanvien.Location = new System.Drawing.Point(138, 73);
            this.cboNhanvien.Name = "cboNhanvien";
            this.cboNhanvien.SelectInDataSource = true;
            this.cboNhanvien.Size = new System.Drawing.Size(736, 21);
            this.cboNhanvien.TabIndex = 131;
            this.cboNhanvien.Text = "Thu ngân viên";
            this.cboNhanvien.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(28, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 132;
            this.label8.Text = "Thu ngân viên:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(459, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 130;
            this.label1.Text = "Bác sĩ chỉ định:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGT
            // 
            this.cboGT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Giới tính";
            uiComboBoxItem1.Value = -1;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Nam";
            uiComboBoxItem2.Value = 0;
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Nữ";
            uiComboBoxItem3.Value = 1;
            this.cboGT.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboGT.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboGT.Location = new System.Drawing.Point(835, 25);
            this.cboGT.Name = "cboGT";
            this.cboGT.SelectedIndex = 0;
            this.cboGT.SelectInDataSource = true;
            this.cboGT.Size = new System.Drawing.Size(11, 21);
            this.cboGT.TabIndex = 128;
            this.cboGT.Text = "Giới tính";
            this.cboGT.Visible = false;
            this.cboGT.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(818, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 22);
            this.label6.TabIndex = 127;
            this.label6.Text = "Giới tính: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // chkLoaitimkiem
            // 
            this.chkLoaitimkiem.Checked = true;
            this.chkLoaitimkiem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoaitimkiem.ForeColor = System.Drawing.Color.Navy;
            this.chkLoaitimkiem.Location = new System.Drawing.Point(852, 25);
            this.chkLoaitimkiem.Name = "chkLoaitimkiem";
            this.chkLoaitimkiem.Size = new System.Drawing.Size(25, 23);
            this.chkLoaitimkiem.TabIndex = 8;
            this.chkLoaitimkiem.Text = "Dịch vụ chưa thanh toán";
            this.toolTip1.SetToolTip(this.chkLoaitimkiem, "Bỏ check sẽ tìm theo ngày chốt viện phí");
            this.chkLoaitimkiem.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "đến";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Location = new System.Drawing.Point(6, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1106, 465);
            this.panel1.TabIndex = 56;
            // 
            // grdChitiet
            // 
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChitiet.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdChitiet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChitiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdChitiet.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RecordNavigator = true;
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.Size = new System.Drawing.Size(1106, 465);
            this.grdChitiet.TabIndex = 21;
            this.grdChitiet.TabStop = false;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grdChitiet.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdChitiet_FormattingRow);
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(138, 49);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(315, 21);
            this.cboDoituongKCB.TabIndex = 2;
            this.cboDoituongKCB.Text = "Chọn loại đối tượng KCB";
            this.cboDoituongKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 53);
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
            this.cbokhoa.Size = new System.Drawing.Size(315, 21);
            this.cbokhoa.TabIndex = 0;
            this.cbokhoa.Text = "Chọn khoa KCB";
            this.cbokhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
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
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtToDate.Location = new System.Drawing.Point(326, 98);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(127, 21);
            this.dtToDate.TabIndex = 7;
            this.dtToDate.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            this.dtToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtFromDate.Location = new System.Drawing.Point(138, 98);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(140, 21);
            this.dtFromDate.TabIndex = 6;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.ForeColor = System.Drawing.Color.Navy;
            this.chkByDate.Location = new System.Drawing.Point(67, 98);
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
            // cmdTonghop
            // 
            this.cmdTonghop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTonghop.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdTonghop.Image = global::VMS.HIS.BAOCAO.Properties.Resources.printer_32;
            this.cmdTonghop.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTonghop.Location = new System.Drawing.Point(862, 10);
            this.cmdTonghop.Name = "cmdTonghop";
            this.cmdTonghop.Size = new System.Drawing.Size(119, 30);
            this.cmdTonghop.TabIndex = 89;
            this.cmdTonghop.Text = "Tổng hợp";
            this.toolTip1.SetToolTip(this.cmdTonghop, "Tổng hợp và in");
            this.cmdTonghop.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdTonghop.Click += new System.EventHandler(this.cmdTonghop_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdTonghop);
            this.panel2.Controls.Add(this.cmdExportToExcel);
            this.panel2.Controls.Add(this.dtNgayInPhieu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 671);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1118, 52);
            this.panel2.TabIndex = 115;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BCDSACH_BNHANLINHTHUOC";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1118, 53);
            this.baocaO_TIEUDE1.TabIndex = 114;
            this.baocaO_TIEUDE1.TIEUDE = "TỔNG HỢP CHI PHÍ KHÁM CHỮA BỆNH";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frm_tonghopchiphiKCB_bvsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 723);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_tonghopchiphiKCB_bvsg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp chi phí KCB";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
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
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UICheckBox chkLoaitimkiem;
        private Janus.Windows.EditControls.UIComboBox cboGT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIComboBox cboNhanvien;
        private System.Windows.Forms.Label label8;
        private UCs.AutoCompleteTextbox txtBacsi;
        private Janus.Windows.EditControls.UIButton cmdTonghop;
    }
}