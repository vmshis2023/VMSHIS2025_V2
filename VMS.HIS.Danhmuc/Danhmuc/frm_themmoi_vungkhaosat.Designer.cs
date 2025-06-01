namespace VNS.HIS.UI.HinhAnh
{
    partial class frm_themmoi_vungkhaosat
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel5 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel6 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_vungkhaosat));
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKichthuocanh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtNoidung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboLoaiDvu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKet_Luan = new System.Windows.Forms.RichTextBox();
            this.txtMoTa = new System.Windows.Forms.WebBrowser();
            this.txtDenghi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFileMau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenVungKs = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdVungKs = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkTrangthai = new Janus.Windows.EditControls.UICheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkChoPhepNhapLienTuc = new Janus.Windows.EditControls.UICheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 706);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel4.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel4.Key = "";
            uiStatusBarPanel4.ProgressBarValue = 0;
            uiStatusBarPanel4.Text = "Ctrl+S: Lưu thông tin ";
            uiStatusBarPanel4.Width = 130;
            uiStatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel5.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel5.Key = "";
            uiStatusBarPanel5.ProgressBarValue = 0;
            uiStatusBarPanel5.Text = "Esc:Thoát Form hiện tại";
            uiStatusBarPanel5.Width = 145;
            uiStatusBarPanel6.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel6.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel6.Key = "";
            uiStatusBarPanel6.ProgressBarValue = 0;
            uiStatusBarPanel6.Text = "F5: Thêm mới";
            uiStatusBarPanel6.Width = 92;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel4,
            uiStatusBarPanel5,
            uiStatusBarPanel6});
            this.uiStatusBar1.Size = new System.Drawing.Size(1008, 23);
            this.uiStatusBar1.TabIndex = 100;
            this.uiStatusBar1.TabStop = false;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(876, 665);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(750, 665);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // grpControl
            // 
            this.grpControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.txtKichthuocanh);
            this.grpControl.Controls.Add(this.txtNoidung);
            this.grpControl.Controls.Add(this.cboLoaiDvu);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.txtKet_Luan);
            this.grpControl.Controls.Add(this.txtMoTa);
            this.grpControl.Controls.Add(this.txtDenghi);
            this.grpControl.Controls.Add(this.label10);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.txtFileMau);
            this.grpControl.Controls.Add(this.label9);
            this.grpControl.Controls.Add(this.txtMa);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.txtTenVungKs);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtIdVungKs);
            this.grpControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.ForeColor = System.Drawing.Color.Black;
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(1008, 655);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "Thông tin Vùng khảo sát";
            this.grpControl.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(712, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 103;
            this.label7.Text = "Kích thước ảnh:";
            // 
            // txtKichthuocanh
            // 
            this.txtKichthuocanh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKichthuocanh.BackColor = System.Drawing.Color.White;
            this.txtKichthuocanh.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtKichthuocanh.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtKichthuocanh.Location = new System.Drawing.Point(811, 40);
            this.txtKichthuocanh.Name = "txtKichthuocanh";
            this.txtKichthuocanh.Size = new System.Drawing.Size(185, 21);
            this.txtKichthuocanh.TabIndex = 102;
            // 
            // txtNoidung
            // 
            this.txtNoidung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoidung.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtNoidung.Location = new System.Drawing.Point(58, 307);
            this.txtNoidung.Multiline = true;
            this.txtNoidung.Name = "txtNoidung";
            this.txtNoidung.Size = new System.Drawing.Size(10, 25);
            this.txtNoidung.TabIndex = 30;
            this.txtNoidung.Visible = false;
            // 
            // cboLoaiDvu
            // 
            this.cboLoaiDvu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiDvu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDvu.FormattingEnabled = true;
            this.cboLoaiDvu.Location = new System.Drawing.Point(573, 14);
            this.cboLoaiDvu.Name = "cboLoaiDvu";
            this.cboLoaiDvu.Size = new System.Drawing.Size(423, 23);
            this.cboLoaiDvu.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(472, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Loại dịch vụ:";
            // 
            // txtKet_Luan
            // 
            this.txtKet_Luan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKet_Luan.BackColor = System.Drawing.Color.White;
            this.txtKet_Luan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKet_Luan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKet_Luan.Location = new System.Drawing.Point(104, 498);
            this.txtKet_Luan.Name = "txtKet_Luan";
            this.txtKet_Luan.Size = new System.Drawing.Size(892, 114);
            this.txtKet_Luan.TabIndex = 5;
            this.txtKet_Luan.Text = "";
            this.txtKet_Luan.Visible = false;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoTa.Location = new System.Drawing.Point(104, 72);
            this.txtMoTa.MinimumSize = new System.Drawing.Size(20, 20);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScriptErrorsSuppressed = true;
            this.txtMoTa.Size = new System.Drawing.Size(892, 573);
            this.txtMoTa.TabIndex = 4;
            // 
            // txtDenghi
            // 
            this.txtDenghi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDenghi.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtDenghi.Location = new System.Drawing.Point(104, 620);
            this.txtDenghi.Multiline = true;
            this.txtDenghi.Name = "txtDenghi";
            this.txtDenghi.Size = new System.Drawing.Size(814, 25);
            this.txtDenghi.TabIndex = 6;
            this.txtDenghi.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(472, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 15);
            this.label10.TabIndex = 27;
            this.label10.Text = "Tên file mẫu KQ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 625);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Đề nghị:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 498);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kết luận";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Mô tả";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileMau
            // 
            this.txtFileMau.BackColor = System.Drawing.Color.White;
            this.txtFileMau.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtFileMau.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtFileMau.Location = new System.Drawing.Point(573, 42);
            this.txtFileMau.MaxLength = 100;
            this.txtFileMau.Name = "txtFileMau";
            this.txtFileMau.Size = new System.Drawing.Size(133, 21);
            this.txtFileMau.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(209, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Mã vùng KS";
            // 
            // txtMa
            // 
            this.txtMa.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtMa.Location = new System.Drawing.Point(301, 19);
            this.txtMa.MaxLength = 30;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(157, 21);
            this.txtMa.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(12, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tên vùng KS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenVungKs
            // 
            this.txtTenVungKs.BackColor = System.Drawing.Color.White;
            this.txtTenVungKs.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtTenVungKs.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtTenVungKs.Location = new System.Drawing.Point(104, 45);
            this.txtTenVungKs.Name = "txtTenVungKs";
            this.txtTenVungKs.Size = new System.Drawing.Size(354, 21);
            this.txtTenVungKs.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID vùng KS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdVungKs
            // 
            this.txtIdVungKs.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtIdVungKs.Enabled = false;
            this.txtIdVungKs.Location = new System.Drawing.Point(104, 20);
            this.txtIdVungKs.MaxLength = 10;
            this.txtIdVungKs.Name = "txtIdVungKs";
            this.txtIdVungKs.Size = new System.Drawing.Size(100, 21);
            this.txtIdVungKs.TabIndex = 0;
            this.txtIdVungKs.TabStop = false;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTrangthai.AutoSize = true;
            this.chkTrangthai.Checked = true;
            this.chkTrangthai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangthai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrangthai.Location = new System.Drawing.Point(210, 676);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(70, 18);
            this.chkTrangthai.TabIndex = 101;
            this.chkTrangthai.TabStop = false;
            this.chkTrangthai.Text = "Hiệu lực?";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkChoPhepNhapLienTuc
            // 
            this.chkChoPhepNhapLienTuc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkChoPhepNhapLienTuc.AutoSize = true;
            this.chkChoPhepNhapLienTuc.Checked = true;
            this.chkChoPhepNhapLienTuc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChoPhepNhapLienTuc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkChoPhepNhapLienTuc.Location = new System.Drawing.Point(12, 676);
            this.chkChoPhepNhapLienTuc.Name = "chkChoPhepNhapLienTuc";
            this.chkChoPhepNhapLienTuc.Size = new System.Drawing.Size(144, 18);
            this.chkChoPhepNhapLienTuc.TabIndex = 100;
            this.chkChoPhepNhapLienTuc.TabStop = false;
            this.chkChoPhepNhapLienTuc.Text = "Cho phép nhập liên tục";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_themmoi_vungkhaosat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.chkChoPhepNhapLienTuc);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.chkTrangthai);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.uiStatusBar1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_vungkhaosat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin vùng khảo sát";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_themmoi_vungkhaosat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtDenghi;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtIdVungKs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenVungKs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Janus.Windows.EditControls.UICheckBox chkChoPhepNhapLienTuc;
        private System.Windows.Forms.Label label9;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMa;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.EditBox txtFileMau;
        private System.Windows.Forms.RichTextBox txtKet_Luan;
        private System.Windows.Forms.WebBrowser txtMoTa;
        private System.Windows.Forms.ComboBox cboLoaiDvu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtNoidung;
        private System.Windows.Forms.Timer timer1;
        private Janus.Windows.EditControls.UICheckBox chkTrangthai;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtKichthuocanh;
    }
}