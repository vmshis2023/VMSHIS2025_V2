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
    public partial class frm_ConfirmPass : Form
    {
        int _count = 0;
        int _total = 5;
        bool AllowClosing = false;
        int _r =30000;
        int _r2 =30000; 
        int _remain = 30000;
        int _remain2 = 30000;
        string _timeout = "300";
        public frm_ConfirmPass()
        {
            
            InitializeComponent();
            this.FormClosing += frm_ConfirmPass_FormClosing;
            txtOldPass.KeyDown += txtOldPass_KeyDown;
            _remain =_r = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_SLEEP", "30", true), 30)*1000;
            _remain2 = _r2 = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_AUTOSLEEP", "30", true), 30) * 1000;
            _total = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_SOLANDANGNHAP", "3", true), 3);
            lblMsg.Text = string.Format("Bạn còn {0}/{1} lần đăng nhập", (_total - _count).ToString(), _total.ToString());
            _timeout = globalVariables.UCL.Substring(globalVariables.UCL.IndexOf(globalVariables.UserName)).Split(',')[0].Replace("#", "").Replace(globalVariables.UserName, "");
            lblMsg0.Text = string.Format("Tài khoản: {0} đang được hệ thống cấu hình yêu cầu xác thực tài khoản {1} giây/ lần để đảm bảo tính bảo mật. Vui lòng nhập lại mật khẩu để tiếp tục làm việc", globalVariables.UserName, _timeout);
            timer1.Enabled = false;
            timer1.Stop();
        }

        void txtOldPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter  && !timer1.Enabled)
            {
                if (Utility.DoTrim(PropertyLib._AppProperties.PWD) != Utility.DoTrim(txtOldPass.Text))
                {
                    _remain2 += 5000;
                    _count += 1;
                    if (_count < _total)
                    {
                        Utility.ShowMsg("Mật khẩu hiện tại bạn nhập không đúng. Đề nghị kiểm tra lại", "Thông báo");

                        lblMsg.Text = string.Format("Bạn còn {0}/{1} lần đăng nhập", (_total - _count).ToString(), _total.ToString());
                        txtOldPass.Focus();
                        return;
                    }
                    else
                    {
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_TYPE", "1", true) == "0")//0=Hidden;1=Exit APP
                        {
                            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                            this.WindowState = FormWindowState.Maximized;
                            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            VNS.Libs.AppUI.UIAction.SetTextStatus(lblMsg1, string.Format(""), true);
                            _remain = _r;
                            timer1.Enabled = true; 
                            timer1.Start();
                            
                        }
                        else
                        {
                            Utility.ShowMsg(string.Format("Bạn đã thử đăng nhập {0} lần thất bại liên tiếp. Bạn cần kiểm tra xem có đúng tài khoản của mình hay không?\nNếu đúng có thể bạn đang quên mật khẩu và cần liên hệ quản trị hệ thống để được hỗ trợ về việc lấy lại mật khẩu này\nHệ thống sẽ tạm thời tắt chương trình để đảm bảo tính bảo mật. Regards", _count.ToString()), "Thông báo");
                            globalVariables._OK = false;
                            AllowClosing = true;
                            this.Close();
                        }
                    }
                }
                else
                {
                    globalVariables._OK = true;
                    AllowClosing = true;
                    this.Close();
                }
            }

        }

        void frm_ConfirmPass_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClosing)
                e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _remain2 = _r2;
            if (_remain > 0)
            {
                _remain -= 1000;
                txtOldPass.Enabled = false;
                VNS.Libs.AppUI.UIAction.SetTextStatus(lblMsg, string.Format("Vui lòng chờ {0} giây để thử đăng nhập lại...", (_remain / 1000).ToString()), true);
            }
            else
            {
                _count = 0;
                timer1.Enabled = false;
                txtOldPass.Enabled = true;
                VNS.Libs.AppUI.UIAction.SetTextStatus(lblMsg, string.Format("Bạn còn {0}/{1} lần đăng nhập", (_total - _count).ToString(), _total.ToString()), false);
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
               
                if (_remain2 > 0)
                {
                    _remain2 -= 1000;
                    VNS.Libs.AppUI.UIAction.SetTextStatus(lblMsg1, string.Format("Màn hình chuẩn bị chuyển về chế độ bảo mật trong vòng {0} giây nếu bạn không đăng nhập lại", (_remain2 / 1000).ToString()), true);
                }
                else
                {
                    _r = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_SLEEP", "30", true), 30) * 1000;
                    _r2 = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("UCL_AUTOSLEEP", "30", true), 30) * 1000;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    VNS.Libs.AppUI.UIAction.SetText(lblMsg1, "");
                    _remain = _r;
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Utility.ShowMsg("Bạn có thể đăng nhập lại bằng tài khoản cá nhân sau khi thoát khỏi hệ thống. Rất xin lỗi vì đã làm bạn phiền lòng với màn hình bảo mật này");
            if (Utility.AcceptQuestion("Bạn đã chắc chắn muốn thoát khỏi hệ thống?","Xác nhận",true))
            {
                globalVariables._OK = false;
                AllowClosing = true;
                this.Close();
            }
        }
       
       
       
       
    }
}
