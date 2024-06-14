using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Controllers
{
    [ApiController]
    [Route(MethodNames.RouteTemplate)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Handles rider sign-up request.
        /// </summary>
        /// <param name="riderSignUpRequest">The sign-up request data for a rider.</param>
        /// <returns>The result of the sign-up operation.</returns>
        [HttpPost(MethodNames.RiderSignUp)]
        public IActionResult RiderSignUp([FromBody] RiderSignUpRequest riderSignUpRequest)
        {
            string result = _authService.SignUpRider(riderSignUpRequest);
            return Ok(result);
        }

        /// <summary>
        /// Handles driver sign-up request.
        /// </summary>
        /// <param name="driverSignUpRequest">The sign-up request data for a driver.</param>
        /// <returns>The result of the sign-up operation.</returns>
        [HttpPost(MethodNames.DriverSignUp)]
        public IActionResult DriverSignUp([FromBody] DriverSignUpRequest driverSignUpRequest)
        {
            string result = _authService.SignUpDriver(driverSignUpRequest);
            return Ok(result);
        }

        /// <summary>
        /// Handles user login request.
        /// </summary>
        /// <param name="loginRequest">The login request data.</param>
        /// <returns>The authentication token.</returns>
        [HttpPost(MethodNames.LoginUser)]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            string token = _authService.Login(loginRequest);
            return Ok(token);
        }

    }
}
