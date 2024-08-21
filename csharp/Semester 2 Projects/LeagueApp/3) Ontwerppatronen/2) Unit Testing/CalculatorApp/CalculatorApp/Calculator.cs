using System;

namespace CalculatorApp
{
    
    /// <summary>
    /// Represents a simple calculator
    /// </summary>
    public class Calculator
    {
        public string DevidingByZero = "Cannot divide by zero";

        /// <summary>
        /// Adds 2 numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of 2 numbers</returns>
        public double Add(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// Subtracts 2 numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Difference of 2 numbers</returns>
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplies 2 numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Product of 2 numbers</returns>
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides 2 numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Quotient of 2 numbers</returns>
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new System.ArgumentException(DevidingByZero);

            }
                
            return a / b;
        }

    }
}