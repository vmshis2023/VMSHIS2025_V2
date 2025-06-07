using DevExpress.XtraPdfViewer;
namespace VNS.HIS.UI.BA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Emr));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiButton2 = new Janus.Windows.EditControls.UIButton();
            this.uiButton1 = new Janus.Windows.EditControls.UIButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.optEdit = new System.Windows.Forms.RadioButton();
            this.optReadOnly = new System.Windows.Forms.RadioButton();
            this.cmdOpenDoc = new Janus.Windows.EditControls.UIButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optPdfView = new System.Windows.Forms.RadioButton();
            this.optDocView = new System.Windows.Forms.RadioButton();
            this.chkForced2Download = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaluotkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdOpen = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3,
            uiStatusBarPanel4});
            this.uiStatusBar1.Size = new System.Drawing.Size(1391, 27);
            this.uiStatusBar1.TabIndex = 14;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.uiGroupBox1.Controls.Add(this.uiButton2);
            this.uiGroupBox1.Controls.Add(this.chkForced2Download);
            this.uiGroupBox1.Controls.Add(this.label13);
            this.uiGroupBox1.Controls.Add(this.txtMaluotkham);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1391, 124);
            this.uiGroupBox1.TabIndex = 15;
            // 
            // uiButton2
            // 
            this.uiButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiButton2.Image = ((System.Drawing.Image)(resources.GetObject("uiButton2.Image")));
            this.uiButton2.ImageSize = new System.Drawing.Size(20, 20);
            this.uiButton2.Location = new System.Drawing.Point(1250, 40);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(114, 36);
            this.uiButton2.TabIndex = 476;
            this.uiButton2.Text = "SaveDoc";
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiButton1.Image = ((System.Drawing.Image)(resources.GetObject("uiButton1.Image")));
            this.uiButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.uiButton1.Location = new System.Drawing.Point(3, 261);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(86, 36);
            this.uiButton1.TabIndex = 475;
            this.uiButton1.Text = "Add MergeField";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "id_benhnhan",
            "ma_luotkham",
            "ten_benhnhan"});
            this.comboBox1.Location = new System.Drawing.Point(3, 303);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(82, 23);
            this.comboBox1.TabIndex = 474;
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
            this.cmdOpenDoc.Location = new System.Drawing.Point(3, 177);
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
            // chkForced2Download
            // 
            this.chkForced2Download.AutoSize = true;
            this.chkForced2Download.Location = new System.Drawing.Point(1158, 99);
            this.chkForced2Download.Name = "chkForced2Download";
            this.chkForced2Download.Size = new System.Drawing.Size(221, 19);
            this.chkForced2Download.TabIndex = 468;
            this.chkForced2Download.Tag = "CLS_LUONLAYFILEKQ_MOINHAT_TUSERVER";
            this.chkForced2Download.Text = "Luôn lấy file KQ mới nhất từ server?";
            this.chkForced2Download.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1060, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 21);
            this.label13.TabIndex = 463;
            this.label13.Text = "Mã lần khám :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaluotkham
            // 
            this.txtMaluotkham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMaluotkham.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaluotkham.Location = new System.Drawing.Point(1171, 12);
            this.txtMaluotkham.MaxLength = 8;
            this.txtMaluotkham.Name = "txtMaluotkham";
            this.txtMaluotkham.Size = new System.Drawing.Size(114, 22);
            this.txtMaluotkham.TabIndex = 0;
            this.txtMaluotkham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaluotkham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // cmdOpen
            // 
            this.cmdOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOpen.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdOpen.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdOpen.Location = new System.Drawing.Point(3, 135);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(85, 36);
            this.cmdOpen.TabIndex = 98;
            this.cmdOpen.Text = "Open Pdf";
            this.cmdOpen.ToolTipText = "Mở file PDF khác trong máy tính";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(3, 219);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(86, 36);
            this.cmdPrint.TabIndex = 96;
            this.cmdPrint.Text = "In kết quả";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(443, 834);
            this.uiGroupBox2.TabIndex = 17;
            // 
            // grdList
            // 
            this.grdList.AutomaticSort = false;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.FrozenColumns = 2;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdList.Location = new System.Drawing.Point(3, 8);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(437, 823);
            this.grdList.TabIndex = 6;
            this.grdList.TabStop = false;
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open PDF File";
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(944, 834);
            this.pdfViewer1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 124);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.pdfViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1391, 834);
            this.splitContainer1.SplitterDistance = 443;
            this.splitContainer1.TabIndex = 556;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(3, 8);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(875, 113);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 477;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.uiButton1);
            this.panel1.Controls.Add(this.cmdOpen);
            this.panel1.Controls.Add(this.cmdOpenDoc);
            this.panel1.Controls.Add(this.cmdPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(853, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(91, 834);
            this.panel1.TabIndex = 1;
            // 
            // frm_Emr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 985);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiGroupBox1);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdOpen;
        private System.Windows.Forms.Label label13;
        public Janus.Windows.GridEX.EditControls.EditBox txtMaluotkham;
        private System.Windows.Forms.CheckBox chkForced2Download;
        private PdfViewer pdfViewer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton cmdOpenDoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optPdfView;
        private System.Windows.Forms.RadioButton optDocView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton optEdit;
        private System.Windows.Forms.RadioButton optReadOnly;
        public Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton uiButton2;
        private Janus.Windows.EditControls.UIButton uiButton1;
        private System.Windows.Forms.ComboBox comboBox1;
        private Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic ucThongtinnguoibenh_emr_basic1;
        private System.Windows.Forms.Panel panel1;
    }
}