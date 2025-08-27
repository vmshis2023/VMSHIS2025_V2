using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class CredentialsAuthorizeRequestBO
    {

        [JsonProperty("credentialID")]
        public string credentialID { get; set; }

        [JsonProperty("numSignatures")]
        public int numSignatures { get; set; }

        [JsonProperty("documents")]
        public DocumentBO[] documents { get; set; }

        [JsonProperty("hash")]
        public String[] hash { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

    }
}
