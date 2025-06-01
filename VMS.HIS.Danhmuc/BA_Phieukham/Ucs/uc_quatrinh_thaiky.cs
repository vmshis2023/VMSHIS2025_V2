using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using Janus.Windows.GridEX.EditControls;
using VMS.HIS.Danhmuc.Dungchung;
using System;
using System.Transactions;

namespace VMS.EMR.PHIEUKHAM.Ucs
{
    public partial class uc_quatrinh_thaiky : UserControl
    {
        EmrQuatrinhThaiky qttk;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public uc_quatrinh_thaiky()
        {
            InitializeComponent();
        }
       
        public void Init(KcbLuotkham objLuotkham, EmrQuatrinhThaiky qttk)
        {

            this.objLuotkham = objLuotkham;
            this.qttk = qttk;
           
        }
        public void Init(KcbLuotkham objLuotkham)
        {

            this.objLuotkham = objLuotkham;
            qttk = new Select().From(EmrQuatrinhThaiky.Schema)
                        .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrQuatrinhThaiky>();
            DisplayData();

        }
        public void HandleKeyEnter()
        {
            Control activeCtrl = Utility.getActiveControl(this);
            if (activeCtrl.GetType().Equals(typeof(EditBox)))
            {
                EditBox box = activeCtrl as EditBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else if (activeCtrl.GetType().Equals(typeof(TextBox)))
            {
                TextBox box = activeCtrl as TextBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else
                SendKeys.Send("{TAB}");
        }
        public void DisplayData()
        {
            try
            {
                if (qttk == null)
                    qttk = new Select().From(EmrQuatrinhThaiky.Schema)
                        .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrQuatrinhThaiky>();
                if (qttk != null)
                {
                    if(qttk.Kinhcuoitungay.HasValue)
                    dtpKinhcuoitungay.Value = qttk.Kinhcuoitungay.Value;
                    if (qttk.Kinhcuoidenngay.HasValue)
                        dtpKinhcuoidenngay.Value = qttk.Kinhcuoidenngay.Value;
                    txtKhamthaitai.Text = Utility.sDbnull(qttk.Khamthaitai);
                    chkTiemphonguonvan.Checked= Utility.Bool2Bool(qttk.TiemphongUonvan);
                    txtDuoctiemphonguonvanSolan.Text = Utility.sDbnull(qttk.TiemphongUonvanSolan);
                    if (qttk.Batdauchuyenda.HasValue)
                        dtpBatdauchuyendatu.Value = qttk.Batdauchuyenda.Value;
                    txtDauhieuLucdau.Text = Utility.sDbnull(qttk.Dauhieulucdau);
                    txtBienchuyen.Text = Utility.sDbnull(qttk.Bienchuyen);
                }
                else
                    ClearControl();
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       void ClearControl()
        {
            foreach (Control ctr in this.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)(ctr)).Checked=false;
                }
        }
        public bool Save()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        qttk = new Select().From(EmrQuatrinhThaiky.Schema)
                   .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrQuatrinhThaiky>();
                        if (qttk == null || qttk.Id <= 0)
                        {
                            qttk = new EmrQuatrinhThaiky();
                            qttk.IsNew = true;
                            qttk.NgayTao = DateTime.Now;
                            qttk.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            qttk.IsNew = false;
                            qttk.MarkOld();
                            qttk.NgaySua = DateTime.Now;
                            qttk.NguoiSua = globalVariables.UserName;
                        }
                        qttk.IdBenhnhan = objLuotkham.IdBenhnhan;
                        qttk.MaLuotkham = objLuotkham.MaLuotkham;
                        qttk.Kinhcuoitungay = dtpKinhcuoitungay.Value;
                        qttk.Kinhcuoidenngay = dtpKinhcuoidenngay.Value;

                        qttk.Khamthaitai = Utility.sDbnull(txtKhamthaitai.Text, "");
                        qttk.TiemphongUonvan = chkTiemphonguonvan.Checked;
                        qttk.TiemphongUonvanSolan = Utility.ByteDbnull(txtDuoctiemphonguonvanSolan.Text, "");
                        qttk.Batdauchuyenda = dtpBatdauchuyendatu.Value;
                        qttk.Dauhieulucdau = Utility.sDbnull(txtDauhieuLucdau.Text);
                        qttk.Bienchuyen = Utility.sDbnull(txtBienchuyen.Text, "");
                        
                        qttk.Save();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
