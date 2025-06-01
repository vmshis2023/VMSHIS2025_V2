using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_HOADONDO
    {
       /// <summary>
       /// hàm thực hiệnv iệc update thông tin của biên lại hóa đơn 
       /// </summary>
       /// <param name="objhoalog"></param>
       /// <param name="objPayment"></param>
       /// <param name="hoadonCapphatID"></param>
       /// <returns></returns>
       public ActionResult UpdateBienLaiHoaDon(HoadonLog objhoalog, KcbThanhtoan objPayment, int hoadonCapphatID)
       {
           try
           {
               using (var Scope = new TransactionScope())
               {
                   using (var dbScope = new SharedDbConnectionScope())
                   {
                       objhoalog.IdThanhtoan = Utility.sDbnull(objPayment.IdThanhtoan);
                       objhoalog.IdBenhnhan = Utility.Int32Dbnull(objPayment.IdBenhnhan);
                       objhoalog.MaLuotkham = Utility.sDbnull(objPayment.MaLuotkham);
                       objhoalog.MaNhanvien = globalVariables.UserName;
                       objhoalog.NgayIn = DateTime.Now;
                       objhoalog.TrangThai = 0;
                       objhoalog.IsNew = true;
                       objhoalog.Save();
                       new Update(HoadonCapphat.Schema)
                           .Set(HoadonCapphat.Columns.SerieHientai).EqualTo(objhoalog.Serie)
                           .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                           .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(hoadonCapphatID).
                           Execute();
                   }
                   Scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {

               return ActionResult.Error;
           }
           
       }
       public static int DeleteRedInVoice(int idHdonLog)
       {
          return new Delete().From(HoadonLog.Schema)
                                               .Where(HoadonLog.Columns.IdHdonLog)
                                               .IsEqualTo(idHdonLog)
                                               .Execute();
       }
       public static int UpdateTrangThaiHoaDon(int idHdonLog)
       {
           
           return 
               new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1).Where(
                             HoadonLog.Columns.IdHdonLog).IsEqualTo(idHdonLog).
                             Execute();
       }
       
       public static void LoadHoaDonDo(ref string mahoadon, ref string kihieu, ref string maQuyen, ref string seri, ref int hoaMauID, ref string error, int status)
       {
           HoadonMau objHoadonMau = HoadonMau.FetchByID(hoaMauID);
           if (objHoadonMau != null)
           {
               var sp1 = SPs.SinhSysHoadonMau(hoaMauID, objHoadonMau.MauHoadon, objHoadonMau.KiHieu,
                   objHoadonMau.MaQuyen, "BV01", 1);
               DataTable histemp = sp1.GetDataSet().Tables[0];
               if (histemp.Rows.Count > 0)
               {
                   mahoadon = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MauHoadon]);
                   seri = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.SerieHientai]);
                   kihieu = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.KiHieu]);
                   maQuyen = Utility.sDbnull(histemp.Rows[0][SysHoadonMau.Columns.MaQuyen]);

               }
           }
       }
       public static DataTable Red_LayHoaDonDo(string macoso, string mabenhvien, string username)
       {
           return SPs.SpGetHoaDonCapPhat(username, macoso, mabenhvien).GetDataSet().Tables[0];
       }
       public static int RedInsertHoaDonLog(HoadonLog objhoalog, string macoso, string mabenhvien, int status)
       {

           objhoalog.MaNhanvien = globalVariables.UserName;
           objhoalog.NgayIn = DateTime.Now;
           objhoalog.DaGui = 0;
           objhoalog.TrangThai = 0;
           var sp = SPs.VienPhiUpdateHoaDonLog(Utility.Int32Dbnull(objhoalog.IdHdonLog),
               Utility.Int32Dbnull(objhoalog.IdCapphat), Utility.Int32Dbnull(objhoalog.IdThanhtoan),
               objhoalog.TongTien, Utility.Int32Dbnull(objhoalog.IdBenhnhan), objhoalog.MaLuotkham, objhoalog.MauHoadon,
               objhoalog.KiHieu,
               objhoalog.MaQuyen, objhoalog.Serie, objhoalog.MaNhanvien, objhoalog.MaLydo, objhoalog.NgayIn,
                Utility.Int16Dbnull(objhoalog.TrangThai), Utility.Int16Dbnull(objhoalog.SolanIn), objhoalog.InGop, objhoalog.DaGui,
               Utility.sDbnull(objhoalog.QrDatacode), status,globalVariables.gv_strIPAddress,globalVariables.gv_strMacAddress);
           int record = sp.Execute();
           objhoalog.IdHdonLog = Utility.Int32Dbnull(sp.OutputValues[0]);
           return record;
       }
       public static void InsertHoaDonDo(HoadonLog objhoalog, KcbThanhtoan objPayment, int hoadonCapphatID, int status)
        {
            if (string.IsNullOrEmpty(objhoalog.Serie))
            {
                string mahoadon = string.Empty;
                string kihieu = string.Empty;
                string ma_quyen = string.Empty;
                string serie = string.Empty;
                // int HoaDon_Mau_ID = Utility.Int32Dbnull(grdHoaDonCapPhat.GetValue(LHoadonMau.Columns.HdonMauId));
                string error = string.Empty;
                LoadHoaDonDo(ref mahoadon, ref kihieu, ref ma_quyen, ref serie, ref hoadonCapphatID, ref error, -1);
                objhoalog.MaQuyen = ma_quyen;
                objhoalog.KiHieu = kihieu;
                objhoalog.Serie = serie;
                objhoalog.MauHoadon = mahoadon;
                objhoalog.IdCapphat = hoadonCapphatID;
            }
            if (!string.IsNullOrEmpty(objhoalog.Serie))
            {
                objhoalog.IdThanhtoan = Utility.sDbnull(objPayment.IdThanhtoan);
                objhoalog.IdBenhnhan = Utility.Int32Dbnull(objPayment.IdBenhnhan);
                objhoalog.MaLuotkham = Utility.sDbnull(objPayment.MaLuotkham);
                objhoalog.MaNhanvien = globalVariables.UserName;
                objhoalog.NgayIn = DateTime.Now;
                objhoalog.TrangThai = 0;
                int record = RedInsertHoaDonLog(objhoalog, "BV01", "", status);
                if (record <= 0)
                {
                    return;
                }
                else
                {
                    new Update(HoadonCapphat.Schema)
                        .Set(HoadonCapphat.Columns.SerieHientai).EqualTo(objhoalog.Serie)
                        // .Set(LHoadonMau.Columns.TrangThai).EqualTo(1)
                        .Where(HoadonCapphat.Columns.IdHoadonMau).IsEqualTo(hoadonCapphatID).
                        Execute();
                }
            }
        }
    }
}
