using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.C1Excel;
using CrystalDecisions.CrystalReports.Engine;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.BaocaoNoitru
{
    public partial class Frm_Sokhambenh_NoiTru : Form
    {
        private readonly string thamso = "";
        public DataTable _dtData = new DataTable();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";

        public Frm_Sokhambenh_NoiTru()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }
        public Frm_Sokhambenh_NoiTru(string sThamSo)
        {
            InitializeComponent();
           
            Initevents();
            thamso = sThamSo;
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }
        private void Initevents()
        {
            
            cmdExit.Click += cmdExit_Click;
            
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_Sokhambenh_NoiTru_Load(object sender, EventArgs e)
        {
            try
            {
                Autocompletenhanvien();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);
                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                dtNgayInPhieu.Value = globalVariables.SysDate;
                dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
                if (query.Any())
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }
        }

        private void Autocompletenhanvien()
        {
            DataTable _nhanvien = SPs.DmucLaydanhsachNhanvienTiepdon().GetDataSet().Tables[0];
            try
            {
                if (_nhanvien == null) return;
                if (!_nhanvien.Columns.Contains("ShortCut"))
                    _nhanvien.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in _nhanvien.Rows)
                {
                    string realName = dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    string shortcut = dr[DmucNhanvien.Columns.UserName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = arrWords.Where(word => word.Trim() != "").Aggregate("", (current, word) => current + (word + " "));
                    shortcut += _space; // +_Nospace;
                    shortcut = arrWords.Where(word => word.Trim() != "").Aggregate(shortcut, (current, word) => current + word.Substring(0, 1));
                    dr["ShortCut"] = shortcut;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                if (_nhanvien != null)
                {
                    EnumerableRowCollection<string> query = from p in _nhanvien.AsEnumerable()
                                                            select
                                                                p[DmucNhanvien.Columns.IdNhanvien] + "#" +
                                                                Utility.sDbnull(p[DmucNhanvien.Columns.UserName], "") + "@" +
                                                                p.Field<string>(DmucNhanvien.Columns.TenNhanvien) + "@" +
                                                                p.Field<string>("shortcut");
                    List<string> source = query.ToList();
                    txtNhanvientiepdon.AutoCompleteList = source;
                }
                txtNhanvientiepdon.TextAlign = HorizontalAlignment.Center;
                txtNhanvientiepdon.CaseSensitive = false;
                txtNhanvientiepdon.MinTypedCharacters = 1;
            }
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        private void Frm_Sokhambenh_NoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                _dtData =
                    SPs.BaocaoThongkeSonoitru(dtFromDate.Value, dtToDate.Value,
                                       Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                       Utility.Int16Dbnull(txtNhanvientiepdon.txtMyID, -1),
                                       Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
               Tables[0];
                if (_dtData.Rows.Count > 0)
                {
                    Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
                    const string reportcode = "baocao_thongke_sokhambenh_noitru";
                    string duongdan = Utility.GetPathExcel(reportcode);
                    var book = new C1XLBook();
                    book.Load(duongdan);
                    book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                    XLSheet sheet = book.Sheets[0];
                    DataTable dt = _dtData;
                    int idxRow = 9;
                    int idxColSh = 0;
                    string condition =
                          string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Tình trạng :{3}",
                          dtFromDate.Text, dtToDate.Text,
                          cboDoituongKCB.SelectedIndex >= 0
                              ? Utility.sDbnull(cboDoituongKCB.Text)
                              : "Tất cả",
                          cboTinhTrang.SelectedIndex > 0
                              ? Utility.sDbnull(cboKhoa.Text)
                              : "Tất cả");

                    sheet[4, idxColSh].SetValue(Convert.ToString(condition), HamDungChung.styleStringCenter(book));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sheet[idxRow, idxColSh].SetValue(Convert.ToString(i + 1),  HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["tuoinam"]), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["tuoinu"]), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["IsCongNhanVienChuc"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["Isbhyt"]),   HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["thanh_thi"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["nong_thon"]),  HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["Nho12thang"]), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["tu1den15tuoi"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["nghe_nghiep"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]),  HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["noi_gioithieu"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["ngay_vaovien"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["ngay_chuyenvien"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["ngay_ravien"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["tu_vong"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 17].SetValue(Convert.ToString(dt.Rows[i]["tu_vong_24h"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 18].SetValue(Convert.ToString(dt.Rows[i]["chandoantuyenduoi"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 19].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 20].SetValue(Convert.ToString(dt.Rows[i]["chandoan_khoa_dtri"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 21].SetValue(Convert.ToString(dt.Rows[i]["chandoan_khoa_giaiphau"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 22].SetValue(Convert.ToString(dt.Rows[i]["khoi"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 23].SetValue(Convert.ToString(dt.Rows[i]["do_giam"]),  HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 24].SetValue(Convert.ToString(dt.Rows[i]["nang_hon"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 25].SetValue(Convert.ToString(dt.Rows[i]["khong_thay_doi"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 26].SetValue(Convert.ToString(dt.Rows[i]["songay_dieutri"]), HamDungChung.styleStringLeft(book));
                        idxRow = idxRow + 1;
                    }
                    // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu t? 0                             
                    string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                    string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);
                    }

                    book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
                              getTime + ".xls");
                    Process.Start(
                        new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                                             "\\TemplateExcel\\ExportExcel\\" +
                                             reportcode + getTime + ".xls"));
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu để báo cáo!");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _dtData =
              SPs.BaocaoThongkeSokhambenh(dtFromDate.Value, dtToDate.Value,
                                          Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                          Utility.Int16Dbnull(txtNhanvientiepdon.txtMyID, -1),
                                          Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
                  Tables[0];

            Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
            THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thongke_sokhambenh.XML");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo",
                                MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
            reportname = "baocao_thongke_sokhambenh";

            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Người tiếp đón: {4}",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboKhoa.SelectedIndex > 0
                                  ? Utility.sDbnull(cboKhoa.Text)
                                  : "Tất cả", txtNhanvientiepdon.MyCode == "-1" ? "Tất cả" : txtNhanvientiepdon.Text);
            ReportDocument crpt = Utility.GetReport(reportname, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count > 0);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportname;
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
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

    }
}
