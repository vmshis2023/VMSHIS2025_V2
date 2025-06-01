namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    partial class frm_BHYT_baocao_tonghop_chiphitangthem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BHYT_baocao_tonghop_chiphitangthem));
            Janus.Windows.GridEX.GridEXLayout grdResult_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdInPhieu = new Janus.Windows.EditControls.UIButton();
            this.grdResult = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(462, 22);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 123;
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(77, 27);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(141, 21);
            this.dtNgayInPhieu.TabIndex = 125;
            this.dtNgayInPhieu.TabStop = false;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(22, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 126;
            this.label3.Text = "Ngày in:";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(740, 22);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 124;
            this.cmdExit.Text = "Thoát (Esc)";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cmdExportToExcel);
            this.uiGroupBox2.Controls.Add(this.dtNgayInPhieu);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.cmdInPhieu);
            this.uiGroupBox2.Controls.Add(this.cmdExit);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 269);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(912, 65);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "Chức năng";
            // 
            // cmdInPhieu
            // 
            this.cmdInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieu.Image")));
            this.cmdInPhieu.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieu.Location = new System.Drawing.Point(601, 22);
            this.cmdInPhieu.Name = "cmdInPhieu";
            this.cmdInPhieu.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieu.TabIndex = 122;
            this.cmdInPhieu.Text = "In báo cáo";
            this.cmdInPhieu.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieu.Click += new System.EventHandler(this.cmdInPhieu_Click);
            // 
            // grdResult
            // 
            this.grdResult.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdResult_DesignTimeLayout.LayoutString = resources.GetString("grdResult_DesignTimeLayout.LayoutString");
            this.grdResult.DesignTimeLayout = grdResult_DesignTimeLayout;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.Font = new System.Drawing.Font("Arial", 9F);
            this.grdResult.GroupByBoxVisible = false;
            this.grdResult.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdResult.Location = new System.Drawing.Point(3, 16);
            this.grdResult.Name = "grdResult";
            this.grdResult.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdResult.Size = new System.Drawing.Size(906, 250);
            this.grdResult.TabIndex = 22;
            this.grdResult.TabStop = false;
            this.grdResult.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdResult.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdResult.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.grdResult);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(912, 269);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Kết quả tìm kiếm";
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "baocao_tonghopchiphitangthem_theothongtu37";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(912, 47);
            this.baocaO_TIEUDE1.TabIndex = 123;
            this.baocaO_TIEUDE1.TIEUDE = "TỔNG HỢP CHI PHÍ TĂNG THÊM DO ÁP DỤNG GIÁ DỊCH VỤ Y TẾ MỚI THEO TTTL 37";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dtToDate.Location = new System.Drawing.Point(392, 54);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 23);
            this.dtToDate.TabIndex = 51;
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
            this.dtFromDate.Location = new System.Drawing.Point(184, 54);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(200, 23);
            this.dtFromDate.TabIndex = 50;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(109, 54);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(69, 23);
            this.chkByDate.TabIndex = 49;
            this.chkByDate.Text = "Từ ngày";
            // 
            // cboKhoa
            // 
            this.cboKhoa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoa.Location = new System.Drawing.Point(184, 20);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.SelectInDataSource = true;
            this.cboKhoa.Size = new System.Drawing.Size(408, 23);
            this.cboKhoa.TabIndex = 45;
            this.cboKhoa.Text = "Khoa thực hiện";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(74, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Khoa KCB";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.cboKhoa);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 47);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(912, 131);
            this.uiGroupBox1.TabIndex = 124;
            this.uiGroupBox1.Text = "Điều kiện tìm kiếm";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.baocaO_TIEUDE1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(912, 516);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 516);
            this.panel1.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(912, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // frm_BHYT_baocao_tonghop_chiphitangthem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frm_BHYT_baocao_tonghop_chiphitangthem";
            this.Text = "DANH SÁCH BỆNH NHÂN THỰC HIỆN SIÊU ÂM";
            this.Load += new System.EventHandler(this.frm_thongke_danhsachbenhnhanh_sieuam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIButton cmdInPhieu;
        private Janus.Windows.GridEX.GridEX grdResult;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
    }
}