namespace VNS.HIS.UCs
{
    partial class ucGrid
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
            Janus.Windows.GridEX.GridEXLayout grdListDrug_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGrid));
            this.grdListDrug = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.grdListDrug)).BeginInit();
            this.SuspendLayout();
            // 
            // grdListDrug
            // 
            this.grdListDrug.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            grdListDrug_DesignTimeLayout.LayoutString = resources.GetString("grdListDrug_DesignTimeLayout.LayoutString");
            this.grdListDrug.DesignTimeLayout = grdListDrug_DesignTimeLayout;
            this.grdListDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListDrug.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdListDrug.Font = new System.Drawing.Font("Arial", 9F);
            this.grdListDrug.GroupByBoxVisible = false;
            this.grdListDrug.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListDrug.Location = new System.Drawing.Point(0, 0);
            this.grdListDrug.Name = "grdListDrug";
            this.grdListDrug.RecordNavigator = true;
            this.grdListDrug.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListDrug.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdListDrug.Size = new System.Drawing.Size(803, 273);
            this.grdListDrug.TabIndex = 518;
            this.grdListDrug.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // ucGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdListDrug);
            this.Name = "ucGrid";
            this.Size = new System.Drawing.Size(803, 273);
            ((System.ComponentModel.ISupportInitialize)(this.grdListDrug)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Janus.Windows.GridEX.GridEX grdListDrug;


    }
}
