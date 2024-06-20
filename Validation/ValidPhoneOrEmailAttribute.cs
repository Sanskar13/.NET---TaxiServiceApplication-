using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TaxiServiceAPI.Validation
{
    public class ValidPhoneOrEmailAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string phoneOrEmail = value.ToString();

                if (IsValidEmail(phoneOrEmail))
                {
                    return ValidationResult.Success;
                }

                if (IsValidPhoneNumber(phoneOrEmail))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Invalid format. Please enter a valid phone number or email address.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?(\d[\d\-\s]+)$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
