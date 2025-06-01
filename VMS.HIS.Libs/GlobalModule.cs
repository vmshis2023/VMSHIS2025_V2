using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Data;
using System.Collections;
using VB6 = Microsoft.VisualBasic.Strings;
using SubSonic;
using VMS.HIS.DAL;
using System.Collections.Generic;
using NLog;
namespace VNS.Libs
{
    public class globalVariables
    {
        // Ma danh muc dùng chung 
        public static string DC_DM_BENHAN = "DM_BA";
        public static string DC_NHOMIN_CLS = "NHOM_INPHIEU_CLS";
        // 

        /// <summary>
        /// Bộ mã bệnh án 
        /// </summary>
        public static string BA_THA = "THA";
        public static string BA_RHM = "RHM";
        public static string BA_DTD = "DTD";
        public static string BA_BAS = "BAS";
        public static string BA_COP = "COP";
        public static string BA_VGB = "VGB";
        public static string BA_TMH = "TMH";
        public static string BA_THU = "THU";
        //public static string BA_THA = "THA";
        //public static string BA_THA = "THA";
        //public static string BA_THA = "THA";
        public static string AppSessionID = "";
        public static bool IsValidLicense = false;
        public static string  StringLicense = "";
        public static string BHYT79ATH_TIEUDE = "BẢNG TỔNG HỢP ĐỀ NGHỊ THANH TOÁN CHI PHÍ KHÁM CHỮA BỆNH NGOẠI TRÚ";
        public static string BHYT79ATH = "BHYT79ATH";
        public static string BHYT79ACT_TIEUDE = "DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KHÁM CHỮA BỆNH NGOẠI TRÚ";
        public static string BHYT79ACT = "BHYT79ACT";

        public static string BHYT05ATH_TIEUDE = "DANH SÁCH CHI PHÍ KHÁM CHỮA BỆNH ĐA TUYẾN NỘI TỈNH";
        public static string BHYT05ATH = "BHYT05ATH";
        public static string BHYT05ACT_TIEUDE = "DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KCB NGOẠI TRÚ";
        public static string BHYT05ACT = "BHYT05ACT";

        public static string BHYT25ATH_TIEUDE = "DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KCB NGOẠI TRÚ";
        public static string BHYT25ATH = "BHYT25ATH";
        public static string BHYT25ACT_TIEUDE = "DANH SÁCH ĐỀ NGHỊ THANH TOÁN CHI PHÍ KCB NGOẠI TRÚ";
        public static string BHYT25ACT = "BHYT25ACT";


        public static string BHYT14A_TIEUDE = "THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ CÁC NHÓM ĐỐI TƯỢNG THEO TUYỄN CHUYÊN MÔN KỸ THUẬT";
        public static string BHYT14A = "BHYT14A";

        public static string BHYT21A_TIEUDE = "THỐNG KÊ TÌNH HÌNH DVKT SỬ DỤNG";
        public static string BHYT21A = "BHYT21A";

        public static string BHYT20A_TIEUDE = "THỐNG KÊ TÌNH HÌNH THUỐC SỬ DỤNG";
        public static string BHYT20A = "BHYT20A";

        public static int gv_intChophepchongiathuoc = 0;
        public static string TieudephoiBHYT = "BẢNG KÊ CHI PHÍ KHÁM, CHỮA BỆNH NGOẠI TRÚ";
        public static string TieudeBienlaithutien = "BIÊN LAI THU TIỀN";
        public static int gv_intChophepChinhgiathuocKhiKedon = 0;
        public static bool gv_blnApdungChedoDuyetBHYT = false;
       // public static int gv_intPhantramLuongcoban = 15;
        public static string gv_strMaUutien = "1,2,3";
        public static string gv_strBankAPILink = "";
        public static string gv_strIPAddress = "";
        public static string gv_strComputerName = "";
        public static string gv_strMacAddress = "";
        public static bool hasLoadedCommonData = false;
        public static string gv_strMaQuyenLoiHuongBHYT100Phantram = "1,2";
        public static DataTable gv_dtSysparams = new DataTable();
        public static DataTable gv_dtSysTieude = new DataTable();
        public static DataTable gv_dtDanhmucchung = new DataTable();
        public static DataTable gv_dtKhoaPhongNgoaiTru = new DataTable();
        public static DataTable gv_dtDoituong = new DataTable();
        public static int CHARACTERCASING = 0;
        public static int SO_BENH_AN = 0;
        public static decimal LUONGCOBAN = 650000;
        public static bool gv_GiathuoctheoGiatrongKho = true;

