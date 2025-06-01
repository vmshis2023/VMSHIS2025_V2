using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C1.C1Excel;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_thongke_danhsachbenhnhanh_phongchucnang : Form
    {
        private readonly string Args = "ALL";

        private string reportname = "";
        private string tieude = "";

        public frm_thongke_danhsachbenhnhanh_phongchucnang(string sthamso)
        {
            Args = sthamso;
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtdichvu._OnEnterMe += txtdichvu__OnEnterMe;
        }

        private void txtdichvu__OnEnterMe()
        {
            //   AddServiceDetail();
        }

        private void AddServiceDetail()
        {
            // if (Utility.Int32Dbnull(txtdichvu.MyID, -1) > 0)
            // {
            //  txtdichvu.Text = txtdichvu.MyText;
            //  }
        }

        private void frm_thongke_danhsachbenhnhanh_sieuam_Load(object sender, EventArgs e)
        {
            try
            {
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb,
                                             "Chọn đối tượng KCB", true);
                DataTable m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);

                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                if (query.Any())
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                dtFromDate.Value = dtToDate.Value = dtNgayInPhieu.Value = DateTime.Now;
                string reportcode = "";
                baocaO_TIEUDE1.TIEUDE = "";
                switch (Args.Substring(0, 2))
                {
                    case "SA":
                        reportcode = "baocao_thongkedanhsach_sieuam";
                        break;
                    case "XQ":
                        reportcode = "baocao_thongkedanhsach_xquang";
                        break;
                    case "DT":
                        reportcode = "baocao_thongkedanhsach_dientim";
                        break;
                    case "NS":
                        reportcode = "baocao_thongkedanhsach_noisoi";
                        break;
                    case "PTTT":
                        reportcode = "baocao_thongkedanhsach_pttt";
                        break;
                    default:
                        reportcode = "baocao_thongkedanhsach_noisoi";
                        break;
                }
                Utility.GetReport(reportcode, ref tieude, ref reportname);
                baocaO_TIEUDE1.TIEUDE = tieude;
                LoaddanhmucCLS();
                // txtdichvu.dtData = 
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private void LoaddanhmucCLS()
        {
            DataTable dtdmuc = null;
            if (raddmucall.Checked)
            {
                dtdmuc = new Select().From(VDmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
            }
            if (radChuathuchien.Checked)
            {
                dtdmuc = new Select().From(VDmucDichvuclsChitiet.Schema).Where(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(0).ExecuteDataSet().Tables[0];
            }
            if (radDathuchien.Checked)
            {
                dtdmuc = new Select().From(VDmucDichvuclsChitiet.Schema).Where(VDmucDichvuclsChitiet.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
            }
            txtdichvu.Init(dtdmuc
                ,
                new List<string>
                        {
                            VDmucDichvuclsChitiet.Columns.IdChitietdichvu,
                            VDmucDichvuclsChitiet.Columns.MaChitietdichvu,
                            VDmucDichvuclsChitiet.Columns.TenChitietdichvu
                        });
            txtdichvu.SetId(-1);
        }
        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
            int trangthai = -1;
            if (radTatca.Checked) trangthai = -1;
            if (radDathuchien.Checked) trangthai = 1;
            if (radChuathuchien.Checked) trangthai = 0;
            DataTable dtDanhsach =
                SPs.BaocaoThongkedanhsachThuchienchucnang(chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1), chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1),
                                                          Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                                          Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), Args,
                                                          Utility.Int32Dbnull(txtdichvu.MyID, -1), trangthai)
                    .GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdResult, dtDanhsach, false, false, "", "");
            THU_VIEN_CHUNG.CreateXML(dtDanhsach, "baocao_thongkedanhsach_chucnang.XML");
            if (dtDanhsach.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu để báo cáo!");
                return;
            }
            Utility.UpdateLogotoDatatable(ref dtDanhsach);
            string reportCode = "";
            switch (Args.Substring(0, 2))
            {
                case "SA":
                    reportCode = "baocao_thongkedanhsach_sieuam";
                    break;
                case "XQ":
                    reportCode = "baocao_thongkedanhsach_xquang";
                    break;
                case "DT":
                    reportCode = "baocao_thongkedanhsach_dientim";
                    break;
                case "NS":
                    reportCode = "baocao_thongkedanhsach_noisoi";
                    break;
                case "PT":
                    reportCode = "baocao_thongkedanhsach_pttt";
                    break;
                default:
                    reportCode = "baocao_thongkedanhsach_noisoi";
                    break;
            }
            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3}",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboKhoa.SelectedIndex > 0
                                  ? Utility.sDbnull(cboKhoa.Text)
                                  : "Tất cả");
            ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, dtDanhsach.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtDanhsach);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        private void radTatca_CheckedChanged(object sender, EventArgs e)
        {
            //   if (radTatca.Checked) trangthai = -1;
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int trangthai = -1;
                if (radTatca.Checked) trangthai = -1;
                if (radDathuchien.Checked) trangthai = 1;
                if (radChuathuchien.Checked) trangthai = 0;
                DataTable dtDanhsach =
                    SPs.BaocaoThongkedanhsachThuchienchucnang(dtFromDate.Value, dtToDate.Value,
                        Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                        Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), Args,
                        Utility.Int32Dbnull(txtdichvu.MyID, -1), trangthai).GetDataSet().Tables[0];
                if (dtDanhsach.Rows.Count > 0)
                {
                    Utility.SetDataSourceForDataGridEx(grdResult, dtDanhsach, false, true, "1=1", "");
                    string reportcode = "";
                    string duongdan = "";
                    string codintion = "";
                     DataTable dt  = new DataTable();
                     int idxRow = 0;
                     int idxColSh = 0;
                     var book = new C1XLBook();
                     XLSheet sheet = book.Sheets[0];
                    string getTime = "";
                    string pathDirectory = "";
                    switch (Args.Substring(0, 2))
                    {
                        case "SA":
                             reportcode = "baocao_thongkedanhsach_sieuam";
                              duongdan = Utility.GetPathExcel(reportcode);
                              book = new C1XLBook();
                              book.Load(duongdan);
                              book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                              sheet = book.Sheets[0];
                              dt = dtDanhsach;
                              idxRow = 7;
                              idxColSh = 0;
                              codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                              sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                              for (int i = 0; i < dt.Rows.Count; i++)
                              {
                                  sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                  sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_khoaphong"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ket_qua"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                  idxRow = idxRow + 1;
                              }
                             getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                             pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                              if (!Directory.Exists(pathDirectory))
                              {
                                Directory.CreateDirectory(pathDirectory);
                               }

                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                              Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                        case "CH":
                            reportcode = "baocao_thongkedanhsach_sieuam";
                            duongdan = Utility.GetPathExcel(reportcode);
                            book = new C1XLBook();
                            book.Load(duongdan);
                            book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                            sheet = book.Sheets[0];
                            dt = dtDanhsach;
                            idxRow = 7;
                            idxColSh = 0;
                            codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                              dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                              cboDoituongKCB.Text);
                            sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                              HamDungChung.styleStringCenter(book));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_khoaphong"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ket_qua"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                            }
                            getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                            pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                  "TemplateExcel\\ExportExcel\\";
                            if (!Directory.Exists(pathDirectory))
                            {
                                Directory.CreateDirectory(pathDirectory);
                            }

                            book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                   reportcode +
                                   getTime + ".xls");
                            Process.Start(
                              new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                   "\\TemplateExcel\\ExportExcel\\" +
                                                   reportcode + getTime + ".xls"));
                            break;
                        case "XQ":
                            reportcode = "baocao_thongkedanhsach_xquang";
                              duongdan = Utility.GetPathExcel(reportcode);
                              book = new C1XLBook();
                              book.Load(duongdan);
                              book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                              sheet = book.Sheets[0];
                              dt = dtDanhsach;
                              idxRow = 7;
                              idxColSh = 0;
                              codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                              sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                              for (int i = 0; i < dt.Rows.Count; i++)
                              {
                                  sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                  sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_khoaphong"]), HamDungChung.styleNumber(book));
                                  sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ket_qua"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["Co_13_18"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["Co_18_24"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["Co_24_30"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["Co_30_40"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                  sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                  idxRow = idxRow + 1;
                              }
                             getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                             pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                              if (!Directory.Exists(pathDirectory))
                              {
                                Directory.CreateDirectory(pathDirectory);
                               }

                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                              Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                            break;
                        case "DT":
                            reportcode = "baocao_thongkedanhsach_dientim";
                                duongdan = Utility.GetPathExcel(reportcode);
                                book.Load(duongdan);
                                book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                                sheet = book.Sheets[0];
                                dt = dtDanhsach;
                                idxRow = 7;
                                idxColSh = 0;
                                 codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                                sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                               for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_khoaphong"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ket_qua"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                               }
                               getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                               pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                               if (!Directory.Exists(pathDirectory))
                               {
                                Directory.CreateDirectory(pathDirectory);
                               }
                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                               Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                        case "NS":
                                reportcode = "baocao_thongkedanhsach_noisoi";
                                duongdan = Utility.GetPathExcel(reportcode);
                                book.Load(duongdan);
                                book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                                sheet = book.Sheets[0];
                                dt = dtDanhsach;
                                idxRow = 7;
                                idxColSh = 0;
                                 codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                                sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                               for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_khoaphong"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ket_qua"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                               }
                               getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                               pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                               if (!Directory.Exists(pathDirectory))
                               {
                                Directory.CreateDirectory(pathDirectory);
                               }
                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                               Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                        case "PT":
                              reportcode = "baocao_thongkedanhsach_pttt";
                              duongdan = Utility.GetPathExcel(reportcode);
                              book = new C1XLBook();
                              book.Load(duongdan);
                              book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                              sheet = book.Sheets[0];
                              dt = dtDanhsach;
                              idxRow = 7;
                              idxColSh = 0;
                              codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                              sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                              for (int i = 0; i < dt.Rows.Count; i++)
                              {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["phuongphap_vocam"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["ngay_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["loai_phauthuat"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["bacsy_gayme"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                              }
                             getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                             pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                              if (!Directory.Exists(pathDirectory))
                              {
                                Directory.CreateDirectory(pathDirectory);
                               }

                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                              Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                        case "TT":
                            reportcode = "baocao_thongkedanhsach_tt";
                              duongdan = Utility.GetPathExcel(reportcode);
                              book = new C1XLBook();
                              book.Load(duongdan);
                              book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                              sheet = book.Sheets[0];
                              dt = dtDanhsach;
                              idxRow = 7;
                              idxColSh = 0;
                              codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                              sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                              for (int i = 0; i < dt.Rows.Count; i++)
                              {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["phuongphap_vocam"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["ngay_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["loai_phauthuat"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["bacsy_gayme"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                              }
                             getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                             pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                              if (!Directory.Exists(pathDirectory))
                              {
                                Directory.CreateDirectory(pathDirectory);
                               }

                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                              Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                        default:
                             reportcode = "baocao_thongkedanhsach_tt";
                              duongdan = Utility.GetPathExcel(reportcode);
                              book = new C1XLBook();
                              book.Load(duongdan);
                              book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                              sheet = book.Sheets[0];
                              dt = dtDanhsach;
                              idxRow = 7;
                              idxColSh = 0;
                              codintion = string.Format("Từ ngày {0} đến ngày {1}. Đối tượng: {2}",
                                dtFromDate.Value.ToString("dd/MM/yyyy"), dtToDate.Value.ToString("dd/MM/yyyy"),
                                cboDoituongKCB.Text);
                              sheet[3, idxColSh].SetValue(Convert.ToString(codintion),
                                HamDungChung.styleStringCenter(book));
                              for (int i = 0; i < dt.Rows.Count; i++)
                              {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1), HamDungChung.styleStringCenter(book));
                                sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["IsBHYT"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleNumber(book));
                                sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_chitietdichvu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["phuongphap_vocam"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["ngay_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["loai_phauthuat"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["nguoi_thuchien"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["bacsy_gayme"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["ghi_chu"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["so_luong"]), HamDungChung.styleStringLeft(book));
                                sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["so_benh_an"]), HamDungChung.styleStringLeft(book));
                                idxRow = idxRow + 1;
                              }
                             getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                             pathDirectory = AppDomain.CurrentDomain.BaseDirectory +
                                                   "TemplateExcel\\ExportExcel\\";
                              if (!Directory.Exists(pathDirectory))
                              {
                                Directory.CreateDirectory(pathDirectory);
                               }

                               book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                                      reportcode +
                                      getTime + ".xls");
                              Process.Start(
                                new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                                     "\\TemplateExcel\\ExportExcel\\" +
                                                     reportcode + getTime + ".xls"));
                            break;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu để báo cáo!");
                }
                //  ExcelUtlity.ExportGridEx(grdResult);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        private void raddmucall_CheckedChanged(object sender, EventArgs e)
        {
            LoaddanhmucCLS();
        }

        private void raddmuctruoc_CheckedChanged(object sender, EventArgs e)
        {
            LoaddanhmucCLS();
        }

        private void raddmucsau_CheckedChanged(object sender, EventArgs e)
        {
            LoaddanhmucCLS();
        }
    }
}