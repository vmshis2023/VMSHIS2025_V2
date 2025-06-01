using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using System.Data;
using VNS.Properties;
namespace VNS.HIS.NGHIEPVU.THUOC
{
    public class THUOC_NHAPKHO
    {
        private NLog.Logger log;
        public THUOC_NHAPKHO()
        {
            log = NLog.LogManager.GetCurrentClassLogger();

        }
        public DataTable Laydanhsachphieunhapkho(string FromDate, string ToDate, int id_thuoc, int IDKHOA_NHAP, int IDKHONHAP, int IDKHOXUAT, int? IDNHANVIEN,
            int IDNHANCCAP, string ma_nhacungcap, string SOPHIEU, int TRANGTHAI, int LoaiPhieu, string MaKho, byte noitru, string KIEUTHUOCVT)
        {
            return SPs.ThuocLaydanhsachphieunhapxuat(FromDate,
                                            ToDate, id_thuoc,
                                            IDKHONHAP, IDKHOA_NHAP, IDKHOXUAT,
                                            IDNHANVIEN,
                                            IDNHANCCAP, ma_nhacungcap, SOPHIEU, TRANGTHAI, LoaiPhieu, MaKho, noitru, KIEUTHUOCVT).GetDataSet().Tables[0];


        }
        public static int ThuocTongnhapngoaiTrongNam(int? Nam, int? Idthuoc)
        {
            try
            {
                int Soluong = 0;
                StoredProcedure sp = SPs.ThuocTongnhapngoaiTrongNam(Nam, Idthuoc, Soluong);
                sp.Execute();
                Soluong = Utility.Int32Dbnull(sp.OutputValues[0], 0);
                return Soluong;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return 0;
                
            }
           
        }
        public DataTable LaythongtinChitietPhieunhapKho(int? ITPhieuNhapxuatthuoc)
        {
            return SPs.ThuocLaychitietphieunhapxuat(ITPhieuNhapxuatthuoc).GetDataSet()
                           .Tables[0];
        }
        public DataTable Laythongtininphieunhapkhothuoc(int? ITPhieuNhapxuatthuoc)
        {
            return SPs.ThuocLaydulieuinphieunhapxuat(ITPhieuNhapxuatthuoc).GetDataSet().Tables[0];
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông itn phiếu nhập
        /// </summary>
        /// <param name="IDPhieu"></param>
        /// <returns></returns>
        public ActionResult XoaPhieuNhapKho(int IDPhieu)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        TPhieuNhapxuatthuoc _objphieu = TPhieuNhapxuatthuoc.FetchByID(IDPhieu);
                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                        new Delete().From(TTamke.Schema)
                           .Where(TTamke.Columns.IdPhieu).IsEqualTo(IDPhieu)
                           .And(TTamke.Columns.Loai).IsEqualTo(_objphieu.LoaiPhieu)
                           .Execute();
                        new Delete().From(TPhieuNhapxuatthuoc.Schema)
                           .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                        new Delete().From(TThuockho.Schema)
                            .Where(TThuockho.Columns.IdPhieu).IsEqualTo(IDPhieu)
                            .And(TThuockho.Columns.SoLuong).IsLessThanOrEqualTo(0)
                            .Execute();
                        Utility.Log("frm_phieunhapxuat_chung", globalVariables.UserName, string.Format("Xóa phiếu ID={0} thành công", IDPhieu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi thực hiện xóa phiếu", ex);
                return ActionResult.Error;

            }
        }
      
        /// <summary>
        /// hàm thực hhienj iệc xóa thông tin nhập trả cho kho thuốc
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
                        Utility.Log("frm_PhieuTrathuocKhoaveKho", globalVariables.UserName, string.Format("Xóa phiếu trả thuốc từ tủ trực về kho nội trú ID={0} thành công", IDPhieu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xoa phieu nhap kho :{0}", exception);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult ThemPhieuNhapKho(TPhieuNhapxuatthuoc objPhieuNhap, List<TPhieuNhapxuatthuocChitiet> arrPhieuNhapCts)
        {
            try
            {
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

                        //StoredProcedure sp = SPs.ThuocPhieunhapThemmoi(objPhieuNhap.IdPhieu,
                        //                                          Utility.sDbnull(objPhieuNhap.MaPhieu),
                        //                                          Utility.sDbnull(objPhieuNhap.SoHoaDon),
                        //                                          objPhieuNhap.NgayNhapHdon, objPhieuNhap.IdKhonhap,
                        //                                          objPhieuNhap.IdKhoxuat, objPhieuNhap.Vat,
                        //                                          objPhieuNhap.IdNhaCcap, objPhieuNhap.GhiChu,
                        //                                          objPhieuNhap.NguoiGiao, objPhieuNhap.IdNhanvien,
                        //                                          objPhieuNhap.HienThi, objPhieuNhap.TrangThai,
                        //                                          objPhieuNhap.Ngaytao, objPhieuNhap.NguoiTao,
                        //                                          objPhieuNhap.NguoiSua, objPhieuNhap.NgaySua,
                        //                                          objPhieuNhap.IpMayTao, objPhieuNhap.IpMaySua,
                        //                                          objPhieuNhap.TongTien, objPhieuNhap.LoaiPhieu,
                        //                                          objPhieuNhap.MaKieuPhieu, objPhieuNhap.IdNvienXacNhan, objPhieuNhap.NguoiXacnhan, objPhieuNhap.NgayXacnhan, objPhieuNhap.IdKhoaLinh);
                        //sp.Execute();
                        //objPhieuNhap.IdPhieu = Utility.Int32Dbnull(sp.OutputValues[0]);
                        //objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(objPhieuNhap.IdPhieu);
                        if (objPhieuNhap != null)
                        {
                            foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                            {
                                objPhieuNhapCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                                objPhieuNhapCt.IsNew = true;
                                objPhieuNhapCt.Save();
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
        /// hàm thực hiện việc cập nhập thông tin nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult UpdatePhieuNhapKho(TPhieuNhapxuatthuoc objPhieuNhap, List<TPhieuNhapxuatthuocChitiet> arrPhieuNhapCts)
        {
            try
            {
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
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                        {
                            objPhieuNhapCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                            objPhieuNhapCt.IsNew = true;
                            objPhieuNhapCt.Save();
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
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuNhapKho(TPhieuNhapxuatthuoc objPhieuNhap,DateTime ngayxacnhan)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        //Sẽ hướng đơn vị theo kiểu bốc thuốc trong bảng t_thuockho+Cho nhập giá BHYT,giá DV ngay tại chức năng nhập thuốc từ nhà cung cấp
                        bool BHYT_LUACHON_APDUNG = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_LUACHON_APDUNG", "0", false), 0) == 1;
                        bool TUDONG_CAPNHAT_GIADICHVU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TUDONG_CAPNHAT_GIADICHVU", "0", false), 0) == 1;
                        bool BHYT_CHOPHEPNHAPGIAPHUTHU = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_CHOPHEPNHAPGIAPHUTHU", "0", false), 0) == 1;
                        TDmucKho objkhonhap = TDmucKho.FetchByID(objPhieuNhap.IdKhonhap);
                        TDmucKho objkhoxuat = TDmucKho.FetchByID(objPhieuNhap.IdKhoxuat);
                        DmucKhoaphong objkhoa = DmucKhoaphong.FetchByID(objPhieuNhap.IdKhoalinh);
                        int num = 0;
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                           
                            long idthuockho=-1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhap.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                                                                      objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau,
                                                                      -1, idthuockho,-1, ngayxacnhan, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu, objPhieuNhapCt.IdQdinh);

                            sp.Execute();
                            idthuockho=Utility.Int64Dbnull(sp.OutputValues[0],-1);
                            num = new Update(TPhieuNhapxuatthuocChitiet.Schema)
                                .Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                                .Set(TPhieuNhapxuatthuocChitiet.Columns.NgayNhap).EqualTo(ngayxacnhan.Date)
                                .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            //Cập nhật số lượng nhập trong thầu
                            TThauChitiet objthauct = TThauChitiet.FetchByID(objPhieuNhapCt.IdThauCt);
                            if (objthauct != null)
                            {
                                int sl_nhap = Utility.Int32Dbnull(objthauct.SlNhap, 0) +Utility.Int32Dbnull( objPhieuNhapCt.SoLuong,0);
                                num = new Update(TThauChitiet.Schema)
                                   .Set(TThauChitiet.Columns.SlNhap).EqualTo(sl_nhap)
                                   .Where(TThauChitiet.Columns.IdThauCt).IsEqualTo(objPhieuNhapCt.IdThauCt).Execute();
                            }
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.NgayNhap = ngayxacnhan.Date;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.ThuocVay = objPhieuNhap.PhieuVay;
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = DateTime.Now;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            //objXuatNhap.IdNhaCcap = Utility.Int32Dbnull(objPhieuNhap.IdNhaCcap);
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdQdinh = objPhieuNhapCt.IdQdinh;
                           
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(objPhieuNhap.LoaiPhieu);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.IdThuockho = idthuockho;
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;

                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu =objPhieuNhapCt.GiaBhytCu;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.DuTru = 0;
                            objXuatNhap.KieuThuocvattu = objPhieuNhap.KieuThuocvattu;
                            objXuatNhap.MotaThem = Utility.Laythongtinbiendongthuoc(objXuatNhap.MaLoaiphieu.Value, objkhoxuat != null ? objkhoxuat.TenKho : "", objkhonhap != null ? objkhonhap.TenKho : "", objkhoa != null ? objkhoa.TenKhoaphong : "", "", "");
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           //KHóa lại ngày 22/05/2025 tránh cập nhật lại giá =0 khi nhập kho để giá bán =0
                            //if ((Utility.ByteDbnull(objPhieuNhapCt.CoBhyt, 0) == 1 && BHYT_LUACHON_APDUNG) || TUDONG_CAPNHAT_GIADICHVU)
                            //{
                            //    DmucDoituongkcbCollection _lstdoituong = new Select().From(DmucDoituongkcb.Schema).ExecuteAsCollection<DmucDoituongkcbCollection>();
                            //    //DmucDoituongkcbCollection _kcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(0).ExecuteAsCollection<DmucDoituongkcbCollection>();
                            //    DmucThuoc _objThuoc = new Select().From(DmucThuoc.Schema).Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(objPhieuNhapCt.IdThuoc).ExecuteSingle<DmucThuoc>();
                            //    if (_lstdoituong != null && _objThuoc != null )
                            //    {
                                    
                            //        //new Update(DmucThuoc.Schema)
                            //        //.Set(DmucThuoc.Columns.SoDangky).EqualTo(objPhieuNhapCt.SoDky)
                            //        //.Set(DmucThuoc.Columns.QD31).EqualTo(objPhieuNhapCt.SoQdinhthau)
                            //        //    .Set(DmucThuoc.Columns.DonGia).EqualTo(objPhieuNhapCt.DonGia)
                            //        //    .Set(DmucThuoc.Columns.GiaDv).EqualTo(objPhieuNhapCt.GiaBan)
                            //        //    .Set(DmucThuoc.Columns.GiaBhyt).EqualTo(objPhieuNhapCt.GiaBhyt)
                            //        //    .Set(DmucThuoc.Columns.PhuthuDungtuyen).EqualTo(objPhieuNhapCt.GiaPhuthuDungtuyen)
                            //        //    .Set(DmucThuoc.Columns.PhuthuTraituyen).EqualTo(objPhieuNhapCt.GiaPhuthuTraituyen)
                            //        //    .Where(DmucThuoc.Columns.IdThuoc).IsEqualTo(_objThuoc.IdThuoc).Execute();
                            //        foreach (DmucDoituongkcb _kcb in _lstdoituong)
                            //        {
                            //            if ((Utility.ByteDbnull(objPhieuNhapCt.CoBhyt, 0) == 1 && BHYT_LUACHON_APDUNG && _kcb.IdLoaidoituongKcb == 0) || (TUDONG_CAPNHAT_GIADICHVU && _kcb.IdLoaidoituongKcb == 1))
                            //            {
                            //                decimal DonGia = 0m;
                            //                decimal PhuthuDungtuyen = 0m;
                            //                decimal PhuthuTraituyen = 0m;
                            //                bool allowupdate = false;
                            //                QheDoituongThuoc objQhe = new Select().From(QheDoituongThuoc.Schema)
                            //                    .Where(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(objPhieuNhapCt.IdThuoc)
                            //              .And(QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(_kcb.IdLoaidoituongKcb).ExecuteSingle<QheDoituongThuoc>();
                            //                if (objQhe!=null)//Đã có quan hệ-->Cập nhật lại
                            //                {
                            //                    if (_kcb.IdLoaidoituongKcb == 0)
                            //                    {
                            //                        if (Utility.ByteDbnull(objPhieuNhapCt.CoBhyt, 0) == 1 && BHYT_LUACHON_APDUNG)
                            //                        {
                            //                            allowupdate = true;
                            //                            DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt, 0);
                            //                            PhuthuDungtuyen = BHYT_CHOPHEPNHAPGIAPHUTHU ? Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen, 0) : objQhe.PhuthuDungtuyen.Value;
                            //                            PhuthuTraituyen = BHYT_CHOPHEPNHAPGIAPHUTHU ? Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen, 0) : objQhe.PhuthuTraituyen.Value;
                            //                        }
                            //                    }
                            //                    else
                            //                    {
                            //                        if (TUDONG_CAPNHAT_GIADICHVU)
                            //                        {
                            //                            allowupdate = true;
                            //                            DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan, 0);
                            //                            PhuthuDungtuyen =  objQhe.PhuthuDungtuyen.Value;
                            //                            PhuthuTraituyen = objQhe.PhuthuTraituyen.Value;
                            //                        }
                            //                    }
                            //                    if (allowupdate)
                            //                        new Update(QheDoituongThuoc.Schema)
                            //                            .Set(QheDoituongThuoc.Columns.NgaySua).EqualTo(DateTime.Now)
                            //                            .Set(QheDoituongThuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            //                            .Set(QheDoituongThuoc.Columns.DonGia).EqualTo(DonGia)
                            //                            .Set(QheDoituongThuoc.Columns.PhuthuDungtuyen).EqualTo(PhuthuDungtuyen)
                            //                            .Set(QheDoituongThuoc.Columns.PhuthuTraituyen).EqualTo(PhuthuTraituyen)
                            //                            .Where(QheDoituongThuoc.Columns.IdLoaidoituongKcb).IsEqualTo(_kcb.IdLoaidoituongKcb)
                            //                            .And(QheDoituongThuoc.Columns.IdThuoc).IsEqualTo(objPhieuNhapCt.IdThuoc)
                            //                                .Execute();
                            //                }
                            //                else
                            //                {
                            //                     if (_kcb.IdLoaidoituongKcb == 0)
                            //                    {
                            //                        if (Utility.ByteDbnull(objPhieuNhapCt.CoBhyt, 0) == 1 && BHYT_LUACHON_APDUNG)
                            //                        {
                            //                            allowupdate = true;
                            //                            DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt, 0);
                            //                            PhuthuDungtuyen = BHYT_CHOPHEPNHAPGIAPHUTHU ? Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen, 0) : 0m;
                            //                            PhuthuTraituyen = BHYT_CHOPHEPNHAPGIAPHUTHU ? Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen, 0) : 0m;
                            //                        }
                            //                    }
                            //                    else
                            //                    {
                            //                        if (TUDONG_CAPNHAT_GIADICHVU)
                            //                        {
                            //                            allowupdate = true;
                            //                            DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan, 0);
                            //                            PhuthuDungtuyen = 0m;
                            //                            PhuthuTraituyen = 0m;
                            //                        }
                            //                    }
                            //                     if (allowupdate)
                            //                     {
                            //                         QheDoituongThuoc _newItems = new QheDoituongThuoc();
                            //                         _newItems.IdDoituongKcb = _kcb.IdDoituongKcb;
                            //                         _newItems.IdLoaithuoc = _objThuoc.IdLoaithuoc;
                            //                         _newItems.IdThuoc = objPhieuNhapCt.IdThuoc;
                            //                         _newItems.TyleGiamgia = 0;
                            //                         _newItems.KieuGiamgia = "%";
                            //                         _newItems.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBhyt, 0);
                            //                         _newItems.PhuthuDungtuyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuDungtuyen, 0);
                            //                         _newItems.PhuthuTraituyen = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaPhuthuTraituyen, 0);
                            //                         _newItems.IdLoaidoituongKcb = _kcb.IdLoaidoituongKcb;
                            //                         _newItems.MaDoituongKcb = _kcb.MaDoituongKcb;

                            //                         _newItems.NguoiTao = globalVariables.UserName;
                            //                         _newItems.NgayTao = DateTime.Now;
                            //                         _newItems.MaKhoaThuchien = "ALL";
                            //                         _newItems.IsNew = true;
                            //                         _newItems.Save();
                            //                     }


                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                        Utility.Log("frm_PhieuNhapKho", globalVariables.UserName, string.Format("Xác nhận phiếu nhập kho ID={0} thành công", objPhieuNhap.IdPhieu), newaction.ConfirmData, this.GetType().Assembly.ManifestModule.Name);
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
        /// Hủy xác nhận phiếu nhập kho-->trừ thuốc khỏi kho
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt)
        {
            ////Kiểm tra xem có bản ghi trong bảng tạm kê đang ở trạng thái--> Bỏ 230823
            //KcbDonthuocChitietCollection _dtct = new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(objPhieuNhapCt.IdThuockho).ExecuteAsCollection<KcbDonthuocChitietCollection>();
            //if (_dtct.Count>0) return ActionResult.DataUsed;
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
            DataTable dtThuockho = SPs.ThuocNhapkhoKiemtratruockhihuy(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuockho).GetDataSet().Tables[0];
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Exceed;
            //if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi mất dòng dữ liệu trong bảng kho-thuốc
            decimal SoLuong = Utility.DecimaltoDbnull(dtThuockho.Rows[0][0], 0);// vCollection[0].SoLuong;
            SoLuong = SoLuong - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);
            if (SoLuong < 0) return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            return ActionResult.Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtradieukienhuyphieunhapkho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt,ref string errMsg)
        {

            DataTable dtThuockho = SPs.ThuocPhieuNhapkhoKiemtradieukienhuyphieu(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuockho).GetDataSet().Tables[0];
            
            if (dtThuockho == null || dtThuockho.Rows.Count <= 0) return ActionResult.Success;
            errMsg = string.Format("Id thuốc ={0}, id thuốc kho ={1}", objPhieuNhapCt.IdThuoc,  objPhieuNhapCt.IdThuockho);
            return ActionResult.DataUsed;
        }
        public ActionResult Kiemtrathuochuychuyenkho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt)
        {
            using (var scope = new TransactionScope())
            {
                //TThuockhoCollection vCollection2 = new TThuockhoCollection();
                TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
                  TThuockho.CreateQuery()
                  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhoxuat)
                  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
                  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
                  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
                  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
                  .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
                  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
                  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
                  );
                if (vCollection.Count <= 0)
                {
                    TThuockho TThuockho1 = new TThuockho();
                    TThuockho1.IdThuoc = objPhieuNhapCt.IdThuoc;
                    TThuockho1.IdKho = Utility.Int32Dbnull(objPhieuNhap.IdKhoxuat);
                    TThuockho1.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                    TThuockho1.GiaNhap = objPhieuNhapCt.GiaNhap;
                    TThuockho1.GiaBan = objPhieuNhapCt.GiaBan;
                    TThuockho1.SoLuong = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);
                    TThuockho1.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                    TThuockho1.Vat = objPhieuNhapCt.Vat.Value;
                    TThuockho1.SoLo = objPhieuNhapCt.SoLo;
                    TThuockho1.IsNew = true;
                    TThuockho1.Save();
                }
                decimal sl = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0) + vCollection[0].SoLuong;
                new Update(TThuockho.Schema)
                    .Set(TThuockho.Columns.SoLuong).EqualTo(sl)
                    .Where(TThuockho.Columns.IdKho).IsEqualTo(objPhieuNhap.IdKhoxuat).And(TThuockho.Columns.IdThuoc).
                    IsEqualTo(objPhieuNhapCt.IdThuoc).And(TThuockho.Columns.GiaNhap).IsEqualTo(objPhieuNhapCt.GiaNhap)
                    .And(TThuockho.Columns.GiaBan).IsEqualTo(objPhieuNhapCt.GiaBan)
                    .And(TThuockho.Columns.NgayHethan).IsEqualTo(objPhieuNhapCt.NgayHethan)
                    .And(TThuockho.Columns.Vat).IsEqualTo(objPhieuNhapCt.Vat)
                    .And(TThuockho.Columns.MaNhacungcap).IsEqualTo(objPhieuNhapCt.MaNhacungcap)
                    .And(TThuockho.Columns.SoLo).IsEqualTo(objPhieuNhapCt.SoLo)
                    .Execute();

                TThuockhoCollection vCollection1 = new TThuockhoController().FetchByQuery(
                  TThuockho.CreateQuery()
                  .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
                  .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
                  .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
                  .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
                  .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
                   .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
                  .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
                  .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
                  );
                if (vCollection1.Count <= 0) return ActionResult.Exceed;//Lỗi mất dòng dữ liệu trong bảng kho-thuốc
                decimal SoLuong1 = vCollection1[0].SoLuong;
                SoLuong1 = SoLuong1 - Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong, 0);
                if (SoLuong1 < 0) return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
                scope.Complete();
                return ActionResult.Success;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult HuyXacNhanPhieuNhapKho(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        int num = 0;
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            #region Sol 1: nếu mỗi phiếu nhập tạo 1 dòng ID thuốc kho(các thông tin khác của thuốc giống hệt nhau)
                            //ActionResult kiemtrathuochuyxacnhan = Kiemtradieukienhuyphieunhapkho(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            //if (kiemtrathuochuyxacnhan != ActionResult.Success) return kiemtrathuochuyxacnhan;
                            #endregion

                            //Sol 2: Kiểm tra xem sau khi hủy thì thuốc trong kho còn đủ để duyệt cho số lượng chờ xác nhận hay không?
                            ActionResult kiemtrathuochuyxacnhan = KiemTra.KiemtraTonthuoctheoIdthuockho_PhieuNhapxuat(objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.SoLuong, 1m, objPhieuNhapCt.IdThuockho, true, ref errMsg);
                            if (kiemtrathuochuyxacnhan != ActionResult.Success) return kiemtrathuochuyxacnhan;

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                   .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                            //Cập nhật số lượng nhập trong thầu
                            TThauChitiet objthauct = TThauChitiet.FetchByID(objPhieuNhapCt.IdThauCt);
                            if (objthauct != null)
                            {
                                int sl_nhap = Utility.Int32Dbnull(objthauct.SlNhap, 0) - Utility.Int32Dbnull(objPhieuNhapCt.SoLuong, 0);
                                num = new Update(TThauChitiet.Schema)
                                   .Set(TThauChitiet.Columns.SlNhap).EqualTo(sl_nhap)
                                   .Where(TThauChitiet.Columns.IdThauCt).IsEqualTo(objPhieuNhapCt.IdThauCt).Execute();
                            }
                            num = new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(-1)
                                .Set(TPhieuNhapxuatthuocChitiet.Columns.NgayNhap).EqualTo(null)
                               .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();

                            StoredProcedure sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuoc,
                                objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                Utility.DecimaltoDbnull(objPhieuNhap.Vat), objPhieuNhapCt.SoLuong,
                                objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                0, errMsg);

                            sp.Execute();
                        }
                        Utility.Log("frm_PhieuNhapKho", globalVariables.UserName, string.Format("Hủy các nhận phiếu nhập kho ID={0} thành công", objPhieuNhap.IdPhieu), newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(-1)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận nhập kho", ex);
                return ActionResult.Error;

            }
        }

       

    }
}
