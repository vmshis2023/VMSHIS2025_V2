using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
namespace VMS.HIS.Danhmuc.UserControls
{
     partial class medRadioButton : UserControl
    {
        public delegate void Oncheck();
        public delegate void OnSelectedChange(medRadioButton obj);
        public event Oncheck _Oncheck;
        public event OnSelectedChange _OnSelectedChange;
        public bool _IsChecked=false;
        public int id_goi;
        public int id_dangky;
        public bool _AllowOnCheck = false;
        public medRadioButton()
        {
            InitializeComponent();
        }
        public void Check()
        {
            this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64;
            IsChecked = true;
            if (_AllowOnCheck) _OnSelectedChange(this);
        }
        public void UnCheck()
        {
            this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_uncheck_64;
            IsChecked = false;
            if (_AllowOnCheck) _OnSelectedChange(this);
            
        }
        public void CheckNoEvent()
        {
            this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64;
            IsChecked = true;
           
        }
        public void UnCheckNoEvent()
        {
            this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_uncheck_64;
            IsChecked = false;
           

        }
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value;
                this.pnlImage.BackgroundImage = _IsChecked ? global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64 : global::VMS.HIS.Danhmuc.Properties.Resources.option_uncheck_64;
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
                this.pnlImage.BackgroundImage = global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64;
            }
            catch
            {
            }
            finally
            {
                IsChecked = this.pnlImage.BackgroundImage == global::VMS.HIS.Danhmuc.Properties.Resources.option_check_64;
                if (AllowOnCheck) _OnSelectedChange(this);
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
    }
}
