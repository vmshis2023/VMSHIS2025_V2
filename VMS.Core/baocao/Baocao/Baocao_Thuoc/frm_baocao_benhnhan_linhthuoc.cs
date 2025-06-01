using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using VNS.Libs;
using VNS.HIS.DAL;
using Microsoft.VisualBasic;
using VNS.HIS.UI.BaoCao;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.Properties;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_benhnhan_linhthuoc : Form
    {
        bool hasLoaded = false;
        string kieuthuoc_vt = "THUOC";
        public frm_baocao_benhnhan_linhthuoc(string kieuthuoc_vt)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.kieuthuoc_vt = kieuthuoc_vt;
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
         
            
            this.Load += new EventHandler(frm_baocao_benhnhan_linhthuoc_Load);
            
            dtNgayInPhieu.Value = globalVariables.SysDate;
           
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            cmdInPhieuXN.Click += new EventHandler(cmdInPhieuXN_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
           
            cmdExportToExcel.Click += new EventHandler(cmdExportToExcel_Click);
            this.KeyDown += new KeyEventHandler(frm_baocao_benhnhan_linhthuoc_KeyDown);
            txtthuoc._OnGridSelectionChanged += txtdrug__OnGridSelectionChanged;
            txtthuoc._OnChangedView += txtdrug__OnChangedView;
            gridEXExporter1.GridEX = grdList;
        }
        private int id_thuockho = -1;
        private bool _allowDrugChanged;
        private void txtdrug__OnGridSelectionChanged(string ID, int id_thuockho, string _name, string Dongia, string phuthu, int tutuc)
        {
            this.id_thuockho = id_thuockho;
            _allowDrugChanged = false;
            txtDrug_ID.Text = ID;
        }
        private void txtdrug__OnChangedView(bool gridview)
        {
            PropertyLib._AppProperties.GridView = gridview;
            PropertyLib.SaveProperty(PropertyLib._AppProperties);
        }
        DataTable m_dtKhoathucHien = new DataTable();
        #region "khai báo contructor của form hiện tại"
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = new Select().From(DmucThuoc.Schema).ExecuteDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// hàm thực hiện việc load form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void frm_baocao_benhnhan_linhthuoc_Load(object sender, EventArgs e)
        {
            AutocompleteThuoc();
            if (kieuthuoc_vt == "THUOC")
                baocaO_TIEUDE1.Init("thuoc_baocao_danhsachBenhnhan_linhthuoc");
            else
                baocaO_TIEUDE1.Init("vt_baocao_danhsachBenhnhan_linhvt");
            DataBinding.BindDataCombobox(cboDoiTuong, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                       DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb,
                                       "---Chọn đối tượng---",false);
            DataBinding.BindDataCombobox(cboStock, kieuthuoc_vt == "THUOC" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE() : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU" }),
                                    TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho ---", false);
            m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI",0);
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiên viecj in báo cáo doanh thu tiền khám chữa bệnh viện phí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            DataTable m_dtReport =
                 BAOCAO_THUOC.ThuocBaocaoTinhhinhBenhnhanlinhthuoc(
                     chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"),
                     chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,
                     Utility.Int32Dbnull(cboStock.SelectedValue, -1), Utility.Int32Dbnull(cboDoiTuong.SelectedValue, "-1"), Utility.Int32Dbnull(txtDrug_ID.Text, -1));
            Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, false, true, "1=1", "");
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns["THANH_TIEN"];


            decimal tonghuy = Utility.DecimaltoDbnull(grdList.GetTotal(gridExColumn, Janus.Windows.GridEX.AggregateFunction.Sum));
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocao_danhsachBenhnhan_linhthuoc");
            if (m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            Utility.AddColumToDataTable(ref m_dtReport, "STT", typeof(Int32));
            int idx = 1;
            foreach (DataRow drv in m_dtReport.Rows)
            {
                drv["STT"] = idx;
                idx++;
            }
            m_dtReport.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            string Condition = string.Format("Từ ngày {0} đến {1}- Đối tượng {2} - Thuộc kho :{3}", dtFromDate.Text, dtToDate.Text,
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
                 string tieude="", reportname = "";
                string reportCode=kieuthuoc_vt == "THUOC" ? "thuoc_baocao_danhsachBenhnhan_linhthuoc" : "vt_baocao_danhsachBenhnhan_linhvt";
                ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return;
                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                // Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"StaffName", StaffName);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt,"FromDateToDate", Condition);
                Utility.SetParameterValue(crpt,"sTitleReport", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt,"Deparment_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                            Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                               globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {

                Utility.ShowMsg("Lỗi:"+ exception.Message);
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
                saveFileDialog1.Filter = @"Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.TIEUDE);
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
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi: " + exception.Message);
            }

        }
        /// <summary>
        /// hàm thực hiện việc của phím tắt khi thu tiền khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_benhnhan_linhthuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
    }
}
