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
namespace VNS.HIS.BusRule.Classes
{
    public class KCB_QMS
    {
        private NLog.Logger log;
        public KCB_QMS()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public void LaySoKhamQMS(string MaQuay, string MaKhoa, string madoituongkcb, ref int SoKham, ref int idDichvukcb, ref long idQMS, byte uutien, string loaiqms, string loaiqmsbo)
        {
            
            try
            {
                SoKham = 0;
                idDichvukcb = 0;
                StoredProcedure sp = SPs.QmsLayso(MaQuay, MaKhoa, madoituongkcb, SoKham, idDichvukcb, idQMS, uutien, loaiqms, loaiqmsbo);
                sp.Execute();
                SoKham = Utility.Int32Dbnull(sp.OutputValues[0]);
                idDichvukcb = Utility.Int32Dbnull(sp.OutputValues[1]);
                idQMS = Utility.Int64Dbnull(sp.OutputValues[2]);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        public void QmsCapnhat(long idqms,string MaQuay, int SoKham, string MaKhoa, string madoituongkcb, string ma_luotkham, long id_benhnhan, int id_phongkham, int id_kieukham, int id_khoakcb, int idDichvukcb, byte trang_thai, byte uutien, byte loaiqms)
        {
            try
            {
                StoredProcedure sp = SPs.QmsCapnhat(idqms, MaQuay, SoKham, MaKhoa, madoituongkcb, ma_luotkham, id_benhnhan, id_phongkham, id_kieukham, id_khoakcb, idDichvukcb, trang_thai, uutien, loaiqms);
                int num = sp.Execute();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
           
        }
        public void QmsPhongkhamDelete(int so_kham, string ma_phong, string ma_luotkham, long id_benhnhan, string ma_khoakcb, int id_phongkham, long id_kham)
        {
            try
            {
                StoredProcedure sp = SPs.QmsPhongkhamDelete(so_kham, ma_phong,id_benhnhan, ma_luotkham,  ma_khoakcb, id_phongkham, id_kham);
                int num = sp.Execute();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }


        }
        public void QmsPhongkhamInsert(int so_kham, string ma_phong, DateTime ngay_tao, DateTime ngay_tiepdon, string ma_luotkham, long id_benhnhan, string ten_benhnhan, int nam_sinh, int tuoi, string gioitinh, string ma_khoakcb, int id_phongkham, long id_kham, int id_dichvu_kcb, string ten_dichvu_kcb)
        {
            try
            {
                StoredProcedure sp = SPs.QmsPhongkhamInsert(so_kham, ma_phong, ngay_tao, ngay_tiepdon, id_benhnhan, ma_luotkham, ten_benhnhan, nam_sinh, tuoi, gioitinh, (byte)1, ma_khoakcb, id_phongkham, id_kham, id_dichvu_kcb, ten_dichvu_kcb, (byte)1);
                int num = sp.Execute();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           

        }
        public void QmsTiepdonCapnhattrangthai(string MaQuay, int SoKham, string MaKhoa, string madoituongkcb, string ma_luotkham, long id_benhnhan, byte trang_thai, byte loaiqms, byte uutien, byte action)
        {
            try
            {
                StoredProcedure sp = SPs.QmsTiepdonCapnhattrangthai(MaQuay, SoKham, MaKhoa, madoituongkcb, ma_luotkham, id_benhnhan, trang_thai, loaiqms, uutien, action);
                int num = sp.Execute();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
          

        }
        public DataTable QmsTiepdonLaysouutien(string MaKhoa, string madoituongkcb, byte trang_thai, byte loaiqms, byte uutien, byte action,string ma_quay,string loai_qms_bo)
        {
            try
            {
                StoredProcedure sp = SPs.QmsTiepdonLaysouutien(MaKhoa, madoituongkcb, trang_thai, loaiqms, uutien, action, ma_quay, loai_qms_bo);
                return sp.GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                return null;
                Utility.CatchException(ex);
            }
          
            
        }
        public void QmsTiepdonCapnhat(long idQMS, long id_benhnhan, string ma_luotkham, int id_kieukham, int id_phongkham)
        {

            StoredProcedure sp = SPs.QmsTiepdonCapnhat(idQMS, -1, ma_luotkham, id_benhnhan, id_phongkham, id_kieukham);
          int num=  sp.Execute();

        }
    }
}
