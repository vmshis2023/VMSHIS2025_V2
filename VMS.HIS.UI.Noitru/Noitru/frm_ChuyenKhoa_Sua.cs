using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Collections.Generic;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ChuyenKhoa_Sua : Form
    {
        public KcbLuotkham objLuotkham;
        string ten_benhnhan = "";
        public frm_ChuyenKhoa_Sua(KcbLuotkham objLuotkham,string ten_benhnhan)
        {
            InitializeComponent();
            this.ten_benhnhan = ten_benhnhan;
            this.objLuotkham = objLuotkham;
            Utility.SetVisualStyle(this);
            lblMsg.Text = string.Format(" Danh sách các khoa nội trú người bệnh {0} đã được điều trị trong đợt khám này", ten_benhnhan);
            this.KeyDown += frm_ChuyenKhoa_Sua_KeyDown;
        }

        void frm_ChuyenKhoa_Sua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter) cmdSave.PerformClick();
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable p_ThongTinLichSuBuongGiuong;
        private void frm_ChuyenKhoa_Sua_Load(object sender, EventArgs e)
        {
            try
            {
                //SqlQuery sqlQuery =
                //new Select(NoitruPhanbuonggiuong.Columns.IdKhoanoitru,
                //    DmucKhoaphong.Columns.TenKhoaphong,
                //    NoitruPhanbuonggiuong.Columns.IdBuong,
                //    NoitruPhanbuonggiuong.Columns.IdGiuong,
                //    NoitruPhanbuonggiuong.Columns.Id,
                //    "(select top 1 lb.ten_giuong from noitru_dmuc_giuongbenh as lb where lb.id_giuong=noitru_phanbuonggiuong.id_giuong)as ten_giuong," +
                //    "(select top 1 lb.ten_buong from noitru_dmuc_buong as lb where lb.id_buong=noitru_phanbuonggiuong.id_buong)as ten_buong" +
                //    ""
                //    ).From<NoitruPhanbuonggiuong>()
                //    .InnerJoin(DmucKhoaphong.IdKhoaphongColumn, NoitruPhanbuonggiuong.IdKhoanoitruColumn)
                //    .Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //    .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                //    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                //    .And(NoitruPhanbuonggiuong.Columns.TrangThai).IsEqualTo(1).And(NoitruPhanbuonggiuong.Columns.IdBuong).IsGreaterThan(0);

                DataSet dsData = SPs.NoitruChuyenkhoasuaDanhsachkhoadieutri(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet();
                p_ThongTinLichSuBuongGiuong=dsData.Tables[0];

                DataTable dtchuyenkhoasua = dsData.Tables[1];
                //NoitruChuyenkhoasuaCollection objChuyenKhoaSuaCollection =
                //    new Select().From<NoitruChuyenkhoasua>()
                //        .Where(NoitruChuyenkhoasua.Columns.MaLuotkham)
                //        .IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(NoitruChuyenkhoasua.Columns.IdBenhnhan)
                //        .IsEqualTo(objLuotkham.IdBenhnhan)
                //        .ExecuteAsCollection<NoitruChuyenkhoasuaCollection>();

                Utility.SetDataSourceForDataGridEx(grdList, p_ThongTinLichSuBuongGiuong, false, true, "1=1", "");

                foreach (Janus.Windows.GridEX.GridEXRow grdRow in grdList.GetDataRows())
                {
                    var query = from cp in dtchuyenkhoasua.AsEnumerable()
                                where
                                    Utility.Int32Dbnull(cp[NoitruChuyenkhoasua.Columns.IdKhoasua]) == Utility.Int32Dbnull(grdRow.Cells["id_khoanoitru"].Value)
                                select cp;
                    if (query.Any())
                    {
                        grdRow.BeginEdit();
                        grdRow.IsChecked = true;
                        grdRow.EndEdit();
                    }
                    else
                    {
                        grdRow.BeginEdit();
                        grdRow.IsChecked = false;
                        grdRow.EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                cmdSave.Enabled = grdList.RowCount > 0;
            }
            
          //  Utility.SetGridEXSortKey(grdList, NoitruPhanbuonggiuong.Columns.PatientDeptId, Janus.Windows.GridEX.SortOrder.Ascending);
        }
        /// <summary>
        /// hàm thực hiện việc cho phép xóa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("NOITRU_CHUYENKHOASUA"))
                {
                    Utility.ShowMsg("Bạn không được phân quyền chuyển khoa sửa. Vui lòng liên hệ bộ phận quản trị Hệ thống để được cấp quyền");
                    return;
                }
                List<string> lstKhoasua = (from p in grdList.GetCheckedRows().AsEnumerable()
                                           select p.Cells["ten_khoaphong"].Value.ToString()).Distinct().ToList<string>();
                string khoasua = String.Join(",", lstKhoasua.ToArray<string>());
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn chuyển người bệnh {0} về các khoa: {1} để sửa không\n Sau khi chuyển về các khoa trên, các bác sĩ tại các khoa đó có thể sửa được các y lệnh", ten_benhnhan, khoasua), "Thông báo", true))
                {
                    new Delete().From<NoitruChuyenkhoasua>()
                   .Where(NoitruChuyenkhoasua.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .And(NoitruChuyenkhoasua.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .Execute();
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                    {
                        NoitruChuyenkhoasua objChuyenKhoaSua = new NoitruChuyenkhoasua();
                        objChuyenKhoaSua.IdBuongGiuong = Utility.Int32Dbnull(gridExRow.Cells["id"].Value);
                        objChuyenKhoaSua.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                        objChuyenKhoaSua.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                        objChuyenKhoaSua.IdKhoasua = Utility.Int16Dbnull(gridExRow.Cells["id_khoanoitru"].Value);
                        objChuyenKhoaSua.IdBuong = Utility.Int16Dbnull(gridExRow.Cells["id_buong"].Value);
                        objChuyenKhoaSua.IdGiuong = Utility.Int16Dbnull(gridExRow.Cells["id_giuong"].Value);
                        objChuyenKhoaSua.NguoiTao = globalVariables.UserName;
                        objChuyenKhoaSua.NgàyTao = globalVariables.SysDate;
                        objChuyenKhoaSua.Save();
                    }
                    if (lstKhoasua.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Bạn đã thực hiện hủy quyền sửa thông tin điều trị của người bệnh {0} với toàn bộ các khoa khác.\nVui lòng thông báo đồng nghiệp tại các khoa bị hủy để biết. Nhấn OK để kết thúc", ten_benhnhan));
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Bạn đã thực hiện chuyển người bệnh {0} sang khoa {1} để các bác sĩ tại khoa trên có thể thực hiện các công việc chỉnh sửa phiếu điều trị (y lệnh). Vui lòng thông báo cho đồng nghiệp.", ten_benhnhan, khoasua));
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
           
        }

        private void cmdHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("NOITRU_HUYCHUYENKHOASUA"))
                {
                    Utility.ShowMsg("Bạn không được phân quyền Hủy chuyển khoa sửa. Vui lòng liên hệ bộ phận quản trị Hệ thống để được cấp quyền");
                    return;
                }
                List<string> lstKhoasua = (from p in grdList.GetCheckedRows().AsEnumerable()
                                           select p.Cells["ten_khoaphong"].Value.ToString()).Distinct().ToList<string>();
                string khoasua = String.Join(",", lstKhoasua.ToArray<string>());
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn hủy chuyển các khoa sửa không\nSau khi thực hiện các khoa {0} sẽ không thể sửa được thông tin điều trị của người bệnh", khoasua), "Thông báo", true))
                {

                    new Delete().From<NoitruChuyenkhoasua>()
                   .Where(NoitruChuyenkhoasua.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .And(NoitruChuyenkhoasua.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .Execute();
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                    {
                        NoitruChuyenkhoasua objChuyenKhoaSua = new NoitruChuyenkhoasua();
                        objChuyenKhoaSua.IdBuongGiuong = Utility.Int32Dbnull(gridExRow.Cells["id"].Value);
                        objChuyenKhoaSua.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                        objChuyenKhoaSua.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                        objChuyenKhoaSua.IdKhoasua = Utility.Int16Dbnull(gridExRow.Cells["id_khoanoitru"].Value);
                        objChuyenKhoaSua.IdBuong = Utility.Int16Dbnull(gridExRow.Cells["id_buong"].Value);
                        objChuyenKhoaSua.IdGiuong = Utility.Int16Dbnull(gridExRow.Cells["id_giuong"].Value);
                        objChuyenKhoaSua.NguoiTao = globalVariables.UserName;
                        objChuyenKhoaSua.NgàyTao = globalVariables.SysDate;
                        objChuyenKhoaSua.Save();
                    }
                    if (lstKhoasua.Count <= 0)
                    {
                        Utility.ShowMsg(string.Format("Bạn đã thực hiện hủy quyền sửa thông tin điều trị của người bệnh {0} với toàn bộ các khoa khác.\nVui lòng thông báo đồng nghiệp tại các khoa bị hủy để biết. Nhấn OK để kết thúc", ten_benhnhan));
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Bạn đã thực hiện hủy quyền sửa thông tin điều trị của người bệnh {0} với các khoa:\n{1}.\nVui lòng thông báo đồng nghiệp tại các khoa: {2} để biết. Nhấn OK để kết thúc", ten_benhnhan, khoasua, khoasua));
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
