using Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EmergentImage.Annotations;
using System.Collections;
using Net.KMean;

namespace EmergentImage
{
    public class EmergentImageContext : INotifyPropertyChanged
    {
        private SqImgLayer _imgLayer;
        private BitmapImage _bitmap;
        private List<ColorStatistic> _colorStatistic;

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        bool IsCanOpen { get; set; } = true;

        /// <summary>
        /// Get list of CommandBindings
        /// </summary>
        /// <returns>Collection of CommandBindings</returns>
        public ICollection CommandBindings()
        {
            return new List<CommandBinding>() { 
                        new CommandBinding(ApplicationCommands.Open, Open, CanOpen),
                        new CommandBinding(EmergentCommands.AnalyseColors, AnalyseColors, CanAnalyseColors) };
        }

        /// <summary>
        /// Command Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Open(object sender, ExecutedRoutedEventArgs e)
        {
            IsCanOpen = false;
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Image"; // Default file name
                dlg.DefaultExt = ".jpg"; // Default file extension
                dlg.Filter = "Images (.jpg)|*.jpg;*.png"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;

                    Bitmap = new BitmapImage(new Uri(filename));

                    //ImgLayer = new SqImgLayer(bitmap);
                }
            }
            finally
            {
                IsCanOpen = true;
            }
        }

        public void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsCanOpen;
            e.Handled = true;
        }

        /// <summary>
        /// Command AnalyseColors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void AnalyseColors(object sender, ExecutedRoutedEventArgs args)
        {
            logger.Info("AnalyseColors started");
            if ( Bitmap == null)
            {
                logger.Warn("Image is empty");
                return;
            }
            AnalyseColors(Bitmap);
        }

        private void AnalyseColors(BitmapImage bitmap)
        {
            ColorStatistic = ColorInterop.AnalyseColors(bitmap);
        }

        public void CanAnalyseColors(object sender, CanExecuteRoutedEventArgs eventArgs)
        {
            eventArgs.CanExecute = Bitmap != null;
            eventArgs.Handled = true;
        }

        public List<ColorStatistic> ColorStatistic 
        {
            get => _colorStatistic;
            set
            {
                _colorStatistic = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage Bitmap
        {
            get => _bitmap;
            set
            {
                if (Equals(value, _bitmap)) return;
                _bitmap = value;
                OnPropertyChanged();
            }
        }

        public SqImgLayer ImgLayer
        {
            get => _imgLayer;
            set
            {
                if (Equals(value, _imgLayer)) return;
                _imgLayer = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
