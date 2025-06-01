namespace VietBaIT.HISLink.UI.ControlUtility.LichSuKcb_CheckThe
{
    partial class frm_LichSuKCB
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
            Janus.Windows.GridEX.GridEXLayout grdLichSuKCB_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LichSuKCB));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdLichSuKCB = new Janus.Windows.GridEX.GridEX();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSuKCB)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdLichSuKCB);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(967, 524);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin các lần khám trước";
            // 
            // grdLichSuKCB
            // 
            grdLichSuKCB_DesignTimeLayout.LayoutString = resources.GetString("grdLichSuKCB_DesignTimeLayout.LayoutString");
            this.grdLichSuKCB.DesignTimeLayout = grdLichSuKCB_DesignTimeLayout;
            this.grdLichSuKCB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLichSuKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLichSuKCB.GroupByBoxVisible = false;
            this.grdLichSuKCB.Location = new System.Drawing.Point(3, 17);
            this.grdLichSuKCB.Name = "grdLichSuKCB";
            this.grdLichSuKCB.Size = new System.Drawing.Size(961, 504);
            this.grdLichSuKCB.TabIndex = 0;
            this.grdLichSuKCB.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(843, 541);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(112, 33);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 538);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tình trạng ra viện (1: Ra viện; 2 Chuyển viện; 3: Trốn viện; 4: Xin về)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 558);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(481, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kết quả điều trị (1: Khỏi; 2 Đỡ; 3: Không thay đổi; 4: Nặng hơn; 5: Tử vong)\r\n";
            // 
            // frm_LichSuKCB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 584);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_LichSuKCB";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra lịch sử khám";
            this.Load += new System.EventHandler(this.frm_LichSuKCB_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_LichSuKCB_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frm_LichSuKCB_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLichSuKCB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdLichSuKCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}