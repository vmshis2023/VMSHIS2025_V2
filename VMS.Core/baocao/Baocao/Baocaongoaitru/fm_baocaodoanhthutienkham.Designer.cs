namespace VNS.HIS.UI.Baocao
{
    partial class fm_baocaodoanhthutienkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_baocaodoanhthutienkham));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboLoaidichvu = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkChitiet = new Janus.Windows.EditControls.UICheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboNhanvien = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            this.SuspendLayout();
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
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(784, 53);
            this.baocaO_TIEUDE1.TabIndex = 115;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO THU TIỀN DỊCH VỤ KCB";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.cboLoaidichvu);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.chkChitiet);
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
            this.uiGroupBox2.Size = new System.Drawing.Size(784, 465);
            this.uiGroupBox2.TabIndex = 116;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // cboLoaidichvu
            // 
            this.cboLoaidichvu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = ((short)(-1));
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Phí khám chữa bệnh";
            uiComboBoxItem2.Value = ((short)(1));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Chi phí dịch vụ Cận lâm sàng(XN,CĐHA...)";
            uiComboBoxItem3.Value = ((short)(2));
            this.cboLoaidichvu.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboLoaidichvu.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboLoaidichvu.Location = new System.Drawing.Point(419, 54);
            this.cboLoaidichvu.Name = "cboLoaidichvu";
            this.cboLoaidichvu.SelectInDataSource = true;
            this.cboLoaidichvu.Size = new System.Drawing.Size(336, 21);
            this.cboLoaidichvu.TabIndex = 2;
            this.cboLoaidichvu.Text = "Chọn loại chi phí";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(329, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "Loại chi phí:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkChitiet
            // 
            this.chkChitiet.Checked = true;
            this.chkChitiet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChitiet.Location = new System.Drawing.Point(546, 108);
            this.chkChitiet.Name = "chkChitiet";
            this.chkChitiet.Size = new System.Drawing.Size(209, 23);
            this.chkChitiet.TabIndex = 7;
            this.chkChitiet.Text = "Chi tiết theo từng Bệnh nhân?";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(6, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 295);
            this.panel1.TabIndex = 46;
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
            this.grdList.Size = new System.Drawing.Size(772, 295);
            this.grdList.TabIndex = 20;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdChitiet
            // 
            this.grdChitiet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.Size = new System.Drawing.Size(772, 295);
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
            this.cboKhoa.Size = new System.Drawing.Size(632, 21);
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
            // cboNhanvien
            // 
            this.cboNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhanvien.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboNhanvien.Location = new System.Drawing.Point(123, 81);
            this.cboNhanvien.Name = "cboNhanvien";
            this.cboNhanvien.SelectInDataSource = true;
            this.cboNhanvien.Size = new System.Drawing.Size(632, 21);
            this.cboNhanvien.TabIndex = 3;
            this.cboNhanvien.Text = "Nhân viên";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(13, 82);
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
            this.dtToDate.Location = new System.Drawing.Point(331, 108);
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
            this.dtFromDate.Location = new System.Drawing.Point(123, 108);
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
            this.chkByDate.Location = new System.Drawing.Point(48, 109);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 4;
            this.chkByDate.Text = "Từ ngày";
            // 
            // fm_baocaodoanhthutienkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
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
            this.Name = "fm_baocaodoanhthutienkham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THU TIỀN DỊCH VỤ KCB";
            this.Load += new System.EventHandler(this.frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
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
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboNhanvien;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private Janus.Windows.EditControls.UICheckBox chkChitiet;
        private Janus.Windows.EditControls.UIComboBox cboLoaidichvu;
        private System.Windows.Forms.Label label2;
    }
}