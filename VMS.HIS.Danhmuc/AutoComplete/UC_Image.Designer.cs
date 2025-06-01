namespace VNS.HIS.UI.Forms.HinhAnh
{
    partial class UC_Image
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Image));
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.txtSTT = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.cmdXoa = new System.Windows.Forms.Button();
            this.chkChonAnhIn = new System.Windows.Forms.CheckBox();
            this.lblMota = new System.Windows.Forms.Label();
            this.txtMota = new System.Windows.Forms.TextBox();
            this.PIC_Image = new System.Windows.Forms.PictureBox();
            this.pnlCheck = new System.Windows.Forms.Panel();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.pnlInfor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_Image)).BeginInit();
            this.pnlSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInfor
            // 
            this.pnlInfor.BackColor = System.Drawing.Color.Silver;
            this.pnlInfor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfor.Controls.Add(this.txtSTT);
            this.pnlInfor.Controls.Add(this.lblID);
            this.pnlInfor.Controls.Add(this.cmdXoa);
            this.pnlInfor.Controls.Add(this.chkChonAnhIn);
            this.pnlInfor.Controls.Add(this.lblMota);
            this.pnlInfor.Controls.Add(this.txtMota);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 127);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(173, 44);
            this.pnlInfor.TabIndex = 0;
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Location = new System.Drawing.Point(131, 21);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(36, 20);
            this.txtSTT.TabIndex = 2;
            this.txtSTT.TabStop = false;
            this.txtSTT.Text = "0";
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(108, 23);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(19, 17);
            this.lblID.TabIndex = 4;
            this.lblID.Text = "1";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblID.Visible = false;
            // 
            // cmdXoa
            // 
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.Location = new System.Drawing.Point(0, 19);
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(38, 23);
            this.cmdXoa.TabIndex = 3;
            this.cmdXoa.TabStop = false;
            this.cmdXoa.UseVisualStyleBackColor = true;
            // 
            // chkChonAnhIn
            // 
            this.chkChonAnhIn.AutoSize = true;
            this.chkChonAnhIn.Location = new System.Drawing.Point(48, 24);
            this.chkChonAnhIn.Name = "chkChonAnhIn";
            this.chkChonAnhIn.Size = new System.Drawing.Size(62, 17);
            this.chkChonAnhIn.TabIndex = 2;
            this.chkChonAnhIn.TabStop = false;
            this.chkChonAnhIn.Text = "Chọn in";
            this.chkChonAnhIn.UseVisualStyleBackColor = true;
            // 
            // lblMota
            // 
            this.lblMota.AutoSize = true;
            this.lblMota.Location = new System.Drawing.Point(11, 3);
            this.lblMota.Name = "lblMota";
            this.lblMota.Size = new System.Drawing.Size(34, 13);
            this.lblMota.TabIndex = 1;
            this.lblMota.Text = "Mô tả";
            // 
            // txtMota
            // 
            this.txtMota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMota.Location = new System.Drawing.Point(52, 0);
            this.txtMota.Name = "txtMota";
            this.txtMota.Size = new System.Drawing.Size(115, 20);
            this.txtMota.TabIndex = 0;
            this.txtMota.TabStop = false;
            this.txtMota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMota_KeyDown);
            // 
            // PIC_Image
            // 
            this.PIC_Image.BackColor = System.Drawing.SystemColors.Control;
            this.PIC_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PIC_Image.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PIC_Image.Location = new System.Drawing.Point(0, 3);
            this.PIC_Image.Name = "PIC_Image";
            this.PIC_Image.Size = new System.Drawing.Size(173, 124);
            this.PIC_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PIC_Image.TabIndex = 1;
            this.PIC_Image.TabStop = false;
            this.PIC_Image.Click += new System.EventHandler(this.PIC_Image_Click);
            // 
            // pnlCheck
            // 
            this.pnlCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCheck.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCheck.BackgroundImage")));
            this.pnlCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlCheck.Location = new System.Drawing.Point(144, 4);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new System.Drawing.Size(26, 28);
            this.pnlCheck.TabIndex = 5;
            this.pnlCheck.Visible = false;
            // 
            // pnlSelect
            // 
            this.pnlSelect.BackColor = System.Drawing.Color.Green;
            this.pnlSelect.Controls.Add(this.pnlCheck);
            this.pnlSelect.Controls.Add(this.PIC_Image);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(173, 127);
            this.pnlSelect.TabIndex = 6;
            // 
            // UC_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlSelect);
            this.Controls.Add(this.pnlInfor);
            this.Name = "UC_Image";
            this.Size = new System.Drawing.Size(173, 171);
            this.Load += new System.EventHandler(this.UC_Image_Load);
            this.pnlInfor.ResumeLayout(false);
            this.pnlInfor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_Image)).EndInit();
            this.pnlSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Label lblMota;
        public System.Windows.Forms.TextBox txtMota;
        public System.Windows.Forms.PictureBox PIC_Image;
        public System.Windows.Forms.CheckBox chkChonAnhIn;
        public System.Windows.Forms.Button cmdXoa;
        public System.Windows.Forms.Label lblID;
        public System.Windows.Forms.TextBox txtSTT;
        private System.Windows.Forms.Panel pnlCheck;
        private System.Windows.Forms.Panel pnlSelect;
    }
}
