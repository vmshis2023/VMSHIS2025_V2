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
    public class BAOCAO_THUOC
    {
        private NLog.Logger log;
        public BAOCAO_THUOC()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static DataTable ThuocBaocaotinhhinhBenhnhanlinhthuoc(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, int? iddoituong)
        {
            return SPs.ThuocBaocaotinhhinhBenhnhanlinhthuoc(FromDate, ToDate, IDKHOXUAT, iddoituong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaotinhhinhPhatthuocbenhnhan(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, 
            int? iddoituong, int? Kieuthongke)
        {
            return SPs.ThuocBaocaotinhhinhPhatthuocbenhnhan(FromDate, ToDate, IDKHOXUAT, iddoituong, Kieuthongke).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaotinhhinhkedonthuocTheobacsy(int? IDKHOXUAT, int? iddoituong, string mabschidinh, int? idThuoc, DateTime? FromDate, DateTime? ToDate)
        {
            return SPs.ThuocBaocaotinhhinhkedonthuocTheobacsy(IDKHOXUAT, iddoituong, mabschidinh, idThuoc, FromDate, ToDate).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaothuochethan(int? IDThuoc, int? IDKHO, int? CanhBaoTruoc, int? NhomThuoc)
        {
            return SPs.ThuocBaocaothuochethan(IDThuoc,
                IDKHO, CanhBaoTruoc, NhomThuoc).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaosoluongtonthuoctheokho(string IDKHOLIST, int? IDTHUOC, short? HETHAN)
        {
            return SPs.ThuocBaocaosoluongtonthuoctheokho(IDKHOLIST, IDTHUOC, HETHAN).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaothuoctheonhacungcap(DateTime? FromDate, DateTime? ToDate, int? IDKHO, string ma_nhacungcap)
        {
            return SPs.ThuocBaocaothuoctheonhacungcap(FromDate, ToDate, IDKHO, ma_nhacungcap).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaobiendongthuocTrongkhotong(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaobiendongthuocTrongkhotong(FromDate,ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];
               
        }
        public static DataTable ThuocBaocaobiendongthuocTrongkhole(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaobiendongthuocTrongkhole(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaotinhhinhnhapkhothuoc(DateTime? FromDate, DateTime? ToDate, int? TrangThai, int? IDKho)
        {
            return SPs.ThuocBaocaotinhhinhnhapkhothuoc(FromDate, ToDate, TrangThai, IDKho).GetDataSet().Tables[0];
        }
        
    }
}
