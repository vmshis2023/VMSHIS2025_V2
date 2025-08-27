


using VMS.HIS.UI.EMR.Ucs;

namespace VMS.HIS.UI.EMR
{
    partial class frm_KhoitaoBA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_KhoitaoBA));
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.txtBSlamBA = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgayBA = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cboLoaiBA = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIDBenhAn = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMaBenhAn = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(208, 228);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(129, 35);
            this.cmdSave.TabIndex = 164;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Thực hiện khởi tạo Bệnh án";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.HIS.EMR.Properties.Resources.close_24;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(343, 228);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(129, 35);
            this.cmdExit.TabIndex = 165;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // txtBSlamBA
            // 
            this.txtBSlamBA._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBSlamBA._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBSlamBA._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBSlamBA.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBSlamBA.AutoCompleteList")));
            this.txtBSlamBA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBSlamBA.buildShortcut = false;
            this.txtBSlamBA.CaseSensitive = false;
            this.txtBSlamBA.CompareNoID = true;
            this.txtBSlamBA.DefaultCode = "-1";
            this.txtBSlamBA.DefaultID = "-1";
            this.txtBSlamBA.DisplayType = 0;
            this.txtBSlamBA.Drug_ID = null;
            this.txtBSlamBA.ExtraWidth = 0;
            this.txtBSlamBA.FillValueAfterSelect = false;
            this.txtBSlamBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.txtBSlamBA.Location = new System.Drawing.Point(156, 101);
            this.txtBSlamBA.MaxHeight = 289;
            this.txtBSlamBA.MinTypedCharacters = 2;
            this.txtBSlamBA.MyCode = "-1";
            this.txtBSlamBA.MyID = "-1";
            this.txtBSlamBA.MyText = "";
            this.txtBSlamBA.MyTextOnly = "";
            this.txtBSlamBA.Name = "txtBSlamBA";
            this.txtBSlamBA.RaiseEvent = true;
            this.txtBSlamBA.RaiseEventEnter = true;
            this.txtBSlamBA.RaiseEventEnterWhenEmpty = true;
            this.txtBSlamBA.SelectedIndex = -1;
            this.txtBSlamBA.Size = new System.Drawing.Size(254, 23);
            this.txtBSlamBA.splitChar = '@';
            this.txtBSlamBA.splitCharIDAndCode = '#';
            this.txtBSlamBA.TabIndex = 506;
            this.txtBSlamBA.TakeCode = false;
            this.txtBSlamBA.txtMyCode = null;
            this.txtBSlamBA.txtMyCode_Edit = null;
            this.txtBSlamBA.txtMyID = null;
            this.txtBSlamBA.txtMyID_Edit = null;
            this.txtBSlamBA.txtMyName = null;
            this.txtBSlamBA.txtMyName_Edit = null;
            this.txtBSlamBA.txtNext = null;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(23, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 22);
            this.label5.TabIndex = 507;
            this.label5.Text = "BS làm BA:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgayBA
            // 
            this.dtpNgayBA.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayBA.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayBA.DropDownCalendar.Name = "";
            this.dtpNgayBA.DropDownCalendar.Visible = false;
            this.dtpNgayBA.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgayBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.dtpNgayBA.Location = new System.Drawing.Point(157, 72);
            this.dtpNgayBA.Name = "dtpNgayBA";
            this.dtpNgayBA.ShowUpDown = true;
            this.dtpNgayBA.Size = new System.Drawing.Size(253, 23);
            this.dtpNgayBA.TabIndex = 505;
            this.dtpNgayBA.Value = new System.DateTime(2025, 6, 2, 0, 0, 0, 0);
            // 
            // cboLoaiBA
            // 
            this.cboLoaiBA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.cboLoaiBA.FormattingEnabled = true;
            this.cboLoaiBA.Location = new System.Drawing.Point(157, 13);
            this.cboLoaiBA.Name = "cboLoaiBA";
            this.cboLoaiBA.Size = new System.Drawing.Size(253, 24);
            this.cboLoaiBA.TabIndex = 502;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(23, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 510;
            this.label2.Text = "Loại BA:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIDBenhAn
            // 
            this.txtIDBenhAn.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtIDBenhAn.Enabled = false;
            this.txtIDBenhAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.txtIDBenhAn.Location = new System.Drawing.Point(354, 44);
            this.txtIDBenhAn.Name = "txtIDBenhAn";
            this.txtIDBenhAn.Size = new System.Drawing.Size(56, 23);
            this.txtIDBenhAn.TabIndex = 504;
            this.txtIDBenhAn.TabStop = false;
            this.txtIDBenhAn.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // txtMaBenhAn
            // 
            this.txtMaBenhAn.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtMaBenhAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.txtMaBenhAn.Location = new System.Drawing.Point(157, 44);
            this.txtMaBenhAn.Name = "txtMaBenhAn";
            this.txtMaBenhAn.Size = new System.Drawing.Size(150, 23);
            this.txtMaBenhAn.TabIndex = 503;
            this.txtMaBenhAn.TabStop = false;
            this.txtMaBenhAn.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(310, 42);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(38, 24);
            this.label38.TabIndex = 509;
            this.label38.Text = "ID :";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(23, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 24);
            this.label10.TabIndex = 508;
            this.label10.Text = "Mã Bệnh Án :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(23, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 24);
            this.label3.TabIndex = 511;
            this.label3.Text = "Ngày khởi tạo BA:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblStatus.Location = new System.Drawing.Point(12, 138);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(460, 76);
            this.lblStatus.TabIndex = 512;
            // 
            // frm_KhoitaoBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 275);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtBSlamBA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpNgayBA);
            this.Controls.Add(this.cboLoaiBA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIDBenhAn);
            this.Controls.Add(this.txtMaBenhAn);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_KhoitaoBA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khởi tạo Bệnh án";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private VNS.HIS.UCs.AutoCompleteTextbox txtBSlamBA;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayBA;
        private System.Windows.Forms.ComboBox cboLoaiBA;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtIDBenhAn;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaBenhAn;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
    }
}