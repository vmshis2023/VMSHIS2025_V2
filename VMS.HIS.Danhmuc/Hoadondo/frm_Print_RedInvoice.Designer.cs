namespace VNS.HIS.UI.HOADONDO
{
    partial class frm_Print_RedInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Print_RedInvoice));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdHoaDonCapPhat_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.grdHoaDonCapPhat = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMauHD = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtKiHieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSerie = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStaff_Name = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboLy_Do = new Janus.Windows.EditControls.UIComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboPrinter = new Janus.Windows.EditControls.UIComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaQuyen = new Janus.Windows.GridEX.EditControls.EditBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHoaDonCapPhat)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdThoat.Location = new System.Drawing.Point(409, 316);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(116, 31);
            this.cmdThoat.TabIndex = 2;
            this.cmdThoat.Text = "&Thoát(Esc)";
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPrint.Location = new System.Drawing.Point(262, 316);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(141, 31);
            this.cmdPrint.TabIndex = 1;
            this.cmdPrint.Text = "&In hóa đơn (F4)";
            this.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
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
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Controls.Add(this.grdHoaDonCapPhat);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(796, 245);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin hóa đơn";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
                " thông tin</FilterRowInfoText></LocalizableData>";
            this.grdList.CardColumnHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.CardSpacing = 0;
            this.grdList.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both;
            this.grdList.CardWidth = 568;
            this.grdList.CenterSingleCard = false;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(221, 18);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(572, 224);
            this.grdList.TabIndex = 33;
            this.grdList.View = Janus.Windows.GridEX.View.SingleCard;
            // 
            // grdHoaDonCapPhat
            // 
            this.grdHoaDonCapPhat.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdHoaDonCapPhat_DesignTimeLayout.LayoutString = resources.GetString("grdHoaDonCapPhat_DesignTimeLayout.LayoutString");
            this.grdHoaDonCapPhat.DesignTimeLayout = grdHoaDonCapPhat_DesignTimeLayout;
            this.grdHoaDonCapPhat.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdHoaDonCapPhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdHoaDonCapPhat.GroupByBoxVisible = false;
            this.grdHoaDonCapPhat.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdHoaDonCapPhat.Location = new System.Drawing.Point(3, 18);
            this.grdHoaDonCapPhat.Name = "grdHoaDonCapPhat";
            this.grdHoaDonCapPhat.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdHoaDonCapPhat.Size = new System.Drawing.Size(218, 224);
            this.grdHoaDonCapPhat.TabIndex = 32;
            this.grdHoaDonCapPhat.SelectionChanged += new System.EventHandler(this.grdHoaDonCapPhat_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mẫu hóa đơn";
            // 
            // txtMauHD
            // 
            this.txtMauHD.Enabled = false;
            this.txtMauHD.Location = new System.Drawing.Point(91, 254);
            this.txtMauHD.Name = "txtMauHD";
            this.txtMauHD.Size = new System.Drawing.Size(100, 20);
            this.txtMauHD.TabIndex = 4;
            // 
            // txtKiHieu
            // 
            this.txtKiHieu.Enabled = false;
            this.txtKiHieu.Location = new System.Drawing.Point(249, 254);
            this.txtKiHieu.Name = "txtKiHieu";
            this.txtKiHieu.Size = new System.Drawing.Size(100, 20);
            this.txtKiHieu.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(201, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ký hiệu";
            // 
            // txtSerie
            // 
            this.txtSerie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.Location = new System.Drawing.Point(91, 284);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 21);
            this.txtSerie.TabIndex = 8;
            this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerie_KeyDown);
            this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerie_KeyPress);
            this.txtSerie.Leave += new System.EventHandler(this.txtSerie_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Serie";
            // 
            // lblStaff_Name
            // 
            this.lblStaff_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStaff_Name.AutoSize = true;
            this.lblStaff_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaff_Name.Location = new System.Drawing.Point(587, 257);
            this.lblStaff_Name.Name = "lblStaff_Name";
            this.lblStaff_Name.Size = new System.Drawing.Size(53, 15);
            this.lblStaff_Name.TabIndex = 9;
            this.lblStaff_Name.Text = "Người in";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(528, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Người in";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(201, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Lý do";
            // 
            // cboLy_Do
            // 
            this.cboLy_Do.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboLy_Do.Location = new System.Drawing.Point(249, 284);
            this.cboLy_Do.Name = "cboLy_Do";
            this.cboLy_Do.Size = new System.Drawing.Size(273, 20);
            this.cboLy_Do.TabIndex = 12;
            this.cboLy_Do.SelectedIndexChanged += new System.EventHandler(this.cboLy_Do_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(528, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Máy in";
            // 
            // cboPrinter
            // 
            this.cboPrinter.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPrinter.Location = new System.Drawing.Point(579, 284);
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Size = new System.Drawing.Size(217, 20);
            this.cboPrinter.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(355, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Mã quyển";
            // 
            // txtMaQuyen
            // 
            this.txtMaQuyen.Enabled = false;
            this.txtMaQuyen.Location = new System.Drawing.Point(422, 254);
            this.txtMaQuyen.Name = "txtMaQuyen";
            this.txtMaQuyen.Size = new System.Drawing.Size(100, 20);
            this.txtMaQuyen.TabIndex = 16;
            // 
            // frm_Print_RedInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 352);
            this.Controls.Add(this.txtMaQuyen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboPrinter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboLy_Do);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblStaff_Name);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKiHieu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMauHD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Print_RedInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IN HÓA ĐƠN ĐỎ";
            this.Load += new System.EventHandler(this.frm_Print_RedInvoice_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Print_RedInvoice_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHoaDonCapPhat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStaff_Name;
        private Janus.Windows.GridEX.EditControls.EditBox txtSerie;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtKiHieu;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtMauHD;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboLy_Do;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIComboBox cboPrinter;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.GridEX.GridEX grdHoaDonCapPhat;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaQuyen;
        // private System.Windows.Forms.RadioButton radChucnang;
    }
}