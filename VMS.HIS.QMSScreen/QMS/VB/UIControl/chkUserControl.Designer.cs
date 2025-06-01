namespace VMS.QMS.UIControl
{
    partial class chkUserControl
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
            this.txtcheck = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtcheck
            // 
            this.txtcheck.BackColor = System.Drawing.SystemColors.Control;
            this.txtcheck.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcheck.Font = new System.Drawing.Font("Arial", 21F, System.Drawing.FontStyle.Bold);
            this.txtcheck.Location = new System.Drawing.Point(1, 2);
            this.txtcheck.MaxLength = 1;
            this.txtcheck.Multiline = true;
            this.txtcheck.Name = "txtcheck";
            this.txtcheck.ReadOnly = true;
            this.txtcheck.Size = new System.Drawing.Size(39, 37);
            this.txtcheck.TabIndex = 1;
            this.txtcheck.Text = "V";
            this.txtcheck.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtcheck.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.txtcheck.Click += new System.EventHandler(this.txtcheck_Click);
            this.txtcheck.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtcheck_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(42, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "ưu tiên";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkUserControl
            // 
            this.AutoSize = true;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcheck);
            this.Name = "chkUserControl";
            this.Size = new System.Drawing.Size(140, 42);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public Janus.Windows.GridEX.EditControls.EditBox txtcheck;

    }
}