namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonngayThanhtoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonngayThanhtoan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.radEditDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radRegisterDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radCurrentDate = new Janus.Windows.EditControls.UIRadioButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiGroupBox1);
            this.panel1.Controls.Add(this.uiGroupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 300);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(100, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(484, 61);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hệ thống phát hiện ngày thanh toán khác với ngày đăng ký khám chữa bệnh của Bệnh " +
    "nhân. Mời bạn xác nhận ngày thanh toán thực tế";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 61);
            this.panel2.TabIndex = 6;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtCreateDate);
            this.uiGroupBox1.Controls.Add(this.radEditDate);
            this.uiGroupBox1.Controls.Add(this.radRegisterDate);
            this.uiGroupBox1.Controls.Add(this.radCurrentDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(584, 232);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ngày thanh toán:";
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtCreateDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCreateDate.Location = new System.Drawing.Point(219, 77);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ReadOnly = true;
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(195, 23);
            this.dtCreateDate.TabIndex = 8;
            this.dtCreateDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // radEditDate
            // 
            this.radEditDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEditDate.Location = new System.Drawing.Point(377, 32);
            this.radEditDate.Name = "radEditDate";
            this.radEditDate.Size = new System.Drawing.Size(97, 27);
            this.radEditDate.TabIndex = 7;
            this.radEditDate.TabStop = true;
            this.radEditDate.Text = "Tùy chỉnh";
            this.radEditDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // radRegisterDate
            // 
            this.radRegisterDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRegisterDate.Location = new System.Drawing.Point(219, 32);
            this.radRegisterDate.Name = "radRegisterDate";
            this.radRegisterDate.Size = new System.Drawing.Size(152, 27);
            this.radRegisterDate.TabIndex = 6;
            this.radRegisterDate.Text = "Ngày đăng ký KCB";
            this.radRegisterDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // radCurrentDate
            // 
            this.radCurrentDate.Checked = true;
            this.radCurrentDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCurrentDate.Location = new System.Drawing.Point(90, 32);
            this.radCurrentDate.Name = "radCurrentDate";
            this.radCurrentDate.Size = new System.Drawing.Size(121, 27);
            this.radCurrentDate.TabIndex = 5;
            this.radCurrentDate.TabStop = true;
            this.radCurrentDate.Text = "Ngày hiện tại";
            this.radCurrentDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cmdAccept);
            this.uiGroupBox2.Controls.Add(this.cmdExit);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 232);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(584, 68);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(322, 17);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(115, 39);
            this.cmdAccept.TabIndex = 553;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(455, 17);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(117, 39);
            this.cmdExit.TabIndex = 554;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // frm_ChonngayThanhtoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonngayThanhtoan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin cấu hình ngày thanh toán";
            this.Load += new System.EventHandler(this.frm_ChonngayThanhtoan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonngayThanhtoan_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIRadioButton radEditDate;
        private Janus.Windows.EditControls.UIRadioButton radRegisterDate;
        private Janus.Windows.EditControls.UIRadioButton radCurrentDate;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}