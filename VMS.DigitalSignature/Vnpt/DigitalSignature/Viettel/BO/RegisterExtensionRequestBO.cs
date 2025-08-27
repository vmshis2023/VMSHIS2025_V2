using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class RegisterExtensionRequestBO
    {

        [JsonProperty("credential_id")]
        public string credential_id { get; set; }

        [JsonProperty("duration")]
        public int duration { get; set; }

        [JsonProperty("SAD")]
        public string SAD { get; set; }

    }
}
