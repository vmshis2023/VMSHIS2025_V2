namespace VNS.HIS.UI.Forms.DUOC
{
    partial class frm_IE_Excel
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_IE_Excel));
            Janus.Windows.GridEX.GridEXLayout grdObjectTypeList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.optOffice2003 = new System.Windows.Forms.RadioButton();
            this.optOffice2007 = new System.Windows.Forms.RadioButton();
            this.chkAutomap = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkQuanhe = new System.Windows.Forms.CheckBox();
            this.grdObjectTypeList = new Janus.Windows.GridEX.GridEX();
            this.cmdExport = new Janus.Windows.EditControls.UIButton();
            this.cmdImport = new Janus.Windows.EditControls.UIButton();
            this.cmdLoadExcel = new Janus.Windows.EditControls.UIButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.txtHelp = new System.Windows.Forms.RichTextBox();
            this.lblNofile = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectTypeList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(8, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 311);
            this.panel1.TabIndex = 429;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.Size = new System.Drawing.Size(762, 311);
            this.grdList.TabIndex = 16;
            this.toolTip1.SetToolTip(this.grdList, "Lưới danh sách các thuốc nhập từ file Excel");
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // optOffice2003
            // 
            this.optOffice2003.AutoSize = true;
            this.optOffice2003.Checked = true;
            this.optOffice2003.Font = new System.Drawing.Font("Arial", 9F);
            this.optOffice2003.Location = new System.Drawing.Point(192, 16);
            this.optOffice2003.Name = "optOffice2003";
            this.optOffice2003.Size = new System.Drawing.Size(145, 19);
            this.optOffice2003.TabIndex = 431;
            this.optOffice2003.TabStop = true;
            this.optOffice2003.Text = "Excel 1997,2003 (.xls)";
            this.toolTip1.SetToolTip(this.optOffice2003, "Nếu file excel danh mục thuốc tạo từ bộ Office 2003,2007 thì chọn mục này?");
            this.optOffice2003.UseVisualStyleBackColor = true;
            // 
            // optOffice2007
            // 
            this.optOffice2007.AutoSize = true;
            this.optOffice2007.Font = new System.Drawing.Font("Arial", 9F);
            this.optOffice2007.Location = new System.Drawing.Point(353, 16);
            this.optOffice2007.Name = "optOffice2007";
            this.optOffice2007.Size = new System.Drawing.Size(150, 19);
            this.optOffice2007.TabIndex = 432;
            this.optOffice2007.Text = "Excel 2007,2012 (.xslx)";
            this.toolTip1.SetToolTip(this.optOffice2007, "Nếu file excel danh mục thuốc tạo từ bộ Office >=2007 thì chọn mục này?");
            this.optOffice2007.UseVisualStyleBackColor = true;
            // 
            // chkAutomap
            // 
            this.chkAutomap.AutoSize = true;
            this.chkAutomap.Location = new System.Drawing.Point(192, 45);
            this.chkAutomap.Name = "chkAutomap";
            this.chkAutomap.Size = new System.Drawing.Size(109, 19);
            this.chkAutomap.TabIndex = 437;
            this.chkAutomap.Text = "Auto mapping?";
            this.toolTip1.SetToolTip(this.chkAutomap, "Tự động dò và map mã đơn vị tính trong tình huống file danh mục excel chỉ có tên " +
        "đơn vị tính");
            this.chkAutomap.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 562);
            this.tabControl1.TabIndex = 433;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.chkAutomap);
            this.tabPage1.Controls.Add(this.lblCount);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.chkQuanhe);
            this.tabPage1.Controls.Add(this.grdObjectTypeList);
            this.tabPage1.Controls.Add(this.cmdExport);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.optOffice2007);
            this.tabPage1.Controls.Add(this.cmdImport);
            this.tabPage1.Controls.Add(this.optOffice2003);
            this.tabPage1.Controls.Add(this.cmdLoadExcel);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 534);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nhập - xuất file excel";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(679, 393);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(24, 15);
            this.lblCount.TabIndex = 436;
            this.lblCount.Text = "0/0";
            this.lblCount.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(290, 392);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(383, 15);
            this.progressBar1.TabIndex = 435;
            this.progressBar1.Visible = false;
            // 
            // chkQuanhe
            // 
            this.chkQuanhe.AutoSize = true;
            this.chkQuanhe.Location = new System.Drawing.Point(8, 392);
            this.chkQuanhe.Name = "chkQuanhe";
            this.chkQuanhe.Size = new System.Drawing.Size(223, 19);
            this.chkQuanhe.TabIndex = 434;
            this.chkQuanhe.Text = "Tạo giá quan hệ cho các đối tượng?";
            this.chkQuanhe.UseVisualStyleBackColor = true;
            // 
            // grdObjectTypeList
            // 
            this.grdObjectTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdObjectTypeList.ColumnAutoResize = true;
            grdObjectTypeList_DesignTimeLayout.LayoutString = resources.GetString("grdObjectTypeList_DesignTimeLayout.LayoutString");
            this.grdObjectTypeList.DesignTimeLayout = grdObjectTypeList_DesignTimeLayout;
            this.grdObjectTypeList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdObjectTypeList.GroupByBoxVisible = false;
            this.grdObjectTypeList.Location = new System.Drawing.Point(8, 417);
            this.grdObjectTypeList.Name = "grdObjectTypeList";
            this.grdObjectTypeList.Size = new System.Drawing.Size(762, 109);
            this.grdObjectTypeList.TabIndex = 433;
            this.grdObjectTypeList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdExport
            // 
            this.cmdExport.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExport.Image = ((System.Drawing.Image)(resources.GetObject("cmdExport.Image")));
            this.cmdExport.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Far;
            this.cmdExport.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExport.Location = new System.Drawing.Point(5, 6);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(124, 31);
            this.cmdExport.TabIndex = 425;
            this.cmdExport.TabStop = false;
            this.cmdExport.Text = "Xuất Excel";
            // 
            // cmdImport
            // 
            this.cmdImport.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdImport.Image = ((System.Drawing.Image)(resources.GetObject("cmdImport.Image")));
            this.cmdImport.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Far;
            this.cmdImport.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdImport.Location = new System.Drawing.Point(645, 6);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(124, 31);
            this.cmdImport.TabIndex = 427;
            this.cmdImport.TabStop = false;
            this.cmdImport.Text = "Cập nhật";
            // 
            // cmdLoadExcel
            // 
            this.cmdLoadExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdLoadExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadExcel.Image")));
            this.cmdLoadExcel.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Far;
            this.cmdLoadExcel.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdLoadExcel.Location = new System.Drawing.Point(515, 6);
            this.cmdLoadExcel.Name = "cmdLoadExcel";
            this.cmdLoadExcel.Size = new System.Drawing.Size(124, 31);
            this.cmdLoadExcel.TabIndex = 430;
            this.cmdLoadExcel.TabStop = false;
            this.cmdLoadExcel.Text = "Chọn file Excel";
            this.cmdLoadExcel.Click += new System.EventHandler(this.cmdLoadExcel_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.pnlHelp);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 534);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hướng dẫn sử dụng";
            // 
            // pnlHelp
            // 
            this.pnlHelp.Controls.Add(this.txtHelp);
            this.pnlHelp.Controls.Add(this.lblNofile);
            this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHelp.Location = new System.Drawing.Point(3, 3);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(770, 528);
            this.pnlHelp.TabIndex = 1;
            // 
            // txtHelp
            // 
            this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelp.Location = new System.Drawing.Point(0, 0);
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.Size = new System.Drawing.Size(770, 528);
            this.txtHelp.TabIndex = 0;
            this.txtHelp.Text = "";
            // 
            // lblNofile
            // 
            this.lblNofile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNofile.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNofile.Location = new System.Drawing.Point(0, 0);
            this.lblNofile.Name = "lblNofile";
            this.lblNofile.Size = new System.Drawing.Size(770, 528);
            this.lblNofile.TabIndex = 0;
            this.lblNofile.Text = "File not found";
            this.lblNofile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_IE_Excel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_IE_Excel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập-xuất với Excel";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectTypeList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExport;
        private Janus.Windows.EditControls.UIButton cmdImport;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIButton cmdLoadExcel;
        private System.Windows.Forms.RadioButton optOffice2003;
        private System.Windows.Forms.RadioButton optOffice2007;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox txtHelp;
        private System.Windows.Forms.CheckBox chkQuanhe;
        private Janus.Windows.GridEX.GridEX grdObjectTypeList;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label lblNofile;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkAutomap;
    }
}