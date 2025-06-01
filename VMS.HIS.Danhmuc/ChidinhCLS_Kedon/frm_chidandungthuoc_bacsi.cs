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
using SubSonic;
namespace VMS.HIS.Danhmuc.ChidinhCLS_Kedon
{
    public partial class frm_chidandungthuoc_bacsi : Form
    {
        int id_donthuocmau = -1;
        public frm_chidandungthuoc_bacsi()
        {
            InitializeComponent();
            this.KeyDown += frm_chidandungthuoc_bacsi_KeyDown;
            grdDetail.ColumnButtonClick += grdDetail_ColumnButtonClick;
        }

        void grdDetail_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                 int num=   new Delete().From(DmucChidanKedonthuoc.Schema)
                     .Where(DmucChidanKedonthuoc.Columns.Id).IsEqualTo(Utility.Int64Dbnull(grdDetail.GetValue("id"), -1))
                     .And(DmucChidanKedonthuoc.Columns.IdBacsi).IsEqualTo(globalVariables.gv_intIDNhanvien)
                     .Execute();
                 if (num > 0)
                 {
                     grdDetail.CurrentRow.Delete();
                     grdDetail.Refetch();
                 }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void frm_chidandungthuoc_bacsi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdExit.PerformClick();
        }

        private void frm_chidandungthuoc_bacsi_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Danh sách chỉ dẫn dùng thuốc của bác sĩ: "+globalVariables.gv_strTenNhanvien;
              DataTable  m_dtChitiet = SPs.ThuocLaydsChidandungthuoctheobacsi(globalVariables.gv_intIDNhanvien,Utility.Bool2byte( globalVariables.IsAdmin)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDetail, m_dtChitiet, false, true, "1=1", DmucThuoc.Columns.TenThuoc);
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
