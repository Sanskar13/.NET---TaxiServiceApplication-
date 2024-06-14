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
    [Authorize(Roles = MethodNames.DriverRole)]
    public class DriverController: ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Toggles the availability status of the driver.
        /// </summary>
        /// <returns>The result of the toggle operation.</returns>
        [HttpPut(MethodNames.ToggleAvailability)]
        public IActionResult ToggleAvailability()
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                string result = _driverService.ToggleAvailability(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Gets the current ride of the driver.
        /// </summary>
        /// <returns>The current ride information.</returns>
        [HttpGet(MethodNames.CurrentRide)]
        public IActionResult GetCurrentRide()
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                DriverRideResponse rideResponse = _driverService.GetCurrentRide(email);
                return Ok(rideResponse);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Starts the ongoing ride for the driver.
        /// </summary>
        /// <param name="startRideRequest">The request data for starting the ride.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost(MethodNames.StartRide)]
        public IActionResult StartRide([FromBody] StartRideRequest startRideRequest)
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                _driverService.StartRide(startRideRequest, email);
                return Ok(Messages.RideStartedSuccessfully);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Rates the rider after completing the ride.
        /// </summary>
        /// <param name="ratingRequest">The rating request data.</param>
        /// <returns>A message indicating the success of the operation.</returns>
        [HttpPost(MethodNames.RateRider)]
        public IActionResult RateRider([FromBody] RatingRequest ratingRequest)
        {
            try
            {
                string? email = JwtHelper.GetEmailFromClaims(User.Claims);
                if (email == null)
                {
                    return Unauthorized(Messages.EmailClaimNotFound);
                }

                _driverService.RateRider(ratingRequest, email);
                return Ok(Messages.RatingSubmittedSuccessfully);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
