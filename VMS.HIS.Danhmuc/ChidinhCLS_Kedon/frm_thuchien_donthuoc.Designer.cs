namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_thuchien_donthuoc
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.GridEX.GridEXLayout grdPresDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thuchien_donthuoc));
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chkTutuc = new Janus.Windows.EditControls.UICheckBox();
            this.txtMaBenhChinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTenBenhChinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtChanDoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPres_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSex = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtYearBirth = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdSavePres = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPresDetail = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_chidan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_dvt = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_soluong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tenthuoc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_noidungthuchien = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeTo = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.Timefrom = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCa = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txt_nguoithuchien = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtchandoan_new = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 738);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Ctrl+S: Lưu lại";
            uiStatusBarPanel1.Width = 93;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Esc: Thoát";
            uiStatusBarPanel2.Width = 73;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2});
            this.uiStatusBar1.Size = new System.Drawing.Size(1202, 23);
            this.uiStatusBar1.TabIndex = 1;
            this.uiStatusBar1.TabStop = false;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.panel1);
            this.uiGroupBox1.Controls.Add(this.cmdSavePres);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 689);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1202, 49);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Hành động";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkTutuc);
            this.panel1.Controls.Add(this.txtchandoan_new);
            this.panel1.Controls.Add(this.txtMaBenhChinh);
            this.panel1.Controls.Add(this.txtTenBenhChinh);
            this.panel1.Controls.Add(this.txtChanDoan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPres_ID);
            this.panel1.Controls.Add(this.txtPatientName);
            this.panel1.Controls.Add(this.txtPatientCode);
            this.panel1.Controls.Add(this.txtSex);
            this.panel1.Controls.Add(this.txtPatientID);
            this.panel1.Controls.Add(this.txtYearBirth);
            this.panel1.Location = new System.Drawing.Point(592, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(31, 20);
            this.panel1.TabIndex = 601;
            this.panel1.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 581;
            this.label4.Text = "Họ và tên:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // chkTutuc
            // 
            this.chkTutuc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTutuc.ForeColor = System.Drawing.Color.Navy;
            this.chkTutuc.Location = new System.Drawing.Point(125, 18);
            this.chkTutuc.Name = "chkTutuc";
            this.chkTutuc.Size = new System.Drawing.Size(105, 23);
            this.chkTutuc.TabIndex = 598;
            this.chkTutuc.Text = "Thuốc tự túc";
            this.chkTutuc.Visible = false;
            this.chkTutuc.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // txtMaBenhChinh
            // 
            this.txtMaBenhChinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBenhChinh.Location = new System.Drawing.Point(125, 46);
            this.txtMaBenhChinh.Name = "txtMaBenhChinh";
            this.txtMaBenhChinh.ReadOnly = true;
            this.txtMaBenhChinh.Size = new System.Drawing.Size(10, 21);
            this.txtMaBenhChinh.TabIndex = 587;
            this.txtMaBenhChinh.TabStop = false;
            this.txtMaBenhChinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaBenhChinh.Visible = false;
            this.txtMaBenhChinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtTenBenhChinh
            // 
            this.txtTenBenhChinh.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenBenhChinh.Location = new System.Drawing.Point(125, 18);
            this.txtTenBenhChinh.Name = "txtTenBenhChinh";
            this.txtTenBenhChinh.Size = new System.Drawing.Size(10, 21);
            this.txtTenBenhChinh.TabIndex = 599;
            this.txtTenBenhChinh.TabStop = false;
            this.txtTenBenhChinh.Visible = false;
            this.txtTenBenhChinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.BackColor = System.Drawing.Color.White;
            this.txtChanDoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoan.ForeColor = System.Drawing.Color.Black;
            this.txtChanDoan.Location = new System.Drawing.Point(80, 44);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(10, 21);
            this.txtChanDoan.TabIndex = 585;
            this.txtChanDoan.Visible = false;
            this.txtChanDoan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 586;
            this.label1.Text = "Chẩn đoán:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // txtPres_ID
            // 
            this.txtPres_ID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPres_ID.Location = new System.Drawing.Point(151, 18);
            this.txtPres_ID.Name = "txtPres_ID";
            this.txtPres_ID.ReadOnly = true;
            this.txtPres_ID.Size = new System.Drawing.Size(10, 21);
            this.txtPres_ID.TabIndex = 584;
            this.txtPres_ID.Visible = false;
            this.txtPres_ID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(80, 15);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(10, 23);
            this.txtPatientName.TabIndex = 569;
            this.txtPatientName.TabStop = false;
            this.txtPatientName.Text = "Nguyễn thị phương thanh";
            this.txtPatientName.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientName.Visible = false;
            this.txtPatientName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.Honeydew;
            this.txtPatientCode.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(151, 17);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.ReadOnly = true;
            this.txtPatientCode.Size = new System.Drawing.Size(10, 23);
            this.txtPatientCode.TabIndex = 583;
            this.txtPatientCode.TabStop = false;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientCode.Visible = false;
            this.txtPatientCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtSex
            // 
            this.txtSex.ButtonFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.Location = new System.Drawing.Point(96, 17);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(10, 23);
            this.txtSex.TabIndex = 571;
            this.txtSex.TabStop = false;
            this.txtSex.Text = "Nam";
            this.txtSex.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSex.Visible = false;
            this.txtSex.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientID
            // 
            this.txtPatientID.BackColor = System.Drawing.Color.Honeydew;
            this.txtPatientID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientID.Location = new System.Drawing.Point(128, 18);
            this.txtPatientID.Name = "txtPatientID";
            this.txtPatientID.ReadOnly = true;
            this.txtPatientID.Size = new System.Drawing.Size(10, 21);
            this.txtPatientID.TabIndex = 582;
            this.txtPatientID.TabStop = false;
            this.txtPatientID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientID.Visible = false;
            this.txtPatientID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtYearBirth
            // 
            this.txtYearBirth.ButtonFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearBirth.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearBirth.Location = new System.Drawing.Point(112, 17);
            this.txtYearBirth.Name = "txtYearBirth";
            this.txtYearBirth.ReadOnly = true;
            this.txtYearBirth.Size = new System.Drawing.Size(10, 23);
            this.txtYearBirth.TabIndex = 572;
            this.txtYearBirth.TabStop = false;
            this.txtYearBirth.Text = "1990";
            this.txtYearBirth.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtYearBirth.Visible = false;
            this.txtYearBirth.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdSavePres
            // 
            this.cmdSavePres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSavePres.Image = global::VMS.HIS.Danhmuc.Properties.Resources.printer_24;
            this.cmdSavePres.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSavePres.Location = new System.Drawing.Point(932, 12);
            this.cmdSavePres.Name = "cmdSavePres";
            this.cmdSavePres.Size = new System.Drawing.Size(144, 32);
            this.cmdSavePres.TabIndex = 10;
            this.cmdSavePres.Text = "In phiếu thực hiện";
            this.cmdSavePres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSavePres.Click += new System.EventHandler(this.cmdSavePres_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1086, 12);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(104, 32);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdPresDetail);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1202, 689);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Chi tiết thuốc trong đơn, nhập trực tiếp thông tin thực hiện thuốc vào lưới";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdPresDetail
            // 
            grdPresDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPresDetail_DesignTimeLayout.LayoutString");
            this.grdPresDetail.DesignTimeLayout = grdPresDetail_DesignTimeLayout;
            this.grdPresDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPresDetail.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.NextCell;
            this.grdPresDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPresDetail.GroupByBoxVisible = false;
            this.grdPresDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdPresDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPresDetail.Location = new System.Drawing.Point(3, 17);
            this.grdPresDetail.Name = "grdPresDetail";
            this.grdPresDetail.RowFormatStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPresDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdPresDetail.Size = new System.Drawing.Size(1196, 669);
            this.grdPresDetail.TabIndex = 551;
            this.grdPresDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdPresDetail.TotalRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPresDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPresDetail.UseGroupRowSelector = true;
            this.grdPresDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCa);
            this.panel2.Controls.Add(this.txt_nguoithuchien);
            this.panel2.Controls.Add(this.txt_chidan);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txt_dvt);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txt_soluong);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txt_tenthuoc);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txt_noidungthuchien);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.timeTo);
            this.panel2.Controls.Add(this.Timefrom);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1202, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 689);
            this.panel2.TabIndex = 552;
            // 
            // txt_chidan
            // 
            this.txt_chidan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chidan.Location = new System.Drawing.Point(128, 74);
            this.txt_chidan.Multiline = true;
            this.txt_chidan.Name = "txt_chidan";
            this.txt_chidan.ReadOnly = true;
            this.txt_chidan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_chidan.Size = new System.Drawing.Size(224, 67);
            this.txt_chidan.TabIndex = 3;
            this.txt_chidan.TabStop = false;
            this.txt_chidan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 67);
            this.label11.TabIndex = 591;
            this.label11.Text = "Chỉ dẫn";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_dvt
            // 
            this.txt_dvt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dvt.Location = new System.Drawing.Point(128, 50);
            this.txt_dvt.Name = "txt_dvt";
            this.txt_dvt.ReadOnly = true;
            this.txt_dvt.Size = new System.Drawing.Size(78, 21);
            this.txt_dvt.TabIndex = 1;
            this.txt_dvt.TabStop = false;
            this.txt_dvt.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 17);
            this.label10.TabIndex = 590;
            this.label10.Text = "Đơn vị tính";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_soluong
            // 
            this.txt_soluong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_soluong.Location = new System.Drawing.Point(280, 50);
            this.txt_soluong.Name = "txt_soluong";
            this.txt_soluong.ReadOnly = true;
            this.txt_soluong.Size = new System.Drawing.Size(72, 21);
            this.txt_soluong.TabIndex = 2;
            this.txt_soluong.TabStop = false;
            this.txt_soluong.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(212, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 588;
            this.label6.Text = "Số lượng";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_tenthuoc
            // 
            this.txt_tenthuoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tenthuoc.Location = new System.Drawing.Point(128, 27);
            this.txt_tenthuoc.Name = "txt_tenthuoc";
            this.txt_tenthuoc.ReadOnly = true;
            this.txt_tenthuoc.Size = new System.Drawing.Size(224, 21);
            this.txt_tenthuoc.TabIndex = 0;
            this.txt_tenthuoc.TabStop = false;
            this.txt_tenthuoc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 586;
            this.label5.Text = "Tên thuốc";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdSave.Location = new System.Drawing.Point(-124, 258);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(104, 29);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Text = "Lưu";
            this.cmdSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 583;
            this.label2.Text = "Người thực hiện";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_noidungthuchien
            // 
            this.txt_noidungthuchien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_noidungthuchien.Location = new System.Drawing.Point(128, 204);
            this.txt_noidungthuchien.Name = "txt_noidungthuchien";
            this.txt_noidungthuchien.Size = new System.Drawing.Size(224, 21);
            this.txt_noidungthuchien.TabIndex = 7;
            this.txt_noidungthuchien.TabStop = false;
            this.txt_noidungthuchien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 581;
            this.label3.Text = "Nội dung thực hiện";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 16);
            this.label9.TabIndex = 255;
            this.label9.Text = "Thời điểm";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(3, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 23);
            this.label7.TabIndex = 254;
            this.label7.Text = "Thời gian từ:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeTo
            // 
            this.timeTo.CustomFormat = "HH:mm";
            this.timeTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.timeTo.DropDownCalendar.Name = "";
            this.timeTo.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.timeTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeTo.Location = new System.Drawing.Point(268, 174);
            this.timeTo.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.timeTo.Name = "timeTo";
            this.timeTo.ShowUpDown = true;
            this.timeTo.Size = new System.Drawing.Size(84, 22);
            this.timeTo.TabIndex = 6;
            this.timeTo.TabStop = false;
            this.timeTo.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // Timefrom
            // 
            this.Timefrom.CustomFormat = "HH:mm";
            this.Timefrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.Timefrom.DropDownCalendar.Name = "";
            this.Timefrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.Timefrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Timefrom.Location = new System.Drawing.Point(128, 176);
            this.Timefrom.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.Timefrom.Name = "Timefrom";
            this.Timefrom.ShowUpDown = true;
            this.Timefrom.Size = new System.Drawing.Size(92, 22);
            this.Timefrom.TabIndex = 5;
            this.Timefrom.TabStop = false;
            this.Timefrom.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(226, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 19);
            this.label8.TabIndex = 253;
            this.label8.Text = "đến";
            // 
            // txtCa
            // 
            this.txtCa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtCa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCa.AddValues = true;
            this.txtCa.AllowMultiline = false;
            this.txtCa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtCa.AutoCompleteList")));
            this.txtCa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCa.buildShortcut = false;
            this.txtCa.CaseSensitive = false;
            this.txtCa.cmdDropDown = null;
            this.txtCa.CompareNoID = true;
            this.txtCa.DefaultCode = "-1";
            this.txtCa.DefaultID = "-1";
            this.txtCa.Drug_ID = null;
            this.txtCa.ExtraWidth = 0;
            this.txtCa.FillValueAfterSelect = false;
            this.txtCa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCa.LOAI_DANHMUC = "CAKCB";
            this.txtCa.Location = new System.Drawing.Point(128, 148);
            this.txtCa.MaxHeight = 279;
            this.txtCa.MinTypedCharacters = 2;
            this.txtCa.MyCode = "-1";
            this.txtCa.MyID = "-1";
            this.txtCa.Name = "txtCa";
            this.txtCa.RaiseEvent = true;
            this.txtCa.RaiseEventEnter = true;
            this.txtCa.RaiseEventEnterWhenEmpty = false;
            this.txtCa.SelectedIndex = -1;
            this.txtCa.SetDefaultWhenInit = true;
            this.txtCa.ShowCodeWithValue = false;
            this.txtCa.Size = new System.Drawing.Size(224, 21);
            this.txtCa.splitChar = '@';
            this.txtCa.splitCharIDAndCode = '#';
            this.txtCa.TabIndex = 4;
            this.txtCa.TakeCode = false;
            this.txtCa.txtMyCode = null;
            this.txtCa.txtMyCode_Edit = null;
            this.txtCa.txtMyID = null;
            this.txtCa.txtMyID_Edit = null;
            this.txtCa.txtMyName = null;
            this.txtCa.txtMyName_Edit = null;
            this.txtCa.txtNext = null;
            this.txtCa.txtNext1 = null;
            // 
            // txt_nguoithuchien
            // 
            this.txt_nguoithuchien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_nguoithuchien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nguoithuchien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_nguoithuchien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_nguoithuchien.AutoCompleteList")));
            this.txt_nguoithuchien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nguoithuchien.buildShortcut = false;
            this.txt_nguoithuchien.CaseSensitive = false;
            this.txt_nguoithuchien.CompareNoID = true;
            this.txt_nguoithuchien.DefaultCode = "-1";
            this.txt_nguoithuchien.DefaultID = "-1";
            this.txt_nguoithuchien.DisplayType = 0;
            this.txt_nguoithuchien.Drug_ID = null;
            this.txt_nguoithuchien.ExtraWidth = -100;
            this.txt_nguoithuchien.FillValueAfterSelect = false;
            this.txt_nguoithuchien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nguoithuchien.Location = new System.Drawing.Point(128, 229);
            this.txt_nguoithuchien.MaxHeight = 289;
            this.txt_nguoithuchien.MinTypedCharacters = 2;
            this.txt_nguoithuchien.MyCode = "-1";
            this.txt_nguoithuchien.MyID = "-1";
            this.txt_nguoithuchien.MyText = "";
            this.txt_nguoithuchien.MyTextOnly = "";
            this.txt_nguoithuchien.Name = "txt_nguoithuchien";
            this.txt_nguoithuchien.RaiseEvent = true;
            this.txt_nguoithuchien.RaiseEventEnter = true;
            this.txt_nguoithuchien.RaiseEventEnterWhenEmpty = true;
            this.txt_nguoithuchien.SelectedIndex = -1;
            this.txt_nguoithuchien.Size = new System.Drawing.Size(224, 21);
            this.txt_nguoithuchien.splitChar = '@';
            this.txt_nguoithuchien.splitCharIDAndCode = '#';
            this.txt_nguoithuchien.TabIndex = 8;
            this.txt_nguoithuchien.TakeCode = false;
            this.txt_nguoithuchien.txtMyCode = null;
            this.txt_nguoithuchien.txtMyCode_Edit = null;
            this.txt_nguoithuchien.txtMyID = null;
            this.txt_nguoithuchien.txtMyID_Edit = null;
            this.txt_nguoithuchien.txtMyName = null;
            this.txt_nguoithuchien.txtMyName_Edit = null;
            this.txt_nguoithuchien.txtNext = null;
            // 
            // txtchandoan_new
            // 
            this.txtchandoan_new._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtchandoan_new._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchandoan_new._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtchandoan_new.AddValues = true;
            this.txtchandoan_new.AllowMultiline = false;
            this.txtchandoan_new.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtchandoan_new.AutoCompleteList")));
            this.txtchandoan_new.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchandoan_new.buildShortcut = false;
            this.txtchandoan_new.CaseSensitive = false;
            this.txtchandoan_new.cmdDropDown = null;
            this.txtchandoan_new.CompareNoID = true;
            this.txtchandoan_new.DefaultCode = "-1";
            this.txtchandoan_new.DefaultID = "-1";
            this.txtchandoan_new.Drug_ID = null;
            this.txtchandoan_new.ExtraWidth = 0;
            this.txtchandoan_new.FillValueAfterSelect = false;
            this.txtchandoan_new.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchandoan_new.LOAI_DANHMUC = "CHANDOAN";
            this.txtchandoan_new.Location = new System.Drawing.Point(125, 19);
            this.txtchandoan_new.MaxHeight = -1;
            this.txtchandoan_new.MinTypedCharacters = 2;
            this.txtchandoan_new.MyCode = "-1";
            this.txtchandoan_new.MyID = "-1";
            this.txtchandoan_new.Name = "txtchandoan_new";
            this.txtchandoan_new.RaiseEvent = false;
            this.txtchandoan_new.RaiseEventEnter = false;
            this.txtchandoan_new.RaiseEventEnterWhenEmpty = false;
            this.txtchandoan_new.SelectedIndex = -1;
            this.txtchandoan_new.SetDefaultWhenInit = true;
            this.txtchandoan_new.ShowCodeWithValue = false;
            this.txtchandoan_new.Size = new System.Drawing.Size(10, 21);
            this.txtchandoan_new.splitChar = '@';
            this.txtchandoan_new.splitCharIDAndCode = '#';
            this.txtchandoan_new.TabIndex = 600;
            this.txtchandoan_new.TakeCode = false;
            this.txtchandoan_new.txtMyCode = null;
            this.txtchandoan_new.txtMyCode_Edit = null;
            this.txtchandoan_new.txtMyID = null;
            this.txtchandoan_new.txtMyID_Edit = null;
            this.txtchandoan_new.txtMyName = null;
            this.txtchandoan_new.txtMyName_Edit = null;
            this.txtchandoan_new.txtNext = null;
            this.txtchandoan_new.txtNext1 = null;
            this.txtchandoan_new.Visible = false;
            // 
            // frm_thuchien_donthuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 761);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_thuchien_donthuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thực hiện đơn thuốc";
            this.Load += new System.EventHandler(this.frm_thuchien_donthuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        internal Janus.Windows.EditControls.UIButton cmdSavePres;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdPresDetail;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        public Janus.Windows.GridEX.EditControls.EditBox txtYearBirth;
        public Janus.Windows.GridEX.EditControls.EditBox txtSex;
        internal System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtChanDoan;
        internal System.Windows.Forms.Label label1;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientCode;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientID;
        public Janus.Windows.GridEX.EditControls.EditBox txtPres_ID;
        public Janus.Windows.GridEX.EditControls.EditBox txtMaBenhChinh;
        private Janus.Windows.EditControls.UICheckBox chkTutuc;
        public Janus.Windows.GridEX.EditControls.EditBox txtTenBenhChinh;
        public UCs.AutoCompleteTextbox_Danhmucchung txtchandoan_new;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label7;
        private Janus.Windows.CalendarCombo.CalendarCombo timeTo;
        private Janus.Windows.CalendarCombo.CalendarCombo Timefrom;
        internal System.Windows.Forms.Label label8;
        private Janus.Windows.EditControls.UIButton cmdSave;
        internal System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txt_noidungthuchien;
        internal System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txt_chidan;
        internal System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.EditBox txt_dvt;
        internal System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.EditBox txt_soluong;
        internal System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txt_tenthuoc;
        internal System.Windows.Forms.Label label5;
        private UCs.AutoCompleteTextbox txt_nguoithuchien;
        private UCs.AutoCompleteTextbox_Danhmucchung txtCa;
    }
}