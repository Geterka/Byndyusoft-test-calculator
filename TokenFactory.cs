using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft_test_calculator
{
    internal class TokenFactory
    {
        private enum State
        {
            S0,
            S1,
            S2,
            S3,
            S4,
            S5
        }

        /// <summary>
        /// Гененрирует список токенов из входной строки
        /// </summary>
        /// <param name="value">Входная строка с математическим выражением</param>
        /// <returns>Список токенов</returns>
        public static List<Token> GetTokens(string value, Library library)
        {
            
            State state = State.S0;
            List<Token> tokens = new List<Token>();

            string validOperators = string.Empty;

            foreach (var key in library.Ops.Keys)
            {
                validOperators += key;
            }

            bool isDigit, isLetter, isOp, isParanth, isPoint, isSep, isLParanth, isRParanth;

            string buffer = string.Empty;
            Token.Type bufferTokenType = Token.Type.Int;

            foreach (var s in value)
            {
                isDigit = char.IsDigit(s);
                isLetter = char.IsLetter(s);
                isLParanth = s == '(';
                isRParanth = s == ')';
                isParanth = isLParanth || isRParanth;
                isPoint = s == '.';
                isSep = s == ',';
                isOp = validOperators.Contains(s);

                switch (state)
                {
                    case State.S0:
                        if (isOp || isParanth)
                            state = State.S1;
                        else if (isDigit)
                            state = State.S2;
                        else if (isLetter)
                            state = State.S4;
                        break;
                    case State.S1:
                        if (isDigit)
                            state = State.S2;
                        else if (isLetter)
                            state = State.S4;
                        break;
                    case State.S2:
                        bufferTokenType = Token.Type.Int;
                        if (isPoint)
                            state = State.S3;
                        else if (isParanth || isOp || isSep)
                            state = State.S5;
                        break;
                    case State.S3:
                        bufferTokenType = Token.Type.Float;
                        if (isParanth || isOp || isSep)
                            state = State.S5;
                        break;
                    case State.S4:
                        bufferTokenType = Token.Type.Function;
                        if (isLParanth)
                            state = State.S5;
                        break;
                    case State.S5:
                        if (isParanth || isOp)
                            state = State.S1;
                        else if (isDigit)
                            state = State.S2;
                        else if (isLetter)
                            state = State.S4;
                        break;
                    default:
                        break;
                }

                void tokenize_Op_Paranth_Sep()
                {
                    if (isOp)
                    {
                        if (tokens.Count == 0 || tokens[tokens.Count - 1].GetTokenType() == Token.Type.L_Parenthesis)
                            tokens.Add(new Token(s.ToString(), Token.Type.Operator, Token.OperatorAssociativity.Right));
                        else
                            tokens.Add(new Token(s.ToString(), Token.Type.Operator, Token.OperatorAssociativity.Left));
                    }
                    else if (isParanth)
                    {
                        tokens.Add(new Token(s.ToString(), isRParanth ? Token.Type.R_Parenthesis : Token.Type.L_Parenthesis));
                    }
                    else if (isSep)
                    {
                        tokens.Add(new Token(s.ToString(), Token.Type.Separator));
                    }
                }

                switch (state)
                {
                    case State.S1:
                        tokenize_Op_Paranth_Sep();
                        break;
                    case State.S2: case State.S3: case State.S4:

                        if(!string.IsNullOrEmpty(buffer) && bufferTokenType == Token.Type.Function && isDigit == true)
                        {
                            tokens.Add(new Token(buffer, bufferTokenType));
                            buffer = string.Empty;
                            state = State.S2;
                        }

                        buffer += s;
                        break;
                    case State.S5:
                        tokens.Add(new Token(buffer, bufferTokenType));
                        buffer = string.Empty;
                        tokenize_Op_Paranth_Sep();
                        break;
                }
            }
            if (!string.IsNullOrEmpty(buffer))
                tokens.Add(new Token(buffer, bufferTokenType));

            return tokens;
        }
    }
}
