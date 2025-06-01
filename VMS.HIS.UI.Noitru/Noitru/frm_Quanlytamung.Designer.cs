namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Quanlytamung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Quanlytamung));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label11 = new System.Windows.Forms.Label();
            this.cboKhoaChuyenDen = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label7 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.pnlTamUng = new System.Windows.Forms.Panel();
            this.ucTamung1 = new VNS.HIS.UCs.Noitru.ucTamung();
            this.ucThongtinnguoibenh_v41 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v4();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlTamUng.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.label11);
            this.uiGroupBox1.Controls.Add(this.cboKhoaChuyenDen);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1264, 68);
            this.uiGroupBox1.TabIndex = 0;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTimKiem.Location = new System.Drawing.Point(1073, 11);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(181, 43);
            this.cmdTimKiem.TabIndex = 18;
            this.cmdTimKiem.Text = "Tìm kiếm(F3)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(390, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Khoa nội trú:";
            // 
            // cboKhoaChuyenDen
            // 
            this.cboKhoaChuyenDen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoaChuyenDen.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoaChuyenDen.Location = new System.Drawing.Point(472, 42);
            this.cboKhoaChuyenDen.Name = "cboKhoaChuyenDen";
            this.cboKhoaChuyenDen.Size = new System.Drawing.Size(595, 21);
            this.cboKhoaChuyenDen.TabIndex = 5;
            this.cboKhoaChuyenDen.Text = "Khoa nội trú";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên bệnh nhân:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.Location = new System.Drawing.Point(472, 12);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(595, 21);
            this.txtPatientName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lượt khám:";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPatientCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(99, 12);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(133, 22);
            this.txtPatientCode.TabIndex = 0;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
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
            this.dtToDate.Location = new System.Drawing.Point(239, 42);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(133, 21);
            this.dtToDate.TabIndex = 3;
            // 
            // chkByDate
            // 
            this.chkByDate.Location = new System.Drawing.Point(9, 40);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(83, 27);
            this.chkByDate.TabIndex = 2;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
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
            this.dtFromDate.Location = new System.Drawing.Point(99, 42);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtFromDate.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(0, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1264, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "Chú ý: Bệnh nhân Dịch vụ phải đóng hết tiền ngoại trú mới được nộp Tạm ứng";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Visible = false;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.BackColor = System.Drawing.SystemColors.Menu;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 88);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(609, 897);
            this.grdList.TabIndex = 2;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlTamUng
            // 
            this.pnlTamUng.Controls.Add(this.ucTamung1);
            this.pnlTamUng.Controls.Add(this.ucThongtinnguoibenh_v41);
            this.pnlTamUng.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTamUng.Location = new System.Drawing.Point(609, 88);
            this.pnlTamUng.Name = "pnlTamUng";
            this.pnlTamUng.Size = new System.Drawing.Size(655, 897);
            this.pnlTamUng.TabIndex = 38;
            // 
            // ucTamung1
            // 
            this.ucTamung1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTamung1.Location = new System.Drawing.Point(0, 80);
            this.ucTamung1.Name = "ucTamung1";
            this.ucTamung1.Size = new System.Drawing.Size(655, 817);
            this.ucTamung1.TabIndex = 1;
            // 
            // ucThongtinnguoibenh_v41
            // 
            this.ucThongtinnguoibenh_v41.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_v41.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_v41.Name = "ucThongtinnguoibenh_v41";
            this.ucThongtinnguoibenh_v41.Size = new System.Drawing.Size(655, 80);
            this.ucThongtinnguoibenh_v41.TabIndex = 2;
            // 
            // frm_Quanlytamung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlTamUng);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.uiGroupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Quanlytamung";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nộp tiền tạm ứng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Quanlytamung_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanlytamung_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlTamUng.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientCode;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.EditControls.UIComboBox cboKhoaChuyenDen;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.Panel pnlTamUng;
        private UCs.Noitru.ucTamung ucTamung1;
        private Forms.Dungchung.UCs.ucThongtinnguoibenh_v4 ucThongtinnguoibenh_v41;
        
    }
}