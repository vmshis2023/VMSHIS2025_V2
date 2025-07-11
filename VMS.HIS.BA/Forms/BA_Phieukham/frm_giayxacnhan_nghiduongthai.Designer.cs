

using VMS.HIS.UI.EMR.Ucs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_giayxacnhan_nghiduongthai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_giayxacnhan_nghiduongthai));
            this.chkCloseAfterSave = new System.Windows.Forms.CheckBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdInphieu = new Janus.Windows.EditControls.UIButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v1();
            this.uc_tt25_giayxacnhan_nghiduongthai1 = new VMS.HIS.UI.EMR.Ucs.uc_tt25_giayxacnhan_nghiduongthai();
            this.SuspendLayout();
            // 
            // chkCloseAfterSave
            // 
            this.chkCloseAfterSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCloseAfterSave.AutoSize = true;
            this.chkCloseAfterSave.BackColor = System.Drawing.Color.Transparent;
            this.chkCloseAfterSave.Checked = true;
            this.chkCloseAfterSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCloseAfterSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseAfterSave.Location = new System.Drawing.Point(154, 501);
            this.chkCloseAfterSave.Name = "chkCloseAfterSave";
            this.chkCloseAfterSave.Size = new System.Drawing.Size(232, 20);
            this.chkCloseAfterSave.TabIndex = 8;
            this.chkCloseAfterSave.TabStop = false;
            this.chkCloseAfterSave.Tag = "tt25_giayxacnhan_nghiduongthai_closeaftersave";
            this.chkCloseAfterSave.Text = "Thoát form sau khi lưu thành công?";
            this.chkCloseAfterSave.UseVisualStyleBackColor = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(585, 488);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(711, 488);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdInphieu
            // 
            this.cmdInphieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInphieu.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInphieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInphieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInphieu.Image")));
            this.cmdInphieu.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInphieu.Location = new System.Drawing.Point(459, 487);
            this.cmdInphieu.Name = "cmdInphieu";
            this.cmdInphieu.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInphieu.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInphieu.Size = new System.Drawing.Size(120, 35);
            this.cmdInphieu.TabIndex = 6;
            this.cmdInphieu.Text = "In phiếu";
            this.cmdInphieu.Click += new System.EventHandler(this.cmdInphieu_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(151, 433);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(664, 37);
            this.lblMsg.TabIndex = 2585;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(852, 204);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 2587;
            // 
            // uc_tt25_giayxacnhan_nghiduongthai1
            // 
            this.uc_tt25_giayxacnhan_nghiduongthai1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uc_tt25_giayxacnhan_nghiduongthai1.Location = new System.Drawing.Point(0, 204);
            this.uc_tt25_giayxacnhan_nghiduongthai1.Name = "uc_tt25_giayxacnhan_nghiduongthai1";
            this.uc_tt25_giayxacnhan_nghiduongthai1.Size = new System.Drawing.Size(852, 228);
            this.uc_tt25_giayxacnhan_nghiduongthai1.TabIndex = 2586;
            // 
            // frm_giayxacnhan_nghiduongthai
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(852, 535);
            this.Controls.Add(this.uc_tt25_giayxacnhan_nghiduongthai1);
            this.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdInphieu);
            this.Controls.Add(this.chkCloseAfterSave);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_giayxacnhan_nghiduongthai";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giấy chứng nhận nghỉ dưỡng thai";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkCloseAfterSave;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdInphieu;
        private System.Windows.Forms.Label lblMsg;
        private uc_tt25_giayxacnhan_nghiduongthai uc_tt25_giayxacnhan_nghiduongthai1;
        public VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v1 ucThongtinnguoibenh_emr_basic1;
    }
}