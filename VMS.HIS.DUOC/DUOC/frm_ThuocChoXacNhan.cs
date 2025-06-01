using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ThuocChoXacNhan : Form
    {
        public int id_kho = -1;
        public int id_thuoc = -1;
        public int id_ThuocKho = -1;
        public DataTable m_dtChoXacNhan;
        public frm_ThuocChoXacNhan()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Utility.VisiableGridEx(grdList, "Id_ThuocKho",globalVariables.IsAdmin);
        }

        private void frm_ThuocChoXacNhan_Load(object sender, EventArgs e) 
        {

            m_dtChoXacNhan = SPs.ThuocTamkeXemtheoIdthuockho(id_ThuocKho, id_thuoc, id_kho).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtChoXacNhan,true,true,"1=1","");
        }

        private void cmdxoacho_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("thuoc_tamke_xoachoxacnhan"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa thuốc chờ xác nhận(thuoc_tamke_xoachoxacnhan).\nVui lòng liên hệ quản trị hệ thống để được cấp phép");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa số lượng kê đang chọn không?", "Thông báo", true))
                {
                    foreach (GridEXRow row in grdList.GetCheckedRows())
                    {
                        Int64 id = Utility.Int64Dbnull(row.Cells[TTamke.Columns.Id].Value, -1);
                        int idThuoc = Utility.Int32Dbnull(  row.Cells[TTamke.Columns.IdThuoc].Value, -1);
                        long idchitiet = Utility.Int64Dbnull(row.Cells[TTamke.Columns.IdPhieuCtiet].Value, -1);
                        long idphieu = Utility.Int64Dbnull(row.Cells[TTamke.Columns.IdPhieu].Value, -1);
                        int loaiPhieu = Utility.Int32Dbnull(row.Cells[TTamke.Columns.Loai].Value, -1);
                        new Delete().From(TTamke.Schema)
                            .Where(TTamke.Columns.Id).IsEqualTo(id)
                            //.Where(TTamke.Columns.IdPhieuCtiet).IsEqualTo(idchitiet)
                            //.And(TTamke.Columns.IdPhieu).IsEqualTo(idphieu)
                            //.And(TTamke.Columns.Loai).IsEqualTo(loaiPhieu)
                            //.And(TTamke.Columns.IdThuoc).IsEqualTo(idThuoc)
                            .Execute();
                        row.Delete();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chờ thuốc {0} của id phiếu chi tiết {1} - id phiếu={2} - Loại phiếu {3}- id tạm kê ={4}", idThuoc, idchitiet, idphieu, loaiPhieu, id), newaction.Delete, "UI");
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
