
namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    partial class ucNguoiKy
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
            this.pic = new System.Windows.Forms.PictureBox();
            this.lnkNguoiky = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Left;
            this.pic.Image = global::VMS.HIS.EMR.Properties.Resources.tick2;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(30, 27);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic.TabIndex = 2;
            this.pic.TabStop = false;
            // 
            // lnkNguoiky
            // 
            this.lnkNguoiky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lnkNguoiky.Location = new System.Drawing.Point(30, 0);
            this.lnkNguoiky.Name = "lnkNguoiky";
            this.lnkNguoiky.Size = new System.Drawing.Size(180, 27);
            this.lnkNguoiky.TabIndex = 3;
            this.lnkNguoiky.TabStop = true;
            this.lnkNguoiky.Text = "ThS.BS. Phan Nguyễn Hoàng Vân";
            this.lnkNguoiky.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkNguoiky.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNguoiky_LinkClicked);
            // 
            // ucNguoiKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkNguoiky);
            this.Controls.Add(this.pic);
            this.Name = "ucNguoiKy";
            this.Size = new System.Drawing.Size(210, 27);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.LinkLabel lnkNguoiky;
    }
}
