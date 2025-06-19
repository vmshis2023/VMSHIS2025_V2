using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VMS.HIS.BA
{
    class Class1_temp
    {
        public static string InphieuChidinhCls_doc(List<long> lstSelectedPrint, long idBenhnhan, string maLuotkham, long vAssignId,
                                                 string vAssignCode, string nhomincls, int selectedIndex,
                                                 bool inTach, ref string mayin, bool returnPdf = false, string PdfFilePath = "")
        {

            try
            {
                mayin = "";
                KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(vAssignId);
                DataTable dt = new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(idBenhnhan, maLuotkham, vAssignCode,
                        nhomincls, string.Join(",", lstSelectedPrint.ToArray())).Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    // Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return "";
                }
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv", "tieude_phieu", "dia_diem", "ngay_chidinh_trinhky" };
                Utility.AddColums2DataTable(ref dtAll, lstMoreColumns, typeof(string));
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();
                SysSystemParameter sysSignsize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                //   THU_VIEN_CHUNG.CreateXML(dt, "Thamkham_InphieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                string vMachidinh = vAssignCode;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("CHIDINH_BODAUCHAM_TRENMAVACH", "0", true) == "1")
                {
                    vMachidinh = vAssignCode.Replace(".", "");
                }
                //Utility.CreateBarcodeData(ref dt, vMachidinh);
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
                if (crpt == null) return "";
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
                    return PdfFilePath;
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                    return "";
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

            return "";

        }
    }
}
