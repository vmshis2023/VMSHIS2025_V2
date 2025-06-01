namespace VNS.HIS.UI.BENH_AN
{
    partial class frm_DSACH_BN
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
            Janus.Windows.GridEX.GridEXLayout grdPatient_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DSACH_BN));
            this.grdPatient = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPatient
            // 
            this.grdPatient.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPatient.ColumnAutoResize = true;
            this.grdPatient.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdPatient_DesignTimeLayout.LayoutString = resources.GetString("grdPatient_DesignTimeLayout.LayoutString");
            this.grdPatient.DesignTimeLayout = grdPatient_DesignTimeLayout;
            this.grdPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatient.DynamicFiltering = true;
            this.grdPatient.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdPatient.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPatient.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPatient.GroupByBoxVisible = false;
            this.grdPatient.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPatient.Location = new System.Drawing.Point(0, 0);
            this.grdPatient.Name = "grdPatient";
            this.grdPatient.RecordNavigator = true;
            this.grdPatient.Size = new System.Drawing.Size(594, 269);
            this.grdPatient.TabIndex = 0;
            this.grdPatient.DoubleClick += new System.EventHandler(this.grdPatient_DoubleClick);
            this.grdPatient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdPatient_KeyDown);
            // 
            // frm_DSACH_BN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 269);
            this.Controls.Add(this.grdPatient);
            this.KeyPreview = true;
            this.Name = "frm_DSACH_BN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách thông tin tìm kiếm bệnh nhân";
            this.Load += new System.EventHandler(this.frm_DSACH_BN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grdPatient;
    }
}