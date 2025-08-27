
namespace VMS.HIS.Duoc.DUOC
{
    partial class frm_bienban_kiemnhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_bienban_kiemnhap));
            Janus.Windows.GridEX.GridEXLayout grd_bspt_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.txt_hoten = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txt_chucdanh = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_ngaybienban = new System.Windows.Forms.DateTimePicker();
            this.lbl_ngaykiemnhap = new System.Windows.Forms.Label();
            this.txt_chucvu = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.grd_bspt = new Janus.Windows.GridEX.GridEX();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.lbl_title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bspt)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_hoten
            // 
            this.txt_hoten._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_hoten._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_hoten._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_hoten.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_hoten.AutoCompleteList")));
            this.txt_hoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_hoten.buildShortcut = false;
            this.txt_hoten.CaseSensitive = false;
            this.txt_hoten.CompareNoID = true;
            this.txt_hoten.DefaultCode = "-1";
            this.txt_hoten.DefaultID = "-1";
            this.txt_hoten.DisplayType = 0;
            this.txt_hoten.Drug_ID = null;
            this.txt_hoten.ExtraWidth = 300;
            this.txt_hoten.FillValueAfterSelect = false;
            this.txt_hoten.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_hoten.Location = new System.Drawing.Point(133, 80);
            this.txt_hoten.MaxHeight = 289;
            this.txt_hoten.MinTypedCharacters = 2;
            this.txt_hoten.MyCode = "-1";
            this.txt_hoten.MyID = "-1";
            this.txt_hoten.MyText = "";
            this.txt_hoten.MyTextOnly = "";
            this.txt_hoten.Name = "txt_hoten";
            this.txt_hoten.RaiseEvent = true;
            this.txt_hoten.RaiseEventEnter = true;
            this.txt_hoten.RaiseEventEnterWhenEmpty = true;
            this.txt_hoten.SelectedIndex = -1;
            this.txt_hoten.Size = new System.Drawing.Size(257, 22);
            this.txt_hoten.splitChar = '@';
            this.txt_hoten.splitCharIDAndCode = '#';
            this.txt_hoten.TabIndex = 1;
            this.txt_hoten.TakeCode = false;
            this.txt_hoten.txtMyCode = null;
            this.txt_hoten.txtMyCode_Edit = null;
            this.txt_hoten.txtMyID = null;
            this.txt_hoten.txtMyID_Edit = null;
            this.txt_hoten.txtMyName = null;
            this.txt_hoten.txtMyName_Edit = null;
            this.txt_hoten.txtNext = null;
            // 
            // txt_chucdanh
            // 
            this.txt_chucdanh._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_chucdanh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucdanh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_chucdanh.AddValues = true;
            this.txt_chucdanh.AllowMultiline = false;
            this.txt_chucdanh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_chucdanh.AutoCompleteList")));
            this.txt_chucdanh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_chucdanh.buildShortcut = false;
            this.txt_chucdanh.CaseSensitive = false;
            this.txt_chucdanh.cmdDropDown = null;
            this.txt_chucdanh.CompareNoID = true;
            this.txt_chucdanh.DefaultCode = "-1";
            this.txt_chucdanh.DefaultID = "-1";
            this.txt_chucdanh.Drug_ID = null;
            this.txt_chucdanh.ExtraWidth = 300;
            this.txt_chucdanh.FillValueAfterSelect = false;
            this.txt_chucdanh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucdanh.LOAI_DANHMUC = "NVIEN_CHUCDANH_NN";
            this.txt_chucdanh.Location = new System.Drawing.Point(133, 108);
            this.txt_chucdanh.MaxHeight = 200;
            this.txt_chucdanh.MinTypedCharacters = 2;
            this.txt_chucdanh.MyCode = "-1";
            this.txt_chucdanh.MyID = "-1";
            this.txt_chucdanh.Name = "txt_chucdanh";
            this.txt_chucdanh.RaiseEvent = false;
            this.txt_chucdanh.RaiseEventEnter = false;
            this.txt_chucdanh.RaiseEventEnterWhenEmpty = false;
            this.txt_chucdanh.SelectedIndex = -1;
            this.txt_chucdanh.SetDefaultWhenInit = false;
            this.txt_chucdanh.ShowCodeWithValue = false;
            this.txt_chucdanh.Size = new System.Drawing.Size(257, 22);
            this.txt_chucdanh.splitChar = '@';
            this.txt_chucdanh.splitCharIDAndCode = '#';
            this.txt_chucdanh.TabIndex = 2;
            this.txt_chucdanh.TakeCode = false;
            this.txt_chucdanh.txtMyCode = null;
            this.txt_chucdanh.txtMyCode_Edit = null;
            this.txt_chucdanh.txtMyID = null;
            this.txt_chucdanh.txtMyID_Edit = null;
            this.txt_chucdanh.txtMyName = null;
            this.txt_chucdanh.txtMyName_Edit = null;
            this.txt_chucdanh.txtNext = null;
            this.txt_chucdanh.txtNext1 = null;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(23, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 18);
            this.label12.TabIndex = 28;
            this.label12.Text = "Họ tên:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(23, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 18);
            this.label9.TabIndex = 27;
            this.label9.Text = "Chức danh:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp_ngaybienban
            // 
            this.dtp_ngaybienban.CustomFormat = "dd/MM/yyyy :HH:mm";
            this.dtp_ngaybienban.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ngaybienban.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ngaybienban.Location = new System.Drawing.Point(133, 52);
            this.dtp_ngaybienban.Name = "dtp_ngaybienban";
            this.dtp_ngaybienban.ShowUpDown = true;
            this.dtp_ngaybienban.Size = new System.Drawing.Size(171, 22);
            this.dtp_ngaybienban.TabIndex = 0;
            // 
            // lbl_ngaykiemnhap
            // 
            this.lbl_ngaykiemnhap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ngaykiemnhap.ForeColor = System.Drawing.Color.Red;
            this.lbl_ngaykiemnhap.Location = new System.Drawing.Point(23, 54);
            this.lbl_ngaykiemnhap.Name = "lbl_ngaykiemnhap";
            this.lbl_ngaykiemnhap.Size = new System.Drawing.Size(108, 18);
            this.lbl_ngaykiemnhap.TabIndex = 30;
            this.lbl_ngaykiemnhap.Text = "Ngày kiểm nhập: ";
            this.lbl_ngaykiemnhap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_chucvu
            // 
            this.txt_chucvu._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txt_chucvu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucvu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_chucvu.AddValues = true;
            this.txt_chucvu.AllowMultiline = false;
            this.txt_chucvu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txt_chucvu.AutoCompleteList")));
            this.txt_chucvu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_chucvu.buildShortcut = false;
            this.txt_chucvu.CaseSensitive = false;
            this.txt_chucvu.cmdDropDown = null;
            this.txt_chucvu.CompareNoID = true;
            this.txt_chucvu.DefaultCode = "-1";
            this.txt_chucvu.DefaultID = "-1";
            this.txt_chucvu.Drug_ID = null;
            this.txt_chucvu.ExtraWidth = 300;
            this.txt_chucvu.FillValueAfterSelect = false;
            this.txt_chucvu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chucvu.LOAI_DANHMUC = "NVIEN_CHUCVU";
            this.txt_chucvu.Location = new System.Drawing.Point(471, 108);
            this.txt_chucvu.MaxHeight = 200;
            this.txt_chucvu.MinTypedCharacters = 2;
            this.txt_chucvu.MyCode = "-1";
            this.txt_chucvu.MyID = "-1";
            this.txt_chucvu.Name = "txt_chucvu";
            this.txt_chucvu.RaiseEvent = false;
            this.txt_chucvu.RaiseEventEnter = false;
            this.txt_chucvu.RaiseEventEnterWhenEmpty = false;
            this.txt_chucvu.SelectedIndex = -1;
            this.txt_chucvu.SetDefaultWhenInit = false;
            this.txt_chucvu.ShowCodeWithValue = false;
            this.txt_chucvu.Size = new System.Drawing.Size(257, 22);
            this.txt_chucvu.splitChar = '@';
            this.txt_chucvu.splitCharIDAndCode = '#';
            this.txt_chucvu.TabIndex = 3;
            this.txt_chucvu.TakeCode = false;
            this.txt_chucvu.txtMyCode = null;
            this.txt_chucvu.txtMyCode_Edit = null;
            this.txt_chucvu.txtMyID = null;
            this.txt_chucvu.txtMyID_Edit = null;
            this.txt_chucvu.txtMyName = null;
            this.txt_chucvu.txtMyName_Edit = null;
            this.txt_chucvu.txtNext = null;
            this.txt_chucvu.txtNext1 = null;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(394, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 32;
            this.label1.Text = "Chức vụ:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(39, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 33;
            this.label2.Text = "STT hiển thị:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.numericUpDown1.Location = new System.Drawing.Point(133, 136);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown1.TabIndex = 4;
            // 
            // grd_bspt
            // 
            this.grd_bspt.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_bspt.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grd_bspt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grd_bspt_DesignTimeLayout.LayoutString = resources.GetString("grd_bspt_DesignTimeLayout.LayoutString");
            this.grd_bspt.DesignTimeLayout = grd_bspt_DesignTimeLayout;
            this.grd_bspt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_bspt.GroupByBoxVisible = false;
            this.grd_bspt.Location = new System.Drawing.Point(133, 164);
            this.grd_bspt.Name = "grd_bspt";
            this.grd_bspt.Size = new System.Drawing.Size(655, 259);
            this.grd_bspt.TabIndex = 572;
            this.grd_bspt.TabStop = false;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = global::VMS.HIS.Duoc.Properties.Resources.printer_32;
            this.cmdPrint.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdPrint.Location = new System.Drawing.Point(416, 450);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 35);
            this.cmdPrint.TabIndex = 11;
            this.cmdPrint.Text = "In (Ctrl+S)";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdSave.Location = new System.Drawing.Point(542, 450);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Text = "Lưu (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.Duoc.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(22, 22);
            this.cmdExit.Location = new System.Drawing.Point(668, 450);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_msg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(12, 447);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(398, 35);
            this.lbl_msg.TabIndex = 576;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.SystemColors.WindowText;
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.vbLine1.Location = new System.Drawing.Point(13, 426);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(774, 22);
            this.vbLine1.TabIndex = 100;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Thực hiện";
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.Navy;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(800, 39);
            this.lbl_title.TabIndex = 578;
            this.lbl_title.Text = "BIÊN BẢN KIỂM NHẬP";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_bienban_kiemnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 497);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grd_bspt);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_chucvu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_ngaybienban);
            this.Controls.Add(this.lbl_ngaykiemnhap);
            this.Controls.Add(this.txt_hoten);
            this.Controls.Add(this.txt_chucdanh);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.vbLine1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_bienban_kiemnhap";
            this.ShowIcon = false;
            this.Text = "Biên bản kiểm nhập";
            this.Load += new System.EventHandler(this.frm_bienban_kiemnhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_bspt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VNS.HIS.UCs.AutoCompleteTextbox txt_hoten;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txt_chucdanh;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtp_ngaybienban;
        private System.Windows.Forms.Label lbl_ngaykiemnhap;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txt_chucvu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Janus.Windows.GridEX.GridEX grd_bspt;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label lbl_msg;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label lbl_title;
    }
}