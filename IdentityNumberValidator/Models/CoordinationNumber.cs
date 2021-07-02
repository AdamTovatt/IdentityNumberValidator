using IdentityNumberValidator.ExtensionMethods;
using System;
using System.Globalization;
using System.Linq;

namespace IdentityNumberValidator.Models
{
    public class CoordinationNumber : IdentityNumber
    {
        public override ValidationResult Validate()
        {
            if (CheckSum != char.GetNumericValue(NumberData.Last()))
                return new ValidationResult(false, string.Format("Invalid checksum: {0} for number: {1}", CheckSum, RawNumberData));

            string fullDate = null;
            if (RawNumberData.Length == 12)
            {
                fullDate = string.Format("{0}{1}", RawNumberData.Substring(0, 6), int.Parse(RawNumberData.Substring(6, 2)) - 60);
            }
            else
            {
                fullDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString().GetCentury() - (RawNumberData.Contains('+') ? 1 : 0), RawNumberData.Substring(0, 4), int.Parse(RawNumberData.Substring(4, 2)) - 60);
            }

            if (!DateTime.TryParseExact(fullDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                return new ValidationResult(false, string.Format("Invalid date: {0} for number: {1}", fullDate, RawNumberData));

            return new ValidationResult(true);
        }
    }
}
