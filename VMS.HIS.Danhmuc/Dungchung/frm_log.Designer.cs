namespace VNS.RISLink.Bussiness.UI
{
    partial class frm_log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_log));
            Janus.Windows.GridEX.GridEXLayout grdThongTin_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboAct = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.linkF5 = new System.Windows.Forms.LinkLabel();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNguoiTao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label14 = new System.Windows.Forms.Label();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.grdThongTin = new Janus.Windows.GridEX.GridEX();
            this.grpTimKiem.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThongTin)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.txtIp);
            this.grpTimKiem.Controls.Add(this.label4);
            this.grpTimKiem.Controls.Add(this.cboAct);
            this.grpTimKiem.Controls.Add(this.label3);
            this.grpTimKiem.Controls.Add(this.linkF5);
            this.grpTimKiem.Controls.Add(this.txtNoiDung);
            this.grpTimKiem.Controls.Add(this.label2);
            this.grpTimKiem.Controls.Add(this.txtNguoiTao);
            this.grpTimKiem.Controls.Add(this.label1);
            this.grpTimKiem.Controls.Add(this.cmdSearch);
            this.grpTimKiem.Controls.Add(this.label14);
            this.grpTimKiem.Controls.Add(this.chkByDate);
            this.grpTimKiem.Controls.Add(this.dtToDate);
            this.grpTimKiem.Controls.Add(this.dtFromDate);
            this.grpTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTimKiem.Location = new System.Drawing.Point(0, 0);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(1114, 110);
            this.grpTimKiem.TabIndex = 0;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Nhập thông tin tìm kiếm";
            // 
            // txtIp
            // 
            this.txtIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIp.Location = new System.Drawing.Point(502, 76);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(301, 20);
            this.txtIp.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(427, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 87;
            this.label4.Text = "IP";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAct
            // 
            this.cboAct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAct.FormattingEnabled = true;
            this.cboAct.Items.AddRange(new object[] {
            "---Tất cả---",
            "Thêm mới",
            "Cập nhật",
            "Xóa",
            "Ghi",
            "Xác nhận",
            "Chọn",
            "Tìm kiếm",
            "Capture",
            "In",
            "Browse",
            "FTP",
            "Hiển thị",
            "Xuất",
            "Nhập",
            "Found",
            "Not Found",
            "Làm mới",
            "Reset",
            "Lỗi",
            "Empty",
            "Khôi phục",
            "Cut"});
            this.cboAct.Location = new System.Drawing.Point(102, 75);
            this.cboAct.Name = "cboAct";
            this.cboAct.Size = new System.Drawing.Size(319, 21);
            this.cboAct.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 85;
            this.label3.Text = "Hành động:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkF5
            // 
            this.linkF5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkF5.AutoSize = true;
            this.linkF5.Location = new System.Drawing.Point(809, 33);
            this.linkF5.Name = "linkF5";
            this.linkF5.Size = new System.Drawing.Size(53, 13);
            this.linkF5.TabIndex = 84;
            this.linkF5.TabStop = true;
            this.linkF5.Text = "Làm sạch";
            this.linkF5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkF5_LinkClicked);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiDung.Location = new System.Drawing.Point(102, 54);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(701, 20);
            this.txtNoiDung.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 82;
            this.label2.Text = "Nội Dung";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNguoiTao
            // 
            this.txtNguoiTao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiTao.Location = new System.Drawing.Point(502, 30);
            this.txtNguoiTao.Name = "txtNguoiTao";
            this.txtNguoiTao.Size = new System.Drawing.Size(301, 20);
            this.txtNguoiTao.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(427, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 80;
            this.label1.Text = "Người tạo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSearch.Location = new System.Drawing.Point(965, 27);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(137, 69);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            this.cmdSearch.ToolTipText = "Nhấn (Dùng phím F3 ) thực hiện tìm thông tin bệnh nhân";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(239, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 15);
            this.label14.TabIndex = 78;
            this.label14.Text = "Đến";
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkByDate.Image = ((System.Drawing.Image)(resources.GetObject("chkByDate.Image")));
            this.chkByDate.Location = new System.Drawing.Point(12, 27);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(84, 23);
            this.chkByDate.TabIndex = 77;
            this.chkByDate.Text = "Từ ngày";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(294, 27);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(127, 21);
            this.dtToDate.TabIndex = 1;
            this.dtToDate.Value = new System.DateTime(2013, 8, 10, 0, 0, 0, 0);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Location = new System.Drawing.Point(102, 27);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(127, 21);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.Value = new System.DateTime(2013, 8, 10, 0, 0, 0, 0);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 595);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1114, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.SystemColors.Control;
            this.grpThongTin.Controls.Add(this.grdThongTin);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.Location = new System.Drawing.Point(0, 110);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1114, 485);
            this.grpThongTin.TabIndex = 2;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông Tin";
            // 
            // grdThongTin
            // 
            grdThongTin_DesignTimeLayout.LayoutString = resources.GetString("grdThongTin_DesignTimeLayout.LayoutString");
            this.grdThongTin.DesignTimeLayout = grdThongTin_DesignTimeLayout;
            this.grdThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThongTin.DynamicFiltering = true;
            this.grdThongTin.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThongTin.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongTin.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThongTin.GroupByBoxVisible = false;
            this.grdThongTin.Location = new System.Drawing.Point(3, 18);
            this.grdThongTin.Name = "grdThongTin";
            this.grdThongTin.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both;
            this.grdThongTin.Size = new System.Drawing.Size(1108, 464);
            this.grdThongTin.TabIndex = 0;
            // 
            // frm_log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 617);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpTimKiem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_log";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Theo dõi thao tác người dùng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_log_KeyDown);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThongTin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNguoiTao;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox grpThongTin;
        private Janus.Windows.GridEX.GridEX grdThongTin;
        private System.Windows.Forms.LinkLabel linkF5;
        private System.Windows.Forms.ComboBox cboAct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label4;
    }
}