using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;

using VNS.Libs;
using VNS.Core.Classes;
using VNS.Properties;

namespace CIS.CoreApp
{
    public partial class frm_ChangePass : Form
    {
        public frm_ChangePass()
        {
            
            InitializeComponent();
            
            chkCloseAfterChange.Checked = PropertyLib._AppProperties.CloseAfterPWDChange;

        }
        /// <summary>
        /// hàm thực hiện việc đóng forrm hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện nhắc lại mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if(!Invali())return;
            if ( LoginService.UpdatePass(txtNewPass.Text)>0)
            {
                Utility.ShowMsg("Bạn đổi mật khẩu thành công. Nhấn Ok để kết thúc","Thông báo",MessageBoxIcon.Information);
                if (chkCloseAfterChange.Checked)
                {
                    PropertyLib._AppProperties.PWD = txtNewPass.Text;
                    this.Close();
                }
                else
                {
                    PropertyLib._AppProperties.PWD = txtNewPass.Text;
                    txtOldPass.Text = txtNewPass.Text;
                    txtNewPass.Clear();
                    txtReNewPass.Clear();
                    txtNewPass.Focus();
                    return;
                }

            }
            else
            {
                Utility.ShowMsg("Bạn đổi mật khẩu thành công","Thông báo");
            }
        }
        private bool Invali()
        {
           
            if(Utility.DoTrim( PropertyLib._AppProperties.PWD)!=Utility.DoTrim( txtOldPass.Text))
            {
                Utility.ShowMsg("Mật khẩu hiện tại bạn nhập không đúng. Đề nghị kiểm tra lại","Thông báo");
                txtOldPass.Focus();
                return false;
            }
            if(!txtNewPass.Text.Equals(txtReNewPass.Text))
            {
                Utility.ShowMsg("Mật khẩu mới không trùng với mật khẩu nhập lại! ", "Thông báo");
                txtNewPass.Focus();
                return false;
            }
            return true;
        }

        private void frm_ChangePass_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }

       
       
    }
}
