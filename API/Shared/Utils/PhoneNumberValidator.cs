using System.Text.RegularExpressions;

namespace API.Shared.Utils
{
    public static class PhoneNumberValidator
    {
        // only accept for 01x-xxx xxxx or 01x-xxxx xxxx
        private static readonly Regex PhoneNumberRegex = new Regex(@"^(01[0-9]-[0-9]{3} [0-9]{4}|01[0-9] [0-9]{3} [0-9]{4})$", RegexOptions.Compiled);

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) && PhoneNumberRegex.IsMatch(phoneNumber);
        }
    }
}
