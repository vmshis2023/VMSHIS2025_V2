namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_xemthongtingia
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
            Janus.Windows.GridEX.GridEXLayout grdGiaCLS_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_xemthongtingia));
            this.uiGroupBox5 = new Janus.Windows.EditControls.UIGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTongChiPhi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdGiaCLS = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).BeginInit();
            this.uiGroupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaCLS)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox5
            // 
            this.uiGroupBox5.Controls.Add(this.label6);
            this.uiGroupBox5.Controls.Add(this.label5);
            this.uiGroupBox5.Controls.Add(this.txtTongChiPhi);
            this.uiGroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox5.Location = new System.Drawing.Point(0, 587);
            this.uiGroupBox5.Name = "uiGroupBox5";
            this.uiGroupBox5.Size = new System.Drawing.Size(485, 0);
            this.uiGroupBox5.TabIndex = 3;
            this.uiGroupBox5.Text = "&Thông tin chi phí";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(258, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 15);
            this.label6.TabIndex = 242;
            this.label6.Text = "Vnđ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 241;
            this.label5.Text = "Chi phí";
            // 
            // txtTongChiPhi
            // 
            this.txtTongChiPhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongChiPhi.Location = new System.Drawing.Point(68, 37);
            this.txtTongChiPhi.Name = "txtTongChiPhi";
            this.txtTongChiPhi.ReadOnly = true;
            this.txtTongChiPhi.Size = new System.Drawing.Size(183, 23);
            this.txtTongChiPhi.TabIndex = 0;
            this.txtTongChiPhi.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdGiaCLS);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 741);
            this.panel1.TabIndex = 547;
            // 
            // grdGiaCLS
            // 
            this.grdGiaCLS.AlternatingColors = true;
            grdGiaCLS_DesignTimeLayout.LayoutString = resources.GetString("grdGiaCLS_DesignTimeLayout.LayoutString");
            this.grdGiaCLS.DesignTimeLayout = grdGiaCLS_DesignTimeLayout;
            this.grdGiaCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGiaCLS.DynamicFiltering = true;
            this.grdGiaCLS.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdGiaCLS.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdGiaCLS.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdGiaCLS.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGiaCLS.GroupByBoxVisible = false;
            this.grdGiaCLS.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdGiaCLS.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdGiaCLS.Location = new System.Drawing.Point(0, 0);
            this.grdGiaCLS.Name = "grdGiaCLS";
            this.grdGiaCLS.RecordNavigator = true;
            this.grdGiaCLS.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdGiaCLS.Size = new System.Drawing.Size(1008, 698);
            this.grdGiaCLS.TabIndex = 14;
            this.grdGiaCLS.UseGroupRowSelector = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 698);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 43);
            this.panel2.TabIndex = 9;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(779, 5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 30);
            this.cmdSave.TabIndex = 19;
            this.cmdSave.Text = "Chấp nhận";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Cancel;
            this.cmdCancel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCancel.Location = new System.Drawing.Point(895, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(110, 30);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Hủy";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // frm_xemthongtingia
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1008, 741);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frm_xemthongtingia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giá dịch vụ CLS từ file Excel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox5)).EndInit();
            this.uiGroupBox5.ResumeLayout(false);
            this.uiGroupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGiaCLS)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox5;

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtTongChiPhi;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdGiaCLS;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdCancel;
    }
}