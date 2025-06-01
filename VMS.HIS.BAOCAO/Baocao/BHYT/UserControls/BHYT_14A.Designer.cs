namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    partial class BHYT_14A
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHYT_14A));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdPreview = new Janus.Windows.EditControls.UIButton();
            this.pnlfunctions = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtPrintDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optOld = new System.Windows.Forms.RadioButton();
            this.optNew = new System.Windows.Forms.RadioButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtNhomBHYT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cboObject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lbFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.pnlfunctions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(9, 40);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(466, 10);
            this.prgBar.TabIndex = 18;
            this.prgBar.Visible = false;
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.Enabled = false;
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExcel.Location = new System.Drawing.Point(869, 7);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(140, 39);
            this.cmdExcel.TabIndex = 17;
            this.cmdExcel.Text = "Xuất Excel";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(564, 7);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(140, 39);
            this.cmdPrint.TabIndex = 13;
            this.cmdPrint.Text = "In";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPreview.Enabled = false;
            this.cmdPreview.Image = ((System.Drawing.Image)(resources.GetObject("cmdPreview.Image")));
            this.cmdPreview.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPreview.Location = new System.Drawing.Point(711, 7);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(152, 39);
            this.cmdPreview.TabIndex = 12;
            this.cmdPreview.Text = "Xem trước khi in";
            // 
            // pnlfunctions
            // 
            this.pnlfunctions.Controls.Add(this.prgBar);
            this.pnlfunctions.Controls.Add(this.cmdExcel);
            this.pnlfunctions.Controls.Add(this.cmdPrint);
            this.pnlfunctions.Controls.Add(this.label2);
            this.pnlfunctions.Controls.Add(this.label5);
            this.pnlfunctions.Controls.Add(this.cmdPreview);
            this.pnlfunctions.Controls.Add(this.dtPrintDate);
            this.pnlfunctions.Controls.Add(this.groupBox1);
            this.pnlfunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlfunctions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlfunctions.Location = new System.Drawing.Point(0, 545);
            this.pnlfunctions.Name = "pnlfunctions";
            this.pnlfunctions.Size = new System.Drawing.Size(1024, 55);
            this.pnlfunctions.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 23);
            this.label2.TabIndex = 51;
            this.label2.Text = "Mẫu báo cáo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(308, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 50;
            this.label5.Text = "Ngày in:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtPrintDate
            // 
            this.dtPrintDate.CustomFormat = "dd/MM/yyyy";
            this.dtPrintDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtPrintDate.DropDownCalendar.Name = "";
            this.dtPrintDate.ForeColor = System.Drawing.Color.Black;
            this.dtPrintDate.Location = new System.Drawing.Point(415, 17);
            this.dtPrintDate.Name = "dtPrintDate";
            this.dtPrintDate.ShowUpDown = true;
            this.dtPrintDate.Size = new System.Drawing.Size(126, 21);
            this.dtPrintDate.TabIndex = 49;
            this.dtPrintDate.Value = new System.DateTime(2014, 10, 7, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optOld);
            this.groupBox1.Controls.Add(this.optNew);
            this.groupBox1.Location = new System.Drawing.Point(113, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 38);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // optOld
            // 
            this.optOld.AutoSize = true;
            this.optOld.Location = new System.Drawing.Point(108, 12);
            this.optOld.Name = "optOld";
            this.optOld.Size = new System.Drawing.Size(64, 19);
            this.optOld.TabIndex = 1;
            this.optOld.Text = "Mẫu cũ";
            this.optOld.UseVisualStyleBackColor = true;
            // 
            // optNew
            // 
            this.optNew.AutoSize = true;
            this.optNew.Checked = true;
            this.optNew.Location = new System.Drawing.Point(24, 12);
            this.optNew.Name = "optNew";
            this.optNew.Size = new System.Drawing.Size(73, 19);
            this.optNew.TabIndex = 0;
            this.optNew.TabStop = true;
            this.optNew.Text = "Mẫu mới";
            this.optNew.UseVisualStyleBackColor = true;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.baocaO_TIEUDE1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1024, 31);
            this.pnlHeader.TabIndex = 4;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "BHYT14A";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1024, 31);
            this.baocaO_TIEUDE1.TabIndex = 3;
            this.baocaO_TIEUDE1.TIEUDE = "THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ CÁC NHÓM ĐỐI TƯỢNG THEO TUYỄN CHUYÊN MÔN KỸ THUẬT";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtNhomBHYT);
            this.pnlSearch.Controls.Add(this.cboObject);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.cmdSearch);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.dtpToDate);
            this.pnlSearch.Controls.Add(this.lbFromDate);
            this.pnlSearch.Controls.Add(this.dtpFromDate);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 31);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1024, 52);
            this.pnlSearch.TabIndex = 8;
            // 
            // txtNhomBHYT
            // 
            this.txtNhomBHYT._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNhomBHYT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomBHYT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhomBHYT.AutoCompleteList")));
            this.txtNhomBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhomBHYT.CaseSensitive = false;
            this.txtNhomBHYT.CompareNoID = true;
            this.txtNhomBHYT.DefaultCode = "-1";
            this.txtNhomBHYT.DefaultID = "-1";
            this.txtNhomBHYT.Drug_ID = null;
            this.txtNhomBHYT.ExtraWidth = 100;
            this.txtNhomBHYT.FillValueAfterSelect = false;
            this.txtNhomBHYT.LOAI_DANHMUC = "NHOMBHYT";
            this.txtNhomBHYT.Location = new System.Drawing.Point(482, 17);
            this.txtNhomBHYT.MaxHeight = 300;
            this.txtNhomBHYT.MinTypedCharacters = 2;
            this.txtNhomBHYT.MyCode = "-1";
            this.txtNhomBHYT.MyID = "-1";
            this.txtNhomBHYT.Name = "txtNhomBHYT";
            this.txtNhomBHYT.RaiseEvent = false;
            this.txtNhomBHYT.RaiseEventEnter = false;
            this.txtNhomBHYT.RaiseEventEnterWhenEmpty = false;
            this.txtNhomBHYT.SelectedIndex = -1;
            this.txtNhomBHYT.Size = new System.Drawing.Size(381, 21);
            this.txtNhomBHYT.splitChar = '@';
            this.txtNhomBHYT.splitCharIDAndCode = '#';
            this.txtNhomBHYT.TabIndex = 54;
            this.txtNhomBHYT.TakeCode = false;
            this.txtNhomBHYT.txtMyCode = null;
            this.txtNhomBHYT.txtMyCode_Edit = null;
            this.txtNhomBHYT.txtMyID = null;
            this.txtNhomBHYT.txtMyID_Edit = null;
            this.txtNhomBHYT.txtMyName = null;
            this.txtNhomBHYT.txtMyName_Edit = null;
            this.txtNhomBHYT.txtNext = null;
            // 
            // cboObject
            // 
            this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObject.FormattingEnabled = true;
            this.cboObject.Location = new System.Drawing.Point(113, 50);
            this.cboObject.Name = "cboObject";
            this.cboObject.Size = new System.Drawing.Size(303, 23);
            this.cboObject.TabIndex = 2;
            this.cboObject.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(426, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 53;
            this.label4.Text = "Nhóm";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(0, 0);
            this.cmdSearch.Location = new System.Drawing.Point(881, 8);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(140, 38);
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(252, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 23);
            this.label3.TabIndex = 45;
            this.label3.Text = "đến";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpToDate.DropDownCalendar.Name = "";
            this.dtpToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtpToDate.Location = new System.Drawing.Point(292, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowUpDown = true;
            this.dtpToDate.Size = new System.Drawing.Size(124, 21);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.Value = new System.DateTime(2014, 10, 7, 0, 0, 0, 0);
            // 
            // lbFromDate
            // 
            this.lbFromDate.Location = new System.Drawing.Point(6, 17);
            this.lbFromDate.Name = "lbFromDate";
            this.lbFromDate.Size = new System.Drawing.Size(101, 23);
            this.lbFromDate.TabIndex = 44;
            this.lbFromDate.Text = "Từ ngày";
            this.lbFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpFromDate.DropDownCalendar.FirstDayOfWeek = Janus.Windows.CalendarCombo.CalendarDayOfWeek.Sunday;
            this.dtpFromDate.DropDownCalendar.Name = "";
            this.dtpFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtpFromDate.Location = new System.Drawing.Point(113, 17);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.Value = new System.DateTime(2014, 10, 7, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(-2, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 43;
            this.label1.Text = "Đối tượng KCB:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdList);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 83);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1024, 462);
            this.pnlMain.TabIndex = 9;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1024, 462);
            this.grdList.TabIndex = 272;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // BHYT_14A
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlfunctions);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BHYT_14A";
            this.Size = new System.Drawing.Size(1024, 600);
            this.pnlfunctions.ResumeLayout(false);
            this.pnlfunctions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.ProgressBar prgBar;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdPreview;
        private System.Windows.Forms.Panel pnlfunctions;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtPrintDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optOld;
        private System.Windows.Forms.RadioButton optNew;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpToDate;
        private System.Windows.Forms.Label lbFromDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ComboBox cboObject;
        private BAOCAO_TIEUDE baocaO_TIEUDE1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNhomBHYT;
    }
}
