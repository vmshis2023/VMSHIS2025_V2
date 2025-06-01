using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VMS.Invoice;

namespace VMS.HIS.API
{
    public class WebApiHelper
    {
        public static async Task<string> CallRestApiFormData(string url, Dictionary<string, string> requestParameters, string userAgent = "", MemoryStream file = null)
        {

            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(userAgent))
            {
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            }
            //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");

            var content = new FormUrlEncodedContent(requestParameters);

            var result = await client.PostAsync(url, content);

            var responseString = await result.Content.ReadAsStringAsync();
            return responseString;
        }
        public static string CallWebClientFormData(string url, Dictionary<string, string> requestParameters, string userAgent = "", MemoryStream file = null)
        {

            var client = new WebClient();

            if (!string.IsNullOrEmpty(userAgent))
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            }
            client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded; charset=UTF-8");

            client.Headers.Add(HttpRequestHeader.Cookie, "PHPSESSID=thjdgkocllbikabqso38keajot");

            var lstContent = new List<string>();
            foreach (KeyValuePair<string, string> a in requestParameters)
            {
                lstContent.Add(string.Format("{0}={1}", a.Key, a.Value));
            }
            var content = string.Join("&", lstContent);
            var responseString = client.UploadString(url, "POST", content);
            return responseString;
        }
        public static string CallRestApi(string webApiLink, string method)
        {
            return CallRestApi(webApiLink, method, string.Empty, string.Empty);
        }
        public static string CallRestApi(string webApiLink, string method, string token)
        {
            return CallRestApi(webApiLink, method, token, string.Empty);
        }
        public static string CallRestApi(string webApiLink, string method, string token, bool isUseBearer = true)
        {
            return CallRestApi(webApiLink, method, token, string.Empty, isUseBearer);
        }
        private static List<string> _allowApiMethod = new List<string> { "POST", "GET", "PATCH", "PUT" };
        public static string CallRestApi(string webApiLink, string method, string token, string content, bool isUseBearer = true)
        {
            try
            {
                if (!_allowApiMethod.Contains(method)) return "Method không hợp lệ";

                byte[] bytes = Encoding.UTF8.GetBytes(content);
                var request = WebRequest.Create(webApiLink) as HttpWebRequest;
                if (request != null)
                {
                    //request.Credentials = mycache;
                    request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    request.Method = method;
                    request.ContentType = "application/json; charset=utf-8";
                    request.ContentLength = bytes.Length;
                    request.KeepAlive = true;
                    if (!string.IsNullOrEmpty(token))
                    {
                        request.Headers.Add("TokenCode", token);
                        request.Headers.Add("Authorization", (isUseBearer ? "Bearer " : string.Empty) + token);
                    }


                    if ((method == "POST" || method == "PUT") && bytes.Length > 0)
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
                        throw new Exception("Error fetching data.");
                    }
                }
                else
                {
                    throw new Exception("Request is Null");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> CallPostApiWithCert(string url, string clientCertificatePath, string clientCertificatePassword, string content,
            string token = "", bool isUseBearer = true)
        {
            // Tạo HttpClientHandler để cấu hình SSL
            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            handler.ClientCertificates.Add(new X509Certificate2(clientCertificatePath, clientCertificatePassword));
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // Tạo HttpClient với HttpClientHandler đã cấu hình
            var client = new HttpClient(handler);

            if (!string.IsNullOrEmpty(token))
            {
                //request.Headers.Add("TokenCode", token);
                //request.Headers.Add("Authorization", (isUseBearer ? "Bearer " : string.Empty) + token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Tạo JSON body
            var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                // Gửi yêu cầu POST đến API với JSON body
                var response = await client.PostAsync(url, jsonContent);
                response.EnsureSuccessStatusCode();

                // Đọc nội dung trả về từ API
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                Utility.Log.Debug(ex);
                return ex.Message;
            }
        }

        public static async Task<string> CallGetApiWithCert(string url, string clientCertificatePath, string clientCertificatePassword,
            string token = "", bool isUseBearer = true)
        {
            // Tạo HttpClientHandler để cấu hình SSL
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(new X509Certificate2(clientCertificatePath, clientCertificatePassword));
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // Tạo HttpClient với HttpClientHandler đã cấu hình
            var client = new HttpClient(handler);

            if (!string.IsNullOrEmpty(token))
            {
                //request.Headers.Add("TokenCode", token);
                //request.Headers.Add("Authorization", (isUseBearer ? "Bearer " : string.Empty) + token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                // Gửi yêu cầu POST đến API với JSON body
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return responseContent;
                }
                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string UploadFilesToRemoteUrl(string webApiLink, string fileName, byte[] byteData
            //string[] files
            , string token, bool isUseBearer = true
            , NameValueCollection nvc = null
            )
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webApiLink);
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            request.ContentType = "multipart/form-data; boundary=" +
                                    boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("TokenCode", token);
                request.Headers.Add("Authorization", (isUseBearer ? "Bearer " : string.Empty) + token);
            }

            Stream memStream = new System.IO.MemoryStream();

            var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                    boundary + "\r\n");
            var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                        boundary + "--");


            string formdataTemplate = "\r\n--" + boundary +
                                        "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    memStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitemBytes = Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitemBytes, 0, formitemBytes.Length);
                }
            }

            string headerTemplate =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                "Content-Type: application/octet-stream\r\n\r\n";

            //for (int i = 0; i < files.Length; i++)
            //{
            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            var header = string.Format(headerTemplate, "uplTheFile", fileName);
            var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);
            memStream.Write(byteData, 0, byteData.Length);

            //using (var fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
            //{
            //    var buffer = new byte[1024];
            //    var bytesRead = 0;
            //    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //    {
            //        memStream.Write(buffer, 0, bytesRead);
            //    }
            //}
            //}

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            request.ContentLength = memStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }

            using (var response = request.GetResponse())
            {
                Stream stream2 = response.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
            }
        }

        public static string HttpUploadFile(string url, string file, byte[] byteData, string paramName, string contentType
            , string token, bool isUseBearer = true
            , Dictionary<string, string> dictData = null)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("TokenCode", token);
                request.Headers.Add("Authorization", (isUseBearer ? "Bearer " : string.Empty) + token);
            }

            using (Stream rs = request.GetRequestStream())
            {
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                foreach (string key in dictData.Keys)
                {
                    rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                    string formitem = string.Format(formdataTemplate, key, dictData[key]);
                    byte[] formitemBytes = Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitemBytes, 0, formitemBytes.Length);
                }

                rs.Write(boundaryBytes, 0, boundaryBytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, paramName, file, contentType);
                byte[] headerBytes = Encoding.UTF8.GetBytes(header);
                rs.Write(headerBytes, 0, headerBytes.Length);

                rs.Write(byteData, 0, byteData.Length);

                byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }

            // Handle the response here (e.g., read the server's response).
            using (var response = request.GetResponse())
            {
                Stream stream2 = response.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                return reader2.ReadToEnd();
            }
        }
    }
}
