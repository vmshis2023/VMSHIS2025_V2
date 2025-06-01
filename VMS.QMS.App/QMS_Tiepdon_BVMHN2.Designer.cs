
namespace QMS
{
    partial class QMS_Tiepdon_BVMHN2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QMS_Tiepdon_BVMHN2));
            this.cmdConfig = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.flowQMS = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdConfig.BackgroundImage")));
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdConfig.Location = new System.Drawing.Point(967, 3);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(61, 61);
            this.cmdConfig.TabIndex = 46;
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 654);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1031, 100);
            this.pnlBottom.TabIndex = 47;
            // 
            // pnlTop
            // 
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlTop.Controls.Add(this.cmdConfig);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1031, 100);
            this.pnlTop.TabIndex = 0;
            // 
            // flowQMS
            // 
            this.flowQMS.AutoScroll = true;
            this.flowQMS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowQMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowQMS.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowQMS.Location = new System.Drawing.Point(0, 100);
            this.flowQMS.Name = "flowQMS";
            this.flowQMS.Size = new System.Drawing.Size(1031, 554);
            this.flowQMS.TabIndex = 48;
            this.flowQMS.WrapContents = false;
            // 
            // QMS_Tiepdon_BVMHN2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 754);
            this.Controls.Add(this.flowQMS);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "QMS_Tiepdon_BVMHN2";
            this.ShowIcon = false;
            this.Text = "HỆ THỐNG QMS - QUẢN LÝ LẤY SỐ HÀNG ĐỢI TIẾP ĐÓN";
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdConfig;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.FlowLayoutPanel flowQMS;
    }
}