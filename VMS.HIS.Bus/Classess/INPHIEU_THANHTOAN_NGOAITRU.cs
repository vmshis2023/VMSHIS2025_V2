using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using NLog;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using System.IO;
namespace VNS.HIS.Classes
{
    /// <summary>
    /// 05-11-2013
    ///  </summary>
   public class INPHIEU_THANHTOAN_NGOAITRU
    {
       private Logger log;
       DateTime NGAYINPHIEU;
       private  decimal SumOfTotal(DataTable dataTable)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(TONG_BN)+sum(PHU_THU)", "1=1"), 0);
       }
       public INPHIEU_THANHTOAN_NGOAITRU(DateTime NGAYINPHIEU)
       {
           this.NGAYINPHIEU = NGAYINPHIEU;
       }
       public INPHIEU_THANHTOAN_NGOAITRU(DateTime NGAYINPHIEU, Logger log)
       {
           this.NGAYINPHIEU = NGAYINPHIEU;
           this.log = log;
       }
       public INPHIEU_THANHTOAN_NGOAITRU(Logger log)
       {
           this.log = log;
       }
       public INPHIEU_THANHTOAN_NGOAITRU()
       {
       }
       
       private decimal TinhTongBienLai(DataTable dataTable)
       {

           decimal tong =Utility.getSUM( dataTable,"tongtien_bnhan");
           return tong;
       }
       public void INBIENLAI_QUAYTHUOC(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "",reportcode="";
           switch (khogiay)
           {
               case "A4":
                   reportcode = "quaythuoc_bienlaithanhtoan_A4";
                   reportDocument = Utility.GetReport("quaythuoc_bienlaithanhtoan_A4", ref tieude, ref reportname);
                   break;
               case "A5":
                   reportcode = "quaythuoc_bienlaithanhtoan_A5";
                   reportDocument = Utility.GetReport("quaythuoc_bienlaithanhtoan_A5", ref tieude, ref reportname);
                   break;

           }
           if (reportDocument == null) return;
           var crpt = reportDocument;
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportcode;
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);

           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);

           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }

     
       /// <summary>
       /// hàm thực hiện việc in phiếu dịch vụ
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_crpt_PhieuDV_A5",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_crpt_PhieuDV_A5";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu).ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
       }
       /// <summary>
       /// hàm thưc hiện việc in phiếu thông tin biên lai của bhyt cho bệnh nhân
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_BIENLAI_BHYT_CHO_BN(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_crpt_PhieuThu_BHYT_Cho_BN_A5",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_crpt_PhieuThu_BHYT_Cho_BN_A5";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu).ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
       }
       /// <summary>
       /// hàm thưc hiện viêc in thông tin của phiếu thu đồng chi trả
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="sTitleReport"></param>
       public void INPHIEU_DONGCHITRA(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_PHIEUTHU_DONGCHITRA",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "thanhtoan_PHIEUTHU_DONGCHITRA";
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyLetter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu,"SO_TIEN").ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       private  MoneyByLetter sMoneyByLetter = new MoneyByLetter();
       /// <summary>
       /// hàm thực hiện in phiếu bảo hiểm đúng tuyến
       /// </summary>
       /// <param name="sTitleReport"></param>
       public void LAOKHOA_INPHIEU_BAOHIEM_NGOAITRU(DataTable m_dtReportPhieuThu, string sTitleReport,DateTime ngayIn)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "THU_TU ASC";
           m_dtReportPhieuThu.AcceptChanges();
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("BHYT_InPhoi",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = "BHYT_InPhoi";
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           // Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           // Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(ngayIn));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "THANH_TIEN").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BH",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "BHCT").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_BN",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "BNCT").ToString()));
           Utility.SetParameterValue(crpt,"sMoneyCharacter_Thanhtien_Khac",
                                  sMoneyByLetter.sMoneyToLetter(
                                      SumOfTotal_BH(m_dtReportPhieuThu, "PHU_THU").ToString()));
           objForm.crptViewer.ReportSource = crpt;
           objForm.addTrinhKy_OnFormLoad();
           PrintDialog frmPrint = new PrintDialog();
           if(frmPrint.ShowDialog() == DialogResult.OK)
           {
               crpt.PrintOptions.PrinterName = frmPrint.PrinterSettings.PrinterName;
               crpt.PrintToPrinter(frmPrint.PrinterSettings.Copies,frmPrint.PrinterSettings.Collate,frmPrint.PrinterSettings.FromPage,frmPrint.PrinterSettings.ToPage);             
           }
           //objForm.ShowDialog();
          
           //}
           //catch (Exception ex)
           //{
           //   
           //}
       }

      
       private  decimal SumOfTotal(DataTable dataTable, string FiledName)
       {
           return Utility.getSUM(dataTable, FiledName);
       }
       public void In_BangkeCPKCB_Noitru(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay, byte noitru)
       {
           try
           {
               Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
               m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
               m_dtReportPhieuThu.AcceptChanges();
               var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                        group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                        select new
                        {
                            _key = r.Key,
                            tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                            tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                            tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                        }).ToList();

               decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
               decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
               decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
               tong = tong - tong_ck;
               ReportDocument reportDocument = new ReportDocument();
               string tieude = "", reportname = "", reportCode = "";

               if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
               {
                   reportCode = Utility.Byte2Bool(noitru) ? "thanhtoan_bangkechiphiKCB_Noitru" : "thanhtoan_bangkechiphiKCB_Ngoaitru";
                   reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
               }
               else
               {
                   switch (khogiay)
                   {
                       case "A4":
                           reportCode = tong_ck <= 0 ? "thanhtoan_bangkechiphiKCB_Noitru" : "thanhtoan_bangkechiphiKCB_Noitru";
                           reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                           break;
                       case "A5":
                           reportCode = tong_ck <= 0 ? "thanhtoan_bangkechiphiKCB_Noitru" : "thanhtoan_bangkechiphiKCB_Noitru";
                           reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                           break;

                   }
               }
               if (reportDocument == null) return;
               var crpt = reportDocument;


               var objForm = new frmPrintPreview("", crpt, true, true);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = reportCode;
               //try
               //{
               crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
               //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
               Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
               Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
               Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
               Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
               Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
               Utility.SetParameterValue(crpt, "tongtien_bn", tong);
               //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
               Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(NgayInPhieu, globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithLocation(NgayInPhieu, globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(crpt, "sTitleReport", tieude);
               Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                      new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
               Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
               objForm.crptViewer.ReportSource = crpt;
               if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
                   objForm.ShowDialog();

               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
                   crpt.PrintToPrinter(1, false, 0, 0);
               }
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
           }
         
       }
       public void INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, KcbThanhtoan objThanhtoan, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_hthi_khoaphong,stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
           m_dtReportPhieuThu.AcceptChanges();
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           ReportDocument crpt = new ReportDocument();
           string tieude = "", reportname = "", reportCode = "";
           DataRow[] arrKCB = m_dtReportPhieuThu.Select("id_loaithanhtoan=1");
           DataRow[] arrOther = m_dtReportPhieuThu.Select("id_loaithanhtoan<>1");
            bool isBHYT = THU_VIEN_CHUNG.IsBaoHiem(Utility.ByteDbnull(m_dtReportPhieuThu.Rows[0]["id_loaidoituong_kcb"]));

            if (!isBHYT)
            {
                if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
                {
                    reportCode = "thanhtoan_Bienlai_Dichvu_A4_Innhiet";
                    crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                }
                else
                {
                    switch (khogiay)
                    {
                        case "A4":
                            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A4" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A4";
                            crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                            break;
                        case "A5":
                            reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A5" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A5";
                            crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                            break;

                    }
                }

                if (arrKCB.Length > 0 && arrOther.Length <= 0)//In biên lai thanh toán dịch vụ KCB có kèm số QMS và Tên phòng khám
                {
                    reportCode = "thanhtoan_Bienlai_KCB_A4";
                    crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                }
                if (Utility.Bool2Bool(objThanhtoan.TtoanThuoc))
                {

                    reportCode = "thanhtoan_Bienlai_BanthuoctaiQuay";
                    if (m_dtReportPhieuThu.Select("id_loaithanhtoan=5").Length>0)
                        reportCode = "thanhtoan_Bienlai_BanthuoctaiQuay_VT";
                    crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                }
                //Biên lai tạm thu
                if (objThanhtoan.KieuThanhtoan == 5)
                {
                    reportCode = "thanhtoan_Bienlai_Tamthu_Dichvu_A4";
                   
                    crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
                }
            }
            else
            {
                if (arrKCB.Length > 0 && arrOther.Length <= 0)//In biên lai thanh toán dịch vụ KCB có kèm số QMS và Tên phòng khám
                {
                    reportCode = "thanhtoan_Bienlai_KCB_A4_BHYT";
                }
                else
                    reportCode = "thanhtoan_Bienlai_Dichvu_A4_BHYT";
                crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
            }
            if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportCode;
           objForm.nguoi_thuchien = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_tnv"], "");
           objForm.ten_benhnhan = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_benhnhan"], "");
           objForm.NGAY = objThanhtoan.NgayThanhtoan;
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(objThanhtoan.NgayThanhtoan));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(objThanhtoan.NgayThanhtoan));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();

           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       /// <summary>
       /// HÀM THỰC HIỆN VIỆC IN PHIẾU BIÊN LAI CHO BẢO HIỂM Y TẾ
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="sTitleReport"></param>
       /// <param name="ngayIn"></param>
       public void INPHIEU_BIENLAI_BHYT(DataTable m_dtReportPhieuThu, DateTime ngayIn, string khogiay)
       {

           if (m_dtReportPhieuThu.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           // THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in ASC";
           m_dtReportPhieuThu.AcceptChanges();
           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           string tieude = "", reportname = "", reportcode = "thanhtoan_Bienlai_BHYT_A4_new";
           ReportDocument crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4_new", ref tieude, ref reportname);
           switch (khogiay)
           {
               case "A4":
                   reportcode = "thanhtoan_Bienlai_BHYT_A4_new";
                   crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4_new", ref tieude, ref reportname);
                   break;
               case "A5":
                   reportcode = "thanhtoan_Bienlai_BHYT_A5_new";
                   crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A5_new", ref tieude, ref reportname);
                   break;

           }
           if (crpt == null) return;
           var objForm = new frmPrintPreview(tieude, crpt, true, true);
           //try
           //{
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();


           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportcode;
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);


           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(DateTime.Now));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;

           if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       /// <summary>
       /// Back up bản trước khi in phiếu xuất thuốc thay cho phiếu biên lai bán thuốc tại quầy
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="NgayInPhieu"></param>
       /// <param name="khogiay"></param>
       public void INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_hthi_khoaphong,stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
           m_dtReportPhieuThu.AcceptChanges();
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "", reportCode = "";
           DataRow[] arrKCB = m_dtReportPhieuThu.Select("id_loaithanhtoan=1");
           DataRow[] arrOther = m_dtReportPhieuThu.Select("id_loaithanhtoan<>1");
           
           if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
           {
               reportCode = "thanhtoan_Bienlai_Dichvu_A4_Innhiet" ;
               reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
           }
           else
           {
               switch (khogiay)
               {
                   case "A4":
                       reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A4" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A4";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;
                   case "A5":
                       reportCode = tong_ck <= 0 ? "thanhtoan_Bienlai_Dichvu_A5" : "thanhtoan_Bienlai_Dichvu_Comiengiam_A5";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;

               }
           }
           if (arrKCB.Length > 0 && arrOther.Length <= 0)//In biên lai thanh toán dịch vụ KCB có kèm số QMS và Tên phòng khám
           {
               reportCode = "thanhtoan_Bienlai_KCB_A4";
               reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
           }
           if (reportDocument == null) return;
           var crpt = reportDocument;
           
          
           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportCode;
           objForm.nguoi_thuchien = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_tnv"], "");
           objForm.ten_benhnhan = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_benhnhan"], "");
           objForm.NGAY = NgayInPhieu;
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "DIADIEM",  globalVariables.gv_strDiadiem);
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet?PropertyLib._MayInProperties.TenMayInBienlai_Nhiet: PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();
           
           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       public void INPHIEU_DICHVU_PHIEUCHI(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
           m_dtReportPhieuThu.AcceptChanges();
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "", reportCode = "";

           if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
           {
               reportCode = "thanhtoan_bienlaiphieuchi";
               reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
           }
           else
           {
               switch (khogiay)
               {
                   case "A4":
                       reportCode = tong_ck <= 0 ? "thanhtoan_bienlaiphieuchi" : "thanhtoan_bienlaiphieuchi";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;
                   case "A5":
                       reportCode = tong_ck <= 0 ? "thanhtoan_bienlaiphieuchi" : "thanhtoan_bienlaiphieuchi";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;

               }
           }
           if (reportDocument == null) return;
           var crpt = reportDocument;


           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportCode;
           objForm.nguoi_thuchien = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_tnv"], "");
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);
           Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithLocation(NgayInPhieu, globalVariables.gv_strDiadiem));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();

           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       public void INPHIEU_NHANTHUOCTRALAI_TAIQUAY(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
           m_dtReportPhieuThu.AcceptChanges();
           var p = (from q in m_dtReportPhieuThu.AsEnumerable()
                    group q by q.Field<long>(KcbThanhtoan.Columns.IdThanhtoan) into r
                    select new
                    {
                        _key = r.Key,
                        tongtien_chietkhau_hoadon = r.Min(g => g.Field<decimal>("tongtien_chietkhau_hoadon")),
                        tongtien_chietkhau_chitiet = r.Min(g => g.Field<decimal>("tongtien_chietkhau_chitiet")),
                        tongtien_chietkhau = r.Min(g => g.Field<decimal>("tongtien_chietkhau"))
                    }).ToList();

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TT_BN");
           decimal tong_ck_hoadon = p.Sum(c => c.tongtien_chietkhau_hoadon);
           decimal tong_ck = p.Sum(c => c.tongtien_chietkhau);
           tong = tong - tong_ck;
           ReportDocument reportDocument = new ReportDocument();
           string tieude = "", reportname = "", reportCode = "";

           if (PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet)
           {
               reportCode = "thuoc_phieuxacnhan_nhanthuoctralaitaiquay";
               reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
           }
           else
           {
               switch (khogiay)
               {
                   case "A4":
                       reportCode = tong_ck <= 0 ? "thuoc_phieuxacnhan_nhanthuoctralaitaiquay" : "thuoc_phieuxacnhan_nhanthuoctralaitaiquay";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;
                   case "A5":
                       reportCode = tong_ck <= 0 ? "thuoc_phieuxacnhan_nhanthuoctralaitaiquay" : "thuoc_phieuxacnhan_nhanthuoctralaitaiquay";
                       reportDocument = Utility.GetReport(reportCode, ref tieude, ref reportname);
                       break;

               }
           }
           if (reportDocument == null) return;
           var crpt = reportDocument;


           var objForm = new frmPrintPreview("", crpt, true, true);
           objForm.mv_sReportFileName = Path.GetFileName(reportname);
           objForm.mv_sReportCode = reportCode;
           objForm.nguoi_thuchien = Utility.sDbnull(m_dtReportPhieuThu.Rows[0]["ten_tnv"], "");
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt, "Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
           Utility.SetParameterValue(crpt, "tienmiengiam_hdon", tong_ck_hoadon);
           Utility.SetParameterValue(crpt, "tong_miengiam", tong_ck);
           Utility.SetParameterValue(crpt, "tongtien_bn", tong);
           Utility.SetParameterValue(crpt, "DIADIEM", globalVariables.gv_strDiadiem);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithLocation(NgayInPhieu, globalVariables.gv_strDiadiem));
           Utility.SetParameterValue(crpt, "sTitleReport", tieude);
           Utility.SetParameterValue(crpt, "sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, NGAYINPHIEU));
           objForm.crptViewer.ReportSource = crpt;
           if (Utility.isPrintPreview(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInBienlai))
           {
               objForm.SetDefaultPrinter(PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai, 0);
               objForm.ShowDialog();

           }
           else
           {
               objForm.addTrinhKy_OnFormLoad();
               crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.KieuInBienlai == KieuIn.Innhiet ? PropertyLib._MayInProperties.TenMayInBienlai_Nhiet : PropertyLib._MayInProperties.TenMayInBienlai;
               crpt.PrintToPrinter(1, false, 0, 0);
           }
       }
       MoneyByLetter _moneyByLetter=new MoneyByLetter();
       
       private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
       }
       public void Inbienlai_DichvuChuathanhtoan(DataTable mDtReportPhieuThu, bool IsTongHop, byte noitru)
       {
           try
           {
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_hthi_khoaphong,stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV_chuathanhtoan.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                   return;
               }
               INPHIEU_DICHVU(mDtReportPhieuThu, DateTime.Now, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
       public void InBienlai(bool IsTongHop, long id_thanhtoan,long id_donthuoc, KcbLuotkham objLuotkham,byte noitru)
       {
          
           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(id_thanhtoan);
               if (actionResult == ActionResult.Success)
               {
                   switch (objLuotkham.MaDoituongKcb)
                   {
                       case "DV":
                           Inbienlai_Dichvu(id_thanhtoan, id_donthuoc, IsTongHop, noitru);
                           break;
                       //case "BHYT":
                       //    Inbienlai_BHYT(id_thanhtoan, IsTongHop, noitru);
                       //    break;
                       default:
                           Inbienlai_Dichvu(id_thanhtoan, id_donthuoc, IsTongHop, noitru);
                           break;
                   }
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));
              
           }
           finally
           {
              
           }

       }
       public void InBienlaiPhieuChiTralaiThuoc(bool IsTongHop, long id_thanhtoan,long id_donthuoc, KcbLuotkham objLuotkham, byte noitru)
       {

           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(id_thanhtoan);
               if (actionResult == ActionResult.Success)
               {
                   switch (objLuotkham.MaDoituongKcb)
                   {
                       case "DV":
                           Inbienlai_Dichvu_Phieuchi_TraLaithuoc(id_thanhtoan, id_donthuoc, IsTongHop, noitru);
                           break;
                       //case "BHYT":
                       //    Inbienlai_Dichvu_Phieuchi_TraLaithuoc(id_thanhtoan, IsTongHop, noitru);
                       //    break;
                       default:
                           Inbienlai_Dichvu_Phieuchi_TraLaithuoc(id_thanhtoan, id_donthuoc, IsTongHop, noitru);
                           break;
                   }
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));

           }
           finally
           {

           }

       }
       public void InBienlaiPhieuChi(bool IsTongHop, long id_thanhtoan, KcbLuotkham objLuotkham, byte noitru)
       {

           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(id_thanhtoan);
               if (actionResult == ActionResult.Success)
               {
                   switch (objLuotkham.MaDoituongKcb)
                   {
                       case "DV":
                           Inbienlai_Dichvu_Phieuchi(id_thanhtoan, IsTongHop, noitru);
                           break;
                       //case "BHYT":
                       //    Inbienlai_BHYT(id_thanhtoan, IsTongHop, noitru);
                       //    break;
                       default:
                           Inbienlai_Dichvu_Phieuchi(id_thanhtoan, IsTongHop, noitru);
                           break;
                   }
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));

           }
           finally
           {

           }

       }
       public void InBienlaiQuaythuoc(bool IsTongHop, int id_thanhtoan, long id_donthuoc)
       {

           try
           {
               ActionResult actionResult = new KCB_THANHTOAN().Capnhattrangthaithanhtoan(id_thanhtoan);
               if (actionResult == ActionResult.Success)
               {

                   Inbienlai_quaythuoc(id_thanhtoan, id_donthuoc, IsTongHop);
                           
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg(string.Format("Lỗi trong quá trình in phiếu dịch vụ ={0}", ex.ToString()));

           }
           finally
           {

           }
       }
       public void In_Bangke_CPKCB(long payment_id, bool IsTongHop, byte noitru)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
               DataTable mDtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment,-1, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_loaidichvu,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                   return;
               }
               In_BangkeCPKCB_Noitru(mDtReportPhieuThu, objPayment.NgayThanhtoan, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5", noitru);

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
       void Inbienlai_Dichvu(long payment_id, long id_donthuoc, bool IsTongHop, byte noitru)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
               DataTable mDtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment, id_donthuoc, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_hthi_khoaphong,stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (Kcb_Thanhtoan_Laythongtin_Inbienlai_Dv_2023)", "Thông báo");
                   return;
               }
               // Utility.CreateBarcodeData(ref mDtReportPhieuThu, objPayment.MaLuotkham);
               INPHIEU_DICHVU(mDtReportPhieuThu, objPayment, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
       private void Inbienlai_BHYT(long payment_id, bool IsTongHop, byte noitru)
       {
           try
           {

               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
               DataTable m_dtReportPhieuThu =
                    new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment,-1, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, true);
               m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
               if (m_dtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                   return;
               }
               //hàm thực hiện xử lý thôgn tin của phiếu dịch vụ
               INPHIEU_BIENLAI_BHYT(m_dtReportPhieuThu, objPayment.NgayThanhtoan, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A5 ? "A5" : "A4");

           }
           catch (Exception ex)
           {
               Utility.ShowMsg("Lỗi:" + ex.Message);
               log.Trace(ex.Message);
           }

       }
       void Inbienlai_Dichvu_Phieuchi_TraLaithuoc(long payment_id,long id_donthuoc, bool IsTongHop, byte noitru)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
               DataTable mDtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu_PhieuChi_Quaythuoc(objPayment, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (Kcb_Thanhtoan_Laythongtin_Inbienlai_Dv_2023)", "Thông báo");
                   return;
               }
               // Utility.CreateBarcodeData(ref mDtReportPhieuThu, objPayment.MaLuotkham);
               INPHIEU_DICHVU_PHIEUCHI(mDtReportPhieuThu, objPayment.NgayThanhtoan, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
      public void In_Phieuchi_TraLaithuoc(long id_tralaithuoc)
       {
           try
           {
               ThuocLichsuTralaithuoctaiquayPhieu objPhieu = ThuocLichsuTralaithuoctaiquayPhieu.FetchByID(id_tralaithuoc);
               ///lấy thông tin vào phiếu thu
               DataTable mDtReportPhieuThu = new KCB_THANHTOAN().ThuocLaydulieuinphieutrathuoctaiquay(objPhieu);
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (Kcb_Thanhtoan_Laythongtin_Inbienlai_Dv_2023)", "Thông báo");
                   return;
               }
               // Utility.CreateBarcodeData(ref mDtReportPhieuThu, objPayment.MaLuotkham);
               INPHIEU_NHANTHUOCTRALAI_TAIQUAY(mDtReportPhieuThu, objPhieu.NgayTao, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
       void Inbienlai_Dichvu_Phieuchi(long payment_id, bool IsTongHop, byte noitru)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
               if (IsTongHop) objPayment.IdThanhtoan = -1;
               ///lấy thông tin vào phiếu thu
               DataTable mDtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu_phieuchi(objPayment, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref mDtReportPhieuThu, false);
               mDtReportPhieuThu.DefaultView.Sort = "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

               THU_VIEN_CHUNG.CreateXML(mDtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
               if (mDtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                   return;
               }
               // Utility.CreateBarcodeData(ref mDtReportPhieuThu, objPayment.MaLuotkham);
               INPHIEU_DICHVU_PHIEUCHI(mDtReportPhieuThu, objPayment.NgayThanhtoan, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

           }
           catch (Exception exception)
           {
               log.Trace(exception.Message);
           }
       }
        void Inbienlai_quaythuoc(long payment_id, long id_donthuoc, bool IsTongHop)
        {
            try
            {
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(payment_id);
                if (IsTongHop) objPayment.IdThanhtoan = -1;
                ///lấy thông tin vào phiếu thu
                DataTable m_dtReportPhieuThu = new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment, id_donthuoc,0);
                THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, false);
                m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";

                THU_VIEN_CHUNG.CreateXML(m_dtReportPhieuThu, Application.StartupPath + @"\Xml4Reports\Thanhtoan_InBienLai_DV.XML");
                if (m_dtReportPhieuThu.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu in phiếu (KCB_THANHTOAN_LAYTHONGTIN_INPHIEU_DICHVU)", "Thông báo");
                    return;
                }
                INBIENLAI_QUAYTHUOC(m_dtReportPhieuThu, objPayment.NgayThanhtoan, PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5");

            }
            catch (Exception exception)
            {
                log.Trace(exception.Message);
            }
        }
      

       public void InHoaDon_BanHang(long  paymentID,long id_donthuoc,byte noitru)
       {
           try
           {
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(paymentID);
               DataTable m_dtReportPhieuThu =
                      new KCB_THANHTOAN().LaythongtininbienlaiDichvu(objPayment, id_donthuoc, noitru);
               THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu, true);
               m_dtReportPhieuThu.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
               if (m_dtReportPhieuThu.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                   return;
               }

           }
           catch (Exception ex)
           {
               Utility.ShowMsg("Lỗi:" + ex.Message);
               log.Trace(ex.Message);
           }

       }
       private  void InPhieuHoaDonBanHang(DataTable m_dtReportPhieuThu,DateTime ngayIn, string khogiay)
       {
        
       }
       public void IN_HOADON(long paymentID)
       {
           string LyDoIn = "0";
           try
           {
               DataTable dtPatientPayment = new KCB_THANHTOAN().Laythongtinhoadondo(paymentID);
               THU_VIEN_CHUNG.CreateXML(dtPatientPayment, "thanhtoan_Hoadondo.xml");
               Utility.UpdateLogotoDatatable(ref dtPatientPayment);
               dtPatientPayment.Rows[0]["sotien_bangchu"] =
                   new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["TONG_TIEN"]));
               string tieude = "", reportname = "";
               ReportDocument report = Utility.GetReport("thanhtoan_Hoadondo", ref tieude, ref reportname);
               if (report == null) return;
               KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(paymentID);
               if (objPayment == null) {
                   Utility.ShowMsg("Thanh toán không tồn tại(Do người khác đã hủy trong quá trình bạn thao tác). Vui lòng chọn lại bệnh nhân để kiểm tra");
                   return; }
               var objForm = new frmPrintPreview("", report, true, true);
               //objForm.AutoClose = true;
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.nguoi_thuchien = Utility.sDbnull(dtPatientPayment.Rows[0]["ten_tnv"], "");
               objForm.mv_sReportCode = "thanhtoan_Hoadondo";
               objForm.NGAY = objPayment.NgayThanhtoan;
               report.SetDataSource(dtPatientPayment);
               Utility.SetParameterValue(report,"NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));

               Utility.SetParameterValue(report,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(report,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(report, "DateTime", Utility.FormatDateTime(Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"])));
               Utility.SetParameterValue(report, "CurrentDate", Utility.FormatDateTimeWithLocation(Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"]), globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(report, "sCurrentDate", Utility.FormatDateTimeWithLocation(Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"]), globalVariables.gv_strDiadiem));
               Utility.SetParameterValue(report, "sTitleReport", tieude);
              // Utility.SetParameterValue(report, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName,DateTime.Now));
               Utility.SetParameterValue(report, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, Convert.ToDateTime(dtPatientPayment.Rows[0]["ngay_thanhtoan"])));
               objForm.crptViewer.ReportSource = report;
              
               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                   objForm.ShowDialog();
               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                   report.PrintToPrinter(1, false, 0, 0);
               }
              
           }
           catch (Exception ex1)
           {
               Utility.CatchException(ex1);
               log.Trace(ex1.Message);
           }

       }
       public void InPhieuthhuchikhac(long idphieuthu,string reportcode)
       {
           string LyDoIn = "0";
           try
           {
               KcbPhieuthu objPhieuthu = KcbPhieuthu.FetchByID(idphieuthu);
               if (objPhieuthu == null)
               {
                   Utility.ShowMsg("Phiếu thu bạn chọn in không tồn tại. Đề nghị chọn lại người bệnh để nạp lại dữ liệu");
                   return;
               }
               DataTable dtPatientPayment = new KCB_THANHTOAN().KcbThanhtoanLaydulieuthuchikhac(idphieuthu);
               Utility.UpdateLogotoDatatable(ref dtPatientPayment);
               THU_VIEN_CHUNG.CreateXML(dtPatientPayment, "thanhtoan_phieuthuchikhac.xml");
               dtPatientPayment.Rows[0]["sotien_bangchu"] =
                   new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["SO_TIEN"]));
               string tieude = "", reportname = "";
               ReportDocument report = Utility.GetReport(reportcode, ref tieude, ref reportname);
               if (report == null) return;
               var objForm = new frmPrintPreview("", report, true, true);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = reportcode;
               report.SetDataSource(dtPatientPayment);
               Utility.SetParameterValue(report, "ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(report, "BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(report, "Telephone", globalVariables.Branch_Phone);
               Utility.SetParameterValue(report, "Address", globalVariables.Branch_Address);
               Utility.SetParameterValue(report, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
               //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
               Utility.SetParameterValue(report, "DIADIEM", globalVariables.gv_strDiadiem);
               Utility.SetParameterValue(report, "NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));
               Utility.SetParameterValue(report, "DateTime", Utility.FormatDateTime(DateTime.Now));
               Utility.SetParameterValue(report, "sCurrentDate", Utility.FormatDateTime(objPhieuthu.NgayThuchien));
               Utility.SetParameterValue(report, "sTitleReport", tieude);
               //Utility.SetParameterValue(report,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
               objForm.crptViewer.ReportSource = report;

               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                   objForm.ShowDialog();
               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                   report.PrintToPrinter(1, false, 0, 0);
               }
           }
           catch (Exception ex1)
           {
               Utility.ShowMsg("Lỗi khi thực hiện in hóa đơn mẫu. Liên hệ IT để được trợ giúp-->" +
                               ex1.Message);
               log.Trace(ex1.Message);
           }

       }
       public void InPhieuchi(long payment_ID)
       {
           string LyDoIn = "0";
           try
           {
               KcbPhieuthu objPhieuthu = new Select().From(KcbPhieuthu.Schema).Where(KcbPhieuthu.Columns.IdThanhtoan).IsEqualTo(payment_ID).ExecuteSingle<KcbPhieuthu>();//  KcbPhieuthu.FetchByID(payment_ID);
               if (objPhieuthu == null)
               {
                   Utility.ShowMsg("Phiếu thu bạn chọn in không tồn tại. Đề nghị chọn lại người bệnh để nạp lại dữ liệu");
                   return;
               }
               DataTable dtPatientPayment = new KCB_THANHTOAN().KcbThanhtoanLaythongtinphieuchi(payment_ID);
               Utility.UpdateLogotoDatatable(ref dtPatientPayment);
               THU_VIEN_CHUNG.CreateXML(dtPatientPayment, "thanhtoan_phieuchi.xml");
               dtPatientPayment.Rows[0]["sotien_bangchu"] =
                   new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(dtPatientPayment.Rows[0]["SO_TIEN"]));
               string tieude = "", reportname = "";
               ReportDocument report = Utility.GetReport("thanhtoan_phieuchi", ref tieude, ref reportname);
               if (report == null) return;
               var objForm = new frmPrintPreview("", report, true, true);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thanhtoan_phieuchi";
               report.SetDataSource(dtPatientPayment);
               Utility.SetParameterValue(report, "ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(report, "BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(report, "Telephone", globalVariables.Branch_Phone);
               Utility.SetParameterValue(report, "Address", globalVariables.Branch_Address);
               Utility.SetParameterValue(report, "Contact", string.Format("Điện thoại: {0} Email: {1}", globalVariables.Branch_Phone, globalVariables.Branch_Email));
               //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
               Utility.SetParameterValue(report, "DIADIEM", globalVariables.gv_strDiadiem);
               Utility.SetParameterValue(report, "NGUOIIN", Utility.sDbnull(globalVariables.gv_strTenNhanvien, ""));
               Utility.SetParameterValue(report, "DateTime", Utility.FormatDateTime(DateTime.Now));
               Utility.SetParameterValue(report, "sCurrentDate", Utility.FormatDateTime(objPhieuthu.NgayThuchien));
               Utility.SetParameterValue(report, "sTitleReport", tieude);
               //Utility.SetParameterValue(report,"CharacterMoney", new MoneyByLetter().sMoneyToLetter(TONG_TIEN.ToString()));
               objForm.crptViewer.ReportSource = report;

               if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInHoadon, PropertyLib._MayInProperties.PreviewInHoadon))
               {
                   objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInHoadon, 1);
                   objForm.ShowDialog();
               }
               else
               {
                   objForm.addTrinhKy_OnFormLoad();
                   report.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInHoadon;
                   report.PrintToPrinter(1, false, 0, 0);
               }
           }
           catch (Exception ex1)
           {
               Utility.ShowMsg("Lỗi khi thực hiện in hóa đơn mẫu. Liên hệ IT để được trợ giúp-->" +
                               ex1.Message);
               log.Trace(ex1.Message);
           }

       }
       public void InphieuDCT_Benhnhan(KcbLuotkham objLuotkham, DataTable dtDataPayment)
       {
           //try
           //{
           //    if (objLuotkham != null)
           //    {

                  
           //        ActionResult actionResult = ActionResult.Success;
           //        if (PropertyLib._ThanhtoanProperties.TaodulieuDCTkhiInphieuDCT)
           //        {
           //            KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra(objLuotkham);
           //            actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
           //        }
                   
           //        switch (actionResult)
           //        {
           //            case ActionResult.Success:
           //                DataTable dtData =    SPs.BhytLaydulieuinphieudct(objLuotkham.MaLuotkham,
           //                                     Utility.Int32Dbnull(objLuotkham.IdBenhnhan)).GetDataSet().Tables[
           //                                         0];
           //                if (dtData.Rows.Count <= 0)
           //                {
           //                    Utility.ShowMsg("Không tìm thấy thông tin phiếu đồng chi trả. Bạn cần kiểm tra xem đã in phôi BHYT chưa?", "Thông báo", MessageBoxIcon.Warning);
           //                    return;
           //                }
           //                if(Utility.DecimaltoDbnull( dtData.Compute("SUM(so_tien)","1=1"),0)>0)
           //                new VNS.HIS.Classes.INPHIEU_THANHTOAN_NGOAITRU().
           //                    INPHIEU_DONGCHITRA(dtData, DateTime.Now, "PHIẾU THU ĐỒNG CHI TRẢ");
           //                else
           //                    Utility.ShowMsg("Bệnh nhân này đã được BHYT chi trả 100% nên không cần in phiếu đồng chi trả", "Thông báo lỗi", MessageBoxIcon.Error);
           //                break;
           //            case ActionResult.Error:
           //                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin đồng  chi trả", "Thông báo lỗi", MessageBoxIcon.Error);
           //                break;
           //        }


           //    }

           //}
           //catch (Exception exception)
           //{

           //}
       }

       private void GetChanDoanPhu( string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
       {
           try
           {
               List<string> lstICD = IDC_Phu.Split(',').ToList();
               //DmucBenhCollection _list =
               //    new DmucBenhController().FetchByQuery(
               //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
               // Cách này cho hạn chế phi vào DB 
               foreach (var row in lstICD)
               {
                   var item = (from p in globalVariables.gv_dtDmucBenh.AsEnumerable()
                       where row != null && p[DmucBenh.Columns.MaBenh].Equals(row)
                       select p).FirstOrDefault();
                   if (item != null)
                   {
                       ICD_Name += item["ten_benh"].ToString() + ";";
                       ICD_Code += item["ma_benh"].ToString() + ";";
                   }
               }
               //foreach (DmucBenh _item in _list)
               //{
               //    ICD_Name += _item.TenBenh + ";";
               //    ICD_Code += _item.MaBenh + ";";
               //}
               //_list =
               //    new DmucBenhController().FetchByQuery(
               //        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
               //foreach (DmucBenh _item in _list)
               //{
               //    ICD_Name += _item.TenBenh + ";";
               //    ICD_Code += _item.MaBenh + ";";
               //}
               if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
               if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
           }
           catch
           {
           }
       }
      
       
    }
}
