using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class HtmlFormatterTests
    {
        [Fact]
        public void FormatAsBold_ReturnsStringWrappedInTag()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("a");

            Assert.StartsWith("<strong>", result);
            Assert.EndsWith("</strong>", result);
        }
    }
}
