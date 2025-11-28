using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VMS.HIS.EMR.Forms.BA_Phieukham;
using VNS.Libs;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_hoso_theodoi_sosinh : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        public action m_enAct = action.FirstOrFinished;
        KcbLuotkham objLuotkham;
        public bool mv_blnCallFromMenu = true;
        public bool IsChanged = false;
        public long id_giaychungsinh = 0;
        EmrHosoTheodoiSosinh phieutheodoisosinh;
        public frm_hoso_theodoi_sosinh()
        {
            InitializeComponent();
            this.FormClosing += frm_hoso_theodoi_sosinh_FormClosing;
          
            this.Shown += frm_hoso_theodoi_sosinh_Shown;
            this.KeyDown += frm_hoso_theodoi_sosinh_KeyDown;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;
            uc_phieutheodoi_tresosinh1._OnMsg += _OnMsg;
            uc_phieutheodoi_tresosinh1._OnStatus += _OnStatus;
         
        }
        public void InitData(KcbLuotkham objLuotkham, EmrHosoTheodoiSosinh phieutheodoisosinh)
        {
            this.objLuotkham = objLuotkham;
            this.phieutheodoisosinh = phieutheodoisosinh;
        }
        
        private void _OnStatus(bool isNew)
        {
           
        }

        private void _OnMsg(string msg, bool IsSucess = false)
        {
            Utility.SetMsg(lblMsg, msg, !IsSucess);
        }

        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {
                //if (id_giaychungsinh <= 0)//Hiển thị
                //{
                //    DateTime dtNgay = new DateTime(1900, 1, 1);
                //    DataTable dtData = SPs.EmrGiayChungsinhLaydanhsach(-1, dtNgay, dtNgay, "", 100, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, "", "", "", 100).GetDataSet().Tables[0];
                //    if (dtData.Rows.Count == 1)
                //    {
                //        id_giaychungsinh = Utility.Int64Dbnull(dtData.Rows[0]["id"]);
                //    }
                //    else
                //    {
                //        frm_danhsach_giaychungsinh_theome _giaychungsinh_theome = new frm_danhsach_giaychungsinh_theome(ucThongtinnguoibenh_emr_basic1.objLuotkham, dtData);
                //        if (_giaychungsinh_theome.ShowDialog() == DialogResult.OK)
                //        {
                //            id_giaychungsinh = _giaychungsinh_theome.id_giaychungsinh;
                //        }
                //    }
                //}
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                uc_phieutheodoi_tresosinh1.Init(objLuotkham, this.phieutheodoisosinh);
                uc_phieutheodoi_tresosinh1.dtp_ngayphieu.Focus();
               
            }
          
        }

        private void frm_hoso_theodoi_sosinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                uc_phieutheodoi_tresosinh1.HandleKeyEnter();
        }

        private void frm_hoso_theodoi_sosinh_Shown(object sender, EventArgs e)
        {
            uc_phieutheodoi_tresosinh1.Init();
            if (mv_blnCallFromMenu)
            {
              
            }
            LoadUserConfigs();
            if (objLuotkham != null) ucThongtinnguoibenh_emr_basic1.Refresh(objLuotkham);
            else
            {
                _OnStatus(true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
        }

        private void frm_hoso_theodoi_sosinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void LoadUserConfigs()
        {
           
        }
        void SaveUserConfigs()
        {
            
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
          bool result= uc_phieutheodoi_tresosinh1.Save(0);
            if (result)
            {
                m_enAct = action.Update;
                if (_OnCreated != null) _OnCreated(uc_phieutheodoi_tresosinh1._phieu.Id, m_enAct);
               
            }
          

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
           
        }

       
       
    }
}
