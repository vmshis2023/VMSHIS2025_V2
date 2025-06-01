namespace VNS.HIS.UI.THUOC
{
    partial class FrmUpdateSoLuongTon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateSoLuongTon));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdKho_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDieuchinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtkho = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdRefresh = new Janus.Windows.EditControls.UIButton();
            this.cmdCauHinh = new Janus.Windows.EditControls.UIButton();
            this.chkTamdung = new Janus.Windows.EditControls.UICheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.grdKho = new Janus.Windows.GridEX.GridEX();
            this.pnlDieuchinh = new System.Windows.Forms.Panel();
            this.optUutien = new System.Windows.Forms.RadioButton();
            this.optExpireDate = new System.Windows.Forms.RadioButton();
            this.optLIFO = new System.Windows.Forms.RadioButton();
            this.optFIFO = new System.Windows.Forms.RadioButton();
            this.grdDieuchinh = new Janus.Windows.GridEX.GridEX();
            this.ctxUpdate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdUpdateGiaNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateNgayHetHan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdupdatengaynhap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateGiaBan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateSolo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateIdThuocKho = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.lnkNgayhethan = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdDown = new Janus.Windows.EditControls.UIButton();
            this.cmdUp = new Janus.Windows.EditControls.UIButton();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkAutoupdate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdInTonKho = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdKho)).BeginInit();
            this.pnlDieuchinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuchinh)).BeginInit();
            this.ctxUpdate.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(625, 694);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Cập nhật";
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(887, 693);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát Form";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtkho);
            this.uiGroupBox1.Controls.Add(this.cmdRefresh);
            this.uiGroupBox1.Controls.Add(this.cmdCauHinh);
            this.uiGroupBox1.Controls.Add(this.chkTamdung);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 61);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // txtkho
            // 
            this.txtkho._backcolor = System.Drawing.SystemColors.Control;
            this.txtkho._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtkho._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtkho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtkho.AutoCompleteList = null;
            this.txtkho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtkho.buildShortcut = false;
            this.txtkho.CaseSensitive = false;
            this.txtkho.CompareNoID = true;
            this.txtkho.DefaultCode = "-1";
            this.txtkho.DefaultID = "-1";
            this.txtkho.DisplayType = 0;
            this.txtkho.Drug_ID = null;
            this.txtkho.ExtraWidth = 0;
            this.txtkho.FillValueAfterSelect = false;
            this.txtkho.Location = new System.Drawing.Point(172, 19);
            this.txtkho.MaxHeight = 289;
            this.txtkho.MinTypedCharacters = 2;
            this.txtkho.MyCode = "-1";
            this.txtkho.MyID = "-1";
            this.txtkho.MyText = "";
            this.txtkho.MyTextOnly = "";
            this.txtkho.Name = "txtkho";
            this.txtkho.RaiseEvent = true;
            this.txtkho.RaiseEventEnter = true;
            this.txtkho.RaiseEventEnterWhenEmpty = true;
            this.txtkho.SelectedIndex = -1;
            this.txtkho.Size = new System.Drawing.Size(391, 22);
            this.txtkho.splitChar = '@';
            this.txtkho.splitCharIDAndCode = '#';
            this.txtkho.TabIndex = 464;
            this.txtkho.TakeCode = false;
            this.txtkho.txtMyCode = null;
            this.txtkho.txtMyCode_Edit = null;
            this.txtkho.txtMyID = null;
            this.txtkho.txtMyID_Edit = null;
            this.txtkho.txtMyName = null;
            this.txtkho.txtMyName_Edit = null;
            this.txtkho.txtNext = null;
            this.txtkho._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(this.txtkho__OnSelectionChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
            this.cmdRefresh.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdRefresh.Location = new System.Drawing.Point(569, 17);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(25, 25);
            this.cmdRefresh.TabIndex = 463;
            this.cmdRefresh.TabStop = false;
            this.toolTip1.SetToolTip(this.cmdRefresh, "Làm mới lại dữ liệu thuốc trong kho");
            this.cmdRefresh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdCauHinh
            // 
            this.cmdCauHinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCauHinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCauHinh.Image")));
            this.cmdCauHinh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCauHinh.Location = new System.Drawing.Point(963, 12);
            this.cmdCauHinh.Name = "cmdCauHinh";
            this.cmdCauHinh.Size = new System.Drawing.Size(39, 33);
            this.cmdCauHinh.TabIndex = 462;
            this.cmdCauHinh.TabStop = false;
            this.cmdCauHinh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // chkTamdung
            // 
            this.chkTamdung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTamdung.Checked = true;
            this.chkTamdung.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTamdung.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTamdung.Location = new System.Drawing.Point(616, 18);
            this.chkTamdung.Name = "chkTamdung";
            this.chkTamdung.Size = new System.Drawing.Size(228, 23);
            this.chkTamdung.TabIndex = 5;
            this.chkTamdung.Text = "Tạm dừng lấy dữ liệu tự động?";
            this.chkTamdung.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn kho thuốc cần xem";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Controls.Add(this.grdKho);
            this.uiGroupBox2.Controls.Add(this.pnlDieuchinh);
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 61);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 627);
            this.uiGroupBox2.TabIndex = 3;
            this.uiGroupBox2.Text = "&Thông tin số lượng tồn";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 19);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(613, 332);
            this.grdList.TabIndex = 4;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // grdKho
            // 
            this.grdKho.AlternatingColors = true;
            this.grdKho.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdKho.ColumnAutoResize = true;
            grdKho_DesignTimeLayout.LayoutString = resources.GetString("grdKho_DesignTimeLayout.LayoutString");
            this.grdKho.DesignTimeLayout = grdKho_DesignTimeLayout;
            this.grdKho.Dock = System.Windows.Forms.DockStyle.Right;
            this.grdKho.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKho.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdKho.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKho.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKho.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdKho.GroupByBoxVisible = false;
            this.grdKho.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKho.Location = new System.Drawing.Point(616, 19);
            this.grdKho.Name = "grdKho";
            this.grdKho.RecordNavigator = true;
            this.grdKho.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKho.Size = new System.Drawing.Size(389, 332);
            this.grdKho.TabIndex = 3;
            this.toolTip1.SetToolTip(this.grdKho, "Chọn kho cho phép kê đơn");
            this.grdKho.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKho.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdKho.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // pnlDieuchinh
            // 
            this.pnlDieuchinh.Controls.Add(this.optUutien);
            this.pnlDieuchinh.Controls.Add(this.optExpireDate);
            this.pnlDieuchinh.Controls.Add(this.optLIFO);
            this.pnlDieuchinh.Controls.Add(this.optFIFO);
            this.pnlDieuchinh.Controls.Add(this.grdDieuchinh);
            this.pnlDieuchinh.Controls.Add(this.pnlNav);
            this.pnlDieuchinh.Controls.Add(this.vbLine1);
            this.pnlDieuchinh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDieuchinh.Location = new System.Drawing.Point(3, 351);
            this.pnlDieuchinh.Name = "pnlDieuchinh";
            this.pnlDieuchinh.Size = new System.Drawing.Size(1002, 273);
            this.pnlDieuchinh.TabIndex = 1;
            // 
            // optUutien
            // 
            this.optUutien.AutoSize = true;
            this.optUutien.Checked = true;
            this.optUutien.Location = new System.Drawing.Point(647, 28);
            this.optUutien.Name = "optUutien";
            this.optUutien.Size = new System.Drawing.Size(172, 21);
            this.optUutien.TabIndex = 10;
            this.optUutien.TabStop = true;
            this.optUutien.Text = "Theo mức ưu tiên bán?";
            this.toolTip1.SetToolTip(this.optUutien, "Chọn mục này nếu muốn xuất thuốc theo thứ tự bán, sau đó đến ngày hết hạn gần nhấ" +
        "t");
            this.optUutien.UseVisualStyleBackColor = true;
            // 
            // optExpireDate
            // 
            this.optExpireDate.AutoSize = true;
            this.optExpireDate.Location = new System.Drawing.Point(382, 28);
            this.optExpireDate.Name = "optExpireDate";
            this.optExpireDate.Size = new System.Drawing.Size(245, 21);
            this.optExpireDate.TabIndex = 9;
            this.optExpireDate.TabStop = true;
            this.optExpireDate.Text = "Ngày hết hạn gần nhất xuất trước?";
            this.toolTip1.SetToolTip(this.optExpireDate, "Chọn mục này nếu muốn trừ các thuốc gần hết hạn trước, sau đó đến nhập trước xuất" +
        " trước");
            this.optExpireDate.UseVisualStyleBackColor = true;
            // 
            // optLIFO
            // 
            this.optLIFO.AutoSize = true;
            this.optLIFO.Location = new System.Drawing.Point(197, 28);
            this.optLIFO.Name = "optLIFO";
            this.optLIFO.Size = new System.Drawing.Size(161, 21);
            this.optLIFO.TabIndex = 8;
            this.optLIFO.TabStop = true;
            this.optLIFO.Text = "Nhập trước xuất sau?";
            this.toolTip1.SetToolTip(this.optLIFO, "Chọn mục này nếu muốn xuất thuốc nhập sau xuất trước, kế đến là ngày hết hạn");
            this.optLIFO.UseVisualStyleBackColor = true;
            // 
            // optFIFO
            // 
            this.optFIFO.AutoSize = true;
            this.optFIFO.Location = new System.Drawing.Point(9, 28);
            this.optFIFO.Name = "optFIFO";
            this.optFIFO.Size = new System.Drawing.Size(170, 21);
            this.optFIFO.TabIndex = 7;
            this.optFIFO.TabStop = true;
            this.optFIFO.Text = "Nhập trước xuất trước?";
            this.toolTip1.SetToolTip(this.optFIFO, "Chọn mục này nếu muốn xuất các thuốc nhập trước, kế đến là ngày hết hạn");
            this.optFIFO.UseVisualStyleBackColor = true;
            // 
            // grdDieuchinh
            // 
            this.grdDieuchinh.AllowColumnDrag = false;
            this.grdDieuchinh.AlternatingColors = true;
            this.grdDieuchinh.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdDieuchinh.AutomaticSort = false;
            this.grdDieuchinh.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin cập nhập số lượng tồn</FilterRowInfoText></LocalizableData>";
            this.grdDieuchinh.ContextMenuStrip = this.ctxUpdate;
            grdDieuchinh_DesignTimeLayout.LayoutString = resources.GetString("grdDieuchinh_DesignTimeLayout.LayoutString");
            this.grdDieuchinh.DesignTimeLayout = grdDieuchinh_DesignTimeLayout;
            this.grdDieuchinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDieuchinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDieuchinh.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdDieuchinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDieuchinh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDieuchinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdDieuchinh.GroupByBoxVisible = false;
            this.grdDieuchinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDieuchinh.Location = new System.Drawing.Point(0, 64);
            this.grdDieuchinh.Name = "grdDieuchinh";
            this.grdDieuchinh.RecordNavigator = true;
            this.grdDieuchinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDieuchinh.Size = new System.Drawing.Size(841, 209);
            this.grdDieuchinh.TabIndex = 6;
            this.grdDieuchinh.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDieuchinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDieuchinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.grdDieuchinh.CurrentCellChanged += new System.EventHandler(this.grdDieuchinh_CurrentCellChanged);
            // 
            // ctxUpdate
            // 
            this.ctxUpdate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUpdateGiaNhap,
            this.cmdUpdateNgayHetHan,
            this.cmdupdatengaynhap,
            this.cmdUpdateGiaBan,
            this.cmdUpdateSolo,
            this.cmdUpdateIdThuocKho});
            this.ctxUpdate.Name = "ctxDelDrug";
            this.ctxUpdate.Size = new System.Drawing.Size(190, 136);
            // 
            // cmdUpdateGiaNhap
            // 
            this.cmdUpdateGiaNhap.Name = "cmdUpdateGiaNhap";
            this.cmdUpdateGiaNhap.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateGiaNhap.Text = "Sửa giá nhập";
            this.cmdUpdateGiaNhap.Visible = false;
            // 
            // cmdUpdateNgayHetHan
            // 
            this.cmdUpdateNgayHetHan.Name = "cmdUpdateNgayHetHan";
            this.cmdUpdateNgayHetHan.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateNgayHetHan.Text = "Sửa ngày hết hạn";
            this.cmdUpdateNgayHetHan.Visible = false;
            // 
            // cmdupdatengaynhap
            // 
            this.cmdupdatengaynhap.Name = "cmdupdatengaynhap";
            this.cmdupdatengaynhap.Size = new System.Drawing.Size(189, 22);
            this.cmdupdatengaynhap.Text = "Sửa ngày nhập";
            this.cmdupdatengaynhap.Visible = false;
            // 
            // cmdUpdateGiaBan
            // 
            this.cmdUpdateGiaBan.Name = "cmdUpdateGiaBan";
            this.cmdUpdateGiaBan.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateGiaBan.Text = "Sửa giá bán ";
            this.cmdUpdateGiaBan.Visible = false;
            // 
            // cmdUpdateSolo
            // 
            this.cmdUpdateSolo.Name = "cmdUpdateSolo";
            this.cmdUpdateSolo.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateSolo.Text = "Sửa số lô";
            this.cmdUpdateSolo.Visible = false;
            // 
            // cmdUpdateIdThuocKho
            // 
            this.cmdUpdateIdThuocKho.Name = "cmdUpdateIdThuocKho";
            this.cmdUpdateIdThuocKho.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateIdThuocKho.Text = "Sửa toàn bộ thông tin";
            this.cmdUpdateIdThuocKho.Click += new System.EventHandler(this.cmdUpdateIdThuocKho_Click);
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.lnkNgayhethan);
            this.pnlNav.Controls.Add(this.label2);
            this.pnlNav.Controls.Add(this.cmdDown);
            this.pnlNav.Controls.Add(this.cmdUp);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNav.Location = new System.Drawing.Point(841, 64);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(161, 209);
            this.pnlNav.TabIndex = 5;
            // 
            // lnkNgayhethan
            // 
            this.lnkNgayhethan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnkNgayhethan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNgayhethan.Location = new System.Drawing.Point(0, 63);
            this.lnkNgayhethan.Name = "lnkNgayhethan";
            this.lnkNgayhethan.Size = new System.Drawing.Size(161, 55);
            this.lnkNgayhethan.TabIndex = 4;
            this.lnkNgayhethan.TabStop = true;
            this.lnkNgayhethan.Text = "Sắp xếp ưu tiên bán theo ngày hết hạn gần nhất?";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 91);
            this.label2.TabIndex = 3;
            this.label2.Text = "Chú ý: Nếu 2 thuốc có cùng số thứ tự và ngày hết hạn khác nhau thì ưu tiên lấy ng" +
    "ày hết hạn gần nhất để bán trước";
            // 
            // cmdDown
            // 
            this.cmdDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdDown.Image")));
            this.cmdDown.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdDown.Location = new System.Drawing.Point(84, 3);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(74, 60);
            this.cmdDown.TabIndex = 2;
            this.cmdDown.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // cmdUp
            // 
            this.cmdUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdUp.Image")));
            this.cmdUp.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdUp.Location = new System.Drawing.Point(6, 3);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(74, 60);
            this.cmdUp.TabIndex = 1;
            this.cmdUp.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.Black;
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(0, 0);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(5);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(1002, 64);
            this.vbLine1.TabIndex = 4;
            this.vbLine1.YourText = "Điều chỉnh độ ưu tiên xuất thuốc trong kho";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // chkAutoupdate
            // 
            this.chkAutoupdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoupdate.Checked = true;
            this.chkAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoupdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoupdate.Location = new System.Drawing.Point(12, 702);
            this.chkAutoupdate.Name = "chkAutoupdate";
            this.chkAutoupdate.Size = new System.Drawing.Size(279, 23);
            this.chkAutoupdate.TabIndex = 6;
            this.chkAutoupdate.Text = "Tự động cập nhật không cần nhấn nút Lưu?";
            this.toolTip1.SetToolTip(this.chkAutoupdate, "Nếu chọn mục này thì dữ liệu sẽ được cập nhật ngay sau khi thay đổi giá trị thay " +
        "vì phải nhấn nút Lưu thông tin");
            this.chkAutoupdate.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cmdInTonKho
            // 
            this.cmdInTonKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInTonKho.Image = ((System.Drawing.Image)(resources.GetObject("cmdInTonKho.Image")));
            this.cmdInTonKho.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInTonKho.Location = new System.Drawing.Point(756, 694);
            this.cmdInTonKho.Name = "cmdInTonKho";
            this.cmdInTonKho.Size = new System.Drawing.Size(120, 35);
            this.cmdInTonKho.TabIndex = 7;
            this.cmdInTonKho.Text = "In tồn kho";
            this.cmdInTonKho.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdInTonKho.Click += new System.EventHandler(this.cmdInTonKho_Click);
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayInPhieu.DropDownCalendar.Name = "";
            this.dtNgayInPhieu.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(376, 704);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(103, 21);
            this.dtNgayInPhieu.TabIndex = 123;
            this.dtNgayInPhieu.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(322, 707);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 124;
            this.label3.Text = "Ngày in:";
            // 
            // FrmUpdateSoLuongTon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInTonKho);
            this.Controls.Add(this.chkAutoupdate);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.KeyPreview = true;
            this.Name = "FrmUpdateSoLuongTon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem và điều chỉnh ưu tiên bán thuốc trong kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_UpdateSoLuongTon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_UpdateSoLuongTon_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdKho)).EndInit();
            this.pnlDieuchinh.ResumeLayout(false);
            this.pnlDieuchinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuchinh)).EndInit();
            this.ctxUpdate.ResumeLayout(false);
            this.pnlNav.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlDieuchinh;
        private Janus.Windows.EditControls.UICheckBox chkTamdung;
        private Janus.Windows.GridEX.GridEX grdDieuchinh;
        private System.Windows.Forms.Panel pnlNav;
        private Janus.Windows.EditControls.UIButton cmdDown;
        private Janus.Windows.EditControls.UIButton cmdUp;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkNgayhethan;
        private Janus.Windows.EditControls.UICheckBox chkAutoupdate;
        private Janus.Windows.EditControls.UIButton cmdCauHinh;
        private System.Windows.Forms.RadioButton optUutien;
        private System.Windows.Forms.RadioButton optExpireDate;
        private System.Windows.Forms.RadioButton optLIFO;
        private System.Windows.Forms.RadioButton optFIFO;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.GridEX.GridEX grdKho;
        private Janus.Windows.EditControls.UIButton cmdRefresh;
        private Janus.Windows.EditControls.UIButton cmdInTonKho;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip ctxUpdate;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateGiaBan;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateGiaNhap;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateNgayHetHan;
        private System.Windows.Forms.ToolStripMenuItem cmdupdatengaynhap;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateSolo;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateIdThuocKho;
        private UCs.AutoCompleteTextbox txtkho;
    }
}