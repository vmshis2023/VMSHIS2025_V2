namespace QMS.UCs
{
    partial class ucQMSPCN_Item
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
            this.cmdGetQMS = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkInsokhac = new SView.UCs.medCheckBox();
            this.chkSoUutien = new SView.UCs.medCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkMore = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmrMore = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.lblQMSNumber = new System.Windows.Forms.TextBox();
            this.ctxOptions = new System.Windows.Forms.ContextMenuStrip();
            this.mnuReprint = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMore)).BeginInit();
            this.ctxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdGetQMS
            // 
            this.cmdGetQMS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdGetQMS.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetQMS.Location = new System.Drawing.Point(0, 146);
            this.cmdGetQMS.Name = "cmdGetQMS";
            this.cmdGetQMS.Size = new System.Drawing.Size(480, 65);
            this.cmdGetQMS.TabIndex = 28;
            this.cmdGetQMS.Text = "Buồng 1";
            this.cmdGetQMS.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkInsokhac);
            this.panel1.Controls.Add(this.chkSoUutien);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 56);
            this.panel1.TabIndex = 29;
            // 
            // chkInsokhac
            // 
            this.chkInsokhac._FontColor = System.Drawing.Color.Navy;
            this.chkInsokhac.AllowOnCheck = true;
            this.chkInsokhac.BackColor = System.Drawing.Color.Transparent;
            this.chkInsokhac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkInsokhac.FontText = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsokhac.ForeColor = System.Drawing.Color.Navy;
            this.chkInsokhac.IsChecked = false;
            this.chkInsokhac.Location = new System.Drawing.Point(239, 0);
            this.chkInsokhac.Margin = new System.Windows.Forms.Padding(0);
            this.chkInsokhac.Name = "chkInsokhac";
            this.chkInsokhac.Size = new System.Drawing.Size(241, 56);
            this.chkInsokhac.TabIndex = 44;
            this.chkInsokhac.YourText = "Lấy số khác?";
            // 
            // chkSoUutien
            // 
            this.chkSoUutien._FontColor = System.Drawing.Color.Red;
            this.chkSoUutien.AllowOnCheck = true;
            this.chkSoUutien.BackColor = System.Drawing.Color.Transparent;
            this.chkSoUutien.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSoUutien.FontText = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSoUutien.ForeColor = System.Drawing.Color.Red;
            this.chkSoUutien.IsChecked = false;
            this.chkSoUutien.Location = new System.Drawing.Point(0, 0);
            this.chkSoUutien.Margin = new System.Windows.Forms.Padding(0);
            this.chkSoUutien.Name = "chkSoUutien";
            this.chkSoUutien.Size = new System.Drawing.Size(239, 56);
            this.chkSoUutien.TabIndex = 43;
            this.chkSoUutien.YourText = "Lấy số ưu tiên";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkMore);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nmrMore);
            this.panel2.Location = new System.Drawing.Point(3, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 20);
            this.panel2.TabIndex = 42;
            this.panel2.Visible = false;
            // 
            // chkMore
            // 
            this.chkMore.AutoSize = true;
            this.chkMore.BackColor = System.Drawing.Color.Transparent;
            this.chkMore.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.chkMore.Location = new System.Drawing.Point(3, 8);
            this.chkMore.Name = "chkMore";
            this.chkMore.Size = new System.Drawing.Size(86, 20);
            this.chkMore.TabIndex = 39;
            this.chkMore.Text = "Lấy thêm";
            this.chkMore.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(174, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 41;
            this.label1.Text = "(số nữa)";
            // 
            // nmrMore
            // 
            this.nmrMore.Enabled = false;
            this.nmrMore.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.nmrMore.Location = new System.Drawing.Point(95, 7);
            this.nmrMore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrMore.Name = "nmrMore";
            this.nmrMore.Size = new System.Drawing.Size(73, 22);
            this.nmrMore.TabIndex = 40;
            this.nmrMore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrMore.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp:";
            // 
            // lblQMSNumber
            // 
            this.lblQMSNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQMSNumber.ContextMenuStrip = this.ctxOptions;
            this.lblQMSNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQMSNumber.Font = new System.Drawing.Font("Arial", 50.25F, System.Drawing.FontStyle.Bold);
            this.lblQMSNumber.Location = new System.Drawing.Point(0, 0);
            this.lblQMSNumber.Name = "lblQMSNumber";
            this.lblQMSNumber.ReadOnly = true;
            this.lblQMSNumber.Size = new System.Drawing.Size(480, 85);
            this.lblQMSNumber.TabIndex = 31;
            this.lblQMSNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.lblQMSNumber, "Số thứ tự khám theo phòng khám");
            // 
            // ctxOptions
            // 
            this.ctxOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReprint});
            this.ctxOptions.Name = "ctxOptions";
            this.ctxOptions.Size = new System.Drawing.Size(144, 26);
            // 
            // mnuReprint
            // 
            this.mnuReprint.CheckOnClick = true;
            this.mnuReprint.Name = "mnuReprint";
            this.mnuReprint.Size = new System.Drawing.Size(143, 22);
            this.mnuReprint.Text = "In lại số QMS";
            this.mnuReprint.Click += new System.EventHandler(this.mnuReprint_Click_1);
            // 
            // ucQMSPCN_Item
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblQMSNumber);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdGetQMS);
            this.Name = "ucQMSPCN_Item";
            this.Size = new System.Drawing.Size(480, 211);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMore)).EndInit();
            this.ctxOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmrMore;
        private System.Windows.Forms.CheckBox chkMore;
        private System.Windows.Forms.ToolTip toolTip1;
        private SView.UCs.medCheckBox chkSoUutien;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox lblQMSNumber;
        private System.Windows.Forms.ContextMenuStrip ctxOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuReprint;
        public System.Windows.Forms.Button cmdGetQMS;
        private SView.UCs.medCheckBox chkInsokhac;
    }
}
