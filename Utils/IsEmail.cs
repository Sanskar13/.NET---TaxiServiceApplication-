namespace TaxiServiceAPI.Utils
{
    public static class IsEmail
    {
        /// <summary>
        /// Checks if the input string contains the "@" symbol, indicating it might be an email address.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        /// <returns>True if the input contains the "@" symbol, otherwise false.</returns>
        public static bool CheckEmail(string input)
        {
            return input.Contains("@");
        }
    }
}
