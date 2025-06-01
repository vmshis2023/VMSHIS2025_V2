using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace SView.UCs
{
    public partial class medCheckBox : UserControl
    {
        public delegate void Oncheck();
        public event Oncheck _Oncheck;
        public bool _IsChecked=false;

        public bool _AllowOnCheck = false;
        public medCheckBox()
        {
            InitializeComponent();
            this.Size = new Size(222, 28);
        }
        public void Check()
        {
            lblcheck.ImageIndex=1;
            IsChecked = true;
            if (AllowOnCheck) _Oncheck();
        }
        public void UnCheck()
        {
            lblcheck.ImageIndex = 0;
            IsChecked = false;
            if (AllowOnCheck) _Oncheck();
        }
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value;
            lblcheck.ImageIndex = _IsChecked ? 1 : 0;
            }
        }
        public bool AllowOnCheck
        {
            get { return _AllowOnCheck; }
            set { _AllowOnCheck = value; }
        }
        public string YourText
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }
        public Font FontText
        {
            get { return lblText.Font; }
            set { lblText.Font = value; }
        }
        public Color _FontColor
        {
            get { return lblText.ForeColor; }
            set { lblText.ForeColor = value; }
        }
        void SetStatus()
        {
            try
            {
                if (lblcheck.ImageIndex == 1)
                {

                    lblcheck.ImageIndex = 0;
                }
                else
                {

                    lblcheck.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                IsChecked = lblcheck.ImageIndex == 1;
                if (AllowOnCheck) _Oncheck();
            }
        }
        private void lblcheck_Click(object sender, EventArgs e)
        {
            SetStatus();   
        }

        private void lblText_Click(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void medCheckBox_Load(object sender, EventArgs e)
        {
            lblcheck.Size = new Size(35, 35);
        }
    }
}
