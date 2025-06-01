namespace VNS.HIS.UI.Forms.HinhAnh
{
    partial class frm_Nhapketqua_HA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Nhapketqua_HA));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.pnl = new System.Windows.Forms.Panel();
            this.txtsDesc = new RicherTextBox.RicherTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdSave);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 683);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 47);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            this.uiGroupBox1.Click += new System.EventHandler(this.uiGroupBox1_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Location = new System.Drawing.Point(402, 9);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 35);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "&Chấp nhận";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(518, 9);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(110, 35);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl.Location = new System.Drawing.Point(0, 683);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1008, 0);
            this.pnl.TabIndex = 1;
            // 
            // txtsDesc
            // 
            this.txtsDesc.AlignCenterVisible = true;
            this.txtsDesc.AlignLeftVisible = true;
            this.txtsDesc.AlignRightVisible = true;
            this.txtsDesc.BoldVisible = true;
            this.txtsDesc.BulletsVisible = true;
            this.txtsDesc.ChooseFontVisible = true;
            this.txtsDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsDesc.FindReplaceVisible = true;
            this.txtsDesc.FontColorVisible = true;
            this.txtsDesc.FontFamilyVisible = true;
            this.txtsDesc.FontSizeVisible = true;
            this.txtsDesc.GroupAlignmentVisible = true;
            this.txtsDesc.GroupBoldUnderlineItalicVisible = true;
            this.txtsDesc.GroupFontColorVisible = true;
            this.txtsDesc.GroupFontNameAndSizeVisible = true;
            this.txtsDesc.GroupIndentationAndBulletsVisible = true;
            this.txtsDesc.GroupInsertVisible = true;
            this.txtsDesc.GroupSaveAndLoadVisible = true;
            this.txtsDesc.GroupZoomVisible = false;
            this.txtsDesc.INDENT = 10;
            this.txtsDesc.IndentVisible = true;
            this.txtsDesc.InsertPictureVisible = true;
            this.txtsDesc.ItalicVisible = true;
            this.txtsDesc.LoadVisible = true;
            this.txtsDesc.Location = new System.Drawing.Point(0, 0);
            this.txtsDesc.Name = "txtsDesc";
            this.txtsDesc.OutdentVisible = true;
            this.txtsDesc.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
    " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
            this.txtsDesc.SaveVisible = true;
            this.txtsDesc.SeparatorAlignVisible = true;
            this.txtsDesc.SeparatorBoldUnderlineItalicVisible = true;
            this.txtsDesc.SeparatorFontColorVisible = true;
            this.txtsDesc.SeparatorFontVisible = true;
            this.txtsDesc.SeparatorIndentAndBulletsVisible = true;
            this.txtsDesc.SeparatorInsertVisible = true;
            this.txtsDesc.SeparatorSaveLoadVisible = true;
            this.txtsDesc.Size = new System.Drawing.Size(1008, 683);
            this.txtsDesc.TabIndex = 4;
            this.txtsDesc.ToolStripVisible = true;
            this.txtsDesc.UnderlineVisible = true;
            this.txtsDesc.WordWrapVisible = true;
            this.txtsDesc.ZoomFactorTextVisible = false;
            this.txtsDesc.ZoomInVisible = true;
            this.txtsDesc.ZoomOutVisible = false;
            // 
            // frm_Nhapketqua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.txtsDesc);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Nhapketqua";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soạn thảo văn bản";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Nhapketqua_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Nhapketqua_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Panel pnl;
        public RicherTextBox.RicherTextBox txtsDesc;
    }
}