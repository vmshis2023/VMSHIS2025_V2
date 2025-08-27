using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class OTPBO
    {

        [JsonProperty("presence")]
        public string presence { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("format")]
        public string format { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }
    }
}
