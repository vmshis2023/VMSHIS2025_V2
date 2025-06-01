namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frmUpdateMaBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateMaBenhAn));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem6 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem7 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem8 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.txtmalankham = new System.Windows.Forms.TextBox();
            this.txtmabenhanmoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboloaibenhan = new Janus.Windows.EditControls.UIComboBox();
            this.txtnamsinhcu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txttenbenhnhancu = new System.Windows.Forms.TextBox();
            this.txtidbenhnhancu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdate.Location = new System.Drawing.Point(114, 193);
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
            this.cmdThoat.Location = new System.Drawing.Point(249, 193);
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
            this.txtmalankham.Location = new System.Drawing.Point(112, 23);
            this.txtmalankham.Multiline = true;
            this.txtmalankham.Name = "txtmalankham";
            this.txtmalankham.ReadOnly = true;
            this.txtmalankham.Size = new System.Drawing.Size(195, 30);
            this.txtmalankham.TabIndex = 2;
            this.txtmalankham.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtmabenhanmoi
            // 
            this.txtmabenhanmoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmabenhanmoi.Location = new System.Drawing.Point(112, 135);
            this.txtmabenhanmoi.Multiline = true;
            this.txtmabenhanmoi.Name = "txtmabenhanmoi";
            this.txtmabenhanmoi.Size = new System.Drawing.Size(195, 30);
            this.txtmabenhanmoi.TabIndex = 3;
            this.txtmabenhanmoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lần khám";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã BA mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Loại bệnh án";
            // 
            // cboloaibenhan
            // 
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Chọn bệnh án";
            uiComboBoxItem1.Value = "-1";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Bệnh án Đái tháo đường";
            uiComboBoxItem2.Value = "DTD";
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Bệnh án Tăng huyết áp";
            uiComboBoxItem3.Value = "THA";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Bệnh án Basedow";
            uiComboBoxItem4.Value = "BAS";
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "Bệnh án COP";
            uiComboBoxItem5.Value = "COP";
            uiComboBoxItem6.FormatStyle.Alpha = 0;
            uiComboBoxItem6.IsSeparator = false;
            uiComboBoxItem6.Text = "Bệnh án Viêm gan B";
            uiComboBoxItem6.Value = "VGB";
            uiComboBoxItem7.FormatStyle.Alpha = 0;
            uiComboBoxItem7.IsSeparator = false;
            uiComboBoxItem7.Text = "Bệnh án Tai mũi họng";
            uiComboBoxItem7.Value = "TMH";
            uiComboBoxItem8.FormatStyle.Alpha = 0;
            uiComboBoxItem8.IsSeparator = false;
            uiComboBoxItem8.Text = "Bệnh án Răng hàm mặt";
            uiComboBoxItem8.Value = "RHM";
            this.cboloaibenhan.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3,
            uiComboBoxItem4,
            uiComboBoxItem5,
            uiComboBoxItem6,
            uiComboBoxItem7,
            uiComboBoxItem8});
            this.cboloaibenhan.Location = new System.Drawing.Point(112, 96);
            this.cboloaibenhan.Name = "cboloaibenhan";
            this.cboloaibenhan.SelectedIndex = 0;
            this.cboloaibenhan.Size = new System.Drawing.Size(195, 23);
            this.cboloaibenhan.TabIndex = 8;
            this.cboloaibenhan.Text = "Chọn bệnh án";
            // 
            // txtnamsinhcu
            // 
            this.txtnamsinhcu.Location = new System.Drawing.Point(313, 60);
            this.txtnamsinhcu.Multiline = true;
            this.txtnamsinhcu.Name = "txtnamsinhcu";
            this.txtnamsinhcu.ReadOnly = true;
            this.txtnamsinhcu.Size = new System.Drawing.Size(66, 30);
            this.txtnamsinhcu.TabIndex = 15;
            this.txtnamsinhcu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tên bệnh nhân";
            // 
            // txttenbenhnhancu
            // 
            this.txttenbenhnhancu.Location = new System.Drawing.Point(112, 60);
            this.txttenbenhnhancu.Multiline = true;
            this.txttenbenhnhancu.Name = "txttenbenhnhancu";
            this.txttenbenhnhancu.ReadOnly = true;
            this.txttenbenhnhancu.Size = new System.Drawing.Size(195, 30);
            this.txttenbenhnhancu.TabIndex = 13;
            this.txttenbenhnhancu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtidbenhnhancu
            // 
            this.txtidbenhnhancu.Location = new System.Drawing.Point(313, 23);
            this.txtidbenhnhancu.Multiline = true;
            this.txtidbenhnhancu.Name = "txtidbenhnhancu";
            this.txtidbenhnhancu.ReadOnly = true;
            this.txtidbenhnhancu.Size = new System.Drawing.Size(66, 30);
            this.txtidbenhnhancu.TabIndex = 17;
            this.txtidbenhnhancu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmUpdateMaBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 245);
            this.Controls.Add(this.txtidbenhnhancu);
            this.Controls.Add(this.txtnamsinhcu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txttenbenhnhancu);
            this.Controls.Add(this.cboloaibenhan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmabenhanmoi);
            this.Controls.Add(this.txtmalankham);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdUpdate);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateMaBenhAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Mã Bệnh Án";
            this.Load += new System.EventHandler(this.frmUpdateMaBenhAn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.TextBox txtmabenhanmoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtmalankham;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboloaibenhan;
        public System.Windows.Forms.TextBox txtnamsinhcu;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txttenbenhnhancu;
        public System.Windows.Forms.TextBox txtidbenhnhancu;
    }
}