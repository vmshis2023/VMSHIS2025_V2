namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT
{
    partial class  frm_BHYT_80A
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
            this.bhyT_80A1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BHYT_80A();
            this.SuspendLayout();
            // 
            // bhyT_80A1
            // 
            this.bhyT_80A1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bhyT_80A1.Location = new System.Drawing.Point(0, 0);
            this.bhyT_80A1.Name = "bhyT_80A1";
            this.bhyT_80A1.Size = new System.Drawing.Size(1008, 730);
            this.bhyT_80A1.TabIndex = 0;
            // 
            // frm_BHYT_80A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.bhyT_80A1);
            this.KeyPreview = true;
            this.Name = "frm_BHYT_80A";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo Bảo hiểm y tế";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.BHYT_80A bhyT_80A1;


    }
}