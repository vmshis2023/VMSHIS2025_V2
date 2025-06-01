namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_Tralaitien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Tralaitien));
            Janus.Windows.GridEX.GridEXLayout grdPaymentDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkNoview = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdInhoadon = new Janus.Windows.EditControls.UIButton();
            this.cmdInBienlai = new Janus.Windows.EditControls.UIButton();
            this.cmdClose1 = new Janus.Windows.EditControls.UIButton();
            this.chkIntonghop = new Janus.Windows.EditControls.UICheckBox();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.txtSotien = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtLydohuy = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIDPhieuthu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMathanhtoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDichvutralai = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNguoinop = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCo = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNo = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSoCTgoc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpngaythanhtoan = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIDthanhtoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaphieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlInhoadon = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHuyThanhtoan = new System.Windows.Forms.Panel();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPaymentDetail = new Janus.Windows.GridEX.GridEX();
            this.pnlInfor.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlInhoadon.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlHuyThanhtoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(310, 14);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(114, 30);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.Text = "Hủy phiếu chi";
            this.toolTip1.SetToolTip(this.cmdPrint, "Nhấn vào đây để bắt đầu hủy thanh toán cho các mục được chọn trên lưới");
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(438, 14);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 30);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkNoview
            // 
            this.chkNoview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkNoview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoview.Location = new System.Drawing.Point(3, 20);
            this.chkNoview.Name = "chkNoview";
            this.chkNoview.Size = new System.Drawing.Size(303, 20);
            this.chkNoview.TabIndex = 7;
            this.chkNoview.Text = "Hiển thị thông tin cho người dùng trước khi hủy?";
            this.toolTip1.SetToolTip(this.chkNoview, resources.GetString("chkNoview.ToolTip"));
            this.chkNoview.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // cmdInhoadon
            // 
            this.cmdInhoadon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInhoadon.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInhoadon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInhoadon.Image = ((System.Drawing.Image)(resources.GetObject("cmdInhoadon.Image")));
            this.cmdInhoadon.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInhoadon.Location = new System.Drawing.Point(519, 3);
            this.cmdInhoadon.Name = "cmdInhoadon";
            this.cmdInhoadon.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInhoadon.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInhoadon.Size = new System.Drawing.Size(120, 35);
            this.cmdInhoadon.TabIndex = 5;
            this.cmdInhoadon.Text = "In phiếu chi";
            this.toolTip1.SetToolTip(this.cmdInhoadon, "Nhấn vào đây để in phiếu chi");
            // 
            // cmdInBienlai
            // 
            this.cmdInBienlai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInBienlai.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInBienlai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInBienlai.Image = ((System.Drawing.Image)(resources.GetObject("cmdInBienlai.Image")));
            this.cmdInBienlai.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInBienlai.Location = new System.Drawing.Point(393, 3);
            this.cmdInBienlai.Name = "cmdInBienlai";
            this.cmdInBienlai.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInBienlai.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInBienlai.Size = new System.Drawing.Size(120, 35);
            this.cmdInBienlai.TabIndex = 6;
            this.cmdInBienlai.Text = "In biên lai";
            this.toolTip1.SetToolTip(this.cmdInBienlai, "Nhấn vào đây để in biên lai của lần thanh toán đang chọn");
            this.cmdInBienlai.Click += new System.EventHandler(this.cmdInBienlai_Click_1);
            // 
            // cmdClose1
            // 
            this.cmdClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose1.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose1.Image")));
            this.cmdClose1.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose1.Location = new System.Drawing.Point(645, 3);
            this.cmdClose1.Name = "cmdClose1";
            this.cmdClose1.Size = new System.Drawing.Size(120, 35);
            this.cmdClose1.TabIndex = 7;
            this.cmdClose1.Text = "&Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdClose1, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            // 
            // chkIntonghop
            // 
            this.chkIntonghop.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIntonghop.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkIntonghop.Location = new System.Drawing.Point(139, 104);
            this.chkIntonghop.Name = "chkIntonghop";
            this.chkIntonghop.Size = new System.Drawing.Size(210, 23);
            this.chkIntonghop.TabIndex = 544;
            this.chkIntonghop.TabStop = false;
            this.chkIntonghop.Text = "In tổng hợp các lần chi?";
            this.toolTip1.SetToolTip(this.chkIntonghop, "Ngày nhập viện");
            this.chkIntonghop.Visible = false;
            // 
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.txtSotien);
            this.pnlInfor.Controls.Add(this.txtLydohuy);
            this.pnlInfor.Controls.Add(this.label11);
            this.pnlInfor.Controls.Add(this.chkIntonghop);
            this.pnlInfor.Controls.Add(this.txtIDPhieuthu);
            this.pnlInfor.Controls.Add(this.label13);
            this.pnlInfor.Controls.Add(this.label10);
            this.pnlInfor.Controls.Add(this.txtMathanhtoan);
            this.pnlInfor.Controls.Add(this.label4);
            this.pnlInfor.Controls.Add(this.txtDichvutralai);
            this.pnlInfor.Controls.Add(this.label9);
            this.pnlInfor.Controls.Add(this.txtNguoinop);
            this.pnlInfor.Controls.Add(this.label8);
            this.pnlInfor.Controls.Add(this.txtCo);
            this.pnlInfor.Controls.Add(this.label7);
            this.pnlInfor.Controls.Add(this.txtNo);
            this.pnlInfor.Controls.Add(this.label6);
            this.pnlInfor.Controls.Add(this.txtSoCTgoc);
            this.pnlInfor.Controls.Add(this.label5);
            this.pnlInfor.Controls.Add(this.dtpngaythanhtoan);
            this.pnlInfor.Controls.Add(this.label2);
            this.pnlInfor.Controls.Add(this.label1);
            this.pnlInfor.Controls.Add(this.txtIDthanhtoan);
            this.pnlInfor.Controls.Add(this.label3);
            this.pnlInfor.Controls.Add(this.txtMaphieu);
            this.pnlInfor.Controls.Add(this.pnlActions);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 551);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(1008, 179);
            this.pnlInfor.TabIndex = 8;
            // 
            // txtSotien
            // 
            this.txtSotien.BackColor = System.Drawing.Color.White;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtSotien.ForeColor = System.Drawing.Color.Green;
            this.txtSotien.Location = new System.Drawing.Point(622, 6);
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.ReadOnly = true;
            this.txtSotien.Size = new System.Drawing.Size(158, 26);
            this.txtSotien.TabIndex = 532;
            this.txtSotien.Tag = "NO";
            this.txtSotien.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtSotien.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtLydohuy
            // 
            this.txtLydohuy._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLydohuy._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydohuy._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydohuy.AddValues = true;
            this.txtLydohuy.AllowMultiline = false;
            this.txtLydohuy.AutoCompleteList = null;
            this.txtLydohuy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydohuy.buildShortcut = false;
            this.txtLydohuy.CaseSensitive = false;
            this.txtLydohuy.CompareNoID = true;
            this.txtLydohuy.DefaultCode = "-1";
            this.txtLydohuy.DefaultID = "-1";
            this.txtLydohuy.Drug_ID = null;
            this.txtLydohuy.ExtraWidth = 0;
            this.txtLydohuy.FillValueAfterSelect = false;
            this.txtLydohuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydohuy.LOAI_DANHMUC = "LYDOTRATIEN";
            this.txtLydohuy.Location = new System.Drawing.Point(139, 81);
            this.txtLydohuy.MaxHeight = -1;
            this.txtLydohuy.MinTypedCharacters = 2;
            this.txtLydohuy.MyCode = "-1";
            this.txtLydohuy.MyID = "-1";
            this.txtLydohuy.Name = "txtLydohuy";
            this.txtLydohuy.RaiseEvent = false;
            this.txtLydohuy.RaiseEventEnter = false;
            this.txtLydohuy.RaiseEventEnterWhenEmpty = false;
            this.txtLydohuy.SelectedIndex = -1;
            this.txtLydohuy.ShowCodeWithValue = false;
            this.txtLydohuy.Size = new System.Drawing.Size(641, 21);
            this.txtLydohuy.splitChar = '@';
            this.txtLydohuy.splitCharIDAndCode = '#';
            this.txtLydohuy.TabIndex = 4;
            this.txtLydohuy.TakeCode = false;
            this.txtLydohuy.txtMyCode = null;
            this.txtLydohuy.txtMyCode_Edit = null;
            this.txtLydohuy.txtMyID = null;
            this.txtLydohuy.txtMyID_Edit = null;
            this.txtLydohuy.txtMyName = null;
            this.txtLydohuy.txtMyName_Edit = null;
            this.txtLydohuy.txtNext = null;
            this.txtLydohuy.txtNext1 = null;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(379, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 18);
            this.label11.TabIndex = 530;
            this.label11.Text = "ID:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIDPhieuthu
            // 
            this.txtIDPhieuthu.Enabled = false;
            this.txtIDPhieuthu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDPhieuthu.Location = new System.Drawing.Point(479, 9);
            this.txtIDPhieuthu.Name = "txtIDPhieuthu";
            this.txtIDPhieuthu.ReadOnly = true;
            this.txtIDPhieuthu.Size = new System.Drawing.Size(89, 21);
            this.txtIDPhieuthu.TabIndex = 529;
            this.txtIDPhieuthu.TabStop = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(19, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 20);
            this.label13.TabIndex = 528;
            this.label13.Text = "Lý do trả tiền";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(921, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 20);
            this.label10.TabIndex = 526;
            this.label10.Text = "Mã phiếu chi";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // txtMathanhtoan
            // 
            this.txtMathanhtoan.Enabled = false;
            this.txtMathanhtoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMathanhtoan.Location = new System.Drawing.Point(937, 38);
            this.txtMathanhtoan.Name = "txtMathanhtoan";
            this.txtMathanhtoan.ReadOnly = true;
            this.txtMathanhtoan.Size = new System.Drawing.Size(10, 21);
            this.txtMathanhtoan.TabIndex = 525;
            this.txtMathanhtoan.TabStop = false;
            this.txtMathanhtoan.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(19, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 524;
            this.label4.Text = "Dịch vụ trả lại";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDichvutralai
            // 
            this.txtDichvutralai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvutralai.Location = new System.Drawing.Point(139, 56);
            this.txtDichvutralai.MaxLength = 255;
            this.txtDichvutralai.Name = "txtDichvutralai";
            this.txtDichvutralai.ReadOnly = true;
            this.txtDichvutralai.Size = new System.Drawing.Size(641, 21);
            this.txtDichvutralai.TabIndex = 509;
            this.txtDichvutralai.TabStop = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(359, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 20);
            this.label9.TabIndex = 523;
            this.label9.Text = "Người chi:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNguoinop
            // 
            this.txtNguoinop.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoinop.Location = new System.Drawing.Point(479, 33);
            this.txtNguoinop.Name = "txtNguoinop";
            this.txtNguoinop.ReadOnly = true;
            this.txtNguoinop.Size = new System.Drawing.Size(301, 21);
            this.txtNguoinop.TabIndex = 508;
            this.txtNguoinop.TabStop = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(970, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 18);
            this.label8.TabIndex = 522;
            this.label8.Text = "Tài khoản có";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // txtCo
            // 
            this.txtCo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCo.Location = new System.Drawing.Point(986, 61);
            this.txtCo.Name = "txtCo";
            this.txtCo.Size = new System.Drawing.Size(10, 21);
            this.txtCo.TabIndex = 2;
            this.txtCo.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(970, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 18);
            this.label7.TabIndex = 521;
            this.label7.Text = "Tài khoản nợ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNo.Location = new System.Drawing.Point(986, 34);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(10, 21);
            this.txtNo.TabIndex = 1;
            this.txtNo.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(970, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 18);
            this.label6.TabIndex = 520;
            this.label6.Text = "Số lượng CT gốc";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // txtSoCTgoc
            // 
            this.txtSoCTgoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoCTgoc.Location = new System.Drawing.Point(986, 7);
            this.txtSoCTgoc.Name = "txtSoCTgoc";
            this.txtSoCTgoc.Size = new System.Drawing.Size(10, 21);
            this.txtSoCTgoc.TabIndex = 0;
            this.txtSoCTgoc.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(574, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
            this.label5.TabIndex = 519;
            this.label5.Text = "Số tiền";
            // 
            // dtpngaythanhtoan
            // 
            this.dtpngaythanhtoan.CustomFormat = "dd/MM/yyyy";
            this.dtpngaythanhtoan.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpngaythanhtoan.DropDownCalendar.Name = "";
            this.dtpngaythanhtoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpngaythanhtoan.Location = new System.Drawing.Point(139, 6);
            this.dtpngaythanhtoan.Name = "dtpngaythanhtoan";
            this.dtpngaythanhtoan.ReadOnly = true;
            this.dtpngaythanhtoan.ShowUpDown = true;
            this.dtpngaythanhtoan.Size = new System.Drawing.Size(216, 21);
            this.dtpngaythanhtoan.TabIndex = 518;
            this.dtpngaythanhtoan.TabStop = false;
            this.dtpngaythanhtoan.Value = new System.DateTime(2023, 8, 18, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(19, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 517;
            this.label2.Text = "Ngày thực hiện";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(921, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 20);
            this.label1.TabIndex = 516;
            this.label1.Text = "ID thanh toán";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // txtIDthanhtoan
            // 
            this.txtIDthanhtoan.Enabled = false;
            this.txtIDthanhtoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDthanhtoan.Location = new System.Drawing.Point(937, 11);
            this.txtIDthanhtoan.Name = "txtIDthanhtoan";
            this.txtIDthanhtoan.ReadOnly = true;
            this.txtIDthanhtoan.Size = new System.Drawing.Size(10, 21);
            this.txtIDthanhtoan.TabIndex = 515;
            this.txtIDthanhtoan.TabStop = false;
            this.txtIDthanhtoan.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(19, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 514;
            this.label3.Text = "Mã phiếu chi";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaphieu
            // 
            this.txtMaphieu.Enabled = false;
            this.txtMaphieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaphieu.Location = new System.Drawing.Point(139, 33);
            this.txtMaphieu.Name = "txtMaphieu";
            this.txtMaphieu.ReadOnly = true;
            this.txtMaphieu.Size = new System.Drawing.Size(216, 21);
            this.txtMaphieu.TabIndex = 507;
            this.txtMaphieu.TabStop = false;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlInhoadon);
            this.pnlActions.Controls.Add(this.pnlHuyThanhtoan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 133);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1008, 46);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlInhoadon
            // 
            this.pnlInhoadon.Controls.Add(this.flowLayoutPanel1);
            this.pnlInhoadon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInhoadon.Location = new System.Drawing.Point(0, 0);
            this.pnlInhoadon.Name = "pnlInhoadon";
            this.pnlInhoadon.Size = new System.Drawing.Size(1008, 46);
            this.pnlInhoadon.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdClose1);
            this.flowLayoutPanel1.Controls.Add(this.cmdInhoadon);
            this.flowLayoutPanel1.Controls.Add(this.cmdInBienlai);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(240, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(768, 46);
            this.flowLayoutPanel1.TabIndex = 23;
            // 
            // pnlHuyThanhtoan
            // 
            this.pnlHuyThanhtoan.Controls.Add(this.cmdPrint);
            this.pnlHuyThanhtoan.Controls.Add(this.chkNoview);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdExit);
            this.pnlHuyThanhtoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHuyThanhtoan.Location = new System.Drawing.Point(0, 0);
            this.pnlHuyThanhtoan.Name = "pnlHuyThanhtoan";
            this.pnlHuyThanhtoan.Size = new System.Drawing.Size(1008, 46);
            this.pnlHuyThanhtoan.TabIndex = 1;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdPaymentDetail);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 551);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết các dịch vụ đã trả lại tiền cho bệnh nhân";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdPaymentDetail
            // 
            this.grdPaymentDetail.AlternatingColors = true;
            this.grdPaymentDetail.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdPaymentDetail.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><RecordNavigator>Số bả" +
    "n ghi:|/</RecordNavigator></LocalizableData>";
            grdPaymentDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPaymentDetail_DesignTimeLayout.LayoutString");
            this.grdPaymentDetail.DesignTimeLayout = grdPaymentDetail_DesignTimeLayout;
            this.grdPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPaymentDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPaymentDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPaymentDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPaymentDetail.FrozenColumns = 10;
            this.grdPaymentDetail.GroupByBoxVisible = false;
            this.grdPaymentDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdPaymentDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdPaymentDetail.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdPaymentDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPaymentDetail.Location = new System.Drawing.Point(3, 17);
            this.grdPaymentDetail.Name = "grdPaymentDetail";
            this.grdPaymentDetail.RecordNavigator = true;
            this.grdPaymentDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPaymentDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdPaymentDetail.Size = new System.Drawing.Size(1002, 531);
            this.grdPaymentDetail.TabIndex = 116;
            this.grdPaymentDetail.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdPaymentDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPaymentDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPaymentDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_Tralaitien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Tralaitien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hủy phiếu trả lại tiền (phiếu chi)";
            this.Load += new System.EventHandler(this.frm_Tralaitien_Load);
            this.pnlInfor.ResumeLayout(false);
            this.pnlInfor.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlInhoadon.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlHuyThanhtoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.CheckBox chkNoview;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlInhoadon;
        private System.Windows.Forms.Panel pnlHuyThanhtoan;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Janus.Windows.EditControls.UIButton cmdInhoadon;
        private Janus.Windows.EditControls.UIButton cmdInBienlai;
        private Janus.Windows.EditControls.UIButton cmdClose1;
        private Janus.Windows.GridEX.GridEX grdPaymentDetail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMathanhtoan;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtDichvutralai;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.GridEX.EditControls.EditBox txtNguoinop;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtCo;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtNo;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoCTgoc;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpngaythanhtoan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtIDthanhtoan;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaphieu;
        private System.Windows.Forms.Label label11;
        internal Janus.Windows.GridEX.EditControls.EditBox txtIDPhieuthu;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLydohuy;
        private Janus.Windows.GridEX.EditControls.EditBox txtSotien;
        private Janus.Windows.EditControls.UICheckBox chkIntonghop;
    }
}