using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonTungayDenngay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonTungayDenngay));
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_denngay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_tungay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.opt_tungay_denngay = new Janus.Windows.EditControls.UIRadioButton();
            this.opt_trongngay = new Janus.Windows.EditControls.UIRadioButton();
            this.pnlheader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.pnlheader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(229, 217);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(125, 33);
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "Chấp nhận";
            this.toolTip1.SetToolTip(this.cmdAccept, "Có thể nhấn tổ hợp phím Ctrl+A hoặc Ctrl+S để chấp nhận ngày thanh toán đang chọn" +
        "");
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(369, 217);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 33);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn phím Escape để thoát");
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiGroupBox1);
            this.panel1.Controls.Add(this.cmdAccept);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.pnlheader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 262);
            this.panel1.TabIndex = 3;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.dtp_denngay);
            this.uiGroupBox1.Controls.Add(this.lblMsg);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtp_tungay);
            this.uiGroupBox1.Controls.Add(this.opt_tungay_denngay);
            this.uiGroupBox1.Controls.Add(this.opt_trongngay);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 55);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(500, 156);
            this.uiGroupBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(254, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "đến ngày:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_denngay
            // 
            this.dtp_denngay.CustomFormat = "dd/MM/yyyy ";
            this.dtp_denngay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtp_denngay.DropDownCalendar.Name = "";
            this.dtp_denngay.Enabled = false;
            this.dtp_denngay.Location = new System.Drawing.Point(322, 62);
            this.dtp_denngay.Name = "dtp_denngay";
            this.dtp_denngay.ShowUpDown = true;
            this.dtp_denngay.Size = new System.Drawing.Size(125, 21);
            this.dtp_denngay.TabIndex = 3;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 129);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(494, 24);
            this.lblMsg.TabIndex = 6;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Từ ngày:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_tungay
            // 
            this.dtp_tungay.CustomFormat = "dd/MM/yyyy ";
            this.dtp_tungay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtp_tungay.DropDownCalendar.Name = "";
            this.dtp_tungay.Enabled = false;
            this.dtp_tungay.Location = new System.Drawing.Point(123, 62);
            this.dtp_tungay.Name = "dtp_tungay";
            this.dtp_tungay.ShowUpDown = true;
            this.dtp_tungay.Size = new System.Drawing.Size(125, 21);
            this.dtp_tungay.TabIndex = 2;
            // 
            // opt_tungay_denngay
            // 
            this.opt_tungay_denngay.Location = new System.Drawing.Point(233, 21);
            this.opt_tungay_denngay.Name = "opt_tungay_denngay";
            this.opt_tungay_denngay.Size = new System.Drawing.Size(130, 23);
            this.opt_tungay_denngay.TabIndex = 1;
            this.opt_tungay_denngay.TabStop = true;
            this.opt_tungay_denngay.Text = "Từ ngày đến ngày";
            this.opt_tungay_denngay.CheckedChanged += new System.EventHandler(this.opt_tungay_denngay_CheckedChanged);
            // 
            // opt_trongngay
            // 
            this.opt_trongngay.Checked = true;
            this.opt_trongngay.Location = new System.Drawing.Point(123, 21);
            this.opt_trongngay.Name = "opt_trongngay";
            this.opt_trongngay.Size = new System.Drawing.Size(104, 23);
            this.opt_trongngay.TabIndex = 0;
            this.opt_trongngay.TabStop = true;
            this.opt_trongngay.Text = "Trong ngày";
            this.opt_trongngay.CheckedChanged += new System.EventHandler(this.opt_trongngay_CheckedChanged);
            // 
            // pnlheader
            // 
            this.pnlheader.Controls.Add(this.label2);
            this.pnlheader.Controls.Add(this.panel2);
            this.pnlheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlheader.Location = new System.Drawing.Point(0, 0);
            this.pnlheader.Name = "pnlheader";
            this.pnlheader.Size = new System.Drawing.Size(500, 55);
            this.pnlheader.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(73, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(427, 55);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chọn ngày báo cáo hoặc ngày thực hiện";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(73, 55);
            this.panel2.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // frm_ChonTungayDenngay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 262);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonTungayDenngay";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn ngày";
            this.Load += new System.EventHandler(this.frm_ChonTungayDenngay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonTungayDenngay_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.pnlheader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlheader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label3;
        public Janus.Windows.CalendarCombo.CalendarCombo dtp_tungay;
        public Janus.Windows.EditControls.UIRadioButton opt_tungay_denngay;
        public Janus.Windows.EditControls.UIRadioButton opt_trongngay;
        public Janus.Windows.CalendarCombo.CalendarCombo dtp_denngay;
    }
}