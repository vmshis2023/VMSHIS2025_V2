namespace VNS.HIS.UI.THUOC
{
    partial class frm_capnhat_thuoctinh_thuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_capnhat_thuoctinh_thuoc));
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.cbo_phanloaithuoc = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.chk_phanloaithuoc = new System.Windows.Forms.CheckBox();
            this.chk_nhomduocly = new System.Windows.Forms.CheckBox();
            this.chk_hoatchat = new System.Windows.Forms.CheckBox();
            this.chk_tinhchat = new System.Windows.Forms.CheckBox();
            this.cbo_duongdung = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cbo_nhomduocly = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cbo_hoatchat = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cbo_tinhchat = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.chk_duongdung = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(394, 368);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 32);
            this.cmdExit.TabIndex = 16;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(282, 368);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 32);
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.chk_duongdung);
            this.grpControl.Controls.Add(this.cbo_tinhchat);
            this.grpControl.Controls.Add(this.cbo_hoatchat);
            this.grpControl.Controls.Add(this.cbo_nhomduocly);
            this.grpControl.Controls.Add(this.cbo_duongdung);
            this.grpControl.Controls.Add(this.chk_tinhchat);
            this.grpControl.Controls.Add(this.chk_hoatchat);
            this.grpControl.Controls.Add(this.chk_nhomduocly);
            this.grpControl.Controls.Add(this.chk_phanloaithuoc);
            this.grpControl.Controls.Add(this.cbo_phanloaithuoc);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(526, 204);
            this.grpControl.TabIndex = 0;
            // 
            // cbo_phanloaithuoc
            // 
            this.cbo_phanloaithuoc.FormattingEnabled = true;
            this.cbo_phanloaithuoc.Location = new System.Drawing.Point(147, 38);
            this.cbo_phanloaithuoc.Name = "cbo_phanloaithuoc";
            this.cbo_phanloaithuoc.Next_Control = null;
            this.cbo_phanloaithuoc.RaiseEnterEventWhenInvisible = true;
            this.cbo_phanloaithuoc.Size = new System.Drawing.Size(364, 23);
            this.cbo_phanloaithuoc.TabIndex = 1;
            // 
            // chk_phanloaithuoc
            // 
            this.chk_phanloaithuoc.AutoSize = true;
            this.chk_phanloaithuoc.Location = new System.Drawing.Point(19, 40);
            this.chk_phanloaithuoc.Name = "chk_phanloaithuoc";
            this.chk_phanloaithuoc.Size = new System.Drawing.Size(111, 19);
            this.chk_phanloaithuoc.TabIndex = 0;
            this.chk_phanloaithuoc.Text = "Phân loại thuốc";
            this.chk_phanloaithuoc.UseVisualStyleBackColor = true;
            this.chk_phanloaithuoc.CheckedChanged += new System.EventHandler(this.chk_phanloaithuoc_CheckedChanged);
            // 
            // chk_nhomduocly
            // 
            this.chk_nhomduocly.AutoSize = true;
            this.chk_nhomduocly.Location = new System.Drawing.Point(21, 97);
            this.chk_nhomduocly.Name = "chk_nhomduocly";
            this.chk_nhomduocly.Size = new System.Drawing.Size(101, 19);
            this.chk_nhomduocly.TabIndex = 4;
            this.chk_nhomduocly.Text = "Nhóm dược lý";
            this.chk_nhomduocly.UseVisualStyleBackColor = true;
            this.chk_nhomduocly.CheckedChanged += new System.EventHandler(this.chk_nhomduocly_CheckedChanged);
            // 
            // chk_hoatchat
            // 
            this.chk_hoatchat.AutoSize = true;
            this.chk_hoatchat.Location = new System.Drawing.Point(21, 126);
            this.chk_hoatchat.Name = "chk_hoatchat";
            this.chk_hoatchat.Size = new System.Drawing.Size(78, 19);
            this.chk_hoatchat.TabIndex = 6;
            this.chk_hoatchat.Text = "Hoạt chất";
            this.chk_hoatchat.UseVisualStyleBackColor = true;
            this.chk_hoatchat.CheckedChanged += new System.EventHandler(this.chk_hoatchat_CheckedChanged);
            // 
            // chk_tinhchat
            // 
            this.chk_tinhchat.AutoSize = true;
            this.chk_tinhchat.Location = new System.Drawing.Point(21, 157);
            this.chk_tinhchat.Name = "chk_tinhchat";
            this.chk_tinhchat.Size = new System.Drawing.Size(76, 19);
            this.chk_tinhchat.TabIndex = 8;
            this.chk_tinhchat.Text = "Tính chất";
            this.chk_tinhchat.UseVisualStyleBackColor = true;
            this.chk_tinhchat.CheckedChanged += new System.EventHandler(this.chk_tinhchat_CheckedChanged);
            // 
            // cbo_duongdung
            // 
            this.cbo_duongdung.FormattingEnabled = true;
            this.cbo_duongdung.Location = new System.Drawing.Point(147, 67);
            this.cbo_duongdung.Name = "cbo_duongdung";
            this.cbo_duongdung.Next_Control = null;
            this.cbo_duongdung.RaiseEnterEventWhenInvisible = true;
            this.cbo_duongdung.Size = new System.Drawing.Size(364, 23);
            this.cbo_duongdung.TabIndex = 3;
            // 
            // cbo_nhomduocly
            // 
            this.cbo_nhomduocly.FormattingEnabled = true;
            this.cbo_nhomduocly.Location = new System.Drawing.Point(147, 96);
            this.cbo_nhomduocly.Name = "cbo_nhomduocly";
            this.cbo_nhomduocly.Next_Control = null;
            this.cbo_nhomduocly.RaiseEnterEventWhenInvisible = true;
            this.cbo_nhomduocly.Size = new System.Drawing.Size(364, 23);
            this.cbo_nhomduocly.TabIndex = 5;
            // 
            // cbo_hoatchat
            // 
            this.cbo_hoatchat.FormattingEnabled = true;
            this.cbo_hoatchat.Location = new System.Drawing.Point(147, 125);
            this.cbo_hoatchat.Name = "cbo_hoatchat";
            this.cbo_hoatchat.Next_Control = null;
            this.cbo_hoatchat.RaiseEnterEventWhenInvisible = true;
            this.cbo_hoatchat.Size = new System.Drawing.Size(364, 23);
            this.cbo_hoatchat.TabIndex = 7;
            // 
            // cbo_tinhchat
            // 
            this.cbo_tinhchat.FormattingEnabled = true;
            this.cbo_tinhchat.Location = new System.Drawing.Point(147, 154);
            this.cbo_tinhchat.Name = "cbo_tinhchat";
            this.cbo_tinhchat.Next_Control = null;
            this.cbo_tinhchat.RaiseEnterEventWhenInvisible = true;
            this.cbo_tinhchat.Size = new System.Drawing.Size(364, 23);
            this.cbo_tinhchat.TabIndex = 9;
            // 
            // chk_duongdung
            // 
            this.chk_duongdung.AutoSize = true;
            this.chk_duongdung.Location = new System.Drawing.Point(19, 71);
            this.chk_duongdung.Name = "chk_duongdung";
            this.chk_duongdung.Size = new System.Drawing.Size(94, 19);
            this.chk_duongdung.TabIndex = 2;
            this.chk_duongdung.Text = "Đường dùng";
            this.chk_duongdung.UseVisualStyleBackColor = true;
            this.chk_duongdung.CheckedChanged += new System.EventHandler(this.chk_duongdung_CheckedChanged);
            // 
            // frm_capnhat_thuoctinh_thuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(526, 412);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_capnhat_thuoctinh_thuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thuốc";
            this.Load += new System.EventHandler(this.frm_capnhat_thuoctinh_thuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_capnhat_thuoctinh_thuoc_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        public UCs.EasyCompletionComboBox cbo_phanloaithuoc;
        public UCs.EasyCompletionComboBox cbo_tinhchat;
        public UCs.EasyCompletionComboBox cbo_hoatchat;
        public UCs.EasyCompletionComboBox cbo_nhomduocly;
        public UCs.EasyCompletionComboBox cbo_duongdung;
        public System.Windows.Forms.CheckBox chk_duongdung;
        public System.Windows.Forms.CheckBox chk_tinhchat;
        public System.Windows.Forms.CheckBox chk_hoatchat;
        public System.Windows.Forms.CheckBox chk_nhomduocly;
        public System.Windows.Forms.CheckBox chk_phanloaithuoc;
    }
}