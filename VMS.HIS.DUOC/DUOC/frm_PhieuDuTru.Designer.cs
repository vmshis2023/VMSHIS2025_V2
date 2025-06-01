namespace VNS.HIS.UI.THUOC
{
    partial class frm_PhieuDuTru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuDuTru));
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHuy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHuyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.chkUpdate = new Janus.Windows.EditControls.UICheckBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtthuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkHienthithuoccoDutru = new Janus.Windows.EditControls.UICheckBox();
            this.txtSoluongdutru = new MaskedTextBox.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKhoxuat = new Janus.Windows.EditControls.UIComboBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdHuydutru_all = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdPhieulinhbututruc = new Janus.Windows.EditControls.UIButton();
            this.cboKhonhan = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboKhoalinh = new VNS.HIS.UCs.EasyCompletionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.ContextMenuStrip = this.contextMenuStrip1;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 17);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.RowWithErrorsFormatStyle.ForeColor = System.Drawing.SystemColors.Control;
            this.grdList.Size = new System.Drawing.Size(1148, 573);
            this.grdList.TabIndex = 4;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdList.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdList_FormattingRow);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHuy,
            this.toolStripMenuItem1,
            this.mnuHuyAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(325, 54);
            // 
            // mnuHuy
            // 
            this.mnuHuy.Name = "mnuHuy";
            this.mnuHuy.Size = new System.Drawing.Size(324, 22);
            this.mnuHuy.Text = "Hủy dự trù thuốc đang chọn";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(321, 6);
            // 
            // mnuHuyAll
            // 
            this.mnuHuyAll.Name = "mnuHuyAll";
            this.mnuHuyAll.Size = new System.Drawing.Size(324, 22);
            this.mnuHuyAll.Text = "Hủy dự trù toàn bộ thuốc trong kho đang chọn";
            // 
            // chkUpdate
            // 
            this.chkUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUpdate.Checked = true;
            this.chkUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUpdate.Location = new System.Drawing.Point(208, 692);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(28, 23);
            this.chkUpdate.TabIndex = 9;
            this.chkUpdate.Tag = "DUOC_PHIEUDUTRU_LUUSAUKHINHAP";
            this.chkUpdate.Text = "Cập nhật ngay sau khi thay đổi trên lưới?";
            this.chkUpdate.Visible = false;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(3, 85);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1154, 593);
            this.uiGroupBox2.TabIndex = 465;
            this.uiGroupBox2.Text = "Danh sách thuốc";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cboKhoalinh);
            this.uiGroupBox1.Controls.Add(this.cboKhonhan);
            this.uiGroupBox1.Controls.Add(this.txtthuoc);
            this.uiGroupBox1.Controls.Add(this.cmdSearch);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.chkHienthithuoccoDutru);
            this.uiGroupBox1.Controls.Add(this.txtSoluongdutru);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1169, 142);
            this.uiGroupBox1.TabIndex = 464;
            this.uiGroupBox1.Text = "Chọn kho thuốc";
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtthuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthuoc.AutoCompleteList")));
            this.txtthuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthuoc.buildShortcut = false;
            this.txtthuoc.CaseSensitive = false;
            this.txtthuoc.CompareNoID = true;
            this.txtthuoc.DefaultCode = "-1";
            this.txtthuoc.DefaultID = "-1";
            this.txtthuoc.DisplayType = 0;
            this.txtthuoc.Drug_ID = null;
            this.txtthuoc.ExtraWidth = 0;
            this.txtthuoc.FillValueAfterSelect = false;
            this.txtthuoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.Location = new System.Drawing.Point(127, 48);
            this.txtthuoc.MaxHeight = 289;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.MyText = "";
            this.txtthuoc.MyTextOnly = "";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = true;
            this.txtthuoc.RaiseEventEnter = true;
            this.txtthuoc.RaiseEventEnterWhenEmpty = true;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(419, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 4;
            this.txtthuoc.TakeCode = false;
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = null;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSearch.Location = new System.Drawing.Point(1002, 17);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(152, 51);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Tag = "0";
            this.cmdSearch.Text = "Lấy dữ liệu dự trù";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(586, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 50;
            this.label6.Text = "Kho dự trù:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Khoa dự trù:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkHienthithuoccoDutru
            // 
            this.chkHienthithuoccoDutru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkHienthithuoccoDutru.Checked = true;
            this.chkHienthithuoccoDutru.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHienthithuoccoDutru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHienthithuoccoDutru.Location = new System.Drawing.Point(127, 109);
            this.chkHienthithuoccoDutru.Name = "chkHienthithuoccoDutru";
            this.chkHienthithuoccoDutru.Size = new System.Drawing.Size(257, 23);
            this.chkHienthithuoccoDutru.TabIndex = 3;
            this.chkHienthithuoccoDutru.TabStop = false;
            this.chkHienthithuoccoDutru.Text = "Hiển thị các thuốc có dự trù lên trên cùng?";
            // 
            // txtSoluongdutru
            // 
            this.txtSoluongdutru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoluongdutru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoluongdutru.Location = new System.Drawing.Point(682, 47);
            this.txtSoluongdutru.Masked = MaskedTextBox.Mask.Digit;
            this.txtSoluongdutru.Name = "txtSoluongdutru";
            this.txtSoluongdutru.Size = new System.Drawing.Size(184, 21);
            this.txtSoluongdutru.TabIndex = 5;
            this.txtSoluongdutru.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(561, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số lượng dự trù:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Thuốc dự trù:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(447, 692);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "(Chú ý: Chỉ các kho lẻ mới hiển thị để lập dự trù)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(401, 690);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn kho xuất:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // cboKhoxuat
            // 
            this.cboKhoxuat.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoxuat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoxuat.Location = new System.Drawing.Point(417, 690);
            this.cboKhoxuat.Name = "cboKhoxuat";
            this.cboKhoxuat.Size = new System.Drawing.Size(13, 21);
            this.cboKhoxuat.TabIndex = 0;
            this.cboKhoxuat.Text = "Kho thuốc";
            this.cboKhoxuat.Visible = false;
            this.cboKhoxuat.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(741, 691);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(133, 31);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1019, 690);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(135, 31);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát Form";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdHuydutru_all
            // 
            this.cmdHuydutru_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHuydutru_all.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuydutru_all.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuydutru_all.Image")));
            this.cmdHuydutru_all.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHuydutru_all.Location = new System.Drawing.Point(6, 690);
            this.cmdHuydutru_all.Name = "cmdHuydutru_all";
            this.cmdHuydutru_all.Size = new System.Drawing.Size(115, 31);
            this.cmdHuydutru_all.TabIndex = 5;
            this.cmdHuydutru_all.TabStop = false;
            this.cmdHuydutru_all.Text = "Hủy dự trù";
            this.toolTip1.SetToolTip(this.cmdHuydutru_all, "Hủy dự trù cho các thuốc đánh dấu chọn trên lưới");
            this.cmdHuydutru_all.Click += new System.EventHandler(this.cmdHuydutru_all_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(880, 690);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(133, 31);
            this.cmdPrint.TabIndex = 7;
            this.cmdPrint.Text = "In (Ctrl+P)";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdPhieulinhbututruc
            // 
            this.cmdPhieulinhbututruc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPhieulinhbututruc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPhieulinhbututruc.Image = global::VMS.HIS.Duoc.Properties.Resources.printer_32;
            this.cmdPhieulinhbututruc.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPhieulinhbututruc.Location = new System.Drawing.Point(574, 691);
            this.cmdPhieulinhbututruc.Name = "cmdPhieulinhbututruc";
            this.cmdPhieulinhbututruc.Size = new System.Drawing.Size(161, 31);
            this.cmdPhieulinhbututruc.TabIndex = 466;
            this.cmdPhieulinhbututruc.Text = "In phiếu lĩnh bù tủ trực";
            this.cmdPhieulinhbututruc.Click += new System.EventHandler(this.cmdPhieulinhbututruc_Click);
            // 
            // cboKhonhan
            // 
            this.cboKhonhan.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cboKhonhan.FormattingEnabled = true;
            this.cboKhonhan.Location = new System.Drawing.Point(682, 20);
            this.cboKhonhan.Name = "cboKhonhan";
            this.cboKhonhan.Size = new System.Drawing.Size(314, 24);
            this.cboKhonhan.TabIndex = 1;
            // 
            // cboKhoalinh
            // 
            this.cboKhoalinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cboKhoalinh.FormattingEnabled = true;
            this.cboKhoalinh.Location = new System.Drawing.Point(127, 20);
            this.cboKhoalinh.Name = "cboKhoalinh";
            this.cboKhoalinh.Size = new System.Drawing.Size(419, 24);
            this.cboKhoalinh.TabIndex = 0;
            // 
            // frm_PhieuDuTru
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1169, 729);
            this.Controls.Add(this.cmdPhieulinhbututruc);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.chkUpdate);
            this.Controls.Add(this.cmdHuydutru_all);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cboKhoxuat);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "frm_PhieuDuTru";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý lập dự trù thuốc trong kho/Cơ số tủ trực";
            this.Load += new System.EventHandler(this.frm_PhieuDuTru_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UICheckBox chkUpdate;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIComboBox cboKhoxuat;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private MaskedTextBox.MaskedTextBox txtSoluongdutru;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIButton cmdHuydutru_all;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHuy;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuHuyAll;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UICheckBox chkHienthithuoccoDutru;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private UCs.AutoCompleteTextbox txtthuoc;
        private Janus.Windows.EditControls.UIButton cmdPhieulinhbututruc;
        private UCs.EasyCompletionComboBox cboKhoalinh;
        private UCs.EasyCompletionComboBox cboKhonhan;
    }
}