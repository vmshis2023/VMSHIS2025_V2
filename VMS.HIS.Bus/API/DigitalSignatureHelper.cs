using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using SubSonic;


namespace VMS.API.Libs
{
    public class UsbTokenCert
    {
        public X509Certificate2 certClient { get; set; }
        public IList<X509Certificate> chainCertificate { get; set; }
    }
    public class VMSDigitalSignature
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userSecret { get; set; }
        public string userTOTP { get; set; }
        public string userFullName { get; set; }
        public string userDesc { get; set; }
        public string SAD { get; set; }
        public int FontSize { get; set; }
        public int FontSizeWhenImage { get; set; }
      
        public DateTime? ngay_ky { get; set; }
        public string signatureType { get; set; }
       
        public string pdfFileName { get; set; }
        public string base64Pdf { get; set; }
        public string base64Signature { get; set; }
       
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
