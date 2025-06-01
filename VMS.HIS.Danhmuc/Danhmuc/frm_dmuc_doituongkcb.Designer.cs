using VNS.Libs.Utilities.UserControls;
namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_dmuc_doituongkcb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dmuc_doituongkcb));
            this.Label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoPayment = new System.Windows.Forms.CheckBox();
            this.chkThanhtoantruockhikham = new System.Windows.Forms.CheckBox();
            this.cboLoaidoituong = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObjectCode = new System.Windows.Forms.TextBox();
            this.txtDiscountDiscorrectLine = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.txtDiscountCorrectLine = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
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
            this.cboKieulaygiathuoc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(21, 58);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(136, 21);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "% BHYT đúng tuyến:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdList);
            this.tabPage1.Controls.Add(this.GroupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1170, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Danh mục loại đối tượng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin nhóm đối tượng bảo hiểm </FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.grdList.Size = new System.Drawing.Size(1164, 390);
            this.grdList.TabIndex = 20;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox1.Controls.Add(this.cboKieulaygiathuoc);
            this.GroupBox1.Controls.Add(this.label8);
            this.GroupBox1.Controls.Add(this.chkAutoPayment);
            this.GroupBox1.Controls.Add(this.chkThanhtoantruockhikham);
            this.GroupBox1.Controls.Add(this.cboLoaidoituong);
            this.GroupBox1.Controls.Add(this.label4);
            this.GroupBox1.Controls.Add(this.txtObjectCode);
            this.GroupBox1.Controls.Add(this.txtDiscountDiscorrectLine);
            this.GroupBox1.Controls.Add(this.txtDiscountCorrectLine);
            this.GroupBox1.Controls.Add(this.label9);
            this.GroupBox1.Controls.Add(this.label10);
            this.GroupBox1.Controls.Add(this.txtPos);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.txtDesc);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.txtName);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtID);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.GroupBox1.Location = new System.Drawing.Point(3, 393);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1164, 215);
            this.GroupBox1.TabIndex = 18;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Thông tin chi tiết";
            this.GroupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // chkAutoPayment
            // 
            this.chkAutoPayment.AutoSize = true;
            this.chkAutoPayment.Location = new System.Drawing.Point(375, 111);
            this.chkAutoPayment.Name = "chkAutoPayment";
            this.chkAutoPayment.Size = new System.Drawing.Size(199, 20);
            this.chkAutoPayment.TabIndex = 28;
            this.chkAutoPayment.TabStop = false;
            this.chkAutoPayment.Text = "Tự động thanh toán phí KCB?";
            this.chkAutoPayment.UseVisualStyleBackColor = true;
            // 
            // chkThanhtoantruockhikham
            // 
            this.chkThanhtoantruockhikham.AutoSize = true;
            this.chkThanhtoantruockhikham.Location = new System.Drawing.Point(163, 192);
            this.chkThanhtoantruockhikham.Name = "chkThanhtoantruockhikham";
            this.chkThanhtoantruockhikham.Size = new System.Drawing.Size(375, 20);
            this.chkThanhtoantruockhikham.TabIndex = 27;
            this.chkThanhtoantruockhikham.TabStop = false;
            this.chkThanhtoantruockhikham.Text = "Cần thanh toán tiền khám trước khi được vào phòng khám?";
            this.chkThanhtoantruockhikham.UseVisualStyleBackColor = true;
            // 
            // cboLoaidoituong
            // 
            this.cboLoaidoituong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaidoituong.FormattingEnabled = true;
            this.cboLoaidoituong.Items.AddRange(new object[] {
            "Bảo hiểm y tế",
            "Dịch vụ"});
            this.cboLoaidoituong.Location = new System.Drawing.Point(162, 136);
            this.cboLoaidoituong.Name = "cboLoaidoituong";
            this.cboLoaidoituong.Size = new System.Drawing.Size(198, 24);
            this.cboLoaidoituong.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(21, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "Loại đối tượng:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObjectCode
            // 
            this.txtObjectCode.BackColor = System.Drawing.Color.White;
            this.txtObjectCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObjectCode.Location = new System.Drawing.Point(232, 30);
            this.txtObjectCode.MaxLength = 20;
            this.txtObjectCode.Name = "txtObjectCode";
            this.txtObjectCode.Size = new System.Drawing.Size(128, 22);
            this.txtObjectCode.TabIndex = 0;
            // 
            // txtDiscountDiscorrectLine
            // 
            this.txtDiscountDiscorrectLine.Location = new System.Drawing.Point(163, 83);
            this.txtDiscountDiscorrectLine.Name = "txtDiscountDiscorrectLine";
            this.txtDiscountDiscorrectLine.Size = new System.Drawing.Size(197, 22);
            this.txtDiscountDiscorrectLine.TabIndex = 3;
            // 
            // txtDiscountCorrectLine
            // 
            this.txtDiscountCorrectLine.Location = new System.Drawing.Point(163, 55);
            this.txtDiscountCorrectLine.Name = "txtDiscountCorrectLine";
            this.txtDiscountCorrectLine.Size = new System.Drawing.Size(197, 22);
            this.txtDiscountCorrectLine.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(366, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(208, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "(Áp dụng cho đối tượng trái tuyến)";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(21, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 21);
            this.label10.TabIndex = 20;
            this.label10.Text = "% BHYT trái tuyến:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPos
            // 
            this.txtPos.BackColor = System.Drawing.Color.White;
            this.txtPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPos.Location = new System.Drawing.Point(447, 30);
            this.txtPos.MaxLength = 5;
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(112, 22);
            this.txtPos.TabIndex = 1;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(366, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "(Áp dụng cho đối tượng đúng tuyến)";
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Location = new System.Drawing.Point(162, 165);
            this.txtDesc.MaxLength = 255;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(680, 22);
            this.txtDesc.TabIndex = 6;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(21, 167);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(136, 21);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Mô tả thêm:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(162, 109);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(198, 22);
            this.txtName.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(21, 110);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(136, 21);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Tên đối tượng:";
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
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(21, 31);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 21);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Mã đối tượng:";
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
            this.tabControl1.Size = new System.Drawing.Size(1178, 640);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
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
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Add;
            this.cmdInsert.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInsert.Location = new System.Drawing.Point(427, 702);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(120, 32);
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Text = "Thêm";
            this.cmdInsert.ToolTipText = "Lưu lại thông tin ";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(805, 702);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 32);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(931, 702);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 32);
            this.cmdPrint.TabIndex = 11;
            this.cmdPrint.Text = "In";
            this.cmdPrint.ToolTipText = "Lưu lại thông tin ";
            this.cmdPrint.Visible = false;
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDelete.Location = new System.Drawing.Point(679, 702);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(120, 32);
            this.cmdDelete.TabIndex = 9;
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.ToolTipText = "Xóa";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdUpdate.Location = new System.Drawing.Point(553, 702);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(120, 32);
            this.cmdUpdate.TabIndex = 8;
            this.cmdUpdate.Text = "Sửa";
            this.cmdUpdate.ToolTipText = "Sửa";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(1057, 702);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 32);
            this.cmdClose.TabIndex = 12;
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
            this.sysColor.Size = new System.Drawing.Size(1184, 50);
            this.sysColor.TabIndex = 37;
            this.sysColor.Title = "DANH MỤC ĐỐI TƯỢNG THAM GIA KHÁM CHỮA BỆNH";
            this.sysColor.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cboKieulaygiathuoc
            // 
            this.cboKieulaygiathuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKieulaygiathuoc.FormattingEnabled = true;
            this.cboKieulaygiathuoc.Items.AddRange(new object[] {
            "Giá thuốc kho",
            "Giá thuốc theo quan hệ đối tượng KCB",
            "Giá thuốc theo Danh mục thuốc"});
            this.cboKieulaygiathuoc.Location = new System.Drawing.Point(520, 135);
            this.cboKieulaygiathuoc.Name = "cboKieulaygiathuoc";
            this.cboKieulaygiathuoc.Size = new System.Drawing.Size(322, 24);
            this.cboKieulaygiathuoc.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(379, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 21);
            this.label8.TabIndex = 30;
            this.label8.Text = "Lấy giá thuốc theo:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_dmuc_doituongkcb
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1184, 761);
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
            this.Name = "frm_dmuc_doituongkcb";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANH MỤC ĐỐI TƯỢNG THAM GIA KHÁM CHỮA BỆNH";
            this.Load += new System.EventHandler(this.frm_dmuc_doituongkcb_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_dmuc_doituongkcb_KeyDown);
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
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtDesc;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private System.Windows.Forms.TabControl tabControl1;
        internal System.Windows.Forms.Label label6;
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
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txtDiscountDiscorrectLine;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txtDiscountCorrectLine;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdInsert;
        internal System.Windows.Forms.TextBox txtObjectCode;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLoaidoituong;
        private System.Windows.Forms.CheckBox chkThanhtoantruockhikham;
        private System.Windows.Forms.CheckBox chkAutoPayment;
        private System.Windows.Forms.ComboBox cboKieulaygiathuoc;
        internal System.Windows.Forms.Label label8;
    }
}