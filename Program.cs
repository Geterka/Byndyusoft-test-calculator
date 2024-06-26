using System;

namespace Byndyusoft_test_calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            CheckString checkString = new CheckString();
            RPN rpn = new RPN();
            Calculator calculator = new Calculator();

            for(; ; )
            {
                string intro = "Введите выражение:";
                printer.Print(intro);
                string input = Console.ReadLine();

                if(checkString.Check(input) == false)
                {
                    printer.DelayPrint("Ошибка ввода, попробуйте еще раз!");
                    Console.Clear();
                    continue;
                }

                var tokens = TokenFactory.GetTokens(input.Replace(" ", ""));
                
                var queue = rpn.GetRPN(tokens);

                var output = "Результат: " + calculator.Calculate(queue);

                printer.DelayPrint(output);
                Console.Clear();
            }
        }
    }
}
