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
using VietBaIT.ChuKySo.Api.Vnpt.Properties;
using VietBaIT.ChuKySo.Api.DigitalSignature.CyberLotus;
using VietBaIT.ChuKySo.Api.DigitalSignature.VietBa;
using VietBaIT.ChuKySo.Api.Helpers;
using VnptHashSignatures.Common;
using VnptHashSignatures.Interface;
using VnptHashSignatures.Pdf;

namespace VietBaIT.ChuKySo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnptController : ControllerBase
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
        public VnptController(IOptions<AppSettings> appIdentitySettingsAccessor)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        /// <summary>
        /// POST: DigitalSignatureCheckAccount
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignatureCheckAccount")]
        public async Task<IActionResult> DigitalSignatureCheckAccount([FromBody] VietBaDigitalSignature objDigitalSignature)
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
        /// POST: DigitalSignatureXMLSign
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignatureXMLSign")]
        public async Task<IActionResult> DigitalSignatureXMLSign([FromBody] VietBaDigitalSignature objDigitalSignature)
        {
            Utility.Log = Utility.LogFactory.GetLogger(nameof(DigitalSignatureXMLSign));
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
                var base64XML = objDigitalSignature.base64Pdf;
                var XMLBytes = Convert.FromBase64String(base64XML);

                Utility.Log.Debug("Begin XMLSign by user '{0}', userFullName '{1}' with appId: '{3}', secret: '{4}'",
                 objDigitalSignature.userName, objDigitalSignature.userFullName,
                objDigitalSignature.appId, objDigitalSignature.secret, objDigitalSignature.signatureType);

                string CertSerial = objDigitalSignature.appId;// "540110beffa622f3ca84bd2f93f0122c";//"5401100015b7ed04b187b438917c4590"; // Serial cua Chung thu so
                string Pin = objDigitalSignature.secret;// "12345678";//"0493645647"; // ma pin cua HSM | mat khau cua CTS

                ////Lấy chứng thư số để định hình ai ký
                var userCert = _getAccountCert(string.Format("{0}/v1/credentials/get_certificate", apiUrl));
                if (userCert == null)
                {
                    Utility.Log.Debug("not found cert");
                    response.Success = false;
                    response.Message = "not found cert";
                    return response.ToHttpResponse();
                }
                Utility.Log.Debug("cert found");
                String certBase64 = userCert.user_certificates[0].cert_data.Replace("\r\n", "");
                //SignHash Begin            
                IHashSigner signer = HashSignerFactory.GenerateSigner(XMLBytes, certBase64, null, HashSignerFactory.XML);
                signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);
                var hashValue = signer.GetSecondHashAsBase64();
                Utility.Log.Debug(string.Format("GetSecondHashAsBase64() done with hasValue={0}", hashValue));

                var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();
                Utility.Log.Debug(string.Format("data_to_be_sign={0}", data_to_be_sign));

                DataSign dataSign = _sign(string.Format("{0}/v2/signatures/sign", apiUrl), data_to_be_sign, userCert.user_certificates[0].serial_number);
                DataConfirm dataConfirm = _confirm(string.Format("{0}/v2/signatures/confirm", apiUrl), dataSign.sad, dataSign.transaction_id);
                var datasigned = dataConfirm.signatures[0].signature_value;
                Utility.Log.Debug(string.Format("datasigned={0}", datasigned));
                if (string.IsNullOrEmpty(datasigned))
                {
                    response.Success = false;
                    response.Message = "Sign error";
                    return response.ToHttpResponse();
                }
                //if (!signer.CheckHashSignature(datasigned))
                //{
                //    //_log.Error("Signature not match");
                //    response.Success = false;
                //    response.Message = "Signature not match";
                //    return response.ToHttpResponse();
                //}
                // ------------------------------------------------------------------------------------------

                // 3. Package external signature to signed file
                byte[] signed = signer.Sign(datasigned);
                XMLBytes = signed.Clone() as byte[];

                response.Data = Convert.ToBase64String(XMLBytes);
                response.Success = true;

                Utility.Log.Debug("Kí XML Thành công");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                Utility.Log.Error("There was an error on '{0}' invocation: {1}", nameof(DigitalSignaturePdfFileSign), ex);
            }
            //}
            return response.ToHttpResponse();
        }

        /// <summary>
        /// POST: DigitalSignaturePdfFileSign
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignaturePdfFileSign")]
        public async Task<IActionResult> DigitalSignaturePdfFileSign([FromBody] VietBaDigitalSignature objDigitalSignature)
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


                var base64Pdf = objDigitalSignature.base64Pdf;
                var filePdfBytes = Convert.FromBase64String(base64Pdf);
                var fileSignatureBytes = Convert.FromBase64String(objDigitalSignature.base64Signature);
                if (!string.IsNullOrEmpty(_appSettings.DigitalSignatureSettings.SignatureImagePath) && System.IO.File.Exists(_appSettings.DigitalSignatureSettings.SignatureImagePath))
                {
                    fileSignatureBytes = System.IO.File.ReadAllBytes(_appSettings.DigitalSignatureSettings.SignatureImagePath);
                }
                Utility.Log.Debug("Begin iSignatureType '{5}' for pdf file '{0}' by user '{1}', userFullName '{2}' with appId: '{3}', secret: '{4}'",
                objDigitalSignature.pdfFileName, objDigitalSignature.userName, objDigitalSignature.userFullName,
                objDigitalSignature.appId, objDigitalSignature.secret, objDigitalSignature.signatureType);
                Utility.Log.Debug($"File Length: {filePdfBytes.Length}");
                Utility.Log.Debug($"Signature Length: {fileSignatureBytes.Length}");

                string CertSerial = objDigitalSignature.appId;// "540110beffa622f3ca84bd2f93f0122c";//"5401100015b7ed04b187b438917c4590"; // Serial cua Chung thu so
                string Pin = objDigitalSignature.secret;// "12345678";//"0493645647"; // ma pin cua HSM | mat khau cua CTS

                var usingBase64Signature = Convert.ToBase64String(fileSignatureBytes);
                SigningAPI signingAPI = new SigningAPI(apiUrl, Pin, Pin);

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

                ////Lấy chứng thư số để định hình ai ký
                var userCert = _getAccountCert(string.Format("{0}/v1/credentials/get_certificate", apiUrl));
                if (userCert == null)
                {
                    Utility.Log.Debug("not found cert");
                    response.Success = false;
                    response.Message = "not found cert";
                    return response.ToHttpResponse();
                }
                Utility.Log.Debug("cert found");
                String certBase64 = userCert.user_certificates[0].cert_data.Replace("\r\n", "");
                //SignHash Begin            
                IHashSigner signer = HashSignerFactory.GenerateSigner(filePdfBytes, certBase64, null, HashSignerFactory.PDF);
                signer.SetHashAlgorithm(MessageDigestAlgorithm.SHA256);



                byte[] signImg = null;// signingAPI.GetSignatureImage(CertSerial, Pin,null, null, null, null, null, false);
                foreach (VietBaDigitalSignatureLocation location in objDigitalSignature.locations)
                {
                    foreach (VietBaDigitalSignatureRect rect in location.lstRect)
                    {
                        var signatureInfo = new SignatureInfo();
                        signatureInfo.visibleX = rect.StartX;
                        signatureInfo.visibleY = rect.StartY;
                        signatureInfo.visibleWidth = rect.EndX - rect.StartX;
                        signatureInfo.visibleHeight = rect.EndY - rect.StartY;
                        signatureInfo.pageNum = location.pageSign;
                        //signatureInfo.signatureImageData = Convert.ToBase64String(signImg);

                        #region Optional -----------------------------------
                        // Property: Lý do ký số
                        ((PdfHashSigner)signer).SetReason("Xác nhận tài liệu");
                        // Hình ảnh hiển thị trên chữ ký (mặc định là logo VNPT)
                        //var imgBytes = File.ReadAllBytes(@"C:\Users\Hung Vu\Desktop\aaaa.jpg");
                        //var x = Convert.ToBase64String(imgBytes);
                        //((PdfHashSigner)signer).SetCustomImage(signImg);
                        // Signing page (@deprecated)
                        //((PdfHashSigner)signer).SetSigningPage(signatureInfo.pageNum);
                        // Kiểu hiển thị chữ ký (OPTIONAL/DEFAULT=TEXT_WITH_BACKGROUND)
                        ((PdfHashSigner)signer).SetRenderingMode(PdfHashSigner.RenderMode.TEXT_ONLY);
                        // Nội dung text trên chữ ký (OPTIONAL)
                        ((PdfHashSigner)signer).SetLayer2Text(string.Format("Ngày ký: {0} \n Người ký: {1} \n Nơi ký: {2}", objDigitalSignature.dateSigned.Value.ToString("dd/MM/yyyy hh:ss:tt"), objDigitalSignature.userFullName, "Khoa Xét Nghiệm"));
                        // Fontsize cho text trên chữ ký (OPTIONAL/DEFAULT = 10)
                        ((PdfHashSigner)signer).SetFontSize(10);
                        //((PdfHashSigner)signer).SetLayer2Text("yahooooooooooooooooooooooooooo");
                        // Màu text trên chữ ký (OPTIONAL/DEFAULT=000000)
                        ((PdfHashSigner)signer).SetFontColor("0000ff");
                        // Kiểu chữ trên chữ ký
                        ((PdfHashSigner)signer).SetFontStyle(PdfHashSigner.FontStyle.Normal);
                        // Font chữ trên chữ ký
                        ((PdfHashSigner)signer).SetFontName(PdfHashSigner.FontName.Arial);

                        //Hiển thị chữ ký và vị trí bên dưới dòng _textFilter cách 1 đoạn _marginTop, độ rộng _width, độ cao _height
                        //using (var reader = new PdfReader(unsignData))
                        //{

                        //    var parser = new PdfReaderContentParser(reader);

                        //    for (int pageNum = 1; pageNum <= reader.NumberOfPages; ++pageNum)
                        //    {
                        //        var strategy = parser.ProcessContent(pageNum, new LocationTextExtractionStrategyWithPosition());

                        //        var res = strategy.GetLocations();

                        //        var post = new TextLocation();

                        //        foreach (TextLocation textContent in res)
                        //        {
                        //            if (textContent.Text.Contains(_textFilter))
                        //            {
                        //                ((PdfHashSigner)signer).AddSignatureView(new PdfSignatureView
                        //                {
                        //                    Rectangle = string.Format("{0},{1},{2},{3}", (int)textContent.X, (object)(int)(textContent.Y - _marginTop - _height), (int)(textContent.X + _width), (int)(textContent.Y - _marginTop)),
                        //                    Page = pageNum
                        //                });
                        //            }
                        //        }
                        //    }



                        //    reader.Close();
                        //    //var searchResult = res.Where(p => p.Text.Contains(searchText)).OrderBy(p => p.Y).Reverse().ToList();
                        //}            

                        // Hiển thị ảnh chữ ký tại nhiều vị trí trên tài liệu
                        ((PdfHashSigner)signer).AddSignatureView(new PdfSignatureView
                        {
                            Rectangle = string.Format("{0},{1},{2},{3}", signatureInfo.visibleX, signatureInfo.visibleY, signatureInfo.visibleX + signatureInfo.visibleWidth, signatureInfo.visibleY + signatureInfo.visibleHeight),
                            //Rectangle = "56,677,250,855",// "56,677,180,755", //"56,677,180,755", //56,564,180,642

                            Page = location.pageSign
                        });

                        //((PdfHashSigner)signer).AddSignatureView(new PdfSignatureView
                        //{
                        //    Rectangle = "283,677,404,755", //"283,677,404,755", //283,564,404,642
                        //    Page = 2
                        //});

                        //((PdfHashSigner)signer).AddSignatureComment(new PdfSignatureComment
                        //{
                        //    Type = (int)PdfSignatureComment.Types.TEXT,
                        //    Text = "This is comment",
                        //    Page = 1,
                        //    Rectangle = "20,20,220,50",
                        //});

                        //// Thêm comment vào dữ liệu

                        //((PdfHashSigner)signer).AddSignatureComment(new PdfSignatureComment
                        //{
                        //    Page = 1,
                        //    Rectangle = "348,19,601,95",
                        //    Background = "",
                        //    Type = (int)PdfSignatureComment.Types.IMAGE,

                        //});
                        #endregion -----------------------------------------            
                        Utility.Log.Debug(string.Format("Sign with Reactangle: {0},{1},{2},{3}", signatureInfo.visibleX, signatureInfo.visibleY, signatureInfo.visibleX + signatureInfo.visibleWidth, signatureInfo.visibleY + signatureInfo.visibleHeight));
                        var hashValue = signer.GetSecondHashAsBase64();
                        Utility.Log.Debug(string.Format("GetSecondHashAsBase64() done with hasValue={0}", hashValue));

                        var data_to_be_sign = BitConverter.ToString(Convert.FromBase64String(hashValue)).Replace("-", "").ToLower();
                        Utility.Log.Debug(string.Format("data_to_be_sign={0}", data_to_be_sign));
                        if (iSignatureType == 0)
                        {
                            var randomString = RandomStringSignature(8, false);
                            var baseDirectory = string.Format(@"{0}PDF", AppContext.BaseDirectory);
                            if (!Directory.Exists(baseDirectory))
                            {
                                Directory.CreateDirectory(baseDirectory);
                            }
                            var filePath = string.Format(@"{0}\{1}.pdf", baseDirectory, randomString);
                            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64Pdf));
                            var hasSignatureFieldPdf = string.Format(@"{0}\{1}_signaturefield.pdf", baseDirectory, randomString);
                            base64Pdf = createPDFFileWithEmptySignatureField(filePath, hasSignatureFieldPdf,
                                location.pageSign, rect.StartX, rect.StartY, rect.EndX, rect.EndY, objDigitalSignature.signatureName);
                        }
                        else
                        {
                            DataSign dataSign = _sign(string.Format("{0}/v2/signatures/sign", apiUrl), data_to_be_sign, userCert.user_certificates[0].serial_number);
                            DataConfirm dataConfirm = _confirm(string.Format("{0}/v2/signatures/confirm", apiUrl), dataSign.sad, dataSign.transaction_id);
                            var datasigned = dataConfirm.signatures[0].signature_value;
                            Utility.Log.Debug(string.Format("datasigned={0}", datasigned));
                            if (string.IsNullOrEmpty(datasigned))
                            {
                                response.Success = false;
                                response.Message = "Sign error";
                                return response.ToHttpResponse();
                            }
                            //if (!signer.CheckHashSignature(datasigned))
                            //{
                            //    //_log.Error("Signature not match");
                            //    response.Success = false;
                            //    response.Message = "Signature not match";
                            //    return response.ToHttpResponse();
                            //}
                            // ------------------------------------------------------------------------------------------

                            // 3. Package external signature to signed file
                            byte[] signed = signer.Sign(datasigned);
                            filePdfBytes = signed.Clone() as byte[];

                            response.Data = Convert.ToBase64String(filePdfBytes);
                            response.Success = true;

                            Utility.Log.Debug("Kí pdf Thành công");

                        }
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

        #region VNPT SignCert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static GetCertData _getAccountCert(String uri)
        {
            var response = Query(new ReqGetCert
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = uid,
                serial_number = "",
                transaction_id = "321"
            }, uri);
            if (response != null)
            {
                ResGetCert res = JsonConvert.DeserializeObject<ResGetCert>(response);
                //String certBase64 = req.data;
                return res.data;
            }
            return null;

        }
        private static String Query(object req, string serviceUri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback
                += new RemoteCertificateValidationCallback(SslHelper.ValidateRemoteCertificate);

            RestClient client = new RestClient(serviceUri);
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(req);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = null;
            try
            {
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                //_log.Error($"Connect gateway error: {ex.Message}", ex);
                return null;
            }

            if (response == null || response.ErrorException != null)
            {
                //_log.Error("Service return null response");
                return null;
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //_log.Error($"Status code={response.StatusCode}. Status content: {response.Content}");
                return null;
            }

            return response.Content;
        }

        private static DataSign _sign(String uri, string data_to_be_signed, String serialNumber)
        {

            var secret = user_secret;
            var secretByte = System.Convert.FromBase64String(secret);
            var OTP = new Totp(secretByte);


            var sign_files = new List<SignFile>();
            var sign_file = new SignFile();
            sign_file.data_to_be_signed = data_to_be_signed;
            sign_file.doc_id = "test";
            sign_file.file_type = "pdf";
            sign_file.sign_type = "hash";
            sign_files.Add(sign_file);
            var response = Query(new ReqSign
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = uid,
                password = password,
                otp = OTP.ComputeTotp(),
                transaction_id = Guid.NewGuid().ToString(),
                sign_files = sign_files,
                serial_number = serialNumber,

            }, uri);
            if (response != null)
            {
                ResSign req = JsonConvert.DeserializeObject<ResSign>(response);
                return req.data;
            }
            return null;
        }


        private static DataConfirm _confirm(String uri, String sad, String transID)
        {

            var response = Query(new ReqConfirm
            {
                sp_id = client_id,
                sp_password = client_secret,
                user_id = uid,
                password = password,
                sad = sad,
                transaction_id = transID
            }, uri);
            if (response != null)
            {
                ResConfirm res = JsonConvert.DeserializeObject<ResConfirm>(response);
                return res.data;
            }
            return null;
        }
        #endregion
        private string createPDFFileWithEmptySignatureField(string rootPdf, string hasSignatureFieldPdf,
               int page,
               int llx,
               int lly,
               int urx,
               int ury,
               string signatureFieldName)
        {
            PdfReader reader = new PdfReader(rootPdf);

            FileStream outa = new FileStream(hasSignatureFieldPdf, FileMode.Append, FileAccess.Write);

            PdfStamper stamp = new PdfStamper(reader, outa, '\0', true);

            PdfFormField field = PdfFormField.CreateSignature(stamp.Writer);
            field.SetWidget(new iTextSharp.text.Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_OUTLINE);
            field.FieldName = signatureFieldName;

            // add the field here, the second param is the page you want it on         
            stamp.AddAnnotation(field, page);

            //stamp.FormFlattening = true; // lock fields and prevent further edits.

            stamp.Close();

            //iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);

            //var document = new Document(size);
            //var fs = new FileStream(hasSignatureFieldPdf, FileMode.Append);

            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //document.Open();
            //PdfFormField field = PdfFormField.CreateSignature(writer);
            //field.SetWidget(new iTextSharp.text.Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_OUTLINE);
            //field.FieldName = signatureFieldName;
            //writer.AddAnnotation(field);

            //PdfContentByte cb = writer.DirectContent;

            ////Create the new page
            //PdfImportedPage importedPage = writer.GetImportedPage(reader, page);
            //cb.AddTemplate(importedPage, 0, 0);


            //document.Close();
            //writer.Close();
            //reader.Close();
            var bytes = System.IO.File.ReadAllBytes(hasSignatureFieldPdf);
            var base64Str = Convert.ToBase64String(bytes);
            return base64Str;
        }

        private async Task<X509Certificate> getCertificate(string appId, string secret)
        {
            string result = string.Empty;
            X509Certificate cert = null;
            try
            {
                string apiUrl = _appSettings.DigitalSignatureSettings.ApiDomain1 + "/api/account/endcert";
                using (var client = HttpClientFactory.CreateHttpClient(apiUrl, appId, secret))
                {
                    var response_x = await client.GetAsync(apiUrl);
                    result = await response_x.Content.ReadAsStringAsync();
                    var certByte = Convert.FromBase64String(result);
                    cert = new X509CertificateParser().ReadCertificate(certByte);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(result)) throw new Exception("Error result response: " + result);

                throw new Exception("Error: /api/account/endcert" + ex);
            }
            return cert;
        }

        private void addExternalSignature(byte[] extSignature, PdfSignatureAppearance sap)
        {
            try
            {
                byte[] paddedSig = new byte[8192];
                extSignature.CopyTo(paddedSig, 0);

                PdfDictionary dic2 = new PdfDictionary();
                dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
                sap.Close(dic2);

                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private string RandomStringSignature(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private byte[] hashPdfFileWithCert(
               iTextSharp.text.Font font,
               byte[] pfdContent,
               Org.BouncyCastle.X509.X509Certificate endCert,
               out MemoryStream baos,
               out PdfSignatureAppearance sap,
               int page,
               int llx,
               int lly,
               int urx,
               int ury,
               int typeSig,
               string base64Image,
               string userFullName,
               string signatureName)
        {
            baos = null;
            sap = null;
            PdfReader reader = null;
            PdfStamper stp = null;

            reader = new PdfReader(pfdContent);

            AcroFields acroFields = reader.AcroFields;
            acroFields.RemoveField(signatureName);

            baos = new MemoryStream();

            stp = PdfStamper.CreateSignature(reader, baos, '\0', null, true);
            sap = stp.SignatureAppearance;
            iTextSharp.text.Rectangle pageRect = new iTextSharp.text.Rectangle((float)llx, (float)lly, (float)urx, (float)ury);

            sap.SetVisibleSignature(pageRect, page, signatureName);
            sap.Certificate = endCert;

            string noidung = "Signature Valid" + "\n";
            noidung += "Ký bởi: " + userFullName + "\n";
            noidung += "Thời gian ký: " + DateTime.Now.ToString("dd/MM/yyyy");
            iTextSharp.text.BaseColor colorSign = new iTextSharp.text.BaseColor(255, 0, 0);
            switch (typeSig)
            {
                case 1:
                    {
                        if (!string.IsNullOrEmpty(base64Image))
                        {
                            iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(base64Image));
                            sap.Image = instance;
                        }

                        sap.Acro6Layers = true;
                        sap.Layer2Text = "";
                        break;
                    }
                case 2:
                    {

                        if (font == null)
                        {
                            var bytes = Resources.times;
                            BaseFont bf = BaseFont.CreateFont("times.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true, bytes, null);
                            font = new iTextSharp.text.Font(bf, _appSettings.DigitalSignatureSettings.FontSize, iTextSharp.text.Font.NORMAL, colorSign);
                        }
                        sap.Layer2Font = font;

                        sap.Layer2Text = noidung;
                        sap.Layer2Text.PadLeft(100);
                        break;
                    }
                case 3:
                    {
                        if (font == null)
                        {
                            var bytes = Resources.times;
                            BaseFont bf = BaseFont.CreateFont("times.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true, bytes, null);
                            font = new iTextSharp.text.Font(bf, _appSettings.DigitalSignatureSettings.FontSize, iTextSharp.text.Font.NORMAL, colorSign);
                        }
                        sap.Layer2Font = font;

                        if (!string.IsNullOrEmpty(base64Image))
                        {
                            iTextSharp.text.Image instance2 = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(base64Image));
                            instance2.ScalePercent(50);
                            instance2.SetAbsolutePosition(100f, 150f);
                            sap.Image = instance2;
                            sap.Image.Alignment = 0;
                            sap.ImageScale = 0.3f;
                            //sap.SignatureGraphic = instance2;
                        }

                        /* DESCRIPTION = 0,
                        NAME_AND_DESCRIPTION = 1,
                        GRAPHIC_AND_DESCRIPTION = 2,
                        GRAPHIC = 3
                        */
                        //sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
                        //sap.Image.ScaleAbsoluteHeight(height);
                        new iTextSharp.text.Rectangle((float)llx, (float)lly, (float)ury, (float)ury);
                        sap.Acro6Layers = true;
                        sap.Layer2Text = noidung;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            // sap.Acro6Layers = false;

            PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
            dic.Reason = sap.Reason;
            dic.Location = sap.Location;
            dic.Contact = sap.Contact;
            dic.Date = new PdfDate(sap.SignDate);
            sap.CryptoDictionary = dic;

            Dictionary<PdfName, int> exc = new Dictionary<PdfName, int>();
            exc.Add(PdfName.CONTENTS, (int)(8192 * 2 + 2));
            sap.PreClose(exc);
            Stream data = sap.GetRangeStream();
            byte[] hash = DigestAlgorithms.Digest(data, "SHA1");

            return hash;
        }


    }

}
