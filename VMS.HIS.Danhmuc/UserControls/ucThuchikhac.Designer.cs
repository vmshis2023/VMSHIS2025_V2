namespace VNS.HIS.UCs.Noitru
{
    partial class ucThuchikhac
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
            this.components = new System.ComponentModel.Container();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucThuchikhac));
            Janus.Windows.GridEX.GridEXLayout grdThuchi_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboNganhang = new Janus.Windows.EditControls.UIComboBox();
            this.cboPttt = new Janus.Windows.EditControls.UIComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optPhieuChi = new Janus.Windows.EditControls.UIRadioButton();
            this.optPhieuThu = new Janus.Windows.EditControls.UIRadioButton();
            this.pnlFunctions = new System.Windows.Forms.Panel();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdGhi = new Janus.Windows.EditControls.UIButton();
            this.cmdxoa = new Janus.Windows.EditControls.UIButton();
            this.cmdHuy = new Janus.Windows.EditControls.UIButton();
            this.cmdthemmoi = new Janus.Windows.EditControls.UIButton();
            this.cmdSua = new Janus.Windows.EditControls.UIButton();
            this.txtMotathem = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.autoNguonkiqui = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label27 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkPrintPreview = new System.Windows.Forms.CheckBox();
            this.chkSaveAndPrint = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNguoithu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.lblNguoithuchi = new System.Windows.Forms.Label();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            this.dtpNgaythu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.grdThuchi = new Janus.Windows.GridEX.GridEX();
            this.ctxMore = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdPhanbo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFunctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThuchi)).BeginInit();
            this.ctxMore.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.cboNganhang);
            this.panel2.Controls.Add(this.cboPttt);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.pnlFunctions);
            this.panel2.Controls.Add(this.txtMotathem);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.autoNguonkiqui);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.chkPrintPreview);
            this.panel2.Controls.Add(this.chkSaveAndPrint);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtNguoithu);
            this.panel2.Controls.Add(this.txtLydo);
            this.panel2.Controls.Add(this.lblNguoithuchi);
            this.panel2.Controls.Add(this.txtSotien);
            this.panel2.Controls.Add(this.dtpNgaythu);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 278);
            this.panel2.TabIndex = 10;
            // 
            // cboNganhang
            // 
            this.cboNganhang.BackColor = System.Drawing.Color.White;
            this.cboNganhang.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboNganhang.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboNganhang.Font = new System.Drawing.Font("Arial", 9F);
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "In nhiệt";
            uiComboBoxItem1.Value = "0";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "In laser";
            uiComboBoxItem2.Value = "1";
            this.cboNganhang.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboNganhang.Location = new System.Drawing.Point(370, 135);
            this.cboNganhang.Name = "cboNganhang";
            this.cboNganhang.Size = new System.Drawing.Size(420, 21);
            this.cboNganhang.TabIndex = 14;
            this.cboNganhang.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboNganhang.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cboPttt
            // 
            this.cboPttt.BackColor = System.Drawing.Color.White;
            this.cboPttt.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboPttt.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPttt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "In nhiệt";
            uiComboBoxItem3.Value = "0";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "In laser";
            uiComboBoxItem4.Value = "1";
            this.cboPttt.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4});
            this.cboPttt.Location = new System.Drawing.Point(111, 135);
            this.cboPttt.Name = "cboPttt";
            this.cboPttt.Size = new System.Drawing.Size(173, 21);
            this.cboPttt.TabIndex = 13;
            this.cboPttt.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboPttt.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cboPttt.SelectedIndexChanged += new System.EventHandler(this.cboPttt_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optPhieuChi);
            this.panel1.Controls.Add(this.optPhieuThu);
            this.panel1.Location = new System.Drawing.Point(114, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 23);
            this.panel1.TabIndex = 403;
            // 
            // optPhieuChi
            // 
            this.optPhieuChi.Dock = System.Windows.Forms.DockStyle.Left;
            this.optPhieuChi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPhieuChi.ForeColor = System.Drawing.Color.Purple;
            this.optPhieuChi.Location = new System.Drawing.Point(131, 0);
            this.optPhieuChi.Name = "optPhieuChi";
            this.optPhieuChi.Size = new System.Drawing.Size(169, 23);
            this.optPhieuChi.TabIndex = 400;
            this.optPhieuChi.Text = "Phiếu chi khác";
            this.optPhieuChi.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // optPhieuThu
            // 
            this.optPhieuThu.Checked = true;
            this.optPhieuThu.Dock = System.Windows.Forms.DockStyle.Left;
            this.optPhieuThu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPhieuThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.optPhieuThu.Name = "optPhieuThu";
            this.optPhieuThu.Size = new System.Drawing.Size(131, 23);
            this.optPhieuThu.TabIndex = 399;
            this.optPhieuThu.TabStop = true;
            this.optPhieuThu.Text = "Phiếu thu khác";
            this.optPhieuThu.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.optPhieuThu.CheckedChanged += new System.EventHandler(this.optPhieuThu_CheckedChanged);
            // 
            // pnlFunctions
            // 
            this.pnlFunctions.Controls.Add(this.cmdIn);
            this.pnlFunctions.Controls.Add(this.cmdGhi);
            this.pnlFunctions.Controls.Add(this.cmdxoa);
            this.pnlFunctions.Controls.Add(this.cmdHuy);
            this.pnlFunctions.Controls.Add(this.cmdthemmoi);
            this.pnlFunctions.Controls.Add(this.cmdSua);
            this.pnlFunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFunctions.Location = new System.Drawing.Point(0, 217);
            this.pnlFunctions.Name = "pnlFunctions";
            this.pnlFunctions.Size = new System.Drawing.Size(800, 41);
            this.pnlFunctions.TabIndex = 402;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(696, 5);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(100, 30);
            this.cmdIn.TabIndex = 19;
            this.cmdIn.Text = "In phiếu";
            this.cmdIn.Click += new System.EventHandler(this.cmdIn_Click_1);
            // 
            // cmdGhi
            // 
            this.cmdGhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGhi.Enabled = false;
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdGhi.Location = new System.Drawing.Point(590, 5);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(100, 30);
            this.cmdGhi.TabIndex = 16;
            this.cmdGhi.Text = "Ghi";
            // 
            // cmdxoa
            // 
            this.cmdxoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdxoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdxoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdxoa.Image")));
            this.cmdxoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdxoa.Location = new System.Drawing.Point(590, 5);
            this.cmdxoa.Name = "cmdxoa";
            this.cmdxoa.Size = new System.Drawing.Size(100, 30);
            this.cmdxoa.TabIndex = 20;
            this.cmdxoa.Text = "Xóa";
            // 
            // cmdHuy
            // 
            this.cmdHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuy.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuy.Image")));
            this.cmdHuy.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHuy.Location = new System.Drawing.Point(696, 5);
            this.cmdHuy.Name = "cmdHuy";
            this.cmdHuy.Size = new System.Drawing.Size(100, 30);
            this.cmdHuy.TabIndex = 23;
            this.cmdHuy.Text = "Hủy";
            // 
            // cmdthemmoi
            // 
            this.cmdthemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdthemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthemmoi.Image = global::VMS.HIS.Danhmuc.Properties.Resources.add_04_32;
            this.cmdthemmoi.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdthemmoi.Location = new System.Drawing.Point(378, 5);
            this.cmdthemmoi.Name = "cmdthemmoi";
            this.cmdthemmoi.Size = new System.Drawing.Size(100, 30);
            this.cmdthemmoi.TabIndex = 21;
            this.cmdthemmoi.Text = "Thêm";
            // 
            // cmdSua
            // 
            this.cmdSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSua.Location = new System.Drawing.Point(484, 5);
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(100, 30);
            this.cmdSua.TabIndex = 18;
            this.cmdSua.Text = "Sửa";
            // 
            // txtMotathem
            // 
            this.txtMotathem._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtMotathem._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotathem._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMotathem.AddValues = true;
            this.txtMotathem.AllowMultiline = false;
            this.txtMotathem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMotathem.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtMotathem.AutoCompleteList")));
            this.txtMotathem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotathem.buildShortcut = false;
            this.txtMotathem.CaseSensitive = false;
            this.txtMotathem.cmdDropDown = null;
            this.txtMotathem.CompareNoID = true;
            this.txtMotathem.DefaultCode = "-1";
            this.txtMotathem.DefaultID = "-1";
            this.txtMotathem.Drug_ID = null;
            this.txtMotathem.ExtraWidth = 0;
            this.txtMotathem.FillValueAfterSelect = false;
            this.txtMotathem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotathem.LOAI_DANHMUC = "NGUONKIQUI";
            this.txtMotathem.Location = new System.Drawing.Point(111, 160);
            this.txtMotathem.MaxHeight = 150;
            this.txtMotathem.MinTypedCharacters = 4;
            this.txtMotathem.MyCode = "-1";
            this.txtMotathem.MyID = "-1";
            this.txtMotathem.Name = "txtMotathem";
            this.txtMotathem.RaiseEvent = false;
            this.txtMotathem.RaiseEventEnter = true;
            this.txtMotathem.RaiseEventEnterWhenEmpty = false;
            this.txtMotathem.SelectedIndex = -1;
            this.txtMotathem.ShowCodeWithValue = false;
            this.txtMotathem.Size = new System.Drawing.Size(679, 22);
            this.txtMotathem.splitChar = '@';
            this.txtMotathem.splitCharIDAndCode = '#';
            this.txtMotathem.TabIndex = 15;
            this.txtMotathem.TakeCode = false;
            this.txtMotathem.txtMyCode = null;
            this.txtMotathem.txtMyCode_Edit = null;
            this.txtMotathem.txtMyID = null;
            this.txtMotathem.txtMyID_Edit = null;
            this.txtMotathem.txtMyName = null;
            this.txtMotathem.txtMyName_Edit = null;
            this.txtMotathem.txtNext = null;
            this.txtMotathem.txtNext1 = null;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 24);
            this.label8.TabIndex = 401;
            this.label8.Text = "Mô tả thêm";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 398;
            this.label2.Text = "Loại phiếu:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 397;
            this.label1.Text = "Nguồn kí quĩ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoNguonkiqui
            // 
            this.autoNguonkiqui._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoNguonkiqui._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNguonkiqui._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoNguonkiqui.AddValues = true;
            this.autoNguonkiqui.AllowMultiline = false;
            this.autoNguonkiqui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoNguonkiqui.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoNguonkiqui.AutoCompleteList")));
            this.autoNguonkiqui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoNguonkiqui.buildShortcut = false;
            this.autoNguonkiqui.CaseSensitive = false;
            this.autoNguonkiqui.cmdDropDown = null;
            this.autoNguonkiqui.CompareNoID = true;
            this.autoNguonkiqui.DefaultCode = "-1";
            this.autoNguonkiqui.DefaultID = "-1";
            this.autoNguonkiqui.Drug_ID = null;
            this.autoNguonkiqui.ExtraWidth = 0;
            this.autoNguonkiqui.FillValueAfterSelect = false;
            this.autoNguonkiqui.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNguonkiqui.LOAI_DANHMUC = "NGUONKIQUI";
            this.autoNguonkiqui.Location = new System.Drawing.Point(112, 111);
            this.autoNguonkiqui.MaxHeight = 150;
            this.autoNguonkiqui.MinTypedCharacters = 4;
            this.autoNguonkiqui.MyCode = "-1";
            this.autoNguonkiqui.MyID = "-1";
            this.autoNguonkiqui.Name = "autoNguonkiqui";
            this.autoNguonkiqui.RaiseEvent = false;
            this.autoNguonkiqui.RaiseEventEnter = true;
            this.autoNguonkiqui.RaiseEventEnterWhenEmpty = false;
            this.autoNguonkiqui.SelectedIndex = -1;
            this.autoNguonkiqui.ShowCodeWithValue = false;
            this.autoNguonkiqui.Size = new System.Drawing.Size(679, 22);
            this.autoNguonkiqui.splitChar = '@';
            this.autoNguonkiqui.splitCharIDAndCode = '#';
            this.autoNguonkiqui.TabIndex = 12;
            this.autoNguonkiqui.TakeCode = false;
            this.autoNguonkiqui.txtMyCode = null;
            this.autoNguonkiqui.txtMyCode_Edit = null;
            this.autoNguonkiqui.txtMyID = null;
            this.autoNguonkiqui.txtMyID_Edit = null;
            this.autoNguonkiqui.txtMyName = null;
            this.autoNguonkiqui.txtMyName_Edit = null;
            this.autoNguonkiqui.txtNext = null;
            this.autoNguonkiqui.txtNext1 = null;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(293, 132);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 24);
            this.label27.TabIndex = 395;
            this.label27.Text = "Ngân hàng";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 24);
            this.label7.TabIndex = 393;
            this.label7.Text = "PTTT:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPrintPreview
            // 
            this.chkPrintPreview.AutoSize = true;
            this.chkPrintPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintPreview.Location = new System.Drawing.Point(224, 191);
            this.chkPrintPreview.Name = "chkPrintPreview";
            this.chkPrintPreview.Size = new System.Drawing.Size(141, 20);
            this.chkPrintPreview.TabIndex = 22;
            this.chkPrintPreview.TabStop = false;
            this.chkPrintPreview.Text = "Xem trước khi in?";
            this.chkPrintPreview.UseVisualStyleBackColor = true;
            // 
            // chkSaveAndPrint
            // 
            this.chkSaveAndPrint.AutoSize = true;
            this.chkSaveAndPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaveAndPrint.Location = new System.Drawing.Point(111, 191);
            this.chkSaveAndPrint.Name = "chkSaveAndPrint";
            this.chkSaveAndPrint.Size = new System.Drawing.Size(95, 20);
            this.chkSaveAndPrint.TabIndex = 23;
            this.chkSaveAndPrint.TabStop = false;
            this.chkSaveAndPrint.Text = "Lưu và In?";
            this.chkSaveAndPrint.UseVisualStyleBackColor = true;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(0, 258);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(800, 20);
            this.lblMsg.TabIndex = 35;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(293, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Số tiền";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Ngày thu:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(10, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Lý do:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNguoithu
            // 
            this.txtNguoithu._backcolor = System.Drawing.SystemColors.Control;
            this.txtNguoithu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoithu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoithu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoithu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoithu.AutoCompleteList")));
            this.txtNguoithu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoithu.buildShortcut = false;
            this.txtNguoithu.CaseSensitive = false;
            this.txtNguoithu.CompareNoID = true;
            this.txtNguoithu.DefaultCode = "-1";
            this.txtNguoithu.DefaultID = "-1";
            this.txtNguoithu.DisplayType = 0;
            this.txtNguoithu.Drug_ID = null;
            this.txtNguoithu.ExtraWidth = 0;
            this.txtNguoithu.FillValueAfterSelect = false;
            this.txtNguoithu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoithu.Location = new System.Drawing.Point(112, 87);
            this.txtNguoithu.MaxHeight = 289;
            this.txtNguoithu.MinTypedCharacters = 2;
            this.txtNguoithu.MyCode = "-1";
            this.txtNguoithu.MyID = "-1";
            this.txtNguoithu.MyText = "";
            this.txtNguoithu.MyTextOnly = "";
            this.txtNguoithu.Name = "txtNguoithu";
            this.txtNguoithu.RaiseEvent = true;
            this.txtNguoithu.RaiseEventEnter = true;
            this.txtNguoithu.RaiseEventEnterWhenEmpty = true;
            this.txtNguoithu.SelectedIndex = -1;
            this.txtNguoithu.Size = new System.Drawing.Size(679, 22);
            this.txtNguoithu.splitChar = '@';
            this.txtNguoithu.splitCharIDAndCode = '#';
            this.txtNguoithu.TabIndex = 10;
            this.txtNguoithu.TakeCode = false;
            this.txtNguoithu.txtMyCode = null;
            this.txtNguoithu.txtMyCode_Edit = null;
            this.txtNguoithu.txtMyID = null;
            this.txtNguoithu.txtMyID_Edit = null;
            this.txtNguoithu.txtMyName = null;
            this.txtNguoithu.txtMyName_Edit = null;
            this.txtNguoithu.txtNext = this.autoNguonkiqui;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.SystemColors.Control;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydo.AddValues = true;
            this.txtLydo.AllowMultiline = false;
            this.txtLydo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.buildShortcut = false;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.cmdDropDown = null;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "\"\"";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.LOAI_DANHMUC = "LYDOTAMUNG";
            this.txtLydo.Location = new System.Drawing.Point(112, 63);
            this.txtLydo.MaxHeight = 300;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.ShowCodeWithValue = false;
            this.txtLydo.Size = new System.Drawing.Size(679, 22);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 9;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = this.txtNguoithu;
            this.txtLydo.txtNext1 = null;
            // 
            // lblNguoithuchi
            // 
            this.lblNguoithuchi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguoithuchi.ForeColor = System.Drawing.Color.Red;
            this.lblNguoithuchi.Location = new System.Drawing.Point(10, 87);
            this.lblNguoithuchi.Name = "lblNguoithuchi";
            this.lblNguoithuchi.Size = new System.Drawing.Size(98, 20);
            this.lblNguoithuchi.TabIndex = 20;
            this.lblNguoithuchi.Text = "Người thu:";
            this.lblNguoithuchi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSotien
            // 
            this.txtSotien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(372, 39);
            this.txtSotien.Masked = MaskedTextBox.Mask.Digit;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(117, 22);
            this.txtSotien.TabIndex = 8;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpNgaythu
            // 
            this.dtpNgaythu.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaythu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaythu.DropDownCalendar.Name = "";
            this.dtpNgaythu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaythu.Location = new System.Drawing.Point(112, 39);
            this.dtpNgaythu.Name = "dtpNgaythu";
            this.dtpNgaythu.ShowUpDown = true;
            this.dtpNgaythu.Size = new System.Drawing.Size(173, 22);
            this.dtpNgaythu.TabIndex = 7;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(6, 16);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(31, 20);
            this.txtID.TabIndex = 33;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // lblTongtien
            // 
            this.lblTongtien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongtien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongtien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTongtien.Location = new System.Drawing.Point(0, 567);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(800, 33);
            this.lblTongtien.TabIndex = 37;
            this.lblTongtien.Text = "Tổng tiền:";
            this.lblTongtien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdThuchi
            // 
            this.grdThuchi.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdThuchi.AlternatingColors = true;
            this.grdThuchi.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdThuchi.BackColor = System.Drawing.Color.Silver;
            this.grdThuchi.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin tiền tạm ứng</FilterRowInfoText></LocalizableData>";
            this.grdThuchi.ContextMenuStrip = this.ctxMore;
            grdThuchi_DesignTimeLayout.LayoutString = resources.GetString("grdThuchi_DesignTimeLayout.LayoutString");
            this.grdThuchi.DesignTimeLayout = grdThuchi_DesignTimeLayout;
            this.grdThuchi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThuchi.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThuchi.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThuchi.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThuchi.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdThuchi.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdThuchi.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdThuchi.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdThuchi.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdThuchi.GroupByBoxVisible = false;
            this.grdThuchi.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThuchi.Location = new System.Drawing.Point(0, 278);
            this.grdThuchi.Name = "grdThuchi";
            this.grdThuchi.RecordNavigator = true;
            this.grdThuchi.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThuchi.Size = new System.Drawing.Size(800, 289);
            this.grdThuchi.TabIndex = 39;
            this.grdThuchi.TabStop = false;
            this.grdThuchi.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThuchi.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // ctxMore
            // 
            this.ctxMore.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdPhanbo});
            this.ctxMore.Name = "ctxupdate";
            this.ctxMore.Size = new System.Drawing.Size(234, 26);
            // 
            // cmdPhanbo
            // 
            this.cmdPhanbo.Name = "cmdPhanbo";
            this.cmdPhanbo.Size = new System.Drawing.Size(233, 22);
            this.cmdPhanbo.Text = "Phân bổ hình thức thanh toán";
            this.cmdPhanbo.Click += new System.EventHandler(this.cmdPhanbo_Click);
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(756, 569);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(40, 30);
            this.cmdConfig.TabIndex = 38;
            this.cmdConfig.TabStop = false;
            this.cmdConfig.Visible = false;
            this.cmdConfig.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // ucThuchikhac
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.grdThuchi);
            this.Controls.Add(this.cmdConfig);
            this.Controls.Add(this.lblTongtien);
            this.Controls.Add(this.panel2);
            this.Name = "ucThuchikhac";
            this.Size = new System.Drawing.Size(800, 600);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlFunctions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThuchi)).EndInit();
            this.ctxMore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkPrintPreview;
        private System.Windows.Forms.CheckBox chkSaveAndPrint;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private HIS.UCs.AutoCompleteTextbox txtNguoithu;
        private HIS.UCs.AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.Label lblNguoithuchi;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaythu;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
        private System.Windows.Forms.Label lblTongtien;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        public Janus.Windows.GridEX.GridEX grdThuchi;
        private System.Windows.Forms.Label label1;
        private AutoCompleteTextbox_Danhmucchung autoNguonkiqui;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label7;
        public Janus.Windows.EditControls.UIButton cmdSua;
        public Janus.Windows.EditControls.UIButton cmdthemmoi;
        public Janus.Windows.EditControls.UIButton cmdGhi;
        public Janus.Windows.EditControls.UIButton cmdIn;
        public Janus.Windows.EditControls.UIButton cmdHuy;
        public Janus.Windows.EditControls.UIButton cmdxoa;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIRadioButton optPhieuChi;
        private Janus.Windows.EditControls.UIRadioButton optPhieuThu;
        private System.Windows.Forms.Label label8;
        private AutoCompleteTextbox_Danhmucchung txtMotathem;
        private System.Windows.Forms.ContextMenuStrip ctxMore;
        private System.Windows.Forms.ToolStripMenuItem cmdPhanbo;
        public System.Windows.Forms.Panel pnlFunctions;
        private System.Windows.Forms.Panel panel1;
        public Janus.Windows.EditControls.UIComboBox cboNganhang;
        public Janus.Windows.EditControls.UIComboBox cboPttt;
    }
}
