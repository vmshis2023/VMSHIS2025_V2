namespace VMS.QMS
{
    partial class FrmShowListData
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
            Janus.Windows.Common.Layouts.JanusLayoutReference grdList_DesignTimeLayout_Reference_0 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowListData));
            Janus.Windows.Common.Layouts.JanusLayoutReference grdList_DesignTimeLayout_Reference_1 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column1.ButtonImage");
            Janus.Windows.Common.Layouts.JanusLayoutReference grdList_DesignTimeLayout_Reference_2 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column7.ButtonImage");
            Janus.Windows.GridEX.GridEXLayout grdListNho_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.Common.Layouts.JanusLayoutReference grdListNho_DesignTimeLayout_Reference_0 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage");
            Janus.Windows.GridEX.GridEXLayout grdDathuchien_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.Common.Layouts.JanusLayoutReference grdDathuchien_DesignTimeLayout_Reference_0 = new Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage");
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiTabPage2 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdListNho = new Janus.Windows.GridEX.GridEX();
            this.uiTabPage3 = new Janus.Windows.UI.Tab.UITabPage();
            this.grdDathuchien = new Janus.Windows.GridEX.GridEX();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.uiTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListNho)).BeginInit();
            this.uiTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDathuchien)).BeginInit();
            this.SuspendLayout();
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 30F);
            this.uiTab1.Location = new System.Drawing.Point(0, 0);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(1264, 985);
            this.uiTab1.TabIndex = 5;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1,
            this.uiTabPage2,
            this.uiTabPage3});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Office2007;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.grdList);
            this.uiTabPage1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 53);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(1262, 931);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Danh sách chờ thực hiện";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("grdList_DesignTimeLayout_Reference_0.Instance")));
            grdList_DesignTimeLayout_Reference_1.Instance = ((object)(resources.GetObject("grdList_DesignTimeLayout_Reference_1.Instance")));
            grdList_DesignTimeLayout_Reference_2.Instance = ((object)(resources.GetObject("grdList_DesignTimeLayout_Reference_2.Instance")));
            grdList_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            grdList_DesignTimeLayout_Reference_0,
            grdList_DesignTimeLayout_Reference_1,
            grdList_DesignTimeLayout_Reference_2});
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdList.Font = new System.Drawing.Font("Arial", 10F);
            this.grdList.GridLineColor = System.Drawing.Color.Navy;
            this.grdList.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1262, 931);
            this.grdList.TabIndex = 1;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdList.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdList_ColumnButtonClick);
            // 
            // uiTabPage2
            // 
            this.uiTabPage2.Controls.Add(this.grdListNho);
            this.uiTabPage2.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.uiTabPage2.Location = new System.Drawing.Point(1, 53);
            this.uiTabPage2.Name = "uiTabPage2";
            this.uiTabPage2.Size = new System.Drawing.Size(1262, 931);
            this.uiTabPage2.TabStop = true;
            this.uiTabPage2.Text = "Danh sách nhỡ thực hiện";
            // 
            // grdListNho
            // 
            this.grdListNho.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdListNho.ColumnAutoResize = true;
            grdListNho_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("grdListNho_DesignTimeLayout_Reference_0.Instance")));
            grdListNho_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            grdListNho_DesignTimeLayout_Reference_0});
            grdListNho_DesignTimeLayout.LayoutString = resources.GetString("grdListNho_DesignTimeLayout.LayoutString");
            this.grdListNho.DesignTimeLayout = grdListNho_DesignTimeLayout;
            this.grdListNho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListNho.DynamicFiltering = true;
            this.grdListNho.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdListNho.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdListNho.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdListNho.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdListNho.Font = new System.Drawing.Font("Arial", 10F);
            this.grdListNho.GridLineColor = System.Drawing.Color.Navy;
            this.grdListNho.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdListNho.GroupByBoxVisible = false;
            this.grdListNho.Location = new System.Drawing.Point(0, 0);
            this.grdListNho.Name = "grdListNho";
            this.grdListNho.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListNho.Size = new System.Drawing.Size(1262, 931);
            this.grdListNho.TabIndex = 2;
            this.grdListNho.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdListNho.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdListNho_ColumnButtonClick);
            // 
            // uiTabPage3
            // 
            this.uiTabPage3.Controls.Add(this.grdDathuchien);
            this.uiTabPage3.Location = new System.Drawing.Point(1, 53);
            this.uiTabPage3.Name = "uiTabPage3";
            this.uiTabPage3.Size = new System.Drawing.Size(1262, 931);
            this.uiTabPage3.TabStop = true;
            this.uiTabPage3.Text = "Đã thực hiện";
            // 
            // grdDathuchien
            // 
            this.grdDathuchien.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdDathuchien.ColumnAutoResize = true;
            grdDathuchien_DesignTimeLayout_Reference_0.Instance = ((object)(resources.GetObject("grdDathuchien_DesignTimeLayout_Reference_0.Instance")));
            grdDathuchien_DesignTimeLayout.LayoutReferences.AddRange(new Janus.Windows.Common.Layouts.JanusLayoutReference[] {
            grdDathuchien_DesignTimeLayout_Reference_0});
            grdDathuchien_DesignTimeLayout.LayoutString = resources.GetString("grdDathuchien_DesignTimeLayout.LayoutString");
            this.grdDathuchien.DesignTimeLayout = grdDathuchien_DesignTimeLayout;
            this.grdDathuchien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDathuchien.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdDathuchien.Font = new System.Drawing.Font("Arial", 10F);
            this.grdDathuchien.GridLineColor = System.Drawing.Color.Navy;
            this.grdDathuchien.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdDathuchien.GroupByBoxVisible = false;
            this.grdDathuchien.Location = new System.Drawing.Point(0, 0);
            this.grdDathuchien.Name = "grdDathuchien";
            this.grdDathuchien.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDathuchien.Size = new System.Drawing.Size(1262, 931);
            this.grdDathuchien.TabIndex = 3;
            this.grdDathuchien.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmShowListData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Controls.Add(this.uiTab1);
            this.Name = "FrmShowListData";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmShowListData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.uiTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListNho)).EndInit();
            this.uiTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDathuchien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage2;
        private Janus.Windows.GridEX.GridEX grdListNho;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage3;
        private Janus.Windows.GridEX.GridEX grdDathuchien;
        private System.Windows.Forms.Timer timer1;
    }
}