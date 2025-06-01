namespace VNS.HIS.UI.Baocao
{
    partial class frm_thongketheoma_ICD10
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thongketheoma_ICD10));
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkChitiet = new Janus.Windows.EditControls.UICheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdAdd = new Janus.Windows.EditControls.UIButton();
            this.txtListICD = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.txtMaBenhICD10 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // chkChitiet
            // 
            this.chkChitiet.Checked = true;
            this.chkChitiet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChitiet.Location = new System.Drawing.Point(587, 54);
            this.chkChitiet.Name = "chkChitiet";
            this.chkChitiet.Size = new System.Drawing.Size(209, 23);
            this.chkChitiet.TabIndex = 7;
            this.chkChitiet.TabStop = false;
            this.chkChitiet.Text = "Chi tiết theo từng Bệnh nhân?";
            this.toolTip1.SetToolTip(this.chkChitiet, "Bỏ chọn mục này để báo cáo theo số lượng tổng hợp");
            this.chkChitiet.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(909, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "Nhấn F3 (In Báo Cáo)";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabel1.Text = "Nhấn F3(In báo cáo)";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel2.Text = "Nhấn F4(Xuất Excel)";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(95, 17);
            this.toolStripStatusLabel3.Text = "Nhấn Esc(Thoát)";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(224, 17);
            this.toolStripStatusLabel4.Text = "Nhấn Crtl+A (Thêm mã ICD để tìm kiếm)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiGroupBox2);
            this.panel1.Controls.Add(this.baocaO_TIEUDE1);
            this.panel1.Controls.Add(this.cmdExportToExcel);
            this.panel1.Controls.Add(this.dtNgayInPhieu);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmdInPhieuXN);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 540);
            this.panel1.TabIndex = 1;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.cmdAdd);
            this.uiGroupBox2.Controls.Add(this.txtListICD);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.txtMaBenhICD10);
            this.uiGroupBox2.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox2.Controls.Add(this.chkChitiet);
            this.uiGroupBox2.Controls.Add(this.panel2);
            this.uiGroupBox2.Controls.Add(this.cboKhoa);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 50);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(909, 443);
            this.uiGroupBox2.TabIndex = 123;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // cmdAdd
            // 
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAdd.Location = new System.Drawing.Point(537, 82);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(44, 27);
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // txtListICD
            // 
            this.txtListICD.Location = new System.Drawing.Point(587, 82);
            this.txtListICD.Name = "txtListICD";
            this.txtListICD.Size = new System.Drawing.Size(310, 27);
            this.txtListICD.TabIndex = 2;
            this.txtListICD.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 62;
            this.label2.Text = "ICD-10";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(587, 25);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(310, 23);
            this.cboDoituongKCB.TabIndex = 1;
            this.cboDoituongKCB.TabStop = false;
            this.cboDoituongKCB.Text = "Đối tượng";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.grdChitiet);
            this.panel2.Controls.Add(this.grdList);
            this.panel2.Location = new System.Drawing.Point(6, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 314);
            this.panel2.TabIndex = 46;
            // 
            // grdChitiet
            // 
            this.grdChitiet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.Size = new System.Drawing.Size(897, 314);
            this.grdChitiet.TabIndex = 21;
            this.grdChitiet.TabStop = false;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdList
            // 
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(897, 314);
            this.grdList.TabIndex = 20;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(123, 25);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(358, 23);
            this.cboKhoa.TabIndex = 0;
            this.cboKhoa.TabStop = false;
            this.cboKhoa.Text = "Khoa thực hiện";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Khoa KCB";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(477, 28);
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
            this.dtToDate.DropDownCalendar.Visible = false;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(331, 54);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 23);
            this.dtToDate.TabIndex = 6;
            this.dtToDate.TabStop = false;
            this.dtToDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
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
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(123, 54);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(200, 23);
            this.dtFromDate.TabIndex = 5;
            this.dtFromDate.TabStop = false;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(48, 54);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.Text = "Từ ngày";
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(380, 499);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 118;
            this.cmdExportToExcel.TabStop = false;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(97, 504);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 120;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(19, 507);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 121;
            this.label3.Text = "Ngày in";
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuXN.Location = new System.Drawing.Point(519, 499);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieuXN.TabIndex = 117;
            this.cmdInPhieuXN.TabStop = false;
            this.cmdInPhieuXN.Text = "In báo cáo";
            this.cmdInPhieuXN.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(658, 499);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 119;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // txtMaBenhICD10
            // 
            this.txtMaBenhICD10._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtMaBenhICD10._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBenhICD10._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMaBenhICD10.AutoCompleteList = null;
            this.txtMaBenhICD10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaBenhICD10.CaseSensitive = false;
            this.txtMaBenhICD10.CompareNoID = true;
            this.txtMaBenhICD10.DefaultCode = "-1";
            this.txtMaBenhICD10.DefaultID = "-1";
            this.txtMaBenhICD10.Drug_ID = null;
            this.txtMaBenhICD10.ExtraWidth = 0;
            this.txtMaBenhICD10.FillValueAfterSelect = false;
            this.txtMaBenhICD10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBenhICD10.Location = new System.Drawing.Point(123, 85);
            this.txtMaBenhICD10.MaxHeight = 289;
            this.txtMaBenhICD10.MaxLength = 100;
            this.txtMaBenhICD10.MinTypedCharacters = 2;
            this.txtMaBenhICD10.MyCode = "-1";
            this.txtMaBenhICD10.MyID = "-1";
            this.txtMaBenhICD10.MyText = "";
            this.txtMaBenhICD10.Name = "txtMaBenhICD10";
            this.txtMaBenhICD10.RaiseEvent = true;
            this.txtMaBenhICD10.RaiseEventEnter = false;
            this.txtMaBenhICD10.RaiseEventEnterWhenEmpty = true;
            this.txtMaBenhICD10.SelectedIndex = -1;
            this.txtMaBenhICD10.Size = new System.Drawing.Size(408, 21);
            this.txtMaBenhICD10.splitChar = '@';
            this.txtMaBenhICD10.splitCharIDAndCode = '#';
            this.txtMaBenhICD10.TabIndex = 0;
            this.txtMaBenhICD10.TakeCode = false;
            this.txtMaBenhICD10.txtMyCode = null;
            this.txtMaBenhICD10.txtMyCode_Edit = null;
            this.txtMaBenhICD10.txtMyID = null;
            this.txtMaBenhICD10.txtMyID_Edit = null;
            this.txtMaBenhICD10.txtMyName = null;
            this.txtMaBenhICD10.txtMyName_Edit = null;
            this.txtMaBenhICD10.txtNext = null;
            this.txtMaBenhICD10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBenhICD10_KeyDown);
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(909, 53);
            this.baocaO_TIEUDE1.TabIndex = 122;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO THỐNG KÊ THEO MÃ BỆNH ICD10";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frm_thongketheoma_ICD10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thongketheoma_ICD10";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THỐNG KÊ THEO MÃ BỆNH ICD10";
            this.Load += new System.EventHandler(this.frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_thongketheoma_ICD10_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIButton cmdAdd;
        private System.Windows.Forms.RichTextBox txtListICD;
        private System.Windows.Forms.Label label2;
        public UCs.AutoCompleteTextbox txtMaBenhICD10;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private Janus.Windows.EditControls.UICheckBox chkChitiet;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
    }
}