using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VMS.HIS.Danhmuc.Goikham
{
    public partial class frm_tronggoi_tuychon : Form
    {
        DataTable dtData;
        public bool mv_blnCancel = true;
        public int id_dangky = 0;
        public frm_tronggoi_tuychon(DataTable dtData)
        {
            InitializeComponent();
            this.dtData = dtData;
            this.Load += Frm_tronggoi_tuychon_Load;
        }

        private void Frm_tronggoi_tuychon_Load(object sender, EventArgs e)
        {
            try
            {
                optTronggoi.Checked = true;
                DataBinding.BindDataCombobox(cboGoi, dtData, "id_dangky", "ten_goi");
                if (cboGoi.Items.Count == 1) cboGoi.SelectedIndex = 0;
            }
            catch (Exception)
            {

               
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (optTronggoi.Checked || (optGoi.Checked && cboGoi.SelectedIndex >= 0))
            {
                mv_blnCancel = false;
                id_dangky = optTronggoi.Checked ? 0 : Utility.Int32Dbnull(cboGoi.SelectedValue, 0);
                this.Close();
            }
            if(optGoi.Checked && Utility.Int32Dbnull(cboGoi.SelectedValue, 0)<=0)
            {
                Utility.ShowMsg("Bạn cần chọn gói muốn chuyển dịch vụ đang chọn vào.\nChú ý: Hệ thống chỉ cho phép chuyển các dịch vụ nằm trong cấu thành gói. Các dịch vụ phía ngoài bạn muốn không tính phí check vào lựa chọn 1");
                return;
            }    
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
