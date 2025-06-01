namespace VNS.Libs
{
    partial class ReportTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportTitle));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPhimtat = new System.Windows.Forms.Label();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.pnlImg = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlHeader.Controls.Add(this.lblPhimtat);
            this.pnlHeader.Controls.Add(this.txtTieuDe);
            this.pnlHeader.Controls.Add(this.pnlImg);
            this.pnlHeader.Controls.Add(this.cmdSave);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(894, 51);
            this.pnlHeader.TabIndex = 121;
            // 
            // lblPhimtat
            // 
            this.lblPhimtat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPhimtat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhimtat.Location = new System.Drawing.Point(67, 30);
            this.lblPhimtat.Name = "lblPhimtat";
            this.lblPhimtat.Size = new System.Drawing.Size(827, 21);
            this.lblPhimtat.TabIndex = 362;
            this.lblPhimtat.Text = "Phím tắt";
            this.lblPhimtat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTieuDe.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txtTieuDe.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtTieuDe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTieuDe.Location = new System.Drawing.Point(73, 4);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.ReadOnly = true;
            this.txtTieuDe.Size = new System.Drawing.Size(779, 24);
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
            this.pnlImg.Size = new System.Drawing.Size(67, 51);
            this.pnlImg.TabIndex = 360;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(851, 3);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdSave.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdSave.Size = new System.Drawing.Size(38, 27);
            this.cmdSave.TabIndex = 359;
            this.cmdSave.Visible = false;
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // ReportTitle
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlHeader);
            this.Name = "ReportTitle";
            this.Size = new System.Drawing.Size(894, 51);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlImg;
        private Janus.Windows.EditControls.UIButton cmdSave;
        public Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        private System.Windows.Forms.Label lblPhimtat;
    }
}
