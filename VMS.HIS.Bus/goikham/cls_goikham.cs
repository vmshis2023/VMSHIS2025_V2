using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using System.Transactions;
namespace VNS.HIS.BusRule.Goikham
{
    public class clsGoikham
    {
        private static Logger log;

        public clsGoikham()
        {
            log = LogManager.GetLogger("Goi_Kham");
        }

        public DataTable LayDanhSachGoiKham(int id_goi,string ma_goi,string ten_goi,byte trang_thai,byte loai_goi,DateTime tungay,DateTime denngay, byte kieu_goi)
        {
            var dt = SPs.GoiLayDanhSachGoiKham(id_goi, ma_goi, ten_goi, trang_thai, loai_goi, kieu_goi, tungay, denngay).GetDataSet().Tables[0];
            return dt;
        }

        public DataTable LayChiTietGoiKhamTheoIdGoi(int idGoiDvu)
        {
            var dt = SPs.GoiLayChiTietTheoMa(idGoiDvu).GetDataSet().Tables[0];
            return dt;
        }

        public DataTable LayChiTietGoiKhamTheoBN(int idGoiDvu, long patientId, int id_dangky)
        {
            var dt = SPs.GoiLayChiTietGoiKhamTheoBN(idGoiDvu, patientId, id_dangky).GetDataSet().Tables[0];
            return dt;
        }

