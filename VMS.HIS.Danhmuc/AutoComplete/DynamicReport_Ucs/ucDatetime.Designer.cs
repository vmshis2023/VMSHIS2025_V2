namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    partial class ucDatetime
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
            this.dtpdate = new Janus.Windows.CalendarCombo.CalendarCombo();
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
            // dtpdate
            // 
            this.dtpdate.CustomFormat = "dd/MM/yyyy ";
            this.dtpdate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            this.dtpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.dtpdate.DropDownCalendar.Name = "";
            this.dtpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdate.Location = new System.Drawing.Point(104, 0);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.ShowUpDown = true;
            this.dtpdate.Size = new System.Drawing.Size(206, 25);
            this.dtpdate.TabIndex = 12;
            this.dtpdate.Value = new System.DateTime(2019, 8, 10, 0, 0, 0, 0);
            // 
            // ucDatetime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dtpdate);
            this.Controls.Add(this.lblName);
            this.Name = "ucDatetime";
            this.Size = new System.Drawing.Size(310, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblName;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpdate;

    }
}
