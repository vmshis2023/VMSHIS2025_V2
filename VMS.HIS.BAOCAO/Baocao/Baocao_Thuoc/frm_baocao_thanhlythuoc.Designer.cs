namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_baocao_thanhlythuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocao_thanhlythuoc));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdCboDownHTTT = new Janus.Windows.EditControls.UIButton();
            this.chkKieungaytimkiem = new Janus.Windows.EditControls.UICheckBox();
            this.txtLydothanhly = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtthuoc = new VNS.HIS.UCs.AutoCompleteTextbox_Thuoc();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.optChuaXacnhan = new System.Windows.Forms.RadioButton();
            this.optDaXacnhan = new System.Windows.Forms.RadioButton();
            this.optTatCa = new System.Windows.Forms.RadioButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKho = new Janus.Windows.EditControls.UIComboBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdBaoCao = new Janus.Windows.EditControls.UIButton();
            this.dtNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.panel1.Size = new System.Drawing.Size(1017, 66);
            this.panel1.TabIndex = 0;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BC_NHAPKHOCHITIET";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt: Ctrl+P=In, Ctrl+S(Hoặc F3)=Tìm kiếm, ESC=Thoát...";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1017, 54);
            this.baocaO_TIEUDE1.TabIndex = 1;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO THANH LÝ THUỐC";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.cmdCboDownHTTT);
            this.uiGroupBox1.Controls.Add(this.chkKieungaytimkiem);
            this.uiGroupBox1.Controls.Add(this.txtLydothanhly);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtthuoc);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.optChuaXacnhan);
            this.uiGroupBox1.Controls.Add(this.optDaXacnhan);
            this.uiGroupBox1.Controls.Add(this.optTatCa);
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboKho);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 66);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1017, 575);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin báo cáo";
            // 
            // cmdCboDownHTTT
            // 
            this.cmdCboDownHTTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCboDownHTTT.Image = ((System.Drawing.Image)(resources.GetObject("cmdCboDownHTTT.Image")));
            this.cmdCboDownHTTT.ImageSize = new System.Drawing.Size(12, 12);
            this.cmdCboDownHTTT.Location = new System.Drawing.Point(679, 75);
            this.cmdCboDownHTTT.Name = "cmdCboDownHTTT";
            this.cmdCboDownHTTT.Size = new System.Drawing.Size(23, 23);
            this.cmdCboDownHTTT.TabIndex = 588;
            this.cmdCboDownHTTT.Click += new System.EventHandler(this.cmdCboDownHTTT_Click);
            // 
            // chkKieungaytimkiem
            // 
            this.chkKieungaytimkiem.Location = new System.Drawing.Point(531, 105);
            this.chkKieungaytimkiem.Name = "chkKieungaytimkiem";
            this.chkKieungaytimkiem.Size = new System.Drawing.Size(169, 23);
            this.chkKieungaytimkiem.TabIndex = 4;
            this.chkKieungaytimkiem.Text = "Tìm theo ngày xác nhận?";
            this.toolTip1.SetToolTip(this.chkKieungaytimkiem, "Nếu bỏ chọn thì là tìm theo ngày lập");
            // 
            // txtLydothanhly
            // 
            this.txtLydothanhly._backcolor = System.Drawing.SystemColors.Control;
            this.txtLydothanhly._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydothanhly._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydothanhly.AddValues = true;
            this.txtLydothanhly.AllowMultiline = false;
            this.txtLydothanhly.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydothanhly.AutoCompleteList")));
            this.txtLydothanhly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydothanhly.buildShortcut = false;
            this.txtLydothanhly.CaseSensitive = false;
            this.txtLydothanhly.CompareNoID = true;
            this.txtLydothanhly.DefaultCode = "-1";
            this.txtLydothanhly.DefaultID = "-1";
            this.txtLydothanhly.Drug_ID = null;
            this.txtLydothanhly.ExtraWidth = 0;
            this.txtLydothanhly.FillValueAfterSelect = false;
            this.txtLydothanhly.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydothanhly.LOAI_DANHMUC = "LYDOTHANHLY";
            this.txtLydothanhly.Location = new System.Drawing.Point(133, 53);
            this.txtLydothanhly.MaxHeight = 300;
            this.txtLydothanhly.MinTypedCharacters = 2;
            this.txtLydothanhly.MyCode = "-1";
            this.txtLydothanhly.MyID = "-1";
            this.txtLydothanhly.Name = "txtLydothanhly";
            this.txtLydothanhly.RaiseEvent = false;
            this.txtLydothanhly.RaiseEventEnter = false;
            this.txtLydothanhly.RaiseEventEnterWhenEmpty = false;
            this.txtLydothanhly.SelectedIndex = -1;
            this.txtLydothanhly.ShowCodeWithValue = false;
            this.txtLydothanhly.Size = new System.Drawing.Size(546, 21);
            this.txtLydothanhly.splitChar = '@';
            this.txtLydothanhly.splitCharIDAndCode = '#';
            this.txtLydothanhly.TabIndex = 1;
            this.txtLydothanhly.TakeCode = false;
            this.txtLydothanhly.txtMyCode = null;
            this.txtLydothanhly.txtMyCode_Edit = null;
            this.txtLydothanhly.txtMyID = null;
            this.txtLydothanhly.txtMyID_Edit = null;
            this.txtLydothanhly.txtMyName = null;
            this.txtLydothanhly.txtMyName_Edit = null;
            this.txtLydothanhly.txtNext = null;
            this.txtLydothanhly.txtNext1 = null;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(39, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 66;
            this.label5.Text = "Lý do thanh lý";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(39, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "Trạng thái:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.SystemColors.Control;
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.AllowedSelectPrice = false;
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
            this.txtthuoc.Location = new System.Drawing.Point(133, 76);
            this.txtthuoc.MaxHeight = 300;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.MyText = "";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = false;
            this.txtthuoc.RaiseEventEnter = false;
            this.txtthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(546, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 2;
            this.txtthuoc.TakeCode = false;
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = null;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(39, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Thuốc thanh lý";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "đến ngày";
            // 
            // optChuaXacnhan
            // 
            this.optChuaXacnhan.AutoSize = true;
            this.optChuaXacnhan.Location = new System.Drawing.Point(417, 136);
            this.optChuaXacnhan.Name = "optChuaXacnhan";
            this.optChuaXacnhan.Size = new System.Drawing.Size(130, 19);
            this.optChuaXacnhan.TabIndex = 8;
            this.optChuaXacnhan.TabStop = true;
            this.optChuaXacnhan.Text = "Chưa xác nhận hủy";
            this.optChuaXacnhan.UseVisualStyleBackColor = true;
            // 
            // optDaXacnhan
            // 
            this.optDaXacnhan.AutoSize = true;
            this.optDaXacnhan.Location = new System.Drawing.Point(225, 136);
            this.optDaXacnhan.Name = "optDaXacnhan";
            this.optDaXacnhan.Size = new System.Drawing.Size(115, 19);
            this.optDaXacnhan.TabIndex = 7;
            this.optDaXacnhan.TabStop = true;
            this.optDaXacnhan.Text = "Đã xác nhận hủy";
            this.optDaXacnhan.UseVisualStyleBackColor = true;
            // 
            // optTatCa
            // 
            this.optTatCa.AutoSize = true;
            this.optTatCa.Checked = true;
            this.optTatCa.Location = new System.Drawing.Point(133, 135);
            this.optTatCa.Name = "optTatCa";
            this.optTatCa.Size = new System.Drawing.Size(58, 19);
            this.optTatCa.TabIndex = 6;
            this.optTatCa.TabStop = true;
            this.optTatCa.Text = "Tất cả";
            this.optTatCa.UseVisualStyleBackColor = true;
            // 
            // grdList
            // 
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(8, 170);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1003, 399);
            this.grdList.TabIndex = 12;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Kho thanh lý";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKho
            // 
            this.cboKho.Location = new System.Drawing.Point(133, 30);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(546, 21);
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
            this.dtToDate.Location = new System.Drawing.Point(345, 105);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(180, 21);
            this.dtToDate.TabIndex = 4;
            this.dtToDate.Value = new System.DateTime(2014, 9, 15, 0, 0, 0, 0);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(133, 105);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(140, 21);
            this.dtFromDate.TabIndex = 3;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 15, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(42, 105);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(85, 23);
            this.chkByDate.TabIndex = 3;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = global::VMS.HIS.BAOCAO.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(881, 647);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(130, 32);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát chức năng";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdBaoCao
            // 
            this.cmdBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBaoCao.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("cmdBaoCao.Image")));
            this.cmdBaoCao.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBaoCao.Location = new System.Drawing.Point(745, 647);
            this.cmdBaoCao.Name = "cmdBaoCao";
            this.cmdBaoCao.Size = new System.Drawing.Size(130, 32);
            this.cmdBaoCao.TabIndex = 9;
            this.cmdBaoCao.Text = "In báo cáo";
            this.cmdBaoCao.Click += new System.EventHandler(this.cmdBaoCao_Click);
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
            this.dtNgayIn.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayIn.Location = new System.Drawing.Point(59, 658);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(134, 21);
            this.dtNgayIn.TabIndex = 13;
            this.dtNgayIn.Value = new System.DateTime(2014, 9, 15, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(5, 660);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày in";
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(609, 649);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(130, 30);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
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
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_baocao_thanhlythuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1017, 691);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtNgayIn);
            this.Controls.Add(this.cmdBaoCao);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocao_thanhlythuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhập kho chi tiết";
            this.Load += new System.EventHandler(this.frm_baocao_thanhlythuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocao_thanhlythuoc_KeyDown);
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
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdBaoCao;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayIn;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboKho;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.RadioButton optChuaXacnhan;
        private System.Windows.Forms.RadioButton optDaXacnhan;
        private System.Windows.Forms.RadioButton optTatCa;
        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.Label label6;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLydothanhly;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UICheckBox chkKieungaytimkiem;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIButton cmdCboDownHTTT;
    }
}