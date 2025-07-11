using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using System.Data;
namespace VMS.HIS.Bus.Emr
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
                    num = SPs.EmrXoaPhieu(deleteObject.IdFile, deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, "", "").Execute();//Không cần truyền loại phiếu, xử lý trên thủ tục
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
                    num=SPs.EmrXoaPhieu(deleteObject.IdFile, deleteObject.IdBenhnhan, deleteObject.MaLuotkham, deleteObject.IdPhieu, loai_phieu_his, report_code).Execute();
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

        public bool DeleteDocument(long id_phieu, List<string> lst_loai_phieu_his, string report_code)
        {
            try
            {
                int num = 0;
                string loai_phieu_his_all = string.Join(",", lst_loai_phieu_his);
                string ma_luotkham = "";
                long id_benhnhan = -1;
                byte TthaiDuyet = 0;
                byte TthaiKyso = 0;
                byte TthaiKydientu = 0;
                byte TthaiXoa = 0;
                string LoaiPhieuHis = "";
                DataTable dtData = SPs.EmrLaythongtinPhieuXoa(-1, "", id_phieu, loai_phieu_his_all, report_code).GetDataSet().Tables[0];
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        foreach (DataRow row in dtData.Rows)
                        {
                            id_benhnhan = Utility.Int64Dbnull(row[EmrDocument.Columns.IdBenhnhan]);
                            ma_luotkham = Utility.sDbnull(row[EmrDocument.Columns.MaLuotkham]);
                            TthaiDuyet = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiDuyet]);
                            TthaiKydientu = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiKydientu]);
                            TthaiKyso = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiKyso]);
                            TthaiXoa = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiXoa]);
                            LoaiPhieuHis = Utility.sDbnull(row[EmrDocument.Columns.LoaiPhieuHis]);
                            if (row != null && !Utility.Byte2Bool(TthaiDuyet) && !Utility.Byte2Bool(TthaiKyso) && !Utility.Byte2Bool(TthaiKydientu) && !Utility.Byte2Bool(TthaiXoa))
                            {
                                num += SPs.EmrXoaPhieu(Utility.Int64Dbnull(row[EmrDocument.Columns.IdFile]), id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code).Execute();
                                if (num > 0)
                                {
                                    Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                }

                            }
                            else
                                Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4} không thể bị xóa do vi phạm 1 trong các trạng thái: TthaiDuyet={5},TthaiKyso={6},TthaiKydientu={7},TthaiXoa={8}", id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code, TthaiDuyet, TthaiKyso, TthaiKydientu, TthaiXoa), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        }
                    }
                    Scope.Complete();
                }
                return num > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        public bool DeleteDocument_WithoutTransaction(long id_phieu, List<string> lst_loai_phieu_his, string report_code)
        {
            try
            {
                int num = 0;
                string loai_phieu_his_all = string.Join(",", lst_loai_phieu_his);
                string ma_luotkham = "";
                long id_benhnhan = -1;
                byte TthaiDuyet = 0;
                byte TthaiKyso = 0;
                byte TthaiKydientu =0;
                byte TthaiXoa = 0;
                string LoaiPhieuHis = "";
                DataTable dtData = SPs.EmrLaythongtinPhieuXoa(-1, "", id_phieu, loai_phieu_his_all, report_code).GetDataSet().Tables[0];

                foreach (DataRow row in dtData.Rows)
                {
                    id_benhnhan = Utility.Int64Dbnull(row[EmrDocument.Columns.IdBenhnhan]);
                    ma_luotkham = Utility.sDbnull(row[EmrDocument.Columns.MaLuotkham]);
                    TthaiDuyet = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiDuyet]);
                    TthaiKydientu = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiKydientu]);
                    TthaiKyso = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiKyso]);
                    TthaiXoa = Utility.ByteDbnull(row[EmrDocument.Columns.TthaiXoa]);
                    LoaiPhieuHis = Utility.sDbnull(row[EmrDocument.Columns.LoaiPhieuHis]);
                    if (row != null && !Utility.Byte2Bool(TthaiDuyet) && !Utility.Byte2Bool( TthaiKyso) && !Utility.Byte2Bool(TthaiKydientu) && !Utility.Byte2Bool(TthaiXoa))
                    {
                        num += SPs.EmrXoaPhieu(Utility.Int64Dbnull(row[EmrDocument.Columns.IdFile]), id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code).Execute();
                        if (num > 0)
                        {
                            Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Xóa thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        }

                    }
                    else
                        Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4} không thể bị xóa do vi phạm 1 trong các trạng thái: TthaiDuyet={5},TthaiKyso={6},TthaiKydientu={7},TthaiXoa={8}", id_benhnhan, ma_luotkham, id_phieu, LoaiPhieuHis, report_code, TthaiDuyet, TthaiKyso, TthaiKydientu, TthaiXoa), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                }

                return num > 0;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }

        public void InitDocument(KcbLuotkham objLuotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string FileName,string loaiphieucha="")
        {
            try
            {
                if (loaiphieucha == "")
                    loaiphieucha = loai_phieu_his;//1 phiếu có thể có 2 cấp: loaiphieucha=Loại phiếu chung,loai_phieu_his=Loại phiếu con=loại phiếu được tách trong phiếu cha khi hiển thị trên EMR. Ví dụ các tờ bệnh án, phiếu chỉ định.
                SysReport objReport = null;
                if (report_code != "") objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(report_code).ExecuteSingle<SysReport>();
                objDoc = new Select().From(EmrDocument.Schema)
                        .Where(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                        .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                        .And(EmrDocument.Columns.LoaiphieuCha).IsEqualTo(loaiphieucha)
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
                    objDoc.LoaiphieuCha = loaiphieucha;
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
                int num = 0;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objDoc != null)
                        {
                            List<KeyValuePair<string, string>> lstNguoiKy = GetThongtinKy(objDoc.IdPhieu.Value, objDoc.LoaiPhieuHis);//username+ vị trí ký
                            if (objDoc.IsNew && objDoc.IdFile <= 0)
                            {
                                objDoc.Save();
                                foreach (var nguoiky in lstNguoiKy)
                                {
                                    EmrFileSignInfor fsi = new EmrFileSignInfor();
                                    fsi.NguoiKy = nguoiky.Key;
                                    fsi.IdBenhnhan = objDoc.IdBenhnhan;
                                    fsi.MaLuotkham = objDoc.MaLuotkham;
                                    fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                                    fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                                    fsi.IdPhieu = objDoc.IdPhieu.Value;
                                    fsi.TenVitriKy = nguoiky.Value;
                                    fsi.TthaiKy = false;
                                    fsi.FileId = objDoc.IdFile;
                                    fsi.Save();
                                }    
                                Utility.Log("EmrDocuments", globalVariables.UserName, string.Format("Thêm mới thành công phiếu emr của người bệnh id bệnh nhân={0}, mã lượt khám ={1}, id phiếu ={2}, loại phiếu ={3}, report code ={4}", objDoc.IdBenhnhan, objDoc.MaLuotkham, objDoc.IdPhieu, objDoc.LoaiPhieuHis, objDoc.ReportCode), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                            }
                            else if (!objDoc.IsNew && Utility.Bool2Bool(objDoc.TthaiXoa))
                            {
                                num = new Update(EmrDocument.Schema)
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
                                    num = new Update(EmrDocument.Schema)
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
                                //PA1. Xóa các thông tin ký cũ sau đó thêm mới lại
                                num = new Delete().From(EmrFileSignInfor.Schema)
                                    .Where(EmrFileSignInfor.IdPhieuColumn).IsEqualTo(objDoc.IdPhieu.Value)
                                     .And(EmrFileSignInfor.Columns.LoaiphieuHis).IsEqualTo(objDoc.LoaiPhieuHis)
                                     .And(EmrFileSignInfor.Columns.FileId).IsEqualTo(objDoc.IdFile)//thêm điều kiện xóa theo file_id
                                    .And(EmrFileSignInfor.TthaiKyColumn).IsEqualTo(0)
                                    .Execute();
                                foreach (var nguoiky in lstNguoiKy)
                                {
                                    EmrFileSignInfor fsi = new EmrFileSignInfor();
                                    fsi.NguoiKy = nguoiky.Key;
                                    fsi.IdBenhnhan = objDoc.IdBenhnhan;
                                    fsi.MaLuotkham = objDoc.MaLuotkham;
                                    fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                                    fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                                    fsi.IdPhieu = objDoc.IdPhieu.Value;
                                    fsi.TenVitriKy = nguoiky.Value;
                                    fsi.TthaiKy = false;
                                    fsi.FileId = objDoc.IdFile;
                                    fsi.Save();
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
        public  void AddSignInfor(DataTable dtData)
        {
            try
            {
                List<KeyValuePair<string, string>> lstNguoiKy = new List<KeyValuePair<string, string>>();
                foreach (DataRow dr in dtData.Rows)
                {
                    long id_phieu = Utility.Int64Dbnull(dr["id_phieu"]);
                    string loaiphieu_his = Utility.sDbnull(dr["loai_phieu_his"]);
                    lstNguoiKy = GetThongtinKy(id_phieu, loaiphieu_his);//username+ vị trí ký
                                                                        //PA1. Xóa các thông tin ký cũ sau đó thêm mới lại
                    new Delete().From(EmrFileSignInfor.Schema)
                        .Where(EmrFileSignInfor.IdPhieuColumn).IsEqualTo(id_phieu)
                         .And(EmrFileSignInfor.Columns.LoaiphieuHis).IsEqualTo(loaiphieu_his)
                        .And(EmrFileSignInfor.TthaiKyColumn).IsEqualTo(0)
                        .Execute();
                    foreach (var nguoiky in lstNguoiKy)
                    {
                        EmrFileSignInfor fsi = new EmrFileSignInfor();
                        fsi.NguoiKy = nguoiky.Key;
                        fsi.IdBenhnhan = objDoc.IdBenhnhan;
                        fsi.MaLuotkham = objDoc.MaLuotkham;
                        fsi.LoaiphieuHis = objDoc.LoaiPhieuHis;
                        fsi.LoaiphieuCha = objDoc.LoaiphieuCha;
                        fsi.IdPhieu = objDoc.IdPhieu.Value;
                        fsi.TenVitriKy = nguoiky.Value;
                        fsi.TthaiKy = false;
                        fsi.FileId = objDoc.IdFile;
                        fsi.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
        public List<KeyValuePair<string, string>> GetThongtinKy(long id_phieu,string loaiphieuhis)
        {

            List<KeyValuePair<string, string>> lstNguoiKy =new List<KeyValuePair<string, string>>();
            try
            {
                if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                    KcbChidinhcl objchidinh = KcbChidinhcl.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objchidinh.IdBacsiChidinh);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDIEUTRI)
                {
                    NoitruPhieudieutri objPhieu = NoitruPhieudieutri.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsi);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    //objBacsi = DmucNhanvien.FetchByID(objPhieu.IdDieuduong);
                    //if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUTOMTATDIEUTRINGOAITRU)
                {
                    KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objCongkham.IdBacsikham);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEURAVIEN)
                {
                    NoitruPhieuravien objRavien = NoitruPhieuravien.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objRavien.IdBacsiChuyenvien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUNHAPVIEN)
                {
                    NoitruPhieunhapvien objpnv = NoitruPhieunhapvien.FetchByID(id_phieu);
                    KcbDangkyKcb objck = KcbDangkyKcb.FetchByID(objpnv.IdKham);//sau lấy theo cột id_bacsi_nhapvien
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objck.IdBacsikham);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHUYENVIEN)
                {
                    KcbPhieuchuyenvien objpcv = KcbPhieuchuyenvien.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objpcv.IdBacsiChuyenvien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUPTTT)
                {
                    KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPttt.IdbacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CAMKET_PTTT)
                {
                    KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPttt.IdbacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CHUNGNHAN_PTTT)
                {
                    KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPttt.IdTruongkhoa);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                    objBacsi = DmucNhanvien.FetchByID(objPttt.IdGiamdoc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_TUONGTRINH_PTTT)
                {
                    KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPttt.IdbacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BIENBANHOICHAN_THONGQUAMO)
                {
                    EmrPt01Bienbanhoichanthongquamo objpt01 = EmrPt01Bienbanhoichanthongquamo.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objpt01.IdBacsyPhauthuat);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_PHAUTHUAT"));
                    objBacsi = DmucNhanvien.FetchByID(objpt01.IdBacsyGayme);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GAYME"));
                    objBacsi = DmucNhanvien.FetchByID(objpt01.IdLanhdaokhoaLamsang);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_LANHDAO_KHOALAMSANG"));
                    objBacsi = DmucNhanvien.FetchByID(objpt01.IdLanhdaoDuyetmo);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_LANHDAO_DUYETMO"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BANGKIEM_ANTOANPHAUTHUAT)
                {
                    EmrPt04BangkiemantoanPhauthuat objbkatpt = EmrPt04BangkiemantoanPhauthuat.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objbkatpt.DieuduongVongngoai);
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objbkatpt.DieuduongVongngoai, "CKS_DIEUDUONG_VONGNGOAI"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objbkatpt.DieuduongVongtrong, "CKS_DIEUDUONG_VONGTRONG"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objbkatpt.KtvDieuduongPhume, "CKS_DIEUDUONG_PHUME"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objbkatpt.BacsyGayme, "CKS_BACSI_GAYME"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objbkatpt.Phauthuatvien, "CKS_PHAUTHUATVIEN"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO1)
                {
                    EmrBa objBA = EmrBa.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                    objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO2)
                {

                }
                else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO3)
                {
                    EmrBa objBA = EmrBa.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                   if(objBacsi!=null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                    objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO4)
                {
                    EmrBa objBA = EmrBa.FetchByID(id_phieu);
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                    objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                    objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                }
            }
            catch (Exception ex)
            {

             
            }
           
            return lstNguoiKy;
        }
        public void SetFilePath(string FileName)
        {
            objDoc.FileIn = FileName;
        }
        public void InitDocument(long id_benhnhan, string ma_luotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string FileIn, bool isTachPhieu = false, bool isPhieuBosung = false,string ma_phieu="", string loaiphieucha = "")
        {
            try
            {
                if (loaiphieucha == "") loaiphieucha = loai_phieu_his;
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
                    objDoc.LoaiphieuCha = loaiphieucha;
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
        public const string TT25_GIAYCHUNGNHAN_TAINANTHUONGTICH = "TT25_GIAYCHUNGNHAN_TAINANTHUONGTICH";
        public const string TT25_GIAYXACNHAN_NGHIDUONGTHAI = "TT25_GIAYXACNHAN_NGHIDUONGTHAI";
        public const string TT25_GIAYXACNHAN_QUATRINHDIEUTRINOITRU = "TT25_GIAYXACNHAN_QUATRINHDIEUTRINOITRU";
        public const string TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH = "TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH";
        public const string TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON = "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON";


        public const string BANGKIEM_ANTOANPHAUTHUAT = "BANGKIEM_ANTOANPHAUTHUAT";
        public const string BIENBANHOICHAN = "BIENBANHOICHAN";
        public const string BIENBANHOICHAN_THONGQUAMO = "BIENBANHOICHAN_THONGQUAMO";
        public const string PHIEUDANGKYKCB = "PHIEUDANGKYKCB";
        public const string FILE_DINHKEM = "FILE_DINHKEM";
        public const string BENHAN = "BENHAN";
        public const string BENHAN_BIA = "BENHAN_BIA";
        public const string BENHAN_TO1 = "BENHAN_TO1";
        public const string BENHAN_TO2 = "BENHAN_TO2";
        public const string BENHAN_TO3 = "BENHAN_TO3";
        public const string BENHAN_TO4 = "BENHAN_TO4";
        public const string PHIEU_TKBA = "BA_TKBA";
        public const string PHIEUTOMTATDIEUTRINGOAITRU = "PHIEUTOMTATDIEUTRINGOAITRU";
        public const string PHIEUDIEUTRI = "PHIEUDIEUTRI";
        public const string PHIEUPTTT = "PHIEUPTTT";
        public const string PHIEU_CAMKET_PTTT = "PHIEU_CAMKET_PTTT";
        public const string PHIEU_CHUNGNHAN_PTTT = "PHIEU_CHUNGNHAN_PTTT";
        public const string PHIEU_TUONGTRINH_PTTT = "PHIEU_TUONGTRINH_PTTT";
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
