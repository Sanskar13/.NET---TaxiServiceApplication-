using System.ComponentModel.DataAnnotations;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Validation;

namespace TaxiServiceAPI.Models.Request
{
    public class DriverSignUpRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vehicle Plate Number is required")]
        public string VehiclePlateNumber { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        [VehicleTypeValidation]
        public string VehicleType { get; set; }
    }
}
