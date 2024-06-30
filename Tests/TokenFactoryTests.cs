using System.Linq;
using Xunit;

namespace Byndyusoft_test_calculator
{
    public class TokenFactoryTests
    {
        [Fact]
        public void TestTokenFactoryReturnsNotEmpty()
        {
            string input = "2+3";
            var library = new Library();

            Assert.Equal(3, TokenFactory.GetTokens(input, library).Count());
        }
    }
}
