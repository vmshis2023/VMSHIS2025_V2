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
    public partial class uc_ba_thankinh : UserControl
    {
        public uc_ba_thankinh()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_thankinh_binhthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBinhthuong);
                opt_thankinh_batthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBatthuong);
                txt_thankinh_mota.Text = Utility.sDbnull(objPK.ThankinhMota);

                opt_cocung_khong.Checked = Utility.Bool2Bool(objPK.CocungKhong);
                opt_cocung_co.Checked = Utility.Bool2Bool(objPK.CocungCo);

                opt_dauhieu_mangnao_co.Checked = Utility.Bool2Bool(objPK.DauhieuMangnaoCo);
                opt_dauhieu_mangnao_khong.Checked = Utility.Bool2Bool(objPK.DauhieuMangnaoKhong);

                opt_coluc_binhthuong.Checked = Utility.Bool2Bool(objPK.ColucBinhthuong);
                opt_coluc_giam.Checked = Utility.Bool2Bool(objPK.ColucGiam);
                txt_coluc_vitri.Text = Utility.sDbnull(objPK.ColucVitri);

                opt_truonglucco_binhthuong.Checked = Utility.Bool2Bool(objPK.TruongluccoBinhthuong);
                opt_truonglucco_tang.Checked = Utility.Bool2Bool(objPK.TruongluccoTang);
                opt_truonglucco_giam.Checked = Utility.Bool2Bool(objPK.TruongluccoGiam);
                txt_truonglucco_vitri.Text = Utility.sDbnull(objPK.TruongluccoVitri);

                opt_phanxaganxuong_binhthuong.Checked = Utility.Bool2Bool(objPK.PhanxaganxuongBinhthuong);
                opt_phanxaganxuong_giam.Checked = Utility.Bool2Bool(objPK.PhanxaganxuongGiam);
                opt_phanxaganxuong_tang.Checked = Utility.Bool2Bool(objPK.PhanxaganxuongTang);
                txt_phanxaganxuong_vitri.Text = Utility.sDbnull(objPK.PhanxaganxuongVitri);

                opt_liet_phai_co.Checked = Utility.Bool2Bool(objPK.LietPhaiCo);
                opt_liet_phai_khong.Checked = Utility.Bool2Bool(objPK.LietPhaiKhong);

                opt_liet_trai_co.Checked = Utility.Bool2Bool(objPK.LietTraiCo);
                opt_liet_trai_khong.Checked = Utility.Bool2Bool(objPK.LietTraiKhong);

                opt_liet_2_chiduoi_co.Checked = Utility.Bool2Bool(objPK.Liet2ChiduoiCo);
                opt_liet_2_chiduoi_khong.Checked = Utility.Bool2Bool(objPK.Liet2ChiduoiKhong);

                opt_liet_4_chi_co.Checked = Utility.Bool2Bool(objPK.Liet4ChiCo);
                opt_liet_4_chi_khong.Checked = Utility.Bool2Bool(objPK.Liet4ChiKhong);

                txt_thankinh_khac.Text = Utility.sDbnull(objPK.ThankinhKhac);
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
                objPK.ThankinhBinhthuong = opt_thankinh_binhthuong.Checked;
                objPK.ThankinhBatthuong = opt_thankinh_batthuong.Checked;
                objPK.ThankinhMota = opt_thankinh_batthuong.Checked? Utility.sDbnull(txt_thankinh_mota.Text):"";

                objPK.CocungKhong = opt_cocung_khong.Checked;
                objPK.CocungCo = opt_cocung_co.Checked;

                objPK.DauhieuMangnaoCo = opt_dauhieu_mangnao_co.Checked;
                objPK.DauhieuMangnaoKhong = opt_dauhieu_mangnao_khong.Checked;

                objPK.ColucBinhthuong = opt_coluc_binhthuong.Checked;
                objPK.ColucGiam = opt_coluc_giam.Checked;
                objPK.ColucVitri = opt_coluc_giam.Checked? Utility.sDbnull(txt_coluc_vitri.Text):"";

                objPK.TruongluccoBinhthuong = opt_truonglucco_binhthuong.Checked;
                objPK.TruongluccoTang = opt_truonglucco_tang.Checked;
                objPK.TruongluccoGiam = opt_truonglucco_giam.Checked;
                objPK.TruongluccoVitri = opt_truonglucco_giam.Checked? Utility.sDbnull(txt_truonglucco_vitri.Text):"";

                objPK.PhanxaganxuongBinhthuong = opt_phanxaganxuong_binhthuong.Checked;
                objPK.PhanxaganxuongGiam = opt_phanxaganxuong_giam.Checked;
                objPK.PhanxaganxuongTang = opt_phanxaganxuong_tang.Checked;
                objPK.PhanxaganxuongVitri = opt_phanxaganxuong_giam.Checked ? Utility.sDbnull(txt_phanxaganxuong_vitri.Text) : "";

                objPK.LietPhaiCo = opt_liet_phai_co.Checked;
                objPK.LietPhaiKhong = opt_liet_phai_khong.Checked;

                objPK.LietTraiCo = opt_liet_trai_co.Checked;
                objPK.LietTraiKhong = opt_liet_trai_khong.Checked;

                objPK.Liet2ChiduoiCo = opt_liet_2_chiduoi_co.Checked;
                objPK.Liet2ChiduoiKhong = opt_liet_2_chiduoi_khong.Checked;

                objPK.Liet4ChiCo = opt_liet_4_chi_co.Checked;
                objPK.Liet4ChiKhong = opt_liet_4_chi_khong.Checked;

                objPK.ThankinhKhac = Utility.sDbnull(txt_thankinh_khac.Text);


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_thankinh_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thankinh_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_thankinh_mota.Focus();
        }

        private void opt_coluc_giam_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_coluc_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_coluc_vitri.Focus();
        }

        private void opt_truonglucco_giam_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_truonglucco_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_truonglucco_vitri.Focus();
        }

        private void opt_phanxaganxuong_giam_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_phanxaganxuong_vitri.Enabled = _obj.Checked;
            if (_obj.Checked) txt_phanxaganxuong_vitri.Focus();
        }
    }
}
