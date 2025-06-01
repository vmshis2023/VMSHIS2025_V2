using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Libs;
using Microsoft.VisualBasic;
using VMS.HIS.DAL;

namespace VNS.HIS.UI.Classess
{
    public class Baocao
    {
        public static void InPhieu(DataTable m_dtReport, DateTime ngayin, string dieukientimkiem, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "dieukientimkiem", dieukientimkiem);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
       
    }
    public class noitru_inphieu
    {
        public static void InPhieuCongkhai(DataTable m_dtReport, DateTime ngayin, string dieukientimkiem, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "dieukientimkiem", dieukientimkiem);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void InPhieu(DataTable m_dtReport, DateTime ngayin,string dieukientimkiem, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "dieukientimkiem", dieukientimkiem);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void InPhieu(DataTable m_dtReport, DateTime ngayin,string nguoi_thuchien, string dieukientimkiem, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                objForm.NGAY = ngayin;
                objForm.nguoi_thuchien = Utility.sDbnull(nguoi_thuchien);
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "dieukientimkiem", dieukientimkiem);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void InBienbanHoichan(DataTable m_dtReport, DateTime ngayin, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void Inphieusoket15ngay(DataTable m_dtReport, DateTime ngayin, bool isView, string report_code)
        {

            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);

            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void InPhieuChungNhanPTTT(DataTable m_dtReport, DateTime ngayin, bool isView,string report_code)
        {

             var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";

            reportDocument = Utility.GetReport(report_code, ref tieude, ref reportname);
                    
            if (reportDocument == null) return;

            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count > 0);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = report_code;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (isView)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(),0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                m_dtReport.Dispose();
                crpt.Close();
                crpt.Dispose();
                objForm.Dispose();
                GC.Collect();
            }
        }
        public static void InPhieutheodoi(DataTable m_dtReport, bool printPreview, string reportCode, string khogiay)
        {
            ReportDocument crpt = new ReportDocument();
            try
            {
                Utility.UpdateLogotoDatatable(ref m_dtReport);
                string tieude = "", reportname = "";
                crpt = Utility.GetReport(reportCode, khogiay, ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(m_dtReport.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (printPreview)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(),0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
              if(m_dtReport!=null)  m_dtReport.Dispose();
                if (crpt != null)
                {
                    crpt.Close();
                    crpt.Dispose();
                }
                GC.Collect();
            }
        }
        public static void BA_noitru_Into1(DataTable dt_MainData, DataTable dt_SubData, bool printPreview, string reportCode, string khogiay)
        {
            ReportDocument crpt = new ReportDocument();
            try
            {
                Utility.UpdateLogotoDatatable(ref dt_MainData);
                string tieude = "", reportname = "";
                crpt = Utility.GetReport(reportCode, khogiay, ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(dt_MainData.DefaultView);
                crpt.Subreports[0].SetDataSource(dt_SubData);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (printPreview)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                crpt.Close();
                crpt.Dispose();
                GC.Collect();
            }
        }
        public static void BA_noitru_Into234_voba_tkba(DataTable dt_MainData,bool printPreview, string reportCode, string khogiay)
        {
            ReportDocument crpt = new ReportDocument();
            try
            {
                Utility.UpdateLogotoDatatable(ref dt_MainData);
                string tieude = "", reportname = "";
                crpt = Utility.GetReport(reportCode, khogiay, ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview("", crpt, true, true);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(dt_MainData.DefaultView);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (printPreview)
                {
                    objForm.SetDefaultPrinter(Utility.GetDefaultPrinter(), 0);
                    objForm.ShowDialog();

                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                crpt.Close();
                crpt.Dispose();
                GC.Collect();
            }
        }
        public static void InPhieuChungNhanTT(DataTable m_dtReport, DateTime ngayin, bool isView)
        {

            //string path = "";
            //string tieude = "";
            //string reportcode = "";
            //ReportDocument crpt = new ReportDocument();

            //reportcode = ReportCode.INPHIEU_CHUNGNHAN_TT;
            //path = Utility.sDbnull(SystemReports.GetPathReport(reportcode));
            //tieude = SystemReports.TieuDeBaoCao(reportcode);

            //if (File.Exists(path))
            //{
            //    crpt.Load(path);

            //}
            //else
            //{
            //    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", path), "Thông báo không tìm thấy File",
            //                    MessageBoxIcon.Warning);
            //    return;
            //}

            //var objForm = new frmPrintPreview("PHIẾU CHỨNG NHẬN THỦ THUẬT", crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            //try
            //{
            //    crpt.SetDataSource(m_dtReport);
            //    objForm.crptTrinhKyName = Path.GetFileName(path);
            //    objForm.crptViewer.ReportSource = crpt;
            //    if (SystemReports.IsThamKemTheo(reportcode))
            //    {
            //        SystemReports.LoadThamSoBaoCao(reportcode, crpt, ngayin, m_dtReport);
            //    }
            //    else
            //    {
            //        crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            //        crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            //        crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            //        crpt.SetParameterValue("Address", globalVariables.Branch_Address);
            //        crpt.SetParameterValue("Email", globalVariables.Branch_Email);
            //        crpt.SetParameterValue("Fax", globalVariables.Branch_Fax);
            //        crpt.SetParameterValue("Phone", globalVariables.Branch_Phone);
            //        crpt.SetParameterValue("Hotline", globalVariables.Branch_Hotline);
            //        crpt.SetParameterValue("Wedsite", globalVariables.Branch_Website);
            //        crpt.SetParameterValue("CurrentDate", Utility.FormatDateTimeWithThanhPho(ngayin.Date));
            //        crpt.SetParameterValue("BottomCondition", BusinessHelper.BottomCondition());
            //        crpt.SetParameterValue("sTitleReport", tieude);

            //    }
            //    if (isView)
            //    {
            //        objForm.addTrinhKy_OnFormLoad();
            //        objForm.ShowDialog();
            //    }
            //    else
            //    {
            //        objForm.addTrinhKy_OnFormLoad();
            //        crpt.PrintOptions.PrinterName = Utility.GetDefaultPrinter();
            //        crpt.PrintToPrinter(1, true, 0, 0);
            //    }

            //}
            //catch (Exception exception)
            //{

            //    Utility.ShowMsg("Lỗi:" + exception.Message);
            //}
            //finally
            //{
            //    m_dtReport.Dispose();
            //    crpt.Close();
            //    crpt.Dispose();
            //    objForm.Dispose();
            //    GC.Collect();
            //}
        }
    }
}
