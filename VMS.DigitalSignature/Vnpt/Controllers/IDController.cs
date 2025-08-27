using com.itextpdf.text.pdf.security;
using EasySign.Core.Domain.LibPdf;
using EasySign.Core.New.Demo.SigningAPI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.X509;
using OtpNet;
using RestSharp;
using SmartCATHWithServiceHash;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using VMS.ChuKySo.Api.DigitalSignature.CyberLotus;
using VMS.ChuKySo.Api.DigitalSignature.VMS;
using VMS.ChuKySo.Api.Helpers;
using VnptHashSignatures.Common;
using VnptHashSignatures.Interface;
using VnptHashSignatures.Pdf;
using demo_signning.model.resp;
using demo_signning.model.req;

namespace VMS.ChuKySo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IDController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private static string client_id = "4184-637127995547330633.apps.signserviceapi.com";
        private static string client_secret = "NGNhMzdmOGE-OGM2Mi00MTg0";

        private static string uid = "871097";//""871097";//"162952530_003";//"112418"; 
        private static string password = "123456a@A";//"123456a@A"; 
        private static string user_secret = "QTQ4RTAxN0JGMTE3MzcyMEIwNDlEREVCNTJBMDA2NjU=";//"QTQ4RTAxN0JGMTE3MzcyMEIwNDlEREVCNTJBMDA2NjU=";//"RTUwODlCMTk5NTg4OEM2Qzk4NzQzQjYwRDU0MjMxN0Y="; //"RjVDRUY1Q0U4QzlDNUY1Q0U5N0EyMjdGNDk2RkJCMTI=";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appIdentitySettingsAccessor"></param>
        public IDController(IOptions<AppSettings> appIdentitySettingsAccessor)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        /// <summary>
        /// POST: DigitalSignatureCheckAccount
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignatureCheckAccount")]
        public async Task<IActionResult> DigitalSignatureCheckAccount([FromBody] VMSDigitalSignature objDigitalSignature)
        {
            Utility.Log = Utility.LogFactory.GetLogger(nameof(DigitalSignatureCheckAccount));
            Utility.Log.Debug("----------------------------------------------------------------------");
            var apiUrl = _appSettings.DigitalSignatureSettings.ApiDomain1;
            Utility.Log.Debug("api: " + apiUrl);
            string result = string.Empty;
            var response = new Response();
            response.Success = true;
            return response.ToHttpResponse();
        }
        
        /// <summary>
        /// POST: DigitalSignaturePdfFileSign
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignaturePdfFileSign")]
        public async Task<IActionResult> DigitalSignaturePdfFileSign([FromBody] VMSDigitalSignature objDigitalSignature)
        {

            Utility.Log = Utility.LogFactory.GetLogger(nameof(DigitalSignaturePdfFileSign));
            Utility.Log.Debug("----------------------------------------------------------------------");
            var apiUrl = _appSettings.DigitalSignatureSettings.ApiDomain1;
            client_id = _appSettings.DigitalSignatureSettings.client_id;
            client_secret = _appSettings.DigitalSignatureSettings.client_secret;
            Utility.Log.Debug("api: " + apiUrl);
            var response = new SingleResponse<string>();
            response.Success = false;
            //using (var client = HttpClientFactory.CreateHttpClient(apiUrl, objDigitalSignature.appId, objDigitalSignature.secret))
            //{
            try
            {
                var dataTobeSign = objDigitalSignature.base64Pdf;

                string certAlias = "BvSonTay";// objDigitalSignature.appId;// "540110beffa622f3ca84bd2f93f0122c";//"5401100015b7ed04b187b438917c4590"; // Serial cua Chung thu so
                string Pin = objDigitalSignature.secret;// "12345678";//"0493645647"; // ma pin cua HSM | mat khau cua CTS


                SigningReq req = new SigningReq();
                req.dataTobeSign = dataTobeSign;
                req.certAlias = certAlias;// "BvSonTay";
                req.signingProfileId = "2sign:signing:profile:088";
                req.keyAuth = "1511c4ff-12da-465f-974a-4b806a1f3212";

                // info
                AdditionAppearanceSetting additionAppearanceSetting = new AdditionAppearanceSetting();

                additionAppearanceSetting.signPdfOverrideType = "CUSTOM_FIELD"; // luôn dùng với dạng truyền tọa độ lên
                additionAppearanceSetting.handSignatureImage = "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"; // base64 ảnh ck tay của bác sĩ

                //1: sign with text, 2: sign with image, 3: sign with text and image
                var iSignatureType = 3;
                switch (objDigitalSignature.signatureType.ToLower())
                {
                    case "text":
                        iSignatureType = 2;
                        break;
                    case "image":
                        iSignatureType = 1;
                        break;
                    case "empty":
                        iSignatureType = 0;
                        break;
                }


                byte[] signImg = null;// signingAPI.GetSignatureImage(CertSerial, Pin,null, null, null, null, null, false);
                foreach (VMSDigitalSignatureLocation location in objDigitalSignature.locations)
                {
                    foreach (VMSDigitalSignatureRect rect in location.lstRect)
                    {
                        //var signatureInfo = new SignatureInfo();
                        //signatureInfo.visibleX = rect.StartX;
                        //signatureInfo.visibleY = rect.StartY;
                        //signatureInfo.visibleWidth = rect.EndX - rect.StartX;
                        //signatureInfo.visibleHeight = rect.EndY - rect.StartY;
                        //signatureInfo.pageNum = location.pageSign;

                        additionAppearanceSetting.signPage = location.pageSign;
                        PDFRectangle rectangle = new PDFRectangle();
                        rectangle.llx = rect.StartX;
                        rectangle.urx = rect.EndX;
                        rectangle.lly = rect.StartY;
                        rectangle.ury = rect.EndY;
                        additionAppearanceSetting.rectangle = rectangle;
                        req.additionAppearanceSetting = additionAppearanceSetting;

                        Task<BaseResponse<SigningResp>> res = SignPDF(req);
                        if (res == null)
                        {
                            Utility.Log.Debug("Sign PDF Failed");
                            response.Success = false;
                            response.Message = "Sign error";
                            return response.ToHttpResponse();
                        }
                        BaseResponse<SigningResp> result = res.Result;
                        byte[] signedData = Convert.FromBase64String(result.data?.dataSigned);
                        response.Data = Convert.ToBase64String(signedData);
                        response.Success = true;

                        Utility.Log.Debug("Kí pdf Thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                Utility.Log.Error("There was an error on '{0}' invocation: {1}", nameof(DigitalSignaturePdfFileSign), ex);
            }
            //}
            return response.ToHttpResponse();
        }
        static async Task<BaseResponse<SigningResp>> SignPDF(SigningReq req)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://uat-api-sign.2id.vn/2sign/v1/external/sign");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("ApiKey", "eyJ4NXQjUzI1NiI6Ik5XUXdPVFJrTWpBNU9XRmpObVUyTnpCbE5UTTNaRFV3T0RVellqWXdabUpsWlROa1pEQTRPRFU0WlRVd1pHSXdObVV5TW1abVpUTmhaRGt5TmpRMlpBPT0iLCJraWQiOiJnYXRld2F5X2NlcnRpZmljYXRlX2FsaWFzIiwidHlwIjoiSldUIiwiYWxnIjoiUlMyNTYifQ==.eyJzdWIiOiJhZG1pbkBjYXJib24uc3VwZXIiLCJhcHBsaWNhdGlvbiI6eyJpZCI6NDYsInV1aWQiOiI2NjdmYzk0Yy1kNzVmLTQ5YzEtODRjMi0zZTlhYjhlN2U0YzcifSwiaXNzIjoiaHR0cHM6XC9cL3VhdC1hcGltLjJpZC52bjo0NDNcL29hdXRoMlwvdG9rZW4iLCJrZXl0eXBlIjoiU0FOREJPWCIsInBlcm1pdHRlZFJlZmVyZXIiOiIiLCJ0b2tlbl90eXBlIjoiYXBpS2V5IiwicGVybWl0dGVkSVAiOiIiLCJpYXQiOjE3NTQ2MTg4MjcsImp0aSI6IjMxOGZjZTJhLTM0OTktNDUzMS04OGQzLWU0ODNhYmY2MTZiYSJ9.S6rNTv9jq7TMeMV8LWZIoVTzQC3pCn4s1SOP3AVolAsqyv1u6GCsUPzvmVwXjx6snsKxxQqI3OwhATNBTWwQas2z3B3tvjT2NjlOMJT_sFgJ_7sisesPEA1CN2l7K-L4UKJtW5GWCyHJ8eRGxeU33Kgcx1_EMR84ALrnFHGZglSB2ktCjDOaN3zlRlBLCrm5UV2oreryTmmw1MJR7IQT38KRiHBZpdaaR_HKIa9dYseY0hs02VUgMSFTJsQvqLRAQpx_ge-tNTsfAwFd9-Zqz8ANQiNMU16V7qQ5MYr-4kA_VLmV3_Qt-_OnGhqc7xp8ECNhHowGnlLtOARB7vVXXA==");
                string contentReq = JsonConvert.SerializeObject(req);
                var content = new StringContent(contentReq, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                BaseResponse<SigningResp> resp = JsonConvert.DeserializeObject<BaseResponse<SigningResp>>(responseBody);
                return resp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
      
    }

}
