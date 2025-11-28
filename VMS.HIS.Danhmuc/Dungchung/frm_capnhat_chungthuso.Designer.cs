using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_capnhat_chungthuso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_capnhat_chungthuso));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblName = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNgayHuyTUHU = new System.Windows.Forms.Label();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.txt_UserId = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txt_PassWord = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txt_TOTP = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtShowHidePwd = new System.Windows.Forms.Label();
            this.cmd_luu_thong_tin_lien_thong = new Janus.Windows.EditControls.UIButton();
            this.lbl_ShowHide_MatKhau_LienThong = new System.Windows.Forms.Label();
            this.txt_matkhau_bacsi_lien_thong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txt_ma_bacsi_lien_thong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.vbLine2 = new VNS.UCs.VBLine();
            this.vbLine3 = new VNS.UCs.VBLine();
            this.vbLine1 = new VNS.UCs.VBLine();
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
            this.panel1.Size = new System.Drawing.Size(609, 63);
            this.panel1.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.Location = new System.Drawing.Point(77, 33);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(464, 21);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "Nhập các thông tin chứng thư số, tài khoản liên thông đơn thuốc quốc gia,...";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(77, 9);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(464, 21);
            this.lblTitle1.TabIndex = 541;
            this.lblTitle1.Text = "CẬP NHẬT THÔNG TIN CÁ NHÂN";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 61);
            this.panel2.TabIndex = 0;
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
            this.lblName.Location = new System.Drawing.Point(36, 117);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 21);
            this.lblName.TabIndex = 540;
            this.lblName.Text = "Mật khẩu kí số";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(10, 308);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(471, 36);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 604;
            this.label1.Text = "TOTP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNgayHuyTUHU
            // 
            this.lblNgayHuyTUHU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayHuyTUHU.ForeColor = System.Drawing.Color.Red;
            this.lblNgayHuyTUHU.Location = new System.Drawing.Point(27, 92);
            this.lblNgayHuyTUHU.Name = "lblNgayHuyTUHU";
            this.lblNgayHuyTUHU.Size = new System.Drawing.Size(112, 24);
            this.lblNgayHuyTUHU.TabIndex = 607;
            this.lblNgayHuyTUHU.Text = "User kí số";
            this.lblNgayHuyTUHU.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(487, 309);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(117, 35);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Thoát";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(485, 92);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(117, 46);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Lưu";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            // 
            // txt_UserId
            // 
            this.txt_UserId.Location = new System.Drawing.Point(145, 96);
            this.txt_UserId.MaxLength = 255;
            this.txt_UserId.Name = "txt_UserId";
            this.txt_UserId.Size = new System.Drawing.Size(287, 20);
            this.txt_UserId.TabIndex = 0;
            // 
            // txt_PassWord
            // 
            this.txt_PassWord.Location = new System.Drawing.Point(145, 117);
            this.txt_PassWord.MaxLength = 255;
            this.txt_PassWord.Name = "txt_PassWord";
            this.txt_PassWord.Size = new System.Drawing.Size(287, 20);
            this.txt_PassWord.TabIndex = 1;
            // 
            // txt_TOTP
            // 
            this.txt_TOTP.Location = new System.Drawing.Point(145, 140);
            this.txt_TOTP.MaxLength = 255;
            this.txt_TOTP.Name = "txt_TOTP";
            this.txt_TOTP.Size = new System.Drawing.Size(321, 20);
            this.txt_TOTP.TabIndex = 2;
            // 
            // txtShowHidePwd
            // 
            this.txtShowHidePwd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShowHidePwd.ForeColor = System.Drawing.Color.Red;
            this.txtShowHidePwd.Image = ((System.Drawing.Image)(resources.GetObject("txtShowHidePwd.Image")));
            this.txtShowHidePwd.Location = new System.Drawing.Point(438, 115);
            this.txtShowHidePwd.Name = "txtShowHidePwd";
            this.txtShowHidePwd.Size = new System.Drawing.Size(28, 23);
            this.txtShowHidePwd.TabIndex = 628;
            this.txtShowHidePwd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtShowHidePwd.Click += new System.EventHandler(this.txtShowHidePwd_Click);
            // 
            // cmd_luu_thong_tin_lien_thong
            // 
            this.cmd_luu_thong_tin_lien_thong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_luu_thong_tin_lien_thong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_luu_thong_tin_lien_thong.Image = global::VMS.HIS.Danhmuc.Properties.Resources.SAVE1;
            this.cmd_luu_thong_tin_lien_thong.ImageSize = new System.Drawing.Size(20, 20);
            this.cmd_luu_thong_tin_lien_thong.Location = new System.Drawing.Point(485, 211);
            this.cmd_luu_thong_tin_lien_thong.Name = "cmd_luu_thong_tin_lien_thong";
            this.cmd_luu_thong_tin_lien_thong.Size = new System.Drawing.Size(117, 47);
            this.cmd_luu_thong_tin_lien_thong.TabIndex = 654;
            this.cmd_luu_thong_tin_lien_thong.Text = "Lưu ";
            this.cmd_luu_thong_tin_lien_thong.Click += new System.EventHandler(this.cmd_luu_thong_tin_lien_thong_Click);
            // 
            // lbl_ShowHide_MatKhau_LienThong
            // 
            this.lbl_ShowHide_MatKhau_LienThong.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_ShowHide_MatKhau_LienThong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ShowHide_MatKhau_LienThong.ForeColor = System.Drawing.Color.Red;
            this.lbl_ShowHide_MatKhau_LienThong.Image = ((System.Drawing.Image)(resources.GetObject("lbl_ShowHide_MatKhau_LienThong.Image")));
            this.lbl_ShowHide_MatKhau_LienThong.Location = new System.Drawing.Point(440, 234);
            this.lbl_ShowHide_MatKhau_LienThong.Name = "lbl_ShowHide_MatKhau_LienThong";
            this.lbl_ShowHide_MatKhau_LienThong.Size = new System.Drawing.Size(28, 23);
            this.lbl_ShowHide_MatKhau_LienThong.TabIndex = 652;
            this.lbl_ShowHide_MatKhau_LienThong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_ShowHide_MatKhau_LienThong.Click += new System.EventHandler(this.lbl_ShowHide_MatKhau_LienThong_Click);
            // 
            // txt_matkhau_bacsi_lien_thong
            // 
            this.txt_matkhau_bacsi_lien_thong.Location = new System.Drawing.Point(146, 237);
            this.txt_matkhau_bacsi_lien_thong.MaxLength = 255;
            this.txt_matkhau_bacsi_lien_thong.Name = "txt_matkhau_bacsi_lien_thong";
            this.txt_matkhau_bacsi_lien_thong.PasswordChar = '*';
            this.txt_matkhau_bacsi_lien_thong.Size = new System.Drawing.Size(288, 20);
            this.txt_matkhau_bacsi_lien_thong.TabIndex = 649;
            // 
            // txt_ma_bacsi_lien_thong
            // 
            this.txt_ma_bacsi_lien_thong.Location = new System.Drawing.Point(146, 211);
            this.txt_ma_bacsi_lien_thong.MaxLength = 255;
            this.txt_ma_bacsi_lien_thong.Name = "txt_ma_bacsi_lien_thong";
            this.txt_ma_bacsi_lien_thong.Size = new System.Drawing.Size(288, 20);
            this.txt_ma_bacsi_lien_thong.TabIndex = 648;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(18, 207);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 24);
            this.label13.TabIndex = 651;
            this.label13.Text = "Mã liên thông";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(10, 237);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(130, 21);
            this.label15.TabIndex = 650;
            this.label15.Text = "Mật khẩu liên thông";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vbLine2
            // 
            this.vbLine2._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine2.BackColor = System.Drawing.Color.Transparent;
            this.vbLine2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine2.Location = new System.Drawing.Point(1, 69);
            this.vbLine2.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine2.Name = "vbLine2";
            this.vbLine2.Size = new System.Drawing.Size(608, 22);
            this.vbLine2.TabIndex = 656;
            this.vbLine2.TabStop = false;
            this.vbLine2.YourText = "Thông tin tài khoản chữ kí số";
            // 
            // vbLine3
            // 
            this.vbLine3._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine3.BackColor = System.Drawing.Color.Transparent;
            this.vbLine3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine3.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine3.Location = new System.Drawing.Point(2, 183);
            this.vbLine3.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine3.Name = "vbLine3";
            this.vbLine3.Size = new System.Drawing.Size(608, 22);
            this.vbLine3.TabIndex = 657;
            this.vbLine3.TabStop = false;
            this.vbLine3.YourText = "Thông tin tài khoản liên thông đơn thuốc quốc gia";
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(2, 282);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(602, 22);
            this.vbLine1.TabIndex = 9;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Hành động";
            // 
            // frm_capnhat_chungthuso
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(609, 356);
            this.Controls.Add(this.vbLine3);
            this.Controls.Add(this.cmd_luu_thong_tin_lien_thong);
            this.Controls.Add(this.lbl_ShowHide_MatKhau_LienThong);
            this.Controls.Add(this.txt_matkhau_bacsi_lien_thong);
            this.Controls.Add(this.txt_ma_bacsi_lien_thong);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtShowHidePwd);
            this.Controls.Add(this.txt_TOTP);
            this.Controls.Add(this.txt_PassWord);
            this.Controls.Add(this.txt_UserId);
            this.Controls.Add(this.lblNgayHuyTUHU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.vbLine2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_capnhat_chungthuso";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin chứng thư số";
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
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNgayHuyTUHU;
        private Janus.Windows.GridEX.EditControls.EditBox txt_TOTP;
        private Janus.Windows.GridEX.EditControls.EditBox txt_PassWord;
        private Janus.Windows.GridEX.EditControls.EditBox txt_UserId;
        private System.Windows.Forms.Label txtShowHidePwd;
        private VNS.UCs.VBLine vbLine3;
        private Janus.Windows.EditControls.UIButton cmd_luu_thong_tin_lien_thong;
        private System.Windows.Forms.Label lbl_ShowHide_MatKhau_LienThong;
        private Janus.Windows.GridEX.EditControls.EditBox txt_matkhau_bacsi_lien_thong;
        private Janus.Windows.GridEX.EditControls.EditBox txt_ma_bacsi_lien_thong;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private VNS.UCs.VBLine vbLine1;
        private VNS.UCs.VBLine vbLine2;
    }
}