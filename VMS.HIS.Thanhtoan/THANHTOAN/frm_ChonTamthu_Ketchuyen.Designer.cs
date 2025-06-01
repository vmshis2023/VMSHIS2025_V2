
namespace VMS.HIS.Thanhtoan.THANHTOAN
{
    partial class frm_ChonTamthu_Ketchuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonTamthu_Ketchuyen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.uc_tamthu1 = new VMS.HIS.Danhmuc.UserControls.uc_tamthu();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdAccept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 718);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 43);
            this.panel1.TabIndex = 1;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(1127, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdCancel.Office2007CustomColor = System.Drawing.Color.Yellow;
            this.cmdCancel.Size = new System.Drawing.Size(120, 35);
            this.cmdCancel.TabIndex = 370;
            this.cmdCancel.Text = "Hủy bỏ";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(999, 4);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdAccept.Office2007CustomColor = System.Drawing.Color.Red;
            this.cmdAccept.Size = new System.Drawing.Size(120, 35);
            this.cmdAccept.TabIndex = 371;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // uc_tamthu1
            // 
            this.uc_tamthu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_tamthu1.Location = new System.Drawing.Point(0, 0);
            this.uc_tamthu1.Name = "uc_tamthu1";
            this.uc_tamthu1.Size = new System.Drawing.Size(1264, 718);
            this.uc_tamthu1.TabIndex = 0;
            // 
            // frm_ChonTamthu_Ketchuyen
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.uc_tamthu1);
            this.Controls.Add(this.panel1);
            this.Name = "frm_ChonTamthu_Ketchuyen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn tạm thu kết chuyển thanh toán";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Danhmuc.UserControls.uc_tamthu uc_tamthu1;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.EditControls.UIButton cmdAccept;
    }
}