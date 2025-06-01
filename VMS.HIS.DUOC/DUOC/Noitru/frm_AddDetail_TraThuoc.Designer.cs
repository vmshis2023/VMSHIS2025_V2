namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu_NoiTru
{
    partial class frm_AddDetail_TraThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AddDetail_TraThuoc));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdChapNhan = new System.Windows.Forms.Button();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtFromdate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Controls.Add(this.cmdChapNhan);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 596);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(833, 53);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Chức năng";
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(383, 13);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(121, 34);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdChapNhan
            // 
            this.cmdChapNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChapNhan.Image = ((System.Drawing.Image)(resources.GetObject("cmdChapNhan.Image")));
            this.cmdChapNhan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdChapNhan.Location = new System.Drawing.Point(250, 13);
            this.cmdChapNhan.Name = "cmdChapNhan";
            this.cmdChapNhan.Size = new System.Drawing.Size(127, 34);
            this.cmdChapNhan.TabIndex = 0;
            this.cmdChapNhan.Text = "&Chấp nhận";
            this.cmdChapNhan.UseVisualStyleBackColor = true;
            this.cmdChapNhan.Click += new System.EventHandler(this.cmdChapNhan_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 48);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(833, 548);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin ";
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin đơn thuốc</FilterRowInfoText></LocalizableData>";
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 16);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(827, 529);
            this.grdList.TabIndex = 0;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox3.Controls.Add(this.label2);
            this.uiGroupBox3.Controls.Add(this.dtToDate);
            this.uiGroupBox3.Controls.Add(this.chkByDate);
            this.uiGroupBox3.Controls.Add(this.dtFromdate);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(833, 48);
            this.uiGroupBox3.TabIndex = 2;
            this.uiGroupBox3.Text = "Tìm kiếm";
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(489, 12);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(105, 32);
            this.cmdTimKiem.TabIndex = 22;
            this.cmdTimKiem.Text = "&Tìm kiếm(F3)";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Đến ngày";
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(312, 18);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(152, 20);
            this.dtToDate.TabIndex = 20;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(27, 19);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(68, 23);
            this.chkByDate.TabIndex = 19;
            this.chkByDate.Text = "&Từ ngày";
            // 
            // dtFromdate
            // 
            this.dtFromdate.Location = new System.Drawing.Point(101, 19);
            this.dtFromdate.Name = "dtFromdate";
            this.dtFromdate.Size = new System.Drawing.Size(140, 20);
            this.dtFromdate.TabIndex = 18;
            // 
            // frm_AddDetail_TraThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 649);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_AddDetail_TraThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin trả thuốc";
            this.Load += new System.EventHandler(this.frm_AddDetail_TraThuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_AddDetail_TraThuoc_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frm_AddDetail_TraThuoc_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdChapNhan;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private System.Windows.Forms.DateTimePicker dtFromdate;
    }
}