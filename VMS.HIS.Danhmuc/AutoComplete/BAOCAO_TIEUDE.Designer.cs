namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    partial class BAOCAO_TIEUDE
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BAOCAO_TIEUDE));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.cmdBrowse = new Janus.Windows.EditControls.UIButton();
            this.lblExcelFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nmrLine = new System.Windows.Forms.NumericUpDown();
            this.lblPhimtat = new System.Windows.Forms.Label();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.pnlImg = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrLine)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.cmdBrowse);
            this.pnlHeader.Controls.Add(this.lblExcelFile);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.nmrLine);
            this.pnlHeader.Controls.Add(this.lblPhimtat);
            this.pnlHeader.Controls.Add(this.txtTieuDe);
            this.pnlHeader.Controls.Add(this.pnlImg);
            this.pnlHeader.Controls.Add(this.cmdSave);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(894, 54);
            this.pnlHeader.TabIndex = 121;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
            this.cmdBrowse.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdBrowse.Location = new System.Drawing.Point(851, 28);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdBrowse.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdBrowse.Size = new System.Drawing.Size(38, 27);
            this.cmdBrowse.TabIndex = 366;
            this.cmdBrowse.ToolTipText = "Chọn file mẫu Excel";
            this.cmdBrowse.Visible = false;
            this.cmdBrowse.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // lblExcelFile
            // 
            this.lblExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExcelFile.Location = new System.Drawing.Point(652, 32);
            this.lblExcelFile.Name = "lblExcelFile";
            this.lblExcelFile.Size = new System.Drawing.Size(193, 19);
            this.lblExcelFile.TabIndex = 365;
            this.lblExcelFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExcelFile.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(709, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 364;
            this.label1.Text = "Dòng xuất Excel";
            this.label1.Visible = false;
            // 
            // nmrLine
            // 
            this.nmrLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nmrLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrLine.Location = new System.Drawing.Point(800, 6);
            this.nmrLine.Name = "nmrLine";
            this.nmrLine.Size = new System.Drawing.Size(45, 22);
            this.nmrLine.TabIndex = 363;
            this.nmrLine.Visible = false;
            this.nmrLine.ValueChanged += new System.EventHandler(this.nmrLine_ValueChanged);
            // 
            // lblPhimtat
            // 
            this.lblPhimtat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhimtat.Location = new System.Drawing.Point(67, 33);
            this.lblPhimtat.Name = "lblPhimtat";
            this.lblPhimtat.Size = new System.Drawing.Size(822, 21);
            this.lblPhimtat.TabIndex = 362;
            this.lblPhimtat.Text = "Phím tắt";
            this.lblPhimtat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTieuDe.BackColor = System.Drawing.SystemColors.Control;
            this.txtTieuDe.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtTieuDe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTieuDe.Location = new System.Drawing.Point(73, 4);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.ReadOnly = true;
            this.txtTieuDe.Size = new System.Drawing.Size(772, 24);
            this.txtTieuDe.TabIndex = 361;
            this.txtTieuDe.Text = "TIÊU ĐỀ BÁO CÁO";
            this.txtTieuDe.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // pnlImg
            // 
            this.pnlImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlImg.BackgroundImage")));
            this.pnlImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlImg.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlImg.Location = new System.Drawing.Point(0, 0);
            this.pnlImg.Name = "pnlImg";
            this.pnlImg.Size = new System.Drawing.Size(67, 54);
            this.pnlImg.TabIndex = 360;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(851, 2);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdSave.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdSave.Size = new System.Drawing.Size(38, 27);
            this.cmdSave.TabIndex = 359;
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // BAOCAO_TIEUDE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlHeader);
            this.Name = "BAOCAO_TIEUDE";
            this.Size = new System.Drawing.Size(894, 54);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlImg;
        private Janus.Windows.EditControls.UIButton cmdSave;
        public Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        private System.Windows.Forms.Label lblPhimtat;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton cmdBrowse;
        public System.Windows.Forms.NumericUpDown nmrLine;
        public System.Windows.Forms.Label lblExcelFile;
    }
}
