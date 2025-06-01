namespace VNS.HIS.UI.NOITRU
{
    partial class frm_PhieuSoKetDieuTri
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
            Janus.Windows.GridEX.GridEXLayout grdLichSu_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuSoKetDieuTri));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdLichSu = new Janus.Windows.GridEX.GridEX();
            this.dtNgayHoiChan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtId = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpBienBan = new Janus.Windows.EditControls.UIGroupBox();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBacsidieutri = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtXNghiemCLS = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtHuongDTTiepVaTienLuong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtDanhGiaKQ = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtQuaTrinhDieuTri = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtDienBienCLS = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdXoa = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdThemmoi = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.ucThongtinnguoibenh_v21 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v2();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBienBan)).BeginInit();
            this.grpBienBan.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiGroupBox1.Controls.Add(this.grdLichSu);
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 137);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(391, 597);
            this.uiGroupBox1.TabIndex = 29;
            this.uiGroupBox1.Text = "Lịch sử";
            // 
            // grdLichSu
            // 
            this.grdLichSu.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdLichSu_DesignTimeLayout.LayoutString = resources.GetString("grdLichSu_DesignTimeLayout.LayoutString");
            this.grdLichSu.DesignTimeLayout = grdLichSu_DesignTimeLayout;
            this.grdLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLichSu.GroupByBoxVisible = false;
            this.grdLichSu.Location = new System.Drawing.Point(3, 17);
            this.grdLichSu.Name = "grdLichSu";
            this.grdLichSu.Size = new System.Drawing.Size(385, 577);
            this.grdLichSu.TabIndex = 0;
            this.grdLichSu.TabStop = false;
            // 
            // dtNgayHoiChan
            // 
            this.dtNgayHoiChan.CustomFormat = "dd/MM/yyyy";
            this.dtNgayHoiChan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayHoiChan.DropDownCalendar.Name = "";
            this.dtNgayHoiChan.Location = new System.Drawing.Point(474, 21);
            this.dtNgayHoiChan.Name = "dtNgayHoiChan";
            this.dtNgayHoiChan.ReadOnly = true;
            this.dtNgayHoiChan.ShowUpDown = true;
            this.dtNgayHoiChan.Size = new System.Drawing.Size(107, 21);
            this.dtNgayHoiChan.TabIndex = 1;
            this.dtNgayHoiChan.Value = new System.DateTime(2019, 11, 29, 14, 4, 44, 0);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 425);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 85);
            this.label9.TabIndex = 17;
            this.label9.Text = "Hướng điều trị tiếp và tiên lượng";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 338);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 84);
            this.label10.TabIndex = 16;
            this.label10.Text = "Đánh giá kết quả";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "Bác sỹ điều trị";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 84);
            this.label6.TabIndex = 15;
            this.label6.Text = "Quá trình điều trị";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(36, 737);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(13, 21);
            this.txtId.TabIndex = 34;
            this.txtId.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 83);
            this.label5.TabIndex = 14;
            this.label5.Text = "Xét nghiệm cận lâm sàng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(410, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Ngày lập:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 84);
            this.label3.TabIndex = 11;
            this.label3.Text = "Diễn biến lâm sàng trong đợt điều trị";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpBienBan
            // 
            this.grpBienBan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBienBan.Controls.Add(this.autoKhoa);
            this.grpBienBan.Controls.Add(this.txtBacsidieutri);
            this.grpBienBan.Controls.Add(this.txtXNghiemCLS);
            this.grpBienBan.Controls.Add(this.txtHuongDTTiepVaTienLuong);
            this.grpBienBan.Controls.Add(this.txtDanhGiaKQ);
            this.grpBienBan.Controls.Add(this.txtQuaTrinhDieuTri);
            this.grpBienBan.Controls.Add(this.txtDienBienCLS);
            this.grpBienBan.Controls.Add(this.dtNgayHoiChan);
            this.grpBienBan.Controls.Add(this.label9);
            this.grpBienBan.Controls.Add(this.label10);
            this.grpBienBan.Controls.Add(this.label7);
            this.grpBienBan.Controls.Add(this.label6);
            this.grpBienBan.Controls.Add(this.label5);
            this.grpBienBan.Controls.Add(this.label4);
            this.grpBienBan.Controls.Add(this.label3);
            this.grpBienBan.Controls.Add(this.label2);
            this.grpBienBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBienBan.Location = new System.Drawing.Point(397, 137);
            this.grpBienBan.Name = "grpBienBan";
            this.grpBienBan.Size = new System.Drawing.Size(775, 597);
            this.grpBienBan.TabIndex = 28;
            this.grpBienBan.Text = "Biên bản";
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
            this.autoKhoa.Location = new System.Drawing.Point(110, 21);
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
            this.autoKhoa.Size = new System.Drawing.Size(294, 21);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 0;
            this.autoKhoa.TakeCode = false;
            this.autoKhoa.txtMyCode = null;
            this.autoKhoa.txtMyCode_Edit = null;
            this.autoKhoa.txtMyID = null;
            this.autoKhoa.txtMyID_Edit = null;
            this.autoKhoa.txtMyName = null;
            this.autoKhoa.txtMyName_Edit = null;
            this.autoKhoa.txtNext = null;
            // 
            // txtBacsidieutri
            // 
            this.txtBacsidieutri._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBacsidieutri._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsidieutri._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBacsidieutri.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBacsidieutri.AutoCompleteList")));
            this.txtBacsidieutri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacsidieutri.buildShortcut = false;
            this.txtBacsidieutri.CaseSensitive = false;
            this.txtBacsidieutri.CompareNoID = true;
            this.txtBacsidieutri.DefaultCode = "-1";
            this.txtBacsidieutri.DefaultID = "-1";
            this.txtBacsidieutri.DisplayType = 0;
            this.txtBacsidieutri.Drug_ID = null;
            this.txtBacsidieutri.ExtraWidth = 0;
            this.txtBacsidieutri.FillValueAfterSelect = false;
            this.txtBacsidieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsidieutri.Location = new System.Drawing.Point(110, 50);
            this.txtBacsidieutri.MaxHeight = 289;
            this.txtBacsidieutri.MinTypedCharacters = 2;
            this.txtBacsidieutri.MyCode = "-1";
            this.txtBacsidieutri.MyID = "-1";
            this.txtBacsidieutri.MyText = "";
            this.txtBacsidieutri.MyTextOnly = "";
            this.txtBacsidieutri.Name = "txtBacsidieutri";
            this.txtBacsidieutri.RaiseEvent = true;
            this.txtBacsidieutri.RaiseEventEnter = true;
            this.txtBacsidieutri.RaiseEventEnterWhenEmpty = true;
            this.txtBacsidieutri.SelectedIndex = -1;
            this.txtBacsidieutri.Size = new System.Drawing.Size(294, 21);
            this.txtBacsidieutri.splitChar = '@';
            this.txtBacsidieutri.splitCharIDAndCode = '#';
            this.txtBacsidieutri.TabIndex = 2;
            this.txtBacsidieutri.TakeCode = false;
            this.txtBacsidieutri.txtMyCode = null;
            this.txtBacsidieutri.txtMyCode_Edit = null;
            this.txtBacsidieutri.txtMyID = null;
            this.txtBacsidieutri.txtMyID_Edit = null;
            this.txtBacsidieutri.txtMyName = null;
            this.txtBacsidieutri.txtMyName_Edit = null;
            this.txtBacsidieutri.txtNext = null;
            // 
            // txtXNghiemCLS
            // 
            this.txtXNghiemCLS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXNghiemCLS.Location = new System.Drawing.Point(110, 165);
            this.txtXNghiemCLS.Multiline = true;
            this.txtXNghiemCLS.Name = "txtXNghiemCLS";
            this.txtXNghiemCLS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXNghiemCLS.Size = new System.Drawing.Size(648, 83);
            this.txtXNghiemCLS.TabIndex = 4;
            // 
            // txtHuongDTTiepVaTienLuong
            // 
            this.txtHuongDTTiepVaTienLuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHuongDTTiepVaTienLuong.Location = new System.Drawing.Point(110, 425);
            this.txtHuongDTTiepVaTienLuong.Multiline = true;
            this.txtHuongDTTiepVaTienLuong.Name = "txtHuongDTTiepVaTienLuong";
            this.txtHuongDTTiepVaTienLuong.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHuongDTTiepVaTienLuong.Size = new System.Drawing.Size(647, 85);
            this.txtHuongDTTiepVaTienLuong.TabIndex = 7;
            // 
            // txtDanhGiaKQ
            // 
            this.txtDanhGiaKQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDanhGiaKQ.Location = new System.Drawing.Point(110, 338);
            this.txtDanhGiaKQ.Multiline = true;
            this.txtDanhGiaKQ.Name = "txtDanhGiaKQ";
            this.txtDanhGiaKQ.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDanhGiaKQ.Size = new System.Drawing.Size(648, 84);
            this.txtDanhGiaKQ.TabIndex = 6;
            // 
            // txtQuaTrinhDieuTri
            // 
            this.txtQuaTrinhDieuTri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuaTrinhDieuTri.Location = new System.Drawing.Point(110, 251);
            this.txtQuaTrinhDieuTri.Multiline = true;
            this.txtQuaTrinhDieuTri.Name = "txtQuaTrinhDieuTri";
            this.txtQuaTrinhDieuTri.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuaTrinhDieuTri.Size = new System.Drawing.Size(648, 84);
            this.txtQuaTrinhDieuTri.TabIndex = 5;
            // 
            // txtDienBienCLS
            // 
            this.txtDienBienCLS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienBienCLS.Location = new System.Drawing.Point(110, 77);
            this.txtDienBienCLS.Multiline = true;
            this.txtDienBienCLS.Name = "txtDienBienCLS";
            this.txtDienBienCLS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDienBienCLS.Size = new System.Drawing.Size(647, 85);
            this.txtDienBienCLS.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Khoa điều trị";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(891, 746);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(129, 37);
            this.cmdIn.TabIndex = 9;
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
            this.cmdXoa.Location = new System.Drawing.Point(756, 744);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(129, 37);
            this.cmdXoa.TabIndex = 10;
            this.cmdXoa.TabStop = false;
            this.cmdXoa.Text = "Xóa";
            this.cmdXoa.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdXoa.Click += new System.EventHandler(this.cmdXoa_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1026, 744);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 39);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdThemmoi
            // 
            this.cmdThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemmoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemmoi.Image")));
            this.cmdThemmoi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdThemmoi.Location = new System.Drawing.Point(486, 744);
            this.cmdThemmoi.Name = "cmdThemmoi";
            this.cmdThemmoi.Size = new System.Drawing.Size(129, 37);
            this.cmdThemmoi.TabIndex = 11;
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
            this.cmdSave.Location = new System.Drawing.Point(621, 744);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(129, 37);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // ucThongtinnguoibenh_v21
            // 
            this.ucThongtinnguoibenh_v21.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_v21.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_v21.Name = "ucThongtinnguoibenh_v21";
            this.ucThongtinnguoibenh_v21.Size = new System.Drawing.Size(1184, 129);
            this.ucThongtinnguoibenh_v21.TabIndex = 0;
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.AutoSize = true;
            this.chkPreview.BackColor = System.Drawing.Color.Transparent;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(12, 762);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(108, 17);
            this.chkPreview.TabIndex = 580;
            this.chkPreview.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkPreview.Text = "Xem trước khi in?";
            this.chkPreview.UseVisualStyleBackColor = false;
            // 
            // frm_PhieuSoKetDieuTri
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1184, 791);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.ucThongtinnguoibenh_v21);
            this.Controls.Add(this.cmdIn);
            this.Controls.Add(this.cmdXoa);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdThemmoi);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.grpBienBan);
            this.KeyPreview = true;
            this.Name = "frm_PhieuSoKetDieuTri";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu sơ kết 15 ngày điều trị";
            this.Load += new System.EventHandler(this.frm_PhieuSoKetDieuTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBienBan)).EndInit();
            this.grpBienBan.ResumeLayout(false);
            this.grpBienBan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdLichSu;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayHoiChan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIGroupBox grpBienBan;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdXoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdThemmoi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.GridEX.EditControls.EditBox txtXNghiemCLS;
        private Janus.Windows.GridEX.EditControls.EditBox txtHuongDTTiepVaTienLuong;
        private Janus.Windows.GridEX.EditControls.EditBox txtDanhGiaKQ;
        private Janus.Windows.GridEX.EditControls.EditBox txtQuaTrinhDieuTri;
        private Janus.Windows.GridEX.EditControls.EditBox txtDienBienCLS;
        private UCs.AutoCompleteTextbox autoKhoa;
        private UCs.AutoCompleteTextbox txtBacsidieutri;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_v2 ucThongtinnguoibenh_v21;
        private System.Windows.Forms.CheckBox chkPreview;
    }
}