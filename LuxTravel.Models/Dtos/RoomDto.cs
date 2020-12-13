using System;

namespace LuxTravel.Models.Dtos
{
    public class RoomDto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public Guid StatusId { get; set; }
    }
}
