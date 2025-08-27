using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace VietBaIT.ChuKySo.Api.DigitalSignature.Viettel.BO
{
    public class SignHashResponceBO : ResponceBO
    {

        [JsonProperty("signatures")]
        public string[] signatures { get; set; }


    }
}
