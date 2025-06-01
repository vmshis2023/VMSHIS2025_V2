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
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_sokhambenh : Form
    {
        private readonly string thamso = "";
        public DataTable _dtData = new DataTable();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";

        public frm_sokhambenh(string sThamSo)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            thamso = sThamSo;
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }

        private void Initevents()
        {
            KeyDown += frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown;
            cmdExit.Click += cmdExit_Click;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load;
        }

        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                Autocompletenhanvien();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
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
                    _nhanvien.Columns.Add(new DataColumn("ShortCut", typeof (string)));
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
                Utility.ShowMsg("Lỗi:"+ ex.Message);
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

        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            Utility.ExportExcel(grdList);

                //try
                //{
                //    _dtData =
                //        SPs.BaocaoThongkeSokhambenh(dtFromDate.Value, dtToDate.Value,
                //                           Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                //                           Utility.Int16Dbnull(txtNhanvientiepdon.txtMyID, -1),
                //                           Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, 
                //                           Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
                //   Tables[0];
                //    if (_dtData.Rows.Count > 0)
                //    {
                //        Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
                //        const string reportcode = "baocao_thongke_sokhambenh";
                //        string duongdan = Utility.GetPathExcel(reportcode);
                //        var book = new C1XLBook();
                //        book.Load(duongdan);
                //        book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                //        XLSheet sheet = book.Sheets[0];
                //        DataTable dt = _dtData;
                //        int idxRow = 7;
                //        int idxColSh = 0;
                //        string condition =
                //              string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Người tiếp đón: {4}",
                //              dtFromDate.Text, dtToDate.Text,
                //              cboDoituongKCB.SelectedIndex >= 0
                //                  ? Utility.sDbnull(cboDoituongKCB.Text)
                //                  : "Tất cả",
                //              cboKhoa.SelectedIndex > 0
                //                  ? Utility.sDbnull(cboKhoa.Text)
                //                  : "Tất cả", txtNhanvientiepdon.MyCode == "-1" ? "Tất cả" : txtNhanvientiepdon.Text);

                //        sheet[3, idxColSh].SetValue(Convert.ToString(condition), HamDungChung.styleStringCenter(book));
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            if (i == 0)
                //            {
                //                //sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ngay_tiepdon"]), HamDungChung.styleStringLeft_Bold(book));
                //                //idxRow = idxRow + 1;
                //                sheet[idxRow, idxColSh].SetValue(string.Format("{0} {1}", "Ngày: ", Convert.ToString(dt.Rows[i]["ngay_tiepdon"])), HamDungChung.styleStringLeft_Bold(book));
                //                idxRow = idxRow + 1;
                //            }
                //            else
                //            {
                //                if (dt.Rows[i]["ngay_tiepdon"].ToString() != dt.Rows[i - 1]["ngay_tiepdon"].ToString())
                //                {
                //                    //sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["ngay_tiepdon"]), HamDungChung.styleStringLeft_Bold(book));
                //                    //idxRow = idxRow + 1;
                //                    sheet[idxRow, idxColSh].SetValue(string.Format("{0} {1}", "Ngày: ", Convert.ToString(dt.Rows[i]["ngay_tiepdon"])), HamDungChung.styleStringLeft_Bold(book));
                //                    idxRow = idxRow + 1;
                //                }
                //                //if (dt.Rows[i]["ngay_tiepdon"].ToString() == dt.Rows[i - 1]["ngay_tiepdon"].ToString())
                //                //{
                //                //    sheet[idxRow, idxColSh].SetValue(string.Format("{0} {1}", "Ngày: ", Convert.ToString(dt.Rows[i]["ngay_tiepdon"])), HamDungChung.styleStringLeft_Bold(book));
                //                //    idxRow = idxRow + 1;
                //                //}
                //            }
                //            sheet[idxRow, idxColSh].SetValue(Convert.ToString(i+ 1), HamDungChung.styleStringCenter(book));
                //            sheet[idxRow, idxColSh + 1].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nam"]), HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh_nu"]), HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["mathe_bhyt"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["noi_gioithieu"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["chandoan_tuyenduoi"]),
                //                HamDungChung.styleStringLeft(book));

                //            sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["ten_benh"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["ChiDinhVaDieuTri"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["Vaovien"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["Tuyentren"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["Tuyenduoi"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["NgoaiTru"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["Venha"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 15].SetValue(Convert.ToString(dt.Rows[i]["Thuthuat"]),
                //           HamDungChung.styleStringLeft(book));

                //            sheet[idxRow, idxColSh + 16].SetValue(Convert.ToString(dt.Rows[i]["KhamChuyenkhoa"]),
                //           HamDungChung.styleStringLeft(book));
                          
                //            sheet[idxRow, idxColSh + 17].SetValue(Convert.ToString(dt.Rows[i]["ten_nhanvien"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 18].SetValue(Convert.ToString(dt.Rows[i]["Doituongthuphi"]),
                //                HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 19].SetValue(Convert.ToString(dt.Rows[i]["Doituongmienphi"]),
                //           HamDungChung.styleStringLeft(book));
                //            sheet[idxRow, idxColSh + 20].SetValue(Convert.ToString(dt.Rows[i]["DoituongCapCuu"]), HamDungChung.styleStringLeft(book));
                //            //sheet[idxRow, idxColSh + 21].SetValue(Convert.ToString(dt.Rows[i]["ngay_tiepdon"]), HamDungChung.styleStringLeft(book));
                //            idxRow = idxRow + 1;
                //        }
                //        // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu t? 0                             
                //        string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                //        string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel\\ExportExcel\\";
                //        if (!Directory.Exists(pathDirectory))
                //        {
                //            Directory.CreateDirectory(pathDirectory);
                //        }

                //        book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + reportcode +
                //                  getTime + ".xls");
                //        Process.Start(
                //            new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory +
                //                                 "\\TemplateExcel\\ExportExcel\\" +
                //                                 reportcode + getTime + ".xls"));
                //    }
                //    else
                //    {
                //        Utility.ShowMsg("Không có dữ liệu để báo cáo!");
                //    }
                   
                //}
                //catch (Exception ex)
                //{
                //    Utility.ShowMsg("Lỗi: " + ex.Message);
                //}
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _dtData =
                SPs.BaocaoThongkeSokhambenh(dtFromDate.Value, dtToDate.Value,
                                            Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                            Utility.Int16Dbnull(txtNhanvientiepdon.txtMyID, -1),
                                            Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, Utility.Int16Dbnull(cboTinhTrang.SelectedValue,-1)).GetDataSet().
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
                Utility.SetParameterValue(crpt, "txtTrinhky",Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
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