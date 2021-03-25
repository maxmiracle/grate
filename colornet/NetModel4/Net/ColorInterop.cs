using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Net
{
    public class ColorInterop
    {
        public static Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            Color color;
            var bytesPerPixel = (bitmap.Format.BitsPerPixel + 7) / 8;
            var bytes = new byte[bytesPerPixel];
            var rect = new Int32Rect(x, y, 1, 1);

            bitmap.CopyPixels(rect, bytes, bytesPerPixel, 0);

            if (bitmap.Format == PixelFormats.Bgra32)
            {
                color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
            }
            else if (bitmap.Format == PixelFormats.Bgr32)
            {
                color = Color.FromRgb(bytes[2], bytes[1], bytes[0]);
            }
            // handle other required formats
            else
            {
                color = Colors.Black;
            }

            return color;
        }


        public static List<xyColor> GetXyColors(BitmapSource source)
        {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

            var width = source.PixelWidth;
            var height = source.PixelHeight;

            List<xyColor> list = new List<xyColor>();
            for (int x = 0; x<width; x++)
                for(int y = 0; y< height; y++)
                {
                    xyColor xyc = new xyColor();
                    Color color = GetPixelColor(source, x, y);
                    xyc.color = color;
                    xyc.x = x;
                    xyc.y = y;
                    list.Add(xyc);
                }
            return list;
        }
    }
}
