namespace VNS.HIS.UI.THUOC
{
    partial class frm_capnhat_idthuockho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_capnhat_idthuockho));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.ctxUpdate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdUpdateGiaNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateNgayHetHan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdupdatengaynhap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateGiaBan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateSolo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpdateIdThuocKho = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblText = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdXemchoxacnhan = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAnthuoc0 = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.ctxUpdate.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(842, 8);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Cập nhật";
            this.toolTip1.SetToolTip(this.cmdSave, "Cập nhật Id thuốc kho (Ctrl+S)");
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(968, 8);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát Form";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Controls.Add(this.panel4);
            this.uiGroupBox1.Controls.Add(this.panel3);
            this.uiGroupBox1.Controls.Add(this.panel6);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1100, 863);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.AllowColumnDrag = false;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.AutomaticSort = false;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin cập nhập số lượng tồn</FilterRowInfoText></LocalizableData>";
            this.grdList.ContextMenuStrip = this.ctxUpdate;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(6, 40);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1088, 817);
            this.grdList.TabIndex = 6;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // ctxUpdate
            // 
            this.ctxUpdate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUpdateGiaNhap,
            this.cmdUpdateNgayHetHan,
            this.cmdupdatengaynhap,
            this.cmdUpdateGiaBan,
            this.cmdUpdateSolo,
            this.cmdUpdateIdThuocKho});
            this.ctxUpdate.Name = "ctxDelDrug";
            this.ctxUpdate.Size = new System.Drawing.Size(190, 136);
            // 
            // cmdUpdateGiaNhap
            // 
            this.cmdUpdateGiaNhap.Name = "cmdUpdateGiaNhap";
            this.cmdUpdateGiaNhap.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateGiaNhap.Text = "Sửa giá nhập";
            this.cmdUpdateGiaNhap.Visible = false;
            // 
            // cmdUpdateNgayHetHan
            // 
            this.cmdUpdateNgayHetHan.Name = "cmdUpdateNgayHetHan";
            this.cmdUpdateNgayHetHan.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateNgayHetHan.Text = "Sửa ngày hết hạn";
            this.cmdUpdateNgayHetHan.Visible = false;
            // 
            // cmdupdatengaynhap
            // 
            this.cmdupdatengaynhap.Name = "cmdupdatengaynhap";
            this.cmdupdatengaynhap.Size = new System.Drawing.Size(189, 22);
            this.cmdupdatengaynhap.Text = "Sửa ngày nhập";
            this.cmdupdatengaynhap.Visible = false;
            // 
            // cmdUpdateGiaBan
            // 
            this.cmdUpdateGiaBan.Name = "cmdUpdateGiaBan";
            this.cmdUpdateGiaBan.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateGiaBan.Text = "Sửa giá bán ";
            this.cmdUpdateGiaBan.Visible = false;
            // 
            // cmdUpdateSolo
            // 
            this.cmdUpdateSolo.Name = "cmdUpdateSolo";
            this.cmdUpdateSolo.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateSolo.Text = "Sửa số lô";
            this.cmdUpdateSolo.Visible = false;
            // 
            // cmdUpdateIdThuocKho
            // 
            this.cmdUpdateIdThuocKho.Name = "cmdUpdateIdThuocKho";
            this.cmdUpdateIdThuocKho.Size = new System.Drawing.Size(189, 22);
            this.cmdUpdateIdThuocKho.Text = "Sửa toàn bộ thông tin";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(363, 14);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(228, 20);
            this.panel4.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Hết hạn trong vòng 6 tháng nữa";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 20);
            this.label7.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(129, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 20);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hết hạn trong vòng 3 tháng nữa";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightCoral;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 20);
            this.label5.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblText);
            this.panel6.Controls.Add(this.lblColor);
            this.panel6.Location = new System.Drawing.Point(3, 12);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(110, 20);
            this.panel6.TabIndex = 1;
            // 
            // lblText
            // 
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(29, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(81, 20);
            this.lblText.TabIndex = 3;
            this.lblText.Text = "Đã hết hạn";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.Red;
            this.lblColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblColor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.Location = new System.Drawing.Point(0, 0);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(29, 20);
            this.lblColor.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdXemchoxacnhan
            // 
            this.cmdXemchoxacnhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdXemchoxacnhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdXemchoxacnhan.Image = global::VMS.HIS.Duoc.Properties.Resources.LOCKED;
            this.cmdXemchoxacnhan.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdXemchoxacnhan.Location = new System.Drawing.Point(679, 6);
            this.cmdXemchoxacnhan.Name = "cmdXemchoxacnhan";
            this.cmdXemchoxacnhan.Size = new System.Drawing.Size(157, 35);
            this.cmdXemchoxacnhan.TabIndex = 467;
            this.cmdXemchoxacnhan.Text = "Xem chờ xác nhận";
            this.toolTip1.SetToolTip(this.cmdXemchoxacnhan, "Xem chờ xác nhận của thuốc thuộc kho đang chọn");
            this.cmdXemchoxacnhan.Click += new System.EventHandler(this.cmdXemchoxacnhan_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdXemchoxacnhan);
            this.panel1.Controls.Add(this.chkAnthuoc0);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 869);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 52);
            this.panel1.TabIndex = 125;
            // 
            // chkAnthuoc0
            // 
            this.chkAnthuoc0.Checked = true;
            this.chkAnthuoc0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnthuoc0.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnthuoc0.Location = new System.Drawing.Point(6, 17);
            this.chkAnthuoc0.Name = "chkAnthuoc0";
            this.chkAnthuoc0.Size = new System.Drawing.Size(358, 23);
            this.chkAnthuoc0.TabIndex = 466;
            this.chkAnthuoc0.Tag = "THUOC_THONGBAOTHAYDOI_IDTHUOCKHO";
            this.chkAnthuoc0.Text = "Thông báo mỗi khi cập nhật Id_Thuốc_Kho thành công?";
            this.chkAnthuoc0.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // frm_capnhat_idthuockho
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1100, 921);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiGroupBox1);
            this.KeyPreview = true;
            this.Name = "frm_capnhat_idthuockho";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem tồn kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_UpdateSoLuongTon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_UpdateSoLuongTon_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ctxUpdate.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Timer timer1;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxUpdate;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateGiaBan;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateGiaNhap;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateNgayHetHan;
        private System.Windows.Forms.ToolStripMenuItem cmdupdatengaynhap;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateSolo;
        private System.Windows.Forms.ToolStripMenuItem cmdUpdateIdThuocKho;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblColor;
        private Janus.Windows.EditControls.UICheckBox chkAnthuoc0;
        private Janus.Windows.EditControls.UIButton cmdXemchoxacnhan;
    }
}