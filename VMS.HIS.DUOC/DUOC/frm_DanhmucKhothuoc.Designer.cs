namespace VNS.HIS.UI.THUOC
{
    partial class frm_DanhmucKhothuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhmucKhothuoc));
            Janus.Windows.GridEX.GridEXLayout grdKhoThuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDutru_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel5 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.cmdThemMoi = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSua = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdXoa = new System.Windows.Forms.ToolStripButton();
            this.cmdThoat = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdKhoThuoc = new Janus.Windows.GridEX.GridEX();
            this.grdDutru = new Janus.Windows.GridEX.GridEX();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDutru)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdThemMoi,
            this.toolStripSeparator1,
            this.cmdSua,
            this.toolStripSeparator2,
            this.cmdXoa,
            this.cmdThoat});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1484, 31);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // cmdThemMoi
            // 
            this.cmdThemMoi.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdThemMoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemMoi.Image")));
            this.cmdThemMoi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThemMoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThemMoi.Name = "cmdThemMoi";
            this.cmdThemMoi.Size = new System.Drawing.Size(91, 28);
            this.cmdThemMoi.Text = "Thêm kho";
            this.cmdThemMoi.Click += new System.EventHandler(this.cmdThemMoi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // cmdSua
            // 
            this.cmdSua.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(80, 28);
            this.cmdSua.Text = "Sửa kho";
            this.cmdSua.Click += new System.EventHandler(this.cmdSua_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // cmdXoa
            // 
            this.cmdXoa.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(79, 28);
            this.cmdXoa.Text = "Xóa kho";
            this.cmdXoa.Click += new System.EventHandler(this.cmdXoa_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(90, 28);
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.splitContainer1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 31);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1484, 707);
            this.uiGroupBox1.TabIndex = 7;
            this.uiGroupBox1.Text = "Danh mục kho thuốc";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdKhoThuoc);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdDutru);
            this.splitContainer1.Size = new System.Drawing.Size(1478, 686);
            this.splitContainer1.SplitterDistance = 907;
            this.splitContainer1.TabIndex = 6;
            // 
            // grdKhoThuoc
            // 
            this.grdKhoThuoc.AlternatingColors = true;
            this.grdKhoThuoc.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.grdKhoThuoc.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin kho thuốc và vật tư</FilterRowInfoText></LocalizableData>";
            this.grdKhoThuoc.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdKhoThuoc_DesignTimeLayout.LayoutString = resources.GetString("grdKhoThuoc_DesignTimeLayout.LayoutString");
            this.grdKhoThuoc.DesignTimeLayout = grdKhoThuoc_DesignTimeLayout;
            this.grdKhoThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKhoThuoc.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKhoThuoc.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdKhoThuoc.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKhoThuoc.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKhoThuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.grdKhoThuoc.GroupByBoxVisible = false;
            this.grdKhoThuoc.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdKhoThuoc.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdKhoThuoc.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdKhoThuoc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKhoThuoc.Location = new System.Drawing.Point(0, 0);
            this.grdKhoThuoc.Name = "grdKhoThuoc";
            this.grdKhoThuoc.RecordNavigator = true;
            this.grdKhoThuoc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKhoThuoc.Size = new System.Drawing.Size(907, 686);
            this.grdKhoThuoc.TabIndex = 0;
            this.grdKhoThuoc.DoubleClick += new System.EventHandler(this.grdKhoThuoc_DoubleClick);
            // 
            // grdDutru
            // 
            this.grdDutru.AlternatingColors = true;
            this.grdDutru.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdDutru_DesignTimeLayout.LayoutString = resources.GetString("grdDutru_DesignTimeLayout.LayoutString");
            this.grdDutru.DesignTimeLayout = grdDutru_DesignTimeLayout;
            this.grdDutru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDutru.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDutru.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdDutru.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDutru.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDutru.Font = new System.Drawing.Font("Arial", 9F);
            this.grdDutru.GroupByBoxVisible = false;
            this.grdDutru.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDutru.Location = new System.Drawing.Point(0, 0);
            this.grdDutru.Name = "grdDutru";
            this.grdDutru.RecordNavigator = true;
            this.grdDutru.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdDutru.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDutru.RowWithErrorsFormatStyle.ForeColor = System.Drawing.SystemColors.Control;
            this.grdDutru.Size = new System.Drawing.Size(567, 686);
            this.grdDutru.TabIndex = 5;
            this.grdDutru.TabStop = false;
            this.grdDutru.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 738);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Ctrl+N:Thêm mới";
            uiStatusBarPanel1.Width = 108;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Ctrl+E:Sửa thông tin ";
            uiStatusBarPanel2.Width = 126;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Ctrl+D:Xóa thông tin chọn";
            uiStatusBarPanel3.Width = 154;
            uiStatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel4.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel4.Key = "";
            uiStatusBarPanel4.ProgressBarValue = 0;
            uiStatusBarPanel4.Text = "F9: Lấy lại dữ liệu";
            uiStatusBarPanel4.Width = 110;
            uiStatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel5.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel5.Key = "";
            uiStatusBarPanel5.ProgressBarValue = 0;
            uiStatusBarPanel5.Text = "Esc:Thoát";
            uiStatusBarPanel5.Width = 69;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3,
            uiStatusBarPanel4,
            uiStatusBarPanel5});
            this.uiStatusBar1.Size = new System.Drawing.Size(1484, 23);
            this.uiStatusBar1.TabIndex = 6;
            // 
            // frm_DanhmucKhothuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 761);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiStatusBar1);
            this.Controls.Add(this.toolStrip);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DanhmucKhothuoc";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý danh mục kho thuốc";
            this.Load += new System.EventHandler(this.frm_DanhmucKhothuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_DanhmucKhothuoc_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDutru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton cmdThemMoi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdSua;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdXoa;
        private System.Windows.Forms.ToolStripButton cmdThoat;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdKhoThuoc;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.GridEX.GridEX grdDutru;
    }
}