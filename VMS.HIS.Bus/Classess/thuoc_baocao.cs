using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using System.Collections.Generic;

namespace VNS.HIS.UI.Baocao
{
    public class thuoc_baocao
    {
        public static void BaocaoNhapxuatThuoc(DataTable m_dtReport, string kieubaocao, string kieuthuoc_vt,
                                               string sTitleReport, string _tondau, string _toncuoi, DateTime NgayIn,
                                               string FromDateToDate, string tenkho, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_nhapxuatthuoc_theonhom";
            ReportDocument crpt = null;

            if (theonhom)
            {
                if (kieubaocao == "0")
                    reportcode = "thuoc_baocao_nhapxuatthuoc_theonhom";
                else if (kieubaocao == "1")
                    reportcode = "thuoc_baocao_nhapthuoc_theonhom";
                else
                    reportcode = "thuoc_baocao_xuatthuoc_theonhom";
            }
            else
            {
                if (kieubaocao == "0")
                    reportcode = "thuoc_baocao_nhapxuatthuoc";
                else if (kieubaocao == "1")
                    reportcode = "thuoc_baocao_nhapthuoc";
                else
                    reportcode = "thuoc_baocao_xuatthuoc";
            }

            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void Baocao_thuoc_bangcandoi(DataTable m_dtReport, string kieubaocao, string kieuthuoc_vt,
                                                   string sTitleReport, string _tondau, string _toncuoi, DateTime NgayIn,
                                                   string FromDateToDate, string tenkho, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_bangcandoi";
            ReportDocument crpt = null;

            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Tondau", _tondau);
                Utility.SetParameterValue(crpt, "Toncuoi", _toncuoi);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                //    Utility.SetParameterValue(crpt, "thanhtien_bangchu", _moneyByLetter.sMoneyToLetter(tinhtong.ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }

        public static void BaocaoNhapxuattonTheoquy(DataTable m_dtReport, string kieubaocao, string kieuthuoc_vt,
                                                    string sTitleReport, string _tondau, string _toncuoi,
                                                    DateTime NgayIn, string FromDateToDate, string tenkho, bool theonhom,byte Baocaotheolohandung)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_nhapxuatton_theoquy_theonhom";
            ReportDocument crpt = null;
            if (kieuthuoc_vt.Contains("THUOC"))
            {
                if (theonhom)
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
                        reportcode = "thuoc_baocao_nhap_theoquy";
                    else
                        reportcode = "thuoc_baocao_xuat_theoquy";
                }
            }
            else //VTTH
            {
                if (theonhom)
                {
                    reportcode = "vt_baocao_nhapxuatton_theoquy_theonhom";
                }
                else
                {
                    reportcode = "vt_baocao_nhapxuatton_theoquy";
                }
            }
            if(Baocaotheolohandung==1)
                reportcode = "thuoc_baocao_nhapxuatton_theoquy_solohandung";
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            string tinhtong = TinhTong(m_dtReport, "TT_TONCUOI");
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Tondau", _tondau);
                Utility.SetParameterValue(crpt, "Toncuoi", _toncuoi);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "thanhtien_bangchu", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoXuatthuoctheoKhoaNoitru(DataTable m_dtReport, string kieubaocao, string kieuthuoc_vt,
                                                         string sTitleReport, DateTime NgayIn, string FromDateToDate,
                                                         string tenkho, string tenkhoa, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_xuatthuocnoitru";
            ReportDocument crpt = null;
            if (kieubaocao == "0")
                reportcode = "thuoc_baocao_xuatthuocnoitru_tonghop";
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "Khoalinh", tenkhoa);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        /// hàm thưc hiện việc in phiếu vật tư và thuốc
        /// </summary>
        /// <param name="objDPhieuCapphat"></param>
        /// <param name="dataTable"></param>
        public static void Inphieutonghoplinhthuocnoitru_bak(TPhieuCapphatNoitru objDPhieuCapphat, DataTable dataTable)
        {
            if (objDPhieuCapphat != null)
            {
                // string loaiphieulinh = objDPhieuCapphat.LinhBSung == false ? "Lĩnh thường" : "Lĩnh bổ sung";

                if (objDPhieuCapphat.KieuThuocVt == "VT")
                    PhieuTongHopLinhThuoc(dataTable,
                                          objDPhieuCapphat.NgayNhap, "thuoc_noitru_phieutonghopvattunoitru");
                else
                {
                    EnumerableRowCollection<DataRow> query = from thuoc in dataTable.AsEnumerable()
                                                             where
                                                                 Utility.Int32Dbnull(thuoc[DmucThuoc.Columns.MaTinhchat]) >=
                                                                 1
                                                             select thuoc;

                    if (query.Any())
                    {
                        DataTable m_dtThuocGayNghien = query.CopyToDataTable();
                        if (m_dtThuocGayNghien != null)
                        {
                            PhieuTongHopLinhThuoc(m_dtThuocGayNghien, objDPhieuCapphat.NgayNhap,
                                                  "thuoc_noitru_phieutonghopthuocgaynghiennoitru");
                        }
                        query = from thuoc in dataTable.AsEnumerable()
                                where Utility.Int32Dbnull(thuoc[DmucThuoc.Columns.MaTinhchat]) == 0
                                select thuoc;
                        if (query.Any())
                        {
                            DataTable m_dtThuocThuong = query.CopyToDataTable();
                            if (m_dtThuocThuong != null)
                                PhieuTongHopLinhThuoc(m_dtThuocThuong, objDPhieuCapphat.NgayNhap,
                                                      "thuoc_noitru_phieutonghopthuocnoitru");
                        }
                    }
                    else
                    {
                        query = from thuoc in dataTable.AsEnumerable()
                                where Utility.Int32Dbnull(thuoc[DmucThuoc.Columns.MaTinhchat], 0) == 0
                                select thuoc;
                        if (query.Any())
                        {
                            dataTable = query.CopyToDataTable();
                            if (dataTable != null)
                            {
                                DataTable m_dtPhieuLinhThuoc = PhieuLinhThuoc(dataTable, "THUOC");
                                if (m_dtPhieuLinhThuoc != null)
                                {
                                    PhieuTongHopLinhThuoc(dataTable, objDPhieuCapphat.NgayNhap,
                                                          "thuoc_noitru_phieutonghopthuocnoitru");
                                }
                                m_dtPhieuLinhThuoc = PhieuLinhThuoc(dataTable, "VT");
                                if (m_dtPhieuLinhThuoc != null)
                                {
                                    PhieuTongHopLinhThuoc(dataTable, objDPhieuCapphat.NgayNhap,
                                                          "thuoc_noitru_phieutonghopvattunoitru");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void Inphieutonghoplinhthuocnoitru(TPhieuCapphatNoitru objDPhieuCapphat, DataTable v_dtData)
        {
            if (objDPhieuCapphat != null)
            {
                if (objDPhieuCapphat.KieuThuocVt == "VT")
                    PhieuTongHopLinhThuoc(v_dtData,
                                          objDPhieuCapphat.NgayNhap, "thuoc_noitru_phieutonghopvattunoitru");
                else
                {
                    List<string> lstmatinhchat = (from p in v_dtData.AsEnumerable()
                                                  select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
                    foreach (string ma_tinhchat in lstmatinhchat)
                    {
                        DataTable v_PrintData = v_dtData.Select(string.Format("ma_tinhchat='{0}' or ma_tinhchat is null", ma_tinhchat)).CopyToDataTable();//Chắc chắn có dữ liệu nên hàm copy ko bị lỗi
                        //Lấy danh sách các reportcode của từng tính chất thuốc
                        List<string> lstReportCode = v_PrintData.Rows[0]["report_code"].ToString().Split('@')[1].Split(';').ToList<string>();
                        if (lstReportCode.Count <= 0) lstReportCode.Add("thamkham_InDonthuocA4");
                        foreach (string _rcode in lstReportCode)
                        {
                            PhieuTongHopLinhThuoc(v_PrintData, objDPhieuCapphat.NgayNhap,
                                                 _rcode);
                        }
                    }
                }
            }
        }
        public static void InPhieutrathuocthua(TPhieutrathuocthua objPhieutrathuocthua, string sTitleReport,
                                               DateTime NgayIn)
        {
            if (PropertyLib._TrathuocthuaProperties.Kieuin == LoaiphieuIn.Tonghop)
                InPhieutrathuocthuaTonghop(objPhieutrathuocthua, sTitleReport, NgayIn);
            else if (PropertyLib._TrathuocthuaProperties.Kieuin == LoaiphieuIn.Chitiet)
                InPhieutrathuocthuaChitiet(objPhieutrathuocthua, sTitleReport, NgayIn);
            else
            {
                InPhieutrathuocthuaTonghop(objPhieutrathuocthua, sTitleReport, NgayIn);
                InPhieutrathuocthuaChitiet(objPhieutrathuocthua, sTitleReport, NgayIn);
            }
        }

        public static void InPhieutrathuocthuaTonghop(TPhieutrathuocthua objPhieutrathuocthua, string sTitleReport,
                                                      DateTime NgayIn)
        {
            DataTable m_dtReport =
                SPs.ThuocNoitruInTonghopPhieutrathuocthua(objPhieutrathuocthua.Id).GetDataSet().Tables[0];
            if (m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_noitru_phieutrathuocthua");
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("thuoc_noitru_phieutrathuocthua", ref tieude, ref reportname);

            if (crpt == null) return;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thuoc_noitru_phieutrathuocthua";

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void InPhieutrathuocthuaChitiet(TPhieutrathuocthua objPhieutrathuocthua, string sTitleReport,
                                                      DateTime NgayIn)
        {
            DataTable m_dtReport =
                SPs.ThuocNoitruInChitietPhieutrathuocthua(objPhieutrathuocthua.Id).GetDataSet().Tables[0];
            if (m_dtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_noitru_phieutrathuocthua_chitiet");
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("thuoc_noitru_phieutrathuocthua_chitiet", ref tieude, ref reportname);
            if (crpt == null) return;

            Utility.UpdateLogotoDatatable(ref m_dtReport);
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thuoc_noitru_phieutrathuocthua_chitiet";

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void PhieuTongHopLinhThuoc(DataTable m_dtReport, DateTime Ngaylapphieu, string reportcode)
        {
            try
            {
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu để in. Mời bạn kiểm tra lại", "Thông báo",
                                    MessageBoxIcon.Warning);
                    return;
                }
                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
                if (crpt == null) return;
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_noitru_phieutonghopvattunoitru.XML");
                crpt.SetDataSource(m_dtReport);
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(Ngaylapphieu));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        private static DataTable PhieuLinhThuoc(DataTable m_dtreport, string kieuvattuthuoc)
        {
            EnumerableRowCollection<DataRow> query = from thuoc in m_dtreport.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(thuoc[DmucThuoc.Columns.KieuThuocvattu]) ==
                                                         kieuvattuthuoc
                                                     select thuoc;
            if (query.Any()) return query.CopyToDataTable();
            else return null;
        }

        public static void Baocaoxuatvacxintuyehuyen(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                                     DateTime NgayIn, string FromDateToDate)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                crpt.SetDataSource(m_dtReport);


                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NgayIn));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoNhapkhoChitiet(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                                DateTime NgayIn, string FromDateToDate)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                crpt.SetDataSource(m_dtReport);


                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                // Utility.SetParameterValue(crpt,"sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                //  frmPrintPreview objForm = new frmPrintPreview("", crpt, true, strPatientCode == null ? false : true);
                //  Utility.SetParameterValue(crpt,"TongTien", Total.ToString());
                //Utility.SetParameterValue(crpt,"characterMoney", MoneyByLetter.sMoneyToLetter(Total.ToString()));
                //Utility.SetParameterValue(crpt,"TongTien","10000");
                //Utility.SetParameterValue(crpt,"Staff_Name", BusinessHelper.GetNameByUserName(globalVariables.UserName));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaohuythuocChitiet(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                                 DateTime NgayIn, string FromDateToDate)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void Baocaotrathuocnhacungcap(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                                    DateTime NgayIn, string FromDateToDate)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoThanhlythuoc(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                              DateTime NgayIn, string FromDateToDate)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        /// BÁO XUÂT NHẬP TỒN KHO CỦA KHO CHẴN
        /// </summary>
        /// <param name="m_dtReport"></param>
        /// <param name="sTitleReport"></param>
        /// <param name="NgayIn"></param>
        /// <param name="FromDateToDate"></param>
        public static void BaocaoNhapxuattonKhochan(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                                    DateTime NgayIn, string FromDateToDate, string tenkho, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_nhapxuatton_khochan_theonhom";
            ReportDocument crpt = null;
            if (kieuthuoc_vt == "THUOC")
            {
                if (theonhom)
                {
                    reportcode = "thuoc_baocao_nhapxuatton_khochan_theonhom";
                }
                else
                {
                    reportcode = "thuoc_baocao_nhapxuatton_khochan";
                }
            }
            else //VTTH
            {
                if (theonhom)
                {
                    reportcode = "vt_baocao_nhapxuatton_khochan_theonhom";
                }
                else
                {
                    reportcode = "vt_baocao_nhapxuatton_khochan";
                }
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoPhanungthongthuongSautiemchung(DataTable m_dtReport, DateTime NgayIn,
                                                                string FromDateToDate, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "vacxin_baocao_phanungthongthuong_sautiemchung_theonhom";
            ReportDocument crpt = null;

            if (theonhom)
            {
                reportcode = "vacxin_baocao_phanungthongthuong_sautiemchung_theonhom";
            }
            else
            {
                reportcode = "vacxin_baocao_phanungthongthuong_sautiemchung";
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_loaithuoc,tenthuoc_bietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenthuoc_bietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoPhanungnangSautiemchung(DataTable m_dtReport, DateTime NgayIn, string FromDateToDate,
                                                         bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "vacxin_baocao_phanungnang_sautiemchung";
            ReportDocument crpt = null;

            if (theonhom)
            {
                reportcode = "vacxin_baocao_phanungnang_sautiemchung_theonhom";
            }
            else
            {
                reportcode = "vacxin_baocao_phanungnang_sautiemchung";
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_loaithuoc,ten_benhnhan,tenthuoc_bietduoc";
                else
                    m_dtReport.DefaultView.Sort = "ten_benhnhan,tenthuoc_bietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "ten_donvi", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaotinhinhsudungVacxin(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                                     string _tondau, string _toncuoi, DateTime NgayIn,
                                                     string FromDateToDate, string tenkho, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "vacxin_baocao_tinhinhsudung_theonhom";
            ReportDocument crpt = null;

            if (theonhom)
            {
                reportcode = "vacxin_baocao_tinhinhsudung_theonhom";
            }
            else
            {
                reportcode = "vacxin_baocao_tinhinhsudung";
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);
                Utility.SetParameterValue(crpt, "Tondau", _tondau);
                Utility.SetParameterValue(crpt, "Toncuoi", _toncuoi);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoNhapxuattonTheoquy(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                                    string _tondau, string _toncuoi, DateTime NgayIn,
                                                    string FromDateToDate, string tenkho, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_nhapxuatton_theoquy_theonhom";
            ReportDocument crpt = null;
            if (kieuthuoc_vt.Contains("THUOC"))
            {
                if (theonhom)
                {
                    reportcode = "thuoc_baocao_nhapxuatton_theoquy_theonhom";
                }
                else
                {
                    reportcode = "thuoc_baocao_nhapxuatton_theoquy";
                }
            }
            else //VTTH
            {
                if (theonhom)
                {
                    reportcode = "vt_baocao_nhapxuatton_theoquy_theonhom";
                }
                else
                {
                    reportcode = "vt_baocao_nhapxuatton_theoquy";
                }
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            string tinhtong = TinhTong(m_dtReport, "TT_TONCUOI");
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Tondau", _tondau);
                Utility.SetParameterValue(crpt, "Toncuoi", _toncuoi);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "thanhtien_bangchu", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoNhapxuattonTheoKhoanoitru(DataTable m_dtReport, string kieuthuoc_vt,
                                                           string sTitleReport, string _tondau, string _toncuoi,
                                                           DateTime NgayIn, string FromDateToDate, string tenkhoa,
                                                           bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_nhapxuatton_theokhoanoitru_theonhom";
            ReportDocument crpt = null;
            if (kieuthuoc_vt.Contains("THUOC"))
            {
                if (theonhom)
                {
                    reportcode = "thuoc_baocao_nhapxuatton_theokhoanoitru_theonhom";
                }
                else
                {
                    reportcode = "thuoc_baocao_nhapxuatton_theokhoanoitru";
                }
            }
            else //VTTH
            {
                if (theonhom)
                {
                    reportcode = "vt_baocao_nhapxuatton_theokhoanoitru_theonhom";
                }
                else
                {
                    reportcode = "vt_baocao_nhapxuatton_theokhoanoitru";
                }
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            string tinhtong = TinhTong(m_dtReport, "TT_TONCUOI");
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Tondau", _tondau);
                Utility.SetParameterValue(crpt, "Toncuoi", _toncuoi);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkhoa", tenkhoa);
                Utility.SetParameterValue(crpt, "thanhtien_bangchu", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void Baocaohuychot(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                         string dieukienbaocao, bool theonhom)
        {
            string tieude = "", reportname = "", reportcode = "thuoc_baocao_huychot_theonhom";
            ReportDocument crpt = null;
            if (kieuthuoc_vt.Contains("THUOC"))
            {
                if (theonhom)
                {
                    reportcode = "thuoc_baocao_huychot_theonhom";
                }
                else
                {
                    reportcode = "thuoc_baocao_huychot";
                }
            }
            else //VTTH
            {
                if (theonhom)
                {
                    reportcode = "vt_baocao_huychot_theonhom";
                }
                else
                {
                    reportcode = "vt_baocao_huychot";
                }
            }
            crpt = Utility.GetReport(reportcode, ref tieude, ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            objForm.mv_sReportFileName = Path.GetFileName(reportname);
            objForm.mv_sReportCode = reportcode;
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                if (theonhom)
                    m_dtReport.DefaultView.Sort = "stt_hthi_nhom,tenbietduoc";
                else
                    m_dtReport.DefaultView.Sort = "tenbietduoc";
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport.DefaultView);

                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "dieukienbaocao", dieukienbaocao);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txttrinhky", "");
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void BaocaoNhapxuattonKhochanTheonhom(DataTable m_dtReport, string sTitleReport, DateTime NgayIn,
                                                            string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("CRPT_BAOCAO_SLUONG_NHAPXUAT_TON_KHOCHAN_NHOM_LOAI", ref tieude,
                                                    ref reportname);
            if (crpt == null) return;


            var _moneyByLetter = new MoneyByLetter();
            // VNS.HIS.UI.BaoCao.PhieuBaoCao.CRPT_BAOCAO_CHITIET_NHAPKHO crpt = new CRPT_BAOCAO_CHITIET_NHAPKHO();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            //string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "CRPT_BAOCAO_SLUONG_NHAPXUAT_TON_KHOCHAN_NHOM_LOAI";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        ///  hàm thực hiện vie báo cáo xuất nhập tồn kho lẻ
        /// </summary>
        /// <param name="m_dtReport"></param>
        /// <param name="sTitleReport"></param>
        /// <param name="NgayIn"></param>
        /// <param name="FromDateToDate"></param>
        /// <param name="tenkho"></param>
        public static void BaocaoNhapxuattonKhole(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                                  DateTime NgayIn, string FromDateToDate, string tenkho, bool Nhomthuoc)
        {
            string tieude = "", reportname = "", reportcode = "";
            ReportDocument crpt = null;
            if (kieuthuoc_vt.Contains("THUOC"))
            {
                if (Nhomthuoc)
                {
                    reportcode = "thuoc_baocao_nhapxuattonkhole_theonhom";
                    crpt = Utility.GetReport("thuoc_baocao_nhapxuattonkhole_theonhom", ref tieude, ref reportname);
                }
                else
                {
                    reportcode = "thuoc_baocao_nhapxuattonkhole";
                    crpt = Utility.GetReport("thuoc_baocao_nhapxuattonkhole", ref tieude, ref reportname);
                }
            }
            else
            {
                if (Nhomthuoc)
                {
                    reportcode = "vt_baocao_nhapxuattonkhole_theonhom";
                    crpt = Utility.GetReport("vt_baocao_nhapxuattonkhole_theonhom", ref tieude, ref reportname);
                }
                else
                {
                    reportcode = "vt_baocao_nhapxuattonkhole";
                    crpt = Utility.GetReport("vt_baocao_nhapxuattonkhole", ref tieude, ref reportname);
                }
            }
            if (crpt == null) return;
            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void ThuocSovatlieuchitiet(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                                 DateTime NgayIn, string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt =
                Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_sovatlieu_chitiet" : "thuoc_sovatlieu_chitiet",
                                  ref tieude, ref reportname);
            if (crpt == null) return;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_sovatlieu_chitiet" : "thuoc_sovatlieu_chitiet";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        public static void ThuocBaocaophatsinh(DataTable m_dtReport, string ma_baocao, string sTitleReport,
                                               DateTime NgayIn, string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(ma_baocao, ref tieude, ref reportname);
            if (crpt == null) return;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = ma_baocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        public static void ThethuocChitiet(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                           DateTime NgayIn, string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt =
                Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_chitiet" : "vt_thevt_chitiet", ref tieude,
                                  ref reportname);
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_thethuoc_chitiet.XML");
            if (crpt == null) return;

            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_chitiet" : "vt_thevt_chitiet";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                        Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void Thethuoc(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport, DateTime NgayIn,
                                    string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc" : "vt_thevt", ref tieude,
                                                    ref reportname);
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_thethuoc.XML");
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc" : "vt_thevt";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                      Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void ThethuocThang(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport, DateTime NgayIn,
                                  string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_thang" : "vt_thevt_thang", ref tieude,
                                                    ref reportname);
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_thethuoc_thang.XML");
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_thang" : "vt_thevt_thang";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                      Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void Thethuockhochan(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                           DateTime NgayIn, string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt =
                Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_khochan" : "vt_thevt_khochan", ref tieude,
                                  ref reportname);
            if (crpt == null) return;

            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_khochan" : "vt_thevt_khochan";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void Bienbankiemkethuoc(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport, 
            DateTime NgayIn, string FromDateToDate, string tenkho, int songayhethan)
        {
              string tieude = "", reportname = "";
            ReportDocument crpt =
                Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_bienban_kiemkethuoc" : "thuoc_bienban_kiemkevattu", ref tieude,
                                  ref reportname);
            if (crpt == null) return;


            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportname;
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "ReportCondition", FromDateToDate);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "DayOfWarning", songayhethan);
                Utility.SetParameterValue(crpt, "ngay_in", NgayIn.ToString("dd/MM/yyyy"));
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
        public static void Thethuoctutruc(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport,
                                          DateTime NgayIn, string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt =
                Utility.GetReport(kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_tutruc" : "vt_thevt_tutruc", ref tieude,
                                  ref reportname);
            if (crpt == null) return;


            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_tutruc" : "vt_thevt_tutruc";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void ThethuocKhole(DataTable m_dtReport, string kieuthuoc_vt, string sTitleReport, DateTime NgayIn,
                                         string FromDateToDate, string tenkho)
        {
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(
                kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_khole" : "vt_thevt_khole", ref tieude, ref reportname);
            if (crpt == null) return;


            var _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = kieuthuoc_vt == "THUOC" ? "thuoc_thethuoc_khole" : "vt_thevt_khole";
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                //Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "tenkho", tenkho);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void baocao_nhapthuoctheo_nhacungcap(DataTable m_dtReport, string tenbaocao, string sTitleReport,
                                                           DateTime NgayIn, string FromDateToDate)
        {
            var _moneyByLetter = new MoneyByLetter();
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport(tenbaocao, ref tieude, ref reportname);
            if (crpt == null) return;

            //  VNS.HIS.UI.BaoCao.Reports.CRPT_PHIEU_BIENBAN_BANGIAO_HOADON crpt = new CRPT_PHIEU_BIENBAN_BANGIAO_HOADON();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport, "thanhtien");
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);

                objForm.mv_sReportCode = tenbaocao;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);

                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "Department_Name", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "TongSoTien", tinhtong);

                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        public static void Baocaothuochethan(DataTable m_dtReport, string sTitleReport, DateTime NgayIn,
                                             string FromDateToDate)
        {
            var _moneyByLetter = new MoneyByLetter();
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("thuoc_baocao_thuochethan", ref tieude, ref reportname);
            if (crpt == null) return;
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            string tinhtong = TinhTong(m_dtReport, "thanhtien");
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thuoc_baocao_thuochethan";


                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "FromDateToDate", FromDateToDate);
                Utility.SetParameterValue(crpt, "KhoaPhong", globalVariables.KhoaDuoc);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                Utility.SetParameterValue(crpt, "TongSoTien", tinhtong);

                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                // Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private static string TinhTong(DataTable dataTable)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName)));
            return sumoftotal;
        }

        private static string TinhTong(DataTable dataTable, string ColName)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(ColName)));
            return sumoftotal;
        }
    }
}