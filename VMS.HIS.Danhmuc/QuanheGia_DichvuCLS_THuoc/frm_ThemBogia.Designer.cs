namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_ThemBogia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ThemBogia));
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMotathem = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtId = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFrom = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpTo = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkHieuluc = new Janus.Windows.EditControls.UICheckBox();
            this.txtTenbogia = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 9F);
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(12, 124);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 15);
            this.label19.TabIndex = 452;
            this.label19.Text = "Trạng thái:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(241, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 455;
            this.label1.Text = "Mã bộ giá :";
            // 
            // txtMa
            // 
            this.txtMa.BackColor = System.Drawing.Color.White;
            this.txtMa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa.ForeColor = System.Drawing.Color.Black;
            this.txtMa.Location = new System.Drawing.Point(319, 12);
            this.txtMa.MaxLength = 30;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(123, 23);
            this.txtMa.TabIndex = 1;
            this.txtMa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtMotathem
            // 
            this.txtMotathem.BackColor = System.Drawing.Color.White;
            this.txtMotathem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotathem.ForeColor = System.Drawing.Color.Black;
            this.txtMotathem.Location = new System.Drawing.Point(112, 90);
            this.txtMotathem.MaxLength = 500;
            this.txtMotathem.Name = "txtMotathem";
            this.txtMotathem.Size = new System.Drawing.Size(460, 23);
            this.txtMotathem.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 459;
            this.label2.Text = "Mô tả bộ giá:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(112, 12);
            this.txtId.MaxLength = 50;
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(123, 23);
            this.txtId.TabIndex = 0;
            this.txtId.TabStop = false;
            this.txtId.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 559;
            this.label3.Text = "Id";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpFrom.DropDownCalendar.Name = "";
            this.dtpFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpFrom.Location = new System.Drawing.Point(112, 65);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(123, 21);
            this.dtpFrom.TabIndex = 3;
            this.dtpFrom.Value = new System.DateTime(2013, 8, 10, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 561;
            this.label4.Text = "Hiệu lực từ :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(241, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 563;
            this.label5.Text = "đến ngày :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpTo.DropDownCalendar.Name = "";
            this.dtpTo.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpTo.ForeColor = System.Drawing.Color.Black;
            this.dtpTo.Location = new System.Drawing.Point(319, 67);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(123, 21);
            this.dtpTo.TabIndex = 4;
            this.dtpTo.Value = new System.DateTime(2013, 8, 10, 0, 0, 0, 0);
            // 
            // chkHieuluc
            // 
            this.chkHieuluc.Checked = true;
            this.chkHieuluc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHieuluc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHieuluc.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkHieuluc.Location = new System.Drawing.Point(112, 119);
            this.chkHieuluc.Name = "chkHieuluc";
            this.chkHieuluc.Size = new System.Drawing.Size(162, 23);
            this.chkHieuluc.TabIndex = 6;
            this.chkHieuluc.Text = "Hiệu lực?";
            // 
            // txtTenbogia
            // 
            this.txtTenbogia.BackColor = System.Drawing.Color.White;
            this.txtTenbogia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenbogia.ForeColor = System.Drawing.Color.Black;
            this.txtTenbogia.Location = new System.Drawing.Point(112, 38);
            this.txtTenbogia.MaxLength = 255;
            this.txtTenbogia.Name = "txtTenbogia";
            this.txtTenbogia.Size = new System.Drawing.Size(330, 23);
            this.txtTenbogia.TabIndex = 2;
            this.txtTenbogia.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(34, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 565;
            this.label6.Text = "Tên bộ giá:";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(443, 312);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 37);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.CmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(307, 312);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(129, 37);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.CmdSave_Click);
            // 
            // frm_ThemBogia
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkHieuluc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMotathem);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.txtTenbogia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label19);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ThemBogia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin bộ giá BHYT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public Janus.Windows.GridEX.EditControls.EditBox txtMa;
        public Janus.Windows.GridEX.EditControls.EditBox txtMotathem;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        public Janus.Windows.GridEX.EditControls.EditBox txtId;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpTo;
        private Janus.Windows.EditControls.UICheckBox chkHieuluc;
        public Janus.Windows.GridEX.EditControls.EditBox txtTenbogia;
        private System.Windows.Forms.Label label6;
    }
}