        public string XoaGoiKham(int idGoikham)
        {
            try
            {
                new Delete().From(GoiChitiet.Schema).Where(GoiChitiet.Columns.IdGoi).IsEqualTo(idGoikham).Execute();
                new Delete().From(GoiDanhsach.Schema).Where(GoiDanhsach.Columns.IdGoi).IsEqualTo(idGoikham).Execute();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool kiemtra_sudung_goikham(int idGoikham)
        {
            try
            {
                DataTable dt = SPs.GoiKiemtraSudung(idGoikham).GetDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable LayThongTinGoiKham(int idGoiDvu)
        {
            var dtGoi =
                new Select("(so_tien - MIEN_GIAM - giam_bhyt) as THANH_TIEN, *").From(GoiDanhsach.Schema).Where(GoiDanhsach.Columns.IdGoi).IsEqualTo(idGoiDvu).
                    ExecuteDataSet().Tables[0];
            return dtGoi;
        }

        public DataSet LayDanhMucDichVu()
        {
            var dsDanhMucDichVu = SPs.GoiLayDanhMucDichVu().GetDataSet();
            return dsDanhMucDichVu;
        }

        public string XoaDichVuTrongGoi(int idChiTiet)
        {
            try
            {
                new Delete().From(GoiChitiet.Schema).Where(GoiChitiet.Columns.IdChitiet).IsEqualTo(idChiTiet).Execute();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int ThemSuaGoiKham(int id,byte kieu_goi, string maGoiDvu, string tenGoiDvu, decimal tienGoi, decimal mienGiam, decimal giamTruBHYT, DateTime? hieuLucTuNgay, DateTime? hieuLucDenNgay, byte TrangThai, byte loai_goi, byte LoaiKhuyenmai, string KieuMG,bool khuyenmai_tong)
        {
            GoiDanhsach goiDanhsach;
            if (id == -1)
            {
                goiDanhsach = new GoiDanhsach();
                goiDanhsach.IsNew = true;
            }
            else
            {
                goiDanhsach = GoiDanhsach.FetchByID(id);
                goiDanhsach.MarkOld();
                goiDanhsach.IsNew = false;
            }
            goiDanhsach.LoaiGoi = loai_goi;
            goiDanhsach.KieuGoi = kieu_goi;
            goiDanhsach.KhuyenmaiTatcadvu=LoaiKhuyenmai;
            goiDanhsach.KhuyenmaiTong = khuyenmai_tong;
            goiDanhsach.KieuChietkhau=KieuMG;
            goiDanhsach.MaGoi = maGoiDvu;
            goiDanhsach.TenGoi = tenGoiDvu;
            goiDanhsach.SoTien = tienGoi;
            goiDanhsach.MienGiam = mienGiam;
            goiDanhsach.GiamBhyt = giamTruBHYT;
            goiDanhsach.HieulucTungay = hieuLucTuNgay.Value;
            goiDanhsach.HieulucDenngay = hieuLucDenNgay.Value;
            goiDanhsach.TrangThai = TrangThai;
            goiDanhsach.Save();
            return goiDanhsach.IdGoi;
        }

        public int ThemChiTietGoiKham(int idGoiDvu, int serviceId, int serviceDetailId, string serviceDetailName, short soLanThucHien, short loaiDv, decimal donGia,bool chophep_denghi_mg,byte tyle_mg)
        {
            var goiChiTiet = new GoiChitiet();
            goiChiTiet.IdGoi = idGoiDvu;
            goiChiTiet.IdDichvu = serviceId;
            goiChiTiet.IdChitietdichvu = serviceDetailId;
            goiChiTiet.TenChitietdichvu = serviceDetailName;
            goiChiTiet.SoLuong = soLanThucHien;
            goiChiTiet.LoaiDvu = loaiDv;
            goiChiTiet.DonGia = donGia;
            goiChiTiet.ChophepDenghiMg = chophep_denghi_mg;
            goiChiTiet.TyleMg = tyle_mg;
            goiChiTiet.Save();
            return goiChiTiet.IdChitiet;
        }

        public void SuaChiTietGoiKham(int idChiTiet, short soLanThucHien)
        {
            var goiChiTiet = GoiChitiet.FetchByID(idChiTiet);
            goiChiTiet.SoLuong = soLanThucHien;

            goiChiTiet.Save();
        }

        //public DataTable LayDanhSachBN(bool theongay, DateTime fromDate, DateTime toDate, int objectType, int hosStatus,
        //    string patientName, int patientId, string patientCode, string patientPhone)
        //{
        //    var dt = SPs.GoiLayDanhSachTiepDonBN(
        //        theongay ? fromDate : Convert.ToDateTime("01/01/1900"),
        //        theongay ? toDate : THU_VIEN_CHUNG.GetSysDateTime(),
        //        objectType, hosStatus,
        //        patientName,
        //        patientId,
        //        patientCode, globalVariables.MA_KHOA_THIEN,
        //        patientPhone).GetDataSet().Tables[0];
        //    return dt;
        //}

        public DataTable LayGoiKhamTheoBN(long patientId, string soLo)
        {
            var dt = SPs.GoiLayGoiKhamTheoBN(patientId, soLo).GetDataSet().Tables[0];
            return dt;
        }
        public void ThemGoiKham_BVM(KcbChidinhcl objChidinh, int patientId, string ma_luotkham, int idGoiDVu,DateTime ngay_dangky)
        {
            GoiDanhsach _goi = GoiDanhsach.FetchByID(idGoiDVu);
            if (_goi != null)
            {
                GoiDangki _goidangky = new GoiDangki();

                _goidangky.IdGoi = idGoiDVu;
                _goidangky.IdBenhnhan = patientId;
                _goidangky.MaLuotkham = ma_luotkham;
                _goidangky.HieulucTungay = _goi.HieulucTungay;
                _goidangky.HieulucDenngay = _goi.HieulucDenngay;
                _goidangky.NgayTao = DateTime.Now;
                _goidangky.NguoiTao = globalVariables.UserName;
                _goidangky.SoTien = _goi.SoTien;
                _goidangky.MienGiam = _goi.MienGiam;
                _goidangky.GiamBhyt = _goi.GiamBhyt;
                _goidangky.Solo = "";
                _goidangky.NgayDangky = ngay_dangky;
                _goidangky.TthaiHuy = false;
                _goidangky.TthaiKichhoat = true;
                _goidangky.TthaiTtoan = false;
                _goidangky.IdKhoaChidinh = objChidinh.IdKhoaChidinh;
                _goidangky.IdKhoadieutri = objChidinh.IdKhoadieutri;
                _goidangky.IdPhongChidinh = objChidinh.IdPhongChidinh;
                _goidangky.IdKhoaThuchien = objChidinh.IdKhoaChidinh;
                _goidangky.IdThe = objChidinh.IdLichsuDoituongKcb;
                _goidangky.IdKham = objChidinh.IdKham;
                _goidangky.Noitru = objChidinh.Noitru;
                _goidangky.IdNvienTao = objChidinh.IdBacsiChidinh;

                _goidangky.Save();
                var id_dangky = _goidangky.IdDangky;
               int num= new Update(KcbChidinhclsChitiet.Schema)
                    .Set(KcbChidinhclsChitiet.Columns.IdDangky).EqualTo(id_dangky)
                    .Where(KcbChidinhclsChitiet.Columns.IdGoi).IsEqualTo(_goidangky.IdGoi)
                    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(objChidinh.IdChidinh)
                    .Execute();
                var dtDichVuTrongGoi =
                    new Select().From(GoiChitiet.Schema).Where(GoiChitiet.Columns.IdGoi).IsEqualTo(idGoiDVu).
                        ExecuteDataSet().Tables[0];
                foreach (DataRow row in dtDichVuTrongGoi.Rows)
                {
                    var serviceId = Utility.Int32Dbnull(row["id_dichvu"]);
                    var serviceDetailId = Utility.Int32Dbnull(row["id_chitietdichvu"]);
                    var serviceDetailName = Utility.sDbnull(row["ten_chitietdichvu"]);
                    var loaiDv = Utility.Int16Dbnull(row["loai_dvu"]);
                    var soLuong = Utility.Int16Dbnull(row["SO_LUONG"]);
                    decimal donGia = Utility.DecimaltoDbnull(row["DON_GIA"]);

                    var goiQuanHe = new GoiTinhtrangsudung();
                    goiQuanHe.IdDangky = id_dangky;
                    goiQuanHe.IdGoi = idGoiDVu;
                    goiQuanHe.IdDichvu = serviceId;
                    goiQuanHe.IdChitietdichvu = serviceDetailId;
                    goiQuanHe.TenChitietdichvu = serviceDetailName;
                    goiQuanHe.LoaiDvu = loaiDv;
                    goiQuanHe.SoLuong = soLuong;
                    goiQuanHe.DonGia = donGia;
                    goiQuanHe.TrangThai = 1;
                    goiQuanHe.SoluongDung = soLuong;
                    goiQuanHe.Save();
                }
            }
        }
        public int ThemGoiKhamChoBN(int patientId,string ma_luotkham, int idGoiDVu, decimal tienGoi, decimal mienGiam, decimal giamTruBhyt, DateTime hieuLucTu, DateTime hieuLucDen,DateTime ngay_dangky, string soLo)
        {
            var _goidangky = new GoiDangki();
            using (var Scope = new TransactionScope())
            {
                using (var dbScope = new SharedDbConnectionScope())
                {
                   
                    _goidangky.IdGoi = idGoiDVu;
                    _goidangky.IdBenhnhan = patientId;
                    _goidangky.MaLuotkham = ma_luotkham;
                    _goidangky.HieulucTungay = hieuLucTu;
                    _goidangky.HieulucDenngay = hieuLucDen;
                    _goidangky.NgayTao = DateTime.Now;
                    _goidangky.NguoiTao = globalVariables.UserName;
                    _goidangky.SoTien = tienGoi;
                    _goidangky.MienGiam = mienGiam;
                    _goidangky.GiamBhyt = giamTruBhyt;
                    _goidangky.Solo = soLo;
                    _goidangky.NgayDangky = ngay_dangky;
                    _goidangky.TthaiHuy = false;
                    _goidangky.Save();
                    var id_dangky = _goidangky.IdDangky;
                    var dtDichVuTrongGoi =
                        new Select().From(GoiChitiet.Schema).Where(GoiChitiet.Columns.IdGoi).IsEqualTo(idGoiDVu).
                            ExecuteDataSet().Tables[0];
                    foreach (DataRow row in dtDichVuTrongGoi.Rows)
                    {
                        var serviceId = Utility.Int32Dbnull(row["id_dichvu"]);
                        var serviceDetailId = Utility.Int32Dbnull(row["id_chitietdichvu"]);
                        var serviceDetailName = Utility.sDbnull(row["ten_chitietdichvu"]);
                        var loaiDv = Utility.Int16Dbnull(row["loai_dvu"]);
                        var soLuong = Utility.Int16Dbnull(row["SO_LUONG"]);
                        decimal donGia = Utility.DecimaltoDbnull(row["DON_GIA"]);

                        var goiQuanHe = new GoiTinhtrangsudung();
                        goiQuanHe.IdDangky = id_dangky;
                        goiQuanHe.IdGoi = idGoiDVu;
                        goiQuanHe.IdDichvu = serviceId;
                        goiQuanHe.IdChitietdichvu = serviceDetailId;
                        goiQuanHe.TenChitietdichvu = serviceDetailName;
                        goiQuanHe.LoaiDvu = loaiDv;
                        goiQuanHe.SoLuong = soLuong;
                        goiQuanHe.DonGia = donGia;
                        goiQuanHe.TrangThai = 1;
                        goiQuanHe.SoluongDung = soLuong;
                        goiQuanHe.Save();
                    }
                }
                Scope.Complete();
            }
            return _goidangky.IdDangky;
        }

        public void SuaGoiKhamChoBN(int idQuanLy, DateTime hieuLucDen)
        {
            var _goidangky = GoiDangki.FetchByID(idQuanLy);
            _goidangky.HieulucDenngay = hieuLucDen;
            _goidangky.Save();
        }

        public DataTable LayThongTinBNTheoPatientCode(string patientCode)
        {
            var dt = SPs.GoiLayThongTinBNTheoPatientCode(patientCode).GetDataSet().Tables[0];
            return dt;
        }

        public DataTable LayLichSuThanhToanGoi(int id_dangky)
        {
            var dt = SPs.GoiLayLichSuThanhToanGoi(id_dangky).GetDataSet().Tables[0];
            return dt;
        }
        /// <summary>
        /// Xóa chi tiết gói công khám
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <param name="lst_id_chitietdichvu"></param>
        /// <param name="loai_dichvu"></param>
        /// <param name="id_dangky"></param>
        /// <param name="id_goi"></param>
        public ActionResult XoachitietthanhtoanGoi_Congkham(KcbLuotkham objLuotkham, List<long> lstIdCongkham, Int16 loai_dichvu, long id_dangky, int id_goi)
        {
            try
            {
                //var option = new TransactionOptions();
                //option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                //option.Timeout = TimeSpan.FromMinutes(15);
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        foreach (long id_congkham in lstIdCongkham)
                        {
                            KcbDangkyKcb objCongkham = new Select().From<KcbDangkyKcb>()
                                .Where(KcbDangkyKcb.Columns.IdKham)
                                .IsEqualTo(id_congkham).ExecuteSingle<KcbDangkyKcb>();
                            if (objCongkham != null)
                            {
                                //Xóa trong 2 bảng TPayment và KcbThanhtoanChitiet
                                new Delete().From(KcbThanhtoanChitiet.Schema)
                                    .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(loai_dichvu)
                                    .And(KcbThanhtoanChitiet.Columns.IdThanhtoan).IsEqualTo(objCongkham.IdThanhtoan)
                                    .And(KcbThanhtoanChitiet.Columns.IdPhieuChitiet).IsEqualTo(objCongkham.IdKham)
                                    .And(KcbThanhtoanChitiet.Columns.IdPhieu).IsEqualTo(objCongkham.IdKham)
                                    .And(KcbThanhtoanChitiet.Columns.IdDichvu).IsEqualTo(objCongkham.IdDichvuKcb)
                                    .And(KcbThanhtoanChitiet.Columns.IdGoi).IsEqualTo(objCongkham.IdGoi)
                                   .And(KcbThanhtoanChitiet.Columns.TrongGoi).IsEqualTo(1)
                                   .Execute();
                                SqlQuery sqlQuery = new Select().From<KcbThanhtoanChitiet>()
                                .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                                .IsEqualTo(objCongkham.IdThanhtoan);
                                if (sqlQuery.GetRecordCount() <= 0)//Không còn bản ghi chi tiết nào-->Xóa luôn bản ghi thanh toán
                                {
                                    new Delete().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.Columns.IdThanhtoan).IsEqualTo(objCongkham.IdThanhtoan).Execute();
                                }
                                int num = 0;
                                num = new Delete().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdThanhtoan).Execute();
                            }
                        }
                    }
                    Scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// Xóa chi tiết gói
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <param name="lst_id_chitietdichvu"></param>
        /// <param name="loai_dichvu"></param>
        /// <param name="id_dangky"></param>
        /// <param name="id_goi"></param>
        public ActionResult GoikhamXoachitiet( List<long> lstIdChitietChidinh, byte loai_dichvu, int id_dangky, int id_goi)
        {
            try
            {
                //var option = new TransactionOptions();
                //option.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;
                //option.Timeout = TimeSpan.FromMinutes(15);
                //using (var Scope = new TransactionScope(TransactionScopeOption.Required, option))
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        foreach (long Id_Chitietchidinh in lstIdChitietChidinh)
                        {
                            SPs.GoikhamXoachitiet(Id_Chitietchidinh,id_goi,id_dangky,loai_dichvu).Execute();
                        }
                    }
                    Scope.Complete();
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Error;
            }
        }
        public ActionResult ThanhToanGoi(KcbLuotkham objLuotkham, List<int> lst_id_chitietdichvu, Int16 loai_dichvu, int id_dangky, int id_goi, byte noitrungoaitru,ref string ErrMsg)
        {
            ErrMsg = "";
            KcbThanhtoan objPayment = GetThanhtoan(objLuotkham, id_dangky, id_goi, noitrungoaitru);
         KCB_THANHTOAN  _THANHTOAN = new KCB_THANHTOAN();
            DataTable dtData =
                    _THANHTOAN.LayThongtinChuaThanhtoaGoikhamn(objLuotkham.MaLuotkham,(int) objLuotkham.IdBenhnhan, noitrungoaitru,
                       globalVariables.MA_KHOA_THIEN, objLuotkham.MaDoituongKcb, loai_dichvu.ToString(), id_dangky, id_goi);
            //Tạo bản ghi thanh toán chi tiết
            List<KcbThanhtoanChitiet> lstItems = Taodulieuthanhtoanchitiet(objLuotkham, dtData, lst_id_chitietdichvu, id_dangky, id_goi,noitrungoaitru, ref ErrMsg);
           if (Utility.DoTrim(ErrMsg).Length > 0)
           {
               Utility.ShowMsg(ErrMsg);
               return ActionResult.Error;
           }
           if (lstItems == null)
           {
               Utility.ShowMsg("Lỗi khi tạo dữ liệu thanh toán chi tiết. Liên hệ đơn vị cung cấp phần mềm để được hỗ trợ\n" + ErrMsg);
               return ActionResult.Error;
           }
            decimal ttbnChitrathucsu=0;
            long v_Payment_ID=-1;
            //Thực hiện tạo bản ghi thanh toán và chi tiết thanh toán
             ActionResult actionResult = _THANHTOAN.ThanhtoanChiphiDvuKcb_Goikham(objPayment, objLuotkham,
                        lstItems,new List<KcbChietkhau>(), ref v_Payment_ID, -1,false,false,"",
                        ref ttbnChitrathucsu, ref ErrMsg);
             switch (actionResult)
             {
                 case ActionResult.Success:
                     Utility.Log("cls_Goikham", globalVariables.UserName, string.Format("Tự động tạo bản ghi thanh toán tiền gói bệnh nhân ID={0}, PID={1}  thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                     break;
                 case ActionResult.Error:
                     Utility.ShowMsg("Lỗi trong quá trình thanh toán");
                     break;
                 case ActionResult.Cancel:
                     Utility.ShowMsg(ErrMsg);
                     break;
             }
             return actionResult;
        }
        private List<KcbThanhtoanChitiet> Taodulieuthanhtoanchitiet(KcbLuotkham objLuotkham, DataTable dtData, List<int> lst_id_chitietdichvu,int id_dangky,int id_goi,byte v_bytNoitru, ref string errMsg)
        {
            try
            {
                DataTable dtDataCheck = new DataTable();
                byte ErrType = 0;//0= xóa dịch vụ sau khi tnv chọn người bệnh-->có trong bảng tt chi tiết, ko có trong các bảng dịch vụ khám,thuốc/vtth,cls;1= đã bị người khác thanh toán;
                List<KcbThanhtoanChitiet> lstItems = new List<KcbThanhtoanChitiet>();
                foreach (DataRow row in dtData.Rows)
                {
                    if (lst_id_chitietdichvu.Contains(Utility.Int16Dbnull(row["Id_Chitietdichvu"], -1)))
                    {
                        KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                        newItem.IdThanhtoan = -1;
                        newItem.IdChitiet = -1;
                        newItem.TinhChiphi = 1;
                        if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                        if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                        //newItem.SoLuong = Utility.DecimaltoDbnull(row["sluong_sua"], 0);
                        //if (newItem.SoLuong <= 0) newItem.SoLuong = Utility.DecimaltoDbnull(row["so_luong"], 0);
                        newItem.SoLuong = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.SoLuong], 0);
                        //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                        newItem.BnhanChitra = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.BnhanChitra], 0);
                        newItem.BhytChitra = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.BhytChitra], 0);
                        newItem.DonGia = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.DonGia], 0);
                        newItem.GiaGoc = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.GiaGoc], 0);
                        newItem.TyleTt = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.TyleTt], 0);
                        newItem.PhuThu = Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.PhuThu], 0);
                        newItem.TinhChkhau = Utility.ByteDbnull(row[KcbThanhtoanChitiet.Columns.TinhChkhau], 0);
                        newItem.TuTuc = Utility.ByteDbnull(row[KcbThanhtoanChitiet.Columns.TuTuc], 0);
                        newItem.IdPhieu = Utility.Int32Dbnull(row["id_phieu"]);
                        newItem.IdKham = Utility.Int32Dbnull(row["Id_Kham"]);
                        newItem.IdPhieuChitiet = Utility.Int32Dbnull(row["Id_Phieu_Chitiet"], -1);
                        newItem.IdDichvu = Utility.Int16Dbnull(row["Id_dichvu"], -1);
                        newItem.IdChitietdichvu = Utility.Int16Dbnull(row["Id_Chitietdichvu"], -1);
                        newItem.TenChitietdichvu = Utility.sDbnull(row["Ten_Chitietdichvu"], "Không xác định").Trim();
                        newItem.TenBhyt = Utility.sDbnull(row["ten_bhyt"], "Không xác định").Trim();
                        newItem.DonviTinh = Utility.chuanhoachuoi(Utility.sDbnull(row["Ten_donvitinh"], "Lượt"));
                        newItem.SttIn = Utility.Int16Dbnull(row["stt_in"], 0);
                        newItem.IdKhoakcb = Utility.Int16Dbnull(row["id_khoakcb"], -1);
                        newItem.IdPhongkham = Utility.Int16Dbnull(row["id_phongkham"], -1);
                        newItem.IdBacsiChidinh = Utility.Int16Dbnull(row["id_bacsi"], -1);
                        newItem.IdLoaithanhtoan = Utility.ByteDbnull(row["Id_Loaithanhtoan"], -1);
                        newItem.IdLichsuDoituongKcb = Utility.Int64Dbnull(row[KcbThanhtoanChitiet.Columns.IdLichsuDoituongKcb], -1);
                        newItem.MatheBhyt = Utility.sDbnull(row[KcbThanhtoanChitiet.Columns.MatheBhyt], -1);
                        newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(Utility.Int32Dbnull(row["Id_Loaithanhtoan"], -1));
                        newItem.TienChietkhau = Math.Round(Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.TienChietkhau], 0m), 3);
                        newItem.TileChietkhau = Math.Round(Utility.DecimaltoDbnull(row[KcbThanhtoanChitiet.Columns.TileChietkhau], 0m), 3);
                        newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                        newItem.UserTao = Utility.sDbnull(row["User_tao"], "UKN").Trim();
                        newItem.KieuChietkhau = "%";
                        newItem.IdThanhtoanhuy = -1;
                        newItem.TrangthaiHuy = 0;
                        newItem.TrangthaiBhyt = 0;
                        newItem.TrangthaiChuyen = 0;
                        newItem.NoiTru = v_bytNoitru;
                        newItem.NguonGoc = (byte)0;
                        newItem.TrongGoi = 1;
                        newItem.IdGoi = id_goi;
                        newItem.IdDangky = id_dangky;
                        newItem.NgayTao = DateTime.Now;
                        newItem.NguoiTao = globalVariables.UserName;
                        lstItems.Add(newItem);
                        dtDataCheck = SPs.ThanhtoanKiemtratontaitruockhithanhtoan(newItem.IdPhieu, newItem.IdPhieuChitiet, newItem.IdLoaithanhtoan).GetDataSet().Tables[0];
                        if (dtDataCheck.Rows.Count <= 0)
                        {
                            ErrType = 0;
                            errMsg += newItem.TenChitietdichvu + "\n";
                            break;
                        }
                        else//Kiểm tra trạng thái thanh toán tránh việc thanh toán 2 lần(2 user cùng chọn và sau đó từng người bấm nút thanh toán)
                            if (dtDataCheck.Rows[0]["trangthai_thanhtoan"].ToString() == "1")
                            {
                                ErrType = 1;
                                errMsg += newItem.TenChitietdichvu + "\n";
                                break;
                            }
                    }
                }
                if (errMsg.Length > 0)
                    if (ErrType == 0)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã bị xóa/hủy bởi người khác. Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ không tồn tại dưới đây:\n" + errMsg;
                    else if (ErrType == 1)
                        errMsg = "Một số dịch vụ đang chọn thanh toán đã được thanh toán bởi TNV khác(trong lúc bạn chọn và chưa bấm thanh toán). Vui lòng chọn lại người bệnh để lấy lại dữ liệu mới nhất. Kiểm tra các dịch vụ đã được thanh toán dưới đây:\n" + errMsg;
                return lstItems;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
        KcbThanhtoan GetThanhtoan(KcbLuotkham objLuotkham, int id_dangky, int id_goi, byte noitrungoaitru)
        {
            bool taorieng = THU_VIEN_CHUNG.Laygiatrithamsohethong("GOI_THANHTOANAO_TAORIENG_MOILANCHIDINH", "1", true)=="1";
            KcbThanhtoan objPayment = new KcbThanhtoan();
            if (!taorieng)//Tìm thanh toán đã có để thêm chi tiết
            {
                objPayment = new Select().From(KcbThanhtoan.Schema)
                  .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .And(KcbThanhtoan.Columns.IdDangky).IsEqualTo(id_dangky)
                  .And(KcbThanhtoan.Columns.IdGoi).IsEqualTo(id_goi)
                  .And(KcbThanhtoan.Columns.TtoanAo).IsEqualTo(1)
                  .ExecuteSingle<KcbThanhtoan>();
            }
            if (objPayment == null) objPayment = new KcbThanhtoan();
            if (objPayment.IdThanhtoan <= 0)
            {
                objPayment.IdThanhtoan = -1;
                objPayment.IdDangky = id_dangky;
                objPayment.IdGoi = id_goi;
                objPayment.TtoanAo = 1;
                objPayment.MaLuotkham = objLuotkham.MaLuotkham;
                objPayment.IdBenhnhan = objLuotkham.IdBenhnhan;
                objPayment.NgayThanhtoan = DateTime.Now;
                objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                objPayment.KieuThanhtoan = 0;//0=Thanh toán thường;1= trả lại tiền;2= thanh toán bỏ viện
                objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objPayment.NoiTru = noitrungoaitru;
                objPayment.TrangthaiIn = 0;
                objPayment.NgayIn = null;
                objPayment.TtoanThuoc = false;//0= thanh toán các loại dịch vụ;1= thanh toán đơn thuốc tại quầy
                objPayment.NguoiIn = string.Empty;
                objPayment.MaPttt = "TM";
                objPayment.MaNganhang = "-1";
                objPayment.NgayTonghop = null;
                objPayment.NguoiTonghop = string.Empty;
                objPayment.NgayChot = null;
                objPayment.TrangthaiChot = 0;
                objPayment.TongTien = 0;
                objPayment.Ghichu = "Bản ghi thanh toán tự động khi đăng ký dịch vụ trong gói";
                //2 mục này được tính lại ở Business
                objPayment.BnhanChitra = 0;
                objPayment.BhytChitra = 0;
                objPayment.TileChietkhau = 0;
                objPayment.KieuChietkhau = "T";
                objPayment.TongtienChietkhau = 0;
                objPayment.TongtienChietkhauChitiet = 0;
                objPayment.TongtienChietkhauHoadon = 0;
                //Hóa đơn đỏ
                objPayment.MauHoadon = "";
                objPayment.KiHieu = "";
                objPayment.IdCapphat = -1;
                objPayment.MaQuyen = "";
                objPayment.Serie = "";

                objPayment.MaLydoChietkhau = "";
                objPayment.LydoChietkhau = "";
                objPayment.NgayTao = DateTime.Now;
                objPayment.NguoiTao = globalVariables.UserName;
                objPayment.IpMaytao = globalVariables.gv_strIPAddress;
                objPayment.TenMaytao = globalVariables.gv_strComputerName;
            }
            return objPayment;
        }
        public void ThanhToanGoi(KcbLuotkham  objLuotkham,int id_goi,  decimal so_tien, string MaPttt, string Ma_nganhang,
            byte hosStatus, string depositCode, DateTime depositDate, string lydotamung)
        {
            //SPs.SpKcbPhieuThuInsert(THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 3),
            //                -1, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham
            //                , DateTime.Now, globalVariables.UserName, "Thanh toán tiền gói cho bệnh nhân",
            //                so_tien, so_tien, "", 0
            //                , 0, 0, 1,
            //                "", "", Convert.ToByte(3), globalVariables.gv_intIDNhanvien, globalVariables.idKhoatheoMay
            //                , 0, "", globalVariables.UserName, DateTime.Now, "", DateTime.Now
            //                , MaPttt, "NB", Ma_nganhang, objLuotkham.IdKhoanoitru, objLuotkham.IdRavien, objLuotkham.IdBuong, objLuotkham.IdGiuong).Execute();//Bỏ do chưa có thông tin id gói

            KcbPhieuthu objPhieuthu = new KcbPhieuthu();
            objPhieuthu.IdBenhnhan = objLuotkham.IdBenhnhan;
            objPhieuthu.MaLuotkham = objLuotkham.MaLuotkham;
            objPhieuthu.SoTien = so_tien;
            objPhieuthu.SotienGoc = so_tien;
            objPhieuthu.IdGoi = id_goi;
            objPhieuthu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(DateTime.Now, 3);
            objPhieuthu.IdThanhtoan = -1;
            objPhieuthu.NgayThuchien = DateTime.Now;
            objPhieuthu.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objPhieuthu.NguoiNop = globalVariables.gv_strTenNhanvien;
            objPhieuthu.LydoNop = "Thanh toán tiền gói cho bệnh nhân";
            objPhieuthu.MaLydoChietkhau = "";
            objPhieuthu.TienChietkhau = 0;
            objPhieuthu.TienChietkhauchitiet = 0;
            objPhieuthu.TienChietkhauhoadon = 0;
            objPhieuthu.LoaiPhieuthu = 3;
            objPhieuthu.MaPttt = MaPttt;
            objPhieuthu.MaNganhang = Ma_nganhang;
            objPhieuthu.IdKhoanoitru = objLuotkham.IdKhoanoitru;
            objPhieuthu.IdBuonggiuong = objLuotkham.IdRavien;
            objPhieuthu.IdBuong = objLuotkham.IdBuong;
            objPhieuthu.IdGiuong = objLuotkham.IdGiuong;
            objPhieuthu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
            objPhieuthu.NoiTru = 0;
            objPhieuthu.NguoiTao = globalVariables.UserName;
            objPhieuthu.NgayTao = DateTime.Now;
            objPhieuthu.NoiDung = objPhieuthu.LydoNop;
            objPhieuthu.Save();
        }
        public void HuyThanhToanGoi(KcbPhieuthu objPhieuthu, string lydoxoa)
        {
            try
            {
                KcbPhieuthu.Delete(objPhieuthu.IdPhieuthu);
                //int record = new Update(KcbPhieuthu.Schema)
                //.Set(KcbPhieuthu.Columns.DepositType).EqualTo(KyQuiHoanQui.HuyKyQui)
                //.Set(KcbPhieuthu.Columns.ModifiedBy).EqualTo(globalVariables.UserName)
                //.Set(KcbPhieuthu.Columns.ModifiedDate).EqualTo(THU_VIEN_CHUNG.GetSysDateTime())
                //.Set(KcbPhieuthu.Columns.LyDoHuy).EqualTo(lydoxoa)
                //.Where(KcbPhieuthu.Columns.IdPhieuthu).IsEqualTo(objDeposit.idph).Execute();
                //if (record > 0)
                //{
                //    LogTamUngHoanUng objLogTamUngHoanUng = new LogTamUngHoanUng();

                //    objLogTamUngHoanUng.IdGoiDvu = Utility.Int32Dbnull(objDeposit.IdGoiDvu);
                //    objLogTamUngHoanUng.PatientCode = Utility.sDbnull(objDeposit.PatientCode);
                //    objLogTamUngHoanUng.PatientId = Utility.Int32Dbnull(objDeposit.PatientId);
                //    objLogTamUngHoanUng.DepositId = Utility.Int32Dbnull(objDeposit.DepositId);
                //    objLogTamUngHoanUng.DepositCode = Utility.sDbnull(objDeposit.DepositCode);
                //    objLogTamUngHoanUng.DepositDate = objDeposit.DepositDate;
                //    objLogTamUngHoanUng.DepositType = objDeposit.DepositType;
                //    objLogTamUngHoanUng.CreatedBy = Utility.sDbnull(objDeposit.CreatedBy);
                //    objLogTamUngHoanUng.CreatedDate = objDeposit.CreatedDate;
                //    objLogTamUngHoanUng.ModifiedBy = Utility.sDbnull(objDeposit.ModifiedBy);
                //    objLogTamUngHoanUng.ModifiedDate = objDeposit.ModifiedDate;
                //    objLogTamUngHoanUng.ToTalMoney = Utility.DecimaltoDbnull(objDeposit.ToTalMoney);
                //    objLogTamUngHoanUng.DepartmentId = Utility.Int16Dbnull(objDeposit.DepartmentId);
                //    objLogTamUngHoanUng.SDesc = objDeposit.SDesc;
                //    objLogTamUngHoanUng.Status = Utility.ByteDbnull(objDeposit.Status);
                //    objLogTamUngHoanUng.MaCoSo = Utility.sDbnull(objDeposit.MaCoSo);
                //    objLogTamUngHoanUng.NguoiXoa = globalVariables.UserName;
                //    objLogTamUngHoanUng.IpXoa = globalVariables.IpAddress;
                //    objLogTamUngHoanUng.NgayXoa = BusinessHelper.GetSysDateTime();
                //    objLogTamUngHoanUng.LyDoXoa = lydoxoa;
                //    objLogTamUngHoanUng.IdHinhThuc = Utility.Int16Dbnull(objDeposit.IdHinhThuc);
                //    objLogTamUngHoanUng.Transactionid = Utility.Int32Dbnull(objDeposit.Transactionid);
                //    objLogTamUngHoanUng.TrucTuyen = Utility.Int32Dbnull(objDeposit.TrucTuyen) == 1;
                //    objLogTamUngHoanUng.Save();

                //}
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh hủy gói :{0}", exception);
                Utility.Log(this.GetType().Name, globalVariables.UserName, string.Format("lỗi trong quá trình :{0} hủy gói tương ứng patient_code :{1} tương ứng với", exception.ToString(), objPhieuthu.MaLuotkham), newaction.Error, "Class");

            }
        }


        public void CapNhatTrangThaiThanhToan(int id_dangky,bool tthaittoan)
        {
            var _goidangky = GoiDangki.FetchByID(id_dangky);
            _goidangky.TthaiTtoan = tthaittoan;
            _goidangky.Save();
        }
       
        public void KichHoatGoiChoBN(int id_dangky,bool status)
        {
            var _goidangky = GoiDangki.FetchByID(id_dangky);
            if (status)
                _goidangky.NgayKichhoat = DateTime.Now;
            else
                _goidangky.NgayKichhoat = null;
            _goidangky.TthaiKichhoat = status;
            _goidangky.Save();
            
        }
        public void KetThucGoiChoBN(int id_dangky,bool status)
        {
            var _goidangky = GoiDangki.FetchByID(id_dangky);
            _goidangky.TthaiKetthuc = status;
            if (status)
                _goidangky.NgayKetthuc = DateTime.Now;
            else
                _goidangky.NgayKetthuc = null;
            _goidangky.Save();
        }
        //public DataTable LayDanhSachKSKTheoLo(string soLo)
        //{
        //    var dt = SPs.KskGetPatientListByBatch(soLo, -1).GetDataSet().Tables[0];
        //    return dt;
        //}

        public decimal LayTongTienTheoLo(string soLo)
        {
            var tongTien =
                new Select("SUM(so_tien - MIEN_GIAM - giam_bhyt)").From(GoiDangki.Schema).InnerJoin(GoiDanhsach.IdGoiColumn,
                                                                     GoiDangki.IdGoiColumn).Where(
                                                                         GoiDangki.Columns.Solo).IsEqualTo(soLo).
                    ExecuteScalar();
            return Utility.DecimaltoDbnull(tongTien);
        }

        public bool LayXacNhanThanhToanTheoLo(string soLo)
        {
            var daThanhToanHet =
                new Select(GoiDangki.Columns.IdDangky).From(GoiDangki.Schema).Where(GoiDangki.Columns.Solo).IsEqualTo(soLo)
                    .And(GoiDangki.Columns.TthaiTtoan).IsNotEqualTo(true).GetRecordCount() > 0;
            return daThanhToanHet;
        }

        public bool LayXacNhanKichHoatTheoLo(string soLo)
        {
            var daKichHoatHet =
                new Select(GoiDangki.Columns.IdDangky).From(GoiDangki.Schema).Where(GoiDangki.Columns.Solo).IsEqualTo(soLo)
                    .And(GoiDangki.Columns.TthaiKichhoat).IsNotEqualTo(1).GetRecordCount() > 0;
            return daKichHoatHet;
        }


        public void XoaGoiKhamCuaBN(int IdDangky, string LydoHuy)
        {

            // new Delete().From(_goidangky.Schema).Where(_goidangky.Columns.id_dangky).IsEqualTo(id_dangky).Execute();
            new Update(GoiDangki.Schema)
                .Set(GoiDangki.Columns.TthaiKichhoat).EqualTo(false)
                .Set(GoiDangki.Columns.TthaiHuy).EqualTo(true)
                .Set(GoiDangki.Columns.NguoiHuy).EqualTo(globalVariables.UserName)
                .Set(GoiDangki.Columns.NgayHuy).EqualTo(THU_VIEN_CHUNG.GetSysDateTime())
                .Set(GoiDangki.Columns.LydoHuy).EqualTo(LydoHuy)
                .Where(GoiDangki.Columns.IdDangky).IsEqualTo(IdDangky).Execute();
        }

        public DataTable LayPhieuQuanLyCrpt(long patientId, string patientCode, int id_dangky)
        {
            var dt = SPs.GoiIntinhhinhsudunggoi(patientId, patientCode, id_dangky).GetDataSet().Tables[0];
            return dt;
        }

        public void ThemGhiChu(int id_dangky, string ghiChu)
        {
            var goiQly = GoiDangki.FetchByID(id_dangky);
            goiQly.GhiChu = ghiChu;
            goiQly.Save();
        }
    }
}
