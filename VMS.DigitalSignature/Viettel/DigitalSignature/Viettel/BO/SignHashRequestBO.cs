using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class SignHashRequestBO
    {

        [JsonProperty("credentialID")]
        public string credentialID { get; set; }

        [JsonProperty("SAD")]
        public string SAD { get; set; }

        [JsonProperty("documents")]
        public DocumentBO[] documents { get; set; }

        [JsonProperty("hash")]
        public string[] hash { get; set; }

        [JsonProperty("hashAlgo")]
        public string hashAlgo { get; set; }

        [JsonProperty("signAlgo")]
        public string signAlgo { get; set; }

    }
}
