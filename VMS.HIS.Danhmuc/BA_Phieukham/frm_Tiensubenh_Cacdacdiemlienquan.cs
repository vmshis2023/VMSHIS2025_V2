using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_Tiensubenh_Cacdacdiemlienquan : Form
    {
        KcbLuotkham objLuotkham;
        public frm_Tiensubenh_Cacdacdiemlienquan(KcbLuotkham objLuotkham)
        {
            InitializeComponent();
           
            this.objLuotkham = objLuotkham;
          
            this.Shown += Frm_TiensuSanphukhoa_Shown;
            this.KeyDown += Frm_Tiensubenh_Cacdacdiemlienquan_KeyDown;
        }

        private void Frm_Tiensubenh_Cacdacdiemlienquan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uc_tiensubenh_cacdacdiemlienquan1.HandleKeyEnter();
        }

        private void Frm_TiensuSanphukhoa_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
            uc_tiensubenh_cacdacdiemlienquan1.InitData(objLuotkham);
        }

        private void Frm_TiensuSanphukhoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void LoadUserConfigs()
        {
            try
            {
                chkCloseAfterSave.Checked = Utility.getUserConfigValue(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked)) == 1;

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
                Utility.SaveUserConfig(chkCloseAfterSave.Tag.ToString(), Utility.Bool2byte(chkCloseAfterSave.Checked));

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
       

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            bool result = uc_tiensubenh_cacdacdiemlienquan1.SaveData();
            if (result)
            {
                Utility.SetMsg(lblMsg, "Lưu thông tin thành công",false);
                if( chkCloseAfterSave.Checked)
                this.Close();
            }
            else
                Utility.SetMsg(lblMsg, "Lỗi khi lưu thông tin", false);
        }
        
       
    }
}
