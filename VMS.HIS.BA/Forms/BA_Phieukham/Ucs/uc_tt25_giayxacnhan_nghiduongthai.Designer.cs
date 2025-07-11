
namespace VMS.HIS.UI.EMR.Ucs
{
    partial class uc_tt25_giayxacnhan_nghiduongthai
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_tt25_giayxacnhan_nghiduongthai));
            this.label62 = new System.Windows.Forms.Label();
            this.dtpNgaynghiDen = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label60 = new System.Windows.Forms.Label();
            this.dtpNgaynghiTu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label59 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.txt_chandoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayxacnhan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtNguoiXacnhan = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.autoTxt = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.nmrSongaynghiduongthai = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nmrSotuantuoithai = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDaidienDonvi = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.cmdTuSinh = new Janus.Windows.EditControls.UIButton();
            this.txtSoHoso = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSongaynghiduongthai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSotuantuoithai)).BeginInit();
            this.SuspendLayout();
            // 
            // label62
            // 
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(575, 82);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(38, 21);
            this.label62.TabIndex = 2567;
            this.label62.Text = "đến";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgaynghiDen
            // 
            this.dtpNgaynghiDen.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaynghiDen.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaynghiDen.DropDownCalendar.Name = "";
            this.dtpNgaynghiDen.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgaynghiDen.Enabled = false;
            this.dtpNgaynghiDen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynghiDen.Location = new System.Drawing.Point(619, 81);
            this.dtpNgaynghiDen.Name = "dtpNgaynghiDen";
            this.dtpNgaynghiDen.ShowUpDown = true;
            this.dtpNgaynghiDen.Size = new System.Drawing.Size(127, 22);
            this.dtpNgaynghiDen.TabIndex = 8;
            this.dtpNgaynghiDen.Value = new System.DateTime(2025, 5, 25, 0, 0, 0, 0);
            this.dtpNgaynghiDen.ValueChanged += new System.EventHandler(this.dtpNgaynghiDen_ValueChanged);
            // 
            // label60
            // 
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(330, 81);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(100, 21);
            this.label60.TabIndex = 2566;
            this.label60.Text = "- Ngày nghỉ từ:";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgaynghiTu
            // 
            this.dtpNgaynghiTu.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaynghiTu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaynghiTu.DropDownCalendar.Name = "";
            this.dtpNgaynghiTu.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgaynghiTu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaynghiTu.Location = new System.Drawing.Point(436, 80);
            this.dtpNgaynghiTu.Name = "dtpNgaynghiTu";
            this.dtpNgaynghiTu.ShowUpDown = true;
            this.dtpNgaynghiTu.Size = new System.Drawing.Size(127, 22);
            this.dtpNgaynghiTu.TabIndex = 7;
            this.dtpNgaynghiTu.Value = new System.DateTime(2025, 5, 25, 0, 0, 0, 0);
            this.dtpNgaynghiTu.ValueChanged += new System.EventHandler(this.dtpNgaynghiTu_ValueChanged);
            // 
            // label59
            // 
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(1, 81);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(204, 21);
            this.label59.TabIndex = 2564;
            this.label59.Text = "- Số ngày cần nghỉ để dưỡng thai:";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label100
            // 
            this.label100.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(1, 109);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(132, 102);
            this.label100.TabIndex = 2570;
            this.label100.Text = "- Chẩn đoán:";
            this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_chandoan
            // 
            this.txt_chandoan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_chandoan.Location = new System.Drawing.Point(139, 109);
            this.txt_chandoan.Multiline = true;
            this.txt_chandoan.Name = "txt_chandoan";
            this.txt_chandoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_chandoan.Size = new System.Drawing.Size(662, 186);
            this.txt_chandoan.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 21);
            this.label3.TabIndex = 2580;
            this.label3.Text = "- Ngày xác nhận:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayxacnhan
            // 
            this.dtpNgayxacnhan.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayxacnhan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayxacnhan.DropDownCalendar.Name = "";
            this.dtpNgayxacnhan.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayxacnhan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayxacnhan.Location = new System.Drawing.Point(139, 3);
            this.dtpNgayxacnhan.Name = "dtpNgayxacnhan";
            this.dtpNgayxacnhan.ShowUpDown = true;
            this.dtpNgayxacnhan.Size = new System.Drawing.Size(186, 22);
            this.dtpNgayxacnhan.TabIndex = 0;
            this.dtpNgayxacnhan.Value = new System.DateTime(2025, 5, 25, 0, 0, 0, 0);
            // 
            // txtNguoiXacnhan
            // 
            this.txtNguoiXacnhan._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNguoiXacnhan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiXacnhan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoiXacnhan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoiXacnhan.AutoCompleteList")));
            this.txtNguoiXacnhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoiXacnhan.buildShortcut = false;
            this.txtNguoiXacnhan.CaseSensitive = false;
            this.txtNguoiXacnhan.CompareNoID = true;
            this.txtNguoiXacnhan.DefaultCode = "-1";
            this.txtNguoiXacnhan.DefaultID = "-1";
            this.txtNguoiXacnhan.DisplayType = 0;
            this.txtNguoiXacnhan.Drug_ID = null;
            this.txtNguoiXacnhan.ExtraWidth = 0;
            this.txtNguoiXacnhan.FillValueAfterSelect = false;
            this.txtNguoiXacnhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiXacnhan.ForeColor = System.Drawing.Color.Red;
            this.txtNguoiXacnhan.Location = new System.Drawing.Point(139, 31);
            this.txtNguoiXacnhan.MaxHeight = 289;
            this.txtNguoiXacnhan.MinTypedCharacters = 2;
            this.txtNguoiXacnhan.MyCode = "-1";
            this.txtNguoiXacnhan.MyID = "-1";
            this.txtNguoiXacnhan.MyText = "";
            this.txtNguoiXacnhan.MyTextOnly = "";
            this.txtNguoiXacnhan.Name = "txtNguoiXacnhan";
            this.txtNguoiXacnhan.RaiseEvent = true;
            this.txtNguoiXacnhan.RaiseEventEnter = true;
            this.txtNguoiXacnhan.RaiseEventEnterWhenEmpty = true;
            this.txtNguoiXacnhan.SelectedIndex = -1;
            this.txtNguoiXacnhan.Size = new System.Drawing.Size(271, 22);
            this.txtNguoiXacnhan.splitChar = '@';
            this.txtNguoiXacnhan.splitCharIDAndCode = '#';
            this.txtNguoiXacnhan.TabIndex = 1;
            this.txtNguoiXacnhan.TakeCode = false;
            this.txtNguoiXacnhan.txtMyCode = null;
            this.txtNguoiXacnhan.txtMyCode_Edit = null;
            this.txtNguoiXacnhan.txtMyID = null;
            this.txtNguoiXacnhan.txtMyID_Edit = null;
            this.txtNguoiXacnhan.txtMyName = null;
            this.txtNguoiXacnhan.txtMyName_Edit = null;
            this.txtNguoiXacnhan.txtNext = null;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(1, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 21);
            this.label4.TabIndex = 2582;
            this.label4.Text = "- Người xác nhận:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoTxt
            // 
            this.autoTxt._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoTxt._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoTxt._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoTxt.AddValues = true;
            this.autoTxt.AllowMultiline = true;
            this.autoTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoTxt.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoTxt.AutoCompleteList")));
            this.autoTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoTxt.buildShortcut = false;
            this.autoTxt.CaseSensitive = false;
            this.autoTxt.cmdDropDown = null;
            this.autoTxt.CompareNoID = true;
            this.autoTxt.DefaultCode = "-1";
            this.autoTxt.DefaultID = "-1";
            this.autoTxt.Drug_ID = null;
            this.autoTxt.ExtraWidth = 0;
            this.autoTxt.FillValueAfterSelect = false;
            this.autoTxt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoTxt.LOAI_DANHMUC = "DANGDI_CACHNAM";
            this.autoTxt.Location = new System.Drawing.Point(768, 15);
            this.autoTxt.MaxHeight = 200;
            this.autoTxt.MinTypedCharacters = 2;
            this.autoTxt.Multiline = true;
            this.autoTxt.MyCode = "-1";
            this.autoTxt.MyID = "-1";
            this.autoTxt.Name = "autoTxt";
            this.autoTxt.RaiseEvent = false;
            this.autoTxt.RaiseEventEnter = false;
            this.autoTxt.RaiseEventEnterWhenEmpty = false;
            this.autoTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.autoTxt.SelectedIndex = -1;
            this.autoTxt.SetDefaultWhenInit = false;
            this.autoTxt.ShowCodeWithValue = false;
            this.autoTxt.Size = new System.Drawing.Size(33, 10);
            this.autoTxt.splitChar = '@';
            this.autoTxt.splitCharIDAndCode = '#';
            this.autoTxt.TabIndex = 2584;
            this.autoTxt.TakeCode = false;
            this.autoTxt.txtMyCode = null;
            this.autoTxt.txtMyCode_Edit = null;
            this.autoTxt.txtMyID = null;
            this.autoTxt.txtMyID_Edit = null;
            this.autoTxt.txtMyName = null;
            this.autoTxt.txtMyName_Edit = null;
            this.autoTxt.txtNext = null;
            this.autoTxt.txtNext1 = null;
            this.autoTxt.Visible = false;
            // 
            // nmrSongaynghiduongthai
            // 
            this.nmrSongaynghiduongthai.Location = new System.Drawing.Point(206, 82);
            this.nmrSongaynghiduongthai.Name = "nmrSongaynghiduongthai";
            this.nmrSongaynghiduongthai.Size = new System.Drawing.Size(87, 20);
            this.nmrSongaynghiduongthai.TabIndex = 6;
            this.nmrSongaynghiduongthai.ValueChanged += new System.EventHandler(this.nmrSongaynghiduongthai_ValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 21);
            this.label1.TabIndex = 2587;
            this.label1.Text = "- Tuần tuổi thai:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmrSotuantuoithai
            // 
            this.nmrSotuantuoithai.Location = new System.Drawing.Point(139, 55);
            this.nmrSotuantuoithai.Name = "nmrSotuantuoithai";
            this.nmrSotuantuoithai.Size = new System.Drawing.Size(120, 20);
            this.nmrSotuantuoithai.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(433, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 21);
            this.label5.TabIndex = 2594;
            this.label5.Text = "- Đại diện đơn vị:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDaidienDonvi
            // 
            this.txtDaidienDonvi._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtDaidienDonvi._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDaidienDonvi._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDaidienDonvi.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDaidienDonvi.AutoCompleteList")));
            this.txtDaidienDonvi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDaidienDonvi.buildShortcut = false;
            this.txtDaidienDonvi.CaseSensitive = false;
            this.txtDaidienDonvi.CompareNoID = true;
            this.txtDaidienDonvi.DefaultCode = "-1";
            this.txtDaidienDonvi.DefaultID = "-1";
            this.txtDaidienDonvi.DisplayType = 0;
            this.txtDaidienDonvi.Drug_ID = null;
            this.txtDaidienDonvi.ExtraWidth = 0;
            this.txtDaidienDonvi.FillValueAfterSelect = false;
            this.txtDaidienDonvi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDaidienDonvi.ForeColor = System.Drawing.Color.Red;
            this.txtDaidienDonvi.Location = new System.Drawing.Point(544, 32);
            this.txtDaidienDonvi.MaxHeight = 289;
            this.txtDaidienDonvi.MinTypedCharacters = 2;
            this.txtDaidienDonvi.MyCode = "-1";
            this.txtDaidienDonvi.MyID = "-1";
            this.txtDaidienDonvi.MyText = "";
            this.txtDaidienDonvi.MyTextOnly = "";
            this.txtDaidienDonvi.Name = "txtDaidienDonvi";
            this.txtDaidienDonvi.RaiseEvent = true;
            this.txtDaidienDonvi.RaiseEventEnter = true;
            this.txtDaidienDonvi.RaiseEventEnterWhenEmpty = true;
            this.txtDaidienDonvi.SelectedIndex = -1;
            this.txtDaidienDonvi.Size = new System.Drawing.Size(257, 22);
            this.txtDaidienDonvi.splitChar = '@';
            this.txtDaidienDonvi.splitCharIDAndCode = '#';
            this.txtDaidienDonvi.TabIndex = 2;
            this.txtDaidienDonvi.TakeCode = false;
            this.txtDaidienDonvi.txtMyCode = null;
            this.txtDaidienDonvi.txtMyCode_Edit = null;
            this.txtDaidienDonvi.txtMyID = null;
            this.txtDaidienDonvi.txtMyID_Edit = null;
            this.txtDaidienDonvi.txtMyName = null;
            this.txtDaidienDonvi.txtMyName_Edit = null;
            this.txtDaidienDonvi.txtNext = null;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(331, 5);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(79, 21);
            this.txtId.TabIndex = 2600;
            this.txtId.TabStop = false;
            // 
            // cmdTuSinh
            // 
            this.cmdTuSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTuSinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdTuSinh.Image")));
            this.cmdTuSinh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTuSinh.Location = new System.Drawing.Point(701, 2);
            this.cmdTuSinh.Name = "cmdTuSinh";
            this.cmdTuSinh.Size = new System.Drawing.Size(31, 27);
            this.cmdTuSinh.TabIndex = 2599;
            this.cmdTuSinh.TabStop = false;
            this.cmdTuSinh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // txtSoHoso
            // 
            this.txtSoHoso.BackColor = System.Drawing.Color.FloralWhite;
            this.txtSoHoso.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtSoHoso.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHoso.Location = new System.Drawing.Point(544, 6);
            this.txtSoHoso.Name = "txtSoHoso";
            this.txtSoHoso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSoHoso.Size = new System.Drawing.Size(151, 20);
            this.txtSoHoso.TabIndex = 2598;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(468, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 21);
            this.label6.TabIndex = 2597;
            this.label6.Text = "- Số hồ sơ:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uc_tt25_giayxacnhan_nghiduongthai
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.cmdTuSinh);
            this.Controls.Add(this.txtSoHoso);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDaidienDonvi);
            this.Controls.Add(this.nmrSotuantuoithai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmrSongaynghiduongthai);
            this.Controls.Add(this.autoTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNguoiXacnhan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpNgayxacnhan);
            this.Controls.Add(this.txt_chandoan);
            this.Controls.Add(this.label100);
            this.Controls.Add(this.label62);
            this.Controls.Add(this.dtpNgaynghiDen);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.dtpNgaynghiTu);
            this.Controls.Add(this.label59);
            this.Name = "uc_tt25_giayxacnhan_nghiduongthai";
            this.Size = new System.Drawing.Size(824, 312);
            ((System.ComponentModel.ISupportInitialize)(this.nmrSongaynghiduongthai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSotuantuoithai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label62;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaynghiDen;
        private System.Windows.Forms.Label label60;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaynghiTu;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label100;
        private Janus.Windows.GridEX.EditControls.EditBox txt_chandoan;
        private System.Windows.Forms.Label label3;
        private VNS.HIS.UCs.AutoCompleteTextbox txtNguoiXacnhan;
        private System.Windows.Forms.Label label4;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung autoTxt;
        private System.Windows.Forms.NumericUpDown nmrSongaynghiduongthai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmrSotuantuoithai;
        private System.Windows.Forms.Label label5;
        private VNS.HIS.UCs.AutoCompleteTextbox txtDaidienDonvi;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpNgayxacnhan;
        private System.Windows.Forms.TextBox txtId;
        private Janus.Windows.EditControls.UIButton cmdTuSinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoHoso;
        private System.Windows.Forms.Label label6;
    }
}
