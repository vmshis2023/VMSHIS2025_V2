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
    public partial class uc_ba_ranghammat : UserControl
    {
        public uc_ba_ranghammat()
        {
            InitializeComponent();
        }
        public void ShowData(EmrPhieukhamNoikhoa objPK)
        {
            try
            {
                opt_ranghammat_batthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBatthuong);
                opt_ranghammat_binhthuong.Checked = Utility.Bool2Bool(objPK.RanghammatBinhthuong);
                txt_ranghammat_ghiro.Text = Utility.sDbnull(objPK.RanghammatGhiro);
                txt_ranghammat_khac.Text = Utility.sDbnull(objPK.RanghammatKhac);

                
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
                objPK.RanghammatBatthuong = opt_ranghammat_batthuong.Checked;
                objPK.RanghammatBinhthuong = opt_ranghammat_binhthuong.Checked;
                objPK.RanghammatGhiro = opt_ranghammat_batthuong.Checked? Utility.sDbnull(txt_ranghammat_ghiro.Text):"";
                objPK.RanghammatKhac = Utility.sDbnull(txt_ranghammat_khac.Text);



            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void opt_ranghammat_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _obj = sender as RadioButton;
            txt_ranghammat_ghiro.Enabled = _obj.Checked;
            if (_obj.Checked) txt_ranghammat_ghiro.Focus();
        }
    }
}
