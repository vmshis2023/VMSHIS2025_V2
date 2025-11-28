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
    public partial class uc_ba_than_tietnieu_ngoaikhoa : UserControl
    {
        public uc_ba_than_tietnieu_ngoaikhoa()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNgoaikhoa objPK)
        {
            try
            {
                opt_thantietnieu_batthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieuBatthuong);
                opt_thantietnieu_binhthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieuBinhthuong);
                txt_thantietnieu_mota.Text = Utility.sDbnull(objPK.ThantietnieuMota);

               
                opt_thanto_co.Checked = Utility.Bool2Bool(objPK.ThantoCo);
                opt_thanto_khong.Checked = Utility.Bool2Bool(objPK.ThantoKhong);

                opt_chamthan_co.Checked = Utility.Bool2Bool(objPK.ChamthanCo);
                opt_chamthan_khong.Checked = Utility.Bool2Bool(objPK.ChamthanKhong);

                opt_bapbenhthan_co.Checked = Utility.Bool2Bool(objPK.BapbenhthanCo);
                opt_bapbenhthan_khong.Checked = Utility.Bool2Bool(objPK.BapbenhthanKhong);

                opt_diemdaunieuquan_co.Checked = Utility.Bool2Bool(objPK.DiemdaunieuquanCo);
                opt_diemdaunieuquan_khong.Checked = Utility.Bool2Bool(objPK.DiemdaunieuquanKhong);

                opt_caubangquan_co.Checked = Utility.Bool2Bool(objPK.CaubangquanCo);
                opt_caubangquan_khong.Checked = Utility.Bool2Bool(objPK.CaubangquanKhong);
                
                opt_tinhhoan_binhthuong.Checked = Utility.Bool2Bool(objPK.TinhhoanBinhthuong);
                opt_tinhhoan_batthuong.Checked = Utility.Bool2Bool(objPK.TinhhoanBatthuong);
                txt_tinhhoan_mota.Text = Utility.sDbnull(objPK.TinhhoanMota);

                opt_tuyentienliet_binhthuong.Checked = Utility.Bool2Bool(objPK.TuyentienlietBinhthuong);
                opt_tuyentienliet_tochac.Checked = Utility.Bool2Bool(objPK.TuyentienlietTochac);
                opt_tuyentienliet_tomem.Checked = Utility.Bool2Bool(objPK.TuyentienlietTomem);

                txt_thantietnieu_khac.Text = Utility.sDbnull(objPK.ThantietnieuKhac);

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
                objPK.ThantietnieuBatthuong = opt_thantietnieu_batthuong.Checked;
                objPK.ThantietnieuBinhthuong = opt_thantietnieu_binhthuong.Checked;
                objPK.ThantietnieuMota = opt_thantietnieu_batthuong.Checked? Utility.sDbnull(txt_thantietnieu_mota.Text):"";

                objPK.ThantoCo = opt_thanto_co.Checked;
                objPK.ThantoKhong = opt_thanto_khong.Checked;

                objPK.ChamthanCo = opt_chamthan_co.Checked;
                objPK.ChamthanKhong = opt_chamthan_khong.Checked;

                objPK.BapbenhthanCo = opt_bapbenhthan_co.Checked;
                objPK.BapbenhthanKhong = opt_bapbenhthan_khong.Checked;

                objPK.DiemdaunieuquanCo = opt_diemdaunieuquan_co.Checked;
                objPK.DiemdaunieuquanKhong = opt_diemdaunieuquan_khong.Checked;

                objPK.CaubangquanCo = opt_caubangquan_co.Checked;
                objPK.CaubangquanKhong = opt_caubangquan_khong.Checked;

                objPK.TinhhoanBinhthuong = opt_tinhhoan_binhthuong.Checked;
                objPK.TinhhoanBatthuong = opt_tinhhoan_batthuong.Checked;
                objPK.TinhhoanMota = opt_tinhhoan_batthuong.Checked ? Utility.sDbnull(txt_tinhhoan_mota.Text) : "";

                objPK.TuyentienlietBinhthuong = opt_tuyentienliet_binhthuong.Checked;
                objPK.TuyentienlietTochac = opt_tuyentienliet_tochac.Checked;
                objPK.TuyentienlietTomem = opt_tuyentienliet_tomem.Checked;

                objPK.ThantietnieuKhac = Utility.sDbnull(txt_thantietnieu_khac.Text);


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_thantietnieu_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thantietnieu_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_thantietnieu_mota.Focus();
        }

        private void opt_tinhhoan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            Utility.EnableAndFocus(txt_tinhhoan_mota, sender as RadioButton);
        }
    }
}
