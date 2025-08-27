using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class CredentialsInfoResponseBO : ResponseBO
    {

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("key")]
        public KeyBO key { get; set; }

        [JsonProperty("cert")]
        public CertBO cert { get; set; }

        [JsonProperty("PIN")]
        public PINBO PIN { get; set; }

        [JsonProperty("OTP")]
        public OTPBO OTP { get; set; }

        [JsonProperty("authMode")]
        public String authMode { get; set; }

        [JsonProperty("SCAL")]
        public String SCAL { get; set; }

        [JsonProperty("multisign")]
        public int multisign { get; set; }

        [JsonProperty("lang")]
        public String lang { get; set; }

    }
}
