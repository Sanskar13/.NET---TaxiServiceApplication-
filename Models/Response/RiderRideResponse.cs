namespace TaxiServiceAPI.Models.Response
{
    public class RiderRideResponse
    {
        public Guid RideId { get; set; }
        public string DriverName { get; set; }
        public string DriverEmail { get; set; }
        public string DriverPhone { get; set; }
        public string TypeOfRide { get; set; }
        public string OTP { get; set; }
        public string Status { get; set; }

    }
}
