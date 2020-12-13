using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Models.Entities
{
    public class LuxTravelContext : DbContext
    {
        public LuxTravelContext()
        {
            
        }
        public LuxTravelContext(DbContextOptions<LuxTravelContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<CityDistrictMapping> CityDistrictMappings { get; set; }
        public virtual DbSet<CurrencySetting> CurrencySettings { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DistrictWardMapping> DistrictWardMappings { get; set; }
        public virtual DbSet<HotelLocation> HotelLocations { get; set; }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<RoomPrice> RoomPrices { get; set; }
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<BookingStatus> BookingStatuses { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Database=LuxTravelDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<BookingDetail>().HasKey(x => new { x.BookingId, x.RoomId });

        }
    }
}
