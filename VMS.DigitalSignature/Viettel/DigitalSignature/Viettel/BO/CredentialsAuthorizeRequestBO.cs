using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class CredentialsAuthorizeRequestBO
    {

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("credentialID")]
        public string credentialID { get; set; }

        [JsonProperty("numSignatures")]
        public int numSignatures { get; set; }

        [JsonProperty("documents")]
        public DocumentBO[] documents { get; set; }

        [JsonProperty("hash")]
        public String[] hash { get; set; }


    }
}
