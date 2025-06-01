using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var converter = new SynchronizedConverter(new PdfTools());

            var htmlContent = File.ReadAllText("sample.html"); // Hoặc HTML string trực tiếp

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Out = "output.pdf"
            },
                Objects = {
                new ObjectSettings() {
                    HtmlContent = htmlContent
                }
            }
            };

            converter.Convert(doc);
            Console.WriteLine("Chuyển đổi thành công!");
        }
    }
}
