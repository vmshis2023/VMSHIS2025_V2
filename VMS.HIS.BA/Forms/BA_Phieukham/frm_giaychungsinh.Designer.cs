

using VMS.HIS.UI.EMR.Ucs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_giaychungsinh
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
            this.chkCloseAfterSave = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2();
            this.uc_giaychungsinh1 = new VMS.HIS.UI.EMR.Ucs.uc_giaychungsinh();
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
            this.chkCloseAfterSave.Location = new System.Drawing.Point(154, 749);
            this.chkCloseAfterSave.Name = "chkCloseAfterSave";
            this.chkCloseAfterSave.Size = new System.Drawing.Size(232, 20);
            this.chkCloseAfterSave.TabIndex = 8;
            this.chkCloseAfterSave.TabStop = false;
            this.chkCloseAfterSave.Tag = "tt25_giaychungnhan_tainanthuongtich_closeaftersave";
            this.chkCloseAfterSave.Text = "Thoát form sau khi lưu thành công?";
            this.chkCloseAfterSave.UseVisualStyleBackColor = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(12, 690);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(374, 79);
            this.lblMsg.TabIndex = 2585;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(400, 783);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 2586;
            // 
            // uc_giaychungsinh1
            // 
            this.uc_giaychungsinh1.Location = new System.Drawing.Point(406, 0);
            this.uc_giaychungsinh1.Name = "uc_giaychungsinh1";
            this.uc_giaychungsinh1.Size = new System.Drawing.Size(839, 769);
            this.uc_giaychungsinh1.TabIndex = 2587;
            // 
            // frm_giaychungsinh
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1254, 783);
            this.Controls.Add(this.uc_giaychungsinh1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.Controls.Add(this.chkCloseAfterSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_giaychungsinh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giấy chứng sinh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkCloseAfterSave;
        private System.Windows.Forms.Label lblMsg;
        public VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2 ucThongtinnguoibenh_emr_basic1;
        private uc_giaychungsinh uc_giaychungsinh1;
    }
}