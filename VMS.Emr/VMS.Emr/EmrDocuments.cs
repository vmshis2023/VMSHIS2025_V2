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
        public  bool  DeleteDocument(long id_benhnhan, string ma_luotkham, string loai_phieu_his, string report_code)
        {
            try
            {
                int num = 0;
                num = new Update(EmrDocument.Schema)
                     .Set(EmrDocument.Columns.TthaiXoa).EqualTo(1)
                    .Where(EmrDocument.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                    .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                    .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                    .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                     .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                     .And(EmrDocument.Columns.TthaiXoa).IsEqualTo(0)
                    .Execute();
                if(num>0)
                {
                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, loại phiếu ={2}, report code ={3}", id_benhnhan, ma_luotkham, loai_phieu_his, report_code), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                }
                return num > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        public  bool DeleteDocument(long IdFile)
        {
            try
            {
                int num = 0;
                EmrDocument deleteObject= EmrDocument.FetchByID(IdFile);
                if (deleteObject != null && !Utility.Byte2Bool(deleteObject.TthaiDuyet) && !Utility.Bool2Bool(deleteObject.TthaiKyso) && !Utility.Bool2Bool(deleteObject.TthaiKydientu))
                {
                    num = SPs.EmrXoaPhieu(deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, "", "").Execute();//Không cần truyền loại phiếu, xử lý trên thủ tục
                    if (num > 0)
                    {
                        Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, deleteObject.LoaiPhieuHis, deleteObject.ReportCode), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }
                   
                }
                return num > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        public  bool DeleteDocument(long id_phieu, string loai_phieu_his, string report_code)
        {
            try
            {
                int num = 0;
                EmrDocument deleteObject = new Select().From(EmrDocument.Schema)
                   .Where(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                           .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                           .And(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                           //.And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                           //.And(EmrDocument.Columns.TthaiXoa).IsEqualTo(0)
                    .ExecuteSingle<EmrDocument>();
                if (deleteObject != null && !Utility.Byte2Bool(deleteObject.TthaiDuyet) && !Utility.Bool2Bool( deleteObject.TthaiKyso) && !Utility.Bool2Bool(deleteObject.TthaiKydientu))
                {
                    num=SPs.EmrXoaPhieu(deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, loai_phieu_his, report_code).Execute();

                    ////num=new Delete().From(EmrDocument.Schema)
                    ////            .Where(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                    ////            .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                    ////            .And(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                    ////            .Execute();
                    //// hoặc
                    //num = new Update(EmrDocument.Schema)
                    //    .Set(EmrDocument.Columns.TthaiXoa).EqualTo(1)
                    //           .Where(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                    //           .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                    //           .And(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                    //           .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                    //           .And(EmrDocument.Columns.TthaiXoa).IsEqualTo(0)
                    //           .Execute();
                    if (num > 0)
                    {
                        Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, deleteObject.LoaiPhieuHis, deleteObject.ReportCode), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }

                }
                else
                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4} không thể bị xóa do vi phạm 1 trong các trạng thái: TthaiDuyet={5},TthaiKyso={6},TthaiKydientu={7},TthaiXoa={8}", deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, deleteObject.LoaiPhieuHis, deleteObject.ReportCode, deleteObject.TthaiDuyet, deleteObject.TthaiKyso, deleteObject.TthaiKydientu, deleteObject.TthaiXoa), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                return num > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
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
                    objDoc.FileIn = FileName;
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
                            else if (!objDoc.IsNew && Utility.Bool2Bool( objDoc.TthaiXoa))
                            {
                                new Update(EmrDocument.Schema)
                                       .Set(EmrDocument.Columns.TthaiXoa).EqualTo(0)
                                       .Where(EmrDocument.Columns.IdFile).IsEqualTo(objDoc.IdFile)
                                       .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                                       .Execute();
                                Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Cập nhật trạng thái xóa cho phiếu emr của người bệnh về trạng thái không xóa, id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", objDoc.IdBenhnhan, objDoc.MaLuotkham, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.ReportCode), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                            }    
                                else
                            {
                                if (objDoc.FileIn != "")
                                {
                                    new Update(EmrDocument.Schema)
                                        .Set(EmrDocument.Columns.FileIn).EqualTo(objDoc.FileIn)
                                         .Set(EmrDocument.Columns.PhieuBosung).EqualTo(objDoc.PhieuBosung)
                                         .Set(EmrDocument.Columns.MaPhieu).EqualTo(objDoc.MaPhieu)
                                         .Set(EmrDocument.Columns.ReportCode).EqualTo(objDoc.ReportCode)
                                         .Set(EmrDocument.Columns.MaPhieuEmr).EqualTo(objDoc.MaPhieuEmr)
                                        .Where(EmrDocument.Columns.IdFile).IsEqualTo(objDoc.IdFile)
                                        .And(EmrDocument.Columns.TthaiDuyet).IsEqualTo(0)
                                        .And(EmrDocument.Columns.FileIn).IsEqualTo("")
                                        .Execute();
                                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Cập nhật tên file pdf thành công cho phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}, tên file ={5}", objDoc.IdBenhnhan, objDoc.MaLuotkham, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.ReportCode, objDoc.FileIn), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
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
            objDoc.FileIn = FileName;
        }
        public void InitDocument(long id_benhnhan, string ma_luotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string FileIn, bool isTachPhieu = false, bool isPhieuBosung = false,string ma_phieu="")
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
                    objDoc.LaPhieutach = isTachPhieu;
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
                  
                    objDoc.ReportCode = report_code;
                    objDoc.IsNew = true;
                    objDoc.MaPhieu = ma_phieu;
                    objDoc.PhieuBosung = isPhieuBosung;
                    objDoc.FileIn = FileIn;
                    objDoc.TthaiKyso = false;
                    objDoc.TthaiKydientu = false;
                    objDoc.TthaiHuy = false;
                    objDoc.TthaiDuyet = 0;
                    objDoc.TthaiAn = false;
                    objDoc.TthaiXoa = false;
                    objDoc.TthaiChiase = false;
                }
                else
                {
                    objDoc.MaPhieu = ma_phieu;
                    objDoc.PhieuBosung = isPhieuBosung;
                    objDoc.LaPhieutach = isTachPhieu;
                    objDoc.ReportCode = report_code;
                    objDoc.FileIn = FileIn;
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
        public const string BIENBANHOICHAN = "BIENBANHOICHAN";
        public const string PHIEUDANGKYKCB = "PHIEUDANGKYKCB";
        public const string BENHAN = "BENHAN";
        public const string PHIEU_TKBA = "BA_TKBA";
        public const string PHIEUTOMTATDIEUTRINGOAITRU = "PHIEUTOMTATDIEUTRINGOAITRU";
        public const string PHIEUDIEUTRI = "PHIEUDIEUTRI";
        public const string PHIEUPTTT = "PHIEUPTTT";
        public const string PHIEU_TUVAN_PTTT = "PHIEU_TUVAN_PTTT";
        public const string PHIEU_KQCDHA = "PHIEU_KQCDHA";
        public const string PHIEU_KQXN = "PHIEU_KQXN";
        public const string DONTHUOC = "DONTHUOC";
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

        public const string PHIEUTRUYENDICH = "PHIEUTRUYENDICH";
        public const string PHIEUTHEODOI = "PHIEUTHEODOI";
        public const string PHIEUCHAMSOC = "PHIEUCHAMSOC";
    }
}
