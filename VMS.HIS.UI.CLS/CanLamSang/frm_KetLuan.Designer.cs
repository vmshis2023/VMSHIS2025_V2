namespace VNS.HIS.UI.Forms.HinhAnh
{
    partial class frm_KetLuan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KetLuan));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtKetLuan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdNhapKetLuan = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdNhapKetLuan);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 247);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(419, 49);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Chức năng";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txtKetLuan);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(419, 247);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "&Thông tin kết luận";
            // 
            // txtKetLuan
            // 
            this.txtKetLuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKetLuan.Location = new System.Drawing.Point(3, 16);
            this.txtKetLuan.Multiline = true;
            this.txtKetLuan.Name = "txtKetLuan";
            this.txtKetLuan.Size = new System.Drawing.Size(413, 228);
            this.txtKetLuan.TabIndex = 1;
            // 
            // cmdNhapKetLuan
            // 
            this.cmdNhapKetLuan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNhapKetLuan.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhapKetLuan.Image")));
            this.cmdNhapKetLuan.Location = new System.Drawing.Point(107, 16);
            this.cmdNhapKetLuan.Name = "cmdNhapKetLuan";
            this.cmdNhapKetLuan.Size = new System.Drawing.Size(93, 27);
            this.cmdNhapKetLuan.TabIndex = 13;
            this.cmdNhapKetLuan.Text = "&Kết luận";
            this.cmdNhapKetLuan.Click += new System.EventHandler(this.cmdNhapKetLuan_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(206, 16);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(89, 28);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frm_KetLuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 296);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_KetLuan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin kết luận";
            this.Load += new System.EventHandler(this.frm_KetLuan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.EditControls.EditBox txtKetLuan;
        private Janus.Windows.EditControls.UIButton cmdNhapKetLuan;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}