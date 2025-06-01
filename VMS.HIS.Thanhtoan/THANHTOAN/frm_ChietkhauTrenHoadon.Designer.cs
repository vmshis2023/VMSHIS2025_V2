using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_ChietkhauTrenHoadon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChietkhauTrenHoadon));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.diachi = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkBoChitiet = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCKChitiet = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtTongtienchietkhau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTongtienBN = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTiensauCK = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPtramCK = new System.Windows.Forms.NumericUpDown();
            this.txtTienChietkhau = new System.Windows.Forms.NumericUpDown();
            this.txtTongtienCuoicung = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.autoUudai = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtLydochietkhau = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPtramCK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienChietkhau)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 63);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 21);
            this.label2.TabIndex = 542;
            this.label2.Text = "Nhập tiền(hoặc % chiết khấu) và lý do chiết khấu trước khi nhấn nút Chấp nhận";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 21);
            this.label1.TabIndex = 541;
            this.label1.Text = "Chiết khấu tiền trên toàn hóa đơn";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 63);
            this.panel2.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(478, 505);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(609, 505);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Hủy bỏ";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // diachi
            // 
            this.diachi.BackColor = System.Drawing.Color.Transparent;
            this.diachi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diachi.Location = new System.Drawing.Point(0, 107);
            this.diachi.Name = "diachi";
            this.diachi.Size = new System.Drawing.Size(192, 21);
            this.diachi.TabIndex = 537;
            this.diachi.Text = "Tổng tiền chiết khấu chi tiết (1):";
            this.diachi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // chkBoChitiet
            // 
            this.chkBoChitiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBoChitiet.AutoSize = true;
            this.chkBoChitiet.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.chkBoChitiet.Location = new System.Drawing.Point(537, 107);
            this.chkBoChitiet.Name = "chkBoChitiet";
            this.chkBoChitiet.Size = new System.Drawing.Size(192, 22);
            this.chkBoChitiet.TabIndex = 0;
            this.chkBoChitiet.TabStop = false;
            this.chkBoChitiet.Text = "Hủy chiết khấu chi tiết?";
            this.toolTip1.SetToolTip(this.chkBoChitiet, "Chọn mục này để không chiết khấu theo chi tiết nữa. Sau khi bạn nhấn đồng ý, toàn" +
        " bộ chiết khấu chi tiết sẽ được hệ thống bỏ đi");
            this.chkBoChitiet.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.chkAll.Location = new System.Drawing.Point(324, 195);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(361, 22);
            this.chkAll.TabIndex = 559;
            this.chkAll.TabStop = false;
            this.chkAll.Text = "Tính chiết khấu dựa trên toàn bộ tiền hóa đơn?";
            this.toolTip1.SetToolTip(this.chkAll, "Chọn mục này nếu muốn tính chiết khấu dựa trên toàn bộ tiền hóa đơn thanh toán th" +
        "ay vì bỏ đi tổng tiền chiết khấu chi tiết");
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(0, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 21);
            this.label3.TabIndex = 538;
            this.label3.Text = "% chiết khấu:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(0, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 21);
            this.label4.TabIndex = 539;
            this.label4.Text = "Tổng tiền chiết khấu (3=1+2):";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(0, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 21);
            this.label5.TabIndex = 540;
            this.label5.Text = "Lý do chiết khấu:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCKChitiet
            // 
            this.txtCKChitiet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCKChitiet.BackColor = System.Drawing.Color.White;
            this.txtCKChitiet.Enabled = false;
            this.txtCKChitiet.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCKChitiet.ForeColor = System.Drawing.Color.Red;
            this.txtCKChitiet.Location = new System.Drawing.Point(198, 104);
            this.txtCKChitiet.Name = "txtCKChitiet";
            this.txtCKChitiet.ReadOnly = true;
            this.txtCKChitiet.Size = new System.Drawing.Size(333, 25);
            this.txtCKChitiet.TabIndex = 1;
            this.txtCKChitiet.TabStop = false;
            this.txtCKChitiet.Tag = "NO";
            this.txtCKChitiet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtCKChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtTongtienchietkhau
            // 
            this.txtTongtienchietkhau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongtienchietkhau.BackColor = System.Drawing.Color.White;
            this.txtTongtienchietkhau.Enabled = false;
            this.txtTongtienchietkhau.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtienchietkhau.ForeColor = System.Drawing.Color.Red;
            this.txtTongtienchietkhau.Location = new System.Drawing.Point(198, 224);
            this.txtTongtienchietkhau.Name = "txtTongtienchietkhau";
            this.txtTongtienchietkhau.ReadOnly = true;
            this.txtTongtienchietkhau.Size = new System.Drawing.Size(531, 25);
            this.txtTongtienchietkhau.TabIndex = 2;
            this.txtTongtienchietkhau.TabStop = false;
            this.txtTongtienchietkhau.Tag = "NO";
            this.txtTongtienchietkhau.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtTongtienchietkhau.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(129, 436);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(600, 27);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(0, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 21);
            this.label6.TabIndex = 547;
            this.label6.Text = "Tổng tiền chiết khấu hóa đơn(2):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(320, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 21);
            this.label7.TabIndex = 549;
            this.label7.Text = "%";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTongtienBN
            // 
            this.txtTongtienBN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongtienBN.BackColor = System.Drawing.Color.White;
            this.txtTongtienBN.Enabled = false;
            this.txtTongtienBN.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtienBN.ForeColor = System.Drawing.Color.Red;
            this.txtTongtienBN.Location = new System.Drawing.Point(198, 73);
            this.txtTongtienBN.Name = "txtTongtienBN";
            this.txtTongtienBN.ReadOnly = true;
            this.txtTongtienBN.Size = new System.Drawing.Size(333, 25);
            this.txtTongtienBN.TabIndex = 0;
            this.txtTongtienBN.TabStop = false;
            this.txtTongtienBN.Tag = "NO";
            this.txtTongtienBN.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtTongtienBN.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 21);
            this.label8.TabIndex = 550;
            this.label8.Text = "Tổng tiền bệnh nhân:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTiensauCK
            // 
            this.txtTiensauCK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTiensauCK.BackColor = System.Drawing.Color.White;
            this.txtTiensauCK.Enabled = false;
            this.txtTiensauCK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTiensauCK.ForeColor = System.Drawing.Color.Red;
            this.txtTiensauCK.Location = new System.Drawing.Point(198, 133);
            this.txtTiensauCK.Name = "txtTiensauCK";
            this.txtTiensauCK.ReadOnly = true;
            this.txtTiensauCK.Size = new System.Drawing.Size(531, 25);
            this.txtTiensauCK.TabIndex = 2;
            this.txtTiensauCK.TabStop = false;
            this.txtTiensauCK.Tag = "NO";
            this.txtTiensauCK.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtTiensauCK.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 21);
            this.label9.TabIndex = 553;
            this.label9.Text = "Tổng tiền sau chiết khấu chi tiết:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPtramCK
            // 
            this.txtPtramCK.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtPtramCK.Location = new System.Drawing.Point(198, 163);
            this.txtPtramCK.Name = "txtPtramCK";
            this.txtPtramCK.Size = new System.Drawing.Size(120, 25);
            this.txtPtramCK.TabIndex = 0;
            this.txtPtramCK.ValueChanged += new System.EventHandler(this.txtPtramCK_ValueChanged_1);
            // 
            // txtTienChietkhau
            // 
            this.txtTienChietkhau.DecimalPlaces = 2;
            this.txtTienChietkhau.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTienChietkhau.Location = new System.Drawing.Point(198, 194);
            this.txtTienChietkhau.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.txtTienChietkhau.Name = "txtTienChietkhau";
            this.txtTienChietkhau.Size = new System.Drawing.Size(120, 25);
            this.txtTienChietkhau.TabIndex = 1;
            this.txtTienChietkhau.ThousandsSeparator = true;
            // 
            // txtTongtienCuoicung
            // 
            this.txtTongtienCuoicung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongtienCuoicung.BackColor = System.Drawing.Color.White;
            this.txtTongtienCuoicung.Enabled = false;
            this.txtTongtienCuoicung.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtienCuoicung.ForeColor = System.Drawing.Color.Red;
            this.txtTongtienCuoicung.Location = new System.Drawing.Point(198, 255);
            this.txtTongtienCuoicung.Name = "txtTongtienCuoicung";
            this.txtTongtienCuoicung.ReadOnly = true;
            this.txtTongtienCuoicung.Size = new System.Drawing.Size(531, 25);
            this.txtTongtienCuoicung.TabIndex = 3;
            this.txtTongtienCuoicung.TabStop = false;
            this.txtTongtienCuoicung.Tag = "NO";
            this.txtTongtienCuoicung.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.txtTongtienCuoicung.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(0, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 21);
            this.label10.TabIndex = 558;
            this.label10.Text = "Bệnh nhân phải trả:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 318);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 21);
            this.label11.TabIndex = 561;
            this.label11.Text = "Đợt ưu đãi";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // autoUudai
            // 
            this.autoUudai._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoUudai._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoUudai._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoUudai.AddValues = true;
            this.autoUudai.AllowMultiline = false;
            this.autoUudai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoUudai.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoUudai.AutoCompleteList")));
            this.autoUudai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoUudai.buildShortcut = false;
            this.autoUudai.CaseSensitive = false;
            this.autoUudai.CompareNoID = true;
            this.autoUudai.DefaultCode = "-1";
            this.autoUudai.DefaultID = "-1";
            this.autoUudai.Drug_ID = null;
            this.autoUudai.ExtraWidth = 0;
            this.autoUudai.FillValueAfterSelect = false;
            this.autoUudai.Font = new System.Drawing.Font("Arial", 11.25F);
            this.autoUudai.LOAI_DANHMUC = "UUDAI";
            this.autoUudai.Location = new System.Drawing.Point(198, 317);
            this.autoUudai.MaxHeight = -1;
            this.autoUudai.MinTypedCharacters = 2;
            this.autoUudai.MyCode = "-1";
            this.autoUudai.MyID = "-1";
            this.autoUudai.Name = "autoUudai";
            this.autoUudai.RaiseEvent = false;
            this.autoUudai.RaiseEventEnter = false;
            this.autoUudai.RaiseEventEnterWhenEmpty = false;
            this.autoUudai.SelectedIndex = -1;
            this.autoUudai.ShowCodeWithValue = false;
            this.autoUudai.Size = new System.Drawing.Size(531, 25);
            this.autoUudai.splitChar = '@';
            this.autoUudai.splitCharIDAndCode = '#';
            this.autoUudai.TabIndex = 560;
            this.autoUudai.TakeCode = false;
            this.autoUudai.txtMyCode = null;
            this.autoUudai.txtMyCode_Edit = null;
            this.autoUudai.txtMyID = null;
            this.autoUudai.txtMyID_Edit = null;
            this.autoUudai.txtMyName = null;
            this.autoUudai.txtMyName_Edit = null;
            this.autoUudai.txtNext = null;
            this.autoUudai.txtNext1 = null;
            // 
            // txtLydochietkhau
            // 
            this.txtLydochietkhau._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLydochietkhau._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydochietkhau._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydochietkhau.AddValues = true;
            this.txtLydochietkhau.AllowMultiline = false;
            this.txtLydochietkhau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLydochietkhau.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydochietkhau.AutoCompleteList")));
            this.txtLydochietkhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydochietkhau.buildShortcut = false;
            this.txtLydochietkhau.CaseSensitive = false;
            this.txtLydochietkhau.CompareNoID = true;
            this.txtLydochietkhau.DefaultCode = "-1";
            this.txtLydochietkhau.DefaultID = "-1";
            this.txtLydochietkhau.Drug_ID = null;
            this.txtLydochietkhau.ExtraWidth = 0;
            this.txtLydochietkhau.FillValueAfterSelect = false;
            this.txtLydochietkhau.Font = new System.Drawing.Font("Arial", 11.25F);
            this.txtLydochietkhau.LOAI_DANHMUC = "LYDOCHIETKHAU";
            this.txtLydochietkhau.Location = new System.Drawing.Point(198, 286);
            this.txtLydochietkhau.MaxHeight = -1;
            this.txtLydochietkhau.MinTypedCharacters = 2;
            this.txtLydochietkhau.MyCode = "-1";
            this.txtLydochietkhau.MyID = "-1";
            this.txtLydochietkhau.Name = "txtLydochietkhau";
            this.txtLydochietkhau.RaiseEvent = false;
            this.txtLydochietkhau.RaiseEventEnter = false;
            this.txtLydochietkhau.RaiseEventEnterWhenEmpty = false;
            this.txtLydochietkhau.SelectedIndex = -1;
            this.txtLydochietkhau.ShowCodeWithValue = false;
            this.txtLydochietkhau.Size = new System.Drawing.Size(531, 25);
            this.txtLydochietkhau.splitChar = '@';
            this.txtLydochietkhau.splitCharIDAndCode = '#';
            this.txtLydochietkhau.TabIndex = 4;
            this.txtLydochietkhau.TakeCode = false;
            this.txtLydochietkhau.txtMyCode = null;
            this.txtLydochietkhau.txtMyCode_Edit = null;
            this.txtLydochietkhau.txtMyID = null;
            this.txtLydochietkhau.txtMyID_Edit = null;
            this.txtLydochietkhau.txtMyName = null;
            this.txtLydochietkhau.txtMyName_Edit = null;
            this.txtLydochietkhau.txtNext = null;
            this.txtLydochietkhau.txtNext1 = null;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(0, 467);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(771, 22);
            this.vbLine1.TabIndex = 9;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Hành động";
            // 
            // frm_ChietkhauTrenHoadon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.autoUudai);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.txtTongtienCuoicung);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTienChietkhau);
            this.Controls.Add(this.txtPtramCK);
            this.Controls.Add(this.txtTiensauCK);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkBoChitiet);
            this.Controls.Add(this.txtTongtienBN);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtLydochietkhau);
            this.Controls.Add(this.txtTongtienchietkhau);
            this.Controls.Add(this.txtCKChitiet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.diachi);
            this.KeyPreview = true;
            this.Name = "frm_ChietkhauTrenHoadon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chiết khấu hóa đơn thanh toán";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPtramCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienChietkhau)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label diachi;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongtienchietkhau;
        private Janus.Windows.GridEX.EditControls.EditBox txtCKChitiet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongtienBN;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtTiensauCK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown txtTienChietkhau;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongtienCuoicung;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.CheckBox chkBoChitiet;
        public System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label11;
        public AutoCompleteTextbox_Danhmucchung autoUudai;
        public System.Windows.Forms.NumericUpDown txtPtramCK;
        public AutoCompleteTextbox_Danhmucchung txtLydochietkhau;


    }
}