using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Grate
{
    public partial class ImageView : UserControl
    {
        private Image image = null;

        public ImageView()
        {
            InitializeComponent();
        }
        
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                if (image != null)
                {
                    this.Size = image.Size;
                }
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (image != null)
            {
                Graphics gr = e.Graphics;
                gr.DrawImage(image, new Point(0, 0));
            }
        }
    }
}
