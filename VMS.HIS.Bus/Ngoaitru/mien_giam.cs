using System;
using System.Data;
using System.Transactions;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using VNS.Properties;
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class mien_giam
    {
        private NLog.Logger log;
        public mien_giam()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public static DataTable Inphieumiengiam(long id_mg)
        {
            try
            {
                return SPs.KcbLaythongtinMiengiamDein(id_mg).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return null;

            }
        }
    }
}
