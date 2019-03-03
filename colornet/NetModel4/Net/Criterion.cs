using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    /// <summary>
    /// Measure common API
    /// </summary>    
    public delegate double MeasureAction<TModel, TResult>(TModel model, TResult result);

    /// <summary>
    /// Assessment method
    /// </summary>
    /// <param name="measureValue"></param>
    /// <returns>true if measurement satisfy criterion</returns>
    public delegate bool AssessAction(double measureValue);


    /// <summary>
    /// Criterion = SuperpositionOf<MeasureAction, AssessAction>
    /// </summary>
    public class Criterion<TModel, TResult>
    {
        public MeasureAction<TModel, TResult> Measure{get; set;}
        public AssessAction Assessment{get; set;}

        public bool Assess(TModel model, TResult result)
        {
            return Assessment(Measure(model, result));
        }
    }
}
