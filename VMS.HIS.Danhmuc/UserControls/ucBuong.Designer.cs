namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    partial class ucBuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBuong));
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblgiuongtrong = new System.Windows.Forms.Label();
            this.lblsogiuong = new System.Windows.Forms.Label();
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
            this.pnlInfo.Controls.Add(this.lblgiuongtrong);
            this.pnlInfo.Controls.Add(this.lblsogiuong);
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 136);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(202, 84);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblgiuongtrong
            // 
            this.lblgiuongtrong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblgiuongtrong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgiuongtrong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblgiuongtrong.Location = new System.Drawing.Point(0, 55);
            this.lblgiuongtrong.Name = "lblgiuongtrong";
            this.lblgiuongtrong.Size = new System.Drawing.Size(202, 29);
            this.lblgiuongtrong.TabIndex = 2;
            this.lblgiuongtrong.Text = "Giường trống: 15";
            this.lblgiuongtrong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblsogiuong
            // 
            this.lblsogiuong.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblsogiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsogiuong.ForeColor = System.Drawing.Color.Green;
            this.lblsogiuong.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Bed_no24;
            this.lblsogiuong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblsogiuong.Location = new System.Drawing.Point(0, 27);
            this.lblsogiuong.Name = "lblsogiuong";
            this.lblsogiuong.Size = new System.Drawing.Size(202, 31);
            this.lblsogiuong.TabIndex = 1;
            this.lblsogiuong.Text = "20 giường";
            this.lblsogiuong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 27);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Buồng 10";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picKhoa
            // 
            this.picKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKhoa.Image = ((System.Drawing.Image)(resources.GetObject("picKhoa.Image")));
            this.picKhoa.Location = new System.Drawing.Point(0, 0);
            this.picKhoa.Name = "picKhoa";
            this.picKhoa.Size = new System.Drawing.Size(202, 136);
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
            // ucBuong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel1);
            this.Name = "ucBuong";
            this.Size = new System.Drawing.Size(208, 226);
            this.pnlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picKhoa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfo;
        public System.Windows.Forms.Label lblgiuongtrong;
        public System.Windows.Forms.Label lblsogiuong;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.PictureBox picKhoa;
        private System.Windows.Forms.Panel panel1;
    }
}
