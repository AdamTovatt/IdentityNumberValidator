namespace IdentityNumberValidator.ExtensionMethods
{
    public static class IntExtensions
    {
        public static int DigitSum(this int value)
        {
            int result = 0;

            foreach (char c in value.ToString())
                result += (int)char.GetNumericValue(c);

            return result;
        }
    }
}
