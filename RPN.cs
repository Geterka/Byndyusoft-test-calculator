using System.Collections.Generic;

namespace Byndyusoft_test_calculator
{
    internal class RPN
    {
        /// <summary>
        /// Получение очереди значений из токенов на основе алгоритма сортировочной станции
        /// </summary>
        /// <param name="input">Входная строка с математическим выражением</param>
        /// <returns>Строка обратной польской записи</returns>
        public Queue<Token> GetRPN(List<Token> tokens)
        {
            var output = new Queue<Token>();

            var stack = new Stack<Token>();

            void fromStackToQueue() { output.Enqueue(stack.Pop()); }

            foreach (var token in tokens)
            {
                switch (token.GetType())
                {
                    case Token.Type.Int:
                    case Token.Type.Float:
                        output.Enqueue(token);
                        break;
                    case Token.Type.L_Parenthesis:
                    case Token.Type.Function:
                        stack.Push(token);
                        break;
                    case Token.Type.Operator:
                        if (stack.Count > 0)
                        {
                            while (stack.Peek().GetType() == Token.Type.Operator &&
                                   ((stack.Peek().GetPrecendance() > token.GetPrecendance()) ||
                                   (stack.Peek().GetPrecendance() == token.GetPrecendance() 
                                   && token.GetAsc() == Token.OperatorAssociativity.Left)))
                            {
                                fromStackToQueue();
                                if (stack.Count == 0)
                                    break;
                            }
                        }
                        stack.Push(token);
                        break;
                    case Token.Type.R_Parenthesis:

                        while (stack.Peek().GetType() != Token.Type.L_Parenthesis)
                        {
                            fromStackToQueue();
                        }

                        stack.Pop();

                        if (stack.Count > 0 && stack.Peek().GetType() == Token.Type.Function)
                            fromStackToQueue();
                        break;
                    case Token.Type.Separator:

                        while (stack.Peek().GetType() != Token.Type.L_Parenthesis)
                        {
                            fromStackToQueue();
                        }
                        break;
                }
            }

            while (stack.Count > 0)
            {
                fromStackToQueue();
            }

            return output;
        }
    }
}
