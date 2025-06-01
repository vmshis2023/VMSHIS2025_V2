using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.HISLink.LoadDataCommon;
using NLog;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.Libs;

namespace VietBaIT.HISLink.UI.Duoc.Form_NghiepVu
{
    public partial class frm_Add_DieuTiet_Thau : Form
    {
        private readonly Logger log;
        public bool BCancel = false;
        public action EmAction = action.Insert;
        public GridEX GrdList;
        public DataTable dtThongTin_DieuTiet = new DataTable();
        private DataTable dtChiTiet_DieuTiet = new DataTable();
        private DataTable dtChiTietThau = new DataTable();
        private string kieuvattuthuoc = "TATCA";
        public int idThau = -1;
        public frm_Add_DieuTiet_Thau(string skieuvattuthuoc)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Utility.loadIconToForm(this);
            log = LogManager.GetCurrentClassLogger();
            kieuvattuthuoc = skieuvattuthuoc;
        }
        public frm_Add_DieuTiet_Thau()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Utility.loadIconToForm(this);
            log = LogManager.GetCurrentClassLogger();
        }
        private void frm_Add_DieuTiet_Thau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit_Click(cmdExit, new EventArgs());
            if (e.Control && e.KeyCode == Keys.S) cmdSave_Click(cmdSave, new EventArgs());
        }
        private void ModifyCommnad()
        {
            try
            {
                cmdSave.Enabled = grdChiTiet_DieuTiet.RowCount > 0;
                cmdIn.Enabled = grdChiTiet_DieuTiet.RowCount > 0;
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
            //int IdThau = Utility.Int32Dbnull(txtID_DieuTiet.Text, -1);
            //if (grdChiTiet_DieuTiet.GetDataRows().Count() <= 0)
            //{
            //    Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
            //    return;
            //}
            //ReportDocument crpt = new ReportDocument();
            //MoneyByLetter _moneyByLetter = new MoneyByLetter(); 
            //string reportcode = "DIEUTIET_THAU";
            //string path = Utility.sDbnull(SystemReports.GetPathReport(reportcode));
            //string sTitleReport = SystemReports.TieuDeBaoCao(reportcode);
            //if (File.Exists(path))
            //{
            //    crpt.Load(path);
            //}
            //else
            //{
            //    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", path), "Thông báo không tìm thấy File",  MessageBoxIcon.Warning);
            //    return;
            //}

            //frmPrintPreview objForm = new frmPrintPreview("ĐIỀU TIẾT THẦU", crpt, true, true);
            //objForm.crptTrinhKyName = Path.GetFileName(path);
            //crpt.SetDataSource(m_dtReport);
            //BusinessHelper.CreateXml(m_dtReport, "dsDieuTietThau.xsd");
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
                DDieuTietThau _objThau = DDieuTietThau.FetchByID(Utility.Int32Dbnull(txtID_DieuTiet.Text));
                if (_objThau != null)
                {
                    txtID_DieuTiet.Text = Utility.sDbnull(_objThau.IdThau, "-1");
                    txtSoThau.Text = _objThau.SoThau;
                    txtSoThau.SetId(_objThau.IdThau);
                    txtThongTinKhac.Text = Utility.sDbnull(_objThau.GhiChu);
                    txtVienDieuTiet.SetId(Utility.Int16Dbnull(_objThau.VienDieutiet, -1));

                    DThongTinThau _thau = DThongTinThau.FetchByID(_objThau.IdThau);
                    txtLoaiThau.Text = Utility.sDbnull(_thau.LoaiThau);
                    txtNhomThau.Text = Utility.sDbnull(_thau.NhomThau);
                    txtGoiThau.Text = Utility.sDbnull(_thau.GoiThau);
                    txtSoQDinh.Text = Utility.sDbnull(_thau.SoQDinh, "");
                }
            }
            dtChiTiet_DieuTiet = SPs.SpChiTietDieuTietThau(Utility.Int32Dbnull(txtID_DieuTiet.Text)).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdChiTiet_DieuTiet, dtChiTiet_DieuTiet, false, true, "1=1", "");
            GetThuocTrongKho();
        }
        private void GetThuocTrongKho()
        {
            try
            {
                dtChiTietThau = SPs.SpChiTietThongTinThau(Utility.Int32Dbnull(txtSoThau.MyID)).GetDataSet().Tables[0];
                Utility.AddColumToDataTable(ref dtChiTietThau, "ShortName", typeof(string));
                foreach (DataRow drv in dtChiTietThau.Rows)
                {
                    drv["ShortName"] = Utility.UnSignedCharacter(Utility.sDbnull(drv[LDrug.Columns.DrugName]));
                }
                dtChiTietThau.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grdChiTiet_Thau, dtChiTietThau, true, true, "", "");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
                log.Trace(exception);
            }
        }
        private DDieuTietThau CreateDieuTietThau()
        {
            var objDDieuTietThau = new DDieuTietThau();
            if (EmAction == action.Update)
            {
                objDDieuTietThau = DDieuTietThau.FetchByID(Utility.Int32Dbnull(txtID_DieuTiet.Text));
                objDDieuTietThau.MarkOld();
                objDDieuTietThau.IsLoaded = true;
                objDDieuTietThau.NguoiSua = globalVariables.UserName;
                objDDieuTietThau.NgaySua = BusinessHelper.GetSysDateTime();
            }
            else
            {
                objDDieuTietThau.NgayDieutiet = BusinessHelper.GetSysDateTime();
                objDDieuTietThau.NgayTao = BusinessHelper.GetSysDateTime();
                objDDieuTietThau.NguoiTao = globalVariables.UserName;
                objDDieuTietThau.NguoiSua = null;
                objDDieuTietThau.NgaySua = null;
                objDDieuTietThau.IsNew = true;
            }
            objDDieuTietThau.SohdDieutiet = Utility.sDbnull(txtSoHD_Dieutiet.Text, string.Empty);
            objDDieuTietThau.NgayhdDieutiet = Convert.ToDateTime(dtpNgayHD_DieuTiet.Value);
            objDDieuTietThau.NgayktDieutiet = Convert.ToDateTime(dtpNgayKT_DieuTiet.Value);

            objDDieuTietThau.IdThau = Utility.Int32Dbnull(txtSoThau.MyID, -1);
            objDDieuTietThau.SoThau = Utility.sDbnull(txtSoThau.Text, string.Empty);
            objDDieuTietThau.GhiChu = Utility.sDbnull(txtThongTinKhac.Text, string.Empty);
            objDDieuTietThau.VienDieutiet = Utility.Int32Dbnull(txtVienDieuTiet.MyID, -1);

            return objDDieuTietThau;
        }
        private DDieuTietThauCtCollection CreateChiTietDieuTietThau()
        {
            DDieuTietThauCtCollection colChiTiet = new DDieuTietThauCtCollection();
            DDieuTietThauCt _obj = null;
            foreach (GridEXRow gridExRow in grdChiTiet_DieuTiet.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    _obj = new DDieuTietThauCt();
                    _obj.IdThauCt = Utility.Int32Dbnull(gridExRow.Cells[DDieuTietThauCt.Columns.IdThauCt].Value);
                    _obj.SoLuong = Utility.Int32Dbnull(gridExRow.Cells[DDieuTietThauCt.Columns.SoLuong].Value, 0);
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
                        ThemDieuTietThau();
                        break;
                    case action.Update:
                        UpdateDieuTietThau();
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
        private void ThemDieuTietThau()
        {
            try
            {
                DDieuTietThau objDieuTiet = CreateDieuTietThau();
                DDieuTietThauCtCollection collectChiTietThau = CreateChiTietDieuTietThau();
                objDieuTiet.IsNew = true;
                objDieuTiet.Save();

                new Delete().From(DDieuTietThauCt.Schema).Where(DDieuTietThauCt.Columns.IdDieutiet).IsEqualTo(objDieuTiet.IdDieutiet).Execute();
                foreach (DDieuTietThauCt item in collectChiTietThau)
                {
                    item.IdDieutiet = objDieuTiet.IdDieutiet;
                    item.IsNew = true;
                    item.Save();
                }

                //SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();

                EmAction = action.Update;
                txtID_DieuTiet.Text = Utility.sDbnull(objDieuTiet.IdDieutiet);

                DDieuTietThau objThongTinThau = DDieuTietThau.FetchByID(Utility.Int32Dbnull(txtID_DieuTiet.Text));

                if (dtThongTin_DieuTiet != null)
                {
                    DataRow newDr = dtThongTin_DieuTiet.NewRow();
                    Utility.FromObjectToDatarow(objThongTinThau, ref newDr);

                    LStaffCollection collStaff = new Select().From(LStaff.Schema).ExecuteAsCollection<LStaffCollection>();
                    if (collStaff != null)
                    {
                        newDr["Ten_NguoiTao"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objDieuTiet.NguoiTao);
                        newDr["Ten_NguoiSua"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objDieuTiet.NguoiSua);
                    }

                    int idNhaCC = -1;
                    DataRow[] arrDr = txtSoThau.dtData.Select(string.Format("id_thau = {0}", txtSoThau.MyID));
                    if (arrDr.GetLength(0) > 0)
                    {
                        idNhaCC = Utility.Int32Dbnull(arrDr[0]["id_nha_ccap"]);
                    }
                    DNhaCungCap nhaCC = DNhaCungCap.FetchByID(idNhaCC);
                    if (nhaCC != null)
                    {
                        newDr["TEN_NHA_CCAP"] = nhaCC.TenNhaCcap;
                    }

                    newDr["Ten_BenhVien"] = txtVienDieuTiet.Text;

                    dtThongTin_DieuTiet.Rows.Add(newDr);
                }

                if (GrdList != null)
                {
                    Utility.GonewRowJanus(GrdList, DDieuTietThau.Columns.IdThau, Utility.sDbnull(txtID_DieuTiet.Text));
                }
                GetData();
                Utility.ShowMsg("Bạn thêm mới phiếu thành công", "Thông báo");

                BCancel = true;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }
        private void UpdateDieuTietThau()
        {
            try
            {
                DDieuTietThau objThau = CreateDieuTietThau();
                DDieuTietThauCtCollection collectChiTietThau = CreateChiTietDieuTietThau();

                objThau.MarkOld();
                objThau.Save();

                new Delete().From(DDieuTietThauCt.Schema).Where(DDieuTietThauCt.Columns.IdDieutiet).IsEqualTo(objThau.IdDieutiet).Execute();
                foreach (DDieuTietThauCt item in collectChiTietThau)
                {
                    item.IdDieutiet = objThau.IdDieutiet;
                    item.IsNew = true;
                    item.Save();
                }

                //SPs.SpPostTTThau2SoQDinh(Utility.Int32Dbnull(objThau.IdThau, -1)).Execute();

                EmAction = action.Update;
                if (dtThongTin_DieuTiet != null)
                {
                    DDieuTietThau objDDieuTietThau = DDieuTietThau.FetchByID(Utility.Int32Dbnull(txtID_DieuTiet.Text));

                    DataRow[] arrDr = dtThongTin_DieuTiet.Select(string.Format("{0}={1}", DDieuTietThau.Columns.IdThau, Utility.Int32Dbnull(txtID_DieuTiet.Text)));
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0].Delete();
                    }

                    DataRow newDr = dtThongTin_DieuTiet.NewRow();
                    Utility.FromObjectToDatarow(objDDieuTietThau, ref newDr);

                    LStaffCollection collStaff = new Select().From(LStaff.Schema).ExecuteAsCollection<LStaffCollection>();
                    if (collStaff != null)
                    {
                        newDr["Ten_NguoiTao"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiTao);
                        newDr["Ten_NguoiSua"] = collStaff.FirstOrDefault(staff => staff.StaffCode == objThau.NguoiSua);
                    }

                    dtThongTin_DieuTiet.Rows.Add(newDr);
                }
                if (GrdList != null)
                {
                    Utility.GonewRowJanus(GrdList, DDieuTietThau.Columns.IdThau, Utility.sDbnull(txtID_DieuTiet.Text));
                }
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
            if (string.IsNullOrEmpty(txtSoThau.Text))
            {
                Utility.ShowMsg("Chưa nhập Số thầu", "Thông báo", MessageBoxIcon.Warning);
                txtSoThau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtVienDieuTiet.Text))
            {
                Utility.ShowMsg("Không được để trống viện điều tiết", "Thông báo", MessageBoxIcon.Warning);
                txtVienDieuTiet.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtVienDieuTiet.MyID) <= 0)
            {
                Utility.ShowMsg("Viện điều tiết không tồn tại trong danh mục", "Thông báo", MessageBoxIcon.Warning);
                txtVienDieuTiet.Focus();
                return false;
            }

            if (grdChiTiet_DieuTiet.DataSource == null || (grdChiTiet_DieuTiet.DataSource != null && grdChiTiet_DieuTiet.GetDataRows().Count() <= 0))
            {
                Utility.ShowMsg("Chưa có chi tiết điều tiết.", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                foreach (GridEXRow gridExRow in grdChiTiet_DieuTiet.GetDataRows())
                {
                    decimal _nSoLuong = Utility.Int32Dbnull(gridExRow.Cells[DDieuTietThauCt.Columns.SoLuong].Value, -1);
                    string _sDrugName = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.DrugName].Value, string.Empty);
                    if (_nSoLuong <= 0)
                    {
                        Utility.ShowMsg(string.Format("Thuốc {0} chưa nhập số lượng điều tiết.", _sDrugName), "Thông báo", MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }
        private void grdChiTietDieuTiet_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (e.Column.Key == "SO_LUONG")
            {
                int slTonThau = Utility.Int32Dbnull(grdChiTiet_DieuTiet.GetValue("sl_conlai"));
                int soluongchuyen = Utility.Int32Dbnull(e.Value);
                if (soluongchuyen < 0)
                {
                    Utility.ShowMsg("Số lượng điều tiết phải >= 0", "Thông báo", MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                if (soluongchuyen > slTonThau)
                {
                    Utility.ShowMsg("Số lượng điều tiết phải <= số lượng tồn thầu", "Thông báo", MessageBoxIcon.Warning);
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
            foreach (GridEXRow gridExRow in grdChiTiet_Thau.GetCheckedRows())
            {
                DataRow drv = dtChiTiet_DieuTiet.NewRow();

                int _IdThauCt = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdChiTiet].Value, -1);

                IEnumerable<GridEXRow> query = from dieuTiet in grdChiTiet_DieuTiet.GetDataRows().AsEnumerable()
                                               where Utility.Int32Dbnull(dieuTiet.Cells[DDieuTietThauCt.Columns.IdThauCt].Value) == _IdThauCt
                                               select dieuTiet;
                if (!query.Any())
                {
                    drv[DDieuTietThauCt.Columns.IdThauCt] = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdChiTiet].Value, -1);
                    drv[DDieuTietThauCt.Columns.SoLuong] = 0;

                    drv[DThongTinThauChiTiet.Columns.IdThuoc] = Utility.Int32Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdThuoc].Value, -1);
                    drv[DThongTinThauChiTiet.Columns.IdDonVi] = Utility.Int16Dbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.IdDonVi].Value, -1);
                    drv[DThongTinThauChiTiet.Columns.DonGia] = Utility.DecimaltoDbnull(gridExRow.Cells[DThongTinThauChiTiet.Columns.DonGia].Value, 0);
                    drv["SL_ConLai"] = Utility.DecimaltoDbnull(gridExRow.Cells["SL_ConLai"].Value, 0);
                    drv[LDrug.Columns.DrugName] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.DrugName].Value);
                    drv[LDrug.Columns.Content] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Content].Value);
                    drv[LDrug.Columns.Active] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Active].Value);
                    drv[LDrug.Columns.Manufacturers] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Manufacturers].Value);
                    drv[LDrug.Columns.Producer] = Utility.sDbnull(gridExRow.Cells[LDrug.Columns.Producer].Value);
                    drv[LNhomThuoc.Columns.TenNhomThuoc] = Utility.sDbnull(gridExRow.Cells[LNhomThuoc.Columns.TenNhomThuoc].Value);
                    drv[LDrugType.Columns.DrugTypeName] = Utility.sDbnull(gridExRow.Cells[LDrugType.Columns.DrugTypeName].Value);
                    drv[LMeasureUnit.Columns.UnitName] = Utility.sDbnull(gridExRow.Cells[LMeasureUnit.Columns.UnitName].Value);

                    dtChiTiet_DieuTiet.Rows.Add(drv);
                    dtChiTiet_DieuTiet.AcceptChanges();
                    grdChiTiet_DieuTiet.UpdateData();
                    grdChiTiet_DieuTiet.Refresh();
                }
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
            foreach (GridEXRow gridExRow in grdChiTiet_DieuTiet.GetCheckedRows())
            {
                gridExRow.Delete();
                grdChiTiet_DieuTiet.UpdateData();
                grdChiTiet_DieuTiet.Refresh();
                dtChiTiet_DieuTiet.AcceptChanges();
                dtChiTietThau.AcceptChanges();
            }
            ModifyCommnad();
        }
        private void ResetValueInGridEx()
        {
            foreach (GridEXRow gridExRow in grdChiTiet_Thau.GetCheckedRows())
            {
                gridExRow.BeginEdit();
                gridExRow.IsChecked = false;
                gridExRow.EndEdit();
            }
            grdChiTiet_Thau.UpdateData();
            dtChiTietThau.AcceptChanges();
        }
        private void frm_Add_DieuTiet_Thau_Load(object sender, EventArgs e)
        {
            DataTable _mDtDataThau = new Select().From(DThongTinThau.Schema).ExecuteDataSet().Tables[0];
            txtSoThau.Init(_mDtDataThau, new List<string>() { DThongTinThau.Columns.IdThau, DThongTinThau.Columns.SoThau, DThongTinThau.Columns.SoThau });

            DataTable _mDtBenhVien = LoadDataCommon.CommonBusiness.LayThongTinBenhVien(1);
            txtVienDieuTiet.Init(_mDtBenhVien, new List<string>() { LBenhVien.Columns.IdBenhVien, LBenhVien.Columns.MaBenhVien, LBenhVien.Columns.TenBenhVien });
            GetData();
        }

        private void txtSoThau__OnEnterMe()
        {
            try
            {
                if (Utility.Int32Dbnull(txtSoThau.MyID, -1) > 0)
                {
                    DThongTinThau objThau = DThongTinThau.FetchByID(Utility.Int32Dbnull(txtSoThau.MyID, -1));
                    if (objThau != null)
                    {
                        txtSoThau._Text = objThau.SoThau;
                        txtSoQDinh.Text = objThau.SoQDinh;
                        txtGoiThau.Text =  objThau.GoiThau;
                        txtNhomThau.Text = objThau.NhomThau;
                        txtLoaiThau.Text = objThau.LoaiThau;
                    }

                    GetThuocTrongKho();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}