using Xunit;

namespace Byndyusoft_test_calculator
{
    public class CalculatorTests
    {
        [Fact] 
        public void CheckTypeOfReturnValueOfCalculator()
        {
            var input = "2+3";
            var rpn = new RPN();
            var calc = new Calculator();
            var library = new Library();

            var tokens = TokenFactory.GetTokens(input, library);
            var queue = rpn.GetRPN(tokens);
            var result = calc.Calculate(queue, library);

            Assert.IsType<double>(result);
        }
    }
}
