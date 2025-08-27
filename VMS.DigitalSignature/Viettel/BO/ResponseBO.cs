using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class ResponseBO
    {

        [JsonProperty("error")]
        public string error { get; set; }

        [JsonProperty("error_description")]
        public string error_description { get; set; }

    }
}
