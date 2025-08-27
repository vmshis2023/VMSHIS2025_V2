using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Data;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VNS.Libs;

namespace VMS.API.Libs
{
    public class HisLisModel
    {
        public DateTime Input_Date { get; set; }
        public string sInput_Date { get; set; }
        public string User_Name { get; set; }
        public string Assign_Code { get; set; }
        public string Patient_Code { get; set; }
        public string barcode { get; set; }
        public int patientId { get; set; }
        public string sHisTestType_Id { get; set; }
        public string fileName { get; set; }
        public byte[] fileBytes { get; set; }
    }

    public class HisLisWebApi
    {
        public Logger Log;
        public static HisLisWebApi INST = new HisLisWebApi();

        public class ApiResponse
        {
            public object Data { get; set; }
        }

        private HisLisWebApi()
        {
            LogConfig();
        }

        /// <summary>
        ///     Cấu hình Log của hệ thống
        /// </summary>
        private void LogConfig()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget
                {
                    FileName =
                        "${basedir}/MyLogHisLis/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log",
                    Layout =
                        "${date:format=dd/MM/yyyy HH\\:mm\\:ss\\.fff}|${threadid}|${level}|${logger}|${message}",
                    ArchiveAboveSize = 5242880,
                    Encoding = Encoding.UTF8
                };
                //var debuggerTarget = new ColoredConsoleTarget
                //{
                //    //Name = "Lablink Service Debuger",
                //    Layout =
                //        "${date:format=dd/MM/yyy HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}"
                //};
                config.AddTarget("file", fileTarget);
                //config.AddTarget("debugger", debuggerTarget);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, debuggerTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CapNhatTrangThaiVeHis(string webServiceLink, string assignCode, string barcode, DateTime inputDate)
        {
            var model = new HisLisModel();
            model.Assign_Code = assignCode;
            model.barcode = barcode;
            model.Input_Date = inputDate;
            var response = CallRestApi(webServiceLink + "/CapNhatTrangThaiVeHis", RequestMethod.POST, JsonConvert.SerializeObject(model));
            return response;
        }

        public bool GuiSMS(string webServiceLink, string telephone, string content, ref string errMsg)
        {
            dynamic jsonObject = new JObject();
            jsonObject.phoneNumber = telephone;
            jsonObject.content = content;
            webServiceLink = webServiceLink.EndsWith("/")
                    ? webServiceLink
                    : webServiceLink + "/";
            var response = CallRestApi(webServiceLink + "SMS", RequestMethod.POST, JsonConvert.SerializeObject(jsonObject));
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            errMsg = ret.Message;
            return ret.Success;
        }

