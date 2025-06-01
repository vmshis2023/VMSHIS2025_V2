namespace VMS.HIS.Danhmuc.UserControls
{
    public partial class medRadioButton
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
            this.lblText = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(45, 0);
            this.lblText.Margin = new System.Windows.Forms.Padding(0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(487, 41);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "Text here";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblText.Click += new System.EventHandler(this.lblText_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64;
            this.pnlImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(45, 41);
            this.pnlImage.TabIndex = 3;
            // 
            // medRadioButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.pnlImage);
            this.Name = "medRadioButton";
            this.Size = new System.Drawing.Size(532, 41);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel pnlImage;
    }
}
