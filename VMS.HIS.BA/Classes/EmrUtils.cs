using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VMS.HIS.EMR.Classes
{
    public class EmrUtils
    {

        // Đầu tiên khai báo các tag đặc biệt
        const string BeginTag = "‹"; // Unicode U+2039
        const string EndTag = "›";   // Unicode U+203A
        const string TagSeparator = "|"; // Phân cách
        public EmrUtils()
        {

        }
        public static void InsertTagField(RichEditControl richEdit, string label, string gid)
        {
            var doc = richEdit.Document;
            doc.BeginUpdate();

            // Tạo mã trường và gán field code
            string fieldCode = $"{label}{TagSeparator}gid={gid}";
            string visibleText = $"{BeginTag}{label}{EndTag}";

            // Chèn field code (code để sau này tìm kiếm)
            var range = doc.InsertText(doc.CaretPosition, fieldCode);
            var field = doc.Fields.Create(range);
            field.ShowCodes = false;

            // Ghi dữ liệu nhập thật trong field.ResultRange
            doc.InsertText(field.ResultRange.Start, visibleText);

            doc.CaretPosition = field.ResultRange.Start;
            doc.EndUpdate();
        }
       public static  void LoadTemplateAndReplace(RichEditControl richEdit, string filePath, Dictionary<string, string> values)
        {
            try
            {
                richEdit.LoadDocument(filePath, DocumentFormat.OpenXml);
                richEdit.ReadOnly = false; // Quan trọng: Cho phép gõ

                var doc = richEdit.Document;
                doc.BeginUpdate();

                foreach (var field in doc.Fields)
                {
                    string code = doc.GetText(field.CodeRange);
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        var parts = code.Split('|');
                        string fieldName = parts[0].Trim();

                        if (values.ContainsKey(fieldName))
                        {
                            string newValue = $"{BeginTag}{values[fieldName]}{EndTag}";
                            DocumentPosition insertPos = field.ResultRange.Start;

                            doc.Delete(field.ResultRange);
                            doc.InsertText(insertPos, newValue);
                        }
                    }
                }

                doc.EndUpdate();
            }
            catch (Exception ex)
            {
            }
           
        }
    }
}
