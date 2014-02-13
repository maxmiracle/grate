using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Grate.ColorDispersion;

namespace GrateTest
{
    public partial class ColorGroupTreeTest : Form
    {
        public ColorGroupTreeTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            colorDialog1.ShowDialog();
            AddColor(colorDialog1.Color);
        }

        private void AddColor(Color col)
        {
            TreeNode node = new TreeNode("X " + String.Format( "{0}", col ));
            node.BackColor = col;
            treeView2.Nodes.Add(node);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<ColorWeight> list = new List<ColorWeight>();
            foreach ( TreeNode node in treeView2.Nodes )
            {
                list.Add ( new ColorWeight ( node.BackColor, 1 ));
            }
            ColorGroup cg = ColorGroup.CreateColorGroup(list.ToArray());
            ShowGroup(treeView1.Nodes, cg);
        }

        private void ShowGroup(TreeNodeCollection nodes, ColorGroup group)
        {
            TreeNode nodeg = new TreeNode("Lev: " + group.Level.ToString() + ", W: " + group.Weight.ToString() + ", Col " + String.Format("{0}", group.Color));
            nodeg.BackColor = group.Color;
            if (ColorWeight.Delta(ColorWeight.BlackNull, group) < 120)
            {
                nodeg.ForeColor = Color.White;
            }
            else
            {
                nodeg.ForeColor = Color.Black;
            }
            nodes.Add(nodeg);
            foreach ( ColorWeight cw in group.Nodes)
            {
                if (cw is ColorGroup)
                {
                    ColorGroup cg = cw as ColorGroup;
                    ShowGroup(nodeg.Nodes, cg);
                }
                else
                {
                    TreeNode node = new TreeNode("X " + String.Format("{0}", cw.Color));
                    node.BackColor = cw.Color;
                    nodeg.Nodes.Add(node);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            AddColor(Color.AliceBlue);
            AddColor(Color.Blue);
            AddColor(Color.Red);
            AddColor(Color.Yellow);
            AddColor(Color.White);
            AddColor(Color.Black);
            AddColor(Color.Green);
            AddColor(Color.Olive);
            AddColor(Color.Red);
            AddColor(Color.FromArgb(10,10,255));
            AddColor(Color.FromArgb(11, 10, 255));
            AddColor(Color.FromArgb(12, 10, 255));
            AddColor(Color.FromArgb(13, 10, 255));
            AddColor(Color.FromArgb(14, 10, 255));
            AddColor(Color.FromArgb(15, 10, 255));
            AddColor(Color.FromArgb(17, 10, 255));
            AddColor(Color.FromArgb(18, 10, 255));
            AddColor(Color.FromArgb(19, 10, 255));
            AddColor(Color.FromArgb(20, 10, 255));
            AddColor(Color.FromArgb(21, 10, 255));
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            int j,jj,jjj;
            j=jj=jjj=1;
            for (int i = 0; i < 256; i += j)
            {
                j++;
                for (int ii = 0; ii < 256; ii += jj)
                {
                    jj++;
                    for (int iii = 0; iii < 256; iii += jjj)
                    {
                        jjj++;
                        AddColor(Color.FromArgb(i, ii, iii));
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            int j, jj, jjj;
            j = jj = jjj = 1;
            for (int i = 0; i < 256; i += j)
            {
                j++;
                for (int ii = 0; ii < 256; ii += jj)
                {
                    jj++;
                    for (int iii = 0; iii < 256; iii += jjj)
                    {
                        jjj++;
                        AddColor(Color.FromArgb(i, ii, iii));
                        AddColor(Color.FromArgb(ii, iii, i));
                        AddColor(Color.FromArgb(iii, i, ii));
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<ColorWeight> list = new List<ColorWeight>();
            foreach (TreeNode node in treeView2.Nodes)
            {
                list.Add(new ColorWeight(node.BackColor, 1));
            }
            ColorGroup cg = ColorGroup.CreateColorGroup2(list.ToArray());
            ShowGroup(treeView1.Nodes, cg);
            ColorWeight cw = cg.Feel(0.4);
            ShowGroup(treeView1.Nodes, cg);
            TreeNode noder = new TreeNode("RES: " + String.Format("Weight: {0} Color:{1}", cw.Weight, cw.Color));
            noder.BackColor = cw.Color;
            treeView1.Nodes.Add(noder);
        }
    }
}
