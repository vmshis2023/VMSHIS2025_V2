using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using VMS.QMS.DAL;

namespace QMSService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        Logger _log;
        void InitLogs()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName =
                    "${basedir}/Mylogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
                config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
            }
        }
#region VMS
        [WebMethod]
        public DataTable VmsQmsLaysoQMSGoilai(string maquay, string makhoa, string loaiqms)
        {
            try
            {
                return SPs.VmsQmsLaysoQMSGoilai(maquay, makhoa, loaiqms).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public DataTable VmsQmsLaydanhsachbenhnhanchokham(string MaPhongKham, string makhoa, int sluong_hthi)
        {
            try
            {
                return SPs.VmsQmsLaydanhsachbenhnhanchokham(MaPhongKham, makhoa, sluong_hthi).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
          [WebMethod]
        public void ResetQMS(long id,string MaQuay, int trang_thai)
        {
            try
            {
                new Update(QmsTiepdon.Schema)
                   .Set(QmsTiepdon.Columns.TrangThai).EqualTo(trang_thai)
                   .Set(QmsTiepdon.Columns.MaQuay).EqualTo(MaQuay)
                   .Where(QmsTiepdon.Columns.Id).IsEqualTo(id).Execute();
            }
            catch (Exception)
            {
                
                throw;
            }
            
           
        }
          [WebMethod]
          public DataTable QMSPhongkham_get(string maPhong, DateTime ngayTao, int trangThai, string makhoakcb)
          {
              try
              {
                  InitLogs();
                  var dt = new DataTable();
                  dt =
                      SPs.QmsPhongkhamGetData(maPhong, ngayTao, makhoakcb, Utility.ByteDbnull(trangThai)).GetDataSet().Tables[0];
                  return dt;
              }
              catch (Exception ex)
              {
                  _log.Trace(ex.ToString());
                  return null;
              }
          }
          [WebMethod]
          public bool QMSPhongkham_CapnhatTrangthai(long id_kham, long id, int trangThai,int qmstype)
          {
              try
              {
                  InitLogs();
                  _log = LogManager.GetLogger("QMSLog");
                  _log.Trace("Start QMS log.......");
                  StoredProcedure sp = SPs.QmsCapnhattrangthai(id_kham, id, qmstype,Utility.ByteDbnull(trangThai));
                  int result = sp.Execute();
                  _log.Trace(string.Format("UpdateStatusQms with params idKham={0},id={1},trangThai={2} -->updated count:{4}", id_kham, id,  trangThai.ToString(), result.ToString()));
                  return true;
              }
              catch (Exception ex)
              {
                  _log.Trace(ex.ToString());
                  return false;
              }
          }

#endregion
        [WebMethod]
        public bool UpdateStatusQms(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        {
            try
            {
                InitLogs();
                _log = LogManager.GetLogger("QMSLog");
                _log.Trace("Start QMS log.......");
                StoredProcedure sp = SPs.QmsCanLamSangUpdateTrangThai(patientCode, maPhong, ngayTao,
                    Utility.ByteDbnull(trangThai));
             int result=   sp.Execute();
             _log.Trace(string.Format("UpdateStatusQms with params patientCode={0},maPhong={1},ngayTao={2},trangThai={3} -->updated count:{4}", patientCode, maPhong, ngayTao.ToString("dd/MM/yyyy"), trangThai.ToString(), result.ToString()));
                return true;
            }
            catch (Exception ex)
            {
               _log.Trace( ex.ToString());
                return false;
            }
        }
        
        [WebMethod]
        public int GetSoKhamQmsChucNang(string patientCode, string maPhong, DateTime ngayTao, int trangThai)
        {
            try
            {
                int soqms = 0;
                StoredProcedure sp = SPs.QmsCanLamSangGetSoQms(patientCode, maPhong, ngayTao,
                    Utility.ByteDbnull(trangThai), soqms);
                sp.Execute();
                soqms = Utility.Int32Dbnull(sp.OutputValues[0]);
                return soqms;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
        [WebMethod]
        public DataTable GetNoiThucHien()
        {
            try
            {
                InitLogs();
                var dt = new DataTable();
                dt = new Select().From(QmsPhongBan.Schema).ExecuteDataSet().Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        [WebMethod]
        public DataTable getQMSInfor(string patientcode,string maphong)
        {
            try
            {
                InitLogs();
                var dt = new DataTable();
                dt = SPs.QmsGetQMSInfor(patientcode,maphong).GetDataSet().Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
        [WebMethod]
        public bool ChangeQMSStatus(string patientcode, string maphong, string machucnang, string makhoa, byte _type)
        {
            try
            {
                InitLogs();
                SPs.QmsChangeQMSStatus(patientcode, maphong,machucnang,makhoa, _type).Execute();
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return false;
            }
        }
         [WebMethod]
        public bool DeleteQMS(int QMSID)
        {
            try
            {
                InitLogs();
                SPs.QmsDeleteQMS(QMSID).Execute();
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return false;
            }
        }
        [WebMethod]
        public bool ChangeQMSPriority(string patientcode, string maphong, byte tthai)
        {
            try
            {
                InitLogs();
                SPs.QmsChangeQMSPriority(patientcode, maphong, tthai).Execute();
                return true;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return false;
            }
        }
        [WebMethod]
        public DataTable GetListQmSbyMaPhong(string maPhong, string machucnang, DateTime ngayTao, int trangThai, string maKhoa)
        {
            try
            {
                InitLogs();
                var dt = new DataTable();
                dt =
                    SPs.QmsCanLamSangGetbyMaPhong(maPhong,machucnang, ngayTao, maKhoa, Utility.ByteDbnull(trangThai))
                        .GetDataSet()
                        .Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }

        [WebMethod]
        public DataTable GetListQmSbyMaPhongAll()
        {
            try
            {
                InitLogs();
                var dt = new DataTable();
                dt =
                    SPs.QmsCanLamSangGetbyMaPhong("XQ", "XQ", DateTime.Now, "XQ", 100)
                        .GetDataSet()
                        .Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }

        [WebMethod]
        public void InsertGoiLoa(string soKham, string quayGoi, string mayGoi, string maKhoa, int trangThai, byte loaiQMS, string nguoiTao, DateTime ngayTao, string mayTao, string loaGoi, string noiDung)
        {
            try
            {
                InitLogs();
                SPs.QmsGoiLoaInsert(soKham, quayGoi, mayGoi, maKhoa, Utility.ByteDbnull(trangThai), nguoiTao, ngayTao, mayTao, loaGoi,
                    noiDung, loaiQMS).Execute();
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
            }
        }
        [WebMethod]
        public DataTable _GetNoiThucHien()
        {
            try
            {
                var dt = new DataTable();
                dt = new Select().From(QmsPhongBan.Schema).ExecuteDataSet().Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                _log.Trace(ex.ToString());
                return null;
            }
        }
    }

    public class Utility
    {
        public static byte ByteDbnull(object obj)
        {
            if (!(((obj != null) && (obj != DBNull.Value)) && IsNumeric(obj)))
            {
                return 0;
            }
            return Convert.ToByte(obj);
        }
        public static Int32 Int32Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32(DefaultVal);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public static bool IsNumeric(object Value)
        {
            return Microsoft.VisualBasic.Information.IsNumeric(Value);
        }
        public static Int32 Int32Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32("0");
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}