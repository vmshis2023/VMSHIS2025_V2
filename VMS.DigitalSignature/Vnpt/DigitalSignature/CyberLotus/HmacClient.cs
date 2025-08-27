using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace VMS.ChuKySo.Api.DigitalSignature.CyberLotus
{
    public class HMACDelegatingHandler : DelegatingHandler
    {

        SigningAlgorithm algorithmName = SigningAlgorithm.HmacSHA256;
        private ApiCredential credential;
        public HMACDelegatingHandler(string apiId, string secret)
        {
            InnerHandler = new HttpClientHandler();
            credential = new ApiCredential();
            credential.API_ID = apiId;
            credential.Secret = secret;

        }
        public enum SigningAlgorithm
        {
            HmacSHA1,
            HmacSHA256
        };

        public const string RFC822DateFormat = "ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T";

        string getUnixTimstamp()
        {
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            return Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            string requestContentBase64String = string.Empty;
            //Get the Request URI
            string requestUri = request.RequestUri.AbsoluteUri.ToLower();
            //Get the Request HTTP Method type
            string requestHttpMethod = request.Method.Method;
            //Calculate UNIX time
            var requestTimeStamp = getUnixTimstamp();

            //Date now 
            var dateNow = DateTime.UtcNow.ToString(RFC822DateFormat);
            //Create the random nonce for each request
            string nonce = Guid.NewGuid().ToString("N");
            //Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
            if (request.Content != null)
            {
                // Hashing the request body, so any change in request body will result a different hash
                // we will achieve message integrity
                requestContentBase64String = await request.Content.ReadAsStringAsync();
            }
            //Creating the raw signature string by combinging
            //APPId, request Http Method, request Uri, request TimeStamp, nonce, request Content Base64 String
            //string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);
            var signatureRawData = new StringBuilder();
            signatureRawData.AppendFormat("{0}\n", requestHttpMethod);
            signatureRawData.AppendFormat("{0}\n", request.RequestUri.Scheme);
            signatureRawData.AppendFormat("{0}\n", request.RequestUri.Host + ":" + request.RequestUri.Port);
            signatureRawData.AppendFormat("{0}\n", request.RequestUri.LocalPath);
            if (request.Content != null)
            {
                signatureRawData.AppendFormat("{0}\n", request.Content.Headers.ContentType);
            }
            signatureRawData.AppendFormat("{0}\n", credential.API_ID);
            signatureRawData.AppendFormat("{0}\n", nonce);
            signatureRawData.AppendFormat("{0}\n", dateNow);
            signatureRawData.AppendFormat("{0}\n", requestContentBase64String);
            Console.Out.WriteLine(signatureRawData.ToString());
            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData.ToString());

            //Converting the APIKey into byte array
            var secretKeyByteArray = Convert.FromBase64String(credential.Secret);
            //Generate the hmac signature and set it in the Authorization header

            KeyedHashAlgorithm algorithm = new HMACSHA256();
            if (null == algorithm)
                throw new InvalidOperationException("Please specify a KeyedHashAlgorithm to use.");

            try
            {
                algorithm.Key = secretKeyByteArray;
                byte[] signatureBytes = algorithm.ComputeHash(signature);
                string digest = Convert.ToBase64String(signatureBytes);
                //Setting the values in the Authorization header using custom scheme (hmacauth)
                request.Headers.Authorization = new AuthenticationHeaderValue(algorithmName.ToString(), string.Format("{0}:{1}:{2}:{3}", credential.API_ID, nonce, digest, requestTimeStamp));
                Console.Out.WriteLine(digest.ToString());
                Console.Out.WriteLine(request.Headers.Authorization.ToString());
                request.Headers.Add("Date", dateNow);
            }
            finally
            {
                algorithm.Clear();
            }

            response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}

