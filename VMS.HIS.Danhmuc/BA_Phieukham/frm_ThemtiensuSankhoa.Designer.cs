
using VMS.EMR.PHIEUKHAM.Ucs;

namespace VMS.EMR.PHIEUKHAM
{
    partial class frm_ThemtiensuSankhoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ThemtiensuSankhoa));
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.autoTxt = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.panel3 = new System.Windows.Forms.Panel();
            this.optConhiensong = new System.Windows.Forms.RadioButton();
            this.optThaichetluu = new System.Windows.Forms.RadioButton();
            this.optChuangoaitucung = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optNao = new System.Windows.Forms.RadioButton();
            this.optHut = new System.Windows.Forms.RadioButton();
            this.optSay = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optDethieuthang = new System.Windows.Forms.RadioButton();
            this.optDeduthang = new System.Windows.Forms.RadioButton();
            this.txtTuoithai = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoiketthucthainghen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThongtintre = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhuongphapdeCachthucsinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.optTaibienhausan = new System.Windows.Forms.CheckBox();
            this.chkCovac = new System.Windows.Forms.CheckBox();
            this.txtDienbienthai = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label23 = new System.Windows.Forms.Label();
            this.dtpNam = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label38 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(119, 135);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 15);
            this.lblMsg.TabIndex = 168;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(416, 243);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(129, 35);
            this.cmdSave.TabIndex = 164;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(551, 243);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 35);
            this.cmdExit.TabIndex = 165;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
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
            this.autoTxt.Location = new System.Drawing.Point(118, 136);
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
            this.autoTxt.ShowCodeWithValue = false;
            this.autoTxt.Size = new System.Drawing.Size(0, 10);
            this.autoTxt.splitChar = '@';
            this.autoTxt.splitCharIDAndCode = '#';
            this.autoTxt.TabIndex = 186;
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.Controls.Add(this.optConhiensong);
            this.panel3.Controls.Add(this.optThaichetluu);
            this.panel3.Controls.Add(this.optChuangoaitucung);
            this.panel3.Location = new System.Drawing.Point(169, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(328, 24);
            this.panel3.TabIndex = 185;
            // 
            // optConhiensong
            // 
            this.optConhiensong.AutoSize = true;
            this.optConhiensong.Checked = true;
            this.optConhiensong.Location = new System.Drawing.Point(232, 4);
            this.optConhiensong.Name = "optConhiensong";
            this.optConhiensong.Size = new System.Drawing.Size(93, 17);
            this.optConhiensong.TabIndex = 12;
            this.optConhiensong.TabStop = true;
            this.optConhiensong.Text = "Con hiện sống";
            this.optConhiensong.UseVisualStyleBackColor = true;
            // 
            // optThaichetluu
            // 
            this.optThaichetluu.AutoSize = true;
            this.optThaichetluu.Location = new System.Drawing.Point(127, 4);
            this.optThaichetluu.Name = "optThaichetluu";
            this.optThaichetluu.Size = new System.Drawing.Size(87, 17);
            this.optThaichetluu.TabIndex = 11;
            this.optThaichetluu.Text = "Thai chết lưu";
            this.optThaichetluu.UseVisualStyleBackColor = true;
            // 
            // optChuangoaitucung
            // 
            this.optChuangoaitucung.AutoSize = true;
            this.optChuangoaitucung.Location = new System.Drawing.Point(3, 4);
            this.optChuangoaitucung.Name = "optChuangoaitucung";
            this.optChuangoaitucung.Size = new System.Drawing.Size(118, 17);
            this.optChuangoaitucung.TabIndex = 10;
            this.optChuangoaitucung.Text = "Chửa ngoài tử cung";
            this.optChuangoaitucung.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.optNao);
            this.panel2.Controls.Add(this.optHut);
            this.panel2.Controls.Add(this.optSay);
            this.panel2.Location = new System.Drawing.Point(375, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(163, 24);
            this.panel2.TabIndex = 184;
            // 
            // optNao
            // 
            this.optNao.AutoSize = true;
            this.optNao.Location = new System.Drawing.Point(100, 4);
            this.optNao.Name = "optNao";
            this.optNao.Size = new System.Drawing.Size(45, 17);
            this.optNao.TabIndex = 6;
            this.optNao.TabStop = true;
            this.optNao.Text = "Nạo";
            this.optNao.UseVisualStyleBackColor = true;
            // 
            // optHut
            // 
            this.optHut.AutoSize = true;
            this.optHut.Location = new System.Drawing.Point(52, 4);
            this.optHut.Name = "optHut";
            this.optHut.Size = new System.Drawing.Size(42, 17);
            this.optHut.TabIndex = 5;
            this.optHut.TabStop = true;
            this.optHut.Text = "Hút";
            this.optHut.UseVisualStyleBackColor = true;
            // 
            // optSay
            // 
            this.optSay.AutoSize = true;
            this.optSay.Location = new System.Drawing.Point(3, 4);
            this.optSay.Name = "optSay";
            this.optSay.Size = new System.Drawing.Size(43, 17);
            this.optSay.TabIndex = 4;
            this.optSay.TabStop = true;
            this.optSay.Text = "Sẩy";
            this.optSay.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.optDethieuthang);
            this.panel1.Controls.Add(this.optDeduthang);
            this.panel1.Location = new System.Drawing.Point(169, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 24);
            this.panel1.TabIndex = 183;
            // 
            // optDethieuthang
            // 
            this.optDethieuthang.AutoSize = true;
            this.optDethieuthang.Location = new System.Drawing.Point(94, 4);
            this.optDethieuthang.Name = "optDethieuthang";
            this.optDethieuthang.Size = new System.Drawing.Size(95, 17);
            this.optDethieuthang.TabIndex = 1;
            this.optDethieuthang.Text = "Đẻ thiếu tháng";
            this.optDethieuthang.UseVisualStyleBackColor = true;
            // 
            // optDeduthang
            // 
            this.optDeduthang.AutoSize = true;
            this.optDeduthang.Checked = true;
            this.optDeduthang.Location = new System.Drawing.Point(3, 4);
            this.optDeduthang.Name = "optDeduthang";
            this.optDeduthang.Size = new System.Drawing.Size(85, 17);
            this.optDeduthang.TabIndex = 0;
            this.optDeduthang.TabStop = true;
            this.optDeduthang.Text = "Đẻ đủ tháng";
            this.optDeduthang.UseVisualStyleBackColor = true;
            // 
            // txtTuoithai
            // 
            this.txtTuoithai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTuoithai.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtTuoithai.Location = new System.Drawing.Point(169, 93);
            this.txtTuoithai.Name = "txtTuoithai";
            this.txtTuoithai.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTuoithai.Size = new System.Drawing.Size(76, 18);
            this.txtTuoithai.TabIndex = 173;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 21);
            this.label4.TabIndex = 182;
            this.label4.Text = "- Tuổi thai :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoiketthucthainghen
            // 
            this.txtNoiketthucthainghen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiketthucthainghen.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtNoiketthucthainghen.Location = new System.Drawing.Point(169, 71);
            this.txtNoiketthucthainghen.Name = "txtNoiketthucthainghen";
            this.txtNoiketthucthainghen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiketthucthainghen.Size = new System.Drawing.Size(511, 18);
            this.txtNoiketthucthainghen.TabIndex = 172;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.TabIndex = 181;
            this.label1.Text = "- Nơi kết thúc thai nghén:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtThongtintre
            // 
            this.txtThongtintre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThongtintre.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtThongtintre.Location = new System.Drawing.Point(169, 136);
            this.txtThongtintre.Name = "txtThongtintre";
            this.txtThongtintre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtThongtintre.Size = new System.Drawing.Size(511, 18);
            this.txtThongtintre.TabIndex = 176;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 21);
            this.label3.TabIndex = 180;
            this.label3.Text = "- Thông tin trẻ:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPhuongphapdeCachthucsinh
            // 
            this.txtPhuongphapdeCachthucsinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhuongphapdeCachthucsinh.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtPhuongphapdeCachthucsinh.Location = new System.Drawing.Point(169, 115);
            this.txtPhuongphapdeCachthucsinh.Name = "txtPhuongphapdeCachthucsinh";
            this.txtPhuongphapdeCachthucsinh.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPhuongphapdeCachthucsinh.Size = new System.Drawing.Size(511, 18);
            this.txtPhuongphapdeCachthucsinh.TabIndex = 175;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 21);
            this.label2.TabIndex = 179;
            this.label2.Text = "- Cách thức sinh:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optTaibienhausan
            // 
            this.optTaibienhausan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.optTaibienhausan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.optTaibienhausan.Font = new System.Drawing.Font("Tahoma", 9F);
            this.optTaibienhausan.Location = new System.Drawing.Point(503, 44);
            this.optTaibienhausan.Name = "optTaibienhausan";
            this.optTaibienhausan.Size = new System.Drawing.Size(121, 19);
            this.optTaibienhausan.TabIndex = 171;
            this.optTaibienhausan.Text = "Tai biến, hậu sản";
            this.optTaibienhausan.UseVisualStyleBackColor = true;
            // 
            // chkCovac
            // 
            this.chkCovac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCovac.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCovac.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkCovac.Location = new System.Drawing.Point(544, 16);
            this.chkCovac.Name = "chkCovac";
            this.chkCovac.Size = new System.Drawing.Size(70, 19);
            this.chkCovac.TabIndex = 170;
            this.chkCovac.Text = "Co vac";
            this.chkCovac.UseVisualStyleBackColor = true;
            // 
            // txtDienbienthai
            // 
            this.txtDienbienthai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienbienthai.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
            this.txtDienbienthai.Location = new System.Drawing.Point(353, 93);
            this.txtDienbienthai.Name = "txtDienbienthai";
            this.txtDienbienthai.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDienbienthai.Size = new System.Drawing.Size(327, 18);
            this.txtDienbienthai.TabIndex = 174;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(251, 91);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(108, 21);
            this.label23.TabIndex = 178;
            this.label23.Text = "- Diễn biến thai :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNam
            // 
            this.dtpNam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpNam.CustomFormat = "yyyy";
            this.dtpNam.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNam.DropDownCalendar.Name = "";
            this.dtpNam.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNam.Location = new System.Drawing.Point(50, 12);
            this.dtpNam.Name = "dtpNam";
            this.dtpNam.ReadOnly = true;
            this.dtpNam.ShowUpDown = true;
            this.dtpNam.Size = new System.Drawing.Size(113, 22);
            this.dtpNam.TabIndex = 169;
            this.dtpNam.Value = new System.DateTime(2025, 5, 25, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(9, 13);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 21);
            this.label38.TabIndex = 177;
            this.label38.Text = "Năm:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frm_ThemtiensuSankhoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 290);
            this.Controls.Add(this.autoTxt);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtTuoithai);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNoiketthucthainghen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThongtintre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPhuongphapdeCachthucsinh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.optTaibienhausan);
            this.Controls.Add(this.chkCovac);
            this.Controls.Add(this.txtDienbienthai);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.dtpNam);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ThemtiensuSankhoa";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiền sử sản khoa";
            this.Load += new System.EventHandler(this.frm_ThemtiensuSankhoa_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lblMsg;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung autoTxt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton optConhiensong;
        private System.Windows.Forms.RadioButton optThaichetluu;
        private System.Windows.Forms.RadioButton optChuangoaitucung;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optNao;
        private System.Windows.Forms.RadioButton optHut;
        private System.Windows.Forms.RadioButton optSay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton optDethieuthang;
        private System.Windows.Forms.RadioButton optDeduthang;
        private Janus.Windows.GridEX.EditControls.EditBox txtTuoithai;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtNoiketthucthainghen;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtThongtintre;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtPhuongphapdeCachthucsinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox optTaibienhausan;
        private System.Windows.Forms.CheckBox chkCovac;
        private Janus.Windows.GridEX.EditControls.EditBox txtDienbienthai;
        private System.Windows.Forms.Label label23;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNam;
        private System.Windows.Forms.Label label38;
    }
}