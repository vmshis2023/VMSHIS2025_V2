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
using VNS.Libs;
using VMS.HIS.DAL;


using VNS.Properties;

using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.Baocao;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_soluong_thuoc_trongkho : Form
    {
        private HisDuocProperties HisDuocProperties;
        string KIEU_THUOC_VT = "THUOC";
          string lstStockID = "-1";
                
        //TDmucKho _item = null;
        bool allowChanged = false;
        string KieuKho = "";
        public frm_baocao_soluong_thuoc_trongkho(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KieuKho = args.Split('-')[0];
            this.KIEU_THUOC_VT = args.Split('-')[1];
            dtNgayIn.Value = dtp_TuNgay.Value = dtp_DenNgay.Value =dtpNam.Value= globalVariables.SysDate;
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.Load+=new EventHandler(frm_baocao_soluong_thuoc_trongkho_Load);
          
            this.KeyDown+=new KeyEventHandler(frm_baocao_soluong_thuoc_trongkho_KeyDown);
            optThang.CheckedChanged += _CheckedChanged;
            optNam.CheckedChanged += _CheckedChanged;
            gridEXExporter1.GridEX = grd_tonkho_nhathuoc;
            CauHinh();
        }


        void _CheckedChanged(object sender, EventArgs e)
        {
           

        }

       
        void InitData()
        {
                GetKieuThuocVT();
                BindThuocVT();
        }
        void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }
        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc = new Select().From(DmucLoaithuoc.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbo_loaithuoc, dtLoaithuoc, DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc, "-----Chọn-----", true);
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLayDanhmucThuocKhoTheoTrangthai(lstStockID,100).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_thuoc, _dataThuoc, DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.TenThuoc,"-----Chọn-----",true);
            }
            catch
            {
            }
        }
        void GetKieuThuocVT()
        {
            KIEU_THUOC_VT = "THUOC";
          
        }
        private void CauHinh()
        {
            HisDuocProperties =PropertyLib._HisDuocProperties;
        }
        
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_dtDrugData = new DataTable();
        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_soluong_thuoc_trongkho_Load(object sender, EventArgs e)
        {
            InitData();
            DataTable dtkho = null;
            if (KIEU_THUOC_VT == "THUOC")
            {
                dtkho = KieuKho == "ALL" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA() : (KieuKho == "CHAN" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE());
            }
            else
            {
                dtkho = KieuKho == "ALL"
                    ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA()
                    : (KieuKho == "CHAN"
                        ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN()
                        : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> {"TATCA", "NGOAITRU", "NOITRU"}));
            }
            DataBinding.BindDataCombobox(cbo_kho, dtkho, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho, "-----Chọn-----", true);
            
            allowChanged = true;
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
        }
        
        /// <summary>
        /// hàm thực hiện in phiếu báo cáo 
        /// thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_soluong_thuoc_trongkho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
        }
        string report_code = "thuoc_baocao_soluong_thuoc_trongkho";
        
        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            allowChanged = false;
            cbo_NgayKhac.SelectedIndex = -1;
            cboQuy.SelectedIndex = -1;
            allowChanged = true;
            if (optThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtp_TuNgay.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtp_DenNgay.Value = endOfMonth;
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            allowChanged = false;
            cboThang.SelectedIndex = -1;
            cbo_NgayKhac.SelectedIndex = -1;
            allowChanged = true;
            var fromdate = new DateTime();
            var todate = new DateTime();
            switch (Utility.sDbnull(cboQuy.SelectedValue))
            {
                case "1":
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                    dtp_TuNgay.Value = fromdate;

                    todate = new DateTime(dtpNam.Value.Year, 3, 31);
                    dtp_DenNgay.Value = todate;
                    break;
                case "2":
                    fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                    dtp_TuNgay.Value = fromdate;

                    todate = new DateTime(dtpNam.Value.Year, 6, 30);
                    dtp_DenNgay.Value = todate;
                    break;
                case "3":
                    fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                    dtp_TuNgay.Value = fromdate;

                    todate = new DateTime(dtpNam.Value.Year, 9, 30);
                    dtp_DenNgay.Value = todate;
                    break;
                case "4":
                    fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                    dtp_TuNgay.Value = fromdate;

                    todate = new DateTime(dtpNam.Value.Year, 12, 31);
                    dtp_DenNgay.Value = todate;
                    break;
                default:
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                    dtp_TuNgay.Value = fromdate;

                    todate = new DateTime(dtpNam.Value.Year, 12, 31);
                    dtp_DenNgay.Value = todate;
                    break;
            }

        }
        private void optNam_CheckedChanged(object sender, EventArgs e)
        {
            if(optNam.Checked)
            {
                var myDate = dtpNam.Value;
                //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtp_TuNgay.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtp_DenNgay.Value = endOfMonth;
            }
        }
        DataSet dsData = new DataSet();//Chứa bảng tổng hợp và chi tiết
        private void cmd_TimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tu_ngay = dtp_TuNgay.Value.Date;
                DateTime den_ngay = dtp_DenNgay.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                dsData = SPs.ThuocBaoCaoTonKhoNhaThuoc(tu_ngay, den_ngay, Utility.Int32Dbnull(cbo_kho.SelectedValue)
                    , Utility.sDbnull(cbo_loaithuoc.SelectedValue), Utility.Int32Dbnull(cbo_thuoc.SelectedValue), Utility.ByteDbnull(cboReportType.SelectedValue)).GetDataSet();
                Utility.SetDataSourceForDataGridEx(grd_tonkho_nhathuoc, dsData.Tables[0], true, true, "1=1", "ten_thuoc");
            }
            catch (Exception ex)
            {

               
            }
        }

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dsData == null || dsData.Tables.Count <= 0) cmd_TimKiem.PerformClick();
            //    if (Utility.sDbnull(cboReportType.SelectedValue) == "0")
            //    {
            //        report_code = "thuoc_baocao_capphatthuoc_taiquay_tonghop";
            //        Utility.SetDataSourceForDataGridEx(grd_tonkho_nhathuoc, dsData.Tables[0], true, true, "1=1", "ten_thuoc");

            //        grd_tonkho_nhathuoc.BringToFront();
            //    }
            //    else
            //    {
            //        Utility.SetDataSourceForDataGridEx(grd_chitiet, dsData.Tables[1], true, true, "1=1", "ten_khoaphong,ten_benhnhan ");
            //        report_code = "thuoc_baocao_capphatthuoc_taiquay_chitiet";
            //        grd_chitiet.BringToFront();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Utility.CatchException(ex);
            //}
               
        }

        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsData == null || dsData.Tables.Count <= 0) cmd_TimKiem.PerformClick();
                string FromDateToDate = Utility.FromToDateTime(dtp_TuNgay.Text, dtp_DenNgay.Text);
               
                THU_VIEN_CHUNG.CreateXML(dsData.Tables[0], "thuoc_baocao_tonkho_nhathuoc.xml");

                
                if (dsData.Tables[0].Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                thuoc_baocao.Thuoc_InBaoCao(dsData.Tables[0], report_code, dtNgayIn.Value, FromDateToDate);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void cbo_NgayKhac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            allowChanged = false;
            cboThang.SelectedIndex = -1;
            cboQuy.SelectedIndex = -1;
            allowChanged = true;
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MaxValue;
            DateTime today = DateTime.Today;
            switch (cbo_NgayKhac.SelectedValue.ToString())
            {
                case "1":
                    fromDate = today;
                    toDate = today;
                    break;
                case "2"://Hôm qua
                    fromDate = today.AddDays(-1);
                    toDate = today.AddDays(-1);
                    break;

                case "3"://"Tuần này":
                    // Tuần tính từ thứ Hai đến Chủ nhật
                    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                    fromDate = today.AddDays(-diff);
                    toDate = fromDate.AddDays(6);
                    break;

                case "4"://"Tuần trước":
                    int diff2 = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                    DateTime firstDayThisWeek = today.AddDays(-diff2);
                    fromDate = firstDayThisWeek.AddDays(-7);
                    toDate = firstDayThisWeek.AddDays(-1);
                    break;

                case "5"://"Tháng này":
                    fromDate = new DateTime(today.Year, today.Month, 1);
                    toDate = fromDate.AddMonths(1).AddDays(-1);
                    break;

                case "6"://"Tháng trước":
                    DateTime firstDayThisMonth = new DateTime(today.Year, today.Month, 1);
                    fromDate = firstDayThisMonth.AddMonths(-1);
                    toDate = firstDayThisMonth.AddDays(-1);
                    break;
            }
            dtp_TuNgay.Value = fromDate;
            dtp_DenNgay.Value = toDate;
        }
    }
}
