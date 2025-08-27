using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBaIT.ChuKySo.Api.Helpers;

namespace VietBaIT.ChuKySo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : Controller
    {
        private readonly AppSettings _appSettings;
        public PdfController(IOptions<AppSettings> appIdentitySettingsAccessor)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        [HttpGet("PdfGetFile")]
        public async Task<IActionResult> PdfGetFile(string filePath)
        {
            Utility.Log = Utility.LogFactory.GetLogger(nameof(PdfGetFile));
            Utility.Log.Debug("----------------------------------------------------------------------");
            var response = new Response();
            try
            {
                // giải mã path từ base 64 
                string fileName = Base64UrlEncoder.Decode(filePath);
                var fullFilePath = (_appSettings.DigitalSignatureSettings.PDFFolder.EndsWith("/")
                    ? _appSettings.DigitalSignatureSettings.PDFFolder
                    : _appSettings.DigitalSignatureSettings.PDFFolder + "/") + fileName;
                Utility.Log.Debug("fullFilePath: " + fullFilePath);
                var stream = new FileStream(fullFilePath, FileMode.Open);
                return new FileStreamResult(stream, "application /pdf");

            }
            catch (Exception ex)
            {
                Utility.Log.Error("There was an error on '{0}'",  ex);
                return null;
            } 
            
              
        }
        

}
}
