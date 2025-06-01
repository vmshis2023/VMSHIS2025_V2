using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

namespace VMS.HIS.Libs
{
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
}
