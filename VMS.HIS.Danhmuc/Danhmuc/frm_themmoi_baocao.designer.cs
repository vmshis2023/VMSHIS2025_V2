namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_baocao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_baocao));
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.chkThemlientuc = new System.Windows.Forms.CheckBox();
            this.cboNhom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTieude = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMoTa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenFileRieng = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenFileMacDinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFileWord = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboPhieuEMR = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(335, 312);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 32);
            this.cmdExit.TabIndex = 17;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(223, 312);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 32);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.label8);
            this.grpControl.Controls.Add(this.cboPhieuEMR);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.txtFileWord);
            this.grpControl.Controls.Add(this.chkThemlientuc);
            this.grpControl.Controls.Add(this.cboNhom);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.txtTieude);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.txtMoTa);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.txtTenFileRieng);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.txtTenFileMacDinh);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtMa);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(511, 306);
            this.grpControl.TabIndex = 0;
            // 
            // chkThemlientuc
            // 
            this.chkThemlientuc.AutoSize = true;
            this.chkThemlientuc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThemlientuc.Location = new System.Drawing.Point(121, 281);
            this.chkThemlientuc.Name = "chkThemlientuc";
            this.chkThemlientuc.Size = new System.Drawing.Size(132, 19);
            this.chkThemlientuc.TabIndex = 15;
            this.chkThemlientuc.Text = "Thêm mới liên tục?";
            this.chkThemlientuc.UseVisualStyleBackColor = true;
            // 
            // cboNhom
            // 
            this.cboNhom.FormattingEnabled = true;
            this.cboNhom.Location = new System.Drawing.Point(121, 90);
            this.cboNhom.Name = "cboNhom";
            this.cboNhom.Size = new System.Drawing.Size(364, 23);
            this.cboNhom.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Tiêu đề:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTieude
            // 
            this.txtTieude.Location = new System.Drawing.Point(121, 63);
            this.txtTieude.MaxLength = 255;
            this.txtTieude.Name = "txtTieude";
            this.txtTieude.Size = new System.Drawing.Size(364, 21);
            this.txtTieude.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mã nhóm :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mô tả thêm:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(121, 220);
            this.txtMoTa.MaxLength = 255;
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(364, 55);
            this.txtMoTa.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tên file riêng";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenFileRieng
            // 
            this.txtTenFileRieng.Location = new System.Drawing.Point(121, 144);
            this.txtTenFileRieng.MaxLength = 100;
            this.txtTenFileRieng.Name = "txtTenFileRieng";
            this.txtTenFileRieng.Size = new System.Drawing.Size(364, 21);
            this.txtTenFileRieng.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên file mặc định :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenFileMacDinh
            // 
            this.txtTenFileMacDinh.Location = new System.Drawing.Point(121, 117);
            this.txtTenFileMacDinh.MaxLength = 100;
            this.txtTenFileMacDinh.Name = "txtTenFileMacDinh";
            this.txtTenFileMacDinh.Size = new System.Drawing.Size(364, 21);
            this.txtTenFileMacDinh.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã báo cáo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(121, 35);
            this.txtMa.MaxLength = 100;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(187, 21);
            this.txtMa.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "File word";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileWord
            // 
            this.txtFileWord.Location = new System.Drawing.Point(121, 169);
            this.txtFileWord.MaxLength = 100;
            this.txtFileWord.Name = "txtFileWord";
            this.txtFileWord.Size = new System.Drawing.Size(364, 21);
            this.txtFileWord.TabIndex = 10;
            // 
            // cboPhieuEMR
            // 
            this.cboPhieuEMR.FormattingEnabled = true;
            this.cboPhieuEMR.Location = new System.Drawing.Point(121, 193);
            this.cboPhieuEMR.Name = "cboPhieuEMR";
            this.cboPhieuEMR.Next_Control = null;
            this.cboPhieuEMR.RaiseEnterEventWhenInvisible = true;
            this.cboPhieuEMR.Size = new System.Drawing.Size(364, 23);
            this.cboPhieuEMR.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Mã phiếu EMR";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_themmoi_baocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(511, 356);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_baocao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin báo cáo";
            this.Load += new System.EventHandler(this.frm_themmoi_baocao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_baocao_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenFileMacDinh;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtMoTa;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenFileRieng;
        private System.Windows.Forms.ComboBox cboNhom;
        private System.Windows.Forms.Label label6;
        internal Janus.Windows.GridEX.EditControls.EditBox txtTieude;
        private System.Windows.Forms.CheckBox chkThemlientuc;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtFileWord;
        private System.Windows.Forms.Label label8;
        private UCs.EasyCompletionComboBox cboPhieuEMR;
    }
}