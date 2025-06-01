namespace VNS.Libs
{
    partial class frm_SignInfor_V1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SignInfor_V1));
            this.sysColor = new System.Windows.Forms.Label();
            this.cmdQuit = new Janus.Windows.EditControls.UIButton();
            this.cmdOK = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkPortrait = new Janus.Windows.EditControls.UICheckBox();
            this.chkGhiLai = new Janus.Windows.EditControls.UICheckBox();
            this.txtBaoCao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.PricTure = new System.Windows.Forms.PictureBox();
            this.cmdUpdateAllUser = new Janus.Windows.EditControls.UIButton();
            this.txtTrinhky = new System.Windows.Forms.WebBrowser();
            this.txtNoidung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricTure)).BeginInit();
            this.SuspendLayout();
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.sysColor.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sysColor.ForeColor = System.Drawing.Color.Maroon;
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(1008, 61);
            this.sysColor.TabIndex = 0;
            this.sysColor.Text = "TÙY BIẾN TRÌNH KÝ CHO CÁC BÁO CÁO";
            this.sysColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdQuit
            // 
            this.cmdQuit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdQuit.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuit.Image")));
            this.cmdQuit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdQuit.Location = new System.Drawing.Point(757, 712);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(120, 35);
            this.cmdQuit.TabIndex = 3;
            this.cmdQuit.Text = "Thoát";
            this.cmdQuit.ToolTipText = "Thoát Form hiện tại";
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdOK.Location = new System.Drawing.Point(631, 712);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(120, 35);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "Chấp nhận";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click_1);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.txtNoidung);
            this.uiGroupBox1.Controls.Add(this.txtTrinhky);
            this.uiGroupBox1.Controls.Add(this.chkPortrait);
            this.uiGroupBox1.Controls.Add(this.chkGhiLai);
            this.uiGroupBox1.Controls.Add(this.txtBaoCao);
            this.uiGroupBox1.Controls.Add(this.Label2);
            this.uiGroupBox1.Controls.Add(this.Label1);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 61);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 645);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin trình ký";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // chkPortrait
            // 
            this.chkPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPortrait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPortrait.ForeColor = System.Drawing.Color.Navy;
            this.chkPortrait.Location = new System.Drawing.Point(287, 613);
            this.chkPortrait.Name = "chkPortrait";
            this.chkPortrait.Size = new System.Drawing.Size(97, 23);
            this.chkPortrait.TabIndex = 22;
            this.chkPortrait.Text = "Portrait?";
            this.chkPortrait.ToolTipText = "Thông tin sẽ được lưu lại cho báo cáo trên";
            // 
            // chkGhiLai
            // 
            this.chkGhiLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGhiLai.Checked = true;
            this.chkGhiLai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhiLai.ForeColor = System.Drawing.Color.Navy;
            this.chkGhiLai.Location = new System.Drawing.Point(105, 613);
            this.chkGhiLai.Name = "chkGhiLai";
            this.chkGhiLai.Size = new System.Drawing.Size(251, 23);
            this.chkGhiLai.TabIndex = 4;
            this.chkGhiLai.Text = "Ghi lại cho lần sau dùng";
            this.chkGhiLai.ToolTipText = "Thông tin sẽ được lưu lại cho báo cáo trên";
            // 
            // txtBaoCao
            // 
            this.txtBaoCao.Location = new System.Drawing.Point(105, 23);
            this.txtBaoCao.Name = "txtBaoCao";
            this.txtBaoCao.ReadOnly = true;
            this.txtBaoCao.Size = new System.Drawing.Size(789, 21);
            this.txtBaoCao.TabIndex = 20;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(48, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(49, 15);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Trình ký";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(22, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 15);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Tên báo cáo";
            // 
            // PricTure
            // 
            this.PricTure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PricTure.Image = ((System.Drawing.Image)(resources.GetObject("PricTure.Image")));
            this.PricTure.Location = new System.Drawing.Point(1, 0);
            this.PricTure.Name = "PricTure";
            this.PricTure.Size = new System.Drawing.Size(50, 55);
            this.PricTure.TabIndex = 14;
            this.PricTure.TabStop = false;
            // 
            // cmdUpdateAllUser
            // 
            this.cmdUpdateAllUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdUpdateAllUser.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateAllUser.Image")));
            this.cmdUpdateAllUser.Location = new System.Drawing.Point(63, 714);
            this.cmdUpdateAllUser.Name = "cmdUpdateAllUser";
            this.cmdUpdateAllUser.Size = new System.Drawing.Size(121, 28);
            this.cmdUpdateAllUser.TabIndex = 15;
            this.cmdUpdateAllUser.Text = "Update All User";
            this.cmdUpdateAllUser.Visible = false;
            this.cmdUpdateAllUser.Click += new System.EventHandler(this.cmdUpdateAllUser_Click);
            // 
            // txtTrinhky
            // 
            this.txtTrinhky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrinhky.Location = new System.Drawing.Point(105, 50);
            this.txtTrinhky.MinimumSize = new System.Drawing.Size(20, 20);
            this.txtTrinhky.Name = "txtTrinhky";
            this.txtTrinhky.ScriptErrorsSuppressed = true;
            this.txtTrinhky.Size = new System.Drawing.Size(789, 557);
            this.txtTrinhky.TabIndex = 23;
            // 
            // txtNoidung
            // 
            this.txtNoidung.Location = new System.Drawing.Point(922, 214);
            this.txtNoidung.Name = "txtNoidung";
            this.txtNoidung.ReadOnly = true;
            this.txtNoidung.Size = new System.Drawing.Size(74, 21);
            this.txtNoidung.TabIndex = 24;
            this.txtNoidung.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // frm_SignInfor_V1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.cmdUpdateAllUser);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.PricTure);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdQuit);
            this.Controls.Add(this.sysColor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SignInfor_V1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin trình ký";
            this.ResizeEnd += new System.EventHandler(this.frm_SignInfor_V1_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SignInfor_V1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricTure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label sysColor;
        private Janus.Windows.EditControls.UIButton cmdQuit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.PictureBox PricTure;
        public Janus.Windows.GridEX.EditControls.EditBox txtBaoCao;
        public Janus.Windows.EditControls.UIButton cmdOK;
        public Janus.Windows.EditControls.UICheckBox chkGhiLai;
        public Janus.Windows.EditControls.UIButton cmdUpdateAllUser;
        public Janus.Windows.EditControls.UICheckBox chkPortrait;
        public System.Windows.Forms.WebBrowser txtTrinhky;
        public Janus.Windows.GridEX.EditControls.EditBox txtNoidung;
        private System.Windows.Forms.Timer timer1;
    }
}