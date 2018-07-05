using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.NetBase;
using Net.View;
using NetBase;
using NLog;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Net.Tests.T1
{
    [TestClass]
    public class T1Test
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static string relPath = "../../Tests/T1/";

        string getPath(string fileName)
        {
            return Path.GetFullPath(relPath + fileName);

        }


        [TestMethod]
        public void Test1_GX_Decomposing()
        {
            
            Bitmap GX = new Bitmap(getPath("GX.png"));
            Bitmap G0 = new Bitmap(getPath("G0.bmp"));
            Bitmap G1 = new Bitmap(getPath("G1.bmp"));


            ImgComposition composition;
            PhotoNeuron.DecomposeImg(GX, out composition);
            Assert.IsNotNull(composition, "composition object should be returned as out param and not null");
            Assert.IsNotNull(composition.Groups, "Groups collection should be not null");
            Assert.AreEqual(2, composition.Groups.Count, "Number of groups should be 2");
            string msg;
            Assert.AreEqual(true, SGroup.Compare(G0 , composition.Groups[0].getBitmask(), out msg), string.Format("Group 0 is wrong {0}", msg));
            Assert.AreEqual(true, SGroup.Compare(G1, composition.Groups[1].getBitmask(), out msg), string.Format("Group 1 is wrong {0}", msg));
        }

        [TestMethod]
        public void Test2_GX1_Decomposing()
        {

            Bitmap GX = new Bitmap(getPath("GX1.png"));
            Bitmap G0 = new Bitmap(getPath("G0.bmp"));
            Bitmap G1 = new Bitmap(getPath("G1.bmp"));


            ImgComposition composition;
            PhotoNeuron.DecomposeImg(GX, out composition);
            Assert.IsNotNull(composition, "composition object should be returned as out param and not null");
            Assert.IsNotNull(composition.Groups, "Groups collection should be not null");
            Assert.AreEqual(2, composition.Groups.Count, "Number of groups should be 2");
            string msg;
            Assert.AreEqual(true, SGroup.Compare(G0, composition.Groups[0].getBitmask(), out msg), string.Format("Group 0 is wrong {0}", msg));
            Assert.AreEqual(true, SGroup.Compare(G1, composition.Groups[1].getBitmask(), out msg), string.Format("Group 1 is wrong {0}", msg));
        }

        [TestMethod]
        public void Test3_GX2_Decomposing()
        {

            Bitmap GX = new Bitmap(getPath("GX2.png"));
            Bitmap G0 = new Bitmap(getPath("G0.bmp"));
            Bitmap G1 = new Bitmap(getPath("G1.bmp"));


            ImgComposition composition;
            PhotoNeuron.DecomposeImg(GX, out composition);
            Assert.IsNotNull(composition, "composition object should be returned as out param and not null");
            Assert.IsNotNull(composition.Groups, "Groups collection should be not null");
            Assert.AreEqual(2, composition.Groups.Count, "Number of groups should be 2");
            string msg;
            Assert.AreEqual(true, SGroup.Compare(G0, composition.Groups[0].getBitmask(), out msg), string.Format("Group 0 is wrong {0}", msg));
            Assert.AreEqual(true, SGroup.Compare(G1, composition.Groups[1].getBitmask(), out msg), string.Format("Group 1 is wrong {0}", msg));
        }

        [TestMethod]
        public void Test4_LDM_AllTheSame()
        {
            Bitmap GX2 = new Bitmap(getPath("GX2.png"));
            SqImgLayer layer = new SqImgLayer(GX2);
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 1], layer[5, 1]), "Equal colors in the line should be liquid");
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 2], layer[5, 2]), "Equal colors in the line should be liquid");
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 3], layer[5, 3]), "Equal colors in the line should be liquid");
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 4], layer[5, 4]), "Equal colors in the line should be liquid");
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 5], layer[5, 5]), "Equal colors in the line should be liquid");
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[4, 6], layer[5, 6]), "Equal colors in the line should be liquid");
        }

        [TestMethod]
        public void Test6_LDM_NextToBorder()
        {
            Bitmap GX2 = new Bitmap(getPath("GX2.png"));
            SqImgLayer layer = new SqImgLayer(GX2);
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[2, 2], layer[3, 2]), "Equal colors in the line should be liquid");
        }

        [TestMethod]
        public void Test5_LDM_Gradient()
        {
            Bitmap GX2 = new Bitmap(getPath("GX2.png"));
            SqImgLayer layer = new SqImgLayer(GX2);
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[2, 4], layer[2, 5]), "Gradient test positive. In gradient field.");

            Assert.IsFalse(PhotoNeuron.LinearDifferenceMethod(layer[2, 1], layer[2, 2]), "Gradient test negative. It is border.");
        }

        [TestMethod]
        public void Test7_LDM_SoftBorder()
        {
            Bitmap GX2 = new Bitmap(getPath("GX2.png"));
            SqImgLayer layer = new SqImgLayer(GX2);
            Assert.IsFalse(PhotoNeuron.LinearDifferenceMethod(layer[2, 7], layer[2, 8]), "Gradient change should be a border");
        }

        [TestMethod]
        public void Test8_LDM_NextToBorder()
        {
            Bitmap GX2 = new Bitmap(getPath("GX2.png"));
            SqImgLayer layer = new SqImgLayer(GX2);
            Assert.IsTrue(PhotoNeuron.LinearDifferenceMethod(layer[2, 2], layer[2, 3]), "Gradient and Border, but should be liquid");
        }

    }
}
