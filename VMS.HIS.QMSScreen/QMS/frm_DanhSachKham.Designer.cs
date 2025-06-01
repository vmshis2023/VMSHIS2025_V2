namespace VMS.QMS
{
    partial class frm_DanhSachKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhSachKham));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdCauHinh = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.lblBacSykham = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 655);
            this.uiStatusBar1.Name = "uiStatusBar1";
            this.uiStatusBar1.Size = new System.Drawing.Size(1003, 23);
            this.uiStatusBar1.TabIndex = 0;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdCauHinh
            // 
            this.cmdCauHinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCauHinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCauHinh.Image")));
            this.cmdCauHinh.Location = new System.Drawing.Point(974, 655);
            this.cmdCauHinh.Name = "cmdCauHinh";
            this.cmdCauHinh.Size = new System.Drawing.Size(29, 23);
            this.cmdCauHinh.TabIndex = 1;
            this.cmdCauHinh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdCauHinh.Click += new System.EventHandler(this.cmdCauHinh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 655);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblBacSykham);
            this.splitContainer1.Size = new System.Drawing.Size(1003, 655);
            this.splitContainer1.SplitterDistance = 574;
            this.splitContainer1.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1003, 574);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "Danh sách bệnh nhân chờ khám bệnh";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GridLineColor = System.Drawing.Color.Navy;
            this.grdList.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 19);
            this.grdList.Margin = new System.Windows.Forms.Padding(6);
            this.grdList.Name = "grdList";
            this.grdList.RowFormatStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.ScrollBarWidth = 17;
            this.grdList.Size = new System.Drawing.Size(997, 552);
            this.grdList.TabIndex = 0;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // lblBacSykham
            // 
            this.lblBacSykham.BackColor = System.Drawing.SystemColors.Control;
            this.lblBacSykham.Font = new System.Drawing.Font("Arial", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBacSykham.ForeColor = System.Drawing.Color.Blue;
            this.lblBacSykham.Location = new System.Drawing.Point(123, 8);
            this.lblBacSykham.Name = "lblBacSykham";
            this.lblBacSykham.Size = new System.Drawing.Size(774, 62);
            this.lblBacSykham.TabIndex = 0;
            this.lblBacSykham.Text = "Bác sỹ: Nguyễn Thị Ngọc Bích";
            this.lblBacSykham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frm_DanhSachKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 678);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdCauHinh);
            this.Controls.Add(this.uiStatusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DanhSachKham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh nhân chờ khám";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_DanhSachKham_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.Timer timer1;
        private Janus.Windows.EditControls.UIButton cmdCauHinh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label lblBacSykham;
        private System.Windows.Forms.Timer timer2;
    }
}