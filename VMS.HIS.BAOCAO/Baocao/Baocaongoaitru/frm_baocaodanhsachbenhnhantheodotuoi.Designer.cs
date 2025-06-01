namespace VNS.HIS.UI.Baocao
{
    partial class frm_baocaodanhsachbenhnhantheodotuoi
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
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocaodanhsachbenhnhantheodotuoi));
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cboSoSanh = new Janus.Windows.EditControls.UIGroupBox();
            this.cboEqual = new System.Windows.Forms.ComboBox();
            this.txtTuTuoi = new System.Windows.Forms.TextBox();
            this.cboGT = new Janus.Windows.EditControls.UIComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDenTuoi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDoTuoi = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboSoSanh)).BeginInit();
            this.cboSoSanh.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
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
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(81, 530);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 11;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(3, 534);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 88;
            this.label3.Text = "Ngày in";
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
            // cboSoSanh
            // 
            this.cboSoSanh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSoSanh.Controls.Add(this.cboEqual);
            this.cboSoSanh.Controls.Add(this.txtTuTuoi);
            this.cboSoSanh.Controls.Add(this.cboGT);
            this.cboSoSanh.Controls.Add(this.label6);
            this.cboSoSanh.Controls.Add(this.txtDenTuoi);
            this.cboSoSanh.Controls.Add(this.label5);
            this.cboSoSanh.Controls.Add(this.chkDoTuoi);
            this.cboSoSanh.Controls.Add(this.label2);
            this.cboSoSanh.Controls.Add(this.panel1);
            this.cboSoSanh.Controls.Add(this.cboKhoa);
            this.cboSoSanh.Controls.Add(this.label4);
            this.cboSoSanh.Controls.Add(this.cboDoituongKCB);
            this.cboSoSanh.Controls.Add(this.label1);
            this.cboSoSanh.Controls.Add(this.dtToDate);
            this.cboSoSanh.Controls.Add(this.dtFromDate);
            this.cboSoSanh.Controls.Add(this.chkByDate);
            this.cboSoSanh.Font = new System.Drawing.Font("Arial", 9F);
            this.cboSoSanh.Image = ((System.Drawing.Image)(resources.GetObject("cboSoSanh.Image")));
            this.cboSoSanh.Location = new System.Drawing.Point(0, 59);
            this.cboSoSanh.Name = "cboSoSanh";
            this.cboSoSanh.Size = new System.Drawing.Size(784, 465);
            this.cboSoSanh.TabIndex = 116;
            this.cboSoSanh.Text = "Thông tin tìm kiếm";
            // 
            // cboEqual
            // 
            this.cboEqual.FormattingEnabled = true;
            this.cboEqual.Items.AddRange(new object[] {
            "---- Chọn so sách -----",
            "     Bằng(=)",
            "     Lớn hơn(>=)",
            "     Nhỏ hơn(<=)"});
            this.cboEqual.Location = new System.Drawing.Point(480, 53);
            this.cboEqual.Name = "cboEqual";
            this.cboEqual.Size = new System.Drawing.Size(95, 23);
            this.cboEqual.TabIndex = 124;
            this.cboEqual.Visible = false;
            // 
            // txtTuTuoi
            // 
            this.txtTuTuoi.Location = new System.Drawing.Point(483, 56);
            this.txtTuTuoi.Name = "txtTuTuoi";
            this.txtTuTuoi.Size = new System.Drawing.Size(92, 21);
            this.txtTuTuoi.TabIndex = 127;
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
            this.cboGT.Location = new System.Drawing.Point(642, 80);
            this.cboGT.Name = "cboGT";
            this.cboGT.SelectedIndex = 0;
            this.cboGT.SelectInDataSource = true;
            this.cboGT.Size = new System.Drawing.Size(92, 21);
            this.cboGT.TabIndex = 126;
            this.cboGT.Text = "Giới tính";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(557, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 22);
            this.label6.TabIndex = 125;
            this.label6.Text = "Giới tính: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDenTuoi
            // 
            this.txtDenTuoi.Location = new System.Drawing.Point(642, 56);
            this.txtDenTuoi.Name = "txtDenTuoi";
            this.txtDenTuoi.Size = new System.Drawing.Size(92, 21);
            this.txtDenTuoi.TabIndex = 123;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(581, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 122;
            this.label5.Text = "------->";
            // 
            // chkDoTuoi
            // 
            this.chkDoTuoi.Checked = true;
            this.chkDoTuoi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoTuoi.Location = new System.Drawing.Point(388, 54);
            this.chkDoTuoi.Name = "chkDoTuoi";
            this.chkDoTuoi.Size = new System.Drawing.Size(95, 23);
            this.chkDoTuoi.TabIndex = 48;
            this.chkDoTuoi.Text = "Từ khoảng";
            this.chkDoTuoi.CheckedChanged += new System.EventHandler(this.chkDoTuoi_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(329, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.TabIndex = 47;
            this.label2.Text = "Tuổi";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Location = new System.Drawing.Point(6, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 336);
            this.panel1.TabIndex = 46;
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
            this.grdChitiet.Size = new System.Drawing.Size(772, 336);
            this.grdChitiet.TabIndex = 21;
            this.grdChitiet.TabStop = false;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(123, 27);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(611, 21);
            this.cboKhoa.TabIndex = 0;
            this.cboKhoa.Text = "Khoa thực hiện";
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
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(331, 81);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 21);
            this.dtToDate.TabIndex = 6;
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
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(123, 81);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(200, 21);
            this.dtFromDate.TabIndex = 5;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(48, 82);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.Text = "Từ ngày";
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "baocao_thongkebn_theodotuoi_ct";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(784, 53);
            this.baocaO_TIEUDE1.TabIndex = 115;
            this.baocaO_TIEUDE1.TIEUDE = "THỐNG KÊ BỆNH NHÂN THEO ĐỘ TUỔI";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(364, 530);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 9;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuXN.Location = new System.Drawing.Point(503, 530);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieuXN.TabIndex = 8;
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
            this.cmdExit.Location = new System.Drawing.Point(642, 530);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Thoát (Esc)";
            // 
            // frm_baocaodanhsachbenhnhantheodotuoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cboSoSanh);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.cmdExit);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocaodanhsachbenhnhantheodotuoi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO TÌNH HÌNH TIẾP ĐÓN BỆNH NHÂN";
            this.Load += new System.EventHandler(this.frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboSoSanh)).EndInit();
            this.cboSoSanh.ResumeLayout(false);
            this.cboSoSanh.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
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
        private Janus.Windows.EditControls.UIGroupBox cboSoSanh;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UICheckBox chkDoTuoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboEqual;
        private System.Windows.Forms.TextBox txtDenTuoi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UIComboBox cboGT;
        private System.Windows.Forms.TextBox txtTuTuoi;
    }
}