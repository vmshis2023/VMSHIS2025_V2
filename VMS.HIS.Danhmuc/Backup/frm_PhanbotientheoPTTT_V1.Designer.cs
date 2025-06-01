namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_PhanbotientheoPTTT_V1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhanbotientheoPTTT_V1));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.autoNganhang = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.autoNganhangchung = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlHuyThanhtoan = new System.Windows.Forms.Panel();
            this.txtTongtien = new MaskedTextBox.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNgaythanhtoan = new System.Windows.Forms.Label();
            this.dtPaymentDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.vbLine2 = new VNS.UCs.VBLine();
            this.pnlPhanbo = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.autoPttt = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtPttt_chung = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.pnlInfor.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlHuyThanhtoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.pnlPhanbo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAccept.Location = new System.Drawing.Point(526, 11);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(120, 35);
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "Chấp nhận";
            this.toolTip1.SetToolTip(this.cmdAccept, "Nhấn vào đây để bắt đầu hủy thanh toán cho các mục được chọn trên lưới");
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(652, 11);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để hủy bỏ việc phân bổ tiền theo phương thức thanh toán và quay lại " +
        "màn hình chính");
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // autoNganhang
            // 
            this.autoNganhang._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoNganhang._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNganhang._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoNganhang.AddValues = true;
            this.autoNganhang.AllowMultiline = false;
            this.autoNganhang.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoNganhang.AutoCompleteList")));
            this.autoNganhang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoNganhang.buildShortcut = false;
            this.autoNganhang.CaseSensitive = false;
            this.autoNganhang.CompareNoID = true;
            this.autoNganhang.DefaultCode = "-1";
            this.autoNganhang.DefaultID = "-1";
            this.autoNganhang.Drug_ID = null;
            this.autoNganhang.Enabled = false;
            this.autoNganhang.ExtraWidth = 0;
            this.autoNganhang.FillValueAfterSelect = false;
            this.autoNganhang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.autoNganhang.LOAI_DANHMUC = "NGANHANG";
            this.autoNganhang.Location = new System.Drawing.Point(379, 15);
            this.autoNganhang.MaxHeight = 150;
            this.autoNganhang.MinTypedCharacters = 2;
            this.autoNganhang.MyCode = "-1";
            this.autoNganhang.MyID = "-1";
            this.autoNganhang.Name = "autoNganhang";
            this.autoNganhang.RaiseEvent = false;
            this.autoNganhang.RaiseEventEnter = false;
            this.autoNganhang.RaiseEventEnterWhenEmpty = false;
            this.autoNganhang.SelectedIndex = -1;
            this.autoNganhang.ShowCodeWithValue = false;
            this.autoNganhang.Size = new System.Drawing.Size(353, 26);
            this.autoNganhang.splitChar = '@';
            this.autoNganhang.splitCharIDAndCode = '#';
            this.autoNganhang.TabIndex = 4;
            this.autoNganhang.TakeCode = false;
            this.toolTip1.SetToolTip(this.autoNganhang, "Nhấn vào đây để xem và bổ sung thêm danh mục dân tộc");
            this.autoNganhang.txtMyCode = null;
            this.autoNganhang.txtMyCode_Edit = null;
            this.autoNganhang.txtMyID = null;
            this.autoNganhang.txtMyID_Edit = null;
            this.autoNganhang.txtMyName = null;
            this.autoNganhang.txtMyName_Edit = null;
            this.autoNganhang.txtNext = null;
            this.autoNganhang.txtNext1 = null;
            // 
            // autoNganhangchung
            // 
            this.autoNganhangchung._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoNganhangchung._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoNganhangchung._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoNganhangchung.AddValues = true;
            this.autoNganhangchung.AllowMultiline = false;
            this.autoNganhangchung.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoNganhangchung.AutoCompleteList")));
            this.autoNganhangchung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoNganhangchung.buildShortcut = false;
            this.autoNganhangchung.CaseSensitive = false;
            this.autoNganhangchung.CompareNoID = true;
            this.autoNganhangchung.DefaultCode = "-1";
            this.autoNganhangchung.DefaultID = "-1";
            this.autoNganhangchung.Drug_ID = null;
            this.autoNganhangchung.ExtraWidth = 0;
            this.autoNganhangchung.FillValueAfterSelect = false;
            this.autoNganhangchung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.autoNganhangchung.LOAI_DANHMUC = "NGANHANG";
            this.autoNganhangchung.Location = new System.Drawing.Point(391, 53);
            this.autoNganhangchung.MaxHeight = 150;
            this.autoNganhangchung.MinTypedCharacters = 2;
            this.autoNganhangchung.MyCode = "-1";
            this.autoNganhangchung.MyID = "-1";
            this.autoNganhangchung.Name = "autoNganhangchung";
            this.autoNganhangchung.RaiseEvent = false;
            this.autoNganhangchung.RaiseEventEnter = false;
            this.autoNganhangchung.RaiseEventEnterWhenEmpty = false;
            this.autoNganhangchung.SelectedIndex = -1;
            this.autoNganhangchung.ShowCodeWithValue = false;
            this.autoNganhangchung.Size = new System.Drawing.Size(353, 26);
            this.autoNganhangchung.splitChar = '@';
            this.autoNganhangchung.splitCharIDAndCode = '#';
            this.autoNganhangchung.TabIndex = 600;
            this.autoNganhangchung.TakeCode = false;
            this.toolTip1.SetToolTip(this.autoNganhangchung, "Nhấn vào đây để xem và bổ sung thêm danh mục dân tộc");
            this.autoNganhangchung.txtMyCode = null;
            this.autoNganhangchung.txtMyCode_Edit = null;
            this.autoNganhangchung.txtMyID = null;
            this.autoNganhangchung.txtMyID_Edit = null;
            this.autoNganhangchung.txtMyName = null;
            this.autoNganhangchung.txtMyName_Edit = null;
            this.autoNganhangchung.txtNext = this.txtSotien;
            this.autoNganhangchung.txtNext1 = null;
            // 
            // txtSotien
            // 
            this.txtSotien.BackColor = System.Drawing.Color.White;
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtSotien.Location = new System.Drawing.Point(379, 47);
            this.txtSotien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(353, 26);
            this.txtSotien.TabIndex = 5;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.pnlActions);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 468);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(784, 93);
            this.pnlInfor.TabIndex = 8;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlHuyThanhtoan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 35);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(784, 58);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlHuyThanhtoan
            // 
            this.pnlHuyThanhtoan.Controls.Add(this.cmdAccept);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdExit);
            this.pnlHuyThanhtoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHuyThanhtoan.Location = new System.Drawing.Point(0, 0);
            this.pnlHuyThanhtoan.Name = "pnlHuyThanhtoan";
            this.pnlHuyThanhtoan.Size = new System.Drawing.Size(784, 58);
            this.pnlHuyThanhtoan.TabIndex = 1;
            // 
            // txtTongtien
            // 
            this.txtTongtien.BackColor = System.Drawing.Color.White;
            this.txtTongtien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongtien.Enabled = false;
            this.txtTongtien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtien.Location = new System.Drawing.Point(601, 27);
            this.txtTongtien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.ReadOnly = true;
            this.txtTongtien.Size = new System.Drawing.Size(143, 22);
            this.txtTongtien.TabIndex = 2;
            this.txtTongtien.TabStop = false;
            this.txtTongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(529, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 24);
            this.label9.TabIndex = 371;
            this.label9.Text = "Tổng tiền";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNgaythanhtoan
            // 
            this.lblNgaythanhtoan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaythanhtoan.ForeColor = System.Drawing.Color.Navy;
            this.lblNgaythanhtoan.Location = new System.Drawing.Point(6, 23);
            this.lblNgaythanhtoan.Name = "lblNgaythanhtoan";
            this.lblNgaythanhtoan.Size = new System.Drawing.Size(112, 24);
            this.lblNgaythanhtoan.TabIndex = 27;
            this.lblNgaythanhtoan.Text = "Ngày thanh toán:";
            this.lblNgaythanhtoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtPaymentDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtPaymentDate.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtPaymentDate.DropDownCalendar.Name = "";
            this.dtPaymentDate.Enabled = false;
            this.dtPaymentDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPaymentDate.Location = new System.Drawing.Point(124, 26);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.ShowUpDown = true;
            this.dtPaymentDate.Size = new System.Drawing.Size(147, 21);
            this.dtPaymentDate.TabIndex = 0;
            this.dtPaymentDate.TabStop = false;
            this.dtPaymentDate.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.autoNganhangchung);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.vbLine2);
            this.uiGroupBox2.Controls.Add(this.pnlPhanbo);
            this.uiGroupBox2.Controls.Add(this.txtTongtien);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.dtPaymentDate);
            this.uiGroupBox2.Controls.Add(this.lblNgaythanhtoan);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.txtPttt_chung);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(784, 468);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết phân bổ tiền theo hình thức thanh toán";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(282, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 601;
            this.label5.Text = "Ngân hàng:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vbLine2
            // 
            this.vbLine2._FontColor = System.Drawing.SystemColors.WindowText;
            this.vbLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine2.BackColor = System.Drawing.Color.Transparent;
            this.vbLine2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.vbLine2.Location = new System.Drawing.Point(14, 90);
            this.vbLine2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.vbLine2.Name = "vbLine2";
            this.vbLine2.Size = new System.Drawing.Size(753, 22);
            this.vbLine2.TabIndex = 598;
            this.vbLine2.TabStop = false;
            this.vbLine2.YourText = "Thực hiện phân bổ PTTT";
            // 
            // pnlPhanbo
            // 
            this.pnlPhanbo.Controls.Add(this.autoPttt);
            this.pnlPhanbo.Controls.Add(this.autoNganhang);
            this.pnlPhanbo.Controls.Add(this.label27);
            this.pnlPhanbo.Controls.Add(this.label4);
            this.pnlPhanbo.Controls.Add(this.txtSotien);
            this.pnlPhanbo.Controls.Add(this.label2);
            this.pnlPhanbo.Location = new System.Drawing.Point(15, 119);
            this.pnlPhanbo.Name = "pnlPhanbo";
            this.pnlPhanbo.Size = new System.Drawing.Size(756, 86);
            this.pnlPhanbo.TabIndex = 599;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(270, 17);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(103, 24);
            this.label27.TabIndex = 393;
            this.label27.Text = "Ngân hàng:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(17, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 24);
            this.label4.TabIndex = 397;
            this.label4.Text = "PTTT:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(270, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 24);
            this.label2.TabIndex = 384;
            this.label2.Text = "Số tiền:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(771, 24);
            this.label7.TabIndex = 395;
            this.label7.Text = "Bạn có thể nhấn Ctr+C để lấy số tiền phân bổ còn lại cho phương thức thanh toán đ" +
    "ang chọn";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(3, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(778, 24);
            this.label3.TabIndex = 394;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.BackColor = System.Drawing.Color.Silver;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 12F);
            this.grdList.FrozenColumns = 3;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 244);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(778, 221);
            this.grdList.TabIndex = 385;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(282, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 383;
            this.label1.Text = "PTTT chung:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // autoPttt
            // 
            this.autoPttt._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoPttt._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoPttt._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoPttt.AddValues = true;
            this.autoPttt.AllowMultiline = false;
            this.autoPttt.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoPttt.AutoCompleteList")));
            this.autoPttt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoPttt.buildShortcut = false;
            this.autoPttt.CaseSensitive = false;
            this.autoPttt.CompareNoID = true;
            this.autoPttt.DefaultCode = "-1";
            this.autoPttt.DefaultID = "-1";
            this.autoPttt.Drug_ID = null;
            this.autoPttt.ExtraWidth = 0;
            this.autoPttt.FillValueAfterSelect = false;
            this.autoPttt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.autoPttt.LOAI_DANHMUC = "PHUONGTHUCTHANHTOAN";
            this.autoPttt.Location = new System.Drawing.Point(96, 16);
            this.autoPttt.MaxHeight = -1;
            this.autoPttt.MinTypedCharacters = 2;
            this.autoPttt.MyCode = "-1";
            this.autoPttt.MyID = "-1";
            this.autoPttt.Name = "autoPttt";
            this.autoPttt.RaiseEvent = false;
            this.autoPttt.RaiseEventEnter = true;
            this.autoPttt.RaiseEventEnterWhenEmpty = false;
            this.autoPttt.SelectedIndex = -1;
            this.autoPttt.ShowCodeWithValue = false;
            this.autoPttt.Size = new System.Drawing.Size(148, 26);
            this.autoPttt.splitChar = '@';
            this.autoPttt.splitCharIDAndCode = '#';
            this.autoPttt.TabIndex = 3;
            this.autoPttt.TakeCode = false;
            this.toolTip1.SetToolTip(this.autoPttt, "Nhấn vào đây để xem và bổ sung thêm danh mục dân tộc");
            this.autoPttt.txtMyCode = null;
            this.autoPttt.txtMyCode_Edit = null;
            this.autoPttt.txtMyID = null;
            this.autoPttt.txtMyID_Edit = null;
            this.autoPttt.txtMyName = null;
            this.autoPttt.txtMyName_Edit = null;
            this.autoPttt.txtNext = null;
            this.autoPttt.txtNext1 = null;
            // 
            // txtPttt_chung
            // 
            this.txtPttt_chung._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPttt_chung._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPttt_chung._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPttt_chung.AddValues = true;
            this.txtPttt_chung.AllowMultiline = false;
            this.txtPttt_chung.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPttt_chung.AutoCompleteList")));
            this.txtPttt_chung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPttt_chung.buildShortcut = false;
            this.txtPttt_chung.CaseSensitive = false;
            this.txtPttt_chung.CompareNoID = true;
            this.txtPttt_chung.DefaultCode = "-1";
            this.txtPttt_chung.DefaultID = "-1";
            this.txtPttt_chung.Drug_ID = null;
            this.txtPttt_chung.ExtraWidth = 0;
            this.txtPttt_chung.FillValueAfterSelect = false;
            this.txtPttt_chung.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPttt_chung.LOAI_DANHMUC = "PHUONGTHUCTHANHTOAN";
            this.txtPttt_chung.Location = new System.Drawing.Point(391, 25);
            this.txtPttt_chung.MaxHeight = -1;
            this.txtPttt_chung.MinTypedCharacters = 2;
            this.txtPttt_chung.MyCode = "-1";
            this.txtPttt_chung.MyID = "-1";
            this.txtPttt_chung.Name = "txtPttt_chung";
            this.txtPttt_chung.RaiseEvent = false;
            this.txtPttt_chung.RaiseEventEnter = true;
            this.txtPttt_chung.RaiseEventEnterWhenEmpty = false;
            this.txtPttt_chung.SelectedIndex = -1;
            this.txtPttt_chung.ShowCodeWithValue = false;
            this.txtPttt_chung.Size = new System.Drawing.Size(134, 22);
            this.txtPttt_chung.splitChar = '@';
            this.txtPttt_chung.splitCharIDAndCode = '#';
            this.txtPttt_chung.TabIndex = 1;
            this.txtPttt_chung.TakeCode = false;
            this.toolTip1.SetToolTip(this.txtPttt_chung, "Nhấn vào đây để xem và bổ sung thêm danh mục dân tộc");
            this.txtPttt_chung.txtMyCode = null;
            this.txtPttt_chung.txtMyCode_Edit = null;
            this.txtPttt_chung.txtMyID = null;
            this.txtPttt_chung.txtMyID_Edit = null;
            this.txtPttt_chung.txtMyName = null;
            this.txtPttt_chung.txtMyName_Edit = null;
            this.txtPttt_chung.txtNext = this.autoPttt;
            this.txtPttt_chung.txtNext1 = null;
            // 
            // frm_PhanbotientheoPTTT_V1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PhanbotientheoPTTT_V1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân bổ tiền theo hình thức thanh toán";
            this.pnlInfor.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlHuyThanhtoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.pnlPhanbo.ResumeLayout(false);
            this.pnlPhanbo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlHuyThanhtoan;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNgaythanhtoan;
        private Janus.Windows.CalendarCombo.CalendarCombo dtPaymentDate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private MaskedTextBox.MaskedTextBox txtTongtien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label27;
        private UCs.AutoCompleteTextbox_Danhmucchung autoNganhang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private VNS.UCs.VBLine vbLine2;
        private System.Windows.Forms.Panel pnlPhanbo;
        private UCs.AutoCompleteTextbox_Danhmucchung autoNganhangchung;
        private System.Windows.Forms.Label label5;
        private UCs.AutoCompleteTextbox_Danhmucchung autoPttt;
        private UCs.AutoCompleteTextbox_Danhmucchung txtPttt_chung;
    }
}