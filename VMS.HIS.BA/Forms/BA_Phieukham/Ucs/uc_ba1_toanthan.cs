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
    public partial class uc_ba1_toanthan : UserControl
    {
        public uc_ba1_toanthan()
        {
            InitializeComponent();
        }
        
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_toanthan_binhthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBinhthuong);
                opt_toanthan_batthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBatthuong);
                txt_toanthan_mota.Text = Utility.sDbnull(objPK.ToanthanMota);
                opt_tiepxuc_tot_khong.Checked = Utility.Bool2Bool(objPK.TiepxucTotKhong);
                opt_tiepxuc_tot_co.Checked = Utility.Bool2Bool(objPK.TiepxucTotCo);
                opt_ngu_ga_khong.Checked = Utility.Bool2Bool(objPK.NguGaKhong);
                opt_ngu_ga_co.Checked = Utility.Bool2Bool(objPK.NguGaCo);
                opt_lo_mo_khong.Checked = Utility.Bool2Bool(objPK.LoMoKhong);
                opt_lo_mo_co.Checked = Utility.Bool2Bool(objPK.LoMoCo);
                opt_hon_me_khong.Checked = Utility.Bool2Bool(objPK.HonMeKhong);
                opt_hon_me_co.Checked = Utility.Bool2Bool(objPK.HonMeCo);
                nmr_glassgow.Value =Utility.DecimaltoDbnull( objPK.Glassgow);
                opt_da_hong.Checked = Utility.Bool2Bool(objPK.DaHong);
                opt_da_vang.Checked = Utility.Bool2Bool(objPK.DaVang);
                opt_da_xanh.Checked = Utility.Bool2Bool(objPK.DaXanh);
                txt_da_khac_mota.Text = Utility.sDbnull(objPK.DaKhacMota);

                opt_xuathuyet_niemmac_khong.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacKhong);
                opt_xuathuyet_niemmac_co.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacCo);
                chk_xuathuyet_niemmac_mat.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacMat);
                chk_xuathuyet_niemmac_mui.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacMui);
                chk_xuathuyet_niemmac_mieng.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacMieng);
                chk_xuathuyet_niemmac_tieumau.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacTieumau);
                chk_xuathuyet_niemmac_roiloan_kinhnguyet.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacRoiloanKinhnguyet);
                chk_xuathuyet_niemmac_khac.Checked = Utility.Bool2Bool(objPK.XuathuyetNiemmacKhac);
                txt_xuathuyet_niemmac_khac_mota.Text = Utility.sDbnull(objPK.XuathuyetNiemmacKhacMota);

                opt_da_xuathuyet_khong.Checked = Utility.Bool2Bool(objPK.DaXuathuyetKhong);
                opt_da_xuathuyet_co.Checked = Utility.Bool2Bool(objPK.DaXuathuyetCo);
                chk_da_xuathuyet_cham_not.Checked = Utility.Bool2Bool(objPK.DaXuathuyetChamNot);
                chk_da_xuathuyet_mangbamda.Checked = Utility.Bool2Bool(objPK.DaXuathuyetMangbamda);
                chk_da_xuathuyet_tumau.Checked = Utility.Bool2Bool(objPK.DaXuathuyetTumau);
                chk_da_xuathuyet_khac.Checked = Utility.Bool2Bool(objPK.DaXuathuyetKhac);
                txt_da_xuathuyet_khac.Text= Utility.sDbnull(objPK.DaXuathuyetKhacMota);

                opt_ketmac_binhthuong.Checked = Utility.Bool2Bool(objPK.KetmacBinhthuong);
                opt_ketmac_do.Checked = Utility.Bool2Bool(objPK.KetmacDo);
                opt_ketmac_vang.Checked = Utility.Bool2Bool(objPK.KetmacVang);
                opt_ketmac_khac.Checked = Utility.Bool2Bool(objPK.KetmacKhac);
                txt_ketmac_khac_mota.Text = Utility.sDbnull(objPK.KetmacKhacMota);

                opt_luoi_binhthuong.Checked = Utility.Bool2Bool(objPK.LuoiBinhthuong);
                opt_luoi_ban.Checked = Utility.Bool2Bool(objPK.LuoiBan);
                opt_luoi_gaimon_mat.Checked = Utility.Bool2Bool(objPK.LuoiGaimonMat);
                opt_luoi_khac.Checked = Utility.Bool2Bool(objPK.LuoiKhac);
                txt_luoi_khac_mota.Text = Utility.sDbnull(objPK.LuoiKhacMota);

                opt_longtocmong_binhthuong.Checked = Utility.Bool2Bool(objPK.LongtocmongBinhthuong);
                opt_longtocmong_khac.Checked = Utility.Bool2Bool(objPK.LongtocmongKhac);
                txt_longtocmong_khac_mota.Text = Utility.sDbnull(objPK.LongtocmongKhacMota);

                opt_tuyengiap_binhthuong.Checked = Utility.Bool2Bool(objPK.TuyengiapBinhthuong);
                opt_tuyengiap_to.Checked = Utility.Bool2Bool(objPK.TuyengiapTo);
                opt_tieng_thoi.Checked = Utility.Bool2Bool(objPK.TiengThoi);

                opt_phu_khong.Checked = Utility.Bool2Bool(objPK.PhuKhong);
                opt_phu_co.Checked = Utility.Bool2Bool(objPK.PhuCo);

                chk_phu_chiduoi.Checked = Utility.Bool2Bool(objPK.PhuChiduoi);
                chk_phu_chitren.Checked = Utility.Bool2Bool(objPK.PhuChitren);
                chk_phu_mat.Checked = Utility.Bool2Bool(objPK.PhuMat);
                chk_phu_aokhoac.Checked = Utility.Bool2Bool(objPK.PhuAokhoac);
                chk_phu_toanthan.Checked = Utility.Bool2Bool(objPK.PhuToanthan);
                chk_phu_khac.Checked = Utility.Bool2Bool(objPK.PhuKhac);
                txt_phu_khacmota.Text = Utility.sDbnull(objPK.PhuKhacmota);

                opt_hach_khong.Checked = Utility.Bool2Bool(objPK.HachKhong);
                opt_hach_co.Checked = Utility.Bool2Bool(objPK.HachCo);
                chk_vitri_co.Checked = Utility.Bool2Bool(objPK.VitriCo);
                chk_vitri_nach.Checked = Utility.Bool2Bool(objPK.VitriNach);
                chk_vitri_ben.Checked = Utility.Bool2Bool(objPK.VitriBen);
                chk_vitri_hach_khac.Checked = Utility.Bool2Bool(objPK.VitriKhac);
                txt_vitrihach_khac.Text = Utility.sDbnull(objPK.VitriKhacMota);

                opt_soluong_mothach.Checked = Utility.Bool2Bool(objPK.HachKhong);
                opt_soluong_nhieuhach.Checked = Utility.Bool2Bool(objPK.SoluongNhieuhach);

                opt_tinhchat_mem.Checked = Utility.Bool2Bool(objPK.TinhchatMem);
                opt_tinhchat_cung.Checked = Utility.Bool2Bool(objPK.TinhchatCung);
                nmr_duongkinh_hach_lonnhat.Value = Utility.DecimaltoDbnull(objPK.DuongkinhHachLonnhat);
                opt_didong_co.Checked = Utility.Bool2Bool(objPK.DidongCo);
                opt_didong_khong.Checked = Utility.Bool2Bool(objPK.DidongKhong);

                opt_dau_khong.Checked = Utility.Bool2Bool(objPK.DauKhong);
                opt_dau_co.Checked = Utility.Bool2Bool(objPK.DauCo);
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
                objPK.ToanthanBinhthuong = opt_toanthan_binhthuong.Checked;
                objPK.ToanthanBatthuong = opt_toanthan_batthuong.Checked;
                objPK.ToanthanMota = opt_toanthan_batthuong.Checked?Utility.sDbnull(txt_toanthan_mota.Text):"";
                objPK.TiepxucTotKhong = opt_tiepxuc_tot_khong.Checked;
                objPK.TiepxucTotCo = opt_tiepxuc_tot_co.Checked;
                objPK.NguGaKhong = opt_ngu_ga_khong.Checked;
                objPK.NguGaCo = opt_ngu_ga_co.Checked;
                objPK.LoMoKhong = opt_lo_mo_khong.Checked;
                objPK.LoMoCo = opt_lo_mo_co.Checked;
                objPK.HonMeKhong = opt_hon_me_khong.Checked;
                objPK.HonMeCo = opt_hon_me_co.Checked;
                objPK.Glassgow = opt_hon_me_co.Checked?Utility.Int16Dbnull( nmr_glassgow.Value):(Int16)0;
                objPK.DaHong = opt_da_hong.Checked;
                objPK.DaVang = opt_da_vang.Checked;
                objPK.DaXanh = opt_da_xanh.Checked;
                objPK.DaKhac = chk_da_khac.Checked;
                objPK.DaKhacMota = Utility.sDbnull(txt_da_khac_mota.Text);

                objPK.XuathuyetNiemmacKhong = opt_xuathuyet_niemmac_khong.Checked;
                objPK.XuathuyetNiemmacCo = opt_xuathuyet_niemmac_co.Checked;
                objPK.XuathuyetNiemmacMat = opt_xuathuyet_niemmac_co.Checked? chk_xuathuyet_niemmac_mat.Checked : false;
                objPK.XuathuyetNiemmacMui = opt_xuathuyet_niemmac_co.Checked ? chk_xuathuyet_niemmac_mui.Checked : false;
                objPK.XuathuyetNiemmacMieng = opt_xuathuyet_niemmac_co.Checked ? chk_xuathuyet_niemmac_mieng.Checked : false;
                objPK.XuathuyetNiemmacTieumau = opt_xuathuyet_niemmac_co.Checked ? chk_xuathuyet_niemmac_tieumau.Checked : false;
                objPK.XuathuyetNiemmacRoiloanKinhnguyet = opt_xuathuyet_niemmac_co.Checked ? chk_xuathuyet_niemmac_roiloan_kinhnguyet.Checked : false;
                objPK.XuathuyetNiemmacKhac = opt_xuathuyet_niemmac_co.Checked ? chk_xuathuyet_niemmac_khac.Checked:false;
                objPK.XuathuyetNiemmacKhacMota = objPK.XuathuyetNiemmacKhac.Value ? Utility.sDbnull(txt_xuathuyet_niemmac_khac_mota.Text):"";

                objPK.DaXuathuyetKhong = opt_da_xuathuyet_khong.Checked;
                objPK.DaXuathuyetCo = opt_da_xuathuyet_co.Checked;
                objPK.DaXuathuyetChamNot = opt_da_xuathuyet_co.Checked? chk_da_xuathuyet_cham_not.Checked : false;
                objPK.DaXuathuyetMangbamda = opt_da_xuathuyet_co.Checked ? chk_da_xuathuyet_mangbamda.Checked : false;
                objPK.DaXuathuyetTumau = opt_da_xuathuyet_co.Checked ? chk_da_xuathuyet_tumau.Checked : false;
                objPK.DaXuathuyetKhac = opt_da_xuathuyet_co.Checked ? chk_da_xuathuyet_khac.Checked : false;
                objPK.DaXuathuyetKhacMota = objPK.DaXuathuyetKhac.Value ? Utility.sDbnull(txt_da_xuathuyet_khac.Text):"";

                objPK.KetmacBinhthuong = opt_ketmac_binhthuong.Checked;
                objPK.KetmacDo = opt_ketmac_do.Checked;
                objPK.KetmacVang = opt_ketmac_vang.Checked;
                objPK.KetmacKhac = opt_ketmac_khac.Checked;
                objPK.KetmacKhacMota = opt_ketmac_khac.Checked? Utility.sDbnull(txt_ketmac_khac_mota.Text):"";

                objPK.LuoiBinhthuong = opt_luoi_binhthuong.Checked;
                objPK.LuoiBan = opt_luoi_ban.Checked;
                objPK.LuoiGaimonMat = opt_luoi_gaimon_mat.Checked;
                objPK.LuoiKhac = opt_luoi_khac.Checked;
                objPK.LuoiKhacMota = opt_luoi_khac.Checked?Utility.sDbnull(txt_luoi_khac_mota.Text):"";

                objPK.LongtocmongBinhthuong = opt_longtocmong_binhthuong.Checked;
                objPK.LongtocmongKhac = opt_longtocmong_khac.Checked;
                objPK.LongtocmongKhacMota = opt_longtocmong_khac.Checked? Utility.sDbnull(txt_longtocmong_khac_mota.Text):"";

                objPK.TuyengiapBinhthuong = opt_tuyengiap_binhthuong.Checked;
                objPK.TuyengiapTo = opt_tuyengiap_to.Checked;
                objPK.TiengThoi = opt_tieng_thoi.Checked;

                objPK.PhuKhong = opt_phu_khong.Checked;
                objPK.PhuCo = opt_phu_co.Checked;

                objPK.PhuChiduoi = opt_phu_co.Checked? chk_phu_chiduoi.Checked : false;
                objPK.PhuChitren = opt_phu_co.Checked ? chk_phu_chitren.Checked : false;
                objPK.PhuMat = opt_phu_co.Checked ? chk_phu_mat.Checked : false;
                objPK.PhuAokhoac = opt_phu_co.Checked ? chk_phu_aokhoac.Checked : false;
                objPK.PhuToanthan = opt_phu_co.Checked ? chk_phu_toanthan.Checked : false;
                objPK.PhuKhac = opt_phu_co.Checked ? chk_phu_khac.Checked:false;
                objPK.PhuKhacmota = objPK.PhuKhac .Value? Utility.sDbnull(txt_phu_khacmota.Text):"";

                objPK.HachKhong = opt_hach_khong.Checked;
                objPK.HachCo = opt_hach_co.Checked;
                objPK.VitriCo = opt_hach_co.Checked?chk_vitri_co.Checked: false;
                objPK.VitriNach = opt_hach_co.Checked?chk_vitri_nach.Checked: false;
                objPK.VitriBen = opt_hach_co.Checked?chk_vitri_ben.Checked: false;
                objPK.VitriKhac = chk_vitri_hach_khac.Checked;
                objPK.VitriKhacMota = chk_vitri_hach_khac.Checked? Utility.sDbnull(txt_vitrihach_khac.Text):"";

                objPK.SoluongMothach = opt_soluong_mothach.Checked;
                objPK.SoluongNhieuhach = opt_soluong_nhieuhach.Checked;

                objPK.TinhchatMem = opt_tinhchat_mem.Checked;
                objPK.TinhchatCung = opt_tinhchat_cung.Checked;
                objPK.DuongkinhHachLonnhat = Utility.Int32Dbnull(nmr_duongkinh_hach_lonnhat.Value);

                objPK.DidongCo = opt_didong_co.Checked;
                objPK.DidongKhong = opt_didong_khong.Checked;

                objPK.DauKhong = opt_dau_khong.Checked;
                objPK.DauCo = opt_dau_co.Checked;


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_toanthan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_toanthan_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_toanthan_mota.Focus();
        }

        private void opt_hon_me_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            nmr_glassgow.Enabled = _obj.Checked;
            if (_obj.Checked) nmr_glassgow.Focus();
        }

        private void chk_da_xuathuyet_khac_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _obj = sender as CheckBox;
            txt_da_xuathuyet_khac.Enabled = _obj.Checked;
            if (_obj.Checked) txt_da_xuathuyet_khac.Focus();
        }

        private void chk_xuathuyet_niemmac_khac_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _obj = sender as CheckBox;
            txt_xuathuyet_niemmac_khac_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_xuathuyet_niemmac_khac_mota.Focus();
        }

        private void opt_ketmac_khac_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_ketmac_khac_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_ketmac_khac_mota.Focus();
        }

        private void opt_luoi_khac_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_luoi_khac_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_luoi_khac_mota.Focus();
        }

        private void opt_longtocmong_khac_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_longtocmong_khac_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_longtocmong_khac_mota.Focus();
        }

        private void chk_phu_khac_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _obj = sender as CheckBox;
            txt_phu_khacmota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_phu_khacmota.Focus();
        }

        private void opt_hach_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            chk_vitri_co.Enabled=chk_vitri_ben.Enabled=chk_vitri_nach.Enabled=txt_vitrihach_khac.Enabled = _obj.Checked;
        }

        private void opt_phu_co_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            pnlPhu.Enabled = chk_phu_khac.Enabled = _obj.Checked;
        }
    }
}
