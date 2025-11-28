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
        long id_benhnhan = -1;
        string ma_luotkham = "";
        public bool Force2Saved = false;
        public EmrDocuments()
        {

        }
        public static bool KiemtratrangthaiKyphieu(long id_benhnhan, string ma_luotkham, long id_phieu, string loaiphieu_cha, string loaiphieu_his)
        {
            DataTable dtData = SPs.EmrKiemtraTrangthaikyPhieu(id_benhnhan, ma_luotkham, id_phieu, loaiphieu_cha, loaiphieu_his).GetDataSet().Tables[0];
            if (dtData != null && dtData.AsEnumerable().Any(c => Utility.ByteDbnull(c["tthai_ky"]) == 1))
            {
                Utility.ShowMsg(string.Format("Phiếu đã được\r\n{0}\r\nDo vậy bạn không được phép Sửa/Xóa", Utility.sDbnull(dtData.Rows[0]["sign_infor"])));
                return true;
            }
            return false;
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
                //Kiểm tra nếu người bệnh đã khởi tạo BA mới tạo phiếu. Các bệnh nhân thường, KSK muốn có Hồ sơ thì sau này có thể dùng nút reset để kéo về
                if (objDoc!=null)
                {
                    KcbLuotkham objLK = new Select()
                        .From(KcbLuotkham.Schema)
                        .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDoc.IdBenhnhan)
                        .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDoc.MaLuotkham)
                        .ExecuteSingle<KcbLuotkham>();
                    if (!Force2Saved && ( objLK ==null ||(objLK != null && Utility.Int64Dbnull(objLK.IdBa, 0) <= 0 && Utility.Int64Dbnull(objLK.IdNhapvien, 0) <= 0)))//Không lưu hồ sơ
                        return;
                }    

                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (objDoc != null)
                        {
                           

                            List<KeyValuePair<string, string>> lstNguoiKy = GetThongtinKy(objDoc.IdPhieu.Value, objDoc.LoaiPhieuHis, objDoc.LoaiphieuCha);//username+ vị trí ký
                            if (objDoc.IsNew && objDoc.IdFile <= 0)
                            {
                                objDoc.Save();
                                foreach (var nguoiky in lstNguoiKy)
                                {
                                    EmrFileSignInfor fsi = new Select().From(EmrFileSignInfor.Schema)
                                        .Where(EmrFileSignInfor.Columns.IdBenhnhan).IsEqualTo(objDoc.IdBenhnhan)
                                        .And(EmrFileSignInfor.Columns.MaLuotkham).IsEqualTo(objDoc.MaLuotkham)
                                         .And(EmrFileSignInfor.Columns.LoaiphieuHis).IsEqualTo(objDoc.LoaiPhieuHis)
                                         .And(EmrFileSignInfor.Columns.IdPhieu).IsEqualTo(objDoc.IdPhieu.Value)
                                         .And(EmrFileSignInfor.Columns.NguoiKy).IsEqualTo(nguoiky.Key)
                                         .ExecuteSingle<EmrFileSignInfor>();
                                    if (fsi == null)
                                    {
                                        fsi = new EmrFileSignInfor();
                                        fsi.IsNew = true;
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
                                         .Set(EmrDocument.Columns.IdPhieu).EqualTo(objDoc.IdPhieu)
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
        public bool CapnhatThongtinNguoiKyTrenPhieu(long id_phieu,long id_benhnhan, string ma_luotkham, string loai_phieu_cha,string loai_phieu_his,ref string Msg)
        {
            try
            {
                int num = SPs.EmrXoaThongTinKyTrenPhieu(id_benhnhan, ma_luotkham, id_phieu, loai_phieu_cha, loai_phieu_his).Execute();
                List<KeyValuePair<string, string>> lstNguoiKy = GetThongtinKy(id_phieu, loai_phieu_his, loai_phieu_cha);//username+ vị trí ký
                foreach (var nguoiky in lstNguoiKy)
                {
                    EmrFileSignInfor fsi = new Select().From(EmrFileSignInfor.Schema)
                        .Where(EmrFileSignInfor.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                        .And(EmrFileSignInfor.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                         .And(EmrFileSignInfor.Columns.LoaiphieuHis).IsEqualTo(loai_phieu_his)
                         .And(EmrFileSignInfor.Columns.IdPhieu).IsEqualTo(id_phieu)
                         .And(EmrFileSignInfor.Columns.NguoiKy).IsEqualTo(nguoiky.Key)
                         .ExecuteSingle<EmrFileSignInfor>();
                    if (fsi == null)
                    {
                        fsi = new EmrFileSignInfor();
                        fsi.IsNew = true;
                        fsi.NguoiKy = nguoiky.Key;
                        fsi.IdBenhnhan = id_benhnhan;
                        fsi.MaLuotkham = ma_luotkham;
                        fsi.LoaiphieuHis = loai_phieu_his;
                        fsi.LoaiphieuCha = loai_phieu_cha;
                        fsi.IdPhieu = id_phieu;
                        fsi.TenVitriKy = nguoiky.Value;
                        fsi.TthaiKy = false;
                        fsi.FileId = 0;
                        fsi.Save();
                    }
                    else
                    {
                        if (Utility.Bool2Bool(fsi.TthaiKy) || Utility.Bool2Bool(fsi.TthaiKyso) || Utility.Bool2Bool(fsi.TthaiKydientu))
                        {
                            Msg = string.Format("Vị trí ký của người dùng {0} trên phiếu đã được ký nên không thể cập nhật. Vui lòng hủy ký trước khi thực hiện cập nhật thông tin mới", nguoiky.Key);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                Msg = ex.Message;
                return false;
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
                    string loaiphieu_cha = Utility.sDbnull(dr["loaiphieu_cha"]);
                    lstNguoiKy = GetThongtinKy(id_phieu, loaiphieu_his, loaiphieu_cha);//username+ vị trí ký
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
        public List<KeyValuePair<string, string>> GetThongtinKy(long id_phieu,string loaiphieuhis,string loaiphieucha="",long id_benhnhan=-1,string ma_luotkham="")
        {

            List<KeyValuePair<string, string>> lstNguoiKy =new List<KeyValuePair<string, string>>();
            try
            {
                if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHIDINH)
                {
                    KcbChidinhcl objPhieu = KcbChidinhcl.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiChidinh);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDIEUTRI)
                {
                    if (id_phieu <= 0)
                    {
                        DataTable dtSignInfor = SPs.EmrLaythongtinChukyPhieu(ma_luotkham, id_benhnhan, id_phieu, loaiphieuhis, loaiphieucha).GetDataSet().Tables[0];
                        foreach(DataRow dr in dtSignInfor.Rows)
                        {
                            DmucNhanvien objBacsi = DmucNhanvien.FetchByID(Utility.Int16Dbnull( dr["id_bacsi"]));
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                            objBacsi = DmucNhanvien.FetchByID(Utility.Int16Dbnull(dr["id_dieuduong"]));
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG"));
                        }    
                    }
                    else
                    {
                        NoitruPhieudieutri objPhieu = NoitruPhieudieutri.FetchByID(id_phieu);
                        if (objPhieu == null) return lstNguoiKy;
                        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsi);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                        objBacsi = DmucNhanvien.FetchByID(objPhieu.IdDieuduong);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG"));
                    }
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUDANGKYKCB)
                {
                    KcbDangkyKcb objPhieu = KcbDangkyKcb.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi =new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(objPhieu.NguoiTao).ExecuteSingle<DmucNhanvien>();
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NHANVIEN"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUTOMTATDIEUTRINGOAITRU)
                {
                    KcbDangkyKcb objPhieu = KcbDangkyKcb.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsikham);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEURAVIEN)
                {
                    NoitruPhieuravien objPhieu = NoitruPhieuravien.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiChuyenvien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUNHAPVIEN)
                {
                    NoitruPhieunhapvien objPhieu = NoitruPhieunhapvien.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    KcbDangkyKcb objck = KcbDangkyKcb.FetchByID(objPhieu.IdKham);//sau lấy theo cột id_bacsi_nhapvien
                    if (objck == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objck.IdBacsikham);
                   
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUCHUYENVIEN)
                {
                    KcbPhieuchuyenvien objPhieu = KcbPhieuchuyenvien.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiChuyenvien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUKHAMTHAI)
                {
                    KcbPhieukhamthai objPhieu = KcbPhieukhamthai.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsi);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUKHAM_TIENME)
                {
                    KcbPhieukhamTienme objPhieu = KcbPhieukhamTienme.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBsigayme);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GAYME"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEUPTTT)
                {
                    KcbPhieupttt objPhieu = KcbPhieupttt.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdbacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CAMKET_PTTT)
                {
                    EmrPhieucamketchapnhanPttt objPhieu = EmrPhieucamketchapnhanPttt.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiGaymehoisuc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GAYME"));

                    //KcbPhieupttt objPttt = KcbPhieupttt.FetchByID(id_phieu);
                    //DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPttt.IdbacsiPttt);
                    //if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CAMKET_CHAPNHAN_PTTT)
                {
                    EmrPhieucamketchapnhanPttt objPhieu = EmrPhieucamketchapnhanPttt.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiGaymehoisuc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GAYME"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_CHUNGNHAN_PTTT)
                {
                    KcbPhieupttt objPhieu = KcbPhieupttt.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdTruongkhoa);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdGiamdoc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_TUONGTRINH_PTTT)
                {
                    KcbPhieupttt objPhieu = KcbPhieupttt.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdbacsiPttt);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BIENBANHOICHAN_THONGQUAMO)
                {
                    EmrPt01Bienbanhoichanthongquamo objPhieu = EmrPt01Bienbanhoichanthongquamo.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsyPhauthuat);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_PHAUTHUAT"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsyGayme);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GAYME"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdLanhdaokhoaLamsang);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_LANHDAO_KHOALAMSANG"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdLanhdaoDuyetmo);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_LANHDAO_DUYETMO"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT)
                {
                    EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat objPhieu = EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiGiao);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOI_GIAO"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiNhan);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOI_NHAN"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BIENBANHOICHAN)
                {
                    KcbBienbanhoichan objPhieu = KcbBienbanhoichan.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.BacsiDexuat);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                    AddThongtinKy(Utility.sDbnull(objPhieu.IdbacsiPttt), lstNguoiKy, "CKS_BACSI_PHAUTHUAT");
                    AddThongtinKy(Utility.sDbnull(objPhieu.IdbacsiGayme), lstNguoiKy, "CKS_BACSI_GAYME");
                    objBacsi = DmucNhanvien.FetchByID(Utility.Int32Dbnull( objPhieu.ChuToa));
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_CHUTOA"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.BANGKIEM_ANTOANPHAUTHUAT)
                {
                    EmrPt04BangkiemantoanPhauthuat objPhieu = EmrPt04BangkiemantoanPhauthuat.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.DieuduongVongngoai);
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objPhieu.DieuduongVongngoai, "CKS_DIEUDUONG_VONGNGOAI"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objPhieu.DieuduongVongtrong, "CKS_DIEUDUONG_VONGTRONG"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objPhieu.KtvDieuduongPhume, "CKS_DIEUDUONG_PHUME"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objPhieu.BacsyGayme, "CKS_BACSI_GAYME"));
                    lstNguoiKy.Add(new KeyValuePair<string, string>(objPhieu.Phauthuatvien, "CKS_PHAUTHUATVIEN"));
                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYCHUNGNHAN_TAINANTHUONGTICH)
                {
                    Tt25GiaychungnhanThuongtich objPhieu = Tt25GiaychungnhanThuongtich.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsy);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoidaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_NGHIDUONGTHAI)
                {
                    Tt25GiayxacnhanNghiduongthai objPhieu = Tt25GiayxacnhanNghiduongthai.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsy);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoidaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON)
                {
                    Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon objPhieu = Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoidaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_QUATRINHDIEUTRINOITRU)
                {
                    Tt25GiayxacnhanQuatrinhdieutrinoitru objPhieu = Tt25GiayxacnhanQuatrinhdieutrinoitru.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsy);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoidaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH)
                {
                    Tt25Giayxacnhanquatrinhdieutrivosinh objPhieu = Tt25Giayxacnhanquatrinhdieutrivosinh.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsy);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoidaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.HOSOTHEODOI_SOSINH)
                {
                    EmrHosoTheodoiSosinh objPhieu = EmrHosoTheodoiSosinh.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsy);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdDieuduong);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsyKham);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));

                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiChamsoc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_CHAMSOC"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiSangloc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_SANGLOC"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoithuchienHiv);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_HIV"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoithuchienTiemviemganB);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_TIEMVIEMGANB"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoitiem);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_TIEM"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoitiemLao);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_TIEMLAO"));
                   

                }
                else if (loaiphieuhis == Loaiphieu_HIS.GIAY_CHUNGSINH)
                {
                    EmrGiayChungsinh objPhieu = EmrGiayChungsinh.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiDode);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIDODE"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdNguoiDaidien);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                   

                }
               
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI)
                {
                    EmrPhieubangiaonguoibenhchuyenkhoa objPhieu = EmrPhieubangiaonguoibenhchuyenkhoa.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiBangiao);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_GIAO"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdBacsiNhan);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_NHAN"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG)
                {
                    EmrPhieubangiaonguoibenhchuyenkhoa objPhieu = EmrPhieubangiaonguoibenhchuyenkhoa.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdDieuduongKhoachuyen);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG_GIAO"));
                    objBacsi = DmucNhanvien.FetchByID(objPhieu.IdDieuduongKhoacnhan);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_DIEUDUONG_NHAN"));

                }
                else if (loaiphieuhis == Loaiphieu_HIS.PHIEU_TTBA)
                {
                    EmrTomtatBa objPhieu = EmrTomtatBa.FetchByID(id_phieu);
                    if (objPhieu == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objPhieu.IdGiamdoc);
                    if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                  

                }
                else if(LoaiBA.All.Contains( loaiphieucha))
                {
                    EmrBa objBA = EmrBa.FetchByID(id_phieu);
                    if (objBA == null) return lstNguoiKy;
                    DmucNhanvien objBacsi = null;
                    if (Loaiphieu_HIS.All.Contains(loaiphieuhis))
                    {
                         objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                        if (loaiphieucha == "15/BV1")//Bệnh án ngoại trú
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                        }
                        else
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                        }
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                    }
                    else if(loaiphieuhis==Loaiphieu_HIS.BENHAN_TO1)
                    {
                        if (loaiphieucha != "BA-16")//Bệnh án ngoại trú mẫu cũ để hết ở tờ 2
                        {
                            if (loaiphieucha == "15/BV1")//Bệnh án ngoại trú mẫu tt32
                            {
                                objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                                if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                            }
                            else
                            {
                                objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                                if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                            }

                            objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                        }
                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO2)
                    {
                        if (loaiphieucha == "BA-16")//Bệnh án ngoại trú mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                        }
                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO3)
                    {
                        if (loaiphieucha == "BA-16")//Bệnh án ngoại trú mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                        }
                        else
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                        }
                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO4)
                    {
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO5)
                    {
                        if (loaiphieucha == "BA-01")//Bệnh án ngoại khoa mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                        }
                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO6)
                    {
                        if (loaiphieucha == "BA-01")//Bệnh án ngoại khoa mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                        }
                        else if (loaiphieucha == "BA-02")//Bệnh án ngoại khoa mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                         
                        }

                    }
                    else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO7)
                    {
                        if (loaiphieucha == "BA-02")//Bệnh án ngoại khoa mẫu cũ
                        {
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                            objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                            if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                        }
                    }
                }
                //else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO1)
                //{
                //    if (loaiphieucha == Loaiphieu_HIS.BA_NAMKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAITRU)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NOIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_PHUKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_SANKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_IVF_VO)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_IVF_CHONG)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdTruongkhoadieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_TRUONGKHOA"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdGiamdoc);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_GIAMDOC"));
                //    }
                //}
                //else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO2)
                //{

                //}
                //else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO3)
                //{
                //    if (loaiphieucha == Loaiphieu_HIS.BA_NAMKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAITRU)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NOIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_PHUKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_SANKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_IVF_CHONG)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_IVF_VO)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        DmucNhanvien objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiKham);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_KHAM"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiLamBA);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_LAMBA"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_SOSINH)
                //    {

                //    }

                //}
                //else if (loaiphieuhis == Loaiphieu_HIS.BENHAN_TO4)
                //{
                //    DmucNhanvien objBacsi = null;
                //    if (loaiphieucha == Loaiphieu_HIS.BA_NAMKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //   else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NGOAITRU)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_NOIKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    else if (loaiphieucha == Loaiphieu_HIS.BA_PHUKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    if (loaiphieucha == Loaiphieu_HIS.BA_SANKHOA)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    if (loaiphieucha == Loaiphieu_HIS.BA_IVF_CHONG)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    if (loaiphieucha == Loaiphieu_HIS.BA_IVF_VO)
                //    {
                //        EmrBa objBA = EmrBa.FetchByID(id_phieu);
                //        if (objBA == null) return lstNguoiKy;
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdBacsiDieutri);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_BACSI_DIEUTRI"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoigiaoHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOIGIAO_HOSO"));
                //        objBacsi = DmucNhanvien.FetchByID(objBA.IdNguoinhanHoso);
                //        if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, "CKS_NGUOINHAN_HOSO"));
                //    }
                //    if (loaiphieucha == Loaiphieu_HIS.BA_SOSINH)
                //    {

                //    }
                //}
            }
            catch (Exception ex)
            {

             
            }
           
            return lstNguoiKy;
        }
        void AddThongtinKy(string strID, List<KeyValuePair<string, string>> lstNguoiKy,string tenvitri_ky)
        {
            List<int> lstId = Utility.ToListInt_Linq(strID);
            DmucNhanvien objBacsi = null;
            foreach (int id in lstId)
            {
                 objBacsi = DmucNhanvien.FetchByID(id);
                if (objBacsi != null) lstNguoiKy.Add(new KeyValuePair<string, string>(objBacsi.UserName, tenvitri_ky));
            }
        }
        public void SetFilePath(string FileName)
        {
            objDoc.FileIn = FileName;
        }
        public void InitDocument(long id_benhnhan, string ma_luotkham, long id_phieu, DateTime ngay_phieu, string loai_phieu_his, string report_code, string nguoi_tao, Int16 id_khoa, Int16 id_phong, bool noitru, string FileIn, bool isTachPhieu = false, bool isPhieuBosung = false,string ma_phieu="", string loaiphieucha = "")
        {
            try
            {
                this.id_benhnhan = id_benhnhan;
                this.ma_luotkham = ma_luotkham;
                if (loaiphieucha == "") loaiphieucha = loai_phieu_his;
                SysReport objReport = null;
                if (report_code != "") objReport = new Select().From(SysReport.Schema).Where(SysReport.Columns.MaBaocao).IsEqualTo(report_code).ExecuteSingle<SysReport>();
                if (loai_phieu_his == Loaiphieu_HIS.PHIEUDIEUTRI)//Phiếu điều trị thì xem xét tách thành 2 luồng, 1 luồng chỉ tạo duy nhất 1 tờ điều trị trong gáy, 1 luồng tất cả tờ lẻ+ 1 tờ chung
                {
                    if (Utility.Laygiatrithamsohethong("EMR_TODIEUTRI_SINGLE", "1", true) == "1")
                    {
                        id_phieu = -1;
                    }
                }
                if (isTachPhieu)//nếu tách phiếu thì tìm theo điều kiện cả report code
                    objDoc = new Select().From(EmrDocument.Schema)
                            .Where(EmrDocument.Columns.IdPhieu).IsEqualTo(id_phieu)
                            .And(EmrDocument.Columns.LoaiPhieuHis).IsEqualTo(loai_phieu_his)
                            .And(EmrDocument.Columns.IdBenhnhan).IsEqualTo(id_benhnhan)
                            .And(EmrDocument.Columns.MaLuotkham).IsEqualTo(ma_luotkham)
                            .And(EmrDocument.Columns.ReportCode).IsEqualTo(report_code)
                            .ExecuteSingle<EmrDocument>();
                else//Không tách phiếu thì tìm theo id phiếu là đủ
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
        public const string BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT = "BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT";
        public const string PHIEUDANGKYKCB = "PHIEUDANGKYKCB";
        public const string FILE_DINHKEM = "FILE_DINHKEM";
        public const string BA_NGOAITRU = "BA_NGOAITRU";//"15/BV1";
        public const string BA_NGOAITRU_BA = "BA_NGOAITRU_BA";//"BA-16";
        public const string BA_NOIKHOA = "BA_NOIKHOA";//"01/BV1","BA-01";
        public const string BA_NOIKHOA_BA = "BA_NOIKHOA_BA";//"01/BV1","BA-01";
        public const string BA_NHIKHOA = "BA_NHIKHOA";//"02/BV1";
        public const string BA_PHUKHOA = "BA_PHUKHOA";//"04/BV1";
        public const string BA_SANKHOA = "BA_SANKHOA";//"05/BV1";
        public const string BA_SOSINH = "BA_SOSINH";//"06/BV1";
        public const string BA_NGOAIKHOA = "BA_NGOAIKHOA";//"10/BV1""BA-02";
        public const string BA_NGOAIKHOA_BA = "BA_NGOAIKHOA_BA";//"10/BV1""BA-02";
        public const string BA_NAMKHOA = "BA_NAMKHOA";
        public const string BENH_AN = "BENH_AN";
        public const string BA_IVF_VO = "BAIVF_VO";
        public const string BA_IVF_CHONG = "BAIVF_CHONG";
        public const string BENHAN_BIA = "BENHAN_BIA";
        public const string BENHAN_TO1 = "BENHAN_TO1";
        public const string BENHAN_TO2 = "BENHAN_TO2";
        public const string BENHAN_TO3 = "BENHAN_TO3";
        public const string BENHAN_TO4 = "BENHAN_TO4";
        public const string BENHAN_TO5 = "BENHAN_TO5";
        public const string BENHAN_TO6 = "BENHAN_TO6";
        public const string BENHAN_TO7 = "BENHAN_TO7";
        public const string PHIEU_TKBA = "BA_TKBA";
        public const string PHIEU_TTBA = "PHIEU_TTBA";
        public const string PHIEUTOMTATDIEUTRINGOAITRU = "PHIEUTOMTATDIEUTRINGOAITRU";
        public const string PHIEUDIEUTRI = "PHIEUDIEUTRI";
        public const string PHIEUPTTT = "PHIEU_PTTT";
        public const string PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA = "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA";
        public const string PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI = "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_BACSI";
        public const string PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG = "PHIEU_BANGIAO_NGUOIBENHCHUYENKHOA_DIEUDUONG";
        public const string PHIEU_CAMKET_PTTT = "PHIEU_CAMKET_PTTT";
        public const string PHIEU_CAMKET_CHAPNHAN_PTTT = "PHIEU_CAMKET_CHAPNHAN_PTTT";
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
        public const string PHIEUKHAM_TIENME = "PHIEUKHAM_TIENME";
        public const string CHUYENKHOA = "CHUYENKHOA";
        public const string UPDATETHONGTIN = "UPDATETHONGTIN";
        public const string BIENLAITT = "BIENLAITT";
        public const string BANGKEKCB = "BANGKEKCB";
        public const string PHIEU_CONGKHAI = "PHIEU_CONGKHAI";
        public const string PHIEUTHEODOI_TRUYENDICH = "PHIEUTHEODOI_TRUYENDICH";
        public const string PHIEUTHEODOI_CHUCNANGSONG = "PHIEUTHEODOI_CHUCNANGSONG";
        public const string PHIEUCHAMSOC = "PHIEUCHAMSOC";
        public const string HOSOTHEODOI_SOSINH = "HOSOTHEODOI_SOSINH";
        public const string GIAY_CHUNGSINH = "GIAY_CHUNGSINH";
        public static readonly HashSet<string> All = new HashSet<string>
    {
        BA_NOIKHOA,
         BA_NOIKHOA_BA,
        BA_NHIKHOA,
        BA_PHUKHOA,
        BA_SANKHOA,
        BA_SOSINH,
        BA_NGOAIKHOA,
        BA_NGOAITRU,
         BA_NGOAIKHOA_BA,
        BA_NGOAITRU_BA,
        BA_NAMKHOA,
        BA_IVF_VO,
        BA_IVF_CHONG
    };
    }
}