        public bool CapNhatTrangThaiVeHis(string webServiceLink, DataSet ds, ref string errMsg)
        {
            var result = JsonConvert.SerializeObject(ds);
            var api = new ApiResponse();
            api.Data = result;
            var response = CallRestApi(webServiceLink + "/CapNhatTrangThaiVeHis", RequestMethod.POST, JsonConvert.SerializeObject(api));
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            errMsg = ret.Message;
            return ret.Success;
        }
        public bool VNPT_CapNhatTrangThaiVeHis(string webServiceLink, string sophieu, string barcode, string ngay_chidinh, string ngay_tiepnhan, string gio_tiepnhan, string trang_thai, ref string errMsg)
        {
            var apiLink = string.Format("{0}/CapNhatTrangThaiVeHis?sophieu={1}&barcode={2}&ngay_chidinh={3}&ngay_tiepnhan={4}&gio_tiepnhan={5}&trang_thai={6}",
                webServiceLink, sophieu, barcode, ngay_chidinh, ngay_tiepnhan, gio_tiepnhan, trang_thai);

            var response = CallRestApi(apiLink, RequestMethod.POST, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            errMsg = ret.Message;
            return ret.Success;
        }
        public DataTable LayDanhMucTestCuaHis(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/LayDanhMucTestCuaHis", RequestMethod.POST, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }
        public DataTable LayDanhMucTestCuaHis_Org(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/LayDanhMucTestCuaHis_Org", RequestMethod.POST, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }
        public DataTable LayDanhMucKhoa(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/LayDanhMucKhoa", RequestMethod.GET, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }

        public DataTable LayDanhMucPhong(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/LayDanhMucPhong", RequestMethod.GET, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }

        public DataTable LayDanhMucUser(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/LayDanhMucUser", RequestMethod.GET, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }

        public DataTable HuyChiDinh(string webServiceLink)
        {
            var response = CallRestApi(webServiceLink + "/HuyChiDinh?request=abc", RequestMethod.GET, "");
            var apiResponse = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var dt = JsonConvert.DeserializeObject<DataTable>(apiResponse.Data.ToString());
            return dt;
        }

        public string GuiKetQuaVeHis(string webServiceLink, DataTable dtResult)
        {
            var result = JsonConvert.SerializeObject(dtResult);
            var api = new ApiResponse();
            api.Data = result;
            var response = CallRestApi(webServiceLink + "/GuiKetQuaVeHis", RequestMethod.POST, JsonConvert.SerializeObject(api));
            return response;
        }

        public List<PatientHisLis> LayThongTinBenhNhanVaChiDinhObject(string webServiceLink, string assignCode, string patientCode, DateTime inputDate, 
            string userName, string sHisTestTypeId, ref string errMsg)
        {
            var model = new HisLisModel();
            model.User_Name = userName;
            model.Assign_Code = assignCode;
            model.Patient_Code = patientCode;
            model.Input_Date = inputDate;            
            model.sHisTestType_Id = sHisTestTypeId;
            
            var api = new ApiResponse();
            api.Data = JsonConvert.SerializeObject(model);
            var response = CallRestApi(webServiceLink + "/LayThongTinBenhNhanVaChiDinh", RequestMethod.POST, JsonConvert.SerializeObject(api));

            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            errMsg = ret.Message;
            if (!ret.Success)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<PatientHisLis>>(ret.Data.ToString());
        }

        public DataSet LayThongTinBenhNhanVaChiDinh(string webServiceLink, string assignCode, string patientCode, DateTime inputDate, string userName, string sHisTestTypeId, ref string errMsg)
        {
            var model = new HisLisModel();
            model.User_Name = userName;
            model.Assign_Code = assignCode;
            model.Patient_Code = patientCode;
            model.Input_Date = inputDate;
            model.sInput_Date = inputDate.ToString("yyyyMMddHHmmss");
            model.sHisTestType_Id = sHisTestTypeId;

            var api = new ApiResponse();
            api.Data = JsonConvert.SerializeObject(model);
            var response = CallRestApi(webServiceLink + "/LayThongTinBenhNhanVaChiDinh", RequestMethod.POST, JsonConvert.SerializeObject(api));

            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            var retDs = JsonConvert.DeserializeObject<DataSet>(Utility.sDbnull(ret.Data));
            if(retDs == null || retDs.Tables.Count <= 0 || retDs.Tables[0].Rows.Count <= 0)
            {
                errMsg = ret.Message;
            }
            
            if (!ret.Success)
            {
                return new DataSet();
            }
            return retDs;
        }

        public DataSet LayThongTinBenhNhanVaChiDinh(string webServiceLink, string assignCode, string patientCode, DateTime inputDate, string userName, ref string errMsg)
        {
            return LayThongTinBenhNhanVaChiDinh(webServiceLink, assignCode, patientCode, inputDate, userName, string.Empty, ref errMsg);
        }

        public DataSet LayThongTinBenhNhanVaChiDinh(string webServiceLink, string assignCode, string patientCode, DateTime inputDate, ref string errMsg)
        {
            return LayThongTinBenhNhanVaChiDinh(webServiceLink, assignCode, patientCode, inputDate, string.Empty, ref errMsg);            
        }

        public DataSet LayThongTinBenhNhanVaChiDinh(string webServiceLink, string assignCode, DateTime inputDate, ref string errMsg)
        {
            return LayThongTinBenhNhanVaChiDinh(webServiceLink, assignCode, string.Empty, inputDate, ref errMsg);
        }

        public DataTable LayDanhSachBenhNhanChiDinhXetNghiem(string webServiceLink, string fromDate, string toDate, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiem?sFromDate={1}&sToDate={2}",
                webServiceLink, fromDate, toDate);

            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<DataTable>(ret.Data.ToString());
        }
        public List<VNPT_Rootobject> VNPT_LayDanhSachBenhNhanChiDinhXetNghiem_new(string webServiceLink, string fromDate, string toDate, string TrangThai, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiem?TuNgay={1}&DenNgay={2}&TrangThai={3}",
                webServiceLink, fromDate, toDate, TrangThai);

            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<List<VNPT_Rootobject>>(ret.Data.ToString());
        }
        public List<PatientHisLis> VNPT_LayDanhSachBenhNhanChiDinhXetNghiem(string webServiceLink, string fromDate, string toDate, string TrangThai, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiem?TuNgay={1}&DenNgay={2}&TrangThai={3}",
                webServiceLink, fromDate, toDate, TrangThai);

            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<List<PatientHisLis>>(ret.Data.ToString());
        }
        public List<PatientHisLis> VNPT_LayDanhSachBenhNhanChiDinhXetNghiemTheoMaBenhNhan(string webServiceLink, string fromDate, string toDate, string TrangThai, string Ma_BenhNhan, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiemTheoMaBenhNhan?TuNgay={1}&DenNgay={2}&TrangThai={3}&MaBenhNhan={4}",
                webServiceLink, fromDate, toDate, TrangThai, Ma_BenhNhan);

            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<List<PatientHisLis>>(ret.Data.ToString());
        }
        public DataSet VNPT_LayThongTinBenhNhanVaChiDinh(string webServiceLink, string SoPhieu, string NgayChidinh, string TrangThai, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayThongTinBenhNhanVaChiDinh?SoPhieu={1}&NgayChiDinh={2}&TrangThai={3}",
                webServiceLink, SoPhieu, NgayChidinh, TrangThai);

            var response = CallRestApi(apiLink, RequestMethod.POST, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<DataSet>(ret.Data.ToString());
        }
        public List<PatientHisLis> LayDanhSachBenhNhanChiDinhXetNghiem(string webServiceLink, string departmentCode, string fromDate, string toDate, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiem?departmentCode={1}&sFromDate={2}&sToDate={3}",
                webServiceLink, departmentCode, fromDate, toDate);
            
            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<List<PatientHisLis>>(ret.Data.ToString());
        }
        public DataSet LayDanhSachBenhNhanChiDinhXetNghiem_SonTay_2024(string webServiceLink, string departmentCode, string fromDate, string toDate, ref string errMsg)
        {
            var apiLink = string.Format("{0}/LayDanhSachBenhNhanChiDinhXetNghiem?departmentCode={1}&sFromDate={2}&sToDate={3}",
                webServiceLink, departmentCode, fromDate, toDate);

            var response = CallRestApi(apiLink, RequestMethod.GET, string.Empty);
            var ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
            if (!ret.Success)
            {
                errMsg = ret.Message;
                return null;
            }
            return JsonConvert.DeserializeObject<DataSet>(ret.Data.ToString());
        }
        public bool DigitalSignatureCheckAccount(string apiLink, string userName, string appId, string secret, ref string errMsg)
        {
            var a = new
            {
                userName,
                appId,
                secret,
            };
            var stringPayload = JsonConvert.SerializeObject(a);
            var response = CallRestApi(apiLink + "/DigitalSignatureCheckAccount", RequestMethod.POST, stringPayload);
            ApiRequestResponse ret;
            var success = false;
            try
            {
                ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
                errMsg = ret.Message;
                success = ret.Success;
            }
            catch (Exception ex)
            {
                errMsg = MethodBase.GetCurrentMethod() + response;
            }
            return success;
        }

        public string GetDigitalSignatureApiLink(string apiLink, string apiVpnLink, string userName, string appId, string secret, ref string errMsg)
        {
            var a = DigitalSignatureCheckAccount(apiLink, userName, appId, secret, ref errMsg);
            if (!a)
            {
                return DigitalSignatureCheckAccount(apiVpnLink, userName, appId, secret, ref errMsg) ? apiVpnLink : string.Empty;
            }
            return apiLink;
        }

        public string DigitalSignaturePdfFileSign(
            string webServiceLink, string pdfFileName, string base64Pdf, string base64Signature, DateTime? dateSigned,
            string signatureType, string signatureName,
            string userName, string userFullName, string appId, string secret, List<VMSDigitalSignatureLocation> locations, ref string errMsg)
        {
            var retData = string.Empty;
            var response = string.Empty;
            try
            {
                var objDigitalSignature = new VMSDigitalSignature();
                objDigitalSignature.base64Pdf = base64Pdf;
                objDigitalSignature.base64Signature = base64Signature;
                objDigitalSignature.signatureType = signatureType;
                objDigitalSignature.signatureName = signatureName;
                objDigitalSignature.pdfFileName = pdfFileName;
                objDigitalSignature.userName = userName;
                objDigitalSignature.userFullName = userFullName;
                objDigitalSignature.appId = appId;
                objDigitalSignature.secret = secret;
                objDigitalSignature.locations = locations;
                objDigitalSignature.dateSigned = dateSigned;
                var stringPayload = JsonConvert.SerializeObject(objDigitalSignature);
                
                response = CallRestApi(webServiceLink + "/DigitalSignaturePdfFileSign", RequestMethod.POST, stringPayload);
                ApiRequestResponse ret;
                ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
                errMsg = ret.Message;
                retData = ret.Data.ToString();
            }
            catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(response))
                {
                    errMsg = response;
                }
                else
                {
                    errMsg = ex.Message;
                }
                
            }
            return retData;
        }
        public string GuiDuLieuKySoXMLVeHIS(DataTable dtResult, string webApiDomain = "")
        {
            var result = JsonConvert.SerializeObject(dtResult);
            var api = new ApiResponse();
            api.Data = result;
            var response = CallRestApi(webApiDomain + "/GuiDuLieuKySoXMLVeHIS", RequestMethod.POST, JsonConvert.SerializeObject(api));
            return response;
        }
      
