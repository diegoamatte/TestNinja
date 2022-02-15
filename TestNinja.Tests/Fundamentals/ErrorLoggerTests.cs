using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        public ErrorLoggerTests()
        {
            _errorLogger = new ErrorLogger();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void Log_ThrowsArgumentNullException_WhenErrorIsNullOrWhitespace(string error)
        {
            Assert.Throws<ArgumentNullException>(() => _errorLogger.Log(error));
        }

        [Fact]
        public void Log_ValidError_RaisesEvent()
        {
            var id = Guid.Empty;
            _errorLogger.ErrorLogged += (sender, args) => id = args;

            _errorLogger.Log("err");

            Assert.False(id == Guid.Empty);
        }
    }
}
