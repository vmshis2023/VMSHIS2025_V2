namespace VNS.HIS.UI.HOADONDO
{
    partial class frm_AllocateRedInvoice
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AllocateRedInvoice));
            this.cmdThoat = new System.Windows.Forms.Button();
            this.cmdGhi = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSoQuyen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSerieCuoi = new System.Windows.Forms.Label();
            this.lblSerieDau = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerie_HienTai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTrang_Thai = new Janus.Windows.EditControls.UIComboBox();
            this.txtSerie_Cuoi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMau_HD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSerie_Dau = new System.Windows.Forms.TextBox();
            this.lblintOrder = new System.Windows.Forms.Label();
            this.txtKi_Hieu = new System.Windows.Forms.TextBox();
            this.lblDepartment_Name = new System.Windows.Forms.Label();
            this.txtNhanvien = new VNS.HIS.UCs.AutoCompleteTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdThoat.Location = new System.Drawing.Point(251, 254);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(116, 31);
            this.cmdThoat.TabIndex = 9;
            this.cmdThoat.Text = "&Thoát(Esc)";
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // cmdGhi
            // 
            this.cmdGhi.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdGhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdGhi.Location = new System.Drawing.Point(95, 254);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(150, 31);
            this.cmdGhi.TabIndex = 8;
            this.cmdGhi.Text = "&Lưu(Ctrl+S)";
            this.cmdGhi.UseVisualStyleBackColor = true;
            this.cmdGhi.Click += new System.EventHandler(this.cmdGhi_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtNhanvien);
            this.uiGroupBox1.Controls.Add(this.label8);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.txtSoQuyen);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.lblSerieCuoi);
            this.uiGroupBox1.Controls.Add(this.lblSerieDau);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtSerie_HienTai);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.cboTrang_Thai);
            this.uiGroupBox1.Controls.Add(this.txtSerie_Cuoi);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtMau_HD);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtSerie_Dau);
            this.uiGroupBox1.Controls.Add(this.lblintOrder);
            this.uiGroupBox1.Controls.Add(this.txtKi_Hieu);
            this.uiGroupBox1.Controls.Add(this.lblDepartment_Name);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(452, 248);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Cấp phát hóa đơn";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(364, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "(*)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(364, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 88;
            this.label7.Text = "(*)";
            // 
            // txtSoQuyen
            // 
            this.txtSoQuyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSoQuyen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoQuyen.Location = new System.Drawing.Point(117, 79);
            this.txtSoQuyen.MaxLength = 99999;
            this.txtSoQuyen.Name = "txtSoQuyen";
            this.txtSoQuyen.ReadOnly = true;
            this.txtSoQuyen.Size = new System.Drawing.Size(241, 22);
            this.txtSoQuyen.TabIndex = 2;
            this.txtSoQuyen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 86;
            this.label3.Text = "Số Quyển";
            // 
            // lblSerieCuoi
            // 
            this.lblSerieCuoi.AutoSize = true;
            this.lblSerieCuoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerieCuoi.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblSerieCuoi.Location = new System.Drawing.Point(364, 162);
            this.lblSerieCuoi.Name = "lblSerieCuoi";
            this.lblSerieCuoi.Size = new System.Drawing.Size(20, 13);
            this.lblSerieCuoi.TabIndex = 85;
            this.lblSerieCuoi.Text = "(*)";
            // 
            // lblSerieDau
            // 
            this.lblSerieDau.AutoSize = true;
            this.lblSerieDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerieDau.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblSerieDau.Location = new System.Drawing.Point(364, 135);
            this.lblSerieDau.Name = "lblSerieDau";
            this.lblSerieDau.Size = new System.Drawing.Size(20, 13);
            this.lblSerieDau.TabIndex = 84;
            this.lblSerieDau.Text = "(*)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 83;
            this.label1.Text = "Serie hiện tại";
            // 
            // txtSerie_HienTai
            // 
            this.txtSerie_HienTai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerie_HienTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie_HienTai.Location = new System.Drawing.Point(117, 185);
            this.txtSerie_HienTai.Name = "txtSerie_HienTai";
            this.txtSerie_HienTai.Size = new System.Drawing.Size(241, 21);
            this.txtSerie_HienTai.TabIndex = 6;
            this.txtSerie_HienTai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 81;
            this.label2.Text = "Mã NV";
            // 
            // cboTrang_Thai
            // 
            this.cboTrang_Thai.Location = new System.Drawing.Point(117, 212);
            this.cboTrang_Thai.Name = "cboTrang_Thai";
            this.cboTrang_Thai.Size = new System.Drawing.Size(241, 22);
            this.cboTrang_Thai.TabIndex = 7;
            // 
            // txtSerie_Cuoi
            // 
            this.txtSerie_Cuoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerie_Cuoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie_Cuoi.Location = new System.Drawing.Point(117, 158);
            this.txtSerie_Cuoi.Name = "txtSerie_Cuoi";
            this.txtSerie_Cuoi.Size = new System.Drawing.Size(241, 21);
            this.txtSerie_Cuoi.TabIndex = 5;
            this.txtSerie_Cuoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSerie_Cuoi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerie_Cuoi_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 65;
            this.label6.Text = "Trạng thái";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 64;
            this.label5.Text = "Mẫu hóa đơn";
            // 
            // txtMau_HD
            // 
            this.txtMau_HD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtMau_HD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMau_HD.Enabled = false;
            this.txtMau_HD.Location = new System.Drawing.Point(117, 26);
            this.txtMau_HD.Name = "txtMau_HD";
            this.txtMau_HD.ReadOnly = true;
            this.txtMau_HD.Size = new System.Drawing.Size(241, 22);
            this.txtMau_HD.TabIndex = 0;
            this.txtMau_HD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 63;
            this.label4.Text = "Serie đầu";
            // 
            // txtSerie_Dau
            // 
            this.txtSerie_Dau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerie_Dau.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie_Dau.Location = new System.Drawing.Point(117, 131);
            this.txtSerie_Dau.Name = "txtSerie_Dau";
            this.txtSerie_Dau.Size = new System.Drawing.Size(241, 21);
            this.txtSerie_Dau.TabIndex = 4;
            this.txtSerie_Dau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSerie_Dau.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerie_Dau_KeyPress);
            // 
            // lblintOrder
            // 
            this.lblintOrder.AutoSize = true;
            this.lblintOrder.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblintOrder.Location = new System.Drawing.Point(16, 160);
            this.lblintOrder.Name = "lblintOrder";
            this.lblintOrder.Size = new System.Drawing.Size(71, 16);
            this.lblintOrder.TabIndex = 54;
            this.lblintOrder.Text = "Serie cuối";
            // 
            // txtKi_Hieu
            // 
            this.txtKi_Hieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtKi_Hieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKi_Hieu.Enabled = false;
            this.txtKi_Hieu.Location = new System.Drawing.Point(117, 52);
            this.txtKi_Hieu.MaxLength = 99999;
            this.txtKi_Hieu.Name = "txtKi_Hieu";
            this.txtKi_Hieu.ReadOnly = true;
            this.txtKi_Hieu.Size = new System.Drawing.Size(241, 22);
            this.txtKi_Hieu.TabIndex = 1;
            this.txtKi_Hieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDepartment_Name
            // 
            this.lblDepartment_Name.AutoSize = true;
            this.lblDepartment_Name.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment_Name.Location = new System.Drawing.Point(16, 54);
            this.lblDepartment_Name.Name = "lblDepartment_Name";
            this.lblDepartment_Name.Size = new System.Drawing.Size(52, 16);
            this.lblDepartment_Name.TabIndex = 48;
            this.lblDepartment_Name.Text = "Kí hiệu";
            // 
            // txtNhanvien
            // 
            this.txtNhanvien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhanvien.AutoCompleteList")));
            this.txtNhanvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhanvien.CaseSensitive = false;
            this.txtNhanvien.CompareNoID = true;
            this.txtNhanvien.DefaultCode = "-1";
            this.txtNhanvien.DefaultID = "-1";
            this.txtNhanvien.Drug_ID = null;
            this.txtNhanvien.ExtraWidth = 100;
            this.txtNhanvien.FillValueAfterSelect = false;
            this.txtNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvien.Location = new System.Drawing.Point(117, 104);
            this.txtNhanvien.MaxHeight = 289;
            this.txtNhanvien.MinTypedCharacters = 2;
            this.txtNhanvien.MyCode = "-1";
            this.txtNhanvien.MyID = "-1";
            this.txtNhanvien.Name = "txtNhanvien";
            this.txtNhanvien.RaiseEvent = true;
            this.txtNhanvien.RaiseEventEnter = false;
            this.txtNhanvien.RaiseEventEnterWhenEmpty = true;
            this.txtNhanvien.SelectedIndex = -1;
            this.txtNhanvien.Size = new System.Drawing.Size(241, 21);
            this.txtNhanvien.splitChar = '@';
            this.txtNhanvien.splitCharIDAndCode = '#';
            this.txtNhanvien.TabIndex = 3;
            this.txtNhanvien.TakeCode = false;
            this.txtNhanvien.txtMyCode = null;
            this.txtNhanvien.txtMyCode_Edit = null;
            this.txtNhanvien.txtMyID = null;
            this.txtNhanvien.txtMyID_Edit = null;
            this.txtNhanvien.txtMyName = null;
            this.txtNhanvien.txtMyName_Edit = null;
            this.txtNhanvien.txtNext = null;
            // 
            // frm_AllocateRedInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 299);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdGhi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_AllocateRedInvoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CẤP PHÁT HÓA ĐƠN";
            this.Load += new System.EventHandler(this.frm_AddRedInvoice_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_AddRedInvoice_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Button cmdGhi;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSerie_Dau;
        // private System.Windows.Forms.RadioButton radChucnang;
        private System.Windows.Forms.Label lblintOrder;
        private System.Windows.Forms.Label lblDepartment_Name;
        private System.Windows.Forms.TextBox txtSerie_Cuoi;
        private Janus.Windows.EditControls.UIComboBox cboTrang_Thai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerie_HienTai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSerieCuoi;
        private System.Windows.Forms.TextBox txtMau_HD;
        private System.Windows.Forms.TextBox txtKi_Hieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoQuyen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSerieDau;
        private System.Windows.Forms.Label label8;
        private UCs.AutoCompleteTextbox txtNhanvien;
    }
}