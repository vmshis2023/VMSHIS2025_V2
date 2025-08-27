using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api.DigitalSignature.CyberLotus
{
    public class HttpClientFactory
    {
        //static Dictionary<String, HttpClient> clients = new Dictionary<string, HttpClient>();

        public static HttpClient CreateHttpClient(string url,string apiId, string apiSecret)
        {
            HttpClient client;

            //if (!clients.TryGetValue("apiId", out client))
            {
                client = new HttpClient(new HMACDelegatingHandler(apiId, apiSecret));
                var uri = new Uri(url);
                client.BaseAddress = uri;
                //clients.Add(apiId, client);
                return client;
            }
            return client;

        }
    }
}
