namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class FrmDoiPhongKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDoiPhongKham));
            Janus.Windows.GridEX.GridEXLayout cboKieuKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout multiColumnCombo1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlChonKieukham = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKieuKham = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtPhongkham = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlChonphongkham = new System.Windows.Forms.Panel();
            this.txtExamtypeCode = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtMyNameEdit = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboKieuKham = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.cmdDoiPhong = new Janus.Windows.EditControls.UIButton();
            this.cmdThoat = new Janus.Windows.EditControls.UIButton();
            this.pnlChonphongkhamold = new System.Windows.Forms.Panel();
            this.autoCompleteTextbox1 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.editBox1 = new Janus.Windows.GridEX.EditControls.EditBox();
            this.multiColumnCombo1 = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.autoCompleteTextbox2 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoCompleteTextbox3 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.pnlChonKieukham.SuspendLayout();
            this.pnlChonphongkham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKieuKham)).BeginInit();
            this.pnlChonphongkhamold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.multiColumnCombo1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pnlChonKieukham);
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(13, 101);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(594, 28);
            this.panel5.TabIndex = 601;
            this.panel5.TabStop = true;
            // 
            // pnlChonKieukham
            // 
            this.pnlChonKieukham.Controls.Add(this.label1);
            this.pnlChonKieukham.Controls.Add(this.txtKieuKham);
            this.pnlChonKieukham.Controls.Add(this.txtPhongkham);
            this.pnlChonKieukham.Controls.Add(this.label23);
            this.pnlChonKieukham.Location = new System.Drawing.Point(0, 0);
            this.pnlChonKieukham.Name = "pnlChonKieukham";
            this.pnlChonKieukham.Size = new System.Drawing.Size(594, 28);
            this.pnlChonKieukham.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 539;
            this.label1.Text = "Kiểu khám:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKieuKham
            // 
            this.txtKieuKham._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtKieuKham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuKham._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKieuKham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKieuKham.AutoCompleteList")));
            this.txtKieuKham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKieuKham.CaseSensitive = false;
            this.txtKieuKham.CompareNoID = true;
            this.txtKieuKham.DefaultCode = "-1";
            this.txtKieuKham.DefaultID = "-1";
            this.txtKieuKham.Drug_ID = null;
            this.txtKieuKham.ExtraWidth = 0;
            this.txtKieuKham.FillValueAfterSelect = false;
            this.txtKieuKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuKham.Location = new System.Drawing.Point(78, 4);
            this.txtKieuKham.MaxHeight = 289;
            this.txtKieuKham.MinTypedCharacters = 2;
            this.txtKieuKham.MyCode = "-1";
            this.txtKieuKham.MyID = "-1";
            this.txtKieuKham.MyText = "";
            this.txtKieuKham.Name = "txtKieuKham";
            this.txtKieuKham.RaiseEvent = true;
            this.txtKieuKham.RaiseEventEnter = true;
            this.txtKieuKham.RaiseEventEnterWhenEmpty = true;
            this.txtKieuKham.SelectedIndex = -1;
            this.txtKieuKham.Size = new System.Drawing.Size(210, 21);
            this.txtKieuKham.splitChar = '@';
            this.txtKieuKham.splitCharIDAndCode = '#';
            this.txtKieuKham.TabIndex = 23;
            this.txtKieuKham.TabStop = false;
            this.txtKieuKham.TakeCode = false;
            this.txtKieuKham.txtMyCode = null;
            this.txtKieuKham.txtMyCode_Edit = null;
            this.txtKieuKham.txtMyID = null;
            this.txtKieuKham.txtMyID_Edit = null;
            this.txtKieuKham.txtMyName = null;
            this.txtKieuKham.txtMyName_Edit = null;
            this.txtKieuKham.txtNext = null;
            // 
            // txtPhongkham
            // 
            this.txtPhongkham._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPhongkham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhongkham._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhongkham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhongkham.AutoCompleteList")));
            this.txtPhongkham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhongkham.CaseSensitive = false;
            this.txtPhongkham.CompareNoID = true;
            this.txtPhongkham.DefaultCode = "-1";
            this.txtPhongkham.DefaultID = "-1";
            this.txtPhongkham.Drug_ID = null;
            this.txtPhongkham.ExtraWidth = 0;
            this.txtPhongkham.FillValueAfterSelect = false;
            this.txtPhongkham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhongkham.Location = new System.Drawing.Point(370, 4);
            this.txtPhongkham.MaxHeight = 289;
            this.txtPhongkham.MinTypedCharacters = 2;
            this.txtPhongkham.MyCode = "-1";
            this.txtPhongkham.MyID = "-1";
            this.txtPhongkham.MyText = "";
            this.txtPhongkham.Name = "txtPhongkham";
            this.txtPhongkham.RaiseEvent = true;
            this.txtPhongkham.RaiseEventEnter = true;
            this.txtPhongkham.RaiseEventEnterWhenEmpty = true;
            this.txtPhongkham.SelectedIndex = -1;
            this.txtPhongkham.Size = new System.Drawing.Size(221, 21);
            this.txtPhongkham.splitChar = '@';
            this.txtPhongkham.splitCharIDAndCode = '#';
            this.txtPhongkham.TabIndex = 24;
            this.txtPhongkham.TabStop = false;
            this.txtPhongkham.TakeCode = false;
            this.txtPhongkham.txtMyCode = null;
            this.txtPhongkham.txtMyCode_Edit = null;
            this.txtPhongkham.txtMyID = null;
            this.txtPhongkham.txtMyID_Edit = null;
            this.txtPhongkham.txtMyName = null;
            this.txtPhongkham.txtMyName_Edit = null;
            this.txtPhongkham.txtNext = null;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Navy;
            this.label23.Location = new System.Drawing.Point(290, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 21);
            this.label23.TabIndex = 538;
            this.label23.Text = "Phòng khám:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlChonphongkham
            // 
            this.pnlChonphongkham.Controls.Add(this.txtExamtypeCode);
            this.pnlChonphongkham.Controls.Add(this.txtMyNameEdit);
            this.pnlChonphongkham.Controls.Add(this.cboKieuKham);
            this.pnlChonphongkham.Location = new System.Drawing.Point(13, 101);
            this.pnlChonphongkham.Name = "pnlChonphongkham";
            this.pnlChonphongkham.Size = new System.Drawing.Size(595, 28);
            this.pnlChonphongkham.TabIndex = 1;
            // 
            // txtExamtypeCode
            // 
            this.txtExamtypeCode._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtExamtypeCode._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExamtypeCode._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtExamtypeCode.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtExamtypeCode.AutoCompleteList")));
            this.txtExamtypeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExamtypeCode.CaseSensitive = false;
            this.txtExamtypeCode.CompareNoID = true;
            this.txtExamtypeCode.DefaultCode = "-1";
            this.txtExamtypeCode.DefaultID = "-1";
            this.txtExamtypeCode.Drug_ID = null;
            this.txtExamtypeCode.ExtraWidth = 400;
            this.txtExamtypeCode.FillValueAfterSelect = false;
            this.txtExamtypeCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExamtypeCode.Location = new System.Drawing.Point(3, 4);
            this.txtExamtypeCode.MaxHeight = 289;
            this.txtExamtypeCode.MinTypedCharacters = 2;
            this.txtExamtypeCode.MyCode = "-1";
            this.txtExamtypeCode.MyID = "-1";
            this.txtExamtypeCode.MyText = "";
            this.txtExamtypeCode.Name = "txtExamtypeCode";
            this.txtExamtypeCode.RaiseEvent = true;
            this.txtExamtypeCode.RaiseEventEnter = false;
            this.txtExamtypeCode.RaiseEventEnterWhenEmpty = false;
            this.txtExamtypeCode.SelectedIndex = -1;
            this.txtExamtypeCode.Size = new System.Drawing.Size(70, 21);
            this.txtExamtypeCode.splitChar = '@';
            this.txtExamtypeCode.splitCharIDAndCode = '#';
            this.txtExamtypeCode.TabIndex = 25;
            this.txtExamtypeCode.TakeCode = true;
            this.txtExamtypeCode.txtMyCode = null;
            this.txtExamtypeCode.txtMyCode_Edit = null;
            this.txtExamtypeCode.txtMyID = null;
            this.txtExamtypeCode.txtMyID_Edit = null;
            this.txtExamtypeCode.txtMyName = null;
            this.txtExamtypeCode.txtMyName_Edit = this.txtMyNameEdit;
            this.txtExamtypeCode.txtNext = null;
            // 
            // txtMyNameEdit
            // 
            this.txtMyNameEdit.BackColor = System.Drawing.Color.White;
            this.txtMyNameEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMyNameEdit.Location = new System.Drawing.Point(691, 3);
            this.txtMyNameEdit.Name = "txtMyNameEdit";
            this.txtMyNameEdit.Size = new System.Drawing.Size(10, 23);
            this.txtMyNameEdit.TabIndex = 550;
            this.txtMyNameEdit.TabStop = false;
            this.txtMyNameEdit.Visible = false;
            // 
            // cboKieuKham
            // 
            this.cboKieuKham.AllowDrop = true;
            this.cboKieuKham.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            cboKieuKham_DesignTimeLayout.LayoutString = resources.GetString("cboKieuKham_DesignTimeLayout.LayoutString");
            this.cboKieuKham.DesignTimeLayout = cboKieuKham_DesignTimeLayout;
            this.cboKieuKham.DisplayMember = "_name";
            this.cboKieuKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKieuKham.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.cboKieuKham.Location = new System.Drawing.Point(79, 4);
            this.cboKieuKham.Name = "cboKieuKham";
            this.cboKieuKham.SelectedIndex = -1;
            this.cboKieuKham.SelectedItem = null;
            this.cboKieuKham.Size = new System.Drawing.Size(513, 21);
            this.cboKieuKham.TabIndex = 26;
            this.cboKieuKham.Text = "CHỌN DỊCH VỤ KCB";
            this.cboKieuKham.ValueMember = "ID";
            this.cboKieuKham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdDoiPhong
            // 
            this.cmdDoiPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDoiPhong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDoiPhong.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdDoiPhong.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdDoiPhong.Location = new System.Drawing.Point(191, 182);
            this.cmdDoiPhong.Name = "cmdDoiPhong";
            this.cmdDoiPhong.Size = new System.Drawing.Size(121, 30);
            this.cmdDoiPhong.TabIndex = 602;
            this.cmdDoiPhong.TabStop = false;
            this.cmdDoiPhong.Text = "Chấp nhận";
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdThoat.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdThoat.Location = new System.Drawing.Point(318, 182);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(121, 30);
            this.cmdThoat.TabIndex = 603;
            this.cmdThoat.TabStop = false;
            this.cmdThoat.Text = "Thoát";
            // 
            // pnlChonphongkhamold
            // 
            this.pnlChonphongkhamold.Controls.Add(this.autoCompleteTextbox1);
            this.pnlChonphongkhamold.Controls.Add(this.editBox1);
            this.pnlChonphongkhamold.Controls.Add(this.multiColumnCombo1);
            this.pnlChonphongkhamold.Location = new System.Drawing.Point(13, 40);
            this.pnlChonphongkhamold.Name = "pnlChonphongkhamold";
            this.pnlChonphongkhamold.Size = new System.Drawing.Size(595, 28);
            this.pnlChonphongkhamold.TabIndex = 604;
            // 
            // autoCompleteTextbox1
            // 
            this.autoCompleteTextbox1._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoCompleteTextbox1._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox1._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextbox1.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoCompleteTextbox1.AutoCompleteList")));
            this.autoCompleteTextbox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextbox1.CaseSensitive = false;
            this.autoCompleteTextbox1.CompareNoID = true;
            this.autoCompleteTextbox1.DefaultCode = "-1";
            this.autoCompleteTextbox1.DefaultID = "-1";
            this.autoCompleteTextbox1.Drug_ID = null;
            this.autoCompleteTextbox1.ExtraWidth = 400;
            this.autoCompleteTextbox1.FillValueAfterSelect = false;
            this.autoCompleteTextbox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox1.Location = new System.Drawing.Point(3, 4);
            this.autoCompleteTextbox1.MaxHeight = 289;
            this.autoCompleteTextbox1.MinTypedCharacters = 2;
            this.autoCompleteTextbox1.MyCode = "-1";
            this.autoCompleteTextbox1.MyID = "-1";
            this.autoCompleteTextbox1.MyText = "";
            this.autoCompleteTextbox1.Name = "autoCompleteTextbox1";
            this.autoCompleteTextbox1.RaiseEvent = true;
            this.autoCompleteTextbox1.RaiseEventEnter = false;
            this.autoCompleteTextbox1.RaiseEventEnterWhenEmpty = false;
            this.autoCompleteTextbox1.SelectedIndex = -1;
            this.autoCompleteTextbox1.Size = new System.Drawing.Size(70, 21);
            this.autoCompleteTextbox1.splitChar = '@';
            this.autoCompleteTextbox1.splitCharIDAndCode = '#';
            this.autoCompleteTextbox1.TabIndex = 25;
            this.autoCompleteTextbox1.TakeCode = true;
            this.autoCompleteTextbox1.txtMyCode = null;
            this.autoCompleteTextbox1.txtMyCode_Edit = null;
            this.autoCompleteTextbox1.txtMyID = null;
            this.autoCompleteTextbox1.txtMyID_Edit = null;
            this.autoCompleteTextbox1.txtMyName = null;
            this.autoCompleteTextbox1.txtMyName_Edit = this.editBox1;
            this.autoCompleteTextbox1.txtNext = null;
            // 
            // editBox1
            // 
            this.editBox1.BackColor = System.Drawing.Color.White;
            this.editBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBox1.Location = new System.Drawing.Point(691, 3);
            this.editBox1.Name = "editBox1";
            this.editBox1.Size = new System.Drawing.Size(10, 23);
            this.editBox1.TabIndex = 550;
            this.editBox1.TabStop = false;
            this.editBox1.Visible = false;
            // 
            // multiColumnCombo1
            // 
            this.multiColumnCombo1.AllowDrop = true;
            this.multiColumnCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            multiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("multiColumnCombo1_DesignTimeLayout.LayoutString");
            this.multiColumnCombo1.DesignTimeLayout = multiColumnCombo1_DesignTimeLayout;
            this.multiColumnCombo1.DisplayMember = "_name";
            this.multiColumnCombo1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiColumnCombo1.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.multiColumnCombo1.Location = new System.Drawing.Point(79, 4);
            this.multiColumnCombo1.Name = "multiColumnCombo1";
            this.multiColumnCombo1.SelectedIndex = -1;
            this.multiColumnCombo1.SelectedItem = null;
            this.multiColumnCombo1.Size = new System.Drawing.Size(513, 21);
            this.multiColumnCombo1.TabIndex = 26;
            this.multiColumnCombo1.Text = "CHỌN DỊCH VỤ KCB";
            this.multiColumnCombo1.ValueMember = "ID";
            this.multiColumnCombo1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(14, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 28);
            this.panel2.TabIndex = 605;
            this.panel2.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.autoCompleteTextbox2);
            this.panel3.Controls.Add(this.autoCompleteTextbox3);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 28);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 21);
            this.label2.TabIndex = 539;
            this.label2.Text = "Kiểu khám:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoCompleteTextbox2
            // 
            this.autoCompleteTextbox2._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoCompleteTextbox2._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox2._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextbox2.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoCompleteTextbox2.AutoCompleteList")));
            this.autoCompleteTextbox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextbox2.CaseSensitive = false;
            this.autoCompleteTextbox2.CompareNoID = true;
            this.autoCompleteTextbox2.DefaultCode = "-1";
            this.autoCompleteTextbox2.DefaultID = "-1";
            this.autoCompleteTextbox2.Drug_ID = null;
            this.autoCompleteTextbox2.ExtraWidth = 0;
            this.autoCompleteTextbox2.FillValueAfterSelect = false;
            this.autoCompleteTextbox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox2.Location = new System.Drawing.Point(78, 4);
            this.autoCompleteTextbox2.MaxHeight = 289;
            this.autoCompleteTextbox2.MinTypedCharacters = 2;
            this.autoCompleteTextbox2.MyCode = "-1";
            this.autoCompleteTextbox2.MyID = "-1";
            this.autoCompleteTextbox2.MyText = "";
            this.autoCompleteTextbox2.Name = "autoCompleteTextbox2";
            this.autoCompleteTextbox2.RaiseEvent = true;
            this.autoCompleteTextbox2.RaiseEventEnter = true;
            this.autoCompleteTextbox2.RaiseEventEnterWhenEmpty = true;
            this.autoCompleteTextbox2.SelectedIndex = -1;
            this.autoCompleteTextbox2.Size = new System.Drawing.Size(210, 21);
            this.autoCompleteTextbox2.splitChar = '@';
            this.autoCompleteTextbox2.splitCharIDAndCode = '#';
            this.autoCompleteTextbox2.TabIndex = 23;
            this.autoCompleteTextbox2.TabStop = false;
            this.autoCompleteTextbox2.TakeCode = false;
            this.autoCompleteTextbox2.txtMyCode = null;
            this.autoCompleteTextbox2.txtMyCode_Edit = null;
            this.autoCompleteTextbox2.txtMyID = null;
            this.autoCompleteTextbox2.txtMyID_Edit = null;
            this.autoCompleteTextbox2.txtMyName = null;
            this.autoCompleteTextbox2.txtMyName_Edit = null;
            this.autoCompleteTextbox2.txtNext = null;
            // 
            // autoCompleteTextbox3
            // 
            this.autoCompleteTextbox3._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoCompleteTextbox3._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox3._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextbox3.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoCompleteTextbox3.AutoCompleteList")));
            this.autoCompleteTextbox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextbox3.CaseSensitive = false;
            this.autoCompleteTextbox3.CompareNoID = true;
            this.autoCompleteTextbox3.DefaultCode = "-1";
            this.autoCompleteTextbox3.DefaultID = "-1";
            this.autoCompleteTextbox3.Drug_ID = null;
            this.autoCompleteTextbox3.ExtraWidth = 0;
            this.autoCompleteTextbox3.FillValueAfterSelect = false;
            this.autoCompleteTextbox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox3.Location = new System.Drawing.Point(370, 4);
            this.autoCompleteTextbox3.MaxHeight = 289;
            this.autoCompleteTextbox3.MinTypedCharacters = 2;
            this.autoCompleteTextbox3.MyCode = "-1";
            this.autoCompleteTextbox3.MyID = "-1";
            this.autoCompleteTextbox3.MyText = "";
            this.autoCompleteTextbox3.Name = "autoCompleteTextbox3";
            this.autoCompleteTextbox3.RaiseEvent = true;
            this.autoCompleteTextbox3.RaiseEventEnter = true;
            this.autoCompleteTextbox3.RaiseEventEnterWhenEmpty = true;
            this.autoCompleteTextbox3.SelectedIndex = -1;
            this.autoCompleteTextbox3.Size = new System.Drawing.Size(221, 21);
            this.autoCompleteTextbox3.splitChar = '@';
            this.autoCompleteTextbox3.splitCharIDAndCode = '#';
            this.autoCompleteTextbox3.TabIndex = 24;
            this.autoCompleteTextbox3.TabStop = false;
            this.autoCompleteTextbox3.TakeCode = false;
            this.autoCompleteTextbox3.txtMyCode = null;
            this.autoCompleteTextbox3.txtMyCode_Edit = null;
            this.autoCompleteTextbox3.txtMyID = null;
            this.autoCompleteTextbox3.txtMyID_Edit = null;
            this.autoCompleteTextbox3.txtMyName = null;
            this.autoCompleteTextbox3.txtMyName_Edit = null;
            this.autoCompleteTextbox3.txtNext = null;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(290, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 538;
            this.label3.Text = "Phòng khám:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(594, 21);
            this.label4.TabIndex = 606;
            this.label4.Text = "Dịch vụ phòng khám hiện tại";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(13, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(594, 21);
            this.label5.TabIndex = 607;
            this.label5.Text = "Dịch vụ phòng khám mới";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydo.AddValues = true;
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "-1";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.LOAI_DANHMUC = "LYDOCHUYENPHONGKHAM";
            this.txtLydo.Location = new System.Drawing.Point(92, 146);
            this.txtLydo.MaxHeight = 150;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.Size = new System.Drawing.Size(515, 21);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 608;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = null;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 22);
            this.label6.TabIndex = 609;
            this.label6.Text = "Lý do chuyển:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FrmDoiPhongKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 224);
            this.Controls.Add(this.txtLydo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlChonphongkhamold);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdDoiPhong);
            this.Controls.Add(this.pnlChonphongkham);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDoiPhongKham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi phòng khám";
            this.panel5.ResumeLayout(false);
            this.pnlChonKieukham.ResumeLayout(false);
            this.pnlChonKieukham.PerformLayout();
            this.pnlChonphongkham.ResumeLayout(false);
            this.pnlChonphongkham.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKieuKham)).EndInit();
            this.pnlChonphongkhamold.ResumeLayout(false);
            this.pnlChonphongkhamold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.multiColumnCombo1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlChonKieukham;
        private System.Windows.Forms.Label label1;
        private HIS.UCs.AutoCompleteTextbox txtKieuKham;
        private HIS.UCs.AutoCompleteTextbox txtPhongkham;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlChonphongkham;
        private HIS.UCs.AutoCompleteTextbox txtExamtypeCode;
        private Janus.Windows.GridEX.EditControls.EditBox txtMyNameEdit;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo cboKieuKham;
        private Janus.Windows.EditControls.UIButton cmdDoiPhong;
        private Janus.Windows.EditControls.UIButton cmdThoat;
        private System.Windows.Forms.Panel pnlChonphongkhamold;
        private HIS.UCs.AutoCompleteTextbox autoCompleteTextbox1;
        private Janus.Windows.GridEX.EditControls.EditBox editBox1;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo multiColumnCombo1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private HIS.UCs.AutoCompleteTextbox autoCompleteTextbox2;
        private HIS.UCs.AutoCompleteTextbox autoCompleteTextbox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private HIS.UCs.AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.Label label6;

    }
}