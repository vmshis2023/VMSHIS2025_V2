namespace QMS
{
    partial class frm_QMSPCN_Layso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QMSPCN_Layso));
            Janus.Windows.GridEX.GridEXLayout grdAssignDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdConfig = new System.Windows.Forms.Button();
            this.txtMachidinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiaChi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtGioitinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTuoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txttenbenhnhan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtma_luotkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowPCN = new System.Windows.Forms.FlowLayoutPanel();
            this.grdAssignDetail = new Janus.Windows.GridEX.GridEX();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 94);
            this.panel1.TabIndex = 67;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdConfig.BackgroundImage")));
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdConfig.Location = new System.Drawing.Point(1511, 2);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(53, 49);
            this.cmdConfig.TabIndex = 45;
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // txtMachidinh
            // 
            this.txtMachidinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMachidinh.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.txtMachidinh.Location = new System.Drawing.Point(674, 10);
            this.txtMachidinh.MaxLength = 11;
            this.txtMachidinh.Name = "txtMachidinh";
            this.txtMachidinh.Size = new System.Drawing.Size(464, 40);
            this.txtMachidinh.TabIndex = 74;
            this.txtMachidinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMachidinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(472, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 46);
            this.label1.TabIndex = 75;
            this.label1.Text = "Mã chỉ định:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDiaChi.Location = new System.Drawing.Point(674, 78);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(464, 21);
            this.txtDiaChi.TabIndex = 459;
            this.txtDiaChi.TabStop = false;
            // 
            // txtGioitinh
            // 
            this.txtGioitinh.BackColor = System.Drawing.Color.White;
            this.txtGioitinh.Font = new System.Drawing.Font("Arial", 9F);
            this.txtGioitinh.Location = new System.Drawing.Point(1103, 54);
            this.txtGioitinh.Name = "txtGioitinh";
            this.txtGioitinh.ReadOnly = true;
            this.txtGioitinh.Size = new System.Drawing.Size(35, 21);
            this.txtGioitinh.TabIndex = 460;
            this.txtGioitinh.TabStop = false;
            // 
            // txtTuoi
            // 
            this.txtTuoi.BackColor = System.Drawing.Color.White;
            this.txtTuoi.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTuoi.Location = new System.Drawing.Point(1034, 54);
            this.txtTuoi.Name = "txtTuoi";
            this.txtTuoi.ReadOnly = true;
            this.txtTuoi.Size = new System.Drawing.Size(68, 21);
            this.txtTuoi.TabIndex = 463;
            this.txtTuoi.TabStop = false;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9F);
            this.label18.Location = new System.Drawing.Point(582, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 21);
            this.label18.TabIndex = 458;
            this.label18.Text = "Mã lần khám:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttenbenhnhan
            // 
            this.txttenbenhnhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttenbenhnhan.ForeColor = System.Drawing.Color.Black;
            this.txttenbenhnhan.Location = new System.Drawing.Point(856, 54);
            this.txttenbenhnhan.Name = "txttenbenhnhan";
            this.txttenbenhnhan.ReadOnly = true;
            this.txttenbenhnhan.Size = new System.Drawing.Size(178, 21);
            this.txttenbenhnhan.TabIndex = 461;
            this.txttenbenhnhan.TabStop = false;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Arial", 9F);
            this.label20.Location = new System.Drawing.Point(751, 54);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(99, 21);
            this.label20.TabIndex = 457;
            this.label20.Text = "Tên Bệnh nhân:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtma_luotkham
            // 
            this.txtma_luotkham.BackColor = System.Drawing.Color.White;
            this.txtma_luotkham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtma_luotkham.Location = new System.Drawing.Point(674, 54);
            this.txtma_luotkham.MaxLength = 8;
            this.txtma_luotkham.Name = "txtma_luotkham";
            this.txtma_luotkham.ReadOnly = true;
            this.txtma_luotkham.Size = new System.Drawing.Size(71, 21);
            this.txtma_luotkham.TabIndex = 456;
            this.txtma_luotkham.TabStop = false;
            this.txtma_luotkham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtma_luotkham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F);
            this.label15.Location = new System.Drawing.Point(566, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(102, 21);
            this.label15.TabIndex = 462;
            this.label15.Text = "Địa chỉ BN:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.flowPCN);
            this.panel2.Controls.Add(this.grdAssignDetail);
            this.panel2.Location = new System.Drawing.Point(2, 101);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1564, 657);
            this.panel2.TabIndex = 464;
            // 
            // flowPCN
            // 
            this.flowPCN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPCN.Location = new System.Drawing.Point(432, 0);
            this.flowPCN.Name = "flowPCN";
            this.flowPCN.Size = new System.Drawing.Size(1132, 657);
            this.flowPCN.TabIndex = 257;
            // 
            // grdAssignDetail
            // 
            this.grdAssignDetail.AlternatingColors = true;
            this.grdAssignDetail.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdAssignDetail.AlternatingRowFormatStyle.BackColorGradient = System.Drawing.Color.White;
            this.grdAssignDetail.Cursor = System.Windows.Forms.Cursors.Default;
            grdAssignDetail_DesignTimeLayout.LayoutString = resources.GetString("grdAssignDetail_DesignTimeLayout.LayoutString");
            this.grdAssignDetail.DesignTimeLayout = grdAssignDetail_DesignTimeLayout;
            this.grdAssignDetail.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdAssignDetail.DynamicFiltering = true;
            this.grdAssignDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdAssignDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdAssignDetail.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grdAssignDetail.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.FocusCellFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdAssignDetail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAssignDetail.GroupByBoxVisible = false;
            this.grdAssignDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdAssignDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdAssignDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdAssignDetail.Location = new System.Drawing.Point(0, 0);
            this.grdAssignDetail.Name = "grdAssignDetail";
            this.grdAssignDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.SaveSettings = true;
            this.grdAssignDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.Transparent;
            this.grdAssignDetail.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.SelectedFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdAssignDetail.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdAssignDetail.SettingsKey = "grdAssignDetail";
            this.grdAssignDetail.Size = new System.Drawing.Size(432, 657);
            this.grdAssignDetail.TabIndex = 256;
            this.grdAssignDetail.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
            this.grdAssignDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdAssignDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdAssignDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // frm_QMSPCN_Layso
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1568, 761);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtGioitinh);
            this.Controls.Add(this.txtTuoi);
            this.Controls.Add(this.txttenbenhnhan);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtma_luotkham);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtMachidinh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdConfig);
            this.Controls.Add(this.panel1);
            this.Name = "frm_QMSPCN_Layso";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lấy số QMS khoa phòng chức năng - Quét(hoặc nhập) mã phiếu chỉ định. Sau đó lấy s" +
    "ố khám theo các phòng chức năng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            ((System.Configuration.IPersistComponentSettings)(this.grdAssignDetail)).LoadComponentSettings();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdConfig;
        private Janus.Windows.GridEX.EditControls.EditBox txtMachidinh;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtDiaChi;
        private Janus.Windows.GridEX.EditControls.EditBox txtGioitinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtTuoi;
        private System.Windows.Forms.Label label18;
        private Janus.Windows.GridEX.EditControls.EditBox txttenbenhnhan;
        private System.Windows.Forms.Label label20;
        private Janus.Windows.GridEX.EditControls.EditBox txtma_luotkham;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdAssignDetail;
        private System.Windows.Forms.FlowLayoutPanel flowPCN;
    }
}