        #region "tham số default"

        public static short gv_DefaultDanToc = 1;
        public static short gv_DefaultNoiGioiThieu = 2;
        public static int SoGioTinhNgay = 5;
        #endregion

        public static string WebPath = "";
        public static string m_strPropertiesFolder = "";
        public static string m_strPropertiesFolderQMS = "";
        public static string m_strPropertiesFolder_V1 = Application.StartupPath + @"\Properties";//Do biến phía trên bị hệ thống QMS chiếm nên dùng thêm biến này để ko ảnh hưởng QMS
        public static DataTable gv_PhongNoitru = new DataTable();
        public static DataTable gv_KhoaNoitru = new DataTable();
        public static DataTable gv_danhmucbenhan = new DataTable();
        public static string NoiDungHoaDonDo = "VIỆN PHÍ NỘI TRÚ";

        public static bool b_QMS_Stop = true;
        #region "nhập kho thuốc"
        public static byte gv_NhapKho = 1;
        public static byte gv_XuatKho = 2;
        public static byte gv_TraLaiKho = 4;
        public static byte gv_XuatKhoBN = 3;
        public static short ID_Kho = -1;
        #endregion
        public static string KhoaPhong = "";
        public static DataTable gv_LyDoNhapKho = new DataTable();
        public static DataTable gv_ChanDoanPhatthuoc = new DataTable();
        public static int NoiTru = 1;
        public static byte[] SysLogo = null;
        public static int NgoaiTru = 0;
        public static int SONGAYSAOCHEP = 3;
        public static string sMenuStyle = "";
        public static string TrongGio = "0:00-23:59";
        public static string TrongNgay = "2,3,4,5,6,7,CN";
        public static string gv_sSymmetricAlgorithmName = "Rijndael";
        public static bool LoginSuceess = false;
        public static int _NumberofBrlink = 0;
        public static int G_DUOCNOITRU = 1;
        #region "tham số của dược"
        public static int gv_intKIEMTRAMATHEBHYT = 0;
        public static int gv_intNHAPGIA_KEDONTHUOC = 0;
        public static int gv_intCHARACTERCASING = 0;
        public static int gv_intBHYT_TUDONGCHECKTRAITUYEN = 0;

