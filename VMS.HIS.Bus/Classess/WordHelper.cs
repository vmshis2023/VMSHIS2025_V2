using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Aspose.Words;
using System.IO;

namespace VMS.HIS.Bus.Classess
{
    public static class WordMerger
    {
        public static bool MergeWordFiles(List<string> inputFiles, string outputFile)
        {
            if (inputFiles == null || inputFiles.Count == 0)
                throw new ArgumentException("Danh sách file Word rỗng.");

            try
            {
                Aspose.Words.Document mainDoc = null;

                foreach (var file in inputFiles)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"⚠️ File không tồn tại: {file}");
                        continue;
                    }

                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var subDoc = new Aspose.Words.Document(stream);

                        if (mainDoc == null)
                        {
                            mainDoc = subDoc;
                        }
                        else
                        {
                            // ✅ Mỗi file bắt đầu ở TRANG MỚI
                            subDoc.FirstSection.PageSetup.SectionStart = SectionStart.NewPage;

                            // ✅ Giữ định dạng gốc, không thêm khoảng trắng
                            mainDoc.AppendDocument(subDoc, ImportFormatMode.KeepSourceFormatting);

                            //// ✅ Ghép liền mạch, không chèn PageBreak thủ công
                            //mainDoc.AppendDocument(subDoc, ImportFormatMode.KeepSourceFormatting);

                            //// ✅ Đảm bảo section nối tiếp nhau, không sinh trang mới
                            //mainDoc.LastSection.PageSetup.SectionStart = SectionStart.Continuous;
                        }
                    }
                }

                if (mainDoc == null)
                    throw new InvalidOperationException("Không có file Word hợp lệ để ghép.");

                mainDoc.Save(outputFile);
                Console.WriteLine($"✅ Đã ghép {inputFiles.Count} file Word thành công → {outputFile}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi nối Word: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Nối nhiều file Word thành 1 file duy nhất, mỗi file bắt đầu ở trang mới.
        /// </summary>
        public static bool MergeWordFiles_bak(List<string> inputFiles, string outputFile)
        {
            if (inputFiles == null || inputFiles.Count == 0)
                throw new ArgumentException("Danh sách file Word rỗng.");

            try
            {
                Aspose.Words.Document mainDoc = null;

                for (int i = 0; i < inputFiles.Count; i++)
                {
                    var file = inputFiles[i];

                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"⚠️ File không tồn tại: {file}");
                        continue;
                    }

                    using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        Document subDoc = new Document(stream);

                        if (mainDoc == null)
                        {
                            mainDoc = subDoc;
                        }
                        else
                        {
                            // Thêm ngắt trang trước khi nối
                            mainDoc.LastSection.Body.AppendChild(new Paragraph(mainDoc));
                            mainDoc.LastSection.Body.LastParagraph.AppendChild(new Run(mainDoc, ControlChar.PageBreak));

                            mainDoc.AppendDocument(subDoc, ImportFormatMode.KeepSourceFormatting);
                        }
                    }
                }

                if (mainDoc == null)
                    throw new InvalidOperationException("Không có file Word hợp lệ để ghép.");

                mainDoc.Save(outputFile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Lỗi khi nối Word: {ex.Message}");
                return false;
            }
        }
    }
   
}
