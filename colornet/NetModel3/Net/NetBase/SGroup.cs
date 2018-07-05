using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBase;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Net.NetBase
{
    public class SGroup
    {
        private IGrouping<SqRt, SqRt> gr;

        public SGroup(IGrouping<SqRt, SqRt> gr)
        {
            this.gr = gr;
        }


        public Bitmap getBitmask()
        {
            var el = gr.First<SqRt>();
            int width = el.Layer.Width;
            int height = el.Layer.Height;

            System.Drawing.Imaging.PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format1bppIndexed;
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
            IntPtr ptr = data.Scan0;
            byte[] myData = new byte[data.Stride * data.Height];

            

            foreach ( var v in gr)
            {

                int x = v.X;
                int y = v.Y;
                myData[y * data.Stride + x / 8] |= (byte)(1 << (7 - x%8)); 

                
            }
            Marshal.Copy(myData, 0, data.Scan0, data.Stride * data.Height);

            bitmap.UnlockBits(data);

            return bitmap;
        }

        public static bool Compare(Bitmap bm1, Bitmap bm2, out string message)
        {
            for (int i = 0; i < bm1.Width; i++)
                for (int j = 0; j < bm2.Width; j++)
                {
                    if ( bm1.GetPixel(i, j) != bm2.GetPixel(i, j))
                    {
                        message = String.Format("Pixel ({0}, {1}) is {2} <> {3}", i, j, bm1.GetPixel(i, j), bm2.GetPixel(i, j));
                        return false;
                    }
                }
            message = null;
            return true;
        }
    }
}
