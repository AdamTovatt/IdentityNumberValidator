using System;
using System.Linq;

namespace IdentityNumberValidator.Models
{
    public abstract class IdentityNumber
    {
        public string NumberData { get; private set; }
        public abstract ValidationResult Validate();

        public static IdentityNumber CreateFromString(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
                throw new Exception("Can not create IdentityNumber from a string that is null or empty");

            string onlyDigits = identityNumber.Replace("-", "").Replace("+", "");

            if (!onlyDigits.All(c => char.IsDigit(c)))
                return new InvalidIdentityNumber(string.Format("An IdentityNumber can only contain \"-\", \"+\" and digits (failed for {0})", identityNumber));

            if (onlyDigits.Length != 10 || onlyDigits.Length != 12)
                return new InvalidIdentityNumber(string.Format("An IdentityNumber must have a length of either 12 or 10 (failed for {0})", identityNumber));

            string tenDigits = onlyDigits.Length == 10 ? onlyDigits : onlyDigits.Substring(0, 10);

            if (int.Parse(new string(tenDigits[2], tenDigits[3])) >= 20)
            {
                return new OrganizationNumber()
                {
                    NumberData = tenDigits,
                };
            }
            else if(int.Parse(new string(tenDigits[4], tenDigits[5])) > 60)
            {
                return new CoordinationNumber()
                {
                    NumberData = tenDigits,
                };
            }
            else
            {
                return new PersonalIdentityNumber()
                {
                    ReallyOldPerson = identityNumber.Contains("+") || (onlyDigits.Length == 12 && char.GetNumericValue(tenDigits[1]) < char.GetNumericValue(DateTime.Now.Year.ToString()[1])),
                    NumberData = tenDigits,
                };
            }
        }
    }
}
