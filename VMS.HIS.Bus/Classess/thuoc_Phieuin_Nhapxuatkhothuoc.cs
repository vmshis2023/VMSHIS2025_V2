using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using System.IO;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.Baocao
{
   public class thuoc_phieuin_nhapxuat
    {
       
       public static void InphieuNhapkho(int IDPhieuNhap, string sTitleReport,DateTime NgayIn)
       {
           DataTable m_dtReport = new THUOC_NHAPKHO().Laythongtininphieunhapkhothuoc(IDPhieuNhap);
           if(m_dtReport.Rows.Count<=0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ","Thông báo",MessageBoxIcon.Warning);
               return;
           }
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieu_nhapkho.xml");
           MoneyByLetter _moneyByLetter = new MoneyByLetter();
           string tinhtong = TinhTong(m_dtReport, TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName);
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_phieunhapkho", ref tieude, ref reportname);

           // VNS.HIS.UI.BaoCao.PhieuNhapKho.CRPT_PHIEU_NHAPKHO crpt =new CRPT_PHIEU_NHAPKHO();
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
          
           Utility.UpdateLogotoDatatable(ref m_dtReport);
           try
           {

               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
              
               ////crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieunhapkho";
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
              
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
               objForm.crptViewer.ReportSource = crpt;
               objForm.ShowDialog();
               // Utility.DefaultNow(this);
           }
           catch (Exception ex)
           {
               if (globalVariables.IsAdmin)
               {
                   Utility.ShowMsg(ex.ToString());
               }
           }
       }
       public static void InphieuThanhly(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaythongtininphieuXuatkhothuoc(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           MoneyByLetter _moneyByLetter = new MoneyByLetter();
           string tinhtong = TinhTong(m_dtReport, TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName);
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_bienban_thanhly");
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_bienban_thanhly", ref tieude, ref reportname);


           Utility.UpdateLogotoDatatable(ref m_dtReport);
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           try
           {
               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_bienban_thanhly";
              
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
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
       public static void InphieuHuythuoc(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaythongtininphieuXuatkhothuoc(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           MoneyByLetter _moneyByLetter = new MoneyByLetter();
           string tinhtong = TinhTong(m_dtReport, TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName);
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_bienban_huythuoc");
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_bienban_huythuoc", ref tieude, ref reportname);


           Utility.UpdateLogotoDatatable(ref m_dtReport);
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           try
           {
               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_bienban_huythuoc";
             
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
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

       public static void InphieuDutru(int IDPhieuNhap,  string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaydulieuinphieuchuyenkho2lien(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieudutru");
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_phieudutru", ref tieude, ref reportname);


           Utility.UpdateLogotoDatatable(ref m_dtReport);
           frmPrintPreview objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           try
           {
               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieudutru";
              
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
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

       public static void InphieuXuatkho(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaydulieuinphieuchuyenkho2lien(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieu_xuatkho_1lien");
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_phieu_xuatkho_1lien", ref tieude, ref reportname);

           string tinhtong = TinhTong(m_dtReport, "THANHTIEN_XUAT");
           Utility.UpdateLogotoDatatable(ref m_dtReport);
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           try
           {

               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieu_xuatkho_1lien";
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "sMoneyLetter", new MoneyByLetter().sMoneyToLetter(tinhtong));
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
               objForm.crptViewer.ReportSource = crpt;
               objForm.ShowDialog();
               // Utility.DefaultNow(this);
           }
           catch (Exception ex)
           {
               if (globalVariables.IsAdmin)
               {
                   Utility.ShowMsg(ex.ToString());
               }
           }
       }
       public static void InBienBanGiaoHang(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaydulieuinphieuchuyenkho2lien(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieu_bienbangiaohang");
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("thuoc_phieu_bienbangiaohang", ref tieude, ref reportname);
           Utility.UpdateLogotoDatatable(ref m_dtReport);
           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           try
           {
               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieu_bienbangiaohang";
               Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt, "sTitleReport", tieude);
               Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
             //  Utility.SetParameterValue(crpt, "sMoneyLetter", new MoneyByLetter().sMoneyToLetter(tinhtong));
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
               objForm.crptViewer.ReportSource = crpt;

               objForm.ShowDialog();
               // Utility.DefaultNow(this);
           }
           catch (Exception ex)
           {
               if (globalVariables.IsAdmin)
               {
                   Utility.ShowMsg(ex.ToString());
               }
           }
           finally
           {
               Utility.FreeMemory(crpt);
           }
       }
       public static void InphieuXuatkho_2lien(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           try
           {
               DataTable dataTable = SPs.ThuocLaydulieuinphieuchuyenkho2lien(IDPhieuNhap).GetDataSet().Tables[0];
               
               if (dataTable.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                   return;
               }
               MoneyByLetter _moneyByLetter = new MoneyByLetter();
               string tinhtong = TinhTong(dataTable, "THANHTIEN_XUAT");
               THU_VIEN_CHUNG.CreateXML(dataTable, "thuoc_phieu_xuatkho_2lien.xml");
               string tieude = "", reportname = "", reportCode = "";
               reportCode = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_INPHIEUXUATKHO_2LIEN", "0", false) == "1"
                                ? "thuoc_phieu_xuatkho_2lien"
                                : "thuoc_phieu_xuatkho_1lien";
               var crpt = Utility.GetReport(reportCode, ref tieude, ref reportname);
               if (crpt != null)
               {


                   frmPrintPreview objForm = new frmPrintPreview(sTitleReport, crpt, true, true);
                   Utility.UpdateLogotoDatatable(ref dataTable);
                   crpt.SetDataSource(dataTable);
                   objForm.mv_sReportFileName = Path.GetFileName(reportname);
                   objForm.mv_sReportCode = reportCode;
                  Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                  Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                  Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
                  Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(tinhtong));
                  Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTime(DateTime.Now));
                  Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
                  objForm.crptViewer.ReportSource = crpt;
                  objForm.ShowDialog();
               }
           }
           catch (Exception ex)
           {
               Utility.ShowMsg("Lỗi khi in báo cáo InphieuXuatkho_2lien()-->\n"+ex.Message);
               return;
           }
       }
       public static void InphieuTraKholeVeKhochan(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           try
           {
               DataTable m_dtReport = SPs.ThuocLaythongtininphieutrakholevekhochan(IDPhieuNhap).GetDataSet().Tables[0];
               if (m_dtReport.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                   return;
               }
               THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieutrakholevekhochan.xml");
               Utility.UpdateLogotoDatatable(ref m_dtReport);
               string tieude = "", reportname = "";
               var crpt = Utility.GetReport( "thuoc_phieutrakholevekhochan" , ref tieude, ref reportname);

               var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);

               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
              
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieutrakholevekhochan";
               Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt, "sTitleReport", sTitleReport);
               Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "sMoneybyletter", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
               objForm.crptViewer.ReportSource = crpt;

               objForm.ShowDialog();
               // Utility.DefaultNow(this);
           }
           catch (Exception ex)
           {
               if (globalVariables.IsAdmin)
               {
                   Utility.ShowMsg(ex.ToString());
               }
           }
       }
       public static void Inphieuxuatthuockhoa(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
           DataTable m_dtReport = SPs.ThuocLaythongtininphieuXuatkhothuoc(IDPhieuNhap).GetDataSet().Tables[0];
           if (m_dtReport.Rows.Count <= 0)
           {
               Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
               return;
           }
           string tieude = "", reportname = "";
           var crpt = Utility.GetReport("CRPT_PHIEUXUAT_KHOLE_TOIKHOA", ref tieude, ref reportname);
           

           var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

           Utility.UpdateLogotoDatatable(ref m_dtReport);
           try
           {

               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                                 //"".Replace("#$X$#",
                                                                                 //           Strings.Chr(34) + "&Chr(13)&" +
                                                                                 //           Strings.Chr(34)) + Strings.Chr(34);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "CRPT_PHIEUXUAT_KHOLE_TOIKHOA";
              
               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
               objForm.crptViewer.ReportSource = crpt;
               objForm.ShowDialog();
               // Utility.DefaultNow(this);
           }
           catch (Exception ex)
           {
               if (globalVariables.IsAdmin)
               {
                   Utility.ShowMsg(ex.ToString());
               }
           }
       }

       public static void InphieuTranhacungcap(int IDPhieuNhap, string sTitleReport, DateTime NgayIn)
       {
          

           try
           {
               DataTable m_dtReport = SPs.ThuocLaydulieuinphieuchuyenkho2lien(IDPhieuNhap).GetDataSet().Tables[0];
               if (m_dtReport.Rows.Count <= 0)
               {
                   Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                   return;
               }
               THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_phieutranhacungcap");
               string tieude = "", reportname = "";
               var crpt = Utility.GetReport("thuoc_phieutranhacungcap", ref tieude, ref reportname);
               Utility.UpdateLogotoDatatable(ref m_dtReport);
               var objForm = new frmPrintPreview(sTitleReport, crpt, true, true);

               m_dtReport.AcceptChanges();
               crpt.SetDataSource(m_dtReport);
               objForm.mv_sReportFileName = Path.GetFileName(reportname);
               objForm.mv_sReportCode = "thuoc_phieutranhacungcap";
              

               Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
               Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
               Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
               Utility.SetParameterValue(crpt,"sTitleReport", tieude);
               Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
               Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, DateTime.Now));
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
       private static string TinhTong(DataTable dataTable,string colname)
       {
           try
           {
               string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
              c => c.Field<decimal>(colname)));
               return sumoftotal;
           }
           catch (Exception)
           {

               return "0";
           }
          
       }
      
    }
}
