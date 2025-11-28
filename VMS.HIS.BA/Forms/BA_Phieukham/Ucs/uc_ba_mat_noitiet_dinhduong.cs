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
    public partial class uc_ba_mat_noitiet_dinhduong : UserControl
    {
        public uc_ba_mat_noitiet_dinhduong()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_mat_binhthuong.Checked = Utility.Bool2Bool(objPK.MatBinhthuong);
                opt_mat_batthuong.Checked = Utility.Bool2Bool(objPK.MatBatthuong);
                txt_mat_ghiro.Text = Utility.sDbnull(objPK.MatGhiro);
                txt_mat_khac.Text = Utility.sDbnull(objPK.MatKhac);

                opt_noitiet_batthuong.Checked = Utility.Bool2Bool(objPK.NoitietBatthuong);
                opt_noitiet_binhthuong.Checked = Utility.Bool2Bool(objPK.NoitietBinhthuong);
                txt_noitiet_mota.Text = Utility.sDbnull(objPK.NoitietMota);

                opt_anuong_binhthuong.Checked = Utility.Bool2Bool(objPK.AnuongBinhthuong);
                opt_anuong_kem.Checked = Utility.Bool2Bool(objPK.AnuongKem);
                opt_anuong_khong.Checked = Utility.Bool2Bool(objPK.AnuongKhong);
               
                txt_noitiet_dinhduong_khac.Text = Utility.sDbnull(objPK.NoitietDinhduongKhac);


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
                objPK.MatBinhthuong = opt_mat_binhthuong.Checked;
                objPK.MatBatthuong = opt_mat_batthuong.Checked;
                objPK.MatGhiro = opt_mat_batthuong.Checked? Utility.sDbnull(txt_mat_ghiro.Text):"";
                objPK.MatKhac = Utility.sDbnull(txt_mat_khac.Text);

                objPK.NoitietBatthuong = opt_noitiet_batthuong.Checked;
                objPK.NoitietBinhthuong = opt_noitiet_binhthuong.Checked;
                objPK.NoitietMota = opt_noitiet_batthuong.Checked?Utility.sDbnull(txt_noitiet_mota.Text):"";

                objPK.AnuongBinhthuong = opt_anuong_binhthuong.Checked;
                objPK.AnuongKem = opt_anuong_kem.Checked;
                objPK.AnuongKhong = opt_anuong_khong.Checked;

                objPK.NoitietDinhduongKhac = Utility.sDbnull(txt_noitiet_dinhduong_khac.Text);

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_mat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_mat_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_mat_ghiro.Focus();
        }

        private void opt_noitiet_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_noitiet_mota.Enabled = _obj.Checked;
            if (_obj.Checked) txt_noitiet_mota.Focus();
        }
    }
}
