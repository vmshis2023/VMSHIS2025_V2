using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Libs;
using VNS.HIS.DAL;

using Microsoft.VisualBasic;
using SubSonic;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_thuockedon_theobacsy : Form
    {
        private AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private int _idThuoc = new int();
        private string _mabschidinh = "-1";
        public frm_baocao_thuockedon_theobacsy()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
             this.cmdExit.Click+=new EventHandler(cmdExit_Click);
           
            this.Load += new EventHandler(frm_baocao_thuockedon_theobacsy_Load);
            
            dtNgayInPhieu.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init();
            this.Text = baocaO_TIEUDE1.TIEUDE;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            cmdInPhieuXN.Click+=new EventHandler(cmdInPhieuXN_Click);
            chkByDate.CheckedChanged+=new EventHandler(chkByDate_CheckedChanged);
            cmdExportToExcel.Click+=new EventHandler(cmdExportToExcel_Click);
            gridEXExporter1.GridEX = grdList;
        }
         DataTable m_dtKhoathucHien = new DataTable();
        #region "khai báo contructor của form hiện tại"
       
        #endregion

        /// <summary>
        /// hàm thực hiện việc load form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private DataTable dt_BacSyChiDinh = new DataTable();
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter=new MoneyByLetter();
        /// <summary>
        /// hàm thực hiên viecj in báo cáo doanh thu tiền khám chữa bệnh viện phí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _mabschidinh = "-1";
            _idThuoc = -1;
            // Lấy Id Bác sỹ
            if (!string.IsNullOrEmpty(cboBacSyChiDinh.Text))
            {
                var query = (from chk in cboBacSyChiDinh.CheckedValues.AsEnumerable()
                             let x = Utility.sDbnull(chk)
                             select x).ToArray();
                if (query.Count() > 0)
                {
                    _mabschidinh = string.Join(",", query);
                }
            }

            //Truyền dữ liệu vào datatable
            DataTable m_dtReport = BAOCAO_THUOC.ThuocBaocaoTinhhinhkedonthuocTheobacsy(Utility.Int32Dbnull(cboStock.SelectedValue, -1), Utility.Int32Dbnull(cboDoiTuong.SelectedValue, -1),
                _mabschidinh, _idThuoc, chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900"), chkByDate.Checked ? dtToDate.Value : globalVariables.SysDate,Utility.Int16Dbnull(cboTrangthai.SelectedValue, -1));
            if (m_dtReport == null) return;
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocaokedon_theobacsy.xml");
            //Kiểm tra dữ liệu
            if (m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            //Truyền tổng tiền vào text và chuyển qua tiền bằng chữ
            Janus.Windows.GridEX.GridEXColumn gridExColumnTong = grdList.RootTable.Columns["thanh_tien"];
            decimal tong =
                Utility.DecimaltoDbnull(grdList.GetTotal(gridExColumnTong, Janus.Windows.GridEX.AggregateFunction.Sum));
          

            m_dtReport.AcceptChanges();
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            //Truyền dữ liệu vào datagrid-view
            Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, false, true, "1=1", "");
            //Lấy chuỗi condition truyền vào biến ?FromDateToDate trên crpt
            string Condition = string.Format("Từ ngày {0} đến {1}- Đối tượng {2} - Thuộc kho :{3} - Bác sỹ: {4}", dtFromDate.Text, dtToDate.Text,
                                             cboDoiTuong.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboDoiTuong.SelectedValue)
                                                 : "Tất cả",
                                             cboStock.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboStock.Text)
                                                 : "Tất cả",
                                                 string.IsNullOrEmpty(cboBacSyChiDinh.Text) ? "Tất cả" : cboBacSyChiDinh.Text);

            //Lấy tên người tạo báo cáo và gọi crpt
            string StaffName = globalVariables.gv_strTenNhanvien;
            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("thuoc_baocaokedon_theobacsy", ref tieude, ref reportname);
            if (crpt == null) return;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thuoc_baocaokedon_theobacsy";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt, "TienBangChu", _moneyByLetter.sMoneyToLetter(tong.ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
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
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
          
        }
        /// <summary>
        /// hàm thực hiện việc của phím tắt khi thu tiền khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frm_BAOCAO_HUYBIENLAI_TONGHOP_Load(object sender, EventArgs e)
        {
        
        }
       
       

        private void frm_baocao_thuockedon_theobacsy_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F4)cmdInPhieuXN.PerformClick();
            if(e.KeyCode==Keys.F5)cmdExportToExcel.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

       
        private void cmdExportToExcel_Click_1(object sender, EventArgs e)
        {
            
        }
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
        private void frm_baocao_thuockedon_theobacsy_Load(object sender, EventArgs e)
        {
            try
            {
                baocaO_TIEUDE1.Init();
                AutocompleteThuoc();
                DataBinding.BindDataCombobox(cboDoiTuong, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb,
                                           "---Chọn đối tượng---", false);
                DataBinding.BindDataCombobox(cboStock, CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA(),
                                        TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "---Chọn kho ---", false);
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,-1);
                cboBacSyChiDinh.DropDownDataSource = dtBacsi;
                cboBacSyChiDinh.DropDownDataMember = DmucNhanvien.Columns.IdNhanvien;
                cboBacSyChiDinh.DropDownDisplayMember = DmucNhanvien.Columns.TenNhanvien;
                cboBacSyChiDinh.DropDownValueMember = DmucNhanvien.Columns.UserName;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
    }
}