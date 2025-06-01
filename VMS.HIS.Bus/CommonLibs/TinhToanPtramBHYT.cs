using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
namespace VNS.Libs
{
   public class TinhToanPtramBHYT
    {
       public static void TinhPtramBHYT(KcbLuotkham objPatientExam)
       {
           TinhPtramBHYTForKham(objPatientExam);
           TinhPtramBHYTForCLS(objPatientExam);
           TinhPtramBHYTForThuoc(objPatientExam);
          
       }
       /// <summary>
       /// hàm thực hiện việc tính phần trăm của khám bệnh
       /// </summary>
       /// <param name="objPatientExam"></param>
       public static void TinhPtramBHYTForKham(KcbLuotkham objPatientExam)
       {
           SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
               .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
               .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan);
           KcbDangkyKcbCollection objRegExamCollection = sqlQuery.ExecuteAsCollection<KcbDangkyKcbCollection>();
           foreach (KcbDangkyKcb objRegExam in objRegExamCollection)
           {
               // decimal PtramBHYT = Utility.DecimaltoDbnull(objPatientExam.PtramBhyt, 0);
              
               
               THU_VIEN_CHUNG.TinhToanKhamPtramBHYT(objPatientExam, objRegExam);
               new Update(KcbDangkyKcb.Schema)
                   .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(objRegExam.BhytChitra)
                   .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(objRegExam.BnhanChitra)
                   .Set(KcbDangkyKcb.Columns.PhuThu).EqualTo(objRegExam.PhuThu)
                   .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objRegExam.IdKham).Execute();
           }

       }
       /// <summary>
       /// hàm thực hiện việc tính phàn trăm bảo hiểm y tế cho cận lâm sàng
       /// </summary>
       /// <param name="objPatientExam"></param>
       public static void TinhPtramBHYTForCLS(KcbLuotkham objPatientExam)
       {
           //SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
           //    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
           //        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
           //            KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(
           //                objPatientExam.MaLuotkham).And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(
           //                    objPatientExam.IdBenhnhan).And(KcbChidinhcl.Columns.Noitru).IsEqualTo(1));
           //KcbChidinhclsChitietCollection objAssignDetailCollection = sqlQuery.ExecuteAsCollection<KcbChidinhclsChitietCollection>();
           //decimal PtramBHYT = Utility.DecimaltoDbnull(objPatientExam.PtramBhyt);
           //foreach (KcbChidinhclsChitiet objAssignDetail in objAssignDetailCollection)
           //{
           //    TinhCLS.GB_TinhPhtramBHYT(objAssignDetail, objPatientExam, PtramBHYT);
           //    new Update(KcbChidinhclsChitiet.Schema)
           //        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(DateTime.Now)
           //        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
           //        .Set(KcbChidinhclsChitiet.Columns.PtramBhyt).EqualTo(objAssignDetail.PtramBhyt)
           //        .Set(KcbChidinhclsChitiet.Columns.BnhanChitra).EqualTo(objAssignDetail.BnhanChitra)
           //        .Set(KcbChidinhclsChitiet.Columns.BhytChitra).EqualTo(objAssignDetail.BhytChitra)
           //        .Set(KcbChidinhclsChitiet.Columns.PhuThu).EqualTo(objAssignDetail.PhuThu)
           //        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objAssignDetail.IdChitietchidinh).Execute();
           //}

       }
       
       /// <summary>
       /// hàm thực hiện việc tính phần trăm của bảo hiểm y tế của thuốc
       /// </summary>
       /// <param name="objPatientExam"></param>
       public static void TinhPtramBHYTForThuoc(KcbLuotkham objPatientExam)
       {
           SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
               .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(
                   new Select(KcbDonthuoc.Columns.IdDonthuoc).From(KcbDonthuoc.Schema).Where(
                       KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(
                           objPatientExam.MaLuotkham).And(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(
                               objPatientExam.IdBenhnhan).And(KcbDonthuoc.Columns.Noitru).IsEqualTo(1));
           KcbDonthuocChitietCollection objPresDetailCollection = sqlQuery.ExecuteAsCollection<KcbDonthuocChitietCollection>();
           foreach (KcbDonthuocChitiet objPresDetail in objPresDetailCollection)
           {
               decimal PtramBHYT = Utility.DecimaltoDbnull(objPatientExam.PtramBhyt, 0);
               TinhGiaThuoc.GB_TinhPhtramBHYT(objPresDetail, PtramBHYT);
               //if (objPatientExam.MaDoiTuong == "BHYT")
               //{
               //    objPresDetail.PhuThu = 0;
               //}
               new Update(KcbDonthuocChitiet.Schema)
                 
                 .Set(KcbDonthuocChitiet.Columns.DonGia).EqualTo(objPresDetail.DonGia)
                 .Set(KcbDonthuocChitiet.Columns.BnhanChitra).EqualTo(objPresDetail.BnhanChitra)
                 .Set(KcbDonthuocChitiet.Columns.BhytChitra).EqualTo(objPresDetail.BhytChitra)
                 .Set(KcbDonthuocChitiet.Columns.PhuThu).EqualTo(objPresDetail.PhuThu)
                 .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objPresDetail.IdChitietdonthuoc).Execute();
           }

       }
       public static decimal Gia_BHYT(decimal PhanTramBHYT, decimal DON_GIA)
       {
           return PhanTramBHYT * DON_GIA / 100;
       }
       public static void TinhPhanTramBHYT(NoitruPhanbuonggiuong objNoitruPhanbuonggiuong, KcbLichsuDoituongKcb objLichsu, decimal PtramBHYT)
       {
           try
           {
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               if (Utility.Int32Dbnull(objNoitruPhanbuonggiuong.TrongGoi, 0) == 1)
               {
                   objNoitruPhanbuonggiuong.BhytChitra = 0;// Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * PtramBHYT / 100;
                   objNoitruPhanbuonggiuong.BnhanChitra = 0;
               }
               else//Ngoài gói
               {
                   if (objNoitruPhanbuonggiuong.TuTuc == 1)
                   {
                       objNoitruPhanbuonggiuong.BhytChitra = 0;
                       objNoitruPhanbuonggiuong.BnhanChitra = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0);
                   }
                   else
                   {
                       decimal BHCT = 0m;
                       if (objLichsu.DungTuyen == 1)
                       {
                           BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc, 0) / 100);
                       }
                       else
                       {
                           if (objLichsu.TrangthaiNoitru <= 0)
                               BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }

                       objNoitruPhanbuonggiuong.BhytChitra = BHCT;// Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * PtramBHYT / 100;
                       objNoitruPhanbuonggiuong.BnhanChitra = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) -
                                               BHCT;
                   }
               }
               

           }
           catch (Exception)
           {

               // throw;
           }

       }
       public static void TinhPhanTramBHYT(NoitruPhanbuonggiuong objNoitruPhanbuonggiuong,KcbLuotkham objLuotkham, decimal PtramBHYT)
       {
           try
           {
             decimal  BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               if (Utility.Int32Dbnull(objNoitruPhanbuonggiuong.TrongGoi, 0) == 1)
               {
                   objNoitruPhanbuonggiuong.BhytChitra = 0;// Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * PtramBHYT / 100;
                   objNoitruPhanbuonggiuong.BnhanChitra = 0;
               }
               else//Ngoài gói
               {
                   if (objNoitruPhanbuonggiuong.TuTuc == 1)
                   {
                       objNoitruPhanbuonggiuong.BhytChitra = 0;
                       objNoitruPhanbuonggiuong.BnhanChitra = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0);
                   }
                   else
                   {
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)
                       {
                           BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia,0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100);
                       }
                       else
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0)
                               BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia,0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }

                       objNoitruPhanbuonggiuong.BhytChitra = BHCT;// Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) * PtramBHYT / 100;
                       objNoitruPhanbuonggiuong.BnhanChitra = Utility.DecimaltoDbnull(objNoitruPhanbuonggiuong.DonGia, 0) -
                                               BHCT;
                   }
               }
               //if (Utility.Int32Dbnull(objNoitruPhanbuonggiuong.TuTuc) == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong.TrongGoi) == 1)
               //{
               //    objNoitruPhanbuonggiuong.BnhanChitra = 0;
               //    objNoitruPhanbuonggiuong.BhytChitra = 0;
               //}

           }
           catch (Exception)
           {

               // throw;
           }

       }

    }
}
