namespace updateVersion_V2
{
    partial class frm_UpdateVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UpdateVersion));
            Janus.Windows.GridEX.GridEXLayout grdIpAddress_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdBrowse = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFiles = new System.Windows.Forms.TextBox();
            this.grdIpAddress = new Janus.Windows.GridEX.GridEX();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdIpAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdUpdate);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdBrowse);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1289, 44);
            this.panel1.TabIndex = 0;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdUpdate.Location = new System.Drawing.Point(1124, 7);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(153, 30);
            this.cmdUpdate.TabIndex = 501;
            this.cmdUpdate.Text = "Cập nhật theo máy";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(780, 7);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(157, 30);
            this.cmdSave.TabIndex = 500;
            this.cmdSave.Text = "Cập nhật phiên bản";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
            this.cmdBrowse.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdBrowse.Location = new System.Drawing.Point(654, 7);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(120, 30);
            this.cmdBrowse.TabIndex = 499;
            this.cmdBrowse.Text = "Chọn tệp tin";
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 26);
            this.label2.TabIndex = 498;
            this.label2.Text = "Tệp tin cần cập nhật";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFiles
            // 
            this.txtFiles.BackColor = System.Drawing.Color.White;
            this.txtFiles.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiles.Location = new System.Drawing.Point(151, 12);
            this.txtFiles.Name = "txtFiles";
            this.txtFiles.ReadOnly = true;
            this.txtFiles.Size = new System.Drawing.Size(497, 22);
            this.txtFiles.TabIndex = 65;
            // 
            // grdIpAddress
            // 
            this.grdIpAddress.AutomaticSort = false;
            grdIpAddress_DesignTimeLayout.LayoutString = resources.GetString("grdIpAddress_DesignTimeLayout.LayoutString");
            this.grdIpAddress.DesignTimeLayout = grdIpAddress_DesignTimeLayout;
            this.grdIpAddress.Dock = System.Windows.Forms.DockStyle.Right;
            this.grdIpAddress.DynamicFiltering = true;
            this.grdIpAddress.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdIpAddress.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdIpAddress.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdIpAddress.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdIpAddress.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdIpAddress.Font = new System.Drawing.Font("Arial", 9F);
            this.grdIpAddress.FrozenColumns = 2;
            this.grdIpAddress.GroupByBoxVisible = false;
            this.grdIpAddress.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdIpAddress.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdIpAddress.Location = new System.Drawing.Point(802, 44);
            this.grdIpAddress.Name = "grdIpAddress";
            this.grdIpAddress.RecordNavigator = true;
            this.grdIpAddress.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdIpAddress.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdIpAddress.Size = new System.Drawing.Size(487, 744);
            this.grdIpAddress.TabIndex = 6;
            this.grdIpAddress.TabStop = false;
            this.grdIpAddress.UseGroupRowSelector = true;
            this.grdIpAddress.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdList
            // 
            this.grdList.AutomaticSort = false;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.FrozenColumns = 2;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdList.Location = new System.Drawing.Point(0, 44);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(802, 744);
            this.grdList.TabIndex = 7;
            this.grdList.TabStop = false;
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_UpdateVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 788);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.grdIpAddress);
            this.Controls.Add(this.panel1);
            this.Name = "frm_UpdateVersion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cập nhật phiên bản";
            this.Load += new System.EventHandler(this.frm_UpdateVersion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdIpAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Janus.Windows.GridEX.GridEX grdIpAddress;
        public Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.TextBox txtFiles;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdBrowse;
    }
}