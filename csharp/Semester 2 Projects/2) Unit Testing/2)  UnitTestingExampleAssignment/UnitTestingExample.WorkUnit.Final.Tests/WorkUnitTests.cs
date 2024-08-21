using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.WorkUnit.Final;

namespace UnitTestingExample.WorkUnit.Final.Tests
{
    [TestClass]
    public class WorkUnitTests
    {
        [TestMethod]
        public void MockedFibonacciTest()
        {
            // Arrange
            var moqLogger = new Mock<ILogger>();

            moqLogger
                .Setup(m => m.Log(It.Is<string>(s => string.Compare(s, "Starting Fibonacci calculation", StringComparison.CurrentCulture) == 0)))
                .Verifiable();

            moqLogger
                .Setup(m => m.Log(It.Is<string>(s => string.Compare(s, "Finished calculation for n=10: 55", StringComparison.CurrentCulture) == 0)))
                .Verifiable();

            // Act
            var workUnit = new UnitTesting.WorkUnit.Final.WorkUnit(moqLogger.Object);
            var result = workUnit.GetNthFibonacci(10);

            // Assert
            result.Should().Be(55);
            moqLogger.Verify();
        }

        [TestMethod]
        public void InvalidNValueTest()
        {
            var moqLogger = new Mock<ILogger>();
            var workUnit = new UnitTesting.WorkUnit.Final.WorkUnit(moqLogger.Object);
            Action act = () => { workUnit.GetNthFibonacci(-1); };
            act
                .Should().Throw<ArgumentException>()
                .WithMessage($"must be a positive integer{Environment.NewLine} parameter name: n");
        }

        [TestMethod]
        public void TestShortcutForNIs0()
        {
            var moqLogger = new Mock<ILogger>();
            var workUnit = new UnitTesting.WorkUnit.Final.WorkUnit(moqLogger.Object);
            var result = workUnit.GetNthFibonacci(0);
            result.Should().Be(0);
            moqLogger.Verify(m => m.Log(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        public void TestShortcutForNIs1()
        {
            var moqLogger = new Mock<ILogger>();
            var workUnit = new UnitTesting.WorkUnit.Final.WorkUnit(moqLogger.Object);
            var result = workUnit.GetNthFibonacci(1);
            result.Should().Be(1);
            moqLogger.Verify(m => m.Log(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
