using System.ComponentModel.DataAnnotations;

namespace TaxiServiceAPI.Validation
{
    public class VehicleTypeValidationAttribute: ValidationAttribute
    {
        private readonly string[] _allowedValues = { "Bike", "Car", "Auto" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue && _allowedValues.Contains(stringValue, StringComparer.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid vehicle type. Allowed values are: {string.Join(", ", _allowedValues)}");
        }
    }
}
