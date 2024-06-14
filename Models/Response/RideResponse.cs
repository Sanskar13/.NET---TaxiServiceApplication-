using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Models.Response
{
    public class RideResponse
    {
        public Guid RideId { get; set; }
        public string DriverName { get; set; }
        public string DriverEmail { get; set; }
        public string DriverPhone { get; set; }
        public string OTP { get; set; }
        public RideStatus Status { get; set; }
    }
}
