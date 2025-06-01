namespace VNS.HIS.UI.Baocao
{
    partial class frm_baocaodoanhthuphongkham_bvsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocaodoanhthuphongkham_bvsg));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.gridEXExporter2 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiButton1 = new Janus.Windows.EditControls.UIButton();
            this.uiButton2 = new Janus.Windows.EditControls.UIButton();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optTonghop = new System.Windows.Forms.RadioButton();
            this.optChitiet = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLoaiDV = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtDvu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.autoNganhang = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPttt = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cboReportType = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkTachCDHA = new Janus.Windows.EditControls.UICheckBox();
            this.cboLoaiDieutri = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboNhanvien = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(81, 697);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 11;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(3, 701);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 88;
            this.label3.Text = "Ngày in";
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdList;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(996, 375);
            this.grdList.TabIndex = 21;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.uiButton1);
            this.uiGroupBox2.Controls.Add(this.uiButton2);
            this.uiGroupBox2.Controls.Add(this.label11);
            this.uiGroupBox2.Controls.Add(this.panel2);
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.txtLoaiDV);
            this.uiGroupBox2.Controls.Add(this.txtDvu);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.label27);
            this.uiGroupBox2.Controls.Add(this.autoNganhang);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.txtPttt);
            this.uiGroupBox2.Controls.Add(this.cboReportType);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.chkTachCDHA);
            this.uiGroupBox2.Controls.Add(this.cboLoaiDieutri);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.panel1);
            this.uiGroupBox2.Controls.Add(this.cboKhoa);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.cboNhanvien);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 59);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 632);
            this.uiGroupBox2.TabIndex = 116;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // uiButton1
            // 
            this.uiButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton1.Image = ((System.Drawing.Image)(resources.GetObject("uiButton1.Image")));
            this.uiButton1.ImageSize = new System.Drawing.Size(12, 12);
            this.uiButton1.Location = new System.Drawing.Point(756, 106);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(23, 23);
            this.uiButton1.TabIndex = 596;
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiButton2
            // 
            this.uiButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton2.Image = ((System.Drawing.Image)(resources.GetObject("uiButton2.Image")));
            this.uiButton2.ImageSize = new System.Drawing.Size(12, 12);
            this.uiButton2.Location = new System.Drawing.Point(323, 106);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(23, 23);
            this.uiButton2.TabIndex = 595;
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 23);
            this.label11.TabIndex = 406;
            this.label11.Text = "Loại báo cáo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optTonghop);
            this.panel2.Controls.Add(this.optChitiet);
            this.panel2.Location = new System.Drawing.Point(123, 181);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 22);
            this.panel2.TabIndex = 405;
            // 
            // optTonghop
            // 
            this.optTonghop.AutoSize = true;
            this.optTonghop.Location = new System.Drawing.Point(3, 3);
            this.optTonghop.Name = "optTonghop";
            this.optTonghop.Size = new System.Drawing.Size(159, 19);
            this.optTonghop.TabIndex = 403;
            this.optTonghop.Text = "Tổng hợp theo lần khám";
            this.optTonghop.UseVisualStyleBackColor = true;
            // 
            // optChitiet
            // 
            this.optChitiet.AutoSize = true;
            this.optChitiet.Checked = true;
            this.optChitiet.Location = new System.Drawing.Point(190, 3);
            this.optChitiet.Name = "optChitiet";
            this.optChitiet.Size = new System.Drawing.Size(131, 19);
            this.optChitiet.TabIndex = 404;
            this.optChitiet.TabStop = true;
            this.optChitiet.Text = "Chi tiết theo dịch vụ";
            this.optChitiet.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(989, 33);
            this.label6.TabIndex = 402;
            this.label6.Text = "Tổng tiền=Tổng chi phí dịch vụ phát sinh + Thu khác + Tạm ứng - hủy tạm ứng - hoà" +
    "n ứng + hủy hoàn ứng - Miễn giảm theo thanh toán - Miễn giảm khác - Chi khác -ti" +
    "ền trả lại";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLoaiDV
            // 
            this.txtLoaiDV._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLoaiDV._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiDV._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLoaiDV.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoaiDV.AutoCompleteList")));
            this.txtLoaiDV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaiDV.buildShortcut = false;
            this.txtLoaiDV.CaseSensitive = false;
            this.txtLoaiDV.CompareNoID = true;
            this.txtLoaiDV.DefaultCode = "-1";
            this.txtLoaiDV.DefaultID = "-1";
            this.txtLoaiDV.DisplayType = 0;
            this.txtLoaiDV.Drug_ID = null;
            this.txtLoaiDV.ExtraWidth = 0;
            this.txtLoaiDV.FillValueAfterSelect = false;
            this.txtLoaiDV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiDV.Location = new System.Drawing.Point(123, 129);
            this.txtLoaiDV.MaxHeight = 289;
            this.txtLoaiDV.MinTypedCharacters = 2;
            this.txtLoaiDV.MyCode = "-1";
            this.txtLoaiDV.MyID = "-1";
            this.txtLoaiDV.MyText = "";
            this.txtLoaiDV.MyTextOnly = "";
            this.txtLoaiDV.Name = "txtLoaiDV";
            this.txtLoaiDV.RaiseEvent = false;
            this.txtLoaiDV.RaiseEventEnter = true;
            this.txtLoaiDV.RaiseEventEnterWhenEmpty = true;
            this.txtLoaiDV.SelectedIndex = -1;
            this.txtLoaiDV.Size = new System.Drawing.Size(200, 21);
            this.txtLoaiDV.splitChar = '@';
            this.txtLoaiDV.splitCharIDAndCode = '#';
            this.txtLoaiDV.TabIndex = 401;
            this.txtLoaiDV.TakeCode = false;
            this.txtLoaiDV.txtMyCode = null;
            this.txtLoaiDV.txtMyCode_Edit = null;
            this.txtLoaiDV.txtMyID = null;
            this.txtLoaiDV.txtMyID_Edit = null;
            this.txtLoaiDV.txtMyName = null;
            this.txtLoaiDV.txtMyName_Edit = null;
            this.txtLoaiDV.txtNext = null;
            // 
            // txtDvu
            // 
            this.txtDvu._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtDvu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDvu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDvu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDvu.AutoCompleteList")));
            this.txtDvu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDvu.buildShortcut = false;
            this.txtDvu.CaseSensitive = false;
            this.txtDvu.CompareNoID = true;
            this.txtDvu.DefaultCode = "-1";
            this.txtDvu.DefaultID = "-1";
            this.txtDvu.DisplayType = 0;
            this.txtDvu.Drug_ID = null;
            this.txtDvu.ExtraWidth = 0;
            this.txtDvu.FillValueAfterSelect = false;
            this.txtDvu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDvu.Location = new System.Drawing.Point(456, 131);
            this.txtDvu.MaxHeight = 289;
            this.txtDvu.MinTypedCharacters = 2;
            this.txtDvu.MyCode = "-1";
            this.txtDvu.MyID = "-1";
            this.txtDvu.MyText = "";
            this.txtDvu.MyTextOnly = "";
            this.txtDvu.Name = "txtDvu";
            this.txtDvu.RaiseEvent = false;
            this.txtDvu.RaiseEventEnter = true;
            this.txtDvu.RaiseEventEnterWhenEmpty = true;
            this.txtDvu.SelectedIndex = -1;
            this.txtDvu.Size = new System.Drawing.Size(299, 21);
            this.txtDvu.splitChar = '@';
            this.txtDvu.splitCharIDAndCode = '#';
            this.txtDvu.TabIndex = 400;
            this.txtDvu.TakeCode = false;
            this.txtDvu.txtMyCode = null;
            this.txtDvu.txtMyCode_Edit = null;
            this.txtDvu.txtMyID = null;
            this.txtDvu.txtMyID_Edit = null;
            this.txtDvu.txtMyName = null;
            this.txtDvu.txtMyName_Edit = null;
            this.txtDvu.txtNext = null;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(348, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 23);
            this.label9.TabIndex = 399;
            this.label9.Text = "Dịch vụ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 23);
            this.label10.TabIndex = 398;
            this.label10.Text = "Loại dịch vụ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(364, 106);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(88, 24);
            this.label27.TabIndex = 395;
            this.label27.Text = "Ngân hàng";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoNganhang
            // 
            this.autoNganhang._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoNganhang._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNganhang._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoNganhang.AddValues = true;
            this.autoNganhang.AllowMultiline = false;
            this.autoNganhang.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoNganhang.AutoCompleteList")));
            this.autoNganhang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoNganhang.buildShortcut = false;
            this.autoNganhang.CaseSensitive = false;
            this.autoNganhang.CompareNoID = true;
            this.autoNganhang.DefaultCode = "-1";
            this.autoNganhang.DefaultID = "-1";
            this.autoNganhang.Drug_ID = null;
            this.autoNganhang.Enabled = false;
            this.autoNganhang.ExtraWidth = 0;
            this.autoNganhang.FillValueAfterSelect = false;
            this.autoNganhang.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNganhang.LOAI_DANHMUC = "NGANHANG";
            this.autoNganhang.Location = new System.Drawing.Point(456, 107);
            this.autoNganhang.MaxHeight = 150;
            this.autoNganhang.MinTypedCharacters = 2;
            this.autoNganhang.MyCode = "-1";
            this.autoNganhang.MyID = "-1";
            this.autoNganhang.Name = "autoNganhang";
            this.autoNganhang.RaiseEvent = false;
            this.autoNganhang.RaiseEventEnter = false;
            this.autoNganhang.RaiseEventEnterWhenEmpty = false;
            this.autoNganhang.SelectedIndex = -1;
            this.autoNganhang.ShowCodeWithValue = false;
            this.autoNganhang.Size = new System.Drawing.Size(299, 21);
            this.autoNganhang.splitChar = '@';
            this.autoNganhang.splitCharIDAndCode = '#';
            this.autoNganhang.TabIndex = 394;
            this.autoNganhang.TakeCode = false;
            this.autoNganhang.txtMyCode = null;
            this.autoNganhang.txtMyCode_Edit = null;
            this.autoNganhang.txtMyID = null;
            this.autoNganhang.txtMyID_Edit = null;
            this.autoNganhang.txtMyName = null;
            this.autoNganhang.txtMyName_Edit = null;
            this.autoNganhang.txtNext = null;
            this.autoNganhang.txtNext1 = null;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(-24, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 24);
            this.label7.TabIndex = 393;
            this.label7.Text = "H.thức T.Toán:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPttt
            // 
            this.txtPttt._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPttt._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPttt._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPttt.AddValues = true;
            this.txtPttt.AllowMultiline = false;
            this.txtPttt.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPttt.AutoCompleteList")));
            this.txtPttt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPttt.buildShortcut = false;
            this.txtPttt.CaseSensitive = false;
            this.txtPttt.CompareNoID = true;
            this.txtPttt.DefaultCode = "-1";
            this.txtPttt.DefaultID = "-1";
            this.txtPttt.Drug_ID = null;
            this.txtPttt.ExtraWidth = 0;
            this.txtPttt.FillValueAfterSelect = false;
            this.txtPttt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPttt.LOAI_DANHMUC = "PHUONGTHUCTHANHTOAN";
            this.txtPttt.Location = new System.Drawing.Point(123, 106);
            this.txtPttt.MaxHeight = 150;
            this.txtPttt.MinTypedCharacters = 2;
            this.txtPttt.MyCode = "-1";
            this.txtPttt.MyID = "-1";
            this.txtPttt.Name = "txtPttt";
            this.txtPttt.RaiseEvent = false;
            this.txtPttt.RaiseEventEnter = true;
            this.txtPttt.RaiseEventEnterWhenEmpty = false;
            this.txtPttt.SelectedIndex = -1;
            this.txtPttt.ShowCodeWithValue = false;
            this.txtPttt.Size = new System.Drawing.Size(200, 21);
            this.txtPttt.splitChar = '@';
            this.txtPttt.splitCharIDAndCode = '#';
            this.txtPttt.TabIndex = 392;
            this.txtPttt.TakeCode = false;
            this.txtPttt.txtMyCode = null;
            this.txtPttt.txtMyCode_Edit = null;
            this.txtPttt.txtMyID = null;
            this.txtPttt.txtMyID_Edit = null;
            this.txtPttt.txtMyName = null;
            this.txtPttt.txtMyName_Edit = null;
            this.txtPttt.txtNext = null;
            this.txtPttt.txtNext1 = null;
            // 
            // cboReportType
            // 
            this.cboReportType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Mẫu EBM";
            uiComboBoxItem1.Value = "1";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Mẫu khác";
            uiComboBoxItem2.Value = "2";
            this.cboReportType.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboReportType.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboReportType.Location = new System.Drawing.Point(456, 81);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.SelectInDataSource = true;
            this.cboReportType.Size = new System.Drawing.Size(299, 21);
            this.cboReportType.TabIndex = 61;
            this.cboReportType.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.uiComboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(346, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 15);
            this.label5.TabIndex = 62;
            this.label5.Text = "Kiểu báo cáo:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTachCDHA
            // 
            this.chkTachCDHA.Location = new System.Drawing.Point(537, 157);
            this.chkTachCDHA.Name = "chkTachCDHA";
            this.chkTachCDHA.Size = new System.Drawing.Size(209, 23);
            this.chkTachCDHA.TabIndex = 60;
            this.chkTachCDHA.Text = "Tách tiền CĐHA?";
            this.chkTachCDHA.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // cboLoaiDieutri
            // 
            this.cboLoaiDieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Tất cả";
            uiComboBoxItem3.Value = ((short)(100));
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Ngoại trú";
            uiComboBoxItem4.Value = ((short)(0));
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "Nội trú";
            uiComboBoxItem5.Value = ((short)(1));
            this.cboLoaiDieutri.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4,
            uiComboBoxItem5});
            this.cboLoaiDieutri.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboLoaiDieutri.Location = new System.Drawing.Point(456, 54);
            this.cboLoaiDieutri.Name = "cboLoaiDieutri";
            this.cboLoaiDieutri.SelectInDataSource = true;
            this.cboLoaiDieutri.Size = new System.Drawing.Size(299, 21);
            this.cboLoaiDieutri.TabIndex = 2;
            this.cboLoaiDieutri.Text = "Chọn loại chi phí";
            this.cboLoaiDieutri.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(343, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "Loại điều trị:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(6, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(996, 375);
            this.panel1.TabIndex = 46;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(123, 27);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(632, 21);
            this.cboKhoa.TabIndex = 0;
            this.cboKhoa.Text = "Khoa thực hiện";
            this.cboKhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 44;
            this.label4.Text = "Khoa KCB";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhanvien
            // 
            this.cboNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhanvien.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboNhanvien.Location = new System.Drawing.Point(123, 81);
            this.cboNhanvien.Name = "cboNhanvien";
            this.cboNhanvien.SelectInDataSource = true;
            this.cboNhanvien.Size = new System.Drawing.Size(200, 21);
            this.cboNhanvien.TabIndex = 3;
            this.cboNhanvien.Text = "Nhân viên";
            this.cboNhanvien.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(13, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 30;
            this.label8.Text = "Thu ngân viên:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(123, 54);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(200, 21);
            this.cboDoituongKCB.TabIndex = 1;
            this.cboDoituongKCB.Text = "Đối tượng";
            this.cboDoituongKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 56);
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
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(331, 156);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 21);
            this.dtToDate.TabIndex = 6;
            this.dtToDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
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
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(123, 156);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(200, 21);
            this.dtFromDate.TabIndex = 5;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(48, 157);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1008, 53);
            this.baocaO_TIEUDE1.TabIndex = 115;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO DOANH THU PHÒNG KHÁM";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(591, 697);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 9;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuXN.Location = new System.Drawing.Point(730, 697);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieuXN.TabIndex = 8;
            this.cmdInPhieuXN.Text = "In báo cáo";
            this.cmdInPhieuXN.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieuXN.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = global::VMS.HIS.BAOCAO.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(869, 697);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // frm_baocaodoanhthuphongkham_bvsg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.cmdExit);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocaodoanhthuphongkham_bvsg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THU TIỀN DỊCH VỤ KCB";
            this.Load += new System.EventHandler(this.frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboNhanvien;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIComboBox cboLoaiDieutri;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UICheckBox chkTachCDHA;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter2;
        private Janus.Windows.EditControls.UIComboBox cboReportType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label27;
        private UCs.AutoCompleteTextbox_Danhmucchung autoNganhang;
        private System.Windows.Forms.Label label7;
        private UCs.AutoCompleteTextbox_Danhmucchung txtPttt;
        private UCs.AutoCompleteTextbox txtLoaiDV;
        private UCs.AutoCompleteTextbox txtDvu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optTonghop;
        private System.Windows.Forms.RadioButton optChitiet;
        private Janus.Windows.EditControls.UIButton uiButton1;
        private Janus.Windows.EditControls.UIButton uiButton2;
    }
}