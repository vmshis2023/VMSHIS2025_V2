namespace  VNS.HIS.UI.Forms.Baocao
{
    partial class frm_KetNoi_DuyetBHYT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KetNoi_DuyetBHYT));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkAllTrangThai = new Janus.Windows.EditControls.UIRadioButton();
            this.radChuaduyet = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaduyet = new Janus.Windows.EditControls.UIRadioButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkAllTinhTrang = new Janus.Windows.EditControls.UIRadioButton();
            this.radNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.lblChuaKetThuc = new System.Windows.Forms.Label();
            this.lblDaKetThuc = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblmsg = new System.Windows.Forms.Label();
            this.cmdExportXML = new Janus.Windows.EditControls.UIButton();
            this.cmdXuatExcel = new Janus.Windows.EditControls.UIButton();
            this.txtMessageDisplay = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1167, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 666);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdList);
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1167, 666);
            this.splitContainer1.SplitterDistance = 555;
            this.splitContainer1.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin tìm kiếm trên lưới</FilterRowInfoText></LocalizableData>";
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 66);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1167, 489);
            this.grdList.TabIndex = 5;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiGroupBox3);
            this.uiGroupBox1.Controls.Add(this.uiGroupBox2);
            this.uiGroupBox1.Controls.Add(this.lblChuaKetThuc);
            this.uiGroupBox1.Controls.Add(this.lblDaKetThuc);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1167, 66);
            this.uiGroupBox1.TabIndex = 4;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.chkAllTrangThai);
            this.uiGroupBox3.Controls.Add(this.radChuaduyet);
            this.uiGroupBox3.Controls.Add(this.radDaduyet);
            this.uiGroupBox3.Location = new System.Drawing.Point(723, 14);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(274, 42);
            this.uiGroupBox3.TabIndex = 18;
            this.uiGroupBox3.Text = "Trạng thái";
            // 
            // chkAllTrangThai
            // 
            this.chkAllTrangThai.Checked = true;
            this.chkAllTrangThai.Location = new System.Drawing.Point(20, 13);
            this.chkAllTrangThai.Name = "chkAllTrangThai";
            this.chkAllTrangThai.Size = new System.Drawing.Size(72, 23);
            this.chkAllTrangThai.TabIndex = 13;
            this.chkAllTrangThai.TabStop = true;
            this.chkAllTrangThai.Text = "Tất cả";
            // 
            // radChuaduyet
            // 
            this.radChuaduyet.Location = new System.Drawing.Point(93, 13);
            this.radChuaduyet.Name = "radChuaduyet";
            this.radChuaduyet.Size = new System.Drawing.Size(94, 23);
            this.radChuaduyet.TabIndex = 15;
            this.radChuaduyet.Text = "Chưa duyệt";
            this.radChuaduyet.CheckedChanged += new System.EventHandler(this.radChuaduyet_CheckedChanged);
            // 
            // radDaduyet
            // 
            this.radDaduyet.Location = new System.Drawing.Point(187, 13);
            this.radDaduyet.Name = "radDaduyet";
            this.radDaduyet.Size = new System.Drawing.Size(72, 23);
            this.radDaduyet.TabIndex = 16;
            this.radDaduyet.Text = "Đã duyệt";
            this.radDaduyet.CheckedChanged += new System.EventHandler(this.radDaduyet_CheckedChanged);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.chkAllTinhTrang);
            this.uiGroupBox2.Controls.Add(this.radNoiTru);
            this.uiGroupBox2.Controls.Add(this.radNgoaiTru);
            this.uiGroupBox2.Location = new System.Drawing.Point(432, 12);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(274, 44);
            this.uiGroupBox2.TabIndex = 17;
            this.uiGroupBox2.Text = "Tình trạng";
            // 
            // chkAllTinhTrang
            // 
            this.chkAllTinhTrang.Checked = true;
            this.chkAllTinhTrang.Location = new System.Drawing.Point(20, 17);
            this.chkAllTinhTrang.Name = "chkAllTinhTrang";
            this.chkAllTinhTrang.Size = new System.Drawing.Size(72, 23);
            this.chkAllTinhTrang.TabIndex = 12;
            this.chkAllTinhTrang.TabStop = true;
            this.chkAllTinhTrang.Text = "Tất cả";
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(187, 17);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(72, 23);
            this.radNoiTru.TabIndex = 11;
            this.radNoiTru.Text = "Nội trú";
            this.radNoiTru.CheckedChanged += new System.EventHandler(this.radNoiTru_CheckedChanged);
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.Location = new System.Drawing.Point(93, 17);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(72, 23);
            this.radNgoaiTru.TabIndex = 10;
            this.radNgoaiTru.Text = "Ngoại trú";
            this.radNgoaiTru.CheckedChanged += new System.EventHandler(this.radNgoaiTru_CheckedChanged);
            // 
            // lblChuaKetThuc
            // 
            this.lblChuaKetThuc.AutoSize = true;
            this.lblChuaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuaKetThuc.ForeColor = System.Drawing.Color.Navy;
            this.lblChuaKetThuc.Location = new System.Drawing.Point(908, 0);
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
            this.lblDaKetThuc.Location = new System.Drawing.Point(743, 0);
            this.lblDaKetThuc.Name = "lblDaKetThuc";
            this.lblDaKetThuc.Size = new System.Drawing.Size(81, 15);
            this.lblDaKetThuc.TabIndex = 13;
            this.lblDaKetThuc.Text = "lblDaKetThuc";
            this.lblDaKetThuc.Visible = false;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(251, 22);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(159, 21);
            this.dtToDate.TabIndex = 7;
            this.dtToDate.Value = new System.DateTime(2015, 11, 2, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(9, 21);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(60, 23);
            this.chkByDate.TabIndex = 6;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(1003, 20);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(131, 35);
            this.cmdTimKiem.TabIndex = 4;
            this.cmdTimKiem.Text = "Tìm kiếm";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(86, 22);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(159, 21);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.Value = new System.DateTime(2015, 11, 2, 0, 0, 0, 0);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.progressBar);
            this.splitContainer2.Panel1.Controls.Add(this.txtMessageDisplay);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer2.Size = new System.Drawing.Size(1167, 107);
            this.splitContainer2.SplitterDistance = 45;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.cmdConfig);
            this.uiGroupBox4.Controls.Add(this.lblmsg);
            this.uiGroupBox4.Controls.Add(this.cmdExportXML);
            this.uiGroupBox4.Controls.Add(this.cmdXuatExcel);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox4.Location = new System.Drawing.Point(0, -3);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(1167, 61);
            this.uiGroupBox4.TabIndex = 1;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(1124, 26);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(43, 34);
            this.cmdConfig.TabIndex = 31;
            this.cmdConfig.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1167, 45);
            this.progressBar.TabIndex = 19;
            this.progressBar.Visible = false;
            // 
            // lblmsg
            // 
            this.lblmsg.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblmsg.Location = new System.Drawing.Point(15, 25);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(372, 23);
            this.lblmsg.TabIndex = 15;
            this.lblmsg.Text = "lblmsg";
            this.lblmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdExportXML
            // 
            this.cmdExportXML.Location = new System.Drawing.Point(466, 19);
            this.cmdExportXML.Name = "cmdExportXML";
            this.cmdExportXML.Size = new System.Drawing.Size(131, 35);
            this.cmdExportXML.TabIndex = 14;
            this.cmdExportXML.Text = "Xuất XML";
            // 
            // cmdXuatExcel
            // 
            this.cmdXuatExcel.Location = new System.Drawing.Point(619, 19);
            this.cmdXuatExcel.Name = "cmdXuatExcel";
            this.cmdXuatExcel.Size = new System.Drawing.Size(131, 35);
            this.cmdXuatExcel.TabIndex = 13;
            this.cmdXuatExcel.Text = "Xuất Excel";
            // 
            // txtMessageDisplay
            // 
            this.txtMessageDisplay.ActiveFormatStyle.BackColor = System.Drawing.Color.Transparent;
            this.txtMessageDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageDisplay.HighlightActiveButton = false;
            this.txtMessageDisplay.Image = ((System.Drawing.Image)(resources.GetObject("txtMessageDisplay.Image")));
            this.txtMessageDisplay.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.txtMessageDisplay.ImageSize = new System.Drawing.Size(24, 24);
            this.txtMessageDisplay.Location = new System.Drawing.Point(0, 0);
            this.txtMessageDisplay.Name = "txtMessageDisplay";
            this.txtMessageDisplay.Size = new System.Drawing.Size(1167, 45);
            this.txtMessageDisplay.TabIndex = 20;
            this.txtMessageDisplay.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Near;
            this.txtMessageDisplay.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // frm_KetNoi_DuyetBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 688);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_KetNoi_DuyetBHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách thông tin phôi bảo hiểm y tế";
            this.Load += new System.EventHandler(this.frm_Danhsach_benhnhan_inphoi_BHYT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Danhsach_benhnhan_inphoi_BHYT_KeyDown);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIRadioButton radChuaduyet;
        private Janus.Windows.EditControls.UIRadioButton radDaduyet;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIRadioButton radNoiTru;
        private Janus.Windows.EditControls.UIRadioButton radNgoaiTru;
        private System.Windows.Forms.Label lblChuaKetThuc;
        private System.Windows.Forms.Label lblDaKetThuc;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UIRadioButton chkAllTrangThai;
        private Janus.Windows.EditControls.UIRadioButton chkAllTinhTrang;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ProgressBar progressBar;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.Label lblmsg;
        private Janus.Windows.EditControls.UIButton cmdExportXML;
        private Janus.Windows.EditControls.UIButton cmdXuatExcel;
        private Janus.Windows.EditControls.UIButton txtMessageDisplay;
    }
}