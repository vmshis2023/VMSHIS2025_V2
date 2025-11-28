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

namespace VMS.HIS.EMR.Forms.BA_Phieukham.Ucs
{
    public partial class uc_ba_tieuhoa : UserControl
    {
        public uc_ba_tieuhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_tieuhoa_binhthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBinhthuong);
                opt_tieuhoa_batthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBatthuong);
                txt_tieuhoa_mota.Text = Utility.sDbnull(objPK.TieuhoaMota);

                opt_daubung_khong.Checked = Utility.Bool2Bool(objPK.DaubungKhong);
                opt_daubung_co.Checked = Utility.Bool2Bool(objPK.DaubungCo);
                txt_daubung_vitri.Text = Utility.sDbnull(objPK.DaubungVitri);

                opt_buonnon_co.Checked = Utility.Bool2Bool(objPK.BuonnonCo);
                opt_buonnon_khong.Checked = Utility.Bool2Bool(objPK.BuonnonKhong);

                opt_non_co.Checked = Utility.Bool2Bool(objPK.NonCo);
                opt_non_khong.Checked = Utility.Bool2Bool(objPK.NonKhong);
                chk_non_mautuoi.Checked = Utility.Bool2Bool(objPK.NonMautuoi);
                chk_non_mautham.Checked = Utility.Bool2Bool(objPK.NonMautham);

                opt_phancomau_co.Checked = Utility.Bool2Bool(objPK.PhancomauCo);
                opt_phancomau_khong.Checked = Utility.Bool2Bool(objPK.PhancomauKhong);
                chk_phancomau_mautuoi.Checked = Utility.Bool2Bool(objPK.PhancomauMautuoi);
                chk_phancomau_mautham.Checked = Utility.Bool2Bool(objPK.PhancomauMautham);
                chk_phancomau_phanden.Checked = Utility.Bool2Bool(objPK.PhancomauPhanden);


                opt_tieuchay_co.Checked = Utility.Bool2Bool(objPK.TieuchayCo);
                opt_tieuchay_khong.Checked = Utility.Bool2Bool(objPK.TieuchayKhong);
                nmr_tieuchay_solan.Value = Utility.DecimaltoDbnull(objPK.TieuchaySolan);

                opt_chuongbung_khong.Checked = Utility.Bool2Bool(objPK.ChuongbungKhong);
                opt_chuongbung_co.Checked = Utility.Bool2Bool(objPK.ChuongbungCo);

                opt_phanung_thanhbung_co.Checked = Utility.Bool2Bool(objPK.PhanungThanhbungCo);
                opt_phanung_thanhbung_khong.Checked = Utility.Bool2Bool(objPK.PhanungThanhbungKhong);

                opt_lach_binhthuong.Checked = Utility.Bool2Bool(objPK.LachBinhthuong);
                opt_lach_batthuong.Checked = Utility.Bool2Bool(objPK.LachBatthuong);
                nmr_lach_do.Value = Utility.DecimaltoDbnull(objPK.LachDo);

                opt_gan_binhthuong.Checked = Utility.Bool2Bool(objPK.GanBinhthuong);
                chk_gan_to.Checked = Utility.Bool2Bool(objPK.GanTo);
                nmr_gan_kichthuoc.Value = Utility.DecimaltoDbnull(objPK.GanKichthuoc);

                chk_gan_cotruong.Checked = Utility.Bool2Bool(objPK.GanCotruong);
                chk_gan_tuanhoang_banghe.Checked = Utility.Bool2Bool(objPK.GanTuanhoangBanghe);

                txt_tieuhoa_khac_mota.Text = Utility.sDbnull(objPK.TieuhoaKhacMota);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void SetData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                objPK.TieuhoaBinhthuong = opt_tieuhoa_binhthuong.Checked;
                objPK.TieuhoaBatthuong = opt_tieuhoa_batthuong.Checked;
                objPK.TieuhoaMota = opt_tieuhoa_batthuong.Checked?Utility.sDbnull(txt_tieuhoa_mota.Text):"";

                objPK.DaubungKhong = opt_daubung_khong.Checked;
                objPK.DaubungCo = opt_daubung_co.Checked;
                objPK.DaubungVitri = opt_daubung_co.Checked? Utility.sDbnull(txt_daubung_vitri.Text):"";

                objPK.BuonnonCo = opt_buonnon_co.Checked;
                objPK.BuonnonKhong = opt_buonnon_khong.Checked;

                objPK.NonCo = opt_non_co.Checked;
                objPK.NonKhong = opt_non_khong.Checked;
                objPK.NonMautuoi = opt_non_co.Checked? chk_non_mautuoi.Checked:false;
                objPK.NonMautham = opt_non_co.Checked? chk_non_mautham.Checked:false;

                objPK.PhancomauCo = opt_phancomau_co.Checked;
                objPK.PhancomauKhong = opt_phancomau_khong.Checked;
                objPK.PhancomauMautuoi = opt_phancomau_co.Checked?chk_phancomau_mautuoi.Checked:false;
                objPK.PhancomauMautham = opt_phancomau_co.Checked?chk_phancomau_mautham.Checked:false;
                objPK.PhancomauPhanden = opt_phancomau_co.Checked? chk_phancomau_phanden.Checked:false;

                objPK.TieuchayCo = opt_tieuchay_co.Checked;
                objPK.TieuchayKhong = opt_tieuchay_khong.Checked;
                objPK.TieuchaySolan = opt_tieuchay_co.Checked?Utility.Int32Dbnull( nmr_tieuchay_solan.Value):0;

                objPK.ChuongbungKhong = opt_chuongbung_khong.Checked;
                objPK.ChuongbungCo = opt_chuongbung_co.Checked;

                objPK.PhanungThanhbungCo = opt_phanung_thanhbung_co.Checked;
                objPK.PhanungThanhbungKhong = opt_phanung_thanhbung_khong.Checked;

                objPK.LachBinhthuong = opt_lach_binhthuong.Checked;
                objPK.LachBatthuong = opt_lach_batthuong.Checked;
                objPK.LachDo = opt_lach_batthuong.Checked? Utility.Int32Dbnull(nmr_lach_do.Value):0;

                objPK.GanBinhthuong = opt_gan_binhthuong.Checked;
                objPK.GanTo = chk_gan_to.Checked;
                objPK.GanKichthuoc = chk_gan_to.Checked? Utility.Int32Dbnull(nmr_gan_kichthuoc.Value):0;

                objPK.GanCotruong = chk_gan_to.Checked ? chk_gan_cotruong.Checked:false ;
                objPK.GanTuanhoangBanghe = chk_gan_to.Checked?chk_gan_tuanhoang_banghe.Checked:false;

                objPK.TieuhoaKhacMota = Utility.sDbnull(txt_tieuhoa_khac_mota.Text);




            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_tieuhoa_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tieuhoa_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tieuhoa_mota.Focus();
        }

        private void opt_daubung_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_daubung_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_daubung_vitri.Focus();
        }

        private void opt_non_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            chk_non_mautuoi.Enabled = chk_non_mautham.Enabled= _obj.Checked;
        }

        private void opt_phancomau_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            chk_phancomau_mautuoi.Enabled = chk_phancomau_mautham.Enabled= chk_phancomau_phanden.Enabled= _obj.Checked;
        }

        private void opt_tieuchay_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            nmr_tieuchay_solan.Enabled = _obj.Checked;
            if (_obj.Checked) nmr_tieuchay_solan.Focus();
        }

        private void opt_lach_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            nmr_lach_do.Enabled = _obj.Checked;
            if (_obj.Checked) nmr_lach_do.Focus();
        }

        private void chk_gan_to_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _obj = sender as CheckBox;
            nmr_gan_kichthuoc.Enabled = chk_gan_cotruong.Enabled= chk_gan_tuanhoang_banghe.Enabled= _obj.Checked;
            if (_obj.Checked) nmr_gan_kichthuoc.Focus();
        }
    }
}
