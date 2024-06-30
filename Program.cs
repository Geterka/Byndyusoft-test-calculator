using System;

namespace Byndyusoft_test_calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CheckString checkString = new CheckString();
            RPN rpn = new RPN();
            Calculator calculator = new Calculator();
            Library library = new Library();
            library.AddOperation("%", (x, y) => x % y);

            for (; ; )
            {
                string intro = "Введите выражение:";
                Printer.Print(intro);
                string input = Console.ReadLine();

                if(checkString.Check(input, library) == false)
                {
                    Printer.DelayPrint("Ошибка ввода, попробуйте еще раз!");
                    Console.Clear();
                    continue;
                }

                var tokens = TokenFactory.GetTokens(input.Replace(" ", ""), library);
                
                var queue = rpn.GetRPN(tokens);

                var output = "Результат: " + calculator.Calculate(queue, library);

                Printer.DelayPrint(output);
                Console.Clear();
            }
        }
    }
}
