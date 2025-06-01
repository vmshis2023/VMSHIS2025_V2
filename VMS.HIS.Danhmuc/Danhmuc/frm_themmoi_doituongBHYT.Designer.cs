namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_doituongBHYT
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
            Janus.Windows.GridEX.GridEXLayout chkMaQuyenLoi_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_doituongBHYT));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkTinhgiachung = new System.Windows.Forms.CheckBox();
            this.chkMaQuyenLoi = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.nmPercent = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cboObjectTypeID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboInsuranceGroupID = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtsDesc = new System.Windows.Forms.TextBox();
            this.txtintOrder = new System.Windows.Forms.NumericUpDown();
            this.lblStt = new System.Windows.Forms.Label();
            this.txtInsObjectName = new System.Windows.Forms.TextBox();
            this.lblCountryDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInsuranceObjects = new System.Windows.Forms.Label();
            this.txtInsObjectCode = new System.Windows.Forms.TextBox();
            this.txtInsObject_ID = new System.Windows.Forms.TextBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMaQuyenLoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtintOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.chkTinhgiachung);
            this.uiGroupBox1.Controls.Add(this.chkMaQuyenLoi);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.nmPercent);
            this.uiGroupBox1.Controls.Add(this.label9);
            this.uiGroupBox1.Controls.Add(this.cboObjectTypeID);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.cboInsuranceGroupID);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtsDesc);
            this.uiGroupBox1.Controls.Add(this.txtintOrder);
            this.uiGroupBox1.Controls.Add(this.lblStt);
            this.uiGroupBox1.Controls.Add(this.txtInsObjectName);
            this.uiGroupBox1.Controls.Add(this.lblCountryDescription);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.lblInsuranceObjects);
            this.uiGroupBox1.Controls.Add(this.txtInsObjectCode);
            this.uiGroupBox1.Controls.Add(this.txtInsObject_ID);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(522, 344);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin đối tượng BHYT";
            // 
            // chkTinhgiachung
            // 
            this.chkTinhgiachung.AutoSize = true;
            this.chkTinhgiachung.Location = new System.Drawing.Point(115, 158);
            this.chkTinhgiachung.Name = "chkTinhgiachung";
            this.chkTinhgiachung.Size = new System.Drawing.Size(298, 19);
            this.chkTinhgiachung.TabIndex = 72;
            this.chkTinhgiachung.Text = "Lấy phần trăm BHYT trên cho tất cả các quyền lợi?";
            this.chkTinhgiachung.UseVisualStyleBackColor = true;
            // 
            // chkMaQuyenLoi
            // 
            this.chkMaQuyenLoi.ColumnAutoResize = true;
            chkMaQuyenLoi_DesignTimeLayout.LayoutString = resources.GetString("chkMaQuyenLoi_DesignTimeLayout.LayoutString");
            this.chkMaQuyenLoi.DesignTimeLayout = chkMaQuyenLoi_DesignTimeLayout;
            this.chkMaQuyenLoi.GroupByBoxVisible = false;
            this.chkMaQuyenLoi.Location = new System.Drawing.Point(114, 258);
            this.chkMaQuyenLoi.Name = "chkMaQuyenLoi";
            this.chkMaQuyenLoi.Size = new System.Drawing.Size(402, 80);
            this.chkMaQuyenLoi.TabIndex = 71;
            this.chkMaQuyenLoi.Visible = false;
            this.chkMaQuyenLoi.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(256, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 70;
            this.label1.Text = "(Chi trả từ 1->100%)";
            // 
            // nmPercent
            // 
            this.nmPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.nmPercent.Location = new System.Drawing.Point(115, 130);
            this.nmPercent.MaxLength = 3;
            this.nmPercent.Minimum = 1;
            this.nmPercent.Name = "nmPercent";
            this.nmPercent.Size = new System.Drawing.Size(135, 22);
            this.nmPercent.TabIndex = 4;
            this.nmPercent.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.nmPercent.Value = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 15);
            this.label9.TabIndex = 68;
            this.label9.Text = "P.trăm BHYT";
            // 
            // cboObjectTypeID
            // 
            this.cboObjectTypeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectTypeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboObjectTypeID.FormattingEnabled = true;
            this.cboObjectTypeID.Location = new System.Drawing.Point(114, 76);
            this.cboObjectTypeID.Name = "cboObjectTypeID";
            this.cboObjectTypeID.Size = new System.Drawing.Size(325, 23);
            this.cboObjectTypeID.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "Đối tượng:";
            // 
            // cboInsuranceGroupID
            // 
            this.cboInsuranceGroupID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInsuranceGroupID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInsuranceGroupID.FormattingEnabled = true;
            this.cboInsuranceGroupID.Location = new System.Drawing.Point(114, 101);
            this.cboInsuranceGroupID.Name = "cboInsuranceGroupID";
            this.cboInsuranceGroupID.Size = new System.Drawing.Size(325, 23);
            this.cboInsuranceGroupID.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 15);
            this.label5.TabIndex = 66;
            this.label5.Text = "Nhóm loại BHYT:";
            // 
            // txtsDesc
            // 
            this.txtsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsDesc.Location = new System.Drawing.Point(113, 208);
            this.txtsDesc.Multiline = true;
            this.txtsDesc.Name = "txtsDesc";
            this.txtsDesc.Size = new System.Drawing.Size(325, 35);
            this.txtsDesc.TabIndex = 6;
            this.txtsDesc.WordWrap = false;
            // 
            // txtintOrder
            // 
            this.txtintOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtintOrder.Location = new System.Drawing.Point(114, 182);
            this.txtintOrder.Name = "txtintOrder";
            this.txtintOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtintOrder.Size = new System.Drawing.Size(135, 21);
            this.txtintOrder.TabIndex = 5;
            this.txtintOrder.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lblStt
            // 
            this.lblStt.AutoSize = true;
            this.lblStt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStt.Location = new System.Drawing.Point(6, 184);
            this.lblStt.Name = "lblStt";
            this.lblStt.Size = new System.Drawing.Size(32, 15);
            this.lblStt.TabIndex = 59;
            this.lblStt.Text = "STT:";
            // 
            // txtInsObjectName
            // 
            this.txtInsObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsObjectName.Location = new System.Drawing.Point(114, 50);
            this.txtInsObjectName.Name = "txtInsObjectName";
            this.txtInsObjectName.Size = new System.Drawing.Size(325, 21);
            this.txtInsObjectName.TabIndex = 1;
            // 
            // lblCountryDescription
            // 
            this.lblCountryDescription.AutoSize = true;
            this.lblCountryDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountryDescription.Location = new System.Drawing.Point(6, 209);
            this.lblCountryDescription.Name = "lblCountryDescription";
            this.lblCountryDescription.Size = new System.Drawing.Size(52, 15);
            this.lblCountryDescription.TabIndex = 54;
            this.lblCountryDescription.Text = "Ghi chú:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 56;
            this.label2.Text = "Tên loại ĐT BHYT:";
            // 
            // lblInsuranceObjects
            // 
            this.lblInsuranceObjects.AutoSize = true;
            this.lblInsuranceObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuranceObjects.Location = new System.Drawing.Point(7, 27);
            this.lblInsuranceObjects.Name = "lblInsuranceObjects";
            this.lblInsuranceObjects.Size = new System.Drawing.Size(104, 15);
            this.lblInsuranceObjects.TabIndex = 57;
            this.lblInsuranceObjects.Text = "Mã loại ĐT BHYT:";
            // 
            // txtInsObjectCode
            // 
            this.txtInsObjectCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtInsObjectCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInsObjectCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsObjectCode.Location = new System.Drawing.Point(114, 25);
            this.txtInsObjectCode.MaxLength = 2;
            this.txtInsObjectCode.Name = "txtInsObjectCode";
            this.txtInsObjectCode.Size = new System.Drawing.Size(177, 21);
            this.txtInsObjectCode.TabIndex = 0;
            // 
            // txtInsObject_ID
            // 
            this.txtInsObject_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsObject_ID.Location = new System.Drawing.Point(115, 25);
            this.txtInsObject_ID.MaxLength = 2;
            this.txtInsObject_ID.Name = "txtInsObject_ID";
            this.txtInsObject_ID.Size = new System.Drawing.Size(51, 21);
            this.txtInsObject_ID.TabIndex = 0;
            this.txtInsObject_ID.Visible = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(133, 356);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(137, 32);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdExit.Location = new System.Drawing.Point(276, 356);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(108, 32);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
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
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // frm_themmoi_doituongBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 394);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_doituongBHYT";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin đối tượng tham gia bảo hiểm";
            this.Load += new System.EventHandler(this.frm_themmoi_doituongBHYT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_doituongBHYT_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMaQuyenLoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtintOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboObjectTypeID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboInsuranceGroupID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtsDesc;
        private System.Windows.Forms.NumericUpDown txtintOrder;
        private System.Windows.Forms.Label lblStt;
        private System.Windows.Forms.TextBox txtInsObjectName;
        private System.Windows.Forms.Label lblCountryDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInsuranceObjects;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        internal System.Windows.Forms.TextBox txtInsObject_ID;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown nmPercent;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtInsObjectCode;
        private Janus.Windows.GridEX.GridEX chkMaQuyenLoi;
        private System.Windows.Forms.CheckBox chkTinhgiachung;

    }
}