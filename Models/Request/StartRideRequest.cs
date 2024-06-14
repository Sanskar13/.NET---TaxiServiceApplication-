using System.ComponentModel.DataAnnotations;

namespace TaxiServiceAPI.Models.Request
{
    public class StartRideRequest
    {
        [Required(ErrorMessage = "RideId is required.")]
        public Guid RideId { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        public string OTP { get; set; }

    }
}
