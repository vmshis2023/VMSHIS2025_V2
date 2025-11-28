

using VMS.HIS.UI.EMR.Ucs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_hoso_theodoi_sosinh
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
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2();
            this.visualStyleManager1 = new Janus.Windows.Common.VisualStyleManager(this.components);
            this.uc_phieutheodoi_tresosinh1 = new VMS.HIS.UI.EMR.Ucs.uc_phieutheodoi_tresosinh();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(1443, 764);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(0, 722);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(404, 89);
            this.lblMsg.TabIndex = 2585;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(404, 722);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 2586;
            // 
            // uc_phieutheodoi_tresosinh1
            // 
            this.uc_phieutheodoi_tresosinh1.AutoScroll = true;
            this.uc_phieutheodoi_tresosinh1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_phieutheodoi_tresosinh1.Location = new System.Drawing.Point(404, 0);
            this.uc_phieutheodoi_tresosinh1.Name = "uc_phieutheodoi_tresosinh1";
            this.uc_phieutheodoi_tresosinh1.Size = new System.Drawing.Size(1180, 811);
            this.uc_phieutheodoi_tresosinh1.TabIndex = 2590;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 811);
            this.panel1.TabIndex = 2591;
            // 
            // frm_hoso_theodoi_sosinh
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1584, 811);
            this.Controls.Add(this.uc_phieutheodoi_tresosinh1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdExit);
            this.KeyPreview = true;
            this.Name = "frm_hoso_theodoi_sosinh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hồ sơ theo dõi sơ sinh";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lblMsg;
        public VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2 ucThongtinnguoibenh_emr_basic1;
        private Janus.Windows.Common.VisualStyleManager visualStyleManager1;
        private uc_phieutheodoi_tresosinh uc_phieutheodoi_tresosinh1;
        private System.Windows.Forms.Panel panel1;
    }
}