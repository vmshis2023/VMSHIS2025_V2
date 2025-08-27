using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class CredentialsListResponseBO : ResponseBO
    {

        [JsonProperty("credentialIDs")]
        public string[] credentialIDs { get; set; }

    }
}
