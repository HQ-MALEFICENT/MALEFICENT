namespace BalloonsPopsTests
{
    using System;
    using BalloonsPopsGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class GameTest
    {
        private byte[,] matrix;

        [TestInitialize]
        public void TestInitialize()
        {
            this.matrix = MatrixUtils.CreateMatrix();
        }

        [TestMethod]
        public void TestMethod1()
        {
          
        }
    }
}
