using System;
using System.Drawing;
using System.IO;

namespace Net.Test
{
    internal class TestImages
    {
        
        const string v1 = @"C:\grate\colornet\NetModel4\Net\TestData\G0.bmp";
        static readonly string[] testImages = { v1 };

        internal static Image getTestImage(int v)
        {
            
            return Image.FromFile(testImages[v]);
        }
    }
}