using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Entities
{
    public class Ride
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
          
        [Required]
        [MaxLength(200)]
        public string PickupLocation { get; set; }

        [Required]
        [MaxLength(200)]
        public string DropLocation { get; set; }

        [Required]
        public VehicleType TypeOfRide { get; set; } 

        [Required]
        public Guid RiderId { get; set; }

        [ForeignKey("RiderId")]
        public User Rider { get; set; }

        [Required]
        public Guid DriverId { get; set; }

        [ForeignKey("DriverId")]
        public User Driver { get; set; }

        [Required]
        public RideStatus Status { get; set; } = RideStatus.Pending;

        [MaxLength(6)]
        public string OTP { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? StartedAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
