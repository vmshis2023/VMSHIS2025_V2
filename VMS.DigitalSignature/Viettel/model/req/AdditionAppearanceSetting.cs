using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_signning.model.req
{
    public class AdditionAppearanceSetting
    {
        public string signPdfOverrideType { get; set; } // CUSTOM_FIELD: ký với dạng đẩy tọa độ từ phần mềm nghiệp lên
        public int? signPage { get; set; }
        public int? fontSize { get; set; }
        public string fontColor { get; set; } // truyề hex của màu
        public string handSignatureImage { get; set; } // base64 ảnh chữ ký của người ký nếu muốn hiển thị

        public PDFRectangle rectangle { get; set; }
    }
}
