using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using NLog;
using System.Transactions;
using VNS.Properties;
namespace VNS.HIS.NGHIEPVU.THUOC
{
   public class PhieuTraLai
    {
         private NLog.Logger log;
         public PhieuTraLai()
        {
            log = NLog.LogManager.GetCurrentClassLogger();

        } 
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult ThemPhieuTraLaiKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgayTao = DateTime.Now;
                        objPhieuNhap.NguoiTao = globalVariables.UserName;
                        objPhieuNhap.MaPhieu = Utility.sDbnull(THU_VIEN_CHUNG.MaTraLaiKho());
                        objPhieuNhap.IsNew = true;
                        objPhieuNhap.Save();
                        if (objPhieuNhap != null)
                        {
                          
                            foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
                            {
                                objPhieuNhapTraCt.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapTraCt.GiaNhap) *
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong);
                                objPhieuNhapTraCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                                objPhieuNhapTraCt.IsNew = true;
                                objPhieuNhapTraCt.Save();
                            }
                        }

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh them phieu nhap kho :{0}", exception);
                return ActionResult.Error;

            }
        }
       /// <summary>
       /// hàm thưc hiện việc xóa thông tin của phiếu nhập trả kho lẻ
       /// </summary>
       /// <param name="IDPhieu"></param>
       /// <returns></returns>
        public ActionResult XoaPhieuNhapTraKho(int IDPhieu)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                        new Delete().From(TPhieuNhapxuatthuoc.Schema)
                           .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xóa phiếu trả thuốc từ kho lẻ về kho chẵn", ex);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult UpdatePhieuTraLaiKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgaySua =  DateTime.Now;
                        objPhieuNhap.NguoiSua = globalVariables.UserName;
                        objPhieuNhap.MarkOld();
                        objPhieuNhap.IsNew = false;
                        objPhieuNhap.Save();
                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
                        {
                            objPhieuNhapTraCt.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapTraCt.GiaNhap)*
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong);
                            objPhieuNhapTraCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                            objPhieuNhapTraCt.IsNew = true;
                            objPhieuNhapTraCt.Save();
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
        /// Kiểm tra xem thuốc trong kho xuất đã được sử dụng hay chưa?
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
              .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
              .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
              .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
              .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
              );

            if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            decimal soLuong = vCollection[0].SoLuong;
            soLuong = soLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong,0);
            if (soLuong < 0) return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            return ActionResult.Success;
        }
        public ActionResult Kiemtrathuochuyxacnhan_tralai(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdThuockhoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuockho)
              );

            if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            decimal SoLuong = vCollection[0].SoLuong;
            SoLuong = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);
            if (SoLuong < 0) return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            return ActionResult.Success;
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult HuyXacNhanPhieuTralaiKho(TPhieuNhapxuatthuoc objPhieuNhapTra,ref string ErrMsg)
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
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu);
                        var objPhieuNhaptraCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in objPhieuNhaptraCtCollection)
                        {
                            #region Giải pháp tách theo id phiếu nhập
                            //ActionResult kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan_tralai(objPhieuNhapTra, objPhieuNhapTraCt);
                            //if (kiemtrathuochuyxacnhan != ActionResult.Success) return kiemtrathuochuyxacnhan;
                            #endregion
                            //Kiểm tra thuốc trong kho nhập có đủ để hoàn lại kho trả hay không
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapTraCt.IdThuoc, objPhieuNhapTra.IdKhonhap, objPhieuNhapTraCt.SoLuong, 1, objPhieuNhapTraCt.IdThuockho.Value, false, ref ErrMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan).Execute();

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuXuatKhoLeTraKhoChan).Execute();

                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(-1)
                             .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet).Execute();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapTraCt.NgayHethan,
                                objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                objPhieuNhapTraCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                objPhieuNhapTraCt.IdThuoc, objPhieuNhapTra.IdKhoxuat,
                                objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo, objPhieuNhapTraCt.SoDky,
                                objPhieuNhapTraCt.SoQdinhthau, objPhieuNhapTraCt.IdChuyen, id_thuockho, objPhieuNhapTraCt.IdThuockho,
                                objPhieuNhapTraCt.NgayNhap, objPhieuNhapTraCt.GiaBhyt,
                                objPhieuNhapTraCt.GiaPhuthuDungtuyen, objPhieuNhapTraCt.GiaPhuthuTraituyen,
                                objPhieuNhapTraCt.KieuThuocvattu, objPhieuNhapTraCt.IdQdinh);
                            sp.Execute();
                            #region Case 2
                            //int numofRec = -1;
                            //StoredProcedure sp = SPs.ThuocPhieunhapxuatHuyxacnhan(objPhieuNhapTraCt.SoLuong, objPhieuNhapTraCt.IdChuyen, numofRec);
                            //sp.Execute();
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //if (numofRec <= 0)
                            //{
                            //    ErrMsg = Utility.sDbnull(objPhieuNhapTraCt.IdChuyen, "0");
                            //    return ActionResult.notExists;
                            //}
                            #endregion
                            sp = SPs.ThuocXuatkho(objPhieuNhapTra.IdKhonhap, objPhieuNhapTraCt.IdThuoc,
                                objPhieuNhapTraCt.NgayHethan, objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                Utility.DecimaltoDbnull(objPhieuNhapTraCt.SoLuong), objPhieuNhapTraCt.IdThuockho,
                                objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo,
                                0, errorMessage);

                            sp.Execute();
                            THU_VIEN_CHUNG.UpdateKeTam(objPhieuNhapTraCt.IdPhieuchitiet, objPhieuNhapTraCt.IdPhieu, GUID,"", Utility.Int64Dbnull(objPhieuNhapTraCt.IdChuyen), objPhieuNhapTraCt.IdThuoc, Utility.Int16Dbnull(objPhieuNhapTra.IdKhoxuat), Utility.DecimaltoDbnull(objPhieuNhapTraCt.SoLuong), Utility.ByteDbnull(objPhieuNhapTra.LoaiPhieu, (byte)LoaiTamKe.XUATKHO),// (byte)LoaiTamKe.XUATKHO,
                                 "-1", -1, 0, THU_VIEN_CHUNG.GetSysDateTime(), objPhieuNhapTra.LoaiPhieu == 15 ? string.Format("Xuất hao phí từ kho {0} sang khoa {1}", objPhieuNhapTra.IdKhoxuat, objPhieuNhapTra.IdKhoalinh) : string.Format("Trả thuốc từ kho lẻ về kho chẵn. Id kho chuyển: {0} sang id kho nhận: {1}", objPhieuNhapTra.IdKhoxuat, objPhieuNhapTra.IdKhonhap));
                        }
                        Utility.Log("frm_PhieuTrathuoc_KhoLeVeKhoChan", globalVariables.UserName, string.Format("Hủy các nhận phiếu trả thuốc từ kho lẻ về kho chẵn ID={0} thành công", objPhieuNhapTra.IdPhieu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhapTra.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
               ErrMsg="Lỗi khi hủy xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn-->"+ex.Message;
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanTraLaiKhoLeVeKhoChan(TPhieuNhapxuatthuoc objPhieuNhap, DateTime _ngayxacnhan,ref string ErrMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            bool Chopheptralaisaikho = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_PHIEUTRALAIKHOLEVEKHOCHAN_CHOPHEPTRASAIKHO", "0", true) == "1";
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
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {

                            #region//Kiểm tra ở kho xuất có đủ thuốc trả hay không
                            //ActionResult _Kiemtrathuocxacnhan = KiemTra.Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt,objPhieuNhapCt.IdChuyen.Value, ref ErrMsg);
                            //if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            #endregion
                            //Kiểm tra xem thuốc trong kho trả còn đủ để trừ hay không(khả dụng có tính cả số lượng này)
                            ActionResult _Kiemtrathuocxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.SoLuong, 1, objPhieuNhapCt.IdChuyen.Value, true, ref ErrMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;
                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan,
                                    objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                    objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                    objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                                    objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau,
                                    objPhieuNhapCt.IdChuyen, idthuockho, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt,
                                    objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen,
                                    objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            #region 
                            //int numofRec = -1;
                            //StoredProcedure sp = SPs.ThuocPhieutrakholevekhochanXacnhan(objPhieuNhap.IdKhonhap, objPhieuNhapCt.SoLuong, objPhieuNhapCt.IdChuyen, idthuockho, numofRec);//IdKhoxuat= id kho trả lại
                            //sp.Execute();
                            //idthuockho = Utility.Int32Dbnull(sp.OutputValues[0], -1);
                            //numofRec = Utility.Int32Dbnull(sp.OutputValues[1], -1);
                            //if (numofRec <= 0)// A chuyển B, B lại trả lại nhưng nhập kho C
                            //{
                            //    if (Chopheptralaisaikho)
                            //    {
                            //        sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan,
                            //        objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                            //        objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                            //        objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                            //        objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau,
                            //        objPhieuNhapCt.IdChuyen, idthuockho, objPhieuNhapCt.IdThuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt,
                            //        objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen,
                            //        objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            //        sp.Execute();
                            //        idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            //    }
                            //    else
                            //    {
                            //        ErrMsg = string.Format("id thuốc ={0}, id chuyển ={1}", objPhieuNhapCt.IdThuoc, objPhieuNhapCt.IdChuyen);
                            //        return ActionResult.UNKNOW;
                            //    }
                            //}
                            #endregion

                            //log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhan, objPhieuNhapCt.IdPhieuChitiet));

                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                0, errorMessage);

                            sp.Execute();
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                              .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            ///Biến động trong kho nhận(kho chẵn)
                          
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.ThuocVay = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.DuTru = 0;


                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdThuockho;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           
                            ///Biến động trong kho trả(kho lẻ)
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet= Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.DuTru = 0;
                            objXuatNhap.ThuocVay = 0;

                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                             objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.IdChuyen = -1;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           

                        }
                        Utility.Log("frm_PhieuTrathuoc_KhoLeVeKhoChan", globalVariables.UserName, string.Format("Xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn ID={0} thành công", objPhieuNhap.IdPhieu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
                      int _numAffected=  new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( DateTime.Now)
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
               ErrMsg="Lỗi khi xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn-->"+ex.Message;
                return ActionResult.Error;

            }

        }

        /// <summary>
        /// hàm thực hiện việc xác nhận phiếu nhập
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuNhapTraNhaCungCap(TPhieuNhapxuatthuoc objPhieuNhap)
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
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                           
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuTraNcc);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                   objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                   objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                   objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, id_thuockho, objPhieuNhapCt.IdThuockho,
                                                                   objPhieuNhap.NgayXacnhan, objPhieuNhapCt.GiaBhyt,
                                                                   objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdPhieuchitiet));
                            //phiếu xuất về kho từ kho lẻ
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objXuatNhap.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,  0, errorMessage);

                            sp.Execute();
                            log.Info(string.Format("xuat tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdPhieuchitiet));

                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }
    }
}
