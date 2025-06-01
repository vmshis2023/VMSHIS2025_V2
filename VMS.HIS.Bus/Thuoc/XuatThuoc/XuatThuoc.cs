using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
using VNS.Properties;
using System.Data;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class XuatThuoc
    {
        private NLog.Logger log;
        public XuatThuoc()
        {
            log = NLog.LogManager.GetLogger("KCB_KEDONTHUOC");
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIECJ CHO PHÉP CẬP NHẬP ĐƠN THUỐC
        /// </summary>
        /// <returns></returns>
        public ActionResult LinhThuocBenhNhan(KcbDonthuoc objDonthuoc,KcbDonthuocChitiet []arrPresDetails, TPhieuXuatthuocBenhnhan objXuatBnhan)
        {
            try
            {
                HisDuocProperties hisDuocProperties=new HisDuocProperties();
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        hisDuocProperties = PropertyLib._HisDuocProperties;
                        objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                        objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                        objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                        objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                        objXuatBnhan.IsNew = true;
                        objXuatBnhan.Save();
                        Int32 PtramBHYT = 0;
                        SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                            KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                        KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                        if(objLuotkham!=null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        foreach (KcbDonthuocChitiet objDetail in arrPresDetails)
                        {
                            ActionResult actionResult = TruThuocTrongKho(objDonthuoc,objDetail, objXuatBnhan);
                            switch (actionResult)
                            {
                                case ActionResult.NotEnoughDrugInStock:
                                    return actionResult;
                                    break;
                            }
                            //REM lại để tránh trường hợp vi phạm phần nội trú. Đơn thuốc được cấp phát nhiều lần
                            //new Delete().From(TXuatthuocTheodon.Schema)
                            //    .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            TXuatthuocTheodon objThuocCt=new TXuatthuocTheodon();
                            objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                            objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                            objThuocCt.NgayTao = DateTime.Now;
                            objThuocCt.SoLuong = Utility.DecimaltoDbnull(objDetail.SoLuong);
                            objThuocCt.NguoiTao = globalVariables.UserName;
                            objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                            objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);
                          
                          //  objThuocCt.gi = Utility.DecimaltoDbnull(objDetail.DonGia);
                            objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                            objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                            objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                            objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                            objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                            objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                            objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                            objThuocCt.PtramBhyt = PtramBHYT;
                            objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                            objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                           // objThuocCt.LoaiDonThuoc = 0;
                            objThuocCt.IsNew = true;
                            objThuocCt.Save();
                        }
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery.GetRecordCount()<=0?1:0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult LinhThuocBenhNhanTaiQuay(long Pres_ID, KcbDonthuocChitietCollection lstDetail, Int16 id_kho, DateTime ngaythuchien)
        {
            try
            {
                string GUID = THU_VIEN_CHUNG.GetGUID();
                string ErrMsg = "";
                HisDuocProperties hisDuocProperties = new HisDuocProperties();
                using (TransactionScope scope = new TransactionScope())
                {

                    hisDuocProperties = PropertyLib._HisDuocProperties;
                    KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                    KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objDonthuoc.IdBenhnhan);
                    KcbLuotkham objLuotkham = null;//KH vãng lai ko có thông tin lượt khám
                    TPhieuXuatthuocBenhnhan objXuatBnhan = CreatePhieuXuatBenhNhan(objDonthuoc, objBenhnhan, objLuotkham);
                    objXuatBnhan.NgayXacnhan = ngaythuchien;
                    objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                    objXuatBnhan.IdKho = id_kho;
                    objXuatBnhan.IsNew = true;
                    objXuatBnhan.Save();
                    Int32 PtramBHYT = 0;
                    if (objLuotkham != null)
                    {
                        PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                    }
                    if (lstDetail == null || lstDetail.Count <= 0)
                        lstDetail = new Select().From(KcbDonthuocChitiet.Schema)
                           .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                           .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                           .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0)
                           .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    //Chỉ việc trừ theo chi tiết do ngay khi kê đơn đã tự động xác định các thuốc cần trừ trong kho theo id_thuockho
                    foreach (KcbDonthuocChitiet objDetail in lstDetail)
                    {
                        objDetail.IdPhieuTXuatthuocBenhnhan = objXuatBnhan.IdPhieu;
                        TThuockho objTThuockho = new Select().From(TThuockho.Schema)
                            .Where(TThuockho.IdThuockhoColumn).IsEqualTo(objDetail.IdThuockho)
                            .ExecuteSingle<TThuockho>();
                        //Kiểm tra xem thuốc còn đủ hay không?
                        if (objTThuockho.SoLuong < objDetail.SoLuong)
                        {
                            //Sau này có thể mở rộng thêm code tự động dò và xác định lại Id_thuockho cho các chi tiết đơn thuốc
                            return ActionResult.NotEnoughDrugInStock;
                        }
                        UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, objDetail.SoLuong, objXuatBnhan);
                        StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                            Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                            objTThuockho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                            Utility.DecimaltoDbnull(objTThuockho.Vat), objDetail.SoLuong, objTThuockho.IdThuockho,
                            objTThuockho.MaNhacungcap, objTThuockho.SoLo,
                            0, ErrMsg);

                        sp.Execute();

                        //new Update(KcbDonthuocChitiet.Schema)
                        //.Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        //.Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(ngaythuchien)
                        //.Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                        //new Update(TblKedonthuocTempt.Schema)
                        //     .Set(TblKedonthuocTempt.Columns.TrangThai).EqualTo(1)
                        //     .Where(TblKedonthuocTempt.Columns.IdChitietdonthuoc).IsEqualTo(
                        //         objDetail.IdChitietdonthuoc).Execute();
                        //REM lại để tránh trường hợp vi phạm phần nội trú. Đơn thuốc được cấp phát nhiều lần
                        TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                        objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                        objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                        objThuocCt.NgayTao = DateTime.Now;
                        objThuocCt.SoLuong = Utility.DecimaltoDbnull(objDetail.SoLuong);
                        objThuocCt.NguoiTao = globalVariables.UserName;
                        objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                        objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);

                        objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                        objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                        objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                        objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                        objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                        objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                        objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                        objThuocCt.PtramBhyt = PtramBHYT;
                        objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                        objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                        objThuocCt.IdThuockho = objDetail.IdThuockho.Value;
                        //objThuocCt.IsNew = true;
                        //objThuocCt.Save();
                        StoredProcedure spxuatthuoctheodon = SPs.ThuocXuatthuocTheodonInsert(objThuocCt.IdPhieuXuat,
                               objThuocCt.IdThuoc, objThuocCt.SoLuong, objThuocCt.DonGia, objThuocCt.PhuThu,
                               objThuocCt.BnhanChitra, objThuocCt.BhytChitra,
                               objThuocCt.PtramBhyt, objThuocCt.ChiDan, objThuocCt.CachDung, objThuocCt.ChidanThem,
                               objThuocCt.SolanDung, objThuocCt.SoluongDung, objThuocCt.NgayTao, objThuocCt.NguoiTao,
                               objThuocCt.IdChitietdonthuoc, objThuocCt.IdDonthuoc,objThuocCt.IdThuockho, ngaythuchien);
                        spxuatthuoctheodon.Execute();
                        //Cập nhật trạng thái cấp phát của đơn chi tiết và xóa trong bảng tạm kê để nhả tồn
                        SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objDetail.IdChitietdonthuoc, objDetail.SoLuong, 1,objDetail.IdPhieuTXuatthuocBenhnhan, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 0).Execute();
                    }
                    DataTable dttempt = new Select().From(KcbDonthuocChitiet.Schema)
                              .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).ExecuteDataSet().Tables[0];


                    int status = dttempt.AsEnumerable().Any(l => l.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 0) ? 0 : 1;
                    int da_capphat = dttempt.AsEnumerable().Any(l => l.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 1) ? 1 : 0;
                    int tthai_capphat = da_capphat == 1 && status == 1 ? 2 : da_capphat;
                    new Update(KcbDonthuoc.Schema)
                              .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                              .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                              .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                              .Set(KcbDonthuoc.Columns.TthaiCapphat).EqualTo(tthai_capphat)
                              .Set(KcbDonthuoc.Columns.NgayHuyxacnhan).EqualTo(null)
                              .Set(KcbDonthuoc.Columns.NguoiHuyxacnhan).EqualTo("")
                              .Set(KcbDonthuoc.Columns.LydoHuyxacnhan).EqualTo("")
                              .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult LinhThuocBenhNhan(long presId, Int16 idKho,  List<long> lstIdchitietcancaphat,DateTime ngaythuchien, ref string ErrMsg )
        {
            try
            {
               string  GUID = THU_VIEN_CHUNG.GetGUID();
               bool isOK = false;
               if (lstIdchitietcancaphat.Count <= 0) return ActionResult.NodataFound;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope dbScope = new SharedDbConnectionScope())
                    {
                    
                        KcbDanhsachBenhnhan objBenhnhan = new KcbDanhsachBenhnhan();
                         KcbLuotkham objLuotkham = new KcbLuotkham();
                        KcbDonthuoc objDonthuoc = new KcbDonthuoc();

                        if (presId > 0)
                        {
                            objDonthuoc = KcbDonthuoc.FetchByID(presId);
                        }
                        else
                        {
                           return ActionResult.Error;
                        }
                        if (objDonthuoc != null)
                        {
                             objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objDonthuoc.IdBenhnhan);
                             objLuotkham = new Select().From(KcbLuotkham.Schema)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
                        }
                        else
                        {
                            return ActionResult.Error;
                        }
                        TPhieuXuatthuocBenhnhan objXuatBnhan = CreatePhieuXuatBenhNhan(objDonthuoc, objBenhnhan, objLuotkham);
                        objXuatBnhan.NgayXacnhan = ngaythuchien;
                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.IdKho = idKho;
                        StoredProcedure spxbenhnhan = SPs.ThuocPhieuXuatthuocBenhnhanInsert(objXuatBnhan.IdPhieu,
                            objXuatBnhan.IdDonthuoc, objXuatBnhan.IdBenhnhan, objXuatBnhan.MaLuotkham,
                            objXuatBnhan.MaPhieu, objXuatBnhan.NgayXacnhan, objXuatBnhan.NgayChot, objXuatBnhan.IdChot,
                            objXuatBnhan.NgayHuychot, objXuatBnhan.NguoiHuychot, objXuatBnhan.LydoHuychot,
                            objXuatBnhan.NgayKedon, objXuatBnhan.TenBenhnhan, objXuatBnhan.TenKhongdau,
                            objXuatBnhan.GioiTinh, objXuatBnhan.ChanDoan, objXuatBnhan.MabenhChinh, objXuatBnhan.DiaChi,
                            objXuatBnhan.NamSinh, objXuatBnhan.MatheBhyt, objXuatBnhan.IdDoituongKcb,
                            objXuatBnhan.MaDoituongKcb, objXuatBnhan.IdCapphat, objXuatBnhan.IdNhanvien,
                            objXuatBnhan.IdKhoaChidinh, objXuatBnhan.IdPhongChidinh, objXuatBnhan.IdBacsiKedon,
                            objXuatBnhan.IdKho, objXuatBnhan.Noitru, objXuatBnhan.NguoiPhatthuoc, objXuatBnhan.NguoiTao,
                            objXuatBnhan.NgayTao, objXuatBnhan.LoaiPhieu, objXuatBnhan.TenLoaiphieu,
                            objXuatBnhan.QuayThuoc, objXuatBnhan.KieuThuocvattu);
                        spxbenhnhan.Execute();

                        objXuatBnhan.IdPhieu = Utility.Int64Dbnull(spxbenhnhan.OutputValues[0]);
                        log.Trace(string.Format("1.3 Phát thuốc thành công của bệnh nhân: {0}", objLuotkham.MaLuotkham));
                        Int32 PtramBHYT = 0;
                        if (objLuotkham != null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        KcbDonthuocChitietCollection lstDetail
                            = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(idKho)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0)//Chỉ cấp phát cho các chi tiết chưa cấp phát
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        //Chỉ việc trừ theo chi tiết do ngay khi kê đơn đã tự động xác định các thuốc cần trừ trong kho theo id_thuockho
                        bool capphattatca = lstIdchitietcancaphat == null ;
                        foreach (KcbDonthuocChitiet objDetail in lstDetail)
                        {
                            if (Utility.ByteDbnull(objDetail.TrangThai,0)==0)//Chưa cấp phát. Xử lý tình huống cấp phát nhiều lần. Sau khi chọn thì chi tiết đã bị người khác cấp phát thì bỏ qua
                            {
                                
                                decimal sluong_thuclinh = Utility.DecimaltoDbnull(objDetail.SluongSua, 0) > 0 ? Utility.DecimaltoDbnull(objDetail.SluongSua, 0) : objDetail.SoLuong;
                                objDetail.SoLuong = sluong_thuclinh;
                                objDetail.IdPhieuTXuatthuocBenhnhan = objXuatBnhan.IdPhieu;
                                if (lstIdchitietcancaphat != null && lstIdchitietcancaphat.Count > 0)
                                {
                                    if (!lstIdchitietcancaphat.Contains(objDetail.IdChitietdonthuoc))
                                        continue;
                                }
                                ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objDetail.IdThuoc, idKho, objDetail.SoLuong, 1m, objDetail.IdThuockho.Value,true, ref ErrMsg);
                                if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                                
                                TThuockho objTThuockho = new Select().From(TThuockho.Schema)
                                    .Where(TThuockho.IdThuockhoColumn).IsEqualTo(objDetail.IdThuockho)
                                    .ExecuteSingle<TThuockho>();
                                ////Đoạn cũ dưới đây không kiểm tra tồn ảo sẽ gây lỗi nếu các đoạn kê, tạo phiếu nhập xuất bắt tồn ảo kém
                                ////Kiểm tra xem thuốc còn đủ hay không?
                                //if (objTThuockho.SoLuong < objDetail.SoLuong)
                                //{
                                //    //Sau này có thể mở rộng thêm code tự động dò và xác định lại Id_thuockho cho các chi tiết đơn thuốc
                                //    return ActionResult.NotEnoughDrugInStock;
                                //}

                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, objDetail.SoLuong, objXuatBnhan);
                                log.Trace(string.Format("1.4 Phát thuốc thành công của bệnh nhân: {0}", objLuotkham.MaLuotkham));
                                //Bước 1: Trừ số lượng theo ID thuốc kho
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                    Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                    objTThuockho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                    Utility.DecimaltoDbnull(objTThuockho.Vat), objDetail.SoLuong, objTThuockho.IdThuockho,
                                    objTThuockho.MaNhacungcap, objTThuockho.SoLo,
                                    0, ErrMsg);

                                sp.Execute();
                                log.Trace(string.Format("1.5 Phát thuốc thành công của bệnh nhân: {0}", objLuotkham.MaLuotkham));
                                //new Update(KcbDonthuocChitiet.Schema)
                                //   .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                                //   .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(ngaythuchien)
                                //   .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                                //new Update(TblKedonthuocTempt.Schema)
                                //    .Set(TblKedonthuocTempt.Columns.TrangThai).EqualTo(1)
                                //    .Where(TblKedonthuocTempt.Columns.IdChitietdonthuoc).IsEqualTo(
                                //        objDetail.IdChitietdonthuoc).Execute();
                                //REM lại để tránh trường hợp vi phạm phần nội trú. Đơn thuốc được cấp phát nhiều lần
                                TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                                objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                                objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                                objThuocCt.NgayTao = DateTime.Now;
                                objThuocCt.SoLuong = Utility.DecimaltoDbnull(objDetail.SoLuong);
                                objThuocCt.NguoiTao = globalVariables.UserName;
                                objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                                objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);

                                objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                                objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                                objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                                objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                                objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                                objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                                objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                                objThuocCt.PtramBhyt = PtramBHYT;
                                objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                                objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                                objThuocCt.IdThuockho = objDetail.IdThuockho.Value;
                                StoredProcedure spxuatthuoctheodon = SPs.ThuocXuatthuocTheodonInsert(objThuocCt.IdPhieuXuat,
                                    objThuocCt.IdThuoc, objThuocCt.SoLuong, objThuocCt.DonGia, objThuocCt.PhuThu,
                                    objThuocCt.BnhanChitra, objThuocCt.BhytChitra,
                                    objThuocCt.PtramBhyt, objThuocCt.ChiDan, objThuocCt.CachDung, objThuocCt.ChidanThem,
                                    objThuocCt.SolanDung, objThuocCt.SoluongDung, objThuocCt.NgayTao, objThuocCt.NguoiTao,
                                    objThuocCt.IdChitietdonthuoc, objThuocCt.IdDonthuoc, objThuocCt.IdThuockho, ngaythuchien);
                                spxuatthuoctheodon.Execute();
                                //Cập nhật trạng thái cấp phát của đơn chi tiết và xóa trong bảng tạm kê để nhả tồn
                                SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objDetail.IdChitietdonthuoc, objDetail.SoLuong, 1, objDetail.IdPhieuTXuatthuocBenhnhan, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 0).Execute();
                                isOK = true;
                            }
                        }
                        if (!isOK) return ActionResult.Cancel;
                        log.Trace(string.Format("1.5 Phát thuốc thành công của bệnh nhân: {0}", objLuotkham.MaLuotkham));
                        DataTable dttempt = new Select().From(KcbDonthuocChitiet.Schema)
                             .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).ExecuteDataSet().Tables[0];


                        int status = dttempt.AsEnumerable().Any(l=>l.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai)==0) ? 0 : 1;
                        int da_capphat = dttempt.AsEnumerable().Any(l => l.Field<byte>(KcbDonthuocChitiet.Columns.TrangThai) == 1) ? 1 : 0;
                        int tthai_capphat = da_capphat == 1 && status == 1 ? 2 : da_capphat;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Set(KcbDonthuoc.Columns.TthaiCapphat).EqualTo(tthai_capphat)
                                  .Set(KcbDonthuoc.Columns.NgayHuyxacnhan).EqualTo(null)
                                  .Set(KcbDonthuoc.Columns.NguoiHuyxacnhan).EqualTo("")
                                  .Set(KcbDonthuoc.Columns.LydoHuyxacnhan).EqualTo("")
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                       
                        
                        log.Trace(string.Format("Phát thuốc thành công của bệnh nhân: {0}", objLuotkham.MaLuotkham));
                            
                    }
                    scope.Complete();
                    
                   
                }
                return ActionResult.Success;
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }


        public static bool InValiKiemTraDonThuoc(KcbDonthuocChitietCollection lstChitiet,byte noitru)
        {
            try
            {
                foreach (KcbDonthuocChitiet item in lstChitiet)
                {

                    decimal SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho((long)item.IdDonthuoc, Utility.Int32Dbnull(item.IdKho, -1), item.IdThuoc, Utility.Int64Dbnull(item.IdThuockho, -1),0, noitru);//Ko cần kiểm tra chờ xác nhận
                    if (SoLuongTon < item.SoLuong)
                    {
                        Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", item.IdThuoc));
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        
       
        public TPhieuXuatthuocBenhnhan CreatePhieuXuatBenhNhan(KcbDonthuoc objDonthuoc, KcbDanhsachBenhnhan objBenhnhan, KcbLuotkham objLuotkham)
        {
           

            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = new TPhieuXuatthuocBenhnhan();
            objPhieuXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
            objPhieuXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
            objPhieuXuatBnhan.NgayXacnhan = DateTime.Now;
            objPhieuXuatBnhan.IdPhongChidinh = Utility.Int16Dbnull(objDonthuoc.IdPhongkham);
            objPhieuXuatBnhan.IdKhoaChidinh = Utility.Int16Dbnull(objDonthuoc.IdKhoadieutri);
            objPhieuXuatBnhan.IdBacsiKedon = Utility.Int16Dbnull(objDonthuoc.IdBacsiChidinh);
            objPhieuXuatBnhan.IdDonthuoc = Utility.Int64Dbnull(objDonthuoc.IdDonthuoc);
            objPhieuXuatBnhan.IdNhanvien = globalVariables.gv_intIDNhanvien;
            //objPhieuXuatBnhan.HienThi = 1;
            if (objLuotkham != null)
            {
                objPhieuXuatBnhan.ChanDoan = Utility.sDbnull(objLuotkham.ChanDoan);
                objPhieuXuatBnhan.MabenhChinh = Utility.sDbnull(objLuotkham.MabenhChinh);
                objPhieuXuatBnhan.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
                objPhieuXuatBnhan.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                objPhieuXuatBnhan.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt);
            }
            else//Đơn thuốc tại quầy thì objLuotkham=null;
            {
                objPhieuXuatBnhan.ChanDoan = "";
                objPhieuXuatBnhan.MabenhChinh = "";
                objPhieuXuatBnhan.IdDoituongKcb = -1;
                objPhieuXuatBnhan.MaDoituongKcb = "DV";
                objPhieuXuatBnhan.MatheBhyt = "";
            }
            objPhieuXuatBnhan.GioiTinh = objBenhnhan.GioiTinh;
            objPhieuXuatBnhan.KieuThuocvattu = objDonthuoc.KieuThuocvattu;
            objPhieuXuatBnhan.TenBenhnhan = Utility.sDbnull(objBenhnhan.TenBenhnhan);
            objPhieuXuatBnhan.TenKhongdau = Utility.sDbnull(Utility.UnSignedCharacter(objBenhnhan.TenBenhnhan));
            objPhieuXuatBnhan.DiaChi = Utility.sDbnull(objBenhnhan.DiaChi);
            objPhieuXuatBnhan.NamSinh = Utility.Int32Dbnull(objBenhnhan.NamSinh);
            
            objPhieuXuatBnhan.NgayKedon = objDonthuoc.NgayKedon;
            objPhieuXuatBnhan.NgayTao = DateTime.Now;
            objPhieuXuatBnhan.NguoiTao = objDonthuoc.NguoiTao;//Dùng cho báo cáo kê đơn theo bác sĩ(trạng thái đã cấp phát để biết người tạo là Admin)
            objPhieuXuatBnhan.NguoiPhatthuoc = globalVariables.UserName;
            objPhieuXuatBnhan.QuayThuoc = (byte)(objDonthuoc.KieuDonthuoc == 2 ? 1 : 0);//0= Đơn thuốc thường;1= Đơn thuốc bổ sung;2=Đơn thuốc tại quầy;3=Đơn tiêm chủng
            objPhieuXuatBnhan.Noitru = objDonthuoc.Noitru;
            objPhieuXuatBnhan.LoaiPhieu = (byte?)LoaiPhieu.PhieuXuatKhoBenhNhan;
            
            return objPhieuXuatBnhan;
        }
        public ActionResult LinhThuocBenhNhan_Tutruc(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrPresDetails, TPhieuXuatthuocBenhnhan objXuatBnhan)
        {
            try
            {
                string GUID = THU_VIEN_CHUNG.GetGUID();
                HisDuocProperties hisDuocProperties = new HisDuocProperties();
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        hisDuocProperties = PropertyLib._HisDuocProperties;
                        objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                        objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;

                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                        objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                        objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                        objXuatBnhan.IsNew = true;
                        objXuatBnhan.Save();
                        Int32 PtramBHYT = 0;
                        SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                            KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                        KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                        if (objLuotkham != null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        foreach (KcbDonthuocChitiet objDetail in arrPresDetails)
                        {
                            objDetail.IdPhieuTXuatthuocBenhnhan = objXuatBnhan.IdPhieu;
                            ActionResult actionResult = TruThuocTrongTuTruc(objDonthuoc,objDetail, objXuatBnhan);
                            switch (actionResult)
                            {
                                case ActionResult.NotEnoughDrugInStock:
                                    return actionResult;
                                    break;
                            }
                            TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                            objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                            objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                            objThuocCt.NgayTao = DateTime.Now;
                            objThuocCt.SoLuong = Utility.DecimaltoDbnull(objDetail.SoLuong);
                            objThuocCt.NguoiTao = globalVariables.UserName;
                            objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                            objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);

                            //  objThuocCt.gi = Utility.DecimaltoDbnull(objDetail.DonGia);
                            objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                            objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                            objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                            objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                            objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                            objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                            objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                            objThuocCt.PtramBhyt = PtramBHYT;
                            objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                            objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                            // objThuocCt.LoaiDonThuoc = 0;
                            objThuocCt.IsNew = true;
                            objThuocCt.Save();
                            //Cập nhật trạng thái cấp phát của đơn chi tiết và xóa trong bảng tạm kê để nhả tồn
                            SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objDetail.IdChitietdonthuoc, objDetail.SoLuong, 1, objDetail.IdPhieuTXuatthuocBenhnhan, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 0).Execute();
                        }
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                        
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIECJ CHO PHÉP CẬP NHẬP ĐƠN THUỐC
        /// </summary>
        /// <returns></returns>
        public ActionResult Linhthuocnoitru(KcbDonthuoc objDonthuoc, TPhieuXuatthuocBenhnhan objXuatBnhan, int ID_KHO_XUAT, DateTime ngaythuchien)
        {
            try
            {
                string GUID = THU_VIEN_CHUNG.GetGUID();
                //using (var scope = new TransactionScope())
                //{
                //    using (var dbscop  = new SharedDbConnectionScope())
                //    {
                        string THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false);
                    HisDuocProperties hisDuocProperties = new HisDuocProperties();
                    objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                    objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
                    objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                    objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                    objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                    objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                    objXuatBnhan.IsNew = true;
                    objXuatBnhan.Save();
                    Int32 PtramBHYT = 0;
                    SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                        KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                    KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                    if (objLuotkham != null)
                    {
                        PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                    }
                    sqlQuery = new Select().From(TPhieuCapphatChitiet.Schema)
                        .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(objXuatBnhan.IdCapphat)
                        .And(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc);

                    TPhieuCapphatChitietCollection objDPhieuCapphatCtCollection =
                        sqlQuery.ExecuteAsCollection<TPhieuCapphatChitietCollection>();
                    foreach (TPhieuCapphatChitiet objCapphatDetail in objDPhieuCapphatCtCollection)
                    {
                        KcbDonthuocChitiet objDetail = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objCapphatDetail.IdChitietdonthuoc)
                            .ExecuteSingle<KcbDonthuocChitiet>();
                        if (objDetail == null) return ActionResult.Exceed;
                        objDetail.IdKho = ID_KHO_XUAT;
                        ActionResult actionResult = TruThuocTrongKho_Noitru(objDonthuoc, objCapphatDetail, objDetail, objXuatBnhan, ID_KHO_XUAT, ngaythuchien, GUID);
                        switch (actionResult)
                        {
                            case ActionResult.NotEnoughDrugInStock:
                                return actionResult;
                        }

                        TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                        objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                        objThuocCt.IdThuoc = Utility.Int32Dbnull(objCapphatDetail.IdThuoc);
                        objThuocCt.NgayTao = DateTime.Now;
                        objThuocCt.SoLuong = objCapphatDetail.SoLuong;
                        objThuocCt.NguoiTao = globalVariables.UserName;
                        objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                        objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);
                        objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                        objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                        objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                        objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                        objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                        objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                        objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                        objThuocCt.PtramBhyt = PtramBHYT;
                        objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objCapphatDetail.IdChitietdonthuoc);
                        objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objCapphatDetail.IdThuoc);
                        objThuocCt.IsNew = true;
                        objThuocCt.Save();
                        if (THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC == "1")
                        {
                            objCapphatDetail.ThucLinh = objCapphatDetail.SoLuong;
                            objCapphatDetail.IsNew = false;
                            objCapphatDetail.MarkOld();
                            objCapphatDetail.Save();
                        }

                    }
                    byte DA_LINH = (byte)(THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC == "1" ? 1 : 0);

                    new Update(TPhieuCapphatChitiet.Schema)
                        .Set(TPhieuCapphatChitiet.Columns.DaLinh).EqualTo(DA_LINH)
                        .Set(TPhieuCapphatChitiet.Columns.IdPhieuxuatthuocBenhnhan).EqualTo(objXuatBnhan.IdPhieu)
                        .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(objXuatBnhan.IdCapphat).Execute();
                    sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                                .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery.GetRecordCount() <= 0 ? 1 : 0;
                    new Update(KcbDonthuoc.Schema)
                              .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                              .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                              .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                              .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                //    dbscop.Dispose();
                //    }
                //     scope.Complete();
                //}
               
                return ActionResult.Success;
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;

            }
        }

        private ActionResult TruThuocTrongKho_Noitru(KcbDonthuoc objDonthuoc, TPhieuCapphatChitiet objCapphatDetail, KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan, int ID_KHO_XUAT, DateTime ngaythuchien, string GUID)
        {
            string errorMessage = "";
            try
            {
                //using (var scope = new TransactionScope())
                //{
                //    using (var dbscope = new SharedDbConnectionScope())
                //    {
                       

                        TThuockho objTThuockho = new Select().From(TThuockho.Schema)
                                  .Where(TThuockho.IdThuockhoColumn).IsEqualTo(objDetail.IdThuockho)
                                  .ExecuteSingle<TThuockho>();
                        ////Đoạn cũ dưới đây không kiểm tra tồn ảo sẽ gây lỗi nếu các đoạn kê, tạo phiếu nhập xuất bắt tồn ảo kém
                        //Kiểm tra xem thuốc còn đủ hay không?
                        //if (objTThuockho.SoLuong < objCapphatDetail.SoLuong)
                        //{
                        //    //Sau này có thể mở rộng thêm code tự động dò và xác định lại Id_thuockho cho các chi tiết đơn thuốc
                        //    return ActionResult.NotEnoughDrugInStock;
                        //}
                        
                        decimal TONGSOLUONG_LINH = 0;
                        decimal SOLUONG_LINH = objCapphatDetail.SoLuong;
                        TONGSOLUONG_LINH = objDetail.SluongLinh == null ? 0 : objDetail.SluongLinh.Value;
                        //Tạm REM lại
                        //if (objDetail.SluongLinh.Value <= 0)//Cấp phát lần đầu
                        //    SOLUONG_LINH = objDetail.Quantity;
                        //else//Cấp phát lần n...
                        //{
                        //    if (objDetail.SluongSua.Value > objDetail.SluongLinh.Value)
                        //        SOLUONG_LINH = objDetail.SluongSua.Value - objDetail.SluongLinh.Value;
                        //}

                        TONGSOLUONG_LINH += SOLUONG_LINH;
                        ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objDetail.IdThuoc,(Int16) ID_KHO_XUAT, TONGSOLUONG_LINH, 1m, objDetail.IdThuockho.Value, true, ref errorMessage);
                        if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                        //Đã xác định xong số thuốc cần lĩnh đợt này-->Kiểm tra xem còn đủ hay không
                        List<TThuockho> objThuocKhoCollection = new List<TThuockho>();//Tạm rem lại 20150127 GetObjThuocKhoCollection_Noitru(objCapphatDetail, ID_KHO_XUAT);
                        objThuocKhoCollection.Add(objTThuockho);
                        decimal iSoLuongConLai = 0;
                        decimal iSoLuongDonThuoc = 0;
                        decimal iSoLuongTru = 0;
                        iSoLuongDonThuoc = SOLUONG_LINH;
                        if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                        foreach (TThuockho objDThuocKho in objThuocKhoCollection)//VÒNG LẶP NÀY CHỈ CHẠY 1 LẦN DUY NHẤT VÌ CHỈ CÓ 1 BẢN TIN ỨNG VỚI ID_THUOCKHO DUY NHẤT. DO VẬY TONGSOLUONG_LINH=SO_LUONG ĐÃ KÊ
                        {
                            string ErrMsg = "";
                            iSoLuongConLai = Utility.DecimaltoDbnull(objDThuocKho.SoLuong);
                            ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                            if (iSoLuongConLai >= iSoLuongDonThuoc)
                            {
                                iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objDThuocKho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objDThuocKho.IdKho),
                                                                              Utility.Int32Dbnull(objDThuocKho.IdThuoc, -1),
                                                                              objDThuocKho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, ErrMsg);

                                sp.Execute();
                                break;
                            }
                            else
                            {
                                iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                                iSoLuongDonThuoc = iSoLuongTru;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objDThuocKho, iSoLuongConLai, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objDThuocKho.IdKho),
                                                                              Utility.Int32Dbnull(objDThuocKho.IdThuoc, -1),
                                                                              objDThuocKho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, ErrMsg);
                                sp.Execute();
                            }
                        }
                        //Cập nhật trạng thái cấp phát của đơn chi tiết và xóa trong bảng tạm kê để nhả tồn
                        SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objDetail.IdChitietdonthuoc, objDetail.SoLuong, 1, objDetail.IdPhieuTXuatthuocBenhnhan, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 0).Execute();

                        //new Update(KcbDonthuocChitiet.Schema)
                        //    .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        //    .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(objTThuockho.IdThuockho)
                        //    .Set(KcbDonthuocChitiet.Columns.IdKho).EqualTo(ID_KHO_XUAT)
                        //     .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(ngaythuchien)
                        //    .Set(KcbDonthuocChitiet.Columns.SluongLinh).EqualTo(TONGSOLUONG_LINH)
                        //    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                        
                //    }
                   
                //    scope.Complete();
                //}
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }
            return ActionResult.Success;
        }

       
        private ActionResult TruThuocTrongKho(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbcope = new SharedDbConnectionScope())
                    {
                        TThuockhoCollection objThuocKhoCollection = GetObjThuocKhoCollection(objDetail);
                        decimal iSoLuongConLai = 0;
                        decimal iSoLuongDonThuoc = 0;
                        decimal iSoLuongTru = 0;
                        iSoLuongDonThuoc = Utility.DecimaltoDbnull(objDetail.SoLuong);
                        if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                        foreach (TThuockho objTThuockho in objThuocKhoCollection)
                        {
                            iSoLuongConLai = Utility.DecimaltoDbnull(objTThuockho.SoLuong);
                            ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                            if (iSoLuongConLai >= iSoLuongDonThuoc)
                            {

                                iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                              Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                              objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, errorMessage);

                                sp.Execute();
                                break;


                            }
                            else
                            {
                                iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                                iSoLuongDonThuoc = iSoLuongTru;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, iSoLuongConLai, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                              Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                              objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, errorMessage);

                                sp.Execute();
                            }



                        }
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                        dbcope.Dispose();
                        
                    }

                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }

            return ActionResult.Success;
        }
        private ActionResult TruThuocTrongTuTruc(KcbDonthuoc objDonthuoc ,KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbcope = new SharedDbConnectionScope())
                    {
                        decimal TONGSOLUONG_LINH = 0;
                        decimal SOLUONG_LINH = 0;
                        TONGSOLUONG_LINH = objDetail.SluongLinh == null ? 0 : objDetail.SluongLinh.Value;
                        if (objDetail.SluongLinh.Value <= 0)//Cấp phát lần đầu
                            SOLUONG_LINH = objDetail.SoLuong;
                        else//Cấp phát lần n...
                        {
                            if (objDetail.SluongSua.Value > objDetail.SluongLinh.Value)
                                SOLUONG_LINH = objDetail.SluongSua.Value - objDetail.SluongLinh.Value;
                        }

                        TONGSOLUONG_LINH += SOLUONG_LINH;
                        //Đã xác định xong số thuốc cần lĩnh đợt này-->Kiểm tra xem còn đủ hay không
                        List<TThuockho> objThuocKhoCollection = GetObjThuocKhoCollection_Tutruc(objDetail, objPhieuXuatBnhan.IdKho.Value);
                        decimal iSoLuongConLai = 0;
                        decimal iSoLuongDonThuoc = 0;
                        decimal iSoLuongTru = 0;
                        iSoLuongDonThuoc = SOLUONG_LINH;
                        if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                        foreach (TThuockho objTThuockho in objThuocKhoCollection)
                        {
                            iSoLuongConLai = Utility.DecimaltoDbnull(objTThuockho.SoLuong);
                            ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                            if (iSoLuongConLai >= iSoLuongDonThuoc)
                            {
                                iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                              Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                              objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, errorMessage);

                                sp.Execute();
                                break;


                            }
                            else
                            {
                                iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                                iSoLuongDonThuoc = iSoLuongTru;
                                UpdateXuatChiTietBN(objDonthuoc, objDetail, objTThuockho, iSoLuongConLai, objPhieuXuatBnhan);
                                StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                              Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                              objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                              Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, 0, errorMessage);

                                sp.Execute();
                            }



                        }
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                            .Set(KcbDonthuocChitiet.Columns.SluongLinh).EqualTo(TONGSOLUONG_LINH)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                        dbcope.Dispose();
                    }
                    
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }
            return ActionResult.Success;
        }
      
        private TThuockhoCollection GetObjThuocKhoCollection(KcbDonthuocChitiet objDetail)
        {
            SqlQuery sqlQuery;
            sqlQuery = new Select().From(TThuockho.Schema)
                .Where(TThuockho.Columns.IdKho).IsEqualTo(objDetail.IdKho)
                .And(TThuockho.Columns.NgayHethan).IsGreaterThanOrEqualTo( DateTime.Now.Date)
                .And(TThuockho.Columns.IdThuoc).IsEqualTo(objDetail.IdThuoc)
                .And(TThuockho.Columns.SoLuong).IsGreaterThan(0)
                .OrderAsc(TThuockho.Columns.NgayHethan)
                .OrderAsc(TThuockho.Columns.GiaNhap);
            return sqlQuery.ExecuteAsCollection<TThuockhoCollection>();
        }
        public DataTable GetObjThuocKhoCollection(int id_kho, int id_thuoc, long id_thuockho, decimal so_luong, byte id_loaidoituong_kcb, byte Dungtuyen, byte Noitru)
        {
            //Lấy thuốc trong kho

            log.Trace("2-->Bat dau lay thong tin thuoc ke don");
            DataTable dtData = SPs.ThuocLaythuocTrongkhoKedon(id_kho, id_thuoc, id_thuockho, id_loaidoituong_kcb, Dungtuyen, Noitru).GetDataSet().Tables[0];
            log.Trace("2.1. Da lay xong thong tin thuoc trong kho ke don");
            DataTable dtReturnData = dtData.Clone();
            //Lấy số lượng ảo của các đơn thuốc có thuốc chưa được xác nhận
            DataTable dtPresDetail = SPs.ThuocLaythuocKedontrongngayChuaxacnhan(id_kho, id_thuoc, so_luong).GetDataSet().Tables[0];
            log.Trace("2.2. Da lay thong tin thuoc trong ngay chua xac nhan");
            List<TThuockho> lstItems = new List<TThuockho>();
            decimal iSoLuongConLai = 0;
            decimal iSoLuongDonThuoc = so_luong;
            decimal iSoLuongTru = 0;
            decimal soluongAo = 0;
            foreach (DataRow item in dtData.Rows)
            {
                DataRow[] arrDR = dtPresDetail.Select(TThuockho.Columns.IdThuockho + "=" + Utility.sDbnull(item[TThuockho.Columns.IdThuockho]));
                if (arrDR.Length > 0)
                    soluongAo = Utility.DecimaltoDbnull(arrDR.CopyToDataTable().Compute("SUM(" + KcbDonthuocChitiet.Columns.SoLuong + ")", "1=1"), 0);
                else
                    soluongAo = 0;
                //Tính số lượng kho còn lại
                iSoLuongConLai = Utility.DecimaltoDbnull(item[TThuockho.Columns.SoLuong], 0) - soluongAo;
                if (iSoLuongConLai > 0)
                {
                    if (iSoLuongConLai >= iSoLuongDonThuoc)
                    {
                        iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                        item[TThuockho.Columns.SoLuong] = iSoLuongDonThuoc;
                        if (Utility.DecimaltoDbnull(item[TThuockho.Columns.SoLuong], 0) > 0)
                            dtReturnData.ImportRow(item);
                        break;
                    }
                    else//Số lượng kho < số lượng thuốc
                    {
                        //Lượng nhỏ hơn
                        iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                        iSoLuongDonThuoc = iSoLuongTru;
                        item[TThuockho.Columns.SoLuong] = iSoLuongConLai;
                        if (Utility.DecimaltoDbnull(item[TThuockho.Columns.SoLuong], 0) > 0)
                            dtReturnData.ImportRow(item);
                    }
                }
            }
            log.Trace("2-->Da lay xong thong tin thuoc dung de ke don");
            return dtReturnData;
           
        }
       
       
        private List<TThuockho> GetObjThuocKhoCollection_Tutruc(KcbDonthuocChitiet objDetail, int KHO)
        {
            return new TThuockhoController().FetchByQuery(TThuockho.CreateQuery()
                .AddWhere(TThuockho.Columns.IdKho, Comparison.Equals, KHO)
                .AND(TThuockho.Columns.NgayHethan, Comparison.GreaterOrEquals,  DateTime.Now.Date)
                .AND(TThuockho.Columns.IdThuoc, Comparison.Equals, objDetail.IdThuoc)
                .AND(TThuockho.Columns.SoLuong, Comparison.GreaterThan, 0)
                .ORDER_BY(TThuockho.NgayHethanColumn, "ASC")).ToList<TThuockho>();
        }
        /// <summary>
        /// hàm thực hiện việc xuất thôn gtin bảng chi tiết của bệnh nhân
        /// </summary>
        /// <param name="objDetail"></param>
        /// <param name="objTThuockho"></param>
        /// <param name="iSoLuongDonThuoc"></param>
        /// <param name="objPhieuXuatBnhan"></param>
        private void UpdateXuatChiTietBN(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDetail, TThuockho objTThuockho, decimal iSoLuonTru, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            //using (var scope = new TransactionScope())
            //{
            //    using (var dbsope = new SharedDbConnectionScope())
            //    {
                    TPhieuXuatthuocBenhnhanChitiet objXuatBnhanCt = new TPhieuXuatthuocBenhnhanChitiet();
                    objXuatBnhanCt.IdPhieu = Utility.Int64Dbnull(objPhieuXuatBnhan.IdPhieu);
                    objXuatBnhanCt.SoLuong = iSoLuonTru;
                    objXuatBnhanCt.SoDky = objDetail.SoDky;
                    objXuatBnhanCt.SoQdinhthau = objDetail.SoQdinhthau;
                    objXuatBnhanCt.ChiDan = objDetail.MotaThem;
                    objXuatBnhanCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                    objXuatBnhanCt.NgayHethan = objDetail.NgayHethan;// objTThuockho.NgayHethan.Date;
                    objXuatBnhanCt.IdThuockho = objDetail.IdThuockho;
                    objXuatBnhanCt.SoLo = objDetail.SoLo;
                    objXuatBnhanCt.MaNhacungcap = objDetail.MaNhacungcap;
                    objXuatBnhanCt.Vat = (int)objDetail.Vat;
                    objXuatBnhanCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);//đơn giá cho bệnh nhân
                    objXuatBnhanCt.Vat = Utility.Int32Dbnull(objDetail.Vat);
                    objXuatBnhanCt.GiaBan = Utility.DecimaltoDbnull(objDetail.GiaBan);//giá bán
                    objXuatBnhanCt.GiaNhap = Utility.DecimaltoDbnull(objDetail.GiaNhap);//giá nhập
                    objXuatBnhanCt.GiaBhyt = Utility.DecimaltoDbnull(objDetail.GiaBhyt);//giá BHYT
                    objXuatBnhanCt.IdDonthuoc = objDetail.IdDonthuoc;
                    objXuatBnhanCt.PhuthuTraituyen = objDetail.PhuthuTraituyen;
                    objXuatBnhanCt.PhuthuDungtuyen = objDetail.PhuthuDungtuyen;

                    objXuatBnhanCt.IdKho = Utility.Int16Dbnull(objDetail.IdKho);
                    objXuatBnhanCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);

                    objXuatBnhanCt.NgayNhap = objTThuockho.NgayNhap;
                    StoredProcedure spxtchitiet = SPs.ThuocPhieuXuatthuocBenhnhanChitietInsert(objXuatBnhanCt.IdPhieuChitiet,
                        objXuatBnhanCt.IdPhieu, objXuatBnhanCt.IdKho, objXuatBnhanCt.IdThuoc, objXuatBnhanCt.SoLuong,
                        objXuatBnhanCt.DonGia, objXuatBnhanCt.GiaNhap, objXuatBnhanCt.GiaBan, objXuatBnhanCt.GiaBhyt,
                        objXuatBnhanCt.PhuthuDungtuyen, objXuatBnhanCt.PhuthuTraituyen
                        , objXuatBnhanCt.SoLo, objXuatBnhanCt.Vat, objXuatBnhanCt.NgayHethan,
                        objXuatBnhanCt.IdChitietdonthuoc, objXuatBnhanCt.IdDonthuoc, objXuatBnhanCt.ChiDan,
                        objXuatBnhanCt.IdThuockho, objXuatBnhanCt.MaNhacungcap, objXuatBnhanCt.NgayNhap,
                        objXuatBnhanCt.SoDky, objXuatBnhanCt.SoQdinhthau);
                    spxtchitiet.Execute();
                    objXuatBnhanCt.IdPhieuChitiet = Utility.Int64Dbnull(spxtchitiet.OutputValues[0]);

                    TBiendongThuoc objNhapXuat = new TBiendongThuoc();
                    objNhapXuat.NgayHethan = objDetail.NgayHethan;// objTThuockho.NgayHethan.Date;
                    objNhapXuat.IdThuockho = objDetail.IdThuockho;
                    objNhapXuat.SoDky = objDetail.SoDky;
                    objNhapXuat.SoQdinhthau = objDetail.SoQdinhthau;
                    objNhapXuat.SoLo = objDetail.SoLo;
                    objNhapXuat.MaNhacungcap = objDetail.MaNhacungcap;
                    objNhapXuat.IdQdinh = objTThuockho.IdQdinh;
                    objNhapXuat.QuayThuoc = objPhieuXuatBnhan.QuayThuoc;
                    objNhapXuat.MaPhieu = Utility.sDbnull(objPhieuXuatBnhan.MaPhieu);
                    objNhapXuat.Noitru = objPhieuXuatBnhan.Noitru;
                    objNhapXuat.NgayHoadon = objDonthuoc.NgayKedon;
                    objNhapXuat.NgayBiendong = objPhieuXuatBnhan.NgayXacnhan;
                    objNhapXuat.NgayTao = DateTime.Now;
                    objNhapXuat.NguoiTao = globalVariables.UserName;
                    objNhapXuat.SoLuong = Utility.DecimaltoDbnull(objXuatBnhanCt.SoLuong);
                    objNhapXuat.Vat = Utility.Int32Dbnull(objXuatBnhanCt.Vat);
                    objNhapXuat.DonGia = Utility.DecimaltoDbnull(objXuatBnhanCt.DonGia);
                    objNhapXuat.GiaBan = Utility.DecimaltoDbnull(objXuatBnhanCt.GiaBan);
                    objNhapXuat.GiaNhap = Utility.DecimaltoDbnull(objXuatBnhanCt.GiaNhap);
                    objNhapXuat.GiaBhyt = Utility.DecimaltoDbnull(objXuatBnhanCt.GiaBhyt);//giá BHYT
                    objNhapXuat.PhuThu = objDetail.PhuThu;
                    objNhapXuat.SoHoadon = "-1";
                    objNhapXuat.IdThuoc = Utility.Int32Dbnull(objXuatBnhanCt.IdThuoc);
                    objNhapXuat.IdPhieu = Utility.Int32Dbnull(objPhieuXuatBnhan.IdPhieu);
                    objNhapXuat.IdPhieuChitiet = Utility.Int32Dbnull(objXuatBnhanCt.IdPhieuChitiet);
                    objNhapXuat.IdNhanvien = globalVariables.gv_intIDNhanvien;
                    objNhapXuat.NgayNhap = objTThuockho.NgayNhap;
                    objNhapXuat.KieuThuocvattu = objPhieuXuatBnhan.KieuThuocvattu;
                    objNhapXuat.IdBenhnhan = objDetail.IdBenhnhan;
                    objNhapXuat.MaLuotkham = objDetail.MaLuotkham;
                    objNhapXuat.IdDoituongKcb = objPhieuXuatBnhan.IdDoituongKcb;

                    objNhapXuat.GiaPhuthuTraituyen = objDetail.PhuthuTraituyen;
                    objNhapXuat.GiaPhuthuDungtuyen = objDetail.PhuthuDungtuyen;

                    objNhapXuat.MaNhacungcap = objXuatBnhanCt.MaNhacungcap;
                    objNhapXuat.IdKho = Utility.Int16Dbnull(objPhieuXuatBnhan.IdKho);
                    objNhapXuat.MaPhieu = Utility.sDbnull(objPhieuXuatBnhan.MaPhieu);
                    objNhapXuat.MaLoaiphieu = Utility.ByteDbnull(objPhieuXuatBnhan.LoaiPhieu);
                    objNhapXuat.TenLoaiphieu = Utility.TenLoaiPhieu((LoaiPhieu)objPhieuXuatBnhan.LoaiPhieu);
                    objNhapXuat.IdKhoaLinh = objPhieuXuatBnhan.IdKhoaChidinh;
                    objNhapXuat.KieuThuocvattu = objDonthuoc.KieuThuocvattu;
                    objNhapXuat.ThanhTien = Utility.DecimaltoDbnull(objXuatBnhanCt.DonGia) *
                                            Utility.Int32Dbnull(objXuatBnhanCt.SoLuong);
                    objNhapXuat.MotaThem = "Xuất thuốc bệnh nhân";
                    StoredProcedure spbiendong = SPs.ThuocBiendongThuocInsert(objNhapXuat.IdBiendong, objNhapXuat.MaPhieu,
                        objNhapXuat.IdKho, objNhapXuat.IdThuoc, objNhapXuat.NgayHethan, objNhapXuat.SoLuong,
                        objNhapXuat.SluongChia, objNhapXuat.DonGia, objNhapXuat.ThanhTien, objNhapXuat.PhuThu,
                        objNhapXuat.IdPhieuChitiet, objNhapXuat.IdPhieu, objNhapXuat.Vat,
                        objNhapXuat.SoHoadon, objNhapXuat.MaNhacungcap, objNhapXuat.MaLoaiphieu, objNhapXuat.TenLoaiphieu,
                        objNhapXuat.NgayHoadon, objNhapXuat.NgayBiendong, objNhapXuat.IdNhanvien, objNhapXuat.NguoiTao,
                        objNhapXuat.NgayTao, objNhapXuat.IdKhoaLinh, objNhapXuat.GiaBan, objNhapXuat.GiaNhap,
                        objNhapXuat.SoLo, objNhapXuat.IdThuockho, objNhapXuat.IdChuyen,
                        objNhapXuat.KieuThuocvattu, objNhapXuat.MotaThem, objNhapXuat.SoChungtuKemtheo, objNhapXuat.Noitru,
                        objNhapXuat.QuayThuoc, objNhapXuat.GiaBhyt, objNhapXuat.GiaBhytCu, objNhapXuat.GiaPhuthuDungtuyen,
                        objNhapXuat.GiaPhuthuTraituyen, objNhapXuat.DuTru, objNhapXuat.NgayNhap, objNhapXuat.KieuBiendong,
                        objNhapXuat.ThuocVay, objNhapXuat.IdBenhnhan, objNhapXuat.IdDoituongKcb,
                        objNhapXuat.MaLuotkham, objNhapXuat.SoDky, objNhapXuat.SoQdinhthau);
                    spbiendong.Execute();
                    objNhapXuat.IdBiendong = Utility.Int64Dbnull(spbiendong.OutputValues[0]);
                //    dbsope.Dispose();
                //}
               
                //scope.Complete();
            //}
        }

        /// <summary>
        /// hàm thực hiện việc cập nhập trạng thái của đơn thuốc
        /// </summary>
        /// <param name="objDonthuoc"></param>
        private void UpdateTrangThaiDonThuoc(KcbDonthuoc objDonthuoc)
        {
            using (var scope = new TransactionScope())
            {
                using (var dbcope = new SharedDbConnectionScope())
                {
                    new Update(KcbDonthuoc.Schema)
                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                    .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    dbcope.Dispose();
                }
                
                scope.Complete();
            }
        }

        public ActionResult XacNhanPhieuHuy_thanhly_thuoc(TPhieuNhapxuatthuoc objPhieuNhap, DateTime ngayxacnhan, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        var objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        TDmucKho objkhonhap = TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat = TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa = DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region//Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho
                            //ActionResult kiemtrathuocxacnhan = KiemTra.Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, objPhieuNhapCt.IdChuyen.Value, ref errMsg);
                            //if (kiemtrathuocxacnhan != ActionResult.Success) return kiemtrathuocxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc trong kho thanh lý còn đủ để trừ hay không(khả dụng có tính cả số lượng này)
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdChuyen.Value, true, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                          0, errorMessage);

                            sp.Execute();
                            //Insert dòng kho xuất
                            TBiendongThuoc  objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);

                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = objPhieuNhapCt.GiaBhytCu;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.IdChuyen = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.MaLoaiphieu = objPhieuNhap.LoaiPhieu;
                            objXuatNhap.TenLoaiphieu = objPhieuNhap.TenLoaiphieu;
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();

                        }
                       int _numAffected= new Update(TPhieuNhapxuatthuoc.Schema)
                             .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();

                        if (_numAffected > 0)
                        {
                            objPhieuNhap.TrangThai = 1;
                            new Delete().From(TTamke.Schema).Where(TTamke.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).And(TTamke.Columns.Loai).IsEqualTo(Utility.ByteDbnull(objPhieuNhap.LoaiPhieu)).Execute();
                        }
                        else
                            return ActionResult.UNKNOW;
                        
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xác nhận phiếu ",ex);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacNhanPhieuHuy_thanhly_Thuoc(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            string GUID = THU_VIEN_CHUNG.GetGUID();
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            long idthuockho = -1;
                            //Nhập lại kho thanh lý
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                                      objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, idthuockho, idthuockho, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.NgayNhap, 
                                                                      objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            if (idthuockho != objPhieuNhapCt.IdThuockho)//Nếu ai đó xóa bằng tay trong bảng thuốc kho thì cần update lại
                                new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                                    .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            #region tách id phiếu
                            //int numofRec = -1;
                            //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(objPhieuNhapCt.SoLuong, objPhieuNhapCt.IdChuyen, numofRec);
                            //sp.Execute();
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //if (numofRec <= 0)
                            //{
                            //    errMsg = Utility.sDbnull(objPhieuNhapCt.IdChuyen, "0");
                            //    return ActionResult.notExists;
                            //}
                            #endregion
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapCt.IdThuockho), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), Utility.ByteDbnull(objPhieuNhap.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                 "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhap.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh) : (objPhieuNhap.LoaiPhieu == 6 ? string.Format("Xuất thuốc từ kho nội trú sang tủ trực khoa nội trú. Id kho chuyển: {0} sang  id kho nhận: {1} thuộc khoa {2} sang", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhonhap, objPhieuNhap.IdKhoalinh) : (objPhieuNhap.LoaiPhieu == 8 ? string.Format("Trả thuốc từ kho {0} về nhà cung cấp {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.MaNhacungcap) : string.Format("Trả thuốc từ tủ trực nội trú về kho nội trú. Id kho chuyển: {0} thuộc khoa {1} sang id kho nhận: {2}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh, objPhieuNhap.IdKhonhap))));

                        }
                        //Xóa toàn bộ chi tiết trong TBiendongThuoc
                        new Delete().From(TBiendongThuoc.Schema)
                            .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu ",ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, DateTime ngayxacnhan, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        TDmucKho objkhonhap = TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat = TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa = DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho
                            //ActionResult _Kiemtrathuocxacnhan = KiemTra.Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, objPhieuNhapCt.IdChuyen.Value, ref errMsg);
                            //if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc trong kho xuất còn đủ để trừ hay không(khả dụng có tính cả số lượng này)
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.SoLuong,1, objPhieuNhapCt.IdChuyen.Value,true, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                      objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                                      objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, idthuockho, objPhieuNhapCt.IdChuyen, ngayxacnhan, objPhieuNhapCt.GiaBhyt, 
                                                                      objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                          0, errorMessage);

                            sp.Execute();
                            errorMessage = Utility.sDbnull(sp.OutputValues[0], "");
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                               .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            //Insert dòng kho nhập
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);
                            objXuatNhap.GiaBhytCu = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhytCu);
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.ThuocVay = 0;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.IdThuockho = Utility.Int64Dbnull(objPhieuNhapCt.IdThuockho);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;

                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapKho);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            //Insert dòng của kho xuất
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBhyt = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt);
                            objXuatNhap.GiaBhytCu = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhytCu);
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.ThuocVay = 0;
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.IdChuyen = -1;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuXuatKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();

                        }
                        string LACTN = string.Format("Xác nhận(duyệt phiếu) bởi {0} vào lúc {1} tại địa chỉ {2}", globalVariables.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), globalVariables.gv_strIPAddress);
                        int _numAffected = new Update(TPhieuNhapxuatthuoc.Schema)
                              .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                              .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                              .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
                              .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                              .Set(TPhieuNhapxuatthuoc.Columns.LastActionName).EqualTo(LACTN)
                              .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                              .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                              .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        if (_numAffected > 0)
                        {
                            objPhieuNhap.TrangThai = 1;
                            new Delete().From(TTamke.Schema).Where(TTamke.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).And(TTamke.Columns.Loai).IsEqualTo(Utility.ByteDbnull(objPhieuNhap.LoaiPhieu)).Execute();
                        }
                        else
                            return ActionResult.UNKNOW;
                        Utility.Log("frm_PhieuChuyenKho", globalVariables.UserName, string.Format("Xác nhận phiếu xuất kho ID={0} thành công", objPhieuNhap.IdPhieu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.CatchException("Lỗi khi xác nhận phiếu xuất kho", ex);
                return ActionResult.Error;
            }
        }

       
        /// <summary>
        /// Kiểm tra xem thuốc trong kho xuất đã được sử dụng hay chưa?
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt,ref string errMsg)
        {
            
            KcbDonthuocChitietCollection _dtct = new Select().From(KcbDonthuocChitiet.Schema)
                .Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(objPhieuNhapCt.IdThuockho)
                .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(objPhieuNhap.IdKhonhap)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (_dtct.Count>0) return ActionResult.DataUsed;
            //TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
            //  TThuockho.CreateQuery()
            //  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
            //  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
            //  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
            //  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
            //  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
            //  .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
            //  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
            //  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
            //  );
            TThuockhoCollection vCollection = new Select().From(TThuockho.Schema)
                .Where(TThuockho.Columns.IdThuockho).IsEqualTo(objPhieuNhapCt.IdThuockho)
                .And(TThuockho.Columns.IdKho).IsEqualTo(objPhieuNhap.IdKhonhap)
                .ExecuteAsCollection<TThuockhoCollection>();
            if (vCollection.Count <= 0)
            {
                errMsg = string.Format("ID thuốc kho {0} không tồn tại. Đề nghị kiểm tra lại", objPhieuNhapCt.IdThuockho.ToString());
                return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            }

            decimal SoLuong = vCollection[0].SoLuong;//Số lượng có trong kho
            decimal soluong_tamke = 0;
            //Lấy số lượng bị giữ ở tạm kê theo id_thuockho + id_kho + loại phiếu
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = string.Format("select sum(so_luong) as so_luong from t_tamke where id_kho={0} and id_thuockho={1} and loai={2}", objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuockho, objPhieuNhap.LoaiPhieu); ;
            DataTable dtData = DataService.GetDataSet(cmd).Tables[0];
            if (dtData != null && dtData.Rows.Count > 0)
                soluong_tamke = Utility.DecimaltoDbnull(dtData.Rows[0]["so_luong"]);
           // DataTable dtData=new Select(
            SoLuong = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0) - soluong_tamke;
            if (SoLuong < 0)
            {
                errMsg = string.Format("ID thuốc={0}, Số lượng còn trong kho {1}. Số lượng bị trừ {2}. Phiếu xuất kho này tạo từ {3}. Có thể do để lâu đã bị chiếm thuốc và không còn đủ để xuất kho. Vui lòng kiểm tra lại", objPhieuNhapCt.IdThuoc.ToString(), vCollection[0].SoLuong.ToString(), objPhieuNhapCt.SoLuong.ToString(), objPhieuNhap.NgayTao.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            }
            return ActionResult.Success;
        }
     
        public ActionResult Kiemtradieukienhuyphieunhapkho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt, ref string errMsg)
        {

            DataTable dtThuockho = SPs.ThuocPhieuNhapkhoKiemtradieukienhuyphieu(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuockho).GetDataSet().Tables[0];

            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Success;
            errMsg = string.Format("Id thuốc ={0}, id thuốc kho ={1}", objPhieuNhapCt.IdThuoc, objPhieuNhapCt.IdThuockho);
            return ActionResult.DataUsed;
        }

        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult HuyXacNhanPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            string GUID = THU_VIEN_CHUNG.GetGUID();
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region Kiểm tra tình huống sau khi xuất kho thì người dùng lập phiếu trả lại thuốc thừa
                            //ActionResult kiemtrathuochuyxacnhan = Kiemtradieukienhuyphieunhapkho(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (kiemtrathuochuyxacnhan != ActionResult.Success) return kiemtrathuochuyxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc trong kho nhập còn đủ để trừ hay không(khả dụng có tính cả số lượng này)
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdThuockho.Value, false, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            ////Kiểm tra ở kho nhập xem thuốc đã sử dụng chưa
                            //kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (kiemtrathuochuyxacnhan != ActionResult.Success) return kiemtrathuochuyxacnhan;
                            //Xóa biến động kho nhập
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuNhapKho).Execute();
                            //Xóa biến động kho xuất
                            new Delete().From(TBiendongThuoc.Schema)
                               .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                               .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                               .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuXuatKho).Execute();
                            int numofRec = -1;
                            long id_Thuockho_new = -1;
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(-1)
                              .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                                      objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, objPhieuNhapCt.IdThuockho.Value, id_Thuockho_new, objPhieuNhapCt.IdChuyen, objPhieuNhapCt.NgayNhap,
                                                                      objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0],-1);
                            #region áp dụng khi sử dụng id phiếu, id chuyển trong t thuốc kho
                            //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(objPhieuNhapCt.SoLuong, objPhieuNhapCt.IdChuyen, numofRec);
                            //sp.Execute();
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //if (numofRec <= 0)
                            //{
                            //    errMsg = Utility.sDbnull(objPhieuNhapCt.IdChuyen, "0");
                            //    return ActionResult.notExists;
                            //}
                            #endregion
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, 0, errorMessage);
                            sp.Execute();
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapCt.IdChuyen), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), Utility.ByteDbnull(objPhieuNhap.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                  "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhap.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh) : string.Format("Vận chuyển thuốc từ kho {0} sang kho {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhonhap));
                        }

                        string LACTN = string.Format("Hủy xác nhận(hủy duyệt phiếu) bởi {0} vào lúc {1} tại địa chỉ {2}", globalVariables.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), globalVariables.gv_strIPAddress);
                        Utility.Log("frm_PhieuChuyenKho", globalVariables.UserName, string.Format("Hủy xác nhận phiếu xuất kho ID={0} thành công", objPhieuNhap.IdPhieu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Set(TPhieuNhapxuatthuoc.Columns.LastActionName).EqualTo(LACTN)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu chuyển kho", ex);
                return ActionResult.Error;
            }
        }

      
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult ThemPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
               string GUID = THU_VIEN_CHUNG.GetGUID();
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgayTao = DateTime.Now;
                        objPhieuNhap.NguoiTao = globalVariables.UserName;
                        objPhieuNhap.MaPhieu = Utility.sDbnull(THU_VIEN_CHUNG.MaNhapKho(Utility.Int32Dbnull(objPhieuNhap.LoaiPhieu)));
                        objPhieuNhap.TongTien = arrPhieuNhapCts.Sum(c => c.ThanhTien);
                        objPhieuNhap.IsNew = true;
                        objPhieuNhap.Save();
                       
                        objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(objPhieuNhap.IdPhieu);
                        if (objPhieuNhap != null)
                        {
                            foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                            {
                                objPhieuNhapCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                                objPhieuNhapCt.IsNew = true;
                                objPhieuNhapCt.Save();
                                THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapCt.IdChuyen), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong),Utility.ByteDbnull( objPhieuNhap.LoaiPhieu,(byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                    "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhap.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh) : (objPhieuNhap.LoaiPhieu == 6 ? string.Format("Xuất thuốc từ kho nội trú sang tủ trực khoa nội trú. Id kho chuyển: {0} sang  id kho nhận: {1} thuộc khoa {2} sang", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhonhap, objPhieuNhap.IdKhoalinh) : (objPhieuNhap.LoaiPhieu == 8 ? string.Format("Trả thuốc từ kho {0} về nhà cung cấp {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.MaNhacungcap) : string.Format("Trả thuốc từ tủ trực nội trú về kho nội trú. Id kho chuyển: {0} thuộc khoa {1} sang id kho nhận: {2}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhoalinh, objPhieuNhap.IdKhonhap))));
                            }
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh them :{0}", exception);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult UpdatePhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                string GUID = THU_VIEN_CHUNG.GetGUID();
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgaySua =  DateTime.Now;
                        objPhieuNhap.NguoiSua = globalVariables.UserName;
                        objPhieuNhap.TongTien = arrPhieuNhapCts.Sum(c => c.ThanhTien);
                        objPhieuNhap.Save();

                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                        new Delete().From(TTamke.Schema)
                           .Where(TTamke.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                           .And(TTamke.Columns.Loai).IsEqualTo(objPhieuNhap.LoaiPhieu)
                           .Execute();
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                        {
                            objPhieuNhapCt.IdPhieu= Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                            objPhieuNhapCt.IsNew = true;
                            objPhieuNhapCt.Save();
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapCt.IdPhieuchitiet, objPhieuNhapCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapCt.IdChuyen), objPhieuNhapCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), Utility.ByteDbnull(objPhieuNhap.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                   "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), string.Format("Vận chuyển thuốc từ kho {0} sang kho {1}", objPhieuNhap.IdKhoxuat, objPhieuNhap.IdKhonhap));
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh sua phieu nhap kho :{0}", exception);
                return ActionResult.Error;

            }
        }

        /// <summary>
        /// hàm thực hiện việc xóa thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="PresDetail_ID"></param>
        /// <returns></returns>
        public ActionResult XoaThongTinChiTietDonThuoc(System.Collections.ArrayList arrayList,int Pres_ID)
        {
              try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(arrayList).Execute();
                        SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID);
                        if(sqlQuery.GetRecordCount()<=0)
                        {
                            KcbDonthuoc.Delete(Pres_ID);
                        }

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
              catch (Exception exception)
              {
                  log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                  return ActionResult.Error;

              }
            
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="PresDetail_ID"></param>
        /// <returns></returns>
        public ActionResult XoaThongTinThongTinDonThuoc(int Pres_ID)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(Pres_ID).Execute();
                        KcbDonthuoc.Delete(Pres_ID);
                      

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;
            }
        }

      
      
     

        #region "hàm thực hiện việc hủy thông tin phat thuốc cho kho"
        public ActionResult HuyXacNhanDonThuocBN(KcbDonthuoc objDonthuoc,KcbDonthuocChitiet []arrDetails)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties=new HisDuocProperties();
                       // if(objHisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")id_kho=Utility.Int32Dbnull(objDonthuoc.IdKho)
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        int id_thuockho = -1;
                        foreach (KcbDonthuocChitiet objDetail in arrDetails)
                        {
                            SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc);
                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection =
                                sqlQuery.ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                TThuockho objTK = TThuockho.FetchByID(PhieuXuatBnhanCt.IdThuockho);
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.GiaBan,
                                                                 PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                 PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho, PhieuXuatBnhanCt.MaNhacungcap,
                                                                 PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau, -1, id_thuockho, PhieuXuatBnhanCt.IdThuockho,
                                                                 objDonthuoc.NgayXacnhan, PhieuXuatBnhanCt.GiaBhyt, PhieuXuatBnhanCt.PhuthuDungtuyen,
                                                                 PhieuXuatBnhanCt.PhuthuTraituyen, objDonthuoc.KieuThuocvattu, objTK.IdQdinh);

                                sp.Execute();
                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();

                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3).Execute();

                                sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieu).IsEqualTo(
                                        PhieuXuatBnhanCt.IdPhieu);
                                if(sqlQuery.GetRecordCount()<=0)
                                {
                                    TPhieuXuatthuocBenhnhan.Delete(PhieuXuatBnhanCt.IdPhieu);
                                }
                            }
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();

                        }
                       SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                             .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                             .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                      
                       
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }
        public ActionResult HuyXacNhanDonThuocBN(long Pres_ID,long id_phieuxuatthuocbenhnhan, int id_kho,DateTime ngay_huy,string lydohuy)
        {
            try
            {
                string GUID = THU_VIEN_CHUNG.GetGUID();
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties = new HisDuocProperties();
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                        KcbDonthuocChitietCollection lstDetail
                           = new Select().From(KcbDonthuocChitiet.Schema)
                           .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                           .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                           .And(KcbDonthuocChitiet.Columns.IdPhieuTXuatthuocBenhnhan).IsEqualTo(id_phieuxuatthuocbenhnhan)
                           .ExecuteAsCollection<KcbDonthuocChitietCollection>();

                        var q = from p in lstDetail
                                where Utility.Byte2Bool(p.DaDung) == true
                                select p;
                        if (q.Any())
                            return ActionResult.DataUsed;
                        if (!globalVariables.isSuperAdmin)
                        {
                            q = from p in lstDetail
                                where Utility.Byte2Bool(p.TrangthaiHuy) == true
                                select p;
                            if (q.Any())
                                return ActionResult.Cancel;
                        }
                       
                        foreach (KcbDonthuocChitiet objDetail in lstDetail)
                        {
                            if (!globalVariables.isSuperAdmin)
                            {
                                DataTable dtTralai = new Select().From(ThuocLichsuTralaithuoctaiquayChitiet.Schema).Where(ThuocLichsuTralaithuoctaiquayChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).ExecuteDataSet().Tables[0];
                                if (dtTralai.Rows.Count > 0)
                                    return ActionResult.Cancel;
                            }

                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc)

                                .ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            
                            //Phần mới này thì mỗi detail chỉ có duy nhất 1 phieuxuatchitiet
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                long id_Thuockho_new = -1;
                                long iTThuockho_old = PhieuXuatBnhanCt.IdThuockho.Value;
                                //Cộng trả lại kho xuất theo Id thuốc kho
                                int numofRec = -1;
                                StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(PhieuXuatBnhanCt.SoLuong, PhieuXuatBnhanCt.IdThuockho, numofRec);
                                sp.Execute();
                                numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                                if (numofRec <= 0)// Tạm làm theo cách cũ
                                {
                                    TThuockho objTK = TThuockho.FetchByID(PhieuXuatBnhanCt.IdThuockho);
                                    sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                                                  PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                  PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                                                  PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau,
                                                                  PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.IdThuockho, PhieuXuatBnhanCt.NgayNhap, PhieuXuatBnhanCt.GiaBhyt, PhieuXuatBnhanCt.PhuthuDungtuyen,
                                                                  PhieuXuatBnhanCt.PhuthuTraituyen, objDonthuoc.KieuThuocvattu, objTK.IdQdinh);
                                    sp.Execute();
                                    //Lấy đầu ra iTThuockho nếu thêm mới để update lại presdetail
                                    id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);
                                }

                               
                               
                                //xóa thông tin bảng chi tiết
                               
                                
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();
                                //Xóa trong bảng biến động
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                     .And(TBiendongThuoc.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieu))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(LoaiPhieu.PhieuXuatKhoBenhNhan).Execute();
                                //Cập nhật laijiTThuockho mới cho chi tiết đơn thuốc

                                if (id_Thuockho_new != -1 ) //Gặp trường hợp khi xuất hết thuốc thì xóa kho-->Khi hủy thì tạo ra dòng thuốc kho mới
                                {
                                    if (id_Thuockho_new != iTThuockho_old)
                                    {
                                        //Cập nhật tất cả các bảng liên quan
                                        new Update(KcbDonthuocChitiet.Schema)
                                        .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();

                                        new Update(TBiendongThuoc.Schema)
                                        .Set(TBiendongThuoc.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(TBiendongThuoc.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();

                                        new Update(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                        .Set(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                        .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                        Execute();
                                    }

                                }
                                //else///Tạm khóa 20/03/2024 do ko chạy vào luồng if (id_Thuockho_new != -1 ) phía trên
                                //{
                                //   string err = Utility.sDbnull(PhieuXuatBnhanCt.IdThuockho, "0");
                                //    return ActionResult.notExists;
                                //}
                                // insert thong tin vao bang tam 
                                //SPs.KcbTblKedonthuocTemptInsert(PhieuXuatBnhanCt.IdChitietdonthuoc,
                                //    PhieuXuatBnhanCt.IdDonthuoc, PhieuXuatBnhanCt.IdKho, PhieuXuatBnhanCt.IdThuoc,
                                //    PhieuXuatBnhanCt.IdThuockho,
                                //    0, DateTime.Now, PhieuXuatBnhanCt.SoLuong).Execute();

                            }
                            //Xóa phiếu đơn thuốc chi tiết
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            //Update trạng thái xác nhận của chi tiết
                            SPs.ThuocCapnhattrangthaicapphatdonthuocChitiet(objDetail.IdChitietdonthuoc, objDetail.SoLuong, 0,-1, globalVariables.UserName, DateTime.Now, GUID, globalVariables.gv_strIPAddress, 1).Execute();
                            // THỪA DÒNG DƯỚI DO THỦ TỤC PHÍA TRÊN ĐÃ THỰC HIỆN. CÓ THỂ BỎ
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(DBNull.Value)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();

                        }
                        //Xóa phiếu xuất bệnh nhân theo ID đơn thuốc
                        new Delete().From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).IsEqualTo(Pres_ID)
                            .And(TPhieuXuatthuocBenhnhan.IdKhoColumn).IsEqualTo(id_kho)
                            .And(TPhieuXuatthuocBenhnhan.Columns.IdPhieu).IsEqualTo(id_phieuxuatthuocbenhnhan)
                            .Execute();
                        //Update trạng thái xác nhận của toàn đơn thuốc-->Phần mới 100% sẽ chạy câu Update
                        SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                              .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                              .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Set(KcbDonthuoc.Columns.NgayCapphat).EqualTo(null)
                                  .Set(KcbDonthuoc.Columns.NgayXacnhan).EqualTo(null)
                                  .Set(KcbDonthuoc.Columns.NgayHuyxacnhan).EqualTo(ngay_huy)
                                  .Set(KcbDonthuoc.Columns.LydoHuyxacnhan).EqualTo(lydohuy)
                                  .Set(KcbDonthuoc.Columns.NguoiHuyxacnhan).EqualTo(globalVariables.UserName)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacNhanDonThuocBNTaiQuay(long Pres_ID, int id_kho, DateTime ngay_huy, string lydohuy)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    HisDuocProperties objHisDuocProperties = new HisDuocProperties();
                    objHisDuocProperties = PropertyLib._HisDuocProperties;
                    KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                    KcbDonthuocChitietCollection lstDetail
                       = new Select().From(KcbDonthuocChitiet.Schema)
                       .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                       .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                       .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    foreach (KcbDonthuocChitiet objDetail in lstDetail)
                    {
                        TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                            .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc)
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();

                        //Phần mới này thì mỗi detail chỉ có duy nhất 1 phieuxuatchitiet
                        foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                        {
                            //Cộng trả lại kho xuất
                            long id_Thuockho_new = -1;
                            long iTThuockho_old = PhieuXuatBnhanCt.IdThuockho.Value;
                            TThuockho objTK = TThuockho.FetchByID(PhieuXuatBnhanCt.IdThuockho);
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                                              PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                              PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                                              PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo, Utility.sDbnull(PhieuXuatBnhanCt.SoDky,""),Utility.sDbnull( PhieuXuatBnhanCt.SoQdinhthau,""),
                                                              PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.IdThuockho, PhieuXuatBnhanCt.NgayNhap, PhieuXuatBnhanCt.GiaBhyt, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.PhuthuDungtuyen, 0),
                                                              Utility.DecimaltoDbnull(PhieuXuatBnhanCt.PhuthuTraituyen, 0), objDonthuoc.KieuThuocvattu, objTK.IdQdinh);
                            sp.Execute();
                            //Lấy đầu ra iTThuockho nếu thêm mới để update lại presdetail
                            id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);
                            ///xóa thông tin bảng chi tiết
                            new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                .Execute();
                            //Xóa trong bảng biến động
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(LoaiPhieu.PhieuXuatKhoBenhNhan).Execute();
                            //Cập nhật laijiTThuockho mới cho chi tiết đơn thuốc
                            if (id_Thuockho_new != -1) //Gặp trường hợp khi xuất hết thuốc thì xóa kho-->Khi hủy thì tạo ra dòng thuốc kho mới
                            {
                                //Cập nhật tất cả các bảng liên quan
                                new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                .Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                Execute();

                                new Update(TBiendongThuoc.Schema)
                                .Set(TBiendongThuoc.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                .Where(TBiendongThuoc.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                Execute();

                                new Update(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Set(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                Execute();

                            }
                        }
                        //Xóa phiếu đơn thuốc chi tiết
                        new Delete().From(TXuatthuocTheodon.Schema)
                            .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                        //Update trạng thái xác nhận của chi tiết
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                            .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(DBNull.Value)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                            Execute();
                    }
                    //Xóa phiếu xuất bệnh nhân theo ID đơn thuốc
                    new Delete().From(TPhieuXuatthuocBenhnhan.Schema)
                        .Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).IsEqualTo(Pres_ID)
                          .And(TPhieuXuatthuocBenhnhan.IdKhoColumn).IsEqualTo(id_kho)
                            .Execute();
                    //Update trạng thái xác nhận của toàn đơn thuốc-->Phần mới 100% sẽ chạy câu Update
                    SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                          .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                          .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                    new Update(KcbDonthuoc.Schema)
                              .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                              .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                              .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                              .Set(KcbDonthuoc.Columns.NgayCapphat).EqualTo(null)
                              .Set(KcbDonthuoc.Columns.NgayXacnhan).EqualTo(null)
                              .Set(KcbDonthuoc.Columns.NgayHuyxacnhan).EqualTo(ngay_huy)
                              .Set(KcbDonthuoc.Columns.LydoHuyxacnhan).EqualTo(lydohuy)
                              .Set(KcbDonthuoc.Columns.NguoiHuyxacnhan).EqualTo(globalVariables.UserName)
                              .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacNhanDonThuocBN_Tutruc(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDetails)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties = new HisDuocProperties();
                        // if(objHisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")id_kho=Utility.Int32Dbnull(objDonthuoc.IdKho)
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        int id_thuockho = -1;
                        foreach (KcbDonthuocChitiet objDetail in arrDetails)
                        {
                            SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(objDetail.IdChitietdonthuoc);
                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection =
                                sqlQuery.ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                 TThuockho objTK = TThuockho.FetchByID(PhieuXuatBnhanCt.IdThuockho);
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.GiaBan,
                                                                 PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                 PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho, PhieuXuatBnhanCt.MaNhacungcap,
                                                                 PhieuXuatBnhanCt.SoLo, PhieuXuatBnhanCt.SoDky, PhieuXuatBnhanCt.SoQdinhthau, -1, id_thuockho, PhieuXuatBnhanCt.IdThuockho,
                                                                 objDonthuoc.NgayXacnhan, PhieuXuatBnhanCt.GiaBhyt,
                                                                 PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen, objDonthuoc.KieuThuocvattu, objTK.IdQdinh);

                                sp.Execute();
                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();
                                
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3).Execute();

                                sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieu).IsEqualTo(
                                        PhieuXuatBnhanCt.IdPhieu);
                                if (sqlQuery.GetRecordCount() <= 0)
                                {
                                    TPhieuXuatthuocBenhnhan.Delete(PhieuXuatBnhanCt.IdPhieu);
                                }
                            }
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();

                        }
                        SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                              .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                              .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();


                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }


        
        #endregion
    }
}
