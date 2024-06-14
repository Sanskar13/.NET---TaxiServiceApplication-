namespace TaxiServiceAPI.Models.Response
{
    public class DriverRideResponse
    {
        public Guid RideId { get; set; }
        public string RiderName { get; set; }
        public string RiderEmail { get; set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public string Status { get; set; }
    }
}
