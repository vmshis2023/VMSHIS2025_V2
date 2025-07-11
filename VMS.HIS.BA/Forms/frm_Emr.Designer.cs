using DevExpress.XtraPdfViewer;
using VNS.HIS.UCs;
using VNS.HIS.UI.Forms.Dungchung.UCs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_Emr
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel5 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Emr));
            Janus.Windows.GridEX.GridEXLayout grdEmrDocuments_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdPatient_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem6 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem7 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem8 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem9 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem10 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem11 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem12 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem13 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.txtNguoiKy = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label23 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.optDaky = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.optChuaky = new System.Windows.Forms.RadioButton();
            this.label21 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.optOrderbyGay = new System.Windows.Forms.RadioButton();
            this.optOrderbyTime = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblMatheBHYT = new System.Windows.Forms.Label();
            this.txtmathebhyt = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiButton4 = new Janus.Windows.EditControls.UIButton();
            this.txtTenPhieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiButton5 = new Janus.Windows.EditControls.UIButton();
            this.chkIsMe = new System.Windows.Forms.CheckBox();
            this.chkForced2Download = new System.Windows.Forms.CheckBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cboLoaiphieuHIS = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboLoaiphieuEmr = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboGay = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.pnlHide = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
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
            this.grdEmrDocuments = new Janus.Windows.GridEX.GridEX();
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
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v1();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiTab = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPageDsach = new Janus.Windows.UI.Tab.UITabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dtpNgaysinh = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.Label7 = new System.Windows.Forms.Label();
            this.cboObjectType = new Janus.Windows.EditControls.UIComboBox();
            this.txtCMT = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.chkNgaysinh = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboPatientSex = new Janus.Windows.EditControls.UIComboBox();
            this.txtDienthoai = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grdPatient = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboLoaiBA = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.optBA_All = new Janus.Windows.EditControls.UIRadioButton();
            this.optBA_Datao = new Janus.Windows.EditControls.UIRadioButton();
            this.optBA_Chuatao = new Janus.Windows.EditControls.UIRadioButton();
            this.cboTrangthaiBA = new Janus.Windows.EditControls.UIComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lnkClear = new System.Windows.Forms.LinkLabel();
            this.cboKhoa = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSovaovien = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSoBA = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.lblTrangthainoitru = new System.Windows.Forms.Label();
            this.cboTrangthainoitru = new Janus.Windows.EditControls.UIComboBox();
            this.lblTtnt = new System.Windows.Forms.Label();
            this.pnlTrangthai = new System.Windows.Forms.Panel();
            this.optTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.optNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.optNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPatient_ID = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtmTo = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtmFrom = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.uiTabPageThongtin = new Janus.Windows.UI.Tab.UITabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdReset = new Janus.Windows.EditControls.UIButton();
            this.cmdLaythongtin = new Janus.Windows.EditControls.UIButton();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cmdNhaplieu = new Janus.Windows.EditControls.UIButton();
            this.cmdSign = new Janus.Windows.EditControls.UIButton();
            this.cmdKidientu = new Janus.Windows.EditControls.UIButton();
            this.cmdHuyKyDientu = new Janus.Windows.EditControls.UIButton();
            this.cmdAddWord = new Janus.Windows.EditControls.UIButton();
            this.cmdSaveWord = new Janus.Windows.EditControls.UIButton();
            this.cmdHosoKhac = new Janus.Windows.EditControls.UIButton();
            this.cmdChuyenGay = new Janus.Windows.EditControls.UIButton();
            this.cmdRestoreDefault_Gay = new Janus.Windows.EditControls.UIButton();
            this.cmdAn = new Janus.Windows.EditControls.UIButton();
            this.cmdHienthi = new Janus.Windows.EditControls.UIButton();
            this.cmdXoaphieu = new Janus.Windows.EditControls.UIButton();
            this.cmdRestore = new Janus.Windows.EditControls.UIButton();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.picSignImg = new System.Windows.Forms.PictureBox();
            this.chkReloadAfterSign = new System.Windows.Forms.CheckBox();
            this.pnlPdf = new System.Windows.Forms.Panel();
            this.flowSignInfor = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.pnlAction.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmrDocuments)).BeginInit();
            this.ctxFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab)).BeginInit();
            this.uiTab.SuspendLayout();
            this.uiTabPageDsach.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.pnlTrangthai.SuspendLayout();
            this.uiTabPageThongtin.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignImg)).BeginInit();
            this.panel1.SuspendLayout();
            this.ctxOtherFunctions.SuspendLayout();
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
            uiStatusBarPanel1.Text = "In: Ctrl+P";
            uiStatusBarPanel1.Width = 74;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Esc: Thoát";
            uiStatusBarPanel2.Width = 84;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Mở file Pdf khác: Ctrl+O";
            uiStatusBarPanel3.Width = 166;
            uiStatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel4.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel4.Key = "";
            uiStatusBarPanel4.ProgressBarValue = 0;
            uiStatusBarPanel4.Text = "F3: Tìm kiếm lại";
            uiStatusBarPanel4.Width = 116;
            uiStatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel5.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel5.Key = "Filter";
            uiStatusBarPanel5.ProgressBarValue = 0;
            uiStatusBarPanel5.Width = 10;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3,
            uiStatusBarPanel4,
            uiStatusBarPanel5});
            this.uiStatusBar1.Size = new System.Drawing.Size(1391, 27);
            this.uiStatusBar1.TabIndex = 14;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
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
            this.pnlAction.Location = new System.Drawing.Point(3, 212);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(509, 189);
            this.pnlAction.TabIndex = 480;
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
            // panel6
            // 
            this.panel6.Controls.Add(this.optOrderbyGay);
            this.panel6.Controls.Add(this.optOrderbyTime);
            this.panel6.Location = new System.Drawing.Point(106, 8);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 22);
            this.panel6.TabIndex = 639;
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
            // pnlHide
            // 
            this.pnlHide.Location = new System.Drawing.Point(9, 466);
            this.pnlHide.Name = "pnlHide";
            this.pnlHide.Size = new System.Drawing.Size(88, 48);
            this.pnlHide.TabIndex = 478;
            this.pnlHide.Visible = false;
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
            this.grdEmrDocuments.Location = new System.Drawing.Point(3, 401);
            this.grdEmrDocuments.Name = "grdEmrDocuments";
            this.grdEmrDocuments.RecordNavigator = true;
            this.grdEmrDocuments.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdEmrDocuments.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdEmrDocuments.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdEmrDocuments.Size = new System.Drawing.Size(509, 528);
            this.grdEmrDocuments.TabIndex = 6;
            this.grdEmrDocuments.TabStop = false;
            this.grdEmrDocuments.UseGroupRowSelector = true;
            this.grdEmrDocuments.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
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
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(3, 8);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(509, 204);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 477;
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
            this.uiTabPageDsach,
            this.uiTabPageThongtin,
            this.uiTabPage1});
            // 
            // uiTabPageDsach
            // 
            this.uiTabPageDsach.Controls.Add(this.panel5);
            this.uiTabPageDsach.Controls.Add(this.grdPatient);
            this.uiTabPageDsach.Controls.Add(this.uiGroupBox1);
            this.uiTabPageDsach.Location = new System.Drawing.Point(1, 23);
            this.uiTabPageDsach.Name = "uiTabPageDsach";
            this.uiTabPageDsach.Size = new System.Drawing.Size(555, 932);
            this.uiTabPageDsach.TabStop = true;
            this.uiTabPageDsach.Text = "Danh sách người bệnh";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dtpNgaysinh);
            this.panel5.Controls.Add(this.Label7);
            this.panel5.Controls.Add(this.cboObjectType);
            this.panel5.Controls.Add(this.txtCMT);
            this.panel5.Controls.Add(this.chkNgaysinh);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.cboPatientSex);
            this.panel5.Controls.Add(this.txtDienthoai);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Location = new System.Drawing.Point(13, 443);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(517, 29);
            this.panel5.TabIndex = 13;
            this.panel5.Visible = false;
            // 
            // dtpNgaysinh
            // 
            this.dtpNgaysinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaysinh.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaysinh.DropDownCalendar.Name = "";
            this.dtpNgaysinh.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgaysinh.Enabled = false;
            this.dtpNgaysinh.Location = new System.Drawing.Point(115, 35);
            this.dtpNgaysinh.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.dtpNgaysinh.Name = "dtpNgaysinh";
            this.dtpNgaysinh.ShowUpDown = true;
            this.dtpNgaysinh.Size = new System.Drawing.Size(131, 21);
            this.dtpNgaysinh.TabIndex = 21;
            this.dtpNgaysinh.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            this.dtpNgaysinh.Visible = false;
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(13, 85);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(96, 19);
            this.Label7.TabIndex = 514;
            this.Label7.Text = "Đối tượng:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label7.Visible = false;
            // 
            // cboObjectType
            // 
            this.cboObjectType.Location = new System.Drawing.Point(115, 86);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(359, 21);
            this.cboObjectType.TabIndex = 25;
            this.cboObjectType.TabStop = false;
            this.cboObjectType.Text = "Đối tượng";
            this.cboObjectType.Visible = false;
            // 
            // txtCMT
            // 
            this.txtCMT.BackColor = System.Drawing.Color.White;
            this.txtCMT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCMT.Location = new System.Drawing.Point(115, 60);
            this.txtCMT.Name = "txtCMT";
            this.txtCMT.Numeric = true;
            this.txtCMT.Size = new System.Drawing.Size(131, 21);
            this.txtCMT.TabIndex = 23;
            this.txtCMT.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtCMT.Visible = false;
            // 
            // chkNgaysinh
            // 
            this.chkNgaysinh.AutoSize = true;
            this.chkNgaysinh.Location = new System.Drawing.Point(29, 36);
            this.chkNgaysinh.Name = "chkNgaysinh";
            this.chkNgaysinh.Size = new System.Drawing.Size(84, 19);
            this.chkNgaysinh.TabIndex = 20;
            this.chkNgaysinh.TabStop = false;
            this.chkNgaysinh.Text = "Ngày sinh:";
            this.chkNgaysinh.UseVisualStyleBackColor = true;
            this.chkNgaysinh.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(58, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 19);
            this.label6.TabIndex = 525;
            this.label6.Text = "CMT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // cboPatientSex
            // 
            this.cboPatientSex.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPatientSex.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = ((byte)(100));
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Nữ";
            uiComboBoxItem2.Value = ((byte)(1));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Nam";
            uiComboBoxItem3.Value = ((byte)(0));
            this.cboPatientSex.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboPatientSex.Location = new System.Drawing.Point(354, 37);
            this.cboPatientSex.Name = "cboPatientSex";
            this.cboPatientSex.Size = new System.Drawing.Size(119, 21);
            this.cboPatientSex.TabIndex = 22;
            this.cboPatientSex.Text = "Giới tính";
            this.cboPatientSex.Visible = false;
            this.cboPatientSex.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // txtDienthoai
            // 
            this.txtDienthoai.BackColor = System.Drawing.Color.White;
            this.txtDienthoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDienthoai.Location = new System.Drawing.Point(354, 61);
            this.txtDienthoai.Name = "txtDienthoai";
            this.txtDienthoai.Size = new System.Drawing.Size(120, 21);
            this.txtDienthoai.TabIndex = 24;
            this.txtDienthoai.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtDienthoai.Visible = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(252, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 21);
            this.label11.TabIndex = 538;
            this.label11.Text = "Giới tính:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(252, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 19);
            this.label8.TabIndex = 527;
            this.label8.Text = "Điện thoại:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // grdPatient
            // 
            this.grdPatient.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPatient.BackColor = System.Drawing.Color.Silver;
            this.grdPatient.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdPatient_DesignTimeLayout.LayoutString = resources.GetString("grdPatient_DesignTimeLayout.LayoutString");
            this.grdPatient.DesignTimeLayout = grdPatient_DesignTimeLayout;
            this.grdPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatient.DynamicFiltering = true;
            this.grdPatient.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdPatient.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPatient.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdPatient.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdPatient.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPatient.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPatient.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPatient.GroupByBoxVisible = false;
            this.grdPatient.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPatient.Location = new System.Drawing.Point(0, 303);
            this.grdPatient.Name = "grdPatient";
            this.grdPatient.RecordNavigator = true;
            this.grdPatient.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPatient.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdPatient.Size = new System.Drawing.Size(555, 629);
            this.grdPatient.TabIndex = 12;
            this.grdPatient.TabStop = false;
            this.grdPatient.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cboLoaiBA);
            this.uiGroupBox1.Controls.Add(this.label24);
            this.uiGroupBox1.Controls.Add(this.label16);
            this.uiGroupBox1.Controls.Add(this.panel9);
            this.uiGroupBox1.Controls.Add(this.cboTrangthaiBA);
            this.uiGroupBox1.Controls.Add(this.label20);
            this.uiGroupBox1.Controls.Add(this.lnkClear);
            this.uiGroupBox1.Controls.Add(this.cboKhoa);
            this.uiGroupBox1.Controls.Add(this.label22);
            this.uiGroupBox1.Controls.Add(this.label10);
            this.uiGroupBox1.Controls.Add(this.txtSovaovien);
            this.uiGroupBox1.Controls.Add(this.label12);
            this.uiGroupBox1.Controls.Add(this.txtSoBA);
            this.uiGroupBox1.Controls.Add(this.lblTrangthainoitru);
            this.uiGroupBox1.Controls.Add(this.cboTrangthainoitru);
            this.uiGroupBox1.Controls.Add(this.lblTtnt);
            this.uiGroupBox1.Controls.Add(this.pnlTrangthai);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtPatient_ID);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.dtmTo);
            this.uiGroupBox1.Controls.Add(this.dtmFrom);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.label9);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(555, 303);
            this.uiGroupBox1.TabIndex = 3;
            // 
            // cboLoaiBA
            // 
            this.cboLoaiBA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiBA.Font = new System.Drawing.Font("Arial", 9F);
            this.cboLoaiBA.FormattingEnabled = true;
            this.cboLoaiBA.Location = new System.Drawing.Point(104, 222);
            this.cboLoaiBA.Name = "cboLoaiBA";
            this.cboLoaiBA.Size = new System.Drawing.Size(253, 23);
            this.cboLoaiBA.TabIndex = 549;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Arial", 9F);
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(6, 221);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 24);
            this.label24.TabIndex = 550;
            this.label24.Text = "Loại BA:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(6, 195);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 21);
            this.label16.TabIndex = 548;
            this.label16.Text = "Đã tạo BA?";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.optBA_All);
            this.panel9.Controls.Add(this.optBA_Datao);
            this.panel9.Controls.Add(this.optBA_Chuatao);
            this.panel9.Location = new System.Drawing.Point(104, 191);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(358, 28);
            this.panel9.TabIndex = 547;
            // 
            // optBA_All
            // 
            this.optBA_All.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBA_All.ForeColor = System.Drawing.Color.Black;
            this.optBA_All.Location = new System.Drawing.Point(9, 3);
            this.optBA_All.Name = "optBA_All";
            this.optBA_All.Size = new System.Drawing.Size(74, 20);
            this.optBA_All.TabIndex = 2;
            this.optBA_All.Text = "Tất cả";
            // 
            // optBA_Datao
            // 
            this.optBA_Datao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBA_Datao.ForeColor = System.Drawing.Color.Black;
            this.optBA_Datao.Location = new System.Drawing.Point(92, 3);
            this.optBA_Datao.Name = "optBA_Datao";
            this.optBA_Datao.Size = new System.Drawing.Size(122, 20);
            this.optBA_Datao.TabIndex = 3;
            this.optBA_Datao.Text = "Đã tạo Bệnh án";
            // 
            // optBA_Chuatao
            // 
            this.optBA_Chuatao.Checked = true;
            this.optBA_Chuatao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBA_Chuatao.ForeColor = System.Drawing.Color.Black;
            this.optBA_Chuatao.Location = new System.Drawing.Point(220, 3);
            this.optBA_Chuatao.Name = "optBA_Chuatao";
            this.optBA_Chuatao.Size = new System.Drawing.Size(126, 20);
            this.optBA_Chuatao.TabIndex = 4;
            this.optBA_Chuatao.TabStop = true;
            this.optBA_Chuatao.Text = "Chưa tạo Bệnh án";
            // 
            // cboTrangthaiBA
            // 
            this.cboTrangthaiBA.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Tất cả";
            uiComboBoxItem4.Value = ((byte)(100));
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "Đã làm Bệnh án";
            uiComboBoxItem5.Value = ((byte)(0));
            uiComboBoxItem6.FormatStyle.Alpha = 0;
            uiComboBoxItem6.IsSeparator = false;
            uiComboBoxItem6.Text = "Chưa làm Bệnh án";
            uiComboBoxItem6.Value = ((byte)(1));
            this.cboTrangthaiBA.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem4,
            uiComboBoxItem5,
            uiComboBoxItem6});
            this.cboTrangthaiBA.Location = new System.Drawing.Point(104, 67);
            this.cboTrangthaiBA.Name = "cboTrangthaiBA";
            this.cboTrangthaiBA.SelectedIndex = 0;
            this.cboTrangthaiBA.Size = new System.Drawing.Size(131, 21);
            this.cboTrangthaiBA.TabIndex = 545;
            this.cboTrangthaiBA.Text = "Tất cả";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(10, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 24);
            this.label20.TabIndex = 546;
            this.label20.Text = "T.thái làm BA";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkClear
            // 
            this.lnkClear.AutoSize = true;
            this.lnkClear.Location = new System.Drawing.Point(101, 254);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.Size = new System.Drawing.Size(131, 15);
            this.lnkClear.TabIndex = 544;
            this.lnkClear.TabStop = true;
            this.lnkClear.Text = "Xóa điều kiện tìm kiếm";
            this.lnkClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClear_LinkClicked);
            // 
            // cboKhoa
            // 
            this.cboKhoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoa.FormattingEnabled = true;
            this.cboKhoa.Location = new System.Drawing.Point(104, 90);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Next_Control = null;
            this.cboKhoa.RaiseEnterEventWhenInvisible = true;
            this.cboKhoa.Size = new System.Drawing.Size(445, 23);
            this.cboKhoa.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Arial", 9F);
            this.label22.Location = new System.Drawing.Point(6, 89);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 23);
            this.label22.TabIndex = 543;
            this.label22.Text = "Khoa nội trú:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(241, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 19);
            this.label10.TabIndex = 542;
            this.label10.Text = "Số vào viện:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSovaovien
            // 
            this.txtSovaovien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSovaovien.BackColor = System.Drawing.Color.White;
            this.txtSovaovien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSovaovien.Location = new System.Drawing.Point(343, 117);
            this.txtSovaovien.Name = "txtSovaovien";
            this.txtSovaovien.Size = new System.Drawing.Size(206, 21);
            this.txtSovaovien.TabIndex = 8;
            this.txtSovaovien.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 19);
            this.label12.TabIndex = 541;
            this.label12.Text = "Số BA:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSoBA
            // 
            this.txtSoBA.BackColor = System.Drawing.Color.White;
            this.txtSoBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoBA.Location = new System.Drawing.Point(104, 116);
            this.txtSoBA.Name = "txtSoBA";
            this.txtSoBA.Numeric = true;
            this.txtSoBA.Size = new System.Drawing.Size(131, 21);
            this.txtSoBA.TabIndex = 7;
            this.txtSoBA.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // lblTrangthainoitru
            // 
            this.lblTrangthainoitru.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrangthainoitru.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangthainoitru.ForeColor = System.Drawing.Color.Red;
            this.lblTrangthainoitru.Location = new System.Drawing.Point(3, 275);
            this.lblTrangthainoitru.Name = "lblTrangthainoitru";
            this.lblTrangthainoitru.Size = new System.Drawing.Size(549, 25);
            this.lblTrangthainoitru.TabIndex = 530;
            // 
            // cboTrangthainoitru
            // 
            uiComboBoxItem7.FormatStyle.Alpha = 0;
            uiComboBoxItem7.IsSeparator = false;
            uiComboBoxItem7.Text = "Tất cả";
            uiComboBoxItem7.Value = ((byte)(100));
            uiComboBoxItem8.FormatStyle.Alpha = 0;
            uiComboBoxItem8.IsSeparator = false;
            uiComboBoxItem8.Text = "Ngoại trú";
            uiComboBoxItem8.Value = ((byte)(0));
            uiComboBoxItem9.FormatStyle.Alpha = 0;
            uiComboBoxItem9.IsSeparator = false;
            uiComboBoxItem9.Text = "Nhập viện";
            uiComboBoxItem9.Value = ((byte)(1));
            uiComboBoxItem10.FormatStyle.Alpha = 0;
            uiComboBoxItem10.IsSeparator = false;
            uiComboBoxItem10.Text = "Đang điều trị";
            uiComboBoxItem10.Value = ((byte)(2));
            uiComboBoxItem11.FormatStyle.Alpha = 0;
            uiComboBoxItem11.IsSeparator = false;
            uiComboBoxItem11.Text = "Tổng hợp ra viện";
            uiComboBoxItem11.Value = ((byte)(3));
            uiComboBoxItem12.FormatStyle.Alpha = 0;
            uiComboBoxItem12.IsSeparator = false;
            uiComboBoxItem12.Text = "Đã duyệt ra viện";
            uiComboBoxItem12.Value = ((byte)(4));
            uiComboBoxItem13.FormatStyle.Alpha = 0;
            uiComboBoxItem13.IsSeparator = false;
            uiComboBoxItem13.Text = "Đã ra viện";
            uiComboBoxItem13.Value = ((byte)(5));
            this.cboTrangthainoitru.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem7,
            uiComboBoxItem8,
            uiComboBoxItem9,
            uiComboBoxItem10,
            uiComboBoxItem11,
            uiComboBoxItem12,
            uiComboBoxItem13});
            this.cboTrangthainoitru.Location = new System.Drawing.Point(343, 67);
            this.cboTrangthainoitru.Name = "cboTrangthainoitru";
            this.cboTrangthainoitru.SelectedIndex = 0;
            this.cboTrangthainoitru.Size = new System.Drawing.Size(206, 21);
            this.cboTrangthainoitru.TabIndex = 5;
            this.cboTrangthainoitru.Text = "Tất cả";
            // 
            // lblTtnt
            // 
            this.lblTtnt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTtnt.ForeColor = System.Drawing.Color.Black;
            this.lblTtnt.Location = new System.Drawing.Point(246, 69);
            this.lblTtnt.Name = "lblTtnt";
            this.lblTtnt.Size = new System.Drawing.Size(91, 19);
            this.lblTtnt.TabIndex = 528;
            this.lblTtnt.Text = "T.thái nội trú";
            this.lblTtnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTrangthai
            // 
            this.pnlTrangthai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTrangthai.Controls.Add(this.optTatCa);
            this.pnlTrangthai.Controls.Add(this.optNgoaiTru);
            this.pnlTrangthai.Controls.Add(this.optNoiTru);
            this.pnlTrangthai.Location = new System.Drawing.Point(104, 37);
            this.pnlTrangthai.Name = "pnlTrangthai";
            this.pnlTrangthai.Size = new System.Drawing.Size(358, 28);
            this.pnlTrangthai.TabIndex = 523;
            // 
            // optTatCa
            // 
            this.optTatCa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTatCa.ForeColor = System.Drawing.Color.Black;
            this.optTatCa.Location = new System.Drawing.Point(9, 3);
            this.optTatCa.Name = "optTatCa";
            this.optTatCa.Size = new System.Drawing.Size(74, 20);
            this.optTatCa.TabIndex = 2;
            this.optTatCa.Text = "Tất cả";
            // 
            // optNgoaiTru
            // 
            this.optNgoaiTru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNgoaiTru.ForeColor = System.Drawing.Color.Black;
            this.optNgoaiTru.Location = new System.Drawing.Point(92, 3);
            this.optNgoaiTru.Name = "optNgoaiTru";
            this.optNgoaiTru.Size = new System.Drawing.Size(92, 20);
            this.optNgoaiTru.TabIndex = 3;
            this.optNgoaiTru.Text = "Ngoại trú";
            // 
            // optNoiTru
            // 
            this.optNoiTru.Checked = true;
            this.optNoiTru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNoiTru.ForeColor = System.Drawing.Color.Black;
            this.optNoiTru.Location = new System.Drawing.Point(198, 3);
            this.optNoiTru.Name = "optNoiTru";
            this.optNoiTru.Size = new System.Drawing.Size(79, 20);
            this.optNoiTru.TabIndex = 4;
            this.optNoiTru.TabStop = true;
            this.optNoiTru.Text = "Nội trú";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 21);
            this.label5.TabIndex = 522;
            this.label5.Text = "Trạng thái:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(241, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 19);
            this.label4.TabIndex = 518;
            this.label4.Text = "Mã lượt khám:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPatientCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(343, 142);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(206, 21);
            this.txtPatientCode.TabIndex = 10;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 516;
            this.label2.Text = "ID Bệnh nhân:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPatient_ID
            // 
            this.txtPatient_ID.BackColor = System.Drawing.Color.White;
            this.txtPatient_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_ID.Location = new System.Drawing.Point(104, 141);
            this.txtPatient_ID.Name = "txtPatient_ID";
            this.txtPatient_ID.Numeric = true;
            this.txtPatient_ID.Size = new System.Drawing.Size(131, 21);
            this.txtPatient_ID.TabIndex = 9;
            this.txtPatient_ID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(104, 166);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(445, 23);
            this.txtPatientName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 21);
            this.label3.TabIndex = 511;
            this.label3.Text = "Tên BN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtmTo
            // 
            this.dtmTo.CustomFormat = "dd/MM/yyyy";
            this.dtmTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtmTo.DropDownCalendar.Name = "";
            this.dtmTo.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtmTo.Location = new System.Drawing.Point(343, 15);
            this.dtmTo.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.ShowUpDown = true;
            this.dtmTo.Size = new System.Drawing.Size(119, 21);
            this.dtmTo.TabIndex = 1;
            this.dtmTo.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // dtmFrom
            // 
            this.dtmFrom.CustomFormat = "dd/MM/yyyy";
            this.dtmFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtmFrom.DropDownCalendar.Name = "";
            this.dtmFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtmFrom.Location = new System.Drawing.Point(104, 15);
            this.dtmFrom.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.ShowUpDown = true;
            this.dtmFrom.Size = new System.Drawing.Size(131, 21);
            this.dtmFrom.TabIndex = 0;
            this.dtmFrom.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.AutoSize = true;
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(15, 15);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(76, 19);
            this.chkByDate.TabIndex = 0;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Từ ngày :";
            this.chkByDate.UseVisualStyleBackColor = true;
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(278, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 15);
            this.label9.TabIndex = 240;
            this.label9.Text = "Đến ngày";
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdTimKiem.Location = new System.Drawing.Point(426, 223);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(123, 40);
            this.cmdTimKiem.TabIndex = 12;
            this.cmdTimKiem.Text = "Tìm kiếm";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // uiTabPageThongtin
            // 
            this.uiTabPageThongtin.Controls.Add(this.uiGroupBox2);
            this.uiTabPageThongtin.Controls.Add(this.flowLayoutPanel1);
            this.uiTabPageThongtin.Font = new System.Drawing.Font("Arial", 9F);
            this.uiTabPageThongtin.Location = new System.Drawing.Point(1, 23);
            this.uiTabPageThongtin.Name = "uiTabPageThongtin";
            this.uiTabPageThongtin.Size = new System.Drawing.Size(555, 932);
            this.uiTabPageThongtin.TabStop = true;
            this.uiTabPageThongtin.Text = "Hồ sơ EMR";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdReset);
            this.flowLayoutPanel1.Controls.Add(this.cmdLaythongtin);
            this.flowLayoutPanel1.Controls.Add(this.label17);
            this.flowLayoutPanel1.Controls.Add(this.label18);
            this.flowLayoutPanel1.Controls.Add(this.label19);
            this.flowLayoutPanel1.Controls.Add(this.cmdNhaplieu);
            this.flowLayoutPanel1.Controls.Add(this.cmdSign);
            this.flowLayoutPanel1.Controls.Add(this.cmdKidientu);
            this.flowLayoutPanel1.Controls.Add(this.cmdHuyKyDientu);
            this.flowLayoutPanel1.Controls.Add(this.cmdAddWord);
            this.flowLayoutPanel1.Controls.Add(this.cmdSaveWord);
            this.flowLayoutPanel1.Controls.Add(this.cmdHosoKhac);
            this.flowLayoutPanel1.Controls.Add(this.cmdChuyenGay);
            this.flowLayoutPanel1.Controls.Add(this.cmdRestoreDefault_Gay);
            this.flowLayoutPanel1.Controls.Add(this.cmdAn);
            this.flowLayoutPanel1.Controls.Add(this.cmdHienthi);
            this.flowLayoutPanel1.Controls.Add(this.cmdXoaphieu);
            this.flowLayoutPanel1.Controls.Add(this.cmdRestore);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(515, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(40, 932);
            this.flowLayoutPanel1.TabIndex = 631;
            // 
            // cmdReset
            // 
            this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Image = ((System.Drawing.Image)(resources.GetObject("cmdReset.Image")));
            this.cmdReset.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdReset.Location = new System.Drawing.Point(3, 3);
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
            this.cmdLaythongtin.Location = new System.Drawing.Point(3, 44);
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
            this.label17.Location = new System.Drawing.Point(3, 82);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 5);
            this.label17.TabIndex = 645;
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(3, 87);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 10);
            this.label18.TabIndex = 646;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 5F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(3, 97);
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
            this.cmdNhaplieu.Location = new System.Drawing.Point(3, 105);
            this.cmdNhaplieu.Name = "cmdNhaplieu";
            this.cmdNhaplieu.Size = new System.Drawing.Size(35, 35);
            this.cmdNhaplieu.TabIndex = 643;
            this.cmdNhaplieu.Click += new System.EventHandler(this.cmdNhaplieu_Click);
            // 
            // cmdSign
            // 
            this.cmdSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSign.Enabled = false;
            this.cmdSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSign.Image = ((System.Drawing.Image)(resources.GetObject("cmdSign.Image")));
            this.cmdSign.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSign.Location = new System.Drawing.Point(3, 146);
            this.cmdSign.Name = "cmdSign";
            this.cmdSign.Size = new System.Drawing.Size(35, 35);
            this.cmdSign.TabIndex = 630;
            this.toolTip1.SetToolTip(this.cmdSign, "Kí số các phiếu đang chọn");
            this.cmdSign.Click += new System.EventHandler(this.cmdSign_Click);
            // 
            // cmdKidientu
            // 
            this.cmdKidientu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKidientu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKidientu.Image = ((System.Drawing.Image)(resources.GetObject("cmdKidientu.Image")));
            this.cmdKidientu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdKidientu.Location = new System.Drawing.Point(3, 187);
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
            this.cmdHuyKyDientu.Location = new System.Drawing.Point(3, 228);
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
            this.cmdAddWord.Location = new System.Drawing.Point(3, 269);
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
            this.cmdSaveWord.Location = new System.Drawing.Point(3, 310);
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
            this.cmdHosoKhac.Location = new System.Drawing.Point(3, 351);
            this.cmdHosoKhac.Name = "cmdHosoKhac";
            this.cmdHosoKhac.Size = new System.Drawing.Size(35, 35);
            this.cmdHosoKhac.TabIndex = 640;
            this.toolTip1.SetToolTip(this.cmdHosoKhac, "Thêm các hồ sơ khác liên quan đến người bệnh như Hình ảnh, giấy tờ Scan,...");
            this.cmdHosoKhac.Click += new System.EventHandler(this.cmdHosoKhac_Click);
            // 
            // cmdChuyenGay
            // 
            this.cmdChuyenGay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyenGay.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyenGay.Image")));
            this.cmdChuyenGay.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdChuyenGay.Location = new System.Drawing.Point(3, 392);
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
            this.cmdRestoreDefault_Gay.Location = new System.Drawing.Point(3, 433);
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
            this.cmdAn.Location = new System.Drawing.Point(3, 474);
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
            this.cmdHienthi.Location = new System.Drawing.Point(3, 515);
            this.cmdHienthi.Name = "cmdHienthi";
            this.cmdHienthi.Size = new System.Drawing.Size(35, 35);
            this.cmdHienthi.TabIndex = 634;
            this.toolTip1.SetToolTip(this.cmdHienthi, "Lấy thông tin các phiếu của người bệnh đang chọn đẩy vào hồ sơ EMR (Áp dụng cho c" +
        "ác người bệnh trước khi triển khai EMR)");
            this.cmdHienthi.Click += new System.EventHandler(this.cmdHienthi_Click);
            // 
            // cmdXoaphieu
            // 
            this.cmdXoaphieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoaphieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoaphieu.Image")));
            this.cmdXoaphieu.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdXoaphieu.Location = new System.Drawing.Point(3, 556);
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
            this.cmdRestore.Location = new System.Drawing.Point(3, 597);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(35, 35);
            this.cmdRestore.TabIndex = 636;
            this.toolTip1.SetToolTip(this.cmdRestore, "Hủy xóa phiếu");
            this.cmdRestore.Visible = false;
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.panel8);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(555, 932);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Cấu hình";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.picSignImg);
            this.panel8.Controls.Add(this.chkReloadAfterSign);
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
            // pnlPdf
            // 
            this.pnlPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPdf.Location = new System.Drawing.Point(0, 0);
            this.pnlPdf.Name = "pnlPdf";
            this.pnlPdf.Size = new System.Drawing.Size(828, 877);
            this.pnlPdf.TabIndex = 2;
            // 
            // flowSignInfor
            // 
            this.flowSignInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowSignInfor.Location = new System.Drawing.Point(0, 877);
            this.flowSignInfor.Name = "flowSignInfor";
            this.flowSignInfor.Size = new System.Drawing.Size(828, 32);
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
            this.panel1.Size = new System.Drawing.Size(0, 909);
            this.panel1.TabIndex = 1;
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
            this.flowKQCLS.Location = new System.Drawing.Point(0, 909);
            this.flowKQCLS.Name = "flowKQCLS";
            this.flowKQCLS.Size = new System.Drawing.Size(828, 49);
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
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem});
            this.ctxOtherFunctions.Name = "ctxBOD";
            this.ctxOtherFunctions.Size = new System.Drawing.Size(233, 94);
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
            this.toolStripMenuItem3.Size = new System.Drawing.Size(232, 30);
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
            this.mnu01BV_BANoikhoa,
            this.mnuBAPhukhoa,
            this.mnuBASanKhoa,
            this.mnu10BV_BANgoaikhoa,
            this.mnu15BV_BANgoaitru,
            this.toolStripMenuItem4,
            this.mnuTKBA});
            this.mnuBA.Image = ((System.Drawing.Image)(resources.GetObject("mnuBA.Image")));
            this.mnuBA.Name = "mnuBA";
            this.mnuBA.Size = new System.Drawing.Size(232, 30);
            this.mnuBA.Text = "Bệnh án";
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
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.Size = new System.Drawing.Size(232, 30);
            this.mẫuPhiếuTheoThôngTư25ToolStripMenuItem.Text = "Mẫu phiếu theo Thông tư 25";
            // 
            // mnuGiaychungnhanTainanThuongtich
            // 
            this.mnuGiaychungnhanTainanThuongtich.Name = "mnuGiaychungnhanTainanThuongtich";
            this.mnuGiaychungnhanTainanThuongtich.Size = new System.Drawing.Size(401, 22);
            this.mnuGiaychungnhanTainanThuongtich.Text = "01. Giấy chứng nhận tai nạn thương tích";
            this.mnuGiaychungnhanTainanThuongtich.Click += new System.EventHandler(this.mnuGiaychungnhanTainanThuongtich_Click);
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
            this.mnuGiayxacnhanquatrinhdieutrinoitru.Click += new System.EventHandler(this.mnuGiayxacnhanquatrinhdieutrinoitru_Click);
            // 
            // mnuGiayxacnhanquatrinhvosinhlaodongnu
            // 
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Name = "mnuGiayxacnhanquatrinhvosinhlaodongnu";
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Text = "09. Giấy xác nhận điều trị quá trình vô sinh của lao động nữ";
            this.mnuGiayxacnhanquatrinhvosinhlaodongnu.Click += new System.EventHandler(this.mnuGiayxacnhanquatrinhvosinhlaodongnu_Click);
            // 
            // mnuGiayxacnhannguoimekhongdusuckhoechamsoccon
            // 
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Name = "mnuGiayxacnhannguoimekhongdusuckhoechamsoccon";
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Size = new System.Drawing.Size(401, 22);
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Text = "10. Giấy xác nhận người mẹ không đủ sức khỏe chăm sóc con";
            this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon.Click += new System.EventHandler(this.mnuGiayxacnhannguoimekhongdusuckhoechamsoccon_Click);
            // 
            // mnuGiaychungnhanNghiduongthai
            // 
            this.mnuGiaychungnhanNghiduongthai.Name = "mnuGiaychungnhanNghiduongthai";
            this.mnuGiaychungnhanNghiduongthai.Size = new System.Drawing.Size(401, 22);
            this.mnuGiaychungnhanNghiduongthai.Text = "11. Giấy chứng nhận nghỉ dưỡng thai";
            this.mnuGiaychungnhanNghiduongthai.Click += new System.EventHandler(this.mnuGiaychungnhanNghiduongthai_Click);
            // 
            // frm_Emr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 985);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.KeyPreview = true;
            this.Name = "frm_Emr";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý hồ sơ EMR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Emr_FormClosing);
            this.Load += new System.EventHandler(this.frm_Emr_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Emr_KeyDown);
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEmrDocuments)).EndInit();
            this.ctxFunction.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab)).EndInit();
            this.uiTab.ResumeLayout(false);
            this.uiTabPageDsach.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.pnlTrangthai.ResumeLayout(false);
            this.uiTabPageThongtin.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignImg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ctxOtherFunctions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdOpen;
        private System.Windows.Forms.CheckBox chkForced2Download;
     
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton cmdOpenDoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optPdfView;
        private System.Windows.Forms.RadioButton optDocView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton optEdit;
        private System.Windows.Forms.RadioButton optReadOnly;
        public Janus.Windows.GridEX.GridEX grdEmrDocuments;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton uiButton1;
        private System.Windows.Forms.ComboBox cboTagFields;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlHide;
        public ucThongtinnguoibenh_emr_basic_v1 ucThongtinnguoibenh_emr_basic1;
        private Janus.Windows.EditControls.UIButton cmdLaythongtin;
        private System.Windows.Forms.Panel pnlAction;
        private Janus.Windows.EditControls.UIButton cmdLoad;
        private EasyCompletionComboBox cboGay;
        private System.Windows.Forms.Label label33;
        private Janus.Windows.EditControls.UIButton uiButton5;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenPhieu;
        private Janus.Windows.EditControls.UIButton uiButton4;
        private Janus.Windows.GridEX.EditControls.EditBox txtmathebhyt;
        private System.Windows.Forms.Label lblMatheBHYT;
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
        private Janus.Windows.EditControls.UIButton cmdSign;
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
        private System.Windows.Forms.CheckBox chkIsMe;
        private System.Windows.Forms.Panel panel4;
        private Janus.Windows.EditControls.UIButton cmdAddWord;
        private Janus.Windows.EditControls.UIButton cmdSaveWord;
        private Janus.Windows.UI.Tab.UITab uiTab;
        private Janus.Windows.UI.Tab.UITabPage uiTabPageDsach;
        private Janus.Windows.UI.Tab.UITabPage uiTabPageThongtin;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        public System.Windows.Forms.CheckBox chkNgaysinh;
        public Janus.Windows.EditControls.UIComboBox cboPatientSex;
        private System.Windows.Forms.Label label11;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpNgaysinh;
        private System.Windows.Forms.Label lblTrangthainoitru;
        private Janus.Windows.EditControls.UIComboBox cboTrangthainoitru;
        internal System.Windows.Forms.Label lblTtnt;
        internal System.Windows.Forms.Label label8;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtDienthoai;
        internal System.Windows.Forms.Label label6;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtCMT;
        private System.Windows.Forms.Panel pnlTrangthai;
        public Janus.Windows.EditControls.UIRadioButton optTatCa;
        public Janus.Windows.EditControls.UIRadioButton optNgoaiTru;
        public Janus.Windows.EditControls.UIRadioButton optNoiTru;
        internal System.Windows.Forms.Label label5;
        public Janus.Windows.EditControls.UIComboBox cboObjectType;
        internal System.Windows.Forms.Label label4;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtPatientCode;
        internal System.Windows.Forms.Label label2;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtPatient_ID;
        internal System.Windows.Forms.Label Label7;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        internal System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtmTo;
        private Janus.Windows.CalendarCombo.CalendarCombo dtmFrom;
        private System.Windows.Forms.CheckBox chkByDate;
        internal System.Windows.Forms.Label label9;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.Panel panel5;
        private Janus.Windows.GridEX.GridEX grdPatient;
        internal System.Windows.Forms.Label label10;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtSovaovien;
        internal System.Windows.Forms.Label label12;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtSoBA;
        private System.Windows.Forms.Label label22;
        private EasyCompletionComboBox cboKhoa;
        private System.Windows.Forms.LinkLabel lnkClear;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton optOrderbyGay;
        private System.Windows.Forms.RadioButton optOrderbyTime;
        private EasyCompletionComboBox cboLoaiphieuHIS;
        private System.Windows.Forms.Label label14;
        private EasyCompletionComboBox cboLoaiphieuEmr;
        private System.Windows.Forms.Label label13;
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
        private Janus.Windows.EditControls.UIComboBox cboTrangthaiBA;
        internal System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton optDaky;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.RadioButton optChuaky;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStripMenuItem mnuBAPhukhoa;
        private System.Windows.Forms.ToolStripMenuItem mnuBASanKhoa;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private AutoCompleteTextbox txtNguoiKy;
        private System.Windows.Forms.Label label23;
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
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel9;
        public Janus.Windows.EditControls.UIRadioButton optBA_All;
        public Janus.Windows.EditControls.UIRadioButton optBA_Datao;
        public Janus.Windows.EditControls.UIRadioButton optBA_Chuatao;
        private System.Windows.Forms.ComboBox cboLoaiBA;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox picSignImg;
        private System.Windows.Forms.Label label25;
    }
}