namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_diachinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_diachinh));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.lblCaptren = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSurveyType = new System.Windows.Forms.ComboBox();
            this.cmdClear = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIntOrder = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsDesc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboDiachinhcaptren = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSurveyName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSurveyCode = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntOrder)).BeginInit();
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
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdSave.Location = new System.Drawing.Point(110, 281);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(137, 32);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Lưu thông tin ";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(253, 281);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(108, 32);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.lblCaptren);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.cboSurveyType);
            this.grpControl.Controls.Add(this.cmdClear);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.txtIntOrder);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.txtsDesc);
            this.grpControl.Controls.Add(this.cboDiachinhcaptren);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.txtSurveyName);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtSurveyCode);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(496, 264);
            this.grpControl.TabIndex = 1;
            this.grpControl.Text = "&Thông tin bệnh";
            // 
            // lblCaptren
            // 
            this.lblCaptren.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCaptren.AutoSize = true;
            this.lblCaptren.Location = new System.Drawing.Point(12, 103);
            this.lblCaptren.Name = "lblCaptren";
            this.lblCaptren.Size = new System.Drawing.Size(53, 15);
            this.lblCaptren.TabIndex = 28;
            this.lblCaptren.Text = "Cấp trên";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 27;
            this.label5.Text = "Loại địa chính";
            // 
            // cboSurveyType
            // 
            this.cboSurveyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSurveyType.FormattingEnabled = true;
            this.cboSurveyType.Items.AddRange(new object[] {
            "Tỉnh,Thành phố",
            "Quận huyện",
            "Xã phường"});
            this.cboSurveyType.Location = new System.Drawing.Point(138, 72);
            this.cboSurveyType.Name = "cboSurveyType";
            this.cboSurveyType.Size = new System.Drawing.Size(335, 23);
            this.cboSurveyType.TabIndex = 26;
            // 
            // cmdClear
            // 
            this.cmdClear.AutoSize = true;
            this.cmdClear.Location = new System.Drawing.Point(388, 241);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(85, 15);
            this.cmdClear.TabIndex = 24;
            this.cmdClear.TabStop = true;
            this.cmdClear.Text = "&Thêm mới(F5)";
            this.cmdClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdClear_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "STT";
            // 
            // txtIntOrder
            // 
            this.txtIntOrder.Location = new System.Drawing.Point(138, 134);
            this.txtIntOrder.Name = "txtIntOrder";
            this.txtIntOrder.Size = new System.Drawing.Size(106, 21);
            this.txtIntOrder.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Mô tả";
            // 
            // txtsDesc
            // 
            this.txtsDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsDesc.Location = new System.Drawing.Point(138, 168);
            this.txtsDesc.Multiline = true;
            this.txtsDesc.Name = "txtsDesc";
            this.txtsDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtsDesc.Size = new System.Drawing.Size(335, 48);
            this.txtsDesc.TabIndex = 4;
            // 
            // cboDiachinhcaptren
            // 
            this.cboDiachinhcaptren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiachinhcaptren.Enabled = false;
            this.cboDiachinhcaptren.FormattingEnabled = true;
            this.cboDiachinhcaptren.Location = new System.Drawing.Point(138, 100);
            this.cboDiachinhcaptren.Name = "cboDiachinhcaptren";
            this.cboDiachinhcaptren.Size = new System.Drawing.Size(335, 23);
            this.cboDiachinhcaptren.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tên địa chính";
            // 
            // txtSurveyName
            // 
            this.txtSurveyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSurveyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtSurveyName.Location = new System.Drawing.Point(138, 49);
            this.txtSurveyName.Name = "txtSurveyName";
            this.txtSurveyName.Size = new System.Drawing.Size(335, 21);
            this.txtSurveyName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mã địa chính";
            // 
            // txtSurveyCode
            // 
            this.txtSurveyCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSurveyCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtSurveyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSurveyCode.Location = new System.Drawing.Point(138, 23);
            this.txtSurveyCode.MaxLength = 5;
            this.txtSurveyCode.Name = "txtSurveyCode";
            this.txtSurveyCode.Size = new System.Drawing.Size(147, 21);
            this.txtSurveyCode.TabIndex = 0;
            // 
            // frm_themmoi_diachinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 325);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_diachinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin địa chính";
            this.Load += new System.EventHandler(this.frm_themmoi_diachinh_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_diachinh_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIntOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtsDesc;
        private System.Windows.Forms.ComboBox cboDiachinhcaptren;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtSurveyName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtIntOrder;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        internal Janus.Windows.GridEX.EditControls.EditBox txtSurveyCode;
        private System.Windows.Forms.LinkLabel cmdClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSurveyType;
        private System.Windows.Forms.Label lblCaptren;
    }
}