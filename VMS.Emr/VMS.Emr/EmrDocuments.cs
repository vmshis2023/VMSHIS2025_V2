using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.Emr
{
    public class EmrDocuments
    {
        public EmrDocument objDoc;
        public EmrDocuments()
        {

        }
        public void DeleteDocument(long id_benhnhan, string ma_luotkham, long id_phieu, string loai_phieu_his, string report_code)
        {
            try
            {
             int num=   new Delete().From(EmrDocument.Schema)
                    .Where(EmrDocument.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                    .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                    .And(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                    .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                    .Execute();
                if(num>0)
                {
                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", id_benhnhan, ma_luotkham, id_phieu, loai_phieu_his, report_code), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                }    
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        public void DeleteDocument(long IdFile)
        {
            try
            {
                EmrDocument deleteObject= EmrDocument.FetchByID(IdFile);
                if (deleteObject != null && deleteObject.TthaiDuyet == 0)
                {
                    int num = new Delete().From(EmrDocument.Schema)
                           .Where(EmrDocument.Columns.IdFile).IsEqualTo(IdFile)
                           .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                           .Execute();
                    if (num > 0)
                    {
                        Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, deleteObject.LoaiPhieuHis, deleteObject.ReportCode), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }
        public void InitDocument(KcbLuotkham objLuotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string FileName)
        {
            try
            {
                SysReport objReport = null;
                if (report_code != "") objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(report_code).ExecuteSingle<SysReport>();
                objDoc = new Select().From(EmrDocument.Schema)
                        .Where(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                        .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                        .And(EmrDocument.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                         .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                        .ExecuteSingle<EmrDocument>();
                if (objDoc == null)
                {

                    objDoc = new EmrDocument();
                    objDoc.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objDoc.MaLuotkham = objLuotkham.MaLuotkham;
                    objDoc.IdPhieu = id_phieu;
                    objDoc.FileData = null;
                    objDoc.NgayPhieu = ngay_phieu;
                    objDoc.LoaiPhieuHis = loai_phieu_his;
                    objDoc.MaPhieuEmr = objReport != null ? objReport.MaPhieuEmr : "KHAC";
                    objDoc.NguoiTao = nguoi_tao;
                    objDoc.NgayTao = DateTime.Now;
                    objDoc.IdKhoa = id_khoa;
                    objDoc.IdPhong = id_phong;
                    objDoc.NguonTao = 0;
                    objDoc.Noitru = noitru;
                    objDoc.TthaiHuy = false;
                    objDoc.TthaiDuyet = 0;
                    objDoc.TthaiAn = false;
                    objDoc.ReportCode = report_code;
                    objDoc.IsNew = true;
                    objDoc.FileName = FileName;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }




        }
        public void Save()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objDoc != null)
                        {
                            if (objDoc.IsNew && objDoc.IdFile <= 0)
                            {
                                objDoc.Save();
                                Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Thêm mới thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", objDoc.IdBenhnhan, objDoc.MaLuotkham, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.ReportCode), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            }
                            else
                            {
                                if (objDoc.FileName != "")
                                {
                                    new Update(EmrDocument.Schema)
                                        .Set(EmrDocument.Columns.FileName).EqualTo(objDoc.FileName)
                                         .Set(EmrDocument.Columns.ReportCode).EqualTo(objDoc.ReportCode)
                                         .Set(EmrDocument.Columns.MaPhieuEmr).EqualTo(objDoc.MaPhieuEmr)
                                        .Where(EmrDocument.Columns.IdFile).IsEqualTo(objDoc.IdFile)
                                        .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                                        .And(EmrDocument.Columns.FileName).IsEqualTo("")
                                        .Execute();
                                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Cập nhật tên file pdf thành công cho phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}, tên file ={5}", objDoc.IdBenhnhan, objDoc.MaLuotkham, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.ReportCode, objDoc.FileName), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                                }
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        public void SetFilePath(string FileName)
        {
            objDoc.FileName = FileName;
        }
        public void InitDocument(long id_benhnhan, string ma_luotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string filePath,bool isTachPhieu=false)
        {
            try
            {
                SysReport objReport = null;
                if (report_code != "") objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(report_code).ExecuteSingle<SysReport>();
                if (isTachPhieu)
                    objDoc = new Select().From(EmrDocument.Schema)
                            .Where(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                            .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                            .And(EmrDocument.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                            .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                            .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                            .ExecuteSingle<EmrDocument>();
                else
                    objDoc = new Select().From(EmrDocument.Schema)
                            .Where(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                            .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                            .And(EmrDocument.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                            .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                            .ExecuteSingle<EmrDocument>();
                if (objDoc == null)
                {

                    objDoc = new EmrDocument();
                    objDoc.IdBenhnhan = id_benhnhan;
                    objDoc.MaLuotkham = ma_luotkham;
                    objDoc.IdPhieu = id_phieu;
                    objDoc.FileData = null;
                    objDoc.NgayPhieu = ngay_phieu;
                    objDoc.LoaiPhieuHis = loai_phieu_his;
                    objDoc.MaPhieuEmr = objReport != null ? objReport.MaPhieuEmr : "KHAC";
                    objDoc.NguoiTao = nguoi_tao;
                    objDoc.NgayTao = DateTime.Now;
                    objDoc.IdKhoa = id_khoa;
                    objDoc.IdPhong = id_phong;
                    objDoc.NguonTao = 0;
                    objDoc.Noitru = noitru;
                    objDoc.TthaiHuy = false;
                    objDoc.TthaiDuyet = 0;
                    objDoc.TthaiAn = false;
                    objDoc.ReportCode = report_code;
                    objDoc.IsNew = true;
                    objDoc.FilePath = filePath;

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
    public class Loaiphieu_HIS
    {
        public const string PHIEUDANGKYKCB = "PHIEUDANGKYKCB";
        public const string BENHAN = "BENHAN";
        public const string PHIEUDIEUTRI = "PHIEUDIEUTRI";
        public const string PHIEUCHIDINH = "PHIEUCHIDINH";
        public const string PHIEUCHUYENVIEN = "PHIEUCHUYENVIEN";
        public const string PHIEURAVIEN = "PHIEURAVIEN";
        public const string PHIEUNHAPVIEN = "PHIEUNHAPVIEN";
        public const string PHIEUKHAM_KSK = "PHIEUKHAM_KSK";
        public const string PHIEUKHAMTHAI = "PHIEUKHAMTHAI";
        public const string CHUYENKHOA = "CHUYENKHOA";
        public const string UPDATETHONGTIN = "UPDATETHONGTIN";
        public const string BIENLAITT = "BIENLAITT";
        public const string BANGKEKCB = "BANGKEKCB";
    }
}
