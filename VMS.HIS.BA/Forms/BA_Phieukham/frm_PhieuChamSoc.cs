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
using SubSonic;
using Janus.Windows.GridEX.EditControls;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_PhieuChamSoc : Form
    {
        public delegate void OnCreated(long id, action m_enAct);
        public event OnCreated _OnCreated;
        public action m_enAct = action.FirstOrFinished;
        KcbLuotkham objLuotkham;
        public bool mv_blnCallFromMenu = true;
        public bool IsChanged = false;
        public bool Force2Saved = false;
        public frm_PhieuChamSoc()
        {
            InitializeComponent();
            this.FormClosing += frm_PhieuChamSoc_FormClosing;
          
            this.Shown += frm_PhieuChamSoc_Shown;
            this.KeyDown += frm_PhieuChamSoc_KeyDown;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;
            uc_PhieuChamSoc1._OnMsg += _OnMsg;
            uc_PhieuChamSoc1._OnAction += Uc_PhieuChamSoc1__OnAction;
        }

        private void Uc_PhieuChamSoc1__OnAction(bool AllowSave)
        {
            cmdSua.Enabled = uc_PhieuChamSoc1.dtPhieuChamSoc != null && uc_PhieuChamSoc1.dtPhieuChamSoc.Rows.Count > 0;
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
          
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {

                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                NoitruPhieunhapvien objPNV = new Select().From(NoitruPhieunhapvien.Schema)
                    .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<NoitruPhieunhapvien>();
                if (objPNV == null)
                {
                    Utility.ShowMsg("Người bệnh chưa có phiếu nhập viện nên không thể tạo Phiếu chăm sóc");
                    return;
                }
                uc_phieu_nhap_vien1.ShowData(objPNV);
                uc_PhieuChamSoc1.Init(objLuotkham);
                cmdSua.Enabled = uc_PhieuChamSoc1.dtPhieuChamSoc!=null && uc_PhieuChamSoc1.dtPhieuChamSoc.Rows.Count > 0;
                uc_PhieuChamSoc1.dtp_ngay_thuchien.Focus();
            }
            else
            {
                uc_PhieuChamSoc1.Reset();
            }
        }

        private void frm_PhieuChamSoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control activeCtrl = Utility.getActiveControl(this);
                if (activeCtrl == null) return;
                if (activeCtrl.GetType().Equals(typeof(EditBox)))
                {
                    EditBox box = activeCtrl as EditBox;
                    if (box.Multiline)
                    {
                        return;
                    }
                    else
                        this.SelectNextControl(activeCtrl, true, true, true, true);
                }
                else if (activeCtrl.GetType().Equals(typeof(TextBox)))
                {
                    TextBox box = activeCtrl as TextBox;
                    if (box.Multiline)
                    {
                        return;
                    }
                    else
                        this.SelectNextControl(activeCtrl, true, true, true, true);
                }
                else
                    this.SelectNextControl(activeCtrl, true, true, true, true);

            }    
                //uc_PhieuChamSoc1.HandleKeyEnter();
        }

        private void frm_PhieuChamSoc_Shown(object sender, EventArgs e)
        {
            uc_PhieuChamSoc1.Init();
            uc_PhieuChamSoc1.Force2Saved = Force2Saved;
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

        private void frm_PhieuChamSoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void LoadUserConfigs()
        {
            try
            {
              
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
              

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
          bool result= uc_PhieuChamSoc1.Save();
            if (result)
            {
                cmdExit.PerformClick();
            }
          

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (cmdExit.Tag.ToString() == "0")
            {
                this.Close();

            }
            else
            {
                uc_PhieuChamSoc1.Huythaotac();
                cmdExit.Tag = "0";
                cmdExit.Text = "Thoát";
                cmdInphieu.Enabled = cmdSua.Enabled = uc_PhieuChamSoc1.dtPhieuChamSoc!=null && uc_PhieuChamSoc1.dtPhieuChamSoc.Rows.Count>0;
                cmdThemMoiPhieuChamSoc.Enabled = true;
                
                cmdSave.Enabled = false;
            }
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
            uc_PhieuChamSoc1.Print();
        }

        private void cmdThemMoiPhieuChamSoc_Click(object sender, EventArgs e)
        {
            cmdThemMoiPhieuChamSoc.Enabled = false;
            cmdSua.Enabled = false;
            cmdInphieu.Enabled = false;
            cmdSave.Enabled = true;
            uc_PhieuChamSoc1.Themmoi();
            cmdExit.Tag = "1";
            cmdExit.Text = "Hủy";
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            cmdThemMoiPhieuChamSoc.Enabled = false;
            cmdSua.Enabled = false;
            cmdInphieu.Enabled = false;
            cmdSave.Enabled = true;
            uc_PhieuChamSoc1.Sua();
            cmdExit.Tag = "1";
            cmdExit.Text = "Hủy";
        }
    }
}
