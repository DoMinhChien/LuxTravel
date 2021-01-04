using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class HotelDetailDto
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public Guid GuestId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestCount { get; set; }
        public Guid StatusId { get; set; }
        public IEnumerable<AvailableRoomDto> AvailableRooms { get; set; }
    }
}
