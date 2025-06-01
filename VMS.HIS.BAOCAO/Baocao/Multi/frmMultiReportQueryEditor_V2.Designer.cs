namespace VNS.MultiReport
{
    partial class frmMultiReportQueryEditor_V2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiReportQueryEditor_V2));
            Janus.Windows.GridEX.GridEXLayout grdSheetConfig_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdConfig_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTab2 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage2 = new Janus.Windows.UI.Tab.UITabPage();
            this.txtQuery = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiTabPage4 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdSheetConfig = new Janus.Windows.GridEX.GridEX();
            this.csmConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tslDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.uiTabPage3 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdConfig = new Janus.Windows.GridEX.GridEX();
            this.txtReportType = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbIsAdmin = new Janus.Windows.EditControls.UICheckBox();
            this.btnAddNew = new Janus.Windows.EditControls.UIButton();
            this.nmrReportID = new Janus.Windows.GridEX.EditControls.NumericEditBox();
            this.txtReport_Name = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSequence = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cboQueryType = new Janus.Windows.EditControls.UIComboBox();
            this.btnSave = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab2)).BeginInit();
            this.uiTab2.SuspendLayout();
            this.uiTabPage2.SuspendLayout();
            this.uiTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSheetConfig)).BeginInit();
            this.csmConfig.SuspendLayout();
            this.uiTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConfig)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 537);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.DrawBorder = false;
            uiStatusBarPanel1.Image = ((System.Drawing.Image)(resources.GetObject("uiStatusBarPanel1.Image")));
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "ESC: Thoát";
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1});
            this.uiStatusBar1.Size = new System.Drawing.Size(1077, 23);
            this.uiStatusBar1.TabIndex = 16;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Location = new System.Drawing.Point(0, 0);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(1077, 537);
            this.uiTab1.TabIndex = 17;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.panel1);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 27);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(1075, 509);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Chức Năng";
            // 
            // uiTab2
            // 
            this.uiTab2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiTab2.BackColor = System.Drawing.Color.Transparent;
            this.uiTab2.Location = new System.Drawing.Point(17, 138);
            this.uiTab2.Name = "uiTab2";
            this.uiTab2.Size = new System.Drawing.Size(1048, 320);
            this.uiTab2.TabIndex = 34;
            this.uiTab2.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage2,
            this.uiTabPage4,
            this.uiTabPage3});
            this.uiTab2.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // uiTabPage2
            // 
            this.uiTabPage2.Controls.Add(this.txtQuery);
            this.uiTabPage2.Location = new System.Drawing.Point(1, 27);
            this.uiTabPage2.Name = "uiTabPage2";
            this.uiTabPage2.Size = new System.Drawing.Size(1046, 292);
            this.uiTabPage2.TabStop = true;
            this.uiTabPage2.Text = "Query";
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(1046, 292);
            this.txtQuery.TabIndex = 5;
            // 
            // uiTabPage4
            // 
            this.uiTabPage4.Controls.Add(this.grdSheetConfig);
            this.uiTabPage4.Key = "tabSheet";
            this.uiTabPage4.Location = new System.Drawing.Point(1, 27);
            this.uiTabPage4.Name = "uiTabPage4";
            this.uiTabPage4.Size = new System.Drawing.Size(1046, 292);
            this.uiTabPage4.TabStop = true;
            this.uiTabPage4.Text = "Cấu hình Excel Sheet";
            // 
            // grdSheetConfig
            // 
            this.grdSheetConfig.ContextMenuStrip = this.csmConfig;
            grdSheetConfig_DesignTimeLayout.LayoutString = resources.GetString("grdSheetConfig_DesignTimeLayout.LayoutString");
            this.grdSheetConfig.DesignTimeLayout = grdSheetConfig_DesignTimeLayout;
            this.grdSheetConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSheetConfig.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.grdSheetConfig.GroupByBoxVisible = false;
            this.grdSheetConfig.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdSheetConfig.Location = new System.Drawing.Point(0, 0);
            this.grdSheetConfig.Name = "grdSheetConfig";
            this.grdSheetConfig.NewRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdSheetConfig.RecordNavigator = true;
            this.grdSheetConfig.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdSheetConfig.SelectedInactiveFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
            this.grdSheetConfig.Size = new System.Drawing.Size(1046, 292);
            this.grdSheetConfig.TabIndex = 1;
            this.grdSheetConfig.CellEdited += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridEX1_CellEdited);
            // 
            // csmConfig
            // 
            this.csmConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddNew,
            this.tslDelete});
            this.csmConfig.Name = "csmConfig";
            this.csmConfig.Size = new System.Drawing.Size(148, 48);
            // 
            // tsmAddNew
            // 
            this.tsmAddNew.Image = ((System.Drawing.Image)(resources.GetObject("tsmAddNew.Image")));
            this.tsmAddNew.Name = "tsmAddNew";
            this.tsmAddNew.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.tsmAddNew.Size = new System.Drawing.Size(147, 22);
            this.tsmAddNew.Text = "Thêm mới";
            this.tsmAddNew.Click += new System.EventHandler(this.tsmAddNew_Click);
            // 
            // tslDelete
            // 
            this.tslDelete.Image = ((System.Drawing.Image)(resources.GetObject("tslDelete.Image")));
            this.tslDelete.Name = "tslDelete";
            this.tslDelete.Size = new System.Drawing.Size(147, 22);
            this.tslDelete.Text = "Xóa";
            this.tslDelete.Click += new System.EventHandler(this.tslDelete_Click);
            // 
            // uiTabPage3
            // 
            this.uiTabPage3.Controls.Add(this.grdConfig);
            this.uiTabPage3.Key = "tabColumn";
            this.uiTabPage3.Location = new System.Drawing.Point(1, 27);
            this.uiTabPage3.Name = "uiTabPage3";
            this.uiTabPage3.Size = new System.Drawing.Size(1046, 292);
            this.uiTabPage3.TabStop = true;
            this.uiTabPage3.Text = "Cấu hình cột Sheet";
            // 
            // grdConfig
            // 
            this.grdConfig.ColumnAutoResize = true;
            this.grdConfig.ContextMenuStrip = this.csmConfig;
            grdConfig_DesignTimeLayout.LayoutString = resources.GetString("grdConfig_DesignTimeLayout.LayoutString");
            this.grdConfig.DesignTimeLayout = grdConfig_DesignTimeLayout;
            this.grdConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConfig.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.grdConfig.GroupByBoxVisible = false;
            this.grdConfig.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdConfig.Location = new System.Drawing.Point(0, 0);
            this.grdConfig.Name = "grdConfig";
            this.grdConfig.NewRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdConfig.RecordNavigator = true;
            this.grdConfig.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdConfig.SelectedInactiveFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
            this.grdConfig.Size = new System.Drawing.Size(1046, 292);
            this.grdConfig.TabIndex = 0;
            this.grdConfig.CellEdited += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdConfig_CellEdited);
            // 
            // txtReportType
            // 
            this.txtReportType.Location = new System.Drawing.Point(154, 39);
            this.txtReportType.Name = "txtReportType";
            this.txtReportType.Size = new System.Drawing.Size(350, 26);
            this.txtReportType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 33;
            this.label2.Text = "Phân Loại";
            // 
            // ckbIsAdmin
            // 
            this.ckbIsAdmin.BackColor = System.Drawing.Color.Transparent;
            this.ckbIsAdmin.Location = new System.Drawing.Point(260, 106);
            this.ckbIsAdmin.Name = "ckbIsAdmin";
            this.ckbIsAdmin.Size = new System.Drawing.Size(104, 23);
            this.ckbIsAdmin.TabIndex = 32;
            this.ckbIsAdmin.Text = "Admin";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageSize = new System.Drawing.Size(24, 24);
            this.btnAddNew.Location = new System.Drawing.Point(18, 466);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(208, 33);
            this.btnAddNew.TabIndex = 6;
            this.btnAddNew.Text = "Tạo mới (Ctrl+N)";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // nmrReportID
            // 
            this.nmrReportID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.nmrReportID.DecimalDigits = 0;
            this.nmrReportID.Location = new System.Drawing.Point(154, 7);
            this.nmrReportID.Name = "nmrReportID";
            this.nmrReportID.Size = new System.Drawing.Size(100, 26);
            this.nmrReportID.TabIndex = 0;
            this.nmrReportID.Text = "0";
            this.nmrReportID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.nmrReportID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nmrReportID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nmrReportID_KeyDown);
            // 
            // txtReport_Name
            // 
            this.txtReport_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReport_Name.Location = new System.Drawing.Point(260, 7);
            this.txtReport_Name.Name = "txtReport_Name";
            this.txtReport_Name.Size = new System.Drawing.Size(805, 26);
            this.txtReport_Name.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "STT";
            // 
            // txtSequence
            // 
            this.txtSequence.Location = new System.Drawing.Point(154, 104);
            this.txtSequence.Maximum = 1000;
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.Size = new System.Drawing.Size(100, 26);
            this.txtSequence.TabIndex = 4;
            this.txtSequence.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 21;
            this.label3.Text = "Loại Query";
            // 
            // cboQueryType
            // 
            this.cboQueryType.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "GETDATATABLE";
            uiComboBoxItem1.Value = "GETDATATABLE";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "EXECUTE";
            uiComboBoxItem2.Value = "EXECUTE";
            this.cboQueryType.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboQueryType.Location = new System.Drawing.Point(154, 71);
            this.cboQueryType.Margin = new System.Windows.Forms.Padding(4);
            this.cboQueryType.Name = "cboQueryType";
            this.cboQueryType.Size = new System.Drawing.Size(350, 26);
            this.cboQueryType.TabIndex = 3;
            this.cboQueryType.Text = "uiComboBox1";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSave.Location = new System.Drawing.Point(232, 466);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(208, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu (Ctrl+S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Tên Chức Năng (F3)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.uiTab2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtReportType);
            this.panel1.Controls.Add(this.cboQueryType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ckbIsAdmin);
            this.panel1.Controls.Add(this.txtSequence);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nmrReportID);
            this.panel1.Controls.Add(this.txtReport_Name);
            this.panel1.Location = new System.Drawing.Point(68, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 219);
            this.panel1.TabIndex = 35;
            // 
            // frmMultiReportQueryEditor_V2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 560);
            this.Controls.Add(this.uiTab1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMultiReportQueryEditor_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUERY EDITOR";
            this.Load += new System.EventHandler(this.frmMultiReportQueryEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMultiReportQueryEditor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab2)).EndInit();
            this.uiTab2.ResumeLayout(false);
            this.uiTabPage2.ResumeLayout(false);
            this.uiTabPage2.PerformLayout();
            this.uiTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSheetConfig)).EndInit();
            this.csmConfig.ResumeLayout(false);
            this.uiTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConfig)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.GridEX.EditControls.EditBox txtQuery;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txtSequence;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboQueryType;
        private Janus.Windows.EditControls.UIButton btnSave;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.NumericEditBox nmrReportID;
        private Janus.Windows.GridEX.EditControls.EditBox txtReport_Name;
        private Janus.Windows.EditControls.UIButton btnAddNew;
        private Janus.Windows.EditControls.UICheckBox ckbIsAdmin;
        private Janus.Windows.GridEX.EditControls.EditBox txtReportType;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.UI.Tab.UITab uiTab2;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage2;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage3;
        private Janus.Windows.GridEX.GridEX grdConfig;
        private System.Windows.Forms.ContextMenuStrip csmConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNew;
        private System.Windows.Forms.ToolStripMenuItem tslDelete;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage4;
        private Janus.Windows.GridEX.GridEX grdSheetConfig;
        private System.Windows.Forms.Panel panel1;

    }
}