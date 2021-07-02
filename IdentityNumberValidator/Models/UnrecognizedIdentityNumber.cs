namespace IdentityNumberValidator.Models
{
    public class UnrecognizedIdentityNumber : IdentityNumber
    {
        public string UnrecognizedReason { get; set; }

        public UnrecognizedIdentityNumber(string unrecognizedReason)
        {
            UnrecognizedReason = unrecognizedReason;
        }

        public override ValidationResult Validate()
        {
            return new ValidationResult(false, UnrecognizedReason);
        }
    }
}
