
namespace VMS.HIS.Danhmuc.UserControls
{
    partial class uc_lichsu_sudungthuoc
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
            Janus.Windows.GridEX.GridEXLayout grdDonthuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_lichsu_sudungthuoc));
            this.grdDonthuoc = new Janus.Windows.GridEX.GridEX();
            this.cboThuoc = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdDonthuoc)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDonthuoc
            // 
            this.grdDonthuoc.AlternatingColors = true;
            this.grdDonthuoc.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdDonthuoc_DesignTimeLayout.LayoutString = resources.GetString("grdDonthuoc_DesignTimeLayout.LayoutString");
            this.grdDonthuoc.DesignTimeLayout = grdDonthuoc_DesignTimeLayout;
            this.grdDonthuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDonthuoc.DynamicFiltering = true;
            this.grdDonthuoc.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDonthuoc.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDonthuoc.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDonthuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.grdDonthuoc.GroupByBoxVisible = false;
            this.grdDonthuoc.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdDonthuoc.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdDonthuoc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDonthuoc.Location = new System.Drawing.Point(0, 0);
            this.grdDonthuoc.Name = "grdDonthuoc";
            this.grdDonthuoc.RecordNavigator = true;
            this.grdDonthuoc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDonthuoc.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdDonthuoc.SettingsKey = "grdPresDetail";
            this.grdDonthuoc.Size = new System.Drawing.Size(751, 564);
            this.grdDonthuoc.TabIndex = 3;
            this.grdDonthuoc.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDonthuoc.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdDonthuoc.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDonthuoc.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDonthuoc.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDonthuoc.UseGroupRowSelector = true;
            // 
            // cboThuoc
            // 
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new System.Drawing.Point(106, 6);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Next_Control = null;
            this.cboThuoc.RaiseEnterEventWhenInvisible = true;
            this.cboThuoc.Size = new System.Drawing.Size(594, 21);
            this.cboThuoc.TabIndex = 28;
            // 
            // label77
            // 
            this.label77.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Image = ((System.Drawing.Image)(resources.GetObject("label77.Image")));
            this.label77.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label77.Location = new System.Drawing.Point(16, 6);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(84, 21);
            this.label77.TabIndex = 614;
            this.label77.Text = "Lọc thuốc";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.label77);
            this.pnlFilter.Controls.Add(this.cboThuoc);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(751, 0);
            this.pnlFilter.TabIndex = 0;
            this.pnlFilter.Visible = false;
            // 
            // uc_lichsu_sudungthuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdDonthuoc);
            this.Controls.Add(this.pnlFilter);
            this.Name = "uc_lichsu_sudungthuoc";
            this.Size = new System.Drawing.Size(751, 564);
            ((System.ComponentModel.ISupportInitialize)(this.grdDonthuoc)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.GridEX.GridEX grdDonthuoc;
        private VNS.HIS.UCs.EasyCompletionComboBox cboThuoc;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Panel pnlFilter;
    }
}
