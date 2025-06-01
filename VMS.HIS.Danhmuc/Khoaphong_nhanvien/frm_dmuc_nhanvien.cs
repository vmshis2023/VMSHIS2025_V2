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
using VNS.HIS.NGHIEPVU;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_nhanvien : Form
    {
        #region "Declare Variable (Level private)"
        private DataTable m_dtStaffList = new DataTable();
        private DataTable m_dtStaffType = new DataTable();
        private DataTable m_dtRank = new DataTable();

        #endregion
        #region "Contructor"
        public frm_dmuc_nhanvien()
        {
            try
            {
                InitializeComponent();
                Utility.SetVisualStyle(this);
                this.KeyDown += new KeyEventHandler(frm_dmuc_nhanvien_KeyDown);
                grdNhanvien.ApplyingFilter += new CancelEventHandler(grdStaffList_ApplyingFilter);
                grdNhanvien.SelectionChanged += new EventHandler(grdStaffList_SelectionChanged);
                grdNhanvien.FilterApplied += new EventHandler(grdStaffList_FilterApplied);
                m_dtStaffType = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAINHANVIEN", true);
                grdPhongkham.SelectionChanged += grdPhongkham_SelectionChanged;
                grdKhoa.SelectionChanged += grdKhoa_SelectionChanged;
                grdNhanvien.CellValueChanged += new ColumnActionEventHandler(grdStaffList_CellValueChanged);
                grdNhanvien.UpdatingCell += new UpdatingCellEventHandler(grdStaffList_UpdatingCell);
                grdNhanvien.CellUpdated += new ColumnActionEventHandler(grdStaffList_CellUpdated);
                txtLoaiDichvu._OnEnterMe += txtLoaiDichvu__OnEnterMe;
                txtLoaiDichvu._OnSelectionChanged += txtLoaiDichvu__OnSelectionChanged;
                optNo.CheckedChanged += optAll_CheckedChanged;
                optYes.CheckedChanged += optAll_CheckedChanged;
            }
            catch
            {
            }
        }

        void grdKhoa_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdKhoa))
            {
                m_dtPhongkham.DefaultView.RowFilter = "1=2";
                m_dtPhongkham.AcceptChanges();
            }
            else
            {
                m_dtPhongkham.DefaultView.RowFilter = "ma_cha=" + grdKhoa.GetValue("id_khoaphong").ToString();
                m_dtPhongkham.AcceptChanges();
            }
        }

        void grdPhongkham_SelectionChanged(object sender, EventArgs e)
        {
           
        }
        #endregion
        #region"Method of Event Form"
        /// <summary>
        /// hàm thực hiện dóng Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện load dữ liệu khi load thông tin Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_nhanvien_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                Timkiemdulieu();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        /// <summary>
        /// hàm dùng phím tắt của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_nhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
        }
        /// <summary>
        /// trạn thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled = grdNhanvien.RowCount > 0 && grdNhanvien.CurrentRow.RowType == RowType.Record;
                cmdDelete.Enabled = grdNhanvien.RowCount > 0 && grdNhanvien.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {


            }

        }
        /// <summary>
        /// sự kiện tìm kiếm khi nhấn tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {

            Timkiemdulieu();
            ModifyCommand();
        }

        private int v_StaffList_Id = -1;
        private bool IsValidData4Delete()
        {
            try
            {
                //v_StaffList_Id = Utility.Int32Dbnull(grdNhanvien.GetValue(DmucNhanvien.Columns.IdNhanvien), -1);
                //SqlQuery sqlQuery = new Select().From(KcbChidinhcl.Schema)
                //    .Where(KcbChidinhcl.Columns.IdBacsiChidinh).IsEqualTo(v_StaffList_Id);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                //    return false;
                //}
                //sqlQuery = new Select().From(KcbDonthuoc.Schema)
                //    .Where(KcbDonthuoc.Columns.IdBacsiChidinh).IsEqualTo(v_StaffList_Id);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                //    return false;
                //}
                //sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                // .Where(KcbDangkyKcb.Columns.IdBacsikham).IsEqualTo(v_StaffList_Id);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                //    return false;
                //}
                //sqlQuery = new Select().From(KcbThanhtoan.Schema)
                // .Where(KcbThanhtoan.Columns.IdNhanvienThanhtoan).IsEqualTo(v_StaffList_Id);
                DataTable dtData = SPs.NhanvienCheckDelete(Utility.Int16Dbnull(grdNhanvien.GetValue(DmucNhanvien.Columns.IdNhanvien), -1)).GetDataSet().Tables[0];
                if (dtData.Rows.Count > 0)
                {
                    Utility.ShowMsg("Nhân viên này đã được sử dụng nên bạn không thể xóa");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidData4Delete()) return;
                //if (Utility.AcceptQuestion("Bạn có muốn xóa nhân viên đang chọn không", "Thông báo", true))
                //{
                foreach (GridEXRow row in grdNhanvien.GetCheckedRows())
                {
                    v_StaffList_Id = Utility.Int32Dbnull(row.Cells[DmucNhanvien.Columns.IdNhanvien].Value, -1);
                    string errMsg = dmucnhanvien_busrule.Delete(v_StaffList_Id);
                    if (errMsg == string.Empty)
                    {
                        string noidung = string.Format("Xóa nhân viên với mã={0}, tên={1}, UID={2}", Utility.sDbnull(row.Cells["ma_nhanvien"].Value, ""), Utility.sDbnull(row.Cells["ten_nhanvien"].Value, ""), Utility.sDbnull(row.Cells["user_name"].Value, ""));
                        Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        row.Delete();
                    }
                    else
                    {
                        Utility.ShowMsg(errMsg);
                    }
                }

                //}
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        void Loaddmucchietkhau()
        {
            try
            {
                v_StaffList_Id = Utility.Int32Dbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.IdNhanvien].Value, -1);
                QheBacsiDichvuclCollection lstQheBacsiDichvucl = new Select().From(QheBacsiDichvucl.Schema)
               .Where(QheBacsiDichvucl.Columns.IdNhanvien).IsEqualTo(v_StaffList_Id)
               .ExecuteAsCollection<QheBacsiDichvuclCollection>();
                foreach (Janus.Windows.GridEX.GridEXRow dvu in grdDvu.GetDataRows())
                {
                    dvu.BeginEdit();

                    var query = from dv in lstQheBacsiDichvucl.AsEnumerable()
                                where dv.IdChitietdichvu == Utility.Int16Dbnull(dvu.Cells["id_chitietdichvu"].Value, -1)
                                select dv;
                    if (query.Count() > 0)
                    {
                        dvu.IsChecked = true;
                        dvu.Cells["ptram_ckhau"].Value = query.FirstOrDefault().PtramCkhau;
                    }

                    else
                    {
                        dvu.IsChecked = false;
                        dvu.Cells["ptram_ckhau"].Value = 0;
                    }

                    dvu.EndEdit();

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
        private void grdStaffList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdNhanvien)) return;
            if (grdNhanvien.CurrentRow != null)
            {
                LoadQuanHeNhanVienKho();
                
                LoadQuanHeNhanVienQuyen();
                LoadQuanHeNhanVienBaocaoMulti();
                LoadQheBS_khoanoitru();
                LoadQheBS_khoangoaitru();
                LoadQheLoaithuoc();
                LoadQheDichvuCLS();
                LoadQuanHeNhanVienDmucchung();
                LoadQuanHeNhanVienCosoKCB();
                Loaddmucchietkhau();
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện thêm mới nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            frm_themmoi_nhanvien frm = new frm_themmoi_nhanvien();
            frm.em_Action = action.Insert;
            frm.p_dtStaffList = m_dtStaffList;
            frm.ShowDialog();
            ModifyCommand();
        }
        /// <summary>
        /// sửa thông tin của phần nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdNhanvien)) return;
            if (v_StaffList_Id <= -1) return;
            frm_themmoi_nhanvien frm = new frm_themmoi_nhanvien();
            frm.em_Action = action.Update;
            frm.txtID.Text = v_StaffList_Id.ToString();
            frm.UserName = Utility.sDbnull(grdNhanvien.GetValue("user_name"));
            frm.p_dtStaffList = m_dtStaffList;
            frm.ShowDialog();
            ModifyCommand();
        }

        #endregion

        #region "Method of Common Form"
        DataTable dtDvu = new DataTable();
        DataTable m_dtPhongkham = new DataTable();
        private DataTable m_dtDepartment = new DataTable();
        private DataTable m_dtPhongkhamNoi = new DataTable();
        private void InitData()
        {
            try
            {
                m_dtDepartment = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", -1);
                DataBinding.BindDataCombox(cboStaffType, m_dtStaffType, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "--- Chọn kiểu nhân viên---", true);
                DataBinding.BindDataCombox(cboParent, m_dtDepartment, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "--- Khoa phòng---", true);

              DataTable  m_dtDichvuCLS = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
              txtLoaiDichvu.Init(m_dtDichvuCLS, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });


                dtDvu = SPs.DmucLaydanhmucDichvuclsChitiet(1, -1).GetDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx(grdDvu, dtDvu, true, true, chkDisplayAll.Checked ? "1=1" : "tinh_chkhau=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");

                //Khởi tạo danh mục phòng ban

               DataTable m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1, "ALL", "ALL", "ALL", 100, 100, 1);//  CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCVaTuThuoc();
               Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, true, true, "1=1", TDmucKho.Columns.SttHthi);
                m_dtPhongkham = THU_VIEN_CHUNG.LaydanhmucPhong(0, "NGOAI", "PHONG");
                Utility.SetDataSourceForDataGridEx(grdPhongkham, m_dtPhongkham, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);
                m_dtPhongkhamNoi = THU_VIEN_CHUNG.LaydanhmucPhong(0, "NOI", "PHONG");
                Utility.SetDataSourceForDataGridEx(grdPhongkhamNoitru, m_dtPhongkhamNoi, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

                DataTable m_dtKhoangoaitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                Utility.SetDataSourceForDataGridEx(grdKhoa, m_dtKhoangoaitru, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

                DataTable m_dtKhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                Utility.SetDataSourceForDataGridEx(grdKhoanoitru, m_dtKhoanoitru, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);


                QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "select MultiReport_ID as ma, MultiReport_Name as ten,MultiReport_Type as Loai,MultiReport_Sequence as stt_hthi,(select TOP 1 TEN from dmuc_chung where LOAI='LOAI_BAOCAOMULTI' AND MA=p.MultiReport_Type) as ten_loai from sys_multi_report p where p.trang_thai=1";
                DataTable dtbaocaomulti = DataService.GetDataSet(cmd).Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdbaocaomulti, dtbaocaomulti, true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);
                DataTable dtQuyen = SPs.DmucQuyennguoidungGetdata(globalVariables.UserName).GetDataSet().Tables[0];// new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("QUYENNHANVIEN").ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdQuyen, dtQuyen, true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);

                Utility.SetDataSourceForDataGridEx_Basic(grdLoaiThuoc, SPs.DmucLaydanhsachLoaithuoc("-1").GetDataSet().Tables[0], true, true, "1=1", "stt_nhomthuoc," + DmucLoaithuoc.Columns.SttHthi + "," + DmucLoaithuoc.Columns.TenLoaithuoc);
                Utility.SetDataSourceForDataGridEx_Basic(grdDichvuCls, new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("LOAIDICHVUCLS").ExecuteDataSet().Tables[0], true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);
                DataTable dtDmucchung = new Select().From(DmucKieudmuc.Schema).ExecuteDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx_Basic(grdDmucchung, dtDmucchung, true, true, "1=1", DmucKieudmuc.Columns.TenLoai);
                DataTable dtcosokcb = THU_VIEN_CHUNG.LayDulieuDanhmucChung("COSOKCB", true);

                Utility.SetDataSourceForDataGridEx_Basic(grdCosoKCB, dtcosokcb, true, true, "1=1", DmucChung.Columns.SttHthi);
            }
            catch
            {
            }
        }
        #region "Dữ liệu quan hệ"
        private void LoadQheLoaithuoc()
        {
            QheNhanvienDanhmucCollection lstQhenhanviendanhmucthuoc = new Select().From(QheNhanvienDanhmuc.Schema)
                .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")))
                .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(1)
                .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdLoaiThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQhenhanviendanhmucthuoc.AsEnumerable()
                            where kho.IdDichvu == Utility.sDbnull(gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheDichvuCLS()
        {
            QheNhanvienDanhmucCollection lstQhenhanviendanhmucdichvucls = new Select().From(QheNhanvienDanhmuc.Schema)
               .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")))
               .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(0)
               .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDichvuCls.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQhenhanviendanhmucdichvucls.AsEnumerable()
                            where kho.IdDichvu == Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheBS_khoanoitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphong = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(1)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkhamNoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                            where kho.IdPhong == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheBS_khoangoaitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphongCollection = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(0)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoa.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdPhong == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienBaocaoMulti()
        {
            QheNhanvienBaocaomultiCollection LstQheNhanvienBaocaomulti = new Select().From(QheNhanvienBaocaomulti.Schema)
                .Where(QheNhanvienBaocaomulti.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien"))).
                ExecuteAsCollection<QheNhanvienBaocaomultiCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdbaocaomulti.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienBaocaomulti.AsEnumerable()
                            where kho.IdBaocao == Utility.Int32Dbnull(gridExRow.Cells["ma"].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienDmucchung()
        {
            QheNhanvienDmucchungCollection LstQheNhanvienDmucchung = new Select().From(QheNhanvienDmucchung.Schema)
                .Where(QheNhanvienDmucchung.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien"))).
                ExecuteAsCollection<QheNhanvienDmucchungCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucchung.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienDmucchung.AsEnumerable()
                            where kho.Loai == Utility.sDbnull(gridExRow.Cells["ma_loai"].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienQuyen()
        {
            QheNhanvienQuyensudungCollection LstQheNhanvienQuyensudung = new Select().From(QheNhanvienQuyensudung.Schema)
                .Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien"))).
                ExecuteAsCollection<QheNhanvienQuyensudungCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuyen.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienQuyensudung.AsEnumerable()
                            where kho.Ma == Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienCosoKCB()
        {
            try
            {
                QheNhanvienCosoCollection LstQheNhanvienCoso = new Select().From(QheNhanvienCoso.Schema)
              .Where(QheNhanvienCoso.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien"))).
              ExecuteAsCollection<QheNhanvienCosoCollection>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdCosoKCB.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    var query = from kho in LstQheNhanvienCoso.AsEnumerable()
                                where kho.MaCoso == Utility.sDbnull(gridExRow.Cells["ma"].Value)
                                select kho;
                    if (query.Count() > 0)
                    {
                        gridExRow.IsChecked = true;
                    }

                    else
                    {
                        gridExRow.IsChecked = false;
                    }
                    gridExRow.EndEdit();

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
        }
        private void LoadQuanHeNhanVienKho()
        {
            QheNhanvienKhoCollection objNhanvienKhoCollection = new Select().From(QheNhanvienKho.Schema)
                .Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien"))).
                ExecuteAsCollection<QheNhanvienKhoCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in objNhanvienKhoCollection.AsEnumerable()
                            where kho.IdKho == Utility.Int32Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.Cells["IsChon"].Value = 1;
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.Cells["IsChon"].Value = 0;
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }

    /// <summary>
    /// test version
    /// </summary>
    /// <param name="objDmucNhanvien"></param>
    /// <returns></returns>
        private QheNhanvienKhoCollection GetQuanheNhanVienKho(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienKhoCollection lst = new QheNhanvienKhoCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
            {
                QheNhanvienKho objDNhanvienKho = new QheNhanvienKho();
                objDNhanvienKho.IdKho = Utility.Int16Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value);
                objDNhanvienKho.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objDNhanvienKho.IsNew = true;
                lst.Add(objDNhanvienKho);
            }
            return lst;
        }
        private QheNhanvienDmucchungCollection GetQuanheNhanVienDmucchung(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienDmucchungCollection lst = new QheNhanvienDmucchungCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucchung.GetCheckedRows())
            {
                QheNhanvienDmucchung objQheNhanvienDmucchung = new QheNhanvienDmucchung();
                objQheNhanvienDmucchung.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDmucchung.Loai = Utility.sDbnull(gridExRow.Cells["ma_loai"].Value, "ERR");
                objQheNhanvienDmucchung.IsNew = true;
                lst.Add(objQheNhanvienDmucchung);
            }
            return lst;
        }
        private QheNhanvienCosoCollection GetQheNhanvienCoso(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienCosoCollection lst = new QheNhanvienCosoCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdCosoKCB.GetCheckedRows())
            {
                QheNhanvienCoso objQheNhanvienCoso = new QheNhanvienCoso();
                objQheNhanvienCoso.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienCoso.MaCoso = Utility.sDbnull(gridExRow.Cells["ma"].Value, "ERR");
                objQheNhanvienCoso.IsNew = true;
                lst.Add(objQheNhanvienCoso);
            }
            return lst;
        }
        private QheNhanvienBaocaomultiCollection GetQuanheNhanVienBaocaomulti(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienBaocaomultiCollection lst = new QheNhanvienBaocaomultiCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdbaocaomulti.GetCheckedRows())
            {
                QheNhanvienBaocaomulti objQheNhanvienBaocaomulti = new QheNhanvienBaocaomulti();
                objQheNhanvienBaocaomulti.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienBaocaomulti.IdBaocao = Utility.Int32Dbnull(gridExRow.Cells["ma"].Value, -1);
                objQheNhanvienBaocaomulti.IsNew = true;
                lst.Add(objQheNhanvienBaocaomulti);
            }
            return lst;
        }
        private QheNhanvienQuyensudungCollection GetQuanheNhanVienQuyen(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienQuyensudungCollection lst = new QheNhanvienQuyensudungCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuyen.GetCheckedRows())
            {
                QheNhanvienQuyensudung objQheNhanvienQuyensudung = new QheNhanvienQuyensudung();
                objQheNhanvienQuyensudung.Ma = Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].Value);
                objQheNhanvienQuyensudung.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienQuyensudung.Loai = Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Loai].Value);
                objQheNhanvienQuyensudung.IsNew = true;
                lst.Add(objQheNhanvienQuyensudung);
            }
            return lst;
        }
        private QheBacsiKhoaphongCollection GetQuanheBsi_khoaphong(DmucNhanvien objDmucNhanvien)
        {
            QheBacsiKhoaphongCollection lst = new QheBacsiKhoaphongCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = -1;
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 0;
                objQheBacsiKhoaphong.IdPhong = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkhamNoitru.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            return lst;
        }
        private QheNhanvienDanhmucCollection GetQheNhanvienDanhmuc(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienDanhmucCollection lst = new QheNhanvienDanhmucCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdLoaiThuoc.GetCheckedRows())
            {
                QheNhanvienDanhmuc objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu = Utility.sDbnull(gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 1;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDichvuCls.GetCheckedRows())
            {
                QheNhanvienDanhmuc objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu = Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 0;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }
        #endregion
        private void Timkiemdulieu()
        {
            try
            {

                SqlQuery _sqlquery = new Select().From(VDmucNhanvien.Schema);
                if (Utility.Int32Dbnull(cboDepartment.SelectedValue, -1) != -1)
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.IdPhong).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.IdPhong).IsEqualTo(Utility.Int32Dbnull(cboDepartment.SelectedValue, -1));

                if (Utility.Int32Dbnull(cboParent.SelectedValue, -1) != -1)
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.IdKhoa).IsEqualTo(Utility.Int32Dbnull(cboParent.SelectedValue, -1));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.IdKhoa).IsEqualTo(Utility.Int32Dbnull(cboParent.SelectedValue, -1));


                if (Utility.sDbnull(cboStaffType.SelectedValue, "-1") != "-1")
                    if (_sqlquery.HasWhere)
                        _sqlquery.Where(VDmucNhanvien.Columns.MaLoainhanvien).IsEqualTo(Utility.sDbnull(cboStaffType.SelectedValue, "-1"));
                    else
                        _sqlquery.And(VDmucNhanvien.Columns.MaLoainhanvien).IsEqualTo(Utility.sDbnull(cboStaffType.SelectedValue, "-1"));

                //if (chknoUID.Checked)
                //    if (_sqlquery.HasWhere)
                //        _sqlquery.Where(VDmucNhanvien.Columns.UserName).IsEqualTo("");
                //    else
                //        _sqlquery.And(VDmucNhanvien.Columns.UserName).IsEqualTo("");
                //else
                //    if (_sqlquery.HasWhere)
                //        _sqlquery.Where(VDmucNhanvien.Columns.UserName).IsNotEqualTo("");
                //    else
                //        _sqlquery.And(VDmucNhanvien.Columns.UserName).IsNotEqualTo("");
                m_dtStaffList = _sqlquery.ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdNhanvien, m_dtStaffList, true, true, "1=1", "");
                ModifyCommand();
            }
            catch
            {
            }
        }

        #endregion

        void txtLoaiDichvu__OnSelectionChanged()
        {
            try
            {
                dtDvu = SPs.DmucLaydanhmucDichvuclsChitiet(1, Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDvu, dtDvu, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
                grdStaffList_SelectionChanged(grdNhanvien, new EventArgs());
            }
            catch
            {
            }
        }

        void txtLoaiDichvu__OnEnterMe()
        {
            try
            {
                dtDvu = SPs.DmucLaydanhmucDichvuclsChitiet(1, Utility.Int32Dbnull(txtLoaiDichvu.MyID, 0)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDvu, dtDvu, true, true, "id_cha<=0", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
                grdStaffList_SelectionChanged(grdNhanvien, new EventArgs());
            }
            catch
            {
            }
        }
        private void grdStaffList_FilterApplied(object sender, EventArgs e)
        {

            ModifyCommand();
        }



        private void cboParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable mDataTable =
                    THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboParent.SelectedValue, -1), -1);
                DataBinding.BindDataCombox(cboDepartment, mDataTable, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Chọn phòng---", true);
            }
            catch
            {
            }
        }

        private void grdStaffList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void cmdReportAsUser_Click(object sender, EventArgs e)
        {
            ReportAsUserName();
        }

        private void ReportAsUserName()
        {
            //if(grdStaffList.CurrentRow!=null)
            //{
            //    v_StaffList_Id = Utility.Int32Dbnull(grdStaffList.CurrentRow.Cells["Staff_ID"].Value, -1);
            //    DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(v_StaffList_Id);
            //    if (string.IsNullOrEmpty(objDmucNhanvien.UserName))
            //    {
            //        Utility.ShowMsg("Nhân viên này chưa được gán User,Bạn xem lại ","Thông báo",MessageBoxIcon.Warning);
            //        return;
            //    }
            //    if(objDmucNhanvien!=null)
            //    {
            //        frm_SysTrinhKyTheoUser frm = new frm_SysTrinhKyTheoUser();
            //        frm.objStaff = objDmucNhanvien;
            //        frm.ShowDialog();
            //    }
            //    ModifyCommand();
            //}
        }

        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }
        /// <summary>
        /// Sửa thông tin trực tiếp trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdStaffList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                grdNhanvien.UpdateData();
                grdNhanvien.Refresh();
                m_dtStaffList.AcceptChanges();
                int record = new Update(DmucNhanvien.Schema)
                 .Set(DmucNhanvien.Columns.TenNhanvien).EqualTo(Utility.sDbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.TenNhanvien].Value, ""))
                 .Set(DmucNhanvien.Columns.MaNhanvien).EqualTo(Utility.sDbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.MaNhanvien].Value, ""))
                 .Set(DmucNhanvien.Columns.MotaThem).EqualTo(Utility.sDbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.MotaThem].Value, ""))
                 .Set(DmucNhanvien.Columns.TrangThai).EqualTo(Utility.Int16Dbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.TrangThai].Value))
                 .Set(DmucNhanvien.Columns.PtramCkhau).EqualTo(Utility.ByteDbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.PtramCkhau].Value, 0))
                 .Set(DmucNhanvien.Columns.ChiphiCungthuchien).EqualTo(Utility.DecimaltoDbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.ChiphiCungthuchien].Value, 0))
                 .Where(DmucNhanvien.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.IdNhanvien].Value, -1)).Execute();

                if (record > 0)
                {
                    grdNhanvien.UpdateData();
                    grdNhanvien.Refresh();
                    m_dtStaffList.AcceptChanges();
                    //Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void grdStaffList_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                var objValue = new object();

                if (e.Column.Key == DmucNhanvien.Columns.MaNhanvien)
                {
                    objValue = e.Value;
                    SqlQuery q = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.MaNhanvien).IsEqualTo(Utility.sDbnull(objValue)).And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(grdNhanvien.CurrentRow.Cells[DmucNhanvien.Columns.MaNhanvien].Value, -1));
                    if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Mã nhân viên không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                    }
                    if (q.GetRecordCount() > 0)
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Đã tồn tại mã nhân viên", "Thông báo tồn tại", MessageBoxIcon.Warning);
                    }
                }
                else if (e.Column.Key == DmucNhanvien.Columns.TenNhanvien)
                {
                    objValue = e.Value;
                    if (string.IsNullOrEmpty(objValue.ToString().Trim()))
                    {
                        e.Cancel = true;
                        Utility.ShowMsg("Tên nhân viên không được để trống", "Thông báo thiếu thông tin", MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {
            }
        }

        private void grdStaffList_CellValueChanged(object sender, ColumnActionEventArgs e)
        {

        }

        private void grdStaffList_DoubleClick(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdNhanvien)) return;
            if (v_StaffList_Id <= -1) return;
            frm_themmoi_nhanvien frm = new frm_themmoi_nhanvien();
            frm.em_Action = action.Update;
            frm.txtID.Text = v_StaffList_Id.ToString();
            frm.UserName = Utility.sDbnull(grdNhanvien.GetValue("user_name"));
            frm.p_dtStaffList = m_dtStaffList;
            frm.ShowDialog();
            ModifyCommand();
        }

        private void chkDisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            Utility.SetDataSourceForDataGridEx(grdDvu, dtDvu, true, true, chkDisplayAll.Checked ? "1=1" : "tinh_chkhau=1", "stt_hthi_loaidvu,stt_hthi_dichvu,stt_hthi,ten_chitietdichvu");
        }

        private void cmdLaytile_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow r in grdDvu.GetDataRows())
            {
                r.BeginEdit();
                r.Cells["ptram_ckhau"].Value = Utility.getValueOfGridCell(grdNhanvien, "ptram_ckhau");
                r.EndEdit();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn cập nhật lại % chiết khấu của các dịch vụ cho các bác sĩ đang chọn?", "Xác nhận", true))
                {
                    using (var scope = new System.Transactions.TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            if (grdNhanvien.GetCheckedRows().Count() <= 0)
                            {
                                grdNhanvien.CurrentRow.BeginEdit();
                                grdNhanvien.CurrentRow.IsChecked = true;
                                grdNhanvien.CurrentRow.EndEdit();
                            }
                            foreach (GridEXRow nv in grdNhanvien.GetCheckedRows())
                            {
                                Int16 id_nhanvien = Utility.Int16Dbnull(nv.Cells["id_nhanvien"].Value, -1);
                                new Delete().From(QheBacsiDichvucl.Schema).Where(QheBacsiDichvucl.Columns.IdNhanvien).IsEqualTo(id_nhanvien).Execute();
                                if (id_nhanvien > 0)
                                {
                                    if (grdDvu.GetCheckedRows().Count() <= 0)
                                    {
                                        grdDvu.CurrentRow.BeginEdit();
                                        grdDvu.CurrentRow.IsChecked = true;
                                        grdDvu.CurrentRow.EndEdit();
                                    }
                                    foreach (GridEXRow dv in grdDvu.GetCheckedRows())
                                    {
                                        Int16 id_dichvu = Utility.Int16Dbnull(dv.Cells["id_dichvu"].Value, -1);
                                        int id_chitietdichvu = Utility.Int32Dbnull(dv.Cells["id_chitietdichvu"].Value, -1);
                                        byte ptram_ckhau = Utility.ByteDbnull(dv.Cells["ptram_ckhau"].Value, 0);
                                        if (id_dichvu > 0 && id_chitietdichvu > 0)
                                        {
                                            QheBacsiDichvucl item = new QheBacsiDichvucl();
                                            item.IdNhanvien = id_nhanvien;
                                            item.IdDichvu = id_dichvu;
                                            item.IdChitietdichvu = id_chitietdichvu;
                                            item.PtramCkhau = ptram_ckhau;
                                            item.IsNew = true;
                                            item.Save();
                                        }
                                    }
                                }

                            }
                        }
                        scope.Complete();
                        //grdStaffList_SelectionChanged(grdStaffList, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
            
        }

        
        private void optAll_CheckedChanged(object sender, EventArgs e)
        {
            if (optAll.Checked)
                m_dtStaffList.DefaultView.RowFilter = "1=1";
            else if (optYes.Checked)
                m_dtStaffList.DefaultView.RowFilter = "len(Isnull(user_name,''))>0";
            else if (optNo.Checked)
                m_dtStaffList.DefaultView.RowFilter = "Isnull(user_name,'') = '' ";
        }
        
        private void cmdGanSysTrinhKy_Click(object sender, EventArgs e)
        {
            List<string> lstErrr = new List<string>();

            try
            {
                grdQuyen.RemoveFilters();
                grdbaocaomulti.RemoveFilters();
                grdCosoKCB.RemoveFilters();
                grdDichvuCls.RemoveFilters();
                grdDmucchung.RemoveFilters();
                grdDvu.RemoveFilters();
                grdKhoa.RemoveFilters();
                grdKhoanoitru.RemoveFilters();
                grdKhoThuoc.RemoveFilters();
                grdLoaiThuoc.RemoveFilters();
                grdPhongkham.RemoveFilters();
                if (grdNhanvien.GetCheckedRows().Count() > 0)
                {
                    string nhanvien = string.Join(",",(from p in grdNhanvien.GetCheckedRows()
                                                   select Utility.sDbnull(p.Cells["ten_nhanvien"].Value, "")).ToArray<string>());

                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn gán quyền truy cập {0} cho các nhân viên dưới đây:\n {1}?", uiTab1.SelectedTab.Text, nhanvien), "Xác nhận", true))
                    {
                        Utility.ShowMsg("Bạn vừa hủy thao tác. Nhấn OK để kết thúc");
                        return;
                    }
                    foreach (GridEXRow row in grdNhanvien.GetCheckedRows())
                    {

                        DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(row.Cells["id_nhanvien"].Value));
                        QheNhanvienDanhmucCollection lstQheDmuc = uiTab1.SelectedIndex == 4 || uiTab1.SelectedIndex == 5 ? GetQheNhanvienDanhmuc(objDmucNhanvien) : null;
                        QheNhanvienKhoCollection lstQhekho = uiTab1.SelectedIndex == 1 ? GetQuanheNhanVienKho(objDmucNhanvien) : null;
                        QheBacsiKhoaphongCollection lstQhekhoa = uiTab1.SelectedIndex == 2 || uiTab1.SelectedIndex == 3 ? GetQuanheBsi_khoaphong(objDmucNhanvien) : null;
                        QheNhanvienQuyensudungCollection lstQheQuyensudung = uiTab1.SelectedIndex == 0 ? GetQuanheNhanVienQuyen(objDmucNhanvien) : null;
                        QheNhanvienBaocaomultiCollection lstQheBaocaomulti = uiTab1.SelectedIndex == 6 ? GetQuanheNhanVienBaocaomulti(objDmucNhanvien) : null;
                        QheNhanvienDmucchungCollection lstQheDmchung = uiTab1.SelectedIndex == 7 ? GetQuanheNhanVienDmucchung(objDmucNhanvien) : null;
                        QheNhanvienCosoCollection lstQheCoso = uiTab1.SelectedIndex == 9 ? GetQheNhanvienCoso(objDmucNhanvien) : null;
                        string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung,lstQheCoso, false);
                        if (ErrMsg != string.Empty)
                        {
                            lstErrr.Add(string.Format("LỖI Cập nhật thông tin quyền cho bác sĩ: {0}",Utility.sDbnull(row.Cells["ten_nhanvien"].Value)));
                        }
                    }
                    if (lstErrr.Count > 0)
                        Utility.ShowMsg(string.Join("\n", lstErrr.ToArray<string>()));
                    else
                        Utility.ShowMsg("Đã cập nhật thông tin Bác sĩ thành công");
                }
                else
                {
                    if (!Utility.isValidGrid(grdNhanvien))
                    {
                        Utility.ShowMsg("Bạn cần chọn bác sĩ cần cập nhật thông tin");
                        return;
                    }
                    string nhanvien = grdNhanvien.GetValue("ten_nhanvien").ToString();

                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn gán quyền truy cập {0} cho nhân viên: {1}?", uiTab1.SelectedTab.Text, nhanvien), "Xác nhận", true))
                    {
                        Utility.ShowMsg("Bạn vừa hủy thao tác. Nhấn OK để kết thúc");
                        return;
                    }

                    DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")));
                    QheNhanvienDanhmucCollection lstQheDmuc = uiTab1.SelectedIndex == 4 || uiTab1.SelectedIndex == 5 ? GetQheNhanvienDanhmuc(objDmucNhanvien) : null;
                    QheNhanvienKhoCollection lstQhekho = uiTab1.SelectedIndex == 1 ? GetQuanheNhanVienKho(objDmucNhanvien) : null;
                    QheBacsiKhoaphongCollection lstQhekhoa = uiTab1.SelectedIndex == 2 || uiTab1.SelectedIndex == 3 ? GetQuanheBsi_khoaphong(objDmucNhanvien) : null;
                    QheNhanvienQuyensudungCollection lstQheQuyensudung = uiTab1.SelectedIndex == 0 ? GetQuanheNhanVienQuyen(objDmucNhanvien) : null;
                    QheNhanvienBaocaomultiCollection lstQheBaocaomulti = uiTab1.SelectedIndex == 6 ? GetQuanheNhanVienBaocaomulti(objDmucNhanvien) : null;
                    QheNhanvienDmucchungCollection lstQheDmchung = uiTab1.SelectedIndex == 7 ? GetQuanheNhanVienDmucchung(objDmucNhanvien) : null;
                    QheNhanvienCosoCollection lstQheCoso = uiTab1.SelectedIndex == 9 ? GetQheNhanvienCoso(objDmucNhanvien) : null;
                    string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung,lstQheCoso, false);
                    if (ErrMsg == string.Empty)
                    {
                        Utility.ShowMsg("Đã cập nhật thông tin Bác sĩ thành công");
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void mnuSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> lstErrr = new List<string>();
                grdQuyen.RemoveFilters();
                grdbaocaomulti.RemoveFilters();
                grdCosoKCB.RemoveFilters();
                grdDichvuCls.RemoveFilters();
                grdDmucchung.RemoveFilters();
                grdDvu.RemoveFilters();
                grdKhoa.RemoveFilters();
                grdKhoanoitru.RemoveFilters();
                grdKhoThuoc.RemoveFilters();
                grdLoaiThuoc.RemoveFilters();
                grdPhongkham.RemoveFilters();
                if (grdNhanvien.GetCheckedRows().Count() > 0)
                {
                    string nhanvien = string.Join(",", (from p in grdNhanvien.GetCheckedRows()
                                                        select Utility.sDbnull(p.Cells["ten_nhanvien"].Value, "")).ToArray<string>());

                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn gán tất cả các quyền đang chọn tại các Tab cho các nhân viên dưới đây:\n {0}?", nhanvien), "Xác nhận", true))
                    {
                        Utility.ShowMsg("Bạn vừa hủy thao tác. Nhấn OK để kết thúc");
                        return;
                    }
                    string noidung = "";
                    foreach (GridEXRow row in grdNhanvien.GetCheckedRows())
                    {

                        DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(row.Cells["id_nhanvien"].Value));
                        QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
                        QheNhanvienKhoCollection lstQhekho = GetQuanheNhanVienKho(objDmucNhanvien);
                        QheBacsiKhoaphongCollection lstQhekhoa = GetQuanheBsi_khoaphong(objDmucNhanvien);
                        QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
                        QheNhanvienBaocaomultiCollection lstQheBaocaomulti = GetQuanheNhanVienBaocaomulti(objDmucNhanvien);
                        QheNhanvienDmucchungCollection lstQheDmchung = GetQuanheNhanVienDmucchung(objDmucNhanvien);
                        QheNhanvienCosoCollection lstQheCoso = uiTab1.SelectedIndex == 9 ? GetQheNhanvienCoso(objDmucNhanvien) : null;
                        string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung,lstQheCoso, false);
                        if (ErrMsg != string.Empty)
                        {
                            noidung = string.Format("LỖI Cập nhật thông tin quyền cho bác sĩ: {0}", Utility.sDbnull(row.Cells["ten_nhanvien"].Value));
                            Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                            lstErrr.Add(string.Format("LỖI Cập nhật thông tin quyền cho bác sĩ: {0}", Utility.sDbnull(row.Cells["ten_nhanvien"].Value)));
                        }
                        else
                        {
                            noidung = string.Format("Cập nhật thông tin quyền cho bác sĩ: {0} thành công", Utility.sDbnull(row.Cells["ten_nhanvien"].Value));
                            Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        }
                     
                    }
                    if (lstErrr.Count > 0)
                        Utility.ShowMsg(string.Join("\n", lstErrr.ToArray<string>()));
                    else
                        Utility.ShowMsg("Đã cập nhật thông tin Bác sĩ thành công");

                   
                   
                }
                else
                {
                    if (!Utility.isValidGrid(grdNhanvien))
                    {
                        Utility.ShowMsg("Bạn cần chọn bác sĩ cần cập nhật thông tin");
                        return;
                    }
                    string nhanvien = grdNhanvien.GetValue("ten_nhanvien").ToString();
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn gán tất cả các quyền đang chọn tại các Tab cho nhân viên: {0}?", nhanvien), "Xác nhận", true))
                    {
                        Utility.ShowMsg("Bạn vừa hủy thao tác. Nhấn OK để kết thúc");
                        return;
                    }
                   
                    DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(grdNhanvien.GetValue("id_nhanvien")));
                    QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
                    QheNhanvienKhoCollection lstQhekho = GetQuanheNhanVienKho(objDmucNhanvien);
                    QheBacsiKhoaphongCollection lstQhekhoa = GetQuanheBsi_khoaphong(objDmucNhanvien);
                    QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
                    QheNhanvienBaocaomultiCollection lstQheBaocaomulti = GetQuanheNhanVienBaocaomulti(objDmucNhanvien);
                    QheNhanvienDmucchungCollection lstQheDmchung = GetQuanheNhanVienDmucchung(objDmucNhanvien);
                    QheNhanvienCosoCollection lstQheCoso = GetQheNhanvienCoso(objDmucNhanvien);
                    string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung,lstQheCoso, false);
                    if (ErrMsg == string.Empty)
                    {
                        string noidung = string.Format("Cập nhật thông tin quyền cho bác sĩ: {0} thành công", objDmucNhanvien.TenNhanvien);
                        Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        Utility.ShowMsg("Đã cập nhật thông tin Bác sĩ thành công");
                    }
                    else
                    {
                        string noidung = string.Format("Cập nhật thông tin quyền cho bác sĩ: {0} thất bại", objDmucNhanvien.TenNhanvien);
                        Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void mnuHieuluc_Click(object sender, EventArgs e)
        {
            try
            {
                string nhanvien = string.Join(",", (from p in grdNhanvien.GetCheckedRows()
                                                    select Utility.sDbnull(p.Cells["ten_nhanvien"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdNhanvien.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["id_nhanvien"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật trạng thái đang làm việc cho các nhân viên dưới đây:\n {0}?", nhanvien), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác cập nhật trạng thái đang làm việc cho các nhân viên. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucNhanvien.Schema).Set(DmucNhanvien.Columns.TrangThai).EqualTo(1).Where(DmucNhanvien.Columns.IdNhanvien).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtStaffList.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id_nhanvien"], -1)) select p).ToList().ForEach(x => x["trang_thai"] = 1);
                    Utility.ShowMsg(string.Format("Đã cập nhật trạng thái đang làm việc cho các bác sĩ {0} thành công", nhanvien));
                    string noidung = string.Format("Đã cập nhật trạng thái đang làm việc cho các bác sĩ {0} thành công", nhanvien);
                    Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }

        private void mnuHethieuluc_Click(object sender, EventArgs e)
        {
            try
            {
                string nhanvien = string.Join(",", (from p in grdNhanvien.GetCheckedRows()
                                                    select Utility.sDbnull(p.Cells["ten_nhanvien"].Value, "")).ToArray<string>());
                List<int> lstID = (from p in grdNhanvien.GetCheckedRows()
                                   select Utility.Int32Dbnull(p.Cells["id_nhanvien"].Value, "")).ToList<int>();
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật trạng thái không làm việc cho các nhân viên dưới đây:\n {0}?", nhanvien), "Xác nhận", true))
                {
                    Utility.ShowMsg("Bạn vừa hủy thao tác cập nhật trạng thái không làm việc cho các nhân viên. Nhấn OK để kết thúc");
                    return;
                }

                int numofa = new Update(DmucNhanvien.Schema).Set(DmucNhanvien.Columns.TrangThai).EqualTo(0).Where(DmucNhanvien.Columns.IdNhanvien).In(lstID).Execute();
                if (numofa > 0)
                {
                    (from p in m_dtStaffList.AsEnumerable() where lstID.Contains(Utility.Int32Dbnull(p["id_nhanvien"], -1))select p).ToList().ForEach(x => x["trang_thai"] = 0);
                    Utility.ShowMsg(string.Format("Đã cập nhật trạng thái không làm việc cho các bác sĩ {0} thành công", nhanvien));
                    string noidung = string.Format("Đã cập nhật trạng thái không làm việc cho các bác sĩ {0} thành công", nhanvien);
                    Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
