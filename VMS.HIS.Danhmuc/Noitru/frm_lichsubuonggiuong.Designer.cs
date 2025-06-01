namespace VNS.HIS.UI.Forms.Noitru
{
    partial class frm_lichsubuonggiuong
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
            Janus.Windows.GridEX.GridEXLayout grd_List_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_lichsubuonggiuong));
            this.grd_List = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.autoKhoa = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoBuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.autoGiuong = new VNS.HIS.UCs.AutoCompleteTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grd_List
            // 
            this.grd_List.AlternatingColors = true;
            this.grd_List.AlternatingRowFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Flat;
            this.grd_List.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grd_List.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin mã bệnh ICD</FilterRowInfoText></LocalizableData>";
            this.grd_List.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.grd_List.DefaultAlphaMode = Janus.Windows.GridEX.AlphaMode.UseAlpha;
            this.grd_List.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grd_List_DesignTimeLayout.LayoutString = resources.GetString("grd_List_DesignTimeLayout.LayoutString");
            this.grd_List.DesignTimeLayout = grd_List_DesignTimeLayout;
            this.grd_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_List.DynamicFiltering = true;
            this.grd_List.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grd_List.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grd_List.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grd_List.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grd_List.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grd_List.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grd_List.FocusCellFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grd_List.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grd_List.FrozenColumns = 1;
            this.grd_List.GroupByBoxVisible = false;
            this.grd_List.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grd_List.Location = new System.Drawing.Point(0, 64);
            this.grd_List.Name = "grd_List";
            this.grd_List.RecordNavigator = true;
            this.grd_List.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_List.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_List.SelectedFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grd_List.Size = new System.Drawing.Size(1008, 616);
            this.grd_List.TabIndex = 4;
            this.grd_List.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.autoKhoa);
            this.panel1.Controls.Add(this.autoBuong);
            this.panel1.Controls.Add(this.autoGiuong);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 64);
            this.panel1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 23);
            this.label4.TabIndex = 25;
            this.label4.Text = "Khoa nội trú(F2) :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(460, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 23);
            this.label9.TabIndex = 27;
            this.label9.Text = "Giường :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(44, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 23);
            this.label10.TabIndex = 26;
            this.label10.Text = "Buồng :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 680);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 49);
            this.panel2.TabIndex = 0;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(853, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(143, 37);
            this.cmdExit.TabIndex = 3;
            this.cmdExit.Text = "Thoát (Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // autoKhoa
            // 
            this.autoKhoa._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoKhoa._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoKhoa.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoKhoa.AutoCompleteList")));
            this.autoKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoKhoa.buildShortcut = false;
            this.autoKhoa.CaseSensitive = false;
            this.autoKhoa.CompareNoID = true;
            this.autoKhoa.DefaultCode = "-1";
            this.autoKhoa.DefaultID = "-1";
            this.autoKhoa.DisplayType = 1;
            this.autoKhoa.Drug_ID = null;
            this.autoKhoa.ExtraWidth = 0;
            this.autoKhoa.FillValueAfterSelect = false;
            this.autoKhoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoKhoa.Location = new System.Drawing.Point(115, 12);
            this.autoKhoa.MaxHeight = 289;
            this.autoKhoa.MinTypedCharacters = 2;
            this.autoKhoa.MyCode = "-1";
            this.autoKhoa.MyID = "-1";
            this.autoKhoa.MyText = "";
            this.autoKhoa.MyTextOnly = "";
            this.autoKhoa.Name = "autoKhoa";
            this.autoKhoa.RaiseEvent = true;
            this.autoKhoa.RaiseEventEnter = true;
            this.autoKhoa.RaiseEventEnterWhenEmpty = true;
            this.autoKhoa.SelectedIndex = -1;
            this.autoKhoa.Size = new System.Drawing.Size(881, 21);
            this.autoKhoa.splitChar = '@';
            this.autoKhoa.splitCharIDAndCode = '#';
            this.autoKhoa.TabIndex = 0;
            this.autoKhoa.TakeCode = false;
            this.autoKhoa.txtMyCode = null;
            this.autoKhoa.txtMyCode_Edit = null;
            this.autoKhoa.txtMyID = null;
            this.autoKhoa.txtMyID_Edit = null;
            this.autoKhoa.txtMyName = null;
            this.autoKhoa.txtMyName_Edit = null;
            this.autoKhoa.txtNext = null;
            // 
            // autoBuong
            // 
            this.autoBuong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoBuong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoBuong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoBuong.AutoCompleteList")));
            this.autoBuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoBuong.buildShortcut = false;
            this.autoBuong.CaseSensitive = false;
            this.autoBuong.CompareNoID = true;
            this.autoBuong.DefaultCode = "-1";
            this.autoBuong.DefaultID = "-1";
            this.autoBuong.DisplayType = 1;
            this.autoBuong.Drug_ID = null;
            this.autoBuong.ExtraWidth = 0;
            this.autoBuong.FillValueAfterSelect = false;
            this.autoBuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoBuong.Location = new System.Drawing.Point(115, 35);
            this.autoBuong.MaxHeight = 289;
            this.autoBuong.MinTypedCharacters = 2;
            this.autoBuong.MyCode = "-1";
            this.autoBuong.MyID = "-1";
            this.autoBuong.MyText = "";
            this.autoBuong.MyTextOnly = "";
            this.autoBuong.Name = "autoBuong";
            this.autoBuong.RaiseEvent = true;
            this.autoBuong.RaiseEventEnter = true;
            this.autoBuong.RaiseEventEnterWhenEmpty = true;
            this.autoBuong.SelectedIndex = -1;
            this.autoBuong.Size = new System.Drawing.Size(346, 22);
            this.autoBuong.splitChar = '@';
            this.autoBuong.splitCharIDAndCode = '#';
            this.autoBuong.TabIndex = 1;
            this.autoBuong.TakeCode = false;
            this.autoBuong.txtMyCode = null;
            this.autoBuong.txtMyCode_Edit = null;
            this.autoBuong.txtMyID = null;
            this.autoBuong.txtMyID_Edit = null;
            this.autoBuong.txtMyName = null;
            this.autoBuong.txtMyName_Edit = null;
            this.autoBuong.txtNext = null;
            // 
            // autoGiuong
            // 
            this.autoGiuong._backcolor = System.Drawing.Color.WhiteSmoke;
            this.autoGiuong._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoGiuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autoGiuong.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoGiuong.AutoCompleteList")));
            this.autoGiuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoGiuong.buildShortcut = false;
            this.autoGiuong.CaseSensitive = false;
            this.autoGiuong.CompareNoID = true;
            this.autoGiuong.DefaultCode = "-1";
            this.autoGiuong.DefaultID = "-1";
            this.autoGiuong.DisplayType = 1;
            this.autoGiuong.Drug_ID = null;
            this.autoGiuong.ExtraWidth = 0;
            this.autoGiuong.FillValueAfterSelect = false;
            this.autoGiuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoGiuong.Location = new System.Drawing.Point(530, 35);
            this.autoGiuong.MaxHeight = 289;
            this.autoGiuong.MinTypedCharacters = 2;
            this.autoGiuong.MyCode = "-1";
            this.autoGiuong.MyID = "-1";
            this.autoGiuong.MyText = "";
            this.autoGiuong.MyTextOnly = "";
            this.autoGiuong.Name = "autoGiuong";
            this.autoGiuong.RaiseEvent = true;
            this.autoGiuong.RaiseEventEnter = true;
            this.autoGiuong.RaiseEventEnterWhenEmpty = true;
            this.autoGiuong.SelectedIndex = -1;
            this.autoGiuong.Size = new System.Drawing.Size(466, 22);
            this.autoGiuong.splitChar = '@';
            this.autoGiuong.splitCharIDAndCode = '#';
            this.autoGiuong.TabIndex = 2;
            this.autoGiuong.TakeCode = false;
            this.autoGiuong.txtMyCode = null;
            this.autoGiuong.txtMyCode_Edit = null;
            this.autoGiuong.txtMyID = null;
            this.autoGiuong.txtMyID_Edit = null;
            this.autoGiuong.txtMyName = null;
            this.autoGiuong.txtMyName_Edit = null;
            this.autoGiuong.txtNext = null;
            // 
            // frm_lichsubuonggiuong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.grd_List);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_lichsubuonggiuong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh viện";
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grd_List;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private UCs.AutoCompleteTextbox autoBuong;
        private UCs.AutoCompleteTextbox autoGiuong;
        private UCs.AutoCompleteTextbox autoKhoa;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}