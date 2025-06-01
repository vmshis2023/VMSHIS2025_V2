using VNS.HIS.UCs;

namespace VMS.HIS.Danhmuc.AutoComplete.DynamicReport_Ucs
{
    partial class ucAutoComplete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAutoComplete));
            this.lblName = new System.Windows.Forms.Label();
            this.txtValue = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(159, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Đối tượng:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtValue
            // 
            this.txtValue._backcolor = System.Drawing.SystemColors.Control;
            this.txtValue._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValue.AddValues = true;
            this.txtValue.AllowMultiline = false;
            this.txtValue.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtValue.AutoCompleteList")));
            this.txtValue.buildShortcut = false;
            this.txtValue.CaseSensitive = false;
            this.txtValue.CompareNoID = true;
            this.txtValue.DefaultCode = "-1";
            this.txtValue.DefaultID = "-1";
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Drug_ID = null;
            this.txtValue.ExtraWidth = 0;
            this.txtValue.FillValueAfterSelect = false;
            this.txtValue.LOAI_DANHMUC = null;
            this.txtValue.Location = new System.Drawing.Point(159, 0);
            this.txtValue.MaxHeight = -1;
            this.txtValue.MinTypedCharacters = 2;
            this.txtValue.MyCode = "-1";
            this.txtValue.MyID = "-1";
            this.txtValue.Name = "txtValue";
            this.txtValue.RaiseEvent = true;
            this.txtValue.RaiseEventEnter = true;
            this.txtValue.RaiseEventEnterWhenEmpty = false;
            this.txtValue.SelectedIndex = -1;
            this.txtValue.ShowCodeWithValue = false;
            this.txtValue.Size = new System.Drawing.Size(624, 22);
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
            this.txtValue.txtNext1 = null;
            // 
            // ucAutoComplete
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucAutoComplete";
            this.Size = new System.Drawing.Size(783, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblName;
        private AutoCompleteTextbox_Danhmucchung txtValue;

    }
}
