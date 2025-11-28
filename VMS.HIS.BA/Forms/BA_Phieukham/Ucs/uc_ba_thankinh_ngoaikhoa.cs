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
    public partial class uc_ba_thankinh_ngoaikhoa : UserControl
    {
        public uc_ba_thankinh_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_thankinh_binhthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBinhthuong);
                opt_thankinh_batthuong.Checked = Utility.Bool2Bool(objPK.ThankinhBatthuong);
                txt_thankinh_mota.Text = Utility.sDbnull(objPK.ThankinhMota);

                opt_vetthuongvungdau_khong.Checked = Utility.Bool2Bool(objPK.VetthuongvungdauKhong);
                opt_vetthuongvungdau_co.Checked = Utility.Bool2Bool(objPK.VetthuongvungdauCo);
                txt_vetthuongvungdau_mota.Text = Utility.sDbnull(objPK.VetthuongvungdauMota);

               
                opt_sungne_co.Checked = Utility.Bool2Bool(objPK.SungneCo);
                opt_sungne_khong.Checked = Utility.Bool2Bool(objPK.SungneKhong);
                txt_sungne_mota.Text = Utility.sDbnull(objPK.SungneMota);

                opt_biendanghammat_co.Checked = Utility.Bool2Bool(objPK.BiendanghammatCo);
                opt_dauhieuthankinhkhutru_khong.Checked = Utility.Bool2Bool(objPK.DauhieuthankinhkhutruKhong);
                txt_dauhieuthankinhkhutru_mota.Text = Utility.sDbnull(objPK.DauhieuthankinhkhutruMota);

                opt_dauhieuthankinhthucvat_co.Checked = Utility.Bool2Bool(objPK.DauhieuthankinhthucvatCo);
                opt_dauhieuthankinhthucvat_khong.Checked = Utility.Bool2Bool(objPK.DauhieuthankinhthucvatKhong);
                txt_dauhieuthankinhthucvat_mota.Text = Utility.sDbnull(objPK.DauhieuthankinhthucvatMota);

                opt_dauhieuthankinhngoaibien_co.Checked = Utility.Bool2Bool(objPK.DauhieuthankinhngoaibienCo);
                opt_dauhieuthankinhngoaibien_khong.Checked = Utility.Bool2Bool(objPK.DauhieuthankinhngoaibienKhong);
                txt_dauhieuthankinhngoaibien_mota.Text = Utility.sDbnull(objPK.DauhieuthankinhngoaibienMota);

                opt_dauhieulietthankinhso_co.Checked = Utility.Bool2Bool(objPK.DauhieulietthankinhsoCo);
                opt_dauhieulietthankinhso_khong.Checked = Utility.Bool2Bool(objPK.DauhieulietthankinhsoKhong);
                txt_dauhieulietthankinhso_mota.Text = Utility.sDbnull(objPK.DauhieulietthankinhsoMota);

                opt_dauhieulietvandong_co.Checked = Utility.Bool2Bool(objPK.DauhieulietvandongCo);
                opt_dauhieulietvandong_khong.Checked = Utility.Bool2Bool(objPK.DauhieulietvandongKhong);
                txt_dauhieulietvandong_mota.Text = Utility.sDbnull(objPK.DauhieulietvandongMota);

                opt_dauhieu_mangnao_co.Checked = Utility.Bool2Bool(objPK.DauhieuMangnaoCo);
                opt_dauhieu_mangnao_khong.Checked = Utility.Bool2Bool(objPK.DauhieuMangnaoKhong);
                txt_dauhieu_mangnao_mota.Text = Utility.sDbnull(objPK.DauhieuMangnaoMota);

                opt_roiloancamgiac_co.Checked = Utility.Bool2Bool(objPK.RoiloancamgiacCo);
                opt_roiloancamgiac_khong.Checked = Utility.Bool2Bool(objPK.RoiloancamgiacKhong);
                txt_roiloancamgiac_mota.Text = Utility.sDbnull(objPK.RoiloancamgiacMota);

                opt_roiloangiacquan_co.Checked = Utility.Bool2Bool(objPK.RoiloangiacquanCo);
                opt_roiloangiacquan_khong.Checked = Utility.Bool2Bool(objPK.RoiloangiacquanKhong);
                txt_roiloangiacquan_mota.Text = Utility.sDbnull(objPK.RoiloangiacquanMota);

                opt_roiloanthangbang_co.Checked = Utility.Bool2Bool(objPK.RoiloanthangbangCo);
                opt_roiloanthangbang_khong.Checked = Utility.Bool2Bool(objPK.RoiloanthangbangKhong);
                txt_roiloanthangbang_mota.Text = Utility.sDbnull(objPK.RoiloanthangbangMota);

                opt_roiloantrinho_co.Checked = Utility.Bool2Bool(objPK.RoiloantrinhoCo);
                opt_roiloantrinho_khong.Checked = Utility.Bool2Bool(objPK.RoiloantrinhoKhong);
                txt_roiloantrinho_mota.Text = Utility.sDbnull(objPK.RoiloantrinhoMota);

                opt_roiloantamthan_co.Checked = Utility.Bool2Bool(objPK.RoiloantamthanCo);
                opt_roiloantamthan_khong.Checked = Utility.Bool2Bool(objPK.RoiloantamthanKhong);
                txt_roiloantamthan_mota.Text = Utility.sDbnull(objPK.RoiloantamthanMota);

                opt_dongkinh_co.Checked = Utility.Bool2Bool(objPK.DongkinhCo);
                opt_dongkinh_khong.Checked = Utility.Bool2Bool(objPK.DongkinhKhong);
                txt_dongkinh_mota.Text = Utility.sDbnull(objPK.DongkinhMota);

                opt_dotquy_co.Checked = Utility.Bool2Bool(objPK.DotquyCo);
                opt_dotquy_khong.Checked = Utility.Bool2Bool(objPK.DotquyKhong);
                txt_dotquy_mota.Text = Utility.sDbnull(objPK.DotquyMota);

                txt_thankinh_mota.Text = Utility.sDbnull(objPK.ThankinhMota);

                opt_cotsong_binhthuong.Checked = Utility.Bool2Bool(objPK.CotsongBinhthuong);
                opt_cotsong_batthuong.Checked = Utility.Bool2Bool(objPK.CotsongBatthuong);
                txt_cotsong_mota.Text = Utility.sDbnull(objPK.CotsongMota);

                opt_biendangcotsong_co.Checked = Utility.Bool2Bool(objPK.BiendangcotsongCo);
                opt_biendangcotsong_khong.Checked = Utility.Bool2Bool(objPK.BiendangcotsongKhong);

                opt_cotsong_bamtim_co.Checked = Utility.Bool2Bool(objPK.CotsongBamtimCo);
                opt_cotsong_bamtim_khong.Checked = Utility.Bool2Bool(objPK.CotsongBamtimKhong);

                opt_daucotsong_co.Checked = Utility.Bool2Bool(objPK.DaucotsongCo);
                opt_daucotsong_khong.Checked = Utility.Bool2Bool(objPK.DaucotsongKhong);

                opt_roiloanthankinhtuysong_co.Checked = Utility.Bool2Bool(objPK.RoiloanthankinhtuysongCo);
                opt_roiloanthankinhtuysong_khong.Checked = Utility.Bool2Bool(objPK.RoiloanthankinhtuysongKhong);

                opt_phanxabatthuong_co.Checked = Utility.Bool2Bool(objPK.PhanxabatthuongCo);
                opt_phanxabatthuong_khong.Checked = Utility.Bool2Bool(objPK.PhanxabatthuongKhong);

                opt_cacnghiemphapkham_co.Checked = Utility.Bool2Bool(objPK.CacnghiemphapkhamCo);
                opt_cacnghiemphapkham_khong.Checked = Utility.Bool2Bool(objPK.CacnghiemphapkhamKhong);


                txt_cotsong_khac.Text = Utility.sDbnull(objPK.CotsongKhac);
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
                objPK.ThankinhBinhthuong = opt_thankinh_binhthuong.Checked;
                objPK.ThankinhBatthuong = opt_thankinh_batthuong.Checked;
                objPK.ThankinhMota = opt_thankinh_batthuong.Checked? Utility.sDbnull(txt_thankinh_mota.Text):"";

                objPK.VetthuongvungdauKhong = opt_vetthuongvungdau_khong.Checked;
                objPK.VetthuongvungdauCo = opt_vetthuongvungdau_co.Checked;
                objPK.VetthuongvungdauMota = opt_vetthuongvungdau_co.Checked ? Utility.sDbnull(txt_vetthuongvungdau_mota.Text) : "";

                objPK.SungneCo = opt_sungne_co.Checked;
                objPK.SungneKhong = opt_sungne_khong.Checked;
                objPK.SungneMota = opt_sungne_co.Checked ? Utility.sDbnull(txt_sungne_mota.Text) : "";

                objPK.BiendanghammatCo = opt_biendanghammat_co.Checked;
                objPK.DauhieuthankinhkhutruKhong = opt_dauhieuthankinhkhutru_khong.Checked;
                objPK.DauhieuthankinhkhutruMota = opt_biendanghammat_co.Checked ? Utility.sDbnull(txt_dauhieuthankinhkhutru_mota.Text) : "";

                objPK.DauhieuthankinhthucvatCo = opt_dauhieuthankinhthucvat_co.Checked;
                objPK.DauhieuthankinhthucvatKhong = opt_dauhieuthankinhthucvat_khong.Checked;
                objPK.DauhieuthankinhthucvatMota = opt_dauhieuthankinhthucvat_co.Checked ? Utility.sDbnull(txt_dauhieuthankinhthucvat_mota.Text) : "";

                objPK.DauhieuthankinhngoaibienCo = opt_dauhieuthankinhngoaibien_co.Checked;
                objPK.DauhieuthankinhngoaibienKhong = opt_dauhieuthankinhngoaibien_khong.Checked;
                objPK.DauhieuthankinhngoaibienMota = opt_dauhieuthankinhngoaibien_co.Checked ? Utility.sDbnull(txt_dauhieuthankinhngoaibien_mota.Text) : "";

                objPK.DauhieulietthankinhsoCo = opt_dauhieulietthankinhso_co.Checked;
                objPK.DauhieulietthankinhsoKhong = opt_dauhieulietthankinhso_khong.Checked;
                objPK.DauhieulietthankinhsoMota = opt_dauhieulietthankinhso_co.Checked ? Utility.sDbnull(txt_dauhieulietthankinhso_mota.Text) : "";

                objPK.DauhieulietvandongCo = opt_dauhieulietvandong_co.Checked;
                objPK.DauhieulietvandongKhong = opt_dauhieulietvandong_khong.Checked;
                objPK.DauhieulietvandongMota = opt_dauhieulietvandong_co.Checked ? Utility.sDbnull(txt_dauhieulietvandong_mota.Text) : "";

                objPK.DauhieuMangnaoCo = opt_dauhieu_mangnao_co.Checked;
                objPK.DauhieuMangnaoKhong = opt_dauhieu_mangnao_khong.Checked;
                objPK.DauhieuMangnaoMota = opt_dauhieu_mangnao_co.Checked ? Utility.sDbnull(txt_dauhieu_mangnao_mota.Text) : "";

                objPK.RoiloancamgiacCo = opt_roiloancamgiac_co.Checked;
                objPK.RoiloancamgiacKhong = opt_roiloancamgiac_khong.Checked;
                objPK.RoiloancamgiacMota = opt_roiloancamgiac_co.Checked ? Utility.sDbnull(txt_roiloancamgiac_mota.Text) : "";

                objPK.RoiloangiacquanCo = opt_roiloangiacquan_co.Checked;
                objPK.RoiloangiacquanKhong = opt_roiloangiacquan_khong.Checked;
                objPK.RoiloangiacquanMota = opt_roiloangiacquan_co.Checked ? Utility.sDbnull(txt_roiloangiacquan_mota.Text) : "";

                objPK.RoiloanthangbangCo = opt_roiloanthangbang_co.Checked;
                objPK.RoiloanthangbangKhong = opt_roiloanthangbang_khong.Checked;
                objPK.RoiloanthangbangMota = opt_roiloanthangbang_co.Checked ? Utility.sDbnull(txt_roiloanthangbang_mota.Text) : "";

                objPK.RoiloantrinhoCo = opt_roiloantrinho_co.Checked;
                objPK.RoiloantrinhoKhong = opt_roiloantrinho_khong.Checked;
                objPK.RoiloantrinhoMota = opt_roiloantrinho_co.Checked ? Utility.sDbnull(txt_roiloantrinho_mota.Text) : "";

                objPK.RoiloantamthanCo = opt_roiloantamthan_co.Checked;
                objPK.RoiloantamthanKhong = opt_roiloantamthan_khong.Checked;
                objPK.RoiloantamthanMota = opt_roiloantamthan_co.Checked ? Utility.sDbnull(txt_roiloantamthan_mota.Text) : "";

                objPK.DongkinhCo = opt_dongkinh_co.Checked;
                objPK.DongkinhKhong = opt_dongkinh_khong.Checked;
                objPK.DongkinhMota = opt_dongkinh_co.Checked ? Utility.sDbnull(txt_dongkinh_mota.Text) : "";

                objPK.DotquyCo = opt_dotquy_co.Checked;
                objPK.DotquyKhong = opt_dotquy_khong.Checked;
                objPK.DotquyMota = opt_dotquy_co.Checked ? Utility.sDbnull(txt_dotquy_mota.Text) : "";

                objPK.ThankinhMota = Utility.sDbnull(txt_thankinh_mota.Text);

                objPK.CotsongBinhthuong = opt_cotsong_binhthuong.Checked;
                objPK.CotsongBatthuong = opt_cotsong_batthuong.Checked;
                objPK.CotsongMota = opt_cotsong_batthuong.Checked ? Utility.sDbnull(txt_cotsong_mota.Text) : "";

                objPK.BiendangcotsongCo = opt_biendangcotsong_co.Checked;
                objPK.BiendangcotsongKhong = opt_biendangcotsong_khong.Checked;

                objPK.CotsongBamtimCo = opt_cotsong_bamtim_co.Checked;
                objPK.CotsongBamtimKhong = opt_cotsong_bamtim_khong.Checked;

                objPK.DaucotsongCo = opt_daucotsong_co.Checked;
                objPK.DaucotsongKhong = opt_daucotsong_khong.Checked;

                objPK.RoiloanthankinhtuysongCo = opt_roiloanthankinhtuysong_co.Checked;
                objPK.RoiloanthankinhtuysongKhong = opt_roiloanthankinhtuysong_khong.Checked;

                objPK.PhanxabatthuongCo = opt_phanxabatthuong_co.Checked;
                objPK.PhanxabatthuongKhong = opt_phanxabatthuong_khong.Checked;

                objPK.CacnghiemphapkhamCo = opt_cacnghiemphapkham_co.Checked;
                objPK.CacnghiemphapkhamKhong = opt_cacnghiemphapkham_khong.Checked;

                objPK.CotsongKhac = Utility.sDbnull(txt_cotsong_khac.Text);



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

        private void opt_vetthuongvungdau_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_vetthuongvungdau_mota, sender as RadioButton);
        }

        private void opt_sungne_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_sungne_mota, sender as RadioButton);
        }

        private void opt_biendanghammat_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_biendanghammat_mota, sender as RadioButton);
        }

        private void opt_dauhieuthankinhkhutru_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieuthankinhkhutru_mota, sender as RadioButton);
        }

        private void opt_dauhieuthankinhthucvat_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieuthankinhthucvat_mota, sender as RadioButton);
        }

        private void opt_dauhieuthankinhngoaibien_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieuthankinhngoaibien_mota, sender as RadioButton);
        }

        private void opt_dauhieulietthankinhso_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieulietthankinhso_mota, sender as RadioButton);
        }

        private void opt_dauhieulietvandong_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieulietvandong_mota, sender as RadioButton);
        }

        private void opt_dauhieu_mangnao_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dauhieu_mangnao_mota, sender as RadioButton);
        }

        private void opt_roiloancamgiac_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_roiloancamgiac_mota, sender as RadioButton);
        }

        private void opt_roiloangiacquan_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_roiloangiacquan_mota, sender as RadioButton);
        }

        private void opt_roiloanthangbang_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_roiloanthangbang_mota, sender as RadioButton);
        }

        private void opt_roiloantrinho_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_roiloantrinho_mota, sender as RadioButton);
        }

        private void opt_roiloantamthan_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_roiloantamthan_mota, sender as RadioButton);
        }

        private void opt_dongkinh_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dongkinh_mota, sender as RadioButton);
        }

        private void opt_dotquy_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_dotquy_mota, sender as RadioButton);
        }

        private void opt_cotsong_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_cotsong_mota, sender as RadioButton);
        }

        private void opt_biendangcotsong_co_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
