using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Text;
using NLog;


namespace VMS.HIS.BHYT
{
   public class APIKey
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string id_token { get; set; }

        [DataMember]
        public DateTime expires_in { get; set; }
        [DataMember]
        public string token_type { get; set; }
    }

    public class ApiToken
    {
        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string password { get; set; }
    }
    public class ParamTracuu2024
    {
        public string maThe { get; set; }
        public string hoTen { get; set; }
        public string ngaySinh { get; set; }
        public string hoTenCb { get; set; }
        public string cccdCb { get; set; }
    }
    public class TheBHYT
    {
        [DataMember]
        public string maThe { get; set; }

        [DataMember]
        public string hoTen { get; set; }

        [DataMember]
        public string ngaySinh { get; set; }

    }
    public class DuLieuHoSo
    {
        public byte[] fileHS { get; set; }
        public string maThe { get; set; }
        public string hoTen { get; set; }
        public string maLK { get; set; } 

    }
    public class KQGiamDinh4210
    {
        public string maKetQua { get; set; }
        public string maGiaoDich { get; set; }
    }
    public class KQLichSuKCB
    {
        public string maKetQua { get; set; }
        public string ghiChu { get; set; }
        public string maThe { get; set; }
        public string hoTen { get; set; }
        public string ngaySinh { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string maDKBD { get; set; }
        public string cqBHXH { get; set; }
        public string gtTheTu { get; set; }
        public string gtTheDen { get; set; }
        public string maKV { get; set; }
        public string ngayDu5Nam { get; set; }
        public string maSoBHXH { get; set; }
        public string maTheCu { get; set; }
        public string maTheMoi { get; set; }
        public string maDKBDMoi { get; set; }
        public string tenDKBDMoi { get; set; }
        public string gtTheTuMoi { get; set; }
        public string gtTheDenMoi { get; set; }

        public List<LichSuKCB2018> dsLichSuKCB2018 { get; set; }
        public List<LichSuKT2018> dsLichSuKT2018 { get; set; }
    }
    public class LichSuKCB2018
    {
        public long maHoSo { get; set; }
        public string maCSKCB { get; set; }
        public string ngayVao { get; set; }
        public string ngayRa { get; set; }
        public string tenBenh { get; set; }
        public string tinhTrang { get; set; }
        public string kqDieuTri { get; set; }
        public string lyDoVV { get; set; }
    }
    public class LichSuKT2018
    {
        public string userKT { get; set; }
        public string thoiGianKT { get; set; }
        public string thongBao { get; set; }
        public string maLoi { get; set; }
    }
    public class CreateRequest
    {
        private static NLog.Logger _log;
        public static string WebRequest(string pzUrl, string pzData, string pzAuthorization, string pzMethod, string pzContentType)
        {
            try
            {
                _log = LogManager.GetLogger(string.Format("{0}", "WebRequest"));
                _log.Trace(pzUrl);
                var httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(pzUrl);
                //httpWebRequest.Timeout = 100 Timeout.Infinite;
                //httpWebRequest.KeepAlive = true;
                httpWebRequest.ContentType = pzContentType;
                httpWebRequest.Method = pzMethod;
                if (!string.IsNullOrEmpty(pzAuthorization))
                    httpWebRequest.Headers.Add("Authorization", pzAuthorization);
                httpWebRequest.Proxy = new WebProxy();//no proxy
                if (!string.IsNullOrEmpty(pzData))
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = pzData;
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                InitiateSslTrust();//bypass SSL
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var result = string.Empty;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                _log.Trace(result);
                return result;
            }
            catch (Exception ex)
            {
                _log.Trace("WebRequest " + ex.Message);
                return null;
            }

        }
        public static void InitiateSslTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class ResponseData
    {
        public bool IsSuccess { get; set; }
        public string Messge { get; set; }
        public object data { get; set; }
    }
}
