namespace VNS.Libs
{
    partial class frm_SysUsers
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
            Janus.Windows.GridEX.GridEXLayout grdSysUser_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SysUsers));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdSysUser = new Janus.Windows.GridEX.GridEX();
            this.cmdUpdateAllUser = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkOverUser = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSysUser)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.grdSysUser);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(633, 549);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin các tài khoản";
            // 
            // grdSysUser
            // 
            this.grdSysUser.AlternatingColors = true;
            this.grdSysUser.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
                " thông tin của Users</FilterRowInfoText></LocalizableData>";
            this.grdSysUser.ColumnAutoResize = true;
            grdSysUser_DesignTimeLayout.LayoutString = resources.GetString("grdSysUser_DesignTimeLayout.LayoutString");
            this.grdSysUser.DesignTimeLayout = grdSysUser_DesignTimeLayout;
            this.grdSysUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSysUser.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdSysUser.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdSysUser.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdSysUser.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdSysUser.GroupByBoxVisible = false;
            this.grdSysUser.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdSysUser.Location = new System.Drawing.Point(3, 16);
            this.grdSysUser.Name = "grdSysUser";
            this.grdSysUser.RecordNavigator = true;
            this.grdSysUser.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdSysUser.Size = new System.Drawing.Size(627, 530);
            this.grdSysUser.TabIndex = 0;
            // 
            // cmdUpdateAllUser
            // 
            this.cmdUpdateAllUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdUpdateAllUser.Location = new System.Drawing.Point(185, 561);
            this.cmdUpdateAllUser.Name = "cmdUpdateAllUser";
            this.cmdUpdateAllUser.Size = new System.Drawing.Size(116, 23);
            this.cmdUpdateAllUser.TabIndex = 16;
            this.cmdUpdateAllUser.Text = "&Update All User";
            this.cmdUpdateAllUser.Click += new System.EventHandler(this.cmdUpdateAllUser_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdExit.Location = new System.Drawing.Point(316, 560);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(116, 23);
            this.cmdExit.TabIndex = 17;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkOverUser
            // 
            this.chkOverUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOverUser.Location = new System.Drawing.Point(15, 560);
            this.chkOverUser.Name = "chkOverUser";
            this.chkOverUser.Size = new System.Drawing.Size(164, 23);
            this.chkOverUser.TabIndex = 1;
            this.chkOverUser.Text = "Update lên những cái có sẵn";
            // 
            // frm_SysUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 590);
            this.Controls.Add(this.chkOverUser);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdUpdateAllUser);
            this.Controls.Add(this.uiGroupBox1);
            this.KeyPreview = true;
            this.Name = "frm_SysUsers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin tài khoản";
            this.Load += new System.EventHandler(this.frm_SysUsers_Load);
            this.ResizeEnd += new System.EventHandler(this.frm_SysUsers_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSysUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdUpdateAllUser;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdSysUser;
        private Janus.Windows.EditControls.UICheckBox chkOverUser;
    }
}