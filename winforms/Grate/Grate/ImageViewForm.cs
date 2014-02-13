using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grate.Reactor;

namespace Grate
{
    public partial class ImageViewForm : Form
    {
        public ImageViewForm()
        {
            InitializeComponent();
        }

        private void loadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Image image = Image.FromFile(openFileDialog.FileName);
                    imageView.Image = image;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void graphiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphiteTool tool = new GraphiteTool();
            tool.RunOn(imageView.Image);
        }

        private void reactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReactorTool tool = new ReactorTool();
            tool.RunOn(imageView.Image);
        }

        private void reactor2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reactor2.ReactorTool tool = new Reactor2.ReactorTool();
            tool.RunOn(imageView.Image);
        }

        private void reactor22ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reactor2.ReactorTool2 tool = new Reactor2.ReactorTool2();
            tool.RunOn(imageView.Image);
        }

        private void reactor3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reactor3.ReactorTool tool = new Reactor3.ReactorTool();
            tool.RunOn(imageView.Image);
        }

        private void reactor4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reactor4.ReactorTool tool = new Reactor4.ReactorTool();
            tool.RunOn(imageView.Image);
        }
    }
}
