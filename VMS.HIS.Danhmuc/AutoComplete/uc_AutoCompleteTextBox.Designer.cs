namespace VMS.HIS.Danhmuc.AutoComplete
{
    partial class uc_AutoCompleteTextBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_AutoCompleteTextBox));
            this.cmdShow = new Janus.Windows.EditControls.UIButton();
            this.autoCompleteTextbox1 = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.SuspendLayout();
            // 
            // cmdShow
            // 
            this.cmdShow.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdShow.Image = ((System.Drawing.Image)(resources.GetObject("cmdShow.Image")));
            this.cmdShow.ImageSize = new System.Drawing.Size(12, 12);
            this.cmdShow.Location = new System.Drawing.Point(531, 0);
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.Size = new System.Drawing.Size(22, 22);
            this.cmdShow.TabIndex = 635;
            this.cmdShow.TabStop = false;
            // 
            // autoCompleteTextbox1
            // 
            this.autoCompleteTextbox1._backcolor = System.Drawing.SystemColors.Control;
            this.autoCompleteTextbox1._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextbox1._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextbox1.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("autoCompleteTextbox1.AutoCompleteList")));
            this.autoCompleteTextbox1.buildShortcut = false;
            this.autoCompleteTextbox1.CaseSensitive = false;
            this.autoCompleteTextbox1.CompareNoID = true;
            this.autoCompleteTextbox1.DefaultCode = "-1";
            this.autoCompleteTextbox1.DefaultID = "-1";
            this.autoCompleteTextbox1.DisplayType = 0;
            this.autoCompleteTextbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoCompleteTextbox1.Drug_ID = null;
            this.autoCompleteTextbox1.ExtraWidth = 0;
            this.autoCompleteTextbox1.FillValueAfterSelect = false;
            this.autoCompleteTextbox1.Location = new System.Drawing.Point(0, 0);
            this.autoCompleteTextbox1.MaxHeight = -1;
            this.autoCompleteTextbox1.MinTypedCharacters = 2;
            this.autoCompleteTextbox1.MyCode = "-1";
            this.autoCompleteTextbox1.MyID = "-1";
            this.autoCompleteTextbox1.MyText = "";
            this.autoCompleteTextbox1.MyTextOnly = "";
            this.autoCompleteTextbox1.Name = "autoCompleteTextbox1";
            this.autoCompleteTextbox1.RaiseEvent = false;
            this.autoCompleteTextbox1.RaiseEventEnter = false;
            this.autoCompleteTextbox1.RaiseEventEnterWhenEmpty = false;
            this.autoCompleteTextbox1.SelectedIndex = -1;
            this.autoCompleteTextbox1.Size = new System.Drawing.Size(531, 20);
            this.autoCompleteTextbox1.splitChar = '@';
            this.autoCompleteTextbox1.splitCharIDAndCode = '#';
            this.autoCompleteTextbox1.TabIndex = 636;
            this.autoCompleteTextbox1.TakeCode = false;
            this.autoCompleteTextbox1.txtMyCode = null;
            this.autoCompleteTextbox1.txtMyCode_Edit = null;
            this.autoCompleteTextbox1.txtMyID = null;
            this.autoCompleteTextbox1.txtMyID_Edit = null;
            this.autoCompleteTextbox1.txtMyName = null;
            this.autoCompleteTextbox1.txtMyName_Edit = null;
            this.autoCompleteTextbox1.txtNext = null;
            // 
            // uc_AutoCompleteTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.autoCompleteTextbox1);
            this.Controls.Add(this.cmdShow);
            this.Name = "uc_AutoCompleteTextBox";
            this.Size = new System.Drawing.Size(553, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Janus.Windows.EditControls.UIButton cmdShow;
        public VNS.HIS.UCs.AutoCompleteTextbox autoCompleteTextbox1;

    }
}
