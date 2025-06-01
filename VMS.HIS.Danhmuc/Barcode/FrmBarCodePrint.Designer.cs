namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class FrmBarCodePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarCodePrint));
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.chkPreview = new Janus.Windows.EditControls.UICheckBox();
            this.txtPatientcode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientId = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtgioitinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtngaysinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtHovaTen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.nmrSoLuong = new System.Windows.Forms.NumericUpDown();
            this.Label14 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel7
            // 
            this.Panel7.BackColor = System.Drawing.SystemColors.Control;
            this.Panel7.Controls.Add(this.pictureBox1);
            this.Panel7.Controls.Add(this.Label15);
            this.Panel7.Controls.Add(this.panel1);
            this.Panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel7.Location = new System.Drawing.Point(0, 0);
            this.Panel7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(688, 91);
            this.Panel7.TabIndex = 105;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.SystemColors.Control;
            this.Label15.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Label15.Location = new System.Drawing.Point(91, 0);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(473, 91);
            this.Label15.TabIndex = 108;
            this.Label15.Text = "IN BARCODE NGƯỜI BỆNH";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.PictureBox6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 91);
            this.panel1.TabIndex = 107;
            // 
            // PictureBox6
            // 
            this.PictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox6.Image")));
            this.PictureBox6.Location = new System.Drawing.Point(11, 4);
            this.PictureBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(74, 80);
            this.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox6.TabIndex = 35;
            this.PictureBox6.TabStop = false;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 91);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(688, 309);
            this.TabControl1.TabIndex = 106;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage1.Controls.Add(this.chkPreview);
            this.TabPage1.Controls.Add(this.txtPatientcode);
            this.TabPage1.Controls.Add(this.txtPatientId);
            this.TabPage1.Controls.Add(this.txtgioitinh);
            this.TabPage1.Controls.Add(this.txtngaysinh);
            this.TabPage1.Controls.Add(this.txtHovaTen);
            this.TabPage1.Controls.Add(this.nmrSoLuong);
            this.TabPage1.Controls.Add(this.Label14);
            this.TabPage1.Controls.Add(this.cmdClose);
            this.TabPage1.Controls.Add(this.cmdPrint);
            this.TabPage1.Controls.Add(this.ProgressBar1);
            this.TabPage1.Controls.Add(this.cboPrinters);
            this.TabPage1.Controls.Add(this.Label16);
            this.TabPage1.Controls.Add(this.label7);
            this.TabPage1.Controls.Add(this.label17);
            this.TabPage1.Controls.Add(this.label11);
            this.TabPage1.Controls.Add(this.label5);
            this.TabPage1.Controls.Add(this.label1);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Location = new System.Drawing.Point(4, 25);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(680, 280);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "In Barcode";
            // 
            // chkPreview
            // 
            this.chkPreview.Location = new System.Drawing.Point(201, 94);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(144, 23);
            this.chkPreview.TabIndex = 537;
            this.chkPreview.Text = "Xem trước khi in?";
            // 
            // txtPatientcode
            // 
            this.txtPatientcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPatientcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientcode.Location = new System.Drawing.Point(441, 70);
            this.txtPatientcode.Name = "txtPatientcode";
            this.txtPatientcode.ReadOnly = true;
            this.txtPatientcode.Size = new System.Drawing.Size(98, 23);
            this.txtPatientcode.TabIndex = 536;
            // 
            // txtPatientId
            // 
            this.txtPatientId.BackColor = System.Drawing.Color.White;
            this.txtPatientId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientId.Location = new System.Drawing.Point(97, 67);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.ReadOnly = true;
            this.txtPatientId.Size = new System.Drawing.Size(248, 23);
            this.txtPatientId.TabIndex = 533;
            // 
            // txtgioitinh
            // 
            this.txtgioitinh.BackColor = System.Drawing.Color.White;
            this.txtgioitinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgioitinh.Location = new System.Drawing.Point(608, 41);
            this.txtgioitinh.Name = "txtgioitinh";
            this.txtgioitinh.ReadOnly = true;
            this.txtgioitinh.Size = new System.Drawing.Size(64, 23);
            this.txtgioitinh.TabIndex = 531;
            // 
            // txtngaysinh
            // 
            this.txtngaysinh.BackColor = System.Drawing.Color.White;
            this.txtngaysinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtngaysinh.Location = new System.Drawing.Point(441, 39);
            this.txtngaysinh.Name = "txtngaysinh";
            this.txtngaysinh.ReadOnly = true;
            this.txtngaysinh.Size = new System.Drawing.Size(98, 23);
            this.txtngaysinh.TabIndex = 529;
            // 
            // txtHovaTen
            // 
            this.txtHovaTen.BackColor = System.Drawing.Color.White;
            this.txtHovaTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHovaTen.Location = new System.Drawing.Point(97, 40);
            this.txtHovaTen.Name = "txtHovaTen";
            this.txtHovaTen.ReadOnly = true;
            this.txtHovaTen.Size = new System.Drawing.Size(248, 23);
            this.txtHovaTen.TabIndex = 527;
            // 
            // nmrSoLuong
            // 
            this.nmrSoLuong.Location = new System.Drawing.Point(97, 93);
            this.nmrSoLuong.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmrSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrSoLuong.Name = "nmrSoLuong";
            this.nmrSoLuong.Size = new System.Drawing.Size(98, 23);
            this.nmrSoLuong.TabIndex = 5;
            this.nmrSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label14
            // 
            this.Label14.Location = new System.Drawing.Point(8, 97);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(83, 20);
            this.Label14.TabIndex = 112;
            this.Label14.Text = "Số bản in";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(530, 233);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(142, 39);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Image = global::VMS.HIS.Danhmuc.Properties.Resources.printer_24;
            this.cmdPrint.Location = new System.Drawing.Point(381, 233);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(142, 39);
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "In Barcode (F4)";
            this.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Location = new System.Drawing.Point(19, 204);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(653, 14);
            this.ProgressBar1.TabIndex = 105;
            // 
            // cboPrinters
            // 
            this.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(97, 10);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(575, 24);
            this.cboPrinters.TabIndex = 0;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(200, 97);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(0, 16);
            this.Label16.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(545, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 532;
            this.label7.Text = "Giới tính";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(352, 73);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 20);
            this.label17.TabIndex = 535;
            this.label17.Text = "Patient Code";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 20);
            this.label11.TabIndex = 534;
            this.label11.Text = "Patient ID";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(373, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 530;
            this.label5.Text = "Ngày sinh";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 528;
            this.label1.Text = "Họ và tên";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(8, 14);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(83, 20);
            this.Label3.TabIndex = 100;
            this.Label3.Text = "Máy in";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(570, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // FrmBarCodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 400);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.Panel7);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmBarCodePrint";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin mã vạch";
            this.Load += new System.EventHandler(this.FrmBarCodePrint_Load);
            this.Panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel7;
        internal System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox PictureBox6;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage TabPage1;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.NumericUpDown nmrSoLuong;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Button cmdClose;
        internal System.Windows.Forms.Button cmdPrint;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.ComboBox cboPrinters;
        private Janus.Windows.GridEX.EditControls.EditBox txtHovaTen;
        internal System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientId;
        private Janus.Windows.GridEX.EditControls.EditBox txtgioitinh;
        private Janus.Windows.GridEX.EditControls.EditBox txtngaysinh;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label11;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientcode;
        private Janus.Windows.EditControls.UICheckBox chkPreview;
        internal System.Windows.Forms.PictureBox pictureBox1;
    }
}