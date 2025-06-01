using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonLydohuyTUHU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonLydohuyTUHU));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblName = new System.Windows.Forms.Label();
            this.txtLydoHuy = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cboNganhang = new Janus.Windows.EditControls.UIComboBox();
            this.cboPttt = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtNgayHuy = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblNgayHuyTUHU = new System.Windows.Forms.Label();
            this.dtNgayTUHU = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblNgayTUHU = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle2);
            this.panel1.Controls.Add(this.lblTitle1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 63);
            this.panel1.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.Location = new System.Drawing.Point(77, 33);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(464, 21);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "Nhập lý do hủy thanh toán trước khi thực hiện...";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(77, 9);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(464, 21);
            this.lblTitle1.TabIndex = 541;
            this.lblTitle1.Text = "Hủy thanh toán tiền Bệnh nhân";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 61);
            this.panel2.TabIndex = 0;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(1, 227);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(627, 22);
            this.vbLine1.TabIndex = 9;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Hành động";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblName.ForeColor = System.Drawing.Color.Red;
            this.lblName.Location = new System.Drawing.Point(12, 114);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 21);
            this.lblName.TabIndex = 540;
            this.lblName.Text = "Lý do hủy:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLydoHuy
            // 
            this.txtLydoHuy._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLydoHuy._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydoHuy._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydoHuy.AddValues = true;
            this.txtLydoHuy.AllowMultiline = false;
            this.txtLydoHuy.AutoCompleteList = null;
            this.txtLydoHuy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydoHuy.buildShortcut = false;
            this.txtLydoHuy.CaseSensitive = false;
            this.txtLydoHuy.CompareNoID = true;
            this.txtLydoHuy.DefaultCode = "-1";
            this.txtLydoHuy.DefaultID = "-1";
            this.txtLydoHuy.Drug_ID = null;
            this.txtLydoHuy.ExtraWidth = 0;
            this.txtLydoHuy.FillValueAfterSelect = false;
            this.txtLydoHuy.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtLydoHuy.LOAI_DANHMUC = "LYDOHUYTU";
            this.txtLydoHuy.Location = new System.Drawing.Point(121, 114);
            this.txtLydoHuy.MaxHeight = -1;
            this.txtLydoHuy.MinTypedCharacters = 2;
            this.txtLydoHuy.MyCode = "-1";
            this.txtLydoHuy.MyID = "-1";
            this.txtLydoHuy.Name = "txtLydoHuy";
            this.txtLydoHuy.RaiseEvent = false;
            this.txtLydoHuy.RaiseEventEnter = false;
            this.txtLydoHuy.RaiseEventEnterWhenEmpty = false;
            this.txtLydoHuy.SelectedIndex = -1;
            this.txtLydoHuy.ShowCodeWithValue = false;
            this.txtLydoHuy.Size = new System.Drawing.Size(440, 22);
            this.txtLydoHuy.splitChar = '@';
            this.txtLydoHuy.splitCharIDAndCode = '#';
            this.txtLydoHuy.TabIndex = 4;
            this.txtLydoHuy.TakeCode = false;
            this.txtLydoHuy.txtMyCode = null;
            this.txtLydoHuy.txtMyCode_Edit = null;
            this.txtLydoHuy.txtMyID = null;
            this.txtLydoHuy.txtMyID_Edit = null;
            this.txtLydoHuy.txtMyName = null;
            this.txtLydoHuy.txtMyName_Edit = null;
            this.txtLydoHuy.txtNext = null;
            this.txtLydoHuy.txtNext1 = null;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(5, 196);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(565, 27);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(441, 254);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Hủy bỏ";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(310, 254);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            // 
            // cboNganhang
            // 
            this.cboNganhang.BackColor = System.Drawing.Color.White;
            this.cboNganhang.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboNganhang.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboNganhang.Enabled = false;
            this.cboNganhang.Font = new System.Drawing.Font("Arial", 9.75F);
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "In nhiệt";
            uiComboBoxItem1.Value = "0";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "In laser";
            uiComboBoxItem2.Value = "1";
            this.cboNganhang.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cboNganhang.Location = new System.Drawing.Point(121, 170);
            this.cboNganhang.Name = "cboNganhang";
            this.cboNganhang.Size = new System.Drawing.Size(440, 22);
            this.cboNganhang.TabIndex = 6;
            this.cboNganhang.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboNganhang.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cboPttt
            // 
            this.cboPttt.BackColor = System.Drawing.Color.White;
            this.cboPttt.BorderStyle = Janus.Windows.UI.BorderStyle.Sunken;
            this.cboPttt.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPttt.Font = new System.Drawing.Font("Arial", 9.75F);
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "In nhiệt";
            uiComboBoxItem3.Value = "0";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "In laser";
            uiComboBoxItem4.Value = "1";
            this.cboPttt.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4});
            this.cboPttt.Location = new System.Drawing.Point(121, 142);
            this.cboPttt.Name = "cboPttt";
            this.cboPttt.Size = new System.Drawing.Size(440, 22);
            this.cboPttt.TabIndex = 5;
            this.cboPttt.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.cboPttt.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cboPttt.SelectedIndexChanged += new System.EventHandler(this.cboPttt_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(13, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 605;
            this.label5.Text = "Ngân hàng:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 604;
            this.label1.Text = "Hình thức TT:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtNgayHuy
            // 
            this.dtNgayHuy.CustomFormat = "dd/MM/yyyy";
            this.dtNgayHuy.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayHuy.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtNgayHuy.DropDownCalendar.Name = "";
            this.dtNgayHuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayHuy.Location = new System.Drawing.Point(121, 87);
            this.dtNgayHuy.Name = "dtNgayHuy";
            this.dtNgayHuy.ShowUpDown = true;
            this.dtNgayHuy.Size = new System.Drawing.Size(147, 21);
            this.dtNgayHuy.TabIndex = 0;
            this.dtNgayHuy.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // lblNgayHuyTUHU
            // 
            this.lblNgayHuyTUHU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayHuyTUHU.ForeColor = System.Drawing.Color.Red;
            this.lblNgayHuyTUHU.Location = new System.Drawing.Point(3, 84);
            this.lblNgayHuyTUHU.Name = "lblNgayHuyTUHU";
            this.lblNgayHuyTUHU.Size = new System.Drawing.Size(112, 24);
            this.lblNgayHuyTUHU.TabIndex = 607;
            this.lblNgayHuyTUHU.Text = "Ngày hủy";
            this.lblNgayHuyTUHU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtNgayTUHU
            // 
            this.dtNgayTUHU.CustomFormat = "dd/MM/yyyy";
            this.dtNgayTUHU.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayTUHU.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtNgayTUHU.DropDownCalendar.Name = "";
            this.dtNgayTUHU.Enabled = false;
            this.dtNgayTUHU.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayTUHU.Location = new System.Drawing.Point(414, 87);
            this.dtNgayTUHU.Name = "dtNgayTUHU";
            this.dtNgayTUHU.ShowUpDown = true;
            this.dtNgayTUHU.Size = new System.Drawing.Size(147, 21);
            this.dtNgayTUHU.TabIndex = 608;
            this.dtNgayTUHU.TabStop = false;
            this.dtNgayTUHU.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // lblNgayTUHU
            // 
            this.lblNgayTUHU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayTUHU.ForeColor = System.Drawing.Color.Navy;
            this.lblNgayTUHU.Location = new System.Drawing.Point(296, 84);
            this.lblNgayTUHU.Name = "lblNgayTUHU";
            this.lblNgayTUHU.Size = new System.Drawing.Size(112, 24);
            this.lblNgayTUHU.TabIndex = 609;
            this.lblNgayTUHU.Text = "Ngày tạm ứng:";
            this.lblNgayTUHU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_ChonLydohuyTUHU
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(577, 301);
            this.Controls.Add(this.dtNgayTUHU);
            this.Controls.Add(this.lblNgayTUHU);
            this.Controls.Add(this.dtNgayHuy);
            this.Controls.Add(this.lblNgayHuyTUHU);
            this.Controls.Add(this.cboNganhang);
            this.Controls.Add(this.cboPttt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtLydoHuy);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vbLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonLydohuyTUHU";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HỦY THANH TOÁN";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UIComboBox cboPttt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public Janus.Windows.CalendarCombo.CalendarCombo dtNgayTUHU;
        public System.Windows.Forms.Label lblNgayTUHU;
        private System.Windows.Forms.Label lblNgayHuyTUHU;
        public Janus.Windows.CalendarCombo.CalendarCombo dtNgayHuy;
        public Janus.Windows.EditControls.UIComboBox cboNganhang;
        public AutoCompleteTextbox_Danhmucchung txtLydoHuy;


    }
}