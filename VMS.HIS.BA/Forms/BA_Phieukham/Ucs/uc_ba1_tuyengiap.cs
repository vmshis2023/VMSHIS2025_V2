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
    public partial class uc_ba1_tuyengiap : UserControl
    {
        public uc_ba1_tuyengiap()
        {
            InitializeComponent();
        }
        public void GetData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
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
                txt_phu_khacmota.Text= Utility.sDbnull(objPK.PhuKhacmota);

                opt_hach_khong.Checked = Utility.Bool2Bool(objPK.HachKhong);
                opt_hach_co.Checked = Utility.Bool2Bool(objPK.HachCo);
                chk_vitri_co.Checked = Utility.Bool2Bool(objPK.VitriCo);
                chk_vitri_nach.Checked = Utility.Bool2Bool(objPK.VitriNach);
                chk_vitri_ben.Checked = Utility.Bool2Bool(objPK.VitriBen);
                txt_vitrihach_khac.Text = Utility.sDbnull(objPK.VitriKhac);

                1hac.Checked = Utility.Bool2Bool(objPK.RiraoPhenangBinthuong);
                opt_rirao_phenang_giam.Checked = Utility.Bool2Bool(objPK.RiraoPhenangGiam);
                txt_rirao_phenang_vitri.Text = Utility.sDbnull(objPK.RiraoPhenangVitri);

                opt_rungthanh_binhthuong.Checked = Utility.Bool2Bool(objPK.RungthanhBinhthuong);
                opt_rungthanh_tang.Checked = Utility.Bool2Bool(objPK.RungthanhTang);
                opt_rungthanh_giam.Checked = Utility.Bool2Bool(objPK.RungthanhGiam);
                txt_rungthanh_vitri.Text = Utility.sDbnull(objPK.RungthanhVitri);

                opt_rale_khong.Checked = Utility.Bool2Bool(objPK.RaleKhong);
                chk_rale_am.Checked = Utility.Bool2Bool(objPK.RaleAm);
                chk_rale_ngay.Checked = Utility.Bool2Bool(objPK.RaleNgay);
                chk_rale_no.Checked = Utility.Bool2Bool(objPK.RaleNo);
                chk_rale_rit.Checked = Utility.Bool2Bool(objPK.RaleRit);
                opt_rale_co.Checked = Utility.Bool2Bool(objPK.RaleCo);
                chk_rale_khac.Checked = Utility.Bool2Bool(objPK.RaleKhac);
                txt_rale_khac_mota.Text = Utility.sDbnull(objPK.RaleKhacMota);



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
                objPK.HohapBatthuong = opt_hohap_batthuong.Checked;
                objPK.HohapBinhthuong = opt_hohap_binhthuong.Checked;
                objPK.HohapMota = opt_hohap_batthuong.Checked ? Utility.sDbnull(txt_hohap_mota.Text) : "";

                objPK.KhothoCo = opt_khotho_co.Checked;
                objPK.KhothoKhong = opt_khotho_khong.Checked;

                objPK.BiendangLongngucCo = opt_biendang_longnguc_co.Checked;
                objPK.BiendangLongngucKhong = opt_biendang_longnguc_khong.Checked;

                objPK.GoBinhthuong = opt_go_binhthuong.Checked;
                objPK.GoDuc = opt_go_duc.Checked;
                objPK.GoVang = opt_go_vang.Checked;
                objPK.GoVitri = opt_go_vang.Checked ? Utility.sDbnull(txt_go_vitri.Text) : "";

                objPK.RiraoPhenangBinthuong = opt_rirao_phenang_binthuong.Checked;
                objPK.RiraoPhenangGiam = opt_rirao_phenang_giam.Checked;
                objPK.RiraoPhenangVitri = opt_rirao_phenang_giam.Checked ? Utility.sDbnull(txt_rirao_phenang_vitri.Text) : "";

                objPK.RungthanhBinhthuong = opt_rungthanh_binhthuong.Checked;
                objPK.RungthanhTang = opt_rungthanh_tang.Checked;
                objPK.RungthanhGiam = opt_rungthanh_giam.Checked;
                objPK.RungthanhVitri = opt_rungthanh_giam.Checked ? Utility.sDbnull(txt_rungthanh_vitri.Text) : "";

                objPK.RaleKhong = opt_rale_khong.Checked;
                objPK.RaleAm = chk_rale_am.Checked;
                objPK.RaleNgay = chk_rale_ngay.Checked;
                objPK.RaleNo = chk_rale_no.Checked;
                objPK.RaleRit = chk_rale_rit.Checked;
                objPK.RaleCo = opt_rale_co.Checked;
                objPK.RaleKhac = chk_rale_khac.Checked;
                objPK.RaleKhacMota = chk_rale_khac.Checked ? Utility.sDbnull(txt_rale_khac_mota.Text) : "";



            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
