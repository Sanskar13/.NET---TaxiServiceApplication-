using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Utils
{
    public static class UserTypeConverter
    {
        public static UserType GetUserType(string userTypeString)
        {
            switch (userTypeString.ToLower())
            {
                case "rider":
                    return UserType.Rider;
                case "driver":
                    return UserType.Driver;
                default:
                    throw new ArgumentException("Invalid user type.", nameof(userTypeString));
            }
        }
    }
}
