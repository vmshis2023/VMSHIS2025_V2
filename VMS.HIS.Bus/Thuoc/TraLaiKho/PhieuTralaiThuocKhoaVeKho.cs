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
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class PhieuTralaiThuocKhoaVeKho
    {
        private NLog.Logger log;
        public PhieuTralaiThuocKhoaVeKho()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        

        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuTraThuocVeKho(TPhieuNhapxuatthuoc objPhieuNhap, DateTime _ngayxacnhan)
        {
            string errorMessage = "";
            try
            {
              
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        TDmucKho objkhonhap = TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat = TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa = DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            //Insert dòng kho nhập
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            //objXuatNhap.IdNhaCcap = Utility.Int32Dbnull(objPhieuNhap.IdNhaCcap);
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapTraKhoLe;
                            objXuatNhap.TenLoaiphieu =Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraKhoLe);
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            //Insert dòng của kho xuất
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            //objXuatNhap.IdNhaCcap = Utility.Int32Dbnull(objPhieuNhap.IdNhaCcap);
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuXuatKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.IdKhoaLinh = objPhieuNhap.IdKhoalinh;
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                                                                      objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, id_thuockho, objPhieuNhapCt.IdThuockho,
                                                                      _ngayxacnhan, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                             sp.Execute();
                           
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objXuatNhap.SoLuong), objPhieuNhapCt.IdThuockho,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, 0, errorMessage);

                            sp.Execute();
                            errorMessage = Utility.sDbnull(sp.OutputValues[0]);


                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( DateTime.Now)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
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
        public ActionResult HuyXacNhanPhieuTraLaiKho(TPhieuNhapxuatthuoc objPhieuNhap)
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
                        
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            //Kiểm tra ở kho nhập xem thuốc đã sử dụng chưa
                            ActionResult _Kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhap, objPhieuNhapCt);
                            if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                                                                      objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, id_thuockho,objPhieuNhapCt.IdThuockho,
                                                                      objPhieuNhap.NgayXacnhan, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);
                            sp.Execute();
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap,objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, 0, errorMessage);

                            sp.Execute();
                            errorMessage = Utility.sDbnull(sp.OutputValues[0]);
                            
                        }
                        //Xóa toàn bộ chi tiết trong TBiendongThuoc
                        new Delete().From(TBiendongThuoc.Schema)
                            .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
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
