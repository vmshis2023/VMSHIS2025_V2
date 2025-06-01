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
    public class BAOCAO_BHYT
    {
        private NLog.Logger log;
        public BAOCAO_BHYT()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static bool BhytKiemtratruockhiHuyinphoiBHYT(long Id_benhnhan, string ma_luotkham)
        {
            int _outvalue = -1;
            StoredProcedure sp = SPs.BhytKiemtratruockhiHuyinphoiBHYT(Id_benhnhan, ma_luotkham, _outvalue);
            sp.Execute();
            _outvalue = Utility.Int32Dbnull(sp.OutputValues[0]);
            return _outvalue == 0;
        }

        public DataTable BHYT_80A(DateTime FromDate, DateTime ToDate, string ObjectTypeCode)
        {
            return SPs.Bhyt80a(FromDate, ToDate, ObjectTypeCode).GetDataSet().Tables[0];
        }

        public DataTable BHYT_79A(DateTime FromDate, DateTime ToDate, string ObjectTypeCode)
        {
            return SPs.Bhyt79a(FromDate, ToDate, ObjectTypeCode).GetDataSet().Tables[0];
        }
      
        public DataTable BHYT_14A_NEW(DateTime? FromDate, DateTime? ToDate, string ObjecType, string Nhom)
        {
            return SPs.Bhyt14a(FromDate, ToDate, ObjecType,
                                    Nhom).GetDataSet().Tables[0];
        }
        public DataTable BHYT_05A_TH(DateTime? FromDate, DateTime? ToDate, string ObjectTypeCode, string Nhom, int? TrangThai, string InsObjectCodeTP, int? ThatAo)
        {
            return SPs.Bhyt05aTh(FromDate, ToDate,
                                         ObjectTypeCode,
                                         Nhom, TrangThai,
                                         InsObjectCodeTP,
                                 -1).GetDataSet().Tables[0];
        }
        public DataTable BHYT_05A_CT(DateTime? FromDate, DateTime? ToDate, string ObjectTypeCode, int? Type, int? DungTuyen, string InsClinicCOde, string SurveysCode)
        {
            return SPs.Bhyt05aCt(FromDate, ToDate,
                                         ObjectTypeCode, Type,
                                         DungTuyen,
                                         InsClinicCOde, SurveysCode).GetDataSet().Tables[0];
                                            

        }
        public DataTable BHYT_25A_TH(DateTime? FromDate, DateTime? ToDate, string DoiTuong, int? LoaiBHYT)
        {
            return SPs.Bhyt25aTh(FromDate, ToDate,
                                        DoiTuong,
                                        LoaiBHYT).GetDataSet().Tables[0];
        }
        public DataTable BHYT_25A_CT(DateTime? FromDate, DateTime? ToDate, string ObjectTypeCode, int? _Type, int? DungTuyen, string NgoaiTuyen, string InsObjectTP, int? NTNT)
        {
            return SPs.Bhyt25aCt(FromDate, ToDate,
                                            ObjectTypeCode, _Type, DungTuyen,
                                            NgoaiTuyen,
                                            InsObjectTP, NTNT).GetDataSet().Tables[0];
        }
        public DataTable BHYT_20A(string fromDate, string toDate, string ObjectTypeCode, int? NTNT, int? DrugID,
            short? DrugTypeID, string Nhom, int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI, int? BacSyCD)
        {
            return SPs.Bhyt20a(fromDate, toDate, ObjectTypeCode,
                                          NTNT, DrugID, DrugTypeID,Nhom,Tuyen,InsClinicCode,InsObjectCodeTP,
                                          MaDKKCB, TRANGTHAI, BacSyCD).
                    GetDataSet().Tables[0];
        }
        public DataTable BHYT_20A_PHANTUYEN(string fromDate, string toDate, string ObjectTypeCode, int? NTNT, int? DrugID,
         short? DrugTypeID, string Nhom, int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI, int? BacSyCD)
        {
            return SPs.Bhyt20aPhantuyen(fromDate, toDate, ObjectTypeCode,
                                          NTNT, DrugID, DrugTypeID, Nhom, Tuyen, InsClinicCode, InsObjectCodeTP,
                                          MaDKKCB, TRANGTHAI, BacSyCD).
                    GetDataSet().Tables[0];
        }
        public DataTable BHYT_19A(string fromDate, string toDate, string ObjectTypeCode, int? NTNT, int? DrugID,
          short? DrugTypeID, string Nhom, int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI, int? BacSyCD)
        {
            return SPs.Bhyt19a(fromDate, toDate, ObjectTypeCode,
                                          NTNT, DrugID, DrugTypeID, Nhom, Tuyen, InsClinicCode, InsObjectCodeTP,
                                          MaDKKCB, TRANGTHAI, BacSyCD).
                    GetDataSet().Tables[0];
        }
        public DataTable BHYT_19A_PHANTUYEN(string fromDate, string toDate, string ObjectTypeCode, int? NTNT, int? DrugID,
         short? DrugTypeID, string Nhom, int? Tuyen, string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI, int? BacSyCD)
        {
            return SPs.Bhyt19aPhantuyen(fromDate, toDate, ObjectTypeCode,
                                          NTNT, DrugID, DrugTypeID, Nhom, Tuyen, InsClinicCode, InsObjectCodeTP,
                                          MaDKKCB, TRANGTHAI, BacSyCD).
                    GetDataSet().Tables[0];
        }
        public DataTable BHYT_21A(DateTime? fromDate, DateTime? toDate, string ObjectTypeCode, int? NTNT, string Nhom, int? Tuyen, 
            string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI)
        {
            return SPs.Bhyt21a(fromDate, toDate, ObjectTypeCode, NTNT,
                                             Nhom, Tuyen, InsClinicCode,
                                             InsObjectCodeTP, MaDKKCB,
                                             TRANGTHAI).GetDataSet().
                    Tables[0];
        }

        public DataTable BHYT_21A_PHANTUYEN(DateTime? fromDate, DateTime? toDate, string ObjectTypeCode, int? NTNT, string Nhom, int? Tuyen,
           string InsClinicCode, string InsObjectCodeTP, string MaDKKCB, string TRANGTHAI)
        {
            return SPs.Bhyt21aPhantuyen(fromDate, toDate, ObjectTypeCode, NTNT,
                                             Nhom, Tuyen, InsClinicCode,
                                             InsObjectCodeTP, MaDKKCB,
                                             TRANGTHAI).GetDataSet().
                    Tables[0];
        }
        
        
    }
}
