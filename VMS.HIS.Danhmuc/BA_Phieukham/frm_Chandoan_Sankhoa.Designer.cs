
using VMS.EMR.PHIEUKHAM.Ucs;
namespace VMS.EMR.PHIEUKHAM
{
    partial class frm_Chandoan_Sankhoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Chandoan_Sankhoa));
            this.chkCloseAfterSave = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uc_chandoan_sankhoa1 = new VMS.EMR.PHIEUKHAM.Ucs.uc_chandoan_sankhoa();
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
            this.chkCloseAfterSave.Location = new System.Drawing.Point(165, 200);
            this.chkCloseAfterSave.Name = "chkCloseAfterSave";
            this.chkCloseAfterSave.Size = new System.Drawing.Size(232, 20);
            this.chkCloseAfterSave.TabIndex = 169;
            this.chkCloseAfterSave.TabStop = false;
            this.chkCloseAfterSave.Tag = "emr_chandoansankhoa_closeaftersave";
            this.chkCloseAfterSave.Text = "Thoát form sau khi lưu thành công?";
            this.chkCloseAfterSave.UseVisualStyleBackColor = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(171, 182);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 15);
            this.lblMsg.TabIndex = 170;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(549, 186);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(129, 35);
            this.cmdSave.TabIndex = 167;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Nhấn vào đây để lưu thông tin bệnh nhân";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(684, 186);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 35);
            this.cmdExit.TabIndex = 168;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uc_chandoan_sankhoa1
            // 
            this.uc_chandoan_sankhoa1.Location = new System.Drawing.Point(3, 3);
            this.uc_chandoan_sankhoa1.Name = "uc_chandoan_sankhoa1";
            this.uc_chandoan_sankhoa1.Size = new System.Drawing.Size(822, 157);
            this.uc_chandoan_sankhoa1.TabIndex = 171;
            // 
            // frm_Chandoan_Sankhoa
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(825, 233);
            this.Controls.Add(this.uc_chandoan_sankhoa1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.chkCloseAfterSave);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Chandoan_Sankhoa";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chẩn đoán sản khoa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkCloseAfterSave;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lblMsg;
        private uc_chandoan_sankhoa uc_chandoan_sankhoa1;
    }
}