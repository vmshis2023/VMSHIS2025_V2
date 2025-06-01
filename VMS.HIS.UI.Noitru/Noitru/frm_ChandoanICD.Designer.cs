namespace VNS.HIS.UI.NOITRU
{
    partial class frm_ChandoanICD
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
            this.ucChandoanICD1 = new VNS.HIS.UCs.Noitru.ucChandoanICD();
            this.ucThongtinnguoibenh1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh();
            this.SuspendLayout();
            // 
            // ucChandoanICD1
            // 
            this.ucChandoanICD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChandoanICD1.Location = new System.Drawing.Point(0, 111);
            this.ucChandoanICD1.Name = "ucChandoanICD1";
            this.ucChandoanICD1.Size = new System.Drawing.Size(1264, 650);
            this.ucChandoanICD1.TabIndex = 1;
            // 
            // ucThongtinnguoibenh1
            // 
            this.ucThongtinnguoibenh1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh1.Name = "ucThongtinnguoibenh1";
            this.ucThongtinnguoibenh1.Size = new System.Drawing.Size(1264, 111);
            this.ucThongtinnguoibenh1.TabIndex = 0;
            // 
            // frm_ChandoanICD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.ucChandoanICD1);
            this.Controls.Add(this.ucThongtinnguoibenh1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChandoanICD";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chẩn đoán ICD trong quá trình điều trị";
            this.Load += new System.EventHandler(this.frm_ChandoanICD_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.Dungchung.UCs.ucThongtinnguoibenh ucThongtinnguoibenh1;
        private UCs.Noitru.ucChandoanICD ucChandoanICD1;

    }
}