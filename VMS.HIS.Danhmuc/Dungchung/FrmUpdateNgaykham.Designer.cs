namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class FrmUpdateNgaykham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateNgaykham));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.statusBarPanel1 = new Janus.Windows.Ribbon.StatusBarPanel();
            this.statusBarPanel2 = new Janus.Windows.Ribbon.StatusBarPanel();
            this.statusBarPanel3 = new Janus.Windows.Ribbon.StatusBarPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSoDT = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtnoidkkcb = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtnoidongtruso = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtmathe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboPatientSex = new Janus.Windows.EditControls.UIComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNgoaitru = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblngayttoan = new System.Windows.Forms.Label();
            this.lblngayravien = new System.Windows.Forms.Label();
            this.lblngaynhapvien = new System.Windows.Forms.Label();
            this.lblngaytiepdon = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDoiTuong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpNgayTToan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpNgayRaVien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpNgayNhapVien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpNgayTiepDon = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtDiaChi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtNamSinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtHoTen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtidbenhnhan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonStatusBar1.ImageSize = new System.Drawing.Size(16, 16);
            this.ribbonStatusBar1.LeftPanelCommands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.statusBarPanel1,
            this.statusBarPanel2,
            this.statusBarPanel3});
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 538);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.Empty;
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(784, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 0;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Key = "statusBarPanel1";
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.SizeStyle = Janus.Windows.Ribbon.CommandSizeStyle.Small;
            this.statusBarPanel1.Text = "Ctrl+S: Cập nhật";
            this.statusBarPanel1.Width = 85;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.Key = "statusBarPanel2";
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.SizeStyle = Janus.Windows.Ribbon.CommandSizeStyle.Small;
            this.statusBarPanel2.Text = "Esc: Thoát";
            this.statusBarPanel2.Width = 70;
            // 
            // statusBarPanel3
            // 
            this.statusBarPanel3.Key = "statusBarPanel3";
            this.statusBarPanel3.Name = "statusBarPanel3";
            this.statusBarPanel3.SizeStyle = Janus.Windows.Ribbon.CommandSizeStyle.Small;
            this.statusBarPanel3.Text = "Sửa thông tin thẻ sẽ không sửa được % thẻ";
            this.statusBarPanel3.Width = 220;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.txtSoDT);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.txtnoidkkcb);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtnoidongtruso);
            this.splitContainer1.Panel1.Controls.Add(this.txtmathe);
            this.splitContainer1.Panel1.Controls.Add(this.cboPatientSex);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.txtNgoaitru);
            this.splitContainer1.Panel1.Controls.Add(this.lblngayttoan);
            this.splitContainer1.Panel1.Controls.Add(this.lblngayravien);
            this.splitContainer1.Panel1.Controls.Add(this.lblngaynhapvien);
            this.splitContainer1.Panel1.Controls.Add(this.lblngaytiepdon);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.txtDoiTuong);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpNgayTToan);
            this.splitContainer1.Panel1.Controls.Add(this.dtpNgayRaVien);
            this.splitContainer1.Panel1.Controls.Add(this.dtpNgayNhapVien);
            this.splitContainer1.Panel1.Controls.Add(this.dtpNgayTiepDon);
            this.splitContainer1.Panel1.Controls.Add(this.txtDiaChi);
            this.splitContainer1.Panel1.Controls.Add(this.txtNamSinh);
            this.splitContainer1.Panel1.Controls.Add(this.txtHoTen);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.txtidbenhnhan);
            this.splitContainer1.Panel2.Controls.Add(this.cmdExit);
            this.splitContainer1.Panel2.Controls.Add(this.cmdUpdate);
            this.splitContainer1.Size = new System.Drawing.Size(784, 538);
            this.splitContainer1.SplitterDistance = 483;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 91);
            this.panel1.TabIndex = 413;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(136, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(634, 44);
            this.label10.TabIndex = 542;
            this.label10.Text = "Thực hiện cập nhật thông tin người bệnh bao gồm: Họ và tên, điện thoại, giới tính" +
    ", năm sinh và các ngày khám";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(98, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(262, 28);
            this.label12.TabIndex = 541;
            this.label12.Text = "Cập nhật thông tin khám";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(12, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 69);
            this.panel2.TabIndex = 0;
            // 
            // txtSoDT
            // 
            this.txtSoDT.BackColor = System.Drawing.Color.White;
            this.txtSoDT.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDT.Location = new System.Drawing.Point(433, 136);
            this.txtSoDT.Name = "txtSoDT";
            this.txtSoDT.Size = new System.Drawing.Size(250, 23);
            this.txtSoDT.TabIndex = 411;
            this.txtSoDT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(329, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 21);
            this.label9.TabIndex = 412;
            this.label9.Text = "Điện thoại:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtnoidkkcb
            // 
            this.txtnoidkkcb.Location = new System.Drawing.Point(625, 217);
            this.txtnoidkkcb.Name = "txtnoidkkcb";
            this.txtnoidkkcb.Size = new System.Drawing.Size(58, 23);
            this.txtnoidkkcb.TabIndex = 410;
            this.txtnoidkkcb.TabStop = false;
            this.txtnoidkkcb.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtnoidkkcb.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(66, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 16);
            this.label8.TabIndex = 409;
            this.label8.Text = "Mã thẻ:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtnoidongtruso
            // 
            this.txtnoidongtruso.Location = new System.Drawing.Point(547, 217);
            this.txtnoidongtruso.Name = "txtnoidongtruso";
            this.txtnoidongtruso.Size = new System.Drawing.Size(72, 23);
            this.txtnoidongtruso.TabIndex = 408;
            this.txtnoidongtruso.TabStop = false;
            this.txtnoidongtruso.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtnoidongtruso.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtmathe
            // 
            this.txtmathe.Location = new System.Drawing.Point(195, 217);
            this.txtmathe.Name = "txtmathe";
            this.txtmathe.Size = new System.Drawing.Size(346, 23);
            this.txtmathe.TabIndex = 407;
            this.txtmathe.TabStop = false;
            this.txtmathe.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cboPatientSex
            // 
            this.cboPatientSex.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPatientSex.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Nam";
            uiComboBoxItem1.Value = 0;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Nữ";
            uiComboBoxItem2.Value = 1;
            this.cboPatientSex.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboPatientSex.Location = new System.Drawing.Point(195, 136);
            this.cboPatientSex.Name = "cboPatientSex";
            this.cboPatientSex.Size = new System.Drawing.Size(138, 23);
            this.cboPatientSex.TabIndex = 405;
            this.cboPatientSex.Text = "Giới tính";
            this.cboPatientSex.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label11.Location = new System.Drawing.Point(66, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 21);
            this.label11.TabIndex = 406;
            this.label11.Text = "Giới tính:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNgoaitru
            // 
            this.txtNgoaitru.Location = new System.Drawing.Point(547, 188);
            this.txtNgoaitru.Name = "txtNgoaitru";
            this.txtNgoaitru.Size = new System.Drawing.Size(136, 23);
            this.txtNgoaitru.TabIndex = 19;
            this.txtNgoaitru.TabStop = false;
            this.txtNgoaitru.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtNgoaitru.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // lblngayttoan
            // 
            this.lblngayttoan.ForeColor = System.Drawing.Color.Red;
            this.lblngayttoan.Location = new System.Drawing.Point(559, 334);
            this.lblngayttoan.Name = "lblngayttoan";
            this.lblngayttoan.Size = new System.Drawing.Size(124, 20);
            this.lblngayttoan.TabIndex = 18;
            // 
            // lblngayravien
            // 
            this.lblngayravien.ForeColor = System.Drawing.Color.Red;
            this.lblngayravien.Location = new System.Drawing.Point(558, 304);
            this.lblngayravien.Name = "lblngayravien";
            this.lblngayravien.Size = new System.Drawing.Size(124, 20);
            this.lblngayravien.TabIndex = 17;
            // 
            // lblngaynhapvien
            // 
            this.lblngaynhapvien.ForeColor = System.Drawing.Color.Red;
            this.lblngaynhapvien.Location = new System.Drawing.Point(558, 279);
            this.lblngaynhapvien.Name = "lblngaynhapvien";
            this.lblngaynhapvien.Size = new System.Drawing.Size(124, 20);
            this.lblngaynhapvien.TabIndex = 16;
            // 
            // lblngaytiepdon
            // 
            this.lblngaytiepdon.ForeColor = System.Drawing.Color.Red;
            this.lblngaytiepdon.Location = new System.Drawing.Point(559, 250);
            this.lblngaytiepdon.Name = "lblngaytiepdon";
            this.lblngaytiepdon.Size = new System.Drawing.Size(124, 20);
            this.lblngaytiepdon.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(66, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Đối tượng:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDoiTuong
            // 
            this.txtDoiTuong.Location = new System.Drawing.Point(195, 188);
            this.txtDoiTuong.Name = "txtDoiTuong";
            this.txtDoiTuong.Size = new System.Drawing.Size(346, 23);
            this.txtDoiTuong.TabIndex = 13;
            this.txtDoiTuong.TabStop = false;
            this.txtDoiTuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(66, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Họ và tên:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayTToan
            // 
            this.dtpNgayTToan.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTToan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayTToan.DropDownCalendar.Name = "";
            this.dtpNgayTToan.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayTToan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayTToan.IsNullDate = true;
            this.dtpNgayTToan.Location = new System.Drawing.Point(195, 332);
            this.dtpNgayTToan.Name = "dtpNgayTToan";
            this.dtpNgayTToan.ShowUpDown = true;
            this.dtpNgayTToan.Size = new System.Drawing.Size(346, 23);
            this.dtpNgayTToan.TabIndex = 6;
            this.dtpNgayTToan.TabStop = false;
            // 
            // dtpNgayRaVien
            // 
            this.dtpNgayRaVien.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayRaVien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayRaVien.DropDownCalendar.Name = "";
            this.dtpNgayRaVien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayRaVien.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayRaVien.IsNullDate = true;
            this.dtpNgayRaVien.Location = new System.Drawing.Point(195, 303);
            this.dtpNgayRaVien.Name = "dtpNgayRaVien";
            this.dtpNgayRaVien.ShowUpDown = true;
            this.dtpNgayRaVien.Size = new System.Drawing.Size(346, 23);
            this.dtpNgayRaVien.TabIndex = 5;
            this.dtpNgayRaVien.TabStop = false;
            // 
            // dtpNgayNhapVien
            // 
            this.dtpNgayNhapVien.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayNhapVien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayNhapVien.DropDownCalendar.Name = "";
            this.dtpNgayNhapVien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayNhapVien.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayNhapVien.IsNullDate = true;
            this.dtpNgayNhapVien.Location = new System.Drawing.Point(195, 276);
            this.dtpNgayNhapVien.Name = "dtpNgayNhapVien";
            this.dtpNgayNhapVien.ShowUpDown = true;
            this.dtpNgayNhapVien.Size = new System.Drawing.Size(346, 23);
            this.dtpNgayNhapVien.TabIndex = 4;
            this.dtpNgayNhapVien.TabStop = false;
            // 
            // dtpNgayTiepDon
            // 
            this.dtpNgayTiepDon.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTiepDon.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayTiepDon.DropDownCalendar.Name = "";
            this.dtpNgayTiepDon.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayTiepDon.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayTiepDon.IsNullDate = true;
            this.dtpNgayTiepDon.Location = new System.Drawing.Point(195, 247);
            this.dtpNgayTiepDon.Name = "dtpNgayTiepDon";
            this.dtpNgayTiepDon.ShowUpDown = true;
            this.dtpNgayTiepDon.Size = new System.Drawing.Size(346, 23);
            this.dtpNgayTiepDon.TabIndex = 3;
            this.dtpNgayTiepDon.TabStop = false;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(195, 161);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(488, 23);
            this.txtDiaChi.TabIndex = 2;
            this.txtDiaChi.TabStop = false;
            this.txtDiaChi.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtNamSinh
            // 
            this.txtNamSinh.Location = new System.Drawing.Point(547, 110);
            this.txtNamSinh.Name = "txtNamSinh";
            this.txtNamSinh.Size = new System.Drawing.Size(136, 23);
            this.txtNamSinh.TabIndex = 1;
            this.txtNamSinh.TabStop = false;
            this.txtNamSinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtNamSinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoTen.Location = new System.Drawing.Point(195, 110);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(346, 23);
            this.txtHoTen.TabIndex = 0;
            this.txtHoTen.TabStop = false;
            this.txtHoTen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(66, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ngày ttoán:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(66, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ngày ra viện:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(66, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ngày nhập viện:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(66, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ngày tiếp đón:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(66, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Địa chỉ:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtidbenhnhan
            // 
            this.txtidbenhnhan.Location = new System.Drawing.Point(3, 8);
            this.txtidbenhnhan.Name = "txtidbenhnhan";
            this.txtidbenhnhan.Size = new System.Drawing.Size(58, 23);
            this.txtidbenhnhan.TabIndex = 413;
            this.txtidbenhnhan.TabStop = false;
            this.txtidbenhnhan.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtidbenhnhan.Visible = false;
            this.txtidbenhnhan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdExit
            // 
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(661, 8);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdUpdate.Location = new System.Drawing.Point(536, 8);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(120, 35);
            this.cmdUpdate.TabIndex = 0;
            this.cmdUpdate.Text = "Cập nhật";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // FrmUpdateNgaykham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdateNgaykham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin khám";
            this.Load += new System.EventHandler(this.FrmUpdateNgaykham_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUpdateNgaykham_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.GridEX.EditControls.EditBox txtDiaChi;
        private Janus.Windows.GridEX.EditControls.EditBox txtNamSinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtHoTen;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayTToan;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayRaVien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayNhapVien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayTiepDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtDoiTuong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblngayttoan;
        private System.Windows.Forms.Label lblngayravien;
        private System.Windows.Forms.Label lblngaynhapvien;
        private System.Windows.Forms.Label lblngaytiepdon;
        private Janus.Windows.GridEX.EditControls.EditBox txtNgoaitru;
        private Janus.Windows.Ribbon.StatusBarPanel statusBarPanel1;
        private Janus.Windows.Ribbon.StatusBarPanel statusBarPanel2;
        private Janus.Windows.EditControls.UIComboBox cboPatientSex;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtnoidongtruso;
        private Janus.Windows.GridEX.EditControls.EditBox txtmathe;
        private Janus.Windows.GridEX.EditControls.EditBox txtnoidkkcb;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtSoDT;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.GridEX.EditControls.EditBox txtidbenhnhan;
        private Janus.Windows.Ribbon.StatusBarPanel statusBarPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
    }
}