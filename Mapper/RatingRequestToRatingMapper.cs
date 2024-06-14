using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public class RatingRequestToRatingMapper
    {
        public static Rating MapToRating(RatingRequest ratingRequest, Guid rideId, UserType ratedBy)
        {
            return new Rating
            {
                RideId = rideId,
                RatingValue = ratingRequest.Rating,
                RatedBy = ratedBy
            };
        }
    }
}
