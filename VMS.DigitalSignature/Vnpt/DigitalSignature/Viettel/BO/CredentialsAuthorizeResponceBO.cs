using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class CredentialsAuthorizeResponceBO : ResponceBO
    {

        [JsonProperty("SAD")]
        public string SAD { get; set; }

    }
}
