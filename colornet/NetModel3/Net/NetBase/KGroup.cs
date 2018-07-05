using NetBase;
using NetBase.ColorMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Net.NetBase
{
    /// <summary>
    /// Строится граф доминирования с оценкой близости.
    /// Ввести барьер по совокупному критерию.
    /// Близость точек границы + близость средних цветов -> K1 - Ура я придумал новый критерий.
    /// Доминирование между группами -> или L2 - новый критерий текучести для групп.
    /// L2 - Это  (Сумма разниц между соседними группами)  / количество соседних групп/  количество точек)
    /// Нужно научиться делать следуещее:
    ///
    /// Группа
    /// Получить средний цвет.
    /// Найти все соседние группы.
    /// Получить границу с другой группой.
    /// Получить разницу границы с другой группой.
    /// 
    /// Попробовать по минимуму вводить дополнительные данные в группу
    /// Пусть ссылка на группу храниться в доминирующем элементе первого уровня.
    /// </summary>
    public class KGroup
    {

        // Get all elements of the group
        public static List<SqRt> GetAllElements(SqRt masterElement)
        {

            var q = from item in masterElement.Layer
                    where item.LiquidityGroupMaster == masterElement
                    select item;
            return q.ToList();
            
        }

        // Get Average Color
        public static Color GetAverageColor(SqRt masterElement)
        {
            var q = from item in masterElement.Layer
                    where item.LiquidityGroupMaster == masterElement
                    select item.Input;
            return q.Aggregate<Color, ColorWeightSum>(ColorWeightSum.BlackNull, (cw, col) => (cw + new ColorWeight(col))).Average().Color;
        }

        // Get neighbours groups
        public static List<SqRt> GetNeighbours(SqRt masterElement)
        {
            // все элементы группы
            var q = from item in masterElement.Layer
                    where item.LiquidityGroupMaster == masterElement
                    select item;
            // все соседи 
            var r = q.SelectMany<SqRt, SqRt>(it => it.nels.Values).Distinct<SqRt>().Where( sqrt => sqrt.LiquidityGroupMaster != masterElement );
            // мастер элементы соседей (соседние группы)
            return r.Select<SqRt, SqRt>(it => it.LiquidityGroupMaster).Distinct().ToList();
        }

        
            
    }
}
