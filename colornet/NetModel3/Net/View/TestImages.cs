using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBase.View
{
    public class TestImages
    {
        public static string[] TestImagePath = new string[] {
            "..\\..\\..\\..\\..\\tests\\pics\\test1.jpg" 
        };

        public static Bitmap GetTestImage(int index)
        {
            if ((index < 0) || (index >= TestImagePath.Length))
                throw new Exception(String.Format("Test image index {0} is not exists", index) );
            string absolutePath = Path.GetFullPath(TestImagePath[index]);
            Bitmap img = new Bitmap(absolutePath);
            return img;
        }
    }
}
