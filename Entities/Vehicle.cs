using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaxiServiceAPI.Enums;

namespace TaxiServiceAPI.Entities
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(20)]
        public string VehiclePlateNumber { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public bool IsAvailable { get; set; } 

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
