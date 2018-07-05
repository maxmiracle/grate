
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using Net.ColorMath;

namespace NetBase.View
{
    [TestClass]
    public class SqImgLayerTest
    {
        [TestCategory("Manual")]
        //[TestMethod]
        public void ShowWindowTest()
        {
            SqImgLayer layer = SqImgLayer.GetLayer();
            SqImgLayerWindow window = new SqImgLayerWindow(layer);
            window.ShowDialog();
        }

        [TestMethod]
        public void TestColorCompare()
        {
            Color black = Colors.Black;
            Color white = Colors.White;
         
            bool b1 = white.ToColor().ToArgb() > black.ToColor().ToArgb();
            Assert.AreEqual(true, b1, "Expected White is bigger then Black");

            bool b2 = black.IsMore(white);
            Assert.AreEqual(false, b2, "Expected Black is less then White");
        }
    }
}
