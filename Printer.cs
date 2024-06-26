using System;

namespace Byndyusoft_test_calculator
{
    internal class Printer
    {
        /// <summary>
        /// Выводит строку в консоль
        /// </summary>
        /// <param name="value">Строка, которую нужно вывести</param>
        public void Print(string value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// Выводит строку в консоль и ожидает нажатия любой кнопки для продолжения работы
        /// </summary>
        /// <param name="value">Строка, которую необходимо вывести</param>
        public void DelayPrint(string value)
        {
            Console.WriteLine(value);
            Console.ReadKey();
        }
    }
}
