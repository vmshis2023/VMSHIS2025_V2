using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;
namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu_NoiTru
{
    public partial class frm_AddDetail_TraThuoc : Form
    {
        public DataTable p_PhieuTraKhoaVeKhoChiTiet;
        public DataTable m_dtPresDetail=new DataTable();
        public bool b_Cancel = false;
        public int Id_Kho { get; set; }
        public int Id_Khoa { get; set; }

        public frm_AddDetail_TraThuoc()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool invali()
        {
            if (grdList.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn 1 bản ghi để thực hiện thêm trả thuốc", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdChapNhan_Click(object sender, EventArgs e)
        {
            if (!invali())return;
            AcceptData();
            b_Cancel = true;
            this.Close();
        }

        private void AcceptData()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
            {
                int soluong = Utility.Int32Dbnull(gridExRow.Cells["SLUONG_SUA"].Value);
                if (soluong > 0)
                {
                    DataRow drv = p_PhieuTraKhoaVeKhoChiTiet.NewRow();
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Drug_Name"))
                        drv["Drug_Name"] = Utility.sDbnull(gridExRow.Cells["Drug_Name"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Content"))
                        drv["Content"] = Utility.sDbnull(gridExRow.Cells["Content"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Unit_Name"))
                        drv["Unit_Name"] = Utility.sDbnull(gridExRow.Cells["Unit_Name"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Manufacturers"))
                        drv["Manufacturers"] = Utility.sDbnull(gridExRow.Cells["Manufacturers"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Producer"))
                        drv["Producer"] = Utility.sDbnull(gridExRow.Cells["Producer"].Value);
                    drv[DPhieuTraKhoaVeKhoCt.Columns.IdDonThuoc] = Utility.Int32Dbnull(gridExRow.Cells["Pres_ID"].Value);
                    drv[DPhieuTraKhoaVeKhoCt.Columns.IdThuoc] = Utility.Int32Dbnull(gridExRow.Cells["Drug_ID"].Value);
                    drv[DPhieuTraKhoaVeKhoCt.Columns.IdDonThuocCt] = Utility.Int32Dbnull(gridExRow.Cells["PresDetail_ID"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("So_Luong"))
                        drv["So_Luong"] = Utility.Int32Dbnull(gridExRow.Cells["SLUONG_SUA"].Value);
                    if (p_PhieuTraKhoaVeKhoChiTiet.Columns.Contains("Quantity"))
                        drv["Quantity"] = Utility.Int32Dbnull(gridExRow.Cells["Quantity"].Value);
                    p_PhieuTraKhoaVeKhoChiTiet.Rows.Add(drv);
                }
            }
        }

        private void frm_AddDetail_TraThuoc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frm_AddDetail_TraThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F3)cmdChapNhan.PerformClick();
           // if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

        private void frm_AddDetail_TraThuoc_Load(object sender, EventArgs e)
        {
            TimKiem();
            foreach (DataRow dr in m_dtPresDetail.Rows)
            {
                var query = from thuoc in p_PhieuTraKhoaVeKhoChiTiet.AsEnumerable()
                    where
                        Utility.Int32Dbnull(thuoc[DPhieuTraKhoaVeKhoCt.Columns.IdDonThuocCt]) ==
                        Utility.Int32Dbnull(dr["PresDetail_ID"])
                    select thuoc;
                if (query.Any())
                {
                    dr.Delete();
                    m_dtPresDetail.AcceptChanges();
                }

            }
           
        }
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// 
        /// </summary>
        private void TimKiem()
        {
            DateTime tungay = chkByDate.Checked ? dtFromdate.Value : Convert.ToDateTime("01/01/1900");
            DateTime Denngay = chkByDate.Checked ? dtToDate.Value : Convert.ToDateTime("01/01/2900");
            m_dtPresDetail = SPs.DuocDanhSachDonThuocCT(Id_Kho, Id_Khoa, tungay, Denngay).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtPresDetail,true,true,"1=1","");
        }
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}
