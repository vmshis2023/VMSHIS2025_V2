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
    public partial class uc_ba_coxuongkhop_ngoaikhoa : UserControl
    {
        public uc_ba_coxuongkhop_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_coxuongkhop_batthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBatthuong);
                opt_coxuongkhop_binhthuong.Checked = Utility.Bool2Bool(objPK.CoxuongkhopBinhthuong);
                txt_coxuongkhop_mota.Text = opt_coxuongkhop_batthuong.Checked ? Utility.sDbnull(objPK.CoxuongkhopMota):"";

                opt_biendangxuong_co.Checked = Utility.Bool2Bool(objPK.BiendangxuongCo);
                opt_biendangxuong_khong.Checked = Utility.Bool2Bool(objPK.BiendangxuongKhong);
                txt_biendangxuong_mota.Text = opt_biendangxuong_co.Checked? Utility.sDbnull(objPK.BiendangxuongMota):"";

                opt_vetthuong_co.Checked = Utility.Bool2Bool(objPK.VetthuongCo);
                opt_vetthuong_khong.Checked = Utility.Bool2Bool(objPK.VetthuongKhong);

                opt_cxk_bamtim_co.Checked = Utility.Bool2Bool(objPK.CxkBamtimCo);
                opt_cxk_bamtim_khong.Checked = Utility.Bool2Bool(objPK.CxkBamtimKhong);
                txt_cxk_bamtim_mota.Text = opt_cxk_bamtim_co.Checked ? Utility.sDbnull(objPK.CxkBamtimMota):"";

                opt_laoxaoxuong_co.Checked = Utility.Bool2Bool(objPK.LaoxaoxuongCo);
                opt_laoxaoxuong_khong.Checked = Utility.Bool2Bool(objPK.LaoxaoxuongKhong);

                opt_machngoaivi_batthuong.Checked = Utility.Bool2Bool(objPK.MachngoaiviBatthuong);
                opt_machngoaivi_binhthuong.Checked = Utility.Bool2Bool(objPK.MachngoaiviBinhthuong);

                opt_vandongbinhthuong_co.Checked = Utility.Bool2Bool(objPK.VandongbinhthuongCo);
                opt_vandongbinhthuong_khong.Checked = Utility.Bool2Bool(objPK.VandongbinhthuongKhong);

                opt_camgiacnongsau_co.Checked = Utility.Bool2Bool(objPK.CamgiacnongsauCo);
                opt_camgiacnongsau_khong.Checked = Utility.Bool2Bool(objPK.CamgiacnongsauKhong);
                txt_camgiacnongsau_mota.Text = opt_camgiacnongsau_co.Checked? Utility.sDbnull(objPK.CamgiacnongsauMota):"";


                opt_chieudaichisovoibinhthuong_batthuong.Checked = Utility.Bool2Bool(objPK.ChieudaichisovoibinhthuongBatthuong);
                opt_chieudaichisovoibinhthuong_binhthuong.Checked = Utility.Bool2Bool(objPK.ChieudaichisovoibinhthuongBinhthuong);
                txt_chieudaichisovoibinhthuong_mota.Text = opt_chieudaichisovoibinhthuong_batthuong.Checked? Utility.sDbnull(objPK.ChieudaichisovoibinhthuongMota):"";

                opt_biendokhop_batthuong.Checked = Utility.Bool2Bool(objPK.BiendokhopBatthuong);
                opt_biendokhop_binhthuong.Checked = Utility.Bool2Bool(objPK.BiendokhopBinhthuong);
                txt_biendokhop_mota.Text = opt_biendokhop_batthuong.Checked? Utility.sDbnull(objPK.BiendokhopMota):"";

                txt_coxuongkhop_khac.Text = Utility.sDbnull(objPK.CoxuongkhopKhac);
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
                objPK.CoxuongkhopBatthuong = opt_coxuongkhop_batthuong.Checked;
                objPK.CoxuongkhopBinhthuong = opt_coxuongkhop_binhthuong.Checked;
                objPK.CoxuongkhopMota = opt_coxuongkhop_batthuong.Checked? Utility.sDbnull(txt_coxuongkhop_mota.Text):"";

                objPK.BiendangxuongCo = opt_biendangxuong_co.Checked;
                objPK.BiendangxuongKhong = opt_biendangxuong_khong.Checked;
                objPK.BiendangxuongMota = opt_biendangxuong_co.Checked ? Utility.sDbnull(txt_biendangxuong_mota.Text) : "";

                objPK.VetthuongCo = opt_vetthuong_co.Checked;
                objPK.VetthuongKhong = opt_vetthuong_khong.Checked;

                objPK.CxkBamtimCo = opt_cxk_bamtim_co.Checked;
                objPK.CxkBamtimKhong = opt_cxk_bamtim_khong.Checked;
                objPK.CxkBamtimMota = opt_cxk_bamtim_co.Checked ? Utility.sDbnull(txt_cxk_bamtim_mota.Text) : "";

                objPK.LaoxaoxuongCo = opt_laoxaoxuong_co.Checked;
                objPK.LaoxaoxuongKhong = opt_laoxaoxuong_khong.Checked;

                objPK.MachngoaiviBatthuong = opt_machngoaivi_batthuong.Checked;
                objPK.MachngoaiviBinhthuong = opt_machngoaivi_binhthuong.Checked;

                objPK.VandongbinhthuongCo = opt_vandongbinhthuong_co.Checked;
                objPK.VandongbinhthuongKhong = opt_vandongbinhthuong_khong.Checked;

                objPK.CamgiacnongsauCo = opt_camgiacnongsau_co.Checked;
                objPK.CamgiacnongsauKhong = opt_camgiacnongsau_khong.Checked;
                objPK.CamgiacnongsauMota = opt_camgiacnongsau_co.Checked ? Utility.sDbnull(txt_camgiacnongsau_mota.Text) : "";

                objPK.ChieudaichisovoibinhthuongBatthuong = opt_chieudaichisovoibinhthuong_batthuong.Checked;
                objPK.ChieudaichisovoibinhthuongBinhthuong = opt_chieudaichisovoibinhthuong_binhthuong.Checked;
                objPK.ChieudaichisovoibinhthuongMota = opt_chieudaichisovoibinhthuong_batthuong.Checked ? Utility.sDbnull(txt_chieudaichisovoibinhthuong_mota.Text) : "";

                objPK.BiendokhopBatthuong = opt_biendokhop_batthuong.Checked;
                objPK.BiendokhopBinhthuong = opt_biendokhop_binhthuong.Checked;
                objPK.BiendokhopMota = opt_biendokhop_batthuong.Checked ? Utility.sDbnull(txt_biendokhop_mota.Text) : "";

                objPK.CoxuongkhopKhac = Utility.sDbnull(txt_coxuongkhop_khac.Text);




            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_coxuongkhop_batthuong_CheckedChanged(object sender, EventArgs e)
        {
           Utility.EnableAndFocus(txt_coxuongkhop_mota, sender as RadioButton);
           
        }

        private void opt_dauco_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_biendangxuong_mota, sender as RadioButton);
           
        }

        
       

       


        private void opt_daucotsong_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_camgiacnongsau_mota, sender as RadioButton);
           
        }

        private void opt_teoco_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_chieudaichisovoibinhthuong_mota, sender as RadioButton);
           
        }

        private void opt_vetthuong_co_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void opt_cxk_bamtim_co_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_cxk_bamtim_mota, sender as RadioButton);
        }

        private void opt_biendokhop_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_biendokhop_mota, sender as RadioButton);
        }
    }
}
