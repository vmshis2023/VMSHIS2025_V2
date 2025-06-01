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
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class DMUC_CHUNG
    {
        private NLog.Logger log;
        public DMUC_CHUNG()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static DataTable LayDulieuDanhmucChung(List<string> lstLoai)
        {
            DataTable m_NN = new DataTable();

            m_NN =
                new Select(DmucChung.Columns.Ma, DmucChung.Columns.Ten, DmucChung.Columns.Loai).From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).In(lstLoai)
                    .OrderAsc(DmucChung.Columns.SttHthi)
                    .ExecuteDataSet().Tables[0];
            return m_NN;
        }
        public static DataTable GetAllDepartment()
        {
            return new Select().From(DmucKhoaphong.Schema).OrderAsc(DmucKhoaphong.Columns.SttHthi).ExecuteDataSet().Tables[0];
        }
        public static DataTable GetStaffListbyDepartmentId(int departmentId)
        {
            DataTable dataTable = new DataTable();
            SqlQuery sqlQuery = new Select().From(DmucNhanvien.Schema)
                .Where(DmucNhanvien.Columns.IdKhoa).IsEqualTo(departmentId);
            dataTable = sqlQuery.ExecuteDataSet().Tables[0];
            return dataTable;

        }
        public static DataTable GetViewDataInDepartment(int Parent_id)
        {
            return SPs.GetViewDataInDepartment(Parent_id).GetDataSet().Tables[0];
        }
      
    }
}
