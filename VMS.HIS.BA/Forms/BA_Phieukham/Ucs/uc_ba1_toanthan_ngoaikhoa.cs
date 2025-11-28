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
    public partial class uc_ba1_toanthan_ngoaikhoa : UserControl
    {
        public uc_ba1_toanthan_ngoaikhoa()
        {
            InitializeComponent();
        }
        
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_toanthan_binhthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBinhthuong);
                opt_toanthan_batthuong.Checked = Utility.Bool2Bool(objPK.ToanthanBatthuong);
                txt_toanthan_mota.Text = Utility.sDbnull(objPK.ToanthanMota);

                opt_tiepxuc_tot_khong.Checked = Utility.Bool2Bool(objPK.TiepxucTotKhong);
                opt_tiepxuc_tot_co.Checked = Utility.Bool2Bool(objPK.TiepxucTotCo);

                opt_tiepxuc_cham_co.Checked = Utility.Bool2Bool(objPK.TiepxucChamCo);
                opt_tiepxuc_cham_khong.Checked = Utility.Bool2Bool(objPK.TiepxucChamKhong);

                opt_ngu_ga_khong.Checked = Utility.Bool2Bool(objPK.NguGaKhong);
                opt_ngu_ga_co.Checked = Utility.Bool2Bool(objPK.NguGaCo);

                opt_lo_mo_khong.Checked = Utility.Bool2Bool(objPK.LoMoKhong);
                opt_lo_mo_co.Checked = Utility.Bool2Bool(objPK.LoMoCo);

                opt_hon_me_khong.Checked = Utility.Bool2Bool(objPK.HonMeKhong);
                opt_hon_me_co.Checked = Utility.Bool2Bool(objPK.HonMeCo);

                nmr_glassgow.Value =Utility.DecimaltoDbnull( objPK.Glassgow);

                opt_da_hong.Checked = Utility.Bool2Bool(objPK.DaHong);
                opt_da_vang.Checked = Utility.Bool2Bool(objPK.DaVang);
                opt_da_sam.Checked = Utility.Bool2Bool(objPK.DaSam);

                opt_niemmac_hong.Checked = Utility.Bool2Bool(objPK.NiemmacHong);
                opt_niemmac_nhot.Checked = Utility.Bool2Bool(objPK.NiemmacNhot);

              

                opt_luoi_binhthuong.Checked = Utility.Bool2Bool(objPK.MoikhoLuoibanKhong);
                opt_luoi_ban.Checked = Utility.Bool2Bool(objPK.MoikhoLuoibanCo);
               

              

                opt_tuyengiap_binhthuong.Checked = Utility.Bool2Bool(objPK.TuyengiapBinhthuong);
                opt_tuyengiap_to.Checked = Utility.Bool2Bool(objPK.TuyengiapTo);
                opt_tieng_thoi.Checked = Utility.Bool2Bool(objPK.TiengThoi);

                opt_hach_khong.Checked = Utility.Bool2Bool(objPK.HachKhong);
                opt_hach_co.Checked = Utility.Bool2Bool(objPK.HachCo);
                opt_tinhchat_mem.Checked = Utility.Bool2Bool(objPK.TinhchatMem);
                opt_tinhchat_cung.Checked = Utility.Bool2Bool(objPK.TinhchatCung);

                opt_phu_khong.Checked = Utility.Bool2Bool(objPK.PhuKhong);
                opt_phu_co.Checked = Utility.Bool2Bool(objPK.PhuCo);
                txt_phu_khacmota.Text = Utility.sDbnull(objPK.PhuKhacmota);

                opt_da_xuathuyet_co.Checked = Utility.Bool2Bool(objPK.DaXuathuyetCo);
                opt_da_xuathuyet_khong.Checked = Utility.Bool2Bool(objPK.DaXuathuyetKhong);
               
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
                objPK.ToanthanBinhthuong = opt_toanthan_binhthuong.Checked;
                objPK.ToanthanBatthuong = opt_toanthan_batthuong.Checked;
                objPK.ToanthanMota = opt_toanthan_batthuong.Checked ? Utility.sDbnull(txt_toanthan_mota.Text):"";

                objPK.TiepxucTotKhong = opt_tiepxuc_tot_khong.Checked;
                objPK.TiepxucTotCo = opt_tiepxuc_tot_co.Checked;

                objPK.TiepxucChamCo = opt_tiepxuc_cham_co.Checked;
                objPK.TiepxucChamKhong = opt_tiepxuc_cham_khong.Checked;

                objPK.NguGaKhong = opt_ngu_ga_khong.Checked;
                objPK.NguGaCo = opt_ngu_ga_co.Checked;

                objPK.LoMoKhong = opt_lo_mo_khong.Checked;
                objPK.LoMoCo = opt_lo_mo_co.Checked;

                objPK.HonMeKhong = opt_hon_me_khong.Checked;
                objPK.HonMeCo = opt_hon_me_co.Checked;

                objPK.Glassgow = Utility.Int16Dbnull(nmr_glassgow.Value);

                objPK.DaHong = opt_da_hong.Checked;
                objPK.DaVang = opt_da_vang.Checked;
                objPK.DaSam = opt_da_sam.Checked;

                objPK.NiemmacHong = opt_niemmac_hong.Checked;
                objPK.NiemmacNhot = opt_niemmac_nhot.Checked;

                objPK.MoikhoLuoibanKhong = opt_luoi_binhthuong.Checked;
                objPK.MoikhoLuoibanCo = opt_luoi_ban.Checked;

                objPK.TuyengiapBinhthuong = opt_tuyengiap_binhthuong.Checked;
                objPK.TuyengiapTo = opt_tuyengiap_to.Checked;
                objPK.TiengThoi = opt_tieng_thoi.Checked;

                objPK.HachKhong = opt_hach_khong.Checked;
                objPK.HachCo = opt_hach_co.Checked;
                objPK.TinhchatMem = opt_tinhchat_mem.Checked;
                objPK.TinhchatCung = opt_tinhchat_cung.Checked;

                objPK.PhuKhong = opt_phu_khong.Checked;
                objPK.PhuCo = opt_phu_co.Checked;
                objPK.PhuKhacmota = opt_phu_co.Checked? Utility.sDbnull(txt_phu_khacmota.Text):"";

                objPK.DaXuathuyetCo = opt_da_xuathuyet_co.Checked;
                objPK.DaXuathuyetKhong = opt_da_xuathuyet_khong.Checked;



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

       

        private void chk_phu_khac_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox _obj = sender as CheckBox;
            txt_phu_khacmota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_phu_khacmota.Focus();
        }

        private void opt_phu_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_phu_khacmota, sender as RadioButton);
        }
    }
}
