using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class LoginRequestBO
    {

        [JsonProperty("client_id")]
        public string client_id { get; set; }

        [JsonProperty("user_id")]
        public string user_id { get; set; }

        [JsonProperty("client_secret")]
        public string client_secret { get; set; }

        [JsonProperty("profile_id")]
        public string profile_id { get; set; }

    }
}
