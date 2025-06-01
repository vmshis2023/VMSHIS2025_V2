using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.DataAccessLayer;
using SubSonic;
using VietBaIT.HISLink.LoadDataCommon;
using C1.C1Excel;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using Aspose.Cells;
using Janus.Windows.GridEX;

namespace VietBaIT.HISLink.Reports.FORM_NOITIET
{
    public partial class frm_BCAO_CHITIET_THUTIEN : Form
    {
        public frm_BCAO_CHITIET_THUTIEN()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            cboNTNT.SelectedIndex = 0;
            cboKhoaThucHien.SelectedIndex = 0;
            cboLoaiIn.SelectedIndex = 0;
        }
        void Modifycommand()
        {
            pLoaiThanhToan.Enabled = radNganHang.Checked;
        }
        DataTable _mDtHinhThucTt = new DataTable();
        private void frm_BCAO_CHITIET_THUTIEN_Load(object sender, EventArgs e)
        {
            BindData2Combobox();
            LoadDataToComboboxCoSo();
            _mDtHinhThucTt = LoadDataCommon.CommonBusiness.LoadHinhThucThanhToan(true);
            DataBinding.BindDataCombox(cboHinhThucThanhToan, _mDtHinhThucTt, LHinhThucTT.Columns.IdHinhThuc, LHinhThucTT.Columns.TenHinhThuc, "== Tất cả ==");
            Modifycommand();
        }

