namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_baocao_xuatvacxin_tuyenhuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocao_xuatvacxin_tuyenhuyen));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkKieungaytimkiem = new Janus.Windows.EditControls.UICheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtthuoc = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoChuaXacNhan = new System.Windows.Forms.RadioButton();
            this.rdoDaXacNhan = new System.Windows.Forms.RadioButton();
            this.rdoTatCa = new System.Windows.Forms.RadioButton();
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
            this.txtKho = new VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox();
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
            this.panel1.Size = new System.Drawing.Size(788, 66);
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(788, 54);
            this.baocaO_TIEUDE1.TabIndex = 1;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO NHẬP KHO CHI TIẾT";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtKho);
            this.uiGroupBox1.Controls.Add(this.chkKieungaytimkiem);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtthuoc);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.rdoChuaXacNhan);
            this.uiGroupBox1.Controls.Add(this.rdoDaXacNhan);
            this.uiGroupBox1.Controls.Add(this.rdoTatCa);
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboKho);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 66);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(788, 453);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin báo cáo";
            // 
            // chkKieungaytimkiem
            // 
            this.chkKieungaytimkiem.Location = new System.Drawing.Point(531, 87);
            this.chkKieungaytimkiem.Name = "chkKieungaytimkiem";
            this.chkKieungaytimkiem.Size = new System.Drawing.Size(169, 23);
            this.chkKieungaytimkiem.TabIndex = 5;
            this.chkKieungaytimkiem.TabStop = false;
            this.chkKieungaytimkiem.Text = "Tìm theo ngày xác nhận?";
            this.toolTip1.SetToolTip(this.chkKieungaytimkiem, "Nếu bỏ chọn thì là tìm theo ngày lập");
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(39, 119);
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
            this.txtthuoc.Location = new System.Drawing.Point(133, 55);
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
            this.txtthuoc.TabIndex = 1;
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
            this.label6.Location = new System.Drawing.Point(39, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Tên thuốc";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "đến ngày";
            // 
            // rdoChuaXacNhan
            // 
            this.rdoChuaXacNhan.AutoSize = true;
            this.rdoChuaXacNhan.Location = new System.Drawing.Point(417, 118);
            this.rdoChuaXacNhan.Name = "rdoChuaXacNhan";
            this.rdoChuaXacNhan.Size = new System.Drawing.Size(108, 19);
            this.rdoChuaXacNhan.TabIndex = 8;
            this.rdoChuaXacNhan.TabStop = true;
            this.rdoChuaXacNhan.Text = "Chưa xác nhận";
            this.rdoChuaXacNhan.UseVisualStyleBackColor = true;
            // 
            // rdoDaXacNhan
            // 
            this.rdoDaXacNhan.AutoSize = true;
            this.rdoDaXacNhan.Location = new System.Drawing.Point(225, 118);
            this.rdoDaXacNhan.Name = "rdoDaXacNhan";
            this.rdoDaXacNhan.Size = new System.Drawing.Size(147, 19);
            this.rdoDaXacNhan.TabIndex = 7;
            this.rdoDaXacNhan.TabStop = true;
            this.rdoDaXacNhan.Text = "Đã xác nhận nhập kho";
            this.rdoDaXacNhan.UseVisualStyleBackColor = true;
            // 
            // rdoTatCa
            // 
            this.rdoTatCa.AutoSize = true;
            this.rdoTatCa.Checked = true;
            this.rdoTatCa.Location = new System.Drawing.Point(133, 117);
            this.rdoTatCa.Name = "rdoTatCa";
            this.rdoTatCa.Size = new System.Drawing.Size(58, 19);
            this.rdoTatCa.TabIndex = 6;
            this.rdoTatCa.TabStop = true;
            this.rdoTatCa.Text = "Tất cả";
            this.rdoTatCa.UseVisualStyleBackColor = true;
            // 
            // grdList
            // 
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(8, 174);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(780, 273);
            this.grdList.TabIndex = 12;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chọn kho";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKho
            // 
            this.cboKho.Location = new System.Drawing.Point(723, 33);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(10, 21);
            this.cboKho.TabIndex = 0;
            this.cboKho.Text = "Kho";
            this.cboKho.Visible = false;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(345, 87);
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
            this.dtFromDate.Location = new System.Drawing.Point(133, 87);
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
            this.chkByDate.Location = new System.Drawing.Point(42, 87);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(92, 23);
            this.chkByDate.TabIndex = 3;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(603, 525);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(130, 32);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát chức năng";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdBaoCao
            // 
            this.cmdBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdBaoCao.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("cmdBaoCao.Image")));
            this.cmdBaoCao.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBaoCao.Location = new System.Drawing.Point(467, 525);
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
            this.dtNgayIn.Location = new System.Drawing.Point(59, 536);
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
            this.label1.Location = new System.Drawing.Point(5, 538);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày in";
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(331, 527);
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
            // txtKho
            // 
            this.txtKho._backcolor = System.Drawing.SystemColors.Control;
            this.txtKho._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKho._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKho.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKho.AutoCompleteList")));
            this.txtKho.CaseSensitive = false;
            this.txtKho.CompareNoID = true;
            this.txtKho.DefaultCode = "-1";
            this.txtKho.DefaultID = "-1";
            this.txtKho.Drug_ID = null;
            this.txtKho.ExtraWidth = 0;
            this.txtKho.FillValueAfterSelect = false;
            this.txtKho.Location = new System.Drawing.Point(133, 30);
            this.txtKho.MaxHeight = 250;
            this.txtKho.MinTypedCharacters = 2;
            this.txtKho.MyCode = "-1";
            this.txtKho.MyID = "-1";
            this.txtKho.MyText = "";
            this.txtKho.Name = "txtKho";
            this.txtKho.RaiseEvent = false;
            this.txtKho.RaiseEventEnter = false;
            this.txtKho.RaiseEventEnterWhenEmpty = false;
            this.txtKho.SelectedIndex = -1;
            this.txtKho.Size = new System.Drawing.Size(546, 21);
            this.txtKho.splitChar = '@';
            this.txtKho.splitCharIDAndCode = '#';
            this.txtKho.TabIndex = 124;
            this.txtKho.TakeCode = false;
            this.txtKho.txtMyCode = null;
            this.txtKho.txtMyCode_Edit = null;
            this.txtKho.txtMyID = null;
            this.txtKho.txtMyID_Edit = null;
            this.txtKho.txtMyName = null;
            this.txtKho.txtMyName_Edit = null;
            this.txtKho.txtNext = null;
            // 
            // frm_baocao_xuatvacxin_tuyenhuyen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(788, 569);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtNgayIn);
            this.Controls.Add(this.cmdBaoCao);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocao_xuatvacxin_tuyenhuyen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhập kho chi tiết";
            this.Load += new System.EventHandler(this.frm_baocao_xuatvacxin_tuyenhuyen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocao_xuatvacxin_tuyenhuyen_KeyDown);
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
        private System.Windows.Forms.RadioButton rdoChuaXacNhan;
        private System.Windows.Forms.RadioButton rdoDaXacNhan;
        private System.Windows.Forms.RadioButton rdoTatCa;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UICheckBox chkKieungaytimkiem;
        private VNS.HIS.UI.DANHMUC.UCs.AutoCompleteTextbox txtKho;
    }
}