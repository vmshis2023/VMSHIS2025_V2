namespace VMS.HIS.BHYT
{
    partial class FrmThongTinBHYT
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
            Janus.Windows.GridEX.GridEXLayout grdLichSuKCB_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongTinBHYT));
            Janus.Windows.GridEX.GridEXLayout grdkiemtrathe_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblcanhbao = new System.Windows.Forms.Label();
            this.lblMessega = new System.Windows.Forms.Label();
            this.cmdLichSuKiemTra = new Janus.Windows.EditControls.UIButton();
            this.cmdChapNhan = new Janus.Windows.EditControls.UIButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdLichSuKCB = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdkiemtrathe = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSuKCB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdkiemtrathe)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 701);
            this.uiStatusBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiStatusBar1.Name = "uiStatusBar1";
            this.uiStatusBar1.Size = new System.Drawing.Size(1008, 28);
            this.uiStatusBar1.TabIndex = 1;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblcanhbao);
            this.splitContainer1.Panel1.Controls.Add(this.lblMessega);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 657);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // lblcanhbao
            // 
            this.lblcanhbao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcanhbao.Font = new System.Drawing.Font("Arial", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblcanhbao.ForeColor = System.Drawing.Color.Red;
            this.lblcanhbao.Location = new System.Drawing.Point(0, 95);
            this.lblcanhbao.Name = "lblcanhbao";
            this.lblcanhbao.Size = new System.Drawing.Size(1008, 71);
            this.lblcanhbao.TabIndex = 38;
            this.lblcanhbao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessega
            // 
            this.lblMessega.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessega.Font = new System.Drawing.Font("Arial", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblMessega.ForeColor = System.Drawing.Color.Red;
            this.lblMessega.Location = new System.Drawing.Point(0, 0);
            this.lblMessega.Name = "lblMessega";
            this.lblMessega.Size = new System.Drawing.Size(1008, 95);
            this.lblMessega.TabIndex = 37;
            this.lblMessega.Text = "lblMessega";
            this.lblMessega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdLichSuKiemTra
            // 
            this.cmdLichSuKiemTra.Location = new System.Drawing.Point(760, 5);
            this.cmdLichSuKiemTra.Name = "cmdLichSuKiemTra";
            this.cmdLichSuKiemTra.Size = new System.Drawing.Size(118, 35);
            this.cmdLichSuKiemTra.TabIndex = 5;
            this.cmdLichSuKiemTra.Text = "In lịch sử KT";
            this.cmdLichSuKiemTra.Click += new System.EventHandler(this.cmdLichSuKiemTra_Click);
            // 
            // cmdChapNhan
            // 
            this.cmdChapNhan.Image = ((System.Drawing.Image)(resources.GetObject("cmdChapNhan.Image")));
            this.cmdChapNhan.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChapNhan.Location = new System.Drawing.Point(636, 5);
            this.cmdChapNhan.Name = "cmdChapNhan";
            this.cmdChapNhan.Size = new System.Drawing.Size(118, 35);
            this.cmdChapNhan.TabIndex = 2;
            this.cmdChapNhan.Text = "Chấp nhận";
            this.cmdChapNhan.Click += new System.EventHandler(this.cmdChapNhan_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1008, 486);
            this.splitContainer2.SplitterDistance = 226;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdLichSuKCB);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 226);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Lịch sử khám chữa bệnh";
            // 
            // grdLichSuKCB
            // 
            grdLichSuKCB_DesignTimeLayout.LayoutString = resources.GetString("grdLichSuKCB_DesignTimeLayout.LayoutString");
            this.grdLichSuKCB.DesignTimeLayout = grdLichSuKCB_DesignTimeLayout;
            this.grdLichSuKCB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLichSuKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLichSuKCB.GroupByBoxVisible = false;
            this.grdLichSuKCB.Location = new System.Drawing.Point(3, 19);
            this.grdLichSuKCB.Name = "grdLichSuKCB";
            this.grdLichSuKCB.RecordNavigator = true;
            this.grdLichSuKCB.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdLichSuKCB.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdLichSuKCB.Size = new System.Drawing.Size(1002, 204);
            this.grdLichSuKCB.TabIndex = 1;
            this.grdLichSuKCB.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdkiemtrathe);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 255);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin kiểm tra thẻ";
            // 
            // grdkiemtrathe
            // 
            grdkiemtrathe_DesignTimeLayout.LayoutString = resources.GetString("grdkiemtrathe_DesignTimeLayout.LayoutString");
            this.grdkiemtrathe.DesignTimeLayout = grdkiemtrathe_DesignTimeLayout;
            this.grdkiemtrathe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdkiemtrathe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdkiemtrathe.GroupByBoxVisible = false;
            this.grdkiemtrathe.Location = new System.Drawing.Point(3, 19);
            this.grdkiemtrathe.Name = "grdkiemtrathe";
            this.grdkiemtrathe.RecordNavigator = true;
            this.grdkiemtrathe.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdkiemtrathe.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdkiemtrathe.Size = new System.Drawing.Size(1002, 233);
            this.grdkiemtrathe.TabIndex = 2;
            this.grdkiemtrathe.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdLichSuKiemTra);
            this.panel1.Controls.Add(this.cmdChapNhan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 657);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 44);
            this.panel1.TabIndex = 3;
            // 
            // cmdExit
            // 
            this.cmdExit.Image = global::VMS.HIS.BHYT.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(884, 5);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(118, 35);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // FrmThongTinBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThongTinBHYT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin check thẻ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThongTinBHYT_FormClosed);
            this.Load += new System.EventHandler(this.FrmThongTinCheckThe_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSuKCB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdkiemtrathe)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdLichSuKCB;
        private Janus.Windows.GridEX.GridEX grdkiemtrathe;
        private System.Windows.Forms.Label lblMessega;
        private Janus.Windows.EditControls.UIButton cmdChapNhan;
        private System.Windows.Forms.Label lblcanhbao;
        private Janus.Windows.EditControls.UIButton cmdLichSuKiemTra;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}