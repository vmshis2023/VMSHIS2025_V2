using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class ExtendTransactionRequestBO
    {
        [JsonProperty("credentialID")]
        public string credentialID { get; set; }

        [JsonProperty("SAD")]
        public string SAD { get; set; }

        [JsonProperty("numSignatures")]
        public int numSignatures { get; set; }

        [JsonProperty("documents")]
        public DocumentBO[] documents { get; set; }

        [JsonProperty("hash")]
        public String[] hash { get; set; }

        [JsonProperty("hashAlgo")]
        public string hashAlgo { get; set; }

        [JsonProperty("signAlgo")]
        public string signAlgo { get; set; }
    }
}
