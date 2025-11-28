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
    public partial class uc_ba_caccoquan_ngoaikhoa : UserControl
    {
        public uc_ba_caccoquan_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhambenh objPK)
        {
            try
            {
                opt_tuanhoan_binhthuong.Checked = Utility.Bool2Bool(objPK.TuanhoanBinhthuong);
                opt_tuanhoan_batthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBatthuong);
                txt_tuanhoan_mota.Text = Utility.sDbnull(objPK.Tuanhoan);

                opt_toanthan_batthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBatthuong);
                opt_toanthan_binhthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBinhthuong);
                txt_toanthan_mota.Text = Utility.sDbnull(objPK.ToanThan);

               
                opt_hohap_batthuong.Checked = Utility.Bool2Bool(objPK.HohapBatthuong);
                opt_hohap_binhthuong.Checked = Utility.Bool2Bool(objPK.HohapBinhthuong);
                txt_hohap_mota.Text = Utility.sDbnull(objPK.Hohap);

                opt_tieuhoa_batthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBatthuong);
                opt_tieuhoa_binhthuong.Checked = Utility.Bool2Bool(objPK.TieuhoaBinhthuong);
                txt_tieuhoa_mota.Text = Utility.sDbnull(objPK.Tieuhoa);

                opt_thantietnieu_batthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieusinhducBatthuong);
                opt_thantietnieu_binhthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieusinhducBinhthuong);
                txt_thantietnieu_mota.Text = Utility.sDbnull(objPK.Thantietnieusinhduc);

                opt_thankinh_batthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBatthuong);
                opt_thankinh_binhthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBinhthuong);
                txt_thankinh_mota.Text = Utility.sDbnull(objPK.Thankinh);

                opt_coxuongkhop_batthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBatthuong);
                opt_coxuongkhop_binhthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBinhthuong);
                txt_coxuongkhop_mota.Text = Utility.sDbnull(objPK.Coxuongkhop);

                opt_taimuihong_batthuong.Checked = Utility.Bool2Bool(objPK.TaimuihongBatthuong);
                opt_taimuihong_binhthuong.Checked = Utility.Bool2Bool(objPK.TaimuihongBinhthuong);
                txt_taimuihong_mota.Text = Utility.sDbnull(objPK.Taimuihong);

                opt_ranghammat_batthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBatthuong);
                opt_ranghammat_binhthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBinhthuong);
                txt_ranghammat_mota.Text = Utility.sDbnull(objPK.Ranghammat);

                opt_mat_batthuong.Checked = Utility.Bool2Bool(objPK.MatBatthuong);
                opt_mat_binhthuong.Checked = Utility.Bool2Bool(objPK.MatBinhthuong);
                txt_mat_mota.Text = Utility.sDbnull(objPK.Mat);
                txt_benhly_khac.Text = Utility.sDbnull(objPK.CacBenhlyKhac);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void SetData(EmrPhieukhambenh objPK)
        {
            try
            {
                objPK.TuanhoanBinhthuong = opt_tuanhoan_binhthuong.Checked;
                objPK.ToanthanBatthuong = opt_tuanhoan_batthuong.Checked;
                objPK.Tuanhoan = opt_tuanhoan_batthuong.Checked? Utility.sDbnull(txt_tuanhoan_mota.Text):"";

                objPK.ToanthanBatthuong = opt_toanthan_batthuong.Checked;
                objPK.ToanthanBinhthuong = opt_toanthan_binhthuong.Checked;
                objPK.ToanThan = opt_toanthan_batthuong.Checked ? Utility.sDbnull(txt_toanthan_mota.Text) : "";

                objPK.HohapBatthuong = opt_hohap_batthuong.Checked;
                objPK.HohapBinhthuong = opt_hohap_binhthuong.Checked;
                objPK.Hohap = opt_hohap_batthuong.Checked ? Utility.sDbnull(txt_hohap_mota.Text) : "";

                objPK.TieuhoaBatthuong = opt_tieuhoa_batthuong.Checked;
                objPK.TieuhoaBinhthuong = opt_tieuhoa_binhthuong.Checked;
                objPK.Tieuhoa = opt_tieuhoa_batthuong.Checked ? Utility.sDbnull(txt_tieuhoa_mota.Text) : "";

                objPK.ThantietnieusinhducBatthuong = opt_thantietnieu_batthuong.Checked;
                objPK.ThantietnieusinhducBinhthuong = opt_thantietnieu_binhthuong.Checked;
                objPK.Thantietnieusinhduc = opt_thantietnieu_batthuong.Checked ? Utility.sDbnull(txt_thantietnieu_mota.Text) : "";

                objPK.ThankinhBatthuong = opt_thankinh_batthuong.Checked;
                objPK.ThankinhBinhthuong = opt_thankinh_binhthuong.Checked;
                objPK.Thankinh = opt_thankinh_batthuong.Checked ? Utility.sDbnull(txt_thankinh_mota.Text) : "";

                objPK.CoxuongkhopBatthuong = opt_coxuongkhop_batthuong.Checked;
                objPK.CoxuongkhopBinhthuong = opt_coxuongkhop_binhthuong.Checked;
                objPK.Coxuongkhop = opt_coxuongkhop_batthuong.Checked ? Utility.sDbnull(txt_coxuongkhop_mota.Text) : "";

                objPK.TaimuihongBatthuong = opt_taimuihong_batthuong.Checked;
                objPK.TaimuihongBinhthuong = opt_taimuihong_binhthuong.Checked;
                objPK.Taimuihong = opt_taimuihong_batthuong.Checked ? Utility.sDbnull(txt_taimuihong_mota.Text) : "";

                objPK.RanghammatBatthuong = opt_ranghammat_batthuong.Checked;
                objPK.RanghammatBinhthuong = opt_ranghammat_binhthuong.Checked;
                objPK.Ranghammat = opt_ranghammat_batthuong.Checked ? Utility.sDbnull(txt_ranghammat_mota.Text) : "";

                objPK.MatBatthuong = opt_mat_batthuong.Checked;
                objPK.MatBinhthuong = opt_mat_binhthuong.Checked;
                objPK.Mat = opt_mat_batthuong.Checked ? Utility.sDbnull(txt_mat_mota.Text) : "";

                objPK.CacBenhlyKhac = Utility.sDbnull(txt_benhly_khac.Text);




            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

    

        private void opt_toanthan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_toanthan_mota, sender as RadioButton);
        }

        private void opt_tuanhoan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tuanhoan_mota, sender as RadioButton);
        }

        private void opt_hohap_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_hohap_mota, sender as RadioButton);
        }

        private void opt_tieuhoa_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tieuhoa_mota, sender as RadioButton);
        }

        private void opt_thantietnieu_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_thantietnieu_mota, sender as RadioButton);
        }

        private void opt_thankinh_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_thankinh_mota, sender as RadioButton);
        }

        private void opt_coxuongkhop_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_coxuongkhop_mota, sender as RadioButton);
        }

        private void opt_taimuihong_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_taimuihong_mota, sender as RadioButton);
        }

        private void opt_ranghammat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_ranghammat_mota, sender as RadioButton);
        }

        private void opt_mat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_mat_mota, sender as RadioButton);
        }
    }
}
