namespace VMS.HIS.Danhmuc
{
    partial class frm_PdfViewer
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel4 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PdfViewer));
            Janus.Windows.GridEX.GridEXLayout grdKQ_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.optMachidinh = new System.Windows.Forms.RadioButton();
            this.optMaluotkham = new System.Windows.Forms.RadioButton();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmachidinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaluotkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdOpen = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.nudPage = new System.Windows.Forms.NumericUpDown();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdKQ = new Janus.Windows.GridEX.GridEX();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.chkForced2Download = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKQ)).BeginInit();
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
            this.uiStatusBar1.Size = new System.Drawing.Size(1524, 27);
            this.uiStatusBar1.TabIndex = 14;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.chkForced2Download);
            this.uiGroupBox1.Controls.Add(this.optMachidinh);
            this.uiGroupBox1.Controls.Add(this.optMaluotkham);
            this.uiGroupBox1.Controls.Add(this.cmdSearch);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtmachidinh);
            this.uiGroupBox1.Controls.Add(this.label13);
            this.uiGroupBox1.Controls.Add(this.txtMaluotkham);
            this.uiGroupBox1.Controls.Add(this.cmdOpen);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Controls.Add(this.cmdPrint);
            this.uiGroupBox1.Controls.Add(this.panel1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1524, 52);
            this.uiGroupBox1.TabIndex = 15;
            // 
            // optMachidinh
            // 
            this.optMachidinh.AutoSize = true;
            this.optMachidinh.Checked = true;
            this.optMachidinh.Location = new System.Drawing.Point(652, 29);
            this.optMachidinh.Name = "optMachidinh";
            this.optMachidinh.Size = new System.Drawing.Size(183, 19);
            this.optMachidinh.TabIndex = 467;
            this.optMachidinh.TabStop = true;
            this.optMachidinh.Text = "Tìm kết quả theo mã chỉ định";
            this.optMachidinh.UseVisualStyleBackColor = true;
            // 
            // optMaluotkham
            // 
            this.optMaluotkham.AutoSize = true;
            this.optMaluotkham.Location = new System.Drawing.Point(652, 7);
            this.optMaluotkham.Name = "optMaluotkham";
            this.optMaluotkham.Size = new System.Drawing.Size(171, 19);
            this.optMaluotkham.TabIndex = 466;
            this.optMaluotkham.Text = "Tìm kết quả theo mã khám";
            this.optMaluotkham.UseVisualStyleBackColor = true;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdSearch.Image = global::VMS.HIS.Danhmuc.Properties.Resources.SEARCH__2_;
            this.cmdSearch.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSearch.Location = new System.Drawing.Point(505, 10);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(114, 36);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Tìm kiếm";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 21);
            this.label1.TabIndex = 465;
            this.label1.Text = "Mã phiếu chỉ định:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtmachidinh
            // 
            this.txtmachidinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtmachidinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmachidinh.Location = new System.Drawing.Point(385, 15);
            this.txtmachidinh.MaxLength = 8;
            this.txtmachidinh.Name = "txtmachidinh";
            this.txtmachidinh.Size = new System.Drawing.Size(114, 22);
            this.txtmachidinh.TabIndex = 1;
            this.txtmachidinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtmachidinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(11, 14);
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
            this.txtMaluotkham.Location = new System.Drawing.Point(122, 15);
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
            this.cmdOpen.Image = global::VMS.HIS.Danhmuc.Properties.Resources.FOLDER3;
            this.cmdOpen.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdOpen.Location = new System.Drawing.Point(1164, 10);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(114, 36);
            this.cmdOpen.TabIndex = 98;
            this.cmdOpen.Text = "Mở file PDF khác";
            this.cmdOpen.ToolTipText = "Mở file PDF khác trong máy tính";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(1404, 10);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(114, 36);
            this.cmdExit.TabIndex = 97;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(1284, 10);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(114, 36);
            this.cmdPrint.TabIndex = 96;
            this.cmdPrint.Text = "In kết quả";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.btnGoTo);
            this.panel1.Controls.Add(this.btnZoom);
            this.panel1.Controls.Add(this.nudPage);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Location = new System.Drawing.Point(14, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 10);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.ForeColor = System.Drawing.Color.White;
            this.btnFirst.Location = new System.Drawing.Point(76, 14);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(88, 30);
            this.btnFirst.TabIndex = 3;
            this.btnFirst.Text = " First Page";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Visible = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.Location = new System.Drawing.Point(15, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(55, 30);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "Check";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Location = new System.Drawing.Point(169, 14);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(88, 30);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Visible = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Arial", 15F);
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(610, 14);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 30);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btnGoTo
            // 
            this.btnGoTo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGoTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoTo.ForeColor = System.Drawing.Color.White;
            this.btnGoTo.Location = new System.Drawing.Point(262, 14);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(52, 30);
            this.btnGoTo.TabIndex = 5;
            this.btnGoTo.Text = "GoTo";
            this.btnGoTo.UseVisualStyleBackColor = false;
            this.btnGoTo.Visible = false;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.BackColor = System.Drawing.SystemColors.Control;
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnZoom.Location = new System.Drawing.Point(557, 14);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(52, 30);
            this.btnZoom.TabIndex = 10;
            this.btnZoom.Text = "Zoom";
            this.btnZoom.UseVisualStyleBackColor = false;
            this.btnZoom.Visible = false;
            // 
            // nudPage
            // 
            this.nudPage.Font = new System.Drawing.Font("Arial", 15F);
            this.nudPage.Location = new System.Drawing.Point(315, 14);
            this.nudPage.Name = "nudPage";
            this.nudPage.Size = new System.Drawing.Size(54, 30);
            this.nudPage.TabIndex = 6;
            this.nudPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPage.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(371, 14);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(88, 30);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.ForeColor = System.Drawing.Color.White;
            this.btnLast.Location = new System.Drawing.Point(464, 14);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(88, 30);
            this.btnLast.TabIndex = 8;
            this.btnLast.Text = "Last Page";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Visible = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdKQ);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 52);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(273, 906);
            this.uiGroupBox2.TabIndex = 17;
            // 
            // grdKQ
            // 
            this.grdKQ.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdKQ.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdKQ_DesignTimeLayout.LayoutString = resources.GetString("grdKQ_DesignTimeLayout.LayoutString");
            this.grdKQ.DesignTimeLayout = grdKQ_DesignTimeLayout;
            this.grdKQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKQ.DynamicFiltering = true;
            this.grdKQ.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKQ.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKQ.GroupByBoxVisible = false;
            this.grdKQ.Location = new System.Drawing.Point(3, 8);
            this.grdKQ.Name = "grdKQ";
            this.grdKQ.RecordNavigator = true;
            this.grdKQ.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both;
            this.grdKQ.Size = new System.Drawing.Size(267, 895);
            this.grdKQ.TabIndex = 1;
            this.grdKQ.SelectionChanged += new System.EventHandler(this.grdThongTin_SelectionChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open PDF File";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(273, 52);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1251, 906);
            this.webBrowser1.TabIndex = 18;
            // 
            // chkForced2Download
            // 
            this.chkForced2Download.AutoSize = true;
            this.chkForced2Download.Location = new System.Drawing.Point(880, 8);
            this.chkForced2Download.Name = "chkForced2Download";
            this.chkForced2Download.Size = new System.Drawing.Size(221, 19);
            this.chkForced2Download.TabIndex = 468;
            this.chkForced2Download.Tag = "CLS_LUONLAYFILEKQ_MOINHAT_TUSERVER";
            this.chkForced2Download.Text = "Luôn lấy file KQ mới nhất từ server?";
            this.chkForced2Download.UseVisualStyleBackColor = true;
            // 
            // frm_PdfViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1524, 985);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.KeyPreview = true;
            this.Name = "frm_PdfViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kết quả cận lâm sàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_PdfViewer_FormClosing);
            this.Load += new System.EventHandler(this.frm_PdfViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_PdfViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKQ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdKQ;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.NumericUpDown nudPage;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdOpen;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        public Janus.Windows.GridEX.EditControls.EditBox txtmachidinh;
        public Janus.Windows.GridEX.EditControls.EditBox txtMaluotkham;
        private System.Windows.Forms.RadioButton optMachidinh;
        private System.Windows.Forms.RadioButton optMaluotkham;
        private System.Windows.Forms.CheckBox chkForced2Download;

    }
}