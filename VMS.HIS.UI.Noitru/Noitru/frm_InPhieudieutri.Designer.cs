namespace VNS.HIS.UI.NOITRU
{
    partial class frm_InPhieudieutri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InPhieudieutri));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdInDieuTri = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.chkHienthiCaDaIn = new Janus.Windows.EditControls.UICheckBox();
            this.chkInYLenhThuocCLS = new Janus.Windows.EditControls.UICheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboKhoanoitru = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(640, 13);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(134, 35);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdInDieuTri
            // 
            this.cmdInDieuTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInDieuTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInDieuTri.Image = global::VMS.HIS.Noitru.Properties.Resources.printer_32;
            this.cmdInDieuTri.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInDieuTri.Location = new System.Drawing.Point(497, 13);
            this.cmdInDieuTri.Name = "cmdInDieuTri";
            this.cmdInDieuTri.Size = new System.Drawing.Size(134, 35);
            this.cmdInDieuTri.TabIndex = 2;
            this.cmdInDieuTri.Text = "In phiếu điều trị";
            this.cmdInDieuTri.Click += new System.EventHandler(this.cmdInDieuTri_Click);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmdInDieuTri);
            this.uiGroupBox3.Controls.Add(this.cmdExit);
            this.uiGroupBox3.Controls.Add(this.dtNgayInPhieu);
            this.uiGroupBox3.Controls.Add(this.label3);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 507);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(784, 54);
            this.uiGroupBox3.TabIndex = 4;
            this.uiGroupBox3.Text = "Tùy chọn in phiếu điều trị";
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(95, 21);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(140, 21);
            this.dtNgayInPhieu.TabIndex = 119;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 118;
            this.label3.Text = "Ngày in phiếu";
            // 
            // chkHienthiCaDaIn
            // 
            this.chkHienthiCaDaIn.Location = new System.Drawing.Point(388, 7);
            this.chkHienthiCaDaIn.Name = "chkHienthiCaDaIn";
            this.chkHienthiCaDaIn.Size = new System.Drawing.Size(178, 23);
            this.chkHienthiCaDaIn.TabIndex = 1;
            this.chkHienthiCaDaIn.Text = "Hiển thị cả những ngày  đã in ?";
            this.chkHienthiCaDaIn.CheckedChanged += new System.EventHandler(this.chkHienthiCaDaIn_CheckedChanged);
            // 
            // chkInYLenhThuocCLS
            // 
            this.chkInYLenhThuocCLS.Checked = true;
            this.chkInYLenhThuocCLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInYLenhThuocCLS.ForeColor = System.Drawing.Color.Navy;
            this.chkInYLenhThuocCLS.Location = new System.Drawing.Point(599, 7);
            this.chkInYLenhThuocCLS.Name = "chkInYLenhThuocCLS";
            this.chkInYLenhThuocCLS.Size = new System.Drawing.Size(198, 23);
            this.chkInYLenhThuocCLS.TabIndex = 0;
            this.chkInYLenhThuocCLS.Text = "In cả y lệnh Thuốc, CLS, VTTH";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboKhoanoitru);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.chkHienthiCaDaIn);
            this.panel1.Controls.Add(this.chkInYLenhThuocCLS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 39);
            this.panel1.TabIndex = 6;
            // 
            // cboKhoanoitru
            // 
            this.cboKhoanoitru.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoanoitru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoanoitru.FormattingEnabled = true;
            this.cboKhoanoitru.Location = new System.Drawing.Point(95, 9);
            this.cboKhoanoitru.Name = "cboKhoanoitru";
            this.cboKhoanoitru.Size = new System.Drawing.Size(270, 21);
            this.cboKhoanoitru.TabIndex = 79;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Arial", 9F);
            this.label22.Location = new System.Drawing.Point(3, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 23);
            this.label22.TabIndex = 80;
            this.label22.Text = "Khoa nội trú";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(784, 468);
            this.uiGroupBox1.TabIndex = 7;
            this.uiGroupBox1.Text = "Chọn những phiếu điều trị cần in ";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.AutomaticSort = false;
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân đưa vào phòng khám</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdList.FrozenColumns = -1;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.LinkFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdList.Location = new System.Drawing.Point(0, 17);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.SelectedFormatStyle.Alpha = 2;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.SelectedFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.SelectedFormatStyle.ForeColor = System.Drawing.Color.White;
            this.grdList.SelectedInactiveFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdList.Size = new System.Drawing.Size(784, 448);
            this.grdList.TabIndex = 10;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_InPhieudieutri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiGroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_InPhieudieutri";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In phiếu điều trị";
            this.Load += new System.EventHandler(this.frm_InPhieudieutri_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_InPhieudieutri_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdInDieuTri;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UICheckBox chkHienthiCaDaIn;
        private Janus.Windows.EditControls.UICheckBox chkInYLenhThuocCLS;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ComboBox cboKhoanoitru;
        private System.Windows.Forms.Label label22;
    }
}