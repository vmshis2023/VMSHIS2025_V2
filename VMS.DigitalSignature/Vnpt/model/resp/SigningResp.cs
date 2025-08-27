using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_signning.model.resp
{
    public class SigningResp
    {
        public string dataSigned { get; set; }
        public Dictionary<string, string> responseItem { get; set; }
    }
}
