using DevExpress.XtraPdfViewer;
using VNS.HIS.UCs;
using VNS.HIS.UI.Forms.Dungchung.UCs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_SingleSign
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SingleSign));
            Janus.Windows.GridEX.GridEXLayout grdEmrDocuments_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.pnlHide = new System.Windows.Forms.Panel();
            this.cboTagFields = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.optEdit = new System.Windows.Forms.RadioButton();
            this.optReadOnly = new System.Windows.Forms.RadioButton();
            this.cmdOpenDoc = new Janus.Windows.EditControls.UIButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optPdfView = new System.Windows.Forms.RadioButton();
            this.optDocView = new System.Windows.Forms.RadioButton();
            this.cmdOpen = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.ctxFunction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuChuyenGay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRestoreDefault_Gay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiTenphieu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAnPhieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuyAnPhieu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuXoaPhieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuyXoaPhieu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMove2Sign = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiTab = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPageEmr = new Janus.Windows.UI.Tab.UITabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdCollapse = new Janus.Windows.EditControls.UIButton();
            this.cmdReset = new Janus.Windows.EditControls.UIButton();
            this.cmdLaythongtin = new Janus.Windows.EditControls.UIButton();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cmdNhaplieu = new Janus.Windows.EditControls.UIButton();
            this.cmdDigitalSign = new Janus.Windows.EditControls.UIButton();
            this.cmdKidientu = new Janus.Windows.EditControls.UIButton();
            this.cmdHuyKyDientu = new Janus.Windows.EditControls.UIButton();
            this.cmdAddWord = new Janus.Windows.EditControls.UIButton();
            this.cmdSaveWord = new Janus.Windows.EditControls.UIButton();
            this.cmdHosoKhac = new Janus.Windows.EditControls.UIButton();
            this.cmdChuyenGay = new Janus.Windows.EditControls.UIButton();
            this.cmdRestoreDefault_Gay = new Janus.Windows.EditControls.UIButton();
            this.cmdAn = new Janus.Windows.EditControls.UIButton();
            this.cmdHienthi = new Janus.Windows.EditControls.UIButton();
            this.cmd_history = new Janus.Windows.EditControls.UIButton();
            this.cmdXoaphieu = new Janus.Windows.EditControls.UIButton();
            this.cmdRestore = new Janus.Windows.EditControls.UIButton();
            this.cmdPrintPreview = new Janus.Windows.EditControls.UIButton();
            this.uiTabPageCauhinh = new Janus.Windows.UI.Tab.UITabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.chkReloadAfterSign = new System.Windows.Forms.CheckBox();
            this.picSignImg = new System.Windows.Forms.PictureBox();
            this.pnlPdf = new System.Windows.Forms.Panel();
            this.flowSignInfor = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdLoad = new Janus.Windows.EditControls.UIButton();
            this.uiButton1 = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.flowKQCLS = new System.Windows.Forms.FlowLayoutPanel();
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxOtherFunctions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT01 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT02 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT03 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT04 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT05 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT06 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT07 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT08 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT09 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT10 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT11 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPT12 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhoiTaoBA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu01BV_BANoikhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBAPhukhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBASanKhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu10BV_BANgoaikhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu15BV_BANgoaitru = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTKBA = new System.Windows.Forms.ToolStripMenuItem();
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiaychungnhanTainanThuongtich = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiayRavien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanTKBA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiayxacnhanquatrinhdieutrinoitru = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiaychungnhanNghiduongthai = new System.Windows.Forms.ToolStripMenuItem();
            this.sảnKhoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_hosotheodoi_sosinh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_giaychungsinh = new System.Windows.Forms.ToolStripMenuItem();
            this.cácPhiếuKhácToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_phieubangiao_nguoibenh_chuyenkhoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_phieu_chapnhan_camket_pttt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_phieukhamthai = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_phieukhamtienme = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_history = new System.Windows.Forms.ToolStripMenuItem();
            this.panelZoom = new System.Windows.Forms.Panel();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.lblZoom = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v1();
            this.cboGay = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboLoaiphieuEmr = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboLoaiphieuHIS = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.chkForced2Download = new System.Windows.Forms.CheckBox();
            this.chkIsMe = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.uiButton5 = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.uiButton4 = new Janus.Windows.EditControls.UIButton();
            this.lblMatheBHYT = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.optOrderbyTime = new System.Windows.Forms.RadioButton();
            this.optOrderbyGay = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.optChuaky = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.optDaky = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.txtNguoiKy = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.txtTenPhieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtmathebhyt = new Janus.Windows.GridEX.EditControls.EditBox();
            this.grdEmrDocuments = new Janus.Windows.GridEX.GridEX();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.ctxFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab)).BeginInit();
            this.uiTab.SuspendLayout();
            this.uiTabPageEmr.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.uiTabPageCauhinh.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignImg)).BeginInit();
            this.panel1.SuspendLayout();
            this.ctxOtherFunctions.SuspendLayout();
            this.panelZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmrDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 10F);
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 958);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Width = 10;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1});
            this.uiStatusBar1.Size = new System.Drawing.Size(1391, 27);
            this.uiStatusBar1.TabIndex = 14;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // pnlHide
            // 
            this.pnlHide.Location = new System.Drawing.Point(9, 466);
            this.pnlHide.Name = "pnlHide";
            this.pnlHide.Size = new System.Drawing.Size(88, 48);
            this.pnlHide.TabIndex = 478;
            this.pnlHide.Visible = false;
            // 
            // cboTagFields
            // 
            this.cboTagFields.FormattingEnabled = true;
            this.cboTagFields.Items.AddRange(new object[] {
            "id_benhnhan",
            "ma_luotkham",
            "ten_benhnhan"});
            this.cboTagFields.Location = new System.Drawing.Point(3, 303);
            this.cboTagFields.Name = "cboTagFields";
            this.cboTagFields.Size = new System.Drawing.Size(82, 23);
            this.cboTagFields.TabIndex = 474;
            this.cboTagFields.SelectedIndexChanged += new System.EventHandler(this.cboTagFields_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.optEdit);
            this.panel3.Controls.Add(this.optReadOnly);
            this.panel3.Location = new System.Drawing.Point(3, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(86, 59);
            this.panel3.TabIndex = 473;
            // 
            // optEdit
            // 
            this.optEdit.AutoSize = true;
            this.optEdit.Checked = true;
            this.optEdit.Location = new System.Drawing.Point(6, 28);
            this.optEdit.Name = "optEdit";
            this.optEdit.Size = new System.Drawing.Size(46, 19);
            this.optEdit.TabIndex = 470;
            this.optEdit.TabStop = true;
            this.optEdit.Text = "Edit";
            this.optEdit.UseVisualStyleBackColor = true;
            this.optEdit.CheckedChanged += new System.EventHandler(this.optEdit_CheckedChanged);
            // 
            // optReadOnly
            // 
            this.optReadOnly.AutoSize = true;
            this.optReadOnly.Location = new System.Drawing.Point(3, 3);
            this.optReadOnly.Name = "optReadOnly";
            this.optReadOnly.Size = new System.Drawing.Size(79, 19);
            this.optReadOnly.TabIndex = 469;
            this.optReadOnly.Text = "ReadOnly";
            this.optReadOnly.UseVisualStyleBackColor = true;
            this.optReadOnly.CheckedChanged += new System.EventHandler(this.optReadOnly_CheckedChanged);
            // 
            // cmdOpenDoc
            // 
            this.cmdOpenDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOpenDoc.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdOpenDoc.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdOpenDoc.Location = new System.Drawing.Point(-88, 177);
            this.cmdOpenDoc.Name = "cmdOpenDoc";
            this.cmdOpenDoc.Size = new System.Drawing.Size(85, 36);
            this.cmdOpenDoc.TabIndex = 472;
            this.cmdOpenDoc.Text = "Open Doc";
            this.cmdOpenDoc.ToolTipText = "Mở file PDF khác trong máy tính";
            this.cmdOpenDoc.Click += new System.EventHandler(this.cmdOpenDoc_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optPdfView);
            this.panel2.Controls.Add(this.optDocView);
            this.panel2.Location = new System.Drawing.Point(6, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(58, 53);
            this.panel2.TabIndex = 471;
            // 
            // optPdfView
            // 
            this.optPdfView.AutoSize = true;
            this.optPdfView.Checked = true;
            this.optPdfView.Location = new System.Drawing.Point(3, 28);
            this.optPdfView.Name = "optPdfView";
            this.optPdfView.Size = new System.Drawing.Size(49, 19);
            this.optPdfView.TabIndex = 470;
            this.optPdfView.TabStop = true;
            this.optPdfView.Text = "PDF";
            this.optPdfView.UseVisualStyleBackColor = true;
            this.optPdfView.CheckedChanged += new System.EventHandler(this.optPdfView_CheckedChanged);
            // 
            // optDocView
            // 
            this.optDocView.AutoSize = true;
            this.optDocView.Location = new System.Drawing.Point(3, 3);
            this.optDocView.Name = "optDocView";
            this.optDocView.Size = new System.Drawing.Size(47, 19);
            this.optDocView.TabIndex = 469;
            this.optDocView.Text = "Doc";
            this.optDocView.UseVisualStyleBackColor = true;
            this.optDocView.CheckedChanged += new System.EventHandler(this.optDocView_CheckedChanged);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOpen.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdOpen.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdOpen.Location = new System.Drawing.Point(-88, 135);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(85, 36);
            this.cmdOpen.TabIndex = 98;
            this.cmdOpen.Text = "Open Pdf";
            this.cmdOpen.ToolTipText = "Mở file PDF khác trong máy tính";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdEmrDocuments);
            this.uiGroupBox2.Controls.Add(this.pnlAction);
            this.uiGroupBox2.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(515, 932);
            this.uiGroupBox2.TabIndex = 17;
            // 
            // ctxFunction
            // 
            this.ctxFunction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChuyenGay,
            this.mnuRestoreDefault_Gay,
            this.mnuDoiTenphieu,
            this.toolStripMenuItem1,
            this.mnuAnPhieu,
            this.mnuHuyAnPhieu,
            this.toolStripMenuItem2,
            this.mnuXoaPhieu,
            this.mnuHuyXoaPhieu,
            this.toolStripMenuItem5,
            this.mnuMove2Sign});
            this.ctxFunction.Name = "ctxBOD";
            this.ctxFunction.Size = new System.Drawing.Size(254, 198);
            // 
            // mnuChuyenGay
            // 
            this.mnuChuyenGay.CheckOnClick = true;
            this.mnuChuyenGay.Image = ((System.Drawing.Image)(resources.GetObject("mnuChuyenGay.Image")));
            this.mnuChuyenGay.Name = "mnuChuyenGay";
            this.mnuChuyenGay.Size = new System.Drawing.Size(253, 22);
            this.mnuChuyenGay.Text = "Chuyển gáy";
            // 
            // mnuRestoreDefault_Gay
            // 
            this.mnuRestoreDefault_Gay.Image = ((System.Drawing.Image)(resources.GetObject("mnuRestoreDefault_Gay.Image")));
            this.mnuRestoreDefault_Gay.Name = "mnuRestoreDefault_Gay";
            this.mnuRestoreDefault_Gay.Size = new System.Drawing.Size(253, 22);
            this.mnuRestoreDefault_Gay.Text = "Về gáy mặc định";
            // 
            // mnuDoiTenphieu
            // 
            this.mnuDoiTenphieu.Name = "mnuDoiTenphieu";
            this.mnuDoiTenphieu.Size = new System.Drawing.Size(253, 22);
            this.mnuDoiTenphieu.Text = "Đổi tên phiếu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(250, 6);
            // 
            // mnuAnPhieu
            // 
            this.mnuAnPhieu.Image = ((System.Drawing.Image)(resources.GetObject("mnuAnPhieu.Image")));
            this.mnuAnPhieu.Name = "mnuAnPhieu";
            this.mnuAnPhieu.Size = new System.Drawing.Size(253, 22);
            this.mnuAnPhieu.Text = "Ẩn phiếu";
            this.mnuAnPhieu.Click += new System.EventHandler(this.mnuAnPhieu_Click);
            // 
            // mnuHuyAnPhieu
            // 
            this.mnuHuyAnPhieu.Image = ((System.Drawing.Image)(resources.GetObject("mnuHuyAnPhieu.Image")));
            this.mnuHuyAnPhieu.Name = "mnuHuyAnPhieu";
            this.mnuHuyAnPhieu.Size = new System.Drawing.Size(253, 22);
            this.mnuHuyAnPhieu.Text = "Hủy ẩn phiếu";
            this.mnuHuyAnPhieu.Click += new System.EventHandler(this.mnuHuyAnPhieu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(250, 6);
            // 
            // mnuXoaPhieu
            // 
            this.mnuXoaPhieu.Image = global::VMS.HIS.EMR.Properties.Resources.trash_full_24;
            this.mnuXoaPhieu.Name = "mnuXoaPhieu";
            this.mnuXoaPhieu.Size = new System.Drawing.Size(253, 22);
            this.mnuXoaPhieu.Text = "Xóa phiếu";
            this.mnuXoaPhieu.Click += new System.EventHandler(this.mnuXoaPhieu_Click);
            // 
            // mnuHuyXoaPhieu
            // 
            this.mnuHuyXoaPhieu.Image = ((System.Drawing.Image)(resources.GetObject("mnuHuyXoaPhieu.Image")));
            this.mnuHuyXoaPhieu.Name = "mnuHuyXoaPhieu";
            this.mnuHuyXoaPhieu.Size = new System.Drawing.Size(253, 22);
            this.mnuHuyXoaPhieu.Text = "Hủy xóa phiếu";
            this.mnuHuyXoaPhieu.Click += new System.EventHandler(this.mnuHuyXoaPhieu_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(250, 6);
            // 
            // mnuMove2Sign
            // 
            this.mnuMove2Sign.Name = "mnuMove2Sign";
            this.mnuMove2Sign.Size = new System.Drawing.Size(253, 22);
            this.mnuMove2Sign.Text = "Di chuyển đến vị trí chữ ký của tôi";
            this.mnuMove2Sign.Click += new System.EventHandler(this.mnuMove2Sign_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open PDF File";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPdf);
            this.splitContainer1.Panel2.Controls.Add(this.flowSignInfor);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.flowKQCLS);
            this.splitContainer1.Size = new System.Drawing.Size(1391, 958);
            this.splitContainer1.SplitterDistance = 559;
            this.splitContainer1.TabIndex = 556;
            // 
            // uiTab
            // 
            this.uiTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab.Font = new System.Drawing.Font("Arial", 9F);
            this.uiTab.Location = new System.Drawing.Point(0, 0);
            this.uiTab.Name = "uiTab";
            this.uiTab.Size = new System.Drawing.Size(559, 958);
            this.uiTab.TabIndex = 481;
            this.uiTab.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPageEmr,
            this.uiTabPageCauhinh});
            // 
            // uiTabPageEmr
            // 
            this.uiTabPageEmr.Controls.Add(this.uiGroupBox2);
            this.uiTabPageEmr.Controls.Add(this.flowLayoutPanel1);
            this.uiTabPageEmr.Font = new System.Drawing.Font("Arial", 9F);
            this.uiTabPageEmr.Location = new System.Drawing.Point(1, 23);
            this.uiTabPageEmr.Name = "uiTabPageEmr";
            this.uiTabPageEmr.Size = new System.Drawing.Size(555, 932);
            this.uiTabPageEmr.TabStop = true;
            this.uiTabPageEmr.Text = "Hồ sơ EMR";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdCollapse);
            this.flowLayoutPanel1.Controls.Add(this.cmdReset);
            this.flowLayoutPanel1.Controls.Add(this.cmdLaythongtin);
            this.flowLayoutPanel1.Controls.Add(this.label17);
            this.flowLayoutPanel1.Controls.Add(this.label18);
            this.flowLayoutPanel1.Controls.Add(this.label19);
            this.flowLayoutPanel1.Controls.Add(this.cmdNhaplieu);
            this.flowLayoutPanel1.Controls.Add(this.cmdDigitalSign);
            this.flowLayoutPanel1.Controls.Add(this.cmdKidientu);
            this.flowLayoutPanel1.Controls.Add(this.cmdHuyKyDientu);
            this.flowLayoutPanel1.Controls.Add(this.cmdAddWord);
            this.flowLayoutPanel1.Controls.Add(this.cmdSaveWord);
            this.flowLayoutPanel1.Controls.Add(this.cmdHosoKhac);
            this.flowLayoutPanel1.Controls.Add(this.cmdChuyenGay);
            this.flowLayoutPanel1.Controls.Add(this.cmdRestoreDefault_Gay);
            this.flowLayoutPanel1.Controls.Add(this.cmdAn);
            this.flowLayoutPanel1.Controls.Add(this.cmdHienthi);
            this.flowLayoutPanel1.Controls.Add(this.cmd_history);
            this.flowLayoutPanel1.Controls.Add(this.cmdXoaphieu);
            this.flowLayoutPanel1.Controls.Add(this.cmdRestore);
            this.flowLayoutPanel1.Controls.Add(this.cmdPrintPreview);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(515, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(40, 932);
            this.flowLayoutPanel1.TabIndex = 631;
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Image = global::VMS.HIS.EMR.Properties.Resources.Down;
            this.cmdCollapse.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdCollapse.Location = new System.Drawing.Point(3, 3);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(35, 35);
            this.cmdCollapse.TabIndex = 644;
            this.toolTip1.SetToolTip(this.cmdCollapse, "Mở rộng/Thu gọn thông tin người bệnh");
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Image = ((System.Drawing.Image)(resources.GetObject("cmdReset.Image")));
            this.cmdReset.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdReset.Location = new System.Drawing.Point(3, 44);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(35, 35);
            this.cmdReset.TabIndex = 639;
            this.toolTip1.SetToolTip(this.cmdReset, "Xóa và làm mới lại toàn bộ phiếu của người bệnh đang chọn (Trừ các phiếu đã kí số" +
        ",duyệt hoặc kí điện tử)");
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdLaythongtin
            // 
            this.cmdLaythongtin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLaythongtin.Image = ((System.Drawing.Image)(resources.GetObject("cmdLaythongtin.Image")));
            this.cmdLaythongtin.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdLaythongtin.Location = new System.Drawing.Point(3, 85);
            this.cmdLaythongtin.Name = "cmdLaythongtin";
            this.cmdLaythongtin.Size = new System.Drawing.Size(35, 35);
            this.cmdLaythongtin.TabIndex = 479;
            this.toolTip1.SetToolTip(this.cmdLaythongtin, "Lấy thông tin các phiếu của người bệnh đang chọn đẩy vào hồ sơ EMR (Áp dụng cho c" +
        "ác người bệnh trước khi triển khai EMR)");
            this.cmdLaythongtin.Click += new System.EventHandler(this.cmdLaythongtin_Click);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(3, 123);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 5);
            this.label17.TabIndex = 645;
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(3, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 10);
            this.label18.TabIndex = 646;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 5F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(3, 138);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 5);
            this.label19.TabIndex = 647;
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdNhaplieu
            // 
            this.cmdNhaplieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNhaplieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhaplieu.Image")));
            this.cmdNhaplieu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdNhaplieu.Location = new System.Drawing.Point(3, 146);
            this.cmdNhaplieu.Name = "cmdNhaplieu";
            this.cmdNhaplieu.Size = new System.Drawing.Size(35, 35);
            this.cmdNhaplieu.TabIndex = 643;
            this.cmdNhaplieu.Click += new System.EventHandler(this.cmdNhaplieu_Click);
            // 
            // cmdDigitalSign
            // 
            this.cmdDigitalSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDigitalSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDigitalSign.Image = ((System.Drawing.Image)(resources.GetObject("cmdDigitalSign.Image")));
            this.cmdDigitalSign.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdDigitalSign.Location = new System.Drawing.Point(3, 187);
            this.cmdDigitalSign.Name = "cmdDigitalSign";
            this.cmdDigitalSign.Size = new System.Drawing.Size(35, 35);
            this.cmdDigitalSign.TabIndex = 630;
            this.toolTip1.SetToolTip(this.cmdDigitalSign, "Kí số các phiếu đang chọn");
            this.cmdDigitalSign.Click += new System.EventHandler(this.cmdSign_Click);
            // 
            // cmdKidientu
            // 
            this.cmdKidientu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKidientu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKidientu.Image = ((System.Drawing.Image)(resources.GetObject("cmdKidientu.Image")));
            this.cmdKidientu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdKidientu.Location = new System.Drawing.Point(3, 228);
            this.cmdKidientu.Name = "cmdKidientu";
            this.cmdKidientu.Size = new System.Drawing.Size(35, 35);
            this.cmdKidientu.TabIndex = 631;
            this.toolTip1.SetToolTip(this.cmdKidientu, "Kí điện tử các phiếu đang chọn");
            this.cmdKidientu.Click += new System.EventHandler(this.cmdKidientu_Click);
            // 
            // cmdHuyKyDientu
            // 
            this.cmdHuyKyDientu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuyKyDientu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuyKyDientu.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuyKyDientu.Image")));
            this.cmdHuyKyDientu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdHuyKyDientu.Location = new System.Drawing.Point(3, 269);
            this.cmdHuyKyDientu.Name = "cmdHuyKyDientu";
            this.cmdHuyKyDientu.Size = new System.Drawing.Size(35, 35);
            this.cmdHuyKyDientu.TabIndex = 637;
            this.toolTip1.SetToolTip(this.cmdHuyKyDientu, "Hủy ký điện tử");
            this.cmdHuyKyDientu.Click += new System.EventHandler(this.cmdHuyKyDientu_Click);
            // 
            // cmdAddWord
            // 
            this.cmdAddWord.Enabled = false;
            this.cmdAddWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddWord.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddWord.Image")));
            this.cmdAddWord.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddWord.Location = new System.Drawing.Point(3, 310);
            this.cmdAddWord.Name = "cmdAddWord";
            this.cmdAddWord.Size = new System.Drawing.Size(35, 35);
            this.cmdAddWord.TabIndex = 641;
            this.toolTip1.SetToolTip(this.cmdAddWord, "Thêm tài liệu word có thể soạn thảo trực tiếp");
            this.cmdAddWord.Click += new System.EventHandler(this.cmdAddWord_Click);
            // 
            // cmdSaveWord
            // 
            this.cmdSaveWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSaveWord.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveWord.Image")));
            this.cmdSaveWord.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSaveWord.Location = new System.Drawing.Point(3, 351);
            this.cmdSaveWord.Name = "cmdSaveWord";
            this.cmdSaveWord.Size = new System.Drawing.Size(35, 35);
            this.cmdSaveWord.TabIndex = 642;
            this.toolTip1.SetToolTip(this.cmdSaveWord, "Lưu tài liệu word đang soạn thảo");
            // 
            // cmdHosoKhac
            // 
            this.cmdHosoKhac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHosoKhac.Image = ((System.Drawing.Image)(resources.GetObject("cmdHosoKhac.Image")));
            this.cmdHosoKhac.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdHosoKhac.Location = new System.Drawing.Point(3, 392);
            this.cmdHosoKhac.Name = "cmdHosoKhac";
            this.cmdHosoKhac.Size = new System.Drawing.Size(35, 35);
            this.cmdHosoKhac.TabIndex = 640;
            this.toolTip1.SetToolTip(this.cmdHosoKhac, "Thêm các hồ sơ khác liên quan đến người bệnh như Hình ảnh, giấy tờ Scan,...");
            // 
            // cmdChuyenGay
            // 
            this.cmdChuyenGay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyenGay.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyenGay.Image")));
            this.cmdChuyenGay.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdChuyenGay.Location = new System.Drawing.Point(3, 433);
            this.cmdChuyenGay.Name = "cmdChuyenGay";
            this.cmdChuyenGay.Size = new System.Drawing.Size(35, 35);
            this.cmdChuyenGay.TabIndex = 632;
            this.toolTip1.SetToolTip(this.cmdChuyenGay, "Chuyển gáy cho tài liệu đang chọn");
            this.cmdChuyenGay.Click += new System.EventHandler(this.cmdChuyenGay_Click);
            // 
            // cmdRestoreDefault_Gay
            // 
            this.cmdRestoreDefault_Gay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestoreDefault_Gay.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestoreDefault_Gay.Image")));
            this.cmdRestoreDefault_Gay.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdRestoreDefault_Gay.Location = new System.Drawing.Point(3, 474);
            this.cmdRestoreDefault_Gay.Name = "cmdRestoreDefault_Gay";
            this.cmdRestoreDefault_Gay.Size = new System.Drawing.Size(35, 35);
            this.cmdRestoreDefault_Gay.TabIndex = 638;
            this.toolTip1.SetToolTip(this.cmdRestoreDefault_Gay, "Đưa các phiếu đang chọn về gáy theo cấu hình phiếu in");
            this.cmdRestoreDefault_Gay.Click += new System.EventHandler(this.cmdRestoreDefault_Gay_Click);
            // 
            // cmdAn
            // 
            this.cmdAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAn.Image = ((System.Drawing.Image)(resources.GetObject("cmdAn.Image")));
            this.cmdAn.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAn.Location = new System.Drawing.Point(3, 515);
            this.cmdAn.Name = "cmdAn";
            this.cmdAn.Size = new System.Drawing.Size(35, 35);
            this.cmdAn.TabIndex = 633;
            this.toolTip1.SetToolTip(this.cmdAn, "Ẩn các phiếu đang chọn(Chỉ người nào có quyền xem Full mới nhìn thấy các phiếu đã" +
        " ẩn)");
            this.cmdAn.Click += new System.EventHandler(this.cmdAn_Click);
            // 
            // cmdHienthi
            // 
            this.cmdHienthi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHienthi.Image = ((System.Drawing.Image)(resources.GetObject("cmdHienthi.Image")));
            this.cmdHienthi.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdHienthi.Location = new System.Drawing.Point(3, 556);
            this.cmdHienthi.Name = "cmdHienthi";
            this.cmdHienthi.Size = new System.Drawing.Size(35, 35);
            this.cmdHienthi.TabIndex = 634;
            this.toolTip1.SetToolTip(this.cmdHienthi, "Hiển thị phiếu ẩn");
            this.cmdHienthi.Click += new System.EventHandler(this.cmdHienthi_Click);
            // 
            // cmd_history
            // 
            this.cmd_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_history.Image = ((System.Drawing.Image)(resources.GetObject("cmd_history.Image")));
            this.cmd_history.ImageSize = new System.Drawing.Size(28, 28);
            this.cmd_history.Location = new System.Drawing.Point(3, 597);
            this.cmd_history.Name = "cmd_history";
            this.cmd_history.Size = new System.Drawing.Size(35, 35);
            this.cmd_history.TabIndex = 649;
            this.toolTip1.SetToolTip(this.cmd_history, "Các chức năng khác");
            this.cmd_history.Visible = false;
            // 
            // cmdXoaphieu
            // 
            this.cmdXoaphieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoaphieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoaphieu.Image")));
            this.cmdXoaphieu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdXoaphieu.Location = new System.Drawing.Point(3, 638);
            this.cmdXoaphieu.Name = "cmdXoaphieu";
            this.cmdXoaphieu.Size = new System.Drawing.Size(35, 35);
            this.cmdXoaphieu.TabIndex = 635;
            this.toolTip1.SetToolTip(this.cmdXoaphieu, "Xóa phiếu");
            this.cmdXoaphieu.Click += new System.EventHandler(this.cmdXoaphieu_Click);
            // 
            // cmdRestore
            // 
            this.cmdRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestore.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestore.Image")));
            this.cmdRestore.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdRestore.Location = new System.Drawing.Point(3, 679);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(35, 35);
            this.cmdRestore.TabIndex = 636;
            this.toolTip1.SetToolTip(this.cmdRestore, "Hủy xóa phiếu");
            this.cmdRestore.Visible = false;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // cmdPrintPreview
            // 
            this.cmdPrintPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintPreview.Image = global::VMS.HIS.EMR.Properties.Resources.printer_32;
            this.cmdPrintPreview.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrintPreview.Location = new System.Drawing.Point(3, 720);
            this.cmdPrintPreview.Name = "cmdPrintPreview";
            this.cmdPrintPreview.Size = new System.Drawing.Size(35, 35);
            this.cmdPrintPreview.TabIndex = 648;
            this.toolTip1.SetToolTip(this.cmdPrintPreview, "In phiếu");
            this.cmdPrintPreview.Click += new System.EventHandler(this.cmdPrintPreview_Click);
            // 
            // uiTabPageCauhinh
            // 
            this.uiTabPageCauhinh.Controls.Add(this.panel8);
            this.uiTabPageCauhinh.Location = new System.Drawing.Point(1, 23);
            this.uiTabPageCauhinh.Name = "uiTabPageCauhinh";
            this.uiTabPageCauhinh.Size = new System.Drawing.Size(555, 932);
            this.uiTabPageCauhinh.TabStop = true;
            this.uiTabPageCauhinh.Text = "Cấu hình";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.chkReloadAfterSign);
            this.panel8.Controls.Add(this.picSignImg);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(555, 932);
            this.panel8.TabIndex = 0;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label25.Location = new System.Drawing.Point(27, 85);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(123, 21);
            this.label25.TabIndex = 639;
            this.label25.Text = "Chữ ký của tôi";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkReloadAfterSign
            // 
            this.chkReloadAfterSign.AutoSize = true;
            this.chkReloadAfterSign.Checked = true;
            this.chkReloadAfterSign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReloadAfterSign.Location = new System.Drawing.Point(11, 11);
            this.chkReloadAfterSign.Name = "chkReloadAfterSign";
            this.chkReloadAfterSign.Size = new System.Drawing.Size(233, 19);
            this.chkReloadAfterSign.TabIndex = 469;
            this.chkReloadAfterSign.Tag = "EMR_NAPLAITAILIEU_SAUKHIKY";
            this.chkReloadAfterSign.Text = "Kí xong nạp lại phiếu để xem kết quả?";
            this.chkReloadAfterSign.UseVisualStyleBackColor = true;
            // 
            // picSignImg
            // 
            this.picSignImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picSignImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSignImg.Image = ((System.Drawing.Image)(resources.GetObject("picSignImg.Image")));
            this.picSignImg.Location = new System.Drawing.Point(25, 122);
            this.picSignImg.Name = "picSignImg";
            this.picSignImg.Size = new System.Drawing.Size(501, 307);
            this.picSignImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSignImg.TabIndex = 470;
            this.picSignImg.TabStop = false;
            // 
            // pnlPdf
            // 
            this.pnlPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPdf.Location = new System.Drawing.Point(0, 0);
            this.pnlPdf.Name = "pnlPdf";
            this.pnlPdf.Size = new System.Drawing.Size(828, 958);
            this.pnlPdf.TabIndex = 2;
            // 
            // flowSignInfor
            // 
            this.flowSignInfor.AutoScroll = true;
            this.flowSignInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowSignInfor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowSignInfor.Location = new System.Drawing.Point(0, 958);
            this.flowSignInfor.Name = "flowSignInfor";
            this.flowSignInfor.Size = new System.Drawing.Size(828, 0);
            this.flowSignInfor.TabIndex = 635;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdLoad);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlHide);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cboTagFields);
            this.panel1.Controls.Add(this.uiButton1);
            this.panel1.Controls.Add(this.cmdOpen);
            this.panel1.Controls.Add(this.cmdOpenDoc);
            this.panel1.Controls.Add(this.cmdPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(828, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 958);
            this.panel1.TabIndex = 1;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(-87, 332);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(76, 36);
            this.cmdSave.TabIndex = 476;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoad.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdLoad.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoad.Image")));
            this.cmdLoad.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdLoad.Location = new System.Drawing.Point(-85, 386);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(76, 36);
            this.cmdLoad.TabIndex = 476;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiButton1.Image = ((System.Drawing.Image)(resources.GetObject("uiButton1.Image")));
            this.uiButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.uiButton1.Location = new System.Drawing.Point(-88, 261);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(86, 36);
            this.uiButton1.TabIndex = 475;
            this.uiButton1.Text = "Add MergeField";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(-88, 219);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(86, 36);
            this.cmdPrint.TabIndex = 96;
            this.cmdPrint.Text = "In kết quả";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // flowKQCLS
            // 
            this.flowKQCLS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowKQCLS.Location = new System.Drawing.Point(0, 958);
            this.flowKQCLS.Name = "flowKQCLS";
            this.flowKQCLS.Size = new System.Drawing.Size(828, 0);
            this.flowKQCLS.TabIndex = 0;
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // ctxOtherFunctions
            // 
            this.ctxOtherFunctions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxOtherFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.mnuBA,
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem,
            this.sảnKhoaToolStripMenuItem,
            this.cácPhiếuKhácToolStripMenuItem,
            this.toolStripMenuItem6,
            this.mnu_history});
            this.ctxOtherFunctions.Name = "ctxBOD";
            this.ctxOtherFunctions.Size = new System.Drawing.Size(282, 190);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPT01,
            this.mnuPT02,
            this.mnuPT03,
            this.mnuPT04,
            this.mnuPT05,
            this.mnuPT06,
            this.mnuPT07,
            this.mnuPT08,
            this.mnuPT09,
            this.mnuPT10,
            this.mnuPT11,
            this.mnuPT12});
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(281, 30);
            this.toolStripMenuItem3.Text = "Phẫu thuật thủ thuật";
            // 
            // mnuPT01
            // 
            this.mnuPT01.Name = "mnuPT01";
            this.mnuPT01.Size = new System.Drawing.Size(431, 22);
            this.mnuPT01.Text = "PT-01/Biên bản hội chẩn thông qua Phẫu thuật";
            // 
            // mnuPT02
            // 
            this.mnuPT02.Name = "mnuPT02";
            this.mnuPT02.Size = new System.Drawing.Size(431, 22);
            this.mnuPT02.Text = "PT-02/Bảng chuẩn bị và bàn giao người bệnh trước Phẫu thuật";
            // 
            // mnuPT03
            // 
            this.mnuPT03.Name = "mnuPT03";
            this.mnuPT03.Size = new System.Drawing.Size(431, 22);
            this.mnuPT03.Text = "PT-03/Phiếu khám tiền mê";
            // 
            // mnuPT04
            // 
            this.mnuPT04.Name = "mnuPT04";
            this.mnuPT04.Size = new System.Drawing.Size(431, 22);
            this.mnuPT04.Text = "PT-04/Bảng kiểm an toàn Phẫu thuật";
            // 
            // mnuPT05
            // 
            this.mnuPT05.Name = "mnuPT05";
            this.mnuPT05.Size = new System.Drawing.Size(431, 22);
            this.mnuPT05.Text = "PT-05/Phiếu theo dõi gây mê hồi sức";
            // 
            // mnuPT06
            // 
            this.mnuPT06.Name = "mnuPT06";
            this.mnuPT06.Size = new System.Drawing.Size(431, 22);
            this.mnuPT06.Text = "PT-06/-Bảng kiểm đếm gạc";
            // 
            // mnuPT07
            // 
            this.mnuPT07.Name = "mnuPT07";
            this.mnuPT07.Size = new System.Drawing.Size(431, 22);
            this.mnuPT07.Text = "PT-07/Phiếu theo dõi phòng hồi tỉnh";
            // 
            // mnuPT08
            // 
            this.mnuPT08.Name = "mnuPT08";
            this.mnuPT08.Size = new System.Drawing.Size(431, 22);
            this.mnuPT08.Text = "PT-08/Phiếu đánh giá khi để cho người bệnh ra khỏi phòng hồi tỉnh";
            // 
            // mnuPT09
            // 
            this.mnuPT09.Name = "mnuPT09";
            this.mnuPT09.Size = new System.Drawing.Size(431, 22);
            this.mnuPT09.Text = "PT-09/Phiếu theo dõi người bệnh sau mổ(trong 24 giờ đầu)";
            // 
            // mnuPT10
            // 
            this.mnuPT10.Name = "mnuPT10";
            this.mnuPT10.Size = new System.Drawing.Size(431, 22);
            this.mnuPT10.Text = "PT-10/Phiếu theo dõi người bệnh sau mổ(trước mổ và từ giờ 25)";
            // 
            // mnuPT11
            // 
            this.mnuPT11.Name = "mnuPT11";
            this.mnuPT11.Size = new System.Drawing.Size(431, 22);
            this.mnuPT11.Text = "PT-11/Phiếu thủ thuật";
            // 
            // mnuPT12
            // 
            this.mnuPT12.Name = "mnuPT12";
            this.mnuPT12.Size = new System.Drawing.Size(431, 22);
            this.mnuPT12.Text = "PT-12/Phiếu phẫu thuật";
            // 
            // mnuBA
            // 
            this.mnuBA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuKhoiTaoBA,
            this.mnu01BV_BANoikhoa,
            this.mnuBAPhukhoa,
            this.mnuBASanKhoa,
            this.mnu10BV_BANgoaikhoa,
            this.mnu15BV_BANgoaitru,
            this.toolStripMenuItem4,
            this.mnuTKBA});
            this.mnuBA.Image = ((System.Drawing.Image)(resources.GetObject("mnuBA.Image")));
            this.mnuBA.Name = "mnuBA";
            this.mnuBA.Size = new System.Drawing.Size(281, 30);
            this.mnuBA.Text = "Bệnh án";
            // 
            // mnuKhoiTaoBA
            // 
            this.mnuKhoiTaoBA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuKhoiTaoBA.Name = "mnuKhoiTaoBA";
            this.mnuKhoiTaoBA.Size = new System.Drawing.Size(218, 22);
            this.mnuKhoiTaoBA.Text = "---Khởi tạo Bệnh Án---";
            // 
            // mnu01BV_BANoikhoa
            // 
            this.mnu01BV_BANoikhoa.Name = "mnu01BV_BANoikhoa";
            this.mnu01BV_BANoikhoa.Size = new System.Drawing.Size(218, 22);
            this.mnu01BV_BANoikhoa.Text = "01/BV. Bệnh án Nội khoa";
            // 
            // mnuBAPhukhoa
            // 
            this.mnuBAPhukhoa.Name = "mnuBAPhukhoa";
            this.mnuBAPhukhoa.Size = new System.Drawing.Size(218, 22);
            this.mnuBAPhukhoa.Text = "04/BV. Bệnh án Phụ khoa";
            // 
            // mnuBASanKhoa
            // 
            this.mnuBASanKhoa.Name = "mnuBASanKhoa";
            this.mnuBASanKhoa.Size = new System.Drawing.Size(218, 22);
            this.mnuBASanKhoa.Text = "05/BV. Bệnh án Sản khoa";
            // 
            // mnu10BV_BANgoaikhoa
            // 
            this.mnu10BV_BANgoaikhoa.Name = "mnu10BV_BANgoaikhoa";
            this.mnu10BV_BANgoaikhoa.Size = new System.Drawing.Size(218, 22);
            this.mnu10BV_BANgoaikhoa.Text = "10/BV. Bệnh án Ngoại khoa";
            // 
            // mnu15BV_BANgoaitru
            // 
            this.mnu15BV_BANgoaitru.Name = "mnu15BV_BANgoaitru";
            this.mnu15BV_BANgoaitru.Size = new System.Drawing.Size(218, 22);
            this.mnu15BV_BANgoaitru.Text = "15/BV. Bệnh án Ngoại trú";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuTKBA
            // 
            this.mnuTKBA.Name = "mnuTKBA";
            this.mnuTKBA.Size = new System.Drawing.Size(218, 22);
            this.mnuTKBA.Text = "Tổng kết BA";
            // 
            // mẫuPhiếuTheoThôngTư25ToolStripMenuItem
            // 
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGiaychungnhanTainanThuongtich,
            this.mnuGiayRavien,
            this.mnuBanTKBA,
            this.mnuGiayxacnhanquatrinhdieutrinoitru,
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu,
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon,
            this.mnuGiaychungnhanNghiduongthai});
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.Name = "mẫuPhiếuTheoThôngTư25ToolStripMenuItem";
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.Size = new System.Drawing.Size(281, 30);
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.Text = "Mẫu phiếu theo Thông tư 25";
            // 
            // mnuGiaychungnhanTainanThuongtich
            // 
            this.mnuGiaychungnhanTainanThuongtich.Name = "mnuGiaychungnhanTainanThuongtich";
            this.mnuGiaychungnhanTainanThuongtich.Size = new System.Drawing.Size(401, 22);
            this.mnuGiaychungnhanTainanThuongtich.Text = "01. Giấy chứng nhận tai nạn thương tích";
            // 
            // mnuGiayRavien
            // 
            this.mnuGiayRavien.Name = "mnuGiayRavien";
            this.mnuGiayRavien.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayRavien.Text = "02. Giấy ra viện";
            // 
            // mnuBanTKBA
            // 
            this.mnuBanTKBA.Name = "mnuBanTKBA";
            this.mnuBanTKBA.Size = new System.Drawing.Size(401, 22);
            this.mnuBanTKBA.Text = "03. Bản tóm tắt hồ sơ Bệnh án";
            // 
            // mnuGiayxacnhanquatrinhdieutrinoitru
            // 
            this.mnuGiayxacnhanquatrinhdieutrinoitru.Name = "mnuGiayxacnhanquatrinhdieutrinoitru";
            this.mnuGiayxacnhanquatrinhdieutrinoitru.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayxacnhanquatrinhdieutrinoitru.Text = "06. Giấy xác nhận quá trình điều trị nội trú";
            // 
            // mnuGiayxacnhanquatrinhvosinhlaodongnu
            // 
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Name = "mnuGiayxacnhanquatrinhvosinhlaodongnu";
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Text = "09. Giấy xác nhận điều trị quá trình vô sinh của lao động nữ";
            // 
            // mnuGiayxacnhannguoimekhongdusuckhoechamsoccon
            // 
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Name = "mnuGiayxacnhannguoimekhongdusuckhoechamsoccon";
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Text = "10. Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con";
            // 
            // mnuGiaychungnhanNghiduongthai
            // 
            this.mnuGiaychungnhanNghiduongthai.Name = "mnuGiaychungnhanNghiduongthai";
            this.mnuGiaychungnhanNghiduongthai.Size = new System.Drawing.Size(401, 22);
            this.mnuGiaychungnhanNghiduongthai.Text = "11. Giấy chứng nhận nghỉ dưỡng thai";
            // 
            // sảnKhoaToolStripMenuItem
            // 
            this.sảnKhoaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_hosotheodoi_sosinh,
            this.mnu_giaychungsinh});
            this.sảnKhoaToolStripMenuItem.Name = "sảnKhoaToolStripMenuItem";
            this.sảnKhoaToolStripMenuItem.Size = new System.Drawing.Size(281, 30);
            this.sảnKhoaToolStripMenuItem.Text = "Sản khoa";
            // 
            // mnu_hosotheodoi_sosinh
            // 
            this.mnu_hosotheodoi_sosinh.Image = ((System.Drawing.Image)(resources.GetObject("mnu_hosotheodoi_sosinh.Image")));
            this.mnu_hosotheodoi_sosinh.Name = "mnu_hosotheodoi_sosinh";
            this.mnu_hosotheodoi_sosinh.Size = new System.Drawing.Size(192, 22);
            this.mnu_hosotheodoi_sosinh.Text = "Hồ sơ theo dõi sơ sinh";
            // 
            // mnu_giaychungsinh
            // 
            this.mnu_giaychungsinh.Image = ((System.Drawing.Image)(resources.GetObject("mnu_giaychungsinh.Image")));
            this.mnu_giaychungsinh.Name = "mnu_giaychungsinh";
            this.mnu_giaychungsinh.Size = new System.Drawing.Size(192, 22);
            this.mnu_giaychungsinh.Text = "Giấy chứng sinh";
            // 
            // cácPhiếuKhácToolStripMenuItem
            // 
            this.cácPhiếuKhácToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_phieubangiao_nguoibenh_chuyenkhoa,
            this.mnu_phieu_chapnhan_camket_pttt,
            this.mnu_phieukhamthai,
            this.mnu_phieukhamtienme,
            this.toolStripMenuItem7});
            this.cácPhiếuKhácToolStripMenuItem.Name = "cácPhiếuKhácToolStripMenuItem";
            this.cácPhiếuKhácToolStripMenuItem.Size = new System.Drawing.Size(281, 30);
            this.cácPhiếuKhácToolStripMenuItem.Text = "Các phiếu khác";
            // 
            // mnu_phieubangiao_nguoibenh_chuyenkhoa
            // 
            this.mnu_phieubangiao_nguoibenh_chuyenkhoa.Name = "mnu_phieubangiao_nguoibenh_chuyenkhoa";
            this.mnu_phieubangiao_nguoibenh_chuyenkhoa.Size = new System.Drawing.Size(288, 22);
            this.mnu_phieubangiao_nguoibenh_chuyenkhoa.Text = "Phiếu bàn giao người bệnh chuyển khoa";
            // 
            // mnu_phieu_chapnhan_camket_pttt
            // 
            this.mnu_phieu_chapnhan_camket_pttt.Name = "mnu_phieu_chapnhan_camket_pttt";
            this.mnu_phieu_chapnhan_camket_pttt.Size = new System.Drawing.Size(288, 22);
            this.mnu_phieu_chapnhan_camket_pttt.Text = "Phiếu cam kết chấp nhận PTTT";
            // 
            // mnu_phieukhamthai
            // 
            this.mnu_phieukhamthai.Name = "mnu_phieukhamthai";
            this.mnu_phieukhamthai.Size = new System.Drawing.Size(288, 22);
            this.mnu_phieukhamthai.Text = "Phiếu khám thai";
            // 
            // mnu_phieukhamtienme
            // 
            this.mnu_phieukhamtienme.Name = "mnu_phieukhamtienme";
            this.mnu_phieukhamtienme.Size = new System.Drawing.Size(288, 22);
            this.mnu_phieukhamtienme.Text = "Phiếu khám tiền mê";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(285, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(278, 6);
            // 
            // mnu_history
            // 
            this.mnu_history.Name = "mnu_history";
            this.mnu_history.Size = new System.Drawing.Size(281, 30);
            this.mnu_history.Text = "Xem lịch sử KCB,Chỉ định CLS, Kê đơn";
            // 
            // panelZoom
            // 
            this.panelZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelZoom.BackColor = System.Drawing.SystemColors.Control;
            this.panelZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelZoom.Controls.Add(this.trackBarZoom);
            this.panelZoom.Controls.Add(this.lblZoom);
            this.panelZoom.Location = new System.Drawing.Point(909, 960);
            this.panelZoom.Name = "panelZoom";
            this.panelZoom.Size = new System.Drawing.Size(470, 25);
            this.panelZoom.TabIndex = 0;
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarZoom.Location = new System.Drawing.Point(30, 0);
            this.trackBarZoom.Maximum = 500;
            this.trackBarZoom.Minimum = 10;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Size = new System.Drawing.Size(438, 23);
            this.trackBarZoom.TabIndex = 0;
            this.trackBarZoom.TickFrequency = 10;
            this.trackBarZoom.Value = 100;
            // 
            // lblZoom
            // 
            this.lblZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblZoom.Location = new System.Drawing.Point(0, 0);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(30, 23);
            this.lblZoom.TabIndex = 1;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(3, 8);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(509, 30);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 477;
            // 
            // cboGay
            // 
            this.cboGay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGay.FormattingEnabled = true;
            this.cboGay.Location = new System.Drawing.Point(106, 113);
            this.cboGay.Name = "cboGay";
            this.cboGay.Next_Control = null;
            this.cboGay.RaiseEnterEventWhenInvisible = true;
            this.cboGay.Size = new System.Drawing.Size(387, 23);
            this.cboGay.TabIndex = 622;
            // 
            // cboLoaiphieuEmr
            // 
            this.cboLoaiphieuEmr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiphieuEmr.FormattingEnabled = true;
            this.cboLoaiphieuEmr.Location = new System.Drawing.Point(106, 60);
            this.cboLoaiphieuEmr.Name = "cboLoaiphieuEmr";
            this.cboLoaiphieuEmr.Next_Control = null;
            this.cboLoaiphieuEmr.RaiseEnterEventWhenInvisible = true;
            this.cboLoaiphieuEmr.Size = new System.Drawing.Size(387, 23);
            this.cboLoaiphieuEmr.TabIndex = 632;
            this.cboLoaiphieuEmr.SelectedIndexChanged += new System.EventHandler(this.cboLoaiphieuEmr_SelectedIndexChanged);
            // 
            // cboLoaiphieuHIS
            // 
            this.cboLoaiphieuHIS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiphieuHIS.FormattingEnabled = true;
            this.cboLoaiphieuHIS.Location = new System.Drawing.Point(106, 88);
            this.cboLoaiphieuHIS.Name = "cboLoaiphieuHIS";
            this.cboLoaiphieuHIS.Next_Control = null;
            this.cboLoaiphieuHIS.RaiseEnterEventWhenInvisible = true;
            this.cboLoaiphieuHIS.Size = new System.Drawing.Size(387, 23);
            this.cboLoaiphieuHIS.TabIndex = 634;
            this.cboLoaiphieuHIS.SelectedIndexChanged += new System.EventHandler(this.cmdLoaiphieuHIS_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label33.Location = new System.Drawing.Point(7, 114);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 21);
            this.label33.TabIndex = 623;
            this.label33.Text = "Chuyển gáy";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label33, "Chuyển gáy cho các tài liệu đang chọn bên dưới");
            // 
            // chkForced2Download
            // 
            this.chkForced2Download.AutoSize = true;
            this.chkForced2Download.Location = new System.Drawing.Point(106, 164);
            this.chkForced2Download.Name = "chkForced2Download";
            this.chkForced2Download.Size = new System.Drawing.Size(221, 19);
            this.chkForced2Download.TabIndex = 468;
            this.chkForced2Download.Tag = "CLS_LUONLAYFILEKQ_MOINHAT_TUSERVER";
            this.chkForced2Download.Text = "Luôn lấy file KQ mới nhất từ server?";
            this.chkForced2Download.UseVisualStyleBackColor = true;
            // 
            // chkIsMe
            // 
            this.chkIsMe.AutoSize = true;
            this.chkIsMe.Location = new System.Drawing.Point(387, 138);
            this.chkIsMe.Name = "chkIsMe";
            this.chkIsMe.Size = new System.Drawing.Size(106, 19);
            this.chkIsMe.TabIndex = 630;
            this.chkIsMe.Tag = "EMR_HIENTHIDULIEUCANHAN";
            this.chkIsMe.Text = "Hồ sơ của tôi?";
            this.chkIsMe.UseVisualStyleBackColor = true;
            this.chkIsMe.CheckedChanged += new System.EventHandler(this.chkIsMe_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblMatheBHYT);
            this.panel4.Controls.Add(this.txtmathebhyt);
            this.panel4.Controls.Add(this.uiButton4);
            this.panel4.Controls.Add(this.txtTenPhieu);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.uiButton5);
            this.panel4.Location = new System.Drawing.Point(9, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 11);
            this.panel4.TabIndex = 631;
            this.panel4.Visible = false;
            // 
            // uiButton5
            // 
            this.uiButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiButton5.Image = ((System.Drawing.Image)(resources.GetObject("uiButton5.Image")));
            this.uiButton5.ImageSize = new System.Drawing.Size(20, 20);
            this.uiButton5.Location = new System.Drawing.Point(-119, 44);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.Size = new System.Drawing.Size(112, 36);
            this.uiButton5.TabIndex = 629;
            this.uiButton5.Text = "Chấp nhận";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 21);
            this.label1.TabIndex = 628;
            this.label1.Text = "Tên phiếu :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiButton4
            // 
            this.uiButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiButton4.Image = ((System.Drawing.Image)(resources.GetObject("uiButton4.Image")));
            this.uiButton4.ImageSize = new System.Drawing.Size(18, 18);
            this.uiButton4.Location = new System.Drawing.Point(-118, 3);
            this.uiButton4.Name = "uiButton4";
            this.uiButton4.Size = new System.Drawing.Size(10, 25);
            this.uiButton4.TabIndex = 626;
            this.uiButton4.Click += new System.EventHandler(this.uiButton4_Click);
            // 
            // lblMatheBHYT
            // 
            this.lblMatheBHYT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatheBHYT.ForeColor = System.Drawing.Color.Red;
            this.lblMatheBHYT.Location = new System.Drawing.Point(15, 6);
            this.lblMatheBHYT.Name = "lblMatheBHYT";
            this.lblMatheBHYT.Size = new System.Drawing.Size(25, 21);
            this.lblMatheBHYT.TabIndex = 625;
            this.lblMatheBHYT.Text = "Chọn phiếu :";
            this.lblMatheBHYT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Navy;
            this.label13.Location = new System.Drawing.Point(7, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 21);
            this.label13.TabIndex = 633;
            this.label13.Text = "Loại phiếu EMR";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label13, "Chuyển gáy cho các tài liệu đang chọn bên dưới");
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(7, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 21);
            this.label14.TabIndex = 635;
            this.label14.Text = "Loại phiếu HIS";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label14, "Chuyển gáy cho các tài liệu đang chọn bên dưới");
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(7, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 21);
            this.label15.TabIndex = 638;
            this.label15.Text = "Hiển thị theo";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.optOrderbyGay);
            this.panel6.Controls.Add(this.optOrderbyTime);
            this.panel6.Location = new System.Drawing.Point(106, 8);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 22);
            this.panel6.TabIndex = 639;
            // 
            // optOrderbyTime
            // 
            this.optOrderbyTime.AutoSize = true;
            this.optOrderbyTime.Location = new System.Drawing.Point(84, 3);
            this.optOrderbyTime.Name = "optOrderbyTime";
            this.optOrderbyTime.Size = new System.Drawing.Size(104, 19);
            this.optOrderbyTime.TabIndex = 636;
            this.optOrderbyTime.Text = "Theo thời gian";
            this.optOrderbyTime.UseVisualStyleBackColor = true;
            this.optOrderbyTime.CheckedChanged += new System.EventHandler(this.optOrderbyTime_CheckedChanged);
            // 
            // optOrderbyGay
            // 
            this.optOrderbyGay.AutoSize = true;
            this.optOrderbyGay.Checked = true;
            this.optOrderbyGay.Location = new System.Drawing.Point(3, 3);
            this.optOrderbyGay.Name = "optOrderbyGay";
            this.optOrderbyGay.Size = new System.Drawing.Size(75, 19);
            this.optOrderbyGay.TabIndex = 637;
            this.optOrderbyGay.TabStop = true;
            this.optOrderbyGay.Text = "Theo gáy";
            this.optOrderbyGay.UseVisualStyleBackColor = true;
            this.optOrderbyGay.CheckedChanged += new System.EventHandler(this.optOrderbyGay_CheckedChanged);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Purple;
            this.label21.Location = new System.Drawing.Point(7, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 21);
            this.label21.TabIndex = 640;
            this.label21.Text = "Trạng thái:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.optDaky);
            this.panel7.Controls.Add(this.optAll);
            this.panel7.Controls.Add(this.optChuaky);
            this.panel7.Location = new System.Drawing.Point(106, 36);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(387, 22);
            this.panel7.TabIndex = 641;
            // 
            // optChuaky
            // 
            this.optChuaky.AutoSize = true;
            this.optChuaky.Location = new System.Drawing.Point(84, 3);
            this.optChuaky.Name = "optChuaky";
            this.optChuaky.Size = new System.Drawing.Size(68, 19);
            this.optChuaky.TabIndex = 636;
            this.optChuaky.Text = "Chưa kí";
            this.optChuaky.UseVisualStyleBackColor = true;
            this.optChuaky.CheckedChanged += new System.EventHandler(this.optChuaky_CheckedChanged);
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Checked = true;
            this.optAll.Location = new System.Drawing.Point(3, 3);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(58, 19);
            this.optAll.TabIndex = 637;
            this.optAll.TabStop = true;
            this.optAll.Text = "Tất cả";
            this.optAll.UseVisualStyleBackColor = true;
            this.optAll.CheckedChanged += new System.EventHandler(this.optAll_CheckedChanged);
            // 
            // optDaky
            // 
            this.optDaky.AutoSize = true;
            this.optDaky.Location = new System.Drawing.Point(171, 1);
            this.optDaky.Name = "optDaky";
            this.optDaky.Size = new System.Drawing.Size(53, 19);
            this.optDaky.TabIndex = 638;
            this.optDaky.Text = "Đã kí";
            this.optDaky.UseVisualStyleBackColor = true;
            this.optDaky.CheckedChanged += new System.EventHandler(this.optDaky_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(45, 141);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 14);
            this.label23.TabIndex = 643;
            this.label23.Text = "Người ký";
            // 
            // txtNguoiKy
            // 
            this.txtNguoiKy._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNguoiKy._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiKy._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoiKy.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoiKy.AutoCompleteList")));
            this.txtNguoiKy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoiKy.buildShortcut = false;
            this.txtNguoiKy.CaseSensitive = false;
            this.txtNguoiKy.CompareNoID = true;
            this.txtNguoiKy.DefaultCode = "-1";
            this.txtNguoiKy.DefaultID = "-1";
            this.txtNguoiKy.DisplayType = 0;
            this.txtNguoiKy.Drug_ID = null;
            this.txtNguoiKy.ExtraWidth = 0;
            this.txtNguoiKy.FillValueAfterSelect = false;
            this.txtNguoiKy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiKy.ForeColor = System.Drawing.Color.Black;
            this.txtNguoiKy.Location = new System.Drawing.Point(106, 137);
            this.txtNguoiKy.MaxHeight = 289;
            this.txtNguoiKy.MinTypedCharacters = 2;
            this.txtNguoiKy.MyCode = "-1";
            this.txtNguoiKy.MyID = "-1";
            this.txtNguoiKy.MyText = "";
            this.txtNguoiKy.MyTextOnly = "";
            this.txtNguoiKy.Name = "txtNguoiKy";
            this.txtNguoiKy.RaiseEvent = true;
            this.txtNguoiKy.RaiseEventEnter = true;
            this.txtNguoiKy.RaiseEventEnterWhenEmpty = true;
            this.txtNguoiKy.SelectedIndex = -1;
            this.txtNguoiKy.Size = new System.Drawing.Size(270, 21);
            this.txtNguoiKy.splitChar = '@';
            this.txtNguoiKy.splitCharIDAndCode = '#';
            this.txtNguoiKy.TabIndex = 642;
            this.txtNguoiKy.TakeCode = false;
            this.txtNguoiKy.txtMyCode = null;
            this.txtNguoiKy.txtMyCode_Edit = null;
            this.txtNguoiKy.txtMyID = null;
            this.txtNguoiKy.txtMyID_Edit = null;
            this.txtNguoiKy.txtMyName = null;
            this.txtNguoiKy.txtMyName_Edit = null;
            this.txtNguoiKy.txtNext = null;
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.txtNguoiKy);
            this.pnlAction.Controls.Add(this.label23);
            this.pnlAction.Controls.Add(this.panel7);
            this.pnlAction.Controls.Add(this.label21);
            this.pnlAction.Controls.Add(this.panel6);
            this.pnlAction.Controls.Add(this.label15);
            this.pnlAction.Controls.Add(this.label14);
            this.pnlAction.Controls.Add(this.label13);
            this.pnlAction.Controls.Add(this.panel4);
            this.pnlAction.Controls.Add(this.chkIsMe);
            this.pnlAction.Controls.Add(this.chkForced2Download);
            this.pnlAction.Controls.Add(this.label33);
            this.pnlAction.Controls.Add(this.cboLoaiphieuHIS);
            this.pnlAction.Controls.Add(this.cboLoaiphieuEmr);
            this.pnlAction.Controls.Add(this.cboGay);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAction.Location = new System.Drawing.Point(3, 38);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(509, 189);
            this.pnlAction.TabIndex = 480;
            // 
            // txtTenPhieu
            // 
            this.txtTenPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenPhieu.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtTenPhieu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTenPhieu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.txtTenPhieu.Location = new System.Drawing.Point(46, 31);
            this.txtTenPhieu.MaxLength = 15;
            this.txtTenPhieu.Name = "txtTenPhieu";
            this.txtTenPhieu.Size = new System.Drawing.Size(0, 23);
            this.txtTenPhieu.TabIndex = 627;
            this.txtTenPhieu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtTenPhieu.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtmathebhyt
            // 
            this.txtmathebhyt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtmathebhyt.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtmathebhyt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtmathebhyt.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.txtmathebhyt.Location = new System.Drawing.Point(46, 3);
            this.txtmathebhyt.MaxLength = 15;
            this.txtmathebhyt.Name = "txtmathebhyt";
            this.txtmathebhyt.Size = new System.Drawing.Size(0, 23);
            this.txtmathebhyt.TabIndex = 624;
            this.txtmathebhyt.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtmathebhyt.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdEmrDocuments
            // 
            this.grdEmrDocuments.AutomaticSort = false;
            this.grdEmrDocuments.ContextMenuStrip = this.ctxFunction;
            grdEmrDocuments_DesignTimeLayout.LayoutString = resources.GetString("grdEmrDocuments_DesignTimeLayout.LayoutString");
            this.grdEmrDocuments.DesignTimeLayout = grdEmrDocuments_DesignTimeLayout;
            this.grdEmrDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEmrDocuments.DynamicFiltering = true;
            this.grdEmrDocuments.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdEmrDocuments.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdEmrDocuments.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdEmrDocuments.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdEmrDocuments.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdEmrDocuments.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmrDocuments.FrozenColumns = -1;
            this.grdEmrDocuments.GroupByBoxVisible = false;
            this.grdEmrDocuments.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdEmrDocuments.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdEmrDocuments.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdEmrDocuments.Location = new System.Drawing.Point(3, 227);
            this.grdEmrDocuments.Name = "grdEmrDocuments";
            this.grdEmrDocuments.RecordNavigator = true;
            this.grdEmrDocuments.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdEmrDocuments.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdEmrDocuments.ScrollBarWidth = 17;
            this.grdEmrDocuments.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdEmrDocuments.Size = new System.Drawing.Size(509, 702);
            this.grdEmrDocuments.TabIndex = 6;
            this.grdEmrDocuments.TabStop = false;
            this.grdEmrDocuments.UseGroupRowSelector = true;
            this.grdEmrDocuments.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_SingleSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 985);
            this.Controls.Add(this.panelZoom);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.KeyPreview = true;
            this.Name = "frm_SingleSign";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý hồ sơ EMR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_SingleSign_FormClosing);
            this.Load += new System.EventHandler(this.frm_SingleSign_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SingleSign_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.ctxFunction.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab)).EndInit();
            this.uiTab.ResumeLayout(false);
            this.uiTabPageEmr.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.uiTabPageCauhinh.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignImg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ctxOtherFunctions.ResumeLayout(false);
            this.panelZoom.ResumeLayout(false);
            this.panelZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmrDocuments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdOpen;
     
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton cmdOpenDoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optPdfView;
        private System.Windows.Forms.RadioButton optDocView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton optEdit;
        private System.Windows.Forms.RadioButton optReadOnly;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton uiButton1;
        private System.Windows.Forms.ComboBox cboTagFields;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlHide;
        private Janus.Windows.EditControls.UIButton cmdLaythongtin;
        private Janus.Windows.EditControls.UIButton cmdLoad;
        private System.Windows.Forms.ContextMenuStrip ctxFunction;
        private System.Windows.Forms.ToolStripMenuItem mnuChuyenGay;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiTenphieu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAnPhieu;
        private System.Windows.Forms.ToolStripMenuItem mnuHuyAnPhieu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuXoaPhieu;
        private System.Windows.Forms.ToolStripMenuItem mnuHuyXoaPhieu;
        private System.Windows.Forms.Panel pnlPdf;
        private PdfViewer pdfViewer1;
        private Janus.Windows.EditControls.UIButton cmdDigitalSign;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIButton cmdKidientu;
        private Janus.Windows.EditControls.UIButton cmdChuyenGay;
        private Janus.Windows.EditControls.UIButton cmdAn;
        private Janus.Windows.EditControls.UIButton cmdHienthi;
        private Janus.Windows.EditControls.UIButton cmdXoaphieu;
        private Janus.Windows.EditControls.UIButton cmdRestore;
        private Janus.Windows.EditControls.UIButton cmdHuyKyDientu;
        private Janus.Windows.EditControls.UIButton cmdRestoreDefault_Gay;
        private System.Windows.Forms.ToolStripMenuItem mnuRestoreDefault_Gay;
        private Janus.Windows.EditControls.UIButton cmdReset;
        private Janus.Windows.EditControls.UIButton cmdHosoKhac;
        private Janus.Windows.EditControls.UIButton cmdAddWord;
        private Janus.Windows.EditControls.UIButton cmdSaveWord;
        private Janus.Windows.UI.Tab.UITab uiTab;
        private Janus.Windows.UI.Tab.UITabPage uiTabPageEmr;
        private System.Windows.Forms.FlowLayoutPanel flowKQCLS;
        private Janus.Windows.EditControls.UIButton cmdNhaplieu;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ContextMenuStrip ctxOtherFunctions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuPT01;
        private System.Windows.Forms.ToolStripMenuItem mnuPT02;
        private System.Windows.Forms.ToolStripMenuItem mnuPT03;
        private System.Windows.Forms.ToolStripMenuItem mnuPT04;
        private System.Windows.Forms.ToolStripMenuItem mnuPT05;
        private System.Windows.Forms.ToolStripMenuItem mnuPT06;
        private System.Windows.Forms.ToolStripMenuItem mnuPT07;
        private System.Windows.Forms.ToolStripMenuItem mnuPT08;
        private System.Windows.Forms.ToolStripMenuItem mnuPT09;
        private System.Windows.Forms.ToolStripMenuItem mnuPT10;
        private System.Windows.Forms.ToolStripMenuItem mnuPT11;
        private System.Windows.Forms.ToolStripMenuItem mnuPT12;
        private System.Windows.Forms.ToolStripMenuItem mnuBA;
        private System.Windows.Forms.ToolStripMenuItem mnu01BV_BANoikhoa;
        private System.Windows.Forms.ToolStripMenuItem mnu10BV_BANgoaikhoa;
        private System.Windows.Forms.ToolStripMenuItem mnu15BV_BANgoaitru;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuTKBA;
        private System.Windows.Forms.ToolStripMenuItem mnuBAPhukhoa;
        private System.Windows.Forms.ToolStripMenuItem mnuBASanKhoa;
        private Janus.Windows.UI.Tab.UITabPage uiTabPageCauhinh;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.FlowLayoutPanel flowSignInfor;
        private System.Windows.Forms.CheckBox chkReloadAfterSign;
        private System.Windows.Forms.ToolStripMenuItem mẫuPhiếuTheoThôngTư25ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGiaychungnhanTainanThuongtich;
        private System.Windows.Forms.ToolStripMenuItem mnuGiayRavien;
        private System.Windows.Forms.ToolStripMenuItem mnuBanTKBA;
        private System.Windows.Forms.ToolStripMenuItem mnuGiayxacnhanquatrinhdieutrinoitru;
        private System.Windows.Forms.ToolStripMenuItem mnuGiayxacnhanquatrinhvosinhlaodongnu;
        private System.Windows.Forms.ToolStripMenuItem mnuGiayxacnhannguoimekhongdusuckhoechamsoccon;
        private System.Windows.Forms.ToolStripMenuItem mnuGiaychungnhanNghiduongthai;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuMove2Sign;
        private System.Windows.Forms.PictureBox picSignImg;
        private System.Windows.Forms.Label label25;
        private Janus.Windows.EditControls.UIButton cmdPrintPreview;
        private Janus.Windows.EditControls.UIButton cmdCollapse;
        private System.Windows.Forms.ToolStripMenuItem mnuKhoiTaoBA;
        private System.Windows.Forms.Panel panelZoom;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.Label lblZoom;
        private Janus.Windows.EditControls.UIButton cmd_history;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnu_history;
        private System.Windows.Forms.ToolStripMenuItem cácPhiếuKhácToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_phieubangiao_nguoibenh_chuyenkhoa;
        private System.Windows.Forms.ToolStripMenuItem mnu_phieu_chapnhan_camket_pttt;
        private System.Windows.Forms.ToolStripMenuItem mnu_phieukhamthai;
        private System.Windows.Forms.ToolStripMenuItem mnu_phieukhamtienme;
        private System.Windows.Forms.ToolStripMenuItem sảnKhoaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_hosotheodoi_sosinh;
        private System.Windows.Forms.ToolStripMenuItem mnu_giaychungsinh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        public Janus.Windows.GridEX.GridEX grdEmrDocuments;
        private System.Windows.Forms.Panel pnlAction;
        private AutoCompleteTextbox txtNguoiKy;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton optDaky;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.RadioButton optChuaky;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton optOrderbyGay;
        private System.Windows.Forms.RadioButton optOrderbyTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblMatheBHYT;
        private Janus.Windows.GridEX.EditControls.EditBox txtmathebhyt;
        private Janus.Windows.EditControls.UIButton uiButton4;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenPhieu;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton uiButton5;
        private System.Windows.Forms.CheckBox chkIsMe;
        private System.Windows.Forms.CheckBox chkForced2Download;
        private System.Windows.Forms.Label label33;
        private EasyCompletionComboBox cboLoaiphieuHIS;
        private EasyCompletionComboBox cboLoaiphieuEmr;
        private EasyCompletionComboBox cboGay;
        public ucThongtinnguoibenh_emr_basic_v1 ucThongtinnguoibenh_emr_basic1;
    }
}