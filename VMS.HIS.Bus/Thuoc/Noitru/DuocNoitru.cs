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

    public class DuocNoitru
    {
        private NLog.Logger log;
        public DuocNoitru()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        public static DataTable ThuocNoitruTimkiemPhieutrathuocthua(int? Id, string tungay, string denngay, byte? kieungay, int? idkhoatra, int? idkhonhan, int? nguoitra, int? nguoinhan, byte? trangthai, string kieuthuocvt)
        {
            return SPs.ThuocNoitruTimkiemPhieutrathuocthua(Id, tungay, denngay, kieungay, idkhoatra, idkhonhan, nguoitra, nguoinhan, trangthai, kieuthuocvt).GetDataSet().Tables[0];
        }
        public static DataSet ThuocNoitruLayChitietPhieutrathuocthua(int Id,byte tonghoplai)
        {
            return SPs.ThuocNoitruLayChitietPhieutrathuocthua(Id, tonghoplai).GetDataSet();
        }
        public static DataSet ThuocNoitruTimkiemThuocthuatralai(int? idkhoanoitru, int? idkholinh, string kieuthuocvt)
        {
            return SPs.ThuocNoitruTimkiemThuocthuatralai(idkhoanoitru, idkholinh, kieuthuocvt).GetDataSet();
        }
    }
}

