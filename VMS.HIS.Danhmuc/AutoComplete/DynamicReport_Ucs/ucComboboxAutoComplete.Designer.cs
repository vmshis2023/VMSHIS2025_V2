namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    partial class ucComboboxAutoComplete
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
            this.cbo = new VNS.HIS.UCs.EasyCompletionComboBox();
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
            this.cbo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbo.FormattingEnabled = true;
            this.cbo.Location = new System.Drawing.Point(104, 0);
            this.cbo.Name = "cbo";
            this.cbo.Next_Control = null;
            this.cbo.RaiseEnterEventWhenInvisible = false;
            this.cbo.Size = new System.Drawing.Size(206, 21);
            this.cbo.TabIndex = 4;
            // 
            // ucComboboxAutoComplete
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cbo);
            this.Controls.Add(this.lblName);
            this.Name = "ucComboboxAutoComplete";
            this.Size = new System.Drawing.Size(310, 25);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblName;
        private VNS.HIS.UCs.EasyCompletionComboBox cbo;
    }
}
