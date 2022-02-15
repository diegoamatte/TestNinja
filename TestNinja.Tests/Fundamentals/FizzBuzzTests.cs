using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class FizzBuzzTests
    {
        [Theory]
        [InlineData(15, "FizzBuzz")]
        [InlineData(30, "FizzBuzz")]
        [InlineData(3, "Fizz")]
        [InlineData(27, "Fizz")]
        [InlineData(10, "Buzz")]
        [InlineData(20, "Buzz")]
        public void GetOutput_ReturnsExpectedString(int number, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.Equal(expectedResult, result);
        }
    }
}
