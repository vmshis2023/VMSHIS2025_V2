namespace VNS.HIS.UI.HinhAnh
{
    partial class frm_DanhMucVungKhaoSat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhMucVungKhaoSat));
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel5 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdMauKQ_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.cmdThemMoi = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSua = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdXoa = new System.Windows.Forms.ToolStripButton();
            this.btnLayDuLieu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdImport = new System.Windows.Forms.ToolStripButton();
            this.cmdExport = new System.Windows.Forms.ToolStripButton();
            this.cmdThoat = new System.Windows.Forms.ToolStripButton();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdMauKQ = new Janus.Windows.GridEX.GridEX();
            this.cmdCapnhatMauKS = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMauKQ)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdThemMoi,
            this.toolStripSeparator1,
            this.cmdSua,
            this.toolStripSeparator2,
            this.cmdXoa,
            this.btnLayDuLieu,
            this.toolStripSeparator3,
            this.cmdImport,
            this.cmdExport,
            this.cmdCapnhatMauKS,
            this.cmdThoat});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // cmdThemMoi
            // 
            this.cmdThemMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemMoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemMoi.Image")));
            this.cmdThemMoi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThemMoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThemMoi.Name = "cmdThemMoi";
            this.cmdThemMoi.Size = new System.Drawing.Size(92, 36);
            this.cmdThemMoi.Text = "Thêm mới";
            this.cmdThemMoi.Click += new System.EventHandler(this.cmdThemMoi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdSua
            // 
            this.cmdSua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(111, 36);
            this.cmdSua.Text = "Sửa thông tin ";
            this.cmdSua.Click += new System.EventHandler(this.cmdSua_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdXoa
            // 
            this.cmdXoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(109, 36);
            this.cmdXoa.Text = "Xóa thông tin ";
            this.cmdXoa.Click += new System.EventHandler(this.cmdXoa_Click);
            // 
            // btnLayDuLieu
            // 
            this.btnLayDuLieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayDuLieu.Image = ((System.Drawing.Image)(resources.GetObject("btnLayDuLieu.Image")));
            this.btnLayDuLieu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLayDuLieu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLayDuLieu.Name = "btnLayDuLieu";
            this.btnLayDuLieu.Size = new System.Drawing.Size(103, 36);
            this.btnLayDuLieu.Text = "Lấy dữ liệu";
            this.btnLayDuLieu.Click += new System.EventHandler(this.btnLayDuLieu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdImport
            // 
            this.cmdImport.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Down1;
            this.cmdImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(66, 36);
            this.cmdImport.Text = "Import";
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Up;
            this.cmdExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(65, 36);
            this.cmdExport.Text = "Export";
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(87, 36);
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 707);
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
            this.uiStatusBar1.Size = new System.Drawing.Size(1008, 23);
            this.uiStatusBar1.TabIndex = 5;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdList.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(603, 668);
            this.grdList.TabIndex = 6;
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdList_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 668);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdMauKQ);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(603, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(405, 668);
            this.panel2.TabIndex = 8;
            // 
            // grdMauKQ
            // 
            this.grdMauKQ.AutomaticSort = false;
            grdMauKQ_DesignTimeLayout.LayoutString = resources.GetString("grdMauKQ_DesignTimeLayout.LayoutString");
            this.grdMauKQ.DesignTimeLayout = grdMauKQ_DesignTimeLayout;
            this.grdMauKQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMauKQ.DynamicFiltering = true;
            this.grdMauKQ.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdMauKQ.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdMauKQ.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdMauKQ.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdMauKQ.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdMauKQ.Font = new System.Drawing.Font("Arial", 9F);
            this.grdMauKQ.GroupByBoxVisible = false;
            this.grdMauKQ.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdMauKQ.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdMauKQ.Location = new System.Drawing.Point(0, 0);
            this.grdMauKQ.Name = "grdMauKQ";
            this.grdMauKQ.RecordNavigator = true;
            this.grdMauKQ.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdMauKQ.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdMauKQ.Size = new System.Drawing.Size(405, 668);
            this.grdMauKQ.TabIndex = 6;
            this.grdMauKQ.TabStop = false;
            this.grdMauKQ.UseGroupRowSelector = true;
            this.grdMauKQ.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdCapnhatMauKS
            // 
            this.cmdCapnhatMauKS.Image = ((System.Drawing.Image)(resources.GetObject("cmdCapnhatMauKS.Image")));
            this.cmdCapnhatMauKS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCapnhatMauKS.Name = "cmdCapnhatMauKS";
            this.cmdCapnhatMauKS.Size = new System.Drawing.Size(159, 36);
            this.cmdCapnhatMauKS.Text = "Cập nhật mẫu khảo sát";
            this.cmdCapnhatMauKS.Click += new System.EventHandler(this.cmdCapnhatMauKS_Click);
            // 
            // frm_DanhMucVungKhaoSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiStatusBar1);
            this.Controls.Add(this.toolStrip);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_DanhMucVungKhaoSat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý vùng khảo sát";
            this.Load += new System.EventHandler(this.frm_DanhMucDichVu_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMauKQ)).EndInit();
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
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ToolStripButton btnLayDuLieu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton cmdImport;
        private System.Windows.Forms.ToolStripButton cmdExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public Janus.Windows.GridEX.GridEX grdMauKQ;
        private System.Windows.Forms.ToolStripButton cmdCapnhatMauKS;
    }
}