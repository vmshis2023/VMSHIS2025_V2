namespace VMS.QMS
{
    partial class frm_ScreenSoKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ScreenSoKham));
            this.lblQuaySo = new System.Windows.Forms.Label();
            this.cmdConfig = new System.Windows.Forms.Button();
            this.lblTenloaiQMS = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblSoKham = new System.Windows.Forms.Label();
            this.pnlLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuaySo
            // 
            this.lblQuaySo.BackColor = System.Drawing.SystemColors.Control;
            this.lblQuaySo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQuaySo.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.lblQuaySo.ForeColor = System.Drawing.Color.Black;
            this.lblQuaySo.Location = new System.Drawing.Point(0, 61);
            this.lblQuaySo.Name = "lblQuaySo";
            this.lblQuaySo.Size = new System.Drawing.Size(783, 45);
            this.lblQuaySo.TabIndex = 3;
            this.lblQuaySo.Text = "Quầy số 1:";
            this.lblQuaySo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdConfig.BackgroundImage")));
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdConfig.Location = new System.Drawing.Point(740, 68);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(40, 40);
            this.cmdConfig.TabIndex = 1;
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click_1);
            // 
            // lblTenloaiQMS
            // 
            this.lblTenloaiQMS.BackColor = System.Drawing.Color.Transparent;
            this.lblTenloaiQMS.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenloaiQMS.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold);
            this.lblTenloaiQMS.ForeColor = System.Drawing.Color.Black;
            this.lblTenloaiQMS.Location = new System.Drawing.Point(0, 0);
            this.lblTenloaiQMS.Name = "lblTenloaiQMS";
            this.lblTenloaiQMS.Size = new System.Drawing.Size(783, 61);
            this.lblTenloaiQMS.TabIndex = 3;
            this.lblTenloaiQMS.Text = "SỐ THƯỜNG";
            this.lblTenloaiQMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogo.BackgroundImage")));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLogo.Controls.Add(this.cmdConfig);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogo.Location = new System.Drawing.Point(0, 523);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(783, 111);
            this.pnlLogo.TabIndex = 4;
            // 
            // lblSoKham
            // 
            this.lblSoKham.BackColor = System.Drawing.SystemColors.Control;
            this.lblSoKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoKham.Font = new System.Drawing.Font("Times New Roman", 300F, System.Drawing.FontStyle.Bold);
            this.lblSoKham.ForeColor = System.Drawing.Color.Black;
            this.lblSoKham.Location = new System.Drawing.Point(0, 106);
            this.lblSoKham.Name = "lblSoKham";
            this.lblSoKham.Size = new System.Drawing.Size(783, 417);
            this.lblSoKham.TabIndex = 5;
            this.lblSoKham.Text = "1";
            this.lblSoKham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_ScreenSoKham
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(783, 634);
            this.Controls.Add(this.lblSoKham);
            this.Controls.Add(this.lblQuaySo);
            this.Controls.Add(this.lblTenloaiQMS);
            this.Controls.Add(this.pnlLogo);
            this.KeyPreview = true;
            this.Name = "frm_ScreenSoKham";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QMS TIẾP ĐÓN";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_ScreenSoKham_Load);
            this.pnlLogo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblQuaySo;
        private System.Windows.Forms.Button cmdConfig;
        private System.Windows.Forms.Label lblTenloaiQMS;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblSoKham;


    }
}