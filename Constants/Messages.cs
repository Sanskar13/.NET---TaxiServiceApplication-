namespace TaxiServiceAPI.Constants
{
    public static class Messages
    {
        // User Messages
        public const string UserAlreadyExists = "User already exists.";
        public const string UserSignUpSuccess = "User signed up successfully.";
        public const string UserNotFound = "User not found.";
        public const string InvalidUsernameOrPassword = "Invalid Username or Password.";

        // Rider Messages
        public const string RiderNotFound = "Rider not found.";
        public const string RideNotFoundOrNotBelongsToRider = "Ride not found or does not belong to the rider.";

        // Driver Messages
        public const string DriverNotFound = "Driver not found.";
        public const string VehicleNotFoundForDriver = "Vehicle not found for the driver.";
        public const string DriverDetailsNotFound = "Driver details not found.";
        public const string CannotToggleAvailabilityWithPendingOrOngoingRides = "Cannot toggle availability while having pending or ongoing rides.";
        public const string DriverNowAvailable = "Driver is now available.";
        public const string DriverNowUnavailable = "Driver is now unavailable.";

        // Ride Messages
        public const string RideNotFound = "Ride not found.";
        public const string RideAlreadyInProgress = "Ride already in progress.";
        public const string NoAvailableDriver = "No available driver found.";
        public const string NoOngoingRide = "No ongoing ride available.";
        public const string OnlyPendingRidesCanBeCancelled = "Only rides in pending state can be cancelled.";
        public const string CannotEndRideNotStarted = "Cannot end a ride that has not started.";
        public const string RideNotFoundOrNotAssignedToDriver = "Ride not found or not assigned to the driver or not yet to start.";
        public const string InvalidOTP = "Invalid OTP.";
        public const string RideNotFoundOrNotBelongToDriver = "Ride not found or does not belong to the driver.";
        public const string CannotRateRideNotEnded = "Cannot rate a ride that has not ended.";
        public const string RideStartedSuccessfully = "Ride started successfully.";
        public const string RideCanceledSuccessfully = "Ride canceled successfully.";
        public const string RideEndedSuccessfully = "Ride ended successfully.";
        public const string RideDoesNotBelongToUser = "Ride does not belong to the user.";

        // Rating Messages
        public const string RatingAlreadyGiven = "Rating already given!!";
        public const string RideNotCompleted = "Cannot rate a ride that has not ended.";
        public const string RatingSubmittedSuccessfully = "Rating submitted successfully.";

        // Miscellaneous Messages
        public const string EmailClaimNotFound = "Email claim not found.";
        public const string NoOngoingRideAvailable = "No ongoing ride available.";
        public const string RiderDetailsNotFound = "Rider details not found.";
    }
}
