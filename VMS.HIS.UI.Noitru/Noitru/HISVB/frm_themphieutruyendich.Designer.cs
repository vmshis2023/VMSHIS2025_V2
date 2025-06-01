using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_themphieutruyendich
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
            Janus.Windows.GridEX.GridEXLayout grdThuockethop_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themphieutruyendich));
            Janus.Windows.GridEX.GridEXLayout grdDonthuocchitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdThuockethop = new Janus.Windows.GridEX.GridEX();
            this.grdDonthuocchitiet = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtKhoaphong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtidphieuthuoc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdGetdata = new Janus.Windows.EditControls.UIButton();
            this.txtBacSyCD = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtYta = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDrug_Id = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtKetthuc = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtSoLo = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTenThuoc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTocDo = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.dtBatDau = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtQuantity = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.chkContine = new Janus.Windows.EditControls.UICheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThuockethop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDonthuocchitiet)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.panel3);
            this.uiGroupBox1.Controls.Add(this.panel2);
            this.uiGroupBox1.Controls.Add(this.panel1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1264, 761);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin theo dõi dịch truyền";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdThuockethop);
            this.panel3.Controls.Add(this.grdDonthuocchitiet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(516, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(745, 687);
            this.panel3.TabIndex = 436;
            // 
            // grdThuockethop
            // 
            this.grdThuockethop.AlternatingColors = true;
            this.grdThuockethop.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdThuockethop_DesignTimeLayout.LayoutString = resources.GetString("grdThuockethop_DesignTimeLayout.LayoutString");
            this.grdThuockethop.DesignTimeLayout = grdThuockethop_DesignTimeLayout;
            this.grdThuockethop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThuockethop.DynamicFiltering = true;
            this.grdThuockethop.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThuockethop.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThuockethop.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThuockethop.Font = new System.Drawing.Font("Arial", 9F);
            this.grdThuockethop.GroupByBoxVisible = false;
            this.grdThuockethop.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdThuockethop.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdThuockethop.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThuockethop.Location = new System.Drawing.Point(0, 380);
            this.grdThuockethop.Name = "grdThuockethop";
            this.grdThuockethop.RecordNavigator = true;
            this.grdThuockethop.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThuockethop.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdThuockethop.Size = new System.Drawing.Size(745, 307);
            this.grdThuockethop.TabIndex = 4;
            this.grdThuockethop.TabStop = false;
            this.grdThuockethop.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThuockethop.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdThuockethop.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdThuockethop.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdThuockethop.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdThuockethop.UseGroupRowSelector = true;
            // 
            // grdDonthuocchitiet
            // 
            this.grdDonthuocchitiet.AlternatingColors = true;
            this.grdDonthuocchitiet.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdDonthuocchitiet_DesignTimeLayout.LayoutString = resources.GetString("grdDonthuocchitiet_DesignTimeLayout.LayoutString");
            this.grdDonthuocchitiet.DesignTimeLayout = grdDonthuocchitiet_DesignTimeLayout;
            this.grdDonthuocchitiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdDonthuocchitiet.DynamicFiltering = true;
            this.grdDonthuocchitiet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDonthuocchitiet.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDonthuocchitiet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDonthuocchitiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdDonthuocchitiet.GroupByBoxVisible = false;
            this.grdDonthuocchitiet.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdDonthuocchitiet.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdDonthuocchitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDonthuocchitiet.Location = new System.Drawing.Point(0, 0);
            this.grdDonthuocchitiet.Name = "grdDonthuocchitiet";
            this.grdDonthuocchitiet.RecordNavigator = true;
            this.grdDonthuocchitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDonthuocchitiet.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdDonthuocchitiet.Size = new System.Drawing.Size(745, 380);
            this.grdDonthuocchitiet.TabIndex = 4;
            this.grdDonthuocchitiet.TabStop = false;
            this.grdDonthuocchitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDonthuocchitiet.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Info;
            this.grdDonthuocchitiet.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDonthuocchitiet.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDonthuocchitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDonthuocchitiet.UseGroupRowSelector = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtKhoaphong);
            this.panel2.Controls.Add(this.txtidphieuthuoc);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.cmdGetdata);
            this.panel2.Controls.Add(this.txtBacSyCD);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dtCreateDate);
            this.panel2.Controls.Add(this.txtYta);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtDrug_Id);
            this.panel2.Controls.Add(this.dtKetthuc);
            this.panel2.Controls.Add(this.txtSoLo);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtTenThuoc);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTocDo);
            this.panel2.Controls.Add(this.dtBatDau);
            this.panel2.Controls.Add(this.txtQuantity);
            this.panel2.Controls.Add(this.chkContine);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 687);
            this.panel2.TabIndex = 435;
            // 
            // txtKhoaphong
            // 
            this.txtKhoaphong._backcolor = System.Drawing.Color.White;
            this.txtKhoaphong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoaphong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKhoaphong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKhoaphong.AutoCompleteList")));
            this.txtKhoaphong.BackColor = System.Drawing.Color.White;
            this.txtKhoaphong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhoaphong.buildShortcut = false;
            this.txtKhoaphong.CaseSensitive = false;
            this.txtKhoaphong.CompareNoID = true;
            this.txtKhoaphong.DefaultCode = "-1";
            this.txtKhoaphong.DefaultID = "-1";
            this.txtKhoaphong.DisplayType = 1;
            this.txtKhoaphong.Drug_ID = null;
            this.txtKhoaphong.ExtraWidth = 0;
            this.txtKhoaphong.FillValueAfterSelect = false;
            this.txtKhoaphong.Location = new System.Drawing.Point(139, 45);
            this.txtKhoaphong.MaxHeight = 100;
            this.txtKhoaphong.MinTypedCharacters = 2;
            this.txtKhoaphong.Multiline = true;
            this.txtKhoaphong.MyCode = "-1";
            this.txtKhoaphong.MyID = "-1";
            this.txtKhoaphong.MyText = "";
            this.txtKhoaphong.MyTextOnly = "";
            this.txtKhoaphong.Name = "txtKhoaphong";
            this.txtKhoaphong.RaiseEvent = false;
            this.txtKhoaphong.RaiseEventEnter = false;
            this.txtKhoaphong.RaiseEventEnterWhenEmpty = false;
            this.txtKhoaphong.ReadOnly = true;
            this.txtKhoaphong.SelectedIndex = -1;
            this.txtKhoaphong.Size = new System.Drawing.Size(367, 22);
            this.txtKhoaphong.splitChar = '@';
            this.txtKhoaphong.splitCharIDAndCode = '#';
            this.txtKhoaphong.TabIndex = 2;
            this.txtKhoaphong.TabStop = false;
            this.txtKhoaphong.TakeCode = false;
            this.txtKhoaphong.txtMyCode = null;
            this.txtKhoaphong.txtMyCode_Edit = null;
            this.txtKhoaphong.txtMyID = null;
            this.txtKhoaphong.txtMyID_Edit = null;
            this.txtKhoaphong.txtMyName = null;
            this.txtKhoaphong.txtMyName_Edit = null;
            this.txtKhoaphong.txtNext = null;
            // 
            // txtidphieuthuoc
            // 
            this.txtidphieuthuoc.Enabled = false;
            this.txtidphieuthuoc.Location = new System.Drawing.Point(139, 17);
            this.txtidphieuthuoc.Name = "txtidphieuthuoc";
            this.txtidphieuthuoc.ReadOnly = true;
            this.txtidphieuthuoc.Size = new System.Drawing.Size(145, 21);
            this.txtidphieuthuoc.TabIndex = 0;
            this.txtidphieuthuoc.TabStop = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(8, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 16);
            this.label13.TabIndex = 91;
            this.label13.Text = "Id phiếu thuốc";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(8, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 16);
            this.label9.TabIndex = 82;
            this.label9.Text = "Y tá thực hiện";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(369, 17);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(137, 21);
            this.txtID.TabIndex = 1;
            this.txtID.TabStop = false;
            // 
            // cmdGetdata
            // 
            this.cmdGetdata.Image = ((System.Drawing.Image)(resources.GetObject("cmdGetdata.Image")));
            this.cmdGetdata.Location = new System.Drawing.Point(319, 73);
            this.cmdGetdata.Name = "cmdGetdata";
            this.cmdGetdata.Size = new System.Drawing.Size(187, 23);
            this.cmdGetdata.TabIndex = 84;
            this.cmdGetdata.Text = "Tìm kiếm dịch truyền";
            this.cmdGetdata.Visible = false;
            this.cmdGetdata.Click += new System.EventHandler(this.cmdGetdata_Click);
            // 
            // txtBacSyCD
            // 
            this.txtBacSyCD._backcolor = System.Drawing.SystemColors.Control;
            this.txtBacSyCD._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacSyCD._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBacSyCD.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBacSyCD.AutoCompleteList")));
            this.txtBacSyCD.BackColor = System.Drawing.Color.White;
            this.txtBacSyCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacSyCD.buildShortcut = false;
            this.txtBacSyCD.CaseSensitive = false;
            this.txtBacSyCD.CompareNoID = true;
            this.txtBacSyCD.DefaultCode = "-1";
            this.txtBacSyCD.DefaultID = "-1";
            this.txtBacSyCD.DisplayType = 1;
            this.txtBacSyCD.Drug_ID = null;
            this.txtBacSyCD.ExtraWidth = 0;
            this.txtBacSyCD.FillValueAfterSelect = false;
            this.txtBacSyCD.Location = new System.Drawing.Point(139, 262);
            this.txtBacSyCD.MaxHeight = 100;
            this.txtBacSyCD.MinTypedCharacters = 2;
            this.txtBacSyCD.MyCode = "-1";
            this.txtBacSyCD.MyID = "-1";
            this.txtBacSyCD.MyText = "";
            this.txtBacSyCD.MyTextOnly = "";
            this.txtBacSyCD.Name = "txtBacSyCD";
            this.txtBacSyCD.RaiseEvent = false;
            this.txtBacSyCD.RaiseEventEnter = false;
            this.txtBacSyCD.RaiseEventEnterWhenEmpty = false;
            this.txtBacSyCD.ReadOnly = true;
            this.txtBacSyCD.SelectedIndex = -1;
            this.txtBacSyCD.Size = new System.Drawing.Size(367, 21);
            this.txtBacSyCD.splitChar = '@';
            this.txtBacSyCD.splitCharIDAndCode = '#';
            this.txtBacSyCD.TabIndex = 10;
            this.txtBacSyCD.TabStop = false;
            this.txtBacSyCD.TakeCode = false;
            this.txtBacSyCD.txtMyCode = null;
            this.txtBacSyCD.txtMyCode_Edit = null;
            this.txtBacSyCD.txtMyID = null;
            this.txtBacSyCD.txtMyID_Edit = null;
            this.txtBacSyCD.txtMyName = null;
            this.txtBacSyCD.txtMyName_Edit = null;
            this.txtBacSyCD.txtNext = null;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(8, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 16);
            this.label8.TabIndex = 80;
            this.label8.Text = "Bác sỹ CĐ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(296, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 16);
            this.label7.TabIndex = 66;
            this.label7.Text = "Mã phiếu";
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.Location = new System.Drawing.Point(139, 75);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(145, 21);
            this.dtCreateDate.TabIndex = 3;
            this.dtCreateDate.Value = new System.DateTime(2020, 4, 13, 0, 0, 0, 0);
            // 
            // txtYta
            // 
            this.txtYta._backcolor = System.Drawing.SystemColors.Control;
            this.txtYta._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYta._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtYta.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtYta.AutoCompleteList")));
            this.txtYta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYta.buildShortcut = false;
            this.txtYta.CaseSensitive = false;
            this.txtYta.CompareNoID = true;
            this.txtYta.DefaultCode = "-1";
            this.txtYta.DefaultID = "-1";
            this.txtYta.DisplayType = 1;
            this.txtYta.Drug_ID = null;
            this.txtYta.ExtraWidth = 0;
            this.txtYta.FillValueAfterSelect = false;
            this.txtYta.Location = new System.Drawing.Point(139, 290);
            this.txtYta.MaxHeight = 100;
            this.txtYta.MinTypedCharacters = 2;
            this.txtYta.MyCode = "-1";
            this.txtYta.MyID = "-1";
            this.txtYta.MyText = "";
            this.txtYta.MyTextOnly = "";
            this.txtYta.Name = "txtYta";
            this.txtYta.RaiseEvent = false;
            this.txtYta.RaiseEventEnter = false;
            this.txtYta.RaiseEventEnterWhenEmpty = false;
            this.txtYta.SelectedIndex = -1;
            this.txtYta.Size = new System.Drawing.Size(367, 21);
            this.txtYta.splitChar = '@';
            this.txtYta.splitCharIDAndCode = '#';
            this.txtYta.TabIndex = 11;
            this.txtYta.TakeCode = false;
            this.txtYta.txtMyCode = null;
            this.txtYta.txtMyCode_Edit = null;
            this.txtYta.txtMyID = null;
            this.txtYta.txtMyID_Edit = null;
            this.txtYta.txtMyName = null;
            this.txtYta.txtMyName_Edit = null;
            this.txtYta.txtNext = null;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(8, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 16);
            this.label6.TabIndex = 79;
            this.label6.Text = "Thời gian kết thúc";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 68;
            this.label1.Text = "Tên dịch truyền";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(8, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 16);
            this.label10.TabIndex = 86;
            this.label10.Text = "Ngày thực hiện";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDrug_Id
            // 
            this.txtDrug_Id.Enabled = false;
            this.txtDrug_Id.Location = new System.Drawing.Point(381, 156);
            this.txtDrug_Id.Name = "txtDrug_Id";
            this.txtDrug_Id.ReadOnly = true;
            this.txtDrug_Id.Size = new System.Drawing.Size(125, 21);
            this.txtDrug_Id.TabIndex = 94;
            this.txtDrug_Id.TabStop = false;
            this.txtDrug_Id.Visible = false;
            // 
            // dtKetthuc
            // 
            this.dtKetthuc.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtKetthuc.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtKetthuc.DropDownCalendar.Name = "";
            this.dtKetthuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtKetthuc.Location = new System.Drawing.Point(139, 236);
            this.dtKetthuc.Name = "dtKetthuc";
            this.dtKetthuc.ShowUpDown = true;
            this.dtKetthuc.Size = new System.Drawing.Size(145, 20);
            this.dtKetthuc.TabIndex = 9;
            this.dtKetthuc.Value = new System.DateTime(2020, 4, 13, 0, 0, 0, 0);
            // 
            // txtSoLo
            // 
            this.txtSoLo.Location = new System.Drawing.Point(139, 129);
            this.txtSoLo.Name = "txtSoLo";
            this.txtSoLo.ReadOnly = true;
            this.txtSoLo.Size = new System.Drawing.Size(367, 21);
            this.txtSoLo.TabIndex = 5;
            this.txtSoLo.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(290, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 16);
            this.label11.TabIndex = 87;
            this.label11.Text = "(Giọt/phút)";
            // 
            // txtTenThuoc
            // 
            this.txtTenThuoc.Location = new System.Drawing.Point(139, 102);
            this.txtTenThuoc.Name = "txtTenThuoc";
            this.txtTenThuoc.ReadOnly = true;
            this.txtTenThuoc.Size = new System.Drawing.Size(367, 21);
            this.txtTenThuoc.TabIndex = 4;
            this.txtTenThuoc.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(8, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 16);
            this.label5.TabIndex = 77;
            this.label5.Text = "Thời gian bắt đầu";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Lô/số sản xuất";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTocDo
            // 
            this.txtTocDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTocDo.Location = new System.Drawing.Point(139, 183);
            this.txtTocDo.Name = "txtTocDo";
            this.txtTocDo.Size = new System.Drawing.Size(145, 21);
            this.txtTocDo.TabIndex = 7;
            // 
            // dtBatDau
            // 
            this.dtBatDau.CustomFormat = "dd/MM/yyyy:HH:mm";
            this.dtBatDau.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtBatDau.DropDownCalendar.Name = "";
            this.dtBatDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtBatDau.Location = new System.Drawing.Point(139, 210);
            this.dtBatDau.Name = "dtBatDau";
            this.dtBatDau.ShowUpDown = true;
            this.dtBatDau.Size = new System.Drawing.Size(145, 20);
            this.dtBatDau.TabIndex = 8;
            this.dtBatDau.Value = new System.DateTime(2020, 4, 13, 0, 0, 0, 0);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(139, 156);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.ReadOnly = true;
            this.txtQuantity.Size = new System.Drawing.Size(145, 21);
            this.txtQuantity.TabIndex = 6;
            this.txtQuantity.TabStop = false;
            // 
            // chkContine
            // 
            this.chkContine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContine.ForeColor = System.Drawing.Color.Black;
            this.chkContine.Location = new System.Drawing.Point(139, 318);
            this.chkContine.Name = "chkContine";
            this.chkContine.Size = new System.Drawing.Size(355, 25);
            this.chkContine.TabIndex = 15;
            this.chkContine.TabStop = false;
            this.chkContine.Text = "Cho phép nhập liên tục";
            this.chkContine.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 16);
            this.label3.TabIndex = 73;
            this.label3.Text = "Số lượng";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(8, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 16);
            this.label4.TabIndex = 75;
            this.label4.Text = "Tốc độ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 16);
            this.label12.TabIndex = 90;
            this.label12.Text = "Khoa Phòng";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 704);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1258, 54);
            this.panel1.TabIndex = 434;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = global::VMS.HIS.Noitru.Properties.Resources.SAVE_AS;
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(1003, 6);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 39);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "Lưu thông tin";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1129, 6);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 39);
            this.cmdExit.TabIndex = 18;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = global::VMS.HIS.Noitru.Properties.Resources.printer_32;
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(877, 6);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 39);
            this.cmdPrint.TabIndex = 17;
            this.cmdPrint.Text = "In phiếu(F4)";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_themphieutruyendich
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_themphieutruyendich";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin phiếu theo dõi dịch truyền";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_themphieutruyendich_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themphieutruyendich_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThuockethop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDonthuocchitiet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoLo;
        internal System.Windows.Forms.Label label6;
        private Janus.Windows.CalendarCombo.CalendarCombo dtKetthuc;
        internal System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtBatDau;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label10;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIButton cmdGetdata;
        internal System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txtTocDo;
        private Janus.Windows.EditControls.UICheckBox chkContine;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        internal Janus.Windows.GridEX.EditControls.EditBox txtidphieuthuoc;
        internal System.Windows.Forms.Label label13;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenThuoc;
        internal Janus.Windows.GridEX.EditControls.EditBox txtDrug_Id;
        public Janus.Windows.GridEX.EditControls.EditBox txtID;
        public Janus.Windows.GridEX.EditControls.IntegerUpDown txtQuantity;
        private AutoCompleteTextbox txtYta;
        private AutoCompleteTextbox txtBacSyCD;
        private AutoCompleteTextbox txtKhoaphong;
        private Janus.Windows.GridEX.GridEX grdDonthuocchitiet;
        private Janus.Windows.GridEX.GridEX grdThuockethop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}