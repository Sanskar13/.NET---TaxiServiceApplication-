using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Exceptions;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Models.Request;

namespace TaxiServiceAPI.Services
{
    public class RideService: IRideService
    {
        private readonly MySQLDBContext _context;

        public RideService(MySQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cancels a ride based on the ride ID and user email.
        /// </summary>
        /// <param name="cancelRideRequest">The request containing the ride ID.</param>
        /// <param name="userEmail">The email address of the user.</param>
        /// <exception cref="UserNotFoundException">Thrown if the user is not found.</exception>
        /// <exception cref="RideInProgressException">Thrown if the ride does not belong to the user or is not in pending status.</exception>
        /// <exception cref="RideNotFoundException">Thrown if the ride is not found.</exception>
        public void CancelRide(RideIdRequest cancelRideRequest, string userEmail)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                throw new UserNotFoundException(Messages.UserNotFound);
            }

            Ride ride = GetRideById(cancelRideRequest.RideId);

            if (ride.RiderId != user.Id && ride.DriverId != user.Id)
            {
                throw new RideInProgressException(Messages.RideDoesNotBelongToUser);
            }

            if (ride.Status != RideStatus.Pending)
            {
                throw new RideNotFoundException(Messages.OnlyPendingRidesCanBeCancelled);
            }

            ride.Status = RideStatus.Cancelled;

            // make driver available again 
            Vehicle vehicleAvailable = _context.Vehicles.FirstOrDefault(v => v.UserId == ride.DriverId)
                ?? throw new VehicleNotFoundException("No vehicle found for the specified driver.");
            vehicleAvailable.IsAvailable = true;

            _context.SaveChanges();
        }

        /// <summary>
        /// Ends a ride based on the ride ID and user email.
        /// </summary>
        /// <param name="endRideRequest">The request containing the ride ID.</param>
        /// <param name="userEmail">The email address of the user.</param>
        /// <exception cref="UserNotFoundException">Thrown if the user is not found.</exception>
        /// <exception cref="UserNotFoundException">Thrown if the ride does not belong to the user or is not in ongoing status.</exception>
        /// <exception cref="RideNotFoundException">Thrown if the ride is not found.</exception>
        public void EndRide(RideIdRequest endRideRequest, string userEmail)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                throw new UserNotFoundException(Messages.UserNotFound);
            }

            Ride ride = GetRideById(endRideRequest.RideId);

            if (ride.RiderId != user.Id && ride.DriverId != user.Id)
            {
                throw new UserNotFoundException(Messages.RideDoesNotBelongToUser);
            }

            if (ride.Status != RideStatus.Ongoing)
            {
                throw new RideInProgressException(Messages.CannotEndRideNotStarted);
            }

            ride.Status = RideStatus.Completed;
            ride.EndAt = DateTime.UtcNow;

            // make driver available again 
            Vehicle vehicleAvailable = _context.Vehicles.FirstOrDefault(v => v.UserId == ride.DriverId)
                ?? throw new VehicleNotFoundException("No vehicle found for the specified driver.");
            vehicleAvailable.IsAvailable = true;

            _context.SaveChanges();
        }

        private Ride GetRideById(Guid rideId)
        {
            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id == rideId);
            if (ride == null)
            {
                throw new RideNotFoundException(Messages.RideNotFound);
            }
            return ride;
        }

    }
}

