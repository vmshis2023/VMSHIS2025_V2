using com.itextpdf.text.pdf.security;
using DEMO_CLOUD_CA_DOTNET;
using DEMO_CLOUD_CA_DOTNET.BO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.X509;

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
using ViettelFileSigner;
using VMS.ChuKySo.Api.Helpers;

namespace VMS.ChuKySo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViettelController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private String userId;
        private String wsdlUrl;
        private String clientId;
        private String clientSecret;
        private String profileId;
        private string desc;
        private string app;
        private int id;
        private static int indexCurrent = 1;
        private int count;
        private String token = "";
        private Dictionary<String, CertBO> certMap = new Dictionary<string, CertBO>();
        private List<string> credentialIDList = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appIdentitySettingsAccessor"></param>
        public ViettelController(IOptions<AppSettings> appIdentitySettingsAccessor)
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
        private bool getCertList(string errMsg)
        {
           
            certMap.Clear();
            credentialIDList.Clear();
            String info = "Get Cert List. ";
            
            certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);

            if (certMap == null || certMap.Count == 0)
            {
                errMsg="Không tìm thấy CTS";
                return false;
            }
            //Default get the last certificate or customer select certificate
            CertBO certBO = null;
            String credentialID = null;

            Org.BouncyCastle.X509.X509Certificate[] certChain = null;
            Org.BouncyCastle.X509.X509Certificate x509Cert = null;

            foreach (KeyValuePair<string, CertBO> kvp in certMap)
            {
                Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
                credentialID = kvp.Key;
                certBO = kvp.Value;
                if (certBO != null && certBO.certificates != null && certBO.certificates.Length != 0)
                {
                    var certParser = new Org.BouncyCastle.X509.X509CertificateParser();
                    x509Cert = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[0]));

                    if (certBO.certificates.Length > 1)
                    {
                        Org.BouncyCastle.X509.X509Certificate certViettelCA = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[1]));
                        if (certViettelCA != null)
                        {
                            certChain = new X509Certificate[] { x509Cert, certViettelCA };
                        }
                    }
                    credentialIDList.Add(credentialID);
                }
            }
            return true;
        }
        private byte[] signWithoutValidate( string pathFile, string signedFile, VMSDigitalSignatureRect rect, string errMsg)
        {
            String info = "Ký file ";
            string idString = "123";
            if (idString.Trim().Length == 0)
            {
                errMsg = info + "ID tài liệu không hợp lệ";
                Utility.Log.Debug(errMsg);
                return null;
            }
            try
            {
                id = Int32.Parse(idString);
            }
            catch (Exception e)
            {
                errMsg = info + "ID tài liệu phải là số nguyên";
                Utility.Log.Debug(errMsg);
              
                return null;
            }
            info += indexCurrent++ + ".";
            try
            {
                if (certMap == null || certMap.Count == 0 || credentialIDList == null || credentialIDList.Count == 0 )
                {
                    errMsg = info + "Chưa chọn CTS";
                    Utility.Log.Debug(errMsg);
                  
                    return null;
                }

                string credentialID = credentialIDList[0];
                CertBO certBO = null;
                certMap.TryGetValue(credentialID, out certBO);

                certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);

                Org.BouncyCastle.X509.X509Certificate[] certChain = null;
                Org.BouncyCastle.X509.X509Certificate x509Cert = null;

                if (certBO.certificates != null && certBO.certificates.Length != 0)
                {
                    var certParser = new Org.BouncyCastle.X509.X509CertificateParser();
                    x509Cert = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[0]));

                    if (certBO.certificates.Length > 1)
                    {
                        Org.BouncyCastle.X509.X509Certificate certViettelCA = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[1]));
                        if (certViettelCA != null)
                        {
                            certChain = new X509Certificate[] { x509Cert, certViettelCA };
                        }
                    }
                }

                if (certChain == null || certChain.Length != 2)
                {
                    errMsg = info + "Lấy Chứng thư số không thành công. Không lấy được CTS CA.";
                    Utility.Log.Debug(errMsg);
                   
                    return null;
                }

                // Set parameters
                DateTime signDate = DateTime.Now;
                int duration = 60;

                // Create hash file

                SignPdfFile pdfSig = new SignPdfFile();
                //Khai bao duong dan toi file pdf can ky tren web server
                string base64Hash = HashFilePDF.GetHashTypeRectangleText(pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256);
                //string base64Hash = HashFilePDF.GetHashTypeRectangleText2_ExistedSignatureField(fileFullPath, certChain, "Ký", "1");
                byte[] hash = Convert.FromBase64String(base64Hash);
                // Sign hash
                String[] hashList = new String[1];
                hashList[0] = base64Hash;
                String dataDisplay = app + " - " + desc;
                String[] signatureList = MobileCA.signHashWithoutValidation(hashList, id, dataDisplay, credentialID, duration, wsdlUrl, token);
                if (signatureList == null || signatureList.Length == 0)
                {
                    errMsg = info + "Ký không thành công";
                    Utility.Log.Debug(errMsg);
                  
                    return null;
                }
                //            Utility.Log.Debug(info + "Ký hash thành công: " + signature);

                Utility.Log.Debug(info + "Ký hash thành công");
                var signature = signatureList[0];

                if (signature == null)
                {
                    errMsg = info + "Phát sinh lỗi trong quá trình thực hiện chữ ký số";
                    Utility.Log.Debug(errMsg);
                    return null;
                }


                TimestampConfig timestampConfig = new TimestampConfig();
                timestampConfig.UseTimestamp = false;
                //string signatureBase64 = Convert.ToBase64String(signature);
                if (!pdfSig.insertSignature(signature, signedFile, timestampConfig, HashFilePDF.HASH_ALGORITHM_SHA_256))
                {
                    errMsg = info + "Insert signature into file fail.";
                    Utility.Log.Debug(errMsg);
                    return null;
                }
                else
                {
                    Utility.Log.Debug(info + "Ký thành công");
                    return System.IO.File.ReadAllBytes(signedFile) ;
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex);
                Utility.Log.Debug(ex.ToString());
                return null;
            }
            return null;
        }
        private byte[] signFile(string  pathFile, string signedFile, VMSDigitalSignatureRect rect, string errMsg)
        {
            String info = "Ký file";
           
            string idString = "123";
            if (idString.Trim().Length == 0)
            {
                errMsg = info + "ID tài liệu không hợp lệ";
                Utility.Log.Debug(errMsg);
                return null;
            }
            try
            {
                id = Int32.Parse(idString);
            }
            catch (Exception e)
            {
                errMsg = info + "ID tài liệu phải là số nguyên";
                Utility.Log.Debug(errMsg);
               
                return null;
            }
            info += indexCurrent++ + ".";

           
            try
            {

                if (certMap == null || certMap.Count == 0 || credentialIDList == null || credentialIDList.Count == 0 )
                {
                    errMsg = info + "Chưa chọn CTS";
                    Utility.Log.Debug(errMsg);
                  
                    return null;
                }

                string credentialID = credentialIDList[0];
                CertBO certBO = null;
                certMap.TryGetValue(credentialID, out certBO);

                certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);

                Org.BouncyCastle.X509.X509Certificate[] certChain = null;
                Org.BouncyCastle.X509.X509Certificate x509Cert = null;

                if (certBO.certificates != null && certBO.certificates.Length != 0)
                {
                    var certParser = new Org.BouncyCastle.X509.X509CertificateParser();
                    x509Cert = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[0]));

                    if (certBO.certificates.Length > 1)
                    {
                        Org.BouncyCastle.X509.X509Certificate certViettelCA = certParser.ReadCertificate(Convert.FromBase64String(certBO.certificates[1]));
                        if (certViettelCA != null)
                        {
                            certChain = new X509Certificate[] { x509Cert, certViettelCA };
                        }
                    }
                }

                if (certChain == null || certChain.Length != 2)
                {
                    errMsg = info + "Lấy Chứng thư số không thành công. Không lấy được CTS CA.";
                    Utility.Log.Debug(errMsg);
                   
                    return null;
                }

                // Set parameters
                DateTime signDate = DateTime.Now;

                // Create hash file

                SignPdfFile pdfSig = new SignPdfFile();
                //Khai bao duong dan toi file pdf can ky tren web server
                string imageFile = "C:\\1.png";
                string base64Hash = HashFilePDF.GetHashTypeImgText(pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256, imageFile);
                //string base64Hash = HashFilePDF.GetHashTypeRectangleText(pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256);
                //string base64Hash = HashFilePDF.GetHashTypeRectangleText2_ExistedSignatureField(fileFullPath, certChain, "Ký", "1");
                byte[] hash = Convert.FromBase64String(base64Hash);
                // Sign hash use Prikey
                String[] hashList = new String[1];
                hashList[0] = base64Hash;
                String dataDisplay = app + " - " + desc;
                String[] signatureList = MobileCA.signHash(hashList, id, dataDisplay, credentialID, wsdlUrl, token);
                if (signatureList == null || signatureList.Length == 0)
                {
                    errMsg = info + "Ký không thành công";
                    Utility.Log.Debug(errMsg);

                 
                    return null;
                }
                //            Utility.Log.Debug(info + "Ký hash thành công: " + signature);
                Utility.Log.Debug(info + "Ký hash thành công");
                var signature = signatureList[0];

                if (signature == null)
                {
                    errMsg = info + "Phát sinh lỗi trong quá trình thực hiện chữ ký số";
                    Utility.Log.Debug(errMsg);
                  
                    return null;
                }


                TimestampConfig timestampConfig = new TimestampConfig();
                timestampConfig.UseTimestamp = false;
                //string signatureBase64 = Convert.ToBase64String(signature);
                if (!pdfSig.insertSignature(signature, signedFile, timestampConfig, HashFilePDF.HASH_ALGORITHM_SHA_256))
                {
                    errMsg = info + "Insert signature into file fail.";
                    Utility.Log.Debug(errMsg);
                 
                    return null;
                }
                else
                {
                    Utility.Log.Debug(info + "Ký thành công");
                    return System.IO.File.ReadAllBytes(signedFile); 
                }
            }
            catch (Exception ex)
            {
                Utility.Log.Error(ex);
                Utility.Log.Debug(ex.ToString());
                return null;
            }
            return null;
        }
        /// <summary>
        /// POST: DigitalSignaturePdfFileSign
        /// </summary>
        /// <returns></returns>
        [HttpPost("DigitalSignaturePdfFileSign")]
        public async Task<IActionResult> DigitalSignaturePdfFileSign([FromBody] VMSDigitalSignature objDigitalSignature)
        {
            string errMsg = "";
            Utility.Log = Utility.LogFactory.GetLogger(nameof(DigitalSignaturePdfFileSign));
            Utility.Log.Debug("----------------------------------------------------------------------");

            var response = new SingleResponse<string>();
            response.Success = false;
            //using (var client = HttpClientFactory.CreateHttpClient(apiUrl, objDigitalSignature.appId, objDigitalSignature.secret))
            //{
            try
            {
                var base64Pdf = objDigitalSignature.base64Pdf;
                var filePdfBytes = Convert.FromBase64String(base64Pdf);

                // Get signature image bytes
                var base64Signature = objDigitalSignature.base64Signature;
                if (!string.IsNullOrEmpty(_appSettings.DigitalSignatureSettings.SignatureImagePath))
                {
                    var signatureImagePath = _appSettings.DigitalSignatureSettings.SignatureImagePath;
                    signatureImagePath =
                        signatureImagePath.StartsWith(AppDomain.CurrentDomain.BaseDirectory) ?
                        signatureImagePath :
                        AppDomain.CurrentDomain.BaseDirectory + signatureImagePath;
                    if (System.IO.File.Exists(signatureImagePath))
                    {
                        Utility.Log.Debug($"SignatureImagePath: {signatureImagePath}");
                        base64Signature = Convert.ToBase64String(System.IO.File.ReadAllBytes(signatureImagePath));
                    }
                }
                var fileSignatureBytes = Convert.FromBase64String(base64Signature);
                Utility.Log.Debug("Begin iSignatureType '{5}' for pdf file '{0}' by user '{1}', userFullName '{2}' with appId: '{3}', secret: '{4}'",
                objDigitalSignature.pdfFileName, objDigitalSignature.userName, objDigitalSignature.userFullName,
                objDigitalSignature.appId, objDigitalSignature.secret, objDigitalSignature.signatureType);
                Utility.Log.Debug($"File Length: {filePdfBytes.Length}");
                Utility.Log.Debug($"Signature Length: {fileSignatureBytes.Length}");


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


                var wsdlUrl = _appSettings.ViettelSettings.Url;
                var clientId = _appSettings.ViettelSettings.ClientId;
                var clientSecret = _appSettings.ViettelSettings.ClientSecret;
                var profileId = _appSettings.ViettelSettings.ProfileId;
                userId = objDigitalSignature.userId;
                desc = "BVHP-Ký văn bản Cloud CA";
                app = "vOffice";
                //#GetC
              
                credentialIDList.Clear();
                var token = string.Empty;
                var tokenExpiresIn = -1;
                var certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);
                if (certMap == null || certMap.Count == 0)
                {
                    errMsg="Không tìm thấy CTS";
                    response.Success = false ;
                    response.ErrorMessage = errMsg;
                    return response.ToHttpResponse(); 
                }
                var baseDirectory = string.Format(@"{0}PDF", AppContext.BaseDirectory);
                if (!Directory.Exists(baseDirectory))
                {
                    Directory.CreateDirectory(baseDirectory);
                }
                //Lưu ra file PDF để truyền cho các hàm kí số của Viettel
                var filePath = string.Format(@"{0}\{1}.pdf", baseDirectory, Guid.NewGuid().ToString());
                var DesPath = string.Format(@"{0}\SignedPDF\{1}_signed.pdf", baseDirectory, Path.GetFileNameWithoutExtension(filePath));
                System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64Pdf));
                var fileImg = string.Format(@"{0}\{1}_.pdf", baseDirectory, Guid.NewGuid().ToString());
                if (base64Signature != null)
                    System.IO.File.WriteAllBytes(fileImg, Convert.FromBase64String(base64Signature));
                foreach (VMSDigitalSignatureLocation location in objDigitalSignature.locations)
                {
                    foreach (VMSDigitalSignatureRect rect in location.lstRect)
                    {
                        var randomString = Guid.NewGuid().ToString();
                        var llx = rect.StartX;
                        var lly = rect.StartY;
                        var urx = rect.EndX;
                        var ury = rect.EndY;
                        if (iSignatureType == 0)
                        {
                            //var filePath = string.Format(@"{0}\{1}.pdf", baseDirectory, randomString);
                            //System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64Pdf));
                            //var hasSignatureFieldPdf = string.Format(@"{0}\{1}_signaturefield.pdf", baseDirectory, randomString);
                            //base64Pdf = createPDFFileWithEmptySignatureField(filePath, hasSignatureFieldPdf,
                            //    location.pageSign, llx, lly, urx, ury, objDigitalSignature.signatureName);
                        }
                        else
                        {
                            byte[] byPdf = null;
                            if (Utility.sDbnull(objDigitalSignature.SAD) == "")//Kí lần đầu
                            {
                                byPdf = signFile(filePath, DesPath, rect,  errMsg);
                            }
                            else//Thực hiện kí lại, có kiểm tra xem nếu qua 25 ngày SAD hết hiệu lực thì 
                            {
                               byPdf = signWithoutValidate(filePath, DesPath, rect, errMsg);
                            }
                            var bytes = System.IO.File.ReadAllBytes(DesPath);
                            base64Pdf = Convert.ToBase64String(bytes);
                        }
                    }

                }


                if (!string.IsNullOrEmpty(base64Pdf))
                {
                    response.Data = base64Pdf;
                    response.Success = true;
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
    }

}
