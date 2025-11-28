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
    public partial class frm_giayxacnhan_quatrinhdieutrinoitru : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        public action m_enAct = action.FirstOrFinished;
        KcbLuotkham objLuotkham;
        public bool mv_blnCallFromMenu = true;
        public bool IsChanged = false;
        public bool Force2Saved = false;
        public frm_giayxacnhan_quatrinhdieutrinoitru()
        {
            InitializeComponent();
            this.FormClosing += frm_giayxacnhan_quatrinhdieutrinoitru_FormClosing;
          
            this.Shown += frm_giayxacnhan_quatrinhdieutrinoitru_Shown;
            this.KeyDown += frm_giayxacnhan_quatrinhdieutrinoitru_KeyDown;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1._OnMsg += _OnMsg;
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1._OnStatus += _OnStatus;
        }
        public void InitData(KcbLuotkham objLuotkham)
        {
            this.objLuotkham = objLuotkham;
        }
        private void _OnStatus(bool isNew)
        {
            cmdInphieu.Enabled = !isNew;
        }

        private void _OnMsg(string msg, bool IsSucess = false)
        {
            Utility.SetMsg(lblMsg, msg, !IsSucess);
        }

        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            IsChanged = false;
            objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.Init(objLuotkham);
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.dtpNgayxacnhan.Focus();
        }

        private void frm_giayxacnhan_quatrinhdieutrinoitru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.HandleKeyEnter();
        }

        private void frm_giayxacnhan_quatrinhdieutrinoitru_Shown(object sender, EventArgs e)
        {
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.Init();
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.Force2Saved = Force2Saved;
            if (mv_blnCallFromMenu)
            {
                chkCloseAfterSave.Checked = false;
                chkCloseAfterSave.Visible = false;
            }
            LoadUserConfigs();
            if (objLuotkham != null) ucThongtinnguoibenh_emr_basic1.Refresh(objLuotkham);
            else
            {
                _OnStatus(true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
        }

        private void frm_giayxacnhan_quatrinhdieutrinoitru_FormClosing(object sender, FormClosingEventArgs e)
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
            IsChanged = true;
            bool result= uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.Save();
            if (result)
            {
                m_enAct = action.Update;
                if (_OnCreated != null) _OnCreated(uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.giayxacnhan.Id, m_enAct);
                if (chkCloseAfterSave.Visible && chkCloseAfterSave.Checked)
                    this.Close();
            }
           

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
            uc_tt25_giayxacnhan_quatrinhdieutrinoitru1.Print();
        }
    }
}
