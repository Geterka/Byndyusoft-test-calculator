using System;

namespace Byndyusoft_test_calculator
{
    internal static class Printer
    {
        /// <summary>
        /// Выводит строку в консоль
        /// </summary>
        /// <param name="value">Строка, которую нужно вывести</param>
        public static void Print(string value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// Выводит строку в консоль и ожидает нажатия любой кнопки для продолжения работы
        /// </summary>
        /// <param name="value">Строка, которую необходимо вывести</param>
        public static void DelayPrint(string value)
        {
            Console.WriteLine(value);
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        public static void Error()
        {
            Console.WriteLine("Ошибка ввода, попробуйте еще раз!");
            Console.ReadKey();
        }
    }
}
