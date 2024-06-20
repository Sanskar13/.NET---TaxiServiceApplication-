namespace TaxiServiceAPI.Exceptions
{
    public class RideNotFoundException: Exception
    {
        public RideNotFoundException(string message) : base(message) { }
    }
}
