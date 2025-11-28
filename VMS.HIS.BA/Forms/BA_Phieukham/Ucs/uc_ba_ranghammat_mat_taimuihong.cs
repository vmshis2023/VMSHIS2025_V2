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
    public partial class uc_ba_ranghammat_mat_taimuihong : UserControl
    {
        public uc_ba_ranghammat_mat_taimuihong()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_ranghammat_batthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBatthuong);
                opt_ranghammat_binhthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBinhthuong);
                txt_ranghammat_ghiro.Text = Utility.sDbnull(objPK.HammatMota);

                txt_rhm_sungne_bamtim.Text = Utility.sDbnull(objPK.RhmSungneBamtim);
                txt_rhm_biendang.Text = Utility.sDbnull(objPK.RhmBiendang);
                txt_rhm_diemdauchoi.Text = Utility.sDbnull(objPK.RhmDiemdauchoi);
                txt_rhm_laoxaoxuong.Text = Utility.sDbnull(objPK.RhmLaoxaoxuong);
                txt_rhm_vandong.Text = Utility.sDbnull(objPK.RhmVandong);
                txt_rhm_hamieng.Text = Utility.sDbnull(objPK.RhmHamieng);
                txt_rhm_khopcan.Text = Utility.sDbnull(objPK.RhmKhopcan);
                txt_rang_mota.Text = Utility.sDbnull(objPK.RangMota);

                opt_mat_binhthuong.Checked = Utility.Bool2Bool(objPK.MatBinhthuong);
                opt_mat_batthuong.Checked = Utility.Bool2Bool(objPK.MatBatthuong);
                txt_mat_ghiro.Text = Utility.sDbnull(objPK.MatGhiro);
               
                opt_nhancau_tonthuong_co.Checked = Utility.Bool2Bool(objPK.NhancauTonthuongCo);
                opt_nhancau_tonthuong_khong.Checked = Utility.Bool2Bool(objPK.NhancauTonthuongKhong);
                txt_nhancau_tonthuong_mota.Text = Utility.sDbnull(objPK.NhancauTonthuongMota);

                opt_nhancau_supmi_co.Checked = Utility.Bool2Bool(objPK.NhancauSupmiCo);
                opt_nhancau_supmi_khong.Checked = Utility.Bool2Bool(objPK.NhancauSupmiKhong);
                txt_nhancau_supmi_mota.Text = Utility.sDbnull(objPK.NhancauSupmiMota);

                opt_nhancau_tonthuongledao_co.Checked = Utility.Bool2Bool(objPK.NhancauTonthuongledaoCo);
                opt_nhancau_tonthuongledao_khong.Checked = Utility.Bool2Bool(objPK.NhancauTonthuongledaoKhong);
                txt_nhancau_tonthuongledao_mota.Text = Utility.sDbnull(objPK.NhancauTonthuongledaoMota);

                opt_tmh_batthuong.Checked = Utility.Bool2Bool(objPK.TmhBatthuong);
                opt_tmh_binhthuong.Checked = Utility.Bool2Bool(objPK.TmhBinhthuong);
                txt_tmh_ghiro.Text = Utility.sDbnull(objPK.TmhGhiro);

                opt_mui_vetthuong_khong.Checked = Utility.Bool2Bool(objPK.MuiVetthuongKhong);
                opt_mui_vetthuong_co.Checked = Utility.Bool2Bool(objPK.MuiVetthuongCo);
                txt_mui_vetthuong_mota.Text = Utility.sDbnull(objPK.MuiVetthuongMota);

                opt_tai_vetthuong_co.Checked = Utility.Bool2Bool(objPK.TaiVetthuongCo);
                opt_tai_vetthuong_khong.Checked = Utility.Bool2Bool(objPK.TaiVetthuongKhong);
                txt_tai_vetthuong_mota.Text = Utility.sDbnull(objPK.TaiVetthuongMota);

                opt_tai_chaymau_khong.Checked = Utility.Bool2Bool(objPK.TaiChaymauKhong);
                opt_tai_chaymau_co.Checked = Utility.Bool2Bool(objPK.TaiChaymauCo);
                txt_tai_chaymau_mota.Text = Utility.sDbnull(objPK.TaiChaymauMota);

                txt_taimuihong_khac.Text = Utility.sDbnull(objPK.TaimuihongKhac);
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
                objPK.RanghammatBatthuong = opt_ranghammat_batthuong.Checked;
                objPK.RanghammatBinhthuong = opt_ranghammat_binhthuong.Checked;
                objPK.HammatMota = opt_ranghammat_batthuong.Checked? Utility.sDbnull(txt_ranghammat_ghiro.Text):"";

                objPK.RhmSungneBamtim = Utility.sDbnull(txt_rhm_sungne_bamtim.Text);
                objPK.RhmBiendang = Utility.sDbnull(txt_rhm_biendang.Text);
                objPK.RhmDiemdauchoi = Utility.sDbnull(txt_rhm_diemdauchoi.Text);
                objPK.RhmLaoxaoxuong = Utility.sDbnull(txt_rhm_laoxaoxuong.Text);
                objPK.RhmVandong = Utility.sDbnull(txt_rhm_vandong.Text);
                objPK.RhmHamieng = Utility.sDbnull(txt_rhm_hamieng.Text);
                objPK.RhmKhopcan = Utility.sDbnull(txt_rhm_khopcan.Text);
                objPK.RangMota = Utility.sDbnull(txt_rang_mota.Text);

                objPK.MatBinhthuong = opt_mat_binhthuong.Checked;
                objPK.MatBatthuong = opt_mat_batthuong.Checked;
                objPK.MatGhiro = opt_mat_batthuong.Checked? Utility.sDbnull(txt_mat_ghiro.Text):"";

                objPK.NhancauTonthuongCo = opt_nhancau_tonthuong_co.Checked;
                objPK.NhancauTonthuongKhong = opt_nhancau_tonthuong_khong.Checked;
                objPK.NhancauTonthuongMota = opt_nhancau_tonthuong_co.Checked? Utility.sDbnull(txt_nhancau_tonthuong_mota.Text) : "";

                objPK.NhancauSupmiCo = opt_nhancau_supmi_co.Checked;
                objPK.NhancauSupmiKhong = opt_nhancau_supmi_khong.Checked;
                objPK.NhancauSupmiMota = opt_nhancau_supmi_co.Checked? Utility.sDbnull(txt_nhancau_supmi_mota.Text) : "";

                objPK.NhancauTonthuongledaoCo = opt_nhancau_tonthuongledao_co.Checked;
                objPK.NhancauTonthuongledaoKhong = opt_nhancau_tonthuongledao_khong.Checked;
                objPK.NhancauTonthuongledaoMota = opt_nhancau_tonthuongledao_co.Checked? Utility.sDbnull(txt_nhancau_tonthuongledao_mota.Text):"";

                objPK.TmhBatthuong = opt_tmh_batthuong.Checked;
                objPK.TmhBinhthuong = opt_tmh_binhthuong.Checked;
                objPK.TmhGhiro = opt_tmh_batthuong.Checked? Utility.sDbnull(txt_tmh_ghiro.Text) : "";

                objPK.MuiVetthuongKhong = opt_mui_vetthuong_khong.Checked;
                objPK.MuiVetthuongCo = opt_mui_vetthuong_co.Checked;
                objPK.MuiVetthuongMota = opt_mui_vetthuong_co.Checked? Utility.sDbnull(txt_mui_vetthuong_mota.Text):"";

                objPK.TaiVetthuongCo = opt_tai_vetthuong_co.Checked;
                objPK.TaiVetthuongKhong = opt_tai_vetthuong_khong.Checked;
                objPK.TaiVetthuongMota = opt_tai_vetthuong_co.Checked? Utility.sDbnull(txt_tai_vetthuong_mota.Text):"";

                objPK.TaiChaymauKhong = opt_tai_chaymau_khong.Checked;
                objPK.TaiChaymauCo = opt_tai_chaymau_co.Checked;
                objPK.TaiChaymauMota = opt_tai_chaymau_co.Checked? Utility.sDbnull(txt_tai_chaymau_mota.Text):"";

                objPK.TaimuihongKhac = Utility.sDbnull(txt_taimuihong_khac.Text);




            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_ranghammat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
          Utility.EnableAndFocus(txt_ranghammat_ghiro, sender as RadioButton);
          
        }
        
        private void opt_mat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_mat_ghiro, sender as RadioButton);
        }

        private void opt_nhancau_tonthuong_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_nhancau_tonthuong_mota, sender as RadioButton);
        }

        private void opt_nhancau_supmi_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_nhancau_supmi_mota, sender as RadioButton);
        }

        private void opt_nhancau_tonthuongledao_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_nhancau_tonthuongledao_mota, sender as RadioButton);
        }

        private void opt_tmh_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tmh_ghiro, sender as RadioButton);
        }

        private void opt_mui_vetthuong_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_mui_vetthuong_mota, sender as RadioButton);
        }

        private void opt_tai_vetthuong_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tai_vetthuong_mota, sender as RadioButton);
        }

        private void opt_tai_chaymau_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tai_chaymau_mota, sender as RadioButton);
        }
    }
}
