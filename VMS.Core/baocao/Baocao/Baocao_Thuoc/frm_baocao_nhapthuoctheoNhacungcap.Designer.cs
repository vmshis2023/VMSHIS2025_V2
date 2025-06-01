namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_baocao_nhapthuoctheoNhacungcap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocao_nhapthuoctheoNhacungcap));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.label1 = new System.Windows.Forms.Label();
            this.dtNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdBaoCao = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkPhieuvay = new Janus.Windows.EditControls.UICheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtthuoc = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc();
            this.txtLoaithuoc = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNhacungcap = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label7 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKho = new Janus.Windows.EditControls.UIComboBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.baocaO_TIEUDE1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 66);
            this.panel1.TabIndex = 1;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BC_NHAPKHOTHEONHACUNGCAP";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1008, 66);
            this.baocaO_TIEUDE1.TabIndex = 0;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO NHẬP KHO THEO NHÀ CUNG CẤP";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 690);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ngày in ";
            // 
            // dtNgayIn
            // 
            this.dtNgayIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayIn.CustomFormat = "dd/MM/yyyy";
            this.dtNgayIn.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayIn.DropDownCalendar.Name = "";
            this.dtNgayIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayIn.Location = new System.Drawing.Point(66, 687);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(134, 21);
            this.dtNgayIn.TabIndex = 11;
            // 
            // cmdBaoCao
            // 
            this.cmdBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBaoCao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("cmdBaoCao.Image")));
            this.cmdBaoCao.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.cmdBaoCao.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBaoCao.Location = new System.Drawing.Point(773, 687);
            this.cmdBaoCao.Name = "cmdBaoCao";
            this.cmdBaoCao.Size = new System.Drawing.Size(109, 31);
            this.cmdBaoCao.TabIndex = 7;
            this.cmdBaoCao.Text = "In báo cáo";
            this.cmdBaoCao.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Far;
            this.cmdBaoCao.Click += new System.EventHandler(this.cmdBaoCao_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(888, 687);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(109, 31);
            this.cmdExit.TabIndex = 9;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Far;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.chkPhieuvay);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtthuoc);
            this.uiGroupBox1.Controls.Add(this.txtLoaithuoc);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtNhacungcap);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboKho);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 72);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(997, 609);
            this.uiGroupBox1.TabIndex = 13;
            this.uiGroupBox1.Text = "&Thông tin báo cáo";
            // 
            // chkPhieuvay
            // 
            this.chkPhieuvay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPhieuvay.Location = new System.Drawing.Point(737, 86);
            this.chkPhieuvay.Name = "chkPhieuvay";
            this.chkPhieuvay.Size = new System.Drawing.Size(203, 23);
            this.chkPhieuvay.TabIndex = 482;
            this.chkPhieuvay.Text = "Thuốc vay bên ngoài?";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(392, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "đến ngày:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.SystemColors.Control;
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.AllowedSelectPrice = false;
            this.txtthuoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtthuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthuoc.AutoCompleteList")));
            this.txtthuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthuoc.CaseSensitive = false;
            this.txtthuoc.CompareNoID = true;
            this.txtthuoc.DefaultCode = "-1";
            this.txtthuoc.DefaultID = "-1";
            this.txtthuoc.Drug_ID = null;
            this.txtthuoc.ExtraWidth = 0;
            this.txtthuoc.ExtraWidth_Pre = 0;
            this.txtthuoc.FillValueAfterSelect = true;
            this.txtthuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.txtthuoc.GridView = false;
            this.txtthuoc.Location = new System.Drawing.Point(486, 58);
            this.txtthuoc.MaxHeight = 300;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = false;
            this.txtthuoc.RaiseEventEnter = false;
            this.txtthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(499, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 3;
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = null;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // txtLoaithuoc
            // 
            this.txtLoaithuoc._backcolor = System.Drawing.SystemColors.Control;
            this.txtLoaithuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaithuoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoaithuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoaithuoc.AutoCompleteList")));
            this.txtLoaithuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaithuoc.CaseSensitive = false;
            this.txtLoaithuoc.CompareNoID = true;
            this.txtLoaithuoc.DefaultCode = "-1";
            this.txtLoaithuoc.DefaultID = "-1";
            this.txtLoaithuoc.Drug_ID = null;
            this.txtLoaithuoc.ExtraWidth = 100;
            this.txtLoaithuoc.FillValueAfterSelect = false;
            this.txtLoaithuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.txtLoaithuoc.Location = new System.Drawing.Point(486, 31);
            this.txtLoaithuoc.MaxHeight = 300;
            this.txtLoaithuoc.MinTypedCharacters = 2;
            this.txtLoaithuoc.MyCode = null;
            this.txtLoaithuoc.MyID = null;
            this.txtLoaithuoc.Name = "txtLoaithuoc";
            this.txtLoaithuoc.RaiseEvent = true;
            this.txtLoaithuoc.RaiseEventEnter = true;
            this.txtLoaithuoc.RaiseEventEnterWhenEmpty = true;
            this.txtLoaithuoc.SelectedIndex = -1;
            this.txtLoaithuoc.Size = new System.Drawing.Size(499, 21);
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
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(392, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 37;
            this.label6.Text = "Tên thuốc";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(392, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "Nhóm thuốc";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNhacungcap
            // 
            this.txtNhacungcap._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNhacungcap._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhacungcap.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhacungcap.AutoCompleteList")));
            this.txtNhacungcap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhacungcap.CaseSensitive = false;
            this.txtNhacungcap.CompareNoID = true;
            this.txtNhacungcap.DefaultCode = "";
            this.txtNhacungcap.DefaultID = "-1";
            this.txtNhacungcap.Drug_ID = null;
            this.txtNhacungcap.ExtraWidth = 0;
            this.txtNhacungcap.FillValueAfterSelect = false;
            this.txtNhacungcap.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNhacungcap.LOAI_DANHMUC = "NHACUNGCAP";
            this.txtNhacungcap.Location = new System.Drawing.Point(147, 57);
            this.txtNhacungcap.MaxHeight = 300;
            this.txtNhacungcap.MinTypedCharacters = 2;
            this.txtNhacungcap.MyCode = "-1";
            this.txtNhacungcap.MyID = "-1";
            this.txtNhacungcap.Name = "txtNhacungcap";
            this.txtNhacungcap.RaiseEvent = false;
            this.txtNhacungcap.RaiseEventEnter = false;
            this.txtNhacungcap.RaiseEventEnterWhenEmpty = false;
            this.txtNhacungcap.SelectedIndex = -1;
            this.txtNhacungcap.Size = new System.Drawing.Size(239, 21);
            this.txtNhacungcap.splitChar = '@';
            this.txtNhacungcap.splitCharIDAndCode = '#';
            this.txtNhacungcap.TabIndex = 2;
            this.txtNhacungcap.TakeCode = false;
            this.txtNhacungcap.txtMyCode = null;
            this.txtNhacungcap.txtMyCode_Edit = null;
            this.txtNhacungcap.txtMyID = null;
            this.txtNhacungcap.txtMyID_Edit = null;
            this.txtNhacungcap.txtMyName = null;
            this.txtNhacungcap.txtMyName_Edit = null;
            this.txtNhacungcap.txtNext = null;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(38, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Nhà cung cấp";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(8, 119);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(978, 484);
            this.grdList.TabIndex = 10;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(38, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Kho nhập:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKho
            // 
            this.cboKho.Font = new System.Drawing.Font("Arial", 9F);
            this.cboKho.Location = new System.Drawing.Point(148, 30);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(238, 21);
            this.cboKho.TabIndex = 0;
            this.cboKho.Text = "Kho";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F);
            this.dtToDate.Location = new System.Drawing.Point(486, 86);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(235, 21);
            this.dtToDate.TabIndex = 6;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F);
            this.dtFromDate.Location = new System.Drawing.Point(148, 86);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(238, 21);
            this.dtFromDate.TabIndex = 5;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Font = new System.Drawing.Font("Arial", 9F);
            this.chkByDate.Location = new System.Drawing.Point(66, 87);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(75, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(656, 687);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(109, 31);
            this.cmdExportToExcel.TabIndex = 8;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Far;
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdList;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // frm_baocao_nhapthuoctheoNhacungcap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtNgayIn);
            this.Controls.Add(this.cmdBaoCao);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocao_nhapthuoctheoNhacungcap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO NHẬP KHO THEO NHÀ CUNG CẤP";
            this.Load += new System.EventHandler(this.frm_baocao_nhapthuoctheoNhacungcap_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayIn;
        private Janus.Windows.EditControls.UIButton cmdBaoCao;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboKho;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Danhmucchung txtNhacungcap;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox txtLoaithuoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UICheckBox chkPhieuvay;
    }
}