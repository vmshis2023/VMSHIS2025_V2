using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_signning.model.resp
{
    public class BaseResponse<T>
    {

        /**
             * 00: thành công
             * SIGN2ID-... : là các mã lỗi
        */
        public string code { get; set; }
        public string transactionId { get; set; } // mã giao dịch
        public T data { get; set; }
    }
}
