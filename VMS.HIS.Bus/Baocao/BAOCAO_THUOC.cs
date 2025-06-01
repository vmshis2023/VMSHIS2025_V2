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
    public class BAOCAO_THUOC
    {
        private NLog.Logger log;
        public BAOCAO_THUOC()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public static DataTable ThuocBaocaonhapxuatthuoc(string FromDate, string ToDate, string IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaonhapxuatthuoc(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];

        }
        public static DataTable BaocaoBanthuoctaiquayBvsg(string FromDate, string ToDate, string ma_doituongkcb, string ma_tnv, string ma_httt, string ma_nganhang, string IDKHO, byte? loaidonthuoc, int? id_bacsi, byte? loaibaocao)
        {
            return SPs.BaocaoBanthuoctaiquayBvsg(FromDate, ToDate, ma_doituongkcb, ma_tnv, ma_httt, ma_nganhang, IDKHO, loaidonthuoc, id_bacsi, loaibaocao).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaoXuatthuockhoaNoitru(string FromDate, string ToDate, string IDKHO, string IDKHOA, string NhomThuoc, int? IDThuoc, byte? tonghoptheokhoa)
        {
            return SPs.ThuocBaocaoXuatthuockhoaNoitru(FromDate, ToDate, IDKHO, IDKHOA, NhomThuoc, IDThuoc, tonghoptheokhoa).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaoXuatthuockhoaNoitruTonghop(string FromDate, string ToDate, string IDKHO, string IDKHOA, string NhomThuoc, int? IDThuoc, byte? tonghoptheokhoa)
        {
            return SPs.ThuocBaocaoXuatthuockhoaNoitruTonghop(FromDate, ToDate, IDKHO, IDKHOA, NhomThuoc, IDThuoc, tonghoptheokhoa).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaoTinhhinhBenhnhanlinhthuoc(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, int? iddoituong, int id_thuoc)
        {
            return SPs.ThuocBaocaoTinhhinhBenhnhanlinhthuoc(FromDate, ToDate, IDKHOXUAT, iddoituong, id_thuoc).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhPhatthuocbenhnhan(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, 
            int? iddoituong, int? Kieuthongke,string kieuthuoc_vt)
        {
            return SPs.ThuocBaocaoTinhhinhPhatthuocbenhnhan(FromDate, ToDate, IDKHOXUAT, iddoituong, Kieuthongke, kieuthuoc_vt).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhkedonthuocTheobacsy(int? IDKHOXUAT, int? iddoituong, string mabschidinh, int? idThuoc, DateTime? FromDate, DateTime? ToDate, short trangthai)
        {
            return SPs.ThuocBaocaoTinhhinhkedonthuocTheobacsy(IDKHOXUAT, iddoituong, mabschidinh, idThuoc, FromDate, ToDate,trangthai).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoThuochethan(int? IDThuoc, int? IDKHO, int? CanhBaoTruoc, int? NhomThuoc)
        {
            return SPs.ThuocBaocaoThuochethan(IDThuoc,
                IDKHO, CanhBaoTruoc, NhomThuoc).GetDataSet().Tables[0];
        }
        public static DataTable ThuocLaythongtinInphieuDutruthuoc(short? id_khonhan, short idkhoxuat, string KIEUTHUOCVT)
        {
            return SPs.ThuocLaythongtinInphieuDutruthuoc(id_khonhan, idkhoxuat, KIEUTHUOCVT).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoSoluongtonthuoctheokho(string IDKHOLIST, int? IDTHUOC, int idloaithuoc, short? HETHAN, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoSoluongtonthuoctheokho(IDKHOLIST, IDTHUOC, idloaithuoc, HETHAN, kieu_thuocvattu).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoInTonKhoThuoc(int IDKHO, int? IDTHUOC, int idloaithuoc, short? HETHAN, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoInTonKhoThuoc(IDKHO, IDTHUOC, idloaithuoc, HETHAN, kieu_thuocvattu).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoThuoctheonhacungcap(DateTime? FromDate, DateTime? ToDate, int? IDKHO, string ma_nhacungcap,string kieu_thuocvattu,byte laphieuvay)
        {
            return SPs.ThuocBaocaoThuoctheonhacungcap(FromDate, ToDate, IDKHO, ma_nhacungcap, kieu_thuocvattu, laphieuvay).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoBiendongthuocTrongkhotong(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaoBiendongthuocTrongkhotong(FromDate,ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];
               
        }
        public static DataTable ThuocBaocaohuychot(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, byte huychothuyxacnhan)
        {
            return SPs.ThuocBaocaohuychot(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc,huychothuyxacnhan).GetDataSet().Tables[0];

        }
        public static DataTable VacxinBaocaoPhanungnangSautiemchung(string FromDate, string ToDate, string tenbenhnhan, byte? idgioitinh, string NhomThuoc, int? IDThuoc)
        {
            return SPs.VacxinBaocaoPhanungnangSautiemchung(FromDate, ToDate, tenbenhnhan, idgioitinh, NhomThuoc, IDThuoc).GetDataSet().Tables[0];

        }

        public static DataTable VacxinBaocaoPhanungthongthuongSautiemchung(string FromDate, string ToDate, string NhomThuoc, int? IDThuoc)
        {
            return SPs.VacxinBaocaoPhanungthongthuongSautiemchung(FromDate, ToDate,  NhomThuoc, IDThuoc).GetDataSet().Tables[0];

        }

        public static DataTable VacxinBaocaotinhinhsudung(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.VacxinBaocaotinhinhsudung(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];

        }

        public static DataTable ThuocBaocaonhapxuatton(string FromDate, string ToDate, string IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong,byte baocaotheosolohandung)
        {
            return SPs.ThuocBaocaonhapxuatton(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong,baocaotheosolohandung).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaonhapxuattontheokho(string FromDate, string ToDate, string IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaonhapxuattonTheokho(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];

        }
        public static DataTable ThuocNoitruBaocaonhapxuattonTheokhoa(string FromDate, string ToDate, int? IDKHOA, string NhomThuoc, int? IDThuoc, string kieuthuocvt, int? Cobiendong)
        {
            return SPs.ThuocNoitruBaocaonhapxuattonTheokhoa(FromDate, ToDate, IDKHOA, NhomThuoc, IDThuoc, kieuthuocvt, Cobiendong).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaoBiendongthuocTrongkhole(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaoBiendongthuocTrongkhole(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocKhole(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocKhole(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocTutruc(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocTutruc(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocKhochan(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocKhochan(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuoc(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuoc(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocThang(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocThang(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocSovatlieuchitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocSovatlieuchitiet(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaophatsinhTonghop(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, byte kieubiendong, string NhomThuoc, int? Cobiendong, int? id_khonhan)
        {
            return SPs.ThuocBaocaophatsinhTonghop(FromDate, ToDate, IDKHO, NhomThuoc,kieubiendong,id_khonhan, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaophatsinhChitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, byte kieubiendong, string NhomThuoc, int? Cobiendong, int? id_khonhan)
        {
            return SPs.ThuocBaocaophatsinhChitiet(FromDate, ToDate, IDKHO, NhomThuoc, kieubiendong,id_khonhan, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocChitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong, long id_thuockho)
        {
            return SPs.ThuocThethuocChitiet(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC,id_thuockho, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocTutrucChitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong,long id_thuockho)
        {
            return SPs.ThuocThethuocTutrucChitiet(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC,id_thuockho, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhnhapkhothuoc(string FromDate, string ToDate, int? TrangThai, int? IDKho, int id_thuoc, byte loaiphieu, int kieungaytimkiem, string lydohuy, string manhacungcap, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoTinhhinhnhapkhothuoc(FromDate, ToDate, TrangThai, IDKho, id_thuoc, loaiphieu, kieungaytimkiem, lydohuy, manhacungcap, kieu_thuocvattu).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhxuatvacxintuyenhuyen(string FromDate, string ToDate, int? TrangThai, int? IDKho, int id_thuoc, byte loaiphieu, int kieungaytimkiem, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoTinhhinhXuatvacxinTuyenhuyen(FromDate, ToDate, TrangThai, IDKho, id_thuoc, loaiphieu, kieungaytimkiem,kieu_thuocvattu).GetDataSet().Tables[0];
        }
        
    }
}
