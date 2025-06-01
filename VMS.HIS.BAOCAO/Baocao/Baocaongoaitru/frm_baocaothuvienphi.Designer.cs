namespace VNS.HIS.UI.Baocao
{
    partial class frm_baocaothuvienphi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocaothuvienphi));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem6 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkChitiet = new Janus.Windows.EditControls.UICheckBox();
            this.cboHoadon = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLoaitimkiem = new Janus.Windows.EditControls.UICheckBox();
            this.cboNTNT = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChitiet = new Janus.Windows.GridEX.GridEX();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.cboDoituongKCB = new Janus.Windows.EditControls.UIComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbokhoa = new Janus.Windows.EditControls.UIComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbonhanvien = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgayThanhToan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.radChotVet = new System.Windows.Forms.RadioButton();
            this.radoChotMoi = new System.Windows.Forms.RadioButton();
            this.cmdChotDanhSach = new Janus.Windows.EditControls.UIButton();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpNgayChot = new Janus.Windows.CalendarCombo.CalendarCombo();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(389, 13);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(119, 30);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayInPhieu.DropDownCalendar.Name = "";
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(88, 19);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 12;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(10, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 88;
            this.label3.Text = "Ngày in";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(514, 13);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(119, 30);
            this.cmdPrint.TabIndex = 9;
            this.cmdPrint.Text = "In báo cáo";
            this.cmdPrint.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(639, 13);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 30);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "&Thoát(Esc)";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.chkChitiet);
            this.uiGroupBox2.Controls.Add(this.cboHoadon);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.chkLoaitimkiem);
            this.uiGroupBox2.Controls.Add(this.cboNTNT);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.panel1);
            this.uiGroupBox2.Controls.Add(this.cboDoituongKCB);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.cbokhoa);
            this.uiGroupBox2.Controls.Add(this.label11);
            this.uiGroupBox2.Controls.Add(this.cbonhanvien);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Location = new System.Drawing.Point(3, 56);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(785, 488);
            this.uiGroupBox2.TabIndex = 115;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // chkChitiet
            // 
            this.chkChitiet.Checked = true;
            this.chkChitiet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChitiet.ForeColor = System.Drawing.Color.Navy;
            this.chkChitiet.Location = new System.Drawing.Point(645, 104);
            this.chkChitiet.Name = "chkChitiet";
            this.chkChitiet.Size = new System.Drawing.Size(119, 23);
            this.chkChitiet.TabIndex = 273;
            this.chkChitiet.Text = "In chi tiết?";
            this.toolTip1.SetToolTip(this.chkChitiet, "Bỏ check sẽ tìm theo ngày chốt viện phí");
            // 
            // cboHoadon
            // 
            this.cboHoadon.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = -1;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Không lấy hóa đơn";
            uiComboBoxItem2.Value = 0;
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Có lấy hóa đơn";
            uiComboBoxItem3.Value = 1;
            this.cboHoadon.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboHoadon.Location = new System.Drawing.Point(550, 77);
            this.cboHoadon.Name = "cboHoadon";
            this.cboHoadon.Size = new System.Drawing.Size(214, 21);
            this.cboHoadon.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(423, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 272;
            this.label4.Text = "Trạng thái hóa đơn:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkLoaitimkiem
            // 
            this.chkLoaitimkiem.Checked = true;
            this.chkLoaitimkiem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoaitimkiem.ForeColor = System.Drawing.Color.Navy;
            this.chkLoaitimkiem.Location = new System.Drawing.Point(450, 104);
            this.chkLoaitimkiem.Name = "chkLoaitimkiem";
            this.chkLoaitimkiem.Size = new System.Drawing.Size(183, 23);
            this.chkLoaitimkiem.TabIndex = 8;
            this.chkLoaitimkiem.Text = "Tìm theo ngày thanh toán?";
            this.toolTip1.SetToolTip(this.chkLoaitimkiem, "Bỏ check sẽ tìm theo ngày chốt viện phí");
            // 
            // cboNTNT
            // 
            this.cboNTNT.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Tất cả";
            uiComboBoxItem4.Value = -1;
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "Ngoại trú";
            uiComboBoxItem5.Value = 0;
            uiComboBoxItem6.FormatStyle.Alpha = 0;
            uiComboBoxItem6.IsSeparator = false;
            uiComboBoxItem6.Text = "Nội trú";
            uiComboBoxItem6.Value = 1;
            this.cboNTNT.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem4,
            uiComboBoxItem5,
            uiComboBoxItem6});
            this.cboNTNT.Location = new System.Drawing.Point(138, 48);
            this.cboNTNT.Name = "cboNTNT";
            this.cboNTNT.Size = new System.Drawing.Size(279, 21);
            this.cboNTNT.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 269;
            this.label1.Text = "Loại điều trị";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "đến";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdChitiet);
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(6, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 283);
            this.panel1.TabIndex = 56;
            // 
            // grdChitiet
            // 
            this.grdChitiet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdChitiet_DesignTimeLayout.LayoutString");
            this.grdChitiet.DesignTimeLayout = grdChitiet_DesignTimeLayout;
            this.grdChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChitiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChitiet.GroupByBoxVisible = false;
            this.grdChitiet.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdChitiet.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdChitiet.Name = "grdChitiet";
            this.grdChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.Size = new System.Drawing.Size(773, 283);
            this.grdChitiet.TabIndex = 21;
            this.grdChitiet.TabStop = false;
            this.grdChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChitiet.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.GroupTotalRowFormatStyle.FontItalic = Janus.Windows.GridEX.TriState.True;
            this.grdList.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdList.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(773, 283);
            this.grdList.TabIndex = 20;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdList.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdList_FormattingRow);
            // 
            // cboDoituongKCB
            // 
            this.cboDoituongKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoituongKCB.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoituongKCB.Location = new System.Drawing.Point(550, 50);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(214, 21);
            this.cboDoituongKCB.TabIndex = 2;
            this.cboDoituongKCB.Text = "Chọn loại đối tượng KCB";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(423, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 15);
            this.label10.TabIndex = 54;
            this.label10.Text = "Đối tượng KCB:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbokhoa
            // 
            this.cbokhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbokhoa.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cbokhoa.Location = new System.Drawing.Point(138, 22);
            this.cbokhoa.Name = "cbokhoa";
            this.cbokhoa.SelectInDataSource = true;
            this.cbokhoa.Size = new System.Drawing.Size(626, 21);
            this.cbokhoa.TabIndex = 0;
            this.cbokhoa.Text = "Chọn khoa KCB";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 15);
            this.label11.TabIndex = 52;
            this.label11.Text = "Khoa KCB:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbonhanvien
            // 
            this.cbonhanvien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbonhanvien.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cbonhanvien.Location = new System.Drawing.Point(138, 75);
            this.cbonhanvien.Name = "cbonhanvien";
            this.cbonhanvien.SelectInDataSource = true;
            this.cbonhanvien.Size = new System.Drawing.Size(279, 21);
            this.cbonhanvien.TabIndex = 3;
            this.cbonhanvien.Text = "Chọn thu ngân viên";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "Thu ngân viên:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(304, 106);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(113, 21);
            this.dtToDate.TabIndex = 7;
            this.dtToDate.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(138, 106);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(126, 21);
            this.dtFromDate.TabIndex = 6;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.ForeColor = System.Drawing.Color.Navy;
            this.chkByDate.Location = new System.Drawing.Point(67, 106);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(65, 23);
            this.chkByDate.TabIndex = 5;
            this.chkByDate.Text = "Từ ngày";
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(3, 3);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BCDSACH_BNHANLINHTHUOC";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(785, 53);
            this.baocaO_TIEUDE1.TabIndex = 114;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO CHỐT VIỆN PHÍ";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 573);
            this.tabControl1.TabIndex = 116;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.uiGroupBox2);
            this.tabPage1.Controls.Add(this.baocaO_TIEUDE1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Báo cáo thu viện phí";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdExportToExcel);
            this.panel2.Controls.Add(this.dtNgayInPhieu);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Controls.Add(this.cmdPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 492);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(785, 52);
            this.panel2.TabIndex = 115;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.dtpNgayThanhToan);
            this.tabPage2.Controls.Add(this.radChotVet);
            this.tabPage2.Controls.Add(this.radoChotMoi);
            this.tabPage2.Controls.Add(this.cmdChotDanhSach);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.dtpNgayChot);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chốt viện phí";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Ngày thanh toán";
            // 
            // dtpNgayThanhToan
            // 
            // 
            // 
            // 
            this.dtpNgayThanhToan.DropDownCalendar.Name = "";
            this.dtpNgayThanhToan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayThanhToan.Location = new System.Drawing.Point(136, 21);
            this.dtpNgayThanhToan.Name = "dtpNgayThanhToan";
            this.dtpNgayThanhToan.ShowUpDown = true;
            this.dtpNgayThanhToan.Size = new System.Drawing.Size(202, 21);
            this.dtpNgayThanhToan.TabIndex = 16;
            // 
            // radChotVet
            // 
            this.radChotVet.AutoSize = true;
            this.radChotVet.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radChotVet.Location = new System.Drawing.Point(71, 161);
            this.radChotVet.Name = "radChotVet";
            this.radChotVet.Size = new System.Drawing.Size(271, 19);
            this.radChotVet.TabIndex = 15;
            this.radChotVet.Text = "Chốt vét (Chốt các bản ghi còn lại trong ngày)";
            this.radChotVet.UseVisualStyleBackColor = true;
            // 
            // radoChotMoi
            // 
            this.radoChotMoi.AutoSize = true;
            this.radoChotMoi.Checked = true;
            this.radoChotMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radoChotMoi.Location = new System.Drawing.Point(71, 135);
            this.radoChotMoi.Name = "radoChotMoi";
            this.radoChotMoi.Size = new System.Drawing.Size(346, 19);
            this.radoChotMoi.TabIndex = 14;
            this.radoChotMoi.TabStop = true;
            this.radoChotMoi.Text = "Chốt danh sách(Chốt tất cả các bản ghi chưa được chốt)";
            this.radoChotMoi.UseVisualStyleBackColor = true;
            // 
            // cmdChotDanhSach
            // 
            this.cmdChotDanhSach.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChotDanhSach.Image = ((System.Drawing.Image)(resources.GetObject("cmdChotDanhSach.Image")));
            this.cmdChotDanhSach.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdChotDanhSach.Location = new System.Drawing.Point(136, 49);
            this.cmdChotDanhSach.Name = "cmdChotDanhSach";
            this.cmdChotDanhSach.Size = new System.Drawing.Size(202, 39);
            this.cmdChotDanhSach.TabIndex = 13;
            this.cmdChotDanhSach.Text = "Chốt danh sách ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(52, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Ngày chốt";
            // 
            // dtpNgayChot
            // 
            // 
            // 
            // 
            this.dtpNgayChot.DropDownCalendar.Name = "";
            this.dtpNgayChot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayChot.Location = new System.Drawing.Point(136, 94);
            this.dtpNgayChot.Name = "dtpNgayChot";
            this.dtpNgayChot.ShowUpDown = true;
            this.dtpNgayChot.Size = new System.Drawing.Size(202, 21);
            this.dtpNgayChot.TabIndex = 11;
            // 
            // frm_baocaothuvienphi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 573);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocaothuvienphi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo thu viện phí";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChitiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIComboBox cboDoituongKCB;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.EditControls.UIComboBox cbokhoa;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.EditControls.UIComboBox cbonhanvien;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIComboBox cboNTNT;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UICheckBox chkLoaitimkiem;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIComboBox cboHoadon;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UICheckBox chkChitiet;
        private Janus.Windows.GridEX.GridEX grdChitiet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayThanhToan;
        private System.Windows.Forms.RadioButton radChotVet;
        private System.Windows.Forms.RadioButton radoChotMoi;
        private Janus.Windows.EditControls.UIButton cmdChotDanhSach;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayChot;
    }
}