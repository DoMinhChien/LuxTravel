using System;
using System.Collections.Generic;

namespace LuxTravel.Model.Dtos
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
        public Decimal SmallestPrice { get; set; }
        public int Reviews { get; set; }
        public decimal AvgRating { get; set; }
    }
}
