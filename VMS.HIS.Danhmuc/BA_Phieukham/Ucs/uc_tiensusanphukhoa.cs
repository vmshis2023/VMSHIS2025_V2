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
    public partial class uc_tiensusanphukhoa : UserControl
    {
        EmrTiensusanphukhoa tsspk;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public uc_tiensusanphukhoa()
        {
            InitializeComponent();
        }
        public bool VisibleSaveButton
        {
            get { return cmdGhi.Visible; }
            set { cmdGhi.Visible = value; }
        }
        public void Init(KcbLuotkham objLuotkham, EmrTiensusanphukhoa tsspk)
        {

            this.objLuotkham = objLuotkham;
            this.tsspk = tsspk;
           
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
        public void Init(KcbLuotkham objLuotkham)
        {

            this.objLuotkham = objLuotkham;
            tsspk = new Select().From(EmrTiensusanphukhoa.Schema)
                        .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrTiensusanphukhoa>();
            DisplayData();

        }
        public void DisplayData()
        {
            try
            {
                if (tsspk == null)
                    tsspk = new Select().From(EmrTiensusanphukhoa.Schema)
                        .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrTiensusanphukhoa>();
                if (tsspk != null)
                {
                    txt_batdauthaykinh_nam.Text = Utility.sDbnull(tsspk.BaTsspkBatdauthaykinhNam);
                    txt_batdauthaykinhtuoi.Text = Utility.sDbnull(tsspk.BaTsspkBatdauthaykinhTuoi);
                    txt_tinhchatkinhnguyet.Text = Utility.sDbnull(tsspk.BaTsspkTinhchatkinhnguyet);
                    txt_chuky.Text = Utility.sDbnull(tsspk.BaTsspkChuky);
                    txt_songaythaykinh.Text = Utility.sDbnull(tsspk.BaTsspkSongaythaykinh);
                    txt_luongkinh.Text = Utility.sDbnull(tsspk.BaTsspkLuongkinh);
                    if (tsspk.BaTsspkKinhlancuoingay.HasValue)
                        dtpKinhlancuoingay.Value =tsspk.BaTsspkKinhlancuoingay.Value;
                    else
                        dtpKinhlancuoingay.ResetText();
                   chkCodaubung.Checked = Utility.Bool2Bool(tsspk.BaTsspkCodaubung);
                    chk_thoigiantruoc.Checked = Utility.Bool2Bool(tsspk.BaTsspkThoigianTruoc);
                    chk_thoigiantrong.Checked = Utility.Bool2Bool(tsspk.BaTsspkThoigianTrong);
                    chk_thoigiansau.Checked = Utility.Bool2Bool(tsspk.BaTsspkThoigianSau);
                    txt_laychongnam.Text = Utility.sDbnull(tsspk.BaTsspkLaychongNam);
                    txt_laychongtuoi.Text = Utility.sDbnull(tsspk.BaTsspkLaychongTuoi);
                    txt_hetkinhnam.Text = Utility.sDbnull(tsspk.BaTsspkHetkinhnam);
                    txt_hetkinhtuoi.Text = Utility.sDbnull(tsspk.BaTsspkHetkinhtuoi);
                    txt_benhphukhoadadieutri.Text = Utility.sDbnull(tsspk.BaTsspkBenhphukhoadadieutri);
                    txt_para.Text = Utility.sDbnull(tsspk.BaTsspkPara);
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
                        tsspk = new Select().From(EmrTiensusanphukhoa.Schema)
                   .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrTiensusanphukhoa>();
                        if (tsspk == null || tsspk.IdTsspk <= 0)
                        {
                            tsspk = new EmrTiensusanphukhoa();
                            tsspk.IsNew = true;
                            tsspk.NgayTao = DateTime.Now;
                            tsspk.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            tsspk.IsNew = false;
                            tsspk.MarkOld();
                            tsspk.NgaySua = DateTime.Now;
                            tsspk.NguoiSua = globalVariables.UserName;
                        }
                        tsspk.IdBenhnhan = objLuotkham.IdBenhnhan;
                        tsspk.MaLuotkham = objLuotkham.MaLuotkham;
                        tsspk.BaTsspkBatdauthaykinhNam = Utility.Int16Dbnull(txt_batdauthaykinh_nam.Text, 0);
                        tsspk.BaTsspkBatdauthaykinhTuoi = Utility.Int16Dbnull(txt_batdauthaykinhtuoi.Text, 0);
                        tsspk.BaTsspkTinhchatkinhnguyet = Utility.sDbnull(txt_tinhchatkinhnguyet.Text);
                        tsspk.BaTsspkChuky = Utility.Int16Dbnull(txt_chuky.Text, 0);
                        tsspk.BaTsspkSongaythaykinh = Utility.Int16Dbnull(txt_songaythaykinh.Text, 0);
                        tsspk.BaTsspkLuongkinh = Utility.sDbnull(txt_luongkinh.Text);
                        tsspk.BaTsspkKinhlancuoingay = dtpKinhlancuoingay.Value;
                        tsspk.BaTsspkCodaubung = chkCodaubung.Checked;
                        tsspk.BaTsspkThoigianTruoc = chk_thoigiantruoc.Checked;
                        tsspk.BaTsspkThoigianTrong = chk_thoigiantrong.Checked;
                        tsspk.BaTsspkThoigianSau = chk_thoigiansau.Checked;

                        tsspk.BaTsspkLaychongNam = Utility.Int16Dbnull(txt_laychongnam.Text, 0);
                        tsspk.BaTsspkLaychongTuoi = Utility.Int16Dbnull(txt_laychongtuoi.Text, 0);
                        tsspk.BaTsspkHetkinhnam = Utility.Int16Dbnull(txt_hetkinhnam.Text, 0);
                        tsspk.BaTsspkHetkinhtuoi = Utility.Int16Dbnull(txt_hetkinhtuoi.Text, 0);
                        tsspk.BaTsspkBenhphukhoadadieutri = Utility.sDbnull(txt_benhphukhoadadieutri.Text);
                        tsspk.BaTsspkPara = Utility.sDbnull(txt_para.Text);
                        tsspk.Save();
                        new Update(KcbThongtinchung.Schema).Set(KcbThongtinchung.Columns.Para).EqualTo(tsspk.BaTsspkPara)
                            .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                           .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                           .Execute();
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
