namespace VMS.HIS.Danhmuc.UserControls
{
    partial class medCheckBox_NoText
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(medCheckBox_NoText));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblcheck = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1347596208_Checkbox Empty.png");
            this.imageList1.Images.SetKeyName(1, "tick.png");
            // 
            // lblcheck
            // 
            this.lblcheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblcheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblcheck.ImageIndex = 0;
            this.lblcheck.ImageList = this.imageList1;
            this.lblcheck.Location = new System.Drawing.Point(0, 0);
            this.lblcheck.Margin = new System.Windows.Forms.Padding(0);
            this.lblcheck.Name = "lblcheck";
            this.lblcheck.Size = new System.Drawing.Size(30, 28);
            this.lblcheck.TabIndex = 1;
            this.lblcheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblcheck.Click += new System.EventHandler(this.lblcheck_Click);
            // 
            // medCheckBox_NoText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblcheck);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "medCheckBox_NoText";
            this.Size = new System.Drawing.Size(30, 28);
            this.Load += new System.EventHandler(this.medCheckBox_NoText_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblcheck;
        private System.Windows.Forms.ImageList imageList1;
    }
}
