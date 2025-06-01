using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Libs;
using VNS.HIS.DAL;
//using reports.Baocao;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class fm_baocaochietkhaucong : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public fm_baocaochietkhaucong()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtpNam.Value=dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            txtLoaiDV._OnEnterMe += txtLoaiDV__OnEnterMe;
        }

        void txtLoaiDV__OnEnterMe()
        {
            string _currentCode = txtDvu.MyCode;
            DataTable dtDvu = new Select().From(DmucDichvuclsChitiet.Schema).Where(DmucDichvuclsChitiet.Columns.IdDichvu).IsEqualTo(txtLoaiDV.MyID).ExecuteDataSet().Tables[0];
            txtDvu.Init(dtDvu, new List<string>() { DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.MaChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu });
            txtDvu.SetCode(_currentCode);
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
            
        }
        
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                GetAllDoctors();
                DataTable dt_loaiDvu = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                txtLoaiDV.Init(dt_loaiDvu, new List<string>() { DmucDichvucl.Columns.IdDichvu, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu });
                DataTable dtDvu = new Select().From(DmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                txtDvu.Init(dtDvu, new List<string>() { DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.MaChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu });
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
            grbDate.Enabled = chkByDate.Checked;

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
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
               
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
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

            }
        }
        public DataTable GetAllDoctors()
        {
            DataTable result = null;
            List<string> lstKhoaphong = new List<string>();
            if (VNS.Properties.PropertyLib._HinhAnhProperties == null) VNS.Properties.PropertyLib._HinhAnhProperties = VNS.Properties.PropertyLib.GetHinhAnhProperties();
            lstKhoaphong = VNS.HIS.UI.HinhAnh.Util.GetKhoaPhong();
            string _khoaphong = THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_KHOAPHONG_BSTHIEN", "", true);
            if (_khoaphong != "") lstKhoaphong = _khoaphong.Split(',').ToList<string>();
            try
            {
                if (lstKhoaphong.Count <= 0)
                    result =
                        new Select().From(DmucNhanvien.Schema)
                        .ExecuteDataSet().Tables[0];
                else
                    result =
                        new Select().From(DmucNhanvien.Schema)
                        .Where(DmucNhanvien.Columns.IdPhong).In(lstKhoaphong)
                        .ExecuteDataSet().Tables[0];
                txtBSthuchien.Init(result, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            }
            catch (Exception)
            {

            }
            return result;
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            string fromdate = "01/01/2000";
            string todate = "01/01/2000";
            string _value = "1";
            string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
            if (chkByDate.Checked)
            {
                FromDateToDate = "";
                fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                todate = dtToDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
               
                if (optThang.Checked)
                {
                    if (cboThang.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Tháng báo cáo");
                        cboThang.Focus();
                        return;
                    }
                    _value = cboThang.SelectedValue.ToString();
                    FromDateToDate = "Tháng " + _value;
                    switch (_value)
                    {
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 29).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                        case "6":
                        case "9":
                        case "11":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 30).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optQuy.Checked)
                {
                    if (cboQuy.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Quý báo cáo");
                        cboQuy.Focus();
                        return;
                    }
                    _value = cboQuy.SelectedValue.ToString();
                    FromDateToDate = "Quý " + _value;
                    switch (_value)
                    {
                        case "1":
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                            break;
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, 4, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 6, 30).ToString("dd/MM/yyyy");
                            break;
                        case "3":
                            fromdate = new DateTime(dtpNam.Value.Year, 7, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 9, 30).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                            fromdate = new DateTime(dtpNam.Value.Year, 10, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optNam.Checked)
                {
                    FromDateToDate = "Năm " + dtpNam.Value.Year.ToString();
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                    todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                }
                else
                {
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                }
            }
            
            
            _dtData =
               BAOCAO_NGOAITRU.BaocaoChietkhauCong(
                chkByDate.Checked ? fromdate : "01/01/1900",
                chkByDate.Checked ? todate : globalVariables.SysDate.ToString("dd/MM/yyyy"),
                Utility.Int32Dbnull(txtLoaiDV.MyID, -1), Utility.Int32Dbnull(txtDvu.MyID, -1),txtBSthuchien.MyCode);
            THU_VIEN_CHUNG.CreateXML(_dtData, "BaocaoChietkhauCong.xml");
            Utility.SetDataSourceForDataGridEx(grdTonghop, _dtData, false, true, "1=1", "");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
            string Condition = string.Format("{0} Từ ngày {1} đến {2} - Loại dịch vụ : {3} - Dịch vụ chỉ định :{4}", FromDateToDate, fromdate, todate,
                                         txtLoaiDV.MyID!="-1"
                                              ? txtLoaiDV.Text
                                              : "Tất cả",
                                         txtDvu.MyID!="-1"
                                              ? txtDvu.Text
                                              : "Tất cả");
            string reportcode = "baocao_chietkhaucong";
            var crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                frmPrintPreview objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count <= 0 ? false : true);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "Tongtien_chu", new MoneyByLetter().sMoneyToLetter(tong_tien.ToString()));
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
            }
        }
        


       
    }
}
