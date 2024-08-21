using System;
using CalculatorApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_ShouldAddNumbers_ShouldSuccessfullyAdd50To7Return27()
        {
            Calculator calculator = new Calculator();
            double a = 50;
            double b = 27;
            double result = calculator.Add(a, b);
            Assert.AreEqual(result, 77);
        }
        [TestMethod]
        public void Divide_ShouldDivideNumbers_ShouldDivide16by4Return4()
        {

            Calculator calculator = new Calculator();
            double a = 16;
            double b = 4;
            double result = calculator.Divide(a, b);
            Assert.AreEqual(result, 4);
        }
        [TestMethod]
        public void Divide_ByZero_ShouldThrowArgumentException()
        {
            Calculator calculator = new Calculator();
            double a = 16;
            double b = 0;
            try
            {
                calculator.Divide(a, b);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, calculator.DevidingByZero);
            }
           
        }
        [TestMethod]
        public void Subtract_ShouldSubtractNumbers_ShouldSuccessfullySubtract10From20Return10()
        {
            Calculator calculator = new Calculator();
            double a = 20;
            double b = 10;
            double result = calculator.Subtract(a, b);
            Assert.AreEqual(result, 10);
        }
        [TestMethod]
        public void Multiply_ShouldMultiplyNumbers_ShouldMultiply4by4Return16()
        {
            Calculator calculator = new Calculator();
            double a = 4;
            double b = 4;
            double result = calculator.Multiply(a, b);
            Assert.AreEqual(result, 16);
        }

    }
}
