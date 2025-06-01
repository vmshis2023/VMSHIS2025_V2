namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    partial class ucGiuong
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
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblDangnam = new System.Windows.Forms.Label();
            this.lblSoluotkham = new System.Windows.Forms.Label();
            this.lblSotien = new System.Windows.Forms.Label();
            this.lnkViewHistory = new System.Windows.Forms.LinkLabel();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picKhoa = new System.Windows.Forms.PictureBox();
            this.pnlInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKhoa)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblDangnam);
            this.pnlInfo.Controls.Add(this.lblSoluotkham);
            this.pnlInfo.Controls.Add(this.lblSotien);
            this.pnlInfo.Controls.Add(this.lnkViewHistory);
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 115);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(202, 105);
            this.pnlInfo.TabIndex = 0;
            // 
            // lblDangnam
            // 
            this.lblDangnam.BackColor = System.Drawing.Color.Transparent;
            this.lblDangnam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDangnam.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDangnam.ForeColor = System.Drawing.Color.Navy;
            this.lblDangnam.Location = new System.Drawing.Point(0, 25);
            this.lblDangnam.Name = "lblDangnam";
            this.lblDangnam.Size = new System.Drawing.Size(202, 20);
            this.lblDangnam.TabIndex = 6;
            this.lblDangnam.Text = "Đang nằm: 1";
            this.lblDangnam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoluotkham
            // 
            this.lblSoluotkham.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSoluotkham.Font = new System.Drawing.Font("Arial", 9F);
            this.lblSoluotkham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSoluotkham.Location = new System.Drawing.Point(0, 45);
            this.lblSoluotkham.Name = "lblSoluotkham";
            this.lblSoluotkham.Size = new System.Drawing.Size(202, 20);
            this.lblSoluotkham.TabIndex = 4;
            this.lblSoluotkham.Text = "Số người bệnh đã nằm: 1000";
            this.lblSoluotkham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSotien
            // 
            this.lblSotien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSotien.Font = new System.Drawing.Font("Arial", 9F);
            this.lblSotien.ForeColor = System.Drawing.Color.Green;
            this.lblSotien.Location = new System.Drawing.Point(0, 65);
            this.lblSotien.Name = "lblSotien";
            this.lblSotien.Size = new System.Drawing.Size(202, 20);
            this.lblSotien.TabIndex = 5;
            this.lblSotien.Text = "Tổng tiền: 1,999,999,999";
            this.lblSotien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkViewHistory
            // 
            this.lnkViewHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnkViewHistory.Font = new System.Drawing.Font("Arial", 9F);
            this.lnkViewHistory.Image = global::VMS.HIS.Danhmuc.Properties.Resources.clock_24;
            this.lnkViewHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkViewHistory.Location = new System.Drawing.Point(0, 85);
            this.lnkViewHistory.Name = "lnkViewHistory";
            this.lnkViewHistory.Size = new System.Drawing.Size(202, 20);
            this.lnkViewHistory.TabIndex = 3;
            this.lnkViewHistory.TabStop = true;
            this.lnkViewHistory.Text = "Xem lịch sử";
            this.lnkViewHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkViewHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewHistory_LinkClicked);
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Giường số 5";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // picKhoa
            // 
            this.picKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picKhoa.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Bed_no196;
            this.picKhoa.Location = new System.Drawing.Point(0, 0);
            this.picKhoa.Name = "picKhoa";
            this.picKhoa.Size = new System.Drawing.Size(202, 115);
            this.picKhoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKhoa.TabIndex = 0;
            this.picKhoa.TabStop = false;
            // 
            // ucGiuong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.panel1);
            this.Name = "ucGiuong";
            this.Size = new System.Drawing.Size(208, 226);
            this.pnlInfo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picKhoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfo;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.PictureBox picKhoa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkViewHistory;
        public System.Windows.Forms.Label lblSoluotkham;
        public System.Windows.Forms.Label lblSotien;
        public System.Windows.Forms.Label lblDangnam;
    }
}
