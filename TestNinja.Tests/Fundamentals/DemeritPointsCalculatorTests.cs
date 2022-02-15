using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        public DemeritPointsCalculatorTests()
        {
            _calculator = new DemeritPointsCalculator();
        }

        [Theory]
        [InlineData(-15)]
        [InlineData(301)]
        public void CalculateDemeritPoints_ThrowsArgumentOutOfRangeException_WhenSpeedIsOutOfBounds(int speed)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateDemeritPoints(speed));
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(65, 0)]
        [InlineData(66, 0)]
        [InlineData(70, 1)]
        [InlineData(75, 2)]
        public void CalculateDemeritPoints_ReturnsDemeritpoints(int speed, int expectedResult)
        {
            Assert.Equal(expectedResult, _calculator.CalculateDemeritPoints(speed));
        }
    }
}
