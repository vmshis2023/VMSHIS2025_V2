namespace VNS.HIS.UI.Forms.CanLamSang
{
    partial class frm_xem_ketqua_xn
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
            Janus.Windows.GridEX.GridEXLayout grdChidinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_xem_ketqua_xn));
            Janus.Windows.GridEX.GridEXLayout grdKetqua_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.ucThongtinnguoibenh_v31 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v3();
            this.pnlAct = new System.Windows.Forms.Panel();
            this.pnlChidinh = new System.Windows.Forms.Panel();
            this.grdChidinh = new Janus.Windows.GridEX.GridEX();
            this.pnlKQ = new System.Windows.Forms.Panel();
            this.grdKetqua = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxDelCLS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCancelResult = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.pnlAct.SuspendLayout();
            this.pnlChidinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).BeginInit();
            this.pnlKQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).BeginInit();
            this.ctxDelCLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucThongtinnguoibenh_v31
            // 
            this.ucThongtinnguoibenh_v31.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_v31.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_v31.Name = "ucThongtinnguoibenh_v31";
            this.ucThongtinnguoibenh_v31.Size = new System.Drawing.Size(1008, 200);
            this.ucThongtinnguoibenh_v31.TabIndex = 478;
            // 
            // pnlAct
            // 
            this.pnlAct.Controls.Add(this.cmdExit);
            this.pnlAct.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAct.Location = new System.Drawing.Point(0, 681);
            this.pnlAct.Name = "pnlAct";
            this.pnlAct.Size = new System.Drawing.Size(1008, 49);
            this.pnlAct.TabIndex = 2;
            // 
            // pnlChidinh
            // 
            this.pnlChidinh.Controls.Add(this.grdChidinh);
            this.pnlChidinh.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlChidinh.Location = new System.Drawing.Point(0, 200);
            this.pnlChidinh.Name = "pnlChidinh";
            this.pnlChidinh.Size = new System.Drawing.Size(379, 481);
            this.pnlChidinh.TabIndex = 3;
            // 
            // grdChidinh
            // 
            this.grdChidinh.AlternatingColors = true;
            grdChidinh_DesignTimeLayout.LayoutString = resources.GetString("grdChidinh_DesignTimeLayout.LayoutString");
            this.grdChidinh.DesignTimeLayout = grdChidinh_DesignTimeLayout;
            this.grdChidinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChidinh.DynamicFiltering = true;
            this.grdChidinh.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdChidinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdChidinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChidinh.Font = new System.Drawing.Font("Arial", 9F);
            this.grdChidinh.GroupByBoxVisible = false;
            this.grdChidinh.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdChidinh.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdChidinh.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdChidinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChidinh.Location = new System.Drawing.Point(0, 0);
            this.grdChidinh.Name = "grdChidinh";
            this.grdChidinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChidinh.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdChidinh.Size = new System.Drawing.Size(379, 481);
            this.grdChidinh.TabIndex = 254;
            this.grdChidinh.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChidinh.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdChidinh.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdChidinh.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdChidinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdChidinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlKQ
            // 
            this.pnlKQ.Controls.Add(this.grdKetqua);
            this.pnlKQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKQ.Location = new System.Drawing.Point(379, 200);
            this.pnlKQ.Name = "pnlKQ";
            this.pnlKQ.Size = new System.Drawing.Size(629, 481);
            this.pnlKQ.TabIndex = 4;
            // 
            // grdKetqua
            // 
            this.grdKetqua.AlternatingColors = true;
            this.grdKetqua.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdKetqua_DesignTimeLayout.LayoutString = resources.GetString("grdKetqua_DesignTimeLayout.LayoutString");
            this.grdKetqua.DesignTimeLayout = grdKetqua_DesignTimeLayout;
            this.grdKetqua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetqua.DynamicFiltering = true;
            this.grdKetqua.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKetqua.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKetqua.Font = new System.Drawing.Font("Arial", 9F);
            this.grdKetqua.GroupByBoxVisible = false;
            this.grdKetqua.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdKetqua.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdKetqua.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdKetqua.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKetqua.Location = new System.Drawing.Point(0, 0);
            this.grdKetqua.Name = "grdKetqua";
            this.grdKetqua.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdKetqua.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.grdKetqua.Size = new System.Drawing.Size(629, 481);
            this.grdKetqua.TabIndex = 255;
            this.grdKetqua.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdKetqua.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKetqua.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdKetqua.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdKetqua.UseGroupRowSelector = true;
            this.grdKetqua.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp:";
            // 
            // ctxDelCLS
            // 
            this.ctxDelCLS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCancelResult,
            this.toolStripMenuItem1});
            this.ctxDelCLS.Name = "ctxDelCLS";
            this.ctxDelCLS.Size = new System.Drawing.Size(222, 32);
            // 
            // mnuCancelResult
            // 
            this.mnuCancelResult.CheckOnClick = true;
            this.mnuCancelResult.Name = "mnuCancelResult";
            this.mnuCancelResult.Size = new System.Drawing.Size(221, 22);
            this.mnuCancelResult.Tag = "0";
            this.mnuCancelResult.Text = "Hủy kết quả CLS đang chọn";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 6);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(876, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 19;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frm_xem_ketqua_xn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.pnlKQ);
            this.Controls.Add(this.pnlChidinh);
            this.Controls.Add(this.pnlAct);
            this.Controls.Add(this.ucThongtinnguoibenh_v31);
            this.KeyPreview = true;
            this.Name = "frm_xem_ketqua_xn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem kết quả xét nghiệm";
            this.pnlAct.ResumeLayout(false);
            this.pnlChidinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChidinh)).EndInit();
            this.pnlKQ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).EndInit();
            this.ctxDelCLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAct;
        private System.Windows.Forms.Panel pnlChidinh;
        private System.Windows.Forms.Panel pnlKQ;
        private Janus.Windows.GridEX.GridEX grdChidinh;
        private Janus.Windows.GridEX.GridEX grdKetqua;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxDelCLS;
        private System.Windows.Forms.ToolStripMenuItem mnuCancelResult;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private Dungchung.UCs.ucThongtinnguoibenh_v3 ucThongtinnguoibenh_v31;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}