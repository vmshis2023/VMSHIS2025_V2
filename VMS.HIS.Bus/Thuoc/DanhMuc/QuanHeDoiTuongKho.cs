using System;
using System.Transactions;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VNS.HIS.NGHIEPVU.THUOC
{
    public class QuanHeDoiTuongKho
    {
        public static void THEM_DOITUONG_KHO(QheDoituongKho objThemDoiTuongKho)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objThemDoiTuongKho.IsNew = true;
                        objThemDoiTuongKho.Save();
                    }
                    Scope.Complete();
                }
            }
            catch (Exception exception)
            {
                //log.Error("Loi trong qua trinh sua phieu nhap kho :{0}", exception);

            }
        }

        public static void XOA_DOITUONG_KHO(QheDoituongKho objXoaDoiTuongKho)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(QheDoituongKho.Schema)
                            .Where(QheDoituongKho.Columns.MaDoituongKcb).IsEqualTo(objXoaDoiTuongKho.MaDoituongKcb)
                            .And(QheDoituongKho.Columns.IdKho).IsEqualTo(objXoaDoiTuongKho.IdKho)
                            .Execute();
                     
                    }
                    Scope.Complete();
                }
            }
            catch (Exception exception)
            {
                //log.Error("Loi trong qua trinh sua phieu nhap kho :{0}", exception);

            }
        }
    }
}
