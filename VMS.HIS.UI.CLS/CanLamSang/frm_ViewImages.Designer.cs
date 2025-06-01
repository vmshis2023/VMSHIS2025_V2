namespace VNS.HIS.UI.Forms.CanLamSang
{
    partial class frm_ViewImages
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
            this.pic4 = new System.Windows.Forms.Integration.ElementHost();
            this.imgBox1 = new WPF.UCs.ImgBox();
            this.pic3 = new System.Windows.Forms.Integration.ElementHost();
            this.imgBox2 = new WPF.UCs.ImgBox();
            this.pic2 = new System.Windows.Forms.Integration.ElementHost();
            this.imgBox3 = new WPF.UCs.ImgBox();
            this.pic1 = new System.Windows.Forms.Integration.ElementHost();
            this.imgBox4 = new WPF.UCs.ImgBox();
            this.SuspendLayout();
            // 
            // pic4
            // 
            this.pic4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic4.Location = new System.Drawing.Point(394, 284);
            this.pic4.Name = "pic4";
            this.pic4.Size = new System.Drawing.Size(370, 270);
            this.pic4.TabIndex = 484;
            this.pic4.TabStop = false;
            this.pic4.Text = "elementHost3";
            this.pic4.Child = this.imgBox1;
            // 
            // pic3
            // 
            this.pic3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic3.Location = new System.Drawing.Point(18, 284);
            this.pic3.Name = "pic3";
            this.pic3.Size = new System.Drawing.Size(370, 270);
            this.pic3.TabIndex = 483;
            this.pic3.TabStop = false;
            this.pic3.Text = "elementHost4";
            this.pic3.Child = this.imgBox2;
            // 
            // pic2
            // 
            this.pic2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic2.Location = new System.Drawing.Point(394, 8);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(370, 270);
            this.pic2.TabIndex = 482;
            this.pic2.TabStop = false;
            this.pic2.Text = "elementHost2";
            this.pic2.Child = this.imgBox3;
            // 
            // pic1
            // 
            this.pic1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic1.Location = new System.Drawing.Point(18, 8);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(370, 270);
            this.pic1.TabIndex = 481;
            this.pic1.TabStop = false;
            this.pic1.Text = "elementHost1";
            this.pic1.Child = this.imgBox4;
            // 
            // frm_ViewImages
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 560);
            this.Controls.Add(this.pic4);
            this.Controls.Add(this.pic3);
            this.Controls.Add(this.pic2);
            this.Controls.Add(this.pic1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ViewImages";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem ảnh Nội soi, siêu âm...(Nhấn F5 để nạp lại ảnh)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost pic4;
        private WPF.UCs.ImgBox imgBox1;
        private System.Windows.Forms.Integration.ElementHost pic3;
        private WPF.UCs.ImgBox imgBox2;
        private System.Windows.Forms.Integration.ElementHost pic2;
        private WPF.UCs.ImgBox imgBox3;
        private System.Windows.Forms.Integration.ElementHost pic1;
        private WPF.UCs.ImgBox imgBox4;

    }
}