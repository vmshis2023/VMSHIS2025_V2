namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_HuyThanhtoan_Quaythuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_HuyThanhtoan_Quaythuoc));
            Janus.Windows.GridEX.GridEXLayout grdPaymentDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkNoview = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdInhoadon = new Janus.Windows.EditControls.UIButton();
            this.cmdInBienlai = new Janus.Windows.EditControls.UIButton();
            this.cmdInBienlaiTonghop = new Janus.Windows.EditControls.UIButton();
            this.txtDachietkhau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdClose1 = new Janus.Windows.EditControls.UIButton();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmNgayInPhoiBHYT = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlInhoadon = new System.Windows.Forms.Panel();
            this.txtSoTienCanNop = new Janus.Windows.GridEX.EditControls.EditBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHuyThanhtoan = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTongtienDCT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtsotiendathu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblNgaythanhtoan = new System.Windows.Forms.Label();
            this.txtBNPhaiTra = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBNCT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtBHCT = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPtramBHChiTra = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPhuThu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtPaymentDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTongChiPhi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTuTuc = new Janus.Windows.GridEX.EditControls.EditBox();
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
            this.cmdPrint.Location = new System.Drawing.Point(310, 3);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(114, 30);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.Text = "&Hủy thanh toán";
            this.toolTip1.SetToolTip(this.cmdPrint, "Nhấn vào đây để bắt đầu hủy thanh toán cho các mục được chọn trên lưới");
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(438, 3);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 30);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkNoview
            // 
            this.chkNoview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkNoview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNoview.Location = new System.Drawing.Point(3, 9);
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
            this.cmdInhoadon.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInhoadon.Location = new System.Drawing.Point(514, 6);
            this.cmdInhoadon.Name = "cmdInhoadon";
            this.cmdInhoadon.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInhoadon.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInhoadon.Size = new System.Drawing.Size(107, 27);
            this.cmdInhoadon.TabIndex = 19;
            this.cmdInhoadon.Text = "In hóa đơn";
            this.toolTip1.SetToolTip(this.cmdInhoadon, "Nhấn vào đây để in hóa đơn");
            // 
            // cmdInBienlai
            // 
            this.cmdInBienlai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInBienlai.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInBienlai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInBienlai.Image = ((System.Drawing.Image)(resources.GetObject("cmdInBienlai.Image")));
            this.cmdInBienlai.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInBienlai.Location = new System.Drawing.Point(401, 6);
            this.cmdInBienlai.Name = "cmdInBienlai";
            this.cmdInBienlai.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInBienlai.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInBienlai.Size = new System.Drawing.Size(107, 27);
            this.cmdInBienlai.TabIndex = 21;
            this.cmdInBienlai.Text = "In phiếu";
            this.toolTip1.SetToolTip(this.cmdInBienlai, "Nhấn vào đây để in biên lai của lần thanh toán đang chọn");
            // 
            // cmdInBienlaiTonghop
            // 
            this.cmdInBienlaiTonghop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInBienlaiTonghop.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInBienlaiTonghop.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInBienlaiTonghop.Image = ((System.Drawing.Image)(resources.GetObject("cmdInBienlaiTonghop.Image")));
            this.cmdInBienlaiTonghop.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdInBienlaiTonghop.Location = new System.Drawing.Point(288, 6);
            this.cmdInBienlaiTonghop.Name = "cmdInBienlaiTonghop";
            this.cmdInBienlaiTonghop.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInBienlaiTonghop.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInBienlaiTonghop.Size = new System.Drawing.Size(107, 27);
            this.cmdInBienlaiTonghop.TabIndex = 20;
            this.cmdInBienlaiTonghop.Text = "In tổng hợp";
            this.toolTip1.SetToolTip(this.cmdInBienlaiTonghop, "Nhấn vào đây để in biên lai tổng hợp của tất cả các lần thanh toán");
            // 
            // txtDachietkhau
            // 
            this.txtDachietkhau.BackColor = System.Drawing.Color.White;
            this.txtDachietkhau.ButtonFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtDachietkhau.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtDachietkhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtDachietkhau.Location = new System.Drawing.Point(572, 69);
            this.txtDachietkhau.Name = "txtDachietkhau";
            this.txtDachietkhau.ReadOnly = true;
            this.txtDachietkhau.Size = new System.Drawing.Size(200, 29);
            this.txtDachietkhau.TabIndex = 377;
            this.txtDachietkhau.Tag = "NO";
            this.txtDachietkhau.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.toolTip1.SetToolTip(this.txtDachietkhau, "Tổng tiền chiết khấu");
            this.txtDachietkhau.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdClose1
            // 
            this.cmdClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose1.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose1.Image")));
            this.cmdClose1.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose1.Location = new System.Drawing.Point(627, 3);
            this.cmdClose1.Name = "cmdClose1";
            this.cmdClose1.Size = new System.Drawing.Size(106, 30);
            this.cmdClose1.TabIndex = 23;
            this.cmdClose1.Text = "&Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdClose1, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            // 
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.label1);
            this.pnlInfor.Controls.Add(this.dtmNgayInPhoiBHYT);
            this.pnlInfor.Controls.Add(this.label38);
            this.pnlInfor.Controls.Add(this.txtDachietkhau);
            this.pnlInfor.Controls.Add(this.pnlActions);
            this.pnlInfor.Controls.Add(this.label9);
            this.pnlInfor.Controls.Add(this.txtTongtienDCT);
            this.pnlInfor.Controls.Add(this.txtsotiendathu);
            this.pnlInfor.Controls.Add(this.lblNgaythanhtoan);
            this.pnlInfor.Controls.Add(this.txtBNPhaiTra);
            this.pnlInfor.Controls.Add(this.label37);
            this.pnlInfor.Controls.Add(this.label14);
            this.pnlInfor.Controls.Add(this.label15);
            this.pnlInfor.Controls.Add(this.txtBNCT);
            this.pnlInfor.Controls.Add(this.txtBHCT);
            this.pnlInfor.Controls.Add(this.txtPtramBHChiTra);
            this.pnlInfor.Controls.Add(this.label13);
            this.pnlInfor.Controls.Add(this.txtPhuThu);
            this.pnlInfor.Controls.Add(this.dtPaymentDate);
            this.pnlInfor.Controls.Add(this.label10);
            this.pnlInfor.Controls.Add(this.lblTongtien);
            this.pnlInfor.Controls.Add(this.label11);
            this.pnlInfor.Controls.Add(this.txtTongChiPhi);
            this.pnlInfor.Controls.Add(this.txtTuTuc);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 353);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(784, 209);
            this.pnlInfor.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(403, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 24);
            this.label1.TabIndex = 379;
            this.label1.Text = "Ngày in phôi BHYT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtmNgayInPhoiBHYT
            // 
            this.dtmNgayInPhoiBHYT.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtmNgayInPhoiBHYT.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtmNgayInPhoiBHYT.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtmNgayInPhoiBHYT.DropDownCalendar.Name = "";
            this.dtmNgayInPhoiBHYT.Enabled = false;
            this.dtmNgayInPhoiBHYT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmNgayInPhoiBHYT.Location = new System.Drawing.Point(572, 125);
            this.dtmNgayInPhoiBHYT.Name = "dtmNgayInPhoiBHYT";
            this.dtmNgayInPhoiBHYT.ShowUpDown = true;
            this.dtmNgayInPhoiBHYT.Size = new System.Drawing.Size(200, 21);
            this.dtmNgayInPhoiBHYT.TabIndex = 380;
            this.dtmNgayInPhoiBHYT.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label38.Location = new System.Drawing.Point(418, 73);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(143, 24);
            this.label38.TabIndex = 378;
            this.label38.Text = "Đã chiết khấu:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlInhoadon);
            this.pnlActions.Controls.Add(this.pnlHuyThanhtoan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 174);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(784, 35);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlInhoadon
            // 
            this.pnlInhoadon.Controls.Add(this.txtSoTienCanNop);
            this.pnlInhoadon.Controls.Add(this.flowLayoutPanel1);
            this.pnlInhoadon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInhoadon.Location = new System.Drawing.Point(0, 0);
            this.pnlInhoadon.Name = "pnlInhoadon";
            this.pnlInhoadon.Size = new System.Drawing.Size(784, 35);
            this.pnlInhoadon.TabIndex = 2;
            // 
            // txtSoTienCanNop
            // 
            this.txtSoTienCanNop.BackColor = System.Drawing.Color.White;
            this.txtSoTienCanNop.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoTienCanNop.ForeColor = System.Drawing.Color.Red;
            this.txtSoTienCanNop.Location = new System.Drawing.Point(0, 0);
            this.txtSoTienCanNop.Name = "txtSoTienCanNop";
            this.txtSoTienCanNop.ReadOnly = true;
            this.txtSoTienCanNop.Size = new System.Drawing.Size(167, 29);
            this.txtSoTienCanNop.TabIndex = 381;
            this.txtSoTienCanNop.Tag = "NO";
            this.txtSoTienCanNop.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtSoTienCanNop.Visible = false;
            this.txtSoTienCanNop.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdClose1);
            this.flowLayoutPanel1.Controls.Add(this.cmdInhoadon);
            this.flowLayoutPanel1.Controls.Add(this.cmdInBienlai);
            this.flowLayoutPanel1.Controls.Add(this.cmdInBienlaiTonghop);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(48, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(736, 35);
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
            this.pnlHuyThanhtoan.Size = new System.Drawing.Size(784, 35);
            this.pnlHuyThanhtoan.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(418, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 24);
            this.label9.TabIndex = 371;
            this.label9.Text = "Đã thu:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTongtienDCT
            // 
            this.txtTongtienDCT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtienDCT.Location = new System.Drawing.Point(157, 53);
            this.txtTongtienDCT.Name = "txtTongtienDCT";
            this.txtTongtienDCT.Size = new System.Drawing.Size(215, 21);
            this.txtTongtienDCT.TabIndex = 373;
            this.txtTongtienDCT.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // txtsotiendathu
            // 
            this.txtsotiendathu.BackColor = System.Drawing.Color.White;
            this.txtsotiendathu.ButtonFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtsotiendathu.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtsotiendathu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtsotiendathu.Location = new System.Drawing.Point(572, 37);
            this.txtsotiendathu.Name = "txtsotiendathu";
            this.txtsotiendathu.ReadOnly = true;
            this.txtsotiendathu.Size = new System.Drawing.Size(200, 29);
            this.txtsotiendathu.TabIndex = 370;
            this.txtsotiendathu.Tag = "NO";
            this.txtsotiendathu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtsotiendathu.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // lblNgaythanhtoan
            // 
            this.lblNgaythanhtoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaythanhtoan.ForeColor = System.Drawing.Color.Navy;
            this.lblNgaythanhtoan.Location = new System.Drawing.Point(3, 5);
            this.lblNgaythanhtoan.Name = "lblNgaythanhtoan";
            this.lblNgaythanhtoan.Size = new System.Drawing.Size(143, 24);
            this.lblNgaythanhtoan.TabIndex = 27;
            this.lblNgaythanhtoan.Text = "Ngày thanh toán:";
            this.lblNgaythanhtoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBNPhaiTra
            // 
            this.txtBNPhaiTra.BackColor = System.Drawing.Color.White;
            this.txtBNPhaiTra.ButtonFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtBNPhaiTra.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtBNPhaiTra.Location = new System.Drawing.Point(572, 5);
            this.txtBNPhaiTra.Name = "txtBNPhaiTra";
            this.txtBNPhaiTra.Size = new System.Drawing.Size(200, 29);
            this.txtBNPhaiTra.TabIndex = 347;
            this.txtBNPhaiTra.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtBNPhaiTra.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(3, 50);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(143, 24);
            this.label37.TabIndex = 372;
            this.label37.Text = "Tổng tiền Đồng chi trả";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(418, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(143, 31);
            this.label14.TabIndex = 346;
            this.label14.Text = "Tổng bệnh nhân trả (1+2+3):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 24);
            this.label15.TabIndex = 348;
            this.label15.Text = "BHYT chi trả :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBNCT
            // 
            this.txtBNCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBNCT.ButtonFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBNCT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBNCT.Location = new System.Drawing.Point(157, 101);
            this.txtBNCT.Name = "txtBNCT";
            this.txtBNCT.Size = new System.Drawing.Size(215, 21);
            this.txtBNCT.TabIndex = 345;
            this.txtBNCT.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // txtBHCT
            // 
            this.txtBHCT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHCT.Location = new System.Drawing.Point(214, 77);
            this.txtBHCT.Name = "txtBHCT";
            this.txtBHCT.Size = new System.Drawing.Size(158, 21);
            this.txtBHCT.TabIndex = 349;
            this.txtBHCT.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // txtPtramBHChiTra
            // 
            this.txtPtramBHChiTra.BackColor = System.Drawing.Color.White;
            this.txtPtramBHChiTra.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtPtramBHChiTra.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPtramBHChiTra.Location = new System.Drawing.Point(157, 77);
            this.txtPtramBHChiTra.Name = "txtPtramBHChiTra";
            this.txtPtramBHChiTra.ReadOnly = true;
            this.txtPtramBHChiTra.Size = new System.Drawing.Size(55, 21);
            this.txtPtramBHChiTra.TabIndex = 368;
            this.txtPtramBHChiTra.Text = "80%";
            this.txtPtramBHChiTra.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(143, 24);
            this.label13.TabIndex = 344;
            this.label13.Text = "Bệnh nhân chi trả (1):";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhuThu
            // 
            this.txtPhuThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPhuThu.ButtonFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuThu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuThu.Location = new System.Drawing.Point(157, 124);
            this.txtPhuThu.Name = "txtPhuThu";
            this.txtPhuThu.Size = new System.Drawing.Size(215, 21);
            this.txtPhuThu.TabIndex = 341;
            this.txtPhuThu.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtPaymentDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtPaymentDate.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtPaymentDate.DropDownCalendar.Name = "";
            this.dtPaymentDate.Enabled = false;
            this.dtPaymentDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPaymentDate.Location = new System.Drawing.Point(157, 6);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.ShowUpDown = true;
            this.dtPaymentDate.Size = new System.Drawing.Size(215, 21);
            this.dtPaymentDate.TabIndex = 363;
            this.dtPaymentDate.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 24);
            this.label10.TabIndex = 340;
            this.label10.Text = "Phụ thu (2):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongtien
            // 
            this.lblTongtien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongtien.Location = new System.Drawing.Point(3, 27);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(143, 24);
            this.lblTongtien.TabIndex = 0;
            this.lblTongtien.Text = "Tổng tiền ";
            this.lblTongtien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 24);
            this.label11.TabIndex = 342;
            this.label11.Text = "Tự túc (3):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTongChiPhi
            // 
            this.txtTongChiPhi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongChiPhi.Location = new System.Drawing.Point(157, 30);
            this.txtTongChiPhi.Name = "txtTongChiPhi";
            this.txtTongChiPhi.Size = new System.Drawing.Size(215, 21);
            this.txtTongChiPhi.TabIndex = 337;
            this.txtTongChiPhi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // txtTuTuc
            // 
            this.txtTuTuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTuTuc.ButtonFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTuTuc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTuTuc.Location = new System.Drawing.Point(157, 147);
            this.txtTuTuc.Name = "txtTuTuc";
            this.txtTuTuc.Size = new System.Drawing.Size(215, 21);
            this.txtTuTuc.TabIndex = 343;
            this.txtTuTuc.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdPaymentDetail);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(784, 353);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết các dịch vụ đã thanh toán cho lần thanh toán đang chọn";
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
            this.grdPaymentDetail.Size = new System.Drawing.Size(778, 333);
            this.grdPaymentDetail.TabIndex = 116;
            this.grdPaymentDetail.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdPaymentDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPaymentDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPaymentDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_HuyThanhtoan_Quaythuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_HuyThanhtoan_Quaythuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hủy thông tin thanh toán";
            this.Load += new System.EventHandler(this.frm_HuyThanhtoan_Quaythuoc_Load);
            this.pnlInfor.ResumeLayout(false);
            this.pnlInfor.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlInhoadon.ResumeLayout(false);
            this.pnlInhoadon.PerformLayout();
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
        private Janus.Windows.EditControls.UIButton cmdInBienlaiTonghop;
        private System.Windows.Forms.Label label38;
        private Janus.Windows.GridEX.EditControls.EditBox txtDachietkhau;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongtienDCT;
        private Janus.Windows.GridEX.EditControls.EditBox txtsotiendathu;
        private System.Windows.Forms.Label lblNgaythanhtoan;
        private Janus.Windows.GridEX.EditControls.EditBox txtBNPhaiTra;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private Janus.Windows.GridEX.EditControls.EditBox txtBNCT;
        private Janus.Windows.GridEX.EditControls.EditBox txtBHCT;
        private Janus.Windows.GridEX.EditControls.EditBox txtPtramBHChiTra;
        private System.Windows.Forms.Label label13;
        private Janus.Windows.GridEX.EditControls.EditBox txtPhuThu;
        private Janus.Windows.CalendarCombo.CalendarCombo dtPaymentDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTongtien;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongChiPhi;
        private Janus.Windows.GridEX.EditControls.EditBox txtTuTuc;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtmNgayInPhoiBHYT;
        private Janus.Windows.EditControls.UIButton cmdClose1;
        public Janus.Windows.GridEX.EditControls.EditBox txtSoTienCanNop;
        private Janus.Windows.GridEX.GridEX grdPaymentDetail;
    }
}