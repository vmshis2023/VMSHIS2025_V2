namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_danhsachbenhvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_danhsachbenhvien));
            this.grd_List = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_List
            // 
            this.grd_List.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin mã bệnh ICD</FilterRowInfoText></LocalizableData>";
            this.grd_List.DefaultAlphaMode = Janus.Windows.GridEX.AlphaMode.UseAlpha;
            this.grd_List.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grd_List_DesignTimeLayout.LayoutString = resources.GetString("grd_List_DesignTimeLayout.LayoutString");
            this.grd_List.DesignTimeLayout = grd_List_DesignTimeLayout;
            this.grd_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_List.DynamicFiltering = true;
            this.grd_List.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grd_List.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grd_List.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grd_List.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grd_List.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grd_List.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grd_List.FrozenColumns = 1;
            this.grd_List.GroupByBoxVisible = false;
            this.grd_List.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grd_List.Location = new System.Drawing.Point(0, 0);
            this.grd_List.Name = "grd_List";
            this.grd_List.RecordNavigator = true;
            this.grd_List.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grd_List.Size = new System.Drawing.Size(915, 648);
            this.grd_List.TabIndex = 1;
            this.toolTip1.SetToolTip(this.grd_List, "Click đúp chuột hoặc nhấn phím Enter để chọn Bệnh viện");
            this.grd_List.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_danhsachbenhvien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(915, 648);
            this.Controls.Add(this.grd_List);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_danhsachbenhvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh viện";
            ((System.ComponentModel.ISupportInitialize)(this.grd_List)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.GridEX.GridEX grd_List;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}