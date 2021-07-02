using System;
using System.Linq;
using IdentityNumberValidator.ExtensionMethods;

namespace IdentityNumberValidator.Models
{
    public abstract class IdentityNumber
    {
        public string NumberData { get { if (_numberData == null) _numberData = CalculateNumerData(); return _numberData; } }
        private string _numberData;
        public string RawNumberData { get { return _rawNumberData; } set { _rawNumberData = value; _checkSum = -1; _numberData = null; } }
        private string _rawNumberData;
        public int CheckSum { get { if (_checkSum == -1) _checkSum = CalculateCheckSum(); return _checkSum; } }
        private int _checkSum = -1;

        public abstract ValidationResult Validate();

        public static IdentityNumber CreateFromString(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
                return new UnrecognizedIdentityNumber("Can not create a valid IdentityNumber from a string that is null or empty");

            string onlyDigits = identityNumber.Replace("-", "").Replace("+", "");

            if (!onlyDigits.All(c => char.IsDigit(c)))
                return new UnrecognizedIdentityNumber(string.Format("An IdentityNumber can only contain \"-\", \"+\" and digits (failed for {0})", identityNumber));

            if (identityNumber.Contains('-') && identityNumber.IndexOf('-') != identityNumber.Length - 5)
                return new UnrecognizedIdentityNumber(string.Format("An IdentityNumber can only contain \"-\" in a position that is immediately before the 4 last digits"));

            if (identityNumber.Contains('+') && identityNumber.IndexOf('+') != identityNumber.Length - 5)
                return new UnrecognizedIdentityNumber(string.Format("An IdentityNumber can only contain \"+\" in a position that is immediately before the 4 last digits"));

            if (onlyDigits.Length != 10 && onlyDigits.Length != 12)
                return new UnrecognizedIdentityNumber(string.Format("An IdentityNumber must have a length of either 12 or 10 (failed for {0})", identityNumber));

            string tenDigits = onlyDigits.Length == 10 ? onlyDigits : onlyDigits.Substring(2, 10);

            if (int.Parse(new string(new char[] { tenDigits[2], tenDigits[3] })) >= 20)
            {
                return new OrganizationNumber()
                {
                    RawNumberData = identityNumber,
                };
            }
            else if (int.Parse(new string(new char[] { tenDigits[4], tenDigits[5] })) > 60)
            {
                return new CoordinationNumber()
                {
                    RawNumberData = identityNumber,
                };
            }
            else
            {
                return new PersonalIdentityNumber()
                {
                    RawNumberData = identityNumber,
                };
            }
        }

        protected string CalculateNumerData()
        {
            string onlyDigits = RawNumberData.Replace("-", "").Replace("+", "");
            return onlyDigits.Length == 10 ? onlyDigits : onlyDigits.Substring(2, 10);
        }

        protected int CalculateCheckSum()
        {
            return NumberData.Substring(NumberData.Length - 10, 10).Substring(0, 9).GetLuhnCheckDigit();
        }
    }
}
