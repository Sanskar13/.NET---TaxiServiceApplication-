using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Interfaces.IService
{
    /// <summary>
    /// Interface for authentication service.
    /// </summary>
    public interface IAuthService
    {
        string SignUpRider(RiderSignUpRequest riderSignUpRequest);
        string SignUpDriver(DriverSignUpRequest driverSignUpRequest);
        string Login(LoginRequest loginRequest);
    }
}
