
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;



namespace VNS.HIS.Classes
{
    /// <summary>
    /// 05-11-2013
    ///  </summary>
   public class INPHIEU_THANHTOAN_NGOAITRU1
    {
       DateTime NGAYINPHIEU;
       private  decimal SumOfTotal(DataTable dataTable)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(TONG_BN)+sum(PHU_THU)", "1=1"), 0);
       }
       public INPHIEU_THANHTOAN_NGOAITRU1(DateTime NGAYINPHIEU)
       {
           this.NGAYINPHIEU = NGAYINPHIEU;
       }
       public INPHIEU_THANHTOAN_NGOAITRU1()
       {
       }
       
       private decimal TinhTongBienLai(DataTable dataTable)
       {

           decimal tong = Utility.getSUM(dataTable, "TONG_BN");
           return tong;
       }
       public  void KYDONG_INPHIEU_DICHVU(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport,string khogiay)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           ReportDocument reportDocument = new ReportDocument();
            string tieude="", reportname = "";
           switch (khogiay)
           {
               case "A4":
                   reportDocument = Utility.GetReport("thanhtoan_Bienlai_Dichvu_A4" ,ref tieude,ref reportname);
                   break;
               case "A5":
                   reportDocument = Utility.GetReport("thanhtoan_13" ,ref tieude,ref reportname);
                   break;

           }

           if (reportDocument == null) return;
           var crpt = reportDocument;

           decimal tong = Utility.getSUM(m_dtReportPhieuThu, "TONG_BN");
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyCharacter",
                                  new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(tong)));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
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
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
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
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
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
       public void LAOKHOA_INPHIEU_DONGCHITRA(DataTable m_dtReportPhieuThu, DateTime NgayInPhieu, string sTitleReport)
       {
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("thanhtoan_PHIEUTHU_DONGCHITRA",ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview("", crpt, true, true);
           //try
           //{
           crpt.SetDataSource(m_dtReportPhieuThu);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                      ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //  Utility.SetParameterValue(crpt,"DateTime", Utility.FormatDateTime(dtCreateDate.Value));
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(NgayInPhieu));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"sMoneyLetter",
                                  new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dtReportPhieuThu,"SO_TIEN").ToString()));
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();
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

           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           // Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           // Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
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
           //    Utility.DefaultNow(this);
           //}
       }

       public void LAOKHOA_INPHIEU_BAOHIEM_NGOAITRU(DataTable m_dtReportPhieuThu, string sTitleReport, DateTime ngayIn, KcbLuotkham objPatientExam)
       {
           
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "THU_TU ASC";
           m_dtReportPhieuThu.AcceptChanges();
            string tieude="", reportname = "";
           var crpt = Utility.GetReport("BHYT_Inphoi" ,ref tieude,ref reportname);
           if (crpt == null) return;
           frmPrintPreview objForm = new frmPrintPreview("", crpt, true, true);
           objForm.NGAY = NGAYINPHIEU;
           //try
           //{

           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           // Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
           // Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
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
           try
           {
               //if (PropertyLib._HISPrintProperties != null)
               //{
               //    if (string.IsNullOrEmpty(PropertyLib._HISPrintProperties.sPrinterNameBHYT))
               //    {
               //        PropertyLib._HISPrintProperties.sPrinterNameBHYT = Utility.GetDefaultPrinter();
               //    }
               //    crpt.PrintOptions.PrinterName = PropertyLib._HISPrintProperties.sPrinterNameBHYT;
               //    crpt.PrintToPrinter(PropertyLib._HISPrintProperties.SoLuongCanInBHYT, true, 0, 0);
               //    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.Locked).EqualTo(1).Where(KcbLuotkham.Columns.MaLuotkham)
               //                 .IsEqualTo(objPatientExam.MaLuotkham).Execute();

               //}
               //else
               //{
                 
               //}
               PrintDialog frmPrint = new PrintDialog();
               if (frmPrint.ShowDialog() == DialogResult.OK)
               {
                   new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.Locked).EqualTo(1).Where(KcbLuotkham.Columns.MaLuotkham)
                            .IsEqualTo(objPatientExam.MaLuotkham).Execute();

                   crpt.PrintOptions.PrinterName = frmPrint.PrinterSettings.PrinterName;
                   crpt.PrintToPrinter(frmPrint.PrinterSettings.Copies, frmPrint.PrinterSettings.Collate, frmPrint.PrinterSettings.FromPage, frmPrint.PrinterSettings.ToPage);
               }
          
           }
           catch (Exception exception)
           {
               
              Utility.ShowMsg("Bạn cần cấu hình lại máy in ","Thông báo",MessageBoxIcon.Warning);
               
           }
          
           //objForm.ShowDialog();

           //}
           //catch (Exception ex)
           //{
           //    Utility.DefaultNow(this);
           //}
       }
       private  decimal SumOfTotal(DataTable dataTable, string FiledName)
       {
           return Utility.getSUM(dataTable, FiledName);
           
       }
       MoneyByLetter _moneyByLetter=new MoneyByLetter();
       /// <summary>
       /// HÀM THỰC HIỆN VIỆC IN PHIẾU BIÊN LAI CHO BẢO HIỂM Y TẾ
       /// </summary>
       /// <param name="m_dtReportPhieuThu"></param>
       /// <param name="sTitleReport"></param>
       /// <param name="ngayIn"></param>
       public void LAOKHOA_INPHIEU_BIENLAI_BHYT(DataTable m_dtReportPhieuThu, string sTitleReport, DateTime ngayIn)
       {
          
           if(m_dtReportPhieuThu.Rows.Count<=0)
           {
               Utility.ShowMsg("Không tìm thấy bản ghi nào","Thông báo",MessageBoxIcon.Warning);
               return;
           }
           Utility.UpdateLogotoDatatable(ref m_dtReportPhieuThu);
           THU_VIEN_CHUNG.Sapxepthutuin(ref m_dtReportPhieuThu,true);
           m_dtReportPhieuThu.DefaultView.Sort = "THU_TU ASC";
           m_dtReportPhieuThu.AcceptChanges();
           decimal phuthu = SumOfTotal(m_dtReportPhieuThu, "PHU_THU");
           decimal bnct = SumOfTotal(m_dtReportPhieuThu, "BNCT");
           decimal Total = bnct+phuthu;
            string tieude="", reportname = "";
           ReportDocument crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4",ref tieude,ref reportname);
           if (PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4)
               crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A4" ,ref tieude,ref reportname);
           else
               crpt = Utility.GetReport("thanhtoan_Bienlai_BHYT_A5" ,ref tieude,ref reportname);
           if (crpt == null) return;
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);
           //try
           //{

           crpt.SetDataSource(m_dtReportPhieuThu.DefaultView);
           //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
           Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
           Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
           //Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
           Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
           Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(ngayIn));
           Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
           Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
           Utility.SetParameterValue(crpt,"sMoneyLetter", _moneyByLetter.sMoneyToLetter(Total.ToString()));
           objForm.crptViewer.ReportSource = crpt;
           objForm.ShowDialog();

           //}
           //catch (Exception ex)
           //{
           //    Utility.DefaultNow(this);
           //}
       }
       private decimal SumOfTotal_BH(DataTable dataTable, string sFildName)
       {
           return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + sFildName + ")", "1=1"), 0);
       }

    }
}
