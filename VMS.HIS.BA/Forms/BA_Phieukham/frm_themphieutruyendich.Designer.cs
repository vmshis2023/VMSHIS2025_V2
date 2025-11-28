using VNS.HIS.UCs;
using VNS.HIS.UI.Forms.Dungchung.UCs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_themphieutruyendich
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themphieutruyendich));
            this.vbLine1 = new VNS.UCs.VBLine();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblMsg = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic();
            this.txtGiuong = new System.Windows.Forms.TextBox();
            this.txtBuong = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lnkChandoan = new System.Windows.Forms.LinkLabel();
            this.txt_chandoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.chkContine = new Janus.Windows.EditControls.UICheckBox();
            this.cbo_khoanoitru = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayphieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(14, 572);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(899, 22);
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
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(327, 541);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(580, 27);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(920, 252);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 0;
            // 
            // txtGiuong
            // 
            this.txtGiuong.BackColor = System.Drawing.Color.White;
            this.txtGiuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiuong.Location = new System.Drawing.Point(749, 268);
            this.txtGiuong.Name = "txtGiuong";
            this.txtGiuong.Size = new System.Drawing.Size(110, 22);
            this.txtGiuong.TabIndex = 3;
            // 
            // txtBuong
            // 
            this.txtBuong.BackColor = System.Drawing.Color.White;
            this.txtBuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuong.Location = new System.Drawing.Point(553, 268);
            this.txtBuong.Name = "txtBuong";
            this.txtBuong.Size = new System.Drawing.Size(127, 22);
            this.txtBuong.TabIndex = 2;
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.Black;
            this.label49.Location = new System.Drawing.Point(686, 267);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(57, 21);
            this.label49.TabIndex = 264335;
            this.label49.Text = "Giường:";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(476, 268);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(71, 21);
            this.label48.TabIndex = 264334;
            this.label48.Text = "Buồng:";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(11, 267);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(136, 21);
            this.label28.TabIndex = 264333;
            this.label28.Text = "Thực hiện tại khoa:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkChandoan
            // 
            this.lnkChandoan.AutoSize = true;
            this.lnkChandoan.Location = new System.Drawing.Point(78, 325);
            this.lnkChandoan.Name = "lnkChandoan";
            this.lnkChandoan.Size = new System.Drawing.Size(60, 13);
            this.lnkChandoan.TabIndex = 264337;
            this.lnkChandoan.TabStop = true;
            this.lnkChandoan.Text = "Chẩn đoán";
            // 
            // txt_chandoan
            // 
            this.txt_chandoan.Location = new System.Drawing.Point(144, 325);
            this.txt_chandoan.Multiline = true;
            this.txt_chandoan.Name = "txt_chandoan";
            this.txt_chandoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_chandoan.Size = new System.Drawing.Size(769, 240);
            this.txt_chandoan.TabIndex = 4;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(865, 268);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(48, 22);
            this.txtId.TabIndex = 264338;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(787, 595);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(656, 595);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Lưu";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click_1);
            // 
            // chkContine
            // 
            this.chkContine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContine.ForeColor = System.Drawing.Color.Black;
            this.chkContine.Location = new System.Drawing.Point(144, 601);
            this.chkContine.Name = "chkContine";
            this.chkContine.Size = new System.Drawing.Size(244, 25);
            this.chkContine.TabIndex = 264339;
            this.chkContine.TabStop = false;
            this.chkContine.Text = "Cho phép thêm mới  liên tục?";
            // 
            // cbo_khoanoitru
            // 
            this.cbo_khoanoitru.FormattingEnabled = true;
            this.cbo_khoanoitru.Location = new System.Drawing.Point(144, 269);
            this.cbo_khoanoitru.Name = "cbo_khoanoitru";
            this.cbo_khoanoitru.Next_Control = null;
            this.cbo_khoanoitru.RaiseEnterEventWhenInvisible = true;
            this.cbo_khoanoitru.Size = new System.Drawing.Size(333, 21);
            this.cbo_khoanoitru.TabIndex = 264340;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(30, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 21);
            this.label3.TabIndex = 264342;
            this.label3.Text = "Ngày phiếu:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayphieu
            // 
            this.dtpNgayphieu.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayphieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayphieu.DropDownCalendar.Name = "";
            this.dtpNgayphieu.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtpNgayphieu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayphieu.IsNullDate = true;
            this.dtpNgayphieu.Location = new System.Drawing.Point(144, 296);
            this.dtpNgayphieu.Name = "dtpNgayphieu";
            this.dtpNgayphieu.ShowUpDown = true;
            this.dtpNgayphieu.Size = new System.Drawing.Size(186, 22);
            this.dtpNgayphieu.TabIndex = 264341;
            // 
            // frm_themphieutruyendich
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(920, 642);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpNgayphieu);
            this.Controls.Add(this.cbo_khoanoitru);
            this.Controls.Add(this.chkContine);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lnkChandoan);
            this.Controls.Add(this.txt_chandoan);
            this.Controls.Add(this.txtGiuong);
            this.Controls.Add(this.txtBuong);
            this.Controls.Add(this.label49);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.vbLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themphieutruyendich";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu truyền dịch";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtGiuong;
        private System.Windows.Forms.TextBox txtBuong;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.LinkLabel lnkChandoan;
        private Janus.Windows.GridEX.EditControls.EditBox txt_chandoan;
        private System.Windows.Forms.TextBox txtId;
        private Janus.Windows.EditControls.UICheckBox chkContine;
        public ucThongtinnguoibenh_emr_basic ucThongtinnguoibenh_emr_basic1;
        private EasyCompletionComboBox cbo_khoanoitru;
        private System.Windows.Forms.Label label3;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpNgayphieu;
    }
}