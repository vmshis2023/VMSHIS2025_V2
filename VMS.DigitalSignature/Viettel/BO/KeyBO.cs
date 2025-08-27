using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class KeyBO
    {

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("algo")]
        public string[] algo { get; set; }

        [JsonProperty("len")]
        public int len { get; set; }

    }
}
