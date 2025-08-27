using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class CertBO
    {

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("certificates")]
        public string[] certificates { get; set; }

        [JsonProperty("issuerDN")]
        public string issuerDN { get; set; }

        [JsonProperty("serialNumber")]
        public string serialNumber { get; set; }


        [JsonProperty("subjectDN")]
        public string subjectDN { get; set; }

        [JsonProperty("validFrom")]
        public string validFrom { get; set; }

        [JsonProperty("validTo")]
        public string validTo { get; set; }

    }
}
