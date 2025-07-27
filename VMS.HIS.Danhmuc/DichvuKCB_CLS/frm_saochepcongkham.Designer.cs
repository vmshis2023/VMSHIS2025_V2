using VNS.HIS.UCs;
namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_saochepcongkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_saochepcongkham));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdChuyen = new Janus.Windows.EditControls.UIButton();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPKMoi = new System.Windows.Forms.Label();
            this.cboPhongkhamNguon = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.cboPhongkhamdich = new VNS.HIS.UCs.EasyCompletionComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 122);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(136, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(570, 42);
            this.label5.TabIndex = 544;
            this.label5.Text = "Bước 3: Nhấn nút sao chép để thực hiện sao chép công khám phòng khám nguồn sang p" +
    "hòng khám đích";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(136, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(570, 21);
            this.label3.TabIndex = 543;
            this.label3.Text = "Bước 2: Chọn phòng khám mới của Bác sĩ mới";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(136, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(570, 21);
            this.label2.TabIndex = 542;
            this.label2.Text = "Bước 1: Chọn phòng khám của Bác sĩ đang làm việc. ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 28);
            this.label1.TabIndex = 541;
            this.label1.Text = "Sao chép công khám";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(26, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(59, 56);
            this.panel2.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(179, 222);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(441, 26);
            this.lblMsg.TabIndex = 555;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdChuyen
            // 
            this.cmdChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdChuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyen.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyen.Image")));
            this.cmdChuyen.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChuyen.Location = new System.Drawing.Point(463, 331);
            this.cmdChuyen.Name = "cmdChuyen";
            this.cmdChuyen.Size = new System.Drawing.Size(120, 35);
            this.cmdChuyen.TabIndex = 7;
            this.cmdChuyen.Text = "Sao chép";
            this.toolTip1.SetToolTip(this.cmdChuyen, "Ctrl+S hoặc Ctrl+A");
            this.cmdChuyen.Click += new System.EventHandler(this.cmdChuyen_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(589, 331);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Thoát (Esc)";
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(7, 306);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(706, 22);
            this.vbLine1.TabIndex = 554;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Chọn hành động";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(54, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 21);
            this.label4.TabIndex = 573;
            this.label4.Text = "Phòng khám nguồn";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPKMoi
            // 
            this.lblPKMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPKMoi.ForeColor = System.Drawing.Color.Red;
            this.lblPKMoi.Location = new System.Drawing.Point(54, 177);
            this.lblPKMoi.Name = "lblPKMoi";
            this.lblPKMoi.Size = new System.Drawing.Size(122, 21);
            this.lblPKMoi.TabIndex = 575;
            this.lblPKMoi.Text = "Phòng khám đích";
            this.lblPKMoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPhongkhamNguon
            // 
            this.cboPhongkhamNguon.FormattingEnabled = true;
            this.cboPhongkhamNguon.Location = new System.Drawing.Point(183, 150);
            this.cboPhongkhamNguon.Name = "cboPhongkhamNguon";
            this.cboPhongkhamNguon.Next_Control = null;
            this.cboPhongkhamNguon.RaiseEnterEventWhenInvisible = true;
            this.cboPhongkhamNguon.Size = new System.Drawing.Size(492, 21);
            this.cboPhongkhamNguon.TabIndex = 0;
            // 
            // cboPhongkhamdich
            // 
            this.cboPhongkhamdich.FormattingEnabled = true;
            this.cboPhongkhamdich.Location = new System.Drawing.Point(182, 177);
            this.cboPhongkhamdich.Name = "cboPhongkhamdich";
            this.cboPhongkhamdich.Next_Control = null;
            this.cboPhongkhamdich.RaiseEnterEventWhenInvisible = true;
            this.cboPhongkhamdich.Size = new System.Drawing.Size(492, 21);
            this.cboPhongkhamdich.TabIndex = 1;
            // 
            // frm_saochepcongkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 391);
            this.Controls.Add(this.cboPhongkhamdich);
            this.Controls.Add(this.cboPhongkhamNguon);
            this.Controls.Add(this.lblPKMoi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdChuyen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_saochepcongkham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sao chép công khám";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.EditControls.UIButton cmdChuyen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private VNS.UCs.VBLine vbLine1;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPKMoi;
        private EasyCompletionComboBox cboPhongkhamNguon;
        private EasyCompletionComboBox cboPhongkhamdich;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}