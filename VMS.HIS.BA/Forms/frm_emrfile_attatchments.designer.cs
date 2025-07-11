using VNS.HIS.UCs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_emrfile_attatchments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_emrfile_attatchments));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.ctxFunction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdCapnhatMaNhomBC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCapnhatMaphieuEMR = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cboGayEMR = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Tientrinh = new System.Windows.Forms.ProgressBar();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdthoat = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.ctxFunction.SuspendLayout();
            this.pnlInfor.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // grdList
            // 
            this.grdList.ContextMenuStrip = this.ctxFunction;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 46);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1029, 587);
            this.grdList.TabIndex = 74;
            this.grdList.UseGroupRowSelector = true;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // ctxFunction
            // 
            this.ctxFunction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCapnhatMaNhomBC,
            this.mnuCapnhatMaphieuEMR});
            this.ctxFunction.Name = "ctxBOD";
            this.ctxFunction.Size = new System.Drawing.Size(203, 48);
            // 
            // cmdCapnhatMaNhomBC
            // 
            this.cmdCapnhatMaNhomBC.Name = "cmdCapnhatMaNhomBC";
            this.cmdCapnhatMaNhomBC.Size = new System.Drawing.Size(202, 22);
            this.cmdCapnhatMaNhomBC.Text = "Cập nhật mã nhóm BC";
            // 
            // mnuCapnhatMaphieuEMR
            // 
            this.mnuCapnhatMaphieuEMR.Name = "mnuCapnhatMaphieuEMR";
            this.mnuCapnhatMaphieuEMR.Size = new System.Drawing.Size(202, 22);
            this.mnuCapnhatMaphieuEMR.Text = "Cập nhật mã phiếu EMR";
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
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.cmdUpdate);
            this.pnlInfor.Controls.Add(this.label8);
            this.pnlInfor.Controls.Add(this.cboGayEMR);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfor.Location = new System.Drawing.Point(0, 0);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(1029, 46);
            this.pnlInfor.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 31;
            this.label8.Text = "Chọn gáy:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGayEMR
            // 
            this.cboGayEMR.FormattingEnabled = true;
            this.cboGayEMR.Location = new System.Drawing.Point(122, 12);
            this.cboGayEMR.Name = "cboGayEMR";
            this.cboGayEMR.Next_Control = null;
            this.cboGayEMR.RaiseEnterEventWhenInvisible = true;
            this.cboGayEMR.Size = new System.Drawing.Size(557, 21);
            this.cboGayEMR.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Tientrinh);
            this.panel1.Controls.Add(this.cmdAccept);
            this.panel1.Controls.Add(this.cmdthoat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 633);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 51);
            this.panel1.TabIndex = 76;
            // 
            // Tientrinh
            // 
            this.Tientrinh.Location = new System.Drawing.Point(12, 18);
            this.Tientrinh.Name = "Tientrinh";
            this.Tientrinh.Size = new System.Drawing.Size(721, 23);
            this.Tientrinh.TabIndex = 25;
            this.Tientrinh.Visible = false;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(749, 8);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(131, 33);
            this.cmdAccept.TabIndex = 23;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdthoat
            // 
            this.cmdthoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdthoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdthoat.Image")));
            this.cmdthoat.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdthoat.Location = new System.Drawing.Point(886, 8);
            this.cmdthoat.Name = "cmdthoat";
            this.cmdthoat.Size = new System.Drawing.Size(131, 33);
            this.cmdthoat.TabIndex = 24;
            this.cmdthoat.Text = "Thoát";
            this.cmdthoat.Click += new System.EventHandler(this.cmdthoat_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdUpdate.Location = new System.Drawing.Point(685, 8);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(35, 28);
            this.cmdUpdate.TabIndex = 637;
            this.cmdUpdate.Visible = false;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // frm_emrfile_attatchments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 684);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlInfor);
            this.Name = "frm_emrfile_attatchments";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hồ sơ đính kèm khác";
            this.Load += new System.EventHandler(this.frm_emrfile_attatchments_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_emrfile_attatchments_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ctxFunction.ResumeLayout(false);
            this.pnlInfor.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ContextMenuStrip ctxFunction;
        private System.Windows.Forms.ToolStripMenuItem cmdCapnhatMaNhomBC;
        private System.Windows.Forms.ToolStripMenuItem mnuCapnhatMaphieuEMR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlInfor;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdthoat;
        private System.Windows.Forms.Label label8;
        public EasyCompletionComboBox cboGayEMR;
        private System.Windows.Forms.ProgressBar Tientrinh;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
    }
}