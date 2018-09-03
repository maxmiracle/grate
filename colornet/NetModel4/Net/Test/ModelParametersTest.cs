using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Test
{
    
    class ModelParametersTest
    {
        [Test]
        public void MaxObjectsTest()
        {
            ModelParameters modelParameters = new ModelParameters();
            Assert.That(modelParameters.MaxObjects, Is.GreaterThan(0), "ModelParametes.MaxObjects should be greater than 0");
            Assert.That(modelParameters.MinObjects, Is.GreaterThan(0), "ModelParametes.MinObjects should be greater than 0");
            Assert.That(modelParameters.MaxObjects, Is.GreaterThan(modelParameters.MinObjects), "ModelParameters.MaxObjects should be greater than ModelParameters.MinObjects");
        }
    }
}
