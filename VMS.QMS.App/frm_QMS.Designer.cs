namespace VNS.QMS
{
    partial class frm_QMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QMS));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdConfig = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabkhoaKcb = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabkhoaKcb.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdConfig.BackgroundImage")));
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdConfig.Location = new System.Drawing.Point(978, 0);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(40, 35);
            this.cmdConfig.TabIndex = 45;
            this.toolTip1.SetToolTip(this.cmdConfig, "Nhấn vào đây để cấu hình một số thông tin");
            this.cmdConfig.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 57);
            this.panel1.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Italic);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(75, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(943, 57);
            this.label4.TabIndex = 26;
            this.label4.Text = "Bạn nhấn vào các nút Lấy số để thực hiện cấp phát số. Muốn in nhiều số bắt đầu từ" +
    " số đang có, bạn chọn mục Lấy thêm và nhập số lượng số cần in thêm, sau đó nhấn " +
    "nút Lấy số để thực hiện in";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 57);
            this.panel2.TabIndex = 25;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.SteelBlue;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1018, 35);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "BỆNH VIỆN NỘI TIẾT TW CƠ SỞ II";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdConfig);
            this.panel3.Controls.Add(this.tabkhoaKcb);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.lblMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1018, 740);
            this.panel3.TabIndex = 27;
            // 
            // tabkhoaKcb
            // 
            this.tabkhoaKcb.Controls.Add(this.tabPage2);
            this.tabkhoaKcb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabkhoaKcb.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabkhoaKcb.Location = new System.Drawing.Point(0, 92);
            this.tabkhoaKcb.Name = "tabkhoaKcb";
            this.tabkhoaKcb.SelectedIndex = 0;
            this.tabkhoaKcb.Size = new System.Drawing.Size(1018, 648);
            this.tabkhoaKcb.TabIndex = 48;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1010, 611);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Số khám bệnh khoa KCB thường";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frm_QMS
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm_QMS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LẤY SỐ CHỜ TIẾP ĐÓN KCB";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabkhoaKcb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button cmdConfig;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabkhoaKcb;
        private System.Windows.Forms.TabPage tabPage2;
    }
}