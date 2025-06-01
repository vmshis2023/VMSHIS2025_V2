using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_TomtatBA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TomtatBA));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtNgayTTBA = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDauhieulamsang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTiensubenh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txtCDRavien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTinhtrangRavien = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtTomtatCLS = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtquatrinhbenhly = new Janus.Windows.GridEX.EditControls.EditBox();
            this.autoLydovv = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPPdieutri = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.ucThongtinnguoibenh_v21 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v2();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCDnhapvien = new System.Windows.Forms.TextBox();
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
            this.chkNoikhoa = new System.Windows.Forms.CheckBox();
            this.txtNoikhoamota = new System.Windows.Forms.TextBox();
            this.txtPTTTmota = new System.Windows.Forms.TextBox();
            this.chkPTTT = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHuongdieutri = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtHuongdieutri);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.chkPTTT);
            this.panel1.Controls.Add(this.txtPTTTmota);
            this.panel1.Controls.Add(this.txtNoikhoamota);
            this.panel1.Controls.Add(this.chkNoikhoa);
            this.panel1.Controls.Add(this.dtNgayTTBA);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtDauhieulamsang);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtTiensubenh);
            this.panel1.Controls.Add(this.label54);
            this.panel1.Controls.Add(this.txtCDRavien);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTinhtrangRavien);
            this.panel1.Controls.Add(this.txtTomtatCLS);
            this.panel1.Controls.Add(this.txtquatrinhbenhly);
            this.panel1.Controls.Add(this.autoLydovv);
            this.panel1.Controls.Add(this.txtPPdieutri);
            this.panel1.Controls.Add(this.autoKhoa);
            this.panel1.Controls.Add(this.ucThongtinnguoibenh_v21);
            this.panel1.Controls.Add(this.dtDenNgay);
            this.panel1.Controls.Add(this.dtTuNgay);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtCDnhapvien);
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
            this.panel1.Size = new System.Drawing.Size(1238, 734);
            this.panel1.TabIndex = 0;
            // 
            // dtNgayTTBA
            // 
            this.dtNgayTTBA.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtNgayTTBA.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayTTBA.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtNgayTTBA.DropDownCalendar.Name = "";
            this.dtNgayTTBA.Enabled = false;
            this.dtNgayTTBA.Location = new System.Drawing.Point(640, 210);
            this.dtNgayTTBA.Name = "dtNgayTTBA";
            this.dtNgayTTBA.ShowUpDown = true;
            this.dtNgayTTBA.Size = new System.Drawing.Size(142, 20);
            this.dtNgayTTBA.TabIndex = 477;
            this.dtNgayTTBA.TabStop = false;
            this.dtNgayTTBA.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(528, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 19);
            this.label11.TabIndex = 476;
            this.label11.Text = "Ngày tạo TTBA:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDauhieulamsang
            // 
            this.txtDauhieulamsang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDauhieulamsang.Location = new System.Drawing.Point(156, 419);
            this.txtDauhieulamsang.Multiline = true;
            this.txtDauhieulamsang.Name = "txtDauhieulamsang";
            this.txtDauhieulamsang.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDauhieulamsang.Size = new System.Drawing.Size(1050, 72);
            this.txtDauhieulamsang.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(0, 414);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(153, 76);
            this.label9.TabIndex = 475;
            this.label9.Text = "Những dấu hiệu lâm sàng chính được ghi nhận (có giá trị chẩn đoán trong quá trình" +
    " điều trị)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTiensubenh
            // 
            this.txtTiensubenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTiensubenh.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtTiensubenh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTiensubenh.Location = new System.Drawing.Point(156, 359);
            this.txtTiensubenh.MaxLength = 1000;
            this.txtTiensubenh.Multiline = true;
            this.txtTiensubenh.Name = "txtTiensubenh";
            this.txtTiensubenh.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTiensubenh.Size = new System.Drawing.Size(1050, 57);
            this.txtTiensubenh.TabIndex = 6;
            this.txtTiensubenh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            this.txtTiensubenh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(0, 359);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(153, 57);
            this.label54.TabIndex = 474;
            this.label54.Text = "Tiền sử bệnh:";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCDRavien
            // 
            this.txtCDRavien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCDRavien.Enabled = false;
            this.txtCDRavien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCDRavien.Location = new System.Drawing.Point(156, 187);
            this.txtCDRavien.Name = "txtCDRavien";
            this.txtCDRavien.Size = new System.Drawing.Size(1050, 20);
            this.txtCDRavien.TabIndex = 35;
            this.txtCDRavien.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(0, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "Chẩn đoán ra viện:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTinhtrangRavien
            // 
            this.txtTinhtrangRavien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTinhtrangRavien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangRavien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTinhtrangRavien.AddValues = true;
            this.txtTinhtrangRavien.AllowMultiline = false;
            this.txtTinhtrangRavien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinhtrangRavien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhtrangRavien.AutoCompleteList")));
            this.txtTinhtrangRavien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTinhtrangRavien.buildShortcut = false;
            this.txtTinhtrangRavien.CaseSensitive = false;
            this.txtTinhtrangRavien.CompareNoID = true;
            this.txtTinhtrangRavien.DefaultCode = "-1";
            this.txtTinhtrangRavien.DefaultID = "-1";
            this.txtTinhtrangRavien.Drug_ID = null;
            this.txtTinhtrangRavien.Enabled = false;
            this.txtTinhtrangRavien.ExtraWidth = 0;
            this.txtTinhtrangRavien.FillValueAfterSelect = false;
            this.txtTinhtrangRavien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangRavien.LOAI_DANHMUC = "TINHTRANGRAVIEN";
            this.txtTinhtrangRavien.Location = new System.Drawing.Point(156, 624);
            this.txtTinhtrangRavien.MaxHeight = 150;
            this.txtTinhtrangRavien.MaxLength = 255;
            this.txtTinhtrangRavien.MinTypedCharacters = 2;
            this.txtTinhtrangRavien.MyCode = "-1";
            this.txtTinhtrangRavien.MyID = "-1";
            this.txtTinhtrangRavien.Name = "txtTinhtrangRavien";
            this.txtTinhtrangRavien.RaiseEvent = false;
            this.txtTinhtrangRavien.RaiseEventEnter = false;
            this.txtTinhtrangRavien.RaiseEventEnterWhenEmpty = false;
            this.txtTinhtrangRavien.SelectedIndex = -1;
            this.txtTinhtrangRavien.ShowCodeWithValue = false;
            this.txtTinhtrangRavien.Size = new System.Drawing.Size(1050, 21);
            this.txtTinhtrangRavien.splitChar = '@';
            this.txtTinhtrangRavien.splitCharIDAndCode = '#';
            this.txtTinhtrangRavien.TabIndex = 10;
            this.txtTinhtrangRavien.TabStop = false;
            this.txtTinhtrangRavien.TakeCode = false;
            this.txtTinhtrangRavien.txtMyCode = null;
            this.txtTinhtrangRavien.txtMyCode_Edit = null;
            this.txtTinhtrangRavien.txtMyID = null;
            this.txtTinhtrangRavien.txtMyID_Edit = null;
            this.txtTinhtrangRavien.txtMyName = null;
            this.txtTinhtrangRavien.txtMyName_Edit = null;
            this.txtTinhtrangRavien.txtNext = null;
            this.txtTinhtrangRavien.txtNext1 = null;
            // 
            // txtTomtatCLS
            // 
            this.txtTomtatCLS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTomtatCLS.Location = new System.Drawing.Point(156, 495);
            this.txtTomtatCLS.Multiline = true;
            this.txtTomtatCLS.Name = "txtTomtatCLS";
            this.txtTomtatCLS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTomtatCLS.Size = new System.Drawing.Size(1050, 57);
            this.txtTomtatCLS.TabIndex = 8;
            // 
            // txtquatrinhbenhly
            // 
            this.txtquatrinhbenhly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtquatrinhbenhly.Location = new System.Drawing.Point(156, 261);
            this.txtquatrinhbenhly.Multiline = true;
            this.txtquatrinhbenhly.Name = "txtquatrinhbenhly";
            this.txtquatrinhbenhly.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtquatrinhbenhly.Size = new System.Drawing.Size(1050, 94);
            this.txtquatrinhbenhly.TabIndex = 5;
            // 
            // autoLydovv
            // 
            this.autoLydovv._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLydovv._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydovv._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoLydovv.AddValues = true;
            this.autoLydovv.AllowMultiline = false;
            this.autoLydovv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoLydovv.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoLydovv.AutoCompleteList")));
            this.autoLydovv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoLydovv.buildShortcut = false;
            this.autoLydovv.CaseSensitive = false;
            this.autoLydovv.CompareNoID = true;
            this.autoLydovv.DefaultCode = "-1";
            this.autoLydovv.DefaultID = "-1";
            this.autoLydovv.Drug_ID = null;
            this.autoLydovv.Enabled = false;
            this.autoLydovv.ExtraWidth = 0;
            this.autoLydovv.FillValueAfterSelect = false;
            this.autoLydovv.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydovv.LOAI_DANHMUC = "LYDOVAOVIEN";
            this.autoLydovv.Location = new System.Drawing.Point(156, 139);
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
            this.autoLydovv.Size = new System.Drawing.Size(1050, 21);
            this.autoLydovv.splitChar = '@';
            this.autoLydovv.splitCharIDAndCode = '#';
            this.autoLydovv.TabIndex = 0;
            this.autoLydovv.TabStop = false;
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
            this.txtPPdieutri.Enabled = false;
            this.txtPPdieutri.ExtraWidth = 0;
            this.txtPPdieutri.FillValueAfterSelect = false;
            this.txtPPdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPdieutri.LOAI_DANHMUC = "HDT";
            this.txtPPdieutri.Location = new System.Drawing.Point(156, 556);
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
            this.txtPPdieutri.Size = new System.Drawing.Size(1050, 21);
            this.txtPPdieutri.splitChar = '@';
            this.txtPPdieutri.splitCharIDAndCode = '#';
            this.txtPPdieutri.TabIndex = 9;
            this.txtPPdieutri.TabStop = false;
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
            this.autoKhoa.Location = new System.Drawing.Point(156, 236);
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
            this.autoKhoa.Size = new System.Drawing.Size(364, 21);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 2;
            this.autoKhoa.TabStop = false;
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
            this.ucThongtinnguoibenh_v21.Location = new System.Drawing.Point(44, 3);
            this.ucThongtinnguoibenh_v21.Name = "ucThongtinnguoibenh_v21";
            this.ucThongtinnguoibenh_v21.Size = new System.Drawing.Size(1171, 129);
            this.ucThongtinnguoibenh_v21.TabIndex = 22;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.Enabled = false;
            this.dtDenNgay.IsNullDate = true;
            this.dtDenNgay.Location = new System.Drawing.Point(371, 211);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(149, 20);
            this.dtDenNgay.TabIndex = 5;
            this.dtDenNgay.TabStop = false;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.Enabled = false;
            this.dtTuNgay.Location = new System.Drawing.Point(156, 211);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(142, 20);
            this.dtTuNgay.TabIndex = 4;
            this.dtTuNgay.TabStop = false;
            this.dtTuNgay.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(0, 557);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(153, 19);
            this.label14.TabIndex = 16;
            this.label14.Text = "Phương pháp điều trị";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(304, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Ngày ra viện:";
            // 
            // txtCDnhapvien
            // 
            this.txtCDnhapvien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCDnhapvien.Enabled = false;
            this.txtCDnhapvien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCDnhapvien.Location = new System.Drawing.Point(156, 163);
            this.txtCDnhapvien.Name = "txtCDnhapvien";
            this.txtCDnhapvien.Size = new System.Drawing.Size(1050, 20);
            this.txtCDnhapvien.TabIndex = 1;
            this.txtCDnhapvien.TabStop = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(0, 488);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 65);
            this.label8.TabIndex = 2;
            this.label8.Text = "Tóm tắt kết quả xét nghiệm, cận lâm sàng có giá trị chẩn đoán: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(0, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 94);
            this.label7.TabIndex = 2;
            this.label7.Text = "Tóm tắt quá trình bệnh lý và diễn biến lâm sàng (Đặc điểm khởi phát, các triệu ch" +
    "ứng lâm sàng, diễn biến bệnh)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(0, 626);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tình trạng ra viện";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(0, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ngày vào viện:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Điều trị tại khoa";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(0, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Chẩn đoán vào viện:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lý do vào viện";
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
            this.panel3.Location = new System.Drawing.Point(0, 734);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1238, 61);
            this.panel3.TabIndex = 1;
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.AutoSize = true;
            this.chkPreview.BackColor = System.Drawing.Color.Transparent;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(156, 30);
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
            this.cmdIn.Enabled = false;
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(719, 16);
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
            this.cmdXoa.Enabled = false;
            this.cmdXoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdXoa.Location = new System.Drawing.Point(845, 16);
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
            this.cmdExit.Location = new System.Drawing.Point(1095, 16);
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
            this.cmdThemmoi.Enabled = false;
            this.cmdThemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemmoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemmoi.Image")));
            this.cmdThemmoi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdThemmoi.Location = new System.Drawing.Point(593, 16);
            this.cmdThemmoi.Name = "cmdThemmoi";
            this.cmdThemmoi.Size = new System.Drawing.Size(120, 33);
            this.cmdThemmoi.TabIndex = 14;
            this.cmdThemmoi.TabStop = false;
            this.cmdThemmoi.Text = "Thêm mới";
            this.cmdThemmoi.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdThemmoi.Visible = false;
            this.cmdThemmoi.Click += new System.EventHandler(this.cmdThemmoi_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(970, 16);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 33);
            this.cmdSave.TabIndex = 11;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // chkNoikhoa
            // 
            this.chkNoikhoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkNoikhoa.AutoSize = true;
            this.chkNoikhoa.BackColor = System.Drawing.Color.Transparent;
            this.chkNoikhoa.Location = new System.Drawing.Point(84, 581);
            this.chkNoikhoa.Name = "chkNoikhoa";
            this.chkNoikhoa.Size = new System.Drawing.Size(69, 17);
            this.chkNoikhoa.TabIndex = 582;
            this.chkNoikhoa.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkNoikhoa.Text = "Nội khoa";
            this.chkNoikhoa.UseVisualStyleBackColor = false;
            this.chkNoikhoa.CheckedChanged += new System.EventHandler(this.chkNoikhoa_CheckedChanged);
            // 
            // txtNoikhoamota
            // 
            this.txtNoikhoamota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoikhoamota.Enabled = false;
            this.txtNoikhoamota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoikhoamota.Location = new System.Drawing.Point(156, 580);
            this.txtNoikhoamota.Name = "txtNoikhoamota";
            this.txtNoikhoamota.Size = new System.Drawing.Size(1050, 20);
            this.txtNoikhoamota.TabIndex = 10;
            // 
            // txtPTTTmota
            // 
            this.txtPTTTmota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPTTTmota.Enabled = false;
            this.txtPTTTmota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTTTmota.Location = new System.Drawing.Point(156, 603);
            this.txtPTTTmota.Name = "txtPTTTmota";
            this.txtPTTTmota.Size = new System.Drawing.Size(1050, 20);
            this.txtPTTTmota.TabIndex = 10;
            // 
            // chkPTTT
            // 
            this.chkPTTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPTTT.AutoSize = true;
            this.chkPTTT.BackColor = System.Drawing.Color.Transparent;
            this.chkPTTT.Location = new System.Drawing.Point(27, 607);
            this.chkPTTT.Name = "chkPTTT";
            this.chkPTTT.Size = new System.Drawing.Size(126, 17);
            this.chkPTTT.TabIndex = 585;
            this.chkPTTT.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkPTTT.Text = "Phẫu thuật, thủ thuật";
            this.chkPTTT.UseVisualStyleBackColor = false;
            this.chkPTTT.CheckedChanged += new System.EventHandler(this.chkPTTT_CheckedChanged);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(3, 650);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 38);
            this.label12.TabIndex = 587;
            this.label12.Text = "Hướng điều trị và các chế độ tiếp theo:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHuongdieutri
            // 
            this.txtHuongdieutri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHuongdieutri.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtHuongdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHuongdieutri.Location = new System.Drawing.Point(156, 648);
            this.txtHuongdieutri.MaxLength = 1000;
            this.txtHuongdieutri.Multiline = true;
            this.txtHuongdieutri.Name = "txtHuongdieutri";
            this.txtHuongdieutri.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHuongdieutri.Size = new System.Drawing.Size(1050, 80);
            this.txtHuongdieutri.TabIndex = 588;
            this.txtHuongdieutri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            this.txtHuongdieutri.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // frm_TomtatBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 795);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TomtatBA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng kết bệnh án";
            this.Load += new System.EventHandler(this.frm_TomtatBA_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCDnhapvien;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private AutoCompleteTextbox autoKhoa;
        public AutoCompleteTextbox_Danhmucchung txtPPdieutri;
        private AutoCompleteTextbox_Danhmucchung autoLydovv;
        public AutoCompleteTextbox_Danhmucchung txtTinhtrangRavien;
        private Janus.Windows.GridEX.EditControls.EditBox txtTomtatCLS;
        private Janus.Windows.GridEX.EditControls.EditBox txtquatrinhbenhly;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdXoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdThemmoi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_v2 ucThongtinnguoibenh_v21;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.TextBox txtCDRavien;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtTiensubenh;
        private System.Windows.Forms.Label label54;
        private Janus.Windows.GridEX.EditControls.EditBox txtDauhieulamsang;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayTTBA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkPTTT;
        private System.Windows.Forms.TextBox txtPTTTmota;
        private System.Windows.Forms.TextBox txtNoikhoamota;
        private System.Windows.Forms.CheckBox chkNoikhoa;
        private Janus.Windows.GridEX.EditControls.EditBox txtHuongdieutri;
        private System.Windows.Forms.Label label12;
    }
}