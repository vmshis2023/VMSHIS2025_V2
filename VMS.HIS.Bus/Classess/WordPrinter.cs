using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using Aspose.Words.MailMerging;
using Aspose.Words.Saving;
using BarcodeLib;
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
using VMS.HIS.Bus.Emr;
using VMS.HIS.DAL;
using VNS.Libs;
using Aspose.Words.Tables;
namespace VMS.HIS.Bus
{
    public class WordPrinter
    {
        public WordPrinter()
        {
        }
        public static string InPhieu(EmrDocuments emrdoc, DataTable dtData, string fileName, bool returnFile = false)
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv" , "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
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
                List<string> fieldNames = new List<string>();

                string PathDoc =string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
               Utility.CreateMergeFields(dtData);
                
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc),  Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                  
                   
                    doc.MailMerge.Execute(drData);
                    //Chèn ảnh chữ ký nếu đã được ký
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc( doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {
                        
                        return fileKetqua;
                    }
                    string path = fileKetqua;
                  
                   
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
        //    if (globalVariables.dtSignInfor.Rows.Count>0 && globalVariables.dtSignInfor.Columns.Count>0)//Tìm các vùng chữ kí để đưa ảnh vào
        //    {
        //        string[] remaining = doc.MailMerge.GetFieldNames();
        //        Dictionary<string, string> lstVitriky = GetDictionaryFromDataTable();
        //        if (remaining.Length > 0)
        //        {

        //            foreach (var name in remaining)
        //            {
        //                if (lstVitriky.ContainsKey(name))
        //                {
        //                    string _defaultSign = string.Format(@"{0}\{1}\default", Application.StartupPath, "sign");
        //                    string _signFile = string.Format(@"{0}\{1}\{2}", Application.StartupPath, "sign", lstVitriky[name]);
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
        //                        if (_sign != null)
        //                        {
        //                            if (Signsize!="")
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
        //                    //else//Không cần vì mergefield này ẩn
        //                    //    builder.InsertImage(NoImage, 10, 10);
        //                }
        //            }
        //        }
        //        else
        //        {

        //        }

        //    }
        //}
        public static string InPhieu(EmrDocuments emrdoc, DataTable dtData, string fileName, List<string> lstBarcodeFields , List<string> lstBarcodeValues , bool returnFile = false )
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
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
                List<string> fieldNames = new List<string>();

                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                            builder.InsertImage(globalVariables.SysLogo);
                    //int barcodeIdx = 0;
                  
                    //foreach (string barcodeField in lstBarcodeFields)
                    //{

                    //    if (builder.MoveToBookmark(barcodeField) != null)
                    //    {
                    //        builder.Font.Name = "IDAutomationHC39M";
                    //        //builder.Font.Size = 36;
                    //        builder.Write(lstBarcodeValues[barcodeIdx]);

                    //        //FieldMergeBarcode bar = (FieldMergeBarcode)builder.InsertField(FieldType.FieldMergeBarcode, true);
                    //        //bar.BarcodeType = "CODE39";
                    //        //bar.BarcodeValue = lstBarcodeValues[barcodeIdx];
                    //        //bar.AddStartStopChar = true;
                    //    }
                    //    barcodeIdx++;
                    //}
                    builder.Font.ClearFormatting();
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                   Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {
                       
                        return fileKetqua;
                    }
                   
                    string path = fileKetqua;

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
        public static string  InPhieu(DataTable dtData, string fileName, string report_code, bool returnFile = false,string CHECKED_FIELDS="")
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
                Document doc;
                DataRow drData = dtData.Rows[0];
                if(dtData.Columns.Contains("ten_benhvien"))  drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("ten_SYT")) drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_dvicaptren")) drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                if (dtData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtData.Columns.Contains("diahchi_benhvien")) drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                if (dtData.Columns.Contains("SDT_bv")) drData["SDT_bv"] = globalVariables.Branch_Phone;
                if (dtData.Columns.Contains("Hotline_bv")) drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                if (dtData.Columns.Contains("Fax_bv")) drData["Fax_bv"] = globalVariables.Branch_Fax;
                if (dtData.Columns.Contains("website_bv")) drData["website_bv"] = globalVariables.Branch_Website;
                if (dtData.Columns.Contains("email_bv")) drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + (CHECKED_FIELDS==""? "MAUBA\\BA_CHECKED_FIELDS.txt": CHECKED_FIELDS);
                List<string> lstcheckboxfields = new List<string>();
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    //Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    //fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    //doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (Utility.MoveToAny( builder,"logo") && globalVariables.SysLogo != null)
                    {
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
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
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {


                        return fileKetqua;
                    }
                   
                    string path = fileKetqua;
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
        public static string InHosoTheodoiSosinh(long id,bool returnFile=true)
        {
            try
            {
                EmrHosoTheodoiSosinh _phieu = new Select().From(EmrHosoTheodoiSosinh.Schema)
                       .Where(EmrHosoTheodoiSosinh.Columns.Id).IsEqualTo(id)
                       //.And(EmrHosoTheodoiSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrHosoTheodoiSosinh>();
                if (_phieu.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Hồ sơ theo dõi sơ sinh trước khi thực hiện in phiếu");
                    return "";
                }
                DataTable dtData = SPs.EmrHosoTheodoiSosinhLaythongtinIn(_phieu.Id).GetDataSet().Tables[0];
                dtData.TableName = "HOSOTHEODOI_SOSINH";
                dtData.Rows[0]["sngay_phieu"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayPhieu, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_kham"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayKham, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_kham_bacsi"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayKham, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_kham_ravien"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayKhamRavien, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_chamsoc"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayChamsoc, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_sangloc"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgaySangloc, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_tiem_viemganB"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayTiemViemganB, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["sngay_tiem_lao"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayTiemLao, "") : "Ngày ......./......./..........";
                dtData.Rows[0]["shiv_ngay"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.HivNgay, "") : "Ngày ......./......./..........";

                return WordPrinter.InPhieu(dtData, "HOSOTHEODOI_SOSINH.doc", "", returnFile, @"doc\HOSOTHEODOI_SOSINH_CHECKED_FIELDS.txt");



            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return "";
            }
        }
        public static string InPhieuChamSoc(DataTable dtData, string fileName, string report_code, bool returnFile = false, string CHECKED_FIELDS = "")
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
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
                List<string> fieldNames = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + (CHECKED_FIELDS == "" ? "MAUBA\\BA_CHECKED_FIELDS.txt" : CHECKED_FIELDS);
                List<string> lstcheckboxfields = new List<string>();
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    //Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    //fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    //doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (Utility.MoveToAny(builder, "logo") && globalVariables.SysLogo != null)
                    {
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
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
                    int rowIdx = 1;
                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                    //Thực hiện sao chép column thời gian và distribute column
                    int startCol = 1;  // cột đầu của vùng thời gian
                    int endCol = 3;  // cột cuối của vùng thời gian ban đầu

                    for (int i = 0; i < dtData.Rows.Count-4; i++)
                    {
                        AddTimeColumn(tab, startCol, endCol);
                        endCol++; // vùng thời gian mở rộng
                    }

                    //int idx = 1;
                    //Aspose.Words.Tables.Row template = tab.LastRow;
                    ////Tạo thông tin y lệnh trong tờ điều trị
                    //foreach (DataRow row in dtData.Rows)
                    //{
                    //    // nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);


                    //    Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)template.Clone(true);
                    //    ClearRow(newRow, 8);

                    //    Run r = new Run(doc);
                    //    SetCellValue(r, doc, newRow, 0, Utility.sDbnull(row["sngay_thuchien"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 1, Utility.sDbnull(row["ten_dichtruyen"], ""), "Times New Roman", false, 12);
                    //    SetCellValue(r, doc, newRow, 2, Utility.sDbnull(row["so_luong"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 3, Utility.sDbnull(row["so_lo"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 4, Utility.sDbnull(row["roman"], ""), "Times New Roman", true, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 5, Utility.sDbnull(row["sthoigian_batdau"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 6, Utility.sDbnull(row["sthoigian_ketthuc"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 7, Utility.sDbnull(row["ten_bacsi_chidinh"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    SetCellValue(r, doc, newRow, 8, Utility.sDbnull(row["ten_yta_thuchien"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                    //    tab.Rows.Insert(tab.Rows.Count - 1, newRow);
                    //    idx += 1;
                    //    rowIdx++;
                    //}
                    //// Xóa dòng mẫu trống cuối cùng
                    //tab.LastRow.Remove();
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {

                        return fileKetqua;
                    }
                    string path = fileKetqua;
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
        public static void AddTimeColumn(Table table, int startCol, int endCol)
        {
            table.AllowAutoFit = false;

            // 1. Thêm cột mới bằng cách clone cell tại endCol
            InsertColumn(table, endCol);
            endCol++;

            // 2. Tính tổng width vùng thời gian
            Row firstRow = table.Rows[0];
            double totalWidth = 0;

            for (int c = startCol; c <= endCol; c++)
                totalWidth += firstRow.Cells[c].CellFormat.Width;

            // 3. Chia đều width lại
            double newWidth = totalWidth / (endCol - startCol + 1);

            foreach (Row row in table.Rows)
            {
                for (int c = startCol; c <= endCol; c++)
                {
                    Cell cell = row.Cells[c];
                    cell.CellFormat.Width = newWidth;
                }
            }
        }
        private static void InsertColumn(Table table, int columnIndex)
        {
            // Lặp qua từng Row trong bảng
            foreach (Row row in table.Rows)
            {
                // Tạo Cell mới
                Cell newCell = (Cell)row.Cells[columnIndex].Clone(true);

                // Chèn vào sau vị trí columnIndex
                row.Cells.Insert(columnIndex + 1, newCell);
            }
        }
        //public static void AddNewCols(Table table, int startCol, int endCol)
        //{
        //    // Khóa tự co giãn để bảng khỏi bị đẩy rộng
        //    table.AllowAutoFit = false;

        //    // ---------------------
        //    // 1. Thêm cột vào đúng vị trí endCol
        //    // ---------------------
        //    table.InsertAfter(endCol);
        //    endCol++;   // cập nhật vì vùng thời gian mở rộng thêm 1 cột

        //    // ---------------------
        //    // 2. Tính tổng width của vùng thời gian (giữ nguyên tổng width)
        //    // ---------------------
        //    Row firstRow = table.Rows[0];

        //    double totalWidth = 0;
        //    for (int c = startCol; c <= endCol; c++)
        //        totalWidth += firstRow.Cells[c].CellFormat.Width;

        //    // ---------------------
        //    // 3. Chia lại width cho toàn bộ vùng thời gian (ĐỀU nhau)
        //    // ---------------------
        //    double newWidth = totalWidth / (endCol - startCol + 1);

        //    for (int r = 0; r < table.Rows.Count; r++)
        //    {
        //        for (int c = startCol; c <= endCol; c++)
        //        {
        //            Cell cell = table.Rows[r].Cells[c];
        //            cell.CellFormat.Width = newWidth;
        //        }
        //    }
        //}


        public static string InPhieuTruyenDich(DataTable dtData, string fileName, string report_code, bool returnFile = false, string CHECKED_FIELDS = "")
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
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
                List<string> fieldNames = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + (CHECKED_FIELDS == "" ? "MAUBA\\BA_CHECKED_FIELDS.txt" : CHECKED_FIELDS);
                List<string> lstcheckboxfields = new List<string>();
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtData);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    //Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    //fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    //doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (Utility.MoveToAny(builder, "logo") && globalVariables.SysLogo != null)
                    {
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
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
                    int rowIdx = 1;
                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                    int idx = 1;
                    Aspose.Words.Tables.Row template = tab.LastRow;
                    //Tạo thông tin y lệnh trong tờ điều trị
                    foreach (DataRow row in dtData.Rows)
                    {
                        // nguoi_tao = Utility.sDbnull(row["nguoi_tao"]);


                        Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)template.Clone(true);
                        ClearRow(newRow, 8);

                        Run r = new Run(doc);
                        SetCellValue(r, doc, newRow, 0, Utility.sDbnull(row["sngay_thuchien"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 1, Utility.sDbnull(row["ten_dichtruyen"], ""), "Times New Roman", false, 12);
                        SetCellValue(r, doc, newRow, 2, Utility.sDbnull(row["the_tich"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 3, Utility.sDbnull(row["so_lo"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 4, Utility.sDbnull(row["roman"], ""), "Times New Roman", true, 12,false,ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 5, Utility.sDbnull(row["sthoigian_batdau"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 6, Utility.sDbnull(row["sthoigian_ketthuc"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 7, Utility.sDbnull(row["ten_bacsi_chidinh"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        SetCellValue(r, doc, newRow, 8, Utility.sDbnull(row["ten_yta_thuchien"], ""), "Times New Roman", false, 12, false, ParagraphAlignment.Center);
                        tab.Rows.Insert(tab.Rows.Count - 1, newRow);
                        idx += 1;
                        rowIdx++;
                    }
                    // Xóa dòng mẫu trống cuối cùng
                    tab.LastRow.Remove();
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {
                       
                        return fileKetqua;
                    }
                    string path = fileKetqua;
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
        public static string InPhieuKhamTienMe(long id_phieu,bool returnFile = false, string CHECKED_FIELDS = "")
        {
            try
            {
                KcbPhieukhamTienme objPkTm = KcbPhieukhamTienme.FetchByID(id_phieu);
                if (objPkTm == null || objPkTm.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo phiếu khám tiền mê cho dịch vụ PTTT trước khi thực hiện in");
                    return "";
                }
                DataTable dtData = SPs.PtttPhieukhamtienmeInphieu(objPkTm.Id).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>() { "diung_chuaghinhan", "diung_thuoc", "diung_thucan",
                "ruoubia_khong", "ruoubia_nghien", "ruoubia_thinhthoang",
                "thuocla_co","thuocla_khong",
                "chatgaynghien_khong", "chatgaynghien_co",
                "tang_ha", "tmcb","daithaoduong","buougiap","COPD","COVID19","roiloannhip","vantim","laophoi","henphequan","chuaghinhan",
                "truyenmau_co","truyenmau_khong","truyenmau_taibien",
                "gmhs_co","gmhs_chuaghinhan",
                "momieng_co","momieng_khong",
                "camgiap_co","camgiap_khong",
                "tonthuongrang_co","tonthuongrang_khong",
                "gapnguaco_gioihan","gapnguaco_bìnhthuong",
                "truyentinhmach_de","truyentinhmach_kho",
                "nhiptho_deu","nhiptho_khongdeu",
                "amthoi_co","amthoi_khong",
                "khotho_co","khotho_khong",
                "ran_co","ran_khong","chuaghinhan_batthuong",
                "duongtho_co","duongtho_khong",
                "daday_co","daday_khong",
                "mucdosaumo_nhe","mucdosaumo_trungbinh","mucdosaumo_nang",
                "matmau_co","matmau_khong","chuongtrinh","capcuu"};

                dtData.TableName = "pttt_phieukham_tienme";
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = "PHIẾU KHÁM TIỀN MÊ";
                drData["sngay_kham"] = Utility.FormatDateTime(objPkTm.NgayKham);
                drData["diung_thucan"] = objPkTm.DiUng.Split(',')[2] == "1" ? drData["diung_thucan"] : "";
                drData["diung_thuoc"] = objPkTm.DiUng.Split(',')[1] == "1" ? drData["diung_thuoc"] : "";
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("diung_chuaghinhan", objPkTm.DiUng.Split(',')[0]);
                dicMF.Add("diung_thuoc", objPkTm.DiUng.Split(',')[1]);
                dicMF.Add("diung_thucan", objPkTm.DiUng.Split(',')[2]);
                dicMF.Add("ruoubia_khong", (objPkTm.ThoiquenRuoubia.Value == 0 ? 1 : 0).ToString());
                dicMF.Add("ruoubia_nghien", (objPkTm.ThoiquenRuoubia.Value == 1 ? 1 : 0).ToString());
                dicMF.Add("ruoubia_thinhthoang", (objPkTm.ThoiquenRuoubia.Value == 2 ? 1 : 0).ToString());
                dicMF.Add("thuocla_khong", (Utility.Int32Dbnull(objPkTm.ThoiquenThuocla, "0") <= 0 ? 1 : 0).ToString());
                dicMF.Add("thuocla_co", (Utility.Int32Dbnull(objPkTm.ThoiquenThuocla, "0") > 0 ? 1 : 0).ToString());
                dicMF.Add("chatgaynghien_khong", (Utility.Byte2Bool(objPkTm.ThoiquenChatgaynghien) ? 0 : 1).ToString());
                dicMF.Add("chatgaynghien_co", (Utility.Byte2Bool(objPkTm.ThoiquenChatgaynghien) ? 1 : 0).ToString());
                List<string> lstTSNK = objPkTm.TiensuNoikhoa.Split(',').ToList<string>();
                dicMF.Add("tang_ha", lstTSNK[0] == "1" ? "1" : "0");
                dicMF.Add("tmcb", lstTSNK[1] == "1" ? "1" : "0");
                dicMF.Add("daithaoduong", lstTSNK[2] == "1" ? "1" : "0");
                dicMF.Add("buougiap", lstTSNK[3] == "1" ? "1" : "0");
                dicMF.Add("COPD", lstTSNK[4] == "1" ? "1" : "0");
                dicMF.Add("COVID19", lstTSNK[5] == "1" ? "1" : "0");
                dicMF.Add("roiloannhip", lstTSNK[6] == "1" ? "1" : "0");
                dicMF.Add("vantim", lstTSNK[7] == "1" ? "1" : "0");
                dicMF.Add("laophoi", lstTSNK[8] == "1" ? "1" : "0");
                dicMF.Add("henphequan", lstTSNK[9] == "1" ? "1" : "0");
                dicMF.Add("chuaghinhan", lstTSNK[10] == "1" ? "1" : "0");
                dicMF.Add("truyenmau_co", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 1 ? "1" : "0");
                dicMF.Add("truyenmau_khong", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 0 ? "1" : "0");
                dicMF.Add("truyenmau_taibien", Utility.ByteDbnull(objPkTm.TiensuNoikhoatruyenmau) == 2 ? "1" : "0");

                dicMF.Add("gmhs_co", Utility.Byte2Bool(objPkTm.TiensuGiadinhGmhs) ? "1" : "0");
                dicMF.Add("gmhs_chuaghinhan", Utility.Byte2Bool(objPkTm.TiensuGiadinhGmhs) ? "0" : "1");
                dicMF.Add("momieng_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoMomieng) ? "1" : "0");
                dicMF.Add("momieng_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoMomieng) ? "0" : "1");
                dicMF.Add("camgiap_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoKhoangcachcamgiap) ? "1" : "0");
                dicMF.Add("camgiap_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoKhoangcachcamgiap) ? "0" : "1");
                dicMF.Add("tonthuongrang_co", Utility.Byte2Bool(objPkTm.DanhgiaduongthoNguycotonthuongrang) ? "1" : "0");
                dicMF.Add("tonthuongrang_khong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoNguycotonthuongrang) ? "0" : "1");
                dicMF.Add("gapnguaco_gioihan", Utility.Byte2Bool(objPkTm.DanhgiaduongthoGapnguaco) ? "1" : "0");
                dicMF.Add("gapnguaco_bìnhthuong", Utility.Byte2Bool(objPkTm.DanhgiaduongthoGapnguaco) ? "0" : "1");
                dicMF.Add("truyentinhmach_de", Utility.Byte2Bool(objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien) ? "1" : "0");
                dicMF.Add("truyentinhmach_kho", Utility.Byte2Bool(objPkTm.DanhgiaduongthoDuongtruyentinhmachngoaibien) ? "0" : "1");
                dicMF.Add("nhiptho_deu", Utility.Byte2Bool(objPkTm.HetimmachNhiptimdeu) ? "1" : "0");
                dicMF.Add("nhiptho_khongdeu", Utility.Byte2Bool(objPkTm.HetimmachNhiptimdeu) ? "0" : "1");
                dicMF.Add("amthoi_co", Utility.Byte2Bool(objPkTm.HetimmachAmthoi) ? "1" : "0");
                dicMF.Add("amthoi_khong", Utility.Byte2Bool(objPkTm.HetimmachAmthoi) ? "0" : "1");
                dicMF.Add("khotho_co", Utility.Byte2Bool(objPkTm.HehohapKhotho) ? "1" : "0");
                dicMF.Add("khotho_khong", Utility.Byte2Bool(objPkTm.HehohapKhotho) ? "0" : "1");
                dicMF.Add("ran_co", Utility.Byte2Bool(objPkTm.HehohapRan) ? "1" : "0");
                dicMF.Add("ran_khong", Utility.Byte2Bool(objPkTm.HehohapRan) ? "0" : "1");
                dicMF.Add("chuaghinhan_batthuong", Utility.Byte2Bool(objPkTm.CquanChuaghinhanbatthuong) ? "1" : "0");
                dicMF.Add("duongtho_co", Utility.Byte2Bool(objPkTm.DanhgianguycoDuongtho) ? "1" : "0");
                dicMF.Add("duongtho_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoDuongtho) ? "0" : "1");
                dicMF.Add("daday_co", Utility.Byte2Bool(objPkTm.DanhgianguycoDadayday) ? "1" : "0");
                dicMF.Add("daday_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoDadayday) ? "0" : "1");
                dicMF.Add("mucdosaumo_nhe", objPkTm.DanhgianguycoMucdosaumo.Value == 0 ? "1" : "0");
                dicMF.Add("mucdosaumo_trungbinh", objPkTm.DanhgianguycoMucdosaumo.Value == 1 ? "1" : "0");
                dicMF.Add("mucdosaumo_nang", objPkTm.DanhgianguycoMucdosaumo.Value == 2 ? "1" : "0");
                dicMF.Add("matmau_co", Utility.Byte2Bool(objPkTm.DanhgianguycoMatmau) ? "1" : "0");
                dicMF.Add("matmau_khong", Utility.Byte2Bool(objPkTm.DanhgianguycoMatmau) ? "0" : "1");
                dicMF.Add("chuongtrinh", Utility.Byte2Bool(objPkTm.ChuongtrinhCapcuu) ? "1" : "0");
                dicMF.Add("capcuu", Utility.Byte2Bool(objPkTm.ChuongtrinhCapcuu) ? "0" : "1");
                List<string> fieldNames = new List<string>();
               

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEUKHAM_TIENME.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEUKHAM_TIENME", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu PTTT tại thư mục sau :" + PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEUKHAM_TIENME", objPkTm.MaLuotkham, Utility.sDbnull(objPkTm.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    if (returnFile)
                    {

                        return fileKetqua;
                    }
                   

                    if (File.Exists(fileKetqua))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = fileKetqua;
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
        public void TaoPhieuCongKhai(DataTable dtData, Document doc)
        {
            List<int> lstLoai = (from p in dtData.AsEnumerable()
                                 orderby Utility.Int32Dbnull(p["stt_in"], -1)
                                 select Utility.Int32Dbnull(p["id_loaithanhtoan"], -1)

                                  ).Distinct().ToList<int>();
            Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
            int idx = 2;
            Aspose.Words.Tables.Row template = tab.Rows[2];
            Run r = new Run(doc);
            foreach (int id_loaithanhtoan in lstLoai)
            {
                DataTable dtLoaiData = dtData.Select("id_loaithanhtoan=" + id_loaithanhtoan.ToString(), "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten").CopyToDataTable();
                //Tạo các nhóm cấp 1 chứa ten_loaithanhtoan
                Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)template.Clone(true);
                ClearRow(newRow, newRow.Cells.Count - 1);
                r = new Run(doc);
                SetCellValue(r, doc, newRow, 0, Utility.sDbnull(dtLoaiData.Rows[0]["ten_loaithanhtoan"], ""), "Times New Roman", false, 12, true);
                tab.Rows.Insert(idx, newRow);
                idx++;

            }
        }
        static void ClearRow(Aspose.Words.Tables.Row newRow, int colNum)
        {
            for (int i = 0; i <= colNum; i++)
            {
                newRow.Cells[i].RemoveAllChildren();
                newRow.Cells[i].EnsureMinimum();
            }
        }
        static void SetCellValue(Run r, Document doc, Aspose.Words.Tables.Row newRow, int cellIndex, string fieldValue, string fontName = "Times New Roman", bool fontBold = false, int fontSize = 12, bool mergeCell = false, Aspose.Words.ParagraphAlignment HAlignment=ParagraphAlignment.Left)
        {
            if (mergeCell)
            {
                for (int i = 0; i < newRow.Cells.Count; i++)
                {
                    if (i == 0)
                        newRow.Cells[i].CellFormat.HorizontalMerge = CellMerge.First;
                    else
                        newRow.Cells[i].CellFormat.HorizontalMerge = CellMerge.Previous;
                }
            }
            r = new Run(doc);
            r.Font.Name = fontName;
            r.Font.Bold = fontBold;
            r.Font.Size = fontSize;
            //r.Font.Color = Color.FromArgb(102, 0, 102);
            r.Text = Utility.sDbnull(fieldValue, "");
            newRow.Cells[cellIndex].FirstParagraph.RemoveAllChildren();
            newRow.Cells[cellIndex].FirstParagraph.AppendChild(r);
            newRow.Cells[cellIndex].CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Top;
            newRow.Cells[cellIndex].FirstParagraph.ParagraphFormat.Alignment = HAlignment;
        }
        public static string InPhieuCongKhai(DataSet dsData, string fileName, string report_code, bool returnFile = false,bool saveAsPdf=false, string CHECKED_FIELDS = "")
        {
            try
            {
                DataTable dtData = dsData.Tables[0];
                DataTable dtMergeData= dsData.Tables[1];
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtMergeData, lstMoreColumns, typeof(string));
                dtMergeData.TableName = Path.GetFileNameWithoutExtension(fileName);
                Document doc;
                DataRow drData = dtMergeData.Rows[0];
                if (dtMergeData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtMergeData.Columns.Contains("ten_SYT")) drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                if (dtMergeData.Columns.Contains("ten_dvicaptren")) drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                if (dtMergeData.Columns.Contains("ten_benhvien")) drData["ten_benhvien"] = globalVariables.Branch_Name;
                if (dtMergeData.Columns.Contains("diahchi_benhvien")) drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                if (dtMergeData.Columns.Contains("SDT_bv")) drData["SDT_bv"] = globalVariables.Branch_Phone;
                if (dtMergeData.Columns.Contains("Hotline_bv")) drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                if (dtMergeData.Columns.Contains("Fax_bv")) drData["Fax_bv"] = globalVariables.Branch_Fax;
                if (dtMergeData.Columns.Contains("website_bv")) drData["website_bv"] = globalVariables.Branch_Website;
                if (dtMergeData.Columns.Contains("email_bv")) drData["email_bv"] = globalVariables.Branch_Email;
                List<string> fieldNames = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + (CHECKED_FIELDS == "" ? "MAUBA\\BA_CHECKED_FIELDS.txt" : CHECKED_FIELDS);
                List<string> lstcheckboxfields = new List<string>();
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                string PathDoc = string.Format(@"{0}\Doc\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeData);

                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu:" + PathDoc);
                    return "";
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}_{3}_{4}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));
                int w = 100;
                int h = 100;
                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    //doc.MailMerge.FieldMergingCallback = new HandleMergeBarcode();
                    //Aspose.Words.Fonts.FontSettings fontSettings = new Aspose.Words.Fonts.FontSettings();
                    //fontSettings.SetFontsFolder(@"C:\Windows\Fonts", true);  // hoặc thư mục riêng
                    //doc.FontSettings = fontSettings;
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return "";
                    }
                    if (Utility.MoveToAny(builder, "logo") && globalVariables.SysLogo != null)
                    {
                        if (sysLogosize != null)
                        {
                            w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
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
                    List<int> lstLoai = (from p in dtData.AsEnumerable()
                                         orderby Utility.Int32Dbnull(p["stt_in"], -1)
                                         select Utility.Int32Dbnull(p["id_loaithanhtoan"], -1)

                                  ).Distinct().ToList<int>();
                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[1];
                    int idx = 1; // bắt đầu sau header
                    Aspose.Words.Tables.Row rowNgay = tab.Rows[1];
                    Run r = new Run(doc);
                    //Điền dữ liệu row ngày
                    int k = 0;
                    DataTable dtNgay = dsData.Tables[2];
                    foreach (DataRow dr in dtNgay.Rows)
                    {
                       
                        SetCellValue(r, doc, rowNgay, k+3, Utility.sDbnull(dr["ngay"], ""), "Times New Roman", true, 10);
                        k++;
                    }    
                    Aspose.Words.Tables.Row template = tab.Rows[2];
                    int tong_so = 0;

                    foreach (int id_loaithanhtoan in lstLoai)
                    {
                        DataTable dtLoaiData = dtData.Select("id_loaithanhtoan=" + id_loaithanhtoan.ToString(),
                                                    "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten")
                                                    .CopyToDataTable();

                        // ===== Group cấp 1: ten_loaithanhtoan =====
                        Aspose.Words.Tables.Row rowLoai = (Aspose.Words.Tables.Row)template.Clone(true);
                        ClearRow(rowLoai, rowLoai.Cells.Count - 1);
                        SetCellValue(r, doc, rowLoai, 0, Utility.sDbnull(dtLoaiData.Rows[0]["ten_loaithanhtoan"], ""), "Times New Roman", true, 12, true);
                        tab.Rows.Insert(idx + 1, rowLoai);
                        idx++;

                        if (id_loaithanhtoan == 2) // nhóm dịch vụ cận lâm sàng có group cấp 2
                        {
                            DataTable dtCDHA = dtLoaiData.Clone();
                            List<string> lstid_loaidvu = (from p in dtLoaiData.AsEnumerable()
                                                          orderby Utility.Int32Dbnull(p["stt_in"], -1), Utility.Int32Dbnull(p["stt_hthi_loaidichvu"], -1)
                                                          select Utility.sDbnull(p["id_loaidichvu"], "-1")
                                        ).Distinct().ToList<string>();
                            foreach (string id_loaidichvu in lstid_loaidvu)
                            {
                                dtCDHA = dtLoaiData.Select("id_loaidichvu='" + id_loaidichvu.ToString() + "'", "stt_in,stt_hthi_loaidichvu ,stt_hthi_dichvu,stt_hthi_chitiet,ten").CopyToDataTable();
                                //Thêm group loại dịch vụ trong nhóm chỉ định CLS
                                Aspose.Words.Tables.Row group2 = (Aspose.Words.Tables.Row)template.Clone(true);
                                ClearRow(group2, group2.Cells.Count - 1);
                                SetCellValue(r, doc, group2, 0, Utility.sDbnull(dtCDHA.Rows[0]["ten_loaidichvu"], ""), "Times New Roman", true, 12, true);
                                tab.Rows.Insert(idx + 1, group2);
                                idx++;
                                for (int i = 0; i < dtCDHA.Rows.Count; i++)//duyệt qua các items và điền đúng thông tin
                                {
                                    tong_so = 0;
                                    Aspose.Words.Tables.Row newItem = (Aspose.Words.Tables.Row)template.Clone(true);
                                    ClearRow(newItem, newItem.Cells.Count - 1);
                                    //Cột số thứ tự trên word
                                    SetCellValue(r, doc, newItem, 0, Utility.sDbnull(i+1, ""), "Times New Roman", false, 10);
                                    //Cột tên
                                    SetCellValue(r, doc, newItem, 1, Utility.sDbnull(dtCDHA.Rows[i]["ten"], ""), "Times New Roman", false, 10);
                                    //Cột ten_donvitinh
                                    SetCellValue(r, doc, newItem, 2, Utility.sDbnull(dtCDHA.Rows[i]["ten_donvitinh"], ""), "Times New Roman", false, 10);
                                    //Điền dữ liệu vào các cột ngày
                                    k = 3;
                                    for (int sl_ngay =12; sl_ngay<= dtCDHA.Columns.Count-1; sl_ngay++)//cột ngày bắt đầu từ vị trí số 12-21=10 ngày
                                    {
                                        tong_so += Utility.Int32Dbnull(dtCDHA.Rows[i][sl_ngay], 0);
                                        SetCellValue(r, doc, newItem, k, Utility.sDbnull(dtCDHA.Rows[i][sl_ngay], ""), "Times New Roman", false, 10);
                                        k++;//K sẽ chạy từ 3-12
                                    }
                                    //Cột tổng số vị trí số 13
                                    SetCellValue(r, doc, newItem, 13, Utility.sDbnull(tong_so), "Times New Roman", true, 10);
                                    tab.Rows.Insert(idx + 1, newItem);
                                    idx++;
                                }
                            }
                        }
                        else // không có group cấp 2 → điền item luôn
                        {
                           
                            for (int i = 0; i < dtLoaiData.Rows.Count; i++)//duyệt qua các items và điền đúng thông tin
                            {
                                tong_so = 0;
                                Aspose.Words.Tables.Row newItem = (Aspose.Words.Tables.Row)template.Clone(true);
                                ClearRow(newItem, newItem.Cells.Count - 1);
                                //Cột số thứ tự trên word
                                SetCellValue(r, doc, newItem, 0, Utility.sDbnull(i + 1, ""), "Times New Roman", false, 10);
                                //Cột tên
                                SetCellValue(r, doc, newItem, 1, Utility.sDbnull(dtLoaiData.Rows[i]["ten"], ""), "Times New Roman", false, 10);
                                //Cột ten_donvitinh
                                SetCellValue(r, doc, newItem, 2, Utility.sDbnull(dtLoaiData.Rows[i]["ten_donvitinh"], ""), "Times New Roman", false, 10);
                                //Điền dữ liệu vào các cột ngày
                                k = 3;
                                for (int sl_ngay = 12; sl_ngay <= dtLoaiData.Columns.Count - 1; sl_ngay++)//cột ngày bắt đầu từ vị trí số 12-21=10 ngày
                                {
                                    tong_so += Utility.Int32Dbnull(dtLoaiData.Rows[i][sl_ngay], 0);
                                    SetCellValue(r, doc, newItem, k, Utility.sDbnull(dtLoaiData.Rows[i][sl_ngay], ""), "Times New Roman", false, 10);
                                    k++;//K sẽ chạy từ 3-12
                                }
                                //Cột tổng số vị trí số 13
                                SetCellValue(r, doc, newItem, 13, Utility.sDbnull(tong_so), "Times New Roman", true, 10);
                                tab.Rows.Insert(idx + 1, newItem);
                                idx++;
                            }
                        }
                    }


                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    if (returnFile)
                    {
                        if (saveAsPdf)
                        {
                            var options = new Aspose.Words.Saving.PdfSaveOptions
                            {
                                SaveFormat = Aspose.Words.SaveFormat.Pdf,
                                Compliance = Aspose.Words.Saving.PdfCompliance.Pdf15, // PDF/A nếu cần lưu trữ lâu dài
                                EmbedFullFonts = true, // Gắn toàn bộ font để không bị lỗi thiếu font
                                TextCompression = Aspose.Words.Saving.PdfTextCompression.Flate,
                                UseHighQualityRendering = true, // Kết xuất chất lượng cao
                                                                // OptimizeOutput = true, // Tối ưu kích thước file
                                ExportDocumentStructure = true // Giữ cấu trúc (cho accessibility)
                            };
                            fileKetqua = fileKetqua.Replace(".doc", ".pdf");
                            // Lưu thành PDF
                            doc.Save(fileKetqua, options);
                            return fileKetqua;
                        }
                        doc.Save(fileKetqua, SaveFormat.Doc);
                        return fileKetqua;

                    }
                    else
                        doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
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

                    e.Text = ""; // Xóa text gốc (tránh bị lặp)
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
        }


    }
}
