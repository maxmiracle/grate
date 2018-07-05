using System.Drawing;
using Net.NetBase;
using NetBase;
using Net.ColorMath;
using System.Linq;
using NLog;
using Net.View;

namespace Net
{
    /// <summary>
    /// Class PhotoNeuron is a facade of the PhotoNeuron library.
    /// </summary>
    public static class PhotoNeuron
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Describes types of algorithms of liquidity equation
        /// </summary>
        public enum LiquidityType
        {
            /// <summary>
            /// Delta of Colors should be not more then LiquidityDeltaThreshold
            /// </summary>
            DeltaThreshold,

            /// <summary>
            /// Line from 3 elements (this side) plus 3 elements (other side)
            /// </summary>
            LinearDifference

        }



        public static void DecomposeImg(Bitmap bitmap, LiquidityType liquidityType, out ImgComposition composition)
        {
            SqImgLayer layer = new SqImgLayer(bitmap);
            switch (liquidityType)
            {
                case LiquidityType.DeltaThreshold:
                    composition = layer.GenerateCompositionOverLiquidity((q1, q2) => DeltaThresholdMethod(q1, q2));
                    break;
                case LiquidityType.LinearDifference:
                    composition = layer.GenerateCompositionOverLiquidity((q1, q2) => LinearDifferenceMethod(q1, q2));
                    //Debug
                    var window = new ShowImageLiquidity(layer, composition);
                    window.ShowDialog();
                    break;
                default:
                    throw new System.NotImplementedException();
            }

        }

        private static bool DeltaThresholdMethod(SqRt q1, SqRt q2)
        {
            return q1.Input.Delta(q2.Input) < LiquidityDeltaThreshold;
        }

        /// <summary>
        /// Parameter for DeltaThreshold liquidity
        /// </summary>
        public static double LiquidityDeltaThreshold = 10;

        public static double LDM_DeltaThreshold = 10;
        public static double LDM_DeltaThreshold_Condition1 = 100;
        public static double LDM_Derivative2Threshold = 40;

        public static bool LinearDifferenceMethod(SqRt q1, SqRt q2)
        {
            var lineDirection = q1.nels.FirstOrDefault(x => x.Value == q2).Key;
            XY oppositeDir = lineDirection * (-1);

            SqRt sqrt;
            ColorVector defaultColor = ColorVector.Undefined;

            ColorVector c5 = q1.Input;
            // Color of cell x = -1 coordinate, assume q1 cell is x = 0; q2 cell is x=1. If it is border then take defaultColor
            ColorVector c4 = q1.nels.TryGetValue(oppositeDir, out sqrt) ? sqrt.Input : defaultColor;
            // Color of cell x = -2 coordinate if cell is exist
            ColorVector c3 = (sqrt?.nels.TryGetValue(oppositeDir, out sqrt) ?? false) ? sqrt.Input : defaultColor;
            ColorVector c2 = (sqrt?.nels.TryGetValue(oppositeDir, out sqrt) ?? false) ? sqrt.Input : defaultColor;
            ColorVector c6 = q2.Input;
            ColorVector c7 = q2.nels.TryGetValue(lineDirection, out sqrt) ? sqrt.Input : defaultColor;
            ColorVector c8 = (sqrt?.nels.TryGetValue(lineDirection, out sqrt) ?? false) ? sqrt.Input : defaultColor;
            ColorVector c9 = (sqrt?.nels.TryGetValue(lineDirection, out sqrt) ?? false) ? sqrt.Input : defaultColor;

            logger.Debug("Colors c2={0}, c3={1}, c4={2}, c5={3}, c6={4}, c7={5}, c8={6}, c9={7}", c2, c3, c4, c5, c6, c7, c8, c9);

            var d2 = c3 - c2;
            var d3 = c4 - c3;
            var d4 = c5 - c4;
            var d5 = c6 - c5; // Delta 
            var d6 = c7 - c6;
            var d7 = c8 - c7;
            var d8 = c9 - c8;

            logger.Debug("Delta d2={0}, d3={1}, d4={2}, d5={3}, d6={4}, d7={5}, d8={6}", d2, d3, d4, d5, d6, d7, d8);

            var dd2 = d3 - d2;
            var dd3 = d4 - d3;
            var dd4 = d5 - d4;
            var dd5 = d6 - d5;
            var dd6 = d7 - d6;
            var dd7 = d8 - d7;

            logger.Debug("Derivation'2 dd2={0}, dd3={1}, dd4={2}, dd5={3}, dd6={4}, dd7={5}", dd2, dd3, dd4, dd5, dd6, dd7);
            logger.Debug("Derivation'2 Modules dd2={0}, dd3={1}, dd4={2}, dd5={3}, dd6={4}, dd7={5}", dd2.GetModule(), dd3.GetModule(), dd4.GetModule(), dd5.GetModule(), dd6.GetModule(), dd7.GetModule());

            //TODO: Equations and Liquidity threshold;
            // First approach
            var d5_module = d5.GetModule();
            bool first_res = d5_module < LDM_DeltaThreshold;
            // first_res is main unconditional criterion if it is fit then return true
            if (first_res) return true;

            // second conditional criterion
            bool second_res = d5_module < LDM_DeltaThreshold_Condition1;
            // Return - non liquidity
            if (!second_res) return false;

            // Second approach - check second derivative (dd)
            if ((!dd4.isUndefined) && (dd4.GetModule() > LDM_Derivative2Threshold )) return false;

            if ((!dd5.isUndefined) && (dd5.GetModule() > LDM_Derivative2Threshold)) return false;

            return true;
        }

        public static void DecomposeImg(Bitmap bitmap, out ImgComposition composition)
        {
            DecomposeImg(bitmap, LiquidityType.LinearDifference, out composition);
        }
    }
}
