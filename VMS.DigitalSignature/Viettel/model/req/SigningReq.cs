using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_signning.model.req
{
    public class SigningReq
    {
        public string certAlias { get; set; }
        public string signingProfileId { get; set; }
        public string dataTobeSign { get; set; }
        public string keyAuth { get; set; }
        public AdditionAppearanceSetting additionAppearanceSetting { get; set; } 

        /**
             * DATA_TYPE: loại file ký số (XML, PDF,...)
             * SIGNATYRE_TYPE: loại chữ ký (DSig, XAdES, PAdES,...)
             * SIGNING_CERTIFICATE: CTS của người ký
             * SIGNING_TIME: thời gian thực hiện ký
        */
        private List<String> responseItems;
    }
}
