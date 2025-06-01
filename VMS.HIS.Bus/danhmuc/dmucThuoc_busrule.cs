using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VMS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using System.Transactions;

namespace VNS.HIS.NGHIEPVU
{
   public class dmucThuoc_busrule
    {
       public static string Insert(DmucThuoc objThuoc, QheCamchidinhChungphieuCollection lstQhe)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       objThuoc.Save();
                       if (!objThuoc.IsNew)
                       {
                          
                       }
                       new Delete().From(QheCamchidinhChungphieu.Schema)
                           .Where(QheCamchidinhChungphieu.Columns.IdDichvu).IsEqualTo(objThuoc.IdThuoc)
                           .And(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0)
                           .Execute();
                       new Delete().From(QheCamchidinhChungphieu.Schema)
                           .Where(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung).IsEqualTo(objThuoc.IdThuoc)
                           .And(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0)
                           .Execute();
                       foreach (QheCamchidinhChungphieu obj in lstQhe)
                       {
                           obj.IdDichvu = objThuoc.IdThuoc;
                       }
                       lstQhe.SaveAll();
                   }
                   scope.Complete();
               }
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }

       }
       public static string Delete(DmucDichvuclsChitiet objClsChitiet)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                      
                   }
                   scope.Complete();
               }
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }

       }
    }
}
