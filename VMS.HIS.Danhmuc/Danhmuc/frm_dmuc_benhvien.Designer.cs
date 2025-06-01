using VNS.Libs.Utilities.UserControls;
namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_dmuc_benhvien
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dmuc_benhvien));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.chkhoatdong = new Janus.Windows.EditControls.UICheckBox();
            this.cboTuyen = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtThanhpho = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObjectCode = new System.Windows.Forms.TextBox();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdInsert = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.sysColor = new VNS.Libs.Utilities.UserControls.Banner();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdList);
            this.tabPage1.Controls.Add(this.GroupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(950, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Danh mục bệnh viện";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin nhóm đối tượng bảo hiểm </FilterRowInfoText></LocalizableData>";
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 3);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.grdList.Size = new System.Drawing.Size(944, 347);
            this.grdList.TabIndex = 20;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox1.Controls.Add(this.chkhoatdong);
            this.GroupBox1.Controls.Add(this.cboTuyen);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.txtThanhpho);
            this.GroupBox1.Controls.Add(this.label4);
            this.GroupBox1.Controls.Add(this.txtObjectCode);
            this.GroupBox1.Controls.Add(this.txtPos);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Controls.Add(this.txtName);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtID);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.GroupBox1.Location = new System.Drawing.Point(3, 350);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(944, 118);
            this.GroupBox1.TabIndex = 18;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Thông tin chi tiết";
            // 
            // chkhoatdong
            // 
            this.chkhoatdong.Location = new System.Drawing.Point(622, 84);
            this.chkhoatdong.Name = "chkhoatdong";
            this.chkhoatdong.Size = new System.Drawing.Size(154, 19);
            this.chkhoatdong.TabIndex = 29;
            this.chkhoatdong.Text = "Hoạt động";
            this.chkhoatdong.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cboTuyen
            // 
            this.cboTuyen.FormattingEnabled = true;
            this.cboTuyen.Items.AddRange(new object[] {
            "Hạng 1",
            "Hạng 2",
            "Hạng 3",
            "Hạng 4"});
            this.cboTuyen.Location = new System.Drawing.Point(622, 29);
            this.cboTuyen.Name = "cboTuyen";
            this.cboTuyen.Size = new System.Drawing.Size(154, 24);
            this.cboTuyen.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(569, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Hạng:";
            // 
            // txtThanhpho
            // 
            this.txtThanhpho._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtThanhpho._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhpho._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtThanhpho.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtThanhpho.AutoCompleteList")));
            this.txtThanhpho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThanhpho.CaseSensitive = false;
            this.txtThanhpho.CompareNoID = true;
            this.txtThanhpho.DefaultCode = "-1";
            this.txtThanhpho.DefaultID = "-1";
            this.txtThanhpho.Drug_ID = null;
            this.txtThanhpho.ExtraWidth = 0;
            this.txtThanhpho.FillValueAfterSelect = false;
            this.txtThanhpho.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhpho.Location = new System.Drawing.Point(163, 86);
            this.txtThanhpho.MaxHeight = 289;
            this.txtThanhpho.MinTypedCharacters = 2;
            this.txtThanhpho.MyCode = "-1";
            this.txtThanhpho.MyID = "-1";
            this.txtThanhpho.MyText = "";
            this.txtThanhpho.Name = "txtThanhpho";
            this.txtThanhpho.RaiseEvent = true;
            this.txtThanhpho.RaiseEventEnter = true;
            this.txtThanhpho.RaiseEventEnterWhenEmpty = true;
            this.txtThanhpho.SelectedIndex = -1;
            this.txtThanhpho.Size = new System.Drawing.Size(396, 21);
            this.txtThanhpho.splitChar = '@';
            this.txtThanhpho.splitCharIDAndCode = '#';
            this.txtThanhpho.TabIndex = 4;
            this.txtThanhpho.TakeCode = false;
            this.txtThanhpho.txtMyCode = null;
            this.txtThanhpho.txtMyCode_Edit = null;
            this.txtThanhpho.txtMyID = null;
            this.txtThanhpho.txtMyID_Edit = null;
            this.txtThanhpho.txtMyName = null;
            this.txtThanhpho.txtMyName_Edit = null;
            this.txtThanhpho.txtNext = null;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "Thành phố";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObjectCode
            // 
            this.txtObjectCode.BackColor = System.Drawing.Color.White;
            this.txtObjectCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObjectCode.Location = new System.Drawing.Point(232, 30);
            this.txtObjectCode.MaxLength = 10;
            this.txtObjectCode.Name = "txtObjectCode";
            this.txtObjectCode.Size = new System.Drawing.Size(128, 22);
            this.txtObjectCode.TabIndex = 1;
            // 
            // txtPos
            // 
            this.txtPos.BackColor = System.Drawing.Color.White;
            this.txtPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPos.Location = new System.Drawing.Point(447, 30);
            this.txtPos.MaxLength = 5;
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(112, 22);
            this.txtPos.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(372, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Số thứ tự:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(163, 58);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(613, 22);
            this.txtName.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(21, 59);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(136, 21);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Tên bệnh viện";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Location = new System.Drawing.Point(163, 30);
            this.txtID.MaxLength = 3;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(63, 22);
            this.txtID.TabIndex = 0;
            this.txtID.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(21, 34);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 21);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Mã bệnh viện";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.tabControl1.Location = new System.Drawing.Point(3, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(958, 500);
            this.tabControl1.TabIndex = 35;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel11});
            this.statusStrip1.Location = new System.Drawing.Point(0, 599);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(964, 22);
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(144, 17);
            this.toolStripStatusLabel1.Text = "Ctrl+N: Thêm mới        ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(156, 17);
            this.toolStripStatusLabel3.Text = "Ctrl+C: Cập nhập            ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabel5.Text = "Del: Xóa           ";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(124, 17);
            this.toolStripStatusLabel7.Text = "Ctrl+S: Lưu            ";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(133, 17);
            this.toolStripStatusLabel9.Text = "Ctrl+P: In ấn             ";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel11.Text = "Escape: Thoát, hủy";
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
            this.DataGridViewTextBoxColumn2.Visible = false;
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
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Mota_Them";
            this.dataGridViewTextBoxColumn5.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Mota_Them";
            this.dataGridViewTextBoxColumn6.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Insurance_Level";
            this.dataGridViewTextBoxColumn7.HeaderText = "Mức bảo hiểm";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.Image = ((System.Drawing.Image)(resources.GetObject("cmdInsert.Image")));
            this.cmdInsert.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInsert.Location = new System.Drawing.Point(7, 562);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(91, 28);
            this.cmdInsert.TabIndex = 6;
            this.cmdInsert.Text = "Thêm";
            this.cmdInsert.ToolTipText = "Lưu lại thông tin ";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(298, 562);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(91, 28);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(769, 562);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(91, 28);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "In";
            this.cmdPrint.ToolTipText = "Lưu lại thông tin ";
            this.cmdPrint.Visible = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdDelete.Location = new System.Drawing.Point(201, 562);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(91, 28);
            this.cmdDelete.TabIndex = 8;
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.ToolTipText = "Xóa";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdUpdate.Location = new System.Drawing.Point(104, 562);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(91, 28);
            this.cmdUpdate.TabIndex = 7;
            this.cmdUpdate.Text = "Sửa";
            this.cmdUpdate.ToolTipText = "Sửa";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdClose.Location = new System.Drawing.Point(866, 562);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(91, 28);
            this.cmdClose.TabIndex = 10;
            this.cmdClose.Text = "Thoát";
            this.cmdClose.ToolTipText = "Lưu lại thông tin ";
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sysColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.PicImg = ((System.Drawing.Image)(resources.GetObject("sysColor.PicImg")));
            this.sysColor.PicImgSizemode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sysColor.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.sysColor.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sysColor.ShortcutGuide = "";
            this.sysColor.Size = new System.Drawing.Size(964, 50);
            this.sysColor.TabIndex = 37;
            this.sysColor.Title = "DANH MỤC BỆNH VIỆN";
            this.sysColor.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frm_dmuc_benhvien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(964, 621);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.sysColor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_dmuc_benhvien";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục Bệnh viện";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_dmuc_benhvien_KeyDown);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectType_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectType_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsHospital_fee;
        internal System.Windows.Forms.TextBox txtPos;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private Banner sysColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdInsert;
        internal System.Windows.Forms.TextBox txtObjectCode;
        internal System.Windows.Forms.Label label4;
        private UCs.AutoCompleteTextbox txtThanhpho;
        private System.Windows.Forms.ComboBox cboTuyen;
        internal System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UICheckBox chkhoatdong;
    }
}