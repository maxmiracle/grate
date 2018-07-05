using NetBase.ColorMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Windows.Media.Color;
using Net.ColorMath;
using Net.NetBase;
using System.Diagnostics;
using System.Windows.Media;
using System.ComponentModel;
using NLog;

namespace NetBase
{
    public class SqRt
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        public System.Windows.Media.Color Input { get { return inputColor; } }
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
            inputColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        internal void InitializeLayerRelations()
        {
            if (Y > 0) nels.Add(XY.XYUp, layer[X, Y - 1]);
            if (Y < (layer.Height - 1)) nels.Add(XY.XYDown, layer[X, Y + 1]);
            if (X < (layer.Width - 1)) nels.Add(XY.XYRight, layer[X + 1, Y]);
            if (X > 0) nels.Add(XY.XYLeft, layer[X - 1, Y]);
        }


        internal void NeighbourGainEasy1()
        {
            //S1 = nels.Average((it) => CW.GetModule(it.Value.inputColor - inputColor));

            double imp = 0;
            foreach (var el in nels)
            {
                var colDif = CW.GetModule(el.Value.inputColor - inputColor);
                //double Nx = NeuroMath.LimitedImpedance(colDif);
                imp += colDif;
            }
            this.S1 = imp / 4;
        }

        public void CalcLiquidityDirection()
        {
            // По умолчанию указывает на себя. То есть нет направления
            XY directionMinS = XY.XYSelf;
            double minS = S1;
            double liquidityThreshold = Liquidity.LiquidityThreshold;
            foreach (var el in nels)
            {

                double pS1 = el.Value.S1;
                double pF1 = CW.GetModule(el.Value.inputColor - inputColor);
                //logger.Info("pF1: {0}", pF1);
                if (Liquidity.Compare(pS1, pF1, minS, liquidityThreshold))
                {
                    //logger.Info("(pS1: {0}, pF1: {1}, minS: {2}, liquidityThreshold: {3})", pS1, pF1, minS, liquidityThreshold);
                    directionMinS = el.Key;
                    minS = pS1;
                    liquidityThreshold = pF1;
                }
            }
            if ( minS > Liquidity.LiquidityMax)
            {
                Liquidity.LiquidityMax = minS;
            }
            LiquidityDirection = directionMinS;
            if ( LiquidityDirection != XY.XYSelf)
            {
                nels[LiquidityDirection].LiquidityInputs.Add(!LiquidityDirection);
            }
        }

        public void CalcOutputColor()
        {
            if (LiquidityGroupMaster != null)
            {
                Output = LiquidityGroupMaster.inputColor;
            }
        }
    }
}