        public string DigitalSignatureXMLSign(
          string webServiceLink, VMSDigitalSignature objDigitalSignature, ref string errMsg)
        {
            var retData = string.Empty;
            var response = string.Empty;
            try
            {
                var stringPayload = JsonConvert.SerializeObject(objDigitalSignature);

                response = CallRestApi(webServiceLink + "/DigitalSignatureXMLSign", RequestMethod.POST, stringPayload);
                ApiRequestResponse ret;
                ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
                errMsg = ret.Message;
                retData = ret.Data.ToString();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(response))
                {
                    errMsg = response;
                }
                else
                {
                    errMsg = ex.Message;
                }

            }
            return retData;
        }
        public string DigitalSignaturePdfFileSign(
            string webServiceLink, VMSDigitalSignature objDigitalSignature, ref string errMsg)
        {
            var retData = string.Empty;
            var response = string.Empty;
            try
            {
                var stringPayload = JsonConvert.SerializeObject(objDigitalSignature);

                response = CallRestApi(webServiceLink + "/DigitalSignaturePdfFileSign", RequestMethod.POST, stringPayload);
                ApiRequestResponse ret;
                ret = JsonConvert.DeserializeObject<ApiRequestResponse>(response);
                errMsg = ret.Message;
                retData = ret.Data.ToString();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(response))
                {
                    errMsg = response;
                }
                else
                {
                    errMsg = ex.Message;
                }

            }
            return retData;
        }

        public VMSQMSAudio GetAudio(string qmsApiLink, string content, ref string errMsg)
        {
            VMSQMSAudio audio = null;
            try
            {

                var response = CallRestApi(qmsApiLink + "/SaveAudio?noidung=" + content, RequestMethod.GET, string.Empty);
                audio = JsonConvert.DeserializeObject<VMSQMSAudio>(response);
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
            }
            return audio;
        }

        private string CallRestApi(string webServiceLink, RequestMethod method, string content)
        {
            try
            {
                
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                var request = WebRequest.Create(webServiceLink) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = method == RequestMethod.POST ? "POST" : "GET";
                    request.ContentType = "application/json";
                    request.ContentLength = bytes.Length;
                    request.KeepAlive = true;

                    if(method == RequestMethod.POST)
                    {
                        using (Stream putStream = request.GetRequestStream())
                        {
                            putStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (request.HaveResponse && response != null)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            var result = reader.ReadToEnd();
                            //result = result.Replace("\\", "");
                            if (result.StartsWith("\""))
                            {
                                result = result.Substring(1);
                            }
                            if (result.EndsWith("\""))
                            {
                                result = result.Substring(0, result.Length - 1);
                            }
                            return result;
                        }
                        //throw new Exception("Error fetching data.");
                    }
                }
                else
                {
                    //throw new Exception("Request is Null");
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return ex.Message;
            }
            return string.Empty;
        }
    }

    public class VMSQMSAudio
    {
        public string fileName { get; set; }
        public string fileByte { get; set; }
    }
    public class VNPT_Rootobject
    {
        public string SOTHUTU { get; set; }
        public string SOPHIEU { get; set; }
        public string SOPHIEUCHUNG { get; set; }
        public string BARCODE { get; set; }
        public string MABENHAN { get; set; }
        public string MABENHNHAN { get; set; }
        public string TENBENHNHAN { get; set; }
        public string NGAYSINH { get; set; }
        public string NAMSINH { get; set; }
        public string TUOI { get; set; }
        public string MAGIOITINH { get; set; }
        public string GIOITINH { get; set; }
        public string DIACHI { get; set; }
        public string CMT { get; set; }
        public string SODIENTHOAI { get; set; }
        public string DTNN { get; set; }
        public string EMAIL { get; set; }
        public string QUOCTICH { get; set; }
        public string MADOITUONG { get; set; }
        public string SOTHEBH { get; set; }
        public string NGAYBD { get; set; }
        public string NGAYKT { get; set; }
        public string CHANDOANVAOVIEN { get; set; }
        public string CHANDOAN { get; set; }
        public string CHANDOANBANDAU { get; set; }
        public string GHICHU_BENHCHINH { get; set; }
        public string KHOAID { get; set; }
        public string KHOACHIDINH { get; set; }
        public string PHONGID { get; set; }
        public string PHONGCHIDINH { get; set; }
        public string BUONGID { get; set; }
        public string TENBUONG { get; set; }
        public string GIUONGID { get; set; }
        public string TENGIUONG { get; set; }
        public string THOIGIANCHIDINH { get; set; }
        public string MABACSI { get; set; }
        public string BACSICHIDINH { get; set; }
        public string NGUOILAYMAU { get; set; }
        public string THOIGIANLAYMAU { get; set; }
        public string LOAITIEPNHAN { get; set; }
        public string CAPCUU { get; set; }
        public string DSPHONGTHUCHIENID { get; set; }
        public string DSPHONGTHUCHIEN { get; set; }
        public string SOVAOVIEN { get; set; }
        public string NHOMXN { get; set; }
        public string TENNHOMXN { get; set; }
        public string TENDOANDIEUTRI { get; set; }
        public string SOHD { get; set; }
        public string TENHOPDONG { get; set; }
        public string TENDONVI { get; set; }
        public string MANV { get; set; }
        public string CHUCDANH { get; set; }
        public string NGHENGHIEP { get; set; }
        public string DANTOC { get; set; }
        public string NOILAMVIEC { get; set; }
        public string LOAITIEPNHANID { get; set; }
        public string MAUBENHPHAMID { get; set; }
        public string ORG_CODE { get; set; }
        public string MA_BHYT { get; set; }
        public string TRANGTHAIDICHVU { get; set; }
        public object DADUYETTHUCHIENCANLAMSANG { get; set; }
        public string LOAIDOITUONG { get; set; }
        public string BHYT_LOAIID { get; set; }
        public string TIEN_MIENGIAM { get; set; }
        public string TIEN_BHYT_TRA { get; set; }
        public string TIEN_CHITRA { get; set; }
        public string SOLUONG { get; set; }
        public string TIEPNHANID { get; set; }
        public string DVLOAIKHAM { get; set; }
        public string BALOAIKHAM { get; set; }
        public object LOAIDVKB { get; set; }
        public string TINHTRANG_BN { get; set; }
        public string TINHTRANG_MAU { get; set; }
        public object CAPCUUNANG { get; set; }
        public string HINHTHUCVAOVIENID { get; set; }
        public string THAMGIABHYTDU5NAM { get; set; }
        public string TRADU6THANGLUONGCOBAN { get; set; }
        public string TYLE_BHYT { get; set; }
        public string TGLAYMAU2 { get; set; }
        public string NGUOIGIAOMAU2 { get; set; }
        public string NOIGIAOMAU { get; set; }
        public string BSCHIDINH { get; set; }
        public string NGAYNHAPVIEN { get; set; }
        public object DADUYETTHUCHIENCLS { get; set; }
        public string CANNANG { get; set; }
        public string CHIEUCAO { get; set; }
        public string DATHU { get; set; }
    }
    public class PatientHisLis
    {
        public string Patient_Id { get; set; }
        public string Patient_Name { get; set; }
        public string PatientSex { get; set; }
        public string Patient_Sex { get; set; }
        public string Patient_Addr { get; set; }
        public string Assign_Code { get; set; }
        public string Patient_Code { get; set; }
        public string Patient_Phone { get; set; }
        public string Patient_Email { get; set; }
        public DateTime? Dob { get; set; }
        public int Hos_Status { get; set; }
        public int Year_Birth { get; set; }
        public int Year_Of_Birth { get; set; }
        public string Ngay_Chidinh { get; set; }
        public DateTime? Input_date { get; set; }
        public string sInput_date { get; set; }
        public string ThoiGianLayMau { get; set; }
        public string Identify_Num { get; set; }
        public string Insurance_Num { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalBaseCode { get; set; }
        public string IcdCode { get; set; }
        public string ChanDoan { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
        public string RequestRoomName { get; set; }
        public string RequestRoomCode { get; set; }
        public string Department_Name { get; set; }
        public string Department_Code { get; set; }
        public string Department_Room_Name { get; set; }
        public string Department_Room_Code { get; set; }
        public string ObjectType_Name { get; set; }
        public string ObjectType_Code { get; set; }
        public string TreatmentTypeCode { get; set; }
        public string TreatmentTypeName { get; set; }
        public string TreatmentCode { get; set; }
        public string DoctorAssign_Name { get; set; }
        public string DoctorAssign_Code { get; set; }
        public string ExecuteRoomName { get; set; }
        public string ExecuteRoomCode { get; set; }
        public string ExecuteDepartmentName { get; set; }
        public string ExecuteDepartmentCode { get; set; }
        public string Service_Codes { get; set; }
        public DateTime? Tube_BloodCollected_Date { get; set; }
        public DateTime? NgayVaoVien { get; set; }
        public string Tube_BloodCollected_User { get; set; }
        public string Sovaovien { get; set; }
        public bool? IsStat { get; set; }
        public int QMSSequence { get; set; }
        public int Partner_Id { get; set; }
        public string Partner_Name { get; set; }
        public string Partner_ApiDomain { get; set; }
        public List<PatientReg> PatientRegList { get; set; }
    }

    public class PatientReg
    {
        public string AssignDetail_Id { get; set; }
        public string AssignDetail_Name { get; set; }
        public string Service_Id { get; set; }
        public string Service_Name { get; set; }
        public bool IsSpecimen { get; set; }
        public bool IsMap { get; set; }

        public string ServiceCode { get; set; }
        public int? Quantity { get; set; }
        public string ServiceDetail_Id { get; set; }
        public string Assign_ID { get; set; }
        public DateTime? Input_Date { get; set; }
        public DateTime? Tube_BloodCollected_Date { get; set; }
        public string Tube_BloodCollected_User { get; set; }
        public string His_LoaiBenhPham_Name { get; set; }
        public string Reg_Payment_Status { get; set; }
    }
}
