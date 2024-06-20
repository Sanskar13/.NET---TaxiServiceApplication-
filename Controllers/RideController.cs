using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Utils;

namespace TaxiServiceAPI.Controllers
{
    [ApiController]
    [Route(MethodNames.RouteTemplate)]
    [Authorize(Roles = MethodNames.RiderRole + "," + MethodNames.DriverRole)]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
            _rideService = rideService;
        }

        /// <summary>
        /// Cancels a ride.
        /// </summary>
        /// <param name="cancelRideRequest">The request data for canceling the ride.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost(MethodNames.CancelRide)]
        public IActionResult CancelRide([FromBody] RideIdRequest cancelRideRequest)
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                _rideService.CancelRide(cancelRideRequest, email);
                return Ok(Messages.RideCanceledSuccessfully);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Ends an ongoing ride.
        /// </summary>
        /// <param name="endRideRequest">The request data for ending the ride.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost(MethodNames.EndRide)]
        public IActionResult EndRide([FromBody] RideIdRequest endRideRequest)
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                _rideService.EndRide(endRideRequest, email);
                return Ok(Messages.RideEndedSuccessfully);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }
    }
}
