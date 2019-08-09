using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Net
{

    /// <summary>
    /// Класс Model агрегирует все операции с моделями. 
    /// </summary>
    public class Model
    {
        private readonly ModelParameters modelParameters;


        // Конструктор базовой прототипной реализации модели.
        public Model(ModelParameters modelParameters)
        {
            this.modelParameters = modelParameters ?? throw new ArgumentNullException(nameof(modelParameters));

        }


        // Find composition for the image using the model
        public Composition ProcessImage(Image image)
        {
            return null;
        }

        /// <summary>
        /// Оценка модели по всем критериям.
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public bool Assess(Composition composition)
        {
            var results = modelParameters.Criteria.AsParallel().Select((criterion) => (criterion.Assess(this, composition)));
            return !results.Contains(false);
        }

        /// <summary>
        /// Функция описывает элементарный цикл обучения, который состоит из опыта, получения оценки(обратной связи), коррекции
        /// Input:Image, Function:ProcessImage, Result:Composition, KnownTarget:Composition, Learn 
        /// </summary>
        public void TryAndLearn(Image image, Composition rightResult)
        {
            // Опыт
            Composition comosition =  ProcessImage(image);
            // Разница
            var diff = new { comosition, rightResult };
            // Обучение
            //Learn(image, diff);
        }
    }
}
