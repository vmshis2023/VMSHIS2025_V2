using VNS.HIS.UCs;
namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu
{
    partial class frm_Add_DieuTiet_Thau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Add_DieuTiet_Thau));
            Janus.Windows.GridEX.GridEXLayout grdChiTiet_DieuTiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChiTiet_Thau_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.txtSoThau = new AutoCompleteTextbox();
            this.txtVienDieuTiet = new AutoCompleteTextbox();
            this.txtGoiThau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNhomThau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLoaiThau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID_DieuTiet = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtThongTinKhac = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSoQDinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdChiTiet_DieuTiet = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdPrevius = new Janus.Windows.EditControls.UIButton();
            this.cmdNext = new Janus.Windows.EditControls.UIButton();
            this.grdChiTiet_Thau = new Janus.Windows.GridEX.GridEX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpNgayKT_DieuTiet = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpNgayHD_DieuTiet = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSoHD_Dieutiet = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTiet_DieuTiet)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTiet_Thau)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.BackColor = System.Drawing.SystemColors.Control;
            this.grpControl.Controls.Add(this.dtpNgayKT_DieuTiet);
            this.grpControl.Controls.Add(this.label15);
            this.grpControl.Controls.Add(this.dtpNgayHD_DieuTiet);
            this.grpControl.Controls.Add(this.label14);
            this.grpControl.Controls.Add(this.label13);
            this.grpControl.Controls.Add(this.txtSoHD_Dieutiet);
            this.grpControl.Controls.Add(this.txtSoThau);
            this.grpControl.Controls.Add(this.txtVienDieuTiet);
            this.grpControl.Controls.Add(this.txtGoiThau);
            this.grpControl.Controls.Add(this.label8);
            this.grpControl.Controls.Add(this.txtNhomThau);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.txtLoaiThau);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.txtID_DieuTiet);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.label11);
            this.grpControl.Controls.Add(this.txtThongTinKhac);
            this.grpControl.Controls.Add(this.txtSoQDinh);
            this.grpControl.Controls.Add(this.label10);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(1088, 105);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "Thông tin thầu";
            // 
            // txtSoThau
            // 
            this.txtSoThau._backcolor = System.Drawing.SystemColors.Control;
            this.txtSoThau._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoThau._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSoThau.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtSoThau.AutoCompleteList")));
            this.txtSoThau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoThau.CaseSensitive = false;
            this.txtSoThau.CompareNoID = true;
            this.txtSoThau.DefaultCode = "-1";
            this.txtSoThau.DefaultID = "-1";
            this.txtSoThau.Drug_ID = null;
            this.txtSoThau.ExtraWidth = 0;
            this.txtSoThau.FillValueAfterSelect = false;
            this.txtSoThau.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoThau.Location = new System.Drawing.Point(112, 44);
            this.txtSoThau.MaxHeight = 100;
            this.txtSoThau.MinTypedCharacters = 2;
            this.txtSoThau.MyCode = "-1";
            this.txtSoThau.MyID = "-1";
            this.txtSoThau.MyText = "";
            this.txtSoThau.Name = "txtSoThau";
            this.txtSoThau.RaiseEvent = false;
            this.txtSoThau.RaiseEventEnter = true;
            this.txtSoThau.RaiseEventEnterWhenEmpty = false;
            this.txtSoThau.SelectedIndex = -1;
            this.txtSoThau.Size = new System.Drawing.Size(167, 21);
            this.txtSoThau.splitChar = '@';
            this.txtSoThau.splitCharIDAndCode = '#';
            this.txtSoThau.TabIndex = 3;
            this.txtSoThau.TakeCode = false;
            this.txtSoThau.txtMyCode = null;
            this.txtSoThau.txtMyCode_Edit = null;
            this.txtSoThau.txtMyID = null;
            this.txtSoThau.txtMyID_Edit = null;
            this.txtSoThau.txtMyName = null;
            this.txtSoThau.txtMyName_Edit = null;
            this.txtSoThau.txtNext = null;
            this.txtSoThau._OnEnterMe += new AutoCompleteTextbox.OnEnterMe(this.txtSoThau__OnEnterMe);
            // 
            // txtVienDieuTiet
            // 
            this.txtVienDieuTiet._backcolor = System.Drawing.SystemColors.Control;
            this.txtVienDieuTiet._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVienDieuTiet._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtVienDieuTiet.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtVienDieuTiet.AutoCompleteList")));
            this.txtVienDieuTiet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVienDieuTiet.CaseSensitive = true;
            this.txtVienDieuTiet.CompareNoID = false;
            this.txtVienDieuTiet.DefaultCode = "-1";
            this.txtVienDieuTiet.DefaultID = "-1";
            this.txtVienDieuTiet.Drug_ID = null;
            this.txtVienDieuTiet.ExtraWidth = 0;
            this.txtVienDieuTiet.FillValueAfterSelect = false;
            this.txtVienDieuTiet.Location = new System.Drawing.Point(112, 69);
            this.txtVienDieuTiet.MaxHeight = 100;
            this.txtVienDieuTiet.MinTypedCharacters = 2;
            this.txtVienDieuTiet.MyCode = "-1";
            this.txtVienDieuTiet.MyID = "-1";
            this.txtVienDieuTiet.MyText = "";
            this.txtVienDieuTiet.Name = "txtVienDieuTiet";
            this.txtVienDieuTiet.RaiseEvent = false;
            this.txtVienDieuTiet.RaiseEventEnter = false;
            this.txtVienDieuTiet.RaiseEventEnterWhenEmpty = false;
            this.txtVienDieuTiet.SelectedIndex = -1;
            this.txtVienDieuTiet.Size = new System.Drawing.Size(412, 21);
            this.txtVienDieuTiet.splitChar = '@';
            this.txtVienDieuTiet.splitCharIDAndCode = '#';
            this.txtVienDieuTiet.TabIndex = 8;
            this.txtVienDieuTiet.TakeCode = false;
            this.txtVienDieuTiet.txtMyCode = null;
            this.txtVienDieuTiet.txtMyCode_Edit = null;
            this.txtVienDieuTiet.txtMyID = null;
            this.txtVienDieuTiet.txtMyID_Edit = null;
            this.txtVienDieuTiet.txtMyName = null;
            this.txtVienDieuTiet.txtMyName_Edit = null;
            this.txtVienDieuTiet.txtNext = null;
            // 
            // txtGoiThau
            // 
            this.txtGoiThau.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoiThau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoiThau.Location = new System.Drawing.Point(983, 43);
            this.txtGoiThau.Name = "txtGoiThau";
            this.txtGoiThau.ReadOnly = true;
            this.txtGoiThau.Size = new System.Drawing.Size(87, 23);
            this.txtGoiThau.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(927, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Gói thầu";
            // 
            // txtNhomThau
            // 
            this.txtNhomThau.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomThau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomThau.Location = new System.Drawing.Point(817, 43);
            this.txtNhomThau.Name = "txtNhomThau";
            this.txtNhomThau.ReadOnly = true;
            this.txtNhomThau.Size = new System.Drawing.Size(87, 23);
            this.txtNhomThau.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(746, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Nhóm thầu";
            // 
            // txtLoaiThau
            // 
            this.txtLoaiThau.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiThau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiThau.Location = new System.Drawing.Point(636, 43);
            this.txtLoaiThau.Name = "txtLoaiThau";
            this.txtLoaiThau.ReadOnly = true;
            this.txtLoaiThau.Size = new System.Drawing.Size(87, 23);
            this.txtLoaiThau.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(543, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Loại thầu";
            // 
            // txtID_DieuTiet
            // 
            this.txtID_DieuTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtID_DieuTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_DieuTiet.Location = new System.Drawing.Point(1072, 22);
            this.txtID_DieuTiet.Name = "txtID_DieuTiet";
            this.txtID_DieuTiet.Size = new System.Drawing.Size(10, 21);
            this.txtID_DieuTiet.TabIndex = 14;
            this.txtID_DieuTiet.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(10, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Viện nhận";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(543, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "Thông tin khác";
            // 
            // txtThongTinKhac
            // 
            this.txtThongTinKhac.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThongTinKhac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThongTinKhac.Location = new System.Drawing.Point(636, 68);
            this.txtThongTinKhac.Name = "txtThongTinKhac";
            this.txtThongTinKhac.Size = new System.Drawing.Size(434, 23);
            this.txtThongTinKhac.TabIndex = 9;
            // 
            // txtSoQDinh
            // 
            this.txtSoQDinh.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoQDinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoQDinh.Location = new System.Drawing.Point(380, 43);
            this.txtSoQDinh.Name = "txtSoQDinh";
            this.txtSoQDinh.ReadOnly = true;
            this.txtSoQDinh.Size = new System.Drawing.Size(144, 23);
            this.txtSoQDinh.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(285, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 9;
            this.label10.Text = "Số Q.định";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Số TT thầu";
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.grdChiTiet_DieuTiet);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox4.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox4.Image")));
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(538, 476);
            this.uiGroupBox4.TabIndex = 0;
            this.uiGroupBox4.Text = "Thông tin thuốc/vật tư trong thầu";
            // 
            // grdChiTiet_DieuTiet
            // 
            grdChiTiet_DieuTiet_DesignTimeLayout.LayoutString = resources.GetString("grdChiTiet_DieuTiet_DesignTimeLayout.LayoutString");
            this.grdChiTiet_DieuTiet.DesignTimeLayout = grdChiTiet_DieuTiet_DesignTimeLayout;
            this.grdChiTiet_DieuTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChiTiet_DieuTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdChiTiet_DieuTiet.GroupByBoxVisible = false;
            this.grdChiTiet_DieuTiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChiTiet_DieuTiet.Location = new System.Drawing.Point(3, 18);
            this.grdChiTiet_DieuTiet.Name = "grdChiTiet_DieuTiet";
            this.grdChiTiet_DieuTiet.RecordNavigator = true;
            this.grdChiTiet_DieuTiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiTiet_DieuTiet.Size = new System.Drawing.Size(532, 455);
            this.grdChiTiet_DieuTiet.TabIndex = 0;
            this.grdChiTiet_DieuTiet.TabStop = false;
            this.grdChiTiet_DieuTiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiTiet_DieuTiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChiTiet_DieuTiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdChiTiet_DieuTiet.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.grdChiTietDieuTiet_UpdatingCell);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdIn);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(538, 49);
            this.panel1.TabIndex = 1;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(223, 7);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(110, 33);
            this.cmdIn.TabIndex = 1;
            this.cmdIn.TabStop = false;
            this.cmdIn.Text = "&In";
            this.cmdIn.Visible = false;
            this.cmdIn.Click += new System.EventHandler(this.cmdInPhieuNhap_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(112, 7);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 33);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.TabStop = false;
            this.cmdSave.Text = "&Lưu";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(334, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(110, 33);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "&Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdPrevius);
            this.panel2.Controls.Add(this.cmdNext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(492, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(54, 525);
            this.panel2.TabIndex = 1;
            // 
            // cmdPrevius
            // 
            this.cmdPrevius.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrevius.Image")));
            this.cmdPrevius.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdPrevius.Location = new System.Drawing.Point(2, 262);
            this.cmdPrevius.Name = "cmdPrevius";
            this.cmdPrevius.Size = new System.Drawing.Size(52, 37);
            this.cmdPrevius.TabIndex = 1;
            this.cmdPrevius.Click += new System.EventHandler(this.cmdPrevius_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Image = ((System.Drawing.Image)(resources.GetObject("cmdNext.Image")));
            this.cmdNext.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdNext.Location = new System.Drawing.Point(2, 219);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(52, 37);
            this.cmdNext.TabIndex = 0;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // grdChiTiet_Thau
            // 
            this.grdChiTiet_Thau.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChiTiet_Thau_DesignTimeLayout.LayoutString = resources.GetString("grdChiTiet_Thau_DesignTimeLayout.LayoutString");
            this.grdChiTiet_Thau.DesignTimeLayout = grdChiTiet_Thau_DesignTimeLayout;
            this.grdChiTiet_Thau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChiTiet_Thau.FilterMode = Janus.Windows.GridEX.FilterMode.Manual;
            this.grdChiTiet_Thau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grdChiTiet_Thau.GroupByBoxVisible = false;
            this.grdChiTiet_Thau.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChiTiet_Thau.Location = new System.Drawing.Point(0, 0);
            this.grdChiTiet_Thau.Name = "grdChiTiet_Thau";
            this.grdChiTiet_Thau.RecordNavigator = true;
            this.grdChiTiet_Thau.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiTiet_Thau.Size = new System.Drawing.Size(492, 525);
            this.grdChiTiet_Thau.TabIndex = 0;
            this.grdChiTiet_Thau.TabStop = false;
            this.grdChiTiet_Thau.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiTiet_Thau.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChiTiet_Thau.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 105);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdChiTiet_Thau);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1088, 525);
            this.splitContainer1.SplitterDistance = 546;
            this.splitContainer1.TabIndex = 1;
            // 
            // dtpNgayKT_DieuTiet
            // 
            this.dtpNgayKT_DieuTiet.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKT_DieuTiet.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayKT_DieuTiet.DropDownCalendar.Name = "";
            this.dtpNgayKT_DieuTiet.Location = new System.Drawing.Point(636, 19);
            this.dtpNgayKT_DieuTiet.Name = "dtpNgayKT_DieuTiet";
            this.dtpNgayKT_DieuTiet.Size = new System.Drawing.Size(144, 21);
            this.dtpNgayKT_DieuTiet.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(543, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 15);
            this.label15.TabIndex = 34;
            this.label15.Text = "ngày kết thúc";
            // 
            // dtpNgayHD_DieuTiet
            // 
            this.dtpNgayHD_DieuTiet.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayHD_DieuTiet.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayHD_DieuTiet.DropDownCalendar.Name = "";
            this.dtpNgayHD_DieuTiet.Location = new System.Drawing.Point(380, 19);
            this.dtpNgayHD_DieuTiet.Name = "dtpNgayHD_DieuTiet";
            this.dtpNgayHD_DieuTiet.Size = new System.Drawing.Size(144, 21);
            this.dtpNgayHD_DieuTiet.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(285, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 15);
            this.label14.TabIndex = 32;
            this.label14.Text = "Ngày hợp đồng";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 15);
            this.label13.TabIndex = 30;
            this.label13.Text = "Số HĐ điều tiết";
            // 
            // txtSoHD_Dieutiet
            // 
            this.txtSoHD_Dieutiet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSoHD_Dieutiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHD_Dieutiet.Location = new System.Drawing.Point(112, 18);
            this.txtSoHD_Dieutiet.Name = "txtSoHD_Dieutiet";
            this.txtSoHD_Dieutiet.Size = new System.Drawing.Size(167, 23);
            this.txtSoHD_Dieutiet.TabIndex = 0;
            // 
            // frm_Add_DieuTiet_Thau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 630);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grpControl);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Add_DieuTiet_Thau";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật điều tiết thầu";
            this.Load += new System.EventHandler(this.frm_Add_DieuTiet_Thau_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Add_DieuTiet_Thau_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTiet_DieuTiet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChiTiet_Thau)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.GridEX.GridEX grdChiTiet_DieuTiet;
        private System.Windows.Forms.Label label10;
        internal Janus.Windows.GridEX.EditControls.EditBox txtSoQDinh;
        internal Janus.Windows.GridEX.EditControls.EditBox txtThongTinKhac;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        public Janus.Windows.GridEX.EditControls.EditBox txtID_DieuTiet;
        internal Janus.Windows.GridEX.EditControls.EditBox txtGoiThau;
        private System.Windows.Forms.Label label8;
        internal Janus.Windows.GridEX.EditControls.EditBox txtNhomThau;
        private System.Windows.Forms.Label label6;
        internal Janus.Windows.GridEX.EditControls.EditBox txtLoaiThau;
        private System.Windows.Forms.Label label4;
        private AutoCompleteTextbox txtVienDieuTiet;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdPrevius;
        private Janus.Windows.EditControls.UIButton cmdNext;
        private Janus.Windows.GridEX.GridEX grdChiTiet_Thau;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AutoCompleteTextbox txtSoThau;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayKT_DieuTiet;
        private System.Windows.Forms.Label label15;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayHD_DieuTiet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoHD_Dieutiet;
    }
}