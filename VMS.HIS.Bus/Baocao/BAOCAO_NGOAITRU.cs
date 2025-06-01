using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;

namespace VNS.HIS.BusRule.Classes
{
    public class BAOCAO_NGOAITRU
    {
        private NLog.Logger log;
        public BAOCAO_NGOAITRU()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static DataTable BaocaoTiepdonbenhnhanTonghop(DateTime? FromDate, DateTime? ToDate, int? iddoituongkcb, string nguoitao, string DeparmentCODE,string loaiBN)
        {
            return SPs.BaocaoTiepdonbenhnhanTonghop(FromDate, ToDate, iddoituongkcb, nguoitao, DeparmentCODE, loaiBN).GetDataSet().Tables[0];
        }
        public static DataTable BaoCaoThongKeBNTheoDotuoiCT(int? iddoituongkcb, DateTime? FromDate, DateTime? ToDate, int gioitinh , string DeparmentCODE)
        {
            return SPs.BaocaoThongKeBNTheoDoTuoiCt(iddoituongkcb, FromDate, ToDate, gioitinh, DeparmentCODE).GetDataSet().Tables[0];
        }
        public static DataTable BaoCaoThongkeSoluongBenhNhanTheoBacsy(DateTime? FromDate, DateTime? ToDate, int? iddoituongkcb, int? idBsThuchien, int? idKhoaPhong, string DeparmentCODE)
        {
            return SPs.BaocaoSoluongbenhnhanTheobacsy(FromDate, ToDate,iddoituongkcb,idKhoaPhong,idBsThuchien, DeparmentCODE).GetDataSet().Tables[0];
        }
        
