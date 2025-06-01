namespace VNS.UCs
{
    partial class ucPhieuxuatthuocBN
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
            this.ScheduleObj = new System.Windows.Forms.Button();
            this.ctxQuickAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToExamListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRefreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxQuickAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScheduleObj
            // 
            this.ScheduleObj.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ScheduleObj.ContextMenuStrip = this.ctxQuickAdd;
            this.ScheduleObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScheduleObj.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.ScheduleObj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ScheduleObj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScheduleObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleObj.ForeColor = System.Drawing.Color.Black;
            this.ScheduleObj.Location = new System.Drawing.Point(0, 0);
            this.ScheduleObj.Name = "ScheduleObj";
            this.ScheduleObj.Size = new System.Drawing.Size(280, 32);
            this.ScheduleObj.TabIndex = 0;
            this.ScheduleObj.Text = "CHEST-AP";
            this.ScheduleObj.UseVisualStyleBackColor = false;
            // 
            // ctxQuickAdd
            // 
            this.ctxQuickAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToExamListToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuUpdate,
            this.toolStripMenuItem2,
            this.mnuRefreshList});
            this.ctxQuickAdd.Name = "ctxQuickAdd";
            this.ctxQuickAdd.Size = new System.Drawing.Size(161, 82);
            // 
            // addToExamListToolStripMenuItem
            // 
            this.addToExamListToolStripMenuItem.Name = "addToExamListToolStripMenuItem";
            this.addToExamListToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addToExamListToolStripMenuItem.Text = "Add to exam list";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(160, 22);
            this.mnuUpdate.Text = "Update";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuRefreshList
            // 
            this.mnuRefreshList.Name = "mnuRefreshList";
            this.mnuRefreshList.Size = new System.Drawing.Size(160, 22);
            this.mnuRefreshList.Text = "Refresh list";
            this.mnuRefreshList.Click += new System.EventHandler(this.mnuRefreshList_Click);
            // 
            // ucPhieuxuatthuocBN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.ScheduleObj);
            this.Name = "ucPhieuxuatthuocBN";
            this.Size = new System.Drawing.Size(280, 32);
            this.Load += new System.EventHandler(this.ucPhieuxuatthuocBN_Load);
            this.ctxQuickAdd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScheduleObj;
        private System.Windows.Forms.ContextMenuStrip ctxQuickAdd;
        private System.Windows.Forms.ToolStripMenuItem addToExamListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshList;
    }
}
