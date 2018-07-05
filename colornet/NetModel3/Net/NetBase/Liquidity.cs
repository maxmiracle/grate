using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.NetBase
{
    public class Liquidity
    {

        public static double threshould(double x, double xThreshould)
        {
            if ((x - xThreshould) >= 0) return 1;
            else return 0;
        }

        /// <summary>
        /// Maximum of module of color difference permitted for liquidity 
        /// </summary>

        public static double LiquidityThreshold = 0.07;


        public static double LiquidityMax
        {
            get; set;
        }

        /// <summary>
        /// Сравниваем 
        /// </summary>
        /// <param name="pS1"></param>
        /// <param name="liquidity">Разность по модулю входного цвета</param>
        /// <param name="minS"></param>
        /// <param name="liquidityMin">Найденный ранее минимум разности входного цвета</param>
        /// <returns></returns>
        internal static bool Compare(double pS1, double liquidity, double minS, double liquidityMin)
        {
            if (pS1 > minS) return false;
            if (liquidity < liquidityMin) return true;
            else return false;
        }
    }
}
