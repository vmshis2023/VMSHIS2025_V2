namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    partial class ucKhoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucKhoa));
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblTruongkhoa = new System.Windows.Forms.Label();
            this.lblsobuong = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picKhoa = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKhoa)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblTruongkhoa);
            this.pnlInfo.Controls.Add(this.lblsobuong);
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 115);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(202, 105);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblTruongkhoa
            // 
            this.lblTruongkhoa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTruongkhoa.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblTruongkhoa.Location = new System.Drawing.Point(0, 76);
            this.lblTruongkhoa.Name = "lblTruongkhoa";
            this.lblTruongkhoa.Size = new System.Drawing.Size(202, 29);
            this.lblTruongkhoa.TabIndex = 2;
            this.lblTruongkhoa.Text = "T.Khoa: GS.VS Trần Thế Mỹ";
            this.lblTruongkhoa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblsobuong
            // 
            this.lblsobuong.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblsobuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsobuong.Image = ((System.Drawing.Image)(resources.GetObject("lblsobuong.Image")));
            this.lblsobuong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblsobuong.Location = new System.Drawing.Point(0, 45);
            this.lblsobuong.Name = "lblsobuong";
            this.lblsobuong.Size = new System.Drawing.Size(202, 31);
            this.lblsobuong.TabIndex = 1;
            this.lblsobuong.Text = "10 buồng";
            this.lblsobuong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 45);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Khoa nội tổng hợp";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picKhoa
            // 
            this.picKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKhoa.Image = ((System.Drawing.Image)(resources.GetObject("picKhoa.Image")));
            this.picKhoa.Location = new System.Drawing.Point(0, 0);
            this.picKhoa.Name = "picKhoa";
            this.picKhoa.Size = new System.Drawing.Size(202, 115);
            this.picKhoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKhoa.TabIndex = 0;
            this.picKhoa.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.picKhoa);
            this.panel1.Controls.Add(this.pnlInfo);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 220);
            this.panel1.TabIndex = 3;
            // 
            // ucKhoa
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel1);
            this.Name = "ucKhoa";
            this.Size = new System.Drawing.Size(208, 226);
            this.pnlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picKhoa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfo;
        public System.Windows.Forms.Label lblTruongkhoa;
        public System.Windows.Forms.Label lblsobuong;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.PictureBox picKhoa;
        private System.Windows.Forms.Panel panel1;
    }
}
