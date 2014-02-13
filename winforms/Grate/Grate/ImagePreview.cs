using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grate
{
    public partial class ImagePreview : Form
    {
        public ImagePreview()
        {
            InitializeComponent();
        }

        public void Show(Bitmap bitmap)
        {
            imageView1.Image = bitmap;
            this.Show();
        }
    }
}
