namespace VNS.HIS.UI.Forms.CanLamSang
{
    partial class Frm_ASTM_CreateResultFromOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ASTM_CreateResultFromOrder));
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.txtFileName = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 8F);
            this.grdList.FrozenColumns = 3;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 47);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(993, 447);
            this.grdList.TabIndex = 27;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // cmdCreate
            // 
            this.cmdCreate.Location = new System.Drawing.Point(882, 500);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(114, 23);
            this.cmdCreate.TabIndex = 28;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(832, 4);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(114, 26);
            this.cmdRefresh.TabIndex = 30;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // txtFileName
            // 
            this.txtFileName._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtFileName._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFileName.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtFileName.AutoCompleteList")));
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.CaseSensitive = false;
            this.txtFileName.CompareNoID = true;
            this.txtFileName.DefaultCode = "-1";
            this.txtFileName.DefaultID = "-1";
            this.txtFileName.Drug_ID = null;
            this.txtFileName.ExtraWidth = 0;
            this.txtFileName.FillValueAfterSelect = false;
            this.txtFileName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtFileName.LOAI_DANHMUC = "PHUONGTHUCTHANHTOAN";
            this.txtFileName.Location = new System.Drawing.Point(3, 4);
            this.txtFileName.MaxHeight = -1;
            this.txtFileName.MinTypedCharacters = 2;
            this.txtFileName.MyCode = "-1";
            this.txtFileName.MyID = "-1";
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.RaiseEvent = false;
            this.txtFileName.RaiseEventEnter = true;
            this.txtFileName.RaiseEventEnterWhenEmpty = true;
            this.txtFileName.SelectedIndex = -1;
            this.txtFileName.Size = new System.Drawing.Size(823, 26);
            this.txtFileName.splitChar = '@';
            this.txtFileName.splitCharIDAndCode = '#';
            this.txtFileName.TabIndex = 29;
            this.txtFileName.TakeCode = false;
            this.txtFileName.txtMyCode = null;
            this.txtFileName.txtMyCode_Edit = null;
            this.txtFileName.txtMyID = null;
            this.txtFileName.txtMyID_Edit = null;
            this.txtFileName.txtMyName = null;
            this.txtFileName.txtMyName_Edit = null;
            this.txtFileName.txtNext = null;
            // 
            // Frm_ASTM_CreateResultFromOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 530);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdCreate);
            this.Controls.Add(this.grdList);
            this.Name = "Frm_ASTM_CreateResultFromOrder";
            this.Text = "Frm_ASTM_CreateResultFromOrder";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Button cmdCreate;
        private UCs.AutoCompleteTextbox_Danhmucchung txtFileName;
        private System.Windows.Forms.Button cmdRefresh;
    }
}