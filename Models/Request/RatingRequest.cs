using System.ComponentModel.DataAnnotations;

namespace TaxiServiceAPI.Models.Request
{
    public class RatingRequest
    {
        [Required]
        public Guid RideId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

    }
}
