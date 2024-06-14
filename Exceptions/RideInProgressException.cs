namespace TaxiServiceAPI.Exceptions
{
    public class RideInProgressException: Exception
    {
        public RideInProgressException(string message) : base(message) { }
    }
}
