using VNS.HIS.UCs;
namespace VMS.HIS.Danhmuc.UI
{
    partial class frm_themmoi_thamsohethong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_thamsohethong));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtnhomthamso = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.txtSBranchID = new System.Windows.Forms.TextBox();
            this.txtDienGiai = new System.Windows.Forms.RichTextBox();
            this.txtIYear = new System.Windows.Forms.TextBox();
            this.txtIMonth = new System.Windows.Forms.TextBox();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.txtTenThamSo = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 57);
            this.panel1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(423, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 35);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "   Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::VMS.HIS.Danhmuc.Properties.Resources.SAVE__2_;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(297, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Ghi";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtnhomthamso);
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.chkTrangThai);
            this.panel2.Controls.Add(this.txtSBranchID);
            this.panel2.Controls.Add(this.txtDienGiai);
            this.panel2.Controls.Add(this.txtIYear);
            this.panel2.Controls.Add(this.txtIMonth);
            this.panel2.Controls.Add(this.txtGiaTri);
            this.panel2.Controls.Add(this.txtTenThamSo);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(589, 337);
            this.panel2.TabIndex = 2;
            // 
            // txtnhomthamso
            // 
            this.txtnhomthamso._backcolor = System.Drawing.SystemColors.Control;
            this.txtnhomthamso._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnhomthamso._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtnhomthamso.AddValues = true;
            this.txtnhomthamso.AllowMultiline = false;
            this.txtnhomthamso.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtnhomthamso.AutoCompleteList")));
            this.txtnhomthamso.buildShortcut = false;
            this.txtnhomthamso.CaseSensitive = false;
            this.txtnhomthamso.cmdDropDown = null;
            this.txtnhomthamso.CompareNoID = true;
            this.txtnhomthamso.DefaultCode = "-1";
            this.txtnhomthamso.DefaultID = "-1";
            this.txtnhomthamso.Drug_ID = null;
            this.txtnhomthamso.ExtraWidth = 0;
            this.txtnhomthamso.FillValueAfterSelect = false;
            this.txtnhomthamso.Font = new System.Drawing.Font("Arial", 9F);
            this.txtnhomthamso.LOAI_DANHMUC = "DANHMUC_NHOMTHAMSO";
            this.txtnhomthamso.Location = new System.Drawing.Point(126, 112);
            this.txtnhomthamso.MaxHeight = -1;
            this.txtnhomthamso.MinTypedCharacters = 2;
            this.txtnhomthamso.MyCode = "";
            this.txtnhomthamso.MyID = "-1";
            this.txtnhomthamso.Name = "txtnhomthamso";
            this.txtnhomthamso.RaiseEvent = false;
            this.txtnhomthamso.RaiseEventEnter = false;
            this.txtnhomthamso.RaiseEventEnterWhenEmpty = false;
            this.txtnhomthamso.SelectedIndex = -1;
            this.txtnhomthamso.ShowCodeWithValue = false;
            this.txtnhomthamso.Size = new System.Drawing.Size(417, 21);
            this.txtnhomthamso.splitChar = '@';
            this.txtnhomthamso.splitCharIDAndCode = '#';
            this.txtnhomthamso.TabIndex = 4;
            this.txtnhomthamso.TakeCode = false;
            this.txtnhomthamso.txtMyCode = null;
            this.txtnhomthamso.txtMyCode_Edit = null;
            this.txtnhomthamso.txtMyID = null;
            this.txtnhomthamso.txtMyID_Edit = null;
            this.txtnhomthamso.txtMyName = null;
            this.txtnhomthamso.txtMyName_Edit = null;
            this.txtnhomthamso.txtNext = null;
            this.txtnhomthamso.txtNext1 = null;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 306);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(589, 31);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label9.Location = new System.Drawing.Point(27, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "SBranchID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label8.Location = new System.Drawing.Point(540, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "IYear";
            this.label8.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.Location = new System.Drawing.Point(530, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "IMonth";
            this.label6.Visible = false;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Font = new System.Drawing.Font("Arial", 9F);
            this.chkTrangThai.Location = new System.Drawing.Point(126, 284);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(81, 19);
            this.chkTrangThai.TabIndex = 7;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // txtSBranchID
            // 
            this.txtSBranchID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSBranchID.Font = new System.Drawing.Font("Arial", 9F);
            this.txtSBranchID.Location = new System.Drawing.Point(126, 48);
            this.txtSBranchID.Name = "txtSBranchID";
            this.txtSBranchID.Size = new System.Drawing.Size(417, 21);
            this.txtSBranchID.TabIndex = 2;
            this.txtSBranchID.Text = "HIS";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDienGiai.Location = new System.Drawing.Point(126, 174);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(417, 102);
            this.txtDienGiai.TabIndex = 6;
            this.txtDienGiai.Text = "";
            // 
            // txtIYear
            // 
            this.txtIYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIYear.Font = new System.Drawing.Font("Arial", 9F);
            this.txtIYear.Location = new System.Drawing.Point(567, 11);
            this.txtIYear.Name = "txtIYear";
            this.txtIYear.Size = new System.Drawing.Size(10, 21);
            this.txtIYear.TabIndex = 5;
            this.txtIYear.Visible = false;
            // 
            // txtIMonth
            // 
            this.txtIMonth.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIMonth.Font = new System.Drawing.Font("Arial", 9F);
            this.txtIMonth.Location = new System.Drawing.Point(567, 11);
            this.txtIMonth.Name = "txtIMonth";
            this.txtIMonth.Size = new System.Drawing.Size(10, 21);
            this.txtIMonth.TabIndex = 4;
            this.txtIMonth.Visible = false;
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Font = new System.Drawing.Font("Arial", 9F);
            this.txtGiaTri.Location = new System.Drawing.Point(126, 147);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(417, 21);
            this.txtGiaTri.TabIndex = 5;
            // 
            // txtTenThamSo
            // 
            this.txtTenThamSo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTenThamSo.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenThamSo.Location = new System.Drawing.Point(126, 81);
            this.txtTenThamSo.Name = "txtTenThamSo";
            this.txtTenThamSo.Size = new System.Drawing.Size(417, 21);
            this.txtTenThamSo.TabIndex = 3;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9F);
            this.txtID.Location = new System.Drawing.Point(126, 16);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(95, 21);
            this.txtID.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label7.Location = new System.Drawing.Point(27, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Diễn giải";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(27, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Giá trị tham số";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(27, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nhóm tham số";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(27, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên tham số";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_themmoi_thamsohethong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 394);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_themmoi_thamsohethong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm tham số hệ thống";
            this.Load += new System.EventHandler(this.frm_themmoi_thamsohethong_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.RichTextBox txtDienGiai;
        public System.Windows.Forms.TextBox txtGiaTri;
        public System.Windows.Forms.TextBox txtTenThamSo;
        public System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtSBranchID;
        public System.Windows.Forms.TextBox txtIYear;
        public System.Windows.Forms.TextBox txtIMonth;
        private System.Windows.Forms.Label lblMessage;
        private AutoCompleteTextbox_Danhmucchung txtnhomthamso;
    }
}