using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace VNS.Libs
{
  public  class CommonLoadDuoc
    {
      static List<string> lstKhoNoitru = new List<string> { "TATCA", "NOITRU" };
      static List<string> lstKhoNgoaitru = new List<string> { "TATCA", "NGOAITRU" };
      static List<string> lstKhoThuoc = new List<string> { "THUOC", "THUOCVT" };
      static List<string> lstKhoThuocVT = new List<string> { "THUOC", "THUOCVT","VT" };
      static List<string> lstKhoVT = new List<string> { "VT", "THUOCVT" };
      static List<string> lstKhole = new List<string> { "LE", "CHANLE" };
      static List<string> lstKhochan = new List<string> { "CHAN", "CHANLE" };
      /// <summary>
      /// hàm thực hiện việc lấy thông tin của dược
      /// </summary>
      /// <returns></returns>
      /// 
      public static DataTable LAYTHONGTIN_KHOTHUOC_TATCA()
      {
          DataTable m_dtKhoThuoc=new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if(!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);

           sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
           m_dtKhoThuoc=sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }

      public static DataTable LAYTHONGTIN_KHOTHUOC_DOITUONG(string doiTuong)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));
          }
          sqlQuery.And(TDmucKho.Columns.IdKho).In(
                  new Select(QheDoituongKho.Columns.IdKho).From(QheDoituongKho.Schema).Where(QheDoituongKho.Columns.MaDoituongKcb)
                      .IsEqualTo(doiTuong));
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }

      public static DataTable LAYTHONGTIN_KHOTHUOC_NOITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));
              //TDmucKho.Columns.KhoThuocVt

          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;

      }
      public static DataTable LAYTHONGTIN_KHOVATTU_NOITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));
              //TDmucKho.Columns.KhoThuocVt

          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;

      }
      public static DataTable LAYTHONGTIN_KHOTHUOC_NGOAITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));
              //TDmucKho.Columns.KhoThuocVt

          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
          .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;

      }
      /// <summary>
      /// hàm thực hiện việc lấy thông tin cua kho thuốc và tủ thuốc
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYDANHMUCKHO()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien).Or(TDmucKho.Columns.NguoiTao).IsEqualTo(globalVariables.UserName));

          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuocVT);
          //sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;

      }
      public static DataTable LAYTHONGTIN_KHOTHUOC(string loaikho)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien)).And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);



          }
          if(sqlQuery.HasWhere)
           sqlQuery.And(TDmucKho.Columns.KieuKho).IsEqualTo(loaikho);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).IsEqualTo(loaikho);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
         // return new Select().From(TDmucKho.Schema).Where(TDmucKho.Columns.KhoThuocVt).IsEqualTo(loaikho).OrderAsc(TDmucKho.Columns.SttHthi).ExecuteDataSet().Tables[0];
      }
      /// <summary>
      /// HÀM THỰC HIỆN VIÊC LẤY THÔNG TIN 
      /// KHO THUỐC BÁN HÀNG TỰ TÚC
      /// KHO LẺ VÀ KHO BÁN THUỐC
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_QUAYTHUOC()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema)
              .Where(TDmucKho.Columns.LaQuaythuoc).IsEqualTo(1);
          //if (!globalVariables.IsAdmin)//Có thể bỏ đối với quầy thuốc
          //{

          //    sqlQuery.And(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
          //                                              .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
          //                                              .IsEqualTo(globalVariables.gv_intIDNhanvien));



          //}
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// Hàm thực hiện danh sách kho thuốc ảo
      /// </summary>
      /// <returns></returns>
      public static  DataTable LAYTHONGTIN_KHOAO_THUOC()
      {
          try
          {
              DataTable m_dtKhoThuoc = new DataTable();
              SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
              if (!globalVariables.IsAdmin)
              {

                  sqlQuery.And(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                            .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                            .IsEqualTo(globalVariables.gv_intIDNhanvien));
              }
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
              sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
              sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
              m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
              return m_dtKhoThuoc;
          }
          catch (Exception ex)
          {
              Utility.ShowMsg("Lỗi:"+ ex.Message);
              return null;
          }
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY DANH SÁCH KHO VẬT TƯ ẢO
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOAO_VT()
      {
          try
          {
              DataTable m_dtKhoVt = new DataTable();
              SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
              if (!globalVariables.IsAdmin)
              {

                  sqlQuery.And(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                            .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                            .IsEqualTo(globalVariables.gv_intIDNhanvien));
              }
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
              sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
              sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
              m_dtKhoVt = sqlQuery.ExecuteDataSet().Tables[0];
              return m_dtKhoVt;
          }
          catch (Exception ex)
          {
              Utility.ShowMsg("Lỗi:" + ex.Message);
              return null;
          }
      }
      /// <summary>
      /// HÀM THỰC HIÊN VIỆC LẤY THÔNG TIN CỦA KHO CHẴN
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_CHAN()
      {
         DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {

              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien)
                                                        .Or(TDmucKho.Columns.NguoiTao).IsEqualTo(globalVariables.UserName));



          }
          if(sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhochan);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_LE()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien).Or(TDmucKho.Columns.NguoiTao).IsEqualTo(globalVariables.UserName));

          }
          if(sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOTHUOC_VATTU_LE()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien).Or(TDmucKho.Columns.NguoiTao).IsEqualTo(globalVariables.UserName));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuocVT);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOTHUOC_AO()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_TUTRUC_AO()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="Khothuoc_Tutruc">tru=danh sách các kho thuốc;false= danh sách kho vật tư</param>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOCLE_TUTRUC(string KIEU_THUOC_VT)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOVATTU_TATCA()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="Khothuoc_Tutruc">tru=danh sách các kho thuốc;false= danh sách kho vật tư</param>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOVATTU_CHAN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="lstLoaiBN">"TATCA", "NGOAITRU" ,"NOITRU"</param>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOVATTU_LE(List<string> lstLoaiBN)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstLoaiBN);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
    
      public static DataTable LAYTHONGTIN_KHOVATTU_LE_TUTRUC()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }

      public static DataTable LAYTHONGTIN_KHOTHUOC_TOANBO_LE()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_LE_NGOAI_TRU_KEDON(string MA_DTUONG)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          if (MA_DTUONG.Trim().ToUpper() != "ALL")
          {
              sqlQuery.And(TDmucKho.Columns.IdKho).In(new Select(QheDoituongKho.IdKhoColumn).From(QheDoituongKho.Schema).Where(QheDoituongKho.MaDoituongKcbColumn).IsEqualTo(MA_DTUONG));
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
          .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_TUTHUOC()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_TUVTTH()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_TUTHUOC_KHOA(int ID_KHOA)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(1);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.IdKhoaphong).IsEqualTo(ID_KHOA);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
     
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_LE_NOITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
           sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOVTTH_LE_NOITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_LE_TUTRUC_NOITRU(string kieuthuoc_vt)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          if (kieuthuoc_vt == "THUOC")
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          else
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
    
      public static DataTable LAYTHONGTIN_KHOTHUOC_TUTHUOC_NOITRU_THEOKHOA(int ID_KHOA)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);//Kho lẻ
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)//Không lấy tủ thuốc
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);//Chỉ lấy các kho nội trú
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuocVT);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);

          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          DataTable dt_tuthuoc = LAYTHONGTIN_TUTHUOC_KHOA(ID_KHOA);
          if (dt_tuthuoc != null)
          {
              foreach (DataRow dr in dt_tuthuoc.Rows)
                  m_dtKhoThuoc.ImportRow(dr);
          }
          return m_dtKhoThuoc;
      }
   
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_VATTU_NOITRU_THEOKHOA(int ID_KHOA)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);//Kho lẻ
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)//Không lấy tủ thuốc
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);//Chỉ lấy các kho nội trú
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuocVT);

          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          DataTable dt_khovattu=LAYTHONGTIN_VATTU_KHOA(ID_KHOA);
          if(dt_khovattu!=null)
          {
          foreach(DataRow dr in dt_khovattu.Rows)
              m_dtKhoThuoc.ImportRow(dr);
          }
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_VATTU_KHOA(int ID_KHOA)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.IdKhoaphong).IsEqualTo(ID_KHOA);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;


      }
      public static DataTable LAYTHONGTIN_KHOTHUOC_NOITRU_THEOKHOA(int ID_KHOA)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);//Kho lẻ
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)//Không lấy tủ thuốc
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);//Chỉ lấy các kho nội trú
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);

          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          
          return m_dtKhoThuoc;
      }
      
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOTHUOC_LE_NGOAITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOLE_NGOAITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.TrangThai).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      /// <summary>
      /// HÀM THỰC HIỆN LẤY THÔNG TIN KHO LẺ
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_KHOVTTH_LE_NGOAITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOXUATTHUOC_XAHUYEN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOXUATVT_XAHUYEN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOTHUOC_XAHUYEN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
      public static DataTable LAYTHONGTIN_KHOVT_XAHUYEN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KhoThuocVt).In(lstKhoVT);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(1);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          return m_dtKhoThuoc;
      }
     
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_MAKHOTHUOC_LE()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_MAKHOTHUOC_LE(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_MAKHOTHUOC_LE_NGOAITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0).And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_MAKHOTHUOC_LE_NGOAITRU(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0)
              .And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNgoaitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_MAKHOTHUOC_LE_NOITRU()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0).And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_MAKHOTHUOC_LE_NOITRU(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0).And(TDmucKho.Columns.LoaiBnhan).In(lstKhoNoitru);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_MAKHOTHUOC_CHAN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhochan);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_MAKHOTHUOC_CHAN(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));
          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhochan);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.MaKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_IDKHOTHUOC_LE()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.IdKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_IDKHOTHUOC_LE(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhole);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhole);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.IdKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> LAYDSACH_IDKHOTHUOC_CHAN()
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhochan);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return new List<string>();
          return m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.IdKho)).ToList<string>();
      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="dauphancach"></param>
      /// <returns></returns>
      public static string LAYDSACH_IDKHOTHUOC_CHAN(string dauphancach)
      {
          DataTable m_dtKhoThuoc = new DataTable();
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(new Select(QheNhanvienKho.Columns.IdKho)
                                                        .From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                                                        .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          if (sqlQuery.HasWhere)
              sqlQuery.And(TDmucKho.Columns.KieuKho).In(lstKhochan);
          else
          {
              sqlQuery.Where(TDmucKho.Columns.KieuKho).In(lstKhochan);
          }
          sqlQuery.And(TDmucKho.Columns.KhoThuocVt).In(lstKhoThuoc);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.OrderAsc(TDmucKho.Columns.SttHthi);
          m_dtKhoThuoc = sqlQuery.ExecuteDataSet().Tables[0];
          if (m_dtKhoThuoc.Rows.Count <= 0) return "";
          return string.Join(dauphancach, m_dtKhoThuoc.AsEnumerable().Select(c => c.Field<string>(TDmucKho.Columns.IdKho)).ToList<string>().ToArray());
      }
      /// <summary>
      /// hàm thực hiện việc lấy thông tin nhà cung cáp
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_NHA_CCAP()
      {
          return THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHACUNGCAP", true);
      }
      /// <summary>
      /// hàm thực hiện việc lấy thôn gtin đơn vị tính
      /// </summary>
      /// <returns></returns>
      public static DataTable LAYTHONGTIN_DonViTinh()
      {
          return THU_VIEN_CHUNG.LayDulieuDanhmucChung("DONVITINH", true);
      }
      public static DataTable LAYTHONGTIN_NHANVIEN()
      {
          return new Select().From(VDmucNhanvien.Schema).OrderAsc(VDmucNhanvien.Columns.TenNhanvien).ExecuteDataSet().Tables[0];
      }
      /// <summary>
      /// hàm thực hiện việc lấy thông tin cảu thuốc thuôc
      /// </summary>
      /// <param name="KieuThuocVT"></param>
      /// <returns></returns>
      public static DataTable LayThongTinThuoc(string KieuThuocVT)
      {
          DataTable dataTable = null;
          if (KieuThuocVT == "ALL")
              dataTable = new Select().From(VDmucThuoc.Schema).Where(VDmucThuoc.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
          else
              dataTable = new Select().From(VDmucThuoc.Schema).Where(VDmucThuoc.Columns.KieuThuocvattu).IsEqualTo(KieuThuocVT).And(VDmucThuoc.Columns.TrangThai).IsEqualTo(1).ExecuteDataSet().Tables[0];
          return dataTable;
      }
    
      /// <summary>
      /// hàm thực hiện việc lấy thông itn của thuốc
      /// </summary>
      /// <param name="id_kho"></param>
      /// <returns></returns>
      public static DataTable LayThongTinThuoc(int id_kho, string KieuThuocVT)
      {
          DataTable dataTable = SPs.DmucLaydanhsachthuoctrongkho(id_kho, KieuThuocVT).GetDataSet().Tables[0];
          foreach (DataRow drv in dataTable.Rows)
          {
              drv[DmucThuoc.Columns.TenThuoc] = string.Format("{0}-{1}", Utility.sDbnull(drv[DmucThuoc.Columns.IdThuoc]),
                                                          Utility.sDbnull(drv[DmucThuoc.Columns.TenThuoc]));
          }
          dataTable.AcceptChanges();
          return dataTable;
      }
      public static void LayThongTinLyDo()
      {
          globalVariables.gv_LyDoNhapKho = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LYDONHAPKHO", true); ;
      }
     
      
      /// <summary>
      /// hàm thực hiện việc lấy thông tin chỉ dẫn thêm
      /// 
      /// </summary>
      /// <returns></returns>
      public static DataTable LayThongTinChiDanThem()
      {
         return THU_VIEN_CHUNG.LayDulieuDanhmucChung("CDDT", true);
          
      }
      /// <summary>
      /// hàm thực hiện việc lấy thông tin cách dùng đơn thuốc
      /// </summary>
      /// <returns></returns>
      public static DataTable LayThongTinCachDung()
      {
          return THU_VIEN_CHUNG.LayDulieuDanhmucChung("CACHDUNG", true);
      }
      /// <summary>
      /// hàm thực hiện việc lấy uqna hệ đối tượng và kho thuốc
      /// </summary>
      /// <param name="MaDoiTuong"></param>
      /// <returns></returns>
      public static DataTable LaythongtinQuanheDoituongKho(string MaDoiTuong)
      {
          SqlQuery sqlQuery=new Select().From(TDmucKho.Schema);
          if(!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheDoituongKho.Columns.IdKho).From(QheDoituongKho.Schema).Where(QheDoituongKho.Columns.MaDoituongKcb)
                      .IsEqualTo(MaDoiTuong));

          }
         
          return sqlQuery.ExecuteDataSet().Tables[0];
      }
      public static DataTable LaythongtinQuanheDoituongKhoBanTaiQuay(string MaDoiTuong)
      {
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          sqlQuery.Where(TDmucKho.Columns.IdKho).In(
              new Select(QheDoituongKho.Columns.IdKho).From(QheDoituongKho.Schema).Where(QheDoituongKho.Columns.MaDoituongKcb)
                  .IsEqualTo(MaDoiTuong)).And(TDmucKho.Columns.LaQuaythuoc).IsEqualTo(1);
          if(!globalVariables.IsAdmin)
          {
              sqlQuery.And(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(
                      globalVariables.gv_intIDNhanvien));

          }
          sqlQuery.And(TDmucKho.Columns.LoaiKho).IsEqualTo(0);
          sqlQuery.And(TDmucKho.Columns.LaTuthuoc).IsEqualTo(0);
          return sqlQuery.ExecuteDataSet().Tables[0];
      }
      /// <summary>
      /// hàm thwucj hiện việc lấy thông tin quan hệ kho theo nhân viên
      /// </summary>
      /// <returns></returns>
      public static DataTable LayThongTinQuanHeKHoTheoNhanVien()
      {
          SqlQuery sqlQuery = new Select().From(TDmucKho.Schema);
          if (!globalVariables.IsAdmin)
          {
              sqlQuery.Where(TDmucKho.Columns.IdKho).In(
                  new Select(QheNhanvienKho.Columns.IdKho).From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien)
                      .IsEqualTo(globalVariables.gv_intIDNhanvien));

          }
          return sqlQuery.ExecuteDataSet().Tables[0];
      }
      /// <summary>
      /// hàm thực hiện việc kiêm tra số lượng tồn của kho
      /// </summary>
      /// <param name="IdKho"></param>
      /// <returns></returns>
      public static bool IsKiemTraTonKho(int IdKho)
      {
          TDmucKho objDKho = TDmucKho.FetchByID(IdKho);
          if (objDKho != null)
          {
              if (Utility.Int32Dbnull(objDKho.KtraTon) == 1) return true;
              else return false;
          }
          return false;
      }
      
      /// <summary>
      /// hàm thực hiện việc kiểm tra số lượng tồn trong kho thuốc
      /// </summary>
      /// <param name="idkho"></param>
      /// <param name="id_thuoc"></param>
      /// <returns></returns>
      public static decimal SoLuongTonTrongKho( long id_donthuoc,int idkho, int id_thuoc,long id_thuockho, int? Kiemtrachoxacnhan,byte noitru)
      {
          int soluongton = 0;

          StoredProcedure sp = SPs.ThuocKiemtrasoluongTonkho2023(id_donthuoc,id_thuoc, idkho,id_thuockho, Kiemtrachoxacnhan,noitru, soluongton);
          sp.Execute();
          soluongton = Utility.Int32Dbnull(sp.OutputValues[0], 0);


          return soluongton;
      }
     
    }
}
