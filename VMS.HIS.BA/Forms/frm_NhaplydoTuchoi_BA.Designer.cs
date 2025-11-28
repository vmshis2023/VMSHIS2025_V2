namespace VMS.HIS.UI.EMR
{
    partial class frm_NhaplydoTuchoi_BA
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NhaplydoTuchoi_BA));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblName = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtp_ngaytuchoi = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txt_lydotu_choi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_lantuchoi = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1019, 63);
            this.panel1.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.BackColor = System.Drawing.Color.DarkCyan;
            this.lblTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.White;
            this.lblTitle2.Location = new System.Drawing.Point(71, 0);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(946, 61);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "NHẬP LÝ DO TỪ CHỐI HỒ SƠ BỆNH ÁN";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 61);
            this.panel2.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(755, 661);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(886, 661);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(13, 638);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(999, 22);
            this.vbLine1.TabIndex = 9;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Hành động";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Red;
            this.lblName.Location = new System.Drawing.Point(4, 118);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(122, 22);
            this.lblName.TabIndex = 540;
            this.lblName.Text = "Lý do từ chối:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(426, 607);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(580, 27);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(4, 87);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(122, 22);
            this.lblDate.TabIndex = 547;
            this.lblDate.Text = "Ngày từ chối";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_ngaytuchoi
            // 
            this.dtp_ngaytuchoi.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_ngaytuchoi.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtp_ngaytuchoi.DropDownCalendar.Name = "";
            this.dtp_ngaytuchoi.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtp_ngaytuchoi.Enabled = false;
            this.dtp_ngaytuchoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ngaytuchoi.Location = new System.Drawing.Point(132, 87);
            this.dtp_ngaytuchoi.Name = "dtp_ngaytuchoi";
            this.dtp_ngaytuchoi.ShowUpDown = true;
            this.dtp_ngaytuchoi.Size = new System.Drawing.Size(189, 22);
            this.dtp_ngaytuchoi.TabIndex = 0;
            this.dtp_ngaytuchoi.TabStop = false;
            // 
            // txt_lydotu_choi
            // 
            this.txt_lydotu_choi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lydotu_choi.Location = new System.Drawing.Point(132, 117);
            this.txt_lydotu_choi.Multiline = true;
            this.txt_lydotu_choi.Name = "txt_lydotu_choi";
            this.txt_lydotu_choi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_lydotu_choi.Size = new System.Drawing.Size(874, 517);
            this.txt_lydotu_choi.TabIndex = 548;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(327, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 22);
            this.label1.TabIndex = 549;
            this.label1.Text = "Lần từ chối thứ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_lantuchoi
            // 
            this.txt_lantuchoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lantuchoi.Location = new System.Drawing.Point(452, 89);
            this.txt_lantuchoi.Name = "txt_lantuchoi";
            this.txt_lantuchoi.Size = new System.Drawing.Size(52, 22);
            this.txt_lantuchoi.TabIndex = 550;
            // 
            // frm_NhaplydoTuchoi_BA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1019, 708);
            this.Controls.Add(this.txt_lantuchoi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_lydotu_choi);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtp_ngaytuchoi);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vbLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_NhaplydoTuchoi_BA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Từ chối hồ sơ bệnh án";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtp_ngaytuchoi;
        private System.Windows.Forms.TextBox txt_lantuchoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_lydotu_choi;
    }
}