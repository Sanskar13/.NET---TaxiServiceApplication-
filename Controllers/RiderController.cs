
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Models.Response;
using TaxiServiceAPI.Utils;

namespace TaxiServiceAPI.Controllers
{
    [ApiController]
    [Route(MethodNames.RouteTemplate)]
    [Authorize(Roles = MethodNames.RiderRole)]
    public class RiderController: ControllerBase
    {
        private readonly IRiderService _riderService;

        public RiderController(IRiderService rideService)
        {
            _riderService = rideService;
        }

        /// <summary>
        /// Requests a ride.
        /// </summary>
        /// <param name="rideRequest">The request data for the ride.</param>
        /// <returns>The ride response.</returns>
        [HttpPost(MethodNames.RequestRide)]
        public IActionResult RequestRide([FromBody] RideRequest rideRequest)
        {
            try
            {
                string? riderEmail = JwtHelper.GetEmailFromClaims(User.Claims);
                if (riderEmail == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                RideResponse rideRequestStatus = _riderService.RequestRide(rideRequest, riderEmail);
                return Ok(rideRequestStatus);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Gets the current ride details for the rider.
        /// </summary>
        /// <returns>The rider's current ride response.</returns>
        [HttpGet(MethodNames.RiderCurrentRide)]
        public IActionResult GetCurrentRide()
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                RiderRideResponse rideResponse = _riderService.GetCurrentRide(email);
                return Ok(rideResponse);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Rates the driver after completing the ride.
        /// </summary>
        /// <param name="ratingRequest">The rating details for the driver.</param>
        /// <returns>A message indicating the success of the rating submission.</returns>
        [HttpPost(MethodNames.RateDriver)]
        public IActionResult RateDriver([FromBody] RatingRequest ratingRequest)
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                _riderService.RateDriver(ratingRequest, email);
                return Ok(Messages.RatingSubmittedSuccessfully);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
    }
}