        #endregion
        public static int MIN_STT = 1;
        public static int MAX_STT = 200;
        public static int G_DUOCNGOAITRU = 1;
        public static bool gv_ConnectSuccess = false;
        public static string gv_strBOTENDIACHINH = "";
        public static string SOMAYLE = "";
        public static string ACCOUNTCLINIC = "01065";
        public static string SERVEY_CODE = "01";
        public static string BHYT = "BHYT";
        public static int CURRENT_YEAR = DateTime.Now.Year;
        public static string sFolderName = Application.StartupPath + "/File_Config/";
        public static string SO_LO = "SO_LO";
        public static bool CheckDrugQuantity = false;
        public static bool PressViewSluongTon = false;
        public static bool DonThuocChuyenTheoDoiTuong = false;
        public static string ServerName = ".";
        public static string sUName = "sa";
        public static string LOAINHOMTHUOC = "THUOC";
        public static string sPwd = "sa";
        public static string sDbName = "RISLINK_DB";
        public static bool gv_drug_XacNhanTaiChinh = false;
        public static bool gv_TuSinhSo_BA = false;
        public static string IsXacNhan = "TATCA";
        public static string gv_BacSyNgoaitru = "BS_NGOAITRU";
        public static string gv_BacSyNoitru = "BS_NOITRU";
        public static string gv_ThuNgan = "NV_THUNGAN";
        public static string gv_YTA = "YTA";
        public static string gv_MaLoaiNhaVien = "MA_LOAI_NVIEN";
        public static bool gv_KiemTraSoLuongGoiDV = false;
        public static string gv_MaKieuGiuongBenh = "GIUONG";
        public static string gv_NhomCLS = "NHOM_CLS";
        public static string gv_MaCLS = "CLS";
        public static string gv_MaThuoc = "THUOC";
        public static string gv_MaGiuong = "GIUONG";
        public static string gv_MaPhong = "PHONG";
        public static int KHOKEDON = -1;
        public static int KHOKEDON_NOITRU = -1;
        public static int KHOKEDON_VTNGOAITRU = -1;
        public static int KHOKEDON_VTNOITRU = -1;
        public static string gv_MaVatTu = "VT";
        public static string gv_MaChiPhiThem = "CHIPHI_THEM";
        public static int PresTypeChiPhiThem = 1;
        public static string gv_PhieuBHYT_NoiTru = "MOI";
        public static int gv_KieuThanhToanNgoaiTru = 0;
        public static int gv_KieuThanhToanNoiTru = 1;
        public static int gv_KieuThanhToanGoiDvu = 3;
        public static int gv_KieuThanhToanTamUng = 2;
        public static string MA_KHOA_THIEN = "KYC";
        public static string ten_khoathuchien = "KYC";
        public static bool isSuperAdmin = false;
        public static string MA_PHONG_THIEN = "108";
        public static string MA_NHOM_BLY = "MA_NHOM_BLY";
        public static bool gv_UserAcceptDeleted = false;
        //public static string gv_MaInPhieuThanhToan = "INPHIEU_DVU";
        public static DataTable gv_LyDoPhieuChi = new DataTable();
        public static DataTable gv_LyDoPhieuThu = new DataTable();
        public static DataTable gv_BenhKemTheo = new DataTable();
        public static DataTable gv_ChiDanThemThuoc = new DataTable();
        public static DataTable gv_DataKetLuanGoi = new DataTable();
        public static DataTable gv_DataLoiDanBS = new DataTable();
        public static DataTable gv_DataDonViDung = new DataTable();
        public static DataTable gv_DataCachDung = new DataTable();
        public static DataTable gv_DataSoLanDung = new DataTable();
        public static DataTable gv_DataTaiKham = new DataTable();
        public static DataTable gv_DataLyDotraLai = new DataTable();
        public static DataTable gv_DataNgheNghiep = new DataTable();
        public static DataTable gv_LyDoMienGiam = new DataTable();
        public static string DANHSACHKHO = "";
        //  public static bool gv_TruDi
        public static int gv_SoGioCanhBao = 3;
        public static string gv_strTuyenBHYT = "1";
        public static bool gv_blnThongTuyen = false;
        public static string gv_strReportFolder = Application.StartupPath + @"\Reports\";
        public static string KhoaDuoc = "KHOA DƯỢC";
        #region "khai bao datatable dùng chung"
        public static DataTable gv_DonThuocMau = new DataTable();
        #endregion
        #region "khai báo biến của phần quyền"

      
       

        // public bool gv_Quyen
        #endregion
        //Khai báo các biến toàn cục sử dụng trong CT
        //Biến kết nối tới CSDL-->Bạn chỉ việc dùng biến này mà không cần khởi tạo chúng
        /// <summary>
        /// Biến kết nối tới CSDL. Sẽ được khởi tạo ở phần Core của hệ thống
        /// </summary>
        public static SqlConnection SqlConn;
        public static System.Data.OleDb.OleDbConnection OleDbConnection;
        /// <summary>
        /// Chuỗi kết nối tới CSDL SqlConn.ConnectionString
        /// </summary>
        public static string SqlConnectionString;
        /// <summary>
        /// Provider Name của Subsonic mặc định là ORM
        /// </summary>
        public static string ProviderName = "ORM";
        public static SqlDataProvider SQLProvider;
        public static int GioiHanTuoi = -1;
      
        public static bool gv_ThanhToanKhamTaiTiepDon = false;
        /// <summary>
        /// Mã nơi khám chữa bệnh ban đầu
        /// </summary>
        public static string gv_strNoiDKKCBBD = "065";
        public static string Branch_ID = "HIS";
        public static string gv_strNoicapBHYT = "01";

