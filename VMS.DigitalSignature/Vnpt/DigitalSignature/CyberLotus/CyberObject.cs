using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api.DigitalSignature.CyberLotus
{
    public class PdfSignData
    {
        public string username { get; set; }
        public string password { get; set; }
        public int typesign { get; set; }
        public string base64pdf { get; set; }
        public string base64cert { get; set; }
        public string base64hash { get; set; }
        public string base64signature { get; set; }
        public string base64pdfSigned { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public string obj { get; set; }
        public string error { get; set; }
    }    
}
