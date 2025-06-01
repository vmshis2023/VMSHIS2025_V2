using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace UpdateVersions
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	partial class Form1 : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.pgr = new System.Windows.Forms.ProgressBar();
			this.lblStatus = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.lblProgress = new System.Windows.Forms.Label();
			this.cmdClose = new System.Windows.Forms.Button();
			this.cmdUpdate = new System.Windows.Forms.Button();
		
			this.SuspendLayout();
			//
			//pgr
			//
			this.pgr.Location = new System.Drawing.Point(37, 84);
			this.pgr.MarqueeAnimationSpeed = 2000;
			this.pgr.Maximum = 100000000;
			this.pgr.Name = "pgr";
			this.pgr.Size = new System.Drawing.Size(410, 16);
			this.pgr.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pgr.TabIndex = 0;
			this.pgr.Visible = false;
			//
			//lblStatus
			//
			this.lblStatus.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblStatus.ForeColor = System.Drawing.Color.Blue;
			this.lblStatus.Location = new System.Drawing.Point(34, 63);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(60, 15);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "Updating:";
			this.lblStatus.Visible = false;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.Label1.ForeColor = System.Drawing.Color.Black;
			this.Label1.Location = new System.Drawing.Point(34, 22);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(116, 15);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Thư mục chứa files:";
			//
			//txtFolder
			//
			this.txtFolder.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtFolder.Enabled = false;
			this.txtFolder.Location = new System.Drawing.Point(37, 40);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(410, 20);
			this.txtFolder.TabIndex = 7;
			//
			//lblProgress
			//
			this.lblProgress.AutoSize = true;
			this.lblProgress.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
			this.lblProgress.ForeColor = System.Drawing.Color.Blue;
			this.lblProgress.Location = new System.Drawing.Point(34, 65);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(111, 15);
			this.lblProgress.TabIndex = 9;
			this.lblProgress.Text = "Tiến trình cập nhật:";
			this.lblProgress.Visible = false;
			//
			//cmdClose
			//
			this.cmdClose.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.Image = (System.Drawing.Image)resources.GetObject("cmdClose.Image");
			this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdClose.Location = new System.Drawing.Point(250, 116);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(102, 29);
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "Thoát";
			this.cmdClose.UseVisualStyleBackColor = true;
			//
			//cmdUpdate
			//
			this.cmdUpdate.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.cmdUpdate.Image = (System.Drawing.Image)resources.GetObject("cmdUpdate.Image");
			this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdUpdate.Location = new System.Drawing.Point(123, 116);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new System.Drawing.Size(104, 29);
			this.cmdUpdate.TabIndex = 0;
			this.cmdUpdate.Text = "Cập nhật";
			this.cmdUpdate.UseVisualStyleBackColor = true;
			//
		
			//
			//Form1
			//
			this.AcceptButton = this.cmdUpdate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(482, 157);
			this.Controls.Add(this.cmdUpdate);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.pgr);
			
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update new Version of your APPs";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.ProgressBar pgr;
		internal System.Windows.Forms.Label lblStatus;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox txtFolder;
		internal System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Button withEventsField_cmdClose;
		internal System.Windows.Forms.Button cmdClose {
			get { return withEventsField_cmdClose; }
			set {
				if (withEventsField_cmdClose != null) {
					withEventsField_cmdClose.Click -= cmdClose_Click;
				}
				withEventsField_cmdClose = value;
				if (withEventsField_cmdClose != null) {
					withEventsField_cmdClose.Click += cmdClose_Click;
				}
			}
		}
		private System.Windows.Forms.Button withEventsField_cmdUpdate;
		internal System.Windows.Forms.Button cmdUpdate {
			get { return withEventsField_cmdUpdate; }
			set {
				if (withEventsField_cmdUpdate != null) {
					withEventsField_cmdUpdate.Click -= cmdUpdate_Click;
				}
				withEventsField_cmdUpdate = value;
				if (withEventsField_cmdUpdate != null) {
					withEventsField_cmdUpdate.Click += cmdUpdate_Click;
				}
			}
		}

		
		public Form1()
		{
			Load += Form1_Load;
			KeyDown += Form1_KeyDown;
			FormClosing += Form1_FormClosing;
			InitializeComponent();
		}
	}
}
