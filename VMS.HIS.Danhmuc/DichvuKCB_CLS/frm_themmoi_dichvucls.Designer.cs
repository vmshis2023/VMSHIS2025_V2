namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_dichvucls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_dichvucls));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.chkIntachphieu = new System.Windows.Forms.CheckBox();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txttenphieutrakqCDHA = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkKiemnghiem = new System.Windows.Forms.CheckBox();
            this.txtThetichtoithieu = new MaskedTextBox.MaskedTextBox();
            this.txtDonvitinh = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkTinhthetichtheochitieu = new System.Windows.Forms.CheckBox();
            this.chkCososanh = new System.Windows.Forms.CheckBox();
            this.txtQuychuan = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtSongaytraKQ = new MaskedTextBox.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNhominphoiBHYT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label15 = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtServiceCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaBHYT = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTenBHYT = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.nmrDongia = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkTrangthai = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPhongthuchien = new Janus.Windows.EditControls.UIComboBox();
            this.txtchidan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDepartment = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbonhombaocao = new System.Windows.Forms.ComboBox();
            this.cboNhomin = new System.Windows.Forms.ComboBox();
            this.chkHaveDetail = new System.Windows.Forms.CheckBox();
            this.chkHighTech = new System.Windows.Forms.CheckBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cboServiceType = new System.Windows.Forms.ComboBox();
            this.txtServiceOrder = new System.Windows.Forms.NumericUpDown();
            this.lblIntOrder = new System.Windows.Forms.Label();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.cmdGhi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(15, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 23);
            this.label7.TabIndex = 103;
            this.label7.Text = "Chỉ dẫn chung:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label7, "Nếu các chi tiết của Dịch vụ này không có chỉ dẫn thì sẽ dùng thông tin chỉ dẫn c" +
        "hung này để in lên phiếu chỉ định hướng dẫn cho Bệnh nhân");
            // 
            // chkIntachphieu
            // 
            this.chkIntachphieu.AutoSize = true;
            this.chkIntachphieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIntachphieu.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkIntachphieu.Location = new System.Drawing.Point(556, 436);
            this.chkIntachphieu.Name = "chkIntachphieu";
            this.chkIntachphieu.Size = new System.Drawing.Size(105, 19);
            this.chkIntachphieu.TabIndex = 138;
            this.chkIntachphieu.TabStop = false;
            this.chkIntachphieu.Text = "In tách phiếu?";
            this.toolTip1.SetToolTip(this.chkIntachphieu, "In tách thành 1 phiếu riêng biệt");
            this.chkIntachphieu.UseVisualStyleBackColor = true;
            this.chkIntachphieu.Visible = false;
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.chkIntachphieu);
            this.grpControl.Controls.Add(this.label18);
            this.grpControl.Controls.Add(this.label17);
            this.grpControl.Controls.Add(this.txttenphieutrakqCDHA);
            this.grpControl.Controls.Add(this.label16);
            this.grpControl.Controls.Add(this.panel1);
            this.grpControl.Controls.Add(this.txtNhominphoiBHYT);
            this.grpControl.Controls.Add(this.label15);
            this.grpControl.Controls.Add(this.lblServiceName);
            this.grpControl.Controls.Add(this.txtServiceName);
            this.grpControl.Controls.Add(this.txtServiceCode);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtMaBHYT);
            this.grpControl.Controls.Add(this.label14);
            this.grpControl.Controls.Add(this.label13);
            this.grpControl.Controls.Add(this.txtTenBHYT);
            this.grpControl.Controls.Add(this.lblMsg);
            this.grpControl.Controls.Add(this.nmrDongia);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.chkTrangthai);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.cboPhongthuchien);
            this.grpControl.Controls.Add(this.txtchidan);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.cboDepartment);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.cbonhombaocao);
            this.grpControl.Controls.Add(this.cboNhomin);
            this.grpControl.Controls.Add(this.chkHaveDetail);
            this.grpControl.Controls.Add(this.chkHighTech);
            this.grpControl.Controls.Add(this.txtDesc);
            this.grpControl.Controls.Add(this.lblDescription);
            this.grpControl.Controls.Add(this.cboServiceType);
            this.grpControl.Controls.Add(this.txtServiceOrder);
            this.grpControl.Controls.Add(this.lblIntOrder);
            this.grpControl.Controls.Add(this.lblServiceType);
            this.grpControl.Controls.Add(this.txtID);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.ImageSize = new System.Drawing.Size(24, 24);
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(784, 495);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "Thông tin dịch vụ";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9F);
            this.label18.Location = new System.Drawing.Point(413, 293);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(363, 23);
            this.label18.TabIndex = 137;
            this.label18.Text = "Tên phiếu trả kết quả CĐHA: KQ Siêu âm, KQ X-Quang,...";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9F);
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(13, 292);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 23);
            this.label17.TabIndex = 136;
            this.label17.Text = "Tên phiếu trả KQ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttenphieutrakqCDHA
            // 
            this.txttenphieutrakqCDHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttenphieutrakqCDHA.Font = new System.Drawing.Font("Arial", 9F);
            this.txttenphieutrakqCDHA.Location = new System.Drawing.Point(137, 294);
            this.txttenphieutrakqCDHA.MaxLength = 100;
            this.txttenphieutrakqCDHA.Name = "txttenphieutrakqCDHA";
            this.txttenphieutrakqCDHA.Size = new System.Drawing.Size(275, 21);
            this.txttenphieutrakqCDHA.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9F);
            this.label16.Location = new System.Drawing.Point(418, 214);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(363, 23);
            this.label16.TabIndex = 134;
            this.label16.Text = "(STT in Loại thanh toán trong bảng kê chi phí KCB, in biên lai)";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkKiemnghiem);
            this.panel1.Controls.Add(this.txtThetichtoithieu);
            this.panel1.Controls.Add(this.txtDonvitinh);
            this.panel1.Controls.Add(this.vbLine1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.chkTinhthetichtheochitieu);
            this.panel1.Controls.Add(this.chkCososanh);
            this.panel1.Controls.Add(this.txtQuychuan);
            this.panel1.Controls.Add(this.txtSongaytraKQ);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Location = new System.Drawing.Point(21, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(28, 10);
            this.panel1.TabIndex = 133;
            // 
            // chkKiemnghiem
            // 
            this.chkKiemnghiem.AutoSize = true;
            this.chkKiemnghiem.Font = new System.Drawing.Font("Arial", 9F);
            this.chkKiemnghiem.Location = new System.Drawing.Point(13, 14);
            this.chkKiemnghiem.Name = "chkKiemnghiem";
            this.chkKiemnghiem.Size = new System.Drawing.Size(312, 19);
            this.chkKiemnghiem.TabIndex = 10;
            this.chkKiemnghiem.Text = "Thông tin kiểm nghiệm(Nếu là dịch vụ kiểm nghiệm)";
            this.chkKiemnghiem.UseVisualStyleBackColor = true;
            this.chkKiemnghiem.Visible = false;
            // 
            // txtThetichtoithieu
            // 
            this.txtThetichtoithieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThetichtoithieu.Enabled = false;
            this.txtThetichtoithieu.Font = new System.Drawing.Font("Arial", 9F);
            this.txtThetichtoithieu.Location = new System.Drawing.Point(142, 40);
            this.txtThetichtoithieu.Masked = MaskedTextBox.Mask.Digit;
            this.txtThetichtoithieu.Name = "txtThetichtoithieu";
            this.txtThetichtoithieu.Size = new System.Drawing.Size(276, 21);
            this.txtThetichtoithieu.TabIndex = 10;
            this.txtThetichtoithieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtThetichtoithieu.Visible = false;
            // 
            // txtDonvitinh
            // 
            this.txtDonvitinh._backcolor = System.Drawing.SystemColors.Control;
            this.txtDonvitinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonvitinh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDonvitinh.AddValues = true;
            this.txtDonvitinh.AllowMultiline = false;
            this.txtDonvitinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDonvitinh.AutoCompleteList")));
            this.txtDonvitinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonvitinh.buildShortcut = false;
            this.txtDonvitinh.CaseSensitive = false;
            this.txtDonvitinh.CompareNoID = true;
            this.txtDonvitinh.DefaultCode = "-1";
            this.txtDonvitinh.DefaultID = "-1";
            this.txtDonvitinh.Drug_ID = null;
            this.txtDonvitinh.Enabled = false;
            this.txtDonvitinh.ExtraWidth = 0;
            this.txtDonvitinh.FillValueAfterSelect = false;
            this.txtDonvitinh.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDonvitinh.LOAI_DANHMUC = "DONVITINH";
            this.txtDonvitinh.Location = new System.Drawing.Point(142, 67);
            this.txtDonvitinh.MaxHeight = -1;
            this.txtDonvitinh.MinTypedCharacters = 2;
            this.txtDonvitinh.MyCode = "-1";
            this.txtDonvitinh.MyID = "-1";
            this.txtDonvitinh.Name = "txtDonvitinh";
            this.txtDonvitinh.RaiseEvent = false;
            this.txtDonvitinh.RaiseEventEnter = false;
            this.txtDonvitinh.RaiseEventEnterWhenEmpty = false;
            this.txtDonvitinh.SelectedIndex = -1;
            this.txtDonvitinh.ShowCodeWithValue = false;
            this.txtDonvitinh.Size = new System.Drawing.Size(276, 21);
            this.txtDonvitinh.splitChar = '@';
            this.txtDonvitinh.splitCharIDAndCode = '#';
            this.txtDonvitinh.TabIndex = 11;
            this.txtDonvitinh.TakeCode = false;
            this.txtDonvitinh.txtMyCode = null;
            this.txtDonvitinh.txtMyCode_Edit = null;
            this.txtDonvitinh.txtMyID = null;
            this.txtDonvitinh.txtMyID_Edit = null;
            this.txtDonvitinh.txtMyName = null;
            this.txtDonvitinh.txtMyName_Edit = null;
            this.txtDonvitinh.txtNext = null;
            this.txtDonvitinh.txtNext1 = null;
            this.txtDonvitinh.Visible = false;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9F);
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(314, 12);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(565, 22);
            this.vbLine1.TabIndex = 114;
            this.vbLine1.TabStop = false;
            this.vbLine1.Visible = false;
            this.vbLine1.YourText = "";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(22, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 23);
            this.label8.TabIndex = 115;
            this.label8.Text = "Thể tích tối thiểu:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.Location = new System.Drawing.Point(22, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 23);
            this.label9.TabIndex = 116;
            this.label9.Text = "Đơn vị chỉ tiêu";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.Location = new System.Drawing.Point(6, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 23);
            this.label10.TabIndex = 117;
            this.label10.Text = "Quy chuẩn so sánh";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // chkTinhthetichtheochitieu
            // 
            this.chkTinhthetichtheochitieu.AutoSize = true;
            this.chkTinhthetichtheochitieu.Enabled = false;
            this.chkTinhthetichtheochitieu.Font = new System.Drawing.Font("Arial", 9F);
            this.chkTinhthetichtheochitieu.Location = new System.Drawing.Point(424, 41);
            this.chkTinhthetichtheochitieu.Name = "chkTinhthetichtheochitieu";
            this.chkTinhthetichtheochitieu.Size = new System.Drawing.Size(168, 19);
            this.chkTinhthetichtheochitieu.TabIndex = 118;
            this.chkTinhthetichtheochitieu.TabStop = false;
            this.chkTinhthetichtheochitieu.Text = "Tính thể tích theo chỉ tiêu?";
            this.chkTinhthetichtheochitieu.UseVisualStyleBackColor = true;
            this.chkTinhthetichtheochitieu.Visible = false;
            // 
            // chkCososanh
            // 
            this.chkCososanh.AutoSize = true;
            this.chkCososanh.Enabled = false;
            this.chkCososanh.Font = new System.Drawing.Font("Arial", 9F);
            this.chkCososanh.Location = new System.Drawing.Point(424, 68);
            this.chkCososanh.Name = "chkCososanh";
            this.chkCososanh.Size = new System.Drawing.Size(216, 19);
            this.chkCososanh.TabIndex = 119;
            this.chkCososanh.TabStop = false;
            this.chkCososanh.Text = "Mặc định mẫu có so sánh chỉ tiêu?";
            this.chkCososanh.UseVisualStyleBackColor = true;
            this.chkCososanh.Visible = false;
            // 
            // txtQuychuan
            // 
            this.txtQuychuan._backcolor = System.Drawing.SystemColors.Control;
            this.txtQuychuan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuychuan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtQuychuan.AddValues = true;
            this.txtQuychuan.AllowMultiline = false;
            this.txtQuychuan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtQuychuan.AutoCompleteList")));
            this.txtQuychuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuychuan.buildShortcut = false;
            this.txtQuychuan.CaseSensitive = false;
            this.txtQuychuan.CompareNoID = true;
            this.txtQuychuan.DefaultCode = "-1";
            this.txtQuychuan.DefaultID = "-1";
            this.txtQuychuan.Drug_ID = null;
            this.txtQuychuan.Enabled = false;
            this.txtQuychuan.ExtraWidth = 0;
            this.txtQuychuan.FillValueAfterSelect = false;
            this.txtQuychuan.Font = new System.Drawing.Font("Arial", 9F);
            this.txtQuychuan.LOAI_DANHMUC = "QUYCHUAN";
            this.txtQuychuan.Location = new System.Drawing.Point(142, 94);
            this.txtQuychuan.MaxHeight = -1;
            this.txtQuychuan.MinTypedCharacters = 2;
            this.txtQuychuan.MyCode = "-1";
            this.txtQuychuan.MyID = "-1";
            this.txtQuychuan.Name = "txtQuychuan";
            this.txtQuychuan.RaiseEvent = false;
            this.txtQuychuan.RaiseEventEnter = false;
            this.txtQuychuan.RaiseEventEnterWhenEmpty = false;
            this.txtQuychuan.SelectedIndex = -1;
            this.txtQuychuan.ShowCodeWithValue = false;
            this.txtQuychuan.Size = new System.Drawing.Size(276, 21);
            this.txtQuychuan.splitChar = '@';
            this.txtQuychuan.splitCharIDAndCode = '#';
            this.txtQuychuan.TabIndex = 12;
            this.txtQuychuan.TakeCode = false;
            this.txtQuychuan.txtMyCode = null;
            this.txtQuychuan.txtMyCode_Edit = null;
            this.txtQuychuan.txtMyID = null;
            this.txtQuychuan.txtMyID_Edit = null;
            this.txtQuychuan.txtMyName = null;
            this.txtQuychuan.txtMyName_Edit = null;
            this.txtQuychuan.txtNext = null;
            this.txtQuychuan.txtNext1 = null;
            this.txtQuychuan.Visible = false;
            // 
            // txtSongaytraKQ
            // 
            this.txtSongaytraKQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSongaytraKQ.Enabled = false;
            this.txtSongaytraKQ.Font = new System.Drawing.Font("Arial", 9F);
            this.txtSongaytraKQ.Location = new System.Drawing.Point(142, 121);
            this.txtSongaytraKQ.Masked = MaskedTextBox.Mask.Digit;
            this.txtSongaytraKQ.Name = "txtSongaytraKQ";
            this.txtSongaytraKQ.Size = new System.Drawing.Size(276, 21);
            this.txtSongaytraKQ.TabIndex = 13;
            this.txtSongaytraKQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSongaytraKQ.Visible = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(23, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 23);
            this.label11.TabIndex = 122;
            this.label11.Text = "Số ngày phải trả KQ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.Location = new System.Drawing.Point(424, 121);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(188, 23);
            this.label12.TabIndex = 123;
            this.label12.Text = "(Không nhập gì ứng với 7 ngày)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Visible = false;
            // 
            // txtNhominphoiBHYT
            // 
            this.txtNhominphoiBHYT._backcolor = System.Drawing.SystemColors.Control;
            this.txtNhominphoiBHYT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhominphoiBHYT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhominphoiBHYT.AddValues = true;
            this.txtNhominphoiBHYT.AllowMultiline = false;
            this.txtNhominphoiBHYT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhominphoiBHYT.AutoCompleteList")));
            this.txtNhominphoiBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhominphoiBHYT.buildShortcut = false;
            this.txtNhominphoiBHYT.CaseSensitive = false;
            this.txtNhominphoiBHYT.CompareNoID = true;
            this.txtNhominphoiBHYT.DefaultCode = "-1";
            this.txtNhominphoiBHYT.DefaultID = "-1";
            this.txtNhominphoiBHYT.Drug_ID = null;
            this.txtNhominphoiBHYT.ExtraWidth = 0;
            this.txtNhominphoiBHYT.FillValueAfterSelect = false;
            this.txtNhominphoiBHYT.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNhominphoiBHYT.LOAI_DANHMUC = "NHOMINPHOIBHYT";
            this.txtNhominphoiBHYT.Location = new System.Drawing.Point(136, 216);
            this.txtNhominphoiBHYT.MaxHeight = -1;
            this.txtNhominphoiBHYT.MinTypedCharacters = 2;
            this.txtNhominphoiBHYT.MyCode = "-1";
            this.txtNhominphoiBHYT.MyID = "-1";
            this.txtNhominphoiBHYT.Name = "txtNhominphoiBHYT";
            this.txtNhominphoiBHYT.RaiseEvent = false;
            this.txtNhominphoiBHYT.RaiseEventEnter = false;
            this.txtNhominphoiBHYT.RaiseEventEnterWhenEmpty = false;
            this.txtNhominphoiBHYT.SelectedIndex = -1;
            this.txtNhominphoiBHYT.ShowCodeWithValue = false;
            this.txtNhominphoiBHYT.Size = new System.Drawing.Size(276, 21);
            this.txtNhominphoiBHYT.splitChar = '@';
            this.txtNhominphoiBHYT.splitCharIDAndCode = '#';
            this.txtNhominphoiBHYT.TabIndex = 6;
            this.txtNhominphoiBHYT.Tag = "";
            this.txtNhominphoiBHYT.TakeCode = false;
            this.txtNhominphoiBHYT.txtMyCode = null;
            this.txtNhominphoiBHYT.txtMyCode_Edit = null;
            this.txtNhominphoiBHYT.txtMyID = null;
            this.txtNhominphoiBHYT.txtMyID_Edit = null;
            this.txtNhominphoiBHYT.txtMyName = null;
            this.txtNhominphoiBHYT.txtMyName_Edit = null;
            this.txtNhominphoiBHYT.txtNext = null;
            this.txtNhominphoiBHYT.txtNext1 = null;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(6, 209);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 35);
            this.label15.TabIndex = 132;
            this.label15.Tag = "";
            this.label15.Text = "Nhóm in bảng kê chi phí KCB, Biên lai";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblServiceName
            // 
            this.lblServiceName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblServiceName.ForeColor = System.Drawing.Color.Red;
            this.lblServiceName.Location = new System.Drawing.Point(15, 52);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(116, 23);
            this.lblServiceName.TabIndex = 131;
            this.lblServiceName.Text = "Tên Dịch Vụ";
            this.lblServiceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServiceName
            // 
            this.txtServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceName.Font = new System.Drawing.Font("Arial", 9F);
            this.txtServiceName.Location = new System.Drawing.Point(136, 50);
            this.txtServiceName.MaxLength = 255;
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(638, 21);
            this.txtServiceName.TabIndex = 2;
            // 
            // txtServiceCode
            // 
            this.txtServiceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceCode.Font = new System.Drawing.Font("Arial", 9F);
            this.txtServiceCode.Location = new System.Drawing.Point(136, 24);
            this.txtServiceCode.Name = "txtServiceCode";
            this.txtServiceCode.Size = new System.Drawing.Size(122, 21);
            this.txtServiceCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 129;
            this.label1.Text = "Mã dịch vụ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaBHYT
            // 
            this.txtMaBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaBHYT.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMaBHYT.Location = new System.Drawing.Point(335, 23);
            this.txtMaBHYT.Name = "txtMaBHYT";
            this.txtMaBHYT.Size = new System.Drawing.Size(77, 21);
            this.txtMaBHYT.TabIndex = 0;
            this.txtMaBHYT.Tag = "BHYT";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(264, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 23);
            this.label14.TabIndex = 127;
            this.label14.Tag = "BHYT";
            this.label14.Text = "Mã QĐ 29";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(15, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 23);
            this.label13.TabIndex = 125;
            this.label13.Tag = "BHYT";
            this.label13.Text = "Tên QĐ 29";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenBHYT
            // 
            this.txtTenBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenBHYT.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenBHYT.Location = new System.Drawing.Point(136, 77);
            this.txtTenBHYT.MaxLength = 255;
            this.txtTenBHYT.Name = "txtTenBHYT";
            this.txtTenBHYT.Size = new System.Drawing.Size(638, 21);
            this.txtTenBHYT.TabIndex = 2;
            this.txtTenBHYT.Tag = "BHYT";
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 469);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(778, 23);
            this.lblMsg.TabIndex = 110;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmrDongia
            // 
            this.nmrDongia.Font = new System.Drawing.Font("Arial", 9F);
            this.nmrDongia.Location = new System.Drawing.Point(137, 131);
            this.nmrDongia.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nmrDongia.Name = "nmrDongia";
            this.nmrDongia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nmrDongia.Size = new System.Drawing.Size(275, 21);
            this.nmrDongia.TabIndex = 4;
            this.nmrDongia.ThousandsSeparator = true;
            this.nmrDongia.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nmrDongia.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(15, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 23);
            this.label5.TabIndex = 109;
            this.label5.Text = "Đơn giá:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.AutoSize = true;
            this.chkTrangthai.Checked = true;
            this.chkTrangthai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangthai.Font = new System.Drawing.Font("Arial", 9F);
            this.chkTrangthai.Location = new System.Drawing.Point(136, 436);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(88, 19);
            this.chkTrangthai.TabIndex = 15;
            this.chkTrangthai.TabStop = false;
            this.chkTrangthai.Text = "Trạng thái?";
            this.chkTrangthai.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(15, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 23);
            this.label3.TabIndex = 106;
            this.label3.Text = "Phòng thực hiện";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPhongthuchien
            // 
            this.cboPhongthuchien.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboPhongthuchien.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPhongthuchien.Font = new System.Drawing.Font("Arial", 9F);
            this.cboPhongthuchien.Location = new System.Drawing.Point(136, 269);
            this.cboPhongthuchien.Name = "cboPhongthuchien";
            this.cboPhongthuchien.Size = new System.Drawing.Size(640, 21);
            this.cboPhongthuchien.TabIndex = 8;
            this.cboPhongthuchien.Text = "Khoa thực hiện";
            // 
            // txtchidan
            // 
            this.txtchidan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchidan.Font = new System.Drawing.Font("Arial", 9F);
            this.txtchidan.Location = new System.Drawing.Point(136, 322);
            this.txtchidan.Multiline = true;
            this.txtchidan.Name = "txtchidan";
            this.txtchidan.Size = new System.Drawing.Size(639, 34);
            this.txtchidan.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(15, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 23);
            this.label6.TabIndex = 102;
            this.label6.Text = "Nhóm in phiếu";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(15, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 23);
            this.label4.TabIndex = 100;
            this.label4.Text = "Khoa thực hiện";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDepartment
            // 
            this.cboDepartment.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboDepartment.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Arial", 9F);
            this.cboDepartment.Location = new System.Drawing.Point(137, 242);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(639, 21);
            this.cboDepartment.TabIndex = 7;
            this.cboDepartment.Text = "Khoa thực hiện";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(15, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 98;
            this.label2.Text = "Nhóm báo cáo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbonhombaocao
            // 
            this.cbonhombaocao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbonhombaocao.Font = new System.Drawing.Font("Arial", 9F);
            this.cbonhombaocao.FormattingEnabled = true;
            this.cbonhombaocao.Location = new System.Drawing.Point(136, 187);
            this.cbonhombaocao.Name = "cbonhombaocao";
            this.cbonhombaocao.Size = new System.Drawing.Size(639, 23);
            this.cbonhombaocao.TabIndex = 6;
            // 
            // cboNhomin
            // 
            this.cboNhomin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomin.Font = new System.Drawing.Font("Arial", 9F);
            this.cboNhomin.FormattingEnabled = true;
            this.cboNhomin.Items.AddRange(new object[] {
            "Phiếu xét nghiệm",
            "Phiếu X Quang",
            "Phiếu siêu âm",
            "Phiếu điện tim",
            "Phiếu nội soi",
            "Phiếu điện não đồ"});
            this.cboNhomin.Location = new System.Drawing.Point(136, 158);
            this.cboNhomin.Name = "cboNhomin";
            this.cboNhomin.Size = new System.Drawing.Size(638, 23);
            this.cboNhomin.TabIndex = 5;
            // 
            // chkHaveDetail
            // 
            this.chkHaveDetail.AutoSize = true;
            this.chkHaveDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.chkHaveDetail.Location = new System.Drawing.Point(426, 435);
            this.chkHaveDetail.Name = "chkHaveDetail";
            this.chkHaveDetail.Size = new System.Drawing.Size(113, 19);
            this.chkHaveDetail.TabIndex = 16;
            this.chkHaveDetail.TabStop = false;
            this.chkHaveDetail.Text = "Hiển thị chi tiết?";
            this.chkHaveDetail.UseVisualStyleBackColor = true;
            this.chkHaveDetail.Visible = false;
            // 
            // chkHighTech
            // 
            this.chkHighTech.AutoSize = true;
            this.chkHighTech.Font = new System.Drawing.Font("Arial", 9F);
            this.chkHighTech.ForeColor = System.Drawing.Color.Navy;
            this.chkHighTech.Location = new System.Drawing.Point(245, 435);
            this.chkHighTech.Name = "chkHighTech";
            this.chkHighTech.Size = new System.Drawing.Size(155, 19);
            this.chkHighTech.TabIndex = 17;
            this.chkHighTech.TabStop = false;
            this.chkHighTech.Text = "Là dịch vụ kỹ thuật cao?";
            this.chkHighTech.UseVisualStyleBackColor = true;
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDesc.Location = new System.Drawing.Point(136, 362);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(639, 62);
            this.txtDesc.TabIndex = 14;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDescription.Location = new System.Drawing.Point(15, 362);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(116, 23);
            this.lblDescription.TabIndex = 91;
            this.lblDescription.Text = "Ghi chú";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboServiceType
            // 
            this.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceType.Font = new System.Drawing.Font("Arial", 9F);
            this.cboServiceType.FormattingEnabled = true;
            this.cboServiceType.Location = new System.Drawing.Point(531, 22);
            this.cboServiceType.Name = "cboServiceType";
            this.cboServiceType.Size = new System.Drawing.Size(244, 23);
            this.cboServiceType.TabIndex = 1;
            // 
            // txtServiceOrder
            // 
            this.txtServiceOrder.Font = new System.Drawing.Font("Arial", 9F);
            this.txtServiceOrder.Location = new System.Drawing.Point(137, 104);
            this.txtServiceOrder.Name = "txtServiceOrder";
            this.txtServiceOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtServiceOrder.Size = new System.Drawing.Size(275, 21);
            this.txtServiceOrder.TabIndex = 3;
            this.txtServiceOrder.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lblIntOrder
            // 
            this.lblIntOrder.Font = new System.Drawing.Font("Arial", 9F);
            this.lblIntOrder.Location = new System.Drawing.Point(15, 107);
            this.lblIntOrder.Name = "lblIntOrder";
            this.lblIntOrder.Size = new System.Drawing.Size(116, 23);
            this.lblIntOrder.TabIndex = 89;
            this.lblIntOrder.Text = "STT hiển thị:";
            this.lblIntOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblServiceType
            // 
            this.lblServiceType.Font = new System.Drawing.Font("Arial", 9F);
            this.lblServiceType.ForeColor = System.Drawing.Color.Red;
            this.lblServiceType.Location = new System.Drawing.Point(423, 22);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(97, 23);
            this.lblServiceType.TabIndex = 88;
            this.lblServiceType.Text = "Nhóm Dịch Vụ";
            this.lblServiceType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9F);
            this.txtID.Location = new System.Drawing.Point(137, 24);
            this.txtID.MaxLength = 1;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(79, 21);
            this.txtID.TabIndex = 94;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdThoat.Location = new System.Drawing.Point(656, 514);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(120, 35);
            this.cmdThoat.TabIndex = 19;
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdThoat.UseVisualStyleBackColor = true;
            // 
            // cmdGhi
            // 
            this.cmdGhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdGhi.Location = new System.Drawing.Point(518, 514);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(117, 35);
            this.cmdGhi.TabIndex = 18;
            this.cmdGhi.Text = "Lưu(Ctrl+S)";
            this.cmdGhi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdGhi.UseVisualStyleBackColor = true;
            this.cmdGhi.Click += new System.EventHandler(this.cmdGhi_Click_1);
            // 
            // frm_themmoi_dichvucls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdGhi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_dichvucls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THÔNG TIN DỊCH VỤ";
            this.Load += new System.EventHandler(this.frm_themmoi_dichvucls_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_dichvucls_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frm_themmoi_dichvucls_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.ComboBox cboNhomin;
        private System.Windows.Forms.CheckBox chkHaveDetail;
        private System.Windows.Forms.CheckBox chkHighTech;
        internal System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.NumericUpDown txtServiceOrder;
        private System.Windows.Forms.Label lblIntOrder;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Button cmdGhi;
        private System.Windows.Forms.ComboBox cbonhombaocao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboDepartment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtchidan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboPhongthuchien;
        private System.Windows.Forms.CheckBox chkTrangthai;
        private System.Windows.Forms.NumericUpDown nmrDongia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ToolTip toolTip1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtDonvitinh;
        private MaskedTextBox.MaskedTextBox txtThetichtoithieu;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private MaskedTextBox.MaskedTextBox txtSongaytraKQ;
        private UCs.AutoCompleteTextbox_Danhmucchung txtQuychuan;
        private System.Windows.Forms.CheckBox chkCososanh;
        private System.Windows.Forms.CheckBox chkTinhthetichtheochitieu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.CheckBox chkKiemnghiem;
        private System.Windows.Forms.TextBox txtMaBHYT;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTenBHYT;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.TextBox txtServiceCode;
        private System.Windows.Forms.Label label1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNhominphoiBHYT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txttenphieutrakqCDHA;
        private System.Windows.Forms.CheckBox chkIntachphieu;
    }
}