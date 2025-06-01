using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_Phieuravien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Phieuravien));
            Janus.Windows.GridEX.GridEXLayout grd_ICD_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPresDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ucThongtinnguoibenh1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cmdHuyphieuravien = new Janus.Windows.EditControls.UIButton();
            this.cmdChuyenvien = new Janus.Windows.EditControls.UIButton();
            this.cmdChuyen = new Janus.Windows.EditControls.UIButton();
            this.chkThoatsaukhiluu = new Janus.Windows.EditControls.UICheckBox();
            this.chkInsaukhiluu = new Janus.Windows.EditControls.UICheckBox();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdHuy = new Janus.Windows.EditControls.UIButton();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpNgayin = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.txtMatheBHYT = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label39 = new System.Windows.Forms.Label();
            this.dtInsToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label40 = new System.Windows.Forms.Label();
            this.lblMatheBHYT = new System.Windows.Forms.Label();
            this.dtInsFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtTenBenhChinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtChandoan = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoLydotuvong = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBenhgiaiphau = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.chkHentaikham = new Janus.Windows.EditControls.UICheckBox();
            this.txtSoNgayHen = new MaskedTextBox.MaskedTextBox();
            this.dtpNgayHen = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSongayhentaikham = new MaskedTextBox.MaskedTextBox();
            this.txtBenhchinh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label17 = new System.Windows.Forms.Label();
            this.chkTuvong = new Janus.Windows.EditControls.UICheckBox();
            this.dtpNgaytuvong = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtsotuanthai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBsChidinh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtTruongkhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label48 = new System.Windows.Forms.Label();
            this.grd_ICD = new Janus.Windows.GridEX.GridEX();
            this.txtSoRaVien = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label31 = new System.Windows.Forms.Label();
            this.dtpNgaynhapvien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label30 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtPhuongphapdieutri = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtTinhtrangravien = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtKqdieutri = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtBenhphu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBenhnguyennhan = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBenhbienchung = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label27 = new System.Windows.Forms.Label();
            this.chkDaCapGiayRaVien = new Janus.Windows.EditControls.UICheckBox();
            this.txtTongSoNgayDtri = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.dtNGAY_CAP_GIAY_RVIEN = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkPhuHopChanDoanCLS = new Janus.Windows.EditControls.UICheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPhutRaVien = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtGioRaVien = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpNgayravien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNguoivanchuyen = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtNoichuyenden = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtKieuchuyenvien = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cmdGetBV = new Janus.Windows.EditControls.UIButton();
            this.chkChuyenvien = new Janus.Windows.EditControls.UICheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtphuongtienvc = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label25 = new System.Windows.Forms.Label();
            this.txtYkien = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label21 = new System.Windows.Forms.Label();
            this.grdPresDetail = new Janus.Windows.GridEX.GridEX();
            this.pnlDonthuoc = new System.Windows.Forms.Panel();
            this.cmdWords = new Janus.Windows.EditControls.UIButton();
            this.cmdCreateNewPresTuTuc = new Janus.Windows.EditControls.UIButton();
            this.cmdPrintPres = new Janus.Windows.EditControls.UIButton();
            this.cmdDeletePres = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdatePres = new Janus.Windows.EditControls.UIButton();
            this.cmdCreateNewPres = new Janus.Windows.EditControls.UIButton();
            this.txtLoidanBS = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uitabpagethongtin = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTabPageloidan = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTabPagedonthuoc = new Janus.Windows.UI.Tab.UITabPage();
            this.cboKhoaRavien = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_ICD)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).BeginInit();
            this.pnlDonthuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uitabpagethongtin.SuspendLayout();
            this.uiTabPageloidan.SuspendLayout();
            this.uiTabPagedonthuoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ucThongtinnguoibenh1);
            this.pnlTop.Controls.Add(this.baocaO_TIEUDE1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(996, 137);
            this.pnlTop.TabIndex = 0;
            // 
            // ucThongtinnguoibenh1
            // 
            this.ucThongtinnguoibenh1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucThongtinnguoibenh1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucThongtinnguoibenh1.Location = new System.Drawing.Point(40, 27);
            this.ucThongtinnguoibenh1.Name = "ucThongtinnguoibenh1";
            this.ucThongtinnguoibenh1.Size = new System.Drawing.Size(953, 111);
            this.ucThongtinnguoibenh1.TabIndex = 0;
            this.ucThongtinnguoibenh1.TabStop = false;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "NOITRU_PHIEURAVIEN";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(996, 30);
            this.baocaO_TIEUDE1.TabIndex = 24;
            this.baocaO_TIEUDE1.TabStop = false;
            this.baocaO_TIEUDE1.TIEUDE = "PHIẾU RA VIỆN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.cmdHuyphieuravien);
            this.pnlBottom.Controls.Add(this.cmdChuyenvien);
            this.pnlBottom.Controls.Add(this.cmdChuyen);
            this.pnlBottom.Controls.Add(this.chkThoatsaukhiluu);
            this.pnlBottom.Controls.Add(this.chkInsaukhiluu);
            this.pnlBottom.Controls.Add(this.cmdPrint);
            this.pnlBottom.Controls.Add(this.cmdExit);
            this.pnlBottom.Controls.Add(this.cmdHuy);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 672);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(996, 67);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.TabStop = true;
            // 
            // cmdHuyphieuravien
            // 
            this.cmdHuyphieuravien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuyphieuravien.Enabled = false;
            this.cmdHuyphieuravien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuyphieuravien.Image = global::VMS.HIS.Danhmuc.Properties.Resources.trash_full_24;
            this.cmdHuyphieuravien.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHuyphieuravien.Location = new System.Drawing.Point(486, 21);
            this.cmdHuyphieuravien.Name = "cmdHuyphieuravien";
            this.cmdHuyphieuravien.Size = new System.Drawing.Size(120, 33);
            this.cmdHuyphieuravien.TabIndex = 64;
            this.cmdHuyphieuravien.Text = "Hủy phiếu ra viện";
            this.cmdHuyphieuravien.Click += new System.EventHandler(this.cmdHuyphieuravien_Click);
            // 
            // cmdChuyenvien
            // 
            this.cmdChuyenvien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChuyenvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyenvien.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyenvien.Image")));
            this.cmdChuyenvien.ImageSize = new System.Drawing.Size(28, 28);
            this.cmdChuyenvien.Location = new System.Drawing.Point(234, 21);
            this.cmdChuyenvien.Name = "cmdChuyenvien";
            this.cmdChuyenvien.Size = new System.Drawing.Size(120, 33);
            this.cmdChuyenvien.TabIndex = 63;
            this.cmdChuyenvien.Text = "Chuyển viện";
            this.cmdChuyenvien.Visible = false;
            this.cmdChuyenvien.Click += new System.EventHandler(this.cmdChuyenvien_Click);
            // 
            // cmdChuyen
            // 
            this.cmdChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChuyen.Enabled = false;
            this.cmdChuyen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyen.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyen.Image")));
            this.cmdChuyen.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChuyen.Location = new System.Drawing.Point(360, 21);
            this.cmdChuyen.Name = "cmdChuyen";
            this.cmdChuyen.Size = new System.Drawing.Size(120, 33);
            this.cmdChuyen.TabIndex = 51;
            this.cmdChuyen.Text = "Lưu (Ctrl+S)";
            this.cmdChuyen.Click += new System.EventHandler(this.cmdChuyen_Click_2);
            // 
            // chkThoatsaukhiluu
            // 
            this.chkThoatsaukhiluu.Checked = true;
            this.chkThoatsaukhiluu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThoatsaukhiluu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThoatsaukhiluu.Location = new System.Drawing.Point(7, 6);
            this.chkThoatsaukhiluu.Name = "chkThoatsaukhiluu";
            this.chkThoatsaukhiluu.Size = new System.Drawing.Size(202, 23);
            this.chkThoatsaukhiluu.TabIndex = 60;
            this.chkThoatsaukhiluu.TabStop = false;
            this.chkThoatsaukhiluu.Tag = "NOITRU_RAVIEN_THOATSAUKHILUU";
            this.chkThoatsaukhiluu.Text = "Thoát chức năng sau khi Lưu?";
            this.chkThoatsaukhiluu.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // chkInsaukhiluu
            // 
            this.chkInsaukhiluu.Checked = true;
            this.chkInsaukhiluu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsaukhiluu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsaukhiluu.Location = new System.Drawing.Point(7, 32);
            this.chkInsaukhiluu.Name = "chkInsaukhiluu";
            this.chkInsaukhiluu.Size = new System.Drawing.Size(153, 23);
            this.chkInsaukhiluu.TabIndex = 61;
            this.chkInsaukhiluu.TabStop = false;
            this.chkInsaukhiluu.Tag = "NOITRU_RAVIEN_INSAUKHILUU";
            this.chkInsaukhiluu.Text = "In ngay sau khi Lưu?";
            this.chkInsaukhiluu.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(612, 21);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 33);
            this.cmdPrint.TabIndex = 52;
            this.cmdPrint.Text = "In (Ctrl+P)";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(864, 21);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 33);
            this.cmdExit.TabIndex = 54;
            this.cmdExit.Text = "Thoát(Esc)";
            // 
            // cmdHuy
            // 
            this.cmdHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuy.Enabled = false;
            this.cmdHuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuy.Image = global::VMS.HIS.Danhmuc.Properties.Resources.HOME_24;
            this.cmdHuy.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHuy.Location = new System.Drawing.Point(738, 21);
            this.cmdHuy.Name = "cmdHuy";
            this.cmdHuy.Size = new System.Drawing.Size(120, 33);
            this.cmdHuy.TabIndex = 53;
            this.cmdHuy.Tag = "0";
            this.cmdHuy.Text = "Ra viện";
            this.cmdHuy.Click += new System.EventHandler(this.cmdHuy_Click_1);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Green;
            this.label20.Location = new System.Drawing.Point(84, 447);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 23);
            this.label20.TabIndex = 21;
            this.label20.Text = "Ngày in";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayin
            // 
            this.dtpNgayin.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayin.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayin.DropDownCalendar.Name = "";
            this.dtpNgayin.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayin.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayin.Location = new System.Drawing.Point(150, 448);
            this.dtpNgayin.Name = "dtpNgayin";
            this.dtpNgayin.ShowUpDown = true;
            this.dtpNgayin.Size = new System.Drawing.Size(123, 21);
            this.dtpNgayin.TabIndex = 62;
            this.dtpNgayin.TabStop = false;
            this.dtpNgayin.Value = new System.DateTime(2013, 1, 16, 0, 0, 0, 0);
            this.dtpNgayin.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFill.Controls.Add(this.label6);
            this.pnlFill.Controls.Add(this.cboKhoaRavien);
            this.pnlFill.Controls.Add(this.txtMatheBHYT);
            this.pnlFill.Controls.Add(this.label39);
            this.pnlFill.Controls.Add(this.dtInsToDate);
            this.pnlFill.Controls.Add(this.label40);
            this.pnlFill.Controls.Add(this.lblMatheBHYT);
            this.pnlFill.Controls.Add(this.dtInsFromDate);
            this.pnlFill.Controls.Add(this.txtTenBenhChinh);
            this.pnlFill.Controls.Add(this.txtChandoan);
            this.pnlFill.Controls.Add(this.autoLydotuvong);
            this.pnlFill.Controls.Add(this.label5);
            this.pnlFill.Controls.Add(this.txtBenhgiaiphau);
            this.pnlFill.Controls.Add(this.chkHentaikham);
            this.pnlFill.Controls.Add(this.label20);
            this.pnlFill.Controls.Add(this.txtSoNgayHen);
            this.pnlFill.Controls.Add(this.dtpNgayin);
            this.pnlFill.Controls.Add(this.dtpNgayHen);
            this.pnlFill.Controls.Add(this.label4);
            this.pnlFill.Controls.Add(this.txtSongayhentaikham);
            this.pnlFill.Controls.Add(this.txtBenhchinh);
            this.pnlFill.Controls.Add(this.label17);
            this.pnlFill.Controls.Add(this.chkTuvong);
            this.pnlFill.Controls.Add(this.dtpNgaytuvong);
            this.pnlFill.Controls.Add(this.txtsotuanthai);
            this.pnlFill.Controls.Add(this.label2);
            this.pnlFill.Controls.Add(this.label1);
            this.pnlFill.Controls.Add(this.txtBsChidinh);
            this.pnlFill.Controls.Add(this.txtTruongkhoa);
            this.pnlFill.Controls.Add(this.label48);
            this.pnlFill.Controls.Add(this.grd_ICD);
            this.pnlFill.Controls.Add(this.txtSoRaVien);
            this.pnlFill.Controls.Add(this.label31);
            this.pnlFill.Controls.Add(this.dtpNgaynhapvien);
            this.pnlFill.Controls.Add(this.label30);
            this.pnlFill.Controls.Add(this.label26);
            this.pnlFill.Controls.Add(this.txtPhuongphapdieutri);
            this.pnlFill.Controls.Add(this.txtTinhtrangravien);
            this.pnlFill.Controls.Add(this.txtKqdieutri);
            this.pnlFill.Controls.Add(this.txtBenhphu);
            this.pnlFill.Controls.Add(this.txtBenhnguyennhan);
            this.pnlFill.Controls.Add(this.txtBenhbienchung);
            this.pnlFill.Controls.Add(this.label27);
            this.pnlFill.Controls.Add(this.chkDaCapGiayRaVien);
            this.pnlFill.Controls.Add(this.txtTongSoNgayDtri);
            this.pnlFill.Controls.Add(this.dtNGAY_CAP_GIAY_RVIEN);
            this.pnlFill.Controls.Add(this.chkPhuHopChanDoanCLS);
            this.pnlFill.Controls.Add(this.label22);
            this.pnlFill.Controls.Add(this.label12);
            this.pnlFill.Controls.Add(this.label13);
            this.pnlFill.Controls.Add(this.label14);
            this.pnlFill.Controls.Add(this.label15);
            this.pnlFill.Controls.Add(this.label16);
            this.pnlFill.Controls.Add(this.label18);
            this.pnlFill.Controls.Add(this.txtPhutRaVien);
            this.pnlFill.Controls.Add(this.label19);
            this.pnlFill.Controls.Add(this.label23);
            this.pnlFill.Controls.Add(this.txtGioRaVien);
            this.pnlFill.Controls.Add(this.label24);
            this.pnlFill.Controls.Add(this.dtpNgayravien);
            this.pnlFill.Controls.Add(this.lblMsg);
            this.pnlFill.Controls.Add(this.txtId);
            this.pnlFill.Controls.Add(this.label3);
            this.pnlFill.Controls.Add(this.groupBox1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(992, 509);
            this.pnlFill.TabIndex = 1;
            this.pnlFill.TabStop = true;
            // 
            // txtMatheBHYT
            // 
            this.txtMatheBHYT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatheBHYT.Location = new System.Drawing.Point(642, 53);
            this.txtMatheBHYT.MaxLength = 30;
            this.txtMatheBHYT.Name = "txtMatheBHYT";
            this.txtMatheBHYT.Size = new System.Drawing.Size(338, 21);
            this.txtMatheBHYT.TabIndex = 4;
            this.txtMatheBHYT.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMatheBHYT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(556, 78);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(83, 21);
            this.label39.TabIndex = 659;
            this.label39.Text = "Hiệu lực từ: ";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtInsToDate
            // 
            this.dtInsToDate.CustomFormat = "dd/MM/yyyy";
            this.dtInsToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtInsToDate.DropDownCalendar.FirstMonth = new System.DateTime(2023, 5, 1, 0, 0, 0, 0);
            this.dtInsToDate.DropDownCalendar.Name = "";
            this.dtInsToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtInsToDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtInsToDate.Location = new System.Drawing.Point(848, 76);
            this.dtInsToDate.Name = "dtInsToDate";
            this.dtInsToDate.ShowUpDown = true;
            this.dtInsToDate.Size = new System.Drawing.Size(132, 22);
            this.dtInsToDate.TabIndex = 5;
            this.dtInsToDate.Value = new System.DateTime(2013, 9, 23, 0, 0, 0, 0);
            this.dtInsToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.SystemColors.Control;
            this.label40.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(784, 78);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(57, 15);
            this.label40.TabIndex = 660;
            this.label40.Text = "đến ngày";
            // 
            // lblMatheBHYT
            // 
            this.lblMatheBHYT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatheBHYT.ForeColor = System.Drawing.Color.Black;
            this.lblMatheBHYT.Location = new System.Drawing.Point(558, 56);
            this.lblMatheBHYT.Name = "lblMatheBHYT";
            this.lblMatheBHYT.Size = new System.Drawing.Size(85, 21);
            this.lblMatheBHYT.TabIndex = 658;
            this.lblMatheBHYT.Text = "Mã thẻ BHYT:";
            this.lblMatheBHYT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMatheBHYT.Click += new System.EventHandler(this.lblMatheBHYT_Click);
            // 
            // dtInsFromDate
            // 
            this.dtInsFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtInsFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtInsFromDate.DropDownCalendar.FirstMonth = new System.DateTime(2023, 5, 1, 0, 0, 0, 0);
            this.dtInsFromDate.DropDownCalendar.Name = "";
            this.dtInsFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtInsFromDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtInsFromDate.Location = new System.Drawing.Point(642, 76);
            this.dtInsFromDate.Name = "dtInsFromDate";
            this.dtInsFromDate.ShowUpDown = true;
            this.dtInsFromDate.Size = new System.Drawing.Size(123, 22);
            this.dtInsFromDate.TabIndex = 5;
            this.dtInsFromDate.Value = new System.DateTime(2013, 9, 23, 0, 0, 0, 0);
            this.dtInsFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // txtTenBenhChinh
            // 
            this.txtTenBenhChinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenBenhChinh.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenBenhChinh.Location = new System.Drawing.Point(270, 124);
            this.txtTenBenhChinh.Name = "txtTenBenhChinh";
            this.txtTenBenhChinh.ReadOnly = true;
            this.txtTenBenhChinh.Size = new System.Drawing.Size(710, 21);
            this.txtTenBenhChinh.TabIndex = 655;
            this.txtTenBenhChinh.TabStop = false;
            this.txtTenBenhChinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtChandoan
            // 
            this.txtChandoan._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtChandoan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChandoan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChandoan.AddValues = true;
            this.txtChandoan.AllowMultiline = false;
            this.txtChandoan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChandoan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtChandoan.AutoCompleteList")));
            this.txtChandoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChandoan.buildShortcut = false;
            this.txtChandoan.CaseSensitive = false;
            this.txtChandoan.cmdDropDown = null;
            this.txtChandoan.CompareNoID = true;
            this.txtChandoan.DefaultCode = "-1";
            this.txtChandoan.DefaultID = "-1";
            this.txtChandoan.Drug_ID = null;
            this.txtChandoan.ExtraWidth = 0;
            this.txtChandoan.FillValueAfterSelect = false;
            this.txtChandoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChandoan.LOAI_DANHMUC = "CHANDOANRAVIEN";
            this.txtChandoan.Location = new System.Drawing.Point(153, 101);
            this.txtChandoan.MaxHeight = 200;
            this.txtChandoan.MaxLength = 1000;
            this.txtChandoan.MinTypedCharacters = 2;
            this.txtChandoan.MyCode = "-1";
            this.txtChandoan.MyID = "-1";
            this.txtChandoan.Name = "txtChandoan";
            this.txtChandoan.RaiseEvent = false;
            this.txtChandoan.RaiseEventEnter = false;
            this.txtChandoan.RaiseEventEnterWhenEmpty = false;
            this.txtChandoan.SelectedIndex = -1;
            this.txtChandoan.ShowCodeWithValue = false;
            this.txtChandoan.Size = new System.Drawing.Size(827, 21);
            this.txtChandoan.splitChar = '@';
            this.txtChandoan.splitCharIDAndCode = '#';
            this.txtChandoan.TabIndex = 10;
            this.txtChandoan.TakeCode = false;
            this.txtChandoan.txtMyCode = null;
            this.txtChandoan.txtMyCode_Edit = null;
            this.txtChandoan.txtMyID = null;
            this.txtChandoan.txtMyID_Edit = null;
            this.txtChandoan.txtMyName = null;
            this.txtChandoan.txtMyName_Edit = null;
            this.txtChandoan.txtNext = null;
            this.txtChandoan.txtNext1 = null;
            // 
            // autoLydotuvong
            // 
            this.autoLydotuvong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoLydotuvong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotuvong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLydotuvong.AddValues = true;
            this.autoLydotuvong.AllowMultiline = false;
            this.autoLydotuvong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLydotuvong.AutoCompleteList")));
            this.autoLydotuvong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLydotuvong.buildShortcut = false;
            this.autoLydotuvong.CaseSensitive = false;
            this.autoLydotuvong.cmdDropDown = null;
            this.autoLydotuvong.CompareNoID = true;
            this.autoLydotuvong.DefaultCode = "-1";
            this.autoLydotuvong.DefaultID = "-1";
            this.autoLydotuvong.Drug_ID = null;
            this.autoLydotuvong.ExtraWidth = 0;
            this.autoLydotuvong.FillValueAfterSelect = false;
            this.autoLydotuvong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotuvong.LOAI_DANHMUC = "LYDOTUVONG";
            this.autoLydotuvong.Location = new System.Drawing.Point(460, 399);
            this.autoLydotuvong.MaxHeight = -1;
            this.autoLydotuvong.MinTypedCharacters = 2;
            this.autoLydotuvong.MyCode = "-1";
            this.autoLydotuvong.MyID = "-1";
            this.autoLydotuvong.Name = "autoLydotuvong";
            this.autoLydotuvong.RaiseEvent = false;
            this.autoLydotuvong.RaiseEventEnter = false;
            this.autoLydotuvong.RaiseEventEnterWhenEmpty = false;
            this.autoLydotuvong.SelectedIndex = -1;
            this.autoLydotuvong.ShowCodeWithValue = false;
            this.autoLydotuvong.Size = new System.Drawing.Size(249, 21);
            this.autoLydotuvong.splitChar = '@';
            this.autoLydotuvong.splitCharIDAndCode = '#';
            this.autoLydotuvong.TabIndex = 50;
            this.autoLydotuvong.TabStop = false;
            this.autoLydotuvong.TakeCode = false;
            this.autoLydotuvong.txtMyCode = null;
            this.autoLydotuvong.txtMyCode_Edit = null;
            this.autoLydotuvong.txtMyID = null;
            this.autoLydotuvong.txtMyID_Edit = null;
            this.autoLydotuvong.txtMyName = null;
            this.autoLydotuvong.txtMyName_Edit = null;
            this.autoLydotuvong.txtNext = null;
            this.autoLydotuvong.txtNext1 = null;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(313, 400);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 653;
            this.label5.Text = "Lý do tử vong :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBenhgiaiphau
            // 
            this.txtBenhgiaiphau._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBenhgiaiphau._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhgiaiphau._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhgiaiphau.AddValues = true;
            this.txtBenhgiaiphau.AllowMultiline = false;
            this.txtBenhgiaiphau.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhgiaiphau.AutoCompleteList")));
            this.txtBenhgiaiphau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhgiaiphau.buildShortcut = false;
            this.txtBenhgiaiphau.CaseSensitive = false;
            this.txtBenhgiaiphau.cmdDropDown = null;
            this.txtBenhgiaiphau.CompareNoID = true;
            this.txtBenhgiaiphau.DefaultCode = "-1";
            this.txtBenhgiaiphau.DefaultID = "-1";
            this.txtBenhgiaiphau.Drug_ID = null;
            this.txtBenhgiaiphau.ExtraWidth = 0;
            this.txtBenhgiaiphau.FillValueAfterSelect = false;
            this.txtBenhgiaiphau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhgiaiphau.LOAI_DANHMUC = "CHANDOANGIAIPHAU";
            this.txtBenhgiaiphau.Location = new System.Drawing.Point(150, 347);
            this.txtBenhgiaiphau.MaxHeight = 272;
            this.txtBenhgiaiphau.MinTypedCharacters = 2;
            this.txtBenhgiaiphau.MyCode = "-1";
            this.txtBenhgiaiphau.MyID = "-1";
            this.txtBenhgiaiphau.Name = "txtBenhgiaiphau";
            this.txtBenhgiaiphau.RaiseEvent = false;
            this.txtBenhgiaiphau.RaiseEventEnter = false;
            this.txtBenhgiaiphau.RaiseEventEnterWhenEmpty = false;
            this.txtBenhgiaiphau.SelectedIndex = -1;
            this.txtBenhgiaiphau.ShowCodeWithValue = false;
            this.txtBenhgiaiphau.Size = new System.Drawing.Size(273, 21);
            this.txtBenhgiaiphau.splitChar = '@';
            this.txtBenhgiaiphau.splitCharIDAndCode = '#';
            this.txtBenhgiaiphau.TabIndex = 40;
            this.txtBenhgiaiphau.TakeCode = false;
            this.txtBenhgiaiphau.txtMyCode = null;
            this.txtBenhgiaiphau.txtMyCode_Edit = null;
            this.txtBenhgiaiphau.txtMyID = null;
            this.txtBenhgiaiphau.txtMyID_Edit = null;
            this.txtBenhgiaiphau.txtMyName = null;
            this.txtBenhgiaiphau.txtMyName_Edit = null;
            this.txtBenhgiaiphau.txtNext = null;
            this.txtBenhgiaiphau.txtNext1 = null;
            // 
            // chkHentaikham
            // 
            this.chkHentaikham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHentaikham.Location = new System.Drawing.Point(307, 374);
            this.chkHentaikham.Name = "chkHentaikham";
            this.chkHentaikham.Size = new System.Drawing.Size(151, 23);
            this.chkHentaikham.TabIndex = 44;
            this.chkHentaikham.TabStop = false;
            this.chkHentaikham.Text = "Số ngày hẹn tái khám:";
            this.chkHentaikham.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.chkHentaikham.CheckedChanged += new System.EventHandler(this.chkHentaikham_CheckedChanged);
            // 
            // txtSoNgayHen
            // 
            this.txtSoNgayHen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoNgayHen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoNgayHen.Location = new System.Drawing.Point(715, 375);
            this.txtSoNgayHen.Masked = MaskedTextBox.Mask.Digit;
            this.txtSoNgayHen.Name = "txtSoNgayHen";
            this.txtSoNgayHen.Size = new System.Drawing.Size(47, 21);
            this.txtSoNgayHen.TabIndex = 47;
            this.txtSoNgayHen.TabStop = false;
            this.txtSoNgayHen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSoNgayHen.Visible = false;
            // 
            // dtpNgayHen
            // 
            this.dtpNgayHen.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayHen.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayHen.DropDownCalendar.Name = "";
            this.dtpNgayHen.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayHen.Enabled = false;
            this.dtpNgayHen.Font = new System.Drawing.Font("Arial", 9F);
            this.dtpNgayHen.Location = new System.Drawing.Point(593, 375);
            this.dtpNgayHen.Name = "dtpNgayHen";
            this.dtpNgayHen.ShowUpDown = true;
            this.dtpNgayHen.Size = new System.Drawing.Size(116, 21);
            this.dtpNgayHen.TabIndex = 46;
            this.dtpNgayHen.TabStop = false;
            this.dtpNgayHen.Value = new System.DateTime(2013, 8, 10, 0, 0, 0, 0);
            this.dtpNgayHen.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayHen.ValueChanged += new System.EventHandler(this.dtpNgayHen_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(525, 375);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 21);
            this.label4.TabIndex = 649;
            this.label4.Text = "Ngày hẹn:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSongayhentaikham
            // 
            this.txtSongayhentaikham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSongayhentaikham.Enabled = false;
            this.txtSongayhentaikham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSongayhentaikham.Location = new System.Drawing.Point(460, 375);
            this.txtSongayhentaikham.Masked = MaskedTextBox.Mask.Digit;
            this.txtSongayhentaikham.Name = "txtSongayhentaikham";
            this.txtSongayhentaikham.Size = new System.Drawing.Size(58, 21);
            this.txtSongayhentaikham.TabIndex = 45;
            this.txtSongayhentaikham.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBenhchinh
            // 
            this.txtBenhchinh._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBenhchinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhchinh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhchinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhchinh.AutoCompleteList")));
            this.txtBenhchinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhchinh.buildShortcut = false;
            this.txtBenhchinh.CaseSensitive = false;
            this.txtBenhchinh.CompareNoID = true;
            this.txtBenhchinh.DefaultCode = "-1";
            this.txtBenhchinh.DefaultID = "-1";
            this.txtBenhchinh.DisplayType = 1;
            this.txtBenhchinh.Drug_ID = null;
            this.txtBenhchinh.ExtraWidth = 500;
            this.txtBenhchinh.FillValueAfterSelect = false;
            this.txtBenhchinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhchinh.Location = new System.Drawing.Point(153, 124);
            this.txtBenhchinh.MaxHeight = 289;
            this.txtBenhchinh.MinTypedCharacters = 2;
            this.txtBenhchinh.MyCode = "-1";
            this.txtBenhchinh.MyID = "-1";
            this.txtBenhchinh.MyText = "";
            this.txtBenhchinh.MyTextOnly = "";
            this.txtBenhchinh.Name = "txtBenhchinh";
            this.txtBenhchinh.RaiseEvent = true;
            this.txtBenhchinh.RaiseEventEnter = true;
            this.txtBenhchinh.RaiseEventEnterWhenEmpty = false;
            this.txtBenhchinh.SelectedIndex = -1;
            this.txtBenhchinh.Size = new System.Drawing.Size(116, 21);
            this.txtBenhchinh.splitChar = '@';
            this.txtBenhchinh.splitCharIDAndCode = '#';
            this.txtBenhchinh.TabIndex = 13;
            this.txtBenhchinh.TakeCode = true;
            this.txtBenhchinh.txtMyCode = null;
            this.txtBenhchinh.txtMyCode_Edit = null;
            this.txtBenhchinh.txtMyID = null;
            this.txtBenhchinh.txtMyID_Edit = null;
            this.txtBenhchinh.txtMyName = null;
            this.txtBenhchinh.txtMyName_Edit = null;
            this.txtBenhchinh.txtNext = null;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(45, 124);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 20);
            this.label17.TabIndex = 645;
            this.label17.Text = "Bệnh chính";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTuvong
            // 
            this.chkTuvong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTuvong.Location = new System.Drawing.Point(46, 401);
            this.chkTuvong.Name = "chkTuvong";
            this.chkTuvong.Size = new System.Drawing.Size(97, 23);
            this.chkTuvong.TabIndex = 48;
            this.chkTuvong.TabStop = false;
            this.chkTuvong.Text = "Ngày tử vong";
            this.chkTuvong.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.chkTuvong.CheckedChanged += new System.EventHandler(this.chkTuvong_CheckedChanged);
            // 
            // dtpNgaytuvong
            // 
            this.dtpNgaytuvong.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtpNgaytuvong.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaytuvong.DropDownCalendar.Name = "";
            this.dtpNgaytuvong.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgaytuvong.Enabled = false;
            this.dtpNgaytuvong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaytuvong.Location = new System.Drawing.Point(149, 401);
            this.dtpNgaytuvong.Name = "dtpNgaytuvong";
            this.dtpNgaytuvong.ShowUpDown = true;
            this.dtpNgaytuvong.Size = new System.Drawing.Size(148, 21);
            this.dtpNgaytuvong.TabIndex = 49;
            this.dtpNgaytuvong.TabStop = false;
            this.dtpNgaytuvong.Value = new System.DateTime(2015, 5, 26, 0, 0, 0, 0);
            this.dtpNgaytuvong.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // txtsotuanthai
            // 
            this.txtsotuanthai.Location = new System.Drawing.Point(152, 230);
            this.txtsotuanthai.Name = "txtsotuanthai";
            this.txtsotuanthai.Size = new System.Drawing.Size(274, 21);
            this.txtsotuanthai.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 641;
            this.label2.Text = "Số tuần tuổi thai";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 639;
            this.label1.Text = "Chẩn đoán";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBsChidinh
            // 
            this.txtBsChidinh._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBsChidinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsChidinh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBsChidinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBsChidinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBsChidinh.AutoCompleteList")));
            this.txtBsChidinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBsChidinh.buildShortcut = false;
            this.txtBsChidinh.CaseSensitive = false;
            this.txtBsChidinh.CompareNoID = true;
            this.txtBsChidinh.DefaultCode = "-1";
            this.txtBsChidinh.DefaultID = "-1";
            this.txtBsChidinh.DisplayType = 0;
            this.txtBsChidinh.Drug_ID = null;
            this.txtBsChidinh.ExtraWidth = 0;
            this.txtBsChidinh.FillValueAfterSelect = false;
            this.txtBsChidinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsChidinh.Location = new System.Drawing.Point(951, 372);
            this.txtBsChidinh.MaxHeight = 289;
            this.txtBsChidinh.MinTypedCharacters = 2;
            this.txtBsChidinh.MyCode = "-1";
            this.txtBsChidinh.MyID = "-1";
            this.txtBsChidinh.MyText = "";
            this.txtBsChidinh.MyTextOnly = "";
            this.txtBsChidinh.Name = "txtBsChidinh";
            this.txtBsChidinh.RaiseEvent = true;
            this.txtBsChidinh.RaiseEventEnter = true;
            this.txtBsChidinh.RaiseEventEnterWhenEmpty = true;
            this.txtBsChidinh.SelectedIndex = -1;
            this.txtBsChidinh.Size = new System.Drawing.Size(8, 21);
            this.txtBsChidinh.splitChar = '@';
            this.txtBsChidinh.splitCharIDAndCode = '#';
            this.txtBsChidinh.TabIndex = 35;
            this.txtBsChidinh.TakeCode = false;
            this.txtBsChidinh.txtMyCode = null;
            this.txtBsChidinh.txtMyCode_Edit = null;
            this.txtBsChidinh.txtMyID = null;
            this.txtBsChidinh.txtMyID_Edit = null;
            this.txtBsChidinh.txtMyName = null;
            this.txtBsChidinh.txtMyName_Edit = null;
            this.txtBsChidinh.txtNext = null;
            this.txtBsChidinh.Visible = false;
            // 
            // txtTruongkhoa
            // 
            this.txtTruongkhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTruongkhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruongkhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTruongkhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTruongkhoa.AutoCompleteList")));
            this.txtTruongkhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTruongkhoa.buildShortcut = false;
            this.txtTruongkhoa.CaseSensitive = false;
            this.txtTruongkhoa.CompareNoID = true;
            this.txtTruongkhoa.DefaultCode = "-1";
            this.txtTruongkhoa.DefaultID = "-1";
            this.txtTruongkhoa.DisplayType = 0;
            this.txtTruongkhoa.Drug_ID = null;
            this.txtTruongkhoa.ExtraWidth = 0;
            this.txtTruongkhoa.FillValueAfterSelect = false;
            this.txtTruongkhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruongkhoa.Location = new System.Drawing.Point(153, 78);
            this.txtTruongkhoa.MaxHeight = 289;
            this.txtTruongkhoa.MinTypedCharacters = 2;
            this.txtTruongkhoa.MyCode = "-1";
            this.txtTruongkhoa.MyID = "-1";
            this.txtTruongkhoa.MyText = "";
            this.txtTruongkhoa.MyTextOnly = "";
            this.txtTruongkhoa.Name = "txtTruongkhoa";
            this.txtTruongkhoa.RaiseEvent = false;
            this.txtTruongkhoa.RaiseEventEnter = false;
            this.txtTruongkhoa.RaiseEventEnterWhenEmpty = false;
            this.txtTruongkhoa.SelectedIndex = -1;
            this.txtTruongkhoa.Size = new System.Drawing.Size(397, 21);
            this.txtTruongkhoa.splitChar = '@';
            this.txtTruongkhoa.splitCharIDAndCode = '#';
            this.txtTruongkhoa.TabIndex = 5;
            this.txtTruongkhoa.TakeCode = false;
            this.txtTruongkhoa.txtMyCode = null;
            this.txtTruongkhoa.txtMyCode_Edit = null;
            this.txtTruongkhoa.txtMyID = null;
            this.txtTruongkhoa.txtMyID_Edit = null;
            this.txtTruongkhoa.txtMyName = null;
            this.txtTruongkhoa.txtMyName_Edit = null;
            this.txtTruongkhoa.txtNext = null;
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("Arial", 9F);
            this.label48.ForeColor = System.Drawing.Color.Red;
            this.label48.Location = new System.Drawing.Point(3, 78);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(143, 21);
            this.label48.TabIndex = 637;
            this.label48.Text = "Trưởng khoa điều trị:";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grd_ICD
            // 
            this.grd_ICD.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_ICD.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grd_ICD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grd_ICD_DesignTimeLayout.LayoutString = resources.GetString("grd_ICD_DesignTimeLayout.LayoutString");
            this.grd_ICD.DesignTimeLayout = grd_ICD_DesignTimeLayout;
            this.grd_ICD.Font = new System.Drawing.Font("Arial", 9F);
            this.grd_ICD.GroupByBoxVisible = false;
            this.grd_ICD.Location = new System.Drawing.Point(435, 150);
            this.grd_ICD.Name = "grd_ICD";
            this.grd_ICD.Size = new System.Drawing.Size(545, 128);
            this.grd_ICD.TabIndex = 633;
            this.grd_ICD.TableViewHorizontalScrollIncrement = 21;
            this.grd_ICD.TabStop = false;
            this.grd_ICD.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grd_ICD.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grd_ICD_FormattingRow);
            // 
            // txtSoRaVien
            // 
            this.txtSoRaVien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoRaVien.Location = new System.Drawing.Point(153, 53);
            this.txtSoRaVien.MaxLength = 50;
            this.txtSoRaVien.Name = "txtSoRaVien";
            this.txtSoRaVien.Size = new System.Drawing.Size(116, 21);
            this.txtSoRaVien.TabIndex = 3;
            this.txtSoRaVien.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSoRaVien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(290, 7);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(102, 20);
            this.label31.TabIndex = 630;
            this.label31.Text = "Ngày nhập viện";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgaynhapvien
            // 
            this.dtpNgaynhapvien.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtpNgaynhapvien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaynhapvien.DropDownCalendar.Name = "";
            this.dtpNgaynhapvien.DropDownCalendar.Visible = false;
            this.dtpNgaynhapvien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgaynhapvien.Enabled = false;
            this.dtpNgaynhapvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynhapvien.Location = new System.Drawing.Point(398, 6);
            this.dtpNgaynhapvien.Name = "dtpNgaynhapvien";
            this.dtpNgaynhapvien.ShowUpDown = true;
            this.dtpNgaynhapvien.Size = new System.Drawing.Size(152, 21);
            this.dtpNgaynhapvien.TabIndex = 629;
            this.dtpNgaynhapvien.Value = new System.DateTime(2023, 6, 20, 0, 0, 0, 0);
            this.dtpNgaynhapvien.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(933, 370);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(10, 23);
            this.label30.TabIndex = 628;
            this.label30.Text = "BS chỉ định";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label30.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Arial", 9F);
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(86, 56);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(61, 15);
            this.label26.TabIndex = 623;
            this.label26.Text = "Số ra viện";
            // 
            // txtPhuongphapdieutri
            // 
            this.txtPhuongphapdieutri._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPhuongphapdieutri._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongphapdieutri._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhuongphapdieutri.AddValues = true;
            this.txtPhuongphapdieutri.AllowMultiline = false;
            this.txtPhuongphapdieutri.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhuongphapdieutri.AutoCompleteList")));
            this.txtPhuongphapdieutri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhuongphapdieutri.buildShortcut = false;
            this.txtPhuongphapdieutri.CaseSensitive = false;
            this.txtPhuongphapdieutri.cmdDropDown = null;
            this.txtPhuongphapdieutri.CompareNoID = true;
            this.txtPhuongphapdieutri.DefaultCode = "-1";
            this.txtPhuongphapdieutri.DefaultID = "-1";
            this.txtPhuongphapdieutri.Drug_ID = null;
            this.txtPhuongphapdieutri.ExtraWidth = 0;
            this.txtPhuongphapdieutri.FillValueAfterSelect = false;
            this.txtPhuongphapdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongphapdieutri.LOAI_DANHMUC = "HDT";
            this.txtPhuongphapdieutri.Location = new System.Drawing.Point(150, 285);
            this.txtPhuongphapdieutri.MaxHeight = -1;
            this.txtPhuongphapdieutri.MinTypedCharacters = 2;
            this.txtPhuongphapdieutri.Multiline = true;
            this.txtPhuongphapdieutri.MyCode = "-1";
            this.txtPhuongphapdieutri.MyID = "-1";
            this.txtPhuongphapdieutri.Name = "txtPhuongphapdieutri";
            this.txtPhuongphapdieutri.RaiseEvent = true;
            this.txtPhuongphapdieutri.RaiseEventEnter = true;
            this.txtPhuongphapdieutri.RaiseEventEnterWhenEmpty = false;
            this.txtPhuongphapdieutri.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPhuongphapdieutri.SelectedIndex = -1;
            this.txtPhuongphapdieutri.ShowCodeWithValue = false;
            this.txtPhuongphapdieutri.Size = new System.Drawing.Size(830, 56);
            this.txtPhuongphapdieutri.splitChar = '@';
            this.txtPhuongphapdieutri.splitCharIDAndCode = '#';
            this.txtPhuongphapdieutri.TabIndex = 26;
            this.txtPhuongphapdieutri.TakeCode = false;
            this.txtPhuongphapdieutri.txtMyCode = null;
            this.txtPhuongphapdieutri.txtMyCode_Edit = null;
            this.txtPhuongphapdieutri.txtMyID = null;
            this.txtPhuongphapdieutri.txtMyID_Edit = null;
            this.txtPhuongphapdieutri.txtMyName = null;
            this.txtPhuongphapdieutri.txtMyName_Edit = null;
            this.txtPhuongphapdieutri.txtNext = null;
            this.txtPhuongphapdieutri.txtNext1 = null;
            // 
            // txtTinhtrangravien
            // 
            this.txtTinhtrangravien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTinhtrangravien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangravien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTinhtrangravien.AddValues = true;
            this.txtTinhtrangravien.AllowMultiline = false;
            this.txtTinhtrangravien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhtrangravien.AutoCompleteList")));
            this.txtTinhtrangravien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTinhtrangravien.buildShortcut = false;
            this.txtTinhtrangravien.CaseSensitive = false;
            this.txtTinhtrangravien.cmdDropDown = null;
            this.txtTinhtrangravien.CompareNoID = true;
            this.txtTinhtrangravien.DefaultCode = "-1";
            this.txtTinhtrangravien.DefaultID = "-1";
            this.txtTinhtrangravien.Drug_ID = null;
            this.txtTinhtrangravien.ExtraWidth = 0;
            this.txtTinhtrangravien.FillValueAfterSelect = false;
            this.txtTinhtrangravien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangravien.LOAI_DANHMUC = "TINHTRANGRAVIEN";
            this.txtTinhtrangravien.Location = new System.Drawing.Point(153, 203);
            this.txtTinhtrangravien.MaxHeight = 272;
            this.txtTinhtrangravien.MinTypedCharacters = 2;
            this.txtTinhtrangravien.MyCode = "-1";
            this.txtTinhtrangravien.MyID = "-1";
            this.txtTinhtrangravien.Name = "txtTinhtrangravien";
            this.txtTinhtrangravien.RaiseEvent = true;
            this.txtTinhtrangravien.RaiseEventEnter = true;
            this.txtTinhtrangravien.RaiseEventEnterWhenEmpty = false;
            this.txtTinhtrangravien.SelectedIndex = -1;
            this.txtTinhtrangravien.ShowCodeWithValue = false;
            this.txtTinhtrangravien.Size = new System.Drawing.Size(273, 21);
            this.txtTinhtrangravien.splitChar = '@';
            this.txtTinhtrangravien.splitCharIDAndCode = '#';
            this.txtTinhtrangravien.TabIndex = 22;
            this.txtTinhtrangravien.TakeCode = false;
            this.txtTinhtrangravien.txtMyCode = null;
            this.txtTinhtrangravien.txtMyCode_Edit = null;
            this.txtTinhtrangravien.txtMyID = null;
            this.txtTinhtrangravien.txtMyID_Edit = null;
            this.txtTinhtrangravien.txtMyName = null;
            this.txtTinhtrangravien.txtMyName_Edit = null;
            this.txtTinhtrangravien.txtNext = null;
            this.txtTinhtrangravien.txtNext1 = null;
            // 
            // txtKqdieutri
            // 
            this.txtKqdieutri._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtKqdieutri._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKqdieutri._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKqdieutri.AddValues = true;
            this.txtKqdieutri.AllowMultiline = false;
            this.txtKqdieutri.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKqdieutri.AutoCompleteList")));
            this.txtKqdieutri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKqdieutri.buildShortcut = false;
            this.txtKqdieutri.CaseSensitive = false;
            this.txtKqdieutri.cmdDropDown = null;
            this.txtKqdieutri.CompareNoID = true;
            this.txtKqdieutri.DefaultCode = "-1";
            this.txtKqdieutri.DefaultID = "-1";
            this.txtKqdieutri.Drug_ID = null;
            this.txtKqdieutri.ExtraWidth = 0;
            this.txtKqdieutri.FillValueAfterSelect = false;
            this.txtKqdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKqdieutri.LOAI_DANHMUC = "KETQUADIEUTRINOITRU";
            this.txtKqdieutri.Location = new System.Drawing.Point(153, 176);
            this.txtKqdieutri.MaxHeight = 272;
            this.txtKqdieutri.MinTypedCharacters = 2;
            this.txtKqdieutri.MyCode = "-1";
            this.txtKqdieutri.MyID = "-1";
            this.txtKqdieutri.Name = "txtKqdieutri";
            this.txtKqdieutri.RaiseEvent = false;
            this.txtKqdieutri.RaiseEventEnter = false;
            this.txtKqdieutri.RaiseEventEnterWhenEmpty = false;
            this.txtKqdieutri.SelectedIndex = -1;
            this.txtKqdieutri.ShowCodeWithValue = false;
            this.txtKqdieutri.Size = new System.Drawing.Size(273, 21);
            this.txtKqdieutri.splitChar = '@';
            this.txtKqdieutri.splitCharIDAndCode = '#';
            this.txtKqdieutri.TabIndex = 20;
            this.txtKqdieutri.TakeCode = false;
            this.txtKqdieutri.txtMyCode = null;
            this.txtKqdieutri.txtMyCode_Edit = null;
            this.txtKqdieutri.txtMyID = null;
            this.txtKqdieutri.txtMyID_Edit = null;
            this.txtKqdieutri.txtMyName = null;
            this.txtKqdieutri.txtMyName_Edit = null;
            this.txtKqdieutri.txtNext = null;
            this.txtKqdieutri.txtNext1 = null;
            // 
            // txtBenhphu
            // 
            this.txtBenhphu._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBenhphu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhphu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhphu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhphu.AutoCompleteList")));
            this.txtBenhphu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhphu.buildShortcut = false;
            this.txtBenhphu.CaseSensitive = false;
            this.txtBenhphu.CompareNoID = true;
            this.txtBenhphu.DefaultCode = "-1";
            this.txtBenhphu.DefaultID = "-1";
            this.txtBenhphu.DisplayType = 1;
            this.txtBenhphu.Drug_ID = null;
            this.txtBenhphu.ExtraWidth = 0;
            this.txtBenhphu.FillValueAfterSelect = false;
            this.txtBenhphu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhphu.Location = new System.Drawing.Point(153, 150);
            this.txtBenhphu.MaxHeight = 289;
            this.txtBenhphu.MinTypedCharacters = 2;
            this.txtBenhphu.MyCode = "-1";
            this.txtBenhphu.MyID = "-1";
            this.txtBenhphu.MyText = "";
            this.txtBenhphu.MyTextOnly = "";
            this.txtBenhphu.Name = "txtBenhphu";
            this.txtBenhphu.RaiseEvent = true;
            this.txtBenhphu.RaiseEventEnter = true;
            this.txtBenhphu.RaiseEventEnterWhenEmpty = false;
            this.txtBenhphu.SelectedIndex = -1;
            this.txtBenhphu.Size = new System.Drawing.Size(273, 21);
            this.txtBenhphu.splitChar = '@';
            this.txtBenhphu.splitCharIDAndCode = '#';
            this.txtBenhphu.TabIndex = 14;
            this.txtBenhphu.TakeCode = false;
            this.txtBenhphu.txtMyCode = null;
            this.txtBenhphu.txtMyCode_Edit = null;
            this.txtBenhphu.txtMyID = null;
            this.txtBenhphu.txtMyID_Edit = null;
            this.txtBenhphu.txtMyName = null;
            this.txtBenhphu.txtMyName_Edit = null;
            this.txtBenhphu.txtNext = null;
            // 
            // txtBenhnguyennhan
            // 
            this.txtBenhnguyennhan._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBenhnguyennhan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhnguyennhan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhnguyennhan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBenhnguyennhan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhnguyennhan.AutoCompleteList")));
            this.txtBenhnguyennhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhnguyennhan.buildShortcut = false;
            this.txtBenhnguyennhan.CaseSensitive = false;
            this.txtBenhnguyennhan.CompareNoID = true;
            this.txtBenhnguyennhan.DefaultCode = "-1";
            this.txtBenhnguyennhan.DefaultID = "-1";
            this.txtBenhnguyennhan.DisplayType = 0;
            this.txtBenhnguyennhan.Drug_ID = null;
            this.txtBenhnguyennhan.ExtraWidth = 0;
            this.txtBenhnguyennhan.FillValueAfterSelect = false;
            this.txtBenhnguyennhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhnguyennhan.Location = new System.Drawing.Point(151, 257);
            this.txtBenhnguyennhan.MaxHeight = 289;
            this.txtBenhnguyennhan.MinTypedCharacters = 2;
            this.txtBenhnguyennhan.MyCode = "-1";
            this.txtBenhnguyennhan.MyID = "-1";
            this.txtBenhnguyennhan.MyText = "";
            this.txtBenhnguyennhan.MyTextOnly = "";
            this.txtBenhnguyennhan.Name = "txtBenhnguyennhan";
            this.txtBenhnguyennhan.RaiseEvent = true;
            this.txtBenhnguyennhan.RaiseEventEnter = true;
            this.txtBenhnguyennhan.RaiseEventEnterWhenEmpty = true;
            this.txtBenhnguyennhan.SelectedIndex = -1;
            this.txtBenhnguyennhan.Size = new System.Drawing.Size(275, 21);
            this.txtBenhnguyennhan.splitChar = '@';
            this.txtBenhnguyennhan.splitCharIDAndCode = '#';
            this.txtBenhnguyennhan.TabIndex = 25;
            this.txtBenhnguyennhan.TakeCode = false;
            this.txtBenhnguyennhan.txtMyCode = null;
            this.txtBenhnguyennhan.txtMyCode_Edit = null;
            this.txtBenhnguyennhan.txtMyID = null;
            this.txtBenhnguyennhan.txtMyID_Edit = null;
            this.txtBenhnguyennhan.txtMyName = null;
            this.txtBenhnguyennhan.txtMyName_Edit = null;
            this.txtBenhnguyennhan.txtNext = null;
            // 
            // txtBenhbienchung
            // 
            this.txtBenhbienchung._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBenhbienchung._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhbienchung._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBenhbienchung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBenhbienchung.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBenhbienchung.AutoCompleteList")));
            this.txtBenhbienchung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBenhbienchung.buildShortcut = false;
            this.txtBenhbienchung.CaseSensitive = false;
            this.txtBenhbienchung.CompareNoID = true;
            this.txtBenhbienchung.DefaultCode = "-1";
            this.txtBenhbienchung.DefaultID = "-1";
            this.txtBenhbienchung.DisplayType = 0;
            this.txtBenhbienchung.Drug_ID = null;
            this.txtBenhbienchung.ExtraWidth = 0;
            this.txtBenhbienchung.FillValueAfterSelect = false;
            this.txtBenhbienchung.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBenhbienchung.Location = new System.Drawing.Point(885, 447);
            this.txtBenhbienchung.MaxHeight = 289;
            this.txtBenhbienchung.MinTypedCharacters = 2;
            this.txtBenhbienchung.MyCode = "-1";
            this.txtBenhbienchung.MyID = "-1";
            this.txtBenhbienchung.MyText = "";
            this.txtBenhbienchung.MyTextOnly = "";
            this.txtBenhbienchung.Name = "txtBenhbienchung";
            this.txtBenhbienchung.RaiseEvent = true;
            this.txtBenhbienchung.RaiseEventEnter = true;
            this.txtBenhbienchung.RaiseEventEnterWhenEmpty = true;
            this.txtBenhbienchung.SelectedIndex = -1;
            this.txtBenhbienchung.Size = new System.Drawing.Size(26, 21);
            this.txtBenhbienchung.splitChar = '@';
            this.txtBenhbienchung.splitCharIDAndCode = '#';
            this.txtBenhbienchung.TabIndex = 11;
            this.txtBenhbienchung.TakeCode = false;
            this.txtBenhbienchung.txtMyCode = null;
            this.txtBenhbienchung.txtMyCode_Edit = null;
            this.txtBenhbienchung.txtMyID = null;
            this.txtBenhbienchung.txtMyID_Edit = null;
            this.txtBenhbienchung.txtMyName = null;
            this.txtBenhbienchung.txtMyName_Edit = null;
            this.txtBenhbienchung.txtNext = null;
            this.txtBenhbienchung.Visible = false;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(15, 285);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(132, 20);
            this.label27.TabIndex = 620;
            this.label27.Text = "Phương pháp điều trị";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDaCapGiayRaVien
            // 
            this.chkDaCapGiayRaVien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDaCapGiayRaVien.Location = new System.Drawing.Point(5, 375);
            this.chkDaCapGiayRaVien.Name = "chkDaCapGiayRaVien";
            this.chkDaCapGiayRaVien.Size = new System.Drawing.Size(138, 23);
            this.chkDaCapGiayRaVien.TabIndex = 42;
            this.chkDaCapGiayRaVien.TabStop = false;
            this.chkDaCapGiayRaVien.Text = "Ngày cấp giấy ra viện";
            this.chkDaCapGiayRaVien.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.chkDaCapGiayRaVien.CheckedChanged += new System.EventHandler(this.chkDaCapGiayRaVien_CheckedChanged);
            // 
            // txtTongSoNgayDtri
            // 
            this.txtTongSoNgayDtri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongSoNgayDtri.Location = new System.Drawing.Point(398, 53);
            this.txtTongSoNgayDtri.MaxLength = 3;
            this.txtTongSoNgayDtri.Name = "txtTongSoNgayDtri";
            this.txtTongSoNgayDtri.Numeric = true;
            this.txtTongSoNgayDtri.Size = new System.Drawing.Size(152, 21);
            this.txtTongSoNgayDtri.TabIndex = 4;
            this.txtTongSoNgayDtri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtTongSoNgayDtri.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // dtNGAY_CAP_GIAY_RVIEN
            // 
            this.dtNGAY_CAP_GIAY_RVIEN.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtNGAY_CAP_GIAY_RVIEN.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNGAY_CAP_GIAY_RVIEN.DropDownCalendar.Name = "";
            this.dtNGAY_CAP_GIAY_RVIEN.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNGAY_CAP_GIAY_RVIEN.Enabled = false;
            this.dtNGAY_CAP_GIAY_RVIEN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNGAY_CAP_GIAY_RVIEN.Location = new System.Drawing.Point(149, 376);
            this.dtNGAY_CAP_GIAY_RVIEN.Name = "dtNGAY_CAP_GIAY_RVIEN";
            this.dtNGAY_CAP_GIAY_RVIEN.ShowUpDown = true;
            this.dtNGAY_CAP_GIAY_RVIEN.Size = new System.Drawing.Size(148, 21);
            this.dtNGAY_CAP_GIAY_RVIEN.TabIndex = 43;
            this.dtNGAY_CAP_GIAY_RVIEN.TabStop = false;
            this.dtNGAY_CAP_GIAY_RVIEN.Value = new System.DateTime(2015, 5, 26, 0, 0, 0, 0);
            this.dtNGAY_CAP_GIAY_RVIEN.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // chkPhuHopChanDoanCLS
            // 
            this.chkPhuHopChanDoanCLS.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPhuHopChanDoanCLS.Location = new System.Drawing.Point(432, 347);
            this.chkPhuHopChanDoanCLS.Name = "chkPhuHopChanDoanCLS";
            this.chkPhuHopChanDoanCLS.Size = new System.Drawing.Size(205, 23);
            this.chkPhuHopChanDoanCLS.TabIndex = 41;
            this.chkPhuHopChanDoanCLS.TabStop = false;
            this.chkPhuHopChanDoanCLS.Text = "Phù hợp chẩn đoán lâm sàng?";
            this.chkPhuHopChanDoanCLS.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(8, 349);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(135, 20);
            this.label22.TabIndex = 618;
            this.label22.Text = "Chẩn đoán giải phẫu bệnh";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(47, 206);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 15);
            this.label12.TabIndex = 617;
            this.label12.Text = "Tình trạng ra viện";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(18, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 20);
            this.label13.TabIndex = 616;
            this.label13.Text = "Kết quả điều trị";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(18, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 20);
            this.label14.TabIndex = 615;
            this.label14.Text = "Bệnh phụ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 256);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(129, 20);
            this.label15.TabIndex = 614;
            this.label15.Text = "Nguyên nhân";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(863, 448);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 20);
            this.label16.TabIndex = 613;
            this.label16.Text = "Biến chứng";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(272, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 15);
            this.label18.TabIndex = 611;
            this.label18.Text = "Tổng số ngày điều trị";
            // 
            // txtPhutRaVien
            // 
            this.txtPhutRaVien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhutRaVien.Location = new System.Drawing.Point(885, 6);
            this.txtPhutRaVien.MaxLength = 2;
            this.txtPhutRaVien.Name = "txtPhutRaVien";
            this.txtPhutRaVien.Numeric = true;
            this.txtPhutRaVien.Size = new System.Drawing.Size(43, 21);
            this.txtPhutRaVien.TabIndex = 2;
            this.txtPhutRaVien.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPhutRaVien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(847, 6);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(32, 15);
            this.label19.TabIndex = 610;
            this.label19.Text = "Phút";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(773, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(27, 15);
            this.label23.TabIndex = 608;
            this.label23.Text = "Giờ";
            // 
            // txtGioRaVien
            // 
            this.txtGioRaVien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGioRaVien.Location = new System.Drawing.Point(805, 6);
            this.txtGioRaVien.MaxLength = 2;
            this.txtGioRaVien.Name = "txtGioRaVien";
            this.txtGioRaVien.Numeric = true;
            this.txtGioRaVien.Size = new System.Drawing.Size(36, 21);
            this.txtGioRaVien.TabIndex = 1;
            this.txtGioRaVien.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtGioRaVien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(547, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(89, 20);
            this.label24.TabIndex = 609;
            this.label24.Text = "Ngày ra viện";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayravien
            // 
            this.dtpNgayravien.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayravien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayravien.DropDownCalendar.Name = "";
            this.dtpNgayravien.DropDownCalendar.Visible = false;
            this.dtpNgayravien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayravien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayravien.IsNullDate = true;
            this.dtpNgayravien.Location = new System.Drawing.Point(642, 6);
            this.dtpNgayravien.Name = "dtpNgayravien";
            this.dtpNgayravien.ShowUpDown = true;
            this.dtpNgayravien.Size = new System.Drawing.Size(123, 21);
            this.dtpNgayravien.TabIndex = 0;
            this.dtpNgayravien.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayravien.ValueChanged += new System.EventHandler(this.dtpNgayravien_ValueChanged_1);
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Navy;
            this.lblMsg.Location = new System.Drawing.Point(0, 486);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(992, 23);
            this.lblMsg.TabIndex = 27;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(153, 6);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(116, 21);
            this.txtId.TabIndex = 10;
            this.txtId.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Id ra viện";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNguoivanchuyen);
            this.groupBox1.Controls.Add(this.txtNoichuyenden);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtKieuchuyenvien);
            this.groupBox1.Controls.Add(this.cmdGetBV);
            this.groupBox1.Controls.Add(this.chkChuyenvien);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.txtphuongtienvc);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(967, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(28, 10);
            this.groupBox1.TabIndex = 654;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chuyển viện";
            this.groupBox1.Visible = false;
            // 
            // txtNguoivanchuyen
            // 
            this.txtNguoivanchuyen._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNguoivanchuyen._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoivanchuyen._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoivanchuyen.AddValues = true;
            this.txtNguoivanchuyen.AllowMultiline = false;
            this.txtNguoivanchuyen.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoivanchuyen.AutoCompleteList")));
            this.txtNguoivanchuyen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoivanchuyen.buildShortcut = false;
            this.txtNguoivanchuyen.CaseSensitive = false;
            this.txtNguoivanchuyen.cmdDropDown = null;
            this.txtNguoivanchuyen.CompareNoID = true;
            this.txtNguoivanchuyen.DefaultCode = "-1";
            this.txtNguoivanchuyen.DefaultID = "-1";
            this.txtNguoivanchuyen.Drug_ID = null;
            this.txtNguoivanchuyen.ExtraWidth = 0;
            this.txtNguoivanchuyen.FillValueAfterSelect = false;
            this.txtNguoivanchuyen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoivanchuyen.LOAI_DANHMUC = "NGUOIHOTONGCHUYENVIEN";
            this.txtNguoivanchuyen.Location = new System.Drawing.Point(143, 43);
            this.txtNguoivanchuyen.MaxHeight = 150;
            this.txtNguoivanchuyen.MaxLength = 255;
            this.txtNguoivanchuyen.MinTypedCharacters = 2;
            this.txtNguoivanchuyen.MyCode = "-1";
            this.txtNguoivanchuyen.MyID = "-1";
            this.txtNguoivanchuyen.Name = "txtNguoivanchuyen";
            this.txtNguoivanchuyen.RaiseEvent = false;
            this.txtNguoivanchuyen.RaiseEventEnter = false;
            this.txtNguoivanchuyen.RaiseEventEnterWhenEmpty = false;
            this.txtNguoivanchuyen.SelectedIndex = -1;
            this.txtNguoivanchuyen.ShowCodeWithValue = false;
            this.txtNguoivanchuyen.Size = new System.Drawing.Size(273, 21);
            this.txtNguoivanchuyen.splitChar = '@';
            this.txtNguoivanchuyen.splitCharIDAndCode = '#';
            this.txtNguoivanchuyen.TabIndex = 34;
            this.txtNguoivanchuyen.TakeCode = false;
            this.txtNguoivanchuyen.txtMyCode = null;
            this.txtNguoivanchuyen.txtMyCode_Edit = null;
            this.txtNguoivanchuyen.txtMyID = null;
            this.txtNguoivanchuyen.txtMyID_Edit = null;
            this.txtNguoivanchuyen.txtMyName = null;
            this.txtNguoivanchuyen.txtMyName_Edit = null;
            this.txtNguoivanchuyen.txtNext = null;
            this.txtNguoivanchuyen.txtNext1 = null;
            // 
            // txtNoichuyenden
            // 
            this.txtNoichuyenden._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNoichuyenden._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoichuyenden._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNoichuyenden.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNoichuyenden.AutoCompleteList")));
            this.txtNoichuyenden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoichuyenden.buildShortcut = false;
            this.txtNoichuyenden.CaseSensitive = false;
            this.txtNoichuyenden.CompareNoID = true;
            this.txtNoichuyenden.DefaultCode = "-1";
            this.txtNoichuyenden.DefaultID = "-1";
            this.txtNoichuyenden.DisplayType = 1;
            this.txtNoichuyenden.Drug_ID = null;
            this.txtNoichuyenden.Enabled = false;
            this.txtNoichuyenden.ExtraWidth = 0;
            this.txtNoichuyenden.FillValueAfterSelect = false;
            this.txtNoichuyenden.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoichuyenden.Location = new System.Drawing.Point(143, 20);
            this.txtNoichuyenden.MaxHeight = 289;
            this.txtNoichuyenden.MinTypedCharacters = 2;
            this.txtNoichuyenden.MyCode = "-1";
            this.txtNoichuyenden.MyID = "-1";
            this.txtNoichuyenden.MyText = "";
            this.txtNoichuyenden.MyTextOnly = "";
            this.txtNoichuyenden.Name = "txtNoichuyenden";
            this.txtNoichuyenden.RaiseEvent = true;
            this.txtNoichuyenden.RaiseEventEnter = true;
            this.txtNoichuyenden.RaiseEventEnterWhenEmpty = true;
            this.txtNoichuyenden.SelectedIndex = -1;
            this.txtNoichuyenden.Size = new System.Drawing.Size(240, 21);
            this.txtNoichuyenden.splitChar = '@';
            this.txtNoichuyenden.splitCharIDAndCode = '#';
            this.txtNoichuyenden.TabIndex = 31;
            this.txtNoichuyenden.TakeCode = false;
            this.txtNoichuyenden.txtMyCode = null;
            this.txtNoichuyenden.txtMyCode_Edit = null;
            this.txtNoichuyenden.txtMyID = null;
            this.txtNoichuyenden.txtMyID_Edit = null;
            this.txtNoichuyenden.txtMyName = null;
            this.txtNoichuyenden.txtMyName_Edit = null;
            this.txtNoichuyenden.txtNext = null;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(496, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 20);
            this.label11.TabIndex = 619;
            this.label11.Text = "Kiểu chuyển viện";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKieuchuyenvien
            // 
            this.txtKieuchuyenvien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtKieuchuyenvien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuchuyenvien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKieuchuyenvien.AddValues = true;
            this.txtKieuchuyenvien.AllowMultiline = false;
            this.txtKieuchuyenvien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKieuchuyenvien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKieuchuyenvien.AutoCompleteList")));
            this.txtKieuchuyenvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKieuchuyenvien.buildShortcut = false;
            this.txtKieuchuyenvien.CaseSensitive = false;
            this.txtKieuchuyenvien.cmdDropDown = null;
            this.txtKieuchuyenvien.CompareNoID = true;
            this.txtKieuchuyenvien.DefaultCode = "-1";
            this.txtKieuchuyenvien.DefaultID = "-1";
            this.txtKieuchuyenvien.Drug_ID = null;
            this.txtKieuchuyenvien.Enabled = false;
            this.txtKieuchuyenvien.ExtraWidth = 0;
            this.txtKieuchuyenvien.FillValueAfterSelect = false;
            this.txtKieuchuyenvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuchuyenvien.LOAI_DANHMUC = "KIEUCHUYENVIEN";
            this.txtKieuchuyenvien.Location = new System.Drawing.Point(632, 43);
            this.txtKieuchuyenvien.MaxHeight = -1;
            this.txtKieuchuyenvien.MinTypedCharacters = 2;
            this.txtKieuchuyenvien.MyCode = "-1";
            this.txtKieuchuyenvien.MyID = "-1";
            this.txtKieuchuyenvien.Name = "txtKieuchuyenvien";
            this.txtKieuchuyenvien.RaiseEvent = false;
            this.txtKieuchuyenvien.RaiseEventEnter = false;
            this.txtKieuchuyenvien.RaiseEventEnterWhenEmpty = false;
            this.txtKieuchuyenvien.SelectedIndex = -1;
            this.txtKieuchuyenvien.ShowCodeWithValue = false;
            this.txtKieuchuyenvien.Size = new System.Drawing.Size(0, 21);
            this.txtKieuchuyenvien.splitChar = '@';
            this.txtKieuchuyenvien.splitCharIDAndCode = '#';
            this.txtKieuchuyenvien.TabIndex = 35;
            this.txtKieuchuyenvien.TakeCode = false;
            this.txtKieuchuyenvien.txtMyCode = null;
            this.txtKieuchuyenvien.txtMyCode_Edit = null;
            this.txtKieuchuyenvien.txtMyID = null;
            this.txtKieuchuyenvien.txtMyID_Edit = null;
            this.txtKieuchuyenvien.txtMyName = null;
            this.txtKieuchuyenvien.txtMyName_Edit = null;
            this.txtKieuchuyenvien.txtNext = null;
            this.txtKieuchuyenvien.txtNext1 = null;
            // 
            // cmdGetBV
            // 
            this.cmdGetBV.Enabled = false;
            this.cmdGetBV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetBV.Image = ((System.Drawing.Image)(resources.GetObject("cmdGetBV.Image")));
            this.cmdGetBV.Location = new System.Drawing.Point(387, 20);
            this.cmdGetBV.Name = "cmdGetBV";
            this.cmdGetBV.Size = new System.Drawing.Size(25, 21);
            this.cmdGetBV.TabIndex = 32;
            this.cmdGetBV.TabStop = false;
            this.cmdGetBV.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // chkChuyenvien
            // 
            this.chkChuyenvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkChuyenvien.Location = new System.Drawing.Point(23, 19);
            this.chkChuyenvien.Name = "chkChuyenvien";
            this.chkChuyenvien.Size = new System.Drawing.Size(115, 23);
            this.chkChuyenvien.TabIndex = 30;
            this.chkChuyenvien.TabStop = false;
            this.chkChuyenvien.Text = "Bệnh viện chuyển";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Navy;
            this.label29.Location = new System.Drawing.Point(486, 18);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(141, 23);
            this.label29.TabIndex = 624;
            this.label29.Text = "Phương tiện vận chuyển";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Navy;
            this.label28.Location = new System.Drawing.Point(21, 44);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(115, 21);
            this.label28.TabIndex = 626;
            this.label28.Text = "Người chuyển";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtphuongtienvc
            // 
            this.txtphuongtienvc._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtphuongtienvc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphuongtienvc._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtphuongtienvc.AddValues = true;
            this.txtphuongtienvc.AllowMultiline = false;
            this.txtphuongtienvc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtphuongtienvc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtphuongtienvc.AutoCompleteList")));
            this.txtphuongtienvc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtphuongtienvc.buildShortcut = false;
            this.txtphuongtienvc.CaseSensitive = false;
            this.txtphuongtienvc.cmdDropDown = null;
            this.txtphuongtienvc.CompareNoID = true;
            this.txtphuongtienvc.DefaultCode = "-1";
            this.txtphuongtienvc.DefaultID = "-1";
            this.txtphuongtienvc.Drug_ID = null;
            this.txtphuongtienvc.Enabled = false;
            this.txtphuongtienvc.ExtraWidth = 0;
            this.txtphuongtienvc.FillValueAfterSelect = false;
            this.txtphuongtienvc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphuongtienvc.LOAI_DANHMUC = "PHUONGTIENVANCHUYEN";
            this.txtphuongtienvc.Location = new System.Drawing.Point(632, 19);
            this.txtphuongtienvc.MaxHeight = -1;
            this.txtphuongtienvc.MaxLength = 255;
            this.txtphuongtienvc.MinTypedCharacters = 2;
            this.txtphuongtienvc.MyCode = "-1";
            this.txtphuongtienvc.MyID = "-1";
            this.txtphuongtienvc.Name = "txtphuongtienvc";
            this.txtphuongtienvc.RaiseEvent = false;
            this.txtphuongtienvc.RaiseEventEnter = false;
            this.txtphuongtienvc.RaiseEventEnterWhenEmpty = false;
            this.txtphuongtienvc.SelectedIndex = -1;
            this.txtphuongtienvc.ShowCodeWithValue = false;
            this.txtphuongtienvc.Size = new System.Drawing.Size(0, 21);
            this.txtphuongtienvc.splitChar = '@';
            this.txtphuongtienvc.splitCharIDAndCode = '#';
            this.txtphuongtienvc.TabIndex = 33;
            this.txtphuongtienvc.TakeCode = false;
            this.txtphuongtienvc.txtMyCode = null;
            this.txtphuongtienvc.txtMyCode_Edit = null;
            this.txtphuongtienvc.txtMyID = null;
            this.txtphuongtienvc.txtMyID_Edit = null;
            this.txtphuongtienvc.txtMyName = null;
            this.txtphuongtienvc.txtMyName_Edit = null;
            this.txtphuongtienvc.txtNext = null;
            this.txtphuongtienvc.txtNext1 = null;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(11, 250);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(100, 20);
            this.label25.TabIndex = 626;
            this.label25.Text = "Ý kiến/đề xuất";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtYkien
            // 
            this.txtYkien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYkien.Location = new System.Drawing.Point(113, 250);
            this.txtYkien.Multiline = true;
            this.txtYkien.Name = "txtYkien";
            this.txtYkien.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtYkien.Size = new System.Drawing.Size(870, 208);
            this.txtYkien.TabIndex = 71;
            this.txtYkien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(-3, 37);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 20);
            this.label21.TabIndex = 625;
            this.label21.Text = "Lời dặn bác sĩ";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // grdPresDetail
            // 
            this.grdPresDetail.AlternatingColors = true;
            grdPresDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPresDetail_DesignTimeLayout.LayoutString");
            this.grdPresDetail.DesignTimeLayout = grdPresDetail_DesignTimeLayout;
            this.grdPresDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPresDetail.DynamicFiltering = true;
            this.grdPresDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPresDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPresDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPresDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPresDetail.GroupByBoxVisible = false;
            this.grdPresDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdPresDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPresDetail.Location = new System.Drawing.Point(0, 0);
            this.grdPresDetail.Name = "grdPresDetail";
            this.grdPresDetail.RecordNavigator = true;
            this.grdPresDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdPresDetail.Size = new System.Drawing.Size(992, 463);
            this.grdPresDetail.TabIndex = 4;
            this.grdPresDetail.TabStop = false;
            this.grdPresDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdPresDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPresDetail.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdPresDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPresDetail.UseGroupRowSelector = true;
            this.grdPresDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // pnlDonthuoc
            // 
            this.pnlDonthuoc.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDonthuoc.Controls.Add(this.cmdWords);
            this.pnlDonthuoc.Controls.Add(this.cmdCreateNewPresTuTuc);
            this.pnlDonthuoc.Controls.Add(this.cmdPrintPres);
            this.pnlDonthuoc.Controls.Add(this.cmdDeletePres);
            this.pnlDonthuoc.Controls.Add(this.cmdUpdatePres);
            this.pnlDonthuoc.Controls.Add(this.cmdCreateNewPres);
            this.pnlDonthuoc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDonthuoc.Location = new System.Drawing.Point(0, 463);
            this.pnlDonthuoc.Name = "pnlDonthuoc";
            this.pnlDonthuoc.Size = new System.Drawing.Size(992, 46);
            this.pnlDonthuoc.TabIndex = 3;
            // 
            // cmdWords
            // 
            this.cmdWords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdWords.Image = ((System.Drawing.Image)(resources.GetObject("cmdWords.Image")));
            this.cmdWords.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdWords.Location = new System.Drawing.Point(4, 6);
            this.cmdWords.Name = "cmdWords";
            this.cmdWords.Size = new System.Drawing.Size(58, 33);
            this.cmdWords.TabIndex = 83;
            this.cmdWords.TabStop = false;
            this.cmdWords.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdCreateNewPresTuTuc
            // 
            this.cmdCreateNewPresTuTuc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCreateNewPresTuTuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreateNewPresTuTuc.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreateNewPresTuTuc.Image")));
            this.cmdCreateNewPresTuTuc.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdCreateNewPresTuTuc.Location = new System.Drawing.Point(1001, 3);
            this.cmdCreateNewPresTuTuc.Name = "cmdCreateNewPresTuTuc";
            this.cmdCreateNewPresTuTuc.Size = new System.Drawing.Size(120, 33);
            this.cmdCreateNewPresTuTuc.TabIndex = 422;
            this.cmdCreateNewPresTuTuc.Text = "Thuốc tự túc";
            this.cmdCreateNewPresTuTuc.ToolTipText = "Thêm thông tin đơn thuốc tự túc(bệnh nhân sẽ lấy thuốc của dịch vụ)";
            this.cmdCreateNewPresTuTuc.Visible = false;
            this.cmdCreateNewPresTuTuc.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdPrintPres
            // 
            this.cmdPrintPres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrintPres.Enabled = false;
            this.cmdPrintPres.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintPres.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrintPres.Image")));
            this.cmdPrintPres.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrintPres.Location = new System.Drawing.Point(68, 6);
            this.cmdPrintPres.Name = "cmdPrintPres";
            this.cmdPrintPres.Size = new System.Drawing.Size(60, 33);
            this.cmdPrintPres.TabIndex = 84;
            this.cmdPrintPres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdDeletePres
            // 
            this.cmdDeletePres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeletePres.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeletePres.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeletePres.Image")));
            this.cmdDeletePres.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdDeletePres.Location = new System.Drawing.Point(867, 6);
            this.cmdDeletePres.Name = "cmdDeletePres";
            this.cmdDeletePres.Size = new System.Drawing.Size(120, 33);
            this.cmdDeletePres.TabIndex = 82;
            this.cmdDeletePres.Text = "Xóa";
            this.cmdDeletePres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdDeletePres.Click += new System.EventHandler(this.cmdDeletePres_Click);
            // 
            // cmdUpdatePres
            // 
            this.cmdUpdatePres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdatePres.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdatePres.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdatePres.Image")));
            this.cmdUpdatePres.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdUpdatePres.Location = new System.Drawing.Point(743, 6);
            this.cmdUpdatePres.Name = "cmdUpdatePres";
            this.cmdUpdatePres.Size = new System.Drawing.Size(120, 33);
            this.cmdUpdatePres.TabIndex = 81;
            this.cmdUpdatePres.Text = "Sửa";
            this.cmdUpdatePres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdUpdatePres.Click += new System.EventHandler(this.cmdUpdatePres_Click);
            // 
            // cmdCreateNewPres
            // 
            this.cmdCreateNewPres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreateNewPres.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreateNewPres.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreateNewPres.Image")));
            this.cmdCreateNewPres.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdCreateNewPres.Location = new System.Drawing.Point(617, 6);
            this.cmdCreateNewPres.Name = "cmdCreateNewPres";
            this.cmdCreateNewPres.Size = new System.Drawing.Size(120, 33);
            this.cmdCreateNewPres.TabIndex = 80;
            this.cmdCreateNewPres.Text = "Thêm";
            this.cmdCreateNewPres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdCreateNewPres.Click += new System.EventHandler(this.cmdCreateNewPres_Click);
            // 
            // txtLoidanBS
            // 
            this.txtLoidanBS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoidanBS.Location = new System.Drawing.Point(113, 21);
            this.txtLoidanBS.Multiline = true;
            this.txtLoidanBS.Name = "txtLoidanBS";
            this.txtLoidanBS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoidanBS.Size = new System.Drawing.Size(870, 208);
            this.txtLoidanBS.TabIndex = 70;
            this.txtLoidanBS.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiTab1.Location = new System.Drawing.Point(0, 137);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(996, 535);
            this.uiTab1.TabIndex = 4;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uitabpagethongtin,
            this.uiTabPageloidan,
            this.uiTabPagedonthuoc});
            // 
            // uitabpagethongtin
            // 
            this.uitabpagethongtin.Controls.Add(this.pnlFill);
            this.uitabpagethongtin.Location = new System.Drawing.Point(1, 23);
            this.uitabpagethongtin.Name = "uitabpagethongtin";
            this.uitabpagethongtin.Size = new System.Drawing.Size(992, 509);
            this.uitabpagethongtin.TabStop = true;
            this.uitabpagethongtin.Text = "Thông tỉn ra viện";
            // 
            // uiTabPageloidan
            // 
            this.uiTabPageloidan.Controls.Add(this.label25);
            this.uiTabPageloidan.Controls.Add(this.txtYkien);
            this.uiTabPageloidan.Controls.Add(this.label21);
            this.uiTabPageloidan.Controls.Add(this.txtLoidanBS);
            this.uiTabPageloidan.Location = new System.Drawing.Point(1, 23);
            this.uiTabPageloidan.Name = "uiTabPageloidan";
            this.uiTabPageloidan.Size = new System.Drawing.Size(992, 509);
            this.uiTabPageloidan.TabStop = true;
            this.uiTabPageloidan.Text = "Lời dặn bác sĩ";
            // 
            // uiTabPagedonthuoc
            // 
            this.uiTabPagedonthuoc.Controls.Add(this.grdPresDetail);
            this.uiTabPagedonthuoc.Controls.Add(this.pnlDonthuoc);
            this.uiTabPagedonthuoc.Location = new System.Drawing.Point(1, 23);
            this.uiTabPagedonthuoc.Name = "uiTabPagedonthuoc";
            this.uiTabPagedonthuoc.Size = new System.Drawing.Size(992, 509);
            this.uiTabPagedonthuoc.TabStop = true;
            this.uiTabPagedonthuoc.Text = "Đơn thuốc ra viện";
            // 
            // cboKhoaRavien
            // 
            this.cboKhoaRavien.FormattingEnabled = true;
            this.cboKhoaRavien.Location = new System.Drawing.Point(153, 29);
            this.cboKhoaRavien.Name = "cboKhoaRavien";
            this.cboKhoaRavien.Next_Control = null;
            this.cboKhoaRavien.RaiseEnterEventWhenInvisible = true;
            this.cboKhoaRavien.Size = new System.Drawing.Size(397, 23);
            this.cboKhoaRavien.TabIndex = 661;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(73, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 662;
            this.label6.Text = "Khoa ra viện";
            // 
            // frm_Phieuravien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(996, 739);
            this.Controls.Add(this.uiTab1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Phieuravien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu ra viện";
            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            this.pnlFill.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_ICD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).EndInit();
            this.pnlDonthuoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uitabpagethongtin.ResumeLayout(false);
            this.uiTabPageloidan.ResumeLayout(false);
            this.uiTabPageloidan.PerformLayout();
            this.uiTabPagedonthuoc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdHuy;
        private System.Windows.Forms.Label label20;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpNgayin;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UICheckBox chkChuyenvien;
        private System.Windows.Forms.Label label26;
        private AutoCompleteTextbox_Danhmucchung txtPhuongphapdieutri;
        private AutoCompleteTextbox txtNoichuyenden;
        private Janus.Windows.EditControls.UIButton cmdGetBV;
        private AutoCompleteTextbox_Danhmucchung txtKieuchuyenvien;
        private AutoCompleteTextbox_Danhmucchung txtTinhtrangravien;
        private AutoCompleteTextbox_Danhmucchung txtKqdieutri;
        private AutoCompleteTextbox txtBenhphu;
        private AutoCompleteTextbox txtBenhnguyennhan;
        private AutoCompleteTextbox txtBenhbienchung;
        internal System.Windows.Forms.Label label27;
        private Janus.Windows.EditControls.UICheckBox chkDaCapGiayRaVien;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtTongSoNgayDtri;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNGAY_CAP_GIAY_RVIEN;
        internal System.Windows.Forms.Label label11;
        private Janus.Windows.EditControls.UICheckBox chkPhuHopChanDoanCLS;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtPhutRaVien;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label23;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtGioRaVien;
        private System.Windows.Forms.Label label24;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayravien;
        private AutoCompleteTextbox_Danhmucchung txtphuongtienvc;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaynhapvien;
        private FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtSoRaVien;
        private Janus.Windows.GridEX.GridEX grd_ICD;
        private Janus.Windows.EditControls.UICheckBox chkInsaukhiluu;
        private Janus.Windows.EditControls.UICheckBox chkThoatsaukhiluu;
        private AutoCompleteTextbox txtBsChidinh;
        private AutoCompleteTextbox txtTruongkhoa;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtsotuanthai;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UICheckBox chkTuvong;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaytuvong;
        private AutoCompleteTextbox txtBenhchinh;
        internal System.Windows.Forms.Label label17;
        private Janus.Windows.EditControls.UIButton cmdChuyen;
        internal System.Windows.Forms.Label label25;
        private Janus.Windows.GridEX.EditControls.EditBox txtYkien;
        internal System.Windows.Forms.Label label21;
        private Janus.Windows.GridEX.GridEX grdPresDetail;
        private System.Windows.Forms.Panel pnlDonthuoc;
        private Janus.Windows.EditControls.UIButton cmdWords;
        private Janus.Windows.EditControls.UIButton cmdCreateNewPresTuTuc;
        private Janus.Windows.EditControls.UIButton cmdPrintPres;
        private Janus.Windows.EditControls.UIButton cmdDeletePres;
        private Janus.Windows.EditControls.UIButton cmdUpdatePres;
        private Janus.Windows.EditControls.UIButton cmdCreateNewPres;
        private Dungchung.UCs.ucThongtinnguoibenh ucThongtinnguoibenh1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayHen;
        private System.Windows.Forms.Label label4;
        private MaskedTextBox.MaskedTextBox txtSongayhentaikham;
        private MaskedTextBox.MaskedTextBox txtSoNgayHen;
        private Janus.Windows.EditControls.UICheckBox chkHentaikham;
        private AutoCompleteTextbox_Danhmucchung txtBenhgiaiphau;
        private AutoCompleteTextbox_Danhmucchung autoLydotuvong;
        internal System.Windows.Forms.Label label5;
        private AutoCompleteTextbox_Danhmucchung txtChandoan;
        private Janus.Windows.GridEX.EditControls.EditBox txtLoidanBS;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenBenhChinh;
        private Janus.Windows.EditControls.UIButton cmdChuyenvien;
        public AutoCompleteTextbox_Danhmucchung txtNguoivanchuyen;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtMatheBHYT;
        private System.Windows.Forms.Label label39;
        private Janus.Windows.CalendarCombo.CalendarCombo dtInsToDate;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblMatheBHYT;
        private Janus.Windows.CalendarCombo.CalendarCombo dtInsFromDate;
        private Janus.Windows.EditControls.UIButton cmdHuyphieuravien;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uitabpagethongtin;
        private Janus.Windows.UI.Tab.UITabPage uiTabPageloidan;
        private Janus.Windows.UI.Tab.UITabPage uiTabPagedonthuoc;
        private System.Windows.Forms.Label label6;
        private EasyCompletionComboBox cboKhoaRavien;
    }
}