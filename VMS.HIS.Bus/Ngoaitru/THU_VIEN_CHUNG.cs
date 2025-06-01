using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using NLog;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace VNS.Libs
{
    public class THU_VIEN_CHUNG
    {
        public static DataTable LayDsach_DoiTuong()
        {
            return SPs.LObjectTypeGetList().GetDataSet().Tables[0];
        }
        public static DataTable LayDsach_DoiTuong(string MaDoiTuong)
        {
            return new Select().From(LObjectType.Schema)
                .Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo(MaDoiTuong).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayDsach_DoiTuong(List< string> lstMaDoiTuong)
        {
            return new Select().From(LObjectType.Schema)
                .Where(LObjectType.Columns.ObjectTypeCode).In(lstMaDoiTuong).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinDonViTinh()
        {
            SqlQuery sqlQuery = new Select().From(LMeasureUnit.Schema)
                .OrderAsc(LMeasureUnit.Columns.IntOrder);
            return sqlQuery.ExecuteDataSet().Tables[0];
        }
        public static DataTable LoadDataServiceDetail(string MaDoiTuong, int ID_GoiDV)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = SPs.DetmayGetDataServiceDetail(MaDoiTuong, ID_GoiDV).GetDataSet().Tables[0];
                if (!dataTable.Columns.Contains("UnSignedService_Name"))
                    dataTable.Columns.Add("UnSignedService_Name", typeof(string));
                if (!dataTable.Columns.Contains("UnSignedServiceDetail_Name"))
                    dataTable.Columns.Add("UnSignedServiceDetail_Name", typeof(string));
                foreach (DataRow drv in dataTable.Rows)
                {
                    drv["UnSignedService_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                    drv["UnSignedServiceDetail_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                }
                dataTable.AcceptChanges();
            }
            catch (Exception)
            {
            }
            return dataTable;
        }
        public static DataTable LaySoTayMaBenh(string userName, LoaiChanDoan loaiChanDoan)
        {
            DataTable m_NN = new DataTable();
            m_NN = SPs.SoTayMaBenh(userName, (int?)loaiChanDoan).GetDataSet().Tables[0];
            return m_NN;
        }
        public static DataTable LayThongTin_KhoaNgoaiTru()
        {
            return new Select().From(LDepartment.Schema)
                .Where(LDepartment.Columns.DeptType).IsEqualTo(0)
                .AndExpression(LDepartment.Columns.ParentId).IsEqualTo(-1).Or(LDepartment.Columns.ParentId).IsNull().Or(
                    LDepartment.Columns.ParentId).IsEqualTo(0).CloseExpression()
                .And(LDepartment.Columns.Speciality).IsEqualTo(1).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinPhong()
        {
            return new Select().From(LDepartment.Schema)
                .WhereExpression(LDepartment.Columns.ParentId)
                .IsNotEqualTo(-1)
                .And(LDepartment.Columns.ParentId)
                .IsNotNull()
                .And(LDepartment.Columns.ParentId)
                .IsNotEqualTo(0)
                .CloseExpression()
                .OrderAsc(LDepartment.Columns.IntOrder).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTin_PhongKhamNgoaiTru()
        {
            SqlQuery sqlQuery = new Select(LDepartment.Columns.DepartmentId).From(LDepartment.Schema)
                .Where(LDepartment.Columns.DeptType).IsEqualTo(0)
                .AndExpression(LDepartment.Columns.ParentId).IsEqualTo(-1).Or(LDepartment.Columns.ParentId).IsNull().Or(
                    LDepartment.Columns.ParentId).IsEqualTo(0).CloseExpression()
                .And(LDepartment.Columns.Speciality).IsEqualTo(1);
            SqlQuery sqlQuery1 = new Select().From(LDepartment.Schema)
                .Where(LDepartment.Columns.ParentId).In(sqlQuery);
            return sqlQuery1.ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinGoi()
        {
            return SPs.TamphucDanhmucThongtinGoiDvu(-1).GetDataSet().Tables[0];
        }
        public static DataTable LayThongTinKhoa()
        {
            return new Select().From(LDepartment.Schema)
                .Where(LDepartment.Columns.ParentId).IsEqualTo(0)
                .OrderAsc(LDepartment.Columns.IntOrder).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinBenhVien()
        {
            return SPs.LBenhVienGetList().GetDataSet().Tables[0];
        }
        public static DataTable Get_PHONGKHAM(string MA_DTUONG)
        {
           return  SPs.LaydsachPhongkham(MA_DTUONG, globalVariables.DepartmentID_TheoMay).GetDataSet().Tables[0];
        }

        public static DataTable Get_KIEUKHAM(string MA_DTUONG)
        {
            return    SPs.LaydsachKieukham(MA_DTUONG, globalVariables.DepartmentID_TheoMay).GetDataSet().Tables[0];
        }
        public static DataTable LayDsach_Dvu_KCB(string MA_DTUONG)
        {
           return SPs.LaythongtinDvuKcb(MA_DTUONG, globalVariables.MA_KHOA_THIEN).GetDataSet()
                        .Tables[0];
        }
        public static TThongtinGoiDvuBnhan LayThongTinGoiNgoaiTru(TPatientExam objPatientExam)
        {
            SqlQuery sqlQuery = new Select().From(TThongtinGoiDvuBnhan.Schema)
                .Where(TThongtinGoiDvuBnhan.Columns.PatientCode).IsEqualTo(objPatientExam.PatientCode)
                .And(TThongtinGoiDvuBnhan.Columns.PatientId).IsEqualTo(objPatientExam.PatientId)
                .And(TThongtinGoiDvuBnhan.Columns.NoiTru).IsEqualTo(0);
            TThongtinGoiDvuBnhan objThongtinGoiDvuBnhan = sqlQuery.ExecuteSingle<TThongtinGoiDvuBnhan>();
            return objThongtinGoiDvuBnhan;

        }
        /// <summary>
        /// hàm thực hiện việc kiêm tra số lượng tồn của kho
        /// </summary>
        /// <param name="IdKho"></param>
        /// <returns></returns>
        public static bool IsKiemTraTonKho(int IdKho)
        {
            DKho objDKho = DKho.FetchByID(IdKho);
            if (objDKho != null)
            {
                if (Utility.Int32Dbnull(objDKho.KtraTon) == 1) return true;
                else return false;
            }
            return false;
        }
        /// <summary>
        /// lấy thông tin của bác sỹ ngoại trú
        /// </summary>
        /// <returns></returns>
        public static DataTable LAYTHONGTIN_BACSY_NGOAITRU()
        {
            return SPs.TamphucLaythongTinVien(globalVariables.gv_BacSyNgoaitru).GetDataSet().Tables[0];
        }
        /// <summary>
        /// Creates an object wrapper for the DANHMUC_LAYTHONGTIN_NHANVIEN_TAICHINH Procedure
        /// </summary>
        public static StoredProcedure DanhmucLaythongtinNhanvienTaichinh()
        {
            var sp = new StoredProcedure("DANHMUC_LAYTHONGTIN_NHANVIEN_TAICHINH", DataService.GetInstance("ORM"), "");

            return sp;
        }

        /// <summary>
        /// hàm thực hiện việc lấy thông tin của khoa tài chính
        /// </summary>
        /// <returns></returns>
        public static DataTable LayThongTinNguoiDungTaiChinh()
        {
            DataTable dataTable = DanhmucLaythongtinNhanvienTaichinh().GetDataSet().Tables[0];

            return dataTable;
        }
        public static DataTable LayThongTinNhanVien()
        {
            SqlQuery sqlQuery = new Select().From(LStaff.Schema).OrderAsc(LStaff.Columns.StaffName);

            return sqlQuery.ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTin_Service()
        {
            try
            {
                return SPs.DanhmucLoadLoaiDvu().GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable GetThongTinDoiTuong()
        {
            return
                new Select().From(LObjectType.Schema).OrderAsc(LObjectType.Columns.IntOrder).ExecuteDataSet().Tables[0];
        }
        /// <summary>
        /// lấy thông tin của nhân viên thuộc khoa nào
        /// </summary>
        /// <returns></returns>
        public static DataTable LayThongTin_Nhanvien_ThuocKhoa(int Department_Id)
        {
            return new Select().From(LStaff.Schema)
                .Where(LStaff.Columns.DepartmentId).IsEqualTo(Department_Id).ExecuteDataSet().Tables[0];
        }

        public static DataTable LayThongTin_Nhanvien_ThuocKhoaNgoaiTru()
        {
            return new Select().From(LStaff.Schema).ExecuteDataSet().Tables[0];
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của kiểu dữ liệu
        /// </summary>
        /// <returns></returns>
        public static DataTable LayThongTin_ServiceType()
        {
            try
            {
                return SPs.DanhmucLoadServiceType().GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable LayThongTin_NhomMau()
        {
            return SPs.LaokhoaGetNhommau().GetDataSet().Tables[0];
        }
        public static DataTable LoadDataServiceDetail(string MaDoiTuong, int ID_GoiDV, string MaKhoaThien)
        {
            var dataTable = new DataTable();
            try
            {
                if (!globalVariables.gv_ChoPhepMaKhoaThucHien)
                    dataTable = SPs.DetmayGetDataServiceDetail(MaDoiTuong, ID_GoiDV).GetDataSet().Tables[0];
                else
                {
                    dataTable =
                        SPs.LaokhoaGetDataServiceDetail(MaDoiTuong, ID_GoiDV, MaKhoaThien).GetDataSet().Tables[0];
                }
                if (!dataTable.Columns.Contains("UnSignedService_Name"))
                    dataTable.Columns.Add("UnSignedService_Name", typeof(string));
                if (!dataTable.Columns.Contains("UnSignedServiceDetail_Name"))
                    dataTable.Columns.Add("UnSignedServiceDetail_Name", typeof(string));
                foreach (DataRow drv in dataTable.Rows)
                {
                    drv["UnSignedService_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                    drv["UnSignedServiceDetail_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                }
                dataTable.AcceptChanges();
            }
            catch (Exception)
            {
            }
            return dataTable;
        }
        public static DataTable LayThongTin_DONVI_TINH()
        {
            try
            {
                return SPs.DanhmucLoadDonviTinh().GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public static DataTable LayThongTinBacSy()
        {
            return SPs.DetmayNoitruLoadDataBacSy(globalVariables.DepartmentID, "BACSY").GetDataSet().Tables[0];
        }
        public static DataTable LayThongTinNhomThuoc(int PreType)
        {
            return SPs.TamphucLaythongtinNhomthuoc(PreType).GetDataSet().Tables[0];
        }
        public static DataTable LayThongTinPhongCapDuoi(int Department_ID)
        {
            return new Select().From(LDepartment.Schema)
                .Where(LDepartment.Columns.ParentId).IsEqualTo(Department_ID)
                .OrderAsc(LDepartment.Columns.IntOrder).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinQuyenNhanVien()
        {
            SqlQuery sqlQuery = new Select().From(DmucChung.Schema)
                .Where(DmucChung.Columns.Loai).IsEqualTo("QUYEN_NHAN_VIEN");
            return sqlQuery.ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinBacChiDinh()
        {
            SqlQuery sqlQuery = new Select().From(LStaff.Schema)
                .Where(LStaff.Columns.StaffTypeId).In(
                    new Select(LStaffType.Columns.StaffTypeId).From(LStaffType.Schema).Where(LStaffType.Columns.SDesc).
                        IsEqualTo("BACSY"));
            return sqlQuery.ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTin_DrugType()
        {
            try
            {
                return SPs.DanhmucLoadDrugtype().GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable LayThongTinNhanVienByStaffID(short Staff_ID)
        {
            SqlQuery sqlQuery = new Select().From(LStaff.Schema)
                .Where(LStaff.Columns.StaffId).IsEqualTo(Staff_ID)
                .OrderAsc(LStaff.Columns.StaffName);

            return sqlQuery.ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTin_BacSy()
        {
            return
                new Select().From(LStaff.Schema).Where(LStaff.StaffTypeIdColumn).IsEqualTo(1).Or(
                    LStaff.StaffTypeIdColumn).IsEqualTo(5).ExecuteDataSet().Tables[0];
        }
        public static DataTable GetThongTinDichVu()
        {
            return new Select().From(LService.Schema).OrderAsc(LService.Columns.IntOrder).ExecuteDataSet().Tables[0];
        }
        public static DataTable LayThongTinThuoc()
        {
            DataTable m_dtDrugDataSource =
                new Select().From(LDrug.Schema).OrderAsc(LDrug.Columns.DrugName).ExecuteDataSet().Tables[0];
            if (!m_dtDrugDataSource.Columns.Contains("UnSignedDrug_Name"))
                m_dtDrugDataSource.Columns.Add("UnSignedDrug_Name", typeof(string));
            foreach (DataRow dr in m_dtDrugDataSource.Rows)
            {
                dr["UnSignedDrug_Name"] = Utility.UnSignedCharacter(dr["Drug_Name"].ToString());
            }
            m_dtDrugDataSource.AcceptChanges();
            return m_dtDrugDataSource;
            return m_dtDrugDataSource;
        }
     

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC LẤY THÔNG TIN KHOA NGOẠI TRÚ
        /// </summary>
        /// <returns></returns>
        public static DataTable LayThongTin_KhoaNGOAITRU()
        {
            return SPs.LaokhoaDanhmucLaythongtinKhoaNgoaitru().GetDataSet().Tables[0];
        }
        public static DataTable LayThongTin_KhoThuoc()
        {
            DataTable dataTable =
                new Select().From(LStock.Schema).OrderAsc(LStock.Columns.IntOrder).ExecuteDataSet().Tables[0];
            if (!dataTable.Columns.Contains("Stock_category_Name"))
                dataTable.Columns.Add("Stock_category_Name", typeof(string));
            foreach (DataRow drv in dataTable.Rows)
            {
                if (drv["Stock_category"].ToString() == "0") drv["Stock_category_Name"] = "Kiểu kho thuốc";
                if (drv["Stock_category"].ToString() == "1") drv["Stock_category_Name"] = "Kiểu kho vật tư";
            }
            dataTable.AcceptChanges();
            return dataTable;
        }
        public static DataTable LAYTHONGTIN_BACSY()
        {
            return SPs.TamphucLaythongTinVien(globalVariables.gv_BacSyNgoaitru).GetDataSet().Tables[0];
        }

        /// <summary>
        /// hàm thực hiện việc lấy thông tin bác sỹ theo khoa chỉ định
        /// </summary>
        /// <param name="Department_ID"></param>
        /// <returns></returns>
        public static DataTable LAYTHONGTIN_BACSYTheoKhoaChiDinh(int Department_ID)
        {
            SqlQuery sqlQuery = new Select().From(LStaff.Schema)
                .Where(LStaff.Columns.DepartmentId).IsEqualTo(Department_ID).OrderAsc(LStaff.Columns.StaffName);
            return sqlQuery.ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// hàm thực hienj việc lấy thông tin của bác sỹ nội trú
        /// </summary>
        /// <returns></returns>
        public static DataTable LAYTHONGTIN_BACSY_NOITRU()
        {
            return SPs.TamphucLaythongTinVien(globalVariables.gv_BacSyNoitru).GetDataSet().Tables[0];
        }
        public static string GeneratePresName(string v_PatientCode, int v_Patient_Id)
        {

            string v_Pres_Name = "";
            v_Pres_Name = Utility.sDbnull(SPs.DrugGetMaxPresName(v_PatientCode, v_Patient_Id).ExecuteScalar(), "");
            return v_Pres_Name;
        }
        
        public static short LaySothutuKCB(int Department_ID)
        {
            short So_kham = 0;
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KcbTiepdonLaysothutuKcb(Department_ID, globalVariables.SysDate).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                So_kham = (short)(Utility.Int16Dbnull(dataTable.Rows[0]["So_Kham"], 0) );
            }
            else
            {
                So_kham = 1;
            }
            return So_kham;
        }
        public static DataTable LayDmucDiachinh()
        {
            DataTable m_dtDataThanhPho = SPs.DanhmucLoadDiachinh().GetDataSet().Tables[0];
            try
            {

            }
            catch (Exception)
            {

                //throw;
            }
            return m_dtDataThanhPho;
            //return L
        }
        public static int SoKhamTrongThang_BH(int ObjectTypeType)
        {
            int So_ThuTu = 0;
            DataTable dataTable = new DataTable();
            dataTable =
                SPs.KydongSothutuTiepdonBh(BusinessHelper.GetSysDateTime(), Utility.Int32Dbnull(ObjectTypeType, -1), GetThamSo_ThuTu()).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                So_ThuTu = Utility.Int32Dbnull(dataTable.Rows[0]["So_ThuTu"], 0) + 1;
            }
            else
            {
                So_ThuTu = 1;
            }
            return So_ThuTu;
        }
        public static string GetThamSo_ThuTu()
        {
            string thamso = "MONTH";
            SqlQuery sqlQuery =
                new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                    "ORDEROBJECT");
            SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) thamso = objSystemParameter.SValue;
            return thamso;
        }
        public static decimal DiscountRate(decimal PhanTramBH, decimal Origin_Price)
        {
            return PhanTramBH * Origin_Price / 100;
        }
        public static decimal DiscountRate(decimal PhanTramBH, decimal Origin_Price, int IsPayment)
        {
            if (IsPayment == 0)
                return PhanTramBH * Origin_Price / 100;
            else
            {
                return 0;
            }
        }
        public static decimal DiscountPrice(decimal PhanTramBH, decimal Origin_Price)
        {
            return (100 - PhanTramBH) * Origin_Price / 100;
        }
        public static decimal DiscountPrice(decimal PhanTramBH, decimal Origin_Price, int IsPayment)
        {
            if (IsPayment == 0)
                return (100 - PhanTramBH) * Origin_Price / 100;
            else
            {
                return Origin_Price;
            }
        }
        public static string GeneratePaymentCode(DateTime dateTime, int Hos_Status)
        {
            DataTable dataTable = new DataTable();
            dataTable = SPs.YhhqGeneratePaymentCode(dateTime, Hos_Status).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) return dataTable.Rows[0]["Payment_Code"].ToString();
            else
            {
                return "";
            }
        }
        public static bool IsBaoHiem(int ObjectType_Id)
        {
            LObjectType objectType = LObjectType.FetchByID(ObjectType_Id);
            if (objectType != null)
            {
                if (objectType.ObjectTypeType == 0) return true;
                else return false;
            }
            return true;

        }
        public static string LaySoBenhAn()
        {
            string SoBenhAn = "";
            DataTable dataTable = SPs.GetMedicalNumber(BusinessHelper.GetSysDateTime()).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0) SoBenhAn = Utility.sDbnull(dataTable.Rows[0]["SoBA"], string.Empty);

            return SoBenhAn;
        }
        public static string KCB_SINH_MALANKHAM()
        {
            string MaxPatientCode = "";
            StoredProcedure sp = SPs.KcbSinhMalankham(MaxPatientCode);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                MaxPatientCode = (String)objOutput;
            });
            return MaxPatientCode;
        }
        public static decimal TinhPtramBHYT(TPatientExam ObjPatientExam)
        {
            decimal ptramBhyt = 0;
            try
            {
                if (string.IsNullOrEmpty(ObjPatientExam.InsuranceNum)) return ptramBhyt;
                LObjectType objObjectType = LObjectType.FetchByID(ObjPatientExam.ObjectTypeId);
                if (!string.IsNullOrEmpty(ObjPatientExam.InsClinicCode))
                {
                    if (ObjPatientExam.CorrectLine == 1)//Đúng tuyến
                    {
                        if (objObjectType != null)
                        {
                            if (ObjPatientExam.InsObjectCodeNumber == 1 || ObjPatientExam.InsObjectCodeNumber == 2)//Đối tượng 1,2 hưởng 100%
                            {
                                ptramBhyt = 100;
                            }
                            else
                            {
                                SqlQuery sqlQuery = new Select(LInsuranceObject.Columns.Percent).From(LInsuranceObject.Schema)
                                .Where(LInsuranceObject.Columns.InsObjectCode).IsEqualTo(
                                    Utility.sDbnull(ObjPatientExam.InsObjectCode));
                                LInsuranceObject objInsuranceObject = sqlQuery.ExecuteSingle<LInsuranceObject>();
                                if (objInsuranceObject != null)
                                {
                                    ptramBhyt = Utility.DecimaltoDbnull(objInsuranceObject.Percent, 0);
                                }
                                else
                                {
                                    ptramBhyt = 100;
                                }
                            }


                        }
                    }
                    else//Trái tuyến
                    {
                        if (objObjectType != null)
                        {
                            ptramBhyt = Utility.DecimaltoDbnull(objObjectType.DiscountDiscorrectLine, 0);
                        }
                    }
                }
                else//Dịch vụ
                {
                    SqlQuery sqlQuery =
                        new Select().From(LObjectType.Schema).Where(LObjectType.Columns.ObjectTypeCode).IsEqualTo("DV");
                    objObjectType = sqlQuery.ExecuteSingle<LObjectType>();
                    if (objObjectType != null) ptramBhyt = Utility.DecimaltoDbnull(objObjectType.DiscountCorrectLine);
                    else ptramBhyt = 0;
                    // ptramBhyt = objObjectType.DiscountCorrectLine;
                }



            }
            catch (Exception exception)
            {
                ptramBhyt = 0;
            }
            return ptramBhyt;
        }
       
        public static TPaymentDetail[] TinhPhamTramBHYT(TPaymentDetail[] arrPaymentDetail, TPatientExam objPatientExam, decimal v_DiscountRate)
        {
            string IsDungTuyen = "DT";
            LObjectType objectType = LObjectType.FetchByID(objPatientExam.ObjectTypeId);
            if (objectType != null)
            {
                switch (objectType.ObjectTypeCode)
                {
                    case "BHYT":
                        if (Utility.Int32Dbnull(objPatientExam.CorrectLine, "0") == 1) IsDungTuyen = "DT";
                        else
                        {
                            IsDungTuyen = "TT";
                        }
                        break;
                    default:
                        IsDungTuyen = "KHAC";
                        break;
                }

            }
            foreach (TPaymentDetail objPaymentDetail in arrPaymentDetail)
            {
                if (objPaymentDetail.IsPayment == 0)//Có thể tính cho BHYT
                {
                    SqlQuery sqlQuery = new Select().From(LInsDetailDiscountRate.Schema)
                        .Where(LInsDetailDiscountRate.Columns.ServiceDetailId).IsEqualTo(objPaymentDetail.ServiceDetailId)
                        .And(LInsDetailDiscountRate.Columns.PaymentTypeId).IsEqualTo(objPaymentDetail.PaymentTypeId)
                        .And(LInsDetailDiscountRate.Columns.InsType).IsEqualTo(IsDungTuyen)
                        .And(LInsDetailDiscountRate.Columns.ObjectTypeCode).IsEqualTo(objPatientExam.MaDoiTuong);
                    LInsDetailDiscountRate objDetailDiscountRate = sqlQuery.ExecuteSingle<LInsDetailDiscountRate>();
                    if (objDetailDiscountRate != null)
                    {
                        objPaymentDetail.MaDv = Utility.sDbnull(objPatientExam.MaDoiTuong);
                        objPaymentDetail.PTramBh = (int?)objDetailDiscountRate.DiscountRate;
                        objPaymentDetail.DiscountRate = DiscountRate(objDetailDiscountRate.DiscountRate,
                                                      Utility.DecimaltoDbnull(
                                                          objPaymentDetail.OriginPrice, 0));
                        objPaymentDetail.DiscountPrice = DiscountPrice(objDetailDiscountRate.DiscountRate,
                                                                 Utility.DecimaltoDbnull(
                                                                     objPaymentDetail.OriginPrice, 0));
                    }
                    else
                    {
                        objPaymentDetail.MaDv = Utility.sDbnull(objPatientExam.MaDoiTuong);
                        objPaymentDetail.PTramBh = (int?)v_DiscountRate;
                        objPaymentDetail.DiscountRate = DiscountRate(v_DiscountRate,
                                                       Utility.DecimaltoDbnull(
                                                           objPaymentDetail.OriginPrice, 0));
                        objPaymentDetail.DiscountPrice = DiscountPrice(v_DiscountRate,
                                                                 Utility.DecimaltoDbnull(
                                                                     objPaymentDetail.OriginPrice, 0));
                    }


                }
                else
                {
                    objPaymentDetail.MaDv = "DV";
                    objPaymentDetail.DiscountRate = 0;
                    objPaymentDetail.DiscountPrice = objPaymentDetail.OriginPrice;
                }

            }
            return arrPaymentDetail;
        }

        /// <summary>
        /// hàm thực hiện việc tính phần trăm của bảo hiểm y tế
        /// </summary>
        /// <param name="objPatientExam"></param>
        /// <param name="objRegExam"></param>
        public static void TinhToanKhamPtramBHYT(TPatientExam objPatientExam, TRegExam objRegExam)
        {
            decimal PTramBHYT = Utility.DecimaltoDbnull(objPatientExam.DiscountRate, 0);
            objRegExam.MaKhoaThien = globalVariables.MA_KHOA_THIEN;
            if (Utility.Int32Dbnull(objRegExam.TrongGoi, 0) == 1)
            {
                objRegExam.GiaBhytCt = 0;
                objRegExam.GiaBnct = 0;
            }
            else
            {
                objRegExam.GiaBhytCt = Utility.DecimaltoDbnull(objRegExam.RegFee) *
                                            Utility.DecimaltoDbnull(PTramBHYT) / 100;

                objRegExam.GiaBnct = Utility.DecimaltoDbnull(objRegExam.RegFee, 0) -
                                          Utility.DecimaltoDbnull(objRegExam.GiaBhytCt, 0);
            }
        }

        public static TPaymentDetail[] TinhPhamTramBHYT(TPaymentDetail[] arrPaymentDetail, decimal v_DiscountRate)
        {

            foreach (TPaymentDetail objPaymentDetail in arrPaymentDetail)
            {
                if (objPaymentDetail.IsPayment == 0)//Không tự túc mới tính
                {
                    objPaymentDetail.PTramBh = (int?)v_DiscountRate;
                    objPaymentDetail.DiscountRate = DiscountRate(v_DiscountRate, Utility.DecimaltoDbnull(objPaymentDetail.OriginPrice, 0));
                    objPaymentDetail.DiscountPrice = DiscountPrice(v_DiscountRate, Utility.DecimaltoDbnull(objPaymentDetail.OriginPrice, 0));
                }
                else//Tự túc
                {
                    objPaymentDetail.MaDv = "DV";
                    objPaymentDetail.DiscountRate = 0;//BHYT chi trả 0 do tự túc
                    objPaymentDetail.DiscountPrice = objPaymentDetail.OriginPrice;
                }

            }
            return arrPaymentDetail;
        }
        public static string GetMaPhieuThu(DateTime dateTime, int LoaiPhieu)
        {
            return Utility.sDbnull(SPs.YhhqGetMaPhieuThu(dateTime, LoaiPhieu).ExecuteScalar<string>(), "");
        }
        public static string GetThanhToan_TraiTuyen()
        {
            string sPaymentFlow = "";
            SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("TRAITUYEN");
            SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) sPaymentFlow = objSystemParameter.SValue;
            return sPaymentFlow;
        }
        /// <summary>
        /// hàm thực hiện lấy thông tin của lương cơ bản
        /// </summary>
        /// <returns></returns>
        public static decimal GetLuongCoBan()
        {
            decimal BASICSALARY = 83000;
            SqlQuery q = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("BASICSALARY");
            SysSystemParameter objSysSystemParameter = q.ExecuteSingle<SysSystemParameter>();
            if (objSysSystemParameter != null)
            {
                BASICSALARY = Utility.DecimaltoDbnull(objSysSystemParameter.SValue, 0);
            }
            return BASICSALARY;
        }
        public static bool CorrectLinePatientExam(string vClinicCode)
        {
            DataTable dataTable = new DataTable();
            dataTable = SPs.DetmayGetClinicCode(vClinicCode).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                SqlQuery q = new SqlQuery().From(SysSystemParameter.Schema)
                    .Where(SysSystemParameter.Columns.SName).IsEqualTo("ACCOUNTCLINIC");
                if (q.GetRecordCount() > 0)
                {
                    SysSystemParameter objParameter = q.ExecuteSingle<SysSystemParameter>();
                    if (objParameter.SValue.Contains(vClinicCode)) return true;
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


            }

            else
            {
                return true;
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC KIỂM TRA XEM BÁO CÁO ĐƠN VỊ NÀO
        /// </summary>
        /// <returns></returns>
        public static string LayMaDviLamViec()
        {
            SqlQuery q = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("ACCOUNTNAME");
            SysSystemParameter objParameter = q.ExecuteSingle<SysSystemParameter>();
            if (objParameter != null) return objParameter.SValue;
            else
            {
                return "DETMAY";
            }
        }
        public static void LoadThamSoHeThong()
        {


            globalVariables.gv_ChoPhepMaKhoaThucHien = THU_VIEN_CHUNG.Laygiatrithamsohethong("MA_KHOA_THIEN", "KKB", false) == "YES";
            globalVariables.gv_GiathuoctheoGiatrongKho = THU_VIEN_CHUNG.Laygiatrithamsohethong("GIATHUOCKHO", "1", false) == "1";


            globalVariables.ChophepNhapkhoLe = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("ChophepNhapkhoLe", "0", false));



            globalVariables.GioiHanTuoi = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("GIOI_HAN_TUOI", "0", false));


            globalVariables.gv_BenhVienTuyen = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_TUYEN", "TW", false);


            globalVariables.gv_ThanhToanKhamTaiTiepDon = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KHAM_TIEPDON", "NO", false) == "YES";



            globalVariables.TrongGio = THU_VIEN_CHUNG.Laygiatrithamsohethong("TRONGGIO", "0:00-23:59", false);


            globalVariables.TrongNgay = THU_VIEN_CHUNG.Laygiatrithamsohethong("TRONGNGAY", "2,3,4,5,6,7,CN", false);


            globalVariables.KT_THANHTOAN_CLS_BARCODE_DV = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KT_THANHTOAN_CLS_BARCODE_DV", "0", false), 0);


            globalVariables.MA_UuTien = THU_VIEN_CHUNG.Laygiatrithamsohethong("MA_UUTIEN", "", false);



            globalVariables.KT_THANHTOAN_CLS_BARCODE_BHYT = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KT_THANHTOAN_CLS_BARCODE_BHYT", "0", false), 0);


            globalVariables.ICD_BENH_AN_NGOAI_TRU = THU_VIEN_CHUNG.Laygiatrithamsohethong("ICD_BENH_AN_NGOAI_TRU", "", false);


            globalVariables.SO_BENH_AN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("SO_BENH_AN", "-1", false), -1);


            globalVariables.MA_BHYT_KT = THU_VIEN_CHUNG.Laygiatrithamsohethong("MA_BHYT_KT", "", false);

           
                globalVariables.CheckDrugQuantity = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHECKDRUGQUANTITY", "FALSE", false) == "TRUE";
           

          
                globalVariables.CheckDrugLimit = THU_VIEN_CHUNG.Laygiatrithamsohethong("CHECKDRUGLIMIT", "FALSE", false) == "TRUE";
           
         
                globalVariables.PressViewSluongTon = THU_VIEN_CHUNG.Laygiatrithamsohethong("VIEWSLUONGTON", "FALSE", false) == "TRUE";

             globalVariables.NHAPGIA_KEDONTHUOC =Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPGIA_KEDONTHUOC", "0", false), 0);
            globalVariables.CHARACTERCASING =Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("CHARACTERCASING", "0", false), 0);
            globalVariables.KIEMTRAMATHEBHYT =Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KIEMTRAMATHEBHYT", "0", false), 0);
            globalVariables.TUDONGCHECKTRAITUYEN =Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("TUDONGCHECKTRAITUYEN", "0", false), 0);
            globalVariables.BOTENDIACHINH = THU_VIEN_CHUNG.Laygiatrithamsohethong("BOTENDIACHINH", "", false);

              
        }
        public static short GetDepartmentID(string sUserName)
        {
            short vDepartment_Id = -1;
            try
            {
                SqlQuery sqlQuery = new Select().From(LStaff.Schema).Where(LStaff.Columns.Uid).IsEqualTo(sUserName);
                LStaff objStaff = sqlQuery.ExecuteSingle<LStaff>();
                if (objStaff != null)
                {
                    vDepartment_Id = objStaff.DepartmentId;
                }
            }
            catch (Exception ex)
            {
                vDepartment_Id = -1;

            }



            return vDepartment_Id;
        }
        public static short GetDepartmentIDByCode(string Code)
        {
            short vDepartment_Id = -1;
            try
            {
                SqlQuery sqlQuery = new Select().From(LDepartment.Schema).Where(LDepartment.Columns.DepartmentCode).IsEqualTo(Code);
                LStaff objStaff = sqlQuery.ExecuteSingle<LStaff>();
                if (objStaff != null)
                {
                    vDepartment_Id = objStaff.DepartmentId;
                }
            }
            catch (Exception ex)
            {
                vDepartment_Id = globalVariables.DepartmentID;

            }



            return vDepartment_Id;
        }
        public static void LoadQuyenCuaNhanVien()
        {
            try
            {
                globalVariables.gv_StaffID = BusinessHelper.GetStaff_IDByUserName(globalVariables.UserName);
                LStaff objStaff =
                    new Select().From(LStaff.Schema).Where(LStaff.Columns.Uid).IsEqualTo(globalVariables.UserName).ExecuteSingle<LStaff>();
                if (objStaff != null)
                {
                    globalVariables.gv_sStaffName = Utility.sDbnull(objStaff.StaffName);
                    globalVariables.gv_StaffID = Utility.Int16Dbnull(objStaff.StaffId);
                    globalVariables.DepartmentID = Utility.Int16Dbnull(objStaff.DepartmentId);
                    globalVariables.Room_ID = Utility.Int16Dbnull(objStaff.DepartmentId);
                    
                    globalVariables.QUYEN_HUYTHANHTOAN_TATCA =globalVariables.IsAdmin|| Utility.Int32Dbnull(objStaff.QuyenHuythanhtoanTatca, 0) == 1;
                    globalVariables.QUYEN_MOKHOA_TATCA = globalVariables.IsAdmin || Utility.Int32Dbnull(objStaff.QuyenMokhoaTatca, 0) == 1;
                    globalVariables.QUYEN_TRALAI_TIEN = globalVariables.IsAdmin || Utility.Int32Dbnull(objStaff.QuyenTralaiTien, 0) == 1;

                    globalVariables.QUYEN_SUANGAY_THANHTOAN = globalVariables.IsAdmin || Utility.Int32Dbnull(objStaff.QuyenSuangayThanhtoan, 0) == 1;

                    



                }
                SqlQuery sqlQuery = new Select().From(SysUser.Schema)
                    .Where(SysUser.Columns.PkSuid).IsEqualTo(globalVariables.UserName);
                SysUser objSysUser = sqlQuery.ExecuteSingle<SysUser>();
                if (objSysUser != null)
                {
                    globalVariables.IsAdmin = Utility.Int32Dbnull(objSysUser.ISecurityLevel) == 1;
                }
                sqlQuery = new Select().From(SysSystemParameter.Schema)
                    .Where(SysSystemParameter.Columns.SName).IsEqualTo("DELETEASUSER");
                SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                if (objSystemParameter != null)
                {
                    globalVariables.gv_UserAcceptDeleted = Utility.sDbnull(objSystemParameter.SValue.Trim()) == "YES";
                }
            }
            catch
            { }
        }
        private static bool ExistsMaQuyen(List<string> listQuyen, string MaQuyen)
        {
            var query = from quyen in listQuyen
                        where Utility.sDbnull(quyen).Contains(MaQuyen)
                        select quyen;
            if (query.Any()) return true;
            else return false;
        }
        /// <summary>
        /// hàm thực hiện việc lấy màu của hệ thống
        /// </summary>
        /// <returns></returns>
        public static string GetSysColor()
        {
            SqlQuery q = new Select().From(SysFormColor.Schema)
               .Where(SysFormColor.Columns.SystemColorId).IsEqualTo(1);
            string SysColor = "";
            SysFormColor objSystemParameter = q.ExecuteSingle<SysFormColor>();
            if (objSystemParameter != null)
            {
                SysColor = Utility.sDbnull(objSystemParameter.ColorValue);
            }
            return SysColor;
        }
        public static bool IsNgoaiGio()
        {
            try
            {
                BusinessHelper.GetTrongNgayTrongGio();
                //Kiểm tra ngày hiện tại có trong tham biến không
                if (KT_TRONGNGAY())
                {
                    // Nếu có trong ngày kiểm tra giờ hiện tại có trong giờ ko
                    if (!Utility.IsBetweenManyTimeranges(GetSysDateTime(), globalVariables.TrongGio))
                    {
                        //Nếu giờ hiện tại không trong giờ tham biến trả về true. Ngoài giờ khám
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void GetTrongNgayTrongGio()
        {
            SysSystemParameter TrongGio =
             new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("TRONGGIO")
                 .ExecuteSingle<SysSystemParameter>();
            if (TrongGio != null)
            {
                globalVariables.TrongGio = Utility.sDbnull(TrongGio.SValue, "");
            }
            else
            {
                globalVariables.TrongGio = "0:00-23:59";
            }
            SysSystemParameter TrongNgay =
                  new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("TRONGNGAY")
                      .ExecuteSingle<SysSystemParameter>();
            if (TrongNgay != null)
            {
                globalVariables.TrongNgay = Utility.sDbnull(TrongNgay.SValue, "");
            }
            else
            {
                globalVariables.TrongGio = "2,3,4,5,6,7,CN";
            }
        }
        public static DateTime GetSysDateTime()
        {
            DataTable dataTable = new DataTable();
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            //dataTable = SPs.GetSYSDateTime().GetDataSet().Tables[0];
            DateTime dateTime = new SubSonic.InlineQuery().ExecuteScalar<DateTime>("select getdate()");

            return dateTime;
        }
        /// <summary>
        /// Kiểm tra so sánh ngày hiện tại với các ngày trong biến TRONGNGAY
        /// </summary>
        /// <returns></returns>
        static bool KT_TRONGNGAY()
        {
            try
            {
                string[] TrongNgay = globalVariables.TrongNgay.Split(',');
                if (TrongNgay.Length > 0)
                {
                    //So sánh giá trị từng ngày trong mảng.
                    foreach (string s in TrongNgay)
                    {
                        switch (s)
                        {
                            //Thứ 2 : giá trị so sánh = 1;
                            case "2":
                                //Nếu so sánh ngày bằng nhau thì trả về true
                                if (_SoSanhNgay(1))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 3 : giá trị so sánh = 2;
                            case "3":
                                if (_SoSanhNgay(2))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 4 : giá trị so sánh = 3;
                            case "4":
                                if (_SoSanhNgay(3))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 5 : giá trị so sánh = 4;
                            case "5":
                                if (_SoSanhNgay(4))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 6 : giá trị so sánh = 5;
                            case "6":
                                if (_SoSanhNgay(5))
                                {
                                    return true;
                                }
                                break;
                            //Thứ 7 : giá trị so sánh = 6;
                            case "7":
                                if (_SoSanhNgay(6))
                                {
                                    return true;
                                }
                                break;
                            //Thứ CN : giá trị so sánh = 0;
                            case "CN":
                                if (_SoSanhNgay(0))
                                {
                                    return true;
                                }
                                break;
                        }
                    }
                    //Nếu hết các giá trị trong mảng ko có giá trị nào bằng ngày hiện tại thì trả về false
                    return false;
                }
                //Nếu mảng giá trị nhỏ hơn không là ko có tham biến thì trả về true
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// KIểm tra so sánh ngày trong biến truyền vào với ngày hiện tại. Nếu bằng nhau thì trả về true else false
        /// </summary>
        /// <param name="Ngay"></param>
        /// <returns></returns>
        static bool _SoSanhNgay(int Ngay)
        {
            try
            {
                return (int)GetSysDateTime().DayOfWeek == Ngay;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lấy địa chỉ ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP4Address()
        {
            try
            {
                if (string.IsNullOrEmpty(globalVariables.IpAddress))
                {
                    string IP4Address = String.Empty;

                    foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                    {
                        if (IPA.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IP4Address = IPA.ToString();
                            break;
                        }
                    }
                    globalVariables.IpAddress = IP4Address;
                }


                return globalVariables.IpAddress;
            }
            catch
            { return "NO-IP"; }
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của địa chỉ mac cho máy tính
        /// </summary>
        /// <returns></returns>

        public static string GetMACAddress()
        {
            try
            {
            if (string.IsNullOrEmpty(globalVariables.IpMacAddress))
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)// only return MAC Address from first card  
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                        globalVariables.IpMacAddress = sMacAddress;
                    }
                }
            }
            //  Utility.sDbnull()
            return globalVariables.IpMacAddress;
             }
            catch
            { return "NO-ADDRESS"; }
        }
        public static DataTable LayDsachBsi()
        {
            return SPs.TamphucLaythongTinVien(globalVariables.gv_BacSyNgoaitru).GetDataSet().Tables[0];
        }
        public static DataTable LayThongTin_KhoaNoiTru()
        {
            return SPs.TamphucLaythongtinKhoaNoitru(-1, (int?)ConditionPhongKhoa.TatCa).GetDataSet().Tables[0];
        }
        public static DataTable LayThongTinPhongKhamNgoaitru()
        {
            return SPs.TamphucLayThongTinPhongNgoaitru(globalVariables.DepartmentID).GetDataSet().Tables[0];
        }

        public static DataTable LayThongTinPhongKhamNgoaitru(string ma_khoa_thien, string ma_phong_thien)
        {
            return SPs.NoitietLayThongTinKHOAPHONGThamkham(ma_khoa_thien, ma_phong_thien).GetDataSet().Tables[0];
        }

        public static DataTable LayThongTinPhongKhamNgoaitruKhac(int Department_ID)
        {
            return SPs.TamphucLayThongTinPhongNgoaitru(Department_ID).GetDataSet().Tables[0];
        }
        public static DataTable LayDulieuDanhmucChung(List<string> lstLoai)
        {
            DataTable m_NN = new DataTable();

            m_NN =
                new Select(DmucChung.Columns.Ma, DmucChung.Columns.Ten, DmucChung.Columns.Loai, DmucChung.Columns.SttHthi).From(DmucChung.Schema)
                    .Where(DmucChung.Columns.Loai).In(lstLoai)
                    .OrderAsc(DmucChung.Columns.SttHthi)
                    .ExecuteDataSet().Tables[0];
            return m_NN;
        }
        public static string GetShortCut(string fullForm)
        {
            try
            {
                string shortcut = "";
                string realName = fullForm.Trim() + " " + Utility.Bodau(fullForm);
                string[] arrWords = realName.ToLower().Split(' ');
                string _space = "";
                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                    {
                        _space += word + " ";
                    }
                }
                shortcut += _space; // +_Nospace;
                foreach (string word in arrWords)
                {
                    if (word.Trim() != "")
                        shortcut += word.Substring(0, 1);
                }
                return shortcut.Trim();
            }
            catch
            {
                return fullForm;
            }
        }
        public static string getLowerValue(string _value)
        {
            string reval = "";
            string[] arrWords = _value.Trim().ToLower().Split(' ');
            foreach (string word in arrWords)
            {
                if (word.Trim() != "")
                    reval += word + " ";
            }
            return reval.Trim();
        }
        public static string BottomCondition()
        {

            return string.Format("Hệ thống quản lý bệnh viện HIS, Phiếu in lúc : {0} bởi : {1}",
                                 BusinessHelper.GetSysDateTime(), 
                                 !string.IsNullOrEmpty(globalVariables.gv_sStaffName) ? globalVariables.gv_sStaffName :
                                 globalVariables.UserName);

        }
        public static string KetNoi_SinhMaBarCode_CD()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable = SPs.KetnoiSinhBarCodeXn(BusinessHelper.GetSysDateTime()).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static string NOITIET_KetNoi_SinhMaBarCode_CD()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable = SPs.NoitietSinhmachidinhCls(BusinessHelper.GetSysDateTime(), globalVariables.MA_KHOA_THIEN, globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static string NOITIET_LAB_KetNoi_SinhMaBarCode_CD()
        {
            string BarCode = "";
            DataTable dataTable = new DataTable();
            dataTable = SPs.NoitietLabSinhmachidinhCls(BusinessHelper.GetSysDateTime(), globalVariables.MA_KHOA_THIEN, globalVariables.MIN_STT, globalVariables.MAX_STT).GetDataSet().Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                BarCode = Utility.sDbnull(dataTable.Rows[0]["BARCODE"], "");
            }
            return BarCode;
        }
        public static DataTable LayThongTinDichVuCLS()
        {
            return SPs.LServicesGetList().GetDataSet().Tables[0];
        }

        public static DataTable LaydsachDvu_CLS(string MaDoiTuong, int ID_GoiDV, string MA_KHOA_THIEN)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = SPs.NoitietGetDataServiceDetail(MaDoiTuong, ID_GoiDV, MA_KHOA_THIEN).GetDataSet().Tables[0];
                if (!dataTable.Columns.Contains("UnSignedService_Name"))
                    dataTable.Columns.Add("UnSignedService_Name", typeof(string));
                if (!dataTable.Columns.Contains("UnSignedServiceDetail_Name"))
                    dataTable.Columns.Add("UnSignedServiceDetail_Name", typeof(string));
                foreach (DataRow drv in dataTable.Rows)
                {
                    drv["UnSignedService_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                    drv["UnSignedServiceDetail_Name"] = Utility.UnSignedCharacter(drv["ServiceDetail_Name"].ToString());
                }
                dataTable.AcceptChanges();
            }
            catch (Exception)
            {
            }
            return dataTable;
        }
        public static string MaKieuThanhToan(int PaymentType_ID)
        {
            string MaKieu = "";
            switch (PaymentType_ID)
            {
                case 0:
                    MaKieu = "KHAM";
                    break;
                case 1:
                    MaKieu = "KHAM";
                    break;
                case 2:
                    MaKieu = "CLS";
                    break;
                case 3:
                    MaKieu = "THUOC";
                    break;
                case 4:
                    MaKieu = "GIUONG";
                    break;
                case 5:
                    MaKieu = "VT";
                    break;
                case 6:
                    MaKieu = "TAMUNG";
                    break;
                case 7:
                    MaKieu = "AN";
                    break;
                case 8:
                    MaKieu = "GOIDV";
                    break;
            }
            return MaKieu;
        }
        public static string Laygiatrithamsohethong(string ParamName,bool fromDB)
        {
            try
            {
                string reval = null;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            ParamName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName+" ='" + ParamName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return null;
            }
        }
        public static void CapnhatgiatriTieudebaocao(string Matieude, string _value)
        {
            try
            {
                if (Utility.DoTrim(Matieude) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                if (arrDR.Length > 0)
                {
                    arrDR[0][SysTieude.NoiDungColumn.ColumnName] = _value;
                    globalVariables.gv_dtSysTieude.AcceptChanges();
                    new Update(SysTieude.Schema).Set(SysTieude.NoiDungColumn).EqualTo(_value).Where(SysTieude.MaTieudeColumn).IsEqualTo(Matieude).Execute();
                }
                else
                {
                    SysTieude newItem = new SysTieude();
                    newItem.MaTieude = Matieude;
                    newItem.NoiDung = _value;
                  
                    newItem.Save();
                    DataRow newrow = globalVariables.gv_dtSysTieude.NewRow();
                    newrow[SysTieude.MaTieudeColumn.ColumnName] = Matieude;
                    newrow[SysTieude.NoiDungColumn.ColumnName] = _value;
                   
                    globalVariables.gv_dtSysTieude.Rows.Add(newrow);
                    globalVariables.gv_dtSysTieude.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tiêu đề báo cáo:\n" + ex.Message);
            }
        }
        public static string LaygiatriTieudebaocao(string Matieude, string defaultval, bool fromDB)
        {
            try
            {
                string reval = defaultval;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysTieude.Schema).Where(SysTieude.Columns.MaTieude).IsEqualTo(
                            Matieude);
                    SysTieude objSystemParameter = sqlQuery.ExecuteSingle<SysTieude>();
                    if (objSystemParameter != null) reval = objSystemParameter.NoiDung;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public static string LaygiatriTieudeBaocao(string Matieude, bool fromDB)
        {
            try
            {
                string reval = "";
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysTieude.Schema).Where(SysTieude.Columns.MaTieude).IsEqualTo(
                            Matieude);
                    SysTieude objSystemParameter = sqlQuery.ExecuteSingle<SysTieude>();
                    if (objSystemParameter != null) reval = objSystemParameter.NoiDung;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysTieude.Select(SysTieude.MaTieudeColumn.ColumnName + " ='" + Matieude + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysTieude.NoiDungColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return "";
            }
        }
        public static void Capnhatgiatrithamsohethong(string ParamName,string _value)
        {
            try
            {
                if (Utility.DoTrim(ParamName) == "") return;
                DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + ParamName + "'");
                if (arrDR.Length > 0)
                {
                    arrDR[0][SysSystemParameter.SValueColumn.ColumnName] = _value;
                    globalVariables.gv_dtSysparams.AcceptChanges();
                    new Update(SysSystemParameter.Schema).Set(SysSystemParameter.SValueColumn).EqualTo(_value).Where(SysSystemParameter.SNameColumn).IsEqualTo(ParamName).Execute();
                }
                else
                {
                    SysSystemParameter newItem = new SysSystemParameter();
                    newItem.FpSBranchID = globalVariables.Branch_ID;
                    newItem.SName = ParamName;
                    newItem.SValue = _value;
                    newItem.IMonth = 0;
                    newItem.IYear = 0;
                    newItem.IStatus = 1;
                    newItem.IsNew = true;
                    newItem.Save();
                    DataRow newrow = globalVariables.gv_dtSysparams.NewRow();
                    newrow[SysSystemParameter.FpSBranchIDColumn.ColumnName] = globalVariables.Branch_ID;
                    newrow[SysSystemParameter.SNameColumn.ColumnName] = ParamName;
                    newrow[SysSystemParameter.SValueColumn.ColumnName] = _value;
                    newrow[SysSystemParameter.IYearColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IMonthColumn.ColumnName] = 0;
                    newrow[SysSystemParameter.IStatusColumn.ColumnName] = 1;
                    globalVariables.gv_dtSysparams.Rows.Add(newrow);
                    globalVariables.gv_dtSysparams.AcceptChanges();
                }
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi khi cập nhật giá trị tham số hệ thống:\n"+ex.Message);
            }
        }
        public static string Laygiatrithamsohethong(string ParamName,string defaultval,bool fromDB)
        {
            try
            {
                string reval = defaultval;
                if (fromDB)
                {
                    SqlQuery sqlQuery =
                        new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo(
                            ParamName);
                    SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
                    if (objSystemParameter != null) reval = objSystemParameter.SValue;
                }
                else
                {
                    DataRow[] arrDR = globalVariables.gv_dtSysparams.Select(SysSystemParameter.SNameColumn.ColumnName + " ='" + ParamName + "'");
                    if (arrDR.Length > 0) reval = Utility.sDbnull(arrDR[0][SysSystemParameter.SValueColumn.ColumnName]);
                }
                return reval;
            }
            catch
            {
                return defaultval;
            }
        }
        public static string GetTieuDeBaoCao(string FormName, string Values)
        {
            string TieuDe = "";
            SqlQuery sqlQuery = new Select().From(SysTieudeBaocao.Schema)
                .Where(SysTieudeBaocao.Columns.NguoiTao).IsEqualTo(globalVariables.UserName)
                .And(SysTieudeBaocao.Columns.TenForm).IsEqualTo(FormName);
            if (sqlQuery.GetRecordCount() <= 0)
            {
                SysTieudeBaocao.Insert(globalVariables.UserName, Values, "", FormName);
            }
            SysTieudeBaocao objSysUserPrinter = sqlQuery.ExecuteSingle<SysTieudeBaocao>();
            if (objSysUserPrinter != null) TieuDe = Utility.sDbnull(objSysUserPrinter.TieuDe, "");

            return TieuDe;
        }
        public static void UpdateTieuDe(string FormName, string Values)
        {
            string TieuDe = "";
            SqlQuery sqlQuery = new Select().From(SysTieudeBaocao.Schema)
                .Where(SysTieudeBaocao.Columns.NguoiTao).IsEqualTo(globalVariables.UserName)
                .And(SysTieudeBaocao.Columns.TenForm).IsEqualTo(FormName);
            if (sqlQuery.GetRecordCount() <= 0)
            {
                SysTieudeBaocao.Insert(globalVariables.UserName, Values, "", FormName);
            }
            else
            {
                new Update(SysTieudeBaocao.Schema)
                    .Set(SysTieudeBaocao.Columns.TieuDe).EqualTo(Values)
                    .Where(SysTieudeBaocao.Columns.TenForm).IsEqualTo(FormName)
                    .And(SysTieudeBaocao.Columns.NguoiTao).IsEqualTo(globalVariables.UserName).Execute();
                // SysUserPrinter.Insert(globalVariables.UserName, 1, FormName, FormName, globalVariables.UserName, "", "");
            }

        }
        
    }
   
}
