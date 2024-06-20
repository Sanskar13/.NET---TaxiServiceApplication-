using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Mapper
{
    public class RatingMapper
    {
        public static Rating MapToRating(RatingRequest ratingRequest, UserType ratedBy)
        {
            if (ratingRequest == null)
            {
                return null;
            }

            return new Rating
            {
                RideId = ratingRequest.RideId,
                RatingValue = ratingRequest.Rating,
                RatedBy = ratedBy,
            };
        }
    }
}
