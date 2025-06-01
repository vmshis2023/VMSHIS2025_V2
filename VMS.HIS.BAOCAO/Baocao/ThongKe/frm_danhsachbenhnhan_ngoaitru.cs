using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Baocao
{
    public partial class frm_danhsachbenhnhan_ngoaitru : Form
    {
        private readonly string thamso = "";
        public DataTable _dtData = new DataTable();
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        private bool m_blnhasLoaded = false;
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";
        private decimal tong_tien = 0m;

        public frm_danhsachbenhnhan_ngoaitru(string sThamSo)
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
                cbolankham.SelectedIndex = 0;
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
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
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
                ExcelUtlity.ExportGridEx(grdList);
                //saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                //saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    string sPath = saveFileDialog1.FileName;
                //    FileStream fs = new FileStream(sPath, FileMode.Create);
                //    fs.CanWrite.CompareTo(true);
                //    fs.CanRead.CompareTo(true);
                //    gridEXExporter1.Export(fs);
                //    fs.Dispose();
                //}
                //saveFileDialog1.Dispose();
                //saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
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
                SPs.BaocaoThongkeDanhsachBenhnhan(dtFromDate.Value, dtToDate.Value,
                                            Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                            Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, 
                                            Utility.Int16Dbnull(cbolankham.SelectedValue,-1)).GetDataSet().
                    Tables[0];

            Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");
            THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thongke_danhsach_benhnhan.XML");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo",
                                MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
            reportname = "baocao_thongke_danhsach_benhnhan";

            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3}",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboKhoa.SelectedIndex > 0
                                  ? Utility.sDbnull(cboKhoa.Text)
                                  : "Tất cả");
            ReportDocument crpt = Utility.GetReport(reportname, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
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


        private void cboKhoa_ThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void txtNhanvientiepdon_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtLoaikham_TextChanged(object sender, EventArgs e)
        {
        }
    }
}