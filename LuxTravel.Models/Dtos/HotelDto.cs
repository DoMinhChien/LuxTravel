using System;
using System.Collections.Generic;
using LuxTravel.Models.Entities;

namespace LuxTravel.Models.Dtos
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
    }
}
