namespace VMS.HIS.UI.EMR
{
    partial class frm_chonky_todieutri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_chonky_todieutri));
            Janus.Windows.GridEX.GridEXLayout grdDaky_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChuaky_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdKy = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdDaky = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdChuaky = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChuaky)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(864, 13);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(134, 35);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmdKy);
            this.uiGroupBox3.Controls.Add(this.cmdExit);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 675);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(1008, 54);
            this.uiGroupBox3.TabIndex = 4;
            // 
            // cmdKy
            // 
            this.cmdKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKy.Image = ((System.Drawing.Image)(resources.GetObject("cmdKy.Image")));
            this.cmdKy.ImageSize = new System.Drawing.Size(28, 28);
            this.cmdKy.Location = new System.Drawing.Point(721, 13);
            this.cmdKy.Name = "cmdKy";
            this.cmdKy.Size = new System.Drawing.Size(134, 35);
            this.cmdKy.TabIndex = 2;
            this.cmdKy.Text = "Kí duyệt";
            this.cmdKy.Click += new System.EventHandler(this.cmdKy_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdDaky);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 343);
            this.uiGroupBox1.TabIndex = 7;
            this.uiGroupBox1.Text = "Các tờ điều trị đã ký";
            // 
            // grdDaky
            // 
            this.grdDaky.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdDaky.AlternatingColors = true;
            this.grdDaky.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdDaky.AutomaticSort = false;
            this.grdDaky.BackColor = System.Drawing.Color.Silver;
            this.grdDaky.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân đưa vào phòng khám</FilterRowInfoText></LocalizableData>";
            grdDaky_DesignTimeLayout.LayoutString = resources.GetString("grdDaky_DesignTimeLayout.LayoutString");
            this.grdDaky.DesignTimeLayout = grdDaky_DesignTimeLayout;
            this.grdDaky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDaky.DynamicFiltering = true;
            this.grdDaky.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdDaky.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDaky.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdDaky.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDaky.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDaky.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdDaky.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDaky.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdDaky.FrozenColumns = -1;
            this.grdDaky.GroupByBoxVisible = false;
            this.grdDaky.LinkFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdDaky.Location = new System.Drawing.Point(0, 17);
            this.grdDaky.Name = "grdDaky";
            this.grdDaky.RecordNavigator = true;
            this.grdDaky.SelectedFormatStyle.Alpha = 2;
            this.grdDaky.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdDaky.SelectedFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdDaky.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDaky.SelectedFormatStyle.ForeColor = System.Drawing.Color.White;
            this.grdDaky.SelectedInactiveFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDaky.Size = new System.Drawing.Size(1008, 323);
            this.grdDaky.TabIndex = 10;
            this.grdDaky.TabStop = false;
            this.grdDaky.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdChuaky);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 343);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 332);
            this.uiGroupBox2.TabIndex = 11;
            this.uiGroupBox2.Text = "Các tờ chưa ký";
            // 
            // grdChuaky
            // 
            this.grdChuaky.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdChuaky.AlternatingColors = true;
            this.grdChuaky.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdChuaky.AutomaticSort = false;
            this.grdChuaky.BackColor = System.Drawing.Color.Silver;
            this.grdChuaky.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân đưa vào phòng khám</FilterRowInfoText></LocalizableData>";
            grdChuaky_DesignTimeLayout.LayoutString = resources.GetString("grdChuaky_DesignTimeLayout.LayoutString");
            this.grdChuaky.DesignTimeLayout = grdChuaky_DesignTimeLayout;
            this.grdChuaky.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChuaky.DynamicFiltering = true;
            this.grdChuaky.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdChuaky.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChuaky.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdChuaky.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChuaky.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChuaky.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdChuaky.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuaky.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdChuaky.FrozenColumns = -1;
            this.grdChuaky.GroupByBoxVisible = false;
            this.grdChuaky.LinkFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdChuaky.Location = new System.Drawing.Point(0, 17);
            this.grdChuaky.Name = "grdChuaky";
            this.grdChuaky.RecordNavigator = true;
            this.grdChuaky.SelectedFormatStyle.Alpha = 2;
            this.grdChuaky.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdChuaky.SelectedFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdChuaky.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChuaky.SelectedFormatStyle.ForeColor = System.Drawing.Color.White;
            this.grdChuaky.SelectedInactiveFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdChuaky.Size = new System.Drawing.Size(1008, 312);
            this.grdChuaky.TabIndex = 10;
            this.grdChuaky.TabStop = false;
            this.grdChuaky.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_chonky_todieutri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiGroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_chonky_todieutri";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In phiếu điều trị";
            this.Load += new System.EventHandler(this.frm_chonky_todieutri_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_chonky_todieutri_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDaky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChuaky)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdDaky;
        private Janus.Windows.EditControls.UIButton cmdKy;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdChuaky;
    }
}