using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_saochepcongkham : Form
    {
       
       
        public frm_saochepcongkham()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
        }

        private void InitEvents()
        {
            Load += frm_saochepcongkham_Load;
            KeyDown += frm_saochepcongkham_KeyDown;
            cmdClose.Click += cmdClose_Click;
          
        }

        private void frm_saochepcongkham_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable dtPhongkham = THU_VIEN_CHUNG.LaydanhmucPhong(-1, "CAHAI", "PHONG");
                DataBinding.BindDataCombobox(cboPhongkhamNguon, dtPhongkham, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                DataBinding.BindDataCombobox(cboPhongkhamdich, dtPhongkham, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                cboPhongkhamNguon.Focus();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

      
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", true);

                if (Utility.Int32Dbnull(cboPhongkhamNguon.SelectedValue) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn phòng khám nguồn từ Bác sĩ đang làm việc để lấy dữ liệu cho phòng khám của Bác sĩ mới", true);
                    cboPhongkhamNguon.Focus();

                    return;
                }
                if (Utility.Int32Dbnull(cboPhongkhamdich.SelectedValue) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn phòng khám phòng khám của Bác sĩ mới để nhận dữ liệu sao chép từ phòng khám nguồn phía trên", true);
                    cboPhongkhamdich.Focus();

                    return;
                }
                int num = SPs.KcbCongkhamSaochep(Utility.Int16Dbnull(cboPhongkhamNguon.SelectedValue), Utility.Int16Dbnull(cboPhongkhamdich.SelectedValue)).Execute();
                Utility.ShowMsg("Sao chép công khám từ {0} sang {1} thành công. Nhấn OK để kết thúc");
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi" + ex.Message);
                //throw;
            }
        }

       
        private void frm_saochepcongkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            else if (e.Control &&(e.KeyCode == Keys.S || e.KeyCode==Keys.A) ) cmdChuyen.PerformClick();
            else if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

    }
}