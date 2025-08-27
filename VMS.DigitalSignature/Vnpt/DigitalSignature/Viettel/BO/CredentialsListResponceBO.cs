using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class CredentialsListResponceBO : ResponceBO
    {

        [JsonProperty("credentialIDs")]
        public string[] credentialIDs { get; set; }

    }
}
