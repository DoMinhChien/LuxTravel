using System.Linq;
using LuxTravel.Model.Entites.StoreProcedures;
using LuxTravel.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Model.Entites
{
    public class LuxTravelDBContext   : DbContext
    {
        public LuxTravelDBContext()
        {

        }
        public LuxTravelDBContext(DbContextOptions<LuxTravelDBContext> options)
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
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<BookingStatus> BookingStatuses { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<BookingDetail> BookingDetails { get; set; }

        public virtual DbSet<HotelRating> HotelRatings { get; set; }
        public virtual DbSet<SpGetListHotel> SpGetListHotel { get; set; }
        public virtual DbSet<SPGetRoomByHotel> SpGetRoomByHotels { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            string connectionStr = "Database=LuxTravelManagement;Trusted_Connection=True;";
          // string connectionStr = "Server=tcp:luxtravelserver.database.windows.net,1433;Initial Catalog=LuxTravelManagement;Persist Security Info=False;User ID=dominhchien206@luxtravelserver.database.windows.net;Password=Chien#2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
           modelBuilder.Entity<BookingDetail>().HasKey(x => new { x.BookingId, x.RoomId });
           modelBuilder.Entity<HotelRating>().HasKey(x => new { x.HotelId, x.RatorId });

            foreach (var property in modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetProperties())
               .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
           {
                property.SetColumnType("decimal(18,2)");


           }
        }
    }
}
