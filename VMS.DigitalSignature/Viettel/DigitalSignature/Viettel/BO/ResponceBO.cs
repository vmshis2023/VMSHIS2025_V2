using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class ResponceBO
    {

        [JsonProperty("error")]
        public string error { get; set; }

        [JsonProperty("error_description")]
        public string error_description { get; set; }

    }
}
