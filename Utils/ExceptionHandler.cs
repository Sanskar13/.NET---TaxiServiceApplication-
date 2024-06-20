using Microsoft.AspNetCore.Mvc;
using TaxiServiceAPI.Exceptions;

namespace TaxiServiceAPI.Utils
{
    public static class ExceptionHandler
    {
        /// <summary>
        /// Handles the given exception and returns an appropriate IActionResult.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>An IActionResult representing the appropriate HTTP response.</returns>
        public static IActionResult HandleException(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                return new UnauthorizedResult();
            }
            else if (ex is RideNotFoundException)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            else if (ex is UserNotFoundException)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            else if (ex is RideInProgressException)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            else if (ex is VehicleNotFoundException)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            else if (ex is RatingAlreadyGivenException)
            {
                return new ConflictObjectResult(ex.Message);
            }
            else
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
