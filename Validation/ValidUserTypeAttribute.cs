using System;
using System.ComponentModel.DataAnnotations;

namespace TaxiServiceAPI.Validation
{
    public class ValidUserTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string userTypeString)
            {
                string normalizedUserType = userTypeString.ToLower();
                if (normalizedUserType == "rider" || normalizedUserType == "driver")
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Invalid user type. Allowed values are 'Rider' or 'Driver'.");
        }
    }
}
