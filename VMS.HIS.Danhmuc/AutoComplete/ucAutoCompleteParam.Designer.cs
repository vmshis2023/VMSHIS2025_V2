namespace VNS.UCs
{
    partial class ucAutoCompleteParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAutoCompleteParam));
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtValue = new VNS.HIS.UCs.AutoCompleteTextbox_DynamicField();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 35);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Đối tượng:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtValue);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 35);
            this.panel1.TabIndex = 4;
            // 
            // txtValue
            // 
            this.txtValue._backcolor = System.Drawing.SystemColors.Control;
            this.txtValue._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValue.AddValues = true;
            this.txtValue.AllowEmpty = false;
            this.txtValue.AllowMultiline = false;
            this.txtValue.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtValue.AutoCompleteList")));
            this.txtValue.CaseSensitive = false;
            this.txtValue.CompareNoID = true;
            this.txtValue.DefaultCode = "-1";
            this.txtValue.DefaultID = "-1";
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Drug_ID = null;
            this.txtValue.ExtraWidth = 0;
            this.txtValue.FillValueAfterSelect = false;
            this.txtValue.LOAI_DANHMUC = null;
            this.txtValue.Location = new System.Drawing.Point(60, 0);
            this.txtValue.MaxHeight = -1;
            this.txtValue.MinTypedCharacters = 2;
            this.txtValue.MyCode = "-1";
            this.txtValue.MyID = "-1";
            this.txtValue.Name = "txtValue";
            this.txtValue.RaiseEvent = false;
            this.txtValue.RaiseEventEnter = false;
            this.txtValue.RaiseEventEnterWhenEmpty = false;
            this.txtValue.SelectedIndex = -1;
            this.txtValue.ShowCodeWithValue = false;
            this.txtValue.Size = new System.Drawing.Size(723, 22);
            this.txtValue.splitChar = '@';
            this.txtValue.splitCharIDAndCode = '#';
            this.txtValue.TabIndex = 2;
            this.txtValue.TakeCode = false;
            this.txtValue.txtMyCode = null;
            this.txtValue.txtMyCode_Edit = null;
            this.txtValue.txtMyID = null;
            this.txtValue.txtMyID_Edit = null;
            this.txtValue.txtMyName = null;
            this.txtValue.txtMyName_Edit = null;
            this.txtValue.txtNext = null;
            this.txtValue.VisibleDefaultItem = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // ucAutoCompleteParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucAutoCompleteParam";
            this.Size = new System.Drawing.Size(783, 35);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label lblName;
        public HIS.UCs.AutoCompleteTextbox_DynamicField txtValue;

    }
}
