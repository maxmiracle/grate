using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Test
{
    class ModelTest
    {
        [Test]
        public void ModelConstruct()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Model(null));
        }

        [Test]
        public void ModelDoComposition()
        {
            // Создаем модель
            var model = new Model(new ModelParameters());
            // Загружаем тестовое изображение.
            Image testImage = TestImages.getTestImage(0);
            // Получаем результат работы модели - композицию
            var composition = model.ProcessImage(testImage);
            // 

            var assess1 = model.Assess(composition);
            Assert.IsTrue(assess1, "Assessment should be true - OK");
        }
    }
}
