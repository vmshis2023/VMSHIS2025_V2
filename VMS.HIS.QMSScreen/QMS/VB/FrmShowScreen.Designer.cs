namespace VMS.QMS
{
    partial class FrmShowScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowScreen));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.lbldanhsachchokham = new System.Windows.Forms.Label();
            this.lblTenDangKham = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.mnuSaveLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnudeleteLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lbltenbacsy = new VMS.QMS.UIControl.Marquee();
            this.lblphongkham = new VMS.QMS.UIControl.Marquee();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lbldanhsachnho = new VMS.QMS.UIControl.Marquee();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblSoDangKham = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbldanhsachchokham
            // 
            this.lbldanhsachchokham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbldanhsachchokham, "lbldanhsachchokham");
            this.lbldanhsachchokham.ForeColor = System.Drawing.Color.Blue;
            this.lbldanhsachchokham.Name = "lbldanhsachchokham";
            // 
            // lblTenDangKham
            // 
            this.lblTenDangKham.BackColor = System.Drawing.SystemColors.Control;
            this.lblTenDangKham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblTenDangKham, "lblTenDangKham");
            this.lblTenDangKham.ForeColor = System.Drawing.Color.Navy;
            this.lblTenDangKham.Name = "lblTenDangKham";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AutomaticSort = false;
            this.grdList.BackColor = System.Drawing.Color.Lavender;
            this.grdList.BlendColor = System.Drawing.Color.Lavender;
            this.grdList.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(grdList_DesignTimeLayout, "grdList_DesignTimeLayout");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            resources.ApplyResources(this.grdList, "grdList");
            this.grdList.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdList.GridLineColor = System.Drawing.Color.DarkBlue;
            this.grdList.GridLines = Janus.Windows.GridEX.GridLines.Default;
            this.grdList.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Name = "grdList";
            this.grdList.RowFormatStyle.BackColor = System.Drawing.Color.Lavender;
            this.grdList.RowFormatStyle.BackColorGradient = System.Drawing.Color.Lavender;
            this.grdList.RowHeaderFormatStyle.BackColor = System.Drawing.Color.Lavender;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdList.RowWithErrorsFormatStyle.BackColor = System.Drawing.Color.Lavender;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveLayout,
            this.toolStripMenuItem2,
            this.mnudeleteLayout});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // mnuSaveLayout
            // 
            this.mnuSaveLayout.Name = "mnuSaveLayout";
            resources.ApplyResources(this.mnuSaveLayout, "mnuSaveLayout");
            this.mnuSaveLayout.Click += new System.EventHandler(this.mnuSaveLayout_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // mnudeleteLayout
            // 
            this.mnudeleteLayout.Name = "mnudeleteLayout";
            resources.ApplyResources(this.mnudeleteLayout, "mnudeleteLayout");
            this.mnudeleteLayout.Click += new System.EventHandler(this.mnudeleteLayout_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.OnTimerEvent);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lbltenbacsy);
            this.pnlTop.Controls.Add(this.lblphongkham);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // lbltenbacsy
            // 
            this.lbltenbacsy.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lbltenbacsy, "lbltenbacsy");
            this.lbltenbacsy.Name = "lbltenbacsy";
            this.lbltenbacsy.Speed = 3;
            this.lbltenbacsy.yOffset = 5;
            // 
            // lblphongkham
            // 
            resources.ApplyResources(this.lblphongkham, "lblphongkham");
            this.lblphongkham.Name = "lblphongkham";
            this.lblphongkham.Speed = 3;
            this.lblphongkham.yOffset = 11;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lbldanhsachnho);
            this.pnlBottom.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // lbldanhsachnho
            // 
            resources.ApplyResources(this.lbldanhsachnho, "lbldanhsachnho");
            this.lbldanhsachnho.Name = "lbldanhsachnho";
            this.lbldanhsachnho.Speed = 3;
            this.lbldanhsachnho.yOffset = 8;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lbldanhsachchokham);
            this.pnlLeft.Controls.Add(this.pnlLogo);
            this.pnlLeft.Controls.Add(this.lblSoDangKham);
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // pnlLogo
            // 
            resources.ApplyResources(this.pnlLogo, "pnlLogo");
            this.pnlLogo.Name = "pnlLogo";
            // 
            // lblSoDangKham
            // 
            resources.ApplyResources(this.lblSoDangKham, "lblSoDangKham");
            this.lblSoDangKham.Name = "lblSoDangKham";
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.grdList);
            this.pnlCenter.Controls.Add(this.lblTenDangKham);
            resources.ApplyResources(this.pnlCenter, "pnlCenter");
            this.pnlCenter.Name = "pnlCenter";
            // 
            // FrmShowScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FrmShowScreen";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmShowScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbldanhsachchokham;
        private System.Windows.Forms.Label lblTenDangKham;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Timer timer1;
        private UIControl.Marquee lbldanhsachnho;
        private UIControl.Marquee lblphongkham;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlCenter;
        private UIControl.Marquee lbltenbacsy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSoDangKham;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnudeleteLayout;
    }
}