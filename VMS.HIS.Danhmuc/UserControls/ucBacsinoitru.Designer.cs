namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    partial class ucBacsinoitru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBacsinoitru));
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblTheodoi = new System.Windows.Forms.Label();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picBSi = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBSi)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblTheodoi);
            this.pnlInfo.Controls.Add(this.lblKhoa);
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 115);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(202, 105);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblTheodoi
            // 
            this.lblTheodoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTheodoi.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblTheodoi.Location = new System.Drawing.Point(0, 76);
            this.lblTheodoi.Name = "lblTheodoi";
            this.lblTheodoi.Size = new System.Drawing.Size(202, 29);
            this.lblTheodoi.TabIndex = 2;
            this.lblTheodoi.Text = "Đang theo dõi 10 người bệnh";
            this.lblTheodoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKhoa
            // 
            this.lblKhoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKhoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoa.Image = ((System.Drawing.Image)(resources.GetObject("lblKhoa.Image")));
            this.lblKhoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblKhoa.Location = new System.Drawing.Point(0, 29);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(202, 47);
            this.lblKhoa.TabIndex = 1;
            this.lblKhoa.Text = "Khoa nội trú";
            this.lblKhoa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 29);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "BS. Trần Đình Dũng";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBSi
            // 
            this.picBSi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBSi.Image = global::VMS.Resources.Properties.Resources.BSNU;
            this.picBSi.Location = new System.Drawing.Point(0, 0);
            this.picBSi.Name = "picBSi";
            this.picBSi.Size = new System.Drawing.Size(202, 115);
            this.picBSi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBSi.TabIndex = 0;
            this.picBSi.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.picBSi);
            this.panel1.Controls.Add(this.pnlInfo);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 220);
            this.panel1.TabIndex = 3;
            // 
            // ucBacsinoitru
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel1);
            this.Name = "ucBacsinoitru";
            this.Size = new System.Drawing.Size(208, 226);
            this.pnlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBSi)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfo;
        public System.Windows.Forms.Label lblTheodoi;
        public System.Windows.Forms.Label lblKhoa;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.PictureBox picBSi;
        private System.Windows.Forms.Panel panel1;
    }
}
