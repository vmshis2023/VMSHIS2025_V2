using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using VMS.HIS.Bus;
using VMS.HIS.Bus.Emr;
using CrystalDecisions.Shared;
using System.Windows.Forms;
using Aspose.Words;
using System.Text;
using Aspose.Words.MailMerging;
using BarcodeLib;
using Aspose.Words.Drawing;

namespace VNS.HIS.UI.Classess
{
    public class KcbInphieu
    {
        public static void INPHIEU_KHAM(KcbDangkyKcb objCongkham, string maDoiTuong, DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            switch (maDoiTuong)
            {
                case "DV":
                    InPhieuKCB_DV(objCongkham,mDtReport, sTitleReport, khoGiay);
                    break;
                case "BHYT":
                    InPhieuKCB_BHYT(mDtReport, sTitleReport, khoGiay);
                    break;
                default:
                    InPhieuKCB_DV(objCongkham,mDtReport, sTitleReport, khoGiay);
                    break;
            }
        }
      
        public static void INPHIEU_HEN(DataTable mDtReport, string sTitleReport)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            SysReport objReport = null;
            string tieude = "", reportname = "";
            ReportDocument reportDocument = Utility.GetReport("thamkham_inphieuhen_benhnhan", ref tieude, ref reportname,ref objReport);
            if (reportDocument == null || objReport == null) return;
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            ReportDocument crpt = reportDocument;
            THU_VIEN_CHUNG.CreateXML(mDtReport, "phieuhen.XML");
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_inphieuhen_benhnhan";
                objForm.nguoi_thuchien = Utility.sDbnull(mDtReport.Rows[0]["ten_bacsi_ketthuckham"], "");
                crpt.SetDataSource(mDtReport);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "HotLine", globalVariables.Branch_Fax);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                //{
                //    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                //    objForm.ShowDialog();
                //}
                //else
                //{
                //    objForm.addTrinhKy_OnFormLoad();
                //    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                //    crpt.PrintToPrinter(1, false, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        public static void INMAU_CHUYENKHAM_CHUYENKHOA(DataTable mDtReport, string sTitleReport, string report,
                                                       string lydochuyen)
        {
            Utility.UpdateLogotoDatatable(ref mDtReport);
            string tieude = "", reportname = "";
            SysReport objReport = null;
            ReportDocument reportDocument = report == "PHIEUKHAM_CHUYENKHOA"
                ? Utility.GetReport("thamkham_phieukham_chuyenkhoa", ref tieude, ref reportname,ref objReport)
                : Utility.GetReport("thamkham_phieuxn_benhpham", ref tieude, ref reportname, ref objReport);
            if (reportDocument == null || objReport == null) return;
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            ReportDocument crpt = reportDocument;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkham_phieukham_chuyenkhoa";
                crpt.SetDataSource(mDtReport);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone + globalVariables.SOMAYLE);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "lydochuyen", lydochuyen);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                //if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                //{
                //    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                //    objForm.ShowDialog();
                //}
                //else
                //{
                //    objForm.addTrinhKy_OnFormLoad();
                //    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                //    crpt.PrintToPrinter(1, false, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static void InPhieuKCB_DV(KcbDangkyKcb objCongkham,DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";
            
            string reportcode = "tiepdon_PhieuKCB_Dvu_A4";
            SysReport objReport = null;
            switch (khoGiay)
            {
                case "A4":
                    reportcode = "tiepdon_PhieuKCB_Dvu_A4";
                    reportDocument = Utility.GetReport(reportcode, ref tieude, ref reportname,ref objReport);
                    break;
                case "A5":
                    reportcode = "tiepdon_PhieuKCB_Dvu_A5";
                    reportDocument = Utility.GetReport(reportcode, ref tieude, ref reportname, ref objReport);
                    break;
            }
            if (reportDocument == null || objReport==null) return;
            EmrDocuments emrdoc = new EmrDocuments();
            emrdoc.InitDocument(objCongkham.IdBenhnhan, objCongkham.MaLuotkham,Utility.Int64Dbnull( objCongkham.IdKham), objCongkham.NgayDangky.Value, Loaiphieu_HIS.PHIEUDANGKYKCB, reportcode, objCongkham.NguoiTao,Utility.Int16Dbnull( objCongkham.IdKhoakcb,-1), Utility.Int16Dbnull(objCongkham.IdPhongkham,-1),Utility.Byte2Bool( objCongkham.Noitru), "");
            emrdoc.Save();
            if (Utility.sDbnull( objReport.FileWord)!="" )
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }    
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview(tieude, crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                crpt.SetDataSource(mDtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                objForm.nguoi_thuchien =Utility.sDbnull( mDtReport.Rows[0]["ten_nguoitao"],"");
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
                
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB,
                                           PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC IN PHIẾU THÔNG TIN KHÁM BỆNH
        /// </summary>
        /// <param name="mDtReport"></param>
        /// <param name="sTitleReport"></param>
        /// <param name="khoGiay"></param>
        public static void InPhieuKCB_BHYT(DataTable mDtReport, string sTitleReport, string khoGiay)
        {
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "";
            SysReport objReport = null;
            switch (khoGiay)
            {
                case "A4":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A4", ref tieude, ref reportname, ref objReport);
                    break;
                case "A5":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A5", ref tieude, ref reportname, ref objReport);
                    break;
            }
            if (reportDocument == null || objReport == null) return;
            if (Utility.sDbnull(objReport.FileWord) != "")
            {
                WordPrinter.InPhieu(null,mDtReport, Utility.sDbnull(objReport.FileWord));
                return;
            }
            ReportDocument crpt = reportDocument;
            // VNS.HISLink.Report_LaoKhoa.Report_LaoKhoa.CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE crpt = new CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE();
            var objForm = new frmPrintPreview("", crpt, true, mDtReport.Rows.Count > 0);
            try
            {
                mDtReport.AcceptChanges();
                crpt.SetDataSource(mDtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                                             Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                                DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB,
                                           PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static string InTachToanBoPhieuCls(List<long> lstSelectedPrint,long idBenhnhan, string maLuotkham, long vAssignId,
                                                        string vAssignCode, List<string> listnhomincls,string selectednhomcls,
                                                        int selectedIndex, bool inTach, ref string mayin,bool returnPdf=false,string PdfFilePath="")
        {
            try
            {
                string nhominCLSTimkiem="ALL";
                mayin = "";
                if (selectedIndex > 0)//Không phải chọn in tách tất cả mà chỉ một phiếu cụ thể
                {
                    nhominCLSTimkiem = selectednhomcls;
                    listnhomincls = new List<string>() { selectednhomcls };
                }
                KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(vAssignId);
               
                DataTable dtAll =
                    new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhominCLSTimkiem,string.Join(",",lstSelectedPrint.ToArray())).Tables[0];
                
                foreach (string nhomcls in listnhomincls.ToList())
                {
                    
                    //   KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                     DataTable dt=dtAll.Clone();
                   
                    DataRow[] arrDR = dtAll.Select("nhom_in_cls = '" + nhomcls + "'");
                    if (arrDR.Length > 0)
                        dt = arrDR.CopyToDataTable();
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        //Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                        //return;
                    }
                    else
                    {
                           THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InTachToanBophieuCLS.XML");
                        Utility.UpdateLogotoDatatable(ref dt);
                        string vMachidinh = vAssignCode;
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", false) == "1")
                        {
                            vMachidinh = vAssignCode.Replace(".", "");
                        }
                        Utility.CreateBarcodeData(ref dt, vMachidinh);
                        string manhomcls = nhomcls;
                        string tieude = "";
                        string khoGiay = "A5";
                        if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                        //Khổ giấy lấy lại theo nhóm in phiếu CLS
                        
                        DmucChung _obj = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Ma).IsEqualTo(nhomcls).And(DmucChung.Columns.Loai).IsEqualTo("NHOM_INPHIEU_CLS").ExecuteSingle<DmucChung>();
                        if (_obj != null && (Utility.DoTrim(_obj.MotaThem).Contains("A4") || Utility.DoTrim(_obj.MotaThem).Contains("A5")))
                            khoGiay = Utility.DoTrim(_obj.MotaThem);
                        string reportname = "";
                        EmrDocuments emrdoc = new EmrDocuments();
                        emrdoc.InitDocument(idBenhnhan, maLuotkham, Utility.Int64Dbnull(vAssignId), objchidinh.NgayChidinh, Loaiphieu_HIS.PHIEUCHIDINH, manhomcls, objchidinh.NguoiTao, Utility.Int16Dbnull(objchidinh.IdKhoaChidinh, -1), Utility.Int16Dbnull(objchidinh.IdPhongChidinh, -1), Utility.Byte2Bool(objchidinh.Noitru), "",true,false,objchidinh.MaChidinh);
                        emrdoc.Save();
                      
                        ReportDocument crpt = Utility.GetReport(manhomcls, khoGiay, ref tieude, ref reportname);
                        decimal tong = Utility.DecimaltoDbnull(dt.Compute("sum(Bnhan_chitra)", "1=1"), 0);
                        if (crpt == null) return "";
                        try
                        {
                            var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                            {
                                mv_sReportFileName = Path.GetFileName(reportname),
                                mv_sReportCode = manhomcls
                            };
                            objForm.NGAY = objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh;
                            objForm.nguoi_thuchien = Utility.sDbnull(dt.Rows[0]["ten_bacsi_chidinh"], "");
                            crpt.SetDataSource(dt);
                            Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                            //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên        Bác sĩ chỉ định     ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                            Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
                            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone,globalVariables.Branch_Email));
                            Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    DateTime.Now));
                            if (!inTach && selectedIndex == 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                    dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                            "PHIẾU CHỈ ĐỊNH", true);
                            }
                            else
                            {
                                Utility.SetParameterValue(crpt, "TitleReport", tieude);
                            }
                            Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh));
                            Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh, globalVariables.gv_strDiadiem));
                            objForm.crptViewer.ReportSource = crpt;
                            if (returnPdf && PdfFilePath != "")
                            {
                                objForm.addTrinhKy_OnFormLoad();
                                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                               
                                crpt.ExportToDisk(ExportFormatType.PortableDocFormat, PdfFilePath);
                            }
                            else
                            {
                                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                    PropertyLib._MayInProperties.PreviewInCLS))
                                {
                                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                    objForm.ShowDialog();
                                    mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                }
                                else
                                {
                                    objForm.addTrinhKy_OnFormLoad();
                                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                    mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                    crpt.PrintToPrinter(1, false, 0, 0);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg("Lỗi:" + ex.Message);
                            // Utility.DefaultNow(this);
                        }
                        finally
                        {
                            Utility.FreeMemory(crpt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return "";
            }


            return "";

        }
        public static string InTachToanBoPhieuCls_Doc(List<long> lstSelectedPrint, long idBenhnhan, string maLuotkham, long vAssignId,
                                                       string vAssignCode, List<string> listnhomincls, string selectednhomcls,
                                                       int selectedIndex, bool inTach, ref string mayin, bool returnPdf = false, string PdfFilePath = "", bool KyDientu = false)
        {
            try
            {
                string nhominCLSTimkiem = "ALL";
                mayin = "";
                if (selectedIndex > 0)//Không phải chọn in tách tất cả mà chỉ một phiếu cụ thể
                {
                    nhominCLSTimkiem = selectednhomcls;
                    listnhomincls = new List<string>() { selectednhomcls };
                }
                KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(vAssignId);
                DataTable dtAll =
                    new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhominCLSTimkiem, string.Join(",", lstSelectedPrint.ToArray())).Tables[0];
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv", "tieude_phieu", "dia_diem", "ngay_chidinh_trinhky" };
                Utility.AddColums2DataTable(ref dtAll, lstMoreColumns, typeof(string));
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                SysSystemParameter sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                foreach (string nhomcls in listnhomincls.ToList())
                {

                    //   KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                    DataTable dt = dtAll.Clone();
                    dt.TableName = "PHIEUCHIDINH_TACH";
                   
                    DataRow[] arrDR = dtAll.Select("nhom_in_cls = '" + nhomcls + "'");
                    if (arrDR.Length > 0)
                        dt = arrDR.CopyToDataTable();
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        //Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                        return "";
                    }
                    else
                    {
                        THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InTachToanBophieuCLS.XML");
                        Utility.UpdateLogotoDatatable(ref dt);
                        string vMachidinh = vAssignCode;
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", false) == "1")
                        {
                            vMachidinh = vAssignCode.Replace(".", "");
                        }
                        Utility.CreateBarcodeData(ref dt, vMachidinh);
                        string manhomcls = nhomcls;
                        string tieude = "";
                        string khoGiay = "A5";
                        if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                        //Khổ giấy lấy lại theo nhóm in phiếu CLS

                        DmucChung _obj = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Ma).IsEqualTo(nhomcls).And(DmucChung.Columns.Loai).IsEqualTo("NHOM_INPHIEU_CLS").ExecuteSingle<DmucChung>();
                        if (_obj != null && (Utility.DoTrim(_obj.MotaThem).Contains("A4") || Utility.DoTrim(_obj.MotaThem).Contains("A5")))
                            khoGiay = Utility.DoTrim(_obj.MotaThem);
                        string reportname = "";
                        //EmrDocuments emrdoc = new EmrDocuments();
                        //emrdoc.InitDocument(idBenhnhan, maLuotkham, Utility.Int64Dbnull(vAssignId), objchidinh.NgayChidinh, Loaiphieu_HIS.PHIEUCHIDINH, manhomcls, objchidinh.NguoiTao, Utility.Int16Dbnull(objchidinh.IdKhoaChidinh, -1), Utility.Int16Dbnull(objchidinh.IdPhongChidinh, -1), Utility.Byte2Bool(objchidinh.Noitru), "", true,false,objchidinh.MaChidinh);
                        //emrdoc.Save();
                        SysReport _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(manhomcls).ExecuteSingle<SysReport>();
                        if (_object == null)
                        {
                            Utility.ShowMsg("Không tồn tại báo cáo có mã:" + manhomcls + "\nKiểm tra lại chức năng khai báo");
                            return "";
                        }
                        try
                        {
                           
                            Document doc;
                            DataRow drData = dt.Rows[0];
                            drData["ten_benhvien"] = globalVariables.Branch_Name;
                            drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                            drData["ten_benhvien"] = globalVariables.Branch_Name;
                            drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                            drData["SDT_bv"] = globalVariables.Branch_Phone;
                            drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                            drData["Fax_bv"] = globalVariables.Branch_Fax;
                            drData["website_bv"] = globalVariables.Branch_Website;
                            drData["email_bv"] = globalVariables.Branch_Email;

                            drData["tieude_phieu"] = _object.TieuDe;
                            drData["dia_diem"] = globalVariables.gv_strDiadiem;
                            drData["ngay_chidinh_trinhky"] = Utility.FormatDateTime(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh);

                            List<string> fieldNames = new List<string>();

                            string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, "PHIEUCHIDINH_TACH.doc");
                            string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                            if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                            string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                            if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                            Utility.CreateMergeFields(dt);

                            if (!File.Exists(PathDoc))
                            {
                                Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                                return "";
                            }



                            string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                            List<string> lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                            string nguoi_tao;
                            if ((drData != null) && File.Exists(PathDoc))
                            {
                                doc = new Document(PathDoc);
                                //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();//View word nên thôi ko cần gọi hàm tạo barcode
                                Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                                fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                                doc.FontSettings = fontSettings;
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                if (doc == null)
                                {
                                    Utility.ShowMsg("Không nạp được file word.", "Thông báo");
                                }
                                Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                                int rowIdx = 1;
                                //Tạo thông tin y lệnh trong tờ điều trị
                                foreach (DataRow row in dt.Rows)
                                {
                                    nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);
                                   
                                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                                    int idx = 1;
                                    Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                                    //newRow.RowFormat.Borders.Shadow = false;
                                    //newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    //newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    //newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                                    newRow.Cells[0].RemoveAllChildren();

                                    newRow.Cells[1].RemoveAllChildren();

                                    newRow.Cells[2].RemoveAllChildren();
                                    newRow.Cells[3].RemoveAllChildren();

                                    newRow.Cells[0].EnsureMinimum();
                                    newRow.Cells[1].EnsureMinimum();
                                    newRow.Cells[2].EnsureMinimum();
                                    newRow.Cells[3].EnsureMinimum();

                                    Run r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Size = 12;
                                    r.Font.Bold = false;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(rowIdx, "");
                                    newRow.Cells[0].FirstParagraph.AppendChild(r);
                                    newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                                    int i = 0;
                                    while (i < newRow.Cells[0].Paragraphs.Count)
                                    {
                                        var para = newRow.Cells[0].Paragraphs[i];
                                        if (para != null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                            para.Remove();
                                        else
                                            i++;
                                    }
                                    //Cột tên dịch vụ
                                    i = 0;
                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 12;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(row["ten_chitietdichvu"], "");
                                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                                    newRow.Cells[1].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                                    while (i < newRow.Cells[1].Paragraphs.Count)
                                    {
                                        var para = newRow.Cells[1].Paragraphs[i];
                                        if (para != null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                            para.Remove();
                                        else
                                            i++;
                                    }
                                    //Cột số lượng
                                    //Cột tên dịch vụ
                                    i = 0;
                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 12;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(row["so_luong"], "");
                                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                                    newRow.Cells[2].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                                    while (i < newRow.Cells[1].Paragraphs.Count)
                                    {
                                        var para = newRow.Cells[2].Paragraphs[i];
                                        if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                            para.Remove();
                                        else
                                            i++;
                                    }
                                    //Cột Ghi chú
                                    i = 0;
                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 12;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(row["chidan_chitiet"], "");
                                    newRow.Cells[3].FirstParagraph.AppendChild(r);
                                    newRow.Cells[3].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                                    newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                                    while (i < newRow.Cells[1].Paragraphs.Count)
                                    {
                                        var para = newRow.Cells[3].Paragraphs[i];
                                        if (para!=null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                            para.Remove();
                                        else
                                            i++;
                                    }

                                   
                                    tab.AppendChild(newRow);
                                    idx += 1;
                                    rowIdx++;
                                }
                                if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                                    if (sysLogosize != null)
                                    {
                                        int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                                        int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                                        if (w > 0 && h > 0)
                                            builder.InsertImage(globalVariables.SysLogo, w, h);
                                        else
                                            builder.InsertImage(globalVariables.SysLogo);
                                    }
                                    else
                                        if (globalVariables.SysLogo != null)
                                        builder.InsertImage(globalVariables.SysLogo);
                                doc.MailMerge.PreserveUnusedTags = true;
                                //Merge các field thông tin chung của người bệnh
                                doc.MailMerge.Execute(drData);
                                sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                                SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                                //List<string> lstSign = new List<string>();
                                //if (KyDientu)//Tìm các vùng chữ kí để đưa ảnh vào
                                //{
                                //    string[] remaining = doc.MailMerge.GetFieldNames();
                                //    lstSign = dt.AsEnumerable().Select(c =>string.Format("CKS_{0}", Utility.sDbnull(c.Field<string>("nguoi_tao")))).Distinct().ToList<string>();
                                //    if (remaining.Length > 0)
                                //    {

                                //        foreach (var name in remaining)
                                //        {
                                //            if (lstSign.Contains(name))
                                //            {
                                //                string _defaultSign = string.Format(@"{0}\{1}\default", Application.StartupPath, "sign");
                                //                string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", name);
                                //                byte[] _sign = null;
                                //                if (File.Exists(_signFile))
                                //                {
                                //                    _sign = Utility.fromimagepath2byte(_signFile);
                                //                }
                                //                else
                                //                {
                                //                    if (File.Exists(_defaultSign))
                                //                        _sign = Utility.fromimagepath2byte(_defaultSign);
                                //                }

                                //                if (builder.MoveToMergeField(name))
                                //                    if (_sign != null)
                                //                    {
                                //                        if (sysSignsize != null)
                                //                        {
                                //                            int w = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[0], 0);
                                //                            int h = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[1], 0);
                                //                            if (w > 0 && h > 0)
                                //                                builder.InsertImage(_sign, w, h);
                                //                            else
                                //                                builder.InsertImage(_sign);
                                //                        }
                                //                        else
                                //                            if (_sign != null)
                                //                            builder.InsertImage(_sign);
                                //                    }
                                //                //else//Không cần vì mergefield này ẩn
                                //                //    builder.InsertImage(NoImage, 10, 10);
                                //            }
                                //        }
                                //    }
                                //    else
                                //    {

                                //    }

                                //}

                                if (File.Exists(PdfFilePath))
                                {
                                    File.Delete(PdfFilePath);
                                }
                                doc.Save(PdfFilePath, SaveFormat.Doc);
                                return PdfFilePath;
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg("Lỗi:" + ex.Message);
                            // Utility.DefaultNow(this);
                        }
                        finally
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return "";
            }
            return "";
        }
        public static string InphieuChidinhCls_doc(List<long> lstSelectedPrint, long idBenhnhan, string maLuotkham, long vAssignId,
                                                   string vAssignCode, string nhomincls, int selectedIndex,
                                                   bool inTach, ref string mayin, bool returnPdf = false, string PdfFilePath = "", bool KyDientu = false)
        {

            try
            {
                mayin = "";
                KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(vAssignId);
                DataTable dt = new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhomincls, string.Join(",", lstSelectedPrint.ToArray())).Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return "";
                }
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv", "tieude_phieu", "dia_diem", "ngay_chidinh_trinhky" };
                Utility.AddColums2DataTable(ref dt, lstMoreColumns, typeof(string));
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                SysSystemParameter sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
               
                string vMachidinh = vAssignCode;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                {
                    vMachidinh = vAssignCode.Replace(".", "");
                }

                dt.TableName = "PHIEUCHIDINH_TACH";

                THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InChungToanBophieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                string tieude = "";
                string khoGiay = "A5";
                if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                string reportCode = "thamkham_InphieuchidinhCLS_A5";
                string reportname = "";
                //EmrDocuments emrdoc = new EmrDocuments();
                //emrdoc.InitDocument(idBenhnhan, maLuotkham, Utility.Int64Dbnull(vAssignId), objchidinh.NgayChidinh, Loaiphieu_HIS.PHIEUCHIDINH, manhomcls, objchidinh.NguoiTao, Utility.Int16Dbnull(objchidinh.IdKhoaChidinh, -1), Utility.Int16Dbnull(objchidinh.IdPhongChidinh, -1), Utility.Byte2Bool(objchidinh.Noitru), "", true,false,objchidinh.MaChidinh);
                //emrdoc.Save();
                SysReport _object = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(reportCode).ExecuteSingle<SysReport>();
                if (_object == null)
                {
                    Utility.ShowMsg("Không tồn tại báo cáo có mã:" + reportCode + "\nKiểm tra lại chức năng khai báo");
                    return "";
                }
                try
                {

                    Document doc;
                    DataRow drData = dt.Rows[0];
                    drData["ten_benhvien"] = globalVariables.Branch_Name;
                    drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                    drData["ten_benhvien"] = globalVariables.Branch_Name;
                    drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                    drData["SDT_bv"] = globalVariables.Branch_Phone;
                    drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                    drData["Fax_bv"] = globalVariables.Branch_Fax;
                    drData["website_bv"] = globalVariables.Branch_Website;
                    drData["email_bv"] = globalVariables.Branch_Email;

                    drData["tieude_phieu"] = _object.TieuDe;
                    drData["dia_diem"] = globalVariables.gv_strDiadiem;
                    drData["ngay_chidinh_trinhky"] = Utility.FormatDateTime(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh);

                    List<string> fieldNames = new List<string>();

                    string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, "PHIEUCHIDINH_TACH.doc");
                    string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                    if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                    string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                    if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                    Utility.CreateMergeFields(dt);

                    if (!File.Exists(PathDoc))
                    {
                        Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                        return "";
                    }



                    string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                    List<string> lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                    string nguoi_tao;
                    if ((drData != null) && File.Exists(PathDoc))
                    {
                        doc = new Document(PathDoc);
                        //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                        Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                        fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                        doc.FontSettings = fontSettings;
                        DocumentBuilder builder = new DocumentBuilder(doc);
                        if (doc == null)
                        {
                            Utility.ShowMsg("Không nạp được file word.", "Thông báo");
                        }
                        Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                        int rowIdx = 1;
                        //Tạo thông tin y lệnh trong tờ điều trị
                        foreach (DataRow row in dt.Rows)
                        {
                            nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);

                            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                            int idx = 1;
                            Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                            //newRow.RowFormat.Borders.Shadow = false;
                            //newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                            //newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                            //newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                            newRow.Cells[0].RemoveAllChildren();

                            newRow.Cells[1].RemoveAllChildren();

                            newRow.Cells[2].RemoveAllChildren();
                            newRow.Cells[3].RemoveAllChildren();

                            newRow.Cells[0].EnsureMinimum();
                            newRow.Cells[1].EnsureMinimum();
                            newRow.Cells[2].EnsureMinimum();
                            newRow.Cells[3].EnsureMinimum();

                            Run r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Size = 12;
                            r.Font.Bold = false;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            r.Text = Utility.sDbnull(rowIdx, "");
                            newRow.Cells[0].FirstParagraph.AppendChild(r);
                            newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
                            int i = 0;
                            while (i < newRow.Cells[0].Paragraphs.Count)
                            {
                                var para = newRow.Cells[0].Paragraphs[i];
                                if (para != null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }
                            //Cột tên dịch vụ
                            i = 0;
                            r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = false;
                            r.Font.Size = 12;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            r.Text = Utility.sDbnull(row["ten_chitietdichvu"], "");
                            newRow.Cells[1].FirstParagraph.AppendChild(r);
                            newRow.Cells[1].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                            while (i < newRow.Cells[1].Paragraphs.Count)
                            {
                                var para = newRow.Cells[1].Paragraphs[i];
                                if (para != null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }
                            //Cột số lượng
                            //Cột tên dịch vụ
                            i = 0;
                            r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = false;
                            r.Font.Size = 12;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            r.Text = Utility.sDbnull(row["so_luong"], "");
                            newRow.Cells[2].FirstParagraph.AppendChild(r);
                            newRow.Cells[2].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                            while (i < newRow.Cells[1].Paragraphs.Count)
                            {
                                var para = newRow.Cells[2].Paragraphs[i];
                                if (string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }
                            //Cột Ghi chú
                            i = 0;
                            r = new Run(doc);
                            r.Font.Name = "Times New Roman";
                            r.Font.Bold = false;
                            r.Font.Size = 12;
                            //r.Font.Color = Color.FromArgb(102, 0, 102);
                            r.Text = Utility.sDbnull(row["chidan_chitiet"], "");
                            newRow.Cells[3].FirstParagraph.AppendChild(r);
                            newRow.Cells[3].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
                            newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Left;
                            while (i < newRow.Cells[1].Paragraphs.Count)
                            {
                                var para = newRow.Cells[3].Paragraphs[i];
                                if (para != null && string.IsNullOrWhiteSpace(para.ToString(SaveFormat.Text)))
                                    para.Remove();
                                else
                                    i++;
                            }


                            tab.AppendChild(newRow);
                            idx += 1;
                            rowIdx++;
                        }
                        if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                            if (sysLogosize != null)
                            {
                                int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                                int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                                if (w > 0 && h > 0)
                                    builder.InsertImage(globalVariables.SysLogo, w, h);
                                else
                                    builder.InsertImage(globalVariables.SysLogo);
                            }
                            else
                                if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                        doc.MailMerge.PreserveUnusedTags = true;
                        //Merge các field thông tin chung của người bệnh
                        doc.MailMerge.Execute(drData);
                        sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                        SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                        //List<string> lstSign = new List<string>();
                        //if (KyDientu)//Tìm các vùng chữ kí để đưa ảnh vào
                        //{
                        //    string[] remaining = doc.MailMerge.GetFieldNames();
                        //    lstSign = dt.AsEnumerable().Select(c => string.Format("CKS_{0}", Utility.sDbnull(c.Field<string>("nguoi_tao")))).Distinct().ToList<string>();
                        //    if (remaining.Length > 0)
                        //    {

                        //        foreach (var name in remaining)
                        //        {
                        //            if (lstSign.Contains(name))
                        //            {
                        //                string _defaultSign = string.Format(@"{0}\{1}\default", Application.StartupPath, "sign");
                        //                string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", name);
                        //                byte[] _sign = null;
                        //                if (File.Exists(_signFile))
                        //                {
                        //                    _sign = Utility.fromimagepath2byte(_signFile);
                        //                }
                        //                else
                        //                {
                        //                    if (File.Exists(_defaultSign))
                        //                        _sign = Utility.fromimagepath2byte(_defaultSign);
                        //                }

                        //                if (builder.MoveToMergeField(name))
                        //                    if (_sign != null)
                        //                    {
                        //                        if (sysSignsize != null)
                        //                        {
                        //                            int w = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[0], 0);
                        //                            int h = Utility.Int32Dbnull(sysSignsize.SValue.Split('x')[1], 0);
                        //                            if (w > 0 && h > 0)
                        //                                builder.InsertImage(_sign, w, h);
                        //                            else
                        //                                builder.InsertImage(_sign);
                        //                        }
                        //                        else
                        //                            if (_sign != null)
                        //                            builder.InsertImage(_sign);
                        //                    }
                        //                //else//Không cần vì mergefield này ẩn
                        //                //    builder.InsertImage(NoImage, 10, 10);
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {

                        //    }

                        //}

                        if (File.Exists(PdfFilePath))
                        {
                            File.Delete(PdfFilePath);
                        }
                        doc.Save(PdfFilePath, SaveFormat.Doc);
                        return PdfFilePath;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                    // Utility.DefaultNow(this);
                }
                finally
                {
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            return "";

        }
        static Dictionary<string, string> GetDictionaryFromDataTable()
        {
            var dict = new Dictionary<string, string>();

            foreach (DataRow row in globalVariables.dtSignInfor.Rows)
            {
                string key = row["ten_vitri_ky"].ToString();
                string value = row["nguoi_ky"].ToString();

                if (!dict.ContainsKey(key))
                    dict.Add(key, value);
            }

            return dict;
        }
        static void SignDoc(Document doc, DocumentBuilder builder, string Signsize)
        {
            if (globalVariables.dtSignInfor.Rows.Count > 0 && globalVariables.dtSignInfor.Columns.Count > 0)//Tìm các vùng chữ kí để đưa ảnh vào
            {
                string[] remaining = doc.MailMerge.GetFieldNames();
                Dictionary<string, string> lstVitriky = GetDictionaryFromDataTable();
                if (remaining.Length > 0)
                {

                    foreach (var name in remaining)
                    {
                        if (lstVitriky.ContainsKey(name))
                        {
                            string _defaultSign = string.Format(@"{0}\{1}\default", Application.StartupPath, "sign");
                            string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", lstVitriky[name]);
                            byte[] _sign = null;
                            if (File.Exists(_signFile))
                            {
                                _sign = Utility.fromimagepath2byte(_signFile);
                            }
                            else
                            {
                                if (File.Exists(_defaultSign))
                                    _sign = Utility.fromimagepath2byte(_defaultSign);
                            }

                            if (builder.MoveToMergeField(name))
                                if (_sign != null)
                                {
                                    if (Signsize != "")
                                    {
                                        int w = Utility.Int32Dbnull(Signsize.Split('x')[0], 0);
                                        int h = Utility.Int32Dbnull(Signsize.Split('x')[1], 0);
                                        if (w > 0 && h > 0)
                                            builder.InsertImage(_sign, w, h);
                                        else
                                            builder.InsertImage(_sign);
                                    }
                                    else
                                        if (_sign != null)
                                        builder.InsertImage(_sign);
                                }
                            //else//Không cần vì mergefield này ẩn
                            //    builder.InsertImage(NoImage, 10, 10);
                        }
                    }
                }
                else
                {

                }

            }
        }
        public static int CalculateMinimumWidth(string data)
        {
            int modulePerChar = 11;              // mỗi ký tự chiếm ~11 module
            int startStopModules = 35;           // start + checksum + stop
            int quietZoneModules = 10;           // vùng trắng ở 2 đầu

            int totalModules = (data.Length * modulePerChar) + startStopModules + quietZoneModules;
            int pixelsPerModule = 1;

            return totalModules * pixelsPerModule;
        }
        public void ImageFieldMerging(ImageFieldMergingArgs e)
        {
            // Không dùng ở đây
        }
        class HandleMergeBarcode : IFieldMergingCallback
        {
            public void FieldMerging(FieldMergingArgs e)
            {
                if (e.FieldName.ToUpper() == "MA_LUOTKHAM" || e.FieldName.ToUpper() == "MA_CHIDINH")
                {
                    int resolution = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_RESOLUTION", "300", false), 300);
                    int Width = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_WIDTH", "600", false), 600);
                    int Height = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BARCODE_HEIGHT", "200", false), 200);

                    string ma = e.FieldValue.ToString();
                    if (!ma.Contains("*")) ma = string.Format("*{0}*", ma);
                    int minWidth = CalculateMinimumWidth(ma);

                    byte[] bytImg = Utility.GetBarcodeDataLeadtools(ma);
                    DocumentBuilder builder = new DocumentBuilder(e.Document);
                    builder.MoveToMergeField(e.FieldName);
                    builder.InsertImage(bytImg, Width, Height);

                    //if (!ma.Contains(".")) ma = ma.Replace(".", "-");
                    // if (ma.Contains("*")) ma = ma.Replace("*","");// string.Format("*{0}*", ma);
                    // Khởi tạo barcode
                    #region BarcodeLibs
                    //Barcode barcode = new Barcode();

                    //barcode.IncludeLabel = true; // Hiện số mã bên dưới
                    //barcode.LabelFont = new System.Drawing.Font("Times New Roman", 12 * 4, System.Drawing.FontStyle.Bold); // Font của text dưới
                    //barcode.Alignment = AlignmentPositions.CENTER;
                    //barcode.Width = Width * 5; // Chiều rộng (px)
                    //barcode.Height = Height * 5; // Chiều cao (px)
                    //barcode.Encode(TYPE.CODE128B, ma);
                    //if (Width > minWidth)
                    //    Width = minWidth;
                    //// Tạo ảnh stream để chèn vào word
                    //using (MemoryStream stream = new MemoryStream())
                    //{
                    //    barcode.SaveImage(stream, SaveTypes.PNG);
                    //    stream.Position = 0;

                    //    // Chèn ảnh vào vị trí merge field
                    //    DocumentBuilder builder = new DocumentBuilder(e.Document);
                    //    builder.MoveToMergeField(e.FieldName);
                    //    builder.InsertImage(stream, Width, Height);

                    //    //// Tùy chỉnh kích thước ảnh
                    //    //image.Width = Width;  // nhỏ lại cho vừa
                    //    //image.Height = Height;
                    //}
                    #endregion


                    e.Text = ""; // Xóa text gốc (tránh bị lặp)
                }
            }

            public void ImageFieldMerging(ImageFieldMergingArgs e)
            {
                // Không dùng ở đây
            }
        }
        public static ActionResult NoitruInTachToanBoPhieuCls( int idBenhnhan, string maLuotkham, int vAssignId,
                                                     string vAssignCode, List<string> listnhomincls,
                                                     int selectedIndex, bool inTach, DateTime ngayin, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        DataTable dtAll =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                "ALL","").Tables[0];
                        foreach (string nhomcls in listnhomincls.ToList())
                        {
                            //   KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                            DataTable dt = dtAll.Select("nhom_in_cls = '" + nhomcls + "'").CopyToDataTable();
                            if (dt == null || dt.Rows.Count <= 0)
                            {
                                Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                                //return;
                            }
                            else
                            {
                                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InTachToanBophieuCLS.XML");
                                Utility.UpdateLogotoDatatable(ref dt);
                                string vMachidinh = vAssignCode;
                                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", false) ==
                                    "1")
                                {
                                    vMachidinh = vAssignCode.Replace(".", "");
                                }
                                Utility.CreateBarcodeData(ref dt, vMachidinh);
                                string manhomcls = nhomcls;
                                string tieude = "";
                                string reportname = "";
                                ReportDocument crpt = Utility.GetReport(manhomcls, ref tieude, ref reportname);
                                if (crpt == null) return ActionResult.Error;
                                try
                                {
                                    var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                                    {
                                        mv_sReportFileName = Path.GetFileName(reportname),
                                        mv_sReportCode = manhomcls
                                    };
                                    crpt.SetDataSource(dt);
                                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên        Bác sĩ chỉ định     ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                                    Utility.SetParameterValue(crpt, "ParentBranchName",
                                        globalVariables.ParentBranch_Name);
                                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                                    Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                                    Utility.SetParameterValue(crpt, "txtTrinhky",
                                        Utility.getTrinhky(objForm.mv_sReportFileName,
                                            DateTime.Now));
                                    if (!inTach && selectedIndex == 0)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                                    "PHIẾU CHỈ ĐỊNH", true);
                                    }
                                    else
                                    {
                                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                                    }
                                    Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(ngayin));
                                    Utility.SetParameterValue(crpt, "CurrentDate",
                                        Utility.FormatDateTimeWithLocation(ngayin, globalVariables.gv_strDiadiem));
                                    objForm.crptViewer.ReportSource = crpt;
                                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                        PropertyLib._MayInProperties.PreviewInCLS))
                                    {
                                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                        objForm.ShowDialog();
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                    }
                                    else
                                    {
                                        objForm.addTrinhKy_OnFormLoad();
                                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                        crpt.PrintToPrinter(1, false, 0, 0);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utility.ShowMsg("Lỗi:" + ex.Message);
                                    // Utility.DefaultNow(this);
                                }
                                finally
                                {
                                    Utility.FreeMemory(crpt);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                        return ActionResult.Error;
                    }

                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
        public static ActionResult InphieuDangkyKiemnghiem(int vIntIdMauKn)
        {
            return ActionResult.Success;
        }
        public static ActionResult InphieuBangiaoMauKiemnghiem(int vIntIdMauKn)
        {
            return ActionResult.Success;
        }
        public static ActionResult InphieuChidinhCls_Old(int idBenhnhan, string maLuotkham, int vAssignId,
                                                     string vAssignCode, string nhomincls, int selectedIndex,
                                                     bool inTach, ref string mayin)
        {

            try
            {
                mayin = "";
                //KcbChidinhcl.FetchByID(vAssignId);

                DataTable dt =
                    new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhomincls,"").Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return ActionResult.Error;
                }
                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                string vMachidinh = vAssignCode;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                {
                    vMachidinh = vAssignCode.Replace(".", "");
                }
                Utility.CreateBarcodeData(ref dt, vMachidinh);
                string reportCode;
                string khoGiay = "A5";
                bool inchung = false;
                string tieude = "", reportname = "";
                if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                if (khoGiay == "A5")
                    if (inTach || selectedIndex>0) //Nếu in riêng mà chọn tất
                    {
                        if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                        {
                            switch (nhomincls)
                            {
                                case "CD":
                                case "XN":
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                    break;
                                //case "XN":
                                //    reportCode = "thamkham_InphieuXetNghiem_A5";
                                //    break;
                                case "SA":
                                    reportCode = "thamkham_InphieuSieuAm_A5";
                                    break;
                                case "XQ":
                                    reportCode = "thamkham_InphieuXQuang_A5";
                                    break;
                                case "NS":
                                    reportCode = "thamkham_InphieuNoiSoi_A5";
                                    break;
                                case "DT":
                                case "DN":
                                    reportCode = "thamkham_InphieuDienTim_A5";
                                    break;
                                default:
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                    break;
                            }
                        }
                        else
                        {
                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                        }
                    }
                    else
                    {
                        inchung = true;
                        reportCode = "thamkham_InphieuchidinhCLS_A5";
                    }
                else //Khổ giấy A4
                    if (inTach || selectedIndex > 0)
                    {
                        if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                        {
                            switch (nhomincls)
                            {
                                case "CD":
                                case "XN":
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                    break;
                                //case "XN":
                                //    reportCode = "thamkham_InphieuXetNghiem_A4";
                                //    break;
                                case "SA":
                                    reportCode = "thamkham_InphieuSieuAm_A4";
                                    break;
                                case "XQ":
                                    reportCode = "thamkham_InphieuXQuang_A4";
                                    break;
                                case "NS":
                                    reportCode = "thamkham_InphieuNoiSoi_A4";
                                    break;
                                case "DT":
                                case "DN":
                                    reportCode = "thamkham_InphieuDienTim_A4";
                                    break;
                                default:
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                    break;
                            }
                        }
                        else
                        {
                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                        }
                    } //Nếu in riêng mà chọn tất-->Gọi báo cáo nhóm theo nhóm in
                    //  _reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                    else
                    {
                        inchung = true;

                        reportCode = "thamkham_InphieuchidinhCLS_A4";
                    }
                ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                if (crpt == null) return ActionResult.Error;
                if (inchung)
                {
                    List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                                 where
                                                     Utility.DoTrim(
                                                         Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                 select p.Field<string>("nhom_in_cls")
                        ).Distinct().ToList();
                    if (lstNhominCls.Count > 1)
                    {
                        string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                            "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                            true);
                        foreach (DataRow dr in dt.Rows)
                            dr["ten_nhominphieucls"] = tenphieuchidinh;
                    }
                }
                var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                {
                    mv_sReportFileName = Path.GetFileName(reportname),
                    mv_sReportCode = reportCode
                };
                try
                {
                    crpt.SetDataSource(dt);
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt, "txtTrinhky",
                        Utility.getTrinhky(objForm.mv_sReportFileName,
                            DateTime.Now));
                    if (!inTach && selectedIndex == 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH", true);
                    }
                    else
                    {
                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                    }
                    Utility.SetParameterValue(crpt, "CurrentDate",
                        Utility.FormatDateTimeWithLocation(DateTime.Now,
                            globalVariables.gv_strDiadiem));
                    objForm.crptViewer.ReportSource = crpt;
                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                        PropertyLib._MayInProperties.PreviewInCLS))
                    {
                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                        objForm.ShowDialog();
                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                    }
                    else
                    {
                        objForm.addTrinhKy_OnFormLoad();
                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                        crpt.PrintToPrinter(1, false, 0, 0);
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                    return ActionResult.Error;
                    // Utility.DefaultNow(this);
                }
                finally
                {
                    Utility.FreeMemory(crpt);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            return ActionResult.Success;

        }
        public static ActionResult InphieuChidinhCls(long idBenhnhan, string maLuotkham, long vAssignId,
                                                     string vAssignCode, string nhomincls, int selectedIndex,
                                                     bool inTach, ref string mayin)
        {

            try
            {
                mayin = "";
              KcbChidinhcl objchidinh=  KcbChidinhcl.FetchByID(vAssignId);

                DataTable dt =
                    new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhomincls,"").Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return ActionResult.Error;
                }
                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                string vMachidinh = vAssignCode;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                {
                    vMachidinh = vAssignCode.Replace(".", "");
                }
                Utility.CreateBarcodeData(ref dt, vMachidinh);
                string reportCode = "thamkham_InphieuchidinhCLS_A5";
                string khoGiay = "A5";
                bool inchung = false;
                string tieude = "", reportname = "";
                ReportDocument crpt = null;
                if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                if (inTach || selectedIndex > 0) //Nếu in riêng mà chọn tất
                {
                    if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                    {
                        crpt = Utility.GetReport(nhomincls, khoGiay, ref tieude, ref reportname);
                    }
                    else
                    {
                        reportCode = "thamkham_InphieuchidinhCLS_A5";// "thamkham_InphieuchidinhCLS_RIENG_A5";
                    }
                }
                else
                {
                    inchung = true;
                        reportCode = "thamkham_InphieuchidinhCLS_A5";
                }
                if (crpt == null)
                    crpt = Utility.GetReport(reportCode, khoGiay, ref tieude, ref reportname);
                if (crpt == null) return ActionResult.Error;
                if (inchung)
                {
                    List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                                 where
                                                     Utility.DoTrim(
                                                         Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                 select p.Field<string>("nhom_in_cls")
                        ).Distinct().ToList();
                    if (lstNhominCls.Count > 1)
                    {
                        string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                            "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                            true);
                        foreach (DataRow dr in dt.Rows)
                            dr["ten_nhominphieucls"] = tenphieuchidinh;
                    }
                }
                var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                {
                    mv_sReportFileName = Path.GetFileName(reportname),
                    mv_sReportCode = nhomincls
                };
                objForm.NGAY = objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh;
                objForm.nguoi_thuchien = Utility.sDbnull(dt.Rows[0]["ten_bacsi_chidinh"], "");
                decimal tong =Utility.DecimaltoDbnull( dt.Compute("sum(Bnhan_chitra)", "1=1"),0);
                try
                {
                    crpt.SetDataSource(dt);
                    Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                    Utility.SetParameterValue(crpt, "txtTrinhky",
                        Utility.getTrinhky(objForm.mv_sReportFileName,
                            DateTime.Now));
                    if (!inTach && selectedIndex == 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH", true);
                    }
                    else
                    {
                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                    }
                    Utility.SetParameterValue(crpt, "CurrentDate",
                        Utility.FormatDateTimeWithLocation(DateTime.Now,
                            globalVariables.gv_strDiadiem));
                    objForm.crptViewer.ReportSource = crpt;
                    
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                        PropertyLib._MayInProperties.PreviewInCLS))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                            objForm.ShowDialog();
                            mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                        }
                        else
                        {
                            objForm.addTrinhKy_OnFormLoad();
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }
                    
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                    return ActionResult.Error;
                    // Utility.DefaultNow(this);
                }
                finally
                {
                    Utility.FreeMemory(crpt);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            return ActionResult.Success;

        }
        public static ActionResult InphieuChidinhCls(List<long> lstSelectedPrint, long idBenhnhan, string maLuotkham, long vAssignId,
                                                    string vAssignCode, string nhomincls, int selectedIndex,
                                                    bool inTach, ref string mayin, bool returnPdf = false, string PdfFilePath = "")
        {

            try
            {
                mayin = "";
                KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(vAssignId);
                DmucNhanvien objBscd = DmucNhanvien.FetchByID(objchidinh.IdBacsiChidinh);
                DataTable dt= new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                       nhomincls, string.Join(",", lstSelectedPrint.ToArray())).Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return ActionResult.Error;
                }
                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                string vMachidinh = vAssignCode;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                {
                    vMachidinh = vAssignCode.Replace(".", "");
                }
                Utility.CreateBarcodeData(ref dt, vMachidinh);
                string reportCode = "thamkham_InphieuchidinhCLS_A5";
                string khoGiay = "A5";
                bool inchung = false;
                string tieude = "", reportname = "";
                ReportDocument crpt = null;
                if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                if (inTach || selectedIndex > 0) //Nếu in riêng mà chọn tất
                {
                    if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                    {
                        crpt = Utility.GetReport(nhomincls, khoGiay, ref tieude, ref reportname);
                    }
                    else
                    {
                        reportCode = "thamkham_InphieuchidinhCLS_A5";// "thamkham_InphieuchidinhCLS_RIENG_A5";
                    }
                }
                else
                {
                    inchung = true;
                    reportCode = "thamkham_InphieuchidinhCLS_A5";
                }
                if (crpt == null)
                    crpt = Utility.GetReport(reportCode, khoGiay, ref tieude, ref reportname);
                if (crpt == null) return ActionResult.Error;
                EmrDocuments emrdoc = new EmrDocuments();
                emrdoc.InitDocument(idBenhnhan, maLuotkham, Utility.Int64Dbnull(vAssignId), objchidinh.NgayChidinh, Loaiphieu_HIS.PHIEUCHIDINH, reportCode, objchidinh.NguoiTao, Utility.Int16Dbnull(objchidinh.IdKhoaChidinh, -1), Utility.Int16Dbnull(objchidinh.IdPhongChidinh, -1), Utility.Byte2Bool(objchidinh.Noitru), "");
                emrdoc.Save();
                if (inchung)
                {
                    List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                                 where
                                                     Utility.DoTrim(
                                                         Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                 select p.Field<string>("nhom_in_cls")
                        ).Distinct().ToList();
                    if (lstNhominCls.Count > 1)
                    {
                        string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                            "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                            true);
                        foreach (DataRow dr in dt.Rows)
                            dr["ten_nhominphieucls"] = tenphieuchidinh;
                    }
                }
                var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                {
                    mv_sReportFileName = Path.GetFileName(reportname),
                    mv_sReportCode = nhomincls
                };
                objForm.NGAY = objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh;
                objForm.nguoi_thuchien = Utility.sDbnull(dt.Rows[0]["ten_bacsi_chidinh"], "");
                decimal tong = Utility.DecimaltoDbnull(dt.Compute("sum(Bnhan_chitra)", "1=1"), 0);
                try
                {
                    crpt.SetDataSource(dt);
                    Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
                    Utility.SetParameterValue(crpt, "txtTrinhky",
                        Utility.getTrinhky(objForm.mv_sReportFileName,
                            DateTime.Now));
                    if (!inTach && selectedIndex == 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH", true);
                    }
                    else
                    {
                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                    }
                    Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
                    Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh));
                    Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(objchidinh == null ? DateTime.Now : objchidinh.NgayChidinh, globalVariables.gv_strDiadiem));
                    objForm.crptViewer.ReportSource = crpt;
                    if (returnPdf && PdfFilePath != "")
                    {
                        objForm.addTrinhKy_OnFormLoad();
                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;

                        crpt.ExportToDisk(ExportFormatType.PortableDocFormat, PdfFilePath);
                    }
                    else
                    {
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                        PropertyLib._MayInProperties.PreviewInCLS))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                            objForm.ShowDialog();
                            mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                        }
                        else
                        {
                            objForm.addTrinhKy_OnFormLoad();
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                    return ActionResult.Error;
                    // Utility.DefaultNow(this);
                }
                finally
                {
                    Utility.FreeMemory(crpt);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }

            return ActionResult.Success;

        }

       
        public static ActionResult NoitruInphieuChidinhCls(int idBenhnhan, string maLuotkham, int vAssignId,
                                                   string vAssignCode, string nhomincls, int selectedIndex,
                                                   bool inTach, ref string mayin)
        {
            using (var scope = new TransactionScope())
            {
                using (new SharedDbConnectionScope())
                {
                    try
                    {
                        mayin = "";
                        //KcbChidinhcl.FetchByID(vAssignId);

                        DataTable dt =
                            new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                                nhomincls,"").Tables[0];
                        if (dt == null || dt.Rows.Count <= 0)
                        {
                            // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                            return ActionResult.Error;
                        }
                        //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                        Utility.UpdateLogotoDatatable(ref dt);
                        string vMachidinh = vAssignCode;
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                        {
                            vMachidinh = vAssignCode.Replace(".", "");
                        }
                        Utility.CreateBarcodeData(ref dt, vMachidinh);
                        string reportCode;
                        string khoGiay = "A5";
                        bool inchung = false;
                        string tieude = "", reportname = "";
                        if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) khoGiay = "A4";
                        if (khoGiay == "A5")
                            if (inTach) //Nếu in riêng mà chọn tất
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A5";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A5";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A5";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A5";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A5";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A5";
                                }
                            }
                            else
                            {
                                inchung = true;
                                reportCode = "thamkham_InphieuchidinhCLS_A5";
                            }
                        else //Khổ giấy A4
                            if (inTach && selectedIndex == 0)
                            {
                                if (PropertyLib._ThamKhamProperties.ChophepintachCLSKhacPhieu)
                                {
                                    switch (selectedIndex)
                                    {
                                        case 0:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                        case 1:
                                            reportCode = "thamkham_InphieuXetNghiem_A4";
                                            break;
                                        case 2:
                                            reportCode = "thamkham_InphieuSieuAm_A4";
                                            break;
                                        case 3:
                                            reportCode = "thamkham_InphieuXQuang_A4";
                                            break;
                                        case 5:
                                            reportCode = "thamkham_InphieuNoiSoi_A4";
                                            break;
                                        case 6:
                                            reportCode = "thamkham_InphieuDienTim_A4";
                                            break;
                                        default:
                                            reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                            break;
                                    }
                                }
                                else
                                {
                                    reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                                }
                            } //Nếu in riêng mà chọn tất-->Gọi báo cáo nhóm theo nhóm in
                            //  _reportCode = "thamkham_InphieuchidinhCLS_RIENG_A4";
                            else
                            {
                                inchung = true;

                                reportCode = "thamkham_InphieuchidinhCLS_A4";
                            }
                        ReportDocument crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                        if (crpt == null) return ActionResult.Error;
                        if (inchung)
                        {
                            List<string> lstNhominCls = (from p in dt.AsEnumerable()
                                                         where
                                                             Utility.DoTrim(
                                                                 Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                         select p.Field<string>("nhom_in_cls")
                                ).Distinct().ToList();
                            if (lstNhominCls.Count > 1)
                            {
                                string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG",
                                    "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG",
                                    true);
                                foreach (DataRow dr in dt.Rows)
                                    dr["ten_nhominphieucls"] = tenphieuchidinh;
                            }
                        }
                        var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true)
                        {
                            mv_sReportFileName = Path.GetFileName(reportname),
                            mv_sReportCode = reportCode
                        };
                        try
                        {
                            crpt.SetDataSource(dt);
                            Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                            Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                            Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                            Utility.SetParameterValue(crpt, "txtTrinhky",
                                Utility.getTrinhky(objForm.mv_sReportFileName,
                                    DateTime.Now));
                            if (!inTach && selectedIndex == 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                    dr[VKcbChidinhcl.Columns.TenNhominphieucls] =
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG",
                                            "PHIẾU CHỈ ĐỊNH", true);
                            }
                            else
                            {
                                Utility.SetParameterValue(crpt, "TitleReport", tieude);
                            }
                            Utility.SetParameterValue(crpt, "CurrentDate",
                                Utility.FormatDateTimeWithLocation(DateTime.Now,
                                    globalVariables.gv_strDiadiem));
                            objForm.crptViewer.ReportSource = crpt;
                            if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                PropertyLib._MayInProperties.PreviewInCLS))
                            {
                                objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                                objForm.ShowDialog();
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                            }
                            else
                            {
                                objForm.addTrinhKy_OnFormLoad();
                                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                                mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                                crpt.PrintToPrinter(1, false, 0, 0);
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.ShowMsg("Lỗi:" + ex.Message);
                            return ActionResult.Error;
                            // Utility.DefaultNow(this);
                        }
                        finally
                        {
                            Utility.FreeMemory(crpt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg("Lỗi:" + ex.Message);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                scope.Complete();
                return ActionResult.Success;
            }
        }
    }
}