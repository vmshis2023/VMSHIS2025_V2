using System;
using System.Transactions;
using NLog;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_tonghopchiphi
    {
        private Logger log;

        public noitru_tonghopchiphi()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// </summary>
        /// <param name="objPatientExam"></param>
        /// <param name="Khoanoitru_tonghop">true= Khoa nội trú tự chốt dữ liệu;fasle= Khoa tổng hợp chốt dữ liệu</param>
        /// <returns></returns>
        public static ActionResult TongHopChiPhi(KcbLuotkham objPatientExam, short idKhoanoitru, bool Khoanoitru_tonghop)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.TrangthaiNoitru)
                            .EqualTo(Utility.Int32Dbnull(objPatientExam.TrangthaiNoitru))
                            .Set(KcbLuotkham.Columns.TthaiThopNoitru)
                            .EqualTo(Utility.Int32Dbnull(objPatientExam.TthaiThopNoitru))
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan)
                            .IsEqualTo(Utility.Int32Dbnull(objPatientExam.IdBenhnhan))
                            .Execute();
                        SPs.NoitruChotdulieuravien(objPatientExam.MaLuotkham, objPatientExam.IdBenhnhan, idKhoanoitru,
                            Utility.Bool2byte(Khoanoitru_tonghop),
                            (byte) (Utility.Byte2Bool(KcbLuotkham.Columns.TthaiThopNoitru) ? 1 : 0)).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }
    }
}