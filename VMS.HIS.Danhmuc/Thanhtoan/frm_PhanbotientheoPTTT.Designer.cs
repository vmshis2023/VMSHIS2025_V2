namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_PhanbotientheoPTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhanbotientheoPTTT));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem6 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem7 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem8 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdTaoQR = new Janus.Windows.EditControls.UIButton();
            this.cmdHuyQR = new Janus.Windows.EditControls.UIButton();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlHuyThanhtoan = new System.Windows.Forms.Panel();
            this.txtTongtien = new MaskedTextBox.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNgaythanhtoan = new System.Windows.Forms.Label();
            this.dtPaymentDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboNganhangChung = new Janus.Windows.EditControls.UIComboBox();
            this.cboPttt_chung = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.vbLine2 = new VNS.UCs.VBLine();
            this.pnlPhanbo = new System.Windows.Forms.Panel();
            this.cboNganhang = new Janus.Windows.EditControls.UIComboBox();
            this.cboPttt_rieng = new Janus.Windows.EditControls.UIComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlInfor.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlHuyThanhtoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.pnlPhanbo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAccept.Location = new System.Drawing.Point(596, 11);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(120, 35);
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "Chấp nhận";
            this.toolTip1.SetToolTip(this.cmdAccept, "Nhấn vào đây để bắt đầu hủy thanh toán cho các mục được chọn trên lưới");
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(722, 11);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để hủy bỏ việc phân bổ tiền theo phương thức thanh toán và quay lại " +
        "màn hình chính");
            // 
            // cmdTaoQR
            // 
            this.cmdTaoQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdTaoQR.Enabled = false;
            this.cmdTaoQR.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTaoQR.Image = ((System.Drawing.Image)(resources.GetObject("cmdTaoQR.Image")));
            this.cmdTaoQR.ImageSize = new System.Drawing.Size(30, 30);
            this.cmdTaoQR.Location = new System.Drawing.Point(6, 11);
            this.cmdTaoQR.Name = "cmdTaoQR";
            this.cmdTaoQR.Size = new System.Drawing.Size(91, 35);
            this.cmdTaoQR.TabIndex = 724;
            this.cmdTaoQR.Text = "Tạo QR";
            // 
            // cmdHuyQR
            // 
            this.cmdHuyQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHuyQR.Enabled = false;
            this.cmdHuyQR.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuyQR.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuyQR.Image")));
            this.cmdHuyQR.ImageSize = new System.Drawing.Size(30, 30);
            this.cmdHuyQR.Location = new System.Drawing.Point(239, 11);
            this.cmdHuyQR.Name = "cmdHuyQR";
            this.cmdHuyQR.Size = new System.Drawing.Size(91, 35);
            this.cmdHuyQR.TabIndex = 725;
            this.cmdHuyQR.Text = "Hủy QR";
            this.cmdHuyQR.Visible = false;
            // 
            // txtSotien
            // 
            this.txtSotien.BackColor = System.Drawing.Color.White;
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(376, 47);
            this.txtSotien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(437, 22);
            this.txtSotien.TabIndex = 6;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.pnlActions);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 468);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(859, 93);
            this.pnlInfor.TabIndex = 8;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlHuyThanhtoan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 35);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(859, 58);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlHuyThanhtoan
            // 
            this.pnlHuyThanhtoan.Controls.Add(this.cmdHuyQR);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdTaoQR);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdAccept);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdExit);
            this.pnlHuyThanhtoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHuyThanhtoan.Location = new System.Drawing.Point(0, 0);
            this.pnlHuyThanhtoan.Name = "pnlHuyThanhtoan";
            this.pnlHuyThanhtoan.Size = new System.Drawing.Size(859, 58);
            this.pnlHuyThanhtoan.TabIndex = 1;
            // 
            // txtTongtien
            // 
            this.txtTongtien.BackColor = System.Drawing.Color.White;
            this.txtTongtien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongtien.Enabled = false;
            this.txtTongtien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtien.Location = new System.Drawing.Point(685, 27);
            this.txtTongtien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.ReadOnly = true;
            this.txtTongtien.Size = new System.Drawing.Size(143, 22);
            this.txtTongtien.TabIndex = 2;
            this.txtTongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(613, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 24);
            this.label9.TabIndex = 371;
            this.label9.Text = "Tổng tiền";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNgaythanhtoan
            // 
            this.lblNgaythanhtoan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaythanhtoan.ForeColor = System.Drawing.Color.Navy;
            this.lblNgaythanhtoan.Location = new System.Drawing.Point(6, 23);
            this.lblNgaythanhtoan.Name = "lblNgaythanhtoan";
            this.lblNgaythanhtoan.Size = new System.Drawing.Size(112, 24);
            this.lblNgaythanhtoan.TabIndex = 27;
            this.lblNgaythanhtoan.Text = "Ngày thanh toán:";
            this.lblNgaythanhtoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtPaymentDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtPaymentDate.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtPaymentDate.DropDownCalendar.Name = "";
            this.dtPaymentDate.Enabled = false;
            this.dtPaymentDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPaymentDate.Location = new System.Drawing.Point(124, 26);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.ShowUpDown = true;
            this.dtPaymentDate.Size = new System.Drawing.Size(147, 21);
            this.dtPaymentDate.TabIndex = 0;
            this.dtPaymentDate.TabStop = false;
            this.dtPaymentDate.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cboNganhangChung);
            this.uiGroupBox2.Controls.Add(this.cboPttt_chung);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.vbLine2);
            this.uiGroupBox2.Controls.Add(this.pnlPhanbo);
            this.uiGroupBox2.Controls.Add(this.txtTongtien);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.dtPaymentDate);
            this.uiGroupBox2.Controls.Add(this.lblNgaythanhtoan);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(859, 468);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết phân bổ tiền theo hình thức thanh toán";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // cboNganhangChung
            // 
            this.cboNganhangChung.BackColor = System.Drawing.Color.White;
            this.cboNganhangChung.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboNganhangChung.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboNganhangChung.Font = new System.Drawing.Font("Arial", 9F);
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "In nhiệt";
            uiComboBoxItem1.Value = "0";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "In laser";
            uiComboBoxItem2.Value = "1";
            this.cboNganhangChung.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboNganhangChung.Location = new System.Drawing.Point(391, 55);
            this.cboNganhangChung.Name = "cboNganhangChung";
            this.cboNganhangChung.Size = new System.Drawing.Size(437, 21);
            this.cboNganhangChung.TabIndex = 3;
            this.cboNganhangChung.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboNganhangChung.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cboPttt_chung
            // 
            this.cboPttt_chung.BackColor = System.Drawing.Color.White;
            this.cboPttt_chung.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboPttt_chung.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPttt_chung.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "In nhiệt";
            uiComboBoxItem3.Value = "0";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "In laser";
            uiComboBoxItem4.Value = "1";
            this.cboPttt_chung.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4});
            this.cboPttt_chung.Location = new System.Drawing.Point(391, 28);
            this.cboPttt_chung.Name = "cboPttt_chung";
            this.cboPttt_chung.Size = new System.Drawing.Size(216, 21);
            this.cboPttt_chung.TabIndex = 1;
            this.cboPttt_chung.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboPttt_chung.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cboPttt_chung.SelectedIndexChanged += new System.EventHandler(this.cboPttt_chung_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(282, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 601;
            this.label5.Text = "Ngân hàng:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vbLine2
            // 
            this.vbLine2._FontColor = System.Drawing.SystemColors.WindowText;
            this.vbLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine2.BackColor = System.Drawing.Color.Transparent;
            this.vbLine2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.vbLine2.Location = new System.Drawing.Point(14, 90);
            this.vbLine2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.vbLine2.Name = "vbLine2";
            this.vbLine2.Size = new System.Drawing.Size(828, 22);
            this.vbLine2.TabIndex = 598;
            this.vbLine2.TabStop = false;
            this.vbLine2.YourText = "Thực hiện phân bổ PTTT";
            // 
            // pnlPhanbo
            // 
            this.pnlPhanbo.Controls.Add(this.cboNganhang);
            this.pnlPhanbo.Controls.Add(this.cboPttt_rieng);
            this.pnlPhanbo.Controls.Add(this.label27);
            this.pnlPhanbo.Controls.Add(this.label4);
            this.pnlPhanbo.Controls.Add(this.txtSotien);
            this.pnlPhanbo.Controls.Add(this.label2);
            this.pnlPhanbo.Location = new System.Drawing.Point(15, 119);
            this.pnlPhanbo.Name = "pnlPhanbo";
            this.pnlPhanbo.Size = new System.Drawing.Size(827, 86);
            this.pnlPhanbo.TabIndex = 599;
            // 
            // cboNganhang
            // 
            this.cboNganhang.BackColor = System.Drawing.Color.White;
            this.cboNganhang.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboNganhang.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboNganhang.Enabled = false;
            this.cboNganhang.Font = new System.Drawing.Font("Arial", 9F);
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "In nhiệt";
            uiComboBoxItem5.Value = "0";
            uiComboBoxItem6.FormatStyle.Alpha = 0;
            uiComboBoxItem6.IsSeparator = false;
            uiComboBoxItem6.Text = "In laser";
            uiComboBoxItem6.Value = "1";
            this.cboNganhang.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem5,
            uiComboBoxItem6});
            this.cboNganhang.Location = new System.Drawing.Point(376, 17);
            this.cboNganhang.Name = "cboNganhang";
            this.cboNganhang.Size = new System.Drawing.Size(437, 21);
            this.cboNganhang.TabIndex = 5;
            this.cboNganhang.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboNganhang.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cboPttt_rieng
            // 
            this.cboPttt_rieng.BackColor = System.Drawing.Color.White;
            this.cboPttt_rieng.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboPttt_rieng.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPttt_rieng.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem7.FormatStyle.Alpha = 0;
            uiComboBoxItem7.IsSeparator = false;
            uiComboBoxItem7.Text = "In nhiệt";
            uiComboBoxItem7.Value = "0";
            uiComboBoxItem8.FormatStyle.Alpha = 0;
            uiComboBoxItem8.IsSeparator = false;
            uiComboBoxItem8.Text = "In laser";
            uiComboBoxItem8.Value = "1";
            this.cboPttt_rieng.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem7,
            uiComboBoxItem8});
            this.cboPttt_rieng.Location = new System.Drawing.Point(88, 20);
            this.cboPttt_rieng.Name = "cboPttt_rieng";
            this.cboPttt_rieng.Size = new System.Drawing.Size(168, 21);
            this.cboPttt_rieng.TabIndex = 4;
            this.cboPttt_rieng.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboPttt_rieng.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cboPttt_rieng.SelectedIndexChanged += new System.EventHandler(this.cboPttt_rieng_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(270, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(103, 24);
            this.label27.TabIndex = 393;
            this.label27.Text = "Ngân hàng:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(15, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 24);
            this.label4.TabIndex = 397;
            this.label4.Text = "PTTT:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(270, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 24);
            this.label2.TabIndex = 384;
            this.label2.Text = "Số tiền:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(771, 24);
            this.label7.TabIndex = 395;
            this.label7.Text = "Bạn có thể nhấn Ctr+C để lấy số tiền phân bổ còn lại cho phương thức thanh toán đ" +
    "ang chọn";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(3, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(853, 24);
            this.label3.TabIndex = 394;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.BackColor = System.Drawing.Color.Silver;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 12F);
            this.grdList.FrozenColumns = 3;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 244);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(853, 221);
            this.grdList.TabIndex = 385;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(282, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 383;
            this.label1.Text = "PTTT chung:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_PhanbotientheoPTTT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(859, 561);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PhanbotientheoPTTT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân bổ tiền theo hình thức thanh toán";
            this.pnlInfor.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlHuyThanhtoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.pnlPhanbo.ResumeLayout(false);
            this.pnlPhanbo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlHuyThanhtoan;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNgaythanhtoan;
        private Janus.Windows.CalendarCombo.CalendarCombo dtPaymentDate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private MaskedTextBox.MaskedTextBox txtTongtien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private VNS.UCs.VBLine vbLine2;
        private System.Windows.Forms.Panel pnlPhanbo;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIComboBox cboPttt_chung;
        private Janus.Windows.EditControls.UIComboBox cboNganhang;
        private Janus.Windows.EditControls.UIComboBox cboPttt_rieng;
        private Janus.Windows.EditControls.UIComboBox cboNganhangChung;
        private Janus.Windows.EditControls.UIButton cmdHuyQR;
        private Janus.Windows.EditControls.UIButton cmdTaoQR;
    }
}