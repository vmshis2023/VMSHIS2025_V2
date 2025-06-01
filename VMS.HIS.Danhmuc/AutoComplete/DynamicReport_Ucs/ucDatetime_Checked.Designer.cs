namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    partial class ucDatetime_Checked
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
            this.dtpdate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dtpdate
            // 
            this.dtpdate.CustomFormat = "dd/MM/yyyy";
            this.dtpdate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            this.dtpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.dtpdate.DropDownCalendar.Name = "";
            this.dtpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdate.Location = new System.Drawing.Point(98, 0);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.ShowUpDown = true;
            this.dtpdate.Size = new System.Drawing.Size(165, 25);
            this.dtpdate.TabIndex = 12;
            this.dtpdate.Value = new System.DateTime(2019, 8, 10, 0, 0, 0, 0);
            // 
            // chk
            // 
            this.chk.Checked = true;
            this.chk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk.Location = new System.Drawing.Point(0, 0);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(98, 25);
            this.chk.TabIndex = 13;
            this.chk.TabStop = false;
            this.chk.Text = "Từ ngày";
            this.chk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk.UseVisualStyleBackColor = true;
            // 
            // ucDatetime_Checked
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dtpdate);
            this.Controls.Add(this.chk);
            this.Name = "ucDatetime_Checked";
            this.Size = new System.Drawing.Size(263, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Janus.Windows.CalendarCombo.CalendarCombo dtpdate;
        public System.Windows.Forms.CheckBox chk;

    }
}
