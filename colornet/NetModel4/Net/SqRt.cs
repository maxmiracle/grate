using System.Collections.Generic;
using Color = System.Windows.Media.Color;


namespace Net
{
    public class SqRt
    {

        public SqRt(int x, int y, SqImgLayer layer)
        {
            this.x = x;
            this.y = y;
            this.layer = layer;
            nels = new Dictionary<XY, SqRt>();
            LiquidityInputs = new List<XY>();
        }
        private int x;
        private int y;
        private SqImgLayer layer;
        private Color inputColor;
        /// <summary>
        /// Neighbours elements = nels
        /// </summary>
        public Dictionary<XY, SqRt> nels;



        public int X { get { return x; } }
        public int Y { get { return y; } }
        public SqImgLayer Layer { get { return layer; } }

        public Color Input { get { return inputColor; } }
        public Color Output { get; internal set; }

        /// <summary>
        /// Average difference of Inputs the element with Neighbours elements (nels). Calculate by NeighbourGainEasy1 function.
        /// </summary>
        public double S1 { get; set; }

        // Liquidity
        public XY LiquidityDirection;
        public List<XY> LiquidityInputs;
        public SqRt LiquidityGroupMaster;


        public void setInput(System.Drawing.Color color)
        {
            inputColor = Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        internal void InitializeLayerRelations()
        {
            if (Y > 0) nels.Add(XY.XYUp, layer[X, Y - 1]);
            if (Y < (layer.Height - 1)) nels.Add(XY.XYDown, layer[X, Y + 1]);
            if (X < (layer.Width - 1)) nels.Add(XY.XYRight, layer[X + 1, Y]);
            if (X > 0) nels.Add(XY.XYLeft, layer[X - 1, Y]);
        }


    }
}
