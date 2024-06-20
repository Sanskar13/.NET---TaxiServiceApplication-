using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Exceptions;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Mapper;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Models.Response;

namespace TaxiServiceAPI.Services
{
    public class DriverService : IDriverService
    {
        private readonly MySQLDBContext _context;

        public DriverService(MySQLDBContext context)
        {
            _context = context;
        }

        private User GetDriverByEmail(string driverEmail)
        {
            User? driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail && u.UserType == UserType.Driver);
            if (driver == null)
            {
                throw new UserNotFoundException(Messages.DriverNotFound);
            }
            return driver;
        }

        private Ride? GetPendingOrOngoingRide(Guid driverId)
        {
            return _context.Rides.FirstOrDefault(r => r.DriverId == driverId && (r.Status == RideStatus.Pending || r.Status == RideStatus.Ongoing));
        }

        /// <summary>
        /// Toggles the availability status of the driver.
        /// </summary>
        /// <param name="email">The email address of the driver.</param>
        /// <returns>A message indicating the result of toggling the availability.</returns>
        /// <exception cref="VehicleNotFoundException">Thrown if the vehicle associated with the driver is not found.</exception>
        /// <exception cref="RideInProgressException">Thrown if the driver has pending or ongoing rides.</exception>
        public string ToggleAvailability(string email)
        {
            User driver = GetDriverByEmail(email);

            Guid driverId = driver.Id;
            Vehicle? vehicleDetail = _context.Vehicles.FirstOrDefault(v => v.UserId == driverId);
            if (vehicleDetail == null)
            {
                throw new VehicleNotFoundException(Messages.VehicleNotFoundForDriver);
            }

            Ride? currentRide = GetPendingOrOngoingRide(driverId);
            if (currentRide != null)
            {
                throw new RideInProgressException(Messages.CannotToggleAvailabilityWithPendingOrOngoingRides);
            }

            vehicleDetail.IsAvailable = !vehicleDetail.IsAvailable;
            _context.SaveChanges();

            return vehicleDetail.IsAvailable ? Messages.DriverNowAvailable : Messages.DriverNowUnavailable;
        }

        /// <summary>
        /// Retrieves the details of the current ride assigned to the driver.
        /// </summary>
        /// <param name="driverEmail">The email address of the driver.</param>
        /// <returns>The details of the current ride, if available; otherwise, a message indicating no ongoing ride.</returns>
        /// <exception cref="UserNotFoundException">Thrown if the driver details are not found.</exception>
        public DriverRideResponse GetCurrentRide(string driverEmail)
        {
            User driver = GetDriverByEmail(driverEmail);

            Ride? currentRide = GetPendingOrOngoingRide(driver.Id);
            if (currentRide == null)
            {
                return new DriverRideResponse
                {
                    Status = Messages.NoOngoingRideAvailable
                };
            }

            User? rider = _context.Users.FirstOrDefault(u => u.Id == currentRide.RiderId);
            if (rider == null)
            {
                throw new UserNotFoundException(Messages.RiderDetailsNotFound);
            }

            return DriverRideMapper.MapToDriverRideResponse(currentRide, rider);
        }

        /// <summary>
        /// Marks the start of the specified ride by the driver.
        /// </summary>
        /// <param name="startRideRequest">The request containing the ride ID and OTP.</param>
        /// <param name="driverEmail">The email address of the driver.</param>
        /// <exception cref="RideNotFoundException">Thrown if the ride is not found or not assigned to the driver.</exception>
        /// <exception cref="InvalidOTPException">Thrown if the OTP provided is invalid.</exception>
        public void StartRide(StartRideRequest startRideRequest, string driverEmail)
        {
            User driver = GetDriverByEmail(driverEmail);

            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id == startRideRequest.RideId && r.DriverId == driver.Id && r.Status == RideStatus.Pending);
            if (ride == null)
            {
                throw new RideNotFoundException(Messages.RideNotFoundOrNotAssignedToDriver);
            }

            if (ride.OTP != startRideRequest.OTP)
            {
                throw new InvalidOTPException(Messages.InvalidOTP);
            }

            ride.Status = RideStatus.Ongoing;
            ride.StartedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        /// <summary>
        /// Rates the rider after completing a ride.
        /// </summary>
        /// <param name="ratingRequest">The request containing the ride ID and rating.</param>
        /// <param name="driverEmail">The email address of the driver.</param>
        /// <exception cref="RatingAlreadyGivenException">Thrown if the driver has already rated the ride.</exception>
        /// <exception cref="RideNotFoundException">Thrown if the ride is not found or not associated with the driver.</exception>
        /// <exception cref="RideInProgressException">Thrown if the ride is not completed.</exception>
        public void RateRider(RatingRequest ratingRequest, string driverEmail)
        {
            Rating? driverAlreadyRated = _context.Ratings.FirstOrDefault(r => r.RideId == ratingRequest.RideId && r.RatedBy == UserType.Driver);
            if (driverAlreadyRated != null)
            {
                throw new RatingAlreadyGivenException(Messages.RatingAlreadyGiven);
            }

            User driver = GetDriverByEmail(driverEmail);

            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id == ratingRequest.RideId && r.DriverId == driver.Id);
            if (ride == null)
            {
                throw new RideNotFoundException(Messages.RideNotFoundOrNotBelongToDriver);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new RideInProgressException(Messages.CannotRateRideNotEnded);
            }

            Rating rating = RatingMapper.MapToRating(ratingRequest, UserType.Driver);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
    }
}
