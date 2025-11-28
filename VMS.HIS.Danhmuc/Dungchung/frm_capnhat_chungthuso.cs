using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.EditControls;

namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_capnhat_chungthuso : Form
    {

        DmucNhanvien objNhanvien;
        public bool m_blnCancel = false;
        bool isShowPassword = false;
        public frm_capnhat_chungthuso(DmucNhanvien objNhanvien)
        {
            InitializeComponent();
            this.objNhanvien = objNhanvien;
            InitEvents();
        }
        public frm_capnhat_chungthuso()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_capnhat_chungthuso_FormClosing);
            this.Load += new EventHandler(frm_capnhat_chungthuso_Load);
            this.KeyDown += new KeyEventHandler(frm_capnhat_chungthuso_KeyDown);
          
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            //txtShowHidePwd.MouseUp += txtShowHidePwd_MouseUp;
            //txtShowHidePwd.MouseDown += txtShowHidePwd_MouseDown;
        }

        void txtShowHidePwd_MouseDown(object sender, MouseEventArgs e)
        {
            txt_PassWord.PasswordChar = '\0';
        }

        void txtShowHidePwd_MouseUp(object sender, MouseEventArgs e)
        {
            txt_PassWord.PasswordChar = '*';
        }



        void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void frm_capnhat_chungthuso_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_capnhat_chungthuso_Load(object sender, EventArgs e)
        {
            try
            {
                if (objNhanvien == null)
                    objNhanvien = DmucNhanvien.FetchByID(globalVariables.gv_intIDNhanvien);
                if (objNhanvien != null)
                {
                    txt_UserId.Text = objNhanvien.UserId;
                    txt_PassWord.Text = objNhanvien.UserSecret;
                    txt_TOTP.Text = objNhanvien.UserTotp;

                    txt_ma_bacsi_lien_thong.Text = objNhanvien.MaLienThongBacSi;
                    txt_matkhau_bacsi_lien_thong.Text = objNhanvien.MatkhauLienThongBacSi;
                }    
            }
            catch
            {
            }
            finally
            {
            }
        }
     
     

        void frm_capnhat_chungthuso_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
                if (objNhanvien != null)
                {
                    Utility.ExecuteSql(string.Format("update dmuc_nhanvien set user_id='{0}',user_secret='{1}',user_totp='{2}' where id_nhanvien={3} ", Utility.sDbnull(txt_UserId.Text), Utility.sDbnull(txt_PassWord.Text), Utility.sDbnull(txt_TOTP.Text), objNhanvien.IdNhanvien), CommandType.Text);
                    Utility.ShowMsg("Cập nhật thông tin chứng thư số thành công");
                    if (globalVariablesPrivate.objNhanvien != null)
                    {
                        globalVariablesPrivate.objNhanvien.UserId =Utility.sDbnull( txt_UserId.Text);
                        globalVariablesPrivate.objNhanvien.UserSecret = Utility.sDbnull(txt_PassWord.Text);
                        globalVariablesPrivate.objNhanvien.UserTotp = Utility.sDbnull(txt_TOTP.Text);
                    }
                }
                m_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút chấp nhận:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
           
            if (Utility.sDbnull(txt_UserId.Text, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn cần nhập thông tin user kí số (User ID)"));
                txt_UserId.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_PassWord.Text, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn cần nhập thông tin mật khẩu kí số (User Password)"));
                txt_PassWord.Focus();
                return false;
            }
            return true;
        }

        private void txtShowHidePwd_Click(object sender, EventArgs e)
        {
            isShowPassword = !isShowPassword;
            if (isShowPassword)
                txt_PassWord.PasswordChar = '\0';
            else
                txt_PassWord.PasswordChar = '*';
           
        }

        private void lbl_ShowHide_MatKhau_LienThong_Click(object sender, EventArgs e)
        {
            isShowPassword = !isShowPassword;
            if (isShowPassword)
                txt_matkhau_bacsi_lien_thong.PasswordChar = '\0';
            else
                txt_matkhau_bacsi_lien_thong.PasswordChar = '*';
        }

        private void cmd_luu_thong_tin_lien_thong_Click(object sender, EventArgs e)
        {
            try
            {
                if (objNhanvien != null )
                {
                    Utility.ExecuteSql(string.Format("update dmuc_nhanvien set ma_lien_thong_bac_si='{0}',matkhau_lien_thong_bac_si='{1}' where id_nhanvien={2} ", Utility.sDbnull(txt_ma_bacsi_lien_thong.Text), Utility.sDbnull(txt_matkhau_bacsi_lien_thong.Text), objNhanvien.IdNhanvien), CommandType.Text);
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Liên thông ĐTQG ma_lien_thong_bac_si='{0}',matkhau_lien_thong_bac_si='{1}'cho nhân viên {2} thành công", Utility.sDbnull(txt_ma_bacsi_lien_thong.Text), Utility.sDbnull(txt_matkhau_bacsi_lien_thong.Text), objNhanvien.TenNhanvien), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Cập nhật thông tin liên thông đơn thuốc quốc gia cho Bác sĩ thành công");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
