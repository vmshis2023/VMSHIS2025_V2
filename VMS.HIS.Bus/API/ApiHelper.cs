using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace VMS.API.Libs
{

    public class ApiRequestResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object Exeception { get; set; }
    }
    public class ApiResponse
    {
        public object Data { get; set; }
    }
    public enum RequestMethod
    {
        GET = 1,
        POST = 2
    }
    public class ApiHelper
    {
        public static string CallRestApi(string webServiceLink, RequestMethod method, string content)
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

                    if (method == RequestMethod.POST)
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
}
