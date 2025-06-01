namespace VietBaIT.HISLink.UI.ControlUtility.LichSuCLS
{
    partial class frm_LichSuCLS
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LichSuCLS));
            Janus.Windows.GridEX.GridEXLayout grdKetQua_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdExportExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNam = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.cmdGetData = new Janus.Windows.EditControls.UIButton();
            this.radNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdKetQua = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmdExportExcel);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 603);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 46);
            this.panel1.TabIndex = 1;
            // 
            // cmdExportExcel
            // 
            this.cmdExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportExcel.Location = new System.Drawing.Point(310, 8);
            this.cmdExportExcel.Name = "cmdExportExcel";
            this.cmdExportExcel.Size = new System.Drawing.Size(111, 30);
            this.cmdExportExcel.TabIndex = 1;
            this.cmdExportExcel.Text = "Xuất Excel";
            this.cmdExportExcel.Click += new System.EventHandler(this.cmdExportExcel_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(427, 8);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(99, 30);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdList);
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(5, 54);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(341, 543);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "Thông tin cận lâm sàng";
            // 
            // grdList
            // 
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(3, 17);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.Size = new System.Drawing.Size(335, 523);
            this.grdList.TabIndex = 0;
            this.grdList.SelectionChanged += new System.EventHandler(this.grdList_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtNam);
            this.panel2.Controls.Add(this.cmdGetData);
            this.panel2.Controls.Add(this.radNoiTru);
            this.panel2.Controls.Add(this.radNgoaiTru);
            this.panel2.Controls.Add(this.radTatCa);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(875, 48);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Năm tra cứu";
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(114, 12);
            this.txtNam.MaxLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Numeric = true;
            this.txtNam.Size = new System.Drawing.Size(100, 20);
            this.txtNam.TabIndex = 4;
            this.txtNam.Text = "2017";
            this.txtNam.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // cmdGetData
            // 
            this.cmdGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetData.Location = new System.Drawing.Point(492, 5);
            this.cmdGetData.Name = "cmdGetData";
            this.cmdGetData.Size = new System.Drawing.Size(116, 34);
            this.cmdGetData.TabIndex = 3;
            this.cmdGetData.Text = "Lấy dữ liệu";
            this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(386, 12);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(68, 23);
            this.radNoiTru.TabIndex = 2;
            this.radNoiTru.Text = "Nội trú";
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.Checked = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(312, 12);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(68, 23);
            this.radNgoaiTru.TabIndex = 1;
            this.radNgoaiTru.TabStop = true;
            this.radNgoaiTru.Text = "Ngoại trú";
            // 
            // radTatCa
            // 
            this.radTatCa.Checked = true;
            this.radTatCa.Location = new System.Drawing.Point(238, 12);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(68, 23);
            this.radTatCa.TabIndex = 0;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdKetQua);
            this.uiGroupBox2.Location = new System.Drawing.Point(352, 54);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(511, 540);
            this.uiGroupBox2.TabIndex = 4;
            this.uiGroupBox2.Text = "Kết quả xét nghiệm";
            // 
            // grdKetQua
            // 
            this.grdKetQua.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            grdKetQua_DesignTimeLayout.LayoutString = resources.GetString("grdKetQua_DesignTimeLayout.LayoutString");
            this.grdKetQua.DesignTimeLayout = grdKetQua_DesignTimeLayout;
            this.grdKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetQua.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKetQua.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdKetQua.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKetQua.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdKetQua.GroupByBoxVisible = false;
            this.grdKetQua.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdKetQua.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKetQua.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKetQua.Location = new System.Drawing.Point(3, 16);
            this.grdKetQua.Name = "grdKetQua";
            this.grdKetQua.RecordNavigator = true;
            this.grdKetQua.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetQua.Size = new System.Drawing.Size(505, 521);
            this.grdKetQua.TabIndex = 14;
            this.grdKetQua.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_LichSuCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 649);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_LichSuCLS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử chỉ định";
            this.Load += new System.EventHandler(this.frm_LichSuCLS_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetQua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdExportExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdGetData;
        private Janus.Windows.EditControls.UIRadioButton radNoiTru;
        private Janus.Windows.EditControls.UIRadioButton radNgoaiTru;
        private Janus.Windows.EditControls.UIRadioButton radTatCa;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtNam;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdKetQua;
    }
}