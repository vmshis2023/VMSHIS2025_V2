using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
namespace VNS.HIS.Danhmuc
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void cmdInhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "";
                if (txtTest.Text.Contains(","))
                {
                    string s1 = txtTest.Text.Split(',')[0];
                    string s2 = txtTest.Text.Split(',')[1];
                    s = String.Format("{0} phẩy {1}", new MoneyByLetter().sMoneyToLetter(s1).Replace("đồng", ""), new MoneyByLetter().sMoneyToLetter(s2).Replace("đồng", ""));
                }
                else if (txtTest.Text.Contains("."))
                {
                    string s1 = txtTest.Text.Split('.')[0];
                    string s2 = txtTest.Text.Split('.')[1];
                    s = String.Format("{0} phẩy {1}", new MoneyByLetter().sMoneyToLetter(s1).Replace("đồng", ""), new MoneyByLetter().sMoneyToLetter(s2).Replace("đồng", ""));
                }
                else
                    s = new MoneyByLetter().sMoneyToLetter(txtTest.Text);
                Utility.ShowMsg(s);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
