namespace VNS.HIS.UI.THUOC
{
    partial class frm_ChooseIn
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
            Janus.Windows.GridEX.GridEXLayout grdGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChooseIn));
            Janus.Windows.GridEX.GridEXLayout grdBuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grdGiuong = new Janus.Windows.GridEX.GridEX();
            this.grdBuong = new Janus.Windows.GridEX.GridEX();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radTatCaThuongBoSung = new System.Windows.Forms.RadioButton();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radChuaLinh = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaLinh = new Janus.Windows.EditControls.UIRadioButton();
            this.radTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radPhieuLinh = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgayDieuTri = new Janus.Windows.EditControls.UIRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radLinhBoSung = new System.Windows.Forms.RadioButton();
            this.radLinhThuong = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPrinpreview = new System.Windows.Forms.CheckBox();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cboKhoadieutri = new Janus.Windows.EditControls.UIComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiGroupBox1.Controls.Add(this.cboKhoadieutri);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.grdGiuong);
            this.uiGroupBox1.Controls.Add(this.grdBuong);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.radTatCaThuongBoSung);
            this.uiGroupBox1.Controls.Add(this.dtDenNgay);
            this.uiGroupBox1.Controls.Add(this.dtTuNgay);
            this.uiGroupBox1.Controls.Add(this.panel2);
            this.uiGroupBox1.Controls.Add(this.panel1);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.radLinhBoSung);
            this.uiGroupBox1.Controls.Add(this.radLinhThuong);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1184, 710);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin ";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(499, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 22);
            this.label5.TabIndex = 557;
            this.label5.Text = "Loại ngày:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(35, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 22);
            this.label4.TabIndex = 556;
            this.label4.Text = "Loại phiếu:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdGiuong
            // 
            this.grdGiuong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdGiuong.AlternatingColors = true;
            this.grdGiuong.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdGiuong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdGiuong.AutomaticSort = false;
            this.grdGiuong.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdGiuong_DesignTimeLayout.LayoutString = resources.GetString("grdGiuong_DesignTimeLayout.LayoutString");
            this.grdGiuong.DesignTimeLayout = grdGiuong_DesignTimeLayout;
            this.grdGiuong.DynamicFiltering = true;
            this.grdGiuong.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdGiuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdGiuong.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdGiuong.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdGiuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdGiuong.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGiuong.GroupByBoxVisible = false;
            this.grdGiuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdGiuong.Location = new System.Drawing.Point(706, 93);
            this.grdGiuong.Name = "grdGiuong";
            this.grdGiuong.RecordNavigator = true;
            this.grdGiuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdGiuong.Size = new System.Drawing.Size(448, 611);
            this.grdGiuong.TabIndex = 555;
            this.grdGiuong.TabStop = false;
            this.grdGiuong.UseGroupRowSelector = true;
            this.grdGiuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // grdBuong
            // 
            this.grdBuong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdBuong.AlternatingColors = true;
            this.grdBuong.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdBuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdBuong.AutomaticSort = false;
            grdBuong_DesignTimeLayout.LayoutString = resources.GetString("grdBuong_DesignTimeLayout.LayoutString");
            this.grdBuong.DesignTimeLayout = grdBuong_DesignTimeLayout;
            this.grdBuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdBuong.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdBuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdBuong.Font = new System.Drawing.Font("Arial", 9F);
            this.grdBuong.GroupByBoxVisible = false;
            this.grdBuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdBuong.Location = new System.Drawing.Point(118, 93);
            this.grdBuong.Name = "grdBuong";
            this.grdBuong.RecordNavigator = true;
            this.grdBuong.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdBuong.Size = new System.Drawing.Size(582, 611);
            this.grdBuong.TabIndex = 554;
            this.grdBuong.TabStop = false;
            this.grdBuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label7.Location = new System.Drawing.Point(469, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 22);
            this.label7.TabIndex = 54;
            this.label7.Text = "Trạng thái:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 22);
            this.label6.TabIndex = 52;
            this.label6.Text = "Khoa nội trú:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radTatCaThuongBoSung
            // 
            this.radTatCaThuongBoSung.AutoSize = true;
            this.radTatCaThuongBoSung.Checked = true;
            this.radTatCaThuongBoSung.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radTatCaThuongBoSung.Location = new System.Drawing.Point(118, 40);
            this.radTatCaThuongBoSung.Name = "radTatCaThuongBoSung";
            this.radTatCaThuongBoSung.Size = new System.Drawing.Size(62, 20);
            this.radTatCaThuongBoSung.TabIndex = 49;
            this.radTatCaThuongBoSung.TabStop = true;
            this.radTatCaThuongBoSung.Text = "Tất cả";
            this.radTatCaThuongBoSung.UseVisualStyleBackColor = true;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dtDenNgay.Location = new System.Drawing.Point(351, 12);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(144, 22);
            this.dtDenNgay.TabIndex = 48;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dtTuNgay.Location = new System.Drawing.Point(118, 12);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(144, 22);
            this.dtTuNgay.TabIndex = 47;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radChuaLinh);
            this.panel2.Controls.Add(this.radDaLinh);
            this.panel2.Controls.Add(this.radTatCa);
            this.panel2.Location = new System.Drawing.Point(577, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(347, 25);
            this.panel2.TabIndex = 42;
            // 
            // radChuaLinh
            // 
            this.radChuaLinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radChuaLinh.Location = new System.Drawing.Point(153, 1);
            this.radChuaLinh.Name = "radChuaLinh";
            this.radChuaLinh.Size = new System.Drawing.Size(112, 22);
            this.radChuaLinh.TabIndex = 45;
            this.radChuaLinh.Text = "Chưa lĩnh";
            // 
            // radDaLinh
            // 
            this.radDaLinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radDaLinh.Location = new System.Drawing.Point(76, 1);
            this.radDaLinh.Name = "radDaLinh";
            this.radDaLinh.Size = new System.Drawing.Size(71, 22);
            this.radDaLinh.TabIndex = 44;
            this.radDaLinh.Text = "Đã lĩnh";
            // 
            // radTatCa
            // 
            this.radTatCa.Checked = true;
            this.radTatCa.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radTatCa.Location = new System.Drawing.Point(3, 1);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(69, 22);
            this.radTatCa.TabIndex = 43;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radPhieuLinh);
            this.panel1.Controls.Add(this.radNgayDieuTri);
            this.panel1.Location = new System.Drawing.Point(576, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 25);
            this.panel1.TabIndex = 41;
            // 
            // radPhieuLinh
            // 
            this.radPhieuLinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radPhieuLinh.Location = new System.Drawing.Point(123, 2);
            this.radPhieuLinh.Name = "radPhieuLinh";
            this.radPhieuLinh.Size = new System.Drawing.Size(162, 22);
            this.radPhieuLinh.TabIndex = 44;
            this.radPhieuLinh.Text = "Ngày  lập phiếu lĩnh";
            // 
            // radNgayDieuTri
            // 
            this.radNgayDieuTri.Checked = true;
            this.radNgayDieuTri.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNgayDieuTri.Location = new System.Drawing.Point(3, 2);
            this.radNgayDieuTri.Name = "radNgayDieuTri";
            this.radNgayDieuTri.Size = new System.Drawing.Size(104, 22);
            this.radNgayDieuTri.TabIndex = 43;
            this.radNgayDieuTri.TabStop = true;
            this.radNgayDieuTri.Text = "Ngày điều trị";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 22);
            this.label3.TabIndex = 40;
            this.label3.Text = "Từ ngày";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radLinhBoSung
            // 
            this.radLinhBoSung.AutoSize = true;
            this.radLinhBoSung.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radLinhBoSung.Location = new System.Drawing.Point(289, 39);
            this.radLinhBoSung.Name = "radLinhBoSung";
            this.radLinhBoSung.Size = new System.Drawing.Size(100, 20);
            this.radLinhBoSung.TabIndex = 32;
            this.radLinhBoSung.Text = "Lĩnh bổ sung";
            this.radLinhBoSung.UseVisualStyleBackColor = true;
            // 
            // radLinhThuong
            // 
            this.radLinhThuong.AutoSize = true;
            this.radLinhThuong.Font = new System.Drawing.Font("Arial", 9.75F);
            this.radLinhThuong.Location = new System.Drawing.Point(186, 39);
            this.radLinhThuong.Name = "radLinhThuong";
            this.radLinhThuong.Size = new System.Drawing.Size(97, 20);
            this.radLinhThuong.TabIndex = 31;
            this.radLinhThuong.Text = "Lĩnh thường";
            this.radLinhThuong.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(268, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đến ngày";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPrinpreview
            // 
            this.chkPrinpreview.AutoSize = true;
            this.chkPrinpreview.Font = new System.Drawing.Font("Arial", 9.75F);
            this.chkPrinpreview.Location = new System.Drawing.Point(118, 728);
            this.chkPrinpreview.Name = "chkPrinpreview";
            this.chkPrinpreview.Size = new System.Drawing.Size(131, 20);
            this.chkPrinpreview.TabIndex = 38;
            this.chkPrinpreview.Tag = "THUOC_SOTAMTRA_XEMTRUOCKHIIN";
            this.chkPrinpreview.Text = "Xem trước khi in?";
            this.chkPrinpreview.UseVisualStyleBackColor = true;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdPrint.Image = global::VMS.HIS.Duoc.Properties.Resources.printer_32;
            this.cmdPrint.ImageSize = new System.Drawing.Size(26, 26);
            this.cmdPrint.Location = new System.Drawing.Point(908, 716);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 33);
            this.cmdPrint.TabIndex = 55;
            this.cmdPrint.Text = "In (Ctrl+P)";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(26, 26);
            this.cmdExit.Location = new System.Drawing.Point(1034, 716);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 33);
            this.cmdExit.TabIndex = 56;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click_1);
            // 
            // cboKhoadieutri
            // 
            this.cboKhoadieutri.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoadieutri.Location = new System.Drawing.Point(118, 63);
            this.cboKhoadieutri.MaxDropDownItems = 15;
            this.cboKhoadieutri.Name = "cboKhoadieutri";
            this.cboKhoadieutri.Size = new System.Drawing.Size(377, 21);
            this.cboKhoadieutri.TabIndex = 558;
            this.cboKhoadieutri.Text = "Chọn khoa điều trị";
            this.cboKhoadieutri.SelectedIndexChanged += new System.EventHandler(this.cboKhoadieutri_SelectedIndexChanged);
            // 
            // frm_ChooseIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.chkPrinpreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChooseIn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu tam tra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_ChooseIn_FormClosing);
            this.Load += new System.EventHandler(this.frm_ChooseIn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChooseIn_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuong)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radLinhBoSung;
        private System.Windows.Forms.RadioButton radLinhThuong;
        private System.Windows.Forms.CheckBox chkPrinpreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIRadioButton radPhieuLinh;
        private Janus.Windows.EditControls.UIRadioButton radNgayDieuTri;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIRadioButton radChuaLinh;
        private Janus.Windows.EditControls.UIRadioButton radDaLinh;
        private Janus.Windows.EditControls.UIRadioButton radTatCa;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private System.Windows.Forms.RadioButton radTatCaThuongBoSung;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.GridEX grdBuong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.GridEX grdGiuong;
        private Janus.Windows.EditControls.UIComboBox cboKhoadieutri;
    }
}