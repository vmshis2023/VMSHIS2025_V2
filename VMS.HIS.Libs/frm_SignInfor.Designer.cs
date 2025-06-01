namespace VNS.Libs
{
    partial class frm_SignInfor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SignInfor));
            this.sysColor = new System.Windows.Forms.Label();
            this.cmdQuit = new Janus.Windows.EditControls.UIButton();
            this.cmdOK = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtMabaocao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkPortrait = new Janus.Windows.EditControls.UICheckBox();
            this.txtTrinhky = new RicherTextBox.RicherTextBox();
            this.chkGhiLai = new Janus.Windows.EditControls.UICheckBox();
            this.txtBaoCao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.PricTure = new System.Windows.Forms.PictureBox();
            this.cmdUpdateAllUser = new Janus.Windows.EditControls.UIButton();
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
            this.cmdQuit.Location = new System.Drawing.Point(506, 558);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(137, 37);
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
            this.cmdOK.Location = new System.Drawing.Point(360, 558);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(140, 37);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "Chấp nhận";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click_1);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.txtMabaocao);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.chkPortrait);
            this.uiGroupBox1.Controls.Add(this.txtTrinhky);
            this.uiGroupBox1.Controls.Add(this.chkGhiLai);
            this.uiGroupBox1.Controls.Add(this.txtBaoCao);
            this.uiGroupBox1.Controls.Add(this.Label2);
            this.uiGroupBox1.Controls.Add(this.Label1);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 61);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 489);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin trình ký";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // txtMabaocao
            // 
            this.txtMabaocao.Location = new System.Drawing.Point(105, 23);
            this.txtMabaocao.Name = "txtMabaocao";
            this.txtMabaocao.ReadOnly = true;
            this.txtMabaocao.Size = new System.Drawing.Size(314, 21);
            this.txtMabaocao.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Mã báo cáo";
            // 
            // chkPortrait
            // 
            this.chkPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPortrait.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPortrait.ForeColor = System.Drawing.Color.Navy;
            this.chkPortrait.Location = new System.Drawing.Point(287, 457);
            this.chkPortrait.Name = "chkPortrait";
            this.chkPortrait.Size = new System.Drawing.Size(97, 23);
            this.chkPortrait.TabIndex = 22;
            this.chkPortrait.Text = "Portrait?";
            this.chkPortrait.ToolTipText = "Thông tin sẽ được lưu lại cho báo cáo trên";
            // 
            // txtTrinhky
            // 
            this.txtTrinhky._AcceptsTab = true;
            this.txtTrinhky._Multiline = true;
            this.txtTrinhky._Readonly = false;
            this.txtTrinhky.AlignCenterVisible = true;
            this.txtTrinhky.AlignLeftVisible = true;
            this.txtTrinhky.AlignRightVisible = true;
            this.txtTrinhky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrinhky.BoldVisible = true;
            this.txtTrinhky.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrinhky.BulletsVisible = true;
            this.txtTrinhky.ChooseFontVisible = true;
            this.txtTrinhky.FindReplaceVisible = true;
            this.txtTrinhky.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrinhky.FontColorVisible = true;
            this.txtTrinhky.FontFamilyVisible = true;
            this.txtTrinhky.FontSizeVisible = true;
            this.txtTrinhky.GroupAlignmentVisible = true;
            this.txtTrinhky.GroupBoldUnderlineItalicVisible = true;
            this.txtTrinhky.GroupFontColorVisible = true;
            this.txtTrinhky.GroupFontNameAndSizeVisible = true;
            this.txtTrinhky.GroupIndentationAndBulletsVisible = true;
            this.txtTrinhky.GroupInsertVisible = true;
            this.txtTrinhky.GroupSaveAndLoadVisible = true;
            this.txtTrinhky.GroupZoomVisible = true;
            this.txtTrinhky.INDENT = 10;
            this.txtTrinhky.IndentVisible = true;
            this.txtTrinhky.InsertPictureVisible = true;
            this.txtTrinhky.ItalicVisible = true;
            this.txtTrinhky.LoadVisible = true;
            this.txtTrinhky.Location = new System.Drawing.Point(105, 52);
            this.txtTrinhky.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTrinhky.myFont = new System.Drawing.Font("Times New Roman", 9F);
            this.txtTrinhky.Name = "txtTrinhky";
            this.txtTrinhky.OutdentVisible = true;
            this.txtTrinhky.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
    " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
            this.txtTrinhky.SaveVisible = true;
            this.txtTrinhky.SelectionLength = 0;
            this.txtTrinhky.SeparatorAlignVisible = true;
            this.txtTrinhky.SeparatorBoldUnderlineItalicVisible = true;
            this.txtTrinhky.SeparatorFontColorVisible = true;
            this.txtTrinhky.SeparatorFontVisible = true;
            this.txtTrinhky.SeparatorIndentAndBulletsVisible = true;
            this.txtTrinhky.SeparatorInsertVisible = true;
            this.txtTrinhky.SeparatorSaveLoadVisible = true;
            this.txtTrinhky.Size = new System.Drawing.Size(876, 397);
            this.txtTrinhky.TabIndex = 21;
            this.txtTrinhky.ToolStripVisible = true;
            this.txtTrinhky.UnderlineVisible = true;
            this.txtTrinhky.WordWrapVisible = true;
            this.txtTrinhky.ZoomFactorTextVisible = true;
            this.txtTrinhky.ZoomInVisible = true;
            this.txtTrinhky.ZoomOutVisible = true;
            // 
            // chkGhiLai
            // 
            this.chkGhiLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGhiLai.Checked = true;
            this.chkGhiLai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhiLai.ForeColor = System.Drawing.Color.Navy;
            this.chkGhiLai.Location = new System.Drawing.Point(105, 457);
            this.chkGhiLai.Name = "chkGhiLai";
            this.chkGhiLai.Size = new System.Drawing.Size(251, 23);
            this.chkGhiLai.TabIndex = 4;
            this.chkGhiLai.Text = "Ghi lại cho lần sau dùng";
            this.chkGhiLai.ToolTipText = "Thông tin sẽ được lưu lại cho báo cáo trên";
            // 
            // txtBaoCao
            // 
            this.txtBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaoCao.Location = new System.Drawing.Point(517, 23);
            this.txtBaoCao.Name = "txtBaoCao";
            this.txtBaoCao.ReadOnly = true;
            this.txtBaoCao.Size = new System.Drawing.Size(464, 21);
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
            this.Label1.Location = new System.Drawing.Point(425, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 15);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Tên báo cáo";
            // 
            // PricTure
            // 
            this.PricTure.BackColor = System.Drawing.SystemColors.Control;
            this.PricTure.Image = ((System.Drawing.Image)(resources.GetObject("PricTure.Image")));
            this.PricTure.Location = new System.Drawing.Point(1, 0);
            this.PricTure.Name = "PricTure";
            this.PricTure.Size = new System.Drawing.Size(59, 55);
            this.PricTure.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PricTure.TabIndex = 14;
            this.PricTure.TabStop = false;
            // 
            // cmdUpdateAllUser
            // 
            this.cmdUpdateAllUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdUpdateAllUser.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateAllUser.Image")));
            this.cmdUpdateAllUser.Location = new System.Drawing.Point(63, 558);
            this.cmdUpdateAllUser.Name = "cmdUpdateAllUser";
            this.cmdUpdateAllUser.Size = new System.Drawing.Size(121, 28);
            this.cmdUpdateAllUser.TabIndex = 15;
            this.cmdUpdateAllUser.Text = "Update All User";
            this.cmdUpdateAllUser.Visible = false;
            this.cmdUpdateAllUser.Click += new System.EventHandler(this.cmdUpdateAllUser_Click);
            // 
            // frm_SignInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 605);
            this.Controls.Add(this.cmdUpdateAllUser);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.PricTure);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdQuit);
            this.Controls.Add(this.sysColor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SignInfor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin trình ký";
            this.ResizeEnd += new System.EventHandler(this.frm_SignInfor_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SignInfor_KeyDown);
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
        public RicherTextBox.RicherTextBox txtTrinhky;
        public Janus.Windows.EditControls.UICheckBox chkPortrait;
        public Janus.Windows.GridEX.EditControls.EditBox txtMabaocao;
        internal System.Windows.Forms.Label label3;
    }
}