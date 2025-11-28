using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using VMS.Helpers;
using static VMS.Helpers.DonThuocQuocGiaModel;

namespace VMS.HIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DTQGController : Controller
    {
        public IConfiguration Configuration { get; set; }
        AppSettingDonThuocQG _appSettings;
        TokenCSKCB _appSettingstokenCSKCB;
        List<TokenBacSy> _appSettingstokenBacsy;
        public IWebHostEnvironment _env;
        public DTQGController(IConfiguration configuration, IWebHostEnvironment env, IOptions<AppSettingDonThuocQG> appIdentitySettingsAccessor)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Configuration = configuration;
            _env = env;
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        #region Hàm xử lý token bác sỹ và token cơ sở khám chữa bệnh  
        private List<TokenBacSy> AppsettingTokenBacSy()
        {
            List<TokenBacSy> tokenBacSy = new List<TokenBacSy>();
            try
            {
                string filePath = string.Format(@"{0}\{1}.json", _env.EnvironmentName.ToLower() == "development" ? Directory.GetCurrentDirectory() : AppContext.BaseDirectory, "TokenCSKCB");
                using (var r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    tokenBacSy = JsonConvert.DeserializeObject<List<TokenBacSy>>(json);
                }
            }
            catch (Exception ex)
            {
                return tokenBacSy;
            }
            return tokenBacSy;
        }
        private TokenCSKCB AppsettingTokenCSKCB()
        {
            TokenCSKCB tokenCSKCB = new TokenCSKCB();
            try
            {
                string filePath = string.Format(@"{0}\{1}.json", _env.EnvironmentName.ToLower() == "development" ? Directory.GetCurrentDirectory() : AppContext.BaseDirectory, "TokenCSKCB");
                using (var r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    tokenCSKCB = JsonConvert.DeserializeObject<TokenCSKCB>(json);
                }
            }
            catch (Exception ex)
            {
                return tokenCSKCB;
            }
            return tokenCSKCB;
        }

        private string getTokenCSKCB()
        {
            try
            {
                _appSettingstokenCSKCB = AppsettingTokenCSKCB();
                Utility.Log = Utility.LogFactory.GetLogger(nameof(getTokenCSKCB));
                Utility.Log.Debug("-----------------------------------------");
                string url = _appSettings.LinkAPI + _appSettings.APIgetToken;
                Utility.Log.Trace($"url  : {url}");
                var returnTokenCSKCB = _appSettingstokenCSKCB;
                if (_appSettingstokenCSKCB == null || string.IsNullOrEmpty(_appSettingstokenCSKCB.token) || Convert.ToDateTime(_appSettingstokenCSKCB.expire_date) < DateTime.Now)
                {
                    Dangnhap objdangnhap = new Dangnhap();
                    objdangnhap.ma_lien_thong_co_so_kham_chua_benh = _appSettings.Ma_lien_thong_co_so_kham;
                    objdangnhap.password = _appSettings.Password;
                    var input = JsonConvert.SerializeObject(objdangnhap);
                   
                    Utility.Log.Trace("Request data ={0}", input);
                    string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
                    Utility.Log.Trace("Response data={0}", result);
                    if (!string.IsNullOrEmpty(result))
                    {
                        TokenCSKCB obj = JsonConvert.DeserializeObject<TokenCSKCB>(result);
                        if (obj != null && !string.IsNullOrEmpty(obj.token))
                        {
                            obj.create_date = DateTime.Now.ToString();
                            obj.expire_date = DateTime.Now.AddDays(7).ToString();
                            _appSettingstokenCSKCB = obj;
                            Utility.AddOrUpdateSetting("TokenCSKCB", Configuration, _env.EnvironmentName, "TokenCSKCB", JsonConvert.SerializeObject(obj, Formatting.Indented));
                        }
                        else
                        {
                            return String.Empty;
                        }

                        return obj.token;
                    }
                }
                return _appSettingstokenCSKCB.token;
            }
            catch (Exception ex)
            {
                Utility.Log.Debug(ex.Message);
                return null;
            }

        }
        private string getTokenBacSy(string ma_lien_thong_bac_si, string password)
        {
            try
            {
                _appSettingstokenBacsy = AppsettingTokenBacSy();
                Utility.Log = Utility.LogFactory.GetLogger(nameof(getTokenBacSy));
                Utility.Log.Trace("-----------------------------------------");
                string url = _appSettings.LinkAPI + _appSettings.APIdangnhapbacsi;
                Utility.Log.Trace($"url  : {url}");
                TokenBacSy returnBacSi = _appSettingstokenBacsy.Where(x => x.ma_lien_thong_bac_si == ma_lien_thong_bac_si).FirstOrDefault();
                if (returnBacSi == null || string.IsNullOrEmpty(returnBacSi.token) || Convert.ToDateTime(returnBacSi.expire_date) < DateTime.Now)
                {
                    DangNhapBacSy objdangnhap = new DangNhapBacSy();
                    objdangnhap.ma_lien_thong_co_so_kham_chua_benh = _appSettings.Ma_lien_thong_co_so_kham;
                    objdangnhap.password = password;
                    objdangnhap.ma_lien_thong_bac_si = ma_lien_thong_bac_si;
                    var input = JsonConvert.SerializeObject(objdangnhap);
                    string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
                    Utility.Log.Debug($"Dữ liệu trả về : {result}");
                    if (!string.IsNullOrEmpty(result))
                    {
                        TokenBacSy obj = JsonConvert.DeserializeObject<TokenBacSy>(result);
                        if (obj != null && !string.IsNullOrEmpty(obj.token))
                        {
                            if (returnBacSi != null)
                            {
                                _appSettingstokenBacsy.Remove(returnBacSi);
                            }
                            obj.create_date = DateTime.Now.ToString();
                            obj.expire_date = DateTime.Now.AddDays(7).ToString();
                            _appSettingstokenBacsy.Add(obj);
                            Utility.AddOrUpdateSetting("TokenBacSy", Configuration, _env.EnvironmentName, "TokenBacSy", JsonConvert.SerializeObject(_appSettingstokenCSKCB, Formatting.Indented));
                        }
                        else
                        {
                            return String.Empty;
                        }

                        return obj.token;
                    }
                }
                return _appSettingstokenCSKCB.token;
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
                return null;
            }
        }
        #endregion

        /// <summary>
        ///  Hàm thực hiện đăng nhập bác sỹ 
        /// </summary>
        /// <param name="objbacsidangnhap"></param>
        /// <returns></returns>
        [HttpPost("DangNhapBacSy")]
        public async Task<IActionResult> dangnhapBacSy(DangNhapBacSy objbacsidangnhap)
        {
            ResponseData responseData = new ResponseData();
            Utility.Log = LogManager.GetLogger("dangnhapBacSy");
            Utility.Log.Trace("_________Begin________");
            try
            {
                string url = _appSettings.LinkAPI + _appSettings.APIdangnhapbacsi;
                objbacsidangnhap.ma_lien_thong_co_so_kham_chua_benh = _appSettings.Ma_lien_thong_co_so_kham;
                var input = JsonConvert.SerializeObject(objbacsidangnhap);
                string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
                if (!string.IsNullOrEmpty(result))
                {
                    responseData.IsSuccess = true;
                    responseData.Messge = JsonConvert.SerializeObject(result);
                    responseData.data = result;
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "Mã liên thông hoặc mật khẩu không đúng";
                    responseData.data = result;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }

        /// <summary>
        /// Hàm thực hiện đăng nhập cơ sở khám chữa bệnh
        /// </summary>
        /// <param name="objbacsidangnhap"></param>
        /// <returns></returns>
        [HttpPost("DangNhapCSKCB")]
        public async Task<IActionResult> DangnhapCSKCB()
        {
            ResponseData responseData = new ResponseData();
            Utility.Log = LogManager.GetLogger("dangnhapCSKCB");
            Utility.Log.Trace("_________Begin________");
            try
            {
                string url = _appSettings.LinkAPI + _appSettings.APIgetToken;
                Dangnhap dangnhap = new Dangnhap();
                dangnhap.ma_lien_thong_co_so_kham_chua_benh = _appSettings.Ma_lien_thong_co_so_kham;
                dangnhap.password = _appSettings.Password;
                var input = JsonConvert.SerializeObject(dangnhap);
                Utility.Log.Trace("url={0}", url);
                Utility.Log.Trace("Request data ={0}", input);
                string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
                Utility.Log.Trace("Response data={0}", result);
                if (!string.IsNullOrEmpty(result))
                {
                    responseData.IsSuccess = true;
                    responseData.Messge = JsonConvert.SerializeObject(result);
                    responseData.data = result;
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "Mã liên thông cơ sở khám chữa bệnh hoặc mật khẩu không đúng";
                    responseData.data = result;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }

        /// <summary>
        /// Hàm thực hiện thêm bác sỹ
        /// </summary>
        /// <returns></returns>
        [HttpPost("ThemBacSy")]
        public async Task<IActionResult> ThemBacSy(Bacsy objBacsy)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                string tokenCSKCB = getTokenCSKCB();
                Utility.Log = LogManager.GetLogger("ThemBacSy");
                Utility.Log.Trace("_________Begin________");
                string url = _appSettings.LinkAPI + _appSettings.APIthembacsi;
                var input = JsonConvert.SerializeObject(objBacsy);
                Utility.Log.Trace("url={0}", url);
                Utility.Log.Trace("Request data ={0}", input);

                string result = CreateRequest.WebRequest(url, input, "Bearer " + tokenCSKCB, "POST", "application/json");
                Utility.Log.Trace("Response data={0}", result);
                if (!string.IsNullOrEmpty(result))
                {
                    responseData.IsSuccess = true;
                    responseData.Messge = JsonConvert.SerializeObject(result);
                    responseData.data = result;
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "Mã liên thông cơ sở khám chữa bệnh hoặc mật khẩu không đúng";
                    responseData.data = result;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }
        /// <summary>
        /// Hàm thực hiện thêm bác sỹ
        /// </summary>
        /// <returns></returns>
        [HttpPost("XoaBacSy")]
        public async Task<IActionResult> XoaBacSy(Bacsy objBacsy)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                string tokenCSKCB = getTokenCSKCB();
                Utility.Log = LogManager.GetLogger("XoaBacSy");
                Utility.Log.Trace("_________Begin________");
                string url = _appSettings.LinkAPI + _appSettings.APIxoabacsi;
                var input = JsonConvert.SerializeObject(objBacsy);
                Utility.Log.Trace("url={0}", url);
                Utility.Log.Trace("Request data ={0}", input);
                string result = CreateRequest.WebRequest(url, input, "Bearer " + tokenCSKCB, "POST", "application/json");
                Utility.Log.Trace("Response data={0}", result);
                if (!string.IsNullOrEmpty(result))
                {
                    responseData.IsSuccess = true;
                    responseData.Messge = JsonConvert.SerializeObject(result);
                    responseData.data = result;
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "Không tồn tại mã liên thông bác sỹ cho cơ sở kcb";
                    responseData.data = result;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }
        /// <summary>
        /// Hàm thực hiện gửi đơn thuốc lên cổng đơn thuốc quốc gia 
        /// </summary>
        /// <param name="objguidonthuoc"></param>
        /// <returns></returns>
        [HttpPost("GuiDonThuoc")]
        public async Task<IActionResult> GuiDonThuoc([FromBody] Guidonthuoc objguidonthuoc)
        {
            ResponseData responseData = new ResponseData();

            try
            {
                Utility.Log.Trace("Bắt đầu gửi đơn thuốc với uid={0}, pwd=[1}", objguidonthuoc.ma_lien_thong_bac_si, objguidonthuoc.password);
                string tokenBacsy = getTokenBacSy(objguidonthuoc.ma_lien_thong_bac_si, objguidonthuoc.password);
                Utility.Log = LogManager.GetLogger("guiDonThuoc");
                Utility.Log.Trace("Lấy được token {0}", tokenBacsy);
                if (string.IsNullOrEmpty(tokenBacsy))
                {
                    Utility.Log.Trace("Lấy token đăng nhập bác sỹ không thành công {0}", tokenBacsy);
                    responseData.IsSuccess = false;
                    responseData.Messge = "Lấy token đăng nhập bác sỹ không thành công!";
                    responseData.data = JsonConvert.SerializeObject(objguidonthuoc);
                }
                else
                {
                    Utility.Log.Trace("guiDonThuocID: " + objguidonthuoc.id_donthuoc_tt);
                    string url = _appSettings.LinkAPI + _appSettings.APIguidonthuoc;

                    var input = JsonConvert.SerializeObject(objguidonthuoc);
                    Utility.Log.Trace("url={0}", url);
                    Utility.Log.Trace("Request data ={0}", input);
                    string result = CreateRequest.WebRequest(url, input, "Bearer " + tokenBacsy, "POST", "application/json");
                    Utility.Log.Trace("Response data={0}", result);
                    if (!string.IsNullOrEmpty(result))
                    {
                        var objResult = JsonConvert.DeserializeObject<GuiDonThuocResponse>(result);
                        if (objResult != null)
                        {
                            if (Utility.sDbnull(objResult.success, "") != "")
                            {
                                responseData.IsSuccess = true;
                                responseData.Messge = "Thành công";
                                responseData.data = JsonConvert.SerializeObject(objResult);
                            }
                            else
                            {
                                responseData.IsSuccess = false;
                                responseData.Messge = JsonConvert.SerializeObject(objResult.error);
                                responseData.data = JsonConvert.SerializeObject(objResult);
                            }
                        }
                        else
                        {
                            responseData.IsSuccess = false;
                            responseData.Messge = "objResult is null";
                            responseData.data = null;
                        }
                    }
                    else
                    {
                        responseData.IsSuccess = false;
                        responseData.Messge = "result is null";
                        responseData.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }

        [HttpGet("LayDonThuoc")]
        public async Task<IActionResult> LayDonThuoc(string ma_don_thuoc)
        {
            ResponseData responseData = new ResponseData();
            Utility.Log = LogManager.GetLogger("LayDonThuoc");
            Utility.Log.Trace("_________Begin________");
            try
            {
                Utility.Log.Trace("LayDonThuoc: " + ma_don_thuoc);
                string url = _appSettings.LinkAPI + _appSettings.APIlaydonthuoc;
                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("app-name", _appSettings.AppName);
                header.Add("app-key", _appSettings.AppKey);
                string result = CreateRequest.WebRequest(url, ma_don_thuoc, "", header, "POST", "application/json");
                if (string.IsNullOrEmpty(result))
                {
                    var objResult = JsonConvert.DeserializeObject<GuiDonThuocResponse>(result);
                    if (objResult != null)
                    {
                        if (Utility.sDbnull(objResult.success, "") != "")
                        {
                            responseData.IsSuccess = true;
                            responseData.Messge = "Thành công";
                            responseData.data = JsonConvert.SerializeObject(objResult);
                        }
                        else
                        {
                            responseData.IsSuccess = false;
                            responseData.Messge = JsonConvert.SerializeObject(objResult.error);
                            responseData.data = JsonConvert.SerializeObject(objResult);
                        }
                    }
                    else
                    {
                        responseData.IsSuccess = false;
                        responseData.Messge = "objResult is null";
                        responseData.data = null;
                    }
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "result is null";
                    responseData.data = null;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);
        }

        [HttpPost("CapnhapSoluongBan")]
        public async Task<IActionResult> capNhatSoLuongBan([FromBody] capnhatsoluongban objcapnhatsoluongban)
        {
            ResponseData responseData = new ResponseData();
            Utility.Log = Utility.LogFactory.GetLogger(nameof(capNhatSoLuongBan));
            Utility.Log.Debug("-------Begin---------------");
            try
            {
                string url = _appSettings.LinkAPI + _appSettings.APIcapnhatdonthuoc;
                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("app-name", _appSettings.AppName);
                header.Add("app-key", _appSettings.AppKey);
                Utility.Log.Debug("url: " + url);
                Utility.Log.Debug("header: " + header);
                Utility.Log.Debug("data: " + JsonConvert.SerializeObject(objcapnhatsoluongban));
                var input = JsonConvert.SerializeObject(objcapnhatsoluongban);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("app-name", _appSettings.AppName);
                headers.Add("app-key", _appSettings.AppKey);
                string result = CreateRequest.WebRequest(url, input, "", headers, "POST", "application/json");
                Utility.Log.Debug($"result : {result}");
                if (!string.IsNullOrEmpty(result))
                {
                    responseData.IsSuccess = true;
                    responseData.Messge = "Thành công";
                    responseData.data = JsonConvert.SerializeObject(result);
                }
                else
                {
                    responseData.IsSuccess = false;
                    responseData.Messge = "Không thành công";
                    responseData.data = null;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Debug(ex.Message);
            }
            Utility.Log.Trace("_________End________");
            return new JsonResult(responseData);

        }

    }
}
