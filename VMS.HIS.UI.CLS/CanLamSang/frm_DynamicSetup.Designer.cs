namespace VNS.HIS.UI.HinhAnh
{
    partial class frm_DynamicSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DynamicSetup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSelectList = new Janus.Windows.EditControls.UIButton();
            this.pnlDynamicValues = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSameSize = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSameSize);
            this.panel1.Controls.Add(this.cmdSelectList);
            this.panel1.Controls.Add(this.pnlDynamicValues);
            this.panel1.Controls.Add(this.cmdConfig);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 266);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 411);
            this.panel1.TabIndex = 0;
            // 
            // cmdSelectList
            // 
            this.cmdSelectList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSelectList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelectList.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectList.Image")));
            this.cmdSelectList.ImageSize = new System.Drawing.Size(48, 48);
            this.cmdSelectList.Location = new System.Drawing.Point(52, 372);
            this.cmdSelectList.Name = "cmdSelectList";
            this.cmdSelectList.Size = new System.Drawing.Size(43, 34);
            this.cmdSelectList.TabIndex = 513;
            this.toolTip1.SetToolTip(this.cmdSelectList, "Copy mục đang chọn");
            this.cmdSelectList.Click += new System.EventHandler(this.cmdSelectList_Click);
            // 
            // pnlDynamicValues
            // 
            this.pnlDynamicValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDynamicValues.AutoScroll = true;
            this.pnlDynamicValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDynamicValues.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlDynamicValues.Location = new System.Drawing.Point(8, 8);
            this.pnlDynamicValues.Name = "pnlDynamicValues";
            this.pnlDynamicValues.Size = new System.Drawing.Size(997, 362);
            this.pnlDynamicValues.TabIndex = 512;
            this.pnlDynamicValues.TabStop = true;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(3, 372);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(43, 34);
            this.cmdConfig.TabIndex = 505;
            this.cmdConfig.TabStop = false;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(862, 374);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(134, 31);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Hủy / Thoát(Esc)";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(711, 374);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(132, 31);
            this.cmdSave.TabIndex = 9;
            this.cmdSave.Text = "Cập nhập(Ctrl+S)";
            this.toolTip1.SetToolTip(this.cmdSave, "Cập nhật lại tất cả dữ liệu");
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
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.NextCell;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1008, 266);
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
            // cmdSameSize
            // 
            this.cmdSameSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSameSize.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSameSize.Image = ((System.Drawing.Image)(resources.GetObject("cmdSameSize.Image")));
            this.cmdSameSize.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSameSize.Location = new System.Drawing.Point(101, 372);
            this.cmdSameSize.Name = "cmdSameSize";
            this.cmdSameSize.Size = new System.Drawing.Size(43, 34);
            this.cmdSameSize.TabIndex = 514;
            this.toolTip1.SetToolTip(this.cmdSameSize, "Tự động cập nhật kích thước các phần có kích thước =0 theo mục đang chọn");
            this.cmdSameSize.Click += new System.EventHandler(this.cmdSameSize_Click);
            // 
            // frm_DynamicSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 677);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DynamicSetup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khai báo các mã - giá trị nhập kết quả CĐHA";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel pnlDynamicValues;
        private Janus.Windows.EditControls.UIButton cmdSelectList;
        private Janus.Windows.EditControls.UIButton cmdSameSize;
    }
}