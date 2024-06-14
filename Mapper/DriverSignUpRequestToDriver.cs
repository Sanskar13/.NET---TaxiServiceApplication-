using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public static class DriverSignUpRequestToDriver
    {
        public static User MapToDriver(DriverSignUpRequest driverSignUpRequest)
        {
            return new User
            {
                Name = driverSignUpRequest.Name,
                Email = driverSignUpRequest.Email,
                Phone = driverSignUpRequest.Phone,
                Password = driverSignUpRequest.Password,
                UserType = UserType.Driver
            };
        }

    }
}
