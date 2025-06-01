using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_TongKetBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TongKetBenhAn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtTinhTrangHienTai = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtCanLamSang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtquatrinhbenhly = new Janus.Windows.GridEX.EditControls.EditBox();
            this.autoLydovv = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPPdieutri = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.ucThongtinnguoibenh_v21 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v2();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtNgayRaVien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSoNgayDtri = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdXoa = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdThemmoi = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.txtTinhTrangHienTai);
            this.panel1.Controls.Add(this.txtCanLamSang);
            this.panel1.Controls.Add(this.txtquatrinhbenhly);
            this.panel1.Controls.Add(this.autoLydovv);
            this.panel1.Controls.Add(this.txtPPdieutri);
            this.panel1.Controls.Add(this.autoKhoa);
            this.panel1.Controls.Add(this.ucThongtinnguoibenh_v21);
            this.panel1.Controls.Add(this.dtDenNgay);
            this.panel1.Controls.Add(this.dtTuNgay);
            this.panel1.Controls.Add(this.dtNgayRaVien);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtSoNgayDtri);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtChanDoan);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 668);
            this.panel1.TabIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(801, 186);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(21, 20);
            this.txtID.TabIndex = 34;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // txtTinhTrangHienTai
            // 
            this.txtTinhTrangHienTai._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTinhTrangHienTai._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhTrangHienTai._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTinhTrangHienTai.AddValues = true;
            this.txtTinhTrangHienTai.AllowMultiline = false;
            this.txtTinhTrangHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinhTrangHienTai.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhTrangHienTai.AutoCompleteList")));
            this.txtTinhTrangHienTai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTinhTrangHienTai.buildShortcut = false;
            this.txtTinhTrangHienTai.CaseSensitive = false;
            this.txtTinhTrangHienTai.CompareNoID = true;
            this.txtTinhTrangHienTai.DefaultCode = "-1";
            this.txtTinhTrangHienTai.DefaultID = "-1";
            this.txtTinhTrangHienTai.Drug_ID = null;
            this.txtTinhTrangHienTai.ExtraWidth = 0;
            this.txtTinhTrangHienTai.FillValueAfterSelect = false;
            this.txtTinhTrangHienTai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhTrangHienTai.LOAI_DANHMUC = "TINHTRANGNGUOIBENH";
            this.txtTinhTrangHienTai.Location = new System.Drawing.Point(133, 470);
            this.txtTinhTrangHienTai.MaxHeight = 150;
            this.txtTinhTrangHienTai.MaxLength = 255;
            this.txtTinhTrangHienTai.MinTypedCharacters = 2;
            this.txtTinhTrangHienTai.MyCode = "-1";
            this.txtTinhTrangHienTai.MyID = "-1";
            this.txtTinhTrangHienTai.Name = "txtTinhTrangHienTai";
            this.txtTinhTrangHienTai.RaiseEvent = false;
            this.txtTinhTrangHienTai.RaiseEventEnter = false;
            this.txtTinhTrangHienTai.RaiseEventEnterWhenEmpty = false;
            this.txtTinhTrangHienTai.SelectedIndex = -1;
            this.txtTinhTrangHienTai.ShowCodeWithValue = false;
            this.txtTinhTrangHienTai.Size = new System.Drawing.Size(852, 21);
            this.txtTinhTrangHienTai.splitChar = '@';
            this.txtTinhTrangHienTai.splitCharIDAndCode = '#';
            this.txtTinhTrangHienTai.TabIndex = 9;
            this.txtTinhTrangHienTai.TakeCode = false;
            this.txtTinhTrangHienTai.txtMyCode = null;
            this.txtTinhTrangHienTai.txtMyCode_Edit = null;
            this.txtTinhTrangHienTai.txtMyID = null;
            this.txtTinhTrangHienTai.txtMyID_Edit = null;
            this.txtTinhTrangHienTai.txtMyName = null;
            this.txtTinhTrangHienTai.txtMyName_Edit = null;
            this.txtTinhTrangHienTai.txtNext = null;
            this.txtTinhTrangHienTai.txtNext1 = null;
            // 
            // txtCanLamSang
            // 
            this.txtCanLamSang.Location = new System.Drawing.Point(133, 375);
            this.txtCanLamSang.Multiline = true;
            this.txtCanLamSang.Name = "txtCanLamSang";
            this.txtCanLamSang.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCanLamSang.Size = new System.Drawing.Size(852, 88);
            this.txtCanLamSang.TabIndex = 8;
            // 
            // txtquatrinhbenhly
            // 
            this.txtquatrinhbenhly.Location = new System.Drawing.Point(133, 275);
            this.txtquatrinhbenhly.Multiline = true;
            this.txtquatrinhbenhly.Name = "txtquatrinhbenhly";
            this.txtquatrinhbenhly.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtquatrinhbenhly.Size = new System.Drawing.Size(852, 88);
            this.txtquatrinhbenhly.TabIndex = 7;
            // 
            // autoLydovv
            // 
            this.autoLydovv._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLydovv._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydovv._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLydovv.AddValues = true;
            this.autoLydovv.AllowMultiline = false;
            this.autoLydovv.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLydovv.AutoCompleteList")));
            this.autoLydovv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLydovv.buildShortcut = false;
            this.autoLydovv.CaseSensitive = false;
            this.autoLydovv.CompareNoID = true;
            this.autoLydovv.DefaultCode = "-1";
            this.autoLydovv.DefaultID = "-1";
            this.autoLydovv.Drug_ID = null;
            this.autoLydovv.ExtraWidth = 0;
            this.autoLydovv.FillValueAfterSelect = false;
            this.autoLydovv.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydovv.LOAI_DANHMUC = "LYDOVAOVIEN";
            this.autoLydovv.Location = new System.Drawing.Point(133, 139);
            this.autoLydovv.MaxHeight = -1;
            this.autoLydovv.MinTypedCharacters = 2;
            this.autoLydovv.MyCode = "-1";
            this.autoLydovv.MyID = "-1";
            this.autoLydovv.Name = "autoLydovv";
            this.autoLydovv.RaiseEvent = false;
            this.autoLydovv.RaiseEventEnter = false;
            this.autoLydovv.RaiseEventEnterWhenEmpty = false;
            this.autoLydovv.SelectedIndex = -1;
            this.autoLydovv.ShowCodeWithValue = false;
            this.autoLydovv.Size = new System.Drawing.Size(852, 21);
            this.autoLydovv.splitChar = '@';
            this.autoLydovv.splitCharIDAndCode = '#';
            this.autoLydovv.TabIndex = 0;
            this.autoLydovv.TakeCode = false;
            this.autoLydovv.txtMyCode = null;
            this.autoLydovv.txtMyCode_Edit = null;
            this.autoLydovv.txtMyID = null;
            this.autoLydovv.txtMyID_Edit = null;
            this.autoLydovv.txtMyName = null;
            this.autoLydovv.txtMyName_Edit = null;
            this.autoLydovv.txtNext = null;
            this.autoLydovv.txtNext1 = null;
            // 
            // txtPPdieutri
            // 
            this.txtPPdieutri._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPPdieutri._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPdieutri._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPPdieutri.AddValues = true;
            this.txtPPdieutri.AllowMultiline = false;
            this.txtPPdieutri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPPdieutri.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPPdieutri.AutoCompleteList")));
            this.txtPPdieutri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPPdieutri.buildShortcut = false;
            this.txtPPdieutri.CaseSensitive = false;
            this.txtPPdieutri.CompareNoID = true;
            this.txtPPdieutri.DefaultCode = "-1";
            this.txtPPdieutri.DefaultID = "-1";
            this.txtPPdieutri.Drug_ID = null;
            this.txtPPdieutri.ExtraWidth = 0;
            this.txtPPdieutri.FillValueAfterSelect = false;
            this.txtPPdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPdieutri.LOAI_DANHMUC = "PHUONGPHAPDIEUTRI";
            this.txtPPdieutri.Location = new System.Drawing.Point(133, 248);
            this.txtPPdieutri.MaxHeight = 150;
            this.txtPPdieutri.MaxLength = 255;
            this.txtPPdieutri.MinTypedCharacters = 2;
            this.txtPPdieutri.MyCode = "-1";
            this.txtPPdieutri.MyID = "-1";
            this.txtPPdieutri.Name = "txtPPdieutri";
            this.txtPPdieutri.RaiseEvent = false;
            this.txtPPdieutri.RaiseEventEnter = false;
            this.txtPPdieutri.RaiseEventEnterWhenEmpty = false;
            this.txtPPdieutri.SelectedIndex = -1;
            this.txtPPdieutri.ShowCodeWithValue = false;
            this.txtPPdieutri.Size = new System.Drawing.Size(852, 21);
            this.txtPPdieutri.splitChar = '@';
            this.txtPPdieutri.splitCharIDAndCode = '#';
            this.txtPPdieutri.TabIndex = 6;
            this.txtPPdieutri.TakeCode = false;
            this.txtPPdieutri.txtMyCode = null;
            this.txtPPdieutri.txtMyCode_Edit = null;
            this.txtPPdieutri.txtMyID = null;
            this.txtPPdieutri.txtMyID_Edit = null;
            this.txtPPdieutri.txtMyName = null;
            this.txtPPdieutri.txtMyName_Edit = null;
            this.txtPPdieutri.txtNext = null;
            this.txtPPdieutri.txtNext1 = null;
            // 
            // autoKhoa
            // 
            this.autoKhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoKhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoKhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoKhoa.AutoCompleteList")));
            this.autoKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoKhoa.buildShortcut = false;
            this.autoKhoa.CaseSensitive = false;
            this.autoKhoa.CompareNoID = true;
            this.autoKhoa.DefaultCode = "-1";
            this.autoKhoa.DefaultID = "-1";
            this.autoKhoa.DisplayType = 0;
            this.autoKhoa.Drug_ID = null;
            this.autoKhoa.ExtraWidth = 0;
            this.autoKhoa.FillValueAfterSelect = false;
            this.autoKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa.Location = new System.Drawing.Point(133, 190);
            this.autoKhoa.MaxHeight = 289;
            this.autoKhoa.MinTypedCharacters = 2;
            this.autoKhoa.MyCode = "-1";
            this.autoKhoa.MyID = "-1";
            this.autoKhoa.MyText = "";
            this.autoKhoa.MyTextOnly = "";
            this.autoKhoa.Name = "autoKhoa";
            this.autoKhoa.RaiseEvent = true;
            this.autoKhoa.RaiseEventEnter = true;
            this.autoKhoa.RaiseEventEnterWhenEmpty = true;
            this.autoKhoa.SelectedIndex = -1;
            this.autoKhoa.Size = new System.Drawing.Size(361, 21);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 2;
            this.autoKhoa.TakeCode = false;
            this.autoKhoa.txtMyCode = null;
            this.autoKhoa.txtMyCode_Edit = null;
            this.autoKhoa.txtMyID = null;
            this.autoKhoa.txtMyID_Edit = null;
            this.autoKhoa.txtMyName = null;
            this.autoKhoa.txtMyName_Edit = null;
            this.autoKhoa.txtNext = null;
            // 
            // ucThongtinnguoibenh_v21
            // 
            this.ucThongtinnguoibenh_v21.Location = new System.Drawing.Point(22, 3);
            this.ucThongtinnguoibenh_v21.Name = "ucThongtinnguoibenh_v21";
            this.ucThongtinnguoibenh_v21.Size = new System.Drawing.Size(974, 129);
            this.ucThongtinnguoibenh_v21.TabIndex = 22;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.Location = new System.Drawing.Point(394, 224);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(165, 20);
            this.dtDenNgay.TabIndex = 5;
            this.dtDenNgay.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.Location = new System.Drawing.Point(133, 224);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(165, 20);
            this.dtTuNgay.TabIndex = 4;
            this.dtTuNgay.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // dtNgayRaVien
            // 
            this.dtNgayRaVien.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtNgayRaVien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayRaVien.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtNgayRaVien.DropDownCalendar.Name = "";
            this.dtNgayRaVien.Location = new System.Drawing.Point(133, 513);
            this.dtNgayRaVien.Name = "dtNgayRaVien";
            this.dtNgayRaVien.ShowUpDown = true;
            this.dtNgayRaVien.Size = new System.Drawing.Size(232, 20);
            this.dtNgayRaVien.TabIndex = 10;
            this.dtNgayRaVien.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9F);
            this.label16.Location = new System.Drawing.Point(14, 491);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 64);
            this.label16.TabIndex = 20;
            this.label16.Text = "Bệnh nhân ra viện/xin ra viện/chuyển viện:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(6, 252);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 15);
            this.label14.TabIndex = 16;
            this.label14.Text = "Phương pháp điều trị";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSoNgayDtri
            // 
            this.txtSoNgayDtri.Location = new System.Drawing.Point(623, 191);
            this.txtSoNgayDtri.Name = "txtSoNgayDtri";
            this.txtSoNgayDtri.Size = new System.Drawing.Size(92, 20);
            this.txtSoNgayDtri.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.Location = new System.Drawing.Point(526, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 15);
            this.label13.TabIndex = 14;
            this.label13.Text = "Số ngày điều trị";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(328, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "đến ngày";
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoan.Location = new System.Drawing.Point(133, 163);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(852, 20);
            this.txtChanDoan.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(51, 375);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Tóm tắt CLS:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(30, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Quá trình bệnh lý";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(24, 472);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tình trạng hiện tại";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(78, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Từ ngày";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(37, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Điều trị tại khoa";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(30, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Được chẩn đoán";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(29, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vào viện với lý do";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkPreview);
            this.panel3.Controls.Add(this.cmdIn);
            this.panel3.Controls.Add(this.cmdXoa);
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Controls.Add(this.cmdThemmoi);
            this.panel3.Controls.Add(this.cmdSave);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 668);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 61);
            this.panel3.TabIndex = 1;
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.AutoSize = true;
            this.chkPreview.BackColor = System.Drawing.Color.Transparent;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(133, 30);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(108, 17);
            this.chkPreview.TabIndex = 581;
            this.chkPreview.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkPreview.Text = "Xem trước khi in?";
            this.chkPreview.UseVisualStyleBackColor = false;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(489, 16);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(120, 33);
            this.cmdIn.TabIndex = 12;
            this.cmdIn.TabStop = false;
            this.cmdIn.Text = "In";
            this.cmdIn.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdIn.Click += new System.EventHandler(this.cmdIn_Click);
            // 
            // cmdXoa
            // 
            this.cmdXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdXoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdXoa.Location = new System.Drawing.Point(615, 16);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(120, 33);
            this.cmdXoa.TabIndex = 13;
            this.cmdXoa.TabStop = false;
            this.cmdXoa.Text = "Xóa";
            this.cmdXoa.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(865, 16);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 33);
            this.cmdExit.TabIndex = 15;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click_1);
            // 
            // cmdThemmoi
            // 
            this.cmdThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemmoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemmoi.Image")));
            this.cmdThemmoi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdThemmoi.Location = new System.Drawing.Point(363, 16);
            this.cmdThemmoi.Name = "cmdThemmoi";
            this.cmdThemmoi.Size = new System.Drawing.Size(120, 33);
            this.cmdThemmoi.TabIndex = 14;
            this.cmdThemmoi.TabStop = false;
            this.cmdThemmoi.Text = "Thêm mới";
            this.cmdThemmoi.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdThemmoi.Click += new System.EventHandler(this.cmdThemmoi_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(740, 16);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 33);
            this.cmdSave.TabIndex = 11;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // frm_TongKetBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TongKetBenhAn";
            this.ShowIcon = false;
            this.Text = "Tổng kết bệnh án";
            this.Load += new System.EventHandler(this.frm_TongKetBenhAn_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtChanDoan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSoNgayDtri;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayRaVien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private AutoCompleteTextbox autoKhoa;
        public AutoCompleteTextbox_Danhmucchung txtPPdieutri;
        private AutoCompleteTextbox_Danhmucchung autoLydovv;
        public AutoCompleteTextbox_Danhmucchung txtTinhTrangHienTai;
        private Janus.Windows.GridEX.EditControls.EditBox txtCanLamSang;
        private Janus.Windows.GridEX.EditControls.EditBox txtquatrinhbenhly;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdXoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdThemmoi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.TextBox txtID;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_v2 ucThongtinnguoibenh_v21;
        private System.Windows.Forms.CheckBox chkPreview;
    }
}