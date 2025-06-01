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
using VMS.HIS.DAL;

using Microsoft.VisualBasic;
using SubSonic;
using VNS.HIS.BusRule.Classes;


namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_tonthuoc_trongkho : Form
    {
        string KIEU_THUOC_VT = "THUOC";
        private AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private int _drugID = new int();
        private string chuoiIDKhoThuoc = "-1";
        private DataTable dt_KhoThuoc = new DataTable();
        string lstStockID = "-1";
        bool allowChanged = false;
        public frm_baocao_tonthuoc_trongkho(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            chkInbienbankiemke.CheckedChanged += new EventHandler(chkInbienbankiemke_CheckedChanged);
            cboKhoThuoc.CheckedValuesChanged += cboKhoThuoc_CheckedValuesChanged;
        }

        void cboKhoThuoc_CheckedValuesChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            if (cboKhoThuoc.CheckedItems == null || cboKhoThuoc.CheckedItems.Count() <= 0)
                lstStockID = "-1";
            else
            {
                var query = (from chk in cboKhoThuoc.CheckedValues.AsEnumerable()
                             let x = Utility.sDbnull(chk)
                             select x).ToArray();
                if (query != null && query.Count() > 0)
                {
                    lstStockID = string.Join(",", query);
                }
            }
            SelectStock();
        }
        void SelectStock()
        {
            GetKieuThuocVT();
            BindThuocVT();
        }
        void GetKieuThuocVT()
        {
            //KIEU_THUOC_VT = "THUOC";
            //modifyTieude();
        }
        void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }
        void chkInbienbankiemke_CheckedChanged(object sender, EventArgs e)
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                if (!chkInbienbankiemke.Checked)
                    baocaO_TIEUDE1.Init("thuoc_baocaothuocton_theokho");
                else
                    baocaO_TIEUDE1.Init("thuoc_bienban_kiemkethuoc");
            }
            else
            {
                if (!chkInbienbankiemke.Checked)
                    baocaO_TIEUDE1.Init("vt_baocaovtton_theokho");
                else
                    baocaO_TIEUDE1.Init("vt_bienban_kiemkevt");
            }
        }

        private void frm_baocao_tonthuoc_trongkho_Load(object sender, EventArgs e)
        {
            if (KIEU_THUOC_VT .Contains("THUOC"))
            {
                dt_KhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA();
                baocaO_TIEUDE1.Init("thuoc_baocaothuocton_theokho");
            }
            else
            {
                dt_KhoThuoc = CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA();
                baocaO_TIEUDE1.Init("vt_baocaovtton_theokho");
            }
            
            cboKhoThuoc.DropDownDataSource = dt_KhoThuoc;
            cboKhoThuoc.DropDownDataMember = TDmucKho.Columns.IdKho;
            cboKhoThuoc.DropDownDisplayMember = TDmucKho.Columns.TenKho;
            cboKhoThuoc.DropDownValueMember = TDmucKho.Columns.IdKho;
            allowChanged = true;
            cboKhoThuoc_CheckedValuesChanged(cboKhoThuoc, e);
            //AutocompleteLoaithuoc();
            //AutocompleteThuoc();
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
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(cboKhoThuoc.Text))
                {
                    var query = (from chk in cboKhoThuoc.CheckedValues.AsEnumerable()
                                 let x = Utility.sDbnull(chk)
                                 select x).ToArray();
                    if (query.Any())
                    {
                        chuoiIDKhoThuoc = string.Join(",", query);
                    }
                }
                //Lấy giá trị xác nhận hết hạn hay chưa
                Int16 ishethan;
                if (radTatCa.Checked)
                {
                    ishethan = -1;
                }
                else if (radChuaHetHan.Checked)
                {
                    ishethan = 0;
                }
                else if (radDaHetHan.Checked)
                {
                    ishethan = 1;
                }
                else
                {
                    ishethan = -1;
                }
                //Truyền dữ liệu vào datatable
                DataTable m_dtReport = BAOCAO_THUOC.ThuocBaocaoSoluongtonthuoctheokho(chuoiIDKhoThuoc, Utility.Int32Dbnull(txtDrugID.Text, -1), Utility.Int32Dbnull(txtLoaithuoc.MyID, -1), ishethan, KIEU_THUOC_VT);
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocaothuocton_theokho.xml");
                //Truyền dữ liệu vào datagrid-view
                Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                //Add stt vào datatable
                Utility.AddColumToDataTable(ref m_dtReport, "STT", typeof(Int32));
                int idx = 1;
                foreach (DataRow drv in m_dtReport.Rows)
                {
                    drv["STT"] = idx;
                    idx++;
                }
                m_dtReport.AcceptChanges();
                //Add logo vào datatable
                Utility.UpdateLogotoDatatable(ref m_dtReport);

               

                //Lấy chuỗi condition truyền vào biến ?FromDateToDate trên crpt
                string Condition = string.Format("Thuộc kho :{0} - Thuốc: {1}", string.IsNullOrEmpty(cboKhoThuoc.Text) ? "Tất cả" : cboKhoThuoc.Text,
                                                string.IsNullOrEmpty(txtthuoc.Text) ? "Tất cả" : txtthuoc.Text);
              
                //Lấy tên người tạo báo cáo và gọi crpt
                string StaffName = globalVariables.gv_strTenNhanvien;
                string tieude = "", reportname = "";
                string tenbaocao=chkInbienbankiemke.Checked?"vt_bienban_kiemkevt":"vt_baocaovtton_theokho";
                if (KIEU_THUOC_VT == "THUOC")
                    tenbaocao = chkInbienbankiemke.Checked ? "thuoc_bienban_kiemkethuoc" : "thuoc_baocaothuocton_theokho";
                var crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
                if (crpt == null) return;

                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(m_dtReport);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt,"ReportCondition", Condition);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"ReportTitle", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt,"DayOfWarning", nmrSongay.Value);
                Utility.SetParameterValue(crpt,"ngay_in", dtNgayInPhieu.Value.ToString("dd/MM/yyyy"));
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
            }

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception exception)
            {
            }
        }

        private void frm_baocao_tonthuoc_trongkho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if ((e.Control && e.KeyCode == Keys.P) || e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if ((e.Control && e.KeyCode == Keys.E) || e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
        }
    }
}
