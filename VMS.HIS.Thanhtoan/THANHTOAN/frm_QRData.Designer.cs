namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_QRData
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
            Janus.Windows.GridEX.GridEXLayout grdListQrcode_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QRData));
            this.grdListQrcode = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.grdListQrcode)).BeginInit();
            this.SuspendLayout();
            // 
            // grdListQrcode
            // 
            this.grdListQrcode.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdListQrcode.BackColor = System.Drawing.Color.Silver;
            grdListQrcode_DesignTimeLayout.LayoutString = resources.GetString("grdListQrcode_DesignTimeLayout.LayoutString");
            this.grdListQrcode.DesignTimeLayout = grdListQrcode_DesignTimeLayout;
            this.grdListQrcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListQrcode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdListQrcode.GroupByBoxVisible = false;
            this.grdListQrcode.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.grdListQrcode.Location = new System.Drawing.Point(0, 0);
            this.grdListQrcode.Name = "grdListQrcode";
            this.grdListQrcode.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListQrcode.Size = new System.Drawing.Size(1144, 802);
            this.grdListQrcode.TabIndex = 415;
            // 
            // frm_QRData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 802);
            this.Controls.Add(this.grdListQrcode);
            this.Name = "frm_QRData";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dữ liệu sinh QR Code theo lần thanh toán";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_QRData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdListQrcode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Janus.Windows.GridEX.GridEX grdListQrcode;
    }
}