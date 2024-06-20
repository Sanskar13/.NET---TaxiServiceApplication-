using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Mapper
{
    public static class UserToRideResponseMapper
    {
        public static RideResponse MapToRideResponse(Ride ride, User driverDetail, string otp)
        {
            return new RideResponse
            {
                RideId = ride.Id,
                DriverName = driverDetail.Name,
                DriverEmail = driverDetail.Email,
                DriverPhone = driverDetail.Phone,
                OTP = otp,
                Status = ride.Status
            };
        }
    }
}
