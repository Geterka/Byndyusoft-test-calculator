using System;
using System.Collections.Generic;

namespace Byndyusoft_test_calculator
{
    internal class Library
    {
        private Dictionary<string, Func<double, double, double>> operations;
        private Dictionary<string, Func<double, double, double>> functions;

        public Library() 
        {
            operations = new Dictionary<string, Func<double, double, double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => x / y },
                { "^", (x, y) => Math.Pow(x, y) }
            };

            functions = new Dictionary<string, Func<double, double, double>>
            {
                { "root", (x, y) => Math.Pow(x, 1/y) } //корень n степени из числа
            };
        }

        public Dictionary<string, Func<double, double, double>> Ops { get { return operations; } }
        public Dictionary<string, Func<double, double, double>> Funcs { get { return functions; } }

        /// <summary>
        /// Добавление новой операции по определенному символу к возможностям калькулятора
        /// </summary>
        /// <param name="symbol">обозначение операции</param>
        /// <param name="operation">математическая формула данной операции</param>
        public void AddOperation(string symbol, Func<double, double, double> operation)
        {
            operations[symbol] = operation;
        }

        /// <summary>
        /// Добавление новой математической функции по определенному названию к возможностям калькулятора
        /// </summary>
        /// <param name="function">название функции</param>
        /// <param name="operation">математическая формула данной операции</param>
        public void AddFunction(string function, Func<double, double, double> operation)
        {
            functions[function] = operation;
        }
    }
}
