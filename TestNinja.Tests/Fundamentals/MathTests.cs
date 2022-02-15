using Xunit;
using TestNinja.Fundamentals;

namespace TestNinja.Tests.Fundamentals
{
    public class MathTests
    {
        private Math _math;
        public MathTests()
        {
            _math = new Math();
        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(2,2,4)]
        [InlineData(4,2,6)]
        public void Add_ReturnsSumOfNumbers(int number1, int number2, int expectedResult)
        {
            var result = _math.Add(number1,number2);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1,2,2)]
        [InlineData(2,2,2)]
        [InlineData(8,3,8)]
        public void Max_ReturnsGreaterNumber(int number1, int number2, int expectedResult)
        {
            var result = _math.Max(number1, number2);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetOddNumbers_ReturnsOddNumbersToLimit_WhenLimitIsGreaterThanZero()
        {
            var result = _math.GetOddNumbers(10);

            Assert.Equal(new int[] { 1, 3, 5, 7, 9 }, result);
        }
    }
}
