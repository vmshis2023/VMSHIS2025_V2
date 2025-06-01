using VNS.Libs.Utilities.UserControls;
using VNS.UCs;
namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_ChonDoituongKCB
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
            Janus.Windows.GridEX.GridEXLayout grdObjectTypeList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonDoituongKCB));
            this.chkCheckAllorNone = new System.Windows.Forms.CheckBox();
            this.grdObjectTypeList = new Janus.Windows.GridEX.GridEX();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectTypeList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCheckAllorNone
            // 
            this.chkCheckAllorNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCheckAllorNone.AutoSize = true;
            this.chkCheckAllorNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCheckAllorNone.Location = new System.Drawing.Point(6, 298);
            this.chkCheckAllorNone.Name = "chkCheckAllorNone";
            this.chkCheckAllorNone.Size = new System.Drawing.Size(145, 20);
            this.chkCheckAllorNone.TabIndex = 14;
            this.chkCheckAllorNone.Text = "Chọn tất cả/bỏ chọn";
            this.chkCheckAllorNone.UseVisualStyleBackColor = true;
            this.chkCheckAllorNone.CheckedChanged += new System.EventHandler(this.chkCheckAllorNone_CheckedChanged);
            // 
            // grdObjectTypeList
            // 
            this.grdObjectTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdObjectTypeList.ColumnAutoResize = true;
            grdObjectTypeList_DesignTimeLayout.LayoutString = resources.GetString("grdObjectTypeList_DesignTimeLayout.LayoutString");
            this.grdObjectTypeList.DesignTimeLayout = grdObjectTypeList_DesignTimeLayout;
            this.grdObjectTypeList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdObjectTypeList.GroupByBoxVisible = false;
            this.grdObjectTypeList.Location = new System.Drawing.Point(6, 75);
            this.grdObjectTypeList.Name = "grdObjectTypeList";
            this.grdObjectTypeList.Size = new System.Drawing.Size(584, 217);
            this.grdObjectTypeList.TabIndex = 17;
            this.grdObjectTypeList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.SystemColors.ControlText;
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(6, 325);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(584, 22);
            this.vbLine1.TabIndex = 16;
            this.vbLine1.YourText = "Tính năng";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = global::VMS.HIS.Danhmuc.Properties.Resources.Cancel;
            this.cmdClose.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdClose.Location = new System.Drawing.Point(475, 344);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(103, 30);
            this.cmdClose.TabIndex = 29;
            this.cmdClose.Text = "Hủy bỏ";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = global::VMS.HIS.Danhmuc.Properties.Resources.tick2424;
            this.cmdAccept.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAccept.Location = new System.Drawing.Point(351, 344);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(103, 30);
            this.cmdAccept.TabIndex = 28;
            this.cmdAccept.Text = "Đồng ý";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 69);
            this.panel1.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(64, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 69);
            this.label1.TabIndex = 32;
            this.label1.Text = "CHỌN ĐỐI TƯỢNG KHÁM CHỮA BỆNH";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(64, 69);
            this.panel2.TabIndex = 31;
            // 
            // frm_ChonDoituongKCB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 386);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.grdObjectTypeList);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.chkCheckAllorNone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonDoituongKCB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục đối tượng khám chữa bệnh";
            this.Load += new System.EventHandler(this.frm_ChonDoituongKCB_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonDoituongKCB_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectTypeList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VBLine vbLine1;
        private System.Windows.Forms.CheckBox chkCheckAllorNone;
        private Janus.Windows.GridEX.GridEX grdObjectTypeList;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}