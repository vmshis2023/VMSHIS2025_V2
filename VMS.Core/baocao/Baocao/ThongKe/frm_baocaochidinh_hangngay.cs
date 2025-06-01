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
using Janus.Windows.GridEX;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_baocaochidinh_hangngay : Form
    {
        private readonly string thamso = "";
        public DataTable _dtData = new DataTable();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";

        public frm_baocaochidinh_hangngay()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
     
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
            
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

              
              
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
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
           

                try
                {
                    _dtData =
                        SPs.BaoCaoChiDinhHangNgay(dtFromDate.Value, dtToDate.Value,
                                           Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),                              
                                         Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
                   Tables[0];
                    if (_dtData.Rows.Count > 0)
                    {
                        Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
                        const string reportcode = "BAOCAO_HANGNGAY";
                        string duongdan = Utility.GetPathExcel(reportcode);
                        var book = new C1XLBook();
                        book.Load(duongdan);
                        book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                        XLSheet sheet = book.Sheets[0];
                        DataTable dt = _dtData;
                        int idxRow = 5;
                        int idxColSh = 0;
                        string condition =
                              string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Trạng thái :{3} ",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboTinhTrang.SelectedIndex > 0
                                  ? Utility.sDbnull(cboTinhTrang.Text)
                                  : "Tất cả");

                        sheet[3, idxColSh].SetValue(Convert.ToString(condition), HamDungChung.styleStringCenter(book));
                        int sttloaidichvu = 1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                                idxRow = idxRow + 1;
                            }
                            else
                            {
                                if (dt.Rows[i]["DoiTuong"].ToString() != dt.Rows[i - 1]["DoiTuong"].ToString())
                                {
                                    sheet[idxRow, idxColSh].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                                    idxRow = idxRow + 1;
                                }
                                //if (dt.Rows[i]["DoiTuong"].ToString() == dt.Rows[i - 1]["DoiTuong"].ToString() && dt.Rows[i]["Ten_nhombaocao_dichvu"].ToString() != dt.Rows[i - 1]["Ten_nhombaocao_dichvu"].ToString())
                                //{
                                //    sheet[idxRow, idxColSh].SetValue(string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])), HamDungChung.styleStringLeft_Bold(book));
                                //    sttloaidichvu = sttloaidichvu + 1;
                                //    idxRow = idxRow + 1;
                                //}
                            }
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(i+ 1), HamDungChung.styleStringCenter(book));
                            sheet[idxRow, idxColSh + 1].SetValue(Convert.ToInt64(dt.Rows[i]["id_benhnhan"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(dt.Rows[i]["ma_luotkham"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(dt.Rows[i]["ten_benhnhan"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(dt.Rows[i]["nam_sinh"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(dt.Rows[i]["gioitinh"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(dt.Rows[i]["mathe_bhyt"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(dt.Rows[i]["gt_the_tu"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(dt.Rows[i]["gt_the_den"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(dt.Rows[i]["dia_chi"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(dt.Rows[i]["mabenh_chinh"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(dt.Rows[i]["mabenh_phu"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(dt.Rows[i]["ma_kcbbd"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(dt.Rows[i]["ngay_vao"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(dt.Rows[i]["ngay_ra"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(dt.Rows[i]["tong_tien"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(dt.Rows[i]["t_bhyt_chitra"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(dt.Rows[i]["t_bnhan_chitra"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 18].SetValue(Convert.ToString(dt.Rows[i]["loai_kcb"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 19].SetValue(Convert.ToString(dt.Rows[i]["loai_dichvu"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 20].SetValue(Convert.ToString(dt.Rows[i]["ma_dichvu"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 21].SetValue(Convert.ToString(dt.Rows[i]["ten_dichvu"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(dt.Rows[i]["so_luong"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(dt.Rows[i]["don_gia"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(dt.Rows[i]["thanh_tien"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(dt.Rows[i]["bhyt_chitra"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 26].SetValue(Convert.ToDecimal(dt.Rows[i]["bnhan_chitra"]), HamDungChung.styleNumber(book));
                            sheet[idxRow, idxColSh + 27].SetValue(Convert.ToString(dt.Rows[i]["ngay_thanhtoan"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 28].SetValue(Convert.ToString(dt.Rows[i]["ten_dung_tuyen"]), HamDungChung.styleStringLeft(book));
                            sheet[idxRow, idxColSh + 29].SetValue(Convert.ToString(dt.Rows[i]["DoiTuong"]), HamDungChung.styleStringLeft(book));    
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

        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _dtData =
                SPs.BaoCaoChiDinhHangNgay(dtFromDate.Value, dtToDate.Value,
                                           Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                         Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
                   Tables[0];

            Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
            THU_VIEN_CHUNG.CreateXML(_dtData, "BAOCAO_HANGNGAY.XML");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo",
                                MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
            reportname = "BAOCAO_HANGNGAY";

            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Trạng thái :{3} ",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboTinhTrang.SelectedIndex > 0
                                  ? Utility.sDbnull(cboTinhTrang.Text)
                                  : "Tất cả");
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

        private void cboXuatTrenLuoi_Click(object sender, EventArgs e)
        {
            try
            {
                _dtData =
                    SPs.BaoCaoChiDinhHangNgay(dtFromDate.Value, dtToDate.Value,
                                       Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                     Utility.Int16Dbnull(cboTinhTrang.SelectedValue, -1)).GetDataSet().
               Tables[0];
                if (_dtData.Rows.Count > 0)
                {
                    Utility.SetDataSourceForDataGridEx(grdList, _dtData, true, true, "1=1", "");
                    const string reportcode = "BAOCAO_HANGNGAY";
                    string duongdan = Utility.GetPathExcel(reportcode);
                    var book = new C1XLBook();
                    book.Load(duongdan);
                    book.DefaultFont = new Font("Time New Roman", 11, FontStyle.Regular);
                    XLSheet sheet = book.Sheets[0];
                    DataTable dt = _dtData;
                    int idxRow = 5;
                    int idxColSh = 0;
                    string condition =
                          string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Trạng thái :{3} ",
                          dtFromDate.Text, dtToDate.Text,
                          cboDoituongKCB.SelectedIndex >= 0
                              ? Utility.sDbnull(cboDoituongKCB.Text)
                              : "Tất cả",
                          cboTinhTrang.SelectedIndex > 0
                              ? Utility.sDbnull(cboTinhTrang.Text)
                              : "Tất cả");

                    sheet[3, idxColSh].SetValue(Convert.ToString(condition), HamDungChung.styleStringCenter(book));
                    int idx = 0;
                    foreach (GridEXRow grdExRow in grdList.GetDataRows())
                    {
                        if (idx == 0)
                        {
                            sheet[idxRow, idxColSh].SetValue(Convert.ToString(grdExRow.Cells["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                            idxRow = idxRow + 1;
                        }
                        else
                        {
                            if (dt.Rows[idx]["DoiTuong"].ToString() != dt.Rows[idx - 1]["DoiTuong"].ToString())
                            {
                                sheet[idxRow, idxColSh].SetValue(Convert.ToString(grdExRow.Cells["DoiTuong"]), HamDungChung.styleStringLeft_Bold(book));
                                idxRow = idxRow + 1;
                            }
                            //if (dt.Rows[i]["DoiTuong"].ToString() == dt.Rows[i - 1]["DoiTuong"].ToString() && dt.Rows[i]["Ten_nhombaocao_dichvu"].ToString() != dt.Rows[i - 1]["Ten_nhombaocao_dichvu"].ToString())
                            //{
                            //    sheet[idxRow, idxColSh].SetValue(string.Format("{0}.{1}", sttloaidichvu, Convert.ToString(dt.Rows[i]["Ten_nhombaocao_dichvu"])), HamDungChung.styleStringLeft_Bold(book));
                            //    sttloaidichvu = sttloaidichvu + 1;
                            //    idxRow = idxRow + 1;
                            //}
                        }
                        sheet[idxRow, idxColSh].SetValue(Convert.ToString(idx + 1), HamDungChung.styleStringCenter(book));
                        sheet[idxRow, idxColSh + 1].SetValue(Convert.ToInt64(grdExRow.Cells["id_benhnhan"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 2].SetValue(Convert.ToString(grdExRow.Cells["ma_luotkham"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 3].SetValue(Convert.ToString(grdExRow.Cells["ten_benhnhan"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 4].SetValue(Convert.ToString(grdExRow.Cells["nam_sinh"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 5].SetValue(Convert.ToString(grdExRow.Cells["gioitinh"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 6].SetValue(Convert.ToString(grdExRow.Cells["mathe_bhyt"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 7].SetValue(Convert.ToString(grdExRow.Cells["gt_the_tu"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 8].SetValue(Convert.ToString(grdExRow.Cells["gt_the_den"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 9].SetValue(Convert.ToString(grdExRow.Cells["dia_chi"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 10].SetValue(Convert.ToString(grdExRow.Cells["mabenh_chinh"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 11].SetValue(Convert.ToString(grdExRow.Cells["mabenh_phu"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 12].SetValue(Convert.ToString(grdExRow.Cells["ma_kcbbd"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 13].SetValue(Convert.ToString(grdExRow.Cells["ngay_vao"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 14].SetValue(Convert.ToString(grdExRow.Cells["ngay_ra"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 15].SetValue(Convert.ToDecimal(grdExRow.Cells["tong_tien"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 16].SetValue(Convert.ToDecimal(grdExRow.Cells["t_bhyt_chitra"].Value), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 17].SetValue(Convert.ToDecimal(grdExRow.Cells["t_bnhan_chitra"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 18].SetValue(Convert.ToString(grdExRow.Cells["loai_kcb"].Value), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 19].SetValue(Convert.ToString(grdExRow.Cells["loai_dichvu"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 20].SetValue(Convert.ToString(grdExRow.Cells["ma_dichvu"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 21].SetValue(Convert.ToString(grdExRow.Cells["ten_dichvu"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 22].SetValue(Convert.ToDecimal(grdExRow.Cells["so_luong"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 23].SetValue(Convert.ToDecimal(grdExRow.Cells["don_gia"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 24].SetValue(Convert.ToDecimal(grdExRow.Cells["thanh_tien"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 25].SetValue(Convert.ToDecimal(grdExRow.Cells["bhyt_chitra"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 26].SetValue(Convert.ToDecimal(grdExRow.Cells["bnhan_chitra"].Value ), HamDungChung.styleNumber(book));
                        sheet[idxRow, idxColSh + 27].SetValue(Convert.ToString(grdExRow.Cells["ngay_thanhtoan"].Value ), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 28].SetValue(Convert.ToString(grdExRow.Cells["ten_dung_tuyen"]), HamDungChung.styleStringLeft(book));
                        sheet[idxRow, idxColSh + 29].SetValue(Convert.ToString(grdExRow.Cells["DoiTuong"]), HamDungChung.styleStringLeft(book));   
                        idx++;
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
    }
}