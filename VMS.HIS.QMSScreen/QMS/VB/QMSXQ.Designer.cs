namespace VMS.QMS
{
    partial class QMSXQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QMSXQ));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblSeper = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlFull = new System.Windows.Forms.Panel();
            this.lblSTT = new System.Windows.Forms.Label();
            this.lblPatientInfor = new System.Windows.Forms.Label();
            this.lblNext = new VMS.QMS.UIControl.Marquee();
            this.lblAct = new System.Windows.Forms.Label();
            this.pnlQMS = new System.Windows.Forms.Panel();
            this.pnlMedia = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlFull.SuspendLayout();
            this.pnlQMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.OnTimerEvent);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlLogo);
            this.pnlTop.Controls.Add(this.lblProductName);
            this.pnlTop.Controls.Add(this.lblSeper);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // lblProductName
            // 
            resources.ApplyResources(this.lblProductName, "lblProductName");
            this.lblProductName.ForeColor = System.Drawing.Color.Purple;
            this.lblProductName.Name = "lblProductName";
            // 
            // lblSeper
            // 
            resources.ApplyResources(this.lblSeper, "lblSeper");
            this.lblSeper.Name = "lblSeper";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Name = "label1";
            // 
            // pnlLeft
            // 
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // pnlRight
            // 
            resources.ApplyResources(this.pnlRight, "pnlRight");
            this.pnlRight.Name = "pnlRight";
            // 
            // pnlFull
            // 
            this.pnlFull.Controls.Add(this.lblSTT);
            this.pnlFull.Controls.Add(this.lblPatientInfor);
            this.pnlFull.Controls.Add(this.lblNext);
            this.pnlFull.Controls.Add(this.lblAct);
            resources.ApplyResources(this.pnlFull, "pnlFull");
            this.pnlFull.Name = "pnlFull";
            // 
            // lblSTT
            // 
            this.lblSTT.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblSTT, "lblSTT");
            this.lblSTT.ForeColor = System.Drawing.Color.Red;
            this.lblSTT.Name = "lblSTT";
            // 
            // lblPatientInfor
            // 
            this.lblPatientInfor.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblPatientInfor, "lblPatientInfor");
            this.lblPatientInfor.ForeColor = System.Drawing.Color.Red;
            this.lblPatientInfor.Name = "lblPatientInfor";
            // 
            // lblNext
            // 
            resources.ApplyResources(this.lblNext, "lblNext");
            this.lblNext.Name = "lblNext";
            this.lblNext.Speed = 3;
            this.lblNext.yOffset = -1;
            // 
            // lblAct
            // 
            this.lblAct.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblAct, "lblAct");
            this.lblAct.ForeColor = System.Drawing.Color.Red;
            this.lblAct.Name = "lblAct";
            // 
            // pnlQMS
            // 
            this.pnlQMS.Controls.Add(this.pnlFull);
            this.pnlQMS.Controls.Add(this.pnlLeft);
            this.pnlQMS.Controls.Add(this.pnlRight);
            this.pnlQMS.Controls.Add(this.pnlBottom);
            this.pnlQMS.Controls.Add(this.pnlTop);
            resources.ApplyResources(this.pnlQMS, "pnlQMS");
            this.pnlQMS.Name = "pnlQMS";
            // 
            // pnlMedia
            // 
            resources.ApplyResources(this.pnlMedia, "pnlMedia");
            this.pnlMedia.Name = "pnlMedia";
            // 
            // pnlLogo
            // 
            resources.ApplyResources(this.pnlLogo, "pnlLogo");
            this.pnlLogo.Name = "pnlLogo";
            // 
            // QMSXQ
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlQMS);
            this.Controls.Add(this.pnlMedia);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "QMSXQ";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.QMSXQ_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlFull.ResumeLayout(false);
            this.pnlQMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlFull;
        private System.Windows.Forms.Label lblAct;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Panel pnlQMS;
        private System.Windows.Forms.Panel pnlMedia;
        public System.Windows.Forms.Label lblSTT;
        public System.Windows.Forms.Label lblPatientInfor;
        public System.Windows.Forms.Label label1;
        private UIControl.Marquee lblNext;
        private System.Windows.Forms.Label lblSeper;
        private System.Windows.Forms.Panel pnlLogo;
    }
}