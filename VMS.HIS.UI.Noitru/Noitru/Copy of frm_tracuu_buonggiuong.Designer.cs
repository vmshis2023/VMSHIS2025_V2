namespace VNS.HIS.UI.NOITRU
{
    partial class frm_tracuu_buonggiuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_tracuu_buonggiuong));
            Janus.Windows.GridEX.GridEXLayout grdBed_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdGiaGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.autokhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoBuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdSearchGiuong = new Janus.Windows.EditControls.UIButton();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdBed = new Janus.Windows.GridEX.GridEX();
            this.grdGiaGiuong = new Janus.Windows.GridEX.GridEX();
            this.pnlDohoa = new System.Windows.Forms.Panel();
            this.pnlKhoa = new System.Windows.Forms.Panel();
            this.flowKhoa = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlGiuong = new System.Windows.Forms.Panel();
            this.flowGiuong = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBuong = new System.Windows.Forms.Panel();
            this.flowBuong = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.lblGuide = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlTimkiemBuong = new System.Windows.Forms.Panel();
            this.cmdBackKhoa = new Janus.Windows.EditControls.UIButton();
            this.autoBuong1 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTimkiemGiuong = new System.Windows.Forms.Panel();
            this.cmdBack = new Janus.Windows.EditControls.UIButton();
            this.autoGiuong1 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlTimkiemKhoa = new System.Windows.Forms.Panel();
            this.autokhoa1 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaGiuong)).BeginInit();
            this.pnlDohoa.SuspendLayout();
            this.pnlKhoa.SuspendLayout();
            this.pnlGiuong.SuspendLayout();
            this.pnlBuong.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlTimkiemBuong.SuspendLayout();
            this.pnlTimkiemGiuong.SuspendLayout();
            this.pnlTimkiemKhoa.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.autokhoa);
            this.uiGroupBox3.Controls.Add(this.autoBuong);
            this.uiGroupBox3.Controls.Add(this.label4);
            this.uiGroupBox3.Controls.Add(this.label10);
            this.uiGroupBox3.Controls.Add(this.cmdSearchGiuong);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(1180, 48);
            this.uiGroupBox3.TabIndex = 8;
            this.uiGroupBox3.Text = "Tìm kiếm thông tin ";
            // 
            // autokhoa
            // 
            this.autokhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autokhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autokhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autokhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autokhoa.AutoCompleteList")));
            this.autokhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autokhoa.buildShortcut = false;
            this.autokhoa.CaseSensitive = false;
            this.autokhoa.CompareNoID = true;
            this.autokhoa.DefaultCode = "-1";
            this.autokhoa.DefaultID = "-1";
            this.autokhoa.DisplayType = 1;
            this.autokhoa.Drug_ID = null;
            this.autokhoa.ExtraWidth = 0;
            this.autokhoa.FillValueAfterSelect = false;
            this.autokhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autokhoa.Location = new System.Drawing.Point(112, 17);
            this.autokhoa.MaxHeight = 289;
            this.autokhoa.MinTypedCharacters = 2;
            this.autokhoa.MyCode = "-1";
            this.autokhoa.MyID = "-1";
            this.autokhoa.MyText = "";
            this.autokhoa.MyTextOnly = "";
            this.autokhoa.Name = "autokhoa";
            this.autokhoa.RaiseEvent = true;
            this.autokhoa.RaiseEventEnter = true;
            this.autokhoa.RaiseEventEnterWhenEmpty = false;
            this.autokhoa.SelectedIndex = -1;
            this.autokhoa.Size = new System.Drawing.Size(566, 21);
            this.autokhoa.splitChar = '@';
            this.autokhoa.splitCharIDAndCode = '#';
            this.autokhoa.TabIndex = 0;
            this.autokhoa.TakeCode = false;
            this.autokhoa.txtMyCode = null;
            this.autokhoa.txtMyCode_Edit = null;
            this.autokhoa.txtMyID = null;
            this.autokhoa.txtMyID_Edit = null;
            this.autokhoa.txtMyName = null;
            this.autokhoa.txtMyName_Edit = null;
            this.autokhoa.txtNext = null;
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
            this.autoBuong.Location = new System.Drawing.Point(746, 17);
            this.autoBuong.MaxHeight = 289;
            this.autoBuong.MinTypedCharacters = 2;
            this.autoBuong.MyCode = "-1";
            this.autoBuong.MyID = "-1";
            this.autoBuong.MyText = "";
            this.autoBuong.MyTextOnly = "";
            this.autoBuong.Name = "autoBuong";
            this.autoBuong.RaiseEvent = true;
            this.autoBuong.RaiseEventEnter = true;
            this.autoBuong.RaiseEventEnterWhenEmpty = true;
            this.autoBuong.SelectedIndex = -1;
            this.autoBuong.Size = new System.Drawing.Size(346, 22);
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
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 23);
            this.label4.TabIndex = 29;
            this.label4.Text = "Khoa nội trú :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(675, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 23);
            this.label10.TabIndex = 30;
            this.label10.Text = "Buồng :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdSearchGiuong
            // 
            this.cmdSearchGiuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearchGiuong.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSearchGiuong.Location = new System.Drawing.Point(1111, 9);
            this.cmdSearchGiuong.Name = "cmdSearchGiuong";
            this.cmdSearchGiuong.Size = new System.Drawing.Size(120, 35);
            this.cmdSearchGiuong.TabIndex = 2;
            this.cmdSearchGiuong.Text = "Tìm kiếm";
            this.cmdSearchGiuong.Click += new System.EventHandler(this.cmdSearchGiuong_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.uiGroupBox2);
            this.pnlGrid.Controls.Add(this.uiGroupBox3);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1180, 616);
            this.pnlGrid.TabIndex = 9;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdBed);
            this.uiGroupBox2.Controls.Add(this.grdGiaGiuong);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 48);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1180, 568);
            this.uiGroupBox2.TabIndex = 7;
            this.uiGroupBox2.Text = "Danh sách giường nội trú";
            // 
            // grdBed
            // 
            this.grdBed.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin giường</FilterRowInfoText></LocalizableData>";
            this.grdBed.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdBed_DesignTimeLayout.LayoutString = resources.GetString("grdBed_DesignTimeLayout.LayoutString");
            this.grdBed.DesignTimeLayout = grdBed_DesignTimeLayout;
            this.grdBed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBed.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdBed.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdBed.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdBed.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdBed.Font = new System.Drawing.Font("Arial", 9F);
            this.grdBed.GroupByBoxVisible = false;
            this.grdBed.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdBed.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdBed.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdBed.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdBed.Location = new System.Drawing.Point(3, 18);
            this.grdBed.Name = "grdBed";
            this.grdBed.RecordNavigator = true;
            this.grdBed.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdBed.Size = new System.Drawing.Size(597, 547);
            this.grdBed.TabIndex = 0;
            this.grdBed.UseGroupRowSelector = true;
            this.grdBed.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdGiaGiuong
            // 
            this.grdGiaGiuong.AutomaticSort = false;
            grdGiaGiuong_DesignTimeLayout.LayoutString = resources.GetString("grdGiaGiuong_DesignTimeLayout.LayoutString");
            this.grdGiaGiuong.DesignTimeLayout = grdGiaGiuong_DesignTimeLayout;
            this.grdGiaGiuong.Dock = System.Windows.Forms.DockStyle.Right;
            this.grdGiaGiuong.DynamicFiltering = true;
            this.grdGiaGiuong.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdGiaGiuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdGiaGiuong.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdGiaGiuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdGiaGiuong.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGiaGiuong.FrozenColumns = 2;
            this.grdGiaGiuong.GroupByBoxVisible = false;
            this.grdGiaGiuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdGiaGiuong.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdGiaGiuong.Location = new System.Drawing.Point(600, 18);
            this.grdGiaGiuong.Name = "grdGiaGiuong";
            this.grdGiaGiuong.RecordNavigator = true;
            this.grdGiaGiuong.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdGiaGiuong.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdGiaGiuong.Size = new System.Drawing.Size(577, 547);
            this.grdGiaGiuong.TabIndex = 1637;
            this.grdGiaGiuong.TabStop = false;
            this.grdGiaGiuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlDohoa
            // 
            this.pnlDohoa.Controls.Add(this.pnlKhoa);
            this.pnlDohoa.Controls.Add(this.pnlGiuong);
            this.pnlDohoa.Controls.Add(this.pnlBuong);
            this.pnlDohoa.Controls.Add(this.pnlNavigation);
            this.pnlDohoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDohoa.Location = new System.Drawing.Point(0, 0);
            this.pnlDohoa.Name = "pnlDohoa";
            this.pnlDohoa.Size = new System.Drawing.Size(1180, 616);
            this.pnlDohoa.TabIndex = 9;
            // 
            // pnlKhoa
            // 
            this.pnlKhoa.Controls.Add(this.flowKhoa);
            this.pnlKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKhoa.Location = new System.Drawing.Point(0, 40);
            this.pnlKhoa.Name = "pnlKhoa";
            this.pnlKhoa.Size = new System.Drawing.Size(1180, 576);
            this.pnlKhoa.TabIndex = 0;
            // 
            // flowKhoa
            // 
            this.flowKhoa.AutoScroll = true;
            this.flowKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowKhoa.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowKhoa.Location = new System.Drawing.Point(0, 0);
            this.flowKhoa.Name = "flowKhoa";
            this.flowKhoa.Size = new System.Drawing.Size(1180, 576);
            this.flowKhoa.TabIndex = 29;
            this.flowKhoa.WrapContents = false;
            // 
            // pnlGiuong
            // 
            this.pnlGiuong.Controls.Add(this.flowGiuong);
            this.pnlGiuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGiuong.Location = new System.Drawing.Point(0, 40);
            this.pnlGiuong.Name = "pnlGiuong";
            this.pnlGiuong.Size = new System.Drawing.Size(1180, 576);
            this.pnlGiuong.TabIndex = 3;
            // 
            // flowGiuong
            // 
            this.flowGiuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGiuong.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGiuong.Location = new System.Drawing.Point(0, 0);
            this.flowGiuong.Name = "flowGiuong";
            this.flowGiuong.Size = new System.Drawing.Size(1180, 576);
            this.flowGiuong.TabIndex = 30;
            // 
            // pnlBuong
            // 
            this.pnlBuong.Controls.Add(this.flowBuong);
            this.pnlBuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBuong.Location = new System.Drawing.Point(0, 40);
            this.pnlBuong.Name = "pnlBuong";
            this.pnlBuong.Size = new System.Drawing.Size(1180, 576);
            this.pnlBuong.TabIndex = 2;
            // 
            // flowBuong
            // 
            this.flowBuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBuong.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBuong.Location = new System.Drawing.Point(0, 0);
            this.flowBuong.Name = "flowBuong";
            this.flowBuong.Size = new System.Drawing.Size(1180, 576);
            this.flowBuong.TabIndex = 30;
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.Controls.Add(this.lblGuide);
            this.pnlNavigation.Controls.Add(this.pnlSearch);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(1180, 40);
            this.pnlNavigation.TabIndex = 1;
            // 
            // lblGuide
            // 
            this.lblGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGuide.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblGuide.Location = new System.Drawing.Point(469, 0);
            this.lblGuide.Name = "lblGuide";
            this.lblGuide.Size = new System.Drawing.Size(711, 40);
            this.lblGuide.TabIndex = 54;
            this.lblGuide.Text = "Bạn đang ở: Khoa nội trú\\Buồng 3\\ Giường 2";
            this.lblGuide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.pnlTimkiemKhoa);
            this.pnlSearch.Controls.Add(this.pnlTimkiemGiuong);
            this.pnlSearch.Controls.Add(this.pnlTimkiemBuong);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(469, 40);
            this.pnlSearch.TabIndex = 53;
            // 
            // pnlTimkiemBuong
            // 
            this.pnlTimkiemBuong.Controls.Add(this.cmdBackKhoa);
            this.pnlTimkiemBuong.Controls.Add(this.autoBuong1);
            this.pnlTimkiemBuong.Controls.Add(this.label1);
            this.pnlTimkiemBuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimkiemBuong.Location = new System.Drawing.Point(0, 0);
            this.pnlTimkiemBuong.Name = "pnlTimkiemBuong";
            this.pnlTimkiemBuong.Size = new System.Drawing.Size(469, 40);
            this.pnlTimkiemBuong.TabIndex = 2;
            // 
            // cmdBackKhoa
            // 
            this.cmdBackKhoa.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdBackKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBackKhoa.Image = global::VNS.HIS.UI.Noitru.Properties.Resources.arrow_left_11_1;
            this.cmdBackKhoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBackKhoa.Location = new System.Drawing.Point(415, 0);
            this.cmdBackKhoa.Name = "cmdBackKhoa";
            this.cmdBackKhoa.Size = new System.Drawing.Size(54, 40);
            this.cmdBackKhoa.TabIndex = 53;
            this.cmdBackKhoa.TabStop = false;
            this.toolTip1.SetToolTip(this.cmdBackKhoa, "Quay về danh sách buồng");
            this.cmdBackKhoa.Click += new System.EventHandler(this.cmdBackKhoa_Click);
            // 
            // autoBuong1
            // 
            this.autoBuong1._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBuong1._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong1._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBuong1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoBuong1.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBuong1.AutoCompleteList")));
            this.autoBuong1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBuong1.buildShortcut = false;
            this.autoBuong1.CaseSensitive = false;
            this.autoBuong1.CompareNoID = true;
            this.autoBuong1.DefaultCode = "-1";
            this.autoBuong1.DefaultID = "-1";
            this.autoBuong1.DisplayType = 1;
            this.autoBuong1.Drug_ID = null;
            this.autoBuong1.ExtraWidth = 0;
            this.autoBuong1.FillValueAfterSelect = false;
            this.autoBuong1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong1.Location = new System.Drawing.Point(118, 9);
            this.autoBuong1.MaxHeight = 289;
            this.autoBuong1.MinTypedCharacters = 2;
            this.autoBuong1.MyCode = "-1";
            this.autoBuong1.MyID = "-1";
            this.autoBuong1.MyText = "";
            this.autoBuong1.MyTextOnly = "";
            this.autoBuong1.Name = "autoBuong1";
            this.autoBuong1.RaiseEvent = true;
            this.autoBuong1.RaiseEventEnter = true;
            this.autoBuong1.RaiseEventEnterWhenEmpty = true;
            this.autoBuong1.SelectedIndex = -1;
            this.autoBuong1.Size = new System.Drawing.Size(289, 21);
            this.autoBuong1.splitChar = '@';
            this.autoBuong1.splitCharIDAndCode = '#';
            this.autoBuong1.TabIndex = 27;
            this.autoBuong1.TakeCode = false;
            this.autoBuong1.txtMyCode = null;
            this.autoBuong1.txtMyCode_Edit = null;
            this.autoBuong1.txtMyID = null;
            this.autoBuong1.txtMyID_Edit = null;
            this.autoBuong1.txtMyName = null;
            this.autoBuong1.txtMyName_Edit = null;
            this.autoBuong1.txtNext = null;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 23);
            this.label1.TabIndex = 28;
            this.label1.Text = "Buồng :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTimkiemGiuong
            // 
            this.pnlTimkiemGiuong.Controls.Add(this.cmdBack);
            this.pnlTimkiemGiuong.Controls.Add(this.autoGiuong1);
            this.pnlTimkiemGiuong.Controls.Add(this.label9);
            this.pnlTimkiemGiuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimkiemGiuong.Location = new System.Drawing.Point(0, 0);
            this.pnlTimkiemGiuong.Name = "pnlTimkiemGiuong";
            this.pnlTimkiemGiuong.Size = new System.Drawing.Size(469, 40);
            this.pnlTimkiemGiuong.TabIndex = 2;
            // 
            // cmdBack
            // 
            this.cmdBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdBack.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBack.Image = global::VNS.HIS.UI.Noitru.Properties.Resources.arrow_left_11_1;
            this.cmdBack.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBack.Location = new System.Drawing.Point(415, 0);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(54, 40);
            this.cmdBack.TabIndex = 52;
            this.cmdBack.TabStop = false;
            this.toolTip1.SetToolTip(this.cmdBack, "Quay về danh sách buồng");
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // autoGiuong1
            // 
            this.autoGiuong1._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoGiuong1._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong1._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoGiuong1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoGiuong1.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoGiuong1.AutoCompleteList")));
            this.autoGiuong1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoGiuong1.buildShortcut = false;
            this.autoGiuong1.CaseSensitive = false;
            this.autoGiuong1.CompareNoID = true;
            this.autoGiuong1.DefaultCode = "-1";
            this.autoGiuong1.DefaultID = "-1";
            this.autoGiuong1.DisplayType = 1;
            this.autoGiuong1.Drug_ID = null;
            this.autoGiuong1.ExtraWidth = 0;
            this.autoGiuong1.FillValueAfterSelect = false;
            this.autoGiuong1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong1.Location = new System.Drawing.Point(118, 9);
            this.autoGiuong1.MaxHeight = 289;
            this.autoGiuong1.MinTypedCharacters = 2;
            this.autoGiuong1.MyCode = "-1";
            this.autoGiuong1.MyID = "-1";
            this.autoGiuong1.MyText = "";
            this.autoGiuong1.MyTextOnly = "";
            this.autoGiuong1.Name = "autoGiuong1";
            this.autoGiuong1.RaiseEvent = true;
            this.autoGiuong1.RaiseEventEnter = true;
            this.autoGiuong1.RaiseEventEnterWhenEmpty = true;
            this.autoGiuong1.SelectedIndex = -1;
            this.autoGiuong1.Size = new System.Drawing.Size(291, 22);
            this.autoGiuong1.splitChar = '@';
            this.autoGiuong1.splitCharIDAndCode = '#';
            this.autoGiuong1.TabIndex = 28;
            this.autoGiuong1.TakeCode = false;
            this.autoGiuong1.txtMyCode = null;
            this.autoGiuong1.txtMyCode_Edit = null;
            this.autoGiuong1.txtMyID = null;
            this.autoGiuong1.txtMyID_Edit = null;
            this.autoGiuong1.txtMyName = null;
            this.autoGiuong1.txtMyName_Edit = null;
            this.autoGiuong1.txtNext = null;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(48, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 23);
            this.label9.TabIndex = 29;
            this.label9.Text = "Giường :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTimkiemKhoa
            // 
            this.pnlTimkiemKhoa.Controls.Add(this.autokhoa1);
            this.pnlTimkiemKhoa.Controls.Add(this.label2);
            this.pnlTimkiemKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimkiemKhoa.Location = new System.Drawing.Point(0, 0);
            this.pnlTimkiemKhoa.Name = "pnlTimkiemKhoa";
            this.pnlTimkiemKhoa.Size = new System.Drawing.Size(469, 40);
            this.pnlTimkiemKhoa.TabIndex = 28;
            // 
            // autokhoa1
            // 
            this.autokhoa1._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autokhoa1._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autokhoa1._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autokhoa1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autokhoa1.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autokhoa1.AutoCompleteList")));
            this.autokhoa1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autokhoa1.buildShortcut = false;
            this.autokhoa1.CaseSensitive = false;
            this.autokhoa1.CompareNoID = true;
            this.autokhoa1.DefaultCode = "-1";
            this.autokhoa1.DefaultID = "-1";
            this.autokhoa1.DisplayType = 1;
            this.autokhoa1.Drug_ID = null;
            this.autokhoa1.ExtraWidth = 0;
            this.autokhoa1.FillValueAfterSelect = false;
            this.autokhoa1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autokhoa1.Location = new System.Drawing.Point(118, 10);
            this.autokhoa1.MaxHeight = 289;
            this.autokhoa1.MinTypedCharacters = 2;
            this.autokhoa1.MyCode = "-1";
            this.autokhoa1.MyID = "-1";
            this.autokhoa1.MyText = "";
            this.autokhoa1.MyTextOnly = "";
            this.autokhoa1.Name = "autokhoa1";
            this.autokhoa1.RaiseEvent = true;
            this.autokhoa1.RaiseEventEnter = true;
            this.autokhoa1.RaiseEventEnterWhenEmpty = true;
            this.autokhoa1.SelectedIndex = -1;
            this.autokhoa1.Size = new System.Drawing.Size(308, 21);
            this.autokhoa1.splitChar = '@';
            this.autokhoa1.splitCharIDAndCode = '#';
            this.autokhoa1.TabIndex = 26;
            this.autokhoa1.TakeCode = false;
            this.autokhoa1.txtMyCode = null;
            this.autokhoa1.txtMyCode_Edit = null;
            this.autokhoa1.txtMyID = null;
            this.autokhoa1.txtMyID_Edit = null;
            this.autokhoa1.txtMyName = null;
            this.autokhoa1.txtMyName_Edit = null;
            this.autokhoa1.txtNext = null;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "Khoa:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkGrid);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 616);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1180, 52);
            this.panel2.TabIndex = 31;
            // 
            // chkGrid
            // 
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(0, 0);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(80, 17);
            this.chkGrid.TabIndex = 29;
            this.chkGrid.Tag = "NOITRU_TRACUUBUONGGIUONG_GRID";
            this.chkGrid.Text = "checkBox1";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.Visible = false;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VNS.HIS.UI.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1038, 6);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 39);
            this.cmdExit.TabIndex = 51;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // frm_tracuu_buonggiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 668);
            this.Controls.Add(this.pnlDohoa);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "frm_tracuu_buonggiuong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tra cứu thông tin buồng giường (Nhấn F2 để chuyển qua lại giữa giao diện đồ họa v" +
    "à giao diện lưới)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_tracuu_buonggiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_tracuu_buonggiuong_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaGiuong)).EndInit();
            this.pnlDohoa.ResumeLayout(false);
            this.pnlKhoa.ResumeLayout(false);
            this.pnlGiuong.ResumeLayout(false);
            this.pnlBuong.ResumeLayout(false);
            this.pnlNavigation.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlTimkiemBuong.ResumeLayout(false);
            this.pnlTimkiemBuong.PerformLayout();
            this.pnlTimkiemGiuong.ResumeLayout(false);
            this.pnlTimkiemGiuong.PerformLayout();
            this.pnlTimkiemKhoa.ResumeLayout(false);
            this.pnlTimkiemKhoa.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdBed;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIButton cmdSearchGiuong;
        public Janus.Windows.GridEX.GridEX grdGiaGiuong;
        private UCs.AutoCompleteTextbox autokhoa;
        private UCs.AutoCompleteTextbox autoBuong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlDohoa;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Panel pnlGiuong;
        private System.Windows.Forms.Panel pnlBuong;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Panel pnlTimkiemBuong;
        private System.Windows.Forms.Panel pnlTimkiemGiuong;
        private System.Windows.Forms.Panel pnlKhoa;
        private UCs.AutoCompleteTextbox autoBuong1;
        private System.Windows.Forms.Label label1;
        private UCs.AutoCompleteTextbox autoGiuong1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FlowLayoutPanel flowGiuong;
        private System.Windows.Forms.FlowLayoutPanel flowBuong;
        private Janus.Windows.EditControls.UIButton cmdBack;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel flowKhoa;
        private System.Windows.Forms.Panel pnlTimkiemKhoa;
        private UCs.AutoCompleteTextbox autokhoa1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.Label lblGuide;
        private System.Windows.Forms.Panel pnlSearch;
        private Janus.Windows.EditControls.UIButton cmdBackKhoa;

    }
}