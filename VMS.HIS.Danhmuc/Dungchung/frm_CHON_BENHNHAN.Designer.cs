namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_CHON_BENHNHAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CHON_BENHNHAN));
            Janus.Windows.GridEX.GridEXLayout grdPatientList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grdPatientList = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatientList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPatientList
            // 
            this.grdPatientList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPatientList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grdPatientList.BackgroundImage")));
            this.grdPatientList.ColumnAutoResize = true;
            grdPatientList_DesignTimeLayout.LayoutString = resources.GetString("grdPatientList_DesignTimeLayout.LayoutString");
            this.grdPatientList.DesignTimeLayout = grdPatientList_DesignTimeLayout;
            this.grdPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatientList.DynamicFiltering = true;
            this.grdPatientList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPatientList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdPatientList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPatientList.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdPatientList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPatientList.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdPatientList.GroupByBoxVisible = false;
            this.grdPatientList.Location = new System.Drawing.Point(0, 0);
            this.grdPatientList.Name = "grdPatientList";
            this.grdPatientList.RecordNavigator = true;
            this.grdPatientList.Size = new System.Drawing.Size(784, 562);
            this.grdPatientList.TabIndex = 27;
            this.grdPatientList.TreeLines = false;
            this.grdPatientList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // frm_CHON_BENHNHAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grdPatientList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CHON_BENHNHAN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn Bệnh nhân";
            ((System.ComponentModel.ISupportInitialize)(this.grdPatientList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grdPatientList;

    }
}