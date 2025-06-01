namespace VNS.HIS.UI.NGOAITRU
{
    partial class frm_themnhanh_nhomcls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themnhanh_nhomcls));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grbInfor = new Janus.Windows.EditControls.UIGroupBox();
            this.chkShared = new Janus.Windows.EditControls.UICheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoainhom = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtMotathem = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTennhom = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtManhom = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtId = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbInfor)).BeginInit();
            this.grbInfor.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdSave);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.uiGroupBox1.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 195);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(549, 55);
            this.uiGroupBox1.TabIndex = 4;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(283, 11);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(121, 35);
            this.cmdSave.TabIndex = 5;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.ToolTipText = "Lưu lại thông tin trên lưới thông tin dịch vụ";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(410, 11);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(121, 35);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // grbInfor
            // 
            this.grbInfor.BackColor = System.Drawing.SystemColors.Control;
            this.grbInfor.Controls.Add(this.chkShared);
            this.grbInfor.Controls.Add(this.label1);
            this.grbInfor.Controls.Add(this.txtLoainhom);
            this.grbInfor.Controls.Add(this.txtMotathem);
            this.grbInfor.Controls.Add(this.label6);
            this.grbInfor.Controls.Add(this.txtTennhom);
            this.grbInfor.Controls.Add(this.label5);
            this.grbInfor.Controls.Add(this.label13);
            this.grbInfor.Controls.Add(this.label3);
            this.grbInfor.Controls.Add(this.txtManhom);
            this.grbInfor.Controls.Add(this.label2);
            this.grbInfor.Controls.Add(this.txtId);
            this.grbInfor.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfor.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.grbInfor.Location = new System.Drawing.Point(0, 0);
            this.grbInfor.Name = "grbInfor";
            this.grbInfor.Size = new System.Drawing.Size(549, 152);
            this.grbInfor.TabIndex = 0;
            this.grbInfor.Text = "Thông tin nhóm chỉ định CLS";
            // 
            // chkShared
            // 
            this.chkShared.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShared.Checked = true;
            this.chkShared.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShared.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShared.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.chkShared.Location = new System.Drawing.Point(99, 132);
            this.chkShared.Name = "chkShared";
            this.chkShared.Size = new System.Drawing.Size(225, 23);
            this.chkShared.TabIndex = 639;
            this.chkShared.Text = "Chia sẻ với các bác sĩ khác?";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 513;
            this.label1.Text = "Loại nhóm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLoainhom
            // 
            this.txtLoainhom._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLoainhom._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoainhom._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLoainhom.AddValues = true;
            this.txtLoainhom.AllowMultiline = false;
            this.txtLoainhom.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoainhom.AutoCompleteList")));
            this.txtLoainhom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoainhom.buildShortcut = false;
            this.txtLoainhom.CaseSensitive = false;
            this.txtLoainhom.cmdDropDown = null;
            this.txtLoainhom.CompareNoID = true;
            this.txtLoainhom.DefaultCode = "-1";
            this.txtLoainhom.DefaultID = "-1";
            this.txtLoainhom.Drug_ID = null;
            this.txtLoainhom.ExtraWidth = 0;
            this.txtLoainhom.FillValueAfterSelect = false;
            this.txtLoainhom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoainhom.LOAI_DANHMUC = "LOAINHOMCHIDINHCLS";
            this.txtLoainhom.Location = new System.Drawing.Point(99, 51);
            this.txtLoainhom.MaxHeight = 150;
            this.txtLoainhom.MinTypedCharacters = 2;
            this.txtLoainhom.MyCode = "-1";
            this.txtLoainhom.MyID = "-1";
            this.txtLoainhom.Name = "txtLoainhom";
            this.txtLoainhom.RaiseEvent = false;
            this.txtLoainhom.RaiseEventEnter = false;
            this.txtLoainhom.RaiseEventEnterWhenEmpty = false;
            this.txtLoainhom.SelectedIndex = -1;
            this.txtLoainhom.ShowCodeWithValue = false;
            this.txtLoainhom.Size = new System.Drawing.Size(432, 21);
            this.txtLoainhom.splitChar = '@';
            this.txtLoainhom.splitCharIDAndCode = '#';
            this.txtLoainhom.TabIndex = 2;
            this.txtLoainhom.TakeCode = false;
            this.txtLoainhom.txtMyCode = null;
            this.txtLoainhom.txtMyCode_Edit = null;
            this.txtLoainhom.txtMyID = null;
            this.txtLoainhom.txtMyID_Edit = null;
            this.txtLoainhom.txtMyName = null;
            this.txtLoainhom.txtMyName_Edit = null;
            this.txtLoainhom.txtNext = null;
            this.txtLoainhom.txtNext1 = null;
            // 
            // txtMotathem
            // 
            this.txtMotathem.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtMotathem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotathem.Location = new System.Drawing.Point(99, 105);
            this.txtMotathem.Name = "txtMotathem";
            this.txtMotathem.Size = new System.Drawing.Size(432, 21);
            this.txtMotathem.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(-2, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 17);
            this.label6.TabIndex = 511;
            this.label6.Text = "Mô tả thêm:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTennhom
            // 
            this.txtTennhom.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtTennhom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTennhom.Location = new System.Drawing.Point(99, 78);
            this.txtTennhom.Name = "txtTennhom";
            this.txtTennhom.Size = new System.Drawing.Size(432, 21);
            this.txtTennhom.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(-2, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 21);
            this.label5.TabIndex = 509;
            this.label5.Text = "Tên nhóm";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(1007, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 17);
            this.label13.TabIndex = 504;
            this.label13.Text = "&Đã thực hiện";
            this.label13.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(185, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mã nhóm";
            // 
            // txtManhom
            // 
            this.txtManhom.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtManhom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManhom.Location = new System.Drawing.Point(248, 24);
            this.txtManhom.Name = "txtManhom";
            this.txtManhom.Size = new System.Drawing.Size(283, 21);
            this.txtManhom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID nhóm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(99, 24);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(80, 21);
            this.txtId.TabIndex = 0;
            this.txtId.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(0, 173);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(549, 22);
            this.lblMsg.TabIndex = 512;
            this.lblMsg.Text = "msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frm_themnhanh_nhomcls
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(549, 250);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.grbInfor);
            this.Controls.Add(this.uiGroupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themnhanh_nhomcls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới nhanh nhóm cận lâm sàng";
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbInfor)).EndInit();
            this.grbInfor.ResumeLayout(false);
            this.grbInfor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UIGroupBox grbInfor;
        private System.Windows.Forms.Label label1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLoainhom;
        private Janus.Windows.GridEX.EditControls.EditBox txtMotathem;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtTennhom;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtManhom;
        private System.Windows.Forms.Label label2;
        public Janus.Windows.GridEX.EditControls.EditBox txtId;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UICheckBox chkShared;

    }
}