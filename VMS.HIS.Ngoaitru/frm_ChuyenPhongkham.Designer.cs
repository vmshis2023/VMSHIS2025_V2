using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_ChuyenPhongkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChuyenPhongkham));
            this.label21 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblLydo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdDangkyKCB = new Janus.Windows.EditControls.UIButton();
            this.cmdChuyen = new Janus.Windows.EditControls.UIButton();
            this.optThem = new System.Windows.Forms.RadioButton();
            this.optChange = new System.Windows.Forms.RadioButton();
            this.cmdInPhieukhamchuyenkhoa = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.vbLine8 = new VNS.UCs.VBLine();
            this.autoComplete_Congkham = new VNS.HIS.UCs.AutoCompleteTextbox_Congkham();
            this.txtCongkhamCu = new System.Windows.Forms.TextBox();
            this.txtPKcu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPKMoi = new System.Windows.Forms.TextBox();
            this.lblPKMoi = new System.Windows.Forms.Label();
            this.chkThutienkhamsau = new Janus.Windows.EditControls.UICheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(36, 255);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 21);
            this.label21.TabIndex = 550;
            this.label21.Text = "Công khám mới:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Navy;
            this.label27.Location = new System.Drawing.Point(27, 207);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(108, 21);
            this.label27.TabIndex = 549;
            this.label27.Text = "Công khám cũ:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(859, 91);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(136, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(634, 44);
            this.label2.TabIndex = 542;
            this.label2.Text = "Khi công khám này chưa thanh toán thì có thể chuyển sang các công khám khác. Nếu " +
    "đã thanh toán thì chỉ được chọn các công khám có cùng đơn giá.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 28);
            this.label1.TabIndex = 541;
            this.label1.Text = "Thay đổi công khám";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(26, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(59, 56);
            this.panel2.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(122, 457);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(441, 26);
            this.lblMsg.TabIndex = 555;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLydo
            // 
            this.lblLydo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLydo.ForeColor = System.Drawing.Color.Red;
            this.lblLydo.Location = new System.Drawing.Point(36, 302);
            this.lblLydo.Name = "lblLydo";
            this.lblLydo.Size = new System.Drawing.Size(101, 21);
            this.lblLydo.TabIndex = 557;
            this.lblLydo.Text = "Lý do chuyển:";
            this.lblLydo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdDangkyKCB
            // 
            this.cmdDangkyKCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDangkyKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDangkyKCB.Image = global::VMS.HIS.Ngoaitru.Properties.Resources.SAVE__2_;
            this.cmdDangkyKCB.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDangkyKCB.Location = new System.Drawing.Point(463, 501);
            this.cmdDangkyKCB.Name = "cmdDangkyKCB";
            this.cmdDangkyKCB.Size = new System.Drawing.Size(134, 35);
            this.cmdDangkyKCB.TabIndex = 7;
            this.cmdDangkyKCB.Text = "Thêm công khám";
            this.toolTip1.SetToolTip(this.cmdDangkyKCB, "Ctrl+T");
            this.cmdDangkyKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdDangkyKCB.Click += new System.EventHandler(this.cmdDangkyKCB_Click);
            // 
            // cmdChuyen
            // 
            this.cmdChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyen.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyen.Image")));
            this.cmdChuyen.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChuyen.Location = new System.Drawing.Point(603, 501);
            this.cmdChuyen.Name = "cmdChuyen";
            this.cmdChuyen.Size = new System.Drawing.Size(120, 35);
            this.cmdChuyen.TabIndex = 7;
            this.cmdChuyen.Text = "Đổi công khám";
            this.toolTip1.SetToolTip(this.cmdChuyen, "Ctrl+S hoặc Ctrl+A");
            this.cmdChuyen.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdChuyen.Click += new System.EventHandler(this.cmdChuyen_Click);
            // 
            // optThem
            // 
            this.optThem.AutoSize = true;
            this.optThem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optThem.Location = new System.Drawing.Point(281, 183);
            this.optThem.Name = "optThem";
            this.optThem.Size = new System.Drawing.Size(121, 19);
            this.optThem.TabIndex = 1;
            this.optThem.TabStop = true;
            this.optThem.Text = "Thêm công khám";
            this.toolTip1.SetToolTip(this.optThem, "Chọn mục này nếu muốn thêm phòng khám");
            this.optThem.UseVisualStyleBackColor = true;
            // 
            // optChange
            // 
            this.optChange.AutoSize = true;
            this.optChange.Checked = true;
            this.optChange.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optChange.Location = new System.Drawing.Point(140, 183);
            this.optChange.Name = "optChange";
            this.optChange.Size = new System.Drawing.Size(108, 19);
            this.optChange.TabIndex = 0;
            this.optChange.TabStop = true;
            this.optChange.Text = "Đổi công khám";
            this.toolTip1.SetToolTip(this.optChange, "Chọn mục này nếu muốn đổi sang phòng khám khác");
            this.optChange.UseVisualStyleBackColor = true;
            // 
            // cmdInPhieukhamchuyenkhoa
            // 
            this.cmdInPhieukhamchuyenkhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInPhieukhamchuyenkhoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieukhamchuyenkhoa.Image")));
            this.cmdInPhieukhamchuyenkhoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieukhamchuyenkhoa.Location = new System.Drawing.Point(13, 501);
            this.cmdInPhieukhamchuyenkhoa.Name = "cmdInPhieukhamchuyenkhoa";
            this.cmdInPhieukhamchuyenkhoa.Size = new System.Drawing.Size(120, 35);
            this.cmdInPhieukhamchuyenkhoa.TabIndex = 9;
            this.cmdInPhieukhamchuyenkhoa.Text = "In phiếu chuyên khoa (F4)";
            this.cmdInPhieukhamchuyenkhoa.Visible = false;
            this.cmdInPhieukhamchuyenkhoa.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdInPhieukhamchuyenkhoa.Click += new System.EventHandler(this.cmdInPhieukhamchuyenkhoa_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Ngoaitru.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(729, 501);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydo.AddValues = true;
            this.txtLydo.AllowMultiline = false;
            this.txtLydo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.buildShortcut = false;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.cmdDropDown = null;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "-1";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.LOAI_DANHMUC = "LYDOCHUYENPHONGKHAM";
            this.txtLydo.Location = new System.Drawing.Point(140, 302);
            this.txtLydo.MaxHeight = 150;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.ShowCodeWithValue = false;
            this.txtLydo.Size = new System.Drawing.Size(713, 21);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 6;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = null;
            this.txtLydo.txtNext1 = null;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(7, 472);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(846, 22);
            this.vbLine1.TabIndex = 554;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Chọn hành động";
            // 
            // vbLine8
            // 
            this.vbLine8._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine8.BackColor = System.Drawing.Color.Transparent;
            this.vbLine8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine8.FontText = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine8.Location = new System.Drawing.Point(0, 98);
            this.vbLine8.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine8.Name = "vbLine8";
            this.vbLine8.Size = new System.Drawing.Size(846, 22);
            this.vbLine8.TabIndex = 545;
            this.vbLine8.TabStop = false;
            this.vbLine8.YourText = "Chọn công khám cần chuyển hoặc thêm mới";
            // 
            // autoComplete_Congkham
            // 
            this.autoComplete_Congkham._backcolor = System.Drawing.SystemColors.Control;
            this.autoComplete_Congkham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoComplete_Congkham.AllowedSelectPrice = false;
            this.autoComplete_Congkham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoComplete_Congkham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoComplete_Congkham.AutoCompleteList")));
            this.autoComplete_Congkham.CaseSensitive = false;
            this.autoComplete_Congkham.CompareNoID = true;
            this.autoComplete_Congkham.DefaultCode = "-1";
            this.autoComplete_Congkham.DefaultID = "-1";
            this.autoComplete_Congkham.Drug_ID = null;
            this.autoComplete_Congkham.ExtraWidth = 0;
            this.autoComplete_Congkham.ExtraWidth_Pre = 0;
            this.autoComplete_Congkham.FillValueAfterSelect = false;
            this.autoComplete_Congkham.GridView = true;
            this.autoComplete_Congkham.Location = new System.Drawing.Point(140, 256);
            this.autoComplete_Congkham.MaxHeight = 250;
            this.autoComplete_Congkham.MinTypedCharacters = 2;
            this.autoComplete_Congkham.MyCode = "-1";
            this.autoComplete_Congkham.MyID = "-1";
            this.autoComplete_Congkham.MyText = "";
            this.autoComplete_Congkham.Name = "autoComplete_Congkham";
            this.autoComplete_Congkham.RaiseEvent = true;
            this.autoComplete_Congkham.RaiseEventEnter = true;
            this.autoComplete_Congkham.RaiseEventEnterWhenEmpty = true;
            this.autoComplete_Congkham.SelectedIndex = -1;
            this.autoComplete_Congkham.Size = new System.Drawing.Size(713, 20);
            this.autoComplete_Congkham.splitChar = '@';
            this.autoComplete_Congkham.splitCharIDAndCode = '#';
            this.autoComplete_Congkham.TabIndex = 4;
            this.autoComplete_Congkham.TakeCode = false;
            this.autoComplete_Congkham.txtMyCode = null;
            this.autoComplete_Congkham.txtMyCode_Edit = null;
            this.autoComplete_Congkham.txtMyID = null;
            this.autoComplete_Congkham.txtMyID_Edit = null;
            this.autoComplete_Congkham.txtMyName = null;
            this.autoComplete_Congkham.txtMyName_Edit = null;
            this.autoComplete_Congkham.txtNext = null;
            // 
            // txtCongkhamCu
            // 
            this.txtCongkhamCu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCongkhamCu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCongkhamCu.Location = new System.Drawing.Point(140, 208);
            this.txtCongkhamCu.Name = "txtCongkhamCu";
            this.txtCongkhamCu.ReadOnly = true;
            this.txtCongkhamCu.Size = new System.Drawing.Size(713, 20);
            this.txtCongkhamCu.TabIndex = 2;
            this.txtCongkhamCu.TabStop = false;
            // 
            // txtPKcu
            // 
            this.txtPKcu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPKcu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPKcu.Location = new System.Drawing.Point(140, 230);
            this.txtPKcu.Name = "txtPKcu";
            this.txtPKcu.ReadOnly = true;
            this.txtPKcu.Size = new System.Drawing.Size(713, 20);
            this.txtPKcu.TabIndex = 3;
            this.txtPKcu.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(12, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 21);
            this.label4.TabIndex = 573;
            this.label4.Text = "Phòng khám cũ:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPKMoi
            // 
            this.txtPKMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPKMoi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPKMoi.Location = new System.Drawing.Point(140, 279);
            this.txtPKMoi.Name = "txtPKMoi";
            this.txtPKMoi.ReadOnly = true;
            this.txtPKMoi.Size = new System.Drawing.Size(713, 20);
            this.txtPKMoi.TabIndex = 5;
            // 
            // lblPKMoi
            // 
            this.lblPKMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPKMoi.ForeColor = System.Drawing.Color.Red;
            this.lblPKMoi.Location = new System.Drawing.Point(13, 278);
            this.lblPKMoi.Name = "lblPKMoi";
            this.lblPKMoi.Size = new System.Drawing.Size(122, 21);
            this.lblPKMoi.TabIndex = 575;
            this.lblPKMoi.Text = "Phòng khám mới:";
            this.lblPKMoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkThutienkhamsau
            // 
            this.chkThutienkhamsau.Checked = true;
            this.chkThutienkhamsau.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThutienkhamsau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThutienkhamsau.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkThutienkhamsau.Location = new System.Drawing.Point(140, 329);
            this.chkThutienkhamsau.Name = "chkThutienkhamsau";
            this.chkThutienkhamsau.Size = new System.Drawing.Size(162, 23);
            this.chkThutienkhamsau.TabIndex = 629;
            this.chkThutienkhamsau.TabStop = false;
            this.chkThutienkhamsau.Text = "Thu tiền khám sau?";
            this.chkThutienkhamsau.Visible = false;
            // 
            // frm_ChuyenPhongkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 561);
            this.Controls.Add(this.chkThutienkhamsau);
            this.Controls.Add(this.txtPKMoi);
            this.Controls.Add(this.lblPKMoi);
            this.Controls.Add(this.txtPKcu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCongkhamCu);
            this.Controls.Add(this.autoComplete_Congkham);
            this.Controls.Add(this.optThem);
            this.Controls.Add(this.optChange);
            this.Controls.Add(this.cmdDangkyKCB);
            this.Controls.Add(this.cmdInPhieukhamchuyenkhoa);
            this.Controls.Add(this.txtLydo);
            this.Controls.Add(this.lblLydo);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.cmdChuyen);
            this.Controls.Add(this.vbLine8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_ChuyenPhongkham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay đổi công khám";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private Janus.Windows.EditControls.UIButton cmdChuyen;
        private VNS.UCs.VBLine vbLine8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private VNS.UCs.VBLine vbLine1;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblLydo;
        private AutoCompleteTextbox_Danhmucchung txtLydo;
        private Janus.Windows.EditControls.UIButton cmdInPhieukhamchuyenkhoa;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIButton cmdDangkyKCB;
        public System.Windows.Forms.RadioButton optThem;
        public System.Windows.Forms.RadioButton optChange;
        private AutoCompleteTextbox_Congkham autoComplete_Congkham;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPKMoi;
        public System.Windows.Forms.TextBox txtPKcu;
        public System.Windows.Forms.TextBox txtCongkhamCu;
        public System.Windows.Forms.TextBox txtPKMoi;
        private Janus.Windows.EditControls.UICheckBox chkThutienkhamsau;
    }
}