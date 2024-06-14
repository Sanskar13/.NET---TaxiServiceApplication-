using System.ComponentModel.DataAnnotations;
using System.IO;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Validation;

namespace TaxiServiceAPI.Models.Request
{
    public class RideRequest
    {
        [Required(ErrorMessage = "Pickup location is required")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Drop location is required")]
        public string DropLocation { get; set; }

        [Required(ErrorMessage = "Type of ride is required")]
        [VehicleTypeValidation]
        public string VehicleType { get; set; }
    }
}
