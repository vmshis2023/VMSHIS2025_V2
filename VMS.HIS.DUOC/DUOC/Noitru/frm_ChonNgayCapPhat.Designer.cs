namespace VNS.HIS.UI.THUOC
{
    partial class frm_ChonNgayCapPhat
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
            this.cmdChapNhan = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.dtNgaycapPhat = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdChapNhan
            // 
            this.cmdChapNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChapNhan.Location = new System.Drawing.Point(61, 81);
            this.cmdChapNhan.Name = "cmdChapNhan";
            this.cmdChapNhan.Size = new System.Drawing.Size(101, 25);
            this.cmdChapNhan.TabIndex = 0;
            this.cmdChapNhan.Text = "&Chấp nhận";
            this.cmdChapNhan.UseVisualStyleBackColor = true;
            this.cmdChapNhan.Click += new System.EventHandler(this.cmdChapNhan_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(168, 81);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(101, 25);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtNgaycapPhat);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(315, 75);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "&Chọn ngày";
            // 
            // dtNgaycapPhat
            // 
            this.dtNgaycapPhat.CustomFormat = "dd/MM/yyyy";
            this.dtNgaycapPhat.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgaycapPhat.DropDownCalendar.Name = "";
            this.dtNgaycapPhat.Location = new System.Drawing.Point(96, 26);
            this.dtNgaycapPhat.Name = "dtNgaycapPhat";
            this.dtNgaycapPhat.ShowUpDown = true;
            this.dtNgaycapPhat.Size = new System.Drawing.Size(200, 20);
            this.dtNgaycapPhat.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Ngày cấp phát";
            // 
            // frm_ChonNgayCapPhat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 116);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdChapNhan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonNgayCapPhat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn ngày cấp phát";
            this.Load += new System.EventHandler(this.frm_ChonNgayCapPhat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdChapNhan;
        private System.Windows.Forms.Button cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgaycapPhat;
    }
}