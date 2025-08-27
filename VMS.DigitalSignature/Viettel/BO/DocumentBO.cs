using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DEMO_CLOUD_CA_DOTNET.BO
{
    public class DocumentBO
    {

        public DocumentBO(int document_id, string document_name)
        {
            this.document_id = document_id;
            this.document_name = document_name;
        }

        [JsonProperty("document_id")]
        public int document_id { get; set; }

        [JsonProperty("document_name")]
        public string document_name { get; set; }

    }
}
