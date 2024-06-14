using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public static class RiderSignUpRequestToRider
    {
        public static User MapToRider(RiderSignUpRequest riderSignUpRequest)
        {
            return new User
            {
                Name = riderSignUpRequest.Name,
                Email = riderSignUpRequest.Email,
                Phone = riderSignUpRequest.Phone,
                Password = riderSignUpRequest.Password,
                UserType = UserType.Rider
            };
        }
    }
}
