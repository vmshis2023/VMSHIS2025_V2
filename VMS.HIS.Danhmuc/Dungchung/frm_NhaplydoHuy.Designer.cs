namespace VNS.HIS.UI.THUOC
{
    partial class frm_NhaplydoHuy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NhaplydoHuy));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblName = new System.Windows.Forms.Label();
            this.txtDmucchung = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtNgaythuchien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle2);
            this.panel1.Controls.Add(this.lblTitle1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 63);
            this.panel1.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle2.Location = new System.Drawing.Point(108, 33);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(358, 21);
            this.lblTitle2.TabIndex = 542;
            this.lblTitle2.Text = "Nhập lý do hủy chốt thuốc trước khi thực hiện...";
            this.lblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(108, 9);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(358, 21);
            this.lblTitle1.TabIndex = 541;
            this.lblTitle1.Text = "Hủy chốt thuốc";
            this.lblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(71, 61);
            this.panel2.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSave.Location = new System.Drawing.Point(328, 219);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(459, 219);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(2, 196);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(583, 22);
            this.vbLine1.TabIndex = 9;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Hành động";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Red;
            this.lblName.Location = new System.Drawing.Point(4, 118);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(122, 21);
            this.lblName.TabIndex = 540;
            this.lblName.Text = "Lý do hủy:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDmucchung
            // 
            this.txtDmucchung._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDmucchung._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDmucchung._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDmucchung.AddValues = true;
            this.txtDmucchung.AllowMultiline = false;
            this.txtDmucchung.AutoCompleteList = null;
            this.txtDmucchung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDmucchung.CaseSensitive = false;
            this.txtDmucchung.CompareNoID = true;
            this.txtDmucchung.DefaultCode = "-1";
            this.txtDmucchung.DefaultID = "-1";
            this.txtDmucchung.Drug_ID = null;
            this.txtDmucchung.ExtraWidth = 0;
            this.txtDmucchung.FillValueAfterSelect = false;
            this.txtDmucchung.Font = new System.Drawing.Font("Arial", 11.25F);
            this.txtDmucchung.LOAI_DANHMUC = "LYDOHUYCHOTTHUOC";
            this.txtDmucchung.Location = new System.Drawing.Point(132, 115);
            this.txtDmucchung.MaxHeight = -1;
            this.txtDmucchung.MinTypedCharacters = 2;
            this.txtDmucchung.MyCode = "-1";
            this.txtDmucchung.MyID = "-1";
            this.txtDmucchung.Name = "txtDmucchung";
            this.txtDmucchung.RaiseEvent = false;
            this.txtDmucchung.RaiseEventEnter = false;
            this.txtDmucchung.RaiseEventEnterWhenEmpty = false;
            this.txtDmucchung.SelectedIndex = -1;
            this.txtDmucchung.ShowCodeWithValue = false;
            this.txtDmucchung.Size = new System.Drawing.Size(424, 25);
            this.txtDmucchung.splitChar = '@';
            this.txtDmucchung.splitCharIDAndCode = '#';
            this.txtDmucchung.TabIndex = 4;
            this.txtDmucchung.TakeCode = false;
            this.txtDmucchung.txtMyCode = null;
            this.txtDmucchung.txtMyCode_Edit = null;
            this.txtDmucchung.txtMyID = null;
            this.txtDmucchung.txtMyID_Edit = null;
            this.txtDmucchung.txtMyName = null;
            this.txtDmucchung.txtMyName_Edit = null;
            this.txtDmucchung.txtNext = null;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(-1, 165);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(580, 27);
            this.lblMsg.TabIndex = 545;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(63, 92);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(57, 15);
            this.lblDate.TabIndex = 547;
            this.lblDate.Text = "Ngày hủy";
            // 
            // dtNgaythuchien
            // 
            this.dtNgaythuchien.CustomFormat = "dd/MM/yyyy";
            this.dtNgaythuchien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgaythuchien.DropDownCalendar.Name = "";
            this.dtNgaythuchien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtNgaythuchien.Enabled = false;
            this.dtNgaythuchien.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgaythuchien.Location = new System.Drawing.Point(132, 87);
            this.dtNgaythuchien.Name = "dtNgaythuchien";
            this.dtNgaythuchien.ShowUpDown = true;
            this.dtNgaythuchien.Size = new System.Drawing.Size(133, 24);
            this.dtNgaythuchien.TabIndex = 0;
            this.dtNgaythuchien.TabStop = false;
            // 
            // frm_NhaplydoHuy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(592, 266);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtNgaythuchien);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtDmucchung);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.vbLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_NhaplydoHuy";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hủy chốt thuốc";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblName;
        private UCs.AutoCompleteTextbox_Danhmucchung txtDmucchung;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgaythuchien;


    }
}