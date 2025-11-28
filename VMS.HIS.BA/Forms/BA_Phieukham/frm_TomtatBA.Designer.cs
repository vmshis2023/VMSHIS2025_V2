using VNS.HIS.UCs;
using VNS.HIS.UI.Forms.Dungchung.UCs;

namespace VMS.HIS.UI.EMR
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
            this.lbk_chandoan_ravien = new System.Windows.Forms.LinkLabel();
            this.lnk_chandoanvaovien = new System.Windows.Forms.LinkLabel();
            this.lnk_kq_cls = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlKetquadieutriravien = new System.Windows.Forms.Panel();
            this.chkTTRVKhoi = new System.Windows.Forms.CheckBox();
            this.chkTTRVDoGiam = new System.Windows.Forms.CheckBox();
            this.chkTTRVKhongThayDoi = new System.Windows.Forms.CheckBox();
            this.chkTTRVNangHon = new System.Windows.Forms.CheckBox();
            this.chkTTRVTuVong = new System.Windows.Forms.CheckBox();
            this.txtGDBV = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2();
            this.txtBSDieuTri = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label245 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtHuongdieutri = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkPTTT = new System.Windows.Forms.CheckBox();
            this.txtPTTTmota = new System.Windows.Forms.TextBox();
            this.txtNoikhoamota = new System.Windows.Forms.TextBox();
            this.chkNoikhoa = new System.Windows.Forms.CheckBox();
            this.dtNgayTTBA = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtDauhieulamsang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTiensubenh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label54 = new System.Windows.Forms.Label();
            this.txt_chandoan_ravien = new System.Windows.Forms.TextBox();
            this.txtTomtatCLS = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtquatrinhbenhly = new Janus.Windows.GridEX.EditControls.EditBox();
            this.autoLydovv = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPPdieutri = new Janus.Windows.GridEX.EditControls.EditBox();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.dtpNgayRavien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpNgayNhapvien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_chandoanvaovien = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtB_Khac = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtB_XetNghiem = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtB_SieuAm = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtB_CTScanner = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtB_Xquang = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.label233 = new System.Windows.Forms.Label();
            this.label234 = new System.Windows.Forms.Label();
            this.label235 = new System.Windows.Forms.Label();
            this.label236 = new System.Windows.Forms.Label();
            this.label237 = new System.Windows.Forms.Label();
            this.txtB_Tongso = new System.Windows.Forms.TextBox();
            this.label238 = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.label240 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label243 = new System.Windows.Forms.Label();
            this.label242 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txttinhtrangravienMota = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTruongkhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtNguoiNhanHoSo = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtNguoiGiaoHoSo = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdXoa = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdThemmoi = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.txtTinhtrangRavien = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cmdTuSinh = new Janus.Windows.EditControls.UIButton();
            this.txtSoHoso = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnlKetquadieutriravien.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.cmdTuSinh);
            this.panel1.Controls.Add(this.txtSoHoso);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbk_chandoan_ravien);
            this.panel1.Controls.Add(this.lnk_chandoanvaovien);
            this.panel1.Controls.Add(this.lnk_kq_cls);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.chkPreview);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.pnlKetquadieutriravien);
            this.panel1.Controls.Add(this.txtGDBV);
            this.panel1.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.panel1.Controls.Add(this.txtBSDieuTri);
            this.panel1.Controls.Add(this.label245);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtHuongdieutri);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.chkPTTT);
            this.panel1.Controls.Add(this.txtPTTTmota);
            this.panel1.Controls.Add(this.txtNoikhoamota);
            this.panel1.Controls.Add(this.chkNoikhoa);
            this.panel1.Controls.Add(this.dtNgayTTBA);
            this.panel1.Controls.Add(this.txtDauhieulamsang);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtTiensubenh);
            this.panel1.Controls.Add(this.label54);
            this.panel1.Controls.Add(this.txt_chandoan_ravien);
            this.panel1.Controls.Add(this.txtTomtatCLS);
            this.panel1.Controls.Add(this.txtquatrinhbenhly);
            this.panel1.Controls.Add(this.autoLydovv);
            this.panel1.Controls.Add(this.txtPPdieutri);
            this.panel1.Controls.Add(this.autoKhoa);
            this.panel1.Controls.Add(this.dtpNgayRavien);
            this.panel1.Controls.Add(this.dtpNgayNhapvien);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txt_chandoanvaovien);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1442, 734);
            this.panel1.TabIndex = 0;
            // 
            // lbk_chandoan_ravien
            // 
            this.lbk_chandoan_ravien.Font = new System.Drawing.Font("Arial", 9F);
            this.lbk_chandoan_ravien.Location = new System.Drawing.Point(428, 52);
            this.lbk_chandoan_ravien.Name = "lbk_chandoan_ravien";
            this.lbk_chandoan_ravien.Size = new System.Drawing.Size(181, 21);
            this.lbk_chandoan_ravien.TabIndex = 776;
            this.lbk_chandoan_ravien.TabStop = true;
            this.lbk_chandoan_ravien.Text = "Chẩn đoán ra viện";
            this.lbk_chandoan_ravien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbk_chandoan_ravien.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbk_chandoan_ravien_LinkClicked);
            // 
            // lnk_chandoanvaovien
            // 
            this.lnk_chandoanvaovien.Font = new System.Drawing.Font("Arial", 9F);
            this.lnk_chandoanvaovien.Location = new System.Drawing.Point(431, 29);
            this.lnk_chandoanvaovien.Name = "lnk_chandoanvaovien";
            this.lnk_chandoanvaovien.Size = new System.Drawing.Size(181, 21);
            this.lnk_chandoanvaovien.TabIndex = 775;
            this.lnk_chandoanvaovien.TabStop = true;
            this.lnk_chandoanvaovien.Text = "Chẩn đoán vào viện";
            this.lnk_chandoanvaovien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnk_chandoanvaovien.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_chandoanvaovien_LinkClicked);
            // 
            // lnk_kq_cls
            // 
            this.lnk_kq_cls.Font = new System.Drawing.Font("Arial", 9F);
            this.lnk_kq_cls.Location = new System.Drawing.Point(431, 369);
            this.lnk_kq_cls.Name = "lnk_kq_cls";
            this.lnk_kq_cls.Size = new System.Drawing.Size(181, 80);
            this.lnk_kq_cls.TabIndex = 774;
            this.lnk_kq_cls.TabStop = true;
            this.lnk_kq_cls.Text = "Tóm tắt kết quả xét nghiệm, cận lâm sàng có giá trị chẩn đoán: ";
            this.lnk_kq_cls.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnk_kq_cls.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_kq_cls_LinkClicked);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(431, 81);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(378, 19);
            this.label17.TabIndex = 773;
            this.label17.Text = "III. TÓM TẮT QUÁ TRÌNH ĐIỀU TRỊ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.AutoSize = true;
            this.chkPreview.BackColor = System.Drawing.Color.Transparent;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Location = new System.Drawing.Point(615, 705);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(108, 17);
            this.chkPreview.TabIndex = 23;
            this.chkPreview.TabStop = false;
            this.chkPreview.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkPreview.Text = "Xem trước khi in?";
            this.chkPreview.UseVisualStyleBackColor = false;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(431, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(378, 19);
            this.label16.TabIndex = 772;
            this.label16.Text = "II. CHẨN ĐOÁN (Tên bệnh và mã ICD đính kèm):";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKetquadieutriravien
            // 
            this.pnlKetquadieutriravien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKetquadieutriravien.Controls.Add(this.chkTTRVKhoi);
            this.pnlKetquadieutriravien.Controls.Add(this.chkTTRVDoGiam);
            this.pnlKetquadieutriravien.Controls.Add(this.chkTTRVKhongThayDoi);
            this.pnlKetquadieutriravien.Controls.Add(this.chkTTRVNangHon);
            this.pnlKetquadieutriravien.Controls.Add(this.chkTTRVTuVong);
            this.pnlKetquadieutriravien.Location = new System.Drawing.Point(615, 550);
            this.pnlKetquadieutriravien.Name = "pnlKetquadieutriravien";
            this.pnlKetquadieutriravien.Size = new System.Drawing.Size(815, 29);
            this.pnlKetquadieutriravien.TabIndex = 771;
            // 
            // chkTTRVKhoi
            // 
            this.chkTTRVKhoi.BackColor = System.Drawing.Color.Transparent;
            this.chkTTRVKhoi.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTTRVKhoi.Enabled = false;
            this.chkTTRVKhoi.Location = new System.Drawing.Point(7, 6);
            this.chkTTRVKhoi.Name = "chkTTRVKhoi";
            this.chkTTRVKhoi.Size = new System.Drawing.Size(71, 19);
            this.chkTTRVKhoi.TabIndex = 20;
            this.chkTTRVKhoi.TabStop = false;
            this.chkTTRVKhoi.Tag = "1";
            this.chkTTRVKhoi.Text = "1.Khỏi                 ";
            this.chkTTRVKhoi.UseVisualStyleBackColor = false;
            // 
            // chkTTRVDoGiam
            // 
            this.chkTTRVDoGiam.BackColor = System.Drawing.Color.Transparent;
            this.chkTTRVDoGiam.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTTRVDoGiam.Enabled = false;
            this.chkTTRVDoGiam.Location = new System.Drawing.Point(139, 6);
            this.chkTTRVDoGiam.Name = "chkTTRVDoGiam";
            this.chkTTRVDoGiam.Size = new System.Drawing.Size(77, 19);
            this.chkTTRVDoGiam.TabIndex = 21;
            this.chkTTRVDoGiam.TabStop = false;
            this.chkTTRVDoGiam.Tag = "2";
            this.chkTTRVDoGiam.Text = "2.Đỡ, giảm          ";
            this.chkTTRVDoGiam.UseVisualStyleBackColor = false;
            // 
            // chkTTRVKhongThayDoi
            // 
            this.chkTTRVKhongThayDoi.BackColor = System.Drawing.Color.Transparent;
            this.chkTTRVKhongThayDoi.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTTRVKhongThayDoi.Enabled = false;
            this.chkTTRVKhongThayDoi.Location = new System.Drawing.Point(298, 6);
            this.chkTTRVKhongThayDoi.Name = "chkTTRVKhongThayDoi";
            this.chkTTRVKhongThayDoi.Size = new System.Drawing.Size(109, 19);
            this.chkTTRVKhongThayDoi.TabIndex = 22;
            this.chkTTRVKhongThayDoi.TabStop = false;
            this.chkTTRVKhongThayDoi.Tag = "3";
            this.chkTTRVKhongThayDoi.Text = "3.Không thay đổi";
            this.chkTTRVKhongThayDoi.UseVisualStyleBackColor = false;
            // 
            // chkTTRVNangHon
            // 
            this.chkTTRVNangHon.BackColor = System.Drawing.Color.Transparent;
            this.chkTTRVNangHon.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTTRVNangHon.Enabled = false;
            this.chkTTRVNangHon.Location = new System.Drawing.Point(466, 6);
            this.chkTTRVNangHon.Name = "chkTTRVNangHon";
            this.chkTTRVNangHon.Size = new System.Drawing.Size(91, 19);
            this.chkTTRVNangHon.TabIndex = 23;
            this.chkTTRVNangHon.TabStop = false;
            this.chkTTRVNangHon.Tag = "4";
            this.chkTTRVNangHon.Text = "4.Nặng hơn";
            this.chkTTRVNangHon.UseVisualStyleBackColor = false;
            // 
            // chkTTRVTuVong
            // 
            this.chkTTRVTuVong.BackColor = System.Drawing.Color.Transparent;
            this.chkTTRVTuVong.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTTRVTuVong.Enabled = false;
            this.chkTTRVTuVong.Location = new System.Drawing.Point(592, 6);
            this.chkTTRVTuVong.Name = "chkTTRVTuVong";
            this.chkTTRVTuVong.Size = new System.Drawing.Size(91, 19);
            this.chkTTRVTuVong.TabIndex = 24;
            this.chkTTRVTuVong.TabStop = false;
            this.chkTTRVTuVong.Tag = "5";
            this.chkTTRVTuVong.Text = "5.Tử vong  ";
            this.chkTTRVTuVong.UseVisualStyleBackColor = false;
            // 
            // txtGDBV
            // 
            this.txtGDBV._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtGDBV._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDBV._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGDBV.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtGDBV.AutoCompleteList")));
            this.txtGDBV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGDBV.buildShortcut = false;
            this.txtGDBV.CaseSensitive = false;
            this.txtGDBV.CompareNoID = true;
            this.txtGDBV.DefaultCode = "-1";
            this.txtGDBV.DefaultID = "-1";
            this.txtGDBV.DisplayType = 0;
            this.txtGDBV.Drug_ID = null;
            this.txtGDBV.ExtraWidth = 0;
            this.txtGDBV.FillValueAfterSelect = false;
            this.txtGDBV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDBV.ForeColor = System.Drawing.Color.Red;
            this.txtGDBV.Location = new System.Drawing.Point(615, 674);
            this.txtGDBV.MaxHeight = 289;
            this.txtGDBV.MinTypedCharacters = 2;
            this.txtGDBV.MyCode = "-1";
            this.txtGDBV.MyID = "-1";
            this.txtGDBV.MyText = "";
            this.txtGDBV.MyTextOnly = "";
            this.txtGDBV.Name = "txtGDBV";
            this.txtGDBV.RaiseEvent = true;
            this.txtGDBV.RaiseEventEnter = true;
            this.txtGDBV.RaiseEventEnterWhenEmpty = true;
            this.txtGDBV.SelectedIndex = -1;
            this.txtGDBV.Size = new System.Drawing.Size(364, 22);
            this.txtGDBV.splitChar = '@';
            this.txtGDBV.splitCharIDAndCode = '#';
            this.txtGDBV.TabIndex = 31;
            this.txtGDBV.TakeCode = false;
            this.txtGDBV.txtMyCode = null;
            this.txtGDBV.txtMyCode_Edit = null;
            this.txtGDBV.txtMyID = null;
            this.txtGDBV.txtMyID_Edit = null;
            this.txtGDBV.txtMyName = null;
            this.txtGDBV.txtMyName_Edit = null;
            this.txtGDBV.txtNext = null;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(3, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(410, 723);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 759;
            // 
            // txtBSDieuTri
            // 
            this.txtBSDieuTri._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBSDieuTri._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBSDieuTri._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBSDieuTri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBSDieuTri.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBSDieuTri.AutoCompleteList")));
            this.txtBSDieuTri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBSDieuTri.buildShortcut = false;
            this.txtBSDieuTri.CaseSensitive = false;
            this.txtBSDieuTri.CompareNoID = true;
            this.txtBSDieuTri.DefaultCode = "-1";
            this.txtBSDieuTri.DefaultID = "-1";
            this.txtBSDieuTri.DisplayType = 0;
            this.txtBSDieuTri.Drug_ID = null;
            this.txtBSDieuTri.ExtraWidth = 0;
            this.txtBSDieuTri.FillValueAfterSelect = false;
            this.txtBSDieuTri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBSDieuTri.Location = new System.Drawing.Point(1111, 111);
            this.txtBSDieuTri.MaxHeight = 289;
            this.txtBSDieuTri.MinTypedCharacters = 2;
            this.txtBSDieuTri.MyCode = "-1";
            this.txtBSDieuTri.MyID = "-1";
            this.txtBSDieuTri.MyText = "";
            this.txtBSDieuTri.MyTextOnly = "";
            this.txtBSDieuTri.Name = "txtBSDieuTri";
            this.txtBSDieuTri.RaiseEvent = true;
            this.txtBSDieuTri.RaiseEventEnter = true;
            this.txtBSDieuTri.RaiseEventEnterWhenEmpty = true;
            this.txtBSDieuTri.SelectedIndex = -1;
            this.txtBSDieuTri.Size = new System.Drawing.Size(319, 21);
            this.txtBSDieuTri.splitChar = '@';
            this.txtBSDieuTri.splitCharIDAndCode = '#';
            this.txtBSDieuTri.TabIndex = 6;
            this.txtBSDieuTri.TabStop = false;
            this.txtBSDieuTri.TakeCode = false;
            this.txtBSDieuTri.txtMyCode = null;
            this.txtBSDieuTri.txtMyCode_Edit = null;
            this.txtBSDieuTri.txtMyID = null;
            this.txtBSDieuTri.txtMyID_Edit = null;
            this.txtBSDieuTri.txtMyName = null;
            this.txtBSDieuTri.txtMyName_Edit = null;
            this.txtBSDieuTri.txtNext = null;
            // 
            // label245
            // 
            this.label245.AutoSize = true;
            this.label245.Font = new System.Drawing.Font("Arial", 9F);
            this.label245.ForeColor = System.Drawing.Color.Black;
            this.label245.Location = new System.Drawing.Point(1024, 114);
            this.label245.Name = "label245";
            this.label245.Size = new System.Drawing.Size(81, 15);
            this.label245.TabIndex = 4;
            this.label245.Text = "Bác sĩ điều trị";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F);
            this.label13.Location = new System.Drawing.Point(462, 550);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(153, 29);
            this.label13.TabIndex = 590;
            this.label13.Text = "Tình trạng ra viện";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHuongdieutri
            // 
            this.txtHuongdieutri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHuongdieutri.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtHuongdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHuongdieutri.Location = new System.Drawing.Point(615, 585);
            this.txtHuongdieutri.MaxLength = 4000;
            this.txtHuongdieutri.Multiline = true;
            this.txtHuongdieutri.Name = "txtHuongdieutri";
            this.txtHuongdieutri.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHuongdieutri.Size = new System.Drawing.Size(815, 85);
            this.txtHuongdieutri.TabIndex = 30;
            this.txtHuongdieutri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(462, 585);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 68);
            this.label12.TabIndex = 587;
            this.label12.Text = "Hướng điều trị và các chế độ tiếp theo:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPTTT
            // 
            this.chkPTTT.AutoSize = true;
            this.chkPTTT.BackColor = System.Drawing.Color.Transparent;
            this.chkPTTT.Location = new System.Drawing.Point(985, 453);
            this.chkPTTT.Name = "chkPTTT";
            this.chkPTTT.Size = new System.Drawing.Size(126, 17);
            this.chkPTTT.TabIndex = 16;
            this.chkPTTT.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkPTTT.Text = "Phẫu thuật, thủ thuật";
            this.chkPTTT.UseVisualStyleBackColor = false;
            this.chkPTTT.CheckedChanged += new System.EventHandler(this.chkPTTT_CheckedChanged);
            // 
            // txtPTTTmota
            // 
            this.txtPTTTmota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPTTTmota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPTTTmota.Enabled = false;
            this.txtPTTTmota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTTTmota.Location = new System.Drawing.Point(1111, 452);
            this.txtPTTTmota.Name = "txtPTTTmota";
            this.txtPTTTmota.Size = new System.Drawing.Size(319, 20);
            this.txtPTTTmota.TabIndex = 17;
            // 
            // txtNoikhoamota
            // 
            this.txtNoikhoamota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoikhoamota.Enabled = false;
            this.txtNoikhoamota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoikhoamota.Location = new System.Drawing.Point(615, 452);
            this.txtNoikhoamota.Name = "txtNoikhoamota";
            this.txtNoikhoamota.Size = new System.Drawing.Size(364, 20);
            this.txtNoikhoamota.TabIndex = 15;
            // 
            // chkNoikhoa
            // 
            this.chkNoikhoa.AutoSize = true;
            this.chkNoikhoa.BackColor = System.Drawing.Color.Transparent;
            this.chkNoikhoa.Location = new System.Drawing.Point(543, 453);
            this.chkNoikhoa.Name = "chkNoikhoa";
            this.chkNoikhoa.Size = new System.Drawing.Size(69, 17);
            this.chkNoikhoa.TabIndex = 14;
            this.chkNoikhoa.Tag = "noitru_phieusoket15ngay_Preview";
            this.chkNoikhoa.Text = "Nội khoa";
            this.chkNoikhoa.UseVisualStyleBackColor = false;
            this.chkNoikhoa.CheckedChanged += new System.EventHandler(this.chkNoikhoa_CheckedChanged);
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
            this.dtNgayTTBA.Location = new System.Drawing.Point(1111, 674);
            this.dtNgayTTBA.Name = "dtNgayTTBA";
            this.dtNgayTTBA.ShowUpDown = true;
            this.dtNgayTTBA.Size = new System.Drawing.Size(142, 20);
            this.dtNgayTTBA.TabIndex = 32;
            this.dtNgayTTBA.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // txtDauhieulamsang
            // 
            this.txtDauhieulamsang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDauhieulamsang.Location = new System.Drawing.Point(615, 286);
            this.txtDauhieulamsang.MaxLength = 4000;
            this.txtDauhieulamsang.Multiline = true;
            this.txtDauhieulamsang.Name = "txtDauhieulamsang";
            this.txtDauhieulamsang.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDauhieulamsang.Size = new System.Drawing.Size(815, 80);
            this.txtDauhieulamsang.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(419, 286);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 82);
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
            this.txtTiensubenh.Location = new System.Drawing.Point(615, 224);
            this.txtTiensubenh.MaxLength = 4000;
            this.txtTiensubenh.Multiline = true;
            this.txtTiensubenh.Name = "txtTiensubenh";
            this.txtTiensubenh.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTiensubenh.Size = new System.Drawing.Size(815, 57);
            this.txtTiensubenh.TabIndex = 11;
            this.txtTiensubenh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(459, 224);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(153, 57);
            this.label54.TabIndex = 474;
            this.label54.Text = "Tiền sử bệnh:";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_chandoan_ravien
            // 
            this.txt_chandoan_ravien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_chandoan_ravien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chandoan_ravien.Location = new System.Drawing.Point(615, 53);
            this.txt_chandoan_ravien.Name = "txt_chandoan_ravien";
            this.txt_chandoan_ravien.Size = new System.Drawing.Size(557, 20);
            this.txt_chandoan_ravien.TabIndex = 3;
            this.txt_chandoan_ravien.TabStop = false;
            // 
            // txtTomtatCLS
            // 
            this.txtTomtatCLS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTomtatCLS.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtTomtatCLS.Location = new System.Drawing.Point(615, 369);
            this.txtTomtatCLS.MaxLength = 4000;
            this.txtTomtatCLS.Multiline = true;
            this.txtTomtatCLS.Name = "txtTomtatCLS";
            this.txtTomtatCLS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTomtatCLS.Size = new System.Drawing.Size(815, 80);
            this.txtTomtatCLS.TabIndex = 13;
            // 
            // txtquatrinhbenhly
            // 
            this.txtquatrinhbenhly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtquatrinhbenhly.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtquatrinhbenhly.Location = new System.Drawing.Point(615, 159);
            this.txtquatrinhbenhly.MaxLength = 4000;
            this.txtquatrinhbenhly.Multiline = true;
            this.txtquatrinhbenhly.Name = "txtquatrinhbenhly";
            this.txtquatrinhbenhly.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtquatrinhbenhly.Size = new System.Drawing.Size(815, 61);
            this.txtquatrinhbenhly.TabIndex = 10;
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
            this.autoLydovv.cmdDropDown = null;
            this.autoLydovv.CompareNoID = true;
            this.autoLydovv.DefaultCode = "-1";
            this.autoLydovv.DefaultID = "-1";
            this.autoLydovv.Drug_ID = null;
            this.autoLydovv.ExtraWidth = 0;
            this.autoLydovv.FillValueAfterSelect = false;
            this.autoLydovv.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLydovv.LOAI_DANHMUC = "LYDOVAOVIEN";
            this.autoLydovv.Location = new System.Drawing.Point(615, 135);
            this.autoLydovv.MaxHeight = -1;
            this.autoLydovv.MinTypedCharacters = 2;
            this.autoLydovv.MyCode = "-1";
            this.autoLydovv.MyID = "-1";
            this.autoLydovv.Name = "autoLydovv";
            this.autoLydovv.RaiseEvent = false;
            this.autoLydovv.RaiseEventEnter = false;
            this.autoLydovv.RaiseEventEnterWhenEmpty = false;
            this.autoLydovv.SelectedIndex = -1;
            this.autoLydovv.SetDefaultWhenInit = true;
            this.autoLydovv.ShowCodeWithValue = false;
            this.autoLydovv.Size = new System.Drawing.Size(815, 21);
            this.autoLydovv.splitChar = '@';
            this.autoLydovv.splitCharIDAndCode = '#';
            this.autoLydovv.TabIndex = 7;
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
            this.txtPPdieutri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPPdieutri.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtPPdieutri.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPdieutri.Location = new System.Drawing.Point(615, 475);
            this.txtPPdieutri.MaxLength = 4000;
            this.txtPPdieutri.Multiline = true;
            this.txtPPdieutri.Name = "txtPPdieutri";
            this.txtPPdieutri.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPPdieutri.Size = new System.Drawing.Size(815, 69);
            this.txtPPdieutri.TabIndex = 18;
            this.txtPPdieutri.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
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
            this.autoKhoa.Location = new System.Drawing.Point(615, 111);
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
            this.autoKhoa.TabIndex = 5;
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
            // dtpNgayRavien
            // 
            this.dtpNgayRavien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayRavien.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtpNgayRavien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayRavien.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtpNgayRavien.DropDownCalendar.Name = "";
            this.dtpNgayRavien.Enabled = false;
            this.dtpNgayRavien.IsNullDate = true;
            this.dtpNgayRavien.Location = new System.Drawing.Point(1291, 54);
            this.dtpNgayRavien.Name = "dtpNgayRavien";
            this.dtpNgayRavien.ShowUpDown = true;
            this.dtpNgayRavien.Size = new System.Drawing.Size(142, 20);
            this.dtpNgayRavien.TabIndex = 4;
            this.dtpNgayRavien.TabStop = false;
            // 
            // dtpNgayNhapvien
            // 
            this.dtpNgayNhapvien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayNhapvien.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtpNgayNhapvien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayNhapvien.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtpNgayNhapvien.DropDownCalendar.Name = "";
            this.dtpNgayNhapvien.Enabled = false;
            this.dtpNgayNhapvien.Location = new System.Drawing.Point(1291, 30);
            this.dtpNgayNhapvien.Name = "dtpNgayNhapvien";
            this.dtpNgayNhapvien.ShowUpDown = true;
            this.dtpNgayNhapvien.Size = new System.Drawing.Size(142, 20);
            this.dtpNgayNhapvien.TabIndex = 2;
            this.dtpNgayNhapvien.TabStop = false;
            this.dtpNgayNhapvien.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(471, 473);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 71);
            this.label14.TabIndex = 16;
            this.label14.Text = "PP điều trị";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Arial", 9F);
            this.label10.Location = new System.Drawing.Point(1189, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "Ngày ra viện:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_chandoanvaovien
            // 
            this.txt_chandoanvaovien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_chandoanvaovien.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chandoanvaovien.Location = new System.Drawing.Point(615, 31);
            this.txt_chandoanvaovien.Name = "txt_chandoanvaovien";
            this.txt_chandoanvaovien.Size = new System.Drawing.Size(557, 20);
            this.txt_chandoanvaovien.TabIndex = 1;
            this.txt_chandoanvaovien.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(459, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 61);
            this.label7.TabIndex = 2;
            this.label7.Text = "Quá trình bệnh lý và diễn biến lâm sàng: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(1189, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ngày vào viện:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(459, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Khoa điều trị";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(459, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lý do vào viện:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(985, 675);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 19);
            this.label11.TabIndex = 476;
            this.label11.Text = "Ngày tóm tắt BA:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(462, 676);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(147, 16);
            this.label15.TabIndex = 767;
            this.label15.Text = "Đại diện đơn vị:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(69, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "KQ điều trị";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtB_Khac);
            this.panel4.Controls.Add(this.txtB_XetNghiem);
            this.panel4.Controls.Add(this.txtB_SieuAm);
            this.panel4.Controls.Add(this.txtB_CTScanner);
            this.panel4.Controls.Add(this.txtB_Xquang);
            this.panel4.Controls.Add(this.label39);
            this.panel4.Controls.Add(this.label154);
            this.panel4.Controls.Add(this.label231);
            this.panel4.Controls.Add(this.label232);
            this.panel4.Controls.Add(this.label233);
            this.panel4.Controls.Add(this.label234);
            this.panel4.Controls.Add(this.label235);
            this.panel4.Controls.Add(this.label236);
            this.panel4.Controls.Add(this.label237);
            this.panel4.Controls.Add(this.txtB_Tongso);
            this.panel4.Controls.Add(this.label238);
            this.panel4.Controls.Add(this.label239);
            this.panel4.Controls.Add(this.label240);
            this.panel4.Location = new System.Drawing.Point(60, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 153);
            this.panel4.TabIndex = 2;
            this.panel4.Visible = false;
            // 
            // txtB_Khac
            // 
            this.txtB_Khac.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_Khac.Location = new System.Drawing.Point(139, 104);
            this.txtB_Khac.MaxLength = 11;
            this.txtB_Khac.Name = "txtB_Khac";
            this.txtB_Khac.Numeric = true;
            this.txtB_Khac.Size = new System.Drawing.Size(67, 21);
            this.txtB_Khac.TabIndex = 84;
            this.txtB_Khac.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtB_Khac.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtB_XetNghiem
            // 
            this.txtB_XetNghiem.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_XetNghiem.Location = new System.Drawing.Point(139, 82);
            this.txtB_XetNghiem.MaxLength = 11;
            this.txtB_XetNghiem.Name = "txtB_XetNghiem";
            this.txtB_XetNghiem.Numeric = true;
            this.txtB_XetNghiem.Size = new System.Drawing.Size(67, 21);
            this.txtB_XetNghiem.TabIndex = 83;
            this.txtB_XetNghiem.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtB_XetNghiem.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtB_SieuAm
            // 
            this.txtB_SieuAm.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_SieuAm.Location = new System.Drawing.Point(139, 60);
            this.txtB_SieuAm.MaxLength = 11;
            this.txtB_SieuAm.Name = "txtB_SieuAm";
            this.txtB_SieuAm.Numeric = true;
            this.txtB_SieuAm.Size = new System.Drawing.Size(67, 21);
            this.txtB_SieuAm.TabIndex = 82;
            this.txtB_SieuAm.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtB_SieuAm.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtB_CTScanner
            // 
            this.txtB_CTScanner.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_CTScanner.Location = new System.Drawing.Point(139, 38);
            this.txtB_CTScanner.MaxLength = 11;
            this.txtB_CTScanner.Name = "txtB_CTScanner";
            this.txtB_CTScanner.Numeric = true;
            this.txtB_CTScanner.Size = new System.Drawing.Size(67, 21);
            this.txtB_CTScanner.TabIndex = 81;
            this.txtB_CTScanner.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtB_CTScanner.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtB_Xquang
            // 
            this.txtB_Xquang.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_Xquang.Location = new System.Drawing.Point(139, 16);
            this.txtB_Xquang.MaxLength = 11;
            this.txtB_Xquang.Name = "txtB_Xquang";
            this.txtB_Xquang.Numeric = true;
            this.txtB_Xquang.Size = new System.Drawing.Size(67, 21);
            this.txtB_Xquang.TabIndex = 80;
            this.txtB_Xquang.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtB_Xquang.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Arial", 9F);
            this.label39.Location = new System.Drawing.Point(212, 130);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(18, 15);
            this.label39.TabIndex = 17;
            this.label39.Text = "tờ";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Font = new System.Drawing.Font("Arial", 9F);
            this.label154.Location = new System.Drawing.Point(212, 108);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(18, 15);
            this.label154.TabIndex = 16;
            this.label154.Text = "tờ";
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.Font = new System.Drawing.Font("Arial", 9F);
            this.label231.Location = new System.Drawing.Point(212, 86);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(18, 15);
            this.label231.TabIndex = 15;
            this.label231.Text = "tờ";
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.Font = new System.Drawing.Font("Arial", 9F);
            this.label232.Location = new System.Drawing.Point(212, 64);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(18, 15);
            this.label232.TabIndex = 14;
            this.label232.Text = "tờ";
            // 
            // label233
            // 
            this.label233.AutoSize = true;
            this.label233.Font = new System.Drawing.Font("Arial", 9F);
            this.label233.Location = new System.Drawing.Point(212, 42);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(18, 15);
            this.label233.TabIndex = 13;
            this.label233.Text = "tờ";
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Font = new System.Drawing.Font("Arial", 9F);
            this.label234.Location = new System.Drawing.Point(212, 20);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(18, 15);
            this.label234.TabIndex = 12;
            this.label234.Text = "tờ";
            // 
            // label235
            // 
            this.label235.Font = new System.Drawing.Font("Arial", 9F);
            this.label235.Location = new System.Drawing.Point(18, 86);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(113, 15);
            this.label235.TabIndex = 9;
            this.label235.Text = "+ Xét nghiệm";
            // 
            // label236
            // 
            this.label236.Font = new System.Drawing.Font("Arial", 9F);
            this.label236.Location = new System.Drawing.Point(18, 130);
            this.label236.Name = "label236";
            this.label236.Size = new System.Drawing.Size(113, 15);
            this.label236.TabIndex = 11;
            this.label236.Text = "+ Toàn bộ hồ sơ";
            // 
            // label237
            // 
            this.label237.Font = new System.Drawing.Font("Arial", 9F);
            this.label237.Location = new System.Drawing.Point(18, 108);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(113, 15);
            this.label237.TabIndex = 10;
            this.label237.Text = "+ Khác";
            // 
            // txtB_Tongso
            // 
            this.txtB_Tongso.Enabled = false;
            this.txtB_Tongso.Font = new System.Drawing.Font("Arial", 9F);
            this.txtB_Tongso.Location = new System.Drawing.Point(139, 126);
            this.txtB_Tongso.Name = "txtB_Tongso";
            this.txtB_Tongso.Size = new System.Drawing.Size(67, 21);
            this.txtB_Tongso.TabIndex = 85;
            // 
            // label238
            // 
            this.label238.Font = new System.Drawing.Font("Arial", 9F);
            this.label238.Location = new System.Drawing.Point(18, 64);
            this.label238.Name = "label238";
            this.label238.Size = new System.Drawing.Size(113, 15);
            this.label238.TabIndex = 8;
            this.label238.Text = "+ Siêu âm";
            // 
            // label239
            // 
            this.label239.Font = new System.Drawing.Font("Arial", 9F);
            this.label239.Location = new System.Drawing.Point(18, 42);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(113, 15);
            this.label239.TabIndex = 7;
            this.label239.Text = "+ CT Scanner";
            // 
            // label240
            // 
            this.label240.Font = new System.Drawing.Font("Arial", 9F);
            this.label240.Location = new System.Drawing.Point(18, 20);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(113, 15);
            this.label240.TabIndex = 6;
            this.label240.Text = "+ X - quang";
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label26.Location = new System.Drawing.Point(278, 101);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(147, 16);
            this.label26.TabIndex = 772;
            this.label26.Text = "Trưởng khoa:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label26.Visible = false;
            // 
            // label243
            // 
            this.label243.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label243.ForeColor = System.Drawing.Color.Red;
            this.label243.Location = new System.Drawing.Point(280, 73);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(147, 16);
            this.label243.TabIndex = 762;
            this.label243.Text = "Người nhận hồ sơ";
            this.label243.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label243.Visible = false;
            // 
            // label242
            // 
            this.label242.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label242.ForeColor = System.Drawing.Color.Red;
            this.label242.Location = new System.Drawing.Point(280, 48);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(147, 16);
            this.label242.TabIndex = 763;
            this.label242.Text = "Người giao hồ sơ";
            this.label242.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label242.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txttinhtrangravienMota);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.cmdIn);
            this.panel3.Controls.Add(this.cmdXoa);
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Controls.Add(this.cmdThemmoi);
            this.panel3.Controls.Add(this.cmdSave);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtTinhtrangRavien);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 734);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1442, 61);
            this.panel3.TabIndex = 1;
            // 
            // txttinhtrangravienMota
            // 
            this.txttinhtrangravienMota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txttinhtrangravienMota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttinhtrangravienMota.Location = new System.Drawing.Point(274, 23);
            this.txttinhtrangravienMota.Name = "txttinhtrangravienMota";
            this.txttinhtrangravienMota.Size = new System.Drawing.Size(40, 20);
            this.txttinhtrangravienMota.TabIndex = 774;
            this.txttinhtrangravienMota.TabStop = false;
            this.txttinhtrangravienMota.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.txtTruongkhoa);
            this.panel2.Controls.Add(this.label242);
            this.panel2.Controls.Add(this.txtNguoiNhanHoSo);
            this.panel2.Controls.Add(this.label243);
            this.panel2.Controls.Add(this.txtNguoiGiaoHoSo);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Location = new System.Drawing.Point(34, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(28, 26);
            this.panel2.TabIndex = 773;
            this.panel2.Visible = false;
            // 
            // txtTruongkhoa
            // 
            this.txtTruongkhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTruongkhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruongkhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTruongkhoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTruongkhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTruongkhoa.AutoCompleteList")));
            this.txtTruongkhoa.BackColor = System.Drawing.Color.White;
            this.txtTruongkhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTruongkhoa.buildShortcut = false;
            this.txtTruongkhoa.CaseSensitive = false;
            this.txtTruongkhoa.CompareNoID = true;
            this.txtTruongkhoa.DefaultCode = "-1";
            this.txtTruongkhoa.DefaultID = "-1";
            this.txtTruongkhoa.DisplayType = 0;
            this.txtTruongkhoa.Drug_ID = null;
            this.txtTruongkhoa.ExtraWidth = 0;
            this.txtTruongkhoa.FillValueAfterSelect = false;
            this.txtTruongkhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTruongkhoa.Location = new System.Drawing.Point(433, 97);
            this.txtTruongkhoa.MaxHeight = 289;
            this.txtTruongkhoa.MinTypedCharacters = 2;
            this.txtTruongkhoa.MyCode = "-1";
            this.txtTruongkhoa.MyID = "-1";
            this.txtTruongkhoa.MyText = "";
            this.txtTruongkhoa.MyTextOnly = "";
            this.txtTruongkhoa.Name = "txtTruongkhoa";
            this.txtTruongkhoa.RaiseEvent = true;
            this.txtTruongkhoa.RaiseEventEnter = true;
            this.txtTruongkhoa.RaiseEventEnterWhenEmpty = true;
            this.txtTruongkhoa.SelectedIndex = -1;
            this.txtTruongkhoa.Size = new System.Drawing.Size(0, 21);
            this.txtTruongkhoa.splitChar = '@';
            this.txtTruongkhoa.splitCharIDAndCode = '#';
            this.txtTruongkhoa.TabIndex = 771;
            this.txtTruongkhoa.TakeCode = false;
            this.txtTruongkhoa.txtMyCode = null;
            this.txtTruongkhoa.txtMyCode_Edit = null;
            this.txtTruongkhoa.txtMyID = null;
            this.txtTruongkhoa.txtMyID_Edit = null;
            this.txtTruongkhoa.txtMyName = null;
            this.txtTruongkhoa.txtMyName_Edit = null;
            this.txtTruongkhoa.txtNext = null;
            this.txtTruongkhoa.Visible = false;
            // 
            // txtNguoiNhanHoSo
            // 
            this.txtNguoiNhanHoSo._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNguoiNhanHoSo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiNhanHoSo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoiNhanHoSo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiNhanHoSo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoiNhanHoSo.AutoCompleteList")));
            this.txtNguoiNhanHoSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoiNhanHoSo.buildShortcut = false;
            this.txtNguoiNhanHoSo.CaseSensitive = false;
            this.txtNguoiNhanHoSo.CompareNoID = true;
            this.txtNguoiNhanHoSo.DefaultCode = "-1";
            this.txtNguoiNhanHoSo.DefaultID = "-1";
            this.txtNguoiNhanHoSo.DisplayType = 0;
            this.txtNguoiNhanHoSo.Drug_ID = null;
            this.txtNguoiNhanHoSo.ExtraWidth = 0;
            this.txtNguoiNhanHoSo.FillValueAfterSelect = false;
            this.txtNguoiNhanHoSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiNhanHoSo.ForeColor = System.Drawing.Color.Red;
            this.txtNguoiNhanHoSo.Location = new System.Drawing.Point(433, 70);
            this.txtNguoiNhanHoSo.MaxHeight = 289;
            this.txtNguoiNhanHoSo.MinTypedCharacters = 2;
            this.txtNguoiNhanHoSo.MyCode = "-1";
            this.txtNguoiNhanHoSo.MyID = "-1";
            this.txtNguoiNhanHoSo.MyText = "";
            this.txtNguoiNhanHoSo.MyTextOnly = "";
            this.txtNguoiNhanHoSo.Name = "txtNguoiNhanHoSo";
            this.txtNguoiNhanHoSo.RaiseEvent = true;
            this.txtNguoiNhanHoSo.RaiseEventEnter = true;
            this.txtNguoiNhanHoSo.RaiseEventEnterWhenEmpty = true;
            this.txtNguoiNhanHoSo.SelectedIndex = -1;
            this.txtNguoiNhanHoSo.Size = new System.Drawing.Size(0, 22);
            this.txtNguoiNhanHoSo.splitChar = '@';
            this.txtNguoiNhanHoSo.splitCharIDAndCode = '#';
            this.txtNguoiNhanHoSo.TabIndex = 765;
            this.txtNguoiNhanHoSo.TakeCode = false;
            this.txtNguoiNhanHoSo.txtMyCode = null;
            this.txtNguoiNhanHoSo.txtMyCode_Edit = null;
            this.txtNguoiNhanHoSo.txtMyID = null;
            this.txtNguoiNhanHoSo.txtMyID_Edit = null;
            this.txtNguoiNhanHoSo.txtMyName = null;
            this.txtNguoiNhanHoSo.txtMyName_Edit = null;
            this.txtNguoiNhanHoSo.txtNext = null;
            this.txtNguoiNhanHoSo.Visible = false;
            // 
            // txtNguoiGiaoHoSo
            // 
            this.txtNguoiGiaoHoSo._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNguoiGiaoHoSo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiGiaoHoSo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoiGiaoHoSo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiGiaoHoSo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoiGiaoHoSo.AutoCompleteList")));
            this.txtNguoiGiaoHoSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoiGiaoHoSo.buildShortcut = false;
            this.txtNguoiGiaoHoSo.CaseSensitive = false;
            this.txtNguoiGiaoHoSo.CompareNoID = true;
            this.txtNguoiGiaoHoSo.DefaultCode = "-1";
            this.txtNguoiGiaoHoSo.DefaultID = "-1";
            this.txtNguoiGiaoHoSo.DisplayType = 0;
            this.txtNguoiGiaoHoSo.Drug_ID = null;
            this.txtNguoiGiaoHoSo.ExtraWidth = 0;
            this.txtNguoiGiaoHoSo.FillValueAfterSelect = false;
            this.txtNguoiGiaoHoSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiGiaoHoSo.ForeColor = System.Drawing.Color.Red;
            this.txtNguoiGiaoHoSo.Location = new System.Drawing.Point(433, 44);
            this.txtNguoiGiaoHoSo.MaxHeight = 289;
            this.txtNguoiGiaoHoSo.MinTypedCharacters = 2;
            this.txtNguoiGiaoHoSo.MyCode = "-1";
            this.txtNguoiGiaoHoSo.MyID = "-1";
            this.txtNguoiGiaoHoSo.MyText = "";
            this.txtNguoiGiaoHoSo.MyTextOnly = "";
            this.txtNguoiGiaoHoSo.Name = "txtNguoiGiaoHoSo";
            this.txtNguoiGiaoHoSo.RaiseEvent = true;
            this.txtNguoiGiaoHoSo.RaiseEventEnter = true;
            this.txtNguoiGiaoHoSo.RaiseEventEnterWhenEmpty = true;
            this.txtNguoiGiaoHoSo.SelectedIndex = -1;
            this.txtNguoiGiaoHoSo.Size = new System.Drawing.Size(0, 22);
            this.txtNguoiGiaoHoSo.splitChar = '@';
            this.txtNguoiGiaoHoSo.splitCharIDAndCode = '#';
            this.txtNguoiGiaoHoSo.TabIndex = 764;
            this.txtNguoiGiaoHoSo.TakeCode = false;
            this.txtNguoiGiaoHoSo.txtMyCode = null;
            this.txtNguoiGiaoHoSo.txtMyCode_Edit = null;
            this.txtNguoiGiaoHoSo.txtMyID = null;
            this.txtNguoiGiaoHoSo.txtMyID_Edit = null;
            this.txtNguoiGiaoHoSo.txtMyName = null;
            this.txtNguoiGiaoHoSo.txtMyName_Edit = null;
            this.txtNguoiGiaoHoSo.txtNext = null;
            this.txtNguoiGiaoHoSo.Visible = false;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Enabled = false;
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(934, 16);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(120, 33);
            this.cmdIn.TabIndex = 19;
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
            this.cmdXoa.Location = new System.Drawing.Point(1060, 16);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(120, 33);
            this.cmdXoa.TabIndex = 20;
            this.cmdXoa.TabStop = false;
            this.cmdXoa.Text = "Xóa";
            this.cmdXoa.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdXoa.Click += new System.EventHandler(this.cmdXoa_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1310, 16);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 33);
            this.cmdExit.TabIndex = 22;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click_1);
            // 
            // cmdThemmoi
            // 
            this.cmdThemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThemmoi.Enabled = false;
            this.cmdThemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemmoi.Image = global::VMS.HIS.EMR.Properties.Resources.add_04_32;
            this.cmdThemmoi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdThemmoi.Location = new System.Drawing.Point(808, 16);
            this.cmdThemmoi.Name = "cmdThemmoi";
            this.cmdThemmoi.Size = new System.Drawing.Size(120, 33);
            this.cmdThemmoi.TabIndex = 21;
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
            this.cmdSave.Location = new System.Drawing.Point(1185, 16);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 33);
            this.cmdSave.TabIndex = 33;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // txtTinhtrangRavien
            // 
            this.txtTinhtrangRavien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtTinhtrangRavien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangRavien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTinhtrangRavien.AddValues = true;
            this.txtTinhtrangRavien.AllowMultiline = false;
            this.txtTinhtrangRavien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhtrangRavien.AutoCompleteList")));
            this.txtTinhtrangRavien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTinhtrangRavien.buildShortcut = false;
            this.txtTinhtrangRavien.CaseSensitive = false;
            this.txtTinhtrangRavien.cmdDropDown = null;
            this.txtTinhtrangRavien.CompareNoID = true;
            this.txtTinhtrangRavien.DefaultCode = "-1";
            this.txtTinhtrangRavien.DefaultID = "-1";
            this.txtTinhtrangRavien.Drug_ID = null;
            this.txtTinhtrangRavien.Enabled = false;
            this.txtTinhtrangRavien.ExtraWidth = 0;
            this.txtTinhtrangRavien.FillValueAfterSelect = false;
            this.txtTinhtrangRavien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangRavien.LOAI_DANHMUC = "TINHTRANGRAVIEN";
            this.txtTinhtrangRavien.Location = new System.Drawing.Point(128, 24);
            this.txtTinhtrangRavien.MaxHeight = 150;
            this.txtTinhtrangRavien.MaxLength = 4000;
            this.txtTinhtrangRavien.MinTypedCharacters = 2;
            this.txtTinhtrangRavien.MyCode = "-1";
            this.txtTinhtrangRavien.MyID = "-1";
            this.txtTinhtrangRavien.Name = "txtTinhtrangRavien";
            this.txtTinhtrangRavien.RaiseEvent = false;
            this.txtTinhtrangRavien.RaiseEventEnter = false;
            this.txtTinhtrangRavien.RaiseEventEnterWhenEmpty = false;
            this.txtTinhtrangRavien.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTinhtrangRavien.SelectedIndex = -1;
            this.txtTinhtrangRavien.SetDefaultWhenInit = true;
            this.txtTinhtrangRavien.ShowCodeWithValue = false;
            this.txtTinhtrangRavien.Size = new System.Drawing.Size(28, 21);
            this.txtTinhtrangRavien.splitChar = '@';
            this.txtTinhtrangRavien.splitCharIDAndCode = '#';
            this.txtTinhtrangRavien.TabIndex = 11;
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
            this.txtTinhtrangRavien.Visible = false;
            // 
            // cmdTuSinh
            // 
            this.cmdTuSinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTuSinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdTuSinh.Image")));
            this.cmdTuSinh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTuSinh.Location = new System.Drawing.Point(1399, 697);
            this.cmdTuSinh.Name = "cmdTuSinh";
            this.cmdTuSinh.Size = new System.Drawing.Size(31, 27);
            this.cmdTuSinh.TabIndex = 2599;
            this.cmdTuSinh.TabStop = false;
            this.cmdTuSinh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdTuSinh.Click += new System.EventHandler(this.cmdTuSinh_Click);
            // 
            // txtSoHoso
            // 
            this.txtSoHoso.BackColor = System.Drawing.Color.FloralWhite;
            this.txtSoHoso.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtSoHoso.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHoso.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHoso.Location = new System.Drawing.Point(1338, 674);
            this.txtSoHoso.Name = "txtSoHoso";
            this.txtSoHoso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSoHoso.Size = new System.Drawing.Size(92, 22);
            this.txtSoHoso.TabIndex = 2598;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(1259, 675);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 2600;
            this.label1.Text = "Số hồ sơ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(1093, 7);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(79, 21);
            this.txtId.TabIndex = 2601;
            this.txtId.TabStop = false;
            this.txtId.Visible = false;
            // 
            // frm_TomtatBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 795);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.Name = "frm_TomtatBA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng kết bệnh án";
            this.Load += new System.EventHandler(this.frm_TomtatBA_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlKetquadieutriravien.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_chandoanvaovien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayRavien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayNhapvien;
        private AutoCompleteTextbox autoKhoa;
        private AutoCompleteTextbox_Danhmucchung autoLydovv;
        public AutoCompleteTextbox_Danhmucchung txtTinhtrangRavien;
        private Janus.Windows.GridEX.EditControls.EditBox txtTomtatCLS;
        private Janus.Windows.GridEX.EditControls.EditBox txtquatrinhbenhly;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdXoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdThemmoi;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.TextBox txt_chandoan_ravien;
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
        private System.Windows.Forms.Label label13;
        private Janus.Windows.GridEX.EditControls.EditBox txtPPdieutri;
        private AutoCompleteTextbox txtBSDieuTri;
        private System.Windows.Forms.Label label245;
        private System.Windows.Forms.Panel panel4;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtB_Khac;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtB_XetNghiem;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtB_SieuAm;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtB_CTScanner;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtB_Xquang;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label154;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label232;
        private System.Windows.Forms.Label label233;
        private System.Windows.Forms.Label label234;
        private System.Windows.Forms.Label label235;
        private System.Windows.Forms.Label label236;
        private System.Windows.Forms.Label label237;
        private System.Windows.Forms.TextBox txtB_Tongso;
        private System.Windows.Forms.Label label238;
        private System.Windows.Forms.Label label239;
        private System.Windows.Forms.Label label240;
        private AutoCompleteTextbox txtNguoiNhanHoSo;
        private AutoCompleteTextbox txtNguoiGiaoHoSo;
        private AutoCompleteTextbox txtGDBV;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label243;
        private System.Windows.Forms.Label label242;
        private AutoCompleteTextbox txtTruongkhoa;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlKetquadieutriravien;
        private System.Windows.Forms.CheckBox chkTTRVKhoi;
        private System.Windows.Forms.CheckBox chkTTRVDoGiam;
        private System.Windows.Forms.CheckBox chkTTRVKhongThayDoi;
        private System.Windows.Forms.CheckBox chkTTRVNangHon;
        private System.Windows.Forms.CheckBox chkTTRVTuVong;
        public ucThongtinnguoibenh_emr_basic_v2 ucThongtinnguoibenh_emr_basic1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txttinhtrangravienMota;
        private System.Windows.Forms.LinkLabel lnk_kq_cls;
        private System.Windows.Forms.LinkLabel lbk_chandoan_ravien;
        private System.Windows.Forms.LinkLabel lnk_chandoanvaovien;
        private Janus.Windows.EditControls.UIButton cmdTuSinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoHoso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
    }
}