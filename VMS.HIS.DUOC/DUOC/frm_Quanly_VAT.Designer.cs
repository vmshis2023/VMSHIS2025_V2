namespace VNS.HIS.UI.THUOC
{
    partial class frm_QuanlyThuoc_VAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QuanlyThuoc_VAT));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.ctxThuoc = new System.Windows.Forms.ContextMenuStrip();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHethieuluc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTaogiaquanhe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateGiaDV = new System.Windows.Forms.ToolStripMenuItem();
            this.gridEXPrintDocument1 = new Janus.Windows.GridEX.GridEXPrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.gridEXExporter = new Janus.Windows.GridEX.Export.GridEXExporter();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.optQhe_tatca = new Janus.Windows.EditControls.UIRadioButton();
            this.optVAT = new Janus.Windows.EditControls.UIRadioButton();
            this.optNoVAT = new Janus.Windows.EditControls.UIRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ctxThuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctxThuoc
            // 
            this.ctxThuoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUpdate,
            this.mnuDelete,
            this.mnuHethieuluc,
            this.toolStripMenuItem1,
            this.mnuTaogiaquanhe,
            this.mnuCreateGiaDV});
            this.ctxThuoc.Name = "contextMenuStrip1";
            this.ctxThuoc.Size = new System.Drawing.Size(488, 120);
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(487, 22);
            this.mnuUpdate.Text = "Cập nhập thuốc";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(487, 22);
            this.mnuDelete.Text = "Xóa thuốc";
            this.mnuDelete.Visible = false;
            // 
            // mnuHethieuluc
            // 
            this.mnuHethieuluc.Name = "mnuHethieuluc";
            this.mnuHethieuluc.Size = new System.Drawing.Size(487, 22);
            this.mnuHethieuluc.Text = "Làm hết hiệu lực thuốc";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(484, 6);
            // 
            // mnuTaogiaquanhe
            // 
            this.mnuTaogiaquanhe.Name = "mnuTaogiaquanhe";
            this.mnuTaogiaquanhe.Size = new System.Drawing.Size(487, 22);
            this.mnuTaogiaquanhe.Text = "Tạo giá dịch vụ cho các đối tượng khác dựa trên giá của đối tượng tham chiếu";
            // 
            // mnuCreateGiaDV
            // 
            this.mnuCreateGiaDV.Name = "mnuCreateGiaDV";
            this.mnuCreateGiaDV.Size = new System.Drawing.Size(487, 22);
            this.mnuCreateGiaDV.Text = "Tạo giá cho đối tượng Dịch vụ";
            // 
            // gridEXPrintDocument1
            // 
            this.gridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.gridEXPrintDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(236)))), ((int)(((byte)(252)))));
            this.grdList.ContextMenuStrip = this.ctxThuoc;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 33);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1249, 697);
            this.grdList.TabIndex = 16;
            this.toolTip1.SetToolTip(this.grdList, "Nhấn phím F5 để nạp lại dữ liệu");
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1249, 33);
            this.panel1.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.optQhe_tatca);
            this.panel3.Controls.Add(this.optVAT);
            this.panel3.Controls.Add(this.optNoVAT);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(110, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(411, 25);
            this.panel3.TabIndex = 467;
            // 
            // optQhe_tatca
            // 
            this.optQhe_tatca.Checked = true;
            this.optQhe_tatca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optQhe_tatca.Location = new System.Drawing.Point(12, 2);
            this.optQhe_tatca.Name = "optQhe_tatca";
            this.optQhe_tatca.Size = new System.Drawing.Size(66, 19);
            this.optQhe_tatca.TabIndex = 15;
            this.optQhe_tatca.TabStop = true;
            this.optQhe_tatca.Text = "Tất cả";
            this.optQhe_tatca.CheckedChanged += new System.EventHandler(this.optQhe_tatca_CheckedChanged);
            // 
            // optVAT
            // 
            this.optVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optVAT.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.optVAT.Location = new System.Drawing.Point(116, 2);
            this.optVAT.Name = "optVAT";
            this.optVAT.Size = new System.Drawing.Size(98, 19);
            this.optVAT.TabIndex = 13;
            this.optVAT.Text = "Có VAT";
            this.optVAT.CheckedChanged += new System.EventHandler(this.optVAT_CheckedChanged);
            // 
            // optNoVAT
            // 
            this.optNoVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNoVAT.ForeColor = System.Drawing.Color.Red;
            this.optNoVAT.Location = new System.Drawing.Point(254, 2);
            this.optNoVAT.Name = "optNoVAT";
            this.optNoVAT.Size = new System.Drawing.Size(139, 19);
            this.optNoVAT.TabIndex = 14;
            this.optNoVAT.Text = "Chưa có VAT";
            this.optNoVAT.CheckedChanged += new System.EventHandler(this.optNoVAT_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 468;
            this.label3.Text = "Trạng thái lọc:";
            // 
            // frm_QuanlyThuoc_VAT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1249, 730);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.Name = "frm_QuanlyThuoc_VAT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý VAT của các thuốc Xuất hóa đơn điện tử";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ctxThuoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.GridEX.GridEXPrintDocument gridEXPrintDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter;
        private System.Windows.Forms.ContextMenuStrip ctxThuoc;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHethieuluc;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuTaogiaquanhe;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateGiaDV;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Janus.Windows.EditControls.UIRadioButton optQhe_tatca;
        private Janus.Windows.EditControls.UIRadioButton optVAT;
        private Janus.Windows.EditControls.UIRadioButton optNoVAT;
        private System.Windows.Forms.Label label3;
    }
}