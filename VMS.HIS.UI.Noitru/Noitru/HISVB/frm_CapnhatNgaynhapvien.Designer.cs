namespace VNS.HIS.UI.NOITRU
{
    partial class frm_CapnhatNgaynhapvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CapnhatNgaynhapvien));
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.dtNgayNhapVien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtngaytiepdon = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.ucThongtinnguoibenh1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(229, 258);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Lưu lại";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(355, 258);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "Thoát Form";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // dtNgayNhapVien
            // 
            this.dtNgayNhapVien.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtNgayNhapVien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayNhapVien.DropDownCalendar.Name = "";
            this.dtNgayNhapVien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayNhapVien.Location = new System.Drawing.Point(111, 139);
            this.dtNgayNhapVien.Name = "dtNgayNhapVien";
            this.dtNgayNhapVien.ShowUpDown = true;
            this.dtNgayNhapVien.Size = new System.Drawing.Size(233, 21);
            this.dtNgayNhapVien.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ngày nhập viện";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày tiếp đón";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtngaytiepdon
            // 
            this.dtngaytiepdon.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtngaytiepdon.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtngaytiepdon.DropDownCalendar.Name = "";
            this.dtngaytiepdon.Enabled = false;
            this.dtngaytiepdon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtngaytiepdon.Location = new System.Drawing.Point(111, 112);
            this.dtngaytiepdon.Name = "dtngaytiepdon";
            this.dtngaytiepdon.ShowUpDown = true;
            this.dtngaytiepdon.Size = new System.Drawing.Size(233, 21);
            this.dtngaytiepdon.TabIndex = 7;
            // 
            // ucThongtinnguoibenh1
            // 
            this.ucThongtinnguoibenh1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh1.Name = "ucThongtinnguoibenh1";
            this.ucThongtinnguoibenh1.Size = new System.Drawing.Size(487, 109);
            this.ucThongtinnguoibenh1.TabIndex = 9;
            // 
            // frm_CapnhatNgaynhapvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 305);
            this.Controls.Add(this.ucThongtinnguoibenh1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtngaytiepdon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.dtNgayNhapVien);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CapnhatNgaynhapvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update thông tin ngày";
            this.Load += new System.EventHandler(this.frm_CapnhatNgaynhapvien_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_CapnhatNgaynhapvien_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label3;
        public Janus.Windows.CalendarCombo.CalendarCombo dtNgayNhapVien;
        private System.Windows.Forms.Label label5;
        public Janus.Windows.CalendarCombo.CalendarCombo dtngaytiepdon;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh ucThongtinnguoibenh1;
    }
}