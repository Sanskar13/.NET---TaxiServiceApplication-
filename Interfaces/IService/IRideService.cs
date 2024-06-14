using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Interfaces.IService
{
    /// <summary>
    /// Interface for ride service.
    /// </summary>
    public interface IRideService
    {
        void CancelRide(RideIdRequest cancelRideRequest, string userEmail);
        void EndRide(RideIdRequest endRideRequest, string userEmail);
    }
}
