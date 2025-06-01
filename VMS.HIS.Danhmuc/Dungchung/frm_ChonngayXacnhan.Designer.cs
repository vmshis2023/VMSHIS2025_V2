using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonngayXacnhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonngayXacnhan));
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtNhanvien = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.lblNhanvien = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.radEditDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radRegisterDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radCurrentDate = new Janus.Windows.EditControls.UIRadioButton();
            this.pnlheader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.pnlheader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(123, 217);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(125, 33);
            this.cmdAccept.TabIndex = 5;
            this.cmdAccept.Text = "Chấp nhận";
            this.toolTip1.SetToolTip(this.cmdAccept, "Có thể nhấn tổ hợp phím Ctrl+A hoặc Ctrl+S để chấp nhận ngày thanh toán đang chọn" +
        "");
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(263, 217);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 33);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn phím Escape để thoát");
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiGroupBox1);
            this.panel1.Controls.Add(this.cmdAccept);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.pnlheader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 262);
            this.panel1.TabIndex = 3;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lblMsg);
            this.uiGroupBox1.Controls.Add(this.txtNhanvien);
            this.uiGroupBox1.Controls.Add(this.lblNhanvien);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtCreateDate);
            this.uiGroupBox1.Controls.Add(this.radEditDate);
            this.uiGroupBox1.Controls.Add(this.radRegisterDate);
            this.uiGroupBox1.Controls.Add(this.radCurrentDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 55);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(500, 156);
            this.uiGroupBox1.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 129);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(494, 24);
            this.lblMsg.TabIndex = 6;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNhanvien
            // 
            this.txtNhanvien._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNhanvien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhanvien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNhanvien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhanvien.AutoCompleteList")));
            this.txtNhanvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhanvien.CaseSensitive = false;
            this.txtNhanvien.CompareNoID = true;
            this.txtNhanvien.DefaultCode = "-1";
            this.txtNhanvien.DefaultID = "-1";
            this.txtNhanvien.Drug_ID = null;
            this.txtNhanvien.ExtraWidth = 0;
            this.txtNhanvien.FillValueAfterSelect = false;
            this.txtNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvien.Location = new System.Drawing.Point(123, 89);
            this.txtNhanvien.MaxHeight = 289;
            this.txtNhanvien.MinTypedCharacters = 2;
            this.txtNhanvien.MyCode = "-1";
            this.txtNhanvien.MyID = "-1";
            this.txtNhanvien.MyText = "";
            this.txtNhanvien.Name = "txtNhanvien";
            this.txtNhanvien.RaiseEvent = true;
            this.txtNhanvien.RaiseEventEnter = true;
            this.txtNhanvien.RaiseEventEnterWhenEmpty = true;
            this.txtNhanvien.SelectedIndex = -1;
            this.txtNhanvien.Size = new System.Drawing.Size(293, 21);
            this.txtNhanvien.splitChar = '@';
            this.txtNhanvien.splitCharIDAndCode = '#';
            this.txtNhanvien.TabIndex = 4;
            this.txtNhanvien.TakeCode = false;
            this.txtNhanvien.txtMyCode = null;
            this.txtNhanvien.txtMyCode_Edit = null;
            this.txtNhanvien.txtMyID = null;
            this.txtNhanvien.txtMyID_Edit = null;
            this.txtNhanvien.txtMyName = null;
            this.txtNhanvien.txtMyName_Edit = null;
            this.txtNhanvien.txtNext = null;
            this.txtNhanvien.Visible = false;
            // 
            // lblNhanvien
            // 
            this.lblNhanvien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanvien.Location = new System.Drawing.Point(3, 92);
            this.lblNhanvien.Name = "lblNhanvien";
            this.lblNhanvien.Size = new System.Drawing.Size(114, 15);
            this.lblNhanvien.TabIndex = 5;
            this.lblNhanvien.Text = "Người trả:";
            this.lblNhanvien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNhanvien.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ngày xác nhận:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.Enabled = false;
            this.dtCreateDate.Location = new System.Drawing.Point(123, 62);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(293, 21);
            this.dtCreateDate.TabIndex = 3;
            this.dtCreateDate.ValueChanged += new System.EventHandler(this.dtCreateDate_ValueChanged);
            // 
            // radEditDate
            // 
            this.radEditDate.Location = new System.Drawing.Point(369, 21);
            this.radEditDate.Name = "radEditDate";
            this.radEditDate.Size = new System.Drawing.Size(126, 23);
            this.radEditDate.TabIndex = 2;
            this.radEditDate.TabStop = true;
            this.radEditDate.Text = "Tùy chỉnh";
            // 
            // radRegisterDate
            // 
            this.radRegisterDate.Location = new System.Drawing.Point(233, 21);
            this.radRegisterDate.Name = "radRegisterDate";
            this.radRegisterDate.Size = new System.Drawing.Size(130, 23);
            this.radRegisterDate.TabIndex = 1;
            this.radRegisterDate.TabStop = true;
            this.radRegisterDate.Text = "Ngày hóa đơn";
            // 
            // radCurrentDate
            // 
            this.radCurrentDate.Checked = true;
            this.radCurrentDate.Location = new System.Drawing.Point(123, 21);
            this.radCurrentDate.Name = "radCurrentDate";
            this.radCurrentDate.Size = new System.Drawing.Size(104, 23);
            this.radCurrentDate.TabIndex = 0;
            this.radCurrentDate.TabStop = true;
            this.radCurrentDate.Text = "Ngày hiện tại";
            // 
            // pnlheader
            // 
            this.pnlheader.Controls.Add(this.label2);
            this.pnlheader.Controls.Add(this.panel2);
            this.pnlheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlheader.Location = new System.Drawing.Point(0, 0);
            this.pnlheader.Name = "pnlheader";
            this.pnlheader.Size = new System.Drawing.Size(500, 55);
            this.pnlheader.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(73, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(427, 55);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ngày xác nhận là ngày biến động thực tế trong kho. Mời bạn chọn ngày xác nhận xuấ" +
    "t-nhập thuốc trong kho";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(73, 55);
            this.panel2.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // frm_ChonngayXacnhan
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 262);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonngayXacnhan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn ngày xác nhận thuốc";
            this.Load += new System.EventHandler(this.frm_ChonngayXacnhan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonngayXacnhan_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.pnlheader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIRadioButton radEditDate;
        private Janus.Windows.EditControls.UIRadioButton radRegisterDate;
        private Janus.Windows.EditControls.UIRadioButton radCurrentDate;
        private System.Windows.Forms.Panel pnlheader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label lblNhanvien;
        public System.Windows.Forms.Label lblMsg;
        public AutoCompleteTextbox txtNhanvien;
    }
}