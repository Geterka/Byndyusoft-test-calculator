using Xunit;

namespace Byndyusoft_test_calculator.Tests
{
    public class RPNTests
    {
        [Fact]
        public void RPNIsNotNullOrEmpty()
        {
            string input = "2+3";
            var library = new Library();
            var tokens = TokenFactory.GetTokens(input, library);
            var rpn = new RPN();

            Assert.NotNull(rpn.GetRPN(tokens));
            Assert.NotEmpty(rpn.GetRPN(tokens));
        }
    }
}
