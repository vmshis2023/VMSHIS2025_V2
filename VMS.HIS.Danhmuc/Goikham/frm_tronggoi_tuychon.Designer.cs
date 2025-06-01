
namespace VMS.HIS.Danhmuc.Goikham
{
    partial class frm_tronggoi_tuychon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_tronggoi_tuychon));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.cboGoi = new System.Windows.Forms.ComboBox();
            this.optTronggoi = new System.Windows.Forms.RadioButton();
            this.optGoi = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 374);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 42);
            this.panel1.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(377, 6);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 30);
            this.cmdSave.TabIndex = 21;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(493, 6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(110, 30);
            this.cmdCancel.TabIndex = 22;
            this.cmdCancel.Text = "Thoát";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cboGoi
            // 
            this.cboGoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGoi.FormattingEnabled = true;
            this.cboGoi.Location = new System.Drawing.Point(37, 209);
            this.cboGoi.Name = "cboGoi";
            this.cboGoi.Size = new System.Drawing.Size(562, 24);
            this.cboGoi.TabIndex = 1;
            // 
            // optTronggoi
            // 
            this.optTronggoi.AutoSize = true;
            this.optTronggoi.Checked = true;
            this.optTronggoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTronggoi.ForeColor = System.Drawing.Color.Red;
            this.optTronggoi.Location = new System.Drawing.Point(37, 147);
            this.optTronggoi.Name = "optTronggoi";
            this.optTronggoi.Size = new System.Drawing.Size(412, 20);
            this.optTronggoi.TabIndex = 2;
            this.optTronggoi.TabStop = true;
            this.optTronggoi.Text = "Lựa chọn 1: Trong gói và không tính chi phí dịch vụ khi thanh toán?";
            this.optTronggoi.UseVisualStyleBackColor = true;
            // 
            // optGoi
            // 
            this.optGoi.AutoSize = true;
            this.optGoi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optGoi.ForeColor = System.Drawing.Color.Navy;
            this.optGoi.Location = new System.Drawing.Point(37, 181);
            this.optGoi.Name = "optGoi";
            this.optGoi.Size = new System.Drawing.Size(489, 20);
            this.optGoi.TabIndex = 3;
            this.optGoi.Text = "Lựa chọn 2: Trong gói có sẵn mà người bệnh đã đăng ký ở lần khám này";
            this.optGoi.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTitle2);
            this.panel2.Controls.Add(this.lblTitle1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 113);
            this.panel2.TabIndex = 4;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.Location = new System.Drawing.Point(141, 45);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(461, 66);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "Bạn có thể đưa các dịch vụ đang chọn vào trong một gói cụ thể mà người bệnh đã đă" +
    "ng ký trong lần khám này (Phục vụ mục đích báo cáo dịch vụ theo gói).";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTitle1.Location = new System.Drawing.Point(130, 8);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(476, 37);
            this.lblTitle1.TabIndex = 541;
            this.lblTitle1.Text = "Lựa chọn hình thức đưa dịch vụ vào trong gói";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(124, 113);
            this.panel3.TabIndex = 0;
            // 
            // frm_tronggoi_tuychon
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(611, 416);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.optGoi);
            this.Controls.Add(this.optTronggoi);
            this.Controls.Add(this.cboGoi);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_tronggoi_tuychon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lựa chọn tùy chọn trong gói";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private System.Windows.Forms.ComboBox cboGoi;
        private System.Windows.Forms.RadioButton optTronggoi;
        private System.Windows.Forms.RadioButton optGoi;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel panel3;
    }
}