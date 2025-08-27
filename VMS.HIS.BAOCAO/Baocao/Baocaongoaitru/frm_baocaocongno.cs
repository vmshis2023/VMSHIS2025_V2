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
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_baocaocongno : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        KcbLuotkham objLuotkham = null;
        public frm_baocaocongno()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            txtMaluotkham.Leave += TxtMaluotkham_Leave;
            txtMaluotkham.KeyDown += TxtMaluotkham_KeyDown;
            
        }

        private void TxtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaluotkham.Text.Trim() != "")
                txtMaluotkham.Text = Utility.AutoFullPatientCode(txtMaluotkham.Text); ;
        }

        private void TxtMaluotkham_Leave(object sender, EventArgs e)
        {
            txtMaluotkham.Text = Utility.AutoFullPatientCode(txtMaluotkham.Text);
        }

        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_baocaocongno_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            this.Load += new EventHandler(frm_baocaocongno_Load);
            opt_tonghopcongnophaithu.CheckedChanged += _CheckedChanged;
            opt_chitietcongnophaithu.CheckedChanged += _CheckedChanged;
            ShowGrid();
        }

        private void _CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        void ShowGrid()
        {
            if (opt_tonghopcongnophaithu.Checked)
            {
                grd_tonghopcongnophaithu.BringToFront();
                baocaO_TIEUDE1.Init("congno_baocao_tonghopcongnophaithu");
            }
            else
            {
               
                    grd_chitietcongnophaithu.BringToFront();
                    baocaO_TIEUDE1.Init("congno_baocao_chitietcongnophaithu");
                
            }
        }
        DataTable m_dtKhoathucHien=new DataTable();
        private void frm_baocaocongno_Load(object sender, EventArgs eventArgs)
        {
            try
            {
               
                DataBinding.BindDataCombobox(cbo_thunganvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "Chọn nhân viên thu ngân", true);
               
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }

        }
        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocaocongno_KeyDown(object sender, KeyEventArgs e)
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
                if (opt_tonghopcongnophaithu.Checked)
                {
                    saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                    saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
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
                else
                {
                    saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                    saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                    //saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string sPath = saveFileDialog1.FileName;
                        FileStream fs = new FileStream(sPath, FileMode.Create);
                        fs.CanWrite.CompareTo(true);
                        fs.CanRead.CompareTo(true);
                        gridEXExporter2.Export(fs);
                        fs.Dispose();
                    }
                    saveFileDialog1.Dispose();
                    saveFileDialog1.Reset();
                }
                

            }
            catch (Exception exception)
            {

            }
        }
        void Tonghopcongnophaithu()
        {

            _dtData =                BAOCAO_NGOAITRU.CongnoBaocaoTonghopcongnoPhaithu(Utility.GetBeginDate(dtFromDate.Value),Utility.GetEndDate( dtToDate.Value), Utility.Int64Dbnull(txtIdbenhnhan.Text, -1), Utility.sDbnull(txtMaluotkham.Text));
                THU_VIEN_CHUNG.CreateXML(_dtData, "congno_baocao_tonghopcongnophaithu.xml");
                Utility.SetDataSourceForDataGridEx(grd_tonghopcongnophaithu, _dtData, false, true, "1=1", "");

            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);


            string Condition = string.Format("Từ ngày {0} đến {1} ", dtFromDate.Text, dtToDate.Text);

            string reportcode = "congno_baocao_tonghopcongnophaithu";
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
               Utility.SetParameterValue(crpt,"StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneybyLetter", new MoneyByLetter().sMoneyToLetter(tong_tien.ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {


            }
        }
        void Chitietcongnophaithu()
        {
            if(Utility.sDbnull(txtMaluotkham.Text)=="")
            {
                Utility.ShowMsg("Báo cáo chi tiết công nợ phải thu áp dụng cho từng người bệnh cụ thể. Do vậy bạn cần nhập mã khám của người bệnh cần xem báo cáo");
                txtMaluotkham.Focus();
                return;
            }
            KcbLuotkham objLk = Utility.getKcbLuotkham(Utility.sDbnull(txtMaluotkham.Text));
            if(objLk==null)
            {
                Utility.ShowMsg(string.Format("Không xác định được người bệnh qua mã lượt khám {0}(Có thể nhập sai hoặc người bệnh đã bị xóa). Vui lòng nhập mã lượt khám khác", Utility.sDbnull(txtMaluotkham.Text)));
                txtMaluotkham.Focus();
                return;
            }    
            _dtData = BAOCAO_NGOAITRU.CongnoBaocaoChitietcongnoPhaithu(Utility.GetBeginDate(dtFromDate.Value), Utility.GetEndDate(dtToDate.Value), objLk.IdBenhnhan, objLk.MaLuotkham);

            Utility.SetDataSourceForDataGridEx(grd_chitietcongnophaithu, _dtData, false, true, "1=1", "");
           

            THU_VIEN_CHUNG.CreateXML(_dtData, "congno_baocao_chitietcongnophaithu.xml");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);


            string Condition = string.Format("Từ ngày {0} đến {1}", dtFromDate.Text, dtToDate.Text);
            string reportcode = "congno_baocao_chitietcongnophaithu";
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
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", Condition);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sMoneybyLetter", new MoneyByLetter().sMoneyToLetter(tong_tien.ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {


            }
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            //if (objLuotkham == null)
            //    objLuotkham = Utility.getKcbLuotkham(txtMaluotkham.Text);
            if (opt_tonghopcongnophaithu.Checked)
                Tonghopcongnophaithu();
            else
                Chitietcongnophaithu();
              
        }
        

        private void cboKhoa_ThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void optThang_CheckedChanged(object sender, EventArgs e)
        {
            if (optThang.Checked)
            {
                cboThang.SelectedIndex = 0;
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtToDate.Value = endOfMonth;
            }
        }

        private void optQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (optQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtFromDate.Value = fromdate;
                dtToDate.Value = todate;
            }
        }

        private void optNam_CheckedChanged(object sender, EventArgs e)
        {
            if (optNam.Checked)
            {
                var myDate = dtpNam.Value;
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtToDate.Value = endOfMonth;
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtToDate.Value = endOfMonth;
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtFromDate.Value = fromdate;
                dtToDate.Value = todate;
            }
        }

        
    }
}
