using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SubSonic;

using VNS.Libs;
using VNS.Core.Classes;
using VNS.Libs.RegistryManager;
using VNS.Libs.AppUI;
using VMS.HIS.DAL;
using System.IO;
using VNS.Properties;

namespace CIS.CoreApp
{
    public partial class frm_ChuyenKhoaKCB : Form
    {
        public delegate void OnChangedDepartment();
        public event OnChangedDepartment _OnChangedDepartment;
        private BackgroundWorker bw = new BackgroundWorker();
        public frm_ChuyenKhoaKCB()
        {
            InitializeComponent();
            
        }
       
       
        bool b_HasSuccess = false;
       public bool blnRelogin = false;
       
    
        /// <summary>
        /// hàm thực hiện việc đăng nhập thông tin của khi đăng nhập Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoaKCB_Load(object sender, EventArgs e)
        {
            try
            {
                bool isAdmin = new LoginService().isAdmin(Utility.sDbnull(globalVariables.UserName));
                DataBinding.BindDataCombox(cboKhoaKCB,
                                           THU_VIEN_CHUNG.LaydanhsachKhoaKhidangnhap(globalVariables.UserName, Utility.Bool2byte(isAdmin)),
                                           DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                           "---Chọn khoa làm việc---", false);
                cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
            }
            catch (Exception)
            {
                
                
            }
           finally
            {
            }
        }

       
        /// <summary>
        /// hàm thực hienj việc lưu lại thông itn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void txtUserName_LostFocus(object  sender,EventArgs eventArgs)
        {
           
        }
        /// <summary>
        /// hàm thực hienj viecj đang nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoaKCB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }
        /// <summary>
        /// hàm thực hiện việc đóng for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {
            }
        }
        private bool isProcessRunning1 = false;

        /// <summary>
        /// hàm thực hiện viecj đang nhập thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoaKCB.SelectedValue.ToString() == "-1")
                {
                    UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc", true);
                    cboKhoaKCB.Focus();
                    return;
                }
                if (cboKhoaKCB.SelectedValue.ToString().ToUpper() == globalVariablesPrivate.objKhoaphong.MaKhoaphong.ToUpper())
                {
                    UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc khác khoa làm việc hiện tại (" + globalVariablesPrivate.objKhoaphong.TenKhoaphong + ")", true);
                    cboKhoaKCB.Focus();
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format( "Bạn đã chắc chắn muốn chuyển từ {0} sang {1} để làm việc hay không?\nChú ý: Sau khi chuyển hệ thống sẽ tắt toàn bộ các chức năng bạn đang bật và tự động bật lại ở chế độ mới mở(Do vậy tốt hơn hết bạn cần kết thúc các công việc đang làm dang dở ở khoa {2} trước khi chuyển khoa làm việc mới)",
                    globalVariablesPrivate.objKhoaphong.TenKhoaphong, cboKhoaKCB.Text, globalVariablesPrivate.objKhoaphong.TenKhoaphong), "Xác nhận chuyển khoa", true))
                {
                    return;
                }
                Application.DoEvents();
                Utility.WaitNow(this);
                cmdLogin.Enabled = false;
                if (!IsValid())
                {
                    cmdLogin.Enabled = true;
                    Utility.DefaultNow(this);
                    return;
                }
                PropertyLib._AppProperties.Makhoathien = Utility.sDbnull(cboKhoaKCB.SelectedValue,"KKB");
                globalVariables.MA_KHOA_THIEN = PropertyLib._AppProperties.Makhoathien;
                PropertyLib.SaveProperty(PropertyLib._AppProperties);
                b_Cancel = false;
                this.Close();
                _OnChangedDepartment();
                Utility.DefaultNow(this);
            }
            catch
            { }
            finally
            {
                cmdLogin.Enabled = true;
                Utility.DefaultNow(this);
            }
           
        }
        
        private bool isProcessRunning = false;
       
        public bool b_Cancel = true;
        /// <summary>
        /// hàm thực hiện việc đăng nhập thông tin  kiểm tra
        /// quyền hợp lệ
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            UIAction.SetTextStatus(lblMsg, "", false);
            LoadDataForm();
            return true;
        }
        private void LoadDataForm()
        {

            Application.DoEvents();
            LoadList();
            Application.DoEvents();

        }
        void Try2SaveXML()
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + @"\Config.xml");
                ds.Tables[0].Rows[0]["INTERFACEDISPLAY"] = globalVariables.sMenuStyle;
                ds.WriteXml(Application.StartupPath + @"\Config.xml", XmlWriteMode.IgnoreSchema);
            }
            catch
            {
            }
        }
        public void LoadList()
        {
            try
            {
              
                globalVariables.idKhoatheoMay = (Int16)THU_VIEN_CHUNG.LayIdPhongbanTheoMay(globalVariables.MA_KHOA_THIEN);
                globalVariablesPrivate.objKhoaphong = DmucKhoaphong.FetchByID(globalVariables.idKhoatheoMay);
            }

            catch (Exception ex)
            {
            }
            finally
            {
                
            }


        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdLogin_Click(cmdLogin, new EventArgs());
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string facebook = "http://www.facebook.com/HIS.QLBV";
            Utility.OpenProcess(facebook);
        }
    }
}
