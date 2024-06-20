using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Mapper
{
    public static class DriverRideMapper
    {
        public static DriverRideResponse MapToDriverRideResponse(Ride ride, User rider)
        {
            if (ride == null || rider == null)
            {
                return null;
            }

            return new DriverRideResponse
            {
                RideId = ride.Id,
                RiderName = rider.Name,
                RiderEmail = rider.Email,
                PickupLocation = ride.PickupLocation,
                DropLocation = ride.DropLocation,
                Status = ride.Status.ToString()
            };
        }
    }
}
