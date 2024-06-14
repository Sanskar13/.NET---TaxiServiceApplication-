using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Interfaces.IService
{
    /// <summary>
    /// Interface for driver service.
    /// </summary>
    public interface IDriverService
    {
        string ToggleAvailability(string email);
        DriverRideResponse GetCurrentRide(string driverEmail);
        void StartRide(StartRideRequest startRideRequest, string driverEmail);
        void RateRider(RatingRequest ratingRequest, string driverEmail);
    }
}
