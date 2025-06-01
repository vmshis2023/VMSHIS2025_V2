
namespace QMS.UCs
{
    partial class ucQMSTiepdon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQMSTiepdon));
            this.lblSoDanggoi = new System.Windows.Forms.Label();
            this.lblSTT = new System.Windows.Forms.Label();
            this.cmdLaysoQMS = new System.Windows.Forms.Button();
            this.pnlQMS = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSTT = new System.Windows.Forms.Label();
            this.lblHeaderSodanggoi = new System.Windows.Forms.Label();
            this.lblHeaderQMS = new System.Windows.Forms.Label();
            this.pnlQMS.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSoDanggoi
            // 
            this.lblSoDanggoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSoDanggoi.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSoDanggoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSoDanggoi.Location = new System.Drawing.Point(1448, 2);
            this.lblSoDanggoi.Name = "lblSoDanggoi";
            this.lblSoDanggoi.Padding = new System.Windows.Forms.Padding(2);
            this.lblSoDanggoi.Size = new System.Drawing.Size(150, 100);
            this.lblSoDanggoi.TabIndex = 1;
            this.lblSoDanggoi.Text = "SỐ ĐANG GỌI";
            this.lblSoDanggoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSTT
            // 
            this.lblSTT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSTT.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSTT.Location = new System.Drawing.Point(1298, 2);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(150, 100);
            this.lblSTT.TabIndex = 2;
            this.lblSTT.Text = "STT";
            this.lblSTT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdLaysoQMS
            // 
            this.cmdLaysoQMS.BackColor = System.Drawing.Color.CadetBlue;
            this.cmdLaysoQMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdLaysoQMS.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cmdLaysoQMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLaysoQMS.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLaysoQMS.ForeColor = System.Drawing.Color.White;
            this.cmdLaysoQMS.Location = new System.Drawing.Point(2, 2);
            this.cmdLaysoQMS.Name = "cmdLaysoQMS";
            this.cmdLaysoQMS.Size = new System.Drawing.Size(1296, 100);
            this.cmdLaysoQMS.TabIndex = 3;
            this.cmdLaysoQMS.Text = "KHÁM BẢO HIỂM Y TẾ";
            this.cmdLaysoQMS.UseVisualStyleBackColor = false;
            // 
            // pnlQMS
            // 
            this.pnlQMS.Controls.Add(this.cmdLaysoQMS);
            this.pnlQMS.Controls.Add(this.lblSTT);
            this.pnlQMS.Controls.Add(this.lblSoDanggoi);
            this.pnlQMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQMS.Location = new System.Drawing.Point(0, 45);
            this.pnlQMS.Name = "pnlQMS";
            this.pnlQMS.Padding = new System.Windows.Forms.Padding(2);
            this.pnlQMS.Size = new System.Drawing.Size(1600, 104);
            this.pnlQMS.TabIndex = 4;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblHeaderQMS);
            this.pnlHeader.Controls.Add(this.lblHeaderSTT);
            this.pnlHeader.Controls.Add(this.lblHeaderSodanggoi);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Size = new System.Drawing.Size(1600, 45);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSTT
            // 
            this.lblHeaderSTT.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblHeaderSTT.Location = new System.Drawing.Point(1298, 2);
            this.lblHeaderSTT.Name = "lblHeaderSTT";
            this.lblHeaderSTT.Size = new System.Drawing.Size(150, 41);
            this.lblHeaderSTT.TabIndex = 2;
            this.lblHeaderSTT.Text = "STT";
            this.lblHeaderSTT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeaderSodanggoi
            // 
            this.lblHeaderSodanggoi.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblHeaderSodanggoi.Location = new System.Drawing.Point(1448, 2);
            this.lblHeaderSodanggoi.Name = "lblHeaderSodanggoi";
            this.lblHeaderSodanggoi.Size = new System.Drawing.Size(150, 41);
            this.lblHeaderSodanggoi.TabIndex = 3;
            this.lblHeaderSodanggoi.Text = "SỐ ĐANG GỌI";
            this.lblHeaderSodanggoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeaderQMS
            // 
            this.lblHeaderQMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderQMS.Image = ((System.Drawing.Image)(resources.GetObject("lblHeaderQMS.Image")));
            this.lblHeaderQMS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeaderQMS.Location = new System.Drawing.Point(2, 2);
            this.lblHeaderQMS.Name = "lblHeaderQMS";
            this.lblHeaderQMS.Size = new System.Drawing.Size(1296, 41);
            this.lblHeaderQMS.TabIndex = 4;
            this.lblHeaderQMS.Text = "CHẠM ĐỂ LẤY SỐ";
            this.lblHeaderQMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucQMSTiepdon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlQMS);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucQMSTiepdon";
            this.Size = new System.Drawing.Size(1600, 149);
            this.pnlQMS.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSoDanggoi;
        private System.Windows.Forms.Label lblSTT;
        private System.Windows.Forms.Button cmdLaysoQMS;
        private System.Windows.Forms.Panel pnlQMS;
        private System.Windows.Forms.Label lblHeaderQMS;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderSodanggoi;
        private System.Windows.Forms.Label lblHeaderSTT;
    }
}
