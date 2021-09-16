using NUnit.Framework;
using SchroedingerParallelPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchroedingerParallelPI.Tests
{
    [TestFixture()]
    public class PIBerechnenParallelVariantenTests
    {
        const double PIApprox = 3.14159265;
        private Stopwatch _stopWatch;


        [SetUp]
        public void Init()
        {
            _stopWatch = Stopwatch.StartNew();
        }

        [TearDown]
        public void Cleanup()
        {
            _stopWatch.Stop();
            TestContext.WriteLine("Excution time for {0} - {1} ms",
            TestContext.CurrentContext.Test.Name,
            _stopWatch.ElapsedMilliseconds);
        }

        [Test()]
        public void SerialPiTest()
        {
            // Arrange
            double PIBerechnet;

            //Act
            PIBerechnet = PIBerechnenParallelVarianten.SerialPi();

            //Assert
            Assert.AreEqual(PIApprox, PIBerechnet, 0.00005);
        }

        [Test()]
        public void MyParallelPiEinfachTest()
        {
            // Arrange
            double PIBerechnet;

            //Act
            PIBerechnet = PIBerechnenParallelVarianten.MyParallelPiEinfach();

            //Assert
            Assert.AreEqual(PIApprox, PIBerechnet, 0.00005);
        }

        [Test()]
        public void ParallelPiTest()
        {
            // Arrange
            double PIBerechnet;

            //Act
            PIBerechnet = PIBerechnenParallelVarianten.ParallelPi();

            //Assert
            Assert.AreEqual(PIApprox, PIBerechnet, 0.00005);
        }

        [Test()]
        public void ParallelPartitionerPiTest()
        {
            // Arrange
            double PIBerechnet;

            //Act
            PIBerechnet = PIBerechnenParallelVarianten.ParallelPartitionerPi();

            //Assert
            Assert.AreEqual(PIApprox, PIBerechnet, 0.00005);
        }
    }
}