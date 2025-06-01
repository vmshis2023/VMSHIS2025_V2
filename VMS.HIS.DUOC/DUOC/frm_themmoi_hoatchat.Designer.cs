using VNS.HIS.UCs;
namespace VNS.HISLink.UI.Duoc.Form_DanhMuc
{
    partial class frm_themmoi_hoatchat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_hoatchat));
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdHoatChat = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkhienthi = new System.Windows.Forms.CheckBox();
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.txtThuTu = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTenHoatChat = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.txtduongdung = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmota = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMaHoatChat = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.officeFormAdorner1 = new Janus.Windows.Ribbon.OfficeFormAdorner(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThuTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.officeFormAdorner1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(25, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 36;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdHoatChat
            // 
            this.txtIdHoatChat.BackColor = System.Drawing.Color.White;
            this.txtIdHoatChat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdHoatChat.Enabled = false;
            this.txtIdHoatChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdHoatChat.Location = new System.Drawing.Point(120, 23);
            this.txtIdHoatChat.Name = "txtIdHoatChat";
            this.txtIdHoatChat.Size = new System.Drawing.Size(80, 21);
            this.txtIdHoatChat.TabIndex = 1;
            this.txtIdHoatChat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // chkhienthi
            // 
            this.chkhienthi.AutoSize = true;
            this.chkhienthi.BackColor = System.Drawing.Color.Transparent;
            this.chkhienthi.Checked = true;
            this.chkhienthi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkhienthi.Location = new System.Drawing.Point(186, 104);
            this.chkhienthi.Name = "chkhienthi";
            this.chkhienthi.Size = new System.Drawing.Size(68, 19);
            this.chkhienthi.TabIndex = 5;
            this.chkhienthi.Text = "&Hiển thị";
            this.chkhienthi.UseVisualStyleBackColor = false;
            // 
            // errorProvider3
            // 
            this.errorProvider3.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(25, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 18);
            this.label12.TabIndex = 32;
            this.label12.Text = "Thứ tự";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtThuTu
            // 
            this.txtThuTu.Location = new System.Drawing.Point(121, 102);
            this.txtThuTu.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtThuTu.Name = "txtThuTu";
            this.txtThuTu.Size = new System.Drawing.Size(59, 21);
            this.txtThuTu.TabIndex = 4;
            this.txtThuTu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(25, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tên hoạt chất";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenHoatChat
            // 
            this.txtTenHoatChat.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtTenHoatChat.Location = new System.Drawing.Point(120, 77);
            this.txtTenHoatChat.Name = "txtTenHoatChat";
            this.txtTenHoatChat.Size = new System.Drawing.Size(311, 21);
            this.txtTenHoatChat.TabIndex = 3;
            this.txtTenHoatChat.TextChanged += new System.EventHandler(this.txtTenHoatChat_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.White;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(3, 258);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(537, 23);
            this.lblMessage.TabIndex = 15;
            this.lblMessage.Text = "lblMessage";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMessage.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(25, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã hoạt chất";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpControl
            // 
            this.grpControl.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Panel;
            this.grpControl.Controls.Add(this.txtduongdung);
            this.grpControl.Controls.Add(this.label15);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.txtmota);
            this.grpControl.Controls.Add(this.txtMaHoatChat);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.txtIdHoatChat);
            this.grpControl.Controls.Add(this.chkhienthi);
            this.grpControl.Controls.Add(this.label12);
            this.grpControl.Controls.Add(this.txtThuTu);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.txtTenHoatChat);
            this.grpControl.Controls.Add(this.lblMessage);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(543, 284);
            this.grpControl.TabIndex = 3;
            this.grpControl.Text = "&Thông tin hoạt chất";
            // 
            // txtduongdung
            // 
            this.txtduongdung._backcolor = System.Drawing.SystemColors.Control;
            this.txtduongdung._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtduongdung._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtduongdung.AddValues = true;
            this.txtduongdung.AllowMultiline = false;
            this.txtduongdung.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtduongdung.AutoCompleteList")));
            this.txtduongdung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtduongdung.buildShortcut = false;
            this.txtduongdung.CaseSensitive = false;
            this.txtduongdung.cmdDropDown = null;
            this.txtduongdung.CompareNoID = true;
            this.txtduongdung.DefaultCode = "-1";
            this.txtduongdung.DefaultID = "-1";
            this.txtduongdung.Drug_ID = null;
            this.txtduongdung.ExtraWidth = 0;
            this.txtduongdung.FillValueAfterSelect = false;
            this.txtduongdung.LOAI_DANHMUC = null;
            this.txtduongdung.Location = new System.Drawing.Point(121, 129);
            this.txtduongdung.MaxHeight = 100;
            this.txtduongdung.MinTypedCharacters = 2;
            this.txtduongdung.MyCode = "-1";
            this.txtduongdung.MyID = "-1";
            this.txtduongdung.Name = "txtduongdung";
            this.txtduongdung.RaiseEvent = false;
            this.txtduongdung.RaiseEventEnter = false;
            this.txtduongdung.RaiseEventEnterWhenEmpty = false;
            this.txtduongdung.SelectedIndex = -1;
            this.txtduongdung.ShowCodeWithValue = false;
            this.txtduongdung.Size = new System.Drawing.Size(310, 21);
            this.txtduongdung.splitChar = '@';
            this.txtduongdung.splitCharIDAndCode = '#';
            this.txtduongdung.TabIndex = 222;
            this.txtduongdung.TakeCode = false;
            this.txtduongdung.txtMyCode = null;
            this.txtduongdung.txtMyCode_Edit = null;
            this.txtduongdung.txtMyID = null;
            this.txtduongdung.txtMyID_Edit = null;
            this.txtduongdung.txtMyName = null;
            this.txtduongdung.txtMyName_Edit = null;
            this.txtduongdung.txtNext = null;
            this.txtduongdung.txtNext1 = null;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(25, 131);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 18);
            this.label15.TabIndex = 223;
            this.label15.Text = "Đường dùng";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(25, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 18);
            this.label3.TabIndex = 38;
            this.label3.Text = "Mô tả";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtmota
            // 
            this.txtmota.Location = new System.Drawing.Point(120, 156);
            this.txtmota.Multiline = true;
            this.txtmota.Name = "txtmota";
            this.txtmota.Size = new System.Drawing.Size(310, 66);
            this.txtmota.TabIndex = 37;
            // 
            // txtMaHoatChat
            // 
            this.txtMaHoatChat.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtMaHoatChat.Location = new System.Drawing.Point(120, 51);
            this.txtMaHoatChat.Name = "txtMaHoatChat";
            this.txtMaHoatChat.Size = new System.Drawing.Size(311, 21);
            this.txtMaHoatChat.TabIndex = 2;
            this.txtMaHoatChat.TextChanged += new System.EventHandler(this.txtMaHoatChat_TextChanged);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(272, 315);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(146, 32);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Location = new System.Drawing.Point(125, 314);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(136, 32);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "&Lưu lại";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // officeFormAdorner1
            // 
            this.officeFormAdorner1.Form = this;
            this.officeFormAdorner1.Office2007CustomColor = System.Drawing.Color.Empty;
            // 
            // frm_themmoi_hoatchat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 374);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpControl);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_hoatchat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin hoạt chất";
            this.Load += new System.EventHandler(this.frm_themmoi_hoatchat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Add_SysReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThuTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.officeFormAdorner1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal Janus.Windows.GridEX.EditControls.EditBox txtIdHoatChat;
        private System.Windows.Forms.CheckBox chkhienthi;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown txtThuTu;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenHoatChat;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaHoatChat;
        private Janus.Windows.Ribbon.OfficeFormAdorner officeFormAdorner1;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtmota;
        private AutoCompleteTextbox_Danhmucchung txtduongdung;
        internal System.Windows.Forms.Label label15;
    }
}