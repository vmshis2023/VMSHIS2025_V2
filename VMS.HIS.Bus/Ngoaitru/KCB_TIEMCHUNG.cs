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
    public class KCB_TIEMCHUNG
    {
        private NLog.Logger log;
        public KCB_TIEMCHUNG()
        {
            log = LogManager.GetCurrentClassLogger();
        }
       
        public static DataSet KcbTiemchungPhieuhen(long idBenhnhan, string maluotkham)
        {
            return SPs.KcbTiemchungPhieuhen(idBenhnhan, maluotkham).GetDataSet();
        }
        public static ActionResult CapnhatPhieuhen(List<KcbPhieuhenTiemchungChitiet> lstItems,List<long> lstDel,ref string ErrMsg)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (lstDel.Count > 0)
                            new Delete().From(KcbPhieuhenTiemchungChitiet.Schema).Where(KcbPhieuhenTiemchungChitiet.Columns.IdChitiet).In(lstDel).Execute();
                        foreach (KcbPhieuhenTiemchungChitiet item in lstItems)
                        {
                            item.Save();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return ActionResult.Error;
            }
        }
        public static ActionResult Xoaphieuhen(long id, ref string ErrMsg)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Delete().From(KcbPhieuhenTiemchungChitiet.Schema).Where(KcbPhieuhenTiemchungChitiet.Columns.Id).IsEqualTo(id).Execute();
                        new Delete().From(KcbPhieuhenTiemchung.Schema).Where(KcbPhieuhenTiemchung.Columns.Id).IsEqualTo(id).Execute();
                        
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                return ActionResult.Error;
            }
        }
    }
}
