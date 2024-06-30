using System;
using System.Collections.Generic;

namespace Byndyusoft_test_calculator
{
    internal class Token
    {
        // Тип
        public enum Type
        {
            Operator,      // унарный/бинарный оператор
            L_Parenthesis, // открывающая скобка
            R_Parenthesis, // закрывающая скобка
            Int,           // целое число
            Float,         // число с плавающей точкой 
            Function,      // функция
            Separator      // разделитель аргументов функции
        }

        // Ассоциативность
        public enum OperatorAssociativity
        {
            None,  // токен - не оператор
            Right, // правоассоциативный
            Left   // левоассоциативный
        }

        private Type _type;
        private OperatorAssociativity _opAsc;
        private string _str;

        public Token(string token, Type type, OperatorAssociativity asc = OperatorAssociativity.None)
        {
            _type = type;
            _str = token;

            // если токен - оператор, но ассоциативность не задана - ошибка (алгоритма)
            if (type == Type.Operator && asc == OperatorAssociativity.None)
                throw new InvalidOperationException("Associativity required!");

            // если токен - НЕ оператор, но ассоциативность задана - ошибка
            else if (type != Type.Operator && asc != OperatorAssociativity.None)
                throw new InvalidOperationException("Non-operator token can't have an associativity!");

            _opAsc = asc;
        }

        // Приоритет
        public int GetPrecendance()
        {
            Dictionary<string, int> op_leftassociative = new Dictionary<string, int>
            {
                {"+", 2},
                {"-", 2},
                {"/", 3},
                {"*", 3},
                {"^", 5}
            };

            Dictionary<string, int> op_rightassociative = new Dictionary<string, int>
            {
                {"-", 4} // унарное отрицание
            };

            // В зависимости от ассоциативности один и тот же символ означает разные операторы
            switch (_opAsc)
            {
                case OperatorAssociativity.Left:
                    // Если str явлеяется ключом map-а, значит мы знаем такой оператор
                    if (op_leftassociative.ContainsKey(_str)) return op_leftassociative[_str];
                    else throw new Exception("Unknown Operator!");
                case OperatorAssociativity.Right:
                    if (op_rightassociative.ContainsKey(_str)) return op_rightassociative[_str];
                    else throw new Exception("Unknown Operator!");
                case OperatorAssociativity.None:
                    throw new InvalidOperationException($"Token \"{_str}\" is not an operator, impossible.");
                default:
                    throw new InvalidOperationException("Invalid operator associativity.");
            }
        }

        public Type GetTokenType() { return _type; }
        public OperatorAssociativity GetAsc() { return _opAsc; }
        public string GetStr() { return _str; }
    }
}
