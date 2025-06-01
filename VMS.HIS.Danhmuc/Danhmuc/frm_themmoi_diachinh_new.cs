using System;
using System.Data;
using System.Windows.Forms;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;


namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_diachinh_new : Form
    {
        private readonly Logger log;
        private DataTable dtQH;
        private DataTable dtTP;
        private DataTable dtXP;
        private string rowFilter = "1=1";
        public bool m_blnHasChanged = false;
        public frm_themmoi_diachinh_new()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            cmdSave.Click+=cmdSave_Click;
            cmdClear.Click+=cmdClear_Click;
            cmdExit.Click+=cmdExit_Click;
            grdTinhThanh.UpdatingCell += grdTinhThanh_UpdatingCell;
            grdQuanHuyen.UpdatingCell += grdQuanHuyen_UpdatingCell;
            grdXaPhuong.UpdatingCell += grdXaPhuong_UpdatingCell;
            this.FormClosing += frm_themmoi_diachinh_new_FormClosing;
        }

        void frm_themmoi_diachinh_new_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_blnHasChanged)
            {
                globalVariables.gv_dtDmucDiachinh = new Select().From(VDmucDiachinh.Schema).ExecuteDataSet().Tables[0];
                Utility.AutoCompeleteAddress(globalVariables.gv_dtDmucDiachinh);
            }
        }

        void grdXaPhuong_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                string MaDiachinh = Utility.GetValueFromGridColumn(grdXaPhuong, DmucDiachinh.Columns.MaDiachinh);
                string TenDiachinh = Utility.GetValueFromGridColumn(grdXaPhuong, DmucDiachinh.Columns.TenDiachinh);
                string MotaThem = Utility.GetValueFromGridColumn(grdXaPhuong, DmucDiachinh.Columns.MotaThem);
                new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(TenDiachinh)
                    .Set(DmucDiachinh.Columns.MotaThem).EqualTo(MotaThem)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(MaDiachinh).Execute();
                m_blnHasChanged = true;
            }
            catch (Exception ex)
            {


            } 
        }

        void grdQuanHuyen_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                string MaDiachinh = Utility.GetValueFromGridColumn(grdQuanHuyen, DmucDiachinh.Columns.MaDiachinh);
                string TenDiachinh = Utility.GetValueFromGridColumn(grdQuanHuyen, DmucDiachinh.Columns.TenDiachinh);
                string MotaThem = Utility.GetValueFromGridColumn(grdQuanHuyen, DmucDiachinh.Columns.MotaThem);
                new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(TenDiachinh)
                    .Set(DmucDiachinh.Columns.MotaThem).EqualTo(MotaThem)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(MaDiachinh).Execute();
                m_blnHasChanged = true;
            }
            catch (Exception ex)
            {


            }
        }

        void grdTinhThanh_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            try
            {
                string MaDiachinh = Utility.GetValueFromGridColumn(grdTinhThanh, DmucDiachinh.Columns.MaDiachinh);
                string TenDiachinh = Utility.GetValueFromGridColumn(grdTinhThanh, DmucDiachinh.Columns.TenDiachinh);
                string MotaThem = Utility.GetValueFromGridColumn(grdTinhThanh, DmucDiachinh.Columns.MotaThem);
                new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(TenDiachinh)
                    .Set(DmucDiachinh.Columns.MotaThem).EqualTo(MotaThem)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(MaDiachinh).Execute();
                m_blnHasChanged = true;
            }
            catch (Exception ex)
            {
                
                
            }
        }

        private void LoadData()
        {
            try
            {
                dtTP =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteDataSet().
                        Tables
                        [0];
                Utility.SetDataSourceForDataGridEx_Basic(grdTinhThanh, dtTP,true, true, "1=1", DmucDiachinh.Columns.TenDiachinh);
                grdTinhThanh.MoveFirst();
                dtQH =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(1).ExecuteDataSet().
                        Tables
                        [0];
                Utility.SetDataSourceForDataGridEx_Basic(grdQuanHuyen, dtQH, true, true, "1=1", DmucDiachinh.Columns.TenDiachinh);
                grdQuanHuyen.MoveFirst();
                dtXP =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(2).ExecuteDataSet().
                        Tables
                        [0];
                Utility.SetDataSourceForDataGridEx_Basic(grdXaPhuong, dtXP, true, true, "1=1", DmucDiachinh.Columns.TenDiachinh);
                grdXaPhuong.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình lấy thông tin " + ex, "Thông Báo", MessageBoxIcon.Error);

                // throw;
            }
        }
        /// <summary>
        /// Kiểm tra trước khi thêm mới
        /// </summary>
        /// <returns></returns>
        #region Kiểm tra trước khi thêm mới
        private bool IsValidDataQH()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mã tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaTinhThanh.Focus();
                    txtMaTinhThanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenTinhthanh.Text))
                {
                    Utility.ShowMsg("Tên tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenTinhthanh.Focus();
                    txtTenTinhthanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mô tả về tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaTinhThanh.Focus();
                    txtMoTaTinhThanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaQuanHuyen.Text))
                {
                    Utility.ShowMsg("Mã quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaQuanHuyen.Focus();
                    txtMaQuanHuyen.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenQuanHuyen.Text))
                {
                    Utility.ShowMsg("Tên quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenQuanHuyen.Focus();
                    txtTenQuanHuyen.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaQuanHuyen.Text))
                {
                    Utility.ShowMsg("Mô tả về quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaQuanHuyen.Focus();
                    txtMoTaQuanHuyen.SelectAll();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        private bool IsValidDataTP()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mã tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaTinhThanh.Focus();
                    txtMaTinhThanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenTinhthanh.Text))
                {
                    Utility.ShowMsg("Tên tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenTinhthanh.Focus();
                    txtTenTinhthanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mô tả về tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaTinhThanh.Focus();
                    txtMoTaTinhThanh.SelectAll();
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                Utility.ShowMsg("Lỗi khi thêm mới thông tin", "Thông Báo", MessageBoxIcon.Error);
                return true;
                //throw;
            }
        }

        private bool IsValidDataXP()
        {
            try
            {

                if (string.IsNullOrEmpty(txtMaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mã tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaTinhThanh.Focus();
                    txtMaTinhThanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenTinhthanh.Text))
                {
                    Utility.ShowMsg("Tên tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenTinhthanh.Focus();
                    txtTenTinhthanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaTinhThanh.Text))
                {
                    Utility.ShowMsg("Mô tả về tỉnh/thành không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaTinhThanh.Focus();
                    txtMoTaTinhThanh.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaQuanHuyen.Text))
                {
                    Utility.ShowMsg("Mã quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaQuanHuyen.Focus();
                    txtMaQuanHuyen.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenQuanHuyen.Text))
                {
                    Utility.ShowMsg("Tên quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenQuanHuyen.Focus();
                    txtTenQuanHuyen.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaQuanHuyen.Text))
                {
                    Utility.ShowMsg("Mô tả về quận/huyện không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaQuanHuyen.Focus();
                    txtMoTaQuanHuyen.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaXaPhuong.Text))
                {
                    Utility.ShowMsg("Mã xã/phường không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMaXaPhuong.Focus();
                    txtMaXaPhuong.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTenXaPhuong.Text))
                {
                    Utility.ShowMsg("Tên xã/phường không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtTenXaPhuong.Focus();
                    txtTenXaPhuong.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMoTaXaPhuong.Text))
                {
                    Utility.ShowMsg("Mô tả về xã/phường không được bỏ trống", "Thông Báo", MessageBoxIcon.Warning);
                    txtMoTaXaPhuong.Focus();
                    txtMoTaXaPhuong.SelectAll();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
        #endregion
        private void frm_themmoi_diachinh_new_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadQH()
        {
            try
            {
                string tinhThanhId = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.MaDiachinh), -1);
                dtQH =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.MaCha).IsEqualTo(tinhThanhId).
                        ExecuteDataSet().Tables
                        [0];
                grdQuanHuyen.DataSource = dtQH;
                grdQuanHuyen.MoveFirst();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void grdTinhThanh_SelectionChanged(object sender, EventArgs e)
        {
            LoadQH();

        }
        void LoadXP()
        {
            try
            {
                string quanHuyenId = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.MaDiachinh), -1);
                dtXP =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.MaCha).IsEqualTo(quanHuyenId).
                        ExecuteDataSet().Tables
                        [0];
                grdXaPhuong.DataSource = dtXP;
                grdXaPhuong.MoveFirst();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void grdQuanHuyen_SelectionChanged(object sender, EventArgs e)
        {
            LoadXP();
           
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlQuery sqlTinhThanh =
              new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(
                  Utility.sDbnull(txtMaTinhThanh.Text.Trim(), ""));
                if (sqlTinhThanh.GetRecordCount() <= 0 && txtMaTinhThanh.TextLength > 0 && txtTenTinhthanh.TextLength > 0)//Thêm mới
                {
                    if (!IsValidDataTP()) return;
                    var objDmucDiachinhT = new DmucDiachinh();
                    objDmucDiachinhT.MaDiachinh = Utility.sDbnull(txtMaTinhThanh.Text.Trim(), "");
                    objDmucDiachinhT.TenDiachinh = Utility.sDbnull(txtTenTinhthanh.Text.Trim(), "");
                    objDmucDiachinhT.LoaiDiachinh = 0;
                    objDmucDiachinhT.MaCha = "";
                    objDmucDiachinhT.SttHthi = 0;
                    objDmucDiachinhT.MotaThem = Utility.sDbnull(txtMoTaTinhThanh.Text.Trim(), "");
                    objDmucDiachinhT.IsNew = true;
                    objDmucDiachinhT.Save();
                    Utility.SetMsg(lblMsgTP, "Thêm thành công TP", true);
                }
                else if (sqlTinhThanh.GetRecordCount() > 0)
                {
                    if (!IsValidDataTP()) return;
                    SqlQuery tinh = new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(Utility.sDbnull(txtTenTinhthanh.Text.Trim(), "")).Set(
                              DmucDiachinh.Columns.MotaThem).
                          EqualTo(Utility.sDbnull(txtMoTaTinhThanh.Text.Trim(), "")).Where(DmucDiachinh.Columns.MaDiachinh).
                          IsEqualTo(Utility.sDbnull(txtMaTinhThanh.Text.Trim(), ""));
                    tinh.Execute();
                    Utility.SetMsg(lblMsgTP, "Cập nhật thành công TP", true);
                }

                SqlQuery sqlQuanHuyen =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(
                        Utility.sDbnull(txtMaQuanHuyen.Text.Trim(), ""));
                if (sqlQuanHuyen.GetRecordCount() <= 0 && txtMaQuanHuyen.TextLength > 0 && txtTenQuanHuyen.TextLength > 0)
                {
                    if (!IsValidDataQH()) return;
                    var objDmucDiachinhH = new DmucDiachinh();
                    objDmucDiachinhH.MaDiachinh = Utility.sDbnull(txtMaQuanHuyen.Text.Trim(), "");
                    objDmucDiachinhH.TenDiachinh = Utility.sDbnull(txtTenQuanHuyen.Text.Trim(), "");
                    objDmucDiachinhH.LoaiDiachinh = 1;
                    objDmucDiachinhH.MaCha = Utility.sDbnull(txtMaTinhThanh.Text.Trim(), "");
                    objDmucDiachinhH.SttHthi = 0;
                    objDmucDiachinhH.MotaThem = Utility.sDbnull(txtMoTaQuanHuyen.Text.Trim(), "");
                    objDmucDiachinhH.IsNew = true;
                    objDmucDiachinhH.Save();
                    Utility.SetMsg(lblMsgQH, "Thêm thành công Quận/Huyện", true);
                }
                else if (sqlQuanHuyen.GetRecordCount() > 0)
                {
                    if (!IsValidDataQH()) return;
                    SqlQuery quan = new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(Utility.sDbnull(txtTenQuanHuyen.Text.Trim(), "")).Set(
                           DmucDiachinh.Columns.MotaThem).
                       EqualTo(Utility.sDbnull(txtMoTaQuanHuyen.Text.Trim(), "")).Where(DmucDiachinh.Columns.MaDiachinh).
                       IsEqualTo(Utility.sDbnull(txtMaQuanHuyen.Text.Trim(), ""));
                    quan.Execute();
                    Utility.SetMsg(lblMsgQH, "Cập nhật thành công Quận/Huyện", true);
                }


                SqlQuery sqlXaPhuong =
                    new Select().From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(
                        Utility.sDbnull(txtMaXaPhuong.Text.Trim(), ""));
                if (sqlXaPhuong.GetRecordCount() <= 0 && txtMaXaPhuong.TextLength > 0 && txtTenXaPhuong.TextLength > 0)
                {
                    if (!IsValidDataXP()) return;
                    var objDmucDiachinhX = new DmucDiachinh();
                    objDmucDiachinhX.MaDiachinh = Utility.sDbnull(txtMaXaPhuong.Text.Trim(), "");
                    objDmucDiachinhX.TenDiachinh = Utility.sDbnull(txtTenXaPhuong.Text.Trim(), "");
                    objDmucDiachinhX.LoaiDiachinh = 2;
                    objDmucDiachinhX.MaCha = Utility.sDbnull(txtMaQuanHuyen.Text.Trim(), "");
                    objDmucDiachinhX.SttHthi = 0;
                    objDmucDiachinhX.MotaThem = Utility.sDbnull(txtMoTaXaPhuong.Text.Trim(), "");
                    objDmucDiachinhX.IsNew = true;
                    objDmucDiachinhX.Save();
                    Utility.SetMsg(lblMsgXP, "Thêm thành công Xã/Phường", true);
                }
                else if (sqlXaPhuong.GetRecordCount() > 0)
                {
                    if (!IsValidDataXP()) return;
                    SqlQuery xa = new Update(DmucDiachinh.Schema).Set(DmucDiachinh.Columns.TenDiachinh).EqualTo(
                          Utility.sDbnull(txtTenXaPhuong.Text.Trim(), "")).Set(
                              DmucDiachinh.Columns.MotaThem).
                          EqualTo(Utility.sDbnull(txtMoTaXaPhuong.Text.Trim(), "")).Where(DmucDiachinh.Columns.MaDiachinh).
                          IsEqualTo(Utility.sDbnull(txtMaXaPhuong.Text.Trim(), ""));
                    xa.Execute();
                    Utility.SetMsg(lblMsgXP, "Cập nhật thành công Xã/Phường", true);
                }
                m_blnHasChanged = true;
               
                LoadData();
                grdTinhThanh_SelectionChanged(grdTinhThanh, e);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
        }

        /// <summary>
        /// Sự kiện tìm kiếm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region Tìm kiếm gần đúng
        private void txtMaTinhThanh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtMaTinhThanh.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtTP.Select("ma_diachinh ='" + txtMaTinhThanh.Text.TrimStart().TrimEnd() +
                                            "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaTinhThanh.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaTinhThanh.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtTP.DefaultView.RowFilter = "1=1";
                dtTP.DefaultView.RowFilter = rowFilter;
                dtTP.AcceptChanges();
                grdTinhThanh.MoveFirst();
                Utility.SetMsg(lblMsgTP, grdTinhThanh.GetDataRows().Length > 0 ? "Cập nhật" : "Thêm mới", true);
            }
        }

        private void txtTenTinhthanh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtTenTinhthanh.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtTP.Select("ten_diachinh ='" + txtTenTinhthanh.Text.TrimStart().TrimEnd() +
                                            "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenTinhthanh.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenTinhthanh.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtTP.DefaultView.RowFilter = "1=1";
                dtTP.DefaultView.RowFilter = rowFilter;
                dtTP.AcceptChanges();
                grdTinhThanh.MoveFirst();
                Utility.SetMsg(lblMsgTP, grdTinhThanh.GetDataRows().Length > 0 ? "Cập nhật" : "Thêm mới", true);
            }
        }

        private void txtMaQuanHuyen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtMaQuanHuyen.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtQH.Select("ma_diachinh ='" + txtMaQuanHuyen.Text.TrimStart().TrimEnd() +
                                            "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaQuanHuyen.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaQuanHuyen.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtQH.DefaultView.RowFilter = "1=1";
                dtQH.DefaultView.RowFilter = rowFilter;
                dtQH.AcceptChanges();
                grdQuanHuyen.MoveFirst();
                Utility.SetMsg(lblMsgQH,grdQuanHuyen.GetDataRows().Length>0? "Cập nhật":"Thêm mới", true);
            }
        }

        private void txtTenQuanHuyen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtTenQuanHuyen.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtQH.Select("ten_diachinh ='" + txtTenQuanHuyen.Text.TrimStart().TrimEnd() +
                                            "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenQuanHuyen.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenQuanHuyen.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtQH.DefaultView.RowFilter = "1=1";
                dtQH.DefaultView.RowFilter = rowFilter;
                dtQH.AcceptChanges();
                grdQuanHuyen.MoveFirst();
                Utility.SetMsg(lblMsgQH, grdQuanHuyen.GetDataRows().Length > 0 ? "Cập nhật" : "Thêm mới", true);
            }
        }

        private void txtMaXaPhuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtMaXaPhuong.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtXP.Select("ma_diachinh ='" + txtMaXaPhuong.Text.TrimStart().TrimEnd() +
                                           "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaXaPhuong.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ma_diachinh like '%" + txtMaXaPhuong.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtXP.DefaultView.RowFilter = "1=1";
                dtXP.DefaultView.RowFilter = rowFilter;
                dtXP.AcceptChanges();
                grdXaPhuong.MoveFirst();
                Utility.SetMsg(lblMsgXP, grdXaPhuong.GetDataRows().Length > 0 ? "Cập nhật" : "Thêm mới", true);
            }
        }

        private void txtTenXaPhuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rowFilter = "1=1";
                if (!string.IsNullOrEmpty(txtTenXaPhuong.Text))
                {
                    int rowcount = 0;
                    rowcount =
                        dtXP.Select("ten_diachinh ='" + txtTenXaPhuong.Text.TrimStart().TrimEnd() +
                                           "'").GetLength(0);
                    if (rowcount > 0)
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenXaPhuong.Text.ToUpper().TrimStart().TrimEnd() + "%'";
                    }
                    else
                    {
                        rowFilter = "ten_diachinh like '%" + txtTenXaPhuong.Text.Trim().TrimStart().TrimEnd() +
                                    "%'";
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("Lỗi trong quá trình lọc thông tin", exception);
                rowFilter = "1=2";
            }
            finally
            {
                dtXP.DefaultView.RowFilter = "1=1";
                dtXP.DefaultView.RowFilter = rowFilter;
                dtXP.AcceptChanges();
                grdXaPhuong.MoveFirst();
                Utility.SetMsg(lblMsgXP, grdXaPhuong.GetDataRows().Length > 0 ? "Cập nhật" : "Thêm mới", true);
            }
        }

        #endregion

        /// <summary>
        /// Bắt sự kiện trong Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region Bắt sự kiện trên Form
        private void grdTinhThanh_Leave(object sender, EventArgs e)
        {
        }

        private void grdTinhThanh_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void grdTinhThanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaTinhThanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
                txtTenTinhthanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
                txtMoTaTinhThanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.MotaThem), "");
                txtMaQuanHuyen.Focus();
                txtMaTinhThanh.Enabled = false;
            }
        }

        private void txtMaTinhThanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                grdTinhThanh.Focus();
                grdTinhThanh.MoveFirst();
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtTenTinhthanh.Focus();
            }
        }

        private void grdQuanHuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
                txtTenQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
                txtMoTaQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.MotaThem), "");
                txtMaQuanHuyen.Enabled = false;
                txtMaXaPhuong.Focus();
            }
        }

        private void grdXaPhuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
                txtTenXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
                txtMoTaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MotaThem), "");
                cmdSave.Focus();
            }
        }

        private void txtMaQuanHuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                grdQuanHuyen.Focus();
                grdQuanHuyen.MoveFirst();
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtTenQuanHuyen.Focus();
            }
        }

        private void txtMaXaPhuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                grdXaPhuong.Focus();
                grdXaPhuong.MoveFirst();
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtTenXaPhuong.Focus();
            }
        }

        private void txtTenTinhthanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMoTaTinhThanh.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                grdTinhThanh.Focus();
                grdTinhThanh.MoveFirst();
            }
        }

        private void txtMoTaTinhThanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaQuanHuyen.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                grdTinhThanh.Focus();
                grdTinhThanh.MoveFirst();
            }
        }

        private void txtTenQuanHuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMoTaQuanHuyen.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                grdQuanHuyen.Focus();
                grdQuanHuyen.MoveFirst();
            }
        }

        private void txtMoTaQuanHuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaXaPhuong.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                grdQuanHuyen.Focus();
                grdQuanHuyen.MoveFirst();
            }
        }

        private void txtTenXaPhuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMoTaXaPhuong.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                grdXaPhuong.Focus();
                grdXaPhuong.MoveFirst();
            }
        }

        private void txtMoTaXaPhuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSave.Focus();
            } 
            if (e.KeyCode == Keys.Down)
            {
                grdXaPhuong.Focus();
                grdXaPhuong.MoveFirst();
            }
           
        }

        #endregion

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtMaTinhThanh.Clear();
            txtTenTinhthanh.Clear();
            txtMoTaTinhThanh.Clear();
            txtMaQuanHuyen.Clear();
            txtTenQuanHuyen.Clear();
            txtMoTaQuanHuyen.Clear();
            txtMaXaPhuong.Clear();
            txtTenXaPhuong.Clear();
            txtMoTaXaPhuong.Clear();
            txtMaTinhThanh.Enabled = true;
            txtMaQuanHuyen.Enabled = true;
            lblMessge.Text = "";
        }

        private void grdXaPhuong_SelectionChanged(object sender, EventArgs e)
        {
            //if (grdXaPhuong.CurrentRow != null)
            //{
            //    txtMaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MaDiachinh), -1);
            //    txtTenXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.TenDiachinh), -1);
            //    txtMoTaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MotaThem), -1);

            //}
        }

        private void grdTinhThanh_DoubleClick(object sender, EventArgs e)
        {
            txtMaTinhThanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
            txtTenTinhthanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
            txtMoTaTinhThanh.Text = Utility.sDbnull(grdTinhThanh.GetValue(DmucDiachinh.Columns.MotaThem), "");
            txtMaQuanHuyen.Focus();
            txtMaTinhThanh.Enabled = false;
        }

        private void grdQuanHuyen_DoubleClick(object sender, EventArgs e)
        {
            txtMaQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
            txtTenQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
            txtMoTaQuanHuyen.Text = Utility.sDbnull(grdQuanHuyen.GetValue(DmucDiachinh.Columns.MotaThem), "");
            txtMaQuanHuyen.Enabled = false;
            txtMaXaPhuong.Focus();
        }

        private void grdXaPhuong_DoubleClick(object sender, EventArgs e)
        {
            txtMaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MaDiachinh), "");
            txtTenXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.TenDiachinh), "");
            txtMoTaXaPhuong.Text = Utility.sDbnull(grdXaPhuong.GetValue(DmucDiachinh.Columns.MotaThem), "");
            cmdSave.Focus();
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}