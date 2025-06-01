using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_QuatrinhThaiky : Form
    {
        KcbLuotkham objLuotkham;
        public frm_QuatrinhThaiky(KcbLuotkham objLuotkham)
        {
            InitializeComponent();
            this.FormClosing += frm_QuatrinhThaiky_FormClosing;
            this.objLuotkham = objLuotkham;
            this.Shown += frm_QuatrinhThaiky_Shown;
            this.KeyDown += frm_QuatrinhThaiky_KeyDown;
        }

        private void frm_QuatrinhThaiky_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uc_quatrinh_thaiky1.HandleKeyEnter();
        }

        private void frm_QuatrinhThaiky_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
            uc_quatrinh_thaiky1.Init(objLuotkham);
        }

        private void frm_QuatrinhThaiky_FormClosing(object sender, FormClosingEventArgs e)
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
        private void cmdSave_Click(object sender, EventArgs e)
        {
          bool result= uc_quatrinh_thaiky1.Save();
            if (result)
            {
                Utility.SetMsg(lblMsg, "Lưu thông tin thành công", false);
                if (chkCloseAfterSave.Checked)
                    this.Close();
            }
            else
                Utility.SetMsg(lblMsg, "Lỗi khi lưu thông tin", false);

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
