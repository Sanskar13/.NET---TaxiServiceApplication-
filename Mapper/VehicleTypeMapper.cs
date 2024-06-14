using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Mapper
{
    public static class VehicleTypeMapper
    {
        public static VehicleType MapToVehicleType(string vehicleType)
        {
            return vehicleType.ToLower() switch
            {
                "bike" => VehicleType.Bike,
                "car" => VehicleType.Car,
                "auto" => VehicleType.Auto,
                _ => throw new Exception("Invalid vehicle type")
            };
        }
    }
}
