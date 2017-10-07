using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows;

namespace Sitzplanverteilung
{
    public static class PDFCreation
    {
        public static void MakePDF(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.AddTitle("Sitzplan");

            doc.Open();

            doc.Add(new Paragraph("Block 1"));

            string imgFileName = System.IO.Path.GetDirectoryName(
                Environment.GetCommandLineArgs()[0]).Replace("\\bin\\Debug", "\\tmp\\" + "Sitzplan.png"
            );

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgFileName);
            img.ScaleToFit(PageSize.A4.Rotate());
            img.SetAbsolutePosition(10, 0);
            doc.Add(img);

            // doc.NewPage();
            // doc.Add(new Paragraph("Block 2"));

            doc.Close();
            writer.Close();
            fs.Close();
        }
    }
}
