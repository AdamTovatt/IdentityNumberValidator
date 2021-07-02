using IdentityNumberValidator.ExtensionMethods;
using System;
using System.Globalization;
using System.Linq;

namespace IdentityNumberValidator.Models
{
    public class PersonalIdentityNumber : IdentityNumber
    {
        public override ValidationResult Validate()
        {
            if (CheckSum != char.GetNumericValue(NumberData.Last()))
                return new ValidationResult(false, string.Format("Invalid checksum: {0} for number: {1}", CheckSum, RawNumberData));

            string fullDate = RawNumberData.Length == 12 ? RawNumberData.Substring(0, 8) : string.Format("{0}{1}", DateTime.Now.Year.ToString().GetCentury() - (RawNumberData.Contains('+') ? 1 : 0), RawNumberData.Substring(0, 6));

            if (!DateTime.TryParseExact(fullDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                return new ValidationResult(false, string.Format("Invalid date: {0} for number: {1}", fullDate, RawNumberData));

            return new ValidationResult(true);
        }
    }
}
