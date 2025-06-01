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
   public class dmucnhanvien_busrule
    {


       public static string Insert(DmucNhanvien objDmucNhanvien, QheNhanvienKhoCollection lstQhekho, QheBacsiKhoaphongCollection lstQhekhoa, QheNhanvienQuyensudungCollection lstQheQuyensudung, QheNhanvienDanhmucCollection lstQheDmuc, QheNhanvienBaocaomultiCollection lstQheBaocaomulti, QheNhanvienDmucchungCollection lstQheDmchung, QheNhanvienCosoCollection lstQhecoso, bool saveNhanvien)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (saveNhanvien && objDmucNhanvien != null) objDmucNhanvien.Save();
                        if (lstQheDmuc != null) new Delete().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQheDmchung != null) new Delete().From(QheNhanvienDmucchung.Schema).Where(QheNhanvienDmucchung.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQhekho != null) new Delete().From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQhekhoa != null) new Delete().From(QheBacsiKhoaphong.Schema).Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQheQuyensudung != null) new Delete().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQheBaocaomulti != null) new Delete().From(QheNhanvienBaocaomulti.Schema).Where(QheNhanvienBaocaomulti.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQhecoso != null) new Delete().From(QheNhanvienCoso.Schema).Where(QheNhanvienCoso.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        if (lstQhecoso != null)
                            foreach (QheNhanvienCoso obj in lstQhecoso)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQheDmuc != null)
                            foreach (QheNhanvienDanhmuc obj in lstQheDmuc)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQhekho != null)
                            foreach (QheNhanvienKho obj in lstQhekho)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQhekhoa != null)
                            foreach (QheBacsiKhoaphong obj in lstQhekhoa)
                            {
                                obj.IdBacsi = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQheQuyensudung != null)
                            foreach (QheNhanvienQuyensudung obj in lstQheQuyensudung)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQheBaocaomulti != null)
                            foreach (QheNhanvienBaocaomulti obj in lstQheBaocaomulti)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQheDmchung != null)
                            foreach (QheNhanvienDmucchung obj in lstQheDmchung)
                            {
                                obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                            }
                        if (lstQheDmchung != null) lstQheDmchung.SaveAll();
                        if (lstQheBaocaomulti != null) lstQheBaocaomulti.SaveAll();
                        if (lstQheDmuc != null) lstQheDmuc.SaveAll();
                        if (lstQhekho != null) lstQhekho.SaveAll();
                        if (lstQhekhoa != null) lstQhekhoa.SaveAll();
                        if (lstQheQuyensudung != null) lstQheQuyensudung.SaveAll();
                        if (lstQhecoso != null) lstQhecoso.SaveAll();
                    }
                    scope.Complete();
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
       public static string Delete(int idNhanvien)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Delete().From(QheNhanvienCoso.Schema).Where(QheNhanvienCoso.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(QheBacsiKhoaphong.Schema).Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
                       new Delete().From(QheNhanvienBaocaomulti.Schema).Where(QheNhanvienBaocaomulti.Columns.IdNhanvien).IsEqualTo(idNhanvien).Execute();
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
