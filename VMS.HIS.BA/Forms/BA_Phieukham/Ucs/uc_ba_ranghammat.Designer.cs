
namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    partial class uc_ba_ranghammat
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txt_ranghammat_khac = new System.Windows.Forms.TextBox();
            this.label189 = new System.Windows.Forms.Label();
            this.panel114 = new System.Windows.Forms.Panel();
            this.txt_ranghammat_ghiro = new System.Windows.Forms.TextBox();
            this.opt_ranghammat_batthuong = new System.Windows.Forms.RadioButton();
            this.opt_ranghammat_binhthuong = new System.Windows.Forms.RadioButton();
            this.groupBox10.SuspendLayout();
            this.panel114.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txt_ranghammat_ghiro);
            this.groupBox10.Controls.Add(this.txt_ranghammat_khac);
            this.groupBox10.Controls.Add(this.label189);
            this.groupBox10.Controls.Add(this.panel114);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(0, 0);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(1175, 72);
            this.groupBox10.TabIndex = 264543;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "2.8 Răng - hàm - mặt:";
            // 
            // txt_ranghammat_khac
            // 
            this.txt_ranghammat_khac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ranghammat_khac.Location = new System.Drawing.Point(64, 47);
            this.txt_ranghammat_khac.Name = "txt_ranghammat_khac";
            this.txt_ranghammat_khac.Size = new System.Drawing.Size(1101, 21);
            this.txt_ranghammat_khac.TabIndex = 264461;
            // 
            // label189
            // 
            this.label189.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label189.Location = new System.Drawing.Point(6, 45);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(52, 20);
            this.label189.TabIndex = 264460;
            this.label189.Text = "Khác";
            this.label189.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel114
            // 
            this.panel114.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel114.Controls.Add(this.opt_ranghammat_batthuong);
            this.panel114.Controls.Add(this.opt_ranghammat_binhthuong);
            this.panel114.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel114.Location = new System.Drawing.Point(64, 20);
            this.panel114.Name = "panel114";
            this.panel114.Size = new System.Drawing.Size(262, 25);
            this.panel114.TabIndex = 264396;
            this.panel114.TabStop = true;
            // 
            // txt_ranghammat_ghiro
            // 
            this.txt_ranghammat_ghiro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ranghammat_ghiro.Enabled = false;
            this.txt_ranghammat_ghiro.Location = new System.Drawing.Point(332, 22);
            this.txt_ranghammat_ghiro.Name = "txt_ranghammat_ghiro";
            this.txt_ranghammat_ghiro.Size = new System.Drawing.Size(833, 21);
            this.txt_ranghammat_ghiro.TabIndex = 766;
            // 
            // opt_ranghammat_batthuong
            // 
            this.opt_ranghammat_batthuong.AutoSize = true;
            this.opt_ranghammat_batthuong.Location = new System.Drawing.Point(111, 2);
            this.opt_ranghammat_batthuong.Name = "opt_ranghammat_batthuong";
            this.opt_ranghammat_batthuong.Size = new System.Drawing.Size(133, 20);
            this.opt_ranghammat_batthuong.TabIndex = 54;
            this.opt_ranghammat_batthuong.Text = "Bất thường, ghi rõ";
            this.opt_ranghammat_batthuong.UseVisualStyleBackColor = true;
            this.opt_ranghammat_batthuong.CheckedChanged += new System.EventHandler(this.opt_ranghammat_batthuong_CheckedChanged);
            // 
            // opt_ranghammat_binhthuong
            // 
            this.opt_ranghammat_binhthuong.AutoSize = true;
            this.opt_ranghammat_binhthuong.Checked = true;
            this.opt_ranghammat_binhthuong.Location = new System.Drawing.Point(6, 3);
            this.opt_ranghammat_binhthuong.Name = "opt_ranghammat_binhthuong";
            this.opt_ranghammat_binhthuong.Size = new System.Drawing.Size(99, 20);
            this.opt_ranghammat_binhthuong.TabIndex = 55;
            this.opt_ranghammat_binhthuong.TabStop = true;
            this.opt_ranghammat_binhthuong.Text = "Bình thường";
            this.opt_ranghammat_binhthuong.UseVisualStyleBackColor = true;
            // 
            // uc_ba_ranghammat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox10);
            this.Name = "uc_ba_ranghammat";
            this.Size = new System.Drawing.Size(1175, 72);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.panel114.ResumeLayout(false);
            this.panel114.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txt_ranghammat_khac;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.Panel panel114;
        private System.Windows.Forms.TextBox txt_ranghammat_ghiro;
        private System.Windows.Forms.RadioButton opt_ranghammat_batthuong;
        private System.Windows.Forms.RadioButton opt_ranghammat_binhthuong;
    }
}
