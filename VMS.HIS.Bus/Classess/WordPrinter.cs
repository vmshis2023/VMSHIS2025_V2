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
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
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
                    if (returnFile)
                    {
                        //var saveOptions = new PdfSaveOptions
                        //{
                        //    EmbedFullFonts = true,
                        //    UseCoreFonts = false,
                        //    FontEmbeddingMode = PdfFontEmbeddingMode.EmbedAll
                        //};

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
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
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
                    if (returnFile)
                    {
                        //var saveOptions = new PdfSaveOptions
                        //{
                        //    EmbedFullFonts = true,
                        //    UseCoreFonts = false,
                        //    FontEmbeddingMode = PdfFontEmbeddingMode.EmbedAll
                        //};

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
        public static string  InPhieu(DataTable dtData, string fileName, string report_code, bool returnFile = false)
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
                dtData.TableName = Path.GetFileNameWithoutExtension(fileName);
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
                List<string> fieldNames = new List<string>();
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
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
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    doc.MailMerge.Execute(drData);
                    sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("signsize").ExecuteSingle<SysSystemParameter>();
                    Utility.SignDoc(doc, builder, sysLogosize != null ? sysLogosize.SValue : "");
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    if (returnFile)
                    {
                        //var saveOptions = new PdfSaveOptions
                        //{
                        //    EmbedFullFonts = true,
                        //    UseCoreFonts = false,
                        //    FontEmbeddingMode = PdfFontEmbeddingMode.EmbedAll
                        //};

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
