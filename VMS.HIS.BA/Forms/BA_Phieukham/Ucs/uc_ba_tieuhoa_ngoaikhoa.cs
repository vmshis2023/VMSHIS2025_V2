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
    public partial class uc_ba_tieuhoa_ngoaikhoa : UserControl
    {
        public uc_ba_tieuhoa_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_tieuhoa_binhthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBinhthuong);
                opt_tieuhoa_batthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBatthuong);
                txt_tieuhoa_mota.Text = Utility.sDbnull(objPK.TieuhoaMota);

                opt_chuongbung_khong.Checked = Utility.Bool2Bool(objPK.BungchuongKhong);
                opt_chuongbung_co.Checked = Utility.Bool2Bool(objPK.BungchuongCo);

                opt_seomocu_co.Checked = Utility.Bool2Bool(objPK.SeomocuCo);
                opt_seomocu_khong.Checked = Utility.Bool2Bool(objPK.SeomocuKhong);
                txt_seomocu_vitri.Text = Utility.sDbnull(objPK.SeomocuVitri);

                opt_vetthuongthanhbung_co.Checked = Utility.Bool2Bool(objPK.VetthuongthanhbungCo);
                opt_vetthuongthanhbung_khong.Checked = Utility.Bool2Bool(objPK.VetthuongthanhbungKhong);
                txt_vetthuongthanhbung_vitri.Text = Utility.sDbnull(objPK.VetthuongthanhbungVitri);

                opt_camung_phucmac_co.Checked = Utility.Bool2Bool(objPK.CamungPhucmacCo);
                opt_camung_phucmac_khong.Checked = Utility.Bool2Bool(objPK.CamungPhucmacKhong);
                txt_camung_phucmac_vitri.Text = Utility.sDbnull(objPK.CamungPhucmacVitri);

                opt_tuanhoanbanghe_co.Checked = Utility.Bool2Bool(objPK.TuanhoanbangheCo);
                opt_tuanhoanbanghe_khong.Checked = Utility.Bool2Bool(objPK.TuanhoanbangheKhong);

                opt_quairuotnoi_khong.Checked = Utility.Bool2Bool(objPK.QuairuotnoiKhong);
                opt_quairuotnoi_co.Checked = Utility.Bool2Bool(objPK.QuairuotnoiCo);

                opt_dauhieuranbo_co.Checked = Utility.Bool2Bool(objPK.DauhieuranboCo);
                opt_dauhieuranbo_khong.Checked = Utility.Bool2Bool(objPK.DauhieuranboKhong);

                opt_ganto_co.Checked = Utility.Bool2Bool(objPK.GantoCo);
                opt_ganto_khong.Checked = Utility.Bool2Bool(objPK.GantoKhong);

                opt_tieuhoabamtim_co.Checked = Utility.Bool2Bool(objPK.TieuhoabamtimCo);
                opt_tieuhoabamtim_khong.Checked = Utility.Bool2Bool(objPK.TieuhoabamtimKhong);

                opt_tuimatto_co.Checked = Utility.Bool2Bool(objPK.TuimattoCo);
                opt_tuimatto_khong.Checked = Utility.Bool2Bool(objPK.TuimattoKhong);


                opt_bungmem_co.Checked = Utility.Bool2Bool(objPK.BungmemCo);
                opt_bungmem_khong.Checked = Utility.Bool2Bool(objPK.BungmemKhong);

                opt_dauhieu_murphy_co.Checked = Utility.Bool2Bool(objPK.DauhieuMurphyCo);
                opt_dauhieu_murphy_khong.Checked = Utility.Bool2Bool(objPK.DauhieuMurphyKhong);

                opt_phanungthanhbung_khong.Checked = Utility.Bool2Bool(objPK.PhanungthanhbungKhong);
                opt_phanung_thanhbung_co.Checked = Utility.Bool2Bool(objPK.PhanungthanhbungCo);


                opt_lachto_co.Checked = Utility.Bool2Bool(objPK.LachtoCo);
                opt_lachto_khong.Checked = Utility.Bool2Bool(objPK.LachtoKhong);

                opt_cocungthanhbung_co.Checked = Utility.Bool2Bool(objPK.CocungthanhbungCo);
                opt_cocungthanhbung_khong.Checked = Utility.Bool2Bool(objPK.CocungthanhbungKhong);

                opt_khoi_u_co.Checked = Utility.Bool2Bool(objPK.KhoiUCo);
                opt_khoi_u_khong.Checked = Utility.Bool2Bool(objPK.KhoiUKhong);
                txt_khoi_u_vitri.Text = Utility.sDbnull(objPK.KhoiUVitri);

                opt_diemdau_co.Checked = Utility.Bool2Bool(objPK.DiemdauCo);
                opt_diemdau_khong.Checked = Utility.Bool2Bool(objPK.DiemdauKhong);

                opt_thoaivi_co.Checked = Utility.Bool2Bool(objPK.ThoaiviCo);
                opt_thoaivi_khong.Checked = Utility.Bool2Bool(objPK.ThoaiviKhong);
                txt_thoaivi_vitri.Text = Utility.sDbnull(objPK.ThoaiviVitri);

                opt_phan_binhthuong.Checked = Utility.Bool2Bool(objPK.PhanBinhthuong);
                opt_phan_batthuong.Checked = Utility.Bool2Bool(objPK.PhanBatthuong);
                txt_phan_mota.Text = Utility.sDbnull(objPK.PhanMota);

                opt_sothayu_co.Checked = Utility.Bool2Bool(objPK.SothayuCo);
                opt_sothayu_khong.Checked = Utility.Bool2Bool(objPK.SothayuKhong);
                nmr_sothayu_khoangcach.Value = Utility.DecimaltoDbnull(objPK.SothayuKhoangcach);

                opt_douglas_co.Checked = Utility.Bool2Bool(objPK.DouglasCo);
                opt_douglas_khong.Checked = Utility.Bool2Bool(objPK.DouglasKhong);

                opt_cothathaumon_binhthuong.Checked = Utility.Bool2Bool(objPK.CothathaumonBinhthuong);
                opt_cothathaumon_batthuong.Checked = Utility.Bool2Bool(objPK.CothathaumonBatthuong);

                txt_tieuhoa_khac_mota.Text = Utility.sDbnull(objPK.TieuhoaKhacMota);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void SetData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                objPK.TieuhoaBinhthuong = opt_tieuhoa_binhthuong.Checked;
                objPK.TieuhoaBatthuong = opt_tieuhoa_batthuong.Checked;
                objPK.TieuhoaMota = opt_tieuhoa_batthuong.Checked ? Utility.sDbnull(txt_tieuhoa_mota.Text):"";

                objPK.BungchuongKhong = opt_chuongbung_khong.Checked;
                objPK.BungchuongCo = opt_chuongbung_co.Checked;

                objPK.SeomocuCo = opt_seomocu_co.Checked;
                objPK.SeomocuKhong = opt_seomocu_khong.Checked;
                objPK.SeomocuVitri = opt_seomocu_co.Checked ? Utility.sDbnull(txt_seomocu_vitri.Text):"";

                objPK.VetthuongthanhbungCo = opt_vetthuongthanhbung_co.Checked;
                objPK.VetthuongthanhbungKhong = opt_vetthuongthanhbung_khong.Checked;
                objPK.VetthuongthanhbungVitri = opt_vetthuongthanhbung_co.Checked ? Utility.sDbnull(txt_vetthuongthanhbung_vitri.Text):"";

                objPK.CamungPhucmacCo = opt_camung_phucmac_co.Checked;
                objPK.CamungPhucmacKhong = opt_camung_phucmac_khong.Checked;
                objPK.CamungPhucmacVitri = opt_camung_phucmac_co.Checked ? Utility.sDbnull(txt_camung_phucmac_vitri.Text):"";

                objPK.TuanhoanbangheCo = opt_tuanhoanbanghe_co.Checked;
                objPK.TuanhoanbangheKhong = opt_tuanhoanbanghe_khong.Checked;

                objPK.QuairuotnoiKhong = opt_quairuotnoi_khong.Checked;
                objPK.QuairuotnoiCo = opt_quairuotnoi_co.Checked;

                objPK.DauhieuranboCo = opt_dauhieuranbo_co.Checked;
                objPK.DauhieuranboKhong = opt_dauhieuranbo_khong.Checked;

                objPK.GantoCo = opt_ganto_co.Checked;
                objPK.GantoKhong = opt_ganto_khong.Checked;

                objPK.TieuhoabamtimCo = opt_tieuhoabamtim_co.Checked;
                objPK.TieuhoabamtimKhong = opt_tieuhoabamtim_khong.Checked;

                objPK.TuimattoCo = opt_tuimatto_co.Checked;
                objPK.TuimattoKhong = opt_tuimatto_khong.Checked;

                objPK.BungmemCo = opt_bungmem_co.Checked;
                objPK.BungmemKhong = opt_bungmem_khong.Checked;

                objPK.DauhieuMurphyCo = opt_dauhieu_murphy_co.Checked;
                objPK.DauhieuMurphyKhong = opt_dauhieu_murphy_khong.Checked;

                objPK.PhanungthanhbungKhong = opt_phanungthanhbung_khong.Checked;
                objPK.PhanungthanhbungCo = opt_phanung_thanhbung_co.Checked;

                objPK.LachtoCo = opt_lachto_co.Checked;
                objPK.LachtoKhong = opt_lachto_khong.Checked;

                objPK.CocungthanhbungCo = opt_cocungthanhbung_co.Checked;
                objPK.CocungthanhbungKhong = opt_cocungthanhbung_khong.Checked;

                objPK.KhoiUCo = opt_khoi_u_co.Checked;
                objPK.KhoiUKhong = opt_khoi_u_khong.Checked;
                objPK.KhoiUVitri = opt_khoi_u_co.Checked ? Utility.sDbnull(txt_khoi_u_vitri.Text):"";

                objPK.DiemdauCo = opt_diemdau_co.Checked;
                objPK.DiemdauKhong = opt_diemdau_khong.Checked;

                objPK.ThoaiviCo = opt_thoaivi_co.Checked;
                objPK.ThoaiviKhong = opt_thoaivi_khong.Checked;
                objPK.ThoaiviVitri = opt_thoaivi_co.Checked ? Utility.sDbnull(txt_thoaivi_vitri.Text):"";

                objPK.PhanBinhthuong = opt_phan_binhthuong.Checked;
                objPK.PhanBatthuong = opt_phan_batthuong.Checked;
                objPK.PhanMota = opt_phan_batthuong.Checked ? Utility.sDbnull(txt_phan_mota.Text):"";

                objPK.SothayuCo = opt_sothayu_co.Checked;
                objPK.SothayuKhong = opt_sothayu_khong.Checked;
                objPK.SothayuKhoangcach = opt_sothayu_co.Checked ? Utility.Int32Dbnull( nmr_sothayu_khoangcach.Value):0;

                objPK.DouglasCo = opt_douglas_co.Checked;
                objPK.DouglasKhong = opt_douglas_khong.Checked;

                objPK.CothathaumonBinhthuong = opt_cothathaumon_binhthuong.Checked;
                objPK.CothathaumonBatthuong = opt_cothathaumon_batthuong.Checked;

                objPK.TieuhoaKhacMota = Utility.sDbnull(txt_tieuhoa_khac_mota.Text);





            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_tieuhoa_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tieuhoa_mota, sender as RadioButton);
           
        }

        private void opt_seomocu_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_seomocu_vitri, sender as RadioButton);
        }

        private void opt_vetthuongthanhbung_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_vetthuongthanhbung_vitri, sender as RadioButton);
        }

        private void opt_camung_phucmac_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_camung_phucmac_vitri, sender as RadioButton);
        }

        private void opt_khoi_u_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_khoi_u_vitri, sender as RadioButton);
        }

        private void opt_thoaivi_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_thoaivi_vitri, sender as RadioButton);
        }

        private void opt_phan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_phan_mota, sender as RadioButton);
        }

        private void opt_sothayu_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton obj = sender as RadioButton;
            nmr_sothayu_khoangcach.Enabled = obj.Checked;
        }
    }
}
