using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class SignHashResponseBO : ResponseBO
    {

        [JsonProperty("signatures")]
        public string[] signatures { get; set; }


    }
}