        public static DataTable BaocaoTiepdonbenhnhanChitiet(int? ObjectType, DateTime? FromDate, DateTime? ToDate, string nguoitao, string DeparmentCODE,string loaiBN,int?IdPhongKham)
        {
            return SPs.BaocaoTiepdonbenhnhanChitiet(ObjectType, FromDate, ToDate, nguoitao, DeparmentCODE, loaiBN,IdPhongKham).GetDataSet().Tables[0];
            
        }
        public static DataTable BaocaoMiengiam(int idnhanvienthanhtoan, string tungay, string denngay)
        {
            return SPs.BaocaoMiengiam(idnhanvienthanhtoan, tungay, denngay).GetDataSet().Tables[0];

        }
        public static DataTable BaocaoTamungHoanung(int IdTNV, string tungay, string denngay, short IdKhoanoitru, short IdDoituongKcb, byte kieutamung)
        {
            return SPs.BaocaoTamungHoanung(IdTNV, tungay, denngay, IdKhoanoitru, IdDoituongKcb, kieutamung).GetDataSet().Tables[0];

        }
        public static DataTable BaocaoHuytien(int IdTNV, string tungay, string denngay, byte loaiphieuhuy)
        {
            return SPs.BaocaoHuytien(IdTNV, tungay, denngay, loaiphieuhuy).GetDataSet().Tables[0];

        }
        public static DataTable BaocaoTonghopchiphiBenhvien(int IdTnv, string tungay, string denngay)
        {
            return SPs.BaocaoTonghopchiphiBenhvien(IdTnv, tungay, denngay).GetDataSet().Tables[0];

        }
        public static DataTable BaocaoThutientheokhoaChitiet(string maphongthien, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string nhomdichvu, string CreateBy, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutientheokhoaChitiet(maphongthien, FromDate, ToDate, MaDoiTuong, nhomdichvu, CreateBy, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThutientheokhoaTonghop(string maphongthien, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string nhomdichvu, string CreateBy, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutientheokhoaTonghop(maphongthien, FromDate, ToDate, MaDoiTuong,  CreateBy,nhomdichvu, MAKHOATHIEN).GetDataSet().Tables[0];
        }


        public static DataTable BaocaoChidinhclsChitiet(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string nhomdichvu, string CreateBy, string MAKHOATHIEN, int? NoExam, string KieuBenhNhan, string BacSyChiDinh, int? IdChitietDichVu)
        {
            return SPs.BaocaoChidinhclsChitiet(FromDate, ToDate, MaDoiTuong, nhomdichvu, CreateBy, MAKHOATHIEN, NoExam, KieuBenhNhan,BacSyChiDinh,IdChitietDichVu).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoChidinhclsTonghop(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string nhomdichvu, string CreateBy, string MAKHOATHIEN, int? NoExam, string KieuBenhNhan)
        {
            return SPs.BaocaoChidinhclsTonghop(FromDate, ToDate, MaDoiTuong, CreateBy, nhomdichvu, MAKHOATHIEN, NoExam, KieuBenhNhan).GetDataSet().Tables[0];
        }



        public static DataTable BaocaoThutienkhamTonghop(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV,short idLoaithanhtoan, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutienkhamTonghop(FromDate, ToDate, maDoituongKCB, maTNV,idLoaithanhtoan, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoDoanhthuphongkham(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, byte noitru, string MAKHOATHIEN)
        {
            return SPs.BaocaoDoanhthuphongkham(FromDate, ToDate, maDoituongKCB, maTNV, noitru, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoDoanhthuphongkhamTonghop(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, byte noitru, string MAKHOATHIEN)
        {
            return SPs.BaocaoDoanhthuphongkhamTonghop(FromDate, ToDate, maDoituongKCB, maTNV, noitru, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoDoanhthuphongkhamBvsg(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, byte noitru, string MAKHOATHIEN, string mahttt, string manganhang, int idLoaidvu, int idDvu, byte loaibaocao)
        {
            return SPs.BaocaoDoanhthuphongkhamBvsg(FromDate, ToDate, maDoituongKCB, maTNV, noitru, MAKHOATHIEN, mahttt, manganhang, idLoaidvu, idDvu, loaibaocao).GetDataSet().Tables[0];
        }
        /// <summary>
        /// Báo cáo doanh thu toàn BV
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="maDoituongKCB"></param>
        /// <param name="maTNV"></param>
        /// <param name="noitru"></param>
        /// <param name="MAKHOATHIEN"></param>
        /// <param name="mahttt"></param>
        /// <param name="manganhang"></param>
        /// <param name="loaibaocao"></param>
        /// <returns></returns>
        public static DataTable BaocaoDoanhthutoanvienBvsg(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, byte noitru, string MAKHOATHIEN, string mahttt, string manganhang, byte loaibaocao, string lstIdloaithanhtoan,byte laycatienquaythuoc,string manguongt,string madoitac)
        {
            return SPs.BaocaoDoanhthutoanvienBvsg(FromDate, ToDate, maDoituongKCB, maTNV, noitru, MAKHOATHIEN, mahttt, manganhang, loaibaocao, lstIdloaithanhtoan, laycatienquaythuoc, manguongt,madoitac).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoSochitietbanhang_bvsg(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string ma_luotkham, long id_benhnhan, string ten_benhnhan, string maTNV, byte noitru, string MAKHOATHIEN, string mahttt, string manganhang, int id_loaithanhtoan, string lstdichvu, byte loaibaocao)
        {
            return SPs.BaocaoSochitietbanhang(FromDate, ToDate, maDoituongKCB, ma_luotkham, id_benhnhan, ten_benhnhan, maTNV, noitru, MAKHOATHIEN, mahttt, manganhang, id_loaithanhtoan, lstdichvu, loaibaocao).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoDoanhthuphongkhamTonghopEbm(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, byte noitru, string MAKHOATHIEN)
        {
            return SPs.BaocaoDoanhthuphongkhamTonghopEbm(FromDate, ToDate, maDoituongKCB, maTNV, noitru, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThutienkhamChitiet(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy,short idLoaithanhtoan, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutienkhamChitiet(FromDate, ToDate, MaDoiTuong, CreateBy,idLoaithanhtoan, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoChenhlech(string FromDate, string ToDate, string MaDoiTuong, string CreateBy, short idLoaithanhtoan, string MAKHOATHIEN, byte loaibc)
        {
            return SPs.BaocaoChenhlech(FromDate, ToDate, MaDoiTuong, CreateBy, idLoaithanhtoan, MAKHOATHIEN, loaibc).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoNguon(string FromDate, string ToDate, string MaDoiTuong, string Noigioithieu,string ma_doitac, string MAKHOATHIEN, byte loaibc)
        {
            return SPs.BaocaoNguon(FromDate, ToDate, MaDoiTuong ,ma_doitac,Noigioithieu, MAKHOATHIEN, loaibc).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoNguonEbm(string FromDate, string ToDate, string MaDoiTuong, string Noigioithieu, string MAKHOATHIEN, byte loaibc)
        {
            return SPs.BaocaoNguonEbm(FromDate, ToDate, MaDoiTuong, Noigioithieu, MAKHOATHIEN, loaibc).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoLuu(string FromDate, string ToDate, int IdloaiDvu,int IDDvu,string BSthuchien,byte trang_thai)
        {
            return SPs.BaocaoLuu(FromDate, ToDate, IdloaiDvu, IDDvu, BSthuchien, trang_thai).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoChietkhauCong(string FromDate, string ToDate, int IdloaiDvu, int IDDvu, string BSthuchien)
        {
            return SPs.BaocaoChietkhauCong(FromDate, ToDate, IdloaiDvu, IDDvu, BSthuchien).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThuchiendichvu(string FromDate, string ToDate, int IdloaiDvu, int IDDvu, string BSthuchien,int reportype)
        {
            return SPs.BaocaoThuchiendichvu(FromDate, ToDate, IdloaiDvu, IDDvu, BSthuchien, reportype).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoDanhsachhoadonThuphi(DateTime? TuNgay, DateTime? DenNgay, int? LoaiTimKiem, string DoiTuong, string NguoiThu, int? NTNT, int? cohoadon, string maquyen, int? fromserie, int? toserie, string KhoaThucHien)
        {
            return SPs.BaocaoDanhsachhoadonThuphi(TuNgay, DenNgay, LoaiTimKiem, DoiTuong, NguoiThu, NTNT, cohoadon,maquyen,fromserie,toserie, KhoaThucHien).GetDataSet().Tables[0];
        }

        public static DataTable BaocaoThuvienphiTonghop(string makhoaKCB, DateTime? FromDate, DateTime? ToDate, string NGUOITHU, string DOITUONG, int? NTNT, int? Cohoadon, int? TTCHOT)
        {
            return SPs.BaocaoThuvienphiTonghop(makhoaKCB, FromDate, ToDate, NGUOITHU, DOITUONG, NTNT, Cohoadon, TTCHOT).GetDataSet().Tables[0];
        }

        public static DataTable BaocaoThuvienphiChitiet(string makhoaKCB, DateTime? FromDate, DateTime? ToDate, string NGUOITHU, string DOITUONG, int? NTNT, int? Cohoadon, int? TTCHOT)
        {
            return SPs.BaocaoThuvienphiChitiet(makhoaKCB, FromDate, ToDate, NGUOITHU, DOITUONG, NTNT, Cohoadon, TTCHOT).GetDataSet().Tables[0];
        }
        public static DataTable BaoCaoThongkeKhamChuaBenh(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int TrangThai, int GioiTinh, int NhanVien, string sThamso)
        {
            return
                SPs.BaocaoThongkeKhamchuabenh(FromDate, ToDate, DoiTuong, GioiTinh, NhanVien, TrangThai,sThamso).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaocaoTonghopchiphiKCB(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int TrangThai, int GioiTinh, string NhanVien,int id_bacsicd, string sThamso)
        {
            return
                SPs.BaocaoTonghopchiphiKCB(FromDate, ToDate, DoiTuong, GioiTinh, NhanVien,id_bacsicd, TrangThai, sThamso).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeChuyenVienDi(DateTime? FromDate, DateTime? ToDate, string mabenh, int idbacsy,  int DoiTuong, int TrangThai, int NoiChuyenDi)
        {
            return
                SPs.BaocaoThongkeChuyenviendi(FromDate, ToDate, NoiChuyenDi,mabenh,idbacsy, TrangThai, DoiTuong).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeChuyenVienDen(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int TrangThai, int NoiChuyenDen)
        {
            return
                SPs.BaocaoThongkeChuyenvienden(FromDate, ToDate, NoiChuyenDen, TrangThai, DoiTuong).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeNhapvienChitiet(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int khoanoitru)
        {
            return
                SPs.BaocaoThongkeNhapvienChitiet(FromDate, ToDate, DoiTuong,khoanoitru).GetDataSet().
                    Tables[0];
        }
        
        public static DataTable BaocaoTinhinhNhapvien(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int khoanoitru, byte loai_baocao,byte trang_thai)
        {
            return
                SPs.BaocaoTinhinhNhapvien(FromDate, ToDate, DoiTuong, khoanoitru, loai_baocao, trang_thai).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeNhapvienTonghop(DateTime? FromDate, DateTime? ToDate, int DoiTuong, int khoanoitru)
        {
            return
                SPs.BaocaoThongkeNhapvienTonghop(FromDate, ToDate, DoiTuong, khoanoitru).GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeTheoMaBenhICD10ChiTiet(DateTime? FromDate, DateTime? ToDate, string DoiTuong, string KhoaThucHien, string ListICD)
        {
            return
                SPs.BaocaoThongkeTheomabenhIcdChitiet(FromDate, ToDate,ListICD, DoiTuong, KhoaThucHien,"").GetDataSet().
                    Tables[0];
        }
        public static DataTable BaoCaoThongkeTheoMaBenhICD10TongHop(DateTime? FromDate, DateTime? ToDate, string DoiTuong, string KhoaThucHien, string ListICD)
        {
            return null;
                //SPs.BaocaoThongkeTheomabenhIcdTonghop(FromDate, ToDate, ListICD, DoiTuong, KhoaThucHien, "").GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThutientiemchungtonghop(DateTime? FromDate, DateTime? ToDate, string maDoituongKCB, string maTNV, short idLoaithanhtoan, string kieukham, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutientiemchungTonghop(FromDate, ToDate, maDoituongKCB, maTNV, kieukham,idLoaithanhtoan, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThutientiemchungchitiet(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy, string idLoaithanhtoan, string kieukham,  string MAKHOATHIEN, string kieutimkiem)
        {
            return SPs.BaocaoThutientiemchungChitiet(FromDate, ToDate, MaDoiTuong, CreateBy, kieukham, idLoaithanhtoan, kieutimkiem, MAKHOATHIEN ).GetDataSet().Tables[0];
        }
    }
}
