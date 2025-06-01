namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_KiemTra_BHYT
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
            Janus.Windows.GridEX.GridEXLayout grdPaymentDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KiemTra_BHYT));
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPaymentDetail = new Janus.Windows.GridEX.GridEX();
            this.grpTongTien = new Janus.Windows.EditControls.UIGroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.lblMoneyLetter = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTienBNPhaiTra = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTienPhuThu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTienBNCT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTienBHCT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSoTienGoc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdInPhieu_DongChiTra = new Janus.Windows.EditControls.UIButton();
            this.cmdPrintAll = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTongTien)).BeginInit();
            this.grpTongTien.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdPaymentDetail);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(997, 521);
            this.uiGroupBox2.TabIndex = 3;
            this.uiGroupBox2.Text = "&Thông tin thanh toán";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdPaymentDetail
            // 
            this.grdPaymentDetail.AlternatingColors = true;
            this.grdPaymentDetail.BackColor = System.Drawing.Color.Silver;
            grdPaymentDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPaymentDetail_DesignTimeLayout.LayoutString");
            this.grdPaymentDetail.DesignTimeLayout = grdPaymentDetail_DesignTimeLayout;
            this.grdPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPaymentDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdPaymentDetail.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.GroupByBoxVisible = false;
            this.grdPaymentDetail.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdPaymentDetail.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdPaymentDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPaymentDetail.Location = new System.Drawing.Point(3, 18);
            this.grdPaymentDetail.Name = "grdPaymentDetail";
            this.grdPaymentDetail.Size = new System.Drawing.Size(991, 500);
            this.grdPaymentDetail.TabIndex = 486;
            this.grdPaymentDetail.TabStop = false;
            this.grdPaymentDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPaymentDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // grpTongTien
            // 
            this.grpTongTien.BackColor = System.Drawing.SystemColors.Control;
            this.grpTongTien.Controls.Add(this.label11);
            this.grpTongTien.Controls.Add(this.txtTieuDe);
            this.grpTongTien.Controls.Add(this.label22);
            this.grpTongTien.Controls.Add(this.lblMoneyLetter);
            this.grpTongTien.Controls.Add(this.label21);
            this.grpTongTien.Controls.Add(this.txtTienBNPhaiTra);
            this.grpTongTien.Controls.Add(this.label20);
            this.grpTongTien.Controls.Add(this.label19);
            this.grpTongTien.Controls.Add(this.txtTienPhuThu);
            this.grpTongTien.Controls.Add(this.label18);
            this.grpTongTien.Controls.Add(this.txtTienBNCT);
            this.grpTongTien.Controls.Add(this.label17);
            this.grpTongTien.Controls.Add(this.txtTienBHCT);
            this.grpTongTien.Controls.Add(this.label16);
            this.grpTongTien.Controls.Add(this.txtSoTienGoc);
            this.grpTongTien.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTongTien.Location = new System.Drawing.Point(0, 521);
            this.grpTongTien.Name = "grpTongTien";
            this.grpTongTien.Size = new System.Drawing.Size(997, 111);
            this.grpTongTien.TabIndex = 70;
            this.grpTongTien.Text = "&Thông tin đồng chi trả BHYT (Đơn vị tính : vnđ)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Navy;
            this.label22.Location = new System.Drawing.Point(908, 41);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 26);
            this.label22.TabIndex = 520;
            this.label22.Text = "(vnđ)";
            // 
            // lblMoneyLetter
            // 
            this.lblMoneyLetter.AutoSize = true;
            this.lblMoneyLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneyLetter.ForeColor = System.Drawing.Color.Navy;
            this.lblMoneyLetter.Location = new System.Drawing.Point(160, 89);
            this.lblMoneyLetter.Name = "lblMoneyLetter";
            this.lblMoneyLetter.Size = new System.Drawing.Size(81, 17);
            this.lblMoneyLetter.TabIndex = 519;
            this.lblMoneyLetter.Text = "&Bằng chữ ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Navy;
            this.label21.Location = new System.Drawing.Point(14, 86);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(76, 17);
            this.label21.TabIndex = 518;
            this.label21.Text = "&Bằng chữ :";
            // 
            // txtTienBNPhaiTra
            // 
            this.txtTienBNPhaiTra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtTienBNPhaiTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienBNPhaiTra.Location = new System.Drawing.Point(709, 36);
            this.txtTienBNPhaiTra.Name = "txtTienBNPhaiTra";
            this.txtTienBNPhaiTra.Size = new System.Drawing.Size(193, 41);
            this.txtTienBNPhaiTra.TabIndex = 517;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Navy;
            this.label20.Location = new System.Drawing.Point(543, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(162, 17);
            this.label20.TabIndex = 516;
            this.label20.Text = "(1)+(2)=(BN phải trả)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Navy;
            this.label19.Location = new System.Drawing.Point(303, 63);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 17);
            this.label19.TabIndex = 515;
            this.label19.Text = "&Chênh lệch (2)";
            // 
            // txtTienPhuThu
            // 
            this.txtTienPhuThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienPhuThu.Location = new System.Drawing.Point(406, 63);
            this.txtTienPhuThu.Name = "txtTienPhuThu";
            this.txtTienPhuThu.Size = new System.Drawing.Size(125, 23);
            this.txtTienPhuThu.TabIndex = 514;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Navy;
            this.label18.Location = new System.Drawing.Point(14, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(146, 17);
            this.label18.TabIndex = 513;
            this.label18.Text = "&BNCT đồng chi trả (1)";
            // 
            // txtTienBNCT
            // 
            this.txtTienBNCT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienBNCT.Location = new System.Drawing.Point(163, 58);
            this.txtTienBNCT.Name = "txtTienBNCT";
            this.txtTienBNCT.Size = new System.Drawing.Size(134, 23);
            this.txtTienBNCT.TabIndex = 512;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(303, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 17);
            this.label17.TabIndex = 511;
            this.label17.Text = "&Số tiền BHCT";
            // 
            // txtTienBHCT
            // 
            this.txtTienBHCT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienBHCT.Location = new System.Drawing.Point(406, 27);
            this.txtTienBHCT.Name = "txtTienBHCT";
            this.txtTienBHCT.Size = new System.Drawing.Size(125, 23);
            this.txtTienBHCT.TabIndex = 510;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Navy;
            this.label16.Location = new System.Drawing.Point(14, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 17);
            this.label16.TabIndex = 509;
            this.label16.Text = "&Số tiền gốc";
            // 
            // txtSoTienGoc
            // 
            this.txtSoTienGoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoTienGoc.Location = new System.Drawing.Point(163, 24);
            this.txtSoTienGoc.Name = "txtSoTienGoc";
            this.txtSoTienGoc.Size = new System.Drawing.Size(134, 23);
            this.txtSoTienGoc.TabIndex = 508;
            // 
            // cmdInPhieu_DongChiTra
            // 
            this.cmdInPhieu_DongChiTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInPhieu_DongChiTra.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieu_DongChiTra.Image")));
            this.cmdInPhieu_DongChiTra.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieu_DongChiTra.Location = new System.Drawing.Point(409, 636);
            this.cmdInPhieu_DongChiTra.Name = "cmdInPhieu_DongChiTra";
            this.cmdInPhieu_DongChiTra.Size = new System.Drawing.Size(144, 35);
            this.cmdInPhieu_DongChiTra.TabIndex = 73;
            this.cmdInPhieu_DongChiTra.Text = "&In thu đồng chi trả (F4)";
            this.cmdInPhieu_DongChiTra.ToolTipText = "In phiếu thông tin của phần phiếu của phần thanh toán";
            this.cmdInPhieu_DongChiTra.Click += new System.EventHandler(this.cmdInPhieu_DongChiTra_Click);
            // 
            // cmdPrintAll
            // 
            this.cmdPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrintAll.Image")));
            this.cmdPrintAll.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrintAll.Location = new System.Drawing.Point(238, 636);
            this.cmdPrintAll.Name = "cmdPrintAll";
            this.cmdPrintAll.Size = new System.Drawing.Size(151, 35);
            this.cmdPrintAll.TabIndex = 72;
            this.cmdPrintAll.Text = "&In phôi BHYT (F5)";
            this.cmdPrintAll.ToolTipText = "In phiếu tổng hợp cho bệnh nhân";
            this.cmdPrintAll.Click += new System.EventHandler(this.cmdPrintAll_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(570, 635);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(121, 36);
            this.cmdExit.TabIndex = 71;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.Location = new System.Drawing.Point(554, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 15);
            this.label11.TabIndex = 524;
            this.label11.Text = "&Tiêu đề";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtTieuDe.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTieuDe.Location = new System.Drawing.Point(608, 86);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(387, 20);
            this.txtTieuDe.TabIndex = 523;
            this.txtTieuDe.Text = "BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ";
            this.txtTieuDe.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // dtNgayIn
            // 
            this.dtNgayIn.CustomFormat = "dd/MM/yyyy";
            this.dtNgayIn.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayIn.DropDownCalendar.Name = "";
            this.dtNgayIn.Location = new System.Drawing.Point(89, 647);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(121, 20);
            this.dtNgayIn.TabIndex = 522;
            this.dtNgayIn.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Navy;
            this.label15.Location = new System.Drawing.Point(10, 650);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 521;
            this.label15.Text = "&Ngày in phiếu";
            // 
            // frm_KiemTra_BHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(997, 673);
            this.Controls.Add(this.cmdInPhieu_DongChiTra);
            this.Controls.Add(this.cmdPrintAll);
            this.Controls.Add(this.dtNgayIn);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grpTongTien);
            this.Controls.Add(this.uiGroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_KiemTra_BHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra thông tin của bảo hiểm";
            this.Load += new System.EventHandler(this.frm_KiemTra_BHYT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTongTien)).EndInit();
            this.grpTongTien.ResumeLayout(false);
            this.grpTongTien.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        public Janus.Windows.GridEX.GridEX grdPaymentDetail;
        private Janus.Windows.EditControls.UIGroupBox grpTongTien;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblMoneyLetter;
        private System.Windows.Forms.Label label21;
        private Janus.Windows.GridEX.EditControls.EditBox txtTienBNPhaiTra;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private Janus.Windows.GridEX.EditControls.EditBox txtTienPhuThu;
        private System.Windows.Forms.Label label18;
        private Janus.Windows.GridEX.EditControls.EditBox txtTienBNCT;
        private System.Windows.Forms.Label label17;
        private Janus.Windows.GridEX.EditControls.EditBox txtTienBHCT;
        private System.Windows.Forms.Label label16;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoTienGoc;
        private Janus.Windows.EditControls.UIButton cmdInPhieu_DongChiTra;
        internal Janus.Windows.EditControls.UIButton cmdPrintAll;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayIn;
        private System.Windows.Forms.Label label15;
    }
}