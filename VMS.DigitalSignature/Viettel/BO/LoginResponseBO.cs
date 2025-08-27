using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class LoginResponseBO : ResponseBO
    {

        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
    }
}
