using System;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using VNS.Libs;

namespace VMS.HIS.BHYT
{
    public class CheckTheBH
    {
        //private LichSuKCBClient _lichSuKcbClient;
        private Logger _log;
        public CheckTheBH()
        {
            LogConfig();
            _log = LogManager.GetLogger("CheckTheBH");
            //if (Utility.sDbnull(globalVariables.BHXH_WebPath, "") == "")
            //{
                globalVariables.BHXH_WebPath = Utility.Laygiatrithamsohethong("BHXH_LINK","",true);// CommonBusiness.BHXH_GetWebPath(globalVariables.MA_COSO);

            //}
            _log.Trace("Load URL CHECKBHXH = " + globalVariables.BHXH_WebPath);
            //XmlDocument doc = new XmlDocument();
            //doc.WriteTo(xmlWriter917);
            //MemoryStream ms = new MemoryStream();
            //doc.Save(ms);
            //bytes = ms.ToArray();
            //globalVariables.BHXH_WebPath = "http://localhost:8044";
            //if (Utility.sDbnull(globalVariables.BHXH_WebPath, "") != "")
            //{
            //    BasicHttpBinding basicHttpbinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            //    basicHttpbinding.Name = "BasicHttpBinding_ILichSuKCB";
            //    basicHttpbinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            //    basicHttpbinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            //    basicHttpbinding.UseDefaultWebProxy = false;
            //    basicHttpbinding.MaxReceivedMessageSize = 2147483647;
            //    basicHttpbinding.ReaderQuotas.MaxArrayLength = 2147483647;
            //    basicHttpbinding.ReaderQuotas.MaxDepth = 2147483647;
            //    basicHttpbinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            //    basicHttpbinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            //    basicHttpbinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            //    var endpointAddress = new EndpointAddress(globalVariables.BHXH_WebPath);
            //    _lichSuKcbClient = new LichSuKCBClient(basicHttpbinding, endpointAddress);
            //}
        }
        public static void LogConfig()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget
                {
                    FileName =
                        "${basedir}/AppLogs/${date:format=yy}${date:format=MM}${date:format=dd}/${logger}.log",
                    Layout =
                        "${date:format=dd/MM/yyy HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}",
                    ArchiveAboveSize = 5242880
                };
                config.AddTarget("file", fileTarget);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
            }
        }
        public KQLichSuKCB KiemTraTheBh366(TheBHYT objTheBhyt)
        {
            var objLichSuKcb = new KQLichSuKCB();
            try
            {
                string url = globalVariables.BHXH_WebPath;
                string apiLink = url + "/api/BHXH/KiemTraTheBHYT2024";
                const string contentType = "application/json";
                const string meThod = "POST";


                ParamTracuu2024 objTracuu2024 = new ParamTracuu2024();
                objTracuu2024.maThe = objTheBhyt.maThe;
                objTracuu2024.hoTen = objTheBhyt.hoTen;
                objTracuu2024.ngaySinh = objTheBhyt.ngaySinh;

                objTracuu2024.hoTenCb = Utility.sDbnull(globalVariables.hoten_canbo, "");
                objTracuu2024.cccdCb = Utility.sDbnull(globalVariables.cccd_canbo, "");

                string stringData = JsonConvert.SerializeObject(objTheBhyt);
                string result = CreateRequest.WebRequest(apiLink, stringData, "", meThod, contentType);
                //_log.Trace("result: " + result);
                if (!string.IsNullOrEmpty(result))
                {
                    var response = JsonConvert.DeserializeObject<ResponseData>(result);
                    if (response != null)
                    {
                        if (response.IsSuccess)
                        {
                            objLichSuKcb = JsonConvert.DeserializeObject<KQLichSuKCB>(response.data.ToString());
                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
                    //objLichSuKcb = JsonConvert.DeserializeObject<KQLichSuKCB>(result);
                    //if (objLichSuKcb != null)
                    //{
                    //    return objLichSuKcb;
                    //}
                }
            }
            catch (Exception ex)
            {
                _log.Trace(ex.Message);
            }
            return objLichSuKcb;
        }

        public string kiemtra_thongtin_bhyt(TheBHYT objApiTheBhyt, ref KQLichSuKCB objLichSuKcbbs,
            ref bool ketqua)
        {
            try
            {
                KQLichSuKCB objLichSuKcb = KiemTraTheBh366(objApiTheBhyt);
                if (objLichSuKcb == null)
                {
                    return "Dữ liệu cổng đang lỗi";
                }
                objLichSuKcbbs = objLichSuKcb;
                if (objLichSuKcb.maKetQua != "000" && objLichSuKcb.maKetQua != "001" && objLichSuKcb.maKetQua != "002" &&
                    objLichSuKcb.maKetQua != "003" && objLichSuKcb.maKetQua != "004")
                {
                    ketqua = false;
                }
                else
                {
                    ketqua = true;
                }
                if (!string.IsNullOrEmpty(objLichSuKcb.ghiChu))
                {
                    return objLichSuKcb.ghiChu;
                }
                else
                {
                    return "Không có dữ liệu trả về";
                }

            }
            catch (Exception ex)
            {
                _log.Trace(ex.Message);
                return ex.Message;
            }
        }
        public string kiemtra_thongtin_bhyt_ravien(TheBHYT objApiTheBhyt, ref KQLichSuKCB objLichSuKcbbs,
           ref bool ketqua)
        {
            try
            {
                KQLichSuKCB objLichSuKcb = KiemTraTheBh366(objApiTheBhyt);
                if (objLichSuKcb == null)
                {
                    return "Dữ liệu cổng đang lỗi";
                }
                objLichSuKcbbs = objLichSuKcb;
                if (objLichSuKcb.maKetQua != "000" && objLichSuKcb.maKetQua != "001" && objLichSuKcb.maKetQua != "002" &&
                    objLichSuKcb.maKetQua != "003" && objLichSuKcb.maKetQua != "004" && objLichSuKcb.maKetQua != "010")
                {
                    ketqua = false;
                }
                else
                {
                    ketqua = true;
                }
                if (!string.IsNullOrEmpty(objLichSuKcb.ghiChu))
                {
                    return objLichSuKcb.ghiChu;
                }
                else
                {
                    return "Không có dữ liệu trả về";
                }

            }
            catch (Exception ex)
            {
                _log.Trace(ex.Message);
                return ex.Message;
            }
        }
    }
}
