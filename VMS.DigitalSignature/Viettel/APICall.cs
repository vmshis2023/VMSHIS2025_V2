using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DEMO_CLOUD_CA_DOTNET
{
    public static class APICall
    {

        public static async Task<T> PostAsync<T>(string url, object json, string token = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 120000; //ms
            if (token != null && token.Length != 0)
            {
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
            }


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonString = JsonConvert.SerializeObject(json);

                streamWriter.Write(jsonString);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var jsonResp = streamReader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<T>(jsonResp);
                return result;
            }
            return default(T);
        }

    }
}
