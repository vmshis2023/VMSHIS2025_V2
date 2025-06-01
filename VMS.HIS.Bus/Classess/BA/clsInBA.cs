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
using VMS.HIS.DAL;
using VNS.Libs;

 namespace VMS.HIS.Bus
{
    public class clsInBA
    {
        public clsInBA()
        {
        }
        public static void InTomTatBA(EmrTongketBenhan ttba)
        {
            try
            {
               
                if (ttba == null || ttba.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                    return;
                }
                VKcbLuotkham objBN = Utility.getKcbBenhnhan(ttba.IdBenhnhan, ttba.MaLuotkham);
                NoitruPhieuravien objRavien = new Select().From(NoitruPhieuravien.Schema).Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(ttba.IdBenhnhan).And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(ttba.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                DataTable dtData = SPs.KcbTomtatBAIn(ttba.Id, ttba.IdBenhnhan, ttba.MaLuotkham).GetDataSet().Tables[0];
                dtData.TableName = "noitru_tomtatBA";
                List<string> lstAddedFields = new List<string>() {"gioitinh_nam","gioitinh_nu","noikhoa_khong", "noikhoa_co", "pttt_khong", "pttt_co",
                "tinhtrangravien_khoi", "tinhtrangravien_do", "tinhtrangravien_khongthaydoi",
                "tinhtrangravien_nanghon", "tinhtrangravien_tuvong", "tinhtrangravien_xinve","tinhtrangravien_khongxacdinh"};
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));

                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tomtatBA.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "noitru_tomtatBA";
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["dia_diem"] = globalVariables.gv_strDiadiem;
                drData["ngay_thang_nam"] = Utility.FormatDateTime(ttba.NgayTtba.Value);
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("gioitinh_nam", objBN.IdGioitinh.ToString() == "0" ? "1" : "0");
                dicMF.Add("gioitinh_nu", objBN.IdGioitinh.ToString() == "0" ? "0" : "1");
                dicMF.Add("noikhoa_co", Utility.Byte2Bool(ttba.Noikhoa) ? "1" : "0");
                dicMF.Add("noikhoa_khong", Utility.Byte2Bool(ttba.Noikhoa) ? "0" : "1");
                dicMF.Add("pttt_co", Utility.Byte2Bool(ttba.Pttt) ? "1" : "0");
                dicMF.Add("pttt_khong", Utility.Byte2Bool(ttba.Pttt) ? "0" : "1");
                if (objRavien != null)
                {
                    dicMF.Add("tinhtrangravien_khoi", Utility.sDbnull(objRavien.MaKquaDieutri) == "1" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_do", Utility.sDbnull(objRavien.MaKquaDieutri) == "2" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_khongthaydoi", Utility.sDbnull(objRavien.MaKquaDieutri) == "3" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_nanghon", Utility.sDbnull(objRavien.MaKquaDieutri) == "4" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_tuvong", Utility.sDbnull(objRavien.MaKquaDieutri) == "5" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_xinve", Utility.sDbnull(objRavien.MaKquaDieutri) == "6" ? "1" : "0");
                    dicMF.Add("tinhtrangravien_khongxacdinh", Utility.sDbnull(objRavien.MaKquaDieutri) == "7" ? "1" : "0");
                }
                else
                {
                    dicMF.Add("tinhtrangravien_khoi", "0");
                    dicMF.Add("tinhtrangravien_do",  "0");
                    dicMF.Add("tinhtrangravien_khongthaydoi",  "0");
                    dicMF.Add("tinhtrangravien_nanghon","0");
                    dicMF.Add("tinhtrangravien_tuvong", "0");
                    dicMF.Add("tinhtrangravien_xinve",  "0");
                    dicMF.Add("tinhtrangravien_khongxacdinh",  "0");
                }
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
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
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
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
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
