﻿namespace TaxiServiceAPI.Models.Response
{
    public class RidingDriverDetailResponse
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
