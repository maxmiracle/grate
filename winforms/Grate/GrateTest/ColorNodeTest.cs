using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grate.ColorDispersion;
using System.Drawing;

namespace GrateTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ColorNodeTest
    {
        public ColorNodeTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [ExpectedException((typeof(ColorDispersionException)), "ColorGroup count couldn't be less then 2")]
        public void TestContructorColorNodeException()
        {
            ColorWeight cw = new ColorWeight( Color.Red, 0.6);
            ColorGroup cg = ColorGroup.CreateColorGroup(cw);
        }

        [TestMethod]
        public void TestContructorColorNode()
        {
            ColorWeight cw1 = new ColorWeight( Color.Red, 0.6);
            ColorWeight cw2 = new ColorWeight(Color.Yellow, 0.8);
            ColorGroup cg = ColorGroup.CreateColorGroup(cw1, cw2);
            Assert.IsInstanceOfType(cg, typeof(ColorGroup));
        }

        [TestMethod]
        public void TestColorNodeAddTwoNodes()
        {
            ColorWeight cw1 = new ColorWeight(Color.Red, 1);
            ColorWeight cw2 = new ColorWeight(Color.OrangeRed, 1);
            ColorWeight cw3 = new ColorWeight(Color.Green, 1);
            ColorGroup cg = ColorGroup.CreateColorGroup(cw1, cw2, cw3);
        }

        [TestMethod]
        public void TestColorSumWeight()
        {
            ColorWeight cw1 = new ColorWeight(Color.Red, 1);
            ColorWeight cw2 = new ColorWeight(Color.OrangeRed, 1);
            ColorWeight cw3 = new ColorWeight(Color.Green, 1);
            ColorWeightSum sum = ColorWeightSum.BlackNull;
            sum += cw1;
            Assert.AreEqual(1, sum.Weight);
            sum += cw2;
            Assert.AreEqual(2, sum.Weight);
            sum += cw3;
            Assert.AreEqual(3, sum.Weight);
            ColorWeight cw4 = ColorWeight.Sum(cw1, cw2, cw3);
            Assert.AreEqual(3, cw4.Weight);
        }


        [TestMethod]
        public void TestByHumanVision()
        {
            ColorGroupTreeTest view = new ColorGroupTreeTest();
            view.ShowDialog();
        }
    }
}
