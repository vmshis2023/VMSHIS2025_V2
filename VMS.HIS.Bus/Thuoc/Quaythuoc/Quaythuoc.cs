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

    public class Quaythuoc
    {
        private NLog.Logger log;
        public Quaythuoc()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        public static DataTable QuaythuocLaydanhsachkhachhang(int? idbenhnhan, string patientName, DateTime? Tungay, DateTime? Denngay)
        {
            return SPs.QuaythuocLaydanhsachkhachhang(idbenhnhan,
                   patientName,
                   Tungay,
                   Denngay).GetDataSet().Tables[0];
        }
        public static DataTable QuaythuocLaydonthuockhachhang(int? iddonthuoc)
        {
            return SPs.QuaythuocLaydonthuockhachhang(iddonthuoc).GetDataSet().Tables[0];
        }
    }
}
