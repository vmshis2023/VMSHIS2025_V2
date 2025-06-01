namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frm_CapnhatNguon_Doitac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CapnhatNguon_Doitac));
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.txtmalankham = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnamsinhcu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txttenbenhnhancu = new System.Windows.Forms.TextBox();
            this.txtidbenhnhancu = new System.Windows.Forms.TextBox();
            this.cboDoitac = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboNguongioithieu = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.lblNguonGT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDoitaccu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNguonGTCu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdate.Location = new System.Drawing.Point(370, 234);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(125, 39);
            this.cmdUpdate.TabIndex = 0;
            this.cmdUpdate.Text = "Chấp nhận";
            this.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdThoat.Location = new System.Drawing.Point(505, 234);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(118, 39);
            this.cmdThoat.TabIndex = 1;
            this.cmdThoat.Text = "Thoát";
            this.cmdThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // txtmalankham
            // 
            this.txtmalankham.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmalankham.Location = new System.Drawing.Point(119, 24);
            this.txtmalankham.Multiline = true;
            this.txtmalankham.Name = "txtmalankham";
            this.txtmalankham.ReadOnly = true;
            this.txtmalankham.Size = new System.Drawing.Size(138, 30);
            this.txtmalankham.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lần khám";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtnamsinhcu
            // 
            this.txtnamsinhcu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnamsinhcu.Location = new System.Drawing.Point(629, 64);
            this.txtnamsinhcu.Multiline = true;
            this.txtnamsinhcu.Name = "txtnamsinhcu";
            this.txtnamsinhcu.ReadOnly = true;
            this.txtnamsinhcu.Size = new System.Drawing.Size(66, 30);
            this.txtnamsinhcu.TabIndex = 15;
            this.txtnamsinhcu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(263, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 27);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tên bệnh nhân";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttenbenhnhancu
            // 
            this.txttenbenhnhancu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttenbenhnhancu.Location = new System.Drawing.Point(374, 24);
            this.txttenbenhnhancu.Multiline = true;
            this.txttenbenhnhancu.Name = "txttenbenhnhancu";
            this.txttenbenhnhancu.ReadOnly = true;
            this.txttenbenhnhancu.Size = new System.Drawing.Size(249, 30);
            this.txttenbenhnhancu.TabIndex = 13;
            // 
            // txtidbenhnhancu
            // 
            this.txtidbenhnhancu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtidbenhnhancu.Location = new System.Drawing.Point(629, 27);
            this.txtidbenhnhancu.Multiline = true;
            this.txtidbenhnhancu.Name = "txtidbenhnhancu";
            this.txtidbenhnhancu.ReadOnly = true;
            this.txtidbenhnhancu.Size = new System.Drawing.Size(66, 30);
            this.txtidbenhnhancu.TabIndex = 17;
            this.txtidbenhnhancu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboDoitac
            // 
            this.cboDoitac.FormattingEnabled = true;
            this.cboDoitac.Location = new System.Drawing.Point(119, 160);
            this.cboDoitac.Name = "cboDoitac";
            this.cboDoitac.Next_Control = null;
            this.cboDoitac.RaiseEnterEventWhenInvisible = true;
            this.cboDoitac.Size = new System.Drawing.Size(504, 24);
            this.cboDoitac.TabIndex = 623;
            // 
            // cboNguongioithieu
            // 
            this.cboNguongioithieu.FormattingEnabled = true;
            this.cboNguongioithieu.Location = new System.Drawing.Point(119, 130);
            this.cboNguongioithieu.Name = "cboNguongioithieu";
            this.cboNguongioithieu.Next_Control = null;
            this.cboNguongioithieu.RaiseEnterEventWhenInvisible = true;
            this.cboNguongioithieu.Size = new System.Drawing.Size(504, 24);
            this.cboNguongioithieu.TabIndex = 622;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(6, 162);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(107, 21);
            this.label33.TabIndex = 625;
            this.label33.Text = "Đối tác mới:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNguonGT
            // 
            this.lblNguonGT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguonGT.ForeColor = System.Drawing.Color.Red;
            this.lblNguonGT.Location = new System.Drawing.Point(6, 132);
            this.lblNguonGT.Name = "lblNguonGT";
            this.lblNguonGT.Size = new System.Drawing.Size(107, 21);
            this.lblNguonGT.TabIndex = 624;
            this.lblNguonGT.Text = "Nguồn GT mới:";
            this.lblNguonGT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 27);
            this.label2.TabIndex = 629;
            this.label2.Text = "Đối tác cũ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDoitaccu
            // 
            this.txtDoitaccu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoitaccu.Location = new System.Drawing.Point(119, 94);
            this.txtDoitaccu.Multiline = true;
            this.txtDoitaccu.Name = "txtDoitaccu";
            this.txtDoitaccu.ReadOnly = true;
            this.txtDoitaccu.Size = new System.Drawing.Size(504, 30);
            this.txtDoitaccu.TabIndex = 628;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 27);
            this.label3.TabIndex = 627;
            this.label3.Text = "Nguồn GT cũ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNguonGTCu
            // 
            this.txtNguonGTCu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguonGTCu.Location = new System.Drawing.Point(119, 59);
            this.txtNguonGTCu.Multiline = true;
            this.txtNguonGTCu.Name = "txtNguonGTCu";
            this.txtNguonGTCu.ReadOnly = true;
            this.txtNguonGTCu.Size = new System.Drawing.Size(504, 30);
            this.txtNguonGTCu.TabIndex = 626;
            // 
            // frm_CapnhatNguon_Doitac
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(627, 285);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDoitaccu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNguonGTCu);
            this.Controls.Add(this.cboDoitac);
            this.Controls.Add(this.cboNguongioithieu);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblNguonGT);
            this.Controls.Add(this.txtidbenhnhancu);
            this.Controls.Add(this.txtnamsinhcu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txttenbenhnhancu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmalankham);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdUpdate);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CapnhatNguon_Doitac";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật Nguồn-Đối tác";
            this.Load += new System.EventHandler(this.frm_CapnhatNguon_Doitac_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtmalankham;
        public System.Windows.Forms.TextBox txtnamsinhcu;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txttenbenhnhancu;
        public System.Windows.Forms.TextBox txtidbenhnhancu;
        private HIS.UCs.EasyCompletionComboBox cboDoitac;
        private HIS.UCs.EasyCompletionComboBox cboNguongioithieu;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblNguonGT;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDoitaccu;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtNguonGTCu;
    }
}