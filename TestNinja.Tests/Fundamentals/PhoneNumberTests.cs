using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Parse_ThrowsArgumentException_WhenNumberIsNullOrWhite(string number)
        {
            var exception = Assert.Throws<ArgumentException>(() => PhoneNumber.Parse(number));
            Assert.Equal("Phone number cannot be blank.", exception.Message);
        }
        
        [Theory]
        [InlineData("123456789")]
        [InlineData("12345678901")]
        public void Parse_ThrowsArgumentException_WhenNumberLengthNotEqualTo10(string number)
        {
            var exception = Assert.Throws<ArgumentException>(() => PhoneNumber.Parse(number));
            Assert.Equal("Phone number should be 10 digits long.", exception.Message);
        }

        [Theory]
        [InlineData("1234567890","(123)456-7890")]
        public void Parse_ReturnsExpectedFormat(string number, string expectedResult)
        {
            var result = PhoneNumber.Parse(number);

            Assert.Equal(expectedResult, result.ToString());
        }

    }
}
