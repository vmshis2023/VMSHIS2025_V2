
namespace VMS.HIS.Duoc.DUOC
{
    partial class frm_bienban_kiemnhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_bienban_kiemnhap));
            Janus.Windows.GridEX.GridEXLayout grd_hoidong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.txt_hoten = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txt_chucdanh = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_ngaybienban = new System.Windows.Forms.DateTimePicker();
            this.lbl_ngaykiemnhap = new System.Windows.Forms.Label();
            this.txt_chucvu = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nmr_stt = new System.Windows.Forms.NumericUpDown();
            this.grd_hoidong = new Janus.Windows.GridEX.GridEX();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.lbl_title = new System.Windows.Forms.Label();
            this.txt_loaihoidong = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label3 = new System.Windows.Forms.Label();
            this.cmd_add = new Janus.Windows.EditControls.UIButton();
            this.cmd_sua = new Janus.Windows.EditControls.UIButton();
            this.cmd_luu = new Janus.Windows.EditControls.UIButton();
            this.txt_IdHoiDong = new System.Windows.Forms.TextBox();
            this.txt_uuid = new System.Windows.Forms.TextBox();
            this.cmd_quanly_loaihoidong = new Janus.Windows.EditControls.UIButton();
            this.cmd_quanly_chucdanh = new Janus.Windows.EditControls.UIButton();
            this.cmd_quanly_chucvu = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nmr_stt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_hoidong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_hoten
            // 
            this.txt_hoten._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_hoten._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_hoten._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_hoten.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_hoten.AutoCompleteList")));
            this.txt_hoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_hoten.buildShortcut = false;
            this.txt_hoten.CaseSensitive = false;
            this.txt_hoten.CompareNoID = true;
            this.txt_hoten.DefaultCode = "-1";
            this.txt_hoten.DefaultID = "-1";
            this.txt_hoten.DisplayType = 0;
            this.txt_hoten.Drug_ID = null;
            this.txt_hoten.ExtraWidth = 300;
            this.txt_hoten.FillValueAfterSelect = false;
            this.txt_hoten.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_hoten.Location = new System.Drawing.Point(133, 80);
            this.txt_hoten.MaxHeight = 289;
            this.txt_hoten.MinTypedCharacters = 2;
            this.txt_hoten.MyCode = "-1";
            this.txt_hoten.MyID = "-1";
            this.txt_hoten.MyText = "";
            this.txt_hoten.MyTextOnly = "";
            this.txt_hoten.Name = "txt_hoten";
            this.txt_hoten.RaiseEvent = true;
            this.txt_hoten.RaiseEventEnter = true;
            this.txt_hoten.RaiseEventEnterWhenEmpty = true;
            this.txt_hoten.SelectedIndex = -1;
            this.txt_hoten.Size = new System.Drawing.Size(257, 22);
            this.txt_hoten.splitChar = '@';
            this.txt_hoten.splitCharIDAndCode = '#';
            this.txt_hoten.TabIndex = 5;
            this.txt_hoten.TakeCode = false;
            this.txt_hoten.txtMyCode = null;
            this.txt_hoten.txtMyCode_Edit = null;
            this.txt_hoten.txtMyID = null;
            this.txt_hoten.txtMyID_Edit = null;
            this.txt_hoten.txtMyName = null;
            this.txt_hoten.txtMyName_Edit = null;
            this.txt_hoten.txtNext = null;
            // 
            // txt_chucdanh
            // 
            this.txt_chucdanh._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_chucdanh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucdanh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_chucdanh.AddValues = true;
            this.txt_chucdanh.AllowMultiline = false;
            this.txt_chucdanh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_chucdanh.AutoCompleteList")));
            this.txt_chucdanh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_chucdanh.buildShortcut = false;
            this.txt_chucdanh.CaseSensitive = false;
            this.txt_chucdanh.cmdDropDown = null;
            this.txt_chucdanh.CompareNoID = true;
            this.txt_chucdanh.DefaultCode = "-1";
            this.txt_chucdanh.DefaultID = "-1";
            this.txt_chucdanh.Drug_ID = null;
            this.txt_chucdanh.ExtraWidth = 300;
            this.txt_chucdanh.FillValueAfterSelect = false;
            this.txt_chucdanh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucdanh.LOAI_DANHMUC = "NVIEN_CHUCDANH_NN";
            this.txt_chucdanh.Location = new System.Drawing.Point(504, 80);
            this.txt_chucdanh.MaxHeight = 200;
            this.txt_chucdanh.MinTypedCharacters = 2;
            this.txt_chucdanh.MyCode = "-1";
            this.txt_chucdanh.MyID = "-1";
            this.txt_chucdanh.Name = "txt_chucdanh";
            this.txt_chucdanh.RaiseEvent = false;
            this.txt_chucdanh.RaiseEventEnter = false;
            this.txt_chucdanh.RaiseEventEnterWhenEmpty = false;
            this.txt_chucdanh.SelectedIndex = -1;
            this.txt_chucdanh.SetDefaultWhenInit = false;
            this.txt_chucdanh.ShowCodeWithValue = false;
            this.txt_chucdanh.Size = new System.Drawing.Size(257, 22);
            this.txt_chucdanh.splitChar = '@';
            this.txt_chucdanh.splitCharIDAndCode = '#';
            this.txt_chucdanh.TabIndex = 6;
            this.txt_chucdanh.TakeCode = false;
            this.txt_chucdanh.txtMyCode = null;
            this.txt_chucdanh.txtMyCode_Edit = null;
            this.txt_chucdanh.txtMyID = null;
            this.txt_chucdanh.txtMyID_Edit = null;
            this.txt_chucdanh.txtMyName = null;
            this.txt_chucdanh.txtMyName_Edit = null;
            this.txt_chucdanh.txtNext = null;
            this.txt_chucdanh.txtNext1 = null;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(23, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 18);
            this.label12.TabIndex = 28;
            this.label12.Text = "Họ tên:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(394, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 18);
            this.label9.TabIndex = 27;
            this.label9.Text = "Chức danh:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_ngaybienban
            // 
            this.dtp_ngaybienban.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtp_ngaybienban.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ngaybienban.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ngaybienban.Location = new System.Drawing.Point(133, 52);
            this.dtp_ngaybienban.Name = "dtp_ngaybienban";
            this.dtp_ngaybienban.ShowUpDown = true;
            this.dtp_ngaybienban.Size = new System.Drawing.Size(171, 22);
            this.dtp_ngaybienban.TabIndex = 0;
            // 
            // lbl_ngaykiemnhap
            // 
            this.lbl_ngaykiemnhap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ngaykiemnhap.ForeColor = System.Drawing.Color.Red;
            this.lbl_ngaykiemnhap.Location = new System.Drawing.Point(23, 54);
            this.lbl_ngaykiemnhap.Name = "lbl_ngaykiemnhap";
            this.lbl_ngaykiemnhap.Size = new System.Drawing.Size(108, 18);
            this.lbl_ngaykiemnhap.TabIndex = 30;
            this.lbl_ngaykiemnhap.Text = "Ngày biên bản";
            this.lbl_ngaykiemnhap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_chucvu
            // 
            this.txt_chucvu._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_chucvu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucvu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_chucvu.AddValues = true;
            this.txt_chucvu.AllowMultiline = false;
            this.txt_chucvu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_chucvu.AutoCompleteList")));
            this.txt_chucvu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_chucvu.buildShortcut = false;
            this.txt_chucvu.CaseSensitive = false;
            this.txt_chucvu.cmdDropDown = null;
            this.txt_chucvu.CompareNoID = true;
            this.txt_chucvu.DefaultCode = "-1";
            this.txt_chucvu.DefaultID = "-1";
            this.txt_chucvu.Drug_ID = null;
            this.txt_chucvu.ExtraWidth = 300;
            this.txt_chucvu.FillValueAfterSelect = false;
            this.txt_chucvu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucvu.LOAI_DANHMUC = "NVIEN_CHUCVU";
            this.txt_chucvu.Location = new System.Drawing.Point(133, 108);
            this.txt_chucvu.MaxHeight = 200;
            this.txt_chucvu.MinTypedCharacters = 2;
            this.txt_chucvu.MyCode = "-1";
            this.txt_chucvu.MyID = "-1";
            this.txt_chucvu.Name = "txt_chucvu";
            this.txt_chucvu.RaiseEvent = false;
            this.txt_chucvu.RaiseEventEnter = false;
            this.txt_chucvu.RaiseEventEnterWhenEmpty = false;
            this.txt_chucvu.SelectedIndex = -1;
            this.txt_chucvu.SetDefaultWhenInit = false;
            this.txt_chucvu.ShowCodeWithValue = false;
            this.txt_chucvu.Size = new System.Drawing.Size(257, 22);
            this.txt_chucvu.splitChar = '@';
            this.txt_chucvu.splitCharIDAndCode = '#';
            this.txt_chucvu.TabIndex = 7;
            this.txt_chucvu.TakeCode = false;
            this.txt_chucvu.txtMyCode = null;
            this.txt_chucvu.txtMyCode_Edit = null;
            this.txt_chucvu.txtMyID = null;
            this.txt_chucvu.txtMyID_Edit = null;
            this.txt_chucvu.txtMyName = null;
            this.txt_chucvu.txtMyName_Edit = null;
            this.txt_chucvu.txtNext = null;
            this.txt_chucvu.txtNext1 = null;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(56, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 32;
            this.label1.Text = "Chức vụ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(410, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 33;
            this.label2.Text = "STT hiển thị:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmr_stt
            // 
            this.nmr_stt.Font = new System.Drawing.Font("Arial", 9.75F);
            this.nmr_stt.Location = new System.Drawing.Point(504, 109);
            this.nmr_stt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_stt.Name = "nmr_stt";
            this.nmr_stt.Size = new System.Drawing.Size(83, 22);
            this.nmr_stt.TabIndex = 8;
            this.nmr_stt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grd_hoidong
            // 
            this.grd_hoidong.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_hoidong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grd_hoidong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grd_hoidong_DesignTimeLayout.LayoutString = resources.GetString("grd_hoidong_DesignTimeLayout.LayoutString");
            this.grd_hoidong.DesignTimeLayout = grd_hoidong_DesignTimeLayout;
            this.grd_hoidong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_hoidong.GroupByBoxVisible = false;
            this.grd_hoidong.Location = new System.Drawing.Point(133, 180);
            this.grd_hoidong.Name = "grd_hoidong";
            this.grd_hoidong.Size = new System.Drawing.Size(687, 243);
            this.grd_hoidong.TabIndex = 572;
            this.grd_hoidong.TabStop = false;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = global::VMS.HIS.Duoc.Properties.Resources.printer_32;
            this.cmdPrint.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdPrint.Location = new System.Drawing.Point(448, 450);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 35);
            this.cmdPrint.TabIndex = 11;
            this.cmdPrint.Text = "In (Ctrl+P)";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdSave.Location = new System.Drawing.Point(574, 450);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdExit.Location = new System.Drawing.Point(700, 450);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_msg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(12, 447);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(398, 35);
            this.lbl_msg.TabIndex = 576;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.SystemColors.WindowText;
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.vbLine1.Location = new System.Drawing.Point(13, 426);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(774, 22);
            this.vbLine1.TabIndex = 100;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Thực hiện";
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.Navy;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(832, 39);
            this.lbl_title.TabIndex = 578;
            this.lbl_title.Text = "BIÊN BẢN KIỂM NHẬP";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_loaihoidong
            // 
            this.txt_loaihoidong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_loaihoidong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_loaihoidong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_loaihoidong.AddValues = true;
            this.txt_loaihoidong.AllowMultiline = false;
            this.txt_loaihoidong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_loaihoidong.AutoCompleteList")));
            this.txt_loaihoidong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_loaihoidong.buildShortcut = false;
            this.txt_loaihoidong.CaseSensitive = false;
            this.txt_loaihoidong.cmdDropDown = null;
            this.txt_loaihoidong.CompareNoID = true;
            this.txt_loaihoidong.DefaultCode = "-1";
            this.txt_loaihoidong.DefaultID = "-1";
            this.txt_loaihoidong.Drug_ID = null;
            this.txt_loaihoidong.ExtraWidth = 300;
            this.txt_loaihoidong.FillValueAfterSelect = false;
            this.txt_loaihoidong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_loaihoidong.LOAI_DANHMUC = "THUOC_LOAIHOIDONG";
            this.txt_loaihoidong.Location = new System.Drawing.Point(504, 53);
            this.txt_loaihoidong.MaxHeight = 200;
            this.txt_loaihoidong.MinTypedCharacters = 2;
            this.txt_loaihoidong.MyCode = "-1";
            this.txt_loaihoidong.MyID = "-1";
            this.txt_loaihoidong.Name = "txt_loaihoidong";
            this.txt_loaihoidong.RaiseEvent = false;
            this.txt_loaihoidong.RaiseEventEnter = false;
            this.txt_loaihoidong.RaiseEventEnterWhenEmpty = false;
            this.txt_loaihoidong.SelectedIndex = -1;
            this.txt_loaihoidong.SetDefaultWhenInit = false;
            this.txt_loaihoidong.ShowCodeWithValue = false;
            this.txt_loaihoidong.Size = new System.Drawing.Size(257, 22);
            this.txt_loaihoidong.splitChar = '@';
            this.txt_loaihoidong.splitCharIDAndCode = '#';
            this.txt_loaihoidong.TabIndex = 1;
            this.txt_loaihoidong.TakeCode = false;
            this.txt_loaihoidong.txtMyCode = null;
            this.txt_loaihoidong.txtMyCode_Edit = null;
            this.txt_loaihoidong.txtMyID = null;
            this.txt_loaihoidong.txtMyID_Edit = null;
            this.txt_loaihoidong.txtMyName = null;
            this.txt_loaihoidong.txtMyName_Edit = null;
            this.txt_loaihoidong.txtNext = null;
            this.txt_loaihoidong.txtNext1 = null;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(394, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 580;
            this.label3.Text = "Loại hội đồng";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmd_add
            // 
            this.cmd_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_add.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_add.Image = global::VMS.HIS.Duoc.Properties.Resources.add_04_32;
            this.cmd_add.ImageSize = new System.Drawing.Size(22, 22);
            this.cmd_add.Location = new System.Drawing.Point(133, 144);
            this.cmd_add.Name = "cmd_add";
            this.cmd_add.Size = new System.Drawing.Size(95, 30);
            this.cmd_add.TabIndex = 581;
            this.cmd_add.Text = "Thêm mới";
            this.cmd_add.Click += new System.EventHandler(this.cmd_add_Click);
            // 
            // cmd_sua
            // 
            this.cmd_sua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_sua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_sua.Image = ((System.Drawing.Image)(resources.GetObject("cmd_sua.Image")));
            this.cmd_sua.ImageSize = new System.Drawing.Size(22, 22);
            this.cmd_sua.Location = new System.Drawing.Point(234, 144);
            this.cmd_sua.Name = "cmd_sua";
            this.cmd_sua.Size = new System.Drawing.Size(95, 30);
            this.cmd_sua.TabIndex = 582;
            this.cmd_sua.Text = "Sửa";
            this.cmd_sua.Click += new System.EventHandler(this.cmd_sua_Click);
            // 
            // cmd_luu
            // 
            this.cmd_luu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_luu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_luu.Image = global::VMS.HIS.Duoc.Properties.Resources.SAVE__2_;
            this.cmd_luu.ImageSize = new System.Drawing.Size(22, 22);
            this.cmd_luu.Location = new System.Drawing.Point(335, 144);
            this.cmd_luu.Name = "cmd_luu";
            this.cmd_luu.Size = new System.Drawing.Size(95, 30);
            this.cmd_luu.TabIndex = 9;
            this.cmd_luu.Text = "Lưu";
            this.cmd_luu.Click += new System.EventHandler(this.cmd_luu_Click);
            // 
            // txt_IdHoiDong
            // 
            this.txt_IdHoiDong.Location = new System.Drawing.Point(767, 54);
            this.txt_IdHoiDong.Name = "txt_IdHoiDong";
            this.txt_IdHoiDong.Size = new System.Drawing.Size(42, 20);
            this.txt_IdHoiDong.TabIndex = 584;
            this.txt_IdHoiDong.Visible = false;
            // 
            // txt_uuid
            // 
            this.txt_uuid.Location = new System.Drawing.Point(767, 78);
            this.txt_uuid.Name = "txt_uuid";
            this.txt_uuid.Size = new System.Drawing.Size(42, 20);
            this.txt_uuid.TabIndex = 585;
            this.txt_uuid.Visible = false;
            // 
            // cmd_quanly_loaihoidong
            // 
            this.cmd_quanly_loaihoidong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_quanly_loaihoidong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_quanly_loaihoidong.Image = global::VMS.HIS.Duoc.Properties.Resources.Add32;
            this.cmd_quanly_loaihoidong.Location = new System.Drawing.Point(762, 52);
            this.cmd_quanly_loaihoidong.Name = "cmd_quanly_loaihoidong";
            this.cmd_quanly_loaihoidong.Size = new System.Drawing.Size(24, 24);
            this.cmd_quanly_loaihoidong.TabIndex = 586;
            // 
            // cmd_quanly_chucdanh
            // 
            this.cmd_quanly_chucdanh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_quanly_chucdanh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_quanly_chucdanh.Image = global::VMS.HIS.Duoc.Properties.Resources.Add32;
            this.cmd_quanly_chucdanh.Location = new System.Drawing.Point(762, 79);
            this.cmd_quanly_chucdanh.Name = "cmd_quanly_chucdanh";
            this.cmd_quanly_chucdanh.Size = new System.Drawing.Size(24, 24);
            this.cmd_quanly_chucdanh.TabIndex = 587;
            // 
            // cmd_quanly_chucvu
            // 
            this.cmd_quanly_chucvu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_quanly_chucvu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_quanly_chucvu.Image = global::VMS.HIS.Duoc.Properties.Resources.Add32;
            this.cmd_quanly_chucvu.Location = new System.Drawing.Point(390, 107);
            this.cmd_quanly_chucvu.Name = "cmd_quanly_chucvu";
            this.cmd_quanly_chucvu.Size = new System.Drawing.Size(24, 24);
            this.cmd_quanly_chucvu.TabIndex = 588;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_bienban_kiemnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 497);
            this.Controls.Add(this.cmd_quanly_chucvu);
            this.Controls.Add(this.cmd_quanly_chucdanh);
            this.Controls.Add(this.cmd_quanly_loaihoidong);
            this.Controls.Add(this.txt_uuid);
            this.Controls.Add(this.txt_IdHoiDong);
            this.Controls.Add(this.cmd_luu);
            this.Controls.Add(this.cmd_sua);
            this.Controls.Add(this.cmd_add);
            this.Controls.Add(this.txt_loaihoidong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grd_hoidong);
            this.Controls.Add(this.nmr_stt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_chucvu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_ngaybienban);
            this.Controls.Add(this.lbl_ngaykiemnhap);
            this.Controls.Add(this.txt_hoten);
            this.Controls.Add(this.txt_chucdanh);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.vbLine1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_bienban_kiemnhap";
            this.ShowIcon = false;
            this.Text = "Biên bản kiểm nhập";
            this.Load += new System.EventHandler(this.frm_bienban_kiemnhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmr_stt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_hoidong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VNS.HIS.UCs.AutoCompleteTextbox txt_hoten;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txt_chucdanh;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtp_ngaybienban;
        private System.Windows.Forms.Label lbl_ngaykiemnhap;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txt_chucvu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmr_stt;
        private Janus.Windows.GridEX.GridEX grd_hoidong;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lbl_msg;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label lbl_title;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txt_loaihoidong;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmd_add;
        private Janus.Windows.EditControls.UIButton cmd_sua;
        private Janus.Windows.EditControls.UIButton cmd_luu;
        private System.Windows.Forms.TextBox txt_IdHoiDong;
        private System.Windows.Forms.TextBox txt_uuid;
        private Janus.Windows.EditControls.UIButton cmd_quanly_loaihoidong;
        private Janus.Windows.EditControls.UIButton cmd_quanly_chucdanh;
        private Janus.Windows.EditControls.UIButton cmd_quanly_chucvu;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}