        void BindData2Combobox()
        {
            try
            {
                DataTable dtDoiTuong = new Select().From(LObjectType.Schema).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombox(cboDoiTuong,dtDoiTuong,LObjectType.Columns.ObjectTypeCode,LObjectType.Columns.ObjectTypeName,"== Tất cả ==");
                cboDoiTuong.SelectedIndex = 0;
               // DataTable dtNgThu =
                //    new Select().From(LStaff.Schema).Where(LStaff.Columns.Uid).IsNotNull().ExecuteDataSet().Tables[0];
               // DataBinding.BindDataCombox(cboNguoiThu, dtNgThu, LStaff.Columns.Uid, LStaff.Columns.StaffName, "== Tất cả ==");
                DataTable dtNhanVien = LoadDataCommon.CommonBusiness.LayThongTinNguoiDungTaiChinh();
                txtnhanvien.Init(LoadDataCommon.CommonBusiness.LayThongTinNguoiDungTaiChinh(), new List<string>() { LStaff.Columns.StaffId, LStaff.Columns.StaffCode, LStaff.Columns.StaffName});

                //DataBinding.BindDataCombox(cboNguoiThu, LoadDataCommon.CommonBusiness.LayThongTinNguoiDungTaiChinh(),
                //                 LStaff.Columns.Uid, LStaff.Columns.StaffName, "---Chọn nhân viên thu ngân---");
                //cboNguoiThu.SelectedIndex = 0;


            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy dữ liệu vào combobox");               
            }
        }
        bool ValidateData()
        {
            try
            {
                if(dtpTuNgay.Value > dtpDenNgay.Value)
                {
                    Utility.ShowMsg("Ngày bắt đầu không thể chọn lớn hơn ngày kết thúc");
                    dtpTuNgay.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
        }
        private DataTable _dtPrint;
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //radNgayChot.Checked ? 0  : radNgayThanhToan.Checked? 1: 0
                int status = 0;
                if (radNgayChot.Checked) status = 0;
                if (radNgayThanhToan.Checked) status = 1;
                if (radChuaChot.Checked) status = 2;
                int tructuyen = -1;
                if (radNganHang.Checked)
                {
                    if (radNganHangThuong.Checked) tructuyen = 0;
                    if (radNganHangOnline.Checked) tructuyen = 1;
                }
                int baocaoDoanhthuquaythuoc = 1;
                if (chkQuayThuoc.Checked) baocaoDoanhthuquaythuoc = 1;
                else
                {
                    baocaoDoanhthuquaythuoc = 0;
                }
                //if (radThanhToanOnline.Checked) tructuyen = 1;
                if (!ValidateData())return;
                _dtPrint =
                    SPs.BcThienducBaocaoChitietThutien(dtpTuNgay.Value, dtpDenNgay.Value, Utility.Int16Dbnull(status, 0),
                        Utility.sDbnull(cboDoiTuong.SelectedValue, "-1"),
                        Utility.sDbnull(txtnhanvien.MyID, "-1"),
                        Utility.Int32Dbnull(cboNTNT.SelectedValue, -1),
                        Utility.sDbnull(cboKhoaThucHien.SelectedValue, "ALL"),
                        Utility.sDbnull(cboMaCoSo.SelectedValue, "ALL"),
                        globalVariables.MA_BENHVIEN,
                        "0", Utility.Int32Dbnull(cboHinhThucThanhToan.SelectedValue, -1), tructuyen,
                        Utility.Int32Dbnull(cboLoaiIn.SelectedValue), baocaoDoanhthuquaythuoc).GetDataSet().Tables[0];
                BusinessHelper.CreateXml(_dtPrint,"BAOCAO_THUTIEN_THAYTHECHOT.XML");

                Utility.SetDataSourceForDataGridEx(grdList, _dtPrint, false, true, "1=1", "");
                
                if (_dtPrint.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu theo điều kiện tìm kiếm.");
                    return;
                }
                string Condition =  string.Format("Từ ngày {0} đến {1} -Đối tượng:  {2} - Nhân viên: {3} - Loại điều trị: {4}  - Khoa thực hiện: {5}", dtpTuNgay.Text, dtpDenNgay.Text,
                                    cboDoiTuong.SelectedIndex > 0
                                        ? Utility.sDbnull(cboDoiTuong.Text)
                                        : "Tất cả",
                                    Utility.Int32Dbnull(txtnhanvien.MyID,-1) > 0
                                        ? Utility.sDbnull(txtnhanvien.Text)
                                        : "Tất cả", cboNTNT.SelectedIndex > 0 ? Utility.sDbnull(cboNTNT.Text) : "Tất cả",
                                        cboKhoaThucHien.SelectedIndex > 0 ? Utility.sDbnull(cboKhoaThucHien.Text) : "Tất cả");
                if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) ==0)
                VietBaIT.HISLink.Business.Reports.Implementation.BC_ThongKe.BC_BCAO_CHITIET_THUTIEN(_dtPrint, Condition);
                if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) == 1)
                    VietBaIT.HISLink.Business.Reports.Implementation.BC_ThongKe.BC_BCAO_TONGHOP_THUTIEN(_dtPrint, Condition);
                if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) == 2)
                {
                    VietBaIT.HISLink.Business.Reports.Implementation.BC_ThongKe.BC_BCAO_TONGHOP_THUTIEN_TOANVIEN(_dtPrint, Condition);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(string.Format("Có lỗi trong quá trình lấy dữ liệu :{0}", exception.Message.ToString()));
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_BCAO_CHITIET_THUTIEN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Escape)
                {
                    Close();
                }
                else if(e.KeyCode == Keys.F4)
                {
                    cmdPrint.PerformClick();
                }
            }
            catch (Exception)
            {
                
                
            }
        }
        void LoadDataToComboboxCoSo()
        {
            DataTable dataTable = CommonBusiness.LoadCoSo_NhanVien_BaoCao(Utility.Int16Dbnull(globalVariables.gv_StaffID));
            if (dataTable.Rows.Count > 0)
            {
                DataBinding.BindData(cboMaCoSo, dataTable, LCoSo.Columns.MaCoSo, LCoSo.Columns.TenCoSo);
                cboMaCoSo.SelectedValue = globalVariables.MA_COSO;
            }
        }
        
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) == 0)
            {
                int status = 0;
                if (radNgayChot.Checked) status = 0;
                if (radNgayThanhToan.Checked) status = 1;
                if (radChuaChot.Checked) status = 2;
                int tructuyen = -1;
                if (radNganHang.Checked)
                {
                    if (radNganHangThuong.Checked) tructuyen = 0;
                    if (radNganHangOnline.Checked) tructuyen = 1;
                }
                int baocaoDoanhthuquaythuoc = 1;
                if (chkQuayThuoc.Checked) baocaoDoanhthuquaythuoc = 1;
                else
                {
                    baocaoDoanhthuquaythuoc = 0;
                }
                //if (radThanhToanOnline.Checked) tructuyen = 1;
                if (!ValidateData()) return;
                _dtPrint =
                    SPs.BcThienducBaocaoChitietThutien(dtpTuNgay.Value, dtpDenNgay.Value, Utility.Int16Dbnull(status, 0),
                        Utility.sDbnull(cboDoiTuong.SelectedValue, "-1"),
                        Utility.sDbnull(txtnhanvien.MyID, "-1"),
                        Utility.Int32Dbnull(cboNTNT.SelectedValue, -1),
                        Utility.sDbnull(cboKhoaThucHien.SelectedValue, "ALL"),
                        Utility.sDbnull(cboMaCoSo.SelectedValue, "ALL"),
                        globalVariables.MA_BENHVIEN,
                        "0", Utility.Int32Dbnull(cboHinhThucThanhToan.SelectedValue, -1), tructuyen,
                        Utility.Int32Dbnull(cboLoaiIn.SelectedValue), baocaoDoanhthuquaythuoc).GetDataSet().Tables[0];
                BusinessHelper.CreateXml(_dtPrint, "BAOCAO_THUTIEN_THAYTHECHOT.XML");

                Utility.SetDataSourceForDataGridEx(grdList, _dtPrint, false, true, "1=1", "");

                if (_dtPrint.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu theo điều kiện tìm kiếm.");
                    return;
                }

                C1XLBook book = new C1XLBook();
                string duongdan = SystemReports.GetPathExcel(ReportCode.EXCEL_BC_THUTIEN_CHITIET);
                //book.Load(duongdan);
                // book.DefaultFont = new System.Drawing.Font("Time New Roman", 11, FontStyle.Regular);
                //XLSheet sheet = book.Sheets[0];
                DataTable dt = _dtPrint.Copy();
                //sheet[2, 0].SetValue(Convert.ToString(Utility.sDbnull(GetReportCondition())),
                //    HamDungChung.styleStringCenter(book));

                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(duongdan);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                Cells objcells = worksheet.Cells;// Get a particular Cell.
                Cell objcell = objcells[2, 0];
                //objcell.PutValue(GetReportCondition());
                Aspose.Cells.Cell cell;


                int idxRow = 5;
                int idxCol_SH = 0;
                int idx = idxRow;
                int endfor = dt.Rows.Count + idx;
                //if (globalVariables.UserName == "ADMINVB")
                //{
                //    Utility.ShowMsg(endfor.ToString());
                //    Utility.ShowMsg((dt.Rows.Count + idx).ToString());
                //    Utility.ShowMsg((dt.Rows.Count).ToString());
                //}
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cell = worksheet.Cells[idxRow, idxCol_SH]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["STT"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 1]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Payment_Code"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 2]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Patient_Id"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 3]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Patient_Code"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 4]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["BENHNHAN"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 5]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Payment_Date"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 6]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["THANH_TIEN_THU"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 7]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_QuayThuoc"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 8]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_TamUng"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Tra_lai"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THANH_TIEN_HUY"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 11]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_hoanquy"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 12]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Goi"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 13]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Huy_Goi"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 14]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THU_THU"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 15]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Mat"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 16]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_CaThe"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 17]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_ChuyenKhoan"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 18]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_CNo"])); SetBorderStype(cell, TextAlignmentType.Right);


                    idxRow = idxRow + 1;
                }
                cell = worksheet.Cells[idxRow, idxCol_SH + 5]; cell.PutValue("Tổng cộng: "); SetBorderStype(cell, TextAlignmentType.Center);
                cell = worksheet.Cells[idxRow, idxCol_SH + 6]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THANH_TIEN_THU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 7]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_QuayThuoc)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 8]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_TamUng)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Tra_lai)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THANH_TIEN_HUY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 11]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_hoanquy)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 12]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Goi)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 13]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Huy_Goi)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 14]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THU_THU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 15]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Mat)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 16]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_CaThe)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 17]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_ChuyenKhoan)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 18]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_CNo)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
               

                //workbook.CalculateFormula();
                worksheet.AutoFitRows();
                worksheet.AutoFitColumns();
                // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu từ 0
                int inDexRowTableNext = idxRow;
                int inDexColumn = 0;
                string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                string saveAsLocation = AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + "bcThuTienChiTiet" +
                          getTime + ".xls";
                workbook.Save(saveAsLocation);
                System.Diagnostics.Process.Start(saveAsLocation);
            }
            
            else if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) == 1)
            {
                //grdList.DataSource = _dtPrint;
                gridEXExporter1.GridEX = grdList;

                if (gridEXExporter1.GridEX.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = "BÁO CÁO THU TIỀN TỔNG HỢP.xls";
                //saveFileDialog1.ShowDialog();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    FileStream fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();
            }
            else if (Utility.Int32Dbnull(cboLoaiIn.SelectedValue) == 2)
            {
                
                C1XLBook book = new C1XLBook();
                string duongdan = SystemReports.GetPathExcel(ReportCode.EXCEL_BC_THUTIEN_TONGHOP_TOANVIEN);
                //book.Load(duongdan);
                // book.DefaultFont = new System.Drawing.Font("Time New Roman", 11, FontStyle.Regular);
                //XLSheet sheet = book.Sheets[0];
                DataTable dt = _dtPrint.Copy();
                //sheet[2, 0].SetValue(Convert.ToString(Utility.sDbnull(GetReportCondition())),
                //    HamDungChung.styleStringCenter(book));

                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(duongdan);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                Cells objcells = worksheet.Cells;// Get a particular Cell.
                Cell objcell = objcells[2, 0];
                //objcell.PutValue(GetReportCondition());
                Aspose.Cells.Cell cell;


                int idxRow = 5;
                int idxCol_SH = 0;
                int idx = idxRow;
                int endfor = dt.Rows.Count + idx;
                //if (globalVariables.UserName == "ADMINVB")
                //{
                //    Utility.ShowMsg(endfor.ToString());
                //    Utility.ShowMsg((dt.Rows.Count + idx).ToString());
                //    Utility.ShowMsg((dt.Rows.Count).ToString());
                //}
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cell = worksheet.Cells[idxRow, idxCol_SH]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["STT"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 1]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Payment_Date"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 2]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["THANH_TIEN_THU"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 3]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_QuayThuoc"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 4]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_TamUng"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 5]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Tra_lai"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 6]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THANH_TIEN_HUY"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 7]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_hoanquy"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 8]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Goi"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tien_Huy_Goi"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THU_THU"])); SetBorderStype(cell, TextAlignmentType.Right);
                   


                    // cell = worksheet.Cells[idxRow, idxCol_SH + 68]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["BHTT"])); SetBorderStype(cell, TextAlignmentType.Right);
                    idxRow = idxRow + 1;
                }
                cell = worksheet.Cells[idxRow, idxCol_SH + 1]; cell.PutValue("Tổng cộng: "); SetBorderStype(cell, TextAlignmentType.Center);
                cell = worksheet.Cells[idxRow, idxCol_SH + 2]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THANH_TIEN_THU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 3]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_QuayThuoc)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 4]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_TamUng)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 5]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Tra_lai)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 6]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THANH_TIEN_HUY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 7]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_hoanquy)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 8]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Goi)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tien_Huy_Goi)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                cell = worksheet.Cells[idxRow, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THU_THU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                


                //workbook.CalculateFormula();
                worksheet.AutoFitRows();
                worksheet.AutoFitColumns();
                // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu từ 0
                int inDexRowTableNext = idxRow;
                int inDexColumn = 0;
                string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                string saveAsLocation = AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + "bcThuTienTongHopToanVien" +
                          getTime + ".xls";
                workbook.Save(saveAsLocation);
                System.Diagnostics.Process.Start(saveAsLocation);
            }

        }
        public static void SetBorderStype(Aspose.Cells.Cell cell, TextAlignmentType TextAlignment)
        {
            Aspose.Cells.Style style = cell.GetStyle();
            //Setting the line style of the top border
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the top border
            style.Borders[BorderType.TopBorder].Color = Color.Black;

            //Setting the line style of the bottom border
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the bottom border
            style.Borders[BorderType.BottomBorder].Color = Color.Black;

            //Setting the line style of the left border
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the left border
            style.Borders[BorderType.LeftBorder].Color = Color.Black;

            //Setting the line style of the right border
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the right border
            style.Borders[BorderType.RightBorder].Color = Color.Black;

            style.VerticalAlignment = TextAlignment;
            style.HorizontalAlignment = TextAlignment;
            //style.Font= new Aspose.Cells.Font("Times New Roman", 10, FontStyle.Bold);
            //  style.Font
            // Aspose.Cells.Font font = new Aspose.Cells.Font("Times New Roman", 10,);
            // var font = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            // cell.SetStyle();
            //style.se(TextAlignmentType.CENTER);
            //Apply the border styles to the cell
            //style.Custom = "";
            cell.SetStyle(style);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
