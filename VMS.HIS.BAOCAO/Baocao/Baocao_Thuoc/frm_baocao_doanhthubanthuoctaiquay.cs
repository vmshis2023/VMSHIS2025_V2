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
    public partial class frm_baocao_doanhthubanthuoctaiquay : Form
    {
        private HisDuocProperties HisDuocProperties;
        string KIEU_THUOC_VT = "THUOC";
          string lstStockID = "-1";
                
        //TDmucKho _item = null;
        bool allowChanged = false;
        string KieuKho = "";
        public frm_baocao_doanhthubanthuoctaiquay(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KieuKho = args.Split('-')[0];
            this.KIEU_THUOC_VT = args.Split('-')[1];
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value =dtpNam.Value= globalVariables.SysDate;
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.Load+=new EventHandler(frm_baocao_doanhthubanthuoctaiquay_Load);
            this.KeyDown+=new KeyEventHandler(frm_baocao_doanhthubanthuoctaiquay_KeyDown);
            cboKho.CheckedValuesChanged += cboKho_CheckedValuesChanged;
            optThang.CheckedChanged += _CheckedChanged;
            optQuy.CheckedChanged += _CheckedChanged;
            optNam.CheckedChanged += _CheckedChanged;
            optChitiet.CheckedChanged += loaiBC_CheckedChanged;
            opttonghopTNV.CheckedChanged += loaiBC_CheckedChanged;
            gridEXExporter1.GridEX = grdList;
            CauHinh();
        }

        void loaiBC_CheckedChanged(object sender, EventArgs e)
        {

            string reportcode = "thuoc_baocao_banthuoctaiquay_chitiet";
           
            if (opttonghopTNV.Checked)
            {
                reportcode = "thuoc_baocao_banthuoctaiquay_TNV";
                grdListDetail.SendToBack();
            }
            else
            {
                reportcode = "thuoc_baocao_banthuoctaiquay_chitiet";
                grdListDetail.BringToFront();
            }
            baocaO_TIEUDE1.Init(reportcode);
        }

        void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            modifyTieude();
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
                DataTable _dataThuoc = SPs.ThuocLayDanhmucThuocTheoDanhmucKho(lstStockID).GetDataSet().Tables[0];
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
        private void frm_baocao_doanhthubanthuoctaiquay_Load(object sender, EventArgs e)
        {
            DataTable dtkho = null;
            modifyTieude();

            if (KIEU_THUOC_VT == "ALL")
                dtkho = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "ALL", "CHANLE,LE", 100, 100, 1,0);// CommonLoadDuoc.LAYTHONGTIN_KHOVTTH_LE_NGOAITRU();
            else if (KIEU_THUOC_VT == "THUOC")
                dtkho = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "THUOC,THUOCVT", "CHANLE,LE", 100, 100, 1, 0);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE_NGOAITRU();
            else
                dtkho = CommonLoadDuoc.LAYDANHMUCKHO(-1, "TATCA,NGOAITRU", "VT,THUOCVT", "CHANLE,LE", 100, 100, 1, 0);// CommonLoadDuoc.LAYTHONGTIN_KHOLE_NGOAITRU();
            cboKho.DropDownDataSource = dtkho;
            cboKho.DropDownDataMember = TDmucKho.Columns.IdKho;
            cboKho.DropDownDisplayMember = TDmucKho.Columns.TenKho;
            cboKho.DropDownValueMember = TDmucKho.Columns.IdKho;
            //Thu ngân viên
            DataBinding.BindDataCombobox(cboNhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                //Bác sĩ
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "---Thu ngân viên---", true);
            txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
            

            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.Columns.KieuThuocvattu).IsEqualTo(KIEU_THUOC_VT)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
            cboKho_CheckedValuesChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
            LoadPtttNganhang();

            cboNhanvien.SelectedValue = globalVariables.UserName;
            if (Utility.Coquyen("baocao_chonthunganvien"))
            {
                cboNhanvien.Enabled = true;
            }
            else
            {
                cboNhanvien.Enabled = false;
            }
        }

        DataTable dtPttt = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            DataBinding.BindDataCombobox(cboPttt, dtPttt,
                                     DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---PTTT---", true);
            DataBinding.BindDataCombobox(cboNganhang, dtNganhang,
                                    DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---Ngân hàng---", true);

            //cboPttt.DataSource = dtPttt;
            //cboPttt.ValueMember = DmucChung.Columns.Ma;
            //cboPttt.DisplayMember = DmucChung.Columns.Ten;
            //cboNganhang.DataSource = dtNganhang;
            //cboNganhang.ValueMember = DmucChung.Columns.Ma;
            //cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = "-1";
            cboNganhang.SelectedValue = "-1";
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_doanhthubanthuoctaiquay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
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

        private void frm_baocao_doanhthubanthuoctaiquay_KeyDown_1(object sender, KeyEventArgs e)
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
            //if(optThang.Checked)
            //{
            //    var myDate = cboThang.SelectedValue;
            //    //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
            //    // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
            //    var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
            //    dtFromDate.Value = startOfMonth;
            //    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            //    dtToDate.Value = endOfMonth;
            //}
        }

        private void cboQuy_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (optQuy.Checked)
            //{
            //    var fromdate = new DateTime();
            //    var todate = new DateTime();
            //    switch (Utility.sDbnull(cboQuy.SelectedValue))
            //    {
            //        case "1":
            //            fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
            //            dtFromDate.Value = fromdate;

            //            todate = new DateTime(dtpNam.Value.Year, 3, 31);
            //            dtToDate.Value = todate;
            //            break;
            //        case "2":
            //            fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
            //            dtFromDate.Value = fromdate;

            //            todate = new DateTime(dtpNam.Value.Year, 6, 30);
            //            dtToDate.Value = todate;
            //            break;
            //        case "3":
            //            fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
            //            dtFromDate.Value = fromdate;

            //            todate = new DateTime(dtpNam.Value.Year, 9, 30);
            //            dtToDate.Value = todate;
            //            break;
            //        case "4":
            //            fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
            //            dtFromDate.Value = fromdate;

            //            todate = new DateTime(dtpNam.Value.Year, 12, 31);
            //            dtToDate.Value = todate;
            //            break;
            //        default:
            //            fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
            //            dtFromDate.Value = fromdate;

            //            todate = new DateTime(dtpNam.Value.Year, 12, 31);
            //            dtToDate.Value = todate;
            //            break;
            //    }
            //}
        }

        private void dtpNam_ValueChanged(object sender, EventArgs e)
        {
            //if (optNam.Checked)
            //{
            //    var myDate = dtpNam.Value;
            //    //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
            //    // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
            //    var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
            //    dtFromDate.Value = startOfMonth;
            //    var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
            //    dtToDate.Value = endOfMonth;
            //}
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
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
                string fromdate = "01/01/2000";
                string todate = "01/01/2000";
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
                byte loaidonthuoc =Utility.ByteDbnull( optAll.Checked?100:(optBsi.Checked?0:1));
                byte loaibaocao = optChitiet.Checked ? (byte)0 : (byte)1;
                m_dtReport = BAOCAO_THUOC.BaocaoBanthuoctaiquayBvsg(fromdate, todate, "-1", Utility.sDbnull(cboNhanvien.SelectedValue, "-1"), Utility.sDbnull(cboPttt.SelectedValue, "-1"), Utility.sDbnull(cboNganhang.SelectedValue, "-1"),
                                       lstStockID, loaidonthuoc, Utility.Int32Dbnull(txtBacsi.MyID, -1), loaibaocao);

                if (optChitiet.Checked)
                {
                    Utility.SetDataSourceForDataGridEx_Basic(grdListDetail, m_dtReport, true, true, "1=1", "");
                    THU_VIEN_CHUNG.CreateXML(m_dtReport, "Baocao_Banthuoctaiquay_chitiet.xml");
                }
                else
                {
                    Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtReport, true, true, "1=1", "");
                    THU_VIEN_CHUNG.CreateXML(m_dtReport, "Baocao_Banthuoctaiquay.xml");
                }
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }


                //thuoc_baocao.BaocaoNhapxuatThuoc(m_dtReport, cboReportType.SelectedValue.ToString(), KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE, _tondau, _toncuoi,
                //                                                                      dtNgayIn.Value, FromDateToDate,
                //                                                                      Utility.sDbnull(cboKho.Text), chkTheoNhomThuoc.Checked);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
