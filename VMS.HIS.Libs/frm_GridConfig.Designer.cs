namespace VNS.Libs
{
    partial class frm_GridConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_GridConfig));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.cmdCopy = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdRestore = new Janus.Windows.EditControls.UIButton();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.chkApplyAll = new Janus.Windows.EditControls.UICheckBox();
            this.grpTimKiem.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.chkApplyAll);
            this.grpTimKiem.Controls.Add(this.cmdCopy);
            this.grpTimKiem.Controls.Add(this.cmdDelete);
            this.grpTimKiem.Controls.Add(this.cmdRestore);
            this.grpTimKiem.Controls.Add(this.cmdAccept);
            this.grpTimKiem.Controls.Add(this.cmdCancel);
            this.grpTimKiem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpTimKiem.Location = new System.Drawing.Point(0, 653);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(1039, 58);
            this.grpTimKiem.TabIndex = 0;
            this.grpTimKiem.TabStop = false;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCopy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
            this.cmdCopy.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCopy.Location = new System.Drawing.Point(248, 14);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(92, 35);
            this.cmdCopy.TabIndex = 559;
            this.cmdCopy.Text = "Sao chép";
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDelete.Location = new System.Drawing.Point(180, 14);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(62, 35);
            this.cmdDelete.TabIndex = 558;
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRestore
            // 
            this.cmdRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRestore.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRestore.Image = ((System.Drawing.Image)(resources.GetObject("cmdRestore.Image")));
            this.cmdRestore.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdRestore.Location = new System.Drawing.Point(6, 14);
            this.cmdRestore.Name = "cmdRestore";
            this.cmdRestore.Size = new System.Drawing.Size(168, 35);
            this.cmdRestore.TabIndex = 557;
            this.cmdRestore.Text = "Khôi phục chế độ gốc";
            this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(789, 14);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(115, 35);
            this.cmdAccept.TabIndex = 555;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(910, 14);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(117, 35);
            this.cmdCancel.TabIndex = 556;
            this.cmdCancel.Text = "Hủy (Esc)";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.SystemColors.Control;
            this.grpThongTin.Controls.Add(this.grdList);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.Location = new System.Drawing.Point(0, 0);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1039, 653);
            this.grpThongTin.TabIndex = 2;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông Tin";
            // 
            // grdList
            // 
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 18);
            this.grdList.Name = "grdList";
            this.grdList.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both;
            this.grdList.Size = new System.Drawing.Size(1033, 632);
            this.grdList.TabIndex = 0;
            // 
            // chkApplyAll
            // 
            this.chkApplyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkApplyAll.Checked = true;
            this.chkApplyAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApplyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApplyAll.ForeColor = System.Drawing.Color.Navy;
            this.chkApplyAll.Location = new System.Drawing.Point(359, 15);
            this.chkApplyAll.Name = "chkApplyAll";
            this.chkApplyAll.Size = new System.Drawing.Size(270, 32);
            this.chkApplyAll.TabIndex = 560;
            this.chkApplyAll.Text = "Áp cấu hình này cho các người dùng khác?";
            this.chkApplyAll.CheckedChanged += new System.EventHandler(this.chkApplyAll_CheckedChanged);
            // 
            // frm_GridConfig
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1039, 711);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.grpTimKiem);
            this.KeyPreview = true;
            this.Name = "frm_GridConfig";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình hiển thị cột trên lưới theo người dùng";
            this.grpTimKiem.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.GroupBox grpThongTin;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdRestore;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.EditControls.UIButton cmdCopy;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        public Janus.Windows.EditControls.UICheckBox chkApplyAll;
    }
}