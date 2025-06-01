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
    public partial class uc_chandoan_sankhoa : UserControl
    {
        EmrChandoanSankhoa cdsk;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public uc_chandoan_sankhoa()
        {
            InitializeComponent();
        }
       
        public void Init(KcbLuotkham objLuotkham, EmrChandoanSankhoa cdsk)
        {

            this.objLuotkham = objLuotkham;
            this.cdsk = cdsk;
           
        }
        public void Init(KcbLuotkham objLuotkham)
        {

            this.objLuotkham = objLuotkham;
            cdsk = new Select().From(EmrChandoanSankhoa.Schema)
                        .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrChandoanSankhoa>();
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
                if (cdsk == null)
                    cdsk = new Select().From(EmrChandoanSankhoa.Schema)
                        .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrChandoanSankhoa>();
                if (cdsk != null)
                {
                    txtLucvaode.Text = Utility.sDbnull(cdsk.CdLucvaode);
                    txtNgoithai.Text = Utility.sDbnull(cdsk.CdNgoithai);
                    txtCachthucde.Text = Utility.sDbnull(cdsk.CdCachthucde);
                    txtKiemsoattucung.Text = Utility.sDbnull(cdsk.CdKiemsoattucung);
                    txtDitat.Text = Utility.sDbnull(cdsk.CdDitatThainhi);
                    nmrCannang.Text = Utility.sDbnull(cdsk.CdCannangThainhi);
                    if (cdsk.CdNgaymode.HasValue)
                        dtpNgaymode.Value = cdsk.CdNgaymode.Value;
                    else
                        dtpNgaymode.ResetText();
                    optDonthai.Checked = Utility.Bool2Bool(cdsk.CdDonthai);
                    optDathai.Checked = Utility.Bool2Bool(cdsk.CdDathai);
                    optTrai.Checked = Utility.Bool2Bool(cdsk.CdTrai);
                    optGai.Checked = Utility.Bool2Bool(cdsk.CdGai);
                    optSong.Checked = Utility.Bool2Bool(cdsk.CdSong);
                    optChet.Checked = Utility.Bool2Bool(cdsk.CdChet);

                }
                else
                    ClearControl();
                txtLucvaode.Focus();
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
                        cdsk = new Select().From(EmrChandoanSankhoa.Schema)
                   .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrChandoanSankhoa>();
                        if (cdsk == null || cdsk.Id <= 0)
                        {
                            cdsk = new EmrChandoanSankhoa();
                            cdsk.IsNew = true;
                            cdsk.NgayTao = DateTime.Now;
                            cdsk.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            cdsk.IsNew = false;
                            cdsk.MarkOld();
                            cdsk.NgaySua = DateTime.Now;
                            cdsk.NguoiSua = globalVariables.UserName;
                        }
                        cdsk.IdBenhnhan = objLuotkham.IdBenhnhan;
                        cdsk.MaLuotkham = objLuotkham.MaLuotkham;
                        cdsk.CdLucvaode = Utility.sDbnull(txtLucvaode.Text, "");
                        cdsk.CdNgoithai = Utility.sDbnull(txtNgoithai.Text, "");
                        cdsk.CdCachthucde = Utility.sDbnull(txtCachthucde.Text);
                        cdsk.CdKiemsoattucung = Utility.sDbnull(txtKiemsoattucung.Text, "");
                        cdsk.CdDitatThainhi = Utility.sDbnull(txtDitat.Text);
                        cdsk.CdCannangThainhi = Utility.sDbnull(nmrCannang.Text, "");

                        cdsk.CdNgaymode = dtpNgaymode.Value;
                        cdsk.CdDonthai = optDonthai.Checked;
                        cdsk.CdDathai = optDathai.Checked;
                        cdsk.CdSong = optSong.Checked;
                        cdsk.CdChet = optChet.Checked;
                        cdsk.CdTrai = optTrai.Checked;
                        cdsk.CdGai = optGai.Checked;
                       
                        cdsk.Save();
                       
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
