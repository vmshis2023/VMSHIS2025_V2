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
        string imageFile = "C:\\1.png";
        public  String firstTimeSAD = "";
        private Dictionary<String, CertBO> certMap = new Dictionary<string, CertBO>();
        private List<string> credentialIDList = new List<string>();
        private string PDFFolder="";
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
        /// POST: KiemtraTaikhoanKyso
        /// </summary>
        /// <returns></returns>
        [HttpPost("KiemtraTaikhoanKyso")]
        public async Task<IActionResult> KiemtraTaikhoanKyso([FromBody] VMSDigitalSignature objDigitalSignature)
        {
            Utility.Log = Utility.LogFactory.GetLogger(nameof(KiemtraTaikhoanKyso));
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
            firstTimeSAD = MobileCA.firstTimeSAD;
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
        private byte[] signWithoutValidate( string pathFile, string signedFile, VMSDigitalSignatureRect rect, string errMsg, float X = 10, float Y = 10, float W = 250, float H = 80, int SizeFont = 10, int SizeFontImage = 6)
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
                if (certMap == null || certMap.Count == 0 || credentialIDList == null || credentialIDList.Count == 0)
                {
                    getCertList(errMsg);
                }
                if (certMap == null || certMap.Count == 0 || credentialIDList == null || credentialIDList.Count == 0)
                {

                    errMsg = info + "Chưa chọn CTS";
                    Utility.Log.Debug(errMsg);
                    return null;
                }
                string credentialID = credentialIDList[0];
                CertBO certBO = null;
                certMap.TryGetValue(credentialID, out certBO);
                //certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);

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
                string base64Hash = HashFilePDF.GetHashTypeImgText(X, Y, W, H, pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256, imageFile, SizeFont, SizeFontImage);
                //string base64Hash = HashFilePDF.GetHashTypeRectangleText(pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256);
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
        //10, 10, 200, 80
        private byte[] signFile(string  pathFile, string signedFile, VMSDigitalSignatureRect rect, string errMsg,float X=10,float Y=10,float W=250,float H=80, int SizeFont=10, int SizeFontImage=6)
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
                    getCertList(errMsg);
                }
                if (certMap == null || certMap.Count == 0 || credentialIDList == null || credentialIDList.Count == 0)
                {

                    errMsg = info + "Chưa chọn CTS";
                    Utility.Log.Debug(errMsg);
                    return null;
                }
                string credentialID = credentialIDList[0];
                CertBO certBO = null;
                certMap.TryGetValue(credentialID, out certBO);

                //certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);

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
               
                string base64Hash = HashFilePDF.GetHashTypeImgText(X,Y,W,H, pdfSig, pathFile, certChain, HashFilePDF.HASH_ALGORITHM_SHA_256, imageFile,SizeFont, SizeFontImage);
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
                var signedDir = Path.GetDirectoryName(signedFile);
                if (!Directory.Exists(signedDir))
                {
                    Directory.CreateDirectory(signedDir);
                }
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
        [HttpPost("KysoPDF")]
        public async Task<IActionResult> KysoPDF([FromBody] VMSDigitalSignature objDigitalSignature)
        {
            string errMsg = "";
            Utility.Log = Utility.LogFactory.GetLogger(nameof(KysoPDF));
            Utility.Log.Debug("---------Begin KysoPDF----------------------------------------------------");

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
               
                byte[] fileSignatureBytes = null;
                if (!string.IsNullOrEmpty(base64Signature))
                    fileSignatureBytes = Convert.FromBase64String(base64Signature);
                Utility.Log.Debug("Thông tin user kí số: user '{0}', userFullName '{1}' with userId: '{2}', password: '{3}'",
                objDigitalSignature.userName, objDigitalSignature.userFullName,
                objDigitalSignature.userId, objDigitalSignature.userSecret);
                Utility.Log.Debug($"File Length: {filePdfBytes.Length}");
                // Utility.Log.Debug($"Signature Length: {fileSignatureBytes.Length}");
                var baseDirectory = string.Format(@"{0}", AppContext.BaseDirectory);
                imageFile = string.Format(@"{0}\{1}_.png", PDFFolder, Guid.NewGuid().ToString());
                if (fileSignatureBytes != null)
                    System.IO.File.WriteAllBytes(imageFile, fileSignatureBytes);
                else
                    imageFile = "";
                //1: sign with text, 2: sign with image, 3: sign with text and image
                var iSignatureType = 3;
                switch (objDigitalSignature.signatureType.ToLower())
                {
                    case "text":
                        iSignatureType = 2;
                        imageFile = "";
                        break;
                    case "image":
                        iSignatureType = 1;
                        break;
                    case "empty":
                        iSignatureType = 0;
                        break;
                }

                PDFFolder = _appSettings.ViettelSettings.PdfFolder;
                 wsdlUrl = _appSettings.ViettelSettings.Url;
                 clientId = _appSettings.ViettelSettings.ClientId;
                 clientSecret = _appSettings.ViettelSettings.ClientSecret;
                 profileId = _appSettings.ViettelSettings.ProfileId;
                userId = objDigitalSignature.userId;
                desc = "BVHP-Ký văn bản Cloud CA";
                app = "vOffice";
                //#GetC
              
                credentialIDList.Clear();
                var token = string.Empty;
                var tokenExpiresIn = -1;
                // certMap = MobileCA.getAllCertificates(userId, wsdlUrl, clientId, clientSecret, profileId, ref token);
                //if (certMap == null || certMap.Count == 0)
                //{
                //    errMsg="Không tìm thấy CTS";
                //    response.Success = false ;
                //    response.ErrorMessage = errMsg;
                //    return response.ToHttpResponse(); 
                //}
               
                if (!Directory.Exists(baseDirectory))
                {
                    Directory.CreateDirectory(baseDirectory);
                }
                //Lưu ra file PDF để truyền cho các hàm kí số của Viettel
                var filePath = string.Format(@"{0}\{1}.pdf", PDFFolder, Guid.NewGuid().ToString());
                var DesPath = string.Format(@"{0}\SignedPDF\{1}_signed.pdf", PDFFolder, Path.GetFileNameWithoutExtension(filePath));
                System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64Pdf));
               
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
                                Utility.Log.Debug("----------------------Bắt đầu ký xác thực----------------------");
                                byPdf = signFile(filePath, DesPath, rect,  errMsg, llx, lly, urx-llx, ury-lly, objDigitalSignature.FontSize, objDigitalSignature.FontSizeWhenImage);
                                objDigitalSignature.SAD = firstTimeSAD;
                                Utility.Log.Debug("----------------------Kết thúc ký xác thực----------------------");
                            }
                            else//Thực hiện kí lại, có kiểm tra xem nếu qua 25 ngày SAD hết hiệu lực thì 
                            {
                                Utility.Log.Debug("----------------------Bắt đầu ký KHÔNG xác thực----------------------");
                                byPdf = signWithoutValidate(filePath, DesPath, rect, errMsg, llx, lly, urx - llx, ury - lly, objDigitalSignature.FontSize, objDigitalSignature.FontSizeWhenImage);
                                Utility.Log.Debug("----------------------Kết thúc ký KHÔNG xác thực----------------------");
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
                    response.SAD = firstTimeSAD;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                Utility.Log.Error("Lỗi khi thực hiện kí số {0}", nameof(KysoPDF), ex.ToString());
            }
            //}
            return response.ToHttpResponse();
        }
    }

}
