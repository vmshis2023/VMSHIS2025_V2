namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_capnhat_thuoctinh_dichvu_cls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_capnhat_thuoctinh_dichvu_cls));
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.chk_Loaidvu = new System.Windows.Forms.CheckBox();
            this.cbo_loai_dichvu = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.chk_LoaiPttt = new System.Windows.Forms.CheckBox();
            this.cbo_LoaiPttt = new VNS.HIS.UCs.EasyCompletionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.chk_LoaiPttt);
            this.grpControl.Controls.Add(this.cbo_LoaiPttt);
            this.grpControl.Controls.Add(this.chk_Loaidvu);
            this.grpControl.Controls.Add(this.cbo_loai_dichvu);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(511, 264);
            this.grpControl.TabIndex = 0;
            // 
            // chk_Loaidvu
            // 
            this.chk_Loaidvu.AutoSize = true;
            this.chk_Loaidvu.Location = new System.Drawing.Point(12, 39);
            this.chk_Loaidvu.Name = "chk_Loaidvu";
            this.chk_Loaidvu.Size = new System.Drawing.Size(91, 19);
            this.chk_Loaidvu.TabIndex = 1;
            this.chk_Loaidvu.Text = "Loại dịch vụ";
            this.chk_Loaidvu.UseVisualStyleBackColor = true;
            this.chk_Loaidvu.CheckedChanged += new System.EventHandler(this.chk_Loaidvu_CheckedChanged);
            // 
            // cbo_loai_dichvu
            // 
            this.cbo_loai_dichvu.FormattingEnabled = true;
            this.cbo_loai_dichvu.Location = new System.Drawing.Point(121, 38);
            this.cbo_loai_dichvu.Name = "cbo_loai_dichvu";
            this.cbo_loai_dichvu.Next_Control = null;
            this.cbo_loai_dichvu.RaiseEnterEventWhenInvisible = true;
            this.cbo_loai_dichvu.Size = new System.Drawing.Size(364, 23);
            this.cbo_loai_dichvu.TabIndex = 2;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(379, 288);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 32);
            this.cmdExit.TabIndex = 17;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(267, 288);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 32);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // chk_LoaiPttt
            // 
            this.chk_LoaiPttt.AutoSize = true;
            this.chk_LoaiPttt.Location = new System.Drawing.Point(12, 69);
            this.chk_LoaiPttt.Name = "chk_LoaiPttt";
            this.chk_LoaiPttt.Size = new System.Drawing.Size(82, 19);
            this.chk_LoaiPttt.TabIndex = 3;
            this.chk_LoaiPttt.Text = "Loại PTTT";
            this.chk_LoaiPttt.UseVisualStyleBackColor = true;
            this.chk_LoaiPttt.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbo_LoaiPttt
            // 
            this.cbo_LoaiPttt.FormattingEnabled = true;
            this.cbo_LoaiPttt.Location = new System.Drawing.Point(121, 67);
            this.cbo_LoaiPttt.Name = "cbo_LoaiPttt";
            this.cbo_LoaiPttt.Next_Control = null;
            this.cbo_LoaiPttt.RaiseEnterEventWhenInvisible = true;
            this.cbo_LoaiPttt.Size = new System.Drawing.Size(364, 23);
            this.cbo_LoaiPttt.TabIndex = 4;
            // 
            // frm_capnhat_thuoctinh_dichvu_cls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(511, 332);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_capnhat_thuoctinh_dichvu_cls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin dịch vụ CLS";
            this.Load += new System.EventHandler(this.frm_capnhat_thuoctinh_dichvu_cls_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_capnhat_thuoctinh_dichvu_cls_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        public UCs.EasyCompletionComboBox cbo_loai_dichvu;
        public UCs.EasyCompletionComboBox cbo_LoaiPttt;
        public System.Windows.Forms.CheckBox chk_Loaidvu;
        public System.Windows.Forms.CheckBox chk_LoaiPttt;
    }
}