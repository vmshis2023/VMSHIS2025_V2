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
using VMS.Invoice;

namespace VMS.HIS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MisaInvoiceController : Controller
	{
		private AppSettingMisaInvoices _appSettings;

		public IWebHostEnvironment _env;

		public IConfiguration Configuration { get; set; }
		private string MISA_TOKEN;
		public MisaInvoiceController(IConfiguration configuration, IWebHostEnvironment env, IOptions<AppSettingMisaInvoices> appIdentitySettingsAccessor)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Configuration = configuration;
			_env = env;
			_appSettings = appIdentitySettingsAccessor.Value;
			Utility.Log = LogManager.GetLogger("GetToken");
			string uuidfile = env.ContentRootPath + @"\ExpireDate_Token.txt";
			bool isValid = System.IO.File.Exists(uuidfile);
			string timeString = "";
			List<string> lines = new List<string>();
			if (isValid)
			{
				lines = System.IO.File.ReadAllLines(uuidfile).ToList<string>();
				timeString = lines[0];
				DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(timeString);

				// Chuyển từ DateTimeOffset sang DateTime (giữ múi giờ)
				DateTime dateTime = dateTimeOffset.DateTime;
				isValid = dateTime.CompareTo(DateTime.Now) > 0;
			}
			if (!isValid)
			{
				MISA_TOKEN = GetToken();
				DateTime expired_date = DateTime.Now.AddHours(24);
				Try2SaveFile(uuidfile, expired_date.ToString("yyyy-MM-ddTHH:mm:ss.sssssssK"), MISA_TOKEN);
				Utility.Log.Trace("Lấy token mới  {0} với ngày hết hạn token = {1}", MISA_TOKEN, expired_date.ToString("yyyy-MM-ddTHH:mm:ss.sssssssK"));
			}
			else
			{
				Utility.Log.Trace("Token cũ vẫn đang trong hạn sử dụng tới hạn {0}", timeString);
				MISA_TOKEN = lines[1];// AppSettings.VNPTTokenObject.result.UUID;
			}
		}
		bool Try2SaveFile(string fileName, string expiredate, string uuid)
		{
			try
			{
				using (StreamWriter _Writer = new StreamWriter(fileName))
				{
					_Writer.WriteLine(expiredate);
					_Writer.WriteLine(uuid);
					_Writer.Flush();
					_Writer.Close();
				}
				return true;
			}
			catch (Exception ex)
			{
				Utility.Log.Error("Lỗi khi lưu thời gian sử dụng token {0}", ex.ToString());
				return false;
			}


		}
		private string GetToken()
		{
			Utility.Log = LogManager.GetLogger("GetToken");
			string token = "";
			try
			{
				MisaResponse responseData = new MisaResponse();
				string url = _appSettings.LinkAPI + _appSettings.API_Token;
				MisaTokenModel misaToken = new MisaTokenModel();
				misaToken.appid = _appSettings.appid;
				misaToken.taxcode = _appSettings.taxcode;
				misaToken.username = _appSettings.username;
				misaToken.password = _appSettings.password;
				string input = JsonConvert.SerializeObject(misaToken);
				Utility.Log.Trace("Request-->" +input);
				string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
				Utility.Log.Trace("Response-->" + result);
				if (!string.IsNullOrEmpty(result))
				{
					JsonSerializerSettings settings = new JsonSerializerSettings();
					settings.NullValueHandling = NullValueHandling.Ignore;
					settings.MissingMemberHandling = MissingMemberHandling.Ignore;
					responseData = JsonConvert.DeserializeObject<MisaResponse>(result, settings);
					if (responseData == null)
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "responseData is null";
						responseData.Data = null;
						responseData.CustomData = null;
						return token;
					}
					return responseData.Data.ToString();
				}
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = "result is null";
				responseData.Data = null;
				responseData.CustomData = null;
				return token;
			}
			catch (Exception)
			{
				return "";
			}
		}

		/// <summary>
		/// Hàm thực hiện get token từ misa 
		/// </summary>
		/// <returns></returns>
		[HttpPost("lay_token")]
		public async Task<MisaResponse> DangNhap()
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("GetToken");
			try
			{
				string url = _appSettings.LinkAPI + _appSettings.API_Token;
				MisaTokenModel misaToken = new MisaTokenModel();
				misaToken.appid = _appSettings.appid;
				misaToken.taxcode = _appSettings.taxcode;
				misaToken.username = _appSettings.username;
				misaToken.password = _appSettings.password;
				string input = JsonConvert.SerializeObject(misaToken);
				Utility.Log.Trace("Request-->" +input);
				string result = CreateRequest.WebRequest(url, input, "", "POST", "application/json");
				Utility.Log.Trace("Response-->" + result);
				if (!string.IsNullOrEmpty(result))
				{
					JsonSerializerSettings settings = new JsonSerializerSettings();
					settings.NullValueHandling = NullValueHandling.Ignore;
					settings.MissingMemberHandling = MissingMemberHandling.Ignore;
					responseData = JsonConvert.DeserializeObject<MisaResponse>(result, settings);
					if (responseData == null)
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "responseData is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
				}
				else
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "result is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện lấy danh sách thông báo phát hành hóa đơn 
		/// </summary>
		/// <returns></returns>
		[HttpGet("lay_danhsach_mauhoadon")]
		public async Task<MisaResponse> lay_danhsach_mauhoadon(string sDataRequest)
		{
			MisaResponse responseData = new MisaResponse();
			int year = Utility.Int32Dbnull(sDataRequest, -1);
			Utility.Log = LogManager.GetLogger("lay_danhsach_mauhoadon");
			Utility.Log.Trace("_________Begin________");
			try
			{
				string text = _appSettings.LinkAPI + _appSettings.API_Templete;
				string url = ((year <= 0) ? (_appSettings.LinkAPI + _appSettings.API_Templete + $"&year={DateTime.Now.Year}") : (_appSettings.LinkAPI + _appSettings.API_Templete + $"&year={year}"));
				string token = MISA_TOKEN;
				if (string.IsNullOrEmpty(token))
				{
					Utility.Log.Trace("Request: Token is null");
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "token is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					Utility.Log.Trace("Request with url={0},token={1}: ",url, token);
					string result = WebApiHelper.CallRestApi(url,"GET", token,"",true);
					Utility.Log.Trace("result: " + result);
					if (!string.IsNullOrEmpty(result))
					{
						responseData = JsonConvert.DeserializeObject<MisaResponse>(result);
						if (responseData != null && responseData.Data != null && responseData.Success)
						{
							JsonSerializerSettings settings = new JsonSerializerSettings();
							settings.NullValueHandling = NullValueHandling.Ignore;
							settings.MissingMemberHandling = MissingMemberHandling.Ignore;
							List<TemplateData> lsttemplateData = JsonConvert.DeserializeObject<List<TemplateData>>(responseData.Data.ToString(), settings);
							TemplateData templateDatum = lsttemplateData[0];
							responseData.Success = true;
							responseData.ErrorCode = null;
							responseData.Errors = "";
							responseData.Data = lsttemplateData;
							responseData.CustomData = null;
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "responseData is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện phát hành hóa đơn Máy Tính Tiền (MTT) 
		/// </summary>
		/// <returns></returns>
		[HttpPost("phathanh_hoadon")]
		public async Task<MisaResponse> phathanh_hoadon(MisaPhatHanhHoaDon dataSendInvoices)
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("phathanh_hoadon");
			Utility.Log.Trace("_________Begin________");
			string url = _appSettings.LinkAPI + _appSettings.API_Phathanh;
			string dataRequest = JsonConvert.SerializeObject(dataSendInvoices);
			try
			{
				Utility.Log.Trace("Url-->" + url);
				Utility.Log.Trace("Request-->" + dataRequest);
				if (string.IsNullOrEmpty(dataRequest))
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "dataRequest is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					
					string token = MISA_TOKEN;
					if (string.IsNullOrEmpty(token))
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "token is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
					else
					{
						string result = WebApiHelper.CallRestApi(url, "POST", token, dataRequest, true);
						Utility.Log.Trace("Response-->" + result);
						//string result = CreateRequest.WebRequest(url, dataRequest, "Bearer " + token, "POST", "application/json");
						if (!string.IsNullOrEmpty(result))
						{
							MisaResonce_HD temp = JsonConvert.DeserializeObject<MisaResonce_HD>(result);
							if (temp != null && temp.publishInvoiceResult != null && temp.success)
							{
								JsonSerializerSettings settings = new JsonSerializerSettings();
								settings.NullValueHandling = NullValueHandling.Ignore;
								settings.MissingMemberHandling = MissingMemberHandling.Ignore;
								List<ResponseMinvoices> lstMinvoices = JsonConvert.DeserializeObject<List<ResponseMinvoices>>(temp.publishInvoiceResult, settings);
								ResponseMinvoices responseMinvoice = lstMinvoices[0];
								responseData.Success = true;
								responseData.ErrorCode = null;
								responseData.Errors = "";
								responseData.Data = lstMinvoices;
								responseData.CustomData = null;
							}
							else
							{
								responseData.Success = false;
								responseData.ErrorCode = null;
								responseData.Errors = "responseData is null";
								responseData.Data = null;
								responseData.CustomData = null;
							}
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "result is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện xem trước Hóa Đơn 
		/// </summary>
		/// <returns></returns>
		[HttpPost("xemtruoc_hoadon")]
		public async Task<MisaResponse> xemtruoc_hoadon(Orginvoicedata dataPreviewInvoice)
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("xemtruoc_hoadon");
			Utility.Log.Trace("_________Begin________");
			string url = _appSettings.LinkAPI + _appSettings.API_XemtruocHD;
			string dataRequest = JsonConvert.SerializeObject(dataPreviewInvoice);
			Utility.Log.Trace("Url-->" + url);
			Utility.Log.Trace("Request-->" + dataRequest);
			try
			{
				
				if (string.IsNullOrEmpty(dataRequest))
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "dataRequest is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					
					string token = MISA_TOKEN;
					if (string.IsNullOrEmpty(token))
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "token is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
					else
					{
						string result = WebApiHelper.CallRestApi(url, "POST", token, dataRequest, true);// CreateRequest.WebRequest(url, dataRequest, "Bearer " + token, "POST", "application/json");
						Utility.Log.Trace("Response-->" + result);
						if (!string.IsNullOrEmpty(result))
						{
							responseData = JsonConvert.DeserializeObject<MisaResponse>(result);
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "result is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện tải hóa đơn Máy Tính Tiền (MTT) 
		/// </summary>
		/// <returns></returns>
		[HttpPost("tai_hoadon")]
		public async Task<MisaResponse> tai_hoadon([FromBody] downloadModel objdownloadModel)
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("tai_hoadon");
			Utility.Log.Trace("_________Begin________");
			List<string> lstdata = objdownloadModel.maTracuu.Split(',').ToList<string>();
			string dataRequest = JsonConvert.SerializeObject(lstdata);
			string url = _appSettings.LinkAPI + _appSettings.API_TaiHoaDon;
			url = url.Replace("{1}", "pdf");
			Utility.Log.Trace("Url-->" + url);
			Utility.Log.Trace("Request-->" + dataRequest);
			try
			{
				if (string.IsNullOrEmpty(dataRequest))
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "dataRequest is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					string token = MISA_TOKEN;
					if (string.IsNullOrEmpty(token))
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "token is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
					else
					{
						string result = CreateRequest.WebRequest(url, dataRequest, "Bearer " + token, "POST", "application/json");
						Utility.Log.Trace("Response-->" + result);
						if (!string.IsNullOrEmpty(result))
						{
							responseData = JsonConvert.DeserializeObject<MisaResponse>(result);
							if (responseData != null && responseData.Data != null && responseData.Success)
							{
								JsonSerializerSettings settings = new JsonSerializerSettings();
								settings.NullValueHandling = NullValueHandling.Ignore;
								settings.MissingMemberHandling = MissingMemberHandling.Ignore;
								List<ResponseFileMinvoices> lstMinvoices = JsonConvert.DeserializeObject<List<ResponseFileMinvoices>>(responseData.Data.ToString(), settings);
								ResponseFileMinvoices responseFileMinvoice = lstMinvoices[0];
								responseData.Success = true;
								responseData.ErrorCode = null;
								responseData.Errors = "";
								responseData.Data = lstMinvoices;
								responseData.CustomData = null;
							}
							else
							{
								responseData.Success = false;
								responseData.ErrorCode = null;
								responseData.Errors = "responseData is null";
								responseData.Data = null;
								responseData.CustomData = null;
							}
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "result is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện tải hóa đơn Máy Tính Tiền (MTT) 
		/// </summary>
		/// <returns></returns>
		[HttpPost("huy_hoadon")]
		public async Task<MisaResponse> huy_hoadon([FromBody] DataCancelMisa dataCancelMisa)
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("huy_hoadon");
			Utility.Log.Trace("_________Begin________");
			string sDataRequest = JsonConvert.SerializeObject(dataCancelMisa);
			string url = _appSettings.LinkAPI + _appSettings.API_HuyHoaDon;
			Utility.Log.Trace("Url-->" + url);
			Utility.Log.Trace("Request-->" + sDataRequest);
			try
			{
				if (string.IsNullOrEmpty(sDataRequest))
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "dataRequest is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					
					string token = MISA_TOKEN;
					if (string.IsNullOrEmpty(token))
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "token is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
					else
					{
						string result = CreateRequest.WebRequest(url, sDataRequest, "Bearer " + token, "POST", "application/json");
						Utility.Log.Trace("Response: " + result);
						if (!string.IsNullOrEmpty(result))
						{
							responseData = JsonConvert.DeserializeObject<MisaResponse>(result);
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "result is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}

		/// <summary>
		/// Hàm thực hiện tải hóa đơn Máy Tính Tiền (MTT) 
		/// </summary>
		/// <returns></returns>
		[HttpPost("lay_trangthai_hoadon")]
		public async Task<MisaResponse> lay_trangthai_hoadon([FromBody] string sDataRequest)
		{
			MisaResponse responseData = new MisaResponse();
			Utility.Log = LogManager.GetLogger("lay_trangthai_hoadon");
			Utility.Log.Trace("_________Begin________");
			List<string> lstdata = new List<string>();
			lstdata.Add(sDataRequest);
			string dataRequest = JsonConvert.SerializeObject(lstdata);
			string url = _appSettings.LinkAPI + _appSettings.API_LayTrangThai;
			url = url.Replace("{1}", "pdf");
			Utility.Log.Trace("Url-->" + url);
			Utility.Log.Trace("Request-->" + sDataRequest);
			try
			{
				if (string.IsNullOrEmpty(dataRequest))
				{
					responseData.Success = false;
					responseData.ErrorCode = null;
					responseData.Errors = "dataRequest is null";
					responseData.Data = null;
					responseData.CustomData = null;
				}
				else
				{
					
					string token = MISA_TOKEN;
					if (string.IsNullOrEmpty(token))
					{
						responseData.Success = false;
						responseData.ErrorCode = null;
						responseData.Errors = "token is null";
						responseData.Data = null;
						responseData.CustomData = null;
					}
					else
					{
						string result = CreateRequest.WebRequest(url, dataRequest, "Bearer " + token, "POST", "application/json");
						Utility.Log.Trace("Response: " + result);
						if (!string.IsNullOrEmpty(result))
						{
							responseData = JsonConvert.DeserializeObject<MisaResponse>(result);
							if (responseData != null && responseData.Data != null && responseData.Success)
							{
								JsonSerializerSettings settings = new JsonSerializerSettings();
								settings.NullValueHandling = NullValueHandling.Ignore;
								settings.MissingMemberHandling = MissingMemberHandling.Ignore;
								List<InvoiceStatusInRefID> lstMinvoices = JsonConvert.DeserializeObject<List<InvoiceStatusInRefID>>(responseData.Data.ToString(), settings);
								responseData.Success = true;
								responseData.ErrorCode = null;
								responseData.Errors = "";
								responseData.Data = lstMinvoices;
								responseData.CustomData = null;
							}
							else
							{
								responseData.Success = false;
								responseData.ErrorCode = null;
								responseData.Errors = "responseData is null";
								responseData.Data = null;
								responseData.CustomData = null;
							}
						}
						else
						{
							responseData.Success = false;
							responseData.ErrorCode = null;
							responseData.Errors = "result is null";
							responseData.Data = null;
							responseData.CustomData = null;
						}
					}
				}
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.ErrorCode = null;
				responseData.Errors = ex.Message;
				responseData.Data = null;
				responseData.CustomData = null;
			}
			Utility.Log.Trace("_________End________");
			return responseData;
		}
	}
}
