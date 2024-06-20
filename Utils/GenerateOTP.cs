using System.Security.Cryptography;

namespace TaxiServiceAPI.Utils
{
    public static class GenerateOTP
    {
        /// <summary>
        /// Generates a random six-digit OTP.
        /// </summary>
        /// <returns>A string representing the generated OTP.</returns>
        public static string RandomOTP()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var otpBytes = new byte[3];
                rng.GetBytes(otpBytes);
                return string.Concat(otpBytes.Select(b => b.ToString("X2"))).Substring(0, 6);
            }
        }
    }
}
