using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_chitiet_thuocdacapphat_benhnhan : Form
    {
        private readonly string kieuthuoc_vt = "THUOC";
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\ThongkethuoccapphatBN.txt";
        private MoneyByLetter _moneyByLetter = new MoneyByLetter();
        private bool hasLoaded;

        private DataTable m_dtKhoathucHien = new DataTable();

        #region "khai báo contructor của form hiện tại"

        #endregion

        public frm_baocao_chitiet_thuocdacapphat_benhnhan(string kieuthuoc_vt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.kieuthuoc_vt = kieuthuoc_vt;
            baocaO_TIEUDE1.Init();
            cmdExit.Click += cmdExit_Click;
            Text = baocaO_TIEUDE1.txtTieuDe.Text;

            Load += frm_baocao_chitiet_thuocdacapphat_benhnhan_Load;

            dtNgayInPhieu.Value = globalVariables.SysDate;

            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            cmdInPhieuXN.Click += cmdInPhieuXN_Click;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            cmdExportToExcel.Click += cmdExportToExcel_Click;
            KeyDown += frm_baocao_chitiet_thuocdacapphat_benhnhan_KeyDown;
            gridEXExporter1.GridEX = grdList;
        }

        /// <summary>
        /// hàm thực hiện việc load form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void frm_baocao_chitiet_thuocdacapphat_benhnhan_Load(object sender, EventArgs e)
        {
            if (kieuthuoc_vt == "THUOC")
                baocaO_TIEUDE1.Init("thuoc_baocao_chitiet_thuoccapphat_benhnhan");
            else
                baocaO_TIEUDE1.Init("vt_baocao_chitiet_vtcapphat_benhnhan");
            DataBinding.BindDataCombobox(cboDoiTuong, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                         DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb,
                                         "---Chọn đối tượng---", false);
            DataBinding.BindDataCombobox(cboStock,
                                         kieuthuoc_vt == "THUOC"
                                             ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()
                                             : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string>
                                                                                          {"TATCA", "NGOAITRU"}),
                                         TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho ---", false);
            m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
            Napcauhinh();
        }

        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// hàm thực hiên viecj in báo cáo doanh thu tiền khám chữa bệnh viện phí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            DataTable m_dtReport =
                BAOCAO_THUOC.ThuocBaocaoTinhhinhPhatthuocbenhnhan(
                    chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                    chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                    Utility.Int32Dbnull(cboStock.SelectedValue, -1),
                    Utility.Int32Dbnull(cboDoiTuong.SelectedValue, "-1"), chkThongketheongaychot.Checked ? 1 : 0,
                    kieuthuoc_vt);
            Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, false, true, "1=1", "");
            GridEXColumn gridExColumn = grdList.RootTable.Columns["THANH_TIEN"];
            if (m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            Utility.AddColumToDataTable(ref m_dtReport, "STT", typeof (Int32));
            int idx = 1;
            foreach (DataRow drv in m_dtReport.Rows)
            {
                drv["STT"] = idx;
                idx++;
            }
            m_dtReport.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            string Condition = string.Format("Từ ngày {0} đến {1}- Đối tượng {2} - Thuộc kho :{3}", dtFromDate.Text,
                                             dtToDate.Text,
                                             cboDoiTuong.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboDoiTuong.Text)
                                                 : "Tất cả",
                                             cboStock.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboStock.Text)
                                                 : "Tất cả");

            //  Utility.AddColumToDataTable(ref m_dtReport, "SO_PHIEU_BARCODE", typeof(byte[]));
            // byte[] arrBarCode = Utility.GenerateBarCode(barcode);
            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                string tieude = "", reportname = "";
                string reportCode = kieuthuoc_vt == "THUOC"
                                        ? "thuoc_baocao_chitiet_thuoccapphat_benhnhan"
                                        : "vt_baocao_chitiet_vtcapphat_benhnhan";
                ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview(baocaO_TIEUDE1.txtTieuDe.Text, crpt, true,
                                                  m_dtReport.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                // Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "Deparment_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName,
                                                             globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INGIAYXINNOP_TIENTHUOC", "0", false), 0m)
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INGIAYXINNOP_TIENTHUOC", "0", false) == "1")
                {
                    string tieude2 = "", reportname2 = "";
                    string reportCode2 = kieuthuoc_vt == "THUOC"
                                             ? "thuoc_baocao_giayxinnop_tienthuoc"
                                             : "vt_baocao_chitiet_vtcapphat_benhnhan";
                    ReportDocument crpt2 = Utility.GetReport(reportCode2, ref tieude2, ref reportname2);
                    if (crpt2 == null) return;
                    var objForm2 = new frmPrintPreview(baocaO_TIEUDE1.txtTieuDe.Text, crpt2, true,
                                                       m_dtReport.Rows.Count <= 0 ? false : true);
                    //try
                    //{
                    crpt2.SetDataSource(m_dtReport);

                    objForm2.mv_sReportFileName = Path.GetFileName(reportname2);
                    objForm2.mv_sReportCode = reportCode2;
                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                    // Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt2, "StaffName", StaffName);
                    Utility.SetParameterValue(crpt2, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt2, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt2, "Phone", globalVariables.Branch_Phone);
                    Utility.SetParameterValue(crpt2, "FromDateToDate", Condition);
                    Utility.SetParameterValue(crpt2, "sTitleReport", tieude2);
                    Utility.SetParameterValue(crpt2, "sCurrentDate",
                                              Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                    Utility.SetParameterValue(crpt2, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                    Utility.SetParameterValue(crpt2, "Deparment_Name", globalVariables.KhoaDuoc);
                    Utility.SetParameterValue(crpt2, "txtTrinhky",
                                              Utility.getTrinhky(objForm2.mv_sReportFileName,
                                                                 globalVariables.SysDate));
                    objForm2.crptViewer.ReportSource = crpt2;
                    objForm2.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }


        /// <summary>
        /// hàm thực iheenj viecj 
        /// export to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.txtTieuDe.Text);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
                    fs.CanWrite.CompareTo(true);
                    fs.CanRead.CompareTo(true);
                    gridEXExporter1.Export(fs);
                    fs.Dispose();
                }
                saveFileDialog1.Dispose();
                saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc của phím tắt khi thu tiền khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_chitiet_thuocdacapphat_benhnhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

        private void cmdInPhieuXN_Click_1(object sender, EventArgs e)
        {
        }

        private void chkNgayChot_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!hasLoaded) return;
                using (var _writer = new StreamWriter(strSaveandprintPath))
                {
                    _writer.WriteLine(chkThongketheongaychot.Checked ? "1" : "0");
                    _writer.Flush();
                    _writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void Napcauhinh()
        {
            try
            {
                Utility.CreateFolder(strSaveandprintPath);
                using (var _reader = new StreamReader(strSaveandprintPath))
                {
                    chkThongketheongaychot.Checked = _reader.ReadLine().Trim() == "1";
                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
            }
            catch
            {
            }
            finally
            {
                hasLoaded = true;
            }
        }
    }
}