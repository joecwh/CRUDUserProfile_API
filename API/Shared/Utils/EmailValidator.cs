using System.Text.RegularExpressions;

namespace API.Shared.Utils
{
    public static class EmailValidator
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && EmailRegex.IsMatch(email);
        }
    }
}
