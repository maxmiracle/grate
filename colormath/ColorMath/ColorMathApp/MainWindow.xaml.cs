using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InternalViewerLib;
using Microsoft.Win32;
using System.IO;
using ColorMathLib.Reactor4;

namespace ColorMathApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private object context = null;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    currentImage.Source = bitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reactor4Click(object sender, RoutedEventArgs e)
        {
            ModelContext ctx = new ModelContext();
            BitmapImage bitmapImage = currentImage.Source as BitmapImage;
            ctx.Image = bitmapImage;
            ctx.Tool = new Grate.Reactor4.ReactorTool();
            ctx.Tool.OnShowBitmap += new Grate.Reactor4.RLayer.ShowBitmapDelegate(tool_OnShowBitmap);
            ctx.Run();
            context = ctx;
        }

        void tool_OnShowBitmap(BitmapSource bitmap, string title)
        {
            ShowImage showImage = new ShowImage(title, bitmap);
            showImage.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            InternalViewerWindow window = new InternalViewerWindow();
            window.AddObject("context", context);
            window.Show();
        }

        private void MenuItem_Test1(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            string currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); 
            string fileName = System.IO.Path.Combine(currentDirectory, @"..\..\..\..\..\tests\pics\test1.jpg");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            currentImage.Source = bitmapImage;
            ModelContext ctx = new ModelContext();
            ctx.Image = bitmapImage;
            ctx.Tool = new Grate.Reactor4.ReactorTool();
            ctx.Tool.OnShowBitmap += new Grate.Reactor4.RLayer.ShowBitmapDelegate(tool_OnShowBitmap);
            ctx.Run();
            context = ctx;
        }
    }
}
