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

namespace VMS.HIS.Danhmuc.ChidinhCLS_Kedon
{
    public partial class frm_chitietdonthuocmau : Form
    {
        int id_donthuocmau = -1;
        public frm_chitietdonthuocmau(int id_donthuocmau)
        {
            InitializeComponent();
            this.id_donthuocmau=id_donthuocmau;
            this.KeyDown += frm_chitietdonthuocmau_KeyDown;
        }

        void frm_chitietdonthuocmau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdExit.PerformClick();
        }

        private void frm_chitietdonthuocmau_Load(object sender, EventArgs e)
        {
            try
            {
              DataTable  m_dtChitiet = SPs.DmucLaychitietDonthuocmau(id_donthuocmau).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDetail, m_dtChitiet, false, true, "1=1",
                                                   "stt_hienthi," + DmucThuoc.Columns.TenThuoc);
                grdDetail.MoveFirst();
                cmdSave.Enabled = grdDetail.RowCount > 0;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy dữ liệu chi tiết đơn thuốc mẫu:\n" + ex.Message);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
