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
    public partial class uc_theodoitaibuongde : UserControl
    {
        EmrPhieutheodoiTaibuongde objPhieutheodoitaibuongde;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public uc_theodoitaibuongde()
        {
            InitializeComponent();
        }
       
        public void Init(KcbLuotkham objLuotkham, EmrPhieutheodoiTaibuongde objPhieutheodoitaibuongde)
        {

            this.objLuotkham = objLuotkham;
            this.objPhieutheodoitaibuongde = objPhieutheodoitaibuongde;
           
        }
        public void Init(KcbLuotkham objLuotkham)
        {

            this.objLuotkham = objLuotkham;
            objPhieutheodoitaibuongde = new Select().From(EmrPhieutheodoiTaibuongde.Schema)
                        .Where(EmrPhieutheodoiTaibuongde.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieutheodoiTaibuongde.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieutheodoiTaibuongde>();
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
                if (objPhieutheodoitaibuongde == null)
                    objPhieutheodoitaibuongde = new Select().From(EmrPhieutheodoiTaibuongde.Schema)
                        .Where(EmrPhieutheodoiTaibuongde.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieutheodoiTaibuongde.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<EmrPhieutheodoiTaibuongde>();
                if (objPhieutheodoitaibuongde != null)
                {
                    if(objPhieutheodoitaibuongde.Vaobuongdeluc.HasValue)
                    dtpVaobuongdeluc.Value = objPhieutheodoitaibuongde.Vaobuongdeluc.Value;
                    txtTennguoitheodoi.Text = objPhieutheodoitaibuongde.Nguoitheodoi;
                    txtChucdanhnguoitheodoi.Text = objPhieutheodoitaibuongde.Chucdanh;
                    if (objPhieutheodoitaibuongde.Deluc.HasValue)
                        dtpDeluc.Value = objPhieutheodoitaibuongde.Deluc.Value;

                    txt1phut.Text = objPhieutheodoitaibuongde.Apgar1phut;
                    txt5phut.Text = objPhieutheodoitaibuongde.Apgar5phut;
                    txt10phut.Text = objPhieutheodoitaibuongde.Apgar10phut;

                    nmrCannangRau.Value =Utility.Int32Dbnull( objPhieutheodoitaibuongde.TresosinhCannang);
                    nmrcao.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.TresosinhCao);
                    nmrvongdau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.TresosinhVongdau);
                    optDonthaiTrai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDonthaiTrai);
                    optDonthaiGai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDonthaiGai);
                    optDathaiTrai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDathaiTrai);
                    optDathaiGai.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhDathaiGai);
                    chkTatbamsinh.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.TresosinhTatbamsinh);
                    chkRaucuonco.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauRaucuonco);

                    txtCuthetatbamsinh.Text = objPhieutheodoitaibuongde.TresosinhCuthetatbamsinh;
                    txtTinhtrangtresosinhsaukhide.Text = objPhieutheodoitaibuongde.TresosinhTinhtrangsaude;
                    txtXulyvaketquaTresosinh.Text = objPhieutheodoitaibuongde.TresosinhXulyvaketqua;

                    optRauboc.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauBoc);
                    optRauso.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauSo);
                    if (objPhieutheodoitaibuongde.SorauLuc.HasValue)
                        dtpRausoluc.Value = objPhieutheodoitaibuongde.SorauLuc.Value;

                    txtCachsorau.Text = objPhieutheodoitaibuongde.SorauCachsorau;
                    txtMatmang.Text = objPhieutheodoitaibuongde.SorauMatmang;
                    txtMatmui.Text = objPhieutheodoitaibuongde.SorauMatmui;
                    txtBanhrau.Text = objPhieutheodoitaibuongde.SorauBanhrau;
                    nmrCannangRau.Value =Utility.Int32Dbnull( objPhieutheodoitaibuongde.SorauCannang);
                    chkRaucuonco.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauRaucuonco);
                    nmrCuongrau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.CuongrauDai);
                    chkCochaymausauso.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauChaymausauso);
                    nmrLuongmaumat.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.SorauLuongmaumat);
                    chkKiemsoattucung.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SorauKiemsoattucung);
                    txtXulyvaketquaRau.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SorauXulyvaketqua);

                    txtDaniemmac.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuDaniemmac);
                    optDethuong.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeThuong);
                    optForceps.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeForceps);
                    optGiachut.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeGiachut);
                    optPhauthuat.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdePt);
                    optDechihuy.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeDechihuy);
                    optKhac.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapdeKhac);
                    txtLydocanthiep.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuLydocanthiep);
                    optTangsinhmonRach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonRach);
                    optTangsinhmonKhongrach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonKhongrach);
                    optTangsinhmonCat.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuTangsinhmonCat);
                    chkPhuongphapkhauvaloaichi.Checked=Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichi);
                    txtPhuongphapkhauvaloaichi.Text = Utility.sDbnull(objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota);
                    nmrSomuikhau.Value = Utility.Int32Dbnull(objPhieutheodoitaibuongde.SanphuSomuikhau);
                    optCotucungKhongrach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuCotucungKhongrach);
                    optCotucungRach.Checked = Utility.Bool2Bool(objPhieutheodoitaibuongde.SanphuCotucungRach);

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
                        objPhieutheodoitaibuongde = new Select().From(EmrPhieutheodoiTaibuongde.Schema)
                   .Where(EmrPhieutheodoiTaibuongde.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieutheodoiTaibuongde.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieutheodoiTaibuongde>();
                        if (objPhieutheodoitaibuongde == null || objPhieutheodoitaibuongde.Id <= 0)
                        {
                            objPhieutheodoitaibuongde = new EmrPhieutheodoiTaibuongde();
                            objPhieutheodoitaibuongde.IsNew = true;
                            objPhieutheodoitaibuongde.NgayTao = DateTime.Now;
                            objPhieutheodoitaibuongde.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            objPhieutheodoitaibuongde.IsNew = false;
                            objPhieutheodoitaibuongde.MarkOld();
                            objPhieutheodoitaibuongde.NgaySua = DateTime.Now;
                            objPhieutheodoitaibuongde.NguoiSua = globalVariables.UserName;
                        }
                        objPhieutheodoitaibuongde.Vaobuongdeluc = dtpVaobuongdeluc.Value;
                        objPhieutheodoitaibuongde.Nguoitheodoi = Utility.sDbnull(txtTennguoitheodoi.Text);
                        objPhieutheodoitaibuongde.Chucdanh = Utility.sDbnull(txtChucdanhnguoitheodoi.Text);
                        
                        objPhieutheodoitaibuongde.Deluc = dtpDeluc.Value;
                        objPhieutheodoitaibuongde.Apgar1phut = Utility.sDbnull(txt1phut.Text);
                        objPhieutheodoitaibuongde.Apgar5phut = Utility.sDbnull(txt5phut.Text);
                        objPhieutheodoitaibuongde.Apgar10phut = Utility.sDbnull(txt10phut.Text);
                        objPhieutheodoitaibuongde.TresosinhCannang = Utility.Int16Dbnull(nmrCannangtresosinh.Value);
                        objPhieutheodoitaibuongde.TresosinhCao = Utility.Int16Dbnull(nmrcao.Value);
                        objPhieutheodoitaibuongde.TresosinhVongdau = Utility.Int16Dbnull(nmrvongdau.Value);

                        objPhieutheodoitaibuongde.TresosinhDonthaiTrai = optDonthaiTrai.Checked;
                        objPhieutheodoitaibuongde.TresosinhDonthaiGai = optDonthaiGai.Checked;
                        objPhieutheodoitaibuongde.TresosinhDathaiTrai = optDathaiTrai.Checked;
                        objPhieutheodoitaibuongde.TresosinhDathaiGai = optDathaiGai.Checked;
                        objPhieutheodoitaibuongde.TresosinhTatbamsinh = chkTatbamsinh.Checked;
                        objPhieutheodoitaibuongde.TresosinhCohaumon = chkCohaumon.Checked;
                        objPhieutheodoitaibuongde.TresosinhCuthetatbamsinh = Utility.sDbnull(txtCuthetatbamsinh.Text);
                        objPhieutheodoitaibuongde.TresosinhTinhtrangsaude = Utility.sDbnull(txtTinhtrangtresosinhsaukhide.Text);
                        objPhieutheodoitaibuongde.TresosinhXulyvaketqua = Utility.sDbnull(txtXulyvaketquaTresosinh.Text);

                        objPhieutheodoitaibuongde.SorauBoc = optRauboc.Checked;
                        objPhieutheodoitaibuongde.SorauSo = optRauso.Checked;

                        objPhieutheodoitaibuongde.SorauLuc = dtpRausoluc.Value;
                        objPhieutheodoitaibuongde.SorauCachsorau = Utility.sDbnull(txtCachsorau.Text);
                        objPhieutheodoitaibuongde.SorauMatmang = Utility.sDbnull(txtMatmang.Text);
                        objPhieutheodoitaibuongde.SorauMatmui = Utility.sDbnull(txtMatmui.Text);
                        objPhieutheodoitaibuongde.SorauBanhrau = Utility.sDbnull(txtBanhrau.Text);
                        objPhieutheodoitaibuongde.CuongrauDai = Utility.Int16Dbnull(nmrCuongrau.Value);
                        objPhieutheodoitaibuongde.SorauChaymausauso = chkCochaymausauso.Checked;
                        objPhieutheodoitaibuongde.SorauLuongmaumat = Utility.Int16Dbnull(nmrLuongmaumat.Value);
                        objPhieutheodoitaibuongde.SorauKiemsoattucung = chkKiemsoattucung.Checked;
                        objPhieutheodoitaibuongde.SorauXulyvaketqua = Utility.sDbnull(txtXulyvaketquaRau.Text);

                        objPhieutheodoitaibuongde.SanphuDaniemmac = Utility.sDbnull(txtDaniemmac.Text);
                        objPhieutheodoitaibuongde.SanphuPhuongphapdeThuong = optDethuong.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapdeForceps = optForceps.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapdeGiachut = optGiachut.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapdePt = optPhauthuat.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapdeDechihuy = optDechihuy.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapdeKhac = optKhac.Checked;
                        objPhieutheodoitaibuongde.SanphuLydocanthiep = Utility.sDbnull(txtLydocanthiep.Text);
                        objPhieutheodoitaibuongde.SanphuTangsinhmonKhongrach = optTangsinhmonKhongrach.Checked;
                        objPhieutheodoitaibuongde.SanphuTangsinhmonRach = optTangsinhmonRach.Checked;
                        objPhieutheodoitaibuongde.SanphuTangsinhmonCat = optTangsinhmonCat.Checked;

                        objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichi = chkPhuongphapkhauvaloaichi.Checked;
                        objPhieutheodoitaibuongde.SanphuPhuongphapkhauvaloaichiMota = Utility.sDbnull(txtPhuongphapkhauvaloaichi.Text);
                        objPhieutheodoitaibuongde.SanphuSomuikhau = Utility.Int16Dbnull(nmrSomuikhau.Value);

                        objPhieutheodoitaibuongde.SanphuCotucungRach = optCotucungRach.Checked;
                        objPhieutheodoitaibuongde.SanphuCotucungKhongrach = optCotucungKhongrach.Checked;
                        


                        objPhieutheodoitaibuongde.Save();
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
