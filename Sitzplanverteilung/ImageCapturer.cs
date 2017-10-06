using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;

namespace Sitzplanverteilung
{
    public static class ImageCapturer
    {
        public static void SaveToBMP(FrameworkElement visual, string fileName)
        {
            var bitmapEncoder = new BmpBitmapEncoder();
            SaveUsingEncoder(visual, fileName, bitmapEncoder);
        }

        public static void SaveToPNG(FrameworkElement visual, string fileName)
        {
            var pngBitmapEncoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, pngBitmapEncoder);
        }

        private static void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            Rect bounds = VisualTreeHelper.GetDescendantBounds(visual);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(visual);
                
                var rect = new Rect(0, 0, visual.RenderSize.Width, visual.RenderSize.Height);
                ctx.DrawRectangle(Brushes.White, null, rect);
                
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                encoder.Save(fileStream);
            }
        }
    }
}
