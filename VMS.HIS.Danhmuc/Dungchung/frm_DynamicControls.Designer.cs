namespace VNS.HIS.UI.HinhAnh
{
    partial class frm_DynamicControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DynamicControls));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkUpdateOnlyZero = new Janus.Windows.EditControls.UICheckBox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.lblName = new System.Windows.Forms.Label();
            this.cboMultiReport = new Janus.Windows.EditControls.UIComboBox();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.cmdSameSize = new Janus.Windows.EditControls.UIButton();
            this.cmdClone = new Janus.Windows.EditControls.UIButton();
            this.flowPnlDynamic = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdRefresh = new Janus.Windows.EditControls.UIButton();
            this.cmdNew = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkUpdateOnlyZero);
            this.panel1.Controls.Add(this.cmdAccept);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.cboMultiReport);
            this.panel1.Controls.Add(this.cmdConfig);
            this.panel1.Controls.Add(this.cmdSameSize);
            this.panel1.Controls.Add(this.cmdClone);
            this.panel1.Controls.Add(this.flowPnlDynamic);
            this.panel1.Controls.Add(this.cmdRefresh);
            this.panel1.Controls.Add(this.cmdNew);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(883, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 985);
            this.panel1.TabIndex = 0;
            // 
            // chkUpdateOnlyZero
            // 
            this.chkUpdateOnlyZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUpdateOnlyZero.Checked = true;
            this.chkUpdateOnlyZero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateOnlyZero.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUpdateOnlyZero.Location = new System.Drawing.Point(9, 915);
            this.chkUpdateOnlyZero.Name = "chkUpdateOnlyZero";
            this.chkUpdateOnlyZero.Size = new System.Drawing.Size(279, 23);
            this.chkUpdateOnlyZero.TabIndex = 632;
            this.chkUpdateOnlyZero.Text = "Chỉ cập nhật các thành phần có giá trị =0?";
            this.toolTip1.SetToolTip(this.chkUpdateOnlyZero, "Nếu chọn mục này thì dữ liệu sẽ được cập nhật ngay sau khi thay đổi giá trị thay " +
        "vì phải nhấn nút Lưu thông tin");
            this.chkUpdateOnlyZero.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdAccept.Location = new System.Drawing.Point(352, 5);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(30, 30);
            this.cmdAccept.TabIndex = 631;
            this.toolTip1.SetToolTip(this.cmdAccept, "Insert");
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(6, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 25);
            this.lblName.TabIndex = 630;
            this.lblName.Text = "Copy từ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMultiReport
            // 
            this.cboMultiReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMultiReport.BackColor = System.Drawing.SystemColors.Menu;
            this.cboMultiReport.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboMultiReport.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboMultiReport.Font = new System.Drawing.Font("Arial", 9F);
            this.cboMultiReport.Location = new System.Drawing.Point(68, 12);
            this.cboMultiReport.Name = "cboMultiReport";
            this.cboMultiReport.Size = new System.Drawing.Size(278, 21);
            this.cboMultiReport.TabIndex = 629;
            this.cboMultiReport.TabStop = false;
            this.cboMultiReport.SelectedIndexChanged += new System.EventHandler(this.cboMultiReport_SelectedIndexChanged);
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(143, 946);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(41, 31);
            this.cmdConfig.TabIndex = 515;
            this.toolTip1.SetToolTip(this.cmdConfig, "Cấu hình");
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click_1);
            // 
            // cmdSameSize
            // 
            this.cmdSameSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSameSize.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSameSize.Image = ((System.Drawing.Image)(resources.GetObject("cmdSameSize.Image")));
            this.cmdSameSize.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSameSize.Location = new System.Drawing.Point(94, 946);
            this.cmdSameSize.Name = "cmdSameSize";
            this.cmdSameSize.Size = new System.Drawing.Size(43, 34);
            this.cmdSameSize.TabIndex = 514;
            this.toolTip1.SetToolTip(this.cmdSameSize, "Cập nhật chiều rộng, chiều cao của các Control khác về kích thước Control đang ch" +
        "ọn");
            this.cmdSameSize.Click += new System.EventHandler(this.cmdSameSize_Click);
            // 
            // cmdClone
            // 
            this.cmdClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClone.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClone.Image = ((System.Drawing.Image)(resources.GetObject("cmdClone.Image")));
            this.cmdClone.ImageSize = new System.Drawing.Size(48, 48);
            this.cmdClone.Location = new System.Drawing.Point(48, 946);
            this.cmdClone.Name = "cmdClone";
            this.cmdClone.Size = new System.Drawing.Size(43, 34);
            this.cmdClone.TabIndex = 513;
            this.toolTip1.SetToolTip(this.cmdClone, "Clone current row");
            this.cmdClone.Click += new System.EventHandler(this.cmdSelectList_Click);
            // 
            // flowPnlDynamic
            // 
            this.flowPnlDynamic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPnlDynamic.AutoScroll = true;
            this.flowPnlDynamic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPnlDynamic.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPnlDynamic.Location = new System.Drawing.Point(8, 39);
            this.flowPnlDynamic.Name = "flowPnlDynamic";
            this.flowPnlDynamic.Size = new System.Drawing.Size(374, 870);
            this.flowPnlDynamic.TabIndex = 512;
            this.flowPnlDynamic.TabStop = true;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
            this.cmdRefresh.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdRefresh.Location = new System.Drawing.Point(3, 946);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(43, 34);
            this.cmdRefresh.TabIndex = 505;
            this.cmdRefresh.TabStop = false;
            this.toolTip1.SetToolTip(this.cmdRefresh, "Refresh");
            // 
            // cmdNew
            // 
            this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNew.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Add32;
            this.cmdNew.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdNew.Location = new System.Drawing.Point(227, 948);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(41, 31);
            this.cmdNew.TabIndex = 10;
            this.toolTip1.SetToolTip(this.cmdNew, "Insert");
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(274, 948);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(105, 31);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Text = "Save (Ctrl+S)";
            this.toolTip1.SetToolTip(this.cmdSave, "Update");
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // grdList
            // 
            this.grdList.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.AutoEdit = true;
            this.grdList.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.False;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.False;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(883, 985);
            this.grdList.TabIndex = 0;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdList.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdList_FormattingRow);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_DynamicControls
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1268, 985);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frm_DynamicControls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khai báo trường tìm kiếm cho các báo cáo nhanh";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdNew;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel flowPnlDynamic;
        private Janus.Windows.EditControls.UIButton cmdClone;
        private Janus.Windows.EditControls.UIButton cmdSameSize;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        public Janus.Windows.EditControls.UIComboBox cboMultiReport;
        public System.Windows.Forms.Label lblName;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UICheckBox chkUpdateOnlyZero;
    }
}