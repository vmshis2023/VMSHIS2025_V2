namespace VMS.QMS
{
    partial class frm_SoKham_GoiLai
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
            Janus.Windows.GridEX.GridEXLayout grdListGoiLaiSoKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SoKham_GoiLai));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdReset4me = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.grdListGoiLaiSoKham = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdConfig = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdReset4me);
            this.panel1.Controls.Add(this.cmdReset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 673);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 56);
            this.panel1.TabIndex = 4;
            // 
            // cmdReset4me
            // 
            this.cmdReset4me.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdReset4me.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset4me.Location = new System.Drawing.Point(271, 0);
            this.cmdReset4me.Name = "cmdReset4me";
            this.cmdReset4me.Size = new System.Drawing.Size(278, 56);
            this.cmdReset4me.TabIndex = 4;
            this.cmdReset4me.Text = "Kích hoạt số QMS đang chọn dùng cho chính máy tiếp đón này (F2)";
            this.toolTip1.SetToolTip(this.cmdReset4me, "Kích hoạt số QMS đang chọn dùng cho chính máy tiếp đón này");
            this.cmdReset4me.UseVisualStyleBackColor = true;
            this.cmdReset4me.Click += new System.EventHandler(this.cmdReset4me_Click_1);
            // 
            // cmdReset
            // 
            this.cmdReset.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdReset.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdReset.Location = new System.Drawing.Point(0, 0);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(271, 56);
            this.cmdReset.TabIndex = 3;
            this.cmdReset.Text = "Kích hoạt lại các số QMS dùng cho cả các máy tiếp đón khác (F1)";
            this.toolTip1.SetToolTip(this.cmdReset, "Kích hoạt tất cả các số QMS đang chọn để dùng lại(Các máy tiếp đón khác cũng dùng" +
        " lại được các số QMS này)");
            this.cmdReset.UseVisualStyleBackColor = true;
            // 
            // grdListGoiLaiSoKham
            // 
            grdListGoiLaiSoKham_DesignTimeLayout.LayoutString = resources.GetString("grdListGoiLaiSoKham_DesignTimeLayout.LayoutString");
            this.grdListGoiLaiSoKham.DesignTimeLayout = grdListGoiLaiSoKham_DesignTimeLayout;
            this.grdListGoiLaiSoKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListGoiLaiSoKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdListGoiLaiSoKham.GroupByBoxVisible = false;
            this.grdListGoiLaiSoKham.Location = new System.Drawing.Point(0, 0);
            this.grdListGoiLaiSoKham.Name = "grdListGoiLaiSoKham";
            this.grdListGoiLaiSoKham.RecordNavigator = true;
            this.grdListGoiLaiSoKham.Size = new System.Drawing.Size(1008, 673);
            this.grdListGoiLaiSoKham.TabIndex = 5;
            this.grdListGoiLaiSoKham.UseGroupRowSelector = true;
            this.grdListGoiLaiSoKham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
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
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdConfig.Location = new System.Drawing.Point(965, 0);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(40, 40);
            this.cmdConfig.TabIndex = 6;
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // frm_SoKham_GoiLai
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.cmdConfig);
            this.Controls.Add(this.grdListGoiLaiSoKham);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SoKham_GoiLai";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gọi lại số khám bị hủy hoặc bỏ qua";
            this.Load += new System.EventHandler(this.frm_SoKham_GoiLai_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListGoiLaiSoKham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdReset4me;
        private System.Windows.Forms.Button cmdReset;
        private Janus.Windows.GridEX.GridEX grdListGoiLaiSoKham;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdConfig;


    }
}