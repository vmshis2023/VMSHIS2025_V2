
namespace VMS.HIS.Ngoaitru
{
    partial class frmSHS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSHS));
            Janus.Windows.GridEX.GridEXLayout grdListHide_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdListShow_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdShow = new Janus.Windows.EditControls.UIButton();
            this.cmdHide = new Janus.Windows.EditControls.UIButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdListHide = new Janus.Windows.GridEX.GridEX();
            this.grdListShow = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboDichvuCLS = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cmdRefresh = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListShow)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdShow);
            this.panel1.Controls.Add(this.cmdHide);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 716);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1384, 45);
            this.panel1.TabIndex = 0;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Ngoaitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1252, 4);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 37);
            this.cmdExit.TabIndex = 556;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdShow
            // 
            this.cmdShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdShow.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdShow.Image = ((System.Drawing.Image)(resources.GetObject("cmdShow.Image")));
            this.cmdShow.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdShow.Location = new System.Drawing.Point(1117, 4);
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.Size = new System.Drawing.Size(129, 37);
            this.cmdShow.TabIndex = 555;
            this.cmdShow.Text = "&Show";
            this.toolTip1.SetToolTip(this.cmdShow, "Hiển thị các người bệnh đang chọn Ctrl+S hoặc Ctrl+V");
            this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
            // 
            // cmdHide
            // 
            this.cmdHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHide.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdHide.Image = ((System.Drawing.Image)(resources.GetObject("cmdHide.Image")));
            this.cmdHide.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHide.Location = new System.Drawing.Point(3, 4);
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.Size = new System.Drawing.Size(129, 37);
            this.cmdHide.TabIndex = 554;
            this.cmdHide.Text = "&Hide";
            this.toolTip1.SetToolTip(this.cmdHide, "Ẩn các người bệnh đang chọn Ctrl+H hoặc Ctrl+I");
            this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdListHide);
            this.panel2.Controls.Add(this.grdListShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1384, 674);
            this.panel2.TabIndex = 1;
            // 
            // grdListHide
            // 
            this.grdListHide.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdListHide.AlternatingColors = true;
            this.grdListHide.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdListHide.BackColor = System.Drawing.Color.Silver;
            this.grdListHide.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdListHide_DesignTimeLayout.LayoutString = resources.GetString("grdListHide_DesignTimeLayout.LayoutString");
            this.grdListHide.DesignTimeLayout = grdListHide_DesignTimeLayout;
            this.grdListHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListHide.DynamicFiltering = true;
            this.grdListHide.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdListHide.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdListHide.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdListHide.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdListHide.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdListHide.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdListHide.Font = new System.Drawing.Font("Arial", 9F);
            this.grdListHide.FrozenColumns = -1;
            this.grdListHide.GroupByBoxVisible = false;
            this.grdListHide.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListHide.Location = new System.Drawing.Point(717, 0);
            this.grdListHide.Name = "grdListHide";
            this.grdListHide.RecordNavigator = true;
            this.grdListHide.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListHide.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdListHide.Size = new System.Drawing.Size(667, 674);
            this.grdListHide.TabIndex = 2;
            this.grdListHide.TabStop = false;
            // 
            // grdListShow
            // 
            this.grdListShow.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdListShow.AlternatingColors = true;
            this.grdListShow.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdListShow.BackColor = System.Drawing.Color.Silver;
            this.grdListShow.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdListShow_DesignTimeLayout.LayoutString = resources.GetString("grdListShow_DesignTimeLayout.LayoutString");
            this.grdListShow.DesignTimeLayout = grdListShow_DesignTimeLayout;
            this.grdListShow.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdListShow.DynamicFiltering = true;
            this.grdListShow.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdListShow.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdListShow.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdListShow.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdListShow.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdListShow.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdListShow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdListShow.FrozenColumns = -1;
            this.grdListShow.GroupByBoxVisible = false;
            this.grdListShow.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListShow.Location = new System.Drawing.Point(0, 0);
            this.grdListShow.Name = "grdListShow";
            this.grdListShow.RecordNavigator = true;
            this.grdListShow.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListShow.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdListShow.Size = new System.Drawing.Size(717, 674);
            this.grdListShow.TabIndex = 1;
            this.grdListShow.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdRefresh);
            this.panel3.Controls.Add(this.label37);
            this.panel3.Controls.Add(this.cboDichvuCLS);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1384, 42);
            this.panel3.TabIndex = 557;
            // 
            // cboDichvuCLS
            // 
            this.cboDichvuCLS.FormattingEnabled = true;
            this.cboDichvuCLS.Location = new System.Drawing.Point(147, 12);
            this.cboDichvuCLS.Name = "cboDichvuCLS";
            this.cboDichvuCLS.Next_Control = null;
            this.cboDichvuCLS.RaiseEnterEventWhenInvisible = true;
            this.cboDichvuCLS.Size = new System.Drawing.Size(570, 21);
            this.cboDichvuCLS.TabIndex = 28;
            this.cboDichvuCLS.SelectedIndexChanged += new System.EventHandler(this.cboDichvuCLS_SelectedIndexChanged);
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(12, 11);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(120, 21);
            this.label37.TabIndex = 635;
            this.label37.Text = "Đã chỉ định dịch vụ:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdRefresh.Image = global::VMS.HIS.Ngoaitru.Properties.Resources.refresh03_24;
            this.cmdRefresh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdRefresh.Location = new System.Drawing.Point(723, 7);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(30, 30);
            this.cmdRefresh.TabIndex = 636;
            this.toolTip1.SetToolTip(this.cmdRefresh, "Hiển thị các người bệnh đang chọn Ctrl+S hoặc Ctrl+V");
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // frmSHS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmSHS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý ẩn hiện người bệnh";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSHS_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListShow)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdListHide;
        private Janus.Windows.GridEX.GridEX grdListShow;
        private Janus.Windows.EditControls.UIButton cmdShow;
        private Janus.Windows.EditControls.UIButton cmdHide;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel3;
        private VNS.HIS.UCs.EasyCompletionComboBox cboDichvuCLS;
        private System.Windows.Forms.Label label37;
        private Janus.Windows.EditControls.UIButton cmdRefresh;
    }
}