        public static string GvStrTendoituongNoiTinhKcbbd = "A. BỆNH NHÂN NỘI TỈNH KCB BAN ĐẦU";
        public static string GvStrTendoituongNoitinhKhongKcbbd = "B. BỆNH NHÂN NỘI TỈNH ĐẾN";
        public static string GvStrTendoituongNgoaitinh = "C. BỆNH NHÂN NGOẠI TỈNH ĐẾN";
        /// <summary>
        /// Tên đơn vị làm việc
        /// </summary>
        /// <summary>
        /// <summary>
        public static string Branch_Name = "HIS";
        /// <summary>
        /// Tên đơn vị cấp trên
        /// </summary>
        public static string ParentBranch_Name = "Lĩnh Nam";
        /// <summary>
        /// Admin đăng nhập?
        /// </summary>
        //public static bool IsAdminLogin = false;
        /// <summary>
        /// Địa chỉ đơn vị làm việc
        /// </summary>
        public static string Branch_Address = "Hà Nội";
        public static string Branch_Phone = "0904 648006";
        public static string Branch_Hotline = "0904 648006";
        public static string Branch_Fax = "0904 648006";
        public static string Branch_Email = "trangdm@daosen.com.vn";
        public static string Branch_Website = "www.daosen.com.vn";
        public static string MaphieuHoanung = "";
        /// <summary>
        /// Tên đăng nhập vào hệ thống
        /// </summary>
         public static string Ma_Coso = "";
        public static string UserName = "";
        public static string Tennguoidung = "";
        public static string UCL = "";
        public static bool _OK = false;
        //Tháng Năm hệ thống
        //Tháng làm việc(Chưa dùng)
        public static int gv_intCurrMonth;
        //Năm làm việc(Chưa dùng)
        public static int gv_intCurrYear;
        /// <summary>
        /// Ngôn ngữ hiển thị của hệ thống: VN hoặc EN
        /// </summary>
        public static string DisplayLanguage = "VN";
        public static string gv_sFormat = "### ### ### ### ###.##";
        //Bạn phải Load lại từ File Config nếu muốn dùng. Tiếng Việt có mã=VN. Tiếng Anh có mã=EN
        public static string gv_sAnnouce = "Thông báo";
        public static DataGridView CurrDtGridView;
        public static ArrayList gv_arrKeySearch = new ArrayList();
        public static bool gv_bCrptHasCached = false;
        public static bool IsAdmin = false;
        public static System.DateTime SysDate = System.DateTime.Now;
        public static int gv_intServiceType_ID = -1;
        public static string gv_strTenNhanvien = "";
        public static string gv_strDiadiem = "Hà Nội";
        /// <summary>
        /// Mã phòng ban theo LocalAlias
        /// </summary>
        public static DmucKhoaphong objDmucKhoaphongDangNhap = null;
        public static Int16 idKhoatheoMay = -1;
        /// <summary>
        /// Mã phòng ban theo UserName
        /// </summary>
        public static Int16 IdKhoaNhanvien = -1;
        public static Int16 IdPhongNhanvien = -1;
        public static DataTable dtQuyennhanvien = null;
        public static Int16 gv_intIDNhanvien = -1;
        public static int AnnounceTime = 10;
        public static int CheckByService = 0;
        public static string DelegateUser = "";
        public static ArrayList ArrDelegateUser;
        public static string PaymentList = "";
        public static string ServiceList = "";
        public static string FunctionName = "";
        public static string SubSystemName = "";
        public static int FunctionID = -1;
        public static bool FinishProcess = false;
        public static string CurrCodeGenStatus = "";
        public static string AssName = "";
        public static string AppActiveKey = "";
        public static string StoreKeepers = "";
        public static string Doctors = "";
        public static string Guardian = "";
        public static string Clerk = "";
        public static string Assistant = "";
        public static string Receptionist = "";
        public static string DiagnosticDoctor = "";
        public static string AssignDoctor = "";
        public static string Accountant = "";
        public static string Druggist = "";
        public static string FlowName = "FLOW1";
        public static Logger logReportCode;
        public static Logger logThamsohethong;
        public static Logger logQuyen;
        public static Logger AppLog;
        public static string StrQheNvpk = "";
        public static int Display = 0;
        public static short gv_intPhongNhanvien = -1;
        public static bool CheckDrugLimit = false;

        public static string hoten_canbo = "";
        public static string cccd_canbo = "";

        public static string BHXH_WebCode = "BHXHVN";
        public static string BHXH_WebPath = "http://127.0.0.1:8589/LichSuKCB.svc";
        public static string Invoice_WebCode = "VIETTEL";
        public static string Invoice_WebPath = "http://localhost:8005/InvoiceTranfer.svc";
        ///<summary>
        ///</summary>
        public static string FORMTITLE = "VMS.HIS";
        public static string ten_congty = "Công ty cổ phần CNTT VMS Việt Nam";
        public static string ten_phanmem = "Hệ thống quản lý thông tin Bệnh viện";
        public static string ten_nguoihotro = "Đào Văn Cường 0915 150148";

