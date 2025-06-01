using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

namespace VNS.Libs
{
    public class TinhGiaThuoc
    {
       /// <summary>
       /// hàm thực hiện việc tính giá chỉ định cls
       /// </summary>
       /// <param name="objPatientExam"></param>
       /// <param name="objPresDetail"></param>
       public static void TinhGiaChiDinhThuoc(KcbLuotkham objPatientExam, KcbDonthuocChitiet objPresDetail)
        {
            GB_TinhPhtramBHYT(objPresDetail, Utility.DecimaltoDbnull(objPatientExam.PtramBhyt, 0));
        }
       
        
        /// <summary>
        /// hàm thực hienj việc tính phâm trăm bảo hiểm
        /// </summary>
        /// <param name="objAssignDetail"></param>
        /// <param name="PTramBHYT"></param>
       public static void GB_TinhPhtramBHYT(KcbDonthuocChitiet objPresDetail, decimal PTramBHYT)
       {
           objPresDetail.PtramBhyt = Utility.DecimaltoDbnull(PTramBHYT);

           if (Utility.Int32Dbnull(objPresDetail.TuTuc, 0) == 1)//Là bảo hiểm y tế tự túc
           {
               objPresDetail.BhytChitra = 0;
               objPresDetail.BnhanChitra = Utility.DecimaltoDbnull(objPresDetail.DonGia, 0);
               objPresDetail.PtramBhyt = 0;
           }
           else
           {
               objPresDetail.BhytChitra = Utility.DecimaltoDbnull(objPresDetail.DonGia) *
                                           Utility.DecimaltoDbnull(PTramBHYT) / 100;

               objPresDetail.BnhanChitra = Utility.DecimaltoDbnull(objPresDetail.DonGia, 0) -
                                         Utility.DecimaltoDbnull(objPresDetail.BhytChitra, 0);
               objPresDetail.PtramBhyt = Utility.DecimaltoDbnull(PTramBHYT);
           }

       }
    }
}
