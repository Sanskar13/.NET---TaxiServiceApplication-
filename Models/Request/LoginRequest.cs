using System.ComponentModel.DataAnnotations;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Validation;

namespace TaxiServiceAPI.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Phone Number or Email is required")]
        [ValidPhoneOrEmail(ErrorMessage = "Invalid format. Please enter a valid phone number or email address.")]
        public string PhoneOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Type is required")]
        [ValidUserType]
        public string UserType { get; set; }
    }
}
