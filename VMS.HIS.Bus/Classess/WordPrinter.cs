using Aspose.Words;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.Emr;
using VMS.HIS.DAL;
using VNS.Libs;

 namespace VMS.HIS.Bus
{
    public class WordPrinter
    {
        public WordPrinter()
        {
        }
        public static void InPhieu(EmrDocuments emrdoc, DataTable dtData, string fileName)
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv" , "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
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
                    return;
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
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
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
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;
                    //Lưu pdf đẩy ra EMR
                    if (emrdoc !=null && emrdoc.objDoc !=null && emrdoc.objDoc.LoaiPhieuHis!="")
                    {
                        string newDocfile = string.Format("{0}{1}{2}.doc", Path.GetDirectoryName(fileKetqua), Path.DirectorySeparatorChar, THU_VIEN_CHUNG.GetGUID());
                        File.Copy(fileKetqua, newDocfile);
                        Pdf2emrItem newItem = new Pdf2emrItem(emrdoc,doc, newDocfile);
                        newItem.DoPdf(null);
                        //globalVariables._Pdf2HisManager.AddItems(newItem);
                    }
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
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public static void InPhieu(DataTable dtData, string fileName, string report_code)
        {
            try
            {
                List<string> lstMoreColumns = new List<string>() { "ten_benhvien", "ten_SYT", "diahchi_benhvien", "SDT_bv", "Hotline_bv", "Fax_bv", "website_bv", "email_bv" };
                Utility.AddColums2DataTable(ref dtData, lstMoreColumns, typeof(string));
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
                    return;
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
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
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
                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
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
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
