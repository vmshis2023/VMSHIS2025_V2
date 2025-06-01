namespace VNS.HIS.UI.Forms.Noitru
{
    partial class frm_chonbacsidieutri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_chonbacsidieutri));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAct = new System.Windows.Forms.Panel();
            this.pnlBsi = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBacsi = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.pnlTop.SuspendLayout();
            this.pnlAct.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmdSearch);
            this.pnlTop.Controls.Add(this.txtBacsi);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1008, 54);
            this.pnlTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm bác sĩ điều trị";
            // 
            // pnlAct
            // 
            this.pnlAct.Controls.Add(this.cmdAccept);
            this.pnlAct.Controls.Add(this.cmdExit);
            this.pnlAct.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAct.Location = new System.Drawing.Point(0, 675);
            this.pnlAct.Name = "pnlAct";
            this.pnlAct.Size = new System.Drawing.Size(1008, 54);
            this.pnlAct.TabIndex = 0;
            // 
            // pnlBsi
            // 
            this.pnlBsi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBsi.Location = new System.Drawing.Point(0, 54);
            this.pnlBsi.Name = "pnlBsi";
            this.pnlBsi.Size = new System.Drawing.Size(1008, 675);
            this.pnlBsi.TabIndex = 0;
            // 
            // txtBacsi
            // 
            this.txtBacsi._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtBacsi._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsi._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBacsi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBacsi.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtBacsi.AutoCompleteList")));
            this.txtBacsi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBacsi.CaseSensitive = false;
            this.txtBacsi.CompareNoID = true;
            this.txtBacsi.DefaultCode = "-1";
            this.txtBacsi.DefaultID = "-1";
            this.txtBacsi.DisplayType = 1;
            this.txtBacsi.Drug_ID = null;
            this.txtBacsi.ExtraWidth = 0;
            this.txtBacsi.FillValueAfterSelect = false;
            this.txtBacsi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBacsi.Location = new System.Drawing.Point(170, 18);
            this.txtBacsi.MaxHeight = 289;
            this.txtBacsi.MinTypedCharacters = 2;
            this.txtBacsi.MyCode = "-1";
            this.txtBacsi.MyID = "-1";
            this.txtBacsi.MyText = "";
            this.txtBacsi.Name = "txtBacsi";
            this.txtBacsi.RaiseEvent = true;
            this.txtBacsi.RaiseEventEnter = true;
            this.txtBacsi.RaiseEventEnterWhenEmpty = true;
            this.txtBacsi.SelectedIndex = -1;
            this.txtBacsi.Size = new System.Drawing.Size(808, 21);
            this.txtBacsi.splitChar = '@';
            this.txtBacsi.splitCharIDAndCode = '#';
            this.txtBacsi.TabIndex = 0;
            this.txtBacsi.TakeCode = false;
            this.txtBacsi.txtMyCode = null;
            this.txtBacsi.txtMyCode_Edit = null;
            this.txtBacsi.txtMyID = null;
            this.txtBacsi.txtMyID_Edit = null;
            this.txtBacsi.txtMyName = null;
            this.txtBacsi.txtMyName_Edit = null;
            this.txtBacsi.txtNext = null;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = global::VMS.Resources.Properties.Resources.SAVE1;
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(760, 9);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(120, 35);
            this.cmdAccept.TabIndex = 7;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::VMS.Resources.Properties.Resources.arrow_left_11;
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(886, 9);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 35);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.Location = new System.Drawing.Point(977, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(21, 21);
            this.cmdSearch.TabIndex = 17;
            this.cmdSearch.TabStop = false;
            this.cmdSearch.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // frm_chonbacsidieutri
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pnlAct);
            this.Controls.Add(this.pnlBsi);
            this.Controls.Add(this.pnlTop);
            this.Name = "frm_chonbacsidieutri";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn bác sĩ điều trị";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlAct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlAct;
        private System.Windows.Forms.FlowLayoutPanel pnlBsi;
        private UCs.AutoCompleteTextbox txtBacsi;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSearch;
    }
}