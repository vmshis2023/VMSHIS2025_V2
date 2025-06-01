using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_phieutuvanPTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_phieutuvanPTTT));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grd_bskhac_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.dtNgayIn = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ucThongtinnguoibenh_doc_v11 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_doc_v1();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkGiamdau2 = new System.Windows.Forms.CheckBox();
            this.chkGiamdau1 = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkThuoc4 = new System.Windows.Forms.CheckBox();
            this.chkThuoc2 = new System.Windows.Forms.CheckBox();
            this.chkThuoc3 = new System.Windows.Forms.CheckBox();
            this.chkThuoc1 = new System.Windows.Forms.CheckBox();
            this.txtPPgiamdau = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhuongPhapVoCam = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label1 = new System.Windows.Forms.Label();
            this.grd_bskhac = new Janus.Windows.GridEX.GridEX();
            this.txtBsKhac = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtBsChinh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtNgaytuvan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label11 = new System.Windows.Forms.Label();
            this.txtGhichu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRuiro = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtChandoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdXoa = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdThemmoi = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bskhac)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.autoKhoa);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chkGiamdau2);
            this.panel1.Controls.Add(this.chkGiamdau1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.chkThuoc4);
            this.panel1.Controls.Add(this.chkThuoc2);
            this.panel1.Controls.Add(this.chkThuoc3);
            this.panel1.Controls.Add(this.chkThuoc1);
            this.panel1.Controls.Add(this.txtPPgiamdau);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPhuongPhapVoCam);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.grd_bskhac);
            this.panel1.Controls.Add(this.txtBsKhac);
            this.panel1.Controls.Add(this.txtBsChinh);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.dtNgaytuvan);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtGhichu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtRuiro);
            this.panel1.Controls.Add(this.txtChandoan);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1410, 790);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(384, 790);
            this.panel4.TabIndex = 618;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdIn);
            this.groupBox1.Controls.Add(this.grdList);
            this.groupBox1.Controls.Add(this.dtNgayIn);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 415);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu tư vấn PTTT";
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Enabled = false;
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(258, 373);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(120, 33);
            this.cmdIn.TabIndex = 12;
            this.cmdIn.TabStop = false;
            this.cmdIn.Text = "In";
            this.cmdIn.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdIn.Click += new System.EventHandler(this.cmdIn_Click);
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(4, 12);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdList.Size = new System.Drawing.Size(374, 356);
            this.grdList.TabIndex = 569;
            this.grdList.TabStop = false;
            // 
            // dtNgayIn
            // 
            this.dtNgayIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayIn.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtNgayIn.Enabled = false;
            this.dtNgayIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayIn.Location = new System.Drawing.Point(58, 386);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(143, 20);
            this.dtNgayIn.TabIndex = 28;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.AutoSize = true;
            this.label26.Enabled = false;
            this.label26.Location = new System.Drawing.Point(9, 389);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 13);
            this.label26.TabIndex = 568;
            this.label26.Text = "Ngày in";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ucThongtinnguoibenh_doc_v11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(384, 375);
            this.panel5.TabIndex = 570;
            // 
            // ucThongtinnguoibenh_doc_v11
            // 
            this.ucThongtinnguoibenh_doc_v11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucThongtinnguoibenh_doc_v11.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_doc_v11.Name = "ucThongtinnguoibenh_doc_v11";
            this.ucThongtinnguoibenh_doc_v11.Size = new System.Drawing.Size(384, 375);
            this.ucThongtinnguoibenh_doc_v11.TabIndex = 1;
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
            this.autoKhoa.Location = new System.Drawing.Point(527, 33);
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
            this.autoKhoa.Size = new System.Drawing.Size(483, 21);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 616;
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
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(390, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 19);
            this.label6.TabIndex = 617;
            this.label6.Text = "Tại khoa";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkGiamdau2
            // 
            this.chkGiamdau2.AutoSize = true;
            this.chkGiamdau2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGiamdau2.ForeColor = System.Drawing.Color.Maroon;
            this.chkGiamdau2.Location = new System.Drawing.Point(761, 411);
            this.chkGiamdau2.Name = "chkGiamdau2";
            this.chkGiamdau2.Size = new System.Drawing.Size(264, 20);
            this.chkGiamdau2.TabIndex = 615;
            this.chkGiamdau2.Text = "Giảm đau bệnh nhân tự kiểm soát (PCA)";
            this.chkGiamdau2.UseVisualStyleBackColor = true;
            // 
            // chkGiamdau1
            // 
            this.chkGiamdau1.AutoSize = true;
            this.chkGiamdau1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGiamdau1.ForeColor = System.Drawing.Color.Maroon;
            this.chkGiamdau1.Location = new System.Drawing.Point(526, 411);
            this.chkGiamdau1.Name = "chkGiamdau1";
            this.chkGiamdau1.Size = new System.Drawing.Size(229, 20);
            this.chkGiamdau1.TabIndex = 614;
            this.chkGiamdau1.Text = "Giảm đau ngoài màng cứng (NMC)";
            this.chkGiamdau1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F);
            this.label15.ForeColor = System.Drawing.Color.Maroon;
            this.label15.Location = new System.Drawing.Point(390, 387);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(886, 23);
            this.label15.TabIndex = 613;
            this.label15.Text = "2. Thủ thuật giảm đau:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(390, 317);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(577, 23);
            this.label10.TabIndex = 612;
            this.label10.Text = "1. Thuốc/vật tư/ thủ thuật theo đặc thù bệnh lý:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkThuoc4
            // 
            this.chkThuoc4.AutoSize = true;
            this.chkThuoc4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc4.ForeColor = System.Drawing.Color.Blue;
            this.chkThuoc4.Location = new System.Drawing.Point(761, 365);
            this.chkThuoc4.Name = "chkThuoc4";
            this.chkThuoc4.Size = new System.Drawing.Size(248, 20);
            this.chkThuoc4.TabIndex = 611;
            this.chkThuoc4.Text = "Catheter huyết áp động mạch xâm lấn";
            this.chkThuoc4.UseVisualStyleBackColor = true;
            // 
            // chkThuoc2
            // 
            this.chkThuoc2.AutoSize = true;
            this.chkThuoc2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc2.ForeColor = System.Drawing.Color.Blue;
            this.chkThuoc2.Location = new System.Drawing.Point(761, 343);
            this.chkThuoc2.Name = "chkThuoc2";
            this.chkThuoc2.Size = new System.Drawing.Size(196, 20);
            this.chkThuoc2.TabIndex = 610;
            this.chkThuoc2.Text = "Catheter tĩnh mạch trung tâm";
            this.chkThuoc2.UseVisualStyleBackColor = true;
            // 
            // chkThuoc3
            // 
            this.chkThuoc3.AutoSize = true;
            this.chkThuoc3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc3.ForeColor = System.Drawing.Color.Blue;
            this.chkThuoc3.Location = new System.Drawing.Point(526, 365);
            this.chkThuoc3.Name = "chkThuoc3";
            this.chkThuoc3.Size = new System.Drawing.Size(129, 20);
            this.chkThuoc3.TabIndex = 609;
            this.chkThuoc3.Text = "Ống nội phế quản";
            this.chkThuoc3.UseVisualStyleBackColor = true;
            // 
            // chkThuoc1
            // 
            this.chkThuoc1.AutoSize = true;
            this.chkThuoc1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc1.ForeColor = System.Drawing.Color.Blue;
            this.chkThuoc1.Location = new System.Drawing.Point(526, 343);
            this.chkThuoc1.Name = "chkThuoc1";
            this.chkThuoc1.Size = new System.Drawing.Size(198, 20);
            this.chkThuoc1.TabIndex = 608;
            this.chkThuoc1.Text = "Thuốc Sugammadex (Bridion)";
            this.chkThuoc1.UseVisualStyleBackColor = true;
            // 
            // txtPPgiamdau
            // 
            this.txtPPgiamdau._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPPgiamdau._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPgiamdau._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPPgiamdau.AddValues = true;
            this.txtPPgiamdau.AllowMultiline = false;
            this.txtPPgiamdau.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPPgiamdau.AutoCompleteList")));
            this.txtPPgiamdau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPPgiamdau.buildShortcut = false;
            this.txtPPgiamdau.CaseSensitive = false;
            this.txtPPgiamdau.cmdDropDown = null;
            this.txtPPgiamdau.CompareNoID = true;
            this.txtPPgiamdau.DefaultCode = "-1";
            this.txtPPgiamdau.DefaultID = "-1";
            this.txtPPgiamdau.Drug_ID = null;
            this.txtPPgiamdau.ExtraWidth = 0;
            this.txtPPgiamdau.FillValueAfterSelect = false;
            this.txtPPgiamdau.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPgiamdau.LOAI_DANHMUC = "PHUONGPHAPGIAMDAU";
            this.txtPPgiamdau.Location = new System.Drawing.Point(526, 289);
            this.txtPPgiamdau.MaxHeight = 200;
            this.txtPPgiamdau.MinTypedCharacters = 2;
            this.txtPPgiamdau.MyCode = "-1";
            this.txtPPgiamdau.MyID = "-1";
            this.txtPPgiamdau.Name = "txtPPgiamdau";
            this.txtPPgiamdau.RaiseEvent = false;
            this.txtPPgiamdau.RaiseEventEnter = false;
            this.txtPPgiamdau.RaiseEventEnterWhenEmpty = false;
            this.txtPPgiamdau.SelectedIndex = -1;
            this.txtPPgiamdau.ShowCodeWithValue = false;
            this.txtPPgiamdau.Size = new System.Drawing.Size(842, 22);
            this.txtPPgiamdau.splitChar = '@';
            this.txtPPgiamdau.splitCharIDAndCode = '#';
            this.txtPPgiamdau.TabIndex = 606;
            this.txtPPgiamdau.TakeCode = false;
            this.txtPPgiamdau.txtMyCode = null;
            this.txtPPgiamdau.txtMyCode_Edit = null;
            this.txtPPgiamdau.txtMyID = null;
            this.txtPPgiamdau.txtMyID_Edit = null;
            this.txtPPgiamdau.txtMyName = null;
            this.txtPPgiamdau.txtMyName_Edit = null;
            this.txtPPgiamdau.txtNext = null;
            this.txtPPgiamdau.txtNext1 = null;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(390, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 23);
            this.label4.TabIndex = 607;
            this.label4.Text = "PP giảm đau dự kiến:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPhuongPhapVoCam
            // 
            this.txtPhuongPhapVoCam._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPhuongPhapVoCam._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapVoCam._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhuongPhapVoCam.AddValues = true;
            this.txtPhuongPhapVoCam.AllowMultiline = false;
            this.txtPhuongPhapVoCam.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhuongPhapVoCam.AutoCompleteList")));
            this.txtPhuongPhapVoCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhuongPhapVoCam.buildShortcut = false;
            this.txtPhuongPhapVoCam.CaseSensitive = false;
            this.txtPhuongPhapVoCam.cmdDropDown = null;
            this.txtPhuongPhapVoCam.CompareNoID = true;
            this.txtPhuongPhapVoCam.DefaultCode = "-1";
            this.txtPhuongPhapVoCam.DefaultID = "-1";
            this.txtPhuongPhapVoCam.Drug_ID = null;
            this.txtPhuongPhapVoCam.ExtraWidth = 0;
            this.txtPhuongPhapVoCam.FillValueAfterSelect = false;
            this.txtPhuongPhapVoCam.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuongPhapVoCam.LOAI_DANHMUC = "PHUONGPHAPVOCAM";
            this.txtPhuongPhapVoCam.Location = new System.Drawing.Point(526, 264);
            this.txtPhuongPhapVoCam.MaxHeight = 200;
            this.txtPhuongPhapVoCam.MinTypedCharacters = 2;
            this.txtPhuongPhapVoCam.MyCode = "-1";
            this.txtPhuongPhapVoCam.MyID = "-1";
            this.txtPhuongPhapVoCam.Name = "txtPhuongPhapVoCam";
            this.txtPhuongPhapVoCam.RaiseEvent = false;
            this.txtPhuongPhapVoCam.RaiseEventEnter = false;
            this.txtPhuongPhapVoCam.RaiseEventEnterWhenEmpty = false;
            this.txtPhuongPhapVoCam.SelectedIndex = -1;
            this.txtPhuongPhapVoCam.ShowCodeWithValue = false;
            this.txtPhuongPhapVoCam.Size = new System.Drawing.Size(841, 22);
            this.txtPhuongPhapVoCam.splitChar = '@';
            this.txtPhuongPhapVoCam.splitCharIDAndCode = '#';
            this.txtPhuongPhapVoCam.TabIndex = 604;
            this.txtPhuongPhapVoCam.TakeCode = false;
            this.txtPhuongPhapVoCam.txtMyCode = null;
            this.txtPhuongPhapVoCam.txtMyCode_Edit = null;
            this.txtPhuongPhapVoCam.txtMyID = null;
            this.txtPhuongPhapVoCam.txtMyID_Edit = null;
            this.txtPhuongPhapVoCam.txtMyName = null;
            this.txtPhuongPhapVoCam.txtMyName_Edit = null;
            this.txtPhuongPhapVoCam.txtNext = null;
            this.txtPhuongPhapVoCam.txtNext1 = null;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(390, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 19);
            this.label1.TabIndex = 605;
            this.label1.Text = "PP vô cảm dự kiến:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grd_bskhac
            // 
            this.grd_bskhac.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_bskhac.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grd_bskhac_DesignTimeLayout.LayoutString = resources.GetString("grd_bskhac_DesignTimeLayout.LayoutString");
            this.grd_bskhac.DesignTimeLayout = grd_bskhac_DesignTimeLayout;
            this.grd_bskhac.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_bskhac.GroupByBoxVisible = false;
            this.grd_bskhac.Location = new System.Drawing.Point(1017, 58);
            this.grd_bskhac.Name = "grd_bskhac";
            this.grd_bskhac.Size = new System.Drawing.Size(351, 190);
            this.grd_bskhac.TabIndex = 603;
            this.grd_bskhac.TabStop = false;
            // 
            // txtBsKhac
            // 
            this.txtBsKhac._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBsKhac._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsKhac._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBsKhac.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBsKhac.AutoCompleteList")));
            this.txtBsKhac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBsKhac.buildShortcut = false;
            this.txtBsKhac.CaseSensitive = false;
            this.txtBsKhac.CompareNoID = true;
            this.txtBsKhac.DefaultCode = "-1";
            this.txtBsKhac.DefaultID = "-1";
            this.txtBsKhac.DisplayType = 0;
            this.txtBsKhac.Drug_ID = null;
            this.txtBsKhac.ExtraWidth = 150;
            this.txtBsKhac.FillValueAfterSelect = false;
            this.txtBsKhac.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsKhac.Location = new System.Drawing.Point(823, 58);
            this.txtBsKhac.MaxHeight = 289;
            this.txtBsKhac.MinTypedCharacters = 2;
            this.txtBsKhac.MyCode = "-1";
            this.txtBsKhac.MyID = "-1";
            this.txtBsKhac.MyText = "";
            this.txtBsKhac.MyTextOnly = "";
            this.txtBsKhac.Name = "txtBsKhac";
            this.txtBsKhac.RaiseEvent = true;
            this.txtBsKhac.RaiseEventEnter = true;
            this.txtBsKhac.RaiseEventEnterWhenEmpty = true;
            this.txtBsKhac.SelectedIndex = -1;
            this.txtBsKhac.Size = new System.Drawing.Size(188, 22);
            this.txtBsKhac.splitChar = '@';
            this.txtBsKhac.splitCharIDAndCode = '#';
            this.txtBsKhac.TabIndex = 600;
            this.txtBsKhac.TakeCode = false;
            this.txtBsKhac.txtMyCode = null;
            this.txtBsKhac.txtMyCode_Edit = null;
            this.txtBsKhac.txtMyID = null;
            this.txtBsKhac.txtMyID_Edit = null;
            this.txtBsKhac.txtMyName = null;
            this.txtBsKhac.txtMyName_Edit = null;
            this.txtBsKhac.txtNext = null;
            // 
            // txtBsChinh
            // 
            this.txtBsChinh._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBsChinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsChinh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBsChinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBsChinh.AutoCompleteList")));
            this.txtBsChinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBsChinh.buildShortcut = false;
            this.txtBsChinh.CaseSensitive = false;
            this.txtBsChinh.CompareNoID = true;
            this.txtBsChinh.DefaultCode = "-1";
            this.txtBsChinh.DefaultID = "-1";
            this.txtBsChinh.DisplayType = 0;
            this.txtBsChinh.Drug_ID = null;
            this.txtBsChinh.ExtraWidth = 300;
            this.txtBsChinh.FillValueAfterSelect = false;
            this.txtBsChinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBsChinh.Location = new System.Drawing.Point(527, 58);
            this.txtBsChinh.MaxHeight = 289;
            this.txtBsChinh.MinTypedCharacters = 2;
            this.txtBsChinh.MyCode = "-1";
            this.txtBsChinh.MyID = "-1";
            this.txtBsChinh.MyText = "";
            this.txtBsChinh.MyTextOnly = "";
            this.txtBsChinh.Name = "txtBsChinh";
            this.txtBsChinh.RaiseEvent = true;
            this.txtBsChinh.RaiseEventEnter = true;
            this.txtBsChinh.RaiseEventEnterWhenEmpty = true;
            this.txtBsChinh.SelectedIndex = -1;
            this.txtBsChinh.Size = new System.Drawing.Size(176, 22);
            this.txtBsChinh.splitChar = '@';
            this.txtBsChinh.splitCharIDAndCode = '#';
            this.txtBsChinh.TabIndex = 599;
            this.txtBsChinh.TakeCode = false;
            this.txtBsChinh.txtMyCode = null;
            this.txtBsChinh.txtMyCode_Edit = null;
            this.txtBsChinh.txtMyID = null;
            this.txtBsChinh.txtMyID_Edit = null;
            this.txtBsChinh.txtMyName = null;
            this.txtBsChinh.txtMyName_Edit = null;
            this.txtBsChinh.txtNext = null;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(709, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 15);
            this.label13.TabIndex = 602;
            this.label13.Text = "BS tư vấn khác:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(390, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 18);
            this.label12.TabIndex = 601;
            this.label12.Text = "BS tư vấn chính:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtNgaytuvan
            // 
            this.dtNgaytuvan.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtNgaytuvan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgaytuvan.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtNgaytuvan.DropDownCalendar.Name = "";
            this.dtNgaytuvan.Enabled = false;
            this.dtNgaytuvan.Location = new System.Drawing.Point(527, 10);
            this.dtNgaytuvan.Name = "dtNgaytuvan";
            this.dtNgaytuvan.ShowUpDown = true;
            this.dtNgaytuvan.Size = new System.Drawing.Size(176, 20);
            this.dtNgaytuvan.TabIndex = 477;
            this.dtNgaytuvan.TabStop = false;
            this.dtNgaytuvan.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.Location = new System.Drawing.Point(390, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 19);
            this.label11.TabIndex = 476;
            this.label11.Text = "Thời gian tư vấn";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhichu
            // 
            this.txtGhichu.Location = new System.Drawing.Point(525, 593);
            this.txtGhichu.Multiline = true;
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhichu.Size = new System.Drawing.Size(842, 172);
            this.txtGhichu.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(397, 593);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 19);
            this.label9.TabIndex = 475;
            this.label9.Text = "Ghi chú:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRuiro
            // 
            this.txtRuiro.Location = new System.Drawing.Point(525, 461);
            this.txtRuiro.Multiline = true;
            this.txtRuiro.Name = "txtRuiro";
            this.txtRuiro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRuiro.Size = new System.Drawing.Size(842, 126);
            this.txtRuiro.TabIndex = 7;
            // 
            // txtChandoan
            // 
            this.txtChandoan.Location = new System.Drawing.Point(527, 85);
            this.txtChandoan.Multiline = true;
            this.txtChandoan.Name = "txtChandoan";
            this.txtChandoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChandoan.Size = new System.Drawing.Size(484, 163);
            this.txtChandoan.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(390, 433);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(886, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "3. Các rủi ro liên quan đến phương pháp vô cảm và giảm đau, thông tin về một số t" +
    "ai biến, biến chứng theo y văn đã nhận ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(390, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 19);
            this.label7.TabIndex = 2;
            this.label7.Text = "Chẩn đoán";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdXoa);
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Controls.Add(this.cmdThemmoi);
            this.panel3.Controls.Add(this.cmdSave);
            this.panel3.Controls.Add(this.cmdCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 790);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1410, 61);
            this.panel3.TabIndex = 1;
            // 
            // cmdXoa
            // 
            this.cmdXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdXoa.Enabled = false;
            this.cmdXoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdXoa.Location = new System.Drawing.Point(997, 16);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(120, 33);
            this.cmdXoa.TabIndex = 13;
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
            this.cmdExit.Location = new System.Drawing.Point(1248, 16);
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
            this.cmdThemmoi.Location = new System.Drawing.Point(869, 16);
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
            this.cmdSave.Location = new System.Drawing.Point(1122, 16);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 33);
            this.cmdSave.TabIndex = 11;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(1248, 16);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(120, 33);
            this.cmdCancel.TabIndex = 576;
            this.cmdCancel.TabStop = false;
            this.cmdCancel.Text = "Hủy (Esc)";
            this.cmdCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // frm_phieutuvanPTTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 851);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_phieutuvanPTTT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu tư vấn Phẫu thuật - Thủ thuật";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_phieutuvanPTTT_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_bskhac)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private Janus.Windows.GridEX.EditControls.EditBox txtChandoan;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdXoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdThemmoi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.GridEX.EditControls.EditBox txtGhichu;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgaytuvan;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.GridEX grd_bskhac;
        private AutoCompleteTextbox txtBsKhac;
        private AutoCompleteTextbox txtBsChinh;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private AutoCompleteTextbox_Danhmucchung txtPPgiamdau;
        private System.Windows.Forms.Label label4;
        private AutoCompleteTextbox_Danhmucchung txtPhuongPhapVoCam;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtRuiro;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkGiamdau2;
        private System.Windows.Forms.CheckBox chkGiamdau1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkThuoc4;
        private System.Windows.Forms.CheckBox chkThuoc2;
        private System.Windows.Forms.CheckBox chkThuoc3;
        private System.Windows.Forms.CheckBox chkThuoc1;
        private AutoCompleteTextbox autoKhoa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtNgayIn;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel5;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_doc_v1 ucThongtinnguoibenh_doc_v11;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdCancel;
    }
}