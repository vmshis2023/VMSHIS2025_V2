namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_XemphieuTralai_Quaythuoc
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
            Janus.Windows.GridEX.GridEXLayout grdDSThuoctralai_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_XemphieuTralai_Quaythuoc));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtDachietkhau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdClose1 = new Janus.Windows.EditControls.UIButton();
            this.cmdInphieu = new Janus.Windows.EditControls.UIButton();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmNgayInPhoiBHYT = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlInhoadon = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
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
            this.grdDSThuoctralai = new Janus.Windows.GridEX.GridEX();
            this.pnlInfor.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlInhoadon.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDSThuoctralai)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
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
            this.cmdClose1.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose1.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose1.Location = new System.Drawing.Point(602, 3);
            this.cmdClose1.Name = "cmdClose1";
            this.cmdClose1.Size = new System.Drawing.Size(131, 35);
            this.cmdClose1.TabIndex = 23;
            this.cmdClose1.Text = "Thoát (Esc)";
            this.toolTip1.SetToolTip(this.cmdClose1, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            // 
            // cmdInphieu
            // 
            this.cmdInphieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdInphieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInphieu.Image = global::VMS.HIS.Danhmuc.Properties.Resources.printer_32;
            this.cmdInphieu.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInphieu.Location = new System.Drawing.Point(465, 3);
            this.cmdInphieu.Name = "cmdInphieu";
            this.cmdInphieu.Size = new System.Drawing.Size(131, 35);
            this.cmdInphieu.TabIndex = 24;
            this.cmdInphieu.Text = "In phiếu";
            this.toolTip1.SetToolTip(this.cmdInphieu, "Nhấn vào đây để hủy bỏ việc hủy thanh toán và quay lại màn hình chính");
            this.cmdInphieu.Click += new System.EventHandler(this.cmdInphieu_Click);
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
            this.pnlInfor.Location = new System.Drawing.Point(0, 683);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(1008, 46);
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
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 2);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1008, 44);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlInhoadon
            // 
            this.pnlInhoadon.Controls.Add(this.flowLayoutPanel1);
            this.pnlInhoadon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInhoadon.Location = new System.Drawing.Point(0, 0);
            this.pnlInhoadon.Name = "pnlInhoadon";
            this.pnlInhoadon.Size = new System.Drawing.Size(1008, 44);
            this.pnlInhoadon.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdClose1);
            this.flowLayoutPanel1.Controls.Add(this.cmdInphieu);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(272, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(736, 44);
            this.flowLayoutPanel1.TabIndex = 23;
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
            this.uiGroupBox2.Controls.Add(this.grdDSThuoctralai);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 683);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết các thuốc trả lại theo phiếu";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdDSThuoctralai
            // 
            this.grdDSThuoctralai.AlternatingColors = true;
            this.grdDSThuoctralai.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdDSThuoctralai.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><RecordNavigator>Số bả" +
    "n ghi:|/</RecordNavigator></LocalizableData>";
            grdDSThuoctralai_DesignTimeLayout.LayoutString = resources.GetString("grdDSThuoctralai_DesignTimeLayout.LayoutString");
            this.grdDSThuoctralai.DesignTimeLayout = grdDSThuoctralai_DesignTimeLayout;
            this.grdDSThuoctralai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDSThuoctralai.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDSThuoctralai.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDSThuoctralai.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDSThuoctralai.Font = new System.Drawing.Font("Arial", 9F);
            this.grdDSThuoctralai.FrozenColumns = 10;
            this.grdDSThuoctralai.GroupByBoxVisible = false;
            this.grdDSThuoctralai.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdDSThuoctralai.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdDSThuoctralai.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDSThuoctralai.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdDSThuoctralai.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDSThuoctralai.Location = new System.Drawing.Point(3, 17);
            this.grdDSThuoctralai.Name = "grdDSThuoctralai";
            this.grdDSThuoctralai.RecordNavigator = true;
            this.grdDSThuoctralai.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDSThuoctralai.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdDSThuoctralai.Size = new System.Drawing.Size(1002, 663);
            this.grdDSThuoctralai.TabIndex = 116;
            this.grdDSThuoctralai.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdDSThuoctralai.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDSThuoctralai.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDSThuoctralai.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDSThuoctralai.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDSThuoctralai.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_XemphieuTralai_Quaythuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_XemphieuTralai_Quaythuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin phiếu trả lại thuốc tại quầy";
            this.Load += new System.EventHandler(this.frm_XemphieuTralai_Quaythuoc_Load);
            this.pnlInfor.ResumeLayout(false);
            this.pnlInfor.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlInhoadon.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDSThuoctralai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlInhoadon;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
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
        private Janus.Windows.GridEX.GridEX grdDSThuoctralai;
        private Janus.Windows.EditControls.UIButton cmdInphieu;
    }
}