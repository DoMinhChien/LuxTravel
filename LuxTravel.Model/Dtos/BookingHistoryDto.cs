using System;

namespace LuxTravel.Model.Dtos
{
    public class BookingHistoryDto
    {
        public Guid BookingId { get; set; }
        public string Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string RoomName { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }

    }
}
