using System.Security.Claims;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Interfaces.IService
{
    /// <summary>
    /// Interface for rider service.
    /// </summary>
    public interface IRiderService
    {
        RideResponse RequestRide(RideRequest rideRequest, string riderEmail);
        RiderRideResponse GetCurrentRide(string riderEmail);
        void RateDriver(RatingRequest ratingRequest, string riderEmail);
    }
}
