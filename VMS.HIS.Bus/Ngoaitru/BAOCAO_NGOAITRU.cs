using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
        public static DataTable baocaotinhinhtiepdonbenhnhanchitiet(int? ObjectType, DateTime? FromDate, DateTime? ToDate, string DeparmentCODE)
        {
            return SPs.BaocaoTinhinhTiepdonbenhnhanChitiet(ObjectType, FromDate, ToDate, DeparmentCODE).GetDataSet().Tables[0];
        }
        public static DataTable baocaotinhinhtiepdonbenhnhan(DateTime? FromDate, DateTime? ToDate, int? ObjectTypeID, string DeparmentCODE)
        {
            return SPs.BaocaoTinhinhTiepdonbenhnhan(FromDate, ToDate, ObjectTypeID, DeparmentCODE).GetDataSet().Tables[0];
        }
        public static DataTable baocaodanhsachhoadonthuphi(DateTime? TuNgay, DateTime? DenNgay, int? LoaiTimKiem, string DoiTuong,
            string NguoiThu, int? NTNT, int? LoaiHoaDon, string KhoaThucHien)
        {
            return SPs.BaocaoDanhsachHoadonthuphi(TuNgay, DenNgay, LoaiTimKiem, DoiTuong, NguoiThu, NTNT, LoaiHoaDon, KhoaThucHien).GetDataSet().Tables[0];
        }
        public static DataTable baocaotonghopthutientheokhoa(int? ServiceTypeId, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong,
            string CreateBy, string MAKHOATHIEN)
        {
            return SPs.BaocaoTonghopThutientheokhoa(ServiceTypeId, FromDate, ToDate, MaDoiTuong, CreateBy, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable baocaothutientheokhoa(string DepartmentssDesc, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong,
            string CreateBy, string MAKHOATHIEN, int? NTNT, int? TrongGioNgoaiGio)
        {
            return SPs.BaocaoThutientheokhoa(DepartmentssDesc, FromDate, ToDate, MaDoiTuong, CreateBy, MAKHOATHIEN,
                                                 NTNT, TrongGioNgoaiGio).GetDataSet().Tables[0];
        }
        public static DataTable baocaotonghopvienphitheonguoithuphi(DateTime? FromDate, DateTime? ToDate, string NGUOITHU, string DOITUONG, int? NTNT, int? TTCHOT)
        {
            return SPs.BaocaoTonghopvienphiTheonguoithuphi(FromDate, ToDate, NGUOITHU, DOITUONG, NTNT, TTCHOT).GetDataSet().Tables[0];
        }
        public static DataTable baocaochitiettheonguoithuphi(DateTime? FromDate, DateTime? ToDate, string NGUOITHU, string DOITUONG, int? NTNT, int? TTCHOT)
        {
            return SPs.BaocaoChitietTheonguoithuphi(FromDate, ToDate, NGUOITHU, DOITUONG, NTNT, TTCHOT).GetDataSet().Tables[0];
        }
        public static DataTable baocaotonghopDVKTmauDM(DateTime? fromDate, DateTime? toDate, string ObjectTypeCode, int? NTNT,
            int? Nhom, int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI)
        {
            return SPs.BaocaoTonghopDvktMauDM(fromDate,toDate,ObjectTypeCode,NTNT,Nhom,
                                             Tuyen,InsClinicCode,InsObjectCodeTP, MaDKKCB,
                                             TRANGTHAI).GetDataSet().Tables[0];
        }
         public static DataTable baocaotonghopDVKTmau1(DateTime? fromDate, DateTime? toDate, string ObjectTypeCode, int? NTNT, int? Nhom, 
             int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI, int? IDGoiDVu, string IsGoiDvu)
        {
             return SPs.BaocaoTonghopDvktMau1(fromDate,toDate,ObjectTypeCode,NTNT,Nhom,Tuyen,InsClinicCode,InsObjectCodeTP,
                 MaDKKCB,TRANGTHAI, IDGoiDVu, IsGoiDvu).GetDataSet().
                    Tables[0];
         }
         public static DataTable baocaotonghopchiphiKCBtheonguoinhap(string fromDate, string ToDate, int? ObjectTypeID)
        {
             return SPs.BaocaoTonghopChiphiKCBTheonguoinhap(fromDate,ToDate, ObjectTypeID).GetDataSet().Tables[0];
         }
         public static DataTable baocaodoanhthuPKEXCEL(DateTime? FromDate, DateTime? ToDate, int? ObjectTypeID)
        {
             return SPs.BaocaoDoanhthuPKExcel(FromDate,ToDate,ObjectTypeID).GetDataSet().Tables[0];
         }
       
         public static DataTable BaocaodoanhthuPK(DateTime? fromDate, DateTime? toDate, string ObjectTypeCode, int? NTNT, string CreateBy, 
             int? DepartmentId, int? ServiceId, int? BHYT, int? IDGoiDVu, string IsGoiDvu, int? DaChuyenTamUng)
        {
             return SPs.BaocaoDoanhthuphongkham(fromDate,toDate,ObjectTypeCode,NTNT,CreateBy,DepartmentId,ServiceId,BHYT,IDGoiDVu,IsGoiDvu,DaChuyenTamUng).GetDataSet().Tables[0];
         }
        public static DataTable BaocaoTonghopTaiKkbDoituongThuphi(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string Createby, string MAKHOATHIEN)
        {
            return  SPs.BaocaoTonghopTaiKkbDoituongThuphi(
                    FromDate,ToDate,MaDoiTuong,Createby, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoTonghop(DateTime? FromDate, DateTime? ToDate, string ObjectTypeCode, string CreateBy, string MAKHOATHIEN, int? DungTuyen)
        {
            return  SPs.BaocaoTonghop(FromDate,ToDate,ObjectTypeCode,CreateBy, MAKHOATHIEN, DungTuyen).GetDataSet().Tables[0];
        }
          public static DataTable BaocaoThutienTheokhoaTonghop(string DepartmentssDesc, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy, string MAKHOATHIEN)
        {
              return SPs.BaocaoThutientheokhoaTonghop(DepartmentssDesc,FromDate,ToDate,MaDoiTuong, CreateBy,MAKHOATHIEN).GetDataSet().Tables[0];
          }
        public static DataTable BaocaoThutienTheokhoachitiet(string DepartmentssDesc, DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy, string MAKHOATHIEN)
        {
            return  SPs.BaocaoThutientheokhoaChitiet(DepartmentssDesc,FromDate,ToDate,MaDoiTuong,CreateBy,MAKHOATHIEN).GetDataSet().Tables[0];
        }
         public static DataTable baocaothutienKCBtheodoituong(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy, int? PaymentTypeID, string MAKHOATHIEN)
        {
             return SPs.BaocaoThutienKCBTheodoituong(
                    FromDate,ToDate,MaDoiTuong,CreateBy, PaymentTypeID, MAKHOATHIEN).GetDataSet().Tables[0];
         }
         public static DataTable baocaothutienDvuCLStonghop(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, 
             string Createby, string MAKHOATHIEN)
        {
             return SPs.BaocaoThutienDvuCLSTonghop(
                    FromDate,
                    ToDate,MaDoiTuong, Createby,MAKHOATHIEN).GetDataSet().Tables[0];
         }
        public static DataTable baocaothutienKCBvaDichvuCLSdoituongDichvu(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string CreateBy, int? PaymentTypeID, string MAKHOATHIEN)
        {
            return SPs.BaocaoThutienKCBvaDichvuCLSDoituongDichvu(
                     FromDate,ToDate,MaDoiTuong, CreateBy, PaymentTypeID,MAKHOATHIEN).GetDataSet().Tables[0];
        }
         public static DataTable BaocaoThutienBnChuaKetthuc(DateTime? FromDate, DateTime? ToDate, string ObjectType_Code, 
             string CreateBy, int? IsKetThuc, int? IsNoiTru, string MaKhoaThucHien)
        {
             return SPs.BaocaoThutienBnChuaKetthuc(
                    FromDate, ToDate,ObjectType_Code, CreateBy
                    ,IsKetThuc,IsNoiTru,
                    MaKhoaThucHien).GetDataSet().Tables[0];
         }
        public static DataTable BaocaoThuphiTheoBacsy(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, string Createby, int? IDBACSY, int? PaymentTypeID, int? IDLOAIDVU, string MAKHOATHIEN)
        
        {
            return SPs.BaocaoThuphiTheoBacsy(FromDate,ToDate,MaDoiTuong, Createby,IDBACSY, PaymentTypeID,IDLOAIDVU,MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoThuTienkhamThuphiTonghop(DateTime? FromDate, DateTime? ToDate, string MaDoiTuong, 
            string Createby, string MAKHOATHIEN)
        {
            return SPs.BaocaoThuTienkhamThuphiTonghop(
                    FromDate, ToDate, MaDoiTuong, Createby, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BAOCAO_THEO_DOI_THUNGAN_CHIDINH(DateTime? FromDate, DateTime? ToDate, string CreateBy, string MaDoiTuong, int? idbacsy, string MAKHOATHIEN)
        {
            return SPs.BaocaoTheoDoiThunganChidinh(
                    FromDate, ToDate, CreateBy, MaDoiTuong,
                    idbacsy, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoTheoDoiThungan(DateTime? FromDate, DateTime? ToDate, string CreateBy, string MaDoiTuong, string MAKHOATHIEN)
        {
            return SPs.BaocaoTheoDoiThungan(FromDate, ToDate, CreateBy, MaDoiTuong, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable BaocaoTheoDoiBacsyChidinh(DateTime? FromDate, DateTime? ToDate, string CreateBy, string MaDoiTuong, int? idbacsy, string MAKHOATHIEN)
        {
            return SPs.BaocaoTheoDoiBacsyChidinh(FromDate,
                    ToDate, CreateBy, MaDoiTuong, idbacsy, MAKHOATHIEN).GetDataSet().Tables[0];
        }
        
        public static DataTable BaocaoBenhnhanKhoaphongTonghop(string DepartmentssDesc, DateTime? FromDate, DateTime? ToDate, 
            string MaDoiTuong, string CreateBy, string MAKHOATHIEN)
        {
            return SPs.BaocaoBenhnhanKhoaphongTonghop(DepartmentssDesc,FromDate,ToDate,MaDoiTuong,CreateBy,MAKHOATHIEN).GetDataSet().Tables[0];
        }
        public static DataTable baocaotinhinhchidinhCLS(DateTime? FromDate, DateTime? ToDate, short? IsPayment, string ObjectTypeCode, string CreateBy)
        {
            return SPs.BaocaoTinhinhChidinhCLS(FromDate, ToDate, IsPayment, ObjectTypeCode, CreateBy).GetDataSet().Tables[0];
        }
        public static DataTable baocaobaocao79arutgon(string ObjectTypeCode, string MAKHOATHIEN, 
            string CreateBy, DateTime? FromDate, DateTime? ToDate, int IsThanhToan)
        {
            return SPs.BaocaoBaocao79aRutgon(ObjectTypeCode, MAKHOATHIEN,CreateBy, FromDate,
                                                                     ToDate, IsThanhToan).GetDataSet().Tables[0];
        }
        public static DataTable baocao79arutgoncothucthu(string ObjectTypeCode, string MAKHOATHIEN, string CreateBy, DateTime? FromDate, DateTime? ToDate, int IsThucThu, int IsTongHop)
        {
            return SPs.Baocao79aRutgonCothucthu(ObjectTypeCode, MAKHOATHIEN, CreateBy, FromDate, ToDate, IsThucThu, IsTongHop).GetDataSet().Tables[0];
        }
        
    }
}
