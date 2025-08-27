using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class PINBO
    {

        [JsonProperty("presence")]
        public string presence { get; set; }

        [JsonProperty("format")]
        public string format { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }
    }
}
