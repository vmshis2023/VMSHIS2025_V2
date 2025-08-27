using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api
{
    public class VMSDigitalSignature
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userFullName { get; set; }
        public string userDesc { get; set; }
        public string SAD { get; set; }
        public string thoigianky_gannhat { get; set; }
        public string appId { get; set; }
        public string secret { get; set; }
        public string serialNum { get; set; }
        public string signatureType { get; set; }
        public string signatureName { get; set; }
        public string pdfFileName { get; set; }
        public string base64Pdf { get; set; }
        public string base64Signature { get; set; }
        public DateTime? dateSigned { get; set; }
        
        public List<VMSDigitalSignatureLocation> locations { get; set; }
    }
    public class VMSDigitalSignatureLocation
    {
        public string SignName { get; set; }
        public int pageSign { get; set; }
        public List<VMSDigitalSignatureRect> lstRect { get; set; }
    }
    public class VMSDigitalSignatureRect
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
    }
}
