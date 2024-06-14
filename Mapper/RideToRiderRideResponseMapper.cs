using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Mapper
{
    public class RideToRiderRideResponseMapper
    {
        public static RiderRideResponse MapToRiderRideResponse(Ride currentRide, User driver)
        {
            return new RiderRideResponse
            {
                RideId = currentRide.Id,
                DriverName = driver.Name,
                DriverEmail = driver.Email,
                DriverPhone = driver.Phone,
                TypeOfRide = currentRide.TypeOfRide.ToString(),
                OTP = currentRide.Status == RideStatus.Pending || currentRide.Status == RideStatus.Ongoing ? currentRide.OTP : null,
                Status = currentRide.Status.ToString()
            };
        }
    }
}
