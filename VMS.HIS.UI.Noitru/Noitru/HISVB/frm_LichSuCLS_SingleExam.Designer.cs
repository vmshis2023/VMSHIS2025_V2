namespace VietBaIT.HISLink.UI.ControlUtility.LichSuCLS
{
    partial class frm_LichSuCLS_SingleExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LichSuCLS_SingleExam));
            Janus.Windows.GridEX.GridEXLayout grdAssignDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdKetQuaXN_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdKetQuaCDHA_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAccept = new Janus.Windows.EditControls.UIButton();
            this.btnCancel = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdAssignDetail = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtYearBirth = new System.Windows.Forms.TextBox();
            this.txtSex = new System.Windows.Forms.TextBox();
            this.txtPatientID = new System.Windows.Forms.TextBox();
            this.txtPatientCode = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.grpOption = new System.Windows.Forms.GroupBox();
            this.radTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.cmdGetData = new Janus.Windows.EditControls.UIButton();
            this.grpOptionKetQua = new System.Windows.Forms.GroupBox();
            this.radTatCaKQ = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaCoKetQua = new Janus.Windows.EditControls.UIRadioButton();
            this.radChuaCoKetQua = new Janus.Windows.EditControls.UIRadioButton();
            this.grpKetQuaXN = new Janus.Windows.EditControls.UIGroupBox();
            this.grdKetQuaXN = new Janus.Windows.GridEX.GridEX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpKetQuaCDHA = new System.Windows.Forms.GroupBox();
            this.grdKetQuaCDHA = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).BeginInit();
            this.panel2.SuspendLayout();
            this.grpOption.SuspendLayout();
            this.grpOptionKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpKetQuaXN)).BeginInit();
            this.grpKetQuaXN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQuaXN)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpKetQuaCDHA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQuaCDHA)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 527);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 46);
            this.panel1.TabIndex = 1;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.Location = new System.Drawing.Point(370, 8);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(111, 30);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Chọn";
            this.btnAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(487, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Thoát(Esc)";
            this.btnCancel.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdAssignDetail);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(385, 465);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "Thông tin cận lâm sàng";
            // 
            // grdAssignDetail
            // 
            this.grdAssignDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdAssignDetail_DesignTimeLayout.LayoutString = resources.GetString("grdAssignDetail_DesignTimeLayout.LayoutString");
            this.grdAssignDetail.DesignTimeLayout = grdAssignDetail_DesignTimeLayout;
            this.grdAssignDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAssignDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdAssignDetail.GroupByBoxVisible = false;
            this.grdAssignDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdAssignDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdAssignDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdAssignDetail.Location = new System.Drawing.Point(3, 17);
            this.grdAssignDetail.Name = "grdAssignDetail";
            this.grdAssignDetail.RecordNavigator = true;
            this.grdAssignDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.Size = new System.Drawing.Size(379, 445);
            this.grdAssignDetail.TabIndex = 12;
            this.grdAssignDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdAssignDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdAssignDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdAssignDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdAssignDetail.UseGroupRowSelector = true;
            this.grdAssignDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdAssignDetail.SelectionChanged += new System.EventHandler(this.grdAssignDetail_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtYearBirth);
            this.panel2.Controls.Add(this.txtSex);
            this.panel2.Controls.Add(this.txtPatientID);
            this.panel2.Controls.Add(this.txtPatientCode);
            this.panel2.Controls.Add(this.txtPatientName);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.grpOption);
            this.panel2.Controls.Add(this.cmdGetData);
            this.panel2.Controls.Add(this.grpOptionKetQua);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(949, 62);
            this.panel2.TabIndex = 3;
            // 
            // txtYearBirth
            // 
            this.txtYearBirth.BackColor = System.Drawing.Color.White;
            this.txtYearBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearBirth.Location = new System.Drawing.Point(378, 8);
            this.txtYearBirth.MaxLength = 99999;
            this.txtYearBirth.Name = "txtYearBirth";
            this.txtYearBirth.ReadOnly = true;
            this.txtYearBirth.Size = new System.Drawing.Size(84, 20);
            this.txtYearBirth.TabIndex = 27;
            this.txtYearBirth.TabStop = false;
            this.txtYearBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSex
            // 
            this.txtSex.BackColor = System.Drawing.Color.White;
            this.txtSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.Location = new System.Drawing.Point(329, 8);
            this.txtSex.MaxLength = 9999;
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(47, 20);
            this.txtSex.TabIndex = 26;
            this.txtSex.TabStop = false;
            // 
            // txtPatientID
            // 
            this.txtPatientID.BackColor = System.Drawing.Color.White;
            this.txtPatientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientID.Location = new System.Drawing.Point(224, 8);
            this.txtPatientID.MaxLength = 9999;
            this.txtPatientID.Name = "txtPatientID";
            this.txtPatientID.ReadOnly = true;
            this.txtPatientID.Size = new System.Drawing.Size(75, 20);
            this.txtPatientID.TabIndex = 22;
            this.txtPatientID.TabStop = false;
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtPatientCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(106, 8);
            this.txtPatientCode.MaxLength = 9999;
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(117, 20);
            this.txtPatientCode.TabIndex = 21;
            this.txtPatientCode.TabStop = false;
            // 
            // txtPatientName
            // 
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(106, 30);
            this.txtPatientName.MaxLength = 999999;
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(356, 20);
            this.txtPatientName.TabIndex = 23;
            this.txtPatientName.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label13.Location = new System.Drawing.Point(17, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Họ và tên";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(17, 12);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(68, 13);
            this.label28.TabIndex = 24;
            this.label28.Text = "Mã lần khám";
            // 
            // grpOption
            // 
            this.grpOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOption.Controls.Add(this.radTatCa);
            this.grpOption.Controls.Add(this.radNgoaiTru);
            this.grpOption.Controls.Add(this.radNoiTru);
            this.grpOption.Location = new System.Drawing.Point(468, 0);
            this.grpOption.Name = "grpOption";
            this.grpOption.Size = new System.Drawing.Size(347, 32);
            this.grpOption.TabIndex = 6;
            this.grpOption.TabStop = false;
            // 
            // radTatCa
            // 
            this.radTatCa.AutoSize = true;
            this.radTatCa.Checked = true;
            this.radTatCa.Location = new System.Drawing.Point(18, 10);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(48, 17);
            this.radTatCa.TabIndex = 0;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.AutoSize = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(138, 10);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(60, 17);
            this.radNgoaiTru.TabIndex = 1;
            this.radNgoaiTru.Text = "Ngoại trú";
            // 
            // radNoiTru
            // 
            this.radNoiTru.AutoSize = true;
            this.radNoiTru.Location = new System.Drawing.Point(266, 10);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(48, 17);
            this.radNoiTru.TabIndex = 2;
            this.radNoiTru.Text = "Nội trú";
            // 
            // cmdGetData
            // 
            this.cmdGetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetData.Image = ((System.Drawing.Image)(resources.GetObject("cmdGetData.Image")));
            this.cmdGetData.Location = new System.Drawing.Point(821, 23);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Size = new System.Drawing.Size(116, 34);
            this.cmdGetData.TabIndex = 3;
            this.cmdGetData.Text = "Lấy dữ liệu";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // grpOptionKetQua
            // 
            this.grpOptionKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptionKetQua.Controls.Add(this.radTatCaKQ);
            this.grpOptionKetQua.Controls.Add(this.radDaCoKetQua);
            this.grpOptionKetQua.Controls.Add(this.radChuaCoKetQua);
            this.grpOptionKetQua.Location = new System.Drawing.Point(468, 28);
            this.grpOptionKetQua.Name = "grpOptionKetQua";
            this.grpOptionKetQua.Size = new System.Drawing.Size(347, 32);
            this.grpOptionKetQua.TabIndex = 7;
            this.grpOptionKetQua.TabStop = false;
            // 
            // radTatCaKQ
            // 
            this.radTatCaKQ.AutoSize = true;
            this.radTatCaKQ.Location = new System.Drawing.Point(266, 10);
            this.radTatCaKQ.Name = "radTatCaKQ";
            this.radTatCaKQ.Size = new System.Drawing.Size(48, 17);
            this.radTatCaKQ.TabIndex = 0;
            this.radTatCaKQ.Text = "Tất cả";
            // 
            // radDaCoKetQua
            // 
            this.radDaCoKetQua.AutoSize = true;
            this.radDaCoKetQua.Checked = true;
            this.radDaCoKetQua.Location = new System.Drawing.Point(18, 10);
            this.radDaCoKetQua.Name = "radDaCoKetQua";
            this.radDaCoKetQua.Size = new System.Drawing.Size(85, 17);
            this.radDaCoKetQua.TabIndex = 1;
            this.radDaCoKetQua.TabStop = true;
            this.radDaCoKetQua.Text = "Đã có kết quả";
            // 
            // radChuaCoKetQua
            // 
            this.radChuaCoKetQua.AutoSize = true;
            this.radChuaCoKetQua.Location = new System.Drawing.Point(138, 10);
            this.radChuaCoKetQua.Name = "radChuaCoKetQua";
            this.radChuaCoKetQua.Size = new System.Drawing.Size(96, 17);
            this.radChuaCoKetQua.TabIndex = 2;
            this.radChuaCoKetQua.Text = "Chưa có kết quả";
            // 
            // grpKetQuaXN
            // 
            this.grpKetQuaXN.Controls.Add(this.grdKetQuaXN);
            this.grpKetQuaXN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetQuaXN.Location = new System.Drawing.Point(0, 0);
            this.grpKetQuaXN.Name = "grpKetQuaXN";
            this.grpKetQuaXN.Size = new System.Drawing.Size(560, 465);
            this.grpKetQuaXN.TabIndex = 4;
            this.grpKetQuaXN.Text = "Kết quả xét nghiệm";
            // 
            // grdKetQuaXN
            // 
            this.grdKetQuaXN.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdKetQuaXN.BackColor = System.Drawing.SystemColors.Control;
            this.grdKetQuaXN.ColumnAutoResize = true;
            grdKetQuaXN_DesignTimeLayout.LayoutString = resources.GetString("grdKetQuaXN_DesignTimeLayout.LayoutString");
            this.grdKetQuaXN.DesignTimeLayout = grdKetQuaXN_DesignTimeLayout;
            this.grdKetQuaXN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetQuaXN.FilterRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdKetQuaXN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.grdKetQuaXN.GroupByBoxVisible = false;
            this.grdKetQuaXN.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.grdKetQuaXN.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKetQuaXN.Location = new System.Drawing.Point(3, 16);
            this.grdKetQuaXN.Name = "grdKetQuaXN";
            this.grdKetQuaXN.RecordNavigator = true;
            this.grdKetQuaXN.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetQuaXN.Size = new System.Drawing.Size(554, 446);
            this.grdKetQuaXN.TabIndex = 22;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpKetQuaXN);
            this.splitContainer1.Panel2.Controls.Add(this.grpKetQuaCDHA);
            this.splitContainer1.Size = new System.Drawing.Size(949, 465);
            this.splitContainer1.SplitterDistance = 385;
            this.splitContainer1.TabIndex = 5;
            // 
            // grpKetQuaCDHA
            // 
            this.grpKetQuaCDHA.Controls.Add(this.grdKetQuaCDHA);
            this.grpKetQuaCDHA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetQuaCDHA.Location = new System.Drawing.Point(0, 0);
            this.grpKetQuaCDHA.Name = "grpKetQuaCDHA";
            this.grpKetQuaCDHA.Size = new System.Drawing.Size(560, 465);
            this.grpKetQuaCDHA.TabIndex = 6;
            this.grpKetQuaCDHA.TabStop = false;
            this.grpKetQuaCDHA.Text = "Kết quả CDHA";
            // 
            // grdKetQuaCDHA
            // 
            this.grdKetQuaCDHA.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdKetQuaCDHA.BackColor = System.Drawing.SystemColors.Control;
            grdKetQuaCDHA_DesignTimeLayout.LayoutString = resources.GetString("grdKetQuaCDHA_DesignTimeLayout.LayoutString");
            this.grdKetQuaCDHA.DesignTimeLayout = grdKetQuaCDHA_DesignTimeLayout;
            this.grdKetQuaCDHA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetQuaCDHA.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdKetQuaCDHA.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKetQuaCDHA.Font = new System.Drawing.Font("Arial", 10F);
            this.grdKetQuaCDHA.GroupByBoxVisible = false;
            this.grdKetQuaCDHA.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 10F);
            this.grdKetQuaCDHA.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKetQuaCDHA.Location = new System.Drawing.Point(3, 16);
            this.grdKetQuaCDHA.Name = "grdKetQuaCDHA";
            this.grdKetQuaCDHA.RecordNavigator = true;
            this.grdKetQuaCDHA.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetQuaCDHA.Size = new System.Drawing.Size(554, 446);
            this.grdKetQuaCDHA.TabIndex = 5;
            // 
            // frm_LichSuCLS_SingleExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frm_LichSuCLS_SingleExam";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử chỉ định";
            this.Load += new System.EventHandler(this.frm_LichSuCLS_SingleExam_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpOption.ResumeLayout(false);
            this.grpOptionKetQua.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpKetQuaXN)).EndInit();
            this.grpKetQuaXN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQuaXN)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpKetQuaCDHA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQuaCDHA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton btnCancel;
        private Janus.Windows.EditControls.UIButton btnAccept;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdGetData;
        private Janus.Windows.EditControls.UIRadioButton radNoiTru;
        private Janus.Windows.EditControls.UIRadioButton radNgoaiTru;
        private Janus.Windows.EditControls.UIRadioButton radTatCa;
        private Janus.Windows.EditControls.UIGroupBox grpKetQuaXN;
        private Janus.Windows.GridEX.GridEX grdAssignDetail;
        private Janus.Windows.GridEX.GridEX grdKetQuaXN;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpKetQuaCDHA;
        private Janus.Windows.GridEX.GridEX grdKetQuaCDHA;
        private System.Windows.Forms.GroupBox grpOption;
        internal System.Windows.Forms.TextBox txtPatientID;
        internal System.Windows.Forms.TextBox txtPatientCode;
        internal System.Windows.Forms.TextBox txtPatientName;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.TextBox txtYearBirth;
        internal System.Windows.Forms.TextBox txtSex;
        private System.Windows.Forms.GroupBox grpOptionKetQua;
        private Janus.Windows.EditControls.UIRadioButton radTatCaKQ;
        private Janus.Windows.EditControls.UIRadioButton radDaCoKetQua;
        private Janus.Windows.EditControls.UIRadioButton radChuaCoKetQua;
    }
}