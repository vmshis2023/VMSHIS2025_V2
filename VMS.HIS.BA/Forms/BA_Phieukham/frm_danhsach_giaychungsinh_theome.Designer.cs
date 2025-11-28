
namespace VMS.HIS.EMR.Forms.BA_Phieukham
{
    partial class frm_danhsach_giaychungsinh_theome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_danhsach_giaychungsinh_theome));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdAccept);
            this.groupBox1.Controls.Add(this.cmdExit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 515);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(535, 9);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(125, 33);
            this.cmdAccept.TabIndex = 6;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(675, 9);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(119, 33);
            this.cmdExit.TabIndex = 7;
            this.cmdExit.Text = "Thoát(Esc)";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.AlternatingColors = true;
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.Cornsilk;
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdList.Size = new System.Drawing.Size(784, 515);
            this.grdList.TabIndex = 264444;
            // 
            // frm_danhsach_giaychungsinh_theome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_danhsach_giaychungsinh_theome";
            this.ShowIcon = false;
            this.Text = "Danh sách giấy chứng sinh các con theo mẹ";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdList;
    }
}