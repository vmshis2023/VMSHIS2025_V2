using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_Dsach_Congkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Dsach_Congkham));
            Janus.Windows.GridEX.GridEXLayout grdRegExam_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.grdRegExam = new Janus.Windows.GridEX.GridEX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegExam)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 91);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(136, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(634, 44);
            this.label2.TabIndex = 542;
            this.label2.Text = "Chức năng này gắn Công khám đang chọn với Phiếu chỉ định cận lâm sàng không qua k" +
    "hám";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(543, 28);
            this.label1.TabIndex = 541;
            this.label1.Text = "CHỌN CÔNG KHÁM GẮN VỚI CHỈ ĐỊNH KHÔNG QUA KHÁM";
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
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(12, 514);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(510, 35);
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
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(668, 514);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(106, 35);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.Text = "Thoát (Esc)";
            // 
            // grdRegExam
            // 
            this.grdRegExam.AlternatingColors = true;
            this.grdRegExam.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            grdRegExam_DesignTimeLayout.LayoutString = resources.GetString("grdRegExam_DesignTimeLayout.LayoutString");
            this.grdRegExam.DesignTimeLayout = grdRegExam_DesignTimeLayout;
            this.grdRegExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRegExam.Font = new System.Drawing.Font("Arial", 9F);
            this.grdRegExam.GroupByBoxVisible = false;
            this.grdRegExam.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdRegExam.Location = new System.Drawing.Point(3, 17);
            this.grdRegExam.Name = "grdRegExam";
            this.grdRegExam.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdRegExam.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdRegExam.Size = new System.Drawing.Size(778, 391);
            this.grdRegExam.TabIndex = 630;
            this.grdRegExam.TabStop = false;
            this.grdRegExam.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdRegExam.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grdRegExam.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdRegExam);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(0, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 411);
            this.groupBox1.TabIndex = 631;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách công khám đã đăng ký";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(556, 514);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 35);
            this.cmdSave.TabIndex = 632;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frm_Dsach_Congkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_Dsach_Congkham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển Phiếu chỉ định không qua khám vào công khám";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRegExam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.GridEX.GridEX grdRegExam;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.EditControls.UIButton cmdSave;
    }
}