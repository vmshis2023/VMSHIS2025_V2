namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_InphieuCLS
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InphieuCLS));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdIntach = new System.Windows.Forms.Button();
            this.cmdInChungPhieu = new System.Windows.Forms.Button();
            this.optA4 = new Janus.Windows.EditControls.UIRadioButton();
            this.optA5 = new Janus.Windows.EditControls.UIRadioButton();
            this.chkPrintPreview = new System.Windows.Forms.CheckBox();
            this.dtNgayInPhieu = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdInXacNhanHIV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(784, 493);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin nhóm chỉ định cần in ";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 18);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(778, 472);
            this.grdList.TabIndex = 0;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdExit
            // 
            this.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(652, 519);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 3;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdIntach
            // 
            this.cmdIntach.BackColor = System.Drawing.SystemColors.Control;
            this.cmdIntach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdIntach.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIntach.Image = ((System.Drawing.Image)(resources.GetObject("cmdIntach.Image")));
            this.cmdIntach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdIntach.Location = new System.Drawing.Point(526, 519);
            this.cmdIntach.Name = "cmdIntach";
            this.cmdIntach.Size = new System.Drawing.Size(120, 35);
            this.cmdIntach.TabIndex = 2;
            this.cmdIntach.Text = "In tách (F5)";
            this.cmdIntach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdIntach.UseVisualStyleBackColor = false;
            this.cmdIntach.Click += new System.EventHandler(this.cmdInPhieuCLS_Click);
            // 
            // cmdInChungPhieu
            // 
            this.cmdInChungPhieu.BackColor = System.Drawing.SystemColors.Control;
            this.cmdInChungPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInChungPhieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInChungPhieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInChungPhieu.Image")));
            this.cmdInChungPhieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInChungPhieu.Location = new System.Drawing.Point(400, 519);
            this.cmdInChungPhieu.Name = "cmdInChungPhieu";
            this.cmdInChungPhieu.Size = new System.Drawing.Size(120, 35);
            this.cmdInChungPhieu.TabIndex = 1;
            this.cmdInChungPhieu.Text = "In chung (F4)";
            this.cmdInChungPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdInChungPhieu.UseVisualStyleBackColor = false;
            this.cmdInChungPhieu.Click += new System.EventHandler(this.cmdInChungPhieu_Click);
            // 
            // optA4
            // 
            this.optA4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optA4.Location = new System.Drawing.Point(227, 499);
            this.optA4.Name = "optA4";
            this.optA4.Size = new System.Drawing.Size(67, 23);
            this.optA4.TabIndex = 452;
            this.optA4.Text = "Khổ A4";
            // 
            // optA5
            // 
            this.optA5.Checked = true;
            this.optA5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optA5.Location = new System.Drawing.Point(144, 499);
            this.optA5.Name = "optA5";
            this.optA5.Size = new System.Drawing.Size(67, 23);
            this.optA5.TabIndex = 451;
            this.optA5.TabStop = true;
            this.optA5.Text = "Khổ A5";
            // 
            // chkPrintPreview
            // 
            this.chkPrintPreview.AutoSize = true;
            this.chkPrintPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintPreview.Location = new System.Drawing.Point(3, 499);
            this.chkPrintPreview.Name = "chkPrintPreview";
            this.chkPrintPreview.Size = new System.Drawing.Size(124, 20);
            this.chkPrintPreview.TabIndex = 509;
            this.chkPrintPreview.Text = "Xem trước khi in";
            this.chkPrintPreview.UseVisualStyleBackColor = true;
            this.chkPrintPreview.CheckedChanged += new System.EventHandler(this.chkInToanBoRamayIn_CheckedChanged);
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayInPhieu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayInPhieu.Location = new System.Drawing.Point(93, 528);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.Size = new System.Drawing.Size(148, 22);
            this.dtNgayInPhieu.TabIndex = 510;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 531);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 511;
            this.label2.Text = "Ngày in phiếu";
            // 
            // cmdInXacNhanHIV
            // 
            this.cmdInXacNhanHIV.BackColor = System.Drawing.Color.Yellow;
            this.cmdInXacNhanHIV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInXacNhanHIV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInXacNhanHIV.Location = new System.Drawing.Point(536, 450);
            this.cmdInXacNhanHIV.Name = "cmdInXacNhanHIV";
            this.cmdInXacNhanHIV.Size = new System.Drawing.Size(125, 34);
            this.cmdInXacNhanHIV.TabIndex = 512;
            this.cmdInXacNhanHIV.Text = "In xác nhận HIV";
            this.cmdInXacNhanHIV.UseVisualStyleBackColor = false;
            this.cmdInXacNhanHIV.Visible = false;
            this.cmdInXacNhanHIV.Click += new System.EventHandler(this.cmdInXacNhanHIV_Click);
            // 
            // frm_InphieuCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.chkPrintPreview);
            this.Controls.Add(this.optA4);
            this.Controls.Add(this.optA5);
            this.Controls.Add(this.cmdInChungPhieu);
            this.Controls.Add(this.cmdIntach);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdInXacNhanHIV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_InphieuCLS";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mời bạn chọn ít nhất một phiếu nếu muốn in Tách.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_InphieuCLS_FormClosing);
            this.Load += new System.EventHandler(this.frm_InphieuCLS_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_InphieuCLS_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdIntach;
        private System.Windows.Forms.Button cmdInChungPhieu;
        private Janus.Windows.EditControls.UIRadioButton optA4;
        private Janus.Windows.EditControls.UIRadioButton optA5;
        private System.Windows.Forms.CheckBox chkPrintPreview;
        private System.Windows.Forms.DateTimePicker dtNgayInPhieu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdInXacNhanHIV;
    }
}