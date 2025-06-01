using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using VNS.HIS.BusRule.Goikham;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using NLog;
using VNS.HIS.UI.Forms.NGOAITRU;
using Janus.Windows.GridEX.EditControls;
using System.Drawing;

namespace VNS.HIS.UI.GOIKHAM
{
    public partial class frm_QuanLyGoiKham : Form
    {
        public string PatientCode;
        private long v_Payment_ID = -1;
        private KcbLuotkham objLuotkham;
        private clsGoikham _goiKhamService;
        private DataTable _dtDanhSachBN;
        private DataTable _dtGoiKham;
        private DataTable _dtGoiKhamTheoBNCaNhan;
        private DataTable _dtGoiKhamTheoBNTheoLo;
        private DataTable _dtThongTinGoiKhamCaNhan;
        private DataTable _dtThongTinGoiKhamTheoLo;
        private DataTable _dtLichSuThanhToanCaNhan;
        private DataTable _dtDanhSachBNTheoLo;
        private byte _hosStatus;
        private BackgroundWorker _bw = new BackgroundWorker();
        GoiDangki objdangkyGoi = null;
        private bool Success = true;
        bool AllowSelectionChanged = false;
        public frm_QuanLyGoiKham()
        {
            InitializeComponent();
            log = LogManager.GetLogger(Name);
            Utility.SetVisualStyle(this);
            _goiKhamService = new clsGoikham();
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            _bw.ProgressChanged += bw_ProgressChanged;
            _bw.WorkerReportsProgress = true;
            cmdThanhToan.Visible =
                cmdKetthucgoi.Visible =
                    cmdKichhoatgoi.Visible = Utility.Coquyen("GOI_QUYEN_THANHTOAN");
            grdPayment.MouseDoubleClick+=grdPayment_MouseDoubleClick;
            grdPayment.ColumnButtonClick+=grdPayment_ColumnButtonClick;
            grdPayment.CellValueChanged += grdPayment_CellValueChanged;
            grdPayment.UpdatingCell+=grdPayment_UpdatingCell;
            dtNgayKhamLai.Value = dtpNgaythanhtoan.Value = DateTime.Now;
            autoGoikham._OnEnterMe += autoGoikham__OnEnterMe;
            txtTienThu.LostFocus += txtTienThu_LostFocus;
            cboPttt.SelectedIndexChanged+=cboPttt_SelectedIndexChanged;
            ucThongtinnguoibenh_doc_v71._OnEnterMe += ucThongtinnguoibenh_doc_v71__OnEnterMe;
        }

       
        void grdPayment_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "ghichu")
            {
                Suaghichu();
            }
        }
        void Suaghichu()
        {
            try
            {
                long id_thanhtoan = Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan));
                KcbThanhtoan _objthanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                if (_objthanhtoan == null)
                {
                    Utility.ShowMsg("Không tìm được bản ghi thanh toán(Có thể vừa bị xóa khỏi hệ thống). Vui lòng chọn lại người bệnh để refresh lại");
                    return;
                }
                string oldValue = Utility.sDbnull(_objthanhtoan.Ghichu, "");
                string newValue = Utility.sDbnull(grdPayment.GetValue("ghichu"), "");
                int numofA = new Update(KcbThanhtoan.Schema).Set(KcbThanhtoan.Columns.Ghichu).EqualTo(newValue).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(id_thanhtoan).Execute();
                if (numofA > 0)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin ghi chú của bản tin thanh toán Id={0}, ghi chú cũ={1}, ghi chú mới ={2} thành công ", id_thanhtoan.ToString(), oldValue, newValue), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Cập nhật ghi chú thành công. Nhấn OK để kết thúc");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void grdPayment_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (!Kiemtradieukienhuytt_theongay()) return;
            if (e.Column.Key == KcbThanhtoan.Columns.NgayThanhtoan)
            {
                UpdatePaymentDate();
            }

            else if (e.Column.Key == "ma_pttt")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }
                //Cập nhật lại PTTT
                if (!Utility.isValidGrid(grdPayment)) return;

                CapnhatPTTT_Grid();
            }
            else if (e.Column.Key == "ma_nganhang")
            {
                if (!Utility.Coquyen("THANHTOAN_SUA_PTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền sửa đổi phương thức thanh toán (THANHTOAN_SUA_PTTT). Vui lòng liên hệ quản trị hệ thống để được phân quyền");
                    return;
                }

                string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                string ma_nganhangcu = grdPayment.CurrentRow.Cells["ma_nganhang"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");

                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    return;
                }
                CapnhatPTTT_Grid();
            }
        }
        private void UpdatePaymentDate()
        {
            if (
                Utility.AcceptQuestion(
                    "Bạn có muốn thay đổi thông tin chỉnh ngày thanh toán. Việc chỉnh ngày có thể sẽ ảnh hưởng tới báo cáo thanh toán theo ngày",
                    "Thông báo", true))
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objPayment != null)
                {
                    DateTime newDate = Convert.ToDateTime(grdPayment.GetValue(KcbThanhtoan.Columns.NgayThanhtoan));
                    if (!globalVariables.isSuperAdmin)
                    {
                        if (objPayment.MaxNgayTao.HasValue && newDate.Date <= objPayment.MaxNgayTao.Value.Date)
                        {
                            Utility.ShowMsg(string.Format("Bạn cần chọn ngày thanh toán cần >= {0}", objPayment.MaxNgayTao.Value.ToString("dd/MM/yyyy")), "Thông báo");
                            return;
                        }
                    }
                    objPayment.NgayThanhtoan = newDate;
                    ActionResult actionResult = _THANHTOAN.UpdateNgayThanhtoan(objPayment);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.ShowMsg("Bạn chỉnh sửa ngày thanh toán thành công", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
        bool Kiemtradieukienhuytt_theongay()
        {
            KcbThanhtoan _vobjTT = KcbThanhtoan.FetchByID(Utility.Int64Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan)));
            if (_vobjTT == null)
            {
                Utility.ShowMsg("Không lấy được thông tin thanh toán từ lưới danh sách phiếu thu. Vui lòng kiểm tra lại");
                return false;
            }
            int kcbThanhtoanSongayHuythanhtoan =
                           Utility.Int32Dbnull(
                               THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            var chenhlech =
                (int)Math.Ceiling((globalVariables.SysDate.Date - _vobjTT.NgayThanhtoan.Date).TotalDays);
            if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán: {0}. Hệ thống không cho phép bạn thay đổi thông tin thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", _vobjTT.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                return false;
            }
            if (Utility.Byte2Bool(_vobjTT.TrangthaiChot))
            {
                Utility.ShowMsg("Thanh toán đang chọn đã được chốt nên bạn không thể thay đổi thông tin thanh toán. Vui lòng kiểm tra lại!");
                return false;
            }
            return true;
        }
        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }
        void txtTienThu_LostFocus(object sender, EventArgs e)
        {
            ModifyCommands();
        }

        void autoGoikham__OnEnterMe()
        {
            cmdThemgoi.Focus();
            ModifyCommands();
        }

       

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (Success)
            //{
            //    btnTimKiemTheoLo.PerformClick();
            //    Utility.ShowMsg("Cập nhật thành công");


            //}
            //else
            //{
            //    Utility.ShowMsg("Cập nhật không thành công. Vui lòng thử lại", "Thông báo", MessageBoxIcon.Warning);
            //}
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{

            //    var option = new TransactionOptions();
            //    option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
            //    option.Timeout = TimeSpan.FromMinutes(55);
            //    using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            //    {
            //        // 1: Thêm gói theo lô
            //        // 2: Xác nhân thanh toán gói theo lô
            //        // 3: Kích hoạt gói theo lô
            //        var workType = Utility.Int32Dbnull(e.Argument);
            //        var i = 0;
            //        switch (workType)
            //        {
            //            case 1:
            //                var idGoiDVu = Utility.Int32Dbnull(cboDanhSachGoiTheoLo.Value);
            //                var hieuLucTu = dtpHieuLucTuTheoLo.Value;
            //                var hieuLucDen = dtpHieuLucDenTheoLo.Value;
            //                var tienGoi = Utility.DecimaltoDbnull(_dtThongTinGoiKhamTheoLo.Rows[0]["so_tien"]);
            //                var mienGiam = Utility.DecimaltoDbnull(_dtThongTinGoiKhamTheoLo.Rows[0]["MIEN_GIAM"]);
            //                var giamTruBhyt = Utility.DecimaltoDbnull(_dtThongTinGoiKhamTheoLo.Rows[0]["giam_bhyt"]);
            //                foreach (GridEXRow row in grdDanhSachBNTheoLo.GetCheckedRows())
            //                {
            //                    _goiKhamService.ThemGoiKhamChoBN(Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value),
            //                                                     idGoiDVu, tienGoi, mienGiam, giamTruBhyt, hieuLucTu,
            //                                                     hieuLucDen, txtSoLo.Text);
            //                    int percentage = (i + 1) * 100 / grdDanhSachBNTheoLo.GetCheckedRows().Count();
            //                    _bw.ReportProgress(percentage);
            //                    i++;
            //                }
            //                grdDanhSachBNTheoLo.MoveFirst();
            //                TinhTongTienTheoLo();
            //                break;

            //            case 2:
            //                foreach (DataRow row in _dtDanhSachBNTheoLo.Rows)
            //                {
            //                    var patientId = Utility.Int32Dbnull(row["id_benhnhan"]);
            //                    var patientCode = Utility.sDbnull(row["ma_luotkham"]);
            //                    var hosStatus = Utility.ByteDbnull(row["trangthai_ngoaitru"]);
            //                    var depositCode = THU_VIEN_CHUNG.TaoMathanhtoan(DateTime.Now);
            //                    var ngayGioThanhToan = DateTime.Now;
            //                    var dtGoiKhamTheoBN = _goiKhamService.LayGoiKhamTheoBN(patientId, txtSoLo.Text);
            //                    foreach (DataRow dr in dtGoiKhamTheoBN.Rows)
            //                    {
            //                        var v_Id_dangky = Utility.Int32Dbnull(dr["id_dangky"]);
            //                        var idGoiDvu = Utility.Int32Dbnull(dr["id_goi"]);
            //                        var tienThu = Utility.DecimaltoDbnull(dr["THANH_TIEN"]);
            //                        _goiKhamService.ThanhToanGoi(idGoiDvu, v_Id_dangky, patientId, patientCode,
            //                                                     tienThu,
            //                                                     3, hosStatus,
            //                                                     depositCode, ngayGioThanhToan, cboDanhSachGoiTheoLo.Text);

            //                        _goiKhamService.CapNhatTrangThaiThanhToan(v_Id_dangky,true);
            //                    }

            //                    int percentage = (i + 1) * 100 / _dtDanhSachBNTheoLo.Rows.Count;
            //                    _bw.ReportProgress(percentage);
            //                    i++;
            //                }
            //                break;
            //            case 3:
            //                foreach (DataRow row in _dtDanhSachBNTheoLo.Rows)
            //                {
            //                    var patientId = Utility.Int32Dbnull(row["id_benhnhan"]);
            //                    var dtGoiKhamTheoBN = _goiKhamService.LayGoiKhamTheoBN(patientId, txtSoLo.Text);
            //                    foreach (DataRow dr in dtGoiKhamTheoBN.Rows)
            //                    {
            //                        var v_Id_dangky = Utility.Int32Dbnull(dr["id_dangky"]);
            //                        _goiKhamService.KichHoatGoiChoBN(v_Id_dangky);
            //                    }
            //                    int percentage = (i + 1) * 100 / _dtDanhSachBNTheoLo.Rows.Count;
            //                    _bw.ReportProgress(percentage);
            //                    i++;
            //                }
            //                break;
            //        }
            //        scope.Complete();
            //        Success = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Success = false;
            //    Utility.ShowMsg(ex.Message);
            //}
        }

        private void TinhTongTienTheoLo()
        {
            try
            {
                lblTongTienTheoLo.Text = string.Format("{0:#,##0}", _goiKhamService.LayTongTienTheoLo(txtSoLo.Text));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private DataTable m_dtHinhThucTT;
        private void frm_QuanLyGoiKham_Load(object sender, EventArgs e)
        {
            try
            {
                setProperties();
                //m_dtHinhThucTT = LoadDataCommon.CommonBusiness.LoadHinhThucThanhToan(true);
                //cboHinhThucThanhToan.DataSource = m_dtHinhThucTT;
                //cboHinhThucThanhToan.ValueMember = LHinhThucTT.Columns.IdHinhThuc;
                //cboHinhThucThanhToan.DisplayMember = LHinhThucTT.Columns.TenHinhThuc;
                //cbohinhthucthukhac.DataSource = m_dtHinhThucTT;
                //cbohinhthucthukhac.ValueMember = LHinhThucTT.Columns.IdHinhThuc;
                //cbohinhthucthukhac.DisplayMember = LHinhThucTT.Columns.TenHinhThuc; 
                LoadPtttNganhang();
                InitPTTTColumns();
                lblTongTienTheoLo.Text = string.Empty;

                dtpNgaydangky.Value = dtpNgaythanhtoan.Value = dtpHieuLucTuCaNhan.Value = DateTime.Now;
                dtpHieuLucDenCaNhan.Value = DateTime.Now;
                //cboHinhThucThanhToan.SelectedIndex = 0;
                LoadDanhSachGoiKhamHieuLuc();
                ucThongtinnguoibenh_doc_v71.txtMaluotkham.Focus();
                if (!string.IsNullOrEmpty(PatientCode))
                {
                    tabControl.TabPages["tabTheoLo"].TabVisible = false;
                    ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text = PatientCode;
                    ucThongtinnguoibenh_doc_v71.Refresh(true);
                }

                txtGhiChu.LostFocus += txtGhiChu_LostFocus;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                AllowSelectionChanged = true;
                grdList_SelectionChanged(grdList, e);
            }
        }
        private void setProperties()
        {
            try
            {
                dtpNgaythanhtoan.Enabled = Utility.Coquyen("thanhtoan_suangaythanhtoan");
                foreach (Control control in pnlAct.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();

                        txtFormantTongTien = ((EditBox)(control));
                        if (txtFormantTongTien.Name != txtsothethamchieu.Name && txtFormantTongTien.Name != txtTienThu.Name)
                        {
                            txtFormantTongTien.Clear();
                            txtFormantTongTien.ReadOnly = true;
                            //if (txtFormantTongTien.Font.Size < 9)
                            //    txtFormantTongTien.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold,
                            //        GraphicsUnit.Point, 0);
                            txtFormantTongTien.TextAlignment = TextAlignment.Far;
                            txtFormantTongTien.KeyPress += txtEventTongTien_KeyPress;
                            txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
        DataTable dtPttt = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            cboPttt.DataSource = dtPttt;
            cboPttt.ValueMember = DmucChung.Columns.Ma;
            cboPttt.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
        }
        #region Cập nhật phân bổ PTTT
        void InitPTTTColumns()
        {
            try
            {
                DataTable dtPTTT = dtPttt;
                GridEXColumn _colmaPttt = grdPayment.RootTable.Columns["ma_pttt"];
                _colmaPttt.HasValueList = true;
                _colmaPttt.LimitToList = true;

                GridEXValueListItemCollection _colmaPttt_Collection = grdPayment.RootTable.Columns["ma_pttt"].ValueList;
                foreach (DataRow item in dtPTTT.Rows)
                {
                    _colmaPttt_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }

                DataTable dtNganhang = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("NGANHANG").And(DmucChung.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
                GridEXColumn _colIDNganHang = grdPayment.RootTable.Columns["ma_nganhang"];
                _colIDNganHang.HasValueList = true;
                _colIDNganHang.LimitToList = true;

                GridEXValueListItemCollection _colIDNganHang_Collection = grdPayment.RootTable.Columns["ma_nganhang"].ValueList;
                foreach (DataRow item in dtNganhang.Rows)
                {
                    _colIDNganHang_Collection.Add(item["MA"].ToString(), item["TEN"].ToString());
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }

        void mnuCapnhatPTTT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPayment)) return;

            CapnhatPTTT();
        }
        bool isValidPttt_Nganhang_Grid()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
            string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
            if (lstPTTT.Contains(ma_pttt) && (ma_nganhang.Length <= 0 || ma_nganhang == "-1"))
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", ma_pttt));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }

        bool isValidPttt_Nganhang()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            if (cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue, "") == "")
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                cboNganhang.Focus();
                return false;
            }
            return true;
        }
        bool Phanbo()
        {
            if (!Utility.isValidGrid(grdPayment)) return false;
            v_Payment_ID = Utility.Int64Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(v_Payment_ID, -1, -1, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            return _PhanbotientheoPTTT.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdPayment.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtPayment.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        void CapnhatPTTT_Grid()
        {
            try
            {
                List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                Int32 IdThanhtoan = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
                string ma_pttt = grdPayment.GetValue("ma_pttt").ToString();
                string ma_pttt_cu = objThanhtoan.MaPttt;
                string ten_pttt_cu = grdPayment.CurrentRow.Cells["ten_pttt"].Value.ToString();
                string ma_nganhang = Utility.sDbnull(grdPayment.GetValue("ma_nganhang").ToString(), "-1");
                if (!lstPTTT.Contains(ma_pttt))
                    if (ma_pttt == ma_pttt_cu) return;
                
                DataRow[] arrDr = m_dtPayment.Select("id_thanhtoan=" + IdThanhtoan.ToString());
                if (arrDr.Length <= 0) return;
                //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                //if (cboNganhang.Enabled && cboNganhang.SelectedValue.ToString() == "-1")
                //{
                //    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                //    cboNganhang.Focus();
                //    return;
                //}
               
                
                 if (!lstPTTT.Contains(ma_pttt))
                     ma_nganhang="";
                if (lstPTTT.Contains(ma_pttt) && (ma_nganhang == "-1" || ma_nganhang.Length == 0))//Đợi chọn ngân hàng
                {
                    if (objThanhtoan.KieuThanhtoan == 1)
                    {
                        Utility.ShowMsg("Bạn cần chọn ngân hàng");
                        Utility.focusCell(grdPayment, "ma_nganhang");
                        return;
                    }
                }
                if (!isValidPttt_Nganhang_Grid())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], ma_pttt), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(IdThanhtoan, -1l, -1l).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(ma_pttt))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion(string.Format("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", ma_pttt, ma_pttt), "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objThanhtoan, ma_pttt, ma_nganhang);
                    }


                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void CapnhatPTTT()
        {
            try
            {
                
                Int32 IdThanhtoan=Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(IdThanhtoan);
                DataRow[] arrDr = m_dtPayment.Select("id_thanhtoan=" + IdThanhtoan.ToString());
                if (arrDr.Length <= 0) return;
                //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
                //if (cboNganhang.Enabled && cboNganhang.SelectedValue.ToString() == "-1")
                //{
                //    Utility.ShowMsg(string.Format("Bạn phải chọn ngân hàng khi chọn phương thức thanh toán {0}", cboPttt.Text));
                //    cboNganhang.Focus();
                //    return;
                //}
                if (!isValidPttt_Nganhang())
                    return;
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thay đổi phương thức thanh toán từ {0} sang {1}", arrDr[0]["ten_pttt"], cboPttt.Text), "Xác nhận cập nhật PTTT", true))
                {
                    Utility.ShowMsg("Bạn vừa chọn hủy cập nhật phương thức thanh toán. Nhấn OK để kết thúc");
                    return;
                }
                List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                DataTable dtPhanbo = _THANHTOAN.KcbThanhtoanLaydulieuphanbothanhtoanTheoPttt(IdThanhtoan,-1l,-1l).Tables[0];
                //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                if (lstPhanbo.Contains(cboPttt.SelectedValue.ToString()))
                {
                    if (dtPhanbo.Select("so_tien>0").Length <= 1)
                        Phanbo();
                }
                else
                {
                    if (dtPhanbo.Select("so_tien>0").Length > 1)//Không phải chọn phân bổ mà số lượng dòng phân bổ >1 thì thông báo người dùng lựa chọn
                    {
                        if (Utility.AcceptQuestion("Lần phân bổ gần nhất bạn đang phân bổ tiền cho nhiều phương thức thanh toán. Trong khi hình thức thanh toán bạn đang chọn ({0}) không phải là phân bổ. Bạn có muốn hệ thống tự động chuyển tất cả số tiền sang hình thức vừa chọn {1} hay không?\n.Nhấn Yes để đồng ý. Nhấn No để xem lại thông tin phân bổ", "Cảnh báo và gợi ý", true))
                        {
                            Capnhatphanbo11(dtPhanbo, objThanhtoan, cboPttt.SelectedValue.ToString(), cboNganhang.Enabled ?  Utility.sDbnull( cboNganhang.SelectedValue,"")  : "");
                        }
                        else
                            Phanbo();
                    }
                    else if (dtPhanbo.Select("so_tien>0").Length == 1)//Thực hiện cập nhật luôn
                    {
                        Capnhatphanbo11(dtPhanbo, objThanhtoan, cboPttt.SelectedValue.ToString(), cboNganhang.Enabled ?  Utility.sDbnull( cboNganhang.SelectedValue,"")  : "");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void Capnhatphanbo11(DataTable dtPhanbo, KcbThanhtoan objThanhtoan, string ma_pttt, string ma_nganhang)
        {
            decimal so_tien = Utility.DecimaltoDbnull(dtPhanbo.Compute("sum(so_tien)", "1=1"), 0);

            using (var scope = new TransactionScope())
            {
                using (var sh = new SharedDbConnectionScope())
                {

                    new Delete().From(KcbThanhtoanPhanbotheoPTTT.Schema).Where(KcbThanhtoanPhanbotheoPTTT.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan).Execute();

                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(objThanhtoan.IdThanhtoan);
                    if (objPayment != null)
                    {
                        objPayment.MaPttt = ma_pttt;
                        objPayment.MaNganhang = ma_nganhang;
                        objPayment.MarkOld();
                        objPayment.IsNew = false;
                        objPayment.Save();
                        SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(objThanhtoan.IdThanhtoan, -1l, -1l, objPayment.MaPttt, objPayment.MaNganhang,
          objThanhtoan.IdBenhnhan, objThanhtoan.MaLuotkham,
          objThanhtoan.NoiTru, so_tien, so_tien,
          globalVariables.UserName, DateTime.Now, "", DateTime.Now, -1, 0, (byte)1).Execute();
                    }
                }
                scope.Complete();
            }
            foreach (DataRow dr in m_dtPayment.Rows)
                if (dr["id_thanhtoan"].ToString() == objThanhtoan.IdThanhtoan.ToString())
                {
                    dr["ma_pttt"] = ma_pttt;
                    dr["ten_pttt"] = cboPttt.Text;
                    dr["ma_nganhang"] = ma_nganhang;
                    dr["ten_nganhang"] = ma_nganhang;
                }
            m_dtPayment.AcceptChanges();
            Utility.ShowMsg("Cập nhật thông tin hình thức thanh toán thành công");
        }
        #endregion
        void txtGhiChu_LostFocus(object sender, EventArgs e)
        {
            try
            {
                var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                _goiKhamService.ThemGhiChu(v_Id_dangky, txtGhiChu.Text);
                grdList.SetValue("Ghi_Chu", txtGhiChu.Text);
                grdList.UpdateData();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void LoadDanhSachGoiKhamHieuLuc()
        {
            try
            {
                _dtGoiKham = _goiKhamService.LayDanhSachGoiKham(-1, "-1", "", 1, 0, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),Utility.ByteDbnull(1));//Lấy gói trừ dần
                //_dtGoiKham.DefaultView.RowFilter = "trang_thai = 1";
                //cboDanhSachGoiTheoLo.DataSource = _dtGoiKham.DefaultView;
                //cboDanhSachGoiTheoLo.ValueMember = "id_goi";
                //cboDanhSachGoiTheoLo.DisplayMember = "ten_goi";
                //cboDanhSachGoiTheoLo.DataMember = "ma_goi";
                //if (_dtGoiKham.Rows.Count > 0)
                //{
                //    cboDanhSachGoiTheoLo.SelectedIndex = 0;
                //}
                //else
                //{
                //    Utility.ShowMsg("Chưa có danh mục gói trong hệ thống");
                //    return;
                //}
                autoGoikham.Init(_dtGoiKham,
                    new List<string>()
                    {
                        GoiDanhsach.Columns.IdGoi,
                        GoiDanhsach.Columns.MaGoi,
                        GoiDanhsach.Columns.TenGoi
                    });
                //cboDanhSachGoiCaNhan.DataSource = _dtGoiKham.DefaultView;
                //cboDanhSachGoiCaNhan.ValueMember = "id_goi";
                //cboDanhSachGoiCaNhan.DisplayMember = "ten_goi";
                //cboDanhSachGoiCaNhan.DataMember = "MA_GOI_DVU";
                if (_dtGoiKham.Rows.Count > 0)
                {
                  autoGoikham.SetId(-1);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void frm_QuanLyGoiKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            else if (e.KeyCode == Keys.Escape)
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thoát khỏi chức năng đăng ký gói khám cho người bệnh?"), "Cảnh báo", true))
                    this.Close();
            }
            else if (e.Control && e.KeyCode == Keys.P) cmdInGoi.PerformClick();
        }

        private void btnKichHoatGoiCaNhan_Click(object sender, EventArgs e)
        {
            ctxKichhoat.Show(cmdKichhoatgoi, new Point(0, cmdKichhoatgoi.Height));
            
        }
        
        private void btnChonFileExcel_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void btnTimKiemTheoLo_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtSoLo.Text.Trim()))
                //{
                //    Utility.ShowMsg("Đề nghị nhập số lô");
                //    return;
                //}
                //_dtDanhSachBNTheoLo = _goiKhamService.LayDanhSachKSKTheoLo(txtSoLo.Text);
                //grdDanhSachBNTheoLo.DataSource = _dtDanhSachBNTheoLo;
                //if (_dtDanhSachBNTheoLo.Rows.Count > 0)
                //{
                //    grdDanhSachBNTheoLo.CheckAllRecords();
                //    grdDanhSachBNTheoLo.MoveFirst();
                //    btnXacNhanThanhToanTheoLo.Enabled = _goiKhamService.LayXacNhanThanhToanTheoLo(txtSoLo.Text);
                //    btnKichHoatGoiTheoLo.Enabled = _goiKhamService.LayXacNhanKichHoatTheoLo(txtSoLo.Text);
                //    btnThemGoiTheoLo.Enabled = true;
                //    TinhTongTienTheoLo();
                //}
                //else
                //{
                //    btnXacNhanThanhToanTheoLo.Enabled = false;
                //    btnKichHoatGoiTheoLo.Enabled = false;
                //    btnThemGoiTheoLo.Enabled = false;
                //}
                ////cmdDeleteLo.Enabled = true;
                ////DataTable dtAssignDetail =
                ////   SPs.NoitietLayAssigndetail(Utility.Int32Dbnull(grdData.GetValue("Assign_id"))).GetDataSet().Tables[0];
                ////if (dtAssignDetail != null)
                ////{
                ////    Utility.SetDataSourceForDataGridEx(grdAssignDetail, dtAssignDetail, true, true, "1=1", "");
                ////}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void btnThemGoiCaNhan_Click(object sender, EventArgs e)
        {
            try
            {
                var idGoiDVu = Utility.Int32Dbnull(autoGoikham.MyID,-1);
                _dtThongTinGoiKhamCaNhan = _goiKhamService.LayThongTinGoiKham(idGoiDVu);
                if (idGoiDVu <= 0)
                {
                    Utility.ShowMsg("Bạn chưa chọn gói dịch vụ cho người bệnh");
                    autoGoikham.Focus();
                    return;
                }
                var hieuLucTu = dtpHieuLucTuCaNhan.Value;
                var hieuLucDen = dtpHieuLucDenCaNhan.Value;

                if (hieuLucTu > hieuLucDen)
                {
                    Utility.ShowMsg("Ngày hiệu lực từ đang lớn hơn ngày hiệu lực đến, bạn cần chỉnh sửa lại");
                    dtpHieuLucDenCaNhan.Focus();
                    return;
                }
                if (dtpNgaydangky.Value.Date < hieuLucTu.Date)
                {
                    Utility.ShowMsg(string.Format("Ngày đăng ký gói phải lớn hơn hoặc bằng ngày bắt đầu hiệu lực của gói {0}", hieuLucTu.ToString("dd/MM/yyyy")));
                    dtpHieuLucTuCaNhan.Focus();
                    return;
                }
                if (dtpNgaydangky.Value.Date > hieuLucDen.Date )
                {
                    Utility.ShowMsg(string.Format("Ngày đăng ký gói phải nhỏ hơn hoặc bằng ngày kết thúc hiệu lực của gói {0}", hieuLucDen.ToString("dd/MM/yyyy")));
                    dtpHieuLucDenCaNhan.Focus();
                    return;
                }
                if (_dtThongTinGoiKhamCaNhan.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"])))
                    {
                        dtpHieuLucTuCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"]).Date;
                        dtpHieuLucTuCaNhan.Enabled = false;
                    }
                    else
                    {
                        dtpHieuLucTuCaNhan.Value = DateTime.Now.Date;
                        dtpHieuLucTuCaNhan.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"])))
                    {
                        dtpHieuLucDenCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"]).Date;
                        dtpHieuLucDenCaNhan.Enabled = false;
                    }
                    else
                    {
                        dtpHieuLucDenCaNhan.Value = DateTime.Now.Date;
                        dtpHieuLucDenCaNhan.Enabled = true;
                    }
                }
                if (_dtThongTinGoiKhamCaNhan.Rows.Count <= 0)
                {
                    Utility.ShowMsg(string.Format("Gói khám {0} không tồn tại ", Utility.sDbnull(autoGoikham.Text)));
                    autoGoikham.Focus();
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thêm gói khám {0} cho bệnh nhân {1} không ?",
                    Utility.sDbnull(autoGoikham.Text), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                        {
                            var tienGoi = Utility.DecimaltoDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["SO_TIEN"]);
                            var mienGiam = Utility.DecimaltoDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["MIEN_GIAM"]);
                            var giamTruBhyt = Utility.DecimaltoDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["GIAM_BHYT"]);
                            var v_Id_dangky = _goiKhamService.ThemGoiKhamChoBN((int)objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham, idGoiDVu,
                                                                         tienGoi, mienGiam, giamTruBhyt, hieuLucTu,
                                                                         hieuLucDen,dtpNgaydangky.Value, null);

                            var newRow = _dtGoiKhamTheoBNCaNhan.NewRow();
                            newRow["id_dangky"] = v_Id_dangky;
                            newRow["id_goi"] = idGoiDVu;
                            newRow["MA_GOI"] = _dtThongTinGoiKhamCaNhan.Rows[0]["MA_GOI"];
                            newRow["ten_goi"] = autoGoikham.Text;
                            newRow["THANH_TIEN"] = _dtThongTinGoiKhamCaNhan.Rows[0]["THANH_TIEN"];
                            newRow["SO_TIEN"] = _dtThongTinGoiKhamCaNhan.Rows[0]["SO_TIEN"];
                            newRow["MIEN_GIAM"] = _dtThongTinGoiKhamCaNhan.Rows[0]["MIEN_GIAM"];
                            newRow["GIAM_BHYT"] = _dtThongTinGoiKhamCaNhan.Rows[0]["GIAM_BHYT"];
                            newRow["HieuLuc_TuNgay"] = hieuLucTu;
                            newRow["HieuLuc_DenNgay"] = hieuLucDen;
                            newRow["tthai_kichhoat"] = false;
                            _dtGoiKhamTheoBNCaNhan.Rows.Add(newRow);
                            _dtGoiKhamTheoBNCaNhan.AcceptChanges();
                            grdList.UpdateData();
                            grdList.MoveLast();
                           
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }
        void ucThongtinnguoibenh_doc_v71__OnEnterMe()
        {
            try
            {
                objLuotkham = ucThongtinnguoibenh_doc_v71.objLuotkham;
                if (ucThongtinnguoibenh_doc_v71.objLuotkham != null)
                {
                    objLuotkham = ucThongtinnguoibenh_doc_v71.objLuotkham;
                    _dtGoiKhamTheoBNCaNhan = _goiKhamService.LayGoiKhamTheoBN(objLuotkham.IdBenhnhan, "-1");
                    grdList.DataSource = _dtGoiKhamTheoBNCaNhan;
                    cmdThemgoi.Enabled = true;
                    txtGhiChu.Enabled = true;
                }
                else
                {
                    cmdThemgoi.Enabled = false;
                    txtGhiChu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                AllowSelectionChanged = true;
                grdList_SelectionChanged(grdList, new EventArgs());
                ModifyCommands();
            }
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text)) return;

            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        objdangkyGoi = null;
            //        ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text = Utility.AutoFullPatientCode(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text); ;

            //        if (!string.IsNullOrEmpty(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text))
            //        {

            //            var dtPatientInfo = _goiKhamService.LayThongTinBNTheoPatientCode(Utility.sDbnull(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text));
            //            if (dtPatientInfo.Rows.Count > 0)
            //            {

            //                txtIdBenhnhan.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["id_benhnhan"]);

            //                ucThongtinnguoibenh_doc_v71.txtTenBN.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["ten_benhnhan"]);
            //                txtGioiTinh.Text = Utility.Int32Dbnull(dtPatientInfo.Rows[0]["gioi_tinh"], 0) == 0 ? "Nam" : "Nữ";
            //                dtpNgaysinh.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["ngay_sinh"]);
            //                txtNamsinh.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["nam_sinh"]);
            //                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
            //                                                       Utility.Int32Dbnull(txtNamsinh.Text));
            //                _hosStatus = Utility.ByteDbnull(dtPatientInfo.Rows[0]["trangthai_noitru"]);
            //                switch (_hosStatus)
            //                {
            //                    case 1:
            //                        txtTrangthainoitru.Text = "Nội trú";
            //                        break;
            //                    case 0:
            //                        txtTrangthainoitru.Text = "Ngoại trú";
            //                        break;
            //                    case 4:
            //                        txtTrangthainoitru.Text = "Ra viện";
            //                        break;
            //                }
            //                txtDiaChi.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["dia_chi"]);
            //                txtTendoituongkcb.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["ten_doituong_kcb"]);
            //                txtBHTT.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["chiet_khau"]);
            //                txtSoBHYT.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["mathe_bhyt"]);
            //                txtSoTheBN.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["so_the"]);
            //                dtpBHYTToDate.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["ngayketthuc_bhyt"]);
            //                dtNgayKhamLai.Text = Utility.sDbnull(dtPatientInfo.Rows[0]["NGAY_KHAM_LAI"]);
            //                if (Utility.Int32Dbnull(dtPatientInfo.Rows[0]["so_ngayhen"]) > 0) dtNgayKhamLai.Enabled = true;
            //                else
            //                {
            //                    dtNgayKhamLai.Enabled = false;
            //                }

            //                _dtGoiKhamTheoBNCaNhan = _goiKhamService.LayGoiKhamTheoBN(objLuotkham.IdBenhnhan, "-1");
            //                grdList.DataSource = _dtGoiKhamTheoBNCaNhan;

            //                cmdThemgoi.Enabled = true;
            //                txtGhiChu.Enabled = true;
            //                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtIdBenhnhan.Text, -1), Utility.sDbnull(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text));
            //            }
            //            else
            //            {
            //                cmdThemgoi.Enabled = false;
            //                txtGhiChu.Enabled = false;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            //}
            //finally
            //{
            //    AllowSelectionChanged = true;
            //    grdList_SelectionChanged(grdList, new EventArgs());
            //    ModifyCommands();
            //}

        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPatient_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        void ModifyCommands()
        {
            if (!Utility.isValidGrid(grdList)) return;
            if (objdangkyGoi == null) objdangkyGoi = GoiDangki.FetchByID(Utility.Int64Dbnull(grdList.GetValue(GoiDangki.Columns.IdDangky), -1));
            if (objdangkyGoi != null)
            {
                bool da_ketthuc = Utility.Bool2Bool(objdangkyGoi.TthaiKetthuc);
                bool da_kichhoat = Utility.Bool2Bool(objdangkyGoi.TthaiKichhoat);
                bool da_thanhtoan = Utility.Bool2Bool(objdangkyGoi.TthaiTtoan);
                bool da_huy = Utility.Bool2Bool(objdangkyGoi.TthaiHuy);
                cmdThanhToan.Enabled = objLuotkham != null && grdList.RowCount > 0 && grdChitiet.RowCount > 0 && !da_ketthuc && Utility.DecimaltoDbnull(txtTienThu.Text, 0) > 0 && !da_huy;//&& txtPttt.myCode != "-1";
                cmdInphieuthu.Enabled = objLuotkham != null && grdList.RowCount > 0 && grdChitiet.RowCount > 0 && grdPayment.RowCount > 0;
                mnuKichhoat.Enabled = objLuotkham != null && grdList.RowCount > 0 && grdChitiet.RowCount > 0 && !da_kichhoat && !da_ketthuc && da_thanhtoan && !da_huy;
                mnuHuykichhoat.Enabled = !mnuKichhoat.Enabled && !da_huy;
                cmdKetthucgoi.Enabled = objLuotkham != null && grdList.RowCount > 0 && grdChitiet.RowCount > 0 && da_kichhoat && !da_ketthuc && !da_huy;
                cmdThemgoi.Enabled = objLuotkham != null && autoGoikham.MyID != "-1";
                cmdInGoi.Enabled = objLuotkham != null && grdChitiet.RowCount > 0;
                return;
            }
          
        }
        DataTable dtChiTietGoiKhamCaNhan = new DataTable();
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged || grdList.RowCount <= 0)
                {
                    objdangkyGoi = null;
                    return;
                }
                dtpNgaythanhtoan.Value = DateTime.Now;
                if (grdList.CurrentRow == null || grdList.CurrentRow.RowType != RowType.Record)
                {
                    objdangkyGoi = null;
                    grdChitiet.DataSource = null;
                    grdPayment.DataSource = null;
                    cmdInGoi.Enabled = false;
                    return;
                }
                cmdInGoi.Enabled = true;
                var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                var idGoiDvu = Utility.Int32Dbnull(grdList.GetValue("id_goi"));
                objdangkyGoi = GoiDangki.FetchByID(v_Id_dangky);
                dtChiTietGoiKhamCaNhan = _goiKhamService.LayChiTietGoiKhamTheoBN(idGoiDvu, objLuotkham.IdBenhnhan, v_Id_dangky);
                if (dtChiTietGoiKhamCaNhan != null && dtChiTietGoiKhamCaNhan.Rows.Count > 0)
                {
                    if (!dtChiTietGoiKhamCaNhan.Columns.Contains("Tong_tien"))
                        dtChiTietGoiKhamCaNhan.Columns.AddRange(new DataColumn[] { new DataColumn("Tong_tien", typeof(decimal)), new DataColumn("Tong_tien_Chuachidinh", typeof(decimal)), new DataColumn("Tong_tien_Dachidinh", typeof(decimal)) });
                    foreach (DataRow dr in dtChiTietGoiKhamCaNhan.Rows)
                    {
                        dr["Tong_tien"] = Utility.DecimaltoDbnull(dr["SO_LUONG"], 0) * Utility.DecimaltoDbnull(dr["DON_GIA"], 0);
                        dr["Tong_tien_Chuachidinh"] = Utility.DecimaltoDbnull(dr["SO_LUONG_CON_LAI"], 0) * Utility.DecimaltoDbnull(dr["DON_GIA"], 0);
                        dr["Tong_tien_Dachidinh"] = Utility.DecimaltoDbnull(dr["Tong_tien"]) - Utility.DecimaltoDbnull(dr["Tong_tien_Chuachidinh"]);
                    }
                }
                grdChitiet.DataSource = dtChiTietGoiKhamCaNhan;
               

                // _dtLichSuThanhToanCaNhan = _goiKhamService.LayLichSuThanhToanGoi(v_Id_dangky);
                // grdPayment.DataSource = _dtLichSuThanhToanCaNhan;



                txtGhiChu.Text = Utility.sDbnull(grdList.GetValue("GHI_CHU"));
                txtThanhTien.Text = Utility.sDbnull(grdList.GetValue("THANH_TIEN"));
                LaydanhsachLichsuthanhtoan_phieuchi();

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }
        bool checkValidData()
        {
            if (Utility.DecimaltoDbnull(txtTienThu.Text) <= 0)
            {
                errorProvider1.SetError(txtTienThu,"Số tiền thanh toán phải >0");
                return false;
            }
            if (Utility.DecimaltoDbnull(txtTienThu.Text) > Utility.DecimaltoDbnull(txtTienConlai.Text))
            {
                errorProvider1.SetError(txtTienThu, "Số tiền thanh toán phải <= số tiền còn lại chưa thanh toán");
                return false;
            }
            if (dtpNgaythanhtoan.Value.Date < dtpNgaydangky.Value.Date)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán gói phải lớn hơn hoặc bằng ngày đăng ký của gói {1}", dtpNgaydangky.Value.ToString("dd/MM/yyyy")));
                dtpNgaythanhtoan.Focus();
                return false;
            }
            return isValidPttt_Nganhang();
        }
       
        private void cmdThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdThanhToan, false);
                if (!checkValidData()) return;
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn thanh toán {0} VNĐ cho gói khám {1} của bệnh nhân {2} với phương thức là {3} không ?",
                    txtTienThu.Text, Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text, cboPttt.Text), "Thông báo", false))
                {
                    var idGoiDvu = Utility.Int32Dbnull(grdList.GetValue("id_goi"));
                    var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    var ngayGioThanhToan = dtpNgaythanhtoan.Value;
                    var depositCode = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 3);
                    var option = new TransactionOptions();
                    option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                    option.Timeout = TimeSpan.FromMinutes(55);
                    PerformAction();
                    //using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
                    //{
                    //    _goiKhamService.ThanhToanGoi(objLuotkham, idGoiDvu, 
                    //                                 Utility.DecimaltoDbnull(txtTienThu.Text),
                    //                                 Utility.sDbnull(cboHinhThucThanhToan.SelectedValue),"", objLuotkham.TrangthaiNoitru,
                    //                                 depositCode, ngayGioThanhToan, Utility.sDbnull(txtGhiChuTamUng.Text));
                    //    var newRow = _dtLichSuThanhToanCaNhan.NewRow();
                    //    newRow["ma_luotkham"] = txtPatient_Code.Text;
                    //    newRow["ngay_thuchien"] = ngayGioThanhToan;
                    //    newRow["so_tien"] = txtTienThu.Text;
                    //    newRow["ma_phieuthu"] = depositCode;
                    //    newRow["ma_pttt"] = cboHinhThucThanhToan.Text;
                    //    _dtLichSuThanhToanCaNhan.Rows.Add(newRow);
                    //    _dtLichSuThanhToanCaNhan.AcceptChanges();
                    //    grdList.UpdateData();
                    //    grdList_SelectionChanged(sender, e);
                    //    _goiKhamService.CapNhatTrangThaiThanhToan(v_Id_dangky,true);

                    //    scope.Complete();

                    //}
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                Utility.EnableButton(cmdThanhToan, true);
                ModifyCommands();
            }
        }
        DataTable m_dtPayment = new DataTable();
        DataTable m_dtPhieuChi = new DataTable();
        DataTable dtData = new DataTable();
        string lst_IDLoaithanhtoan = "8";
        
        private void LaydanhsachLichsuthanhtoan_phieuchi()
        {
            try
            {
                m_dtPayment = null;
                m_dtPhieuChi = null;
                if (objdangkyGoi == null) return;
                string id_goi=grdList.GetValue("id_goi").ToString();
                string id_dangky = grdList.GetValue("id_dangky").ToString();
                dtData =
                       _THANHTOAN.LaythongtinCacLanthanhtoan(Utility.sDbnull(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text, ""),
                           objLuotkham.IdBenhnhan, 0, 0, 0,
                           globalVariables.MA_KHOA_THIEN, lst_IDLoaithanhtoan,Utility.Int16Dbnull( id_goi,-1));
                DataRow[] arrDR = dtData.Select(string.Format("Kieu_ThanhToan = 0 and id_goi={0} and id_dangky={1}", id_goi, id_dangky));
                if (arrDR.Length > 0) m_dtPayment = arrDR.CopyToDataTable();
                else
                    m_dtPayment = dtData.Clone();
                arrDR = dtData.Select(string.Format("Kieu_ThanhToan = 1 and id_goi={0} and id_dangky={1}", id_goi, id_dangky));
                if (arrDR.Length > 0) m_dtPhieuChi = arrDR.CopyToDataTable();
                else
                    m_dtPhieuChi = dtData.Clone();


                grdPayment.Visible = dtData.Rows.Count > 0;
                Utility.SetDataSourceForDataGridEx(grdPayment, dtData, false, true, "1=1", "");

                if (grdPayment.GetRows().Any())
                {
                    var daKichHoat = Utility.Int32Dbnull(grdList.GetValue("tthai_kichhoat"), 0);
                    var daketthuc = Utility.Int32Dbnull(grdList.GetValue("tthai_ketthuc"), 0);
                    cmdKichhoatgoi.Enabled = daKichHoat != 1;
                    cmdKetthucgoi.Enabled = daKichHoat == 1 && daketthuc == 0;
                }
                else
                {
                    cmdKichhoatgoi.Enabled = false;
                    cmdKetthucgoi.Enabled = false;
                }
                var tongTienDaThanhToan =
                      (from row in m_dtPayment.AsEnumerable()
                       select Utility.DecimaltoDbnull(row["BN_CT"])).Sum();
                decimal tienconlai = Utility.DecimaltoDbnull(txtThanhTien.Text) - tongTienDaThanhToan;
                txtTienConlai.Text = string.Format("{0:#,0.####}", tienconlai);
                txtTienThu.Text = string.Format("{0:#,0.####}", tienconlai);//txtTienConlai.Text.ToString("");
                GetTongtienconlaitronggoi();
                cmdThanhToan.Enabled = Utility.DecimaltoDbnull(txtTienConlai.Text, 0) != 0;
            }
            catch (Exception exception)
            {
                Utility.CatchException("Lỗi khi lấy thông tin lịch sử thanh toán gói của bệnh nhân", exception);
                // throw;
            }
        }
        #region Thanhtoan
        public decimal TongtienCk = 0m;
        public decimal TileChietkhau = 0m;
        public decimal TongtienCkHoadon = 0m;
        public decimal TongtienCkChitiet = 0m;
        public string MaLdoCk = "";
        public string Lydo_chietkhau = "";
        bool ttoan_dthuoc = false;
        private bool INPHIEU_CLICK = false;
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        private void PerformAction()
        {
            try
            {
                v_Payment_ID = -1;
                objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text.Trim());
                if (objLuotkham != null)
                {
                    if (INPHIEU_CLICK)
                    {
                        goto INPHIEU;
                    }
                    if (PropertyLib._ThanhtoanProperties.Hoitruockhithanhtoan)
                        if (!Utility.AcceptQuestion("Bạn có muốn thanh toán cho bệnh nhân này không", "Thông báo thanh toán", true))
                        {
                            return;
                        }

                    //Nếu thanh toán khi in phôi thì không cần hỏi
                INPHIEU:
                    bool IN_HOADON = false;
                    string ErrMsg = "";
                    long IdHdonLog = -1;
                    IN_HOADON = Utility.DecimaltoDbnull(txtTienThu.Text, 0) > 0;
                    if (IN_HOADON)
                    {
                        //if (chkLayHoadon.Checked)//nếu lấy hóa đơn đỏ mới kiểm tra
                        //{
                        //    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1")//Chỉ khi dùng hóa đơn đỏ mới kiểm tra tiếp.
                        //    {
                        //        if (grdHoaDonCapPhat.RowCount <= 0)
                        //        {
                        //            uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                        //            Utility.ShowMsg("Bạn cần khai báo quyển hóa đơn đỏ trước khi sử dụng tính năng thanh toán với hóa đơn đỏ");
                        //            return;
                        //        }
                        //        if (!Utility.isValidGrid(grdHoaDonCapPhat))
                        //        {
                        //            uiTabHoadon_chiphi.SelectedTab = tabpageHoaDon;
                        //            Utility.ShowMsg("Mời bạn chọn quyển hóa đơn thanh toán");
                        //            return;
                        //        }
                        //        if (!checkSerie(ref IdHdonLog))
                        //        {
                        //            return;
                        //        }
                        //    }
                        //}
                    }

                    List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(ref ErrMsg);
                    if (lstItems == null)
                    {
                        Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
                        return;
                    }
                    TongtienCk = 0;
                    TileChietkhau = 0;
                    TongtienCkHoadon = 0;
                    TongtienCkChitiet = 0;
                    bool bo_ckchitiet = true;
                    string ma_uudai = "";
                    if (chkChietkhauthem.Checked || TongtienCkChitiet > 0)
                    {
                        frm_ChietkhauTrenHoadon chietkhauTrenHoadon = new frm_ChietkhauTrenHoadon();
                        chietkhauTrenHoadon.TongCKChitiet = 0;
                        chietkhauTrenHoadon.TongtienBN = Utility.DecimaltoDbnull(txtTienThu.Text);
                        chietkhauTrenHoadon.ckthem = chkChietkhauthem.Checked;
                        chietkhauTrenHoadon.ShowDialog();
                        if (!chietkhauTrenHoadon.m_blnCancel)
                        {
                            ma_uudai = chietkhauTrenHoadon.autoUudai.myCode;
                            bo_ckchitiet = chietkhauTrenHoadon.chkAll.Checked;
                            if (chietkhauTrenHoadon.chkBoChitiet.Checked)
                            {
                                TongtienCkChitiet = 0;
                            }
                            TongtienCk = chietkhauTrenHoadon.TongtienCK;
                            TongtienCkHoadon = chietkhauTrenHoadon.TongCKHoadon;
                            TileChietkhau = chietkhauTrenHoadon.txtPtramCK.Value;
                            MaLdoCk = chietkhauTrenHoadon.ma_ldoCk;
                            Lydo_chietkhau = chietkhauTrenHoadon.txtLydochietkhau.Text;
                        }
                        else
                        {
                            if (TongtienCkChitiet > 0)
                            {
                                Utility.ShowMsg("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Yêu cầu bạn nhập lý do chiết khấu(Do tiền chiết khấu >0). Mời bạn bấm lại nút thanh toán để bắt đầu lại");
                                return;
                            }
                            else
                            {
                                if (!Utility.AcceptQuestion("Bạn vừa thực hiện hủy thao tác nhập thông tin chiết khấu. Bạn có muốn tiếp tục thanh toán không cần chiết khấu hay không?", "Xác nhận chiết khấu", true))
                                {
                                    return;
                                }
                            }
                        }
                    }
                    decimal ttbnChitrathucsu = 0;
                    ErrMsg = "";
                    KcbThanhtoan v_objPayment = TaophieuThanhtoan();
                    List<KcbChietkhau> lstChietkhau = new List<KcbChietkhau>();
                    ActionResult actionResult = _THANHTOAN.ThanhtoanChiphiDvuKcb(v_objPayment, objLuotkham,null,
                        lstItems, lstChietkhau, ref v_Payment_ID, IdHdonLog,
                        false, bo_ckchitiet, ma_uudai,
                        ref ttbnChitrathucsu, ref ErrMsg);
                    IN_HOADON = ttbnChitrathucsu > 0;
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Thanh toán tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text, ucThongtinnguoibenh_doc_v71.txtTenBN.Text, v_objPayment.TongTien.ToString()), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            LaydanhsachLichsuthanhtoan_phieuchi();
                            Utility.GotoNewRowJanus(grdPayment, KcbThanhtoan.Columns.IdThanhtoan, v_Payment_ID.ToString());
                            if (v_Payment_ID <= 0)
                            {
                                grdPayment.MoveFirst();
                            }
                            //Kiểm tra nếu hình thức thanh toán phân bổ thì hiển thị chức năng phân bổ
                            List<string> lstPhanbo = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_BATBUOCPHANBO", false).Split(',').ToList<string>();
                            //Check nếu chọn Pttt=phân bổ và số dòng phân bổ <1 thì tự động hiển thị form phân bổ
                            if (lstPhanbo.Contains(cboPttt.SelectedValue.ToString()))
                            {
                                Phanbo();
                            }

                           ucThongtinnguoibenh_doc_v71.txtMaluotkham.Focus();
                           ucThongtinnguoibenh_doc_v71.txtMaluotkham.SelectAll();
                            //Tạm rem phần hóa đơn đỏ lại
                            //if (IN_HOADON && PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan)
                            //{
                            //    int kcbThanhtoanKieuinhoadon = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                            //    if (kcbThanhtoanKieuinhoadon == 1 || kcbThanhtoanKieuinhoadon == 3)
                                    InHoadon();
                            //    if (kcbThanhtoanKieuinhoadon == 2 || kcbThanhtoanKieuinhoadon == 3)
                            //        new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_ID, objLuotkham);
                            //}
                            //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1")
                            //{
                            //    if (grdHoaDonCapPhat.CurrentRow != null)
                            //    {
                            //        if (grdHoaDonCapPhat.CurrentRow.RowType == RowType.Record)
                            //        {
                            //            LoadHoaDonDoNgoaiTruTheoCoSo(1);
                            //        }
                            //    }
                            //}
                            //if (PropertyLib._ThanhtoanProperties.HienthidichvuNgaysaukhithanhtoan)
                            //{
                            //    ShowPaymentDetail(v_Payment_ID);
                            //}
                            //if (TabPageTamung.TabVisible)
                            //{
                            //    var objTamung = new Select().From(NoitruTamung.Schema)
                            //        .Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            //        .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            //        .And(NoitruTamung.Columns.KieuTamung).IsEqualTo(1)//Hoàn ứng. Có thể kiểm tra bằng trường trạng thái=1
                            //        .And(NoitruTamung.Columns.Noitru).IsEqualTo(0)
                            //        .ExecuteSingle<NoitruTamung>();
                            //    ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNG_NGOAITRU");
                            //    cmdHoanung.Text = objTamung == null ? "Hoàn ứng" : "Hủy hoàn ứng";
                            //    cmdHoanung.Tag = objTamung == null ? "0" : "1";
                            //    SetSumTotalProperties();

                            //}
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                        case ActionResult.Cancel:
                            Utility.ShowMsg(ErrMsg);
                            break;
                    }
                    IN_HOADON = false;
                    INPHIEU_CLICK = false;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                TongtienCk = 0m;
                TongtienCkChitiet = 0m;
                TongtienCkHoadon = 0m;
                MaLdoCk = "";
                //ModifyCommand();
                GC.Collect();
            }
        }
        private NLog.Logger log;
        void InHoadon()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                decimal TONG_TIEN = Utility.Int32Dbnull(grdPayment.CurrentRow.Cells["BN_CT"].Value, -1);
                ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }
        }
        void InPhieuchi()
        {
            //try
            //{
            //    int _Payment_ID = Utility.Int32Dbnull(grdPhieuChi.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
            //    decimal TONG_TIEN = Utility.Int32Dbnull(grdPhieuChi.CurrentRow.Cells["BN_CT"].Value, -1);
            //    ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(_Payment_ID);
            //    switch (actionResult)
            //    {
            //        case ActionResult.Success:
            //            new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(_Payment_ID);
            //            break;
            //        case ActionResult.Error:
            //            Utility.ShowMsg("Lỗi trong quá trình in phiếu chi", "Thông báo lỗi", MessageBoxIcon.Warning);
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }
        private List<KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(ref string errMsg)
        {
            try
            {
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                //foreach (GridEXRow row in grdThongTinChuaThanhToan.GetCheckedRows())
                //{
                    KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = 1;// Utility.DecimaltoDbnull(row.Cells[KcbThanhtoanChitiet.Columns.SoLuong].Value, 0);
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = Utility.DecimaltoDbnull(txtTienThu.Text, 0);
                    newItem.BhytChitra = 0;
                    newItem.DonGia = Utility.DecimaltoDbnull(txtTienThu.Text, 0);
                    newItem.GiaGoc = Utility.DecimaltoDbnull(txtTienThu.Text, 0);
                    newItem.TyleTt = 100;
                    newItem.PhuThu = 0;
                    newItem.TinhChkhau = 0;
                    newItem.CkNguongt = 0;
                    newItem.TuTuc = 0;
                    newItem.IdGoi = Utility.Int32Dbnull(grdList.GetValue("id_goi"));
                    newItem.IdPhieu = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    newItem.IdKham = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    newItem.IdDichvu = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    newItem.IdChitietdichvu = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                    newItem.TenChitietdichvu = Utility.sDbnull(grdList.GetValue("ten_goi"));
                    newItem.TenBhyt = Utility.sDbnull(grdList.GetValue("ten_goi"));
                    newItem.DonviTinh = "Lần";
                    newItem.SttIn = 1;
                    newItem.IdKhoakcb = globalVariables.IdKhoaNhanvien;
                    newItem.IdPhongkham = globalVariables.IdPhongNhanvien;
                    newItem.IdBacsiChidinh = Utility.Int16Dbnull(grdList.GetValue("id_nvien_tao")); 
                    newItem.IdLoaithanhtoan = 8;//gói dịch vụ nhưng có ID gói để phân biệt với các dịch vụ gói khác
                    newItem.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                    newItem.MatheBhyt = objLuotkham.MatheBhyt;
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(8);
                    newItem.TienChietkhau = 0m ;
                    newItem.TileChietkhau = 0m;
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.KieuChietkhau = "%";
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = objLuotkham.Noitru.Value;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;
                    lstItems.Add(newItem);
                //}
                return lstItems;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        private bool PayCheckDate(DateTime InputDate)
        {
            if (globalVariables.SysDate.Date != InputDate.Date && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_CHOPHEPCHONGAYTHANHTOAN", "1", false) == "1")
            {
                frm_ChonngayThanhtoan frm = new frm_ChonngayThanhtoan();
                frm.pdt_InputDate = dtpNgaydangky.Value;
                frm.ShowDialog();
                if (!frm.mv_blnCancel)
                {
                    dtpNgaythanhtoan.Value = frm.pdt_InputDate;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }

        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbThanhtoan objPayment = new KcbThanhtoan();
            objPayment.IdThanhtoan = -1;
            objPayment.MaLuotkham = objLuotkham.MaLuotkham;
            objPayment.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPayment.NgayThanhtoan = dtpNgaythanhtoan.Value;
            objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
            objPayment.KieuThanhtoan = 0;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
            objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objPayment.NoiTru = 0;
            objPayment.TrangthaiIn = 0;
            objPayment.NgayIn = null;
            objPayment.NguoiIn = string.Empty;
            objPayment.MaPttt = cboPttt.SelectedValue.ToString();
            objPayment.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "") : "-1";
            objPayment.Ghichu = Utility.DoTrim(txtsothethamchieu.Text);
            objPayment.NgayTonghop = null;
            objPayment.NguoiTonghop = string.Empty;
            objPayment.NgayChot = null;
            objPayment.TrangthaiChot = 0;
            objPayment.TongTien = Utility.DecimaltoDbnull(txtTienThu.Text, 0);
            objPayment.IdGoi = objdangkyGoi.IdGoi;
            objPayment.IdDangky = objdangkyGoi.IdDangky;
            //2 mục này được tính lại ở Business
            objPayment.BnhanChitra = -1;
            objPayment.BhytChitra = -1;
            objPayment.TileChietkhau = 0;
            objPayment.KieuChietkhau = "T";
            objPayment.TongtienChietkhau = TongtienCk;
            objPayment.TongtienChietkhauChitiet = TongtienCkChitiet;
            objPayment.TongtienChietkhauHoadon = TongtienCkHoadon;
            //if (chkLayHoadon.Checked && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SUDUNGHOADONDO", "0", false) == "1")
            //{
            //    objPayment.MauHoadon = Utility.DoTrim(txtMauHD.Text);
            //    objPayment.KiHieu = Utility.DoTrim(txtKiHieu.Text);
            //    objPayment.IdCapphat = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(HoadonCapphat.Columns.IdCapphat), -1);
            //    objPayment.MaQuyen = Utility.DoTrim(txtMaQuyen.Text);
            //    objPayment.Serie = Utility.DoTrim(txtSerie.Text);
            //}

            objPayment.MaLydoChietkhau = MaLdoCk;
            objPayment.NgayTao = globalVariables.SysDate;
            objPayment.NguoiTao = globalVariables.UserName;
            objPayment.IpMaytao = globalVariables.gv_strIPAddress;
            objPayment.TenMaytao = globalVariables.gv_strComputerName;
            return objPayment;
        }
        #endregion
        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtThanhTien);
        }

        private void txtTienConlai_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtTienConlai);
        }

        private void txtTienThu_TextChanged(object sender, EventArgs e)
        {
            if (Utility.DecimaltoDbnull(txtTienThu.Text) > Utility.DecimaltoDbnull(txtTienConlai.Text))
            {
                txtTienThu.Text = txtTienConlai.Text;
            }
            Utility.FormatCurrencyHIS(txtTienThu);
        }

        private void dtpHieuLucTuCaNhan_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpHieuLucDenCaNhan_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpHieuLucTuTheoLo_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpHieuLucDenTheoLo_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtPatient_Code_TextChanged(object sender, EventArgs e)
        {
            objLuotkham = null;
            cmdThemgoi.Enabled = false;
        }

        private void cboDanhSachGoiTheoLo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var idGoiDvu = Utility.Int32Dbnull(cboDanhSachGoiTheoLo.Value);
                _dtThongTinGoiKhamTheoLo = _goiKhamService.LayThongTinGoiKham(idGoiDvu);
                if (_dtThongTinGoiKhamTheoLo.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamTheoLo.Rows[0]["HieuLuc_TuNgay"])))
                    {
                        dtpHieuLucTuTheoLo.Value = Convert.ToDateTime(_dtThongTinGoiKhamTheoLo.Rows[0]["HieuLuc_TuNgay"]).Date;
                        dtpHieuLucTuTheoLo.Enabled = false;
                    }
                    else
                    {
                        dtpHieuLucTuTheoLo.Value = DateTime.Now.Date;
                        dtpHieuLucTuTheoLo.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamTheoLo.Rows[0]["HieuLuc_DenNgay"])))
                    {
                        dtpHieuLucDenTheoLo.Value = Convert.ToDateTime(_dtThongTinGoiKhamTheoLo.Rows[0]["HieuLuc_DenNgay"]).Date;
                        dtpHieuLucDenTheoLo.Enabled = false;
                    }
                    else
                    {
                        dtpHieuLucDenTheoLo.Value = DateTime.Now.Date;
                        dtpHieuLucDenTheoLo.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void btnThemGoiTheoLo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!grdDanhSachBNTheoLo.GetCheckedRows().Any())
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để thêm gói.");
                    return;
                }

                if (dtpHieuLucTuTheoLo.Value.Date > dtpHieuLucDenTheoLo.Value.Date)
                {
                    Utility.ShowMsg("Ngày hiệu lực từ đang lớn hơn ngày hiệu lực đến, bạn cần chỉnh sửa lại");
                    dtpHieuLucDenTheoLo.Focus();
                    return;
                }

                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thêm gói khám {0} cho {1} bệnh nhân không ?",
                        Utility.sDbnull(cboDanhSachGoiTheoLo.Text), grdDanhSachBNTheoLo.GetCheckedRows().Count()), "Thông báo", false))
                {

                    if (!_bw.IsBusy)
                    {
                        _bw.RunWorkerAsync(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
        }

        private void grdDanhSachBNTheoLo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdDanhSachBNTheoLo.CurrentRow == null || grdDanhSachBNTheoLo.CurrentRow.RowType != RowType.Record)
                {
                    grdGoiKhamTheoLo.DataSource = null;
                    return;
                }

                _dtGoiKhamTheoBNTheoLo = _goiKhamService.LayGoiKhamTheoBN(Utility.Int32Dbnull(grdDanhSachBNTheoLo.GetValue("id_benhnhan")), txtSoLo.Text);
                if (_dtGoiKhamTheoBNTheoLo.Rows.Count > 0)
                {
                    grdGoiKhamTheoLo.DataSource = _dtGoiKhamTheoBNTheoLo;
                }
                else
                {
                    grdGoiKhamTheoLo.DataSource = null;
                    grdChiTietGoiKhamTheoLo.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdGoiKhamTheoLo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdGoiKhamTheoLo.CurrentRow == null || grdGoiKhamTheoLo.CurrentRow.RowType != RowType.Record)
                {
                    grdChiTietGoiKhamTheoLo.DataSource = null;
                    return;
                }
                //var v_Id_dangky = Utility.Int32Dbnull(grdGoiKhamTheoLo.GetValue("id_dangky"));
                var idGoiDvu = Utility.Int32Dbnull(grdGoiKhamTheoLo.GetValue("id_goi"));
                var dtChiTietGoiKhamTheoLo = _goiKhamService.LayChiTietGoiKhamTheoBN(idGoiDvu, Utility.Int32Dbnull( grdDanhSachBNTheoLo.GetValue(  "id_benhnhan")),-1);
                grdChiTietGoiKhamTheoLo.DataSource = dtChiTietGoiKhamTheoLo.Rows.Count > 0
                                                         ? dtChiTietGoiKhamTheoLo
                                                         : null;


            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnXacNhanThanhToanTheoLo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn xác nhận thanh toán cho toàn bộ bệnh nhân có gói trong lô {0} không ?", txtSoLo.Text), "Thông báo", false))
                {
                    if (!_bw.IsBusy)
                    {
                        // 2: Xác nhận thanh toán theo lô
                        _bw.RunWorkerAsync(2);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void btnKichHoatGoiTheoLo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn kích hoạt cho toàn bộ bệnh nhân trong lô {0} không ?", txtSoLo.Text), "Thông báo", false))
                {
                    if (!_bw.IsBusy)
                    {
                        // 3: Kích hoạt theo lô
                        _bw.RunWorkerAsync(3);
                    }
                    btnTimKiemTheoLo.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void tabControl_SelectedTabChanged(object sender, Janus.Windows.UI.Tab.TabEventArgs e)
        {
            switch (e.Page.Key)
            {
                case "tabCaNhan":
                    ucThongtinnguoibenh_doc_v71.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_doc_v71.txtMaluotkham.SelectAll();
                    break;
                case "tabTheoLo":
                    txtSoLo.Focus();
                    txtSoLo.SelectAll();
                    break;
            }
        }

        private void tsmiXoaGoi_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem menuItem = sender as ToolStripItem;
                txtLyDoHuy.Clear();
                if (menuItem != null)
                {
                    // Retrieve the ContextMenuStrip that owns this ToolStripItem
                    ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                    if (owner != null)
                    {
                        // Get the control that is displaying this context menu
                        var grd = (GridEX)owner.SourceControl;
                        if (grd.CurrentRow == null || grd.CurrentRow.RowType != RowType.Record)
                        {
                            return;
                        }

                        if (grd.Name == grdList.Name)
                        {
                            if (KiemTraGoiChuaSuDung(grdChitiet) && KiemTraGoiDaThanhToan())
                            {
                               
                                if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa gói khám {0} của bệnh nhân {1} không?",
                                    Utility.sDbnull(grd.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                                {
                                    frm_Chondanhmucdungchung nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYGOI",
                                  "Hủy gói khám", "Nhập lý do hủy gói trước khi thực hiện",
                                  "Lý do hủy gói", false);
                                    nhaplydohuythanhtoan.ShowDialog();
                                    if (nhaplydohuythanhtoan.m_blnCancel) return;
                                    ma_lydohuy = nhaplydohuythanhtoan.ma;
                                    lydo_huy = nhaplydohuythanhtoan.ten;
                                    txtLyDoHuy.Text = lydo_huy;
                                    if (string.IsNullOrEmpty(txtLyDoHuy.Text))
                                    {
                                        Utility.ShowMsg("Bắt buộc phải nhập lý do hủy", "Thông báo", MessageBoxIcon.Warning);
                                        txtLyDoHuy.Focus();
                                        return;
                                    }
                                    XoaGoiKhamCuaBN(grd);
                                }

                            }
                        }
                        else if (grd.Name == grdGoiKhamTheoLo.Name)
                        {
                            if (KiemTraGoiChuaSuDung(grdChiTietGoiKhamTheoLo))
                            {
                                if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa gói khám {0} của bệnh nhân {1} không?",
                                    Utility.sDbnull(grd.GetValue("ten_goi")), Utility.sDbnull(grdDanhSachBNTheoLo.GetValue("ten_benhnhan"))), "Thông báo", false))
                                {
                                    frm_Chondanhmucdungchung nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYGOI",
                                  "Hủy gói khám", "Nhập lý do hủy gói trước khi thực hiện",
                                  "Lý do hủy gói", false);
                                    nhaplydohuythanhtoan.ShowDialog();
                                    if (nhaplydohuythanhtoan.m_blnCancel) return;
                                    ma_lydohuy = nhaplydohuythanhtoan.ma;
                                    lydo_huy = nhaplydohuythanhtoan.ten;
                                    txtLyDoHuy.Text = lydo_huy;
                                    if (string.IsNullOrEmpty(txtLyDoHuy.Text))
                                    {
                                        Utility.ShowMsg("Bắt buộc phải nhập lý do hủy", "Thông báo", MessageBoxIcon.Warning);
                                        txtLyDoHuy.Focus();
                                        return;
                                    }
                                    XoaGoiKhamCuaBN(grd);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void XoaGoiKhamCuaBN(GridEX grd)
        {
            try
            {
                var v_Id_dangky = Utility.Int32Dbnull(grd.GetValue("id_dangky"));
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        _goiKhamService.XoaGoiKhamCuaBN(v_Id_dangky, Utility.sDbnull(txtLyDoHuy.Text));
                        grd.CurrentRow.Delete();
                        grd.UpdateData();
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private bool KiemTraGoiChuaSuDung(GridEX grd)
        {
            foreach (GridEXRow row in grd.GetRows())
            {
                var soLuong = Utility.Int32Dbnull(row.Cells["SO_LUONG"].Value);
                var soLuongConLai = Utility.Int32Dbnull(row.Cells["SO_LUONG_CON_LAI"].Value);
                var tenDichVu = Utility.sDbnull(row.Cells["ten_chitietdichvu"].Value);
                if (soLuong != soLuongConLai)
                {
                    Utility.ShowMsg(string.Format("Dịch vụ {0} trong gói đã được sử dụng ít nhất 1 lần. Không thể xóa gói.", tenDichVu));
                    return false;
                }
            }
            return true;
        }
        private bool KiemTraGoiDaThanhToan()
        {
            if (m_dtPayment.Rows.Count > 0 || m_dtPhieuChi.Rows.Count>0)
            {
                Utility.ShowMsg(string.Format("Gói đã đươc thanh toán. Không thể xóa gói."));
                return false;
            }
            return true;
        }

        private void btnInChiTietGoi_Click(object sender, EventArgs e)
        {
            InTinhinhsudunggoi();  
        }
        void InTinhinhsudunggoi()
        {
            try
            {
                var dt = _goiKhamService.LayPhieuQuanLyCrpt(objLuotkham.IdBenhnhan, Utility.sDbnull(ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text),
                                                            Utility.Int32Dbnull(grdList.GetValue("id_dangky")));
                var sMoneyByLetter = new MoneyByLetter();
                if (!dt.Columns.Contains("thanhtien_chu"))
                {
                    dt.Columns.Add("thanhtien_chu");
                }
                if (!dt.Columns.Contains("conlai_chu"))
                {
                    dt.Columns.Add("conlai_chu");
                }
                string thanhtien = "";
                string conlai = "";
                foreach (DataRow row in dt.Rows)
                {
                    if(thanhtien=="") thanhtien=sMoneyByLetter.sMoneyToLetter(Utility.sDbnull(row["THANH_TIEN"], "0"));
                    row["thanhtien_chu"] = thanhtien;
                    if (conlai == "") conlai = sMoneyByLetter.sMoneyToLetter(Utility.sDbnull(row["tongtien_conlai"], "0"));
                    row["conlai_chu"] = conlai;
                }
                THU_VIEN_CHUNG.CreateXML(dt, "GOI_TINHHINHSUDUNG.xml");
                VNS.HIS.UI.Classess.Baocao.InPhieu(dt, DateTime.Now, "", true, "GOI_TINHHINHSUDUNG");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void grdPayment_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdPHIEU_THU")
            {
                CallPhieuThu();
            }
            if (e.Column.Key == "cmdHUY_PHIEUTHU")
            {
                HuyThanhtoan();
            }
        }
        void grdPayment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CallPhieuThu();
        }
        private void CallPhieuThu()
        {
            if (grdPayment.CurrentRow != null)
            {
                v_Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);


                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);
                if (objLuotkham == null)
                {
                    objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text.Trim());
                }
                if (objPayment != null)
                {
                    if (objLuotkham != null)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                        frm.Chuathanhtoan = _chuathanhtoan;
                        frm.TotalPayment = grdPayment.GetDataRows().Length;
                        frm.txtSoTienCanNop.Text = txtTienThu.Text;
                        frm.ShowCancel = false;
                        frm.ShowDialog();
                    }
                }
            }
        }
        string ma_lydohuy = "";
        string lydo_huy = "";
        decimal _chuathanhtoan = 0m;
        private void HuyThanhtoan()
        {
            try
            {
                ma_lydohuy = "";
                if (!Utility.isValidGrid(grdPayment)) return;
                if (grdPayment.CurrentRow != null)
                {
                    if (objLuotkham == null)
                    {
                        objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text.Trim());
                    }
                    if (objLuotkham.TrangthaiNoitru >=
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                    {
                        Utility.ShowMsg(
                            "Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                        return;
                    }
                    //if (objPhieuDct.TrangthaiXml > Utility.Int32Dbnull(1))
                    //{
                    //    Utility.ShowMsg("Bệnh nhân đã được gửi dữ liệu BHYT!");
                    //    return;
                    //}

                    v_Payment_ID =
                        Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1);
                    KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_ID);

                    if (objPayment != null)
                    {
                        //Kiểm tra ngày hủy
                        int kcbThanhtoanSongayHuythanhtoan =
                            Utility.Int32Dbnull(
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYTHANHTOAN_GOI", "0", true), 0);
                        var chenhlech =
                            (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                        if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                        {
                            Utility.ShowMsg(
                               string.Format("Ngày thanh toán: {0}. Hệ thống không cho phép bạn hủy thanh toán khi đã quá {1} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.Date.ToString("dd/MM/yyyy"), kcbThanhtoanSongayHuythanhtoan.ToString()));
                            return;
                        }
                        if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                        {
                            Utility.ShowMsg(
                                "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                            return;
                        }
                        if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                        {
                            var frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                            frm.objLuotkham = objLuotkham;
                            frm.v_Payment_Id = Utility.Int32Dbnull(objPayment.IdThanhtoan, -1);
                            frm.Chuathanhtoan = _chuathanhtoan;
                            frm.TotalPayment = grdPayment.GetDataRows().Length;
                            frm.txtSoTienCanNop.Text = txtTienThu.Text;
                            frm.ShowCancel = true;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                LaydanhsachLichsuthanhtoan_phieuchi();
                                //grdList_SelectionChanged(grdList, new EventArgs());
                            }
                        }
                        else
                        {
                            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                                if (
                                    !Utility.AcceptQuestion(
                                        string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}",
                                            objPayment.IdThanhtoan), "Thông báo", true))
                                {
                                    return;
                                }
                            if (
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN_GOIDE", "1",
                                    false) == "1")
                            {
                                var nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN_GOIDE",
                                    "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện",
                                    "Lý do hủy thanh toán",false);
                                nhaplydohuythanhtoan.ShowDialog();
                                if (nhaplydohuythanhtoan.m_blnCancel) return;
                                ma_lydohuy = nhaplydohuythanhtoan.ma;
                                lydo_huy = nhaplydohuythanhtoan.ten;
                            }
                            int idHdonLog =
                                Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[HoadonLog.Columns.IdHdonLog].Value, -1);
                            bool huythanhtoanHuybienlai =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                            ActionResult actionResult = _THANHTOAN.HuyThanhtoan(objPayment, objLuotkham, lydo_huy,
                                idHdonLog, huythanhtoanHuybienlai);
                            switch (actionResult)
                            {
                                case ActionResult.Success:
                                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy thanh toán tiền cho bệnh nhân ID={0}, PID={1}, Tên={2}, sô tiền={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, ucThongtinnguoibenh_doc_v71.txtTenBN.Text, objPayment.TongTien.ToString()), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    LaydanhsachLichsuthanhtoan_phieuchi();
                                    //grdList_SelectionChanged(grdList,new EventArgs());
                                    Utility.Log(Name, globalVariables.UserName,
                                        string.Format(
                                            "Hủy thanh toán ngoại trú của bệnh nhân có mã lần khám {0} và mã bệnh nhân là: {1} bởi {2}",
                                            objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, globalVariables.UserName),
                                        newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                    break;
                                case ActionResult.ExistedRecord:
                                    break;
                                case ActionResult.Error:
                                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo",
                                        MessageBoxIcon.Error);
                                    break;
                                case ActionResult.UNKNOW:
                                    Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                    break;
                                case ActionResult.Cancel:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommands();
                GC.Collect();
            }

        }
        private void XoaThanhToanGoi()
        {

            //int depositId = Utility.Int32Dbnull(grdLichSuThanhToanCaNhan.GetValue("id_phieu"));
            //var v_Id_dangky = Utility.Int32Dbnull(grdGoiKhamCaNhan.GetValue("id_dangky"));

            //KcbPhieuthu objDeposit = KcbPhieuthu.FetchByID(depositId);
            //if (objDeposit != null)
            //{
            //    if (!globalVariables.IsAdmin)
            //    {
            //        if (Utility.Int32Dbnull(objDeposit.DaChot) == 1)
            //        {
            //            Utility.ShowMsg("Bản ghi này đã được chốt, mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    //var objHoadonLog = new Select().From(LHoadonLog.Schema)
            //    //                      .Where(LHoadonLog.Columns.PaymentId).IsEqualTo(objDeposit.DepositId)
            //    //                      .And(LHoadonLog.Columns.TrangThai).IsEqualTo(0)
            //    //                       .And(LHoadonLog.Columns.LoaiXuat).IsEqualTo(1)
            //    //                      .ExecuteSingle<LHoadonLog>();
            //    //if (objHoadonLog != null && objHoadonLog.DaGui == true)
            //    //{
            //    //    Utility.ShowMsg("Hóa đơn đã được gửi sang hóa đơn điện tử. Để hủy thanh toán mời bạn hủy hóa đơn điện tử trước.", "Thông báo", MessageBoxIcon.Error);                    
            //    //    return;
            //    //}
            //    if (KiemTraGoiChuaSuDung(grdChiTietGoiKhamCaNhan))
            //    {

            //        try
            //        {
                        
            //                if (Utility.AcceptQuestion(string.Format("Bạn có muốn hủy thanh toán tiền gói không ?"), "Thông báo", false))
            //                {
            //                    var option = new TransactionOptions();
            //                option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
            //                option.Timeout = TimeSpan.FromMinutes(55);
            //                using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            //                {
            //                    _goiKhamService.HuyThanhToanGoi(objDeposit, "Hủy đăng ký gói");
            //                    _goiKhamService.CapNhatTrangThaiHuyThanhToan(v_Id_dangky,false);
            //                    DataRow[] arrDr = _dtLichSuThanhToanCaNhan.Select("id_phieu=" + depositId);
            //                    if (arrDr.GetLength(0) > 0)
            //                    {
            //                        arrDr[0].Delete();
            //                    }
            //                    _dtLichSuThanhToanCaNhan.AcceptChanges();                               
            //                    grdGoiKhamCaNhan.UpdateData();                               
            //                    scope.Complete();
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            //        }
            //    }

            //}
        }
        Int16 HinhthucTT = 0;
        Int16 IdNganHang = -1;
        private void tsmChuyenHTTT_Click(object sender, EventArgs e)
        {
            if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
            {
                Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                return;
            }
            Phanbo();
        }

        private void cmdKetThucGoi_Click(object sender, EventArgs e)
        {
            Ketthucgoi();
        }
        void Ketthucgoi()
        {
            try
            {
                if (objdangkyGoi == null) objdangkyGoi = GoiDangki.FetchByID(Utility.Int64Dbnull(grdList.GetValue(GoiDangki.Columns.IdDangky), -1));
                if (objdangkyGoi == null)
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} không tồn tại. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (!Utility.Bool2Bool(objdangkyGoi.TthaiKichhoat))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} chưa được Kích hoạt nên không thể Kết thúc gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.Bool2Bool(objdangkyGoi.TthaiHuy) || Utility.Bool2Bool(objdangkyGoi.TthaiKetthuc))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} đã kết thúc hoặc bị hủy nên không thể kết thúc gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                

                if (Utility.AcceptQuestion(string.Format("Bạn có muốn kết thúc gói {0} của người bệnh {1} không ?",
                    Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                {
                    //Kiểm tra cảnh báo tình trạng sử dụng
                    DataTable dtCheck = Utility.ExecuteSql(string.Format("select 1 from goi_tinhtrangsudung where id_dangky={0} and so_luong<.soluong_dung", objdangkyGoi.IdDangky), CommandType.Text).Tables[0];
                    if (dtCheck.Rows.Count <= 0)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Gói {0} của bệnh nhân {1} chưa sử dụng dịch vụ nào. Bạn có muốn kết thúc gói? ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Xác nhận", true))
                            return;
                    }
                    //var option = new TransactionOptions();
                    //option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                    //option.Timeout = TimeSpan.FromMinutes(55);
                    //using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
                    //{
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                            _goiKhamService.KetThucGoiChoBN(v_Id_dangky, true);
                            grdList.SetValue("DaKetThuc", true);
                            grdList.UpdateData();
                            _dtGoiKhamTheoBNCaNhan.AcceptChanges();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Kết thúc gói khám, id đăng ký: {0}, mã bệnh nhân : {1}, Mã lần khám:{2}", v_Id_dangky, objLuotkham.IdBenhnhan, ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text), newaction.Update, "UI");
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }
        void Huyketthucgoi()
        {
            try
            {
                if (objdangkyGoi == null) objdangkyGoi = GoiDangki.FetchByID(Utility.Int64Dbnull(grdList.GetValue(GoiDangki.Columns.IdDangky), -1));
                if (objdangkyGoi == null)
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} không tồn tại. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (!Utility.Bool2Bool(objdangkyGoi.TthaiKetthuc))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} chưa được kết thúc nên không thể Hủy kết thúc gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.Bool2Bool(objdangkyGoi.TthaiHuy) )
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} đã bị hủy nên không thể hủy kết thúc gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn Hủy kết thúc gói {0} của người bệnh {1} không ?",
                    Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                {
                    //var option = new TransactionOptions();
                    //option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                    //option.Timeout = TimeSpan.FromMinutes(55);
                    //using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
                    //{
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                            _goiKhamService.KetThucGoiChoBN(v_Id_dangky, false);
                            grdList.SetValue("DaKetThuc", false);
                            grdList.UpdateData();
                            _dtGoiKhamTheoBNCaNhan.AcceptChanges();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy kết thúc gói khám,id đăng ký :{0}, mã bệnh nhân : {1}, Mã lần khám:{2}", v_Id_dangky, objLuotkham.IdBenhnhan, ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text), newaction.Update, "UI");
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }
        private void txtdanhmucgoicanhan_TextChanged(object sender, EventArgs e)
        {
            var idGoiDVu = Utility.Int32Dbnull(autoGoikham.MyID, -1);
            _dtThongTinGoiKhamCaNhan = _goiKhamService.LayThongTinGoiKham(idGoiDVu);
            if (_dtThongTinGoiKhamCaNhan.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"])))
                {
                    dtpHieuLucTuCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"]).Date;
                    dtpHieuLucTuCaNhan.Enabled = false;
                }
                else
                {
                    dtpHieuLucTuCaNhan.Value = DateTime.Now.Date;
                    dtpHieuLucTuCaNhan.Enabled = true;
                }

                if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"])))
                {
                    dtpHieuLucDenCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"]).Date;
                    dtpHieuLucDenCaNhan.Enabled = false;
                }
                else
                {
                    dtpHieuLucDenCaNhan.Value = DateTime.Now.Date;
                    dtpHieuLucDenCaNhan.Enabled = true;
                }
            }
        }

        private void cmdInphieuthu_Click(object sender, EventArgs e)
        {
            ctxIn.Show(cmdInphieuthu, new Point(0, cmdInphieuthu.Height));
        }
        private void InBienLai()
        {
            try
            {

                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
                byte ttoan_thuoc = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.TtoanThuoc].Value, 0);
                if (kieuthanhtoan == 0)
                {
                    if (chkIntonghop.Visible && chkIntonghop.Checked)
                        new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, _Payment_ID, objLuotkham, 0);
                    else
                        new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, _Payment_ID, objLuotkham, 0);
                }
                else//Phiếu chi
                {
                    KcbThanhtoan objKcbThanhtoan = KcbThanhtoan.FetchByID(_Payment_ID);
                    if (ttoan_thuoc == 0)
                        new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChi(chkIntonghop.Visible && chkIntonghop.Checked, _Payment_ID, objLuotkham, objKcbThanhtoan.NoiTru);
                    else
                        new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChiTralaiThuoc(false, _Payment_ID, objLuotkham, objKcbThanhtoan.NoiTru);
                }

            }
            catch (Exception ex)
            {


                Utility.CatchException(ex);
                
            }

        }

        private void grdChiTietGoiKhamCaNhan_FormattingRow(object sender, RowLoadEventArgs e)
        {

        }
        //private TPayment CreatePayment_Khac()
        //{

        //    var objPayment = new TPayment();
        //    objPayment.PatientCode = Utility.sDbnull(objLuotkham.PatientCode, "");
        //    objPayment.PatientId = Utility.Int32Dbnull(objLuotkham.PatientId);
        //    objPayment.CreatedDate = BusinessHelper.GetSysDateTime();
        //    objPayment.CreatedBy = globalVariables.UserName;
        //    objPayment.Status = 0;
        //    objPayment.TrongGoi = 0;
        //    objPayment.PaymentCode = BusinessHelper.GeneratePaymentCode(BusinessHelper.GetSysDateTime(), 0);
        //    objPayment.PaymentDate = BusinessHelper.GetSysDateTime();
        //    objPayment.HtThanhtoan = Utility.Int16Dbnull(cboHinhThucThanhToan.SelectedValue, -1);
        //    objPayment.StaffId = globalVariables.gv_StaffID;
        //    objPayment.IpMacTao = BusinessHelper.GetMACAddress();
        //    objPayment.IpMayTao = BusinessHelper.GetIP4Address();
        //    objPayment.KieuThanhToan = radNgoaiTru.Checked ? Utility.Int32Dbnull(KieuThanhToan.Thu_Chi_Khac_NgoaiTru) : Utility.Int32Dbnull(KieuThanhToan.Thu_Chi_Khac_NoiTru);
        //    return objPayment;
        //}
        //private TPaymentDetail[] CreateArrayObjectPaymentDetail_Khac()
        //{
        //    int idx = 0;


        //    var ObjectArrayPaymentDetail = new TPaymentDetail[1];
        //    ObjectArrayPaymentDetail[idx] = new TPaymentDetail();
        //    ObjectArrayPaymentDetail[idx].PaymentId = -1;
        //    ObjectArrayPaymentDetail[idx].Quantity = Utility.DoubletoDbnull(1);
        //    ObjectArrayPaymentDetail[idx].OriginPrice = Utility.DecimaltoDbnull(txtSoTien.Text);
        //    ObjectArrayPaymentDetail[idx].SurchargePrice = Utility.DecimaltoDbnull(0);
        //    ObjectArrayPaymentDetail[idx].ServiceId = Utility.Int16Dbnull(-1);
        //    ObjectArrayPaymentDetail[idx].IsCancel = 0;
        //    ObjectArrayPaymentDetail[idx].PaymentTypeId = Utility.ByteDbnull(KieuLoaiThanhToan.ChiPhiThem);
        //    ObjectArrayPaymentDetail[idx].IsPayment = 1;
        //    ObjectArrayPaymentDetail[idx].Id = -1;
        //    ObjectArrayPaymentDetail[idx].ThuTuIn = -1;
        //    ObjectArrayPaymentDetail[idx].DepartmentId = -1;
        //    ObjectArrayPaymentDetail[idx].DoctorAssignId = -1;
        //    ObjectArrayPaymentDetail[idx].ServiceDetailName = Utility.sDbnull(txtLyDoKhac.Text).Trim();
        //    ObjectArrayPaymentDetail[idx].TenHienThi = Utility.sDbnull(txtLyDoKhac.Text).Trim();
        //    ObjectArrayPaymentDetail[idx].IdDetail = -1;
        //    ObjectArrayPaymentDetail[idx].MaKieuTtoan = BusinessHelper.MaKieuThanhToan(Utility.Int32Dbnull(KieuLoaiThanhToan.ChiPhiThem));
        //    ObjectArrayPaymentDetail[idx].ServiceDetailId = Utility.Int16Dbnull(-1);
        //    ObjectArrayPaymentDetail[idx].TienBnTra = Utility.DecimaltoDbnull(txtSoTien.Text);
        //    ObjectArrayPaymentDetail[idx].PTramBh = Utility.DecimaltoDbnull(0);
        //    ObjectArrayPaymentDetail[idx].MaDv = "DV";
        //    ObjectArrayPaymentDetail[idx].NoiTru = Utility.ByteDbnull(radNgoaiTru.Checked ? 0 : 1);
        //    ObjectArrayPaymentDetail[idx].TrongGoi = Utility.ByteDbnull(0);
        //    ObjectArrayPaymentDetail[idx].NguoiTao = globalVariables.UserName;
        //    ObjectArrayPaymentDetail[idx].NgayTao = BusinessHelper.GetSysDateTime();
        //    ObjectArrayPaymentDetail[idx].IpMacTao = BusinessHelper.GetMACAddress();
        //    ObjectArrayPaymentDetail[idx].IpMayTao = BusinessHelper.GetIP4Address();
        //    ObjectArrayPaymentDetail[idx].PtramCkhau = Utility.DecimaltoDbnull(0);
        //    ObjectArrayPaymentDetail[idx].DonViTinh = Utility.chuanhoachuoi("Lượt");
        //    ObjectArrayPaymentDetail[idx].IdGoiDvu =  Utility.Int32Dbnull(grdGoiKhamCaNhan.GetValue("id_dangky"));
        //    return ObjectArrayPaymentDetail;

        //}
        //private TPayment CreatePaymentHuy_Khac()
        //{
        //    var objPayment = new TPayment();
        //    objPayment.PatientCode = Utility.sDbnull(objLuotkham.PatientCode, "");
        //    objPayment.PatientId = Utility.Int32Dbnull(objLuotkham.PatientId);
        //    objPayment.CreatedDate = BusinessHelper.GetSysDateTime();
        //    objPayment.CreatedBy = globalVariables.UserName;
        //    objPayment.Status = 1;
        //    objPayment.PaymentCode = BusinessHelper.GeneratePaymentCode(BusinessHelper.GetSysDateTime(), 0);
        //    objPayment.PaymentDate = BusinessHelper.GetSysDateTime();
        //    objPayment.StaffId = globalVariables.gv_StaffID;
        //    objPayment.IpMayTao = BusinessHelper.GetIP4Address();
        //    objPayment.IpMacTao = BusinessHelper.GetMACAddress();
        //    objPayment.MaCoSo = globalVariables.MA_COSO;
        //    objPayment.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
        //    objPayment.NguoiIn = string.Empty;
        //    objPayment.TrongGoi = 0;
        //    objPayment.DaIn = 0;
        //    objPayment.KieuThanhToan = radNgoaiTru.Checked ? Utility.Int32Dbnull(KieuThanhToan.Thu_Chi_Khac_NgoaiTru) : Utility.Int32Dbnull(KieuThanhToan.Thu_Chi_Khac_NoiTru);
        //    return objPayment;
        //}
        //private TPaymentDetail[] CreateArrayObjectPaymentDetailHuy_Khac()
        //{
        //    int idx = 0;
        //    var objectArrayPaymentDetail = new TPaymentDetail[1];
        //    objectArrayPaymentDetail[idx] = new TPaymentDetail();
        //    objectArrayPaymentDetail[idx].PaymentId = -1;
        //    objectArrayPaymentDetail[idx].Quantity = Utility.DoubletoDbnull(1);
        //    objectArrayPaymentDetail[idx].OriginPrice = Utility.DecimaltoDbnull(txtSoTien.Text);
        //    objectArrayPaymentDetail[idx].SurchargePrice = Utility.DecimaltoDbnull(0);
        //    objectArrayPaymentDetail[idx].ServiceId = Utility.Int16Dbnull(-1);
        //    objectArrayPaymentDetail[idx].IsCancel = 1;
        //    objectArrayPaymentDetail[idx].PaymentTypeId = Utility.ByteDbnull(KieuLoaiThanhToan.ChiPhiThem);
        //    objectArrayPaymentDetail[idx].IsPayment = 1;
        //    objectArrayPaymentDetail[idx].Id = -1;
        //    objectArrayPaymentDetail[idx].ThuTuIn = -1;
        //    objectArrayPaymentDetail[idx].DepartmentId = -1;
        //    objectArrayPaymentDetail[idx].DoctorAssignId = -1;
        //    objectArrayPaymentDetail[idx].ServiceDetailName = Utility.sDbnull(txtLyDoKhac.Text).Trim();
        //    objectArrayPaymentDetail[idx].TenHienThi = Utility.sDbnull(txtLyDoKhac.Text).Trim();
        //    objectArrayPaymentDetail[idx].IdDetail = -1;
        //    objectArrayPaymentDetail[idx].MaKieuTtoan = BusinessHelper.MaKieuThanhToan(Utility.Int32Dbnull(KieuLoaiThanhToan.TraLaiTien));
        //    objectArrayPaymentDetail[idx].ServiceDetailId = Utility.Int16Dbnull(-1);
        //    objectArrayPaymentDetail[idx].TienBnTra = Utility.DecimaltoDbnull(0);
        //    objectArrayPaymentDetail[idx].PTramBh = Utility.DecimaltoDbnull(0);
        //    objectArrayPaymentDetail[idx].MaDv = "DV";
        //    objectArrayPaymentDetail[idx].NoiTru = Utility.ByteDbnull(radNgoaiTru.Checked ? 0 : 1);
        //    objectArrayPaymentDetail[idx].TrongGoi = Utility.ByteDbnull(0);
        //    objectArrayPaymentDetail[idx].NguoiTao = globalVariables.UserName;
        //    objectArrayPaymentDetail[idx].NgayTao = BusinessHelper.GetSysDateTime();
        //    objectArrayPaymentDetail[idx].IpMacTao = BusinessHelper.GetMACAddress();
        //    objectArrayPaymentDetail[idx].IpMayTao = BusinessHelper.GetIP4Address();
        //    objectArrayPaymentDetail[idx].PtramCkhau = Utility.DecimaltoDbnull(0);
        //    objectArrayPaymentDetail[idx].DonViTinh = Utility.chuanhoachuoi("Lượt");
        //    objectArrayPaymentDetail[idx].TienTraLai = Utility.DecimaltoDbnull(txtSoTien.Text);
        //    objectArrayPaymentDetail[idx].IdGoiDvu = Utility.Int32Dbnull(grdGoiKhamCaNhan.GetValue("id_dangky"));
        //    return objectArrayPaymentDetail;
        //}

        private void cmdThanhToanThuChiKhac_Click(object sender, EventArgs e)
        {
            //if (grdGoiKhamCaNhan.CurrentRow == null || grdGoiKhamCaNhan.CurrentRow.RowType != RowType.Record)
            //{
            //    return;
            //}
            //string patientCode = txtPatient_Code.Text;
            //objLuotkham = CreatePatientExam(patientCode);
            //if (objLuotkham != null )
            //{
            //    //Thu thêm chi phí khác
            //    if (radThu.Checked)
            //    {
            //        string returnmessage = string.Empty;
            //        TPayment objPayment = CreatePayment_Khac();
            //        TMienGiamThanhtoan[] objMienGiamThanhtoans = null;
            //        LHoadonLog objLHoadonLog = null;
            //        var actionResult = new VienPhiThanhToan().VienPhiThanhToanNgoaiTru(false,
            //             Utility.DecimaltoDbnull(txtSoTien.Text), objPayment, objLuotkham,
            //             CreateArrayObjectPaymentDetail_Khac(), ref returnmessage, null, objMienGiamThanhtoans, Utility.sDbnull(""));
            //        switch (actionResult)
            //        {
            //            case ActionResult.Success:
            //                Utility.ShowMsg("Thanh toán thành công", "Thông báo");
            //                v_Payment_ID = Utility.Int32Dbnull(objPayment.PaymentId);
            //                if (m_dtHinhThucTT.Rows.Count > 0)
            //                {
            //                    var queryHinhThuc =
            //                        from chonNganHang in m_dtHinhThucTT.Copy().AsEnumerable().Cast<DataRow>()
            //                        where
            //                            Utility.Int32Dbnull(chonNganHang["IdHinhThuc"]) ==
            //                            Utility.Int32Dbnull(cbohinhthucthukhac.SelectedValue)
            //                        select chonNganHang;

            //                    if (queryHinhThuc.Any())
            //                    {
            //                        DataRow drHinhThuc = queryHinhThuc.FirstOrDefault();
            //                        if (drHinhThuc != null)
            //                        {
            //                            if (Utility.Int16Dbnull(drHinhThuc["MoTa"], 0) == 1)
            //                            {
            //                                FrmPhanBoSoTienThanhToan frm = new FrmPhanBoSoTienThanhToan();
            //                                frm.IDThanhtoan = v_Payment_ID;
            //                                frm.MaPhieuThanhToan = Utility.sDbnull(objPayment.PaymentCode);
            //                                frm.NgayThanhtoan = Convert.ToDateTime(objPayment.PaymentDate);
            //                                frm.NguoiThanhToan = Utility.sDbnull(globalVariables.UserName);
            //                                frm.TongTien = Utility.DecimaltoDbnull(objPayment.SoTienThanhtoan);
            //                                frm.IDHinhthuc = Utility.Int32Dbnull(cboHinhThucThanhToan.SelectedValue);
            //                                frm.PatientCode = Utility.sDbnull(objLuotkham.PatientCode, "");
            //                                frm.PatientName = Utility.sDbnull(txtPatient_Name.Text);
            //                                frm.ShowDialog();
            //                            }
            //                        }
            //                    }
            //                }
            //                Utility.GotoNewRowJanus(grdLichSuThanhToanCaNhan, TPayment.Columns.PaymentId, Utility.sDbnull(v_Payment_ID, "-1"));
            //                break;
            //            case ActionResult.NotExistedRecord:
            //                Utility.ShowMsg("Không tồn tại  bản ghi, hoặc bản ghi đã bị thay đổi, đã thanh toán\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
            //                break;
            //            case ActionResult.Error:
            //                Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
            //                break;
            //        }
            //    }
            //    // chi chi phí khác
            //    if (radChi.Checked)
            //    {
            //        string returnmessage = string.Empty;
            //        TPayment objPayment = CreatePaymentHuy_Khac();
            //        TMienGiamThanhtoan[] objMienGiamThanhtoans = null;
            //        LHoadonLog objLHoadonLog = null;
            //        ActionResult actionResult = new VienPhiThanhToan().VienPhiThanhToanNgoaiTru(false,
            //             Utility.DecimaltoDbnull(txtSoTien.Text), objPayment, objLuotkham,
            //             CreateArrayObjectPaymentDetailHuy_Khac(), ref returnmessage, null, objMienGiamThanhtoans, Utility.sDbnull(""));
            //        switch (actionResult)
            //        {
            //            case ActionResult.Success:
            //                Utility.ShowMsg("Thanh toán thành công", "Thông báo");
            //                Utility.GotoNewRowJanus(grdLichSuThanhToanCaNhan, TPayment.Columns.PaymentId, Utility.sDbnull(v_Payment_ID, "-1"));
            //                break;
            //            case ActionResult.NotExistedRecord:
            //                Utility.ShowMsg("Không tồn tại  bản ghi, hoặc bản ghi đã bị thay đổi, đã thanh toán\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
            //                break;
            //            case ActionResult.Error:
            //                Utility.ShowMsg("Lỗi trong quá trình thanh toán", "Thông báo lỗi", MessageBoxIcon.Error);
            //                break;
            //        }
            //    }
            //}
            //else
            //{
            //    Utility.ShowMsg("Chưa chọn bệnh nhân để thực hiện chức năng thu thêm hoặc chi khác");
            //}
        }

        private void cmdkhongsudung_Click(object sender, EventArgs e)
        {
            try
            {
                int id_goidichvu = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_goi"].Value, -1);
                int id_qhe = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_sudung"].Value, -1);
                int servicesDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_chitietdichvu"].Value, -1);
                int trangThai = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["trang_thai"].Value, 0);
                int trangthai_new = 0;
                if (trangThai == 0) trangthai_new = 1;
                else trangthai_new = 0;
                new Update(GoiTinhtrangsudung.Schema).Set(GoiTinhtrangsudung.Columns.TrangThai)
                    .EqualTo(trangthai_new)
                    .Where(GoiTinhtrangsudung.Columns.IdSudung).IsEqualTo(id_qhe)
                    .And(GoiTinhtrangsudung.Columns.IdChitietdichvu)
                    .IsEqualTo(servicesDetail_Id).Execute();
                grdChitiet.CurrentRow.BeginEdit();
                grdChitiet.CurrentRow.Cells["trang_thai"].Value = trangthai_new;
                grdChitiet.CurrentRow.EndEdit();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);

            }
            finally
            {
                GetTongtienconlaitronggoi();
                ModifyCommands();
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                int trangThai = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["trang_thai"].Value, 0);
                if (trangThai == 1)
                {
                    cmdkhongsudung.Text = @"Không sử dụng dịch vụ";
                }
                else
                {
                    cmdkhongsudung.Text =  @"Sử dụng dịch vụ";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);

            }

        }
        void GetTongtienconlaitronggoi()
        {
            try
            {
                lblTitle.Text = "";
                Decimal Tongtiengoi = Utility.DecimaltoDbnull(Utility.sDbnull(grdList.GetValue("THANH_TIEN")));
                //Tính lại do liên quan đến phần cập nhật đơn giá trên lưới
                if (dtChiTietGoiKhamCaNhan != null && dtChiTietGoiKhamCaNhan.Rows.Count > 0)
                {
                    if (!dtChiTietGoiKhamCaNhan.Columns.Contains("Tong_tien"))
                        dtChiTietGoiKhamCaNhan.Columns.AddRange(new DataColumn[] { new DataColumn("Tong_tien", typeof(decimal)), new DataColumn("Tong_tien_Chuachidinh", typeof(decimal)), new DataColumn("Tong_tien_Dachidinh", typeof(decimal)) });
                    foreach (DataRow dr in dtChiTietGoiKhamCaNhan.Rows)
                    {
                        dr["Tong_tien"] = Utility.DecimaltoDbnull(dr["SO_LUONG"], 0) * Utility.DecimaltoDbnull(dr["DON_GIA"], 0);
                        dr["Tong_tien_Chuachidinh"] = Utility.DecimaltoDbnull(dr["SO_LUONG_CON_LAI"], 0) * Utility.DecimaltoDbnull(dr["DON_GIA"], 0);
                        dr["Tong_tien_Dachidinh"] = Utility.DecimaltoDbnull(dr["Tong_tien"]) - Utility.DecimaltoDbnull(dr["Tong_tien_Chuachidinh"]);
                    }
                }

                decimal tongTienDachidinh =
                     (from row in dtChiTietGoiKhamCaNhan.AsEnumerable()
                      select Utility.DecimaltoDbnull(row["Tong_tien_dachidinh"])
                      ).Sum();
                decimal Tong_tien_Chuachidinh =
                   (from row in dtChiTietGoiKhamCaNhan.AsEnumerable()
                    where Utility.sDbnull(row["trang_thai"]) == "1"
                    select Utility.DecimaltoDbnull(row["Tong_tien_Chuachidinh"])
                    ).Sum();
                decimal Tong_tien =
                   (from row in dtChiTietGoiKhamCaNhan.AsEnumerable()
                    where Utility.sDbnull(row["trang_thai"]) == "1"
                    select Utility.DecimaltoDbnull(row["Tong_tien"])
                    ).Sum();

                decimal Tong_tien_khongsudung =
                 (from row in dtChiTietGoiKhamCaNhan.AsEnumerable()
                  where Utility.sDbnull(row["trang_thai"]) == "0"
                  select Utility.DecimaltoDbnull(row["Tong_tien"])
                  ).Sum();
                //lblTitle.Text = String.Format("Tổng tiền chưa chỉ định={0}", string.Format("{0:#,0.####}", Tong_tien_Chuachidinh));
                //lblTitle.Text = String.Format("Tổng tiền gói(1)={0}; Tổng tiền dịch vụ trong gói(2) ={1}; Tổng tiền dịch vụ đã chỉ định(3) ={2};Tổng tiền dịch vụ không sử dụng(4) ={3}; Tổng tiền dịch vụ chưa chỉ định(5=2-3) ={4}; ", string.Format("{0:#,0.####}", Tongtiengoi), string.Format("{0:#,0.####}", Tong_tien), string.Format("{0:#,0.####}", tongTienDachidinh), string.Format("{0:#,0.####}", Tong_tien_khongsudung), string.Format("{0:#,0.####}", Tong_tien_Chuachidinh));
                txtConlai.Text = string.Format("{0:#,0.####}", Tong_tien_Chuachidinh);
            }
            catch
            {
            }
        }
        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtSoTien);
        }

        private void grdChiTietGoiKhamCaNhan_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if(e.Column.Key == "MienGiam")
                {
                int idGoidichvu = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_goi"].Value, -1);
                int id_qhe = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_sudung"].Value, -1);
                int servicesDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_chitietdichvu"].Value, -1);
                decimal miengiamcu = Utility.DecimaltoDbnull(e.InitialValue, 0);
                decimal miengiam_new = Utility.DecimaltoDbnull(e.Value);
                decimal dongia = Utility.DecimaltoDbnull(grdChitiet.CurrentRow.Cells["don_gia"].Value);
                if (miengiam_new > dongia)
                {
                    Utility.ShowMsg("Số tiền miễn giảm không được lớn hơn đơn giá", "Thông báo", MessageBoxIcon.Warning);
                    e.Value = miengiamcu;
                    return;
                }
                else
                {
                    new Update(GoiTinhtrangsudung.Schema).Set(GoiTinhtrangsudung.Columns.MienGiam).EqualTo(miengiam_new)
                   .Where(GoiTinhtrangsudung.Columns.IdSudung).IsEqualTo(id_qhe)
                   .And(GoiTinhtrangsudung.Columns.IdChitietdichvu)
                   .IsEqualTo(servicesDetail_Id).Execute();
                    grdChitiet.CurrentRow.BeginEdit();
                    grdChitiet.CurrentRow.Cells["MienGiam"].Value = miengiam_new;
                    grdChitiet.CurrentRow.EndEdit();
                }
               
            }
            if (e.Column.Key == "don_gia")
            {
                int idGoidichvu = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_goi"].Value, -1);
                int id_qhe = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_sudung"].Value, -1);
                int servicesDetail_Id = Utility.Int32Dbnull(grdChitiet.CurrentRow.Cells["id_chitietdichvu"].Value, -1);
                decimal dongia = Utility.DecimaltoDbnull(e.Value);

                new Update(GoiTinhtrangsudung.Schema).Set(GoiTinhtrangsudung.Columns.DonGia).EqualTo(dongia)
                   .Where(GoiTinhtrangsudung.Columns.IdSudung).IsEqualTo(id_qhe)
                   .And(GoiTinhtrangsudung.Columns.IdChitietdichvu)
                   .IsEqualTo(servicesDetail_Id).Execute();
                    grdChitiet.CurrentRow.BeginEdit();
                    grdChitiet.CurrentRow.Cells["don_gia"].Value = dongia;
                    grdChitiet.CurrentRow.EndEdit();
                    GetTongtienconlaitronggoi();
            }
        }

        private void txtdanhmucgoicanhan__OnSelectionChanged()
        {
           
        }

        private void txtdanhmucgoicanhan__OnEnterMe()
        {
            var idGoiDVu = Utility.Int32Dbnull(autoGoikham.MyID, -1);
            _dtThongTinGoiKhamCaNhan = _goiKhamService.LayThongTinGoiKham(idGoiDVu);
            if (_dtThongTinGoiKhamCaNhan.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"])))
                {
                    dtpHieuLucTuCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"]).Date;
                    dtpHieuLucTuCaNhan.Enabled = false;
                }
                else
                {
                    dtpHieuLucTuCaNhan.Value = DateTime.Now.Date;
                    dtpHieuLucTuCaNhan.Enabled = true;
                }

                if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"])))
                {
                    dtpHieuLucDenCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"]).Date;
                    dtpHieuLucDenCaNhan.Enabled = false;
                }
                else
                {
                    dtpHieuLucDenCaNhan.Value = DateTime.Now.Date;
                    dtpHieuLucDenCaNhan.Enabled = true;
                }
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            autoGoikham.ShowMe();
        }

        private void mnuInBienlai_Click(object sender, EventArgs e)
        {
            InBienLai();
        }

        private void mnuInHoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPayment))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất một phiếu thanh toán để in hóa đơn trong lưới bên dưới", "thông báo");
                return;
            }
            byte kieuthanhtoan = Utility.ByteDbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.KieuThanhtoan].Value, 0);
            if (kieuthanhtoan == 0)
                InHoadon();
            else
                new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(Utility.Int32Dbnull(grdPayment.CurrentRow.Cells[KcbThanhtoan.Columns.IdThanhtoan].Value, -1));
            string seria = Utility.sDbnull(grdPayment.GetValue(KcbThanhtoan.Columns.Serie), "");
            if (seria != "")
            {
                InHoaDon_BanHang();
            }
        }
        void InHoaDon_BanHang()
        {
            try
            {
                int _Payment_ID = Utility.Int32Dbnull(grdPayment.GetValue(KcbThanhtoan.Columns.IdThanhtoan), -1);
                new INPHIEU_THANHTOAN_NGOAITRU().InHoaDon_BanHang(_Payment_ID,Utility.ByteDbnull( objLuotkham.TrangthaiNoitru > 0 ? 1 : 0));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\n" + ex.Message, "Thông báo lỗi");
                log.Trace(ex.Message);
            }

        }

        private void mnuKichhoat_Click(object sender, EventArgs e)
        {
            Kichhoatgoi();
        }

        private void mnuHuykichhoat_Click(object sender, EventArgs e)
        {
            Huykichhoatgoi();
        }
        void Kichhoatgoi()
        {
            try
            {
                if (objdangkyGoi == null) objdangkyGoi = GoiDangki.FetchByID(Utility.Int64Dbnull(grdList.GetValue(GoiDangki.Columns.IdDangky), -1));
                if (objdangkyGoi == null)
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} không tồn tại. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (!Utility.Bool2Bool(objdangkyGoi.TthaiTtoan))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} chưa được thanh toán nên không thể kích hoạt gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.Bool2Bool(objdangkyGoi.TthaiHuy) || Utility.Bool2Bool(objdangkyGoi.TthaiKetthuc))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} đã kết thúc hoặc bị hủy nên không thể kích hoạt gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn kích hoạt gói {0} cho bệnh nhân {1} không ?",
                      Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                            _goiKhamService.KichHoatGoiChoBN(v_Id_dangky,true);
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Kích hoạt gói khám id={0} cho người bệnh mã khám={1} tên ={2}", v_Id_dangky, objLuotkham.MaLuotkham, ucThongtinnguoibenh_doc_v71.txtTenBN.Text), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                            grdList.SetValue("tthai_kichhoat", true);
                            grdList.UpdateData();
                            _dtGoiKhamTheoBNCaNhan.AcceptChanges();

                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }
        void Huykichhoatgoi()
        {
            try
            {

                if (objdangkyGoi == null) objdangkyGoi = GoiDangki.FetchByID(Utility.Int64Dbnull(grdList.GetValue(GoiDangki.Columns.IdDangky), -1));
                if (objdangkyGoi == null)
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} không tồn tại. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (!Utility.Bool2Bool(objdangkyGoi.TthaiKichhoat))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} chưa được Kích hoạt nên không thể Hủy kích hoạt gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.Bool2Bool(objdangkyGoi.TthaiHuy) || Utility.Bool2Bool(objdangkyGoi.TthaiKetthuc))
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} đã kết thúc hoặc bị hủy nên không thể Hủy kích hoạt gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                //Kiểm tra tình trạng sử dụng
                DataTable dtCheck = Utility.ExecuteSql(string.Format("select 1 from goi_tinhtrangsudung where id_dangky={0} and so_luong<.soluong_dung", objdangkyGoi.IdDangky), CommandType.Text).Tables[0];
                if (dtCheck.Rows.Count > 0)
                {
                    Utility.ShowMsg(string.Format("Gói {0} của bệnh nhân {1} đã có các dịch vụ được chỉ định nên không thể Hủy kích hoạt gói. Vui lòng kiểm tra lại ?", Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text));
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn Hủy kích hoạt gói {0} cho bệnh nhân {1} không ?",
                      Utility.sDbnull(grdList.GetValue("ten_goi")), ucThongtinnguoibenh_doc_v71.txtTenBN.Text), "Thông báo", false))
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            var v_Id_dangky = Utility.Int32Dbnull(grdList.GetValue("id_dangky"));
                            _goiKhamService.KichHoatGoiChoBN(v_Id_dangky,false);
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy kích hoạt gói khám id={0} cho người bệnh mã khám={1} tên ={2}", v_Id_dangky, objLuotkham.MaLuotkham, ucThongtinnguoibenh_doc_v71.txtTenBN.Text), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                            grdList.SetValue("tthai_kichhoat", false);
                            grdList.UpdateData();
                            _dtGoiKhamTheoBNCaNhan.AcceptChanges();

                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
            }
            finally
            {
                ModifyCommands();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (objdangkyGoi != null)
            {
                objdangkyGoi.NgayDangky = dtpNgaydangky.Value;
                objdangkyGoi.Save();
            }
        }

        private void mnuFinish_Click(object sender, EventArgs e)
        {
            Ketthucgoi();
        }

        private void mnuRollback_Click(object sender, EventArgs e)
        {
            Huyketthucgoi();
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            InTinhinhsudunggoi();  
        }
        //private void txtdanhmucgoicanhan__OnSelectionChanged()
        //{
        //    try
        //    {
        //        var idGoiDvu = Utility.Int32Dbnull(txtdanhmucgoicanhan.MyID, -1);
        //        if (idGoiDvu <= 0)
        //        {
        //            Utility.ShowMsg("Bạn chưa chọn gói dịch vụ cho người bệnh");
        //            txtdanhmucgoicanhan.Focus();
        //            return;
        //        }
        //        _dtThongTinGoiKhamCaNhan = _goiKhamService.LayThongTinGoiKham(idGoiDvu);
        //        if (_dtThongTinGoiKhamCaNhan.Rows.Count > 0)
        //        {
        //            if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"])))
        //            {
        //                dtpHieuLucTuCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_TuNgay"]).Date;
        //                dtpHieuLucTuCaNhan.Enabled = false;
        //            }
        //            else
        //            {
        //                dtpHieuLucTuCaNhan.Value = DateTime.Now.Date;
        //                dtpHieuLucTuCaNhan.Enabled = true;
        //            }

        //            if (!string.IsNullOrEmpty(Utility.sDbnull(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"])))
        //            {
        //                dtpHieuLucDenCaNhan.Value = Convert.ToDateTime(_dtThongTinGoiKhamCaNhan.Rows[0]["HieuLuc_DenNgay"]).Date;
        //                dtpHieuLucDenCaNhan.Enabled = false;
        //            }
        //            else
        //            {
        //                dtpHieuLucDenCaNhan.Value = DateTime.Now.Date;
        //                dtpHieuLucDenCaNhan.Enabled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.ShowMsg(MethodBase.GetCurrentMethod() + ex.Message);
        //    }
        //}

    }
}
