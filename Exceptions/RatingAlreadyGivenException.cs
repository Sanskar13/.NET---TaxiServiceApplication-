namespace TaxiServiceAPI.Exceptions
{
    public class RatingAlreadyGivenException: Exception
    {
        public RatingAlreadyGivenException(string message) : base(message) { }
    }
}
