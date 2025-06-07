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
    public partial class frm_baocao_nhapxuaton_theoquy : Form
    {
        private HisDuocProperties HisDuocProperties;
        string KIEU_THUOC_VT = "THUOC";
          string lstStockID = "-1";
                
        //TDmucKho _item = null;
        bool allowChanged = false;
        string KieuKho = "";
        public frm_baocao_nhapxuaton_theoquy(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KieuKho = args.Split('-')[0];
            this.KIEU_THUOC_VT = args.Split('-')[1];
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value =dtpNam.Value= globalVariables.SysDate;
         
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.Load+=new EventHandler(frm_baocao_nhapxuaton_theoquy_Load);
            cmdBaoCao.Click+=new EventHandler(cmdBaoCao_Click);
            this.KeyDown+=new KeyEventHandler(frm_baocao_nhapxuaton_theoquy_KeyDown);
            cboKho.CheckedValuesChanged += cboKho_CheckedValuesChanged;
            chkTheoNhomThuoc.CheckedChanged += new EventHandler(chkTheoNhomThuoc_CheckedChanged);
            optThang.CheckedChanged += _CheckedChanged;
            optQuy.CheckedChanged += _CheckedChanged;
            optNam.CheckedChanged += _CheckedChanged;
            cboReportType.SelectedIndexChanged += cboReportType_SelectedIndexChanged;
            gridEXExporter1.GridEX = grdList;
            CauHinh();
        }

        void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kieubaocao = cboReportType.SelectedValue.ToString();
            string reportcode = "thuoc_baocao_nhapxuatton_theoquy";
            if (chkTheoNhomThuoc.Checked)
            {
                if (kieubaocao == "0")
                    reportcode = "thuoc_baocao_nhapxuatton_theoquy_theonhom";
                else if (kieubaocao == "1")
                    reportcode = "thuoc_baocao_nhap_theoquy_theonhom";
                else
                    reportcode = "thuoc_baocao_xuat_theoquy_theonhom";

            }
            else
            {
                if (kieubaocao == "0")
                    reportcode = "thuoc_baocao_nhapxuatton_theoquy";
                else if (kieubaocao == "1")
                    reportcode = "thuoc_baocao_nhap_theoquy_theonhom";
                else
                    reportcode = "thuoc_baocao_xuat_theoquy_theonhom";
            }
            baocaO_TIEUDE1.Init(reportcode);
        }

        void cboKho_CheckedValuesChanged(object sender, EventArgs e)
        {

            if (!allowChanged) return;
            if (cboKho.CheckedItems == null || cboKho.CheckedItems.Count() <= 0)
                lstStockID = "-1";
            else
            {
                var query = (from chk in cboKho.CheckedValues.AsEnumerable()
                             let x = Utility.sDbnull(chk)
                             select x).ToArray();
                if (query != null && query.Count() > 0)
                {
                    lstStockID = string.Join(",", query);
                }
            }
            SelectStock();
        }

        void _CheckedChanged(object sender, EventArgs e)
        {
           

        }

        void modifyTieude()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                if (chkTheoNhomThuoc.Checked)
                {

                    baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_theoquy_theonhom");
                    grdList.BringToFront();

                }
                else
                {

                    baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_theoquy");
                    grdList.BringToFront();

                }
                if(chkBangCanDoi.Checked)
                {
                    baocaO_TIEUDE1.Init("thuoc_baocao_bangcandoi");
                    grdbangcandoi.BringToFront();
                }
            }
            else
            {
                if (chkTheoNhomThuoc.Checked)
                {
                    baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_theoquy_theonhom");
                    grdList.BringToFront();

                }
                else
                {

                    baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_theoquy");
                    grdList.BringToFront();

                }
                if (chkBangCanDoi.Checked)
                {
                    baocaO_TIEUDE1.Init("thuoc_baocao_bangcandoi");
                    grdbangcandoi.BringToFront();
                }
            }
        }
        void chkTheoNhomThuoc_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

       
        void txtLoaithuoc__OnEnterMe()
        {

        }
        void txtLoaithuoc__OnSelectionChanged()
        {

        }
       
        void SelectStock()
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
            DataTable dtLoaithuoc = SPs.ThuocLayDanhmucLoaiThuocTheoDanhmucKho(lstStockID).GetDataSet().Tables[0];
            txtLoaithuoc.Init(dtLoaithuoc, new List<string>() { DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.MaLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc });
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLayDanhmucThuocKhoTheoTrangthai(lstStockID,Convert.ToByte( chkHieuluc.Checked?1:100)).GetDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        void GetKieuThuocVT()
        {
            KIEU_THUOC_VT = "THUOC";
            modifyTieude();
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
        private void frm_baocao_nhapxuaton_theoquy_Load(object sender, EventArgs e)
        {
            DataTable dtkho = null;
            if (KIEU_THUOC_VT == "THUOC")
            {
                baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_theoquy");
                dtkho = KieuKho == "ALL" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA() : (KieuKho == "CHAN" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE());
            }
            else
            {
                baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_theoquy");
                dtkho = KieuKho == "ALL"
                    ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA()
                    : (KieuKho == "CHAN"
                        ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN()
                        : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> {"TATCA", "NGOAITRU", "NOITRU"}));
            }
            cboKho.DropDownDataSource = dtkho;
            cboKho.DropDownDataMember = TDmucKho.Columns.IdKho;
            cboKho.DropDownDisplayMember = TDmucKho.Columns.TenKho;
            cboKho.DropDownValueMember = TDmucKho.Columns.IdKho;

            //DataBinding.BindData(cboKho, dtkho, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.Columns.KieuThuocvattu).IsEqualTo(KIEU_THUOC_VT)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
            cboKho_CheckedValuesChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
        }
        
        /// <summary>
        /// hàm thực hiện in phiếu báo cáo 
        /// thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string nhomthuoc = "-1";

                nhomthuoc = txtLoaithuoc.MyID.ToString();
                string fromdate="01/01/2000";
                string todate="01/01/2000";
                string _value = "1";
                string _tondau = "Tồn đầu";
                string _toncuoi = "Tồn cuối";
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                if (optThang.Checked)
                {
                    if (cboThang.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Tháng báo cáo");
                        cboThang.Focus();
                        return;
                    }
                    _value = cboThang.SelectedValue.ToString();
                    _tondau = "Tồn đầu tháng " + _value;
                    _toncuoi = "Tồn cuối tháng " + _value;
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
                    _tondau = "Tồn đầu quý " + _value;
                    _toncuoi = "Tồn cuối quý " + _value;
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
                    _tondau = "Tồn " + dtpNam.Value.AddYears(-1).Year.ToString();
                    _toncuoi = "Tồn " + dtpNam.Value.Year.ToString(); 
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                    todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                }
                else
                {
                    _tondau = "Tồn đầu " + dtFromDate.Value.ToString("dd/MM/yyyy");
                    _toncuoi = "Tồn cuối " + dtToDate.Value.ToString("dd/MM/yyyy");
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                }
                DataTable m_dtReport = null;
              if(radCandoikho.Checked)
              {
                  m_dtReport = BAOCAO_THUOC.ThuocBaocaonhapxuattontheokho(fromdate,
                                   todate,
                                  lstStockID, nhomthuoc, Utility.Int32Dbnull(txtthuoc.MyID, -1), chkBiendong.Checked ? 1 : 0);
                  Utility.SetDataSourceForDataGridEx(grdbangcandoi, m_dtReport, true, true, "1=1", "");
                  THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocao_bangcandoi.xml");
                  if (m_dtReport.Rows.Count <= 0)
                  {
                      Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                      return;
                  }
                  thuoc_baocao.Baocao_thuoc_bangcandoi(m_dtReport, cboReportType.SelectedValue.ToString(), KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE, _tondau, _toncuoi,
                                                                                      dtNgayIn.Value, FromDateToDate,
                                                                                      Utility.sDbnull(cboKho.Text), chkTheoNhomThuoc.Checked);
              }

              if(radXuatnhapton.Checked)
              {
                    
                        m_dtReport = BAOCAO_THUOC.ThuocBaocaonhapxuatton(fromdate,
                                          todate,
                                         lstStockID, nhomthuoc, Utility.Int32Dbnull(txtthuoc.MyID, -1), chkBiendong.Checked ? 1 : 0,Utility.Bool2byte(chkBaocaotheolohandung.Checked));


                  Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
                  THU_VIEN_CHUNG.CreateXML(m_dtReport, "baocao_xuatnhapton_theoquy.xml");
                  if (m_dtReport.Rows.Count <= 0)
                  {
                      Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                      return;
                  }
                 
                  thuoc_baocao.BaocaoNhapxuattonTheoquy(m_dtReport, cboReportType.SelectedValue.ToString(), KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE, _tondau, _toncuoi,
                                                                                        dtNgayIn.Value, FromDateToDate,
                                                                                        Utility.sDbnull(cboKho.Text), chkTheoNhomThuoc.Checked, Utility.Bool2byte(chkBaocaotheolohandung.Checked));
              }
               if(radBienbankiemke.Checked)
               {
                   m_dtReport = BAOCAO_THUOC.ThuocBaocaonhapxuatton(fromdate,
                                   todate,
                                  lstStockID, nhomthuoc, Utility.Int32Dbnull(txtthuoc.MyID, -1), chkBiendong.Checked ? 1 : 0,0);


                   Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
                   THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_bienban_kiemkethuoc.xml");
                   if (m_dtReport.Rows.Count <= 0)
                   {
                       Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                       return;
                   }
                   string Condition = string.Format("Thuộc kho :{0} - Thuốc: {1}", string.IsNullOrEmpty(cboKho.Text) ? "Tất cả" : cboKho.Text,
                                           string.IsNullOrEmpty(txtthuoc.Text) ? "Tất cả" : txtthuoc.Text);

                   thuoc_baocao.Bienbankiemkethuoc(m_dtReport, KIEU_THUOC_VT,
                                                   baocaO_TIEUDE1.TIEUDE, dtNgayIn.Value, Condition, cboKho.Text, 30);
               }
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapxuaton_theoquy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void cmdBaoCao_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {

                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                    return;
                }
                gridEXExporter1.GridEX = grdList;

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
            catch (Exception exception)
            {

            }
        }

        private void frm_baocao_nhapxuaton_theoquy_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetDataDrug_Click(object sender, EventArgs e)
        {
           
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (optThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
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
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        dtToDate.Value = todate;
                        break;
                    case "2":
                         fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        dtToDate.Value = todate;
                        break;
                    case "3":
                          fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        dtToDate.Value = todate;
                        break;
                    case "4":
                           fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        dtToDate.Value = todate;
                        break;
                    default:
                         fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        dtToDate.Value = todate;
                        break;
                }
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
                dtFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtToDate.Value = endOfMonth;
            }
        }

        private void chkBangCanDoi_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        private void grdbangcandoi_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void dtNgayIn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmdComboDown_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
        }
    }
}
