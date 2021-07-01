namespace IdentityNumberValidator.Models
{
    public class InvalidIdentityNumber : IdentityNumber
    {
        public string InvalidReason { get; set; }

        public InvalidIdentityNumber(string invalidReason)
        {
            InvalidReason = invalidReason;
        }

        public override ValidationResult Validate()
        {
            return new ValidationResult(false, InvalidReason);
        }
    }
}
