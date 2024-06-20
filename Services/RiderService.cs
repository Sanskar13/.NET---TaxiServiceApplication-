using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Exceptions;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Mapper;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Models.Response;
using TaxiServiceAPI.Utils;

namespace TaxiServiceAPI.Services
{
    public class RiderService : IRiderService
    {
        private readonly MySQLDBContext _context;

        public RiderService(MySQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Requests a ride for the rider.
        /// </summary>
        /// <param name="rideRequest">The request containing ride details.</param>
        /// <param name="riderEmail">The email address of the rider.</param>
        /// <returns>The response containing ride details and OTP.</returns>
        /// <exception cref="RideNotFoundException">Thrown if the rider already has an ongoing ride.</exception>
        /// <exception cref="UserNotFoundException">Thrown if no available driver is found.</exception>
        public RideResponse RequestRide(RideRequest rideRequest, string riderEmail)
        {
            User riderDetail = ValidateRider(riderEmail);
  
            Ride? rideDetail = _context.Rides.FirstOrDefault(r => r.RiderId == riderDetail.Id && (r.Status == RideStatus.Pending || r.Status == RideStatus.Ongoing));
            if(rideDetail != null)
            {
                throw new RideNotFoundException(Messages.RideAlreadyInProgress);
            }

            User? riderSameAsDriver = _context.Users.FirstOrDefault(d => d.Email == riderEmail && d.UserType == UserType.Driver);

            VehicleType vehicleType = VehicleTypeMapper.MapToVehicleType(rideRequest.VehicleType);

            Vehicle? driverAvailable = _context.Vehicles.FirstOrDefault(d => d.IsAvailable
                && d.VehicleType == vehicleType
                && (riderSameAsDriver == null || d.UserId == riderSameAsDriver.Id));

            if (driverAvailable == null)
            {
                throw new UserNotFoundException(Messages.NoAvailableDriver);
            }

            string otp = GenerateOTP.RandomOTP();

            Ride ride = RideRequestToRideMapper.MapToRide(rideRequest, riderDetail.Id, driverAvailable.UserId, otp, vehicleType);

            _context.Rides.Add(ride);
            _context.SaveChanges();

            User driverDetail = _context.Users.FirstOrDefault(d => d.Id == driverAvailable.UserId);

            driverAvailable.IsAvailable = !driverAvailable.IsAvailable;
            _context.SaveChanges();

            return UserToRideResponseMapper.MapToRideResponse(ride, driverDetail, otp);
        }

        /// <summary>
        /// Retrieves details of the current ride for the rider.
        /// </summary>
        /// <param name="riderEmail">The email address of the rider.</param>
        /// <returns>The response containing details of the current ride.</returns>
        /// <exception cref="RideNotFoundException">Thrown if the rider has no ongoing ride.</exception>
        /// <exception cref="UserNotFoundException">Thrown if the details of the driver are not found.</exception>
        public RiderRideResponse GetCurrentRide(string riderEmail)
        {
            User rider = ValidateRider(riderEmail);

            Ride? currentRide = _context.Rides.FirstOrDefault(r => r.RiderId == rider.Id && (r.Status == RideStatus.Pending || r.Status == RideStatus.Ongoing));
            if (currentRide == null)
            {
                throw new RideNotFoundException(Messages.NoOngoingRide);
            }

            User? driver = _context.Users.FirstOrDefault(u => u.Id == currentRide.DriverId);
            if (driver == null)
            {
                throw new UserNotFoundException(Messages.DriverDetailsNotFound);
            }

            return RideToRiderRideResponseMapper.MapToRiderRideResponse(currentRide, driver);
        }

        /// <summary>
        /// Rates the driver after completing a ride.
        /// </summary>
        /// <param name="ratingRequest">The request containing the ride ID and rating.</param>
        /// <param name="riderEmail">The email address of the rider.</param>
        /// <exception cref="RatingAlreadyGivenException">Thrown if the rider has already rated the ride.</exception>
        /// <exception cref="RideNotFoundException">Thrown if the ride is not found or not associated with the rider.</exception>
        /// <exception cref="RideInProgressException">Thrown if the ride is not completed.</exception>
        public void RateDriver(RatingRequest ratingRequest, string riderEmail)
        {
            Rating? rideAlreadyRated = _context.Ratings.FirstOrDefault(r => r.RideId == ratingRequest.RideId && r.RatedBy == UserType.Rider);
            if(rideAlreadyRated != null)
            {
                throw new RatingAlreadyGivenException(Messages.RatingAlreadyGiven);
            }

            User rider = ValidateRider(riderEmail);

            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id == ratingRequest.RideId && r.RiderId == rider.Id);
            if (ride == null)
            {
                throw new RideNotFoundException(Messages.RideNotFoundOrNotBelongsToRider);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new RideInProgressException(Messages.RideNotCompleted);
            }

            Rating rating = RatingRequestToRatingMapper.MapToRating(ratingRequest, ride.Id, UserType.Rider);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        private User ValidateRider(string riderEmail)
        {
            User? riderDetail = _context.Users.FirstOrDefault(r => r.Email == riderEmail && r.UserType == UserType.Rider);
            if (riderDetail == null)
            {
                throw new UserNotFoundException(Messages.RiderNotFound);
            }
            return riderDetail;
        }
    }
}
