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
    public partial class uc_ba_taimuihong : UserControl
    {
        public uc_ba_taimuihong()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_tai_binhthuong.Checked = Utility.Bool2Bool(objPK.TaiBinhthuong);
                opt_tai_batthuong.Checked = Utility.Bool2Bool(objPK.TaiBatthuong);
                txt_tai_ghiro.Text = Utility.sDbnull(objPK.TaiGhiro);

                opt_mui_binhthuong.Checked = Utility.Bool2Bool(objPK.MuiBinhthuong);
                opt_mui_batthuong.Checked = Utility.Bool2Bool(objPK.MuiBatthuong);
                txt_mui_ghiro.Text = Utility.sDbnull(objPK.MuiGhiro);

                opt_hong_binhthuong.Checked = Utility.Bool2Bool(objPK.HongBinhthuong);
                opt_hong_batthuong.Checked = Utility.Bool2Bool(objPK.HongBatthuong);
                txt_hong_ghiro.Text = Utility.sDbnull(objPK.HongGhiro);

                opt_thanhquan_batthuong.Checked = Utility.Bool2Bool(objPK.ThanhquanBatthuong);
                opt_thanhquan_binhthuong.Checked = Utility.Bool2Bool(objPK.ThanhquanBinhthuong);
                txt_thanhquan_ghiro.Text = Utility.sDbnull(objPK.ThanhquanGhiro);

                txt_taimuihong_khac.Text = Utility.sDbnull(objPK.TaimuihongKhac);
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
                objPK.TaiBinhthuong = opt_tai_binhthuong.Checked;
                objPK.TaiBatthuong = opt_tai_batthuong.Checked;
                objPK.TaiGhiro = opt_tai_batthuong.Checked?Utility.sDbnull(txt_tai_ghiro.Text):"";

                objPK.MuiBinhthuong = opt_mui_binhthuong.Checked;
                objPK.MuiBatthuong = opt_mui_batthuong.Checked;
                objPK.MuiGhiro = opt_mui_batthuong.Checked? Utility.sDbnull(txt_mui_ghiro.Text):"";

                objPK.HongBinhthuong = opt_hong_binhthuong.Checked;
                objPK.HongBatthuong = opt_hong_batthuong.Checked;
                objPK.HongGhiro = opt_hong_batthuong.Checked? Utility.sDbnull(txt_hong_ghiro.Text):"";

                objPK.ThanhquanBatthuong = opt_thanhquan_batthuong.Checked;
                objPK.ThanhquanBinhthuong = opt_thanhquan_binhthuong.Checked;
                objPK.ThanhquanGhiro = opt_thanhquan_batthuong.Checked? Utility.sDbnull(txt_thanhquan_ghiro.Text):"";

                objPK.TaimuihongKhac = Utility.sDbnull(txt_taimuihong_khac.Text);


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_tai_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_tai_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_tai_ghiro.Focus();
        }

        private void opt_mui_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_mui_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_mui_ghiro.Focus();
        }

        private void opt_hong_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_hong_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_hong_ghiro.Focus();
        }

        private void opt_thanhquan_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_thanhquan_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_thanhquan_ghiro.Focus();
        }
    }
}
