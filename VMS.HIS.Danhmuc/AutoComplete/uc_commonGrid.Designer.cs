namespace VNS.HIS.UCs
{
    partial class uc_commonGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Janus.Windows.GridEX.GridEXLayout grdICD_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_commonGrid));
            this.grdICD = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.grdICD)).BeginInit();
            this.SuspendLayout();
            // 
            // grdICD
            // 
            this.grdICD.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            grdICD_DesignTimeLayout.LayoutString = resources.GetString("grdICD_DesignTimeLayout.LayoutString");
            this.grdICD.DesignTimeLayout = grdICD_DesignTimeLayout;
            this.grdICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdICD.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdICD.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdICD.Font = new System.Drawing.Font("Arial", 9F);
            this.grdICD.GroupByBoxVisible = false;
            this.grdICD.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdICD.Location = new System.Drawing.Point(0, 0);
            this.grdICD.Name = "grdICD";
            this.grdICD.RecordNavigator = true;
            this.grdICD.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdICD.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdICD.Size = new System.Drawing.Size(803, 273);
            this.grdICD.TabIndex = 518;
            this.grdICD.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // uc_commonGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdICD);
            this.Name = "uc_commonGrid";
            this.Size = new System.Drawing.Size(803, 273);
            ((System.ComponentModel.ISupportInitialize)(this.grdICD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Janus.Windows.GridEX.GridEX grdICD;


    }
}
