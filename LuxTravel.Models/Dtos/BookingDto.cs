using System;

namespace LuxTravel.Models.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public Guid GuestId { get; set; }
        public DateTime DateFrom  { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomCount { get; set; }
        public Guid StatusId { get; set; }
    }

}
