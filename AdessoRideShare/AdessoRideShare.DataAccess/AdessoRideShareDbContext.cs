using AdessoRideShare.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdessoRideShare.DataAccess
{
    public class AdessoRideShareDbContext : DbContext
    {
        public AdessoRideShareDbContext()
        {
        }
        public AdessoRideShareDbContext(DbContextOptions<AdessoRideShareDbContext> options) : base(options)
        {
        }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<TripPassengers> TripPassengers { get; set; }
        public DbSet<TripDriver> TripDriver { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}