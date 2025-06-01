namespace  VNS.HIS.UI.THANHTOAN
{
    partial class frm_DanhSach_Phoi_BHYT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhSach_Phoi_BHYT));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.lblChuaKetThuc = new System.Windows.Forms.Label();
            this.lblDaKetThuc = new System.Windows.Forms.Label();
            this.cmdXuatExcel = new Janus.Windows.EditControls.UIButton();
            this.radNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.chkChuaKetThuc = new Janus.Windows.EditControls.UICheckBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaLanKham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lblChuaKetThuc);
            this.uiGroupBox1.Controls.Add(this.lblDaKetThuc);
            this.uiGroupBox1.Controls.Add(this.cmdXuatExcel);
            this.uiGroupBox1.Controls.Add(this.radNoiTru);
            this.uiGroupBox1.Controls.Add(this.radNgoaiTru);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.panel1);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.chkChuaKetThuc);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtMaLanKham);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(843, 104);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // lblChuaKetThuc
            // 
            this.lblChuaKetThuc.AutoSize = true;
            this.lblChuaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuaKetThuc.ForeColor = System.Drawing.Color.Navy;
            this.lblChuaKetThuc.Location = new System.Drawing.Point(292, 82);
            this.lblChuaKetThuc.Name = "lblChuaKetThuc";
            this.lblChuaKetThuc.Size = new System.Drawing.Size(94, 15);
            this.lblChuaKetThuc.TabIndex = 14;
            this.lblChuaKetThuc.Text = "lblChuaKetThuc";
            this.lblChuaKetThuc.Visible = false;
            // 
            // lblDaKetThuc
            // 
            this.lblDaKetThuc.AutoSize = true;
            this.lblDaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaKetThuc.ForeColor = System.Drawing.Color.Navy;
            this.lblDaKetThuc.Location = new System.Drawing.Point(12, 82);
            this.lblDaKetThuc.Name = "lblDaKetThuc";
            this.lblDaKetThuc.Size = new System.Drawing.Size(81, 15);
            this.lblDaKetThuc.TabIndex = 13;
            this.lblDaKetThuc.Text = "lblDaKetThuc";
            this.lblDaKetThuc.Visible = false;
            // 
            // cmdXuatExcel
            // 
            this.cmdXuatExcel.Location = new System.Drawing.Point(756, 14);
            this.cmdXuatExcel.Name = "cmdXuatExcel";
            this.cmdXuatExcel.Size = new System.Drawing.Size(81, 28);
            this.cmdXuatExcel.TabIndex = 12;
            this.cmdXuatExcel.Text = "&Xuất Excel";
            this.cmdXuatExcel.Click += new System.EventHandler(this.cmdXuatExcel_Click);
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(425, 48);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(72, 23);
            this.radNoiTru.TabIndex = 11;
            this.radNoiTru.Text = "&Nội trú";
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.Checked = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(425, 19);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(72, 23);
            this.radNgoaiTru.TabIndex = 10;
            this.radNgoaiTru.TabStop = true;
            this.radNgoaiTru.Text = "&Ngoại trú";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(763, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "&Chưa kết thúc";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(727, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 21);
            this.panel1.TabIndex = 8;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Enabled = false;
            this.dtToDate.Location = new System.Drawing.Point(251, 54);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(159, 20);
            this.dtToDate.TabIndex = 7;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(15, 53);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(60, 23);
            this.chkByDate.TabIndex = 6;
            this.chkByDate.Text = "&Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // chkChuaKetThuc
            // 
            this.chkChuaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkChuaKetThuc.Location = new System.Drawing.Point(295, 24);
            this.chkChuaKetThuc.Name = "chkChuaKetThuc";
            this.chkChuaKetThuc.Size = new System.Drawing.Size(115, 23);
            this.chkChuaKetThuc.TabIndex = 5;
            this.chkChuaKetThuc.Text = "&Chưa kết thúc";
            this.chkChuaKetThuc.CheckedChanged += new System.EventHandler(this.chkChuaKetThuc_CheckedChanged);
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(527, 19);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(131, 55);
            this.cmdTimKiem.TabIndex = 4;
            this.cmdTimKiem.Text = "&Tìm kiếm";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Mã lần khám";
            // 
            // txtMaLanKham
            // 
            this.txtMaLanKham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtMaLanKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaLanKham.Location = new System.Drawing.Point(86, 19);
            this.txtMaLanKham.Name = "txtMaLanKham";
            this.txtMaLanKham.Size = new System.Drawing.Size(159, 26);
            this.txtMaLanKham.TabIndex = 2;
            this.txtMaLanKham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaLanKham.TextChanged += new System.EventHandler(this.txtMaLanKham_TextChanged);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Enabled = false;
            this.dtFromDate.Location = new System.Drawing.Point(86, 54);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(159, 20);
            this.dtFromDate.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
                " thông tin tìm kiếm trên lưới</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 104);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(843, 562);
            this.grdList.TabIndex = 1;
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdList;
            // 
            // frm_DanhSach_Phoi_BHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 666);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.uiGroupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DanhSach_Phoi_BHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách thông tin phôi bảo hiểm y tế";
            this.Load += new System.EventHandler(this.frm_DanhSach_Phoi_BHYT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DanhSach_Phoi_BHYT_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaLanKham;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UICheckBox chkChuaKetThuc;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIRadioButton radNoiTru;
        private Janus.Windows.EditControls.UIRadioButton radNgoaiTru;
        private Janus.Windows.EditControls.UIButton cmdXuatExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.Label lblChuaKetThuc;
        private System.Windows.Forms.Label lblDaKetThuc;
    }
}