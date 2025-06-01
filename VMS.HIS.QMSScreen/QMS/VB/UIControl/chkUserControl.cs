using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VMS.QMS.UIControl
{
    public partial class chkUserControl : UserControl
    {  
        public delegate  void OnChange(int uutien);

        public event OnChange _myChange;
        public chkUserControl()
        {
            InitializeComponent();
            txtcheck.BackColor = _TxtColor;
        }

        public bool bValueChange = false;
        public Color _TxtColor;
        private void txtcheck_MouseClick(object sender, MouseEventArgs e)
        {
            //if (txtcheck.Text == "")
            //{
            //    txtcheck.Text = "V";
            //}
            //else
            //{
            //    txtcheck.Text = "";
            //}
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (txtcheck.Text == "")
            //{
            //    txtcheck.Text = "V";
            //}
            //else
            //{
            //    txtcheck.Text = "";
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (txtcheck.Text == "")
            {
                txtcheck.Text = "V";
                if (_myChange != null) _myChange(1);
            }
            else
            {
                txtcheck.Text = "";
                if (_myChange != null) _myChange(0);
            }
           
        }

        private void txtcheck_Click(object sender, EventArgs e)
        {
            if (txtcheck.Text == "")
            {
                txtcheck.Text = "V";
                if (_myChange != null) _myChange(1);
            }
            else
            {
                txtcheck.Text = "";
                if (_myChange != null) _myChange(0);
            }
        }
    }
}
