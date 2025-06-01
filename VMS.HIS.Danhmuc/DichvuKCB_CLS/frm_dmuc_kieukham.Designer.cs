namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_dmuc_kieukham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dmuc_kieukham));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.labelCommand1 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand2 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand3 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand4 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand5 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand6 = new Janus.Windows.Ribbon.LabelCommand();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdNew = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.chkAutoNew = new Janus.Windows.EditControls.UICheckBox();
            this.pnlView = new System.Windows.Forms.Panel();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.nmrDongia = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbodoituongkcb = new System.Windows.Forms.ComboBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbonhombaocao = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkTrangthai = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSTT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBottom.SuspendLayout();
            this.pnlView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTOP
            // 
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 0);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Size = new System.Drawing.Size(784, 0);
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
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 539);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007ColorScheme = Janus.Windows.Ribbon.Office2007ColorScheme.Custom;
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(784, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 1;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
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
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.cmdPrint);
            this.pnlBottom.Controls.Add(this.cmdCancel);
            this.pnlBottom.Controls.Add(this.cmdUpdate);
            this.pnlBottom.Controls.Add(this.cmdNew);
            this.pnlBottom.Controls.Add(this.cmdDelete);
            this.pnlBottom.Controls.Add(this.cmdSave);
            this.pnlBottom.Controls.Add(this.chkAutoNew);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 496);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(784, 43);
            this.pnlBottom.TabIndex = 2;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(221, 8);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(89, 25);
            this.cmdPrint.TabIndex = 10;
            this.cmdPrint.Text = "In danh sách";
            this.cmdPrint.Visible = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.Location = new System.Drawing.Point(677, 8);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 25);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Thoát";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(487, 8);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(89, 25);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "Cập nhật";
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(392, 8);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(89, 25);
            this.cmdNew.TabIndex = 0;
            this.cmdNew.Text = "Thêm mới";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(582, 8);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(89, 25);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "Xóa";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(582, 8);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(89, 25);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Visible = false;
            // 
            // chkAutoNew
            // 
            this.chkAutoNew.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoNew.Location = new System.Drawing.Point(6, 12);
            this.chkAutoNew.Name = "chkAutoNew";
            this.chkAutoNew.Size = new System.Drawing.Size(186, 23);
            this.chkAutoNew.TabIndex = 12;
            this.chkAutoNew.Text = "Cho phép thêm mới liên tục?";
            this.chkAutoNew.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.uiTab1);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlView.Location = new System.Drawing.Point(0, 348);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(784, 148);
            this.pnlView.TabIndex = 3;
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTab1.Location = new System.Drawing.Point(0, 0);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(784, 148);
            this.uiTab1.TabIndex = 0;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.nmrDongia);
            this.uiTabPage1.Controls.Add(this.label6);
            this.uiTabPage1.Controls.Add(this.label3);
            this.uiTabPage1.Controls.Add(this.cbodoituongkcb);
            this.uiTabPage1.Controls.Add(this.lblMsg);
            this.uiTabPage1.Controls.Add(this.label4);
            this.uiTabPage1.Controls.Add(this.cbonhombaocao);
            this.uiTabPage1.Controls.Add(this.label5);
            this.uiTabPage1.Controls.Add(this.txtID);
            this.uiTabPage1.Controls.Add(this.chkTrangthai);
            this.uiTabPage1.Controls.Add(this.label2);
            this.uiTabPage1.Controls.Add(this.txtSTT);
            this.uiTabPage1.Controls.Add(this.txtTen);
            this.uiTabPage1.Controls.Add(this.lblTen);
            this.uiTabPage1.Controls.Add(this.label1);
            this.uiTabPage1.Controls.Add(this.txtMa);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(782, 124);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Thông tin";
            // 
            // nmrDongia
            // 
            this.nmrDongia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmrDongia.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrDongia.Location = new System.Drawing.Point(585, 39);
            this.nmrDongia.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nmrDongia.Name = "nmrDongia";
            this.nmrDongia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nmrDongia.Size = new System.Drawing.Size(170, 22);
            this.nmrDongia.TabIndex = 110;
            this.nmrDongia.ThousandsSeparator = true;
            this.nmrDongia.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(452, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 23);
            this.label6.TabIndex = 111;
            this.label6.Text = "Đơn giá:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(452, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 23);
            this.label3.TabIndex = 102;
            this.label3.Text = "Đối tượng áp dụng:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbodoituongkcb
            // 
            this.cbodoituongkcb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbodoituongkcb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbodoituongkcb.FormattingEnabled = true;
            this.cbodoituongkcb.Location = new System.Drawing.Point(585, 65);
            this.cbodoituongkcb.Name = "cbodoituongkcb";
            this.cbodoituongkcb.Size = new System.Drawing.Size(170, 24);
            this.cbodoituongkcb.TabIndex = 101;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Location = new System.Drawing.Point(400, 95);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(380, 23);
            this.lblMsg.TabIndex = 9;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 23);
            this.label4.TabIndex = 100;
            this.label4.Text = "Nhóm báo cáo";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbonhombaocao
            // 
            this.cbonhombaocao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbonhombaocao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbonhombaocao.FormattingEnabled = true;
            this.cbonhombaocao.Location = new System.Drawing.Point(112, 65);
            this.cbonhombaocao.Name = "cbonhombaocao";
            this.cbonhombaocao.Size = new System.Drawing.Size(334, 24);
            this.cbonhombaocao.TabIndex = 99;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(1, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 23);
            this.label5.TabIndex = 14;
            this.label5.Text = "ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(112, 14);
            this.txtID.MaxLength = 20;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(44, 21);
            this.txtID.TabIndex = 13;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.BackColor = System.Drawing.Color.Transparent;
            this.chkTrangthai.Location = new System.Drawing.Point(112, 95);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(126, 23);
            this.chkTrangthai.TabIndex = 6;
            this.chkTrangthai.Text = "Đang sử dụng?";
            this.chkTrangthai.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(452, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "STT hiển thị:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(585, 14);
            this.txtSTT.MaxLength = 10;
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(170, 21);
            this.txtSTT.TabIndex = 4;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(112, 39);
            this.txtTen.MaxLength = 255;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(334, 21);
            this.txtTen.TabIndex = 3;
            // 
            // lblTen
            // 
            this.lblTen.BackColor = System.Drawing.Color.Transparent;
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTen.Location = new System.Drawing.Point(1, 39);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(109, 16);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên:";
            this.lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(162, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(198, 14);
            this.txtMa.MaxLength = 20;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(248, 21);
            this.txtMa.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.ContextMenuStrip = this.contextMenuStrip1;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.FrozenColumns = 2;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(784, 348);
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
            this.mnuPrint,
            this.mnuRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 120);
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
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Size = new System.Drawing.Size(141, 22);
            this.mnuRefresh.Text = "Refresh (F5)";
            // 
            // frm_dmuc_kieukham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.pnlTOP);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_dmuc_kieukham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục...";
            this.pnlBottom.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            this.uiTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private Janus.Windows.GridEX.EditControls.EditBox txtMa;
        private Janus.Windows.Ribbon.LabelCommand labelCommand3;
        private Janus.Windows.Ribbon.LabelCommand labelCommand4;
        private Janus.Windows.Ribbon.LabelCommand labelCommand5;
        private Janus.Windows.Ribbon.LabelCommand labelCommand6;
        private Janus.Windows.EditControls.UICheckBox chkTrangthai;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtSTT;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UICheckBox chkAutoNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuInsert;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbonhombaocao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbodoituongkcb;
        private System.Windows.Forms.NumericUpDown nmrDongia;
        private System.Windows.Forms.Label label6;
    }
}