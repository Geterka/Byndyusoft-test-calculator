using System;
using System.Collections.Generic;

namespace Byndyusoft_test_calculator
{
    internal class Calculator
    {
        /// <summary>
        /// Выполняет расчет на основе очереди обратной польской записи математического выражения
        /// </summary>
        /// <param name="RPN">Очередь с обратной польской записью</param>
        /// <returns>Результат расчета с плавающей точкой</returns>
        public double Calculate(Queue<Token> RPN)
        {
            Stack<double> stack = new Stack<double>();

            double res = 0;

            foreach (var token in RPN)
            {
                string str = token.GetStr();
                switch (token.GetType())
                {
                    case Token.Type.Int:
                    case Token.Type.Float:
                        stack.Push(double.Parse(str));
                        break;

                    case Token.Type.Operator:
                        switch (token.GetAsc())
                        {
                            case Token.OperatorAssociativity.Left:
                                double[] buf = GetTwoTokens(stack);
                                if (str == "+") res = buf[0] + buf[1];
                                else if (str == "-") res = buf[1] - buf[0];
                                else if (str == "*") res = buf[0] * buf[1];
                                else if (str == "/") res = buf[1] / buf[0];
                                else if (str == "^") res = Math.Pow(buf[0], buf[1]);
                                break;

                            case Token.OperatorAssociativity.Right:
                                double a = GetOneToken(stack);
                                if (str == "-") res = -a;
                                break;
                        }
                        stack.Push(res);
                        break;

                    case Token.Type.Function:
                        if (str == "log")
                        {
                            double[] buf = GetTwoTokens(stack);
                            res = Math.Log(buf[1]) / Math.Log(buf[0]);
                        }
                        else
                        {
                            // Add more functions as needed
                        }
                        stack.Push(res);
                        break;
                }
            }

            return stack.Peek();
        }

        /// <summary>
        /// Получение последнего значения из стека
        /// </summary>
        /// <param name="stack">Стек, из которого необходимо получить значение</param>
        /// <returns>Одно число с плавающей точкой</returns>
        private double GetOneToken(Stack<double> stack)
        {
            return stack.Pop();
        }

        /// <summary>
        /// Возвращает 2 последних значения из стека
        /// </summary>
        /// <param name="stack">Стек, из которого необходимо получить значения</param>
        /// <returns>Массив, состоящий из двух значений с плавающей точкой</returns>
        private double[] GetTwoTokens(Stack<double> stack)
        {
            double[] result = new double[2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetOneToken(stack);
            }

            return result;
        }
    }
}
