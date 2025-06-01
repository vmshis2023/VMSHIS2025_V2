namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    partial class ucCombobox
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
            this.lblName = new System.Windows.Forms.Label();
            this.cbo = new Janus.Windows.EditControls.UIComboBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 25);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Từ ngày: ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo
            // 
            this.cbo.BackColor = System.Drawing.SystemColors.Menu;
            this.cbo.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cbo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbo.Font = new System.Drawing.Font("Arial", 9F);
            this.cbo.Location = new System.Drawing.Point(104, 0);
            this.cbo.Name = "cbo";
            this.cbo.Size = new System.Drawing.Size(206, 25);
            this.cbo.TabIndex = 628;
            this.cbo.TabStop = false;
            // 
            // ucCombobox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cbo);
            this.Controls.Add(this.lblName);
            this.Name = "ucCombobox";
            this.Size = new System.Drawing.Size(310, 25);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblName;
        public Janus.Windows.EditControls.UIComboBox cbo;

    }
}
