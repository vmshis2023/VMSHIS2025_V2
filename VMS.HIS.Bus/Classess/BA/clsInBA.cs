using Aspose.Words;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;

 namespace VMS.HIS.Bus
{
    public class clsInBA
    {
        public clsInBA()
        {
        }
        public static string InTomTatBA(EmrTomtatBa ttba, bool returnFile = false)
        {
            try
            {
               
                if (ttba == null || ttba.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                    return "";
                }
                VKcbLuotkham objBN = Utility.getKcbBenhnhan(ttba.IdBenhnhan, ttba.MaLuotkham);
                NoitruPhieuravien objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(ttba.IdBenhnhan).And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(ttba.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                DataTable dtData = SPs.EmrTongketbanhanIn(ttba.Id, ttba.IdBenhnhan, ttba.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "noitru_tomtatBA";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh"};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                List<string>  lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();

                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tomtatBA.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return "";
                }
                dtData.TableName = "noitru_tomtatBA";
                Document doc;
                DataRow drData = dtData.Rows[0];
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("ten_SYT")) drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_dvicaptren")) drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("diahchi_benhvien")) drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                if (dtData.Columns.Contains("SDT_bv")) drData["SDT_bv"] = globalVariables.Branch_Phone;
                if (dtData.Columns.Contains("Hotline_bv")) drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                if (dtData.Columns.Contains("Fax_bv")) drData["Fax_bv"] = globalVariables.Branch_Fax;
                if (dtData.Columns.Contains("website_bv")) drData["website_bv"] = globalVariables.Branch_Website;
                if (dtData.Columns.Contains("email_bv")) drData["email_bv"] = globalVariables.Branch_Email;
                drData["dia_diem"] = globalVariables.gv_strDiadiem;
                drData["ngay_thang_nam"] = Utility.FormatDateTime(ttba.NgayTtba.Value);
                //Dictionary<string, string> dicMF = new Dictionary<string, string>();
                //dicMF.Add("gioitinh_nam", objBN.IdGioitinh.ToString() == "0" ? "1" : "0");
                //dicMF.Add("gioitinh_nu", objBN.IdGioitinh.ToString() == "0" ? "0" : "1");
                //dicMF.Add("noikhoa_co", Utility.Byte2Bool(ttba.Noikhoa) ? "1" : "0");
                //dicMF.Add("noikhoa_khong", Utility.Byte2Bool(ttba.Noikhoa) ? "0" : "1");
                //dicMF.Add("pttt_co", Utility.Byte2Bool(ttba.Pttt) ? "1" : "0");
                //dicMF.Add("pttt_khong", Utility.Byte2Bool(ttba.Pttt) ? "0" : "1");
                //if (objRavien != null)
                //{
                //    dicMF.Add("tinhtrangravien_khoi", Utility.sDbnull(objRavien.MaKquaDieutri) == "1" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_do", Utility.sDbnull(objRavien.MaKquaDieutri) == "2" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_khongthaydoi", Utility.sDbnull(objRavien.MaKquaDieutri) == "3" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_nanghon", Utility.sDbnull(objRavien.MaKquaDieutri) == "4" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_tuvong", Utility.sDbnull(objRavien.MaKquaDieutri) == "5" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_xinve", Utility.sDbnull(objRavien.MaKquaDieutri) == "6" ? "1" : "0");
                //    dicMF.Add("tinhtrangravien_khongxacdinh", Utility.sDbnull(objRavien.MaKquaDieutri) == "7" ? "1" : "0");
                //}
                //else
                //{
                //    dicMF.Add("tinhtrangravien_khoi", "0");
                //    dicMF.Add("tinhtrangravien_do",  "0");
                //    dicMF.Add("tinhtrangravien_khongthaydoi",  "0");
                //    dicMF.Add("tinhtrangravien_nanghon","0");
                //    dicMF.Add("tinhtrangravien_tuvong", "0");
                //    dicMF.Add("tinhtrangravien_xinve",  "0");
                //    dicMF.Add("tinhtrangravien_khongxacdinh",  "0");
                //}
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\TomtatBA_V1.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("noitru_tomtatBA", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Tóm tắt hồ sơ bệnh án tại thư mục sau :" + PathDoc);
                    return "";
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "TOMTAT_BA", ttba.MaLuotkham, Utility.sDbnull(ttba.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
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
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                   // Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    if (returnFile)
                        doc.Save(fileKetqua, SaveFormat.Pdf);
                    else
                        doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
                    if (returnFile) return path;
                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return "";
                }
                return "";
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
                return "";
            }
        }
        static string LayMaBA( string loai_ba)
        {
            string tenToBA = "BENH_AN";
            switch (loai_ba)
            {
                case LoaiBA.BA_SANKHOA:
                    tenToBA= "BA_SANKHOA";
                    break;
                case LoaiBA.BA_PHUKHOA:
                    tenToBA = "BA_PHUKHOA";
                    break;
                case LoaiBA.BA_NAMKHOA:
                    tenToBA = "BA_NAMKHOA";
                    break;
                case LoaiBA.BA_NOIKHOA:
                    tenToBA = "BA_NOIKHOA";
                    break;
                case LoaiBA.BA_NGOAIKHOA:
                    tenToBA = "BA_NGOAIKHOA";
                    break;
                case LoaiBA.BA_NGOAITRU:
                    tenToBA = "BA_NGOAITRU";
                    break;
                case LoaiBA.BA_SOSINH:
                    tenToBA = "BA_SOSINH";
                    break;
                case LoaiBA.BA_NHIKHOA:
                    tenToBA = "BA_NHIKHOA";
                    break;
                case LoaiBA.BA_IVF_VO:
                    tenToBA = "BA_IVF_VO";
                    break;
                case LoaiBA.BA_IVF_CHONG:
                    tenToBA = "BA_IVF_CHONG";
                    break;
                default:
                    tenToBA = "BENH_AN";
                    break;

            }
            return tenToBA;
        }
        static string LayTenToBA(int toBA,string loai_ba)
        {
            string tenToBA = "";
            switch (loai_ba)
            {
                case LoaiBA.BA_SANKHOA:
                    if (toBA == 1) tenToBA = "BA05_BASANKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA05_BASANKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA05_BASANKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA05_BASANKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA05_BASANKHOA_TO4.doc";
                    else tenToBA = "BA05_BASANKHOA.doc";
                    break;
                case LoaiBA.BA_PHUKHOA:
                    if (toBA == 1) tenToBA = "BA04_BAPHUKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA04_BAPHUKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA04_BAPHUKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA04_BAPHUKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA04_BAPHUKHOA_TO4.doc";
                    else tenToBA = "BA04_BAPHUKHOA.doc";
                    break;
                case LoaiBA.BA_NAMKHOA:
                    if (toBA == 1) tenToBA = "BANK_BANAMKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BANK_BANAMKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BANK_BANAMKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BANK_BANAMKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BANK_BANAMKHOA_TO4.doc";
                    else tenToBA = "BANK_BANAMKHOA.doc";
                    break;
                case LoaiBA.BA_NOIKHOA:
                    if (toBA == 1) tenToBA = "BA01_BANOIKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA01_BANOIKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA01_BANOIKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA01_BANOIKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA01_BANOIKHOA_TO4.doc";
                    else tenToBA = "BA01_BANOIKHOA.doc";
                    break;
                case LoaiBA.BA_NGOAIKHOA:
                    if (toBA == 1) tenToBA = "BA10_BANGOAIKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA10_BANGOAIKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA10_BANGOAIKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA10_BANGOAIKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA10_BANGOAIKHOA_TO4.doc";
                    else tenToBA = "BA10_BANGOAIKHOA.doc";
                    break;
                case LoaiBA.BA_NGOAITRU:
                    if (toBA == 1) tenToBA = "BA15_BANGOAITRU_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA15_BANGOAITRU_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA15_BANGOAITRU_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA15_BANGOAITRU_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA15_BANGOAITRU_TO4.doc";
                    else tenToBA = "BA15_BANGOAITRU.doc";
                    break;
                case LoaiBA.BA_SOSINH:
                    if (toBA == 1) tenToBA = "BA06_BANSOSINH_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA06_BANSOSINH_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA06_BANSOSINH_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA06_BANSOSINH_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA06_BANSOSINH_TO4.doc";
                    else tenToBA = "BA06_BANSOSINH.doc";
                    break;
                case LoaiBA.BA_NHIKHOA:
                    if (toBA == 1) tenToBA = "BA02_BANNHIKHOA_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA02_BANNHIKHOA_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA02_BANNHIKHOA_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA02_BANNHIKHOA_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA02_BANNHIKHOA_TO4.doc";
                    else tenToBA = "BA02_BANNHIKHOA.doc";
                    break;
                case LoaiBA.BA_IVF_CHONG:
                    if (toBA == 1) tenToBA = "BA_IVF_CHONG_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA_IVF_CHONG_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA_IVF_CHONG_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA_IVF_CHONG_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA_IVF_CHONG_TO4.doc";
                    else tenToBA = "BA_IVF_CHONG.doc";
                    break;
                case LoaiBA.BA_IVF_VO:
                    if (toBA == 1) tenToBA = "BA_IVF_VO_TO1.doc";
                    else if (toBA == 0) tenToBA = "BA_IVF_VO_BIA.doc";
                    else if (toBA == 2) tenToBA = "BA_IVF_VO_TO2.doc";
                    else if (toBA == 3) tenToBA = "BA_IVF_VO_TO3.doc";
                    else if (toBA == 4) tenToBA = "BA_IVF_VO_TO4.doc";
                    else tenToBA = "BA_IVF_VO.doc";
                    break;
                default:
                    break;

            }
            return tenToBA;
        }
        public static string InBA_bak_250716(long IdBa,string MaBa,string LoaiBa, KcbLuotkham objLuotkham, string tenToBA,bool returnFile=false)
        {
            try
            {
              

                //if (objEmrBa == null || objEmrBa.IdBa <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần tạo Bệnh án Ngoại trú trước khi thực hiện in");
                //    return "";
                //}
                DataTable dtData = SPs.EmrBaLaythongtinIn(IdBa, MaBa, LoaiBa, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien objPhieuRavien = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = "BA_EMR";
                Document doc;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("ten_SYT")) drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_dvicaptren")) drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("diahchi_benhvien")) drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                if (dtData.Columns.Contains("SDT_bv")) drData["SDT_bv"] = globalVariables.Branch_Phone;
                if (dtData.Columns.Contains("Hotline_bv")) drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                if (dtData.Columns.Contains("Fax_bv")) drData["Fax_bv"] = globalVariables.Branch_Fax;
                if (dtData.Columns.Contains("website_bv")) drData["website_bv"] = globalVariables.Branch_Website;
                if (dtData.Columns.Contains("email_bv")) drData["email_bv"] = globalVariables.Branch_Email;

                drData["p102"] = globalVariables.Branch_Name;
                drData["p101"] = globalVariables.ParentBranch_Name;
                drData["p132"] = _phieunv != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, "") : ".......... giờ ....... ngày ........./........./.............";//Vào viện
                drData["p131_1"] = objLuotkham != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayTiepdon, "") : ".......... giờ ....... ngày ........./........./.............";//Đến khám bệnh lúc

            
                drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["p145_1"] = objPhieuRavien != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(objPhieuRavien.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";//ra viện
                drData["p230_9"] = objLuotkham.NgayKetthuc.HasValue? Utility.FormatDateTime(objLuotkham.NgayKetthuc.Value) : "Ngày ....... tháng ........ năm .........";//Ngày khám cuối tờ 1 BA ngoại trú

                List<string> fieldNames = new List<string>();

                if (!tenToBA.ToUpper().Contains(".DOC")) tenToBA = string.Format("{0}.doc", tenToBA);
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
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
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);



                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Pdf);
                    string path = fileKetqua;
                    if (returnFile)
                        return path;
                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                    return "";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return "";
                }
                return "";
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }

        public static string InBA(long IdBa, string MaBa,string LoaiBa, KcbLuotkham objLuotkham,DataTable dtkhoanhapvien,DataTable dtkhoachuyen,DataTable dt_tssk,DataTable dtPhieuPttt, int toBA, bool returnFile = false)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                    return "";
                }

                //if (objEmrBa == null || objEmrBa.IdBa <= 0)
                //{
                //    Utility.ShowMsg("Bạn cần tạo Bệnh án trước khi thực hiện in");
                //    return "";
                //}
                DataTable dtData = SPs.EmrBaLaythongtinIn(IdBa, MaBa, LoaiBa, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien objPhieuRavien = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = LayMaBA(LoaiBa); ;
                Document doc;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("ten_SYT")) drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_dvicaptren")) drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("diahchi_benhvien")) drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                if (dtData.Columns.Contains("SDT_bv")) drData["SDT_bv"] = globalVariables.Branch_Phone;
                if (dtData.Columns.Contains("Hotline_bv")) drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                if (dtData.Columns.Contains("Fax_bv")) drData["Fax_bv"] = globalVariables.Branch_Fax;
                if (dtData.Columns.Contains("website_bv")) drData["website_bv"] = globalVariables.Branch_Website;
                if (dtData.Columns.Contains("email_bv")) drData["email_bv"] = globalVariables.Branch_Email;

                if (dtData.Columns.Contains("p102")) drData["p102"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("p101")) drData["p101"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("p132")) drData["p132"] = _phieunv != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, "") : ".......... giờ ....... ngày ........./........./.............";//Vào viện
                if (dtkhoanhapvien!=null && dtkhoanhapvien.Columns.Count>0 && dtkhoanhapvien.Rows.Count > 0)
                {
                    if (dtData.Columns.Contains("p141")) drData["p141"] = Utility.FormatDateTime_giophut_ngay_thang_nam(Convert.ToDateTime(dtkhoanhapvien.Rows[0]["ngay_vaokhoa"]), "");//vào khoa
                    if (dtData.Columns.Contains("p141_1")) drData["p141_1"] = Utility.sDbnull(dtkhoanhapvien.Rows[0]["so_luong"], "0");
                }
                //REM lại do đã xử lý ở bước fillData trước khi ghi
                //drData["p103"] = drData["p140"];
                //if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                //{
                //    drData["p103"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                //    drData["p104"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                //}
                if (dtData.Columns.Contains("p128")) drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                if (dtData.Columns.Contains("p145_1")) drData["p145_1"] = objPhieuRavien != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(objPhieuRavien.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";//ra viện
                if (dtData.Columns.Contains("p155_2")) drData["p155_2"] = Utility.FormatDateTime(Utility.sDbnull(drData["p155_2"], ""), globalVariables.NgayThangNam);//ra viện
                                                                                                                                                // drData["p155_1"] = objPhieuRavien != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(objPhieuRavien.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";

                //drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                //drData["SDT_bv"] = globalVariables.Branch_Phone;
                //drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                //drData["Fax_bv"] = globalVariables.Branch_Fax;
                //drData["website_bv"] = globalVariables.Branch_Website;
                //drData["email_bv"] = globalVariables.Branch_Email;
                //drData["ten_phieu"] = "Phiếu khám thai";
                //drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["sNgaykykinh_cuoi"] = _pkt.NgayDaukykinhcuoi.HasValue ? _pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                //drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                //drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                //drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");

                Utility.CreateMergeFields(dtData);
                List<string> fieldNames = new List<string>();
              string tenToBA = LayTenToBA(toBA, LoaiBa);
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); 
                        return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                    {
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
                        {
                            if (globalVariables.SysLogo != null)
                            {
                                builder.InsertImage(globalVariables.SysLogo);
                            }
                        }
                    }
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    string loai_ba = Utility.sDbnull(drData["loai_ba"]);
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    try
                    {
                        int table_idx = 0;
                        int row_idx = 0;
                        string tbl_idx = "";
                        string vitribangcha = "0";//Vị trí bảng cha chứa bảng(chỉ có ý nghĩa với các tờ phía sau, chứ tờ 1 chắc nằm trong bảng 1 ứng với idx=0)
                        string vitrihang = "14";//Vị trí hàng trong bảng cha chứa bảng
                        if (dtkhoachuyen != null && dtkhoachuyen.Columns.Count > 0 && (toBA == 1 || toBA == 100))//Thông tin chuyển khoa chỉ có ở tờ 1 hoặc in full
                        {
                            Aspose.Words.Tables.Table topTable = doc.FirstSection.Body.Tables[0];//Table có 4 rows, mỗi row chứa 1 tờ
                            vitribangcha = string.Format("{0}_CHUYENKHOA_VITRIBANGCHA", loai_ba);
                            table_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitribangcha, "-1", true));
                            vitrihang = string.Format("{0}_CHUYENKHOA_VITRIHANG", loai_ba);
                            row_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitrihang, "-1", true));
                            if (table_idx >= 0 && row_idx >= 0)
                            {

                                Aspose.Words.Tables.Table tab = topTable.Rows[0].Cells[0].ChildNodes[1] as Aspose.Words.Tables.Table;//dòng index=0 là chữ font nhỏ để hiển thị viền trên của bảng phía trong
                                tab = tab.Rows[row_idx].Cells[1].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                                int idx = 1;//Đè lên header trong design
                                foreach (DataRow dr in dtkhoachuyen.Rows)
                                {
                                    Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                    newRow.RowFormat.Borders.Shadow = false;
                                    newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;


                                    newRow.Cells[0].FirstParagraph.Runs.Clear();
                                    newRow.Cells[1].FirstParagraph.Runs.Clear();
                                    newRow.Cells[2].FirstParagraph.Runs.Clear();

                                    Run r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Size = 10d;
                                    r.Font.Bold = true;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["ma_khoanoitru"], "");
                                    newRow.Cells[0].FirstParagraph.AppendChild(r);
                                    newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["sngay_vaokhoa"], "");
                                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["so_luong"], "");
                                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                    if (idx > 1)
                                        tab.AppendChild(newRow);
                                    idx += 1;
                                }
                            }
                        }
                        if (dt_tssk != null && dt_tssk.Columns.Count > 0 && dt_tssk.Rows.Count>0 && (toBA == 2 || toBA == 100))//In thông tin tiền sử sản khoa in tờ 2, BA nào không có thì không cần khai báo tham số hệ thống
                        {
                            Aspose.Words.Tables.Table topTable = doc.FirstSection.Body.Tables[0];//Table có 4 rows, mỗi row chứa 1 tờ
                            vitribangcha = toBA == 2 ? string.Format("{0}_TIENSUSANKHOA_VITRIBANGCHA", loai_ba) : string.Format("{0}_TIENSUSANKHOA_VITRIBANGCHA_FULL", loai_ba);
                            table_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitribangcha, "-1", true));
                            vitrihang = string.Format("{0}_TIENSUSANKHOA_VITRIHANG", loai_ba);
                            row_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitrihang, "-1", true));
                            byte isCheck = 0;
                            if (table_idx >= 0 && row_idx >= 0)
                            {
                                Aspose.Words.Tables.Table tab = topTable.Rows[table_idx].Cells[0].ChildNodes[1] as Aspose.Words.Tables.Table;//doc.FirstSection.Body.Tables[table_idx];
                                //Bảng nằm ở vị trí dòng thứ 11 của bảng lớn. Có 14 cột luôn
                                tab = tab.Rows[row_idx].Cells[0].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                                int idx = 2;//Giữ lại tiêu đề header của bảng
                                int solan_cothai = 1;
                                foreach (DataRow dr in dt_tssk.Rows)
                                {
                                    Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                    newRow.RowFormat.Borders.Shadow = false;
                                    newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[4].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[5].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[6].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[7].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[8].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[9].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[10].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[11].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[12].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[13].CellFormat.Shading.BackgroundPatternColor = Color.White;



                                    newRow.Cells[0].FirstParagraph.Runs.Clear();
                                    newRow.Cells[1].FirstParagraph.Runs.Clear();
                                    newRow.Cells[2].FirstParagraph.Runs.Clear();
                                    newRow.Cells[3].FirstParagraph.Runs.Clear();
                                    newRow.Cells[4].FirstParagraph.Runs.Clear();
                                    newRow.Cells[5].FirstParagraph.Runs.Clear();
                                    newRow.Cells[6].FirstParagraph.Runs.Clear();
                                    newRow.Cells[7].FirstParagraph.Runs.Clear();
                                    newRow.Cells[8].FirstParagraph.Runs.Clear();
                                    newRow.Cells[9].FirstParagraph.Runs.Clear();
                                    newRow.Cells[10].FirstParagraph.Runs.Clear();
                                    newRow.Cells[11].FirstParagraph.Runs.Clear();
                                    newRow.Cells[12].FirstParagraph.Runs.Clear();
                                    newRow.Cells[13].FirstParagraph.Runs.Clear();


                                    Run r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Size = 10d;
                                    r.Font.Bold = false;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(solan_cothai);
                                    newRow.Cells[0].FirstParagraph.AppendChild(r);
                                    newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                                    solan_cothai += 1;
                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["nam"], "");
                                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    isCheck = Utility.ByteDbnull(dr["deduthang"], 0);
                                    //if (dicMF[field] == "1")
                                    //{
                                    //    builder.Font.Name = "Wingdings 2";
                                    //    builder.Write(char.ConvertFromUtf32(82));
                                    //    builder.Font.ClearFormatting();
                                    //}
                                    //else
                                    //{
                                    //    builder.Font.Name = "Wingdings 2";
                                    //    builder.Write(char.ConvertFromUtf32(163));
                                    //    builder.Font.ClearFormatting();
                                    //}

                                    r = new Run(doc);

                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    //r.Text = Utility.sDbnull(dr["deduthang"], "");
                                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;


                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["dethieuthang"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);
                                    newRow.Cells[3].FirstParagraph.AppendChild(r);
                                    newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["say"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[4].FirstParagraph.AppendChild(r);
                                    newRow.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["hut"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[5].FirstParagraph.AppendChild(r);
                                    newRow.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["nao"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[6].FirstParagraph.AppendChild(r);
                                    newRow.Cells[6].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["covac"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);


                                    newRow.Cells[7].FirstParagraph.AppendChild(r);
                                    newRow.Cells[7].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["chuangoaitucung"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);
                                    newRow.Cells[8].FirstParagraph.AppendChild(r);
                                    newRow.Cells[8].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["thaichetluu"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[9].FirstParagraph.AppendChild(r);
                                    newRow.Cells[9].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["conhiensong"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[10].FirstParagraph.AppendChild(r);
                                    newRow.Cells[10].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["thongtintre_cannang_benhtat"], "");
                                    newRow.Cells[11].FirstParagraph.AppendChild(r);
                                    newRow.Cells[11].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["phuongphapde"], "");//Chửa trứng
                                    newRow.Cells[12].FirstParagraph.AppendChild(r);
                                    newRow.Cells[12].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    isCheck = Utility.ByteDbnull(dr["taibien_hausan"], 0);
                                    r.Font.Name = "Wingdings 2";
                                    r.Text = isCheck == 1 ? char.ConvertFromUtf32(82) : char.ConvertFromUtf32(163);

                                    newRow.Cells[13].FirstParagraph.AppendChild(r);
                                    newRow.Cells[13].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                                    if (idx > 1)
                                        tab.AppendChild(newRow);
                                    idx += 1;
                                }
                            }
                        }

                        if (dtPhieuPttt != null && dtPhieuPttt.Columns.Count > 0  && dtPhieuPttt.Rows.Count>0 && (toBA == 4 || toBA == 100))//In thông tin phiếu PTTT Bệnh án phụ khoa,sản khoa, ngoại khoa tại tờ 4
                        {
                            Aspose.Words.Tables.Table topTable = doc.FirstSection.Body.Tables[0];//Table có 4 rows, mỗi row chứa 1 tờ
                            vitribangcha = toBA == 4 ? string.Format("{0}_PTTT_VITRIBANGCHA", loai_ba) : string.Format("{0}_PTTT_VITRIBANGCHA_FULL", loai_ba);
                            table_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitribangcha, "-1", true));
                            vitrihang = string.Format("{0}_PTTT_VITRIHANG", loai_ba);
                            row_idx = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong(vitrihang, "-1", true));
                            if (table_idx >= 0 && row_idx >= 0)
                            {
                                Aspose.Words.Tables.Table tab = topTable.Rows[table_idx].Cells[0].ChildNodes[1] as Aspose.Words.Tables.Table;// doc.FirstSection.Body.Tables[table_idx];

                                tab = tab.Rows[row_idx].Cells[0].FirstChild as Aspose.Words.Tables.Table;//(Aspose.Words.Tables.Table)doc.GetChild(NodeType.Table, 0, true);//
                                int idx = 2;//Giữ lại tiêu đề header của bảng
                                foreach (DataRow dr in dtPhieuPttt.Rows)
                                {
                                    Aspose.Words.Tables.Row newRow = idx == 1 ? (Aspose.Words.Tables.Row)tab.LastRow : (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);//.Clone(true);
                                    newRow.RowFormat.Borders.Shadow = false;
                                    newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                                    newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;

                                    newRow.Cells[0].FirstParagraph.Runs.Clear();
                                    newRow.Cells[1].FirstParagraph.Runs.Clear();
                                    newRow.Cells[2].FirstParagraph.Runs.Clear();
                                    newRow.Cells[3].FirstParagraph.Runs.Clear();

                                    Run r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Size = 10d;
                                    r.Font.Bold = false;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["ngay_pttt"], "");
                                    newRow.Cells[0].FirstParagraph.AppendChild(r);
                                    newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["phuongphap_pt_vc"], "");
                                    newRow.Cells[1].FirstParagraph.AppendChild(r);
                                    newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["ten_bacsy_phauthuat"], "");
                                    newRow.Cells[2].FirstParagraph.AppendChild(r);
                                    newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;


                                    r = new Run(doc);
                                    r.Font.Name = "Times New Roman";
                                    r.Font.Bold = false;
                                    r.Font.Size = 10d;
                                    //r.Font.Color = Color.FromArgb(102, 0, 102);
                                    r.Text = Utility.sDbnull(dr["ten_bacsy_gayme"], "");
                                    newRow.Cells[3].FirstParagraph.AppendChild(r);
                                    newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                                    if (idx > 1)
                                        tab.AppendChild(newRow);
                                    idx += 1;
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }


                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("SIGNSIZE").ExecuteSingle<SysSystemParameter>();
                   Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
                    if (returnFile)
                        return path;
                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return "";
                }
                return "";
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
                return "";
            }
        }
        //static Dictionary<string, string> GetDictionaryFromDataTable()
        //{
        //    var dict = new Dictionary<string, string>();

        //    foreach (DataRow row in globalVariables.dtSignInfor.Rows)
        //    {
        //        string key = row["ten_vitri_ky"].ToString();
        //        string value = row["nguoi_ky"].ToString();

        //        if (!dict.ContainsKey(key))
        //            dict.Add(key, value);
        //    }

        //    return dict;
        //}
        //static void SignDoc(Document doc, DocumentBuilder builder, string Signsize)
        //{
        //    if (globalVariables.dtSignInfor.Rows.Count > 0 && globalVariables.dtSignInfor.Columns.Count > 0)//Tìm các vùng chữ kí để đưa ảnh vào
        //    {
        //        string[] remaining = doc.MailMerge.GetFieldNames();
        //        globalVariables.lstVitriky = GetDictionaryFromDataTable();
        //        if (remaining.Length > 0)
        //        {

        //            foreach (var name in remaining)
        //            {
        //                if (globalVariables.lstVitriky.ContainsKey(name))
        //                {
        //                    string _defaultSign = string.Format(@"{0}\{1}\default.png", Application.StartupPath, "sign");
        //                    string _signFile = string.Format(@"{0}\{1}\{2}.PNG", Application.StartupPath, "sign", globalVariables.lstVitriky[name]);
        //                    byte[] _sign = null;
        //                    if (File.Exists(_signFile))
        //                    {
        //                        _sign = Utility.fromimagepath2byte(_signFile);
        //                    }
        //                    else
        //                    {
        //                        if (File.Exists(_defaultSign))
        //                            _sign = Utility.fromimagepath2byte(_defaultSign);
        //                    }

        //                    if (builder.MoveToMergeField(name))
        //                    {
        //                        //Chèn 2 cái này mục đích đánh dấu vị trí chữ ký phục vụ công tác di chuyển con trỏ đến sau khi ký (nếu muốn)
        //                        builder.StartBookmark(name);
        //                        builder.EndBookmark(name);
        //                        if (_sign != null)
        //                        {
        //                            if (Signsize != "")
        //                            {
        //                                int w = Utility.Int32Dbnull(Signsize.Split('x')[0], 0);
        //                                int h = Utility.Int32Dbnull(Signsize.Split('x')[1], 0);
        //                                if (w > 0 && h > 0)
        //                                    builder.InsertImage(_sign, w, h);
        //                                else
        //                                    builder.InsertImage(_sign);
        //                            }
        //                            else
        //                                if (_sign != null)
        //                                builder.InsertImage(_sign);
        //                        }
        //                        //else//Không cần vì mergefield này ẩn
        //                        //    builder.InsertImage(NoImage, 10, 10);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {

        //        }

        //    }
        //}
      
    }
}
