using VNS.HIS.UCs;
namespace VNS.HIS.UI.GOIKHAM
{
    partial class frm_TaoGoiKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TaoGoiKham));
            Janus.Windows.GridEX.GridEXLayout grdKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdCLS_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdThuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdVTu_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdThoat = new Janus.Windows.EditControls.UIButton();
            this.txtMienGiam = new MaskedTextBox.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl = new Janus.Windows.UI.Tab.UITab();
            this.tabKham = new Janus.Windows.UI.Tab.UITabPage();
            this.grdKham = new Janus.Windows.GridEX.GridEX();
            this.tabCLS = new Janus.Windows.UI.Tab.UITabPage();
            this.grdCLS = new Janus.Windows.GridEX.GridEX();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdAddCLS = new Janus.Windows.EditControls.UIButton();
            this.label12 = new System.Windows.Forms.Label();
            this.autoNhomCLS = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.tabThuoc = new Janus.Windows.UI.Tab.UITabPage();
            this.grdThuoc = new Janus.Windows.GridEX.GridEX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdAddThuoc = new Janus.Windows.EditControls.UIButton();
            this.label13 = new System.Windows.Forms.Label();
            this.autoThuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.tabGiuong = new Janus.Windows.UI.Tab.UITabPage();
            this.grdGiuong = new Janus.Windows.GridEX.GridEX();
            this.tabVTu = new Janus.Windows.UI.Tab.UITabPage();
            this.grdVTu = new Janus.Windows.GridEX.GridEX();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmdAddVTTH = new Janus.Windows.EditControls.UIButton();
            this.lbldonthuocmau = new System.Windows.Forms.Label();
            this.autoVTTH = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtMaGoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtThanhTien = new MaskedTextBox.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdTake = new Janus.Windows.EditControls.UIButton();
            this.cboKieugoi = new Janus.Windows.EditControls.UIComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtGiamtruBHYT = new MaskedTextBox.MaskedTextBox();
            this.txtThanhTienThamChieu = new MaskedTextBox.MaskedTextBox();
            this.txtTienGoi = new MaskedTextBox.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.txtVT = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtGiuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtThuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtDichvuCLS = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtCongkham = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtnhomchidinh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmdNhomChidinh = new Janus.Windows.EditControls.UIButton();
            this.dtpHieuLucDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpHieuLucTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkChiDinhNhanh = new Janus.Windows.EditControls.UICheckBox();
            this.txtFilterName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkHieuLuc = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTenGoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabKham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKham)).BeginInit();
            this.tabCLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCLS)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabThuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThuoc)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabGiuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).BeginInit();
            this.tabVTu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVTu)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdSave);
            this.uiGroupBox1.Controls.Add(this.cmdThoat);
            this.uiGroupBox1.Controls.Add(this.txtMienGiam);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 681);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1306, 48);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Chức năng";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(1049, 9);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 20;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Lưu lại thông tin trên lưới thông tin dịch vụ";
            this.cmdSave.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdThoat.Location = new System.Drawing.Point(1175, 9);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(120, 35);
            this.cmdThoat.TabIndex = 21;
            this.cmdThoat.Text = "Thoát (Esc)";
            this.cmdThoat.ToolTipText = "Thoát Form hiện tại";
            this.cmdThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtMienGiam
            // 
            this.txtMienGiam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMienGiam.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMienGiam.Location = new System.Drawing.Point(248, 20);
            this.txtMienGiam.Masked = MaskedTextBox.Mask.Decimal;
            this.txtMienGiam.Name = "txtMienGiam";
            this.txtMienGiam.Size = new System.Drawing.Size(155, 22);
            this.txtMienGiam.TabIndex = 7;
            this.txtMienGiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMienGiam.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(153, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 15);
            this.label7.TabIndex = 516;
            this.label7.Text = "MIỄN GIẢM";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.Location = new System.Drawing.Point(0, 125);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(509, 556);
            this.tabControl.TabIndex = 4;
            this.tabControl.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.tabKham,
            this.tabCLS,
            this.tabThuoc,
            this.tabGiuong,
            this.tabVTu});
            this.tabControl.SelectedTabChanged += new Janus.Windows.UI.Tab.TabEventHandler(this.tabControl_SelectedTabChanged);
            // 
            // tabKham
            // 
            this.tabKham.Controls.Add(this.grdKham);
            this.tabKham.Key = "1";
            this.tabKham.Location = new System.Drawing.Point(1, 23);
            this.tabKham.Name = "tabKham";
            this.tabKham.Size = new System.Drawing.Size(505, 530);
            this.tabKham.TabStop = true;
            this.tabKham.Text = "Khám";
            // 
            // grdKham
            // 
            this.grdKham.BackColor = System.Drawing.Color.Silver;
            this.grdKham.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin chỉ định</FilterRowInfoText></LocalizableData>";
            this.grdKham.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdKham_DesignTimeLayout.LayoutString = resources.GetString("grdKham_DesignTimeLayout.LayoutString");
            this.grdKham.DesignTimeLayout = grdKham_DesignTimeLayout;
            this.grdKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKham.DynamicFiltering = true;
            this.grdKham.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKham.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKham.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdKham.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdKham.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdKham.GroupByBoxVisible = false;
            this.grdKham.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdKham.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKham.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdKham.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKham.Location = new System.Drawing.Point(0, 0);
            this.grdKham.Name = "grdKham";
            this.grdKham.RecordNavigator = true;
            this.grdKham.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKham.Size = new System.Drawing.Size(505, 530);
            this.grdKham.TabIndex = 10;
            this.grdKham.TableSpacing = 0;
            this.grdKham.UseGroupRowSelector = true;
            this.grdKham.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdKham_CellValueChanged);
            // 
            // tabCLS
            // 
            this.tabCLS.Controls.Add(this.grdCLS);
            this.tabCLS.Controls.Add(this.panel3);
            this.tabCLS.Key = "2";
            this.tabCLS.Location = new System.Drawing.Point(1, 23);
            this.tabCLS.Name = "tabCLS";
            this.tabCLS.Size = new System.Drawing.Size(505, 530);
            this.tabCLS.TabStop = true;
            this.tabCLS.Text = "Cận lâm sàng";
            // 
            // grdCLS
            // 
            this.grdCLS.BackColor = System.Drawing.Color.Silver;
            this.grdCLS.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin chỉ định</FilterRowInfoText></LocalizableData>";
            this.grdCLS.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grdCLS.ColumnAutoResize = true;
            this.grdCLS.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdCLS_DesignTimeLayout.LayoutString = resources.GetString("grdCLS_DesignTimeLayout.LayoutString");
            this.grdCLS.DesignTimeLayout = grdCLS_DesignTimeLayout;
            this.grdCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCLS.DynamicFiltering = true;
            this.grdCLS.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdCLS.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdCLS.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdCLS.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdCLS.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdCLS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdCLS.GroupByBoxVisible = false;
            this.grdCLS.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdCLS.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdCLS.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdCLS.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdCLS.Location = new System.Drawing.Point(0, 36);
            this.grdCLS.Name = "grdCLS";
            this.grdCLS.RecordNavigator = true;
            this.grdCLS.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdCLS.Size = new System.Drawing.Size(505, 494);
            this.grdCLS.TabIndex = 8;
            this.grdCLS.TableSpacing = 0;
            this.grdCLS.UseGroupRowSelector = true;
            this.grdCLS.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdCLS_CellValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdAddCLS);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.autoNhomCLS);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(505, 36);
            this.panel3.TabIndex = 9;
            // 
            // cmdAddCLS
            // 
            this.cmdAddCLS.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddCLS.Image")));
            this.cmdAddCLS.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddCLS.Location = new System.Drawing.Point(469, 5);
            this.cmdAddCLS.Name = "cmdAddCLS";
            this.cmdAddCLS.Size = new System.Drawing.Size(31, 23);
            this.cmdAddCLS.TabIndex = 624;
            this.cmdAddCLS.TabStop = false;
            this.cmdAddCLS.Click += new System.EventHandler(this.cmdAddCLS_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 19);
            this.label12.TabIndex = 525;
            this.label12.Text = "Chọn mẫu(F3):";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoNhomCLS
            // 
            this.autoNhomCLS._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoNhomCLS._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNhomCLS._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoNhomCLS.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoNhomCLS.AutoCompleteList")));
            this.autoNhomCLS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoNhomCLS.buildShortcut = false;
            this.autoNhomCLS.CaseSensitive = false;
            this.autoNhomCLS.CompareNoID = true;
            this.autoNhomCLS.DefaultCode = "-1";
            this.autoNhomCLS.DefaultID = "-1";
            this.autoNhomCLS.DisplayType = 0;
            this.autoNhomCLS.Drug_ID = null;
            this.autoNhomCLS.ExtraWidth = 0;
            this.autoNhomCLS.FillValueAfterSelect = false;
            this.autoNhomCLS.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNhomCLS.Location = new System.Drawing.Point(118, 6);
            this.autoNhomCLS.MaxHeight = 150;
            this.autoNhomCLS.MinTypedCharacters = 2;
            this.autoNhomCLS.MyCode = "-1";
            this.autoNhomCLS.MyID = "-1";
            this.autoNhomCLS.MyText = "";
            this.autoNhomCLS.MyTextOnly = "";
            this.autoNhomCLS.Name = "autoNhomCLS";
            this.autoNhomCLS.RaiseEvent = false;
            this.autoNhomCLS.RaiseEventEnter = true;
            this.autoNhomCLS.RaiseEventEnterWhenEmpty = false;
            this.autoNhomCLS.SelectedIndex = -1;
            this.autoNhomCLS.Size = new System.Drawing.Size(347, 23);
            this.autoNhomCLS.splitChar = '@';
            this.autoNhomCLS.splitCharIDAndCode = '#';
            this.autoNhomCLS.TabIndex = 524;
            this.autoNhomCLS.TabStop = false;
            this.autoNhomCLS.TakeCode = false;
            this.autoNhomCLS.txtMyCode = null;
            this.autoNhomCLS.txtMyCode_Edit = null;
            this.autoNhomCLS.txtMyID = null;
            this.autoNhomCLS.txtMyID_Edit = null;
            this.autoNhomCLS.txtMyName = null;
            this.autoNhomCLS.txtMyName_Edit = null;
            this.autoNhomCLS.txtNext = this.cmdAccept;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.Location = new System.Drawing.Point(1156, 80);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(144, 40);
            this.cmdAccept.TabIndex = 14;
            this.cmdAccept.TabStop = false;
            this.cmdAccept.Text = "Thêm(Ctrl+A)";
            this.cmdAccept.ToolTipText = "Ctrl+A";
            this.cmdAccept.Click += new System.EventHandler(this.btnThemDichVu_Click);
            // 
            // tabThuoc
            // 
            this.tabThuoc.Controls.Add(this.grdThuoc);
            this.tabThuoc.Controls.Add(this.panel4);
            this.tabThuoc.Key = "3";
            this.tabThuoc.Location = new System.Drawing.Point(1, 23);
            this.tabThuoc.Name = "tabThuoc";
            this.tabThuoc.Size = new System.Drawing.Size(505, 530);
            this.tabThuoc.TabStop = true;
            this.tabThuoc.Text = "Thuốc";
            // 
            // grdThuoc
            // 
            this.grdThuoc.BackColor = System.Drawing.Color.Silver;
            this.grdThuoc.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin chỉ định</FilterRowInfoText></LocalizableData>";
            this.grdThuoc.ColumnAutoResize = true;
            this.grdThuoc.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdThuoc_DesignTimeLayout.LayoutString = resources.GetString("grdThuoc_DesignTimeLayout.LayoutString");
            this.grdThuoc.DesignTimeLayout = grdThuoc_DesignTimeLayout;
            this.grdThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThuoc.DynamicFiltering = true;
            this.grdThuoc.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThuoc.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThuoc.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdThuoc.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdThuoc.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdThuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdThuoc.GroupByBoxVisible = false;
            this.grdThuoc.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdThuoc.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdThuoc.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdThuoc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThuoc.Location = new System.Drawing.Point(0, 36);
            this.grdThuoc.Name = "grdThuoc";
            this.grdThuoc.RecordNavigator = true;
            this.grdThuoc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThuoc.Size = new System.Drawing.Size(505, 494);
            this.grdThuoc.TabIndex = 9;
            this.grdThuoc.TableSpacing = 0;
            this.grdThuoc.UseGroupRowSelector = true;
            this.grdThuoc.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdThuoc_CellValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmdAddThuoc);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.autoThuoc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(505, 36);
            this.panel4.TabIndex = 11;
            // 
            // cmdAddThuoc
            // 
            this.cmdAddThuoc.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddThuoc.Image")));
            this.cmdAddThuoc.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddThuoc.Location = new System.Drawing.Point(471, 7);
            this.cmdAddThuoc.Name = "cmdAddThuoc";
            this.cmdAddThuoc.Size = new System.Drawing.Size(31, 23);
            this.cmdAddThuoc.TabIndex = 623;
            this.cmdAddThuoc.TabStop = false;
            this.cmdAddThuoc.Click += new System.EventHandler(this.cmdAddThuoc_Click);
            // 
            // label13
            // 
            this.label13.Enabled = false;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label13.Location = new System.Drawing.Point(8, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 23);
            this.label13.TabIndex = 622;
            this.label13.Text = "Đơn mẫu (F2):";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoThuoc
            // 
            this.autoThuoc._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoThuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoThuoc._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoThuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoThuoc.AutoCompleteList")));
            this.autoThuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoThuoc.buildShortcut = false;
            this.autoThuoc.CaseSensitive = false;
            this.autoThuoc.CompareNoID = true;
            this.autoThuoc.DefaultCode = "-1";
            this.autoThuoc.DefaultID = "-1";
            this.autoThuoc.DisplayType = 0;
            this.autoThuoc.Drug_ID = null;
            this.autoThuoc.ExtraWidth = 0;
            this.autoThuoc.FillValueAfterSelect = false;
            this.autoThuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.autoThuoc.Location = new System.Drawing.Point(106, 8);
            this.autoThuoc.MaxHeight = 150;
            this.autoThuoc.MinTypedCharacters = 2;
            this.autoThuoc.MyCode = "-1";
            this.autoThuoc.MyID = "-1";
            this.autoThuoc.MyText = "";
            this.autoThuoc.MyTextOnly = "";
            this.autoThuoc.Name = "autoThuoc";
            this.autoThuoc.RaiseEvent = false;
            this.autoThuoc.RaiseEventEnter = true;
            this.autoThuoc.RaiseEventEnterWhenEmpty = false;
            this.autoThuoc.SelectedIndex = -1;
            this.autoThuoc.Size = new System.Drawing.Size(365, 21);
            this.autoThuoc.splitChar = '@';
            this.autoThuoc.splitCharIDAndCode = '#';
            this.autoThuoc.TabIndex = 621;
            this.autoThuoc.TabStop = false;
            this.autoThuoc.TakeCode = false;
            this.autoThuoc.txtMyCode = null;
            this.autoThuoc.txtMyCode_Edit = null;
            this.autoThuoc.txtMyID = null;
            this.autoThuoc.txtMyID_Edit = null;
            this.autoThuoc.txtMyName = null;
            this.autoThuoc.txtMyName_Edit = null;
            this.autoThuoc.txtNext = null;
            // 
            // tabGiuong
            // 
            this.tabGiuong.Controls.Add(this.grdGiuong);
            this.tabGiuong.Key = "4";
            this.tabGiuong.Location = new System.Drawing.Point(1, 23);
            this.tabGiuong.Name = "tabGiuong";
            this.tabGiuong.Size = new System.Drawing.Size(505, 530);
            this.tabGiuong.TabStop = true;
            this.tabGiuong.Text = "Giường";
            // 
            // grdGiuong
            // 
            this.grdGiuong.BackColor = System.Drawing.Color.Silver;
            this.grdGiuong.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin chỉ định</FilterRowInfoText></LocalizableData>";
            this.grdGiuong.ColumnAutoResize = true;
            this.grdGiuong.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdGiuong_DesignTimeLayout.LayoutString = resources.GetString("grdGiuong_DesignTimeLayout.LayoutString");
            this.grdGiuong.DesignTimeLayout = grdGiuong_DesignTimeLayout;
            this.grdGiuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGiuong.DynamicFiltering = true;
            this.grdGiuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdGiuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdGiuong.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdGiuong.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdGiuong.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdGiuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdGiuong.GroupByBoxVisible = false;
            this.grdGiuong.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdGiuong.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdGiuong.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdGiuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdGiuong.Location = new System.Drawing.Point(0, 0);
            this.grdGiuong.Name = "grdGiuong";
            this.grdGiuong.RecordNavigator = true;
            this.grdGiuong.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdGiuong.Size = new System.Drawing.Size(505, 530);
            this.grdGiuong.TabIndex = 10;
            this.grdGiuong.TableSpacing = 0;
            this.grdGiuong.UseGroupRowSelector = true;
            this.grdGiuong.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdGiuong_CellValueChanged);
            // 
            // tabVTu
            // 
            this.tabVTu.Controls.Add(this.grdVTu);
            this.tabVTu.Controls.Add(this.panel5);
            this.tabVTu.Key = "5";
            this.tabVTu.Location = new System.Drawing.Point(1, 23);
            this.tabVTu.Name = "tabVTu";
            this.tabVTu.Size = new System.Drawing.Size(505, 530);
            this.tabVTu.TabStop = true;
            this.tabVTu.Text = "Vật tư";
            // 
            // grdVTu
            // 
            this.grdVTu.BackColor = System.Drawing.Color.Silver;
            this.grdVTu.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin chỉ định</FilterRowInfoText></LocalizableData>";
            this.grdVTu.ColumnAutoResize = true;
            this.grdVTu.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdVTu_DesignTimeLayout.LayoutString = resources.GetString("grdVTu_DesignTimeLayout.LayoutString");
            this.grdVTu.DesignTimeLayout = grdVTu_DesignTimeLayout;
            this.grdVTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVTu.DynamicFiltering = true;
            this.grdVTu.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdVTu.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdVTu.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdVTu.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdVTu.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdVTu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdVTu.GroupByBoxVisible = false;
            this.grdVTu.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdVTu.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdVTu.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdVTu.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdVTu.Location = new System.Drawing.Point(0, 36);
            this.grdVTu.Name = "grdVTu";
            this.grdVTu.RecordNavigator = true;
            this.grdVTu.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdVTu.Size = new System.Drawing.Size(505, 494);
            this.grdVTu.TabIndex = 9;
            this.grdVTu.TableSpacing = 0;
            this.grdVTu.UseGroupRowSelector = true;
            this.grdVTu.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdVTu_CellValueChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cmdAddVTTH);
            this.panel5.Controls.Add(this.lbldonthuocmau);
            this.panel5.Controls.Add(this.autoVTTH);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(505, 36);
            this.panel5.TabIndex = 10;
            // 
            // cmdAddVTTH
            // 
            this.cmdAddVTTH.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddVTTH.Image")));
            this.cmdAddVTTH.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddVTTH.Location = new System.Drawing.Point(471, 7);
            this.cmdAddVTTH.Name = "cmdAddVTTH";
            this.cmdAddVTTH.Size = new System.Drawing.Size(31, 23);
            this.cmdAddVTTH.TabIndex = 623;
            this.cmdAddVTTH.TabStop = false;
            this.cmdAddVTTH.Click += new System.EventHandler(this.cmdAddVTTH_Click);
            // 
            // lbldonthuocmau
            // 
            this.lbldonthuocmau.Enabled = false;
            this.lbldonthuocmau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldonthuocmau.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbldonthuocmau.Location = new System.Drawing.Point(8, 6);
            this.lbldonthuocmau.Name = "lbldonthuocmau";
            this.lbldonthuocmau.Size = new System.Drawing.Size(92, 23);
            this.lbldonthuocmau.TabIndex = 622;
            this.lbldonthuocmau.Text = "Đơn mẫu (F2):";
            this.lbldonthuocmau.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoVTTH
            // 
            this.autoVTTH._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoVTTH._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoVTTH._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoVTTH.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoVTTH.AutoCompleteList")));
            this.autoVTTH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoVTTH.buildShortcut = false;
            this.autoVTTH.CaseSensitive = false;
            this.autoVTTH.CompareNoID = true;
            this.autoVTTH.DefaultCode = "-1";
            this.autoVTTH.DefaultID = "-1";
            this.autoVTTH.DisplayType = 0;
            this.autoVTTH.Drug_ID = null;
            this.autoVTTH.ExtraWidth = 0;
            this.autoVTTH.FillValueAfterSelect = false;
            this.autoVTTH.Font = new System.Drawing.Font("Arial", 9F);
            this.autoVTTH.Location = new System.Drawing.Point(106, 8);
            this.autoVTTH.MaxHeight = 150;
            this.autoVTTH.MinTypedCharacters = 2;
            this.autoVTTH.MyCode = "-1";
            this.autoVTTH.MyID = "-1";
            this.autoVTTH.MyText = "";
            this.autoVTTH.MyTextOnly = "";
            this.autoVTTH.Name = "autoVTTH";
            this.autoVTTH.RaiseEvent = false;
            this.autoVTTH.RaiseEventEnter = true;
            this.autoVTTH.RaiseEventEnterWhenEmpty = false;
            this.autoVTTH.SelectedIndex = -1;
            this.autoVTTH.Size = new System.Drawing.Size(365, 21);
            this.autoVTTH.splitChar = '@';
            this.autoVTTH.splitCharIDAndCode = '#';
            this.autoVTTH.TabIndex = 621;
            this.autoVTTH.TabStop = false;
            this.autoVTTH.TakeCode = false;
            this.autoVTTH.txtMyCode = null;
            this.autoVTTH.txtMyCode_Edit = null;
            this.autoVTTH.txtMyID = null;
            this.autoVTTH.txtMyID_Edit = null;
            this.autoVTTH.txtMyName = null;
            this.autoVTTH.txtMyName_Edit = null;
            this.autoVTTH.txtNext = null;
            // 
            // grdChitiet
            // 
            this.grdChitiet.AlternatingColors = true;
            this.grdChitiet.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin dùng chung</FilterRowInfoText></LocalizableData>";
            this.grdChitiet.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grdChitiet.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdChitiet.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdChitiet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChitiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdChitiet.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdChitiet.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RecordNavigator = true;
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.Size = new System.Drawing.Size(797, 512);
            this.grdChitiet.TabIndex = 8;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdChitiet.UseGroupRowSelector = true;
            this.grdChitiet.RowCheckStateChanged += new Janus.Windows.GridEX.RowCheckStateChangeEventHandler(this.grdDichVuDaChon_RowCheckStateChanged);
            this.grdChitiet.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdDichVuDaChon_CellUpdated);
            this.grdChitiet.SelectionChanged += new System.EventHandler(this.grdDichVuDaChon_SelectionChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this.txtMaGoi;
            // 
            // txtMaGoi
            // 
            this.txtMaGoi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaGoi.Location = new System.Drawing.Point(107, 46);
            this.txtMaGoi.Name = "txtMaGoi";
            this.txtMaGoi.Size = new System.Drawing.Size(78, 22);
            this.txtMaGoi.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtThanhTien);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(509, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 556);
            this.panel1.TabIndex = 628;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 512);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 44);
            this.panel2.TabIndex = 0;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Location = new System.Drawing.Point(6, 6);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(119, 33);
            this.cmdDelete.TabIndex = 22;
            this.cmdDelete.TabStop = false;
            this.cmdDelete.Text = "Xóa dịch vụ";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(328, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 52;
            this.label4.Text = "THÀNH TIỀN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThanhTien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhTien.Location = new System.Drawing.Point(423, 245);
            this.txtThanhTien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Size = new System.Drawing.Size(11, 22);
            this.txtThanhTien.TabIndex = 9;
            this.txtThanhTien.TabStop = false;
            this.txtThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThanhTien.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(482, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 16);
            this.label9.TabIndex = 531;
            this.label9.Text = "TỔNG TIỀN THAM CHIẾU";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmdTake);
            this.uiGroupBox3.Controls.Add(this.cboKieugoi);
            this.uiGroupBox3.Controls.Add(this.label9);
            this.uiGroupBox3.Controls.Add(this.label15);
            this.uiGroupBox3.Controls.Add(this.txtGiamtruBHYT);
            this.uiGroupBox3.Controls.Add(this.txtThanhTienThamChieu);
            this.uiGroupBox3.Controls.Add(this.txtTienGoi);
            this.uiGroupBox3.Controls.Add(this.label10);
            this.uiGroupBox3.Controls.Add(this.cmdAccept);
            this.uiGroupBox3.Controls.Add(this.pnlFilter);
            this.uiGroupBox3.Controls.Add(this.label11);
            this.uiGroupBox3.Controls.Add(this.txtnhomchidinh);
            this.uiGroupBox3.Controls.Add(this.label14);
            this.uiGroupBox3.Controls.Add(this.cmdNhomChidinh);
            this.uiGroupBox3.Controls.Add(this.dtpHieuLucDenNgay);
            this.uiGroupBox3.Controls.Add(this.dtpHieuLucTuNgay);
            this.uiGroupBox3.Controls.Add(this.label8);
            this.uiGroupBox3.Controls.Add(this.label6);
            this.uiGroupBox3.Controls.Add(this.label5);
            this.uiGroupBox3.Controls.Add(this.chkChiDinhNhanh);
            this.uiGroupBox3.Controls.Add(this.txtFilterName);
            this.uiGroupBox3.Controls.Add(this.chkHieuLuc);
            this.uiGroupBox3.Controls.Add(this.label2);
            this.uiGroupBox3.Controls.Add(this.txtID);
            this.uiGroupBox3.Controls.Add(this.txtTenGoi);
            this.uiGroupBox3.Controls.Add(this.label3);
            this.uiGroupBox3.Controls.Add(this.label1);
            this.uiGroupBox3.Controls.Add(this.txtMaGoi);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox3.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox3.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox3.Image")));
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(1306, 125);
            this.uiGroupBox3.TabIndex = 0;
            this.uiGroupBox3.Text = "Thông tin gói";
            // 
            // cmdTake
            // 
            this.cmdTake.Image = ((System.Drawing.Image)(resources.GetObject("cmdTake.Image")));
            this.cmdTake.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdTake.Location = new System.Drawing.Point(813, 21);
            this.cmdTake.Name = "cmdTake";
            this.cmdTake.Size = new System.Drawing.Size(31, 23);
            this.cmdTake.TabIndex = 625;
            this.cmdTake.TabStop = false;
            this.cmdTake.Click += new System.EventHandler(this.cmdTake_Click);
            // 
            // cboKieugoi
            // 
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Gói dùng 1 lần";
            uiComboBoxItem1.Value = ((byte)(0));
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Gói trừ dần";
            uiComboBoxItem2.Value = ((byte)(1));
            this.cboKieugoi.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboKieugoi.Location = new System.Drawing.Point(248, 22);
            this.cboKieugoi.Name = "cboKieugoi";
            this.cboKieugoi.Size = new System.Drawing.Size(231, 21);
            this.cboKieugoi.TabIndex = 631;
            this.cboKieugoi.TabStop = false;
            this.cboKieugoi.Text = "---Tất cả----";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(167, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 15);
            this.label15.TabIndex = 630;
            this.label15.Text = "Kiểu gói:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGiamtruBHYT
            // 
            this.txtGiamtruBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiamtruBHYT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiamtruBHYT.Location = new System.Drawing.Point(840, 129);
            this.txtGiamtruBHYT.Masked = MaskedTextBox.Mask.Decimal;
            this.txtGiamtruBHYT.Name = "txtGiamtruBHYT";
            this.txtGiamtruBHYT.Size = new System.Drawing.Size(162, 22);
            this.txtGiamtruBHYT.TabIndex = 8;
            this.txtGiamtruBHYT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiamtruBHYT.Visible = false;
            // 
            // txtThanhTienThamChieu
            // 
            this.txtThanhTienThamChieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThanhTienThamChieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhTienThamChieu.Location = new System.Drawing.Point(657, 22);
            this.txtThanhTienThamChieu.Masked = MaskedTextBox.Mask.Decimal;
            this.txtThanhTienThamChieu.Name = "txtThanhTienThamChieu";
            this.txtThanhTienThamChieu.Size = new System.Drawing.Size(155, 22);
            this.txtThanhTienThamChieu.TabIndex = 10;
            this.txtThanhTienThamChieu.TabStop = false;
            this.txtThanhTienThamChieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTienGoi
            // 
            this.txtTienGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTienGoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTienGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienGoi.Location = new System.Drawing.Point(946, 21);
            this.txtTienGoi.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTienGoi.Name = "txtTienGoi";
            this.txtTienGoi.Size = new System.Drawing.Size(161, 22);
            this.txtTienGoi.TabIndex = 6;
            this.txtTienGoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 17);
            this.label10.TabIndex = 629;
            this.label10.Text = "Tìm nhanh(F2)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.txtVT);
            this.pnlFilter.Controls.Add(this.txtGiuong);
            this.pnlFilter.Controls.Add(this.txtThuoc);
            this.pnlFilter.Controls.Add(this.txtDichvuCLS);
            this.pnlFilter.Controls.Add(this.txtCongkham);
            this.pnlFilter.Location = new System.Drawing.Point(107, 97);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(372, 22);
            this.pnlFilter.TabIndex = 628;
            // 
            // txtVT
            // 
            this.txtVT._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtVT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtVT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtVT.AutoCompleteList")));
            this.txtVT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVT.buildShortcut = false;
            this.txtVT.CaseSensitive = false;
            this.txtVT.CompareNoID = true;
            this.txtVT.DefaultCode = "-1";
            this.txtVT.DefaultID = "-1";
            this.txtVT.DisplayType = 0;
            this.txtVT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVT.Drug_ID = null;
            this.txtVT.ExtraWidth = 200;
            this.txtVT.FillValueAfterSelect = false;
            this.txtVT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVT.Location = new System.Drawing.Point(0, 0);
            this.txtVT.MaxHeight = 289;
            this.txtVT.MinTypedCharacters = 2;
            this.txtVT.MyCode = "-1";
            this.txtVT.MyID = "-1";
            this.txtVT.MyText = "";
            this.txtVT.MyTextOnly = "";
            this.txtVT.Name = "txtVT";
            this.txtVT.RaiseEvent = true;
            this.txtVT.RaiseEventEnter = true;
            this.txtVT.RaiseEventEnterWhenEmpty = false;
            this.txtVT.SelectedIndex = -1;
            this.txtVT.Size = new System.Drawing.Size(372, 21);
            this.txtVT.splitChar = '@';
            this.txtVT.splitCharIDAndCode = '#';
            this.txtVT.TabIndex = 9;
            this.txtVT.TakeCode = false;
            this.txtVT.txtMyCode = null;
            this.txtVT.txtMyCode_Edit = null;
            this.txtVT.txtMyID = null;
            this.txtVT.txtMyID_Edit = null;
            this.txtVT.txtMyName = null;
            this.txtVT.txtMyName_Edit = null;
            this.txtVT.txtNext = null;
            // 
            // txtGiuong
            // 
            this.txtGiuong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtGiuong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiuong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGiuong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtGiuong.AutoCompleteList")));
            this.txtGiuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiuong.buildShortcut = false;
            this.txtGiuong.CaseSensitive = false;
            this.txtGiuong.CompareNoID = true;
            this.txtGiuong.DefaultCode = "-1";
            this.txtGiuong.DefaultID = "-1";
            this.txtGiuong.DisplayType = 0;
            this.txtGiuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGiuong.Drug_ID = null;
            this.txtGiuong.ExtraWidth = 200;
            this.txtGiuong.FillValueAfterSelect = false;
            this.txtGiuong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiuong.Location = new System.Drawing.Point(0, 0);
            this.txtGiuong.MaxHeight = 289;
            this.txtGiuong.MinTypedCharacters = 2;
            this.txtGiuong.MyCode = "-1";
            this.txtGiuong.MyID = "-1";
            this.txtGiuong.MyText = "";
            this.txtGiuong.MyTextOnly = "";
            this.txtGiuong.Name = "txtGiuong";
            this.txtGiuong.RaiseEvent = true;
            this.txtGiuong.RaiseEventEnter = true;
            this.txtGiuong.RaiseEventEnterWhenEmpty = false;
            this.txtGiuong.SelectedIndex = -1;
            this.txtGiuong.Size = new System.Drawing.Size(372, 21);
            this.txtGiuong.splitChar = '@';
            this.txtGiuong.splitCharIDAndCode = '#';
            this.txtGiuong.TabIndex = 10;
            this.txtGiuong.TakeCode = false;
            this.txtGiuong.txtMyCode = null;
            this.txtGiuong.txtMyCode_Edit = null;
            this.txtGiuong.txtMyID = null;
            this.txtGiuong.txtMyID_Edit = null;
            this.txtGiuong.txtMyName = null;
            this.txtGiuong.txtMyName_Edit = null;
            this.txtGiuong.txtNext = null;
            // 
            // txtThuoc
            // 
            this.txtThuoc._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtThuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThuoc._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtThuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtThuoc.AutoCompleteList")));
            this.txtThuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThuoc.buildShortcut = false;
            this.txtThuoc.CaseSensitive = false;
            this.txtThuoc.CompareNoID = true;
            this.txtThuoc.DefaultCode = "-1";
            this.txtThuoc.DefaultID = "-1";
            this.txtThuoc.DisplayType = 0;
            this.txtThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtThuoc.Drug_ID = null;
            this.txtThuoc.ExtraWidth = 200;
            this.txtThuoc.FillValueAfterSelect = false;
            this.txtThuoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThuoc.Location = new System.Drawing.Point(0, 0);
            this.txtThuoc.MaxHeight = 289;
            this.txtThuoc.MinTypedCharacters = 2;
            this.txtThuoc.MyCode = "-1";
            this.txtThuoc.MyID = "-1";
            this.txtThuoc.MyText = "";
            this.txtThuoc.MyTextOnly = "";
            this.txtThuoc.Name = "txtThuoc";
            this.txtThuoc.RaiseEvent = true;
            this.txtThuoc.RaiseEventEnter = true;
            this.txtThuoc.RaiseEventEnterWhenEmpty = false;
            this.txtThuoc.SelectedIndex = -1;
            this.txtThuoc.Size = new System.Drawing.Size(372, 21);
            this.txtThuoc.splitChar = '@';
            this.txtThuoc.splitCharIDAndCode = '#';
            this.txtThuoc.TabIndex = 11;
            this.txtThuoc.TakeCode = false;
            this.txtThuoc.txtMyCode = null;
            this.txtThuoc.txtMyCode_Edit = null;
            this.txtThuoc.txtMyID = null;
            this.txtThuoc.txtMyID_Edit = null;
            this.txtThuoc.txtMyName = null;
            this.txtThuoc.txtMyName_Edit = null;
            this.txtThuoc.txtNext = null;
            // 
            // txtDichvuCLS
            // 
            this.txtDichvuCLS._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtDichvuCLS._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuCLS._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDichvuCLS.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDichvuCLS.AutoCompleteList")));
            this.txtDichvuCLS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDichvuCLS.buildShortcut = false;
            this.txtDichvuCLS.CaseSensitive = false;
            this.txtDichvuCLS.CompareNoID = true;
            this.txtDichvuCLS.DefaultCode = "-1";
            this.txtDichvuCLS.DefaultID = "-1";
            this.txtDichvuCLS.DisplayType = 0;
            this.txtDichvuCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDichvuCLS.Drug_ID = null;
            this.txtDichvuCLS.ExtraWidth = 200;
            this.txtDichvuCLS.FillValueAfterSelect = false;
            this.txtDichvuCLS.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuCLS.Location = new System.Drawing.Point(0, 0);
            this.txtDichvuCLS.MaxHeight = 289;
            this.txtDichvuCLS.MinTypedCharacters = 2;
            this.txtDichvuCLS.MyCode = "-1";
            this.txtDichvuCLS.MyID = "-1";
            this.txtDichvuCLS.MyText = "";
            this.txtDichvuCLS.MyTextOnly = "";
            this.txtDichvuCLS.Name = "txtDichvuCLS";
            this.txtDichvuCLS.RaiseEvent = true;
            this.txtDichvuCLS.RaiseEventEnter = true;
            this.txtDichvuCLS.RaiseEventEnterWhenEmpty = false;
            this.txtDichvuCLS.SelectedIndex = -1;
            this.txtDichvuCLS.Size = new System.Drawing.Size(372, 21);
            this.txtDichvuCLS.splitChar = '@';
            this.txtDichvuCLS.splitCharIDAndCode = '#';
            this.txtDichvuCLS.TabIndex = 12;
            this.txtDichvuCLS.TakeCode = false;
            this.txtDichvuCLS.txtMyCode = null;
            this.txtDichvuCLS.txtMyCode_Edit = null;
            this.txtDichvuCLS.txtMyID = null;
            this.txtDichvuCLS.txtMyID_Edit = null;
            this.txtDichvuCLS.txtMyName = null;
            this.txtDichvuCLS.txtMyName_Edit = null;
            this.txtDichvuCLS.txtNext = null;
            // 
            // txtCongkham
            // 
            this.txtCongkham._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtCongkham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCongkham._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCongkham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtCongkham.AutoCompleteList")));
            this.txtCongkham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCongkham.buildShortcut = false;
            this.txtCongkham.CaseSensitive = false;
            this.txtCongkham.CompareNoID = true;
            this.txtCongkham.DefaultCode = "-1";
            this.txtCongkham.DefaultID = "-1";
            this.txtCongkham.DisplayType = 0;
            this.txtCongkham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCongkham.Drug_ID = null;
            this.txtCongkham.ExtraWidth = 200;
            this.txtCongkham.FillValueAfterSelect = false;
            this.txtCongkham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCongkham.Location = new System.Drawing.Point(0, 0);
            this.txtCongkham.MaxHeight = 289;
            this.txtCongkham.MinTypedCharacters = 2;
            this.txtCongkham.MyCode = "-1";
            this.txtCongkham.MyID = "-1";
            this.txtCongkham.MyText = "";
            this.txtCongkham.MyTextOnly = "";
            this.txtCongkham.Name = "txtCongkham";
            this.txtCongkham.RaiseEvent = true;
            this.txtCongkham.RaiseEventEnter = true;
            this.txtCongkham.RaiseEventEnterWhenEmpty = false;
            this.txtCongkham.SelectedIndex = -1;
            this.txtCongkham.Size = new System.Drawing.Size(372, 21);
            this.txtCongkham.splitChar = '@';
            this.txtCongkham.splitCharIDAndCode = '#';
            this.txtCongkham.TabIndex = 13;
            this.txtCongkham.TakeCode = false;
            this.txtCongkham.txtMyCode = null;
            this.txtCongkham.txtMyCode_Edit = null;
            this.txtCongkham.txtMyID = null;
            this.txtCongkham.txtMyID_Edit = null;
            this.txtCongkham.txtMyName = null;
            this.txtCongkham.txtMyName_Edit = null;
            this.txtCongkham.txtNext = null;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(230, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 17);
            this.label11.TabIndex = 627;
            this.label11.Text = "đến ngày";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtnhomchidinh
            // 
            this.txtnhomchidinh._backcolor = System.Drawing.SystemColors.Control;
            this.txtnhomchidinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnhomchidinh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtnhomchidinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtnhomchidinh.AutoCompleteList")));
            this.txtnhomchidinh.buildShortcut = false;
            this.txtnhomchidinh.CaseSensitive = false;
            this.txtnhomchidinh.CompareNoID = true;
            this.txtnhomchidinh.DefaultCode = "-1";
            this.txtnhomchidinh.DefaultID = "-1";
            this.txtnhomchidinh.DisplayType = 0;
            this.txtnhomchidinh.Drug_ID = null;
            this.txtnhomchidinh.ExtraWidth = 0;
            this.txtnhomchidinh.FillValueAfterSelect = false;
            this.txtnhomchidinh.Font = new System.Drawing.Font("Arial", 10F);
            this.txtnhomchidinh.Location = new System.Drawing.Point(28, 112);
            this.txtnhomchidinh.MaxHeight = 100;
            this.txtnhomchidinh.MinTypedCharacters = 2;
            this.txtnhomchidinh.MyCode = "-1";
            this.txtnhomchidinh.MyID = "-1";
            this.txtnhomchidinh.MyText = "";
            this.txtnhomchidinh.MyTextOnly = "";
            this.txtnhomchidinh.Name = "txtnhomchidinh";
            this.txtnhomchidinh.RaiseEvent = false;
            this.txtnhomchidinh.RaiseEventEnter = true;
            this.txtnhomchidinh.RaiseEventEnterWhenEmpty = false;
            this.txtnhomchidinh.SelectedIndex = -1;
            this.txtnhomchidinh.Size = new System.Drawing.Size(10, 23);
            this.txtnhomchidinh.splitChar = '@';
            this.txtnhomchidinh.splitCharIDAndCode = '#';
            this.txtnhomchidinh.TabIndex = 625;
            this.txtnhomchidinh.TakeCode = false;
            this.txtnhomchidinh.txtMyCode = null;
            this.txtnhomchidinh.txtMyCode_Edit = null;
            this.txtnhomchidinh.txtMyID = null;
            this.txtnhomchidinh.txtMyID_Edit = null;
            this.txtnhomchidinh.txtMyName = null;
            this.txtnhomchidinh.txtMyName_Edit = null;
            this.txtnhomchidinh.txtNext = null;
            this.txtnhomchidinh.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(16, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(10, 17);
            this.label14.TabIndex = 624;
            this.label14.Text = "Nhóm chỉ định";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Visible = false;
            // 
            // cmdNhomChidinh
            // 
            this.cmdNhomChidinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNhomChidinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhomChidinh.Image")));
            this.cmdNhomChidinh.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdNhomChidinh.Location = new System.Drawing.Point(44, 101);
            this.cmdNhomChidinh.Name = "cmdNhomChidinh";
            this.cmdNhomChidinh.Size = new System.Drawing.Size(33, 28);
            this.cmdNhomChidinh.TabIndex = 623;
            this.cmdNhomChidinh.ToolTipText = "Chỉ định theo nhóm";
            this.cmdNhomChidinh.Visible = false;
            // 
            // dtpHieuLucDenNgay
            // 
            // 
            // 
            // 
            this.dtpHieuLucDenNgay.DropDownCalendar.Name = "";
            this.dtpHieuLucDenNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHieuLucDenNgay.Location = new System.Drawing.Point(317, 70);
            this.dtpHieuLucDenNgay.Name = "dtpHieuLucDenNgay";
            this.dtpHieuLucDenNgay.Size = new System.Drawing.Size(162, 22);
            this.dtpHieuLucDenNgay.TabIndex = 4;
            this.dtpHieuLucDenNgay.Value = new System.DateTime(2018, 3, 8, 0, 0, 0, 0);
            // 
            // dtpHieuLucTuNgay
            // 
            // 
            // 
            // 
            this.dtpHieuLucTuNgay.DropDownCalendar.Name = "";
            this.dtpHieuLucTuNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHieuLucTuNgay.Location = new System.Drawing.Point(107, 70);
            this.dtpHieuLucTuNgay.Name = "dtpHieuLucTuNgay";
            this.dtpHieuLucTuNgay.Size = new System.Drawing.Size(104, 22);
            this.dtpHieuLucTuNgay.TabIndex = 3;
            this.dtpHieuLucTuNgay.Value = new System.DateTime(2018, 3, 8, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(854, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 15);
            this.label8.TabIndex = 518;
            this.label8.Text = "TIỀN GÓI";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(748, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 15);
            this.label6.TabIndex = 514;
            this.label6.Text = "GIẢM BHYT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 17);
            this.label5.TabIndex = 512;
            this.label5.Text = "Lọc dịch vụ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // chkChiDinhNhanh
            // 
            this.chkChiDinhNhanh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkChiDinhNhanh.Location = new System.Drawing.Point(577, 78);
            this.chkChiDinhNhanh.Name = "chkChiDinhNhanh";
            this.chkChiDinhNhanh.Size = new System.Drawing.Size(546, 45);
            this.chkChiDinhNhanh.TabIndex = 30;
            this.chkChiDinhNhanh.TabStop = false;
            this.chkChiDinhNhanh.Text = "Chỉ định nhanh(Check chọn các dịch vụ từ danh mục là tự động chuyển sang). Nếu kh" +
    "ông chọn thì cần nhấn Thêm(Ctrl+A) sau khi chọn xong";
            this.chkChiDinhNhanh.CheckedChanged += new System.EventHandler(this.chkChiDinhNhanh_CheckedChanged);
            // 
            // txtFilterName
            // 
            this.txtFilterName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtFilterName.Location = new System.Drawing.Point(28, 141);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(10, 21);
            this.txtFilterName.TabIndex = 509;
            this.txtFilterName.Visible = false;
            this.txtFilterName.TextChanged += new System.EventHandler(this.txtFilterName_TextChanged);
            // 
            // chkHieuLuc
            // 
            this.chkHieuLuc.AutoSize = true;
            this.chkHieuLuc.Checked = true;
            this.chkHieuLuc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHieuLuc.Location = new System.Drawing.Point(12, 72);
            this.chkHieuLuc.Name = "chkHieuLuc";
            this.chkHieuLuc.Size = new System.Drawing.Size(89, 19);
            this.chkHieuLuc.TabIndex = 5;
            this.chkHieuLuc.TabStop = false;
            this.chkHieuLuc.Text = "Hiệu lực từ:";
            this.chkHieuLuc.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(107, 22);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(54, 22);
            this.txtID.TabIndex = 0;
            this.txtID.TabStop = false;
            // 
            // txtTenGoi
            // 
            this.txtTenGoi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTenGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenGoi.Location = new System.Drawing.Point(248, 46);
            this.txtTenGoi.Name = "txtTenGoi";
            this.txtTenGoi.Size = new System.Drawing.Size(859, 22);
            this.txtTenGoi.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(191, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "Tên gói";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 46;
            this.label1.Text = "Mã gói";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_TaoGoiKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiGroupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frm_TaoGoiKham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm mới gói dịch vụ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_TaoGoiKham_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_TaoGoiKham_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabKham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKham)).EndInit();
            this.tabCLS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCLS)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabThuoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThuoc)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabGiuong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGiuong)).EndInit();
            this.tabVTu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVTu)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private System.Windows.Forms.CheckBox chkHieuLuc;
        private System.Windows.Forms.Label label2;
        internal Janus.Windows.GridEX.EditControls.EditBox txtID;
        internal Janus.Windows.GridEX.EditControls.EditBox txtTenGoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMaGoi;
        private Janus.Windows.UI.Tab.UITab tabControl;
        private Janus.Windows.UI.Tab.UITabPage tabKham;
        private Janus.Windows.UI.Tab.UITabPage tabCLS;
        private Janus.Windows.GridEX.GridEX grdCLS;
        private Janus.Windows.UI.Tab.UITabPage tabThuoc;
        private Janus.Windows.GridEX.GridEX grdThuoc;
        private Janus.Windows.UI.Tab.UITabPage tabVTu;
        private Janus.Windows.GridEX.GridEX grdVTu;
        private Janus.Windows.UI.Tab.UITabPage tabGiuong;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdThoat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UICheckBox chkChiDinhNhanh;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.GridEX.EditControls.EditBox txtFilterName;
        private Janus.Windows.GridEX.GridEX grdKham;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpHieuLucDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpHieuLucTuNgay;
        private Janus.Windows.GridEX.GridEX grdGiuong;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.EditControls.UIButton cmdNhomChidinh;
        private AutoCompleteTextbox txtnhomchidinh;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label label10;
        private AutoCompleteTextbox txtVT;
        private AutoCompleteTextbox txtGiuong;
        private AutoCompleteTextbox txtThuoc;
        private AutoCompleteTextbox txtDichvuCLS;
        private AutoCompleteTextbox txtCongkham;
        private MaskedTextBox.MaskedTextBox txtGiamtruBHYT;
        private MaskedTextBox.MaskedTextBox txtTienGoi;
        private MaskedTextBox.MaskedTextBox txtThanhTien;
        private MaskedTextBox.MaskedTextBox txtMienGiam;
        private MaskedTextBox.MaskedTextBox txtThanhTienThamChieu;
        private System.Windows.Forms.Panel panel3;
        private AutoCompleteTextbox autoNhomCLS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private Janus.Windows.EditControls.UIButton cmdAddCLS;
        private System.Windows.Forms.Panel panel4;
        private Janus.Windows.EditControls.UIButton cmdAddThuoc;
        internal System.Windows.Forms.Label label13;
        private AutoCompleteTextbox autoThuoc;
        private Janus.Windows.EditControls.UIButton cmdAddVTTH;
        internal System.Windows.Forms.Label lbldonthuocmau;
        private AutoCompleteTextbox autoVTTH;
        private Janus.Windows.EditControls.UIComboBox cboKieugoi;
        internal System.Windows.Forms.Label label15;
        private Janus.Windows.EditControls.UIButton cmdTake;
    }
}