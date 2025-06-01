namespace VNS.HIS.UI.HinhAnh
{
    partial class frm_chonXN_Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_chonXN_Print));
            Janus.Windows.GridEX.GridEXLayout grdChidinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdInKQXN = new Janus.Windows.EditControls.UIButton();
            this.grdChidinh = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdInKQXN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 376);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 45);
            this.panel1.TabIndex = 8;
            // 
            // cmdInKQXN
            // 
            this.cmdInKQXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInKQXN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInKQXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInKQXN.Image")));
            this.cmdInKQXN.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInKQXN.Location = new System.Drawing.Point(443, 6);
            this.cmdInKQXN.Name = "cmdInKQXN";
            this.cmdInKQXN.Size = new System.Drawing.Size(129, 30);
            this.cmdInKQXN.TabIndex = 456;
            this.cmdInKQXN.TabStop = false;
            this.cmdInKQXN.Text = "In kết quả XN";
            this.cmdInKQXN.Click += new System.EventHandler(this.cmdInKQXN_Click);
            // 
            // grdChidinh
            // 
            this.grdChidinh.AlternatingColors = true;
            grdChidinh_DesignTimeLayout.LayoutString = resources.GetString("grdChidinh_DesignTimeLayout.LayoutString");
            this.grdChidinh.DesignTimeLayout = grdChidinh_DesignTimeLayout;
            this.grdChidinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChidinh.DynamicFiltering = true;
            this.grdChidinh.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdChidinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChidinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChidinh.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChidinh.GroupByBoxVisible = false;
            this.grdChidinh.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdChidinh.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdChidinh.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdChidinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChidinh.Location = new System.Drawing.Point(0, 0);
            this.grdChidinh.Name = "grdChidinh";
            this.grdChidinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChidinh.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdChidinh.Size = new System.Drawing.Size(584, 376);
            this.grdChidinh.TabIndex = 255;
            this.grdChidinh.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdChidinh.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChidinh.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdChidinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChidinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_chonXN_Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 421);
            this.Controls.Add(this.grdChidinh);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_chonXN_Print";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In kết quả xét nghiệm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.GridEX.GridEX grdChidinh;
        private Janus.Windows.EditControls.UIButton cmdInKQXN;
    }
}