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
    public partial class uc_ba_tuanhoan_hohap_ngoaikhoa : UserControl
    {
        public uc_ba_tuanhoan_hohap_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_tuanhoan_binhthuong.Checked = Utility.Bool2Bool(objPK.TuanhoanBinhthuong);
                opt_tuanhoan_batthuong.Checked = Utility.Bool2Bool(objPK.TuanhoanBatthuong);
                txt_tuanhoan_mota.Text = Utility.sDbnull(objPK.TuanhoanMota);
                
                opt_hinhdanglongnguc_candoi_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucCandoiKhong);
                opt_hinhdanglongnguc_candoi_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucCandoiCo);
               
                opt_hinhdanglongnguc_corut_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucCorutCo);
                opt_hinhdanglongnguc_corut_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucCorutKhong);

                opt_hinhdanglongnguc_bamtim_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucBamtimCo);
                opt_hinhdanglongnguc_bamtim_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucBamtimKhong);

                opt_rungmiu_co.Checked = Utility.Bool2Bool(objPK.RungmiuCo);
                opt_rungmiu_khong.Checked = Utility.Bool2Bool(objPK.RungmiuKhong);

                opt_tiengtim_ro.Checked = Utility.Bool2Bool(objPK.TiengtimRo);
                opt_tiengtim_mo.Checked = Utility.Bool2Bool(objPK.TiengtimMo);

                opt_tiengtimdeu_co.Checked = Utility.Bool2Bool(objPK.TiengtimdeuCo);
                opt_tiengtimdeu_khong.Checked = Utility.Bool2Bool(objPK.TiengtimdeuKhong);
              

                opt_hinhdanglongnguc__hohapdaochieu_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucHohapdaochieuCo);
                opt_hinhdanglongnguc_hohapdaochieu_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucHohapdaochieuKhong);

                opt_tiengtimbatthuong_co.Checked = Utility.Bool2Bool(objPK.TiengtimbatthuongCo);
                opt_tiengtimbatthuong_khong.Checked = Utility.Bool2Bool(objPK.TiengtimbatthuongKhong);

                opt_hinhdanglongnguc_mangsuongdidong_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucMangsuongdidongCo);
                opt_hinhdanglongnguc_mangsuongdidong_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucMangsuongdidongKhong);

                opt_rirao_phenang_co.Checked = Utility.Bool2Bool(objPK.RiraoPhenangCo);
                opt_rirao_phenang_khong.Checked = Utility.Bool2Bool(objPK.RiraoPhenangKhong);
                opt_rirao_phenang_mota.Text = Utility.sDbnull(objPK.RiraoPhenangMota);


                opt_hinhdanglongnguc_thogangsuc_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucThogangsucCo);
                opt_hinhdanglongnguc_thogangsuc_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucThogangsucKhong);
               

                opt_rale_co.Checked = Utility.Bool2Bool(objPK.RaleCo);
                opt_rale_khong.Checked = Utility.Bool2Bool(objPK.RaleKhong);
                txt_rale_co_mota.Text = Utility.sDbnull(objPK.RaleCoMota);
                

                opt_hinhdanglongnguc_vetthuongthanhnguc_co.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucVetthuongthanhngucCo);
                opt_hinhdanglongnguc_vetthuongthanhnguc_khong.Checked = Utility.Bool2Bool(objPK.HinhdanglongngucVetthuongthanhngucKhong);
                txt_hinhdanglongnguc_vetthuongthanhnguc_mota.Text = Utility.sDbnull(objPK.HinhdanglongngucVetthuongthanhngucMota);
               
                txt_tuanhoan_khac.Text = Utility.sDbnull(objPK.TuanhoanMota);

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
                objPK.TuanhoanBinhthuong = opt_tuanhoan_binhthuong.Checked;
                objPK.TuanhoanBatthuong = opt_tuanhoan_batthuong.Checked;
                //objPK.TuanhoanMota = opt_tuanhoan_batthuong.Checked? Utility.sDbnull(txt_tuanhoan_mota.Text):"";

                objPK.HinhdanglongngucCandoiKhong = opt_hinhdanglongnguc_candoi_khong.Checked;
                objPK.HinhdanglongngucCandoiCo = opt_hinhdanglongnguc_candoi_co.Checked;

                objPK.HinhdanglongngucCorutCo = opt_hinhdanglongnguc_corut_co.Checked;
                objPK.HinhdanglongngucCorutKhong = opt_hinhdanglongnguc_corut_khong.Checked;

                objPK.HinhdanglongngucBamtimCo = opt_hinhdanglongnguc_bamtim_co.Checked;
                objPK.HinhdanglongngucBamtimKhong = opt_hinhdanglongnguc_bamtim_khong.Checked;

                objPK.RungmiuCo = opt_rungmiu_co.Checked;
                objPK.RungmiuKhong = opt_rungmiu_khong.Checked;

                objPK.TiengtimRo = opt_tiengtim_ro.Checked;
                objPK.TiengtimMo = opt_tiengtim_mo.Checked;

                objPK.TiengtimdeuCo = opt_tiengtimdeu_co.Checked;
                objPK.TiengtimdeuKhong = opt_tiengtimdeu_khong.Checked;

                objPK.HinhdanglongngucHohapdaochieuCo = opt_hinhdanglongnguc__hohapdaochieu_co.Checked;
                objPK.HinhdanglongngucHohapdaochieuKhong = opt_hinhdanglongnguc_hohapdaochieu_khong.Checked;

                objPK.TiengtimbatthuongCo = opt_tiengtimbatthuong_co.Checked;
                objPK.TiengtimbatthuongKhong = opt_tiengtimbatthuong_khong.Checked;

                objPK.HinhdanglongngucMangsuongdidongCo = opt_hinhdanglongnguc_mangsuongdidong_co.Checked;
                objPK.HinhdanglongngucMangsuongdidongKhong = opt_hinhdanglongnguc_mangsuongdidong_khong.Checked;

                objPK.RiraoPhenangCo = opt_rirao_phenang_co.Checked;
                objPK.RiraoPhenangKhong = opt_rirao_phenang_khong.Checked;
                objPK.RiraoPhenangMota = opt_rirao_phenang_co.Checked ? Utility.sDbnull(opt_rirao_phenang_mota.Text) : "";

                objPK.HinhdanglongngucThogangsucCo = opt_hinhdanglongnguc_thogangsuc_co.Checked;
                objPK.HinhdanglongngucThogangsucKhong = opt_hinhdanglongnguc_thogangsuc_khong.Checked;

                objPK.RaleCo = opt_rale_co.Checked;
                objPK.RaleKhong = opt_rale_khong.Checked;
                objPK.RaleCoMota = opt_rale_co.Checked ? Utility.sDbnull(txt_rale_co_mota.Text) : "";

                objPK.HinhdanglongngucVetthuongthanhngucCo = opt_hinhdanglongnguc_vetthuongthanhnguc_co.Checked;
                objPK.HinhdanglongngucVetthuongthanhngucKhong = opt_hinhdanglongnguc_vetthuongthanhnguc_khong.Checked;
                objPK.HinhdanglongngucVetthuongthanhngucMota = opt_hinhdanglongnguc_vetthuongthanhnguc_co.Checked ? Utility.sDbnull(txt_hinhdanglongnguc_vetthuongthanhnguc_mota.Text) : "";

                objPK.TuanhoanMota = Utility.sDbnull(txt_tuanhoan_khac.Text);



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

        private void opt_rirao_phenang_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(opt_rirao_phenang_mota, sender as RadioButton);
        }

        private void opt_rale_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_rale_co_mota, sender as RadioButton);
        }

        private void opt_hinhdanglongnguc_vetthuongthanhnguc_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_hinhdanglongnguc_vetthuongthanhnguc_mota, sender as RadioButton);
        }
    }
}
