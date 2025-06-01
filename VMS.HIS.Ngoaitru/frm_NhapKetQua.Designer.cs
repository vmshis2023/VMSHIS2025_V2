namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_NhapKetQua
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
            Janus.Windows.GridEX.GridEXLayout grdChidinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NhapKetQua));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtThongbao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtidbenhnhan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtchandoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtphong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtnamsinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtgioitinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtdiachi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txthovaten = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtmathebhyt = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtmaluotkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSoPhieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdChidinh = new Janus.Windows.GridEX.GridEX();
            this.dtpNgaytraketqua = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 515);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(110, 17);
            this.toolStripStatusLabel1.Text = "Ctrl+S(Lưu kết quả)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 515);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(638, 515);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdExit);
            this.groupBox1.Controls.Add(this.cmdDelete);
            this.groupBox1.Controls.Add(this.cmdPrint);
            this.groupBox1.Controls.Add(this.cmdSave);
            this.groupBox1.Controls.Add(this.dtpNgaytraketqua);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtThongbao);
            this.groupBox1.Controls.Add(this.txtidbenhnhan);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtchandoan);
            this.groupBox1.Controls.Add(this.txtphong);
            this.groupBox1.Controls.Add(this.txtnamsinh);
            this.groupBox1.Controls.Add(this.txtgioitinh);
            this.groupBox1.Controls.Add(this.txtdiachi);
            this.groupBox1.Controls.Add(this.txthovaten);
            this.groupBox1.Controls.Add(this.txtmathebhyt);
            this.groupBox1.Controls.Add(this.txtmaluotkham);
            this.groupBox1.Controls.Add(this.txtSoPhieu);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(638, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tìm kiếm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(359, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 15);
            this.label10.TabIndex = 19;
            this.label10.Text = "Ngày trả kết quả:";
            // 
            // txtThongbao
            // 
            this.txtThongbao.BackColor = System.Drawing.Color.White;
            this.txtThongbao.Enabled = false;
            this.txtThongbao.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThongbao.Location = new System.Drawing.Point(84, 140);
            this.txtThongbao.Name = "txtThongbao";
            this.txtThongbao.ReadOnly = true;
            this.txtThongbao.Size = new System.Drawing.Size(274, 23);
            this.txtThongbao.TabIndex = 18;
            this.txtThongbao.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtThongbao.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtidbenhnhan
            // 
            this.txtidbenhnhan.Location = new System.Drawing.Point(532, 12);
            this.txtidbenhnhan.Name = "txtidbenhnhan";
            this.txtidbenhnhan.ReadOnly = true;
            this.txtidbenhnhan.Size = new System.Drawing.Size(63, 21);
            this.txtidbenhnhan.TabIndex = 17;
            this.txtidbenhnhan.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(189, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "Chẩn đoán:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Phòng:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Địa chỉ:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(477, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Năm sinh:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Giới tính:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Họ và tên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Số thẻ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Mã LK:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mã LK:";
            // 
            // txtchandoan
            // 
            this.txtchandoan.Location = new System.Drawing.Point(266, 113);
            this.txtchandoan.Name = "txtchandoan";
            this.txtchandoan.ReadOnly = true;
            this.txtchandoan.Size = new System.Drawing.Size(329, 21);
            this.txtchandoan.TabIndex = 8;
            this.txtchandoan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtphong
            // 
            this.txtphong.Location = new System.Drawing.Point(84, 113);
            this.txtphong.Name = "txtphong";
            this.txtphong.ReadOnly = true;
            this.txtphong.Size = new System.Drawing.Size(105, 21);
            this.txtphong.TabIndex = 7;
            this.txtphong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtnamsinh
            // 
            this.txtnamsinh.Location = new System.Drawing.Point(541, 60);
            this.txtnamsinh.Name = "txtnamsinh";
            this.txtnamsinh.ReadOnly = true;
            this.txtnamsinh.Size = new System.Drawing.Size(54, 21);
            this.txtnamsinh.TabIndex = 6;
            this.txtnamsinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtgioitinh
            // 
            this.txtgioitinh.Location = new System.Drawing.Point(420, 60);
            this.txtgioitinh.Name = "txtgioitinh";
            this.txtgioitinh.ReadOnly = true;
            this.txtgioitinh.Size = new System.Drawing.Size(57, 21);
            this.txtgioitinh.TabIndex = 5;
            this.txtgioitinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtdiachi
            // 
            this.txtdiachi.Location = new System.Drawing.Point(84, 87);
            this.txtdiachi.Name = "txtdiachi";
            this.txtdiachi.ReadOnly = true;
            this.txtdiachi.Size = new System.Drawing.Size(511, 21);
            this.txtdiachi.TabIndex = 4;
            this.txtdiachi.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txthovaten
            // 
            this.txthovaten.Location = new System.Drawing.Point(84, 60);
            this.txthovaten.Name = "txthovaten";
            this.txthovaten.ReadOnly = true;
            this.txthovaten.Size = new System.Drawing.Size(276, 21);
            this.txthovaten.TabIndex = 3;
            this.txthovaten.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtmathebhyt
            // 
            this.txtmathebhyt.Location = new System.Drawing.Point(420, 33);
            this.txtmathebhyt.Name = "txtmathebhyt";
            this.txtmathebhyt.ReadOnly = true;
            this.txtmathebhyt.Size = new System.Drawing.Size(175, 21);
            this.txtmathebhyt.TabIndex = 2;
            this.txtmathebhyt.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtmaluotkham
            // 
            this.txtmaluotkham.Location = new System.Drawing.Point(266, 33);
            this.txtmaluotkham.Name = "txtmaluotkham";
            this.txtmaluotkham.ReadOnly = true;
            this.txtmaluotkham.Size = new System.Drawing.Size(94, 21);
            this.txtmaluotkham.TabIndex = 1;
            this.txtmaluotkham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.BackColor = System.Drawing.Color.NavajoWhite;
            this.txtSoPhieu.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoPhieu.Location = new System.Drawing.Point(84, 33);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(105, 23);
            this.txtSoPhieu.TabIndex = 0;
            this.txtSoPhieu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSoPhieu.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.txtSoPhieu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoPhieu_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdChidinh);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(638, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin chỉ định";
            // 
            // grdChidinh
            // 
            this.grdChidinh.ColumnAutoResize = true;
            grdChidinh_DesignTimeLayout.LayoutString = resources.GetString("grdChidinh_DesignTimeLayout.LayoutString");
            this.grdChidinh.DesignTimeLayout = grdChidinh_DesignTimeLayout;
            this.grdChidinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChidinh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChidinh.GroupByBoxVisible = false;
            this.grdChidinh.Location = new System.Drawing.Point(3, 17);
            this.grdChidinh.Name = "grdChidinh";
            this.grdChidinh.RecordNavigator = true;
            this.grdChidinh.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdChidinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChidinh.Size = new System.Drawing.Size(632, 279);
            this.grdChidinh.TabIndex = 0;
            this.grdChidinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grdChidinh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdChidinh_KeyDown);
            // 
            // dtpNgaytraketqua
            // 
            this.dtpNgaytraketqua.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgaytraketqua.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaytraketqua.DropDownCalendar.Name = "";
            this.dtpNgaytraketqua.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgaytraketqua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaytraketqua.Location = new System.Drawing.Point(456, 141);
            this.dtpNgaytraketqua.Name = "dtpNgaytraketqua";
            this.dtpNgaytraketqua.ShowUpDown = true;
            this.dtpNgaytraketqua.Size = new System.Drawing.Size(140, 21);
            this.dtpNgaytraketqua.TabIndex = 21;
            this.dtpNgaytraketqua.Value = new System.DateTime(2014, 9, 28, 0, 0, 0, 0);
            this.dtpNgaytraketqua.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(111, 173);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(100, 30);
            this.cmdSave.TabIndex = 22;
            this.cmdSave.Text = "Lưu kết quả";
            this.cmdSave.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(323, 173);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(100, 30);
            this.cmdPrint.TabIndex = 23;
            this.cmdPrint.Text = "In kết quả";
            this.cmdPrint.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDelete.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdDelete.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDelete.Location = new System.Drawing.Point(217, 173);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(100, 30);
            this.cmdDelete.TabIndex = 24;
            this.cmdDelete.Text = "Xóa kết quả";
            this.cmdDelete.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdDelete.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(429, 173);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(100, 30);
            this.cmdExit.TabIndex = 25;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frm_NhapKetQua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 537);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_NhapKetQua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập kết quả ";
            this.Load += new System.EventHandler(this.frm_NhapKetQua_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtchandoan;
        private Janus.Windows.GridEX.EditControls.EditBox txtphong;
        private Janus.Windows.GridEX.EditControls.EditBox txtnamsinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtgioitinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtdiachi;
        private Janus.Windows.GridEX.EditControls.EditBox txthovaten;
        private Janus.Windows.GridEX.EditControls.EditBox txtmathebhyt;
        private Janus.Windows.GridEX.EditControls.EditBox txtmaluotkham;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoPhieu;
        private System.Windows.Forms.GroupBox groupBox2;
        private Janus.Windows.GridEX.GridEX grdChidinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtidbenhnhan;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Janus.Windows.GridEX.EditControls.EditBox txtThongbao;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaytraketqua;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;

    }
}