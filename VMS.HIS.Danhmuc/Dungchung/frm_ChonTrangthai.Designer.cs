namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonTrangthai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonTrangthai));
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.optCahai = new Janus.Windows.EditControls.UIRadioButton();
            this.optChitiet = new Janus.Windows.EditControls.UIRadioButton();
            this.optTonghop = new Janus.Windows.EditControls.UIRadioButton();
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
            this.cmdAccept.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(123, 178);
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
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(263, 178);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(125, 33);
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
            this.panel1.Size = new System.Drawing.Size(500, 221);
            this.panel1.TabIndex = 3;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtCreateDate);
            this.uiGroupBox1.Controls.Add(this.optCahai);
            this.uiGroupBox1.Controls.Add(this.optChitiet);
            this.uiGroupBox1.Controls.Add(this.optTonghop);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 55);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(500, 117);
            this.uiGroupBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Trạng thái";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ngày in:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.Enabled = false;
            this.dtCreateDate.Location = new System.Drawing.Point(123, 62);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(227, 21);
            this.dtCreateDate.TabIndex = 3;
            // 
            // optCahai
            // 
            this.optCahai.Location = new System.Drawing.Point(369, 24);
            this.optCahai.Name = "optCahai";
            this.optCahai.Size = new System.Drawing.Size(96, 23);
            this.optCahai.TabIndex = 2;
            this.optCahai.TabStop = true;
            this.optCahai.Text = "Cả hai";
            // 
            // optChitiet
            // 
            this.optChitiet.Location = new System.Drawing.Point(233, 24);
            this.optChitiet.Name = "optChitiet";
            this.optChitiet.Size = new System.Drawing.Size(91, 23);
            this.optChitiet.TabIndex = 1;
            this.optChitiet.Text = "In chi tiết";
            // 
            // optTonghop
            // 
            this.optTonghop.Checked = true;
            this.optTonghop.Location = new System.Drawing.Point(123, 24);
            this.optTonghop.Name = "optTonghop";
            this.optTonghop.Size = new System.Drawing.Size(104, 23);
            this.optTonghop.TabIndex = 0;
            this.optTonghop.TabStop = true;
            this.optTonghop.Text = "In tổng hợp";
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
            this.label2.Text = "Mời bạn chọn mẫu in phiếu";
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
            // frm_ChonTrangthai
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 221);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonTrangthai";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn loại phiếu in";
            this.Load += new System.EventHandler(this.frm_ChonTrangthai_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonTrangthai_KeyDown);
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
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIRadioButton optCahai;
        private Janus.Windows.EditControls.UIRadioButton optChitiet;
        private Janus.Windows.EditControls.UIRadioButton optTonghop;
        private System.Windows.Forms.Panel pnlheader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
    }
}