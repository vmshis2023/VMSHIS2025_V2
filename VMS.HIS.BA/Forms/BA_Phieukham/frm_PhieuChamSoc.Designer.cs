

using VMS.HIS.UI.EMR.Ucs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_PhieuChamSoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuChamSoc));
            this.lblMsg = new System.Windows.Forms.Label();
            this.ucThongtinnguoibenh_emr_basic1 = new VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2();
            this.cmdInphieu = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdThemMoiPhieuChamSoc = new Janus.Windows.EditControls.UIButton();
            this.cmdSua = new Janus.Windows.EditControls.UIButton();
            this.uc_PhieuChamSoc1 = new VMS.HIS.UI.EMR.Ucs.uc_PhieuChamSoc();
            this.uc_phieu_nhap_vien1 = new VMS.HIS.EMR.Forms.BA_Phieukham.Ucs.uc_phieu_nhap_vien();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(424, 721);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(415, 45);
            this.lblMsg.TabIndex = 2585;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucThongtinnguoibenh_emr_basic1
            // 
            this.ucThongtinnguoibenh_emr_basic1.AutoScroll = true;
            this.ucThongtinnguoibenh_emr_basic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucThongtinnguoibenh_emr_basic1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucThongtinnguoibenh_emr_basic1.Location = new System.Drawing.Point(0, 0);
            this.ucThongtinnguoibenh_emr_basic1.Name = "ucThongtinnguoibenh_emr_basic1";
            this.ucThongtinnguoibenh_emr_basic1.Size = new System.Drawing.Size(421, 775);
            this.ucThongtinnguoibenh_emr_basic1.TabIndex = 2586;
            this.ucThongtinnguoibenh_emr_basic1.TabStop = false;
            // 
            // cmdInphieu
            // 
            this.cmdInphieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInphieu.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdInphieu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInphieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInphieu.Image")));
            this.cmdInphieu.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInphieu.Location = new System.Drawing.Point(845, 728);
            this.cmdInphieu.Name = "cmdInphieu";
            this.cmdInphieu.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdInphieu.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdInphieu.Size = new System.Drawing.Size(120, 35);
            this.cmdInphieu.TabIndex = 71;
            this.cmdInphieu.Text = "In phiếu";
            this.cmdInphieu.Click += new System.EventHandler(this.cmdInphieu_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Enabled = false;
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(1223, 728);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 70;
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
            this.cmdExit.Location = new System.Drawing.Point(1349, 728);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.TabStop = false;
            this.cmdExit.Tag = "74";
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdThemMoiPhieuChamSoc
            // 
            this.cmdThemMoiPhieuChamSoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThemMoiPhieuChamSoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemMoiPhieuChamSoc.Image = global::VMS.HIS.EMR.Properties.Resources.add_04_32;
            this.cmdThemMoiPhieuChamSoc.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdThemMoiPhieuChamSoc.Location = new System.Drawing.Point(971, 728);
            this.cmdThemMoiPhieuChamSoc.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmdThemMoiPhieuChamSoc.Name = "cmdThemMoiPhieuChamSoc";
            this.cmdThemMoiPhieuChamSoc.Size = new System.Drawing.Size(120, 35);
            this.cmdThemMoiPhieuChamSoc.TabIndex = 2590;
            this.cmdThemMoiPhieuChamSoc.Tag = "72";
            this.cmdThemMoiPhieuChamSoc.Text = "Thêm mới";
            this.cmdThemMoiPhieuChamSoc.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cmdThemMoiPhieuChamSoc.Click += new System.EventHandler(this.cmdThemMoiPhieuChamSoc_Click);
            // 
            // cmdSua
            // 
            this.cmdSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSua.Enabled = false;
            this.cmdSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSua.Location = new System.Drawing.Point(1097, 728);
            this.cmdSua.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(120, 35);
            this.cmdSua.TabIndex = 2591;
            this.cmdSua.Tag = "73";
            this.cmdSua.Text = "Sửa";
            this.cmdSua.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cmdSua.Click += new System.EventHandler(this.cmdSua_Click);
            // 
            // uc_PhieuChamSoc1
            // 
            this.uc_PhieuChamSoc1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uc_PhieuChamSoc1.AutoScroll = true;
            this.uc_PhieuChamSoc1.Location = new System.Drawing.Point(427, 0);
            this.uc_PhieuChamSoc1.Name = "uc_PhieuChamSoc1";
            this.uc_PhieuChamSoc1.Size = new System.Drawing.Size(1058, 718);
            this.uc_PhieuChamSoc1.TabIndex = 2589;
            this.uc_PhieuChamSoc1.TabStop = false;
            // 
            // uc_phieu_nhap_vien1
            // 
            this.uc_phieu_nhap_vien1.Location = new System.Drawing.Point(0, 499);
            this.uc_phieu_nhap_vien1.Name = "uc_phieu_nhap_vien1";
            this.uc_phieu_nhap_vien1.Size = new System.Drawing.Size(410, 276);
            this.uc_phieu_nhap_vien1.TabIndex = 2588;
            this.uc_phieu_nhap_vien1.TabStop = false;
            // 
            // frm_PhieuChamSoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1490, 775);
            this.Controls.Add(this.cmdSua);
            this.Controls.Add(this.cmdThemMoiPhieuChamSoc);
            this.Controls.Add(this.uc_PhieuChamSoc1);
            this.Controls.Add(this.uc_phieu_nhap_vien1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdInphieu);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.ucThongtinnguoibenh_emr_basic1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frm_PhieuChamSoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu chăm sóc";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
       
        private Janus.Windows.EditControls.UIButton cmdInphieu;
        private System.Windows.Forms.Label lblMsg;
        public VNS.HIS.UI.Forms.Dungchung.UCs.ucThongtinnguoibenh_emr_basic_v2 ucThongtinnguoibenh_emr_basic1;
        private HIS.EMR.Forms.BA_Phieukham.Ucs.uc_phieu_nhap_vien uc_phieu_nhap_vien1;
        private uc_PhieuChamSoc uc_PhieuChamSoc1;
        private Janus.Windows.EditControls.UIButton cmdThemMoiPhieuChamSoc;
        private Janus.Windows.EditControls.UIButton cmdSua;
    }
}