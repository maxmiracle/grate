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
        public Composition ProcessImage(Image image) {
            return null;
        }

        public bool Assess(Composition composition)
        {
            var results = modelParameters.Criteria.AsParallel().Select((criterion) => (criterion.Assess(this, composition)));
            return !results.Contains(false);

        }
    }
}
