namespace VNS.HIS.UI.Baocao
{
    partial class frm_baocaodanhsachhoadonthuphi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocaodanhsachhoadonthuphi));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem7 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem8 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem9 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem10 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem11 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem12 = new Janus.Windows.EditControls.UIComboBoxItem();
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
            this.cboHoadon = new Janus.Windows.EditControls.UIComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLoaitimkiem = new Janus.Windows.EditControls.UICheckBox();
            this.cboNTNT = new Janus.Windows.EditControls.UIComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cboQuyen = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtfromSerie = new MaskedTextBox.MaskedTextBox();
            this.txtToserie = new MaskedTextBox.MaskedTextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE();
            this.pnlQuyen = new System.Windows.Forms.Panel();
            this.chkMaQuyen = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlQuyen.SuspendLayout();
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
            this.cmdExportToExcel.Location = new System.Drawing.Point(395, 532);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(119, 30);
            this.cmdExportToExcel.TabIndex = 13;
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
            this.dtNgayInPhieu.Location = new System.Drawing.Point(82, 539);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 15;
            this.dtNgayInPhieu.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(4, 543);
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
            this.cmdPrint.Location = new System.Drawing.Point(520, 532);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(119, 30);
            this.cmdPrint.TabIndex = 12;
            this.cmdPrint.Text = "In báo cáo";
            this.cmdPrint.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(645, 532);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 30);
            this.cmdExit.TabIndex = 14;
            this.cmdExit.Text = "&Thoát(Esc)";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.chkMaQuyen);
            this.uiGroupBox2.Controls.Add(this.pnlQuyen);
            this.uiGroupBox2.Controls.Add(this.lblMsg);
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
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 59);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(776, 466);
            this.uiGroupBox2.TabIndex = 115;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // cboHoadon
            // 
            this.cboHoadon.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem7.FormatStyle.Alpha = 0;
            uiComboBoxItem7.IsSeparator = false;
            uiComboBoxItem7.Text = "Tất cả";
            uiComboBoxItem7.Value = -1;
            uiComboBoxItem8.FormatStyle.Alpha = 0;
            uiComboBoxItem8.IsSeparator = false;
            uiComboBoxItem8.Text = "Không lấy hóa đơn";
            uiComboBoxItem8.Value = 0;
            uiComboBoxItem9.FormatStyle.Alpha = 0;
            uiComboBoxItem9.IsSeparator = false;
            uiComboBoxItem9.Text = "Có lấy hóa đơn";
            uiComboBoxItem9.Value = 1;
            this.cboHoadon.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem7,
            uiComboBoxItem8,
            uiComboBoxItem9});
            this.cboHoadon.Location = new System.Drawing.Point(535, 75);
            this.cboHoadon.Name = "cboHoadon";
            this.cboHoadon.Size = new System.Drawing.Size(229, 21);
            this.cboHoadon.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(423, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 16);
            this.label4.TabIndex = 272;
            this.label4.Text = "Trạng thái HĐ:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkLoaitimkiem
            // 
            this.chkLoaitimkiem.Checked = true;
            this.chkLoaitimkiem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoaitimkiem.Location = new System.Drawing.Point(535, 130);
            this.chkLoaitimkiem.Name = "chkLoaitimkiem";
            this.chkLoaitimkiem.Size = new System.Drawing.Size(189, 23);
            this.chkLoaitimkiem.TabIndex = 11;
            this.chkLoaitimkiem.Text = "Tìm theo ngày thanh toán?";
            this.toolTip1.SetToolTip(this.chkLoaitimkiem, "Bỏ check sẽ tìm theo ngày chốt viện phí");
            // 
            // cboNTNT
            // 
            this.cboNTNT.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem10.FormatStyle.Alpha = 0;
            uiComboBoxItem10.IsSeparator = false;
            uiComboBoxItem10.Text = "Tất cả";
            uiComboBoxItem10.Value = -1;
            uiComboBoxItem11.FormatStyle.Alpha = 0;
            uiComboBoxItem11.IsSeparator = false;
            uiComboBoxItem11.Text = "Ngoại trú";
            uiComboBoxItem11.Value = 0;
            uiComboBoxItem12.FormatStyle.Alpha = 0;
            uiComboBoxItem12.IsSeparator = false;
            uiComboBoxItem12.Text = "Nội trú";
            uiComboBoxItem12.Value = 1;
            this.cboNTNT.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem10,
            uiComboBoxItem11,
            uiComboBoxItem12});
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
            this.label2.Location = new System.Drawing.Point(270, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "đến";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Location = new System.Drawing.Point(6, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 279);
            this.panel1.TabIndex = 56;
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
            this.grdList.Size = new System.Drawing.Size(758, 279);
            this.grdList.TabIndex = 16;
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
            this.cboDoituongKCB.Location = new System.Drawing.Point(535, 48);
            this.cboDoituongKCB.Name = "cboDoituongKCB";
            this.cboDoituongKCB.SelectInDataSource = true;
            this.cboDoituongKCB.Size = new System.Drawing.Size(229, 21);
            this.cboDoituongKCB.TabIndex = 2;
            this.cboDoituongKCB.Text = "Chọn loại đối tượng KCB";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(423, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 15);
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
            this.label8.Location = new System.Drawing.Point(11, 79);
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
            this.dtToDate.Location = new System.Drawing.Point(304, 130);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(113, 21);
            this.dtToDate.TabIndex = 10;
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
            this.dtFromDate.Location = new System.Drawing.Point(138, 130);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(126, 21);
            this.dtFromDate.TabIndex = 9;
            this.dtFromDate.Value = new System.DateTime(2014, 9, 27, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(48, 130);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(84, 23);
            this.chkByDate.TabIndex = 8;
            this.chkByDate.Text = "Từ ngày";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cboQuyen
            // 
            this.cboQuyen.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboQuyen.Location = new System.Drawing.Point(5, 3);
            this.cboQuyen.Name = "cboQuyen";
            this.cboQuyen.Size = new System.Drawing.Size(279, 21);
            this.cboQuyen.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 295;
            this.label5.Text = "Serie từ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 293;
            this.label6.Text = "Đến";
            // 
            // txtfromSerie
            // 
            this.txtfromSerie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtfromSerie.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfromSerie.Location = new System.Drawing.Point(402, 2);
            this.txtfromSerie.Masked = MaskedTextBox.Mask.Digit;
            this.txtfromSerie.MaxLength = 12;
            this.txtfromSerie.Name = "txtfromSerie";
            this.txtfromSerie.Size = new System.Drawing.Size(89, 21);
            this.txtfromSerie.TabIndex = 1;
            this.txtfromSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtToserie
            // 
            this.txtToserie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToserie.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToserie.Location = new System.Drawing.Point(542, 2);
            this.txtToserie.Masked = MaskedTextBox.Mask.Digit;
            this.txtToserie.MaxLength = 12;
            this.txtToserie.Name = "txtToserie";
            this.txtToserie.Size = new System.Drawing.Size(89, 21);
            this.txtToserie.TabIndex = 2;
            this.txtToserie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMsg
            // 
            this.lblMsg.Location = new System.Drawing.Point(12, 162);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(752, 15);
            this.lblMsg.TabIndex = 300;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BCDSACH_BNHANLINHTHUOC";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(776, 53);
            this.baocaO_TIEUDE1.TabIndex = 114;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO DANH SÁCH HÓA ĐƠN THU PHÍ";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlQuyen
            // 
            this.pnlQuyen.Controls.Add(this.cboQuyen);
            this.pnlQuyen.Controls.Add(this.label5);
            this.pnlQuyen.Controls.Add(this.label6);
            this.pnlQuyen.Controls.Add(this.txtToserie);
            this.pnlQuyen.Controls.Add(this.txtfromSerie);
            this.pnlQuyen.Location = new System.Drawing.Point(133, 99);
            this.pnlQuyen.Name = "pnlQuyen";
            this.pnlQuyen.Size = new System.Drawing.Size(636, 27);
            this.pnlQuyen.TabIndex = 5;
            this.pnlQuyen.TabStop = true;
            // 
            // chkMaQuyen
            // 
            this.chkMaQuyen.Checked = true;
            this.chkMaQuyen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaQuyen.Location = new System.Drawing.Point(48, 101);
            this.chkMaQuyen.Name = "chkMaQuyen";
            this.chkMaQuyen.Size = new System.Drawing.Size(84, 23);
            this.chkMaQuyen.TabIndex = 4;
            this.chkMaQuyen.Text = "Mã Quyển:";
            // 
            // frm_baocaodanhsachhoadonthuphi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 573);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdExit);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocaodanhsachhoadonthuphi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo danh sách hóa đơn thu phí";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlQuyen.ResumeLayout(false);
            this.pnlQuyen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private VNS.HIS.UI.DANHMUC.UCs.BAOCAO_TIEUDE baocaO_TIEUDE1;
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
        private Janus.Windows.EditControls.UIComboBox cboQuyen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private MaskedTextBox.MaskedTextBox txtToserie;
        private MaskedTextBox.MaskedTextBox txtfromSerie;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UICheckBox chkMaQuyen;
        private System.Windows.Forms.Panel pnlQuyen;
    }
}