using System.Text.RegularExpressions;

namespace Byndyusoft_test_calculator
{
    internal class CheckString
    {
        /// <summary>
        /// Проверка входной строки на наличие некорректных символов
        /// </summary>
        /// <param name="value">Входная строка?которую необходимо проверить<param>
        public bool Check(string value)
        {
            if (IsValid(value) == false)
                return false;

            //Соответствие любому символу, которого нет в данной символьной группе
            string pattern = @"[^\d(*/+-., )]";
            Regex regex = new Regex(pattern);
            var matches = regex.Matches(value);
            if (matches.Count != 0)
                return false;

            return true;
        }

        /// <summary>
        /// Проверка входной строки на закрытие всех скобок
        /// </summary>
        /// <param name="value">Входная строка</param>
        /// <returns>Возврат true, если все скобки закрыты</returns>
        private bool IsValid(string value)
        {
            var count = 0;
            foreach (var c  in value)
            {
                if (c == '(')
                    count++;

                if (c == ')')
                {
                    if (count == 0) 
                        return false;

                    count--;
                }
            }

            return count == 0;
        }
    }
}
    