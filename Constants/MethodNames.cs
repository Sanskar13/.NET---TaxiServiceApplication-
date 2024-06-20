using TaxiServiceAPI.Controllers;

namespace TaxiServiceAPI.Constants
{
    public static class MethodNames
    {
        public const string RouteTemplate = "api/[controller]";

        // AuthController routes
        public const string RiderSignUp = "ridersignup";
        public const string DriverSignUp = "driversignup";
        public const string LoginUser = "loginUser";

        // DriverController routes
        public const string ToggleAvailability = "toggle-availability";
        public const string CurrentRide = "current-ride";
        public const string StartRide = "start-ride";
        public const string RateRider = "rate-rider";

        // RideController routes
        public const string CancelRide = "cancel-ride";
        public const string EndRide = "end-ride";

        // RiderController routes
        public const string RequestRide = "request-ride";
        public const string RiderCurrentRide = "current-ride";
        public const string RateDriver = "rate-driver";

        // Role constants
        public const string RiderRole = "Rider";
        public const string DriverRole = "Driver";

    }
}
