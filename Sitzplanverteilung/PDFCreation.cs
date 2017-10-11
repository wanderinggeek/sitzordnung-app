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
            // string tmpPath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]).Replace("\\bin\\Debug", "\\tmp\\");
            string tmpPath = System.IO.Path.GetTempPath();
            string picName = "Sitzplan-Block{}.png";

            FileStream fs = new FileStream(fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff"), FileMode.Create);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.AddTitle("Sitzplan");

            doc.Open();

            for (int i = 1; i < 7; i++)
            {
                doc.Add(new Paragraph("Block " + Convert.ToString(i)));

                string tmpPicName = picName.Replace("{}", Convert.ToString(i));

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(tmpPath + tmpPicName);
                img.ScaleToFit(PageSize.A4.Rotate());
                img.SetAbsolutePosition(10, 0);
                doc.Add(img);

                doc.NewPage();
            }

            doc.Close();
            writer.Close();
            fs.Close();
        }
    }
}
