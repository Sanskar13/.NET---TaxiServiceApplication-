using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Entities
{
    public class Rating
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        public Guid RideId { get; set; }

        [ForeignKey("RideId")]
        public Ride Ride { get; set; }

        [Required]
        [Range(1, 5)]
        public int RatingValue { get; set; } 

        [Required]
        public UserType RatedBy { get; set; }
    }
}