        /// <summary>
        /// 
        /// </summary>
        ///bien khai bao cua TuDN
        public static bool b_ConnectedLis = false;
        public static bool b_LISConnectionState = false;
        public static DataTable qh_NhanVienPhongKham = new DataTable();
        //  public static DateTime v_VNdtGetDateTime = new DateTime();
        public static DataTable g_dtConfigService = new DataTable();
        public static DataTable gv_dtDmucNoiKCBBD = new DataTable();
        public static DataTable gv_dtDmucPhongban = new DataTable();
        // public static DataTable g_dtGroupService = new DataTable();
        public static DataTable gv_dtDmucBenh = new DataTable();
        public static DataTable g_dtConfigLablink = new DataTable();
      
        public static DataTable g_dtMeasureUnit = new DataTable();
        public static DataTable gv_dtDmucLoaibenh = new DataTable();
        public static DataTable gv_dtDmucChung = new DataTable();
        public static DataTable gv_dtDangbaoche = new DataTable();
        public static DataTable gv_dtDanhMucThuoc = new DataTable();
        public static DataTable gv_dtLoaiThuoc = new DataTable();
        public static DataTable gv_dtDmucDichvuKcb = new DataTable();
        //public  static  DataTable g_dtDrugObjectRelation=new DataTable();
        public static DataTable gv_DataKetLuan = new DataTable();
        public static DataTable gv_DataChanDoan = new DataTable();
        public static DataTable gv_DataMauChanDoan = new DataTable();
        public static DataTable gv_DiaChi = new DataTable();
        ///Bien khai bao cua CUONGDV
        //public static DataTable g_dtDiseaseList = new DataTable();
        public static DataTable gv_dtDmucNhanvien = new DataTable();
        public static DataTable gv_dtQuyenNhanvien = new DataTable();
        public static DataTable gv_dtQuyenNhanvienCapnhatnhanhDmucChung = new DataTable();
        public static DataTable gv_dtQuyenNhanvien_Dmuc = new DataTable();
        public static int ChophepNhapkhoLe = 0;
        public static string MaKhoaXn = "XN";
        public static DataTable gv_dtQheDoituongThuoc = new DataTable();
        public static DataTable gv_dtQheDoituongDichvu = new DataTable();
        public static DataTable gv_dtDmucDiachinh = new DataTable();
        public static DataTable gv_dtDmucBenhVien = new DataTable();
        public static DataTable gv_dtDantoc  = new DataTable();
        public static DataTable gv_dtSysUserConfig = new DataTable();
        public static DataTable gv_dtKhoaPhong = new DataTable();
        //  public static DataTable gv_mdtDataQuanHuyen=new DataTable();
        //  public static DataTable g_dtMaterial = new DataTable();
        public static DataTable gv_dtDmucLoaiDichvuCls = new DataTable();
        public static DataTable gv_dtDmucDichvuCls = new DataTable();
        public static DataTable gv_dtNhomDichVuCLS = new DataTable();
        public static DataTable gv_dtDmucDichvuClsChitiet = new DataTable();
        public static bool HasAddedCols = false;
        public static  DataTable gv_dtDmucQheCamCLSChungPhieu = new DataTable();

        public static DataTable gv_dtNhomInCLS = new DataTable();
        public static string str_ConnectionstringLis = "";
        public static DataTable g_dsInsurance_Num = new DataTable();
        public static DataTable g_dtObjectServiceList = new DataTable();
        public static DataTable g_dtObjectTypeRelationSurgery = new DataTable();
        public static DataTable g_dtMaterialList = new DataTable();
        
        public static int gv_intKT_TT_ChuyenCLS_DV = 0;
        public static int gv_intKT_TT_ChuyenCLS_BHYT = 0;
        public static string gv_strBHYT_MAQUYENLOI_UUTIEN = "";
        public static DataTable dtAutocompleteAddress=null;
        public static List<string> LstAutocompleteAddressSource = null;

        //BIẾN SỬ DỤNG ĐỂ CÀI ĐẶT THUỘC TÍNH CHO CỘT GIÁ TRONG KÊ ĐƠN
        public static int g_Lock_Pres = 0;
        public static string gv_strICD_BENH_AN_NGOAI_TRU = "";
        public static int gv_intSO_BENH_AN_BATDAU;
        public static string gv_strMA_BHYT_KT = "";
        // public static string="System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))))";
    }
    public class globalVariablesPrivate
    {
        public static DmucKhoaphong objKhoaphong = null;
        public static DmucKhoaphong objKhoaphongNhanvien = null;
        public static DmucNhanvien objNhanvien = null;
    }

}
