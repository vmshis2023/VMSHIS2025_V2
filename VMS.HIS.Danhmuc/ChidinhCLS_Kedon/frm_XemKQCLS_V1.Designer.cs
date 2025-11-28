namespace VMS.HIS.Danhmuc.Dungchung
{
    partial class frm_XemKQCLS_V1
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
            this.components = new System.ComponentModel.Container();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_XemKQCLS_V1));
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.AlternatingRowFormatStyle.BackColorGradient = System.Drawing.Color.White;
            this.grdList.Cursor = System.Windows.Forms.Cursors.Default;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.FocusCellFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.Transparent;
            this.grdList.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdList.Size = new System.Drawing.Size(1108, 689);
            this.grdList.TabIndex = 561;
            this.grdList.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdList.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.UseGroupRowSelector = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 689);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1108, 40);
            this.panel1.TabIndex = 78;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(977, 3);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(128, 32);
            this.cmdExit.TabIndex = 19;
            this.cmdExit.Text = "Hủy (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(843, 3);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(128, 32);
            this.cmdSave.TabIndex = 18;
            this.cmdSave.Text = "Chọn";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frm_XemKQCLS_V1
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(1108, 729);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.Name = "frm_XemKQCLS_V1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem KQ cận lâm sàng";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
    }
}