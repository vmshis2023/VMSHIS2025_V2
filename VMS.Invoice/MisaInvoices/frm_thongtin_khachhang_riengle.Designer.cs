namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frm_thongtin_khachhang_riengle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thongtin_khachhang_riengle));
            this.txthovaten = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtgioitinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtnamsinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtManguoimua = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdthoat = new Janus.Windows.EditControls.UIButton();
            this.lblManguoimua = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTennguoinhan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmail = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdPReview = new Janus.Windows.EditControls.UIButton();
            this.cmdPhathanhHDon = new Janus.Windows.EditControls.UIButton();
            this.chkCloseAfterSaving = new System.Windows.Forms.CheckBox();
            this.grbThongtinHoadon = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgaythanhtoan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txttencongty = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboSeries = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpNgayhoadon = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCC = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSendEmail = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.grbThongtinHoadon.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txthovaten
            // 
            this.txthovaten.Enabled = false;
            this.txthovaten.Font = new System.Drawing.Font("Arial", 10F);
            this.txthovaten.Location = new System.Drawing.Point(119, 102);
            this.txthovaten.Name = "txthovaten";
            this.txthovaten.ReadOnly = true;
            this.txthovaten.Size = new System.Drawing.Size(479, 23);
            this.txthovaten.TabIndex = 2;
            // 
            // txtgioitinh
            // 
            this.txtgioitinh.Enabled = false;
            this.txtgioitinh.Font = new System.Drawing.Font("Arial", 10F);
            this.txtgioitinh.Location = new System.Drawing.Point(872, 54);
            this.txtgioitinh.Name = "txtgioitinh";
            this.txtgioitinh.ReadOnly = true;
            this.txtgioitinh.Size = new System.Drawing.Size(60, 23);
            this.txtgioitinh.TabIndex = 3;
            this.txtgioitinh.TabStop = false;
            this.txtgioitinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtgioitinh.Visible = false;
            // 
            // txtnamsinh
            // 
            this.txtnamsinh.Enabled = false;
            this.txtnamsinh.Font = new System.Drawing.Font("Arial", 10F);
            this.txtnamsinh.Location = new System.Drawing.Point(872, 83);
            this.txtnamsinh.Name = "txtnamsinh";
            this.txtnamsinh.ReadOnly = true;
            this.txtnamsinh.Size = new System.Drawing.Size(60, 23);
            this.txtnamsinh.TabIndex = 4;
            this.txtnamsinh.TabStop = false;
            this.txtnamsinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtnamsinh.Visible = false;
            // 
            // txtManguoimua
            // 
            this.txtManguoimua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtManguoimua.Enabled = false;
            this.txtManguoimua.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.txtManguoimua.Location = new System.Drawing.Point(119, 76);
            this.txtManguoimua.Name = "txtManguoimua";
            this.txtManguoimua.Size = new System.Drawing.Size(152, 23);
            this.txtManguoimua.TabIndex = 0;
            this.txtManguoimua.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // cmdthoat
            // 
            this.cmdthoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdthoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdthoat.Image")));
            this.cmdthoat.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdthoat.Location = new System.Drawing.Point(517, 400);
            this.cmdthoat.Name = "cmdthoat";
            this.cmdthoat.Size = new System.Drawing.Size(103, 33);
            this.cmdthoat.TabIndex = 12;
            this.cmdthoat.Text = "Hủy bỏ";
            this.cmdthoat.Click += new System.EventHandler(this.cmdthoat_Click);
            // 
            // lblManguoimua
            // 
            this.lblManguoimua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblManguoimua.Location = new System.Drawing.Point(-3, 78);
            this.lblManguoimua.Name = "lblManguoimua";
            this.lblManguoimua.Size = new System.Drawing.Size(120, 19);
            this.lblManguoimua.TabIndex = 11;
            this.lblManguoimua.Text = "Mã khách hàng";
            this.lblManguoimua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(14, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tên khách hàng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(5, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "Tên người nhận:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTennguoinhan
            // 
            this.txtTennguoinhan.Enabled = false;
            this.txtTennguoinhan.Font = new System.Drawing.Font("Arial", 10F);
            this.txtTennguoinhan.Location = new System.Drawing.Point(119, 27);
            this.txtTennguoinhan.Name = "txtTennguoinhan";
            this.txtTennguoinhan.Size = new System.Drawing.Size(479, 23);
            this.txtTennguoinhan.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 20;
            this.label8.Text = "Email";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Font = new System.Drawing.Font("Arial", 10F);
            this.txtEmail.Location = new System.Drawing.Point(119, 53);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmail.Size = new System.Drawing.Size(479, 23);
            this.txtEmail.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 65);
            this.panel1.TabIndex = 608;
            // 
            // lblTitle2
            // 
            this.lblTitle2.BackColor = System.Drawing.Color.Cornsilk;
            this.lblTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.ForeColor = System.Drawing.Color.Black;
            this.lblTitle2.Location = new System.Drawing.Point(65, 0);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(564, 63);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "PHÁT HÀNH HÓA ĐƠN ĐIỆN TỬ";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(65, 63);
            this.panel2.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Location = new System.Drawing.Point(6, 373);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(613, 24);
            this.lblMsg.TabIndex = 619;
            // 
            // cmdPReview
            // 
            this.cmdPReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPReview.Image = ((System.Drawing.Image)(resources.GetObject("cmdPReview.Image")));
            this.cmdPReview.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPReview.Location = new System.Drawing.Point(380, 400);
            this.cmdPReview.Name = "cmdPReview";
            this.cmdPReview.Size = new System.Drawing.Size(131, 33);
            this.cmdPReview.TabIndex = 11;
            this.cmdPReview.Text = "Xem trước HĐ";
            this.cmdPReview.Click += new System.EventHandler(this.cmdPReview_Click);
            // 
            // cmdPhathanhHDon
            // 
            this.cmdPhathanhHDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPhathanhHDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPhathanhHDon.Image = ((System.Drawing.Image)(resources.GetObject("cmdPhathanhHDon.Image")));
            this.cmdPhathanhHDon.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPhathanhHDon.Location = new System.Drawing.Point(215, 400);
            this.cmdPhathanhHDon.Name = "cmdPhathanhHDon";
            this.cmdPhathanhHDon.Size = new System.Drawing.Size(159, 33);
            this.cmdPhathanhHDon.TabIndex = 10;
            this.cmdPhathanhHDon.Text = "Phát hành hóa đơn";
            this.cmdPhathanhHDon.Click += new System.EventHandler(this.cmdPhathanhHDon_Click);
            // 
            // chkCloseAfterSaving
            // 
            this.chkCloseAfterSaving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCloseAfterSaving.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseAfterSaving.ForeColor = System.Drawing.Color.Navy;
            this.chkCloseAfterSaving.Location = new System.Drawing.Point(9, 410);
            this.chkCloseAfterSaving.Name = "chkCloseAfterSaving";
            this.chkCloseAfterSaving.Size = new System.Drawing.Size(199, 24);
            this.chkCloseAfterSaving.TabIndex = 627;
            this.chkCloseAfterSaving.TabStop = false;
            this.chkCloseAfterSaving.Tag = "INVOICE_TAOHOADONTAY_THOATSAUKHILUU";
            this.chkCloseAfterSaving.Text = "Thoát form sau khi phát hành?";
            this.chkCloseAfterSaving.UseVisualStyleBackColor = true;
            // 
            // grbThongtinHoadon
            // 
            this.grbThongtinHoadon.Controls.Add(this.label3);
            this.grbThongtinHoadon.Controls.Add(this.dtpNgaythanhtoan);
            this.grbThongtinHoadon.Controls.Add(this.txttencongty);
            this.grbThongtinHoadon.Controls.Add(this.label1);
            this.grbThongtinHoadon.Controls.Add(this.label10);
            this.grbThongtinHoadon.Controls.Add(this.cboSeries);
            this.grbThongtinHoadon.Controls.Add(this.label9);
            this.grbThongtinHoadon.Controls.Add(this.dtpNgayhoadon);
            this.grbThongtinHoadon.Controls.Add(this.txtManguoimua);
            this.grbThongtinHoadon.Controls.Add(this.lblManguoimua);
            this.grbThongtinHoadon.Controls.Add(this.txthovaten);
            this.grbThongtinHoadon.Controls.Add(this.label2);
            this.grbThongtinHoadon.Location = new System.Drawing.Point(4, 71);
            this.grbThongtinHoadon.Name = "grbThongtinHoadon";
            this.grbThongtinHoadon.Size = new System.Drawing.Size(616, 158);
            this.grbThongtinHoadon.TabIndex = 631;
            this.grbThongtinHoadon.TabStop = false;
            this.grbThongtinHoadon.Text = "Thông tin hóa đơn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(281, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 301;
            this.label3.Text = "Ngày chứng từ:";
            // 
            // dtpNgaythanhtoan
            // 
            this.dtpNgaythanhtoan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaythanhtoan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaythanhtoan.DropDownCalendar.Name = "";
            this.dtpNgaythanhtoan.Enabled = false;
            this.dtpNgaythanhtoan.Location = new System.Drawing.Point(386, 27);
            this.dtpNgaythanhtoan.Name = "dtpNgaythanhtoan";
            this.dtpNgaythanhtoan.ReadOnly = true;
            this.dtpNgaythanhtoan.ShowUpDown = true;
            this.dtpNgaythanhtoan.Size = new System.Drawing.Size(152, 21);
            this.dtpNgaythanhtoan.TabIndex = 300;
            this.dtpNgaythanhtoan.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // txttencongty
            // 
            this.txttencongty.Font = new System.Drawing.Font("Arial", 10F);
            this.txttencongty.Location = new System.Drawing.Point(119, 128);
            this.txttencongty.Name = "txttencongty";
            this.txttencongty.Size = new System.Drawing.Size(479, 23);
            this.txttencongty.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(14, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 299;
            this.label1.Text = "Tên đơn vị";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(15, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 19);
            this.label10.TabIndex = 297;
            this.label10.Text = "Kí hiệu Hóa đơn";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSeries
            // 
            this.cboSeries.FormattingEnabled = true;
            this.cboSeries.Location = new System.Drawing.Point(119, 49);
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Next_Control = null;
            this.cboSeries.RaiseEnterEventWhenInvisible = true;
            this.cboSeries.Size = new System.Drawing.Size(152, 23);
            this.cboSeries.TabIndex = 1;
            this.cboSeries.TabStop = false;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(14, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 15);
            this.label9.TabIndex = 295;
            this.label9.Text = "Ngày hóa đơn";
            // 
            // dtpNgayhoadon
            // 
            this.dtpNgayhoadon.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayhoadon.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayhoadon.DropDownCalendar.Name = "";
            this.dtpNgayhoadon.Location = new System.Drawing.Point(119, 25);
            this.dtpNgayhoadon.Name = "dtpNgayhoadon";
            this.dtpNgayhoadon.ShowUpDown = true;
            this.dtpNgayhoadon.Size = new System.Drawing.Size(152, 21);
            this.dtpNgayhoadon.TabIndex = 0;
            this.dtpNgayhoadon.Value = new System.DateTime(2025, 5, 20, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCC);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkSendEmail);
            this.groupBox1.Controls.Add(this.txtTennguoinhan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(4, 232);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 136);
            this.groupBox1.TabIndex = 632;
            this.groupBox1.TabStop = false;
            // 
            // txtCC
            // 
            this.txtCC.Enabled = false;
            this.txtCC.Font = new System.Drawing.Font("Arial", 10F);
            this.txtCC.Location = new System.Drawing.Point(119, 79);
            this.txtCC.Multiline = true;
            this.txtCC.Name = "txtCC";
            this.txtCC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCC.Size = new System.Drawing.Size(479, 51);
            this.txtCC.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 22;
            this.label4.Text = "CC";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSendEmail
            // 
            this.chkSendEmail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendEmail.ForeColor = System.Drawing.Color.Navy;
            this.chkSendEmail.Location = new System.Drawing.Point(0, -3);
            this.chkSendEmail.Name = "chkSendEmail";
            this.chkSendEmail.Size = new System.Drawing.Size(199, 24);
            this.chkSendEmail.TabIndex = 3;
            this.chkSendEmail.Tag = "";
            this.chkSendEmail.Text = "Gửi hóa đơn cho khách hàng?";
            this.chkSendEmail.UseVisualStyleBackColor = true;
            this.chkSendEmail.CheckedChanged += new System.EventHandler(this.chkSendEmail_CheckedChanged);
            // 
            // frm_thongtin_khachhang_riengle
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(631, 446);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbThongtinHoadon);
            this.Controls.Add(this.chkCloseAfterSaving);
            this.Controls.Add(this.cmdPReview);
            this.Controls.Add(this.cmdPhathanhHDon);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdthoat);
            this.Controls.Add(this.txtnamsinh);
            this.Controls.Add(this.txtgioitinh);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thongtin_khachhang_riengle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phát hành hóa đơn điện tử";
            this.Load += new System.EventHandler(this.frm_thongtin_khachhang_riengle_Load);
            this.panel1.ResumeLayout(false);
            this.grbThongtinHoadon.ResumeLayout(false);
            this.grbThongtinHoadon.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.EditBox txthovaten;
        private Janus.Windows.GridEX.EditControls.EditBox txtgioitinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtnamsinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtManguoimua;
        private Janus.Windows.EditControls.UIButton cmdthoat;
        private System.Windows.Forms.Label lblManguoimua;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtTennguoinhan;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtEmail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UIButton cmdPReview;
        private Janus.Windows.EditControls.UIButton cmdPhathanhHDon;
        private System.Windows.Forms.CheckBox chkCloseAfterSaving;
        private System.Windows.Forms.GroupBox grbThongtinHoadon;
        private HIS.UCs.EasyCompletionComboBox cboSeries;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayhoadon;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSendEmail;
        private Janus.Windows.GridEX.EditControls.EditBox txttencongty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaythanhtoan;
        private Janus.Windows.GridEX.EditControls.EditBox txtCC;
        private System.Windows.Forms.Label label4;
    }
}