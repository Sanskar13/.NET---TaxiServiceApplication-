using System.Security.Claims;
using TaxiServiceAPI.Entities;

namespace TaxiServiceAPI.Utils
{
    public static class JwtHelper
    {
        /// <summary>
        /// Retrieves the email claim from a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <returns>The value of the email claim, or null if not found.</returns>
        public static string? GetEmailFromClaims(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        /// <summary>
        /// Retrieves the role claim from a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <returns>The value of the role claim, or null if not found.</returns>
        public static string? GetRoleFromClaims(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }
    }
}
