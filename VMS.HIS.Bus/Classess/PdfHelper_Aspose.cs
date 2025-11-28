using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf;
namespace VMS.HIS.Bus.Classess
{
    public static class PdfMerger
    {
        /// <summary>
        /// Nối nhiều file PDF lại thành 1 file PDF duy nhất (hỗ trợ file rất lớn)
        /// </summary>
        public static bool MergePdfFiles(List<string> inputFiles, string outputFile)
        {
            if (inputFiles == null || inputFiles.Count == 0)
                throw new ArgumentException("Danh sách file PDF rỗng.");

            Aspose.Pdf.Document outputDocument = null;

            try
            {
                outputDocument = new Aspose.Pdf.Document();

                foreach (var file in inputFiles)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"⚠️ File không tồn tại: {file}");
                        continue;
                    }

                    using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        try
                        {
                            using (var pdfDoc = new Aspose.Pdf.Document(fs))
                            {
                                outputDocument.Pages.Add(pdfDoc.Pages);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Lỗi khi đọc file {file}: {ex.Message}");
                        }
                    }
                }

                // Dùng PdfSaveOptions cho đúng version
                var saveOptions = new PdfSaveOptions
                {
                    // Có thể bật chế độ tối ưu hoá bộ nhớ ở đây (nếu bản hỗ trợ)
                    // Chú ý: 20.7 chưa có FullCompression, chỉ có OptimizeResources()
                };

               
                // Tối ưu hóa bộ nhớ cho file lớn
                outputDocument.OptimizeResources(new Document.OptimizationOptions
                {
                    LinkDuplcateStreams = true,
                    RemoveUnusedObjects = true,
                    RemoveUnusedStreams = true,
                    CompressImages = true,
                    ImageQuality = 70
                });
                outputDocument.Save(outputFile, saveOptions);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Lỗi khi nối PDF: {ex.Message}");
                return false;
            }
            finally
            {
                outputDocument?.Dispose();
            }
        }
    }
}
