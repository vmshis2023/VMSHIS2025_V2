using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using System.Data;

namespace VNS.Libs
{
   public class TinhCLS
    {
       public static void GB_TinhPhtramBHYT(KcbChidinhclsChitiet objChidinhChitiet, KcbLichsuDoituongKcb objLichsu, bool noitru, decimal PTramBHYT)
       {
           byte TrangthaiBhyt = 1;
           decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
           bool b_ExistPtramBHYT = false;
           if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb.Value))//Đối tượng DV
           {
               TrangthaiBhyt = (byte)0;
               objChidinhChitiet.TuTuc = 0;
           }
           else
               TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
           if (Utility.Int32Dbnull(objChidinhChitiet.TrangthaiHuy, -1) == -1) objChidinhChitiet.TrangthaiHuy = 0;
           DmucDichvuclsChitiet obServiceDetail =
               DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu));
           if (obServiceDetail != null)
           {
               objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(obServiceDetail.DonGia);
           }
           objChidinhChitiet.PtramBhyt = PTramBHYT;
           objChidinhChitiet.PtramBhytGoc = objLichsu.PtramBhytGoc;
           objChidinhChitiet.LoaiChietkhau = 0;
           objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
           objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
           if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc, 0) == 1)
           {
               objChidinhChitiet.BhytChitra = 0;
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0);
               objChidinhChitiet.PtramBhyt = 0;
           }
           else
           {
               //Mục tính BHYT đặc biệt để dành sử dụng trong tương lai
               PtramBhytDacBiet(objChidinhChitiet, objLichsu, 2, ref b_ExistPtramBHYT);
               if (b_ExistPtramBHYT)
               {
                   objChidinhChitiet.BhytChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia) *
                                          Utility.DecimaltoDbnull(objChidinhChitiet.PtramBhyt) / 100;
                   objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         Utility.DecimaltoDbnull(objChidinhChitiet.BhytChitra);
               }
               else//99% rơi vào nhánh này
               {

                   PTramBHYT = Utility.DecimaltoDbnull(objLichsu.PtramBhyt);
                   decimal BHCT = 0m;
                   if (objLichsu.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                   {
                       BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                   }
                   else//DV và BHYT trái tuyến
                   {
                       if (objLichsu.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhyt, 0) / 100);
                       else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia.Value, 0) * (Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                   }
                   decimal BNCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) - BHCT;
                   objChidinhChitiet.BhytChitra = BHCT;
                   objChidinhChitiet.BnhanChitra = BNCT;
               }

           }
       }
       /// <summary>
       /// hàm thực hienj việc tính phâm trăm bảo hiểm
       /// </summary>
       /// <param name="objChidinhChitiet"></param>
       /// <param name="PTramBHYT"></param>
       public static void GB_TinhPhtramBHYT(KcbChidinhclsChitiet objChidinhChitiet, KcbLuotkham objLuotkham,bool noitru, decimal PTramBHYT)
       {
           byte trangthaiBhyt = 1;
           decimal bhytPtramTraituyennoitru = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
           bool bExistPtramBHYT = false;
           if (objLuotkham.IdLoaidoituongKcb != null && !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
           {
               trangthaiBhyt = (byte)0;
               objChidinhChitiet.TuTuc = 0;
           }
           else
               trangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
           if (Utility.Int32Dbnull(objChidinhChitiet.TrangthaiHuy, -1) == -1) objChidinhChitiet.TrangthaiHuy = 0;
           //DataTable dtDichvu =  SPs.SpKcbLaydoituongDichvuChitiet(objChidinhChitiet.IdChitietdichvu).GetDataSet().Tables[0];
           //if (dtDichvu != null && dtDichvu.Rows.Count>0)
           //{
           //    objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(dtDichvu.Rows[0]["Don_Gia"]);
           //}
           //objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia,0);
           objChidinhChitiet.PtramBhyt = PTramBHYT;
           objChidinhChitiet.PtramBhytGoc = objLuotkham.PtramBhytGoc;
           objChidinhChitiet.LoaiChietkhau = 0;
           objChidinhChitiet.TrangthaiBhyt = trangthaiBhyt;
           objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
           if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc, 0) == 1)
           {
               objChidinhChitiet.BhytChitra = 0;
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0)/100 ;
               objChidinhChitiet.PtramBhyt = 0;
           }
           else
           {
               //Mục tính BHYT đặc biệt để dành sử dụng trong tương lai
               PtramBhytDacBiet(objChidinhChitiet, objLuotkham, 2, ref bExistPtramBHYT);
               if (bExistPtramBHYT)
               {
                   objChidinhChitiet.BhytChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0)/100 *
                                          Utility.DecimaltoDbnull(objChidinhChitiet.PtramBhyt) / 100;
                   objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0)/100 * -
                                         Utility.DecimaltoDbnull(objChidinhChitiet.BhytChitra);
               }
               else//99% rơi vào nhánh này
               {
                   
                   PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                   decimal BHCT = 0m;
                   if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                   {
                       BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0) / 100;
                   }
                   else//DV và BHYT trái tuyến
                   {
                       if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0) / 100;
                       else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                           if (objChidinhChitiet.DonGia != null)
                               BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia.Value, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (bhytPtramTraituyennoitru / 100);
                   }
                   decimal bnct = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 0) / 100 - BHCT;
                   objChidinhChitiet.BhytChitra =BHCT;
                   objChidinhChitiet.BnhanChitra = bnct;
               }
               
           }
       }
       /// <summary>
       /// Hàm thực hiện lấy giá theo đối tượng
       /// </summary>
       /// <param name="madoituong"> Mã đối tượng giá</param>
       /// <param name="id_chitiet_dichvu"> Id chi tiết dịch vụ</param>
       /// <param name="makhoathuchien"> Mã Khoa thực hiện</param>
       /// <returns></returns>
       public static QheDoituongDichvucl LayGiaTheoDoiTuong(string madoituong, int id_chitiet_dichvu,string makhoathuchien)
       {
           QheDoituongDichvucl objQheDtuongGia =
               new Select().From(QheDoituongDichvucl.Schema).Where(QheDoituongDichvucl.Columns.MaDoituongKcb).IsEqualTo(madoituong)
               .And(QheDoituongDichvucl.Columns.IdChitietdichvu).IsEqualTo(id_chitiet_dichvu)
                   //.And(QheDoituongDichvucl.Columns.MaKhoaThuchien).IsEqualTo(makhoathuchien)
                   .ExecuteSingle<QheDoituongDichvucl>();
           return objQheDtuongGia;

       }
       public static bool CapnhatTrangthaiTutuc(KcbChidinhclsChitiet objChidinhChitiet, KcbLuotkham objLuotkham, bool noitru,byte tu_tuc, decimal PTramBHYT)
       {
           try
           {
               byte TrangthaiBhyt = 1;
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               bool b_ExistPtramBHYT = false;
               objChidinhChitiet.TuTuc = tu_tuc;
               if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
               {
                   TrangthaiBhyt = (byte)0;
                   objChidinhChitiet.TuTuc = 0;
               }
               else
               {
                     TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
               }
                 
               if (Utility.Int32Dbnull(objChidinhChitiet.TrangthaiHuy, -1) == -1)
               {
                   objChidinhChitiet.TrangthaiHuy = 0;
               }

               DmucDichvuclsChitiet obServiceDetail =
                   DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(objChidinhChitiet.IdChitietdichvu));
              QheDoituongDichvucl objqhedoituong = new QheDoituongDichvucl();
               if (obServiceDetail != null)
               {
                   if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc,0) ==1)
                   {
                       objqhedoituong = LayGiaTheoDoiTuong("DV", objChidinhChitiet.IdChitietdichvu,
                                                                        objLuotkham.MaKhoaThuchien);
                   }
                   else
                   {
                       objqhedoituong = LayGiaTheoDoiTuong(objLuotkham.MaDoituongKcb, objChidinhChitiet.IdChitietdichvu,
                                                                       objLuotkham.MaKhoaThuchien);
                   }
                  // objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(obServiceDetail.DonGia);
                    if (objqhedoituong !=null)
                    {
                        objChidinhChitiet.GiaDanhmuc = Utility.DecimaltoDbnull(objqhedoituong.DonGia, 0);
                        objChidinhChitiet.DonGia = Utility.DecimaltoDbnull(objqhedoituong.DonGia, 0);
                        objChidinhChitiet.MadoituongGia = Utility.sDbnull(objqhedoituong.MaDoituongKcb);
                    }
                   
               }
               objChidinhChitiet.PtramBhyt = PTramBHYT;
               objChidinhChitiet.PtramBhytGoc = objLuotkham.PtramBhytGoc;
              // objChidinhChitiet.LoaiChietkhau = 0;
              // objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
              //objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
               if (Utility.Int32Dbnull(objChidinhChitiet.TuTuc, 0) == 1)
               {
                   objChidinhChitiet.BhytChitra = 0;
                   if(objqhedoituong !=null)
                   {
                       objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objqhedoituong.DonGia, 0) * Utility.DecimaltoDbnull(objqhedoituong.TyleTt,100)/100;
                       
                   }
                   objChidinhChitiet.PtramBhyt = 0;
               }
               else
               {
                   //Mục tính BHYT đặc biệt để dành sử dụng trong tương lai
                   PtramBhytDacBiet(objChidinhChitiet, objLuotkham, 2, ref b_ExistPtramBHYT);
                   if (b_ExistPtramBHYT)
                   {
                       objChidinhChitiet.BhytChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 * Utility.DecimaltoDbnull(objChidinhChitiet.PtramBhyt) / 100;
                       objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 - Utility.DecimaltoDbnull(objChidinhChitiet.BhytChitra);
                   }
                   else//99% rơi vào nhánh này
                   {

                       PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                       {
                           BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       }
                       else//DV và BHYT trái tuyến
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                               BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               if (objChidinhChitiet.DonGia != null)
                                   BHCT = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia.Value, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }
                       decimal bnct = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) * Utility.DecimaltoDbnull(objChidinhChitiet.TyleTt, 100) / 100 - BHCT;
                       objChidinhChitiet.BhytChitra = BHCT;
                       objChidinhChitiet.BnhanChitra = bnct;
                   }

               }
               objChidinhChitiet.MarkOld();
               objChidinhChitiet.IsNew = false;
               objChidinhChitiet.Save();
               return true;
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
               return false;
           }
          
       }
       public static bool CapnhatTrangthaiTutuc(NoitruPhanbuonggiuong objDoituong, KcbLuotkham objLuotkham, bool noitru, byte tu_tuc, decimal PTramBHYT)
       {
           try
           {
               byte TrangthaiBhyt = 1;
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               bool b_ExistPtramBHYT = false;
               objDoituong.TuTuc = tu_tuc;
               if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
               {
                   TrangthaiBhyt = (byte)0;
                   objDoituong.TuTuc = 0;
               }
               else
                   TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
               if (Utility.Int32Dbnull(objDoituong.TrangthaiHuy, -1) == -1) objDoituong.TrangthaiHuy = 0;

               // objChidinhChitiet.LoaiChietkhau = 0;
               // objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
               //objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
               if (Utility.Int32Dbnull(objDoituong.TuTuc, 0) == 1)
               {
                   objDoituong.BhytChitra = 0;
                   objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0);
               }
               else
               {
                   if (b_ExistPtramBHYT)
                   {
                       objDoituong.BhytChitra = Utility.DecimaltoDbnull(objDoituong.DonGia) *
                                              Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                       objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) -
                                             Utility.DecimaltoDbnull(objDoituong.BhytChitra);
                   }
                   else//99% rơi vào nhánh này
                   {

                       PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                       {
                           BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       }
                       else//DV và BHYT trái tuyến
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }
                       decimal BNCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) - BHCT;
                       objDoituong.BhytChitra = BHCT;
                       objDoituong.BnhanChitra = BNCT;
                   }

               }
               objDoituong.MarkOld();
               objDoituong.IsNew = false;
               objDoituong.Save();
               return true;
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
               return false;
           }

       }
       public static bool CapnhatTrangthaiTutuc(KcbDangkySokham objDoituong, KcbLuotkham objLuotkham, bool noitru, byte tu_tuc, decimal PTramBHYT)
       {
           try
           {
               byte TrangthaiBhyt = 1;
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               bool b_ExistPtramBHYT = false;
               objDoituong.TuTuc = tu_tuc;
               if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
               {
                   TrangthaiBhyt = (byte)0;
                   objDoituong.TuTuc = 0;
               }
               else
                   TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);

               objDoituong.PtramBhyt = PTramBHYT;
               objDoituong.PtramBhytGoc = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, PTramBHYT);
               // objChidinhChitiet.LoaiChietkhau = 0;
               // objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
               //objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
               if (Utility.Int32Dbnull(objDoituong.TuTuc, 0) == 1)
               {
                   objDoituong.BhytChitra = 0;
                   objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0);
                   objDoituong.PtramBhyt = 0;
               }
               else
               {
                   if (b_ExistPtramBHYT)
                   {
                       objDoituong.BhytChitra = Utility.DecimaltoDbnull(objDoituong.DonGia) *
                                              Utility.DecimaltoDbnull(objDoituong.PtramBhyt) / 100;
                       objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) -
                                             Utility.DecimaltoDbnull(objDoituong.BhytChitra);
                   }
                   else//99% rơi vào nhánh này
                   {

                       PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                       {
                           BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       }
                       else//DV và BHYT trái tuyến
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }
                       decimal BNCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) - BHCT;
                       objDoituong.BhytChitra = BHCT;
                       objDoituong.BnhanChitra = BNCT;
                   }

               }
               objDoituong.MarkOld();
               objDoituong.IsNew = false;
               objDoituong.Save();
               return true;
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
               return false;
           }

       }
       public static bool CapnhatTrangthaiTutuc(KcbDonthuocChitiet objDoituong, KcbLuotkham objLuotkham, bool noitru, byte tu_tuc, decimal PTramBHYT)
       {
           try
           {
               byte TrangthaiBhyt = 1;
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               bool b_ExistPtramBHYT = false;
               objDoituong.TuTuc = tu_tuc;
               if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
               {
                   TrangthaiBhyt = (byte)0;
                   objDoituong.TuTuc = 0;
               }
               else
                   TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
               if (Utility.Int32Dbnull(objDoituong.TrangthaiHuy, -1) == -1) objDoituong.TrangthaiHuy = 0;

               objDoituong.PtramBhyt = PTramBHYT;
               objDoituong.PtramBhytGoc = objLuotkham.PtramBhytGoc;
               // objChidinhChitiet.LoaiChietkhau = 0;
               // objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
               //objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
               if (Utility.Int32Dbnull(objDoituong.TuTuc, 0) == 1)
               {
                   objDoituong.BhytChitra = 0;
                   objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0);
                   objDoituong.PtramBhyt = 0;
               }
               else
               {
                   if (b_ExistPtramBHYT)
                   {
                       objDoituong.BhytChitra = Utility.DecimaltoDbnull(objDoituong.DonGia) *
                                              Utility.DecimaltoDbnull(objDoituong.PtramBhyt) / 100;
                       objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) -
                                             Utility.DecimaltoDbnull(objDoituong.BhytChitra);
                   }
                   else//99% rơi vào nhánh này
                   {

                       PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                       {
                           BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       }
                       else//DV và BHYT trái tuyến
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }
                       decimal BNCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) - BHCT;
                       objDoituong.BhytChitra = BHCT;
                       objDoituong.BnhanChitra = BNCT;
                   }

               }
               objDoituong.MarkOld();
               objDoituong.IsNew = false;
               objDoituong.Save();
               return true;
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
               return false;
           }

       }
       public static bool CapnhatTrangthaiTutuc(KcbDangkyKcb objDoituong, KcbLuotkham objLuotkham, bool noitru, byte tu_tuc, decimal PTramBHYT)
       {
           try
           {
               byte TrangthaiBhyt = 1;
               decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
               bool b_ExistPtramBHYT = false;
               objDoituong.TuTuc = tu_tuc;
               if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//Đối tượng DV
               {
                   TrangthaiBhyt = (byte)0;
                   objDoituong.TuTuc = 0;
               }
               else
                   TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
               if (Utility.Int32Dbnull(objDoituong.TrangthaiHuy, -1) == -1) objDoituong.TrangthaiHuy = 0;
              
               objDoituong.PtramBhyt = PTramBHYT;
               objDoituong.PtramBhytGoc = objLuotkham.PtramBhytGoc;
               // objChidinhChitiet.LoaiChietkhau = 0;
               // objChidinhChitiet.TrangthaiBhyt = TrangthaiBhyt;
               //objChidinhChitiet.IdLoaichidinh = 0;//Chưa hiểu trường này-->Cần xem lại
               if (Utility.Int32Dbnull(objDoituong.TuTuc, 0) == 1)
               {
                   objDoituong.BhytChitra = 0;
                   objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0);
                   objDoituong.PtramBhyt = 0;
               }
               else
               {
                   if (b_ExistPtramBHYT)
                   {
                       objDoituong.BhytChitra = Utility.DecimaltoDbnull(objDoituong.DonGia) *
                                              Utility.DecimaltoDbnull(objDoituong.PtramBhyt) / 100;
                       objDoituong.BnhanChitra = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) -
                                             Utility.DecimaltoDbnull(objDoituong.BhytChitra);
                   }
                   else//99% rơi vào nhánh này
                   {

                       PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                       decimal BHCT = 0m;
                       if (objLuotkham.DungTuyen == 1)//BHYT đúng tuyến rơi vào nhánh này dù nội trú ngay ngoại trú
                       {
                           BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       }
                       else//DV và BHYT trái tuyến
                       {
                           if (objLuotkham.TrangthaiNoitru <= 0 || !noitru)//Đối tượng ngoại trú hoặc Chỉ định ngoại trú-->Lấy phần trăm ngoại trú
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                           else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                               BHCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                       }
                       decimal BNCT = Utility.DecimaltoDbnull(objDoituong.DonGia, 0) - BHCT;
                       objDoituong.BhytChitra = BHCT;
                       objDoituong.BnhanChitra = BNCT;
                   }

               }
               objDoituong.MarkOld();
               objDoituong.IsNew = false;
               objDoituong.Save();
               return true;
           }
           catch (Exception ex)
           {
               Utility.CatchException(ex);
               return false;
           }

       }
       public static void PtramBhytDacBiet(KcbChidinhclsChitiet objChidinhChitiet, KcbLichsuDoituongKcb objLichsu, int paymentTypeId, ref bool bExist)
       {
           bExist = false;
           string isDungTuyen = "DT";
           DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLichsu.IdDoituongKcb);
           if (objectType != null)
           {
               switch (objectType.MaDoituongKcb)
               {
                   case "BHYT":
                       if (Utility.Int32Dbnull(objLichsu.DungTuyen, "0") == 1) isDungTuyen = "DT";
                       else
                       {
                           isDungTuyen = "TT";
                       }
                       break;
                   default:
                       isDungTuyen = "KHAC";
                       break;
               }

           }
           SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
               .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objChidinhChitiet.IdChitietchidinh)
               .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(paymentTypeId)
               .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(isDungTuyen)
               .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLichsu.MaDoituongKcb);
           var objDetailPtramBhyt = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
           if (objDetailPtramBhyt != null)
           {
               objChidinhChitiet.IdLoaichidinh = 1;
               objChidinhChitiet.BhytChitra = Gia_BHYT(objDetailPtramBhyt.TileGiam, Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0));
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         objChidinhChitiet.BhytChitra;
               objChidinhChitiet.PtramBhyt = Utility.DecimaltoDbnull(objDetailPtramBhyt.TileGiam, 0);
               objChidinhChitiet.LoaiChietkhau = 1;
               bExist = true;
               // objChidinhChitiet.DonGia =
           }
       }
       /// <summary>
       /// hàm thực hiện viec tính toán giá đặc biệt cho bệnh nhân
       /// </summary>
       /// <param name="objChidinhChitiet"></param>
       /// <param name="objLuotkham"></param>
       /// <param name="paymentTypeId"></param>
       public static void PtramBhytDacBiet(KcbChidinhclsChitiet objChidinhChitiet,KcbLuotkham objLuotkham,int paymentTypeId, ref bool bExist)
       {
           bExist = false;
           string isDungTuyen = "DT";
           DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
           if (objectType != null)
           {
               switch (objectType.MaDoituongKcb)
               {
                   case "BHYT":
                       if (Utility.Int32Dbnull(objLuotkham.DungTuyen, "0") == 1) isDungTuyen = "DT";
                       else
                       {
                           isDungTuyen = "TT";
                       }
                       break;
                   default:
                       isDungTuyen = "KHAC";
                       break;
               }

           }
           SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
               .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objChidinhChitiet.IdChitietchidinh)
               .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(paymentTypeId)
               .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(isDungTuyen)
               .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
           var objDetailPtramBhyt = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
           if(objDetailPtramBhyt!=null)
           {
               objChidinhChitiet.IdLoaichidinh = 1;
               objChidinhChitiet.BhytChitra = Gia_BHYT(objDetailPtramBhyt.TileGiam,Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0));
               objChidinhChitiet.BnhanChitra = Utility.DecimaltoDbnull(objChidinhChitiet.DonGia, 0) -
                                         objChidinhChitiet.BhytChitra;
               objChidinhChitiet.PtramBhyt = Utility.DecimaltoDbnull(objDetailPtramBhyt.TileGiam, 0);
               objChidinhChitiet.LoaiChietkhau = 1;
               bExist = true;
           }
       }
       private static decimal Gia_BHYT(decimal PhanTramBHYT, decimal DON_GIA)
       {
           return PhanTramBHYT * DON_GIA / 100;
       }
    }
}
