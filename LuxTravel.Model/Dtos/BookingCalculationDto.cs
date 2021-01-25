using System;

namespace LuxTravel.Model.Dtos
{
    public class BookingCalculationDto
    {
        public Guid HotelId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestCount { get; set; }
        public Guid RoomId { get; set; }
    }
}
