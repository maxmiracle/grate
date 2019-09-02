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

namespace EmergentImage
{
    public class EmergentImageContext : INotifyPropertyChanged
    {
        private SqImgLayer _imgLayer;
        private BitmapImage _bitmap;
        bool IsCanOpen { get; set; } = true;

        public void Open(object sender, ExecutedRoutedEventArgs e)
        {
            IsCanOpen = false;
            try
            {

                // Configure open file dialog box
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Image"; // Default file name
                dlg.DefaultExt = ".jpg"; // Default file extension
                dlg.Filter = "Images (.jpg)|*.jpg"; // Filter files by extension

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


        public void CanOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsCanOpen;
            e.Handled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
