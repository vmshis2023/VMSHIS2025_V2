using VNS.HIS.UCs;
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_phieucongkhai
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
            Janus.Windows.GridEX.GridEXLayout grdPhieudieutri_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_phieucongkhai));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.chkGiuong = new System.Windows.Forms.CheckBox();
            this.grdPhieudieutri = new Janus.Windows.GridEX.GridEX();
            this.chkVTTH = new System.Windows.Forms.CheckBox();
            this.chkThuoc = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCLS = new System.Windows.Forms.CheckBox();
            this.ucThongtinnguoibenh_v21 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_v2();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkAutoFillEmpty = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieudieutri)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.chkGiuong);
            this.panel1.Controls.Add(this.grdPhieudieutri);
            this.panel1.Controls.Add(this.chkVTTH);
            this.panel1.Controls.Add(this.chkThuoc);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkCLS);
            this.panel1.Controls.Add(this.ucThongtinnguoibenh_v21);
            this.panel1.Controls.Add(this.dtDenNgay);
            this.panel1.Controls.Add(this.dtTuNgay);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 672);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(6, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 67);
            this.label2.TabIndex = 29;
            this.label2.Text = "Danh sách các phiếu điều trị :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkGiuong
            // 
            this.chkGiuong.AutoSize = true;
            this.chkGiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGiuong.Location = new System.Drawing.Point(616, 175);
            this.chkGiuong.Name = "chkGiuong";
            this.chkGiuong.Size = new System.Drawing.Size(131, 20);
            this.chkGiuong.TabIndex = 7;
            this.chkGiuong.TabStop = false;
            this.chkGiuong.Text = "Công khai giường";
            this.chkGiuong.UseVisualStyleBackColor = true;
            this.chkGiuong.Visible = false;
            // 
            // grdPhieudieutri
            // 
            this.grdPhieudieutri.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPhieudieutri.AlternatingColors = true;
            this.grdPhieudieutri.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdPhieudieutri.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPhieudieutri.AutomaticSort = false;
            this.grdPhieudieutri.BackColor = System.Drawing.Color.Silver;
            this.grdPhieudieutri.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân đưa vào phòng khám</FilterRowInfoText></LocalizableData>";
            grdPhieudieutri_DesignTimeLayout.LayoutString = resources.GetString("grdPhieudieutri_DesignTimeLayout.LayoutString");
            this.grdPhieudieutri.DesignTimeLayout = grdPhieudieutri_DesignTimeLayout;
            this.grdPhieudieutri.DynamicFiltering = true;
            this.grdPhieudieutri.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdPhieudieutri.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPhieudieutri.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdPhieudieutri.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPhieudieutri.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPhieudieutri.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPhieudieutri.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdPhieudieutri.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPhieudieutri.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdPhieudieutri.FrozenColumns = -1;
            this.grdPhieudieutri.GroupByBoxVisible = false;
            this.grdPhieudieutri.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPhieudieutri.LinkFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdPhieudieutri.Location = new System.Drawing.Point(133, 212);
            this.grdPhieudieutri.Name = "grdPhieudieutri";
            this.grdPhieudieutri.RecordNavigator = true;
            this.grdPhieudieutri.SelectedFormatStyle.Alpha = 2;
            this.grdPhieudieutri.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdPhieudieutri.SelectedFormatStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPhieudieutri.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPhieudieutri.SelectedFormatStyle.ForeColor = System.Drawing.Color.White;
            this.grdPhieudieutri.SelectedInactiveFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdPhieudieutri.Size = new System.Drawing.Size(852, 454);
            this.grdPhieudieutri.TabIndex = 20;
            this.grdPhieudieutri.TabStop = false;
            // 
            // chkVTTH
            // 
            this.chkVTTH.AutoSize = true;
            this.chkVTTH.Checked = true;
            this.chkVTTH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVTTH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVTTH.ForeColor = System.Drawing.Color.Navy;
            this.chkVTTH.Location = new System.Drawing.Point(475, 175);
            this.chkVTTH.Name = "chkVTTH";
            this.chkVTTH.Size = new System.Drawing.Size(129, 20);
            this.chkVTTH.TabIndex = 7;
            this.chkVTTH.TabStop = false;
            this.chkVTTH.Text = "Công khai VTTH";
            this.chkVTTH.UseVisualStyleBackColor = true;
            // 
            // chkThuoc
            // 
            this.chkThuoc.AutoSize = true;
            this.chkThuoc.Checked = true;
            this.chkThuoc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThuoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chkThuoc.Location = new System.Drawing.Point(331, 176);
            this.chkThuoc.Name = "chkThuoc";
            this.chkThuoc.Size = new System.Drawing.Size(130, 20);
            this.chkThuoc.TabIndex = 7;
            this.chkThuoc.TabStop = false;
            this.chkThuoc.Text = "Công khai thuốc";
            this.chkThuoc.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "Loại công khai:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkCLS
            // 
            this.chkCLS.AutoSize = true;
            this.chkCLS.Checked = true;
            this.chkCLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCLS.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCLS.ForeColor = System.Drawing.Color.Indigo;
            this.chkCLS.Location = new System.Drawing.Point(133, 175);
            this.chkCLS.Name = "chkCLS";
            this.chkCLS.Size = new System.Drawing.Size(180, 20);
            this.chkCLS.TabIndex = 6;
            this.chkCLS.Text = "Công khai cận lâm sàng";
            this.chkCLS.UseVisualStyleBackColor = true;
            // 
            // ucThongtinnguoibenh_v21
            // 
            this.ucThongtinnguoibenh_v21.Location = new System.Drawing.Point(22, 3);
            this.ucThongtinnguoibenh_v21.Name = "ucThongtinnguoibenh_v21";
            this.ucThongtinnguoibenh_v21.Size = new System.Drawing.Size(974, 129);
            this.ucThongtinnguoibenh_v21.TabIndex = 0;
            this.ucThongtinnguoibenh_v21.TabStop = false;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDenNgay.Location = new System.Drawing.Point(394, 138);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(165, 22);
            this.dtDenNgay.TabIndex = 5;
            this.dtDenNgay.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.FirstMonth = new System.DateTime(2020, 3, 1, 0, 0, 0, 0);
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTuNgay.Location = new System.Drawing.Point(133, 138);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(165, 22);
            this.dtTuNgay.TabIndex = 4;
            this.dtTuNgay.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(328, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "đến ngày";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(6, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ngày điều trị từ:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkAutoFillEmpty);
            this.panel3.Controls.Add(this.lblMsg);
            this.panel3.Controls.Add(this.cmdExcel);
            this.panel3.Controls.Add(this.cmdIn);
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 672);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 57);
            this.panel3.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(140, 25);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(20, 16);
            this.lblMsg.TabIndex = 17;
            this.lblMsg.Text = "...";
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExcel.Location = new System.Drawing.Point(739, 12);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(120, 33);
            this.cmdExcel.TabIndex = 16;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdIn.Location = new System.Drawing.Point(613, 12);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(120, 33);
            this.cmdIn.TabIndex = 9;
            this.cmdIn.Text = "In";
            this.cmdIn.ToolTipText = "Nhấn vào đây để thêm mới Bệnh nhân";
            this.cmdIn.Click += new System.EventHandler(this.cmdIn_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Noitru.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(865, 12);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 33);
            this.cmdExit.TabIndex = 15;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click_1);
            // 
            // chkAutoFillEmpty
            // 
            this.chkAutoFillEmpty.AutoSize = true;
            this.chkAutoFillEmpty.Checked = true;
            this.chkAutoFillEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoFillEmpty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoFillEmpty.ForeColor = System.Drawing.Color.Black;
            this.chkAutoFillEmpty.Location = new System.Drawing.Point(133, 25);
            this.chkAutoFillEmpty.Name = "chkAutoFillEmpty";
            this.chkAutoFillEmpty.Size = new System.Drawing.Size(432, 20);
            this.chkAutoFillEmpty.TabIndex = 18;
            this.chkAutoFillEmpty.Text = "Tự động fill các ngày trống trong khoảng ngày min - ngày max?";
            this.chkAutoFillEmpty.UseVisualStyleBackColor = true;
            this.chkAutoFillEmpty.Visible = false;
            // 
            // frm_phieucongkhai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.KeyPreview = true;
            this.Name = "frm_phieucongkhai";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng kết bệnh án";
            this.Load += new System.EventHandler(this.frm_phieucongkhai_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieudieutri)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdExit;
        public Forms.Dungchung.UCs.ucThongtinnguoibenh_v2 ucThongtinnguoibenh_v21;
        private System.Windows.Forms.CheckBox chkVTTH;
        private System.Windows.Forms.CheckBox chkThuoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCLS;
        private Janus.Windows.GridEX.GridEX grdPhieudieutri;
        private System.Windows.Forms.CheckBox chkGiuong;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkAutoFillEmpty;
    }
}