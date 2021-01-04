using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LuxTravel.Model.Dtos
{
    public class BookingDto
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public AvailableRoomDto SelectedRoom { get; set; }
        public decimal Totals { get; set; }
        public int GuestCount { get; set; }
        public int RoomCount { get; set; }
        public int NightCount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }

}
