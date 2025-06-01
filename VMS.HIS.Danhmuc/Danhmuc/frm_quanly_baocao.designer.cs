namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_quanly_baocao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_quanly_baocao));
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.sysColor = new System.Windows.Forms.ToolStrip();
            this.cmdThemMoi = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSua = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdXoa = new System.Windows.Forms.ToolStripButton();
            this.cmdXML = new System.Windows.Forms.ToolStripButton();
            this.cmdDuplicate = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdateXml = new System.Windows.Forms.ToolStripButton();
            this.cmdThoat = new System.Windows.Forms.ToolStripButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.sysColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(105, 17);
            this.toolStripStatusLabel11.Text = "Escape: Thoát, hủy";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel9.Text = "Ctrl+P: In ấn             ";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(102, 17);
            this.toolStripStatusLabel7.Text = "Ctrl+S: Lưu            ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel5.Text = "Del: Xóa           ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(135, 17);
            this.toolStripStatusLabel3.Text = "Ctrl+C: Cập nhập            ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel1.Text = "Ctrl+N: Thêm mới        ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel11});
            this.statusStrip1.Location = new System.Drawing.Point(0, 599);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 22);
            this.statusStrip1.TabIndex = 45;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sysColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdThemMoi,
            this.toolStripSeparator1,
            this.cmdSua,
            this.toolStripSeparator2,
            this.cmdXoa,
            this.cmdXML,
            this.cmdDuplicate,
            this.cmdUpdateXml,
            this.cmdThoat});
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(944, 31);
            this.sysColor.TabIndex = 47;
            this.sysColor.Text = "toolStrip1";
            // 
            // cmdThemMoi
            // 
            this.cmdThemMoi.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdThemMoi.Image = global::VMS.HIS.Danhmuc.Properties.Resources.add_04_32;
            this.cmdThemMoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThemMoi.Name = "cmdThemMoi";
            this.cmdThemMoi.Size = new System.Drawing.Size(90, 28);
            this.cmdThemMoi.Text = "Thêm mới";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // cmdSua
            // 
            this.cmdSua.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(84, 28);
            this.cmdSua.Text = "Cập nhật";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // cmdXoa
            // 
            this.cmdXoa.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(55, 28);
            this.cmdXoa.Text = "Xóa";
            // 
            // cmdXML
            // 
            this.cmdXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXML.Image = ((System.Drawing.Image)(resources.GetObject("cmdXML.Image")));
            this.cmdXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXML.Name = "cmdXML";
            this.cmdXML.Size = new System.Drawing.Size(89, 28);
            this.cmdXML.Text = "Xuất XML";
            this.cmdXML.Click += new System.EventHandler(this.cmdXML_Click);
            // 
            // cmdDuplicate
            // 
            this.cmdDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("cmdDuplicate.Image")));
            this.cmdDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDuplicate.Name = "cmdDuplicate";
            this.cmdDuplicate.Size = new System.Drawing.Size(83, 28);
            this.cmdDuplicate.Text = "Sao chép";
            this.cmdDuplicate.Click += new System.EventHandler(this.cmdDuplicate_Click);
            // 
            // cmdUpdateXml
            // 
            this.cmdUpdateXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdateXml.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateXml.Image")));
            this.cmdUpdateXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdateXml.Name = "cmdUpdateXml";
            this.cmdUpdateXml.Size = new System.Drawing.Size(104, 28);
            this.cmdUpdateXml.Text = "Update XML";
            this.cmdUpdateXml.Click += new System.EventHandler(this.cmdUpdateXml_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(90, 28);
            this.cmdThoat.Text = "Thoát(Esc)";
            // 
            // grdList
            // 
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 31);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(944, 568);
            this.grdList.TabIndex = 74;
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Insurance_Level";
            this.dataGridViewTextBoxColumn7.HeaderText = "Mức bảo hiểm";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "sDesc";
            this.dataGridViewTextBoxColumn6.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "sDesc";
            this.dataGridViewTextBoxColumn5.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Desc";
            this.dataGridViewTextBoxColumn4.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.DataPropertyName = "Desc";
            this.DataGridViewTextBoxColumn3.HeaderText = "Mô tả thêm";
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            this.DataGridViewTextBoxColumn3.ReadOnly = true;
            this.DataGridViewTextBoxColumn3.Width = 200;
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.DataPropertyName = "PaymentMethod_Name";
            this.DataGridViewTextBoxColumn2.HeaderText = "Tên PTTT";
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.ReadOnly = true;
            this.DataGridViewTextBoxColumn2.Visible = false;
            this.DataGridViewTextBoxColumn2.Width = 200;
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.DataPropertyName = "PaymentMethod_ID";
            this.DataGridViewTextBoxColumn1.HeaderText = "Mã PTTT";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            this.DataGridViewTextBoxColumn1.Width = 80;
            // 
            // frm_quanly_baocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 621);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.sysColor);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frm_quanly_baocao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý báo cáo";
            this.Load += new System.EventHandler(this.frm_quanly_baocao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_quanly_baocao_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.sysColor.ResumeLayout(false);
            this.sysColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ToolStrip sysColor;
        private System.Windows.Forms.ToolStripButton cmdThemMoi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdSua;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdXoa;
        private System.Windows.Forms.ToolStripButton cmdThoat;
        private System.Windows.Forms.ToolStripButton cmdXML;
        private System.Windows.Forms.ToolStripButton cmdUpdateXml;
        private System.Windows.Forms.ToolStripButton cmdDuplicate;
    }
}