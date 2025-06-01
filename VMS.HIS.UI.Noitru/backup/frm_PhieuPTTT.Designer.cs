using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_PhieuPTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuPTTT));
            Janus.Windows.GridEX.GridEXLayout grdChiDinh_Layout_0 = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPhieuPTTT_Layout_0 = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDungcuvongtrong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDungcuvongngoai_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDieuduonggayme_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grd_bsphauthuatphu_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grd_bsgm_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grd_bspt_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.txtIdPhieuPTTT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label25 = new System.Windows.Forms.Label();
            this.grpThongTinPhieu = new System.Windows.Forms.GroupBox();
            this.cmdAddNew = new Janus.Windows.EditControls.UIButton();
            this.grdChiDinh = new Janus.Windows.GridEX.GridEX();
            this.ctxInphieu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCamket = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInchungnhanPTTT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInphieuPTTT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPhieutuongtrinhPTTT = new System.Windows.Forms.ToolStripMenuItem();
            this.grdPhieuPTTT = new Janus.Windows.GridEX.GridEX();
            this.tabThongTin = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.picPTTT = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cboHinhPTTT = new System.Windows.Forms.ComboBox();
            this.autoDieuduonggayme = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoDungcuvongngoai = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoDungcuvongtrong = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.grdDungcuvongtrong = new Janus.Windows.GridEX.GridEX();
            this.grdDungcuvongngoai = new Janus.Windows.GridEX.GridEX();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.grdDieuduonggayme = new Janus.Windows.GridEX.GridEX();
            this.txtMaphieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grd_bsphauthuatphu = new Janus.Windows.GridEX.GridEX();
            this.grd_bsgm = new Janus.Windows.GridEX.GridEX();
            this.autoGiuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoBuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.autoLoaiPTTT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoBSphu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoBSGayme = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoBSPhauthuat = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoLydotuvong = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoLydotaibien = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPhuongPhapPT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPhuongPhapVoCam = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtLuocDoPhauThuat = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtChanDoanTruocPT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtChanDoanSauPT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtTrinhTuPhauThat = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.chkNgayCatChi = new System.Windows.Forms.CheckBox();
            this.chkNgayRut = new System.Windows.Forms.CheckBox();
            this.chkPTTT_KetThuc = new System.Windows.Forms.CheckBox();
            this.txtphu = new System.Windows.Forms.Label();
            this.chkTaibien = new System.Windows.Forms.CheckBox();
            this.dtNgayGioTuVong = new System.Windows.Forms.DateTimePicker();
            this.chkTuvong = new System.Windows.Forms.CheckBox();
            this.dtpNgayGioKetThucPTTT = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radNgoaiTru = new System.Windows.Forms.RadioButton();
            this.radNoiTru = new System.Windows.Forms.RadioButton();
            this.txtDanLuu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayRut = new System.Windows.Forms.DateTimePicker();
            this.txtKhac = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtBac = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtNgayPhauThuat = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpNgayCatChi = new System.Windows.Forms.DateTimePicker();
            this.grd_bspt = new Janus.Windows.GridEX.GridEX();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkPreview = new Janus.Windows.EditControls.UICheckBox();
            this.chkHoitruockhixoa = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtNgayIn = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ucThongtinnguoibenh_doc_v11 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_doc_v1();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdScanFinger = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.grpThongTinPhieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiDinh)).BeginInit();
            this.ctxInphieu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuPTTT)).BeginInit();
            this.tabThongTin.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPTTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDungcuvongtrong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDungcuvongngoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuduonggayme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bsphauthuatphu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bsgm)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bspt)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIdPhieuPTTT
            // 
            this.txtIdPhieuPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdPhieuPTTT.Enabled = false;
            this.txtIdPhieuPTTT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPhieuPTTT.Location = new System.Drawing.Point(703, 52);
            this.txtIdPhieuPTTT.Name = "txtIdPhieuPTTT";
            this.txtIdPhieuPTTT.Size = new System.Drawing.Size(305, 22);
            this.txtIdPhieuPTTT.TabIndex = 546;
            this.txtIdPhieuPTTT.TabStop = false;
            this.txtIdPhieuPTTT.Visible = false;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(604, 55);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(93, 15);
            this.label25.TabIndex = 551;
            this.label25.Text = "ID Phiếu :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label25.Visible = false;
            // 
            // grpThongTinPhieu
            // 
            this.grpThongTinPhieu.Controls.Add(this.cmdAddNew);
            this.grpThongTinPhieu.Controls.Add(this.grdChiDinh);
            this.grpThongTinPhieu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinPhieu.Location = new System.Drawing.Point(0, 375);
            this.grpThongTinPhieu.Name = "grpThongTinPhieu";
            this.grpThongTinPhieu.Size = new System.Drawing.Size(384, 243);
            this.grpThongTinPhieu.TabIndex = 0;
            this.grpThongTinPhieu.TabStop = false;
            this.grpThongTinPhieu.Text = "Thông tin chỉ định";
            // 
            // cmdAddNew
            // 
            this.cmdAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddNew.Image")));
            this.cmdAddNew.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddNew.Location = new System.Drawing.Point(252, 205);
            this.cmdAddNew.Name = "cmdAddNew";
            this.cmdAddNew.Size = new System.Drawing.Size(120, 32);
            this.cmdAddNew.TabIndex = 572;
            this.cmdAddNew.TabStop = false;
            this.cmdAddNew.Text = "Thêm mới";
            this.cmdAddNew.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdAddNew.Click += new System.EventHandler(this.cmdThemMoiBN_Click);
            // 
            // grdChiDinh
            // 
            this.grdChiDinh.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdChiDinh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdChiDinh.BackColor = System.Drawing.Color.Silver;
            this.grdChiDinh.ContextMenuStrip = this.ctxInphieu;
            this.grdChiDinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChiDinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChiDinh.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdChiDinh.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdChiDinh.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdChiDinh.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChiDinh.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChiDinh.GroupByBoxVisible = false;
            this.grdChiDinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            grdChiDinh_Layout_0.IsCurrentLayout = true;
            grdChiDinh_Layout_0.Key = "layout";
            grdChiDinh_Layout_0.LayoutString = resources.GetString("grdChiDinh_Layout_0.LayoutString");
            this.grdChiDinh.Layouts.AddRange(new Janus.Windows.GridEX.GridEXLayout[] {
            grdChiDinh_Layout_0});
            this.grdChiDinh.Location = new System.Drawing.Point(3, 16);
            this.grdChiDinh.Name = "grdChiDinh";
            this.grdChiDinh.RecordNavigator = true;
            this.grdChiDinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiDinh.SettingsKey = "grdList";
            this.grdChiDinh.Size = new System.Drawing.Size(378, 183);
            this.grdChiDinh.TabIndex = 2;
            this.grdChiDinh.TabStop = false;
            this.grdChiDinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // ctxInphieu
            // 
            this.ctxInphieu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCamket,
            this.toolStripMenuItem2,
            this.mnuInchungnhanPTTT,
            this.toolStripMenuItem1,
            this.mnuInphieuPTTT,
            this.toolStripMenuItem3,
            this.mnuPhieutuongtrinhPTTT});
            this.ctxInphieu.Name = "contextMenuStrip1";
            this.ctxInphieu.Size = new System.Drawing.Size(309, 110);
            // 
            // mnuCamket
            // 
            this.mnuCamket.Name = "mnuCamket";
            this.mnuCamket.Size = new System.Drawing.Size(308, 22);
            this.mnuCamket.Text = "In cam kết PTTT";
            this.mnuCamket.Click += new System.EventHandler(this.mnuCamket_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(305, 6);
            // 
            // mnuInchungnhanPTTT
            // 
            this.mnuInchungnhanPTTT.Name = "mnuInchungnhanPTTT";
            this.mnuInchungnhanPTTT.Size = new System.Drawing.Size(308, 22);
            this.mnuInchungnhanPTTT.Text = "In phiếu  chứng nhận phẫu thuật/  thủ thuật";
            this.mnuInchungnhanPTTT.Click += new System.EventHandler(this.mnuInphieuthuthuat_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(305, 6);
            // 
            // mnuInphieuPTTT
            // 
            this.mnuInphieuPTTT.Name = "mnuInphieuPTTT";
            this.mnuInphieuPTTT.Size = new System.Drawing.Size(308, 22);
            this.mnuInphieuPTTT.Text = "In phiếu Phẫu thuật/ thủ thuật";
            this.mnuInphieuPTTT.Click += new System.EventHandler(this.mnuInphieuPTTT_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(305, 6);
            // 
            // mnuPhieutuongtrinhPTTT
            // 
            this.mnuPhieutuongtrinhPTTT.Name = "mnuPhieutuongtrinhPTTT";
            this.mnuPhieutuongtrinhPTTT.Size = new System.Drawing.Size(308, 22);
            this.mnuPhieutuongtrinhPTTT.Text = "In phiếu tường trình Phẫu thuật - Thủ thuật";
            this.mnuPhieutuongtrinhPTTT.Click += new System.EventHandler(this.mnuPhieutuongtrinhPTTT_Click);
            // 
            // grdPhieuPTTT
            // 
            this.grdPhieuPTTT.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPhieuPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPhieuPTTT.BackColor = System.Drawing.Color.Silver;
            this.grdPhieuPTTT.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPhieuPTTT.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPhieuPTTT.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdPhieuPTTT.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdPhieuPTTT.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPhieuPTTT.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPhieuPTTT.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPhieuPTTT.GroupByBoxVisible = false;
            this.grdPhieuPTTT.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            grdPhieuPTTT_Layout_0.IsCurrentLayout = true;
            grdPhieuPTTT_Layout_0.Key = "layout";
            grdPhieuPTTT_Layout_0.LayoutString = resources.GetString("grdPhieuPTTT_Layout_0.LayoutString");
            this.grdPhieuPTTT.Layouts.AddRange(new Janus.Windows.GridEX.GridEXLayout[] {
            grdPhieuPTTT_Layout_0});
            this.grdPhieuPTTT.Location = new System.Drawing.Point(3, 16);
            this.grdPhieuPTTT.Name = "grdPhieuPTTT";
            this.grdPhieuPTTT.RecordNavigator = true;
            this.grdPhieuPTTT.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhieuPTTT.SettingsKey = "grdList";
            this.grdPhieuPTTT.Size = new System.Drawing.Size(378, 130);
            this.grdPhieuPTTT.TabIndex = 1;
            this.grdPhieuPTTT.TabStop = false;
            this.grdPhieuPTTT.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // tabThongTin
            // 
            this.tabThongTin.Controls.Add(this.tabPage1);
            this.tabThongTin.Controls.Add(this.tabPage2);
            this.tabThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabThongTin.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabThongTin.Location = new System.Drawing.Point(384, 0);
            this.tabThongTin.Name = "tabThongTin";
            this.tabThongTin.SelectedIndex = 0;
            this.tabThongTin.Size = new System.Drawing.Size(1026, 799);
            this.tabThongTin.TabIndex = 0;
            this.tabThongTin.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.grpThongTin);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1018, 771);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin phiếu";
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.SystemColors.Control;
            this.grpThongTin.Controls.Add(this.picPTTT);
            this.grpThongTin.Controls.Add(this.label20);
            this.grpThongTin.Controls.Add(this.cboHinhPTTT);
           
            this.grpThongTin.Controls.Add(this.label17);
            this.grpThongTin.Controls.Add(this.label18);
            this.grpThongTin.Controls.Add(this.label19);
            this.grpThongTin.Controls.Add(this.grdDieuduonggayme);
            this.grpThongTin.Controls.Add(this.txtMaphieu);
            this.grpThongTin.Controls.Add(this.label16);
            this.grpThongTin.Controls.Add(this.grd_bsphauthuatphu);
            this.grpThongTin.Controls.Add(this.grd_bsgm);
            this.grpThongTin.Controls.Add(this.autoGiuong);
            this.grpThongTin.Controls.Add(this.autoBuong);
            this.grpThongTin.Controls.Add(this.autoKhoa);
            this.grpThongTin.Controls.Add(this.label2);
            this.grpThongTin.Controls.Add(this.label4);
            this.grpThongTin.Controls.Add(this.label5);
            this.grpThongTin.Controls.Add(this.label1);
            this.grpThongTin.Controls.Add(this.autoLoaiPTTT);
            this.grpThongTin.Controls.Add(this.autoBSphu);
            this.grpThongTin.Controls.Add(this.autoBSGayme);
            this.grpThongTin.Controls.Add(this.autoBSPhauthuat);
            this.grpThongTin.Controls.Add(this.autoDieuduonggayme);
            this.grpThongTin.Controls.Add(this.autoDungcuvongngoai);
            this.grpThongTin.Controls.Add(this.autoDungcuvongtrong);
            this.grpThongTin.Controls.Add(this.grdDungcuvongtrong);
            this.grpThongTin.Controls.Add(this.grdDungcuvongngoai);
            this.grpThongTin.Controls.Add(this.autoLydotuvong);
            this.grpThongTin.Controls.Add(this.autoLydotaibien);
            this.grpThongTin.Controls.Add(this.txtPhuongPhapPT);
            this.grpThongTin.Controls.Add(this.txtPhuongPhapVoCam);
            this.grpThongTin.Controls.Add(this.txtLuocDoPhauThuat);
            this.grpThongTin.Controls.Add(this.txtChanDoanTruocPT);
            this.grpThongTin.Controls.Add(this.txtChanDoanSauPT);
            this.grpThongTin.Controls.Add(this.txtIdPhieuPTTT);
            this.grpThongTin.Controls.Add(this.label25);
            this.grpThongTin.Controls.Add(this.txtTrinhTuPhauThat);
            this.grpThongTin.Controls.Add(this.chkNgayCatChi);
            this.grpThongTin.Controls.Add(this.chkNgayRut);
            this.grpThongTin.Controls.Add(this.chkPTTT_KetThuc);
            this.grpThongTin.Controls.Add(this.txtphu);
            this.grpThongTin.Controls.Add(this.chkTaibien);
            this.grpThongTin.Controls.Add(this.dtNgayGioTuVong);
            this.grpThongTin.Controls.Add(this.chkTuvong);
            this.grpThongTin.Controls.Add(this.dtpNgayGioKetThucPTTT);
            this.grpThongTin.Controls.Add(this.panel1);
            this.grpThongTin.Controls.Add(this.txtDanLuu);
            this.grpThongTin.Controls.Add(this.label3);
            this.grpThongTin.Controls.Add(this.dtpNgayRut);
            this.grpThongTin.Controls.Add(this.txtKhac);
            this.grpThongTin.Controls.Add(this.txtBac);
            this.grpThongTin.Controls.Add(this.dtNgayPhauThuat);
            this.grpThongTin.Controls.Add(this.label22);
            this.grpThongTin.Controls.Add(this.label21);
            this.grpThongTin.Controls.Add(this.label15);
            this.grpThongTin.Controls.Add(this.label14);
            this.grpThongTin.Controls.Add(this.label13);
            this.grpThongTin.Controls.Add(this.label12);
            this.grpThongTin.Controls.Add(this.label11);
            this.grpThongTin.Controls.Add(this.label10);
            this.grpThongTin.Controls.Add(this.label9);
            this.grpThongTin.Controls.Add(this.label8);
            this.grpThongTin.Controls.Add(this.label7);
            this.grpThongTin.Controls.Add(this.label6);

            this.grpThongTin.Controls.Add(this.dtpNgayCatChi);
            this.grpThongTin.Controls.Add(this.grd_bspt);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Location = new System.Drawing.Point(3, 3);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1012, 765);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            // 
            // picPTTT
            // 
            this.picPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picPTTT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPTTT.Location = new System.Drawing.Point(755, 532);
            this.picPTTT.Name = "picPTTT";
            this.picPTTT.Size = new System.Drawing.Size(251, 227);
            this.picPTTT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPTTT.TabIndex = 614;
            this.picPTTT.TabStop = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(641, 509);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(108, 18);
            this.label20.TabIndex = 613;
            this.label20.Text = "Hình phẫu thuật";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboHinhPTTT
            // 
            this.cboHinhPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHinhPTTT.FormattingEnabled = true;
            this.cboHinhPTTT.Items.AddRange(new object[] {
            "Chọn hình phẫu thuật",
            "CẤU TẠO THẬN",
            "HỈNH TỬ CUNG",
            "CẤU TRÚC SINH DỤC NAM"});
            this.cboHinhPTTT.Location = new System.Drawing.Point(755, 504);
            this.cboHinhPTTT.Name = "cboHinhPTTT";
            this.cboHinhPTTT.Size = new System.Drawing.Size(252, 23);
            this.cboHinhPTTT.TabIndex = 612;
            this.cboHinhPTTT.SelectedIndexChanged += new System.EventHandler(this.cboHinhPTTT_SelectedIndexChanged);
            // 
            // autoDieuduonggayme
            // 
            this.autoDieuduonggayme._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoDieuduonggayme._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDieuduonggayme._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoDieuduonggayme.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoDieuduonggayme.AutoCompleteList")));
            this.autoDieuduonggayme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoDieuduonggayme.buildShortcut = false;
            this.autoDieuduonggayme.CaseSensitive = false;
            this.autoDieuduonggayme.CompareNoID = true;
            this.autoDieuduonggayme.DefaultCode = "-1";
            this.autoDieuduonggayme.DefaultID = "-1";
            this.autoDieuduonggayme.DisplayType = 0;
            this.autoDieuduonggayme.Drug_ID = null;
            this.autoDieuduonggayme.ExtraWidth = 300;
            this.autoDieuduonggayme.FillValueAfterSelect = false;
            this.autoDieuduonggayme.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDieuduonggayme.Location = new System.Drawing.Point(109, 311);
            this.autoDieuduonggayme.MaxHeight = 289;
            this.autoDieuduonggayme.MinTypedCharacters = 2;
            this.autoDieuduonggayme.MyCode = "-1";
            this.autoDieuduonggayme.MyID = "-1";
            this.autoDieuduonggayme.MyText = "";
            this.autoDieuduonggayme.MyTextOnly = "";
            this.autoDieuduonggayme.Name = "autoDieuduonggayme";
            this.autoDieuduonggayme.RaiseEvent = true;
            this.autoDieuduonggayme.RaiseEventEnter = true;
            this.autoDieuduonggayme.RaiseEventEnterWhenEmpty = true;
            this.autoDieuduonggayme.SelectedIndex = -1;
            this.autoDieuduonggayme.Size = new System.Drawing.Size(202, 22);
            this.autoDieuduonggayme.splitChar = '@';
            this.autoDieuduonggayme.splitCharIDAndCode = '#';
            this.autoDieuduonggayme.TabIndex = 22;
            this.autoDieuduonggayme.TakeCode = false;
            this.autoDieuduonggayme.txtMyCode = null;
            this.autoDieuduonggayme.txtMyCode_Edit = null;
            this.autoDieuduonggayme.txtMyID = null;
            this.autoDieuduonggayme.txtMyID_Edit = null;
            this.autoDieuduonggayme.txtMyName = null;
            this.autoDieuduonggayme.txtMyName_Edit = null;
            this.autoDieuduonggayme.txtNext = null;
            // 
            // autoDungcuvongngoai
            // 
            this.autoDungcuvongngoai._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoDungcuvongngoai._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDungcuvongngoai._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoDungcuvongngoai.AddValues = true;
            this.autoDungcuvongngoai.AllowMultiline = false;
            this.autoDungcuvongngoai.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoDungcuvongngoai.AutoCompleteList")));
            this.autoDungcuvongngoai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoDungcuvongngoai.buildShortcut = false;
            this.autoDungcuvongngoai.CaseSensitive = false;
            this.autoDungcuvongngoai.CompareNoID = true;
            this.autoDungcuvongngoai.DefaultCode = "-1";
            this.autoDungcuvongngoai.DefaultID = "-1";
            this.autoDungcuvongngoai.Drug_ID = null;
            this.autoDungcuvongngoai.ExtraWidth = 0;
            this.autoDungcuvongngoai.FillValueAfterSelect = false;
            this.autoDungcuvongngoai.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDungcuvongngoai.LOAI_DANHMUC = "DUNGCUVONGNGOAI";
            this.autoDungcuvongngoai.Location = new System.Drawing.Point(462, 310);
            this.autoDungcuvongngoai.MaxHeight = 200;
            this.autoDungcuvongngoai.MinTypedCharacters = 2;
            this.autoDungcuvongngoai.MyCode = "-1";
            this.autoDungcuvongngoai.MyID = "-1";
            this.autoDungcuvongngoai.Name = "autoDungcuvongngoai";
            this.autoDungcuvongngoai.RaiseEvent = true;
            this.autoDungcuvongngoai.RaiseEventEnter = true;
            this.autoDungcuvongngoai.RaiseEventEnterWhenEmpty = false;
            this.autoDungcuvongngoai.SelectedIndex = -1;
            this.autoDungcuvongngoai.ShowCodeWithValue = false;
            this.autoDungcuvongngoai.Size = new System.Drawing.Size(190, 22);
            this.autoDungcuvongngoai.splitChar = '@';
            this.autoDungcuvongngoai.splitCharIDAndCode = '#';
            this.autoDungcuvongngoai.TabIndex = 22;
            this.autoDungcuvongngoai.TakeCode = false;
            this.autoDungcuvongngoai.txtMyCode = null;
            this.autoDungcuvongngoai.txtMyCode_Edit = null;
            this.autoDungcuvongngoai.txtMyID = null;
            this.autoDungcuvongngoai.txtMyID_Edit = null;
            this.autoDungcuvongngoai.txtMyName = null;
            this.autoDungcuvongngoai.txtMyName_Edit = null;
            this.autoDungcuvongngoai.txtNext = null;
            this.autoDungcuvongngoai.txtNext1 = null;
            // 
            // autoDungcuvongtrong
            // 
            this.autoDungcuvongtrong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoDungcuvongtrong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDungcuvongtrong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoDungcuvongtrong.AddValues = true;
            this.autoDungcuvongtrong.AllowMultiline = false;
            this.autoDungcuvongtrong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoDungcuvongtrong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoDungcuvongtrong.AutoCompleteList")));
            this.autoDungcuvongtrong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoDungcuvongtrong.buildShortcut = false;
            this.autoDungcuvongtrong.CaseSensitive = false;
            this.autoDungcuvongtrong.CompareNoID = true;
            this.autoDungcuvongtrong.DefaultCode = "-1";
            this.autoDungcuvongtrong.DefaultID = "-1";
            this.autoDungcuvongtrong.Drug_ID = null;
            this.autoDungcuvongtrong.ExtraWidth = -300;
            this.autoDungcuvongtrong.FillValueAfterSelect = false;
            this.autoDungcuvongtrong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDungcuvongtrong.LOAI_DANHMUC = "DUNGCUVONGTRONG";
            this.autoDungcuvongtrong.Location = new System.Drawing.Point(800, 310);
            this.autoDungcuvongtrong.MaxHeight = 200;
            this.autoDungcuvongtrong.MinTypedCharacters = 2;
            this.autoDungcuvongtrong.MyCode = "-1";
            this.autoDungcuvongtrong.MyID = "-1";
            this.autoDungcuvongtrong.Name = "autoDungcuvongtrong";
            this.autoDungcuvongtrong.RaiseEvent = true;
            this.autoDungcuvongtrong.RaiseEventEnter = true;
            this.autoDungcuvongtrong.RaiseEventEnterWhenEmpty = false;
            this.autoDungcuvongtrong.SelectedIndex = -1;
            this.autoDungcuvongtrong.ShowCodeWithValue = false;
            this.autoDungcuvongtrong.Size = new System.Drawing.Size(208, 22);
            this.autoDungcuvongtrong.splitChar = '@';
            this.autoDungcuvongtrong.splitCharIDAndCode = '#';
            this.autoDungcuvongtrong.TabIndex = 22;
            this.autoDungcuvongtrong.TakeCode = false;
            this.autoDungcuvongtrong.txtMyCode = null;
            this.autoDungcuvongtrong.txtMyCode_Edit = null;
            this.autoDungcuvongtrong.txtMyID = null;
            this.autoDungcuvongtrong.txtMyID_Edit = null;
            this.autoDungcuvongtrong.txtMyName = null;
            this.autoDungcuvongtrong.txtMyName_Edit = null;
            this.autoDungcuvongtrong.txtNext = null;
            this.autoDungcuvongtrong.txtNext1 = null;
            // 
            // grdDungcuvongtrong
            // 
            this.grdDungcuvongtrong.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDungcuvongtrong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdDungcuvongtrong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grdDungcuvongtrong_DesignTimeLayout.LayoutString = resources.GetString("grdDungcuvongtrong_DesignTimeLayout.LayoutString");
            this.grdDungcuvongtrong.DesignTimeLayout = grdDungcuvongtrong_DesignTimeLayout;
            this.grdDungcuvongtrong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDungcuvongtrong.GroupByBoxVisible = false;
            this.grdDungcuvongtrong.Location = new System.Drawing.Point(607, 335);
            this.grdDungcuvongtrong.Name = "grdDungcuvongtrong";
            this.grdDungcuvongtrong.Size = new System.Drawing.Size(401, 106);
            this.grdDungcuvongtrong.TabIndex = 610;
            this.grdDungcuvongtrong.TabStop = false;
            // 
            // grdDungcuvongngoai
            // 
            this.grdDungcuvongngoai.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDungcuvongngoai.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdDungcuvongngoai_DesignTimeLayout.LayoutString = resources.GetString("grdDungcuvongngoai_DesignTimeLayout.LayoutString");
            this.grdDungcuvongngoai.DesignTimeLayout = grdDungcuvongngoai_DesignTimeLayout;
            this.grdDungcuvongngoai.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDungcuvongngoai.GroupByBoxVisible = false;
            this.grdDungcuvongngoai.Location = new System.Drawing.Point(317, 333);
            this.grdDungcuvongngoai.Name = "grdDungcuvongngoai";
            this.grdDungcuvongngoai.Size = new System.Drawing.Size(281, 106);
            this.grdDungcuvongngoai.TabIndex = 609;
            this.grdDungcuvongngoai.TabStop = false;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(652, 311);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(142, 21);
            this.label17.TabIndex = 607;
            this.label17.Text = "Điều dưỡng vòng trong";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(313, 310);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(143, 21);
            this.label18.TabIndex = 606;
            this.label18.Text = "Điều dưỡng vòng ngoài";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(-1, 310);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 38);
            this.label19.TabIndex = 605;
            this.label19.Text = "Điều dưỡng gây mê";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdDieuduonggayme
            // 
            this.grdDieuduonggayme.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDieuduonggayme.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdDieuduonggayme_DesignTimeLayout.LayoutString = resources.GetString("grdDieuduonggayme_DesignTimeLayout.LayoutString");
            this.grdDieuduonggayme.DesignTimeLayout = grdDieuduonggayme_DesignTimeLayout;
            this.grdDieuduonggayme.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDieuduonggayme.GroupByBoxVisible = false;
            this.grdDieuduonggayme.Location = new System.Drawing.Point(109, 335);
            this.grdDieuduonggayme.Name = "grdDieuduonggayme";
            this.grdDieuduonggayme.Size = new System.Drawing.Size(202, 106);
            this.grdDieuduonggayme.TabIndex = 608;
            this.grdDieuduonggayme.TabStop = false;
            // 
            // txtMaphieu
            // 
            this.txtMaphieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaphieu.Enabled = false;
            this.txtMaphieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaphieu.Location = new System.Drawing.Point(703, 74);
            this.txtMaphieu.Name = "txtMaphieu";
            this.txtMaphieu.Size = new System.Drawing.Size(305, 22);
            this.txtMaphieu.TabIndex = 600;
            this.txtMaphieu.TabStop = false;
            this.txtMaphieu.Visible = false;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(604, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 15);
            this.label16.TabIndex = 601;
            this.label16.Text = "Mã phiếu:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Visible = false;
            // 
            // grd_bsphauthuatphu
            // 
            this.grd_bsphauthuatphu.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_bsphauthuatphu.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grd_bsphauthuatphu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grd_bsphauthuatphu_DesignTimeLayout.LayoutString = resources.GetString("grd_bsphauthuatphu_DesignTimeLayout.LayoutString");
            this.grd_bsphauthuatphu.DesignTimeLayout = grd_bsphauthuatphu_DesignTimeLayout;
            this.grd_bsphauthuatphu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_bsphauthuatphu.GroupByBoxVisible = false;
            this.grd_bsphauthuatphu.Location = new System.Drawing.Point(607, 175);
            this.grd_bsphauthuatphu.Name = "grd_bsphauthuatphu";
            this.grd_bsphauthuatphu.Size = new System.Drawing.Size(401, 128);
            this.grd_bsphauthuatphu.TabIndex = 599;
            this.grd_bsphauthuatphu.TabStop = false;
            // 
            // grd_bsgm
            // 
            this.grd_bsgm.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_bsgm.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grd_bsgm_DesignTimeLayout.LayoutString = resources.GetString("grd_bsgm_DesignTimeLayout.LayoutString");
            this.grd_bsgm.DesignTimeLayout = grd_bsgm_DesignTimeLayout;
            this.grd_bsgm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_bsgm.GroupByBoxVisible = false;
            this.grd_bsgm.Location = new System.Drawing.Point(317, 173);
            this.grd_bsgm.Name = "grd_bsgm";
            this.grd_bsgm.Size = new System.Drawing.Size(281, 128);
            this.grd_bsgm.TabIndex = 598;
            this.grd_bsgm.TabStop = false;
            // 
            // autoGiuong
            // 
            this.autoGiuong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoGiuong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoGiuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoGiuong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoGiuong.AutoCompleteList")));
            this.autoGiuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoGiuong.buildShortcut = false;
            this.autoGiuong.CaseSensitive = false;
            this.autoGiuong.CompareNoID = true;
            this.autoGiuong.DefaultCode = "-1";
            this.autoGiuong.DefaultID = "-1";
            this.autoGiuong.DisplayType = 1;
            this.autoGiuong.Drug_ID = null;
            this.autoGiuong.ExtraWidth = 0;
            this.autoGiuong.FillValueAfterSelect = false;
            this.autoGiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong.Location = new System.Drawing.Point(703, 19);
            this.autoGiuong.MaxHeight = 289;
            this.autoGiuong.MinTypedCharacters = 2;
            this.autoGiuong.MyCode = "-1";
            this.autoGiuong.MyID = "-1";
            this.autoGiuong.MyText = "";
            this.autoGiuong.MyTextOnly = "";
            this.autoGiuong.Name = "autoGiuong";
            this.autoGiuong.RaiseEvent = false;
            this.autoGiuong.RaiseEventEnter = false;
            this.autoGiuong.RaiseEventEnterWhenEmpty = false;
            this.autoGiuong.SelectedIndex = -1;
            this.autoGiuong.Size = new System.Drawing.Size(306, 22);
            this.autoGiuong.splitChar = '@';
            this.autoGiuong.splitCharIDAndCode = '#';
            this.autoGiuong.TabIndex = 2;
            this.autoGiuong.TakeCode = false;
            this.autoGiuong.txtMyCode = null;
            this.autoGiuong.txtMyCode_Edit = null;
            this.autoGiuong.txtMyID = null;
            this.autoGiuong.txtMyID_Edit = null;
            this.autoGiuong.txtMyName = null;
            this.autoGiuong.txtMyName_Edit = null;
            this.autoGiuong.txtNext = null;
            // 
            // autoBuong
            // 
            this.autoBuong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBuong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBuong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBuong.AutoCompleteList")));
            this.autoBuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBuong.buildShortcut = false;
            this.autoBuong.CaseSensitive = false;
            this.autoBuong.CompareNoID = true;
            this.autoBuong.DefaultCode = "-1";
            this.autoBuong.DefaultID = "-1";
            this.autoBuong.DisplayType = 1;
            this.autoBuong.Drug_ID = null;
            this.autoBuong.ExtraWidth = 0;
            this.autoBuong.FillValueAfterSelect = false;
            this.autoBuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong.Location = new System.Drawing.Point(409, 19);
            this.autoBuong.MaxHeight = 289;
            this.autoBuong.MinTypedCharacters = 2;
            this.autoBuong.MyCode = "-1";
            this.autoBuong.MyID = "-1";
            this.autoBuong.MyText = "";
            this.autoBuong.MyTextOnly = "";
            this.autoBuong.Name = "autoBuong";
            this.autoBuong.RaiseEvent = true;
            this.autoBuong.RaiseEventEnter = true;
            this.autoBuong.RaiseEventEnterWhenEmpty = false;
            this.autoBuong.SelectedIndex = -1;
            this.autoBuong.Size = new System.Drawing.Size(189, 22);
            this.autoBuong.splitChar = '@';
            this.autoBuong.splitCharIDAndCode = '#';
            this.autoBuong.TabIndex = 1;
            this.autoBuong.TakeCode = false;
            this.autoBuong.txtMyCode = null;
            this.autoBuong.txtMyCode_Edit = null;
            this.autoBuong.txtMyID = null;
            this.autoBuong.txtMyID_Edit = null;
            this.autoBuong.txtMyName = null;
            this.autoBuong.txtMyName_Edit = null;
            this.autoBuong.txtNext = null;
            // 
            // autoKhoa
            // 
            this.autoKhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoKhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoKhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoKhoa.AutoCompleteList")));
            this.autoKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoKhoa.buildShortcut = false;
            this.autoKhoa.CaseSensitive = false;
            this.autoKhoa.CompareNoID = true;
            this.autoKhoa.DefaultCode = "-1";
            this.autoKhoa.DefaultID = "-1";
            this.autoKhoa.DisplayType = 1;
            this.autoKhoa.Drug_ID = null;
            this.autoKhoa.ExtraWidth = 0;
            this.autoKhoa.FillValueAfterSelect = false;
            this.autoKhoa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa.Location = new System.Drawing.Point(109, 19);
            this.autoKhoa.MaxHeight = 289;
            this.autoKhoa.MinTypedCharacters = 2;
            this.autoKhoa.MyCode = "-1";
            this.autoKhoa.MyID = "-1";
            this.autoKhoa.MyText = "";
            this.autoKhoa.MyTextOnly = "";
            this.autoKhoa.Name = "autoKhoa";
            this.autoKhoa.RaiseEvent = true;
            this.autoKhoa.RaiseEventEnter = true;
            this.autoKhoa.RaiseEventEnterWhenEmpty = false;
            this.autoKhoa.SelectedIndex = -1;
            this.autoKhoa.Size = new System.Drawing.Size(202, 22);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 0;
            this.autoKhoa.TakeCode = false;
            this.autoKhoa.txtMyCode = null;
            this.autoKhoa.txtMyCode_Edit = null;
            this.autoKhoa.txtMyID = null;
            this.autoKhoa.txtMyID_Edit = null;
            this.autoKhoa.txtMyName = null;
            this.autoKhoa.txtMyName_Edit = null;
            this.autoKhoa.txtNext = null;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(604, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 23);
            this.label2.TabIndex = 594;
            this.label2.Text = "Giường :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(313, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 23);
            this.label4.TabIndex = 593;
            this.label4.Text = "Buồng :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(-1, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 18);
            this.label5.TabIndex = 592;
            this.label5.Text = "Khoa nội trú :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(317, 706);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 19);
            this.label1.TabIndex = 588;
            this.label1.Text = "Lúc";
            // 
            // autoLoaiPTTT
            // 
            this.autoLoaiPTTT._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoLoaiPTTT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLoaiPTTT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLoaiPTTT.AddValues = true;
            this.autoLoaiPTTT.AllowMultiline = false;
            this.autoLoaiPTTT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLoaiPTTT.AutoCompleteList")));
            this.autoLoaiPTTT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLoaiPTTT.buildShortcut = false;
            this.autoLoaiPTTT.CaseSensitive = false;
            this.autoLoaiPTTT.CompareNoID = true;
            this.autoLoaiPTTT.DefaultCode = "-1";
            this.autoLoaiPTTT.DefaultID = "-1";
            this.autoLoaiPTTT.Drug_ID = null;
            this.autoLoaiPTTT.ExtraWidth = 0;
            this.autoLoaiPTTT.FillValueAfterSelect = false;
            this.autoLoaiPTTT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLoaiPTTT.LOAI_DANHMUC = "LOAIPTTT";
            this.autoLoaiPTTT.Location = new System.Drawing.Point(109, 77);
            this.autoLoaiPTTT.MaxHeight = 279;
            this.autoLoaiPTTT.MinTypedCharacters = 2;
            this.autoLoaiPTTT.MyCode = "-1";
            this.autoLoaiPTTT.MyID = "-1";
            this.autoLoaiPTTT.Name = "autoLoaiPTTT";
            this.autoLoaiPTTT.RaiseEvent = false;
            this.autoLoaiPTTT.RaiseEventEnter = false;
            this.autoLoaiPTTT.RaiseEventEnterWhenEmpty = false;
            this.autoLoaiPTTT.SelectedIndex = -1;
            this.autoLoaiPTTT.ShowCodeWithValue = false;
            this.autoLoaiPTTT.Size = new System.Drawing.Size(489, 22);
            this.autoLoaiPTTT.splitChar = '@';
            this.autoLoaiPTTT.splitCharIDAndCode = '#';
            this.autoLoaiPTTT.TabIndex = 12;
            this.autoLoaiPTTT.TakeCode = false;
            this.autoLoaiPTTT.txtMyCode = null;
            this.autoLoaiPTTT.txtMyCode_Edit = null;
            this.autoLoaiPTTT.txtMyID = null;
            this.autoLoaiPTTT.txtMyID_Edit = null;
            this.autoLoaiPTTT.txtMyName = null;
            this.autoLoaiPTTT.txtMyName_Edit = null;
            this.autoLoaiPTTT.txtNext = null;
            this.autoLoaiPTTT.txtNext1 = null;
            // 
            // autoBSphu
            // 
            this.autoBSphu._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBSphu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSphu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBSphu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoBSphu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBSphu.AutoCompleteList")));
            this.autoBSphu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBSphu.buildShortcut = false;
            this.autoBSphu.CaseSensitive = false;
            this.autoBSphu.CompareNoID = true;
            this.autoBSphu.DefaultCode = "-1";
            this.autoBSphu.DefaultID = "-1";
            this.autoBSphu.DisplayType = 0;
            this.autoBSphu.Drug_ID = null;
            this.autoBSphu.ExtraWidth = -300;
            this.autoBSphu.FillValueAfterSelect = false;
            this.autoBSphu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSphu.Location = new System.Drawing.Point(703, 149);
            this.autoBSphu.MaxHeight = 289;
            this.autoBSphu.MinTypedCharacters = 2;
            this.autoBSphu.MyCode = "-1";
            this.autoBSphu.MyID = "-1";
            this.autoBSphu.MyText = "";
            this.autoBSphu.MyTextOnly = "";
            this.autoBSphu.Name = "autoBSphu";
            this.autoBSphu.RaiseEvent = true;
            this.autoBSphu.RaiseEventEnter = true;
            this.autoBSphu.RaiseEventEnterWhenEmpty = true;
            this.autoBSphu.SelectedIndex = -1;
            this.autoBSphu.Size = new System.Drawing.Size(305, 22);
            this.autoBSphu.splitChar = '@';
            this.autoBSphu.splitCharIDAndCode = '#';
            this.autoBSphu.TabIndex = 22;
            this.autoBSphu.TakeCode = false;
            this.autoBSphu.txtMyCode = null;
            this.autoBSphu.txtMyCode_Edit = null;
            this.autoBSphu.txtMyID = null;
            this.autoBSphu.txtMyID_Edit = null;
            this.autoBSphu.txtMyName = null;
            this.autoBSphu.txtMyName_Edit = null;
            this.autoBSphu.txtNext = null;
            // 
            // autoBSGayme
            // 
            this.autoBSGayme._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBSGayme._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSGayme._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBSGayme.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBSGayme.AutoCompleteList")));
            this.autoBSGayme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBSGayme.buildShortcut = false;
            this.autoBSGayme.CaseSensitive = false;
            this.autoBSGayme.CompareNoID = true;
            this.autoBSGayme.DefaultCode = "-1";
            this.autoBSGayme.DefaultID = "-1";
            this.autoBSGayme.DisplayType = 0;
            this.autoBSGayme.Drug_ID = null;
            this.autoBSGayme.ExtraWidth = 150;
            this.autoBSGayme.FillValueAfterSelect = false;
            this.autoBSGayme.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSGayme.Location = new System.Drawing.Point(410, 148);
            this.autoBSGayme.MaxHeight = 289;
            this.autoBSGayme.MinTypedCharacters = 2;
            this.autoBSGayme.MyCode = "-1";
            this.autoBSGayme.MyID = "-1";
            this.autoBSGayme.MyText = "";
            this.autoBSGayme.MyTextOnly = "";
            this.autoBSGayme.Name = "autoBSGayme";
            this.autoBSGayme.RaiseEvent = true;
            this.autoBSGayme.RaiseEventEnter = true;
            this.autoBSGayme.RaiseEventEnterWhenEmpty = true;
            this.autoBSGayme.SelectedIndex = -1;
            this.autoBSGayme.Size = new System.Drawing.Size(188, 22);
            this.autoBSGayme.splitChar = '@';
            this.autoBSGayme.splitCharIDAndCode = '#';
            this.autoBSGayme.TabIndex = 21;
            this.autoBSGayme.TakeCode = false;
            this.autoBSGayme.txtMyCode = null;
            this.autoBSGayme.txtMyCode_Edit = null;
            this.autoBSGayme.txtMyID = null;
            this.autoBSGayme.txtMyID_Edit = null;
            this.autoBSGayme.txtMyName = null;
            this.autoBSGayme.txtMyName_Edit = null;
            this.autoBSGayme.txtNext = null;
            // 
            // autoBSPhauthuat
            // 
            this.autoBSPhauthuat._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBSPhauthuat._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSPhauthuat._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBSPhauthuat.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBSPhauthuat.AutoCompleteList")));
            this.autoBSPhauthuat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBSPhauthuat.buildShortcut = false;
            this.autoBSPhauthuat.CaseSensitive = false;
            this.autoBSPhauthuat.CompareNoID = true;
            this.autoBSPhauthuat.DefaultCode = "-1";
            this.autoBSPhauthuat.DefaultID = "-1";
            this.autoBSPhauthuat.DisplayType = 0;
            this.autoBSPhauthuat.Drug_ID = null;
            this.autoBSPhauthuat.ExtraWidth = 300;
            this.autoBSPhauthuat.FillValueAfterSelect = false;
            this.autoBSPhauthuat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBSPhauthuat.Location = new System.Drawing.Point(109, 149);
            this.autoBSPhauthuat.MaxHeight = 289;
            this.autoBSPhauthuat.MinTypedCharacters = 2;
            this.autoBSPhauthuat.MyCode = "-1";
            this.autoBSPhauthuat.MyID = "-1";
            this.autoBSPhauthuat.MyText = "";
            this.autoBSPhauthuat.MyTextOnly = "";
            this.autoBSPhauthuat.Name = "autoBSPhauthuat";
            this.autoBSPhauthuat.RaiseEvent = true;
            this.autoBSPhauthuat.RaiseEventEnter = true;
            this.autoBSPhauthuat.RaiseEventEnterWhenEmpty = true;
            this.autoBSPhauthuat.SelectedIndex = -1;
            this.autoBSPhauthuat.Size = new System.Drawing.Size(202, 22);
            this.autoBSPhauthuat.splitChar = '@';
            this.autoBSPhauthuat.splitCharIDAndCode = '#';
            this.autoBSPhauthuat.TabIndex = 20;
            this.autoBSPhauthuat.TakeCode = false;
            this.autoBSPhauthuat.txtMyCode = null;
            this.autoBSPhauthuat.txtMyCode_Edit = null;
            this.autoBSPhauthuat.txtMyID = null;
            this.autoBSPhauthuat.txtMyID_Edit = null;
            this.autoBSPhauthuat.txtMyName = null;
            this.autoBSPhauthuat.txtMyName_Edit = null;
            this.autoBSPhauthuat.txtNext = null;
            // 
            // autoLydotuvong
            // 
            this.autoLydotuvong._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLydotuvong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotuvong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLydotuvong.AddValues = true;
            this.autoLydotuvong.AllowMultiline = false;
            this.autoLydotuvong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLydotuvong.AutoCompleteList")));
            this.autoLydotuvong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLydotuvong.buildShortcut = false;
            this.autoLydotuvong.CaseSensitive = false;
            this.autoLydotuvong.CompareNoID = true;
            this.autoLydotuvong.DefaultCode = "-1";
            this.autoLydotuvong.DefaultID = "-1";
            this.autoLydotuvong.Drug_ID = null;
            this.autoLydotuvong.ExtraWidth = 0;
            this.autoLydotuvong.FillValueAfterSelect = false;
            this.autoLydotuvong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotuvong.LOAI_DANHMUC = "LYDOTUVONG";
            this.autoLydotuvong.Location = new System.Drawing.Point(109, 703);
            this.autoLydotuvong.MaxHeight = 200;
            this.autoLydotuvong.MinTypedCharacters = 2;
            this.autoLydotuvong.MyCode = "-1";
            this.autoLydotuvong.MyID = "-1";
            this.autoLydotuvong.Name = "autoLydotuvong";
            this.autoLydotuvong.RaiseEvent = false;
            this.autoLydotuvong.RaiseEventEnter = false;
            this.autoLydotuvong.RaiseEventEnterWhenEmpty = false;
            this.autoLydotuvong.SelectedIndex = -1;
            this.autoLydotuvong.ShowCodeWithValue = false;
            this.autoLydotuvong.Size = new System.Drawing.Size(202, 22);
            this.autoLydotuvong.splitChar = '@';
            this.autoLydotuvong.splitCharIDAndCode = '#';
            this.autoLydotuvong.TabIndex = 36;
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
            // autoLydotaibien
            // 
            this.autoLydotaibien._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLydotaibien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotaibien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLydotaibien.AddValues = true;
            this.autoLydotaibien.AllowMultiline = false;
            this.autoLydotaibien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLydotaibien.AutoCompleteList")));
            this.autoLydotaibien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLydotaibien.buildShortcut = false;
            this.autoLydotaibien.CaseSensitive = false;
            this.autoLydotaibien.CompareNoID = true;
            this.autoLydotaibien.DefaultCode = "-1";
            this.autoLydotaibien.DefaultID = "-1";
            this.autoLydotaibien.Drug_ID = null;
            this.autoLydotaibien.ExtraWidth = 0;
            this.autoLydotaibien.FillValueAfterSelect = false;
            this.autoLydotaibien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydotaibien.LOAI_DANHMUC = "LYDOTAIBIEN";
            this.autoLydotaibien.Location = new System.Drawing.Point(109, 676);
            this.autoLydotaibien.MaxHeight = 200;
            this.autoLydotaibien.MinTypedCharacters = 2;
            this.autoLydotaibien.MyCode = "-1";
            this.autoLydotaibien.MyID = "-1";
            this.autoLydotaibien.Name = "autoLydotaibien";
            this.autoLydotaibien.RaiseEvent = false;
            this.autoLydotaibien.RaiseEventEnter = false;
            this.autoLydotaibien.RaiseEventEnterWhenEmpty = false;
            this.autoLydotaibien.SelectedIndex = -1;
            this.autoLydotaibien.ShowCodeWithValue = false;
            this.autoLydotaibien.Size = new System.Drawing.Size(202, 22);
            this.autoLydotaibien.splitChar = '@';
            this.autoLydotaibien.splitCharIDAndCode = '#';
            this.autoLydotaibien.TabIndex = 34;
            this.autoLydotaibien.TakeCode = false;
            this.autoLydotaibien.txtMyCode = null;
            this.autoLydotaibien.txtMyCode_Edit = null;
            this.autoLydotaibien.txtMyID = null;
            this.autoLydotaibien.txtMyID_Edit = null;
            this.autoLydotaibien.txtMyName = null;
            this.autoLydotaibien.txtMyName_Edit = null;
            this.autoLydotaibien.txtNext = null;
            this.autoLydotaibien.txtNext1 = null;
            // 
            // txtPhuongPhapPT
            // 
            this.txtPhuongPhapPT._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPhuongPhapPT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapPT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhuongPhapPT.AddValues = true;
            this.txtPhuongPhapPT.AllowMultiline = false;
            this.txtPhuongPhapPT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhuongPhapPT.AutoCompleteList")));
            this.txtPhuongPhapPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhuongPhapPT.buildShortcut = false;
            this.txtPhuongPhapPT.CaseSensitive = false;
            this.txtPhuongPhapPT.CompareNoID = true;
            this.txtPhuongPhapPT.DefaultCode = "-1";
            this.txtPhuongPhapPT.DefaultID = "-1";
            this.txtPhuongPhapPT.Drug_ID = null;
            this.txtPhuongPhapPT.ExtraWidth = 0;
            this.txtPhuongPhapPT.FillValueAfterSelect = false;
            this.txtPhuongPhapPT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapPT.LOAI_DANHMUC = "CACHTHUC_PTTT";
            this.txtPhuongPhapPT.Location = new System.Drawing.Point(109, 125);
            this.txtPhuongPhapPT.MaxHeight = 200;
            this.txtPhuongPhapPT.MinTypedCharacters = 2;
            this.txtPhuongPhapPT.MyCode = "-1";
            this.txtPhuongPhapPT.MyID = "-1";
            this.txtPhuongPhapPT.Name = "txtPhuongPhapPT";
            this.txtPhuongPhapPT.RaiseEvent = false;
            this.txtPhuongPhapPT.RaiseEventEnter = false;
            this.txtPhuongPhapPT.RaiseEventEnterWhenEmpty = false;
            this.txtPhuongPhapPT.SelectedIndex = -1;
            this.txtPhuongPhapPT.ShowCodeWithValue = false;
            this.txtPhuongPhapPT.Size = new System.Drawing.Size(202, 22);
            this.txtPhuongPhapPT.splitChar = '@';
            this.txtPhuongPhapPT.splitCharIDAndCode = '#';
            this.txtPhuongPhapPT.TabIndex = 17;
            this.txtPhuongPhapPT.TakeCode = false;
            this.txtPhuongPhapPT.txtMyCode = null;
            this.txtPhuongPhapPT.txtMyCode_Edit = null;
            this.txtPhuongPhapPT.txtMyID = null;
            this.txtPhuongPhapPT.txtMyID_Edit = null;
            this.txtPhuongPhapPT.txtMyName = null;
            this.txtPhuongPhapPT.txtMyName_Edit = null;
            this.txtPhuongPhapPT.txtNext = null;
            this.txtPhuongPhapPT.txtNext1 = null;
            // 
            // txtPhuongPhapVoCam
            // 
            this.txtPhuongPhapVoCam._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPhuongPhapVoCam._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapVoCam._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhuongPhapVoCam.AddValues = true;
            this.txtPhuongPhapVoCam.AllowMultiline = false;
            this.txtPhuongPhapVoCam.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhuongPhapVoCam.AutoCompleteList")));
            this.txtPhuongPhapVoCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhuongPhapVoCam.buildShortcut = false;
            this.txtPhuongPhapVoCam.CaseSensitive = false;
            this.txtPhuongPhapVoCam.CompareNoID = true;
            this.txtPhuongPhapVoCam.DefaultCode = "-1";
            this.txtPhuongPhapVoCam.DefaultID = "-1";
            this.txtPhuongPhapVoCam.Drug_ID = null;
            this.txtPhuongPhapVoCam.ExtraWidth = 0;
            this.txtPhuongPhapVoCam.FillValueAfterSelect = false;
            this.txtPhuongPhapVoCam.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapVoCam.LOAI_DANHMUC = "PHUONGPHAPVOCAM";
            this.txtPhuongPhapVoCam.Location = new System.Drawing.Point(410, 125);
            this.txtPhuongPhapVoCam.MaxHeight = 200;
            this.txtPhuongPhapVoCam.MinTypedCharacters = 2;
            this.txtPhuongPhapVoCam.MyCode = "-1";
            this.txtPhuongPhapVoCam.MyID = "-1";
            this.txtPhuongPhapVoCam.Name = "txtPhuongPhapVoCam";
            this.txtPhuongPhapVoCam.RaiseEvent = false;
            this.txtPhuongPhapVoCam.RaiseEventEnter = false;
            this.txtPhuongPhapVoCam.RaiseEventEnterWhenEmpty = false;
            this.txtPhuongPhapVoCam.SelectedIndex = -1;
            this.txtPhuongPhapVoCam.ShowCodeWithValue = false;
            this.txtPhuongPhapVoCam.Size = new System.Drawing.Size(188, 22);
            this.txtPhuongPhapVoCam.splitChar = '@';
            this.txtPhuongPhapVoCam.splitCharIDAndCode = '#';
            this.txtPhuongPhapVoCam.TabIndex = 18;
            this.txtPhuongPhapVoCam.TakeCode = false;
            this.txtPhuongPhapVoCam.txtMyCode = null;
            this.txtPhuongPhapVoCam.txtMyCode_Edit = null;
            this.txtPhuongPhapVoCam.txtMyID = null;
            this.txtPhuongPhapVoCam.txtMyID_Edit = null;
            this.txtPhuongPhapVoCam.txtMyName = null;
            this.txtPhuongPhapVoCam.txtMyName_Edit = null;
            this.txtPhuongPhapVoCam.txtNext = null;
            this.txtPhuongPhapVoCam.txtNext1 = null;
            // 
            // txtLuocDoPhauThuat
            // 
            this.txtLuocDoPhauThuat._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLuocDoPhauThuat._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLuocDoPhauThuat._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLuocDoPhauThuat.AddValues = true;
            this.txtLuocDoPhauThuat.AllowMultiline = false;
            this.txtLuocDoPhauThuat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLuocDoPhauThuat.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLuocDoPhauThuat.AutoCompleteList")));
            this.txtLuocDoPhauThuat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLuocDoPhauThuat.buildShortcut = false;
            this.txtLuocDoPhauThuat.CaseSensitive = false;
            this.txtLuocDoPhauThuat.CompareNoID = true;
            this.txtLuocDoPhauThuat.DefaultCode = "-1";
            this.txtLuocDoPhauThuat.DefaultID = "-1";
            this.txtLuocDoPhauThuat.Drug_ID = null;
            this.txtLuocDoPhauThuat.ExtraWidth = -300;
            this.txtLuocDoPhauThuat.FillValueAfterSelect = false;
            this.txtLuocDoPhauThuat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLuocDoPhauThuat.LOAI_DANHMUC = "LUOCDOPHAUTHUAT";
            this.txtLuocDoPhauThuat.Location = new System.Drawing.Point(703, 125);
            this.txtLuocDoPhauThuat.MaxHeight = 200;
            this.txtLuocDoPhauThuat.MinTypedCharacters = 2;
            this.txtLuocDoPhauThuat.MyCode = "-1";
            this.txtLuocDoPhauThuat.MyID = "-1";
            this.txtLuocDoPhauThuat.Name = "txtLuocDoPhauThuat";
            this.txtLuocDoPhauThuat.RaiseEvent = false;
            this.txtLuocDoPhauThuat.RaiseEventEnter = false;
            this.txtLuocDoPhauThuat.RaiseEventEnterWhenEmpty = false;
            this.txtLuocDoPhauThuat.SelectedIndex = -1;
            this.txtLuocDoPhauThuat.ShowCodeWithValue = false;
            this.txtLuocDoPhauThuat.Size = new System.Drawing.Size(305, 22);
            this.txtLuocDoPhauThuat.splitChar = '@';
            this.txtLuocDoPhauThuat.splitCharIDAndCode = '#';
            this.txtLuocDoPhauThuat.TabIndex = 19;
            this.txtLuocDoPhauThuat.TakeCode = false;
            this.txtLuocDoPhauThuat.txtMyCode = null;
            this.txtLuocDoPhauThuat.txtMyCode_Edit = null;
            this.txtLuocDoPhauThuat.txtMyID = null;
            this.txtLuocDoPhauThuat.txtMyID_Edit = null;
            this.txtLuocDoPhauThuat.txtMyName = null;
            this.txtLuocDoPhauThuat.txtMyName_Edit = null;
            this.txtLuocDoPhauThuat.txtNext = null;
            this.txtLuocDoPhauThuat.txtNext1 = null;
            // 
            // txtChanDoanTruocPT
            // 
            this.txtChanDoanTruocPT._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtChanDoanTruocPT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoanTruocPT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChanDoanTruocPT.AddValues = true;
            this.txtChanDoanTruocPT.AllowMultiline = false;
            this.txtChanDoanTruocPT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtChanDoanTruocPT.AutoCompleteList")));
            this.txtChanDoanTruocPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChanDoanTruocPT.buildShortcut = false;
            this.txtChanDoanTruocPT.CaseSensitive = false;
            this.txtChanDoanTruocPT.CompareNoID = true;
            this.txtChanDoanTruocPT.DefaultCode = "-1";
            this.txtChanDoanTruocPT.DefaultID = "-1";
            this.txtChanDoanTruocPT.Drug_ID = null;
            this.txtChanDoanTruocPT.ExtraWidth = 0;
            this.txtChanDoanTruocPT.FillValueAfterSelect = false;
            this.txtChanDoanTruocPT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoanTruocPT.LOAI_DANHMUC = "CHANDOAN";
            this.txtChanDoanTruocPT.Location = new System.Drawing.Point(109, 101);
            this.txtChanDoanTruocPT.MaxHeight = 200;
            this.txtChanDoanTruocPT.MinTypedCharacters = 2;
            this.txtChanDoanTruocPT.MyCode = "-1";
            this.txtChanDoanTruocPT.MyID = "-1";
            this.txtChanDoanTruocPT.Name = "txtChanDoanTruocPT";
            this.txtChanDoanTruocPT.RaiseEvent = false;
            this.txtChanDoanTruocPT.RaiseEventEnter = false;
            this.txtChanDoanTruocPT.RaiseEventEnterWhenEmpty = false;
            this.txtChanDoanTruocPT.SelectedIndex = -1;
            this.txtChanDoanTruocPT.ShowCodeWithValue = false;
            this.txtChanDoanTruocPT.Size = new System.Drawing.Size(202, 22);
            this.txtChanDoanTruocPT.splitChar = '@';
            this.txtChanDoanTruocPT.splitCharIDAndCode = '#';
            this.txtChanDoanTruocPT.TabIndex = 13;
            this.txtChanDoanTruocPT.TakeCode = false;
            this.txtChanDoanTruocPT.txtMyCode = null;
            this.txtChanDoanTruocPT.txtMyCode_Edit = null;
            this.txtChanDoanTruocPT.txtMyID = null;
            this.txtChanDoanTruocPT.txtMyID_Edit = null;
            this.txtChanDoanTruocPT.txtMyName = null;
            this.txtChanDoanTruocPT.txtMyName_Edit = null;
            this.txtChanDoanTruocPT.txtNext = null;
            this.txtChanDoanTruocPT.txtNext1 = null;
            // 
            // txtChanDoanSauPT
            // 
            this.txtChanDoanSauPT._backcolor = System.Drawing.SystemColors.Control;
            this.txtChanDoanSauPT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoanSauPT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChanDoanSauPT.AddValues = true;
            this.txtChanDoanSauPT.AllowMultiline = false;
            this.txtChanDoanSauPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChanDoanSauPT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtChanDoanSauPT.AutoCompleteList")));
            this.txtChanDoanSauPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChanDoanSauPT.buildShortcut = false;
            this.txtChanDoanSauPT.CaseSensitive = false;
            this.txtChanDoanSauPT.CompareNoID = true;
            this.txtChanDoanSauPT.DefaultCode = "-1";
            this.txtChanDoanSauPT.DefaultID = "-1";
            this.txtChanDoanSauPT.Drug_ID = null;
            this.txtChanDoanSauPT.ExtraWidth = 0;
            this.txtChanDoanSauPT.FillValueAfterSelect = false;
            this.txtChanDoanSauPT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoanSauPT.LOAI_DANHMUC = "CHANDOAN";
            this.txtChanDoanSauPT.Location = new System.Drawing.Point(409, 101);
            this.txtChanDoanSauPT.MaxHeight = 200;
            this.txtChanDoanSauPT.MinTypedCharacters = 2;
            this.txtChanDoanSauPT.MyCode = "-1";
            this.txtChanDoanSauPT.MyID = "-1";
            this.txtChanDoanSauPT.Name = "txtChanDoanSauPT";
            this.txtChanDoanSauPT.RaiseEvent = false;
            this.txtChanDoanSauPT.RaiseEventEnter = false;
            this.txtChanDoanSauPT.RaiseEventEnterWhenEmpty = false;
            this.txtChanDoanSauPT.SelectedIndex = -1;
            this.txtChanDoanSauPT.ShowCodeWithValue = false;
            this.txtChanDoanSauPT.Size = new System.Drawing.Size(599, 22);
            this.txtChanDoanSauPT.splitChar = '@';
            this.txtChanDoanSauPT.splitCharIDAndCode = '#';
            this.txtChanDoanSauPT.TabIndex = 14;
            this.txtChanDoanSauPT.TakeCode = false;
            this.txtChanDoanSauPT.txtMyCode = null;
            this.txtChanDoanSauPT.txtMyCode_Edit = null;
            this.txtChanDoanSauPT.txtMyID = null;
            this.txtChanDoanSauPT.txtMyID_Edit = null;
            this.txtChanDoanSauPT.txtMyName = null;
            this.txtChanDoanSauPT.txtMyName_Edit = null;
            this.txtChanDoanSauPT.txtNext = null;
            this.txtChanDoanSauPT.txtNext1 = null;
            // 
            // txtTrinhTuPhauThat
            // 
            this.txtTrinhTuPhauThat._backcolor = System.Drawing.SystemColors.Control;
            this.txtTrinhTuPhauThat._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrinhTuPhauThat._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTrinhTuPhauThat.AddValues = true;
            this.txtTrinhTuPhauThat.AllowMultiline = false;
            this.txtTrinhTuPhauThat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrinhTuPhauThat.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTrinhTuPhauThat.AutoCompleteList")));
            this.txtTrinhTuPhauThat.buildShortcut = false;
            this.txtTrinhTuPhauThat.CaseSensitive = false;
            this.txtTrinhTuPhauThat.CompareNoID = true;
            this.txtTrinhTuPhauThat.DefaultCode = "";
            this.txtTrinhTuPhauThat.DefaultID = "";
            this.txtTrinhTuPhauThat.Drug_ID = null;
            this.txtTrinhTuPhauThat.ExtraWidth = 0;
            this.txtTrinhTuPhauThat.FillValueAfterSelect = false;
            this.txtTrinhTuPhauThat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrinhTuPhauThat.LOAI_DANHMUC = "";
            this.txtTrinhTuPhauThat.Location = new System.Drawing.Point(109, 561);
            this.txtTrinhTuPhauThat.MaxHeight = 150;
            this.txtTrinhTuPhauThat.MinTypedCharacters = 2;
            this.txtTrinhTuPhauThat.Multiline = true;
            this.txtTrinhTuPhauThat.MyCode = "";
            this.txtTrinhTuPhauThat.MyID = "-1";
            this.txtTrinhTuPhauThat.Name = "txtTrinhTuPhauThat";
            this.txtTrinhTuPhauThat.RaiseEvent = false;
            this.txtTrinhTuPhauThat.RaiseEventEnter = false;
            this.txtTrinhTuPhauThat.RaiseEventEnterWhenEmpty = false;
            this.txtTrinhTuPhauThat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTrinhTuPhauThat.SelectedIndex = -1;
            this.txtTrinhTuPhauThat.ShowCodeWithValue = false;
            this.txtTrinhTuPhauThat.Size = new System.Drawing.Size(613, 109);
            this.txtTrinhTuPhauThat.splitChar = '@';
            this.txtTrinhTuPhauThat.splitCharIDAndCode = '#';
            this.txtTrinhTuPhauThat.TabIndex = 32;
            this.txtTrinhTuPhauThat.TakeCode = false;
            this.txtTrinhTuPhauThat.txtMyCode = null;
            this.txtTrinhTuPhauThat.txtMyCode_Edit = null;
            this.txtTrinhTuPhauThat.txtMyID = null;
            this.txtTrinhTuPhauThat.txtMyID_Edit = null;
            this.txtTrinhTuPhauThat.txtMyName = null;
            this.txtTrinhTuPhauThat.txtMyName_Edit = null;
            this.txtTrinhTuPhauThat.txtNext = null;
            this.txtTrinhTuPhauThat.txtNext1 = null;
            // 
            // chkNgayCatChi
            // 
            this.chkNgayCatChi.AutoSize = true;
            this.chkNgayCatChi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNgayCatChi.Location = new System.Drawing.Point(276, 507);
            this.chkNgayCatChi.Name = "chkNgayCatChi";
            this.chkNgayCatChi.Size = new System.Drawing.Size(100, 20);
            this.chkNgayCatChi.TabIndex = 29;
            this.chkNgayCatChi.Text = "Ngày cắt chỉ";
            this.chkNgayCatChi.UseVisualStyleBackColor = true;
            this.chkNgayCatChi.CheckedChanged += new System.EventHandler(this.chkNgayCatChi_CheckedChanged);
            // 
            // chkNgayRut
            // 
            this.chkNgayRut.AutoSize = true;
            this.chkNgayRut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNgayRut.Location = new System.Drawing.Point(26, 507);
            this.chkNgayRut.Name = "chkNgayRut";
            this.chkNgayRut.Size = new System.Drawing.Size(76, 20);
            this.chkNgayRut.TabIndex = 27;
            this.chkNgayRut.Text = "Ngày rút";
            this.chkNgayRut.UseVisualStyleBackColor = true;
            this.chkNgayRut.CheckedChanged += new System.EventHandler(this.chkNgayRut_CheckedChanged);
            // 
            // chkPTTT_KetThuc
            // 
            this.chkPTTT_KetThuc.AutoSize = true;
            this.chkPTTT_KetThuc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPTTT_KetThuc.Location = new System.Drawing.Point(295, 52);
            this.chkPTTT_KetThuc.Name = "chkPTTT_KetThuc";
            this.chkPTTT_KetThuc.Size = new System.Drawing.Size(108, 20);
            this.chkPTTT_KetThuc.TabIndex = 5;
            this.chkPTTT_KetThuc.TabStop = false;
            this.chkPTTT_KetThuc.Text = "PTTT kết thúc";
            this.chkPTTT_KetThuc.UseVisualStyleBackColor = true;
            // 
            // txtphu
            // 
            this.txtphu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphu.ForeColor = System.Drawing.Color.Red;
            this.txtphu.Location = new System.Drawing.Point(604, 149);
            this.txtphu.Name = "txtphu";
            this.txtphu.Size = new System.Drawing.Size(93, 15);
            this.txtphu.TabIndex = 561;
            this.txtphu.Text = "PT viên phụ";
            this.txtphu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTaibien
            // 
            this.chkTaibien.AutoSize = true;
            this.chkTaibien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTaibien.Location = new System.Drawing.Point(1, 676);
            this.chkTaibien.Name = "chkTaibien";
            this.chkTaibien.Size = new System.Drawing.Size(105, 20);
            this.chkTaibien.TabIndex = 33;
            this.chkTaibien.Text = "Lý do tai biến";
            this.chkTaibien.UseVisualStyleBackColor = true;
            this.chkTaibien.CheckedChanged += new System.EventHandler(this.chkTaibien_CheckedChanged);
            // 
            // dtNgayGioTuVong
            // 
            this.dtNgayGioTuVong.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtNgayGioTuVong.Enabled = false;
            this.dtNgayGioTuVong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayGioTuVong.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayGioTuVong.Location = new System.Drawing.Point(360, 703);
            this.dtNgayGioTuVong.Name = "dtNgayGioTuVong";
            this.dtNgayGioTuVong.ShowUpDown = true;
            this.dtNgayGioTuVong.Size = new System.Drawing.Size(156, 22);
            this.dtNgayGioTuVong.TabIndex = 37;
            // 
            // chkTuvong
            // 
            this.chkTuvong.AutoSize = true;
            this.chkTuvong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTuvong.Location = new System.Drawing.Point(0, 705);
            this.chkTuvong.Name = "chkTuvong";
            this.chkTuvong.Size = new System.Drawing.Size(106, 20);
            this.chkTuvong.TabIndex = 35;
            this.chkTuvong.Text = "Lý do tử vong";
            this.chkTuvong.UseVisualStyleBackColor = true;
            this.chkTuvong.CheckedChanged += new System.EventHandler(this.chkTuvong_CheckedChanged);
            // 
            // dtpNgayGioKetThucPTTT
            // 
            this.dtpNgayGioKetThucPTTT.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtpNgayGioKetThucPTTT.Enabled = false;
            this.dtpNgayGioKetThucPTTT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayGioKetThucPTTT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayGioKetThucPTTT.Location = new System.Drawing.Point(409, 52);
            this.dtpNgayGioKetThucPTTT.Name = "dtpNgayGioKetThucPTTT";
            this.dtpNgayGioKetThucPTTT.ShowUpDown = true;
            this.dtpNgayGioKetThucPTTT.Size = new System.Drawing.Size(189, 22);
            this.dtpNgayGioKetThucPTTT.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radNgoaiTru);
            this.panel1.Controls.Add(this.radNoiTru);
            this.panel1.Enabled = false;
            this.panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(520, 699);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 32);
            this.panel1.TabIndex = 549;
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.AutoSize = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(95, 3);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(78, 20);
            this.radNgoaiTru.TabIndex = 1;
            this.radNgoaiTru.Text = "Ngoại trú";
            this.radNgoaiTru.UseVisualStyleBackColor = true;
            // 
            // radNoiTru
            // 
            this.radNoiTru.AutoSize = true;
            this.radNoiTru.Checked = true;
            this.radNoiTru.Location = new System.Drawing.Point(9, 5);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(64, 20);
            this.radNoiTru.TabIndex = 0;
            this.radNoiTru.TabStop = true;
            this.radNoiTru.Text = "Nội trú";
            this.radNoiTru.UseVisualStyleBackColor = true;
            // 
            // txtDanLuu
            // 
            this.txtDanLuu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDanLuu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDanLuu.ForeColor = System.Drawing.Color.Black;
            this.txtDanLuu.Location = new System.Drawing.Point(109, 447);
            this.txtDanLuu.Name = "txtDanLuu";
            this.txtDanLuu.Size = new System.Drawing.Size(900, 22);
            this.txtDanLuu.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(604, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 536;
            this.label3.Text = "Lược đồ PT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayRut
            // 
            this.dtpNgayRut.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtpNgayRut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayRut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayRut.Location = new System.Drawing.Point(109, 505);
            this.dtpNgayRut.Name = "dtpNgayRut";
            this.dtpNgayRut.ShowUpDown = true;
            this.dtpNgayRut.Size = new System.Drawing.Size(133, 22);
            this.dtpNgayRut.TabIndex = 28;
            // 
            // txtKhac
            // 
            this.txtKhac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKhac.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhac.ForeColor = System.Drawing.Color.Black;
            this.txtKhac.Location = new System.Drawing.Point(109, 532);
            this.txtKhac.Name = "txtKhac";
            this.txtKhac.Size = new System.Drawing.Size(612, 22);
            this.txtKhac.TabIndex = 31;
            // 
            // txtBac
            // 
            this.txtBac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBac.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBac.ForeColor = System.Drawing.Color.Black;
            this.txtBac.Location = new System.Drawing.Point(109, 476);
            this.txtBac.Name = "txtBac";
            this.txtBac.Size = new System.Drawing.Size(899, 22);
            this.txtBac.TabIndex = 26;
            // 
            // dtNgayPhauThuat
            // 
            this.dtNgayPhauThuat.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtNgayPhauThuat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayPhauThuat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayPhauThuat.Location = new System.Drawing.Point(109, 52);
            this.dtNgayPhauThuat.Name = "dtNgayPhauThuat";
            this.dtNgayPhauThuat.ShowUpDown = true;
            this.dtNgayPhauThuat.Size = new System.Drawing.Size(171, 22);
            this.dtNgayPhauThuat.TabIndex = 10;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(-1, 536);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 18);
            this.label22.TabIndex = 32;
            this.label22.Text = "Khác";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(-1, 580);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(108, 18);
            this.label21.TabIndex = 31;
            this.label21.Text = "Trình tự PT";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(-1, 480);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 18);
            this.label15.TabIndex = 27;
            this.label15.Text = "Bấc";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(-1, 451);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 18);
            this.label14.TabIndex = 26;
            this.label14.Text = "Dẫn lưu";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(313, 149);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 15);
            this.label13.TabIndex = 25;
            this.label13.Text = "B.sĩ Gây mê";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(-1, 149);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 18);
            this.label12.TabIndex = 24;
            this.label12.Text = "PT viên chính";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(313, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 15);
            this.label11.TabIndex = 23;
            this.label11.Text = "P.pháp VC";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(-1, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 18);
            this.label10.TabIndex = 22;
            this.label10.Text = "Loại PT/TT";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(-1, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 18);
            this.label9.TabIndex = 21;
            this.label9.Text = "Phương pháp PT\r\n";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(313, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "CĐ sau PT";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(-1, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "CĐ trước PT";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(-1, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "PTTT lúc";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayCatChi
            // 
            this.dtpNgayCatChi.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtpNgayCatChi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayCatChi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayCatChi.Location = new System.Drawing.Point(378, 505);
            this.dtpNgayCatChi.Name = "dtpNgayCatChi";
            this.dtpNgayCatChi.ShowUpDown = true;
            this.dtpNgayCatChi.Size = new System.Drawing.Size(202, 22);
            this.dtpNgayCatChi.TabIndex = 30;
            // 
            // grd_bspt
            // 
            this.grd_bspt.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_bspt.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grd_bspt_DesignTimeLayout.LayoutString = resources.GetString("grd_bspt_DesignTimeLayout.LayoutString");
            this.grd_bspt.DesignTimeLayout = grd_bspt_DesignTimeLayout;
            this.grd_bspt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_bspt.GroupByBoxVisible = false;
            this.grd_bspt.Location = new System.Drawing.Point(109, 175);
            this.grd_bspt.Name = "grd_bspt";
            this.grd_bspt.Size = new System.Drawing.Size(202, 128);
            this.grd_bspt.TabIndex = 571;
            this.grd_bspt.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.chkPreview);
            this.tabPage2.Controls.Add(this.chkHoitruockhixoa);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1018, 771);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cấu hình cá nhân";
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreview.Location = new System.Drawing.Point(28, 207);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(147, 23);
            this.chkPreview.TabIndex = 549;
            this.chkPreview.TabStop = false;
            this.chkPreview.Tag = "noitru_pttt_preview";
            this.chkPreview.Text = "Xem trước khi in?";
            // 
            // chkHoitruockhixoa
            // 
            this.chkHoitruockhixoa.AutoSize = true;
            this.chkHoitruockhixoa.Location = new System.Drawing.Point(28, 23);
            this.chkHoitruockhixoa.Name = "chkHoitruockhixoa";
            this.chkHoitruockhixoa.Size = new System.Drawing.Size(429, 19);
            this.chkHoitruockhixoa.TabIndex = 28;
            this.chkHoitruockhixoa.Tag = "PHIEUPTTT_HOITRUOCKHIXOABACSI";
            this.chkHoitruockhixoa.Text = "Hỏi khi xóa bác sĩ gây mê, bác sĩ phẫu thuật chính, bác sĩ phẫu thuật phụ?";
            this.chkHoitruockhixoa.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdPhieuPTTT);
            this.groupBox1.Controls.Add(this.dtNgayIn);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 618);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 181);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu PTTT";
            // 
            // dtNgayIn
            // 
            this.dtNgayIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayIn.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtNgayIn.Enabled = false;
            this.dtNgayIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayIn.Location = new System.Drawing.Point(58, 152);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(143, 20);
            this.dtNgayIn.TabIndex = 28;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.AutoSize = true;
            this.label26.Enabled = false;
            this.label26.Location = new System.Drawing.Point(9, 155);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 13);
            this.label26.TabIndex = 568;
            this.label26.Text = "Ngày in";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.grpThongTinPhieu);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(384, 799);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ucThongtinnguoibenh_doc_v11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(384, 375);
            this.panel5.TabIndex = 570;
            // 
            // ucThongtinnguoibenh_doc_v11
            // 
            this.ucThongtinnguoibenh_doc_v11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucThongtinnguoibenh_doc_v11.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_doc_v11.Name = "ucThongtinnguoibenh_doc_v11";
            this.ucThongtinnguoibenh_doc_v11.Size = new System.Drawing.Size(384, 375);
            this.ucThongtinnguoibenh_doc_v11.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.cmdScanFinger);
            this.panel2.Controls.Add(this.cmdPrint);
            this.panel2.Controls.Add(this.cmdDelete);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 799);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1410, 52);
            this.panel2.TabIndex = 569;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblStatus.Location = new System.Drawing.Point(397, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(704, 37);
            this.lblStatus.TabIndex = 577;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdScanFinger
            // 
            this.cmdScanFinger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdScanFinger.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdScanFinger.Image = ((System.Drawing.Image)(resources.GetObject("cmdScanFinger.Image")));
            this.cmdScanFinger.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdScanFinger.Location = new System.Drawing.Point(257, 6);
            this.cmdScanFinger.Name = "cmdScanFinger";
            this.cmdScanFinger.Size = new System.Drawing.Size(127, 35);
            this.cmdScanFinger.TabIndex = 576;
            this.cmdScanFinger.TabStop = false;
            this.cmdScanFinger.Text = "Quét vân tay";
            this.cmdScanFinger.Click += new System.EventHandler(this.cmdScanFinger_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.DropDownButton;
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(131, 7);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 32);
            this.cmdPrint.TabIndex = 574;
            this.cmdPrint.TabStop = false;
            this.cmdPrint.Text = "In";
            this.cmdPrint.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDelete.Location = new System.Drawing.Point(5, 7);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(120, 32);
            this.cmdDelete.TabIndex = 573;
            this.cmdDelete.TabStop = false;
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(1131, 6);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(127, 35);
            this.cmdSave.TabIndex = 38;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1264, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(127, 35);
            this.cmdExit.TabIndex = 571;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(1264, 7);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(127, 35);
            this.cmdCancel.TabIndex = 575;
            this.cmdCancel.TabStop = false;
            this.cmdCancel.Text = "Hủy (Esc)";
            this.cmdCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frm_PhieuPTTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 851);
            this.Controls.Add(this.tabThongTin);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm_PhieuPTTT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu phẫu thuật thủ thuật";
            this.Load += new System.EventHandler(this.frm_PhieuPTTT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_PhieuPTTT_KeyDown);
            this.grpThongTinPhieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChiDinh)).EndInit();
            this.ctxInphieu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuPTTT)).EndInit();
            this.tabThongTin.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPTTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDungcuvongtrong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDungcuvongngoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuduonggayme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bsphauthuatphu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bsgm)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bspt)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTinPhieu;
        private System.Windows.Forms.TabControl tabThongTin;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private Janus.Windows.GridEX.GridEX grdPhieuPTTT;
        private System.Windows.Forms.DateTimePicker dtNgayPhauThuat;
        private Janus.Windows.GridEX.EditControls.EditBox txtKhac;
        private Janus.Windows.GridEX.EditControls.EditBox txtBac;
        private System.Windows.Forms.DateTimePicker dtpNgayRut;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtDanLuu;
        private System.Windows.Forms.DateTimePicker dtpNgayCatChi;
        private Janus.Windows.GridEX.EditControls.EditBox txtIdPhieuPTTT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radNgoaiTru;
        private System.Windows.Forms.RadioButton radNoiTru;
        private Janus.Windows.GridEX.GridEX grdChiDinh;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DateTimePicker dtpNgayGioKetThucPTTT;
        private System.Windows.Forms.DateTimePicker dtNgayGioTuVong;
        private System.Windows.Forms.CheckBox chkTuvong;
        private System.Windows.Forms.CheckBox chkTaibien;
        private System.Windows.Forms.Label txtphu;
        private System.Windows.Forms.CheckBox chkPTTT_KetThuc;
        private System.Windows.Forms.CheckBox chkNgayCatChi;
        private System.Windows.Forms.CheckBox chkNgayRut;
        private AutoCompleteTextbox_Danhmucchung txtTrinhTuPhauThat;
        private Janus.Windows.GridEX.GridEX grd_bspt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtNgayIn;
        private Janus.Windows.EditControls.UIButton cmdAddNew;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.Panel panel5;
        private AutoCompleteTextbox_Danhmucchung autoLydotuvong;
        private AutoCompleteTextbox_Danhmucchung autoLydotaibien;
        private AutoCompleteTextbox_Danhmucchung txtPhuongPhapPT;
        private AutoCompleteTextbox_Danhmucchung txtPhuongPhapVoCam;
        private AutoCompleteTextbox_Danhmucchung txtLuocDoPhauThuat;
        private AutoCompleteTextbox_Danhmucchung txtChanDoanTruocPT;
        private AutoCompleteTextbox_Danhmucchung txtChanDoanSauPT;
        private AutoCompleteTextbox autoBSphu;
        private AutoCompleteTextbox autoBSGayme;
        private AutoCompleteTextbox autoBSPhauthuat;
        private System.Windows.Forms.Label label1;
        private AutoCompleteTextbox_Danhmucchung autoLoaiPTTT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private AutoCompleteTextbox autoGiuong;
        private AutoCompleteTextbox autoBuong;
        private AutoCompleteTextbox autoKhoa;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.GridEX.GridEX grd_bsphauthuatphu;
        private Janus.Windows.GridEX.GridEX grd_bsgm;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkHoitruockhixoa;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaphieu;
        private System.Windows.Forms.Label label16;
        private Janus.Windows.EditControls.UICheckBox chkPreview;
        private Janus.Windows.EditControls.UIButton cmdScanFinger;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ContextMenuStrip ctxInphieu;
        private System.Windows.Forms.ToolStripMenuItem mnuInchungnhanPTTT;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuInphieuPTTT;
        private System.Windows.Forms.ToolStripMenuItem mnuCamket;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_doc_v1 ucThongtinnguoibenh_doc_v11;
        private Janus.Windows.GridEX.GridEX grdDungcuvongtrong;
        private Janus.Windows.GridEX.GridEX grdDungcuvongngoai;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private Janus.Windows.GridEX.GridEX grdDieuduonggayme;
        private AutoCompleteTextbox_Danhmucchung autoDungcuvongngoai;
        private AutoCompleteTextbox_Danhmucchung autoDungcuvongtrong;
        private AutoCompleteTextbox autoDieuduonggayme;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cboHinhPTTT;
        private System.Windows.Forms.PictureBox picPTTT;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuPhieutuongtrinhPTTT;
    }
}