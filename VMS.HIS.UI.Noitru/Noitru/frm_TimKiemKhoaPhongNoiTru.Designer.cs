namespace VNS.HIS.UI.NOITRU
{
    partial class frm_TimKiemKhoaPhongNoiTru
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
            Janus.Windows.GridEX.GridEXLayout grdKhoaNoiTru_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TimKiemKhoaPhongNoiTru));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdKhoaNoiTru = new Janus.Windows.GridEX.GridEX();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdChapNhan = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimKiem = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoaNoiTru)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdKhoaNoiTru);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(541, 327);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Khoa nội trú";
            // 
            // grdKhoaNoiTru
            // 
            this.grdKhoaNoiTru.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdKhoaNoiTru.AlternatingColors = true;
            this.grdKhoaNoiTru.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdKhoaNoiTru.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin mã bệnh ICD-10</FilterRowInfoText></LocalizableData>";
            this.grdKhoaNoiTru.ColumnAutoResize = true;
            grdKhoaNoiTru_DesignTimeLayout.LayoutString = resources.GetString("grdKhoaNoiTru_DesignTimeLayout.LayoutString");
            this.grdKhoaNoiTru.DesignTimeLayout = grdKhoaNoiTru_DesignTimeLayout;
            this.grdKhoaNoiTru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKhoaNoiTru.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdKhoaNoiTru.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdKhoaNoiTru.GroupByBoxVisible = false;
            this.grdKhoaNoiTru.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKhoaNoiTru.Location = new System.Drawing.Point(3, 16);
            this.grdKhoaNoiTru.Name = "grdKhoaNoiTru";
            this.grdKhoaNoiTru.Size = new System.Drawing.Size(535, 308);
            this.grdKhoaNoiTru.TabIndex = 3;
            this.grdKhoaNoiTru.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdKhoaNoiTru.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdKhoaNoiTru_KeyDown);
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.Location = new System.Drawing.Point(440, 372);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(97, 27);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "&Thoát Form";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdChapNhan
            // 
            this.cmdChapNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChapNhan.Image = ((System.Drawing.Image)(resources.GetObject("cmdChapNhan.Image")));
            this.cmdChapNhan.Location = new System.Drawing.Point(337, 372);
            this.cmdChapNhan.Name = "cmdChapNhan";
            this.cmdChapNhan.Size = new System.Drawing.Size(97, 27);
            this.cmdChapNhan.TabIndex = 4;
            this.cmdChapNhan.Text = "&Chấp nhận";
            this.cmdChapNhan.Click += new System.EventHandler(this.cmdChapNhan_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 39);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(67, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(463, 22);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);
            this.txtTimKiem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimKiem_KeyPress);
            // 
            // frm_TimKiemKhoaPhongNoiTru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 407);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdChapNhan);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TimKiemKhoaPhongNoiTru";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm khoa nội trú";
            this.Load += new System.EventHandler(this.frm_TimKiemKhoaPhongNoiTru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoaNoiTru)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdKhoaNoiTru;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdChapNhan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtTimKiem;
    }
}