using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_BHYT_baocao_dongchitra : Form
    {
        private string Args = "ALL";

        private string reportname = "";
        private string tieude = "";

        public frm_BHYT_baocao_dongchitra()
        {
         //   Args = sthamso;
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }

        private void frm_BHYT_baocao_dongchitra_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);

                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                txtNhanvien.Init("-1");
                if (query.Any())
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                dtFromDate.Value = dtToDate.Value = dtNgayInPhieu.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
            int trangthai = -1;
        
            DataTable dtDanhsach =
                SPs.BhytBaocaoDongchitra(dtFromDate.Value, dtToDate.Value,
                                                          Utility.sDbnull(cboKhoa.SelectedValue, "KKB"),globalVariables.gv_intIDNhanvien,trangthai)
                    .GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdResult,dtDanhsach,false,false,"","");
          
            THU_VIEN_CHUNG.CreateXML(dtDanhsach, "BHYT_baocao_dongchitra.XML");
            Utility.UpdateLogotoDatatable(ref dtDanhsach);
            string reportCode = "BHYT_baocao_dongchitra";
           
            string Condition =
                string.Format("Từ ngày {0} đến {1} - Khoa KCB :{2}",
                              dtFromDate.Text, dtToDate.Text,globalVariables.MA_KHOA_THIEN);
            ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, dtDanhsach.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtDanhsach);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
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

        private void radTatca_CheckedChanged(object sender, EventArgs e)
        {
            //   if (radTatca.Checked) trangthai = -1;
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelUtlity.ExportGridEx(grdResult);
                //saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                //saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    string sPath = saveFileDialog1.FileName;
                //    var fs = new FileStream(sPath, FileMode.Create);
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
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }
    }
}