using System.Collections.Generic;
using System.Linq;

namespace IdentityNumberValidator.ExtensionMethods
{
    public static class StringExtensions
    {
        public static int GetLuhnCheckDigit(this string numberData)
        {
            List<char> numericChars = numberData.Where(c => char.IsNumber(c)).ToList();

            int sum = 0;
            for (int i = 0; i < numericChars.Count; i++)
            {
                sum += ((int)char.GetNumericValue(numericChars[i]) * (2 - (i % 2))).DigitSum();
            }

            return (10 - (sum % 10)) % 10;
        }

        public static int GetCentury(this string year)
        {
            return int.Parse(year.Substring(0, 2));
        }
    }
}
