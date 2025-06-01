namespace CIS.CoreApp
{
    partial class frm_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Settings));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdPrintSettings = new System.Windows.Forms.Button();
            this.cmdLoadSysparams = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.grdProperties = new System.Windows.Forms.PropertyGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdPrintSettings);
            this.panel1.Controls.Add(this.cmdLoadSysparams);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 518);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 44);
            this.panel1.TabIndex = 5;
            // 
            // cmdPrintSettings
            // 
            this.cmdPrintSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrintSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrintSettings.Location = new System.Drawing.Point(198, 6);
            this.cmdPrintSettings.Name = "cmdPrintSettings";
            this.cmdPrintSettings.Size = new System.Drawing.Size(92, 35);
            this.cmdPrintSettings.TabIndex = 374;
            this.cmdPrintSettings.Text = "Cấu hình in";
            this.cmdPrintSettings.UseVisualStyleBackColor = true;
            this.cmdPrintSettings.Click += new System.EventHandler(this.cmdPrintSettings_Click);
            // 
            // cmdLoadSysparams
            // 
            this.cmdLoadSysparams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadSysparams.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadSysparams.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadSysparams.Image")));
            this.cmdLoadSysparams.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near;
            this.cmdLoadSysparams.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdLoadSysparams.Location = new System.Drawing.Point(3, 6);
            this.cmdLoadSysparams.Name = "cmdLoadSysparams";
            this.cmdLoadSysparams.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdLoadSysparams.Office2007CustomColor = System.Drawing.Color.White;
            this.cmdLoadSysparams.Size = new System.Drawing.Size(189, 35);
            this.cmdLoadSysparams.TabIndex = 373;
            this.cmdLoadSysparams.Text = "Nạp lại các biến hệ thống";
            this.cmdLoadSysparams.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Near;
            this.cmdLoadSysparams.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(680, 6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(92, 35);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(575, 6);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(92, 35);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grdProperties
            // 
            this.grdProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProperties.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProperties.Location = new System.Drawing.Point(0, 0);
            this.grdProperties.Name = "grdProperties";
            this.grdProperties.Size = new System.Drawing.Size(784, 518);
            this.grdProperties.TabIndex = 7;
            // 
            // frm_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grdProperties);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Settings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình ứng dụng";
            this.Load += new System.EventHandler(this.frm_Settings_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private Janus.Windows.EditControls.UIButton cmdLoadSysparams;
        private System.Windows.Forms.Button cmdPrintSettings;
        private System.Windows.Forms.PropertyGrid grdProperties;
    }
}