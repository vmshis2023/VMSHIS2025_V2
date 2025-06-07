using System;
using System.Data;
using System.IO;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using VMS.Emr;
using VMS.HIS.Bus;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Baocao
{
    public class noitru_baocao
    {
        public static void Inphieunhapvien(NoitruPhieunhapvien objPNV, DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            SysReport objReport = null;
            string tieude = "", reportname = "";
            string reportCode = "noitru_phieunhapvien";
            ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname,ref objReport);
            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_phieunhapvien.xml");
            if (crpt == null || objReport == null) return;
            EmrDocuments emrdoc = new EmrDocuments();
            emrdoc.InitDocument(objPNV.IdBenhnhan, objPNV.MaLuotkham, Utility.Int64Dbnull(objPNV.IdPhieu), objPNV.NgayNhapvien.Value, Loaiphieu_HIS.PHIEUNHAPVIEN, reportCode, objPNV.NguoiTao, -1,-1, Utility.Byte2Bool(0), "");
            emrdoc.Save();
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            NoitruPhieunhapvien pnv = NoitruPhieunhapvien.FetchByID(Utility.Int64Dbnull(mDtReport.Rows[0]["id_phieunv"], -1));
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);
                objForm.nguoi_thuchien = Utility.sDbnull(mDtReport.Rows[0]["ten_bacsichidinh"], "");
                objForm.NGAY = pnv != null ? pnv.NgayNhapvien.Value : ngayIn;
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieunhapvien";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "txtTrinhky", "");
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        public static void InBanCamKetPhauThuat(DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            string tieude = "", reportname = "";
            SysReport objReport = null;
            string reportCode = "noitru_giaycamketPT_A5";
            ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname, ref objReport);

            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_giaycamketPT_A5.xml");
            if (crpt == null || objReport == null) return;
            //EmrDocuments emrdoc = new EmrDocuments();
            //emrdoc.InitDocument(objPNV.IdBenhnhan, objPNV.MaLuotkham, Utility.Int64Dbnull(objPNV.IdPhieu), objPNV.NgayNhapvien.Value, Loaiphieu_HIS.PHIEUCHIDINH, reportCode, objPNV.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
            //emrdoc.Save();
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_giaycamketPT_A5";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "txtTrinhky", "");
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private static string TinhTong(DataTable dataTable)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName)));
            return sumoftotal;
        }

        private static string TinhTong(DataTable dataTable, string colName)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(colName)));
            return sumoftotal;
        }
        public static void InTricbienbanhoichan(DataTable mDtReport, string sTitleReport, DateTime ngayIn)
        {
            string tieude = "", reportname = "";
            SysReport objReport = null;
            ReportDocument crpt = Utility.GetReport("noitru_trichbienbanhoichan_A4", ref tieude, ref reportname, ref objReport);
            THU_VIEN_CHUNG.CreateXML(mDtReport, "noitru_trichbienbanhoichan_A4.xml");
            if (crpt == null || objReport == null) return;
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            var moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                crpt.SetDataSource(mDtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_trichbienbanhoichan_A4";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "txtTrinhky", "");
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }
    }
}