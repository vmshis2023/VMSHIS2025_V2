namespace VNS.HIS.UI.THUOC
{
    partial class frm_danhmuc_loaithuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_danhmuc_loaithuoc));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtKieuthuocVT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNhom = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkInrieng = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDrug_Code = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sysColor = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbltendanhmuc = new System.Windows.Forms.Label();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdInsert = new Janus.Windows.EditControls.UIButton();
            this.GroupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.sysColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.DataPropertyName = "PaymentMethod_ID";
            this.DataGridViewTextBoxColumn1.HeaderText = "Mã PTTT";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Width = 80;
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.DataPropertyName = "PaymentMethod_Name";
            this.DataGridViewTextBoxColumn2.HeaderText = "Tên PTTT";
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.ReadOnly = true;
            this.DataGridViewTextBoxColumn2.Width = 200;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.DataPropertyName = "Desc";
            this.DataGridViewTextBoxColumn3.HeaderText = "Mô tả thêm";
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            this.DataGridViewTextBoxColumn3.ReadOnly = true;
            this.DataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Desc";
            this.dataGridViewTextBoxColumn4.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(25, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(77, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Mã chủng loại:";
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(118, 19);
            this.txtID.MaxLength = 3;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(128, 21);
            this.txtID.TabIndex = 0;
            // 
            // Label2
            // 
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(6, 49);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(96, 17);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Tên dược lý:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(118, 46);
            this.txtName.MaxLength = 255;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(717, 21);
            this.txtName.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(6, 97);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(96, 17);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Mô tả thêm:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(118, 94);
            this.txtDesc.MaxLength = 255;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(861, 21);
            this.txtDesc.TabIndex = 5;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(841, 46);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(60, 15);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "Số thứ tự:";
            // 
            // txtPos
            // 
            this.txtPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPos.BackColor = System.Drawing.Color.White;
            this.txtPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPos.Location = new System.Drawing.Point(907, 43);
            this.txtPos.MaxLength = 5;
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(72, 21);
            this.txtPos.TabIndex = 2;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox1.Controls.Add(this.txtKieuthuocVT);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Controls.Add(this.txtNhom);
            this.GroupBox1.Controls.Add(this.lblMsg);
            this.GroupBox1.Controls.Add(this.chkInrieng);
            this.GroupBox1.Controls.Add(this.label8);
            this.GroupBox1.Controls.Add(this.txtDrug_Code);
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.txtPos);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.txtDesc);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.txtName);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtID);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupBox1.Location = new System.Drawing.Point(3, 393);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(985, 145);
            this.GroupBox1.TabIndex = 9;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Thông tin chi tiết";
            // 
            // txtKieuthuocVT
            // 
            this.txtKieuthuocVT._backcolor = System.Drawing.SystemColors.Control;
            this.txtKieuthuocVT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuthuocVT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKieuthuocVT.AddValues = true;
            this.txtKieuthuocVT.AllowMultiline = false;
            this.txtKieuthuocVT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKieuthuocVT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKieuthuocVT.AutoCompleteList")));
            this.txtKieuthuocVT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKieuthuocVT.buildShortcut = false;
            this.txtKieuthuocVT.CaseSensitive = false;
            this.txtKieuthuocVT.CompareNoID = true;
            this.txtKieuthuocVT.DefaultCode = "-1";
            this.txtKieuthuocVT.DefaultID = "-1";
            this.txtKieuthuocVT.Drug_ID = null;
            this.txtKieuthuocVT.ExtraWidth = 0;
            this.txtKieuthuocVT.FillValueAfterSelect = false;
            this.txtKieuthuocVT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuthuocVT.LOAI_DANHMUC = "KIEUTHUOCVT";
            this.txtKieuthuocVT.Location = new System.Drawing.Point(528, 70);
            this.txtKieuthuocVT.MaxHeight = -1;
            this.txtKieuthuocVT.MinTypedCharacters = 2;
            this.txtKieuthuocVT.MyCode = "-1";
            this.txtKieuthuocVT.MyID = "-1";
            this.txtKieuthuocVT.Name = "txtKieuthuocVT";
            this.txtKieuthuocVT.RaiseEvent = false;
            this.txtKieuthuocVT.RaiseEventEnter = false;
            this.txtKieuthuocVT.RaiseEventEnterWhenEmpty = false;
            this.txtKieuthuocVT.SelectedIndex = -1;
            this.txtKieuthuocVT.ShowCodeWithValue = false;
            this.txtKieuthuocVT.Size = new System.Drawing.Size(451, 21);
            this.txtKieuthuocVT.splitChar = '@';
            this.txtKieuthuocVT.splitCharIDAndCode = '#';
            this.txtKieuthuocVT.TabIndex = 4;
            this.txtKieuthuocVT.TakeCode = false;
            this.txtKieuthuocVT.txtMyCode = null;
            this.txtKieuthuocVT.txtMyCode_Edit = null;
            this.txtKieuthuocVT.txtMyID = null;
            this.txtKieuthuocVT.txtMyID_Edit = null;
            this.txtKieuthuocVT.txtMyName = null;
            this.txtKieuthuocVT.txtMyName_Edit = null;
            this.txtKieuthuocVT.txtNext = null;
            this.txtKieuthuocVT.txtNext1 = null;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(411, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 505;
            this.label7.Text = "Kiểu Nhóm:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNhom
            // 
            this.txtNhom._backcolor = System.Drawing.SystemColors.Control;
            this.txtNhom._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhom._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhom.AddValues = true;
            this.txtNhom.AllowMultiline = false;
            this.txtNhom.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhom.AutoCompleteList")));
            this.txtNhom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhom.buildShortcut = false;
            this.txtNhom.CaseSensitive = false;
            this.txtNhom.CompareNoID = true;
            this.txtNhom.DefaultCode = "-1";
            this.txtNhom.DefaultID = "-1";
            this.txtNhom.Drug_ID = null;
            this.txtNhom.ExtraWidth = 0;
            this.txtNhom.FillValueAfterSelect = false;
            this.txtNhom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhom.LOAI_DANHMUC = "NHOMTHUOC";
            this.txtNhom.Location = new System.Drawing.Point(118, 70);
            this.txtNhom.MaxHeight = -1;
            this.txtNhom.MinTypedCharacters = 2;
            this.txtNhom.MyCode = "-1";
            this.txtNhom.MyID = "-1";
            this.txtNhom.Name = "txtNhom";
            this.txtNhom.RaiseEvent = false;
            this.txtNhom.RaiseEventEnter = false;
            this.txtNhom.RaiseEventEnterWhenEmpty = false;
            this.txtNhom.SelectedIndex = -1;
            this.txtNhom.ShowCodeWithValue = false;
            this.txtNhom.Size = new System.Drawing.Size(272, 21);
            this.txtNhom.splitChar = '@';
            this.txtNhom.splitCharIDAndCode = '#';
            this.txtNhom.TabIndex = 3;
            this.txtNhom.TakeCode = false;
            this.txtNhom.txtMyCode = null;
            this.txtNhom.txtMyCode_Edit = null;
            this.txtNhom.txtMyID = null;
            this.txtNhom.txtMyID_Edit = null;
            this.txtNhom.txtMyName = null;
            this.txtNhom.txtMyName_Edit = null;
            this.txtNhom.txtNext = null;
            this.txtNhom.txtNext1 = null;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(204, 119);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(775, 25);
            this.lblMsg.TabIndex = 503;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkInrieng
            // 
            this.chkInrieng.AutoSize = true;
            this.chkInrieng.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInrieng.Location = new System.Drawing.Point(118, 121);
            this.chkInrieng.Name = "chkInrieng";
            this.chkInrieng.Size = new System.Drawing.Size(74, 19);
            this.chkInrieng.TabIndex = 5;
            this.chkInrieng.TabStop = false;
            this.chkInrieng.Text = "In riêng?";
            this.chkInrieng.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(6, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 501;
            this.label8.Text = "Nhóm:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDrug_Code
            // 
            this.txtDrug_Code.BackColor = System.Drawing.Color.White;
            this.txtDrug_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDrug_Code.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrug_Code.Location = new System.Drawing.Point(118, 19);
            this.txtDrug_Code.MaxLength = 20;
            this.txtDrug_Code.Name = "txtDrug_Code";
            this.txtDrug_Code.Size = new System.Drawing.Size(128, 21);
            this.txtDrug_Code.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mã dược lý:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(999, 569);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdList);
            this.tabPage1.Controls.Add(this.GroupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(991, 541);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thông tin nhóm dược lý";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin dùng chung</FilterRowInfoText></LocalizableData>";
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 3);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(988, 381);
            this.grdList.TabIndex = 11;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel11});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabel1.Text = "Ctrl+N: Thêm mới             ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(160, 17);
            this.toolStripStatusLabel3.Text = "Ctrl+C: Cập nhập             ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(108, 17);
            this.toolStripStatusLabel5.Text = "Del: Xóa             ";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(120, 17);
            this.toolStripStatusLabel7.Text = "Ctrl+S: Lưu           ";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel9.Text = "Ctrl+P: In ấn          ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel2.Text = "F5: Làm mới dữ liệu";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel11.Text = "Escape: Thoát, hủy";
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.Controls.Add(this.pictureBox1);
            this.sysColor.Controls.Add(this.label4);
            this.sysColor.Controls.Add(this.lbltendanhmuc);
            this.sysColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(1008, 58);
            this.sysColor.TabIndex = 32;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 58);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(73, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(924, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "( BẠN CÓ THỂ DÙNG PHÍM TẮT THAO TÁC)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbltendanhmuc
            // 
            this.lbltendanhmuc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbltendanhmuc.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltendanhmuc.Location = new System.Drawing.Point(67, 3);
            this.lbltendanhmuc.Name = "lbltendanhmuc";
            this.lbltendanhmuc.Size = new System.Drawing.Size(937, 31);
            this.lbltendanhmuc.TabIndex = 3;
            this.lbltendanhmuc.Text = "DANH MỤC CHỦNG LOẠI THUỐC (NHÓM DƯỢC LÝ)";
            this.lbltendanhmuc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(877, 653);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 21;
            this.cmdClose.Text = "Thoát";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(751, 653);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 35);
            this.cmdPrint.TabIndex = 20;
            this.cmdPrint.Text = "In";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(625, 653);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "Ghi";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdDelete.Location = new System.Drawing.Point(499, 653);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(120, 35);
            this.cmdDelete.TabIndex = 19;
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Near;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdUpdate.Location = new System.Drawing.Point(373, 653);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(120, 35);
            this.cmdUpdate.TabIndex = 18;
            this.cmdUpdate.Text = "Sửa";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.Image = ((System.Drawing.Image)(resources.GetObject("cmdInsert.Image")));
            this.cmdInsert.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInsert.Location = new System.Drawing.Point(247, 653);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(120, 35);
            this.cmdInsert.TabIndex = 17;
            this.cmdInsert.Text = "Thêm";
            // 
            // frm_danhmuc_loaithuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.sysColor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_danhmuc_loaithuoc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục nhóm dược lý";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.sysColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtDesc;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtPos;
        internal System.Windows.Forms.GroupBox GroupBox1;
      //  private VBLine vbLine1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.Panel sysColor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbltendanhmuc;
        internal System.Windows.Forms.TextBox txtDrug_Code;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkInrieng;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdInsert;
        internal System.Windows.Forms.Label lblMsg;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNhom;
        private UCs.AutoCompleteTextbox_Danhmucchung txtKieuthuocVT;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}