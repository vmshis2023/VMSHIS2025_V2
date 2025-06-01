namespace VNS.HIS.Danhmuc
{
    partial class frmTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.cmdInhoadon = new Janus.Windows.EditControls.UIButton();
            this.txtTest = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.SuspendLayout();
            // 
            // cmdInhoadon
            // 
            this.cmdInhoadon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInhoadon.Image = ((System.Drawing.Image)(resources.GetObject("cmdInhoadon.Image")));
            this.cmdInhoadon.Location = new System.Drawing.Point(416, 70);
            this.cmdInhoadon.Name = "cmdInhoadon";
            this.cmdInhoadon.Size = new System.Drawing.Size(91, 27);
            this.cmdInhoadon.TabIndex = 39;
            this.cmdInhoadon.TabStop = false;
            this.cmdInhoadon.Text = "Test";
            this.cmdInhoadon.ToolTipText = "Nhấn vào đây để in hóa đơn thanh toán";
            this.cmdInhoadon.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdInhoadon.Click += new System.EventHandler(this.cmdInhoadon_Click);
            // 
            // txtTest
            // 
            this.txtTest.BackColor = System.Drawing.Color.White;
            this.txtTest.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTest.Location = new System.Drawing.Point(41, 73);
            this.txtTest.MaxLength = 100;
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(366, 23);
            this.txtTest.TabIndex = 38;
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 109);
            this.Controls.Add(this.cmdInhoadon);
            this.Controls.Add(this.txtTest);
            this.Name = "frmTest";
            this.Text = "frmTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdInhoadon;
        public Janus.Windows.GridEX.EditControls.MaskedEditBox txtTest;

    }
}

