namespace VNS.HIS.UI.DANHMUC
{
    partial class dmuc_doitac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dmuc_doitac));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.chkAutoNew = new Janus.Windows.EditControls.UICheckBox();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdNew = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.pnlView = new System.Windows.Forms.Panel();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.chkConfirm = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkTrangthai = new Janus.Windows.EditControls.UICheckBox();
            this.txtTen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.chkTrangthaiMacdinh = new Janus.Windows.EditControls.UICheckBox();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.txtNGT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtMa = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.labelCommand1 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand2 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand3 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand4 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand5 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand6 = new Janus.Windows.Ribbon.LabelCommand();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSTT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.pnlBottom.SuspendLayout();
            this.pnlView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTOP
            // 
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 0);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Size = new System.Drawing.Size(1008, 0);
            this.pnlTOP.TabIndex = 0;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ImageSize = new System.Drawing.Size(16, 16);
            this.ribbonStatusBar1.LeftPanelCommands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.labelCommand1,
            this.labelCommand2,
            this.labelCommand3,
            this.labelCommand4,
            this.labelCommand5,
            this.labelCommand6});
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 706);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007ColorScheme = Janus.Windows.Ribbon.Office2007ColorScheme.Custom;
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1008, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 1;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.chkAutoNew);
            this.pnlBottom.Controls.Add(this.cmdPrint);
            this.pnlBottom.Controls.Add(this.cmdCancel);
            this.pnlBottom.Controls.Add(this.cmdUpdate);
            this.pnlBottom.Controls.Add(this.cmdNew);
            this.pnlBottom.Controls.Add(this.cmdDelete);
            this.pnlBottom.Controls.Add(this.cmdSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 663);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1008, 43);
            this.pnlBottom.TabIndex = 2;
            // 
            // chkAutoNew
            // 
            this.chkAutoNew.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoNew.Location = new System.Drawing.Point(12, 10);
            this.chkAutoNew.Name = "chkAutoNew";
            this.chkAutoNew.Size = new System.Drawing.Size(214, 23);
            this.chkAutoNew.TabIndex = 12;
            this.chkAutoNew.TabStop = false;
            this.chkAutoNew.Text = "Cho phép thêm mới liên tục?";
            this.chkAutoNew.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(372, 5);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 35);
            this.cmdPrint.TabIndex = 10;
            this.cmdPrint.Text = "Mở danh mục";
            this.cmdPrint.Visible = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(876, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(120, 35);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Thoát";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdUpdate.Location = new System.Drawing.Point(624, 5);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(120, 35);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "Cập nhật";
            // 
            // cmdNew
            // 
            this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdNew.Location = new System.Drawing.Point(498, 5);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(120, 35);
            this.cmdNew.TabIndex = 0;
            this.cmdNew.Text = "Thêm mới";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDelete.Location = new System.Drawing.Point(750, 6);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(120, 35);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "Xóa";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(750, 5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Visible = false;
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.uiTab1);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlView.Location = new System.Drawing.Point(0, 515);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(1008, 148);
            this.pnlView.TabIndex = 3;
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTab1.Location = new System.Drawing.Point(0, 0);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(1008, 148);
            this.uiTab1.TabIndex = 0;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // chkConfirm
            // 
            this.chkConfirm.BackColor = System.Drawing.Color.Transparent;
            this.chkConfirm.Location = new System.Drawing.Point(442, 93);
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.Size = new System.Drawing.Size(240, 23);
            this.chkConfirm.TabIndex = 16;
            this.chkConfirm.TabStop = false;
            this.chkConfirm.Text = "Yêu cầu xác nhận khi đổi tên bằng lưới?";
            this.chkConfirm.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(19, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "ID :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(130, 10);
            this.txtID.MaxLength = 20;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(131, 21);
            this.txtID.TabIndex = 11;
            this.txtID.Text = "(Tự sinh)";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(19, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nguồn giới thiệu :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Location = new System.Drawing.Point(538, 94);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(457, 23);
            this.lblMsg.TabIndex = 9;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.BackColor = System.Drawing.Color.Transparent;
            this.chkTrangthai.Location = new System.Drawing.Point(130, 93);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(126, 23);
            this.chkTrangthai.TabIndex = 5;
            this.chkTrangthai.TabStop = false;
            this.chkTrangthai.Text = "Đang sử dụng?";
            this.chkTrangthai.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // txtTen
            // 
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTen.Location = new System.Drawing.Point(371, 39);
            this.txtTen.MaxLength = 255;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(436, 21);
            this.txtTen.TabIndex = 3;
            // 
            // lblTen
            // 
            this.lblTen.BackColor = System.Drawing.Color.Transparent;
            this.lblTen.ForeColor = System.Drawing.Color.Red;
            this.lblTen.Location = new System.Drawing.Point(267, 42);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(98, 16);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên đối tác:";
            this.lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã  đối tác: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.AutomaticSort = false;
            this.grdList.ContextMenuStrip = this.contextMenuStrip1;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.FrozenColumns = 2;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1008, 515);
            this.grdList.TabIndex = 5;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsert,
            this.mnuUpdate,
            this.mnuDelete,
            this.toolStripMenuItem1,
            this.mnuPrint});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 98);
            // 
            // mnuInsert
            // 
            this.mnuInsert.Name = "mnuInsert";
            this.mnuInsert.Size = new System.Drawing.Size(141, 22);
            this.mnuInsert.Text = "Thêm mới";
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(141, 22);
            this.mnuUpdate.Text = "Cập nhật";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(141, 22);
            this.mnuDelete.Text = "Xóa";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(141, 22);
            this.mnuPrint.Text = "In danh sách";
            // 
            // chkTrangthaiMacdinh
            // 
            this.chkTrangthaiMacdinh.BackColor = System.Drawing.Color.Transparent;
            this.chkTrangthaiMacdinh.Location = new System.Drawing.Point(270, 93);
            this.chkTrangthaiMacdinh.Name = "chkTrangthaiMacdinh";
            this.chkTrangthaiMacdinh.Size = new System.Drawing.Size(166, 23);
            this.chkTrangthaiMacdinh.TabIndex = 18;
            this.chkTrangthaiMacdinh.TabStop = false;
            this.chkTrangthaiMacdinh.Text = "Trạng thái mặc định?";
            this.chkTrangthaiMacdinh.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.label3);
            this.uiTabPage1.Controls.Add(this.txtSTT);
            this.uiTabPage1.Controls.Add(this.chkTrangthaiMacdinh);
            this.uiTabPage1.Controls.Add(this.txtNGT);
            this.uiTabPage1.Controls.Add(this.chkConfirm);
            this.uiTabPage1.Controls.Add(this.txtMa);
            this.uiTabPage1.Controls.Add(this.label2);
            this.uiTabPage1.Controls.Add(this.txtID);
            this.uiTabPage1.Controls.Add(this.label4);
            this.uiTabPage1.Controls.Add(this.lblMsg);
            this.uiTabPage1.Controls.Add(this.chkTrangthai);
            this.uiTabPage1.Controls.Add(this.txtTen);
            this.uiTabPage1.Controls.Add(this.lblTen);
            this.uiTabPage1.Controls.Add(this.label1);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(1006, 124);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Thông tin";
            // 
            // txtNGT
            // 
            this.txtNGT._backcolor = System.Drawing.SystemColors.Control;
            this.txtNGT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNGT._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNGT.AddValues = true;
            this.txtNGT.AllowMultiline = false;
            this.txtNGT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNGT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNGT.AutoCompleteList")));
            this.txtNGT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNGT.buildShortcut = false;
            this.txtNGT.CaseSensitive = false;
            this.txtNGT.CompareNoID = true;
            this.txtNGT.DefaultCode = "-1";
            this.txtNGT.DefaultID = "-1";
            this.txtNGT.Drug_ID = null;
            this.txtNGT.ExtraWidth = 0;
            this.txtNGT.FillValueAfterSelect = false;
            this.txtNGT.LOAI_DANHMUC = "NGUONGTHIEU";
            this.txtNGT.Location = new System.Drawing.Point(130, 68);
            this.txtNGT.MaxHeight = 300;
            this.txtNGT.MinTypedCharacters = 2;
            this.txtNGT.MyCode = "-1";
            this.txtNGT.MyID = "-1";
            this.txtNGT.Name = "txtNGT";
            this.txtNGT.RaiseEvent = false;
            this.txtNGT.RaiseEventEnter = false;
            this.txtNGT.RaiseEventEnterWhenEmpty = false;
            this.txtNGT.SelectedIndex = -1;
            this.txtNGT.ShowCodeWithValue = false;
            this.txtNGT.Size = new System.Drawing.Size(677, 21);
            this.txtNGT.splitChar = '@';
            this.txtNGT.splitCharIDAndCode = '#';
            this.txtNGT.TabIndex = 5;
            this.txtNGT.TakeCode = false;
            this.txtNGT.txtMyCode = null;
            this.txtNGT.txtMyCode_Edit = null;
            this.txtNGT.txtMyID = null;
            this.txtNGT.txtMyID_Edit = null;
            this.txtNGT.txtMyName = null;
            this.txtNGT.txtMyName_Edit = null;
            this.txtNGT.txtNext = null;
            this.txtNGT.txtNext1 = null;
            // 
            // txtMa
            // 
            this.txtMa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtMa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMa.AddValues = true;
            this.txtMa.AllowMultiline = false;
            this.txtMa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtMa.AutoCompleteList")));
            this.txtMa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMa.buildShortcut = false;
            this.txtMa.CaseSensitive = false;
            this.txtMa.CompareNoID = true;
            this.txtMa.DefaultCode = "-1";
            this.txtMa.DefaultID = "-1";
            this.txtMa.Drug_ID = null;
            this.txtMa.ExtraWidth = 0;
            this.txtMa.FillValueAfterSelect = false;
            this.txtMa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa.LOAI_DANHMUC = "DAN_TOC";
            this.txtMa.Location = new System.Drawing.Point(130, 39);
            this.txtMa.MaxHeight = 279;
            this.txtMa.MinTypedCharacters = 2;
            this.txtMa.MyCode = "-1";
            this.txtMa.MyID = "-1";
            this.txtMa.Name = "txtMa";
            this.txtMa.RaiseEvent = false;
            this.txtMa.RaiseEventEnter = false;
            this.txtMa.RaiseEventEnterWhenEmpty = false;
            this.txtMa.SelectedIndex = -1;
            this.txtMa.ShowCodeWithValue = false;
            this.txtMa.Size = new System.Drawing.Size(131, 21);
            this.txtMa.splitChar = '@';
            this.txtMa.splitCharIDAndCode = '#';
            this.txtMa.TabIndex = 2;
            this.txtMa.TakeCode = false;
            this.txtMa.txtMyCode = null;
            this.txtMa.txtMyCode_Edit = null;
            this.txtMa.txtMyID = null;
            this.txtMa.txtMyID_Edit = null;
            this.txtMa.txtMyName = null;
            this.txtMa.txtMyName_Edit = null;
            this.txtMa.txtNext = null;
            this.txtMa.txtNext1 = null;
            // 
            // labelCommand1
            // 
            this.labelCommand1.Key = "labelCommand1";
            this.labelCommand1.Name = "labelCommand1";
            this.labelCommand1.Text = "Thêm mới(Ctrl+N)";
            // 
            // labelCommand2
            // 
            this.labelCommand2.Key = "labelCommand2";
            this.labelCommand2.Name = "labelCommand2";
            this.labelCommand2.Text = "Cập nhật(Ctrl+E)";
            // 
            // labelCommand3
            // 
            this.labelCommand3.Key = "labelCommand3";
            this.labelCommand3.Name = "labelCommand3";
            this.labelCommand3.Text = "Xóa(Ctrl+D)";
            // 
            // labelCommand4
            // 
            this.labelCommand4.Key = "labelCommand4";
            this.labelCommand4.Name = "labelCommand4";
            this.labelCommand4.Text = "Ghi(Ctrl+S)";
            // 
            // labelCommand5
            // 
            this.labelCommand5.Key = "labelCommand5";
            this.labelCommand5.Name = "labelCommand5";
            this.labelCommand5.Text = "In(Ctrl+P)";
            // 
            // labelCommand6
            // 
            this.labelCommand6.Key = "labelCommand6";
            this.labelCommand6.Name = "labelCommand6";
            this.labelCommand6.Text = "Thoát(Esc)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(813, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "STT hiển thị:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Location = new System.Drawing.Point(911, 41);
            this.txtSTT.MaxLength = 10;
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(84, 21);
            this.txtSTT.TabIndex = 4;
            // 
            // dmuc_doitac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.pnlTOP);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "dmuc_doitac";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục...";
            this.pnlBottom.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            this.uiTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTOP;
        private Janus.Windows.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlView;
        public Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdNew;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.Ribbon.LabelCommand labelCommand1;
        private Janus.Windows.Ribbon.LabelCommand labelCommand2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTen;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.Ribbon.LabelCommand labelCommand3;
        private Janus.Windows.Ribbon.LabelCommand labelCommand4;
        private Janus.Windows.Ribbon.LabelCommand labelCommand5;
        private Janus.Windows.Ribbon.LabelCommand labelCommand6;
        private Janus.Windows.EditControls.UICheckBox chkTrangthai;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UICheckBox chkAutoNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuInsert;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
        private UCs.AutoCompleteTextbox_Danhmucchung txtMa;
        private Janus.Windows.EditControls.UICheckBox chkConfirm;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNGT;
        private Janus.Windows.EditControls.UICheckBox chkTrangthaiMacdinh;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtSTT;
    }
}