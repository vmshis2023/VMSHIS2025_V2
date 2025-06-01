using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.Libs;
using System.Transactions;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ThongTinThau : Form
    {
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         string.Format("SplitterDistancefrm_ThongTinThau.txt"));

        private string kieu_thuocvt = "TATCA";
        private  AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();

        private int Distance = 488;
        private bool b_Hasloaded = false;
        private int id_PhieuNhap;
        private DataTable m_dtData = new DataTable();
        private DataTable m_dtDataChiTiet = new DataTable();

        public frm_ThongTinThau(string skieu_thuocvt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtFromdate.Value = DateTime.Now.AddMonths(-1);
            dtToDate.Value = DateTime.Now;
            kieu_thuocvt = skieu_thuocvt;
            KeyDown += frm_ThongTinThau_KeyDown;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            grdList.ApplyingFilter += grdList_ApplyingFilter;
            grdList.SelectionChanged += grdDanhSachThau_SelectionChanged;
            grdList.RowDoubleClick += grdList_RowDoubleClick;
            grdDieutiet.SelectionChanged += grdDieutiet_SelectionChanged;
            ReadSliper();
        }

        void grdList_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            cmdUpdate.PerformClick();
        }

        void grdDieutiet_SelectionChanged(object sender, EventArgs e)
        {
            ModifyDieutietCommands();
        }
        public frm_ThongTinThau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtFromdate.Value = DateTime.Now.AddMonths(-1);
            dtToDate.Value = DateTime.Now;
           // kieu_thuocvt = skieu_thuocvt;
            KeyDown += frm_ThongTinThau_KeyDown;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            grdList.ApplyingFilter += grdList_ApplyingFilter;
            grdList.SelectionChanged += grdDanhSachThau_SelectionChanged;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdDieutiet.MouseDoubleClick += grdDieutiet_MouseDoubleClick;
            ReadSliper();
        }

        void grdDieutiet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuadieutiet.PerformClick();
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ThongTinThau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Control) cmdThem.PerformClick();
            if (e.KeyCode == Keys.U && e.Control) cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdXoa.PerformClick();
            if (e.KeyCode == Keys.F4) cmdIn.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F12 && e.Control&&e.Shift)
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện việc lưu kéo ra kheo vào của cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!b_Hasloaded) return;
            SplitterDistance = splitContainer1.SplitterDistance;
            if (File.Exists(FileName))
            {
                WriteSlipterContaiter();
            }
        }

        /// <summary>
        /// hàm thực hiên viecj viết thông tin của sliper khi di chuyển vào file text
        /// </summary>
        private void WriteSlipterContaiter()
        {
            SplitterDistance = splitContainer1.SplitterDistance;
            File.WriteAllText(FileName, SplitterDistance.ToString());
        }

        private void ReadSliper()
        {
            if (File.Exists(FileName))
            {
                SplitterDistance = Utility.Int32Dbnull(File.ReadAllLines(FileName)[0]);
            }
            else
            {
                WriteSlipterContaiter();
            }
            splitContainer1.SplitterDistance = SplitterDistance;
        }
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        private void SearchData()
        {
            grdList.DataSource = null;
            grdChiTiet.DataSource = null;
            byte trangThai = 100;
            if (optXacnhan.Checked) trangThai = 1;
            if (optChuaXacnhan.Checked) trangThai = 0;
            if (optAll.Checked) trangThai = 100;
            string goi_thau = Utility.sDbnull(txtGoiThau.myCode);
            string loai_thau = Utility.sDbnull(txtLoaiThau.myCode);
            string so_qd = Utility.sDbnull(txtSoQDinh.Text);
            string so_thau = Utility.sDbnull(txtSoThau.Text);
            int hthuc_thau = Utility.Int32Dbnull(cbohinhthucthau.SelectedValue,-1);
            int id_thau = Utility.Int32Dbnull(txtIDThau.Text);
            m_dtData = SPs.ThuocThauLaydanhsach(chkByDate.Checked ? dtFromdate.Value : Convert.ToDateTime("01/01/1900"), chkByDate.Checked ? dtToDate.Value : DateTime.Now, "-1", loai_thau, goi_thau, so_thau, so_qd, Utility.sDbnull(txtDrugName.Text), txtNhacungcap.myCode, id_thau, trangThai).GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "");
            Utility.SetGridEXSortKey(grdList, TThau.Columns.IdThau, SortOrder.Ascending);
            ModifyCommands();
        }
        private void ModifyDieutietCommands()
        {
            bool isValidGid = Utility.isValidGrid(grdList);
            if (!isValidGid)
            {
                cmdSuadieutiet.Enabled = cmdXoadieutiet.Enabled = cmdXacnhandieutiet.Enabled = cmdHuyxacnhandieutiet.Enabled = cmdView.Enabled = false;
                grbLichsudieutiet.Height = 0;
                return;
            }
            bool LaApthau = isValidGid && Utility.Byte2Bool(grdList.GetValue("la_apthau"));
            bool chuaxacnhan = isValidGid && Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.TrangThai), 0) == 0;
            if (LaApthau)
                grbLichsudieutiet.Height = 0;
            else
            {
                bool xacnhan_dieutiet = Utility.isValidGrid(grdDieutiet) && Utility.Int32Dbnull(grdDieutiet.GetValue(TThauDieutiet.Columns.TrangThai), 0) == 1;
                cmdThemdieutiet.Enabled = isValidGid && !LaApthau && !chuaxacnhan;
                grbLichsudieutiet.Height = 266;
                cmdSuadieutiet.Enabled = cmdXoadieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && !xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdXacnhandieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && !xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdHuyxacnhandieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdView.Enabled = Utility.isValidGrid(grdDieutiet);
            }

        }
        private void ModifyCommands()
        {
            bool isValidGid = Utility.isValidGrid(grdList);
            if (!isValidGid)
            {
                cmdXoa.Enabled = cmdUpdate.Enabled = cmdView.Enabled = cmdXacNhan.Enabled = cmdHuyXacNhan.Enabled = cmdBoSung.Enabled = cmdIn.Enabled = false;
                cmdSuadieutiet.Enabled = cmdXoadieutiet.Enabled = cmdXacnhandieutiet.Enabled = cmdHuyxacnhandieutiet.Enabled = cmdView.Enabled = false;
                grbLichsudieutiet.Height = 0;
                return;
            }
            bool LaApthau = isValidGid && Utility.Byte2Bool(grdList.GetValue("la_apthau"));
            bool chuaxacnhan = isValidGid && Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.TrangThai), 0) == 0;

            cmdUpdate.Enabled = cmdXoa.Enabled = chuaxacnhan && isValidGid;
            cmdXacNhan.Enabled = chuaxacnhan && isValidGid;
            cmdHuyXacNhan.Enabled = !cmdXacNhan.Enabled && isValidGid;
            cmdBoSung.Enabled = !chuaxacnhan && isValidGid;
            cmdView.Enabled = isValidGid;
            cmdIn.Enabled = isValidGid;
            if (LaApthau)
                grbLichsudieutiet.Height = 0;
            else
            {
                bool xacnhan_dieutiet = Utility.isValidGrid(grdDieutiet) && Utility.Int32Dbnull(grdDieutiet.GetValue(TThauDieutiet.Columns.TrangThai), 0) == 1;
                cmdThemdieutiet.Enabled = isValidGid && !LaApthau && !chuaxacnhan;
                grbLichsudieutiet.Height = 266;
                cmdSuadieutiet.Enabled = cmdXoadieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && !xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdXacnhandieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && !xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdHuyxacnhandieutiet.Enabled = Utility.isValidGrid(grdDieutiet) && xacnhan_dieutiet && !LaApthau && !chuaxacnhan;
                cmdView.Enabled = Utility.isValidGrid(grdDieutiet);
            }

        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromdate.Enabled = chkByDate.Checked;
        }

        private bool isValidXoa()
        {
            long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            SqlQuery sqlQuery = new Select().From(TThau.Schema).Where(TThau.Columns.TrangThai).IsEqualTo(1).And(TThau.Columns.IdThau).IsEqualTo(IdThau);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được duyệt nên không thể sửa/xóa", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            sqlQuery = new Select().From(TThauQdinh.Schema).Where(TThauQdinh.Columns.IdThau).IsEqualTo(IdThau);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được duyệt nên không thể sửa/xóa.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            sqlQuery = new Select().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdThau).IsEqualTo(IdThau);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được điều tiết nên không thể sửa/xóa.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            //sqlQuery = new Select().From(TThauChiTiet.Schema).Where(TThauChiTiet.Columns.IdThau).IsEqualTo(IdThau).And(TThauChiTiet.Columns.SlDaNhap).IsGreaterThan(0);
            sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema).Where(TPhieuNhapxuatthuocChitiet.Columns.IdQdinh).In(new Select(TThauQdinh.Columns.IdQdinh).From(TThauQdinh.Schema).Where(TThauQdinh.Columns.IdThau).IsEqualTo(IdThau));
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã có phiếu nhập sử dụng thông tin thầu này.\nKhông thể sửa/xóa thông tin.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdXoa_Click(object sender, EventArgs e)
        {
            int IdThau = Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            string soQdinh = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoQuyetdinh), -1);
            string soHDong = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoThau), -1);
            if (!isValidXoa()) return;
            //if(!isValidXacnhan())return;
            if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa thông tin Thầu {0}.", IdThau), "Thông báo", true))
            {
                DeleteMe();
                grdList.CurrentRow.Delete();
                grdList.UpdateData();
                m_dtData.AcceptChanges();
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa thông tin thầu: ID: {0}; Số QĐ: {1}; Số HĐ: {2}", IdThau, soQdinh, soHDong), newaction.Delete, "UI");
                Utility.ShowMsg("Bạn xóa thông tin thầu thành công", "Thông báo", MessageBoxIcon.Information);
            }
            ModifyCommands();
        }
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        long id_thau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                        //new Delete().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdThau).IsEqualTo(id_thau).Execute();
                        //new Delete().From(TThauQdinh.Schema).Where(TThauQdinh.Columns.IdThau).IsEqualTo(id_thau).Execute();
                        new Delete().From(TThauChitiet.Schema).Where(TThauChitiet.Columns.IdThau).IsEqualTo(id_thau).Execute();
                        new Delete().From(TThau.Schema).Where(TThau.Columns.IdThau).IsEqualTo(id_thau).Execute();
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        private void frm_ThongTinThau_Load(object sender, EventArgs e)
        {
            txtGoiThau.Init();
            txtLoaiThau.Init();
            txtNhacungcap.Init();
            DataTable dtHinhthucthau = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("HINHTHUCTHAU").ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbohinhthucthau, dtHinhthucthau, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "-----Chọn hình thức thầu-----", true);
            SearchData();
            ModifyCommands();
        }
        private void cmdInPhieuNhapKho_Click(object sender, EventArgs e)
        {
            try
            {
                id_PhieuNhap = Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            }
            catch (Exception)
            {
            }
        }
        private bool isValidXacnhan()
        {
            int IdThau = Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            SqlQuery sqlQuery = new Select().From<TThau>().Where(TThau.Columns.IdThau).IsEqualTo(IdThau).And(TThau.Columns.TrangThai).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được duyệt nên không thể duyệt tiếp", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            sqlQuery = new Select().From<TThauQdinh>().Where(TThauQdinh.Columns.IdThau).IsEqualTo(IdThau);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được duyệt nên không thể duyệt tiếp", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdXacNhan_Click(object sender, EventArgs e)
        {
            cmdXacNhan.Enabled = false;
            if (!isValidXacnhan()) return;
            long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            string soQdinh = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoQuyetdinh), -1);
            string soHDong = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoThau), -1);
            try
            {
                TThau objTThau = TThau.FetchByID(IdThau);
                if (objTThau != null)
                {
                    objTThau.TrangThai = 1;
                    objTThau.NgayXacnhan = DateTime.Now;
                    objTThau.NguoiXacnhan = globalVariables.UserName;
                    objTThau.IsNew = false;
                    objTThau.MarkOld();
                    objTThau.Save();

                    grdList.CurrentRow.Cells["trang_thai"].Value = 1;
                    grdList.UpdateData();
                    m_dtData.AcceptChanges();

                    SPs.ThuocThauXacnhan(IdThau).Execute();

                    Utility.ShowMsg("Xác nhận thành công.", "Thông báo", MessageBoxIcon.Information);
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xác nhận thông tin thầu: ID: {0}; Số QĐ: {1}; Số HĐ: {2}", IdThau, soQdinh, soHDong), newaction.Update, "UI");
                    cmdHuyXacNhan.Enabled = true;
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy thông tin thầu trong CSDL.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(string.Format("Lỗi: {0}", ex.Message), "Thông báo", MessageBoxIcon.Information);
                cmdXacNhan.Enabled = true;
            }
        }
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommands();
        }
        DataTable m_dtDieutiet = new DataTable();
        long id_thau = -1;
        private void grdDanhSachThau_SelectionChanged(object sender, EventArgs e)
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                 id_thau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                 m_dtDataChiTiet = SPs.ThuocThauLaythongtinchitiet(id_thau).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiTiet, m_dtDataChiTiet, true, true, "1=1", "");
                m_dtDieutiet = SPs.ThuocThauDieutietLaydanhsach(id_thau,-1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDieutiet, m_dtDieutiet, true, true, "1=1", "");
            }
            else
            {
                grbLichsudieutiet.Height = 0;
                id_thau = -1;
                grdChiTiet.DataSource = null;
                grdDieutiet.DataSource = null;
            }
            ModifyCommands();
        }
        private void cmdThem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_themthau(kieu_thuocvt);
                frm.m_enAct = action.Insert;
                frm._objThau = null;
                frm.txtIDThau.Text = "-1";
                frm._OnCreated += _OnCreated;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    grdDanhSachThau_SelectionChanged(grdList, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidXoa()) return;
                //if (!isValidXacnhan()) return;
                long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                string soQdinh = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoQuyetdinh), -1);
                string soHDong = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoThau), -1);
                var frm = new frm_themthau(kieu_thuocvt);
                frm.m_enAct = action.Update;
                frm._OnCreated += _OnCreated;
                frm._objThau = TThau.FetchByID(IdThau);
                frm.txtIDThau.Text = Utility.sDbnull(IdThau);
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Sửa thông tin thầu:  ID: {0}; Số QĐ: {1}; Số HĐ: {2}", IdThau, soQdinh, soHDong), newaction.Update, "UI");
                    grdDanhSachThau_SelectionChanged(grdList, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }

       
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
               
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                    return;
                }
                DataTable dt_temp = SPs.ThuocThauLaydanhsach(Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "-1", "", "", "", "", "", "", id, 100).GetDataSet().Tables[0];
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id_thau=" + id);
                    if (arrDr.Length > 0)
                    {
                        Utility.CopyData(dt_temp.Rows[0],ref  arrDr[0]);
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "id_thau", id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        private void txtSoThau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSearch.PerformClick();
            }
        }
        private bool isValidHuyxacnhan()
        {
            long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
            SqlQuery sqlQuery = new Select().From<TThau>().Where(TThau.Columns.IdThau).IsEqualTo(IdThau).And(TThau.Columns.TrangThai).IsEqualTo(0);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu chưa được duyệt nên bạn không thể hủy duyệt", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            sqlQuery = new Select().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdThau).IsEqualTo(IdThau);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được điều tiết nên bạn không thể hủy duyệt.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                                .Where(TPhieuNhapxuatthuocChitiet.Columns.IdQdinh).In(new Select(TThauQdinh.Columns.IdQdinh)
                                                                            .From(TThauQdinh.Schema).Where(TThauQdinh.Columns.IdThau).IsEqualTo(IdThau));
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Thầu đã được sử dụng trong phiếu nhập kho nên bạn không thể hủy duyệt.", "Thông báo", MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void cmdHuyXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidHuyxacnhan()) return;
                if (Utility.AcceptQuestion("Bạn chắc chắn muốn hủy xác nhận thông tin thầu này?", "Thông báo", true))
                {
                    cmdHuyXacNhan.Enabled = false;
                    long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                    string soQdinh = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoQuyetdinh), -1);
                    string soHDong = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoThau), -1);

                    TThau objTThau = TThau.FetchByID(IdThau);
                    if (objTThau != null)
                    {
                        int _record = new Delete().From(TThauQdinh.Schema).Where(TThauQdinh.Columns.IdThau).IsEqualTo(IdThau).Execute();
                        if (_record > 0)
                        {
                            objTThau.TrangThai = 0;
                            objTThau.NgayXacnhan = null;
                            objTThau.NguoiXacnhan = string.Empty;
                            objTThau.IsNew = false;
                            objTThau.MarkOld();
                            objTThau.Save();
                            _record = new Update(TThauChitiet.Schema).Set(TThauChitiet.Columns.TrangThai).EqualTo(0).Where(TThauChitiet.Columns.IdThau).IsEqualTo(IdThau).Execute();

                            grdList.CurrentRow.Cells["trang_thai"].Value = 0;
                            grdList.UpdateData();
                            m_dtData.AcceptChanges();
                            Utility.ShowMsg("Hủy Xác nhận thành công.", "Thông báo", MessageBoxIcon.Information);
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy Xác nhận thông tin thầu:  ID: {0}; Số QĐ: {1}; Số HĐ: {2}", IdThau, soQdinh, soHDong), newaction.Update, "UI");
                            cmdXacNhan.Enabled = true;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg("Không tìm thấy thông tin thầu trong CSDL.\nMời bạn lấy lại dữ liệu", "Thông báo", MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(string.Format("Lỗi: {0}", ex.Message), "Thông báo", MessageBoxIcon.Information);
                cmdXacNhan.Enabled = true;
            }
        }

        private void tsmDieuTiet_Click(object sender, EventArgs e)
        {
            try
            {
                //var frm = new frm_Add_DieuTiet_Thau(kieu_thuocvt);
                //frm.m_enAct = action.Insert;
                //frm.txtID_DieuTiet.Text = "-1";
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            //var frmImport = new frm_Import_THONGTIN_THAU_new();
            //frmImport.dTThau = m_dtData;
            //frmImport.grdList = grdDanhSachThau;
            //frmImport.ShowDialog();
        }

        private void cmdBoSung_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Thông tin thầu đã được xác nhận. \nBạn chắc chắn muốn bổ sung chi tiết thầu?", "Thông báo", true))
                {
                    long IdThau = Utility.Int64Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                    string soQdinh = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoQuyetdinh), -1);
                    string soHDong = Utility.sDbnull(grdList.GetValue(TThau.Columns.SoThau), -1);
                    var frm = new frm_themthau(kieu_thuocvt);
                    frm.m_enAct = action.Add;
                    frm.txtIDThau.Text = Utility.sDbnull(IdThau);
                    frm._objThau = TThau.FetchByID(IdThau);
                    frm.ShowDialog();
                    if (frm.m_blnCancel)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Bổ sung chi tiết thầu: ID: {0}; Số QĐ: {1}; Số HĐ: {2}", IdThau, soQdinh, soHDong), newaction.Update, "UI");
                        grdDanhSachThau_SelectionChanged(grdList, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                int IdThau = Utility.Int32Dbnull(grdList.GetValue(TThau.Columns.IdThau), -1);
                var frm = new frm_themthau(kieu_thuocvt);
                frm.m_enAct = action.View;
                frm.txtIDThau.Text = Utility.sDbnull(IdThau);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }

        private void cmdDieutietthau_Click(object sender, EventArgs e)
        {
          
        }

        void _themmoi_dieutiet__OnCreated(long id, action m_enAct)
        {
            try
            {

                if (m_enAct == action.Delete)
                {

                    if (DeleteDieutiet())
                    {
                        DataRow[] arrDr = m_dtDieutiet.Select(string.Format("id_dieutiet={0}", grdDieutiet.GetValue("id_dieutiet")));
                        if (arrDr.Length > 0)
                            m_dtDieutiet.Rows.Remove(arrDr[0]);
                        m_dtDieutiet.AcceptChanges();
                    }
                    return;
                }
                DataTable dt_temp = SPs.ThuocThauDieutietLaydanhsach(id_thau, id).GetDataSet().Tables[0];
                if (m_enAct == action.Insert && m_dtDieutiet != null && m_dtDieutiet.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtDieutiet.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtDieutiet != null && m_dtDieutiet.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtDieutiet.Select("id_dieutiet=" + id);
                    if (arrDr.Length > 0)
                    {
                        Utility.CopyData(dt_temp.Rows[0], ref  arrDr[0]);
                    }
                    else
                        m_dtDieutiet.ImportRow(dt_temp.Rows[0]);

                }
                m_dtDieutiet.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "id_dieutiet", id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommands();
            }
        }
        bool DeleteDieutiet()
        {
            try
            {
                long id_dieutiet = Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), -1);
                TThauDieutiet objDieutiet = TThauDieutiet.FetchByID(id_dieutiet);
                if (objDieutiet.TrangThai == 1)
                {
                    Utility.ShowMsg("Điều tiết thầu đã được xác nhận nên không thể xóa. Muốn xóa thì cần hủy xác nhận điều tiết");
                    return false;
                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(TThauDieutietCt.Schema).Where(TThauDieutietCt.Columns.IdDieutiet).IsEqualTo(id_dieutiet).Execute();
                        new Delete().From(TThauDieutiet.Schema).Where(TThauDieutiet.Columns.IdDieutiet).IsEqualTo(id_dieutiet).Execute();
                    }
                    scope.Complete();
                    return true;

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }

        private void cmdThemdieutiet_Click(object sender, EventArgs e)
        {
            frm_themmoi_dieutiet _themmoi_dieutiet = new frm_themmoi_dieutiet(this.kieu_thuocvt);
            _themmoi_dieutiet.id_thau = Utility.Int64Dbnull(grdList.GetValue("id_thau"), 0);
            _themmoi_dieutiet.id_dieutiet = -1;
            _themmoi_dieutiet.m_enAction = action.Insert;
            _themmoi_dieutiet._OnCreated += _themmoi_dieutiet__OnCreated;
            _themmoi_dieutiet.ShowDialog();
        }

        private void cmdSuadieutiet_Click(object sender, EventArgs e)
        {
            TThauDieutiet objDieutiet = TThauDieutiet.FetchByID(Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), 0));
            if (objDieutiet.TrangThai == 1)
            {
                Utility.ShowMsg("Điều tiết thầu đã được xác nhận nên không thể sửa. Muốn sửa thì cần hủy xác nhận điều tiết");
                return ;
            }
            frm_themmoi_dieutiet _themmoi_dieutiet = new frm_themmoi_dieutiet(this.kieu_thuocvt);
            _themmoi_dieutiet.id_dieutiet = Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), 0);
            _themmoi_dieutiet.id_thau = Utility.Int64Dbnull(grdList.GetValue("id_thau"), 0);
            _themmoi_dieutiet.m_enAction = action.Update;
            _themmoi_dieutiet._OnCreated += _themmoi_dieutiet__OnCreated;
            _themmoi_dieutiet.ShowDialog();
        }

        private void cmdXoadieutiet_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu Điều tiết với thông tin số QĐ/CV: {0}, đơn vị điều tiết: {1} ?", grdDieutiet.GetValue("so_hdong_dieutiet"), grdDieutiet.GetValue("ten_benhvien_dieutiet")), "Xác nhận xóa", true))
                _themmoi_dieutiet__OnCreated(Utility.Int64Dbnull(grdList.GetValue("id_thau"), 0), action.Delete);
        }

        private void cmdXacnhandieutiet_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                long id_dieutiet = Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), 0);
                TThauDieutiet tdt = TThauDieutiet.FetchByID(id_dieutiet);
                if (tdt != null && tdt.TrangThai == 1)
                {
                    Utility.ShowMsg(string.Format("Điều tiết với thông tin số QĐ/CV: {0}, đơn vị điều tiết: {1} đã được xác nhận nên bạn không thể xác nhận tiếp. Vui lòng kiểm tra lại", tdt.SoHdongDieutiet, grdDieutiet.GetValue("ten_benhvien_dieutiet")));
                    return;
                }
                DataTable dtChitietdieutiet = new Select().From(TThauDieutietCt.Schema).Where(TThauDieutietCt.Columns.IdDieutiet).IsEqualTo(id_dieutiet).ExecuteDataSet().Tables[0];
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        num = new Update(TThauDieutiet.Schema)
                            .Set(TThauDieutiet.Columns.TrangThai).EqualTo(1)
                            .Set(TThauDieutiet.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TThauDieutiet.Columns.NgayXacnhan).EqualTo(globalVariables.SysDate)
                            .Where(TThauDieutiet.Columns.IdDieutiet).IsEqualTo(id_dieutiet).Execute();
                        foreach (DataRow dr in dtChitietdieutiet.Rows)
                        {
                            long id_thau_ct = Utility.Int64Dbnull(dr["id_thau_ct"]);
                            TThauChitiet objthauchitiet = TThauChitiet.FetchByID(id_thau_ct);
                            if (objthauchitiet != null)
                            {
                                int SoLuong = Utility.Int32Dbnull(objthauchitiet.SoLuong, 0);//Số lượng nhập theo thầu
                                int SlNhap = Utility.Int32Dbnull(objthauchitiet.SlNhap, 0);//Tổng Số lượng đã nhập kho
                                int SlDieutietDi = Utility.Int32Dbnull(objthauchitiet.SlDieutietDi, 0);//Tổng số lượng đã điều tiết tới các BV khác
                                int sl_khachuyen = SoLuong - SlNhap - SlDieutietDi;
                                if (sl_khachuyen >= Utility.Int32Dbnull(dr["so_luong"], 0))
                                {
                                    SlDieutietDi += Utility.Int32Dbnull(dr["so_luong"], 0);
                                    num = new Update(TThauChitiet.Schema)
                                        .Set(TThauChitiet.Columns.SlDieutietDi).EqualTo(SlDieutietDi)
                                        .Where(TThauChitiet.Columns.IdThauCt).IsEqualTo(id_thau_ct).Execute();
                                }
                                else//Ít khi xảy ra vì kể cả khi chưa xác nhận thì số lượng khả nhập khi nhập thuốc từ nhà cung cấp cũng sẽ tự trừ đi số lượng điều tiết chưa được xác nhận
                                {
                                    Utility.ShowMsg(string.Format("Hàng với Id={0} có số lượng điều tiết {1} đang lớn hơn số lượng khả chuyển {2}", objthauchitiet.IdThuoc, Utility.Int32Dbnull(dr["so_luong"], 0), sl_khachuyen));
                                    return;
                                }
                            }
                        }
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Xác nhận điều tiết thầu id_dieutiet: {0}, nơi điều tiết: {1}, số HĐ.CV điều tiết: {2} thành công", Utility.sDbnull(grdDieutiet.GetValue("id_dieutiet"), ""), Utility.sDbnull(grdDieutiet.GetValue("ten_benhvien_dieutiet"), ""), Utility.sDbnull(grdDieutiet.GetValue("so_hdong_dieutiet"), "")), newaction.CancelData, "UI");
                    }
                    scope.Complete();

                }
                grdDanhSachThau_SelectionChanged(grdList, e);
                //if (num > 0)
                //{
                //    grdDieutiet.CurrentRow.BeginEdit();
                //    grdDieutiet.CurrentRow.Cells["trang_thai"].Value = 1;
                //    grdDieutiet.CurrentRow.EndEdit();
                //}

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                //ModifyCommands();
            }
        }

        private void cmdHuyxacnhandieutiet_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                long id_dieutiet = Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), 0);
                TThauDieutiet tdt = TThauDieutiet.FetchByID(id_dieutiet);
                if (tdt != null && tdt.TrangThai == 0)
                {
                    Utility.ShowMsg(string.Format("Điều thiết với thông tin số QĐ/CV: {0}, đơn vị điều tiết: {1} đang ở trạng thái chưa xác nhận nên bạn không thể hủy xác nhận. Vui lòng kiểm tra lại", tdt.SoHdongDieutiet, grdDieutiet.GetValue("ten_benhvien_dieutiet")));
                    return;
                }
                DataTable dtChitietdieutiet = new Select().From(TThauDieutietCt.Schema).Where(TThauDieutietCt.Columns.IdDieutiet).IsEqualTo(id_dieutiet).ExecuteDataSet().Tables[0];
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        num = new Update(TThauDieutiet.Schema)
                            .Set(TThauDieutiet.Columns.TrangThai).EqualTo(0)
                            .Set(TThauDieutiet.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TThauDieutiet.Columns.NgayXacnhan).EqualTo(null)
                            .Where(TThauDieutiet.Columns.IdDieutiet).IsEqualTo(id_dieutiet).Execute();
                        foreach (DataRow dr in dtChitietdieutiet.Rows)
                        {
                            long id_thau_ct = Utility.Int64Dbnull(dr["id_thau_ct"]);
                            TThauChitiet objthauchitiet = TThauChitiet.FetchByID(id_thau_ct);
                            if (objthauchitiet != null)
                            {
                                int sl_dieutiet = Utility.Int32Dbnull(dr["so_luong"], 0);//Số lượng nhập theo thầu
                                int SlDieutietDi = Utility.Int32Dbnull(objthauchitiet.SlDieutietDi, 0);//Số lượng điều tiết tới các bệnh viện khác
                                if (SlDieutietDi >= sl_dieutiet)
                                {
                                    SlDieutietDi -= sl_dieutiet;
                                    num += new Update(TThauChitiet.Schema)
                                        .Set(TThauChitiet.Columns.SlDieutietDi).EqualTo(SlDieutietDi)
                                        .Where(TThauChitiet.Columns.IdThauCt).IsEqualTo(id_thau_ct).Execute();
                                }
                                else
                                {
                                    Utility.ShowMsg(string.Format("Hàng với Id={0} có tổng số lượng điều tiết {1} <= số lượng điều tiết lần này {2}.Vui lòng kiểm tra lại dữ liệu",objthauchitiet.IdThuoc, SlDieutietDi, sl_dieutiet));
                                    return;
                                }
                            }
                        }
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy xác nhận điều tiết thầu id_dieutiet: {0}, nơi điều tiết: {1}, số HĐ.CV điều tiết: {2} thành công", Utility.sDbnull(grdDieutiet.GetValue("id_dieutiet"),""), Utility.sDbnull(grdDieutiet.GetValue("ten_benhvien_dieutiet"),""), Utility.sDbnull(grdDieutiet.GetValue("so_hdong_dieutiet"),"")), newaction.CancelData, "UI");
                    }
                    scope.Complete();

                }
                grdDanhSachThau_SelectionChanged(grdList, e);
                //if (num > 0)
                //{
                //    grdDieutiet.CurrentRow.BeginEdit();
                //    grdDieutiet.CurrentRow.Cells["trang_thai"].Value = 0;
                //    grdDieutiet.CurrentRow.EndEdit();
                //}
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                //ModifyCommands();
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                m_dtDieutiet = SPs.ThuocThauDieutietLaydanhsach(id_thau,-1).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDieutiet, m_dtDieutiet, true, true, "1=1", "");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdViewPhieuDT_Click(object sender, EventArgs e)
        {
            frm_themmoi_dieutiet _themmoi_dieutiet = new frm_themmoi_dieutiet(this.kieu_thuocvt);
            _themmoi_dieutiet.id_dieutiet = Utility.Int64Dbnull(grdDieutiet.GetValue("id_dieutiet"), 0);
            _themmoi_dieutiet.id_thau = Utility.Int64Dbnull(grdList.GetValue("id_thau"), 0);
            _themmoi_dieutiet.m_enAction = action.View;
            //_themmoi_dieutiet._OnCreated += _themmoi_dieutiet__OnCreated;
            _themmoi_dieutiet.ShowDialog();
        }
    }
}