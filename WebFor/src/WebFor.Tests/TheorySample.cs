using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WebFor.Tests
{
    public class MemoryCalculator 
    {
        public int CurrentValue { get; private set; }

        public void Subtract(int number)
        {
            CurrentValue -= number;
        }
    }


    public class TheorySample
    {
        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void AllNumber_ShouldBe_Odd(int value)
        {
            Assert.True(IsOdd(value));
        }


        [Theory]
        [InlineData(5, 10, -15)]
        [InlineData(-5, -10, 15)]
        [InlineData(10, 0, -10)]
        [InlineData(0, 0, 0)]
        [InlineData(-99, 99, 0)]
        public void ShouldSubtractTwoNumbers(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Subtract(firstNumber);
            sut.Subtract(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }

    }
}
