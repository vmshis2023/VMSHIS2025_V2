namespace VNS.HIS.UI.Baocao
{
    partial class frm_INPHIEU_CLS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_INPHIEU_CLS));
            Janus.Windows.GridEX.GridEXLayout grdAssignInfo_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.lblStaffName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAssignCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdAssignInfo = new Janus.Windows.GridEX.GridEX();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.radKhoA4 = new Janus.Windows.EditControls.UIRadioButton();
            this.radA5 = new Janus.Windows.EditControls.UIRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.uiGroupBox1.Controls.Add(this.lblStaffName);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.lblAssignCode);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(917, 94);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin phiếu chỉ định";
            // 
            // lblStaffName
            // 
            this.lblStaffName.AutoSize = true;
            this.lblStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffName.Location = new System.Drawing.Point(146, 53);
            this.lblStaffName.Name = "lblStaffName";
            this.lblStaffName.Size = new System.Drawing.Size(118, 17);
            this.lblStaffName.TabIndex = 4;
            this.lblStaffName.Text = "Bác sỹ chỉ định";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bác sỹ chỉ định :";
           
            // 
            // lblAssignCode
            // 
            this.lblAssignCode.AutoSize = true;
            this.lblAssignCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignCode.Location = new System.Drawing.Point(92, 26);
            this.lblAssignCode.Name = "lblAssignCode";
            this.lblAssignCode.Size = new System.Drawing.Size(17, 17);
            this.lblAssignCode.TabIndex = 1;
            this.lblAssignCode.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số phiếu";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdAssignInfo);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 94);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(917, 458);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "&Thông tin chi tiết";
            // 
            // grdAssignInfo
            // 
            grdAssignInfo_DesignTimeLayout.LayoutString = resources.GetString("grdAssignInfo_DesignTimeLayout.LayoutString");
            this.grdAssignInfo.DesignTimeLayout = grdAssignInfo_DesignTimeLayout;
            this.grdAssignInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAssignInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdAssignInfo.GroupByBoxFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignInfo.GroupByBoxFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.grdAssignInfo.GroupByBoxVisible = false;
            this.grdAssignInfo.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdAssignInfo.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdAssignInfo.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdAssignInfo.Location = new System.Drawing.Point(3, 18);
            this.grdAssignInfo.Name = "grdAssignInfo";
            this.grdAssignInfo.RecordNavigator = true;
            this.grdAssignInfo.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignInfo.Size = new System.Drawing.Size(911, 437);
            this.grdAssignInfo.TabIndex = 1;
            this.grdAssignInfo.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignInfo.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdAssignInfo.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignInfo.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdAssignInfo.UseGroupRowSelector = true;
            this.grdAssignInfo.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(this.grdAssignInfo_RowCheckStateChanged);
            this.grdAssignInfo.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignInfo_CellValueChanged);
            this.grdAssignInfo.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.grdAssignInfo_UpdatingCell);
            this.grdAssignInfo.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignInfo_ColumnHeaderClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 567);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 77;
            this.label3.Text = "&Ngày in phiếu";
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuXN.Location = new System.Drawing.Point(261, 596);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 40);
            this.cmdInPhieuXN.TabIndex = 76;
            this.cmdInPhieuXN.Text = "&In chỉ định (F4)";
            this.cmdInPhieuXN.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(405, 596);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 40);
            this.cmdExit.TabIndex = 75;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayInPhieu.DropDownCalendar.Name = "";
            this.dtNgayInPhieu.Location = new System.Drawing.Point(113, 564);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(177, 20);
            this.dtNgayInPhieu.TabIndex = 78;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTieuDe.Location = new System.Drawing.Point(296, 562);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(238, 26);
            this.txtTieuDe.TabIndex = 79;
            this.txtTieuDe.Text = "PHIẾU CHỈ ĐỊNH";
            this.txtTieuDe.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // radKhoA4
            // 
            this.radKhoA4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radKhoA4.Location = new System.Drawing.Point(86, 595);
            this.radKhoA4.Name = "radKhoA4";
            this.radKhoA4.Size = new System.Drawing.Size(67, 23);
            this.radKhoA4.TabIndex = 448;
            this.radKhoA4.Tag = "A4";
            this.radKhoA4.Text = "Khổ A4";
            // 
            // radA5
            // 
            this.radA5.Checked = true;
            this.radA5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radA5.Location = new System.Drawing.Point(12, 596);
            this.radA5.Name = "radA5";
            this.radA5.Size = new System.Drawing.Size(67, 23);
            this.radA5.TabIndex = 447;
            this.radA5.TabStop = true;
            this.radA5.Tag = "A5";
            this.radA5.Text = "Khổ A5";
            // 
            // frm_INPHIEU_CLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 638);
            this.Controls.Add(this.radKhoA4);
            this.Controls.Add(this.radA5);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_INPHIEU_CLS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin chỉ định";
            this.Load += new System.EventHandler(this.frm_INPHIEU_CLS_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_INPHIEU_CLS_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        private System.Windows.Forms.Label lblAssignCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStaffName;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.GridEX grdAssignInfo;
        private Janus.Windows.EditControls.UIRadioButton radKhoA4;
        private Janus.Windows.EditControls.UIRadioButton radA5;
    }
}