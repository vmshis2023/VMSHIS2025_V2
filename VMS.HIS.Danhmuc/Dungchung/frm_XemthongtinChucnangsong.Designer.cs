namespace VMS.HIS.Danhmuc.Dungchung
{
    partial class frm_XemthongtinChucnangsong
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
            Janus.Windows.GridEX.GridEXLayout grdChucnangsong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_XemthongtinChucnangsong));
            this.grdChucnangsong = new Janus.Windows.GridEX.GridEX();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdChucnangsong)).BeginInit();
            this.SuspendLayout();
            // 
            // grdChucnangsong
            // 
            this.grdChucnangsong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdChucnangsong.AlternatingColors = true;
            grdChucnangsong_DesignTimeLayout.LayoutString = resources.GetString("grdChucnangsong_DesignTimeLayout.LayoutString");
            this.grdChucnangsong.DesignTimeLayout = grdChucnangsong_DesignTimeLayout;
            this.grdChucnangsong.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdChucnangsong.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdChucnangsong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.grdChucnangsong.GroupByBoxVisible = false;
            this.grdChucnangsong.Location = new System.Drawing.Point(0, 0);
            this.grdChucnangsong.Name = "grdChucnangsong";
            this.grdChucnangsong.RecordNavigator = true;
            this.grdChucnangsong.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChucnangsong.Size = new System.Drawing.Size(1008, 682);
            this.grdChucnangsong.TabIndex = 3;
            this.grdChucnangsong.TabStop = false;
            this.grdChucnangsong.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.close_24;
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(876, 688);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(120, 35);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Thoát (Esc)";
            this.cmdClose.ToolTipText = "Nhấn vào đây để thoát khỏi chức năng";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(745, 688);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(120, 35);
            this.cmdSave.TabIndex = 8;
            this.cmdSave.Text = "Chấp nhận";
            this.cmdSave.ToolTipText = "Phím tắt Ctrl+S";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frm_XemthongtinChucnangsong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grdChucnangsong);
            this.Name = "frm_XemthongtinChucnangsong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin theo dõi chức năng sống";
            this.Load += new System.EventHandler(this.frm_XemthongtinChucnangsong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdChucnangsong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grdChucnangsong;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdSave;

    }
}