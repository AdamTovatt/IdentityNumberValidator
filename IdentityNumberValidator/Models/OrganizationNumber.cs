using System.Linq;

namespace IdentityNumberValidator.Models
{
    public class OrganizationNumber : IdentityNumber
    {
        public override ValidationResult Validate()
        {
            if (RawNumberData.Contains('+'))
                return new ValidationResult(false, string.Format("OrganizationNumbers can not contain \"+\" for number: {0}", RawNumberData));

            if (CheckSum != char.GetNumericValue(NumberData.Last()))
                return new ValidationResult(false, string.Format("Invalid checksum: {0} for number: {1}", CheckSum, RawNumberData));

            return new ValidationResult(true);
        }
    }
}
