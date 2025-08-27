using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class RegisterExtensionResponseBO : ResponseBO
    {
        [JsonProperty("user_id")]
        public string user_id { get; set; }

        [JsonProperty("app_id")]
        public string app_id { get; set; }

        [JsonProperty("expire_date")]
        public string expire_date { get; set; }
    }
}
