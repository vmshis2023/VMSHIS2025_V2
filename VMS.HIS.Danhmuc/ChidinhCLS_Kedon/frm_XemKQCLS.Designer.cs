namespace VMS.HIS.Danhmuc.Dungchung
{
    partial class frm_XemKQCLS
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
            Janus.Windows.GridEX.GridEXLayout grdKetqua_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_XemKQCLS));
            Janus.Windows.GridEX.GridEXLayout grdAssignDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiTabKqCls = new Janus.Windows.UI.Tab.UITab();
            this.UITabPageKQ = new Janus.Windows.UI.Tab.UITabPage();
            this.pnlXN = new System.Windows.Forms.Panel();
            this.grdKetqua = new Janus.Windows.GridEX.GridEX();
            this.pnlXQ = new System.Windows.Forms.Panel();
            this.pnlCKEditor = new System.Windows.Forms.Panel();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.lnkMore = new System.Windows.Forms.LinkLabel();
            this.txtDenghi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.richtxtKetluan = new System.Windows.Forms.RichTextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label8 = new System.Windows.Forms.Label();
            this.flowDynamics = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cdViewPDF = new Janus.Windows.EditControls.UIButton();
            this.grdAssignDetail = new Janus.Windows.GridEX.GridEX();
            this.txtNoiDung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiTabKqCls)).BeginInit();
            this.uiTabKqCls.SuspendLayout();
            this.UITabPageKQ.SuspendLayout();
            this.pnlXN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).BeginInit();
            this.pnlXQ.SuspendLayout();
            this.pnlCKEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // uiTabKqCls
            // 
            this.uiTabKqCls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabKqCls.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTabKqCls.Location = new System.Drawing.Point(0, 0);
            this.uiTabKqCls.Name = "uiTabKqCls";
            this.uiTabKqCls.Size = new System.Drawing.Size(840, 921);
            this.uiTabKqCls.TabIndex = 255;
            this.uiTabKqCls.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.UITabPageKQ});
            this.uiTabKqCls.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Normal;
            // 
            // UITabPageKQ
            // 
            this.UITabPageKQ.Controls.Add(this.pnlXN);
            this.UITabPageKQ.Controls.Add(this.pnlXQ);
            this.UITabPageKQ.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UITabPageKQ.Location = new System.Drawing.Point(1, 24);
            this.UITabPageKQ.Name = "UITabPageKQ";
            this.UITabPageKQ.Size = new System.Drawing.Size(836, 894);
            this.UITabPageKQ.TabStop = true;
            this.UITabPageKQ.Text = "Kết quả CLS";
            // 
            // pnlXN
            // 
            this.pnlXN.Controls.Add(this.grdKetqua);
            this.pnlXN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlXN.Location = new System.Drawing.Point(0, 0);
            this.pnlXN.Name = "pnlXN";
            this.pnlXN.Size = new System.Drawing.Size(836, 894);
            this.pnlXN.TabIndex = 0;
            // 
            // grdKetqua
            // 
            this.grdKetqua.AlternatingColors = true;
            this.grdKetqua.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdKetqua_DesignTimeLayout.LayoutString = resources.GetString("grdKetqua_DesignTimeLayout.LayoutString");
            this.grdKetqua.DesignTimeLayout = grdKetqua_DesignTimeLayout;
            this.grdKetqua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetqua.DynamicFiltering = true;
            this.grdKetqua.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKetqua.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKetqua.Font = new System.Drawing.Font("Arial", 9F);
            this.grdKetqua.GroupByBoxVisible = false;
            this.grdKetqua.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdKetqua.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdKetqua.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKetqua.Location = new System.Drawing.Point(0, 0);
            this.grdKetqua.Name = "grdKetqua";
            this.grdKetqua.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdKetqua.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.grdKetqua.Size = new System.Drawing.Size(836, 894);
            this.grdKetqua.TabIndex = 258;
            this.grdKetqua.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdKetqua.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKetqua.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdKetqua.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdKetqua.UseGroupRowSelector = true;
            // 
            // pnlXQ
            // 
            this.pnlXQ.Controls.Add(this.pnlCKEditor);
            this.pnlXQ.Controls.Add(this.flowDynamics);
            this.pnlXQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlXQ.Location = new System.Drawing.Point(0, 0);
            this.pnlXQ.Name = "pnlXQ";
            this.pnlXQ.Size = new System.Drawing.Size(836, 894);
            this.pnlXQ.TabIndex = 506;
            // 
            // pnlCKEditor
            // 
            this.pnlCKEditor.Controls.Add(this.cmdConfig);
            this.pnlCKEditor.Controls.Add(this.lnkMore);
            this.pnlCKEditor.Controls.Add(this.txtDenghi);
            this.pnlCKEditor.Controls.Add(this.label44);
            this.pnlCKEditor.Controls.Add(this.label45);
            this.pnlCKEditor.Controls.Add(this.richtxtKetluan);
            this.pnlCKEditor.Controls.Add(this.webBrowser1);
            this.pnlCKEditor.Controls.Add(this.label8);
            this.pnlCKEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCKEditor.Location = new System.Drawing.Point(0, 0);
            this.pnlCKEditor.Name = "pnlCKEditor";
            this.pnlCKEditor.Size = new System.Drawing.Size(836, 894);
            this.pnlCKEditor.TabIndex = 22;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(790, 867);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(43, 25);
            this.cmdConfig.TabIndex = 505;
            this.cmdConfig.TabStop = false;
            // 
            // lnkMore
            // 
            this.lnkMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkMore.AutoSize = true;
            this.lnkMore.Location = new System.Drawing.Point(73, 867);
            this.lnkMore.Name = "lnkMore";
            this.lnkMore.Size = new System.Drawing.Size(113, 16);
            this.lnkMore.TabIndex = 81;
            this.lnkMore.TabStop = true;
            this.lnkMore.Text = "Xem thêm kết quả";
            // 
            // txtDenghi
            // 
            this.txtDenghi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDenghi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDenghi.Location = new System.Drawing.Point(76, 843);
            this.txtDenghi.Name = "txtDenghi";
            this.txtDenghi.Size = new System.Drawing.Size(744, 21);
            this.txtDenghi.TabIndex = 78;
            this.txtDenghi.Visible = false;
            // 
            // label44
            // 
            this.label44.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(0, 845);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(68, 15);
            this.label44.TabIndex = 80;
            this.label44.Text = "Đề nghị:";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label44.Visible = false;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(2, 770);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(68, 16);
            this.label45.TabIndex = 79;
            this.label45.Text = "Kết luận :";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Visible = false;
            // 
            // richtxtKetluan
            // 
            this.richtxtKetluan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richtxtKetluan.BackColor = System.Drawing.Color.White;
            this.richtxtKetluan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richtxtKetluan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxtKetluan.Location = new System.Drawing.Point(76, 827);
            this.richtxtKetluan.Name = "richtxtKetluan";
            this.richtxtKetluan.Size = new System.Drawing.Size(744, 10);
            this.richtxtKetluan.TabIndex = 77;
            this.richtxtKetluan.Text = "";
            this.richtxtKetluan.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(8, 29);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(818, 841);
            this.webBrowser1.TabIndex = 76;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 23);
            this.label8.TabIndex = 75;
            this.label8.Text = "Mô tả:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowDynamics
            // 
            this.flowDynamics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDynamics.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowDynamics.Location = new System.Drawing.Point(0, 0);
            this.flowDynamics.Name = "flowDynamics";
            this.flowDynamics.Size = new System.Drawing.Size(836, 894);
            this.flowDynamics.TabIndex = 77;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cdViewPDF);
            this.splitContainer1.Panel1.Controls.Add(this.grdAssignDetail);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiTabKqCls);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 921);
            this.splitContainer1.SplitterDistance = 420;
            this.splitContainer1.TabIndex = 256;
            // 
            // cdViewPDF
            // 
            this.cdViewPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cdViewPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cdViewPDF.Image = ((System.Drawing.Image)(resources.GetObject("cdViewPDF.Image")));
            this.cdViewPDF.ImageSize = new System.Drawing.Size(24, 24);
            this.cdViewPDF.Location = new System.Drawing.Point(3, 3);
            this.cdViewPDF.Name = "cdViewPDF";
            this.cdViewPDF.Size = new System.Drawing.Size(30, 30);
            this.cdViewPDF.TabIndex = 560;
            this.cdViewPDF.TabStop = false;
            this.cdViewPDF.Click += new System.EventHandler(this.cdViewPDF_Click);
            // 
            // grdAssignDetail
            // 
            this.grdAssignDetail.AlternatingColors = true;
            this.grdAssignDetail.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdAssignDetail.AlternatingRowFormatStyle.BackColorGradient = System.Drawing.Color.White;
            this.grdAssignDetail.Cursor = System.Windows.Forms.Cursors.Default;
            grdAssignDetail_DesignTimeLayout.LayoutString = resources.GetString("grdAssignDetail_DesignTimeLayout.LayoutString");
            this.grdAssignDetail.DesignTimeLayout = grdAssignDetail_DesignTimeLayout;
            this.grdAssignDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAssignDetail.DynamicFiltering = true;
            this.grdAssignDetail.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdAssignDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdAssignDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdAssignDetail.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grdAssignDetail.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.FocusCellFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdAssignDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdAssignDetail.GroupByBoxVisible = false;
            this.grdAssignDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdAssignDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdAssignDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdAssignDetail.Location = new System.Drawing.Point(0, 0);
            this.grdAssignDetail.Name = "grdAssignDetail";
            this.grdAssignDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.Transparent;
            this.grdAssignDetail.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdAssignDetail.Size = new System.Drawing.Size(420, 921);
            this.grdAssignDetail.TabIndex = 561;
            this.grdAssignDetail.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
            this.grdAssignDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdAssignDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdAssignDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdAssignDetail.UseGroupRowSelector = true;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNoiDung.BackColor = System.Drawing.Color.White;
            this.txtNoiDung.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNoiDung.Location = new System.Drawing.Point(623, 450);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.Size = new System.Drawing.Size(19, 21);
            this.txtNoiDung.TabIndex = 488;
            this.txtNoiDung.TabStop = false;
            this.txtNoiDung.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_XemKQCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_XemKQCLS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem KQ cận lâm sàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_XemKQCLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiTabKqCls)).EndInit();
            this.uiTabKqCls.ResumeLayout(false);
            this.UITabPageKQ.ResumeLayout(false);
            this.pnlXN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).EndInit();
            this.pnlXQ.ResumeLayout(false);
            this.pnlCKEditor.ResumeLayout(false);
            this.pnlCKEditor.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.UI.Tab.UITab uiTabKqCls;
        private Janus.Windows.UI.Tab.UITabPage UITabPageKQ;
        private System.Windows.Forms.Panel pnlXN;
        private Janus.Windows.GridEX.GridEX grdKetqua;
        private System.Windows.Forms.Panel pnlXQ;
        private System.Windows.Forms.Panel pnlCKEditor;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.LinkLabel lnkMore;
        private Janus.Windows.GridEX.EditControls.EditBox txtDenghi;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.RichTextBox richtxtKetluan;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowDynamics;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.GridEX.EditControls.EditBox txtNoiDung;
        private System.Windows.Forms.Timer timer1;
        private Janus.Windows.EditControls.UIButton cdViewPDF;
        private Janus.Windows.GridEX.GridEX grdAssignDetail;
    }
}