namespace VietbaIT.QMS.GoiLoa
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Janus.Windows.GridEX.GridEXLayout grdListGoiLaiSoKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdListGoiLaiSoKham = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdGoiSo = new Janus.Windows.EditControls.UIButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hiệnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtmaloa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtnoidung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdsavemaloa = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 206);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.txtnoidung);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(630, 206);
            this.splitContainer1.SplitterDistance = 507;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdListGoiLaiSoKham);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(507, 177);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Danh sách gọi";
            // 
            // grdListGoiLaiSoKham
            // 
            this.grdListGoiLaiSoKham.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdListGoiLaiSoKham_DesignTimeLayout.LayoutString = resources.GetString("grdListGoiLaiSoKham_DesignTimeLayout.LayoutString");
            this.grdListGoiLaiSoKham.DesignTimeLayout = grdListGoiLaiSoKham_DesignTimeLayout;
            this.grdListGoiLaiSoKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListGoiLaiSoKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdListGoiLaiSoKham.GroupByBoxVisible = false;
            this.grdListGoiLaiSoKham.Location = new System.Drawing.Point(3, 19);
            this.grdListGoiLaiSoKham.Name = "grdListGoiLaiSoKham";
            this.grdListGoiLaiSoKham.RecordNavigator = true;
            this.grdListGoiLaiSoKham.Size = new System.Drawing.Size(501, 155);
            this.grdListGoiLaiSoKham.TabIndex = 6;
            this.grdListGoiLaiSoKham.UseGroupRowSelector = true;
            this.grdListGoiLaiSoKham.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cmdsavemaloa);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.txtmaloa);
            this.uiGroupBox2.Controls.Add(this.cmdGoiSo);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 10F);
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(122, 206);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin loa";
            // 
            // cmdGoiSo
            // 
            this.cmdGoiSo.Location = new System.Drawing.Point(3, 171);
            this.cmdGoiSo.Name = "cmdGoiSo";
            this.cmdGoiSo.Size = new System.Drawing.Size(116, 31);
            this.cmdGoiSo.TabIndex = 0;
            this.cmdGoiSo.Text = "Gọi số";
            this.cmdGoiSo.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdGoiSo.Click += new System.EventHandler(this.cmdGoiSo_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiệnToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 48);
            // 
            // hiệnToolStripMenuItem
            // 
            this.hiệnToolStripMenuItem.Name = "hiệnToolStripMenuItem";
            this.hiệnToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.hiệnToolStripMenuItem.Text = "Hiện";
            this.hiệnToolStripMenuItem.Click += new System.EventHandler(this.hiệnToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // txtmaloa
            // 
            this.txtmaloa.Location = new System.Drawing.Point(3, 132);
            this.txtmaloa.Name = "txtmaloa";
            this.txtmaloa.Size = new System.Drawing.Size(92, 23);
            this.txtmaloa.TabIndex = 1;
            // 
            // txtnoidung
            // 
            this.txtnoidung.Location = new System.Drawing.Point(0, 180);
            this.txtnoidung.Name = "txtnoidung";
            this.txtnoidung.Size = new System.Drawing.Size(507, 23);
            this.txtnoidung.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã loa gọi";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdsavemaloa
            // 
            this.cmdsavemaloa.Image = ((System.Drawing.Image)(resources.GetObject("cmdsavemaloa.Image")));
            this.cmdsavemaloa.Location = new System.Drawing.Point(98, 133);
            this.cmdsavemaloa.Name = "cmdsavemaloa";
            this.cmdsavemaloa.Size = new System.Drawing.Size(22, 21);
            this.cmdsavemaloa.TabIndex = 5;
            this.cmdsavemaloa.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdsavemaloa.Click += new System.EventHandler(this.cmdsavemaloa_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(630, 206);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "Thông tin gọi loa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdListGoiLaiSoKham;
        private Janus.Windows.EditControls.UIButton cmdGoiSo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hiệnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private Janus.Windows.GridEX.EditControls.EditBox txtnoidung;
        private Janus.Windows.GridEX.EditControls.EditBox txtmaloa;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton cmdsavemaloa;
    }
}

