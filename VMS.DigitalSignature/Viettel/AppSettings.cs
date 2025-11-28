using DEMO_CLOUD_CA_DOTNET.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api
{
    public class AppSettings
    {
        public DigitalSignatureSettings DigitalSignatureSettings { get; set; }
        public ICASignCloudSettings ICASignCloudSettings { get; set; }
        public ViettelSettings ViettelSettings { get; set; }
        public VinCASettings VinCASettings { get; set; }
    }

    public class VinCASettings
    {
        public string Region { get; set; }
    }

    public class ICASignCloudSettings
    {
        public string Url { get; set; }
        public string RelyingParty { get; set; }
        public string RelyingPartyUser { get; set; }
        public string RelyingPartyPassword { get; set; }
        public string RelyingPartySignature { get; set; }
        public string RelyingPartyKeyStore { get; set; }
        public string RelyingPartyKeyStorePassword { get; set; }
    }
    public class DigitalSignatureSettings
    {
        public string profileId { get; set; }
        public string ApiDomain1 { get; set; }
        public string ApiDomain2 { get; set; }
        public string PDFFolder { get; set; }
        public float FontSize { get; set; }
        public float Top { get; set; }
        public float Left { get; set; }
        public string PartnerName { get; set; }
        public string SignatureImagePath { get; set; }
        public string CertificateFilePath { get; set; }
        public string CertificatePassword { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
    public class ViettelSettings
    {
        public string PdfFolder { get; set; }
        public string Url { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ProfileId { get; set; }
        public string userId { get; set; }
        public string AccessToken { get; set; }
        public int AccessTokenExpiresIn { get; set; }
        public CertBO[] CertBOs { get; set; }
    }
}
