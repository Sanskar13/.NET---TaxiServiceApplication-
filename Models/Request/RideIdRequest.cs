using System.ComponentModel.DataAnnotations;

namespace TaxiServiceAPI.Models.Request
{
    public class RideIdRequest
    {
        [Required(ErrorMessage = "RideId is required.")]
        public Guid RideId { get; set; }

    }
}
