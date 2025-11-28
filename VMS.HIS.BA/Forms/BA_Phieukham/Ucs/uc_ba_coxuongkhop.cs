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
    public partial class uc_ba_coxuongkhop : UserControl
    {
        public uc_ba_coxuongkhop()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_coxuongkhop_batthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBatthuong);
                opt_coxuongkhop_binhthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBinhthuong);
                txt_coxuongkhop_mota.Text = Utility.sDbnull(objPK.CoxuongkhopMota);

                opt_dauco_co.Checked = Utility.Bool2Bool(objPK.DaucoCo);
                opt_dauco_khong.Checked = Utility.Bool2Bool(objPK.DaucoKhong);
                txt_dauco_vitri.Text = Utility.sDbnull(objPK.DaucoVitri);

                opt_daukhop_co.Checked = Utility.Bool2Bool(objPK.DaukhopCo);
                opt_daukhop_khong.Checked = Utility.Bool2Bool(objPK.DaukhopKhong);
                txt_daukhop_vitri.Text = Utility.sDbnull(objPK.DaukhopVitri);

                opt_sungdo_khop_co.Checked = Utility.Bool2Bool(objPK.SungdoKhopCo);
                opt_sungdo_khop_khong.Checked = Utility.Bool2Bool(objPK.SungdoKhopKhong);
                txt_sungdo_khop_vitri.Text = Utility.sDbnull(objPK.SungdoKhopVitri);

                opt_daucotsong_co.Checked = Utility.Bool2Bool(objPK.DaucotsongCo);
                opt_daucotsong_khong.Checked = Utility.Bool2Bool(objPK.DaucotsongKhong);
                txt_daucotsong_vitri.Text = Utility.sDbnull(objPK.DaucotsongVitri);

                opt_hanchevandongkhop_co.Checked = Utility.Bool2Bool(objPK.HanchevandongkhopCo);
                opt_hanchevandongkhop_khong.Checked = Utility.Bool2Bool(objPK.HanchevandongkhopKhong);
                txt_hanchevandongkhop_vitri.Text = Utility.sDbnull(objPK.HanchevandongkhopVitri);


                opt_teoco_khong.Checked = Utility.Bool2Bool(objPK.TeocoKhong);
                opt_teoco_co.Checked = Utility.Bool2Bool(objPK.TeocoCo);
                txt_teoco_vitri.Text = Utility.sDbnull(objPK.TeocoVitri);

                opt_hat_tophi_co.Checked = Utility.Bool2Bool(objPK.HatTophiCo);
                opt_hat_tophi_khong.Checked = Utility.Bool2Bool(objPK.HatTophiKhong);
                txt_hat_tophi_vitri.Text = Utility.sDbnull(objPK.HatTophiVitri);

                opt_daucungkhopbuoisang_khong.Checked = Utility.Bool2Bool(objPK.DaucungkhopbuoisangKhong);
                opt_daucungkhopbuoisang_co.Checked = Utility.Bool2Bool(objPK.DaucungkhopbuoissangCo);
                txt_daucungkhopbuoissang_mota.Text = Utility.sDbnull(objPK.DaucungkhopbuoissangMota);

                txt_coxuongkhop_khac.Text = Utility.sDbnull(objPK.CoxuongkhopKhac);
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
                objPK.CoxuongkhopBatthuong = opt_coxuongkhop_batthuong.Checked;
                objPK.CoxuongkhopBinhthuong = opt_coxuongkhop_binhthuong.Checked;
                objPK.CoxuongkhopMota = opt_coxuongkhop_batthuong.Checked?Utility.sDbnull(txt_coxuongkhop_mota.Text):"";

                objPK.DaucoCo = opt_dauco_co.Checked;
                objPK.DaucoKhong = opt_dauco_khong.Checked;
                objPK.DaucoVitri = opt_dauco_co.Checked ? Utility.sDbnull(txt_dauco_vitri.Text) : "";

                objPK.DaukhopCo = opt_daukhop_co.Checked;
                objPK.DaukhopKhong = opt_daukhop_khong.Checked;
                objPK.DaukhopVitri = opt_dauco_co.Checked ? Utility.sDbnull(txt_daukhop_vitri.Text) : "";

                objPK.SungdoKhopCo = opt_sungdo_khop_co.Checked;
                objPK.SungdoKhopKhong = opt_sungdo_khop_khong.Checked;
                objPK.SungdoKhopVitri = opt_sungdo_khop_co.Checked? Utility.sDbnull(txt_sungdo_khop_vitri.Text):"";

                objPK.DaucotsongCo = opt_daucotsong_co.Checked;
                objPK.DaucotsongKhong = opt_daucotsong_khong.Checked;
                objPK.DaucotsongVitri = opt_daucotsong_co.Checked?Utility.sDbnull(txt_daucotsong_vitri.Text):"";

                objPK.HanchevandongkhopCo = opt_hanchevandongkhop_co.Checked;
                objPK.HanchevandongkhopKhong = opt_hanchevandongkhop_khong.Checked;
                objPK.HanchevandongkhopVitri = opt_hanchevandongkhop_co.Checked? Utility.sDbnull(txt_hanchevandongkhop_vitri.Text):"";

                objPK.TeocoKhong = opt_teoco_khong.Checked;
                objPK.TeocoCo = opt_teoco_co.Checked;
                objPK.TeocoVitri = opt_teoco_co.Checked? Utility.sDbnull(txt_teoco_vitri.Text):"";

                objPK.HatTophiCo = opt_hat_tophi_co.Checked;
                objPK.HatTophiKhong = opt_hat_tophi_khong.Checked;
                objPK.HatTophiVitri = opt_hat_tophi_co.Checked? Utility.sDbnull(txt_hat_tophi_vitri.Text):"";

                objPK.DaucungkhopbuoisangKhong = opt_daucungkhopbuoisang_khong.Checked;
                objPK.DaucungkhopbuoissangCo = opt_daucungkhopbuoisang_co.Checked;
                objPK.DaucungkhopbuoissangMota = opt_daucungkhopbuoisang_co.Checked? Utility.sDbnull(txt_daucungkhopbuoissang_mota.Text):"";

                objPK.CoxuongkhopKhac = Utility.sDbnull(txt_coxuongkhop_khac.Text);



            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_coxuongkhop_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_coxuongkhop_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_coxuongkhop_mota.Focus();
        }

        private void opt_dauco_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_dauco_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_dauco_vitri.Focus();
        }

        private void opt_sungdo_khop_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_sungdo_khop_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_sungdo_khop_vitri.Focus();
        }

        private void opt_hanchevandongkhop_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_hanchevandongkhop_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_hanchevandongkhop_vitri.Focus();
        }

        private void opt_hat_tophi_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_hat_tophi_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_hat_tophi_vitri.Focus();
        }

        private void opt_daucungkhopbuoisang_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_daucungkhopbuoissang_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_daucungkhopbuoissang_mota.Focus();
        }

        private void opt_daukhop_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_daukhop_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_daukhop_vitri.Focus();
        }

        private void opt_daucotsong_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_daucotsong_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_daucotsong_vitri.Focus();
        }

        private void opt_teoco_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_teoco_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_teoco_vitri.Focus();
        }
    }
}
