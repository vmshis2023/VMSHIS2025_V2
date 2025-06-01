namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_benh
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_benh));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDiease_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsDesc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDieaseName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDieaseCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenTiengAnh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboDieaseType = new VNS.HIS.UCs.EasyCompletionComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(342, 405);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(111, 32);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Lưu";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(459, 405);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(108, 32);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cboDieaseType);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtTenTiengAnh);
            this.uiGroupBox1.Controls.Add(this.txtDieaseName);
            this.uiGroupBox1.Controls.Add(this.txtDiease_ID);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtsDesc);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtDieaseCode);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(601, 390);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin bệnh";
            // 
            // txtDiease_ID
            // 
            this.txtDiease_ID.Location = new System.Drawing.Point(261, 32);
            this.txtDiease_ID.Name = "txtDiease_ID";
            this.txtDiease_ID.ReadOnly = true;
            this.txtDiease_ID.Size = new System.Drawing.Size(62, 21);
            this.txtDiease_ID.TabIndex = 0;
            this.txtDiease_ID.TabStop = false;
            this.txtDiease_ID.TextChanged += new System.EventHandler(this.txtDiease_ID_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "Mô tả";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsDesc
            // 
            this.txtsDesc.Location = new System.Drawing.Point(101, 142);
            this.txtsDesc.Multiline = true;
            this.txtsDesc.Name = "txtsDesc";
            this.txtsDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtsDesc.Size = new System.Drawing.Size(466, 242);
            this.txtsDesc.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Kiểu bệnh";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tên bệnh";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDieaseName
            // 
            this.txtDieaseName.BackColor = System.Drawing.Color.White;
            this.txtDieaseName.Location = new System.Drawing.Point(101, 58);
            this.txtDieaseName.Name = "txtDieaseName";
            this.txtDieaseName.Size = new System.Drawing.Size(467, 21);
            this.txtDieaseName.TabIndex = 1;
            this.txtDieaseName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.txtDieaseName.TextChanged += new System.EventHandler(this.txtDieaseName_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mã bệnh";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDieaseCode
            // 
            this.txtDieaseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDieaseCode.Location = new System.Drawing.Point(101, 33);
            this.txtDieaseCode.MaxLength = 10;
            this.txtDieaseCode.Name = "txtDieaseCode";
            this.txtDieaseCode.Size = new System.Drawing.Size(156, 21);
            this.txtDieaseCode.TabIndex = 0;
            this.txtDieaseCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.txtDieaseCode.TextChanged += new System.EventHandler(this.txtDieaseCode_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 19);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tên tiếng Anh";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenTiengAnh
            // 
            this.txtTenTiengAnh.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTenTiengAnh.Location = new System.Drawing.Point(102, 86);
            this.txtTenTiengAnh.MaxLength = 10;
            this.txtTenTiengAnh.Name = "txtTenTiengAnh";
            this.txtTenTiengAnh.Size = new System.Drawing.Size(466, 21);
            this.txtTenTiengAnh.TabIndex = 2;
            this.txtTenTiengAnh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cboDieaseType
            // 
            this.cboDieaseType.FormattingEnabled = true;
            this.cboDieaseType.Location = new System.Drawing.Point(101, 112);
            this.cboDieaseType.Name = "cboDieaseType";
            this.cboDieaseType.Next_Control = null;
            this.cboDieaseType.RaiseEnterEventWhenInvisible = true;
            this.cboDieaseType.Size = new System.Drawing.Size(467, 23);
            this.cboDieaseType.TabIndex = 3;
            // 
            // frm_themmoi_benh
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(601, 449);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_benh";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin bệnh";
            this.Load += new System.EventHandler(this.frm_themmoi_benh_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_benh_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtDieaseName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtDieaseCode;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtsDesc;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        internal Janus.Windows.GridEX.EditControls.EditBox txtDiease_ID;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenTiengAnh;
        private UCs.EasyCompletionComboBox cboDieaseType;
    }
}