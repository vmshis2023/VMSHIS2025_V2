namespace VMS.QMS.GoiLoa
{
    partial class FrmMain_New
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain_New));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdListGoiLaiSoKham = new Janus.Windows.GridEX.GridEX();
            this.txtnoidung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdsavemaloa = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtmaloa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdGoiSo = new Janus.Windows.EditControls.UIButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hiệnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDotre = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtThoigiancho = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdStop = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.grdListGoiLaiSoKham);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 92);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1003, 547);
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
            this.grdListGoiLaiSoKham.Size = new System.Drawing.Size(997, 525);
            this.grdListGoiLaiSoKham.TabIndex = 6;
            this.grdListGoiLaiSoKham.UseGroupRowSelector = true;
            this.grdListGoiLaiSoKham.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtnoidung
            // 
            this.txtnoidung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtnoidung.Location = new System.Drawing.Point(100, 659);
            this.txtnoidung.MaxLength = 500;
            this.txtnoidung.Name = "txtnoidung";
            this.txtnoidung.Size = new System.Drawing.Size(774, 23);
            this.txtnoidung.TabIndex = 2;
            // 
            // cmdsavemaloa
            // 
            this.cmdsavemaloa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdsavemaloa.Image = ((System.Drawing.Image)(resources.GetObject("cmdsavemaloa.Image")));
            this.cmdsavemaloa.Location = new System.Drawing.Point(831, 696);
            this.cmdsavemaloa.Name = "cmdsavemaloa";
            this.cmdsavemaloa.Size = new System.Drawing.Size(22, 21);
            this.cmdsavemaloa.TabIndex = 5;
            this.cmdsavemaloa.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdsavemaloa.Click += new System.EventHandler(this.cmdsavemaloa_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(0, 690);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã loa gọi";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtmaloa
            // 
            this.txtmaloa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtmaloa.Location = new System.Drawing.Point(100, 688);
            this.txtmaloa.Name = "txtmaloa";
            this.txtmaloa.Size = new System.Drawing.Size(92, 23);
            this.txtmaloa.TabIndex = 1;
            // 
            // cmdGoiSo
            // 
            this.cmdGoiSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGoiSo.Location = new System.Drawing.Point(880, 653);
            this.cmdGoiSo.Name = "cmdGoiSo";
            this.cmdGoiSo.Size = new System.Drawing.Size(116, 31);
            this.cmdGoiSo.TabIndex = 0;
            this.cmdGoiSo.Text = "Test";
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 48);
            // 
            // hiệnToolStripMenuItem
            // 
            this.hiệnToolStripMenuItem.Name = "hiệnToolStripMenuItem";
            this.hiệnToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.hiệnToolStripMenuItem.Text = "Hiện";
            this.hiệnToolStripMenuItem.Click += new System.EventHandler(this.hiệnToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // txtDotre
            // 
            this.txtDotre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDotre.Location = new System.Drawing.Point(300, 690);
            this.txtDotre.Name = "txtDotre";
            this.txtDotre.Size = new System.Drawing.Size(92, 23);
            this.txtDotre.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(200, 692);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Độ trễ đọc";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtThoigiancho
            // 
            this.txtThoigiancho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtThoigiancho.Location = new System.Drawing.Point(606, 692);
            this.txtThoigiancho.Name = "txtThoigiancho";
            this.txtThoigiancho.Size = new System.Drawing.Size(112, 23);
            this.txtThoigiancho.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(398, 694);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Thời gian nghỉ giữa 2 lần gọi";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(0, 662);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nội dung";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStop.Image = global::VMS.QMS.TTS.Properties.Resources.QMS_Play_06_48;
            this.cmdStop.ImageSize = new System.Drawing.Size(28, 28);
            this.cmdStop.Location = new System.Drawing.Point(880, 690);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(116, 31);
            this.cmdStop.TabIndex = 11;
            this.cmdStop.Tag = "0";
            this.cmdStop.Text = "Start";
            this.cmdStop.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 86);
            this.panel1.TabIndex = 12;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(398, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(602, 77);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "HỆ THỐNG GỌI LOA QMS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(722, 694);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "(ms)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain_New
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtThoigiancho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDotre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdsavemaloa);
            this.Controls.Add(this.txtmaloa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtnoidung);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdGoiSo);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.KeyPreview = true;
            this.Name = "FrmMain_New";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý gọi loa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_New_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_New_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_New_Load);
            this.Resize += new System.EventHandler(this.FrmMain_New_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
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
        private Janus.Windows.GridEX.EditControls.EditBox txtDotre;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtThoigiancho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIButton cmdStop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label6;
    }
}

