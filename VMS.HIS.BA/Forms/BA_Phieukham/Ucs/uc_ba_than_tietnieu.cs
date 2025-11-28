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
    public partial class uc_ba_than_tietnieu : UserControl
    {
        public uc_ba_than_tietnieu()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_thantietnieu_batthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieuBatthuong);
                opt_thantietnieu_binhthuong.Checked = Utility.Bool2Bool(objPK.ThantietnieuBinhthuong);
                txt_thantietnieu_mota.Text = Utility.sDbnull(objPK.ThantietnieuMota);

                opt_mausacnuoctieu_batthuong.Checked = Utility.Bool2Bool(objPK.MausacnuoctieuBatthuong);
                opt_mausacnuoctieu_binhthuong.Checked = Utility.Bool2Bool(objPK.MausacnuoctieuBinhthuong);
                txt_mausacnuoctieu_mausac.Text = Utility.sDbnull(objPK.MausacnuoctieuMausac);
                nmr_mausacnuoctieu_thetich.Value= Utility.DecimaltoDbnull(objPK.MausacnuoctieuThetich);
              
                opt_tieubuot_co.Checked = Utility.Bool2Bool(objPK.TieubuotCo);
                opt_tieubuot_khong.Checked = Utility.Bool2Bool(objPK.TieubuotKhong);

                opt_tieurat_co.Checked = Utility.Bool2Bool(objPK.TieuratCo);
                opt_tieurat_khong.Checked = Utility.Bool2Bool(objPK.TieuratKhong);
               
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
                objPK.ThantietnieuBatthuong = opt_thantietnieu_batthuong.Checked;
                objPK.ThantietnieuBinhthuong = opt_thantietnieu_binhthuong.Checked;
                objPK.ThantietnieuMota = opt_thantietnieu_batthuong.Checked?Utility.sDbnull(txt_thantietnieu_mota.Text):"";

                objPK.MausacnuoctieuBatthuong = opt_mausacnuoctieu_batthuong.Checked;
                objPK.MausacnuoctieuBinhthuong = opt_mausacnuoctieu_binhthuong.Checked;
                objPK.MausacnuoctieuMausac = opt_mausacnuoctieu_batthuong.Checked?Utility.sDbnull(txt_mausacnuoctieu_mausac.Text):"";
                objPK.MausacnuoctieuThetich = opt_mausacnuoctieu_batthuong.Checked?Utility.Int32Dbnull( nmr_mausacnuoctieu_thetich.Value):0;

                objPK.TieubuotCo = opt_tieubuot_co.Checked;
                objPK.TieubuotKhong = opt_tieubuot_khong.Checked;

                objPK.TieuratCo = opt_tieurat_co.Checked;
                objPK.TieuratKhong = opt_tieurat_khong.Checked;

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

        private void opt_mausacnuoctieu_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_mausacnuoctieu_mausac.Enabled = nmr_mausacnuoctieu_thetich.Enabled= _obj.Checked;
            if (_obj.Checked) txt_mausacnuoctieu_mausac.Focus();
        }
    }
}
