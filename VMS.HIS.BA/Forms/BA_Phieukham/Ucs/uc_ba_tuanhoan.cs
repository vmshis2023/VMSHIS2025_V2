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
    public partial class uc_ba_tuanhoan : UserControl
    {
        public uc_ba_tuanhoan()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_tuanhoan_binhthuong.Checked = Utility.Bool2Bool(objPK.TuanhoanBinhthuong);
                opt_tuanhoan_batthuong.Checked = Utility.Bool2Bool(objPK.TuanhoanBatthuong);
                txt_tuanhoan_mota.Text = Utility.sDbnull(objPK.TuanhoanMota);
                opt_daunguc_khong.Checked = Utility.Bool2Bool(objPK.DaungucKhong);
                opt_daunguc_co.Checked = Utility.Bool2Bool(objPK.DaungucCo);
                opt_daunguc_dienhinh.Checked = Utility.Bool2Bool(objPK.DaungucDienhinh);
                opt_daunguc_khongdienhinh.Checked = Utility.Bool2Bool(objPK.DaungucKhongdienhinh);

                opt_hoihop_co.Checked = Utility.Bool2Bool(objPK.HoihopCo);
                opt_hoihop_khong.Checked = Utility.Bool2Bool(objPK.HoihopKhong);

                opt_nhipnhanh_co.Checked = Utility.Bool2Bool(objPK.NhipnhanhCo);
                opt_nhipnhanh_khong.Checked = Utility.Bool2Bool(objPK.NhipnhanhKhong);

                opt_nhipcham_co.Checked = Utility.Bool2Bool(objPK.NhipchamCo);
                opt_nhipcham_khong.Checked = Utility.Bool2Bool(objPK.NhipchamKhong);

                opt_loannhip_co.Checked = Utility.Bool2Bool(objPK.LoannhipCo);
                opt_loannhip_khong.Checked = Utility.Bool2Bool(objPK.LoannhipKhong);
              

                opt_daplech_co.Checked = Utility.Bool2Bool(objPK.DaplechCo);
                opt_daplech_khong.Checked = Utility.Bool2Bool(objPK.DaplechKhong);

                opt_diendap_rong_co.Checked = Utility.Bool2Bool(objPK.DiendapRongCo);
                opt_diendap_rong_khong.Checked = Utility.Bool2Bool(objPK.DiendapRongKhong);

                opt_timmo_co.Checked = Utility.Bool2Bool(objPK.TimmoCo);
                opt_timmo_khong.Checked = Utility.Bool2Bool(objPK.TimmoKhong);

                chk_t1.Checked = Utility.Bool2Bool(objPK.T1);
                chk_t2.Checked = Utility.Bool2Bool(objPK.T2);
               

                opt_thoi_tamthu_co.Checked = Utility.Bool2Bool(objPK.ThoiTamthuCo);
                opt_thoi_tamthu_khong.Checked = Utility.Bool2Bool(objPK.ThoiTamthuKhong);
                txt_thoi_tamthu_vitri.Text= Utility.sDbnull(objPK.ThoiTamthuVitri);
                txt_thoi_tamthu_mucdo.Text = Utility.sDbnull(objPK.ThoiTamthuMucdo);
                chk_thoi_tamthu_rungmiu.Checked = Utility.Bool2Bool(objPK.ThoiTamthuRungmiu);

                opt_rung_tamtruong_co.Checked = Utility.Bool2Bool(objPK.RungTamtruongCo);
                opt_rung_tamtruong_khong.Checked = Utility.Bool2Bool(objPK.RungTamtruongKhong);
                txt_rung_tamtruong_vitri.Text = Utility.sDbnull(objPK.RungTamtruongVitri);
                txt_rung_tamtruong_mucdo.Text = Utility.sDbnull(objPK.RungTamtruongMucdo);
                

                opt_thoi_tamtruong_co.Checked = Utility.Bool2Bool(objPK.ThoiTamtruongCo);
                opt_thoi_tamtruong_khong.Checked = Utility.Bool2Bool(objPK.ThoiTamtruongKhong);
                txt_thoi_tamtruong_vitri.Text = Utility.sDbnull(objPK.ThoiTamtruongVitri);
                txt_thoi_tamtruong_mucdo.Text = Utility.sDbnull(objPK.ThoiTamtruongMucdo);
                chk_thoi_tamtruong_rungmiu.Checked = Utility.Bool2Bool(objPK.ThoiTamtruongRungmiu);

                opt_thoi_lientuc_co.Checked = Utility.Bool2Bool(objPK.ThoiLientucCo);
                opt_thoi_lientuc_khong.Checked = Utility.Bool2Bool(objPK.ThoiLientucKhong);
                txt_thoi_lientuc_vitri.Text = Utility.sDbnull(objPK.ThoiLientucVitri);

                opt_tiengthoi_dongmach_co.Checked = Utility.Bool2Bool(objPK.TiengthoiDongmachCo);
                opt_tiengthoi_dongmach_khong.Checked = Utility.Bool2Bool(objPK.TiengthoiDongmachKhong);
                txt_tiengthoi_dongmach_vitri.Text = Utility.sDbnull(objPK.TiengthoiDongmachVitri);

               
                txt_tuanhoan_khac.Text = Utility.sDbnull(objPK.TuanhoanKhac);

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
                objPK.TuanhoanBinhthuong = opt_tuanhoan_binhthuong.Checked;
                objPK.TuanhoanBatthuong = opt_tuanhoan_batthuong.Checked;
                objPK.TuanhoanMota = opt_tuanhoan_batthuong.Checked?Utility.sDbnull(txt_tuanhoan_mota.Text):"";

                objPK.DaungucKhong = opt_daunguc_khong.Checked;
                objPK.DaungucCo = opt_daunguc_co.Checked;
                objPK.DaungucDienhinh = opt_daunguc_dienhinh.Checked;
                objPK.DaungucKhongdienhinh = opt_daunguc_khongdienhinh.Checked;

                objPK.HoihopCo = opt_hoihop_co.Checked;
                objPK.HoihopKhong = opt_hoihop_khong.Checked;

                objPK.NhipnhanhCo = opt_nhipnhanh_co.Checked;
                objPK.NhipnhanhKhong = opt_nhipnhanh_khong.Checked;

                objPK.NhipchamCo = opt_nhipcham_co.Checked;
                objPK.NhipchamKhong = opt_nhipcham_khong.Checked;

                objPK.LoannhipCo = opt_loannhip_co.Checked;
                objPK.LoannhipKhong = opt_loannhip_khong.Checked;

                objPK.DaplechCo = opt_daplech_co.Checked;
                objPK.DaplechKhong = opt_daplech_khong.Checked;

                objPK.DiendapRongCo = opt_diendap_rong_co.Checked;
                objPK.DiendapRongKhong = opt_diendap_rong_khong.Checked;

                objPK.TimmoCo = opt_timmo_co.Checked;
                objPK.TimmoKhong = opt_timmo_khong.Checked;

                objPK.T1 = chk_t1.Checked;
                objPK.T2 = chk_t2.Checked;

                objPK.ThoiTamthuCo = opt_thoi_tamthu_co.Checked;
                objPK.ThoiTamthuKhong = opt_thoi_tamthu_khong.Checked;
                objPK.ThoiTamthuVitri = opt_thoi_tamthu_co.Checked? Utility.sDbnull(txt_thoi_tamthu_vitri.Text):"";
                objPK.ThoiTamthuMucdo = opt_thoi_tamthu_co.Checked? Utility.sDbnull(txt_thoi_tamthu_mucdo.Text):"";
                objPK.ThoiTamthuRungmiu = opt_thoi_tamthu_co.Checked? chk_thoi_tamthu_rungmiu.Checked:false;

                objPK.RungTamtruongCo = opt_rung_tamtruong_co.Checked;
                objPK.RungTamtruongKhong = opt_rung_tamtruong_khong.Checked;
                objPK.RungTamtruongVitri = opt_rung_tamtruong_co.Checked? Utility.sDbnull(txt_rung_tamtruong_vitri.Text):"";
                objPK.RungTamtruongMucdo = opt_rung_tamtruong_co.Checked? Utility.sDbnull(txt_rung_tamtruong_mucdo.Text):"";

                objPK.ThoiTamtruongCo = opt_thoi_tamtruong_co.Checked;
                objPK.ThoiTamtruongKhong = opt_thoi_tamtruong_khong.Checked;
                objPK.ThoiTamtruongVitri = opt_thoi_tamtruong_co.Checked? Utility.sDbnull(txt_thoi_tamtruong_vitri.Text):"";
                objPK.ThoiTamtruongMucdo = opt_thoi_tamtruong_co.Checked? Utility.sDbnull(txt_thoi_tamtruong_mucdo.Text):"";
                objPK.ThoiTamtruongRungmiu = opt_thoi_tamtruong_co.Checked? chk_thoi_tamtruong_rungmiu.Checked:false;

                objPK.ThoiLientucCo = opt_thoi_lientuc_co.Checked;
                objPK.ThoiLientucKhong = opt_thoi_lientuc_khong.Checked;
                objPK.ThoiLientucVitri = opt_thoi_lientuc_co.Checked? Utility.sDbnull(txt_thoi_lientuc_vitri.Text):"";

                objPK.TiengthoiDongmachCo = opt_tiengthoi_dongmach_co.Checked;
                objPK.TiengthoiDongmachKhong = opt_tiengthoi_dongmach_khong.Checked;
                objPK.TiengthoiDongmachVitri = opt_tiengthoi_dongmach_co.Checked? Utility.sDbnull(txt_tiengthoi_dongmach_vitri.Text):"";

                objPK.TuanhoanKhac = Utility.sDbnull(txt_tuanhoan_khac.Text);


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_tuanhoan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tuanhoan_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tuanhoan_mota.Focus();
        }

        private void opt_timmo_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            chk_t1.Enabled = chk_t2.Enabled= _obj.Checked;
        }

        private void opt_thoi_tamthu_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thoi_tamthu_vitri.Enabled = txt_thoi_tamthu_mucdo.Enabled= chk_thoi_tamthu_rungmiu.Enabled= _obj.Checked;
            if (_obj.Checked) txt_thoi_tamthu_vitri.Focus();
        }

        private void opt_thoi_tamtruong_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thoi_tamtruong_vitri.Enabled = txt_thoi_tamtruong_mucdo.Enabled= chk_thoi_tamtruong_rungmiu.Enabled= _obj.Checked;
            if (_obj.Checked) txt_thoi_tamtruong_vitri.Focus();
        }

        private void opt_rung_tamtruong_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_rung_tamtruong_vitri.Enabled = txt_rung_tamtruong_mucdo.Enabled= _obj.Checked;
            if (_obj.Checked) txt_rung_tamtruong_vitri.Focus();
        }

        private void opt_thoi_lientuc_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thoi_lientuc_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_thoi_lientuc_vitri.Focus();
        }

        private void opt_tiengthoi_dongmach_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tiengthoi_dongmach_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tiengthoi_dongmach_vitri.Focus();
        }
    }
}
