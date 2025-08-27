using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class LoginResponceBO : ResponceBO
    {

        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
    }
}
