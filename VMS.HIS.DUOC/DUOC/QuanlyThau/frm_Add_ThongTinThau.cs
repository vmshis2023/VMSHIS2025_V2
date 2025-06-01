using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VietBaIT.CommonLibrary;
//using VietBaIT.HISLink.Business.Duoc.XuatThuoc;
//using VietBaIT.HISLink.Business.Reports.Implementation;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.LoadDataCommon;
//using VietBaIT.HISLink.UI.Duoc.Form_TAMPHUC_Duoc;
using NLog;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu
{
    public partial class frm_Add_ThongTinThau : Form
    {
        private readonly Logger log;
        public bool BCancel = false;
        public action EmAction = action.Insert;
        public GridEX GrdList;
        public DataTable dtThongTinThau = new DataTable();
        private DataTable dtChiTietThau = new DataTable();
        private DataTable dtDmThuoc = new DataTable();
        private string kieuvattuthuoc = "TATCA";

        public frm_Add_ThongTinThau(string skieuvattuthuoc)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Utility.loadIconToForm(this);
            log = LogManager.GetCurrentClassLogger();
            dtNgayThau.Value = BusinessHelper.GetSysDateTime();
            dtNgayQDinh.Value = BusinessHelper.GetSysDateTime();
            kieuvattuthuoc = skieuvattuthuoc;
        }
        public frm_Add_ThongTinThau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Utility.loadIconToForm(this);
            log = LogManager.GetCurrentClassLogger();
            dtNgayThau.Value = BusinessHelper.GetSysDateTime();
            dtNgayQDinh.Value = BusinessHelper.GetSysDateTime();
        }
        private void frm_Add_ThongTinThau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
        }
        private void ModifyCommnad()
        {
            try
            {
                cmdSave.Enabled = grdChiTietThau.RowCount > 0;
                cmdIn.Enabled = grdChiTietThau.RowCount > 0;
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception.Message);
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmdInPhieuNhap_Click(object sender, EventArgs e)
        {
            //int IdThau = Utility.Int32Dbnull(txtIDThau.Text, -1);
            //if (grdChiTietThau.GetDataRows().Count() <= 0)
            //{
            //    Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
            //    return;
            //}
            //ReportDocument crpt = new ReportDocument();
            //MoneyByLetter _moneyByLetter = new MoneyByLetter();
            //// crpt = new CRPT_BENHAN_NGOAITRU();
            //string reportcode = "PHIEU_XUAT_KHO";
            //string path = Utility.sDbnull(SystemReports.GetPathReport(reportcode));
            //string sTitleReport = SystemReports.TieuDeBaoCao(reportcode);
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", path), "Thông báo không tìm thấy File",
            //                    MessageBoxIcon.Warning);
            //    return;
            //}
            ////CRPT_PHIEUXUAT_KHO.rpt
            //// NẾU ĐƠN VỊ LÀ KYDONG CRPT_PHIEUXUAT_KHO_TAMPHUC.rpt
            //frmPrintPreview objForm = new frmPrintPreview("PHIẾU XUẤT KHO", crpt, true, true);
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.SetDataSource(m_dtReport);
            //BusinessHelper.CreateXml(m_dtReport, "dsPhieuXuatKho.xsd");
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            //crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            //crpt.SetParameterValue("sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
            //crpt.SetParameterValue("CurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
            //crpt.SetParameterValue("sTitleReport", sTitleReport);
            //crpt.SetParameterValue("BottomCondition", BusinessHelper.BottomCondition());
            //objForm.crptViewer.ReportSource = crpt;
            //objForm.ShowDialog();
        }
        private void GetData()
        {
            if (EmAction == action.Update)
            {
                DThongTinThau _objThau = DThongTinThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));
                if (_objThau != null)
                {
                    txtIDThau.Text = Utility.sDbnull(_objThau.IdThau, "-1");
                    txtLoaiThau.Text = Utility.sDbnull(_objThau.LoaiThau);
                    txtNhomThau.Text = Utility.sDbnull(_objThau.NhomThau);
                    txtGoiThau.Text = Utility.sDbnull(_objThau.GoiThau);
                    txtSoThau.Text = _objThau.SoThau;
                    dtNgayThau.Value = Convert.ToDateTime(_objThau.NgayThau);
                    txtSoQDinh.Text = Utility.sDbnull(_objThau.SoQDinh, "");
                    dtNgayQDinh.Value = Convert.ToDateTime(_objThau.NgayQDinh);
                    txtThongTinKhac.Text = Utility.sDbnull(_objThau.GhiChu);
                    txtNhaThau.SetId(_objThau.IdNhaCCap);
                    txtNhaThau.SetCode(_objThau.MaNhaCCap);

                    chkDieuTiet.Checked = Utility.Byte2Bool(Utility.ByteDbnull(_objThau.DieuTiet, 0));
                    if (chkDieuTiet.Checked)
                    {
                        txtVienDieuTiet.SetId(Utility.Int32Dbnull(_objThau.VienDieuTiet, -1));
                        txtSoHD_Dieutiet.Text = Utility.sDbnull(_objThau.SohdDieutiet, string.Empty);
                        dtpNgayHD_DieuTiet.Value = Convert.ToDateTime(_objThau.NgayhdDieutiet);
                        dtpNgayKT_DieuTiet.Value = Convert.ToDateTime(_objThau.NgayktDieutiet);
                    }

                    cbohinhthucthau.SelectedValue = Utility.Int32Dbnull(_objThau.HtThau, -1);

                    dtChiTietThau = SPs.SpChiTietThongTinThau(Utility.Int32Dbnull(txtIDThau.Text)).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grdChiTietThau, dtChiTietThau, false, true, "1=1", "");
                }
            }
            if (EmAction == action.Insert)
            {
                dtChiTietThau = SPs.SpChiTietThongTinThau(-100).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdChiTietThau, dtChiTietThau, false, true, "", "");
            }
            GetThuocTrongKho();
        }
        private void GetThuocTrongKho()
        {
            try
            {
                dtDmThuoc = SPs.DuocLayThongtinThuoc(kieuvattuthuoc).GetDataSet().Tables[0];
                Utility.AddColumToDataTable(ref dtDmThuoc, "ShortName", typeof(string));
                foreach (DataRow drv in dtDmThuoc.Rows)
                {
                    drv["ShortName"] = Utility.UnSignedCharacter(Utility.sDbnull(drv[LDrug.Columns.DrugName]));
                }
                dtDmThuoc.AcceptChanges();
                BindKhoThuoc(dtDmThuoc);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception);
            }
        }
        private void BindKhoThuoc(DataTable dataTable)
        {
            Utility.SetDataSourceForDataGridEx(grdDanhSachThuoc, dataTable, true, true, "", "");
        }
        private DThongTinThau CreateThongTinThau()
        {
            var objDThongTinThau = new DThongTinThau();
            if (EmAction == action.Update)
            {
                objDThongTinThau = DThongTinThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));
                objDThongTinThau.MarkOld();
                objDThongTinThau.IsLoaded = true;
                objDThongTinThau.NguoiSua = globalVariables.UserName;
                objDThongTinThau.NgaySua = BusinessHelper.GetSysDateTime();
            }
            else
            {
                objDThongTinThau.DaXacNhan = 0;
                objDThongTinThau.TrangThai = 0;

                objDThongTinThau.NgayTao = BusinessHelper.GetSysDateTime();
                objDThongTinThau.NguoiTao = globalVariables.UserName;
                objDThongTinThau.NguoiSua = null;
                objDThongTinThau.NgaySua = null;
                objDThongTinThau.IsNew = true;
            }
            objDThongTinThau.LoaiThau = Utility.sDbnull(txtLoaiThau.Text, string.Empty);
            objDThongTinThau.NhomThau = Utility.sDbnull(txtNhomThau.Text, string.Empty);
            objDThongTinThau.GoiThau = Utility.sDbnull(txtGoiThau.Text, string.Empty);
            objDThongTinThau.SoThau = Utility.sDbnull(txtSoThau.Text, string.Empty);
            objDThongTinThau.NgayThau = Convert.ToDateTime(dtNgayThau.Value);
            objDThongTinThau.SoQDinh = Utility.sDbnull(txtSoQDinh.Text, string.Empty);
            objDThongTinThau.NgayQDinh = Convert.ToDateTime(dtNgayQDinh.Value);
            objDThongTinThau.IdNhaCCap = Utility.Int16Dbnull(txtNhaThau.MyID, -1);
            objDThongTinThau.MaNhaCCap = Utility.sDbnull(txtNhaThau.MyCode, string.Empty);
            objDThongTinThau.GhiChu = Utility.sDbnull(txtThongTinKhac.Text, string.Empty);

            objDThongTinThau.DieuTiet = chkDieuTiet.Checked;
            if (chkDieuTiet.Checked)
            {
                objDThongTinThau.VienDieuTiet = Utility.Int32Dbnull(txtVienDieuTiet.MyID, -1);
                objDThongTinThau.SohdDieutiet = Utility.sDbnull(txtSoHD_Dieutiet.Text, string.Empty);
                objDThongTinThau.NgayhdDieutiet = Convert.ToDateTime(dtpNgayHD_DieuTiet.Value);
                objDThongTinThau.NgayktDieutiet = Convert.ToDateTime(dtpNgayKT_DieuTiet.Value);
            }
            objDThongTinThau.HtThau = Utility.Int32Dbnull(cbohinhthucthau.SelectedValue, -1);

            return objDThongTinThau;
        }
        private DThongTinThauChiTietCollection CreateChiTietThau()
        {
            DThongTinThauChiTietCollection colChiTiet = new DThongTinThauChiTietCollection();
            DThongTinThauChiTiet _obj = null;
            foreach (GridEXRow gridExRow in grdChiTietThau.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    _obj = new DThongTinThauChiTiet();
                    if (EmAction == action.Insert)
                    {
                        _obj.NgayTao = globalVariables.SysDate;
                        _obj.NguoiTao = globalVariables.UserName;
                        _obj.IdThau = -1;
                        _obj.IdChiTiet = -1;
                    }
                    else
                    {
                        _obj.NgaySua = globalVariables.SysDate;
                        _obj.NguoiSua = globalVariables.UserName;
                        _obj.IdThau = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdThau].Value);
                        _obj.IdChiTiet = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdChiTiet].Value);
                    }
                    _obj.IdThuoc = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdThuoc].Value);
                    _obj.IdDonVi = Utility.Int16Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdDonVi].Value);
                    _obj.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.DonGia].Value);
                    _obj.SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.SoLuong].Value, 0);
                    colChiTiet.Add(_obj);
                }
            }
            return colChiTiet;
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!InValid()) return;
            PerformAction();
        }
        private void PerformAction()
        {
            try
            {
                cmdSave.Enabled = false;
                switch (EmAction)
                {
                    case action.Insert:
                        ThemThongTinThau();
                        break;
                    case action.Update:
                        UpdateThongTinThau();
                        break;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception.Message);
            }
            finally
            {
                cmdSave.Enabled = true;
            }
        }
        private void ThemThongTinThau()
        {
            try
            {
                DThongTinThau objThau = CreateThongTinThau();
                DThongTinThauChiTietCollection collectChiTietThau = CreateChiTietThau();
                objThau.IsNew = true;
                objThau.Save();

                new Delete().From(DThongTinThauChiTiet.Schema).Where(DThongTinThauChiTiet.Columns.IdThau).IsEqualTo(objThau.IdThau).Execute();
                foreach (DThongTinThauChiTiet item in collectChiTietThau)
                {
                    item.IdThau = objThau.IdThau;
                    item.IsNew = true;
                    item.Save();
                }

                SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();

                EmAction = action.Update;
                txtIDThau.Text = Utility.sDbnull(objThau.IdThau);

                DThongTinThau objThongTinThau = DThongTinThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));

                DataRow newDr = dtThongTinThau.NewRow();
                Utility.FromObjectToDatarow(objThongTinThau, ref newDr);

                DNhaCungCap objNhaCC = DNhaCungCap.FetchByID(Utility.Int32Dbnull(objThau.IdNhaCCap, -1));
                if (objNhaCC != null)
                    newDr["TEN_NHA_CCAP"] = Utility.sDbnull(objNhaCC.TenNhaCcap);


                LStaffCollection collStaff = new Select().From(LStaff.Schema).ExecuteAsCollection<LStaffCollection>();
                if (collStaff != null)
                {
                    newDr["Ten_NguoiTao"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiTao);
                    newDr["Ten_NguoiSua"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiSua);
                    newDr["Ten_NguoiXacNhan"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiXacNhan);
                }

                dtThongTinThau.Rows.Add(newDr);

                Utility.GonewRowJanus(GrdList, DThongTinThau.Columns.IdThau, Utility.sDbnull(txtIDThau.Text));
                GetData();
                Utility.ShowMsg("Bạn thêm mới phiếu thành công", "Thông báo");

                BCancel = true;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }
        private void UpdateThongTinThau()
        {
            try
            {
                DThongTinThau objThau = CreateThongTinThau();
                DThongTinThauChiTietCollection collectChiTietThau = CreateChiTietThau();

                objThau.MarkOld();
                objThau.Save();

                new Delete().From(DThongTinThauChiTiet.Schema).Where(DThongTinThauChiTiet.Columns.IdThau).IsEqualTo(objThau.IdThau).Execute();
                foreach (DThongTinThauChiTiet item in collectChiTietThau)
                {
                    item.IdThau = objThau.IdThau;
                    item.IsNew = true;
                    item.Save();
                }

                SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();

                EmAction = action.Update;
                DThongTinThau objDThongTinThau = DThongTinThau.FetchByID(Utility.Int32Dbnull(txtIDThau.Text));

                DataRow[] arrDr = dtThongTinThau.Select(string.Format("{0}={1}", DThongTinThau.Columns.IdThau, Utility.Int32Dbnull(txtIDThau.Text)));
                if (arrDr.GetLength(0) > 0)
                {
                    arrDr[0].Delete();
                }

                DataRow newDr = dtThongTinThau.NewRow();
                Utility.FromObjectToDatarow(objDThongTinThau, ref newDr);

                DNhaCungCap objNhaCC = DNhaCungCap.FetchByID(Utility.Int32Dbnull(objThau.IdNhaCCap, -1));
                if (objNhaCC != null)
                    newDr["TEN_NHA_CCAP"] = Utility.sDbnull(objNhaCC.TenNhaCcap);

                LStaffCollection collStaff = new Select().From(LStaff.Schema).ExecuteAsCollection<LStaffCollection>();
                if (collStaff != null)
                {
                    newDr["Ten_NguoiTao"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiTao);
                    newDr["Ten_NguoiSua"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiSua);
                    newDr["Ten_NguoiXacNhan"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiXacNhan);
                }

                dtThongTinThau.Rows.Add(newDr);
                Utility.GonewRowJanus(GrdList, DThongTinThau.Columns.IdThau, Utility.sDbnull(txtIDThau.Text));
                Utility.ShowMsg("Bạn sửa  phiếu thành công", "Thông báo");
                GetData();
                BCancel = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình sửa phiếu: " + ex.Message, "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
        private bool InValid()
        {
            if (string.IsNullOrEmpty(txtLoaiThau.Text))
            {
                Utility.ShowMsg("Chưa nhập loại thầu", "Thông báo", MessageBoxIcon.Warning);
                txtLoaiThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNhomThau.Text))
            {
                Utility.ShowMsg("Chưa nhập nhóm thầu", "Thông báo", MessageBoxIcon.Warning);
                txtNhomThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtGoiThau.Text))
            {
                Utility.ShowMsg("Chưa nhập gói thầu", "Thông báo", MessageBoxIcon.Warning);
                txtGoiThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Số thầu", "Thông báo", MessageBoxIcon.Warning);
                txtSoThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNhaThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Nhà thầu", "Thông báo", MessageBoxIcon.Warning);
                txtNhaThau.Focus();
                return false;
            }
            else
            {
                if (Utility.Int16Dbnull(txtNhaThau.MyID, -1) <= 0)
                {
                    Utility.ShowMsg("Không tồn tại thông tin Nhà thầu. Đề nghị bạn bổ sung thông tin nhà thầu vào hệ thống trước.", "Thông báo", MessageBoxIcon.Warning);
                    txtNhaThau.Focus();
                    txtNhaThau.SelectAll();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(dtNgayThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Ngày thầu", "Thông báo", MessageBoxIcon.Warning);
                dtNgayThau.Focus();
                return false;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(dtNgayThau.Text);
                    if ((BusinessHelper.GetSysDateTime() - dt).TotalDays < 0)
                    {
                        Utility.ShowMsg("Ngày thầu không được lớn hơn ngày hệ thống(Ngày máy chủ).", "Thông báo", MessageBoxIcon.Warning);
                        dtNgayThau.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    Utility.ShowMsg("Chưa nhập Ngày thầu", "Thông báo", MessageBoxIcon.Warning);
                    dtNgayThau.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtSoQDinh.Text))
            {
                Utility.ShowMsg("Chưa nhập Số quyết định", "Thông báo", MessageBoxIcon.Warning);
                txtSoQDinh.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbohinhthucthau.SelectedIndex, -1) <= 0)
            {
                Utility.ShowMsg("Chưa chọn hình thức thầu", "Thông báo", MessageBoxIcon.Warning);
                cbohinhthucthau.Focus();
                return false;
            }

            if (grdChiTietThau.DataSource == null || (grdChiTietThau.DataSource != null && grdChiTietThau.GetDataRows().Count() <= 0))
            {
                Utility.ShowMsg("Chưa có chi tiết thầu.", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                foreach (GridEXRow gridExRow in grdChiTietThau.GetDataRows())
                {
                    decimal _nSoLuong = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.SoLuong].Value, -1);
                    string _sDrugName = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.DrugName].Value, string.Empty);
                    if (_nSoLuong <= 0)
                    {
                        Utility.ShowMsg(string.Format("Thuốc {0} chưa nhập số lượng thầu.", _sDrugName), "Thông báo", MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }
        private void grdChiTietThau_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "SO_LUONG")
            {
                int soluongchuyen = Utility.Int32Dbnull(e.Value);
                if (soluongchuyen < 0)
                {
                    Utility.ShowMsg("Số lượng thuốc cần chuyển phải >= 0", "Thông báo", MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
        private void cmdNext_Click(object sender, EventArgs e)
        {
            AddDetailNext();
        }
        private void AddDetailNext()
        {
            foreach (GridEXRow gridExRow in grdDanhSachThuoc.GetCheckedRows())
            {
                DataRow drv = dtChiTietThau.NewRow();
                drv[DThongTinThauChiTiet.Columns.IdThau] = Utility.Int32Dbnull(txtIDThau.Text, -1);
                drv[DThongTinThauChiTiet.Columns.IdThuoc] = Utility.Int32Dbnull(gridExRow.Cells[LDrug.Columns.DrugId].Value, -1);
                drv[DThongTinThauChiTiet.Columns.IdDonVi] = Utility.Int16Dbnull(gridExRow.Cells[LDrug.Columns.UnitId].Value, -1);
                drv[DThongTinThauChiTiet.Columns.SoLuong] = 0;
                drv[DThongTinThauChiTiet.Columns.DonGia] = Utility.DecimaltoDbnull(gridExRow.Cells[LDrug.Columns.Price].Value, 0);
                drv[LDrug.Columns.DrugName] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.DrugName].Value);
                drv[LDrug.Columns.Content] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Content].Value);
                drv[LDrug.Columns.Active] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Active].Value);
                drv[LDrug.Columns.Manufacturers] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Manufacturers].Value);
                drv[LDrug.Columns.Producer] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Producer].Value);
                drv[LNhomThuoc.Columns.TenNhomThuoc] = Utility.sDbnull(gridExRow.Cells[LNhomThuoc.Columns.TenNhomThuoc].Value);
                drv[LDrugType.Columns.DrugTypeName] = Utility.sDbnull(gridExRow.Cells[LDrugType.Columns.DrugTypeName].Value);
                drv[LMeasureUnit.Columns.UnitName] = Utility.sDbnull(gridExRow.Cells[LMeasureUnit.Columns.UnitName].Value);

                dtChiTietThau.Rows.Add(drv);
                dtChiTietThau.AcceptChanges();
                grdChiTietThau.UpdateData();
                grdChiTietThau.Refresh();
            }
            ResetValueInGridEx();
            ModifyCommnad();
        }
        private void cmdPrevius_Click(object sender, EventArgs e)
        {
            RemoveDetails();
        }
        private void RemoveDetails()
        {
            foreach (GridEXRow gridExRow in grdChiTietThau.GetCheckedRows())
            {
                gridExRow.Delete();
                grdChiTietThau.UpdateData();
                grdChiTietThau.Refresh();
                dtChiTietThau.AcceptChanges();
                dtDmThuoc.AcceptChanges();
            }
            ModifyCommnad();
        }
        private void ResetValueInGridEx()
        {
            foreach (GridEXRow gridExRow in grdDanhSachThuoc.GetCheckedRows())
            {
                gridExRow.BeginEdit();
                gridExRow.IsChecked = false;
                gridExRow.EndEdit();
            }
            grdDanhSachThuoc.UpdateData();
            dtDmThuoc.AcceptChanges();
        }
        private void txtSoThau_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoThau.Text) && EmAction == action.Insert)
            {
                DThongTinThauCollection _obj = new Select().From(DThongTinThau.Schema).Where(DThongTinThau.Columns.SoThau).IsEqualTo(Utility.sDbnull(txtSoThau.Text, string.Empty)).ExecuteAsCollection<DThongTinThauCollection>();
                if (_obj != null && _obj.Count > 0)
                {
                        Utility.ShowMsg(string.Format("Đã tồn tại Số thầu {0} trong CSDL. Bạn ko thể thêm Số thầu này", txtSoThau.Text), "Thông báo", MessageBoxIcon.Warning);
                        txtSoThau.Focus();
                        return;
                }
            }
        }
        DataTable _mDtDataNhaCungCap = null;
        private void frm_Add_ThongTinThau_Load(object sender, EventArgs e)
        {
            DataTable _mDtDataNhaCungCap = LoadDataCommon.CommonLoadDuoc.LAYTHONGTIN_NHA_CCAP(1);
            txtNhaThau.Init(_mDtDataNhaCungCap, new List<string>() { DNhaCungCap.Columns.IdNhaCcap, DNhaCungCap.Columns.MaNhaCcap, DNhaCungCap.Columns.TenNhaCcap });

            DataTable _mDtBenhVien = LoadDataCommon.CommonBusiness.LayThongTinBenhVien(1);
            txtVienDieuTiet.Init(_mDtBenhVien, new List<string>() { LBenhVien.Columns.IdBenhVien, LBenhVien.Columns.MaBenhVien, LBenhVien.Columns.TenBenhVien });

            DataTable _hinhThucThau = LoadDataCommon.CommonLoadDuoc.Lay_HinhThucThau();
            DataBinding.BindDataCombox(cbohinhthucthau, _hinhThucThau, HinhthucThau.Columns.IdHinhthuc, HinhthucThau.Columns.TenHinhthuc, "-----Chọn hình thức thầu-----");
            GetData();
        }
        private void cmdAddNhaCCap_Click(object sender, EventArgs e)
        {
            try
            {
                Form_DanhMuc.frm_AddNhaCungCap frm = new Form_DanhMuc.frm_AddNhaCungCap();
                frm.p_dtDataChung = _mDtDataNhaCungCap;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    _mDtDataNhaCungCap = LoadDataCommon.CommonLoadDuoc.LAYTHONGTIN_NHA_CCAP(1);
                    txtNhaThau.Init(_mDtDataNhaCungCap, new List<string>() { DNhaCungCap.Columns.IdNhaCcap, DNhaCungCap.Columns.MaNhaCcap, DNhaCungCap.Columns.TenNhaCcap });
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }
        }

        private void chkDieuTiet_CheckedChanged(object sender, EventArgs e)
        {
            pnlDieuTiet.Enabled = chkDieuTiet.Checked;
        }
    }
}