namespace VMS.QMS.UIControl
{
    partial class UISoPhong_ChucNang
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtSokham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdNhanSo = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSokham);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdNhanSo);
            this.splitContainer1.Size = new System.Drawing.Size(300, 200);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtSokham
            // 
            this.txtSokham.BackColor = System.Drawing.Color.Beige;
            this.txtSokham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtSokham.DisabledBackColor = System.Drawing.Color.Beige;
            this.txtSokham.DisabledForeColor = System.Drawing.Color.Black;
            this.txtSokham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSokham.Font = new System.Drawing.Font("Arial", 40F, System.Drawing.FontStyle.Bold);
            this.txtSokham.Location = new System.Drawing.Point(0, 0);
            this.txtSokham.Name = "txtSokham";
            this.txtSokham.ReadOnly = true;
            this.txtSokham.Size = new System.Drawing.Size(300, 69);
            this.txtSokham.TabIndex = 1;
            this.txtSokham.TabStop = false;
            this.txtSokham.Text = "00";
            this.txtSokham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSokham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // cmdNhanSo
            // 
            this.cmdNhanSo.ActiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdNhanSo.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdNhanSo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdNhanSo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.cmdNhanSo.Location = new System.Drawing.Point(0, 0);
            this.cmdNhanSo.Name = "cmdNhanSo";
            this.cmdNhanSo.Size = new System.Drawing.Size(300, 96);
            this.cmdNhanSo.TabIndex = 1;
            this.cmdNhanSo.Text = "uiButton1";
            this.cmdNhanSo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdNhanSo.Click += new System.EventHandler(this.cmdNhanSo_Click);
            // 
            // UISoPhong_ChucNang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UISoPhong_ChucNang";
            this.Size = new System.Drawing.Size(300, 200);
            this.Load += new System.EventHandler(this.UISoPhong_ChucNang_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIButton cmdNhanSo;
        public Janus.Windows.GridEX.EditControls.EditBox txtSokham;
    }
}