using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using VNS.Libs;
using VNS.Libs.AppUI;
namespace VNS.HIS.UI.Cauhinh
{
    public partial class frm_SpecialPass : Form
    {

        string pwd="";
        public string reval = "";
        public frm_SpecialPass(string pwd)
        {
            InitializeComponent();
            this.pwd = pwd;
            this.KeyDown += new KeyEventHandler(frm_SpecialPass_KeyDown);
            txtpwd.KeyDown += new KeyEventHandler(txtpwd_KeyDown);

            txtShowHidePwd.MouseUp += txtShowHidePwd_MouseUp;
            txtShowHidePwd.MouseDown += txtShowHidePwd_MouseDown;
        }

        void txtShowHidePwd_MouseDown(object sender, MouseEventArgs e)
        {
            txtpwd.PasswordChar = '\0';
        }

        void txtShowHidePwd_MouseUp(object sender, MouseEventArgs e)
        {
            txtpwd.PasswordChar = '*';
        }

        void txtpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdAccept_Click(cmdAccept, new EventArgs());
        }

        void frm_SpecialPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void frm_SpecialPass_Load(object sender, EventArgs e)
        {
            txtpwd.Focus();
            
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "",true);
           
                if (txtpwd.Text.Trim() == pwd)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    UIAction.SetTextStatus(lblMsg, "Wrong pass word!", Color.Red);
                    txtpwd.Focus();
                }
            //}
        }
    }
}
