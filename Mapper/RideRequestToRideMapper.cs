using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public static class RideRequestToRideMapper
    {
        public static Ride MapToRide(RideRequest rideRequest, Guid riderId, Guid driverId, string otp, VehicleType vehicleType)
        {
            return new Ride
            {
                PickupLocation = rideRequest.PickupLocation,
                DropLocation = rideRequest.DropLocation,
                TypeOfRide = vehicleType,
                RiderId = riderId,
                DriverId = driverId,
                Status = RideStatus.Pending,
                OTP = otp
            };
        }
    }
}
