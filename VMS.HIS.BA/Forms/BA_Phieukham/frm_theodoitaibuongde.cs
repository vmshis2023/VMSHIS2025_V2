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

namespace VMS.HIS.UI.EMR
{
    public partial class frm_theodoitaibuongde : Form
    {
        KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        public frm_theodoitaibuongde(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.FormClosing += frm_theodoitaibuongde_FormClosing;
            this.objLuotkham = objLuotkham;
            this.objBenhnhan = objBenhnhan;
            this.Shown += frm_theodoitaibuongde_Shown;
            this.KeyDown += frm_theodoitaibuongde_KeyDown;
        }

        private void frm_theodoitaibuongde_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uc_theodoitaibuongde1.HandleKeyEnter();
        }

        private void frm_theodoitaibuongde_Shown(object sender, EventArgs e)
        {
            LoadUserConfigs();
            uc_theodoitaibuongde1.Init(objLuotkham);
        }

        private void frm_theodoitaibuongde_FormClosing(object sender, FormClosingEventArgs e)
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
          bool result= uc_theodoitaibuongde1.Save();
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
