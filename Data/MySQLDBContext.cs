using Microsoft.EntityFrameworkCore;
using TaxiServiceAPI.Entities;

namespace TaxiServiceAPI.Data
{
    public class MySQLDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public MySQLDBContext(DbContextOptions<MySQLDBContext> options)
            : base(options)
        {
        }
    }
}
