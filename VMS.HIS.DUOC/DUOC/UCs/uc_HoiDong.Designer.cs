
namespace VMS.HIS.Duoc.DUOC.UCs
{
    partial class uc_HoiDong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Janus.Windows.GridEX.GridEXLayout grd_thanhvien_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_HoiDong));
            this.cmd_quanly_chucvu = new Janus.Windows.EditControls.UIButton();
            this.txt_uuid = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.grd_thanhvien = new Janus.Windows.GridEX.GridEX();
            this.nmr_stt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txt_Id = new System.Windows.Forms.TextBox();
            this.cmd_them = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cbo_chucvu = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cbo_thanhvien = new VNS.HIS.UCs.EasyCompletionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grd_thanhvien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_stt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmd_quanly_chucvu
            // 
            this.cmd_quanly_chucvu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_quanly_chucvu.Font = new System.Drawing.Font("Arial", 9F);
            this.cmd_quanly_chucvu.Image = global::VMS.HIS.Duoc.Properties.Resources.Add32;
            this.cmd_quanly_chucvu.Location = new System.Drawing.Point(355, 48);
            this.cmd_quanly_chucvu.Name = "cmd_quanly_chucvu";
            this.cmd_quanly_chucvu.Size = new System.Drawing.Size(24, 24);
            this.cmd_quanly_chucvu.TabIndex = 603;
            this.cmd_quanly_chucvu.Click += new System.EventHandler(this.cmd_quanly_chucvu_Click);
            // 
            // txt_uuid
            // 
            this.txt_uuid.Location = new System.Drawing.Point(38, 223);
            this.txt_uuid.Name = "txt_uuid";
            this.txt_uuid.Size = new System.Drawing.Size(42, 20);
            this.txt_uuid.TabIndex = 602;
            this.txt_uuid.TabStop = false;
            this.txt_uuid.Visible = false;
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.Navy;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(559, 22);
            this.lbl_title.TabIndex = 601;
            this.lbl_title.Text = "DANH SÁCH THÀNH VIÊN";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_msg
            // 
            this.lbl_msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_msg.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(18, 202);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(405, 35);
            this.lbl_msg.TabIndex = 600;
            // 
            // grd_thanhvien
            // 
            this.grd_thanhvien.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_thanhvien.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grd_thanhvien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grd_thanhvien_DesignTimeLayout.LayoutString = resources.GetString("grd_thanhvien_DesignTimeLayout.LayoutString");
            this.grd_thanhvien.DesignTimeLayout = grd_thanhvien_DesignTimeLayout;
            this.grd_thanhvien.Font = new System.Drawing.Font("Arial", 9F);
            this.grd_thanhvien.GroupByBoxVisible = false;
            this.grd_thanhvien.Location = new System.Drawing.Point(86, 78);
            this.grd_thanhvien.Name = "grd_thanhvien";
            this.grd_thanhvien.Size = new System.Drawing.Size(463, 118);
            this.grd_thanhvien.TabIndex = 599;
            this.grd_thanhvien.TabStop = false;
            // 
            // nmr_stt
            // 
            this.nmr_stt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nmr_stt.Font = new System.Drawing.Font("Arial", 9F);
            this.nmr_stt.Location = new System.Drawing.Point(419, 48);
            this.nmr_stt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmr_stt.Name = "nmr_stt";
            this.nmr_stt.Size = new System.Drawing.Size(43, 21);
            this.nmr_stt.TabIndex = 595;
            this.nmr_stt.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(375, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 23);
            this.label2.TabIndex = 598;
            this.label2.Text = "STT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 597;
            this.label1.Text = "Chức vụ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F);
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 18);
            this.label12.TabIndex = 596;
            this.label12.Text = "Họ tên:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txt_Id
            // 
            this.txt_Id.Location = new System.Drawing.Point(42, 263);
            this.txt_Id.Name = "txt_Id";
            this.txt_Id.Size = new System.Drawing.Size(42, 20);
            this.txt_Id.TabIndex = 604;
            this.txt_Id.TabStop = false;
            this.txt_Id.Visible = false;
            // 
            // cmd_them
            // 
            this.cmd_them.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_them.Font = new System.Drawing.Font("Arial", 9F);
            this.cmd_them.Image = ((System.Drawing.Image)(resources.GetObject("cmd_them.Image")));
            this.cmd_them.ImageSize = new System.Drawing.Size(22, 22);
            this.cmd_them.Location = new System.Drawing.Point(469, 24);
            this.cmd_them.Name = "cmd_them";
            this.cmd_them.Size = new System.Drawing.Size(80, 46);
            this.cmd_them.TabIndex = 592;
            this.cmd_them.Text = "Thêm";
            this.cmd_them.Click += new System.EventHandler(this.cmd_them_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdSave.Location = new System.Drawing.Point(429, 202);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 593;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cbo_chucvu
            // 
            this.cbo_chucvu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_chucvu.Font = new System.Drawing.Font("Arial", 9F);
            this.cbo_chucvu.FormattingEnabled = true;
            this.cbo_chucvu.Location = new System.Drawing.Point(86, 49);
            this.cbo_chucvu.Name = "cbo_chucvu";
            this.cbo_chucvu.Next_Control = null;
            this.cbo_chucvu.RaiseEnterEventWhenInvisible = true;
            this.cbo_chucvu.Size = new System.Drawing.Size(263, 23);
            this.cbo_chucvu.TabIndex = 591;
            // 
            // cbo_thanhvien
            // 
            this.cbo_thanhvien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_thanhvien.Font = new System.Drawing.Font("Arial", 9F);
            this.cbo_thanhvien.FormattingEnabled = true;
            this.cbo_thanhvien.Location = new System.Drawing.Point(86, 23);
            this.cbo_thanhvien.Name = "cbo_thanhvien";
            this.cbo_thanhvien.Next_Control = null;
            this.cbo_thanhvien.RaiseEnterEventWhenInvisible = true;
            this.cbo_thanhvien.Size = new System.Drawing.Size(377, 23);
            this.cbo_thanhvien.TabIndex = 590;
            // 
            // uc_HoiDong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cmd_quanly_chucvu);
            this.Controls.Add(this.txt_uuid);
            this.Controls.Add(this.cmd_them);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grd_thanhvien);
            this.Controls.Add(this.nmr_stt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_Id);
            this.Controls.Add(this.cbo_chucvu);
            this.Controls.Add(this.cbo_thanhvien);
            this.Name = "uc_HoiDong";
            this.Size = new System.Drawing.Size(559, 243);
            ((System.ComponentModel.ISupportInitialize)(this.grd_thanhvien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmr_stt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmd_quanly_chucvu;
        private System.Windows.Forms.TextBox txt_uuid;
        private Janus.Windows.EditControls.UIButton cmd_them;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label lbl_msg;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.GridEX.GridEX grd_thanhvien;
        private System.Windows.Forms.NumericUpDown nmr_stt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txt_Id;
        private VNS.HIS.UCs.EasyCompletionComboBox cbo_chucvu;
        private VNS.HIS.UCs.EasyCompletionComboBox cbo_thanhvien;
    }
}
