using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NUnit.Framework.Interfaces;

namespace Net.Test
{
    internal class TestImages
    {
        /// <summary>
        /// Относительный путь в проекте к тестовому изображению malevich10.
        /// Черный квадрат. Изображение 10x10 1 bit. Белый фон
        /// Композиция: черный квадрат Малевича.
        /// Типовая задача: разделить области на две.
        /// Научиться работать с битовыми изображениями.
        /// </summary>
        const string malevich10 = @".\TestData\G0.bmp";

        static readonly Dictionary<string, string> testImages = new Dictionary<string, string>
        {
            { nameof(malevich10), malevich10 }
        };

      
        public static Image getTestImage(string key)
        {
            
            return Image.FromFile(testImages[key]);
        }
    }
}