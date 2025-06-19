namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_capnhat_nhombc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_capnhat_nhombc));
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboNhomBC = new VNS.HIS.UCs.EasyCompletionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(379, 136);
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
            this.cmdSave.Location = new System.Drawing.Point(267, 136);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 32);
            this.cmdSave.TabIndex = 16;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.cboNhomBC);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(511, 117);
            this.grpControl.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mã nhóm :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhomBC
            // 
            this.cboNhomBC.FormattingEnabled = true;
            this.cboNhomBC.Location = new System.Drawing.Point(121, 36);
            this.cboNhomBC.Name = "cboNhomBC";
            this.cboNhomBC.Next_Control = null;
            this.cboNhomBC.RaiseEnterEventWhenInvisible = true;
            this.cboNhomBC.Size = new System.Drawing.Size(364, 23);
            this.cboNhomBC.TabIndex = 0;
            // 
            // frm_capnhat_nhombc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(511, 180);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_capnhat_nhombc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật mã nhóm báo cáo";
            this.Load += new System.EventHandler(this.frm_capnhat_nhombc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_capnhat_nhombc_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label5;
        public UCs.EasyCompletionComboBox cboNhomBC;
    }
}