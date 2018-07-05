
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Net.View
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImage : Window
    {
        private System.Drawing.Bitmap bitmap;

        public ShowImage()
        {
            InitializeComponent();
        }

        public ShowImage(System.Drawing.Bitmap bitmap) : this()
        {

            this.bitmap = bitmap;
            MemoryStream mem = new MemoryStream();
            bitmap.Save(mem, ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = mem;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            img.Source = bitmapImage;
            
        }
    }
}
