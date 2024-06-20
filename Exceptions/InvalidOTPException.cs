namespace TaxiServiceAPI.Exceptions
{
    public class InvalidOTPException: Exception
    {
        public InvalidOTPException(string message) : base(message) { }
    }
}
