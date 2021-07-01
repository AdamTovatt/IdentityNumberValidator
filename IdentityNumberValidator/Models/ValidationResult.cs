namespace IdentityNumberValidator.Models
{
    public class ValidationResult
    {
        public bool Valid { get; set; }
        public string InvalidReason { get; set; }

        public ValidationResult(bool valid)
        {
            Valid = valid;
        }

        public ValidationResult(bool valid, string invalidReason)
        {
            Valid = valid;
            InvalidReason = invalidReason;
        }
    }
}
