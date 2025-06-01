namespace VMS.QMS
{
    partial class frm_Config
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
            this.grdProperties = new System.Windows.Forms.PropertyGrid();
            this.cmdColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // grdProperties
            // 
            this.grdProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProperties.Location = new System.Drawing.Point(0, 0);
            this.grdProperties.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdProperties.Name = "grdProperties";
            this.grdProperties.Size = new System.Drawing.Size(609, 642);
            this.grdProperties.TabIndex = 3;
            this.grdProperties.SelectedObjectsChanged += new System.EventHandler(this.grdProperties_SelectedObjectsChanged);
            // 
            // cmdColor
            // 
            this.cmdColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdColor.Location = new System.Drawing.Point(485, -1);
            this.cmdColor.Name = "cmdColor";
            this.cmdColor.Size = new System.Drawing.Size(124, 28);
            this.cmdColor.TabIndex = 4;
            this.cmdColor.Text = "Common Color";
            this.cmdColor.UseVisualStyleBackColor = true;
            this.cmdColor.Click += new System.EventHandler(this.cmdColor_Click);
            // 
            // frm_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 642);
            this.Controls.Add(this.cmdColor);
            this.Controls.Add(this.grdProperties);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình";
            this.Load += new System.EventHandler(this.frm_Config_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid grdProperties;
        private System.Windows.Forms.Button cmdColor;
    }
}