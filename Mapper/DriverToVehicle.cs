using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public class DriverToVehicle
    {
        public static Vehicle MapToVehicle(Guid newUserId, DriverSignUpRequest driverSignUpRequest)
        {
            var vehicleType = driverSignUpRequest.VehicleType.ToLower() switch
            {
                "bike" => VehicleType.Bike,
                "car" => VehicleType.Car,
                "auto" => VehicleType.Auto,
            };

            return new Vehicle
            {
                Id = Guid.NewGuid(),
                VehiclePlateNumber = driverSignUpRequest.VehiclePlateNumber,
                VehicleType = vehicleType,
                IsAvailable = true,
                UserId = newUserId
            };
        }
    }
}
