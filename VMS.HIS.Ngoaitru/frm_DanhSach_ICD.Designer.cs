namespace  VNS.HIS.UI.NGOAITRU
{
    partial class frm_DanhSach_ICD
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
            Janus.Windows.GridEX.GridEXLayout grd_List_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DanhSach_ICD));
            this.grd_List = new Janus.Windows.GridEX.GridEX();
            this.cmdSavePres = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.lnkAll = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_List
            // 
            this.grd_List.AlternatingColors = true;
            this.grd_List.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grd_List.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin mã bệnh ICD</FilterRowInfoText></LocalizableData>";
            this.grd_List.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grd_List_DesignTimeLayout.LayoutString = resources.GetString("grd_List_DesignTimeLayout.LayoutString");
            this.grd_List.DesignTimeLayout = grd_List_DesignTimeLayout;
            this.grd_List.Dock = System.Windows.Forms.DockStyle.Top;
            this.grd_List.DynamicFiltering = true;
            this.grd_List.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grd_List.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grd_List.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grd_List.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grd_List.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_List.FrozenColumns = 1;
            this.grd_List.GroupByBoxVisible = false;
            this.grd_List.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grd_List.Location = new System.Drawing.Point(0, 0);
            this.grd_List.Name = "grd_List";
            this.grd_List.RecordNavigator = true;
            this.grd_List.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_List.Size = new System.Drawing.Size(784, 508);
            this.grd_List.TabIndex = 0;
            this.grd_List.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // cmdSavePres
            // 
            this.cmdSavePres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSavePres.Image = ((System.Drawing.Image)(resources.GetObject("cmdSavePres.Image")));
            this.cmdSavePres.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSavePres.Location = new System.Drawing.Point(502, 514);
            this.cmdSavePres.Name = "cmdSavePres";
            this.cmdSavePres.Size = new System.Drawing.Size(148, 35);
            this.cmdSavePres.TabIndex = 9;
            this.cmdSavePres.Text = "Chấp nhận(Ctrl+A)";
            this.cmdSavePres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(656, 514);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // lnkAll
            // 
            this.lnkAll.AutoSize = true;
            this.lnkAll.Location = new System.Drawing.Point(12, 529);
            this.lnkAll.Name = "lnkAll";
            this.lnkAll.Size = new System.Drawing.Size(214, 15);
            this.lnkAll.TabIndex = 11;
            this.lnkAll.TabStop = true;
            this.lnkAll.Text = "Nhấn vào đây để hiển thị toàn bộ ICDs";
            this.lnkAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAll_LinkClicked);
            // 
            // frm_DanhSach_ICD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lnkAll);
            this.Controls.Add(this.cmdSavePres);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grd_List);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DanhSach_ICD";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh mục ICD Bệnh(Nhấn đúp chuột hoặc Ctrl+A hoặc bấm nút chấp nhận để chọn ICDs " +
    "cần lấy)";
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grd_List;
        internal Janus.Windows.EditControls.UIButton cmdSavePres;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.LinkLabel lnkAll;
    }
}